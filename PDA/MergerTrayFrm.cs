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
    public partial class MergerTrayFrm : Form
    {
        public MergerTrayFrm()
        {
            InitializeComponent(); 
        }

        Management management = Management.GetSingleton();
        MiddleService service = new MiddleService();
        string sourceTrayNo = string.Empty;
        string targetTrayNo = string.Empty;
        decimal sourceQty = 0;
        decimal qty = 0;

        private void MergerTrayFrm_Load(object sender, EventArgs e)
        {
            InitializeControl();
            sourceTrayTextBox.Focus();
        }

        private void InitializeControl()
        {
            InitializeSourceTrayTextBox();
            InitializeDataGrids(); 

            targetTrayTextBox.Text = "";
            qtyTextBox.Text = "";
        }

        private void InitializeDataGrids()
        {
            sourceTrayDataGrid.DataSource = null;
            targetTrayDataGrid.DataSource = null;
        }

        private void InitializeSourceTrayTextBox()
        {
            sourceTrayTextBox.Text = "";
            sourceTrayTextBox.Focus();
            sourceTrayTextBox.SelectAll();
        } 
          
        private void sourceTrayTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Return) //回车
                {
                    sourceTrayNo = this.sourceTrayTextBox.Text.Trim();
                    if (string.IsNullOrEmpty(sourceTrayNo))
                    {
                        this.sourceTrayTextBox.Text = "";
                        this.sourceTrayTextBox.Focus();
                        this.sourceTrayTextBox.SelectAll();
                    }
                    else
                    {
                        //if(barcode.IndexOf('$')>=0)
                        //{
                        //    string[] barcodes = barcode.Split('$');
                        //    if (barcodes.Length < 2) throw new Exception("扫描条码格式不符合");
                        //    if (barcodes[1] != "MATSN") throw new Exception("扫描条码格式不符合");
                        //    barcode = barcodes[barcodes.Length-1];
                        //}

                        CheckTray(sourceTrayNo);

                        DataSet ds = service.GetSourceTrayInfo(sourceTrayNo);
                        sourceTrayDataGrid.DataSource = ds.Tables[0];
                         
                        string qty = sourceTrayDataGrid[1, 6].ToString();  //剩余数量
                        if (!management.CheckQuantity(qty))
                            throw new Exception("源托盘剩余数量格式不合法");
                        sourceQty = Convert.ToDecimal(qty);

                        targetTrayTextBox.Text = "";
                        targetTrayTextBox.Focus();
                        targetTrayTextBox.SelectAll();
                    }  
                }
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        private void CheckTray(string sourceTrayNo)
        {
            service.CheckTray(sourceTrayNo);
        }

        private void targetTrayTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Return) //回车
                {
                    targetTrayNo = this.targetTrayTextBox.Text.Trim();
                    if (string.IsNullOrEmpty(targetTrayNo))
                    {
                        this.targetTrayTextBox.Text = "";
                        this.targetTrayTextBox.Focus();
                        this.targetTrayTextBox.SelectAll();
                    }
                    else
                    { 
                        CheckTray(targetTrayNo);

                        DataSet ds = service.GetTargetTrayInfo(targetTrayNo);
                        targetTrayDataGrid.DataSource = ds.Tables[0];

                        qtyTextBox.Text = "";
                        qtyTextBox.Focus();
                        qtyTextBox.SelectAll();
                    }
                }
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        private void qtyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Return) //回车
                {
                    string quantity = this.qtyTextBox.Text.Trim();
                    if (string.IsNullOrEmpty(quantity))
                    {
                        this.qtyTextBox.Text = "";
                        this.qtyTextBox.Focus();
                        this.qtyTextBox.SelectAll();
                    }
                    else
                    {
                        CheckQuantity(quantity);

                        //SetDataGrid
                        qty = Convert.ToDecimal(quantity) ;
                        sourceTrayDataGrid[1, 5] = quantity;       //移出数量
                        sourceTrayDataGrid[1, 6] = Convert.ToString( Convert.ToDecimal(sourceTrayDataGrid[1, 6].ToString()) - qty );  //剩余数量
                        targetTrayDataGrid[1, 5] = quantity;       //移入数量
                        targetTrayDataGrid[1, 6] = Convert.ToString( Convert.ToDecimal(targetTrayDataGrid[1, 6].ToString()) + qty );  //剩余数量

                        commitButton.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        /// <summary>
        /// 校验数量是否合法，是否超出源托盘剩余数量
        /// </summary>
        /// <param name="quantity"></param>
        private void CheckQuantity(string quantity)
        {
            if (!management.CheckQuantity(quantity)) 
                throw new Exception("输入的数量格式不合法"); 

            if (Convert.ToDecimal(sourceQty) < Convert.ToDecimal(quantity))
                throw new Exception("本次转移数量不能大于源托盘剩余数量");
        } 
          

        /// <summary>
        /// 撤销合盘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            //撤销数量
            sourceTrayDataGrid[1, 5] = Convert.ToString(Convert.ToDecimal(sourceTrayDataGrid[1, 5].ToString()) - qty);       //移出数量
            sourceTrayDataGrid[1, 6] = Convert.ToString(Convert.ToDecimal(sourceTrayDataGrid[1, 6].ToString()) + qty);      //剩余数量
            targetTrayDataGrid[1, 5] = Convert.ToString(Convert.ToDecimal(sourceTrayDataGrid[1, 5].ToString()) + qty);      //移入数量
            targetTrayDataGrid[1, 6] = Convert.ToString(Convert.ToDecimal(targetTrayDataGrid[1, 6].ToString()) - qty);      //剩余数量
             
            qty = 0;
            qtyTextBox.Text = "";
            qtyTextBox.Focus();
            qtyTextBox.SelectAll();
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void commitButton_Click(object sender, EventArgs e)
        {
            try
            {
                service.CommitMergeTray(sourceTrayNo, targetTrayNo, qty);
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

        private void sourceTrayDataGrid_MouseUp(object sender, MouseEventArgs e)
        {
            //DataGrid.HitTestInfo hti = dataGrid.HitTest(e.X, e.Y);
            //switch (hti.Type)
            //{
            //    case DataGrid.HitTestType.ColumnHeader:
            //        string columnHeader = bills.Tables[0].Columns[hti.Column].ColumnName;
            //        dv.Sort = columnHeader + " ASC";

            //        dataGrid.DataSource = dv;
            //        break;
            //    case DataGrid.HitTestType.RowHeader:
            //        string billNo = dataGrid[dataGrid.CurrentRowIndex, 0].ToString();
            //        this.inputBox3.Text = billNo;
            //        break;
            //}
        } 
        
    }
}