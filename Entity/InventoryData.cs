using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class InventoryData
    {
        private string matCode;
        public string MatCode
        {
            get { return matCode; }
            set { matCode = value; }
        }

        private string batchNo;
        public string BatchNo
        {
            get { return batchNo; }
            set { batchNo = value; }
        }

        private string sn;
        public string Sn
        {
            get { return sn; }
            set { sn = value; }
        }
         
        private string storeSite;
        public string StoreSite
        {
            get { return storeSite; }
            set { storeSite = value; }
        }

        private decimal inventoryQty;
        public decimal InventoryQty
        {
            get { return inventoryQty; }
            set { inventoryQty = value; }
        }

        private string invTaskItemid;
        public string InvTaskItemid
        {
            get { return invTaskItemid; }
            set { invTaskItemid = value; }
        }

        private string matId;
        public string MatId
        {
            get { return matId; }
            set { matId = value; }
        }

        private string storeRoom;
        public string StoreRoom
        {
            get { return storeRoom; }
            set { storeRoom = value; }
        }

        private string trayNo;
        public string TrayNo
        {
            get { return trayNo; }
            set { trayNo = value; }
        }
    }
}
