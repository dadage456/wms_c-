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
using BizLayer.WebService;

namespace PDA
{
    public partial class UpTaskItemlFrm : Form
    {
        public UpTaskItemlFrm(string taskNo, string roomCode, string taskComment)
        {
            InitializeComponent();
            this.taskNo = taskNo;
            this.taskComment = taskComment;
            storeRoom = roomCode;
        }
        private string taskComment;
        private string taskNo;
        MiddleService service = new MiddleService();
        Management management = Management.GetSingleton();

        string matCode = string.Empty;
        string batchNo = string.Empty;
        string sn = string.Empty;
        string storeRoom = string.Empty;
        string storeSite = string.Empty;
        decimal collectQty = 0;     //采集数量 
        string matControlFlag = string.Empty;
        string supplier = string.Empty;//供应商
        string protype = string.Empty;//判断是否完工入库
        bool booCheck = true;//是否校验批次
        bool booCheckAagentCode = true;//是否校验供应商和子库
        private DataTable dtRepertory = new DataTable();
        Dictionary<string, List<string>> dicMtlQty = new Dictionary<string, List<string>>();//key: intaskitemid value: 0:开始采集数  1：本次数量
        Dictionary<string, string> dicSeq = new Dictionary<string, string>();
        private string erpStoreSite = string.Empty;//ERP子库
        private void UpTsakItemlFrm_Load(object sender, EventArgs e)
        {
            try
            {
                UpCollectData.Instance.Collect = new List<Stock>();
                lblMsg.Text = "请扫描库位：";
                tbxBarcode.Text = "";
                tbxBarcode.Focus();
                tbxBarcode.SelectAll();

                qtyLabel.Text = "";
                 
                //根据任务号获取任务明细
                this.detailListView.Columns.Clear();
                detailListView.Columns.Add("物料号", 120, HorizontalAlignment.Center);
                detailListView.Columns.Add("库位", 120, HorizontalAlignment.Center);
                detailListView.Columns.Add("任务数", 70, HorizontalAlignment.Center);
                detailListView.Columns.Add("已采数", 70, HorizontalAlignment.Center);
                detailListView.Columns.Add("批号", 140, HorizontalAlignment.Center);
                detailListView.Columns.Add("序列号", 100, HorizontalAlignment.Center);
                detailListView.Columns.Add("库房", 100, HorizontalAlignment.Center);
                detailListView.Columns.Add("ERP子库", 90, HorizontalAlignment.Center);
                detailListView.Columns.Add("intaskitemid", 0, HorizontalAlignment.Center);
                detailListView.Columns.Add("parno", 0, HorizontalAlignment.Center);//
                detailListView.Columns.Add("protype", 0, HorizontalAlignment.Center);//
                DataSet ds = service.GetInTaskItem(taskNo,taskComment);
                DataTable dt = ds.Tables[0];
                supplier = string.Empty;
                foreach (DataRow dr in dt.Rows)
                {
                    if (string.IsNullOrEmpty(supplier))
                    {
                        supplier = dr[9].ToString();
                        protype = dr[10].ToString();
                    }
                    detailListView.Items.Add(new ListViewItem(
                    new string[] { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString(), dr[9].ToString(), dr[10].ToString() }));
                }

                if (!protype.Equals("11") && !protype.Equals("13") && !protype.Equals("17") && !protype.Equals("18"))
                {
                    //校验批次
                    booCheck = true;
                }
                else
                {
                    booCheck = false;
                }

            
                if (protype.Equals("9") || protype.Equals("10"))
                {
                    //校验供应商
                    booCheckAagentCode = true;
                }
                else
                {
                    booCheckAagentCode = false;
                }
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        private void tbxBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Return)
                {
                    string barcode = this.tbxBarcode.Text.Trim();
                    if (barcode == "")
                    {
                        this.tbxBarcode.Text = "";
                        this.tbxBarcode.Focus();
                        this.tbxBarcode.SelectAll();
                        return;
                    }

                    PerformingBarcode(barcode);
                    this.tbxBarcode.Text = "";
                    this.tbxBarcode.Focus();
                    this.tbxBarcode.SelectAll();
                }
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
                //InitializeCollect();
                this.tbxBarcode.Focus();
                this.tbxBarcode.SelectAll();
            }
        }

