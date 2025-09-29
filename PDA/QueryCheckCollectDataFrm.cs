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
    public partial class QueryCheckCollectDataFrm : Form
    {
        public QueryCheckCollectDataFrm(string strtaskComment, string strtaskId, string strtaskNo, string strtrayNo, string strtaskType)
        {
            InitializeComponent();
            this.taskId = strtaskId;
            this.taskNo = strtaskNo;
            this.taskType = strtaskType;
            this.trayNo = strtrayNo;
            this.taskComment = strtaskComment;

        }

        private string taskId = string.Empty;
        private string trayNo = string.Empty;
        private string taskType = string.Empty;
        private string taskComment = string.Empty;
        private string taskNo = string.Empty;
        private string sourcetrayNo = string.Empty;
        private string startAddr = string.Empty;
        private string endAddr = string.Empty;        
        

        DataTable taskTable = new DataTable();
        DataView dv = new DataView();
        MiddleService service = new MiddleService();        
   
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryCheckCollectData_Load(object sender, EventArgs e)
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
                DataSet ds = service.GetCheckCollectDataByTrayNo(taskComment, taskId, taskNo, trayNo, taskType);
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    //throw new Exception("暂无下架任务"); 
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
                if (taskType == "00")
                {
                    if (i == 0) aColumnTextColumnStyle.Width = 0;//托盘号
                }
                else
                {
                    if (i == 0) aColumnTextColumnStyle.Width = 150;//托盘号
                }                
                if (i == 1) aColumnTextColumnStyle.Width = 100; //货位号
                if (i == 2) aColumnTextColumnStyle.Width = 100;//物料编号               
                if (i == 3) aColumnTextColumnStyle.Width = 100;//批次号
                if (i == 4) aColumnTextColumnStyle.Width = 150;//序列
                if (i == 5) aColumnTextColumnStyle.Width = 120;//数量
                if (i == 6) aColumnTextColumnStyle.Width = 100;//物料名称              
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
            //this.txtProofNo.Text = "";
            //this.txtPalletNo.Text = "";
            //this.txtTaskNo.Text = "";
            taskNo = "";
            sourcetrayNo = "";
            startAddr = "";
            endAddr = "";
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
                        //托盘号
                        //this.txtPalletNo.Text = taskDataGrid[taskDataGrid.CurrentRowIndex, 0].ToString();
                        sourcetrayNo = taskDataGrid[taskDataGrid.CurrentRowIndex, 0].ToString();                       
                        break;
                }

                
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
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

        private void Refreshbutton_Click(object sender, EventArgs e)
        {
            taskDataGrid.DataSource = null;
            getReceivebyUser();
        }
         
    }
}