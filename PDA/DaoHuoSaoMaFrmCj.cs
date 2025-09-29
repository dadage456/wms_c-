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
using System.Globalization;

namespace PDA
{
    public partial class DaoHuoSaoMaFrmCj : Form
    {
        #region 变量
        private string taskNo = string.Empty; //传入的任务号
        DataView taskDtls_dv = new DataView(); //本任务下,所有的(物料+批次)的信息
        public DataTable taskDtls_tbl = null;

        DataView taskMat_dv = new DataView(); //单一物料的信息
        public DataTable taskMat_tbl = null;
        Management management = Management.GetSingleton();
        
        #endregion

        public DaoHuoSaoMaFrmCj(string taskNo)
        {
            InitializeComponent();
            this.taskNo = taskNo;
            this.rkbillno.Text = taskNo; //默认带出
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DaoHuoSaoMaFrmCj_Load(object sender, EventArgs e)
        { 
            try
            {
                BindingTrayCollectData.Instance.Collect = new List<BindingTray>();
                //boCheckMtl = service.GetIsCheckMtl();//检查物料

                lblMsg.Text = "请扫描：";
                tbxBarcode.Text = "";
                tbxBarcode.Focus();
                string larr_colwidths = null; int li_noDispRCols = 0;
                //得到当前任务的所有的物料任务明细
                try
                {
                    detailListView.DataSource = null;
                    //
                    List<string> args_nv = new List<string>();
                    args_nv.Add("userid"); args_nv.Add(User.Instance.UserData.UserId);
                    args_nv.Add("billid"); args_nv.Add(taskNo);
                    args_nv.Add("ndispycj"); args_nv.Add("Y");  //已采集完成的物料,不显示
                    // 
                    SQLHelper s = new SQLHelper();
                    DataSet ds = s.WS("PDA_DH_V2_CMX", args_nv.ToArray());
                    if (ds == null || s.sqlcode == -1)
                    {
                        MessageBox.Show("查询待到货验货信息错误", "提示");
                        return;
                    }
                    //
                    if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    {
                        //throw new Exception("暂无下架任务"); 
                        return;
                    }
                    taskDtls_tbl = ds.Tables[0]; //本任务号下的所有物料明细
                    //2018-04-09 zxj 设置detailListView为不显示, 对应的数据藏在后面
                    taskDtls_dv.Table = taskDtls_tbl;
                    taskDtls_dv.AllowEdit = false;
                    taskDtls_dv.AllowDelete = false;
                    taskDtls_dv.AllowNew = false;
                    detailListView.DataSource = taskDtls_dv;
                    //进行列宽的控制
                    larr_colwidths = ds.Tables[0].Columns.IndexOf("colwidths") >= 0 ? SQLHelper.SNVL(ds.Tables[0].Rows[0]["colwidths"], "") : "";
                    li_noDispRCols = ds.Tables[0].Columns.IndexOf("nodisprcols") >= 0 ? (int)SQLHelper.NNVL(ds.Tables[0].Rows[0]["nodisprcols"], 0) : 0;
                    SQLHelper.DataGridColumnSize(taskDtls_dv, detailListView, null, li_noDispRCols, larr_colwidths); //显示未采集的到货任务单明细
                }
                catch (Exception ex)
                {
                    Message.Alarm("提示", ex.Message);
                }
                //-- 某种物料的各批号信息,按物料加批次的任务进行显示,新扫描的数据进行分摊到原来的物料+批次记录上
                //taskMat_tbl = taskDtls_tbl.Clone();  //不复制数据, 每扫一个mat,复制相关的部分
                List<string> hText = new List<string>();
                int[] colwidths = new int[20]; int c = -1;
                taskMat_tbl = new DataTable("采集任务信息");
                //
                taskMat_tbl.Columns.Add(new DataColumn("matcode")); hText.Add("物料编码"); c++; colwidths[c] = 100;
                taskMat_tbl.Columns.Add(new DataColumn("BATCHNO")); hText.Add("批号"); c++; colwidths[c] = 100;
                taskMat_tbl.Columns.Add(new DataColumn("SN")); hText.Add("序列号"); c++; colwidths[c] = 100;
                taskMat_tbl.Columns.Add(new DataColumn("matname")); hText.Add("物料名称"); c++; colwidths[c] = 150;
                taskMat_tbl.Columns.Add(new DataColumn("QTY")); hText.Add("任务数"); c++; colwidths[c] = 100;
                taskMat_tbl.Columns.Add(new DataColumn("GOODQTY")); hText.Add("已采集"); c++; colwidths[c] = 100;
                taskMat_tbl.Columns.Add(new DataColumn("MATCODECONTROL")); hText.Add("批号控制"); c++; colwidths[c] = 5;
                //
                taskMat_dv.Table = taskMat_tbl;
                taskMat_dv.AllowEdit = false;
                taskMat_dv.AllowDelete = false;
                taskMat_dv.AllowNew = false;
                QueryListView.DataSource = taskMat_dv;
                //SQLHelper.DataGridColumnSize(taskMat_dv, QueryListView, null, li_noDispRCols, larr_colwidths); //显示未采集的到货任务单明细-某物料的各批次信息
                //
                DataGridTableStyle dgdtblStyle = new DataGridTableStyle(); //显示header中的title
                dgdtblStyle.MappingName = taskMat_tbl.TableName;
                QueryListView.TableStyles.Add(dgdtblStyle);
                GridColumnStylesCollection colStyle = QueryListView.TableStyles[0].GridColumnStyles;
                System.Collections.Generic.List<string>.Enumerator ienum = hText.GetEnumerator();
                for (int iht = 0; iht < hText.Count; iht++)
                {
                    ienum.MoveNext();
                    colStyle[iht].HeaderText = ienum.Current;
                    colStyle[iht].Width = colwidths[iht]; //设置列宽
                    colStyle[iht].NullText = "";
                }
                //
                if (!refresh_taskMat(taskDtls_tbl, taskMat_tbl)) //显示已完成任务的汇总信息 //load中
                {
                    Message.Alarm("提示", "已采集的数量和任务要求的数量不匹配,请修正");
                }
                //
                //条码框获得焦点,等待输入
                tbxBarcode.Focus();
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        /// <summary>
        /// 按物料为单位进行任务数和采集数的分摊,尽量满足原来的批号(序列号)匹配规则
        /// </summary>
        /// <param name="pm_taskDtls_tbl">已采集的任务列表</param>
        /// <param name="pm_taskMat_tbl">按原任务的分摊结果显示</param>
        /// <returns></returns>
        public bool refresh_taskMat(DataTable pm_taskDtls_tbl, DataTable pm_taskMat_tbl)
        {
            DataTable dt_allline = pm_taskDtls_tbl.Copy(); //复制一份,计算的时候用
            pm_taskMat_tbl.Clear();
            bool ib_valid = true;
            //直接从父窗口的结果集中进行读取; 父窗结果集中 就是(物料+批次)的记录;
            for (int t = 0; t < 2; t++)
            {
                for (int i = 0; i < dt_allline.Rows.Count; i++)
                {
                    if (t == 0)//小于0的是新加的记录; 大于0的是原来的任务;
                    {
                        if (SQLHelper.NNVL(dt_allline.Rows[i]["QTY"], 0) > 0) //QTY 大于0的记录,为原始的任务记录
                        {
                            string ls_MATCODE = SQLHelper.SNVL(dt_allline.Rows[i]["MATCODE"], "");
                            string ls_MATNAME = SQLHelper.SNVL(dt_allline.Rows[i]["MATNAME"], "");
                            string ls_BATCHNO = SQLHelper.SNVL(dt_allline.Rows[i]["BATCHNO"], "");
                            string ls_SN = SQLHelper.SNVL(dt_allline.Rows[i]["SN"], "");
                            decimal ldc_qty = SQLHelper.NNVL(dt_allline.Rows[i]["QTY"], 0);
                            //decimal ldc_googdqty = SQLHelper.NNVL(dt_allline.Rows[i]["GOODQTY"], 0); //可能本身带入的记录就超出数量了,需要进行控制
                            DataRow dr = pm_taskMat_tbl.NewRow();
                            dr["matcode"] = ls_MATCODE;
                            dr["matname"] = ls_MATNAME;
                            dr["BATCHNO"] = ls_BATCHNO;
                            dr["SN"] = ls_SN;
                            dr["MATCODECONTROL"] = SQLHelper.SNVL(this.taskDtls_tbl.Rows[i]["MATCODECONTROL"], "");
                            dr["QTY"] = ldc_qty;
                            //dr["GOODQTY"] = ldc_googdqty; //
                            pm_taskMat_tbl.Rows.Add(dr);
                        }
                    } //
                    else
                    { //第2次循环,进行采集数量的分摊
                        decimal ldc_ft_tqty = SQLHelper.NNVL(dt_allline.Rows[i]["GOODQTY"], 0);
                        string ls_ft_MATCODE = SQLHelper.SNVL(dt_allline.Rows[i]["MATCODE"], "");
                        string ls_ft_MATNAME = SQLHelper.SNVL(dt_allline.Rows[i]["MATNAME"], "");
                        string ls_ft_BATCHNO = SQLHelper.SNVL(dt_allline.Rows[i]["BATCHNO"], "");
                        string ls_ft_SN = SQLHelper.SNVL(dt_allline.Rows[i]["SN"], "");
                        string ls_ft_ctrl = SQLHelper.SNVL(dt_allline.Rows[i]["MATCODECONTROL"], "");
                        for (int j = 0; j < pm_taskMat_tbl.Rows.Count; j++) //新加的记录,数量分摊到其他的行上, 不能分摊的,返回false
                        {//!!! 是在mat datatable 中循环, 所以不需要判断qty
                            if ((ls_ft_ctrl == "0"
                                 && ls_ft_MATCODE == SQLHelper.SNVL(pm_taskMat_tbl.Rows[j]["MATCODE"], "")
                                 && ls_ft_BATCHNO == SQLHelper.SNVL(pm_taskMat_tbl.Rows[j]["BATCHNO"], "")
                                 && ls_ft_SN == SQLHelper.SNVL(pm_taskMat_tbl.Rows[j]["SN"], "")) ||
                                (ls_ft_ctrl != "0"
                                 && ls_ft_MATCODE == SQLHelper.SNVL(pm_taskMat_tbl.Rows[j]["MATCODE"], "")
                                 && ls_ft_BATCHNO == SQLHelper.SNVL(pm_taskMat_tbl.Rows[j]["BATCHNO"], "")))
                            {
                                decimal ldc_ycj_qty = SQLHelper.NNVL(pm_taskMat_tbl.Rows[j]["QTY"], 0);
                                decimal ldc_ycj_googdqty = SQLHelper.NNVL(pm_taskMat_tbl.Rows[j]["GOODQTY"], 0);
                                if (ldc_ycj_googdqty >= ldc_ycj_qty) //本条(批次)记录不能再分摊了
                                {
                                    continue;
                                }
                                decimal ldc_qty_inc = ldc_ycj_qty - ldc_ycj_googdqty; //可分摊总数
                                if (ldc_qty_inc >= ldc_ft_tqty) { ldc_qty_inc = ldc_ft_tqty; } //本条记录分摊的数
                                //
                                pm_taskMat_tbl.Rows[j]["GOODQTY"] = ldc_ycj_googdqty + ldc_qty_inc;

                                //MessageBox.Show("采集任务的sn1：" + dt_allline.Rows[i]["SN"] + " 循环值i的值为：" + i, "提示信息");
                                //MessageBox.Show("采集任务的sn2：" + pm_taskMat_tbl.Rows[j]["SN"] + " 循环值j的值为" + j, "提示信息");

                               // MessageBox.Show("采集任务的数量：" + pm_taskMat_tbl.Rows[j]["GOODQTY"], "提示信息");

                                //
                                ldc_ft_tqty -= ldc_qty_inc;
                                if (ldc_ft_tqty <= 0) { break; } //不记录已经分摊完了,直接跳出循环
                            }
                        }//精确匹配结束
                        if (ldc_ft_tqty > 0)
                        {
                            for (int j = 0; j < pm_taskMat_tbl.Rows.Count; j++) //新加的记录,数量分摊到其他的行上, 不能分摊的,返回false
                            {//!!! 是在mat datatable 中循环, 所以不需要判断qty
                                if (ls_ft_MATCODE == SQLHelper.SNVL(pm_taskMat_tbl.Rows[j]["MATCODE"], "")  )
                                {
                                    decimal ldc_ycj_qty = SQLHelper.NNVL(pm_taskMat_tbl.Rows[j]["QTY"], 0);
                                    decimal ldc_ycj_googdqty = SQLHelper.NNVL(pm_taskMat_tbl.Rows[j]["GOODQTY"], 0);
                                    if (ldc_ycj_googdqty >= ldc_ycj_qty) //本条(批次)记录不能再分摊了
                                    {
                                        continue;
                                    }
                                    decimal ldc_qty_inc = ldc_ycj_qty - ldc_ycj_googdqty; //可分摊总数
                                    if (ldc_qty_inc >= ldc_ft_tqty) { ldc_qty_inc = ldc_ft_tqty; } //本条记录分摊的数
                                    //
                                    pm_taskMat_tbl.Rows[j]["GOODQTY"] = ldc_ycj_googdqty + ldc_qty_inc;
                                    //
                                    ldc_ft_tqty -= ldc_qty_inc;
                                    if (ldc_ft_tqty <= 0) { break; } //不记录已经分摊完了,直接跳出循环
                                }
                            }
                        }//按物料为单位的匹配结果
                        if (ldc_ft_tqty > 0)
                        {
                            ib_valid = false; //没有分摊完
                        }
                    }//第二次循环
                }
            }
            return ib_valid;
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

                    PerformingBarcode(barcode); //解析扫描得到的条码
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
        /// 取某物料的总任务数
        /// </summary>
        /// <param name="ls_matcode"></param>
        /// <param name="pm_taskDtls_tbl"></param>
        /// <returns></returns>
        private decimal getMatTotQty(string ls_matcode, DataTable pm_taskDtls_tbl)
        {
            decimal ldc_tqty = 0;
            for (int i = 0; i < pm_taskDtls_tbl.Rows.Count; i++)
            {
                if (ls_matcode.Equals(SQLHelper.SNVL(pm_taskDtls_tbl.Rows[i]["matcode"], "")))
                {
                    ldc_tqty += SQLHelper.NNVL(pm_taskDtls_tbl.Rows[i]["qty"], 0);
                }
            }
            return ldc_tqty;
        }

        private Step last_Step = Step.Quantity;
         

        //找物料对应的行号
        private void getRowNum(string ls_matcode, string ls_batch_no, string ls_serialno, DataTable taskDtls_tbl, ref string ls_MATCODECONTROL, ref int li_found_mat, ref int li_found_row_dtl)
        {
            li_found_row_dtl = -1;
            for (int i = 0; i < this.taskDtls_tbl.Rows.Count; i++)
            {
                if (ls_matcode.Equals(SQLHelper.SNVL(taskDtls_tbl.Rows[i]["matcode"], "")))
                {
                    li_found_mat = i; //
                    ls_MATCODECONTROL = SQLHelper.SNVL(taskDtls_tbl.Rows[i]["MATCODECONTROL"], ""); //相同物料此值应该是一样的
                    //
                    if ("0".Equals(ls_MATCODECONTROL))
                    { //序列号控制
                        if (ls_serialno == null || "".Equals(ls_serialno))
                        {
                            throw new Exception("物料需管理序列号,但条码内容中无序列号信息,不能处理");
                        }
                        if (ls_batch_no == null || "".Equals(ls_batch_no))
                        {
                            throw new Exception("物料需管理批次,但条码内容中无批次号信息,不能处理");
                        }
                        if (ls_matcode == SQLHelper.SNVL(taskDtls_tbl.Rows[i]["MATCODE"], "") &&
                            ls_batch_no == SQLHelper.SNVL(taskDtls_tbl.Rows[i]["BATCHNO"], "") &&
                            ls_serialno == SQLHelper.SNVL(taskDtls_tbl.Rows[i]["SN"], "") &&
                            0 == SQLHelper.NNVL(taskDtls_tbl.Rows[i]["GOODQTY"], 0) //第一轮,优先找空着的
                            )
                        {
                            li_found_row_dtl = i; break;
                        }
                    }
                    else
                    {
                        if (ls_batch_no == null || "".Equals(ls_batch_no))
                        {
                            throw new Exception("物料需管理批次,但条码内容中无批次号信息,不能处理");
                        }
                        if (ls_matcode == SQLHelper.SNVL(taskDtls_tbl.Rows[i]["MATCODE"], "") &&
                            ls_batch_no == SQLHelper.SNVL(taskDtls_tbl.Rows[i]["BATCHNO"], "") &&
                            0 == SQLHelper.NNVL(taskDtls_tbl.Rows[i]["GOODQTY"], 0)) //第一轮,优先找空着的
                        {
                            li_found_row_dtl = i; break;
                        }
                    }
                }
            }
            //
            if (li_found_row_dtl == -1) //再循环一次,不为空的也允许
            {
                for (int i = 0; i < this.taskDtls_tbl.Rows.Count; i++)
                {
                    if (ls_matcode.Equals(SQLHelper.SNVL(taskDtls_tbl.Rows[i]["matcode"], "")))
                    {
                        li_found_mat = i; //
                        ls_MATCODECONTROL = SQLHelper.SNVL(taskDtls_tbl.Rows[i]["MATCODECONTROL"], ""); //相同物料此值应该是一样的
                        //
                        if ("0".Equals(ls_MATCODECONTROL))
                        { //序列号控制
                            if (ls_serialno == null || "".Equals(ls_serialno))
                            {
                                throw new Exception("物料需管理序列号,但条码内容中无序列号信息,不能处理");
                            }
                            if (ls_batch_no == null || "".Equals(ls_batch_no))
                            {
                                throw new Exception("物料需管理批次,但条码内容中无批次号信息,不能处理");
                            }
                            if (ls_matcode == SQLHelper.SNVL(taskDtls_tbl.Rows[i]["MATCODE"], "") &&
                                ls_batch_no == SQLHelper.SNVL(taskDtls_tbl.Rows[i]["BATCHNO"], "") &&
                                ls_serialno == SQLHelper.SNVL(taskDtls_tbl.Rows[i]["SN"], ""))
                            {
                                li_found_row_dtl = i; break;
                            }
                        }
                        else
                        {
                            if (ls_batch_no == null || "".Equals(ls_batch_no))
                            {
                                throw new Exception("物料需管理批次,但条码内容中无批次号信息,不能处理");
                            }
                            if (ls_matcode == SQLHelper.SNVL(taskDtls_tbl.Rows[i]["MATCODE"], "") &&
                                ls_batch_no == SQLHelper.SNVL(taskDtls_tbl.Rows[i]["BATCHNO"], ""))
                            {
                                li_found_row_dtl = i; break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 处理采集信息, 解析扫描得到的条码, 就修改后的数据写到datagrid 中暂存
        /// </summary>
        /// <param name="barcode"></param>
        private void PerformingBarcode(string barcode)
        {
            decimal li_collectQty = 0; //本次的采集数量
            Step currStep;
            if (string.IsNullOrEmpty(barcode)) throw new Exception("采集内容不能为空");

            #region  判断模式
            if (barcode.IndexOf('*') > 0)
            {
                currStep = Step._2DBarcode;
            }
            else if ((management.CheckQuantity(barcode)))//数量
            {
                currStep = Step.Quantity;
            }
            else
            {
                throw new Exception("采集内容不合法,");
            }
            #endregion

            #region 处理逻辑
            switch (currStep)
            {
                case Step._2DBarcode:
                    //
                    if (this.last_Step != Step.Quantity)
                    {
                        if (DialogResult.Yes == MessageBox.Show("上次扫描没确认数量,是否现在输入数量?", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                        {
                            lblMsg.Text = "请确认数量:"; return; //丢弃本次扫描得到的信息,然后等用户重新输入数量
                        }
                    }
                    //单一物料的信息 taskMat_tbl
                    TXT_matcodecontrol.Text = ""; matCodeLabel.Text = ""; lblMsg.Text = "请扫描条码:";
                    string ls_matcode = SQLHelper.getBarcodeField(barcode, "MC"); //物料
                    string ls_batch_no = SQLHelper.getBarcodeField(barcode, "BN"); //批号
                    string ls_serialno = SQLHelper.getBarcodeField(barcode, "SN"); //序列号
                    string ls_pdate = SQLHelper.getBarcodeField(barcode, "PD"); //生产日期
                    string ls_vdays = SQLHelper.getBarcodeField(barcode, "WD"); //有效期天数
                    string ls_qty = SQLHelper.getBarcodeField(barcode, "QT"); //数量(?包装数)
                    CultureInfo enUS = new CultureInfo("en-US");
                    string format = "yyyyMMdd";

                    if(ls_pdate !=null && ls_pdate !=""){
                     try
                    {
                        DateTime result = DateTime.ParseExact(ls_pdate, format, enUS, DateTimeStyles.None);
                     }
                    catch (FormatException)
                    {
                        throw new Exception("生产日期格式不符合要求 应该是yyyyMMdd格式，请核实。");
                    }
                    }

                    //
                    string ls_MATCODECONTROL = null;  //物料的批次,序列号管理 控制标记, --'0'序列控制，'1'批次控制，'2' 无批次序列控制
                    int li_found_row_dtl = -1, li_found_mat = -1;
                    bool ISNEWEWM = false; 
                    if (barcode.IndexOf("*BN") >= 0) { ISNEWEWM = true; } //是否新的标签格式

                    if (ls_matcode == null || "".Equals(ls_matcode) ||
                        ((ls_batch_no == null || "".Equals(ls_batch_no)) && (ls_serialno == null || "".Equals(ls_serialno))))
                    {
                        throw new Exception("条码内容中无物料批号/序列号信息,不能处理");
                    }
                    if (!ISNEWEWM)
                    {
                        bool lb_found_oldmatcode = false;
                        for (int i = 0; i < this.taskDtls_tbl.Rows.Count; i++)
                        {
                            if (ls_matcode.Equals(SQLHelper.SNVL(taskDtls_tbl.Rows[i]["MATINNERCODE"], "")))
                            {
                                ls_matcode = SQLHelper.SNVL(taskDtls_tbl.Rows[i]["MATCODE"], "");
                                ls_MATCODECONTROL = SQLHelper.SNVL(taskDtls_tbl.Rows[i]["MATCODECONTROL"], "");
                                lb_found_oldmatcode = true; break;
                            }
                        }
                        if (!lb_found_oldmatcode)
                        {
                            throw new Exception(string.Format("没有找到旧编码为" + ls_matcode + "的物料，请确认"));
                        }
                        if (ls_MATCODECONTROL == "0")
                        {
                            ls_batch_no = string.Empty;
                        }
                        else
                        {
                            ls_batch_no = ls_serialno; ls_serialno = string.Empty;
                        }
                    }
                    else {
                        if (ls_batch_no == null || "".Equals(ls_batch_no))
                        {
                            throw new Exception("条码内容中无物料批号信息,不能处理");
                        }
                        
                        for (int i = 0; i < this.taskDtls_tbl.Rows.Count; i++)
                        {
                            if (ls_matcode.Equals(SQLHelper.SNVL(taskDtls_tbl.Rows[i]["MATCODE"], "")))
                            {
                                ls_MATCODECONTROL = SQLHelper.SNVL(taskDtls_tbl.Rows[i]["MATCODECONTROL"], "");
                                break;
                            }
                        }
                       
                        if (ls_MATCODECONTROL =="2" || ls_MATCODECONTROL == "1")
                        {
                     
                            if (ls_serialno != null && !"".Equals(ls_serialno))
                            {
                                MessageBox.Show("该物料是批次或无控制物料 但条码内容中含有序列号", "提示");
                            
                            }
                        }

                    }
                    //判断物料,批号,是否存在
                    getRowNum(ls_matcode, ls_batch_no, ls_serialno, taskDtls_tbl, ref ls_MATCODECONTROL, ref li_found_mat, ref li_found_row_dtl);
                    if (li_found_mat < 0)
                    {
                        throw new Exception(string.Format("没有此物料" + ls_matcode + "的到货信息，请确认"));
                    }
                    decimal li_qty = 0;
                    if (li_found_row_dtl == -1)
                    {
                        if (MessageBox.Show("没有找到匹配的批次+序号的记录,是否采集?", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                        {
                            return; //录入的物料,批次在列表中不存在,提示是否进行采集
                        }
                    }
                    TXT_matcodecontrol.Text = ls_MATCODECONTROL;
                    matCodeLabel.Text = ls_matcode;
                    batchnoLabel.Text = ls_batch_no;
                    seqnoLabel.Text = ls_serialno;
                    pdateLabel.Text = ls_pdate; //生产日期
                    vdaysLabel.Text = ls_vdays; //保质期
                    txtQty.Text = "";
                    if (!"0".Equals(ls_MATCODECONTROL))
                    {
                        lblMsg.Text = "请确认数量:";
                        this.last_Step = Step._2DBarcode; 
                    }
                    else
                    {
                        li_collectQty = 1; this.last_Step = Step.Quantity; //上步骤已经完成了数量采集
                    }
                    break;
                case Step.Quantity:
                    if(matCodeLabel.Text == ""){
                        throw new Exception("请先扫描物料条码");
                    }
                    if ("0".Equals(TXT_matcodecontrol.Text))
                    {
                        this.last_Step = Step.Quantity;
                        throw new Exception("已采集序列号无需采集数量,请直接扫下一个物料");
                    }
                    li_collectQty = Convert.ToDecimal(barcode);
                    txtQty.Text = barcode; //记录数量信息
                    //
                    this.last_Step = Step.Quantity;
                    lblMsg.Text = "请扫描条码:";
                    break;
                default:
                    break;
            }
            #endregion
            //判断,是否提交到本地的缓存中
            if( this.last_Step == Step.Quantity)
            {
                string ls_MATCODECONTROL = ""; int li_found_mat = -1, li_found_row_dtl = -1;
                getRowNum( matCodeLabel.Text, batchnoLabel.Text,  seqnoLabel.Text, taskDtls_tbl, ref ls_MATCODECONTROL, ref li_found_mat, ref li_found_row_dtl);
                //MessageBox.Show("匹配到的行号：" + li_found_row_dtl, "提示信息"); 
                if (li_found_mat < 0)
                {
                    throw new Exception(string.Format("没有此物料" + matCodeLabel.Text + "的到货信息，请确认"));
                }
                bool lb_addnewrow = false; decimal ldc_oqty = 0, ldc_ogoodqty = 0, ldc_otmodqty = 0;
                if (li_found_row_dtl == -1)
                {
                    //向表中追加新记录, 设置新的li_found_row 的值
                    DataRow dr1 = taskDtls_tbl.NewRow();
                    for (int c = 0; c < taskDtls_tbl.Columns.Count; c++)
                    {
                        dr1[c] = this.taskDtls_tbl.Rows[li_found_mat][c];
                    }
                    dr1["QTY"] = 0; ldc_oqty = 0;
                    dr1["BATCHNO"] = batchnoLabel.Text;
                    dr1["SN"] = seqnoLabel.Text;
                    //注意可能还需要其他字段,如保质期和有效期
                    taskDtls_tbl.Rows.Add(dr1);
                    li_found_row_dtl = taskDtls_tbl.Rows.Count - 1; lb_addnewrow = true;
                }else{
                    ldc_oqty = SQLHelper.NNVL(taskDtls_tbl.Rows[li_found_row_dtl]["QTY"], 0);
                    ldc_ogoodqty = SQLHelper.NNVL(taskDtls_tbl.Rows[li_found_row_dtl]["GOODQTY"], 0);
                    ldc_otmodqty = SQLHelper.NNVL(taskDtls_tbl.Rows[li_found_row_dtl]["tmodqty"], 0);
                    if (li_collectQty > ldc_oqty)
                    {
                        decimal ldc_mat_tot = getMatTotQty(matCodeLabel.Text, taskDtls_tbl);
                        //查看是否超过了物料的总任务数,然后给出提示
                        if(li_collectQty > ldc_mat_tot)
                        {
                            MessageBox.Show("采集数大于任务数不能继续", "提示信息"); return;
                        }else{
                            if (DialogResult.Yes != MessageBox.Show("采集数大于当前批次的任务数,是否继续?", "提示信息",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1))
                            {
                                return;
                            }
                        }
                    }
                }
                //
                taskDtls_tbl.Rows[li_found_row_dtl]["tmodqty"] = li_collectQty; //写此物料的
                taskDtls_tbl.Rows[li_found_row_dtl]["GOODQTY"] = li_collectQty;  
                //
                
                //判断是否可以接受此数量
                DataTable pm_taskMat_tbl = this.taskMat_tbl.Copy();
                if (refresh_taskMat(taskDtls_tbl, pm_taskMat_tbl))
                {
                   // MessageBox.Show("行走1", "提示信息");
                    refresh_taskMat(taskDtls_tbl, taskMat_tbl);
                    
                    taskDtls_tbl.Rows[li_found_row_dtl]["pdate"] = pdateLabel.Text;
                    taskDtls_tbl.Rows[li_found_row_dtl]["vdays"] = vdaysLabel.Text;
                   // MessageBox.Show("行走2", "提示信息");
                    //
                }else{
                   // MessageBox.Show("行走3", "提示信息");
                    MessageBox.Show("采集总数大于任务总数不能继续","提示信息");//分摊不完啊
                    if(lb_addnewrow){
                        taskDtls_tbl.Rows.RemoveAt(li_found_row_dtl);
                    }else{
                        taskDtls_tbl.Rows[li_found_row_dtl]["GOODQTY"] = ldc_ogoodqty;
                        taskDtls_tbl.Rows[li_found_row_dtl]["tmodqty"] = ldc_otmodqty;
                    }
                }
                TXT_matcodecontrol.Text = ""; //是批次 还是 序列号控制, 清空当前处理的物料信息
                matCodeLabel.Text = ""; //物料编码
                batchnoLabel.Text = ""; //批次号
                seqnoLabel.Text = ""; //序列号
                pdateLabel.Text = ""; //生产日期
                vdaysLabel.Text = ""; //保质期
                txtQty.Text = "";
            }
        }

        /// <summary>
        /// 上架提交获取目标库位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("开发集成中", "提示");
        }

 
        /// <summary>
        /// 提交采集数据, 选中所有 tmodqty >= 0 的记录,提交后台,进行判断 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CjTjBN_Click(object sender, EventArgs e)
        {
            List<string> args_nv = new List<string>();
            args_nv.Add("userid"); args_nv.Add(User.Instance.UserData.UserId);
            args_nv.Add("billid"); args_nv.Add(taskNo);
            //
            int li_totlines = 0;

            string tmpMat = string.Empty;
            decimal taskQty = 0;
            decimal tmpQty = 0;
            string msg = string.Empty;

            for (int i = 0; i < this.taskDtls_tbl.Rows.Count; i++)
            {
                tmpMat = taskDtls_tbl.Rows[i]["MATCODE"];//物料
                taskQty = Convert.ToDecimal(taskDtls_tbl.Rows[i]["QTY"]);
                tmpQty = Convert.ToDecimal(taskDtls_tbl.Rows[i]["GOODQTY"]);
                if (taskQty != tmpQty)
                {
                    msg += string.Format("物料【{0}】还剩【{1}】未做", tmpMat, (taskQty - tmpQty));
                }
            }

            if (!string.IsNullOrEmpty(msg))
            {
                msg = msg.Remove(0, 1) + "，请确认是否提交？";
                if (MessageBox.Show(msg,
                        "上线采集", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    return;
                }
            }

            for (int i = 0; i < this.taskDtls_tbl.Rows.Count; i++)
            {
                decimal ldc_q =  SQLHelper.NNVL(taskDtls_tbl.Rows[i]["tmodqty"],-1); //提交后窗口关了,再次打开, tmodqty就已经清空复位了
                if(ldc_q > 0)
                {
                    args_nv.Add("matcod_"+i); args_nv.Add(SQLHelper.SNVL(taskDtls_tbl.Rows[i]["MATCODE"], ""));
                    args_nv.Add("batchn_"+i); args_nv.Add(SQLHelper.SNVL(taskDtls_tbl.Rows[i]["BATCHNO"], ""));
                    args_nv.Add("sn_"+i); args_nv.Add(SQLHelper.SNVL(taskDtls_tbl.Rows[i]["SN"], ""));
                    args_nv.Add("gooqty_"+i); args_nv.Add(SQLHelper.SNVL(taskDtls_tbl.Rows[i]["GOODQTY"], ""));
                    args_nv.Add("pdate_" + i); args_nv.Add(SQLHelper.SNVL(taskDtls_tbl.Rows[i]["pdate"], ""));
                    args_nv.Add("vdays_" + i); args_nv.Add(SQLHelper.SNVL(taskDtls_tbl.Rows[i]["vdays"], ""));
                    li_totlines++;
                }
            }
            
            args_nv.Add("_totlines"); args_nv.Add(""+li_totlines);
            try
            {
                SQLHelper s = new SQLHelper();
                DataSet ds = s.WS("PDA_DH_V2_CJTJ", args_nv.ToArray());
                if (ds == null || s.sqlcode == -1)
                {
                    MessageBox.Show("采集提交错,"+s.sqlerrtext, "提示");
                    return;
                }
                //
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    return;
                }
                //提交完了关窗口,再重新打开
                this.Close();
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        /// <summary>
        /// 显示本地缓存的已采集的信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void collectItemButton_Click(object sender, EventArgs e)
        {
            try
            {
                DaoHuoSaoMaFrmCjCollect collectedFrm = new DaoHuoSaoMaFrmCjCollect(this); //在Load中会用到taskDtls_tbl进行显示
                collectedFrm.ShowDialog();
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }
 
        //关闭时检查时候有未提交的数据
        private void closeButton_Click(object sender, EventArgs e)
        {
            decimal ldc_t_cj = 0, ldc_i = 0;
            for (int i = 0; i < this.taskDtls_tbl.Rows.Count; i++)
            {
                ldc_i = SQLHelper.NNVL(taskDtls_tbl.Rows[i]["tmodqty"], 0);
                if (ldc_i > 0) {
                    ldc_t_cj += ldc_i; 
                }
            }
            if (ldc_t_cj > 0)
            {
                if (MessageBox.Show(string.Format("当前采集数量是{0},是否确认关闭？", ldc_t_cj),
                  "到货采集", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                  MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    return;
                }
            }
            this.Close();
        }

        //输条码, 确认数量
        enum Step
        {
            _2DBarcode, Quantity
        }

 
 
    }
}