        /// <summary>
        /// 处理采集信息
        /// </summary>
        /// <param name="barcode"></param>
        private void PerformingBarcode(string barcode)
        {
            if (string.IsNullOrEmpty(barcode)) throw new Exception("采集内容不能为空");
           
            #region  判断模式
            Step currStep;
            if (barcode.IndexOf('*') > 0)
            {
                currStep = Step._2DBarcode;
            }
            else if (barcode.StartsWith("$KW$"))
            {
                currStep = Step.Site;
            }
            else if((management.CheckQuantity(barcode)))
            {
                currStep =Step.Quantity;
            }
            else
            {
                throw new Exception(setMsg("采集内容不合法,"));
            }
            #endregion

            #region 处理逻辑
            switch (currStep)
            {
                case Step._2DBarcode:
                    //if (barcode.IndexOf('*') < 0) throw new Exception("采集内容不合法，请采集二维码");
                    BarcodeContent barcodeContent = service.AnalysisForNewBarcode(barcode);
 
                    //根据物料属性判断，该物料对应的编码控制    0单件(序列)控制，1批次控制，2无控制
                    int matControl = service.GetMatControl(barcodeContent.MatCode);
                    CheckMat(barcodeContent.MatCode, matControl.ToString(), barcodeContent.SN);
                    if (booCheckAagentCode && !storeRoom.Equals("XN-BL") && !supplier.Equals(barcodeContent.AagentCode))//卡供应商
                    {
                        throw new Exception(string.Format("凭证号对应的供应商代码【{0}】与当前物料供应商代码【{1}】不一致，请确认", supplier, barcodeContent.AagentCode));
                    }

                    if (matControl == 0)
                    {
                        if (dicSeq.ContainsKey(barcodeContent.SN))
                        {
                            matCode = string.Empty;
                            throw new Exception(string.Format("序列号【{0}】不允许重复采集，请确认", barcodeContent.SN));
                        }
                    
                        batchNo = string.Empty;
                        batchLabel.Text = batchNo;
                        collectQty = 1;
                        qtyLabel.Text = "1";
                        sn = barcodeContent.SN;
                        serialNoLabel.Text = sn;
                    }
                    else if (matControl == 1)
                    {
                        sn = string.Empty;
                        serialNoLabel.Text = sn;
                        batchNo = barcodeContent.SN;
                        batchLabel.Text = batchNo;
                    }
                    else
                    {
                        throw new Exception("物料" + barcodeContent.MatCode + "编码控制维护值维护不合法");
                    }
                    matCode = barcodeContent.MatCode;
                    matCodeLabel.Text = matCode;
                    matControlFlag = matControl.ToString();
                    //currStep = Step.Site;
                    //lblMsg.Text = "请采集库位：";
                    checkInv(0);
                    break;
                case Step.Site:         //$KW$+库位号
                    //if (barcode.IndexOf('$') < 0) throw new Exception("库位条码不合法");
                    string[] sArry = barcode.Split('$');
                    CheckSite(sArry[2]);
                    storeSite = sArry[2];
                    //20160124与刘确认：如果实际采集库位与任务明细中的库位不一致，不去更新任务明细界面的库位信息，去采集明细中查看
                    siteLabel.Text = storeSite;
                    //currStep = Step.Quantity;
                    //lblMsg.Text = "请采集数量：";
                    checkInv(0);
                    break;
                case Step.Quantity:
                    if (!sn.Equals(string.Empty))
                    {
                        throw new Exception("已采集序列号无需采集数量，请扫描二维码");
                    }
                    //if (!management.CheckQuantity(barcode)) throw new Exception("采集数量不合法");
                    collectQty = Convert.ToDecimal(barcode);
                    //currStep = Step.Site;
                    //lblMsg.Text = "请扫描二维码：";
                    qtyLabel.Text = barcode;
                    break;
                default:
                    break;
            }

            string strMsg = setMsg("");
            //表示条码都扫描完毕
            if (strMsg.Trim().Equals(""))
            {
                DealQuantity(Convert.ToDecimal(qtyLabel.Text.Trim()), matControlFlag);
                InitializeCollect();
            }
            lblMsg.Text = setMsg("");
            #endregion
        }

