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
    public partial class DaoHuoSaoMaFrm : Form
    {
        public DaoHuoSaoMaFrm()
        {
            InitializeComponent();
        }

        Management management = Management.GetSingleton();
        DataTable taskTable = new DataTable(); //任务数据源
        DataView dv = new DataView();
        MiddleService service = new MiddleService();
        string taskId = string.Empty;

        private void DaoHuoSaoMaFrm_Load(object sender, EventArgs e)
        {
            taskDataGrid.DataSource = null;
            this.txtTaskNo.Text = "";
            getTaskListbyUser(); //查询已领的任务
            this.txtTaskNo.Focus();
        }

        /// <summary>
        /// 显示用户已领的任务
        /// </summary>
        private void getTaskListbyUser()
        {
            try
            {
                this.txtTaskNo.Text = ""; //任务单号
                taskDataGrid.DataSource = null;
                //
                List<string> args_nv = new List<string>();
                args_nv.Add("userid"); args_nv.Add(User.Instance.UserData.UserId); 
                args_nv.Add("ynlrw"); args_nv.Add("Y"); //显示已领的任务
                SQLHelper s = new SQLHelper();
                DataSet ds = s.WS("PDA_DH_V2_RWLIST", args_nv.ToArray());
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
                SQLHelper.DataGridColumnSize(dv, taskDataGrid, null, li_noDispRCols, larr_colwidths); //显示已领的到货任务单
                //AutoSizeCol(taskTable.Columns.Count); //重排列的宽度
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        private void AutoSizeCol(int p)
        {
            this.taskDataGrid.TableStyles.Clear();
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
                if (i == 10) aColumnTextColumnStyle.Width = 100;
                if (i == 11) aColumnTextColumnStyle.Width = 200;

                ts.GridColumnStyles.Add(aColumnTextColumnStyle);
            }
            this.taskDataGrid.TableStyles.Add(ts);
        }

        //点击采集按钮后执行
        private void collectButton_Click(object sender, EventArgs e)
        {
            try
            {
                string ls_taskNo = this.txtTaskNo.Text.Trim();
                if (string.IsNullOrEmpty(ls_taskNo)) throw new Exception("任务号为空");
                //
                bool lb_found = false;
                for(int i = 0; i< taskTable.Rows.Count; i++)
                {
                    if (ls_taskNo.Equals(SQLHelper.SNVL(taskTable.Rows[i][0],""))) //认为第0列是任务号
                    {
                        lb_found = true; break;
                    }
                }
                if(!lb_found)
                {
                    throw new Exception("采集任务号不在任务列表中，请核实");
                }
                //
                GoodsSignTaskItemFrm frm = new GoodsSignTaskItemFrm(ls_taskNo);
                frm.ShowDialog();
                getTaskListbyUser();
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        //弹出接收到货任务的窗口
        private void taskButton_Click(object sender, EventArgs e)
        {
            DaoHuoSaoMaFrmRecv gdReceive = new DaoHuoSaoMaFrmRecv();
            gdReceive.ShowDialog();
            getTaskListbyUser(); //刷新已领任务
        }

        //点击明细按钮后执行
        private void btnDtlCanel_Click(object sender, EventArgs e)
        {
            string ls_taskNo = txtTaskNo.Text;
            if (string.IsNullOrEmpty(ls_taskNo))
            {
                Message.Alarm("提示", "任务号不能为空,请选择要查询的任务号!");
                return;
            }

            DaoHuoSaoMaFrmDtl gdReceiveItem = new DaoHuoSaoMaFrmDtl(ls_taskNo);
            gdReceiveItem.ShowDialog();
            getTaskListbyUser();
        }

        //在datagrid 上选中一行时的动作
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
                        break;
                }
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        //关闭窗口
        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        //扫码选择并进行采集
        private void txtProofNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Return)
                {
                    string barcode = this.txtTaskNo.Text.Trim();
                    if (barcode == "")
                    {
                        this.txtTaskNo.Text = "";
                        this.txtTaskNo.Focus();
                        this.txtTaskNo.SelectAll();
                        return;
                    }
                    collectButton_Click(null, null); //扫纸质单据的 单号条码 触发开始扫描
                }
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        private void btnCanel_Click(object sender, EventArgs e)
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

                if (DialogResult.No == MessageBox.Show("是否执行撤销操作?", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    return; //丢弃本次扫描得到的信息,然后等用户重新输入数量
                }
                //
                
                List<string> args_nv = new List<string>();
                args_nv.Add("userid"); args_nv.Add(User.Instance.UserData.UserId);
                args_nv.Add("billid"); args_nv.Add(ls_taskNo); //显示要领的单号
                SQLHelper s = new SQLHelper();
                DataSet ds = s.WS("PDA_DH_V2_RECV_DEL", args_nv.ToArray());
                //
                if (ds == null || s.sqlcode == -1)
                {
                    MessageBox.Show("撤销任务错误" + s.sqlerrtext, "提示");
                    return;
                }
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("撤销任务错误" + s.sqlerrtext, "提示");
                    return;
                }
                this.getTaskListbyUser();
                //
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

    }
}