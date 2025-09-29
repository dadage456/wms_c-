namespace PDA
{
    partial class GoodsUpTaskItemFrm
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
            this.tbxBarcode = new System.Windows.Forms.TextBox();
            this.detailListView = new System.Windows.Forms.ListView();
            this.lblMsg = new System.Windows.Forms.Label();
            this.collectItemButton = new System.Windows.Forms.Button();
            this.matCodeLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.commitButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.batchLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.serialNoLabel = new System.Windows.Forms.Label();
            this.qtyLabel = new System.Windows.Forms.Label();
            this.siteLabel = new System.Windows.Forms.Label();
            this.lbInv = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.exceptButton = new System.Windows.Forms.Button();
            this.QueryListView = new System.Windows.Forms.ListView();
            this.TasksTitile = new Entity.HeadLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.matinnercode = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbxBarcode
            // 
            this.tbxBarcode.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.tbxBarcode.Location = new System.Drawing.Point(153, 30);
            this.tbxBarcode.Name = "tbxBarcode";
            this.tbxBarcode.Size = new System.Drawing.Size(324, 26);
            this.tbxBarcode.TabIndex = 172;
            this.tbxBarcode.TextChanged += new System.EventHandler(this.tbxBarcode_TextChanged);
            this.tbxBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxBarcode_KeyDown);
            // 
            // detailListView
            // 
            this.detailListView.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.detailListView.FullRowSelect = true;
            this.detailListView.Location = new System.Drawing.Point(2, 287);
            this.detailListView.Name = "detailListView";
            this.detailListView.Size = new System.Drawing.Size(474, 310);
            this.detailListView.TabIndex = 175;
            this.detailListView.View = System.Windows.Forms.View.Details;
            // 
            // lblMsg
            // 
            this.lblMsg.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lblMsg.Location = new System.Drawing.Point(6, 32);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(141, 20);
            this.lblMsg.Text = "请扫描";
            // 
            // collectItemButton
            // 
            this.collectItemButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.collectItemButton.Location = new System.Drawing.Point(6, 152);
            this.collectItemButton.Name = "collectItemButton";
            this.collectItemButton.Size = new System.Drawing.Size(96, 39);
            this.collectItemButton.TabIndex = 174;
            this.collectItemButton.Text = "采集明细";
            this.collectItemButton.Click += new System.EventHandler(this.collectItemButton_Click);
            // 
            // matCodeLabel
            // 
            this.matCodeLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.matCodeLabel.Location = new System.Drawing.Point(69, 83);
            this.matCodeLabel.Name = "matCodeLabel";
            this.matCodeLabel.Size = new System.Drawing.Size(211, 17);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(6, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 20);
            this.label3.Text = "料号：";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(286, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 20);
            this.label4.Text = "数量：";
            // 
            // commitButton
            // 
            this.commitButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.commitButton.Location = new System.Drawing.Point(253, 152);
            this.commitButton.Name = "commitButton";
            this.commitButton.Size = new System.Drawing.Size(96, 39);
            this.commitButton.TabIndex = 201;
            this.commitButton.Text = "提交";
            this.commitButton.Click += new System.EventHandler(this.commitButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.closeButton.Location = new System.Drawing.Point(378, 152);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(96, 39);
            this.closeButton.TabIndex = 202;
            this.closeButton.Text = "关闭";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(87, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 20);
            this.label5.Text = "库位：";
            // 
            // batchLabel
            // 
            this.batchLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.batchLabel.Location = new System.Drawing.Point(352, 90);
            this.batchLabel.Name = "batchLabel";
            this.batchLabel.Size = new System.Drawing.Size(122, 17);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label7.Location = new System.Drawing.Point(286, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 20);
            this.label7.Text = "批号：";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label8.Location = new System.Drawing.Point(6, 107);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 20);
            this.label8.Text = "序列号：";
            // 
            // serialNoLabel
            // 
            this.serialNoLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.serialNoLabel.Location = new System.Drawing.Point(87, 108);
            this.serialNoLabel.Name = "serialNoLabel";
            this.serialNoLabel.Size = new System.Drawing.Size(193, 17);
            // 
            // qtyLabel
            // 
            this.qtyLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.qtyLabel.Location = new System.Drawing.Point(352, 121);
            this.qtyLabel.Name = "qtyLabel";
            this.qtyLabel.Size = new System.Drawing.Size(122, 17);
            // 
            // siteLabel
            // 
            this.siteLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.siteLabel.Location = new System.Drawing.Point(152, 60);
            this.siteLabel.Name = "siteLabel";
            this.siteLabel.Size = new System.Drawing.Size(128, 17);
            // 
            // lbInv
            // 
            this.lbInv.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lbInv.Location = new System.Drawing.Point(352, 61);
            this.lbInv.Name = "lbInv";
            this.lbInv.Size = new System.Drawing.Size(122, 19);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(286, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 20);
            this.label2.Text = "库存：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // exceptButton
            // 
            this.exceptButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.exceptButton.Location = new System.Drawing.Point(131, 152);
            this.exceptButton.Name = "exceptButton";
            this.exceptButton.Size = new System.Drawing.Size(93, 40);
            this.exceptButton.TabIndex = 227;
            this.exceptButton.Text = "异常处理";
            this.exceptButton.Click += new System.EventHandler(this.exceptButton_Click);
            // 
            // QueryListView
            // 
            this.QueryListView.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.QueryListView.FullRowSelect = true;
            this.QueryListView.Location = new System.Drawing.Point(2, 194);
            this.QueryListView.Name = "QueryListView";
            this.QueryListView.Size = new System.Drawing.Size(474, 87);
            this.QueryListView.TabIndex = 241;
            this.QueryListView.View = System.Windows.Forms.View.Details;
            // 
            // TasksTitile
            // 
            this.TasksTitile.BackColor = System.Drawing.SystemColors.HotTrack;
            this.TasksTitile.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.TasksTitile.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new System.Drawing.Point(1, 0);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new System.Drawing.Size(479, 24);
            this.TasksTitile.TabIndex = 178;
            this.TasksTitile.Text = "上架采集";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(6, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 20);
            this.label1.Text = "料号(旧)：";
            // 
            // matinnercode
            // 
            this.matinnercode.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.matinnercode.Location = new System.Drawing.Point(108, 129);
            this.matinnercode.Name = "matinnercode";
            this.matinnercode.Size = new System.Drawing.Size(170, 17);
            // 
            // GoodsUpTaskItemFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(480, 600);
            this.ControlBox = false;
            this.Controls.Add(this.matinnercode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.QueryListView);
            this.Controls.Add(this.exceptButton);
            this.Controls.Add(this.lbInv);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.siteLabel);
            this.Controls.Add(this.qtyLabel);
            this.Controls.Add(this.serialNoLabel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.batchLabel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.commitButton);
            this.Controls.Add(this.matCodeLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TasksTitile);
            this.Controls.Add(this.tbxBarcode);
            this.Controls.Add(this.detailListView);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.collectItemButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GoodsUpTaskItemFrm";
            this.Text = "上架采集";
            this.Load += new System.EventHandler(this.UpTsakItemlFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Entity.HeadLabel TasksTitile;
        private System.Windows.Forms.TextBox tbxBarcode;
        private System.Windows.Forms.ListView detailListView;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button collectItemButton;
        private System.Windows.Forms.Label matCodeLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button commitButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label batchLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label serialNoLabel;
        private System.Windows.Forms.Label qtyLabel;
        private System.Windows.Forms.Label siteLabel;
        private System.Windows.Forms.Label lbInv;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button exceptButton;
        private System.Windows.Forms.ListView QueryListView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label matinnercode;
    }
}