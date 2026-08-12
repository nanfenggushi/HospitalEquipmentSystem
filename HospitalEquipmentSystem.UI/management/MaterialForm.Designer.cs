namespace HospitalEquipmentSystem.UI.management
{
    partial class MaterialForm
    {
        private System.ComponentModel.IContainer components = null;

        private Sunny.UI.UIPanel pnlSearch;
        private Sunny.UI.UITextBox txtSearchName;
        private Sunny.UI.UIComboBox cmbCategory;
        private Sunny.UI.UISymbolButton btnSearch;
        private Sunny.UI.UISymbolButton btnAdd;
        private Sunny.UI.UIDataGridView dgvMaterials;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFaultType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActive;
        private System.Windows.Forms.DataGridViewButtonColumn colEdit;
        private System.Windows.Forms.DataGridViewButtonColumn colToggle;
        private System.Windows.Forms.Panel pnlPager;
        private Sunny.UI.UISymbolButton btnPrevPage;
        private Sunny.UI.UILabel lblPageInfo;
        private Sunny.UI.UISymbolButton btnNextPage;
        private Sunny.UI.UILabel lblSearch;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlSearch = new Sunny.UI.UIPanel();
            this.lblSearch = new Sunny.UI.UILabel();
            this.txtSearchName = new Sunny.UI.UITextBox();
            this.cmbCategory = new Sunny.UI.UIComboBox();
            this.btnSearch = new Sunny.UI.UISymbolButton();
            this.btnAdd = new Sunny.UI.UISymbolButton();
            this.dgvMaterials = new Sunny.UI.UIDataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFaultType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colToggle = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pnlPager = new System.Windows.Forms.Panel();
            this.btnPrevPage = new Sunny.UI.UISymbolButton();
            this.lblPageInfo = new Sunny.UI.UILabel();
            this.btnNextPage = new Sunny.UI.UISymbolButton();
            this.pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterials)).BeginInit();
            this.pnlPager.SuspendLayout();
            this.SuspendLayout();

            this.pnlSearch.Controls.Add(this.lblSearch);
            this.pnlSearch.Controls.Add(this.txtSearchName);
            this.pnlSearch.Controls.Add(this.cmbCategory);
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Controls.Add(this.btnAdd);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.FillColor = System.Drawing.Color.FromArgb(19, 35, 58);
            this.pnlSearch.RectColor = System.Drawing.Color.FromArgb(30, 58, 138);
            this.pnlSearch.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.pnlSearch.Location = new System.Drawing.Point(0, 0);
            this.pnlSearch.Size = new System.Drawing.Size(1040, 60);
            this.pnlSearch.Style = Sunny.UI.UIStyle.Custom;

            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(230, 238, 247);
            this.lblSearch.Location = new System.Drawing.Point(20, 20);
            this.lblSearch.Text = "物料名称：";

            this.txtSearchName.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txtSearchName.Location = new System.Drawing.Point(120, 16);
            this.txtSearchName.Size = new System.Drawing.Size(200, 29);

            this.cmbCategory.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.cmbCategory.Location = new System.Drawing.Point(340, 16);
            this.cmbCategory.Size = new System.Drawing.Size(160, 29);

            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnSearch.Location = new System.Drawing.Point(520, 16);
            this.btnSearch.Size = new System.Drawing.Size(80, 29);
            this.btnSearch.Text = "搜索";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FillColor = System.Drawing.Color.FromArgb(27, 111, 214);
            this.btnAdd.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(940, 16);
            this.btnAdd.Size = new System.Drawing.Size(80, 29);
            this.btnAdd.Style = Sunny.UI.UIStyle.Custom;
            this.btnAdd.Symbol = 61543;
            this.btnAdd.Text = "新增";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.dgvMaterials.AllowUserToAddRows = false;
            this.dgvMaterials.AllowUserToDeleteRows = false;
            this.dgvMaterials.AllowUserToResizeRows = false;
            this.dgvMaterials.BackgroundColor = System.Drawing.Color.FromArgb(11, 22, 34);
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(19, 35, 58);
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 12F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(230, 238, 247);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(30, 58, 138);
            this.dgvMaterials.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMaterials.ColumnHeadersHeight = 36;
            this.dgvMaterials.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colId, this.colName, this.colFaultType, this.colPrice, this.colQty, this.colUnit,
                this.colActive, this.colEdit, this.colToggle
            });
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(11, 22, 34);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(230, 238, 247);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(30, 58, 138);
            this.dgvMaterials.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMaterials.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMaterials.GridColor = System.Drawing.Color.FromArgb(30, 58, 138);
            this.dgvMaterials.Location = new System.Drawing.Point(0, 60);
            this.dgvMaterials.MultiSelect = false;
            this.dgvMaterials.ReadOnly = true;
            this.dgvMaterials.RowHeadersVisible = false;
            this.dgvMaterials.RowTemplate.Height = 30;
            this.dgvMaterials.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMaterials.Size = new System.Drawing.Size(1040, 416);
            this.dgvMaterials.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMaterials_CellContentClick);

            this.colId.HeaderText = "ID";
            this.colId.Name = "colId";
            this.colId.Width = 50;

            this.colName.HeaderText = "物料名称";
            this.colName.Name = "colName";
            this.colName.Width = 150;

            this.colFaultType.HeaderText = "故障类型";
            this.colFaultType.Name = "colFaultType";
            this.colFaultType.Width = 120;

            this.colPrice.HeaderText = "单价";
            this.colPrice.Name = "colPrice";
            this.colPrice.Width = 90;
            this.colPrice.DefaultCellStyle.Format = "¥0.00";

            this.colQty.HeaderText = "默认数量";
            this.colQty.Name = "colQty";
            this.colQty.Width = 90;

            this.colUnit.HeaderText = "单位";
            this.colUnit.Name = "colUnit";
            this.colUnit.Width = 70;

            this.colActive.HeaderText = "状态";
            this.colActive.Name = "colActive";
            this.colActive.Width = 70;

            this.colEdit.HeaderText = "操作";
            this.colEdit.Name = "colEdit";
            this.colEdit.Text = "编辑";
            this.colEdit.UseColumnTextForButtonValue = true;
            this.colEdit.Width = 70;

            this.colToggle.HeaderText = "启用/禁用";
            this.colToggle.Name = "colToggle";
            this.colToggle.Text = "切换";
            this.colToggle.UseColumnTextForButtonValue = true;
            this.colToggle.Width = 70;

            this.pnlPager.BackColor = System.Drawing.Color.FromArgb(19, 35, 58);
            this.pnlPager.Controls.Add(this.btnPrevPage);
            this.pnlPager.Controls.Add(this.lblPageInfo);
            this.pnlPager.Controls.Add(this.btnNextPage);
            this.pnlPager.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPager.Location = new System.Drawing.Point(0, 476);
            this.pnlPager.Size = new System.Drawing.Size(1040, 40);

            this.btnPrevPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrevPage.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnPrevPage.Location = new System.Drawing.Point(420, 6);
            this.btnPrevPage.Size = new System.Drawing.Size(80, 28);
            this.btnPrevPage.Symbol = 61540;
            this.btnPrevPage.Text = "上一页";

            this.lblPageInfo.AutoSize = true;
            this.lblPageInfo.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblPageInfo.ForeColor = System.Drawing.Color.FromArgb(159, 179, 200);
            this.lblPageInfo.Location = new System.Drawing.Point(510, 11);
            this.lblPageInfo.Text = "第 1/1 页 · 每页 6 条 · 共 0 条";

            this.btnNextPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNextPage.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnNextPage.Location = new System.Drawing.Point(750, 6);
            this.btnNextPage.Size = new System.Drawing.Size(80, 28);
            this.btnNextPage.Symbol = 61541;
            this.btnNextPage.Text = "下一页";

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 516);
            this.Controls.Add(this.dgvMaterials);
            this.Controls.Add(this.pnlPager);
            this.Controls.Add(this.pnlSearch);
            this.Name = "MaterialForm";
            this.Text = "物料管理";
            this.Load += new System.EventHandler(this.MaterialForm_Load);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterials)).EndInit();
            this.pnlPager.ResumeLayout(false);
            this.pnlPager.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
