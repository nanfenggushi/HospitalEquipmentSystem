namespace HospitalEquipmentSystem.UI
{
    partial class DeptRevenueForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlEdit = new System.Windows.Forms.Panel();
            this.btnCancel = new Sunny.UI.UIButton();
            this.btnDelete = new Sunny.UI.UIButton();
            this.btnSave = new Sunny.UI.UIButton();
            this.uiTextBoxRemark = new Sunny.UI.UITextBox();
            this.uiLabelRemark = new Sunny.UI.UILabel();
            this.uiTextBoxAmount = new Sunny.UI.UITextBox();
            this.uiLabelAmount = new Sunny.UI.UILabel();
            this.uiComboBoxDept = new Sunny.UI.UIComboBox();
            this.uiLabelDept = new Sunny.UI.UILabel();
            this.uiTextBoxPeriod = new Sunny.UI.UITextBox();
            this.uiLabelPeriod = new Sunny.UI.UILabel();
            this.uiDataGridView1 = new Sunny.UI.UIDataGridView();
            this.colPeriod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDept = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreatedAt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uiPanelFilter = new Sunny.UI.UIPanel();
            this.uiLabelTip = new Sunny.UI.UILabel();
            this.btnQuery = new Sunny.UI.UIButton();
            this.uiComboBoxPeriod = new Sunny.UI.UIComboBox();
            this.uiTitlePanel1 = new Sunny.UI.UITitlePanel();
            this.btnAdd = new Sunny.UI.UIButton();
            this.lblSummary = new Sunny.UI.UILabel();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.pnlEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiDataGridView1)).BeginInit();
            this.uiPanelFilter.SuspendLayout();
            this.uiTitlePanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlEdit
            // 
            this.pnlEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.pnlEdit.Controls.Add(this.btnCancel);
            this.pnlEdit.Controls.Add(this.btnDelete);
            this.pnlEdit.Controls.Add(this.btnSave);
            this.pnlEdit.Controls.Add(this.uiTextBoxRemark);
            this.pnlEdit.Controls.Add(this.uiLabelRemark);
            this.pnlEdit.Controls.Add(this.uiTextBoxAmount);
            this.pnlEdit.Controls.Add(this.uiLabelAmount);
            this.pnlEdit.Controls.Add(this.uiComboBoxDept);
            this.pnlEdit.Controls.Add(this.uiLabelDept);
            this.pnlEdit.Controls.Add(this.uiTextBoxPeriod);
            this.pnlEdit.Controls.Add(this.uiLabelPeriod);
            this.pnlEdit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlEdit.Location = new System.Drawing.Point(0, 656);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Size = new System.Drawing.Size(1241, 110);
            this.pnlEdit.TabIndex = 40;
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.btnCancel.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.btnCancel.Location = new System.Drawing.Point(1075, 30);
            this.btnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.btnCancel.Size = new System.Drawing.Size(85, 40);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "取消";
            this.btnCancel.TipsFont = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnDelete
            // 
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnDelete.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(985, 30);
            this.btnDelete.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(85, 40);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "删除";
            this.btnDelete.TipsFont = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
            this.btnSave.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(895, 30);
            this.btnSave.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(85, 40);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "保存";
            this.btnSave.TipsFont = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // uiTextBoxRemark
            // 
            this.uiTextBoxRemark.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.uiTextBoxRemark.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.uiTextBoxRemark.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiTextBoxRemark.ForeColor = System.Drawing.Color.White;
            this.uiTextBoxRemark.Location = new System.Drawing.Point(670, 32);
            this.uiTextBoxRemark.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTextBoxRemark.MinimumSize = new System.Drawing.Size(1, 16);
            this.uiTextBoxRemark.Name = "uiTextBoxRemark";
            this.uiTextBoxRemark.Padding = new System.Windows.Forms.Padding(5);
            this.uiTextBoxRemark.ShowText = false;
            this.uiTextBoxRemark.Size = new System.Drawing.Size(200, 36);
            this.uiTextBoxRemark.TabIndex = 6;
            this.uiTextBoxRemark.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiTextBoxRemark.Watermark = "备注(选填)";
            this.uiTextBoxRemark.WatermarkActiveColor = System.Drawing.Color.White;
            // 
            // uiLabelRemark
            // 
            this.uiLabelRemark.BackColor = System.Drawing.Color.Transparent;
            this.uiLabelRemark.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.uiLabelRemark.ForeColor = System.Drawing.Color.White;
            this.uiLabelRemark.Location = new System.Drawing.Point(620, 40);
            this.uiLabelRemark.Name = "uiLabelRemark";
            this.uiLabelRemark.Size = new System.Drawing.Size(50, 32);
            this.uiLabelRemark.TabIndex = 5;
            this.uiLabelRemark.Text = "备注";
            // 
            // uiTextBoxAmount
            // 
            this.uiTextBoxAmount.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.uiTextBoxAmount.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.uiTextBoxAmount.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiTextBoxAmount.ForeColor = System.Drawing.Color.White;
            this.uiTextBoxAmount.Location = new System.Drawing.Point(480, 32);
            this.uiTextBoxAmount.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTextBoxAmount.MinimumSize = new System.Drawing.Size(1, 16);
            this.uiTextBoxAmount.Name = "uiTextBoxAmount";
            this.uiTextBoxAmount.Padding = new System.Windows.Forms.Padding(5);
            this.uiTextBoxAmount.ShowText = false;
            this.uiTextBoxAmount.Size = new System.Drawing.Size(120, 36);
            this.uiTextBoxAmount.TabIndex = 4;
            this.uiTextBoxAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiTextBoxAmount.Watermark = "0.00";
            this.uiTextBoxAmount.WatermarkActiveColor = System.Drawing.Color.White;
            // 
            // uiLabelAmount
            // 
            this.uiLabelAmount.BackColor = System.Drawing.Color.Transparent;
            this.uiLabelAmount.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.uiLabelAmount.ForeColor = System.Drawing.Color.White;
            this.uiLabelAmount.Location = new System.Drawing.Point(420, 40);
            this.uiLabelAmount.Name = "uiLabelAmount";
            this.uiLabelAmount.Size = new System.Drawing.Size(60, 32);
            this.uiLabelAmount.TabIndex = 3;
            this.uiLabelAmount.Text = "金额(元)";
            // 
            // uiComboBoxDept
            // 
            this.uiComboBoxDept.DataSource = null;
            this.uiComboBoxDept.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.uiComboBoxDept.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiComboBoxDept.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.uiComboBoxDept.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiComboBoxDept.Location = new System.Drawing.Point(250, 32);
            this.uiComboBoxDept.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiComboBoxDept.MinimumSize = new System.Drawing.Size(63, 0);
            this.uiComboBoxDept.Name = "uiComboBoxDept";
            this.uiComboBoxDept.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.uiComboBoxDept.Size = new System.Drawing.Size(150, 36);
            this.uiComboBoxDept.SymbolSize = 24;
            this.uiComboBoxDept.TabIndex = 2;
            this.uiComboBoxDept.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiComboBoxDept.Watermark = "请选择科室";
            this.uiComboBoxDept.WatermarkActiveColor = System.Drawing.Color.White;
            // 
            // uiLabelDept
            // 
            this.uiLabelDept.BackColor = System.Drawing.Color.Transparent;
            this.uiLabelDept.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.uiLabelDept.ForeColor = System.Drawing.Color.White;
            this.uiLabelDept.Location = new System.Drawing.Point(200, 40);
            this.uiLabelDept.Name = "uiLabelDept";
            this.uiLabelDept.Size = new System.Drawing.Size(50, 32);
            this.uiLabelDept.TabIndex = 1;
            this.uiLabelDept.Text = "科室";
            // 
            // uiTextBoxPeriod
            // 
            this.uiTextBoxPeriod.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.uiTextBoxPeriod.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.uiTextBoxPeriod.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiTextBoxPeriod.ForeColor = System.Drawing.Color.White;
            this.uiTextBoxPeriod.Location = new System.Drawing.Point(75, 32);
            this.uiTextBoxPeriod.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTextBoxPeriod.MinimumSize = new System.Drawing.Size(1, 16);
            this.uiTextBoxPeriod.Name = "uiTextBoxPeriod";
            this.uiTextBoxPeriod.Padding = new System.Windows.Forms.Padding(5);
            this.uiTextBoxPeriod.ShowText = false;
            this.uiTextBoxPeriod.Size = new System.Drawing.Size(105, 36);
            this.uiTextBoxPeriod.TabIndex = 0;
            this.uiTextBoxPeriod.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiTextBoxPeriod.Watermark = "yyyy-MM";
            this.uiTextBoxPeriod.WatermarkActiveColor = System.Drawing.Color.White;
            // 
            // uiLabelPeriod
            // 
            this.uiLabelPeriod.BackColor = System.Drawing.Color.Transparent;
            this.uiLabelPeriod.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.uiLabelPeriod.ForeColor = System.Drawing.Color.White;
            this.uiLabelPeriod.Location = new System.Drawing.Point(25, 40);
            this.uiLabelPeriod.Name = "uiLabelPeriod";
            this.uiLabelPeriod.Size = new System.Drawing.Size(50, 32);
            this.uiLabelPeriod.TabIndex = 7;
            this.uiLabelPeriod.Text = "期间";
            // 
            // uiDataGridView1
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(225)))), ((int)(((byte)(245)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.uiDataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.uiDataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.uiDataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.uiDataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.uiDataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.uiDataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.uiDataGridView1.ColumnHeadersHeight = 32;
            this.uiDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.uiDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPeriod,
            this.colDept,
            this.colAmount,
            this.colRemark,
            this.colCreatedAt});
            this.uiDataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiDataGridView1.EnableHeadersVisualStyles = false;
            this.uiDataGridView1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiDataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.uiDataGridView1.Location = new System.Drawing.Point(0, 178);
            this.uiDataGridView1.Name = "uiDataGridView1";
            this.uiDataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(225)))), ((int)(((byte)(245)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            this.uiDataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.uiDataGridView1.RowHeadersVisible = false;
            this.uiDataGridView1.RowHeadersWidth = 62;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(225)))), ((int)(((byte)(245)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.uiDataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.uiDataGridView1.RowTemplate.Height = 38;
            this.uiDataGridView1.SelectedIndex = -1;
            this.uiDataGridView1.Size = new System.Drawing.Size(1241, 478);
            this.uiDataGridView1.StripeEvenColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.uiDataGridView1.StripeOddColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.uiDataGridView1.TabIndex = 38;
            // 
            // colPeriod
            // 
            this.colPeriod.HeaderText = "期间";
            this.colPeriod.MinimumWidth = 6;
            this.colPeriod.Name = "colPeriod";
            this.colPeriod.ReadOnly = true;
            this.colPeriod.Width = 110;
            // 
            // colDept
            // 
            this.colDept.HeaderText = "科室";
            this.colDept.MinimumWidth = 6;
            this.colDept.Name = "colDept";
            this.colDept.ReadOnly = true;
            this.colDept.Width = 180;
            // 
            // colAmount
            // 
            this.colAmount.HeaderText = "金额(元)";
            this.colAmount.MinimumWidth = 6;
            this.colAmount.Name = "colAmount";
            this.colAmount.ReadOnly = true;
            this.colAmount.Width = 150;
            // 
            // colRemark
            // 
            this.colRemark.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colRemark.FillWeight = 60F;
            this.colRemark.HeaderText = "备注";
            this.colRemark.MinimumWidth = 150;
            this.colRemark.Name = "colRemark";
            this.colRemark.ReadOnly = true;
            // 
            // colCreatedAt
            // 
            this.colCreatedAt.HeaderText = "创建时间";
            this.colCreatedAt.MinimumWidth = 6;
            this.colCreatedAt.Name = "colCreatedAt";
            this.colCreatedAt.ReadOnly = true;
            this.colCreatedAt.Width = 180;
            // 
            // uiPanelFilter
            // 
            this.uiPanelFilter.BackColor = System.Drawing.Color.Transparent;
            this.uiPanelFilter.Controls.Add(this.uiLabelTip);
            this.uiPanelFilter.Controls.Add(this.btnQuery);
            this.uiPanelFilter.Controls.Add(this.uiComboBoxPeriod);
            this.uiPanelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiPanelFilter.FillColor = System.Drawing.Color.Transparent;
            this.uiPanelFilter.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanelFilter.Location = new System.Drawing.Point(0, 117);
            this.uiPanelFilter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanelFilter.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanelFilter.Name = "uiPanelFilter";
            this.uiPanelFilter.Size = new System.Drawing.Size(1241, 61);
            this.uiPanelFilter.TabIndex = 37;
            this.uiPanelFilter.Text = null;
            this.uiPanelFilter.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiLabelTip
            // 
            this.uiLabelTip.BackColor = System.Drawing.Color.Transparent;
            this.uiLabelTip.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.uiLabelTip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.uiLabelTip.Location = new System.Drawing.Point(330, 10);
            this.uiLabelTip.Name = "uiLabelTip";
            this.uiLabelTip.Size = new System.Drawing.Size(600, 40);
            this.uiLabelTip.TabIndex = 2;
            this.uiLabelTip.Text = "选择期间查询各科室收入，未选择时默认最新期间；点击表格行可修改。";
            // 
            // btnQuery
            // 
            this.btnQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuery.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
            this.btnQuery.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuery.Location = new System.Drawing.Point(220, 10);
            this.btnQuery.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(88, 40);
            this.btnQuery.TabIndex = 1;
            this.btnQuery.Text = "查询";
            this.btnQuery.TipsFont = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // uiComboBoxPeriod
            // 
            this.uiComboBoxPeriod.DataSource = null;
            this.uiComboBoxPeriod.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.uiComboBoxPeriod.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiComboBoxPeriod.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.uiComboBoxPeriod.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiComboBoxPeriod.Location = new System.Drawing.Point(30, 10);
            this.uiComboBoxPeriod.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiComboBoxPeriod.MinimumSize = new System.Drawing.Size(63, 0);
            this.uiComboBoxPeriod.Name = "uiComboBoxPeriod";
            this.uiComboBoxPeriod.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.uiComboBoxPeriod.Size = new System.Drawing.Size(180, 40);
            this.uiComboBoxPeriod.SymbolSize = 24;
            this.uiComboBoxPeriod.TabIndex = 0;
            this.uiComboBoxPeriod.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiComboBoxPeriod.Watermark = "选择期间(默认最新)";
            this.uiComboBoxPeriod.WatermarkActiveColor = System.Drawing.Color.White;
            // 
            // uiTitlePanel1
            // 
            this.uiTitlePanel1.Controls.Add(this.btnAdd);
            this.uiTitlePanel1.Controls.Add(this.lblSummary);
            this.uiTitlePanel1.Controls.Add(this.uiLabel1);
            this.uiTitlePanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiTitlePanel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.uiTitlePanel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiTitlePanel1.Location = new System.Drawing.Point(0, 0);
            this.uiTitlePanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTitlePanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTitlePanel1.Name = "uiTitlePanel1";
            this.uiTitlePanel1.Padding = new System.Windows.Forms.Padding(1, 35, 1, 1);
            this.uiTitlePanel1.Radius = 10;
            this.uiTitlePanel1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.uiTitlePanel1.ShowText = false;
            this.uiTitlePanel1.Size = new System.Drawing.Size(1241, 117);
            this.uiTitlePanel1.TabIndex = 36;
            this.uiTitlePanel1.Text = "科室收入对比";
            this.uiTitlePanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiTitlePanel1.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.uiTitlePanel1.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
            this.btnAdd.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(1125, 40);
            this.btnAdd.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
            this.btnAdd.Size = new System.Drawing.Size(100, 40);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "+新增";
            this.btnAdd.TipsFont = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblSummary
            // 
            this.lblSummary.BackColor = System.Drawing.Color.Transparent;
            this.lblSummary.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblSummary.ForeColor = System.Drawing.Color.White;
            this.lblSummary.Location = new System.Drawing.Point(260, 40);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(360, 40);
            this.lblSummary.TabIndex = 1;
            this.lblSummary.Text = "共 0 个科室，合计 ¥0.00";
            // 
            // uiLabel1
            // 
            this.uiLabel1.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold);
            this.uiLabel1.ForeColor = System.Drawing.Color.White;
            this.uiLabel1.Location = new System.Drawing.Point(30, 36);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(220, 44);
            this.uiLabel1.TabIndex = 0;
            this.uiLabel1.Text = "科室收入对比";
            // 
            // DeptRevenueForm
            // 
            this.AllowShowTitle = false;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(1241, 766);
            this.Controls.Add(this.uiDataGridView1);
            this.Controls.Add(this.uiPanelFilter);
            this.Controls.Add(this.uiTitlePanel1);
            this.Controls.Add(this.pnlEdit);
            this.Name = "DeptRevenueForm";
            this.Padding = new System.Windows.Forms.Padding(0);
            this.ShowTitle = false;
            this.Text = "DeptRevenueForm";
            this.ZoomScaleRect = new System.Drawing.Rectangle(19, 19, 800, 450);
            this.pnlEdit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiDataGridView1)).EndInit();
            this.uiPanelFilter.ResumeLayout(false);
            this.uiTitlePanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlEdit;
        private Sunny.UI.UIButton btnCancel;
        private Sunny.UI.UIButton btnDelete;
        private Sunny.UI.UIButton btnSave;
        private Sunny.UI.UITextBox uiTextBoxRemark;
        private Sunny.UI.UILabel uiLabelRemark;
        private Sunny.UI.UITextBox uiTextBoxAmount;
        private Sunny.UI.UILabel uiLabelAmount;
        private Sunny.UI.UIComboBox uiComboBoxDept;
        private Sunny.UI.UILabel uiLabelDept;
        private Sunny.UI.UITextBox uiTextBoxPeriod;
        private Sunny.UI.UILabel uiLabelPeriod;
        private Sunny.UI.UIDataGridView uiDataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPeriod;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDept;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRemark;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCreatedAt;
        private Sunny.UI.UIPanel uiPanelFilter;
        private Sunny.UI.UILabel uiLabelTip;
        private Sunny.UI.UIButton btnQuery;
        private Sunny.UI.UIComboBox uiComboBoxPeriod;
        private Sunny.UI.UITitlePanel uiTitlePanel1;
        private Sunny.UI.UIButton btnAdd;
        private Sunny.UI.UILabel lblSummary;
        private Sunny.UI.UILabel uiLabel1;
    }
}
