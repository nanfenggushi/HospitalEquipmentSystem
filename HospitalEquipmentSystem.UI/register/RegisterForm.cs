using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        // 输入控件 -> 对应错误提示Label
        private readonly Dictionary<Control, UILabel> controlErrorLabels = new Dictionary<Control, UILabel>();

        /// <summary>
        /// 注册成功的新账号Id（供登录页定位使用）
        /// </summary>
        public int CreatedUserId { get; private set; }

        public RegisterForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;   // 减少界面重绘闪烁
            ResizeBackground();           // 大图背景一次性缩放，避免每次重绘都做高开销缩放
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            // 从数据库加载身份下拉框
            BindRoleComboBox();
            // 从数据库加载科室下拉框
            BindDepartmentComboBox();
            // 生成初始验证码
            GenerateCaptcha();
            // 为所有输入控件绑定焦点事件（Enter 清错误提示 / Leave 即时校验）
            WireFocusEvents();
        }

        /// <summary>
        /// 为所有输入控件绑定焦点事件：
        /// Enter（获得焦点）-> 清空该字段的错误提示
        /// Leave（失去焦点）-> 即时校验该字段并显示提示
        /// </summary>
        private void WireFocusEvents()
        {
            controlErrorLabels[ui_txtname] = uiLabel11;          // 真实姓名
            controlErrorLabels[ui_txtrealname] = uiLabel7;       // 用户名
            controlErrorLabels[ui_txtpwd] = uiLabel8;            // 密码
            controlErrorLabels[ui_txtverpwd] = uiLabel9;         // 确认密码
            controlErrorLabels[ui_txtphone] = uiLabel12;         // 手机号
            controlErrorLabels[ui_txtverma] = uiLabel10;         // 验证码
            controlErrorLabels[uiComboBox1] = uiLabel_role_err;  // 用户身份
            controlErrorLabels[uiComboBox_dept] = uiLabel_dept_err; // 科室

            foreach (Control c in controlErrorLabels.Keys)
            {
                c.Enter += Input_Enter;   // 获得焦点：清空该字段错误提示
                c.Leave += Input_Leave;   // 失去焦点：即时校验
            }
        }

        /// <summary>
        /// 控件获得焦点：清空对应的错误提示，方便用户重新编辑
        /// </summary>
        private void Input_Enter(object sender, EventArgs e)
        {
            if (sender is Control control && controlErrorLabels.TryGetValue(control, out UILabel lbl))
            {
                lbl.Text = "";
            }
        }

        /// <summary>
        /// 控件失去焦点：校验该字段，输入无误则清空错误提示，有误则显示
        /// </summary>
        private void Input_Leave(object sender, EventArgs e)
        {
            if (sender is Control control)
            {
                string error = ValidateField(control);
                if (controlErrorLabels.TryGetValue(control, out UILabel lbl))
                {
                    lbl.Text = error;
                }
            }
        }

        /// <summary>
        /// 校验单个字段，返回错误信息；无误返回空字符串
        /// </summary>
        private string ValidateField(Control control)
        {
            try
            {
                if (control == ui_txtname)
                {
                    if (string.IsNullOrEmpty(ui_txtname.Text.Trim())) return "请输入真实姓名";
                    return "";
                }
                if (control == ui_txtrealname)
                {
                    string username = ui_txtrealname.Text.Trim();
                    if (string.IsNullOrEmpty(username)) return "请输入用户名";
                    if (username.Length < 3) return "用户名至少3位";
                    if (loginDAL.IsUsernameExists(username)) return "用户名已被注册";
                    return "";
                }
                if (control == ui_txtpwd)
                {
                    if (string.IsNullOrEmpty(ui_txtpwd.Text)) return "请输入密码";
                    if (ui_txtpwd.Text.Length < 6) return "密码至少6位";
                    return "";
                }
                if (control == ui_txtverpwd)
                {
                    if (string.IsNullOrEmpty(ui_txtverpwd.Text)) return "请再次输入密码";
                    return ui_txtverpwd.Text != ui_txtpwd.Text ? "两次密码不一致" : "";
                }
                if (control == ui_txtphone)
                {
                    if (string.IsNullOrEmpty(ui_txtphone.Text.Trim())) return "请输入手机号";
                    return IsValidPhone(ui_txtphone.Text.Trim()) ? "" : "手机号格式不正确";
                }
                if (control == ui_txtverma)
                {
                    if (string.IsNullOrEmpty(ui_txtverma.Text.Trim())) return "请输入验证码";
                    return ui_txtverma.Text.Trim().ToUpper() == currentCaptcha.ToUpper() ? "" : "验证码错误";
                }
                if (control == uiComboBox1)
                    return string.IsNullOrEmpty(uiComboBox1.SelectedValue?.ToString()) ? "请选择用户身份" : "";
                if (control == uiComboBox_dept)
                    return string.IsNullOrEmpty(uiComboBox_dept.SelectedValue?.ToString()) ? "请选择科室" : "";
            }
            catch
            {
                // 数据库查询失败等异常不阻塞失焦校验，保持静默
            }
            return "";
        }

        /// <summary>
        /// 从数据库读取已使用过的角色，绑定到下拉框
        /// ValueMember = Role（英文代码，存库使用）
        /// DisplayMember = RoleName（中文名，展示用）
        /// </summary>
        private void BindRoleComboBox()
        {
            DataTable dt = loginDAL.GetExistingRoles();
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
            DataTable dt = loginDAL.GetActiveDepartments();
            uiComboBox_dept.DataSource = dt;
            uiComboBox_dept.ValueMember = "DeptId";
            uiComboBox_dept.DisplayMember = "DeptName";
            uiComboBox_dept.SelectedIndex = dt.Rows.Count > 0 ? 0 : -1;
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
            else if (username.Length < 3)
            {
                uiLabel7.Text = "用户名至少3位";
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

        private void uiSymbolButton1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void uiSymbolButton2_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
                this.WindowState = FormWindowState.Normal;
            else
                this.WindowState = FormWindowState.Maximized;
        }

        private void uiSymbolButton3_Click(object sender, EventArgs e)
        {
            // 关闭注册页并返回登录页（登录页在对话框关闭后会重新显示）
            this.Close();
        }

        /// <summary>
        /// 背景源图是 2847x1498 的大图，窗体每次重绘 Stretch 缩放开销极高，
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
