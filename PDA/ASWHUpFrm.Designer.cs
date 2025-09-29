namespace PDA
{
    partial class ASWHUpFrm
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
            this.btnCanel = new System.Windows.Forms.Button();
            this.txtTaskNo = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnDtlCanel = new System.Windows.Forms.Button();
            this.txtProofNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.QueryBN = new System.Windows.Forms.Button();
            this.exceptButton = new System.Windows.Forms.Button();
            this.allPalletCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtWorkStation = new System.Windows.Forms.TextBox();
            this.roomLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // taskButton
            // 
            this.taskButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.taskButton.Location = new System.Drawing.Point(3, 38);
            this.taskButton.Name = "taskButton";
            this.taskButton.Size = new System.Drawing.Size(132, 38);
            this.taskButton.TabIndex = 192;
            this.taskButton.Text = "接收组盘任务";
            this.taskButton.Click += new System.EventHandler(this.taskButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(416, 555);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(58, 41);
            this.closeButton.TabIndex = 201;
            this.closeButton.Text = "关闭";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // taskDataGrid
            // 
            this.taskDataGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.taskDataGrid.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.taskDataGrid.Location = new System.Drawing.Point(3, 82);
            this.taskDataGrid.Name = "taskDataGrid";
            this.taskDataGrid.Size = new System.Drawing.Size(474, 403);
            this.taskDataGrid.TabIndex = 203;
            this.taskDataGrid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.taskDataGrid_MouseUp);
            // 
            // collectButton
            // 
            this.collectButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.collectButton.Location = new System.Drawing.Point(296, 555);
            this.collectButton.Name = "collectButton";
            this.collectButton.Size = new System.Drawing.Size(58, 41);
            this.collectButton.TabIndex = 204;
            this.collectButton.Text = "采集";
            this.collectButton.Click += new System.EventHandler(this.collectButton_Click);
            // 
            // TasksTitile
            // 
            this.TasksTitile.BackColor = System.Drawing.SystemColors.HotTrack;
            this.TasksTitile.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.TasksTitile.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new System.Drawing.Point(1, 0);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new System.Drawing.Size(476, 32);
            this.TasksTitile.TabIndex = 178;
            this.TasksTitile.Text = "组盘任务";
            // 
            // btnCanel
            // 
            this.btnCanel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.btnCanel.Location = new System.Drawing.Point(164, 555);
            this.btnCanel.Name = "btnCanel";
            this.btnCanel.Size = new System.Drawing.Size(18, 41);
            this.btnCanel.TabIndex = 229;
            this.btnCanel.Text = "任务撤销";
            this.btnCanel.Visible = false;
            this.btnCanel.Click += new System.EventHandler(this.btnCanel_Click);
            // 
            // txtTaskNo
            // 
            this.txtTaskNo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtTaskNo.Location = new System.Drawing.Point(73, 527);
            this.txtTaskNo.Name = "txtTaskNo";
            this.txtTaskNo.Size = new System.Drawing.Size(190, 20);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(3, 527);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 20);
            this.label5.Text = "任务号:";
            // 
            // btnDtlCanel
            // 
            this.btnDtlCanel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.btnDtlCanel.Location = new System.Drawing.Point(356, 555);
            this.btnDtlCanel.Name = "btnDtlCanel";
            this.btnDtlCanel.Size = new System.Drawing.Size(58, 41);
            this.btnDtlCanel.TabIndex = 256;
            this.btnDtlCanel.Text = "明细";
            this.btnDtlCanel.Click += new System.EventHandler(this.btnDtlCanel_Click);
            // 
            // txtProofNo
            // 
            this.txtProofNo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtProofNo.Location = new System.Drawing.Point(73, 488);
            this.txtProofNo.Name = "txtProofNo";
            this.txtProofNo.Size = new System.Drawing.Size(190, 26);
            this.txtProofNo.TabIndex = 251;
            this.txtProofNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.taskcomment_KeyDown);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(3, 491);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 20);
            this.label3.Text = "凭证号:";
            // 
            // QueryBN
            // 
            this.QueryBN.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.QueryBN.Location = new System.Drawing.Point(6, 555);
            this.QueryBN.Name = "QueryBN";
            this.QueryBN.Size = new System.Drawing.Size(63, 41);
            this.QueryBN.TabIndex = 260;
            this.QueryBN.Text = "查询";
            this.QueryBN.Click += new System.EventHandler(this.QueryBN_Click);
            // 
            // exceptButton
            // 
            this.exceptButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.exceptButton.Location = new System.Drawing.Point(72, 555);
            this.exceptButton.Name = "exceptButton";
            this.exceptButton.Size = new System.Drawing.Size(90, 41);
            this.exceptButton.TabIndex = 259;
            this.exceptButton.Text = "异常处理";
            this.exceptButton.Visible = false;
            this.exceptButton.Click += new System.EventHandler(this.exceptButton_Click);
            // 
            // allPalletCheckBox
            // 
            this.allPalletCheckBox.Location = new System.Drawing.Point(198, 565);
            this.allPalletCheckBox.Name = "allPalletCheckBox";
            this.allPalletCheckBox.Size = new System.Drawing.Size(95, 20);
            this.allPalletCheckBox.TabIndex = 266;
            this.allPalletCheckBox.Text = "所有托盘";
            this.allPalletCheckBox.Visible = false;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(272, 491);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 20);
            this.label2.Text = "工位:";
            // 
            // txtWorkStation
            // 
            this.txtWorkStation.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtWorkStation.Location = new System.Drawing.Point(329, 488);
            this.txtWorkStation.Name = "txtWorkStation";
            this.txtWorkStation.Size = new System.Drawing.Size(145, 26);
            this.txtWorkStation.TabIndex = 288;
            // 
            // roomLabel
            // 
            this.roomLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.roomLabel.Location = new System.Drawing.Point(329, 527);
            this.roomLabel.Name = "roomLabel";
            this.roomLabel.Size = new System.Drawing.Size(145, 20);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(272, 527);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 20);
            this.label1.Text = "库房:";
            // 
            // ASWHUpFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(480, 600);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtWorkStation);
            this.Controls.Add(this.roomLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.allPalletCheckBox);
            this.Controls.Add(this.QueryBN);
            this.Controls.Add(this.exceptButton);
            this.Controls.Add(this.txtTaskNo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnDtlCanel);
            this.Controls.Add(this.txtProofNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCanel);
            this.Controls.Add(this.collectButton);
            this.Controls.Add(this.taskDataGrid);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.taskButton);
            this.Controls.Add(this.TasksTitile);
            this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ASWHUpFrm";
            this.Text = "组盘";
            this.Load += new System.EventHandler(this.ASWHUpFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Entity.HeadLabel TasksTitile;
        private System.Windows.Forms.Button taskButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.DataGrid taskDataGrid;
        private System.Windows.Forms.Button collectButton;
        private System.Windows.Forms.Button btnCanel;
        private System.Windows.Forms.Label txtTaskNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnDtlCanel;
        private System.Windows.Forms.TextBox txtProofNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button QueryBN;
        private System.Windows.Forms.Button exceptButton;
        private System.Windows.Forms.CheckBox allPalletCheckBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtWorkStation;
        private System.Windows.Forms.Label roomLabel;
        private System.Windows.Forms.Label label1;
    }
}