        /// <summary>
        /// 设定提示信息
        /// </summary>
        /// <param name="msg"></param>
        private string setMsg(string msg)
        {
            if (siteLabel.Text.Trim().Equals(""))//库位为空 采集库位
            {
                return string.Format("{0}请采集库位", msg);
            }
            else if (matCodeLabel.Text.Trim().Equals(""))//条码为空 采集条码
            {
                return string.Format("{0}请扫描二维码", msg);
            }
            else if (serialNoLabel.Text.Trim ().Equals ("") && qtyLabel.Text.Trim().Equals(""))//肯定是批次  如数量为空
            {
                return string.Format("{0}请输入数量", msg);
            }
            else
            {
                return string.Format("{0}", msg);
            }
        }

        /// <summary>
        /// 重新初始采集 
        /// </summary>
        private void InitializeCollect()
        {
            //lblMsg.Text = "请扫描二维码：";
            tbxBarcode.Text = "";
            tbxBarcode.Focus();
            tbxBarcode.SelectAll();

            matCodeLabel.Text = "";
            batchLabel.Text = "";
            serialNoLabel.Text = "";
            qtyLabel.Text = "";

            //库位不清掉
            //siteLabel.Text = "";
            //storeSite = string.Empty;
            lbInv.Text = "";

            matCode = string.Empty;
            batchNo = string.Empty;
            sn = string.Empty;


            //currStep = Step._2DBarcode;
        }

