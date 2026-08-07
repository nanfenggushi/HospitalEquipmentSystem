using HospitalEquipment.BLL;
using HospitalEquipment.Model.Dashboard;
using MiniExcelLibs;
using ScottPlot;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalEquipmentSystem.UI.Dashboard
{
    public partial class StatisticsForm : UIForm
    {
        private readonly EquipmentBLL _equipmentBLL = new EquipmentBLL();
        private readonly MaintenanceRecordsBLL _maintenanceRecordsBLL = new MaintenanceRecordsBLL();

        // 移除全局的 cardDto，确保每个组件使用自己的独立数据
        private bool isRefreshing = false; // 防止实时报警列表查询时间大于刷新间隔时重复查询数据

        // 存储用于 ScottPlot 渲染的数组 (最近6个月)，由于十字光标事件需要实时读取，保持全局
        private double[] monthsX;
        private double[] faultsY;
        private double[] repairedY;
        private string[] monthLabels;

        // 十字光标组件
        private ScottPlot.Plottables.Crosshair crosshair;

        public StatisticsForm()
        {
            InitializeComponent();
            // 2. 绑定鼠标移动事件，让十字光标跟随鼠标
            formsPlot1.MouseMove += FormsPlot1_MouseMove;
        }

        private async void StatisticsForm_LoadAsync(object sender, EventArgs e)
        {
            // 关闭DataGridView的自动创建列功能
            uiDataGridView1.AutoGenerateColumns = false;

            // 加载更新时间
            LoadLastUpdated();

            // 🌟 1. 【先渲染图表骨架】先用空数据把所有图表的外框、坐标轴、背景画出来，避免查询慢时界面一片空白
            RenderEmptySkeleton();

            // 🌟 2. 【后加载真实数据】所有数据独立并行加载，互不干扰，查完谁就更新谁
            await Task.WhenAll(
                LoadAndRenderStatisticCardsAsync(),
                LoadAndRenderDeviceStatusChartAsync(),
                LoadAndRenderDeviceUsageChartAsync(),
                LoadAndRenderMaintenanceTrendChartAsync(),
                LoadAndRenderRealTimeAlarmListAsync()
            );
        }

        /// <summary>
        /// 渲染图表的空骨架
        /// </summary>
        private void RenderEmptySkeleton()
        {
            // 1. 卡片全部显示 0 占位
            RenderStatisticCards(new StatisticCardDto());

            // 2. 饼状图渲染外框和图例，数据为 0
            RenderDeviceStatusChart(new StatisticCardDto());

            // 3. 柱状图传入空列表，只画出空坐标轴
            RenderDeviceUsageChart(new List<CategoryUsageDto>());

            // 4. 折线图画出空坐标系和主题色
            var plot = formsPlot1.Plot;
            plot.Clear();
            SetupScottPlotTheme(plot);
            plot.Axes.SetLimits(0.5, 6.5, 0, 10); // 假装有6个月的范围
            formsPlot1.Refresh();
        }

        /// <summary>
        /// 抽离 ScottPlot 折线图的公共主题配置
        /// </summary>
        private void SetupScottPlotTheme(Plot plot)
        {
            // 设置深色主题
            plot.FigureBackground.Color = ScottPlot.Color.FromHex("#223246");
            plot.DataBackground.Color = ScottPlot.Color.FromHex("#223246");

            // 设置坐标轴颜色
            plot.Axes.Color(ScottPlot.Color.FromHex("#8FA3B8"));

            // 设置网格线
            plot.Grid.MajorLineColor = ScottPlot.Color.FromHex("#FFFFFF20");

            // 防止中文变方块
            string fontName = "微软雅黑";
            plot.Axes.Bottom.TickLabelStyle.FontName = fontName;
            plot.Axes.Bottom.TickLabelStyle.ForeColor = ScottPlot.Color.FromHex("#B8C7D9");
            plot.Axes.Left.TickLabelStyle.ForeColor = ScottPlot.Color.FromHex("#B8C7D9");
            plot.Axes.Left.TickLabelStyle.FontName = fontName;
            plot.Legend.FontName = fontName;
        }

        /// <summary>
        /// 加载更新时间
        /// </summary>
        private void LoadLastUpdated()
        {
            uiLabel1.Text = "更新于" + DateTime.Now.ToString("t");
        }

        /// <summary>
        /// 独立加载并渲染统计卡片
        /// </summary>
        private async Task LoadAndRenderStatisticCardsAsync()
        {
            try
            {
                StatisticCardDto cardDto = await _equipmentBLL.GetStatisticCardDataAsync();
                RenderStatisticCards(cardDto);
            } catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"加载统计卡片失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 渲染统计卡片内容 (纯UI操作)
        /// </summary>
        private void RenderStatisticCards(StatisticCardDto cardDto)
        {
            lblTotalCount.Text = cardDto.TotalCount.ToString();
            lblNormalCount.Text = cardDto.NormalCount.ToString();
            lblMaintenanceCount.Text = cardDto.MaintenanceCount.ToString();
            lblBorrowedCount.Text = cardDto.BorrowedCount.ToString();
            lblScrappedCount.Text = cardDto.ScrappedCount.ToString();
        }

        /// <summary>
        /// 独立加载并渲染设备状态占比饼状图
        /// </summary>
        private async Task LoadAndRenderDeviceStatusChartAsync()
        {
            try
            {
                StatisticCardDto statusDto = await _equipmentBLL.GetStatisticCardDataAsync();
                RenderDeviceStatusChart(statusDto);
            } catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"加载设备状态饼图失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 渲染设备状态占比饼状图 (纯UI操作)
        /// </summary>
        private void RenderDeviceStatusChart(StatisticCardDto statusDto)
        {
            UIDoughnutOption option = new UIDoughnutOption();
            option.Title = null;
            option.ToolTip = new UIPieToolTip { Visible = true };

            // 🌟 关键修改 2：把图例设置为“横向平铺” (UIOrient.Horizontal)
            option.Legend = new UILegend { Orient = UIOrient.Horizontal };
            option.Legend.AddData("使用中");
            option.Legend.AddData("空闲闲置");
            option.Legend.AddData("跨科借用");
            option.Legend.AddData("故障维修");
            option.Legend.AddData("已报废");

            var series = new UIDoughnutSeries { Name = "设备状态" };
            series.Center = new UICenter(50, 60);
            series.Radius.Inner = 42;
            series.Radius.Outer = 72;

            series.AddData("使用中", statusDto.InUseCount);
            series.AddData("空闲闲置", statusDto.IdleCount);
            series.AddData("跨科借用", statusDto.BorrowedCount);
            series.AddData("故障维修", statusDto.MaintenanceCount);
            series.AddData("已报废", statusDto.ScrappedCount);

            option.Series.Add(series);
            uiDoughnutChart1.SetOption(option);
        }

        /// <summary>
        /// 独立加载并渲染使用率柱状图
        /// </summary>
        private async Task LoadAndRenderDeviceUsageChartAsync()
        {
            try
            {
                List<CategoryUsageDto> dt = await _equipmentBLL.GetCategoryUsageDataAsync();
                RenderDeviceUsageChart(dt);
            } catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"加载柱状图失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 渲染使用率柱状图 (纯UI操作)
        /// </summary>
        private void RenderDeviceUsageChart(List<CategoryUsageDto> dataList)
        {
            UIBarOption option = new UIBarOption();
            option.Title = null;
            option.ToolTip = new UIBarToolTip { Visible = true };
            var series = new UIBarSeries { Name = "使用率 (%)" };

            // 如果传进来是空数据，这里就不会进循环，图表就只会画出空的 X轴和Y轴
            foreach (var item in dataList)
            {
                option.XAxis.Data.Add(item.CategoryName);
                series.AddData(item.UsageRate);
            }

            option.Series.Add(series);
            uiBarChart1.SetOption(option);
        }

        /// <summary>
        /// 独立加载最近 6 个月的维保趋势图
        /// </summary>
        private async Task LoadAndRenderMaintenanceTrendChartAsync()
        {
            try
            {
                List<MonthlyMaintenanceDto> dtoList = await _maintenanceRecordsBLL.GetRecent6MonthsTrendAsync();
                RenderMaintenanceTrendChart(dtoList);
            } catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"加载折线图失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 渲染维保趋势图 (纯UI操作)
        /// </summary>
        private void RenderMaintenanceTrendChart(List<MonthlyMaintenanceDto> dtoList)
        {
            // 防御性判断，避免查询为空或查询失败时解析数据引发崩溃
            if (dtoList == null || dtoList.Count == 0) return;

            // 2. 提取数据
            monthsX = Enumerable.Range(1, dtoList.Count).Select(i => (double)i).ToArray();
            faultsY = dtoList.Select(x => x.FaultCount).ToArray();
            repairedY = dtoList.Select(x => x.RepairedCount).ToArray();
            monthLabels = dtoList.Select(x => x.MonthLabel).ToArray();

            // 3. 开始绘制图表
            var plot = formsPlot1.Plot;
            plot.Clear();

            // 调用抽离的统一主题配置
            SetupScottPlotTheme(plot);

            // 绘制折线
            var line1 = plot.Add.Scatter(monthsX, faultsY);
            line1.Color = ScottPlot.Color.FromHex("#FF6B6B");
            line1.LineWidth = 2;
            line1.LegendText = "新增故障";

            var line2 = plot.Add.Scatter(monthsX, repairedY);
            line2.Color = ScottPlot.Color.FromHex("#36CFC9");
            line2.LineWidth = 2;
            line2.LegendText = "完成维修";

            // 添加原生十字光标组件
            crosshair = plot.Add.Crosshair(0, 0);

            // X 轴显示动态标签
            var manualTicks = new ScottPlot.TickGenerators.NumericManual();
            for (int i = 0; i < monthsX.Length; i++)
            {
                manualTicks.AddMajor(monthsX[i], monthLabels[i]);
            }
            plot.Axes.Bottom.TickGenerator = manualTicks;

            // 自动算出 Y 轴最大值，并向上多留 35% 的空间给右上角图例
            double maxVal = Math.Max(faultsY.Max(), repairedY.Max());
            double maxY = maxVal > 0 ? maxVal * 1.35 : 5;
            plot.Axes.SetLimitsY(0, maxY);
            plot.Axes.SetLimitsX(0.5, 6.5);

            plot.ShowLegend(Alignment.UpperRight);
            formsPlot1.Refresh();
        }

        /// <summary>
        /// 折线图的鼠标移动事件
        /// </summary>
        private void FormsPlot1_MouseMove(object sender, MouseEventArgs e)
        {
            if (crosshair != null && monthLabels != null && monthLabels.Length > 0)
            {
                Pixel mousePixel = new Pixel(e.X, e.Y);
                Coordinates mouseLocation = formsPlot1.Plot.GetCoordinates(mousePixel);

                crosshair.Position = mouseLocation;
                crosshair.HorizontalLine.Text = $" {mouseLocation.Y:F0} ";
                crosshair.HorizontalLine.LabelFontName = "微软雅黑";
                crosshair.HorizontalLine.LabelFontSize = 10;

                int monthIndex = (int)Math.Round(mouseLocation.X);
                if (monthIndex >= 1 && monthIndex <= monthLabels.Length)
                {
                    crosshair.VerticalLine.Text = $" {monthLabels[monthIndex - 1]} ";
                    crosshair.VerticalLine.LabelFontName = "微软雅黑";
                    crosshair.VerticalLine.LabelFontSize = 10;
                } else
                {
                    crosshair.VerticalLine.Text = "";
                }

                formsPlot1.Refresh();
            }
        }

        /// <summary>
        /// 独立加载并渲染实时报警列表
        /// </summary>
        private async Task LoadAndRenderRealTimeAlarmListAsync()
        {
            try
            {
                List<MaintenanceRecordsDto> alarmList = await _maintenanceRecordsBLL.GetAlarmListAsync();
                RenderRealTimeAlarmList(alarmList);
            } catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"加载报警列表失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 渲染实时报警列表数据 (纯UI操作)
        /// </summary>
        private void RenderRealTimeAlarmList(List<MaintenanceRecordsDto> alarmList)
        {
            uiDataGridView1.DataSource = alarmList;
        }

        /// <summary>
        /// 间隔两秒刷新一次警报列表
        /// </summary>
        private async void timer1_Tick(object sender, EventArgs e)
        {
            if (isRefreshing) return;
            isRefreshing = true;
            try
            {
                await LoadAndRenderRealTimeAlarmListAsync();
            } catch (Exception ex)
            {
                UIMessageBox.Show("报警列表刷新失败：" + ex.Message);
            } finally
            {
                isRefreshing = false;
            }
        }

        /// <summary>
        /// 一键导出看板所有数据到 Excel
        /// </summary>
        private async void uiSymbolButton1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel 文件|*.xlsx";
            sfd.FileName = $"设备看板数据看板_{DateTime.Now:yyyyMMdd_HHmm}.xlsx";

            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                this.Cursor = Cursors.WaitCursor;

                var currentCardDto = await _equipmentBLL.GetStatisticCardDataAsync();

                var summarySheet = new[]
                {
                    new { 统计指标 = "设备总数", 数量 = currentCardDto.TotalCount },
                    new { 统计指标 = "正常运行 (使用中+闲置)", 数量 = currentCardDto.NormalCount },
                    new { 统计指标 = "跨科借用", 数量 = currentCardDto.BorrowedCount },
                    new { 统计指标 = "故障维修", 数量 = currentCardDto.MaintenanceCount },
                    new { 统计指标 = "已报废",   数量 = currentCardDto.ScrappedCount }
                };

                var usageList = await _equipmentBLL.GetCategoryUsageDataAsync();
                var usageSheet = usageList.Select(x => new {
                    设备类别 = x.CategoryName,
                    使用率 = x.UsageRate + "%"
                }).ToList();

                var trendList = await _maintenanceRecordsBLL.GetRecent6MonthsTrendAsync();
                var trendSheet = trendList.Select(x => new {
                    月份 = x.MonthLabel,
                    新增故障数 = x.FaultCount,
                    完成维修数 = x.RepairedCount
                }).ToList();

                var alarmSheet = await _maintenanceRecordsBLL.GetAlarmListAsync();

                var sheets = new Dictionary<string, object>
                {
                    { "概览统计", summarySheet },
                    { "分类使用率", usageSheet },
                    { "维保趋势分析", trendSheet },
                    { "实时报警记录", alarmSheet }
                };

                MiniExcel.SaveAs(sfd.FileName, sheets);
                UIMessageBox.ShowSuccess("数据全部导出成功！");
            } catch (Exception ex)
            {
                UIMessageBox.ShowError("导出失败: " + ex.Message);
            } finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}