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
    public partial class DaoHuoSaoMaFrmRecv : Form
    {
        public DaoHuoSaoMaFrmRecv()
        {
            InitializeComponent();
        }

        DataTable taskTable = new DataTable(); //查询到的结果列表
        DataView dv = new DataView();
        MiddleService service = new MiddleService();
        int currentRowIndex = -1;
   
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DaoHuoSaoMaFrmRecv_Load(object sender, EventArgs e)
        {
            taskDataGrid.DataSource = null;
            txtTaskNo.Text = "";
            getReceivebyUser(null);
        }

        //按指定的单号进行查询
        private void QueryBN_Click(object sender, EventArgs e)
        {
            string ls_taskNo = txtTaskNo.Text.Trim();
            if (ls_taskNo == null || ls_taskNo == "")
            {
                MessageBox.Show("请指定要查询的单号!", "提示信息");
                return;
            }
            getReceivebyUser(ls_taskNo);
        }

        /// <summary>
        /// 显示用户可接收的下架任务
        /// </summary>
        private void getReceivebyUser(string pm_taskno)
        {
            try
            {
                taskDataGrid.DataSource = null;
                //
                List<string> args_nv = new List<string>();
                args_nv.Add("userid"); args_nv.Add(User.Instance.UserData.UserId);
                args_nv.Add("ynlrw"); args_nv.Add("N"); //显示已领的任务
                if (pm_taskno != null && !pm_taskno.Equals( ""))
                {
                    args_nv.Add("billid"); args_nv.Add(pm_taskno); //参数名为id,实际存的是单号 
                }
                SQLHelper s = new SQLHelper();
                DataSet ds = s.WS("PDA_DH_V2_RWLIST", args_nv.ToArray());
                //
                if (ds == null || s.sqlcode == -1)
                {
                    MessageBox.Show("查询待到货验货信息错误", "提示");
                    return;
                }
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
                SQLHelper.DataGridColumnSize(dv, taskDataGrid, null, li_noDispRCols, larr_colwidths); //显示未领的到货任务单
               // AutoSizeCol(taskTable.Columns.Count);
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
        /// 选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        this.txtTaskNo.Text = taskDataGrid[taskDataGrid.CurrentRowIndex, 0].ToString();
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
        /// 执行接收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void collectButton_Click(object sender, EventArgs e)
        {
            try
            {
                string ls_taskNo = txtTaskNo.Text;
                if (string.IsNullOrEmpty(ls_taskNo)) throw new Exception("请先选中要接收的任务号");
                //任务号应在上面的列表中
                bool lb_found = false;
                for (int i = 0; i < taskTable.Rows.Count; i++)
                {
                    if (ls_taskNo.Equals(SQLHelper.SNVL(taskTable.Rows[i][0], ""))) //认为第0列是任务号
                    {
                        lb_found = true; break;
                    }
                }
                if (!lb_found)
                {
                    throw new Exception("任务号不在任务列表中，请核实");
                }
                //
                List<string> args_nv = new List<string>();
                args_nv.Add("userid"); args_nv.Add(User.Instance.UserData.UserId);
                args_nv.Add("billid"); args_nv.Add(ls_taskNo); //显示要领的单号
                SQLHelper s = new SQLHelper();
                DataSet ds = s.WS("PDA_DH_V2_RECV", args_nv.ToArray());
                //
                if (ds == null || s.sqlcode == -1)
                {
                    MessageBox.Show("领到货验货任务错误" + s.sqlerrtext, "提示");
                    return;
                }
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    //throw new Exception("暂无下架任务"); 
                    return;
                }
                //
                this.Close();
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        //查看明细
        private void QueryDtlBN_Click(object sender, EventArgs e)
        {
            string ls_taskNo = txtTaskNo.Text;
            if (string.IsNullOrEmpty(ls_taskNo))
            {
                Message.Alarm("提示", "任务号不能为空,请选择任务号!");
                return;
            }

            DaoHuoSaoMaFrmDtl gdReceiveItem = new DaoHuoSaoMaFrmDtl(ls_taskNo);
            gdReceiveItem.ShowDialog();
            getReceivebyUser(null);
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

        private void taskDataGrid_CurrentCellChanged(object sender, EventArgs e)
        {

        }

        

         
    }
}