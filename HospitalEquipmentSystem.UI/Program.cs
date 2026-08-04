using System;
using System.IO;
using System.Windows.Forms;

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

            // 设置控件文本渲染方式为兼容模式
            Application.SetCompatibleTextRenderingDefault(false);

            // 全局异常捕获必须在进入消息循环前注册，否则不会生效
            Application.ThreadException += OnThreadException;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            // 先登录：取消则直接退出
            using (var login = new LoginForm())
            {
                if (login.ShowDialog() != DialogResult.OK) return;
            }

            // 登录成功后进入带侧边栏的页面切换外壳（SwitchPages）
            Application.Run(new SwitchPages());
        }

        private static void OnThreadException(object sender, System.Threading.ThreadExceptionEventArgs e) {
            LogCrash(e.Exception);
            MessageBox.Show("发生错误：" + e.Exception.Message + Environment.NewLine + "详情已写入 crash.log",
                "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e) {
            LogCrash(e.ExceptionObject as Exception);
        }

        private static void LogCrash(Exception ex) {
            try {
                string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "crash.log");
                string text = string.Format("[{0:yyyy-MM-dd HH:mm:ss}] {1}{2}{2}----------------------------------------{2}",
                    DateTime.Now,
                    ex == null ? "未知异常" : ex.ToString(),
                    Environment.NewLine);
                File.AppendAllText(file, text);
            }
            catch {
            }
        }
    }
}
