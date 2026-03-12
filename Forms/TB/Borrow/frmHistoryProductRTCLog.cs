using BMS.Model;
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
    public partial class frmHistoryProductRTCLog : _Forms
    {
        public int historyID = 0;
        public frmHistoryProductRTCLog()
        {
            InitializeComponent();
        }

        private void frmHistoryProductRTCLog_Load(object sender, EventArgs e)
        {
            loadData();
        }

        void loadData()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetHistoryProductRTCLog", "A", new string[] { "@HistoryProductRTCID" }, new object[] { historyID });
            grdData.DataSource = dt;
        }
    }
}
