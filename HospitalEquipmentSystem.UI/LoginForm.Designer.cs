namespace HospitalEquipmentSystem.UI
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.loginPanel = new Sunny.UI.UIPanel();
            this.lblrole = new Sunny.UI.UILabel();
            this.rdbRepair = new Sunny.UI.UIRadioButton();
            this.rdbDoctor = new Sunny.UI.UIRadioButton();
            this.rdbAdmin = new Sunny.UI.UIRadioButton();
            this.btnLogin = new Sunny.UI.UISymbolButton();
            this.txtPassword = new Sunny.UI.UITextBox();
            this.cmbUser = new Sunny.UI.UIComboBox();
            this.lblPwd = new Sunny.UI.UILabel();
            this.lblUser = new Sunny.UI.UILabel();
            this.lblTitle = new Sunny.UI.UILabel();
            this.loginPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // loginPanel
            // 
            this.loginPanel.Controls.Add(this.lblrole);
            this.loginPanel.Controls.Add(this.rdbRepair);
            this.loginPanel.Controls.Add(this.rdbDoctor);
            this.loginPanel.Controls.Add(this.rdbAdmin);
            this.loginPanel.Controls.Add(this.btnLogin);
            this.loginPanel.Controls.Add(this.txtPassword);
            this.loginPanel.Controls.Add(this.cmbUser);
            this.loginPanel.Controls.Add(this.lblPwd);
            this.loginPanel.Controls.Add(this.lblUser);
            this.loginPanel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.loginPanel.Location = new System.Drawing.Point(86, 101);
            this.loginPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.loginPanel.MinimumSize = new System.Drawing.Size(1, 1);
            this.loginPanel.Name = "loginPanel";
            this.loginPanel.Radius = 20;
            this.loginPanel.RectColor = System.Drawing.Color.DimGray;
            this.loginPanel.Size = new System.Drawing.Size(559, 284);
            this.loginPanel.TabIndex = 0;
            this.loginPanel.Text = null;
            this.loginPanel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblrole
            // 
            this.lblrole.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblrole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblrole.Location = new System.Drawing.Point(31, 183);
            this.lblrole.Name = "lblrole";
            this.lblrole.Size = new System.Drawing.Size(93, 23);
            this.lblrole.TabIndex = 8;
            this.lblrole.Text = "职务:";
            this.lblrole.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rdbRepair
            // 
            this.rdbRepair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdbRepair.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdbRepair.Location = new System.Drawing.Point(379, 177);
            this.rdbRepair.MinimumSize = new System.Drawing.Size(1, 1);
            this.rdbRepair.Name = "rdbRepair";
            this.rdbRepair.Size = new System.Drawing.Size(126, 29);
            this.rdbRepair.TabIndex = 7;
            this.rdbRepair.Text = "维修人员";
            this.rdbRepair.CheckedChanged += new System.EventHandler(this.rdbRepair_CheckedChanged);
            // 
            // rdbDoctor
            // 
            this.rdbDoctor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdbDoctor.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdbDoctor.Location = new System.Drawing.Point(247, 177);
            this.rdbDoctor.MinimumSize = new System.Drawing.Size(1, 1);
            this.rdbDoctor.Name = "rdbDoctor";
            this.rdbDoctor.Size = new System.Drawing.Size(120, 29);
            this.rdbDoctor.TabIndex = 6;
            this.rdbDoctor.Text = "医护人员";
            this.rdbDoctor.CheckedChanged += new System.EventHandler(this.rdbDoctor_CheckedChanged);
            // 
            // rdbAdmin
            // 
            this.rdbAdmin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdbAdmin.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdbAdmin.Location = new System.Drawing.Point(130, 177);
            this.rdbAdmin.MinimumSize = new System.Drawing.Size(1, 1);
            this.rdbAdmin.Name = "rdbAdmin";
            this.rdbAdmin.Size = new System.Drawing.Size(93, 29);
            this.rdbAdmin.TabIndex = 5;
            this.rdbAdmin.Text = "管理员";
            this.rdbAdmin.CheckedChanged += new System.EventHandler(this.rdbAdmin_CheckedChanged);
            // 
            // btnLogin
            // 
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.FillHoverColor = System.Drawing.Color.Green;
            this.btnLogin.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLogin.Location = new System.Drawing.Point(210, 232);
            this.btnLogin.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(111, 35);
            this.btnLogin.Symbol = 61447;
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "登　录";
            this.btnLogin.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPassword.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPassword.Location = new System.Drawing.Point(199, 112);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPassword.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Padding = new System.Windows.Forms.Padding(5);
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.ShowText = false;
            this.txtPassword.Size = new System.Drawing.Size(257, 29);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtPassword.Watermark = "";
            // 
            // cmbUser
            // 
            this.cmbUser.DataSource = null;
            this.cmbUser.FillColor = System.Drawing.Color.White;
            this.cmbUser.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbUser.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.cmbUser.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cmbUser.Location = new System.Drawing.Point(199, 37);
            this.cmbUser.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbUser.MinimumSize = new System.Drawing.Size(63, 0);
            this.cmbUser.Name = "cmbUser";
            this.cmbUser.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cmbUser.Size = new System.Drawing.Size(257, 29);
            this.cmbUser.SymbolSize = 24;
            this.cmbUser.TabIndex = 2;
            this.cmbUser.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmbUser.Watermark = "";
            // 
            // lblPwd
            // 
            this.lblPwd.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPwd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblPwd.Location = new System.Drawing.Point(40, 118);
            this.lblPwd.Name = "lblPwd";
            this.lblPwd.Size = new System.Drawing.Size(100, 23);
            this.lblPwd.TabIndex = 1;
            this.lblPwd.Text = "密　码:";
            this.lblPwd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUser
            // 
            this.lblUser.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblUser.Location = new System.Drawing.Point(40, 43);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(100, 23);
            this.lblUser.TabIndex = 0;
            this.lblUser.Text = "选择人员:";
            this.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("楷体", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblTitle.Location = new System.Drawing.Point(141, 36);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(435, 44);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "医院设备借用管理系统";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LoginForm
            // 
            this.AcceptButton = this.btnLogin;
            this.AllowShowTitle = false;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(740, 474);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.loginPanel);
            this.Name = "LoginForm";
            this.Padding = new System.Windows.Forms.Padding(0);
            this.ShowTitle = false;
            this.Text = "LoginForm";
            this.ZoomScaleRect = new System.Drawing.Rectangle(19, 19, 800, 450);
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.loginPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIPanel loginPanel;
        private Sunny.UI.UILabel lblTitle;
        private Sunny.UI.UILabel lblUser;
        private Sunny.UI.UILabel lblPwd;
        private Sunny.UI.UISymbolButton btnLogin;
        private Sunny.UI.UITextBox txtPassword;
        private Sunny.UI.UIComboBox cmbUser;
        private Sunny.UI.UILabel lblrole;
        private Sunny.UI.UIRadioButton rdbRepair;
        private Sunny.UI.UIRadioButton rdbDoctor;
        private Sunny.UI.UIRadioButton rdbAdmin;
    }
}