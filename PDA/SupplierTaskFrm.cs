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
using System.Threading;
using System.IO.Ports;

namespace PDA
{
    public partial class SupplierTaskFrm : Form
    {
        SerialPort server;

        public SupplierTaskFrm(string strTaskComment, string strTaskNo, string strTaskId, string strProtype, string roomCode, string strtrayNo)
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
        private string supplierNo;
         //private string supplierNo;
        private string supplierNoSN;
        MiddleService service = new MiddleService();
        Management management = Management.GetSingleton();

        string matCode = string.Empty;
        string matName = string.Empty;
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

        private void SupplierTaskFrm_Load(object sender, EventArgs e)
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
                detailListView.Columns.Add("已采数", 70, HorizontalAlignment.Center);
                detailListView.Columns.Add("批号", 140, HorizontalAlignment.Center);
                detailListView.Columns.Add("序列号", 100, HorizontalAlignment.Center);
                detailListView.Columns.Add("供应商批次", 100, HorizontalAlignment.Center);
                detailListView.Columns.Add("供应商序列", 100, HorizontalAlignment.Center);
                detailListView.Columns.Add("托盘号", 100, HorizontalAlignment.Center);                
                detailListView.Columns.Add("业务类型", 90, HorizontalAlignment.Center);
                detailListView.Columns.Add("货位", 140, HorizontalAlignment.Center);
                detailListView.Columns.Add("库房", 120, HorizontalAlignment.Center);
                detailListView.Columns.Add("taskid", 0, HorizontalAlignment.Center);
                detailListView.Columns.Add("parno", 0, HorizontalAlignment.Center);//
                detailListView.Columns.Add("protype", 0, HorizontalAlignment.Center);//
                
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
            //if (barcode.IndexOf('*') > 0)
            //{
            //    currStep = Step._2DBarcode;
            //}
            if (barcode.IndexOf("*DN") > 0) //采集托盘信息
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
                currStep = Step.SupplierNo;
            }
            #endregion

