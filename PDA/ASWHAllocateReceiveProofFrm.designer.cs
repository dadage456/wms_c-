namespace PDA
{
    partial class ASWHAllocateReceiveProofFrm
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
            this.closeButton = new System.Windows.Forms.Button();
            this.taskDataGrid = new System.Windows.Forms.DataGrid();
            this.collectButton = new System.Windows.Forms.Button();
            this.lbmsg = new System.Windows.Forms.Label();
            this.TasksTitile = new Entity.HeadLabel();
            this.QueryDtlBN = new System.Windows.Forms.Button();
            this.txtTaskNo = new System.Windows.Forms.Label();
            this.txtProofNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.QueryBN = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtWorkStation = new System.Windows.Forms.TextBox();
            this.roomLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.closeButton.Location = new System.Drawing.Point(412, 534);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(62, 41);
            this.closeButton.TabIndex = 201;
            this.closeButton.Text = "关闭";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // taskDataGrid
            // 
            this.taskDataGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.taskDataGrid.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.taskDataGrid.Location = new System.Drawing.Point(5, 46);
            this.taskDataGrid.Name = "taskDataGrid";
            this.taskDataGrid.Size = new System.Drawing.Size(469, 395);
            this.taskDataGrid.TabIndex = 203;
            this.taskDataGrid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.taskDataGrid_MouseUp);
            // 
            // collectButton
            // 
            this.collectButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.collectButton.Location = new System.Drawing.Point(257, 534);
            this.collectButton.Name = "collectButton";
            this.collectButton.Size = new System.Drawing.Size(20, 41);
            this.collectButton.TabIndex = 204;
            this.collectButton.Text = "接收";
            this.collectButton.Visible = false;
            this.collectButton.Click += new System.EventHandler(this.collectButton_Click);
            // 
            // lbmsg
            // 
            this.lbmsg.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lbmsg.ForeColor = System.Drawing.Color.Red;
            this.lbmsg.Location = new System.Drawing.Point(8, 529);
            this.lbmsg.Name = "lbmsg";
            this.lbmsg.Size = new System.Drawing.Size(243, 56);
            // 
            // TasksTitile
            // 
            this.TasksTitile.BackColor = System.Drawing.SystemColors.HotTrack;
            this.TasksTitile.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.TasksTitile.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new System.Drawing.Point(1, 0);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new System.Drawing.Size(476, 42);
            this.TasksTitile.TabIndex = 178;
            this.TasksTitile.Text = "接收配盘任务";
            // 
            // QueryDtlBN
            // 
            this.QueryDtlBN.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.QueryDtlBN.Location = new System.Drawing.Point(346, 534);
            this.QueryDtlBN.Name = "QueryDtlBN";
            this.QueryDtlBN.Size = new System.Drawing.Size(63, 41);
            this.QueryDtlBN.TabIndex = 230;
            this.QueryDtlBN.Text = "明细";
            this.QueryDtlBN.Click += new System.EventHandler(this.QueryDtlBN_Click);
            // 
            // txtTaskNo
            // 
            this.txtTaskNo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtTaskNo.Location = new System.Drawing.Point(84, 491);
            this.txtTaskNo.Name = "txtTaskNo";
            this.txtTaskNo.Size = new System.Drawing.Size(190, 20);
            // 
            // txtProofNo
            // 
            this.txtProofNo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtProofNo.Location = new System.Drawing.Point(84, 452);
            this.txtProofNo.Name = "txtProofNo";
            this.txtProofNo.Size = new System.Drawing.Size(190, 26);
            this.txtProofNo.TabIndex = 251;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(14, 491);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 20);
            this.label5.Text = "任务号:";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(14, 455);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 20);
            this.label3.Text = "凭证号:";
            // 
            // QueryBN
            // 
            this.QueryBN.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.QueryBN.Location = new System.Drawing.Point(280, 534);
            this.QueryBN.Name = "QueryBN";
            this.QueryBN.Size = new System.Drawing.Size(63, 41);
            this.QueryBN.TabIndex = 257;
            this.QueryBN.Text = "查询";
            this.QueryBN.Click += new System.EventHandler(this.QueryBN_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(276, 455);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 20);
            this.label2.Text = "工位:";
            // 
            // txtWorkStation
            // 
            this.txtWorkStation.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtWorkStation.Location = new System.Drawing.Point(331, 452);
            this.txtWorkStation.Name = "txtWorkStation";
            this.txtWorkStation.Size = new System.Drawing.Size(145, 26);
            this.txtWorkStation.TabIndex = 300;
            // 
            // roomLabel
            // 
            this.roomLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.roomLabel.Location = new System.Drawing.Point(331, 491);
            this.roomLabel.Name = "roomLabel";
            this.roomLabel.Size = new System.Drawing.Size(145, 20);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(276, 491);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 20);
            this.label1.Text = "库房:";
            // 
            // ASWHAllocateReceiveProofFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(480, 590);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtWorkStation);
            this.Controls.Add(this.roomLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.QueryBN);
            this.Controls.Add(this.txtTaskNo);
            this.Controls.Add(this.txtProofNo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.QueryDtlBN);
            this.Controls.Add(this.lbmsg);
            this.Controls.Add(this.collectButton);
            this.Controls.Add(this.taskDataGrid);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.TasksTitile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ASWHAllocateReceiveProofFrm";
            this.Text = "配盘任务";
            this.Load += new System.EventHandler(this.ASWHAllocateReceiveProofFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.DataGrid taskDataGrid;
        private System.Windows.Forms.Button collectButton;
        private Entity.HeadLabel TasksTitile;
        private System.Windows.Forms.Label lbmsg;
        private System.Windows.Forms.Button QueryDtlBN;
        private System.Windows.Forms.Label txtTaskNo;
        private System.Windows.Forms.TextBox txtProofNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button QueryBN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtWorkStation;
        private System.Windows.Forms.Label roomLabel;
        private System.Windows.Forms.Label label1;
    }
}