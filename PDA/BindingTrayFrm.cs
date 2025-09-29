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
    public partial class BindingTrayFrm : Form
    {
        public BindingTrayFrm()
        {
            InitializeComponent(); 
        }

        Management management = Management.GetSingleton();
        DataTable taskTable = new DataTable();
        DataView dv = new DataView();
        MiddleService service = new MiddleService();
        string taskNo = string.Empty;
        private void BindingTrayFrm_Load(object sender, EventArgs e)
        {
            try
            {
                taskDataGrid.DataSource = null;
                txtComment.Text = "";
                getTaskListbyUser();
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
                return;
            } 
        }

        private void taskButton_Click(object sender, EventArgs e)
        {
            GoodsUpReceiveFrm receive = new GoodsUpReceiveFrm("<>");
            receive.ShowDialog();
            getTaskListbyUser();
        }


        /// <summary>
        /// 显示用户上架任务
        /// </summary>
        private void getTaskListbyUser()
        {
            try
            {
                txtComment.Text = "";
                roomLabel.Text = "";
                taskDataGrid.DataSource = null;
                DataSet ds = service.GetInTask(User.Instance.UserData.UserId,"<>");
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    //throw new Exception("暂无上架任务"); 
                    return;
                }
                taskDataGrid.MouseUp -= taskDataGrid_MouseUp;
                taskTable = ds.Tables[0];
                dv.Table = taskTable;
                taskDataGrid.DataSource = dv;
                AutoSizeCol(taskTable.Columns.Count);
                taskDataGrid.MouseUp += taskDataGrid_MouseUp;
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

            //分别对列进行渲染，其中前三列用for循环实现，对列宽进行设定，值为75   
            for (int i = 0; i < p; i++)
            {
                DataGridColumnStyle aColumnTextColumnStyle = new DataGridTextBoxColumn();//定义该列用textbox来进行渲染 
                aColumnTextColumnStyle.HeaderText = dv.Table.Columns[i].ColumnName;   //列头 
                aColumnTextColumnStyle.MappingName = dv.Table.Columns[i].ColumnName; //映射数据源的列名，很重要，否则无数据显示 

                if (i >= 5 && i <= 7)
                {
                    aColumnTextColumnStyle.Width = 0; //在这里就可以对列宽进行设置了     
                }
                else
                {
                    aColumnTextColumnStyle.Width = 145; //在这里就可以对列宽进行设置了     
                }
                ts.GridColumnStyles.Add(aColumnTextColumnStyle);
            }
            this.taskDataGrid.TableStyles.Add(ts);
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

        private void GetRoom(object taskComment)
        {
            DataRow[] dr = taskTable.Select("内容='" + taskComment + "'");
            if (dr.Length <= 0)
                throw new Exception("采集凭证号不在任务列表中，请核实");
            roomLabel.Text = dr[0][1].ToString();
        }

        private void collectButton_Click(object sender, EventArgs e)
        {
            try
            {
                string taskComment = txtComment.Text.Trim();
                if (string.IsNullOrEmpty(taskComment)) throw new Exception("凭证号为空");
                CheckTaskComment(taskComment);
                string roomCode = roomLabel.Text.Trim();
                if (string.IsNullOrEmpty(roomCode)) throw new Exception("库房号不能为空");
                BindingDetailFrm frm = new BindingDetailFrm(taskNo, roomCode, taskComment);
                frm.ShowDialog();
                getTaskListbyUser();
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        private void CheckTaskComment(string taskComment)
        {
            DataRow[] dr = taskTable.Select("内容='" + taskComment + "'");
            if (dr.Length <= 0)
                throw new Exception("采集凭证号不在任务列表中，请核实");

            taskNo = dr[0][5].ToString();
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
                        string comment = taskDataGrid[taskDataGrid.CurrentRowIndex, 0].ToString();
                        string roomNo = taskDataGrid[taskDataGrid.CurrentRowIndex, 1].ToString();
                        txtComment.Text = comment;
                        roomLabel.Text = roomNo;
                        break;
                }
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 取消任务接收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCanel_Click(object sender, EventArgs e)
        {
            string taskComment = txtComment.Text.Trim();
            if (string.IsNullOrEmpty(taskComment))
            {
                Message.Alarm("提示", "凭证号为空");
                return;
            }
            CheckTaskComment(taskComment);
            string roomCode = roomLabel.Text.Trim();
            if (string.IsNullOrEmpty(roomCode))
            {
                Message.Alarm("提示", "库房号不能为空");
                return;
            }
            try
            {
                service.CommitReceiveCanelTask(taskComment, User.Instance.UserData.UserId, true);
                Message.Alarm("提示", string.Format("取消凭证号：【{0}】的任务成功", taskComment));
                getTaskListbyUser();
            }
            catch (Exception ee)
            {
                Message.Alarm("提示", ee.Message);
            }
        }
    }
}