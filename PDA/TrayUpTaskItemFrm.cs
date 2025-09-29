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
    public partial class TrayUpTaskItemFrm : Form
    {
        public TrayUpTaskItemFrm(string taskNo, string taskId, string roomCode, string siteFlag, string batchFlag, string taskComment, string taskFinishFlag, string workStation)
        {
            InitializeComponent();
            this.taskNo = taskNo;
            this.taskId = taskId;
            this.storeRoom = roomCode;
            this.siteFlag = siteFlag;
            this.siteFlag = siteFlag;
            this.finishFlag = taskFinishFlag;
            this.taskComment = taskComment;
            this.workStation = workStation;
        }
        #region 变量
        private string taskComment;
        private string workStation;
        private string taskNo;
        private string taskId;
        string storeRoom = string.Empty; 
        private string siteFlag = string.Empty;
        private string batchFlag = string.Empty;
        private string finishFlag = string.Empty;
        private string inTaskItemid = string.Empty;
        Step currStep;
        Management management = Management.GetSingleton(); 
        MiddleService service = new MiddleService();
        //List<BindingTray> totalStocks = new List<BindingTray>();
        DataTable siteTable = new DataTable();
        string trayNo = string.Empty;
        string matCode = string.Empty;
        string sn = string.Empty;
        string batchNo = string.Empty;
        string storeSite = string.Empty;
        string supplier = string.Empty;//供应商        
        string matControlFlag = string.Empty;               //物料编码控制
        private Dictionary<string, List<string>> dicMtlQty = new Dictionary<string, List<string>>();//key: intaskitemid value: 0:开始采集数  1：本次数量
        private Dictionary<string, decimal> dicInvMtlQty = new Dictionary<string, decimal>();//Key：库位+物料+批次 ||  库位+序列  Value：本次作业数量
        private Dictionary<string, string> dicPalletNo = new Dictionary<string, string>();
        private Dictionary<string, string> dicTryNoMtl = new Dictionary<string, string>();//存储托盘物料明细
        private Dictionary<string, List<string>> dicMtlWeight = new Dictionary<string, List<string>>();//存储物料的容量 //第一位 承重 第二位容积
        private bool boCheckMtl = true;//是否检查物料
        private Dictionary<string, string> dicSeq = new Dictionary<string, string>();
        
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BindingDetailFrm_Load(object sender, EventArgs e)
        { 
            try
            {
                BindingTrayCollectData.Instance.Collect = new List<BindingTray>();
                boCheckMtl = service.GetIsCheckMtl();//检查物料

                lblMsg.Text = "请扫描托盘号：";
                tbxBarcode.Text = "";
                tbxBarcode.Focus();
                qtyLabel.Text = "";
                supplier = string.Empty;

                //根据任务号获取任务明细
                this.detailListView.Columns.Clear();
                detailListView.Columns.Add("托盘号", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("任务数", 70, HorizontalAlignment.Left);
                detailListView.Columns.Add("已采数", 70, HorizontalAlignment.Left);
                detailListView.Columns.Add("库位", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("凭证号", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("入库单号", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("来源单号", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("任务号", 120, HorizontalAlignment.Left);
                //detailListView.Columns.Add("任务数", 70, HorizontalAlignment.Left);
                //detailListView.Columns.Add("已采数", 70, HorizontalAlignment.Left);
                detailListView.Columns.Add("托盘状态", 70, HorizontalAlignment.Left);
                detailListView.Columns.Add("工位", 120, HorizontalAlignment.Left);

                DataSet ds = service.GetInTaskPalletNo(User.Instance.UserData.UserId, taskNo, taskComment, "1", finishFlag, workStation);
                DataTable dt = ds.Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    detailListView.Items.Add(new ListViewItem(
                    new string[] { dr[3].ToString(), dr[6].ToString(), dr[7].ToString(), dr[4].ToString(), dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[5].ToString(), dr[8].ToString(), dr[9].ToString() }));
                }
                DataSet InOutds = service.GetInOutLocation("2");
                DataTable InOutdt = InOutds.Tables[0];
                INOUTComboBox.DataSource = InOutdt;

                currStep = Step.TrayNo;
                tbxBarcode.Focus();
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        /// <summary>
        /// 扫描条码
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
        /// 处理采集信息
        /// </summary>
        /// <param name="barcode"></param>
        private void PerformingBarcode(string barcode)
        {
            if (string.IsNullOrEmpty(barcode)) throw new Exception("采集内容不能为空");

            #region  判断模式
            
            if (barcode.StartsWith("$TP$"))//采集托盘信息
            {
                currStep = Step.TrayNo;
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
                    trayNo = barcode.Substring(4);
                    CheckTrayNo(trayNo);
                    trayLabel.Text = trayNo;                    
                    qtyLabel.Text = "1";
                    break;
                case Step.Quantity:
                    if (!trayNo.Equals(string.Empty))
                    {
                        throw new Exception("已采集托盘号无需采集数量，请扫描托盘号条码");
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
        /// 校验托盘号
        /// </summary>
        /// <param name="trayNo"></param>
        private decimal CheckTray(string trayNo)
        {
            if (string.IsNullOrEmpty(trayNo)) throw new Exception("托盘号不能为空");
            return service.CheckDownTray(trayNo);
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
        /// 校验采集托盘号
        /// </summary>
        /// <param name="palletNo"></param>
        private void CheckTrayNo(string palletNo)
        {
            string tmpTrayNo = string.Empty;
            for (int i = 0; i < detailListView.Items.Count; i++)
            {
                tmpTrayNo = detailListView.Items[i].SubItems[0].Text.Trim();

                if (tmpTrayNo.Equals(palletNo))
                {
                    return;
                }
            }
            throw new Exception(string.Format("任务明细中不存在托盘号【{0}】", palletNo));
        }

        /// <summary>
        /// 回填数量，更新LISTVIEW;添加采集记录集
        /// </summary>
        /// <param name="barcode"></param>
        private void DealQuantity(decimal collectQty, string matFlag)
        {
            #region 变量及校验

            if (collectQty <= 0) throw new Exception("采集数量必须大于0");
            decimal taskQty = 0;
            decimal tmpQty = 0;

            bool exsitFlag = false;

            #endregion

            #region 统计当前托盘总扫描数和总计划数
            decimal tatalTaskQty = 0;//当前托盘总计划数
            decimal tatalTmpQty = 0;//当前托盘总扫描数
            string tmptrayNo = string.Empty;
            string tmpSite = string.Empty;

            for (int i = 0; i < detailListView.Items.Count; i++)
            {
                tmptrayNo = detailListView.Items[i].SubItems[0].Text.Trim();//托盘
                if (tmptrayNo != trayNo) continue;//如果托盘不是当前输入的托盘 继续                

                taskQty = Convert.ToDecimal(detailListView.Items[i].SubItems[1].Text.Trim());
                tmpQty = Convert.ToDecimal(detailListView.Items[i].SubItems[2].Text.Trim());
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
            Dictionary<string, List<decimal>> dicMtlOperatin = new Dictionary<string, List<decimal>>();

            for (int i = 0; i < detailListView.Items.Count; i++)
            {
                #region 校验
                if (decQty <= 0) break;
                tmptrayNo = detailListView.Items[i].SubItems[0].Text.Trim();//托盘                           
                taskQty = Convert.ToDecimal(detailListView.Items[i].SubItems[1].Text.Trim());
                tmpQty = Convert.ToDecimal(detailListView.Items[i].SubItems[2].Text.Trim());

                if (tmptrayNo != trayNo) continue;//如果物料不是当前输入的物料 继续

                if (taskQty == tmpQty) continue;

                #endregion

                #region 计算使用明细的使用量 处理
                storeSite = detailListView.Items[i].SubItems[3].Text.Trim();
                dicMtlOperatin.Add(trayNo, new List<decimal>());
                dicMtlOperatin[trayNo].Add(taskQty);//第一笔存托盘计划数
                if (!dicMtlQty.ContainsKey(trayNo))
                {
                    ls = new List<string>();
                    ls.Add(tmpQty.ToString());
                    ls.Add("0");
                    ls.Add(trayNo);
                    dicMtlQty.Add(trayNo, ls);
                }

                if ((taskQty - tmpQty) >= decQty)//表示足够扣
                {
                    detailListView.Items[i].SubItems[2].Text = Convert.ToString(tmpQty + decQty);
                    dicMtlQty[trayNo][1] = (tmpQty + decQty).ToString();
                    dicMtlOperatin[trayNo].Add(decQty);
                    decQty = 0;
                    exsitFlag = true;
                }
                else
                {
                    decQty = decQty - (taskQty - tmpQty);//本次扫描数量- 计划剩余数量
                    detailListView.Items[i].SubItems[2].Text = taskQty.ToString();
                    dicMtlQty[trayNo][1] = taskQty.ToString();
                    dicMtlOperatin[trayNo].Add(taskQty - tmpQty);
                }
                #endregion
            }

            if (!exsitFlag) throw new Exception("采集托盘号信息匹配任务明细失败");


            if (!string.IsNullOrEmpty(trayNo) && !dicSeq.ContainsKey(trayNo))
            {
                dicSeq.Add(trayNo, trayNo);
            }

            if (!dicInvMtlQty.ContainsKey(trayNo))
            {
                dicInvMtlQty.Add(trayNo, collectQty);
            }
            else
            {
                dicInvMtlQty[trayNo] += collectQty;
            }
            #endregion
            
            //添加采集记录;对于采集记录的修改操作统一在采集明细中操作 
            BindingTrayCollectData.Instance.AddCollectData("0.0000.0000", "0.0000.0000", "", collectQty, storeSite, trayNo, dicMtlOperatin);            
        }

        /// <summary>
        /// 重新初始采集 
        /// </summary>
        private void InitializeCollect()
        {
            tbxBarcode.Text = "";
            tbxBarcode.Focus();

            qtyLabel.Text = "";

        }

        /// <summary>
        /// 上架提交获取目标库位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void submitUpButton_Click(object sender, EventArgs e)
        {
            try
            {
                #region 校验是否有未完成的物料
                if (BindingTrayCollectData.Instance.Collect.Count == 0)
                {
                    throw new Exception("本次无采集明细，请确认！");
                }

                string tmpMat = string.Empty;
                decimal taskQty = 0;
                decimal tmpQty = 0;
                string msg = string.Empty;
                string tmpStore = string.Empty;
                string tmpTrayNo = string.Empty;
                string startAddr = string.Empty;
                string endAddr = string.Empty;                

                for (int ii = 0; ii < detailListView.Items.Count; ii++)
                {
                    taskQty = Convert.ToDecimal(detailListView.Items[ii].SubItems[1].Text.Trim());
                    tmpQty = Convert.ToDecimal(detailListView.Items[ii].SubItems[2].Text.Trim());
                    tmpTrayNo = detailListView.Items[ii].SubItems[0].Text.Trim();

                    if ((taskQty != tmpQty) && (tmpTrayNo == trayNo))
                    {
                        msg += string.Format("托盘号【{0}】未采集", tmpTrayNo, (taskQty - tmpQty));
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(msg))
                {
                    msg = msg.Remove(0, 1) + "，请确认是否提交？";
                    if (MessageBox.Show(msg,
                            "托盘上架采集", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                    {
                        return;
                    }
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
                    trayInfo.TrayNo = tray.TrayNo;                     
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

                service.CommitTrayUpShelves(trayInfos, User.Instance.UserData.UserId, lsItems, taskNo);                

                BindingTrayCollectData.Instance.Collect = new List<BindingTray>();
                Message.Alarm("成功", "组盘数据提交成功");
                //this.Close();
                dicMtlQty.Clear();

                service.CommitUpWmsToWcs(User.Instance.UserData.UserId, taskId, taskNo, trayNo, startAddr, endAddr);
                Message.Alarm("成功", "托盘上架提交成功");
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        /// <summary>
        /// 修改采集目标库位数据
        /// </summary>
        /// <param name="targetSite"></param>
        private void UpdateCollect(string targetSite)
        {
            for (int i = 0;i< BindingTrayCollectData.Instance.Collect.Count;i++ )
            {
                if (BindingTrayCollectData.Instance.Collect[i] != null)
                {
                    BindingTrayCollectData.Instance.Collect[i].StoreSite = targetSite;
                }
            }
        }

        /// <summary>
        /// 提交组盘数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void commitButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (BindingTrayCollectData.Instance.Collect.Count > 0) throw new Exception("采集数据未提交,不允许获取上架货位！");
                
                if (trayNo.Equals(string.Empty)) throw new Exception("托盘号为空，请确认");

                string startAddr = INOUTComboBox.SelectedValue.ToString();
                if (startAddr.Equals(string.Empty)) throw new Exception("入口位置不能为空");

                string targetSite = service.GetPalletTargetSite(trayNo,startAddr, 0, 0);

                siteLabel.Text = targetSite;
                Message.Alarm("成功", "获取上架货位成功");

            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        /// <summary>
        /// 提交数据前校验
        /// </summary>
        /// <param name="checkSite">是否校验库位</param>
        private void beforeCommit(bool checkSite)
        {
            if (trayNo.Equals(string.Empty))
            {
                throw new Exception("托盘号为空，请确认");
            }

            if (BindingTrayCollectData.Instance.Collect.Count == 0)
            {
                throw new Exception("未采集物料明细，请确认");
            }

        }

        private void collectItemButton_Click(object sender, EventArgs e)
        {
            try
            {
                TrayUpCollectDetailFrm detailFrm = new TrayUpCollectDetailFrm(storeRoom);
                detailFrm.ShowDialog();
                UpdateListViewItem(detailFrm.dicUpdateInfo, detailFrm.dicDeleteSeq, detailFrm.dicDeleteInv);
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        /// <summary>
        /// 更新组盘采集任务主界面数据
        /// </summary>
        private void UpdateListViewItem(Dictionary<string, string[]> dicUpdateInfo, Dictionary<string, string> dicDeleteSeq, Dictionary<string, decimal> dicDeleteInv)
        {
            string inTaskItemid =string.Empty;
            string mtlCode = string.Empty;
            for (int i = 0; i < detailListView.Items.Count; i++)
            {
                inTaskItemid = detailListView.Items[i].SubItems[0].Text.Trim();
                mtlCode = detailListView.Items[i].SubItems[0].Text.Trim();//物料
                if (dicUpdateInfo.ContainsKey(inTaskItemid))
                {
                    string[] updateInfo = dicUpdateInfo[inTaskItemid];
                    if (updateInfo[0] == string.Empty)
                    {
                        detailListView.Items[i].SubItems[3].Text = updateInfo[1];
                    }
                    else
                    {
                        decimal dec = Convert.ToDecimal(dicMtlQty[inTaskItemid][1]);
                        dicMtlQty[inTaskItemid][1] = (dec - Convert.ToDecimal(updateInfo[0])).ToString();

                        detailListView.Items[i].SubItems[2].Text = dicMtlQty[inTaskItemid][1];
                        
                    }
                }
            }

            foreach (string del in dicDeleteSeq.Values)
            {
                if (dicSeq.ContainsKey(del))
                    dicSeq.Remove(del);
            }

            //处理库存
            foreach (KeyValuePair<string, decimal> inv in dicDeleteInv)
            {
                if (dicInvMtlQty.ContainsKey(inv.Key))
                    dicInvMtlQty[inv.Key] -= inv.Value;
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

        private void closeButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("当前采集数量是{0},是否确认关闭？", BindingTrayCollectData.Instance.Collect.Count),
                  "组盘采集", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                  MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }
            this.Close();
        }

        enum Step
        {
            TrayNo,Quantity
        }

        private void exceptButton_Click(object sender, EventArgs e)
        {
            List<BindingTray> stocks = BindingTrayCollectData.Instance.Collect;

            if (stocks.Count > 0)
            {
                MessageBox.Show("采集数据未提交,不允许异常登记！");
                return;
            }

            ExceptTaskFrm frm = new ExceptTaskFrm(taskComment, taskNo, taskId, "组盘上架", storeRoom, trayNo);
            frm.ShowDialog();
        }

        private void WCSButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("请确认组盘完成，准备托盘上架？",
                      "托盘上架", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                      MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    return;
                }
                if (BindingTrayCollectData.Instance.Collect.Count > 0) throw new Exception("采集数据未提交,不允许托盘上架！");
                if (trayNo.Equals(string.Empty)) throw new Exception("托盘号为空，请确认");
                if (taskNo.Equals(string.Empty)) throw new Exception("凭证号为空，请确认");

                string startAddr = string.Empty;
                string endAddr = string.Empty;

                service.CommitUpWmsToWcs(User.Instance.UserData.UserId, taskId, taskNo, trayNo, startAddr, endAddr);
                Message.Alarm("成功", "托盘上架提交成功");
                
            }

            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        private void WmsToWcsBN_Click(object sender, EventArgs e)
        {
            List<BindingTray> stocks = BindingTrayCollectData.Instance.Collect;

            if (stocks.Count > 0)
            {
                MessageBox.Show("采集数据未提交,不允许查看指令！");
                return;
            }

            ASWHWmsToWcs frm = new ASWHWmsToWcs(taskComment, taskId, "01");
            frm.ShowDialog();
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

            TrayRepertory frm = new TrayRepertory(taskComment, taskId, trayNo, User.Instance.UserData.UserId);
            frm.ShowDialog();
        } 
    }
}