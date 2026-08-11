using ArcFaceSDK;
using ArcFaceSDK.Entity;
using ArcFaceSDK.SDKModels;
using HospitalEquipment.BLL;
using HospitalEquipment.Model;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace HospitalEquipmentSystem.UI.Login
{
    public partial class faceRecognition : UIForm
    {
        private readonly LoginBLL _bll = new LoginBLL();
        private readonly Timer _frameTimer;

        private FaceEngine _engine;
        private WebcamCapture _camera;
        private List<UserDto> _users = new List<UserDto>();
        private List<FaceTemplateItem> _templates = new List<FaceTemplateItem>();

        private bool _isRunning;
        private int _statusRefreshCount;
        private int _displayFrameCount;
        private float _bestSimilarity = -1f;

        public faceRecognition()
        {
            InitializeComponent();

            // 按需求不改 Designer/UI 布局，事件在代码里挂载。
            this.uiButton2.Click += UiButton2_Click;
            this.uiButton1.Click += UiButton1_Click;
            this.FormClosing += FaceRecognition_FormClosing;
            this.Shown += FaceRecognition_Shown;

            // 摄像头预览和识别共用同一帧定时器，避免两个定时器争抢同一帧。
            _frameTimer = new Timer();
            _frameTimer.Interval = 50;
            _frameTimer.Tick += FrameTimer_Tick;

            pictureBox1.Image = null;
            pictureBox1.BackColor = Color.FromArgb(11, 22, 34);
        }

        /// <summary>“开始人脸识别”按钮：只启动 ArcFace 识别；摄像头进入窗体后已经保持开启。</summary>
        private void UiButton2_Click(object sender, EventArgs e)
        {
            if (_isRunning)
            {
                StopRecognition();
                return;
            }

            uiButton2.Enabled = false;
            try
            {
                if (_camera == null || !_camera.IsRunning)
                {
                    if (!StartCameraOnly())
                        return;
                }

                uiLabel1.Text = "正在初始化人脸引擎...";
                if (!InitFaceEngine())
                    return;

                _users = _bll.GetLoginUsers();
                Dictionary<int, UserDto> userMap = new Dictionary<int, UserDto>();
                foreach (UserDto user in _users)
                {
                    if (!userMap.ContainsKey(user.UserId))
                        userMap.Add(user.UserId, user);
                }

                _templates = FaceTemplateStore.LoadAll(_engine, userMap);
                if (_templates.Count == 0)
                {
                    UIMessageBox.ShowWarning(
                        "尚未录入人脸。");
                    uiLabel1.Text = "未检测到已录入的人脸模板";
                    return;
                }

                _isRunning = true;
                _bestSimilarity = -1f;
                _statusRefreshCount = 0;
                _displayFrameCount = 0;
                uiLabel1.Text = "请正对摄像头，保持单人入镜";
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError("人脸识别启动失败：" + ex.Message);
                StopRecognition();
            }
            finally
            {
                uiButton2.Enabled = true;
            }
        }

        /// <summary>“返回密码登录”按钮：停止摄像头并关闭当前窗体。</summary>
        private void UiButton1_Click(object sender, EventArgs e)
        {
            StopRecognition();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FaceRecognition_Shown(object sender, EventArgs e)
        {
            // 窗体一打开就保持摄像头预览，点击“开始人脸识别”时只启动识别流程。
            StartCameraOnly();
        }

        private bool StartCameraOnly()
        {
            if (_camera != null && _camera.IsRunning)
            {
                _frameTimer.Start();
                return true;
            }

            try
            {
                uiLabel1.Text = "正在开启摄像头...";
                int cameraIndex = GetIntSetting("RGB_CAMERA_INDEX", 0);
                _camera = new WebcamCapture();
                _camera.Start(cameraIndex);
                _frameTimer.Start();
                uiLabel1.Text = "摄像头已开启，点击「开始人脸识别」";
                return true;
            }
            catch (Exception ex)
            {
                if (_camera != null)
                {
                    try
                    {
                        _camera.Dispose();
                    }
                    catch
                    {
                    }
                    _camera = null;
                }

                uiLabel1.Text = "摄像头启动失败：" + ex.Message;
                return false;
            }
        }

        private void FaceRecognition_FormClosing(object sender, FormClosingEventArgs e)
        {
            _frameTimer.Stop();
            StopRecognition();

            if (_engine != null)
            {
                try
                {
                    _engine.ASFUninitEngine();
                }
                catch
                {
                }
                _engine = null;
            }

            if (_camera != null)
            {
                try
                {
                    _camera.Dispose();
                }
                catch
                {
                }
                _camera = null;
            }
        }

        /// <summary>摄像头帧入口：未识别时只显示预览，开始识别后同帧送入 ArcFace。</summary>
        private void FrameTimer_Tick(object sender, EventArgs e)
        {
            if (_camera == null || !_camera.IsRunning)
                return;

            using (Bitmap frame = _camera.GetLatestFrame())
            {
                if (frame == null)
                    return;

                if (_isRunning)
                {
                    try
                    {
                        ProcessFrame(frame);
                    }
                    catch (Exception ex)
                    {
                        uiLabel1.Text = "识别过程出错：" + ex.Message;
                        StopRecognition();
                    }
                }
                else
                {
                    UpdatePreview(frame, null);
                }
            }
        }

        private void ProcessFrame(Bitmap frame)
        {
            int detectResult = _engine.ASFDetectFaces(frame, out MultiFaceInfo multiFaceInfo);
            if (detectResult != 0)
            {
                ShowStatusOnce("人脸检测失败，错误码：" + detectResult);
                UpdatePreview(frame, null);
                return;
            }

            if (multiFaceInfo == null || multiFaceInfo.faceNum <= 0)
            {
                ShowStatusOnce("未检测到人脸，请正对摄像头");
                UpdatePreview(frame, null);
                return;
            }

            if (multiFaceInfo.faceNum > 1)
            {
                ShowStatusOnce("检测到多张人脸，请单人入镜");
                UpdatePreview(frame, null);
                return;
            }

            // RGB 活体检测：只接受返回“真人”的帧，降低照片/视频攻击风险。
            int processResult = _engine.ASFProcess(frame, multiFaceInfo, FaceEngineMask.ASF_LIVENESS);
            if (processResult == 0)
            {
                int livenessResult = _engine.ASFGetLivenessScore(out LivenessInfo livenessInfo);
                if (livenessResult == 0 && livenessInfo != null &&
                    livenessInfo.num > 0 && livenessInfo.isLive[0] != 1)
                {
                    ShowStatusOnce("活体检测未通过，请使用本人真人脸");
                    UpdatePreview(frame, multiFaceInfo.faceRects[0]);
                    return;
                }
            }

            int extractResult = _engine.ASFFaceFeatureExtract(frame, multiFaceInfo, out FaceFeature liveFeature);
            if (extractResult != 0 || liveFeature == null)
            {
                ShowStatusOnce("人脸特征提取失败，错误码：" + extractResult);
                UpdatePreview(frame, multiFaceInfo.faceRects[0]);
                return;
            }

            UserDto matchedUser = MatchUser(liveFeature, out float similarity);
            if (matchedUser != null)
            {
                CompleteLogin(matchedUser, similarity);
                return;
            }

            _bestSimilarity = Math.Max(_bestSimilarity, similarity);
            ShowStatusOnce(string.Format("识别中...最高相似度 {0:P0}", Math.Max(0f, _bestSimilarity)));
            UpdatePreview(frame, multiFaceInfo.faceRects[0]);
        }

        private UserDto MatchUser(FaceFeature liveFeature, out float similarity)
        {
            similarity = -1f;
            FaceTemplateItem bestTemplate = null;
            float bestScore = -1f;

            foreach (FaceTemplateItem template in _templates)
            {
                if (template == null || template.Feature == null)
                    continue;

                int compareResult = _engine.ASFFaceFeatureCompare(
                    liveFeature,
                    template.Feature,
                    out float currentScore);
                if (compareResult == 0 && currentScore > bestScore)
                {
                    bestScore = currentScore;
                    bestTemplate = template;
                }
            }

            if (bestTemplate == null)
                return null;

            similarity = bestScore;
            float threshold = GetFloatSetting("FACE_SIMILARITY_THRESHOLD", 0.80f);
            if (bestScore < threshold)
                return null;

            foreach (UserDto user in _users)
            {
                if (user.UserId == bestTemplate.UserId)
                    return user;
            }

            return null;
        }

        private void CompleteLogin(UserDto user, float similarity)
        {
            _frameTimer.Stop();
            _isRunning = false;
            BLLResult result = _bll.LoginByFace(user.UserId, out UserDto refreshedUser);
            if (!result.Success)
            {
                uiLabel1.Text = result.Message;
                UIMessageBox.ShowError(result.Message);
                return;
            }

            LoginUser.SetUser(refreshedUser ?? user);
            uiLabel1.Text = string.Format("识别成功：{0}，相似度 {1:P1}", user.DisplayName, similarity);

            // 关闭本窗体后，LoginForm 检测到 DialogResult.OK 并完成主流程跳转。
            this.DialogResult = DialogResult.OK;
        }

        private void UpdatePreview(Bitmap frame, MRECT? faceRect)
        {
            // 画面只做运行期显示，不修改 Designer 中的控件布局。
            _displayFrameCount++;
            if (_displayFrameCount % 2 != 0)
                return;

            Bitmap display = (Bitmap)frame.Clone();
            using (Graphics graphics = Graphics.FromImage(display))
            {
                if (faceRect.HasValue)
                {
                    using (Pen pen = new Pen(Color.Lime, 2f))
                    {
                        MRECT rect = faceRect.Value;
                        int left = Math.Max(0, rect.left);
                        int top = Math.Max(0, rect.top);
                        int width = Math.Min(display.Width - left, rect.right - rect.left);
                        int height = Math.Min(display.Height - top, rect.bottom - rect.top);
                        graphics.DrawRectangle(pen, left, top, Math.Max(0, width), Math.Max(0, height));
                    }
                }
            }

            Image oldImage = pictureBox1.Image;
            pictureBox1.Image = display;
            oldImage?.Dispose();
        }

        private void ShowStatusOnce(string text)
        {
            _statusRefreshCount++;
            if (_statusRefreshCount % 5 == 1)
                uiLabel1.Text = text;
        }

        private bool InitFaceEngine()
        {
            if (_engine != null && _engine.GetEngineStatus())
                return true;

            string appId = ConfigurationManager.AppSettings["APPID"];
            string sdkKey = Environment.Is64BitProcess
                ? ConfigurationManager.AppSettings["SDKKEY64"]
                : ConfigurationManager.AppSettings["SDKKEY32"];

            if (string.IsNullOrEmpty(appId) || string.IsNullOrEmpty(sdkKey))
            {
                UIMessageBox.ShowError("ArcFace APPID 或 SDKKEY 未配置。");
                return false;
            }

            _engine = new FaceEngine();
            int activateResult = _engine.ASFOnlineActivation(appId, sdkKey);
            if (activateResult != 0 && activateResult != 90114)
            {
                UIMessageBox.ShowError("ArcFace 激活失败，错误码：" + activateResult);
                return false;
            }

            int combinedMask = FaceEngineMask.ASF_FACE_DETECT
                             | FaceEngineMask.ASF_FACERECOGNITION
                             | FaceEngineMask.ASF_LIVENESS;
            int initResult = _engine.ASFInitEngine(
                DetectionMode.ASF_DETECT_MODE_VIDEO,
                ASF_OrientPriority.ASF_OP_0_ONLY,
                16,
                10,
                combinedMask);
            if (initResult != 0)
            {
                UIMessageBox.ShowError("ArcFace 引擎初始化失败，错误码：" + initResult);
                return false;
            }

            float rgbThreshold = GetFloatSetting("RGB_LIVENESS_THRESHOLD", 0.5f);
            _engine.ASFSetLivenessParam(rgbThreshold);
            return true;
        }

        private void StopRecognition()
        {
            _isRunning = false;

            if (!this.IsDisposed)
                uiLabel1.Text = (_camera != null && _camera.IsRunning)
                    ? "摄像头已开启，点击「开始人脸识别」"
                    : "请正对摄像头，点击「开始人脸识别」";
        }

        private static int GetIntSetting(string key, int defaultValue)
        {
            string value = ConfigurationManager.AppSettings[key];
            return int.TryParse(value, out int result) ? result : defaultValue;
        }

        private static float GetFloatSetting(string key, float defaultValue)
        {
            string value = ConfigurationManager.AppSettings[key];
            return float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out float result)
                ? result
                : defaultValue;
        }

        private void uiLabel1_Click(object sender, EventArgs e)
        {
            // 保留 Designer 中已有的空点击事件，不做界面调整。
        }
    }
}
