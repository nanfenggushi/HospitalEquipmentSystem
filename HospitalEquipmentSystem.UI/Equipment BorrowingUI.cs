using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HospitalEquipment.BLL;
using HospitalEquipment.Model;
using HospitalEquipment.Model.management;
using Sunny.UI;

namespace HospitalEquipmentSystem.UI
{
    public partial class Equipment_BorrowingUI : UIForm
    {
        private const int PageSize = 10;

        private static readonly Color[] ChartColors =
        {
            Color.FromArgb(64, 128, 204),
            Color.FromArgb(46, 165, 68),
            Color.FromArgb(230, 80, 80),
            Color.FromArgb(232, 147, 12),
            Color.FromArgb(154, 100, 220),
            Color.FromArgb(0, 175, 155),
            Color.FromArgb(245, 124, 0),
            Color.FromArgb(92, 107, 192),
            Color.FromArgb(196, 69, 155),
            Color.FromArgb(112, 128, 144)
        };

        private List<BorrowRecord> _currentRecords = new List<BorrowRecord>();
        private List<BorrowRecord> _calendarRecords = new List<BorrowRecord>();
        private int _statusColumnIndex = -1;
        private int _overdueColumnIndex = -1;
        private int _actionColumnIndex = -1;
        private int _loadGeneration;
        private bool _loadingCalendar;
        private DateTime _calendarDisplayDate = DateTime.MinValue;
        private DateTime _calendarLastUserInput = DateTime.MinValue;
        private bool _chartsLoaded;

        public Equipment_BorrowingUI()
        {
            InitializeComponent();
            // 使用 exe 自带的空白图标替换 SunnyUI 默认图标（不涉及图表逻辑）
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            InitControls();
            this.Load += Equipment_BorrowingUI_Load;
        }

        // ==================== 初始化 ====================

        private void InitControls()
        {
            lblUser.Text = "当前用户：" + LoginUser.DisplayName;

            cmbStatus.Items.Add("全部状态");
            cmbStatus.Items.Add("待审批");
            cmbStatus.Items.Add("借用中");
            cmbStatus.Items.Add("已超期");
            cmbStatus.Items.Add("已归还");
            cmbStatus.Items.Add("已驳回");
            cmbStatus.SelectedIndex = 0;

            cmbEquipment.DataSource = new List<Equipment> { new Equipment { EquipmentId = 0, EquipmentName = "全部设备" } };
            cmbEquipment.DisplayMember = "EquipmentName";
            cmbEquipment.ValueMember = "EquipmentId";
            cmbEquipment.SelectedIndex = 0;

            dpFrom.Clear();
            dpTo.Clear();

            InitGrid();
            uiPagination1.PageSize = PageSize;

            btnApply.Click += btnApply_Click;
            btnSearch.Click += btnSearch_Click;
            btnReset.Click += btnReset_Click;
            btnRefresh.Click += btnRefresh_Click;
            txtKeyword.KeyDown += txtKeyword_KeyDown;
            uiPagination1.PageChanged += uiPagination1_PageChanged;
            dgvList.CellClick += dgvList_CellClick;
            btnListView.Click += btnView_Click;
            btnCalendarView.Click += btnView_Click;
            btnChartView.Click += btnView_Click;
            monthCalendar1.DateChanged += monthCalendar1_DateChanged;
            monthCalendar1.MouseDown += monthCalendar1_MouseDown;
            monthCalendar1.KeyDown += monthCalendar1_KeyDown;
            lstCalendar.DoubleClick += lstCalendar_DoubleClick;
            chartBar.SizeChanged += chartBar_SizeChanged;

            btnListView.Selected = true;

            FixCalendarLayout();
        }

        /// <summary>
        /// 修复日历视图 Dock 布局：Fill 控件必须排在边缘 Dock 控件之后处理，
        /// 否则 Fill 会覆盖日历/提示区域，导致应还列表被日历挡住。
        /// </summary>
        private void FixCalendarLayout()
        {
            pnlCalendar.Controls.SetChildIndex(pnlCalendarRight, 0);
            pnlCalendar.Controls.SetChildIndex(monthCalendar1, 1);
            pnlCalendarRight.Controls.SetChildIndex(lstCalendar, 0);
            pnlCalendarRight.Controls.SetChildIndex(lblCalendarHint, 1);
        }

