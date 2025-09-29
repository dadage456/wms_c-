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
    public partial class InventoryTaskItemFrm : Form
    {
        public InventoryTaskItemFrm(string taskNo, string taskId,  string taskComment,string storeRoom)
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
        private string taskNo = string.Empty;
        private string taskId = string.Empty;
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

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InventoryTaskItemFrm_Load(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "请扫描库位：";
                InvTaskCollectData.Instance.Collect = new List<InventoryData>();
                //根据任务号获取任务明细
                this.detailListView.Columns.Clear();
                detailListView.Columns.Add("库房", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("库位", 0, HorizontalAlignment.Left);
                detailListView.Columns.Add("物料", 0, HorizontalAlignment.Left);
                detailListView.Columns.Add("盘库类型", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("盘库数量", 80, HorizontalAlignment.Left);
                detailListView.Columns.Add("任务号", 80, HorizontalAlignment.Left);                
                detailListView.Columns.Add("co_checkitemid", 0, HorizontalAlignment.Left);
                detailListView.Columns.Add("托盘号", 0, HorizontalAlignment.Left);

                lbSiteRoom.Text = storeRoom;
                DataSet ds = service.GetInventoryTaskItem(taskComment, taskNo, "0");
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
                    if (!dicMtlInfo.ContainsKey(dr[1].ToString()))
                    {
                        //存储物料位置  扫描数量默认为0
                        List<decimal> ls = new List<decimal>();
                        ls.Add(i);
                        ls.Add(0);
                        dicMtlInfo.Add(dr[1].ToString(), ls);
                    }
                    detailListView.Items.Add(new ListViewItem(
                    new string[] { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[7].ToString(), dr[6].ToString() }));
                    i++;
                }

                if (checkType == CheckType.Mtl)
                {
                    detailListView.Columns[2].Width =150;
                }
                else
                {
                    detailListView.Columns[1].Width = 150;
                }
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
                    BarcodeContent barcodeContent = service.AnalysisForNewBarcode(barcode);
                    bool ISNEWEWM = false; if (barcode.IndexOf("*BN") >= 0) { ISNEWEWM = true; }
                     //根据物料属性判断，该物料对应的编码控制    0单件(序列)控制，1批次控制，2无控制
                    matId = string.Empty;
                    int matControl = service.GetMatControl(barcodeContent.MatCode, out matId);

                    //校验物料
                    CheckMat(barcodeContent.MatCode);
                   
                    if (matControl == 0)
                    {
                        if (dicSeq.ContainsKey(barcodeContent.MatCode+"@"+barcodeContent.SN))
                        {
                            throw new Exception(string.Format("序列号【{0}】不允许重复采集，请确认", barcodeContent.MatCode + "@" + barcodeContent.SN));
                        }

                        batchLabel.Text = ISNEWEWM ? barcodeContent.BatchNo : string.Empty; ;
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
                    CheckSite(sArry[2]);
                    siteLabel.Text = sArry[2];
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
        /// 回填数量，更新LISTVIEW(结合强制库位强制批次标识)
        /// </summary>
        /// <param name="barcode"></param>
        private void DealQuantity(decimal collectQty, string matFlag)
        {
            #region 变量及校验
            if (string.IsNullOrEmpty(matControlFlag)) throw new Exception("获取物料编码属性失败");
            if (collectQty <= 0) throw new Exception("采集数量必须大于0");

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
                invTaskItemid = detailListView.Items[i].SubItems[6].Text.Trim();//库位

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
                if ((invTaskItemid.Equals(string.Empty)) && (detailListView.Items[0].SubItems[3].Text.Trim().ToString() == "全盘盘点"))
                {
                    invTaskItemid = detailListView.Items[0].SubItems[6].Text.ToString();//获取明细Id
                }
                if (!string.IsNullOrEmpty(strSn) && !dicSeq.ContainsKey(strMatCode+"@"+strSn))
                {
                    dicSeq.Add(strMatCode + "@" + strSn, strMatCode + "@" + strSn);
                }
                #endregion

                //添加采集记录;对于采集记录的修改操作统一在采集明细中操作 
                InvTaskCollectData.Instance.AddCollectData(strMatCode, strBatch, strSn, collectQty, storeRoom, strSite, matId, invTaskItemid, "");
            
            }
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
                InventoryTaskDetailFrm invDetailFrm = new InventoryTaskDetailFrm();
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
                string invTaskItemid = detailListView.Items[i].SubItems[6].Text.Trim();
                string tmp= detailListView.Items[i].SubItems[1].Text.Trim();
                if (dicUpdateInfo.ContainsKey(invTaskItemid))
                {
                    dicMtlInfo[tmp][1] -= dicUpdateInfo[invTaskItemid];//删除数
                    detailListView.Items[i].SubItems[4].Text = dicMtlInfo[tmp][1].ToString ();
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
            _2DBarcode, Site, Quantity
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
            ExceptTaskFrm frm = new ExceptTaskFrm(taskComment, taskNo, taskId, "平库盘点", storeRoom,"");
            frm.ShowDialog();
        }

        private void QueryCollectBN_Click(object sender, EventArgs e)
        {
            try
            {
                if (siteLabel.Text.Equals(string.Empty)) throw new Exception("货位号不能为空，请扫描货位号再查询");
                QueryCheckCollectDataFrm frm = new QueryCheckCollectDataFrm(taskComment, taskId, taskNo, siteLabel.Text, "00");
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }
    }
}