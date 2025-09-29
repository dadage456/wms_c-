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

namespace PDA
{
    public partial class DaoHuoSaoMaFrmDtl : Form
    {
        private string is_taskNo = "";
        public DaoHuoSaoMaFrmDtl(string pm_taskNo)
        {
            is_taskNo = pm_taskNo;
            InitializeComponent();
        }

        DataTable taskTable = new DataTable();
        DataView dv = new DataView();
        MiddleService service = new MiddleService();
        int currentRowIndex = -1;
   
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DaoHuoSaoMaFrmDtl_Load(object sender, EventArgs e)
        {
            taskDataGrid.DataSource = null;
            getReceivebyUser();
        }

        /// <summary>
        /// 显示用户可接收的下架任务
        /// </summary>
        private void getReceivebyUser()
        {
            try
            {
                taskDataGrid.DataSource = null;
                //
                List<string> args_nv = new List<string>();
                args_nv.Add("userid"); args_nv.Add(User.Instance.UserData.UserId); 
                args_nv.Add("billid"); args_nv.Add(is_taskNo);
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
                taskTable = ds.Tables[0];
                dv.Table = taskTable;
                taskDataGrid.DataSource = dv;

                string larr_colwidths = ds.Tables[0].Columns.IndexOf("colwidths") >= 0 ? SQLHelper.SNVL(ds.Tables[0].Rows[0]["colwidths"], "") : "";
                int li_noDispRCols = ds.Tables[0].Columns.IndexOf("nodisprcols") >= 0 ? (int)SQLHelper.NNVL(ds.Tables[0].Rows[0]["nodisprcols"], 0) : 0;
                SQLHelper.DataGridColumnSize(dv, taskDataGrid, null, li_noDispRCols, larr_colwidths); //显示任务单明细
                //AutoSizeCol(taskTable.Columns.Count);
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
            this.taskDataGrid.TableStyles.Clear() ;
            //定义DataGridTableStyle  
            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = dv.Table.TableName;  //映射style对应数据源的表名，很重要，否则无数据显示  

            //分别对列进行渲染，其中前三列用for循环实现，对列宽进行设定，值为75   
            for (int i = 0; i < p ; i++)
            {
                DataGridColumnStyle aColumnTextColumnStyle = new DataGridTextBoxColumn();//定义该列用textbox来进行渲染 
                aColumnTextColumnStyle.HeaderText = dv.Table.Columns[i].ColumnName;   //列头 
                aColumnTextColumnStyle.MappingName = dv.Table.Columns[i].ColumnName; //映射数据源的列名，很重要，否则无数据显示 
                if (i == 0) aColumnTextColumnStyle.Width = 150;//凭证号
                if (i == 1) aColumnTextColumnStyle.Width = 80; //库房号
                if (i == 2) aColumnTextColumnStyle.Width = 150;//出库单号
                if (i == 3) aColumnTextColumnStyle.Width = 120;//工位
                if (i == 4) aColumnTextColumnStyle.Width = 100;//任务号
                if (i == 5) aColumnTextColumnStyle.Width = 100;//来源单号
                if (i == 6) aColumnTextColumnStyle.Width = 0;
                if (i == 7) aColumnTextColumnStyle.Width = 0;
                if (i == 8) aColumnTextColumnStyle.Width = 0;
                if (i == 9) aColumnTextColumnStyle.Width = 0;
                if (i == 10) aColumnTextColumnStyle.Width = 100;
                if (i == 11) aColumnTextColumnStyle.Width = 200;
                ts.GridColumnStyles.Add(aColumnTextColumnStyle); 
            }
            this.taskDataGrid.TableStyles.Add(ts);
 
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}