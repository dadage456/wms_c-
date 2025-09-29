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
    public partial class TransferForm : Form
    {
        public TransferForm()
        {
            InitializeComponent();
        }

        #region 变量
        Management management = Management.GetSingleton();
        DataTable taskTable = new DataTable();
        DataView dv = new DataView();
        MiddleService service = new MiddleService();
        int currentRowIndex = -1;

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
                //清空上次选择的数据
                taskDataGrid.DataSource = null;
                projectNumLabel.Text = "";
                erpOwnerCodeLabel.Text = "";
                erpStoreRoomLabel.Text = "";

                currStep = Step.Barcode;                
            }
            else if (barcode.StartsWith("$KW$"))//采集库位信息优先 
            {
                //清空上次选择的数据
                taskDataGrid.DataSource = null;
                projectNumLabel.Text = "";
                erpOwnerCodeLabel.Text = "";
                erpStoreRoomLabel.Text = "";

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

                if ((taskDataGrid == null) || (string.IsNullOrEmpty(erpOwnerCodeLabel.Text.Trim())))
                {
                    throw new Exception("请您：首先点击【查询】，根据来源货位【" + sourceSiteLabel.Text.Trim() + "】物料号【" + matCodeLabel.Text.Trim() + "】查询库存信息；然后选择移库记录；最后在输入数量！");
                }
                else
                {
                    currStep = Step.Quantity;
                }
                
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
                    if ((!string.IsNullOrEmpty(sourceSiteLabel.Text.Trim())) && (!string.IsNullOrEmpty(matCodeLabel.Text.Trim())))
                    {
                        QueryButton_Click(null, null);
                    }
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
                        //qtyLabel.Text = "1";
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
                    if ((!string.IsNullOrEmpty(sourceSiteLabel.Text.Trim())) && (!string.IsNullOrEmpty(matCodeLabel.Text.Trim()))) 
                    {
                        QueryButton_Click(null, null);
                    }
                    break;
                case Step.Quantity:
                    //if (!serialNoLabel.Text.Trim().Equals(""))
                    //{
                    //    throw new Exception("已采集序列号无需采集数量，请扫描库位");
                    //}
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
                return string.Format("{0}请先查询后输入数量", msg);
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
            string strErpOwnerCode = erpOwnerCodeLabel.Text.Trim();
            string strErpStoreRoom = erpStoreRoomLabel.Text.Trim();
            string strProjectNum = projectNumLabel.Text.Trim();   
            string strRepQty = qtyLabel.Text.Trim();
            DataTable dtRepertory = service.GetRepertoryByStoresiteNo(sourceSiteLabel.Text.Trim(), targetSiteLabel.Text.Trim()).Tables[0];
            string strKey = string.Empty;
            decimal RepQty = 0;
            #endregion

            #region 校验库存
            DataRow[] drCheck = null;
            if (matControlFlag.Equals("1") || matControlFlag.Equals("2"))
            {
                strKey = strSourceSite + strMatCode + strBatch + strErpStoreRoom + strErpOwnerCode + strProjectNum;
                //strKey = strRepertoryId;

                //drCheck = dtRepertory.Select(string.Format("storesiteno='{0}' and matcode='{1}'  and parno='{2}' and batchno='{3}' ", strSourceSite, strMatCode, supplier, strBatch));
                if (string.IsNullOrEmpty(strProjectNum))
                {
                    drCheck = dtRepertory.Select(string.Format("storesiteno='{0}' and matcode='{1}'  and batchno='{2}'  and erp_storeroom ='{3}' and rep_owner_code='{4}' ", strSourceSite, strMatCode, strBatch, strErpStoreRoom, strErpOwnerCode));
                    RepQty = Convert.ToDecimal(dtRepertory.Compute("sum(repqty)", string.Format("storesiteno='{0}' and matcode='{1}'  and batchno='{2}'  and erp_storeroom ='{3}' and rep_owner_code='{4}' ", strSourceSite, strMatCode, strBatch, strErpStoreRoom, strErpOwnerCode)).ToString());
                }
                else
                {
                    drCheck = dtRepertory.Select(string.Format("storesiteno='{0}' and matcode='{1}'  and batchno='{2}'  and erp_storeroom ='{3}' and rep_owner_code='{4}'  and project_num = '{5}'", strSourceSite, strMatCode, strBatch, strErpStoreRoom, strErpOwnerCode, strProjectNum));
                    RepQty = Convert.ToDecimal(dtRepertory.Compute("sum(repqty)", string.Format("storesiteno='{0}' and matcode='{1}'  and batchno='{2}'  and erp_storeroom ='{3}' and rep_owner_code='{4}'  and project_num = '{5}'", strSourceSite, strMatCode, strBatch, strErpStoreRoom, strErpOwnerCode, strProjectNum)).ToString());
                }

                if (drCheck.Length == 0)
                {
                    throw new Exception(string.Format("物料【{0}】批次【{1}】库位【{2}】子库【{3}】拥有方【{4}】项目号【{5}】不存在，请确认", strMatCode, strBatch, strSourceSite, strErpStoreRoom,strErpOwnerCode, strProjectNum));
                }
            }
            else
            {
                strKey = strSourceSite + strMatCode + strSn + strErpStoreRoom + strErpOwnerCode + strProjectNum;
                //strKey = strRepertoryId;
                //drCheck = dtRepertory.Select(string.Format("storesiteno='{0}' and matcode='{1}'  and parno='{2}' and sn='{3}' ", strSourceSite, strMatCode, supplier, strSn));
                if (string.IsNullOrEmpty(strProjectNum))
                {
                    drCheck = dtRepertory.Select(string.Format("storesiteno='{0}' and matcode='{1}' and sn='{2}'  and erp_storeroom ='{3}' and rep_owner_code='{4}' ", strSourceSite, strMatCode, strSn, strErpStoreRoom, strErpOwnerCode));
                    RepQty = Convert.ToDecimal(dtRepertory.Compute("sum(repqty)", string.Format("storesiteno='{0}' and matcode='{1}' and sn='{2}'  and erp_storeroom ='{3}' and rep_owner_code='{4}' ", strSourceSite, strMatCode, strSn, strErpStoreRoom, strErpOwnerCode)).ToString());
                }
                else
                {
                    drCheck = dtRepertory.Select(string.Format("storesiteno='{0}' and matcode='{1}'  and batchno='{2}'  and erp_storeroom ='{3}' and rep_owner_code='{4}'  and project_num = '{5}' ", strSourceSite, strMatCode, strBatch, strErpStoreRoom, strErpOwnerCode, strProjectNum));
                    RepQty = Convert.ToDecimal(dtRepertory.Compute("sum(repqty)", string.Format("storesiteno='{0}' and matcode='{1}'  and batchno='{2}'  and erp_storeroom ='{3}' and rep_owner_code='{4}'  and project_num = '{5}' ", strSourceSite, strMatCode, strBatch, strErpStoreRoom, strErpOwnerCode, strProjectNum)).ToString());
                }

                if (drCheck.Length == 0)
                {
                    throw new Exception(string.Format("物料【{0}】序列【{1}】库位【{2}】子库【{3}】拥有方【{4}】项目号【{5}】不存在，请确认", strMatCode, strSn, strSourceSite, strErpStoreRoom, strErpOwnerCode, strProjectNum));
                }
            }

            decimal decRepqty = 0;
            if (dicMtlQty.ContainsKey(strKey))
                decRepqty = dicMtlQty[strKey];

            //if (Convert.ToDecimal(drCheck[0]["repqty"].ToString()) - decRepqty < qty)
            if (RepQty - decRepqty < qty)
            {
                throw new Exception(string.Format("库位【{0}】物料【{1}】拥有方【{2}】项目号【{3}】的库存【{5}】小于本次移出库存【{6}】，请确认", strSourceSite, strMatCode, strErpOwnerCode, strProjectNum, (RepQty - decRepqty), qty));
            }

            string strErpRoom = drCheck[0]["erp_storeroom"].ToString();
            #endregion

            #region 移入模式检验目标库位
            //移入模式  
            if (rdoMoveIn.Checked)
            {
                drCheck = dtRepertory.Select(string.Format("storesiteno='{0}' and matcode='{1}' and (erp_storeroom <> '{2}' ) ", strTargetSite, strMatCode, strErpRoom));

                if (drCheck.Length > 0)
                {
                    throw new Exception(string.Format("目标库位【{0}】已存在子库【{1}】的物料【{2}】，请确认", strTargetSite, drCheck[0]["erp_storeroom"].ToString(), strMatCode));
                }
            }
            
            #endregion

            #region 校验完成 处理数据
            if (!string.IsNullOrEmpty(strSn) && !dicSeq.ContainsKey(strMatCode + "@" + strSn))
            {
                dicSeq.Add(strMatCode + "@" + strSn, String.Format("{0}@{1}", strMatCode, strSn));
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
            detailListView.Items.Add(new ListViewItem(new string[] { strSourceSite, strTargetSite, strMatCode, strBatch, strSn, qty.ToString(), strErpOwnerCode, strErpRoom, strProjectNum }));
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
            taskDataGrid.DataSource = null;
            projectNumLabel.Text = "";
            erpOwnerCodeLabel.Text = "";
            erpStoreRoomLabel.Text = "";
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
                    transferInfo.ProjectNum = detailListView.Items[i].SubItems[8].Text.Trim();//项目号
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
            string strSupplier = detailListView.Items[index].SubItems[6].Text.Trim();//供应商
            string strErpRoom = detailListView.Items[index].SubItems[7].Text.Trim();//子库
            string strProjectNum = detailListView.Items[index].SubItems[8].Text.Trim();//项目号

            string strKey = string.Empty;
            if (matControlFlag.Equals("1"))
            {
                strKey = strSourceSite + strMatCode + strBatch + strErpRoom + strSupplier + strProjectNum;
                //strKey = strRepertoryId;
            }
            else
            {
                strKey = strSourceSite + strMatCode + strSn + strErpRoom + strSupplier + strProjectNum;
                //strKey = strRepertoryId;
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

        private void taskDataGrid_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                DataGrid.HitTestInfo hti = taskDataGrid.HitTest(e.X, e.Y);
                switch (hti.Type)
                {
                    case DataGrid.HitTestType.ColumnHeader:     //列头排序
                        string columnHeader = taskTable.Columns[hti.Column].ColumnName;
                        dv.Sort = columnHeader + " ASC";
                        taskDataGrid.DataSource = dv;
                        break;
                    case DataGrid.HitTestType.RowHeader:        //行选中                        
                        this.projectNumLabel.Text = taskDataGrid[taskDataGrid.CurrentRowIndex, 3].ToString();
                        this.erpStoreRoomLabel.Text = taskDataGrid[taskDataGrid.CurrentRowIndex, 5].ToString();
                        this.erpOwnerCodeLabel.Text = taskDataGrid[taskDataGrid.CurrentRowIndex, 6].ToString();
                        currentRowIndex = taskDataGrid.CurrentRowIndex;                        
                        break;
                }
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }
        
        /// <summary>
        /// 自动宽
        /// </summary>
        /// <param name="p"></param>
        private void AutoSizeCol(int p)
        {
            this.taskDataGrid.TableStyles.Clear();
            //定义DataGridTableStyle  
            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = dv.Table.TableName;  //映射style对应数据源的表名，很重要，否则无数据显示  

            //分别对列进行渲染，其中前三列用for循环实现，对列宽进行设定，值为75   
            for (int i = 0; i < p; i++)
            {

                DataGridColumnStyle aColumnTextColumnStyle = new DataGridTextBoxColumn();//定义该列用textbox来进行渲染 
                aColumnTextColumnStyle.HeaderText = dv.Table.Columns[i].ColumnName;   //列头 
                aColumnTextColumnStyle.MappingName = dv.Table.Columns[i].ColumnName; //映射数据源的列名，很重要，否则无数据显示
                //显示顺序 --物料 数量 批次 项目号 储位
                //排序顺序 项目号 批次 
                if (i == 0) aColumnTextColumnStyle.Width = 120;//物料编号
                if (i == 1) aColumnTextColumnStyle.Width = 80; //数量
                if (i == 2) aColumnTextColumnStyle.Width = 100;//批次号               
                if (i == 3) aColumnTextColumnStyle.Width = 100;//项目号
                if (i == 4) aColumnTextColumnStyle.Width = 100;//货位号
                if (i == 5) aColumnTextColumnStyle.Width = 100;//子库
                if (i == 6) aColumnTextColumnStyle.Width = 100;//拥有方
                if (i == 7) aColumnTextColumnStyle.Width = 100;//托盘号
                if (i == 8) aColumnTextColumnStyle.Width = 100;//序列号
                if (i == 9) aColumnTextColumnStyle.Width = 100;//库房
                if (i == 10) aColumnTextColumnStyle.Width = 0;//
                if (i == 11) aColumnTextColumnStyle.Width = 0;//
                if (i == 12) aColumnTextColumnStyle.Width = 0;//
                ts.GridColumnStyles.Add(aColumnTextColumnStyle);
            }
            this.taskDataGrid.TableStyles.Add(ts);

        }
        private void QueryButton_Click(object sender, EventArgs e)
        {
            try
            {
                taskDataGrid.DataSource = null;

                string storeSiteNo = sourceSiteLabel.Text.Trim();
                string matCode = matCodeLabel.Text.Trim();
                string batchNo = batchNoLabel.Text.Trim();
                if (!string.IsNullOrEmpty(serialNoLabel.Text.Trim()))
                {
                    batchNo = serialNoLabel.Text.Trim();
                }

                DataSet ds = service.GetRepertoryBySiteNoMatCode(storeSiteNo, matCode, batchNo);

                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    return;
                }
                taskTable = ds.Tables[0];
                dv.Table = taskTable;
                taskDataGrid.DataSource = dv;
                AutoSizeCol(taskTable.Columns.Count);
            
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }
        
    }
}