namespace HospitalEquipmentSystem.UI
{
    partial class RepairResultForm
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
            this.lblResult = new Sunny.UI.UILabel();
            this.txtResult = new Sunny.UI.UITextBox();
            this.lblCost = new Sunny.UI.UILabel();
            this.txtCost = new Sunny.UI.UITextBox();
            this.lblDowntime = new Sunny.UI.UILabel();
            this.txtDowntime = new Sunny.UI.UITextBox();
            this.btnSubmit = new Sunny.UI.UISymbolButton();
            this.btnCancel = new Sunny.UI.UISymbolButton();
            this.SuspendLayout();
            // 
            // lblResult
            // 
            this.lblResult.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblResult.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.lblResult.Location = new System.Drawing.Point(22, 141);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(100, 24);
            this.lblResult.TabIndex = 1;
            this.lblResult.Text = "维修结果 *";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtResult
            // 
            this.txtResult.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtResult.Font = new System.Drawing.Font("宋体", 10F);
            this.txtResult.Location = new System.Drawing.Point(20, 179);
            this.txtResult.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtResult.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.Padding = new System.Windows.Forms.Padding(5);
            this.txtResult.ShowText = false;
            this.txtResult.Size = new System.Drawing.Size(440, 80);
            this.txtResult.TabIndex = 2;
            this.txtResult.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtResult.Watermark = "";
            // 
            // lblCost
            // 
            this.lblCost.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblCost.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.lblCost.Location = new System.Drawing.Point(20, 274);
            this.lblCost.Name = "lblCost";
            this.lblCost.Size = new System.Drawing.Size(129, 25);
            this.lblCost.TabIndex = 3;
            this.lblCost.Text = "维修费用(元)";
            this.lblCost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCost
            // 
            this.txtCost.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCost.Font = new System.Drawing.Font("宋体", 10F);
            this.txtCost.Location = new System.Drawing.Point(20, 304);
            this.txtCost.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCost.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtCost.Name = "txtCost";
            this.txtCost.Padding = new System.Windows.Forms.Padding(5);
            this.txtCost.ShowText = false;
            this.txtCost.Size = new System.Drawing.Size(200, 28);
            this.txtCost.TabIndex = 4;
            this.txtCost.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtCost.Watermark = "";
            // 
            // lblDowntime
            // 
            this.lblDowntime.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblDowntime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.lblDowntime.Location = new System.Drawing.Point(260, 274);
            this.lblDowntime.Name = "lblDowntime";
            this.lblDowntime.Size = new System.Drawing.Size(100, 24);
            this.lblDowntime.TabIndex = 5;
            this.lblDowntime.Text = "停机时长(h)";
            this.lblDowntime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDowntime
            // 
            this.txtDowntime.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDowntime.Font = new System.Drawing.Font("宋体", 10F);
            this.txtDowntime.Location = new System.Drawing.Point(260, 304);
            this.txtDowntime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDowntime.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtDowntime.Name = "txtDowntime";
            this.txtDowntime.Padding = new System.Windows.Forms.Padding(5);
            this.txtDowntime.ShowText = false;
            this.txtDowntime.Size = new System.Drawing.Size(200, 28);
            this.txtDowntime.TabIndex = 6;
            this.txtDowntime.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtDowntime.Watermark = "";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSubmit.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
            this.btnSubmit.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(230, 354);
            this.btnSubmit.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(111)))), ((int)(((byte)(214)))));
            this.btnSubmit.Size = new System.Drawing.Size(100, 36);
            this.btnSubmit.Style = Sunny.UI.UIStyle.Custom;
            this.btnSubmit.Symbol = 61452;
            this.btnSubmit.TabIndex = 7;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSubmit.Click += new System.EventHandler(this.BtnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.btnCancel.Location = new System.Drawing.Point(350, 354);
            this.btnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.btnCancel.Size = new System.Drawing.Size(100, 36);
            this.btnCancel.Style = Sunny.UI.UIStyle.Custom;
            this.btnCancel.Symbol = 61453;
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "取消";
            this.btnCancel.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // RepairResultForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(22)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(485, 469);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtDowntime);
            this.Controls.Add(this.lblDowntime);
            this.Controls.Add(this.txtCost);
            this.Controls.Add(this.lblCost);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.lblResult);
            this.AllowShowTitle = true;
            this.Name = "RepairResultForm";
            this.Padding = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.ShowTitle = true;
            this.Text = "提交维修结果";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(35)))), ((int)(((byte)(58)))));
            this.TitleFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.ZoomScaleRect = new System.Drawing.Rectangle(22, 22, 480, 370);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UILabel lblResult;
        private Sunny.UI.UITextBox txtResult;
        private Sunny.UI.UILabel lblCost;
        private Sunny.UI.UITextBox txtCost;
        private Sunny.UI.UILabel lblDowntime;
        private Sunny.UI.UITextBox txtDowntime;
        private Sunny.UI.UISymbolButton btnSubmit;
        private Sunny.UI.UISymbolButton btnCancel;
    }
}
