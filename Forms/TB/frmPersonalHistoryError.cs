using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmHistoryError : Form
    {
        int Id;
        public frmHistoryError(int Id)
        {
            this.Id = Id;
            InitializeComponent();
        }

        void loaddata()
        {
            DataTable dt = new DataTable();
            dt = TextUtils.LoadDataFromSP("spGetHistoryErrorByID", "A", new string[] {@"UserID"}, new object[] { this.Id });
            grdData.DataSource = dt;
        }
        private void frmHistoryError_Load(object sender, EventArgs e)
        {
            if (Global.UserID ==Id)
            {
                lblListError.Text = "Danh Sách Lỗi Nhân Viên: " + Global.AppFullName;
                loaddata();
            }
            else
            {
                lblListError.Text = "Danh Sách Lỗi Tất Cả Nhân Viên";
                loaddata();
            }
        }
    }
}
