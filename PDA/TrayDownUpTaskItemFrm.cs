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
    public partial class TrayDownUpTaskItemFrm : Form
    {
        public TrayDownUpTaskItemFrm(string taskNo, string taskId, string storeRoom, string siteFlag, string batchFlag, string taskComment, string orderNo, string taskFinshFlag, string workStation)
        {
            InitializeComponent();
            this.taskNo = taskNo ;
            this.taskId = taskId;
            this.storeRoom = storeRoom;
            this.siteFlag = siteFlag;
            this.batchFlag = batchFlag;
            this.finishFlag = taskFinshFlag;
            this.taskComment = taskComment;
            this.workStation = workStation;
            this.orderNo = orderNo;
            
        }

        private string taskNo=string.Empty;
        private string taskId = string.Empty;
        private string taskComment = string.Empty;
        private string workStation = string.Empty;
        private string orderNo = string.Empty;
        private string storeRoom = string.Empty;
        private string siteFlag = string.Empty;
        private string batchFlag = string.Empty;
        private string finishFlag = string.Empty;        
        private string matId = string.Empty;
        private string inTaskItemid = string.Empty;

        MiddleService service = new MiddleService();
        Management management = Management.GetSingleton();
        DataTable roomTable = new DataTable();

        private string trayNo = string.Empty;
        private Step currStep;
        private string matCode = string.Empty;
        private string matName = string.Empty;        
        private string QuerySn = string.Empty;
        private string batchNo = string.Empty;
        private string supplier = string.Empty;
        private string protype = string.Empty;
        private string sn = string.Empty;
        private decimal collectQty = 0;
        private decimal trayCapacity = 0;   //剩余容量
        private decimal currentWeight = 0;//当前总承重
        private decimal currentCapacity = 0; //当前总容积 
        private string storeSite = string.Empty;
        private string matControlFlag = string.Empty;
        private string erpStoreSite = string.Empty;//ERP子库
        private string erpRoom = string.Empty;//ERP子库
        private Dictionary<string, List<string>> dicMtlQty = new Dictionary<string, List<string>>();//key: outTaskItemid value: 0:开始采集数  1：本次数量
        private Dictionary<string, decimal> dicInvMtlQty = new Dictionary<string, decimal>();//Key：库位+物料+批次 ||  库位+序列  Value：本次作业数量
        Dictionary<string, string> dicTryNoMtl = new Dictionary<string, string>();//存储托盘物料明细
        Dictionary<string, List<string>> dicMtlWeight = new Dictionary<string, List<string>>();//存储物料的容量 //第一位 承重 第二位容积
        private Dictionary<string, string> dicSeq = new Dictionary<string, string>();
        private Dictionary<string, string> dicPalletNo = new Dictionary<string, string>();
        private string erpStoreInv = string.Empty;//库存erp子库
        bool boCheckMtl = true;//是否检查物料
        decimal mtlCapacity = 0;//物料容量
        decimal mtlWeight = 0;//物料承重

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
        private void TrayDownUpTaskItemFrm_Load(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "请扫描托盘号：";
                proofLabel.Text = orderNo;
                tbxBarcode.Text = "";
                tbxBarcode.Focus();
                tbxBarcode.SelectAll(); 
                qtyLabel.Text = "";

                //根据任务号获取任务明细
                this.QueryListView.Columns.Clear();
                QueryListView.Columns.Add("物料号", 120, HorizontalAlignment.Center);
                QueryListView.Columns.Add("托盘号", 120, HorizontalAlignment.Center);
                QueryListView.Columns.Add("任务数", 70, HorizontalAlignment.Center);
                QueryListView.Columns.Add("已采数", 70, HorizontalAlignment.Center);
                QueryListView.Columns.Add("批号", 140, HorizontalAlignment.Center);
                QueryListView.Columns.Add("序列号", 100, HorizontalAlignment.Center);
                QueryListView.Columns.Add("库房", 100, HorizontalAlignment.Center);
                QueryListView.Columns.Add("ERP子库", 90, HorizontalAlignment.Center);
                QueryListView.Columns.Add("intaskitemid", 0, HorizontalAlignment.Center);
                QueryListView.Columns.Add("parno", 0, HorizontalAlignment.Center);//
                QueryListView.Columns.Add("protype", 0, HorizontalAlignment.Center);//
                QueryListView.Columns.Add("库位", 120, HorizontalAlignment.Center);
                QueryListView.Columns.Add("物料名称", 120, HorizontalAlignment.Left);
                QueryListView.Columns.Add("入库单号", 100, HorizontalAlignment.Center);
                QueryListView.Columns.Add("托盘状态", 120, HorizontalAlignment.Left);

                this.detailListView.Columns.Clear();
                detailListView.Columns.Add("物料号", 120, HorizontalAlignment.Center);
                detailListView.Columns.Add("托盘号", 120, HorizontalAlignment.Center);
                detailListView.Columns.Add("任务数", 70, HorizontalAlignment.Center);
                detailListView.Columns.Add("已采数", 70, HorizontalAlignment.Center);
                detailListView.Columns.Add("批号", 140, HorizontalAlignment.Center);
                detailListView.Columns.Add("序列号", 100, HorizontalAlignment.Center);
                detailListView.Columns.Add("库房", 100, HorizontalAlignment.Center);
                detailListView.Columns.Add("ERP子库", 90, HorizontalAlignment.Center);
                detailListView.Columns.Add("intaskitemid", 0, HorizontalAlignment.Center);
                detailListView.Columns.Add("parno", 0, HorizontalAlignment.Center);//
                detailListView.Columns.Add("protype", 0, HorizontalAlignment.Center);//
                detailListView.Columns.Add("库位", 120, HorizontalAlignment.Center);
                detailListView.Columns.Add("物料名称", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("入库单号", 100, HorizontalAlignment.Center);
                detailListView.Columns.Add("托盘状态", 120, HorizontalAlignment.Left);


                DataSet ds = service.GetInTaskItem(User.Instance.UserData.UserId, taskNo, taskComment, "1", finishFlag, workStation);
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
                    new string[] { dr[0].ToString(), dr[13].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString(), dr[9].ToString(), dr[10].ToString(), dr[1].ToString(), dr[12].ToString(), dr[11].ToString(), dr[14].ToString() }));
                }

                DataSet InOutds = service.GetInOutLocation("1");
                DataTable InOutdt = InOutds.Tables[0];
                INOUTComboBox.DataSource = InOutdt;

                //INOUTComboBox.SelectedValue = "1431";
                INOUTComboBox.SelectedValue = "-1";

                #region 设定参数

                //if (siteFlag=="Y") 
                //    siteCheckBox.Checked = true; 
                //else 
                //    siteCheckBox.Checked = false;

                if (batchFlag == "Y")
                    lotNoCheckBox.Checked = true; 
                else
                    lotNoCheckBox.Checked = false;

                currStep = Step.TrayNo;
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

            //if (siteFlag == "Y")
            //    siteCheckBox.Checked = true;
            //else
            //    siteCheckBox.Checked = false;

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
            if (barcode.IndexOf('*') > 0)
            {
                currStep = Step._2DBarcode;
            }
            //如需要可修改库位放开即可 柏磊
            //else if (barcode.StartsWith("$KW$"))//采集库位信息优先 
            //{
            //    currStep = Step.Site;
            //}
            else if (barcode.StartsWith("$TP$"))//采集托盘信息
            {
                if (trayNo.Equals(string.Empty))
                {
                    currStep = Step.TrayNo;
                }
                else
                {
                    DialogResult r1 = MessageBox.Show("更换托盘之前扫描的数据将清空，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (r1 == DialogResult.Yes)
                    {
                        if (barcode.Substring(4).Equals(trayNo))
                        {
                            throw new Exception("扫描的托盘号与当前装载的一样，请确认");
                        }

                        initialTaryInfo();
                        currStep = Step.TrayNo;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else if ((management.CheckQuantity(barcode)))//数量
            {
                currStep = Step.Quantity;
            }
            else
            {
                throw new Exception(setMsg("采集内容不合法,"));
            }
            #endregion

            #region 处理逻辑
            switch (currStep)
            {
                //托盘
                case Step.TrayNo:
                    decimal decTrayCapacity = CheckTray(barcode.Substring(4));// "" 、 TP 、 000002
                    trayCapacity = decTrayCapacity;
                    //lbTrayCapacity.Text = trayCapacity.ToString();
                    currentCapacity = 0;
                    currentWeight = 0;
                    trayNo = barcode.Substring(4);
                    CheckTrayNo(trayNo);
                    trayLabel.Text = trayNo;
                    siteLabel.Text = "";
                    for (int i = 0; i < detailListView.Items.Count; i++)
                    {
                        string tmpTrayNo = detailListView.Items[i].SubItems[1].Text.Trim();
                        storeSite = detailListView.Items[i].SubItems[11].Text.Trim();
                        if (tmpTrayNo == trayNo)
                        {
                            siteLabel.Text = storeSite;
                            break;
                        }
                    }
                    
                    QueryTask(trayNo);
                    break;
                case Step._2DBarcode:
                    BarcodeContent barcodeContent = service.AnalysisForNewBarcode(barcode);
                    bool ISNEWEWM = false; if (barcode.IndexOf("*BN") >= 0) { ISNEWEWM = true; }
                    decimal decMtlCapacity = 0;
                    decimal decMtlWeight = 0;
                    //根据物料属性判断，该物料对应的编码控制    0单件(序列)控制，1批次控制，2无控制
                    int matControl = service.GetMatControl(barcodeContent.MatCode, out decMtlCapacity, out decMtlWeight);
                    CheckMat(barcodeContent.MatCode, matControl.ToString(), (ISNEWEWM ? barcodeContent.BatchNo : barcodeContent.SN));

                    //if (!supplier.Equals(barcodeContent.AagentCode))
                    //{
                    //    throw new Exception(string.Format("凭证号对应的供应商代码【{0}】与当前物料供应商代码【{1}】不一致，请确认", supplier,barcodeContent.AagentCode));
                    //}

                    if (matControl == 0)
                    {
                        if (dicSeq.ContainsKey(barcodeContent.MatCode+"@"+barcodeContent.SN))
                        {
                            matCode = string.Empty;
                            throw new Exception(string.Format("序列号【{0}】不允许重复采集，请确认", barcodeContent.MatCode + "@" + barcodeContent.SN));
                        }

                        batchNo = ISNEWEWM ? barcodeContent.BatchNo : string.Empty;
                        batchLabel.Text = batchNo;
                        collectQty = 1;
                        qtyLabel.Text = "1";
                        sn = barcodeContent.SN;
                        serialNoLabel.Text = sn;
                    }
                    else if ((matControl == 1) || (matControl == 2))
                    {
                        sn = ISNEWEWM ? barcodeContent.SN : string.Empty; if (sn == null) { sn = string.Empty; }
                        serialNoLabel.Text = sn;
                        batchNo = ISNEWEWM ? barcodeContent.BatchNo : barcodeContent.SN;
                        batchLabel.Text = batchNo;
                    }
                    else
                    {
                        throw new Exception("物料【" + barcodeContent.MatCode + "】编码控制维护值维护不合法");
                    }
                    matControlFlag = matControl.ToString();
                    matCode = barcodeContent.MatCode;
                    matCodeLabel.Text = matCode;
                    mtlCapacity = decMtlCapacity;
                    mtlWeight = decMtlWeight;
                    break;
                case Step.Quantity:
                    if (!sn.Equals(string.Empty))
                    {
                        throw new Exception("已采集序列号无需采集数量，请扫描库位");
                    }
                    collectQty = Convert.ToDecimal(barcode);
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
        /// 校验托盘号
        /// </summary>
        /// <param name="trayNo"></param>
        private decimal CheckTray(string trayNo)
        {
            if (string.IsNullOrEmpty(trayNo)) throw new Exception("托盘号不能为空");
            return service.CheckDownTray(trayNo);
        }

        /// <summary>
        /// 校验物料
        /// </summary>
        /// <param name="barcode"></param>
        /// <param name="matControl"></param>
        /// <param name="sn"></param>
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
                        return;
                    }
                }
                else if (matControl.Equals("1") || matControl.Equals("2"))
                {
                    string tmpBatch = detailListView.Items[i].SubItems[4].Text.Trim();
                    if (tmpMat.Equals(barcode) && tmpBatch.Equals(sn))
                    {
                        return;
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

        /// <summary>
        /// 设定提示信息
        /// </summary>
        /// <param name="msg"></param>
        private string setMsg(string msg)
        {
            if (trayLabel.Text.Trim().Equals(""))//托盘为空 
            {
                return string.Format("{0}请扫描托盘", msg);
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
        /// 校验采集库位
        /// </summary>
        /// <param name="storeSite"></param>
        private void CheckSite(string site)
        {
            if (string.IsNullOrEmpty(site)) throw new Exception("库位号不能为空");
            DataTable siteTable = service.GetStoreSiteByRoom(storeRoom, site).Tables[0];    //根据库房获取该库房下的所有库位
            DataRow[] siteDr = siteTable.Select("storesiteno= '" + site + "'");
            if (siteDr.Length <= 0) throw new Exception("库房" + storeRoom + "下无库位号" + site);
        }

        /// <summary>
        /// 校验采集托盘号
        /// </summary>
        /// <param name="palletNo"></param>
        private void CheckTrayNo(string palletNo)
        {
            string tmpTrayNo = string.Empty;
            for (int i = 0; i < detailListView.Items.Count; i++)
            {
                tmpTrayNo = detailListView.Items[i].SubItems[1].Text.Trim();

                if (tmpTrayNo.Equals(palletNo))
                {
                    return;
                }
            }
            throw new Exception(string.Format("任务明细中不存在托盘号【{0}】", palletNo));
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
                    erpRoom = detailListView.Items[i].SubItems[7].Text.Trim();
                    return;
                }
            }
            throw new Exception(string.Format("任务明细中不存在物料【{0}】", mtlCode));
        }

        
        /// <summary>
        /// 回填数量，更新LISTVIEW(结合强制库位强制批次标识)
        /// </summary>
        /// <param name="barcode"></param>
        private void DealQuantity(decimal qty, string matFlag)
        {
            #region 变量及校验
            if (string.IsNullOrEmpty(matControlFlag)) throw new Exception("获取物料编码属性失败");
            if (qty <= 0) throw new Exception("采集数量必须大于0");

            if (!dicMtlWeight.ContainsKey(matCode))
            {
                dicMtlWeight.Add(matCode, new List<string>());
                dicMtlWeight[matCode].Add(mtlWeight.ToString());
                dicMtlWeight[matCode].Add(mtlCapacity.ToString());
            }

            if (mtlCapacity * qty > trayCapacity)
            {
                throw new Exception(string.Format("物料【{0}】容量大于托盘剩余容量", mtlCapacity * qty));
            }

            bool exsitFlag = false;
            decimal taskQty = 0;
            decimal tmpQty = 0;
            //if (Convert.ToInt16(matFlag) == 2)  //0单件(序列)控制，1批次控制，2无控制
            //{
            //    throw new Exception("物料" + matCode + "编码控制维护值维护不合法");
            //}

            //托盘明细校验
            string strInfo = string.Empty;
            if (boCheckMtl)//表示校验物料
            {
                strInfo = matCode;
            }

            //if (matFlag == "0")//序列管控
            //{
            //    strInfo = strInfo + supplier;//组盘按物料+供应商区分 供应商在之前校验过了
            //}
            if ((matFlag == "1") || (matFlag == "2"))//批次管控
            {
                strInfo = strInfo + batchNo;//组盘按物料+批次区分
            }
            strInfo = strInfo.Trim();
            if (dicTryNoMtl.ContainsKey(trayNo))
            {
                if (!dicTryNoMtl[trayNo].Equals(strInfo) && (dicTryNoMtl[trayNo].IndexOf(matCode, 1) > 0))
                {
                    if (boCheckMtl)//表示校验物料
                    {
                        if (matFlag == "0")//序列管控
                            throw new Exception(string.Format("扫描物料【{0}】与托盘【{1}】物料不一致", matCode, trayNo));
                        else
                            throw new Exception(string.Format("扫描物料【{0}】批号【{1}】与托盘【{2}】物料批号不一致", matCode, batchNo, trayNo));
                    }
                    else
                    {
                        throw new Exception(string.Format("扫描批号【{0}】与托盘【{1}】扫描物料批号不一致", batchNo, trayNo));
                    }
                }
            }

            #endregion

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
                if ((Convert.ToInt16(matFlag) == 1) || (Convert.ToInt16(matFlag) == 2))//批次管控
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
                throw new Exception(string.Format("本次采集数量【{0}】大于剩余可采集数量【{1}】", qty, tatalTaskQty - tatalTmpQty));
            #endregion

            #region 处理逻辑
            decimal decQty = qty;
            List<string> ls = new List<string>();

            Dictionary<string, List<decimal>> dicMtlOperatin = new Dictionary<string, List<decimal>>();
            for (int i = 0; i < detailListView.Items.Count; i++)
            {
                #region  校验
                if (decQty <= 0) break;
                string tmpMat = detailListView.Items[i].SubItems[0].Text.Trim();//物料
                taskQty = Convert.ToDecimal(detailListView.Items[i].SubItems[2].Text.Trim());
                tmpQty = Convert.ToDecimal(detailListView.Items[i].SubItems[3].Text.Trim());

                if (tmpMat != matCode) continue;//如果物料不是当前输入的物料 继续
                if (taskQty == tmpQty) continue;
                if (matFlag.Equals("1") || matFlag.Equals("2"))
                {
                    string tmpBatch = detailListView.Items[i].SubItems[4].Text.Trim();
                    if (tmpBatch != batchNo) continue;
                }
                #endregion

                #region 处理明细 已扫描数
                inTaskItemid = detailListView.Items[i].SubItems[8].Text.Trim();
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
                #endregion
            }
            #endregion

            if (!exsitFlag) throw new Exception("采集物料批号序列号信息匹配任务明细失败");

            trayCapacity -= mtlCapacity * qty;
            currentCapacity += mtlCapacity * qty;//当前容积增加
            currentWeight += mtlWeight * qty;//当前体积增加
            //lbTrayCapacity.Text = trayCapacity.ToString();

            if (!string.IsNullOrEmpty(sn) && !dicSeq.ContainsKey(matCode+"@"+sn))
            {
                dicSeq.Add(matCode + "@" + sn, matCode + "@" + sn);
            }

            if (!dicTryNoMtl.ContainsKey(trayNo))
            {
                dicTryNoMtl.Add(trayNo, strInfo);
            }
            //添加采集记录;对于采集记录的修改操作统一在采集明细中操作 
            BindingTrayCollectData.Instance.AddCollectData(matCode, batchNo, sn, collectQty, storeSite, trayNo, dicMtlOperatin);
            QueryTask(trayNo);
        }

        /// <summary>
        /// 提交数据前校验
        /// </summary>
        /// <param name="checkSite">是否校验库位</param>
        private void beforeCommit(bool checkSite)
        {
            if (trayNo.Equals(string.Empty))
            {
                throw new Exception("托盘数量为空，请确认");
            }

            if (BindingTrayCollectData.Instance.Collect.Count == 0)
            {
                throw new Exception("未采集物料明细，请确认");
            }

            //if (checkSite && storeSite.Equals (string.Empty))
            //{
            //    throw new Exception("库位为空，请确认");
            //}
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
                TrayDownUpCollectDetailFrm detailFrm = new TrayDownUpCollectDetailFrm(storeRoom);
                detailFrm.ShowDialog();
                UpdateListViewItem(detailFrm.dicUpdateInfo, detailFrm.dicDeleteSeq);
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        /// <summary>
        /// 更新组盘采集任务主界面数据
        /// </summary>
        private void UpdateListViewItem(Dictionary<string, string[]> dicUpdateInfo, Dictionary<string, string> dicDeleteSeq)
        {
            string inTaskItemid = string.Empty;
            string mtlCode = string.Empty;
            for (int i = 0; i < detailListView.Items.Count; i++)
            {
                inTaskItemid = detailListView.Items[i].SubItems[8].Text.Trim();
                mtlCode = detailListView.Items[i].SubItems[0].Text.Trim();//物料
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

                        //dicMtlWeight 第 一位 重 第二位容积
                        //减去承重 物料 乘以 移除的数量
                        //托盘容量得加上来
                        currentWeight -= Convert.ToDecimal(dicMtlWeight[mtlCode][0]) * Convert.ToDecimal(updateInfo[0]);
                        currentCapacity -= Convert.ToDecimal(dicMtlWeight[mtlCode][1]) * Convert.ToDecimal(updateInfo[0]);
                        trayCapacity += Convert.ToDecimal(dicMtlWeight[mtlCode][1]) * Convert.ToDecimal(updateInfo[0]);
                        //lbTrayCapacity.Text = trayCapacity.ToString();
                        detailListView.Items[i].SubItems[3].Text = dicMtlQty[inTaskItemid][1];
                        //if (updateInfo[2] != string.Empty && dicSeq.ContainsKey(updateInfo[2]))
                        //    dicSeq.Remove(updateInfo[2]);
                    }
                }
            }

            for (int i = 0; i < QueryListView.Items.Count; i++)
            {
                inTaskItemid = QueryListView.Items[i].SubItems[8].Text.Trim();

                if (dicUpdateInfo.ContainsKey(inTaskItemid))
                {
                    string[] updateInfo = dicUpdateInfo[inTaskItemid];
                    if (updateInfo[0] == string.Empty)
                    {
                        QueryListView.Items[i].SubItems[1].Text = updateInfo[1];
                    }
                    else
                    {
                        QueryListView.Items[i].SubItems[3].Text = dicMtlQty[inTaskItemid][1];
                    }
                }

            }

            foreach (string del in dicDeleteSeq.Values)
            {
                if (dicSeq.ContainsKey(del))
                    dicSeq.Remove(del);
            }

            if (BindingTrayCollectData.Instance.Collect.Count == 0)
            {
                if (dicTryNoMtl.ContainsKey(trayLabel.Text.Trim()))
                    dicTryNoMtl.Remove(trayLabel.Text.Trim());
            }
        }

        private string GetSumQtyByMat(List<BindingTray> trays, string matCode, string batchNo, string sn)
        {
            decimal sumQty = 0;
            foreach (BindingTray tray in trays)
            {
                if (tray.MatCode == matCode && tray.BatchNo == batchNo && tray.Sn == sn)
                    sumQty += tray.CollectQty;
            }
            return sumQty.ToString();
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
                #region 校验
                beforeCommit(true);

                if (trayNo.Equals(string.Empty)) throw new Exception("托盘号为空，请确认");
                if (taskNo.Equals(string.Empty)) throw new Exception("凭证号为空，请确认");

                string startAddr = string.Empty;
                string endAddr = string.Empty;
                string filter = string.Empty;

                foreach (string value in dicSeq.Values)
                {
                    filter += ",'" + value + "'";
                }
                if (!filter.Equals(string.Empty))
                {
                    filter = filter.Remove(0, 1);
                }
                #endregion

                #region 组托盘数据
                BindingTrayInfo[] trayInfos = new BindingTrayInfo[BindingTrayCollectData.Instance.Collect.Count];   //托盘内货物信息
                int i = 0;
                foreach (BindingTray tray in BindingTrayCollectData.Instance.Collect)
                {
                    BindingTrayInfo trayInfo = new BindingTrayInfo();
                    trayInfo.BatchNo = tray.BatchNo;
                    trayInfo.Sn = tray.Sn;
                    trayInfo.MatCode = tray.MatCode;
                    trayInfo.Qty = tray.CollectQty;
                    trayInfo.StoreSiteNo = tray.StoreSite;
                    trayInfo.InTaskItemid = tray.InTaskItemid;
                    trayInfos[i] = trayInfo;
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
                #endregion

                service.CommitUpTray(trayInfos, lsItems, User.Instance.UserData.UserId, taskNo, trayNo, filter, currentWeight, currentCapacity);

                BindingTrayCollectData.Instance.Collect = new List<BindingTray>();
                Message.Alarm("成功", "托盘补料提交成功");
                //this.Close();
                dicMtlQty.Clear();
                
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        

        private void closeButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("当前采集数量是{0},是否确认关闭？", BindingTrayCollectData.Instance.Collect.Count),
                  "托盘补料采集", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                  MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }
            this.Close();
        }
          

        enum Step
        {
            _2DBarcode, Quantity, TrayNo
        }

        private void exceptButton_Click(object sender, EventArgs e)
        {
            List<BindingTray> stocks = BindingTrayCollectData.Instance.Collect;

            if (stocks.Count > 0)
            {
                MessageBox.Show("采集数据未提交,不允许异常登记！");
                return;
            }

            ExceptTaskFrm frm = new ExceptTaskFrm(taskComment, taskNo, taskId, "托盘补料上架", storeRoom, trayNo);
            frm.ShowDialog();
        }
        

        private void INButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("请确认补料盘回库吗？",
                      "补料采集", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                      MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    return;
                }
                if (BindingTrayCollectData.Instance.Collect.Count > 0) throw new Exception("采集数据未提交,不允许补料盘回库！");
                if (taskNo.Equals(string.Empty)) throw new Exception("凭证号为空，请确认");

                if (INOUTComboBox.SelectedIndex == -1) throw new Exception("拣选口位置不能为空");

                string startAddr = INOUTComboBox.SelectedValue.ToString();
                if (startAddr.Equals(string.Empty)) throw new Exception("拣选口位置不能为空");

                string endAddr = string.Empty;
                int j = detailListView.Items.Count - 1;
                for (int i = j; i >= 0; i--)
                {
                    if (detailListView.Items[i].SubItems[1].Text.Trim() == trayNo)
                    {
                        endAddr = detailListView.Items[i].SubItems[11].Text.Trim();
                    }
                }

                service.CommitUpWmsToWcs(User.Instance.UserData.UserId, taskId, taskNo, trayNo, startAddr, endAddr);
                Message.Alarm("成功", "补料盘回库成功,请等待");
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        private void WmsToWcsBN_Click(object sender, EventArgs e)
        {
            if (BindingTrayCollectData.Instance.Collect.Count > 0)
            {
                MessageBox.Show("采集数据未提交,不允许查看指令！");
                return;
            }

            ASWHWmsToWcs frm = new ASWHWmsToWcs(taskComment,taskId,"03");
            frm.ShowDialog();
        }

        private void taskDataGrid_MouseUp(object sender, MouseEventArgs e)
        {

        }


        private void WCSbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("请确认获取补料托盘吗？",
                      "在线补料", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                      MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    return;
                }
                if (BindingTrayCollectData.Instance.Collect.Count > 0) throw new Exception("采集数据未提交,不允许获取新的补料盘！");
                if (taskNo.Equals(string.Empty)) throw new Exception("凭证号为空，请确认");

                if (INOUTComboBox.SelectedIndex == -1) throw new Exception("拣选口位置不能为空");

                string endAddr = INOUTComboBox.SelectedValue.ToString();
                if (endAddr.Equals(string.Empty)) throw new Exception("拣选口位置不能为空");

                if (string.IsNullOrEmpty(PalletNum.Text.Trim())) throw new Exception("获取补料盘数据不能为空！");

                string sourcetrayNo = string.Empty;
                string startAddr = string.Empty;
                string errMessage = string.Empty;
                int j = detailListView.Items.Count - 1;

                string successFlg = string.Empty;
                int palletnum = Convert.ToInt16(PalletNum.Text.Trim());
                for (int i = j; i >= 0; i--)
                {
                    if (palletnum > 0)
                    {
                        startAddr = detailListView.Items[i].SubItems[11].Text.Trim();
                        sourcetrayNo = detailListView.Items[i].SubItems[1].Text.Trim();
                        if (!(dicPalletNo.ContainsKey(sourcetrayNo)))
                        {
                            successFlg = service.CommitDownWmsToWcs(User.Instance.UserData.UserId, taskId, taskNo, sourcetrayNo, startAddr, endAddr, "0");
                            if (successFlg == "N")
                            {
                                if (string.IsNullOrEmpty(errMessage))
                                {
                                    errMessage = sourcetrayNo;
                                }
                                else
                                {
                                    errMessage = errMessage + "、" + sourcetrayNo;
                                }
                            }
                            if (successFlg == "Y")
                            {
                                palletnum = palletnum - 1;
                            }
                            dicPalletNo.Add(sourcetrayNo, sourcetrayNo);
                        }
                    }
                }
                
                if (string.IsNullOrEmpty(errMessage))
                {
                    Message.Alarm("成功", "获取补料盘成功,请等待");
                }
                else
                {
                    errMessage = "来料盘【" + errMessage + "】获取失败，请逐个选择这些托盘，点击【单个补料盘】按钮获取详细错误信息！";
                    Message.Alarm("失败", errMessage);
                }
                
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        private void SingleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("请确认获取补料托盘吗？",
                      "在线补料", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                      MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    return;
                }
                if (BindingTrayCollectData.Instance.Collect.Count > 0) throw new Exception("采集数据未提交,不允许获取新的补料盘！");
                if (taskNo.Equals(string.Empty)) throw new Exception("凭证号为空，请确认");

                if (INOUTComboBox.SelectedIndex == -1) throw new Exception("拣选口位置不能为空");

                string endAddr = INOUTComboBox.SelectedValue.ToString();
                if (endAddr.Equals(string.Empty)) throw new Exception("拣选口位置不能为空");

                string sourcetrayNo = string.Empty;
                string startAddr = string.Empty;
                int j = detailListView.Items.Count - 1;

                for (int i = j; i >= 0; i--)
                {
                    if (detailListView.Items[i].Selected)
                    {
                        startAddr = detailListView.Items[i].SubItems[11].Text.Trim();
                        sourcetrayNo = detailListView.Items[i].SubItems[1].Text.Trim();
                    }
                }
                service.CommitDownWmsToWcs(User.Instance.UserData.UserId, taskId, taskNo, sourcetrayNo, startAddr, endAddr,"1");

                Message.Alarm("成功", "获取补料盘成功,请等待");
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        /// <summary>
        /// 重新扫描托盘时初始化界面
        /// </summary>
        private void initialTaryInfo()
        {
            if (dicTryNoMtl.Count == 0) return;

            string itemId = string.Empty;
            for (int i = 0; i < detailListView.Items.Count; i++)
            {
                itemId = detailListView.Items[i].SubItems[8].Text.Trim();
                if (dicMtlQty.ContainsKey(itemId))
                {
                    detailListView.Items[i].SubItems[3].Text = dicMtlQty[itemId][0];
                }
            }

            BindingTrayCollectData.Instance.Collect = new List<BindingTray>();
            dicMtlQty = new Dictionary<string, List<string>>();//key: intaskitemid value: 0:开始采集数  1：本次数量
            dicTryNoMtl = new Dictionary<string, string>();//存储托盘物料明细
            tbxBarcode.Text = "";
            tbxBarcode.Focus();
            matCodeLabel.Text = "";
            batchLabel.Text = "";
            serialNoLabel.Text = "";
            qtyLabel.Text = "";
            matCode = string.Empty;
            batchNo = string.Empty;
            sn = string.Empty;
            lblMsg.Text = setMsg("");
        }

        /// <summary>
        /// //根据扫描托盘号/物料号/货位号获取库存明细
        /// </summary>
        /// <param name="barcode">扫描内容</param>
        /// <param name="currStep">类型</param>
        /// <returns></returns>
        private void QueryTask(string barcode)
        {
            //根据扫描托盘号/物料号/货位号获取库存明细
            this.QueryListView.Items.Clear();
            this.QueryListView.Columns.Clear();
            //根据任务号获取任务明细
            QueryListView.Columns.Add("物料号", 120, HorizontalAlignment.Center);
            QueryListView.Columns.Add("托盘号", 120, HorizontalAlignment.Center);
            QueryListView.Columns.Add("任务数", 70, HorizontalAlignment.Center);
            QueryListView.Columns.Add("已采数", 70, HorizontalAlignment.Center);
            QueryListView.Columns.Add("批号", 140, HorizontalAlignment.Center);
            QueryListView.Columns.Add("序列号", 100, HorizontalAlignment.Center);
            QueryListView.Columns.Add("库房", 100, HorizontalAlignment.Center);
            QueryListView.Columns.Add("ERP子库", 90, HorizontalAlignment.Center);
            QueryListView.Columns.Add("intaskitemid", 0, HorizontalAlignment.Center);
            QueryListView.Columns.Add("parno", 0, HorizontalAlignment.Center);//
            QueryListView.Columns.Add("protype", 0, HorizontalAlignment.Center);//
            QueryListView.Columns.Add("库位", 120, HorizontalAlignment.Center);
            QueryListView.Columns.Add("物料名称", 120, HorizontalAlignment.Left);
            QueryListView.Columns.Add("入库单号", 100, HorizontalAlignment.Center);
            QueryListView.Columns.Add("托盘状态", 120, HorizontalAlignment.Left);

            for (int i = 0; i < detailListView.Items.Count; i++)
            {
                if (barcode == detailListView.Items[i].SubItems[1].Text.Trim())
                {
                    QueryListView.Items.Add(new ListViewItem(
                new string[] { detailListView.Items[i].SubItems[0].Text.Trim(), 
                               detailListView.Items[i].SubItems[1].Text.Trim(),  
                               detailListView.Items[i].SubItems[2].Text.Trim(),  
                               detailListView.Items[i].SubItems[3].Text.Trim(),  
                               detailListView.Items[i].SubItems[4].Text.Trim(),  
                               detailListView.Items[i].SubItems[5].Text.Trim(),  
                               detailListView.Items[i].SubItems[6].Text.Trim(),  
                               detailListView.Items[i].SubItems[7].Text.Trim(),  
                               detailListView.Items[i].SubItems[8].Text.Trim(),  
                               detailListView.Items[i].SubItems[9].Text.Trim(),  
                               detailListView.Items[i].SubItems[10].Text.Trim(),  
                               detailListView.Items[i].SubItems[11].Text.Trim(),  
                               detailListView.Items[i].SubItems[12].Text.Trim(),  
                               detailListView.Items[i].SubItems[13].Text.Trim(),
                               detailListView.Items[i].SubItems[14].Text.Trim()}));

                }
            }
        }

        private void QueryPalletNoItemBN_Click(object sender, EventArgs e)
        {
            List<BindingTray> stocks = BindingTrayCollectData.Instance.Collect;

            if (stocks.Count > 0)
            {
                MessageBox.Show("采集数据未提交,不允许查看提交数据！");
                return;
            }

            //trayNo = "TP000010";测试

            if (trayNo.Equals(string.Empty))
            {
                MessageBox.Show("托盘号为空,不允许查看提交数据！");
                return;
            }

            ASWHPalletItem frm = new ASWHPalletItem(taskComment, taskId, trayNo, User.Instance.UserData.UserId);
            frm.ShowDialog();
        }


    }
}