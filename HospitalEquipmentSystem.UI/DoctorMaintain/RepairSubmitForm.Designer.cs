namespace HospitalEquipmentSystem.UI
{
    partial class RepairSubmitForm
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
            this.pnlHeader = new Sunny.UI.UIPanel();
            this.lblTitle = new Sunny.UI.UILabel();
            this.lblEquipment = new Sunny.UI.UILabel();
            this.cmbEquipment = new Sunny.UI.UIComboBox();
            this.rdbDept = new Sunny.UI.UIRadioButton();
            this.rdkBorrowed = new Sunny.UI.UIRadioButton();
            this.lblFaultType = new Sunny.UI.UILabel();
            this.cmbFaultType = new Sunny.UI.UIComboBox();
            this.lblFaultDesc = new Sunny.UI.UILabel();
            this.txtFaultDesc = new Sunny.UI.UITextBox();
            this.lblUrgency = new Sunny.UI.UILabel();
            this.cmbUrgency = new Sunny.UI.UIComboBox();
            this.btnSubmit = new Sunny.UI.UISymbolButton();
            this.btnCancel = new Sunny.UI.UISymbolButton();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.pnlHeader.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnlHeader.Location = new System.Drawing.Point(0, 35);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlHeader.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.pnlHeader.Size = new System.Drawing.Size(496, 54);
            this.pnlHeader.Style = Sunny.UI.UIStyle.Custom;
            this.pnlHeader.TabIndex = 0;
            this.pnlHeader.Text = null;
            this.pnlHeader.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(200, 30);
            this.lblTitle.Style = Sunny.UI.UIStyle.Custom;
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "提交报修";
            // 
            // lblEquipment
            // 
            this.lblEquipment.BackColor = System.Drawing.Color.Transparent;
            this.lblEquipment.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblEquipment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.lblEquipment.Location = new System.Drawing.Point(26, 94);
            this.lblEquipment.Name = "lblEquipment";
            this.lblEquipment.Size = new System.Drawing.Size(100, 22);
            this.lblEquipment.Style = Sunny.UI.UIStyle.Custom;
            this.lblEquipment.TabIndex = 1;
            this.lblEquipment.Text = "选择设备";
            this.lblEquipment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbEquipment
            // 
            this.cmbEquipment.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cmbEquipment.FillColor = System.Drawing.Color.White;
            this.cmbEquipment.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.cmbEquipment.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.cmbEquipment.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cmbEquipment.Location = new System.Drawing.Point(26, 121);
            this.cmbEquipment.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbEquipment.MinimumSize = new System.Drawing.Size(63, 0);
            this.cmbEquipment.Name = "cmbEquipment";
            this.cmbEquipment.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cmbEquipment.Size = new System.Drawing.Size(440, 28);
            this.cmbEquipment.Style = Sunny.UI.UIStyle.Custom;
            this.cmbEquipment.SymbolSize = 24;
            this.cmbEquipment.TabIndex = 2;
            this.cmbEquipment.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmbEquipment.Watermark = "";
            // 
            // rdbDept
            // 
            this.rdbDept.BackColor = System.Drawing.Color.Transparent;
            this.rdbDept.Checked = true;
            this.rdbDept.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdbDept.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.rdbDept.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.rdbDept.Location = new System.Drawing.Point(26, 149);
            this.rdbDept.MinimumSize = new System.Drawing.Size(1, 1);
            this.rdbDept.Name = "rdbDept";
            this.rdbDept.Size = new System.Drawing.Size(120, 28);
            this.rdbDept.Style = Sunny.UI.UIStyle.Custom;
            this.rdbDept.TabIndex = 3;
            this.rdbDept.Text = "本科室设备";
            // 
            // rdkBorrowed
            // 
            this.rdkBorrowed.BackColor = System.Drawing.Color.Transparent;
            this.rdkBorrowed.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdkBorrowed.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.rdkBorrowed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.rdkBorrowed.Location = new System.Drawing.Point(150, 149);
            this.rdkBorrowed.MinimumSize = new System.Drawing.Size(1, 1);
            this.rdkBorrowed.Name = "rdkBorrowed";
            this.rdkBorrowed.Size = new System.Drawing.Size(120, 28);
            this.rdkBorrowed.Style = Sunny.UI.UIStyle.Custom;
            this.rdkBorrowed.TabIndex = 4;
            this.rdkBorrowed.Text = "借用设备";
            // 
            // lblFaultType
            // 
            this.lblFaultType.BackColor = System.Drawing.Color.Transparent;
            this.lblFaultType.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblFaultType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.lblFaultType.Location = new System.Drawing.Point(26, 189);
            this.lblFaultType.Name = "lblFaultType";
            this.lblFaultType.Size = new System.Drawing.Size(100, 20);
            this.lblFaultType.Style = Sunny.UI.UIStyle.Custom;
            this.lblFaultType.TabIndex = 5;
            this.lblFaultType.Text = "故障类型";
            this.lblFaultType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbFaultType
            // 
            this.cmbFaultType.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cmbFaultType.FillColor = System.Drawing.Color.White;
            this.cmbFaultType.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.cmbFaultType.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.cmbFaultType.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cmbFaultType.Location = new System.Drawing.Point(26, 212);
            this.cmbFaultType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbFaultType.MinimumSize = new System.Drawing.Size(63, 0);
            this.cmbFaultType.Name = "cmbFaultType";
            this.cmbFaultType.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cmbFaultType.Size = new System.Drawing.Size(440, 28);
            this.cmbFaultType.Style = Sunny.UI.UIStyle.Custom;
            this.cmbFaultType.SymbolSize = 24;
            this.cmbFaultType.TabIndex = 6;
            this.cmbFaultType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmbFaultType.Watermark = "";
            // 
            // lblFaultDesc
            // 
            this.lblFaultDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblFaultDesc.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblFaultDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.lblFaultDesc.Location = new System.Drawing.Point(26, 247);
            this.lblFaultDesc.Name = "lblFaultDesc";
            this.lblFaultDesc.Size = new System.Drawing.Size(100, 31);
            this.lblFaultDesc.Style = Sunny.UI.UIStyle.Custom;
            this.lblFaultDesc.TabIndex = 7;
            this.lblFaultDesc.Text = "故障描述";
            this.lblFaultDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFaultDesc
            // 
            this.txtFaultDesc.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFaultDesc.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.txtFaultDesc.Location = new System.Drawing.Point(26, 272);
            this.txtFaultDesc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFaultDesc.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtFaultDesc.Multiline = true;
            this.txtFaultDesc.Name = "txtFaultDesc";
            this.txtFaultDesc.Padding = new System.Windows.Forms.Padding(5);
            this.txtFaultDesc.ShowText = false;
            this.txtFaultDesc.Size = new System.Drawing.Size(440, 80);
            this.txtFaultDesc.Style = Sunny.UI.UIStyle.Custom;
            this.txtFaultDesc.TabIndex = 8;
            this.txtFaultDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtFaultDesc.Watermark = "";
            // 
            // lblUrgency
            // 
            this.lblUrgency.BackColor = System.Drawing.Color.Transparent;
            this.lblUrgency.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblUrgency.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.lblUrgency.Location = new System.Drawing.Point(26, 364);
            this.lblUrgency.Name = "lblUrgency";
            this.lblUrgency.Size = new System.Drawing.Size(100, 20);
            this.lblUrgency.Style = Sunny.UI.UIStyle.Custom;
            this.lblUrgency.TabIndex = 9;
            this.lblUrgency.Text = "紧急程度";
            this.lblUrgency.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbUrgency
            // 
            this.cmbUrgency.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cmbUrgency.FillColor = System.Drawing.Color.White;
            this.cmbUrgency.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.cmbUrgency.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.cmbUrgency.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cmbUrgency.Location = new System.Drawing.Point(26, 387);
            this.cmbUrgency.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbUrgency.MinimumSize = new System.Drawing.Size(63, 0);
            this.cmbUrgency.Name = "cmbUrgency";
            this.cmbUrgency.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cmbUrgency.Size = new System.Drawing.Size(200, 28);
            this.cmbUrgency.Style = Sunny.UI.UIStyle.Custom;
            this.cmbUrgency.SymbolSize = 24;
            this.cmbUrgency.TabIndex = 10;
            this.cmbUrgency.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmbUrgency.Watermark = "";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSubmit.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnSubmit.Location = new System.Drawing.Point(236, 444);
            this.btnSubmit.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(110, 32);
            this.btnSubmit.Style = Sunny.UI.UIStyle.Custom;
            this.btnSubmit.StyleCustomMode = true;
            this.btnSubmit.Symbol = 61452;
            this.btnSubmit.TabIndex = 11;
            this.btnSubmit.Text = "提交报修";
            this.btnSubmit.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSubmit.Click += new System.EventHandler(this.BtnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnCancel.Location = new System.Drawing.Point(356, 444);
            this.btnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 32);
            this.btnCancel.Style = Sunny.UI.UIStyle.Custom;
            this.btnCancel.Symbol = 61453;
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "取消";
            this.btnCancel.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // RepairSubmitForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.ClientSize = new System.Drawing.Size(496, 509);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.cmbUrgency);
            this.Controls.Add(this.lblUrgency);
            this.Controls.Add(this.txtFaultDesc);
            this.Controls.Add(this.lblFaultDesc);
            this.Controls.Add(this.cmbFaultType);
            this.Controls.Add(this.lblFaultType);
            this.Controls.Add(this.rdkBorrowed);
            this.Controls.Add(this.rdbDept);
            this.Controls.Add(this.cmbEquipment);
            this.Controls.Add(this.lblEquipment);
            this.Controls.Add(this.pnlHeader);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(496, 509);
            this.MinimumSize = new System.Drawing.Size(496, 509);
            this.Name = "RepairSubmitForm";
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "提交报修";
            this.TitleColor = System.Drawing.SystemColors.Desktop;
            this.ZoomScaleRect = new System.Drawing.Rectangle(22, 22, 496, 509);
            this.pnlHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIPanel pnlHeader;
        private Sunny.UI.UILabel lblTitle;
        private Sunny.UI.UILabel lblEquipment;
        private Sunny.UI.UIComboBox cmbEquipment;
        private Sunny.UI.UIRadioButton rdbDept;
        private Sunny.UI.UIRadioButton rdkBorrowed;
        private Sunny.UI.UILabel lblFaultType;
        private Sunny.UI.UIComboBox cmbFaultType;
        private Sunny.UI.UILabel lblFaultDesc;
        private Sunny.UI.UITextBox txtFaultDesc;
        private Sunny.UI.UILabel lblUrgency;
        private Sunny.UI.UIComboBox cmbUrgency;
        private Sunny.UI.UISymbolButton btnSubmit;
        private Sunny.UI.UISymbolButton btnCancel;
    }
}
