using DevExpress.XtraEditors;
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
    public partial class frmTSAssetManagementHistoryLog : _Forms
    {
        public int historyID;
        public frmTSAssetManagementHistoryLog()
        {
            InitializeComponent();
        }

        private void frmTSAssetManagementHistoryLog_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        void LoadData()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetTSAssetManagementHistoryLog", "A", new string[] { "@TSAssetManagementHistoryID" }, new object[] { historyID });
            grdData.DataSource = dt;
        }
    }
}