namespace PDA
{
    partial class PrintFrm
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
            this.print = new System.Windows.Forms.Button();
            this.canclePrint = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.TasksTitile = new Entity.HeadLabel();
            this.SuspendLayout();
            // 
            // print
            // 
            this.print.Location = new System.Drawing.Point(33, 216);
            this.print.Name = "print";
            this.print.Size = new System.Drawing.Size(82, 36);
            this.print.TabIndex = 179;
            this.print.Text = "打印";
            this.print.Click += new System.EventHandler(this.print_Click);
            // 
            // canclePrint
            // 
            this.canclePrint.Location = new System.Drawing.Point(166, 216);
            this.canclePrint.Name = "canclePrint";
            this.canclePrint.Size = new System.Drawing.Size(82, 36);
            this.canclePrint.TabIndex = 180;
            this.canclePrint.Text = "取消打印";
            this.canclePrint.Click += new System.EventHandler(this.canclePrint_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(33, 152);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(100, 20);
            this.radioButton1.TabIndex = 181;
            this.radioButton1.Text = "50*50MM";
            // 
            // radioButton2
            // 
            this.radioButton2.Location = new System.Drawing.Point(33, 109);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(100, 20);
            this.radioButton2.TabIndex = 182;
            this.radioButton2.TabStop = false;
            this.radioButton2.Text = "30*30MM";
            // 
            // radioButton3
            // 
            this.radioButton3.Location = new System.Drawing.Point(33, 69);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(100, 20);
            this.radioButton3.TabIndex = 183;
            this.radioButton3.TabStop = false;
            this.radioButton3.Text = "20*20MM";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(33, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "打印规格选择:";
            // 
            // TasksTitile
            // 
            this.TasksTitile.BackColor = System.Drawing.SystemColors.HotTrack;
            this.TasksTitile.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.TasksTitile.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new System.Drawing.Point(1, 0);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new System.Drawing.Size(289, 24);
            this.TasksTitile.TabIndex = 178;
            this.TasksTitile.Text = "实物码打印";
            this.TasksTitile.Click += new System.EventHandler(this.TasksTitile_Click);
            // 
            // PrintFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(290, 276);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.canclePrint);
            this.Controls.Add(this.print);
            this.Controls.Add(this.TasksTitile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrintFrm";
            this.Text = "实物码打印";
            this.Load += new System.EventHandler(this.PrintFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Entity.HeadLabel TasksTitile;
        private System.Windows.Forms.Button print;
        private System.Windows.Forms.Button canclePrint;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.Label label1;
    }
}