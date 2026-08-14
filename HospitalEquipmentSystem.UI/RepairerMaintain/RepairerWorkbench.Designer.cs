namespace HospitalEquipmentSystem.UI
{
    partial class RepairerWorkbench
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new Sunny.UI.UIPanel();
            this.lblTitle = new Sunny.UI.UILabel();
            this.lblRepairer = new Sunny.UI.UILabel();
            this.btnRefresh = new Sunny.UI.UISymbolButton();
            this.pnlKpiContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlKpi1 = new Sunny.UI.UIPanel();
            this.lblKpi1Val = new Sunny.UI.UILabel();
            this.lblKpi1Sub = new Sunny.UI.UILabel();
            this.pnlKpi2 = new Sunny.UI.UIPanel();
            this.lblKpi2Val = new Sunny.UI.UILabel();
            this.lblKpi2Sub = new Sunny.UI.UILabel();
            this.pnlKpi3 = new Sunny.UI.UIPanel();
            this.lblKpi3Val = new Sunny.UI.UILabel();
            this.lblKpi3Sub = new Sunny.UI.UILabel();
            this.tabControl = new Sunny.UI.UITabControl();
            this.tpPending = new System.Windows.Forms.TabPage();
            this.pnlPager = new System.Windows.Forms.Panel();
            this.btnPrevPage = new Sunny.UI.UISymbolButton();
            this.lblPageInfo = new Sunny.UI.UILabel();
            this.btnNextPage = new Sunny.UI.UISymbolButton();
            this.dgvOrders = new Sunny.UI.UIDataGridView();
            this.colRepairNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEquipment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFaultDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUrgency = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDept = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReportTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDowntime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAction = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colReject = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colProcess = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tpInProgress = new System.Windows.Forms.TabPage();
            this.tpDone = new System.Windows.Forms.TabPage();
            this.pnlHeader.SuspendLayout();
            this.pnlKpiContainer.SuspendLayout();
            this.pnlKpi1.SuspendLayout();
            this.pnlKpi2.SuspendLayout();
            this.pnlKpi3.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tpPending.SuspendLayout();
            this.pnlPager.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblRepairer);
            this.pnlHeader.Controls.Add(this.btnRefresh);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.pnlHeader.Font = new System.Drawing.Font("宋体", 12F);
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlHeader.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(20, 12, 20, 12);
            this.pnlHeader.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.pnlHeader.Size = new System.Drawing.Size(1260, 80);
            this.pnlHeader.TabIndex = 0;
            this.pnlHeader.Text = null;
            this.pnlHeader.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(300, 44);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "维修员工作台";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRepairer
            // 
            this.lblRepairer.BackColor = System.Drawing.Color.Transparent;
            this.lblRepairer.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblRepairer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.lblRepairer.Location = new System.Drawing.Point(984, 27);
            this.lblRepairer.Name = "lblRepairer";
            this.lblRepairer.Size = new System.Drawing.Size(170, 24);
            this.lblRepairer.TabIndex = 1;
            this.lblRepairer.Text = "李工（维修员）";
            this.lblRepairer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnRefresh.Location = new System.Drawing.Point(1160, 27);
            this.btnRefresh.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 32);
            this.btnRefresh.Style = Sunny.UI.UIStyle.Custom;
            this.btnRefresh.Symbol = 61473;
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // pnlKpiContainer
            // 
            this.pnlKpiContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.pnlKpiContainer.Controls.Add(this.pnlKpi1);
            this.pnlKpiContainer.Controls.Add(this.pnlKpi2);
            this.pnlKpiContainer.Controls.Add(this.pnlKpi3);
            this.pnlKpiContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlKpiContainer.Location = new System.Drawing.Point(0, 80);
            this.pnlKpiContainer.Name = "pnlKpiContainer";
            this.pnlKpiContainer.Padding = new System.Windows.Forms.Padding(15, 10, 15, 5);
            this.pnlKpiContainer.Size = new System.Drawing.Size(1260, 100);
            this.pnlKpiContainer.TabIndex = 1;
            // 
            // pnlKpi1
            // 
            this.pnlKpi1.Controls.Add(this.lblKpi1Val);
            this.pnlKpi1.Controls.Add(this.lblKpi1Sub);
            this.pnlKpi1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.pnlKpi1.Font = new System.Drawing.Font("宋体", 12F);
            this.pnlKpi1.Location = new System.Drawing.Point(19, 15);
            this.pnlKpi1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlKpi1.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlKpi1.Name = "pnlKpi1";
            this.pnlKpi1.Radius = 10;
            this.pnlKpi1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(255)))));
            this.pnlKpi1.Size = new System.Drawing.Size(400, 80);
            this.pnlKpi1.TabIndex = 0;
            this.pnlKpi1.Text = null;
            this.pnlKpi1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKpi1Val
            // 
            this.lblKpi1Val.BackColor = System.Drawing.Color.Transparent;
            this.lblKpi1Val.Font = new System.Drawing.Font("微软雅黑", 22F, System.Drawing.FontStyle.Bold);
            this.lblKpi1Val.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(255)))));
            this.lblKpi1Val.Location = new System.Drawing.Point(14, 10);
            this.lblKpi1Val.Name = "lblKpi1Val";
            this.lblKpi1Val.Size = new System.Drawing.Size(80, 54);
            this.lblKpi1Val.TabIndex = 0;
            this.lblKpi1Val.Text = "0";
            this.lblKpi1Val.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblKpi1Sub
            // 
            this.lblKpi1Sub.BackColor = System.Drawing.Color.Transparent;
            this.lblKpi1Sub.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblKpi1Sub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.lblKpi1Sub.Location = new System.Drawing.Point(100, 24);
            this.lblKpi1Sub.Name = "lblKpi1Sub";
            this.lblKpi1Sub.Size = new System.Drawing.Size(200, 22);
            this.lblKpi1Sub.TabIndex = 1;
            this.lblKpi1Sub.Text = "待接单 · 等待你接手";
            this.lblKpi1Sub.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlKpi2
            // 
            this.pnlKpi2.Controls.Add(this.lblKpi2Val);
            this.pnlKpi2.Controls.Add(this.lblKpi2Sub);
            this.pnlKpi2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.pnlKpi2.Font = new System.Drawing.Font("宋体", 12F);
            this.pnlKpi2.Location = new System.Drawing.Point(427, 15);
            this.pnlKpi2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlKpi2.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlKpi2.Name = "pnlKpi2";
            this.pnlKpi2.Radius = 10;
            this.pnlKpi2.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(166)))), ((int)(((byte)(66)))));
            this.pnlKpi2.Size = new System.Drawing.Size(400, 80);
            this.pnlKpi2.TabIndex = 1;
            this.pnlKpi2.Text = null;
            this.pnlKpi2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKpi2Val
            // 
            this.lblKpi2Val.BackColor = System.Drawing.Color.Transparent;
            this.lblKpi2Val.Font = new System.Drawing.Font("微软雅黑", 22F, System.Drawing.FontStyle.Bold);
            this.lblKpi2Val.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(166)))), ((int)(((byte)(66)))));
            this.lblKpi2Val.Location = new System.Drawing.Point(14, 10);
            this.lblKpi2Val.Name = "lblKpi2Val";
            this.lblKpi2Val.Size = new System.Drawing.Size(80, 54);
            this.lblKpi2Val.TabIndex = 0;
            this.lblKpi2Val.Text = "0";
            this.lblKpi2Val.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblKpi2Sub
            // 
            this.lblKpi2Sub.BackColor = System.Drawing.Color.Transparent;
            this.lblKpi2Sub.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblKpi2Sub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.lblKpi2Sub.Location = new System.Drawing.Point(100, 24);
            this.lblKpi2Sub.Name = "lblKpi2Sub";
            this.lblKpi2Sub.Size = new System.Drawing.Size(200, 22);
            this.lblKpi2Sub.TabIndex = 1;
            this.lblKpi2Sub.Text = "处理中 · 正在维修";
            this.lblKpi2Sub.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlKpi3
            // 
            this.pnlKpi3.Controls.Add(this.lblKpi3Val);
            this.pnlKpi3.Controls.Add(this.lblKpi3Sub);
            this.pnlKpi3.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.pnlKpi3.Font = new System.Drawing.Font("宋体", 12F);
            this.pnlKpi3.Location = new System.Drawing.Point(835, 15);
            this.pnlKpi3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlKpi3.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlKpi3.Name = "pnlKpi3";
            this.pnlKpi3.Radius = 10;
            this.pnlKpi3.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.pnlKpi3.Size = new System.Drawing.Size(400, 80);
            this.pnlKpi3.TabIndex = 2;
            this.pnlKpi3.Text = null;
            this.pnlKpi3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKpi3Val
            // 
            this.lblKpi3Val.BackColor = System.Drawing.Color.Transparent;
            this.lblKpi3Val.Font = new System.Drawing.Font("微软雅黑", 22F, System.Drawing.FontStyle.Bold);
            this.lblKpi3Val.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.lblKpi3Val.Location = new System.Drawing.Point(14, 10);
            this.lblKpi3Val.Name = "lblKpi3Val";
            this.lblKpi3Val.Size = new System.Drawing.Size(80, 54);
            this.lblKpi3Val.TabIndex = 0;
            this.lblKpi3Val.Text = "0";
            this.lblKpi3Val.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblKpi3Sub
            // 
            this.lblKpi3Sub.BackColor = System.Drawing.Color.Transparent;
            this.lblKpi3Sub.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblKpi3Sub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.lblKpi3Sub.Location = new System.Drawing.Point(100, 24);
            this.lblKpi3Sub.Name = "lblKpi3Sub";
            this.lblKpi3Sub.Size = new System.Drawing.Size(200, 22);
            this.lblKpi3Sub.TabIndex = 1;
            this.lblKpi3Sub.Text = "已完成 · 历史记录";
            this.lblKpi3Sub.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tpPending);
            this.tabControl.Controls.Add(this.tpInProgress);
            this.tabControl.Controls.Add(this.tpDone);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.tabControl.ItemSize = new System.Drawing.Size(150, 40);
            this.tabControl.Location = new System.Drawing.Point(0, 180);
            this.tabControl.MainPage = "";
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1260, 538);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl.TabIndex = 2;
            this.tabControl.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // tpPending
            // 
            this.tpPending.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.tpPending.Controls.Add(this.pnlPager);
            this.tpPending.Controls.Add(this.dgvOrders);
            this.tpPending.Location = new System.Drawing.Point(0, 40);
            this.tpPending.Name = "tpPending";
            this.tpPending.Size = new System.Drawing.Size(1260, 498);
            this.tpPending.TabIndex = 0;
            this.tpPending.Text = "待接单";
            // 
            // pnlPager
            // 
            this.pnlPager.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.pnlPager.Controls.Add(this.btnPrevPage);
            this.pnlPager.Controls.Add(this.lblPageInfo);
            this.pnlPager.Controls.Add(this.btnNextPage);
            this.pnlPager.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPager.Location = new System.Drawing.Point(0, 458);
            this.pnlPager.Name = "pnlPager";
            this.pnlPager.Size = new System.Drawing.Size(1260, 40);
            this.pnlPager.TabIndex = 1;
            // 
            // btnPrevPage
            // 
            this.btnPrevPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrevPage.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnPrevPage.Location = new System.Drawing.Point(480, 6);
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
            this.lblPageInfo.Location = new System.Drawing.Point(580, 10);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(200, 20);
            this.lblPageInfo.TabIndex = 1;
            this.lblPageInfo.Text = "第 1/1 页 · 每页 6 条";
            this.lblPageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnNextPage
            // 
            this.btnNextPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNextPage.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnNextPage.Location = new System.Drawing.Point(800, 6);
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
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvOrders.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvOrders.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.dgvOrders.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvOrders.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvOrders.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvOrders.ColumnHeadersHeight = 40;
            this.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvOrders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRepairNo,
            this.colEquipment,
            this.colFaultDesc,
            this.colUrgency,
            this.colDept,
            this.colReportTime,
            this.colResult,
            this.colCost,
            this.colDowntime,
            this.colAction,
            this.colReject,
            this.colProcess});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9.5F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvOrders.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrders.EnableHeadersVisualStyles = false;
            this.dgvOrders.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvOrders.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.dgvOrders.Location = new System.Drawing.Point(0, 0);
            this.dgvOrders.MultiSelect = false;
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.ReadOnly = true;
            this.dgvOrders.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOrders.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvOrders.RowHeadersVisible = false;
            this.dgvOrders.RowHeadersWidth = 62;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.InactiveCaption;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvOrders.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvOrders.RowTemplate.Height = 38;
            this.dgvOrders.SelectedIndex = -1;
            this.dgvOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrders.Size = new System.Drawing.Size(1260, 498);
            this.dgvOrders.StripeEvenColor = System.Drawing.SystemColors.InactiveCaption;
            this.dgvOrders.StripeOddColor = System.Drawing.SystemColors.ActiveCaption;
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
            this.colEquipment.Width = 120;
            // 
            // colFaultDesc
            // 
            this.colFaultDesc.DataPropertyName = "FaultDesc";
            this.colFaultDesc.HeaderText = "故障描述";
            this.colFaultDesc.MinimumWidth = 8;
            this.colFaultDesc.Name = "colFaultDesc";
            this.colFaultDesc.ReadOnly = true;
            this.colFaultDesc.Width = 200;
            // 
            // colUrgency
            // 
            this.colUrgency.DataPropertyName = "UrgencyText";
            this.colUrgency.HeaderText = "紧急度";
            this.colUrgency.MinimumWidth = 8;
            this.colUrgency.Name = "colUrgency";
            this.colUrgency.ReadOnly = true;
            this.colUrgency.Width = 70;
            // 
            // colDept
            // 
            this.colDept.DataPropertyName = "DeptName";
            this.colDept.HeaderText = "科室";
            this.colDept.MinimumWidth = 8;
            this.colDept.Name = "colDept";
            this.colDept.ReadOnly = true;
            this.colDept.Width = 90;
            // 
            // colReportTime
            // 
            this.colReportTime.DataPropertyName = "ReportTime";
            this.colReportTime.HeaderText = "申报时间";
            this.colReportTime.MinimumWidth = 8;
            this.colReportTime.Name = "colReportTime";
            this.colReportTime.ReadOnly = true;
            this.colReportTime.Width = 130;
            // 
            // colResult
            // 
            this.colResult.DataPropertyName = "RepairResult";
            this.colResult.HeaderText = "维修结果";
            this.colResult.MinimumWidth = 8;
            this.colResult.Name = "colResult";
            this.colResult.ReadOnly = true;
            this.colResult.Width = 180;
            // 
            // colCost
            // 
            this.colCost.DataPropertyName = "RepairCost";
            this.colCost.HeaderText = "维修费用";
            this.colCost.MinimumWidth = 8;
            this.colCost.Name = "colCost";
            this.colCost.ReadOnly = true;
            this.colCost.Width = 90;
            // 
            // colDowntime
            // 
            this.colDowntime.DataPropertyName = "DowntimeHours";
            this.colDowntime.HeaderText = "停机(h)";
            this.colDowntime.MinimumWidth = 8;
            this.colDowntime.Name = "colDowntime";
            this.colDowntime.ReadOnly = true;
            this.colDowntime.Width = 70;
            // 
            // colAction
            // 
            this.colAction.HeaderText = "操作";
            this.colAction.MinimumWidth = 8;
            this.colAction.Name = "colAction";
            this.colAction.ReadOnly = true;
            this.colAction.Text = "操作";
            this.colAction.UseColumnTextForButtonValue = true;
            this.colAction.Width = 80;
            // 
            // colReject
            // 
            this.colReject.HeaderText = "拒绝";
            this.colReject.MinimumWidth = 8;
            this.colReject.Name = "colReject";
            this.colReject.ReadOnly = true;
            this.colReject.Text = "拒绝";
            this.colReject.UseColumnTextForButtonValue = true;
            this.colReject.Width = 80;
            // 
            // colProcess
            // 
            this.colProcess.HeaderText = "处理";
            this.colProcess.MinimumWidth = 8;
            this.colProcess.Name = "colProcess";
            this.colProcess.ReadOnly = true;
            this.colProcess.Text = "处理";
            this.colProcess.UseColumnTextForButtonValue = true;
            this.colProcess.Width = 80;
            // 
            // tpInProgress
            // 
            this.tpInProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.tpInProgress.Location = new System.Drawing.Point(0, 40);
            this.tpInProgress.Name = "tpInProgress";
            this.tpInProgress.Size = new System.Drawing.Size(200, 60);
            this.tpInProgress.TabIndex = 1;
            this.tpInProgress.Text = "处理中";
            // 
            // tpDone
            // 
            this.tpDone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.tpDone.Location = new System.Drawing.Point(0, 40);
            this.tpDone.Name = "tpDone";
            this.tpDone.Size = new System.Drawing.Size(200, 60);
            this.tpDone.TabIndex = 2;
            this.tpDone.Text = "已完成";
            // 
            // RepairerWorkbench
            // 
            this.AllowShowTitle = false;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(1260, 718);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.pnlKpiContainer);
            this.Controls.Add(this.pnlHeader);
            this.Name = "RepairerWorkbench";
            this.Padding = new System.Windows.Forms.Padding(0);
            this.ShowTitle = false;
            this.Text = "维修员工作台";
            this.ZoomScaleRect = new System.Drawing.Rectangle(22, 22, 1300, 850);
            this.pnlHeader.ResumeLayout(false);
            this.pnlKpiContainer.ResumeLayout(false);
            this.pnlKpi1.ResumeLayout(false);
            this.pnlKpi2.ResumeLayout(false);
            this.pnlKpi3.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tpPending.ResumeLayout(false);
            this.pnlPager.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIPanel pnlHeader;
        private Sunny.UI.UILabel lblTitle;
        private Sunny.UI.UILabel lblRepairer;
        private Sunny.UI.UISymbolButton btnRefresh;
        private System.Windows.Forms.FlowLayoutPanel pnlKpiContainer;
        private Sunny.UI.UIPanel pnlKpi1;
        private Sunny.UI.UILabel lblKpi1Val;
        private Sunny.UI.UILabel lblKpi1Sub;
        private Sunny.UI.UIPanel pnlKpi2;
        private Sunny.UI.UILabel lblKpi2Val;
        private Sunny.UI.UILabel lblKpi2Sub;
        private Sunny.UI.UIPanel pnlKpi3;
        private Sunny.UI.UILabel lblKpi3Val;
        private Sunny.UI.UILabel lblKpi3Sub;
        private Sunny.UI.UITabControl tabControl;
        private System.Windows.Forms.TabPage tpPending;
        private System.Windows.Forms.TabPage tpInProgress;
        private System.Windows.Forms.TabPage tpDone;
        private Sunny.UI.UIDataGridView dgvOrders;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRepairNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEquipment;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFaultDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUrgency;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDept;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReportTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDowntime;
        private System.Windows.Forms.DataGridViewButtonColumn colAction;
        private System.Windows.Forms.DataGridViewButtonColumn colReject;
        private System.Windows.Forms.DataGridViewButtonColumn colProcess;
        private System.Windows.Forms.Panel pnlPager;
        private Sunny.UI.UISymbolButton btnPrevPage;
        private Sunny.UI.UILabel lblPageInfo;
        private Sunny.UI.UISymbolButton btnNextPage;
    }
}
