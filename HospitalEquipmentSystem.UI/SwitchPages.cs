using HospitalEquipment.Util;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalEquipmentSystem.UI
{
    /// <summary>
    /// 页面切换窗体：用于各功能页面的切换入口
    /// </summary>
    public partial class SwitchPages : UIForm
    {
        /// <summary>
        /// 侧边栏是否展开
        /// </summary>
        private bool sideBarExpanded = true;

        /// <summary>
        /// 侧边栏展开时的宽度
        /// </summary>
        private const int SideBarWidth = 200;

        /// <summary>
        /// 侧边栏收起时的宽度
        /// </summary>
        private const int SideBarCollapsedWidth = 60;

        /// <summary>
        /// 当前选中的菜单按钮
        /// </summary>
        private Button currentSelectedButton;

        /// <summary>
        /// 初始窗体宽度（用于缩放计算）
        /// </summary>
        private float initialWidth;

        /// <summary>
        /// 初始窗体高度（用于缩放计算）
        /// </summary>
        private float initialHeight;

        /// <summary>
        /// 初始侧边栏展开宽度
        /// </summary>
        private int initialSideBarWidth;

        /// <summary>
        /// 初始侧边栏收起宽度
        /// </summary>
        private int initialSideBarCollapsedWidth;

        /// <summary>
        /// 初始菜单按钮高度
        /// </summary>
        private int initialMenuButtonHeight;

        /// <summary>
        /// 初始菜单字体大小
        /// </summary>
        private float initialMenuFontSize;

        /// <summary>
        /// 初始顶部栏高度
        /// </summary>
        private int initialTopBarHeight;

        /// <summary>
        /// 初始标题字体大小
        /// </summary>
        private float initialTitleFontSize;

        /// <summary>
        /// 构造函数：初始化窗体控件
        /// </summary>
        public SwitchPages()
        {
            InitializeComponent();
            this.Resize += new EventHandler(SwitchPages_Resize);
        }

        /// <summary>
        /// 窗体加载事件：窗体显示时执行
        /// 当前为空实现，后续可在这里做页面初始化
        /// </summary>
        private void SwitchPages_Load(object sender, EventArgs e)
        {
            // 记录初始大小，用于缩放计算
            initialWidth = this.ClientSize.Width;
            initialHeight = this.ClientSize.Height;
            initialSideBarWidth = SideBarWidth;
            initialSideBarCollapsedWidth = SideBarCollapsedWidth;
            initialMenuButtonHeight = btnDashboard.Height;
            initialMenuFontSize = btnDashboard.Font.Size;
            initialTopBarHeight = topPanel.Height;
            initialTitleFontSize = lblTitle.Font.Size;

            // 初始化默认选中设备管理
            currentSelectedButton = btnDeviceManage;
            UpdateMenuSelection(btnDeviceManage);
            ShowWelcomePage();
        }

        /// <summary>
        /// 窗体大小改变事件：自动缩放侧边栏和文字
        /// </summary>
        private void SwitchPages_Resize(object sender, EventArgs e)
        {
            if (initialWidth == 0 || initialHeight == 0) return;

            // 计算缩放比例（取宽高比例中较小的，保持整体协调）
            float scaleX = (float)this.ClientSize.Width / initialWidth;
            float scaleY = (float)this.ClientSize.Height / initialHeight;
            float scale = Math.Min(scaleX, scaleY);

            // 限制最小缩放比例，防止太小看不清
            if (scale < 0.5f) scale = 0.5f;

            // 缩放侧边栏宽度
            int newSideBarWidth = sideBarExpanded
                ? (int)(initialSideBarWidth * scale)
                : (int)(initialSideBarCollapsedWidth * scale);
            sidePanel.Width = newSideBarWidth;

            // 缩放顶部栏高度
            int newTopBarHeight = (int)(initialTopBarHeight * scale);
            topPanel.Height = newTopBarHeight;
            btnMenu.Height = newTopBarHeight;
            btnNotify.Height = newTopBarHeight;
            btnExit.Height = newTopBarHeight;

            // 缩放标题字体
            float newTitleFontSize = initialTitleFontSize * scale;
            lblTitle.Font = new Font(lblTitle.Font.FontFamily, newTitleFontSize, lblTitle.Font.Style);
            lblUserName.Font = new Font(lblUserName.Font.FontFamily, newTitleFontSize * 0.8f, lblUserName.Font.Style);

            // 缩放菜单按钮高度和字体
            int newButtonHeight = (int)(initialMenuButtonHeight * scale);
            float newFontSize = initialMenuFontSize * scale;

            UpdateMenuButtonSize(btnDashboard, newButtonHeight, newFontSize);
            UpdateMenuButtonSize(btnDeviceManage, newButtonHeight, newFontSize);
            UpdateMenuButtonSize(btnRepairManage, newButtonHeight, newFontSize);
            UpdateMenuButtonSize(btnBorrowManage, newButtonHeight, newFontSize);
            UpdateMenuButtonSize(btnMonitoringCenter, newButtonHeight, newFontSize);
            UpdateMenuButtonSize(btnDataStatistics, newButtonHeight, newFontSize);
            UpdateMenuButtonSize(btnSystemSetting, newButtonHeight, newFontSize);
        }

        /// <summary>
        /// 更新菜单按钮大小和字体
        /// </summary>
        private void UpdateMenuButtonSize(Button button, int height, float fontSize)
        {
            button.Height = height;
            button.Font = new Font(button.Font.FontFamily, fontSize, button.Font.Style);
        }

        /// <summary>
        /// 菜单按钮点击事件：切换侧边栏展开/收起
        /// </summary>
        private void btnMenu_Click(object sender, EventArgs e)
        {
            ToggleSideBar();
        }

        /// <summary>
        /// 切换侧边栏展开/收起状态
        /// </summary>
        private void ToggleSideBar()
        {
            // 计算当前缩放比例
            float scaleX = (float)this.ClientSize.Width / initialWidth;
            float scaleY = (float)this.ClientSize.Height / initialHeight;
            float scale = Math.Min(scaleX, scaleY);
            if (scale < 0.5f) scale = 0.5f;

            if (sideBarExpanded)
            {
                // 收起侧边栏
                sidePanel.Width = (int)(initialSideBarCollapsedWidth * scale);
                btnDashboard.Text = "🏠";
                btnDeviceManage.Text = "📦";
                btnRepairManage.Text = "🔧";
                btnBorrowManage.Text = "↕";
                btnMonitoringCenter.Text = "📈";
                btnDataStatistics.Text = "📊";
                btnSystemSetting.Text = "⚙";
                btnDashboard.TextAlign = ContentAlignment.MiddleCenter;
                btnDeviceManage.TextAlign = ContentAlignment.MiddleCenter;
                btnRepairManage.TextAlign = ContentAlignment.MiddleCenter;
                btnBorrowManage.TextAlign = ContentAlignment.MiddleCenter;
                btnMonitoringCenter.TextAlign = ContentAlignment.MiddleCenter;
                btnDataStatistics.TextAlign = ContentAlignment.MiddleCenter;
                btnSystemSetting.TextAlign = ContentAlignment.MiddleCenter;
            }
            else
            {
                // 展开侧边栏
                sidePanel.Width = (int)(initialSideBarWidth * scale);
                btnDashboard.Text = "  🏠  首页仪表盘";
                btnDeviceManage.Text = "  📦  设备管理";
                btnRepairManage.Text = "  🔧  维修管理";
                btnBorrowManage.Text = "  ↕  借用管理";
                btnMonitoringCenter.Text = "  📈  监控中心";
                btnDataStatistics.Text = "  📊  数据统计";
                btnSystemSetting.Text = "  ⚙  系统设置";
                btnDashboard.TextAlign = ContentAlignment.MiddleLeft;
                btnDeviceManage.TextAlign = ContentAlignment.MiddleLeft;
                btnRepairManage.TextAlign = ContentAlignment.MiddleLeft;
                btnBorrowManage.TextAlign = ContentAlignment.MiddleLeft;
                btnMonitoringCenter.TextAlign = ContentAlignment.MiddleLeft;
                btnDataStatistics.TextAlign = ContentAlignment.MiddleLeft;
                btnSystemSetting.TextAlign = ContentAlignment.MiddleLeft;
            }
            sideBarExpanded = !sideBarExpanded;
        }

        /// <summary>
        /// 更新菜单选中状态
        /// </summary>
        private void UpdateMenuSelection(Button selectedButton)
        {
            // 重置所有按钮样式
            ResetButtonStyle(btnDashboard);
            ResetButtonStyle(btnDeviceManage);
            ResetButtonStyle(btnRepairManage);
            ResetButtonStyle(btnBorrowManage);
            ResetButtonStyle(btnMonitoringCenter);
            ResetButtonStyle(btnDataStatistics);
            ResetButtonStyle(btnSystemSetting);

            // 设置选中按钮样式
            SetButtonSelectedStyle(selectedButton);
            currentSelectedButton = selectedButton;
        }

        /// <summary>
        /// 重置按钮为未选中样式
        /// </summary>
        private void ResetButtonStyle(Button button)
        {
            button.BackColor = Color.Transparent;
            button.Font = new Font("微软雅黑", 12F, FontStyle.Regular);
        }

        /// <summary>
        /// 设置按钮为选中样式
        /// </summary>
        private void SetButtonSelectedStyle(Button button)
        {
            button.BackColor = Color.FromArgb(30, 50, 90);
            button.Font = new Font("微软雅黑", 12F, FontStyle.Bold);
        }

        /// <summary>
        /// 首页仪表盘菜单点击
        /// </summary>
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            UpdateMenuSelection(btnDashboard);
            lblTitle.Text = "首页仪表盘";
            ShowDashboardPage();
        }

        /// <summary>
        /// 设备管理菜单点击
        /// </summary>
        private void btnDeviceManage_Click(object sender, EventArgs e)
        {
            UpdateMenuSelection(btnDeviceManage);
            lblTitle.Text = "设备管理";
            ShowDeviceManagePage();
        }

        /// <summary>
        /// 维修管理菜单点击
        /// </summary>
        private void btnRepairManage_Click(object sender, EventArgs e)
        {
            UpdateMenuSelection(btnRepairManage);
            lblTitle.Text = "维修管理";
            ShowRepairManagePage();
        }

        /// <summary>
        /// 借用管理菜单点击
        /// </summary>
        private void btnBorrowManage_Click(object sender, EventArgs e)
        {
            UpdateMenuSelection(btnBorrowManage);
            lblTitle.Text = "借用管理";
            ShowBorrowManagePage();
        }

        /// <summary>
        /// 监控中心菜单点击
        /// </summary>
        private void btnMonitoringCenter_Click(object sender, EventArgs e)
        {
            UpdateMenuSelection(btnMonitoringCenter);
            lblTitle.Text = "监控中心";
            ShowMonitoringCenterPage();
        }

        /// <summary>
        /// 数据统计菜单点击
        /// </summary>
        private void btnDataStatistics_Click(object sender, EventArgs e)
        {
            UpdateMenuSelection(btnDataStatistics);
            lblTitle.Text = "数据统计";
            ShowDataStatisticsPage();
        }

        /// <summary>
        /// 系统设置菜单点击
        /// </summary>
        private void btnSystemSetting_Click(object sender, EventArgs e)
        {
            UpdateMenuSelection(btnSystemSetting);
            lblTitle.Text = "系统设置";
            ShowSystemSettingPage();
        }

        /// <summary>
        /// 显示欢迎页面
        /// </summary>
        private void ShowWelcomePage()
        {
            contentPanel.Controls.Clear();
            Label lblWelcome = new Label();
            lblWelcome.Text = "欢迎使用医院设备管理系统";
            lblWelcome.Font = new Font("微软雅黑", 24F, FontStyle.Bold);
            lblWelcome.ForeColor = Color.FromArgb(64, 64, 64);
            lblWelcome.Dock = DockStyle.Fill;
            lblWelcome.TextAlign = ContentAlignment.MiddleCenter;
            contentPanel.Controls.Add(lblWelcome);
        }

        /// <summary>
        /// 显示首页仪表盘页面
        /// </summary>
        private void ShowDashboardPage()
        {
            DataReaderMapper.ShowPlaceholder(contentPanel, "首页仪表盘 - 功能开发中...");
        }

        /// <summary>
        /// 显示设备管理页面
        /// </summary>
        private void ShowDeviceManagePage()
        {
            DataReaderMapper.ShowPlaceholder(contentPanel, "设备管理 - 功能开发中...");
        }

        /// <summary>
        /// 显示维修管理页面
        /// </summary>
        private void ShowRepairManagePage()
        {
            DataReaderMapper.ShowPlaceholder(contentPanel, "维修管理 - 功能开发中...\n\n待处理维修：3 条");
        }

        /// <summary>
        /// 显示借用管理页面
        /// </summary>
        private void ShowBorrowManagePage()
        {
            DataReaderMapper.ShowPlaceholder(contentPanel, "借用管理 - 功能开发中...");
        }

        /// <summary>
        /// 显示监控中心页面（用泛型方法 + 自动缩放）
        /// </summary>
        private void ShowMonitoringCenterPage()
        {
            try
            {
                DataReaderMapper.ShowFormInPanel<MonitoringCenter>(contentPanel, autoScale: true);
            }
            catch
            {
                DataReaderMapper.ShowPlaceholder(contentPanel, "监控中心 - 功能开发中...");
            }
        }

        /// <summary>
        /// 显示数据统计页面
        /// </summary>
        private void ShowDataStatisticsPage()
        {
            DataReaderMapper.ShowPlaceholder(contentPanel, "数据统计 - 功能开发中...");
        }

        /// <summary>
        /// 显示系统设置页面
        /// </summary>
        private void ShowSystemSettingPage()
        {
            DataReaderMapper.ShowPlaceholder(contentPanel, "系统设置 - 功能开发中...");
        }

        /// <summary>
        /// 通知按钮点击
        /// </summary>
        private void btnNotify_Click(object sender, EventArgs e)
        {
            UIMessageBox.Show("暂无新通知", "通知");
        }

        /// <summary>
        /// 退出按钮点击
        /// </summary>
        private void btnExit_Click(object sender, EventArgs e)
        {
            if (UIMessageBox.ShowAsk("确定要退出系统吗？", true))
            {
                Application.Exit();
            }
        }

        private void uiNavMenu1_MenuItemClick(TreeNode node, NavMenuItem item, int pageIndex)
        {
        
        }
    }
}
