namespace PDA
{
    partial class TrayUpTaskItemFrm
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
            this.submitUpButton = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.collectItemButton = new System.Windows.Forms.Button();
            this.trayLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.commitButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.siteLabel = new System.Windows.Forms.Label();
            this.qtyLabel = new System.Windows.Forms.Label();
            this.TasksTitile = new Entity.HeadLabel();
            this.exceptButton = new System.Windows.Forms.Button();
            this.WCSButton = new System.Windows.Forms.Button();
            this.WmsToWcsBN = new System.Windows.Forms.Button();
            this.INOUTComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.QueryPalletNoItemBN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbxBarcode
            // 
            this.tbxBarcode.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.tbxBarcode.Location = new System.Drawing.Point(114, 43);
            this.tbxBarcode.Name = "tbxBarcode";
            this.tbxBarcode.Size = new System.Drawing.Size(347, 26);
            this.tbxBarcode.TabIndex = 172;
            this.tbxBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxBarcode_KeyDown);
            // 
            // detailListView
            // 
            this.detailListView.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.detailListView.FullRowSelect = true;
            this.detailListView.Location = new System.Drawing.Point(3, 176);
            this.detailListView.Name = "detailListView";
            this.detailListView.Size = new System.Drawing.Size(474, 421);
            this.detailListView.TabIndex = 175;
            this.detailListView.View = System.Windows.Forms.View.Details;
            // 
            // submitUpButton
            // 
            this.submitUpButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.submitUpButton.Location = new System.Drawing.Point(369, 133);
            this.submitUpButton.Name = "submitUpButton";
            this.submitUpButton.Size = new System.Drawing.Size(54, 37);
            this.submitUpButton.TabIndex = 173;
            this.submitUpButton.Text = "提交";
            this.submitUpButton.Click += new System.EventHandler(this.submitUpButton_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lblMsg.Location = new System.Drawing.Point(3, 46);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(105, 20);
            this.lblMsg.Text = "请扫描：";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // collectItemButton
            // 
            this.collectItemButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.collectItemButton.Location = new System.Drawing.Point(5, 133);
            this.collectItemButton.Name = "collectItemButton";
            this.collectItemButton.Size = new System.Drawing.Size(80, 37);
            this.collectItemButton.TabIndex = 174;
            this.collectItemButton.Text = "采集明细";
            this.collectItemButton.Click += new System.EventHandler(this.collectItemButton_Click);
            // 
            // trayLabel
            // 
            this.trayLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.trayLabel.Location = new System.Drawing.Point(114, 75);
            this.trayLabel.Name = "trayLabel";
            this.trayLabel.Size = new System.Drawing.Size(112, 20);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(3, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 20);
            this.label1.Text = "托盘号：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(263, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 20);
            this.label4.Text = "数量：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // commitButton
            // 
            this.commitButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.commitButton.Location = new System.Drawing.Point(355, 475);
            this.commitButton.Name = "commitButton";
            this.commitButton.Size = new System.Drawing.Size(111, 39);
            this.commitButton.TabIndex = 201;
            this.commitButton.Text = "获取货位";
            this.commitButton.Visible = false;
            this.commitButton.Click += new System.EventHandler(this.commitButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.closeButton.Location = new System.Drawing.Point(424, 133);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(50, 37);
            this.closeButton.TabIndex = 202;
            this.closeButton.Text = "关闭";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(3, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 20);
            this.label5.Text = "库位：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // siteLabel
            // 
            this.siteLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.siteLabel.Location = new System.Drawing.Point(114, 105);
            this.siteLabel.Name = "siteLabel";
            this.siteLabel.Size = new System.Drawing.Size(153, 17);
            // 
            // qtyLabel
            // 
            this.qtyLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.qtyLabel.Location = new System.Drawing.Point(337, 78);
            this.qtyLabel.Name = "qtyLabel";
            this.qtyLabel.Size = new System.Drawing.Size(138, 17);
            // 
            // TasksTitile
            // 
            this.TasksTitile.BackColor = System.Drawing.SystemColors.HotTrack;
            this.TasksTitile.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.TasksTitile.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new System.Drawing.Point(1, 0);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new System.Drawing.Size(476, 34);
            this.TasksTitile.TabIndex = 178;
            this.TasksTitile.Text = "托盘上架采集";
            // 
            // exceptButton
            // 
            this.exceptButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.exceptButton.Location = new System.Drawing.Point(86, 133);
            this.exceptButton.Name = "exceptButton";
            this.exceptButton.Size = new System.Drawing.Size(80, 37);
            this.exceptButton.TabIndex = 227;
            this.exceptButton.Text = "异常处理";
            this.exceptButton.Click += new System.EventHandler(this.exceptButton_Click);
            // 
            // WCSButton
            // 
            this.WCSButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.WCSButton.Location = new System.Drawing.Point(395, 93);
            this.WCSButton.Name = "WCSButton";
            this.WCSButton.Size = new System.Drawing.Size(80, 37);
            this.WCSButton.TabIndex = 244;
            this.WCSButton.Text = "托盘上架";
            this.WCSButton.Click += new System.EventHandler(this.WCSButton_Click);
            // 
            // WmsToWcsBN
            // 
            this.WmsToWcsBN.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.WmsToWcsBN.Location = new System.Drawing.Point(167, 133);
            this.WmsToWcsBN.Name = "WmsToWcsBN";
            this.WmsToWcsBN.Size = new System.Drawing.Size(80, 37);
            this.WmsToWcsBN.TabIndex = 303;
            this.WmsToWcsBN.Text = "查询指令";
            this.WmsToWcsBN.Click += new System.EventHandler(this.WmsToWcsBN_Click);
            // 
            // INOUTComboBox
            // 
            this.INOUTComboBox.DisplayMember = "NAME";
            this.INOUTComboBox.Location = new System.Drawing.Point(238, 498);
            this.INOUTComboBox.Name = "INOUTComboBox";
            this.INOUTComboBox.Size = new System.Drawing.Size(111, 23);
            this.INOUTComboBox.TabIndex = 246;
            this.INOUTComboBox.ValueMember = "CODE";
            this.INOUTComboBox.Visible = false;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label6.Location = new System.Drawing.Point(256, 475);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 20);
            this.label6.Text = "入口位置:";
            this.label6.Visible = false;
            // 
            // QueryPalletNoItemBN
            // 
            this.QueryPalletNoItemBN.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.QueryPalletNoItemBN.Location = new System.Drawing.Point(248, 133);
            this.QueryPalletNoItemBN.Name = "QueryPalletNoItemBN";
            this.QueryPalletNoItemBN.Size = new System.Drawing.Size(120, 37);
            this.QueryPalletNoItemBN.TabIndex = 322;
            this.QueryPalletNoItemBN.Text = "查询托盘物料";
            this.QueryPalletNoItemBN.Click += new System.EventHandler(this.QueryPalletNoItemBN_Click);
            // 
            // TrayUpTaskItemFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(480, 600);
            this.ControlBox = false;
            this.Controls.Add(this.QueryPalletNoItemBN);
            this.Controls.Add(this.WmsToWcsBN);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.INOUTComboBox);
            this.Controls.Add(this.WCSButton);
            this.Controls.Add(this.exceptButton);
            this.Controls.Add(this.qtyLabel);
            this.Controls.Add(this.siteLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.commitButton);
            this.Controls.Add(this.submitUpButton);
            this.Controls.Add(this.trayLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TasksTitile);
            this.Controls.Add(this.tbxBarcode);
            this.Controls.Add(this.detailListView);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.collectItemButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TrayUpTaskItemFrm";
            this.Text = "组盘采集";
            this.Load += new System.EventHandler(this.BindingDetailFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Entity.HeadLabel TasksTitile;
        private System.Windows.Forms.TextBox tbxBarcode;
        private System.Windows.Forms.ListView detailListView;
        private System.Windows.Forms.Button submitUpButton;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button collectItemButton;
        private System.Windows.Forms.Label trayLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button commitButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label siteLabel;
        private System.Windows.Forms.Label qtyLabel;
        private System.Windows.Forms.Button exceptButton;
        private System.Windows.Forms.Button WCSButton;
        private System.Windows.Forms.Button WmsToWcsBN;
        private System.Windows.Forms.ComboBox INOUTComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button QueryPalletNoItemBN;
    }
}