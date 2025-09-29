namespace PDA
{
    partial class GoodsDownReceiveItemFrm
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
            this.TasksTitile = new Entity.HeadLabel();
            this.SelectBN = new System.Windows.Forms.Button();
            this.txtTaskNo = new System.Windows.Forms.Label();
            this.txtProofNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.roomLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbmsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.closeButton.Location = new System.Drawing.Point(410, 534);
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
            this.taskDataGrid.Location = new System.Drawing.Point(5, 46);
            this.taskDataGrid.Name = "taskDataGrid";
            this.taskDataGrid.Size = new System.Drawing.Size(469, 407);
            this.taskDataGrid.TabIndex = 203;
            this.taskDataGrid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.taskDataGrid_MouseUp);
            // 
            // collectButton
            // 
            this.collectButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.collectButton.Location = new System.Drawing.Point(343, 534);
            this.collectButton.Name = "collectButton";
            this.collectButton.Size = new System.Drawing.Size(65, 41);
            this.collectButton.TabIndex = 204;
            this.collectButton.Text = "撤销";
            this.collectButton.Click += new System.EventHandler(this.collectButton_Click);
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
            this.TasksTitile.Text = "接收下架任务";
            // 
            // SelectBN
            // 
            this.SelectBN.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.SelectBN.Location = new System.Drawing.Point(277, 534);
            this.SelectBN.Name = "SelectBN";
            this.SelectBN.Size = new System.Drawing.Size(63, 41);
            this.SelectBN.TabIndex = 267;
            this.SelectBN.Text = "全选";
            this.SelectBN.Click += new System.EventHandler(this.SelectBN_Click);
            // 
            // txtTaskNo
            // 
            this.txtTaskNo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtTaskNo.Location = new System.Drawing.Point(79, 498);
            this.txtTaskNo.Name = "txtTaskNo";
            this.txtTaskNo.Size = new System.Drawing.Size(148, 20);
            // 
            // txtProofNo
            // 
            this.txtProofNo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtProofNo.Location = new System.Drawing.Point(79, 459);
            this.txtProofNo.Name = "txtProofNo";
            this.txtProofNo.Size = new System.Drawing.Size(388, 26);
            this.txtProofNo.TabIndex = 273;
            this.txtProofNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProofNo_KeyDown);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(9, 498);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 20);
            this.label5.Text = "任务号:";
            // 
            // roomLabel
            // 
            this.roomLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.roomLabel.Location = new System.Drawing.Point(326, 498);
            this.roomLabel.Name = "roomLabel";
            this.roomLabel.Size = new System.Drawing.Size(141, 20);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(267, 498);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 20);
            this.label1.Text = "库房:";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(9, 462);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 20);
            this.label3.Text = "凭证号:";
            // 
            // lbmsg
            // 
            this.lbmsg.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lbmsg.ForeColor = System.Drawing.Color.Red;
            this.lbmsg.Location = new System.Drawing.Point(9, 534);
            this.lbmsg.Name = "lbmsg";
            this.lbmsg.Size = new System.Drawing.Size(241, 43);
            // 
            // GoodsDownReceiveItemFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(480, 590);
            this.ControlBox = false;
            this.Controls.Add(this.lbmsg);
            this.Controls.Add(this.txtTaskNo);
            this.Controls.Add(this.txtProofNo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.roomLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SelectBN);
            this.Controls.Add(this.collectButton);
            this.Controls.Add(this.taskDataGrid);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.TasksTitile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GoodsDownReceiveItemFrm";
            this.Text = "上架任务";
            this.Load += new System.EventHandler(this.GoodsDownReceiveItemFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.DataGrid taskDataGrid;
        private System.Windows.Forms.Button collectButton;
        private Entity.HeadLabel TasksTitile;
        private System.Windows.Forms.Button SelectBN;
        private System.Windows.Forms.Label txtTaskNo;
        private System.Windows.Forms.TextBox txtProofNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label roomLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbmsg;
    }
}