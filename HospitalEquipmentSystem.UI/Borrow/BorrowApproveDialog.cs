using System;
using System.Windows.Forms;
using HospitalEquipment.BLL;
using HospitalEquipment.Model;
using HospitalEquipment.Util;
using Sunny.UI;

namespace HospitalEquipmentSystem.UI
{
    /// <summary>
    /// 借用审批对话框：查看申请信息，通过或驳回
    /// </summary>
    public partial class BorrowApproveDialog : UIForm
    {
        private readonly BorrowRecord _record;

        public BorrowApproveDialog(BorrowRecord record)
        {
            InitializeComponent();
            ThemeHelper.ApplyDarkTheme(this);
            StartPosition = FormStartPosition.CenterParent;
            _record = record;
            FillInfo(record);
            btnApprove.Click += btnApprove_Click;
            btnReject.Click += btnReject_Click;
            btnCancel.Click += (s, e) => DialogResult = DialogResult.Cancel;
        }

        private void FillInfo(BorrowRecord r)
        {
            lblNoValue.Text = r.BorrowNo;
            lblEquipmentValue.Text = (r.EquipmentNo ?? "") + "  " + r.EquipmentName;
            lblApplicantValue.Text = r.ApplicantName;
            lblDeptValue.Text = r.ApplicantDeptName;
            lblPurposeValue.Text = r.Purpose;
            lblReturnValue.Text = r.ExpectedReturnDate.ToString("yyyy-MM-dd");
            lblCreatedValue.Text = r.CreatedAt.ToString("yyyy-MM-dd HH:mm");
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (!UIMessageBox.ShowAsk("确认审批通过该借用申请？"))
                return;

            BLLResult result;
            try
            {
                result = BorrowBLL.Approve(_record.BorrowId, LoginUser.UserId);
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError("审批失败：" + ex.Message);
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

        private void btnReject_Click(object sender, EventArgs e)
        {
            if (!UIMessageBox.ShowAsk("确认驳回该借用申请？"))
                return;

            BLLResult result;
            try
            {
                result = BorrowBLL.Reject(_record.BorrowId, LoginUser.UserId);
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError("驳回失败：" + ex.Message);
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
