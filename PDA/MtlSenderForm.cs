using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BizLayer;
using System.IO;
using Entity;
using BizLayer.WebService;

namespace PDA
{
    public partial class MtlSenderForm : Form
    {
        public MtlSenderForm()
        {
            InitializeComponent();
        }

        #region 变量
        Management management = Management.GetSingleton(); 
        MiddleService service = new MiddleService();
        private string taskNo = string.Empty;
        private string taskComment = string.Empty;
        private string storeRoom = string.Empty;
        private string siteFlag = string.Empty;
        private string batchFlag = string.Empty;

        
        DataTable roomTable = new DataTable();

        private string matCode = string.Empty;
        private string batchNo = string.Empty;
        private string sn = string.Empty;
        private string storeSite = string.Empty;
        private string matControlFlag = string.Empty;
        private string erpStoreSite = string.Empty;//ERP子库        
        private Dictionary<string, decimal> dicInvMtlQty = new Dictionary<string, decimal>();//Key：库位+物料+批次 ||  库位+序列  Value：本次作业数量
        private Dictionary<string, string> dicSeq = new Dictionary<string, string>();
       
        private string erpStoreInv = string.Empty;//库存erp子库
        //private string supplier = string.Empty;

        /// <summary>
        /// Key：货架号+物料号  Values：数量
        /// </summary>
        Dictionary<string, decimal> dicMtlInfo = new Dictionary<string, decimal>();

        /// <summary>
        /// Key：物料 Values：数量
        /// </summary>
        Dictionary<string, decimal> dicMtlQty = new Dictionary<string, decimal>();

        /// <summary>
        /// 作业类型
        /// </summary>
        enum Step
        {
           Barcode, Quantity, Location
        }
        
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TransferForm_Load(object sender, EventArgs e)
        {
            lblMsg.Text = "请扫描货架号：";
            tbxBarcode.Text = "";
            tbxBarcode.Focus();

            detailListView.Items.Clear();
        }

        /// <summary>
        /// 条码扫描
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                #region 按回车同时条码不为空
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
                #endregion
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
            #region  判断条码模式
            Step currStep;
            if (barcode.IndexOf('*') > 0)//物料条码
            {
                currStep = Step.Barcode;
            }
            else if (barcode.StartsWith("$KW$"))//货架号
            {
                currStep = Step.Location;
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
                //货架号
                case Step.Location:
                    locationLabel.Text = barcode.Substring(4);
                    break;
                //扫描条码
                case Step.Barcode:
                    //解析物料
                    BarcodeContent barcodeContent = service.AnalysisForNewBarcode(barcode);
                    //根据物料属性判断，该物料对应的编码控制    0单件(序列)控制，1批次控制，2无控制
                    service.GetMatControl(barcodeContent.MatCode);
                    matCodeLabel.Text = barcodeContent.MatCode;
                    checkInv(locationLabel.Text,barcodeContent.MatCode);
                    break;
                case Step.Quantity:
                    qtyLabel.Text = barcode;
                    break;
                default:
                    break;
            }

            string strMsg = setMsg(string.Empty);
            //表示条码都扫描完毕
            if (strMsg.Trim().Equals(string.Empty))
            {
                DealQuantity(Convert.ToDecimal(qtyLabel.Text.Trim()));
                InitializeCollect();
            }
            else
            {
                if (!string.IsNullOrEmpty(locationLabel.Text.Trim()) && !string.IsNullOrEmpty(matCodeLabel.Text.Trim()))
                {
                    DataTable dtMtlInfo = service.GetMtlQtyByMtlCode(matCodeLabel.Text.Trim(), locationLabel.Text.Trim()).Tables[0];
                    if (dtMtlInfo != null && dtMtlInfo.Rows.Count > 0)
                    {
                        lbMinQty.Text = dtMtlInfo.Rows[0]["MIN_QTY"].ToString();
                        lbDefaultQty.Text = dtMtlInfo.Rows[0]["DELIVERY_QTY"].ToString();
                    }
                    else
                    {
                        lbMinQty.Text = "0";
                        lbDefaultQty.Text = "0";
                    }
                }
            }
            lblMsg.Text = setMsg("");
            #endregion
        }


        /// <summary>
        /// 检验库存
        /// </summary>
        /// <param name="collectQty">当前数量</param>
        /// <returns></returns>
        private void checkInv(string siteNo, string matCode)
        {
            if (string.IsNullOrEmpty(matCode)) return;

            #region 校验库存
            DataTable dtRepertory = service.GetLSMtlRepertoryByStoresiteNo("", matCode).Tables[0];

            if (dtRepertory != null && dtRepertory.Rows.Count > 0)
            {
                lbInvQty.Text = dtRepertory.Rows[0]["repqty"].ToString();//显示库存数
            }
            else
            {
                lbInvQty.Text = "0";//显示库存数
            }
            #endregion
        }
        


        /// <summary>
        /// 设定提示信息
        /// </summary>
        /// <param name="msg"></param>
        private string setMsg(string msg)
        {
            if (string.IsNullOrEmpty(locationLabel.Text.Trim()))//货架号 
            {
                return string.Format("{0}请扫描货架号", msg);
            }
            else if (string.IsNullOrEmpty(matCodeLabel.Text.Trim()))//物料
            {
                return string.Format("{0}请扫描物料", msg);
            }
            else if (string.IsNullOrEmpty(qtyLabel.Text.Trim()))//数量
            {
                return string.Format("{0}请输入数量", msg);
            }
            else
            {
                return string.Format("{0}", msg);
            }

        }

