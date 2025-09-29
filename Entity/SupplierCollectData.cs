using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class SupplierCollectData
    {
        private SupplierCollectData() { }
        private static SupplierCollectData instance;
        public static SupplierCollectData Instance
        {
            get
            {
                if (instance == null) instance = new SupplierCollectData();
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
        public void AddCollectData(string matCode, string batchNo, string sn, decimal collectQty, string storeSite, string storeRoom, string taskid,
                                   string proofNo, string taskNo, string proType, string supplierNo,string supplierNoSN, string trayNo)
        {
            Stock stock = new Stock();
            stock.MatCode = matCode;
            stock.BatchNo = batchNo;
            stock.Sn = sn;

            stock.StoreRoom = storeRoom;
            stock.StoreSite = storeSite;            
           
            stock.TaskQty = 0;//计划数
            stock.CollectQty = collectQty;//本次采集数量
            stock.Taskid = taskid;
            stock.TrayNo = trayNo;
            stock.SupplierNo = supplierNo;
            stock.SupplierNoSN = supplierNoSN;

            stock.Protype = proType;
            stock.TaskNo = taskNo;
            stock.ProofNo = proofNo;
            collect.Add(stock);
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
        public void DeleteCollectData(string matCode, string batchNo, string sn, string taskQty, string collectQty, string supplierNo, string trayNo)
        {
            for (int i = collect.Count - 1; i >= 0; i--)
            {
                if (collect[i].MatCode == matCode && collect[i].BatchNo == batchNo && collect[i].Sn == sn && collect[i].SupplierNo == supplierNo && collect[i].TrayNo == trayNo && collect[i].CollectQty == Convert.ToDecimal(collectQty))
                {
                    collect.RemoveAt(i);
                    return;
                }
            }
        }

    }

}
