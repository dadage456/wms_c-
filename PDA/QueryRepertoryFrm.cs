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
    public partial class QueryRepertoryFrm : Form
    {
        public QueryRepertoryFrm()
        {
            InitializeComponent();            
        }

        private string taskNo=string.Empty;
        private string taskComment = string.Empty;
        private string storeRoom = string.Empty;

        MiddleService service = new MiddleService();
        Management management = Management.GetSingleton();
        DataTable roomTable = new DataTable();
        DataView dv = new DataView();
        DataTable taskTable = new DataTable();

        private string matCode = string.Empty;
        private string storeSite = string.Empty;
        private string trayNo = string.Empty;        
        private string erpStoreSite = string.Empty;//ERP子库
        private string erpStoreInv = string.Empty;//库存erp子库

        private string orderby = string.Empty;
        private string sortColumn = string.Empty;
        private string preOrderby = string.Empty;
        private string preSortColumn = string.Empty;
        
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryRepertoryFrm_Load(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "请扫描：";
                tbxBarcode.Text = "";
                tbxBarcode.Focus();
                tbxBarcode.SelectAll();


                //根据扫描托盘号/物料号/货位号获取库存明细
                /*
                this.detailListView.Columns.Clear();
                detailListView.Columns.Add("物料号", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("库位", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("库存数", 70, HorizontalAlignment.Left);
                detailListView.Columns.Add("批号", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("序列号", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("库房", 100, HorizontalAlignment.Left);
                detailListView.Columns.Add("ERP子库", 120, HorizontalAlignment.Left);
                detailListView.Columns.Add("托盘号", 100, HorizontalAlignment.Left);//
            */
                //DataSet ds = service.GetRepertoryByBarCode("ALL", "0");
                //DataTable dt = ds.Tables[0];
                //foreach (DataRow dr in dt.Rows)
                //{
                //    detailListView.Items.Add(new ListViewItem(
                //    new string[] { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString() }));
                //}

                #region 设定参数
                

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

            matCodeLabel.Text = "";
            trayLabel.Text = "";
            lbInv.Text = "";

            matCode = string.Empty;
            
        }

        /// <summary>
        /// 扫描条码执行
        /// </summary>
        /// <param name="barcode"></param>
        private void PerformingBarcode(string barcode)
        {
            if (string.IsNullOrEmpty(barcode)) throw new Exception("采集凭证号不能为空");
         
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
            else if (barcode.StartsWith("$TP$"))//采集托盘信息
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
                case Step._2DBarcode:
                    #region 物料条码逻辑
                    //if (barcode.IndexOf('*') < 0) throw new Exception("采集内容不合法，请采集二维码");
                    BarcodeContent barcodeContent = service.AnalysisForNewBarcode(barcode);
                    matCode = barcodeContent.MatCode;
                    matCodeLabel.Text = matCode;                
                    //检验库存
                    lbInv.Text = "";
                    trayLabel.Text = "";
                    siteLabel.Text = "";
                    QueryRepertory(matCode, "M");
                    #endregion
                    break;
                case Step.TrayNo:
                    #region
                    trayNo = barcode.Substring(4);
                    trayLabel.Text = trayNo;
                    //检验库存
                    lbInv.Text = "";
                    matCodeLabel.Text = "";
                    siteLabel.Text = "";
                    QueryRepertory(trayNo, "P");
                    #endregion
                    break;
                case Step.Site:         //$KW$+库位号
                    string[] sArry = barcode.Split('$');                    
                    storeSite = sArry[2];                  
                    siteLabel.Text = storeSite;
                    //检验库存
                    matCodeLabel.Text = "";
                    trayLabel.Text = "";
                    lbInv.Text = "";
                    QueryRepertory(storeSite, "S");
                    break;                
                default:
                    break;
            }            
            #endregion
        }

        /// <summary>
        /// //根据扫描托盘号/物料号/货位号获取库存明细
        /// </summary>
        /// <param name="barcode">扫描内容</param>
        /// <param name="currStep">类型</param>
        /// <returns></returns>
        private void QueryRepertory(string barcode, string currStep)
        {
            //根据扫描托盘号/物料号/货位号获取库存明细
            /*this.detailListView.Items.Clear();
            this.detailListView.Columns.Clear();
            detailListView.Columns.Add("物料号", 120, HorizontalAlignment.Right);
            detailListView.Columns.Add("库位", 120, HorizontalAlignment.Left);
            detailListView.Columns.Add("库存数", 70, HorizontalAlignment.Right);
            detailListView.Columns.Add("批号", 120, HorizontalAlignment.Left);
            detailListView.Columns.Add("序列号", 120, HorizontalAlignment.Left);
            detailListView.Columns.Add("库房", 100, HorizontalAlignment.Left);
            detailListView.Columns.Add("ERP子库", 120, HorizontalAlignment.Left);
            detailListView.Columns.Add("托盘号", 100, HorizontalAlignment.Left);//    
            */
            DataSet ds = service.GetRepertoryByBarCode(barcode, currStep);
            //DataTable dt = ds.Tables[0];

            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                //throw new Exception("暂无上架任务"); 
                return;
            }
            detailListView.MouseUp -= taskDataGrid_MouseUp;
            taskTable = ds.Tables[0];
            dv.Table = taskTable;
            detailListView.DataSource = dv;
            AutoSizeCol(taskTable.Columns.Count);
            detailListView.MouseUp += taskDataGrid_MouseUp;
            
            
            
            
            decimal repqty = 0;
            foreach (DataRow dr in taskTable.Rows)
            {
               // detailListView.Items.Add(new ListViewItem(
                //new string[] { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString() }));

                repqty = repqty + Convert.ToDecimal(dr[2].ToString());
            }

            lbInv.Text = repqty.ToString();
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
            
            else
            {
                return string.Format("{0}", msg);
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
          

        enum Step
        {
            TrayNo, _2DBarcode, Site, Quantity
        }

        private void AutoSizeCol(int p)
        {
            //2016 3 9 增加删除  柏磊
            this.detailListView.TableStyles.Clear();
            //定义DataGridTableStyle  
            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = dv.Table.TableName;  //映射style对应数据源的表名，很重要，否则无数据显示  

            //分别对列进行渲染，其中前三列用for循环实现，对列宽进行设定，值为75   
            for (int i = 0; i < p; i++)
            {
                DataGridColumnStyle aColumnTextColumnStyle = new DataGridTextBoxColumn();//定义该列用textbox来进行渲染 
                aColumnTextColumnStyle.HeaderText = dv.Table.Columns[i].ColumnName;   //列头 
                aColumnTextColumnStyle.MappingName = dv.Table.Columns[i].ColumnName; //映射数据源的列名，很重要，否则无数据显示 

                if (i == 0) aColumnTextColumnStyle.Width = 110;//凭证号
                if (i == 1) aColumnTextColumnStyle.Width = 115; //库房号
                if (i == 2) aColumnTextColumnStyle.Width = 100;//入库单号
                if (i == 3) aColumnTextColumnStyle.Width = 180;//工位
                if (i == 4) aColumnTextColumnStyle.Width = 300;//任务号
                if (i == 5) aColumnTextColumnStyle.Width = 100;//来源单号
                if (i == 6) aColumnTextColumnStyle.Width = 100;
                if (i == 7) aColumnTextColumnStyle.Width = 100;

                ts.GridColumnStyles.Add(aColumnTextColumnStyle);
            }
            this.detailListView.TableStyles.Add(ts);
        }

        private void taskDataGrid_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                DataGrid.HitTestInfo hti = detailListView.HitTest(e.X, e.Y);
                switch (hti.Type)
                {
                    case DataGrid.HitTestType.ColumnHeader:     //列头排序
                        string columnHeader = taskTable.Columns[hti.Column].ColumnName;
                        dv.Sort = columnHeader + " ASC";
                        detailListView.DataSource = dv;
                        break;
                }
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        /*
        private void detailListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            Message.Alarm("提示", this.detailListView.Columns[e.Column].Text);
            
            sortColumn = this.detailListView.Columns[e.Column].Text;
            if (string.IsNullOrEmpty(orderby))
            {
                orderby = "ASC";
            }

            if (preSortColumn.Equals(preSortColumn))
            {
                if (preOrderby.Equals(orderby))
                {
                    if (orderby.Equals("ASC"))
                    {
                        orderby = "DESC";
                    }

                    if (orderby.Equals("DESC"))
                    {
                        orderby = "ASC";
                    }
                }
            }
            else {
                orderby = "ASC";
            }
            
            preSortColumn = sortColumn;
            preOrderby = orderby;

            string conMatcode = matCodeLabel.Text;
            string conTray = trayLabel.Text;
            string conSite = siteLabel.Text;

            if (!conMatcode.Equals(string.Empty))
            {
                QueryRepertory(conMatcode, "M");
            }

            if (!conTray.Equals(string.Empty))
            {
                QueryRepertory(conTray, "P");
            }

            if (!conSite.Equals(string.Empty))
            {
                QueryRepertory(conSite, "S");
            }

        }*/

    }
}