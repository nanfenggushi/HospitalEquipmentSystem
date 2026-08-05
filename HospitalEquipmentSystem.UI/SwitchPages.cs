using HospitalEquipment.Model;
using HospitalEquipment.Util;
using HospitalEquipmentSystem.UI.Dashboard;
using Sunny.UI;
using System;
using System.Drawing;
using System.Reflection;
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
        /// 最近一次缩放比例（避免比例没变时反复重建字体）
        /// </summary>
        private float _lastScale = 0f;

        /// <summary>
        /// 菜单字体缓存（避免每次点击菜单都新建字体导致 GDI 句柄累积）
        /// </summary>
        private static readonly Font MenuFontRegular = new Font("微软雅黑", 12F, FontStyle.Regular);
        private static readonly Font MenuFontBold = new Font("微软雅黑", 12F, FontStyle.Bold);

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

            // 开启内容区双缓冲，减少切换页面时的闪烁
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(contentPanel, true, null);

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
            currentSelectedButton = btnDashboard;
            UpdateMenuSelection(btnDashboard);
            ShowWelcomePage();

            // 按当前登录角色控制侧边栏菜单显示
            ApplyRolePermissions();
        }

        /// <summary>
        /// 根据当前登录角色控制侧边栏菜单的可见性：
        /// 管理员全部可见，医护隐藏数据统计/系统设置，维修员只保留维修与监控相关菜单
        /// </summary>
        private void ApplyRolePermissions()
        {
            // 先全部恢复显示，避免角色切换后残留隐藏状态
            Button[] menus = {
                btnDashboard, btnDeviceManage, btnRepairManage, btnBorrowManage,
                btnMonitoringCenter, btnDataStatistics, btnSystemSetting
            };
            foreach (var b in menus)
            {
                b.Visible = true;
            }

            switch (LoginUser.Role)
            {
                case UserRoleText.Doctor:
                    // 医护：可以查看设备、申报故障、申请借用，隐藏统计与系统设置
                    btnDataStatistics.Visible = false;
                    btnSystemSetting.Visible = false;
                    break;

                case UserRoleText.Repair:
                    // 维修员：只保留首页、维修管理、监控中心
                    btnDeviceManage.Visible = false;
                    btnBorrowManage.Visible = false;
                    btnDataStatistics.Visible = false;
                    btnSystemSetting.Visible = false;
                    break;

                    // 管理员（admin）默认全部显示，无需处理
            }

            // 顶部栏显示当前登录用户，例如：张三（设备科·管理员）
            btnUserName.Text = string.IsNullOrEmpty(LoginUser.Role)
                ? "未登录"
                : LoginUser.DisplayName;

            // 如果当前选中的菜单被隐藏，自动跳到第一个可见菜单
            if (currentSelectedButton != null && !currentSelectedButton.Visible)
            {
                foreach (var b in menus)
                {
                    if (b.Visible)
                    {
                        b.PerformClick();
                        break;
                    }
                }
            }
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

            // 缩放比例没变化时直接跳过，避免每次布局触发都重建字体导致卡顿
            if (Math.Abs(scale - _lastScale) < 0.0001f) return;
            _lastScale = scale;

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
            btnUserName.Font = new Font(btnUserName.Font.FontFamily, newTitleFontSize * 1.0f, btnUserName.Font.Style);

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
            } else
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
            button.Font = MenuFontRegular;
        }

        /// <summary>
        /// 设置按钮为选中样式
        /// </summary>
        private void SetButtonSelectedStyle(Button button)
        {
            button.BackColor = Color.FromArgb(210, 158, 64);
            button.Font = MenuFontBold;
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

            try
            {
                DataReaderMapper.ShowFormInPanel<MainTainManagement>(contentPanel, autoScale: true);
            } catch
            {
                DataReaderMapper.ShowPlaceholder(contentPanel, "维修管理 - 功能开发中...\n\n待处理维修：3 条");
            }

        }

        /// <summary>
        /// 显示借用管理页面
        /// </summary>
        private void ShowBorrowManagePage()
        {
            try
            {
                DataReaderMapper.ShowFormInPanel<Equipment_BorrowingUI>(contentPanel, autoScale: true);
            } catch
            {
                DataReaderMapper.ShowPlaceholder(contentPanel, "借用管理 - 功能开发中...");
            }

        }

        /// <summary>
        /// 显示监控中心页面（用泛型方法 + 自动缩放）
        /// </summary>
        private void ShowMonitoringCenterPage()
        {
            try
            {
                DataReaderMapper.ShowFormInPanel<MonitoringCenter>(contentPanel, autoScale: true);
            } catch
            {
                DataReaderMapper.ShowPlaceholder(contentPanel, "监控中心 - 功能开发中...");
            }
        }

        /// <summary>
        /// 显示数据统计页面
        /// </summary>
        private void ShowDataStatisticsPage()
        {
            try
            {
                DataReaderMapper.ShowFormInPanel<StatisticsForm>(contentPanel, autoScale: true);
            } catch
            {
                DataReaderMapper.ShowPlaceholder(contentPanel, "监控中心 - 功能开发中...");
            }
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

        /// <summary>
        /// 用户名按钮点击：在按钮下方弹出下拉菜单
        /// </summary>
        private void btnUserName_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(btnUserName, 0, btnUserName.Height);
        }

        /// <summary>
        /// 切换用户菜单项点击：清除登录信息，回到登录页面
        /// </summary>
        private void toolStripMenuItemSwitchUser_Click(object sender, EventArgs e)
        {
            if (!UIMessageBox.ShowAsk("确定要切换用户吗？", true))
                return;

            // 清除当前登录信息
            LoginUser.Reset();

            // 隐藏当前主窗体
            this.Hide();

            // 打开登录窗体
            using (var login = new LoginForm())
            {
                if (login.ShowDialog() == DialogResult.OK)
                {
                    // 登录成功，刷新页面并重新显示
                    ApplyRolePermissions();
                    ShowWelcomePage();
                    this.Show();
                } else
                {
                    // 登录取消，退出应用
                    Application.Exit();
                }
            }
        }

        private void uiNavMenu1_MenuItemClick(TreeNode node, NavMenuItem item, int pageIndex)
        {

        }
    }
}
