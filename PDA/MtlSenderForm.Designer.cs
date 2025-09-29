namespace PDA
{
    partial class MtlSenderForm
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
            this.qtyLabel = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.commitButton = new System.Windows.Forms.Button();
            this.matCodeLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxBarcode = new System.Windows.Forms.TextBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.detailListView = new System.Windows.Forms.ListView();
            this.货架号 = new System.Windows.Forms.ColumnHeader();
            this.物料号 = new System.Windows.Forms.ColumnHeader();
            this.数量 = new System.Windows.Forms.ColumnHeader();
            this.btnDelete = new System.Windows.Forms.Button();
            this.locationLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbMinQty = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbDefaultQty = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbInvQty = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.TasksTitile = new Entity.HeadLabel();
            this.exceptButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // qtyLabel
            // 
            this.qtyLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.qtyLabel.Location = new System.Drawing.Point(119, 177);
            this.qtyLabel.Name = "qtyLabel";
            this.qtyLabel.Size = new System.Drawing.Size(347, 17);
            // 
            // closeButton
            // 
            this.closeButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.closeButton.Location = new System.Drawing.Point(377, 212);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(85, 39);
            this.closeButton.TabIndex = 225;
            this.closeButton.Text = "关闭";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // commitButton
            // 
            this.commitButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.commitButton.Location = new System.Drawing.Point(195, 212);
            this.commitButton.Name = "commitButton";
            this.commitButton.Size = new System.Drawing.Size(85, 39);
            this.commitButton.TabIndex = 224;
            this.commitButton.Text = "提交";
            this.commitButton.Click += new System.EventHandler(this.commitButton_Click);
            // 
            // matCodeLabel
            // 
            this.matCodeLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.matCodeLabel.Location = new System.Drawing.Point(119, 114);
            this.matCodeLabel.Name = "matCodeLabel";
            this.matCodeLabel.Size = new System.Drawing.Size(148, 17);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(8, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 20);
            this.label3.Text = "物料号：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(8, 174);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 20);
            this.label4.Text = "数量：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbxBarcode
            // 
            this.tbxBarcode.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.tbxBarcode.Location = new System.Drawing.Point(119, 38);
            this.tbxBarcode.Name = "tbxBarcode";
            this.tbxBarcode.Size = new System.Drawing.Size(347, 26);
            this.tbxBarcode.TabIndex = 219;
            this.tbxBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxBarcode_KeyDown);
            // 
            // lblMsg
            // 
            this.lblMsg.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lblMsg.Location = new System.Drawing.Point(8, 41);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(105, 20);
            this.lblMsg.Text = "请扫描：";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // detailListView
            // 
            this.detailListView.Columns.Add(this.货架号);
            this.detailListView.Columns.Add(this.物料号);
            this.detailListView.Columns.Add(this.数量);
            this.detailListView.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.detailListView.FullRowSelect = true;
            this.detailListView.Location = new System.Drawing.Point(5, 257);
            this.detailListView.Name = "detailListView";
            this.detailListView.Size = new System.Drawing.Size(471, 340);
            this.detailListView.TabIndex = 234;
            this.detailListView.View = System.Windows.Forms.View.Details;
            // 
            // 货架号
            // 
            this.货架号.Text = "货架号";
            this.货架号.Width = 160;
            // 
            // 物料号
            // 
            this.物料号.Text = "物料号";
            this.物料号.Width = 170;
            // 
            // 数量
            // 
            this.数量.Text = "数量";
            this.数量.Width = 140;
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.btnDelete.Location = new System.Drawing.Point(286, 212);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(85, 39);
            this.btnDelete.TabIndex = 239;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // locationLabel
            // 
            this.locationLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.locationLabel.Location = new System.Drawing.Point(119, 78);
            this.locationLabel.Name = "locationLabel";
            this.locationLabel.Size = new System.Drawing.Size(347, 20);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(8, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 20);
            this.label1.Text = "货架号：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbMinQty
            // 
            this.lbMinQty.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lbMinQty.Location = new System.Drawing.Point(119, 143);
            this.lbMinQty.Name = "lbMinQty";
            this.lbMinQty.Size = new System.Drawing.Size(148, 19);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(-18, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 20);
            this.label2.Text = "最小包装数：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbDefaultQty
            // 
            this.lbDefaultQty.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lbDefaultQty.Location = new System.Drawing.Point(333, 145);
            this.lbDefaultQty.Name = "lbDefaultQty";
            this.lbDefaultQty.Size = new System.Drawing.Size(129, 17);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label6.Location = new System.Drawing.Point(261, 142);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 20);
            this.label6.Text = "配送量：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbInvQty
            // 
            this.lbInvQty.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lbInvQty.Location = new System.Drawing.Point(333, 111);
            this.lbInvQty.Name = "lbInvQty";
            this.lbInvQty.Size = new System.Drawing.Size(133, 19);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label8.Location = new System.Drawing.Point(264, 111);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 20);
            this.label8.Text = "库存数：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TasksTitile
            // 
            this.TasksTitile.BackColor = System.Drawing.SystemColors.HotTrack;
            this.TasksTitile.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.TasksTitile.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new System.Drawing.Point(2, 1);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new System.Drawing.Size(476, 34);
            this.TasksTitile.TabIndex = 223;
            this.TasksTitile.Text = "拉式发料";
            // 
            // exceptButton
            // 
            this.exceptButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.exceptButton.Location = new System.Drawing.Point(96, 212);
            this.exceptButton.Name = "exceptButton";
            this.exceptButton.Size = new System.Drawing.Size(93, 39);
            this.exceptButton.TabIndex = 247;
            this.exceptButton.Text = "异常处理";
            // 
            // MtlSenderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(480, 600);
            this.ControlBox = false;
            this.Controls.Add(this.exceptButton);
            this.Controls.Add(this.lbInvQty);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lbDefaultQty);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lbMinQty);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.detailListView);
            this.Controls.Add(this.qtyLabel);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.commitButton);
            this.Controls.Add(this.locationLabel);
            this.Controls.Add(this.matCodeLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TasksTitile);
            this.Controls.Add(this.tbxBarcode);
            this.Controls.Add(this.lblMsg);
            this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MtlSenderForm";
            this.Text = "拉式发料";
            this.Load += new System.EventHandler(this.TransferForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label qtyLabel;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button commitButton;
        private System.Windows.Forms.Label matCodeLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private Entity.HeadLabel TasksTitile;
        private System.Windows.Forms.TextBox tbxBarcode;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.ListView detailListView;
        private System.Windows.Forms.ColumnHeader 物料号;
        private System.Windows.Forms.ColumnHeader 数量;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ColumnHeader 货架号;
        private System.Windows.Forms.Label locationLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbMinQty;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbDefaultQty;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbInvQty;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button exceptButton;

    }
}