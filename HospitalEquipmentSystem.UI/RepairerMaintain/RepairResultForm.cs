using HospitalEquipment.Util;
using Sunny.UI;
using System;
using System.Windows.Forms;

namespace HospitalEquipmentSystem.UI
{
    /// <summary>
    /// 维修结果提交弹窗：维修员完成维修后填写维修结果、费用、停机时长
    /// </summary>
    public partial class RepairResultForm : UIForm
    {
        /// <summary>维修结果描述</summary>
        public string RepairResult => txtResult.Text.Trim();

        /// <summary>维修费用</summary>
        public decimal? RepairCost
        {
            get
            {
                if (string.IsNullOrWhiteSpace(txtCost.Text)) return null;
                return decimal.TryParse(txtCost.Text, out var v) ? v : (decimal?)null;
            }
        }

        /// <summary>停机时长（小时）</summary>
        public int? DowntimeHours
        {
            get
            {
                if (string.IsNullOrWhiteSpace(txtDowntime.Text)) return null;
                return int.TryParse(txtDowntime.Text, out var v) ? v : (int?)null;
            }
        }

        public RepairResultForm(string repairNo, string equipmentName)
        {
            InitializeComponent();
            ThemeHelper.ApplyDarkTheme(this);

            if (DesignMode) return;

            this.Text = $"维修结果 - {repairNo}";
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtResult.Text))
            {
                UIMessageBox.ShowWarning("请填写维修结果描述");
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
