namespace PDA
{
    partial class DaoHuoSaoMa
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
            this.taskButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.ds_bill = new System.Windows.Forms.DataGrid();
            this.labMat = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.btnDetailQr = new System.Windows.Forms.Button();
            this.btn_shuaxin = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGoodQty = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBILLID = new System.Windows.Forms.TextBox();
            this.lab_taskinfo = new System.Windows.Forms.Label();
            this.txtExceptQty = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ds_detail = new System.Windows.Forms.DataGrid();
            this.btn_rescan = new System.Windows.Forms.Button();
            this.btn_billok = new System.Windows.Forms.Button();
            this.txt_scantot = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // taskButton
            // 
            this.taskButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.taskButton.Location = new System.Drawing.Point(334, 4);
            this.taskButton.Name = "taskButton";
            this.taskButton.Size = new System.Drawing.Size(62, 31);
            this.taskButton.TabIndex = 192;
            this.taskButton.Text = "...";
            this.taskButton.Click += new System.EventHandler(this.taskButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.closeButton.Location = new System.Drawing.Point(412, 4);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(63, 31);
            this.closeButton.TabIndex = 201;
            this.closeButton.Text = "关闭";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // ds_bill
            // 
            this.ds_bill.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ds_bill.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.ds_bill.Location = new System.Drawing.Point(3, 41);
            this.ds_bill.Name = "ds_bill";
            this.ds_bill.Size = new System.Drawing.Size(474, 153);
            this.ds_bill.TabIndex = 203;
            this.ds_bill.MouseUp += new System.Windows.Forms.MouseEventHandler(this.taskDataGrid_MouseUp);
            this.ds_bill.CurrentCellChanged += new System.EventHandler(this.ds_bill_CurrentCellChanged);
            // 
            // labMat
            // 
            this.labMat.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.labMat.Location = new System.Drawing.Point(8, 544);
            this.labMat.Name = "labMat";
            this.labMat.Size = new System.Drawing.Size(465, 50);
            this.labMat.Text = "物料名称批次信息";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(9, 482);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 20);
            this.label3.Text = "条码:";
            // 
            // txtBarcode
            // 
            this.txtBarcode.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtBarcode.Location = new System.Drawing.Point(60, 480);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(223, 26);
            this.txtBarcode.TabIndex = 243;
            this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarcode_KeyDown);
            // 
            // btnDetailQr
            // 
            this.btnDetailQr.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.btnDetailQr.Location = new System.Drawing.Point(288, 519);
            this.btnDetailQr.Name = "btnDetailQr";
            this.btnDetailQr.Size = new System.Drawing.Size(98, 21);
            this.btnDetailQr.TabIndex = 248;
            this.btnDetailQr.Text = "数量确认";
            this.btnDetailQr.Click += new System.EventHandler(this.btnDetailQr_Click);
            // 
            // btn_shuaxin
            // 
            this.btn_shuaxin.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.btn_shuaxin.Location = new System.Drawing.Point(287, 480);
            this.btn_shuaxin.Name = "btn_shuaxin";
            this.btn_shuaxin.Size = new System.Drawing.Size(98, 21);
            this.btn_shuaxin.TabIndex = 264;
            this.btn_shuaxin.Text = "刷新单据";
            this.btn_shuaxin.Click += new System.EventHandler(this.btn_shuaxin_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(9, 517);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 20);
            this.label2.Text = "正品数:";
            // 
            // txtGoodQty
            // 
            this.txtGoodQty.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtGoodQty.Location = new System.Drawing.Point(79, 513);
            this.txtGoodQty.Name = "txtGoodQty";
            this.txtGoodQty.Size = new System.Drawing.Size(64, 26);
            this.txtGoodQty.TabIndex = 284;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(12, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 20);
            this.label4.Text = "采购/装箱单:";
            // 
            // txtBILLID
            // 
            this.txtBILLID.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtBILLID.Location = new System.Drawing.Point(130, 9);
            this.txtBILLID.Name = "txtBILLID";
            this.txtBILLID.Size = new System.Drawing.Size(198, 26);
            this.txtBILLID.TabIndex = 292;
            // 
            // lab_taskinfo
            // 
            this.lab_taskinfo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lab_taskinfo.Location = new System.Drawing.Point(7, 200);
            this.lab_taskinfo.Name = "lab_taskinfo";
            this.lab_taskinfo.Size = new System.Drawing.Size(466, 20);
            this.lab_taskinfo.Text = "单号及供应商信息";
            // 
            // txtExceptQty
            // 
            this.txtExceptQty.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtExceptQty.Location = new System.Drawing.Point(214, 513);
            this.txtExceptQty.Name = "txtExceptQty";
            this.txtExceptQty.Size = new System.Drawing.Size(69, 26);
            this.txtExceptQty.TabIndex = 296;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label7.Location = new System.Drawing.Point(147, 517);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 20);
            this.label7.Text = "异常数:";
            // 
            // ds_detail
            // 
            this.ds_detail.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ds_detail.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.ds_detail.Location = new System.Drawing.Point(3, 252);
            this.ds_detail.Name = "ds_detail";
            this.ds_detail.Size = new System.Drawing.Size(474, 222);
            this.ds_detail.TabIndex = 298;
            // 
            // btn_rescan
            // 
            this.btn_rescan.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.btn_rescan.Location = new System.Drawing.Point(391, 479);
            this.btn_rescan.Name = "btn_rescan";
            this.btn_rescan.Size = new System.Drawing.Size(86, 21);
            this.btn_rescan.TabIndex = 299;
            this.btn_rescan.Text = "整单重扫";
            this.btn_rescan.Click += new System.EventHandler(this.btnrescan_Click);
            // 
            // btn_billok
            // 
            this.btn_billok.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.btn_billok.Location = new System.Drawing.Point(391, 519);
            this.btn_billok.Name = "btn_billok";
            this.btn_billok.Size = new System.Drawing.Size(86, 21);
            this.btn_billok.TabIndex = 300;
            this.btn_billok.Text = "整单完成";
            this.btn_billok.Click += new System.EventHandler(this.btn_billok_Click);
            // 
            // txt_scantot
            // 
            this.txt_scantot.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txt_scantot.Location = new System.Drawing.Point(8, 226);
            this.txt_scantot.Name = "txt_scantot";
            this.txt_scantot.Size = new System.Drawing.Size(466, 20);
            this.txt_scantot.Text = "本单的完成信息汇总";
            // 
            // DaoHuoSaoMaFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(480, 600);
            this.ControlBox = false;
            this.Controls.Add(this.txt_scantot);
            this.Controls.Add(this.btn_billok);
            this.Controls.Add(this.btn_rescan);
            this.Controls.Add(this.ds_detail);
            this.Controls.Add(this.txtExceptQty);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lab_taskinfo);
            this.Controls.Add(this.txtBILLID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtGoodQty);
            this.Controls.Add(this.btn_shuaxin);
            this.Controls.Add(this.btnDetailQr);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.labMat);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ds_bill);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.taskButton);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DaoHuoSaoMaFrm";
            this.Text = "到货扫描任务";
            this.Load += new System.EventHandler(this.DaoHuoSaoMaFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button taskButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.DataGrid ds_bill;
        private System.Windows.Forms.Label labMat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Button btnDetailQr;
        private System.Windows.Forms.Button btn_shuaxin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGoodQty;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBILLID;
        private System.Windows.Forms.Label lab_taskinfo;
        private System.Windows.Forms.TextBox txtExceptQty;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGrid ds_detail;
        private System.Windows.Forms.Button btn_rescan;
        private System.Windows.Forms.Button btn_billok;
        private System.Windows.Forms.Label txt_scantot;
    }
}