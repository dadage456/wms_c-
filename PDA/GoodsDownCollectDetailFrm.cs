using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Entity;

namespace PDA
{
    public partial class GoodsDownCollectDetailFrm : Form
    {  
        public GoodsDownCollectDetailFrm( )
        {
            InitializeComponent(); 
        }
        public Dictionary<string, string[]> dicUpdateInfo = new Dictionary<string, string[]>();//0表示删除的数量 1表示修改的库位  2表示删除的序列
        public Dictionary<string, string> dicDeleteSeq = new Dictionary<string, string>();//删除的序列
        public Dictionary<string, decimal> dicDeleteInv = new Dictionary<string, decimal>();//删除批次库存

        private void DownCollectDetailFrm_Load(object sender, EventArgs e)
        {
            detailListView.Items.Clear();
            dicUpdateInfo = new Dictionary<string, string[]>();
            dicDeleteSeq = new Dictionary<string, string>();//删除的序列
            dicDeleteInv = new Dictionary<string, decimal>();
            List<Stock> stocks = DownCollectData.Instance.Collect;
            foreach (Stock stock in stocks)
            {
                detailListView.Items.Add(new ListViewItem(new string[] { stock.MatCode,stock.BatchNo,stock.Sn,stock.TaskQty.ToString (),
                                        stock.CollectQty.ToString(),stock.StoreSite,stock.StoreRoom,stock.InTaskItemid}));
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

            string matCode = detailListView.Items[index].SubItems[0].Text.Trim();
            string batchNo = detailListView.Items[index].SubItems[1].Text.Trim();
            string sn = detailListView.Items[index].SubItems[2].Text.Trim();
            string taskQty = detailListView.Items[index].SubItems[3].Text.Trim();
            string collectQty = detailListView.Items[index].SubItems[4].Text.Trim();
            string storeSite = detailListView.Items[index].SubItems[5].Text.Trim();
            string storeRoom = detailListView.Items[index].SubItems[6].Text.Trim();
            string itemId = detailListView.Items[index].SubItems[7].Text.Trim();

            if (!sn.Equals(string.Empty) && !dicDeleteSeq.ContainsKey(matCode+"@"+sn))
            {
                dicDeleteSeq.Add(matCode + "@" + sn, matCode + "@" + sn);
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
                strKey = storeSite + matCode + batchNo;
            }
            else
            {
                strKey = storeSite + matCode + sn;
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

            DownCollectData.Instance.DeleteCollectData(matCode, batchNo, sn, collectQty, storeRoom, storeSite);
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


    }
}