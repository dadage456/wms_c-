namespace PDA
{
    partial class PInventoryFrm
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
            this.barcodeTextBox = new System.Windows.Forms.TextBox();
            this.commitButton = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.quantityLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.materialLabel = new System.Windows.Forms.Label();
            this.siteLlabel = new System.Windows.Forms.Label();
            this.detailListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.deleteButton = new System.Windows.Forms.Button();
            this.TasksTitile = new Entity.HeadLabel();
            this.batchNoLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.siteCheckBox = new System.Windows.Forms.CheckBox();
            this.matCheckBox = new System.Windows.Forms.CheckBox();
            this.snLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.billNoLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.roomNoLabel = new System.Windows.Forms.Label();
            this.resetButton = new System.Windows.Forms.Button();
            this.exceptButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // barcodeTextBox
            // 
            this.barcodeTextBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.barcodeTextBox.Location = new System.Drawing.Point(192, 42);
            this.barcodeTextBox.Name = "barcodeTextBox";
            this.barcodeTextBox.Size = new System.Drawing.Size(285, 26);
            this.barcodeTextBox.TabIndex = 172;
            this.barcodeTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.barcodeTextBox_KeyDown);
            // 
            // commitButton
            // 
            this.commitButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.commitButton.Location = new System.Drawing.Point(190, 242);
            this.commitButton.Name = "commitButton";
            this.commitButton.Size = new System.Drawing.Size(92, 40);
            this.commitButton.TabIndex = 173;
            this.commitButton.Text = "提交";
            this.commitButton.Click += new System.EventHandler(this.commitButton_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lblMsg.Location = new System.Drawing.Point(8, 43);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(178, 20);
            this.lblMsg.Text = "请扫描盘库单：";
            // 
            // closeButton
            // 
            this.closeButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.closeButton.Location = new System.Drawing.Point(387, 242);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(87, 40);
            this.closeButton.TabIndex = 174;
            this.closeButton.Text = "关闭";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // quantityLabel
            // 
            this.quantityLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.quantityLabel.Location = new System.Drawing.Point(88, 216);
            this.quantityLabel.Name = "quantityLabel";
            this.quantityLabel.Size = new System.Drawing.Size(98, 17);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(9, 216);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 17);
            this.label4.Text = "数量：";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(113, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 17);
            this.label3.Text = "料号：";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(113, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 17);
            this.label1.Text = "库位：";
            // 
            // materialLabel
            // 
            this.materialLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.materialLabel.Location = new System.Drawing.Point(192, 125);
            this.materialLabel.Name = "materialLabel";
            this.materialLabel.Size = new System.Drawing.Size(285, 26);
            // 
            // siteLlabel
            // 
            this.siteLlabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.siteLlabel.Location = new System.Drawing.Point(192, 98);
            this.siteLlabel.Name = "siteLlabel";
            this.siteLlabel.Size = new System.Drawing.Size(285, 26);
            // 
            // detailListView
            // 
            this.detailListView.CheckBoxes = true;
            this.detailListView.Columns.Add(this.columnHeader1);
            this.detailListView.Columns.Add(this.columnHeader2);
            this.detailListView.Columns.Add(this.columnHeader4);
            this.detailListView.Columns.Add(this.columnHeader3);
            this.detailListView.Columns.Add(this.columnHeader5);
            this.detailListView.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.detailListView.FullRowSelect = true;
            this.detailListView.Location = new System.Drawing.Point(3, 287);
            this.detailListView.Name = "detailListView";
            this.detailListView.Size = new System.Drawing.Size(474, 313);
            this.detailListView.TabIndex = 180;
            this.detailListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "料号";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "批号";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "序列号";
            this.columnHeader4.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "库位";
            this.columnHeader3.Width = 100;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "盘点数";
            this.columnHeader5.Width = 80;
            // 
            // deleteButton
            // 
            this.deleteButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.deleteButton.Location = new System.Drawing.Point(5, 242);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(77, 40);
            this.deleteButton.TabIndex = 191;
            this.deleteButton.Text = "删除";
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // TasksTitile
            // 
            this.TasksTitile.BackColor = System.Drawing.SystemColors.HotTrack;
            this.TasksTitile.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.TasksTitile.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new System.Drawing.Point(1, 0);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new System.Drawing.Size(476, 37);
            this.TasksTitile.TabIndex = 178;
            this.TasksTitile.Text = "平库盘点";
            // 
            // batchNoLabel
            // 
            this.batchNoLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.batchNoLabel.Location = new System.Drawing.Point(192, 158);
            this.batchNoLabel.Name = "batchNoLabel";
            this.batchNoLabel.Size = new System.Drawing.Size(285, 26);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(113, 158);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 17);
            this.label5.Text = "批号：";
            // 
            // siteCheckBox
            // 
            this.siteCheckBox.AutoCheck = false;
            this.siteCheckBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.siteCheckBox.Location = new System.Drawing.Point(192, 216);
            this.siteCheckBox.Name = "siteCheckBox";
            this.siteCheckBox.Size = new System.Drawing.Size(90, 20);
            this.siteCheckBox.TabIndex = 199;
            this.siteCheckBox.Text = "按库位";
            // 
            // matCheckBox
            // 
            this.matCheckBox.AutoCheck = false;
            this.matCheckBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.matCheckBox.Location = new System.Drawing.Point(379, 216);
            this.matCheckBox.Name = "matCheckBox";
            this.matCheckBox.Size = new System.Drawing.Size(90, 20);
            this.matCheckBox.TabIndex = 200;
            this.matCheckBox.Text = "按物料";
            // 
            // snLabel
            // 
            this.snLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.snLabel.Location = new System.Drawing.Point(192, 186);
            this.snLabel.Name = "snLabel";
            this.snLabel.Size = new System.Drawing.Size(285, 26);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label6.Location = new System.Drawing.Point(113, 186);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 17);
            this.label6.Text = "序列号：";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(9, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 17);
            this.label2.Text = "盘库单号：";
            // 
            // billNoLabel
            // 
            this.billNoLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.billNoLabel.Location = new System.Drawing.Point(113, 72);
            this.billNoLabel.Name = "billNoLabel";
            this.billNoLabel.Size = new System.Drawing.Size(160, 26);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label7.Location = new System.Drawing.Point(279, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 17);
            this.label7.Text = "库房号：";
            // 
            // roomNoLabel
            // 
            this.roomNoLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.roomNoLabel.Location = new System.Drawing.Point(360, 73);
            this.roomNoLabel.Name = "roomNoLabel";
            this.roomNoLabel.Size = new System.Drawing.Size(117, 26);
            // 
            // resetButton
            // 
            this.resetButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.resetButton.Location = new System.Drawing.Point(89, 242);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(95, 40);
            this.resetButton.TabIndex = 210;
            this.resetButton.Text = "重新采集";
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // exceptButton
            // 
            this.exceptButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.exceptButton.Location = new System.Drawing.Point(288, 242);
            this.exceptButton.Name = "exceptButton";
            this.exceptButton.Size = new System.Drawing.Size(93, 40);
            this.exceptButton.TabIndex = 227;
            this.exceptButton.Text = "异常处理";
            // 
            // PInventoryFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(480, 600);
            this.ControlBox = false;
            this.Controls.Add(this.exceptButton);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.roomNoLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.billNoLabel);
            this.Controls.Add(this.snLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.matCheckBox);
            this.Controls.Add(this.siteCheckBox);
            this.Controls.Add(this.batchNoLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.barcodeTextBox);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.detailListView);
            this.Controls.Add(this.quantityLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.materialLabel);
            this.Controls.Add(this.siteLlabel);
            this.Controls.Add(this.TasksTitile);
            this.Controls.Add(this.commitButton);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.closeButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PInventoryFrm";
            this.Text = "盘库";
            this.Load += new System.EventHandler(this.MaterialInventoryFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Entity.HeadLabel TasksTitile;
        private System.Windows.Forms.TextBox barcodeTextBox;
        private System.Windows.Forms.Button commitButton;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label quantityLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label materialLabel;
        private System.Windows.Forms.Label siteLlabel;
        private System.Windows.Forms.ListView detailListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label batchNoLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.CheckBox siteCheckBox;
        private System.Windows.Forms.CheckBox matCheckBox;
        private System.Windows.Forms.Label snLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label billNoLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label roomNoLabel;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button exceptButton;
    }
}