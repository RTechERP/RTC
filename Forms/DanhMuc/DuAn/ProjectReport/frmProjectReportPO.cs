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
    public partial class frmProjectReportPO : _Forms
    {
        public frmProjectReportPO()
        {
            InitializeComponent();
        }


        private void btnLoad_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void frmProjectReportPO_Load(object sender, EventArgs e)
        {
            dtpStartDate.EditValue = DateTime.Now;
            dtpEndDate.EditValue = DateTime.Now;
            dtpStartYear.EditValue = DateTime.Now;
            dtpEndYear.EditValue = DateTime.Now;
            loadData();
            loadDataQuater();
        }
        void loadData()
        {
            DataSet dt = TextUtils.LoadDataSetFromSP("spGetProjectReport", new string[] { "@StartDate", "@EndDate" }, new object[] { dtpStartDate.EditValue, dtpEndDate.EditValue });
            chartProjectReportPO.DataSource = dt.Tables[1];
        }
        void loadDataQuater()
        {
            DataSet dt = TextUtils.LoadDataSetFromSP("spGetProjectReport", new string[] { "@StartDate", "@EndDate" }, new object[] { dtpStartYear.EditValue, dtpEndYear.EditValue });
            chartProjectPOQuarter.DataSource = dt.Tables[3];
        }
        private void btnLoadQuater_Click(object sender, EventArgs e)
        {
            loadDataQuater();
        }
    }
}