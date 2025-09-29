namespace PDA
{
    partial class UpCollectDetailFrm
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
            this.btnDelete = new System.Windows.Forms.Button();
            this.headerNo = new System.Windows.Forms.ColumnHeader();
            this.detailListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.id = new System.Windows.Forms.ColumnHeader();
            this.TaskDetailNo = new System.Windows.Forms.ColumnHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.siteTextBox = new System.Windows.Forms.TextBox();
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
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.btnDelete.Location = new System.Drawing.Point(328, 567);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(70, 30);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // headerNo
            // 
            this.headerNo.Text = "记录索引号";
            this.headerNo.Width = 0;
            // 
            // detailListView
            // 
            this.detailListView.Columns.Add(this.columnHeader1);
            this.detailListView.Columns.Add(this.columnHeader2);
            this.detailListView.Columns.Add(this.columnHeader3);
            this.detailListView.Columns.Add(this.columnHeader4);
            this.detailListView.Columns.Add(this.columnHeader7);
            this.detailListView.Columns.Add(this.columnHeader8);
            this.detailListView.Columns.Add(this.columnHeader6);
            this.detailListView.Columns.Add(this.id);
            this.detailListView.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.detailListView.FullRowSelect = true;
            this.detailListView.Location = new System.Drawing.Point(6, 51);
            this.detailListView.Name = "detailListView";
            this.detailListView.Size = new System.Drawing.Size(471, 510);
            this.detailListView.TabIndex = 8;
            this.detailListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "物料号";
            this.columnHeader1.Width = 140;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "批号";
            this.columnHeader2.Width = 140;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "序列号";
            this.columnHeader3.Width = 140;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "任务数";
            this.columnHeader4.Width = 80;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "采集数";
            this.columnHeader7.Width = 80;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "库位";
            this.columnHeader8.Width = 100;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "库房";
            this.columnHeader6.Width = 70;
            // 
            // id
            // 
            this.id.Text = "id";
            this.id.Width = 0;
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
            // btnBack
            // 
            this.btnBack.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.btnBack.Location = new System.Drawing.Point(404, 567);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(70, 30);
            this.btnBack.TabIndex = 9;
            this.btnBack.Text = "返回";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(6, 567);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "修改库位：";
            this.label2.Visible = false;
            // 
            // siteTextBox
            // 
            this.siteTextBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.siteTextBox.Location = new System.Drawing.Point(112, 567);
            this.siteTextBox.Name = "siteTextBox";
            this.siteTextBox.Size = new System.Drawing.Size(210, 26);
            this.siteTextBox.TabIndex = 15;
            this.siteTextBox.Visible = false;
            this.siteTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.siteTextBox_KeyDown);
            // 
            // UpCollectDetailFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(480, 600);
            this.ControlBox = false;
            this.Controls.Add(this.siteTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TasksTitile);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.detailListView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBack);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpCollectDetailFrm";
            this.Text = "CollectDetail";
            this.Load += new System.EventHandler(this.UpCollectDetailFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColumnHeader columnHeader5;
        private Entity.HeadLabel TasksTitile;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ColumnHeader headerNo;
        private System.Windows.Forms.ListView detailListView;
        private System.Windows.Forms.ColumnHeader TaskDetailNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox siteTextBox;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader id;
    }
}