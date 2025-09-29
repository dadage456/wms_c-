namespace PDA
{
    partial class ASWHOutCollectDetailFrm
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
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.TasksTitile = new Entity.HeadLabel();
            this.deleteButton = new System.Windows.Forms.Button();
            this.headerNo = new System.Windows.Forms.ColumnHeader();
            this.detailListView = new System.Windows.Forms.ListView();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.TaskDetailNo = new System.Windows.Forms.ColumnHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.backButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "数量";
            this.columnHeader5.Width = 40;
            // 
            // TasksTitile
            // 
            this.TasksTitile.BackColor = System.Drawing.SystemColors.HotTrack;
            this.TasksTitile.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.TasksTitile.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new System.Drawing.Point(0, 1);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new System.Drawing.Size(477, 24);
            this.TasksTitile.TabIndex = 12;
            this.TasksTitile.Text = "采集明细";
            // 
            // deleteButton
            // 
            this.deleteButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.deleteButton.Location = new System.Drawing.Point(299, 552);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(86, 45);
            this.deleteButton.TabIndex = 10;
            this.deleteButton.Text = "删除";
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // headerNo
            // 
            this.headerNo.Text = "记录索引号";
            this.headerNo.Width = 0;
            // 
            // detailListView
            // 
            this.detailListView.Columns.Add(this.columnHeader10);
            this.detailListView.Columns.Add(this.columnHeader7);
            this.detailListView.Columns.Add(this.columnHeader1);
            this.detailListView.Columns.Add(this.columnHeader2);
            this.detailListView.Columns.Add(this.columnHeader3);
            this.detailListView.Columns.Add(this.columnHeader9);
            this.detailListView.Columns.Add(this.columnHeader6);
            this.detailListView.Columns.Add(this.columnHeader8);
            this.detailListView.Columns.Add(this.columnHeader11);
            this.detailListView.Columns.Add(this.columnHeader4);
            this.detailListView.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.detailListView.FullRowSelect = true;
            this.detailListView.Location = new System.Drawing.Point(3, 52);
            this.detailListView.Name = "detailListView";
            this.detailListView.Size = new System.Drawing.Size(474, 494);
            this.detailListView.TabIndex = 8;
            this.detailListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "托盘号";
            this.columnHeader10.Width = 150;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "库位";
            this.columnHeader7.Width = 150;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "物料号";
            this.columnHeader1.Width = 0;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "计划数";
            this.columnHeader9.Width = 70;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "已采数";
            this.columnHeader6.Width = 70;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "批号";
            this.columnHeader2.Width = 0;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "序列号";
            this.columnHeader3.Width = 0;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "库房";
            this.columnHeader8.Width = 0;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "物料名称";
            this.columnHeader11.Width = 0;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "ColumnHeader";
            this.columnHeader4.Width = 0;
            // 
            // TaskDetailNo
            // 
            this.TaskDetailNo.Text = "任务明细号";
            this.TaskDetailNo.Width = 0;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(3, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 20);
            this.label1.Text = "已采集数据:";
            // 
            // backButton
            // 
            this.backButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.backButton.Location = new System.Drawing.Point(391, 552);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(86, 45);
            this.backButton.TabIndex = 9;
            this.backButton.Text = "返回";
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // ASWHOutCollectDetailFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(480, 600);
            this.ControlBox = false;
            this.Controls.Add(this.TasksTitile);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.detailListView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.backButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ASWHOutCollectDetailFrm";
            this.Text = "采集明细";
            this.Load += new System.EventHandler(this.ASWHDownCollectDetailFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColumnHeader columnHeader5;
        private Entity.HeadLabel TasksTitile;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.ColumnHeader headerNo;
        private System.Windows.Forms.ListView detailListView;
        private System.Windows.Forms.ColumnHeader TaskDetailNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
    }
}