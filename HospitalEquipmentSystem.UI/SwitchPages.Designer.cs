namespace HospitalEquipmentSystem.UI
{
    partial class SwitchPages
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
            this.components = new System.ComponentModel.Container();
            this.topPanel = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnUserName = new Sunny.UI.UIButton();
            this.btnNotify = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnMenu = new System.Windows.Forms.Button();
            this.sidePanel = new System.Windows.Forms.Panel();
            this.btnSystemSetting = new System.Windows.Forms.Button();
            this.btnDataStatistics = new System.Windows.Forms.Button();
            this.btnMonitoringCenter = new System.Windows.Forms.Button();
            this.btnBorrowManage = new System.Windows.Forms.Button();
            this.btnRepairManage = new System.Windows.Forms.Button();
            this.btnDeviceManage = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemSwitchUser = new System.Windows.Forms.ToolStripMenuItem();
            this.topPanel.SuspendLayout();
            this.sidePanel.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.White;
            this.topPanel.Controls.Add(this.btnExit);
            this.topPanel.Controls.Add(this.btnUserName);
            this.topPanel.Controls.Add(this.btnNotify);
            this.topPanel.Controls.Add(this.lblTitle);
            this.topPanel.Controls.Add(this.btnMenu);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(1588, 60);
            this.topPanel.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnExit.Location = new System.Drawing.Point(1130, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(94, 60);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "⏻";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnUserName
            // 
            this.btnUserName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUserName.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnUserName.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.btnUserName.Location = new System.Drawing.Point(1224, 0);
            this.btnUserName.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnUserName.Name = "btnUserName";
            this.btnUserName.Size = new System.Drawing.Size(314, 60);
            this.btnUserName.TabIndex = 3;
            this.btnUserName.Text = "管理员";
            this.btnUserName.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUserName.Click += new System.EventHandler(this.btnUserName_Click);
            // 
            // btnNotify
            // 
            this.btnNotify.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNotify.FlatAppearance.BorderSize = 0;
            this.btnNotify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNotify.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnNotify.Location = new System.Drawing.Point(1538, 0);
            this.btnNotify.Name = "btnNotify";
            this.btnNotify.Size = new System.Drawing.Size(50, 60);
            this.btnNotify.TabIndex = 2;
            this.btnNotify.Text = "🔔";
            this.btnNotify.UseVisualStyleBackColor = false;
            this.btnNotify.Click += new System.EventHandler(this.btnNotify_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblTitle.Location = new System.Drawing.Point(71, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(110, 31);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "设备管理";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnMenu
            // 
            this.btnMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnMenu.FlatAppearance.BorderSize = 0;
            this.btnMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenu.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.btnMenu.Location = new System.Drawing.Point(0, 0);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(50, 60);
            this.btnMenu.TabIndex = 0;
            this.btnMenu.Text = "☰";
            this.btnMenu.UseVisualStyleBackColor = false;
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
            // 
            // sidePanel
            // 
            this.sidePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(39)))), ((int)(((byte)(54)))));
            this.sidePanel.Controls.Add(this.btnSystemSetting);
            this.sidePanel.Controls.Add(this.btnDataStatistics);
            this.sidePanel.Controls.Add(this.btnMonitoringCenter);
            this.sidePanel.Controls.Add(this.btnBorrowManage);
            this.sidePanel.Controls.Add(this.btnRepairManage);
            this.sidePanel.Controls.Add(this.btnDeviceManage);
            this.sidePanel.Controls.Add(this.btnDashboard);
            this.sidePanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidePanel.Location = new System.Drawing.Point(0, 60);
            this.sidePanel.Name = "sidePanel";
            this.sidePanel.Size = new System.Drawing.Size(200, 1042);
            this.sidePanel.TabIndex = 1;
            // 
            // btnSystemSetting
            // 
            this.btnSystemSetting.BackColor = System.Drawing.Color.Transparent;
            this.btnSystemSetting.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSystemSetting.FlatAppearance.BorderSize = 0;
            this.btnSystemSetting.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this.btnSystemSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSystemSetting.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnSystemSetting.ForeColor = System.Drawing.Color.White;
            this.btnSystemSetting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSystemSetting.Location = new System.Drawing.Point(0, 300);
            this.btnSystemSetting.Name = "btnSystemSetting";
            this.btnSystemSetting.Size = new System.Drawing.Size(200, 50);
            this.btnSystemSetting.TabIndex = 6;
            this.btnSystemSetting.Text = "  ⚙  系统设置";
            this.btnSystemSetting.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSystemSetting.UseVisualStyleBackColor = false;
            this.btnSystemSetting.Click += new System.EventHandler(this.btnSystemSetting_Click);
            // 
            // btnDataStatistics
            // 
            this.btnDataStatistics.BackColor = System.Drawing.Color.Transparent;
            this.btnDataStatistics.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDataStatistics.FlatAppearance.BorderSize = 0;
            this.btnDataStatistics.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this.btnDataStatistics.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDataStatistics.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnDataStatistics.ForeColor = System.Drawing.Color.White;
            this.btnDataStatistics.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDataStatistics.Location = new System.Drawing.Point(0, 250);
            this.btnDataStatistics.Name = "btnDataStatistics";
            this.btnDataStatistics.Size = new System.Drawing.Size(200, 50);
            this.btnDataStatistics.TabIndex = 5;
            this.btnDataStatistics.Text = "  📊  数据统计";
            this.btnDataStatistics.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDataStatistics.UseVisualStyleBackColor = false;
            this.btnDataStatistics.Click += new System.EventHandler(this.btnDataStatistics_Click);
            // 
            // btnMonitoringCenter
            // 
            this.btnMonitoringCenter.BackColor = System.Drawing.Color.Transparent;
            this.btnMonitoringCenter.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMonitoringCenter.FlatAppearance.BorderSize = 0;
            this.btnMonitoringCenter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this.btnMonitoringCenter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMonitoringCenter.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnMonitoringCenter.ForeColor = System.Drawing.Color.White;
            this.btnMonitoringCenter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMonitoringCenter.Location = new System.Drawing.Point(0, 200);
            this.btnMonitoringCenter.Name = "btnMonitoringCenter";
            this.btnMonitoringCenter.Size = new System.Drawing.Size(200, 50);
            this.btnMonitoringCenter.TabIndex = 4;
            this.btnMonitoringCenter.Text = "  📈  监控中心";
            this.btnMonitoringCenter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMonitoringCenter.UseVisualStyleBackColor = false;
            this.btnMonitoringCenter.Click += new System.EventHandler(this.btnMonitoringCenter_Click);
            // 
            // btnBorrowManage
            // 
            this.btnBorrowManage.BackColor = System.Drawing.Color.Transparent;
            this.btnBorrowManage.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBorrowManage.FlatAppearance.BorderSize = 0;
            this.btnBorrowManage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this.btnBorrowManage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBorrowManage.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnBorrowManage.ForeColor = System.Drawing.Color.White;
            this.btnBorrowManage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBorrowManage.Location = new System.Drawing.Point(0, 150);
            this.btnBorrowManage.Name = "btnBorrowManage";
            this.btnBorrowManage.Size = new System.Drawing.Size(200, 50);
            this.btnBorrowManage.TabIndex = 3;
            this.btnBorrowManage.Text = "  ↕  借用管理";
            this.btnBorrowManage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBorrowManage.UseVisualStyleBackColor = false;
            this.btnBorrowManage.Click += new System.EventHandler(this.btnBorrowManage_Click);
            // 
            // btnRepairManage
            // 
            this.btnRepairManage.BackColor = System.Drawing.Color.Transparent;
            this.btnRepairManage.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRepairManage.FlatAppearance.BorderSize = 0;
            this.btnRepairManage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this.btnRepairManage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRepairManage.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnRepairManage.ForeColor = System.Drawing.Color.White;
            this.btnRepairManage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRepairManage.Location = new System.Drawing.Point(0, 100);
            this.btnRepairManage.Name = "btnRepairManage";
            this.btnRepairManage.Size = new System.Drawing.Size(200, 50);
            this.btnRepairManage.TabIndex = 2;
            this.btnRepairManage.Text = "  🔧  维修管理";
            this.btnRepairManage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRepairManage.UseVisualStyleBackColor = false;
            this.btnRepairManage.Click += new System.EventHandler(this.btnRepairManage_Click);
            // 
            // btnDeviceManage
            // 
            this.btnDeviceManage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this.btnDeviceManage.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDeviceManage.FlatAppearance.BorderSize = 0;
            this.btnDeviceManage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this.btnDeviceManage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeviceManage.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btnDeviceManage.ForeColor = System.Drawing.Color.White;
            this.btnDeviceManage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeviceManage.Location = new System.Drawing.Point(0, 50);
            this.btnDeviceManage.Name = "btnDeviceManage";
            this.btnDeviceManage.Size = new System.Drawing.Size(200, 50);
            this.btnDeviceManage.TabIndex = 1;
            this.btnDeviceManage.Text = "  📦  设备管理";
            this.btnDeviceManage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeviceManage.UseVisualStyleBackColor = false;
            this.btnDeviceManage.Click += new System.EventHandler(this.btnDeviceManage_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.BackColor = System.Drawing.Color.Transparent;
            this.btnDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(90)))));
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnDashboard.ForeColor = System.Drawing.Color.White;
            this.btnDashboard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.Location = new System.Drawing.Point(0, 0);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(200, 50);
            this.btnDashboard.TabIndex = 0;
            this.btnDashboard.Text = "  🏠  首页仪表盘";
            this.btnDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.UseVisualStyleBackColor = false;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // contentPanel
            // 
            this.contentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(39)))), ((int)(((byte)(54)))));
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(200, 60);
            this.contentPanel.Margin = new System.Windows.Forms.Padding(0);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(1388, 1042);
            this.contentPanel.TabIndex = 2;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSwitchUser});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(139, 28);
            // 
            // toolStripMenuItemSwitchUser
            // 
            this.toolStripMenuItemSwitchUser.Name = "toolStripMenuItemSwitchUser";
            this.toolStripMenuItemSwitchUser.Size = new System.Drawing.Size(138, 24);
            this.toolStripMenuItemSwitchUser.Text = "切换用户";
            this.toolStripMenuItemSwitchUser.Click += new System.EventHandler(this.toolStripMenuItemSwitchUser_Click);
            // 
            // SwitchPages
            // 
            this.AllowShowTitle = false;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1588, 1102);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.sidePanel);
            this.Controls.Add(this.topPanel);
            this.Name = "SwitchPages";
            this.Padding = new System.Windows.Forms.Padding(0);
            this.ShowTitle = false;
            this.Text = "SwitchPages";
            this.ZoomScaleRect = new System.Drawing.Rectangle(22, 22, 800, 450);
            this.Load += new System.EventHandler(this.SwitchPages_Load);
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.sidePanel.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Button btnMenu;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnNotify;
        private Sunny.UI.UIButton btnUserName;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSwitchUser;
        private System.Windows.Forms.Panel sidePanel;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnDeviceManage;
        private System.Windows.Forms.Button btnRepairManage;
        private System.Windows.Forms.Button btnBorrowManage;
        private System.Windows.Forms.Button btnMonitoringCenter;
        private System.Windows.Forms.Button btnDataStatistics;
        private System.Windows.Forms.Button btnSystemSetting;
        private System.Windows.Forms.Panel contentPanel;
    }
}
