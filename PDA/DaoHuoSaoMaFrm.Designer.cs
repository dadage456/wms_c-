namespace PDA
{
    partial class DaoHuoSaoMaFrm
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
            this.taskButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.taskDataGrid = new System.Windows.Forms.DataGrid();
            this.collectButton = new System.Windows.Forms.Button();
            this.TasksTitile = new Entity.HeadLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTaskNo = new System.Windows.Forms.TextBox();
            this.btnDtlCanel = new System.Windows.Forms.Button();
            this.btnCanel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // taskButton
            // 
            this.taskButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.taskButton.Location = new System.Drawing.Point(3, 40);
            this.taskButton.Name = "taskButton";
            this.taskButton.Size = new System.Drawing.Size(141, 42);
            this.taskButton.TabIndex = 192;
            this.taskButton.Text = "接收到货任务";
            this.taskButton.Click += new System.EventHandler(this.taskButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.closeButton.Location = new System.Drawing.Point(414, 556);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(63, 41);
            this.closeButton.TabIndex = 201;
            this.closeButton.Text = "关闭";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // taskDataGrid
            // 
            this.taskDataGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.taskDataGrid.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.taskDataGrid.Location = new System.Drawing.Point(3, 88);
            this.taskDataGrid.Name = "taskDataGrid";
            this.taskDataGrid.Size = new System.Drawing.Size(474, 422);
            this.taskDataGrid.TabIndex = 203;
            this.taskDataGrid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.taskDataGrid_MouseUp);
            // 
            // collectButton
            // 
            this.collectButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.collectButton.Location = new System.Drawing.Point(284, 556);
            this.collectButton.Name = "collectButton";
            this.collectButton.Size = new System.Drawing.Size(60, 41);
            this.collectButton.TabIndex = 204;
            this.collectButton.Text = "采集";
            this.collectButton.Click += new System.EventHandler(this.collectButton_Click);
            // 
            // TasksTitile
            // 
            this.TasksTitile.BackColor = System.Drawing.SystemColors.HotTrack;
            this.TasksTitile.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.TasksTitile.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new System.Drawing.Point(3, 0);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new System.Drawing.Size(474, 34);
            this.TasksTitile.TabIndex = 178;
            this.TasksTitile.Text = "到货任务";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(12, 523);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 20);
            this.label3.Text = "入库单号:";
            // 
            // txtTaskNo
            // 
            this.txtTaskNo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtTaskNo.Location = new System.Drawing.Point(107, 520);
            this.txtTaskNo.Name = "txtTaskNo";
            this.txtTaskNo.Size = new System.Drawing.Size(336, 26);
            this.txtTaskNo.TabIndex = 243;
            this.txtTaskNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProofNo_KeyDown);
            // 
            // btnDtlCanel
            // 
            this.btnDtlCanel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.btnDtlCanel.Location = new System.Drawing.Point(350, 556);
            this.btnDtlCanel.Name = "btnDtlCanel";
            this.btnDtlCanel.Size = new System.Drawing.Size(58, 41);
            this.btnDtlCanel.TabIndex = 248;
            this.btnDtlCanel.Text = "明细";
            this.btnDtlCanel.Click += new System.EventHandler(this.btnDtlCanel_Click);
            // 
            // btnCanel
            // 
            this.btnCanel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.btnCanel.Location = new System.Drawing.Point(3, 559);
            this.btnCanel.Name = "btnCanel";
            this.btnCanel.Size = new System.Drawing.Size(58, 41);
            this.btnCanel.TabIndex = 250;
            this.btnCanel.Text = "撤销";
            this.btnCanel.Click += new System.EventHandler(this.btnCanel_Click);
            // 
            // DaoHuoSaoMaFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(480, 600);
            this.ControlBox = false;
            this.Controls.Add(this.btnCanel);
            this.Controls.Add(this.btnDtlCanel);
            this.Controls.Add(this.txtTaskNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.collectButton);
            this.Controls.Add(this.taskDataGrid);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.taskButton);
            this.Controls.Add(this.TasksTitile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DaoHuoSaoMaFrm";
            this.Text = "到货检验任务";
            this.Load += new System.EventHandler(this.DaoHuoSaoMaFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Entity.HeadLabel TasksTitile;
        private System.Windows.Forms.Button taskButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.DataGrid taskDataGrid;
        private System.Windows.Forms.Button collectButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTaskNo;
        private System.Windows.Forms.Button btnDtlCanel;
        private System.Windows.Forms.Button btnCanel;
    }
}