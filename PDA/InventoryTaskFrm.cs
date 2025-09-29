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
    public partial class InventoryTaskFrm : Form
    {
        public InventoryTaskFrm()
        {
            InitializeComponent();
        }

        Management management = Management.GetSingleton();
        DataTable taskTable = new DataTable();
        DataView dv = new DataView();
        string taskId = string.Empty;
        string taskNo = string.Empty;
        MiddleService service = new MiddleService();

        private void InventoryTaskFrm_Load(object sender, EventArgs e)
        {
            taskDataGrid.DataSource = null;
            txtComment.Text = "";
            txtTaskNo.Text = "";
            getTaskListbyUser();
            txtComment.Focus();
        }
         

        private void taskNoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Return)
                {
                    string barcode = this.txtComment.Text.Trim();
                    if (barcode == "")
                    {
                        this.txtComment.Text = "";
                        this.txtComment.Focus();
                        this.txtComment.SelectAll();
                        return;
                    }

                    GetRoom(barcode);
                    collectButton_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        private void GetRoom(object strTaskNo)
        {
            DataRow[] dr = taskTable.Select("任务号='" + strTaskNo + "'");
            if (dr.Length <= 0)
                throw new Exception("采集任务号不在任务列表中，请核实");
            roomLabel.Text = dr[0][1].ToString();
        }

        private void collectButton_Click(object sender, EventArgs e)
        {
            try
            {
                string taskComment = txtComment.Text.Trim();
                taskNo = txtTaskNo.Text.Trim();
                if (string.IsNullOrEmpty(taskNo)) throw new Exception("任务号为空");
                string roomCode = roomLabel.Text.Trim();
                if (string.IsNullOrEmpty(roomCode)) throw new Exception("库房号不能为空");

                CheckComment(taskNo);

                InventoryTaskItemFrm frm = new InventoryTaskItemFrm(taskNo, taskId,taskComment, roomCode);
                frm.ShowDialog();
                getTaskListbyUser();
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        private void CheckComment(string strTaskNo)
        {
            DataRow[] dr = taskTable.Select("任务号='" + strTaskNo + "'");
            if (dr.Length <= 0)  throw new Exception("采集任务号不在任务列表中，请核实");
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
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
                        string taskComment = taskDataGrid[taskDataGrid.CurrentRowIndex, 0].ToString();
                        string roomNo = taskDataGrid[taskDataGrid.CurrentRowIndex, 1].ToString();
                        string strTaskNo = taskDataGrid[taskDataGrid.CurrentRowIndex, 3].ToString();
                        string taskId = taskDataGrid[taskDataGrid.CurrentRowIndex, 5].ToString();                        
                        txtComment.Text = taskComment;
                        txtTaskNo.Text = strTaskNo;
                        roomLabel.Text = roomNo;
                        break;
                }
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        private void taskButton_Click(object sender, EventArgs e)
        {
            InventoryTaskReceiveFrm gdReceive = new InventoryTaskReceiveFrm();
            gdReceive.ShowDialog();
            getTaskListbyUser();
        }
        /// <summary>
        /// 显示用户下架任务
        /// </summary>
        private void getTaskListbyUser()
        {
            try
            {
                txtComment.Text = "";
                txtTaskNo.Text = "";
                roomLabel.Text = "";
                taskDataGrid.DataSource = null;
                DataSet ds = service.GetInventoryTask(User.Instance.UserData.UserId, User.Instance.UserData.UserId,"0");
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

        private void AutoSizeCol(int p)
        {
            this.taskDataGrid.TableStyles.Clear();
            //定义DataGridTableStyle  
            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = dv.Table.TableName;  //映射style对应数据源的表名，很重要，否则无数据显示  

            //分别对列进行渲染
            for (int i = 0; i < p; i++)
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

        private void btnCanel_Click(object sender, EventArgs e)
        {
            string taskComment = txtComment.Text.Trim();
            string strTaskNo = txtTaskNo.Text.Trim();
            if (string.IsNullOrEmpty(strTaskNo))
            {
                Message.Alarm("提示", "任务号为空");
                return;
            }

            DataRow[] dr = taskTable.Select("任务号='" + strTaskNo + "'");
            if (dr.Length <= 0)
            {
                Message.Alarm("提示", "采集任务号不在任务列表中，请核实");
                return;
            }

            string roomCode = roomLabel.Text.Trim();
            if (string.IsNullOrEmpty(roomCode))
            {
                Message.Alarm("提示", "库房号不能为空");
                return;
            }

            try
            {
                service.CommitInventoryTask(strTaskNo, User.Instance.UserData.UserId, true);
                Message.Alarm("提示", string.Format("取消凭证号：【{0}】的任务成功", strTaskNo));
                getTaskListbyUser();
            }
            catch (Exception ee)
            {
                Message.Alarm("提示", ee.Message);
            }
        }
    }
}