        /// <summary>
        /// 回填数量，更新LISTVIEW;添加采集记录集
        /// </summary>
        /// <param name="barcode"></param>
        private void DealQuantity(decimal qty, string matFlag)
        {
            #region 变量及校验@
            if (string.IsNullOrEmpty(matControlFlag)) throw new Exception("获取物料编码属性失败");
            if (qty<=0) throw new Exception("采集数量必须大于0");
            bool exsitFlag = false;
            decimal taskQty = 0;
            decimal tmpQty=0;
            if (Convert.ToInt16(matFlag) == 2)  //0单件(序列)控制，1批次控制，2无控制
            {
                throw new Exception("物料" + matCode + "编码控制维护值维护不合法");
            }
            #endregion

            checkInv(-1);

            #region 统计当前物料总扫描数和总计划数
            decimal tatalTaskQty = 0;//当前物料总计划数
            decimal tatalTmpQty = 0;//当前物料总扫描数
            for (int i = 0; i < detailListView.Items.Count; i++)
            {
                string tmpMat = detailListView.Items[i].SubItems[0].Text.Trim();//物料
                if (tmpMat != matCode) continue;//如果物料不是当前输入的物料 继续
                //if (Convert.ToInt16(matFlag) == 0)//序列管控
                //{
                    //2016 3 11 从洋  序列模式 只校验物料
                    //string tmpSn = detailListView.Items[i].SubItems[5].Text.Trim();//序列
                    //if (tmpSn != sn) continue;//如果物料序列跟当前输入不一致 继续
                //}
                if (Convert.ToInt16(matFlag) == 1 && booCheck)//批次管控
                {
                    string tmpBatch = detailListView.Items[i].SubItems[4].Text.Trim();
                    if (tmpBatch != batchNo) continue;//如果物料批次跟当前输入不一致 继续
                }

                taskQty = Convert.ToDecimal(detailListView.Items[i].SubItems[2].Text.Trim());
                tmpQty = Convert.ToDecimal(detailListView.Items[i].SubItems[3].Text.Trim());
                tatalTaskQty += taskQty;
                tatalTmpQty += tmpQty;
            }
            #endregion

            #region 校验数量是否足够
            if ((tatalTmpQty + qty) > tatalTaskQty)
                throw new Exception(string.Format("本次采集数量【{0}】大于剩余可采集数量【{1}】", qty,tatalTaskQty - tatalTmpQty));
            #endregion

            #region 处理逻辑
            decimal decQty = qty;
            List<string> ls = new List<string>();
            string inTaskItemid = string.Empty;
            Dictionary<string, List<decimal>> dicMtlOperatin = new Dictionary<string, List<decimal>>();
            for (int i = 0; i < detailListView.Items.Count; i++)
            {

                if (decQty <= 0) break;
                string tmpMat = detailListView.Items[i].SubItems[0].Text.Trim();//物料
                if (tmpMat != matCode) continue;//如果物料不是当前输入的物料 继续
                taskQty = Convert.ToDecimal(detailListView.Items[i].SubItems[2].Text.Trim());
                tmpQty = Convert.ToDecimal(detailListView.Items[i].SubItems[3].Text.Trim());
                inTaskItemid = detailListView.Items[i].SubItems[8].Text.Trim();
                if (taskQty == tmpQty) continue;
                if (matFlag.Equals("1"))
                {
                    //booCheck true表示完工入库 不校验批次
                    if (booCheck)
                    {
                        string tmpBatch = detailListView.Items[i].SubItems[4].Text.Trim();
                        if (tmpBatch != batchNo) continue;
                    }
                }

                dicMtlOperatin.Add(inTaskItemid, new List<decimal>());
                dicMtlOperatin[inTaskItemid].Add(taskQty);//第一笔存物料计划数
                if (!dicMtlQty.ContainsKey(inTaskItemid))
                {
                    ls = new List<string>();
                    ls.Add(tmpQty.ToString());
                    ls.Add("0");
                    ls.Add(tmpMat);
                    dicMtlQty.Add(inTaskItemid, ls);
                }

                if ((taskQty - tmpQty) >= decQty)//表示足够扣
                {
                    detailListView.Items[i].SubItems[3].Text = Convert.ToString(tmpQty + decQty);
                    dicMtlQty[inTaskItemid][1] = (tmpQty + decQty).ToString();
                    dicMtlOperatin[inTaskItemid].Add(decQty);
                    decQty = 0;
                    exsitFlag = true;
                }
                else
                {
                    decQty = decQty - (taskQty - tmpQty);//本次扫描数量- 计划剩余数量
                    detailListView.Items[i].SubItems[3].Text = taskQty.ToString();
                    dicMtlQty[inTaskItemid][1] = taskQty.ToString();
                    dicMtlOperatin[inTaskItemid].Add(taskQty - tmpQty);
                }
            }
            #endregion

            #region 旧逻辑
            //for (int i = 0; i < detailListView.Items.Count; i++)
            //{
            //    string tmpMat = detailListView.Items[i].SubItems[0].Text.Trim();
            //    string tmpBatch = detailListView.Items[i].SubItems[4].Text.Trim();
            //    string tmpSn = detailListView.Items[i].SubItems[5].Text.Trim();
            //    switch (Convert.ToInt16(matFlag))       //0单件(序列)控制，1批次控制，2无控制
            //    {
            //        case 0:
            //            if (tmpMat == matCode && tmpSn == sn )
            //            {
            //                taskQty = Convert.ToDecimal(detailListView.Items[i].SubItems[2].Text.Trim());
            //                decimal tmpCollectQty = Convert.ToDecimal(detailListView.Items[i].SubItems[3].Text.Trim());
            //                if ((tmpCollectQty + qty) > taskQty)
            //                    throw new Exception("采集数量累计超出任务数量");
            //                detailListView.Items[i].SubItems[3].Text = Convert.ToString(tmpQty + Convert.ToDecimal(qty));
            //                exsitFlag = true;
            //                break;
            //            }
            //            break;
            //        case 1:
            //            if (tmpMat == matCode && tmpBatch == batchNo)
            //            {
            //                taskQty = Convert.ToDecimal(detailListView.Items[i].SubItems[2].Text.Trim());
            //                decimal tmpCollectQty = Convert.ToDecimal(detailListView.Items[i].SubItems[3].Text.Trim());
            //                if ((tmpCollectQty +qty) > taskQty)
            //                    throw new Exception("采集数量累计超出任务数量");
            //                detailListView.Items[i].SubItems[3].Text = Convert.ToString(tmpCollectQty + Convert.ToDecimal(qty));
            //                exsitFlag = true;
            //                break;
            //            }
            //            break;
            //        case 2:
            //            throw new Exception("物料"+matCode+"编码控制维护值维护不合法");
            //            //if (tmpMat == matCode && tmpBatch == batchNo && tmpSn == sn)
            //            //{
            //            //    taskQty = Convert.ToDecimal(lsvTaskDetails.Items[i].SubItems[3].Text.Trim());
            //            //    string tmpQty = lsvTaskDetails.Items[i].SubItems[4].Text.Trim();
            //            //    decimal tmpCollectQty = string.IsNullOrEmpty(tmpQty) ? 0 : Convert.ToDecimal(tmpQty);
            //            //    if ((tmpCollectQty + Convert.ToDecimal(qty)) > taskQty)
            //            //        throw new Exception("采集数量累计超出任务数量");
            //            //    lsvTaskDetails.Items[i].SubItems[4].Text = Convert.ToString(tmpQty + Convert.ToDecimal(qty));
            //            //    exsitFlag = true;
            //            //    return;
            //            //}
            //            //break;
            //    }
            //}
            #endregion

            if (!exsitFlag) throw new Exception("采集物料批号序列号信息匹配任务明细失败");

            if (!string.IsNullOrEmpty(sn) && !dicSeq.ContainsKey(sn))
            {
                dicSeq.Add(sn, sn);
            }
            //添加采集记录;对于采集记录的修改操作统一在采集明细中操作 
            UpCollectData.Instance.AddCollectData(matCode, batchNo, sn, taskQty, Convert.ToDecimal(qty), storeRoom, storeSite, dicMtlOperatin);
        }