        private void InitGrid()
        {
            dgvList.ClearAll();
            dgvList.AddColumn("借用单号", "BorrowNo", 100, DataGridViewContentAlignment.MiddleLeft, true);
            dgvList.AddColumn("设备", "Equipment", 180, DataGridViewContentAlignment.MiddleLeft, true);
            dgvList.AddColumn("申请人", "Applicant", 80, DataGridViewContentAlignment.MiddleLeft, true);
            dgvList.AddColumn("科室", "Dept", 100, DataGridViewContentAlignment.MiddleLeft, true);
            dgvList.AddColumn("用途", "Purpose", 220, DataGridViewContentAlignment.MiddleLeft, true);
            dgvList.AddDateTimeColumn("预计归还", "ReturnDate", "yyyy-MM-dd", 90, DataGridViewContentAlignment.MiddleCenter, true);
            dgvList.AddColumn("状态", "Status", 70, DataGridViewContentAlignment.MiddleCenter, true);
            dgvList.AddColumn("超期", "Overdue", 60, DataGridViewContentAlignment.MiddleCenter, true);
            dgvList.AddButtonColumn("操作", "Action", 80, true);

            _statusColumnIndex = dgvList.Columns.Count - 3;
            _overdueColumnIndex = dgvList.Columns.Count - 2;
            _actionColumnIndex = dgvList.Columns.Count - 1;

            dgvList.SetColumnHeadersHeight(36);
            dgvList.SetRowHeight(36);
        }

        // ==================== 加载 ====================

        private void Equipment_BorrowingUI_Load(object sender, EventArgs e)
        {
            // 先让窗口显示出来，所有数据库操作放到后台线程异步执行
            this.BeginInvoke(new Action(RunInitialLoad));
        }

        private void RunInitialLoad()
        {
            Task.Run(delegate
            {
                try { BorrowBLL.MarkOverdue(); }
                catch (Exception) { }
                SafeBeginInvoke(() =>
                {
                    RefreshStats();
                    LoadData();
                    LoadEquipment();
                });
            });
        }

        /// <summary>
        /// 窗体已销毁时安全跳过 UI 更新，避免后台线程 BeginInvoke 抛异常
        /// </summary>
        private void SafeBeginInvoke(Action action)
        {
            if (IsDisposed) return;
            try
            {
                BeginInvoke(action);
            }
            catch (ObjectDisposedException)
            {
                // 窗体已销毁，忽略本次更新
            }
            catch (InvalidOperationException)
            {
                // 窗体已关闭，忽略本次更新
            }
        }

        private void LoadData()
        {
            string keyword = txtKeyword.Text.Trim();
            string status = SelectedStatus();
            int equipmentId = SelectedEquipmentId();
            DateTime? from = GetDateOrNull(dpFrom);
            DateTime? to = GetDateOrNull(dpTo);
            if (from.HasValue && to.HasValue && from.Value.Date > to.Value.Date)
            {
                DateTime? t = from;
                from = to;
                to = t;
            }

            int page = uiPagination1.ActivePage < 1 ? 1 : uiPagination1.ActivePage;
            int generation = ++_loadGeneration;

            Task.Run(delegate
            {
                List<BorrowRecord> records;
                int total;
                try
                {
                    records = BorrowBLL.Search(keyword, status, equipmentId, from, to, page, PageSize, out total);
                }
                catch (Exception ex)
                {
                    SafeBeginInvoke(() => UIMessageBox.ShowError("数据加载失败：" + ex.Message));
                    return;
                }
                SafeBeginInvoke(() =>
                {
                    if (generation != _loadGeneration) return;
                    ApplyData(records, total);
                });
            });
        }

        private void ApplyData(List<BorrowRecord> records, int total)
        {
            _currentRecords = records ?? new List<BorrowRecord>();

            dgvList.ClearRows();
            for (int i = 0; i < _currentRecords.Count; i++)
            {
                BorrowRecord r = _currentRecords[i];
                dgvList.AddRow(
                    r.BorrowNo,
                    (r.EquipmentName ?? "") + "  " + (r.EquipmentNo ?? ""),
                    r.ApplicantName ?? "",
                    r.ApplicantDeptName ?? "",
                    r.Purpose ?? "",
                    r.ExpectedReturnDate.ToString("yyyy-MM-dd"),
                    r.StatusText,
                    r.OverdueDays > 0 ? "+" + r.OverdueDays + "天" : "",
                    ActionText(r));
                StyleRow(i, r);
            }

            // 仅在总条数变化时才重新绑定分页。
            // UIPagination.DataBind() 会触发 PageChanged，若每次都绑定会形成
            // ApplyData -> DataBind -> PageChanged -> LoadData -> ApplyData 的死循环
            // （后台每 200ms 持续重查数据库，并使窗体不断重绘）。
            if (uiPagination1.TotalCount != total)
            {
                uiPagination1.TotalCount = total;
                uiPagination1.DataBind();
            }
            lblTotalLabel.Text = "共 " + total + " 条记录";

            RefreshStats();
        }

