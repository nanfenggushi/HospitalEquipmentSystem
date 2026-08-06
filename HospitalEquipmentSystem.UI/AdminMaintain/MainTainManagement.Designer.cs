namespace HospitalEquipmentSystem.UI
{
    partial class MainTainManagement
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
            this.lblTitle = new Sunny.UI.UILabel();
            this.btnNewOrder = new Sunny.UI.UISymbolButton();
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
            this.pnlKpi4 = new Sunny.UI.UIPanel();
            this.lblKpi4Val = new Sunny.UI.UILabel();
            this.lblKpi4Sub = new Sunny.UI.UILabel();
            this.pnlKpi5 = new Sunny.UI.UIPanel();
            this.lblKpi5Val = new Sunny.UI.UILabel();
            this.lblKpi5Sub = new Sunny.UI.UILabel();
            this.pnlKpi6 = new Sunny.UI.UIPanel();
            this.lblKpi6Val = new Sunny.UI.UILabel();
            this.lblKpi6Sub = new Sunny.UI.UILabel();
            this.tlpBody = new System.Windows.Forms.TableLayoutPanel();
            this.pnlLeft = new Sunny.UI.UIPanel();
            this.pnlPager = new System.Windows.Forms.Panel();
            this.btnPrevPage = new Sunny.UI.UISymbolButton();
            this.lblPageInfo = new Sunny.UI.UILabel();
            this.btnNextPage = new Sunny.UI.UISymbolButton();
            this.dgvOrders = new Sunny.UI.UIDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pnlFilter = new System.Windows.Forms.FlowLayoutPanel();
            this.txtSearch = new Sunny.UI.UITextBox();
            this.cmbUrgency = new Sunny.UI.UIComboBox();
            this.cmbDept = new Sunny.UI.UIComboBox();
            this.cmbSource = new Sunny.UI.UIComboBox();
            this.cmbDateRange = new Sunny.UI.UIComboBox();
            this.btnQuery = new Sunny.UI.UISymbolButton();
            this.btnReset = new Sunny.UI.UISymbolButton();
            this.pnlMainHeader = new Sunny.UI.UIPanel();
            this.lblTableTitle = new Sunny.UI.UILabel();
            this.pnlRight = new Sunny.UI.UIPanel();
            this.pnlWorkload = new Sunny.UI.UIPanel();
            this.pnlWorkloadList = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlWorkloadHeader = new Sunny.UI.UIPanel();
            this.lblWorkloadTitle = new Sunny.UI.UILabel();
            this.pnlAlerts = new Sunny.UI.UIPanel();
            this.pnlAlertsList = new HospitalEquipmentSystem.UI.ColoredFlowLayoutPanel();
            this.pnlAlertsHeader = new Sunny.UI.UIPanel();
            this.lblAlertsTitle = new Sunny.UI.UILabel();
            this.pnl_mid = new Sunny.UI.UIPanel();
            this.pnlHeader.SuspendLayout();
            this.pnlKpiContainer.SuspendLayout();
            this.pnlKpi1.SuspendLayout();
            this.pnlKpi2.SuspendLayout();
            this.pnlKpi3.SuspendLayout();
            this.pnlKpi4.SuspendLayout();
            this.pnlKpi5.SuspendLayout();
            this.pnlKpi6.SuspendLayout();
            this.tlpBody.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.pnlPager.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            this.pnlFilter.SuspendLayout();
            this.pnlMainHeader.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.pnlWorkload.SuspendLayout();
            this.pnlWorkloadHeader.SuspendLayout();
            this.pnlAlerts.SuspendLayout();
            this.pnlAlertsHeader.SuspendLayout();
            this.pnl_mid.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.btnNewOrder);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.FillColor = System.Drawing.Color.Transparent;
            this.pnlHeader.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.pnlHeader.Font = new System.Drawing.Font("宋体", 12F);
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(0);
            this.pnlHeader.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(20, 12, 20, 12);
            this.pnlHeader.Radius = 10;
            this.pnlHeader.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.pnlHeader.Size = new System.Drawing.Size(1727, 80);
            this.pnlHeader.TabIndex = 0;
            this.pnlHeader.Text = null;
            this.pnlHeader.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.lblTitle.Location = new System.Drawing.Point(23, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(200, 51);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "维修管理";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnNewOrder
            // 
            this.btnNewOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewOrder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNewOrder.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
            this.btnNewOrder.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
            this.btnNewOrder.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(255)))));
            this.btnNewOrder.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
            this.btnNewOrder.Font = new System.Drawing.Font("宋体", 10F);
            this.btnNewOrder.Location = new System.Drawing.Point(1577, 21);
            this.btnNewOrder.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnNewOrder.Name = "btnNewOrder";
            this.btnNewOrder.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
            this.btnNewOrder.Size = new System.Drawing.Size(130, 38);
            this.btnNewOrder.Style = Sunny.UI.UIStyle.Custom;
            this.btnNewOrder.Symbol = 61543;
            this.btnNewOrder.TabIndex = 3;
            this.btnNewOrder.Text = "新建工单";
            this.btnNewOrder.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // pnlKpiContainer
            // 
            this.pnlKpiContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.pnlKpiContainer.Controls.Add(this.pnlKpi1);
            this.pnlKpiContainer.Controls.Add(this.pnlKpi2);
            this.pnlKpiContainer.Controls.Add(this.pnlKpi3);
            this.pnlKpiContainer.Controls.Add(this.pnlKpi4);
            this.pnlKpiContainer.Controls.Add(this.pnlKpi5);
            this.pnlKpiContainer.Controls.Add(this.pnlKpi6);
            this.pnlKpiContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlKpiContainer.Location = new System.Drawing.Point(0, 80);
            this.pnlKpiContainer.Margin = new System.Windows.Forms.Padding(0);
            this.pnlKpiContainer.Name = "pnlKpiContainer";
            this.pnlKpiContainer.Padding = new System.Windows.Forms.Padding(16, 10, 16, 6);
            this.pnlKpiContainer.Size = new System.Drawing.Size(1727, 120);
            this.pnlKpiContainer.TabIndex = 1;
            this.pnlKpiContainer.WrapContents = false;
            // 
            // pnlKpi1
            // 
            this.pnlKpi1.Controls.Add(this.lblKpi1Val);
            this.pnlKpi1.Controls.Add(this.lblKpi1Sub);
            this.pnlKpi1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.pnlKpi1.Font = new System.Drawing.Font("宋体", 12F);
            this.pnlKpi1.Location = new System.Drawing.Point(19, 13);
            this.pnlKpi1.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.pnlKpi1.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlKpi1.Name = "pnlKpi1";
            this.pnlKpi1.Radius = 10;
            this.pnlKpi1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.pnlKpi1.Size = new System.Drawing.Size(195, 80);
            this.pnlKpi1.TabIndex = 0;
            this.pnlKpi1.Text = null;
            this.pnlKpi1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKpi1Val
            // 
            this.lblKpi1Val.Font = new System.Drawing.Font("微软雅黑", 22F, System.Drawing.FontStyle.Bold);
            this.lblKpi1Val.ForeColor = System.Drawing.Color.White;
            this.lblKpi1Val.Location = new System.Drawing.Point(14, 10);
            this.lblKpi1Val.Name = "lblKpi1Val";
            this.lblKpi1Val.Size = new System.Drawing.Size(167, 36);
            this.lblKpi1Val.TabIndex = 0;
            this.lblKpi1Val.Text = "2";
            this.lblKpi1Val.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblKpi1Sub
            // 
            this.lblKpi1Sub.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblKpi1Sub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(130)))), ((int)(((byte)(156)))));
            this.lblKpi1Sub.Location = new System.Drawing.Point(14, 48);
            this.lblKpi1Sub.Name = "lblKpi1Sub";
            this.lblKpi1Sub.Size = new System.Drawing.Size(167, 22);
            this.lblKpi1Sub.TabIndex = 1;
            this.lblKpi1Sub.Text = "待分配工单 · 需尽快派单";
            this.lblKpi1Sub.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlKpi2
            // 
            this.pnlKpi2.Controls.Add(this.lblKpi2Val);
            this.pnlKpi2.Controls.Add(this.lblKpi2Sub);
            this.pnlKpi2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.pnlKpi2.Font = new System.Drawing.Font("宋体", 12F);
            this.pnlKpi2.Location = new System.Drawing.Point(227, 13);
            this.pnlKpi2.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.pnlKpi2.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlKpi2.Name = "pnlKpi2";
            this.pnlKpi2.Radius = 10;
            this.pnlKpi2.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.pnlKpi2.Size = new System.Drawing.Size(195, 80);
            this.pnlKpi2.TabIndex = 1;
            this.pnlKpi2.Text = null;
            this.pnlKpi2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKpi2Val
            // 
            this.lblKpi2Val.Font = new System.Drawing.Font("微软雅黑", 22F, System.Drawing.FontStyle.Bold);
            this.lblKpi2Val.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(176)))), ((int)(((byte)(66)))));
            this.lblKpi2Val.Location = new System.Drawing.Point(14, 10);
            this.lblKpi2Val.Name = "lblKpi2Val";
            this.lblKpi2Val.Size = new System.Drawing.Size(167, 36);
            this.lblKpi2Val.TabIndex = 0;
            this.lblKpi2Val.Text = "5";
            this.lblKpi2Val.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblKpi2Sub
            // 
            this.lblKpi2Sub.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblKpi2Sub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(130)))), ((int)(((byte)(156)))));
            this.lblKpi2Sub.Location = new System.Drawing.Point(14, 48);
            this.lblKpi2Sub.Name = "lblKpi2Sub";
            this.lblKpi2Sub.Size = new System.Drawing.Size(167, 22);
            this.lblKpi2Sub.TabIndex = 1;
            this.lblKpi2Sub.Text = "处理中 · 2台生命支持设备";
            this.lblKpi2Sub.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlKpi3
            // 
            this.pnlKpi3.Controls.Add(this.lblKpi3Val);
            this.pnlKpi3.Controls.Add(this.lblKpi3Sub);
            this.pnlKpi3.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.pnlKpi3.Font = new System.Drawing.Font("宋体", 12F);
            this.pnlKpi3.Location = new System.Drawing.Point(435, 13);
            this.pnlKpi3.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.pnlKpi3.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlKpi3.Name = "pnlKpi3";
            this.pnlKpi3.Radius = 10;
            this.pnlKpi3.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.pnlKpi3.Size = new System.Drawing.Size(195, 80);
            this.pnlKpi3.TabIndex = 2;
            this.pnlKpi3.Text = null;
            this.pnlKpi3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKpi3Val
            // 
            this.lblKpi3Val.Font = new System.Drawing.Font("微软雅黑", 22F, System.Drawing.FontStyle.Bold);
            this.lblKpi3Val.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(211)))), ((int)(((byte)(238)))));
            this.lblKpi3Val.Location = new System.Drawing.Point(14, 10);
            this.lblKpi3Val.Name = "lblKpi3Val";
            this.lblKpi3Val.Size = new System.Drawing.Size(167, 36);
            this.lblKpi3Val.TabIndex = 0;
            this.lblKpi3Val.Text = "1";
            this.lblKpi3Val.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblKpi3Sub
            // 
            this.lblKpi3Sub.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblKpi3Sub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(130)))), ((int)(((byte)(156)))));
            this.lblKpi3Sub.Location = new System.Drawing.Point(14, 48);
            this.lblKpi3Sub.Name = "lblKpi3Sub";
            this.lblKpi3Sub.Size = new System.Drawing.Size(167, 22);
            this.lblKpi3Sub.TabIndex = 1;
            this.lblKpi3Sub.Text = "特急工单 · 需优先处理";
            this.lblKpi3Sub.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlKpi4
            // 
            this.pnlKpi4.Controls.Add(this.lblKpi4Val);
            this.pnlKpi4.Controls.Add(this.lblKpi4Sub);
            this.pnlKpi4.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.pnlKpi4.Font = new System.Drawing.Font("宋体", 12F);
            this.pnlKpi4.Location = new System.Drawing.Point(643, 13);
            this.pnlKpi4.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.pnlKpi4.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlKpi4.Name = "pnlKpi4";
            this.pnlKpi4.Radius = 10;
            this.pnlKpi4.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.pnlKpi4.Size = new System.Drawing.Size(195, 80);
            this.pnlKpi4.TabIndex = 3;
            this.pnlKpi4.Text = null;
            this.pnlKpi4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKpi4Val
            // 
            this.lblKpi4Val.Font = new System.Drawing.Font("微软雅黑", 22F, System.Drawing.FontStyle.Bold);
            this.lblKpi4Val.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.lblKpi4Val.Location = new System.Drawing.Point(14, 10);
            this.lblKpi4Val.Name = "lblKpi4Val";
            this.lblKpi4Val.Size = new System.Drawing.Size(167, 36);
            this.lblKpi4Val.TabIndex = 0;
            this.lblKpi4Val.Text = "28";
            this.lblKpi4Val.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblKpi4Sub
            // 
            this.lblKpi4Sub.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblKpi4Sub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(130)))), ((int)(((byte)(156)))));
            this.lblKpi4Sub.Location = new System.Drawing.Point(14, 48);
            this.lblKpi4Sub.Name = "lblKpi4Sub";
            this.lblKpi4Sub.Size = new System.Drawing.Size(167, 22);
            this.lblKpi4Sub.TabIndex = 1;
            this.lblKpi4Sub.Text = "已完成工单";
            this.lblKpi4Sub.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlKpi5
            // 
            this.pnlKpi5.Controls.Add(this.lblKpi5Val);
            this.pnlKpi5.Controls.Add(this.lblKpi5Sub);
            this.pnlKpi5.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.pnlKpi5.Font = new System.Drawing.Font("宋体", 12F);
            this.pnlKpi5.Location = new System.Drawing.Point(851, 13);
            this.pnlKpi5.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.pnlKpi5.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlKpi5.Name = "pnlKpi5";
            this.pnlKpi5.Radius = 10;
            this.pnlKpi5.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.pnlKpi5.Size = new System.Drawing.Size(195, 80);
            this.pnlKpi5.TabIndex = 4;
            this.pnlKpi5.Text = null;
            this.pnlKpi5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKpi5Val
            // 
            this.lblKpi5Val.Font = new System.Drawing.Font("微软雅黑", 22F, System.Drawing.FontStyle.Bold);
            this.lblKpi5Val.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.lblKpi5Val.Location = new System.Drawing.Point(14, 10);
            this.lblKpi5Val.Name = "lblKpi5Val";
            this.lblKpi5Val.Size = new System.Drawing.Size(167, 36);
            this.lblKpi5Val.TabIndex = 0;
            this.lblKpi5Val.Text = "3";
            this.lblKpi5Val.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblKpi5Sub
            // 
            this.lblKpi5Sub.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblKpi5Sub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(130)))), ((int)(((byte)(156)))));
            this.lblKpi5Sub.Location = new System.Drawing.Point(14, 48);
            this.lblKpi5Sub.Name = "lblKpi5Sub";
            this.lblKpi5Sub.Size = new System.Drawing.Size(167, 22);
            this.lblKpi5Sub.TabIndex = 1;
            this.lblKpi5Sub.Text = "已指派工单 · 维修人员已就位";
            this.lblKpi5Sub.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlKpi6
            // 
            this.pnlKpi6.Controls.Add(this.lblKpi6Val);
            this.pnlKpi6.Controls.Add(this.lblKpi6Sub);
            this.pnlKpi6.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.pnlKpi6.Font = new System.Drawing.Font("宋体", 12F);
            this.pnlKpi6.Location = new System.Drawing.Point(1059, 13);
            this.pnlKpi6.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.pnlKpi6.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlKpi6.Name = "pnlKpi6";
            this.pnlKpi6.Radius = 10;
            this.pnlKpi6.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.pnlKpi6.Size = new System.Drawing.Size(195, 80);
            this.pnlKpi6.TabIndex = 5;
            this.pnlKpi6.Text = null;
            this.pnlKpi6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKpi6Val
            // 
            this.lblKpi6Val.Font = new System.Drawing.Font("微软雅黑", 22F, System.Drawing.FontStyle.Bold);
            this.lblKpi6Val.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.lblKpi6Val.Location = new System.Drawing.Point(14, 10);
            this.lblKpi6Val.Name = "lblKpi6Val";
            this.lblKpi6Val.Size = new System.Drawing.Size(167, 36);
            this.lblKpi6Val.TabIndex = 0;
            this.lblKpi6Val.Text = "6.2h";
            this.lblKpi6Val.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblKpi6Sub
            // 
            this.lblKpi6Sub.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblKpi6Sub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(130)))), ((int)(((byte)(156)))));
            this.lblKpi6Sub.Location = new System.Drawing.Point(14, 48);
            this.lblKpi6Sub.Name = "lblKpi6Sub";
            this.lblKpi6Sub.Size = new System.Drawing.Size(167, 22);
            this.lblKpi6Sub.TabIndex = 1;
            this.lblKpi6Sub.Text = "平均修复 MTTR · 较上周 -0.8h";
            this.lblKpi6Sub.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tlpBody
            // 
            this.tlpBody.BackColor = System.Drawing.Color.Transparent;
            this.tlpBody.ColumnCount = 2;
            this.tlpBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72F));
            this.tlpBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tlpBody.Controls.Add(this.pnlLeft, 0, 0);
            this.tlpBody.Controls.Add(this.pnlRight, 1, 0);
            this.tlpBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpBody.Location = new System.Drawing.Point(20, 20);
            this.tlpBody.Margin = new System.Windows.Forms.Padding(10);
            this.tlpBody.Name = "tlpBody";
            this.tlpBody.RowCount = 1;
            this.tlpBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpBody.Size = new System.Drawing.Size(1687, 1299);
            this.tlpBody.TabIndex = 2;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.pnlPager);
            this.pnlLeft.Controls.Add(this.dgvOrders);
            this.pnlLeft.Controls.Add(this.pnlFilter);
            this.pnlLeft.Controls.Add(this.pnlMainHeader);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.pnlLeft.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.pnlLeft.Font = new System.Drawing.Font("宋体", 12F);
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Margin = new System.Windows.Forms.Padding(0, 0, 7, 0);
            this.pnlLeft.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Radius = 10;
            this.pnlLeft.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.pnlLeft.Size = new System.Drawing.Size(1207, 1299);
            this.pnlLeft.TabIndex = 0;
            this.pnlLeft.Text = null;
            this.pnlLeft.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlPager
            // 
            this.pnlPager.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.pnlPager.Controls.Add(this.btnPrevPage);
            this.pnlPager.Controls.Add(this.lblPageInfo);
            this.pnlPager.Controls.Add(this.btnNextPage);
            this.pnlPager.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPager.Location = new System.Drawing.Point(0, 1259);
            this.pnlPager.Name = "pnlPager";
            this.pnlPager.Size = new System.Drawing.Size(1207, 40);
            this.pnlPager.TabIndex = 4;
            // 
            // btnPrevPage
            // 
            this.btnPrevPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrevPage.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnPrevPage.Location = new System.Drawing.Point(340, 6);
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
            this.lblPageInfo.Location = new System.Drawing.Point(440, 10);
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
            this.btnNextPage.Location = new System.Drawing.Point(660, 6);
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
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(225)))), ((int)(((byte)(245)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.dgvOrders.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvOrders.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.dgvOrders.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvOrders.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvOrders.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOrders.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvOrders.ColumnHeadersHeight = 32;
            this.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvOrders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12});
            this.dgvOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrders.EnableHeadersVisualStyles = false;
            this.dgvOrders.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.dgvOrders.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.dgvOrders.Location = new System.Drawing.Point(0, 94);
            this.dgvOrders.Margin = new System.Windows.Forms.Padding(0);
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.ReadOnly = true;
            this.dgvOrders.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(225)))), ((int)(((byte)(245)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOrders.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvOrders.RowHeadersVisible = false;
            this.dgvOrders.RowHeadersWidth = 62;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 9F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(225)))), ((int)(((byte)(245)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.dgvOrders.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvOrders.RowTemplate.Height = 38;
            this.dgvOrders.ScrollBarBackColor = System.Drawing.Color.DimGray;
            this.dgvOrders.ScrollBarStyleInherited = false;
            this.dgvOrders.SelectedIndex = -1;
            this.dgvOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrders.Size = new System.Drawing.Size(1207, 1205);
            this.dgvOrders.StripeEvenColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.dgvOrders.StripeOddColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.dgvOrders.TabIndex = 3;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "RepairNo";
            this.Column1.HeaderText = "工单号";
            this.Column1.MinimumWidth = 8;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 130;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "EquipmentName";
            this.Column2.HeaderText = "设备";
            this.Column2.MinimumWidth = 8;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 120;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "FaultType";
            this.Column3.HeaderText = "故障类型";
            this.Column3.MinimumWidth = 8;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 80;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "FaultDesc";
            this.Column4.HeaderText = "故障描述";
            this.Column4.MinimumWidth = 8;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 160;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "UrgencyText";
            this.Column5.HeaderText = "紧急度";
            this.Column5.MinimumWidth = 8;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 70;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "ProgressStageText";
            this.Column6.HeaderText = "进度阶段";
            this.Column6.MinimumWidth = 8;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 90;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "DeptName";
            this.Column7.HeaderText = "科室";
            this.Column7.MinimumWidth = 8;
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 80;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "RepairerName";
            this.Column8.HeaderText = "维修人";
            this.Column8.MinimumWidth = 8;
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 75;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "ReportTime";
            this.Column9.HeaderText = "申报时间";
            this.Column9.MinimumWidth = 8;
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 120;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "DowntimeHours";
            this.Column10.HeaderText = "停机时长";
            this.Column10.MinimumWidth = 8;
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 75;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "StatusText";
            this.Column11.HeaderText = "状态";
            this.Column11.MinimumWidth = 8;
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 65;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "操作";
            this.Column12.MinimumWidth = 8;
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Text = "操作";
            this.Column12.ToolTipText = "操作";
            this.Column12.UseColumnTextForButtonValue = true;
            this.Column12.Width = 55;
            // 
            // pnlFilter
            // 
            this.pnlFilter.AutoSize = true;
            this.pnlFilter.Controls.Add(this.txtSearch);
            this.pnlFilter.Controls.Add(this.cmbUrgency);
            this.pnlFilter.Controls.Add(this.cmbDept);
            this.pnlFilter.Controls.Add(this.cmbSource);
            this.pnlFilter.Controls.Add(this.cmbDateRange);
            this.pnlFilter.Controls.Add(this.btnQuery);
            this.pnlFilter.Controls.Add(this.btnReset);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 42);
            this.pnlFilter.Margin = new System.Windows.Forms.Padding(0);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.pnlFilter.Size = new System.Drawing.Size(1207, 52);
            this.pnlFilter.TabIndex = 2;
            // 
            // txtSearch
            // 
            this.txtSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSearch.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.txtSearch.Font = new System.Drawing.Font("宋体", 10F);
            this.txtSearch.Location = new System.Drawing.Point(13, 9);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            this.txtSearch.MinimumSize = new System.Drawing.Size(1, 1);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Padding = new System.Windows.Forms.Padding(5);
            this.txtSearch.ShowText = false;
            this.txtSearch.Size = new System.Drawing.Size(180, 34);
            this.txtSearch.SymbolSize = 20;
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtSearch.Watermark = "工单号/设备/故障描述";
            // 
            // cmbUrgency
            // 
            this.cmbUrgency.DataSource = null;
            this.cmbUrgency.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.cmbUrgency.Font = new System.Drawing.Font("宋体", 10F);
            this.cmbUrgency.ForeColor = System.Drawing.Color.Gray;
            this.cmbUrgency.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.cmbUrgency.Items.AddRange(new object[] {
            "全部紧急度",
            "低",
            "普通",
            "紧急"});
            this.cmbUrgency.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cmbUrgency.Location = new System.Drawing.Point(201, 9);
            this.cmbUrgency.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            this.cmbUrgency.MinimumSize = new System.Drawing.Size(1, 1);
            this.cmbUrgency.Name = "cmbUrgency";
            this.cmbUrgency.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cmbUrgency.Size = new System.Drawing.Size(120, 34);
            this.cmbUrgency.SymbolSize = 24;
            this.cmbUrgency.TabIndex = 1;
            this.cmbUrgency.Text = "全部紧急度";
            this.cmbUrgency.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmbUrgency.Watermark = "";
            // 
            // cmbDept
            // 
            this.cmbDept.DataSource = null;
            this.cmbDept.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.cmbDept.Font = new System.Drawing.Font("宋体", 10F);
            this.cmbDept.ForeColor = System.Drawing.Color.Gray;
            this.cmbDept.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.cmbDept.Items.AddRange(new object[] {
            "全部科室",
            "急诊科",
            "ICU",
            "放射科",
            "检验科",
            "手术室"});
            this.cmbDept.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cmbDept.Location = new System.Drawing.Point(329, 9);
            this.cmbDept.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            this.cmbDept.MinimumSize = new System.Drawing.Size(1, 1);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cmbDept.Size = new System.Drawing.Size(120, 34);
            this.cmbDept.SymbolSize = 24;
            this.cmbDept.TabIndex = 2;
            this.cmbDept.Text = "全部科室";
            this.cmbDept.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmbDept.Watermark = "";
            // 
            // cmbSource
            // 
            this.cmbSource.DataSource = null;
            this.cmbSource.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.cmbSource.Font = new System.Drawing.Font("宋体", 10F);
            this.cmbSource.ForeColor = System.Drawing.Color.Gray;
            this.cmbSource.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.cmbSource.Items.AddRange(new object[] {
            "全部来源",
            "手动申报",
            "告警生成"});
            this.cmbSource.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cmbSource.Location = new System.Drawing.Point(457, 9);
            this.cmbSource.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            this.cmbSource.MinimumSize = new System.Drawing.Size(1, 1);
            this.cmbSource.Name = "cmbSource";
            this.cmbSource.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cmbSource.Size = new System.Drawing.Size(120, 34);
            this.cmbSource.SymbolSize = 24;
            this.cmbSource.TabIndex = 3;
            this.cmbSource.Text = "全部来源";
            this.cmbSource.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmbSource.Watermark = "";
            // 
            // cmbDateRange
            // 
            this.cmbDateRange.DataSource = null;
            this.cmbDateRange.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.cmbDateRange.Font = new System.Drawing.Font("宋体", 10F);
            this.cmbDateRange.ForeColor = System.Drawing.Color.Gray;
            this.cmbDateRange.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.cmbDateRange.Items.AddRange(new object[] {
            "近30天",
            "今日",
            "本周",
            "本月",
            "自定义"});
            this.cmbDateRange.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cmbDateRange.Location = new System.Drawing.Point(585, 9);
            this.cmbDateRange.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            this.cmbDateRange.MinimumSize = new System.Drawing.Size(1, 1);
            this.cmbDateRange.Name = "cmbDateRange";
            this.cmbDateRange.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cmbDateRange.Size = new System.Drawing.Size(120, 34);
            this.cmbDateRange.SymbolSize = 24;
            this.cmbDateRange.TabIndex = 4;
            this.cmbDateRange.Text = "近30天";
            this.cmbDateRange.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmbDateRange.Watermark = "";
            // 
            // btnQuery
            // 
            this.btnQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuery.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
            this.btnQuery.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
            this.btnQuery.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(255)))));
            this.btnQuery.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
            this.btnQuery.Font = new System.Drawing.Font("宋体", 9F);
            this.btnQuery.Location = new System.Drawing.Point(713, 9);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            this.btnQuery.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
            this.btnQuery.Size = new System.Drawing.Size(70, 34);
            this.btnQuery.Style = Sunny.UI.UIStyle.Custom;
            this.btnQuery.TabIndex = 5;
            this.btnQuery.Text = "查询";
            this.btnQuery.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // btnReset
            // 
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.btnReset.Font = new System.Drawing.Font("宋体", 9F);
            this.btnReset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.btnReset.Location = new System.Drawing.Point(791, 9);
            this.btnReset.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.btnReset.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnReset.Name = "btnReset";
            this.btnReset.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.btnReset.Size = new System.Drawing.Size(70, 34);
            this.btnReset.TabIndex = 6;
            this.btnReset.Text = "重置";
            this.btnReset.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // pnlMainHeader
            // 
            this.pnlMainHeader.Controls.Add(this.lblTableTitle);
            this.pnlMainHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMainHeader.FillColor = System.Drawing.Color.Transparent;
            this.pnlMainHeader.Font = new System.Drawing.Font("宋体", 12F);
            this.pnlMainHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlMainHeader.Margin = new System.Windows.Forms.Padding(0);
            this.pnlMainHeader.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlMainHeader.Name = "pnlMainHeader";
            this.pnlMainHeader.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
            this.pnlMainHeader.Radius = 0;
            this.pnlMainHeader.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.pnlMainHeader.Size = new System.Drawing.Size(1207, 42);
            this.pnlMainHeader.TabIndex = 0;
            this.pnlMainHeader.Text = null;
            this.pnlMainHeader.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTableTitle
            // 
            this.lblTableTitle.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.lblTableTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.lblTableTitle.Location = new System.Drawing.Point(17, 10);
            this.lblTableTitle.Name = "lblTableTitle";
            this.lblTableTitle.Size = new System.Drawing.Size(200, 23);
            this.lblTableTitle.TabIndex = 0;
            this.lblTableTitle.Text = "维修工单列表";
            this.lblTableTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.pnlWorkload);
            this.pnlRight.Controls.Add(this.pnlAlerts);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Font = new System.Drawing.Font("宋体", 12F);
            this.pnlRight.Location = new System.Drawing.Point(1218, 0);
            this.pnlRight.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.pnlRight.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Radius = 0;
            this.pnlRight.Size = new System.Drawing.Size(469, 1299);
            this.pnlRight.TabIndex = 1;
            this.pnlRight.Text = null;
            this.pnlRight.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlWorkload
            // 
            this.pnlWorkload.Controls.Add(this.pnlWorkloadList);
            this.pnlWorkload.Controls.Add(this.pnlWorkloadHeader);
            this.pnlWorkload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWorkload.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.pnlWorkload.Font = new System.Drawing.Font("宋体", 12F);
            this.pnlWorkload.Location = new System.Drawing.Point(0, 330);
            this.pnlWorkload.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.pnlWorkload.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlWorkload.Name = "pnlWorkload";
            this.pnlWorkload.Radius = 10;
            this.pnlWorkload.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.pnlWorkload.Size = new System.Drawing.Size(469, 969);
            this.pnlWorkload.TabIndex = 1;
            this.pnlWorkload.Text = null;
            this.pnlWorkload.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlWorkloadList
            // 
            this.pnlWorkloadList.AutoScroll = true;
            this.pnlWorkloadList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWorkloadList.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlWorkloadList.Location = new System.Drawing.Point(0, 47);
            this.pnlWorkloadList.Margin = new System.Windows.Forms.Padding(0);
            this.pnlWorkloadList.Name = "pnlWorkloadList";
            this.pnlWorkloadList.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.pnlWorkloadList.Size = new System.Drawing.Size(469, 922);
            this.pnlWorkloadList.TabIndex = 1;
            this.pnlWorkloadList.WrapContents = false;
            // 
            // pnlWorkloadHeader
            // 
            this.pnlWorkloadHeader.Controls.Add(this.lblWorkloadTitle);
            this.pnlWorkloadHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlWorkloadHeader.FillColor = System.Drawing.Color.Transparent;
            this.pnlWorkloadHeader.Font = new System.Drawing.Font("宋体", 12F);
            this.pnlWorkloadHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlWorkloadHeader.Margin = new System.Windows.Forms.Padding(0);
            this.pnlWorkloadHeader.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlWorkloadHeader.Name = "pnlWorkloadHeader";
            this.pnlWorkloadHeader.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
            this.pnlWorkloadHeader.Radius = 0;
            this.pnlWorkloadHeader.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.pnlWorkloadHeader.Size = new System.Drawing.Size(469, 47);
            this.pnlWorkloadHeader.TabIndex = 0;
            this.pnlWorkloadHeader.Text = null;
            this.pnlWorkloadHeader.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWorkloadTitle
            // 
            this.lblWorkloadTitle.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.lblWorkloadTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.lblWorkloadTitle.Location = new System.Drawing.Point(17, 9);
            this.lblWorkloadTitle.Name = "lblWorkloadTitle";
            this.lblWorkloadTitle.Size = new System.Drawing.Size(198, 30);
            this.lblWorkloadTitle.TabIndex = 0;
            this.lblWorkloadTitle.Text = "维修员工作负载";
            this.lblWorkloadTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlAlerts
            // 
            this.pnlAlerts.Controls.Add(this.pnlAlertsList);
            this.pnlAlerts.Controls.Add(this.pnlAlertsHeader);
            this.pnlAlerts.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAlerts.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.pnlAlerts.Font = new System.Drawing.Font("宋体", 12F);
            this.pnlAlerts.Location = new System.Drawing.Point(0, 0);
            this.pnlAlerts.Margin = new System.Windows.Forms.Padding(0);
            this.pnlAlerts.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlAlerts.Name = "pnlAlerts";
            this.pnlAlerts.Radius = 10;
            this.pnlAlerts.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.pnlAlerts.Size = new System.Drawing.Size(469, 330);
            this.pnlAlerts.TabIndex = 0;
            this.pnlAlerts.Text = null;
            this.pnlAlerts.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlAlertsList
            // 
            this.pnlAlertsList.AutoScroll = true;
            this.pnlAlertsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAlertsList.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlAlertsList.Location = new System.Drawing.Point(0, 52);
            this.pnlAlertsList.Margin = new System.Windows.Forms.Padding(0);
            this.pnlAlertsList.Name = "pnlAlertsList";
            this.pnlAlertsList.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.pnlAlertsList.ScrollBarBackColor = System.Drawing.Color.DimGray;
            this.pnlAlertsList.ScrollBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.pnlAlertsList.Size = new System.Drawing.Size(469, 278);
            this.pnlAlertsList.TabIndex = 1;
            this.pnlAlertsList.WrapContents = false;
            // 
            // pnlAlertsHeader
            // 
            this.pnlAlertsHeader.Controls.Add(this.lblAlertsTitle);
            this.pnlAlertsHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAlertsHeader.FillColor = System.Drawing.Color.Transparent;
            this.pnlAlertsHeader.Font = new System.Drawing.Font("宋体", 12F);
            this.pnlAlertsHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlAlertsHeader.Margin = new System.Windows.Forms.Padding(0);
            this.pnlAlertsHeader.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlAlertsHeader.Name = "pnlAlertsHeader";
            this.pnlAlertsHeader.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
            this.pnlAlertsHeader.Radius = 0;
            this.pnlAlertsHeader.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.pnlAlertsHeader.Size = new System.Drawing.Size(469, 52);
            this.pnlAlertsHeader.TabIndex = 0;
            this.pnlAlertsHeader.Text = null;
            this.pnlAlertsHeader.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAlertsTitle
            // 
            this.lblAlertsTitle.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.lblAlertsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.lblAlertsTitle.Location = new System.Drawing.Point(17, 9);
            this.lblAlertsTitle.Name = "lblAlertsTitle";
            this.lblAlertsTitle.Size = new System.Drawing.Size(150, 35);
            this.lblAlertsTitle.TabIndex = 0;
            this.lblAlertsTitle.Text = "实时告警";
            this.lblAlertsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnl_mid
            // 
            this.pnl_mid.Controls.Add(this.tlpBody);
            this.pnl_mid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_mid.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.pnl_mid.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnl_mid.Location = new System.Drawing.Point(0, 200);
            this.pnl_mid.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnl_mid.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnl_mid.Name = "pnl_mid";
            this.pnl_mid.Padding = new System.Windows.Forms.Padding(20);
            this.pnl_mid.Radius = 10;
            this.pnl_mid.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.pnl_mid.Size = new System.Drawing.Size(1727, 1339);
            this.pnl_mid.TabIndex = 3;
            this.pnl_mid.Text = null;
            this.pnl_mid.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainTainManagement
            // 
            this.AllowShowTitle = false;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(1727, 1539);
            this.Controls.Add(this.pnl_mid);
            this.Controls.Add(this.pnlKpiContainer);
            this.Controls.Add(this.pnlHeader);
            this.Name = "MainTainManagement";
            this.Padding = new System.Windows.Forms.Padding(0);
            this.ShowTitle = false;
            this.Text = "维修管理";
            this.ZoomScaleRect = new System.Drawing.Rectangle(22, 22, 1300, 850);
            this.pnlHeader.ResumeLayout(false);
            this.pnlKpiContainer.ResumeLayout(false);
            this.pnlKpi1.ResumeLayout(false);
            this.pnlKpi2.ResumeLayout(false);
            this.pnlKpi3.ResumeLayout(false);
            this.pnlKpi4.ResumeLayout(false);
            this.pnlKpi5.ResumeLayout(false);
            this.pnlKpi6.ResumeLayout(false);
            this.tlpBody.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            this.pnlPager.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            this.pnlFilter.ResumeLayout(false);
            this.pnlMainHeader.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.pnlWorkload.ResumeLayout(false);
            this.pnlWorkloadHeader.ResumeLayout(false);
            this.pnlAlerts.ResumeLayout(false);
            this.pnlAlertsHeader.ResumeLayout(false);
            this.pnl_mid.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIPanel pnlHeader;
        private Sunny.UI.UILabel lblTitle;
        private Sunny.UI.UISymbolButton btnNewOrder;
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
        private Sunny.UI.UIPanel pnlKpi4;
        private Sunny.UI.UILabel lblKpi4Val;
        private Sunny.UI.UILabel lblKpi4Sub;
        private Sunny.UI.UIPanel pnlKpi5;
        private Sunny.UI.UILabel lblKpi5Val;
        private Sunny.UI.UILabel lblKpi5Sub;
        private Sunny.UI.UIPanel pnlKpi6;
        private Sunny.UI.UILabel lblKpi6Val;
        private Sunny.UI.UILabel lblKpi6Sub;
        private System.Windows.Forms.TableLayoutPanel tlpBody;
        private Sunny.UI.UIPanel pnlLeft;
        private Sunny.UI.UIPanel pnlMainHeader;
        private Sunny.UI.UILabel lblTableTitle;
        private System.Windows.Forms.FlowLayoutPanel pnlFilter;
        private Sunny.UI.UITextBox txtSearch;
        private Sunny.UI.UIComboBox cmbUrgency;
        private Sunny.UI.UIComboBox cmbDept;
        private Sunny.UI.UIComboBox cmbSource;
        private Sunny.UI.UIComboBox cmbDateRange;
        private Sunny.UI.UISymbolButton btnQuery;
        private Sunny.UI.UISymbolButton btnReset;
        private Sunny.UI.UIDataGridView dgvOrders;
        private System.Windows.Forms.Panel pnlPager;
        private Sunny.UI.UISymbolButton btnPrevPage;
        private Sunny.UI.UILabel lblPageInfo;
        private Sunny.UI.UISymbolButton btnNextPage;
        private Sunny.UI.UIPanel pnlRight;
        private Sunny.UI.UIPanel pnlAlerts;
        private Sunny.UI.UIPanel pnlAlertsHeader;
        private Sunny.UI.UILabel lblAlertsTitle;
        private HospitalEquipmentSystem.UI.ColoredFlowLayoutPanel pnlAlertsList;
        private Sunny.UI.UIPanel pnlWorkload;
        private Sunny.UI.UIPanel pnlWorkloadHeader;
        private Sunny.UI.UILabel lblWorkloadTitle;
        private System.Windows.Forms.FlowLayoutPanel pnlWorkloadList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewButtonColumn Column12;
        private Sunny.UI.UIPanel pnl_mid;
    }
}