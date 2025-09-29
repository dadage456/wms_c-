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
    public partial class ASWHOutTaskItemFrm : Form
    {
        public ASWHOutTaskItemFrm(string taskNo, string taskId, string storeRoom, string siteFlag, string batchFlag, string taskComment, string orderNo, string taskFinishFlag, string workStation)
        {
            InitializeComponent();
            this.taskNo = taskNo ;
            this.taskId = taskId;
            this.storeRoom = storeRoom;
            this.siteFlag = siteFlag;
            this.batchFlag = batchFlag;
            this.finishFlag = taskFinishFlag;            
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

        MiddleService service = new MiddleService();
        Management management = Management.GetSingleton();
        DataTable roomTable = new DataTable();

        private string trayNo = string.Empty;
        private Step currStep;
        
        private string storeSite = string.Empty;
        private string erpStoreSite = string.Empty;//ERP子库
        private string erpRoom = string.Empty;//ERP子库
        private Dictionary<string, List<string>> dicMtlQty = new Dictionary<string, List<string>>();//key: outTaskItemid value: 0:开始采集数  1：本次数量
        private Dictionary<string, decimal> dicInvMtlQty = new Dictionary<string, decimal>();//Key：库位+物料+批次 ||  库位+序列  Value：本次作业数量
        private Dictionary<string, string> dicSeq = new Dictionary<string, string>();
        private Dictionary<string, string> dicPalletNo = new Dictionary<string, string>();       
        private string erpStoreInv = string.Empty;//库存erp子库
        
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownTaskItemFrm_Load(object sender, EventArgs e)
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
                this.detailListView.Columns.Clear();
                detailListView.Columns.Add("托盘号", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("任务数", 70, HorizontalAlignment.Left);
                detailListView.Columns.Add("已采数", 70, HorizontalAlignment.Left);
                detailListView.Columns.Add("库位", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("凭证号", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("出库单号", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("来源单号", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("任务号", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("托盘状态", 70, HorizontalAlignment.Left);
                detailListView.Columns.Add("工位", 120, HorizontalAlignment.Left);

                DataSet ds = service.GetOutTaskPalletNo(User.Instance.UserData.UserId, taskNo, taskComment, "1", finishFlag, workStation);
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    detailListView.Items.Add(new ListViewItem(
                    new string[] { dr[3].ToString(), dr[6].ToString(), dr[7].ToString(), dr[4].ToString(), dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[5].ToString(), dr[8].ToString(), dr[9].ToString() }));
                }

                DataSet InOutds = service.GetInOutLocation("0");
                DataTable InOutdt = InOutds.Tables[0];
                INOUTComboBox.DataSource = InOutdt;

                #region 设定参数
                
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
            qtyLabel.Text = "";
            
        }

        /// <summary>
        /// 扫描条码执行
        /// </summary>
        /// <param name="barcode"></param>
        private void PerformingBarcode(string barcode)
        {
            if (string.IsNullOrEmpty(barcode)) throw new Exception("采集凭证号不能为空");
         
            #region  判断模式           
            
            if (barcode.StartsWith("$TP$"))//采集托盘信息
            {
                currStep = Step.TrayNo;
            }
            else
            {
                throw new Exception(setMsg("采集内容不合法,"));
            }
            #endregion

            #region 条码校验
            switch (currStep)
            {                
                //托盘
                case Step.TrayNo:
                    decimal decTrayCapacity = CheckTray(barcode.Substring(4));// "" 、 TP 、 000002                    
                    trayNo = barcode.Substring(4);
                    CheckTrayNo(trayNo);
                    trayLabel.Text = trayNo;
                    siteLabel.Text = "";
                    for (int i = 0; i < detailListView.Items.Count; i++)
                    {                        
                        string tmpTrayNo = detailListView.Items[i].SubItems[0].Text.Trim();
                        storeSite = detailListView.Items[i].SubItems[3].Text.Trim();
                        if (tmpTrayNo == trayNo)
                        {
                            siteLabel.Text = storeSite;
                            break;
                        }
                    }
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
                DealQuantity(Convert.ToDecimal(qtyLabel.Text.Trim()), "0");
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
            if (trayLabel.Text.Trim().Equals(""))//库位为空 采集库位
            {
                return string.Format("{0}请采集托盘号", msg);
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
        /// 回填数量，更新LISTVIEW(结合强制库位强制批次标识)
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
            ASWHDownCollectData.Instance.AddCollectData("0.0000.0000", "0.0000.0000", "", collectQty, storeRoom, storeSite, dicMtlOperatin, erpStoreInv, "0.0000.0000", trayNo);
        
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
                ASWHOutCollectDetailFrm downDetailFrm = new ASWHOutCollectDetailFrm();
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
                string strTrayNo = detailListView.Items[i].SubItems[0].Text.Trim();//物料              
                if (dicUpdateInfo.ContainsKey(strTrayNo))
                {
                    string[] updateInfo = dicUpdateInfo[strTrayNo];
                    if (updateInfo[0] == string.Empty)
                    {
                        detailListView.Items[i].SubItems[3].Text = updateInfo[1];
                    }
                    else
                    {
                        decimal dec = Convert.ToDecimal(dicMtlQty[strTrayNo][1]);
                        dicMtlQty[strTrayNo][1] = (dec - Convert.ToDecimal(updateInfo[0])).ToString();
                        detailListView.Items[i].SubItems[2].Text = dicMtlQty[strTrayNo][1];
                        
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
                if (ASWHDownCollectData.Instance.Collect.Count == 0)
                {
                    throw new Exception("本次无采集明细，请确认！");
                }

                string tmpMat = string.Empty;
                decimal taskQty = 0;
                decimal tmpQty = 0;
                string msg = string.Empty;
                string tmpStore = string.Empty;
                string tmpTrayNo = string.Empty;

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
                            "整盘出库采集", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                    {
                        return;
                    }
                }
                #endregion

                int i = 0;
                List<Stock> collectStocks = ASWHDownCollectData.Instance.Collect;
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
                    //CheckSite(stock.StoreRoom, stock.StoreSite);
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
                service.CommitASWHPalletNoDownShelves(downShelvesInfos, User.Instance.UserData.UserId, lsItems);                
                ASWHDownCollectData.Instance.Collect = new List<Stock>();
                dicMtlQty.Clear();
                Message.Alarm("成功", "提交成功");
                //this.Close();
                
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("当前采集数量是{0},是否确认关闭？", ASWHDownCollectData.Instance.Collect.Count),
              "整盘出库采集", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
              MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }

            ASWHDownCollectData.Instance.Collect = new List<Stock>();
            this.Close(); 
        }
          

        enum Step
        {
            _2DBarcode, Site, Quantity, TrayNo
        }

        private void exceptButton_Click(object sender, EventArgs e)
        {
            List<Stock> stocks = ASWHDownCollectData.Instance.Collect;

            if (stocks.Count > 0)
            {
                MessageBox.Show("采集数据未提交,不允许异常登记！");
                return;
            }

            ExceptTaskFrm frm = new ExceptTaskFrm(taskComment, taskNo, taskId, "整盘出库", storeRoom, trayNo);
            frm.ShowDialog();
        }

        private void WmsToWcsBN_Click(object sender, EventArgs e)
        {
            List<Stock> stocks = ASWHDownCollectData.Instance.Collect;

            if (stocks.Count > 0)
            {
                MessageBox.Show("采集数据未提交,不允许查看指令！");
                return;
            }

            ASWHWmsToWcs frm = new ASWHWmsToWcs(taskComment, taskId,"11");
            frm.ShowDialog();
        }

        private void WCSButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("请确认获取来料托盘吗？",
                      "整盘出库", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                      MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    return;
                }
                if (ASWHDownCollectData.Instance.Collect.Count > 0) throw new Exception("采集数据未提交,不允许获取新的来料盘！");
                if (taskNo.Equals(string.Empty)) throw new Exception("凭证号为空，请确认");

                if (INOUTComboBox.SelectedIndex == -1) throw new Exception("拣选口位置不能为空");

                string endAddr = INOUTComboBox.SelectedValue.ToString();
                if (endAddr.Equals(string.Empty)) throw new Exception("整盘出库口位置不能为空");

                if (string.IsNullOrEmpty(PalletNum.Text.Trim())) throw new Exception("获取来料盘数据不能为空！");

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
                        sourcetrayNo = detailListView.Items[i].SubItems[0].Text.Trim();
                        startAddr = detailListView.Items[i].SubItems[3].Text.Trim();
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

        private void SingleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("请确认获取来料托盘吗？",
                      "整盘出库", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                      MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    return;
                }
                if (ASWHDownCollectData.Instance.Collect.Count > 0) throw new Exception("采集数据未提交,不允许获取新的来料盘！");
                if (taskNo.Equals(string.Empty)) throw new Exception("凭证号为空，请确认");

                if (INOUTComboBox.SelectedIndex == -1) throw new Exception("拣选口位置不能为空");

                string endAddr = INOUTComboBox.SelectedValue.ToString();
                if (endAddr.Equals(string.Empty)) throw new Exception("出库口位置不能为空");

                string sourcetrayNo = string.Empty;
                string startAddr = string.Empty;
                int j = detailListView.Items.Count - 1;
                for (int i = j; i >= 0; i--)
                {
                    if (detailListView.Items[i].Selected)
                    {
                        sourcetrayNo = detailListView.Items[i].SubItems[0].Text.Trim();
                        startAddr = detailListView.Items[i].SubItems[3].Text.Trim();
                    }
                }
                service.CommitDownWmsToWcs(User.Instance.UserData.UserId, taskId, taskNo, sourcetrayNo, startAddr, endAddr,"1");

                Message.Alarm("成功", "获取来料盘成功,请等待");
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }
    }
}