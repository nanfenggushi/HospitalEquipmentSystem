using Sunny.UI;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using HospitalEquipment.DAL;

namespace HospitalEquipmentSystem.UI.register
{
    public partial class RegisterForm : UIForm
    {
        // 当前验证码文本
        private string currentCaptcha = "";
        // 登录DAL
        private LoginDAL loginDAL = new LoginDAL();

        /// <summary>
        /// 注册成功的新账号Id（供登录页定位使用）
        /// </summary>
        public int CreatedUserId { get; private set; }

        public RegisterForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;   // 减少界面重绘闪烁
          
        }

        private async void RegisterForm_Load(object sender, EventArgs e)
        {
            await LoadComboBoxDataAsync();
            // 生成初始验证码
            GenerateCaptcha();
            // 绑定用户名失去焦点事件
            ui_txtrealname.Leave += Ui_txtusername_Leave;
        }

        private async Task LoadComboBoxDataAsync()
        {
            try
            {
                var data = await Task.Run(() => new
                {
                    Roles = loginDAL.GetExistingRoles(),
                    Departments = loginDAL.GetActiveDepartments()
                });
                if (IsDisposed) return;
                BindRoleComboBox(data.Roles);
                BindDepartmentComboBox(data.Departments);
            }
            catch
            {
            }
        }

        /// <summary>
        /// 从数据库读取已使用过的角色，绑定到下拉框
        /// ValueMember = Role（英文代码，存库使用）
        /// DisplayMember = RoleName（中文名，展示用）
        /// </summary>
        private void BindRoleComboBox()
        {
            BindRoleComboBox(loginDAL.GetExistingRoles());
        }

        private void BindRoleComboBox(DataTable dt)
        {
            // 数据库没有角色记录时，兜底使用默认三种
            if (dt.Rows.Count == 0)
            {
                dt = new DataTable();
                dt.Columns.Add("Role", typeof(string));
                dt.Columns.Add("RoleName", typeof(string));
                dt.Rows.Add("admin", "系统管理员");
                dt.Rows.Add("doctor", "医生");
                dt.Rows.Add("repair", "维修人员");
            }
            uiComboBox1.DataSource = dt;
            uiComboBox1.ValueMember = "Role";
            uiComboBox1.DisplayMember = "RoleName";
            uiComboBox1.SelectedIndex = 0;
        }

        /// <summary>
        /// 从数据库读取所有启用中的科室，绑定到下拉框
        /// ValueMember = DeptId（存库使用）
        /// DisplayMember = DeptName（展示用）
        /// </summary>
        private void BindDepartmentComboBox()
        {
            BindDepartmentComboBox(loginDAL.GetActiveDepartments());
        }

        private void BindDepartmentComboBox(DataTable dt)
        {
            uiComboBox_dept.DataSource = dt;
            uiComboBox_dept.ValueMember = "DeptId";
            uiComboBox_dept.DisplayMember = "DeptName";
            uiComboBox_dept.SelectedIndex = dt.Rows.Count > 0 ? 0 : -1;
        }



        /// <summary>
        /// 用户名失去焦点时，实时检测是否已存在
        /// </summary>
        private void Ui_txtusername_Leave(object sender, EventArgs e)
        {
            string username = ui_txtrealname.Text.Trim();
            if (!string.IsNullOrEmpty(username))
            {
                if (loginDAL.IsUsernameExists(username))
                {
                    uiLabel7.Text = "用户名已被注册";
                }
                else
                {
                    uiLabel7.Text = "";
                }
            }
        }

        /// <summary>
        /// 生成验证码图片
        /// </summary>
        private void GenerateCaptcha()
        {
            // 生成随机验证码字符串（4位数字+字母）
            string chars = "0123456789ABCDEFGHJKLMNPQRSTUVWXYZ";
            Random random = new Random();
            char[] captchaChars = new char[4];
            for (int i = 0; i < 4; i++)
            {
                captchaChars[i] = chars[random.Next(chars.Length)];
            }
            currentCaptcha = new string(captchaChars);

            // 创建图片
            int width = 110;
            int height = 50;
            Bitmap bitmap = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bitmap);

