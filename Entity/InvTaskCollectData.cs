using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class InvTaskCollectData
    {
        private InvTaskCollectData() { }
        private static InvTaskCollectData instance;
        public static InvTaskCollectData Instance
        {
            get
            {
                if (instance == null) instance = new InvTaskCollectData();
                return instance;
            }
        }

        private List<InventoryData> collect = new List<InventoryData>();
        public List<InventoryData> Collect
        {
            get { return collect; }
            set { collect = value; }
        }

        /// <summary>
        /// 添加采集对象
        /// </summary>
        /// <param name="matCode"></param>
        /// <param name="batchNo"></param>
        /// <param name="sn"></param>
        /// <param name="taskQty"></param>
        /// <param name="collectQty"></param>
        /// <param name="storeSite"></param>
        /// <param name="storeRoom"></param>
        public void AddCollectData(string matCode, string batchNo, string sn, decimal collectQty, string storeRoom, string storeSite, string matId, string invTaskItemid,string trayNo)
        {
            InventoryData invData = collect.Find(delegate(InventoryData p) { return p.BatchNo == batchNo && p.MatCode == matCode && p.Sn == sn && p.StoreRoom == storeRoom && p.StoreSite == storeSite; });
            if (invData != null)
            {
                invData.InventoryQty += collectQty;
            }
            else
            {
                InventoryData invDataNew = new InventoryData();
                invDataNew.MatCode = matCode;
                invDataNew.BatchNo = batchNo;
                invDataNew.Sn = sn;
                invDataNew.InventoryQty = collectQty;
                invDataNew.MatId = matId;
                invDataNew.StoreRoom = storeRoom;
                invDataNew.StoreSite = storeSite;
                invDataNew.InvTaskItemid = invTaskItemid;
                invDataNew.TrayNo = trayNo;
                collect.Add(invDataNew);
            }
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="matCode"></param>
        /// <param name="batchNo"></param>
        /// <param name="sn"></param>
        /// <param name="taskQty"></param>
        /// <param name="collectQty"></param>
        /// <param name="storeSite"></param>
        /// <param name="storeRoom"></param>
        public void DeleteCollectData(string matCode, string batchNo, string sn, string storeRoom, string storeSite)
        {
            for (int i = collect.Count - 1; i >= 0; i--)
            {
                if (collect[i].MatCode == matCode && collect[i].BatchNo == batchNo && collect[i].Sn == sn && collect[i].StoreSite == storeSite)
                {
                    collect.RemoveAt(i);
                    return;
                }
            }
        }

    }

}
