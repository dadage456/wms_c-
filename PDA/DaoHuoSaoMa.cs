using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Entity;
using BizLayer;
using PDA;

namespace PDA
{
    public partial class DaoHuoSaoMa : Form
    {
        public DaoHuoSaoMa()
        {
            InitializeComponent();
        }

        DataTable taskhideTable = new DataTable();
        DataTable taskDispTable = new DataTable();
        string is_billkey = "", is_detailkey = "";
        DataTable detailTable = new DataTable();
        MiddleService service = new MiddleService();
        string taskId = string.Empty;

        private void DaoHuoSaoMaFrm_Load(object sender, EventArgs e)
        {
            ds_bill.DataSource = null; //任务主表
            ds_detail.DataSource = null; //任务子表
            this.txtBarcode.Text = "";
            this.txtGoodQty.Text = "";
            this.txtBarcode.Focus();
        }

        //关闭窗口
        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        //查询所有的可到货检验的任务
        private void taskButton_Click(object sender, EventArgs e)
        {
            List<string> args_nv = new List<string>();
            if(txtBILLID.Text != "")
            {
                args_nv.Add("billno"); args_nv.Add(txtBILLID.Text);
            }
            SQLHelper s = new SQLHelper();
            DataSet ds = s.WS("PDA_DH_01CANDEAL", args_nv.ToArray());
            if (ds == null || s.sqlcode == -1)
            {
                MessageBox.Show("查询待到货验货信息错误", "提示"); 
            }
            taskhideTable = ds.Tables[0]; //查询出的待 到货检验的任务
            //可能需要 distinct billkey 的进行显示即可
            //
            taskDispTable = taskhideTable.Copy();
            taskDispTable.Clear();
            for (int r = 0; r < taskhideTable.Rows.Count; r++)
            {
                bool lb_no_add = false;
                string ls_billkey = SQLHelper.SNVL(taskhideTable.Rows[r]["BILLKEY"], "");
                for (int r1 = 0; r1 < taskDispTable.Rows.Count; r1++)
                {
                    if (ls_billkey.Equals(SQLHelper.SNVL(taskDispTable.Rows[r1]["BILLKEY"],"")))
                    { lb_no_add = true; break; }
                }
                if (!lb_no_add)
                {
                    DataRow dr = taskDispTable.NewRow();
                    dr["BILLKEY"] = ls_billkey;
                    dr["BILLDESC"] = SQLHelper.SNVL(taskhideTable.Rows[r]["BILLDESC"], "");
                    taskDispTable.Rows.Add(dr);
                }
            }
            ds_bill.DataSource = taskDispTable;
            is_billkey = ""; is_detailkey = "";
        }

