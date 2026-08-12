namespace HospitalEquipmentSystem.UI
{
    partial class DoctorWorkbench
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new Sunny.UI.UIPanel();
            this.lblHeaderTitle = new Sunny.UI.UILabel();
            this.lblHeaderUser = new Sunny.UI.UILabel();
            this.btnRefresh = new Sunny.UI.UISymbolButton();
            this.btnNewRepair = new Sunny.UI.UISymbolButton();
            this.pnlKpiContainer = new System.Windows.Forms.Panel();
            this.pnlKpi1 = new Sunny.UI.UIPanel();
            this.lblKpi1Title = new Sunny.UI.UILabel();
            this.lblKpi1Val = new Sunny.UI.UILabel();
            this.pnlKpi2 = new Sunny.UI.UIPanel();
            this.lblKpi2Title = new Sunny.UI.UILabel();
            this.lblKpi2Val = new Sunny.UI.UILabel();
            this.pnlKpi3 = new Sunny.UI.UIPanel();
            this.lblKpi3Title = new Sunny.UI.UILabel();
            this.lblKpi3Val = new Sunny.UI.UILabel();
            this.pnlKpi4 = new Sunny.UI.UIPanel();
            this.lblKpi4Title = new Sunny.UI.UILabel();
            this.lblKpi4Val = new Sunny.UI.UILabel();
            this.tabControl = new Sunny.UI.UITabControl();
            this.tpAll = new System.Windows.Forms.TabPage();
            this.pnlPager = new System.Windows.Forms.Panel();
            this.btnPrevPage = new Sunny.UI.UISymbolButton();
            this.lblPageInfo = new Sunny.UI.UILabel();
            this.btnNextPage = new Sunny.UI.UISymbolButton();
            this.dgvOrders = new Sunny.UI.UIDataGridView();
            this.colRepairNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEquipment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFaultDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUrgency = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRepairer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReportTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAction = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tpPending = new System.Windows.Forms.TabPage();
            this.tpInProgress = new System.Windows.Forms.TabPage();
            this.tpDone = new System.Windows.Forms.TabPage();
            this.pnlHeader.SuspendLayout();
            this.pnlKpiContainer.SuspendLayout();
            this.pnlKpi1.SuspendLayout();
            this.pnlKpi2.SuspendLayout();
            this.pnlKpi3.SuspendLayout();
            this.pnlKpi4.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tpAll.SuspendLayout();
            this.pnlPager.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.pnlHeader.Controls.Add(this.lblHeaderTitle);
            this.pnlHeader.Controls.Add(this.lblHeaderUser);
            this.pnlHeader.Controls.Add(this.btnRefresh);
            this.pnlHeader.Controls.Add(this.btnNewRepair);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.pnlHeader.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlHeader.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.pnlHeader.Size = new System.Drawing.Size(1260, 56);
            this.pnlHeader.Style = Sunny.UI.UIStyle.Custom;
            this.pnlHeader.TabIndex = 0;
            this.pnlHeader.Text = null;
            this.pnlHeader.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblHeaderTitle.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.lblHeaderTitle.Location = new System.Drawing.Point(14, 18);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(300, 28);
            this.lblHeaderTitle.Style = Sunny.UI.UIStyle.Custom;
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "医护报修中心";
            // 
            // lblHeaderUser
            // 
            this.lblHeaderUser.BackColor = System.Drawing.Color.Transparent;
            this.lblHeaderUser.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblHeaderUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.lblHeaderUser.Location = new System.Drawing.Point(1022, 20);
            this.lblHeaderUser.Name = "lblHeaderUser";
            this.lblHeaderUser.Size = new System.Drawing.Size(238, 26);
            this.lblHeaderUser.Style = Sunny.UI.UIStyle.Custom;
            this.lblHeaderUser.TabIndex = 1;
            this.lblHeaderUser.Text = "当前登录：李护士（医护）";
            this.lblHeaderUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnRefresh.Location = new System.Drawing.Point(810, 14);
            this.btnRefresh.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(80, 32);
            this.btnRefresh.Style = Sunny.UI.UIStyle.Custom;
            this.btnRefresh.Symbol = 61473;
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // btnNewRepair
            // 
            this.btnNewRepair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNewRepair.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnNewRepair.Location = new System.Drawing.Point(896, 14);
            this.btnNewRepair.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnNewRepair.Name = "btnNewRepair";
            this.btnNewRepair.Size = new System.Drawing.Size(109, 32);
            this.btnNewRepair.Style = Sunny.UI.UIStyle.Custom;
            this.btnNewRepair.Symbol = 61543;
            this.btnNewRepair.TabIndex = 3;
            this.btnNewRepair.Text = "提交报修";
            this.btnNewRepair.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // pnlKpiContainer
            // 
            this.pnlKpiContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.pnlKpiContainer.Controls.Add(this.pnlKpi1);
            this.pnlKpiContainer.Controls.Add(this.pnlKpi2);
            this.pnlKpiContainer.Controls.Add(this.pnlKpi3);
            this.pnlKpiContainer.Controls.Add(this.pnlKpi4);
            this.pnlKpiContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlKpiContainer.Location = new System.Drawing.Point(0, 56);
            this.pnlKpiContainer.Name = "pnlKpiContainer";
            this.pnlKpiContainer.Size = new System.Drawing.Size(1260, 97);
            this.pnlKpiContainer.TabIndex = 1;
            // 
            // pnlKpi1
            // 
            this.pnlKpi1.Controls.Add(this.lblKpi1Title);
            this.pnlKpi1.Controls.Add(this.lblKpi1Val);
            this.pnlKpi1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.pnlKpi1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnlKpi1.Location = new System.Drawing.Point(20, 10);
            this.pnlKpi1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlKpi1.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlKpi1.Name = "pnlKpi1";
            this.pnlKpi1.Radius = 8;
            this.pnlKpi1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.pnlKpi1.Size = new System.Drawing.Size(300, 79);
            this.pnlKpi1.Style = Sunny.UI.UIStyle.Custom;
            this.pnlKpi1.TabIndex = 0;
            this.pnlKpi1.Text = null;
            this.pnlKpi1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKpi1Title
            // 
            this.lblKpi1Title.BackColor = System.Drawing.Color.Transparent;
            this.lblKpi1Title.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblKpi1Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.lblKpi1Title.Location = new System.Drawing.Point(16, 10);
            this.lblKpi1Title.Name = "lblKpi1Title";
            this.lblKpi1Title.Size = new System.Drawing.Size(120, 20);
            this.lblKpi1Title.Style = Sunny.UI.UIStyle.Custom;
            this.lblKpi1Title.TabIndex = 0;
            this.lblKpi1Title.Text = "待分配";
            // 
            // lblKpi1Val
            // 
            this.lblKpi1Val.BackColor = System.Drawing.Color.Transparent;
            this.lblKpi1Val.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold);
            this.lblKpi1Val.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.lblKpi1Val.Location = new System.Drawing.Point(16, 32);
            this.lblKpi1Val.Name = "lblKpi1Val";
            this.lblKpi1Val.Size = new System.Drawing.Size(120, 47);
            this.lblKpi1Val.Style = Sunny.UI.UIStyle.Custom;
            this.lblKpi1Val.TabIndex = 1;
            this.lblKpi1Val.Text = "0";
            // 
            // pnlKpi2
            // 
            this.pnlKpi2.Controls.Add(this.lblKpi2Title);
            this.pnlKpi2.Controls.Add(this.lblKpi2Val);
            this.pnlKpi2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.pnlKpi2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnlKpi2.Location = new System.Drawing.Point(340, 10);
            this.pnlKpi2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlKpi2.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlKpi2.Name = "pnlKpi2";
            this.pnlKpi2.Radius = 8;
            this.pnlKpi2.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(255)))));
            this.pnlKpi2.Size = new System.Drawing.Size(300, 79);
            this.pnlKpi2.Style = Sunny.UI.UIStyle.Custom;
            this.pnlKpi2.TabIndex = 1;
            this.pnlKpi2.Text = null;
            this.pnlKpi2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKpi2Title
            // 
            this.lblKpi2Title.BackColor = System.Drawing.Color.Transparent;
            this.lblKpi2Title.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblKpi2Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.lblKpi2Title.Location = new System.Drawing.Point(16, 10);
            this.lblKpi2Title.Name = "lblKpi2Title";
            this.lblKpi2Title.Size = new System.Drawing.Size(120, 20);
            this.lblKpi2Title.Style = Sunny.UI.UIStyle.Custom;
            this.lblKpi2Title.TabIndex = 0;
            this.lblKpi2Title.Text = "处理中";
            // 
            // lblKpi2Val
            // 
            this.lblKpi2Val.BackColor = System.Drawing.Color.Transparent;
            this.lblKpi2Val.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold);
            this.lblKpi2Val.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(255)))));
            this.lblKpi2Val.Location = new System.Drawing.Point(16, 32);
            this.lblKpi2Val.Name = "lblKpi2Val";
            this.lblKpi2Val.Size = new System.Drawing.Size(120, 47);
            this.lblKpi2Val.Style = Sunny.UI.UIStyle.Custom;
            this.lblKpi2Val.TabIndex = 1;
            this.lblKpi2Val.Text = "0";
            // 
            // pnlKpi3
            // 
            this.pnlKpi3.Controls.Add(this.lblKpi3Title);
            this.pnlKpi3.Controls.Add(this.lblKpi3Val);
            this.pnlKpi3.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.pnlKpi3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnlKpi3.Location = new System.Drawing.Point(660, 10);
            this.pnlKpi3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlKpi3.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlKpi3.Name = "pnlKpi3";
            this.pnlKpi3.Radius = 8;
            this.pnlKpi3.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.pnlKpi3.Size = new System.Drawing.Size(300, 79);
            this.pnlKpi3.Style = Sunny.UI.UIStyle.Custom;
            this.pnlKpi3.TabIndex = 2;
            this.pnlKpi3.Text = null;
            this.pnlKpi3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKpi3Title
            // 
            this.lblKpi3Title.BackColor = System.Drawing.Color.Transparent;
            this.lblKpi3Title.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblKpi3Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.lblKpi3Title.Location = new System.Drawing.Point(16, 10);
            this.lblKpi3Title.Name = "lblKpi3Title";
            this.lblKpi3Title.Size = new System.Drawing.Size(120, 20);
            this.lblKpi3Title.Style = Sunny.UI.UIStyle.Custom;
            this.lblKpi3Title.TabIndex = 0;
            this.lblKpi3Title.Text = "已完成";
            // 
            // lblKpi3Val
            // 
            this.lblKpi3Val.BackColor = System.Drawing.Color.Transparent;
            this.lblKpi3Val.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold);
            this.lblKpi3Val.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.lblKpi3Val.Location = new System.Drawing.Point(16, 32);
            this.lblKpi3Val.Name = "lblKpi3Val";
            this.lblKpi3Val.Size = new System.Drawing.Size(120, 47);
            this.lblKpi3Val.Style = Sunny.UI.UIStyle.Custom;
            this.lblKpi3Val.TabIndex = 1;
            this.lblKpi3Val.Text = "0";
            // 
            // pnlKpi4
            // 
            this.pnlKpi4.Controls.Add(this.lblKpi4Title);
            this.pnlKpi4.Controls.Add(this.lblKpi4Val);
            this.pnlKpi4.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.pnlKpi4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnlKpi4.Location = new System.Drawing.Point(980, 10);
            this.pnlKpi4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlKpi4.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlKpi4.Name = "pnlKpi4";
            this.pnlKpi4.Radius = 8;
            this.pnlKpi4.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.pnlKpi4.Size = new System.Drawing.Size(280, 82);
            this.pnlKpi4.Style = Sunny.UI.UIStyle.Custom;
            this.pnlKpi4.TabIndex = 3;
            this.pnlKpi4.Text = null;
            this.pnlKpi4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKpi4Title
            // 
            this.lblKpi4Title.BackColor = System.Drawing.Color.Transparent;
            this.lblKpi4Title.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblKpi4Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.lblKpi4Title.Location = new System.Drawing.Point(16, 10);
            this.lblKpi4Title.Name = "lblKpi4Title";
            this.lblKpi4Title.Size = new System.Drawing.Size(120, 20);
            this.lblKpi4Title.Style = Sunny.UI.UIStyle.Custom;
            this.lblKpi4Title.TabIndex = 0;
            this.lblKpi4Title.Text = "全部工单";
            // 
            // lblKpi4Val
            // 
            this.lblKpi4Val.BackColor = System.Drawing.Color.Transparent;
            this.lblKpi4Val.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold);
            this.lblKpi4Val.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.lblKpi4Val.Location = new System.Drawing.Point(16, 32);
            this.lblKpi4Val.Name = "lblKpi4Val";
            this.lblKpi4Val.Size = new System.Drawing.Size(120, 50);
            this.lblKpi4Val.Style = Sunny.UI.UIStyle.Custom;
            this.lblKpi4Val.TabIndex = 1;
            this.lblKpi4Val.Text = "0";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tpAll);
            this.tabControl.Controls.Add(this.tpPending);
            this.tabControl.Controls.Add(this.tpInProgress);
            this.tabControl.Controls.Add(this.tpDone);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.tabControl.ItemSize = new System.Drawing.Size(150, 40);
            this.tabControl.Location = new System.Drawing.Point(0, 153);
            this.tabControl.MainPage = "";
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1260, 565);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl.Style = Sunny.UI.UIStyle.Custom;
            this.tabControl.TabIndex = 2;
            this.tabControl.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // tpAll
            // 
            this.tpAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.tpAll.Controls.Add(this.pnlPager);
            this.tpAll.Controls.Add(this.dgvOrders);
            this.tpAll.Location = new System.Drawing.Point(0, 40);
            this.tpAll.Name = "tpAll";
            this.tpAll.Size = new System.Drawing.Size(1260, 525);
            this.tpAll.TabIndex = 0;
            this.tpAll.Text = "全部";
            // 
            // pnlPager
            // 
            this.pnlPager.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.pnlPager.Controls.Add(this.btnPrevPage);
            this.pnlPager.Controls.Add(this.lblPageInfo);
            this.pnlPager.Controls.Add(this.btnNextPage);
            this.pnlPager.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPager.Location = new System.Drawing.Point(0, 485);
            this.pnlPager.Name = "pnlPager";
            this.pnlPager.Size = new System.Drawing.Size(1260, 40);
            this.pnlPager.TabIndex = 1;
            // 
            // btnPrevPage
            // 
            this.btnPrevPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrevPage.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnPrevPage.Location = new System.Drawing.Point(490, 6);
            this.btnPrevPage.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnPrevPage.Name = "btnPrevPage";
            this.btnPrevPage.Size = new System.Drawing.Size(80, 28);
            this.btnPrevPage.Style = Sunny.UI.UIStyle.Custom;
            this.btnPrevPage.Symbol = 61696;
            this.btnPrevPage.TabIndex = 0;
            this.btnPrevPage.Text = "上一页";
            this.btnPrevPage.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // lblPageInfo
            // 
            this.lblPageInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblPageInfo.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblPageInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.lblPageInfo.Location = new System.Drawing.Point(590, 10);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(200, 20);
            this.lblPageInfo.Style = Sunny.UI.UIStyle.Custom;
            this.lblPageInfo.TabIndex = 1;
            this.lblPageInfo.Text = "第 1/1 页 · 每页 6 条";
            this.lblPageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnNextPage
            // 
            this.btnNextPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNextPage.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnNextPage.Location = new System.Drawing.Point(810, 6);
            this.btnNextPage.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(80, 28);
            this.btnNextPage.Style = Sunny.UI.UIStyle.Custom;
            this.btnNextPage.Symbol = 61697;
            this.btnNextPage.TabIndex = 2;
            this.btnNextPage.Text = "下一页";
            this.btnNextPage.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // dgvOrders
            // 
            this.dgvOrders.AllowUserToAddRows = false;
            this.dgvOrders.AllowUserToDeleteRows = false;
            this.dgvOrders.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.dgvOrders.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvOrders.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.dgvOrders.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvOrders.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOrders.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvOrders.ColumnHeadersHeight = 32;
            this.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvOrders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRepairNo,
            this.colEquipment,
            this.colFaultDesc,
            this.colUrgency,
            this.colStage,
            this.colRepairer,
            this.colReportTime,
            this.colAction});
            this.dgvOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrders.EnableHeadersVisualStyles = false;
            this.dgvOrders.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.dgvOrders.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.dgvOrders.Location = new System.Drawing.Point(0, 0);
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.ReadOnly = true;
            this.dgvOrders.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOrders.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvOrders.RowHeadersVisible = false;
            this.dgvOrders.RowHeadersWidth = 62;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Azure;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvOrders.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvOrders.RowTemplate.Height = 38;
            this.dgvOrders.ScrollBarBackColor = System.Drawing.Color.DimGray;
            this.dgvOrders.ScrollBarStyleInherited = false;
            this.dgvOrders.SelectedIndex = -1;
            this.dgvOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrders.Size = new System.Drawing.Size(1260, 525);
            this.dgvOrders.StripeEvenColor = System.Drawing.Color.Azure;
            this.dgvOrders.StripeOddColor = System.Drawing.Color.LightCyan;
            this.dgvOrders.Style = Sunny.UI.UIStyle.Custom;
            this.dgvOrders.TabIndex = 0;
            // 
            // colRepairNo
            // 
            this.colRepairNo.DataPropertyName = "RepairNo";
            this.colRepairNo.HeaderText = "工单号";
            this.colRepairNo.MinimumWidth = 8;
            this.colRepairNo.Name = "colRepairNo";
            this.colRepairNo.ReadOnly = true;
            this.colRepairNo.Width = 130;
            // 
            // colEquipment
            // 
            this.colEquipment.DataPropertyName = "EquipmentName";
            this.colEquipment.HeaderText = "设备名称";
            this.colEquipment.MinimumWidth = 8;
            this.colEquipment.Name = "colEquipment";
            this.colEquipment.ReadOnly = true;
            this.colEquipment.Width = 150;
            // 
            // colFaultDesc
            // 
            this.colFaultDesc.DataPropertyName = "FaultDesc";
            this.colFaultDesc.HeaderText = "故障描述";
            this.colFaultDesc.MinimumWidth = 8;
            this.colFaultDesc.Name = "colFaultDesc";
            this.colFaultDesc.ReadOnly = true;
            this.colFaultDesc.Width = 220;
            // 
            // colUrgency
            // 
            this.colUrgency.DataPropertyName = "UrgencyText";
            this.colUrgency.HeaderText = "紧急度";
            this.colUrgency.MinimumWidth = 8;
            this.colUrgency.Name = "colUrgency";
            this.colUrgency.ReadOnly = true;
            this.colUrgency.Width = 80;
            // 
            // colStage
            // 
            this.colStage.DataPropertyName = "ProgressStageText";
            this.colStage.HeaderText = "当前状态";
            this.colStage.MinimumWidth = 8;
            this.colStage.Name = "colStage";
            this.colStage.ReadOnly = true;
            this.colStage.Width = 150;
            // 
            // colRepairer
            // 
            this.colRepairer.DataPropertyName = "RepairerName";
            this.colRepairer.HeaderText = "维修员";
            this.colRepairer.MinimumWidth = 8;
            this.colRepairer.Name = "colRepairer";
            this.colRepairer.ReadOnly = true;
            this.colRepairer.Width = 150;
            // 
            // colReportTime
            // 
            this.colReportTime.DataPropertyName = "ReportTime";
            this.colReportTime.HeaderText = "报修时间";
            this.colReportTime.MinimumWidth = 8;
            this.colReportTime.Name = "colReportTime";
            this.colReportTime.ReadOnly = true;
            this.colReportTime.Width = 140;
            // 
            // colAction
            // 
            this.colAction.HeaderText = "操作";
            this.colAction.MinimumWidth = 8;
            this.colAction.Name = "colAction";
            this.colAction.ReadOnly = true;
            this.colAction.Text = "查看详情";
            this.colAction.UseColumnTextForButtonValue = false;
            this.colAction.Width = 150;
            // 
            // tpPending
            // 
            this.tpPending.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.tpPending.Location = new System.Drawing.Point(0, 40);
            this.tpPending.Name = "tpPending";
            this.tpPending.Size = new System.Drawing.Size(200, 60);
            this.tpPending.TabIndex = 1;
            this.tpPending.Text = "待分配";
            // 
            // tpInProgress
            // 
            this.tpInProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.tpInProgress.Location = new System.Drawing.Point(0, 40);
            this.tpInProgress.Name = "tpInProgress";
            this.tpInProgress.Size = new System.Drawing.Size(200, 60);
            this.tpInProgress.TabIndex = 2;
            this.tpInProgress.Text = "处理中";
            // 
            // tpDone
            // 
            this.tpDone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.tpDone.Location = new System.Drawing.Point(0, 40);
            this.tpDone.Name = "tpDone";
            this.tpDone.Size = new System.Drawing.Size(200, 60);
            this.tpDone.TabIndex = 3;
            this.tpDone.Text = "已完成";
            // 
            // DoctorWorkbench
            // 
            this.AllowShowTitle = false;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.ClientSize = new System.Drawing.Size(1260, 718);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.pnlKpiContainer);
            this.Controls.Add(this.pnlHeader);
            this.Name = "DoctorWorkbench";
            this.Padding = new System.Windows.Forms.Padding(0);
            this.ShowTitle = false;
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "医护报修中心";
            this.ZoomScaleRect = new System.Drawing.Rectangle(22, 22, 1280, 670);
            this.pnlHeader.ResumeLayout(false);
            this.pnlKpiContainer.ResumeLayout(false);
            this.pnlKpi1.ResumeLayout(false);
            this.pnlKpi2.ResumeLayout(false);
            this.pnlKpi3.ResumeLayout(false);
            this.pnlKpi4.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tpAll.ResumeLayout(false);
            this.pnlPager.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIPanel pnlHeader;
        private Sunny.UI.UILabel lblHeaderTitle;
        private Sunny.UI.UILabel lblHeaderUser;
        private Sunny.UI.UISymbolButton btnRefresh;
        private Sunny.UI.UISymbolButton btnNewRepair;
        private System.Windows.Forms.Panel pnlKpiContainer;
        private Sunny.UI.UIPanel pnlKpi1;
        private Sunny.UI.UILabel lblKpi1Title;
        private Sunny.UI.UILabel lblKpi1Val;
        private Sunny.UI.UIPanel pnlKpi2;
        private Sunny.UI.UILabel lblKpi2Title;
        private Sunny.UI.UILabel lblKpi2Val;
        private Sunny.UI.UIPanel pnlKpi3;
        private Sunny.UI.UILabel lblKpi3Title;
        private Sunny.UI.UILabel lblKpi3Val;
        private Sunny.UI.UIPanel pnlKpi4;
        private Sunny.UI.UILabel lblKpi4Title;
        private Sunny.UI.UILabel lblKpi4Val;
        private Sunny.UI.UITabControl tabControl;
        private System.Windows.Forms.TabPage tpAll;
        private System.Windows.Forms.TabPage tpPending;
        private System.Windows.Forms.TabPage tpInProgress;
        private System.Windows.Forms.TabPage tpDone;
        private Sunny.UI.UIDataGridView dgvOrders;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRepairNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEquipment;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFaultDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUrgency;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRepairer;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReportTime;
        private System.Windows.Forms.DataGridViewButtonColumn colAction;
        private System.Windows.Forms.Panel pnlPager;
        private Sunny.UI.UISymbolButton btnPrevPage;
        private Sunny.UI.UILabel lblPageInfo;
        private Sunny.UI.UISymbolButton btnNextPage;
    }
}
