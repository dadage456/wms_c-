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
    public partial class InventoryTaskDetailFrm : Form
    {
        public InventoryTaskDetailFrm()
        {
            InitializeComponent();
        }
        public Dictionary<string, string> dicDelete = new Dictionary<string, string>();//存储删除的SN
        public Dictionary<string, decimal> dicUpdateInfo = new Dictionary<string, decimal>();//存储明细Id 对应的删除数量

        private void InventoryTaskDetailFrm_Load(object sender, EventArgs e)
        {
            detailListView.Items.Clear();

            List<InventoryData> invDatas = InvTaskCollectData.Instance.Collect;
            foreach (InventoryData invData in invDatas)
            {
                detailListView.Items.Add(new ListViewItem(new string[] { invData.MatCode,invData.BatchNo,invData.Sn,
                                        invData.InventoryQty.ToString(),invData.StoreSite,invData.StoreRoom,invData.InvTaskItemid}));
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
            string collectQty = detailListView.Items[index].SubItems[3].Text.Trim();
            string storeSite = detailListView.Items[index].SubItems[4].Text.Trim();
            string storeRoom = detailListView.Items[index].SubItems[5].Text.Trim();
            string itemId = detailListView.Items[index].SubItems[6].Text.Trim();

            if (!sn.Equals(string.Empty) && !dicDelete.ContainsKey(matCode+"@"+sn))
            {
                dicDelete.Add(matCode + "@" + sn, matCode + "@" + sn);
            }

            if (!dicUpdateInfo.ContainsKey(itemId))
            {
                dicUpdateInfo.Add(itemId, Convert.ToDecimal(collectQty));
            }
            else
            {
                dicUpdateInfo[itemId] += Convert.ToDecimal(collectQty);
            }

            InvTaskCollectData.Instance.DeleteCollectData(matCode, batchNo, sn, storeRoom, storeSite);
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