            #region 处理逻辑
            switch (currStep)
            {
                case Step._2DBarcode:
                    //if (barcode.IndexOf('*') < 0) throw new Exception("采集内容不合法，请采集二维码");
                    BarcodeContent barcodeContent = service.AnalysisForNewBarcode(barcode);
                    //throw new Exception("物料" + barcodeContent.MatCode + "批次号：" + barcodeContent.BN + "序列号：" + barcodeContent.SN);
 
                    //根据物料属性判断，该物料对应的编码控制    0单件(序列)控制，1批次控制，2无控制
                    int matControl = service.GetMatControl(barcodeContent.MatCode);

                    if (matControl == 0)
                    {
                        if (dicSeq.ContainsKey(barcodeContent.SN))
                        {
                            matCode = string.Empty;
                            throw new Exception(string.Format("序列号【{0}】不允许重复采集，请确认", barcodeContent.SN));
                        }

                        sn = barcodeContent.SN;
                        serialNoLabel.Text = sn;
                         if (barcode.IndexOf("*BN") >= 0)
                         {
                             batchNo = barcodeContent.BatchNo;
                        }
                        else {
                            batchNo = sn;
                        }
                        batchLabel.Text = batchNo;
                        collectQty = 1;
                        qtyLabel.Text = "1";
                        
                        if (!supplierCheckBox.Checked)
                        {
                            supplierNoSN = sn;
                            supplierNoLabelSN.Text = supplierNoSN;

                            supplierNo = batchNo;
                            supplierNoLabel.Text = supplierNo;
                        }
                    }
                    else if ((matControl == 1) || (matControl == 2))
                    {
                        sn = string.Empty;
                        serialNoLabel.Text = sn;

                        supplierNoSN = string.Empty;
                        supplierNoLabelSN.Text = supplierNoSN;

                        if (barcode.IndexOf("*BN") >= 0)
                        {
                            batchNo = barcodeContent.BatchNo;
                        }
                        else {
                            batchNo = barcodeContent.SN;
                        }

                        batchLabel.Text = batchNo;
                        if (!supplierCheckBox.Checked) {
                            supplierNo = batchNo;
                            supplierNoLabel.Text = supplierNo;
                        }
                        
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
                    trayLabel.Text = trayNo;                  
                    break;
                case Step.Site:         //$KW$+库位号
                    string[] sArry = barcode.Split('$');
                    storeSite = sArry[2];
                    siteLabel.Text = storeSite;
                    //检验库存
                    lbInv.Text = "";
                    break;
                //供应商二维码
                case Step.SupplierNo:
                    int matControl2 = service.GetMatControl(matCode);
                    supplierNo = barcode;
                    supplierNoLabel.Text = supplierNo;
                    if (matControl2 == 0)
                    {
                        supplierNoSN = barcode;
                        supplierNoLabel.Text = supplierNo;
                    }
                    
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
            if (matCodeLabel.Text.Trim().Equals(""))//条码为空 采集条码
            {
                return string.Format("{0}请扫描二维码", msg);
            }
            else if (serialNoLabel.Text.Trim ().Equals ("") && qtyLabel.Text.Trim().Equals(""))//肯定是批次  如数量为空
            {
                return string.Format("{0}请输入数量", msg);
            }
            else if (supplierNoLabel.Text.Trim().Equals("") && supplierCheckBox.Checked)//格式不一致，必须扫描供应商二维码
            {
                return string.Format("{0}请扫描供应商二维码", msg);
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
            supplierNoLabel.Text = "";
            supplierNoLabelSN.Text = "";
            trayLabel.Text = "";
            siteLabel.Text = "";
            lbInv.Text = "";

            matCode = string.Empty;
            batchNo = string.Empty;            
            sn = string.Empty;
            supplierNo = string.Empty;
            supplierNoSN = string.Empty;

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
                if (qty <= 0) throw new Exception("采集数量必须大于0");
                #endregion

                detailListView.Items.Add(new ListViewItem(
                        new string[] { matCode, qty.ToString(), batchNo, sn, supplierNo,supplierNoSN, trayNo, proType, storeSite, storeRoom, taskid }));
                SupplierCollectData.Instance.AddCollectData(matCode, batchNo, sn, Convert.ToDecimal(qty), storeSite, storeRoom, taskid, taskComment, taskNo, proType, supplierNo,supplierNoSN, trayNo);
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        
        private void collectItemButton_Click(object sender, EventArgs e)
        {
            try
            {
                SupplierCollectDetailFrm supplierDetailFrm = new SupplierCollectDetailFrm();
                supplierDetailFrm.ShowDialog();
                List<Stock> stocks = SupplierCollectData.Instance.Collect;
                detailListView.Items.Clear();
                foreach (Stock stock in stocks)
                {
                    detailListView.Items.Add(new ListViewItem(new string[] { stock.MatCode,stock.CollectQty.ToString(),stock.BatchNo,stock.Sn,stock.SupplierNo,stock.TrayNo,
                                        stock.Protype,stock.StoreRoom,stock.StoreSite,stock.Taskid }));
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
                if (SupplierCollectData.Instance.Collect.Count == 0)
                {
                    throw new Exception("本次无采集明细，请确认！");
                }
                string tmpMat = string.Empty;
                string msg = string.Empty;
                string tmpStore=string.Empty;
                
                msg = "请确认是否提交？";
                if (MessageBox.Show(msg,
                        "供应商二维码数据采集", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
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

                List<Stock> collectStocks = SupplierCollectData.Instance.Collect;
                SupplierShelvesInfo[] supplierShelvesInfos = new SupplierShelvesInfo[collectStocks.Count];
                foreach (Stock stock in collectStocks)
                {
                    SupplierShelvesInfo supplierShelvesInfo = new SupplierShelvesInfo();
                    supplierShelvesInfo.TaskNo = taskNo;
                    supplierShelvesInfo.MatCode = stock.MatCode;        //物料号
                    supplierShelvesInfo.BatchNo = stock.BatchNo;        //批号 
                    supplierShelvesInfo.Sn = stock.Sn;                  //序列号   
                    supplierShelvesInfo.TaskQty = stock.TaskQty;        //任务数量 
                    supplierShelvesInfo.CollectQty = stock.CollectQty;  //已采集数量 
                    supplierShelvesInfo.Taskid = stock.Taskid;
                    supplierShelvesInfo.StoreSiteNo = stock.StoreSite;
                    supplierShelvesInfo.Protype = stock.Protype;
                    supplierShelvesInfo.ProofNo = stock.ProofNo;
                    supplierShelvesInfo.PalletNo = stock.TrayNo;
                    supplierShelvesInfo.SupplierNo = stock.SupplierNo;
                    supplierShelvesInfos[i] = supplierShelvesInfo;
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

                service.CommitSupplierShelves(supplierShelvesInfos, User.Instance.UserData.UserId, lsItems, filter);
                //Message.Alarm("成功", "提交成功");

                SupplierCollectData.Instance.Collect = new List<Stock>();
                detailListView.Items.Clear();
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("当前采集数量是{0},是否确认关闭？", SupplierCollectData.Instance.Collect.Count),
                          "供应商二维码采集", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                          MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }
            SupplierCollectData.Instance.Collect = new List<Stock>();
            this.Close();
        }

        enum Step
        {
            _2DBarcode, Quantity, Site, TrayNo, SupplierNo
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            int j = detailListView.Items.Count - 1;
            for (int i = j; i >= 0; i--)
            {
                if (detailListView.Items[i].Checked)
                {

                    String StrMtlCode = detailListView.Items[i].SubItems[0].Text.Trim();
                    String StrMtlName = service.GetMaName(StrMtlCode);
                    String StrBatchNo = detailListView.Items[i].SubItems[2].Text.Trim();
                    String StrSnNo = detailListView.Items[i].SubItems[3].Text.Trim();
                    String StrSuperNo = ""; 
                    String StrSuperBar = detailListView.Items[i].SubItems[4].Text.Trim();
                    String StrSuperBarSN = detailListView.Items[i].SubItems[5].Text.Trim();

                    String mtlCode = "";
                    String mtlName = "";
                    String BatchNo = "";
                    String SNNo = "";
                    String SupplierNo = "";
                    String SupplierNoSN = "";
                    String SupplierBarCode = "";
                    String SupplierBarSNCode = "";

                    mtlCode = string.Format("{0}", "物料编号：" + StrMtlCode);

                    mtlName = string.Format("{0}", "物料名称：" + StrMtlName);

                    if (!string.IsNullOrEmpty(StrBatchNo) && !string.IsNullOrEmpty(StrSnNo))
                    {
                        BatchNo = string.Format("{0}", "批次号：" + StrBatchNo);
                        SNNo = string.Format("{0}", "序列号：" + StrSnNo);
                        SupplierNo = string.Format("{0}", "批 次 号：" + StrSuperBar);
                        SupplierNoSN = string.Format("{0}", "序 列 号：" + StrSuperBarSN);
                    }
                    else if (!string.IsNullOrEmpty(StrBatchNo) && string.IsNullOrEmpty(StrSnNo))
                    {
                        BatchNo = string.Format("{0}", "批次号：" + StrBatchNo);
                        SupplierNo = string.Format("{0}", "批 次 号：" + StrSuperBar);
                    }
                    else
                    {
                        BatchNo = string.Format("{0}", "批次号：" + StrBatchNo);
                        SupplierNo = string.Format("{0}", "批 次 号：" + StrSuperBar);
                    }                    
                    
                    SupplierBarCode = string.Format("{0}", StrSuperBar);
                    SupplierBarSNCode = string.Format("{0}", StrSuperBarSN);

                    //MessageBox.Show("BatchNo:" + BatchNo + "SNNo:" + SNNo + "SupplierNo:" + SupplierNo + "SupplierNoSN:" + SupplierNoSN + "SupplierBarCode:" + SupplierBarCode + "SupplierBarSNCode:" + SupplierBarSNCode);
                    if (!PrintSuperBarCode(StrMtlCode,mtlCode, mtlName, BatchNo, SNNo, SupplierNo, SupplierNoSN, SupplierBarCode, SupplierBarSNCode))
                    {
                        MessageBox.Show("打印失败");
                    }
                    detailListView.Items[i].Checked = false;
                }
            }

            
        }

        public bool PrintSuperBarCode(string StrMtlCode,string mtlCode, string mtlName, string BatchNo, string SNNo, string SupplierNo, string SupplierNoSN,string SupplierBarCode,string SupplierBarSNCode)
        {
            bool ret = false;
            try
            {
                server = new SerialPort();
                server.PortName = "COM9";//固定
                server.WriteBufferSize = 2048;
                server.BaudRate = 57600;//波特率
                server.Parity = System.IO.Ports.Parity.None;
                server.DataBits = 8;//数据位
                server.StopBits = System.IO.Ports.StopBits.One;//停止位d
                server.Handshake = System.IO.Ports.Handshake.None;
                server.RtsEnable = true;
                server.WriteTimeout = 1000;
                server.Open();
                string sssqrcode = "";
                sssqrcode = service.GetMtlSupplier(StrMtlCode, mtlCode, mtlName, BatchNo, SNNo, SupplierNo, SupplierNoSN, SupplierBarCode, SupplierBarSNCode);
                int k = 0;
                if (server.IsOpen)
                {
                    try
                    {
                        int BufferSize1;
                        byte[] cmdData1 = Encoding.Default.GetBytes(sssqrcode);
                        BufferSize1 = Encoding.Default.GetByteCount(sssqrcode);
                        server.DiscardInBuffer();
                        server.DiscardOutBuffer();
                        server.Write(cmdData1, 0, BufferSize1);                        
                        Thread.Sleep(100);
                        Application.DoEvents();
                        while (server.BytesToWrite > 0)
                        {
                            Application.DoEvents();
                            k++;
                            if (k > 9999)
                                break;
                        }
                        server.Close();
                        ret = true;
                    }
                    catch { }
                }
                server.Close();
            }
            catch { }
            return ret;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            List<SupplierShelvesInfo> mList = new List<SupplierShelvesInfo>(); 
             int j = detailListView.Items.Count - 1;
             for (int i = j; i >= 0; i--)
             {
                 if (detailListView.Items[i].Checked)
                 {
                     SupplierShelvesInfo item = new SupplierShelvesInfo();
                     item.MatCode = detailListView.Items[i].SubItems[0].Text.Trim();
                     item.BatchNo = detailListView.Items[i].SubItems[2].Text.Trim();
                     item.Sn = detailListView.Items[i].SubItems[3].Text.Trim();
                     item.SupplierCode = "";
                     item.SupplierNo = detailListView.Items[i].SubItems[4].Text.Trim();
                     mList.Add(item);
                 }
             }
             if (mList.Count > 0) {
                // PrintFrm frm = new PrintFrm();
                // frm.ShowDialog();
             }
            
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < detailListView.Items.Count; i++)
            {
                detailListView.Items[i].Checked = true;
            }
        }
    }
}