using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{

    public class BindingTrayCollectData
    {
        private BindingTrayCollectData() { }
        private static BindingTrayCollectData instance;
        public static BindingTrayCollectData Instance
        {
            get
            {
                if (instance == null) instance = new BindingTrayCollectData();
                return instance;
            }
        }

        private List<BindingTray> collect = new List<BindingTray>();
        public List<BindingTray> Collect
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
        /// <param name="trayNo">托盘号</param>
        public void AddCollectData(string matCode, string batchNo, string sn, decimal collectQty, string storeSite, string trayNo, Dictionary<string, List<decimal>> dicMtlOperatin)
        {
            foreach (KeyValuePair<string, List<decimal>> mtl in dicMtlOperatin)
            {
                BindingTray tray = new BindingTray();
                tray.MatCode = matCode;
                tray.BatchNo = batchNo;
                tray.Sn = sn;

                tray.StoreSite = storeSite;     //不一定有值
                tray.TrayNo = trayNo;

                tray.CollectQty = mtl.Value[1];//本次采集数量
                tray.InTaskItemid = mtl.Key;
                collect.Add(tray);
            }
        }

        /// <summary>
        /// 修改采集对象（修改库位/数量）
        /// </summary>
        /// <param name="matCode"></param>
        /// <param name="batchNo"></param>
        /// <param name="sn"></param>
        /// <param name="taskQty"></param>
        /// <param name="collectQty"></param>
        /// <param name="storeSite"></param>
        /// <param name="storeRoom"></param>
        public void UpdateCollectData(string trayNo, string matCode, string batchNo, string sn, string oldStoreSite, string newStoreSite,decimal updateQty)
        {
            for (int i = collect.Count - 1; i >= 0; i--)
            {
                if (collect[i].MatCode == matCode && collect[i].BatchNo == batchNo && collect[i].Sn == sn && collect[i].StoreSite == oldStoreSite)
                {
                    collect[i].StoreSite = newStoreSite;
                    collect[i].CollectQty = updateQty;
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
        public void DeleteCollectData(string trayNo, string matCode, string batchNo, string sn, string storeSite, decimal collectQty)
        {
            for (int i = collect.Count - 1; i >= 0; i--)
            {
                if (collect[i].MatCode == matCode && collect[i].BatchNo == batchNo && collect[i].Sn == sn && collect[i].StoreSite == storeSite && collect[i].CollectQty == collectQty)
                {
                    collect.RemoveAt(i);
                    return;
                }
            }
        }

    }

}
