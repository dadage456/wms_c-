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
    public partial class ExceptTaskFrm : Form
    {
        public ExceptTaskFrm(string strTaskComment, string strTaskNo, string strTaskId, string strProtype, string roomCode, string strtrayNo)
        {
            InitializeComponent();
            this.taskComment = strTaskComment;
            this.taskNo = strTaskNo;
            this.taskid = strTaskId;
            this.proType = strProtype;
            this.storeRoom = roomCode;
            this.trayNo = strtrayNo;
        }
        private string taskComment;
        private string taskNo;
        private string taskid;
        private string proType;
        private string storeRoom;
        private string trayNo;
        MiddleService service = new MiddleService();
        Management management = Management.GetSingleton();

        string matCode = string.Empty;
        string batchNo = string.Empty;        
        string sn = string.Empty;        
        string storeSite = string.Empty;
        decimal collectQty = 0;     //采集数量 
        string matControlFlag = string.Empty;
        string supplier = string.Empty;//供应商
        string exceptType = string.Empty;//判断是否完工入库
        Dictionary<string, List<string>> dicMtlQty = new Dictionary<string, List<string>>();//key: intaskitemid value: 0:开始采集数  1：本次数量
        Dictionary<string, string> dicSeq = new Dictionary<string, string>();
        private string erpStoreSite = string.Empty;//ERP子库

        private void ExceptTaskFrm_Load(object sender, EventArgs e)
        {
            try
            {
                UpCollectData.Instance.Collect = new List<Stock>();
                lblMsg.Text = "请扫描库位：";
                tbxBarcode.Text = "";
                tbxBarcode.Focus();
                tbxBarcode.SelectAll();

                qtyLabel.Text = "";
                trayLabel.Text = trayNo;
                 
                //根据任务号获取任务明细
                this.detailListView.Columns.Clear();
                detailListView.Columns.Add("物料号", 120, HorizontalAlignment.Center);
                detailListView.Columns.Add("库位", 120, HorizontalAlignment.Center);
                detailListView.Columns.Add("异常类型", 70, HorizontalAlignment.Center);
                detailListView.Columns.Add("已采数", 70, HorizontalAlignment.Center);
                detailListView.Columns.Add("批号", 140, HorizontalAlignment.Center);
                detailListView.Columns.Add("序列号", 100, HorizontalAlignment.Center);
                detailListView.Columns.Add("库房", 100, HorizontalAlignment.Center);
                detailListView.Columns.Add("业务类型", 90, HorizontalAlignment.Center);
                detailListView.Columns.Add("taskid", 0, HorizontalAlignment.Center);
                detailListView.Columns.Add("parno", 0, HorizontalAlignment.Center);//
                detailListView.Columns.Add("protype", 0, HorizontalAlignment.Center);//

                DataSet ds = service.GetExceptionType();
                DataTable dt = ds.Tables[0];
                ExceptComboBox.DataSource = dt;
                ExceptComboBox.SelectedValue = "-1";
                                
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
            else if (barcode.StartsWith("$TP$"))//采集托盘信息
            {
                currStep = Step.TrayNo;
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
                    bool ISNEWEWM = false; if (barcode.IndexOf("*BN") >= 0) { ISNEWEWM = true; }
                    //根据物料属性判断，该物料对应的编码控制    0单件(序列)控制，1批次控制，2无控制
                    int matControl = service.GetMatControl(barcodeContent.MatCode);

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
                        throw new Exception("物料" + barcodeContent.MatCode + "编码控制维护值维护不合法");
                    }
                    matCode = barcodeContent.MatCode;
                    matCodeLabel.Text = matCode;
                    matControlFlag = matControl.ToString();
                    //currStep = Step.Site;
                    //lblMsg.Text = "请采集库位：";
                    //checkInv(0);
                    break;
                //托盘
                case Step.TrayNo:
                    trayNo = barcode.Substring(4);
                    storeSite = GetPalletSiteNo(trayNo);
                    trayLabel.Text = trayNo;
                    siteLabel.Text = storeSite;                    
                    break;
                case Step.Site:         //$KW$+库位号
                    //if (barcode.IndexOf('$') < 0) throw new Exception("库位条码不合法");
                    string[] sArry = barcode.Split('$');
                    //CheckSite(sArry[2]);
                    storeSite = sArry[2];
                    //20160124与刘确认：如果实际采集库位与任务明细中的库位不一致，不去更新任务明细界面的库位信息，去采集明细中查看
                    siteLabel.Text = storeSite;
                    //currStep = Step.Quantity;
                    //lblMsg.Text = "请采集数量：";
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

        }

        /// <summary>
        /// 回填数量，更新LISTVIEW;添加采集记录集
        /// </summary>
        /// <param name="barcode"></param>
        private void DealQuantity(decimal qty, string matFlag)
        {
            try
            {
                #region 变量及校验@
                if (string.IsNullOrEmpty(matControlFlag)) throw new Exception("获取物料编码属性失败");
                if (ExceptComboBox.SelectedValue == null || string.IsNullOrEmpty(ExceptComboBox.SelectedValue.ToString()) || string.IsNullOrEmpty(ExceptComboBox.Text.Trim())) throw new Exception("异常类型不能为空");
                if (qty <= 0) throw new Exception("采集数量必须大于0");
                #endregion

                exceptType = ExceptComboBox.SelectedValue.ToString();
                string exceptName = ExceptComboBox.Text;

                //添加采集记录;对于采集记录的修改操作统一在采集明细中操作
                //detailListView.Columns.Add("物料号", 120, HorizontalAlignment.Center);
                //detailListView.Columns.Add("库位", 120, HorizontalAlignment.Center);
                //detailListView.Columns.Add("异常类型", 70, HorizontalAlignment.Center);
                //detailListView.Columns.Add("已采数", 70, HorizontalAlignment.Center);
                //detailListView.Columns.Add("批号", 140, HorizontalAlignment.Center);
                //detailListView.Columns.Add("序列号", 100, HorizontalAlignment.Center);
                //detailListView.Columns.Add("库房", 100, HorizontalAlignment.Center);
                //detailListView.Columns.Add("taskid", 0, HorizontalAlignment.Center);
                //detailListView.Columns.Add("parno", 0, HorizontalAlignment.Center);
                //detailListView.Columns.Add("protype", 0, HorizontalAlignment.Center);
                detailListView.Items.Add(new ListViewItem(
                        new string[] { matCode, storeSite, exceptName, qty.ToString(), batchNo, sn, storeRoom, proType, taskid }));
                ExceptCollectData.Instance.AddCollectData(matCode, batchNo, sn, exceptType, Convert.ToDecimal(qty), storeRoom, storeSite, taskid, exceptName, taskComment, taskNo, proType);
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }

        }

        private void CheckSite(string siteCode)
        {
            try
            {
                DataTable siteTable = service.GetStoreSiteByRoom(storeRoom, siteCode).Tables[0];    //根据库房获取该库房下的所有库位
                DataRow[] siteDr = siteTable.Select("storesiteno= '" + siteCode + "'");
                if (siteDr.Length <= 0) throw new Exception("库房" + storeRoom + "下无库位号" + siteCode);

                if (siteDr[0]["isfrozen"].ToString() != "0")
                {
                    throw new Exception(string.Format("库位【{0}】被锁定或者冻结", siteCode));
                }
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        /// <summary>
        /// 根据托盘号获取货位
        /// </summary>
        /// <param name="trayNo"></param>
        private string GetPalletSiteNo(string trayNo)
        {
            if (string.IsNullOrEmpty(trayNo)) throw new Exception("托盘号不能为空");
            return service.GetPalletSiteNo(trayNo);
        }

        private void collectItemButton_Click(object sender, EventArgs e)
        {
            try
            {
                ExceptCollectDetailFrm exceptDetailFrm = new ExceptCollectDetailFrm();
                exceptDetailFrm.ShowDialog();
                List<Stock> stocks = ExceptCollectData.Instance.Collect;
                detailListView.Items.Clear();
                foreach (Stock stock in stocks)
                {
                    detailListView.Items.Add(new ListViewItem(new string[] { stock.MatCode,stock.StoreSite,stock.Desc,stock.CollectQty.ToString(),stock.BatchNo,stock.Sn,
                                        stock.StoreRoom ,stock.Protype,stock.Taskid }));
                }
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
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
                if (ExceptCollectData.Instance.Collect.Count == 0)
                {
                    throw new Exception("本次无采集明细，请确认！");
                }

                string tmpMat = string.Empty;
                string msg = string.Empty;
                string tmpStore=string.Empty;
                
                msg = "请确认是否提交？";
                if (MessageBox.Show(msg,
                        "异常数据采集", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    return;
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

                List<Stock> collectStocks = ExceptCollectData.Instance.Collect;
                ExceptShelvesInfo[] exceptShelvesInfos = new ExceptShelvesInfo[collectStocks.Count];
                foreach (Stock stock in collectStocks)
                {
                    ExceptShelvesInfo exceptShelvesInfo = new ExceptShelvesInfo();
                    exceptShelvesInfo.TaskNo = taskNo;
                    exceptShelvesInfo.MatCode = stock.MatCode;        //物料号
                    exceptShelvesInfo.BatchNo = stock.BatchNo;        //批号 
                    exceptShelvesInfo.Sn = stock.Sn;             //序列号   
                    exceptShelvesInfo.TaskQty = stock.TaskQty;    //任务数量 
                    exceptShelvesInfo.CollectQty = stock.CollectQty;      //已采集数量 
                    exceptShelvesInfo.StoreRoomNo = stock.StoreRoom;
                    exceptShelvesInfo.StoreSiteNo = stock.StoreSite;
                    exceptShelvesInfo.Taskid = stock.Taskid;
                    exceptShelvesInfo.Excepttype = stock.Excepttype;
                    exceptShelvesInfo.Protype = stock.Protype;
                    exceptShelvesInfo.ProofNo = stock.ProofNo;
                    exceptShelvesInfo.PalletNo = trayNo;
                    //CheckSite(stock.StoreRoom, stock.StoreSite);
                    exceptShelvesInfos[i] = exceptShelvesInfo;
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

                service.CommitExceptShelves(exceptShelvesInfos, User.Instance.UserData.UserCode, lsItems, filter);
                //Message.Alarm("成功", "提交成功");

                ExceptCollectData.Instance.Collect = new List<Stock>();
                detailListView.Items.Clear();
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
            if (MessageBox.Show(string.Format("当前采集数量是{0},是否确认关闭？", ExceptCollectData.Instance.Collect.Count),
                          "异常采集", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                          MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }
            ExceptCollectData.Instance.Collect = new List<Stock>();
            this.Close();
        }

        enum Step
        {
            _2DBarcode, Site, Quantity, TrayNo
        }
    }
}