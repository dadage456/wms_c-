using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BizLayer;
using Entity;
using BizLayer.WebService; 

namespace PDA
{
    public partial class PInventoryFrm : Form
    {
        public PInventoryFrm()
        {
            InitializeComponent(); 

        }

        Step currStep;
        List<InventoryData> totalInfos = new List<InventoryData>(); 
        Management management = Management.GetSingleton();
        MiddleService service = new MiddleService();
        DataTable siteTable = new DataTable();
        private string billNo = string.Empty;
        private string storeRoomCode = string.Empty; 

        private void MaterialInventoryFrm_Load(object sender, EventArgs e)
        {
            Initialize();
            barcodeTextBox.Focus();
        }

        private void Initialize()
        {
            siteCheckBox.Checked = false;
            matCheckBox.Checked = false;

            billNoLabel.Text = "";
            roomNoLabel.Text = "";
            siteLlabel.Text = "";
            materialLabel.Text = "";
            batchNoLabel.Text = "";
            snLabel.Text = ""; 
            quantityLabel.Text = "";

            barcodeTextBox.Text = "";
            barcodeTextBox.Focus();

            detailListView.Items.Clear();

            totalInfos = new List<InventoryData>(); 
        }

        /// <summary>
        /// 采集事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barcodeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Return)
                {
                    string barcode = this.barcodeTextBox.Text.Trim();
                    if (string.IsNullOrEmpty(barcode))
                    {
                        this.barcodeTextBox.Text = "";
                        this.barcodeTextBox.Focus();
                        this.barcodeTextBox.SelectAll();
                        return;
                    }

                    if (string.IsNullOrEmpty(billNo))
                    {
                        CollectBillNo(barcode);
                    }
                    else
                    {
                        if (siteCheckBox.Checked && matCheckBox.Checked) throw new Exception("按库位/按物料盘点方式不能同时勾选");
                        if (!siteCheckBox.Checked && !matCheckBox.Checked) throw new Exception("按库位/按物料盘点方式必须勾选一项");
                        //定库位盘点，采集库位标签$STATION$；定物料盘点，采集第一个二维码解析物料
                        if (siteCheckBox.Checked)
                        {
                            PerformingSite(barcode);
                        }

                        if (matCheckBox.Checked)
                        {
                            PerformingMat(barcode);
                        }
                    }


                    
                }
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
                barcodeTextBox.Focus();
                barcodeTextBox.SelectAll();
            }
        }

        /// <summary>
        /// 采集盘库单号
        /// </summary>
        private void CollectBillNo(string barcode)
        {
            //billNo = barcode;
            //InventoryInfo info = service.GetCheckMethod(barcode);
            
            //billNoLabel.Text = info.BillNo;
            //roomNoLabel.Text = info.StoreRoomNo;
            //storeRoomCode = info.StoreRoomNo;
            //siteTable = service.GetStoreSiteByRoom(storeRoomCode).Tables[0];    //根据库房获取该库房下的所有库位
            //if (siteTable.Rows.Count == 0 || siteTable == null)
            //    throw new Exception("库房"+storeRoomCode+"下未维护库位");
            //switch (info.CheckMethod)
            //{ 
            //    case 0 :
            //        siteCheckBox.Checked = true ;
            //        currStep = Step.Site;
            //        lblMsg.Text = "请采集库位：";
            //        siteLlabel.Text = "";
            //        materialLabel.Text = "";
            //        batchNoLabel.Text = "";
            //        snLabel.Text = ""; 
            //        quantityLabel.Text = "";
            //        barcodeTextBox.Text = "";
            //        barcodeTextBox.Focus();
            //        break;
            //    case 1 :
            //    case 2:
            //        matCheckBox.Checked=true;
            //        currStep = Step._2DBarcode;
            //        lblMsg.Text = "请采集料号：";
            //        siteLlabel.Text = "";
            //        materialLabel.Text = "";
            //        batchNoLabel.Text = "";
            //        snLabel.Text = "";
            //        quantityLabel.Text = "";
            //        barcodeTextBox.Text = "";
            //        barcodeTextBox.Focus();
            //        break;
            //    default :
            //        throw new Exception("盘点方式不在举值范围内");
            //} 
        }

        /// <summary>
        /// 按库位采集
        /// </summary>
        /// <param name="barcode"></param> 
        private void PerformingSite(string barcode)
        {
            try
            {
                if (string.IsNullOrEmpty(barcode)) throw new Exception("采集内容不能为空"); 
                switch (currStep)
                {
                    case Step.Site:
                        CollectSite(barcode); 
                        break;
                    case Step._2DBarcode:
                        if (barcode.StartsWith("$STATION$") && barcode.Length > 9)
                        {
                            CollectSite(barcode);
                            materialLabel.Text = "";
                            break;
                        }
                         
                        if (barcode.IndexOf('*') < 0) throw new Exception("采集内容不合法，请采集二维码");
                        BarcodeContent barcodeContent = service.AnalysisForNewBarcode(barcode);
                        bool ISNEWEWM = false; if (barcode.IndexOf("*BN") >= 0) { ISNEWEWM = true; }
                        string matCode = barcodeContent.MatCode;
                        materialLabel.Text = matCode; 
                        //根据物料属性判断，该物料对应的编码控制    0单件(序列)控制，1批次控制，2无控制
                        int matControl = service.GetMatControl(matCode);
                        string matControlFlag = matControl.ToString();
                        if (matControl == 0)
                        {
                            batchNoLabel.Text = ISNEWEWM ? barcodeContent.BatchNo : string.Empty;
                            string sn = barcodeContent.SN;
                            snLabel.Text = sn;
                        }
                        else if (matControl == 1)
                        {
                            string batchNo = ISNEWEWM ? barcodeContent.BatchNo : barcodeContent.SN;
                            batchNoLabel.Text = batchNo;
                        }
                        else
                        {
                            throw new Exception("物料" + matCode + "编码控制维护值维护不合法");
                        }
                        currStep = Step.Quantity; 
                        break;
                    case Step.Quantity:
                        if (!management.CheckQuantity(barcode.ToString())) 
                            throw new Exception("请输入合法的数量"); 
                        DealQuantity(Convert.ToDecimal(barcode)); 
                        currStep = Step._2DBarcode;
                        lblMsg.Text = "请采集二维码:"; 
                        materialLabel.Text = ""; 
                        break;
                    default:
                        break;
                }
                 
                barcodeTextBox.Text = "";
                barcodeTextBox.Focus();
                barcodeTextBox.SelectAll(); 
            }
            catch (Exception e)
            {
                Message.Alarm("提示", e.Message);
                barcodeTextBox.Focus();
                barcodeTextBox.SelectAll();
            }
        }

        /// <summary>
        /// 采集库位条码
        /// </summary>
        private void CollectSite(string barcode)
        {
            ChecSite(barcode);
            currStep = Step._2DBarcode;
            lblMsg.Text = "请采集二维码：";
        }

        /// <summary>
        /// 按物料采集
        /// </summary>
        /// <param name="barcode"></param>
        private void PerformingMat(string barcode)
        {
            try
            {
                if (string.IsNullOrEmpty(barcode)) throw new Exception("采集内容不能为空");  

                switch (currStep)
                {
                    case Step._2DBarcode: 
                        if (barcode.IndexOf('*') < 0) throw new Exception("采集内容不合法，请采集二维码");
                        BarcodeContent barcodeContent = service.AnalysisForNewBarcode(barcode);
                        bool ISNEWEWM = false; if (barcode.IndexOf("*BN") >= 0) { ISNEWEWM = true; }
                        string matCode = barcodeContent.MatCode;
                        if (string.IsNullOrEmpty(materialLabel.Text))
                        {
                            materialLabel.Text = matCode;
                        }
                        else
                        {
                            if (!materialLabel.Text.Equals(matCode))
                            { 
                                 DialogResult re = MessageBox.Show("采集物料已经改变，是否切换成当前采集物料？", "Warning",
                                       MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                                 if (re == DialogResult.No)
                                 {
                                     barcodeTextBox.Text = "";
                                     barcodeTextBox.Focus();
                                     barcodeTextBox.SelectAll();
                                     return;
                                 }
                            }
                        }
                        
                        //根据物料属性判断，该物料对应的编码控制    0单件(序列)控制，1批次控制，2无控制
                        int matControl = service.GetMatControl(matCode);
                        string matControlFlag = matControl.ToString();
                        if (matControl == 0)
                        {
                            string sn = barcodeContent.SN;
                            snLabel.Text = sn;
                        }
                        else if (matControl == 1)
                        {
                            string batchNo = (ISNEWEWM ? barcodeContent.BatchNo : barcodeContent.SN);
                            batchNoLabel.Text = batchNo;
                        }
                        else
                        {
                            throw new Exception("物料" + matCode + "编码控制维护值维护不合法");
                        }
                        currStep = Step.Site;  
                        lblMsg.Text = "请采集库房/库位："; 

                        break;
                    case Step.Site:
                        if (barcode.StartsWith("$STATION$"))
                        {
                            siteLlabel.Text = barcode.Substring(9); 
                        } 
                        else
                        {
                            throw new Exception("请采集库房/库位");
                        }
                          
                        currStep = Step.Quantity;
                        lblMsg.Text = "请输入数量："; 

                        break; 
                    case Step.Quantity:
                        if (!management.CheckQuantity(barcode.ToString()))
                        {
                            throw new Exception( "请输入合法的数量"); 
                        }
                        DealQuantity(Convert.ToDecimal(barcode));

                        currStep = Step._2DBarcode;
                        lblMsg.Text = "请采集库房/库位："; 
                        siteLlabel.Text = "";
                        break;
                    default:
                        break;
                }

                barcodeTextBox.Text = "";
                barcodeTextBox.Focus();
                barcodeTextBox.SelectAll();
            }
            catch (Exception e)
            {
                Message.Alarm("提示", e.Message);
                barcodeTextBox.Focus();
                barcodeTextBox.SelectAll();
            }
        }

        /// <summary>
        /// 校验采集库位信息
        /// </summary>
        /// <param name="barcode"></param>
        private void ChecSite(string barcode)
        {
            if (barcode.StartsWith("$STATION$") && barcode.Length > 9)
            {
                DataRow[] siteDr = siteTable.Select("storesiteno= '" + barcode + "'");
                if (siteDr.Length <= 0) throw new Exception("库房" + roomNoLabel.Text + "下无库位号" + barcode);
                siteLlabel.Text = barcode.Substring(9); 
            }
            else
            {
                throw new Exception("请采集库位条码");
            }
        }

        /// <summary>
        /// 采集数量
        /// </summary>
        /// <param name="collectQuantity"></param>
        private void DealQuantity(decimal collectQuantity)
        {
            try
            {
                bool hasFlag = false;
                foreach (InventoryData info in totalInfos)
                {
                    if (info.MatCode == materialLabel.Text.Trim() && info.StoreSite == siteLlabel.Text.Trim() &&
                        (info.BatchNo == batchNoLabel.Text.Trim() || info.Sn == snLabel.Text.Trim()))
                    {
                        hasFlag = true;
                        info.InventoryQty += collectQuantity;
                        break;
                    }
                }

                if (!hasFlag)
                {
                    InventoryData newInfo = new InventoryData();
                    newInfo.MatCode = materialLabel.Text.Trim();
                    newInfo.BatchNo = batchNoLabel.Text.Trim();
                    newInfo.Sn = snLabel.Text.Trim();
                    newInfo.StoreSite = siteLlabel.Text.Trim();
                    newInfo.InventoryQty = collectQuantity;

                    totalInfos.Add(newInfo);
                }
                 
                UpdateListView(collectQuantity); 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 更新控件显示内容
        /// </summary>
        /// <param name="collectQty"></param>
        private void UpdateListView(decimal collectQty)
        {
            bool hasFlag = false;
            for (int i = 0; i < detailListView.Items.Count; i++)
            {
                //料号 批号 序列号 库位 盘点数
                if (detailListView.Items[i].SubItems[0].Text == materialLabel.Text &&
                    (detailListView.Items[i].SubItems[1].Text == batchNoLabel.Text || detailListView.Items[i].SubItems[2].Text == snLabel.Text) &&
                    detailListView.Items[i].SubItems[3].Text == siteLlabel.Text)
                { 
                    //已采集数
                    detailListView.Items[i].SubItems[4].Text = Convert.ToString(Convert.ToDecimal(detailListView.Items[i].SubItems[4].Text) + collectQty);
                    hasFlag = true;
                    break;
                }
            }

            if(!hasFlag)
                detailListView.Items.Add(new ListViewItem(
                    new string[] { materialLabel.Text, batchNoLabel.Text, snLabel.Text, siteLlabel.Text, collectQty.ToString() }));
        }
         
        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (detailListView.SelectedIndices == null || detailListView.SelectedIndices.Count == 0) return;
            DialogResult re = MessageBox.Show("你确定要删除该条数据吗？", "提示", MessageBoxButtons.OKCancel,
               MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
            if (re == DialogResult.Cancel)
                return;

            int index = detailListView.SelectedIndices[0];
            //料号 批号 序列号 库位 盘点数
            InventoryData info = new InventoryData();
            info.MatCode = detailListView.Items[index].SubItems[0].Text.Trim();
            info.BatchNo = detailListView.Items[index].SubItems[1].Text.Trim();
            info.Sn = detailListView.Items[index].SubItems[2].Text.Trim();
            info.StoreSite = detailListView.Items[index].SubItems[3].Text.Trim();
            DeleteInventorys(info.MatCode,info.BatchNo,info.Sn, info.StoreSite);
            detailListView.Items.RemoveAt(index); 
        }

        private void DeleteInventorys(string material,string batch,string sn, string site)
        {
            for (int j = totalInfos.Count - 1; j >= 0; j--)
            {
                if (totalInfos[j].MatCode == material && totalInfos[j].StoreSite == site &&
                    (totalInfos[j].BatchNo == batch || totalInfos[j].Sn == sn))
                {
                    totalInfos.RemoveAt(j);
                    return;
                }
            }
        }

        /// <summary>
        /// 重置按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resetButton_Click(object sender, EventArgs e)
        {
            billNoLabel.Text = "";
            roomNoLabel.Text = "";
            siteLlabel.Text = "";
            materialLabel.Text = "";
            batchNoLabel.Text = "";
            snLabel.Text = "";
            quantityLabel.Text = "";

            barcodeTextBox.Text = "";
            barcodeTextBox.Focus(); 
        }

          
        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void commitButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (totalInfos.Count == 0) throw new Exception("该任务还没有任何采集数据，无法提交！");
                DialogResult re = MessageBox.Show("你确定要提交吗？", "Warning",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (re == DialogResult.No) return;
                    
                Cursor.Current = Cursors.WaitCursor;
                int i = 0; 
                InventoryInfo[] infos = new InventoryInfo[totalInfos.Count];
                foreach (InventoryData data in totalInfos)
                {
                    InventoryInfo info = new InventoryInfo();
                    i++;
                }
                service.CommitInventoryInfos(billNo, infos, User.Instance.UserData.UserCode);
                Cursor.Current = Cursors.Default;
                Message.Alarm("Success", "提交成功！");
                Initialize();
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Message.Alarm("提示", ex.Message);
                return;
            } 
        }
         

        /// <summary>
        /// 关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeButton_Click(object sender, EventArgs e)
        {
            DialogResult re = MessageBox.Show("你确定要放弃采集吗？", "Warning",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (re == DialogResult.No)
                return;
            Close();
        }

        enum Step
        {
            Site, _2DBarcode, Quantity
        }

        
    }
}