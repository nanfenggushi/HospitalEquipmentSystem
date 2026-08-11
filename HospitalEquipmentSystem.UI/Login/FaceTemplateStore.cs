using ArcFaceSDK;
using ArcFaceSDK.Entity;
using ArcFaceSDK.Utils;
using HospitalEquipment.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

namespace HospitalEquipmentSystem.UI.Login
{
    /// <summary>
    /// 一条已登记的人脸模板：用户 ID + ArcFace 特征。
    /// </summary>
    internal sealed class FaceTemplateItem
    {
        public int UserId { get; set; }

        public string SourceFile { get; set; }

        public FaceFeature Feature { get; set; }
    }

    /// <summary>
    /// 人脸模板加载与保存。
    ///
    /// 当前数据库没有人脸特征字段，所以这里使用本地 FaceTemplates 目录作为特征来源。
    /// 文件命名规则：
    ///   1. 图片模板：FaceTemplates\1.jpg / 1_张三.png，文件名的第一个数字必须是 UserId。
    ///   2. 特征模板：FaceTemplates\1.dat，由 SaveFeature 生成，读取更快。
    /// 运行目录中没有 FaceTemplates 时，会提示“尚未录入人脸”。
    /// </summary>  
    internal static class FaceTemplateStore
    {
        private static readonly Regex UserIdPrefix = new Regex(@"^\s*(\d+)", RegexOptions.Compiled);
        private static readonly string Header = "ARCFACE1";

        public static string DirectoryPath
        {
            get
            {
                string configured = ConfigurationManager.AppSettings["FaceTemplatePath"];
                if (string.IsNullOrWhiteSpace(configured))
                    return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FaceTemplates");

                return Path.IsPathRooted(configured)
                    ? Path.GetFullPath(configured)
                    : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configured);
            }
        }

        /// <summary>
        /// 加载所有可识别用户的人脸模板。
        /// 图片模板会在加载时用 ArcFace 提取一次特征；.dat 模板直接反序列化。
        /// </summary>
        public static List<FaceTemplateItem> LoadAll(FaceEngine engine, Dictionary<int, UserDto> users)
        {
            List<FaceTemplateItem> templates = new List<FaceTemplateItem>();
            if (engine == null || users == null || !Directory.Exists(DirectoryPath))
                return templates;

            foreach (string file in Directory.EnumerateFiles(DirectoryPath, "*", SearchOption.AllDirectories))
            {
                try
                {
                    string extension = Path.GetExtension(file).ToLowerInvariant();
                    int userId = ParseUserId(file);
                    if (userId <= 0 || !users.ContainsKey(userId))
                        continue;

                    FaceFeature feature = null;
                    if (extension == ".dat")
                    {
                        feature = ReadFeatureFile(file, userId);
                    }
                    else if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".bmp")
                    {
                        feature = ExtractFeatureFromImage(engine, file);
                    }

                    if (feature != null && feature.feature != null && feature.feature.Length > 0)
                    {
                        templates.Add(new FaceTemplateItem
                        {
                            UserId = userId,
                            SourceFile = file,
                            Feature = feature
                        });
                    }
                }
                catch
                {
                    // 单个模板损坏时跳过，不影响其他用户的人脸登录。
                }
            }

            return templates;
        }

        /// <summary>
        /// 保存 ArcFace 特征到 FaceTemplates\{userId}.dat，供后续注册页面复用。
        /// </summary>
        public static bool SaveFeature(int userId, FaceFeature feature)
        {
            if (userId <= 0 || feature == null || feature.feature == null || feature.feature.Length == 0)
                return false;

            try
            {
                Directory.CreateDirectory(DirectoryPath);
                string file = Path.Combine(DirectoryPath, userId + ".dat");

                using (FileStream stream = new FileStream(file, FileMode.Create, FileAccess.Write))
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(Header);
                    writer.Write(userId);
                    writer.Write(feature.feature.Length);
                    writer.Write(feature.feature);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static FaceFeature ReadFeatureFile(string file, int expectedUserId)
        {
            using (FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                string header = reader.ReadString();
                if (header != Header)
                    return null;

                int userId = reader.ReadInt32();
                if (userId != expectedUserId)
                    return null;

                int length = reader.ReadInt32();
                if (length <= 0 || length > 1024 * 1024)
                    return null;

                byte[] bytes = reader.ReadBytes(length);
                if (bytes.Length != length)
                    return null;

                return new FaceFeature
                {
                    featureSize = length,
                    feature = bytes
                };
            }
        }

        private static FaceFeature ExtractFeatureFromImage(FaceEngine engine, string file)
        {
            using (Image image = Image.FromFile(file))
            using (Image templateImage = NormalizeTemplateImage(image))
            {
                int detectResult = engine.ASFDetectFaces(templateImage, out MultiFaceInfo multiFaceInfo);
                if (detectResult != 0 || multiFaceInfo == null || multiFaceInfo.faceNum <= 0)
                    return null;

                int extractResult = engine.ASFFaceFeatureExtract(templateImage, multiFaceInfo, out FaceFeature feature);
                return extractResult == 0 ? feature : null;
            }
        }

        /// <summary>
        /// ArcFace 对超大照片可能返回 90127（图像尺寸/检测参数不在支持范围），
        /// 所以录入照片超过阈值时先等比缩放，保证能正常检测和提取特征。
        /// </summary>
        private static Image NormalizeTemplateImage(Image source)
        {
            const int maxWidth = 800;
            const int maxHeight = 1000;

            if (source.Width <= maxWidth && source.Height <= maxHeight)
                return (Image)source.Clone();

            return ImageUtil.ScaleImage(
                source,
                Math.Min(source.Width, maxWidth),
                Math.Min(source.Height, maxHeight));
        }

        private static int ParseUserId(string file)
        {
            string fileName = Path.GetFileNameWithoutExtension(file);
            Match match = UserIdPrefix.Match(fileName);
            return match.Success ? int.Parse(match.Groups[1].Value) : 0;
        }
    }
}
