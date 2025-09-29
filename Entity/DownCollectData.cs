using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class DownCollectData
    {
        private DownCollectData() { }
        private static DownCollectData instance;
        public static DownCollectData Instance
        {
            get
            {
                if (instance == null) instance = new DownCollectData();
                return instance;
            }
        }

        private List<Stock> collect = new List<Stock>();
        public List<Stock> Collect
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
        public void AddCollectData(string matCode, string batchNo, string sn, decimal collectQty, string storeRoom, string storeSite, Dictionary<string, List<decimal>> dicMtlOperatin, string erpStore, string matName,string trayNo)
        {
            foreach (KeyValuePair<string, List<decimal>> mtl in dicMtlOperatin)
            {
                Stock stock = new Stock();
                stock.MatCode = matCode; //物料号
                stock.BatchNo = batchNo; //批次号
                stock.Sn = sn; //序列号
                stock.TaskQty = mtl.Value[0];//计划数
                stock.CollectQty = mtl.Value[1];//本次采集数量
                stock.InTaskItemid = mtl.Key;
                stock.StoreRoom = storeRoom; //库房
                stock.StoreSite = storeSite; //库位(仓位)
                stock.ErpStore = erpStore; //
                stock.MatName = matName; //物料名称
                stock.TrayNo = trayNo;    //托盘号            
                collect.Add(stock);
            }
        }
        /// <summary>
        /// 修改采集对象
        /// </summary>
        /// <param name="matCode"></param>
        /// <param name="batchNo"></param>
        /// <param name="sn"></param>
        /// <param name="taskQty"></param>
        /// <param name="collectQty"></param>
        /// <param name="storeSite"></param>
        /// <param name="storeRoom"></param>
        public void UpdateCollectData(string matCode, string batchNo, string sn, string storeRoom, string storeSite, string newStoreSite, string trayNo)
        {
            for (int i = collect.Count - 1; i >= 0; i--)
            {
                if (collect[i].MatCode == matCode && collect[i].BatchNo == batchNo && collect[i].Sn == sn && collect[i].StoreSite == storeSite && collect[i].TrayNo == trayNo)
                {
                    collect[i].StoreSite = newStoreSite;
                    return;
                }
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
        public void DeleteCollectData(string matCode, string batchNo, string sn, string collectQty, string storeRoom, string storeSite)
        {
            for (int i = collect.Count - 1; i >= 0; i--)
            {
                if (collect[i].MatCode == matCode && collect[i].BatchNo == batchNo && collect[i].Sn == sn && collect[i].StoreSite == storeSite && collect[i].CollectQty == Convert.ToDecimal(collectQty))
                {
                    collect.RemoveAt(i);
                    return;
                }
            }
        }

    }

}
