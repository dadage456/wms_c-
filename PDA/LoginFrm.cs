using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BizLayer;
using PDA;
using System.IO;
using System.Runtime.InteropServices;
using Entity; 


namespace PDA
{
    public partial class LoginFrm : Form
    {
        private Management management;

        MiddleService service;
        private string station = string.Empty;

        public static uint SIPF_OFF = 0x00;//关闭    
        public static uint SIPF_ON = 0x01;//打开    
        [DllImport("coredll.dll")]
        public extern static void SipShowIM(uint dwFlag); 

        public LoginFrm()
        {
            InitializeComponent();
            pdtFilePath = @"\Program Files\BackUp\WMSPDA.cab";
            serviceWMS = new MiddleService();
            service = new MiddleService();
        }

        string pdtFilePath;
        string serverFileDateTime;
        MiddleService serviceWMS;

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string userCode = useridTextBox.Text.Trim();
                string password =passwordTextBox.Text.Trim();
                if (string.IsNullOrEmpty(userCode))
                {
                    Message.Alarm("提示","用户名不能为空");
                    useridTextBox.Focus();
                    useridTextBox.SelectAll();
                    return;
                }
                Cursor.Current = Cursors.WaitCursor;
                string userId=  Program.MiddleService.Login(userCode, password);
                Program.MiddleService.LogId = userCode;

                User.Instance.AddUserInfo(userCode, password, userId); 

