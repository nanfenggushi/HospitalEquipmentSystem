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
            Application.Run(new EquipmentManagement());
        }
    }
}