            try
            {
                // 背景渐变
                LinearGradientBrush brush = new LinearGradientBrush(
                    new Rectangle(0, 0, width, height),
                    Color.White,
                    Color.LightGray,
                    LinearGradientMode.Horizontal);
                g.FillRectangle(brush, 0, 0, width, height);

                // 画干扰线
                for (int i = 0; i < 8; i++)
                {
                    int x1 = random.Next(width);
                    int y1 = random.Next(height);
                    int x2 = random.Next(width);
                    int y2 = random.Next(height);
                    Color lineColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                    g.DrawLine(new Pen(lineColor), x1, y1, x2, y2);
                }

                // 画验证码文字
                Font font = new Font("Arial", 18, FontStyle.Bold | FontStyle.Italic);
                for (int i = 0; i < currentCaptcha.Length; i++)
                {
                    // 每个字符颜色随机
                    Color textColor = Color.FromArgb(
                        random.Next(100, 200),
                        random.Next(0, 100),
                        random.Next(0, 100));
                    Brush textBrush = new SolidBrush(textColor);

                    // 字符位置略有偏移
                    float x = 10 + i * 24;
                    float y = random.Next(3, 10);

                    // 字符旋转角度
                    float angle = random.Next(-15, 15);

                    g.TranslateTransform(x, y);
                    g.RotateTransform(angle);
                    g.DrawString(currentCaptcha[i].ToString(), font, textBrush, 0, 0);
                    g.ResetTransform();
                }

                // 画干扰点
                for (int i = 0; i < 40; i++)
                {
                    int x = random.Next(width);
                    int y = random.Next(height);
                    Color dotColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                    bitmap.SetPixel(x, y, dotColor);
                }

                // 显示到PictureBox
                pictureBoxCaptcha.Image = bitmap;
            }
            catch
            {
                g.Dispose();
                bitmap.Dispose();
                throw;
            }
        }

        /// <summary>
        /// 点击验证码图片刷新
        /// </summary>
        private void pictureBoxCaptcha_Click(object sender, EventArgs e)
        {
            GenerateCaptcha();
            // 清空验证码输入框
            ui_txtverma.Text = "";
            uiLabel10.Text = "";
        }

        /// <summary>
        /// 清空所有错误提示
        /// </summary>
        private void ClearErrorLabels()
        {
            uiLabel_role_err.Text = ""; // 用户身份错误
            uiLabel7.Text = "";          // 用户名错误
            uiLabel8.Text = "";          // 密码错误
            uiLabel9.Text = "";          // 验证密码错误
            uiLabel10.Text = "";         // 验证码错误
            uiLabel11.Text = "";         // 真实姓名错误
            uiLabel12.Text = "";         // 手机号错误
            uiLabel_dept_err.Text = "";  // 科室错误
        }

        /// <summary>
        /// 验证手机号格式
        /// </summary>
        private bool IsValidPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone)) return false;
            if (phone.Length != 11) return false;
            // 简单验证：1开头，第二位3-9，后面9位数字
            return System.Text.RegularExpressions.Regex.IsMatch(phone, @"^1[3-9]\d{9}$");
        }

        /// <summary>
        /// 注册按钮点击事件
        /// </summary>
        private void uiButton1_Click(object sender, EventArgs e)
        {
            // 先清空所有错误提示
            ClearErrorLabels();

            string realName = ui_txtname.Text.Trim();
            string username = ui_txtrealname.Text.Trim();
            string password = ui_txtpwd.Text;
            string verifyPassword = ui_txtverpwd.Text;
            string phone = ui_txtphone.Text.Trim();
            string captchaInput = ui_txtverma.Text.Trim();

            string role = uiComboBox1.SelectedValue?.ToString();
            string deptValue = uiComboBox_dept.SelectedValue?.ToString();
            string title = uiComboBox1.Text.ToString();
            bool hasError = false;

            // 1. 验证真实姓名
            if (string.IsNullOrEmpty(realName))
            {
                uiLabel11.Text = "请输入真实姓名";
                hasError = true;
            }

            // 2. 验证用户名
            if (string.IsNullOrEmpty(username))
            {
                uiLabel7.Text = "请输入用户名";
                hasError = true;
            }
            else if (loginDAL.IsUsernameExists(username))
            {
                uiLabel7.Text = "用户名已被注册";
                hasError = true;
            }

            // 3. 验证密码
            if (string.IsNullOrEmpty(password))
            {
                uiLabel8.Text = "请输入密码";
                hasError = true;
            }
            else if (password.Length < 6)
            {
                uiLabel8.Text = "密码至少6位";
                hasError = true;
            }

            // 4. 验证两次密码是否一致
            if (password != verifyPassword)
            {
                uiLabel9.Text = "两次密码不一致";
                hasError = true;
            }

            // 5. 验证手机号
            if (string.IsNullOrEmpty(phone))
            {
                uiLabel12.Text = "请输入手机号";
                hasError = true;
            }
            else if (!IsValidPhone(phone))
            {
                uiLabel12.Text = "手机号格式不正确";
                hasError = true;
            }

            // 6. 验证科室已选择
            if (string.IsNullOrEmpty(deptValue))
            {
                uiLabel_dept_err.Text = "请选择科室";
                hasError = true;
            }

            // 7. 验证身份已选择
            if (string.IsNullOrEmpty(role))
            {
                uiLabel_role_err.Text = "请选择用户身份";
                hasError = true;
            }

            // 8. 验证验证码
            if (string.IsNullOrEmpty(captchaInput))
            {
                uiLabel10.Text = "请输入验证码";
                hasError = true;
            }
            else if (captchaInput.ToUpper() != currentCaptcha.ToUpper())
            {
                uiLabel10.Text = "验证码错误";
                hasError = true;
                // 验证码错误时刷新验证码
                GenerateCaptcha();
                ui_txtverma.Text = "";
            }

            // 如果有错误，不继续
            if (hasError)
            {
                return;
            }

            // 9. 所有验证通过，执行注册
            try
            {
                int deptId = Convert.ToInt32(deptValue);

                // 调用DAL注册（密码与登录校验方式保持一致，Title 存身份显示名称）
                int userId = loginDAL.RegisterUserFull(
                    username, password, realName, role, deptId, phone, title);

                if (userId > 0)
                {
                    MessageBox.Show("注册成功！请使用新账号登录。", "注册成功",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // 注册成功后清空表单
                    ClearForm();
                    // 记录新账号，关闭注册页，返回登录页自动定位到新账号
                    CreatedUserId = userId;
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("注册失败，请重试！", "错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("注册出错：" + ex.Message, "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 清空注册表单
        /// </summary>
        private void ClearForm()
        {
            BindRoleComboBox();
            BindDepartmentComboBox();
            ui_txtrealname.Text = "";
            ui_txtname.Text = "";
            ui_txtpwd.Text = "";
            ui_txtverpwd.Text = "";
            ui_txtphone.Text = "";
            ui_txtverma.Text = "";
            if (uiComboBox1.Items.Count > 0) uiComboBox1.SelectedIndex = 0;
            if (uiComboBox_dept.Items.Count > 0) uiComboBox_dept.SelectedIndex = 0;
            GenerateCaptcha();
            ClearErrorLabels();
            ui_txtname.Focus();
        }

      

        private void uiSymbolButton3_Click(object sender, EventArgs e)
        {
            // 关闭注册页并返回登录页（登录页在对话框关闭后会重新显示）
            this.Close();
        }


    } 
}
