namespace PDA
{
    partial class ASWHAllocateFrm
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
            this.label5 = new System.Windows.Forms.Label();
            this.roomLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtProofNo = new System.Windows.Forms.TextBox();
            this.txtTaskNo = new System.Windows.Forms.Label();
            this.btnDtlCanel = new System.Windows.Forms.Button();
            this.QueryBN = new System.Windows.Forms.Button();
            this.exceptButton = new System.Windows.Forms.Button();
            this.allPalletCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPalletNo = new System.Windows.Forms.TextBox();
            this.tbxBarcode = new System.Windows.Forms.TextBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtWorkStation = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // taskButton
            // 
            this.taskButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.taskButton.Location = new System.Drawing.Point(3, 40);
            this.taskButton.Name = "taskButton";
            this.taskButton.Size = new System.Drawing.Size(141, 42);
            this.taskButton.TabIndex = 192;
            this.taskButton.Text = "接收配盘任务";
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
            this.taskDataGrid.Size = new System.Drawing.Size(474, 393);
            this.taskDataGrid.TabIndex = 203;
            this.taskDataGrid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.taskDataGrid_MouseUp);
            // 
            // collectButton
            // 
            this.collectButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.collectButton.Location = new System.Drawing.Point(287, 556);
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
            this.TasksTitile.Text = "配盘任务";
            // 
            // btnCanel
            // 
            this.btnCanel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.btnCanel.Location = new System.Drawing.Point(162, 556);
            this.btnCanel.Name = "btnCanel";
            this.btnCanel.Size = new System.Drawing.Size(27, 41);
            this.btnCanel.TabIndex = 228;
            this.btnCanel.Text = "撤销任务";
            this.btnCanel.Visible = false;
            this.btnCanel.Click += new System.EventHandler(this.btnCanel_Click);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(12, 526);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 20);
            this.label5.Text = "任务号:";
            // 
            // roomLabel
            // 
            this.roomLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.roomLabel.Location = new System.Drawing.Point(220, 526);
            this.roomLabel.Name = "roomLabel";
            this.roomLabel.Size = new System.Drawing.Size(49, 20);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(163, 526);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 20);
            this.label1.Text = "库房:";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(12, 490);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 20);
            this.label3.Text = "凭证号:";
            // 
            // txtProofNo
            // 
            this.txtProofNo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtProofNo.Location = new System.Drawing.Point(82, 487);
            this.txtProofNo.Name = "txtProofNo";
            this.txtProofNo.Size = new System.Drawing.Size(190, 26);
            this.txtProofNo.TabIndex = 243;
            this.txtProofNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProofNo_KeyDown);
            // 
            // txtTaskNo
            // 
            this.txtTaskNo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtTaskNo.Location = new System.Drawing.Point(82, 526);
            this.txtTaskNo.Name = "txtTaskNo";
            this.txtTaskNo.Size = new System.Drawing.Size(77, 20);
            // 
            // btnDtlCanel
            // 
            this.btnDtlCanel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.btnDtlCanel.Location = new System.Drawing.Point(351, 556);
            this.btnDtlCanel.Name = "btnDtlCanel";
            this.btnDtlCanel.Size = new System.Drawing.Size(58, 41);
            this.btnDtlCanel.TabIndex = 248;
            this.btnDtlCanel.Text = "明细";
            this.btnDtlCanel.Click += new System.EventHandler(this.btnDtlCanel_Click);
            // 
            // QueryBN
            // 
            this.QueryBN.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.QueryBN.Location = new System.Drawing.Point(96, 556);
            this.QueryBN.Name = "QueryBN";
            this.QueryBN.Size = new System.Drawing.Size(63, 41);
            this.QueryBN.TabIndex = 264;
            this.QueryBN.Text = "查询";
            this.QueryBN.Click += new System.EventHandler(this.QueryBN_Click);
            // 
            // exceptButton
            // 
            this.exceptButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.exceptButton.Location = new System.Drawing.Point(4, 556);
            this.exceptButton.Name = "exceptButton";
            this.exceptButton.Size = new System.Drawing.Size(90, 41);
            this.exceptButton.TabIndex = 263;
            this.exceptButton.Text = "异常处理";
            this.exceptButton.Visible = false;
            this.exceptButton.Click += new System.EventHandler(this.exceptButton_Click);
            // 
            // allPalletCheckBox
            // 
            this.allPalletCheckBox.Location = new System.Drawing.Point(194, 567);
            this.allPalletCheckBox.Name = "allPalletCheckBox";
            this.allPalletCheckBox.Size = new System.Drawing.Size(90, 20);
            this.allPalletCheckBox.TabIndex = 270;
            this.allPalletCheckBox.Text = "所有托盘";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(271, 527);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 20);
            this.label2.Text = "托盘号(配):";
            // 
            // txtPalletNo
            // 
            this.txtPalletNo.BackColor = System.Drawing.Color.Silver;
            this.txtPalletNo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtPalletNo.Location = new System.Drawing.Point(375, 524);
            this.txtPalletNo.Name = "txtPalletNo";
            this.txtPalletNo.ReadOnly = true;
            this.txtPalletNo.Size = new System.Drawing.Size(102, 26);
            this.txtPalletNo.TabIndex = 278;
            // 
            // tbxBarcode
            // 
            this.tbxBarcode.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.tbxBarcode.Location = new System.Drawing.Point(318, 48);
            this.tbxBarcode.Name = "tbxBarcode";
            this.tbxBarcode.Size = new System.Drawing.Size(157, 26);
            this.tbxBarcode.TabIndex = 286;
            // 
            // lblMsg
            // 
            this.lblMsg.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lblMsg.Location = new System.Drawing.Point(151, 51);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(163, 20);
            this.lblMsg.Text = "请扫描托盘号(配)：";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(277, 490);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 20);
            this.label4.Text = "工位:";
            // 
            // txtWorkStation
            // 
            this.txtWorkStation.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtWorkStation.Location = new System.Drawing.Point(331, 487);
            this.txtWorkStation.Name = "txtWorkStation";
            this.txtWorkStation.Size = new System.Drawing.Size(145, 26);
            this.txtWorkStation.TabIndex = 295;
            // 
            // ASWHAllocateFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(480, 600);
            this.ControlBox = false;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtWorkStation);
            this.Controls.Add(this.tbxBarcode);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.txtPalletNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.allPalletCheckBox);
            this.Controls.Add(this.QueryBN);
            this.Controls.Add(this.exceptButton);
            this.Controls.Add(this.btnDtlCanel);
            this.Controls.Add(this.txtTaskNo);
            this.Controls.Add(this.txtProofNo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.roomLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCanel);
            this.Controls.Add(this.collectButton);
            this.Controls.Add(this.taskDataGrid);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.taskButton);
            this.Controls.Add(this.TasksTitile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ASWHAllocateFrm";
            this.Text = "配盘任务";
            this.Load += new System.EventHandler(this.ASWHAllocateFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Entity.HeadLabel TasksTitile;
        private System.Windows.Forms.Button taskButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.DataGrid taskDataGrid;
        private System.Windows.Forms.Button collectButton;
        private System.Windows.Forms.Button btnCanel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label roomLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtProofNo;
        private System.Windows.Forms.Label txtTaskNo;
        private System.Windows.Forms.Button btnDtlCanel;
        private System.Windows.Forms.Button QueryBN;
        private System.Windows.Forms.Button exceptButton;
        private System.Windows.Forms.CheckBox allPalletCheckBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPalletNo;
        private System.Windows.Forms.TextBox tbxBarcode;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtWorkStation;
    }
}