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
    public partial class GoodsDownReceiveFrm : Form
    {
        public GoodsDownReceiveFrm(string strtaskComment, string strworkStation, string strTaskNo)
        {
            InitializeComponent();
            this.taskComment = strtaskComment;
            this.workStation = strworkStation;
            this.TaskNo = strTaskNo;
        }

        private string taskComment = string.Empty;
        private string workStation = string.Empty;
        private string TaskNo = string.Empty;

        DataTable taskTable = new DataTable();
        DataView dv = new DataView();
        MiddleService service = new MiddleService();
        int currentRowIndex = -1;
   
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoodsDownReceiveFrm_Load(object sender, EventArgs e)
        {
            taskDataGrid.DataSource = null;
            txtTaskNo.Text = "";
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
                DataSet ds = service.GetOutTaskItemByUserID("ALL", taskComment, "0", workStation, TaskNo);
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
                if (i == 0) aColumnTextColumnStyle.Width = 150;//凭证号
                if (i == 1) aColumnTextColumnStyle.Width = 80; //库房号
                if (i == 2) aColumnTextColumnStyle.Width = 100;//出库单号               
                if (i == 3) aColumnTextColumnStyle.Width = 100;//货位号
                if (i == 4) aColumnTextColumnStyle.Width = 150;//物料号
                if (i == 5) aColumnTextColumnStyle.Width = 120;//任务号
                if (i == 6) aColumnTextColumnStyle.Width = 100;//来源单号
                if (i == 7) aColumnTextColumnStyle.Width = 0;
                if (i == 8) aColumnTextColumnStyle.Width = 0;
                if (i == 9) aColumnTextColumnStyle.Width = 0;
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
                lbmsg.Text = "";
                DataGrid.HitTestInfo hti = taskDataGrid.HitTest(e.X, e.Y);
                switch (hti.Type)
                {
                    case DataGrid.HitTestType.ColumnHeader:     //列头排序
                        string columnHeader = taskTable.Columns[hti.Column].ColumnName;
                        dv.Sort = columnHeader + " ASC";
                        taskDataGrid.DataSource = dv;
                        break;
                    case DataGrid.HitTestType.RowHeader:        //行选中
                        this.txtProofNo.Text = taskDataGrid[taskDataGrid.CurrentRowIndex, 0].ToString();
                        this.roomLabel.Text = taskDataGrid[taskDataGrid.CurrentRowIndex, 1].ToString();
                        this.txtTaskNo.Text = taskDataGrid[taskDataGrid.CurrentRowIndex, 5].ToString();
                        currentRowIndex = taskDataGrid.CurrentRowIndex;
                        break;
                }
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }


        private void txtProofNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                lbmsg.Text = "";
                if (e.KeyCode == Keys.Return)
                {
                    string barcode = this.txtProofNo.Text.Trim();
                    if (barcode == "")
                    {
                        this.txtProofNo.Text = "";
                        this.txtProofNo.Focus();
                        return;
                    }

                    GetRoomByTaskNo(barcode);
                    collectButton_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }


        private void GetRoomByTaskNo(object taskNo)
        {
            DataRow[] dr = taskTable.Select("任务号='" + taskNo + "'");
            if (dr.Length <= 0)
                throw new Exception("接收任务号不在任务列表中，请核实");
            roomLabel.Text = dr[0][2].ToString();
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
                lbmsg.Text = "";
                //string taskNo = txtTaskNo.Text;
                //if (string.IsNullOrEmpty(taskNo)) throw new Exception("任务号为空");
                //CheckTaskNo(taskNo);
                //string ProofNo = txtProofNo.Text;
                //if (string.IsNullOrEmpty(ProofNo)) throw new Exception("凭证号不能为空");
                //string roomCode = roomLabel.Text.Trim();
                //if (string.IsNullOrEmpty(roomCode)) throw new Exception("库房号不能为空");

                //接收完毕将接收的任务移掉
                string outTaskItemId = string.Empty;
                for (int i = 0; i < taskTable.Rows.Count; i++)
                {
                    if (taskDataGrid.IsSelected(i))
                    {
                        outTaskItemId = taskDataGrid[i, 10].ToString();
                        service.CommitRCOutTaskItem(outTaskItemId, User.Instance.UserData.UserId, "0", false);
                    }
                    
                }
                int j = taskTable.Rows.Count - 1;
                for (int i = j; i >= 0; i--)
                {
                    if (taskDataGrid.IsSelected(i))
                    {
                        //接收完毕将接收的任务移掉
                        taskTable.Rows.RemoveAt(i);//移除指定行
                    }
                }
                
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        /// <summary>
        /// 任务号校验 在不在表格中
        /// </summary>
        /// <param name="taskNo"></param>
        private void CheckTaskNo(string taskNo)
        {
            DataRow[] dr = taskTable.Select("任务号='" + taskNo + "'");
            if (dr.Length <= 0)
                throw new Exception("接收任务号不在任务列表中，请核实");
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

        private void QueryButton_Click(object sender, EventArgs e)
        {
            taskDataGrid.DataSource = null;

            string s_QueryNo = txtQueryNo.Text.Trim();
            string ProofNo = txtProofNo.Text;

            DataSet ds = service.GetOutTaskItemByQueryNo("ALL", ProofNo, s_QueryNo, "0", workStation);

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

        private void SelectBN_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < taskTable.Rows.Count; i++)
            {
                taskDataGrid.Select(i);
            }
        }



         
    }
}