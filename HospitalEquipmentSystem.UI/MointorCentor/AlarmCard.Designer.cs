namespace HospitalEquipmentSystem.UI
{
    partial class AlarmCard
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblLevel = new Sunny.UI.UILabel();
            this.lblInfo = new Sunny.UI.UILabel();
            this.lblTime = new Sunny.UI.UILabel();
            this.SuspendLayout();
            // 
            // lblLevel
            // 
            this.lblLevel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLevel.ForeColor = System.Drawing.Color.White;
            this.lblLevel.Location = new System.Drawing.Point(32, 25);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(100, 23);
            this.lblLevel.TabIndex = 0;
            this.lblLevel.Text = "uiLabel1";
            // 
            // lblInfo
            // 
            this.lblInfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInfo.ForeColor = System.Drawing.Color.White;
            this.lblInfo.Location = new System.Drawing.Point(204, 25);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(646, 44);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "uiLabel2";
            // 
            // lblTime
            // 
            this.lblTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTime.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTime.ForeColor = System.Drawing.Color.White;
            this.lblTime.Location = new System.Drawing.Point(978, 25);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(156, 23);
            this.lblTime.TabIndex = 2;
            this.lblTime.Text = "uiLabel3";
            // 
            // AlarmCard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lblLevel);
            this.Name = "AlarmCard";
            this.Size = new System.Drawing.Size(1146, 84);
            this.ResumeLayout(false);

        }

        #endregion

        public Sunny.UI.UILabel lblLevel;
        public Sunny.UI.UILabel lblInfo;
        public Sunny.UI.UILabel lblTime;
    }
}
