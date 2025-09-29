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
    public partial class ASWHInventoryTaskReceiveFrm : Form
    {
        public ASWHInventoryTaskReceiveFrm()
        {
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
        private void InventoryTaskReceiveFrm_Load(object sender, EventArgs e)
        {
            taskDataGrid.DataSource = null;
            txtComment.Text = "";
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
                DataSet ds = service.GetInventoryTask("ALL",User.Instance.UserData.UserId,"1");
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

            //分别对列进行渲染
            for (int i = 0; i < p ; i++)
            {
               
                DataGridColumnStyle aColumnTextColumnStyle = new DataGridTextBoxColumn();//定义该列用textbox来进行渲染 
                aColumnTextColumnStyle.HeaderText = dv.Table.Columns[i].ColumnName;   //列头 
                aColumnTextColumnStyle.MappingName = dv.Table.Columns[i].ColumnName; //映射数据源的列名，很重要，否则无数据显示 
                if (i == 0) aColumnTextColumnStyle.Width = 170;
                if (i == 1) aColumnTextColumnStyle.Width = 120;
                if (i == 2) aColumnTextColumnStyle.Width = 120;
                if (i == 3) aColumnTextColumnStyle.Width = 120;
                if (i == 4) aColumnTextColumnStyle.Width = 120;
                if (i == 5) aColumnTextColumnStyle.Width = 0;

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
                        string roomNo = taskDataGrid[taskDataGrid.CurrentRowIndex, 1].ToString();
                        txtComment.Text = taskDataGrid[taskDataGrid.CurrentRowIndex, 0].ToString();
                        txtTaskNo.Text = taskDataGrid[taskDataGrid.CurrentRowIndex, 3].ToString();
                        roomLabel.Text = roomNo;
                        currentRowIndex = taskDataGrid.CurrentRowIndex;
                        break;
                }
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }
         

        private void txtComment_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                lbmsg.Text = "";
                if (e.KeyCode == Keys.Return)
                {
                    string barcode = this.txtComment.Text.Trim();
                    if (barcode == "")
                    {
                        this.txtComment.Text = "";
                        this.txtComment.Focus();
                        return;
                    }

                    GetRoomByComment(barcode);
                    collectButton_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }


        private void GetRoomByComment(object strTaskNo)
        {
            DataRow[] dr = taskTable.Select("任务号='" + strTaskNo + "'");
            if (dr.Length <= 0)
                throw new Exception("接收凭证号不在任务列表中，请核实");
            roomLabel.Text = dr[0][1].ToString();
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
                string taskcomment = txtComment.Text;
                string strTaskNo = txtTaskNo.Text;
                if (string.IsNullOrEmpty(strTaskNo)) throw new Exception("任务号为空");
                CheckTaskComment(strTaskNo);
                string roomCode = roomLabel.Text.Trim();
                if (string.IsNullOrEmpty(roomCode)) throw new Exception("库房号不能为空");
                service.CommitInventoryTask(strTaskNo, User.Instance.UserData.UserId, false);
     
               //接收完毕将接收的任务移掉
                DataRow[] drselect = taskTable.Select("任务号='" + strTaskNo + "'");
                if (drselect.Length > 0)
                {
                    taskTable.Rows.Remove(drselect[0]);//移除指定行
                    if (currentRowIndex >= taskTable.Rows.Count)
                    {
                        currentRowIndex = currentRowIndex - 1;
                    }
                    lbmsg.Text = string.Format("任务号：{0}接收成功！", strTaskNo);
                    if (taskTable.Rows.Count > 0)
                    {
                        taskDataGrid.CurrentRowIndex = currentRowIndex;
                        taskcomment = taskDataGrid[taskDataGrid.CurrentRowIndex, 0].ToString();
                        string roomNo = taskDataGrid[taskDataGrid.CurrentRowIndex, 1].ToString();
                        strTaskNo = taskDataGrid[taskDataGrid.CurrentRowIndex, 3].ToString();
                        txtComment.Text = taskcomment;
                        txtTaskNo.Text = strTaskNo;
                        roomLabel.Text = roomNo;
                    }
                    else
                    {
                        txtComment.Text = "";
                        txtTaskNo.Text = "";
                        roomLabel.Text = "";
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
        private void CheckTaskComment(string strTaskNo)
        {
            DataRow[] dr = taskTable.Select("任务号='" + strTaskNo + "'");
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

        private void taskNoTextBox_KeyDown(object sender, KeyEventArgs e)
        {

        }



         
    }
}