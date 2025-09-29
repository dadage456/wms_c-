using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BizLayer;
using System.IO;

namespace PDA
{
    public partial class InstallForm : Form
    {
        public InstallForm()
        {
            InitializeComponent();
        }

        private void transing(object sender, EventArgs e)
        {
            pbDownload.Value = (int)sender;
            Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("现在退出采集系统，进行版本更新吗？", "版本更新", MessageBoxButtons.YesNo,
             MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }

            string url = Config.Instance.DefaultURL + "/WMSPDA.cab1";
            string file = @"\Program Files\BackUp\";
            string fileName = @"WMSPDA.cab\";
            if (!Directory.Exists(file))
            {
                Directory.CreateDirectory(file); //
            }
            file = file + fileName;
            //开始下载文件
            try
            {
                this.lbDownLoad.Visible = true;
                this.pbDownload.Visible = true;

                FileInfo fInfo = new FileInfo(file);
                HttpFileTrans trans = new HttpFileTrans(new Uri(url), fInfo);
                trans.Transing += new EventHandler(this.transing);
                trans.Download();
                this.lbDownLoad.Visible = false;
                this.pbDownload.Visible = false;
            }
            catch (Exception ee)
            {
                MessageBox.Show("更新失败。", ee.Message);
                return;
            }
            MessageBox.Show("下载完成，将退出系统并进行安装。请在安装完成后重新启动。", "版本更新", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);

            try
            {
                DialogResult = DialogResult.Abort;
            }
            catch
            {
                DialogResult = DialogResult.Cancel;
            }
        
        }
    }
}