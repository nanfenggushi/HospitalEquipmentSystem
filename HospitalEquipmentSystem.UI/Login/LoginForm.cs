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
        private bool _isLoading = true;   // 防止初始化时误触发事件

        public LoginForm()
        {
            InitializeComponent();
            cmbUser.SelectedIndexChanged += CmbUser_SelectedIndexChanged;
            this.Shown += (s, e) =>
            {
                //（隐藏和现实密码按钮 关键是该按钮的父类应该是form） 把按钮移到 Form 上，避免被 loginPanel 内控件覆盖
                int absX = loginPanel.Left + txtPassword.Left + txtPassword.Width - btnTogglePwd.Width - 8;
                int absY = loginPanel.Top + txtPassword.Top + (txtPassword.Height - btnTogglePwd.Height) / 2;
                btnTogglePwd.Location = new Point(absX, absY);
                btnTogglePwd.Parent = this;
                btnTogglePwd.FillColor = Color.FromArgb(19, 35, 58);
                btnTogglePwd.SymbolColor = Color.FromArgb(230, 238, 247);
                btnTogglePwd.BringToFront();
            };
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

            // 登录成功之后勾选记住密码逻辑
            if (chkRememberPwd.Checked)
            {
                Properties.Settings.Default.RememberPwd = true;
                Properties.Settings.Default.SavedUser = userId.ToString();
                Properties.Settings.Default.SavedPassword = txtPassword.Text;  // 保存明文
                Properties.Settings.Default.Save();
            }
            else
            {
                // 取消勾选时清除
                Properties.Settings.Default.RememberPwd = false;
                Properties.Settings.Default.SavedUser = "";
                Properties.Settings.Default.SavedPassword = "";
                Properties.Settings.Default.Save();
            }

            LoginUser.SetUser(user);
            this.DialogResult = DialogResult.OK;
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            SelectRole(btnAdmin);
            ApplyRoleFilter();
            ApplySavedCredentials();   // 切换角色后尝试恢复记住的密码
        }

        private void btnDoctor_Click(object sender, EventArgs e)
        {
            SelectRole(btnDoctor);
            ApplyRoleFilter();
            ApplySavedCredentials();
        }

        private void btnRepair_Click(object sender, EventArgs e)
        {
            SelectRole(btnRepair);
            ApplyRoleFilter();
            ApplySavedCredentials();
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
            // 读取记住密码设置
            ApplySavedCredentials();
            txtPassword.Focus();
            _isLoading = false;   // 初始化完成，允许 SelectedIndexChanged 生效
        }


        /// <summary>
        /// 如果勾了"记住密码"，
        /// 在 cmbUser 里找到保存的用户并选中它，填充密码
        /// </summary>
        private void ApplySavedCredentials()
        {
            if (!Properties.Settings.Default.RememberPwd)
                return;   // 没勾记住密码 → 什么都不做

            string savedUser = Properties.Settings.Default.SavedUser;
            string savedPwd = Properties.Settings.Default.SavedPassword;

            if (string.IsNullOrEmpty(savedUser))
                return;   // 没保存过用户 → 什么都不做

            // ① 在 cmbUser 里找到保存的用户
            UserDto foundUser = null;
            for (int i = 0; i < _allUsers.Count; i++)
            {
                var user = _allUsers[i];
                if (user != null && user.UserId.ToString() == savedUser)
                {
                    foundUser = user;
                    break;
                }
            }

            if (foundUser == null)
                return;   // 当前角色下没有这个用户 → 不处理

            // ② 如果这个用户在别的角色里，自动切换角色按钮
            if (foundUser.Role == "Admin")
                SelectRole(btnAdmin);
            else if (foundUser.Role == "Doctor")
                SelectRole(btnDoctor);
            else if (foundUser.Role == "Repair")
                SelectRole(btnRepair);

            ApplyRoleFilter();   // 刷新下拉框

            // ③ ApplyRoleFilter 可能重置了选中项，重新选中保存用户
            for (int i = 0; i < cmbUser.Items.Count; i++)
            {
                var u = cmbUser.Items[i] as UserDto;
                if (u != null && u.UserId.ToString() == savedUser)
                {
                    cmbUser.SelectedIndex = i;
                    break;
                }
            }

            // ④ 填充密码
            txtPassword.Text = savedPwd ?? "";
            chkRememberPwd.Checked = true;
        }
        /// <summary>
        /// 下拉框只显示对应角色的用户
        /// </summary>
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

        /// <summary>
        /// 用户切换下拉项时：是保存的用户就填密码，否则清空
        /// </summary>
        private void CmbUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoading) return;   // 初始化阶段不处理

            if (cmbUser.SelectedItem is UserDto user)
            {

                // 切到上次记住的用户 → 填充密码
                if (chkRememberPwd.Checked
                    && user.UserId.ToString() == Properties.Settings.Default.SavedUser)
                {
                    txtPassword.Text = Properties.Settings.Default.SavedPassword;
                }
                else
                {
                    // 其他用户 → 清空密码
                    txtPassword.Clear();
                }
            }
        }

        private void btnTogglePwd_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '*')
            {
                txtPassword.PasswordChar = '\0';   // 显示明文
                btnTogglePwd.Symbol = 61550;        // 睁眼图标
            }
            else
            {
                txtPassword.PasswordChar = '*';    // 隐藏
                btnTogglePwd.Symbol = 61552;        // 闭眼图标
            }
        }
    }
}
