namespace HospitalEquipmentSystem.UI
{
    partial class AgentForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.GroupBox grpAi;
        private System.Windows.Forms.Label lblEquipment;
        private System.Windows.Forms.Label lblFaultDesc;
        private System.Windows.Forms.PictureBox picFault;
        private Sunny.UI.UISymbolButton btnAnalyze;
        private System.Windows.Forms.Label lblAiResult;
        private Sunny.UI.UIComboBox cmbFaultType;
        private Sunny.UI.UISymbolButton btnConfirmFault;

        private System.Windows.Forms.GroupBox grpMaterials;
        private Sunny.UI.UIDataGridView dgvMaterials;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMatName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMatUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnitPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubtotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMatId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIsManual;
        private System.Windows.Forms.ComboBox cmbMaterialSelect;
        private Sunny.UI.UITextBox txtManualQty;
        private Sunny.UI.UISymbolButton btnAddToGrid;
        private System.Windows.Forms.Label lblAiAmount;
        private System.Windows.Forms.Label lblManualAmount;
        private System.Windows.Forms.Label lblTotalAmount;
        private Sunny.UI.UISymbolButton btnConfirm;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpAi = new System.Windows.Forms.GroupBox();
            this.cmbFaultType = new Sunny.UI.UIComboBox();
            this.btnConfirmFault = new Sunny.UI.UISymbolButton();
            this.lblAiResult = new System.Windows.Forms.Label();
            this.btnAnalyze = new Sunny.UI.UISymbolButton();
            this.picFault = new System.Windows.Forms.PictureBox();
            this.lblFaultDesc = new System.Windows.Forms.Label();
            this.lblEquipment = new System.Windows.Forms.Label();
            this.grpMaterials = new System.Windows.Forms.GroupBox();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.lblManualAmount = new System.Windows.Forms.Label();
            this.lblAiAmount = new System.Windows.Forms.Label();
            this.btnAddToGrid = new Sunny.UI.UISymbolButton();
            this.txtManualQty = new Sunny.UI.UITextBox();
            this.cmbMaterialSelect = new System.Windows.Forms.ComboBox();
            this.dgvMaterials = new Sunny.UI.UIDataGridView();
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colMatName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMatUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMatId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsManual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnConfirm = new Sunny.UI.UISymbolButton();
            this.grpAi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFault)).BeginInit();
            this.grpMaterials.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterials)).BeginInit();
            this.SuspendLayout();
            // 
            // grpAi
            // 
            this.grpAi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.grpAi.Controls.Add(this.cmbFaultType);
            this.grpAi.Controls.Add(this.btnConfirmFault);
            this.grpAi.Controls.Add(this.lblAiResult);
            this.grpAi.Controls.Add(this.btnAnalyze);
            this.grpAi.Controls.Add(this.picFault);
            this.grpAi.Controls.Add(this.lblFaultDesc);
            this.grpAi.Controls.Add(this.lblEquipment);
            this.grpAi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.grpAi.Location = new System.Drawing.Point(12, 40);
            this.grpAi.Name = "grpAi";
            this.grpAi.Size = new System.Drawing.Size(740, 240);
            this.grpAi.TabIndex = 1;
            this.grpAi.TabStop = false;
            this.grpAi.Text = "AI 故障分析";
            // 
            // cmbFaultType
            // 
            this.cmbFaultType.DataSource = null;
            this.cmbFaultType.FillColor = System.Drawing.Color.White;
            this.cmbFaultType.Font = new System.Drawing.Font("微软雅黑", 10F); this.cmbFaultType.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cmbFaultType.Location = new System.Drawing.Point(280, 160);
            this.cmbFaultType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbFaultType.MinimumSize = new System.Drawing.Size(63, 0);
            this.cmbFaultType.Name = "cmbFaultType";
            this.cmbFaultType.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cmbFaultType.Size = new System.Drawing.Size(240, 28);
            this.cmbFaultType.Style = Sunny.UI.UIStyle.Custom;
            this.cmbFaultType.SymbolSize = 24;
            this.cmbFaultType.TabIndex = 0;
            this.cmbFaultType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmbFaultType.Visible = false;
            this.cmbFaultType.Watermark = "手动选择故障类型";
            // 
            // btnConfirmFault
            // 
            this.btnConfirmFault.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmFault.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
            this.btnConfirmFault.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnConfirmFault.Location = new System.Drawing.Point(530, 160);
            this.btnConfirmFault.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnConfirmFault.Name = "btnConfirmFault";
            this.btnConfirmFault.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.btnConfirmFault.Size = new System.Drawing.Size(160, 28);
            this.btnConfirmFault.Style = Sunny.UI.UIStyle.Custom;
            this.btnConfirmFault.TabIndex = 1;
            this.btnConfirmFault.Text = "确认故障类型，加载推荐物料";
            this.btnConfirmFault.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirmFault.Visible = false;
            // 
            // lblAiResult
            // 
            this.lblAiResult.AutoSize = true;
            this.lblAiResult.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblAiResult.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblAiResult.Location = new System.Drawing.Point(277, 126);
            this.lblAiResult.Name = "lblAiResult";
            this.lblAiResult.Size = new System.Drawing.Size(139, 27);
            this.lblAiResult.TabIndex = 2;
            this.lblAiResult.Text = "等待 AI 分析...";
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAnalyze.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
            this.btnAnalyze.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnAnalyze.Location = new System.Drawing.Point(280, 76);
            this.btnAnalyze.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.btnAnalyze.Size = new System.Drawing.Size(140, 36);
            this.btnAnalyze.Style = Sunny.UI.UIStyle.Custom;
            this.btnAnalyze.Symbol = 61450;
            this.btnAnalyze.TabIndex = 3;
            this.btnAnalyze.Text = "AI 分析故障";
            this.btnAnalyze.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAnalyze.Click += new System.EventHandler(this.BtnAnalyze_Click);
            // 
            // picFault
            // 
            this.picFault.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.picFault.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picFault.Location = new System.Drawing.Point(17, 76);
            this.picFault.Name = "picFault";
            this.picFault.Size = new System.Drawing.Size(240, 150);
            this.picFault.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picFault.TabIndex = 4;
            this.picFault.TabStop = false;
            // 
            // lblFaultDesc
            // 
            this.lblFaultDesc.AutoSize = true;
            this.lblFaultDesc.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblFaultDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.lblFaultDesc.Location = new System.Drawing.Point(14, 50);
            this.lblFaultDesc.Name = "lblFaultDesc";
            this.lblFaultDesc.Size = new System.Drawing.Size(82, 24);
            this.lblFaultDesc.TabIndex = 5;
            this.lblFaultDesc.Text = "故障描述";
            // 
            // lblEquipment
            // 
            this.lblEquipment.AutoSize = true;
            this.lblEquipment.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.lblEquipment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.lblEquipment.Location = new System.Drawing.Point(14, 24);
            this.lblEquipment.Name = "lblEquipment";
            this.lblEquipment.Size = new System.Drawing.Size(92, 27);
            this.lblEquipment.TabIndex = 6;
            this.lblEquipment.Text = "设备名称";
            // 
            // grpMaterials
            // 
            this.grpMaterials.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.grpMaterials.Controls.Add(this.lblTotalAmount);
            this.grpMaterials.Controls.Add(this.lblManualAmount);
            this.grpMaterials.Controls.Add(this.lblAiAmount);
            this.grpMaterials.Controls.Add(this.btnAddToGrid);
            this.grpMaterials.Controls.Add(this.txtManualQty);
            this.grpMaterials.Controls.Add(this.cmbMaterialSelect);
            this.grpMaterials.Controls.Add(this.dgvMaterials);
            this.grpMaterials.Controls.Add(this.btnConfirm);
            this.grpMaterials.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.grpMaterials.Location = new System.Drawing.Point(12, 292);
            this.grpMaterials.Name = "grpMaterials";
            this.grpMaterials.Size = new System.Drawing.Size(740, 280);
            this.grpMaterials.TabIndex = 0;
            this.grpMaterials.TabStop = false;
            this.grpMaterials.Text = "维修物料";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotalAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblTotalAmount.Location = new System.Drawing.Point(380, 231);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(163, 30);
            this.lblTotalAmount.TabIndex = 0;
            this.lblTotalAmount.Text = "总金额：¥0.00";
            // 
            // lblManualAmount
            // 
            this.lblManualAmount.AutoSize = true;
            this.lblManualAmount.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblManualAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.lblManualAmount.Location = new System.Drawing.Point(200, 232);
            this.lblManualAmount.Name = "lblManualAmount";
            this.lblManualAmount.Size = new System.Drawing.Size(148, 24);
            this.lblManualAmount.TabIndex = 1;
            this.lblManualAmount.Text = "手动添加：¥0.00";
            // 
            // lblAiAmount
            // 
            this.lblAiAmount.AutoSize = true;
            this.lblAiAmount.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblAiAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.lblAiAmount.Location = new System.Drawing.Point(17, 232);
            this.lblAiAmount.Name = "lblAiAmount";
            this.lblAiAmount.Size = new System.Drawing.Size(130, 24);
            this.lblAiAmount.TabIndex = 2;
            this.lblAiAmount.Text = "AI推荐：¥0.00";
            // 
            // btnAddToGrid
            // 
            this.btnAddToGrid.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddToGrid.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.btnAddToGrid.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnAddToGrid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.btnAddToGrid.Location = new System.Drawing.Point(260, 198);
            this.btnAddToGrid.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnAddToGrid.Name = "btnAddToGrid";
            this.btnAddToGrid.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.btnAddToGrid.Size = new System.Drawing.Size(65, 28);
            this.btnAddToGrid.Style = Sunny.UI.UIStyle.Custom;
            this.btnAddToGrid.TabIndex = 3;
            this.btnAddToGrid.Text = "添加";
            this.btnAddToGrid.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // txtManualQty
            // 
            this.txtManualQty.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtManualQty.DoubleValue = 1D;
            this.txtManualQty.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.txtManualQty.IntValue = 1;
            this.txtManualQty.Location = new System.Drawing.Point(199, 198);
            this.txtManualQty.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtManualQty.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtManualQty.Name = "txtManualQty";
            this.txtManualQty.Padding = new System.Windows.Forms.Padding(5);
            this.txtManualQty.ShowText = false;
            this.txtManualQty.Size = new System.Drawing.Size(55, 28);
            this.txtManualQty.TabIndex = 4;
            this.txtManualQty.Text = "1";
            this.txtManualQty.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtManualQty.Watermark = "数量";
            // 
            // cmbMaterialSelect
            // 
            this.cmbMaterialSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMaterialSelect.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.cmbMaterialSelect.Location = new System.Drawing.Point(17, 198);
            this.cmbMaterialSelect.Name = "cmbMaterialSelect";
            this.cmbMaterialSelect.Size = new System.Drawing.Size(175, 28);
            this.cmbMaterialSelect.TabIndex = 5;
            // 
            // dgvMaterials
            // 
            this.dgvMaterials.AllowUserToAddRows = false;
            this.dgvMaterials.AllowUserToDeleteRows = false;
            this.dgvMaterials.AllowUserToResizeRows = false;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.dgvMaterials.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvMaterials.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.dgvMaterials.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("微软雅黑", 9F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.dgvMaterials.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvMaterials.ColumnHeadersHeight = 30;
            this.dgvMaterials.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.colMatName,
            this.colQuantity,
            this.colMatUnit,
            this.colUnitPrice,
            this.colSubtotal,
            this.colMatId,
            this.colIsManual});
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle12.Font = new System.Drawing.Font("微软雅黑", 9F);
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMaterials.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgvMaterials.EnableHeadersVisualStyles = false;
            this.dgvMaterials.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvMaterials.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.dgvMaterials.Location = new System.Drawing.Point(17, 24);
            this.dgvMaterials.MultiSelect = false;
            this.dgvMaterials.Name = "dgvMaterials";
            this.dgvMaterials.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle13.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMaterials.RowHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dgvMaterials.RowHeadersVisible = false;
            this.dgvMaterials.RowHeadersWidth = 62;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.dgvMaterials.RowsDefaultCellStyle = dataGridViewCellStyle14;
            this.dgvMaterials.RowTemplate.Height = 28;
            this.dgvMaterials.SelectedIndex = -1;
            this.dgvMaterials.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMaterials.Size = new System.Drawing.Size(705, 170);
            this.dgvMaterials.StripeOddColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.dgvMaterials.TabIndex = 6;
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "选择";
            this.colCheck.MinimumWidth = 8;
            this.colCheck.Name = "colCheck";
            this.colCheck.Width = 45;
            // 
            // colMatName
            // 
            this.colMatName.HeaderText = "物料名称";
            this.colMatName.MinimumWidth = 8;
            this.colMatName.Name = "colMatName";
            this.colMatName.ReadOnly = true;
            this.colMatName.Width = 160;
            // 
            // colQuantity
            // 
            this.colQuantity.HeaderText = "数量";
            this.colQuantity.MinimumWidth = 8;
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.Width = 60;
            // 
            // colMatUnit
            // 
            this.colMatUnit.HeaderText = "单位";
            this.colMatUnit.MinimumWidth = 8;
            this.colMatUnit.Name = "colMatUnit";
            this.colMatUnit.ReadOnly = true;
            this.colMatUnit.Width = 60;
            // 
            // colUnitPrice
            // 
            dataGridViewCellStyle10.Format = "¥0.00";
            this.colUnitPrice.DefaultCellStyle = dataGridViewCellStyle10;
            this.colUnitPrice.HeaderText = "单价";
            this.colUnitPrice.MinimumWidth = 8;
            this.colUnitPrice.Name = "colUnitPrice";
            this.colUnitPrice.ReadOnly = true;
            this.colUnitPrice.Width = 80;
            // 
            // colSubtotal
            // 
            dataGridViewCellStyle11.Format = "¥0.00";
            this.colSubtotal.DefaultCellStyle = dataGridViewCellStyle11;
            this.colSubtotal.HeaderText = "小计";
            this.colSubtotal.MinimumWidth = 8;
            this.colSubtotal.Name = "colSubtotal";
            this.colSubtotal.ReadOnly = true;
            this.colSubtotal.Width = 80;
            // 
            // colMatId
            // 
            this.colMatId.HeaderText = "MaterialId";
            this.colMatId.MinimumWidth = 8;
            this.colMatId.Name = "colMatId";
            this.colMatId.Visible = false;
            this.colMatId.Width = 150;
            // 
            // colIsManual
            // 
            this.colIsManual.HeaderText = "IsManual";
            this.colIsManual.MinimumWidth = 8;
            this.colIsManual.Name = "colIsManual";
            this.colIsManual.Visible = false;
            this.colIsManual.Width = 150;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirm.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
            this.btnConfirm.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnConfirm.Location = new System.Drawing.Point(610, 243);
            this.btnConfirm.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.btnConfirm.Size = new System.Drawing.Size(112, 30);
            this.btnConfirm.Style = Sunny.UI.UIStyle.Custom;
            this.btnConfirm.Symbol = 61452;
            this.btnConfirm.TabIndex = 7;
            this.btnConfirm.Text = "确认物料清单";
            this.btnConfirm.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // AgentForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(770, 590);
            this.Controls.Add(this.grpMaterials);
            this.Controls.Add(this.grpAi);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(770, 590);
            this.MinimumSize = new System.Drawing.Size(770, 590);
            this.Name = "AgentForm";
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "AI 辅助维修（Agent）";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.TitleFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.ZoomScaleRect = new System.Drawing.Rectangle(22, 22, 770, 590);
            this.grpAi.ResumeLayout(false);
            this.grpAi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFault)).EndInit();
            this.grpMaterials.ResumeLayout(false);
            this.grpMaterials.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterials)).EndInit();
            this.ResumeLayout(false);

        }
    }
}

