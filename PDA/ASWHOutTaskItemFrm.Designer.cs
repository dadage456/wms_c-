namespace PDA
{
    partial class ASWHOutTaskItemFrm
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
            this.commitButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.siteLabel = new System.Windows.Forms.Label();
            this.TasksTitile = new Entity.HeadLabel();
            this.exceptButton = new System.Windows.Forms.Button();
            this.trayLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.WCSButton = new System.Windows.Forms.Button();
            this.INOUTComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.WmsToWcsBN = new System.Windows.Forms.Button();
            this.SingleButton = new System.Windows.Forms.Button();
            this.qtyLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.proofLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.PalletNum = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbxBarcode
            // 
            this.tbxBarcode.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.tbxBarcode.Location = new System.Drawing.Point(151, 33);
            this.tbxBarcode.Name = "tbxBarcode";
            this.tbxBarcode.Size = new System.Drawing.Size(324, 26);
            this.tbxBarcode.TabIndex = 172;
            this.tbxBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxBarcode_KeyDown);
            // 
            // detailListView
            // 
            this.detailListView.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.detailListView.FullRowSelect = true;
            this.detailListView.Location = new System.Drawing.Point(3, 192);
            this.detailListView.Name = "detailListView";
            this.detailListView.Size = new System.Drawing.Size(474, 405);
            this.detailListView.TabIndex = 175;
            this.detailListView.View = System.Windows.Forms.View.Details;
            // 
            // lblMsg
            // 
            this.lblMsg.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lblMsg.Location = new System.Drawing.Point(3, 35);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(144, 20);
            this.lblMsg.Text = "请扫描托盘号：";
            // 
            // collectItemButton
            // 
            this.collectItemButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.collectItemButton.Location = new System.Drawing.Point(4, 153);
            this.collectItemButton.Name = "collectItemButton";
            this.collectItemButton.Size = new System.Drawing.Size(65, 33);
            this.collectItemButton.TabIndex = 174;
            this.collectItemButton.Text = "查明细";
            this.collectItemButton.Click += new System.EventHandler(this.collectItemButton_Click);
            // 
            // commitButton
            // 
            this.commitButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.commitButton.Location = new System.Drawing.Point(336, 153);
            this.commitButton.Name = "commitButton";
            this.commitButton.Size = new System.Drawing.Size(65, 33);
            this.commitButton.TabIndex = 201;
            this.commitButton.Text = "提交";
            this.commitButton.Click += new System.EventHandler(this.commitButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.closeButton.Location = new System.Drawing.Point(404, 153);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(65, 33);
            this.closeButton.TabIndex = 202;
            this.closeButton.Text = "关闭";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(255, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 20);
            this.label5.Text = "库位：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // siteLabel
            // 
            this.siteLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.siteLabel.Location = new System.Drawing.Point(321, 69);
            this.siteLabel.Name = "siteLabel";
            this.siteLabel.Size = new System.Drawing.Size(149, 17);
            // 
            // TasksTitile
            // 
            this.TasksTitile.BackColor = System.Drawing.SystemColors.HotTrack;
            this.TasksTitile.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.TasksTitile.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new System.Drawing.Point(1, 0);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new System.Drawing.Size(476, 32);
            this.TasksTitile.TabIndex = 178;
            this.TasksTitile.Text = "整盘下架采集";
            // 
            // exceptButton
            // 
            this.exceptButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.exceptButton.Location = new System.Drawing.Point(140, 153);
            this.exceptButton.Name = "exceptButton";
            this.exceptButton.Size = new System.Drawing.Size(90, 33);
            this.exceptButton.TabIndex = 226;
            this.exceptButton.Text = "异常处理";
            this.exceptButton.Click += new System.EventHandler(this.exceptButton_Click);
            // 
            // trayLabel
            // 
            this.trayLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.trayLabel.Location = new System.Drawing.Point(137, 67);
            this.trayLabel.Name = "trayLabel";
            this.trayLabel.Size = new System.Drawing.Size(112, 20);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(44, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 20);
            this.label1.Text = "托盘号：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // WCSButton
            // 
            this.WCSButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.WCSButton.Location = new System.Drawing.Point(336, 117);
            this.WCSButton.Name = "WCSButton";
            this.WCSButton.Size = new System.Drawing.Size(100, 33);
            this.WCSButton.TabIndex = 244;
            this.WCSButton.Text = "全部来料盘";
            this.WCSButton.Click += new System.EventHandler(this.WCSButton_Click);
            // 
            // INOUTComboBox
            // 
            this.INOUTComboBox.DisplayMember = "NAME";
            this.INOUTComboBox.Location = new System.Drawing.Point(137, 122);
            this.INOUTComboBox.Name = "INOUTComboBox";
            this.INOUTComboBox.Size = new System.Drawing.Size(126, 23);
            this.INOUTComboBox.TabIndex = 248;
            this.INOUTComboBox.ValueMember = "CODE";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label6.Location = new System.Drawing.Point(36, 123);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 20);
            this.label6.Text = "拣选位置：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // WmsToWcsBN
            // 
            this.WmsToWcsBN.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.WmsToWcsBN.Location = new System.Drawing.Point(72, 153);
            this.WmsToWcsBN.Name = "WmsToWcsBN";
            this.WmsToWcsBN.Size = new System.Drawing.Size(65, 33);
            this.WmsToWcsBN.TabIndex = 302;
            this.WmsToWcsBN.Text = "查指令";
            this.WmsToWcsBN.Click += new System.EventHandler(this.WmsToWcsBN_Click);
            // 
            // SingleButton
            // 
            this.SingleButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.SingleButton.Location = new System.Drawing.Point(233, 153);
            this.SingleButton.Name = "SingleButton";
            this.SingleButton.Size = new System.Drawing.Size(100, 33);
            this.SingleButton.TabIndex = 319;
            this.SingleButton.Text = "单个来料盘";
            this.SingleButton.Click += new System.EventHandler(this.SingleButton_Click);
            // 
            // qtyLabel
            // 
            this.qtyLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.qtyLabel.Location = new System.Drawing.Point(137, 96);
            this.qtyLabel.Name = "qtyLabel";
            this.qtyLabel.Size = new System.Drawing.Size(112, 17);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(75, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 20);
            this.label2.Text = "数量：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // proofLabel
            // 
            this.proofLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.proofLabel.Location = new System.Drawing.Point(320, 96);
            this.proofLabel.Name = "proofLabel";
            this.proofLabel.Size = new System.Drawing.Size(150, 17);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(254, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 20);
            this.label4.Text = "单号：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // PalletNum
            // 
            this.PalletNum.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.PalletNum.Location = new System.Drawing.Point(442, 120);
            this.PalletNum.Name = "PalletNum";
            this.PalletNum.Size = new System.Drawing.Size(24, 26);
            this.PalletNum.TabIndex = 388;
            this.PalletNum.Text = "10";
            // 
            // ASWHOutTaskItemFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(480, 600);
            this.ControlBox = false;
            this.Controls.Add(this.PalletNum);
            this.Controls.Add(this.proofLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.WmsToWcsBN);
            this.Controls.Add(this.exceptButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.commitButton);
            this.Controls.Add(this.collectItemButton);
            this.Controls.Add(this.qtyLabel);
            this.Controls.Add(this.SingleButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.INOUTComboBox);
            this.Controls.Add(this.WCSButton);
            this.Controls.Add(this.trayLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.siteLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TasksTitile);
            this.Controls.Add(this.tbxBarcode);
            this.Controls.Add(this.detailListView);
            this.Controls.Add(this.lblMsg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ASWHOutTaskItemFrm";
            this.Text = "下架采集";
            this.Load += new System.EventHandler(this.DownTaskItemFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Entity.HeadLabel TasksTitile;
        private System.Windows.Forms.TextBox tbxBarcode;
        private System.Windows.Forms.ListView detailListView;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button collectItemButton;
        private System.Windows.Forms.Button commitButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label siteLabel;
        private System.Windows.Forms.Button exceptButton;
        private System.Windows.Forms.Label trayLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button WCSButton;
        private System.Windows.Forms.ComboBox INOUTComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button WmsToWcsBN;
        private System.Windows.Forms.Button SingleButton;
        private System.Windows.Forms.Label qtyLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label proofLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox PalletNum;
    }
}