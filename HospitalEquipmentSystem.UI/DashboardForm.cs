using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using Sunny.UI;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace HospitalEquipmentSystem.UI
{
    /// <summary>
    /// 首页仪表盘：内嵌山海鲸大屏页面（WebView2 内核）
    /// 大屏地址可在 App.config 的 appSettings/DashboardUrl 中配置
    /// </summary>
    public partial class DashboardForm : UIForm
    {
        /// <summary>
        /// 构造函数：创建窗体时调用一次
        /// </summary>
        public DashboardForm()
        {
            InitializeComponent();
            this.Load += DashboardForm_Load;
        }

        /// <summary>
        /// 窗体加载事件：初始化 WebView2 并导航到大屏地址
        /// WebView2 初始化是异步的，放在 Load 事件中执行
        /// </summary>
        private async void DashboardForm_Load(object sender, EventArgs e)
        {
            try
            {
                // 如果页面已经加载完成则无需重复初始化
                if (webView.CoreWebView2 != null)
                {
                    webView.Reload();
                    return;
                }

                string url = GetDashboardUrl();

                // 初始化 WebView2 环境（异步），失败时抛出异常
                await webView.EnsureCoreWebView2Async();

                // 隐藏默认标题栏，让大屏铺满整个面板
                webView.CoreWebView2.Settings.AreDefaultContextMenusEnabled = true;
                webView.CoreWebView2.Settings.IsStatusBarEnabled = false;

                webView.CoreWebView2.Navigate(url);
            }
            catch (Exception ex)
            {
                UIMessageBox.Show("大屏加载失败：" + ex.Message + Environment.NewLine +
                    "请确认山海鲸服务已启动，且在 App.config 中正确配置了 DashboardUrl。", "错误", UIStyle.Red);
            }
        }

        /// <summary>
        /// 从配置文件读取大屏地址，未配置时使用默认地址
        /// </summary>
        private static string GetDashboardUrl()
        {
            string url = ConfigurationManager.AppSettings["DashboardUrl"];
            if (string.IsNullOrWhiteSpace(url))
            {
                url = "http://192.168.0.41:10000/zv20wvhk2eigabps38wdktnw22dde0nz/";
            }
            return url;
        }
    }
}
