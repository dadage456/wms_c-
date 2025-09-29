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
    public partial class ASWHCheckOrderPalletNo : Form
    {
        public ASWHCheckOrderPalletNo(string strtaskComment, string strtaskId, string strtaskNo, string strtaskType)
        {
            InitializeComponent();
            this.taskId = strtaskId;
            this.taskNo = strtaskNo;
            this.taskType = strtaskType;
            this.taskComment = strtaskComment;
        }

        private string taskId = string.Empty;
        private string taskNo = string.Empty;    
        private string taskType = string.Empty;
        private string taskComment = string.Empty;

        DataTable taskTable = new DataTable();
        DataView dv = new DataView();
        MiddleService service = new MiddleService();
        int currentRowIndex = -1;
   
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ASWHWmsToWcs_Load(object sender, EventArgs e)
        {
            taskDataGrid.DataSource = null;
            getReceivebyUser();

            DataSet InOutds = service.GetInOutLocation("1");
            DataTable InOutdt = InOutds.Tables[0];
            INOUTComboBox.DataSource = InOutdt;
        }

        /// <summary>
        /// 显示用户可接收的下架任务
        /// </summary>
        private void getReceivebyUser()
        {
            try
            {
                taskDataGrid.DataSource = null;
                DataSet ds = service.GetCheckOrderPalletNoByTaskID(taskComment, taskId, taskType);
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

                if (i == 0) aColumnTextColumnStyle.Width = 120;//托盘号
                if (i == 1) aColumnTextColumnStyle.Width = 120; //库位
                if (i == 2) aColumnTextColumnStyle.Width = 100;//任务号               
                if (i == 3) aColumnTextColumnStyle.Width = 100;//库房
                if (i == 4) aColumnTextColumnStyle.Width = 0;//
                if (i == 5) aColumnTextColumnStyle.Width = 0;//
                if (i == 6) aColumnTextColumnStyle.Width = 0;//
                
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

        private void INButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("请确认来料托盘回库吗？",
                      "在线盘点", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                      MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    return;
                }
                if (InvTaskCollectData.Instance.Collect.Count > 0) throw new Exception("采集数据未提交,不允许来料盘回库！");
                if (taskNo.Equals(string.Empty)) throw new Exception("任务号为空，请确认");
                if (taskId.Equals(string.Empty)) throw new Exception("任务ID为空，请确认");
                string startAddr = INOUTComboBox.SelectedValue.ToString();
                if (startAddr.Equals(string.Empty)) throw new Exception("拣选口位置不能为空");

                string sourcetrayNo = string.Empty;
                string endAddr = string.Empty;                
                for (int i = 0; i < taskTable.Rows.Count; i++)
                {
                    if (taskDataGrid.IsSelected(i))
                    {
                        sourcetrayNo = taskDataGrid[i, 0].ToString();
                        endAddr = taskDataGrid[i, 1].ToString();

                        if (sourcetrayNo.Equals(string.Empty)) throw new Exception("托盘号不能为空");
                        if (endAddr.Equals(string.Empty)) throw new Exception("目的地址不能为空");
                        service.CommitInvResetWmsToWcs(User.Instance.UserData.UserId, taskId, taskNo, sourcetrayNo, startAddr, endAddr);
                    }
                }                
                Message.Alarm("成功", "盘点来料盘回库成功,请等待");
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        private void WCSButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("请确认获取来料托盘吗？",
                      "在线盘点", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                      MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    return;
                }
                if (InvTaskCollectData.Instance.Collect.Count > 0) throw new Exception("采集数据未提交,不允许获取新的来料盘！");
                if (taskNo.Equals(string.Empty)) throw new Exception("任务号为空，请确认");
                if (taskId.Equals(string.Empty)) throw new Exception("任务ID为空，请确认");
                string endAddr = INOUTComboBox.SelectedValue.ToString();
                if (endAddr.Equals(string.Empty)) throw new Exception("拣选口位置不能为空");

                string sourcetrayNo = string.Empty;
                string startAddr = string.Empty;
                for (int i = 0; i < taskTable.Rows.Count; i++)
                {
                    if (taskDataGrid.IsSelected(i))
                    {
                        sourcetrayNo = taskDataGrid[i, 0].ToString();
                        startAddr = taskDataGrid[i, 1].ToString();
                        if (sourcetrayNo.Equals(string.Empty)) throw new Exception("来料盘号不能为空");
                        if (startAddr.Equals(string.Empty)) throw new Exception("起始地址不能为空");
                        service.CommitInvDownWmsToWcs(User.Instance.UserData.UserId, taskId, taskNo, sourcetrayNo, startAddr, endAddr,"1");
                    }
                } 
                
                Message.Alarm("成功", "获取盘点来料盘成功,请等待");
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }
         
    }
}