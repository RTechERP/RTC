using BMS.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmMain_QR : _Forms
    {
        bool _logOut = false;
        int warehouseID;
        public frmMain_QR()
        {
            //Global.DefaultFileName = "default.ini";
            Global.ComputerName = TextUtils.GetHostName();
            //check update
            DocUtils.InitFTPQLSX();

            //Check update version
            //updateVersion();


            frmLoginUseByQRCode frm = new frmLoginUseByQRCode();
            frm.ShowDialog();
            if (frm.loginSuccess == false)
            {
                Application.Exit();
                return;
            }
            InitializeComponent();
        }
        public frmMain_QR(int WarehouseID)
        {
            //Global.DefaultFileName = "default.ini";
            Global.ComputerName = TextUtils.GetHostName();
            //check update
            DocUtils.InitFTPQLSX();

            //Check update version
            //updateVersion();


            frmLoginUseByQRCode frm = new frmLoginUseByQRCode();
            frm.ShowDialog();
            if (frm.loginSuccess == false)
            {
                Application.Exit();
                return;
            }
            InitializeComponent();
            warehouseID = WarehouseID;
        }
        private void frmMain_QR_Load(object sender, EventArgs e)
        {
            //string s = TextUtils.ToString(TextUtils.ExcuteScalar("Select PasswordHash from[dbo].[Users] where ID = 78"));
            //textBox1.Text = MD5.DecryptPassword(s);

            lblUser.Text += Global.AppFullName;
            this.Text += warehouseID == 1 ? " - HN" : (warehouseID == 2 ? " - HCM" : " - BN");
        }
        private void btnBorrow_Click(object sender, EventArgs e)
        {
            frmProductHistoryBorrowDetailNew frm = new frmProductHistoryBorrowDetailNew(warehouseID, new List<string>());
            
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.isLogout)
                {
                    btnLogout_Click(null, null);
                }
                
            }

        }
        void ColorExchange(Label label)
        {
            //label.
        }
        private void btnReturn_Click(object sender, EventArgs e)
        {
            frmProductHistoryReturnDetail frm = new frmProductHistoryReturnDetail(warehouseID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.isLogout)
                {
                    btnLogout_Click(null, null);
                }

            }
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            _logOut = true;
            this.Close();

        }
        private void btnBorrow_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void btnReturn_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void frmMain_QR_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_logOut)
            {
                string path = Application.StartupPath + "\\RTC.exe";
                System.Diagnostics.Process.Start(path);
            }
        }

        private void frmMain_QR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            frmProductHistory frm = new frmProductHistory();
            frm.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //frmAddQRCode frm = new frmAddQRCode();
            //frm.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            frmProductRTC frm = new frmProductRTC(1);
            frm.Show();
        }
    }
}