        /// <summary>
        /// 添加采集记录集
        /// </summary>
        /// <param name="barcode"></param>
        private void DealQuantity(decimal qty)
        {
            #region 变量
            if (qty <= 0) throw new Exception("采集数量必须大于0");
            
            string strLocation = locationLabel.Text.Trim();
            string strMatCode = matCodeLabel.Text.Trim();
            string strKey = strMatCode + strLocation;
            
            decimal decQty=Convert.ToDecimal(lbInvQty.Text.Trim());//可用库存数量
            decimal oldQty = 0;//已经扫描数
            if (dicMtlQty.ContainsKey(strMatCode))
            {
                oldQty= dicMtlQty[strMatCode];
            }
            if ((decQty - oldQty) < qty)
            {
                if (oldQty > 0)
                    throw new Exception(string.Format("已经扫描数【{1}】加上本次扫描数量【{2}】大于库存数【{0}】，请确认", decQty, oldQty, qty));
                else
                    throw new Exception(string.Format("库存数【{0}】小于本次作业数量【{1}】，请确认", decQty, qty));
            }
            #endregion

            #region 处理数据
            if (!dicMtlInfo.ContainsKey(strKey))
            {
                dicMtlInfo.Add(strKey, qty);

                //不存在添加采集记录
                detailListView.Items.Add(new ListViewItem(new string[] { strLocation, strMatCode, qty.ToString() }));
            }
            else
            {
                #region 如果存在表示 已经扫描过此库位+物料，数量累计
                dicMtlInfo[strKey] += qty;
                string tmpLocation = string.Empty;
                string tmpMatCode = string.Empty;
                for (int i = 0; i < detailListView.Items.Count; i++)
                {
                    tmpLocation = detailListView.Items[i].SubItems[0].Text.Trim();//货架号
                    tmpMatCode = detailListView.Items[i].SubItems[1].Text.Trim();//物料号
                    if (tmpLocation.Equals(strLocation) && tmpMatCode.Equals(strMatCode))
                    {
                        detailListView.Items[i].SubItems[2].Text = dicMtlInfo[strKey].ToString();
                        break;
                    }
                }
                #endregion
            }

            //记录物料总数
            if (!dicMtlQty.ContainsKey(strMatCode))
            {
                dicMtlQty.Add(strMatCode, qty);
            }
            else
            {
                dicMtlQty[strMatCode] += qty;
            }
            #endregion
        }

        /// <summary>
        /// 重新初始采集 
        /// </summary>
        private void InitializeCollect()
        {
            tbxBarcode.Text = "";
            tbxBarcode.Focus();

            matCodeLabel.Text = "";
            qtyLabel.Text = "";

            lbMinQty.Text = "";
            lbInvQty.Text = "";
            lbDefaultQty.Text = "";
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void commitButton_Click(object sender, EventArgs e)
        {
            try
            {
                commitButton.Enabled = false;
                BeforeCommit();

                MtlSenderInfo[] mtlSenderInfos = new MtlSenderInfo[detailListView.Items.Count];   //托盘内货物信息
                for (int i = 0; i < detailListView.Items.Count; i++)
                {
                    MtlSenderInfo mtlSenderInfo = new MtlSenderInfo();
                    mtlSenderInfo.LocationNo = detailListView.Items[i].SubItems[0].Text.Trim();//货架号
                    mtlSenderInfo.MatCode = detailListView.Items[i].SubItems[1].Text.Trim();//物料号
                    mtlSenderInfo.Qty = detailListView.Items[i].SubItems[2].Text.Trim();//数量
                    mtlSenderInfos[i] = mtlSenderInfo;
                }
                service.CommitMtlSender(mtlSenderInfos, User.Instance.UserData.UserId);
                commitButton.Enabled = true;
                this.Close();
            }
            catch(Exception ex)
            {
                commitButton.Enabled = true;
                Message.Alarm("提示", ex.Message);
            }
        }

        /// <summary>
        /// 确认之前
        /// </summary>
        private void BeforeCommit()
        {
            if (detailListView.Items.Count == 0)
            {
                tbxBarcode.SelectAll();
                tbxBarcode.Focus();
                commitButton.Enabled = true;
                throw new Exception("物料明细不可以为空，请确认");
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("当前采集数量是{0},是否确认关闭？", detailListView.Items.Count),
"拉式发料", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }
            this.Close();
        }

        /// <summary>
        /// 删除明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (detailListView.SelectedIndices == null || detailListView.Items.Count == 0) return;
            DialogResult re = MessageBox.Show("你确定要删除该条数据吗？", "提示", MessageBoxButtons.OKCancel,
               MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
            if (re == DialogResult.Cancel)
                return;

            int index = detailListView.SelectedIndices[0];
            string strLocation = detailListView.Items[index].SubItems[0].Text.Trim();//货架号
            string strMatCode = detailListView.Items[index].SubItems[1].Text.Trim();//物料号
            decimal decMatQty =Convert.ToDecimal ( detailListView.Items[index].SubItems[2].Text.Trim());//数量
            string strKey = strMatCode + strLocation;

            if (dicMtlInfo.ContainsKey(strKey))
            {
                dicMtlInfo.Remove(strKey);
            }
            if (dicMtlQty.ContainsKey(strMatCode))
            {
                dicMtlQty[strMatCode] -= decMatQty;

                if (dicMtlQty[strMatCode] == 0)
                {
                    dicMtlQty.Remove(strMatCode);
                }
            }
            

            this.detailListView.Items.RemoveAt(index);
            Application.DoEvents();
            Message.Alarm("Success", "删除成功！");
        }
    }
}