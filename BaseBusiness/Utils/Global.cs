using BMS.Utils;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace BMS
{
    public static class Global
    {
        //public static string Pass`
        static int _AppUserID;
        static int _AppGroupID;
        static string _AppUserName = "";
        static string _AppFullName = "";
        static string _AppPassword;
        static int _AppRegionID;
        static bool _IsRoot = false;

        public static int[] IDAdminDemo = new int[] { 24, 1434, 88, 1534 };
        public static int[] idAdminSale = new int[] { 1, 2, 1293, 1177, 1313, 23, 1380, 1132, 11, 17, 1185, 1463, 1431, 1604 }; //UserID
        public static int[] POSITION_ADMIN_SALE_ID = new int[] { 17,101,92,55,94,98 }; //ID Chức vụ nội bộ

        public static int[] userAllsOfficeSupply = new int[] { 354, 156, 331, 4, 505, 515, 395, 526, 396, 557, 598 }; //EmployeeID
        public static List<int> departmentIDs = new List<int> { 9, 10, Global.DepartmentID };
        //public static string kpiFolderTechnical = @"D:\LeTheAnh\RTC\KPI";
        public static string kpiFolderTechnical = @"\\192.168.1.190\Technical\23.KPI";
        //public static string kpiFolderTechnical = @"\\192.168.1.190\Software\Test";
        public static string configFileName = "ConfigUpdateAPI.txt";

        //static string _ConnectionString = @"Server=DESKTOP-40H717B\SQLEXPRESS;Initial Catalog=RTC;User ID=sa;Password=123456a@"; //server Local
        //static string _ConnectionString = @"Server=192.168.1.3,9000;Initial Catalog=RTC;User ID=sa;Password=1"; //server test
        static string _ConnectionString = ""; //Server public;

        public static bool DebugFlag
        {
            get
            {
                bool isDebug = false;
                isDebug = !string.IsNullOrWhiteSpace(_ConnectionString);
                //string code1 = "GCN-12-ÐO";
                //string code2 = "GCN-12-ĐO";

                //if (code1 == code2)
                //{
                //    return true;
                //}
                return isDebug;
            }
        }

        public static string UrlAPI
        {
            get
            {
                string url = "https://localhost:44309/api";
                if (string.IsNullOrWhiteSpace(_ConnectionString)) url = "http://113.190.234.64:9090/rtcapi/api";
                return url;
            }
        }

        public static string Host
        {
            get
            {
                string host = "http://localhost:8390";
                if (string.IsNullOrWhiteSpace(_ConnectionString)) host = "http://113.190.234.64:8083";
                return host;
            }
        }

        public static string HostKPITeam
        {
            get
            {
                string url = "http://10.20.29.65:8088/rerpapi/api";
                if (string.IsNullOrWhiteSpace(_ConnectionString)) url = "https://erp.rtc.edu.vn/api/api";
                return url;
            }
        }

        public static string PathLocationProject
        {
            get
            {
                string pathLocation = @"\\192.168.1.190\duan\Projects"; //Thư mục trên server
                if (Global.IsOnline && string.IsNullOrEmpty(_ConnectionString)) pathLocation = @"\\113.190.234.64\duan\Projects";
                else if (!string.IsNullOrEmpty(_ConnectionString)) pathLocation = @"D:\LeTheAnh\RTC\Project"; //Thư mục test local

                return pathLocation;
            }
        }

        public static string PathCoursePDF
        {
            get
            {
                //string path = @"\\192.168.1.2\ftp\Upload\Course\Test\";
                string path = @"\\113.190.234.64\Software\ftp\Upload\Course\Test\PDFFileLesson\";
                if (string.IsNullOrEmpty(_ConnectionString))
                {
                    //path = @"\\192.168.1.2\ftp\Upload\Course\PDFFileLesson\";
                    path = @"\\113.190.234.64\Software\ftp\Upload\Course\PDFFileLesson\";
                }
                return path;
            }
        }

        public static string PathCourseFile
        {
            get
            {
                string path = @"\\113.190.234.64\Software\ftp\Upload\Course\Test\";
                if (string.IsNullOrEmpty(_ConnectionString))
                {
                    //path = @"\\192.168.1.2\ftp\Upload\Course\";
                    path = @"\\113.190.234.64\Software\ftp\Upload\Course\";

                }
                return path;
            }
        }


        static string _AppUserCode;
        static int _StoreID;
        public static int DepartmentID = 1;

        static decimal _ExchangeRate = 0;
        static string _CashierNo = "";
        static int _ShiftID = 0;
        static bool _isNotCreateSession = true;

        static string _ServerName = "";
        static string _DatabaseName = "";

        static int _OutletID;
        static int _ZoneID;
        static int _ComputerID;

        static int _ExportPrice;

        static int _MainViewID;
        public static string ComputerName { get; set; }
        public static string ComputerMACAddresses { get; set; }
        public static bool IsTest { get; set; }

        static public int ExportPrice
        {
            get { return _ExportPrice; }
            set { _ExportPrice = value; }
        }

        static public int ComputerID
        {
            get { return _ComputerID; }
            set { _ComputerID = value; }
        }

        static public decimal ExchangeRate
        {
            get { return _ExchangeRate; }
            set { _ExchangeRate = value; }
        }

        /// <summary>
        /// Access routine for global variable.
        /// </summary>
        public static int ShiftID
        {
            get { return _ShiftID; }
            set { _ShiftID = value; }
        }

        /// <summary>
        /// Access routine for global variable.
        /// </summary>
        public static bool IsNotCreateSession
        {
            get { return _isNotCreateSession; }
            set { _isNotCreateSession = value; }
        }

        /// <summary>
        /// Access routine for global variable.
        /// </summary>
        public static string CashierNo
        {
            get { return _CashierNo; }
            set { _CashierNo = value; }
        }
        /// <summary>
        /// Access routine for global variable.
        /// </summary>
        public static string ServerName
        {
            get { return _ServerName; }
            set { _ServerName = value; }
        }
        /// <summary>
        /// Access routine for global variable.
        /// </summary>
        public static string DatabaseName
        {
            get { return _DatabaseName; }
            set { _DatabaseName = value; }
        }

        public static string LoginName
        {
            get { return Global._AppUserCode; }
            set { Global._AppUserCode = value; }
        }

        public static int UserID
        {
            get { return _AppUserID; }
            set { _AppUserID = value; }
        }

        public static int MainViewID
        {
            get { return _MainViewID; }
            set { _MainViewID = value; }
        }

        public static int RegionID
        {
            get { return _AppRegionID; }
            set { _AppRegionID = value; }
        }

        public static string ConnectionString
        {
            get
            {
                string connectString = _ConnectionString;
                if (string.IsNullOrEmpty(_ConnectionString))
                {
                    string strPath = Application.StartupPath.ToString() + @"\" + Global.DefaultFileName;
                    //string server = MD5.DecodeChecksum(File.ReadAllText(Application.StartupPath.ToString() + @"\Server.ini").Trim());
                    string conn = MD5.DecodeChecksum(File.ReadAllText(strPath).Trim());
                    //_ConnectionString = conn;
                    connectString = conn;
                }
                return connectString;
            }
            //set { _ConnectionString = value; }
        }

        public static int AppGroupID
        {
            get { return _AppGroupID; }
            set { _AppGroupID = value; }
        }

        public static string AppUserName
        {
            get { return _AppUserName; }
            set { _AppUserName = value; }
        }

        public static string AppFullName
        {
            get { return _AppFullName; }
            set { _AppFullName = value; }
        }

        public static string AppPassword
        {
            get { return _AppPassword; }
            set { _AppPassword = value; }
        }

        public static bool IsRoot
        {
            get { return _IsRoot; }
            set { _IsRoot = value; }
        }

        public static int StoreID
        {
            get { return _StoreID; }
            set { _StoreID = value; }
        }

        public static int OutlefID
        {
            get { return _OutletID; }
            set { _OutletID = value; }
        }

        public static int ZoneID
        {
            get { return _ZoneID; }
            set { _ZoneID = value; }
        }
        public static string DefaultFileName { get; set; }
        public static int SessionID { get; set; }
        public static int ChangePassType { get; set; }//0: bình thường, k đổi pass, 1: hết hạn đổi pass, 2: Reset pass

        public static bool IsInvStore { get; set; }

        public static string BPX_Ext { get; set; }

        public static string PBXNum { get; set; }

        public static string Default_PBX_Interface { get; set; }

        public static string SiteName { get; set; }

        public static string DebitNoteDetailPrintName { get; set; }

        public static string DebitNoteDetailPrintName_ENG { get; set; }
        public static bool IsAdmin { get; set; }
        public static int EmployeeID { get; set; }
        public static int HeadOfDepartment { get; set; }
        public static string DepartmentName { get; set; }
        public static string AppCodeName { get; set; }
        public static int IsLeader { get; set; }
        public static string PositionCode { get; set; }
        public static int UserTeamID { get; set; }
        public static string DepartmentCode { get; set; }
        public static bool IsAdminSale { get; set; }
        public static bool IsOnline { get; set; }
    }
}
