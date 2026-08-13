using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace HospitalEquipment.Common
{
    public class R2Helper
    {
        // ================= 从 App.config 动态读取配置 =================
        private static readonly string AccountId = ConfigurationManager.AppSettings["R2_AccountId"];
        private static readonly string AccessKey = ConfigurationManager.AppSettings["R2_AccessKey"];
        private static readonly string SecretKey = ConfigurationManager.AppSettings["R2_SecretKey"];
        private static readonly string BucketName = ConfigurationManager.AppSettings["R2_BucketName"];
        private static readonly string PublicDomain = ConfigurationManager.AppSettings["R2_PublicDomain"];
        // ==============================================================

        /// <summary>
        /// 异步上传图片文件到 Cloudflare R2
        /// </summary>
        /// <param name="localFilePath">本地文件绝对路径</param>
        /// <param name="folder">R2 中的文件夹名，默认 avatars</param>
        /// <returns>上传成功后的公网 URL</returns>
        public static async Task<string> UploadImageAsync(string localFilePath, string folder = "avatars")
        {
            if (!File.Exists(localFilePath))
                throw new FileNotFoundException("找不到要上传的文件", localFilePath);

            // Cloudflare R2 的固定 Endpoint 格式
            string serviceUrl = $"https://{AccountId}.r2.cloudflarestorage.com";

            // 配置 S3 客户端，指向 R2 Endpoint
            var config = new AmazonS3Config {
                ServiceURL = serviceUrl,
            };

            // 1. 生成新的文件名和 R2 上的存储路径 (Key)
            string extension = Path.GetExtension(localFilePath).ToLower();
            string newFileName = Guid.NewGuid().ToString("N") + extension;
            string objectKey = $"{folder}/{DateTime.Now:yyyyMM}/{newFileName}";

            try
            {
                using (var s3Client = new AmazonS3Client(AccessKey, SecretKey, config))
                {
                    var request = new PutObjectRequest {
                        BucketName = BucketName,
                        Key = objectKey,
                        FilePath = localFilePath,
                        // 【重要细节】：设置正确的 ContentType，否则浏览器打开 URL 会变成下载，而不是直接显示图片
                        ContentType = GetContentType(extension),
                        // 禁用负载签名在 R2 中可以提升一点性能，并且有时能解决兼容性问题
                        DisablePayloadSigning = true
                    };

                    // 2. 执行上传
                    PutObjectResponse response = await s3Client.PutObjectAsync(request);

                    // HTTP 状态码 200 表示 S3/R2 接收成功
                    if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    {
                        // 3. 拼接并返回最终的公网访问地址
                        return PublicDomain + objectKey;
                    } else
                    {
                        throw new Exception($"上传失败，HTTP状态码: {response.HttpStatusCode}");
                    }
                }
            } catch (AmazonS3Exception s3Ex)
            {
                throw new Exception($"R2 服务端错误: {s3Ex.Message}", s3Ex);
            } catch (Exception ex)
            {
                throw new Exception($"文件上传发生异常: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 根据文件扩展名获取 MIME 类型
        /// </summary>
        private static string GetContentType(string extension)
        {
            switch (extension)
            {
                case ".jpg":
                case ".jpeg": return "image/jpeg";
                case ".png": return "image/png";
                case ".bmp": return "image/bmp";
                case ".gif": return "image/gif";
                case ".webp": return "image/webp";
                default: return "application/octet-stream";
            }
        }
    }
}
