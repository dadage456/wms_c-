namespace PDA
{
    partial class ASWHCheckOrderPalletNo
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
            this.TasksTitile = new Entity.HeadLabel();
            this.Refreshbutton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.INOUTComboBox = new System.Windows.Forms.ComboBox();
            this.INButton = new System.Windows.Forms.Button();
            this.WCSButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.closeButton.Location = new System.Drawing.Point(394, 544);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(80, 40);
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
            this.taskDataGrid.Size = new System.Drawing.Size(469, 471);
            this.taskDataGrid.TabIndex = 203;
            this.taskDataGrid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.taskDataGrid_MouseUp);
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
            this.TasksTitile.Text = "复查托盘信息";
            // 
            // Refreshbutton
            // 
            this.Refreshbutton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.Refreshbutton.Location = new System.Drawing.Point(310, 544);
            this.Refreshbutton.Name = "Refreshbutton";
            this.Refreshbutton.Size = new System.Drawing.Size(80, 40);
            this.Refreshbutton.TabIndex = 204;
            this.Refreshbutton.Text = "刷新";
            this.Refreshbutton.Click += new System.EventHandler(this.Refreshbutton_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(6, 531);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 20);
            this.label1.Text = "盘点位置：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // INOUTComboBox
            // 
            this.INOUTComboBox.DisplayMember = "NAME";
            this.INOUTComboBox.Location = new System.Drawing.Point(5, 554);
            this.INOUTComboBox.Name = "INOUTComboBox";
            this.INOUTComboBox.Size = new System.Drawing.Size(126, 23);
            this.INOUTComboBox.TabIndex = 287;
            this.INOUTComboBox.ValueMember = "CODE";
            // 
            // INButton
            // 
            this.INButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.INButton.Location = new System.Drawing.Point(134, 544);
            this.INButton.Name = "INButton";
            this.INButton.Size = new System.Drawing.Size(65, 40);
            this.INButton.TabIndex = 286;
            this.INButton.Text = "回库";
            this.INButton.Click += new System.EventHandler(this.INButton_Click);
            // 
            // WCSButton
            // 
            this.WCSButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.WCSButton.Location = new System.Drawing.Point(201, 544);
            this.WCSButton.Name = "WCSButton";
            this.WCSButton.Size = new System.Drawing.Size(105, 40);
            this.WCSButton.TabIndex = 285;
            this.WCSButton.Text = "获取来料盘";
            this.WCSButton.Click += new System.EventHandler(this.WCSButton_Click);
            // 
            // ASWHCheckOrderPalletNo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(480, 590);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.INOUTComboBox);
            this.Controls.Add(this.INButton);
            this.Controls.Add(this.WCSButton);
            this.Controls.Add(this.Refreshbutton);
            this.Controls.Add(this.taskDataGrid);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.TasksTitile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ASWHCheckOrderPalletNo";
            this.Text = "上架任务";
            this.Load += new System.EventHandler(this.ASWHWmsToWcs_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.DataGrid taskDataGrid;
        private Entity.HeadLabel TasksTitile;
        private System.Windows.Forms.Button Refreshbutton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox INOUTComboBox;
        private System.Windows.Forms.Button INButton;
        private System.Windows.Forms.Button WCSButton;
    }
}