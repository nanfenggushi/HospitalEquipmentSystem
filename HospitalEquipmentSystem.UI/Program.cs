using System;                          // 引用基础命名空间，STAThread 特性在这里
using System.Windows.Forms;            // 引用 WinForms，Application 在这里

namespace HospitalEquipmentSystem.UI    // 声明当前代码所属的命名空间（界面层）
{
    /// <summary>
    /// 程序入口类：整个应用程序从这里开始运行
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点
        /// </summary>
        [STAThread]                      // 声明程序使用单线程模型（WinForms 要求）
        static void Main()
        {
            // 开启 Windows 视觉样式，让控件外观和系统一致
            Application.EnableVisualStyles();

            // 设置控件文本渲染方式为兼容模式
            Application.SetCompatibleTextRenderingDefault(false);

            // 启动主窗体（设备监控中心），程序运行期间会一直停留在这里
            Application.Run(new SwitchPages());
        }
    }
}