        /// <summary>
        /// 检验库存
        /// </summary>
        /// <param name="collectQty">当前数量</param>
        /// <returns></returns>
        private void checkInv(decimal collectQty)
        {
            if (string.IsNullOrEmpty(matCode) || string.IsNullOrEmpty(storeSite)) return;

            #region 校验库存
            DataTable dtRepertory = service.GetMtlRepertoryByStoresiteNo(storeSite, matCode).Tables[0];

            if (dtRepertory != null && dtRepertory.Rows.Count > 0)
            {
                if (collectQty == 0)
                    lbInv.Text = dtRepertory.Rows[0]["repqty"].ToString();//显示库存数

                //入库类型为9、10时，若库房编码为“XN-BL”则不校验子库  2016 3 30 从洋提出
                if (booCheckAagentCode && storeRoom.Equals("XN-BL"))
                {
                    //预留
                }
                else
                {
                    if (!dtRepertory.Rows[0]["erp_storeroom"].ToString().Equals(erpStoreSite))
                    {
                        throw new Exception(string.Format("当前物料明细指定子库【{0}】与当前库位的物料批次子库【{1}】存在不一致，请确认", erpStoreSite, dtRepertory.Rows[0]["erp_storeroom"].ToString()));
                    }
                }

                //if (!storeRoom.Equals("XN-BL"))
                //{
                //    //booCheckAagentCode为true时 校验供应商  2016.3.31 刘益峰提出 
                //    //if (booCheckAagentCode && !dtRepertory.Rows[0]["parno"].ToString().Equals(supplier))//卡供应商
                //    //{
                //    //    throw new Exception(string.Format("物料对应的供应商代码【{0}】与库位物料供应商代码【{1}】不一致，请确认", supplier, dtRepertory.Rows[0]["parno"].ToString()));
                //    //}
                //}
            }
            else
            {
                if (collectQty == 0)
                    lbInv.Text = "0";//显示库存数
            }
            #endregion
        }
        
        private void CheckSite(string siteCode)
        {
            DataTable siteTable = service.GetStoreSiteByRoom(storeRoom, siteCode).Tables[0];    //根据库房获取该库房下的所有库位
            DataRow[] siteDr = siteTable.Select("storesiteno= '" + siteCode + "'");
            if (siteDr.Length <= 0) throw new Exception("库房" + storeRoom + "下无库位号" + siteCode);

            if (siteDr[0]["isfrozen"].ToString() != "0")
            {
                throw new Exception(string.Format ("库位【{0}】被锁定或者冻结",siteCode));
            }
        }

        private void CheckSn(string barcode)
        {
            for (int i = 0; i < detailListView.Items.Count; i++)
            {
                string tmpSn = detailListView.Items[i].SubItems[5].Text.Trim();
                if (tmpSn.Equals(barcode))
                    return;
            }
            throw new Exception("任务明细中不存在序列号" + barcode);
        }

        private void CheckBatchNo(string barcode)
        {
            for (int i = 0; i < detailListView.Items.Count; i++)
            {
                string tmpBatch = detailListView.Items[i].SubItems[4].Text.Trim();
                if (tmpBatch.Equals(barcode))
                    return;
            }
            throw new Exception("任务明细中不存在批号" + barcode);
        }

