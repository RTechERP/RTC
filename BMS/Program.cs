using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualBasic.ApplicationServices;
using System.Security.AccessControl;
using System.IO;
using DevExpress.XtraGrid.Localization;
using System.Globalization;
using RTC;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json;
using static Forms.Classes.cGlobVar;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace BMS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static List<int> RequestIDList = new List<int>();

        static string _folderUpdate = ""; // Thư mục chứa file update trên local
        static string _urlFileUpdate = ""; //Url get file update trên server
        static string _urlDownload = ""; // Url download file từ server
        static string _filePathVersion = "";

        [STAThread]
        static void Main(string[] args)
        {
            //Application.Run(new Form1());


            try
            {
                Application.EnableVisualStyles();

                CultureInfo customCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
                customCulture.NumberFormat.NumberDecimalSeparator = ".";
                customCulture.NumberFormat.NumberGroupSeparator = ",";
                customCulture.NumberFormat.CurrencySymbol = "đ";
                customCulture.DateTimeFormat.FullDateTimePattern = "dd/MM/yyyy HH:mm:ss";

                Thread.CurrentThread.CurrentCulture = customCulture;
                //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("vi-VN");

                GridLocalizer.Active = new NVGridLocalizer();
                Application.SetCompatibleTextRenderingDefault(false);

                #region Set Full Permission for Users Group
                try
                {
                    bool setPermission = SetAccessRule();
                }
                catch
                {
                }
                #endregion

                //clsSingleInstance.Run(new frmMain());

                var processNotifies = Process.GetProcessesByName("Notification");
                foreach (var process in processNotifies)
                {
                    process.Kill();
                }

                //Check update version
                if (IscheckUpdateVersionAPI())
                {
                    string pathUpdate = Path.Combine(Application.StartupPath, "UpdateVersion.exe");
                    if (File.Exists(pathUpdate))
                    {
                        //var processNotifies = Process.GetProcessesByName("Notification");
                        //foreach (var process in processNotifies)
                        //{
                        //    process.Kill();
                        //}
                        //Process.Start(pathUpdate);
                    }
                    Process.Start(Path.Combine(Application.StartupPath, "UpdateVersion.exe"));
                }
                else //Login
                {
                    frmLogin frm = new frmLogin();
                    frm.ShowDialog();

                    if (frm.loginSuccess == false)
                    {
                        return;
                    }

                    //string pathNotify = Path.Combine(Application.StartupPath, "Notification.exe");
                    //if (File.Exists(pathNotify))
                    //{
                    //    //Process.Start(pathNotify);
                    //}
                }


                Application.Run(new frmMain());
                //Application.Run(new frmMainNew());


                //Application.Run(new frmTest());
                //Main f = new Main();
                //SingleInstance.Run(f, new StartupNextInstanceEventHandler(
                //    SingleInstance.StartupNextInstanceHandler));
                //Application.Run(new ());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        static bool SetAccessRule()
        {
            FileSystemRights pRights = (FileSystemRights)0;
            pRights = FileSystemRights.FullControl;

            // [HocPD]: Add Access Rule to the actual directory itself
            FileSystemAccessRule pAccessRule = new FileSystemAccessRule("Users", pRights,
                                        InheritanceFlags.None,
                                        PropagationFlags.NoPropagateInherit,
                                        AccessControlType.Allow);

            DirectoryInfo pInfo = new DirectoryInfo(Application.StartupPath);
            DirectorySecurity pSecurity = pInfo.GetAccessControl(AccessControlSections.Access);

            bool pResult = false;
            pSecurity.ModifyAccessRule(AccessControlModification.Set, pAccessRule, out pResult);

            if (!pResult)
                return false;

            // [HocPD]: Always allow objects to inherit on a directory
            InheritanceFlags iFlags = InheritanceFlags.ObjectInherit;
            iFlags = InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit;

            // [HocPD]: Add Access rule for the inheritance
            pAccessRule = new FileSystemAccessRule("Users", pRights,
                                        iFlags,
                                        PropagationFlags.InheritOnly,
                                        AccessControlType.Allow);
            pResult = false;
            pSecurity.ModifyAccessRule(AccessControlModification.Add, pAccessRule, out pResult);

            if (!pResult)
                return false;

            pInfo.SetAccessControl(pSecurity);

            return true;
        }

        static bool IscheckUpdateVersionAPI()
        {
            SetValueVariableAPI();
            bool isUpdate = false;
            try
            {
                //Get version hiện tại
                int currentVersion = TextUtils.ToInt(File.ReadAllText(_filePathVersion));

                //Get version mới nhất trên API
                HttpClient client = new HttpClient();
                var result = client.GetAsync(_urlFileUpdate);
                var r = result.Result;
                string a = r.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<ReponseFileUpdate>(a);
                if (obj.Status == 1)
                {
                    int newVersion = TextUtils.ToInt(Path.GetFileNameWithoutExtension(obj.NewVersion));
                    //MessageBox.Show(newVersion.ToString(), currentVersion.ToString());
                    if (currentVersion != newVersion)
                    {
                        isUpdate = true;
                    }
                }

                //MessageBox.Show(isUpdate.ToString());
                return isUpdate;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Thông báo");
                return false;
            }
        }

        static void SetValueVariableAPI()
        {
            try
            {
                string filePath = $@"\\113.190.234.64\Software\Config";
                //string fileConfig = Path.Combine(Application.StartupPath, "ConfigUpdateAPI.txt");
                string fileConfig = Path.Combine(filePath, Global.configFileName);

                if (!File.Exists(fileConfig))
                {
                    filePath = $@"\\192.168.1.190\Software\Config";
                    fileConfig = Path.Combine(filePath, Global.configFileName);

                    if (!File.Exists(fileConfig))
                    {
                        MessageBox.Show($@"Vui lòng đăng nhập vào server \\192.168.1.190 hoặc \\113.190.234.64 (Nếu bạn đang truy cập online)\nNếu chưa có tài khoản vui lòng liên hệ IT để được cấp!", "Thông báo");
                        return;
                    }
                }

                string[] lines = File.ReadAllLines(fileConfig);

                string[] values = lines[1].Split('|');

                _folderUpdate = Path.Combine(Application.StartupPath, values[1].Trim());
                _urlFileUpdate = values[2].Trim();
                _urlDownload = values[3].Trim();
                _filePathVersion = Path.Combine(Application.StartupPath, values[4].Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update version fail!\n" + ex.Message, "Thông báo");
                Application.Exit();
            }
        }

        //static async void SetValueVariableAPI()
        //{
        //    string message = "";
        //    try
        //    {
        //        HttpClient http = new HttpClient();
        //        string host = "https://localhost:44365/api";
        //        host = Global.HostKPITeam;

        //        string api = host + "/home/get-config-autoupdate";
        //        var repsonse = await http.GetAsync(api);
        //        string content = await repsonse.Content.ReadAsStringAsync();
        //        JObject json = JObject.Parse(content);

        //        var status = TextUtils.ToInt(json["status"]);
        //        if (status == 1)
        //        {
        //            //var data = json["data"];

        //            if (json.TryGetValue("data", out JToken data))
        //            {
        //                _urlFileUpdate = TextUtils.ToString(data["LinkFileUpdate"]);
        //                _urlDownload = TextUtils.ToString(data["LinkDownload"]);
        //                _filePathVersion = Path.Combine(Application.StartupPath, TextUtils.ToString(data["Version"]));
        //            }
        //        }
        //        else message = TextUtils.ToString(json["message"]);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Update version fail!\n" + ex.Message + $"\r\n{message}", "Thông báo");
        //        //Application.Exit();
        //    }
        //}
    }
}