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
    public partial class TrayDownTaskItemFrm : Form
    {
        public TrayDownTaskItemFrm(string taskNo, string taskId, string storeRoom, string siteFlag, string batchFlag, string taskComment, string workStation)
        {
            InitializeComponent();
            this.taskNo = taskNo ;
            this.taskId = taskId;
            this.storeRoom = storeRoom;
            this.siteFlag = siteFlag;
            this.batchFlag = batchFlag;
            this.taskComment = taskComment;
            this.workStation = workStation;
            
        }

        private string taskNo=string.Empty;
        private string taskId = string.Empty;        
        private string taskComment = string.Empty;
        private string workStation = string.Empty;
        private string storeRoom = string.Empty;
        private string siteFlag = string.Empty;
        private string batchFlag = string.Empty;
        private string matSendControl = string.Empty;
        private string roomMatControl = string.Empty;
        private string matId = string.Empty;

        MiddleService service = new MiddleService();
        Management management = Management.GetSingleton();
        DataTable roomTable = new DataTable();

        private string matCode = string.Empty;
        private string matName = string.Empty;
        private string QuerySn = string.Empty;
        private string batchNo = string.Empty;
        private string sn = string.Empty; 
        private string storeSite = string.Empty;
        private string matControlFlag = string.Empty;
        private string erpRoom = string.Empty;//ERP子库
        private string trayNo = string.Empty;
        private decimal trayCapacity = 0;   //剩余容量
        private decimal currentWeight = 0;//当前总承重
        private decimal currentCapacity = 0; //当前总容积
        private decimal mtlCapacity = 0;//物料容量
        private decimal mtlWeight = 0;//物料承重
        private Dictionary<string, List<string>> dicMtlQty = new Dictionary<string, List<string>>();//key: outTaskItemid value: 0:开始采集数  1：本次数量
        private Dictionary<string, string> dicTryNoMtl = new Dictionary<string, string>();//存储托盘物料明细
        private Dictionary<string, List<string>> dicMtlWeight = new Dictionary<string, List<string>>();//存储物料的容量 //第一位 承重 第二位容积
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
                siteFlag = "Y";
                batchFlag = "Y";
                workStationlabel.Text = workStation;

                //根据任务号获取任务明细
               // this.QueryListView.Columns.Clear();
                //QueryListView.Columns.Add("物料号", 120, HorizontalAlignment.Left);
                //QueryListView.Columns.Add("库位", 120, HorizontalAlignment.Left);
                //QueryListView.Columns.Add("任务数", 70, HorizontalAlignment.Left);
                //QueryListView.Columns.Add("已采数", 70, HorizontalAlignment.Left);
                //QueryListView.Columns.Add("库存数", 70, HorizontalAlignment.Left);
                //QueryListView.Columns.Add("批号", 120, HorizontalAlignment.Left);
                //QueryListView.Columns.Add("序列号", 120, HorizontalAlignment.Left);
               // QueryListView.Columns.Add("库房", 100, HorizontalAlignment.Left);
               // QueryListView.Columns.Add("outtaskitemid", 0, HorizontalAlignment.Left);
               // QueryListViewColumns.Add("ERP子库", 120, HorizontalAlignment.Left);
               // QueryListView.Columns.Add("出库单号", 100, HorizontalAlignment.Left);
               // QueryListView.Columns.Add("物料名称", 200, HorizontalAlignment.Left);
                 
                //根据任务号获取任务明细
                this.detailListView.Columns.Clear();
                detailListView.Columns.Add("物料号", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("库位", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("任务数", 70, HorizontalAlignment.Left);
                detailListView.Columns.Add("已采数", 70, HorizontalAlignment.Left);
                detailListView.Columns.Add("库存数", 70, HorizontalAlignment.Left);
                detailListView.Columns.Add("批号", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("序列号", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("库房", 100, HorizontalAlignment.Left);
                detailListView.Columns.Add("outtaskitemid", 0, HorizontalAlignment.Left);
                detailListView.Columns.Add("ERP子库", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("出库单号", 100, HorizontalAlignment.Left);
                detailListView.Columns.Add("物料名称", 200, HorizontalAlignment.Left);

                DataSet ds = service.GetOutTaskItem(User.Instance.UserData.UserId, taskNo, taskComment, "0", "0", workStation);
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    detailListView.Items.Add(new ListViewItem(
                    new string[] { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), " ",dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString(), dr[11].ToString(), dr[9].ToString() }));
                }

                #region 设定参数

                roomMatControl = service.GetRoomMatControl(taskId);

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
            if (string.IsNullOrEmpty(barcode)) throw new Exception("采集凭证号不能为空");
         
            #region  判断模式
            Step currStep;
            if (barcode.IndexOf('*') > 0) //扫入的是物流信息 'MC'||m.matcode||'*DN*DV*SN'||ti.hintbatchno||'*MF*AG'||g.parno||'*DC*'
            {
                currStep = Step._2DBarcode; //物料 + 批次号 + 供应商
            }
            else if (barcode.StartsWith("$TP$"))//采集托盘信息 如:TP$TP003482表示托盘号: TP003482
            {
                currStep = Step.TrayNo;
            }
            else if (barcode.StartsWith("$KW$"))
            {
                currStep = Step.Site; //如 扫入的是 $KW$C1PR120402 表示库位/仓位C1PR120402 
            }
            else if ((management.CheckQuantity(barcode))) //是否输入的是数值
            {
                currStep = Step.Quantity;
            }
            else
            {
                throw new Exception(setMsg("采集凭证号不合法,"));
            }
            #endregion

            #region 条码校验
            switch (currStep)
            {
                case Step._2DBarcode:

                    #region 物料条码逻辑 
                    //barcode = 'MC'||m.matcode||'*DN*DV*SN'||ti.hintbatchno||'*MF*AG'||g.parno||'*DC*' //物料+批号+供应商
                    //if (barcode.IndexOf('*') < 0) throw new Exception("采集内容不合法，请采集二维码");
                    BarcodeContent barcodeContent = service.AnalysisForNewBarcode(barcode);
                    bool ISNEWEWM = false; if (barcode.IndexOf("*BN") >= 0) { ISNEWEWM = true; }
                     //根据物料属性判断，该物料对应的编码控制    0单件(序列)控制，1批次控制，2无控制                    
                    decimal decMtlCapacity = 0;
                    decimal decMtlWeight = 0;
                    int matControl = service.GetMatControl(barcodeContent.MatCode, out decMtlCapacity, out decMtlWeight);

                    matSendControl = service.GetMatSendControl(barcodeContent.MatCode);

                    if (matControl == 0)
                    {
                        if (dicSeq.ContainsKey(barcodeContent.MatCode+"@"+barcodeContent.SN))
                        {
                            matCode = string.Empty;
                            throw new Exception(string.Format("序列号【{0}】不允许重复采集，请确认", barcodeContent.MatCode + "@" + barcodeContent.SN));
                        }

                        batchNo = ISNEWEWM ? barcodeContent.BatchNo : string.Empty;
                        batchLabel.Text = batchNo;
                        qtyLabel.Text = "1";
                        sn = barcodeContent.SN;     //单件不校验批次控制
                        serialNoLabel.Text = sn;

                        QuerySn = "N";
                        List<Stock> stocks = DownCollectData.Instance.Collect;
                        foreach (Stock stock in stocks)
                        {
                            if (stock.Sn == barcodeContent.SN)
                            {
                                QuerySn = "Y";
                            }
                        }

                        if (QuerySn == "Y")
                        {
                            throw new Exception(string.Format("采集物料【{0}】序列号【{1}】库位【{2}】已经采集,不允许重复采集!", barcodeContent.MatCode, barcodeContent.SN, storeSite));
                        }

                        //QuerySn = "N";
                        //for (int i = 0; i < detailListView.Items.Count; i++)
                        //{
                        //    string tmpMat = detailListView.Items[i].SubItems[0].Text.Trim();
                        //    string tmpBatch = detailListView.Items[i].SubItems[6].Text.Trim();
                        //    string tmpSite = detailListView.Items[i].SubItems[1].Text.Trim();
                            
                        //    if (tmpMat == barcodeContent.MatCode && tmpBatch == barcodeContent.SN && tmpSite == storeSite)
                        //        QuerySn = "Y";                            
                        //}
                        //if (QuerySn == "N")
                        //{
                        //    throw new Exception(string.Format("采集物料【{0}】序列号【{1}】库位【{2}】不在任务明细中，请核实", barcodeContent.MatCode, barcodeContent.SN, storeSite));
                        //}                        
                    }
                    else if ((matControl == 1)||(matControl == 2))
                    {
                        if ((matSendControl == "0" && roomMatControl == "0")||(roomMatControl == "1"))
                        {
                            //校验物料
                            CheckMat(barcodeContent.MatCode);

                            //校验强制批次
                            CheckMtlSite(storeSite, (ISNEWEWM ? barcodeContent.BatchNo : barcodeContent.SN), barcodeContent.MatCode, matControl.ToString());
                        }
                        sn = ISNEWEWM ? barcodeContent.SN : string.Empty; if (sn == null) { sn = string.Empty; }
                        serialNoLabel.Text = sn;
                        batchNo = ISNEWEWM ? barcodeContent.BatchNo : barcodeContent.SN;
                        batchLabel.Text = batchNo;
                    }
                    else 
                    {
                        throw new Exception("物料" + barcodeContent.MatCode + "编码控制维护值维护不合法");
                    }

                    matCode = barcodeContent.MatCode;
                    matCodeLabel.Text = matCode;
                    matControlFlag = matControl.ToString();
                    mtlCapacity = decMtlCapacity;
                    mtlWeight = decMtlWeight;
                    //检验库存
                    lbInv.Text = "";
                    checkInv(0);
                    #endregion
                    break;
                case Step.Site:         //$KW$+库位号
                    string[] sArry = barcode.Split('$'); //库位,仓位信息
                    CheckSite(sArry[2]);

                    if ((matSendControl == "0" && roomMatControl == "0")||(roomMatControl == "1"))
                    {
                        //校验强制批次
                        CheckMtlSite(sArry[2], batchNo, matCode, matControlFlag);
                    }
                    storeSite = sArry[2];
                  
                    //20160124与刘确认：如果实际采集库位与任务明细中的库位不一致，不去更新任务明细界面的库位信息，去采集明细中查看
                    siteLabel.Text = storeSite;
                    //检验库存
                    lbInv.Text = "";
                    checkInv(0);
                    break;
                //托盘
                case Step.TrayNo: //TP$TP003482 ==> TP$+托盘号的值, 故用substring(4)来取值
                    decimal decTrayCapacity = CheckTray(barcode.Substring(4));// "" 、 TP 、 000002
                    trayCapacity = decTrayCapacity;
                    lbTrayCapacity.Text = trayCapacity.ToString();
                    currentCapacity = 0;
                    currentWeight = 0;
                    trayNo = barcode.Substring(4);
                    trayLabel.Text = trayNo; //托盘信息设置到界面上
                    CheckBindingTrayByTaskId(trayNo); //检验托盘是否可用
                    break;
                case Step.Quantity:
                    if (!sn.Equals(string.Empty))
                    {//按批次管理的数量已经默认为1了,也不需要修改
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
                DealQuantity(Convert.ToDecimal(qtyLabel.Text.Trim()), matControlFlag); //采集数据 累加 到缓冲区中
                InitializeCollect(); //重新初始化界面
            }
            else if (lbTrayCapacity.Text != null && lbTrayCapacity.Text.Length > 0 && siteLabel.Text != null && siteLabel.Text.Length > 0 && matCodeLabel.Text != null && matCodeLabel.Text.Length > 0)
            {
                //应在此处再调用一次检查,看看对应的托盘或者物料是否可用, 其实最好应该在数数量之前判断,而不是输数量之后判断
                //保存数据库,采用调程序包的方式
                int pii = 0, pinx = 0; SQLHelper s = new SQLHelper(); List<string> args_nv = new List<string>();
                args_nv.Add("tpnocur"); args_nv.Add(trayNo); pinx = 0; //本次采集用的托盘号
                Dictionary<string, List<decimal>> dicMtlOperatin = new Dictionary<string, List<decimal>>(); //记录 <taskitemid, [计划数,采集数]> 的数组, 
                string matFlag = matControlFlag; 
                for (int i = 0; i < detailListView.Items.Count; i++)
                {
                    #region 校验是否可组盘,当前代表的taskitemid
                    string tmpMat = detailListView.Items[i].SubItems[0].Text.Trim();//物料
                    string tmpSite = detailListView.Items[i].SubItems[1].Text.Trim();//库位
                    decimal taskQty = Convert.ToDecimal(detailListView.Items[i].SubItems[2].Text.Trim());
                    decimal tmpQty = Convert.ToDecimal(detailListView.Items[i].SubItems[3].Text.Trim());
                    if (((tmpMat != matCode) || (tmpSite != storeSite)) && (matFlag != "0") && ((matSendControl == "0" && roomMatControl == "0") || (roomMatControl == "1"))) continue;//如果物料不是当前输入的物料 继续
                    if ((tmpMat != matCode) && (matFlag == "0") && ((matSendControl == "0" && roomMatControl == "0") || (roomMatControl == "1"))) continue;//如果物料不是当前输入的物料 继续
                    if (tmpMat != matCode) continue; //
                    if (taskQty == tmpQty) continue;
                    #endregion

                    #region 计算使用明细的使用量 处理
                    string outTaskItemid = detailListView.Items[i].SubItems[8].Text.Trim();
                    dicMtlOperatin.Add(outTaskItemid, new List<decimal>());
                    #endregion
                }
                foreach (string taskitemid in dicMtlOperatin.Keys)
                {
                    args_nv.Add("taskitemidcur" + (++pinx)); args_nv.Add(taskitemid); //本此采集,用到的taskitemid(多条数据)
                }
                //MessageBox.Show("加断点");
                pinx = 0;
                for (pinx = 0; pinx < DownCollectData.Instance.Collect.Count; pinx++)
                { //本此采集,用到的taskitemid
                    Stock t = DownCollectData.Instance.Collect[pinx];
                    args_nv.Add("tpnoycj" + (++pinx)); args_nv.Add(t.TrayNo); //已采集的托盘号
                    args_nv.Add("taskitemidycj" + (++pinx)); args_nv.Add(t.InTaskItemid); //已采集数据影响到的的任务记录号
                }
                DataSet ds = s.WS("WMS_AGV_PAK.getpda_caijizupan", args_nv.ToArray());
                if ((s.sqlcode == -1) || ds == null)
                {
                    s.logstr(s.sqlerrtext);
                    throw new Exception("检查是否允许组盘采集错误!" + s.sqlerrtext);
                }
                if (ds.Tables[0].Rows.Count == 0)
                {
                    throw new Exception("检查是否允许组盘采集提交错误,没有返回执行结果!");
                }
                int li_rtn = Convert.ToInt32(ds.Tables[0].Rows[0]["RET_CODE"]);
                if (li_rtn != 0)
                {
                    throw new Exception("检查是否允许组盘采集提交错误:\n" + ds.Tables[0].Rows[0]["RET_MESS"]);
                }
            }
            //
            lblMsg.Text = setMsg(""); //显示应做扫描动作的提示
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
            this.QueryListView.Items.Clear();
            this.QueryListView.Columns.Clear();
            //根据任务号获取任务明细
            QueryListView.Columns.Add("物料号", 120, HorizontalAlignment.Center);
            QueryListView.Columns.Add("库位", 120, HorizontalAlignment.Center);
            QueryListView.Columns.Add("任务数", 70, HorizontalAlignment.Center);
            QueryListView.Columns.Add("已采数", 70, HorizontalAlignment.Center);
            QueryListView.Columns.Add("批号", 140, HorizontalAlignment.Center);
            QueryListView.Columns.Add("序列号", 100, HorizontalAlignment.Center);
            QueryListView.Columns.Add("库房", 100, HorizontalAlignment.Center);
            QueryListView.Columns.Add("ERP子库", 90, HorizontalAlignment.Center);
            QueryListView.Columns.Add("intaskitemid", 0, HorizontalAlignment.Center);
            QueryListView.Columns.Add("parno", 0, HorizontalAlignment.Center);//
            QueryListView.Columns.Add("protype", 0, HorizontalAlignment.Center);//
            QueryListView.Columns.Add("入库单号", 90, HorizontalAlignment.Center);
            QueryListView.Columns.Add("物料旧编码", 140, HorizontalAlignment.Center);

            decimal repqty = 0;// matinnercode.Text = "";
            for (int i = 0; i < detailListView.Items.Count; i++)
            {
                if ((barcode == detailListView.Items[i].SubItems[0].Text.Trim()) && (batchNo == detailListView.Items[i].SubItems[4].Text.Trim()) && (sn == detailListView.Items[i].SubItems[5].Text.Trim()))
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
                               detailListView.Items[i].SubItems[12].Text.Trim() }));
                    //matinnercode.Text = detailListView.Items[i].SubItems[12].Text.Trim(); //2018-08-05 zxj add 显示物料旧编码
                    repqty = repqty + Convert.ToDecimal(detailListView.Items[i].SubItems[2].Text.Trim());

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
            if (siteLabel.Text.Trim().Equals(""))//库位为空 采集库位
            {
                return string.Format("{0}请采集库位", msg);
            }
            else if (trayLabel.Text.Trim().Equals(""))//托盘号为空 采集托盘号
            {
                return string.Format("{0}请采集托盘号", msg);
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
        /// 校验托盘号
        /// </summary>
        /// <param name="trayNo"></param>
        private decimal CheckTray(string trayNo)
        {
            if (string.IsNullOrEmpty(trayNo)) throw new Exception("托盘号不能为空");
            return service.CheckBindingTray(trayNo); //档案存在,且 PALLET.PALLET_USE_STATE = '4'空托盘 select * from PALLET where PALLET_USE_STATE = '4'
        }

        /// <summary>
        /// 校验托盘号
        /// </summary>
        /// <param name="trayNo"></param>
        private string CheckBindingTrayByTaskId(string trayNo)
        {
            if (string.IsNullOrEmpty(trayNo)) throw new Exception("托盘号不能为空");
            return service.CheckBindingTrayByTaskId(taskId,trayNo,"01"); //调用程序包
        }

        /// <summary>
        /// 校验采集物料
        /// </summary>
        /// <param name="mtlCode"></param>
        private void CheckMat(string mtlCode)
        {
            string tmpMat = string.Empty;
            string tmpSite = string.Empty;
            for (int i = 0; i < detailListView.Items.Count; i++)
            {
                tmpMat = detailListView.Items[i].SubItems[0].Text.Trim();
                tmpSite = detailListView.Items[i].SubItems[1].Text.Trim();

                if (tmpMat.Equals(mtlCode) && tmpSite.Equals(storeSite))
                {
                    erpRoom = detailListView.Items[i].SubItems[9].Text.Trim();
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
            if (matControl == "0") return;

            if (strMtlCode.Equals(string.Empty)) return;
            //如果强制库位 库位为空 校验
            if (siteFlag.Equals("Y"))
            {
                if (string.IsNullOrEmpty(strSiteCode))
                    return;
            }
            //如果强制批号 批号为空 校验
            if (batchFlag.Equals("Y") && (matControl == "1" || matControl == "2"))
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
                    string tmpBatch = detailListView.Items[i].SubItems[5].Text.Trim();
                    string tmpSite = detailListView.Items[i].SubItems[1].Text.Trim();
                    if (batchFlag == "Y" && (matControl == "1"||matControl == "2"))
                    {
                        if (tmpMat == strMtlCode && tmpBatch == strBatch && tmpSite == strSiteCode)
                        {
                            erpRoom = detailListView.Items[i].SubItems[9].Text.Trim();
                            return;
                        }
                    }
                    else
                    {
                        if (tmpMat == strMtlCode && tmpSite == strSiteCode)
                        {
                            erpRoom = detailListView.Items[i].SubItems[9].Text.Trim();
                            return;
                        }
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
                    string tmpBatch = detailListView.Items[i].SubItems[5].Text.Trim();
                    if (batchFlag == "Y" && (matControl == "1" || matControl == "2"))
                    {
                        if (tmpMat == strMtlCode && tmpBatch == strBatch)
                        {
                            erpRoom = detailListView.Items[i].SubItems[9].Text.Trim();
                            return;
                        }
                    }
                    else
                    {
                        if (tmpMat == strMtlCode)
                        {
                            erpRoom = detailListView.Items[i].SubItems[9].Text.Trim();
                            return;
                        }
                    }
                }
                if (batchFlag == "Y" && matControl == "0")
                {
                    throw new Exception(string.Format("采集物料【{0}】批次【{1}】不在任务明细中，请核实", strMtlCode, strBatch));
                }
                if (batchFlag == "Y" && (matControl == "1"||matControl == "2"))
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

            if (!dicMtlWeight.ContainsKey(matCode))
            {
                dicMtlWeight.Add(matCode, new List<string>());
                dicMtlWeight[matCode].Add(mtlWeight.ToString());
                dicMtlWeight[matCode].Add(mtlCapacity.ToString());
            }

            if (mtlCapacity * collectQty > trayCapacity)
            {
                throw new Exception(string.Format("物料【{0}】容量大于托盘剩余容量", mtlCapacity * collectQty));
            }

            decimal taskQty = 0;
            decimal tmpQty = 0;
            decimal tmpRepQty = 0;            
            decimal repQty = 0;
            if (!(string.IsNullOrEmpty(lbInv.Text)))
            {
                repQty = Convert.ToDecimal(lbInv.Text); //库存数量
            }
            else
            {
                repQty = 0;
            }
            
            bool exsitFlag = false;
            //if (Convert.ToInt16(matFlag) == 2)  //0单件(序列)控制，1批次控制，2无控制
            //{
            //    throw new Exception("物料【" + matCode + "】编码控制维护值维护不合法");
            //}
            #endregion

            #region 校验库存

            string strKey = checkInv(collectQty); //检查库存

            #endregion

            #region 统计当前物料总扫描数和总计划数
            decimal tatalTaskQty = 0;//当前物料总计划数
            decimal tatalTmpQty = 0;//当前物料总扫描数
            string tmpMat = string.Empty;
            string tmpBatch = string.Empty;
            string tmpSN = string.Empty;
            string tmpSite = string.Empty;
            
            for (int i = 0; i < detailListView.Items.Count; i++)
            {
                tmpMat = detailListView.Items[i].SubItems[0].Text.Trim();//物料
                if (tmpMat != matCode) continue;//如果物料不是当前输入的物料 继续
                
                if (Convert.ToInt16(matFlag) == 0)
                {
                    //tmpSN = detailListView.Items[i].SubItems[5].Text.Trim();
                    //if (tmpSN != sn) continue;//如果序列跟当前输入的不一致 继续

                    //if ((mtlCheckMode == MtlCheckMode.MtlBatchSite) || (mtlCheckMode == MtlCheckMode.MtlSite))
                    //{
                    //    tmpSite = detailListView.Items[i].SubItems[1].Text.Trim();
                    //    if (tmpSite != storeSite) continue;//如果库位跟当前输入的不一致 继续
                    //}
                }
                if (((Convert.ToInt16(matFlag) == 1) || (Convert.ToInt16(matFlag) == 2)) && ((matSendControl == "0" && roomMatControl == "0") || (roomMatControl == "1")))
                {
                    if (mtlCheckMode == MtlCheckMode.MtlBatch)
                    {
                        tmpBatch = detailListView.Items[i].SubItems[5].Text.Trim();
                        if (tmpBatch != batchNo) continue;//如果物料批次跟当前输入不一致 继续
                    }
                    else if (mtlCheckMode == MtlCheckMode.MtlBatchSite)
                    {
                        tmpBatch = detailListView.Items[i].SubItems[5].Text.Trim();
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
                matName = detailListView.Items[i].SubItems[11].Text.Trim();
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
            Dictionary<string, List<decimal>> dicMtlOperatin = new Dictionary<string, List<decimal>>(); //记录 <taskitemid, [计划数,采集数]> 的数组, 

            //考虑到物料采集数据中间被删除
            for (int i = 0; i < detailListView.Items.Count; i++)
            {
                #region 校验
                tmpMat = detailListView.Items[i].SubItems[0].Text.Trim();//物料
                tmpSite = detailListView.Items[i].SubItems[1].Text.Trim();//库位
                
                //计算单个物料剩余库存
                if ((tmpMat == matCode) && (tmpSite == storeSite) && (!(string.IsNullOrEmpty(detailListView.Items[i].SubItems[4].Text.Trim()))))
                {
                    tmpRepQty = Convert.ToDecimal(detailListView.Items[i].SubItems[4].Text.Trim());
                }
                if ((repQty > 0) && (tmpRepQty > 0) && (repQty > tmpRepQty))
                {
                    repQty = tmpRepQty;
                }
                #endregion 校验
            }

            for (int i = 0; i < detailListView.Items.Count; i++)
            {
                #region 校验
                if (decQty <= 0) break;
                tmpMat = detailListView.Items[i].SubItems[0].Text.Trim();//物料
                tmpSite = detailListView.Items[i].SubItems[1].Text.Trim();//库位
                taskQty = Convert.ToDecimal(detailListView.Items[i].SubItems[2].Text.Trim()); 
                tmpQty = Convert.ToDecimal(detailListView.Items[i].SubItems[3].Text.Trim());

                ////计算单个物料剩余库存
                //if ((tmpMat == matCode) && (!(string.IsNullOrEmpty(detailListView.Items[i].SubItems[4].Text.Trim()))))
                //{
                //   tmpRepQty = Convert.ToDecimal(detailListView.Items[i].SubItems[4].Text.Trim());
                //}
                //if ((repQty > 0) && (tmpRepQty > 0) && (repQty > tmpRepQty))
                //{
                //    repQty = tmpRepQty;
                //}

                if (((tmpMat != matCode) || (tmpSite != storeSite)) && (matFlag != "0") && ((matSendControl == "0" && roomMatControl == "0") || (roomMatControl == "1"))) continue;//如果物料不是当前输入的物料 继续

                if ((tmpMat != matCode) && (matFlag == "0") && ((matSendControl == "0" && roomMatControl == "0") || (roomMatControl == "1"))) continue;//如果物料不是当前输入的物料 继续

                if (tmpMat != matCode) continue; //

                if (taskQty == tmpQty) continue;

                switch (Convert.ToInt16(matFlag))       //0单件(序列)控制，1批次控制
                {
                    case 0:
                        #region 序列处理
                        //tmpSN = detailListView.Items[i].SubItems[6].Text.Trim();
                        //if (tmpSN != sn) continue;//如果序列跟当前输入的不一致 继续

                        //if ((mtlCheckMode == MtlCheckMode.MtlBatchSite) || (mtlCheckMode == MtlCheckMode.MtlSite))
                        //{
                        //    if (tmpSite != storeSite) continue;//如果库位跟当前输入的不一致 继续
                        //}
                        exsitFlag = true;
                        #endregion
                        break;
                    case 1:
                        #region 批次处理
                        if ((matSendControl == "0" && roomMatControl == "0")||(roomMatControl == "1"))
                        {
                            if (mtlCheckMode == MtlCheckMode.MtlBatch)
                            {
                                tmpBatch = detailListView.Items[i].SubItems[5].Text.Trim();
                                if (tmpBatch != batchNo) continue;//如果物料批次跟当前输入不一致 继续
                            }
                            else if (mtlCheckMode == MtlCheckMode.MtlBatchSite)
                            {
                                tmpBatch = detailListView.Items[i].SubItems[5].Text.Trim();
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
                        #endregion
                        break;
                    case 2:
                        #region 无控制类似于批次处理
                        if ((matSendControl == "0" && roomMatControl == "0")||(roomMatControl == "1"))
                        {
                            if (mtlCheckMode == MtlCheckMode.MtlBatch)
                            {
                                tmpBatch = detailListView.Items[i].SubItems[5].Text.Trim();
                                if (tmpBatch != batchNo) continue;//如果物料批次跟当前输入不一致 继续
                            }
                            else if (mtlCheckMode == MtlCheckMode.MtlBatchSite)
                            {
                                tmpBatch = detailListView.Items[i].SubItems[5].Text.Trim();
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
                        #endregion
                        break;
                }
                #endregion

                #region 计算使用明细的使用量 处理
                outTaskItemid = detailListView.Items[i].SubItems[8].Text.Trim();
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
                    detailListView.Items[i].SubItems[4].Text = Convert.ToString(repQty - decQty);
                    dicMtlQty[outTaskItemid][1] = (tmpQty + decQty).ToString();
                    dicMtlOperatin[outTaskItemid].Add(decQty);
                    decQty = 0;
                    exsitFlag = true;
                }
                else
                {
                    decQty = decQty - (taskQty - tmpQty);//本次扫描数量- 计划剩余数量
                    detailListView.Items[i].SubItems[3].Text = taskQty.ToString();
                    detailListView.Items[i].SubItems[4].Text = Convert.ToString(repQty - taskQty);
                    dicMtlQty[outTaskItemid][1] = taskQty.ToString();
                    dicMtlOperatin[outTaskItemid].Add(taskQty - tmpQty);
                }
                #endregion
            }
            if ((matSendControl == "0" && roomMatControl == "0")||(roomMatControl == "1"))
            {
                if (!exsitFlag) throw new Exception("采集物料批号序列号信息匹配任务明细失败");
            }

            trayCapacity -= mtlCapacity * collectQty;
            currentCapacity += mtlCapacity * collectQty;//当前容积增加
            currentWeight += mtlWeight * collectQty;//当前体积增加
            lbTrayCapacity.Text = trayCapacity.ToString();

            if (!string.IsNullOrEmpty(sn) && !dicSeq.ContainsKey(matCode+"@"+sn))
            {
                dicSeq.Add(matCode + "@" + sn, matCode + "@" + sn);
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

            /*
             * //应在此处再调用一次检查,看看对应的托盘或者物料是否可用, 其实最好应该在数数量之前判断,而不是输数量之后判断
            //保存数据库,采用调程序包的方式
            int pii = 0, pinx = 0; SQLHelper s = new SQLHelper(); List<string> args_nv = new List<string>();
            args_nv.Add("tpnocur"); args_nv.Add(trayNo); pinx = 0; //本次采集用的托盘号
            foreach (string taskitemid in dicMtlOperatin.Keys)
            {
                args_nv.Add("taskitemidcur" + (++pinx)); args_nv.Add(taskitemid); //本此采集,用到的taskitemid(多条数据)
            }
            //MessageBox.Show("加断点");
            pinx = 0;
            for (pinx = 0; pinx < DownCollectData.Instance.Collect.Count; pinx++)
            { //本此采集,用到的taskitemid
                Stock t = DownCollectData.Instance.Collect[pinx];
                args_nv.Add("tpnoycj" + (++pinx)); args_nv.Add(t.TrayNo); //已采集的托盘号
                args_nv.Add("taskitemidycj" + (++pinx)); args_nv.Add(t.InTaskItemid); //已采集数据影响到的的任务记录号
            }
            DataSet ds = s.WS("WMS_AGV_PAK.getpda_caijizupan", args_nv.ToArray());
            if ((s.sqlcode == -1) || ds == null)
            {
                throw new Exception("检查是否允许组盘采集错误!" + s.sqlerrtext);
            }
            if (ds.Tables[0].Rows.Count == 0)
            {
                throw new Exception("检查是否允许组盘采集提交错误,没有返回执行结果!");
            }
            int li_rtn = Convert.ToInt32(ds.Tables[0].Rows[0]["RET_CODE"]);
            if (li_rtn != 0)
            {
                throw new Exception("检查是否允许组盘采集提交错误:\n" + ds.Tables[0].Rows[0]["RET_MESS"]);
            }*/
            //添加采集记录;对于采集记录的修改操作统一在采集明细中操作 
            DownCollectData.Instance.AddCollectData(matCode, batchNo, sn, collectQty, storeRoom, storeSite, dicMtlOperatin, erpStoreInv, matName, trayNo);
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
            drCheck = dtRepertory.Select(string.Format("storesiteno='{0}' and matcode='{1}'", storeSite, matCode));
            //if (collectQty == 0)
            lbInv.Text = dtRepertory.Compute("sum(repqty)", string.Format("storesiteno='{0}' and matcode='{1}'", storeSite, matCode)).ToString();//显示库存数
            string strRepQty = string.Empty;
            decimal RepQty = 0;
            if (!(string.IsNullOrEmpty(lbInv.Text)))
            {
                RepQty = Convert.ToDecimal(lbInv.Text);
            }
 
            string strKey = string.Empty;

            if (matControlFlag.Equals("1") || matControlFlag.Equals("2"))
            {
                strKey = storeSite + matCode + batchNo;
                if (!string.IsNullOrEmpty(erpRoom))
                {
                    drCheck = dtRepertory.Select(string.Format("storesiteno='{0}' and matcode='{1}' and batchno='{2}' and erp_storeroom='{3}' ", storeSite, matCode, batchNo, erpRoom));
                    strRepQty = dtRepertory.Compute("sum(repqty)", string.Format("storesiteno='{0}' and matcode='{1}' and batchno='{2}' and erp_storeroom='{3}' ", storeSite, matCode, batchNo, erpRoom)).ToString();
                    if (!(string.IsNullOrEmpty(strRepQty)))
                    {
                        RepQty = Convert.ToDecimal(strRepQty);
                    }
                    else
                    {
                        RepQty = 0;
                    } 
                }
                else
                {
                    drCheck = dtRepertory.Select(string.Format("storesiteno='{0}' and matcode='{1}' and batchno='{2}'  ", storeSite, matCode, batchNo));
                    strRepQty = dtRepertory.Compute("sum(repqty)", string.Format("storesiteno='{0}' and matcode='{1}' and batchno='{2}'  ", storeSite, matCode, batchNo)).ToString();
                    if (!(string.IsNullOrEmpty(strRepQty)))
                    {
                        RepQty = Convert.ToDecimal(strRepQty);
                    }
                    else
                    {
                        RepQty = 0;
                    }
                }
                if (drCheck.Length == 0)
                {
                    throw new Exception(string.Format("物料【{0}】批次【{1}】在库位【{2}】不存在，请确认", matCode, batchNo, storeSite));
                }
            }
            else
            {
                strKey = storeSite + matCode + sn;
                if (!string.IsNullOrEmpty(erpRoom))
                {
                    drCheck = dtRepertory.Select(string.Format("storesiteno='{0}' and matcode='{1}' and sn='{2}' and erp_storeroom='{3}'", storeSite, matCode, sn, erpRoom));
                    strRepQty = dtRepertory.Compute("sum(repqty)", string.Format("storesiteno='{0}' and matcode='{1}' and sn='{2}' and erp_storeroom='{3}'", storeSite, matCode, sn, erpRoom)).ToString();
                    if (!(string.IsNullOrEmpty(strRepQty)))
                    {
                        RepQty = Convert.ToDecimal(strRepQty);
                    }
                    else
                    {
                        RepQty = 0;
                    }
                }
                else
                {
                    drCheck = dtRepertory.Select(string.Format("storesiteno='{0}' and matcode='{1}' and sn='{2}' ", storeSite, matCode, sn));
                    strRepQty = dtRepertory.Compute("sum(repqty)", string.Format("storesiteno='{0}' and matcode='{1}' and sn='{2}' ", storeSite, matCode, sn)).ToString();
                    if (!(string.IsNullOrEmpty(strRepQty)))
                    {
                        RepQty = Convert.ToDecimal(strRepQty);
                    }
                    else
                    {
                        RepQty = 0;
                    }
                }
                if (drCheck.Length == 0)
                {
                    throw new Exception(string.Format("物料【{0}】序列【{1}】在库位【{2}】不存在，请确认", matCode, sn, storeSite));
                }
               
            }

            
            //库存ERP子库
            erpStoreInv = drCheck[0]["erp_storeroom"].ToString();
            if (!string.IsNullOrEmpty(erpRoom.Trim()))
            {
                if (!erpStoreInv.Equals(erpRoom))
                {
                    throw new Exception(string.Format("当前物料明细指定子库【{0}】与当前库位的物料批次子库【{1}】存在不一致，请确认", erpRoom, drCheck[0]["erp_storeroom"].ToString()));
                }
            }

            if (collectQty > 0)
            {
                decimal decRepqty = 0;
                if (dicInvMtlQty.ContainsKey(strKey))
                    decRepqty = dicInvMtlQty[strKey];

                //if (Convert.ToDecimal(drCheck[0]["repqty"].ToString()) - decRepqty < collectQty)
                if (RepQty - decRepqty < collectQty)                    
                {
                    throw new Exception(string.Format("库位【{0}】物料【{1}】的库存【{2}】小于本次移出库存【{3}】，请确认", storeSite, matCode, (RepQty - decRepqty), collectQty));
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
                TrayDownCollectDetailFrm downDetailFrm = new TrayDownCollectDetailFrm();
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
                string outTaskItemid = detailListView.Items[i].SubItems[8].Text.Trim();
                string matCode = detailListView.Items[i].SubItems[0].Text.Trim();//物料
                string siteNo = detailListView.Items[i].SubItems[1].Text.Trim();//库位
                int matControl = service.GetMatControl(matCode);
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
                        decimal inv_dec = Convert.ToDecimal(detailListView.Items[i].SubItems[4].Text);
                        dicMtlQty[outTaskItemid][1] = (dec - Convert.ToDecimal(updateInfo[0])).ToString();

                        //dicMtlWeight 第 一位 重 第二位容积
                        //减去承重 物料 乘以 移除的数量
                        //托盘容量得加上来
                        currentWeight -= Convert.ToDecimal(dicMtlWeight[matCode][0]) * Convert.ToDecimal(updateInfo[0]);
                        currentCapacity -= Convert.ToDecimal(dicMtlWeight[matCode][1]) * Convert.ToDecimal(updateInfo[0]);
                        trayCapacity += Convert.ToDecimal(dicMtlWeight[matCode][1]) * Convert.ToDecimal(updateInfo[0]);
                        lbTrayCapacity.Text = trayCapacity.ToString();

                        detailListView.Items[i].SubItems[3].Text = dicMtlQty[outTaskItemid][1];
                        if (matControl == 0)
                        {
                            detailListView.Items[i].SubItems[4].Text = "";
                        }
                        else
                        {
                            detailListView.Items[i].SubItems[4].Text = (inv_dec + Convert.ToDecimal(updateInfo[0])).ToString();
                        }
                        for (int j = 0; j < detailListView.Items.Count; j++)
                        {
                            string tmpMatCode = detailListView.Items[j].SubItems[0].Text.Trim();//物料
                            string tmpSite = detailListView.Items[j].SubItems[1].Text.Trim();//库位
                            if (((j > i)) && (matCode == tmpMatCode) && (siteNo == tmpSite) && (!string.IsNullOrEmpty(detailListView.Items[j].SubItems[4].Text)))
                            {
                                decimal invdec = Convert.ToDecimal(detailListView.Items[j].SubItems[4].Text);
                                detailListView.Items[j].SubItems[4].Text = (invdec + Convert.ToDecimal(updateInfo[0])).ToString();
                            }

                        }
                        
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
            foreach (KeyValuePair<string, decimal> inv in dicDeleteInv)
            {
                if (dicInvMtlQty.ContainsKey(inv.Key))
                    dicInvMtlQty[inv.Key] -= inv.Value;
            }
  
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
                    downShelvesInfo.TrayNo = stock.TrayNo;
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
                service.CommitTrayDownShelves(downShelvesInfos, User.Instance.UserData.UserId, lsItems);
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
            _2DBarcode, Site,TrayNo, Quantity
        }

        private void exceptButton_Click(object sender, EventArgs e)
        {
            List<Stock> stocks = DownCollectData.Instance.Collect;

            if (stocks.Count > 0)
            {
                MessageBox.Show("采集数据未提交,不允许异常登记！");
                return;
            }

            ExceptTaskFrm frm = new ExceptTaskFrm(taskComment, taskNo, taskId, "平库下架", storeRoom,"");
            frm.ShowDialog();
        }

        
        private void finishButton_Click(object sender, EventArgs e)
        {
            try
            {
                List<Stock> stocks = DownCollectData.Instance.Collect;

                if (stocks.Count > 0) throw new Exception("采集数据未提交,不允许报缺！");

                if (MessageBox.Show("请确认是否该采集明细物料报缺？",
                        "物料报缺", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    return;
                }
                //接收完毕将接收的任务移掉
                string outTaskItemId = string.Empty;
                int j = detailListView.Items.Count - 1;
                for (int i =j ; i >= 0; i--)
                {
                    if (detailListView.Items[i].Selected)
                    {
                        outTaskItemId = detailListView.Items[i].SubItems[8].Text.Trim();
                        service.CommitFinishOutTaskItem(outTaskItemId, User.Instance.UserData.UserId, "0", false);                        
                    }
                }
                this.detailListView.Items.Clear();
                this.detailListView.Columns.Clear();
                detailListView.Columns.Add("物料号", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("库位", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("任务数", 70, HorizontalAlignment.Left);
                detailListView.Columns.Add("已采数", 70, HorizontalAlignment.Left);
                detailListView.Columns.Add("库存数", 70, HorizontalAlignment.Left);
                detailListView.Columns.Add("批号", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("序列号", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("库房", 100, HorizontalAlignment.Left);
                detailListView.Columns.Add("outtaskitemid", 0, HorizontalAlignment.Left);
                detailListView.Columns.Add("ERP子库", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("物料名称", 200, HorizontalAlignment.Left);
                DataSet ds = service.GetOutTaskItem(User.Instance.UserData.UserId, taskNo, taskComment, "0", "0", workStation);
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    detailListView.Items.Add(new ListViewItem(
                    new string[] { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), " ",dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString(), dr[9].ToString() }));
                }
                
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        private void supplierButton_Click(object sender, EventArgs e)
        {
            //List<Stock> stocks = DownCollectData.Instance.Collect;

            //if (stocks.Count > 0)
            //{
            //    MessageBox.Show("采集数据未提交,不允许采集供应商二维码！");
            //    return;
            //}
            SupplierTaskFrm frm = new SupplierTaskFrm(taskComment, taskNo, taskId, "平库下架", storeRoom, "");
            frm.ShowDialog();
        }
    }
}