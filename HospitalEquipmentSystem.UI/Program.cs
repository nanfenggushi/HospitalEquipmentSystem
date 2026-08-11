using HospitalEquipmentSystem.UI.Dashboard;
using HospitalEquipmentSystem.UI.register;
using System;                          // 引用基础命名空间，STAThread 特性在这里
using System.IO;
using System.Windows.Forms;            // 引用 WinForms，Application 在这里

namespace HospitalEquipmentSystem.UI
{
    /// <summary>
    /// 程序入口类：整个应用程序从这里开始运行
    /// </summary>
    internal static class Program
        {
        /// <summary>
        /// 应用程序的主入口点
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 开启 Windows 视觉样式，让控件外观和系统一致
            Application.EnableVisualStyles();

            ApiService.Start();

            // 全局异常捕获必须在进入消息循环前注册，否则不会生效
            Application.ThreadException += OnThreadException;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            // 循环：登录 → 主面板 → 退出登录 → 重新登录
            while (true)
            {
                LoginForm loginForm = new LoginForm();
                loginForm.StartPosition = FormStartPosition.CenterScreen;
                if (loginForm.ShowDialog() != DialogResult.OK)
                    return; // 用户关闭登录窗口，直接退出

                // 登录成功，进入主面板
                using (var mainForm = new RegisterForm())
                {
                    Application.Run(mainForm);
                }

                // 主面板关闭后，如果登录状态已被清除（退出登录），继续循环重新登录；
                // 如果登录状态仍存在（异常关闭），则退出程序
                if (LoginUser.UserId == 0)
                    continue; // 正常退出登录，回到登录界面
                else
                    return; // 异常关闭，直接退出
            }
        }

        private static void OnThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            LogCrash(e.Exception);
            MessageBox.Show("发生错误：" + e.Exception.Message + Environment.NewLine + "详情已写入 crash.log",
                "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            LogCrash(e.ExceptionObject as Exception);
        }

        private static void LogCrash(Exception ex)
        {
            try
            {
                string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "crash.log");
                string text = string.Format("[{0:yyyy-MM-dd HH:mm:ss}] {1}{2}{2}----------------------------------------{2}",
                    DateTime.Now,
                    ex == null ? "未知异常" : ex.ToString(),
                    Environment.NewLine);
                File.AppendAllText(file, text);
            }
            catch
            {
            }
        }
    }
}
