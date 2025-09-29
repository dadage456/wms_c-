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
    public partial class ASWHInventoryTaskItemFrm : Form
    {
        public ASWHInventoryTaskItemFrm(string taskNo, string taskId, string taskComment, string storeRoom)
        {
            InitializeComponent();
            this.taskNo = taskNo;
            this.taskId = taskId;
            this.storeRoom = storeRoom;
            this.taskComment = taskComment;
        }

        private MiddleService service = new MiddleService();
        private Management management = Management.GetSingleton();
        private DataTable roomTable = new DataTable();
        private DataTable siteTable = new DataTable();
        private string trayNo = string.Empty;
        private string taskNo = string.Empty;
        private string taskId = string.Empty;
        private string taskType = string.Empty;
        private string storeSite = string.Empty;
        private string isWhole = string.Empty;
        private string matControlFlag = string.Empty;//物料标识
        private string matId = string.Empty;//物料id
        private string taskComment = string.Empty;//凭证号
        private string storeRoom = string.Empty;//库房
        private CheckType checkType=CheckType.Mtl;//盘库类型 默认物料

        /// <summary>
        /// Key: 物料\库位 Value：0 位置 1数量
        /// </summary>
        Dictionary<string, List<decimal>> dicMtlInfo = new Dictionary<string, List<decimal>>();
        Dictionary<string, string> dicSeq = new Dictionary<string, string>();
        private Dictionary<string, string> dicPalletNo = new Dictionary<string, string>();

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InventoryTaskItemFrm_Load(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "请扫描托盘号：";
                InvTaskCollectData.Instance.Collect = new List<InventoryData>();
                //根据任务号获取任务明细
                this.detailListView.Columns.Clear();
                detailListView.Columns.Add("托盘号", 100, HorizontalAlignment.Left);
                detailListView.Columns.Add("库位", 0, HorizontalAlignment.Left);
                detailListView.Columns.Add("物料", 0, HorizontalAlignment.Left);
                detailListView.Columns.Add("盘库类型", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("盘库数量", 80, HorizontalAlignment.Left);
                detailListView.Columns.Add("任务号", 80, HorizontalAlignment.Left);
                detailListView.Columns.Add("库房", 120, HorizontalAlignment.Left);              
                detailListView.Columns.Add("co_checkitemid", 0, HorizontalAlignment.Left);
                detailListView.Columns.Add("子库", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("状态", 120, HorizontalAlignment.Left);

                //根据任务号获取任务明细
                this.QueryListView.Columns.Clear();
                QueryListView.Columns.Add("托盘号", 100, HorizontalAlignment.Left);
                QueryListView.Columns.Add("库位", 0, HorizontalAlignment.Left);
                QueryListView.Columns.Add("物料", 0, HorizontalAlignment.Left);
                QueryListView.Columns.Add("盘库类型", 120, HorizontalAlignment.Left);
                QueryListView.Columns.Add("盘库数量", 80, HorizontalAlignment.Left);
                QueryListView.Columns.Add("任务号", 80, HorizontalAlignment.Left);
                QueryListView.Columns.Add("库房", 120, HorizontalAlignment.Left);
                QueryListView.Columns.Add("co_checkitemid", 0, HorizontalAlignment.Left);
                QueryListView.Columns.Add("子库", 120, HorizontalAlignment.Left);
                QueryListView.Columns.Add("状态", 120, HorizontalAlignment.Left);

                lbSiteRoom.Text = storeRoom;
                DataSet ds = service.GetInventoryTaskItem(taskComment,taskNo,"1");
                DataTable dt = ds.Tables[0];
                string strCheckType = string.Empty;
                int i=0;
                foreach (DataRow dr in dt.Rows)
                {
                    //校验检验类型
                    if (strCheckType == string.Empty)
                    {
                        if (dr[3].ToString() == "库位")
                            checkType = CheckType.Site;
                            strCheckType = dr[2].ToString();
                    }
                    if (dr[3].ToString() == "全盘盘点")
                    {
                        isWhole = "Y";                        
                    }
                    else
                    {
                        isWhole = "N";
                    }
                    if (!dicMtlInfo.ContainsKey(dr[1].ToString()))
                    {
                        //存储物料位置  扫描数量默认为0
                        List<decimal> ls = new List<decimal>();
                        ls.Add(i);
                        ls.Add(0);
                        dicMtlInfo.Add(dr[1].ToString(), ls);
                    }
                    detailListView.Items.Add(new ListViewItem(
                    new string[] { dr[6].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[0].ToString(), dr[7].ToString(), dr[8].ToString(), dr[9].ToString() }));
                    i++;
                }

                if (checkType == CheckType.Mtl)
                {
                    detailListView.Columns[2].Width = 150;
                    QueryListView.Columns[2].Width = 150;
                }
                else
                {                    
                    detailListView.Columns[1].Width = 150;
                    QueryListView.Columns[1].Width = 150;
                }
                if (isWhole == "Y")
                {
                    detailListView.Columns[0].Width = 0;
                    QueryListView.Columns[0].Width = 0;
                }
                else
                {
                    detailListView.Columns[0].Width = 100;
                    QueryListView.Columns[0].Width = 100;
                }
                
                DataSet InOutds = service.GetInOutLocation("1");
                DataTable InOutdt = InOutds.Tables[0];
                INOUTComboBox.DataSource = InOutdt;

                #region 设定参数

                //根据库房获取该库房下的所有库位
                siteTable = service.GetStoreSiteByRoom(storeRoom,"").Tables[0];
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

            matCodeLabel.Text = "";
            batchLabel.Text = "";
            serialNoLabel.Text = "";
            qtyLabel.Text = "";

            matControlFlag = string.Empty;
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
            else if (barcode.StartsWith("$TP$"))//采集托盘信息
            {
                currStep = Step.TrayNo;
            }
            //else if (barcode.StartsWith("$KW$"))
            //{
            //    currStep = Step.Site;
            //}            
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
                    BarcodeContent barcodeContent = service.AnalysisForNewBarcode(barcode);
                    bool ISNEWEWM = false; if (barcode.IndexOf("*BN") >= 0)  {  ISNEWEWM = true;  }
                     //根据物料属性判断，该物料对应的编码控制    0单件(序列)控制，1批次控制，2无控制
                    matId = string.Empty;
                    int matControl = service.GetMatControl(barcodeContent.MatCode, out matId);

                    //校验物料
                    //CheckMat(barcodeContent.MatCode);
                   
                    if (matControl == 0)
                    {
                        if (dicSeq.ContainsKey(barcodeContent.MatCode+"@"+barcodeContent.SN))
                        {
                            throw new Exception(string.Format("序列号【{0}】不允许重复采集，请确认", barcodeContent.MatCode + "@" + barcodeContent.SN));
                        }

                        batchLabel.Text = ISNEWEWM ? barcodeContent.BatchNo : string.Empty;
                        qtyLabel.Text = "1";
                        serialNoLabel.Text = barcodeContent.SN;
                    }
                    else if ((matControl == 1)||(matControl == 2))
                    {
                        serialNoLabel.Text = ISNEWEWM ? barcodeContent.SN : string.Empty; if (serialNoLabel.Text == null) { serialNoLabel.Text = string.Empty; }
                        batchLabel.Text = ISNEWEWM ? barcodeContent.BatchNo : barcodeContent.SN;
                    }
                    else
                    {
                        throw new Exception("物料" + barcodeContent.MatCode + "编码控制维护值维护不合法");
                    }

                    matCodeLabel.Text = barcodeContent.MatCode; ;
                    matControlFlag = matControl.ToString();
                    #endregion
                    break;
                case Step.Site:         //$KW$+库位号
                    string[] sArry = barcode.Split('$');
                    //CheckSite(sArry[2]);
                    siteLabel.Text = sArry[2];
                    break;
                //托盘
                case Step.TrayNo:
                    //decimal decTrayCapacity = CheckTray(barcode.Substring(4));// "" 、 TP 、 000002
                    //trayCapacity = decTrayCapacity;
                    //lbTrayCapacity.Text = trayCapacity.ToString();
                    //currentCapacity = 0;
                    //currentWeight = 0;
                    trayNo = barcode.Substring(4);
                    CheckTrayNo(trayNo);
                    trayLabel.Text = trayNo;
                    QueryTask(trayNo);
                    siteLabel.Text = "";
                    for (int i = 0; i < detailListView.Items.Count; i++)
                    {
                        string tmpTrayNo = detailListView.Items[i].SubItems[0].Text.Trim();
                        storeSite = detailListView.Items[i].SubItems[1].Text.Trim();
                        if (tmpTrayNo == trayNo)
                        {
                            siteLabel.Text = storeSite;
                            break;
                        }
                    }
                    if (siteLabel.Text.Trim().Equals(string.Empty) && (taskType == "全盘盘点"))
                    {
                        siteLabel.Text = GetCheckTray(taskComment,trayNo);
                    }
                    break;
                case Step.Quantity:
                    if (!serialNoLabel.Text.Trim ().Equals(string.Empty))
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
                tmpTrayNo = detailListView.Items[i].SubItems[0].Text.Trim();
                taskType = detailListView.Items[i].SubItems[3].Text.Trim();

                if (tmpTrayNo.Equals(palletNo))
                {
                    return;
                }
            }
            if (!(isWhole == "Y")) throw new Exception(string.Format("任务明细中不存在托盘号【{0}】", palletNo));
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
            //如果为库位模式 不校验物料
            if (checkType == CheckType.Site) return;
            if (!dicMtlInfo.ContainsKey(mtlCode))
            {
                throw new Exception(string.Format("物料【{0}】不在盘点任务清单", mtlCode));
            }
        }

        /// <summary>
        /// 校验库房下的所有库位中是否包含当前库位
        /// </summary>
        /// <param name="siteCode"></param>
        private void CheckSite(string siteCode)
        {
            DataRow[] siteDr = siteTable.Select("storesiteno= '" + siteCode + "'");
            if (siteDr.Length <= 0) throw new Exception("库房" + storeRoom + "下不存在编号为" + siteCode + "的库位");
            //如果是库位模式
            if (checkType == CheckType.Site)
            {
                if (!dicMtlInfo.ContainsKey(siteCode))
                {
                    throw new Exception(string.Format ("库位【{0}】不在盘点任务清单",siteCode));
                }
            }
        }

        /// <summary>
        /// 获取盘点托盘号的货位
        /// </summary>
        /// <param name="trayNo"></param>
        private string GetCheckTray(string strtaskComment,string strtrayNo)
        {
            if (string.IsNullOrEmpty(strtaskComment)) throw new Exception("凭证号不能为空");
            if (string.IsNullOrEmpty(trayNo)) throw new Exception("托盘号不能为空");

            return service.GetCheckTray(strtaskComment, strtrayNo);
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

            string strtrayNo = trayLabel.Text.Trim();
            string strSite = siteLabel.Text.Trim();
            string strMatCode = matCodeLabel.Text.Trim();
            string strSn = serialNoLabel.Text.Trim();
            string strBatch = batchLabel.Text.Trim();
            string tmpMat = string.Empty;
            string tmpSite = string.Empty;
            string invTaskItemid = string.Empty;
            string tmp = string.Empty;

            if (checkType == CheckType.Mtl)
            {
                tmp = matCodeLabel.Text.Trim();//物料
            }
            else if (checkType == CheckType.Site)
            {
                tmp = siteLabel.Text.Trim();//库位
            }
            else
            {
                tmp = matCodeLabel.Text.Trim();//物料
            }

            #endregion

            #region 处理逻辑
            //dicMtlInfo Value: 0位置 1是数量

            for (int i = 0; i < detailListView.Items.Count; i++)
            {
                #region 校验
                tmpSite = detailListView.Items[i].SubItems[1].Text.Trim();//库位
                tmpMat = detailListView.Items[i].SubItems[2].Text.Trim();//物料
                invTaskItemid = detailListView.Items[i].SubItems[7].Text.Trim();//ID

                if (!((tmp == tmpMat) || (tmp == tmpSite))) continue;//如果物料或货位不是当前输入的物料 继续
                
                if (!dicMtlInfo.ContainsKey(invTaskItemid))
                {
                    //存储物料位置  扫描数量默认为0
                    List<decimal> ls = new List<decimal>();
                    ls.Add(0);
                    ls.Add(0);
                    dicMtlInfo.Add(invTaskItemid, ls);
                }

                dicMtlInfo[invTaskItemid][1] += collectQty;//采集数
                detailListView.Items[i].SubItems[4].Text = dicMtlInfo[invTaskItemid][1].ToString();//更新画面采集数

                if (!string.IsNullOrEmpty(strSn) && !dicSeq.ContainsKey(strMatCode+"@"+strSn))
                {
                    dicSeq.Add(strMatCode + "@" + strSn, strMatCode + "@" + strSn);
                }
                #endregion

                //添加采集记录;对于采集记录的修改操作统一在采集明细中操作 
                InvTaskCollectData.Instance.AddCollectData(strMatCode, strBatch, strSn, collectQty, storeRoom, strSite, matId, invTaskItemid, strtrayNo);
            }

            #endregion
            
            QueryTask(trayNo);
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
                ASWHInventoryTaskDetailFrm invDetailFrm = new ASWHInventoryTaskDetailFrm();
                invDetailFrm.ShowDialog();
                UpdateListViewItem(invDetailFrm.dicUpdateInfo, invDetailFrm.dicDelete);
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        /// <summary>
        /// 刷新显示明细，主要是更新数量字段
        /// </summary>
        private void UpdateListViewItem(Dictionary<string, decimal> dicUpdateInfo, Dictionary<string, string> dicDeleteSeq)
        {
            for (int i = 0; i < detailListView.Items.Count; i++)
            {
                string invTaskItemid = detailListView.Items[i].SubItems[7].Text.Trim();
                string tmp= detailListView.Items[i].SubItems[1].Text.Trim();
                if (dicUpdateInfo.ContainsKey(invTaskItemid))
                {
                    dicMtlInfo[tmp][1] -= dicUpdateInfo[invTaskItemid];//删除数
                    detailListView.Items[i].SubItems[4].Text = dicMtlInfo[tmp][1].ToString ();
                }
            }

            for (int i = 0; i < QueryListView.Items.Count; i++)
            {
                string invTaskItemid = QueryListView.Items[i].SubItems[7].Text.Trim();
                string tmp = detailListView.Items[i].SubItems[1].Text.Trim();

                if (dicUpdateInfo.ContainsKey(invTaskItemid))
                {
                    dicMtlInfo[tmp][1] -= dicUpdateInfo[invTaskItemid];//删除数
                    QueryListView.Items[i].SubItems[4].Text = dicMtlInfo[tmp][1].ToString();
                }
            }

            //处理删除的
            foreach (string del in dicDeleteSeq.Values)
            {
                if (dicDeleteSeq.ContainsKey(del))
                    dicSeq.Remove(del);
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
                //校验
                beforeCommit();

                int i = 0;
                InventoryInfo[] infos = new InventoryInfo[InvTaskCollectData.Instance.Collect.Count];
                foreach (InventoryData invData in InvTaskCollectData.Instance.Collect)
                {
                    InventoryInfo info = new InventoryInfo();
                    info.TaskComment = taskComment;
                    info.MatCode = invData.MatCode;        //物料号
                    info.BatchNo = invData.BatchNo;        //批号 
                    info.Sn = invData.Sn;             //序列号   
                    info.CollectQty = invData.InventoryQty;      //已采集数量 
                    info.StoreRoomNo = invData.StoreRoom;
                    info.StoreSiteNo = invData.StoreSite;
                    info.InvTaskItemid = invData.InvTaskItemid;
                    info.MaterialId = invData.MatId;
                    info.TrayNo = invData.TrayNo;
                    infos[i] = info;
                    i++;
                }

                service.CommitInventoryInfos(taskComment, infos, User.Instance.UserData.UserId);
                InvTaskCollectData.Instance.Collect = new List<InventoryData>();
                dicMtlInfo.Clear();
                Message.Alarm("提示", "提交成功！");
                //this.Close();
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Message.Alarm("提示", ex.Message);
                return;
            } 
        }


        /// <summary>
        /// 提交数据前校验
        /// </summary>
        private void beforeCommit()
        {
            if (InvTaskCollectData.Instance.Collect.Count == 0)
            {
                throw new Exception("未采集物料明细，请确认");
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
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("当前采集数量是{0},是否确认关闭？", InvTaskCollectData.Instance.Collect.Count),
      "盘库采集", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
      MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }

            InvTaskCollectData.Instance.Collect = new List<InventoryData>();
            this.Close(); 
        }
          
        /// <summary>
        /// 条码类型
        /// </summary>
        enum Step
        {
            _2DBarcode, Site, Quantity, TrayNo
        }

        /// <summary>
        /// 检验类型
        /// </summary>
        enum CheckType
        {
            Site,Mtl
        }

        private void exceptButton_Click(object sender, EventArgs e)
        {
            ExceptTaskFrm frm = new ExceptTaskFrm(taskComment, taskNo, taskId, "立库盘点", storeRoom, "");
            frm.ShowDialog();
        }

        private void INButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("请确认来料托盘回库吗？",
                      "在线盘点", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                      MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    return;
                }
                if (InvTaskCollectData.Instance.Collect.Count > 0) throw new Exception("采集数据未提交,不允许来料盘回库！");
                if (taskNo.Equals(string.Empty)) throw new Exception("凭证号为空，请确认");
                string startAddr = INOUTComboBox.SelectedValue.ToString();
                if (startAddr.Equals(string.Empty)) throw new Exception("拣选口位置不能为空");

                if (trayNo.Equals(string.Empty)) throw new Exception("回库的托盘号不能为空，请扫描托盘号！");

                string endAddr = string.Empty;
                int j = detailListView.Items.Count - 1;
                for (int i = j; i >= 0; i--)
                {
                    if (detailListView.Items[i].SubItems[0].Text.Trim() == trayNo)
                    {
                        endAddr = detailListView.Items[i].SubItems[1].Text.Trim();
                    }
                }
                
                if (endAddr.Equals(string.Empty))
                {
                    endAddr = siteLabel.Text.Trim();
                }
                
                if (endAddr.Equals(string.Empty))
                {
                    endAddr = "000000";
                }

                service.CommitInvResetWmsToWcs(User.Instance.UserData.UserId, taskId, taskNo, trayNo, startAddr, endAddr);
                Message.Alarm("成功", "来料盘料盘回库成功,请等待");
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
                if (MessageBox.Show("请确认获取来料托盘吗？",
                      "在线盘点", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                      MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    return;
                }
                if (InvTaskCollectData.Instance.Collect.Count > 0) throw new Exception("采集数据未提交,不允许获取新的来料盘！");
                if (taskNo.Equals(string.Empty)) throw new Exception("凭证号为空，请确认");
                string endAddr = INOUTComboBox.SelectedValue.ToString();
                if (endAddr.Equals(string.Empty)) throw new Exception("拣选口位置不能为空");

                string sourcetrayNo = string.Empty;
                string startAddr = string.Empty;
                int j = detailListView.Items.Count - 1;
                for (int i = j; i >= 0; i--)
                {
                    if (detailListView.Items[i].Selected)
                    {
                        sourcetrayNo = detailListView.Items[i].SubItems[0].Text.Trim();
                        startAddr = detailListView.Items[i].SubItems[1].Text.Trim();
                    }
                }
         
                if (sourcetrayNo.Equals(string.Empty))
                {
                    sourcetrayNo = "TP000000";
                    startAddr = "000000";
                }                
                service.CommitInvDownWmsToWcs(User.Instance.UserData.UserId, taskId, taskNo, sourcetrayNo, startAddr, endAddr, "1");
                Message.Alarm("成功", "获取来料盘成功,请等待");
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        private void WmsToWcsBN_Click(object sender, EventArgs e)
        {
            List<InventoryData> stocks = InvTaskCollectData.Instance.Collect;

            if (stocks.Count > 0)
            {
                MessageBox.Show("采集数据未提交,不允许查看指令！");
                return;
            }

            ASWHWmsToWcs frm = new ASWHWmsToWcs(taskComment, taskId,"99");
            frm.ShowDialog();
        }

        private void QryCheckOrderPalletNoBN_Click(object sender, EventArgs e)
        {
            List<InventoryData> stocks = InvTaskCollectData.Instance.Collect;

            if (stocks.Count > 0)
            {
                MessageBox.Show("采集数据未提交,不允许查看复查托盘！");
                return;
            }

            ASWHCheckOrderPalletNo frm = new ASWHCheckOrderPalletNo(taskComment, taskId, taskNo, "4");
            frm.ShowDialog();
        }

        private void WCSbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("请确认获取来料托盘吗？",
                      "在线盘点", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                      MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    return;
                }

                if (InvTaskCollectData.Instance.Collect.Count > 0) throw new Exception("采集数据未提交,不允许获取新的来料盘！");
                if (taskNo.Equals(string.Empty)) throw new Exception("凭证号为空，请确认");

                if (INOUTComboBox.SelectedIndex == -1) throw new Exception("拣选口位置不能为空");
                string endAddr = INOUTComboBox.SelectedValue.ToString();
                if (endAddr.Equals(string.Empty)) throw new Exception("拣选口位置不能为空");

                if (string.IsNullOrEmpty(PalletNum.Text.Trim())) throw new Exception("获取来料盘数据不能为空！");

                string sourcetrayNo = string.Empty;
                string startAddr = string.Empty;

                string errMessage = string.Empty;
                int j = detailListView.Items.Count - 1;

                string successFlg = string.Empty;
                int palletnum = Convert.ToInt16(PalletNum.Text.Trim());
                if (checkType == CheckType.Mtl)
                {
                    if (j < 5) j = 100;

                    for (int i = j; i >= 0; i--)
                    {
                        if (palletnum > 0)
                        {
                            sourcetrayNo = "TP000000";
                            startAddr = "000000";

                            successFlg = service.CommitInvDownWmsToWcs(User.Instance.UserData.UserId, taskId, taskNo, sourcetrayNo, startAddr, endAddr, "0");
                            if (successFlg == "Y")
                            {
                                palletnum = palletnum - 1;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = j; i >= 0; i--)
                    {
                        if (palletnum > 0)
                        {
                            sourcetrayNo = detailListView.Items[i].SubItems[0].Text.Trim();
                            startAddr = detailListView.Items[i].SubItems[1].Text.Trim();
                            if (!(dicPalletNo.ContainsKey(sourcetrayNo)))
                            {
                                successFlg = service.CommitInvDownWmsToWcs(User.Instance.UserData.UserId, taskId, taskNo, sourcetrayNo, startAddr, endAddr, "0");
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
                }

                if (string.IsNullOrEmpty(errMessage))
                {
                    Message.Alarm("成功", "获取来料盘成功,请等待");
                }
                else
                {
                    errMessage = "来料盘【" + errMessage + "】获取失败，请逐个选择这些托盘，点击【单个来料盘】按钮获取详细错误信息！";
                    Message.Alarm("失败", errMessage);
                }

            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
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
            QueryListView.Columns.Add("托盘号", 100, HorizontalAlignment.Left);
            QueryListView.Columns.Add("库位", 0, HorizontalAlignment.Left);
            QueryListView.Columns.Add("物料", 0, HorizontalAlignment.Left);
            QueryListView.Columns.Add("盘库类型", 120, HorizontalAlignment.Left);
            QueryListView.Columns.Add("盘库数量", 80, HorizontalAlignment.Left);
            QueryListView.Columns.Add("任务号", 80, HorizontalAlignment.Left);
            QueryListView.Columns.Add("库房", 120, HorizontalAlignment.Left);
            QueryListView.Columns.Add("co_checkitemid", 0, HorizontalAlignment.Left);
            QueryListView.Columns.Add("子库", 120, HorizontalAlignment.Left);
            QueryListView.Columns.Add("状态", 120, HorizontalAlignment.Left);

            if (checkType == CheckType.Mtl)
            {
                detailListView.Columns[2].Width = 150;
                QueryListView.Columns[2].Width = 150;
            }
            else
            {
                detailListView.Columns[1].Width = 150;
                QueryListView.Columns[1].Width = 150;
            }
            if (isWhole == "Y")
            {
                detailListView.Columns[0].Width = 0;
                QueryListView.Columns[0].Width = 0;
            }
            else
            {
                detailListView.Columns[0].Width = 100;
                QueryListView.Columns[0].Width = 100;
            }

            for (int i = 0; i < detailListView.Items.Count; i++)
            {
                if (barcode == detailListView.Items[i].SubItems[0].Text.Trim())
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
                               detailListView.Items[i].SubItems[9].Text.Trim()}));

                }
            }
        }

        private void QueryCollectBN_Click(object sender, EventArgs e)
        {
            try
            {
                if (trayNo.Equals(string.Empty)) throw new Exception("托盘号不能为空，请扫描托盘号再查询");
                //trayNo = "TP000602";
                QueryCheckCollectDataFrm frm = new QueryCheckCollectDataFrm(taskComment, taskId, taskNo, trayNo, "04");
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }
    }
}