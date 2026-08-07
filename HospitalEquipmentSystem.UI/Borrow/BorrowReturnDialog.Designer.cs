namespace HospitalEquipmentSystem.UI
{
    partial class BorrowReturnDialog
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
            this.lblNoCaption = new Sunny.UI.UILabel();
            this.lblNoValue = new Sunny.UI.UILabel();
            this.lblEquipmentCaption = new Sunny.UI.UILabel();
            this.lblEquipmentValue = new Sunny.UI.UILabel();
            this.lblApplicantCaption = new Sunny.UI.UILabel();
            this.lblApplicantValue = new Sunny.UI.UILabel();
            this.lblDeptCaption = new Sunny.UI.UILabel();
            this.lblDeptValue = new Sunny.UI.UILabel();
            this.lblReturnCaption = new Sunny.UI.UILabel();
            this.lblReturnValue = new Sunny.UI.UILabel();
            this.lblOverdueCaption = new Sunny.UI.UILabel();
            this.lblOverdueValue = new Sunny.UI.UILabel();
            this.chkDamage = new Sunny.UI.UICheckBox();
            this.txtDamageDesc = new Sunny.UI.UITextBox();
            this.lblReturnNote = new Sunny.UI.UILabel();
            this.txtReturnNote = new Sunny.UI.UITextBox();
            this.btnConfirm = new Sunny.UI.UISymbolButton();
            this.btnCancel = new Sunny.UI.UIButton();
            this.SuspendLayout();
            // 
            // lblNoCaption
            // 
            this.lblNoCaption.Font = new System.Drawing.Font("宋体", 12F);
            this.lblNoCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.lblNoCaption.Location = new System.Drawing.Point(38, 69);
            this.lblNoCaption.Name = "lblNoCaption";
            this.lblNoCaption.Size = new System.Drawing.Size(116, 28);
            this.lblNoCaption.TabIndex = 0;
            this.lblNoCaption.Text = "借用单号：";
            this.lblNoCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNoValue
            // 
            this.lblNoValue.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.lblNoValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.lblNoValue.Location = new System.Drawing.Point(170, 69);
            this.lblNoValue.Name = "lblNoValue";
            this.lblNoValue.Size = new System.Drawing.Size(330, 28);
            this.lblNoValue.TabIndex = 1;
            this.lblNoValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEquipmentCaption
            // 
            this.lblEquipmentCaption.Font = new System.Drawing.Font("宋体", 12F);
            this.lblEquipmentCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.lblEquipmentCaption.Location = new System.Drawing.Point(38, 107);
            this.lblEquipmentCaption.Name = "lblEquipmentCaption";
            this.lblEquipmentCaption.Size = new System.Drawing.Size(126, 28);
            this.lblEquipmentCaption.TabIndex = 2;
            this.lblEquipmentCaption.Text = "借用设备：";
            this.lblEquipmentCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEquipmentValue
            // 
            this.lblEquipmentValue.Font = new System.Drawing.Font("宋体", 12F);
            this.lblEquipmentValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.lblEquipmentValue.Location = new System.Drawing.Point(170, 107);
            this.lblEquipmentValue.Name = "lblEquipmentValue";
            this.lblEquipmentValue.Size = new System.Drawing.Size(330, 28);
            this.lblEquipmentValue.TabIndex = 3;
            this.lblEquipmentValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblApplicantCaption
            // 
            this.lblApplicantCaption.Font = new System.Drawing.Font("宋体", 12F);
            this.lblApplicantCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.lblApplicantCaption.Location = new System.Drawing.Point(38, 145);
            this.lblApplicantCaption.Name = "lblApplicantCaption";
            this.lblApplicantCaption.Size = new System.Drawing.Size(100, 28);
            this.lblApplicantCaption.TabIndex = 4;
            this.lblApplicantCaption.Text = "借用人：";
            this.lblApplicantCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblApplicantValue
            // 
            this.lblApplicantValue.Font = new System.Drawing.Font("宋体", 12F);
            this.lblApplicantValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.lblApplicantValue.Location = new System.Drawing.Point(170, 145);
            this.lblApplicantValue.Name = "lblApplicantValue";
            this.lblApplicantValue.Size = new System.Drawing.Size(330, 28);
            this.lblApplicantValue.TabIndex = 5;
            this.lblApplicantValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDeptCaption
            // 
            this.lblDeptCaption.Font = new System.Drawing.Font("宋体", 12F);
            this.lblDeptCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.lblDeptCaption.Location = new System.Drawing.Point(38, 183);
            this.lblDeptCaption.Name = "lblDeptCaption";
            this.lblDeptCaption.Size = new System.Drawing.Size(116, 28);
            this.lblDeptCaption.TabIndex = 6;
            this.lblDeptCaption.Text = "申请科室：";
            this.lblDeptCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDeptValue
            // 
            this.lblDeptValue.Font = new System.Drawing.Font("宋体", 12F);
            this.lblDeptValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.lblDeptValue.Location = new System.Drawing.Point(170, 183);
            this.lblDeptValue.Name = "lblDeptValue";
            this.lblDeptValue.Size = new System.Drawing.Size(330, 28);
            this.lblDeptValue.TabIndex = 7;
            this.lblDeptValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblReturnCaption
            // 
            this.lblReturnCaption.Font = new System.Drawing.Font("宋体", 12F);
            this.lblReturnCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.lblReturnCaption.Location = new System.Drawing.Point(38, 221);
            this.lblReturnCaption.Name = "lblReturnCaption";
            this.lblReturnCaption.Size = new System.Drawing.Size(126, 28);
            this.lblReturnCaption.TabIndex = 8;
            this.lblReturnCaption.Text = "应还日期：";
            this.lblReturnCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblReturnValue
            // 
            this.lblReturnValue.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.lblReturnValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.lblReturnValue.Location = new System.Drawing.Point(170, 221);
            this.lblReturnValue.Name = "lblReturnValue";
            this.lblReturnValue.Size = new System.Drawing.Size(330, 28);
            this.lblReturnValue.TabIndex = 9;
            this.lblReturnValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOverdueCaption
            // 
            this.lblOverdueCaption.Font = new System.Drawing.Font("宋体", 12F);
            this.lblOverdueCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.lblOverdueCaption.Location = new System.Drawing.Point(38, 259);
            this.lblOverdueCaption.Name = "lblOverdueCaption";
            this.lblOverdueCaption.Size = new System.Drawing.Size(116, 28);
            this.lblOverdueCaption.TabIndex = 10;
            this.lblOverdueCaption.Text = "超期状态：";
            this.lblOverdueCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOverdueValue
            // 
            this.lblOverdueValue.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.lblOverdueValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.lblOverdueValue.Location = new System.Drawing.Point(170, 259);
            this.lblOverdueValue.Name = "lblOverdueValue";
            this.lblOverdueValue.Size = new System.Drawing.Size(330, 28);
            this.lblOverdueValue.TabIndex = 11;
            this.lblOverdueValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkDamage
            // 
            this.chkDamage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkDamage.Font = new System.Drawing.Font("宋体", 12F);
            this.chkDamage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.chkDamage.Location = new System.Drawing.Point(38, 311);
            this.chkDamage.MinimumSize = new System.Drawing.Size(1, 1);
            this.chkDamage.Name = "chkDamage";
            this.chkDamage.Size = new System.Drawing.Size(200, 30);
            this.chkDamage.TabIndex = 12;
            this.chkDamage.Text = "设备有损坏";
            // 
            // txtDamageDesc
            // 
            this.txtDamageDesc.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDamageDesc.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.txtDamageDesc.Font = new System.Drawing.Font("宋体", 12F);
            this.txtDamageDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.txtDamageDesc.Location = new System.Drawing.Point(38, 349);
            this.txtDamageDesc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDamageDesc.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtDamageDesc.Multiline = true;
            this.txtDamageDesc.Name = "txtDamageDesc";
            this.txtDamageDesc.Padding = new System.Windows.Forms.Padding(5);
            this.txtDamageDesc.ShowText = false;
            this.txtDamageDesc.Size = new System.Drawing.Size(462, 70);
            this.txtDamageDesc.TabIndex = 13;
            this.txtDamageDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtDamageDesc.Watermark = "";
            // 
            // lblReturnNote
            // 
            this.lblReturnNote.Font = new System.Drawing.Font("宋体", 12F);
            this.lblReturnNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.lblReturnNote.Location = new System.Drawing.Point(38, 443);
            this.lblReturnNote.Name = "lblReturnNote";
            this.lblReturnNote.Size = new System.Drawing.Size(116, 28);
            this.lblReturnNote.TabIndex = 14;
            this.lblReturnNote.Text = "归还备注：";
            this.lblReturnNote.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtReturnNote
            // 
            this.txtReturnNote.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtReturnNote.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.txtReturnNote.Font = new System.Drawing.Font("宋体", 12F);
            this.txtReturnNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.txtReturnNote.Location = new System.Drawing.Point(38, 479);
            this.txtReturnNote.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtReturnNote.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtReturnNote.Multiline = true;
            this.txtReturnNote.Name = "txtReturnNote";
            this.txtReturnNote.Padding = new System.Windows.Forms.Padding(5);
            this.txtReturnNote.ShowText = false;
            this.txtReturnNote.Size = new System.Drawing.Size(462, 70);
            this.txtReturnNote.TabIndex = 15;
            this.txtReturnNote.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtReturnNote.Watermark = "";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirm.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
            this.btnConfirm.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(130)))), ((int)(((byte)(230)))));
            this.btnConfirm.Font = new System.Drawing.Font("宋体", 12F);
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Location = new System.Drawing.Point(180, 575);
            this.btnConfirm.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Radius = 6;
            this.btnConfirm.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
            this.btnConfirm.Size = new System.Drawing.Size(120, 40);
            this.btnConfirm.Symbol = 61452;
            this.btnConfirm.TabIndex = 16;
            this.btnConfirm.Text = "确认归还";
            this.btnConfirm.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.btnCancel.Font = new System.Drawing.Font("宋体", 12F);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.btnCancel.Location = new System.Drawing.Point(320, 575);
            this.btnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.btnCancel.Size = new System.Drawing.Size(100, 40);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "取 消";
            this.btnCancel.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // BorrowReturnDialog
            // 
            this.AllowShowTitle = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(540, 645);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtReturnNote);
            this.Controls.Add(this.lblReturnNote);
            this.Controls.Add(this.txtDamageDesc);
            this.Controls.Add(this.chkDamage);
            this.Controls.Add(this.lblOverdueValue);
            this.Controls.Add(this.lblOverdueCaption);
            this.Controls.Add(this.lblReturnValue);
            this.Controls.Add(this.lblReturnCaption);
            this.Controls.Add(this.lblDeptValue);
            this.Controls.Add(this.lblDeptCaption);
            this.Controls.Add(this.lblApplicantValue);
            this.Controls.Add(this.lblApplicantCaption);
            this.Controls.Add(this.lblEquipmentValue);
            this.Controls.Add(this.lblEquipmentCaption);
            this.Controls.Add(this.lblNoValue);
            this.Controls.Add(this.lblNoCaption);
            this.Name = "BorrowReturnDialog";
            this.Padding = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.ShowTitle = true;
            this.Text = "归还验收";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.TitleFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.ZoomScaleRect = new System.Drawing.Rectangle(19, 19, 520, 625);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UILabel lblNoCaption;
        private Sunny.UI.UILabel lblNoValue;
        private Sunny.UI.UILabel lblEquipmentCaption;
        private Sunny.UI.UILabel lblEquipmentValue;
        private Sunny.UI.UILabel lblApplicantCaption;
        private Sunny.UI.UILabel lblApplicantValue;
        private Sunny.UI.UILabel lblDeptCaption;
        private Sunny.UI.UILabel lblDeptValue;
        private Sunny.UI.UILabel lblReturnCaption;
        private Sunny.UI.UILabel lblReturnValue;
        private Sunny.UI.UILabel lblOverdueCaption;
        private Sunny.UI.UILabel lblOverdueValue;
        private Sunny.UI.UICheckBox chkDamage;
        private Sunny.UI.UITextBox txtDamageDesc;
        private Sunny.UI.UILabel lblReturnNote;
        private Sunny.UI.UITextBox txtReturnNote;
        private Sunny.UI.UISymbolButton btnConfirm;
        private Sunny.UI.UIButton btnCancel;
    }
}
