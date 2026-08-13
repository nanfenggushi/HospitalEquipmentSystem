namespace HospitalEquipmentSystem.UI
{
    partial class BorrowApplyDialog
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
            this.lblEquipment = new Sunny.UI.UILabel();
            this.cmbEquipment = new Sunny.UI.UIComboBox();
            this.lblCheck = new Sunny.UI.UILabel();
            this.lblPurpose = new Sunny.UI.UILabel();
            this.txtPurpose = new Sunny.UI.UITextBox();
            this.lblReturn = new Sunny.UI.UILabel();
            this.dpReturn = new Sunny.UI.UIDatePicker();
            this.lblBorrower = new Sunny.UI.UILabel();
            this.lblBorrowerValue = new Sunny.UI.UILabel();
            this.lblRemarks = new Sunny.UI.UILabel();
            this.txtRemarks = new Sunny.UI.UITextBox();
            this.btnSubmit = new Sunny.UI.UISymbolButton();
            this.btnCancel = new Sunny.UI.UIButton();
            this.SuspendLayout();
            // 
            // lblEquipment
            // 
            this.lblEquipment.Font = new System.Drawing.Font("宋体", 12F);
            this.lblEquipment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.lblEquipment.Location = new System.Drawing.Point(38, 77);
            this.lblEquipment.Name = "lblEquipment";
            this.lblEquipment.Size = new System.Drawing.Size(115, 30);
            this.lblEquipment.TabIndex = 0;
            this.lblEquipment.Text = "借用设备：";
            this.lblEquipment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbEquipment
            // 
            this.cmbEquipment.DataSource = null;
            this.cmbEquipment.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.cmbEquipment.Font = new System.Drawing.Font("宋体", 12F);
            this.cmbEquipment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.cmbEquipment.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.cmbEquipment.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cmbEquipment.Location = new System.Drawing.Point(150, 73);
            this.cmbEquipment.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbEquipment.MinimumSize = new System.Drawing.Size(1, 1);
            this.cmbEquipment.Name = "cmbEquipment";
            this.cmbEquipment.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cmbEquipment.Size = new System.Drawing.Size(330, 36);
            this.cmbEquipment.SymbolSize = 24;
            this.cmbEquipment.TabIndex = 1;
            this.cmbEquipment.Text = "请选择设备";
            this.cmbEquipment.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmbEquipment.Watermark = "";
            // 
            // lblCheck
            // 
            this.lblCheck.Font = new System.Drawing.Font("宋体", 12F);
            this.lblCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.lblCheck.Location = new System.Drawing.Point(150, 117);
            this.lblCheck.Name = "lblCheck";
            this.lblCheck.Size = new System.Drawing.Size(330, 26);
            this.lblCheck.TabIndex = 2;
            this.lblCheck.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPurpose
            // 
            this.lblPurpose.Font = new System.Drawing.Font("宋体", 12F);
            this.lblPurpose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.lblPurpose.Location = new System.Drawing.Point(38, 157);
            this.lblPurpose.Name = "lblPurpose";
            this.lblPurpose.Size = new System.Drawing.Size(115, 30);
            this.lblPurpose.TabIndex = 3;
            this.lblPurpose.Text = "借用用途：";
            this.lblPurpose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPurpose
            // 
            this.txtPurpose.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPurpose.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.txtPurpose.Font = new System.Drawing.Font("宋体", 12F);
            this.txtPurpose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.txtPurpose.Location = new System.Drawing.Point(150, 153);
            this.txtPurpose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPurpose.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtPurpose.Multiline = true;
            this.txtPurpose.Name = "txtPurpose";
            this.txtPurpose.Padding = new System.Windows.Forms.Padding(5);
            this.txtPurpose.ShowText = false;
            this.txtPurpose.Size = new System.Drawing.Size(330, 76);
            this.txtPurpose.TabIndex = 4;
            this.txtPurpose.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtPurpose.Watermark = "";
            // 
            // lblReturn
            // 
            this.lblReturn.Font = new System.Drawing.Font("宋体", 12F);
            this.lblReturn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.lblReturn.Location = new System.Drawing.Point(38, 255);
            this.lblReturn.Name = "lblReturn";
            this.lblReturn.Size = new System.Drawing.Size(115, 30);
            this.lblReturn.TabIndex = 5;
            this.lblReturn.Text = "预计归还：";
            this.lblReturn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dpReturn
            // 
            this.dpReturn.DateCultureInfo = new System.Globalization.CultureInfo("");
            this.dpReturn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.dpReturn.Font = new System.Drawing.Font("宋体", 12F);
            this.dpReturn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.dpReturn.Location = new System.Drawing.Point(150, 251);
            this.dpReturn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dpReturn.MaxLength = 10;
            this.dpReturn.MinimumSize = new System.Drawing.Size(1, 1);
            this.dpReturn.Name = "dpReturn";
            this.dpReturn.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.dpReturn.Size = new System.Drawing.Size(170, 36);
            this.dpReturn.SymbolDropDown = 61555;
            this.dpReturn.SymbolNormal = 61555;
            this.dpReturn.SymbolSize = 24;
            this.dpReturn.TabIndex = 6;
            this.dpReturn.Text = "2026-07-31";
            this.dpReturn.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.dpReturn.Value = new System.DateTime(2026, 7, 31, 15, 27, 52, 524);
            this.dpReturn.Watermark = "";
            // 
            // lblBorrower
            // 
            this.lblBorrower.Font = new System.Drawing.Font("宋体", 12F);
            this.lblBorrower.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.lblBorrower.Location = new System.Drawing.Point(38, 305);
            this.lblBorrower.Name = "lblBorrower";
            this.lblBorrower.Size = new System.Drawing.Size(100, 30);
            this.lblBorrower.TabIndex = 7;
            this.lblBorrower.Text = "申请人：";
            this.lblBorrower.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBorrowerValue
            // 
            this.lblBorrowerValue.Font = new System.Drawing.Font("宋体", 12F);
            this.lblBorrowerValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.lblBorrowerValue.Location = new System.Drawing.Point(150, 305);
            this.lblBorrowerValue.Name = "lblBorrowerValue";
            this.lblBorrowerValue.Size = new System.Drawing.Size(330, 30);
            this.lblBorrowerValue.TabIndex = 8;
            this.lblBorrowerValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRemarks
            // 
            this.lblRemarks.Font = new System.Drawing.Font("宋体", 12F);
            this.lblRemarks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.lblRemarks.Location = new System.Drawing.Point(38, 355);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(100, 30);
            this.lblRemarks.TabIndex = 9;
            this.lblRemarks.Text = "备注：";
            this.lblRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtRemarks
            // 
            this.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtRemarks.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.txtRemarks.Font = new System.Drawing.Font("宋体", 12F);
            this.txtRemarks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.txtRemarks.Location = new System.Drawing.Point(150, 351);
            this.txtRemarks.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtRemarks.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Padding = new System.Windows.Forms.Padding(5);
            this.txtRemarks.ShowText = false;
            this.txtRemarks.Size = new System.Drawing.Size(330, 56);
            this.txtRemarks.TabIndex = 10;
            this.txtRemarks.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtRemarks.Watermark = "";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSubmit.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
            this.btnSubmit.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(130)))), ((int)(((byte)(230)))));
            this.btnSubmit.Font = new System.Drawing.Font("宋体", 12F);
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(180, 455);
            this.btnSubmit.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Radius = 6;
            this.btnSubmit.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
            this.btnSubmit.Size = new System.Drawing.Size(120, 40);
            this.btnSubmit.Symbol = 61543;
            this.btnSubmit.TabIndex = 11;
            this.btnSubmit.Text = "提交申请";
            this.btnSubmit.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.btnCancel.Font = new System.Drawing.Font("宋体", 12F);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.btnCancel.Location = new System.Drawing.Point(320, 455);
            this.btnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.btnCancel.Size = new System.Drawing.Size(100, 40);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "取 消";
            this.btnCancel.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // BorrowApplyDialog
            // 
            this.AllowShowTitle = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(540, 525);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtRemarks);
            this.Controls.Add(this.lblRemarks);
            this.Controls.Add(this.lblBorrowerValue);
            this.Controls.Add(this.lblBorrower);
            this.Controls.Add(this.dpReturn);
            this.Controls.Add(this.lblReturn);
            this.Controls.Add(this.txtPurpose);
            this.Controls.Add(this.lblPurpose);
            this.Controls.Add(this.lblCheck);
            this.Controls.Add(this.cmbEquipment);
            this.Controls.Add(this.lblEquipment);
            this.Name = "BorrowApplyDialog";
            this.Padding = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.ShowTitle = true;
            this.Text = "申请借用设备";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.TitleFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.ZoomScaleRect = new System.Drawing.Rectangle(19, 19, 520, 505);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UILabel lblEquipment;
        private Sunny.UI.UIComboBox cmbEquipment;
        private Sunny.UI.UILabel lblCheck;
        private Sunny.UI.UILabel lblPurpose;
        private Sunny.UI.UITextBox txtPurpose;
        private Sunny.UI.UILabel lblReturn;
        private Sunny.UI.UIDatePicker dpReturn;
        private Sunny.UI.UILabel lblBorrower;
        private Sunny.UI.UILabel lblBorrowerValue;
        private Sunny.UI.UILabel lblRemarks;
        private Sunny.UI.UITextBox txtRemarks;
        private Sunny.UI.UISymbolButton btnSubmit;
        private Sunny.UI.UIButton btnCancel;
    }
}
