using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HospitalEquipment.BLL;
using HospitalEquipment.Model;
using Microsoft.VisualBasic;

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

        // 从监控中心告警卡片跳转过来时，需要高亮定位的工单ID
        private int? _highlightRecordId;

        // 标记是否已加载过数据（OnLoad 可能多次触发）
        private bool _dataLoaded = false;

        public RepairerWorkbench() : this(null)
        {
        }

        public RepairerWorkbench(MaintenanceRecord alarmRecord)
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
                if (alarmRecord.ProgressStage == "InProgress")
                    tabControl.SelectedTab = tpInProgress;
                else if (alarmRecord.ProgressStage == "Done")
                    tabControl.SelectedTab = tpDone;
                else
                    tabControl.SelectedTab = tpPending;

                // 设计器默认把表格放在第一个 Tab，先手动挪到目标 Tab
                var currentTab = tabControl.SelectedTab;
                currentTab.Controls.Add(pnlPager);
                pnlPager.Dock = DockStyle.Bottom;
                currentTab.Controls.Add(dgvOrders);
                dgvOrders.Dock = DockStyle.Fill;
            }

            WireEvents();
            // LoadData() 改为在 OnLoad 中调用，避免 Handle 未创建时 BeginInvoke 失败
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode && !_dataLoaded)
            {
                _dataLoaded = true;
                LoadData();
            }
        }

        // ==================== 数据加载 ====================

        private void LoadData()
        {
            string stage = GetCurrentStage();
            int userId = LoginUser.UserId;

            Task.Run(() =>
            {
                var data = BLL.GetRepairerOrders(userId, stage) ?? new List<MaintenanceRecordDto>();
                var kpi = BLL.GetRepairerStageCounts(userId);

                this.BeginInvoke(new Action(() =>
                {
                    _allData = data;
                    _currentPage = 1;
                    PrepareHighlightPage();
                    ApplyPaging();
                    HighlightTargetRow();
                    ApplyKpiCards(kpi);
                    UpdateActionButtonText();
                }));
            });
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

        /// <summary>
        /// 根据当前 Tab 更新操作按钮列头文字
        /// 待接单 → colAction="接单" | 处理中 → colProcess="处理" | 已完成 → colAction="查看详情"
        /// </summary>
        private void UpdateActionButtonText()
        {
            if (tabControl.SelectedTab == tpPending)
                colAction.Text = "接单";
            else if (tabControl.SelectedTab == tpInProgress)
                colProcess.Text = "处理";
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

            // 待接单：colAction → 接单
            if (colName == colAction.Name && dto.ProgressStage == "Assigned")
            {
                DirectAcceptOrder(dto);
                return;
            }

            // 待接单：colReject → 拒绝接单
            if (colName == colReject.Name && dto.ProgressStage == "Assigned")
            {
                DirectRejectOrder(dto);
                return;
            }

            // 处理中：colProcess → 打开 AI 检修助手
            if (colName == colProcess.Name && dto.ProgressStage == "InProgress")
            {
                using (var agent = new AgentForm(dto.RecordId))
                {
                    if (agent.ShowDialog() == DialogResult.OK)
                        LoadData();
                }
                return;
            }

            // 已完成：colAction → 查看详情（含物料清单）
            if (colName == colAction.Name && dto.ProgressStage == "Done")
            {
#pragma warning disable CS4014
                ViewDetailAsync(dto);
#pragma warning restore CS4014
                return;
            }
        }

        /// <summary>
        /// 直接接单：Assigned → InProgress（不走 Agent 页面）
        /// </summary>
        private void DirectAcceptOrder(MaintenanceRecordDto dto)
        {
            if (this.ShowAskDialog($"确认接单？\n工单号：{dto.RepairNo}\n设备：{dto.EquipmentName}"))
            {
                if (BLL.AcceptOrder(dto.RecordId, LoginUser.UserId))
                {
                    UIMessageBox.Show($"工单 {dto.RepairNo} 已接单，请开始维修");
                    LoadData();
                }
                else
                    UIMessageBox.ShowError("接单失败，请重试");
            }
        }

        /// <summary>
        /// 拒绝接单：Assigned → Pending（清空指派人并记录拒绝理由）
        /// </summary>
        private void DirectRejectOrder(MaintenanceRecordDto dto)
        {
            string reason = Interaction.InputBox("请输入拒绝理由：", "拒绝接单", "");
            if (string.IsNullOrEmpty(reason))
                return;

            if (BLL.RejectOrder(dto.RecordId, LoginUser.UserId, reason))
            {
                UIMessageBox.Show("已退回管理员");
                LoadData();
            }
            else
                UIMessageBox.ShowError("拒绝失败，请重试");
        }

        /// <summary>
        /// 行添加时动态设置按钮文字和可见性
        /// 待接单(Assigned): colAction="接单"可见, colProcess隐藏
        /// 处理中(InProgress): colAction隐藏, colProcess="处理"可见
        /// 已完成(Done): colAction="查看详情"可见, colProcess隐藏
        /// </summary>
        private void DgvOrders_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = e.RowIndex; i < e.RowIndex + e.RowCount; i++)
            {
                var row = dgvOrders.Rows[i];
                if (row.DataBoundItem is MaintenanceRecordDto dto)
                {
                    var actionCell = row.Cells[colAction.Name] as DataGridViewButtonCell;
                    var rejectCell = row.Cells[colReject.Name] as DataGridViewButtonCell;
                    var processCell = row.Cells[colProcess.Name] as DataGridViewButtonCell;

                    bool isAssigned = dto.ProgressStage == "Assigned";
                    bool isInProgress = dto.ProgressStage == "InProgress";
                    bool isDone = dto.ProgressStage == "Done";

                    // colAction：待接单显示"接单"，已完成显示"查看详情"，处理中隐藏
                    if (actionCell != null)
                    {
                        if (isAssigned)
                        {
                            actionCell.Value = "接单";
                            SetCellVisible(actionCell, true);
                        }
                        else if (isDone)
                        {
                            actionCell.Value = "查看详情";
                            SetCellVisible(actionCell, true);
                        }
                        else
                        {
                            actionCell.Value = "";
                            SetCellHidden(actionCell);
                        }
                    }

                    // colReject：待接单显示"拒绝"，其余隐藏
                    if (rejectCell != null)
                    {
                        if (isAssigned)
                        {
                            rejectCell.Value = "拒绝";
                            SetCellVisible(rejectCell, true);
                        }
                        else
                        {
                            rejectCell.Value = "";
                            SetCellHidden(rejectCell);
                        }
                    }

                    // colProcess：处理中显示"处理"，其余隐藏
                    if (processCell != null)
                    {
                        if (isInProgress)
                        {
                            processCell.Value = "处理";
                            SetCellVisible(processCell, true);
                        }
                        else
                        {
                            processCell.Value = "";
                            SetCellHidden(processCell);
                        }
                    }
                }
            }
        }

        private void SetCellVisible(DataGridViewButtonCell cell, bool visible)
        {
            cell.Style.ForeColor = System.Drawing.Color.White;
            cell.Style.SelectionForeColor = System.Drawing.Color.White;
            cell.FlatStyle = FlatStyle.Flat;
        }

        private void SetCellHidden(DataGridViewButtonCell cell)
        {
            cell.Style.ForeColor = System.Drawing.Color.Transparent;
            cell.Style.SelectionForeColor = System.Drawing.Color.Transparent;
            cell.Style.BackColor = System.Drawing.Color.FromArgb(19, 35, 58);
            cell.Style.SelectionBackColor = System.Drawing.Color.FromArgb(30, 58, 138);
            cell.FlatStyle = FlatStyle.Flat;
        }

        /// <summary>
        /// 查看详情（已完成工单）：异步加载物料清单后弹窗展示完整信息
        /// </summary>
        private async Task ViewDetailAsync(MaintenanceRecordDto dto)
        {
            var matManager = new MaintenanceMaterialManager(new MockFaultRecognizer());
            List<MaintenanceMaterial> materials = null;
            try
            {
                materials = await matManager.GetExistingMaterials(dto.RecordId);
            }
            catch
            {
                materials = new List<MaintenanceMaterial>();
            }

            decimal materialTotal = materials?.Sum(m => m.Subtotal) ?? 0;

            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"工单号：{dto.RepairNo}");
            sb.AppendLine($"设备：{dto.EquipmentName}");
            sb.AppendLine($"故障描述：{dto.FaultDesc}");
            sb.AppendLine($"维修结果：{dto.RepairResult ?? "无"}");

            if (materials != null && materials.Count > 0)
            {
                sb.AppendLine("──────────────");
                sb.AppendLine("【物料清单】");
                foreach (var m in materials)
                {
                    sb.AppendLine($"  {m.MaterialName}  x{m.Quantity}  ¥{m.UnitPrice:F2}  ¥{m.Subtotal:F2}");
                }
                sb.AppendLine("──────────────");
                sb.AppendLine($"物料总金额：¥{materialTotal:F2}");
            }
            else
            {
                sb.AppendLine("物料清单：无物料记录");
            }

            sb.AppendLine($"维修费用：{(dto.RepairCost.HasValue ? $"¥{dto.RepairCost.Value:F2}" : "无")}");
            sb.AppendLine($"停机时长：{(dto.DowntimeHours.HasValue ? $"{dto.DowntimeHours}h" : "无")}");
            sb.AppendLine($"完成时间：{(dto.CompleteTime.HasValue ? dto.CompleteTime.Value.ToString("yyyy-MM-dd HH:mm") : "无")}");

            UIMessageBox.Show(sb.ToString());
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
