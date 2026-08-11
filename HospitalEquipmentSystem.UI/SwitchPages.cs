using HospitalEquipment.Model;
using HospitalEquipmentSystem.Common;
using HospitalEquipmentSystem.UI.Dashboard;
using Sunny.UI;
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace HospitalEquipmentSystem.UI
{
    /// <summary>
    /// 页面切换窗体：方案二（自定义 Panel + UISymbolButton 动态路由与自适应布局）
    /// </summary>
    public partial class SwitchPages : UIForm
    {
        /// <summary>
        /// 侧边栏是否展开（单一状态源）
        /// </summary>
        private bool sideBarExpanded = true;

        /// <summary>
        /// 侧边栏展开时的初始宽度
        /// </summary>
        private const int SideBarWidth = 158;

        /// <summary>
        /// 当前选中的菜单按钮
        /// </summary>
        private UISymbolButton currentSelectedButton;

        /// <summary>
        /// 从监控中心告警卡片带过来的维修记录，用于跳到对应明细列表
        /// </summary>
        private MaintenanceRecord _alarmDetailRecord;

        /// <summary>
        /// 最近一次缩放比例（避免比例没变时反复重建字体）
        /// </summary>
        private float _lastScale = 0f;

        // [修复] 移除原来的 static 静态字体，改为动态缓存字体实例，防止自适应缩放时点击按钮导致字体大小被重置
        /// <summary>
        /// 菜单字体缓存（动态响应缩放，避免每次点击菜单都新建字体导致 GDI 句柄累积）
        /// </summary>
        private Font _currentMenuFontRegular;
        private Font _currentMenuFontBold;

        #region 初始布局尺寸缓存（用于响应式缩放计算）
        private float initialWidth;
        private float initialHeight;
        private int initialSideBarWidth;
        private int initialMenuButtonHeight;
        private float initialMenuFontSize;
        private int initialTopBarHeight;
        private float initialTitleFontSize;
        #endregion

        /// <summary>
        /// 所有侧边栏菜单按钮数组
        /// </summary>
        private UISymbolButton[] menus;

        // [修复] 将菜单标题提取为类级别，与上面的 menus 数组严格一一对应
        private readonly string[] menuTitles = {
            "首页仪表盘", "设备管理", "维修管理", "借用管理", "监控中心", "科室收入", "数据统计", "系统设置"
        };

        /// <summary>
        /// 构造函数：初始化窗体控件及事件绑定
        /// </summary>
        public SwitchPages()
        {
            InitializeComponent();

            // [修复] 初始化动态字体缓存
            _currentMenuFontRegular = new Font("微软雅黑", 12F, FontStyle.Regular);
            _currentMenuFontBold = new Font("微软雅黑", 12F, FontStyle.Bold);

            // [修复] 初始化菜单按钮数组，调整"科室收入(btnRevenue)"到中间位置（不在首位也不在末位）
            menus = new UISymbolButton[]
            {
                btnDashboard, btnDeviceManage, btnRepairManage, btnBorrowManage,
                btnMonitoringCenter, btnRevenue, btnDataStatistics, btnSystemSetting
            };

            // 开启内容区 DoubleBuffered 减少页面切换闪烁
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.SetValue(contentPanel, true, null);

            this.Resize += new EventHandler(SwitchPages_Resize);
        }

        /// <summary>
        /// 窗体加载事件：记录初始比例参数并初始化状态
        /// </summary>
        private void SwitchPages_Load(object sender, EventArgs e)
        {
            // 记录初始大小
            initialWidth = this.ClientSize.Width;
            initialHeight = this.ClientSize.Height;
            initialSideBarWidth = sidePanel.Width; // 使用 Designer 实际宽度 158
            initialMenuButtonHeight = btnDashboard.Height;
            initialMenuFontSize = btnDashboard.Font.Size;
            initialTopBarHeight = topPanel.Height;
            initialTitleFontSize = label1.Font.Size;

            // [修复] 强制修正侧边栏菜单在 UI 上的视觉排列顺序，使其严格与 menus 数组逻辑顺序一致
            // 解决 "科室收入" 因为设计器层级问题跑到最前面的 Bug
            if (menus.Length > 0 && menus[0].Parent != null)
            {
                var parent = menus[0].Parent;
                int minIndex = parent.Controls.Count;
                // 找出所有菜单按钮中当前的最小层级索引（WinForms 中 Top 停靠时，Index 越小越靠上）
                foreach (var m in menus)
                {
                    int idx = parent.Controls.GetChildIndex(m);
                    if (idx < minIndex) minIndex = idx;
                }
                // 按我们定义的正确顺序，依次覆盖控件层级
                for (int i = 0; i < menus.Length; i++)
                {
                    parent.Controls.SetChildIndex(menus[i], minIndex + i);
                }
            }

            // 默认选中首页仪表盘
            currentSelectedButton = btnDashboard;
            UpdateMenuSelection(btnDashboard);

            // 按角色应用权限并加载默认页面
            ApplyRolePermissions();
        }

        /// <summary>
        /// 根据当前登录角色控制侧边栏菜单可见性
        /// </summary>
        private void ApplyRolePermissions()
        {
            // 恢复所有菜单可见性
            foreach (var b in menus)
            {
                b.Visible = true;
            }

            switch (LoginUser.Role)
            {
                case UserRoleText.Doctor:
                    btnDataStatistics.Visible = false;
                    btnRevenue.Visible = false;
                    btnSystemSetting.Visible = false;
                    btnDashboard.Visible = false;
                    break;

                case UserRoleText.Repair:
                    btnDeviceManage.Visible = false;
                    btnBorrowManage.Visible = false;
                    btnDataStatistics.Visible = false;
                    btnRevenue.Visible = false;
                    btnSystemSetting.Visible = false;
                    btnDashboard.Visible = false;
                    break;

                    // 管理员（Admin）全部可见
            }

            // 更新顶部栏用户角色显示
            uiLabel2.Text = string.IsNullOrEmpty(LoginUser.Role)
                ? "未登录"
                : LoginUser.DisplayName;

            // 若当前选中的菜单被隐藏，自动触发第一个可见菜单的点击
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
        /// 切换用户点击事件
        /// </summary>
        private void toolStripMenuItemSwitchUser_Click(object sender, EventArgs e)
        {
            if (!UIMessageBox.ShowAsk("确定要切换用户吗？", true))
                return;

            LoginUser.Reset();
            PageManager.ClearCache(); // 清理页面缓存（使用通用 PageManager）

            this.Hide();

            using (var login = new LoginForm())
            {
                if (login.ShowDialog() == DialogResult.OK)
                {
                    ApplyRolePermissions();
                    this.Show();

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
                } else
                {
                    Application.Exit();
                }
            }
        }

        /// <summary>
        /// 窗体 Resize：自适应动态缩放侧边栏与文字
        /// </summary>
        private void SwitchPages_Resize(object sender, EventArgs e)
        {
            if (initialWidth == 0 || initialHeight == 0) return;

            float scaleX = (float)this.ClientSize.Width / initialWidth;
            float scaleY = (float)this.ClientSize.Height / initialHeight;
            float scale = Math.Min(scaleX, scaleY);

            if (scale < 0.5f) scale = 0.5f;

            if (Math.Abs(scale - _lastScale) < 0.0001f) return;
            _lastScale = scale;

            // 缩放侧边栏及顶部栏
            if (sidePanel.Visible)
            {
                sidePanel.Width = (int)(initialSideBarWidth * scale);
            }

            int newTopBarHeight = (int)(initialTopBarHeight * scale);
            topPanel.Height = newTopBarHeight;

            // 缩放标题字体
            float newTitleFontSize = initialTitleFontSize * scale;
            if (Math.Abs(label1.Font.Size - newTitleFontSize) > 0.01f)
            {
                Font oldTitleFont = label1.Font;
                label1.Font = new Font(oldTitleFont.FontFamily, newTitleFontSize, oldTitleFont.Style);
                oldTitleFont.Dispose();
            }

            // [修复] 缩放菜单按钮，统一管理全局动态字体
            int newButtonHeight = (int)(initialMenuButtonHeight * scale);
            float newFontSize = initialMenuFontSize * scale;

            // 如果字体大小发生实质变化，则重构全局缓存字体
            if (Math.Abs(_currentMenuFontRegular.Size - newFontSize) > 0.01f)
            {
                Font oldReg = _currentMenuFontRegular;
                Font oldBold = _currentMenuFontBold;

                _currentMenuFontRegular = new Font(oldReg.FontFamily, newFontSize, FontStyle.Regular);
                _currentMenuFontBold = new Font(oldBold.FontFamily, newFontSize, FontStyle.Bold);

                oldReg.Dispose();
                oldBold.Dispose();
            }

            // 应用新的高度与字体给所有按钮
            foreach (var b in menus)
            {
                b.Height = newButtonHeight;
                // 根据是否选中赋予不同的字体
                b.Font = (b == currentSelectedButton) ? _currentMenuFontBold : _currentMenuFontRegular;
            }
        }

        /// <summary>
        /// 顶部栏侧边栏折叠/展开按钮响应事件
        /// </summary>
        private void uiSymbolButton3_Click(object sender, EventArgs e)
        {
            ToggleSideBar();
        }

        /// <summary>
        /// 切换侧边栏展开与折叠状态
        /// </summary>
        private void ToggleSideBar()
        {
            if (sideBarExpanded)
            {
                // 隐藏侧边栏 → contentPanel 自动占满
                sidePanel.Visible = false;
                uiSymbolButton3.Symbol = 97; // ▶ 展开
            } else
            {
                // 显示侧边栏 → contentPanel 自动让出空间
                sidePanel.Width = 158;
                sidePanel.Visible = true;
                uiSymbolButton3.Symbol = 77; // ◀ 收起
            }

            sideBarExpanded = !sideBarExpanded;
        }

        /// <summary>
        /// 更新菜单高亮高赞选中状态
        /// </summary>
        private void UpdateMenuSelection(UISymbolButton selectedButton)
        {
            // 先取消所有按钮的高亮状态
            foreach (var b in menus)
            {
                ResetButtonStyle(b);
                b.Refresh();
            }

            // 高亮选中的按钮
            SetButtonSelectedStyle(selectedButton);
            selectedButton.Refresh();

            currentSelectedButton = selectedButton;

            // [修复] 更新顶部标题文字 (不再每次都新建数组，直接匹配同步好的全局数组)
            for (int i = 0; i < menus.Length; i++)
            {
                if (menus[i] == selectedButton)
                {
                    label1.Text = menuTitles[i];
                    break;
                }
            }

            contentPanel.Focus();
        }

        private void ResetButtonStyle(UISymbolButton button)
        {
            button.FillColor = Color.Transparent;
            // [修复] 使用动态缓存的常规字体，而不是写死的 12pt 静态字体，防止缩放后点击变小
            button.Font = _currentMenuFontRegular;
        }

        private void SetButtonSelectedStyle(UISymbolButton button)
        {
            button.FillColor = Color.FromArgb(210, 158, 64);
            // [修复] 使用动态缓存的加粗字体
            button.Font = _currentMenuFontBold;
        }

        /// <summary>
        /// 首页仪表盘菜单点击
        /// </summary>
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            UpdateMenuSelection(btnDashboard);
            ShowDashboardPage();
        }

        /// <summary>
        /// 设备管理菜单点击
        /// </summary>
        private void btnDeviceManage_Click(object sender, EventArgs e)
        {
            UpdateMenuSelection(btnDeviceManage);
            ShowDeviceManagePage();
        }

        /// <summary>
        /// 维修管理菜单点击
        /// </summary>
        private void btnRepairManage_Click(object sender, EventArgs e)
        {
            UpdateMenuSelection(btnRepairManage);
            ShowRepairManagePage();
        }

        /// <summary>
        /// 借用管理菜单点击
        /// </summary>
        private void btnBorrowManage_Click(object sender, EventArgs e)
        {
            UpdateMenuSelection(btnBorrowManage);
            ShowBorrowManagePage();
        }

        /// <summary>
        /// 监控中心菜单点击
        /// </summary>
        private void btnMonitoringCenter_Click(object sender, EventArgs e)
        {
            UpdateMenuSelection(btnMonitoringCenter);
            ShowMonitoringCenterPage();
        }

        /// <summary>
        /// 数据统计菜单点击
        /// </summary>
        private void btnDataStatistics_Click(object sender, EventArgs e)
        {
            UpdateMenuSelection(btnDataStatistics);
            ShowDataStatisticsPage();
        }

        /// <summary>
        /// 系统设置菜单点击
        /// </summary>
        private void btnSystemSetting_Click(object sender, EventArgs e)
        {
            UpdateMenuSelection(btnSystemSetting);
            ShowSystemSettingPage();
        }

        /// <summary>
        /// 科室收入菜单点击
        /// </summary>
        private void btnRevenue_Click(object sender, EventArgs e)
        {
            UpdateMenuSelection(btnRevenue);
            ShowDeptRevenuePage();
        }

        /// <summary>
        /// 从监控卡片穿透跳转至维修明细
        /// </summary>
        public void OpenAlarmDetail(MaintenanceRecord record)
        {
            if (record == null) return;

            _alarmDetailRecord = record;
            UpdateMenuSelection(btnRepairManage);
            ShowRepairManagePage();
        }

        /// <summary>
        /// 显示首页仪表盘页面
        /// </summary>
        private void ShowDashboardPage()
        {
            try
            {
                // 内嵌山海鲸大屏（DashboardForm 内含 WebView2）
                PageManager.ShowPage<DashboardForm>(contentPanel, autoScale: false);
            } catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[首页仪表盘加载失败] {ex}");
                PageManager.ShowPlaceholder(contentPanel, "首页仪表盘 - 功能开发中...");
            }
        }

        /// <summary>
        /// 显示设备管理页面
        /// </summary>
        private void ShowDeviceManagePage()
        {
            try
            {
                PageManager.ShowPage<EquipmentManagement>(contentPanel, autoScale: false);
            } catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[设备管理加载失败] {ex}");
                PageManager.ShowPlaceholder(contentPanel, "设备管理 - 功能开发中...");
            }
        }

        /// <summary>
        /// 显示维修管理页面（基于角色路由）
        /// </summary>
        private void ShowRepairManagePage()
        {
            try
            {
                MaintenanceRecord alarm = _alarmDetailRecord;
                _alarmDetailRecord = null;

                string role = LoginUser.Role;
                if (role == UserRoleText.Admin)
                    PageManager.ShowPage(contentPanel, new MainTainManagement(alarm), autoScale: false);
                else if (role == UserRoleText.Repair)
                    PageManager.ShowPage(contentPanel, new RepairerWorkbench(alarm), autoScale: false);
                else if (role == UserRoleText.Doctor)
                    PageManager.ShowPage(contentPanel, new DoctorWorkbench(alarm), autoScale: false);
                else
                    PageManager.ShowPlaceholder(contentPanel, "未知角色，无法加载维修管理");
            } catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[维修管理加载失败] {ex}");
                PageManager.ShowPlaceholder(contentPanel, "维修管理 - 功能开发中...");
            }
        }

        /// <summary>
        /// 显示借用管理页面
        /// </summary>
        private void ShowBorrowManagePage()
        {
            try
            {
                PageManager.ShowPage<Equipment_BorrowingUI>(contentPanel, autoScale: false);
            } catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[借用管理加载失败] {ex}");
                PageManager.ShowPlaceholder(contentPanel, "借用管理 - 功能开发中...");
            }
        }

        /// <summary>
        /// 显示监控中心页面
        /// </summary>
        private void ShowMonitoringCenterPage()
        {
            try
            {
                PageManager.ShowPage<MonitoringCenter>(contentPanel, autoScale: false);
            } catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[监控中心加载失败] {ex}");
                PageManager.ShowPlaceholder(contentPanel, "监控中心 - 功能开发中...");
            }
        }

        /// <summary>
        /// 显示数据统计页面
        /// </summary>
        private void ShowDataStatisticsPage()
        {
            try
            {
                PageManager.ShowPage<StatisticsForm>(contentPanel, autoScale: false);
            } catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[数据统计加载失败] {ex}");
                PageManager.ShowPlaceholder(contentPanel, "数据统计 - 功能开发中...");
            }
        }

        /// <summary>
        /// 显示系统设置页面
        /// </summary>
        private void ShowSystemSettingPage()
        {
            try
            {
                PageManager.ShowPage<sysmset>(contentPanel, autoScale: false);
            } catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[系统设置加载失败] {ex}");
                PageManager.ShowPlaceholder(contentPanel, "系统设置 - 功能开发中...");
            }
        }

        /// <summary>
        /// 显示科室收入页面
        /// </summary>
        private void ShowDeptRevenuePage()
        {
            try
            {
                PageManager.ShowPage<DeptRevenueForm>(contentPanel, autoScale: false);
            } catch
            {
                PageManager.ShowPlaceholder(contentPanel, "科室收入 - 功能开发中...");
            }
        }

        private void btnNotify_Click(object sender, EventArgs e)
        {
            UIMessageBox.Show("暂无新通知", "通知");
        }

        /// <summary>
        /// 最小化
        /// </summary>
        private void uiSymbolButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// 退出
        /// </summary>
        private void uiSymbolButton1_Click(object sender, EventArgs e)
        {
            if (UIMessageBox.ShowAsk("确定要退出系统吗？", true))
            {
                Application.Exit();
            }
        }
    }
}