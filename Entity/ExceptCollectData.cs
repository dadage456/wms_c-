using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class ExceptCollectData
    {
        private ExceptCollectData() { }
        private static ExceptCollectData instance;
        public static ExceptCollectData Instance
        {
            get
            {
                if (instance == null) instance = new ExceptCollectData();
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
        public void AddCollectData(string matCode, string batchNo, string sn, string exceptType, decimal collectQty, string storeRoom, string storeSite, string taskid, 
                                   string exceptName, string proofNo, string taskNo, string proType)
        {
            Stock stock = new Stock();
            stock.MatCode = matCode;
            stock.BatchNo = batchNo;
            stock.Sn = sn;
            
           
            stock.TaskQty = 0;//计划数
            stock.CollectQty = collectQty;//本次采集数量
            stock.Taskid = taskid;

            stock.StoreRoom = storeRoom;
            stock.StoreSite = storeSite;
            stock.Excepttype = exceptType;
            stock.Protype = proType;
            stock.TaskNo = taskNo;
            stock.ProofNo = proofNo;
            stock.Desc = exceptName;
            collect.Add(stock);
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
        public void UpdateCollectData(string matCode, string batchNo, string sn, string storeRoom, string storeSite, string newStoreSite)
        {
            for (int i = collect.Count - 1; i >= 0; i--)
            {
                if (collect[i].MatCode == matCode && collect[i].BatchNo == batchNo && collect[i].Sn == sn && collect[i].StoreSite == storeSite)
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
        public void DeleteCollectData(string matCode, string batchNo, string sn, string taskQty, string collectQty, string storeRoom, string storeSite)
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
