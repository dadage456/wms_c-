namespace PDA
{
    partial class MergerTrayFrm
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
            this.TasksTitile = new Entity.HeadLabel();
            this.closeButton = new System.Windows.Forms.Button();
            this.sourceTrayDataGrid = new System.Windows.Forms.DataGrid();
            this.commitButton = new System.Windows.Forms.Button();
            this.sourceTrayTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.targetTrayTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.targetTrayDataGrid = new System.Windows.Forms.DataGrid();
            this.label2 = new System.Windows.Forms.Label();
            this.qtyTextBox = new System.Windows.Forms.TextBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TasksTitile
            // 
            this.TasksTitile.BackColor = System.Drawing.SystemColors.HotTrack;
            this.TasksTitile.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.TasksTitile.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new System.Drawing.Point(1, 0);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new System.Drawing.Size(237, 24);
            this.TasksTitile.TabIndex = 178;
            this.TasksTitile.Text = "合盘任务";
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(170, 263);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(72, 23);
            this.closeButton.TabIndex = 201;
            this.closeButton.Text = "关闭";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // sourceTrayDataGrid
            // 
            this.sourceTrayDataGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.sourceTrayDataGrid.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.sourceTrayDataGrid.Location = new System.Drawing.Point(5, 56);
            this.sourceTrayDataGrid.Name = "sourceTrayDataGrid";
            this.sourceTrayDataGrid.Size = new System.Drawing.Size(237, 75);
            this.sourceTrayDataGrid.TabIndex = 203;
            this.sourceTrayDataGrid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.sourceTrayDataGrid_MouseUp);
            // 
            // commitButton
            // 
            this.commitButton.Location = new System.Drawing.Point(92, 263);
            this.commitButton.Name = "commitButton";
            this.commitButton.Size = new System.Drawing.Size(72, 23);
            this.commitButton.TabIndex = 204;
            this.commitButton.Text = "提交";
            this.commitButton.Click += new System.EventHandler(this.commitButton_Click);
            // 
            // sourceTrayTextBox
            // 
            this.sourceTrayTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.sourceTrayTextBox.Location = new System.Drawing.Point(90, 29);
            this.sourceTrayTextBox.Name = "sourceTrayTextBox";
            this.sourceTrayTextBox.Size = new System.Drawing.Size(150, 21);
            this.sourceTrayTextBox.TabIndex = 226;
            this.sourceTrayTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sourceTrayTextBox_KeyDown);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(4, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 20);
            this.label3.Text = "源托盘号:";
            // 
            // targetTrayTextBox
            // 
            this.targetTrayTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.targetTrayTextBox.Location = new System.Drawing.Point(88, 135);
            this.targetTrayTextBox.Name = "targetTrayTextBox";
            this.targetTrayTextBox.Size = new System.Drawing.Size(154, 21);
            this.targetTrayTextBox.TabIndex = 230;
            this.targetTrayTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.targetTrayTextBox_KeyDown);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(2, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.Text = "目标托盘:";
            // 
            // targetTrayDataGrid
            // 
            this.targetTrayDataGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.targetTrayDataGrid.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.targetTrayDataGrid.Location = new System.Drawing.Point(3, 183);
            this.targetTrayDataGrid.Name = "targetTrayDataGrid";
            this.targetTrayDataGrid.Size = new System.Drawing.Size(237, 75);
            this.targetTrayDataGrid.TabIndex = 229;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(5, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 20);
            this.label2.Text = "转移数量：";
            // 
            // qtyTextBox
            // 
            this.qtyTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.qtyTextBox.Location = new System.Drawing.Point(88, 159);
            this.qtyTextBox.Name = "qtyTextBox";
            this.qtyTextBox.Size = new System.Drawing.Size(76, 21);
            this.qtyTextBox.TabIndex = 233;
            this.qtyTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.qtyTextBox_KeyDown);
            // 
            // cancelButton
            // 
            this.cancelButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.cancelButton.Location = new System.Drawing.Point(168, 159);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(72, 20);
            this.cancelButton.TabIndex = 234;
            this.cancelButton.Text = "撤销";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // MergerTrayFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(245, 289);
            this.ControlBox = false;
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.qtyTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.targetTrayTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.targetTrayDataGrid);
            this.Controls.Add(this.sourceTrayTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.commitButton);
            this.Controls.Add(this.sourceTrayDataGrid);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.TasksTitile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MergerTrayFrm";
            this.Text = "合盘";
            this.Load += new System.EventHandler(this.MergerTrayFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Entity.HeadLabel TasksTitile;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.DataGrid sourceTrayDataGrid;
        private System.Windows.Forms.Button commitButton;
        private System.Windows.Forms.TextBox sourceTrayTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox targetTrayTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGrid targetTrayDataGrid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox qtyTextBox;
        private System.Windows.Forms.Button cancelButton;
    }
}