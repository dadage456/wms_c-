namespace PDA
{
    partial class ASWHUpCollectDetailFrm
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
            this.deleteButton = new System.Windows.Forms.Button();
            this.headerNo = new System.Windows.Forms.ColumnHeader();
            this.detailListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.序列 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.id = new System.Windows.Forms.ColumnHeader();
            this.TaskDetailNo = new System.Windows.Forms.ColumnHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.siteTextBox = new System.Windows.Forms.TextBox();
            this.TasksTitile = new Entity.HeadLabel();
            this.qtyTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "数量";
            this.columnHeader5.Width = 40;
            // 
            // deleteButton
            // 
            this.deleteButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.deleteButton.Location = new System.Drawing.Point(303, 550);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(84, 40);
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
            this.detailListView.Columns.Add(this.columnHeader1);
            this.detailListView.Columns.Add(this.columnHeader2);
            this.detailListView.Columns.Add(this.columnHeader3);
            this.detailListView.Columns.Add(this.序列);
            this.detailListView.Columns.Add(this.columnHeader4);
            this.detailListView.Columns.Add(this.columnHeader6);
            this.detailListView.Columns.Add(this.id);
            this.detailListView.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.detailListView.FullRowSelect = true;
            this.detailListView.Location = new System.Drawing.Point(6, 51);
            this.detailListView.Name = "detailListView";
            this.detailListView.Size = new System.Drawing.Size(471, 436);
            this.detailListView.TabIndex = 8;
            this.detailListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "托盘号";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "物料号";
            this.columnHeader2.Width = 140;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "批次号";
            this.columnHeader3.Width = 120;
            // 
            // 序列
            // 
            this.序列.Text = "序列";
            this.序列.Width = 60;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "数量";
            this.columnHeader4.Width = 60;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "库位";
            this.columnHeader6.Width = 120;
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
            this.btnBack.Location = new System.Drawing.Point(393, 550);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(84, 40);
            this.btnBack.TabIndex = 9;
            this.btnBack.Text = "返回";
            this.btnBack.Click += new System.EventHandler(this.backButton_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(5, 490);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "修改库位：";
            this.label2.Visible = false;
            // 
            // siteTextBox
            // 
            this.siteTextBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.siteTextBox.Location = new System.Drawing.Point(111, 490);
            this.siteTextBox.Name = "siteTextBox";
            this.siteTextBox.Size = new System.Drawing.Size(366, 26);
            this.siteTextBox.TabIndex = 15;
            this.siteTextBox.Visible = false;
            this.siteTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.siteTextBox_KeyDown);
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
            // qtyTextBox
            // 
            this.qtyTextBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.qtyTextBox.Location = new System.Drawing.Point(111, 518);
            this.qtyTextBox.Name = "qtyTextBox";
            this.qtyTextBox.Size = new System.Drawing.Size(366, 26);
            this.qtyTextBox.TabIndex = 19;
            this.qtyTextBox.Visible = false;
            this.qtyTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.qtyTextBox_KeyDown);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(5, 518);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.Text = "修改数量：";
            this.label3.Visible = false;
            // 
            // ASWHUpCollectDetailFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(480, 600);
            this.ControlBox = false;
            this.Controls.Add(this.qtyTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.siteTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TasksTitile);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.detailListView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBack);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ASWHUpCollectDetailFrm";
            this.Text = "CollectDetail";
            this.Load += new System.EventHandler(this.BindingCollectDetailFrm_Load);
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
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox siteTextBox;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.TextBox qtyTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ColumnHeader id;
        private System.Windows.Forms.ColumnHeader 序列;
    }
}