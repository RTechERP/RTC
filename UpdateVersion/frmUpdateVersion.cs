using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using UpdateVersion;

namespace BMS.UP
{
    public partial class frmUpdateVersion : Form
    {
        public frmUpdateVersion()
        {
            InitializeComponent();
        }

        void TurnOffApp(string appName)
        {
            Process[] arr = Process.GetProcessesByName(appName);
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i].Kill();
            }
            Thread.Sleep(1000);
        }

        string _pathFileConfigUpdate = Path.Combine(Application.StartupPath, "ConfigUpdate.txt");
        string _extractPath = Path.GetFullPath(Application.StartupPath);
        string _pathFolderUpdate = "";
        string _pathUpdateServer = "";
        string _appName = "";
        string _pathFileVersion = "";
        bool _isErrorExtract = false;


        string _folderUpdate = ""; // Thư mục chứa file update trên local
        string _urlFileUpdate = ""; //Url api get file update trên server
        string _urlDownload = ""; // Url api download file từ server
        string _filePathVersion = "";

        void updateVersion(string PathFileVersion)
        {
            try
            {
                string[] listFileSv = DocUtils.GetFilesList(@_pathUpdateServer);
                List<string> lst = listFileSv.ToList();
                lst = lst.Where(o => o.Contains(".zip")).ToList();
                int newVersion = lst.Max(o => TextUtils.ToInt(Path.GetFileNameWithoutExtension(o)));
                string fileName = newVersion + ".zip";

                if (File.Exists(Path.Combine(_pathFolderUpdate, fileName)))
                {
                    File.Delete(Path.Combine(_pathFolderUpdate, fileName));
                }
                DocUtils.DownloadFile(_pathFolderUpdate, fileName, Path.Combine(_pathUpdateServer, fileName));
                //DocUtils.DownloadFileFromServer(_pathFolderUpdate, fileName, Path.Combine(_pathUpdateServer, fileName));

                if (File.Exists(Path.Combine(_pathFolderUpdate, fileName)))
                {
                    if (!_extractPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                        _extractPath += Path.DirectorySeparatorChar;
                    using (ZipArchive archive = ZipFile.OpenRead(Path.Combine(_pathFolderUpdate, fileName)))
                    {
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            try
                            {
                                string destinationPath = Path.GetFullPath(Path.Combine(_extractPath, entry.FullName));
                                if (destinationPath.StartsWith(_extractPath, StringComparison.Ordinal))
                                    entry.ExtractToFile(destinationPath, true);
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.ToString());
                                _isErrorExtract = true;
                                break;
                            }
                        }
                        if (!_isErrorExtract)
                        {
                            File.WriteAllText(PathFileVersion, TextUtils.ToString(newVersion));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void UpdateNewVersionAPI()
        {
            try
            {
                //Get version mới nhất trên API
                HttpClient client = new HttpClient();
                var result = client.GetAsync(_urlFileUpdate);
                var r = result.Result;
                string a = r.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<ReponseFileUpdate>(a);
                if (obj.Status == 1)
                {
                    //Download file về thư mục update
                    string newFile = Path.GetFileName(obj.NewVersion);
                    Uri uri = new Uri(_urlDownload + newFile);
                    WebClient webClient = new WebClient();
                    webClient.DownloadFile(uri, Path.Combine(_folderUpdate, newFile));

                    //Giải nén file vừa download
                    //MessageBox.Show(_folderUpdate);
                    if (File.Exists(Path.Combine(_folderUpdate, newFile)))
                    {
                        if (!_extractPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                        {
                            _extractPath += Path.DirectorySeparatorChar;
                        }
                        using (ZipArchive archive = ZipFile.OpenRead(Path.Combine(_folderUpdate, newFile)))
                        {
                            foreach (ZipArchiveEntry entry in archive.Entries)
                            {
                                try
                                {
                                    string destinationPath = Path.GetFullPath(Path.Combine(_extractPath, entry.FullName));
                                    if (destinationPath.StartsWith(_extractPath, StringComparison.Ordinal))
                                        entry.ExtractToFile(destinationPath, true);
                                }
                                catch (Exception e)
                                {
                                    MessageBox.Show(e.ToString());
                                    _isErrorExtract = true;
                                    break;
                                }
                            }
                            if (!_isErrorExtract)
                            {
                                File.WriteAllText(_filePathVersion, Path.GetFileNameWithoutExtension(obj.NewVersion));
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
                Application.Exit();
            }

        }

        void SetValueVariableAPI()
        {
            try
            {
                string filePath = $@"\\192.168.1.190\Software\Config";
                //string fileConfig = Path.Combine(Application.StartupPath, "ConfigUpdateAPI.txt");
                string fileConfig = Path.Combine(filePath, "ConfigUpdateAPI.txt");

                string[] lines = File.ReadAllLines(fileConfig);

                string[] values = lines[1].Split('|');

                _appName = values[0].Trim();
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

        private void Form1_Load(object sender, EventArgs e)
        {
            //string[] arrSv = File.ReadAllLines(Path.Combine(Application.StartupPath, "ftpServer.txt"));
            //if (arrSv.Length < 3) MessageBox.Show("Lỗi file ftpServer.txt. Hãy kiểm tra lại! ");

            //DocUtils.InitFTPQLSX(arrSv[0].Trim(), arrSv[1].Trim(), arrSv[2].Trim());
            //string[] lines = File.ReadAllLines(_pathFileConfigUpdate);
            //string[] stringSeparators = new string[] { "||" };
            //string[] arr = lines[1].Split(stringSeparators, 4, StringSplitOptions.RemoveEmptyEntries);
            //_appName = arr[0].Trim();
            //_pathFolderUpdate = Path.Combine(Application.StartupPath, arr[1].Trim());
            //_pathUpdateServer = arr[2].Trim();
            //_pathFileVersion = Path.Combine(Application.StartupPath, arr[3].Trim());

            //setValueVariable();
            SetValueVariableAPI();

            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
            else
            {
                //Tắt các ứng dụng RTC
                try
                {
                    TurnOffApp(_appName);
                    backgroundWorker1.RunWorkerAsync();
                }
                catch (Exception ex)
                {
                    Application.Exit();
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //updateVersion(_pathFileVersion);
                UpdateNewVersionAPI();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!_isErrorExtract)
            {
                //MessageBox.Show(Path.Combine(Application.StartupPath, _appName + ".exe"));
                Process.Start(Path.Combine(Application.StartupPath, _appName + ".exe"));
                Application.Exit();
            }
        }
    }
}
