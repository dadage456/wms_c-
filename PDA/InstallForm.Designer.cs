namespace PDA
{
    partial class InstallForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pbDownload = new System.Windows.Forms.ProgressBar();
            this.lbDownLoad = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pbDownload
            // 
            this.pbDownload.Location = new System.Drawing.Point(106, 250);
            this.pbDownload.Name = "pbDownload";
            this.pbDownload.Size = new System.Drawing.Size(247, 38);
            this.pbDownload.Visible = false;
            // 
            // lbDownLoad
            // 
            this.lbDownLoad.Location = new System.Drawing.Point(106, 227);
            this.lbDownLoad.Name = "lbDownLoad";
            this.lbDownLoad.Size = new System.Drawing.Size(247, 20);
            this.lbDownLoad.Text = "正在下载。。。";
            this.lbDownLoad.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(243, 103);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(110, 91);
            this.button2.TabIndex = 9;
            this.button2.Text = "退出";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(106, 103);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 91);
            this.button1.TabIndex = 8;
            this.button1.Text = "版本更新";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // InstallForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(480, 640);
            this.ControlBox = false;
            this.Controls.Add(this.pbDownload);
            this.Controls.Add(this.lbDownLoad);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "InstallForm";
            this.Text = "InstallForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar pbDownload;
        private System.Windows.Forms.Label lbDownLoad;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}