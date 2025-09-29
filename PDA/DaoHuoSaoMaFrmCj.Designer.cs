namespace PDA
{
    partial class DaoHuoSaoMaFrmCj
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
            this.printButton = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.collectItemButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.TasksTitile = new Entity.HeadLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.CjTjBN = new System.Windows.Forms.Button();
            this.detailListView = new System.Windows.Forms.DataGrid();
            this.QueryListView = new System.Windows.Forms.DataGrid();
            this.TXT_matcodecontrol = new System.Windows.Forms.Label();
            this.rkbillno = new System.Windows.Forms.TextBox();
            this.seqnoLabel = new System.Windows.Forms.TextBox();
            this.batchnoLabel = new System.Windows.Forms.TextBox();
            this.matCodeLabel = new System.Windows.Forms.TextBox();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.pdateLabel = new System.Windows.Forms.TextBox();
            this.vdaysLabel = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbxBarcode
            // 
            this.tbxBarcode.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.tbxBarcode.Location = new System.Drawing.Point(114, 43);
            this.tbxBarcode.Name = "tbxBarcode";
            this.tbxBarcode.Size = new System.Drawing.Size(358, 26);
            this.tbxBarcode.TabIndex = 172;
            this.tbxBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxBarcode_KeyDown);
            // 
            // printButton
            // 
            this.printButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.printButton.Location = new System.Drawing.Point(272, 192);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(80, 37);
            this.printButton.TabIndex = 173;
            this.printButton.Text = "打印";
            this.printButton.Click += new System.EventHandler(this.printButton_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lblMsg.Location = new System.Drawing.Point(6, 46);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(102, 20);
            this.lblMsg.Text = "请扫描：";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // collectItemButton
            // 
            this.collectItemButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.collectItemButton.Location = new System.Drawing.Point(5, 192);
            this.collectItemButton.Name = "collectItemButton";
            this.collectItemButton.Size = new System.Drawing.Size(103, 37);
            this.collectItemButton.TabIndex = 174;
            this.collectItemButton.Text = "采集明细";
            this.collectItemButton.Click += new System.EventHandler(this.collectItemButton_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(233, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 20);
            this.label3.Text = "序列号：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(6, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 20);
            this.label1.Text = "物料编号：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(233, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 20);
            this.label4.Text = "有效期：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // closeButton
            // 
            this.closeButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.closeButton.Location = new System.Drawing.Point(410, 192);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(65, 37);
            this.closeButton.TabIndex = 202;
            this.closeButton.Text = "关闭";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(6, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 20);
            this.label5.Text = "批次号：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label7.Location = new System.Drawing.Point(6, 130);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 20);
            this.label7.Text = "生产日期：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label8.Location = new System.Drawing.Point(6, 161);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 20);
            this.label8.Text = "数量：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
            this.TasksTitile.Text = "到货采集";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(237, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 20);
            this.label2.Text = "入库号：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // CjTjBN
            // 
            this.CjTjBN.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.CjTjBN.Location = new System.Drawing.Point(146, 192);
            this.CjTjBN.Name = "CjTjBN";
            this.CjTjBN.Size = new System.Drawing.Size(80, 37);
            this.CjTjBN.TabIndex = 322;
            this.CjTjBN.Text = "提交";
            this.CjTjBN.Click += new System.EventHandler(this.CjTjBN_Click);
            // 
            // detailListView
            // 
            this.detailListView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.detailListView.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.detailListView.Location = new System.Drawing.Point(79, 284);
            this.detailListView.Name = "detailListView";
            this.detailListView.Size = new System.Drawing.Size(313, 151);
            this.detailListView.TabIndex = 353;
            this.detailListView.Visible = false;
            // 
            // QueryListView
            // 
            this.QueryListView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.QueryListView.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.QueryListView.Location = new System.Drawing.Point(4, 236);
            this.QueryListView.Name = "QueryListView";
            this.QueryListView.Size = new System.Drawing.Size(472, 360);
            this.QueryListView.TabIndex = 354;
            // 
            // TXT_matcodecontrol
            // 
            this.TXT_matcodecontrol.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.TXT_matcodecontrol.Location = new System.Drawing.Point(326, 46);
            this.TXT_matcodecontrol.Name = "TXT_matcodecontrol";
            this.TXT_matcodecontrol.Size = new System.Drawing.Size(51, 14);
            this.TXT_matcodecontrol.Visible = false;
            // 
            // rkbillno
            // 
            this.rkbillno.BackColor = System.Drawing.SystemColors.Info;
            this.rkbillno.Enabled = false;
            this.rkbillno.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.rkbillno.Location = new System.Drawing.Point(326, 74);
            this.rkbillno.Name = "rkbillno";
            this.rkbillno.Size = new System.Drawing.Size(146, 26);
            this.rkbillno.TabIndex = 369;
            // 
            // seqnoLabel
            // 
            this.seqnoLabel.BackColor = System.Drawing.SystemColors.Info;
            this.seqnoLabel.Enabled = false;
            this.seqnoLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.seqnoLabel.Location = new System.Drawing.Point(326, 104);
            this.seqnoLabel.Name = "seqnoLabel";
            this.seqnoLabel.Size = new System.Drawing.Size(146, 26);
            this.seqnoLabel.TabIndex = 370;
            // 
            // batchnoLabel
            // 
            this.batchnoLabel.BackColor = System.Drawing.SystemColors.Info;
            this.batchnoLabel.Enabled = false;
            this.batchnoLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.batchnoLabel.Location = new System.Drawing.Point(114, 102);
            this.batchnoLabel.Name = "batchnoLabel";
            this.batchnoLabel.Size = new System.Drawing.Size(136, 26);
            this.batchnoLabel.TabIndex = 372;
            // 
            // matCodeLabel
            // 
            this.matCodeLabel.BackColor = System.Drawing.SystemColors.Info;
            this.matCodeLabel.Enabled = false;
            this.matCodeLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.matCodeLabel.Location = new System.Drawing.Point(114, 74);
            this.matCodeLabel.Name = "matCodeLabel";
            this.matCodeLabel.Size = new System.Drawing.Size(136, 26);
            this.matCodeLabel.TabIndex = 371;
            // 
            // txtQty
            // 
            this.txtQty.BackColor = System.Drawing.SystemColors.Info;
            this.txtQty.Enabled = false;
            this.txtQty.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtQty.Location = new System.Drawing.Point(114, 160);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(136, 26);
            this.txtQty.TabIndex = 374;
            // 
            // pdateLabel
            // 
            this.pdateLabel.BackColor = System.Drawing.SystemColors.Info;
            this.pdateLabel.Enabled = false;
            this.pdateLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.pdateLabel.Location = new System.Drawing.Point(114, 131);
            this.pdateLabel.Name = "pdateLabel";
            this.pdateLabel.Size = new System.Drawing.Size(136, 26);
            this.pdateLabel.TabIndex = 373;
            // 
            // vdaysLabel
            // 
            this.vdaysLabel.BackColor = System.Drawing.SystemColors.Info;
            this.vdaysLabel.Enabled = false;
            this.vdaysLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.vdaysLabel.Location = new System.Drawing.Point(326, 136);
            this.vdaysLabel.Name = "vdaysLabel";
            this.vdaysLabel.Size = new System.Drawing.Size(146, 26);
            this.vdaysLabel.TabIndex = 375;
            // 
            // DaoHuoSaoMaFrmCj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(480, 600);
            this.ControlBox = false;
            this.Controls.Add(this.vdaysLabel);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.pdateLabel);
            this.Controls.Add(this.batchnoLabel);
            this.Controls.Add(this.matCodeLabel);
            this.Controls.Add(this.seqnoLabel);
            this.Controls.Add(this.rkbillno);
            this.Controls.Add(this.TXT_matcodecontrol);
            this.Controls.Add(this.QueryListView);
            this.Controls.Add(this.detailListView);
            this.Controls.Add(this.CjTjBN);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.printButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TasksTitile);
            this.Controls.Add(this.tbxBarcode);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.collectItemButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DaoHuoSaoMaFrmCj";
            this.Text = "到货采集";
            this.Load += new System.EventHandler(this.DaoHuoSaoMaFrmCj_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Entity.HeadLabel TasksTitile;
        private System.Windows.Forms.TextBox tbxBarcode;
        private System.Windows.Forms.Button printButton;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button collectItemButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button CjTjBN;
        private System.Windows.Forms.DataGrid detailListView;
        private System.Windows.Forms.DataGrid QueryListView;
        private System.Windows.Forms.Label TXT_matcodecontrol;
        private System.Windows.Forms.TextBox rkbillno;
        private System.Windows.Forms.TextBox seqnoLabel;
        private System.Windows.Forms.TextBox batchnoLabel;
        private System.Windows.Forms.TextBox matCodeLabel;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.TextBox pdateLabel;
        private System.Windows.Forms.TextBox vdaysLabel;
    }
}