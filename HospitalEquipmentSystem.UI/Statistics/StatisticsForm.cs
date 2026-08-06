using HospitalEquipment.BLL;
using HospitalEquipment.Model.Dashboard;
using ScottPlot;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HospitalEquipmentSystem.UI.Dashboard
{
    public partial class StatisticsForm : UIForm
    {
        private readonly EquipmentBLL _equipmentBLL = new EquipmentBLL();
        private readonly MaintenanceRecordsBLL _maintenanceRecordsBLL = new MaintenanceRecordsBLL();
        private StatisticCardDto cardDto;
        private bool _isLoading = false; // 查询状态，防止还没有查询完数据又再次查询

        // 存储用于 ScottPlot 渲染的数组 (最近6个月)
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

        private void StatisticsForm_Load(object sender, EventArgs e)
        {
            // 关闭DataGridView的自动创建列功能
            uiDataGridView1.AutoGenerateColumns = false;

            LoadStatisticCardsAsync();
            LoadDeviceStatusChartAsync();
            LoadDeviceUsageChartAsync();
            LoadMaintenanceTrendChartAsync();
            LoadRealTimeAlarmListAsync();
        }

        /// <summary>
        /// 加载实时报警列表
        /// </summary>
        private async void LoadRealTimeAlarmListAsync()
        {
            List<MaintenanceRecordsDto> alarmList = _maintenanceRecordsBLL.GetAlarmList();

            uiDataGridView1.DataSource = alarmList;
        }

        /// <summary>
        /// 加载统计卡片内容
        /// </summary>
        private async void LoadStatisticCardsAsync()
        {
            try
            {
                cardDto = _equipmentBLL.GetStatisticCardData();

                lblTotalCount.Text = cardDto.TotalCount.ToString();
                lblNormalCount.Text = cardDto.NormalCount.ToString();
                lblMaintenanceCount.Text = cardDto.MaintenanceCount.ToString();
                lblBorrowedCount.Text = cardDto.BorrowedCount.ToString();
                lblScrappedCount.Text = cardDto.ScrappedCount.ToString();
            } catch (Exception ex)
            {
                UIMessageBox.Show("加载统计卡片失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 加载设备状态占比饼状图数据
        /// </summary>
        private async void LoadDeviceStatusChartAsync()
        {
            // 1. 去掉控件内部的灰底和蓝框，和外层卡片融为一体
            uiDoughnutChart1.FillColor = System.Drawing.Color.White;
            uiDoughnutChart1.RectColor = System.Drawing.Color.Transparent;

            // 2. 创建配置对象
            UIDoughnutOption option = new UIDoughnutOption();

            // 清空控件内部标题
            option.Title = null;

            // 3. 配置提示框 (鼠标悬浮显示数值)
            option.ToolTip = new UIPieToolTip { Visible = true };

            // 🌟 关键修改 2：把图例设置为“横向平铺” (UIOrient.Horizontal)
            // 这样 5 个状态说明就会在上方整整齐齐地平铺开，绝不重叠！
            option.Legend = new UILegend {
                Orient = UIOrient.Horizontal // 横向一字排开
            };

            // 为图例添加 5 个状态名称
            option.Legend.AddData("使用中");
            option.Legend.AddData("空闲闲置");
            option.Legend.AddData("跨科借用");
            option.Legend.AddData("故障维修");
            option.Legend.AddData("已报废");

            // 4. 配置环形系列
            var series = new UIDoughnutSeries { Name = "设备状态" };

            // 把圆环中心设为 (50, 60)，往下方挪一点，给上方的说明留出空间！
            series.Center = new UICenter(50, 60);
            series.Radius.Inner = 42; // 内洞大小
            series.Radius.Outer = 72; // 外环大小

            // 5. 填充 5 个状态的数据
            series.AddData("使用中", cardDto.InUseCount);
            series.AddData("空闲闲置", cardDto.IdleCount);
            series.AddData("跨科借用", cardDto.BorrowedCount);
            series.AddData("故障维修", cardDto.MaintenanceCount);
            series.AddData("已报废", cardDto.ScrappedCount);

            // 6. 刷入配置并刷新图表
            option.Series.Add(series);
            uiDoughnutChart1.SetOption(option);
        }

        /// <summary>
        /// 从数据库动态加载使用率柱状图
        /// </summary>
        private async void LoadDeviceUsageChartAsync()
        {
            try
            {
                List<CategoryUsageDto> dt = _equipmentBLL.GetCategoryUsageData();

                uiBarChart1.FillColor = System.Drawing.Color.White;
                uiBarChart1.RectColor = System.Drawing.Color.Transparent;

                UIBarOption option = new UIBarOption();
                option.Title = null; // 清空内置标题
                option.ToolTip = new UIBarToolTip { Visible = true };

                var series = new UIBarSeries { Name = "使用率 (%)" };

                // 2. 循环添加数据库查询到的真实数据
                foreach (var item in dt)
                {
                    string categoryName = item.CategoryName;
                    double usageRate = item.UsageRate;

                    // X 轴添加名称，Series 添加数值
                    option.XAxis.Data.Add(categoryName);
                    series.AddData(usageRate);
                }

                option.Series.Add(series);
                uiBarChart1.SetOption(option);
            } catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"加载柱状图失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 加载最近 6 个月的维保趋势图
        /// </summary>
        private async void LoadMaintenanceTrendChartAsync()
        {
            // 1. 通过 BLL 业务逻辑层获取实体数据列表
            List<MonthlyMaintenanceDto> dtoList = _maintenanceRecordsBLL.GetRecent6MonthsTrend();

            // 2. 使用 LINQ 一秒提取 ScottPlot 需要的 4 个数组
            monthsX = Enumerable.Range(1, dtoList.Count).Select(i => (double)i).ToArray(); // { 1, 2, 3, 4, 5, 6 }
            faultsY = dtoList.Select(x => x.FaultCount).ToArray();
            repairedY = dtoList.Select(x => x.RepairedCount).ToArray();
            monthLabels = dtoList.Select(x => x.MonthLabel).ToArray(); // { "2月", "3月", "4月", "5月", "6月", "7月" }

            // 3. 开始使用 ScottPlot 5 绘制图表
            var plot = formsPlot1.Plot;
            plot.Clear();

            // 绘制普通折线
            var line1 = plot.Add.Scatter(monthsX, faultsY);
            line1.LegendText = "新增故障";

            var line2 = plot.Add.Scatter(monthsX, repairedY);
            line2.LegendText = "完成维修";

            // 添加原生十字光标组件
            crosshair = plot.Add.Crosshair(0, 0);

            // X 轴显示动态的最近6个月标签
            var manualTicks = new ScottPlot.TickGenerators.NumericManual();
            for (int i = 0; i < monthsX.Length; i++)
            {
                manualTicks.AddMajor(monthsX[i], monthLabels[i]);
            }
            plot.Axes.Bottom.TickGenerator = manualTicks;

            // 4. 防止中文变方块
            string fontName = "微软雅黑";
            plot.Axes.Bottom.TickLabelStyle.FontName = fontName;
            plot.Axes.Left.TickLabelStyle.FontName = fontName;
            plot.Legend.FontName = fontName;

            // 自动算出 Y 轴最大值，并向上多留 35% 的空间给右上角图例
            double maxVal = Math.Max(faultsY.Max(), repairedY.Max());
            double maxY = maxVal > 0 ? maxVal * 1.35 : 5;
            plot.Axes.SetLimitsY(0, maxY);
            plot.Axes.SetLimitsX(0.5, 6.5); // X 轴局限在 6 个月内

            // 5. 图例移动到右上角
            plot.ShowLegend(Alignment.UpperRight);

            // 6. 刷新图表
            formsPlot1.Refresh();
        }

        /// <summary>
        /// 折线图的鼠标移动事件 (显示实时数值和动态月份)
        /// </summary>
        private void FormsPlot1_MouseMove(object sender, MouseEventArgs e)
        {
            if (crosshair != null && monthLabels != null && monthLabels.Length > 0)
            {
                // 1. 获取当前鼠标位置的图表坐标
                Pixel mousePixel = new Pixel(e.X, e.Y);
                Coordinates mouseLocation = formsPlot1.Plot.GetCoordinates(mousePixel);

                // 2. 更新十字光标交叉点位置
                crosshair.Position = mouseLocation;

                // 3. 在横线 (Y轴) 上实时显示 Y 轴数值 (如 35)
                crosshair.HorizontalLine.Text = $" {mouseLocation.Y:F0} ";
                crosshair.HorizontalLine.LabelFontName = "微软雅黑";
                crosshair.HorizontalLine.LabelFontSize = 10;

                // 4. 在竖线 (X轴) 上实时显示对应的动态月份 (如 "7月")
                int monthIndex = (int)Math.Round(mouseLocation.X);
                if (monthIndex >= 1 && monthIndex <= monthLabels.Length)
                {
                    crosshair.VerticalLine.Text = $" {monthLabels[monthIndex - 1]} ";
                    crosshair.VerticalLine.LabelFontName = "微软雅黑";
                    crosshair.VerticalLine.LabelFontSize = 10;
                } else
                {
                    crosshair.VerticalLine.Text = ""; // 移出月份范围时不显示
                }

                // 5. 刷新图表
                formsPlot1.Refresh();
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}