namespace PDA
{
    partial class DaoHuoSaoMaFrmRecv
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
            this.QueryDtlBN = new System.Windows.Forms.Button();
            this.txtTaskNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.QueryBN = new System.Windows.Forms.Button();
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
            this.taskDataGrid.Size = new System.Drawing.Size(469, 450);
            this.taskDataGrid.TabIndex = 203;
            this.taskDataGrid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.taskDataGrid_MouseUp);
            this.taskDataGrid.CurrentCellChanged += new System.EventHandler(this.taskDataGrid_CurrentCellChanged);
            // 
            // collectButton
            // 
            this.collectButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.collectButton.Location = new System.Drawing.Point(321, 534);
            this.collectButton.Name = "collectButton";
            this.collectButton.Size = new System.Drawing.Size(76, 41);
            this.collectButton.TabIndex = 204;
            this.collectButton.Text = "接收";
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
            this.TasksTitile.Text = "接收到货任务";
            // 
            // QueryDtlBN
            // 
            this.QueryDtlBN.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.QueryDtlBN.Location = new System.Drawing.Point(242, 534);
            this.QueryDtlBN.Name = "QueryDtlBN";
            this.QueryDtlBN.Size = new System.Drawing.Size(63, 41);
            this.QueryDtlBN.TabIndex = 230;
            this.QueryDtlBN.Text = "明细";
            this.QueryDtlBN.Click += new System.EventHandler(this.QueryDtlBN_Click);
            // 
            // txtTaskNo
            // 
            this.txtTaskNo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtTaskNo.Location = new System.Drawing.Point(102, 502);
            this.txtTaskNo.Name = "txtTaskNo";
            this.txtTaskNo.Size = new System.Drawing.Size(349, 26);
            this.txtTaskNo.TabIndex = 251;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(14, 505);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 20);
            this.label3.Text = "入库单号:";
            // 
            // QueryBN
            // 
            this.QueryBN.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.QueryBN.Location = new System.Drawing.Point(162, 534);
            this.QueryBN.Name = "QueryBN";
            this.QueryBN.Size = new System.Drawing.Size(63, 41);
            this.QueryBN.TabIndex = 257;
            this.QueryBN.Text = "查询";
            this.QueryBN.Click += new System.EventHandler(this.QueryBN_Click);
            // 
            // DaoHuoSaoMaFrmRecv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(480, 590);
            this.ControlBox = false;
            this.Controls.Add(this.QueryBN);
            this.Controls.Add(this.txtTaskNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.QueryDtlBN);
            this.Controls.Add(this.collectButton);
            this.Controls.Add(this.taskDataGrid);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.TasksTitile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DaoHuoSaoMaFrmRecv";
            this.Text = "接收到货任务";
            this.Load += new System.EventHandler(this.DaoHuoSaoMaFrmRecv_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.DataGrid taskDataGrid;
        private System.Windows.Forms.Button collectButton;
        private Entity.HeadLabel TasksTitile;
        private System.Windows.Forms.Button QueryDtlBN;
        private System.Windows.Forms.TextBox txtTaskNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button QueryBN;
    }
}