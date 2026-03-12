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
    public partial class frmLoginUseByQRCode : Form
    {
        public bool loginSuccess = false;
        public string loginname = "";
        public string pass = "";
        string pImage = "";
        public frmLoginUseByQRCode()
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
            //Global.DefaultFileName = "default.ini";
            //Check sử dụng wifi công ty không
            //Ping ping = new Ping();
            //PingReply reply = ping.Send("192.168.1.2");
            //Global.DefaultFileName = reply.Status == IPStatus.Success ? "default.ini" : "defaultonline.ini";

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
            string _user = "";
            string _pass = "";

            if (txtUserName.Text.Contains(";") || txtPassword.Text.Contains(";"))
            {
                if (txtUserName.Text.Contains(";"))
                {
                    _user = txtUserName.Text.Trim().Split(';')[0];
                    _pass = BMS.Utils.MD5.EncryptPassword(txtUserName.Text.Trim().Split(';')[1]);
                }
                else
                {
                    _user = txtPassword.Text.Trim().Split(';')[0];
                    _pass = BMS.Utils.MD5.EncryptPassword(txtPassword.Text.Trim().Split(';')[1]);
                }
            }
            else
            {
                _user = txtUserName.Text.Trim();
                _pass = BMS.Utils.MD5.EncryptPassword(txtPassword.Text);
            }
            #endregion
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
                //,,,
                Global.IsRoot = TextUtils.ToBoolean(dt.Rows[0]["IsAdmin"]);

                saveLaslog();
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
            //if (TextUtils.Log(_user, _pass, ref userModel) == true)
            //{
            //    Global.UserID = userModel.ID;
            //    Global.LoginName = userModel.LoginName;
            //    Global.AppUserName = userModel.LoginName;
            //    Global.AppFullName = userModel.FullName;
            //    Global.AppPassword = _pass;
            //    Global.MainViewID = userModel.MainViewID;
            //    Global.DepartmentID = userModel.DepartmentID;
            //    Global.IsAdmin = userModel.IsAdmin;
            //    //,,,
            //    Global.IsRoot = userModel.IsAdmin;

            //    #region Create Session
            //    //Global.IsNotCreateSession = true;
            //    //Expression exp = new Expression("UserID", Global.UserID, "=");
            //    //exp = exp.And(new Expression("Status", 0, "="));
            //    //ArrayList arr = SessionBO.Instance.FindByExpression(exp);
            //    //if (arr.Count > 0)
            //    //{
            //    //    for (int i = 0; i < arr.Count; i++)
            //    //    {
            //    //        SessionModel _modelSession = (SessionModel)arr[i];
            //    //        _modelSession.Status = 2;
            //    //        _modelSession.EndTime = TextUtils.GetSystemDate();

            //    //        SessionBO.Instance.Update(_modelSession);
            //    //    }
            //    //}
            //    //SessionModel _sessionModel = new SessionModel();
            //    //_sessionModel.ComputerName = Global.ComputerName;
            //    //_sessionModel.UserID = Global.UserID;
            //    //_sessionModel.StartTime = TextUtils.GetSystemDate();
            //    //_sessionModel.EndTime = _sessionModel.StartTime;
            //    //_sessionModel.Status = 0;
            //    //Global.SessionID = Convert.ToInt32(SessionBO.Instance.Insert(_sessionModel));
            //    #endregion

            //    saveLaslog();

            //    //string serial = TextUtils.GetSerialNumberDiskDrive();
            //    //if (serial.ToUpper() == "WD-WMAYUA786372")
            //    //{
            //    //    loginSuccess = Global.LoginName == "SX";
            //    //}
            //    //else
            //    //{
            //    //    loginSuccess = Global.LoginName != "SX";
            //    //    //loginSuccess = true;  
            //    //}

            //    loginSuccess = true;
            //    this.DialogResult = DialogResult.OK;
            //}
            //else
            //{
            //    MessageBox.Show(this, "Sai tên đăng nhập hoặc mật khẩu, vui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtPassword.SelectAll();
            //    loginSuccess = false;
            //    return;
            //}
            this.Dispose();

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
                    pZone = sr.ReadLine();
                i = i + 1;
            }
            sr.Close();
            file.Close();
            txtUserName.Text = pLastLog;
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
            w.Write(pImage);
            w.Close();
            file.Close();
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
