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
    public partial class DownTaskItemFrm : Form
    {
        public DownTaskItemFrm(string taskNo, string storeRoom, string siteFlag, string batchFlag, string taskComment)
        {
            InitializeComponent();
            this.taskNo = taskNo ;
            this.storeRoom = storeRoom;
            this.siteFlag = siteFlag;
            this.batchFlag = batchFlag;
            this.taskComment = taskComment;
        }

        private string taskNo=string.Empty;
        private string taskComment = string.Empty;
        private string storeRoom = string.Empty;
        private string siteFlag = string.Empty;
        private string batchFlag = string.Empty;

        MiddleService service = new MiddleService();
        Management management = Management.GetSingleton();
        DataTable roomTable = new DataTable();

        private string matCode = string.Empty;
        private string batchNo = string.Empty;
        private string sn = string.Empty; 
        private string storeSite = string.Empty;
        private string matControlFlag = string.Empty;
        private string erpStoreSite = string.Empty;//ERP子库
        private Dictionary<string, List<string>> dicMtlQty = new Dictionary<string, List<string>>();//key: outTaskItemid value: 0:开始采集数  1：本次数量
        private Dictionary<string, decimal> dicInvMtlQty = new Dictionary<string, decimal>();//Key：库位+物料+批次 ||  库位+序列  Value：本次作业数量
        private Dictionary<string, string> dicSeq = new Dictionary<string, string>();
        private MtlCheckMode mtlCheckMode;
        private string erpStoreInv = string.Empty;//库存erp子库
        //private string supplier = string.Empty;
        enum MtlCheckMode
        {
            /// <summary>
            /// 检查物料
            /// </summary>
            Mtl,
            /// <summary>
            /// 物料+批号
            /// </summary>
            MtlBatch,
            /// <summary>
            /// 物料+库位
            /// </summary>
            MtlSite,
            /// <summary>
            /// 物料+批号+库位
            /// </summary>
            MtlBatchSite
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownTaskItemFrm_Load(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "请扫描库位：";
                tbxBarcode.Text = "";
                tbxBarcode.Focus();
                tbxBarcode.SelectAll(); 
                qtyLabel.Text = "";
                 
                //根据任务号获取任务明细
                this.detailListView.Columns.Clear();
                detailListView.Columns.Add("物料号", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("库位", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("任务数", 70, HorizontalAlignment.Left);
                detailListView.Columns.Add("已采数", 70, HorizontalAlignment.Left);
                detailListView.Columns.Add("批号", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("序列号", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("库房", 100, HorizontalAlignment.Left);
                detailListView.Columns.Add("outtaskitemid", 0, HorizontalAlignment.Left);
                detailListView.Columns.Add("ERP子库", 120, HorizontalAlignment.Left);
                DataSet ds = service.GetOutTaskItem(taskNo, taskComment);
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    detailListView.Items.Add(new ListViewItem(
                    new string[] { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString() ,dr[8].ToString() }));
                }

                #region 设定参数
                if (siteFlag=="Y") 
                    siteCheckBox.Checked = true; 
                else 
                    siteCheckBox.Checked = false;

                if (batchFlag == "Y")
                    lotNoCheckBox.Checked = true; 
                else
                    lotNoCheckBox.Checked = false;

                if (siteFlag == "Y" && batchFlag == "Y")
                    mtlCheckMode = MtlCheckMode.MtlBatchSite;
                else if (siteFlag == "Y" && batchFlag != "Y")
                    mtlCheckMode = MtlCheckMode.MtlSite;
                else if (siteFlag != "Y" && batchFlag == "Y")
                    mtlCheckMode = MtlCheckMode.MtlBatch;
                else
                    mtlCheckMode = MtlCheckMode.Mtl;

                tbxBarcode.Focus();
                #endregion
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        } 

        /// <summary>
        /// 扫描事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                this.tbxBarcode.Focus();
                this.tbxBarcode.SelectAll();
            }
        }

        /// <summary>
        /// 重新初始采集 
        /// </summary>
        private void InitializeCollect()
        {
            tbxBarcode.Text = "";
            tbxBarcode.Focus();
            tbxBarcode.SelectAll();

            matCodeLabel.Text = "";
            batchLabel.Text = "";
            serialNoLabel.Text = "";
            qtyLabel.Text = "";
            lbInv.Text = "";

            matCode = string.Empty;
            batchNo = string.Empty;
            sn = string.Empty;

            //storeSite = string.Empty; 
            //siteLabel.Text = ""; 

            if (siteFlag == "Y")
                siteCheckBox.Checked = true;
            else
                siteCheckBox.Checked = false;

            if (batchFlag == "Y")
                lotNoCheckBox.Checked = true;
            else
                lotNoCheckBox.Checked = false; 
        }

        /// <summary>
        /// 扫描条码执行
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
            else if ((management.CheckQuantity(barcode)))
            {
                currStep = Step.Quantity;
            }
            else
            {
                throw new Exception(setMsg("采集内容不合法,"));
            }
            #endregion

            #region 条码校验
            switch (currStep)
            {
                case Step._2DBarcode:

                    #region 物料条码逻辑
                    //if (barcode.IndexOf('*') < 0) throw new Exception("采集内容不合法，请采集二维码");
                    BarcodeContent barcodeContent = service.AnalysisForNewBarcode(barcode);

                     //根据物料属性判断，该物料对应的编码控制    0单件(序列)控制，1批次控制，2无控制
                    int matControl = service.GetMatControl(barcodeContent.MatCode);

                    //校验物料
                    CheckMat(barcodeContent.MatCode);
                   
                    if (matControl == 0)
                    {
                        if (dicSeq.ContainsKey(barcodeContent.SN))
                        {
                            matCode = string.Empty;
                            throw new Exception(string.Format("序列号【{0}】不允许重复采集，请确认", barcodeContent.SN));
                        }

                        batchNo = string.Empty;
                        batchLabel.Text = batchNo;
                        qtyLabel.Text = "1";
                        sn = barcodeContent.SN;     //单件不校验批次控制
                        serialNoLabel.Text = sn;
                    }
                    else if (matControl == 1)
                    {
                        //校验强制批次
                        CheckMtlSite(storeSite, barcodeContent.SN, barcodeContent.MatCode, matControl.ToString());
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
                
                    //检验库存
                    lbInv.Text = "";
                    checkInv(0);
                    #endregion
                    break;
                case Step.Site:         //$KW$+库位号
                    string[] sArry = barcode.Split('$');
                    CheckSite(sArry[2]);
                    //校验强制批次
                    CheckMtlSite(sArry[2], batchNo, matCode, matControlFlag);
                    storeSite = sArry[2];
                  
                    //20160124与刘确认：如果实际采集库位与任务明细中的库位不一致，不去更新任务明细界面的库位信息，去采集明细中查看
                    siteLabel.Text = storeSite;
                    //检验库存
                    lbInv.Text = "";
                    checkInv(0);
                    break;
                case Step.Quantity:
                    if (!sn.Equals(string.Empty))
                    {
                        throw new Exception("已采集序列号无需采集数量，请扫描二维码");
                    }
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

            #region 旧逻辑
            //switch (currStep)
            //{
            //    case Step._2DBarcode:
            //        //if (barcode.IndexOf('*') < 0) throw new Exception("采集内容不合法，请采集二维码");
            //        BarcodeContent barcodeContent = service.AnalysisForNewBarcode(barcode);
            //        matCode = barcodeContent.MatCode;
            //        matCodeLabel.Text = matCode;
            //        CheckMat(matCode);

            //        //根据物料属性判断，该物料对应的编码控制    0单件(序列)控制，1批次控制，2无控制
            //        int matControl = service.GetMatControl(matCode);
            //        matControlFlag = matControl.ToString();
            //        if (matControl == 0)
            //        {
            //            sn = barcodeContent.SN;             //单件不校验批次控制
            //            serialNoLabel.Text = sn;
            //        }
            //        else if (matControl == 1)
            //        {
            //            batchNo = barcodeContent.SN;
            //            batchLabel.Text = batchNo;
            //            CheckBatchNo(batchNo);
            //        }
            //        else
            //        {
            //            throw new Exception("物料" + matCode + "编码控制维护值维护不合法");
            //        }

            //        currStep = Step.Site;
            //        lblMsg.Text = "请采集库位：";

            //        break;
            //    case Step.Site:         //$KW$+库位号
            //        //if (barcode.IndexOf('$') < 0) throw new Exception("库位条码不合法");
            //        string[] sArry = barcode.Split('$');
            //        if (sArry[2] != "KW") throw new Exception("库位条码不合法");
            //        storeSite = sArry[2];
            //        CheckSite(storeSite);
            //        CheckSiteFlag(storeSite);
            //        //20160124与刘确认：如果实际采集库位与任务明细中的库位不一致，不去更新任务明细界面的库位信息，去采集明细中查看
            //        siteLabel.Text = storeSite;
            //        currStep = Step.Quantity;
            //        lblMsg.Text = "请采集数量：";
            //        break;
            //    case Step.Quantity:
            //        //if (!management.CheckQuantity(barcode)) throw new Exception("采集数量不合法");
            //        DealQuantity(barcode, matControlFlag);
            //        //decimal collectQty = Convert.ToDecimal(barcode);
            //        currStep = Step.Site;
            //        lblMsg.Text = "请扫描二维码：";
            //        qtyLabel.Text = barcode;

            //        InitializeCollect();
            //        break;
            //    default:
            //        break;
            //}
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
            else if (serialNoLabel.Text.Trim().Equals("") && batchLabel.Text.Trim().Equals(""))//条码为空 采集条码
            {
                return string.Format("{0}请扫描二维码", msg);
            }
            else if (!serialNoLabel.Text.Trim().Equals(""))//序列不为空  扫描还是二维码
            {
                return string.Format("{0}", msg);
            }
            else if (qtyLabel.Text.Trim().Equals(""))//肯定是批次  如数量为空
            {
                return string.Format("{0}请输入数量", msg);
            }
            else
            {
                return string.Format("{0}", msg);
            }
        }

        /// <summary>
        /// 校验采集物料
        /// </summary>
        /// <param name="mtlCode"></param>
        private void CheckMat(string mtlCode)
        {
            string tmpMat = string.Empty;
            for (int i = 0; i < detailListView.Items.Count; i++)
            {
                tmpMat = detailListView.Items[i].SubItems[0].Text.Trim();

                if (tmpMat.Equals(mtlCode))
                {
                    erpStoreSite = detailListView.Items[i].SubItems[8].Text.Trim();
                    return;
                }
            }
            throw new Exception(string.Format("任务明细中不存在物料【{0}】", mtlCode));
        }

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="strSiteCode"></param>
        /// <param name="strBatch"></param>
        /// <param name="strMtlCode"></param>
        /// <returns></returns>
        private void CheckMtlSite(string strSiteCode, string strBatch, string strMtlCode,string matControl)
        {
            #region 校验
            if (strMtlCode.Equals(string.Empty)) return;
            //如果强制库位 库位为空 校验
            if (siteFlag.Equals("Y"))
            {
                if (string.IsNullOrEmpty(strSiteCode))
                    return;
            }
            //如果强制批号 批号为空 校验
            if (batchFlag.Equals("Y") && matControl == "1")
            {
                if (string.IsNullOrEmpty(strBatch))
                    return;
            }
            #endregion

            //强制库位
            if (siteFlag == "Y")
            {
                #region 强制库位逻辑
                for (int i = 0; i < detailListView.Items.Count; i++)
                {
                    string tmpMat = detailListView.Items[i].SubItems[0].Text.Trim();
                    string tmpBatch = detailListView.Items[i].SubItems[4].Text.Trim();
                    string tmpSite = detailListView.Items[i].SubItems[1].Text.Trim();
                    if (batchFlag == "Y" && matControl == "1")
                    {
                        if (tmpMat == strMtlCode && tmpBatch == strBatch && tmpSite == strSiteCode)
                            return;
                    }
                    else
                    {
                        if (tmpMat == strMtlCode && tmpSite == strSiteCode)
                            return;
                    }
                }
                if (batchFlag == "Y")
                {
                    throw new Exception(string.Format("采集物料【{0}】批次【{1}】库位【{2}】不在任务明细中，请核实", strMtlCode, strBatch, strSiteCode));
                }
                else
                {
                    throw new Exception(string.Format("采集物料【{0}】库位【{1}】不在任务明细中，请核实", strMtlCode, strSiteCode));
                }
                #endregion
            }
            else
            {
                #region 不强制库位
                for (int i = 0; i < detailListView.Items.Count; i++)
                {
                    string tmpMat = detailListView.Items[i].SubItems[0].Text.Trim();
                    string tmpBatch = detailListView.Items[i].SubItems[4].Text.Trim();
                    if (batchFlag == "Y" && matControl == "1")
                    {
                        if (tmpMat == strMtlCode && tmpBatch == strBatch)
                            return;
                    }
                    else
                    {
                        if (tmpMat == strMtlCode)
                            return;
                    }
                }
                if (batchFlag == "Y" && matControl == "1")
                {
                    throw new Exception(string.Format("采集物料【{0}】批次【{1}】不在任务明细中，请核实", strMtlCode, strBatch));
                }
                else
                {
                    throw new Exception(string.Format("采集物料【{0}】不在任务明细中，请核实", strMtlCode));
                }
                #endregion
            }
        }

        /// <summary>
        /// 校验库房下的所有库位中是否包含当前库位
        /// </summary>
        /// <param name="siteCode"></param>
        private void CheckSite(string siteCode)
        {
            DataTable siteTable = service.GetStoreSiteByRoom(storeRoom, siteCode).Tables[0];    //根据库房获取该库房下的所有库位
            DataRow[] siteDr = siteTable.Select("storesiteno= '" + siteCode + "'");
            if (siteDr.Length <= 0) throw new Exception("库房" + storeRoom + "下不存在编号为" + siteCode + "的库位");
            if (siteDr[0]["isfrozen"].ToString() != "0")
            {
                throw new Exception(string.Format("库位【{0}】被锁定或者冻结", siteCode));
            }
        }

        /// <summary>
        /// 回填数量，更新LISTVIEW(结合强制库位强制批次标识)
        /// </summary>
        /// <param name="barcode"></param>
        private void DealQuantity(decimal collectQty, string matFlag)
        {
            #region 变量及校验
            if (string.IsNullOrEmpty(matControlFlag)) throw new Exception("获取物料编码属性失败");
            if (collectQty <= 0) throw new Exception("采集数量必须大于0");
            decimal taskQty = 0;
            decimal tmpQty = 0;
            bool exsitFlag = false;
            if (Convert.ToInt16(matFlag) == 2)  //0单件(序列)控制，1批次控制，2无控制
            {
                throw new Exception("物料【" + matCode + "】编码控制维护值维护不合法");
            }
            #endregion

            #region 校验库存
            string strKey = checkInv(collectQty);
            #endregion

            #region 统计当前物料总扫描数和总计划数
            decimal tatalTaskQty = 0;//当前物料总计划数
            decimal tatalTmpQty = 0;//当前物料总扫描数
            string tmpMat = string.Empty;
            string tmpBatch = string.Empty;
            string tmpSite = string.Empty;
            for (int i = 0; i < detailListView.Items.Count; i++)
            {
                tmpMat = detailListView.Items[i].SubItems[0].Text.Trim();//物料
                if (tmpMat != matCode) continue;//如果物料不是当前输入的物料 继续
                if (Convert.ToInt16(matFlag) == 0)
                {
                    if ((mtlCheckMode == MtlCheckMode.MtlBatchSite) || (mtlCheckMode == MtlCheckMode.MtlSite))
                    {
                        tmpSite = detailListView.Items[i].SubItems[1].Text.Trim();
                        if (tmpSite != storeSite) continue;//如果库位跟当前输入的不一致 继续
                    }
                }
                if (Convert.ToInt16(matFlag) == 1)
                {
                    if (mtlCheckMode == MtlCheckMode.MtlBatch)
                    {
                        tmpBatch = detailListView.Items[i].SubItems[4].Text.Trim();
                        if (tmpBatch != batchNo) continue;//如果物料批次跟当前输入不一致 继续
                    }
                    else if (mtlCheckMode == MtlCheckMode.MtlBatchSite)
                    {
                        tmpBatch = detailListView.Items[i].SubItems[4].Text.Trim();
                        if (tmpBatch != batchNo) continue;//如果物料批次跟当前输入不一致 继续
                        tmpSite = detailListView.Items[i].SubItems[1].Text.Trim();
                        if (tmpSite != storeSite) continue;//如果库位跟当前输入的不一致 继续
                    }
                    else if (mtlCheckMode == MtlCheckMode.MtlSite)
                    {
                        tmpSite = detailListView.Items[i].SubItems[1].Text.Trim();
                        if (tmpSite != storeSite) continue;//如果库位跟当前输入的不一致 继续
                    }
                }

                taskQty = Convert.ToDecimal(detailListView.Items[i].SubItems[2].Text.Trim());
                tmpQty = Convert.ToDecimal(detailListView.Items[i].SubItems[3].Text.Trim());
                tatalTaskQty += taskQty;
                tatalTmpQty += tmpQty;
            }
            #endregion

            #region 校验数量是否足够
            if ((tatalTmpQty + collectQty) > tatalTaskQty)
                throw new Exception(string.Format("本次采集数量【{0}】大于剩余可采集数量【{1}】", collectQty, tatalTaskQty - tatalTmpQty));
            #endregion

            #region 处理逻辑
            decimal decQty = collectQty;
            List<string> ls = new List<string>();
            string outTaskItemid = string.Empty;
            Dictionary<string, List<decimal>> dicMtlOperatin = new Dictionary<string, List<decimal>>();
            for (int i = 0; i < detailListView.Items.Count; i++)
            {
                #region 校验
                if (decQty <= 0) break;
                tmpMat = detailListView.Items[i].SubItems[0].Text.Trim();//物料
                tmpSite = detailListView.Items[i].SubItems[1].Text.Trim();//库位
                taskQty = Convert.ToDecimal(detailListView.Items[i].SubItems[2].Text.Trim()); 
                tmpQty = Convert.ToDecimal(detailListView.Items[i].SubItems[3].Text.Trim());

                if (tmpMat != matCode) continue;//如果物料不是当前输入的物料 继续
                if (taskQty == tmpQty) continue;

                switch (Convert.ToInt16(matFlag))       //0单件(序列)控制，1批次控制
                {
                    case 0:
                        #region 序列处理
                        if ((mtlCheckMode == MtlCheckMode.MtlBatchSite) || (mtlCheckMode == MtlCheckMode.MtlSite))
                        {
                            if (tmpSite != storeSite) continue;//如果库位跟当前输入的不一致 继续
                        }
                        #endregion
                        break;
                    case 1:
                        #region 批次处理
                        if (mtlCheckMode == MtlCheckMode.MtlBatch)
                        {
                            tmpBatch = detailListView.Items[i].SubItems[4].Text.Trim();
                            if (tmpBatch != batchNo) continue;//如果物料批次跟当前输入不一致 继续
                        }
                        else if (mtlCheckMode == MtlCheckMode.MtlBatchSite)
                        {
                            tmpBatch = detailListView.Items[i].SubItems[4].Text.Trim();
                            if (tmpBatch != batchNo) continue;//如果物料批次跟当前输入不一致 继续

                            tmpSite = detailListView.Items[i].SubItems[1].Text.Trim();
                            if (tmpSite != storeSite) continue;//如果库位跟当前输入的不一致 继续
                        }
                        else if (mtlCheckMode == MtlCheckMode.MtlSite)
                        {
                            tmpSite = detailListView.Items[i].SubItems[1].Text.Trim();
                            if (tmpSite != storeSite) continue;//如果库位跟当前输入的不一致 继续
                        }
                        #endregion
                        break;
                }
                #endregion

                #region 计算使用明细的使用量 处理
                outTaskItemid = detailListView.Items[i].SubItems[7].Text.Trim();
                dicMtlOperatin.Add(outTaskItemid, new List<decimal>());
                dicMtlOperatin[outTaskItemid].Add(taskQty);//第一笔存物料计划数
                if (!dicMtlQty.ContainsKey(outTaskItemid))
                {
                    ls = new List<string>();
                    ls.Add(tmpQty.ToString());
                    ls.Add("0");
                    ls.Add(tmpMat);
                    dicMtlQty.Add(outTaskItemid, ls);
                }

                if ((taskQty - tmpQty) >= decQty)//表示足够扣
                {
                    detailListView.Items[i].SubItems[3].Text = Convert.ToString(tmpQty + decQty);
                    dicMtlQty[outTaskItemid][1] = (tmpQty + decQty).ToString();
                    dicMtlOperatin[outTaskItemid].Add(decQty);
                    decQty = 0;
                    exsitFlag = true;
                }
                else
                {
                    decQty = decQty - (taskQty - tmpQty);//本次扫描数量- 计划剩余数量
                    detailListView.Items[i].SubItems[3].Text = taskQty.ToString();
                    dicMtlQty[outTaskItemid][1] = taskQty.ToString();
                    dicMtlOperatin[outTaskItemid].Add(taskQty - tmpQty);
                }
                #endregion
            }

            if (!exsitFlag) throw new Exception("采集物料批号序列号信息匹配任务明细失败");

            if (!string.IsNullOrEmpty(sn) && !dicSeq.ContainsKey(sn))
            {
                dicSeq.Add(sn, sn);
            }

            if (!dicInvMtlQty.ContainsKey(strKey))
            {
                dicInvMtlQty.Add(strKey, collectQty);
            }
            else
            {
                dicInvMtlQty[strKey] += collectQty;
            }
            #endregion

            #region 旧逻辑
            //Dictionary<int,decimal> dic = new  Dictionary<int,decimal>();
            //for (int i = 0; i < detailListView.Items.Count; i++)
            //{
            //    string tmpMat = detailListView.Items[i].SubItems[0].Text.Trim();
            //    string tmpBatch = detailListView.Items[i].SubItems[4].Text.Trim();  
            //    string taskQty = detailListView.Items[i].SubItems[2].Text.Trim();
            //    string tmpQty = detailListView.Items[i].SubItems[3].Text.Trim();
            //    string tmpSite = detailListView.Items[i].SubItems[1].Text.Trim();
            //    //最大可采数量
            //    decimal diffQty = string.IsNullOrEmpty(tmpQty)? Convert.ToDecimal(taskQty) : Convert.ToDecimal(taskQty) - Convert.ToDecimal(tmpQty);
            //    if (diffQty < 0) throw new Exception("第"+Convert.ToString(i+1)+"条记录任务数量小于已采集数");
            //    if (siteFlag == "Y")
            //    {
            //        #region 强制库位
            //        if (batchFlag == "Y")
            //        {
            //            if (matCode == tmpMat && batchNo == tmpBatch && storeSite == tmpSite)       //同时强制批号
            //            {
            //                if (collectQty > diffQty)
            //                {
            //                    detailListView.Items[i].SubItems[3].Text = taskQty.ToString();
            //                    collectQty -= diffQty;
            //                    dic.Add(i,diffQty);
            //                }
            //                else
            //                {
            //                    detailListView.Items[i].SubItems[3].Text =string.IsNullOrEmpty(tmpQty)?collectQty.ToString(): Convert.ToString(Convert.ToDecimal(tmpQty)+collectQty); 
            //                    collectQty = 0; 
            //                    break;
            //                }
            //            }  
            //        }
            //        else
            //        {  
            //            if (matCode == tmpMat && storeSite == tmpSite)       //同时强制库位，批号
            //            {
            //                if (collectQty > diffQty)
            //                {
            //                    detailListView.Items[i].SubItems[3].Text = taskQty.ToString();
            //                    collectQty -= diffQty;
            //                    dic.Add(i,diffQty);
            //                }
            //                else
            //                {
            //                    detailListView.Items[i].SubItems[3].Text = string.IsNullOrEmpty(tmpQty) ? collectQty.ToString() : Convert.ToString(Convert.ToDecimal(tmpQty) + collectQty);
            //                    collectQty = 0;
            //                    break;
            //                }
            //            }
            //        }
            //        #endregion
            //    }
            //    else
            //    {
            //        if (batchFlag == "Y")
            //        {
            //            if (matCode == tmpMat && batchNo == tmpBatch)       //强制批号
            //            {
            //                if (collectQty > diffQty)
            //                {
            //                    detailListView.Items[i].SubItems[3].Text = taskQty.ToString();
            //                    collectQty -= diffQty;
            //                    dic.Add(i,diffQty);
            //                }
            //                else
            //                {
            //                    detailListView.Items[i].SubItems[3].Text = string.IsNullOrEmpty(tmpQty) ? collectQty.ToString() : Convert.ToString(Convert.ToDecimal(tmpQty) + collectQty);
            //                    collectQty = 0;
            //                    break;
            //                }
            //            }  
            //        }
            //        else
            //        {
            //            if (collectQty > diffQty)
            //            {
            //                detailListView.Items[i].SubItems[3].Text = taskQty.ToString();
            //                collectQty -= diffQty;
            //                dic.Add(i,diffQty);
            //            }
            //            else
            //            {
            //                detailListView.Items[i].SubItems[3].Text = string.IsNullOrEmpty(tmpQty) ? collectQty.ToString() : Convert.ToString(Convert.ToDecimal(tmpQty) + collectQty);
            //                collectQty = 0;
            //                break;
            //            }
            //        }
            //    }
            //}

            //if (collectQty == Convert.ToDecimal(qty))
            //{
            //    throw new Exception("采集物料批号、库位信息匹配任务明细失败");
            //}
            //else
            //{
            //    if (collectQty > 0)
            //    {
            //        foreach(KeyValuePair<int, decimal> item in dic)
            //        {
            //            int i = item.Key;
            //            decimal tmpQty = item.Value;
            //            detailListView.Items[i].SubItems[3].Text = Convert.ToString((Convert.ToDecimal(detailListView.Items[i].SubItems[3].Text) - tmpQty)); 
            //        }
            //        throw new Exception("采集数量超出任务明细可分配数量");
            //    }
            //}
            #endregion

            //添加采集记录;对于采集记录的修改操作统一在采集明细中操作 
            DownCollectData.Instance.AddCollectData(matCode, batchNo, sn, collectQty, storeRoom, storeSite, dicMtlOperatin, erpStoreInv);
        }

        /// <summary>
        /// 检验库存
        /// </summary>
        /// <param name="collectQty">当前数量</param>
        /// <returns></returns>
        private string checkInv(decimal collectQty)
        {
            if (string.IsNullOrEmpty(storeSite)) return "";
            if (string.IsNullOrEmpty(matCode)) return "";

            DataTable dtRepertory = service.GetRepertoryByStoresiteNo(storeSite, "").Tables[0];
            #region 校验库存
            DataRow[] drCheck = null;
            string strKey = string.Empty;
            if (matControlFlag.Equals("1"))
            {
                strKey = storeSite + matCode + batchNo;
                drCheck = dtRepertory.Select(string.Format("storesiteno='{0}' and matcode='{1}'   and batchno='{2}' ", storeSite, matCode, batchNo));
                if (drCheck.Length == 0)
                {
                    throw new Exception(string.Format("物料【{0}】批次【{1}】在库位【{2}】不存在，请确认", matCode, batchNo, storeSite));
                }
            }
            else
            {
                strKey = storeSite + matCode + sn;
                drCheck = dtRepertory.Select(string.Format("storesiteno='{0}' and matcode='{1}' and sn='{2}' ", storeSite, matCode, sn));
                if (drCheck.Length == 0)
                {
                    throw new Exception(string.Format("物料【{0}】序列【{1}】在库位【{2}】不存在，请确认", matCode, sn, storeSite));
                }
            }

            if (collectQty == 0)
                lbInv.Text = drCheck[0]["repqty"].ToString();//显示库存数

            //库存ERP子库
            erpStoreInv = drCheck[0]["erp_storeroom"].ToString();
            if (!string.IsNullOrEmpty(erpStoreSite.Trim ()))
            {
                if (!erpStoreInv.Equals(erpStoreSite))
                {
                    throw new Exception(string.Format("当前物料明细指定子库【{0}】与当前库位的物料批次子库【{1}】存在不一致，请确认", erpStoreSite, drCheck[0]["erp_storeroom"].ToString()));
                }
            }

            if (collectQty > 0)
            {
                decimal decRepqty = 0;
                if (dicInvMtlQty.ContainsKey(strKey))
                    decRepqty = dicInvMtlQty[strKey];

                if (Convert.ToDecimal(drCheck[0]["repqty"].ToString()) - decRepqty < collectQty)
                {
                    throw new Exception(string.Format("库位【{0}】物料【{1}】的库存【{2}】小于本次移出库存【{3}】，请确认", storeSite, matCode, (Convert.ToDecimal(drCheck[0]["repqty"].ToString()) - decRepqty), collectQty));
                }
            }

            return strKey;
            #endregion
        }
        
        /// <summary>
        /// 采集明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void collectItemButton_Click(object sender, EventArgs e)
        {
            try
            {
                DownCollectDetailFrm downDetailFrm = new DownCollectDetailFrm();
                downDetailFrm.ShowDialog();
                UpdateListViewItem(downDetailFrm.dicUpdateInfo, downDetailFrm.dicDeleteSeq, downDetailFrm.dicDeleteInv);
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        /// <summary>
        /// 刷新显示明细，主要是更新数量字段
        /// </summary>
        private void UpdateListViewItem(Dictionary<string, string[]> dicUpdateInfo, Dictionary<string, string> dicDeleteSeq, Dictionary<string, decimal> dicDeleteInv)
        {
            for (int i = 0; i < detailListView.Items.Count; i++)
            {
                string outTaskItemid = detailListView.Items[i].SubItems[7].Text.Trim();
                if (dicUpdateInfo.ContainsKey(outTaskItemid))
                {
                    string[] updateInfo = dicUpdateInfo[outTaskItemid];
                    if (updateInfo[0] == string.Empty)
                    {
                        detailListView.Items[i].SubItems[1].Text = updateInfo[1];
                    }
                    else
                    {
                        decimal dec = Convert.ToDecimal(dicMtlQty[outTaskItemid][1]);
                        dicMtlQty[outTaskItemid][1] = (dec - Convert.ToDecimal(updateInfo[0])).ToString();
                        detailListView.Items[i].SubItems[3].Text = dicMtlQty[outTaskItemid][1];
                    }
                }
            }
            //处理删除的
            foreach (string del in dicDeleteSeq.Values)
            {
                if (dicDeleteSeq.ContainsKey(del))
                    dicSeq.Remove(del);
            }
            //处理库存
            foreach (KeyValuePair<string,decimal> inv in dicDeleteInv)
            {
                if (dicInvMtlQty.ContainsKey(inv.Key))
                    dicInvMtlQty[inv.Key] -= inv.Value;
            }
            
            #region 旧逻辑
            //List<Stock> collect = DownCollectData.Instance.Collect;
            //List<int> fullList = new List<int>();       //满数量回填的行数据

            //if (siteFlag == "Y")
            //{
            //    #region 强制库位
            //    if (batchFlag == "Y")
            //    {
            //        foreach (Stock item in collect)
            //        {
            //            string mat = item.MatCode;
            //            string batch = item.BatchNo;
            //            string site = item.StoreSite;
            //            decimal qty = item.CollectQty;
            //            for (int i = 0; i < detailListView.Items.Count; i++)
            //            {
            //                bool fullFlag = false;
            //                foreach (int j in fullList)
            //                {
            //                    if (j == i)
            //                    {
            //                        fullFlag = true;
            //                        break;
            //                    }
            //                }
            //                if (fullFlag) break;

            //                string tmpMat = detailListView.Items[i].SubItems[0].Text.Trim();
            //                string tmpBatchNo = detailListView.Items[i].SubItems[4].Text.Trim();
            //                string tmpTaskQty = detailListView.Items[i].SubItems[2].Text.Trim();        //任务数量
            //                string tmpQty = detailListView.Items[i].SubItems[3].Text.Trim();            //已采数量
            //                string tmpSite = detailListView.Items[i].SubItems[1].Text.Trim();           //采集库位 
            //                decimal tmpDiffQty =string.IsNullOrEmpty(tmpQty)?Convert.ToDecimal(tmpTaskQty):  Convert.ToDecimal(tmpTaskQty) - Convert.ToDecimal(tmpQty);
            //                if (mat == tmpMat && batch == tmpBatchNo && site == tmpSite)
            //                {
            //                    if (qty > tmpDiffQty)        //采集数量大于当前可采集数量
            //                    {
            //                        detailListView.Items[i].SubItems[3].Text = tmpTaskQty;
            //                        qty -= tmpDiffQty;
            //                        fullList.Add(i);
            //                    }
            //                    else
            //                    {
            //                        detailListView.Items[i].SubItems[3].Text = Convert.ToString(Convert.ToDecimal(tmpQty) + qty);
            //                        break;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        foreach (Stock item in collect)
            //        {
            //            string mat = item.MatCode;
            //            string batch = item.BatchNo;
            //            string site = item.StoreSite;
            //            decimal qty = item.CollectQty;
            //            for (int i = 0; i < detailListView.Items.Count; i++)
            //            {
            //                bool fullFlag = false;
            //                foreach (int j in fullList)
            //                {
            //                    if (j == i)
            //                    {
            //                        fullFlag = true;
            //                        break;
            //                    }
            //                }
            //                if (fullFlag) break;

            //                string tmpMat = detailListView.Items[i].SubItems[0].Text.Trim();
            //                string tmpBatchNo = detailListView.Items[i].SubItems[4].Text.Trim();
            //                string tmpTaskQty = detailListView.Items[i].SubItems[2].Text.Trim();        //任务数量
            //                string tmpQty = detailListView.Items[i].SubItems[3].Text.Trim();            //已采数量
            //                string tmpSite = detailListView.Items[i].SubItems[1].Text.Trim();           //采集库位 
            //                decimal tmpDiffQty = string.IsNullOrEmpty(tmpQty) ? Convert.ToDecimal(tmpTaskQty) : Convert.ToDecimal(tmpTaskQty) - Convert.ToDecimal(tmpQty);
            //                if (mat == tmpMat && site == tmpSite)
            //                {
            //                    if (qty > tmpDiffQty)         
            //                    {
            //                        detailListView.Items[i].SubItems[3].Text = tmpTaskQty;
            //                        fullList.Add(i);
            //                        qty -= tmpDiffQty;
            //                    }
            //                    else
            //                    {
            //                        detailListView.Items[i].SubItems[3].Text = Convert.ToString(Convert.ToDecimal(tmpQty) + qty);
            //                        break;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    #endregion
            //}
            //else
            //{
            //    #region 不强制库位
            //    if (batchFlag == "Y")
            //    {
            //        foreach (Stock item in collect)
            //        {
            //            string mat = item.MatCode;
            //            string batch = item.BatchNo;
            //            string site = item.StoreSite;
            //            decimal qty = item.CollectQty;
            //            for (int i = 0; i < detailListView.Items.Count; i++)
            //            {
            //                bool fullFlag = false;
            //                foreach (int j in fullList)
            //                {
            //                    if (j == i)
            //                    {
            //                        fullFlag = true;
            //                        break;
            //                    }
            //                }
            //                if (fullFlag) break;

            //                string tmpMat = detailListView.Items[i].SubItems[0].Text.Trim();
            //                string tmpBatchNo = detailListView.Items[i].SubItems[4].Text.Trim();
            //                string tmpTaskQty = detailListView.Items[i].SubItems[2].Text.Trim();        //任务数量
            //                string tmpQty = detailListView.Items[i].SubItems[3].Text.Trim();            //已采数量
            //                decimal tmpDiffQty = string.IsNullOrEmpty(tmpQty) ? Convert.ToDecimal(tmpTaskQty) : Convert.ToDecimal(tmpTaskQty) - Convert.ToDecimal(tmpQty);
            //                if (mat == tmpMat && batch == tmpBatchNo)
            //                {
            //                    if (qty > tmpDiffQty)        //采集数量大于当前记录的剩余数量
            //                    {
            //                        detailListView.Items[i].SubItems[2].Text = tmpTaskQty;
            //                        fullList.Add(i);
            //                        qty -= tmpDiffQty;
            //                    }
            //                    else
            //                    {
            //                        detailListView.Items[i].SubItems[3].Text = Convert.ToString(Convert.ToDecimal(tmpQty)+qty);
            //                        break;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        foreach (Stock item in collect)
            //        {
            //            string mat = item.MatCode;
            //            string batch = item.BatchNo;
            //            string site = item.StoreSite;
            //            decimal qty = item.CollectQty;
            //            for (int i = 0; i < detailListView.Items.Count; i++)
            //            {
            //                bool fullFlag = false;
            //                foreach (int j in fullList)
            //                {
            //                    if (j == i)
            //                    {
            //                        fullFlag = true;
            //                        break;
            //                    }
            //                }
            //                if (fullFlag) break;

            //                string tmpMat = detailListView.Items[i].SubItems[0].Text.Trim();
            //                string tmpBatchNo = detailListView.Items[i].SubItems[4].Text.Trim();
            //                string tmpTaskQty = detailListView.Items[i].SubItems[2].Text.Trim();        //任务数量
            //                string tmpQty = detailListView.Items[i].SubItems[3].Text.Trim();            //已采数量
            //                string tmpSite = detailListView.Items[i].SubItems[1].Text.Trim();           //采集库位 
            //                decimal tmpDiffQty = string.IsNullOrEmpty(tmpQty) ? Convert.ToDecimal(tmpTaskQty) : Convert.ToDecimal(tmpTaskQty) - Convert.ToDecimal(tmpQty);
            //                if (mat == tmpMat)
            //                {
            //                    if (qty > tmpDiffQty)        //采集数量大于当前记录的剩余数量
            //                    {
            //                        detailListView.Items[i].SubItems[3].Text = tmpTaskQty;
            //                        fullList.Add(i);
            //                        qty -= tmpDiffQty;
            //                    }
            //                    else
            //                    {
            //                        detailListView.Items[i].SubItems[3].Text = Convert.ToString(Convert.ToDecimal(tmpQty) + qty);
            //                        break;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    #endregion
            //}
            #endregion
        }

        /// <summary>
        /// 提交采集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void commitButton_Click(object sender, EventArgs e)
        {
            try
            {
                #region 校验是否有未完成的物料
                if (DownCollectData.Instance.Collect.Count == 0)
                {
                    throw new Exception("本次无采集明细，请确认！");
                }

                string tmpMat = string.Empty;
                decimal taskQty = 0;
                decimal tmpQty = 0;
                string msg = string.Empty;
                string tmpStore = string.Empty;
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
                List<Stock> collectStocks = DownCollectData.Instance.Collect;
                DownShelvesInfo[] downShelvesInfos = new DownShelvesInfo[collectStocks.Count];
                foreach (Stock stock in collectStocks)
                {
                    DownShelvesInfo downShelvesInfo = new DownShelvesInfo();
                    downShelvesInfo.TaskNo = taskNo;
                    downShelvesInfo.MatCode = stock.MatCode;        //物料号
                    downShelvesInfo.BatchNo = stock.BatchNo;        //批号 
                    downShelvesInfo.Sn = stock.Sn;             //序列号   
                    //downShelvesInfo.TaskQty = stock.TaskQty;    //任务数量 
                    downShelvesInfo.CollectQty = stock.CollectQty;      //已采集数量 
                    downShelvesInfo.StoreRoomNo = stock.StoreRoom;
                    downShelvesInfo.StoreSiteNo = stock.StoreSite;
                    downShelvesInfo.OutTaskItemid = stock.InTaskItemid;
                    downShelvesInfo.ErpStore = stock.ErpStore;
                    CheckSite(stock.StoreRoom, stock.StoreSite);
                    downShelvesInfos[i] = downShelvesInfo;
                    i++;
                }

                ItemListInfo[] lsItems = new ItemListInfo[dicMtlQty.Count];
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
                service.CommitDownShelves(downShelvesInfos, User.Instance.UserData.UserId, lsItems);
                //Message.Alarm("成功", "提交成功");
                DownCollectData.Instance.Collect = new List<Stock>();
                this.Close();
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        private void CheckSite(string room, string site)
        {
            if (string.IsNullOrEmpty(room)) throw new Exception("库房不能为空");
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
            if (MessageBox.Show(string.Format("当前采集数量是{0},是否确认关闭？", DownCollectData.Instance.Collect.Count),
              "下线采集", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
              MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }

            DownCollectData.Instance.Collect = new List<Stock>();
            this.Close(); 
        }
          

        enum Step
        {
            _2DBarcode, Site, Quantity
        }
    }
}