using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HospitalEquipment.BLL;
using HospitalEquipment.Model;
using HospitalEquipment.Util;

namespace HospitalEquipmentSystem.UI
{
    public partial class MaintenanceEngineerWorkbench : UIForm
    {
        private readonly MaintenanceBLL _bll = new MaintenanceBLL();
        private int _currentRepairerId;
        private string _currentStage;

        private UIPanel _pnlHeader;
        private UILabel _lblTitle;
        private UILabel _lblSubTitle;
        private UIPanel _pnlKpiContainer;
        private UIPanel _pnlKpiPending;
        private UILabel _lblKpiPendingVal;
        private UILabel _lblKpiPendingSub;
        private UIPanel _pnlKpiProgress;
        private UILabel _lblKpiProgressVal;
        private UILabel _lblKpiProgressSub;
        private UIPanel _pnlKpiDone;
        private UILabel _lblKpiDoneVal;
        private UILabel _lblKpiDoneSub;
        private UIPanel _pnlTabPending;
        private UIPanel _pnlTabProgress;
        private UIPanel _pnlTabDone;
        private UILabel _lblTabPending;
        private UILabel _lblTabProgress;
        private UILabel _lblTabDone;
        private UIPanel _pnlTableHeader;
        private UILabel _lblTableTitle;
        private UIDataGridView _dgvOrders;
        private DataGridViewTextBoxColumn _colRepairNo;
        private DataGridViewTextBoxColumn _colEquipment;
        private DataGridViewTextBoxColumn _colFaultType;
        private DataGridViewTextBoxColumn _colFaultDesc;
        private DataGridViewTextBoxColumn _colUrgency;
        private DataGridViewTextBoxColumn _colDept;
        private DataGridViewTextBoxColumn _colReportTime;
        private DataGridViewTextBoxColumn _colDowntime;
        private DataGridViewButtonColumn _colAction;
        private UIPanel _pnlBottom;
        private UILabel _lblStatus;

