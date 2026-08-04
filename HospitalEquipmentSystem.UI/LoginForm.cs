using HospitalEquipment.BLL;
using HospitalEquipment.Model;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalEquipmentSystem.UI
{
    public partial class LoginForm : UIForm
    {
        private readonly LoginBLL bll = new LoginBLL();
        private List<UserDto> _allUsers = new List<UserDto>();
        public LoginForm()
        {
            InitializeComponent();

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (cmbUser.SelectedValue==null)
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

        private void rdbAdmin_CheckedChanged(object sender, EventArgs e)
        {
            ApplyRoleFilter();
        }

        private void rdbDoctor_CheckedChanged(object sender, EventArgs e)
        {
            ApplyRoleFilter();
        }

        private void rdbRepair_CheckedChanged(object sender, EventArgs e)
        {
            ApplyRoleFilter();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            try
            {
                _allUsers = bll.GetLoginUsers();
                if (_allUsers.Count==0)
                {
                    UIMessageBox.ShowError("系统没有任何可用账号");
                    return;
                }
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError("加载人员失败："+ex.Message);
                return;
            }
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
            if(cmbUser.Items.Count>0) cmbUser.SelectedIndex = 0;
        }

        private string GetSelectRole()
        {
            if(rdbAdmin.Checked) return UserRoleText.Admin;
            if(rdbDoctor.Checked) return UserRoleText.Doctor;
            if(rdbRepair.Checked) return UserRoleText.Repair;
            return null;
        }
    }
}
