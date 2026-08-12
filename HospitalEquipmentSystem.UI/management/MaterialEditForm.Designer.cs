namespace HospitalEquipmentSystem.UI.management
{
    partial class MaterialEditForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblFaultType;
        private System.Windows.Forms.ComboBox cmbFaultType;
        private System.Windows.Forms.Label lblUnitPrice;
        private System.Windows.Forms.TextBox txtUnitPrice;
        private System.Windows.Forms.Label lblDefaultQuantity;
        private System.Windows.Forms.TextBox txtDefaultQuantity;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Label lblTitle;

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
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblFaultType = new System.Windows.Forms.Label();
            this.cmbFaultType = new System.Windows.Forms.ComboBox();
            this.lblUnitPrice = new System.Windows.Forms.Label();
            this.txtUnitPrice = new System.Windows.Forms.TextBox();
            this.lblDefaultQuantity = new System.Windows.Forms.Label();
            this.txtDefaultQuantity = new System.Windows.Forms.TextBox();
            this.lblUnit = new System.Windows.Forms.Label();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlTitle.SuspendLayout();
            this.SuspendLayout();

            this.pnlTitle.BackColor = System.Drawing.Color.FromArgb(19, 35, 58);
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Size = new System.Drawing.Size(460, 36);

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(230, 238, 247);
            this.lblTitle.Location = new System.Drawing.Point(14, 7);
            this.lblTitle.Text = "物料信息";

            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblName.ForeColor = System.Drawing.Color.FromArgb(230, 238, 247);
            this.lblName.Location = new System.Drawing.Point(30, 55);
            this.lblName.Text = "物料名称：";

            this.txtName.BackColor = System.Drawing.Color.FromArgb(19, 35, 58);
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtName.ForeColor = System.Drawing.Color.FromArgb(230, 238, 247);
            this.txtName.Location = new System.Drawing.Point(130, 52);
            this.txtName.Size = new System.Drawing.Size(300, 25);

            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblCategory.ForeColor = System.Drawing.Color.FromArgb(230, 238, 247);
            this.lblCategory.Location = new System.Drawing.Point(30, 95);
            this.lblCategory.Text = "设备分类：";

            this.cmbCategory.BackColor = System.Drawing.Color.FromArgb(19, 35, 58);
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCategory.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.cmbCategory.ForeColor = System.Drawing.Color.FromArgb(230, 238, 247);
            this.cmbCategory.Location = new System.Drawing.Point(130, 92);
            this.cmbCategory.Size = new System.Drawing.Size(300, 25);

            this.lblFaultType.AutoSize = true;
            this.lblFaultType.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblFaultType.ForeColor = System.Drawing.Color.FromArgb(230, 238, 247);
            this.lblFaultType.Location = new System.Drawing.Point(30, 135);
            this.lblFaultType.Text = "故障类型：";

            this.cmbFaultType.BackColor = System.Drawing.Color.FromArgb(19, 35, 58);
            this.cmbFaultType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFaultType.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.cmbFaultType.ForeColor = System.Drawing.Color.FromArgb(230, 238, 247);
            this.cmbFaultType.Location = new System.Drawing.Point(130, 132);
            this.cmbFaultType.Size = new System.Drawing.Size(300, 25);

            this.lblUnitPrice.AutoSize = true;
            this.lblUnitPrice.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblUnitPrice.ForeColor = System.Drawing.Color.FromArgb(230, 238, 247);
            this.lblUnitPrice.Location = new System.Drawing.Point(30, 175);
            this.lblUnitPrice.Text = "单价（元）：";

            this.txtUnitPrice.BackColor = System.Drawing.Color.FromArgb(19, 35, 58);
            this.txtUnitPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUnitPrice.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtUnitPrice.ForeColor = System.Drawing.Color.FromArgb(230, 238, 247);
            this.txtUnitPrice.Location = new System.Drawing.Point(130, 172);
            this.txtUnitPrice.Size = new System.Drawing.Size(140, 25);

            this.lblDefaultQuantity.AutoSize = true;
            this.lblDefaultQuantity.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblDefaultQuantity.ForeColor = System.Drawing.Color.FromArgb(230, 238, 247);
            this.lblDefaultQuantity.Location = new System.Drawing.Point(290, 175);
            this.lblDefaultQuantity.Text = "默认数量：";

            this.txtDefaultQuantity.BackColor = System.Drawing.Color.FromArgb(19, 35, 58);
            this.txtDefaultQuantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDefaultQuantity.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtDefaultQuantity.ForeColor = System.Drawing.Color.FromArgb(230, 238, 247);
            this.txtDefaultQuantity.Location = new System.Drawing.Point(370, 172);
            this.txtDefaultQuantity.Size = new System.Drawing.Size(60, 25);

            this.lblUnit.AutoSize = true;
            this.lblUnit.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblUnit.ForeColor = System.Drawing.Color.FromArgb(230, 238, 247);
            this.lblUnit.Location = new System.Drawing.Point(30, 215);
            this.lblUnit.Text = "计量单位：";

            this.txtUnit.BackColor = System.Drawing.Color.FromArgb(19, 35, 58);
            this.txtUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUnit.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtUnit.ForeColor = System.Drawing.Color.FromArgb(230, 238, 247);
            this.txtUnit.Location = new System.Drawing.Point(130, 212);
            this.txtUnit.Size = new System.Drawing.Size(140, 25);

            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblDescription.ForeColor = System.Drawing.Color.FromArgb(230, 238, 247);
            this.lblDescription.Location = new System.Drawing.Point(30, 255);
            this.lblDescription.Text = "备注：";

            this.txtDescription.BackColor = System.Drawing.Color.FromArgb(19, 35, 58);
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtDescription.ForeColor = System.Drawing.Color.FromArgb(230, 238, 247);
            this.txtDescription.Location = new System.Drawing.Point(130, 252);
            this.txtDescription.Size = new System.Drawing.Size(300, 50);
            this.txtDescription.Multiline = true;

            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(19, 35, 58);
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(230, 238, 247);
            this.btnCancel.Location = new System.Drawing.Point(260, 325);
            this.btnCancel.Size = new System.Drawing.Size(80, 32);
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.btnSave.BackColor = System.Drawing.Color.FromArgb(27, 111, 214);
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(350, 325);
            this.btnSave.Size = new System.Drawing.Size(80, 32);
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(11, 22, 34);
            this.ClientSize = new System.Drawing.Size(460, 378);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtUnit);
            this.Controls.Add(this.lblUnit);
            this.Controls.Add(this.txtDefaultQuantity);
            this.Controls.Add(this.lblDefaultQuantity);
            this.Controls.Add(this.txtUnitPrice);
            this.Controls.Add(this.lblUnitPrice);
            this.Controls.Add(this.cmbFaultType);
            this.Controls.Add(this.lblFaultType);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.pnlTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MaterialEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "物料信息";
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
