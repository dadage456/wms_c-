namespace PDA
{
    partial class MainFrm
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
            this.bindingButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.btnReceiveOut = new System.Windows.Forms.Button();
            this.btnPurchaseIn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.updateButton = new System.Windows.Forms.Button();
            this.mergerButton = new System.Windows.Forms.Button();
            this.upButton = new System.Windows.Forms.Button();
            this.downButton = new System.Windows.Forms.Button();
            this.checkButton = new System.Windows.Forms.Button();
            this.moveButton = new System.Windows.Forms.Button();
            this.btnCheckL = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.P_BeatOut = new System.Windows.Forms.Button();
            this.L_BeatOut = new System.Windows.Forms.Button();
            this.btnDaoHuoSaoMa = new System.Windows.Forms.Button();
            this.trayToUpButton = new System.Windows.Forms.Button();
            this.downToTrayButton = new System.Windows.Forms.Button();
            this.SupplierButton = new System.Windows.Forms.Button();
            this.QueryButton = new System.Windows.Forms.Button();
            this.PalletOutButton = new System.Windows.Forms.Button();
            this.downOutButton = new System.Windows.Forms.Button();
            this.btnSenderMtl = new System.Windows.Forms.Button();
            this.trayDecButton = new System.Windows.Forms.Button();
            this.trayAddButton = new System.Windows.Forms.Button();
            this.AllocateBN = new System.Windows.Forms.Button();
            this.TasksTitile = new Entity.HeadLabel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bindingButton
            // 
            this.bindingButton.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.bindingButton.Location = new System.Drawing.Point(214, 63);
            this.bindingButton.Name = "bindingButton";
            this.bindingButton.Size = new System.Drawing.Size(184, 41);
            this.bindingButton.TabIndex = 13;
            this.bindingButton.Text = "立库入库(&Z)";
            this.bindingButton.Click += new System.EventHandler(this.bindingButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.closeButton.Location = new System.Drawing.Point(214, 434);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(184, 41);
            this.closeButton.TabIndex = 14;
            this.closeButton.Text = "退出(&E)";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // btnReceiveOut
            // 
            this.btnReceiveOut.Location = new System.Drawing.Point(0, 0);
            this.btnReceiveOut.Name = "btnReceiveOut";
            this.btnReceiveOut.Size = new System.Drawing.Size(72, 20);
            this.btnReceiveOut.TabIndex = 23;
            // 
            // btnPurchaseIn
            // 
            this.btnPurchaseIn.Location = new System.Drawing.Point(0, 0);
            this.btnPurchaseIn.Name = "btnPurchaseIn";
            this.btnPurchaseIn.Size = new System.Drawing.Size(72, 20);
            this.btnPurchaseIn.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(166, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 47);
            this.label2.Text = "功能选择";
            // 
            // updateButton
            // 
            this.updateButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.updateButton.Location = new System.Drawing.Point(314, 578);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(105, 38);
            this.updateButton.TabIndex = 22;
            this.updateButton.Text = "自动更新(&U)";
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // mergerButton
            // 
            this.mergerButton.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.mergerButton.Location = new System.Drawing.Point(63, 578);
            this.mergerButton.Name = "mergerButton";
            this.mergerButton.Size = new System.Drawing.Size(97, 38);
            this.mergerButton.TabIndex = 26;
            this.mergerButton.Text = "合盘(&H)";
            this.mergerButton.Visible = false;
            this.mergerButton.Click += new System.EventHandler(this.mergerButton_Click);
            // 
            // upButton
            // 
            this.upButton.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.upButton.Location = new System.Drawing.Point(14, 63);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(184, 41);
            this.upButton.TabIndex = 27;
            this.upButton.Text = "平库入库(&S)";
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // downButton
            // 
            this.downButton.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.downButton.Location = new System.Drawing.Point(14, 169);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(184, 41);
            this.downButton.TabIndex = 28;
            this.downButton.Text = "平库出库(&X)";
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // checkButton
            // 
            this.checkButton.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.checkButton.Location = new System.Drawing.Point(14, 116);
            this.checkButton.Name = "checkButton";
            this.checkButton.Size = new System.Drawing.Size(184, 41);
            this.checkButton.TabIndex = 29;
            this.checkButton.Text = "平库盘点(&P)";
            this.checkButton.Click += new System.EventHandler(this.checkButton_Click);
            // 
            // moveButton
            // 
            this.moveButton.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.moveButton.Location = new System.Drawing.Point(214, 381);
            this.moveButton.Name = "moveButton";
            this.moveButton.Size = new System.Drawing.Size(184, 41);
            this.moveButton.TabIndex = 31;
            this.moveButton.Text = "平库移库(&Y)";
            this.moveButton.Click += new System.EventHandler(this.moveButton_Click);
            // 
            // btnCheckL
            // 
            this.btnCheckL.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.btnCheckL.Location = new System.Drawing.Point(214, 116);
            this.btnCheckL.Name = "btnCheckL";
            this.btnCheckL.Size = new System.Drawing.Size(184, 41);
            this.btnCheckL.TabIndex = 33;
            this.btnCheckL.Text = "立库盘点(&L)";
            this.btnCheckL.Click += new System.EventHandler(this.btnCheckL_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.P_BeatOut);
            this.panel1.Controls.Add(this.L_BeatOut);
            this.panel1.Controls.Add(this.btnDaoHuoSaoMa);
            this.panel1.Controls.Add(this.trayToUpButton);
            this.panel1.Controls.Add(this.downToTrayButton);
            this.panel1.Controls.Add(this.SupplierButton);
            this.panel1.Controls.Add(this.QueryButton);
            this.panel1.Controls.Add(this.PalletOutButton);
            this.panel1.Controls.Add(this.downOutButton);
            this.panel1.Controls.Add(this.btnSenderMtl);
            this.panel1.Controls.Add(this.upButton);
            this.panel1.Controls.Add(this.btnCheckL);
            this.panel1.Controls.Add(this.moveButton);
            this.panel1.Controls.Add(this.closeButton);
            this.panel1.Controls.Add(this.bindingButton);
            this.panel1.Controls.Add(this.checkButton);
            this.panel1.Controls.Add(this.downButton);
            this.panel1.Location = new System.Drawing.Point(38, 79);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(407, 493);
            // 
            // P_BeatOut
            // 
            this.P_BeatOut.BackColor = System.Drawing.Color.Moccasin;
            this.P_BeatOut.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.P_BeatOut.Location = new System.Drawing.Point(14, 329);
            this.P_BeatOut.Name = "P_BeatOut";
            this.P_BeatOut.Size = new System.Drawing.Size(184, 41);
            this.P_BeatOut.TabIndex = 45;
            this.P_BeatOut.Text = "平库节拍(&K)";
            this.P_BeatOut.Click += new System.EventHandler(this.P_BeatOut_Click);
            // 
            // L_BeatOut
            // 
            this.L_BeatOut.BackColor = System.Drawing.Color.Moccasin;
            this.L_BeatOut.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.L_BeatOut.Location = new System.Drawing.Point(214, 329);
            this.L_BeatOut.Name = "L_BeatOut";
            this.L_BeatOut.Size = new System.Drawing.Size(184, 41);
            this.L_BeatOut.TabIndex = 45;
            this.L_BeatOut.Text = "立库节拍(&Z)";
            this.L_BeatOut.Click += new System.EventHandler(this.L_BeatOut_Click);
            // 
            // btnDaoHuoSaoMa
            // 
            this.btnDaoHuoSaoMa.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.btnDaoHuoSaoMa.Location = new System.Drawing.Point(14, 13);
            this.btnDaoHuoSaoMa.Name = "btnDaoHuoSaoMa";
            this.btnDaoHuoSaoMa.Size = new System.Drawing.Size(184, 41);
            this.btnDaoHuoSaoMa.TabIndex = 40;
            this.btnDaoHuoSaoMa.Text = "到货扫码(&R)";
            this.btnDaoHuoSaoMa.Click += new System.EventHandler(this.btnDaoHuoSaoMa_Click);
            // 
            // trayToUpButton
            // 
            this.trayToUpButton.BackColor = System.Drawing.Color.Moccasin;
            this.trayToUpButton.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.trayToUpButton.Location = new System.Drawing.Point(214, 275);
            this.trayToUpButton.Name = "trayToUpButton";
            this.trayToUpButton.Size = new System.Drawing.Size(184, 41);
            this.trayToUpButton.TabIndex = 42;
            this.trayToUpButton.Text = "调拨入库(&U)";
            this.trayToUpButton.Click += new System.EventHandler(this.trayToUpButton_Click);
            // 
            // downToTrayButton
            // 
            this.downToTrayButton.BackColor = System.Drawing.Color.Moccasin;
            this.downToTrayButton.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.downToTrayButton.Location = new System.Drawing.Point(14, 275);
            this.downToTrayButton.Name = "downToTrayButton";
            this.downToTrayButton.Size = new System.Drawing.Size(184, 41);
            this.downToTrayButton.TabIndex = 41;
            this.downToTrayButton.Text = "工单调拨(&D)";
            this.downToTrayButton.Click += new System.EventHandler(this.downToTrayButton_Click);
            // 
            // SupplierButton
            // 
            this.SupplierButton.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.SupplierButton.Location = new System.Drawing.Point(14, 381);
            this.SupplierButton.Name = "SupplierButton";
            this.SupplierButton.Size = new System.Drawing.Size(184, 41);
            this.SupplierButton.TabIndex = 40;
            this.SupplierButton.Text = "供应商码(&C)";
            this.SupplierButton.Click += new System.EventHandler(this.SupplierButton_Click);
            // 
            // QueryButton
            // 
            this.QueryButton.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.QueryButton.Location = new System.Drawing.Point(16, 434);
            this.QueryButton.Name = "QueryButton";
            this.QueryButton.Size = new System.Drawing.Size(184, 41);
            this.QueryButton.TabIndex = 32;
            this.QueryButton.Text = "扫描查询(&Q)";
            this.QueryButton.Click += new System.EventHandler(this.QueryButton_Click);
            // 
            // PalletOutButton
            // 
            this.PalletOutButton.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.PalletOutButton.Location = new System.Drawing.Point(214, 222);
            this.PalletOutButton.Name = "PalletOutButton";
            this.PalletOutButton.Size = new System.Drawing.Size(184, 41);
            this.PalletOutButton.TabIndex = 37;
            this.PalletOutButton.Text = "整盘出库(&O)";
            this.PalletOutButton.Click += new System.EventHandler(this.PalletOutButton_Click);
            // 
            // downOutButton
            // 
            this.downOutButton.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.downOutButton.Location = new System.Drawing.Point(214, 169);
            this.downOutButton.Name = "downOutButton";
            this.downOutButton.Size = new System.Drawing.Size(184, 41);
            this.downOutButton.TabIndex = 36;
            this.downOutButton.Text = "在线拣选(&J)";
            this.downOutButton.Click += new System.EventHandler(this.downOutButton_Click);
            // 
            // btnSenderMtl
            // 
            this.btnSenderMtl.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.btnSenderMtl.Location = new System.Drawing.Point(14, 222);
            this.btnSenderMtl.Name = "btnSenderMtl";
            this.btnSenderMtl.Size = new System.Drawing.Size(184, 41);
            this.btnSenderMtl.TabIndex = 35;
            this.btnSenderMtl.Text = "拉式发料(&F)";
            this.btnSenderMtl.Click += new System.EventHandler(this.btnSenderMtl_Click);
            // 
            // trayDecButton
            // 
            this.trayDecButton.BackColor = System.Drawing.Color.Moccasin;
            this.trayDecButton.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.trayDecButton.Location = new System.Drawing.Point(54, 578);
            this.trayDecButton.Name = "trayDecButton";
            this.trayDecButton.Size = new System.Drawing.Size(184, 41);
            this.trayDecButton.TabIndex = 45;
            this.trayDecButton.Text = "托盘拆料(&D)";
            this.trayDecButton.Visible = false;
            this.trayDecButton.Click += new System.EventHandler(this.trayDecButton_Click);
            // 
            // trayAddButton
            // 
            this.trayAddButton.BackColor = System.Drawing.Color.Moccasin;
            this.trayAddButton.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.trayAddButton.Location = new System.Drawing.Point(54, 560);
            this.trayAddButton.Name = "trayAddButton";
            this.trayAddButton.Size = new System.Drawing.Size(184, 41);
            this.trayAddButton.TabIndex = 44;
            this.trayAddButton.Text = "托盘补料(&A)";
            this.trayAddButton.Visible = false;
            this.trayAddButton.Click += new System.EventHandler(this.trayAddButton_Click);
            // 
            // AllocateBN
            // 
            this.AllocateBN.Enabled = false;
            this.AllocateBN.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.AllocateBN.Location = new System.Drawing.Point(166, 578);
            this.AllocateBN.Name = "AllocateBN";
            this.AllocateBN.Size = new System.Drawing.Size(142, 38);
            this.AllocateBN.TabIndex = 38;
            this.AllocateBN.Text = "线下配盘(&T)";
            this.AllocateBN.Visible = false;
            this.AllocateBN.Click += new System.EventHandler(this.AllocateBN_Click);
            // 
            // TasksTitile
            // 
            this.TasksTitile.BackColor = System.Drawing.SystemColors.HotTrack;
            this.TasksTitile.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.TasksTitile.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.TasksTitile.Location = new System.Drawing.Point(-8, 0);
            this.TasksTitile.Name = "TasksTitile";
            this.TasksTitile.Size = new System.Drawing.Size(485, 24);
            this.TasksTitile.TabIndex = 15;
            this.TasksTitile.Text = "菜单";
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(480, 640);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.trayAddButton);
            this.Controls.Add(this.trayDecButton);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.TasksTitile);
            this.Controls.Add(this.btnReceiveOut);
            this.Controls.Add(this.AllocateBN);
            this.Controls.Add(this.btnPurchaseIn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mergerButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainFrm";
            this.Text = "功能菜单";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainFrm_KeyDown);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bindingButton;
        private Entity.HeadLabel TasksTitile;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button btnReceiveOut;
        private System.Windows.Forms.Button btnPurchaseIn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button mergerButton;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.Button checkButton;
        private System.Windows.Forms.Button moveButton;
        private System.Windows.Forms.Button btnCheckL;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSenderMtl;
        private System.Windows.Forms.Button downOutButton;
        private System.Windows.Forms.Button PalletOutButton;
        private System.Windows.Forms.Button QueryButton;
        private System.Windows.Forms.Button AllocateBN;
        private System.Windows.Forms.Button SupplierButton;
        private System.Windows.Forms.Button trayAddButton;
        private System.Windows.Forms.Button trayToUpButton;
        private System.Windows.Forms.Button downToTrayButton;
        private System.Windows.Forms.Button trayDecButton;
        private System.Windows.Forms.Button btnDaoHuoSaoMa;
        private System.Windows.Forms.Button L_BeatOut;
        private System.Windows.Forms.Button P_BeatOut;
    }
}