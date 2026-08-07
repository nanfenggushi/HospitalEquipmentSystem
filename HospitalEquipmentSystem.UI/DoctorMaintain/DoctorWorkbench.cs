using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HospitalEquipment.BLL;
using HospitalEquipment.Model;

namespace HospitalEquipmentSystem.UI
{
    /// <summary>
    /// 医生工作台：提交报修 + 查看我的工单状态
    /// </summary>
    public partial class DoctorWorkbench : UIForm
    {
        private MaintenanceBLL _bll;
        private List<MaintenanceRecordDto> _allData;
        private int _currentPage = 1;
        private const int PageSize = 6;

        private MaintenanceBLL BLL => _bll ?? (_bll = new MaintenanceBLL());

        public DoctorWorkbench()
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
            int userId = LoginUser.UserId;

            Task.Run(() =>
            {
                var data = BLL.GetDoctorOrders(userId, stage) ?? new List<MaintenanceRecordDto>();
                var kpi = BLL.GetDoctorStageCounts(userId);

                this.BeginInvoke(new Action(() =>
                {
                    _allData = data;
                    _currentPage = 1;
                    ApplyPaging();
                    ApplyKpiCards(kpi);
                }));
            });
        }

        /// <summary>
        /// 根据 Tab 选中的页返回对应的阶段值
        /// </summary>
        private string GetCurrentStage()
        {
            if (tabControl.SelectedTab == tpAll) return null; // 全部
            if (tabControl.SelectedTab == tpPending) return "Pending";
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

        private void ApplyKpiCards(Dictionary<string, int> kpi)
        {
            lblKpi1Val.Text = kpi["Pending"].ToString();      // 待分配
            lblKpi2Val.Text = kpi["InProgress"].ToString();    // 处理中
            lblKpi3Val.Text = kpi["Done"].ToString();          // 已完成
            lblKpi4Val.Text = kpi["Total"].ToString();         // 全部
        }

        // ==================== 事件绑定 ====================

        private void WireEvents()
        {
            btnRefresh.Click += (s, e) => LoadData();
            btnNewRepair.Click += BtnNewRepair_Click;
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
            dgvOrders.RowsAdded += DgvOrders_RowsAdded;
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

        // ==================== 提交报修 ====================

        private void BtnNewRepair_Click(object sender, EventArgs e)
        {
            using (var form = new RepairSubmitForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (BLL.SubmitRepair(form.SelectedEquipmentId, form.SelectedDeptId,
                                          LoginUser.UserId, form.FaultType, form.FaultDesc, form.Urgency))
                    {
                        UIMessageBox.Show("报修提交成功，等待管理员分配维修员");
                        LoadData();
                    }
                    else
                        UIMessageBox.ShowError("提交失败，请重试");
                }
            }
        }

        // ==================== 查看详情 ====================

        private void DgvOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var colName = dgvOrders.Columns[e.ColumnIndex].Name;
            if (colName != colAction.Name) return;

            var row = dgvOrders.Rows[e.RowIndex];
            if (!(row.DataBoundItem is MaintenanceRecordDto dto)) return;

            // 未被维修员接单前（Pending 或 Assigned）的工单，按钮显示"删除"
            bool canDelete = dto.ProgressStage == "Pending" || dto.ProgressStage == "Assigned";
            if (canDelete)
            {
                if (UIMessageBox.ShowAsk($"确认删除工单【{dto.RepairNo}】吗？\n此操作不可恢复。"))
                {
                    if (BLL.DeleteDoctorOrder(dto.RecordId, LoginUser.UserId))
                    {
                        UIMessageBox.Show("删除成功");
                        LoadData();
                    }
                    else
                    {
                        UIMessageBox.ShowError("删除失败，可能工单已被分配，无法删除");
                    }
                }
                return;
            }

            // 其他状态显示详情
            var msg = $"工单号：{dto.RepairNo}\n" +
                      $"设备：{dto.EquipmentName}\n" +
                      $"科室：{dto.DeptName}\n" +
                      $"故障类型：{dto.FaultType}\n" +
                      $"故障描述：{dto.FaultDesc}\n" +
                      $"紧急度：{dto.UrgencyText}\n" +
                      $"当前状态：{dto.ProgressStageText}\n" +
                      $"维修员：{dto.RepairerName}\n" +
                      $"报修时间：{dto.ReportTime:yyyy-MM-dd HH:mm}\n" +
                      (dto.DowntimeHours.HasValue ? $"停机时长：{dto.DowntimeHours}h\n" : "") +
                      (dto.RepairResult != null ? $"维修结果：{dto.RepairResult}\n" : "") +
                      (dto.RepairCost.HasValue ? $"维修费用：{dto.RepairCost:C}\n" : "") +
                      (dto.CompleteTime.HasValue ? $"完成时间：{dto.CompleteTime:yyyy-MM-dd HH:mm}" : "");
            UIMessageBox.Show(msg);
        }

        /// <summary>
        /// DataGridView 行格式化：根据状态动态显示操作列按钮文字
        /// </summary>
        private void DgvOrders_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = e.RowIndex; i < e.RowIndex + e.RowCount; i++)
            {
                var row = dgvOrders.Rows[i];
                if (row.DataBoundItem is MaintenanceRecordDto dto)
                {
                    // 未被维修员接单前（Pending 或 Assigned）显示"删除"，接单后（InProgress/Done）显示"查看详情"
                    var cell = row.Cells[colAction.Name] as DataGridViewButtonCell;
                    if (cell != null)
                    {
                        bool canDelete = dto.ProgressStage == "Pending" || dto.ProgressStage == "Assigned";
                        cell.Value = canDelete ? "删除" : "查看详情";
                    }
                }
            }
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