        private void CheckMat(string barcode, string matControl, string sn)
        {
            for (int i = 0; i < detailListView.Items.Count; i++)
            {
                string tmpMat = detailListView.Items[i].SubItems[0].Text.Trim();
                if (matControl.Equals("0"))//0单件(序列)控制，1批次控制，2无控制
                {
                    //序列只检查物料
                    //string tmpSn = detailListView.Items[i].SubItems[5].Text.Trim();//序列
                    if (tmpMat.Equals(barcode))// && tmpSn.Equals(sn))
                    {
                        erpStoreSite = detailListView.Items[i].SubItems[7].Text.Trim();
                        return;
                    }
                }
                else if (matControl.Equals("1"))
                {
                    if (booCheck)
                    {
                        string tmpBatch = detailListView.Items[i].SubItems[4].Text.Trim();
                        if (tmpMat.Equals(barcode) && tmpBatch.Equals(sn))
                        {
                            erpStoreSite = detailListView.Items[i].SubItems[7].Text.Trim();
                            return;
                        }
                    }
                    else
                    {
                        if (tmpMat.Equals(barcode))
                        {
                            erpStoreSite = detailListView.Items[i].SubItems[7].Text.Trim();
                            return;
                        }
                    }
                }
            }
            if (matControl.Equals("0"))//0单件(序列)控制，1批次控制，2无控制
            {
                //throw new Exception("任务明细中物料【" + barcode + "】序号【" + sn + "】的物料不存在");
                throw new Exception("任务明细中物料【" + barcode + "】不存在");
            }
            else
            {
                throw new Exception("任务明细中物料【" + barcode + "】批号【" + sn + "】的物料不存在");
            }
        }

