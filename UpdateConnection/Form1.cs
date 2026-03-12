using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace UpdateConnection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // The path to the key where Windows looks for startup applications
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (rkApp.GetValue("AppUpdateConnection") == null)
            {
                // Add the value in the registry so that the application runs at startup
                rkApp.SetValue("AppUpdateConnection", Application.ExecutablePath);
            }

            string filePath = @"Z:\Software\RTC\Server.ini";
            
            //rkApp.DeleteValue("AppUpdateConnection", false);

            while (true)
            {
                try
                {
                    string realServer = new System.Net.WebClient().DownloadString("https://api.ipify.org");
                    string server = BMS.Utils.MD5.DecodeChecksum(File.ReadAllText(filePath));
                    if (realServer.Trim() != server.Trim())
                    {
                        string cServer = BMS.Utils.MD5.EncodeChecksum(realServer.Trim());
                        File.WriteAllText(filePath, cServer);
                        string fileUpdate = @"Z:\Software\RTC\UpdateNumber.ini";
                        string number = File.ReadAllText(fileUpdate);
                        File.WriteAllText(fileUpdate, (int.Parse(number) + 1).ToString());
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    Thread.Sleep(15000);
                }
            }
        }
    }
}
