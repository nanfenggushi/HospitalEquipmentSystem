using HospitalEquipment.BLL;
using HospitalEquipment.Model;
using Sunny.UI;
using System;

namespace HospitalEquipmentSystem.UI.Dashboard
{
    public partial class UcDashboard : UIForm
    {
        private readonly EquipmentBLL _equipmentBLL = new EquipmentBLL();

        public UcDashboard()
        {
            InitializeComponent();
        }

        private void UcDashboard_Load(object sender, EventArgs e)
        {
            LoadStatisticCards();
        }


        private void LoadStatisticCards()
        {
            try
            {
                StatisticCardDto dto = _equipmentBLL.GetStatisticCardData();

                lblTotalCount.Text = dto.TotalCount.ToString();
                lblNormalCount.Text = dto.NormalCount.ToString();
                lblMaintenanceCount.Text = dto.MaintenanceCount.ToString();
                lblBorrowedCount.Text = dto.BorrowedCount.ToString();
                lblScrappedCount.Text = dto.ScrappedCount.ToString();
            } catch (Exception ex)
            {
                UIMessageBox.Show("加载统计卡片失败：" + ex.Message);
            }
        }
    }
}
