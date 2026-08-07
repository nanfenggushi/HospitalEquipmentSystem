using HospitalEquipment.BLL;
using HospitalEquipment.Model;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace HospitalEquipmentSystem.UI
{
    public partial class LoginForm : UIForm
    {
        private readonly LoginBLL bll = new LoginBLL();
        private List<UserDto> _allUsers = new List<UserDto>();
        private UISymbolButton _selectedRoleBtn;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (cmbUser.SelectedValue == null)
            {
                UIMessageBox.ShowWarning("请选择人员");
                return;
            }

            int userId = Convert.ToInt32(cmbUser.SelectedValue);
            BLLResult r = bll.login(userId, txtPassword.Text.Trim(), out UserDto user);
            if (!r.Success)
            {
                UIMessageBox.ShowError(r.Message);
                txtPassword.Clear();
                txtPassword.Focus();
                return;
            }

            LoginUser.SetUser(user);
            this.DialogResult = DialogResult.OK;
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            SelectRole(btnAdmin);
            ApplyRoleFilter();
        }

        private void btnDoctor_Click(object sender, EventArgs e)
        {
            SelectRole(btnDoctor);
            ApplyRoleFilter();
        }

        private void btnRepair_Click(object sender, EventArgs e)
        {
            SelectRole(btnRepair);
            ApplyRoleFilter();
        }

        private void SelectRole(UISymbolButton btn)
        {
            if (_selectedRoleBtn != null)
            {
                _selectedRoleBtn.FillColor = Color.FromArgb(19, 35, 58);
                _selectedRoleBtn.FillSelectedColor = Color.FromArgb(0, 255, 255);
                _selectedRoleBtn.ForeColor = Color.FromArgb(230, 238, 247);
            }
            btn.FillColor = Color.FromArgb(0, 255, 255);
            btn.FillSelectedColor = Color.FromArgb(0, 255, 255);
            btn.ForeColor = Color.FromArgb(11, 22, 34);
            _selectedRoleBtn = btn;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            try
            {
                _allUsers = bll.GetLoginUsers();
                if (_allUsers.Count == 0)
                {
                    UIMessageBox.ShowError("系统没有任何可用账号");
                    return;
                }
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError("加载人员失败：" + ex.Message);
                return;
            }

            // 默认选中管理员
            SelectRole(btnAdmin);
            ApplyRoleFilter();
            txtPassword.Focus();
        }

        private void ApplyRoleFilter()
        {
            string role = GetSelectRole();
            cmbUser.DataSource = role == null
                ? _allUsers
                : _allUsers.FindAll(u => u.Role == role);
            cmbUser.DisplayMember = "DisplayName";
            cmbUser.ValueMember = "UserId";
            if (cmbUser.Items.Count > 0) cmbUser.SelectedIndex = 0;
        }

        private string GetSelectRole()
        {
            if (_selectedRoleBtn == btnAdmin) return UserRoleText.Admin;
            if (_selectedRoleBtn == btnDoctor) return UserRoleText.Doctor;
            if (_selectedRoleBtn == btnRepair) return UserRoleText.Repair;
            return null;
        }
    }
}
