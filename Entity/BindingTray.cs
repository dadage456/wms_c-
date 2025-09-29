using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class BindingTray
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
         
        private decimal collectQty;
        public decimal CollectQty
        {
            get { return collectQty; }
            set { collectQty = value; }
        }
   
        private string storeSite;
        public string StoreSite
        {
            get { return storeSite; }
            set { storeSite = value; }
        }
          
        private string trayNo;
        public string TrayNo
        {
            get { return trayNo; }
            set { trayNo = value; }
        }

        private string inTaskItemid = string.Empty;
        public string InTaskItemid
        {
            get { return inTaskItemid; }
            set { inTaskItemid = value; }
        }

        private string materialId = string.Empty;
        public string MaterialId
        {
            get { return materialId; }
            set { materialId = value; }
        } 
    }
}
