using BMS;
using BMS.Model;
using BMS.Utils;
using CustomAlertBoxDemo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notification
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.Alert("Info Alert", Form_Alert.enmType.Info);
            //ShowNotify();
            //var processs = Process.GetProcessesByName("Notification");
            //foreach (var process in Process.GetProcessesByName("Notification"))
            //{
            //    process.Kill();
            //}

            //return;
            Thread thread = new Thread(ShowNotify);
            thread.IsBackground = true;
            thread.Start();
        }

        public void Alert(string msg, string content, Form_Alert.enmType type, int id)
        {
            Form_Alert frm = new Form_Alert();
            frm.pictureBox1.Tag = id;
            //frm.MdiParent = this;
            //if (frm.InvokeRequired)
            //{
            //    frm.Invoke(new Action(() =>
            //    {
            frm.showAlert(msg, content, type);
            //    }));
            //}
            //else
            //{
            //    frm.showAlert(msg, content, type);
            //}

        }


        void ShowNotify()
        {

            while (true)
            {
                Thread.Sleep(10000);

                string strPath = Application.StartupPath.ToString() + @"\Session.ini";
                FileStream file = new FileStream(strPath, FileMode.OpenOrCreate, FileAccess.Read);
                StreamReader sr = new StreamReader(file);
                int employeeID = TextUtils.ToInt(sr.ReadLine());
                var exp1 = new Expression("EmployeeID", employeeID);
                var exp2 = new Expression("NotifyStatus", 1);

                //List<NotifyModel> listNotifies = SQLHelper<NotifyModel>.FindByExpression(exp1.And(exp2));
                List<NotifyModel> listNotifies = SQLHelper<NotifyModel>.FindByExpression(exp2).Take(5).ToList();
                foreach (var item in listNotifies)
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            Alert($"{item.Title}", $"{item.Text}", Form_Alert.enmType.Info, item.ID);
                        }));
                    }
                    else
                    {
                        Alert($"{item.Title}", $"{item.Text}", Form_Alert.enmType.Info, item.ID);
                    }
                }

                file.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Alert("Info Alert", "", Form_Alert.enmType.Info);
        }
    }
}
