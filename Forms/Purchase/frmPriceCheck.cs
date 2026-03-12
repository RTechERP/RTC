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
    public partial class frmPriceCheck : _Forms
    {
        public frmPriceCheck()
        {
            InitializeComponent();
        }

        private void frmPriceCheck_Load(object sender, EventArgs e)
        {
            DateTime datenow = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, dtpStartDate.Value.Day, 0, 0, 0);
            dtpStartDate.Value = datenow.AddMonths(-1);
            txtPageNumber.Text = "1";
            loadGrvData();
        }
        void loadGrvData()
        {
            DateTime dateTimeS = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, dtpStartDate.Value.Day, 0, 0, 0);
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);

            DataTable dt = TextUtils.LoadDataFromSP("spGetPriceCheck", "A", new string[] { "@DateStart", "@DateEnd", "@PageNumber", "@PageSize", "@FilterText" }
            , new object[] {TextUtils.ToDate2(dateTimeS), TextUtils.ToDate2(dateTimeE), TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value),TextUtils.ToString(txtFilterText.Text) });
            grdData.DataSource = dt;
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            frmPriceCheckDetail frm = new frmPriceCheckDetail();
            if(frm.ShowDialog()==DialogResult.OK)
            {

            }    
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            loadGrvData();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadGrvData();
        }
    }
}
