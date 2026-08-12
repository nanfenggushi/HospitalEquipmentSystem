using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace HospitalEquipment.BLL
{
    public class OpenAiFaultRecognizer : IFaultRecognizer
    {
        private static readonly HttpClient s_httpClient = new HttpClient();
        private readonly string _apiKey;
        private readonly string _apiUrl;
        private readonly string _model;

        public OpenAiFaultRecognizer()
        {
            _apiKey = ConfigurationManager.AppSettings["AiApiKey"] ?? "";
            _apiUrl = ConfigurationManager.AppSettings["AiApiUrl"] ?? "https://api.openai.com/v1/chat/completions";
            _model = ConfigurationManager.AppSettings["AiModel"] ?? "gpt-4o";
        }

        public OpenAiFaultRecognizer(string apiKey, string apiUrl, string model)
        {
            _apiKey = apiKey ?? "";
            _apiUrl = apiUrl ?? "https://api.openai.com/v1/chat/completions";
            _model = model ?? "gpt-4o";
        }

        public async Task<FaultRecognitionResult> RecognizeAsync(byte[] photoData, List<string> candidateFaultTypes)
        {
            if (string.IsNullOrEmpty(_apiKey))
                return new FaultRecognitionResult { FaultType = "未配置AI密钥", Confidence = 0 };

            if (photoData == null || photoData.Length == 0)
                return new FaultRecognitionResult { FaultType = "未识别", Confidence = 0 };

            if (candidateFaultTypes == null || candidateFaultTypes.Count == 0)
                return new FaultRecognitionResult { FaultType = "无可用故障类型", Confidence = 0 };

            try
            {
                // 压缩图片：最大边长1024px，JPEG质量80%，减少base64体积
                byte[] compressedData = CompressImage(photoData);
                string base64Image = Convert.ToBase64String(compressedData);
                string faultList = string.Join("、", candidateFaultTypes);

                var requestBody = new
                {
                    model = _model,
                    messages = new object[]
                    {
                        new
                        {
                            role = "system",
                            content = $"你是一个医疗设备故障诊断专家。请根据设备照片，从以下候选故障类型中选择最匹配的一项：{faultList}。你必须从上述候选中选择一个，强制给出结果，即使不确定也要选最可能的。请只回复故障类型名称，不要添加任何解释。"
                        },
                        new
                        {
                            role = "user",
                            content = new object[]
                            {
                                new { type = "text", text = "请识别这张设备照片中的故障类型。" },
                                new
                                {
                                    type = "image_url",
                                    image_url = new { url = $"data:image/jpeg;base64,{base64Image}" }
                                }
                            }
                        }
                    },
                    temperature = 0.3
                };

                string json = new JavaScriptSerializer().Serialize(requestBody);

                var request = new HttpRequestMessage(HttpMethod.Post, _apiUrl)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };
                request.Headers.Add("Authorization", $"Bearer {_apiKey}");

                var response = await s_httpClient.SendAsync(request).ConfigureAwait(false);
                string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    return new FaultRecognitionResult { FaultType = "未识别", Confidence = 0 };
                }

                string aiText = ExtractContent(responseBody);

                foreach (var candidate in candidateFaultTypes)
                {
                    if (aiText.Contains(candidate))
                        return new FaultRecognitionResult { FaultType = candidate, Confidence = 0.85m };
                }

                return new FaultRecognitionResult { FaultType = "未识别", Confidence = 0 };
            }
            catch
            {
                return new FaultRecognitionResult { FaultType = "未识别", Confidence = 0 };
            }
        }

        /// <summary>
        /// 压缩图片：限制最大边长为1024px，JPEG质量80%
        /// </summary>
        private static byte[] CompressImage(byte[] imageData)
        {
            try
            {
                using (var ms = new MemoryStream(imageData))
                using (var img = Image.FromStream(ms))
                {
                    int maxSize = 1024;
                    int width = img.Width;
                    int height = img.Height;

                    if (width > maxSize || height > maxSize)
                    {
                        double ratio = Math.Min((double)maxSize / width, (double)maxSize / height);
                        width = (int)(width * ratio);
                        height = (int)(height * ratio);
                    }

                    using (var bmp = new Bitmap(width, height))
                    using (var g = Graphics.FromImage(bmp))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.DrawImage(img, 0, 0, width, height);

                        using (var outMs = new MemoryStream())
                        {
                            var encoder = ImageCodecInfo.GetImageEncoders()
                                .FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);
                            var encParams = new EncoderParameters(1);
                            encParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 80L);

                            if (encoder != null)
                                bmp.Save(outMs, encoder, encParams);
                            else
                                bmp.Save(outMs, ImageFormat.Jpeg);

                            return outMs.ToArray();
                        }
                    }
                }
            }
            catch
            {
                // 压缩失败时返回原始数据
                return imageData;
            }
        }

        private static string ExtractContent(string responseJson)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                var obj = serializer.Deserialize<Dictionary<string, object>>(responseJson);

                if (obj.TryGetValue("choices", out object choicesObj)
                    && choicesObj is System.Collections.ArrayList choices
                    && choices.Count > 0
                    && choices[0] is Dictionary<string, object> firstChoice
                    && firstChoice.TryGetValue("message", out object msgObj)
                    && msgObj is Dictionary<string, object> message
                    && message.TryGetValue("content", out object content))
                {
                    return content?.ToString()?.Trim() ?? "";
                }
            }
            catch { }
            return "";
        }
    }
}
