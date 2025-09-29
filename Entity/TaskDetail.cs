using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Entity
{
    public class TaskDetail
    {
        /// <summary>
        /// 料号
        /// </summary>
        private string materialCode;

        public string MaterialCode
        {
            get { return materialCode; }
            set { materialCode = value; }
        }
        
        /// <summary>
        /// 料名
        /// </summary>
        private string materialName;

        public string MaterialName
        {
            get { return materialName; }
            set { materialName = value; }
        }
        
        /// <summary>
        /// 行号
        /// </summary>
        private string lineNo;

        public string LineNo
        {
            get { return lineNo; }
            set { lineNo = value; }
        }

        /// <summary>
        /// 批号
        /// </summary>
        private string batchNo;

        public string BatchNo
        {
            get { return batchNo; }
            set { batchNo = value; }
        }

        /// <summary>
        /// 任务数
        /// </summary>
        private decimal requiredQuantity;

        public decimal RequiredQuantity
        {
            get { return requiredQuantity; }
            set { requiredQuantity = value; }
        }

        /// <summary>
        /// 完成数
        /// </summary>
        private decimal completeQuantity;

        public decimal CompleteQuantity
        {
            get { return completeQuantity; }
            set { completeQuantity = value; }
        }

        /// <summary>
        /// 库房
        /// </summary>
        private string whCode;
        public string WhCode
        {
            get { return whCode; }
            set { whCode = value; }
        }
            

        /// <summary>
        /// 库位
        /// </summary>
        private string location;

        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        /// <summary>
        /// 料品规格
        /// </summary>
        private string spec;

        public string Spec
        {
            get { return spec; }
            set { spec = value; }
        }


        private decimal stockQuantity;

        public decimal StockQuantity
        {
            get { return stockQuantity; }
            set { stockQuantity = value; }
        }

        private string matInnerCode; //参考料号

        public string MatInnerCode
        {
            get { return matInnerCode; }
            set { matInnerCode = value; }
        }   

    }
}
