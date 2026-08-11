using HospitalEquipment.BLL;
using HospitalEquipment.Model;
using HospitalEquipmentSystem.UI.Login;
using HospitalEquipmentSystem.UI.register;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalEquipmentSystem.UI
{
    public partial class LoginForm : UIForm
    {
        private readonly LoginBLL bll = new LoginBLL();
        private List<UserDto> _allUsers = new List<UserDto>();
        private UISymbolButton _selectedRoleBtn;
        private bool _isLoading = true;   // 防止初始化时误触发事件
        private int _loadVersion;         // 人员数据加载版本号，防止过期数据覆盖新数据

        public LoginForm()
        {
            InitializeComponent();
            // 按需求不改 Designer 布局，人脸按钮事件在代码里挂载。
            this.uiSymbolButton5.Click += new EventHandler(BtnFaceRecognition_Click);
            this.DoubleBuffered = true;   // 减少界面重绘闪烁
            ResizeBackground();           // 大图背景一次性缩放，避免每次重绘都做高开销缩放
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

        /// <summary>打开人脸识别窗体；识别成功后直接沿用登录成功流程。</summary>
        private void BtnFaceRecognition_Click(object sender, EventArgs e)
        {
            try
            {
                using (faceRecognition faceForm = new faceRecognition())
                {
                    faceForm.StartPosition = FormStartPosition.CenterScreen;
                    if (faceForm.ShowDialog(this) == DialogResult.OK)
                    {
                        this.DialogResult = DialogResult.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError("打开人脸识别失败：" + ex.Message);
            }
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
            // 异步加载人员列表，界面先显示出来，不阻塞
            LoadUsersAsync();
        }

        /// <summary>
        /// 异步加载用户列表，避免加载人员时界面卡住
        /// </summary>
        private void LoadUsersAsync()
        {
            int version = ++_loadVersion;
            var task = Task.Run(() => bll.GetLoginUsers());
            task.ContinueWith(t =>
            {
                if (IsDisposed) return;
                bool failed = t.Exception != null;
                List<UserDto> result = failed ? null : t.Result;
                try
                {
                    BeginInvoke((Action)(() =>
                    {
                        if (IsDisposed || version != _loadVersion) return;   // 已有更新数据，丢弃过期结果
                        if (failed)
                        {
                            UIMessageBox.ShowError("加载人员失败：" + t.Exception.GetBaseException().Message);
                            return;
                        }

                        _allUsers = result;
                        if (_allUsers.Count == 0)
                        {
                            UIMessageBox.ShowError("系统没有任何可用账号");
                            return;
                        }

                        // 默认选中管理员
                        SelectRole(btnAdmin);
                        ApplyRoleFilter();
                        // 读取记住密码设置
                        ApplySavedCredentials();
                        txtPassword.Focus();
                        _isLoading = false;   // 初始化完成，允许 SelectedIndexChanged 生效
                    }));
                }
                catch
                {
                    // 窗口已关闭，忽略
                }
            });
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

        private void uibtn_register_Click(object sender, EventArgs e)
        {
            this.Hide();   // 关闭（隐藏）当前登录页，进入注册页
            int createdUserId = 0;
            using (var registerForm = new RegisterForm())
            {
                if (registerForm.ShowDialog() == DialogResult.OK)
                    createdUserId = registerForm.CreatedUserId;
            }

            this.Show();             // 注册页关闭后，跳回登录页
            this.BringToFront();
            RefreshUsers(createdUserId);   // 刷新人员列表，新注册的账号立即可登录
        }

        /// <summary>
        /// 从注册页返回后刷新人员列表；若刚注册成功，自动切换角色并定位到新账号
        /// </summary>
        private void RefreshUsers(int focusUserId)
        {
            try
            {
                _loadVersion++;            // 取消尚未完成的旧加载任务
                _allUsers = bll.GetLoginUsers();
                if (_allUsers.Count == 0) return;

                UserDto focus = null;
                for (int i = 0; i < _allUsers.Count; i++)
                {
                    if (_allUsers[i] != null && _allUsers[i].UserId == focusUserId)
                    {
                        focus = _allUsers[i];
                        break;
                    }
                }

                if (focus != null)
                {
                    if (focus.Role == UserRoleText.Admin) SelectRole(btnAdmin);
                    else if (focus.Role == UserRoleText.Doctor) SelectRole(btnDoctor);
                    else if (focus.Role == UserRoleText.Repair) SelectRole(btnRepair);
                }

                ApplyRoleFilter();

                if (focus != null)
                {
                    for (int i = 0; i < cmbUser.Items.Count; i++)
                    {
                        var u = cmbUser.Items[i] as UserDto;
                        if (u != null && u.UserId == focusUserId)
                        {
                            cmbUser.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError("刷新人员失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 背景源图是几千像素的大图，窗体每次重绘 Stretch 缩放开销极高，
        /// 这里一次性缩放到窗体尺寸，之后重绘只是近乎等尺寸的快速复制。
        /// </summary>
        private void ResizeBackground()
        {
            Image src = BackgroundImage;
            if (src == null) return;

            // 目标尺寸：窗体客户区；若窗体可能最大化，则取屏幕尺寸（不超过原图）
            int w = Math.Min(src.Width, Math.Max(ClientSize.Width, Screen.PrimaryScreen.Bounds.Width));
            int h = Math.Min(src.Height, Math.Max(ClientSize.Height, Screen.PrimaryScreen.Bounds.Height));
            if (w <= 0 || h <= 0) return;
            if (w == src.Width && h == src.Height) return;   // 本来就够小，无需处理

            BackgroundImage = new Bitmap(src, w, h);
        }
    }
}