        private void StyleRow(int rowIndex, BorrowRecord r)
        {
            dgvList.SetCellStyle(rowIndex, _statusColumnIndex, StatusBackColor(r.Status), Color.White);
            if (r.OverdueDays > 0 && rowIndex < dgvList.Rows.Count)
            {
                dgvList.Rows[rowIndex].Cells[_overdueColumnIndex].Style.ForeColor = Color.Red;
            }
        }

        private void RefreshStats()
        {
            Task.Run(delegate
            {
                int pending, borrowing, overdue, dueToday;
                try
                {
                    pending = BorrowBLL.PendingCount();
                    borrowing = BorrowBLL.BorrowingCount();
                    overdue = BorrowBLL.OverdueCount();
                    dueToday = BorrowBLL.DueTodayCount();
                }
                catch (Exception ex)
                {
                    SafeBeginInvoke(() => UIMessageBox.ShowError("统计加载失败：" + ex.Message));
                    return;
                }
                SafeBeginInvoke(() =>
                {
                    lblPendingValue.Text = pending.ToString();
                    lblBorrowingValue.Text = borrowing.ToString();
                    lblOverdueValue.Text = overdue.ToString();
                    lblDueTodayValue.Text = dueToday.ToString();
                });
            });
        }

        private void LoadEquipment()
        {
            Task.Run(delegate
            {
                List<Equipment> list;
                try
                {
                    list = BorrowBLL.GetAllEquipment();
                }
                catch (Exception ex)
                {
                    SafeBeginInvoke(() => UIMessageBox.ShowError("设备列表加载失败：" + ex.Message));
                    return;
                }
                SafeBeginInvoke(() =>
                {
                    var allEquip = new List<Equipment>();
                    allEquip.Add(new Equipment { EquipmentId = 0, EquipmentName = "全部设备" });
                    allEquip.AddRange(list ?? new List<Equipment>());
                    cmbEquipment.DataSource = allEquip;
                    cmbEquipment.DisplayMember = "EquipmentName";
                    cmbEquipment.ValueMember = "EquipmentId";
                    cmbEquipment.SelectedIndex = 0;
                });
            });
        }

        // ==================== 查询条件 ====================

        private string SelectedStatus()
        {
            string[] statuses =
            {
                "", BorrowConst.Pending, BorrowConst.Approved,
                BorrowConst.Overdue, BorrowConst.Returned, BorrowConst.Rejected
            };
            int idx = cmbStatus.SelectedIndex;
            return idx >= 0 && idx < statuses.Length ? statuses[idx] : "";
        }

        private int SelectedEquipmentId()
        {
            object v = cmbEquipment.SelectedValue;
            if (v == null) return 0;
            return Convert.ToInt32(v);
        }

        private DateTime? GetDateOrNull(UIDatePicker dp)
        {
            return dp.IsEmpty ? (DateTime?)null : dp.Value.Date;
        }

        private static string ActionText(BorrowRecord r)
        {
            if (r.Status == BorrowConst.Pending) return "审批";
            if (r.Status == BorrowConst.Approved || r.Status == BorrowConst.Overdue) return "归还";
            return "详情";
        }

        private static Color StatusBackColor(string status)
        {
            switch (status)
            {
                case BorrowConst.Pending: return Color.FromArgb(232, 147, 12);
                case BorrowConst.Approved: return Color.FromArgb(46, 165, 68);
                case BorrowConst.Overdue: return Color.FromArgb(230, 80, 80);
                case BorrowConst.Returned: return Color.FromArgb(128, 128, 128);
                case BorrowConst.Rejected: return Color.FromArgb(169, 169, 169);
                default: return Color.FromArgb(128, 128, 128);
            }
        }

        // ==================== 事件：查询 / 刷新 ====================

