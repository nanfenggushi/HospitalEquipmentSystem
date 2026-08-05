using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using HospitalEquipment.BLL;
using HospitalEquipment.Model;
using HospitalEquipment.Model.management;
using Sunny.UI;

namespace HospitalEquipmentSystem.UI
{
    /// <summary>
    /// 借用申请对话框：选择可用设备、填写用途与归还日期，实时校验设备可借性
    /// </summary>
    public partial class BorrowApplyDialog : UIForm
    {
        private List<Equipment> _available;

        public BorrowApplyDialog()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;
            lblBorrowerValue.Text = LoginUser.DisplayName;
            dpReturn.MinDate = DateTime.Today;
            dpReturn.Value = DateTime.Today.AddDays(7);
            LoadAvailableEquipment();
            cmbEquipment.SelectedIndexChanged += cmbEquipment_SelectedIndexChanged;
            btnSubmit.Click += btnSubmit_Click;
            btnCancel.Click += (s, e) => DialogResult = DialogResult.Cancel;
        }

        private void LoadAvailableEquipment()
        {
            try
            {
                _available = BorrowBLL.GetAvailableEquipment();
            }
            catch(Exception ex)
            {
                lblCheck.ForeColor = Color.Red;
                lblCheck.Text="设备加载失败"+ex.Message;
                cmbEquipment.Enabled = false;
                return;
            }
            if (_available.Count == 0)
            {
                lblCheck.ForeColor = Color.Red;
                lblCheck.Text = "当前没有可借用的空闲设备";
                cmbEquipment.Enabled = false;
                return;
            }

            cmbEquipment.DataSource = _available;
            cmbEquipment.DisplayMember = "EquipmentName";
            cmbEquipment.ValueMember = "EquipmentId";
            CheckAvailability();
        }

        private void cmbEquipment_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckAvailability();
        }

        private void CheckAvailability()
        {
            if (!(cmbEquipment.SelectedItem is Equipment eq)) return;
            try
            {
                string reason = BorrowBLL.GetEquipmentAvailableMessage(eq.EquipmentId);
                if (reason == null)
                {
                    lblCheck.ForeColor = Color.Green;
                    lblCheck.Text = "该设备可借用";
                }
                else
                {
                    lblCheck.ForeColor = Color.Red;
                    lblCheck.Text = reason;
                }
            }
            catch (Exception ex)
            {
                lblCheck.ForeColor = Color.Red;
                lblCheck.Text = "校验失败：" + ex.Message;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!(cmbEquipment.SelectedItem is Equipment eq))
            {
                UIMessageBox.ShowWarning("请选择要借用的设备");
                return;
            }

            string purpose = txtPurpose.Text.Trim();
            if (string.IsNullOrEmpty(purpose))
            {
                UIMessageBox.ShowWarning("请填写借用用途");
                txtPurpose.Focus();
                return;
            }

            BLLResult result;
            try
            {
                result = BorrowBLL.Apply(
                    eq.EquipmentId,
                    LoginUser.UserId,
                    LoginUser.DeptId,
                    purpose,
                    dpReturn.Value,
                    txtRemarks.Text.Trim());
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError("提交失败：" + ex.Message);
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
