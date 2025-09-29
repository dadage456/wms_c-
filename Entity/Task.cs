using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class Task
    {
        private string taskid;

        public string Taskid
        {
            get { return taskid; }
            set { taskid = value; }
        }

        List<TaskDetail> taskDetail = new List<TaskDetail>();

        public List<TaskDetail> TaskDetail
        {
            get { return taskDetail; }
            set { taskDetail = value; }
        }

        public enum EnumTaskType
        {
            MaterialPurchaseIn, MaterialReceiveOut, SelfmadeProductIn, SelfmadeProductOut,
            SaleOut, MaterialInventory, SelfmadeInventory
        }

        public enum KindType
        {
            Material, Batch
        }
    }

}
