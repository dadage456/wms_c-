using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Entity;
using BizLayer;

namespace PDA
{
    public partial class DaoHuoSaoMaFrmCjCollect : Form
    {
        public DaoHuoSaoMaFrmCj frm_parent = null;
        DataTable dt = null;
        DataView _dv = new DataView(); 

        public DaoHuoSaoMaFrmCjCollect(DaoHuoSaoMaFrmCj pm_parent)
        {
            frm_parent = pm_parent;
            InitializeComponent();
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 打开窗口时显示,本地缓存的已采集信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DaoHuoSaoMaFrmCjCollect_Load(object sender, EventArgs e)
        {
            List<string> hText = new List<string>();
            int[] colwidths = new int[20]; int c = -1;
            dt = new DataTable("已采集信息");
            //
            dt.Columns.Add(new DataColumn("matcode")); hText.Add("物料编码"); c++; colwidths[c] = 100;
            dt.Columns.Add(new DataColumn("BATCHNO")); hText.Add("批号"); c++; colwidths[c] = 100;
            dt.Columns.Add(new DataColumn("SN")); hText.Add("序列号"); c++; colwidths[c] = 100;
            dt.Columns.Add(new DataColumn("matname")); hText.Add("物料名称"); c++; colwidths[c] = 150;
            dt.Columns.Add(new DataColumn("GOODQTY")); hText.Add("采集数量"); c++; colwidths[c] = 100;
            dt.Columns.Add(new DataColumn("MATCODECONTROL")); hText.Add("控制"); c++; colwidths[c] = 5;
            //直接从父窗口的结果集中进行读取; 父窗结果集中 就是(物料+批次)的记录;
            for (int i = 0; i < frm_parent.taskDtls_tbl.Rows.Count; i++)
            {
                if (SQLHelper.NNVL(frm_parent.taskDtls_tbl.Rows[i]["tmodqty"], 0) <= 0) //小于等于0的,认为是未采集的
                {
                    continue; //没有修改的记录不列出
                }
                string ls_MATCODE = SQLHelper.SNVL(frm_parent.taskDtls_tbl.Rows[i]["MATCODE"], "");
                string ls_MATNAME = SQLHelper.SNVL(frm_parent.taskDtls_tbl.Rows[i]["MATNAME"], "");
                string ls_BATCHNO = SQLHelper.SNVL(frm_parent.taskDtls_tbl.Rows[i]["BATCHNO"], "");
                string ls_SN = SQLHelper.SNVL(frm_parent.taskDtls_tbl.Rows[i]["SN"], "");
                decimal ldc_googdqty = SQLHelper.NNVL(frm_parent.taskDtls_tbl.Rows[i]["GOODQTY"], 0);
                //
                DataRow dr = dt.NewRow();
                dr["matcode"] = ls_MATCODE;
                dr["matname"] = ls_MATNAME;
                dr["BATCHNO"] = ls_BATCHNO;
                dr["SN"] = ls_SN;
                dr["GOODQTY"] = ldc_googdqty;
                dr["MATCODECONTROL"] = SQLHelper.SNVL(frm_parent.taskDtls_tbl.Rows[i]["MATCODECONTROL"], ""); 
                dt.Rows.Add(dr);
            }
            _dv.Table = dt;
            _dv.AllowEdit = false;
            _dv.AllowDelete = false;
            _dv.AllowNew = false;

            datagrid1.DataSource = _dv;
            //
            DataGridTableStyle dgdtblStyle = new DataGridTableStyle(); //显示header中的title
            dgdtblStyle.MappingName = dt.TableName;
            datagrid1.TableStyles.Add(dgdtblStyle);
            GridColumnStylesCollection colStyle = datagrid1.TableStyles[0].GridColumnStyles;
            System.Collections.Generic.List<string>.Enumerator ienum = hText.GetEnumerator();
            for (int iht = 0; iht < hText.Count; iht++)
            {
                ienum.MoveNext();
                colStyle[iht].HeaderText = ienum.Current;
                colStyle[iht].Width = colwidths[iht]; //设置列宽
                colStyle[iht].NullText = "";
            }
        }

        /// <summary>
        /// 执行删除某行的动作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool lb_found = false;
            if (ls_seldel_code == null)
            {
                Message.Alarm("Success", "请选中要删除的行！");
                return;
            }

            if (DialogResult.Yes != MessageBox.Show("是否删除物料" + ls_seldel_code + ls_seldel_name + "批号"+ls_seldel_bn+"序列号"+ls_seldel_sn+"的采集信息?", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
            {
                return;
            }
            //
            DataTable dt_tmp = frm_parent.taskDtls_tbl;
            for (int i = dt_tmp.Rows.Count -1; i >= 0; i--)
            {
                if (ls_seldel_code == SQLHelper.SNVL(dt_tmp.Rows[i]["MATCODE"], "") &&
                        ls_seldel_bn == SQLHelper.SNVL(dt_tmp.Rows[i]["BATCHNO"], "") &&
                       (("0".Equals(ls_seldel_control)
                            && ls_seldel_sn == SQLHelper.SNVL(dt_tmp.Rows[i]["SN"], "")
                          ) || (!"0".Equals(ls_seldel_control))))
                {
                    dt_tmp.Rows[i]["GOODQTY"] = Convert.DBNull; dt_tmp.Rows[i]["tmodqty"] = 0;
                    if(SQLHelper.NNVL(dt_tmp.Rows[i]["QTY"],0) == 0){
                        dt_tmp.Rows.RemoveAt(i);
                    }
                }
            }
            frm_parent.refresh_taskMat(frm_parent.taskDtls_tbl, frm_parent.taskMat_tbl); //刷新显示
            //
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                if (ls_seldel_code == SQLHelper.SNVL(dt.Rows[i]["MATCODE"], "") &&
                        ls_seldel_bn == SQLHelper.SNVL(dt.Rows[i]["BATCHNO"], "") &&
                       (("0".Equals(ls_seldel_control)
                            && ls_seldel_sn == SQLHelper.SNVL(dt.Rows[i]["SN"], "")
                          ) || (!"0".Equals(ls_seldel_control))))
                {
                    dt.Rows.RemoveAt(i); lb_found = true;
                }
            }
            //
            if (lb_found)
            {
                Message.Alarm("Success", "删除成功！");
            }
            else
            {
                Message.Alarm("Success", "没有找到对应记录！");
            }
        }

        private string ls_seldel_code = null, ls_seldel_name = null, ls_seldel_bn = null, ls_seldel_sn = null, ls_seldel_control = null;

        private void datagrid1_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                DataGrid.HitTestInfo hti = datagrid1.HitTest(e.X, e.Y);
                switch (hti.Type)
                {
                    case DataGrid.HitTestType.ColumnHeader:     //列头排序
                        string columnHeader = dt.Columns[hti.Column].ColumnName;
                        _dv.Sort = columnHeader + " ASC";
                        datagrid1.DataSource = _dv;
                        ls_seldel_code = null;
                        break;
                    case DataGrid.HitTestType.RowHeader:        //行选中
                        ls_seldel_code= datagrid1[datagrid1.CurrentRowIndex, 0].ToString();
                        ls_seldel_bn = datagrid1[datagrid1.CurrentRowIndex, 1].ToString();
                        ls_seldel_sn = datagrid1[datagrid1.CurrentRowIndex, 2].ToString();
                        ls_seldel_name = datagrid1[datagrid1.CurrentRowIndex, 3].ToString();
                        ls_seldel_control = datagrid1[datagrid1.CurrentRowIndex, 5].ToString();
                        break;
                }
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }
    }
}