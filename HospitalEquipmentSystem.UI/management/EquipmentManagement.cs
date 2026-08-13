using HospitalEquipment.Util;
using HospitalEquipmentSystem.Common;
using HospitalEquipmentSystem.UI.management;
using Sunny.UI;
using System;
using System.Windows.Forms;

namespace HospitalEquipmentSystem.UI
{
    public partial class EquipmentManagement : UIForm
    {
        public EquipmentManagement()
        {
            InitializeComponent();
            ThemeHelper.ApplyDarkTheme(this);
        }

        private void uiDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void EquipmentManagement_Load(object sender, EventArgs e)
        {
            // 事件已在 Designer.cs 中绑定，无需重复绑定
            Redirect.NavigateTo<List>(uiPanel1);
        }

        private void OnBtnClick(object sender, EventArgs e)
        {
            UIButton button = (UIButton)sender;
            string path = button.TagString;
            switch (path)
            {
                case "List": PageManager.ShowPage<List>(uiPanel1); break;
                case "class": PageManager.ShowPage<Classification>(uiPanel1); break;
                case "Supply": PageManager.ShowPage<Supplier>(uiPanel1); break;
                case "Warehousing": PageManager.ShowPage<WarehousingManagement>(uiPanel1); break;
            }
        }
    }
}
