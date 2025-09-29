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
    public partial class TrayUpCollectDetailFrm : Form
    {
        public TrayUpCollectDetailFrm(string room)
        {
            InitializeComponent();
            storeRoom = room;
        }

        private string storeRoom = string.Empty;
        public Dictionary<string, string[]> dicUpdateInfo = new Dictionary<string, string[]>();//0表示删除的数量 1表示修改的库位  2表示删除的序列
        public Dictionary<string, string> dicDeleteSeq = new Dictionary<string, string>();//删除的序列
        public Dictionary<string, decimal> dicDeleteInv = new Dictionary<string, decimal>();//删除批次库存
        

        private void BindingCollectDetailFrm_Load(object sender, EventArgs e)
        {
            detailListView.Items.Clear();
            List<BindingTray> trays = BindingTrayCollectData.Instance.Collect;
            dicDeleteSeq = new Dictionary<string, string>();//删除的序列
            dicUpdateInfo = new Dictionary<string, string[]>();//0表示删除的数量 1表示修改的库位  2表示删除的序列
            dicDeleteInv = new Dictionary<string, decimal>();

            foreach (BindingTray tray in trays)
            {
                detailListView.Items.Add(new ListViewItem(new string[] { tray.TrayNo,tray.MatCode, tray.BatchNo, tray.Sn, tray.CollectQty.ToString(), tray.StoreSite, tray.InTaskItemid }));
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (detailListView.SelectedIndices == null || detailListView.SelectedIndices.Count == 0) return;
            DialogResult re = MessageBox.Show("你确定要删除该条数据吗？", "提示", MessageBoxButtons.OKCancel,
               MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
            if (re == DialogResult.Cancel)
                return;

            int index = detailListView.SelectedIndices[0];

            string itemId = detailListView.Items[index].SubItems[6].Text.Trim();
            string sn = detailListView.Items[index].SubItems[3].Text.Trim();
            string collectQty = detailListView.Items[index].SubItems[4].Text.Trim();
            string trayNo = detailListView.Items[index].SubItems[0].Text.Trim();
            string matCode = detailListView.Items[index].SubItems[1].Text.Trim();
            string batchNo = detailListView.Items[index].SubItems[2].Text.Trim();
            string storeSite = detailListView.Items[index].SubItems[5].Text.Trim();
            string qty = detailListView.Items[index].SubItems[4].Text.Trim();


            if (!sn.Equals(string.Empty) && !dicDeleteSeq.ContainsKey(sn))
            {
                dicDeleteSeq.Add(sn, sn);
            }

            if (!dicUpdateInfo.ContainsKey(itemId))
            {
                dicUpdateInfo.Add(itemId, new string[3]);
                dicUpdateInfo[itemId][0] = collectQty;
                dicUpdateInfo[itemId][2] = itemId;
            }
            else
            {
                dicUpdateInfo[itemId][0] = Convert.ToString(Convert.ToDecimal(dicUpdateInfo[itemId][0]) + Convert.ToDecimal(collectQty));
                dicUpdateInfo[itemId][2] = itemId;
            }

            #region 记录删除的物料批次

            string strKey = string.Empty;
            if (sn.Equals(""))
            {
                strKey = trayNo;
            }
            else
            {
                strKey = trayNo;
            }

            if (!dicDeleteInv.ContainsKey(strKey))
            {
                dicDeleteInv.Add(strKey, Convert.ToDecimal(collectQty));
            }
            else
            {
                dicDeleteInv[strKey] += Convert.ToDecimal(collectQty);
            }
            #endregion

            BindingTrayCollectData.Instance.DeleteCollectData(trayNo,matCode, batchNo, sn, storeSite,Convert.ToDecimal (qty));
            this.detailListView.Items.RemoveAt(index); 
            Application.DoEvents();
            Message.Alarm("Success", "删除成功！"); 
            if (detailListView.Items.Count == 0)
            {
                this.Close();
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 修改库位功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void siteTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Return)
                {
                    string newStoreSite = this.siteTextBox.Text.Trim();
                    if (string.IsNullOrEmpty(newStoreSite)) return;
                    if (detailListView.SelectedIndices == null || detailListView.SelectedIndices.Count == 0)
                        throw new Exception("请选择要修改库位的采集明细行");
                    if (!newStoreSite.StartsWith("$KW$")) throw new Exception("采集的库位条码不合法");
                    newStoreSite = newStoreSite.Substring(4);
                    Check(storeRoom, newStoreSite);

                    string trayNo = detailListView.Items[detailListView.SelectedIndices[0]].SubItems[0].Text.Trim();
                    string matCode = detailListView.Items[detailListView.SelectedIndices[0]].SubItems[1].Text.Trim();
                    string batchNo = detailListView.Items[detailListView.SelectedIndices[0]].SubItems[2].Text.Trim();
                    string sn = detailListView.Items[detailListView.SelectedIndices[0]].SubItems[3].Text.Trim();
                    string qty = detailListView.Items[detailListView.SelectedIndices[0]].SubItems[4].Text.Trim() ; 
                    string storeSite = detailListView.Items[detailListView.SelectedIndices[0]].SubItems[5].Text.Trim();
                    string itemId = detailListView.Items[detailListView.SelectedIndices[0]].SubItems[6].Text.Trim();

                    if (!dicUpdateInfo.ContainsKey(itemId))
                    {
                        dicUpdateInfo.Add(itemId, new string[3]);
                        dicUpdateInfo[itemId][1] = newStoreSite;
                    }
                    else
                    {
                        dicUpdateInfo[itemId][1] = newStoreSite;
                    }

                    detailListView.Items[detailListView.SelectedIndices[0]].SubItems[5].Text = newStoreSite;   //  修改库位

                    BindingTrayCollectData.Instance.UpdateCollectData(matCode, batchNo, sn, storeSite, storeRoom, newStoreSite, string.IsNullOrEmpty(qty)?0:Convert.ToDecimal(qty));
                }
            }
            catch (Exception ex)
            {
                Message.Alarm("error", ex.Message);
                this.siteTextBox.Focus();
                this.siteTextBox.SelectAll();
            } 
        }

        /// <summary>
        /// 校验采集库位是否在库房下
        /// </summary>
        /// <param name="storeRoom"></param>
        /// <param name="newStoreSite"></param>
        private void Check(string storeRoom, string newStoreSite)
        {
            MiddleService service = new MiddleService();
            DataTable siteTable = service.GetStoreSiteByRoom(storeRoom, newStoreSite).Tables[0];
            DataRow[] siteDr = siteTable.Select("storesiteno= '" + newStoreSite + "'");
            if (siteDr.Length <= 0) throw new Exception("库房" + storeRoom + "下无库位号" + newStoreSite); 
        }

        /// <summary>
        /// 修改数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void qtyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Return)
                {
                    string newQty = this.qtyTextBox.Text.Trim();
                    if (string.IsNullOrEmpty(newQty)) return;
                    if (detailListView.SelectedIndices == null || detailListView.SelectedIndices.Count == 0)
                        throw new Exception("请选择要修改数量的采集明细行");
                    Management management = Management.GetSingleton(); 
                    if (!management.CheckQuantity(newQty)) throw new Exception("采集数量不合法");
                    string qty = detailListView.Items[detailListView.SelectedIndices[0]].SubItems[4].Text.Trim();
                    if (Convert.ToDecimal(qty) < Convert.ToDecimal(newQty))
                        throw new Exception("修改数量不能大于原先采集数量");

                    string trayNo = detailListView.Items[detailListView.SelectedIndices[0]].SubItems[0].Text.Trim();
                    string matCode = detailListView.Items[detailListView.SelectedIndices[0]].SubItems[1].Text.Trim();
                    string batchNo = detailListView.Items[detailListView.SelectedIndices[0]].SubItems[2].Text.Trim();
                    string sn = detailListView.Items[detailListView.SelectedIndices[0]].SubItems[3].Text.Trim(); 
                    string storeSite = detailListView.Items[detailListView.SelectedIndices[0]].SubItems[5].Text.Trim();
                      
                    BindingTrayCollectData.Instance.UpdateCollectData(trayNo, matCode, batchNo, sn, storeSite, storeSite, Convert.ToDecimal(newQty));
                    detailListView.Items[detailListView.SelectedIndices[0]].SubItems[4].Text = newQty;   //  修改数量
                }
            }
            catch (Exception ex)
            {
                Message.Alarm("error", ex.Message);
                this.siteTextBox.Focus();
                this.siteTextBox.SelectAll();
            } 
        }


    }
}