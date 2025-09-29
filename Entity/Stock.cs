using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class Stock
    {
        private string taskNo = string.Empty;
        public string TaskNo
        {
            get { return taskNo; }
            set { taskNo = value; }
        }

        private string proofNo = string.Empty;
        public string ProofNo
        {
            get { return proofNo; }
            set { proofNo = value; }
        }

        private string matCode;
        public string MatCode
        {
            get { return matCode; }
            set { matCode = value; }
        }

        private string matName;
        public string MatName
        {
            get { return matName; }
            set { matName = value; }
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

        private decimal taskQty;
        public decimal TaskQty
        {
            get { return taskQty; }
            set { taskQty = value; }
        }

        private decimal collectQty;
        public decimal CollectQty
        {
            get { return collectQty; }
            set { collectQty = value; }
        }
  
        private string storeRoom;
        public string StoreRoom
        {
            get { return storeRoom; }
            set { storeRoom = value; }
        }

        private string storeSite;
        public string StoreSite
        {
            get { return storeSite; }
            set { storeSite = value; }
        }

        private string inTaskItemid;
        public string InTaskItemid
        {
            get { return inTaskItemid; }
            set { inTaskItemid = value; }
        }

        private string taskid;
        public string Taskid
        {
            get { return taskid; }
            set { taskid = value; }
        }

        private string excepttype = string.Empty;
        public string Excepttype
        {
            get { return excepttype; }
            set { excepttype = value; }
        }

        private string protype = string.Empty;
        public string Protype
        {
            get { return protype; }
            set { protype = value; }
        }

        private string desc;
        public string Desc
        {
            get { return desc; }
            set { desc = value; }
        }

        private string erpStore;
        public string ErpStore
        {
            get { return erpStore; }
            set { erpStore = value; }
        }

        private string trayNo;
        public string TrayNo
        {
            get { return trayNo; }
            set { trayNo = value; }
        }

        private string supplierNo;
        public string SupplierNo
        {
            get { return supplierNo; }
            set { supplierNo = value; }
        }

        private string supplierNoSN;
        public string SupplierNoSN
        {
            get { return supplierNoSN; }
            set { supplierNoSN = value; }
        }
        //CollectFlg
        private string collectFlg;
        public string CollectFlg
        {
            get { return collectFlg; }
            set { collectFlg = value; }
        }
    }
}
