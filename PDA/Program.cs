using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Entity;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using BizLayer;
using System.IO;
using System.Reflection;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.Tar;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System.Xml.Serialization;
using System.Xml;
using System.Data;
using System.Net.Sockets;
using System.Text;
using System.Net;

namespace PDA
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static public string m_CurrentPath = null; //运行的路径
        static public bool IS_WINCE = false;
        static public string Platform; //运行的平台
        static public bool gb_disptrace = true;
        static public bool apprsock = false;
        static public string apprhost = null; 
        static public int apprport = 8080;
        static public bool gb_ya_suo = false;

        private const string appname = @"PDA"; //CreateMutex 的名字
        [DllImport("coredll")]
        private static extern int GetLastError();
        [DllImport("user32.dll", EntryPoint = "GetLastError")]
        private static extern int GetLastError_win32();

        public class SECURITY_ATTRIBUTES
        {
            public int nLength = 0;
            public int lpSecurityDescriptor = 0;
            public int bInheritHandle = 0;
        }
        private const int ERROR_ALREADY_EXISTS = 183;
        
        [DllImport("coredll")]
        private static extern IntPtr CreateMutex(SECURITY_ATTRIBUTES lpMutexAttributes, bool InitialOwner, string MutexName);
        [DllImport("coredll.Dll", SetLastError = true)]
        private static extern bool ReleaseMutex(IntPtr hMutex);

        [DllImport("kernel32.dll", EntryPoint = "ReleaseMutex", SetLastError = true)]
        internal static extern bool ReleaseMutex_win32(IntPtr hHandle);
        [DllImport("kernel32.dll", EntryPoint = "CreateMutex", SetLastError = true)]
        static extern IntPtr CreateMutex_win32(SECURITY_ATTRIBUTES lpMutexAttributes, bool bInitialOwner, string lpName);
        

        [MTAThread]
        static void Main()
        {
            //MyMessageBox.Show("5.0->" + decimal.Floor(new decimal(5.0)) + " 5.5->" + decimal.Floor(new decimal(5.5)) + " 5.6->" + decimal.Floor(new decimal(5.6)) + " 5.9999->" + decimal.Floor(new decimal(5.9999)));
            Platform = Environment.OSVersion.Platform.ToString();
            //MyMessageBox.Show(Environment.Version.ToString()); //PC上运行使用的是net 2.0 在wince上是net 3.5
            Platform = Platform.ToLower();
            IntPtr hMutex;
            //程序的运行路径
            try
            {
                if (Platform.Equals("wince"))
                {
                    hMutex = CreateMutex(null, true, appname);
                    if (hMutex != null)
                    {
                        if (Marshal.GetLastWin32Error() == ERROR_ALREADY_EXISTS)
                        {
                            ReleaseMutex(hMutex);
                            Application.DoEvents();
                            MessageBox.Show("本程序只允许启动一个实例,不能重复运行!");
                            //Application.Run(new w_runOnceTip());
                            return;
                        }
                    }
                    m_CurrentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
                    IS_WINCE = true;
                    //winceinit.init(); //wince特殊的初始化动作
                }
                else
                {
                    hMutex = CreateMutex_win32(null, false, appname);
                    if (Marshal.GetLastWin32Error() == ERROR_ALREADY_EXISTS)
                    {
                        ReleaseMutex_win32(hMutex);
                        MessageBox.Show("本程序只允许启动一个实例,不能重复运行!");
                        //Application.Exit();
                        //Application.Run(new w_runOnceTip());
                        return;
                    }
                    m_CurrentPath = Directory.GetCurrentDirectory();
                    IS_WINCE = false;
                }
            }
            catch (Exception e_hMutex)
            {
                MessageBox.Show("检查程序单实例运行异常:" + e_hMutex.Message + "\n" + e_hMutex.StackTrace); //MyMessageBox
                Application.Exit();
                return;
            }
            //启动splash窗口,在form1的load中findwindow,将之关闭
            /* 原来的代码
            if (CreateMutex(IntPtr.Zero, true, appname) == 0)
                return;
            if (GetLastError() == ERROR_ALREADY_EXISTS)
                return;
            */
            Application.Run(new LoginFrm());

        }
               
 
        public static MiddleService MiddleService; 
    }

    #region SQLite和webService数据访问封装
    public class SQLHelper
    {
        private string is_log_filename = "\\log.txt"; // 或者为 \\log_tx.txt
        public int sqlcode = -1;
        public string sqlerrtext = "";
        public int sqlnrows = 0;
 
        public SQLHelper(string pm_log_file_name)
            : this()
        {
            if (!(pm_log_file_name.StartsWith("\\") || pm_log_file_name.StartsWith("/")))
            { is_log_filename = "\\" + pm_log_file_name; }
            else { is_log_filename = pm_log_file_name; }
        }

        public SQLHelper()
        {
 
        }

        static public string SNVL(object pb_obj, string pm_default)
        {
            if (Convert.IsDBNull(pb_obj) || pb_obj == null) { return pm_default; } else { return Convert.ToString(pb_obj); }
        }
        static public decimal NNVL(object pb_obj, decimal pm_default)
        {
            if (Convert.IsDBNull(pb_obj)) { return pm_default; } else { return Convert.ToDecimal(pb_obj); }
        }
        static public DateTime DNVL(object pb_obj, DateTime pm_default)
        {
            if (Convert.IsDBNull(pb_obj)) { return pm_default; } else { return Convert.ToDateTime(pb_obj); }
        }

        //写日志字符串
        public void logstr(string pm_mess)
        {
            //在后台线程上,不允许记日志,防止访问冲突,后台线程的日志使用单独的名称
            string fpath = Program.m_CurrentPath + is_log_filename;
            try
            {   //可能执行 消息提醒检测 的后台窗口,正在写日志,若前台的线程,此时也要写日志,将导致exception的发生
                using (FileStream fs = new FileStream(fpath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    if (fs.CanSeek) { fs.Seek(0, SeekOrigin.End); }
                    if (pm_mess == null) { pm_mess = ""; }
                    pm_mess = "\r\n" + DateTime.Now.ToLongTimeString() + " " + pm_mess;
                    byte[] byteArray = System.Text.Encoding.Default.GetBytes(pm_mess);
                    fs.Write(byteArray, 0, byteArray.Length);
                    fs.Flush();
                    fs.Close();
                }
            }
            catch (Exception efile)
            {
                MessageBox.Show("sql信息写日志错误,可能磁盘空间不足,或者多个线程同时写日志,\n" + efile.Message + (Program.gb_disptrace ? "\n" + efile.StackTrace : ""));
                return;
            }
        }

        public string ToHexString(byte[] bytes)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("X2"));
            }
            return builder.ToString();
        }

        private string rwSocket(char pmRWtype, Socket pmSock, ref byte[][] pmArgs)
        {
            //8字节的特殊标识[!@#$%^&*]
            //8字节的数据项数目
            //8字节数据项目1长度
            //数据项目1的字节数组
            //8字节数据项目2长度
            //数据项目2的字节数组

            //Send(Byte[] buffer, Int32 offset, Int32 size, SocketFlags socketFlags); 
            //This overload is like the previous one except you can specify which index within the 
            //byte array to start from. For example, to send bytes 3 through 7 of a byte array, you could use the following:
            //l_Socket.Send(l_buffer, 2, 6, SocketFlags.None);

            //Receive (Byte[] buffer, Int32 offset, Int32 size, SocketFlags socketFlags) 
            //This overload is like the previous one except you can specify which index within the 
            //byte array to use to start writing data into the array. 
            //For example, to receive up to 7 bytes of data into the buffer starting at position 3 in the buffer, use this code:
            //l_Socket.Receive(l_buffer, 2, 6, SocketFlags.None);

            byte[] byte8 = new byte[8] { 33, 64, 35, 36, 37, 94, 38, 42 };//{ '!', '@', '#', '$', '%', '^', '&', '*' }; 
            int liItemCount = 0, lrt, lread; int liItemDataLen; string ls_len;
            byte[] byteheader = new byte[1024]; int li_p = 0;
            if ((pmRWtype == 'R') || (pmRWtype == 'r'))
            {
                lrt = 0; while (lrt != 8) { lread = pmSock.Receive(byteheader, lrt, 8 - lrt, SocketFlags.None); if (lread <= 0) { throw new Exception("读取错误,Receive返回" + lread); }; lrt += lread; };
                li_p = 8;
                while (true)
                {
                    if ((byteheader[li_p - 8] == 33) && (byteheader[li_p - 7] == 64) &&
                        (byteheader[li_p - 6] == 35) && (byteheader[li_p - 5] == 36) &&
                        (byteheader[li_p - 4] == 37) && (byteheader[li_p - 3] == 94) &&
                        (byteheader[li_p - 2] == 38) && (byteheader[li_p - 1] == 42))
                    {
                        if (li_p > 8) { if (!Program.IS_WINCE) { logstr("查找到指令头,丢弃" + (li_p - 8) + "字节!"); } }
                        break;//已经查找到报文头
                    }
                    if (li_p == 1024)
                    {
                        byteheader[0] = byteheader[li_p - 8]; byteheader[1] = byteheader[li_p - 7];
                        byteheader[2] = byteheader[li_p - 6]; byteheader[3] = byteheader[li_p - 5];
                        byteheader[4] = byteheader[li_p - 4]; byteheader[5] = byteheader[li_p - 3];
                        byteheader[6] = byteheader[li_p - 2]; byteheader[7] = byteheader[li_p - 1];
                        li_p = 8; if (!Program.IS_WINCE) { logstr("未查找指令头,丢弃1024字节!"); }
                    }
                    lread = pmSock.Receive(byteheader, li_p, 1, SocketFlags.None); if (lread <= 0) { throw new Exception("读取错误,Receive返回" + lread); }; li_p++;
                }
                //8字节的数据项数目,十进制数的编码,不是hex的编码
                lrt = 0; while (lrt != 8) { lread = pmSock.Receive(byte8, lrt, 8 - lrt, SocketFlags.None); if (lread <= 0) { throw new Exception("读取错误,Receive返回" + lread); }; lrt += lread; };
                liItemCount = Convert.ToInt32(Encoding.UTF8.GetString(byte8, 0, 8).Trim()); //不大于99999999字节
                pmArgs = new byte[liItemCount][];
                for (int i = 0; i < liItemCount; i++)
                {
                    lrt = 0; while (lrt != 8) { lread = pmSock.Receive(byte8, lrt, 8 - lrt, SocketFlags.None); lrt += lread; };
                    liItemDataLen = Convert.ToInt32(Encoding.UTF8.GetString(byte8, 0, 8).Trim());
                    pmArgs[i] = new byte[liItemDataLen];
                    lrt = 0; while (lrt != liItemDataLen) { lread = pmSock.Receive(pmArgs[i], lrt, liItemDataLen - lrt, SocketFlags.None); if (lread <= 0) { throw new Exception("读取错误,Receive返回" + lread); }; lrt += lread; };
                    if (!Program.IS_WINCE) { logstr("收到第" + i + "项(" + liItemDataLen + ")byte:" + ToHexString(pmArgs[i])); }
                }
            }
            else
            { //send是否也需要处理,可能一次send不成功吗?
                //指令头
                pmSock.Send(byte8, 0, 8, SocketFlags.None);
                //指令项目数
                liItemCount = pmArgs.Length; ls_len = Convert.ToString(liItemCount);
                for (int i = 0; i < 8; i++) { if (ls_len.Length >= 8) { break; } ls_len = " " + ls_len; }
                byte8 = Encoding.UTF8.GetBytes(ls_len);
                pmSock.Send(byte8, 0, 8, SocketFlags.None);
                //发送每一个项目
                for (int i = 0; i < liItemCount; i++)
                {
                    liItemDataLen = pmArgs[i].Length; ls_len = Convert.ToString(liItemDataLen);
                    for (int j = 0; j < 8; j++) { if (ls_len.Length >= 8) { break; } ls_len = " " + ls_len; }
                    byte8 = Encoding.UTF8.GetBytes(ls_len);
                    pmSock.Send(byte8, 0, 8, SocketFlags.None);
                    if (liItemDataLen > 0)
                    {
                        pmSock.Send(pmArgs[i], 0, liItemDataLen, SocketFlags.None);
                    }
                }
            }
            return "";
        }

        /// <summary>
        /// 调datagrid的宽度,未定义的列设置为的100,最后一个参数,指定最末的n个列隐藏,不显示
        /// </summary>
        /// <param name="dv">DataTable的升级版DataView控件的引用</param>
        /// <param name="dt">DataGrid控件</param>
        /// <param name="colWidths">从第1列开始的列宽数组</param>
        /// <param name="rightNoVisible">从最末开始的倒数n个列,隐藏不显示</param>
        /// <param name="larr_colwdef">逗号分隔的列宽字符串,可直接在sql中返回列宽的定义</param>
        public static void DataGridColumnSize(DataView dv, DataGrid dt, int[] colWidths, int rightNoVisible, string larr_colwdef)
        {
            if (larr_colwdef != null && larr_colwdef.Length > 2)
            {
                string[] larr_w_vals = larr_colwdef.Split(new char[]{','});
                List<int> t = new List<int>();
                for (int i = 0; i < larr_w_vals.Length; i++)
                {
                    if (larr_w_vals[i] != null && larr_w_vals[i].Length > 0)
                    {
                        t.Add(Convert.ToInt32(larr_w_vals[i]));
                    }
                }
                if(t.Count > 0){ colWidths = t.ToArray();}
            }
            if (colWidths == null || colWidths.Length <= 0) { return; }
            dt.TableStyles.Clear();
            //定义DataGridTableStyle  
            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = dv.Table.TableName;  //映射style对应数据源的表名，很重要，否则无数据显示  

            //分别对列进行渲染，其中前三列用for循环实现，对列宽进行设定，值为75   
            for (int i = 0; i < dv.Table.Columns.Count; i++)
            {
                DataGridColumnStyle aColumnTextColumnStyle = new DataGridTextBoxColumn();//定义该列用textbox来进行渲染 
                aColumnTextColumnStyle.HeaderText = dv.Table.Columns[i].ColumnName;   //列头 
                aColumnTextColumnStyle.MappingName = dv.Table.Columns[i].ColumnName; //映射数据源的列名，很重要，否则无数据显示 
                if (i < colWidths.Length)
                {
                    aColumnTextColumnStyle.Width = colWidths[i];
                }
                else
                {
                    aColumnTextColumnStyle.Width = 100;
                }
                ts.GridColumnStyles.Add(aColumnTextColumnStyle);
            }
            for (int i = dv.Table.Columns.Count - rightNoVisible; i < dv.Table.Columns.Count; i++)
            {
                ts.GridColumnStyles[i].Width = 0;
            }
            dt.TableStyles.Add(ts);
        }

        /// <summary>
        /// 访问远程的webservice,传入参数调用程序包
        /// </summary>
        /// <param name="pm_dbfunname">程序包名</param>
        /// <param name="pm_args_n_v">程序包中用到的参数,pname1,pvalue1,pname2,pvalue2的n1,v1,n2,v2方式</param>
        /// <returns></returns>
        public DataSet WS(string pm_dbfunname, string[] pm_args_n_v)
        {
            string ps_zb_type = "接口尚未开通"; StringBuilder lcb_pm_args_n_v = new StringBuilder();
            if (!Program.IS_WINCE)
            {//写日志文件
                for (int ip = 0; ip < pm_args_n_v.Length; ip++)
                {
                    lcb_pm_args_n_v.Append(pm_args_n_v[ip]).Append(',');
                }
                logstr(ps_zb_type + " pm_dbfunname:" + (pm_dbfunname != null ? pm_dbfunname : "") + " \r\npm_args_n_v:" + lcb_pm_args_n_v.ToString());
            }
            if (pm_dbfunname == null || pm_dbfunname == "") 
            {
                this.sqlcode = -1; this.sqlerrtext = "必须指定要调用的程序包名";
                return null;
            }
            byte[] rtnByte = null;
            if (Program.apprsock) //使用socket服务器 Program.apprhost :Program.apprport
            {//使用socket的tcp连接方式与服务器通讯
                if (Program.apprhost == null || Program.apprhost.Equals("") || Program.apprport == 0)
                {
                    this.sqlcode = -1; this.sqlerrtext = "服务器不能连接,没有指定ip和端口";
                    return null;
                }
                try
                {   
                    EndPoint l_EndPoint = new IPEndPoint(IPAddress.Parse(Program.apprhost), Convert.ToInt16(Program.apprport));
                    Socket l_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    l_Socket.Connect(l_EndPoint);
                    if (l_Socket.Connected)
                    {
                        byte[][] pmArgs = new byte[4][];
                        pmArgs[0] = Encoding.UTF8.GetBytes(ps_zb_type); //执行的命令类型,PS 配送, ZB 总部
                        pmArgs[1] = Encoding.UTF8.GetBytes(pm_dbfunname); //exec sql
                        pmArgs[2] = Encoding.UTF8.GetBytes(lcb_pm_args_n_v.ToString()); //select sql
                        pmArgs[3] = Encoding.UTF8.GetBytes(ZIP_TYPE_GZIP.ToString()); //压缩类型
                        if (pmArgs[1].Length > 1) { pmArgs[1] = Compress(pmArgs[1], ZIP_TYPE_GZIP); } //压缩后再传输
                        if (pmArgs[2].Length > 1) { pmArgs[2] = Compress(pmArgs[2], ZIP_TYPE_GZIP); }

                        rwSocket('W', l_Socket, ref pmArgs);
                        //读取socket的返回结果
                        rwSocket('R', l_Socket, ref pmArgs);
                        if (pmArgs.Length != 1)
                        {
                            this.sqlcode = -1; this.sqlerrtext = "客户端接收到服务器返回的itemcount必须为1";
                            if (!Program.IS_WINCE) { logstr("sqlcode = -1," + sqlerrtext); }
                            return null;
                        }
                        rtnByte = pmArgs[0];
                        l_Socket.Close();
                        //写文件用于比较
                        //string fpath = Program.m_CurrentPath + "\\rtn_bin_socket.dat";
                        //FileStream fs = null;
                        //if (File.Exists(fpath))
                        //{
                        //    fs = new FileStream(fpath, FileMode.Truncate, FileAccess.Write);
                        //}
                        //else
                        //{
                        //    fs = new FileStream(fpath, FileMode.OpenOrCreate, FileAccess.Write);
                        //}
                        //fs.Write(rtnByte, 0, rtnByte.Length);
                        //fs.Close();
                        //
                    }
                    else
                    {
                        this.sqlcode = -1; this.sqlerrtext = "不能连接公司服务器,请确保网络连接正常!";
                        if (!Program.IS_WINCE) { logstr("sqlcode = -1," + sqlerrtext); }
                        return null;
                    }
                }
                catch (SocketException ex)
                {
                    this.sqlcode = -1; this.sqlerrtext = "连接公司服务器socket错误,请确保网络连接正常," + ex.Message + (Program.gb_disptrace ? "\n" + ex.StackTrace : "");
                    if (!Program.IS_WINCE) { logstr("sqlcode = -1," + sqlerrtext); }
                    return null;
                }
            }
            else
            { //仍使用webserver的方式进行通讯
                try
                {
                    BizLayer.WebService.PDA web1 = MiddleService.Instance.PDAservice;
                    //MyMessageBox.Show("Environment.Version:"+Environment.Version.ToString()+ " SoapVersion:" + web1.SoapVersion + " UserAgent:" + web1.UserAgent);
                    if (Program.gb_ya_suo)
                    {
                       rtnByte = web1.SQLSWS(ps_zb_type, pm_dbfunname, pm_args_n_v, (int)ZIP_TYPE_GZIP);
                    }
                    else
                    {
                       rtnByte = System.Text.UTF8Encoding.UTF8.GetBytes(web1.SQLSWS2(ps_zb_type, pm_dbfunname, pm_args_n_v)); //非压缩方式传输
                    }
                    //写文件用于比较
                    //string fpath = Program.m_CurrentPath + "\\rtn_bin_webservice.dat";
                    //FileStream fs = null;
                    //if (File.Exists(fpath))
                    //{
                    //    fs = new FileStream(fpath, FileMode.Truncate, FileAccess.Write);
                    //}
                    //else
                    //{
                    //    fs = new FileStream(fpath, FileMode.OpenOrCreate, FileAccess.Write);
                    //}
                    //fs.Write(rtnByte, 0, rtnByte.Length);
                    //fs.Close();
                    //
                }
                catch (Exception e)
                {
                    Program.gb_disptrace = true;
                    sqlcode = -1; sqlnrows = 0; sqlerrtext = "连接公司服务器webService错误,请确保网络连接正常," + e.Message + (Program.gb_disptrace ? "\n" + e.StackTrace : "");
                    return null;
                }
            }
            /////////////////
            if (rtnByte == null || (rtnByte.Length == 1 && rtnByte[0] == 0))
            {
                sqlcode = 0; sqlerrtext = "";
                return null;
            }
            try
            {
                if (69 == rtnByte[0] && 82 == rtnByte[1] && 82 == rtnByte[2])//ERR
                {
                    this.sqlcode = -1;
                    this.sqlerrtext = "\n[--公司服务器端错误,请稍候再试--]\n" + System.Text.Encoding.UTF8.GetString(rtnByte, 0, rtnByte.Length);
                    //判断此处是否有stackTrace的信息
                    if (!Program.IS_WINCE) { logstr("sqlcode = -1," + sqlerrtext); };
                    return null;
                }
                else
                {
                    byte[] rtnByte1 = null;
                    if (Program.gb_ya_suo || Program.apprsock)
                    {
                        rtnByte1 = DeCompress(rtnByte, ZIP_TYPE_GZIP);
                    }
                    else
                    {
                        rtnByte1 = rtnByte; //不压缩的方式
                    }
                    //
                    //rtnByte1中的信息,例
                    //<?xml version="1.0" encoding="utf-8"?>
                    //<DataSet>
                    //    <xs:schema id="NewDataSet" xmlns="" 
                    //        xmlns:xs="http://www.w3.org/2001/XMLSchema" 
                    //        xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
                    //        <xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
                    //            <xs:complexType>
                    //                <xs:choice minOccurs="0" maxOccurs="unbounded">
                    //                    <xs:element name="Table">
                    //                        <xs:complexType>
                    //                            <xs:sequence>
                    //                                <xs:element name="STOREID" type="xs:string" minOccurs="0" />
                    //                                <xs:element name="WHSID" type="xs:string" minOccurs="0" />
                    //                                <xs:element name="STORENAME" type="xs:string" minOccurs="0" />
                    //                            </xs:sequence>
                    //                        </xs:complexType>
                    //                    </xs:element>
                    //                </xs:choice>
                    //            </xs:complexType>
                    //        </xs:element>
                    //    </xs:schema>
                    //    <diffgr:diffgram 
                    //        xmlns:msdata="urn:schemas-microsoft-com:xml-msdata" 
                    //        xmlns:diffgr="urn:schemas-microsoft-com:xml-diffgram-v1">
                    //        <NewDataSet>
                    //            <Table diffgr:id="Table1" msdata:rowOrder="0">
                    //                <STOREID>01</STOREID>
                    //                <WHSID>001</WHSID>
                    //                <STORENAME>全福元物流中心</STORENAME>
                    //            </Table>
                    //        </NewDataSet>
                    //    </diffgr:diffgram>
                    //</DataSet>
                    //
                    //
                    DataSet ds = new DataSet();
                    ds = (DataSet)objXmlDeserialize(rtnByte1, ds.GetType());
                    sqlnrows = ds.Tables[0].Rows.Count;
                    if (sqlnrows == 0)
                    {
                        sqlcode = 100; sqlerrtext = "查询无记录!";
                    }
                    else
                    {
                        sqlcode = 0; sqlerrtext = "";
                    }
                    ///写日志
                    if (!Program.IS_WINCE)
                    {
                        logstr("查询结果ds.Tables[0].Rows.Count:" + ds.Tables[0].Rows.Count);
                        for (int r = 0; r < ds.Tables[0].Rows.Count; r++)
                        {
                            string ls_colvalues = "第" + r + "行:";
                            for (int c = 0; c < ds.Tables[0].Columns.Count; c++)
                            {
                                ls_colvalues = ds.Tables[0].Rows[r][c] + " ";

                            }
                            logstr(ls_colvalues);
                        }
                    }
                    ///写日志
                    return ds;
                }
            }
            catch (Exception e)
            {
                sqlcode = -1; sqlnrows = 0; sqlerrtext = "连接公司服务器通讯结果错误,请确保网络连接正常," + e.Message + (Program.gb_disptrace ? "\n" + e.StackTrace : "");
                return null;
            }
        }

        /// <summary>
        /// 提取字符串中 (字段名前缀+字段值+星号) 'MC'||m.matcode||'*DN*DV*SN'||ti.hintbatchno||'*MF*AG'||g.parno||'*DC*'
        /// --- 新的条码签格式, 估计还没有正式启动2018-03-06 
        /// PO(采购订单)+13位*PL(装箱单)+13位*MC(物料)+13位*
        /// DN(图纸编号)+65位*DV(图纸版本号)+06位*BN(批次号)+10位*SN(序列号)+12位*
        /// MF(制造商)+04位*AG(供应商)+04位*PD(生产日期)+08位*WD(有效期)+03位*QT(数量)+04位
        /// </summary>
        /// <param name="pm_bacodevalue"></param>
        /// <param name="pm_fieldname"></param>
        /// <returns></returns>
        public static string getBarcodeField(string pm_bacodevalue, string pm_fieldname)
        {
            pm_bacodevalue += "*";
            string ls_mc = "";
            int p1 = -1, p2 = -1;
            p1 = -1; p2 = -1; p1 = pm_bacodevalue.IndexOf(pm_fieldname);
            if (p1 >= 0) { p2 = pm_bacodevalue.IndexOf("*", p1 + 1); }
            if (p1 >= 0 && p2 > p1)
            { ls_mc = pm_bacodevalue.Substring(p1 + pm_fieldname.Length, p2 - p1 - pm_fieldname.Length); }
            return ls_mc;
        }


        #region 压缩方式测试代码
        public const int ZIP_TYPE_BZIP = 1, ZIP_TYPE_GZIP = 2, ZIP_TYPE_ZIP = 3, ZIP_TYPE_Deflater = 4;
        //返回序列化后的byte[]数组
        public static byte[] byteXmlSerializer(object o)
        {
            if (o == null)
            {
                throw new ArgumentNullException("null input");
            }
            try
            {
                System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(o.GetType());
                System.IO.MemoryStream mem = new MemoryStream();
                System.Xml.XmlTextWriter writer = new System.Xml.XmlTextWriter(mem, Encoding.UTF8);
                ser.Serialize(writer, o);
                writer.Close();
                return mem.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //返回反序列化后的object数据
        public static object objXmlDeserialize(byte[] input, Type type)
        {
            if (input == null)
            {
                throw new ArgumentNullException("null input");
            }
            try
            {
                System.Xml.Serialization.XmlSerializer mySerializer = new XmlSerializer(type);
                StreamReader stmRead = new StreamReader(new MemoryStream(input), System.Text.Encoding.UTF8);
                return mySerializer.Deserialize(stmRead);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static int buffSize = 2048;//指定压缩块缓存的大小，一般为2048的倍数

        //按类型压缩数据
        public static byte[] Compress(byte[] input, int itype)
        {
            if (input == null) { throw new ArgumentNullException("null input"); }
            try
            {
                using (MemoryStream outmsStrm = new MemoryStream())
                {
                    switch (itype)
                    {
                        case ZIP_TYPE_BZIP:
                            using (MemoryStream inmsStrm = new MemoryStream(input))
                            {
                                BZip2.Compress(inmsStrm, outmsStrm, buffSize);
                            }
                            return outmsStrm.ToArray();
                        case ZIP_TYPE_Deflater:
                            Deflater mDeflater = new Deflater(Deflater.BEST_COMPRESSION);
                            using (DeflaterOutputStream mStream = new DeflaterOutputStream(outmsStrm, mDeflater, buffSize))
                            {
                                mStream.Write(input, 0, input.Length);
                            }
                            return outmsStrm.ToArray();
                        case ZIP_TYPE_GZIP:
                            using (GZipOutputStream gzip = new GZipOutputStream(outmsStrm))
                            {
                                gzip.Write(input, 0, input.Length);
                            }
                            return outmsStrm.ToArray();
                        case ZIP_TYPE_ZIP:
                            using (ZipOutputStream zipStrm = new ZipOutputStream(outmsStrm))
                            {
                                ZipEntry zn = new ZipEntry("znName");
                                zipStrm.PutNextEntry(zn);
                                zipStrm.Write(input, 0, input.Length);
                            }
                            return outmsStrm.ToArray();
                        default:
                            throw new Exception("不支持的压缩类型!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //按类型解压数据
        public static byte[] DeCompress(byte[] input, int itype)
        {
            if (input == null) { throw new ArgumentNullException("null input"); }
            try
            {
                int mSize;
                byte[] buff = null;
                if (itype != ZIP_TYPE_BZIP) { buff = new byte[buffSize]; }
                using (MemoryStream outmsStrm = new MemoryStream())
                {
                    switch (itype)
                    {
                        case ZIP_TYPE_BZIP:
                            using (MemoryStream inmsStrm = new MemoryStream(input))
                            {
                                BZip2.Decompress(inmsStrm, outmsStrm);
                            }
                            return outmsStrm.ToArray();
                        case ZIP_TYPE_Deflater:
                            using (InflaterInputStream mStream = new InflaterInputStream(new MemoryStream(input)))
                            {
                                while (true)
                                {
                                    mSize = mStream.Read(buff, 0, buff.Length);
                                    if (mSize > 0)
                                    {
                                        outmsStrm.Write(buff, 0, mSize);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            return outmsStrm.ToArray();
                        case ZIP_TYPE_GZIP:
                            using (GZipInputStream gzip = new GZipInputStream(new MemoryStream(input)))
                            {
                                while (true)
                                {
                                    mSize = gzip.Read(buff, 0, buffSize);
                                    if (mSize > 0)
                                    {
                                        outmsStrm.Write(buff, 0, mSize);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            return outmsStrm.ToArray();
                        case ZIP_TYPE_ZIP:
                            using (ZipInputStream zipStrm = new ZipInputStream(new MemoryStream(input)))
                            {
                                ZipEntry zn = new ZipEntry("znName");
                                while ((zn = zipStrm.GetNextEntry()) != null)
                                {
                                    while (true)
                                    {
                                        mSize = zipStrm.Read(buff, 0, buffSize);
                                        if (mSize > 0)
                                        {
                                            outmsStrm.Write(buff, 0, mSize);
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            return outmsStrm.ToArray();
                        default:
                            throw new Exception("不支持的压缩类型!");
                    }

                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #endregion

    }
    #endregion SQLite和webService数据访问封装
}