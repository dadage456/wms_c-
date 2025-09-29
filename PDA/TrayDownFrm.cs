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
    public partial class TrayDownFrm : Form
    {
        public TrayDownFrm()
        {
            InitializeComponent();
        }

        Management management = Management.GetSingleton();
        DataTable taskTable = new DataTable();
        DataView dv = new DataView();
        string taskId = string.Empty;
        MiddleService service = new MiddleService();
        
        private void GoodsDownFrm_Load(object sender, EventArgs e)
        {
            taskDataGrid.DataSource = null;
            this.txtTaskNo.Text = "";
            this.txtProofNo.Text = "";
            this.txtWorkStation.Text = "";
            this.roomLabel.Text = "";
            getTaskListbyUser();
            this.txtProofNo.Focus();
        }


        private void txtProofNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Return)
                {
                    string barcode = this.txtProofNo.Text.Trim();
                    if (barcode == "")
                    {
                        this.txtTaskNo.Text = "";
                        this.txtProofNo.Text = "";
                        this.roomLabel.Text = "";
                        this.txtProofNo.Focus();
                        this.txtProofNo.SelectAll();
                        return;
                    }

                    GetRoomByProofNo(barcode);
                    collectButton_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        private void GetRoomByProofNo(object proofNo)
        {
            DataRow[] dr = taskTable.Select("凭证号='" + proofNo + "'");
            if (dr.Length <= 0)
                throw new Exception("采集凭证号不在任务列表中，请核实");
            this.roomLabel.Text = dr[0][2].ToString();
            this.txtProofNo.Text = dr[0][1].ToString();
            
        }

        private void collectButton_Click(object sender, EventArgs e)
        {
            try
            {
                string strTaskNo = this.txtTaskNo.Text.Trim();
                if (string.IsNullOrEmpty(strTaskNo)) throw new Exception("任务号为空");
                string proofNo = this.txtProofNo.Text.Trim();
                if (string.IsNullOrEmpty(proofNo)) throw new Exception("凭证号为空");                
                string roomCode = roomLabel.Text.Trim();
                if (string.IsNullOrEmpty(roomCode)) throw new Exception("库房号不能为空");
                string workStation = txtWorkStation.Text;
                if (string.IsNullOrEmpty(workStation)) throw new Exception("工位不能为空");
                string siteFlag = string.Empty;
                string batchFlag = string.Empty;
                string taskNo = string.Empty;
                //CheckProofNo(proofNo, ref siteFlag, ref batchFlag, ref taskNo);
                CheckTaskNo(strTaskNo, ref siteFlag, ref batchFlag, ref taskNo);

                TrayDownTaskItemFrm frm = new TrayDownTaskItemFrm(taskNo, taskId, roomCode, siteFlag, batchFlag, proofNo, workStation);
                frm.ShowDialog();
                getTaskListbyUser();
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        private void CheckProofNo(string proofNo, ref string siteFlag, ref string batchFlag, ref string taskNo)
        {
            DataRow[] dr = taskTable.Select("凭证号='" + proofNo + "'");
            if (dr.Length <= 0)
                throw new Exception("采集凭证号不在任务列表中，请核实");
            siteFlag = dr[0][6].ToString();         //强制库位
            batchFlag = dr[0][7].ToString();        //强制批次
            taskNo = dr[0][4].ToString();           //任务号
            taskId = dr[0][9].ToString();
        }

        private void CheckTaskNo(string strtaskNo, ref string siteFlag, ref string batchFlag, ref string taskNo)
        {
            DataRow[] dr = taskTable.Select("任务号='" + strtaskNo + "'");
            if (dr.Length <= 0)
                throw new Exception("采集任务号不在任务列表中，请核实");
            siteFlag = dr[0][6].ToString();         //强制库位
            batchFlag = dr[0][7].ToString();        //强制批次
            taskNo = dr[0][4].ToString();           //任务号
            taskId = dr[0][9].ToString();
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
                        this.txtProofNo.Text = taskDataGrid[taskDataGrid.CurrentRowIndex, 0].ToString();
                        this.roomLabel.Text = taskDataGrid[taskDataGrid.CurrentRowIndex, 1].ToString();
                        this.txtWorkStation.Text = taskDataGrid[taskDataGrid.CurrentRowIndex, 3].ToString();
                        this.txtTaskNo.Text = taskDataGrid[taskDataGrid.CurrentRowIndex, 4].ToString();
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
            TrayDownReceiveProofFrm gdReceive = new TrayDownReceiveProofFrm();
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
                this.txtTaskNo.Text = "";
                this.txtProofNo.Text = "";
                this.txtWorkStation.Text = "";
                this.roomLabel.Text = "";
                taskDataGrid.DataSource = null;
                DataSet ds = service.GetOutTask(User.Instance.UserData.UserId, "0", "0", "1");
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
            //2016 3 9 增加删除  柏磊
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
                if (i == 10) aColumnTextColumnStyle.Width = 100;//紧急补单wip_supplement_flag
                if (i == 11) aColumnTextColumnStyle.Width = 200;//班组wip_supplement_flag

                ts.GridColumnStyles.Add(aColumnTextColumnStyle);
            }
            this.taskDataGrid.TableStyles.Add(ts);
        }

        private void btnCanel_Click(object sender, EventArgs e)
        {
            string taskNo = this.txtTaskNo.Text.Trim();
            if (string.IsNullOrEmpty(taskNo))
            {
                Message.Alarm("提示", "任务号为空");
                return;
            }            

            DataRow[] dr = taskTable.Select("任务号='" + taskNo + "'");
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

            string proofNo = this.txtProofNo.Text.Trim();
            if (string.IsNullOrEmpty(proofNo))
            {
                Message.Alarm("提示", "凭证号为空");
                return;
            }
            
            try
            {
                service.CommitRCOutTask(taskNo, User.Instance.UserData.UserId, true);
                Message.Alarm("提示", string.Format("取消任务号：【{0}】的任务成功", taskNo));
                getTaskListbyUser();
            }
            catch (Exception ee)
            {
                Message.Alarm("提示", ee.Message);
            }
        }

        private void btnDtlCanel_Click(object sender, EventArgs e)
        {
            string ProofNo = txtProofNo.Text;
            if (string.IsNullOrEmpty(ProofNo))
            {
                Message.Alarm("提示", "凭证号不能为空,请选择凭证号!");
                return;
            }

            string WorkStation = txtWorkStation.Text;
            if (string.IsNullOrEmpty(WorkStation))
            {
                Message.Alarm("提示", "工位不能为空,请选择工位!");
                return;
            }
            string TaskNo = txtTaskNo.Text;
            if (string.IsNullOrEmpty(TaskNo))
            {
                Message.Alarm("提示", "任务号不能为空,请选择任务号!");
                return;
            }
            TrayDownReceiveItemFrm gdReceiveItem = new TrayDownReceiveItemFrm(this.txtProofNo.Text, this.txtWorkStation.Text, txtTaskNo.Text);
            gdReceiveItem.ShowDialog();
            getTaskListbyUser();
        }

        private void exceptButton_Click(object sender, EventArgs e)
        {
            try
            {
                string strTaskNo = this.txtTaskNo.Text.Trim();
                if (string.IsNullOrEmpty(strTaskNo)) throw new Exception("任务号为空");
                string proofNo = this.txtProofNo.Text.Trim();
                if (string.IsNullOrEmpty(proofNo)) throw new Exception("凭证号为空");
                string roomCode = roomLabel.Text.Trim();
                if (string.IsNullOrEmpty(roomCode)) throw new Exception("库房号不能为空");
                string WorkStation = txtWorkStation.Text;
                if (string.IsNullOrEmpty(WorkStation)) throw new Exception("工位不能为空");
                string siteFlag = string.Empty;
                string batchFlag = string.Empty;
                string taskNo = string.Empty;
                //CheckProofNo(proofNo, ref siteFlag, ref batchFlag, ref taskNo);
                CheckTaskNo(strTaskNo, ref siteFlag, ref batchFlag, ref taskNo);

                ExceptTaskFrm frm = new ExceptTaskFrm(proofNo, strTaskNo, taskId, "平库下架组盘", roomCode,"");
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        private void QueryBN_Click(object sender, EventArgs e)
        {
            try
            {
                taskDataGrid.DataSource = null;
                roomLabel.Text = "";

                string s_ProofNo = txtProofNo.Text.Trim();

                if (string.IsNullOrEmpty(s_ProofNo))
                {
                    Message.Alarm("提示", "凭证号不能为空,请输入凭证号!");
                    return;
                }

                DataSet ds = service.GetOutTaskByProofNo(User.Instance.UserData.UserId, s_ProofNo, "0", "0", "1");
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    //throw new Exception("暂无上架任务"); 
                    return;
                }
                this.txtProofNo.Text = "";
                taskDataGrid.MouseUp -= taskDataGrid_MouseUp;
                taskTable = ds.Tables[0];
                dv.Table = taskTable;
                taskDataGrid.DataSource = dv;
                AutoSizeCol(taskTable.Columns.Count);
                taskDataGrid.MouseUp += taskDataGrid_MouseUp;
                txtProofNo.Focus();
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }

        }

        
        
    }
}