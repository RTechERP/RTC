using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BMS.Model;
using BMS.Utils;
using System.IO;
using System.Collections;
using BMS.Business;
using System.Threading;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Net.NetworkInformation;

namespace BMS
{
    public partial class frmLogin : Form
    {
        public bool loginSuccess = false;
        public string loginname = "";
        public string pass = "";
        string pImage = "";
        public frmLogin()
        {
            InitializeComponent();

            //CheckAutoUpdate();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            //pnl.Top = (this.Height - pnl.Height) / 2;
            //pnl.Left = (this.Width - pnl.Width) / 2;
            bool _IsOK = true;

            //LoadLogo(ref _IsOK);



            //Ket noi duoc voi db thi tiep tuc
            if (_IsOK == true)
            {
                getLastlog();

                txtUserName.Focus();
                if (txtUserName.Text != "")
                {
                    txtPassword.Select();
                }
            }
            else
                this.Close();



            
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            ProcessLogIn();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ProcessLogIn()
        {
            //Check sử dụng wifi công ty không
            //Ping ping = new Ping();
            //PingReply reply = ping.Send("192.168.1.2");
            Global.DefaultFileName = !chkIsOnline.Checked ? "default.ini" : "defaultonline.ini";
            Global.IsOnline = chkIsOnline.Checked;

            //DBUtils.GetNewDBConnectionString(100);
            //using (var l_oConnection = new SqlConnection(Global.ConnectionString))
            //{
            //    try
            //    {                    
            //        l_oConnection.Open();
            //        l_oConnection.Close();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Không thể kết nối được với server!" + Environment.NewLine + ex.Message, "TPA - Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }
            //}
            #region Validate Login
            if (txtUserName.Text.Trim() == "" || txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập đầy đủ tên đăng nhập và mật khẩu !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion

            #region Khai báo biến
            //UsersModel userModel = null;
            string _user = txtUserName.Text;
            string _pass = BMS.Utils.MD5.EncryptPassword(txtPassword.Text);
            //_pass = txtPassword.Text;
            #endregion

            try
            {
                DataTable dt = TextUtils.LoadDataFromSP("spLogin", "A",
                                                    new string[] { "@LoginName", "@Password" },
                                                    new object[] { _user, _pass });
                #region Nếu không phải thì kiểm tra
                if (dt.Rows.Count > 0)
                {
                    Global.UserID = TextUtils.ToInt(dt.Rows[0]["ID"]);
                    Global.LoginName = TextUtils.ToString(dt.Rows[0]["LoginName"]);
                    Global.AppUserName = TextUtils.ToString(dt.Rows[0]["LoginName"]);
                    Global.AppFullName = TextUtils.ToString(dt.Rows[0]["FullName"]);
                    Global.AppPassword = _pass;
                    Global.MainViewID = TextUtils.ToInt(dt.Rows[0]["MainViewID"]);
                    Global.DepartmentID = TextUtils.ToInt(dt.Rows[0]["DepartmentID"]);
                    Global.IsAdmin = TextUtils.ToBoolean(dt.Rows[0]["IsAdmin"]);
                    Global.EmployeeID = TextUtils.ToInt(dt.Rows[0]["EmployeeID"]);
                    Global.HeadOfDepartment = TextUtils.ToInt(dt.Rows[0]["HeadOfDepartment"]);
                    Global.DepartmentName = TextUtils.ToString(dt.Rows[0]["DepartmentName"]);
                    Global.AppCodeName = TextUtils.ToString(dt.Rows[0]["Code"]);
                    //,,,
                    Global.IsRoot = TextUtils.ToBoolean(dt.Rows[0]["IsAdmin"]);
                    Global.IsLeader = TextUtils.ToInt(dt.Rows[0]["IsLeader"]);
                    Global.PositionCode = TextUtils.ToString(dt.Rows[0]["PositionCode"]).Trim();
                    Global.UserTeamID = TextUtils.ToInt(dt.Rows[0]["UserTeamID"]);
                    Global.DepartmentCode = TextUtils.ToString(dt.Rows[0]["DepartmentCode"]);
                    Global.IsAdminSale = TextUtils.ToBoolean(dt.Rows[0]["isAdminSale"]);


                    saveLaslog();
                    SaveSession(dt);
                    loginSuccess = true;
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(this, "Sai tên đăng nhập hoặc mật khẩu, vui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.SelectAll();
                    loginSuccess = false;
                    return;
                }

                this.Dispose();
            }
            catch (Exception ex)
            {
                if (!chkIsOnline.Checked)
                {
                    MessageBox.Show("Vui lòng tích Online và thử Đăng nhập lại!", "Thông báo");
                }
                else
                {
                    MessageBox.Show("Không thể kết nối server!\r\n" + ex.ToString(), "Thông báo");
                }
            }

            #endregion
        }

        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            string key = "";
            key = key + e.KeyCode;
            if (key.Contains("ControlKeyN"))
            {
                key = "";
                //tsSave.Enabled = true;
                //clearForm();
            }
            if (key.Contains("Alt"))
            {
                Application.Exit();
            }
            if (key.Contains("F4") && key.Contains("Alt"))
            {
                Application.Exit();
            }
            if (key.Contains("Escape"))
            {
                Application.Exit();
            }
        }

        private void getLastlog()
        {
            string pLastLog = "";
            string pServerID = "";
            string pZone = "";
            bool pOnline = false;
            //Read from file LastLog.ini
            string strPath = Application.StartupPath.ToString() + @"\LastLog.ini";
            FileStream file = new FileStream(strPath, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader sr = new StreamReader(file);
            int i = 0;
            while (i < 4)
            {
                if (i == 0)
                    pLastLog = sr.ReadLine();
                if (i == 1)
                    pServerID = sr.ReadLine();
                if (i == 2)
                    pImage = sr.ReadLine();
                if (i == 3)
                    pOnline = sr.ReadLine() == "True";
                //pZone = sr.ReadLine();
                //if (i == 4)
                //{

                //}
                i = i + 1;
            }
            sr.Close();
            file.Close();
            txtUserName.Text = pLastLog;
            chkIsOnline.Checked = pOnline;
        }
        private void saveLaslog()
        {
            bool isOK = true;
            string strPath = Application.StartupPath.ToString() + @"\LastLog.ini";
            File.Delete(strPath);
            FileStream file = new FileStream(strPath, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter w = new StreamWriter(file);
            w.Write(txtUserName.Text);
            w.Write("\r\n");
            w.Write("default.ini");
            w.Write("\r\n");
            w.Write("");
            w.Write("\r\n");
            w.Write(chkIsOnline.Checked.ToString());
            w.Close();
            file.Close();
        }


        bool IsOpenFile(FileInfo file)
        {
            try
            {
                FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
                stream.Close();
            }
            catch (Exception ex)
            {
                return true;
            }

            return false;
        }

        private void SaveSession(DataTable dtSession)
        {
            string strPath = Application.StartupPath.ToString() + @"\Session.ini";

            //if (dtSession.Rows.Count <= 0) return;
            //string employeeID = TextUtils.ToString(dtSession.Rows[0]["EmployeeID"]).Trim();
            // Create a file to write to.
            //string createText = "Hello and Welcome" + Environment.NewLine;
            //File.WriteAllText(strPath, employeeID);

            //FileInfo fileInfo = new FileInfo(strPath);
            //bool isOpen = IsOpenFile(fileInfo);
            //if (isOpen)
            //{
            //    var stream = new FileStream(strPath, FileMode., FileAccess.Read);
            //    stream.Close();
            //}

            //File.Delete(strPath);
            FileStream file = new FileStream(strPath, FileMode.Create, FileAccess.Write);
            StreamWriter w = new StreamWriter(file);
            if (dtSession.Rows.Count <= 0) return;
            int employeeID = TextUtils.ToInt(dtSession.Rows[0]["EmployeeID"]);
            w.Write(employeeID);
            w.Close();

            //try
            //{

            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{
            //    file.Close();
            //}
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcessLogIn();
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            btnExit_Click(null, null);
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