                Cursor.Current = Cursors.Default;
                //Program.CurrUser = new Entity.User(userid, password);
                useridTextBox.Text = "";
                passwordTextBox.Text = "";
                updateButton_Click(null, null);
                new MainFrm().ShowDialog();
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                useridTextBox.Focus();
                useridTextBox.SelectAll();
                Message.Alarm("提示",ex.Message);
            }
        }

        /// <summary>
        /// true：版本一致
        /// false：版本不一致
        /// </summary>
        private bool checkVersion()
        {
            try
            {
                serverFileDateTime = serviceWMS.GetVersion();//获取服务器的版本信息
                if (serverFileDateTime == "")
                    return true;

                if (File.Exists(pdtFilePath))
                {
                    //采集器上cab的生成时间。
                    string pdtFileDateTime = File.GetLastWriteTime(pdtFilePath).ToString("yyyy-MM-dd HH:mm:ss");
                    if (pdtFileDateTime.Substring(0, 16) != serverFileDateTime.Substring(0, 16))
                    {
                        return MessageBox.Show("发现新版本，版本号为：" + serverFileDateTime + "\n是否进行版本更新？",
                           "WMS无线采集系统", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                           MessageBoxDefaultButton.Button2) != DialogResult.Yes;
                    }
                    else
                    {
                        //MessageBox.Show("当前已是最新版本，不需要更新。", "版本更新", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                        return true;
                    }
                }
                else
                {
                    //采集器上不存在cab文件。
                    return MessageBox.Show("需要下载新版本，版本号为：" + serverFileDateTime + "\n是否需要版本更新？",
                        "WMS无线采集系统", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2) != DialogResult.Yes;
                }
            }
            catch
            {
                return true;
            }
        }


        private void updateButton_Click(object sender, EventArgs e)
        {
            if (checkVersion())
            {
                return;
            }
            if (new InstallForm().ShowDialog() == DialogResult.Abort)
            {
                //采集器从服务器上下载更新文件成功。
                try
                {
                    IntPtr ipRet = ShellExecute.CreateFile(
                        pdtFilePath,
                        ShellExecute.GENERIC_WRITE | ShellExecute.GENERIC_READ,
                        ShellExecute.FILE_SHARE_READ | ShellExecute.FILE_SHARE_WRITE,
                        IntPtr.Zero,
                        ShellExecute.OPEN_EXISTING,
                        ShellExecute.FILE_ATTRIBUTE_NORMAL,
                        IntPtr.Zero);
                    if (ipRet.ToInt32() == ShellExecute.INVALID_HANDLE_VALUE)
                    {
                        ipRet = IntPtr.Zero;
                    }
                    else
                    {
                        DateTime sysnew = Convert.ToDateTime(serverFileDateTime);
                        ShellExecute.SystemTime systemtime = new ShellExecute.SystemTime();
                        systemtime.wYear = (ushort)sysnew.ToUniversalTime().Year;
                        systemtime.wMonth = (ushort)sysnew.ToUniversalTime().Month;
                        systemtime.wDay = (ushort)sysnew.ToUniversalTime().Day;
                        systemtime.wHour = (ushort)sysnew.ToUniversalTime().Hour;
                        systemtime.wMinute = (ushort)sysnew.ToUniversalTime().Minute;
                        systemtime.wSecond = (ushort)sysnew.ToUniversalTime().Second;
                        systemtime.wMiliseconds = 1;
                        systemtime.wDayOfWeek = (ushort)sysnew.ToUniversalTime().DayOfWeek;
                        ShellExecute.FILETIME fileMtime = ShellExecute.SystemTimetoFILETIME(systemtime);
                        ShellExecute.SetFileTime(ipRet, ref fileMtime, ref fileMtime, ref fileMtime);
                        ShellExecute.CloseHandle(ipRet);
                    }
                }
                catch
                {
                }
                ShellExecute.ExeFile(pdtFilePath);
                this.Close();
                Application.Exit();
                return;
            }
            GC.Collect();
        }

        private void btnAdvan_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.Visible)
            {
                this.tabControl1.Hide();
                this.btnAdvan.Text = "高级>>";
                this.btnLogin.Text = "登录";
            }
            else
            {
                this.tabControl1.Show();
                this.btnAdvan.Text = "标准<<";
                this.btnLogin.Text = "确定";
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            ConnectionTest();
        }

        private void ConnectionTest()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                //Program.MiddleService.ConnectionTest(urlTextBox.Text);
            }
            catch (ApplicationException ex)
            {
                Cursor.Current = Cursors.Default;
                Message.Alarm("Error", ex.Message);
                return;
            }
            if (urlTextBox.Text.Length > 5) //滤掉结尾多余的 路径分隔符号
            {
                int p = urlTextBox.Text.Length -1;
                for(; p>4; p--)
                {
                    if(urlTextBox.Text[p] == '\\' || urlTextBox.Text[p] == '/')
                    {
                        urlTextBox.Text = urlTextBox.Text.Substring(0,p);
                    }else{break;}
                }
            }

            management.SaveConfig(urlTextBox.Text, BizLayer.Management.EnumSaveContent.Url);
            Cursor.Current = Cursors.Default;
            Program.MiddleService.ServiceUrl = urlTextBox.Text;
            Message.Alarm("Success", "地址保存成功");
            this.tabControl1.Hide();
            this.btnAdvan.Text = "高级 >>";
            this.btnLogin.Text = "登录";
        }

        private void LoginFrm_Load(object sender, EventArgs e)
        {
            try
            {
                this.Location = new Point(40, 100);
                useridTextBox.Focus();

                Program.MiddleService = new MiddleService();
                management = Management.GetSingleton();
                urlTextBox.Text = management.DefaultBaseUrl;

                station = management.DefaultStation;
            }
            catch (ApplicationException ax)
            {
                Message.Alarm("配置错误", ax.Message);
                this.Close();
                return;
            }
            catch
            {
                Message.Alarm("配置错误", "配置文件错误，请更新正确的配置文件！");
                this.Close();
                return;
            }

            //try
            //{
            //    IDataCollection col = DataCollection.Create();
            //}
            //catch (ErrorScannerException)
            //{
            //    Message.Alarm("配置错误", "扫描头设置错误，请更新正确的配置文件！");
            //    this.Close();
            //}
            //catch (ApplicationException ax)
            //{
            //    Message.Alarm("配置错误", ax.Message);
            //    this.Close();
            //}
            //catch
            //{
            //    Message.Alarm("错误", "缺少依赖的扫描类库文件！");
            //    this.Close();
            //}
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("你确定要退出采集程序吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    Close();
                }
            }
            catch
            {
            
            }
        }
          
        private void tb_FocusChanged(object sender, EventArgs e)
        {
            if (Program.IS_WINCE)
            {
                SipShowIM(SIPF_ON);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (TESTBN.Text == "打开串口")
            //{
            //    try
            //    {
            //        serialPort1.PortName = textBox1.Text;
            //        serialPort1.BaudRate =
            //        serialPort1.DataBits =
            //        serialPort1.StopBits = StopBits.One;
            //        serialPort1.Parity = Parity.None;
            //        serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);  //异步接收数据
            //        serialPort1.Open();
            //        TESTBN.Text = "关闭串口";
            //    }
            //    catch
            //    {
            //        MessageBox.Show("打开串口失败");
            //    }
            //}
            //else
            //{
            //    try
            //    {
            //        serialPort1.Close();
            //    }
            //    catch
            //    {
            //        MessageBox.Show("关闭端口失败");
            //    }
            //    TESTBN.Text = "打开串口";
            //}
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //if (textBox1.Text == "")
            //{
            //    MessageBox.Show("请输入数据");
            //}
            //else
            //{
            //    byte[] data = Encoding.GetEncoding(936).GetBytes(textBox1.Text + "\r\n");
            //    try
            //    {
            //        serialPort1.Write(data, 0, data.Length);
            //        MessageBox.Show("数据发送成功");
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}

        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            //myDelegate md = new myDelegate(SetText);
            //try
            //{
            //    byte[] rdata = new byte[serialPort1.ReceivedBytesThreshold];
            //    serialPort1.Read(rdata, 0, rdata.Length);
            //    string readstr = Encoding.ASCII.GetString(rdata, 0, rdata.Length);
            //    Invoke(md, readstr);
            //}
            //catch
            //{
            //    MessageBox.Show("接收返回数据失败");
            //}

        }

        private void SetText(string s)
        {
            //textBox1.Text = s;
        }

    }
}