        //任务主窗口,点击某行后
        private void taskDataGrid_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                DataGrid.HitTestInfo hti = ds_bill.HitTest(e.X, e.Y);
                switch (hti.Type)
                {
                    case DataGrid.HitTestType.ColumnHeader:     //列头排序
                        string columnHeader = taskhideTable.Columns[hti.Column].ColumnName;
                        //taskTable.sSort = columnHeader + " ASC";
                        //ds_bill.DataSource = taskTable;
                        break;
                    case DataGrid.HitTestType.RowHeader:        //行选中
                        is_billkey = ds_bill[ds_bill.CurrentRowIndex, taskDispTable.Columns.IndexOf("BILLKEY")].ToString();
                        lab_taskinfo.Text = is_billkey + ds_bill[ds_bill.CurrentRowIndex, taskDispTable.Columns.IndexOf("BILLDESC")].ToString();
                        txt_scantot.Text = "";
                        //
                        this.txtBarcode.Text = "";
                        this.txtBarcode.Focus(); //取得焦点
                        this.txtGoodQty.Text = "";
                        this.txtExceptQty.Text = "";
                        //
                        this.detailTable.Rows.Clear(); //删除所有的明细信息
                        //
                        btn_shuaxin_Click(null, null);
                        break;
                }
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }

        }

        //扫条码后的处理
        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        { 
            try
            {
                if (e.KeyCode == Keys.Return)
                {
                    string barcode = this.txtBarcode.Text.Trim();
                    if (barcode == "")
                    {
                        this.labMat.Text = ""; //物料信息
                        this.txtBarcode.Text = ""; //条码
                        this.txtGoodQty.Text = ""; //正品数
                        this.txtExceptQty.Text = ""; //异常数
                        this.txtBarcode.Focus();
                        this.txtBarcode.SelectAll();
                        return;
                    }
                    txtBarcodeModified(barcode);
                }
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        
        //解析条码的值,并显示相关的信息
        private void txtBarcodeModified(string barcodevalue)
        {
            //条码的构成为:
  // select 'MC'||m.matcode||'*DN*DV*SN'||ti.hintbatchno||'*MF*AG'||g.parno||'*DC*' ,
  //     '$KW$'||si.storesiteno,
  //     '$TP$TP003482', -- 应输入托盘号,此号需要人工确定

//            --- 新的条码签格式, 估计还没有正式启动2018-03-06 
//PO(采购订单)+13位*PL(装箱单)+13位*MC(物料)+13位*
//DN(图纸编号)+65位*DV(图纸版本号)+06位*BN(批次号)+10位*SN(序列号)+12位*
//MF(制造商)+04位*AG(供应商)+04位*PD(生产日期)+08位*WD(有效期)+03位*QT(数量)+04位
            string ls_mc = SQLHelper.getBarcodeField(barcodevalue, "MC");
            string ls_bn = SQLHelper.getBarcodeField(barcodevalue, "BN");
            string ls_sn = SQLHelper.getBarcodeField(barcodevalue, "SN");
            if (ls_mc.Equals("") || (ls_bn.Equals("") && ls_sn.Equals("")))
            {
                MessageBox.Show("没有从条码中解析到物料及批次号序列号等信息", "提示信息"); return;
            }
            int li_found_row = -1;
            for (int r = 0; r < taskhideTable.Rows.Count; r++)
            {
                if (is_billkey.Equals(SQLHelper.SNVL(taskhideTable.Rows[r]["BILLKEY"],"")) && ls_mc.Equals(SQLHelper.SNVL(taskhideTable.Rows[r]["Matcode"],""))) //物料编码
                {
                    //M.MATCODECONTROL, --'0'序列控制，'1'批次控制，'2' 无批次序列控制
                    string ls_MATCODECONTROL= SQLHelper.SNVL(taskhideTable.Rows[r]["MATCODECONTROL"],"");
                    if (ls_MATCODECONTROL == "0")
                    {
                        if (ls_bn.Equals(SQLHelper.SNVL(taskhideTable.Rows[r]["BATCHNO"],"")) && ls_sn.Equals(SQLHelper.SNVL(taskhideTable.Rows[r]["SN"],"")))
                        {
                            li_found_row = r; break;
                        }
                    }
                    else
                    {
                        if (ls_bn.Equals(SQLHelper.SNVL(taskhideTable.Rows[r]["BATCHNO"],"")))
                        {
                            li_found_row = r; break;
                        }
                    }
                }
            }
            //显示明细信息
            if (li_found_row == -1)
            {
                MessageBox.Show("没有找到对应的物料批次信息","提示信息"); return;
            }
            this.is_detailkey = SQLHelper.SNVL(taskhideTable.Rows[li_found_row]["DETAILKEY"], "");
            this.labMat.Text = SQLHelper.SNVL(taskhideTable.Rows[li_found_row]["DETAILDESC"], ""); //物料信息
            this.txtBarcode.Text = ""; //条码
            decimal ldc_GoodQty = SQLHelper.NNVL(taskhideTable.Rows[li_found_row]["goodqty"], 0);
            decimal ldc_ExceptQty = SQLHelper.NNVL(taskhideTable.Rows[li_found_row]["EXCEPT_QTY"], 0);
            if (taskhideTable.Rows[li_found_row]["goodqty"] == null || Convert.IsDBNull(taskhideTable.Rows[li_found_row]["goodqty"]) )
            {
                ldc_GoodQty = SQLHelper.NNVL(taskhideTable.Rows[li_found_row]["qty"], 0);
            }
            this.txtGoodQty.Text = "" + ldc_GoodQty; //正品数
            this.txtExceptQty.Text = "" + ldc_ExceptQty; //异常数
        }

        private void scantot()
        {
            txt_scantot.Text = "";
            decimal ldc_tqty = 0, ldc_gqty = 0, ldc_eqty = 0;
            for (int r = detailTable.Rows.Count - 1; r >= 0; r--)
            {
                if (is_billkey.Equals(SQLHelper.SNVL(detailTable.Rows[r]["BILLKEY"], ""))) //物料编码
                {
                    ldc_tqty += SQLHelper.NNVL(detailTable.Rows[r]["qty"], 0);
                    ldc_gqty += SQLHelper.NNVL(detailTable.Rows[r]["goodqty"], 0);
                    ldc_eqty += SQLHelper.NNVL(detailTable.Rows[r]["EXCEPT_QTY"], 0);
                }
                else
                {
                    detailTable.Rows.RemoveAt(r); //非此单的删除
                }
            }
            txt_scantot.Text = "已扫描正品数:" + ldc_gqty + ",异常:" + ldc_eqty + "/总任务数:" + ldc_tqty+" 差:"+(ldc_tqty - ldc_gqty - ldc_eqty);
        }

        
        private void ds_bill_CurrentCellChanged(object sender, EventArgs e)
        {

        }

        //整单刷新
        private void btn_shuaxin_Click(object sender, EventArgs e)
        {
            this.detailTable = this.taskhideTable.Copy();
            this.ds_detail.DataSource = this.detailTable;
            for (int r =detailTable.Rows.Count -1; r >= 0; r--)
            {
                if (is_billkey.Equals(SQLHelper.SNVL(detailTable.Rows[r]["BILLKEY"],""))) //物料编码
                {}
                else
                {
                    detailTable.Rows.RemoveAt(r); //非此单的删除
                }
            }
            scantot();
        }

        //整单重扫
        private void btnrescan_Click(object sender, EventArgs e)
        {
            this.detailTable = this.taskhideTable.Copy();
            this.ds_detail.DataSource = this.detailTable;
            for (int r = detailTable.Rows.Count - 1; r >= 0; r--)
            {
                if (is_billkey.Equals(SQLHelper.SNVL(detailTable.Rows[r]["BILLKEY"],""))) //物料编码
                {
                    detailTable.Rows[r]["goodqty"] = Convert.DBNull;
                    detailTable.Rows[r]["EXCEPT_QTY"] = Convert.DBNull;
                }
                else
                {
                    detailTable.Rows.RemoveAt(r); //非此单的删除
                }
            }
            scantot();
        }

        //明细条记录确认
        private void btnDetailQr_Click(object sender, EventArgs e)
        {
            if (is_billkey == null || is_billkey == "" || is_detailkey == null || is_detailkey == "") {
                MessageBox.Show("请确定要错误的物料批次","提示信息");
                return;
            }
            
            string ls_GOODQTY  = this.txtGoodQty.Text; //正品数量
            string ls_EXCEPT_QTY  = this.txtExceptQty.Text; //异常数量
            if (ls_GOODQTY == null || ls_GOODQTY == "" || ls_EXCEPT_QTY == null || ls_EXCEPT_QTY == "")
            {
                MessageBox.Show("请指定正品数量和异常品数量", "提示信息");
                return;
            }
            if ("" + decimal.Parse(ls_GOODQTY) == ls_GOODQTY && "" + decimal.Parse(ls_EXCEPT_QTY) == ls_EXCEPT_QTY)
            {}else
            {
                MessageBox.Show("输入的数量必须是合法的数值!", "提示信息");
                return;
            }
            List<string> args_nv = new List<string>();
            args_nv.Add("billkey"); args_nv.Add(is_billkey);
            args_nv.Add("detailkey"); args_nv.Add(is_detailkey);

            args_nv.Add("GOODQTY"); args_nv.Add(ls_GOODQTY);
            args_nv.Add("EXCEPT_QTY"); args_nv.Add(ls_EXCEPT_QTY);

            SQLHelper s = new SQLHelper();
            DataSet ds = s.WS("PDA_DH_02DETAILQTY", args_nv.ToArray());
            if (ds == null || ds.Tables[0].Rows.Count != 1)
            {
                MessageBox.Show("对明细行" + is_billkey +"|"+is_detailkey+ "进行确认错,远程调用没有返回信息", "提示信息");
                return;
            }
            decimal li_retcode = SQLHelper.NNVL(ds.Tables[0].Rows[0]["RET_CODE"], -1);
            if (li_retcode == 0)
            {
                for (int r = detailTable.Rows.Count - 1; r >= 0; r--)
                {
                    if (is_billkey.Equals(SQLHelper.SNVL(detailTable.Rows[r]["BILLKEY"],"")) && is_detailkey.Equals(SQLHelper.SNVL(detailTable.Rows[r]["DETAILKEY"],""))) //物料编码
                    {
                        detailTable.Rows[r]["GOODQTY"] = SQLHelper.NNVL(ls_GOODQTY,0);
                        detailTable.Rows[r]["EXCEPT_QTY"] = SQLHelper.NNVL(ls_EXCEPT_QTY, 0);
                    }
                }
                for (int r = taskhideTable.Rows.Count - 1; r >= 0; r--)
                {
                    if (is_billkey.Equals(SQLHelper.SNVL(taskhideTable.Rows[r]["BILLKEY"],"")) && is_detailkey.Equals(SQLHelper.SNVL(taskhideTable.Rows[r]["DETAILKEY"],""))) //物料编码
                    {
                        taskhideTable.Rows[r]["GOODQTY"] = SQLHelper.NNVL(ls_GOODQTY, 0);
                        taskhideTable.Rows[r]["EXCEPT_QTY"] = SQLHelper.NNVL(ls_EXCEPT_QTY, 0);
                    }
                }
                scantot();
                //
                this.txtBarcode.Text = ""; is_detailkey = "";
                this.txtBarcode.Focus(); //取得焦点
                this.txtGoodQty.Text = "";
                this.txtExceptQty.Text = "";
                return;
            }
            else
            {
                string ls_mess = SQLHelper.SNVL(ds.Tables[0].Rows[0]["RET_MESS"], "");
                MessageBox.Show("对明细行" + is_billkey + "|" + is_detailkey + "进行确认错," + ls_mess, "提示信息");
                return;
            }
        }

        //执行整单完成
        private void btn_billok_Click(object sender, EventArgs e)
        {
            if (is_billkey == null || is_billkey == "") { }
            else
            {
                if (MessageBox.Show("是否对单据" + is_billkey + "结单?", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                }
                else { return; }
            }
            List<string> args_nv = new List<string>();
            args_nv.Add("billkey"); args_nv.Add(is_billkey);
            SQLHelper s = new SQLHelper();
            DataSet ds = s.WS("PDA_DH_03CLOSE", args_nv.ToArray());
            if(ds == null || ds.Tables[0].Rows.Count != 1)
            {
                MessageBox.Show("对单据"+is_billkey+"进行结单错,远程调用没有返回信息","提示信息");
                return;
            }
            decimal li_retcode =  SQLHelper.NNVL( ds.Tables[0].Rows[0]["RET_CODE"], -1);
            if(li_retcode == 0)
            {
                MessageBox.Show("对单据" + is_billkey + "结单完成", "提示信息");
                this.txtBarcode.Text = "";
                this.txtBarcode.Focus(); //取得焦点
                this.txtGoodQty.Text = "";
                this.txtExceptQty.Text = "";
                is_billkey = ""; is_detailkey = "";
                this.detailTable.Rows.Clear(); //删除所有的明细信息
                return;
            }else{
                string ls_mess = SQLHelper.SNVL( ds.Tables[0].Rows[0]["RET_MESS"], "");
                MessageBox.Show("对单据" + is_billkey + "结单错," + ls_mess, "提示信息");
                return;
            }
        }

        

        /*
        /// <summary>
        /// 显示用户下架任务
        /// </summary>
        private void getTaskListbyUser()
        {
            try
            {
                this.txtTaskNo.Text = "";
                this.txtBarcode.Text = "";
                this.txtGoodQty.Text = "";
                this.roomLabel.Text = "";
                ds_bill.DataSource = null;
                DataSet ds = service.GetOutTask(User.Instance.UserData.UserId, "1", "0", "2");
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    //throw new Exception("暂无下架任务");
                    return;
                }
                taskTable = ds.Tables[0];
                dv.Table = taskTable;
                ds_bill.DataSource = dv;
                AutoSizeCol(taskTable.Columns.Count);
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        private void AutoSizeCol(int p)
        {
            //2016 3 9 增加删除  柏磊
            this.ds_bill.TableStyles.Clear();
            //定义DataGridTableStyle  
            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = dv.Table.TableName;  //映射style对应数据源的表名，很重要，否则无数据显示  

            //分别对列进行渲染，其中前三列用for循环实现，对列宽进行设定，值为75   
            for (int i = 0; i < p; i++)
            {
                DataGridColumnStyle aColumnTextColumnStyle = new DataGridTextBoxColumn();//定义该列用textbox来进行渲染 
                aColumnTextColumnStyle.HeaderText = dv.Table.Columns[i].ColumnName;   //列头 
                aColumnTextColumnStyle.MappingName = dv.Table.Columns[i].ColumnName; //映射数据源的列名，很重要，否则无数据显示 
                if (i == 0) aColumnTextColumnStyle.Width = 150;//凭证号
                if (i == 1) aColumnTextColumnStyle.Width = 80; //库房号
                if (i == 2) aColumnTextColumnStyle.Width = 150;//出库单号
                if (i == 3) aColumnTextColumnStyle.Width = 120;//工位
                if (i == 4) aColumnTextColumnStyle.Width = 100;//任务号
                if (i == 5) aColumnTextColumnStyle.Width = 100;//来源单号
                if (i == 6) aColumnTextColumnStyle.Width = 0;
                if (i == 7) aColumnTextColumnStyle.Width = 0;
                if (i == 8) aColumnTextColumnStyle.Width = 0;
                if (i == 9) aColumnTextColumnStyle.Width = 0;
                if (i == 10) aColumnTextColumnStyle.Width = 100;
                if (i == 11) aColumnTextColumnStyle.Width = 200;

                ts.GridColumnStyles.Add(aColumnTextColumnStyle);
            }
            this.ds_bill.TableStyles.Add(ts);
        }

         */ 
        /*
        private void btnCanel_Click(object sender, EventArgs e)
        {
            string taskNo = this.txtTaskNo.Text.Trim();
            if (string.IsNullOrEmpty(taskNo))
            {
                Message.Alarm("提示", "任务号为空");
                return;
            }            

            DataRow[] dr = taskTable.Select("任务号='" + taskNo + "'");
            if (dr.Length <= 0)
            {
                Message.Alarm("提示", "采集任务号不在任务列表中，请核实");
                return;
            }

            string roomCode = roomLabel.Text.Trim();
            if (string.IsNullOrEmpty(roomCode))
            {
                Message.Alarm("提示", "库房号不能为空");
                return;
            }

            string proofNo = this.txtBarcode.Text.Trim();
            if (string.IsNullOrEmpty(proofNo))
            {
                Message.Alarm("提示", "凭证号为空");
                return;
            }
            
            try
            {
                service.CommitRCOutTask(taskNo, User.Instance.UserData.UserId, true);
                Message.Alarm("提示", string.Format("取消任务号：【{0}】的任务成功", taskNo));
                getTaskListbyUser();
            }
            catch (Exception ee)
            {
                Message.Alarm("提示", ee.Message);
            }
        }

        private void btnDtlCanel_Click(object sender, EventArgs e)
        {
            string ProofNo = txtBarcode.Text;
            if (string.IsNullOrEmpty(ProofNo))
            {
                Message.Alarm("提示", "凭证号不能为空,请选择凭证号!");
                return;
            }

            string WorkStation = txtGoodQty.Text;
            if (string.IsNullOrEmpty(WorkStation))
            {
                Message.Alarm("提示", "工位不能为空,请选择工位!");
                return;
            }

            TrayUpDownReceiveItemFrm gdReceiveItem = new TrayUpDownReceiveItemFrm(this.txtBarcode.Text, this.txtGoodQty.Text);
            gdReceiveItem.ShowDialog();
            getTaskListbyUser();
        }

        private void QueryBN_Click(object sender, EventArgs e)
        {
            try
            {
                ds_bill.DataSource = null;
                roomLabel.Text = "";

                string s_ProofNo = txtBarcode.Text.Trim();

                if (string.IsNullOrEmpty(s_ProofNo))
                {
                    Message.Alarm("提示", "凭证号不能为空,请输入凭证号!");
                    return;
                }

                DataSet ds = service.GetOutTaskByProofNo(User.Instance.UserData.UserId, s_ProofNo, "1", "0", "2");
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    //throw new Exception("暂无上架任务"); 
                    return;
                }
                this.txtBarcode.Text = "";
                ds_bill.MouseUp -= taskDataGrid_MouseUp;
                taskTable = ds.Tables[0];
                dv.Table = taskTable;
                ds_bill.DataSource = dv;
                AutoSizeCol(taskTable.Columns.Count);
                ds_bill.MouseUp += taskDataGrid_MouseUp;
                txtBarcode.Focus();
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        private void exceptButton_Click(object sender, EventArgs e)
        {
            try
            {
                string strTaskNo = this.txtTaskNo.Text.Trim();
                if (string.IsNullOrEmpty(strTaskNo)) throw new Exception("任务号为空");
                string proofNo = this.txtBarcode.Text.Trim();
                if (string.IsNullOrEmpty(proofNo)) throw new Exception("凭证号为空");
                string roomCode = roomLabel.Text.Trim();
                if (string.IsNullOrEmpty(roomCode)) throw new Exception("库房号不能为空");
                string WorkStation = txtGoodQty.Text;
                if (string.IsNullOrEmpty(WorkStation)) throw new Exception("工位不能为空");
                string orderNo = string.Empty; 
                string siteFlag = string.Empty;
                string batchFlag = string.Empty;
                string taskNo = string.Empty;
                //CheckProofNo(proofNo, ref siteFlag, ref batchFlag, ref taskNo, ref orderNo);
                CheckTaskNo(strTaskNo, ref siteFlag, ref batchFlag, ref taskNo, ref orderNo);

                ExceptTaskFrm frm = new ExceptTaskFrm(proofNo, strTaskNo, taskId, "立库下架", roomCode,"");
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }
        */
        
    }
}