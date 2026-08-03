using HospitalEquipmentSystem.UI.Dashboard;
using System;
using System.IO;
using System.Windows.Forms;

namespace HospitalEquipmentSystem.UI
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += OnThreadException;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            Application.Run(new Equipment_BorrowingUI());
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
