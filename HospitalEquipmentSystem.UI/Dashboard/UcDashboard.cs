using HospitalEquipment.BLL;
using HospitalEquipment.Model;
using Sunny.UI;
using System;
using System.Drawing;

namespace HospitalEquipmentSystem.UI.Dashboard
{
    public partial class UcDashboard : UIForm
    {
        private readonly EquipmentBLL _equipmentBLL = new EquipmentBLL();
        private StatisticCardDto cardDto;

        public UcDashboard()
        {
            InitializeComponent();
        }

        private void UcDashboard_Load(object sender, EventArgs e)
        {
            LoadStatisticCards();
            RenderDoughnutChart();
        }

        /// <summary>
        /// 加载统计卡片内容
        /// </summary>
        private void LoadStatisticCards()
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


        private void RenderDoughnutChart()
        {
            // 1. 去掉控件内部的灰底和蓝框，和外层卡片融为一体（解决双重框丑的问题）
            uiDoughnutChart1.FillColor = Color.White;
            uiDoughnutChart1.RectColor = Color.Transparent;

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
    }
}
