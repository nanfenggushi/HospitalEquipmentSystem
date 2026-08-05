namespace HospitalEquipmentSystem.UI
{
    partial class OrderEditForm
    {
        private System.ComponentModel.IContainer components = null;

        private Sunny.UI.UIPanel pnlHeader;
        private Sunny.UI.UILabel lblTitle;
        private Sunny.UI.UILabel lblNo;
        private Sunny.UI.UITextBox txtRepairNo;
        private Sunny.UI.UILabel lblEq;
        private Sunny.UI.UIComboBox cmbEquipment;
        private Sunny.UI.UILabel lblDept;
        private Sunny.UI.UIComboBox cmbDept;
        private Sunny.UI.UILabel lblFt;
        private Sunny.UI.UIComboBox cmbFaultType;
        private Sunny.UI.UILabel lblDesc;
        private Sunny.UI.UITextBox txtFaultDesc;
        private Sunny.UI.UILabel lblUrg;
        private Sunny.UI.UIComboBox cmbUrgency;
        private Sunny.UI.UISymbolButton btnSave;
        private Sunny.UI.UISymbolButton btnCancel;
        private Sunny.UI.UISymbolButton btnDelete;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new Sunny.UI.UIPanel();
            this.lblTitle = new Sunny.UI.UILabel();
            this.lblNo = new Sunny.UI.UILabel();
            this.txtRepairNo = new Sunny.UI.UITextBox();
            this.lblEq = new Sunny.UI.UILabel();
            this.cmbEquipment = new Sunny.UI.UIComboBox();
            this.lblDept = new Sunny.UI.UILabel();
            this.cmbDept = new Sunny.UI.UIComboBox();
            this.lblFt = new Sunny.UI.UILabel();
            this.cmbFaultType = new Sunny.UI.UIComboBox();
            this.lblDesc = new Sunny.UI.UILabel();
            this.txtFaultDesc = new Sunny.UI.UITextBox();
            this.lblUrg = new Sunny.UI.UILabel();
            this.cmbUrgency = new Sunny.UI.UIComboBox();
            this.btnSave = new Sunny.UI.UISymbolButton();
            this.btnCancel = new Sunny.UI.UISymbolButton();
            this.btnDelete = new Sunny.UI.UISymbolButton();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.pnlHeader.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(29)))), ((int)(((byte)(46)))));
            this.pnlHeader.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.pnlHeader.Location = new System.Drawing.Point(0, 35);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlHeader.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(20, 12, 20, 12);
            this.pnlHeader.Radius = 0;
            this.pnlHeader.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.pnlHeader.Size = new System.Drawing.Size(519, 60);
            this.pnlHeader.TabIndex = 0;
            this.pnlHeader.Text = null;
            this.pnlHeader.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.lblTitle.Location = new System.Drawing.Point(129, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(270, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNo
            // 
            this.lblNo.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.lblNo.Location = new System.Drawing.Point(35, 134);
            this.lblNo.Name = "lblNo";
            this.lblNo.Size = new System.Drawing.Size(80, 24);
            this.lblNo.TabIndex = 1;
            this.lblNo.Text = "工单号";
            this.lblNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRepairNo
            // 
            this.txtRepairNo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtRepairNo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.txtRepairNo.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.txtRepairNo.FillReadOnlyColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.txtRepairNo.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtRepairNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.txtRepairNo.Location = new System.Drawing.Point(125, 134);
            this.txtRepairNo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtRepairNo.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtRepairNo.Name = "txtRepairNo";
            this.txtRepairNo.Padding = new System.Windows.Forms.Padding(5);
            this.txtRepairNo.ReadOnly = true;
            this.txtRepairNo.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.txtRepairNo.ShowText = false;
            this.txtRepairNo.Size = new System.Drawing.Size(360, 32);
            this.txtRepairNo.Style = Sunny.UI.UIStyle.Custom;
            this.txtRepairNo.TabIndex = 2;
            this.txtRepairNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtRepairNo.Watermark = "";
            // 
            // lblEq
            // 
            this.lblEq.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblEq.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.lblEq.Location = new System.Drawing.Point(35, 185);
            this.lblEq.Name = "lblEq";
            this.lblEq.Size = new System.Drawing.Size(80, 24);
            this.lblEq.TabIndex = 3;
            this.lblEq.Text = "设备";
            this.lblEq.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbEquipment
            // 
            this.cmbEquipment.DataSource = null;
            this.cmbEquipment.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cmbEquipment.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.cmbEquipment.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.cmbEquipment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.cmbEquipment.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.cmbEquipment.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cmbEquipment.Location = new System.Drawing.Point(125, 185);
            this.cmbEquipment.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbEquipment.MinimumSize = new System.Drawing.Size(63, 0);
            this.cmbEquipment.Name = "cmbEquipment";
            this.cmbEquipment.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cmbEquipment.Size = new System.Drawing.Size(360, 30);
            this.cmbEquipment.SymbolSize = 24;
            this.cmbEquipment.TabIndex = 4;
            this.cmbEquipment.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmbEquipment.Watermark = "";
            // 
            // lblDept
            // 
            this.lblDept.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblDept.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.lblDept.Location = new System.Drawing.Point(35, 235);
            this.lblDept.Name = "lblDept";
            this.lblDept.Size = new System.Drawing.Size(80, 24);
            this.lblDept.TabIndex = 5;
            this.lblDept.Text = "科室";
            this.lblDept.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbDept
            // 
            this.cmbDept.DataSource = null;
            this.cmbDept.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cmbDept.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.cmbDept.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.cmbDept.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.cmbDept.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.cmbDept.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cmbDept.Location = new System.Drawing.Point(125, 235);
            this.cmbDept.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbDept.MinimumSize = new System.Drawing.Size(63, 0);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cmbDept.Size = new System.Drawing.Size(360, 30);
            this.cmbDept.SymbolSize = 24;
            this.cmbDept.TabIndex = 6;
            this.cmbDept.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmbDept.Watermark = "";
            // 
            // lblFt
            // 
            this.lblFt.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblFt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.lblFt.Location = new System.Drawing.Point(35, 285);
            this.lblFt.Name = "lblFt";
            this.lblFt.Size = new System.Drawing.Size(80, 24);
            this.lblFt.TabIndex = 7;
            this.lblFt.Text = "故障类型";
            this.lblFt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbFaultType
            // 
            this.cmbFaultType.DataSource = null;
            this.cmbFaultType.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cmbFaultType.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.cmbFaultType.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.cmbFaultType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.cmbFaultType.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.cmbFaultType.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cmbFaultType.Location = new System.Drawing.Point(125, 285);
            this.cmbFaultType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbFaultType.MinimumSize = new System.Drawing.Size(63, 0);
            this.cmbFaultType.Name = "cmbFaultType";
            this.cmbFaultType.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cmbFaultType.Size = new System.Drawing.Size(360, 30);
            this.cmbFaultType.SymbolSize = 24;
            this.cmbFaultType.TabIndex = 8;
            this.cmbFaultType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmbFaultType.Watermark = "";
            // 
            // lblDesc
            // 
            this.lblDesc.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.lblDesc.Location = new System.Drawing.Point(20, 335);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(95, 32);
            this.lblDesc.TabIndex = 9;
            this.lblDesc.Text = "故障描述";
            this.lblDesc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFaultDesc
            // 
            this.txtFaultDesc.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFaultDesc.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.txtFaultDesc.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtFaultDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.txtFaultDesc.Location = new System.Drawing.Point(125, 335);
            this.txtFaultDesc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFaultDesc.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtFaultDesc.Multiline = true;
            this.txtFaultDesc.Name = "txtFaultDesc";
            this.txtFaultDesc.Padding = new System.Windows.Forms.Padding(5);
            this.txtFaultDesc.ShowText = false;
            this.txtFaultDesc.Size = new System.Drawing.Size(360, 60);
            this.txtFaultDesc.TabIndex = 10;
            this.txtFaultDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtFaultDesc.Watermark = "";
            // 
            // lblUrg
            // 
            this.lblUrg.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblUrg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.lblUrg.Location = new System.Drawing.Point(30, 415);
            this.lblUrg.Name = "lblUrg";
            this.lblUrg.Size = new System.Drawing.Size(80, 24);
            this.lblUrg.TabIndex = 11;
            this.lblUrg.Text = "紧急度";
            this.lblUrg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbUrgency
            // 
            this.cmbUrgency.DataSource = null;
            this.cmbUrgency.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cmbUrgency.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.cmbUrgency.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.cmbUrgency.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.cmbUrgency.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.cmbUrgency.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cmbUrgency.Location = new System.Drawing.Point(125, 415);
            this.cmbUrgency.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbUrgency.MinimumSize = new System.Drawing.Size(63, 0);
            this.cmbUrgency.Name = "cmbUrgency";
            this.cmbUrgency.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cmbUrgency.Size = new System.Drawing.Size(355, 30);
            this.cmbUrgency.SymbolSize = 24;
            this.cmbUrgency.TabIndex = 12;
            this.cmbUrgency.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmbUrgency.Watermark = "";
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
            this.btnSave.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnSave.Location = new System.Drawing.Point(300, 470);
            this.btnSave.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSave.Name = "btnSave";
            this.btnSave.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
            this.btnSave.Size = new System.Drawing.Size(90, 36);
            this.btnSave.Symbol = 61639;
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "保存";
            this.btnSave.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.btnCancel.Location = new System.Drawing.Point(400, 470);
            this.btnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.btnCancel.Size = new System.Drawing.Size(90, 36);
            this.btnCancel.Symbol = 61453;
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "取消";
            this.btnCancel.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // btnDelete
            // 
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.btnDelete.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnDelete.Location = new System.Drawing.Point(125, 470);
            this.btnDelete.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.btnDelete.Size = new System.Drawing.Size(90, 36);
            this.btnDelete.Symbol = 61457;
            this.btnDelete.TabIndex = 15;
            this.btnDelete.Text = "删除";
            this.btnDelete.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // OrderEditForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(519, 553);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.lblNo);
            this.Controls.Add(this.txtRepairNo);
            this.Controls.Add(this.lblEq);
            this.Controls.Add(this.cmbEquipment);
            this.Controls.Add(this.lblDept);
            this.Controls.Add(this.cmbDept);
            this.Controls.Add(this.lblFt);
            this.Controls.Add(this.cmbFaultType);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.txtFaultDesc);
            this.Controls.Add(this.lblUrg);
            this.Controls.Add(this.cmbUrgency);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDelete);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OrderEditForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TitleColor = System.Drawing.SystemColors.Desktop;
            this.ZoomScaleRect = new System.Drawing.Rectangle(22, 22, 520, 480);
            this.pnlHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
