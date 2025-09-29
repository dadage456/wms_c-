using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Entity;
using BizLayer;
using PDA;
using BizLayer.WebService;
using System.Threading;
using System.IO.Ports;

namespace PDA
{
    public partial class PrintFrm : Form
    {
        //SerialPort server;
        List<SupplierShelvesInfo> mAtList=new List<SupplierShelvesInfo>();

        public PrintFrm(List<SupplierShelvesInfo> mList)
        {
           // Message.Alarm("matCode:", matCode);
           // Message.Alarm("matName:", matName);
           // Message.Alarm("batchNo:", batchNo);
           // Message.Alarm("sn:", sn);
           // Message.Alarm("matControlFlag:", matControlFlag);
            mAtList= mList;
            InitializeComponent();
           
        }

        private void TasksTitile_Click(object sender, EventArgs e)
        {

        }

        private void PrintFrm_Load(object sender, EventArgs e)
        {
            try
            {
                this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);
                UpCollectData.Instance.Collect = new List<Stock>();
            }
            catch (Exception ex)
            {
                Message.Alarm("提示", ex.Message);
            }
        }

        private void canclePrint_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void print_Click(object sender, EventArgs e)
        {
            foreach (SupplierShelvesInfo s in mAtList)
            {
                 Message.Alarm("提示", s.MatCode);
            }
        }

    }
}