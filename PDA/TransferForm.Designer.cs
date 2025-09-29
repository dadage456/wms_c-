namespace PDA
{
    partial class TransferForm
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
            this.serialNoLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.batchNoLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.commitButton = new System.Windows.Forms.Button();
            this.sourceSiteLabel = new System.Windows.Forms.Label();
            this.matCodeLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxBarcode = new System.Windows.Forms.TextBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.targetSiteLabel = new System.Windows.Forms.Label();
            this.detailListView = new System.Windows.Forms.ListView();
            this.来源库位 = new System.Windows.Forms.ColumnHeader();
            this.目标库位 = new System.Windows.Forms.ColumnHeader();
            this.物料号 = new System.Windows.Forms.ColumnHeader();
            this.批次号 = new System.Windows.Forms.ColumnHeader();
            this.序列号 = new System.Windows.Forms.ColumnHeader();
            this.数量 = new System.Windows.Forms.ColumnHeader();
            this.供应商 = new System.Windows.Forms.ColumnHeader();
            this.子库 = new System.Windows.Forms.ColumnHeader();
            this.项目号 = new System.Windows.Forms.ColumnHeader();
            this.storeRoomlabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.rdoMoveOut = new System.Windows.Forms.RadioButton();
            this.rdoMoveIn = new System.Windows.Forms.RadioButton();
            this.rdoNormal = new System.Windows.Forms.RadioButton();
            this.TasksTitile = new Entity.HeadLabel();
            this.taskDataGrid = new System.Windows.Forms.DataGrid();
            this.QueryButton = new System.Windows.Forms.Button();
            this.projectNumLabel = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.erpOwnerCodeLabel = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.erpStoreRoomLabel = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // qtyLabel
            // 
            this.qtyLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.qtyLabel.Location = new System.Drawing.Point(336, 155);
            this.qtyLabel.Name = "qtyLabel";
            this.qtyLabel.Size = new System.Drawing.Size(138, 17);
            // 
            // serialNoLabel
            // 
            this.serialNoLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.serialNoLabel.Location = new System.Drawing.Point(124, 155);
            this.serialNoLabel.Name = "serialNoLabel";
            this.serialNoLabel.Size = new System.Drawing.Size(137, 17);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label8.Location = new System.Drawing.Point(12, 153);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 20);
            this.label8.Text = "序列号：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // batchNoLabel
            // 
            this.batchNoLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.batchNoLabel.Location = new System.Drawing.Point(337, 128);
            this.batchNoLabel.Name = "batchNoLabel";
            this.batchNoLabel.Size = new System.Drawing.Size(138, 17);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label7.Location = new System.Drawing.Point(267, 126);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 20);
            this.label7.Text = "批号：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(234, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 20);
            this.label5.Text = "目标库位：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // closeButton
            // 
            this.closeButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.closeButton.Location = new System.Drawing.Point(398, 213);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(68, 40);
            this.closeButton.TabIndex = 225;
            this.closeButton.Text = "关闭";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // commitButton
            // 
            this.commitButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.commitButton.Location = new System.Drawing.Point(256, 213);
            this.commitButton.Name = "commitButton";
            this.commitButton.Size = new System.Drawing.Size(68, 40);
            this.commitButton.TabIndex = 224;
            this.commitButton.Text = "提交";
            this.commitButton.Click += new System.EventHandler(this.commitButton_Click);
            // 
            // sourceSiteLabel
            // 
            this.sourceSiteLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.sourceSiteLabel.Location = new System.Drawing.Point(124, 99);
            this.sourceSiteLabel.Name = "sourceSiteLabel";
            this.sourceSiteLabel.Size = new System.Drawing.Size(110, 20);
            // 
            // matCodeLabel
            // 
            this.matCodeLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.matCodeLabel.Location = new System.Drawing.Point(124, 128);
            this.matCodeLabel.Name = "matCodeLabel";
            this.matCodeLabel.Size = new System.Drawing.Size(137, 17);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(12, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 20);
            this.label3.Text = "料号：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(12, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 20);
            this.label1.Text = "来源库位：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(257, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 20);
            this.label4.Text = "数量：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbxBarcode
            // 
            this.tbxBarcode.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.tbxBarcode.Location = new System.Drawing.Point(124, 38);
            this.tbxBarcode.Name = "tbxBarcode";
            this.tbxBarcode.Size = new System.Drawing.Size(351, 26);
            this.tbxBarcode.TabIndex = 219;
            this.tbxBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxBarcode_KeyDown);
            // 
            // lblMsg
            // 
            this.lblMsg.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lblMsg.Location = new System.Drawing.Point(3, 41);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(114, 20);
            this.lblMsg.Text = "请扫描：";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // targetSiteLabel
            // 
            this.targetSiteLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.targetSiteLabel.Location = new System.Drawing.Point(337, 101);
            this.targetSiteLabel.Name = "targetSiteLabel";
            this.targetSiteLabel.Size = new System.Drawing.Size(138, 17);
            // 
            // detailListView
            // 
            this.detailListView.Columns.Add(this.来源库位);
            this.detailListView.Columns.Add(this.目标库位);
            this.detailListView.Columns.Add(this.物料号);
            this.detailListView.Columns.Add(this.批次号);
            this.detailListView.Columns.Add(this.序列号);
            this.detailListView.Columns.Add(this.数量);
            this.detailListView.Columns.Add(this.供应商);
            this.detailListView.Columns.Add(this.子库);
            this.detailListView.Columns.Add(this.项目号);
            this.detailListView.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.detailListView.FullRowSelect = true;
            this.detailListView.Location = new System.Drawing.Point(5, 408);
            this.detailListView.Name = "detailListView";
            this.detailListView.Size = new System.Drawing.Size(471, 188);
            this.detailListView.TabIndex = 234;
            this.detailListView.View = System.Windows.Forms.View.Details;
            // 
            // 来源库位
            // 
            this.来源库位.Text = "来源库位";
            this.来源库位.Width = 120;
            // 
            // 目标库位
            // 
            this.目标库位.Text = "目标库位";
            this.目标库位.Width = 120;
            // 
            // 物料号
            // 
            this.物料号.Text = "物料号";
            this.物料号.Width = 140;
            // 
            // 批次号
            // 
            this.批次号.Text = "批次号";
            this.批次号.Width = 120;
            // 
            // 序列号
            // 
            this.序列号.Text = "序列号";
            this.序列号.Width = 120;
            // 
            // 数量
            // 
            this.数量.Text = "数量";
            this.数量.Width = 60;
            // 
            // 供应商
            // 
            this.供应商.Text = "供应商";
            this.供应商.Width = 0;
            // 
            // 子库
            // 
            this.子库.Text = "子库";
            this.子库.Width = 60;
            // 
            // 项目号
            // 
            this.项目号.Text = "项目号";
            this.项目号.Width = 80;
            // 
            // storeRoomlabel
            // 
            this.storeRoomlabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.storeRoomlabel.Location = new System.Drawing.Point(124, 70);
            this.storeRoomlabel.Name = "storeRoomlabel";
            this.storeRoomlabel.Size = new System.Drawing.Size(121, 20);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label6.Location = new System.Drawing.Point(12, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 20);
            this.label6.Text = "库房：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.btnDelete.Location = new System.Drawing.Point(327, 213);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(68, 40);
            this.btnDelete.TabIndex = 239;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(12, 211);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 20);
            this.label2.Text = "作业模式：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // rdoMoveOut
            // 
            this.rdoMoveOut.Checked = true;
            this.rdoMoveOut.Location = new System.Drawing.Point(117, 213);
            this.rdoMoveOut.Name = "rdoMoveOut";
            this.rdoMoveOut.Size = new System.Drawing.Size(55, 20);
            this.rdoMoveOut.TabIndex = 273;
            this.rdoMoveOut.Text = "移出";
            this.rdoMoveOut.CheckedChanged += new System.EventHandler(this.rdoMoveOut_CheckedChanged);
            // 
            // rdoMoveIn
            // 
            this.rdoMoveIn.Location = new System.Drawing.Point(117, 239);
            this.rdoMoveIn.Name = "rdoMoveIn";
            this.rdoMoveIn.Size = new System.Drawing.Size(55, 20);
            this.rdoMoveIn.TabIndex = 274;
            this.rdoMoveIn.TabStop = false;
            this.rdoMoveIn.Text = "移入";
            this.rdoMoveIn.CheckedChanged += new System.EventHandler(this.rdoMoveIn_CheckedChanged);
            // 
            // rdoNormal
            // 
            this.rdoNormal.Location = new System.Drawing.Point(45, 241);
            this.rdoNormal.Name = "rdoNormal";
            this.rdoNormal.Size = new System.Drawing.Size(61, 20);
            this.rdoNormal.TabIndex = 275;
            this.rdoNormal.TabStop = false;
            this.rdoNormal.Text = "移库";
            this.rdoNormal.Visible = false;
            this.rdoNormal.CheckedChanged += new System.EventHandler(this.rdoNormal_CheckedChanged);
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
            this.TasksTitile.Text = "移库作业";
            // 
            // taskDataGrid
            // 
            this.taskDataGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.taskDataGrid.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.taskDataGrid.Location = new System.Drawing.Point(5, 267);
            this.taskDataGrid.Name = "taskDataGrid";
            this.taskDataGrid.Size = new System.Drawing.Size(469, 136);
            this.taskDataGrid.TabIndex = 292;
            this.taskDataGrid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.taskDataGrid_MouseUp);
            // 
            // QueryButton
            // 
            this.QueryButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.QueryButton.Location = new System.Drawing.Point(185, 213);
            this.QueryButton.Name = "QueryButton";
            this.QueryButton.Size = new System.Drawing.Size(68, 40);
            this.QueryButton.TabIndex = 294;
            this.QueryButton.Text = "查询";
            this.QueryButton.Click += new System.EventHandler(this.QueryButton_Click);
            // 
            // projectNumLabel
            // 
            this.projectNumLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.projectNumLabel.Location = new System.Drawing.Point(336, 182);
            this.projectNumLabel.Name = "projectNumLabel";
            this.projectNumLabel.Size = new System.Drawing.Size(138, 17);
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label10.Location = new System.Drawing.Point(244, 180);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 20);
            this.label10.Text = "项目号：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // erpOwnerCodeLabel
            // 
            this.erpOwnerCodeLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.erpOwnerCodeLabel.Location = new System.Drawing.Point(124, 182);
            this.erpOwnerCodeLabel.Name = "erpOwnerCodeLabel";
            this.erpOwnerCodeLabel.Size = new System.Drawing.Size(129, 17);
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label12.Location = new System.Drawing.Point(12, 180);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(105, 20);
            this.label12.Text = "拥有方：";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // erpStoreRoomLabel
            // 
            this.erpStoreRoomLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.erpStoreRoomLabel.Location = new System.Drawing.Point(336, 72);
            this.erpStoreRoomLabel.Name = "erpStoreRoomLabel";
            this.erpStoreRoomLabel.Size = new System.Drawing.Size(138, 17);
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label13.Location = new System.Drawing.Point(257, 70);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 20);
            this.label13.Text = "子库：";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TransferForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(480, 600);
            this.ControlBox = false;
            this.Controls.Add(this.erpStoreRoomLabel);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.erpOwnerCodeLabel);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.projectNumLabel);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.QueryButton);
            this.Controls.Add(this.taskDataGrid);
            this.Controls.Add(this.rdoNormal);
            this.Controls.Add(this.rdoMoveIn);
            this.Controls.Add(this.rdoMoveOut);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.storeRoomlabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.detailListView);
            this.Controls.Add(this.targetSiteLabel);
            this.Controls.Add(this.qtyLabel);
            this.Controls.Add(this.serialNoLabel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.batchNoLabel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.commitButton);
            this.Controls.Add(this.sourceSiteLabel);
            this.Controls.Add(this.matCodeLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TasksTitile);
            this.Controls.Add(this.tbxBarcode);
            this.Controls.Add(this.lblMsg);
            this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "TransferForm";
            this.Text = "移库作业";
            this.Load += new System.EventHandler(this.TransferForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label qtyLabel;
        private System.Windows.Forms.Label serialNoLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label batchNoLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button commitButton;
        private System.Windows.Forms.Label sourceSiteLabel;
        private System.Windows.Forms.Label matCodeLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private Entity.HeadLabel TasksTitile;
        private System.Windows.Forms.TextBox tbxBarcode;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label targetSiteLabel;
        private System.Windows.Forms.ListView detailListView;
        private System.Windows.Forms.ColumnHeader 来源库位;
        private System.Windows.Forms.ColumnHeader 物料号;
        private System.Windows.Forms.ColumnHeader 批次号;
        private System.Windows.Forms.ColumnHeader 序列号;
        private System.Windows.Forms.ColumnHeader 数量;
        private System.Windows.Forms.Label storeRoomlabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ColumnHeader 目标库位;
        private System.Windows.Forms.ColumnHeader 供应商;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdoMoveOut;
        private System.Windows.Forms.RadioButton rdoMoveIn;
        private System.Windows.Forms.RadioButton rdoNormal;
        private System.Windows.Forms.ColumnHeader 子库;
        private System.Windows.Forms.DataGrid taskDataGrid;
        private System.Windows.Forms.Button QueryButton;
        private System.Windows.Forms.Label projectNumLabel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label erpOwnerCodeLabel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ColumnHeader 项目号;
        private System.Windows.Forms.Label erpStoreRoomLabel;
        private System.Windows.Forms.Label label13;

    }
}