        public MaintenanceEngineerWorkbench(int repairerId, string repairerName)
        {
            _currentRepairerId = repairerId;

            InitializeComponent();

            if (DesignMode) return;

            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.UserPaint |
                          ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;

            _lblSubTitle.Text = $"维修工程师：{repairerName}";

            WireEvents();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // ==================== 主窗体 ====================
            this.AutoScaleDimensions = new SizeF(96F, 96F);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.BackColor = Color.FromArgb(11, 22, 34);
            this.ClientSize = new Size(1200, 800);
            this.Name = "MaintenanceEngineerWorkbench";
            this.Text = "维修工程师工作台";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(1000, 700);

            // ==================== 顶部标题栏 ====================
            _pnlHeader = new UIPanel
            {
                Location = new Point(0, 0),
                Size = new Size(1200, 70),
                FillColor = Color.FromArgb(19, 35, 58),
                RectColor = Color.FromArgb(30, 58, 138),
                Radius = 0,
                Dock = DockStyle.Top
            };

            _lblTitle = new UILabel
            {
                Text = "维修工程师工作台",
                Font = new Font("Microsoft YaHei", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(230, 238, 247),
                Location = new Point(20, 15),
                AutoSize = true
            };

            _lblSubTitle = new UILabel
            {
                Text = "维修工程师",
                Font = new Font("Microsoft YaHei", 10F),
                ForeColor = Color.FromArgb(107, 130, 156),
                Location = new Point(22, 45),
                AutoSize = true
            };

            _pnlHeader.Controls.AddRange(new Control[] { _lblTitle, _lblSubTitle });

            // ==================== KPI 卡片容器 ====================
            _pnlKpiContainer = new UIPanel
            {
                Location = new Point(12, 82),
                Size = new Size(1176, 90),
                FillColor = Color.FromArgb(11, 22, 34),
                RectColor = Color.Transparent,
                Radius = 0
            };

            // KPI - 待接单
            _pnlKpiPending = CreateKpiPanel(10, 10, 380, 70, Color.FromArgb(30, 58, 138));
            _lblKpiPendingVal = CreateKpiValueLabel(Color.FromArgb(46, 139, 255));
            _lblKpiPendingVal.Location = new Point(14, 12);
            _lblKpiPendingSub = CreateKpiSubLabel();
            _lblKpiPendingSub.Text = "待接单 · 管理员已指派";
            _lblKpiPendingSub.Location = new Point(14, 45);
            _pnlKpiPending.Controls.AddRange(new Control[] { _lblKpiPendingVal, _lblKpiPendingSub });

            // KPI - 处理中
            _pnlKpiProgress = CreateKpiPanel(405, 10, 380, 70, Color.FromArgb(245, 176, 66));
            _lblKpiProgressVal = CreateKpiValueLabel(Color.FromArgb(245, 176, 66));
            _lblKpiProgressVal.Location = new Point(14, 12);
            _lblKpiProgressSub = CreateKpiSubLabel();
            _lblKpiProgressSub.Text = "处理中 · 正在维修";
            _lblKpiProgressSub.Location = new Point(14, 45);
            _pnlKpiProgress.Controls.AddRange(new Control[] { _lblKpiProgressVal, _lblKpiProgressSub });

            // KPI - 已完成
            _pnlKpiDone = CreateKpiPanel(800, 10, 366, 70, Color.FromArgb(46, 160, 67));
            _lblKpiDoneVal = CreateKpiValueLabel(Color.FromArgb(46, 160, 67));
            _lblKpiDoneVal.Location = new Point(14, 12);
            _lblKpiDoneSub = CreateKpiSubLabel();
            _lblKpiDoneSub.Text = "已完成 · 本周完成";
            _lblKpiDoneSub.Location = new Point(14, 45);
            _pnlKpiDone.Controls.AddRange(new Control[] { _lblKpiDoneVal, _lblKpiDoneSub });

            _pnlKpiContainer.Controls.AddRange(new Control[] { _pnlKpiPending, _pnlKpiProgress, _pnlKpiDone });

            // ==================== Tab 切换 ====================
            _pnlTabPending = CreateTabPanel(12, 182, 150, 44, Color.FromArgb(46, 139, 255));
            _lblTabPending = CreateTabLabel("📋 待接单", Color.FromArgb(230, 238, 247));
            _pnlTabPending.Controls.Add(_lblTabPending);

            _pnlTabProgress = CreateTabPanel(168, 182, 150, 44, Color.FromArgb(19, 35, 58));
            _lblTabProgress = CreateTabLabel("🔧 处理中", Color.FromArgb(159, 179, 200));
            _pnlTabProgress.Controls.Add(_lblTabProgress);

            _pnlTabDone = CreateTabPanel(324, 182, 150, 44, Color.FromArgb(19, 35, 58));
            _lblTabDone = CreateTabLabel("✅ 已完成", Color.FromArgb(159, 179, 200));
            _pnlTabDone.Controls.Add(_lblTabDone);

            // ==================== 表格标题栏 ====================
            _pnlTableHeader = new UIPanel
            {
                Location = new Point(12, 232),
                Size = new Size(1176, 44),
                FillColor = Color.FromArgb(19, 35, 58),
                RectColor = Color.FromArgb(30, 58, 138),
                Radius = 8
            };

            _lblTableTitle = new UILabel
            {
                Text = "待接单工单列表",
                Font = new Font("Microsoft YaHei", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(230, 238, 247),
                Location = new Point(16, 10),
                AutoSize = true
            };

            _pnlTableHeader.Controls.Add(_lblTableTitle);

            // ==================== DataGridView ====================
            _dgvOrders = new UIDataGridView
            {
                Location = new Point(12, 282),
                Size = new Size(1176, 440),
                BackgroundColor = Color.FromArgb(19, 35, 58),
                ForeColor = Color.FromArgb(230, 238, 247),
                GridColor = Color.FromArgb(30, 58, 138),
                ColumnHeadersBackColor = Color.FromArgb(19, 35, 58),
                ColumnHeadersForeColor = Color.FromArgb(230, 238, 247),
                RowsBackColor = Color.FromArgb(19, 35, 58),
                RowsForeColor = Color.FromArgb(230, 238, 247),
                SelectedBackColor = Color.FromArgb(30, 58, 138),
                SelectedForeColor = Color.FromArgb(230, 238, 247),
                RowCount = 0,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoGenerateColumns = false,
                BorderStyle = BorderStyle.None,
                EnableHeadersVisualStyles = false,
                Font = new Font("Microsoft YaHei", 9F)
            };

            // 列定义
            _colRepairNo = new DataGridViewTextBoxColumn
            {
                Name = "colRepairNo",
                HeaderText = "工单号",
                DataPropertyName = "RepairNo",
                Width = 110,
                ReadOnly = true
            };

            _colEquipment = new DataGridViewTextBoxColumn
            {
                Name = "colEquipment",
                HeaderText = "设备名称",
                DataPropertyName = "EquipmentName",
                Width = 150,
                ReadOnly = true
            };

            _colFaultType = new DataGridViewTextBoxColumn
            {
                Name = "colFaultType",
                HeaderText = "故障类型",
                DataPropertyName = "FaultType",
                Width = 90,
                ReadOnly = true
            };

            _colFaultDesc = new DataGridViewTextBoxColumn
            {
                Name = "colFaultDesc",
                HeaderText = "故障描述",
                DataPropertyName = "FaultDesc",
                Width = 250,
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };

            _colUrgency = new DataGridViewTextBoxColumn
            {
                Name = "colUrgency",
                HeaderText = "紧急度",
                DataPropertyName = "UrgencyText",
                Width = 70,
                ReadOnly = true
            };

            _colDept = new DataGridViewTextBoxColumn
            {
                Name = "colDept",
                HeaderText = "科室",
                DataPropertyName = "DeptName",
                Width = 100,
                ReadOnly = true
            };

            _colReportTime = new DataGridViewTextBoxColumn
            {
                Name = "colReportTime",
                HeaderText = "报修时间",
                DataPropertyName = "ReportTime",
                Width = 130,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd HH:mm" }
            };

            _colDowntime = new DataGridViewTextBoxColumn
            {
                Name = "colDowntime",
                HeaderText = "停机(h)",
                DataPropertyName = "DowntimeHours",
                Width = 80,
                ReadOnly = true
            };

            _colAction = new DataGridViewButtonColumn
            {
                Name = "colAction",
                HeaderText = "操作",
                Width = 100,
                ReadOnly = true,
                Text = "查看/操作",
                UseColumnTextForButtonValue = true
            };

            _dgvOrders.Columns.AddRange(new DataGridViewColumn[]
            {
                _colRepairNo, _colEquipment, _colFaultType, _colFaultDesc,
                _colUrgency, _colDept, _colReportTime, _colDowntime, _colAction
            });

            _dgvOrders.CellContentClick += DgvOrders_CellContentClick;
            _dgvOrders.CellFormatting += DgvOrders_CellFormatting;

            // ==================== 底部状态栏 ====================
            _pnlBottom = new UIPanel
            {
                Location = new Point(0, 734),
                Size = new Size(1200, 30),
                FillColor = Color.FromArgb(19, 35, 58),
                RectColor = Color.FromArgb(30, 58, 138),
                Radius = 0,
                Dock = DockStyle.Bottom
            };

            _lblStatus = new UILabel
            {
                Text = "就绪",
                Font = new Font("Microsoft YaHei", 9F),
                ForeColor = Color.FromArgb(107, 130, 156),
                Location = new Point(12, 6),
                AutoSize = true
            };

            _pnlBottom.Controls.Add(_lblStatus);

            // ==================== 添加控件到窗体 ====================
            this.Controls.AddRange(new Control[]
            {
                _pnlBottom,
                _dgvOrders,
                _pnlTableHeader,
                _pnlTabDone,
                _pnlTabProgress,
                _pnlTabPending,
                _pnlKpiContainer,
                _pnlHeader
            });

            this.ResumeLayout(false);
        }

        // ==================== 辅助创建方法 ====================

        private UIPanel CreateKpiPanel(int x, int y, int w, int h, Color rectColor)
        {
            return new UIPanel
            {
                Location = new Point(x, y),
                Size = new Size(w, h),
                FillColor = Color.FromArgb(19, 35, 58),
                RectColor = rectColor,
                Radius = 10
            };
        }

        private UILabel CreateKpiValueLabel(Color color)
        {
            return new UILabel
            {
                Font = new Font("Microsoft YaHei", 28F, FontStyle.Bold),
                ForeColor = color,
                AutoSize = true
            };
        }

        private UILabel CreateKpiSubLabel()
        {
            return new UILabel
            {
                Font = new Font("Microsoft YaHei", 9F),
                ForeColor = Color.FromArgb(107, 130, 156),
                AutoSize = true
            };
        }

        private UIPanel CreateTabPanel(int x, int y, int w, int h, Color rectColor)
        {
            return new UIPanel
            {
                Location = new Point(x, y),
                Size = new Size(w, h),
                FillColor = rectColor,
                RectColor = rectColor,
                Radius = 8,
                Cursor = Cursors.Hand
            };
        }

        private UILabel CreateTabLabel(string text, Color color)
        {
            return new UILabel
            {
                Text = text,
                Font = new Font("Microsoft YaHei", 11F, FontStyle.Bold),
                ForeColor = color,
                Location = new Point(16, 12),
                AutoSize = true,
                Cursor = Cursors.Hand
            };
        }

        // ==================== 事件绑定 ====================

        private void WireEvents()
        {
            _pnlTabPending.Click += (s, e) => SwitchStage("Assigned");
            _pnlTabProgress.Click += (s, e) => SwitchStage("InProgress");
            _pnlTabDone.Click += (s, e) => SwitchStage("Done");
            _lblTabPending.Click += (s, e) => SwitchStage("Assigned");
            _lblTabProgress.Click += (s, e) => SwitchStage("InProgress");
            _lblTabDone.Click += (s, e) => SwitchStage("Done");
        }

        // ==================== 数据加载 ====================

        private void LoadData()
        {
            _currentStage = "Assigned";
            UpdateTabStyles();
            LoadStageData();
            RefreshKpi();
        }

        private void RefreshKpi()
        {
            var counts = _bll.GetRepairerStageCounts(_currentRepairerId);
            _lblKpiPendingVal.Text = counts["PendingAccept"].ToString();
            _lblKpiProgressVal.Text = counts["InProgress"].ToString();
            _lblKpiDoneVal.Text = counts["Completed"].ToString();
        }

        private void SwitchStage(string stage)
        {
            _currentStage = stage;
            UpdateTabStyles();
            LoadStageData();
        }

        private void UpdateTabStyles()
        {
            // 重置所有 tab 样式
            ResetTab(_pnlTabPending, _lblTabPending);
            ResetTab(_pnlTabProgress, _lblTabProgress);
            ResetTab(_pnlTabDone, _lblTabDone);

            // 高亮当前 tab
            switch (_currentStage)
            {
                case "Assigned":
                    HighlightTab(_pnlTabPending, _lblTabPending, Color.FromArgb(46, 139, 255));
                    _lblTableTitle.Text = "📋 待接单工单列表";
                    break;
                case "InProgress":
                    HighlightTab(_pnlTabProgress, _lblTabProgress, Color.FromArgb(245, 176, 66));
                    _lblTableTitle.Text = "🔧 处理中工单列表";
                    break;
                case "Done":
                    HighlightTab(_pnlTabDone, _lblTabDone, Color.FromArgb(46, 160, 67));
                    _lblTableTitle.Text = "✅ 已完成工单列表";
                    break;
            }
        }

        private void ResetTab(UIPanel panel, UILabel label)
        {
            panel.FillColor = Color.FromArgb(19, 35, 58);
            panel.RectColor = Color.FromArgb(19, 35, 58);
            label.ForeColor = Color.FromArgb(159, 179, 200);
        }

        private void HighlightTab(UIPanel panel, UILabel label, Color color)
        {
            panel.FillColor = color;
