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
            this.lblEquipment = new Sunny.UI.UILabel();
            this.cmbEquipment = new System.Windows.Forms.ComboBox();
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
            this.lblPhoto = new Sunny.UI.UILabel();
            this.btnUploadPhoto = new Sunny.UI.UISymbolButton();
            this.picPreview = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // lblEquipment
            // 
            this.lblEquipment.BackColor = System.Drawing.Color.Transparent;
            this.lblEquipment.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblEquipment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.lblEquipment.Location = new System.Drawing.Point(26, 59);
            this.lblEquipment.Name = "lblEquipment";
            this.lblEquipment.Size = new System.Drawing.Size(100, 22);
            this.lblEquipment.Style = Sunny.UI.UIStyle.Custom;
            this.lblEquipment.TabIndex = 1;
            this.lblEquipment.Text = "选择设备";
            this.lblEquipment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbEquipment
            // 
            this.cmbEquipment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEquipment.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.cmbEquipment.Location = new System.Drawing.Point(26, 86);
            this.cmbEquipment.Name = "cmbEquipment";
            this.cmbEquipment.Size = new System.Drawing.Size(440, 28);
            this.cmbEquipment.TabIndex = 2;
            // 
            // rdbDept
            // 
            this.rdbDept.BackColor = System.Drawing.Color.Transparent;
            this.rdbDept.Checked = true;
            this.rdbDept.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdbDept.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.rdbDept.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.rdbDept.Location = new System.Drawing.Point(26, 114);
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
            this.rdkBorrowed.Location = new System.Drawing.Point(150, 114);
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
            this.lblFaultType.Location = new System.Drawing.Point(26, 154);
            this.lblFaultType.Name = "lblFaultType";
            this.lblFaultType.Size = new System.Drawing.Size(100, 20);
            this.lblFaultType.Style = Sunny.UI.UIStyle.Custom;
            this.lblFaultType.TabIndex = 5;
            this.lblFaultType.Text = "故障类型";
            this.lblFaultType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFaultType.Visible = false;
            // 
            // cmbFaultType
            // 
            this.cmbFaultType.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cmbFaultType.FillColor = System.Drawing.Color.White;
            this.cmbFaultType.Font = new System.Drawing.Font("微软雅黑", 9F); this.cmbFaultType.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cmbFaultType.Location = new System.Drawing.Point(26, 177);
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
            this.cmbFaultType.Visible = false;
            // 
            // lblFaultDesc
            // 
            this.lblFaultDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblFaultDesc.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblFaultDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.lblFaultDesc.Location = new System.Drawing.Point(26, 154);
            this.lblFaultDesc.Name = "lblFaultDesc";
            this.lblFaultDesc.Size = new System.Drawing.Size(100, 20);
            this.lblFaultDesc.Style = Sunny.UI.UIStyle.Custom;
            this.lblFaultDesc.TabIndex = 7;
            this.lblFaultDesc.Text = "故障描述";
            this.lblFaultDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFaultDesc
            // 
            this.txtFaultDesc.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFaultDesc.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.txtFaultDesc.Location = new System.Drawing.Point(26, 178);
            this.txtFaultDesc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFaultDesc.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtFaultDesc.Multiline = true;
            this.txtFaultDesc.Name = "txtFaultDesc";
            this.txtFaultDesc.Padding = new System.Windows.Forms.Padding(5);
            this.txtFaultDesc.ShowText = false;
            this.txtFaultDesc.Size = new System.Drawing.Size(440, 70);
            this.txtFaultDesc.Style = Sunny.UI.UIStyle.Custom;
            this.txtFaultDesc.TabIndex = 8;
            this.txtFaultDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtFaultDesc.Watermark = "";
            // 
            // lblPhoto
            // 
            this.lblPhoto.BackColor = System.Drawing.Color.Transparent;
            this.lblPhoto.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblPhoto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.lblPhoto.Location = new System.Drawing.Point(26, 258);
            this.lblPhoto.Name = "lblPhoto";
            this.lblPhoto.Size = new System.Drawing.Size(100, 22);
            this.lblPhoto.Style = Sunny.UI.UIStyle.Custom;
            this.lblPhoto.TabIndex = 8;
            this.lblPhoto.Text = "故障照片";
            this.lblPhoto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnUploadPhoto
            // 
            this.btnUploadPhoto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUploadPhoto.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnUploadPhoto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.btnUploadPhoto.Location = new System.Drawing.Point(26, 284);
            this.btnUploadPhoto.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnUploadPhoto.Name = "btnUploadPhoto";
            this.btnUploadPhoto.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.btnUploadPhoto.Size = new System.Drawing.Size(100, 30);
            this.btnUploadPhoto.Style = Sunny.UI.UIStyle.Custom;
            this.btnUploadPhoto.Symbol = 61747;
            this.btnUploadPhoto.TabIndex = 9;
            this.btnUploadPhoto.Text = "拍照上传";
            // 
            // picPreview
            // 
            this.picPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPreview.Location = new System.Drawing.Point(144, 258);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(200, 126);
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPreview.TabIndex = 10;
            this.picPreview.TabStop = false;
            // 
            // lblUrgency
            // 
            this.lblUrgency.BackColor = System.Drawing.Color.Transparent;
            this.lblUrgency.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblUrgency.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.lblUrgency.Location = new System.Drawing.Point(26, 396);
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
            this.cmbUrgency.Font = new System.Drawing.Font("微软雅黑", 9F); this.cmbUrgency.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cmbUrgency.Location = new System.Drawing.Point(26, 419);
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
            this.btnSubmit.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
            this.btnSubmit.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(236, 465);
            this.btnSubmit.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
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
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.btnCancel.Location = new System.Drawing.Point(356, 465);
            this.btnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
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
            this.AllowShowTitle = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(496, 520);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.cmbUrgency);
            this.Controls.Add(this.lblUrgency);
            this.Controls.Add(this.picPreview);
            this.Controls.Add(this.btnUploadPhoto);
            this.Controls.Add(this.lblPhoto);
            this.Controls.Add(this.txtFaultDesc);
            this.Controls.Add(this.lblFaultDesc);
            this.Controls.Add(this.cmbFaultType);
            this.Controls.Add(this.lblFaultType);
            this.Controls.Add(this.rdkBorrowed);
            this.Controls.Add(this.rdbDept);
            this.Controls.Add(this.cmbEquipment);
            this.Controls.Add(this.lblEquipment);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(496, 520);
            this.MinimumSize = new System.Drawing.Size(496, 520);
            this.Name = "RepairSubmitForm";
            this.Padding = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.ShowTitle = true;
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "提交报修";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.TitleFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.ZoomScaleRect = new System.Drawing.Rectangle(22, 22, 496, 520);
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UILabel lblEquipment;
        private System.Windows.Forms.ComboBox cmbEquipment;
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
        private Sunny.UI.UILabel lblPhoto;
        private Sunny.UI.UISymbolButton btnUploadPhoto;
        private System.Windows.Forms.PictureBox picPreview;
    }
}