        private void collectItemButton_Click(object sender, EventArgs e)
        {
            try
            {
                UpCollectDetailFrm upDetailFrm = new UpCollectDetailFrm();
                upDetailFrm.ShowDialog();
                UpdateListViewItem(upDetailFrm.dicUpdateInfo, upDetailFrm.dicDeleteSeq);
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        /// <summary>
        /// 刷新显示明细，主要是更新数量字段
        /// </summary>
        private void UpdateListViewItem(Dictionary<string, string[]> dicUpdateInfo, Dictionary<string, string> dicDeleteSeq)
        {
            for (int i = 0; i < detailListView.Items.Count; i++)
            {
                string inTaskItemid = detailListView.Items[i].SubItems[8].Text.Trim();
                if (dicUpdateInfo.ContainsKey(inTaskItemid))
                {
                    string[] updateInfo = dicUpdateInfo[inTaskItemid];
                    if (updateInfo[0] == string.Empty)
                    {
                        detailListView.Items[i].SubItems[1].Text = updateInfo[1];
                    }
                    else
                    {
                        decimal dec = Convert.ToDecimal(dicMtlQty[inTaskItemid][1]);
                        dicMtlQty[inTaskItemid][1] = (dec - Convert.ToDecimal(updateInfo[0])).ToString();
                        detailListView.Items[i].SubItems[3].Text = dicMtlQty[inTaskItemid][1];
                        //if (updateInfo[2] != string.Empty && dicSeq.ContainsKey(updateInfo[2]))
                        //    dicSeq.Remove(updateInfo[2]);
                    }
                }

            }

            foreach (string del in dicDeleteSeq.Values)
            {
                if (dicSeq.ContainsKey(del))
                    dicSeq.Remove(del);
            }
        }

        private string GetSumQtyByMat(List<Stock> stocks, string matCode, string batchNo ,string sn)
        {
            decimal sumQty = 0;
            foreach (Stock stock in stocks)
            {
                if (stock.MatCode == matCode && stock.BatchNo == batchNo && stock.Sn == sn )
                    sumQty += stock.CollectQty;
            }
            return sumQty.ToString();
        }

        private void commitButton_Click(object sender, EventArgs e)
        {
            try
            {
                #region 校验是否有未完成的物料
                if (UpCollectData.Instance.Collect.Count == 0)
                {
                    throw new Exception("本次无采集明细，请确认！");
                }

                string tmpMat = string.Empty;
                decimal taskQty = 0;
                decimal tmpQty = 0;
                string msg = string.Empty;
                string tmpStore=string.Empty;
                for (int ii = 0; ii < detailListView.Items.Count; ii++)
                {
                    tmpMat = detailListView.Items[ii].SubItems[0].Text.Trim();//物料
                    tmpStore = detailListView.Items[ii].SubItems[1].Text.Trim();//库位
                    taskQty = Convert.ToDecimal(detailListView.Items[ii].SubItems[2].Text.Trim());
                    tmpQty = Convert.ToDecimal(detailListView.Items[ii].SubItems[3].Text.Trim());
                    if (taskQty != tmpQty)
                    {
                        msg += string.Format("、库位【{2}】物料【{0}】还剩【{1}】未做", tmpMat, (taskQty - tmpQty), tmpStore);
                        break;
                    }
                }
                if (!string.IsNullOrEmpty(msg))
                {
                    msg = msg.Remove(0, 1) + "，请确认是否提交？";
                    if (MessageBox.Show(msg,
                            "上线采集", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                    {
                        return;
                    }
                }

                #endregion

                int i = 0;
                string filter = string.Empty;
                foreach (string value in dicSeq.Values)
                {
                    filter += ",'" + value + "'";
                }
                if (!filter.Equals(string.Empty))
                {
                    filter = filter.Remove(0, 1);
                }
       
                List<Stock> collectStocks = UpCollectData.Instance.Collect;
                UpShelvesInfo[] upShelvesInfos = new UpShelvesInfo[collectStocks.Count];
                foreach (Stock stock in collectStocks)
                {
                    UpShelvesInfo upShelvesInfo = new UpShelvesInfo();
                    upShelvesInfo.TaskNo = taskNo;
                    upShelvesInfo.MatCode = stock.MatCode;        //物料号
                    upShelvesInfo.BatchNo = stock.BatchNo;        //批号 
                    upShelvesInfo.Sn = stock.Sn;             //序列号   
                    upShelvesInfo.TaskQty = stock.TaskQty;    //任务数量 
                    upShelvesInfo.CollectQty = stock.CollectQty;      //已采集数量 
                    upShelvesInfo.StoreRoomNo = stock.StoreRoom;
                    upShelvesInfo.StoreSiteNo = stock.StoreSite;
                    upShelvesInfo.InTaskItemid = stock.InTaskItemid;
                    CheckSite(stock.StoreRoom, stock.StoreSite);
                    upShelvesInfos[i] = upShelvesInfo;
                    i++;
                }

                ItemListInfo[] lsItems=new ItemListInfo [dicMtlQty.Count];
                i = 0;
                foreach (KeyValuePair<string, List<string>> mtl in dicMtlQty)
                {
                    ItemListInfo itemListInfo = new ItemListInfo();
                    itemListInfo.MtlQty = new string[2];
                    itemListInfo.InTaskItemid = mtl.Key;
                    itemListInfo.MtlQty[0] = mtl.Value[0];
                    itemListInfo.MtlQty[1] = mtl.Value[1];
                    itemListInfo.MtlCode = mtl.Value[2];
                    lsItems[i] = itemListInfo;
                    i++;
                }

                service.CommitUpShelves(upShelvesInfos, User.Instance.UserData.UserCode, lsItems, filter);
                //Message.Alarm("成功", "提交成功");

                UpCollectData.Instance.Collect = new List<Stock>();
                this.Close();
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        private void CheckSite(string room, string site)
        {
            if(string.IsNullOrEmpty(room))  throw new Exception("库房不能为空");
            if (string.IsNullOrEmpty(site)) throw new Exception("库位不能为空");

            MiddleService service = new MiddleService();
            DataTable siteTable = service.GetStoreSiteByRoom(room, site).Tables[0];
            DataRow[] siteDr = siteTable.Select("storesiteno= '" + site + "'");
            if (siteDr.Length <= 0) throw new Exception("库房" + room + "下无库位号" + site);

            if (siteDr[0]["isfrozen"].ToString() != "0")
            {
                throw new Exception(string.Format("库位【{0}】被锁定或者冻结", site));
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("当前采集数量是{0},是否确认关闭？", UpCollectData.Instance.Collect.Count),
                          "上线采集", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                          MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }
            UpCollectData.Instance.Collect = new List<Stock>();
            this.Close();
        }

        enum Step
        {
            _2DBarcode, Site, Quantity
        }
    }
}