using System;
using System.Drawing;
using System.Windows.Forms;
using HospitalEquipment.Model;
using Sunny.UI;

namespace HospitalEquipmentSystem.UI
{
    /// <summary>
    /// 借用详情窗体（只读展示）
    /// </summary>
    public partial class BorrowDetailForm : UIForm
    {
        public BorrowDetailForm(BorrowRecord record)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;
            FillInfo(record);
            btnClose.Click += (s, e) => Close();
        }

        private void FillInfo(BorrowRecord r)
        {
            lblNoValue.Text = r.BorrowNo;
            lblStatusValue.Text = r.StatusText;
            if (r.OverdueDays > 0)
            {
                lblStatusValue.ForeColor = Color.Red;
                lblStatusValue.Text = r.StatusText + "（超期 " + r.OverdueDays + " 天）";
            }
            else if (r.Status == BorrowConst.Pending)
            {
                lblStatusValue.ForeColor = Color.FromArgb(232, 147, 12);
            }
            else if (r.Status == BorrowConst.Returned)
            {
                lblStatusValue.ForeColor = Color.Gray;
            }
            else
            {
                lblStatusValue.ForeColor = Color.FromArgb(46, 165, 68);
            }

            lblEquipmentValue.Text = (r.EquipmentNo ?? "") + "  " + r.EquipmentName;
            lblApplicantValue.Text = r.ApplicantName;
            lblDeptValue.Text = r.ApplicantDeptName;
            lblPurposeValue.Text = r.Purpose;
            lblReturnValue.Text = r.ExpectedReturnDate.ToString("yyyy-MM-dd");
            lblApproveValue.Text = r.ApproveDate.HasValue
                ? (r.ApproverName ?? "") + "  " + r.ApproveDate.Value.ToString("yyyy-MM-dd HH:mm")
                : "未审批";
            lblActualReturnValue.Text = r.ActualReturnDate.HasValue
                ? r.ActualReturnDate.Value.ToString("yyyy-MM-dd") + (string.IsNullOrEmpty(r.ReturnNote) ? "" : "（" + r.ReturnNote + "）")
                : "未归还";
            lblCreatedValue.Text = r.CreatedAt.ToString("yyyy-MM-dd HH:mm");
            lblRemarksValue.Text = string.IsNullOrEmpty(r.Remarks) ? "无" : r.Remarks;
        }
    }
}
