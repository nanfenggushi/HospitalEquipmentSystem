using System;
using System.Drawing;
using System.Windows.Forms;
using HospitalEquipment.BLL;
using HospitalEquipment.Model;
using HospitalEquipment.Util;
using Sunny.UI;

namespace HospitalEquipmentSystem.UI
{
    /// <summary>
    /// 归还验收对话框：登记归还，支持损坏情况记录并自动创建维修工单
    /// </summary>
    public partial class BorrowReturnDialog : UIForm
    {
        private readonly BorrowRecord _record;

        public BorrowReturnDialog(BorrowRecord record)
        {
            InitializeComponent();
            ThemeHelper.ApplyDarkTheme(this);
            StartPosition = FormStartPosition.CenterParent;
            _record = record;
            FillInfo(record);
            txtDamageDesc.Enabled = false;
            chkDamage.CheckedChanged += (s, e) => txtDamageDesc.Enabled = chkDamage.Checked;
            btnConfirm.Click += btnConfirm_Click;
            btnCancel.Click += (s, e) => DialogResult = DialogResult.Cancel;
        }

        private void FillInfo(BorrowRecord r)
        {
            lblNoValue.Text = r.BorrowNo;
            lblEquipmentValue.Text = (r.EquipmentNo ?? "") + "  " + r.EquipmentName;
            lblApplicantValue.Text = r.ApplicantName;
            lblDeptValue.Text = r.ApplicantDeptName;
            lblReturnValue.Text = r.ExpectedReturnDate.ToString("yyyy-MM-dd");

            if (r.OverdueDays > 0)
            {
                lblOverdueValue.ForeColor = Color.Red;
                lblOverdueValue.Text = "已超期 " + r.OverdueDays + " 天";
            }
            else
            {
                lblOverdueValue.ForeColor = Color.Green;
                lblOverdueValue.Text = "未超期";
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!UIMessageBox.ShowAsk("确认完成归还验收？"))
                return;
            BLLResult result;
            try
            {
                result = BorrowBLL.ReturnBorrow(_record.BorrowId,
                txtReturnNote.Text.Trim(),
                chkDamage.Checked,
                txtDamageDesc.Text.Trim(),
                LoginUser.UserId,
                LoginUser.DeptId);
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError("归还失败：" + ex.Message);
                return;
            }

            if (result.Success)
            {
                UIMessageBox.ShowSuccess(result.Message);
                DialogResult = DialogResult.OK;
            }
            else
            {
                UIMessageBox.ShowError(result.Message);
            }
        }
    }
}