        private void txtKeyword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                uiPagination1.ActivePage = 1;
                LoadData();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            uiPagination1.ActivePage = 1;
            LoadData();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtKeyword.Text = "";
            cmbStatus.SelectedIndex = 0;
            cmbEquipment.SelectedIndex = 0;
            dpFrom.Clear();
            dpTo.Clear();
            uiPagination1.ActivePage = 1;
            LoadData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Task.Run(delegate
            {
                try { BorrowBLL.MarkOverdue(); }
                catch (Exception) { }
                SafeBeginInvoke(() =>
                {
                    LoadData();
                    if (pnlCalendar.Visible) LoadCalendarDates();
                    if (pnlChart.Visible) LoadCharts();
                });
            });
        }

        private void uiPagination1_PageChanged(object sender, object pagingSource, int pageIndex, int count)
        {
            LoadData();
        }

        // ==================== 事件：列表操作 ====================

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _currentRecords.Count) return;
            if (e.ColumnIndex != _actionColumnIndex) return;

            BorrowRecord r = _currentRecords[e.RowIndex];
            if (r.Status == BorrowConst.Pending) OpenApprove(r);
            else if (r.Status == BorrowConst.Approved || r.Status == BorrowConst.Overdue) OpenReturn(r);
            else OpenDetail(r);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            using (var dlg = new BorrowApplyDialog())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    uiPagination1.ActivePage = 1;
                    LoadData();
                    if (pnlCalendar.Visible) LoadCalendarDates();
                    if (pnlChart.Visible) LoadCharts();
                }
            }
        }

        private void OpenApprove(BorrowRecord r)
        {
            using (var dlg = new BorrowApproveDialog(r))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    LoadData();
                    if (pnlCalendar.Visible) LoadCalendarDates();
                    if (pnlChart.Visible) LoadCharts();
                }
            }
        }

        private void OpenReturn(BorrowRecord r)
        {
            using (var dlg = new BorrowReturnDialog(r))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    LoadData();
                    if (pnlCalendar.Visible) LoadCalendarDates();
                    if (pnlChart.Visible) LoadCharts();
                }
            }
        }

        private void OpenDetail(BorrowRecord r)
        {
            using (var dlg = new BorrowDetailForm(r))
            {
                dlg.ShowDialog(this);
            }
        }

        // ==================== 视图切换 ====================

        private void btnView_Click(object sender, EventArgs e)
        {
            if (sender == btnListView)
            {
                pnlCalendar.Visible = false;
                pnlChart.Visible = false;
                pnlList.Visible = true;
            }
            else if (sender == btnCalendarView)
            {
                pnlList.Visible = false;
                pnlChart.Visible = false;
                pnlCalendar.Visible = true;
                ShowCalendarView();
            }
            else
            {
                pnlList.Visible = false;
                pnlCalendar.Visible = false;
                pnlChart.Visible = true;
                LoadCharts();
            }
        }

        // ==================== 日历视图 ====================

        private void ShowCalendarView()
        {
            // 进入视图：异步加载加粗日期，加载完成后统一设置选中今天并刷新当日列表，
            // 全程不阻塞界面线程，也避免 UpdateBoldedDates 反复触发 DateChanged 导致选中乱跳
            LoadCalendarDates();
        }

        /// <summary>异步加载所有应还日期并加粗标记</summary>
        private void LoadCalendarDates()
        {
            _loadingCalendar = true;
            Task.Run(delegate
            {
                List<DateTime> dates;
                try
                {
                    dates = BorrowBLL.ReturnDates();
                }
                catch (Exception ex)
                {
                    SafeBeginInvoke(() =>
                    {
                        _loadingCalendar = false;
                        UIMessageBox.ShowError("日历数据加载失败：" + ex.Message);
                    });
                    return;
                }
                SafeBeginInvoke(() =>
                {
                    monthCalendar1.RemoveAllBoldedDates();
                    foreach (DateTime d in dates)
                    {
                        monthCalendar1.AddBoldedDate(d);
                    }
                    monthCalendar1.UpdateBoldedDates();
                    monthCalendar1.SelectionStart = DateTime.Today;
                    monthCalendar1.SelectionEnd = DateTime.Today;
                    _loadingCalendar = false;
                    RefreshCalendarDay(DateTime.Today);
                });
            });
        }

        private void monthCalendar1_MouseDown(object sender, MouseEventArgs e)
        {
            _calendarLastUserInput = DateTime.Now;
        }

        private void monthCalendar1_KeyDown(object sender, KeyEventArgs e)
        {
            _calendarLastUserInput = DateTime.Now;
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            if (_loadingCalendar) return;

            DateTime selected = monthCalendar1.SelectionStart.Date;

            // 非用户操作（控件重绘/初始化/加粗刷新）触发的伪 DateChanged：
            // 忽略并把选中日期还原到最近一次显示的日期，避免日历选中乱跳
            if (DateTime.Now - _calendarLastUserInput > TimeSpan.FromSeconds(1))
            {
                if (_calendarDisplayDate != DateTime.MinValue && selected != _calendarDisplayDate)
                {
                    _loadingCalendar = true;
                    monthCalendar1.SelectionStart = _calendarDisplayDate;
                    monthCalendar1.SelectionEnd = _calendarDisplayDate;
                    _loadingCalendar = false;
                }
                return;
            }

            _calendarLastUserInput = DateTime.MinValue;
            RefreshCalendarDay(selected);
        }

        /// <summary>异步加载某日应还记录并刷新下方列表</summary>
        private void RefreshCalendarDay(DateTime date)
        {
            date = date.Date;
            if (date == _calendarDisplayDate) return;

            Task.Run(delegate
            {
                List<BorrowRecord> records;
                try
                {
                    records = BorrowBLL.GetByReturnDate(date);
                }
                catch (Exception ex)
                {
                    SafeBeginInvoke(() => UIMessageBox.ShowError("应还列表加载失败：" + ex.Message));
                    return;
                }
                SafeBeginInvoke(() =>
                {
                    // 期间用户又切换了日期则丢弃过期结果
                    if (monthCalendar1.SelectionStart.Date != date) return;
                    if (date == _calendarDisplayDate) return;

                    _calendarDisplayDate = date;
                    _calendarRecords = records;
                    lblCalendarHint.Text = date.ToString("yyyy-MM-dd") + " 应还借用（共 " + _calendarRecords.Count + " 条）";
                    lstCalendar.Items.Clear();
                    foreach (BorrowRecord r in _calendarRecords)
                    {
                        lstCalendar.Items.Add(string.Format("{0,-16}{1,-26}{2,-12}{3}",
                            r.BorrowNo, r.EquipmentName ?? "", r.ApplicantName ?? "", r.StatusText));
                    }
                });
            });
        }

        private void lstCalendar_DoubleClick(object sender, EventArgs e)
        {
            int idx = lstCalendar.SelectedIndex;
            if (idx < 0 || idx >= _calendarRecords.Count) return;
            OpenDetail(_calendarRecords[idx]);
        }

        // ==================== 统计视图 ====================

        private void LoadCharts()
        {
            Task.Run(delegate
            {
                List<CountItem> top = null, dept = null, borrow = null, ret = null, status = null;
                Exception lastError = null;
                // Azure SQL 偶发瞬时网络错误（如“信号灯超时/连接被中断”），失败后自动重试一次再报错
                for (int attempt = 1; attempt <= 2; attempt++)
                {
                    try
                    {
                        top = BorrowBLL.TopEquipment();
                        dept = BorrowBLL.ByDept();
                        borrow = BorrowBLL.BorrowByMonth();
                        ret = BorrowBLL.ReturnByMonth();
                        status = BorrowBLL.EquipmentStatus();
                        lastError = null;
                        break;
                    }
                    catch (Exception ex)
                    {
                        lastError = ex;
                        if (attempt == 1) Thread.Sleep(1500);
                    }
                }
                if (lastError != null)
                {
                    Exception err = lastError;
                    SafeBeginInvoke(() => UIMessageBox.ShowError("统计数据加载失败（已自动重试一次）：" + err.Message));
                    return;
                }
                SafeBeginInvoke(() =>
                {
                    LoadBarChart(top);
                    LoadPieChart(dept);
                    LoadLineChart(borrow, ret);
                    LoadDoughnutChart(status);
                    _chartsLoaded = true;
                });
            });
        }

        private void chartBar_SizeChanged(object sender, EventArgs e)
        {
            // UIBarChart 在布局过程中尺寸瞬时为 0 时，内部 CalcData 会把绘制标记清掉，
            // 导致之后重绘空白（闪一下后消失）。尺寸恢复有效时调用 Refresh() 重新
            // SetOption + CalcData，保证绘制标记复位。
            if (!_chartsLoaded) return;
            if (chartBar.Width <= 0 || chartBar.Height <= 0) return;
            chartBar.Refresh();
        }

        private void pnlChart_SizeChanged(object sender, EventArgs e)
        {
            // pnlChart 改为标准 Panel 后支持 AutoScroll：窗体放大时图表跟随容器变大，
            // 窗体缩小时保持最小高度 620，超出部分用滚动条查看，避免图表被压扁。
            if (chartLayout == null) return;
            int w = Math.Max(pnlChart.ClientSize.Width, 300);
            int h = Math.Max(pnlChart.ClientSize.Height, 620);
            if (chartLayout.Size.Width != w || chartLayout.Size.Height != h)
                chartLayout.Size = new Size(w, h);
        }

        private void LoadBarChart(List<CountItem> top)
        {
            var option = new UIBarOption();
            // UIBarOption 默认 Title.Text = "SunnyUI Chart"，会显示在图表顶部，需清空
            option.Title.Text = "";
            option.Title.SubText = "";
            option.ShowValue = true;
            option.XAxis.Name = "设备";
            option.YAxis.Name = "借用次数";
            option.YAxis.ShowGridLine = true;

            var series = new UIBarSeries { Name = "借用次数" };
            for (int i = 0; i < top.Count; i++)
            {
                // UIBarChart 以 XAxis.Data 为准生成柱子，必须把分类名填进去，
                // 否则 CalcData 直接返回导致图表空白
                option.XAxis.Data.Add(top[i].Name);
                series.AddData(top[i].Value, ChartColors[i % ChartColors.Length]);
            }
            option.AddSeries(series);
            chartBar.SetOption(option);
            chartBar.Refresh();
        }

        private void LoadPieChart(List<CountItem> dept)
        {
            var option = new UIPieOption();
            // 同上：清掉默认的 "SunnyUI Chart" 标题
            option.Title.Text = "";
            option.Title.SubText = "";
            var series = new UIPieSeries { Name = "科室借用" };
            for (int i = 0; i < dept.Count; i++)
            {
                series.AddData(dept[i].Name, dept[i].Value, ChartColors[i % ChartColors.Length]);
            }
            option.AddSeries(series);
            chartPie.SetOption(option);
        }

        private void LoadLineChart(List<CountItem> borrow, List<CountItem> ret)
        {
            var borrowMap = borrow.ToDictionary(c => c.Name, c => c.Value);
            var returnMap = ret.ToDictionary(c => c.Name, c => c.Value);
            List<string> months = borrow.Select(c => c.Name)
                .Union(ret.Select(c => c.Name))
                .OrderBy(m => m)
                .ToList();
            if (months.Count == 0)
            {
                // 无数据时也用一个空 option，但要清掉默认的 "SunnyUI Chart" 标题
                var empty = new UILineOption();
                empty.Title.Text = "";
                empty.Title.SubText = "";
                chartLine.SetOption(empty);
                return;
            }

            var option = new UILineOption();
            // 清掉默认的 "SunnyUI Chart" 标题
            option.Title.Text = "";
            option.Title.SubText = "";
            option.XAxis.Name = "月份";
            option.YAxis.Name = "数量";
            option.XAxisType = UIAxisType.DateTime;
            option.XAxis.ShowGridLine = false;
            option.YAxis.ShowGridLine = true;
            option.AddSeries("借用", false);
            option.AddSeries("归还", false);

            foreach (string m in months)
            {
                DateTime dt = DateTime.ParseExact(m, "yyyy-MM", CultureInfo.InvariantCulture);
                double b = 0, r = 0;
                if (borrowMap.ContainsKey(m)) b = borrowMap[m];
                if (returnMap.ContainsKey(m)) r = returnMap[m];
                option.AddData("借用", dt, b);
                option.AddData("归还", dt, r);
            }
            chartLine.SetOption(option);
        }

        private void LoadDoughnutChart(List<CountItem> status)
        {
            var option = new UIDoughnutOption();
            // 同上：清掉默认的 "SunnyUI Chart" 标题
            option.Title.Text = "";
            option.Title.SubText = "";
            var series = new UIDoughnutSeries { Name = "设备状态" };
            for (int i = 0; i < status.Count; i++)
            {
                series.AddData(BorrowConst.EquipmentStatusText(status[i].Name), status[i].Value,
                    ChartColors[i % ChartColors.Length]);
            }
            option.AddSeries(series);
            chartDoughnut.SetOption(option);
        }

        private void lblOverdueCaption_Click(object sender, EventArgs e)
        {

        }
    }
}
