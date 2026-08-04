using HospitalEquipmentSystem.UI.Dashboard;
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

            // 设置控件文本渲染方式为兼容模式
            Application.SetCompatibleTextRenderingDefault(false);

            // 启动主窗体（设备监控中心），程序运行期间会一直停留在这里
            Application.Run(new UcDashboard());
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
