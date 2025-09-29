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
    public partial class GoodsSignTaskItemFrm : Form
    {
        public GoodsSignTaskItemFrm(string ls_taskNo)
        {
            InitializeComponent();
            this.ls_taskNo = ls_taskNo;
        }
        private string taskComment = string.Empty;
        private string workStation = string.Empty;
        private string taskNo = string.Empty;
        private string taskId = string.Empty;
        private string siteFlag = string.Empty;
        private string finishFlag = string.Empty;
        private string inTaskItemid = string.Empty;

        private string ls_taskNo = string.Empty;

        MiddleService service = new MiddleService();
        Management management = Management.GetSingleton();

        private string QuerySn = string.Empty;
        string matCode = string.Empty;
        string batchNo = string.Empty;
        string sn = string.Empty;
        string pdate = string.Empty;
        string vdays = string.Empty;

        string storeRoom = string.Empty;
        string collectFlg = string.Empty;
        string matFoundFlg = string.Empty;
        string batchFountFlg = string.Empty;
        string storeSite = string.Empty;
        decimal collectQty = 0;     //采集数量 
        string matControlFlag = string.Empty;
        string batchFlag = string.Empty;
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
                lblMsg.Text = "请扫描二维码";
                tbxBarcode.Text = "";
                tbxBarcode.Focus();
                tbxBarcode.SelectAll();

                qtyLabel.Text = "";
                 
                //根据任务号获取任务明细
                this.detailListView.Columns.Clear();
                detailListView.Columns.Add("物料号", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("库位", 0, HorizontalAlignment.Left);
                detailListView.Columns.Add("任务数", 70, HorizontalAlignment.Left);
                detailListView.Columns.Add("已采数", 70, HorizontalAlignment.Left);
                detailListView.Columns.Add("批号", 140, HorizontalAlignment.Left);
                detailListView.Columns.Add("序列号", 100, HorizontalAlignment.Left);
                detailListView.Columns.Add("库房", 100, HorizontalAlignment.Left);
                detailListView.Columns.Add("ERP子库", 90, HorizontalAlignment.Left);
                detailListView.Columns.Add("intaskitemid", 0, HorizontalAlignment.Left);
                detailListView.Columns.Add("parno", 0, HorizontalAlignment.Left);//
                detailListView.Columns.Add("protype", 0, HorizontalAlignment.Left);//
                detailListView.Columns.Add("入库单号", 100, HorizontalAlignment.Left);
                detailListView.Columns.Add("物料名称", 140, HorizontalAlignment.Left);

                List<string> args_nv = new List<string>();
                args_nv.Add("userid"); args_nv.Add(User.Instance.UserData.UserId);
                args_nv.Add("billid"); args_nv.Add(ls_taskNo);
                args_nv.Add("ndispycj"); args_nv.Add("Y");  //已采集完成的物料,不显示
                // 
                SQLHelper s = new SQLHelper();
                DataSet ds = s.WS("PDA_DH_V2_CMX", args_nv.ToArray());
                if (ds == null || s.sqlcode == -1)
                {
                    MessageBox.Show("查询待到货验货信息错误", "提示");
                    return;
                }
                //
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    //throw new Exception("暂无下架任务"); 
                    return;
                }

                //DataSet ds = service.GetInTaskItem(User.Instance.UserData.UserId, taskNo, taskComment, "0", finishFlag, workStation);
                DataTable dt = ds.Tables[0];
                supplier = string.Empty;
                foreach (DataRow dr in dt.Rows)
                {

                    //select pm.matcode 物料号, 0
                    //                                    sts.storesiteno 库位,1
                    //                                    ii.qty 任务数量, 2
                    //                                    nvl(ii.collectedqty,0) 已采集数量, 3
                    //                                    ii.batchno 批号, 4
                    //                                    ii.sn 序列号, 5
                    //                                    str.storeroomno as 库房, 6
                    //                                    ibl.subinventory_code ERP子库, 7
                    //                                    ii.intaskitemid,8
                    //                                    owning_organization_id parno, 9
                    //                                    inf.protype,10
                    //                                    inf.ORDERNO, 11
                    //                                    pm.matname 物料名称,
                    //                                    ii.palletNo,
                    //                                    GetPalletStatusByOutTaskItemId(ii.intaskid,ii.palletno) 托盘状态,
                    //                                    pm.matinnercode

                    detailListView.Items.Add(new ListViewItem( //加载所有的任务信息明细
                     new string[] { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(),  //物料号,库位,任务数,已采数,批号
                        dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString(), dr[9].ToString(), //序列号,库房,ERP子库,intaskitemid,parno
                        dr[10].ToString(), dr[11].ToString(), dr[12].ToString()  //protype, 入库单号, 物料旧编码
                    }));
                }


                    //校验批次
                    booCheck = true;
                    //校验供应商
                    booCheckAagentCode = true;

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
            /*
            else if (barcode.StartsWith("$KW$"))
            {
                currStep = Step.Site;
            }*/
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
                    // 2018-07-12 新旧 二维码格式的处理
                    //if (barcode.IndexOf('*') < 0) throw new Exception("采集内容不合法，请采集二维码");
                    collectFlg = string.Empty;
                    BarcodeContent barcodeContent = service.AnalysisForNewBarcode(barcode); //上架采集
                    string newmarttask ="0";
                    if(barcode.IndexOf("*BN")>=0)
                    {
                       newmarttask = "1";
                    }
                    //根据物料属性判断，该物料对应的编码控制    0单件(序列)控制，1批次控制，2无控制
                    int matControl = service.GetMatControl(barcodeContent.MatCode);
                    //if (booCheckAagentCode && !storeRoom.Equals("XN-BL") && !supplier.Equals(barcodeContent.AagentCode))//卡供应商
                    //{
                    //    throw new Exception(string.Format("凭证号对应的供应商代码【{0}】与当前物料供应商代码【{1}】不一致，请确认", supplier, barcodeContent.AagentCode));
                    //}
                    if (newmarttask == "0")
                    {
                        CheckMat(barcodeContent.MatCode, matControl.ToString(), barcodeContent.SN); //旧模式, SN中存的是批次号或序列号
                        if (matControl == 0)
                        {
                            if (barcodeContent.SN == string.Empty || barcodeContent.SN == null)
                            {
                                throw new Exception("物料" + barcodeContent.MatCode + "序列号不能为空");
                            }

                            if (dicSeq.ContainsKey(barcodeContent.MatCode+"@"+barcodeContent.SN))
                            {
                                matCode = string.Empty;
                                throw new Exception(string.Format("序列号【{0}】不允许重复采集，请确认", barcodeContent.MatCode + "@" + barcodeContent.SN));
                            }

                            batchNo = string.Empty;
                            sn = string.Empty;

                            batchNo = barcodeContent.SN;
                            batchLabel.Text = batchNo;
                            collectQty = 1;
                            qtyLabel.Text = "1";
                            sn = barcodeContent.SN;
                            serialNoLabel.Text = sn;

                            //QuerySn = "N";
                            //for (int i = 0; i < detailListView.Items.Count; i++)
                            //{
                            //    string tmpMat = detailListView.Items[i].SubItems[0].Text.Trim();
                            //    string tmpBatch = detailListView.Items[i].SubItems[5].Text.Trim();
                            //    string tmpSite = detailListView.Items[i].SubItems[1].Text.Trim();

                            //    if (tmpMat == barcodeContent.MatCode && tmpBatch == barcodeContent.SN && tmpSite == storeSite)
                            //        QuerySn = "Y";
                            //}
                            //if (QuerySn == "N")
                            //{
                            //    throw new Exception(string.Format("采集物料【{0}】序列号【{1}】库位【{2}】不在任务明细中，请核实", barcodeContent.MatCode, barcodeContent.SN, storeSite));
                            //}
                        }
                        else if ((matControl == 1) || (matControl == 2))
                        {
                            if (barcodeContent.SN == string.Empty || barcodeContent.SN == null)
                            {
                                throw new Exception("物料" + barcodeContent.MatCode + "批次号不能为空");
                            }

                            sn = string.Empty;
                            batchNo = string.Empty;
                            //serialNoLabel.Text = sn;
                            batchNo = barcodeContent.SN;
                            batchLabel.Text = batchNo;
                        }
                        else
                        {
                            throw new Exception("物料" + barcodeContent.MatCode + "编码控制维护值维护不合法");
                        }
                    
                    }else{


                        CheckMat2(barcodeContent.MatCode, matControl.ToString(), barcodeContent.BatchNo, barcodeContent.SN); //新二维码模式
                        if (matFoundFlg.Equals("1") && batchFountFlg.Equals("0") && !collectFlg.Equals("1"))
                        {
                            break;
                        }
                        
                        if (matControl == 0)
                        {
                            sn = string.Empty;
                            batchNo = string.Empty;

                            batchNo = barcodeContent.BatchNo;
                            batchLabel.Text = batchNo;

                            sn = barcodeContent.SN;
                            serialNoLabel.Text = sn;

                            collectQty = 1;
                            qtyLabel.Text = "1";


                            if (sn == string.Empty || sn == null)
                            {
                                throw new Exception("物料" + barcodeContent.MatCode + "序列号不能为空");
                            }

                            if (batchNo == string.Empty || batchNo == null)
                            {
                                throw new Exception("物料" + barcodeContent.MatCode + "批次号不能为空");
                            }

                            if (dicSeq.ContainsKey(barcodeContent.MatCode + "@" + barcodeContent.SN))
                            {
                                matCode = string.Empty;
                                throw new Exception(string.Format("序列号【{0}】不允许重复采集，请确认", barcodeContent.SN));
                            }
                        }
                        else
                        {
                            batchNo = (barcodeContent.BatchNo == null ? string.Empty : barcodeContent.BatchNo);
                            batchLabel.Text = batchNo;
                            //sn = (barcodeContent.SN == null ? string.Empty : barcodeContent.SN);
                            //serialNoLabel.Text = sn;

                            if (barcodeContent.BatchNo == string.Empty || barcodeContent.BatchNo == null)
                            {
                                throw new Exception("物料" + barcodeContent.MatCode + "批次号不能为空");
                            }

                        }
                    }
                    matCode = barcodeContent.MatCode;
                    matCodeLabel.Text = matCode; 
                    matControlFlag = matControl.ToString();

                    vdaysLabel.Text = barcodeContent.Color;
                    pdateLabel.Text = barcodeContent.SpecificAttribute;
                    
                    pdate = barcodeContent.SpecificAttribute; //生产日期
                    vdays = barcodeContent.Color; //有效期天数

                    //currStep = Step.Site;
                    //lblMsg.Text = "请采集库位：";
                    //QueryTask(matCode, batchNo, sn);
                    //checkInv(0);
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
        /// //根据扫描托盘号/物料号/货位号获取库存明细
        /// </summary>
        /// <param name="barcode">扫描内容</param>
        /// <param name="currStep">类型</param>
        /// <returns></returns>
        private void QueryTask(string barcode, string batchNo, string sn)
        {
            //根据扫描托盘号/物料号/货位号获取库存明细
            decimal repqty = 0; matinnercode.Text = "";
            for (int i = 0; i < detailListView.Items.Count; i++)
            {
                if ((barcode == detailListView.Items[i].SubItems[0].Text.Trim()) && (batchNo == detailListView.Items[i].SubItems[4].Text.Trim()) && (sn == detailListView.Items[i].SubItems[5].Text.Trim()))
                {

                    matinnercode.Text = detailListView.Items[i].SubItems[12].Text.Trim(); //2018-08-05 zxj add 显示物料旧编码
                    //repqty = repqty + Convert.ToDecimal(detailListView.Items[i].SubItems[2].Text.Trim());
           
                } 
            }            

            //lbInv.Text = repqty.ToString();
        }

        /// <summary>
        /// 设定提示信息
        /// </summary>
        /// <param name="msg"></param>
        private string setMsg(string msg)
        {
            
            if (matCodeLabel.Text.Trim().Equals(""))//条码为空 采集条码
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

            matCodeLabel.Text = ""; matinnercode.Text = "";
            batchLabel.Text = "";
            serialNoLabel.Text = "";
            qtyLabel.Text = "";

            //库位不清掉
            //pdateLabel.Text = "";
            //storeSite = string.Empty;
            vdaysLabel.Text = "";
            pdateLabel.Text = "";

            matCode = string.Empty;
            batchNo = string.Empty;
            sn = string.Empty;
            pdate = string.Empty;
            vdays = string.Empty;


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
            decimal tmpNotmatQty = 0;

            int qiangzhicajiFlag = 0;
            //if (Convert.ToInt16(matFlag) == 2)  //0单件(序列)控制，1批次控制，2无控制
            //{
            //    throw new Exception("物料" + matCode + "编码控制维护值维护不合法");
            //}
            #endregion

            //checkInv(-1);

            #region 统计当前物料总扫描数和总计划数

            decimal tatalTaskQty = 0;//当前物料总计划数
            decimal tatalTmpQty = 0;//当前物料总扫描数
            decimal tatalNotmatQty = 0;//当前物料总扫描数
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
                if (((Convert.ToInt16(matFlag) == 1) || (Convert.ToInt16(matFlag) == 2)) && (booCheck) && (Convert.ToInt16(collectFlg) == 0))//批次管控
                {
                    string tmpBatch = detailListView.Items[i].SubItems[4].Text.Trim();
                    if (tmpBatch != batchNo) continue;//如果物料批次跟当前输入不一致 继续
                }
                //20180731--赵保河 开始

                if ((Convert.ToInt16(matFlag) == 0) && (booCheck) && (Convert.ToInt16(collectFlg) == 0))//序列管控
                {
                    string tmpBatch = detailListView.Items[i].SubItems[4].Text.Trim();
                    string tmpSN = detailListView.Items[i].SubItems[5].Text.Trim();
                    if (!(tmpBatch == batchNo && tmpSN == sn)) continue;//如果物料批次跟当前输入不一致 继续
                    
                }

                List<Stock> stocks = UpCollectData.Instance.Collect;
                foreach (Stock stock in stocks)
                {
                    string InTaskItemid = stock.InTaskItemid;
                    string InCollectFlg = stock.CollectFlg;

                    if (InTaskItemid.Equals(detailListView.Items[i].SubItems[8].Text.Trim()) && Convert.ToInt16(InCollectFlg) == 1)
                    {
                        tmpNotmatQty = stock.CollectQty;
                        tatalNotmatQty += tmpNotmatQty;
                    }
                    //Message.Alarm("提示", InTaskItemid);
                }

                //20180731--赵保河 结束
                taskQty = Convert.ToDecimal(detailListView.Items[i].SubItems[2].Text.Trim());
                tmpQty = Convert.ToDecimal(detailListView.Items[i].SubItems[3].Text.Trim());
                tatalTaskQty += taskQty;
                tatalTmpQty += tmpQty;
            }
            #endregion
            #region 校验数量是否足够  //批次序列相符的任务比较
            if ((tatalTmpQty + qty) > tatalTaskQty)
            {
                qiangzhicajiFlag = 1;
                decimal mattatalTaskQty = 0;//当前物料总计划数
                decimal mattatalTmpQty = 0;//当前物料总扫描数
                for (int i = 0; i < detailListView.Items.Count; i++)
                {
                    string tmpMat = detailListView.Items[i].SubItems[0].Text.Trim();//物料
                    if (tmpMat != matCode) continue;//如果物料不是当前输入的物料 继续
    
                    taskQty = Convert.ToDecimal(detailListView.Items[i].SubItems[2].Text.Trim());
                    tmpQty = Convert.ToDecimal(detailListView.Items[i].SubItems[3].Text.Trim());
                    mattatalTaskQty += taskQty;
                    mattatalTmpQty += tmpQty;
                }
                //排除掉当前的任务明细不相符采集记录后再次比较
                if ((mattatalTmpQty + qty) > mattatalTaskQty)
                {
                    throw new Exception(string.Format("本次采集数量【{0}】大于剩余可采集数量【{1}】", qty, mattatalTaskQty - mattatalTmpQty));
                }

                if (((tatalTmpQty - tatalNotmatQty) + qty) > tatalTaskQty)
                {
                    if ((mattatalTmpQty + qty) > mattatalTaskQty)
                    {
                        throw new Exception(string.Format("本次采集数量【{0}】大于剩余可采集数量【{1}】", qty, mattatalTaskQty - mattatalTmpQty));
                    }

                    // qiangzhicajiFlag=0 正常采集 批次序列付采集任务匹配 且不需要拆解已经采集数据
                    // qiangzhicajiFlag=1 正常采集 批次序列付采集任务匹配 需要踢掉已经采集数据里面的采集不批次记录，并将其转到其他任务
                    // qiangzhicajiFlag=2 正常采集 批次序列付采集任务匹配 需要踢掉已经采集数据里面的采集不批次记录，并将其转到其他任务；并且当前匹配任务不满足还需要分摊至其他任务
                    
                    if (DialogResult.No == MessageBox.Show(string.Format("采集的物料【{0}】批号【{1}】在采集任务仅能匹配上【{2}】颗，剩余的【{3}】颗物料批次与其他采集任务不匹配，如果采集则将剩余采集数据分摊至该物料的其他采集任务，是否采集?", matCode, batchNo, (tatalTaskQty - tatalTmpQty + tatalNotmatQty), (qty - (tatalTaskQty - (tatalTmpQty - tatalNotmatQty)))), "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                    {
                        return;

                    }

                }

                
            }

            #endregion

            #region 处理逻辑
            if (qiangzhicajiFlag == 0 && Convert.ToInt16(collectFlg) == 0)
            {
                decimal decQty = qty;
                List<string> ls = new List<string>();

                Dictionary<string, List<decimal>> dicMtlOperatin = new Dictionary<string, List<decimal>>();
                for (int i = 0; i < detailListView.Items.Count; i++)
                {

                    if (decQty <= 0) break;

                    string tmpMat = detailListView.Items[i].SubItems[0].Text.Trim();//物料

                    if (tmpMat != matCode) continue;//如果物料、货位不是当前输入的物料、货位 继续
                    taskQty = Convert.ToDecimal(detailListView.Items[i].SubItems[2].Text.Trim());
                    tmpQty = Convert.ToDecimal(detailListView.Items[i].SubItems[3].Text.Trim());
                    inTaskItemid = detailListView.Items[i].SubItems[8].Text.Trim();

                    if (taskQty == tmpQty) continue;

                    if (matFlag.Equals("1") || matFlag.Equals("2"))
                    {
                        //booCheck true表示完工入库 不校验批次
                        if (booCheck)
                        {
                            string tmpBatch = detailListView.Items[i].SubItems[4].Text.Trim();
                            if ((tmpBatch != batchNo) && (Convert.ToInt16(collectFlg) == 0)) continue;
                        }
                    }

                    if (matFlag.Equals("0"))
                    {
                        //booCheck true表示完工入库 不校验批次
                        if (booCheck)
                        {
                            string tmpBatch = detailListView.Items[i].SubItems[4].Text.Trim();
                            string tmpSn = detailListView.Items[i].SubItems[5].Text.Trim();
                            string aa = "batchNo:" + batchNo + "-sn:" + sn + "-本次采集tmpBatch:" + tmpBatch + "-tmpSn:" + tmpSn;
                            //Message.Alarm("数据:",aa);
                            if (!(tmpBatch == batchNo && tmpSn == sn && (Convert.ToInt16(collectFlg) == 0))) continue;

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

                if (!string.IsNullOrEmpty(sn) && !dicSeq.ContainsKey(matCode + "@" + sn))
                {
                    dicSeq.Add(matCode + "@" + sn, matCode + "@" + sn);
                }
                //添加采集记录;对于采集记录的修改操作统一在采集明细中操作 
                UpCollectData.Instance.AddCollectData(matCode, batchNo, sn, taskQty, Convert.ToDecimal(qty), storeRoom, null, null, dicMtlOperatin, collectFlg, pdate, vdays);
            
            }

            // qiangzhicajiFlag=1 正常采集 批次序列付采集任务匹配 需要踢掉已经采集数据里面的采集不批次记录，并将其转到其他任务
            if (qiangzhicajiFlag == 1 || Convert.ToInt16(collectFlg) == 1)
            {
                MessageBox.Show("第一步");
                decimal decQty = qty;
                List<string> ls = new List<string>();

                Dictionary<string, List<decimal>> dicMtlOperatin = new Dictionary<string, List<decimal>>();

                List<Stock> stocks_cl =  new List<Stock>();
                for (int i = 0; i < detailListView.Items.Count; i++)
                {                 
                  //首先修改任务-不匹配数据从已采集列表删除
                  //将采集任务的已采集数据减去不匹配数据数量
                  //处理当前采集数量
                  //处理删除的不匹配数量




                    if (decQty <= 0) break;
                    string tmpMat = detailListView.Items[i].SubItems[0].Text.Trim();//物料

                    if (tmpMat != matCode) continue;//如果物料、货位不是当前输入的物料、货位 继续
                    taskQty = Convert.ToDecimal(detailListView.Items[i].SubItems[2].Text.Trim());
                    tmpQty = Convert.ToDecimal(detailListView.Items[i].SubItems[3].Text.Trim());
                    inTaskItemid = detailListView.Items[i].SubItems[8].Text.Trim();

                    //if (taskQty == tmpQty) continue;

                    if (matFlag.Equals("1") || matFlag.Equals("2"))
                    {
                        //booCheck true表示完工入库 不校验批次
                        if (booCheck)
                        {
                            string tmpBatch = detailListView.Items[i].SubItems[4].Text.Trim();
                            if ((tmpBatch != batchNo) && (Convert.ToInt16(collectFlg) == 0)) continue;
                        }
                    }

                    if (matFlag.Equals("0"))
                    {
                        //booCheck true表示完工入库 不校验批次
                        if (booCheck)
                        {
                            string tmpBatch = detailListView.Items[i].SubItems[4].Text.Trim();
                            string tmpSn = detailListView.Items[i].SubItems[5].Text.Trim();
                            string aa = "batchNo:" + batchNo + "-sn:" + sn + "-本次采集tmpBatch:" + tmpBatch + "-tmpSn:" + tmpSn;
                            //Message.Alarm("数据:",aa);
                            if (!(tmpBatch == batchNo && tmpSn == sn && (Convert.ToInt16(collectFlg) == 0))) continue;

                        }
                    }
          
                    //检查是否有非匹配采集记录
                   List<Stock> stocks = UpCollectData.Instance.Collect;                   
                   decimal shengyuQty = 0;

                  
                    
                    for (int j = stocks.Count - 1; j >= 0; j--)
                   {
                       Stock stock = stocks[j];
                       string InTaskItemid = stock.InTaskItemid;
                       string InCollectFlg = stock.CollectFlg;

                       if (InTaskItemid.Equals(detailListView.Items[i].SubItems[8].Text.Trim()) && Convert.ToInt16(InCollectFlg) == 1)
                       {
                           stocks_cl.Add(stock);
                           shengyuQty += stock.CollectQty;
                           stocks.RemoveAt(j);
                       }
                   }
                   /*
                   foreach (Stock stock in stocks)
                   {
                       string InTaskItemid = stock.InTaskItemid;
                       string InCollectFlg = stock.CollectFlg;

                       if (InTaskItemid.Equals(detailListView.Items[i].SubItems[8].Text.Trim()) && Convert.ToInt16(InCollectFlg) == 1)
                       {
                           stocks_cl.Add(stock);
                           shengyuQty += stock.CollectQty;
                           stocks.Remove(stoc);
                       }
                   }*/
    

                   tmpQty = tmpQty - shengyuQty;
                   if (taskQty == tmpQty) continue;

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

                if (!exsitFlag && decQty!=qty) throw new Exception("采集物料批号序列号信息匹配任务明细失败");

                if (!string.IsNullOrEmpty(sn) && !dicSeq.ContainsKey(matCode + "@" + sn))
                {
                    dicSeq.Add(matCode + "@" + sn, matCode + "@" + sn);
                }
                //添加采集记录;对于采集记录的修改操作统一在采集明细中操作
                if (decQty<qty)
                {
                    UpCollectData.Instance.AddCollectData(matCode, batchNo, sn, taskQty, Convert.ToDecimal(qty), storeRoom, null, null, dicMtlOperatin, collectFlg, pdate, vdays);
                }

                if (decQty>0)
                {
                    Stock stock2 = new Stock();
                    stock2.MatCode = matCode;
                    stock2.BatchNo = batchNo;
                    stock2.Sn = sn;
                    stock2.CollectQty = decQty;

                    stocks_cl.Add(stock2);
                }
                foreach (Stock stock in stocks_cl)
                {
                    dicMtlOperatin = new Dictionary<string, List<decimal>>();
                    decQty = stock.CollectQty;
                    collectFlg="1";
                    for (int i = 0; i < detailListView.Items.Count; i++)
                    {             
                        
                    if (decQty <= 0) break;
                    string tmpMat = detailListView.Items[i].SubItems[0].Text.Trim();//物料

                    if (tmpMat != matCode) continue;//如果物料、货位不是当前输入的物料、货位 继续
                    taskQty = Convert.ToDecimal(detailListView.Items[i].SubItems[2].Text.Trim());
                    tmpQty = Convert.ToDecimal(detailListView.Items[i].SubItems[3].Text.Trim());
                    inTaskItemid = detailListView.Items[i].SubItems[8].Text.Trim();


                    if (taskQty == tmpQty) continue;

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

                if (!exsitFlag) throw new Exception("采集物料批号序列号信息匹配任务明细失败");

                if (!string.IsNullOrEmpty(stock.Sn) && !dicSeq.ContainsKey(matCode + "@" + sn))
                {
                    dicSeq.Add(matCode + "@" + stock.Sn, matCode + "@" + stock.Sn);
                }
                //添加采集记录;对于采集记录的修改操作统一在采集明细中操作 
                    UpCollectData.Instance.AddCollectData(matCode, stock.BatchNo, stock.Sn, taskQty, Convert.ToDecimal(stock.CollectQty), storeRoom, null, null, dicMtlOperatin, collectFlg, pdate, vdays);                   
              }
          }
            
            QueryTask(matCode, batchNo, sn);
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
                    vdaysLabel.Text = dtRepertory.Rows[0]["repqty"].ToString();//显示库存数

                //入库类型为9、10时，若库房编码为“XN-BL”则不校验子库  2016 3 30 从洋提出
                if (booCheckAagentCode && storeRoom.Equals("XN-BL"))
                {
                    //预留
                }
                else
                {
                    if (!dtRepertory.Rows[0]["erp_storeroom"].ToString().Equals(erpStoreSite))
                    {
                        //throw new Exception(string.Format("当前物料明细指定子库【{0}】与当前库位的物料批次子库【{1}】存在不一致，请确认", erpStoreSite, dtRepertory.Rows[0]["erp_storeroom"].ToString()));
                        throw new Exception("此物料在当前货位存在其他物权属性的库存，请选择其他上架库位");
                    
                    }
                }

                if (!storeRoom.Equals("XN-BL"))
                {
                    //booCheckAagentCode为true时 校验供应商  2016.3.31 刘益峰提出 
                    if (booCheckAagentCode && !dtRepertory.Rows[0]["parno"].ToString().Equals(supplier))//卡供应商
                    {
                        throw new Exception(string.Format("物料对应的拥有方【{0}】与库位物料拥有方【{1}】不一致，请确认", supplier, dtRepertory.Rows[0]["parno"].ToString()));
                    }
                }
            }
            else
            {
                if (collectQty == 0)
                    vdaysLabel.Text = "0";//显示库存数
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

        /// <summary>
        /// 检查物料是否在任务中存在, 为0序列管理的只校验物料码, 其他的看 booCheck flag的设置,比较批次
        /// </summary>
        /// <param name="barcode">商品条码 matcode</param>
        /// <param name="matControl">序列批次管理方式0序列</param>
        /// <param name="sn"></param>
        private void CheckMat(string barcode, string matControl, string sn)
        {
            for (int i = 0; i < detailListView.Items.Count; i++)
            {
                string tmpMat = detailListView.Items[i].SubItems[0].Text.Trim();
                if (matControl.Equals("0"))//0单件(序列)控制，1批次控制，2无控制
                {
                    ////序列只检查物料
                    ////string tmpSn = detailListView.Items[i].SubItems[5].Text.Trim();//序列
                    //if (tmpMat.Equals(barcode))// && tmpSn.Equals(sn))
                    //{
                    //    erpStoreSite = detailListView.Items[i].SubItems[7].Text.Trim();
                    //    return;
                    //}
                    //20180731-赵保河 开始
                    //序列只检查物料
                    if (booCheck)
                    {
                        string tmpSn = detailListView.Items[i].SubItems[5].Text.Trim();//序列
                        string tmpBatch = detailListView.Items[i].SubItems[4].Text.Trim();
                        //if (tmpMat.Equals(barcode) && tmpBatch.Equals(batchNo) && tmpSn.Equals(sn))
                        if (tmpMat.Equals(barcode))
                        {
                            erpStoreSite = detailListView.Items[i].SubItems[7].Text.Trim();
                            storeRoom = detailListView.Items[i].SubItems[6].Text.Trim();
                            return;
                        }
                    }
                    else
                    {
                        if (tmpMat.Equals(barcode))// && tmpSn.Equals(sn))
                        {
                            erpStoreSite = detailListView.Items[i].SubItems[7].Text.Trim();
                            storeRoom = detailListView.Items[i].SubItems[6].Text.Trim();
                            return;
                        }
                    }
                    //20180731-赵保河 结束
                }
                else if (matControl.Equals("1") || matControl.Equals("2"))
                {
                    if (booCheck)
                    {
                        string tmpBatch = detailListView.Items[i].SubItems[4].Text.Trim();
                        if (tmpMat.Equals(barcode) && tmpBatch.Equals(sn))
                        {
                            erpStoreSite = detailListView.Items[i].SubItems[7].Text.Trim();
                            storeRoom = detailListView.Items[i].SubItems[6].Text.Trim();
                            return;
                        }
                    }
                    else
                    {
                        if (tmpMat.Equals(barcode))
                        {
                            erpStoreSite = detailListView.Items[i].SubItems[7].Text.Trim();
                            storeRoom = detailListView.Items[i].SubItems[6].Text.Trim();
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

        private void CheckMat2(string barcode, string matControl, string BatchNo, string Sn)
        {
            matFoundFlg = "0";
            batchFountFlg = "0";

            for (int i = 0; i < detailListView.Items.Count; i++)
            {
                string tmpMat = detailListView.Items[i].SubItems[0].Text.Trim();
                if (matControl.Equals("0"))//0单件(序列)控制，1批次控制，2无控制
                {
                    ////序列只检查物料
                    ////string tmpSn = detailListView.Items[i].SubItems[5].Text.Trim();//序列
                    //if (tmpMat.Equals(barcode))// && tmpSn.Equals(sn))
                    //{
                    //    erpStoreSite = detailListView.Items[i].SubItems[7].Text.Trim();
                    //    return;
                    //}
                    //20180731-赵保河 开始
                    //序列只检查物料
                    //Message.Alarm("物料：类型", matControl);
                    if (booCheck)
                    {
                        if (tmpMat.Equals(barcode))// && tmpSn.Equals(sn))
                        {
                            erpStoreSite = detailListView.Items[i].SubItems[7].Text.Trim();
                            storeRoom = detailListView.Items[i].SubItems[6].Text.Trim();
                            matFoundFlg = "1";
                            break;
                        }
                    }

                    //20180731-赵保河 结束
                }

                else if (matControl.Equals("1") || matControl.Equals("2"))
                {
                    if (booCheck)
                    {
                        if (tmpMat.Equals(barcode))
                        {
                            erpStoreSite = detailListView.Items[i].SubItems[7].Text.Trim();
                            storeRoom = detailListView.Items[i].SubItems[6].Text.Trim();
                            matFoundFlg = "1";
                            break;
                        }
                    }

                }
            }




            
            for (int i = 0; i < detailListView.Items.Count; i++)
            {

                string tmpMat = detailListView.Items[i].SubItems[0].Text.Trim();
                if (matControl.Equals("0"))//0单件(序列)控制，1批次控制，2无控制
                {
                    ////序列只检查物料
                    ////string tmpSn = detailListView.Items[i].SubItems[5].Text.Trim();//序列
                    //if (tmpMat.Equals(barcode))// && tmpSn.Equals(sn))
                    //{
                    //    erpStoreSite = detailListView.Items[i].SubItems[7].Text.Trim();
                    //    return;
                    //}
                    //20180731-赵保河 开始
                    //序列只检查物料
                   //Message.Alarm("物料：类型", matControl);
                    if (booCheck)
                    {
                        string tmpSn = detailListView.Items[i].SubItems[5].Text.Trim();//序列
                        string tmpBatch = detailListView.Items[i].SubItems[4].Text.Trim();
                        //if (tmpMat.Equals(barcode) && tmpBatch.Equals(batchNo) && tmpSn.Equals(sn))
                        if (tmpMat.Equals(barcode) && tmpBatch.Equals(BatchNo) && tmpSn.Equals(Sn))
                        {
                            erpStoreSite = detailListView.Items[i].SubItems[7].Text.Trim();
                            storeRoom = detailListView.Items[i].SubItems[6].Text.Trim();
                            batchFountFlg = "1";
                            break;
                        }
                    }
                    else
                    {
                        if (tmpMat.Equals(barcode))// && tmpSn.Equals(sn))
                        {
                            erpStoreSite = detailListView.Items[i].SubItems[7].Text.Trim();
                            storeRoom = detailListView.Items[i].SubItems[6].Text.Trim();
                            batchFountFlg = "1";
                            break;
                        }
                    }
                    //20180731-赵保河 结束
                }
                else if (matControl.Equals("1") || matControl.Equals("2"))
                {
                    if (booCheck)
                    {
                        string tmpBatch = detailListView.Items[i].SubItems[4].Text.Trim();
                        if (tmpMat.Equals(barcode) && tmpBatch.Equals(BatchNo))
                        {
                            erpStoreSite = detailListView.Items[i].SubItems[7].Text.Trim();
                            storeRoom = detailListView.Items[i].SubItems[6].Text.Trim();
                            batchFountFlg = "1";
                            break;
                        }
                    }
                    else
                    {
                        if (tmpMat.Equals(barcode))
                        {
                            erpStoreSite = detailListView.Items[i].SubItems[7].Text.Trim();
                            storeRoom = detailListView.Items[i].SubItems[6].Text.Trim();
                            batchFountFlg = "1";
                            break;
                        }
                    }
                }
            }



            if (matFoundFlg.Equals("0"))
            {
                throw new Exception("任务明细中物料【" + barcode + "】不存在");
            }

            if (matFoundFlg.Equals("1") && batchFountFlg.Equals("0"))
            {

                if (matControl.Equals("0"))//0单件(序列)控制，1批次控制，2无控制
                {
                    if (DialogResult.Yes == MessageBox.Show("任务明细中物料【" + barcode + "】物料存在，但采集的批号【" + BatchNo + "】序列【" + Sn + "】与任务不匹配，是否采集?", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                    {
                        collectFlg = "1"; //批次序列不匹配下 采集
                        return;                
                    }
                    else {
                        erpStoreSite = string.Empty;
                        storeRoom = string.Empty;
                        return; 
                    }
                }
                else
                {
                    if (DialogResult.Yes == MessageBox.Show("任务明细中物料【" + barcode + "】物料存在，但采集的批号【" + BatchNo + "】与任务不匹配，是否采集?", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                    {
                        collectFlg = "1"; //批次序列不匹配下 采集
                        return;                 
                    }
                    else
                    {
                        erpStoreSite = string.Empty;
                        storeRoom = string.Empty;
                        return; 
                    }
                }
            }

            if (matFoundFlg.Equals("1") && batchFountFlg.Equals("1"))
            {
                collectFlg = "0";  //批次序列匹配下正常采集
            }
            
        }

        private void collectItemButton_Click(object sender, EventArgs e)
        {
            try
            {
                GoodsSignCollectDetailFrm upDetailFrm = new GoodsSignCollectDetailFrm();
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
                    upShelvesInfo.Taskid = stock.Taskid;
                    upShelvesInfo.InTaskItemid = stock.InTaskItemid;
                    upShelvesInfo.MatchingFlg = stock.CollectFlg;
                    upShelvesInfo.Data1 = stock.SupplierNo;
                    upShelvesInfo.Data2 = stock.SupplierNoSN;
                    //CheckSite(stock.StoreRoom, stock.StoreSite);
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

                Message.Alarm("成功", "提交成功");
                service.CommitSignShelves(upShelvesInfos, User.Instance.UserData.UserCode, lsItems, filter);
                

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

        private void exceptButton_Click(object sender, EventArgs e)
        {
            List<Stock> stocks = UpCollectData.Instance.Collect;

            if (stocks.Count > 0)
            {
                MessageBox.Show("采集数据未提交,不允许异常登记！");
                return;
            }

            ExceptTaskFrm frm = new ExceptTaskFrm(taskComment, taskNo, taskId, "平库上架", storeRoom,"");
            frm.ShowDialog();
        }

        private void tbxBarcode_TextChanged(object sender, EventArgs e)
        {

        }
    }
}