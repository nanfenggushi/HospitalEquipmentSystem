using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HospitalEquipment.BLL;
using HospitalEquipment.Model;

namespace HospitalEquipmentSystem.UI
{
    /// <summary>
    /// 维修员工作台：待接单 / 处理中 / 已完成 三个 Tab
    /// </summary>
    public partial class RepairerWorkbench : UIForm
    {
        private MaintenanceBLL _bll;
        private List<MaintenanceRecordDto> _allData;
        private int _currentPage = 1;
        private const int PageSize = 6;

        private MaintenanceBLL BLL => _bll ?? (_bll = new MaintenanceBLL());

        public RepairerWorkbench()
        {
            InitializeComponent();

            if (DesignMode) return;

            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.UserPaint |
                          ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;

            WireEvents();
            LoadData();
        }

        // ==================== 数据加载 ====================

        private void LoadData()
        {
            string stage = GetCurrentStage();
            _allData = BLL.GetRepairerOrders(LoginRepairer.UserId, stage) ?? new List<MaintenanceRecordDto>();
            _currentPage = 1;
            ApplyPaging();

            RefreshKpiCards();
            UpdateActionButtonText();
        }

        /// <summary>
        /// 根据 Tab 选中的页返回对应的阶段值
        /// </summary>
        private string GetCurrentStage()
        {
            if (tabControl.SelectedTab == tpPending) return "Assigned";
            if (tabControl.SelectedTab == tpInProgress) return "InProgress";
            if (tabControl.SelectedTab == tpDone) return "Done";
            return null;
        }

        // ==================== 分页 ====================

        private void ApplyPaging()
        {
            int totalCount = _allData?.Count ?? 0;
            int totalPages = (int)Math.Ceiling((double)totalCount / PageSize);
            if (totalPages < 1) totalPages = 1;
            if (_currentPage > totalPages) _currentPage = totalPages;
            if (_currentPage < 1) _currentPage = 1;

            var pageData = _allData?
                .Skip((_currentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList() ?? new List<MaintenanceRecordDto>();

            dgvOrders.DataSource = pageData;

            lblPageInfo.Text = $"第 {_currentPage}/{totalPages} 页 · 每页 {PageSize} 条 · 共 {totalCount} 条";
            btnPrevPage.Enabled = _currentPage > 1;
            btnNextPage.Enabled = _currentPage < totalPages;
        }

        // ==================== KPI 卡片 ====================

        private void RefreshKpiCards()
        {
            var kpi = BLL.GetRepairerStageCounts(LoginRepairer.UserId);
            lblKpi1Val.Text = kpi["PendingAccept"].ToString();  // 待接单
            lblKpi2Val.Text = kpi["InProgress"].ToString();       // 处理中
            lblKpi3Val.Text = kpi["Completed"].ToString();       // 已完成
        }

        // ==================== 事件绑定 ====================

        private void WireEvents()
        {
            btnRefresh.Click += (s, e) => LoadData();
            tabControl.SelectedIndexChanged += (s, e) =>
            {
                // 切换 Tab 时把 dgvOrders 和 pnlPager 移到当前页
                var currentTab = tabControl.SelectedTab;
                if (dgvOrders.Parent != currentTab)
                {
                    currentTab.Controls.Add(pnlPager);
                    pnlPager.Dock = DockStyle.Bottom;
                    currentTab.Controls.Add(dgvOrders);
                    dgvOrders.Dock = DockStyle.Fill;
                }
                LoadData();
            };
            dgvOrders.CellContentClick += DgvOrders_CellContentClick;
            btnPrevPage.Click += (s, e) =>
            {
                if (_currentPage > 1)
                {
                    _currentPage--;
                    ApplyPaging();
                }
            };
            btnNextPage.Click += (s, e) =>
            {
                int totalPages = (int)Math.Ceiling((double)(_allData?.Count ?? 0) / PageSize);
                if (_currentPage < totalPages)
                {
                    _currentPage++;
                    ApplyPaging();
                }
            };
        }

        /// <summary>
        /// 根据当前 Tab 更新操作按钮文字
        /// </summary>
        private void UpdateActionButtonText()
        {
            if (tabControl.SelectedTab == tpPending)
                colAction.Text = "接单";
            else if (tabControl.SelectedTab == tpInProgress)
                colAction.Text = "提交结果";
            else if (tabControl.SelectedTab == tpDone)
                colAction.Text = "查看详情";
        }

        // ==================== 操作按钮 ====================

        private void DgvOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var colName = dgvOrders.Columns[e.ColumnIndex].Name;
            var row = dgvOrders.Rows[e.RowIndex];
            if (!(row.DataBoundItem is MaintenanceRecordDto dto)) return;

            if (colName == colAction.Name)
            {
                switch (dto.ProgressStage)
                {
                    case "Assigned":
                        AcceptOrder(dto);
                        break;
                    case "InProgress":
                        SubmitResult(dto);
                        break;
                    case "Done":
                        ViewDetail(dto);
                        break;
                }
            }
        }

        /// <summary>
        /// 接单：Assigned → InProgress
        /// </summary>
        private void AcceptOrder(MaintenanceRecordDto dto)
        {
            if (this.ShowAskDialog($"确认接单？\n工单号：{dto.RepairNo}\n设备：{dto.EquipmentName}"))
            {
                if (BLL.AcceptOrder(dto.RecordId, LoginRepairer.UserId))
                {
                    UIMessageBox.Show($"工单 {dto.RepairNo} 已接单，请开始维修");
                    LoadData();
                }
                else
                    UIMessageBox.ShowError("接单失败，请重试");
            }
        }

        /// <summary>
        /// 提交维修结果：InProgress → Done
        /// </summary>
        private void SubmitResult(MaintenanceRecordDto dto)
        {
            using (var form = new RepairResultForm(dto.RepairNo, dto.EquipmentName))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (BLL.SubmitRepairResult(dto.RecordId, form.RepairResult,
                                                form.RepairCost, form.DowntimeHours))
                    {
                        UIMessageBox.Show($"工单 {dto.RepairNo} 维修完成");
                        LoadData();
                    }
                    else
                        UIMessageBox.ShowError("提交失败，请重试");
                }
            }
        }

        /// <summary>
        /// 查看详情（已完成工单）
        /// </summary>
        private void ViewDetail(MaintenanceRecordDto dto)
        {
            var msg = $"工单号：{dto.RepairNo}\n" +
                      $"设备：{dto.EquipmentName}\n" +
                      $"科室：{dto.DeptName}\n" +
                      $"故障描述：{dto.FaultDesc}\n" +
                      $"维修结果：{dto.RepairResult ?? "无"}\n" +
                      (dto.RepairCost.HasValue ? $"维修费用：{dto.RepairCost:C}\n" : "") +
                      (dto.DowntimeHours.HasValue ? $"停机时长：{dto.DowntimeHours}h\n" : "") +
                      (dto.CompleteTime.HasValue ? $"完成时间：{dto.CompleteTime:yyyy-MM-dd HH:mm}" : "");
            UIMessageBox.Show(msg);
        }

        // ==================== 双缓冲防闪烁 ====================

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
    }
}
