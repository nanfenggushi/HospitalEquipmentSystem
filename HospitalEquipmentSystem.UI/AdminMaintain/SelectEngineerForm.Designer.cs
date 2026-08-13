namespace HospitalEquipmentSystem.UI
{
    partial class SelectEngineerForm
    {
        private System.ComponentModel.IContainer components = null;
        private Sunny.UI.UILabel lblPrompt;
        private Sunny.UI.UIComboBox cmbEngineer;
        private Sunny.UI.UISymbolButton btnOk;
        private Sunny.UI.UISymbolButton btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblPrompt = new Sunny.UI.UILabel();
            this.cmbEngineer = new Sunny.UI.UIComboBox();
            this.btnOk = new Sunny.UI.UISymbolButton();
            this.btnCancel = new Sunny.UI.UISymbolButton();
            this.SuspendLayout();
            // 
            // lblPrompt
            // 
            this.lblPrompt.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.lblPrompt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.lblPrompt.Location = new System.Drawing.Point(25, 45);
            this.lblPrompt.Name = "lblPrompt";
            this.lblPrompt.Size = new System.Drawing.Size(300, 35);
            this.lblPrompt.TabIndex = 0;
            this.lblPrompt.Text = "请选择指派的维修工程师：";
            // 
            // cmbEngineer
            // 
            this.cmbEngineer.DataSource = null;
            this.cmbEngineer.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cmbEngineer.FillColor = System.Drawing.Color.White;
            this.cmbEngineer.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.cmbEngineer.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.cmbEngineer.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cmbEngineer.Location = new System.Drawing.Point(30, 120);
            this.cmbEngineer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbEngineer.MinimumSize = new System.Drawing.Size(63, 0);
            this.cmbEngineer.Name = "cmbEngineer";
            this.cmbEngineer.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cmbEngineer.Size = new System.Drawing.Size(300, 36);
            this.cmbEngineer.SymbolSize = 24;
            this.cmbEngineer.TabIndex = 1;
            this.cmbEngineer.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmbEngineer.Watermark = "";
            // 
            // btnOk
            // 
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOk.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
            this.btnOk.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(121, 178);
            this.btnOk.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnOk.Name = "btnOk";
            this.btnOk.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
            this.btnOk.Size = new System.Drawing.Size(110, 38);
            this.btnOk.Style = Sunny.UI.UIStyle.Custom;
            this.btnOk.Symbol = 61543;
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "确定指派";
            this.btnOk.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.btnCancel.Location = new System.Drawing.Point(250, 178);
            this.btnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.btnCancel.Size = new System.Drawing.Size(90, 38);
            this.btnCancel.Style = Sunny.UI.UIStyle.Custom;
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // SelectEngineerForm
            // 
            this.AllowShowTitle = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(380, 255);
            this.ControlBoxFillHoverColor = System.Drawing.Color.Gray;
            this.Controls.Add(this.lblPrompt);
            this.Controls.Add(this.cmbEngineer);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectEngineerForm";
            this.Padding = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择维修工程师";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.TitleFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.ShowTitle = true;
            this.ZoomScaleRect = new System.Drawing.Rectangle(22, 22, 380, 255);
            this.ResumeLayout(false);

        }
    }
}
