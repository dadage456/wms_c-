namespace PDA
{
    partial class ASWHAllocateTaskItemFrm
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
            this.siteCheckBox = new System.Windows.Forms.CheckBox();
            this.lotNoCheckBox = new System.Windows.Forms.CheckBox();
            this.siteLabel = new System.Windows.Forms.Label();
            this.qtyLabel = new System.Windows.Forms.Label();
            this.TasksTitile = new Entity.HeadLabel();
            this.lbInv = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.exceptButton = new System.Windows.Forms.Button();
            this.trayLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SingleButton = new System.Windows.Forms.Button();
            this.INOUTComboBox = new System.Windows.Forms.ComboBox();
            this.INButton = new System.Windows.Forms.Button();
            this.EMPTYINButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.ASWHBPButton = new System.Windows.Forms.Button();
            this.WmsToWcsBN = new System.Windows.Forms.Button();
            this.detailListView = new System.Windows.Forms.ListView();
            this.WCSbutton = new System.Windows.Forms.Button();
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
            // lblMsg
            // 
            this.lblMsg.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lblMsg.Location = new System.Drawing.Point(3, 35);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(144, 20);
            this.lblMsg.Text = "请扫描";
            // 
            // collectItemButton
            // 
            this.collectItemButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.collectItemButton.Location = new System.Drawing.Point(5, 239);
            this.collectItemButton.Name = "collectItemButton";
            this.collectItemButton.Size = new System.Drawing.Size(80, 33);
            this.collectItemButton.TabIndex = 174;
            this.collectItemButton.Text = "查看明细";
            this.collectItemButton.Click += new System.EventHandler(this.collectItemButton_Click);
            // 
            // matCodeLabel
            // 
            this.matCodeLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.matCodeLabel.Location = new System.Drawing.Point(153, 96);
            this.matCodeLabel.Name = "matCodeLabel";
            this.matCodeLabel.Size = new System.Drawing.Size(124, 19);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(80, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 20);
            this.label3.Text = "料号：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(80, 190);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 20);
            this.label4.Text = "数量：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // commitButton
            // 
            this.commitButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.commitButton.Location = new System.Drawing.Point(339, 239);
            this.commitButton.Name = "commitButton";
            this.commitButton.Size = new System.Drawing.Size(65, 33);
            this.commitButton.TabIndex = 201;
            this.commitButton.Text = "提交";
            this.commitButton.Click += new System.EventHandler(this.commitButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.closeButton.Location = new System.Drawing.Point(405, 239);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(65, 33);
            this.closeButton.TabIndex = 202;
            this.closeButton.Text = "关闭";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(283, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 20);
            this.label5.Text = "库位：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // batchLabel
            // 
            this.batchLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.batchLabel.Location = new System.Drawing.Point(153, 129);
            this.batchLabel.Name = "batchLabel";
            this.batchLabel.Size = new System.Drawing.Size(102, 19);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label7.Location = new System.Drawing.Point(80, 128);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 20);
            this.label7.Text = "批号：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label8.Location = new System.Drawing.Point(66, 160);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 20);
            this.label8.Text = "序列号：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // serialNoLabel
            // 
            this.serialNoLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.serialNoLabel.Location = new System.Drawing.Point(153, 160);
            this.serialNoLabel.Name = "serialNoLabel";
            this.serialNoLabel.Size = new System.Drawing.Size(229, 17);
            // 
            // siteCheckBox
            // 
            this.siteCheckBox.AutoCheck = false;
            this.siteCheckBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.siteCheckBox.Location = new System.Drawing.Point(7, 215);
            this.siteCheckBox.Name = "siteCheckBox";
            this.siteCheckBox.Size = new System.Drawing.Size(67, 20);
            this.siteCheckBox.TabIndex = 215;
            this.siteCheckBox.Text = "强制库位";
            this.siteCheckBox.Visible = false;
            // 
            // lotNoCheckBox
            // 
            this.lotNoCheckBox.AutoCheck = false;
            this.lotNoCheckBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lotNoCheckBox.Location = new System.Drawing.Point(80, 215);
            this.lotNoCheckBox.Name = "lotNoCheckBox";
            this.lotNoCheckBox.Size = new System.Drawing.Size(49, 20);
            this.lotNoCheckBox.TabIndex = 216;
            this.lotNoCheckBox.Text = "强制批号";
            this.lotNoCheckBox.Visible = false;
            // 
            // siteLabel
            // 
            this.siteLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.siteLabel.Location = new System.Drawing.Point(349, 64);
            this.siteLabel.Name = "siteLabel";
            this.siteLabel.Size = new System.Drawing.Size(126, 17);
            // 
            // qtyLabel
            // 
            this.qtyLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.qtyLabel.Location = new System.Drawing.Point(153, 192);
            this.qtyLabel.Name = "qtyLabel";
            this.qtyLabel.Size = new System.Drawing.Size(102, 17);
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
            this.TasksTitile.Text = "线下配盘数据采集";
            // 
            // lbInv
            // 
            this.lbInv.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lbInv.Location = new System.Drawing.Point(349, 96);
            this.lbInv.Name = "lbInv";
            this.lbInv.Size = new System.Drawing.Size(122, 19);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(283, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 20);
            this.label2.Text = "库存：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // exceptButton
            // 
            this.exceptButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.exceptButton.Location = new System.Drawing.Point(86, 239);
            this.exceptButton.Name = "exceptButton";
            this.exceptButton.Size = new System.Drawing.Size(80, 33);
            this.exceptButton.TabIndex = 226;
            this.exceptButton.Text = "异常处理";
            this.exceptButton.Click += new System.EventHandler(this.exceptButton_Click);
            // 
            // trayLabel
            // 
            this.trayLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.trayLabel.Location = new System.Drawing.Point(153, 62);
            this.trayLabel.Name = "trayLabel";
            this.trayLabel.Size = new System.Drawing.Size(123, 20);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(7, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 20);
            this.label1.Text = "原料盘号：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // SingleButton
            // 
            this.SingleButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.SingleButton.Location = new System.Drawing.Point(233, 239);
            this.SingleButton.Name = "SingleButton";
            this.SingleButton.Size = new System.Drawing.Size(105, 33);
            this.SingleButton.TabIndex = 244;
            this.SingleButton.Text = "单个来料盘";
            this.SingleButton.Click += new System.EventHandler(this.SingleButton_Click);
            // 
            // INOUTComboBox
            // 
            this.INOUTComboBox.DisplayMember = "NAME";
            this.INOUTComboBox.Location = new System.Drawing.Point(349, 127);
            this.INOUTComboBox.Name = "INOUTComboBox";
            this.INOUTComboBox.Size = new System.Drawing.Size(126, 23);
            this.INOUTComboBox.TabIndex = 248;
            this.INOUTComboBox.ValueMember = "CODE";
            // 
            // INButton
            // 
            this.INButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.INButton.Location = new System.Drawing.Point(167, 239);
            this.INButton.Name = "INButton";
            this.INButton.Size = new System.Drawing.Size(65, 33);
            this.INButton.TabIndex = 264;
            this.INButton.Text = "回库";
            this.INButton.Click += new System.EventHandler(this.INButton_Click);
            // 
            // EMPTYINButton
            // 
            this.EMPTYINButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.EMPTYINButton.Location = new System.Drawing.Point(388, 199);
            this.EMPTYINButton.Name = "EMPTYINButton";
            this.EMPTYINButton.Size = new System.Drawing.Size(82, 33);
            this.EMPTYINButton.TabIndex = 281;
            this.EMPTYINButton.Text = "空盘入库";
            this.EMPTYINButton.Click += new System.EventHandler(this.EMPTYINButton_Click);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label6.Location = new System.Drawing.Point(244, 128);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 20);
            this.label6.Text = "拣选位置：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ASWHBPButton
            // 
            this.ASWHBPButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.ASWHBPButton.Location = new System.Drawing.Point(388, 163);
            this.ASWHBPButton.Name = "ASWHBPButton";
            this.ASWHBPButton.Size = new System.Drawing.Size(82, 33);
            this.ASWHBPButton.TabIndex = 283;
            this.ASWHBPButton.Text = "配盘入库";
            this.ASWHBPButton.Visible = false;
            this.ASWHBPButton.Click += new System.EventHandler(this.ASWHBPButton_Click);
            // 
            // WmsToWcsBN
            // 
            this.WmsToWcsBN.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.WmsToWcsBN.Location = new System.Drawing.Point(300, 199);
            this.WmsToWcsBN.Name = "WmsToWcsBN";
            this.WmsToWcsBN.Size = new System.Drawing.Size(82, 33);
            this.WmsToWcsBN.TabIndex = 300;
            this.WmsToWcsBN.Text = "查看指令";
            this.WmsToWcsBN.Click += new System.EventHandler(this.WmsToWcsBN_Click);
            // 
            // detailListView
            // 
            this.detailListView.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.detailListView.FullRowSelect = true;
            this.detailListView.Location = new System.Drawing.Point(3, 275);
            this.detailListView.Name = "detailListView";
            this.detailListView.Size = new System.Drawing.Size(474, 322);
            this.detailListView.TabIndex = 317;
            this.detailListView.View = System.Windows.Forms.View.Details;
            // 
            // WCSbutton
            // 
            this.WCSbutton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.WCSbutton.Location = new System.Drawing.Point(189, 200);
            this.WCSbutton.Name = "WCSbutton";
            this.WCSbutton.Size = new System.Drawing.Size(105, 33);
            this.WCSbutton.TabIndex = 334;
            this.WCSbutton.Text = "全部来料盘";
            this.WCSbutton.Click += new System.EventHandler(this.WCSbutton_Click);
            // 
            // ASWHAllocateTaskItemFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(480, 600);
            this.ControlBox = false;
            this.Controls.Add(this.WCSbutton);
            this.Controls.Add(this.detailListView);
            this.Controls.Add(this.WmsToWcsBN);
            this.Controls.Add(this.ASWHBPButton);
            this.Controls.Add(this.EMPTYINButton);
            this.Controls.Add(this.INButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.INOUTComboBox);
            this.Controls.Add(this.SingleButton);
            this.Controls.Add(this.trayLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.exceptButton);
            this.Controls.Add(this.lbInv);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.qtyLabel);
            this.Controls.Add(this.siteLabel);
            this.Controls.Add(this.lotNoCheckBox);
            this.Controls.Add(this.siteCheckBox);
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
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.collectItemButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ASWHAllocateTaskItemFrm";
            this.Text = "配盘任务";
            this.Load += new System.EventHandler(this.ASWHAllocateTaskItemFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Entity.HeadLabel TasksTitile;
        private System.Windows.Forms.TextBox tbxBarcode;
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
        private System.Windows.Forms.CheckBox siteCheckBox;
        private System.Windows.Forms.CheckBox lotNoCheckBox;
        private System.Windows.Forms.Label siteLabel;
        private System.Windows.Forms.Label qtyLabel;
        private System.Windows.Forms.Label lbInv;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button exceptButton;
        private System.Windows.Forms.Label trayLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SingleButton;
        private System.Windows.Forms.ComboBox INOUTComboBox;
        private System.Windows.Forms.Button INButton;
        private System.Windows.Forms.Button EMPTYINButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button ASWHBPButton;
        private System.Windows.Forms.Button WmsToWcsBN;
        private System.Windows.Forms.ListView detailListView;
        private System.Windows.Forms.Button WCSbutton;
    }
}