namespace PDA
{
    partial class TrayDownTaskItemFrm
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
            this.finishButton = new System.Windows.Forms.Button();
            this.supplierButton = new System.Windows.Forms.Button();
            this.trayLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lbTrayCapacity = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.workStationlabel = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.QueryListView = new System.Windows.Forms.ListView();
            this.detailListView = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // tbxBarcode
            // 
            this.tbxBarcode.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.tbxBarcode.Location = new System.Drawing.Point(151, 33);
            this.tbxBarcode.Name = "tbxBarcode";
            this.tbxBarcode.Size = new System.Drawing.Size(324, 35);
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
            this.collectItemButton.Location = new System.Drawing.Point(4, 239);
            this.collectItemButton.Name = "collectItemButton";
            this.collectItemButton.Size = new System.Drawing.Size(90, 33);
            this.collectItemButton.TabIndex = 174;
            this.collectItemButton.Text = "查看明细";
            this.collectItemButton.Click += new System.EventHandler(this.collectItemButton_Click);
            // 
            // matCodeLabel
            // 
            this.matCodeLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.matCodeLabel.Location = new System.Drawing.Point(133, 97);
            this.matCodeLabel.Name = "matCodeLabel";
            this.matCodeLabel.Size = new System.Drawing.Size(143, 19);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(24, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 20);
            this.label3.Text = "料号：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(24, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 20);
            this.label4.Text = "数量：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // commitButton
            // 
            this.commitButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.commitButton.Location = new System.Drawing.Point(351, 239);
            this.commitButton.Name = "commitButton";
            this.commitButton.Size = new System.Drawing.Size(60, 33);
            this.commitButton.TabIndex = 201;
            this.commitButton.Text = "提交";
            this.commitButton.Click += new System.EventHandler(this.commitButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.closeButton.Location = new System.Drawing.Point(413, 239);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(60, 33);
            this.closeButton.TabIndex = 202;
            this.closeButton.Text = "关闭";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(24, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 20);
            this.label5.Text = "库位：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // batchLabel
            // 
            this.batchLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.batchLabel.Location = new System.Drawing.Point(133, 129);
            this.batchLabel.Name = "batchLabel";
            this.batchLabel.Size = new System.Drawing.Size(143, 19);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label7.Location = new System.Drawing.Point(24, 128);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 20);
            this.label7.Text = "批号：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label8.Location = new System.Drawing.Point(24, 160);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 20);
            this.label8.Text = "序列号：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // serialNoLabel
            // 
            this.serialNoLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.serialNoLabel.Location = new System.Drawing.Point(133, 162);
            this.serialNoLabel.Name = "serialNoLabel";
            this.serialNoLabel.Size = new System.Drawing.Size(338, 17);
            // 
            // siteCheckBox
            // 
            this.siteCheckBox.AutoCheck = false;
            this.siteCheckBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.siteCheckBox.Location = new System.Drawing.Point(7, 215);
            this.siteCheckBox.Name = "siteCheckBox";
            this.siteCheckBox.Size = new System.Drawing.Size(118, 20);
            this.siteCheckBox.TabIndex = 215;
            this.siteCheckBox.Text = "强制库位";
            this.siteCheckBox.Visible = false;
            // 
            // lotNoCheckBox
            // 
            this.lotNoCheckBox.AutoCheck = false;
            this.lotNoCheckBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lotNoCheckBox.Location = new System.Drawing.Point(153, 215);
            this.lotNoCheckBox.Name = "lotNoCheckBox";
            this.lotNoCheckBox.Size = new System.Drawing.Size(123, 20);
            this.lotNoCheckBox.TabIndex = 216;
            this.lotNoCheckBox.Text = "强制批号";
            this.lotNoCheckBox.Visible = false;
            // 
            // siteLabel
            // 
            this.siteLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.siteLabel.Location = new System.Drawing.Point(133, 66);
            this.siteLabel.Name = "siteLabel";
            this.siteLabel.Size = new System.Drawing.Size(123, 17);
            // 
            // qtyLabel
            // 
            this.qtyLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.qtyLabel.Location = new System.Drawing.Point(133, 193);
            this.qtyLabel.Name = "qtyLabel";
            this.qtyLabel.Size = new System.Drawing.Size(107, 17);
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
            this.TasksTitile.Text = "下架组盘采集";
            // 
            // lbInv
            // 
            this.lbInv.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lbInv.Location = new System.Drawing.Point(349, 97);
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
            this.exceptButton.Location = new System.Drawing.Point(96, 239);
            this.exceptButton.Name = "exceptButton";
            this.exceptButton.Size = new System.Drawing.Size(90, 33);
            this.exceptButton.TabIndex = 226;
            this.exceptButton.Text = "异常处理";
            this.exceptButton.Click += new System.EventHandler(this.exceptButton_Click);
            // 
            // finishButton
            // 
            this.finishButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.finishButton.Location = new System.Drawing.Point(289, 239);
            this.finishButton.Name = "finishButton";
            this.finishButton.Size = new System.Drawing.Size(60, 33);
            this.finishButton.TabIndex = 240;
            this.finishButton.Text = "报缺";
            this.finishButton.Click += new System.EventHandler(this.finishButton_Click);
            // 
            // supplierButton
            // 
            this.supplierButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.supplierButton.Location = new System.Drawing.Point(188, 239);
            this.supplierButton.Name = "supplierButton";
            this.supplierButton.Size = new System.Drawing.Size(99, 33);
            this.supplierButton.TabIndex = 254;
            this.supplierButton.Text = "采集供应商";
            this.supplierButton.Click += new System.EventHandler(this.supplierButton_Click);
            // 
            // trayLabel
            // 
            this.trayLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.trayLabel.Location = new System.Drawing.Point(351, 64);
            this.trayLabel.Name = "trayLabel";
            this.trayLabel.Size = new System.Drawing.Size(124, 20);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(262, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 20);
            this.label1.Text = "托盘号：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label9.Location = new System.Drawing.Point(436, 191);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 20);
            this.label9.Text = "cm³";
            // 
            // lbTrayCapacity
            // 
            this.lbTrayCapacity.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lbTrayCapacity.Location = new System.Drawing.Point(349, 191);
            this.lbTrayCapacity.Name = "lbTrayCapacity";
            this.lbTrayCapacity.Size = new System.Drawing.Size(86, 20);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label6.Location = new System.Drawing.Point(245, 191);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 20);
            this.label6.Text = "剩余容量：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // workStationlabel
            // 
            this.workStationlabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.workStationlabel.Location = new System.Drawing.Point(349, 129);
            this.workStationlabel.Name = "workStationlabel";
            this.workStationlabel.Size = new System.Drawing.Size(122, 19);
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label11.Location = new System.Drawing.Point(283, 128);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 20);
            this.label11.Text = "工位：";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // QueryListView
            // 
            this.QueryListView.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.QueryListView.FullRowSelect = true;
            this.QueryListView.Location = new System.Drawing.Point(3, 276);
            this.QueryListView.Name = "QueryListView";
            this.QueryListView.Size = new System.Drawing.Size(474, 87);
            this.QueryListView.TabIndex = 268;
            this.QueryListView.View = System.Windows.Forms.View.Details;
            // 
            // detailListView
            // 
            this.detailListView.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.detailListView.FullRowSelect = true;
            this.detailListView.Location = new System.Drawing.Point(3, 363);
            this.detailListView.Name = "detailListView";
            this.detailListView.Size = new System.Drawing.Size(474, 235);
            this.detailListView.TabIndex = 269;
            this.detailListView.View = System.Windows.Forms.View.Details;
            // 
            // TrayDownTaskItemFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(480, 600);
            this.ControlBox = false;
            this.Controls.Add(this.detailListView);
            this.Controls.Add(this.QueryListView);
            this.Controls.Add(this.workStationlabel);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lbTrayCapacity);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.trayLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.supplierButton);
            this.Controls.Add(this.finishButton);
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
            this.Name = "TrayDownTaskItemFrm";
            this.Text = "下架采集";
            this.Load += new System.EventHandler(this.DownTaskItemFrm_Load);
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
        private System.Windows.Forms.Button finishButton;
        private System.Windows.Forms.Button supplierButton;
        private System.Windows.Forms.Label trayLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbTrayCapacity;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label workStationlabel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ListView QueryListView;
        private System.Windows.Forms.ListView detailListView;
    }
}