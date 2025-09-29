using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BizLayer;
using System.Runtime.InteropServices;
using System.IO;

namespace PDA
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
            pdtFilePath = @"\Program Files\BackUp\WMSPDA.cab";
            serviceWMS = new MiddleService();
        }

        string pdtFilePath;
        string serverFileDateTime;
        MiddleService serviceWMS;

        private void MainFrm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.S:
                    upButton_Click(null, null);
                    break;
                case Keys.Z :
                    bindingButton_Click(null, null);
                    break;
                case Keys.P:
                    checkButton_Click(null, null);
                    break;
                case Keys.L:
                    btnCheckL_Click(null, null);
                    break;
                case Keys.X:
                    downButton_Click(null, null);
                    break;
                case Keys.F:
                    btnSenderMtl_Click(null, null);
                    break;
                case Keys.Y:
                    moveButton_Click(null, null);
                    break;
                case Keys.U:
                    closeButton_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 组盘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bindingButton_Click(object sender, EventArgs e)
        {
            ASWHUpFrm frm = new ASWHUpFrm();
            frm.ShowDialog();
        }
            
        /// <summary>
        /// 合盘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mergerButton_Click(object sender, EventArgs e)
        {
            MergerTrayFrm frm = new MergerTrayFrm();
            frm.ShowDialog();
        }

        /// <summary>
        /// 上架
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void upButton_Click(object sender, EventArgs e)
        {
            GoodsUpFrm frm = new GoodsUpFrm();
            frm.ShowDialog();
        }

        /// <summary>
        /// 下架
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downButton_Click(object sender, EventArgs e)
        {
            GoodsDownFrm frm = new GoodsDownFrm();
            frm.ShowDialog();
        }

        /// <summary>
        /// 盘点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkButton_Click(object sender, EventArgs e)
        {
            InventoryTaskFrm frm = new InventoryTaskFrm();
            frm.ShowDialog();
        }

        /// <summary>
        /// 立库盘点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckL_Click(object sender, EventArgs e)
        {
            ASWHInventoryTaskFrm frm = new ASWHInventoryTaskFrm();
            frm.ShowDialog();
        }


        /// <summary>
        /// 拉式发料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSenderMtl_Click(object sender, EventArgs e)
        {
            MtlSenderForm senderFrom = new MtlSenderForm();
            senderFrom.ShowDialog();
        }
        
        /// <summary>
        /// 移库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveButton_Click(object sender, EventArgs e)
        {
            TransferForm frm = new TransferForm();
            frm.ShowDialog();
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
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
                        MessageBox.Show("当前已是最新版本，不需要更新。", "版本更新", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
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

        private void downOutButton_Click(object sender, EventArgs e)
        {
            ASWHDownFrm frm = new ASWHDownFrm();
            frm.ShowDialog();
        }

        private void QueryButton_Click(object sender, EventArgs e)
        {
            QueryRepertoryFrm frm = new QueryRepertoryFrm();
            frm.ShowDialog();
            
        }

        private void PalletOutButton_Click(object sender, EventArgs e)
        {
            ASWHOutFrm frm = new ASWHOutFrm();
            frm.ShowDialog();
        }

        private void AllocateBN_Click(object sender, EventArgs e)
        {
            ASWHAllocateFrm frm = new ASWHAllocateFrm();
            frm.ShowDialog();
        }

        private void SupplierButton_Click(object sender, EventArgs e)
        {
            SupplierTaskFrm frm = new SupplierTaskFrm("999999", "999999", "999999", "补充打印", "999999", "");
            frm.ShowDialog();
        }

        private void downToTrayButton_Click(object sender, EventArgs e)
        {
            TrayDownFrm frm = new TrayDownFrm();
            frm.ShowDialog();
        }

        private void trayToUpButton_Click(object sender, EventArgs e)
        {
            TrayUpFrm frm = new TrayUpFrm();
            frm.ShowDialog();
        }

        private void trayAddButton_Click(object sender, EventArgs e)
        {
            TrayDownUpFrm frm = new TrayDownUpFrm();
            frm.ShowDialog();
        }

        private void trayDecButton_Click(object sender, EventArgs e)
        {
            TrayUpDownFrm frm = new TrayUpDownFrm();
            frm.ShowDialog();
        }

        //2018-03-05 执行到货扫码检验的功能
        private void btnDaoHuoSaoMa_Click(object sender, EventArgs e)
        {
            DaoHuoSaoMaFrm frm = new DaoHuoSaoMaFrm();
            frm.ShowDialog();
        }

        private void L_BeatOut_Click(object sender, EventArgs e)
        {
            BeatASWHDownFrm frm = new BeatASWHDownFrm();
            frm.ShowDialog();
        }

        private void P_BeatOut_Click(object sender, EventArgs e)
        {
            BeatGoodsDownFrm frm = new BeatGoodsDownFrm();
            frm.ShowDialog();
        }

    }

    public class ShellExecute
    {
        /// <summary>
        /// SHELLEXECUTEEX结构体
        /// </summary>
        public const uint BAUD_075 = 1;
        public const uint BAUD_110 = 2;
        public const uint BAUD_115200 = 131072;
        public const uint BAUD_1200 = 64;
        public const uint BAUD_128K = 65536;
        public const uint BAUD_134_5 = 4;
        public const uint BAUD_14400 = 4096;
        public const uint BAUD_150 = 8;
        public const uint BAUD_1800 = 128;
        public const uint BAUD_19200 = 8192;
        public const uint BAUD_2400 = 256;
        public const uint BAUD_300 = 16;
        public const uint BAUD_38400 = 16384;
        public const uint BAUD_4800 = 512;
        public const uint BAUD_56K = 32768;
        public const uint BAUD_57600 = 262144;
        public const uint BAUD_600 = 32;
        public const uint BAUD_7200 = 1024;
        public const uint BAUD_9600 = 2048;
        public const uint BAUD_USER = 268435456;
        public const uint CBR_110 = 110;
        public const uint CBR_115200 = 115200;
        public const uint CBR_1200 = 1200;
        public const uint CBR_128000 = 128000;
        public const uint CBR_14400 = 14400;
        public const uint CBR_19200 = 19200;
        public const uint CBR_2400 = 2400;
        public const uint CBR_256000 = 256000;
        public const uint CBR_300 = 300;
        public const uint CBR_38400 = 38400;
        public const uint CBR_4800 = 4800;
        public const uint CBR_56000 = 56000;
        public const uint CBR_57600 = 57600;
        public const uint CBR_600 = 600;
        public const uint CBR_9600 = 9600;
        public const uint CE_BREAK = 16;
        public const uint CE_DNS = 2048;
        public const uint CE_FRAME = 8;
        public const uint CE_IOE = 1024;
        public const uint CE_MODE = 32768;
        public const uint CE_OOP = 4096;
        public const uint CE_OVERRUN = 2;
        public const uint CE_PTO = 512;
        public const uint CE_RXOVER = 1;
        public const uint CE_RXPARITY = 4;
        public const uint CE_TXFULL = 256;
        public const uint CLRBREAK = 9;
        public const uint CLRDTR = 6;
        public const uint CLRIR = 11;
        public const uint CLRRTS = 4;
        public const uint CREATE_ALWAYS = 2;
        public const uint CREATE_NEW = 1;
        public const byte DATABITS_16 = 16;
        public const byte DATABITS_16X = 32;
        public const byte DATABITS_5 = 1;
        public const byte DATABITS_6 = 2;
        public const byte DATABITS_7 = 4;
        public const byte DATABITS_8 = 8;
        public const uint DTR_CONTROL_DISABLE = 0;
        public const uint DTR_CONTROL_ENABLE = 16;
        public const uint DTR_CONTROL_HANDSHAKE = 32;
        public const uint EV_BREAK = 64;
        public const uint EV_CTS = 8;
        public const uint EV_DSR = 16;
        public const uint EV_ERR = 128;
        public const uint EV_EVENT1 = 2048;
        public const uint EV_EVENT2 = 4096;
        public const uint EV_PERR = 512;
        public const uint EV_POWER = 8192;
        public const uint EV_RING = 256;
        public const uint EV_RLSD = 32;
        public const uint EV_RX80FULL = 1024;
        public const uint EV_RXCHAR = 1;
        public const uint EV_RXFLAG = 2;
        public const uint EV_TXEMPTY = 4;
        public const byte EVENPARITY = 2;
        public const uint fAbortOnError = 16384;
        public const uint fBinary = 1;
        public const uint fCtsHold = 1;
        public const uint fDsrHold = 2;
        public const uint fDsrSensitivity = 64;
        public const uint fDtrControl = 48;
        public const uint fDummy = 4294934528;
        public const uint fEof = 32;
        public const uint fErrorChar = 1024;
        public const uint FILE_ACTION_ADDED = 1;
        public const uint FILE_ACTION_MODIFIED = 3;
        public const uint FILE_ACTION_REMOVED = 2;
        public const uint FILE_ACTION_RENAMED_NEW_NAME = 5;
        public const uint FILE_ACTION_RENAMED_OLD_NAME = 4;
        public const uint FILE_ATTRIBUTE_ARCHIVE = 32;
        public const uint FILE_ATTRIBUTE_COMPRESSED = 2048;
        public const uint FILE_ATTRIBUTE_DIRECTORY = 16;
        public const uint FILE_ATTRIBUTE_ENCRYPTED = 64;
        public const uint FILE_ATTRIBUTE_HIDDEN = 2;
        public const uint FILE_ATTRIBUTE_INROM = 64;
        public const uint FILE_ATTRIBUTE_NORMAL = 128;
        public const uint FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = 8192;
        public const uint FILE_ATTRIBUTE_OFFLINE = 4096;
        public const uint FILE_ATTRIBUTE_READONLY = 1;
        public const uint FILE_ATTRIBUTE_REPARSE_POINT = 1024;
        public const uint FILE_ATTRIBUTE_ROMMODULE = 8192;
        public const uint FILE_ATTRIBUTE_ROMSTATICREF = 4096;
        public const uint FILE_ATTRIBUTE_SPARSE_FILE = 512;
        public const uint FILE_ATTRIBUTE_SYSTEM = 4;
        public const uint FILE_ATTRIBUTE_TEMPORARY = 256;
        public const uint FILE_CASE_PRESERVED_NAMES = 2;
        public const uint FILE_CASE_SENSITIVE_SEARCH = 1;
        public const uint FILE_FILE_COMPRESSION = 16;
        public const uint FILE_FLAG_BACKUP_SEMANTICS = 33554432;
        public const uint FILE_FLAG_DELETE_ON_CLOSE = 67108864;
        public const uint FILE_FLAG_NO_BUFFERING = 536870912;
        public const uint FILE_FLAG_OVERLAPPED = 1073741824;
        public const uint FILE_FLAG_POSIX_SEMANTICS = 16777216;
        public const uint FILE_FLAG_RANDOM_ACCESS = 268435456;
        public const uint FILE_FLAG_SEQUENTIAL_SCAN = 134217728;
        public const uint FILE_FLAG_WRITE_THROUGH = 2147483648;
        public const uint FILE_NOTIFY_CHANGE_ATTRIBUTES = 4;
        public const uint FILE_NOTIFY_CHANGE_CREATION = 64;
        public const uint FILE_NOTIFY_CHANGE_DIR_NAME = 2;
        public const uint FILE_NOTIFY_CHANGE_FILE_NAME = 1;
        public const uint FILE_NOTIFY_CHANGE_LAST_ACCESS = 32;
        public const uint FILE_NOTIFY_CHANGE_LAST_WRITE = 16;
        public const uint FILE_NOTIFY_CHANGE_SECURITY = 256;
        public const uint FILE_NOTIFY_CHANGE_SIZE = 8;
        public const uint FILE_PERSISTENT_ACLS = 8;
        public const uint FILE_SHARE_DELETE = 4;
        public const uint FILE_SHARE_READ = 1;
        public const uint FILE_SHARE_WRITE = 2;
        public const uint FILE_SUPPORTS_ENCRYPTION = 131072;
        public const uint FILE_SUPPORTS_OBJECT_IDS = 65536;
        public const uint FILE_SUPPORTS_REMOTE_STORAGE = 256;
        public const uint FILE_SUPPORTS_REPARSE_POINTS = 128;
        public const uint FILE_SUPPORTS_SPARSE_FILES = 64;
        public const uint FILE_UNICODE_ON_DISK = 4;
        public const uint FILE_VOLUME_IS_COMPRESSED = 32768;
        public const uint FILE_VOLUME_QUOTAS = 32;
        public const uint fInX = 512;
        public const uint fNull = 2048;
        public const uint fOutX = 256;
        public const uint fOutxCtsFlow = 4;
        public const uint fOutxDsrFlow = 8;
        public const uint fParity = 2;
        public const uint fReserved = 4294967168;
        public const uint fRlsdHold = 4;
        public const uint fRtsControl = 12288;
        public const uint fTXContinueOnXoff = 128;
        public const uint fTxim = 64;
        public const uint fXoffHold = 8;
        public const uint fXoffSent = 16;
        public const uint GENERIC_ALL = 268435456;
        public const uint GENERIC_EXECUTE = 536870912;
        public const uint GENERIC_READ = 2147483648;
        public const uint GENERIC_WRITE = 1073741824;
        public const int INVALID_HANDLE_VALUE = -1;
        public const int MAILSLOT_NO_MESSAGE = -1;
        public const int MAILSLOT_WAIT_FOREVER = -1;
        public const byte MARKPARITY = 3;
        public const uint MODULE_ATTR_NODEBUG = 1024;
        public const uint MODULE_ATTR_NOT_TRUSTED = 512;
        public const uint MS_CTS_ON = 16;
        public const uint MS_DSR_ON = 32;
        public const uint MS_RING_ON = 64;
        public const uint MS_RLSD_ON = 128;
        public const byte NOPARITY = 0;
        public const byte ODDPARITY = 1;
        public const byte ONE5STOPBITS = 1;
        public const byte ONESTOPBIT = 0;
        public const uint OPEN_ALWAYS = 4;
        public const uint OPEN_EXISTING = 3;
        public const uint OPEN_FOR_LOADER = 6;
        public const uint PARITY_EVEN = 1024;
        public const uint PARITY_MARK = 2048;
        public const uint PARITY_NONE = 256;
        public const uint PARITY_ODD = 512;
        public const uint PARITY_SPACE = 4096;
        public const uint PCF_16BITMODE = 512;
        public const uint PCF_DTRDSR = 1;
        public const uint PCF_INTTIMEOUTS = 128;
        public const uint PCF_PARITY_CHECK = 8;
        public const uint PCF_RLSD = 4;
        public const uint PCF_RTSCTS = 2;
        public const uint PCF_SETXCHAR = 32;
        public const uint PCF_SPECIALCHARS = 256;
        public const uint PCF_TOTALTIMEOUTS = 64;
        public const uint PCF_XONXOFF = 16;
        public const uint PST_FAX = 33;
        public const uint PST_LAT = 257;
        public const uint PST_MODEM = 6;
        public const uint PST_NETWORK_BRIDGE = 256;
        public const uint PST_PARALLELPORT = 2;
        public const uint PST_RS232 = 1;
        public const uint PST_RS422 = 3;
        public const uint PST_RS423 = 4;
        public const uint PST_RS449 = 5;
        public const uint PST_SCANNER = 34;
        public const uint PST_TCPIP_TELNET = 258;
        public const uint PST_UNSPECIFIED = 0;
        public const uint PST_X25 = 259;
        public const uint RTS_CONTROL_DISABLE = 0;
        public const uint RTS_CONTROL_ENABLE = 4096;
        public const uint RTS_CONTROL_HANDSHAKE = 8192;
        public const uint RTS_CONTROL_TOGGLE = 12288;
        public const uint SETBREAK = 8;
        public const uint SETDTR = 5;
        public const uint SETIR = 10;
        public const uint SETRTS = 3;
        public const uint SETXOFF = 1;
        public const uint SETXON = 2;
        public const byte SP_BAUD = 2;
        public const byte SP_DATABITS = 4;
        public const byte SP_HANDSHAKING = 16;
        public const byte SP_PARITY = 1;
        public const byte SP_PARITY_CHECK = 32;
        public const byte SP_RLSD = 64;
        public const byte SP_STOPBITS = 8;
        public const byte SPACEPARITY = 4;
        public const uint STOPBITS_10 = 1;
        public const uint STOPBITS_15 = 2;
        public const uint STOPBITS_20 = 4;
        public const uint TRUNCATE_EXISTING = 5;
        public const byte TWOSTOPBITS = 2;

        class SHELLEXECUTEEX
        {
            public UInt32 cbSize;
            public UInt32 fMask;
            public IntPtr hwnd;
            public IntPtr lpVerb;
            public IntPtr lpFile;
            public IntPtr lpParameters;
            public IntPtr lpDirectory;
            public int nShow;
            public IntPtr hInstApp;

            // Optional members 
            public IntPtr lpIDList;
            public IntPtr lpClass;
            public IntPtr hkeyClass;
            public UInt32 dwHotKey;
            public IntPtr hIcon;
            public IntPtr hProcess;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SHELLEXECUTEINFO
        {
            public int cbSize;
            public uint fMask;
            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpVerb;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpFile;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpParameters;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpDirectory;
            public int nShow;
            public IntPtr hInstApp;
            public IntPtr lpIDList;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpClass;
            public IntPtr hkeyClass;
            public uint dwHotKey;
            public IntPtr hIcon;
            public IntPtr hProcess;
        }


        #region 自定义变量和属性
        public struct SystemTime
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMiliseconds;

        }
        public struct FILETIME
        {
            Int32 dwLowDateTime;
            Int32 dwHighDateTime;
        }
        [DllImport("coredll")]
        public static extern bool SetLocalTime(ref SystemTime sysTime);
        [DllImport("kernel32.dll", SetLastError = true, EntryPoint = "SetLocalTime")]
        private static extern int SetLocalTime_win32(ref SystemTime lpSystemTime);

        [DllImport("coredll")]　//不太确定int
        public static extern int SetFileTime(IntPtr hFile, ref FILETIME sysCreateTime, ref FILETIME sysAccessTime, ref FILETIME sysModifyTime);
        //long lCreationTime    = creationTime.ToFileTime();
        //long lAccessTime    = accessTime.ToFileTime();
        //long lWriteTime        = writeTime.ToFileTime();
        //if(!SetFileTime(hFile, ref lCreationTime, ref lAccessTime, ref lWriteTime))
        //{
        //    throw new Win32Exception();
        //}
        [DllImport("kernel32.dll", SetLastError = true, EntryPoint = "SetFileTime")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetFileTime_win32(IntPtr hFile, ref long lpCreationTime, ref long lpLastAccessTime, ref long lpLastWriteTime);
 
        [DllImport("coredll")]
        private static extern int FileTimeToSystemTime( IntPtr lpFileTime, IntPtr lpSystemTime);
        [DllImport("kernel32.dll", CallingConvention = CallingConvention.Winapi, SetLastError = true, EntryPoint = "FileTimeToSystemTime")]
        static extern bool FileTimeToSystemTime_win32([In] ref FILETIME lpFileTime, out SystemTime lpSystemTime);

        [DllImport("coredll")]
        private static extern int SystemTimeToFileTime( IntPtr lpSystemTime, IntPtr lpFileTime );        
        [DllImport("kernel32.dll", EntryPoint = "SystemTimeToFileTime")]
        static extern bool SystemTimeToFileTime_win32([In] IntPtr lpSystemTime, IntPtr lpFileTime);

        [DllImport("coredll")]
        public extern static bool CloseHandle(IntPtr hObject);
        [DllImport("kernel32", EntryPoint = "CloseHandle")]
        public extern static bool CloseHandle_win32(IntPtr hObject);

        [DllImport("coredll")]
        public extern static IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);
        [DllImport("kernel32", EntryPoint = "CreateFile")]
        public extern static IntPtr CreateFile_win32(string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        [DllImport("coredll")]
        extern static int ShellExecuteEx(SHELLEXECUTEEX ex);
        [DllImport("shell32.dll", CharSet = CharSet.Auto, EntryPoint = "ShellExecuteEx")]
        static extern bool ShellExecuteEx_win32(ref SHELLEXECUTEINFO lpExecInfo);

        [DllImport("coredll")]
        extern static IntPtr LocalAlloc(int flags, int size);
        [DllImport("kernel32.dll", EntryPoint = "LocalAlloc")]
        static extern IntPtr LocalAlloc_win32(uint uFlags, int uBytes);

        [DllImport("coredll")]
        extern static void LocalFree(IntPtr ptr);
        [DllImport("kernel32.dll", SetLastError = true, EntryPoint = "LocalFree")]
        static extern IntPtr LocalFree_win32(IntPtr hMem);
 
        #endregion 自定义变量和属性

        #region FileTimeToSystemTime

        public static string FILETIMEtoDataTime(FILETIME time)
        {
            IntPtr filetime = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(FILETIME)));
            IntPtr systime = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SystemTime)));
            Marshal.StructureToPtr(time, filetime, false); //true不支持
            FileTimeToSystemTime(filetime, systime);
            SystemTime st = (SystemTime)Marshal.PtrToStructure(systime, typeof(SystemTime));
            string Time = st.wYear.ToString() + "." + st.wMonth.ToString() + "." + st.wDay.ToString() + "." + st.wHour.ToString() + "." + st.wMinute.ToString() + "." + st.wSecond.ToString();
            return Time;
        }

        public static FILETIME SystemTimetoFILETIME(SystemTime time)
        {
            IntPtr filetime = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(FILETIME)));
            IntPtr systime = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SystemTime)));
            Marshal.StructureToPtr(time, systime, false);//true不支持
            SystemTimeToFileTime(systime, filetime);
            FILETIME st = (FILETIME)Marshal.PtrToStructure(filetime, typeof(FILETIME));
            //string timestr = FILETIMEtoDataTime(st);
            // string Time = st.wYear.ToString() + "." + st.wMonth.ToString() + "." + st.wDay.ToString() + "." + st.wHour.ToString() + "." + st.wMinute.ToString() + "." + st.wSecond.ToString();
            return st;
        }
        #endregion
        /// <summary>
        /// 运行程序
        /// </summary>
        /// <param name="sFilePath">程序文件名</param>
        public static void ExeFile(string sFilePath)
        {
            int nSize = sFilePath.Length * 2 + 2;
            IntPtr pData = LocalAlloc(0x40, nSize);
            Marshal.Copy(System.Text.Encoding.Unicode.GetBytes(sFilePath), 0, pData, nSize - 2);
            SHELLEXECUTEEX see = new SHELLEXECUTEEX();
            see.cbSize = 60;
            see.dwHotKey = 0;
            see.fMask = 0;
            see.hIcon = IntPtr.Zero;
            see.hInstApp = IntPtr.Zero;
            see.hProcess = IntPtr.Zero; ;
            see.lpClass = IntPtr.Zero;
            see.lpDirectory = IntPtr.Zero;
            see.lpIDList = IntPtr.Zero;
            see.lpParameters = IntPtr.Zero;
            see.lpVerb = IntPtr.Zero;
            see.hwnd = IntPtr.Zero;
            see.hkeyClass = IntPtr.Zero;
            see.nShow = 1;
            see.lpFile = pData;
            ShellExecuteEx(see);
            LocalFree(pData);
        }
    }
}