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
    public partial class ASWHWmsToWcs : Form
    {
        public ASWHWmsToWcs(string strtaskComment, string strtaskId, string strtaskType)
        {
            InitializeComponent();
            this.taskId = strtaskId;
            this.taskType = strtaskType;
            this.taskComment = strtaskComment;

        }

        private string taskId = string.Empty;
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
        private void ASWHWmsToWcs_Load(object sender, EventArgs e)
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
                DataSet ds = service.GetWmsToWcsByTaskID(taskComment, taskId, taskType);
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
                if (i == 0) aColumnTextColumnStyle.Width = 150;//托盘号
                if (i == 1) aColumnTextColumnStyle.Width = 100; //起始地址
                if (i == 2) aColumnTextColumnStyle.Width = 100;//目标地址               
                if (i == 3) aColumnTextColumnStyle.Width = 100;//堆垛机号
                if (i == 4) aColumnTextColumnStyle.Width = 150;//发送时间
                if (i == 5) aColumnTextColumnStyle.Width = 120;//状态
                if (i == 6) aColumnTextColumnStyle.Width = 100;//重量等级
                if (i == 7) aColumnTextColumnStyle.Width = 100;//高度等级
                if (i == 8) aColumnTextColumnStyle.Width = 100;//任务号
                if (i == 9) aColumnTextColumnStyle.Width = 100;//凭证号
                if (i == 10) aColumnTextColumnStyle.Width = 100;//任务类型
                if (i == 11) aColumnTextColumnStyle.Width = 100;//更改类型
                if (i == 12) aColumnTextColumnStyle.Width = 100;//出入库
                if (i == 13) aColumnTextColumnStyle.Width = 100;//WCS错误
                if (i == 14) aColumnTextColumnStyle.Width = 100;//任务ID
                if (i == 15) aColumnTextColumnStyle.Width = 100;//凭证ID
                if (i == 16) aColumnTextColumnStyle.Width = 100;//指令ID
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
            this.txtProofNo.Text = "";
            this.txtPalletNo.Text = "";
            this.txtTaskNo.Text = "";
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
                        this.txtPalletNo.Text = taskDataGrid[taskDataGrid.CurrentRowIndex, 0].ToString();
                        sourcetrayNo = taskDataGrid[taskDataGrid.CurrentRowIndex, 0].ToString();
                        //起始地址
                        startAddr = taskDataGrid[taskDataGrid.CurrentRowIndex, 1].ToString();
                        //目标地址   
                        endAddr = taskDataGrid[taskDataGrid.CurrentRowIndex, 2].ToString();
                        //任务号
                        this.txtTaskNo.Text = taskDataGrid[taskDataGrid.CurrentRowIndex, 8].ToString();
                        taskNo = taskDataGrid[taskDataGrid.CurrentRowIndex, 8].ToString();
                        //凭证号
                        this.txtProofNo.Text = taskDataGrid[taskDataGrid.CurrentRowIndex, 9].ToString();
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

        private void CancelDownBN_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("请确认撤销来料盘出库指令吗？",
                      "在线拣选", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                      MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    return;
                }
                if (string.IsNullOrEmpty(this.txtProofNo.Text.Trim())) throw new Exception("凭证号为空，请确认");
                if (taskId.Equals(string.Empty)) throw new Exception("任务号为空，请确认");
                if (taskNo.Equals(string.Empty)) throw new Exception("任务号为空，请确认");
                if (sourcetrayNo.Equals(string.Empty)) throw new Exception("托盘号为空，请确认");
                if (startAddr.Equals(string.Empty)) throw new Exception("起始地址为空，请确认");
                if (endAddr.Equals(string.Empty)) throw new Exception("目标地址为空，请确认");

               
                service.CancelDownWmsToWcs(User.Instance.UserData.UserId, taskId, taskNo, sourcetrayNo, startAddr, endAddr);

                

                Message.Alarm("成功", "撤销获取来料盘指令成功,请等待");
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        private void CancelResetBN_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("请确认撤销来料盘回库指令吗？",
                      "在线拣选", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                      MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    return;
                }
                if (string.IsNullOrEmpty(this.txtProofNo.Text.Trim())) throw new Exception("凭证号为空，请确认");
                if (taskId.Equals(string.Empty)) throw new Exception("任务号为空，请确认");
                if (taskNo.Equals(string.Empty)) throw new Exception("任务号为空，请确认");
                if (sourcetrayNo.Equals(string.Empty)) throw new Exception("托盘号为空，请确认");
                if (startAddr.Equals(string.Empty)) throw new Exception("起始地址为空，请确认");
                if (endAddr.Equals(string.Empty)) throw new Exception("目标地址为空，请确认");


                service.CancelResetWmsToWcs(User.Instance.UserData.UserId, taskId, taskNo, sourcetrayNo, startAddr, endAddr);



                Message.Alarm("成功", "撤销来料盘回库指令成功,请等待");
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        private void QueryButton_Click(object sender, EventArgs e)
        {
            taskDataGrid.DataSource = null;
            string s_QueryNo = txtQueryNo.Text.Trim();

            DataSet ds = service.GetWmsToWcsByQueryStr(taskComment, taskId, taskType, s_QueryNo);
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

        

       



         
    }
}