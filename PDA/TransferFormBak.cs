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
    public partial class TransferFormBak : Form
    {
        public TransferFormBak()
        {
            InitializeComponent();
        }

        #region 变量
        Management management = Management.GetSingleton(); 
        MiddleService service = new MiddleService();
        Dictionary<string, string> dicSeq = new Dictionary<string, string>();
        string matControlFlag = string.Empty;
        string supplier = string.Empty;//供应商代码
        Dictionary<string, decimal> dicMtlQty = new Dictionary<string, decimal>();//Key：库位+物料+批次 ||  库位+序列  Value：本次作业数量
     
        enum Step
        {
           Barcode, Quantity, SourceSite,targetSite
        } 
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TransferForm_Load(object sender, EventArgs e)
        {
            lblMsg.Text = "请扫描来源库位：";
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
            if (barcode.IndexOf('*') > 0)
            {
                currStep = Step.Barcode;
            }
            else if (barcode.StartsWith("$KW$"))//采集库位信息优先 
            {
                //移出模式 目标库位无需维护
                if (rdoMoveOut.Checked)
                {
                    currStep = Step.SourceSite;
                }
                //移入模式  来源库位无需维护
                else if (rdoMoveIn.Checked)
                {
                    currStep = Step.targetSite;
                }
                else
                {
                    //正常 如果来源库位为空  来源库位模式
                    if (string.IsNullOrEmpty(sourceSiteLabel.Text.Trim()))
                    {
                        currStep = Step.SourceSite;
                    }
                    else
                    {
                        currStep = Step.targetSite;
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

            //if (currStep != Step.SourceSite  && string.IsNullOrEmpty(sourceSiteLabel.Text.Trim()))//来源库位为空 
            //{
            //    throw new Exception("请先扫描来源库位");
            //}
            #endregion

            #region 处理逻辑
            switch (currStep)
            {
                //来源库位
                case Step.SourceSite:
                    CheckSite(barcode.Substring(4));
                    sourceSiteLabel.Text = barcode.Substring(4);
                    break;
                //目标库位
                case Step.targetSite:
                    if (sourceSiteLabel.Text.Trim().Equals(barcode.Substring(4)))
                    {
                        throw new Exception("扫描的目标库位与来源库位一样，请确认");
                    }
                    CheckSite(barcode.Substring(4));
                    targetSiteLabel.Text = barcode.Substring(4);
                    break;
                //扫描条码
                case Step.Barcode:
                    BarcodeContent barcodeContent = service.AnalysisForNewBarcode(barcode);
                    bool ISNEWEWM = false; if (barcode.IndexOf("*BN") >= 0) { ISNEWEWM = true; }
                    //根据物料属性判断，该物料对应的编码控制    0单件(序列)控制，1批次控制，2无控制
                    int matControl = service.GetMatControl(barcodeContent.MatCode);
                    if (matControl == 0)
                    {
                        if (dicSeq.ContainsKey(barcodeContent.MatCode+"@"+barcodeContent.SN))
                        {
                            throw new Exception(string.Format("序列号【{0}】不允许重复采集，请确认", barcodeContent.MatCode + "@" + barcodeContent.SN));
                        }

                        batchNoLabel.Text = ISNEWEWM ? barcodeContent.BatchNo : string.Empty;
                        qtyLabel.Text = "1";
                        serialNoLabel.Text =  barcodeContent.SN;
                    }
                    else if ((matControl == 1)||(matControl == 2))
                    {
                        serialNoLabel.Text = ISNEWEWM ? barcodeContent.SN : string.Empty; if (serialNoLabel.Text == null) { serialNoLabel.Text = string.Empty; }
                        batchNoLabel.Text = ISNEWEWM ? barcodeContent.BatchNo : barcodeContent.SN;
                    }
                    else
                    {
                        throw new Exception("物料【" + barcodeContent.MatCode + "】编码控制维护值维护不合法");
                    }
                    supplier = barcodeContent.AagentCode;
                    matControlFlag = matControl.ToString();
                    matCodeLabel.Text = barcodeContent.MatCode;
                    break;
                case Step.Quantity:
                    if (!serialNoLabel.Text.Trim().Equals(""))
                    {
                        throw new Exception("已采集序列号无需采集数量，请扫描库位");
                    }
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
            lblMsg.Text = setMsg("");
            #endregion
        }

        /// <summary>
        /// 设定提示信息
        /// </summary>
        /// <param name="msg"></param>
        private string setMsg(string msg)
        {
            if (string.IsNullOrEmpty(sourceSiteLabel.Text.Trim()))//来源库位为空 
            {
                return string.Format("{0}请扫描来源库位", msg);
            }
            else if (string.IsNullOrEmpty(serialNoLabel.Text.Trim()) && string.IsNullOrEmpty(batchNoLabel.Text.Trim()))//条码为空 采集条码
            {
                return string.Format("{0}请扫描二维码", msg);
            }
            else if (string.IsNullOrEmpty(qtyLabel.Text.Trim()))//如果序列  数量自动填1
            {
                return string.Format("{0}请输入数量", msg);
            }
            else if (string.IsNullOrEmpty(targetSiteLabel.Text.Trim ()))//目标库位
            {
                return string.Format("{0}请扫描目标库位", msg);
            }
            else
            {
                return string.Format("{0}", msg);
            }

        }

        /// <summary>
        /// 检验库位
        /// </summary>
        /// <param name="site"></param>
        private void CheckSite(string site)
        {
            if (string.IsNullOrEmpty(site)) throw new Exception("库位号不能为空");

            //根据库位库房的信息
            DataTable siteTable = siteTable = service.GetStoreSiteBySiteId(site).Tables[0];
            if (siteTable == null || siteTable.Rows.Count == 0)
            {
                throw new Exception(string.Format("库位号【{0}】不存在，请确认",site) );
            }

            string strStoreRoomNo = siteTable.Rows[0]["storeroomno"].ToString();
            if (!siteTable.Rows[0]["isfrozen"].ToString().Equals("0"))
            {
                throw new Exception(string.Format("库位号【{0}】非正常状态，请确认", site));
            }

            if (string.IsNullOrEmpty(storeRoomlabel.Text.Trim()))
            {
                storeRoomlabel.Text = strStoreRoomNo;
                if (!rdoNormal.Checked)
                    updateCheck();
            }
            else
            {
                if (!strStoreRoomNo.Equals(storeRoomlabel.Text.Trim()))
                {
                    throw new Exception(string.Format("目标库位【{0}】与来源库位【{1}】不属于同一库房请确认", site, sourceSiteLabel.Text.Trim()));
                }
            }

        }

        /// <summary>
        /// 添加采集记录集
        /// </summary>
        /// <param name="barcode"></param>
        private void DealQuantity(decimal qty)
        {
            #region 变量
            if (string.IsNullOrEmpty(matControlFlag)) throw new Exception("获取物料编码属性失败");
            if (qty <= 0) throw new Exception("采集数量必须大于0");

            string strSourceSite = sourceSiteLabel.Text.Trim();
            string strTargetSite = targetSiteLabel.Text.Trim();
            string strMatCode = matCodeLabel.Text.Trim();
            string strSn = serialNoLabel.Text.Trim();
            string strBatch = batchNoLabel.Text.Trim();
            DataTable dtRepertory = service.GetRepertoryByStoresiteNo(sourceSiteLabel.Text.Trim(), targetSiteLabel.Text.Trim()).Tables[0];
            string strKey = string.Empty;
            #endregion

            #region 校验库存
            DataRow[] drCheck = null;
            if (matControlFlag.Equals("1") || matControlFlag.Equals("2"))
            {
                strKey = strSourceSite + strMatCode + strBatch;

                //drCheck = dtRepertory.Select(string.Format("storesiteno='{0}' and matcode='{1}'  and parno='{2}' and batchno='{3}' ", strSourceSite, strMatCode, supplier, strBatch));
                drCheck = dtRepertory.Select(string.Format("storesiteno='{0}' and matcode='{1}'  and batchno='{2}' ", strSourceSite, strMatCode, strBatch));
                if (drCheck.Length == 0)
                {
                    throw new Exception(string.Format("物料【{0}】批次【{1}】在库位【{2}】不存在，请确认", strMatCode, strBatch,strSourceSite));
                }
            }
            else
            {
                strKey = strSourceSite + strMatCode + strSn;
                //drCheck = dtRepertory.Select(string.Format("storesiteno='{0}' and matcode='{1}'  and parno='{2}' and sn='{3}' ", strSourceSite, strMatCode, supplier, strSn));
                drCheck = dtRepertory.Select(string.Format("storesiteno='{0}' and matcode='{1}' and sn='{2}' ", strSourceSite, strMatCode, strSn));
                if (drCheck.Length == 0)
                {
                    throw new Exception(string.Format("物料【{0}】序列【{1}】在库位【{2}】不存在，请确认", strMatCode, strSn, strSourceSite));
                }
            }

            decimal decRepqty = 0;
            if (dicMtlQty.ContainsKey(strKey))
                decRepqty = dicMtlQty[strKey];

            if (Convert.ToDecimal(drCheck[0]["repqty"].ToString()) - decRepqty < qty)
            {
                throw new Exception(string.Format("库位【{0}】物料【{1}】的库存【{2}】小于本次移出库存【{3}】，请确认", strSourceSite, strMatCode, (Convert.ToDecimal(drCheck[0]["repqty"].ToString()) - decRepqty), qty));
            }

            string strErpRoom = drCheck[0]["erp_storeroom"].ToString();
            #endregion

            #region 检验目标库位
            drCheck = dtRepertory.Select(string.Format("storesiteno='{0}' and matcode='{1}' and (erp_storeroom <> '{2}') ", strTargetSite, strMatCode, strErpRoom));
            
            if (drCheck.Length > 0)
            {
                throw new Exception(string.Format("目标库位【{0}】已存在子库【{2}】的物料【{3}】，请确认", strTargetSite, drCheck[0]["erp_storeroom"].ToString(), strMatCode));
            }
            
            #endregion

            #region 校验完成 处理数据
            if (!string.IsNullOrEmpty(strSn) && !dicSeq.ContainsKey(strMatCode+"@"+strSn))
            {
                dicSeq.Add(strMatCode + "@" + strSn, strMatCode + "@" + strSn);
            }

            if (!dicMtlQty.ContainsKey(strKey))
            {
                dicMtlQty.Add(strKey, qty);
            }
            else
            {
                dicMtlQty[strKey] += qty;
            }

            //添加采集记录
            detailListView.Items.Add(new ListViewItem(new string[] { strSourceSite, strTargetSite, strMatCode, strBatch, strSn, qty.ToString(), supplier, strErpRoom }));
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
            batchNoLabel.Text = "";
            serialNoLabel.Text = "";
            qtyLabel.Text = "";
        }

        /// <summary>
        /// 更换库位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateSite_Click(object sender, EventArgs e)
        {
            storeRoomlabel.Text = "";
            sourceSiteLabel.Text = "";
            targetSiteLabel.Text = "";
            InitializeCollect();
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
                BeforeCommit();

                Dictionary<string, string> dicSite = new Dictionary<string, string>();
                string filter = string.Empty;
                string strSite = string.Empty;

                TransferInfo[] transferInfos = new TransferInfo[detailListView.Items.Count];   //托盘内货物信息
                for (int i = 0; i < detailListView.Items.Count; i++)
                {
                    strSite=detailListView.Items[i].SubItems[0].Text.Trim();//源库位
                    if (!dicSite.ContainsKey(strSite))
                    {
                        dicSite.Add(strSite, strSite);
                        filter += ",'" + strSite + "'";
                    }
                    TransferInfo transferInfo = new TransferInfo();

                    transferInfo.InSite= detailListView.Items[i].SubItems[1].Text.Trim();//目标库位
                    transferInfo.OutSite = strSite;//源库位
                    transferInfo.MaterialCode = detailListView.Items[i].SubItems[2].Text.Trim();//物料代码
                    transferInfo.BatchNo = detailListView.Items[i].SubItems[3].Text.Trim();//批号
                    transferInfo.Sn = detailListView.Items[i].SubItems[4].Text.Trim();//物料SN
                    transferInfo.Qty = detailListView.Items[i].SubItems[5].Text.Trim();//数量
                    transferInfo.MoveDesc = "";
                    transferInfo.Supplier = detailListView.Items[i].SubItems[6].Text.Trim();//供应商
                    transferInfo.ErpRoom = detailListView.Items[i].SubItems[7].Text.Trim();//子库
                    transferInfos[i] = transferInfo;
                }
                service.CommitTransfer(transferInfos, User.Instance.UserData.UserId, filter.Remove(0,1));
                storeRoomlabel.Text = "";
                sourceSiteLabel.Text = "";
                targetSiteLabel.Text = "";
                dicSeq = new Dictionary<string, string>();
                matControlFlag = string.Empty;
                supplier = string.Empty;//供应商代码
                dicMtlQty = new Dictionary<string, decimal>();//Key：库位+物料+批次 ||  库位+序列  Value：本次作业数量
                lblMsg.Text = "请扫描来源库位：";

                detailListView.Items.Clear();
                InitializeCollect();
            }
            catch(Exception ex)
            {
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
                throw new Exception("移库明细不可以为空，请确认");
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
"移库作业", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
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

            string strSourceSite = detailListView.Items[index].SubItems[0].Text.Trim();//来源库位
            string strMatCode = detailListView.Items[index].SubItems[2].Text.Trim();//物料代码
            string strSn = detailListView.Items[index].SubItems[4].Text.Trim();//物料SN
            string strBatch = detailListView.Items[index].SubItems[3].Text.Trim();//批号
            decimal decQty = Convert.ToDecimal(detailListView.Items[index].SubItems[5].Text.Trim());
            string strKey = string.Empty;
            if (matControlFlag.Equals("1"))
            {
                strKey = strSourceSite + strMatCode + strBatch;
            }
            else
            {
                strKey = strSourceSite + strMatCode + strSn;
            }

            if (dicMtlQty.ContainsKey(strKey))
            {
                dicMtlQty[strKey] -= decQty;
            }

            if (!strSn.Equals(string.Empty) && dicSeq.ContainsKey(strMatCode+"@"+strSn))
                dicSeq.Remove(strMatCode + "@" + strSn);

            this.detailListView.Items.RemoveAt(index);
            Application.DoEvents();
            Message.Alarm("Success", "删除成功！");
        }

        private void rdoMoveOut_CheckedChanged(object sender, EventArgs e)
        {
            rdoMoveIn.CheckedChanged -= rdoMoveIn_CheckedChanged;
            rdoNormal.CheckedChanged -= rdoNormal_CheckedChanged;
            updateCheck();
            rdoMoveIn.CheckedChanged += rdoMoveIn_CheckedChanged;
            rdoNormal.CheckedChanged += rdoNormal_CheckedChanged;
        }

        private void rdoMoveIn_CheckedChanged(object sender, EventArgs e)
        {
            rdoMoveOut.CheckedChanged -= rdoMoveOut_CheckedChanged;
            rdoNormal.CheckedChanged -= rdoNormal_CheckedChanged;
            updateCheck();
            rdoMoveOut.CheckedChanged += rdoMoveOut_CheckedChanged;
            rdoNormal.CheckedChanged += rdoNormal_CheckedChanged;
        }

        private void rdoNormal_CheckedChanged(object sender, EventArgs e)
        {
            rdoMoveIn.CheckedChanged -= rdoMoveIn_CheckedChanged;
            rdoMoveOut.CheckedChanged -= rdoMoveOut_CheckedChanged;
            updateCheck();
            rdoMoveIn.CheckedChanged += rdoMoveIn_CheckedChanged;
            rdoMoveOut.CheckedChanged += rdoMoveOut_CheckedChanged;
        }

        private void updateCheck()
        {
            if (storeRoomlabel.Text.Trim().Equals(""))
            {
                return;
            }
            InitializeCollect();
            if (rdoMoveOut.Checked)
            {
                sourceSiteLabel.Text = "";
                targetSiteLabel.Text = storeRoomlabel.Text + "MOVESITE";
            }
            else if (rdoMoveIn.Checked)
            {
                sourceSiteLabel.Text = storeRoomlabel.Text  + "MOVESITE";
                targetSiteLabel.Text = "";
            }
            else
            {
                storeRoomlabel.Text = "";
                sourceSiteLabel.Text = "";
                targetSiteLabel.Text = "";
            }
        }
    }
}