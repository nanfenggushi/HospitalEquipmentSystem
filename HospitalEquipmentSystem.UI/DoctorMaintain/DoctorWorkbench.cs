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

        // 从监控中心告警卡片跳转过来时，需要高亮定位的工单ID
        private int? _highlightRecordId;

        public DoctorWorkbench() : this(null)
        {
        }

        public DoctorWorkbench(MaintenanceRecord alarmRecord)
        {
            InitializeComponent();
            if (DesignMode) return;

            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.UserPaint |
                          ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;

            if (alarmRecord != null)
            {
                _highlightRecordId = alarmRecord.RecordId;

                // 按工单当前阶段切到对应 Tab
                if (alarmRecord.ProgressStage == "Pending")
                    tabControl.SelectedTab = tpPending;
                else if (alarmRecord.ProgressStage == "InProgress")
                    tabControl.SelectedTab = tpInProgress;
                else if (alarmRecord.ProgressStage == "Done")
                    tabControl.SelectedTab = tpDone;
                else
                    tabControl.SelectedTab = tpAll;

                // 设计器默认把表格放在第一个 Tab，先手动挪到目标 Tab
                var currentTab = tabControl.SelectedTab;
                currentTab.Controls.Add(pnlPager);
                pnlPager.Dock = DockStyle.Bottom;
                currentTab.Controls.Add(dgvOrders);
                dgvOrders.Dock = DockStyle.Fill;
            }

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
                    PrepareHighlightPage();
                    ApplyPaging();
                    HighlightTargetRow();
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

        /// <summary>
        /// 如果当前需要定位某条工单，先翻到它所在的分页
        /// </summary>
        private void PrepareHighlightPage()
        {
            if (!_highlightRecordId.HasValue || _allData == null) return;

            int index = _allData.FindIndex(dto => dto.RecordId == _highlightRecordId.Value);
            if (index >= 0)
            {
                _currentPage = index / PageSize + 1;
            }
        }

        /// <summary>
        /// 在当前分页数据里选中并滚动到目标工单行
        /// </summary>
        private void HighlightTargetRow()
        {
            if (!_highlightRecordId.HasValue) return;

            for (int i = 0; i < dgvOrders.Rows.Count; i++)
            {
                if (dgvOrders.Rows[i].DataBoundItem is MaintenanceRecordDto dto &&
                    dto.RecordId == _highlightRecordId.Value)
                {
                    dgvOrders.ClearSelection();
                    dgvOrders.Rows[i].Selected = true;
                    dgvOrders.CurrentCell = dgvOrders.Rows[i].Cells[0];
                    dgvOrders.FirstDisplayedScrollingRowIndex = i;
                    return;
                }
            }
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
            dgvOrders.CellFormatting += DgvOrders_CellFormatting;
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
                    int recordId = BLL.SubmitRepair(form.SelectedEquipmentId, form.SelectedDeptId,
                                          LoginUser.UserId, form.FaultType, form.FaultDesc, form.Urgency);
                    if (recordId > 0)
                    {
                        // 保存照片路径
                        if (!string.IsNullOrEmpty(form.PhotoPath))
                            BLL.UpdatePhotoPath(recordId, form.PhotoPath);
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

            // 医生只能查看详情，不能删除
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
        private void DgvOrders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == colAction.Index && e.RowIndex >= 0)
            {
                e.Value = "查看详情";
                e.FormattingApplied = true;
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
