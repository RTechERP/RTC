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
    public partial class frmProjectIsBehindSchedule : _Forms
    {
        public frmProjectIsBehindSchedule()
        {
            InitializeComponent();
        }

        private void frmProjectIsBehindSchedule_Load(object sender, EventArgs e)
        {
            dtpStartDate.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpEndDate.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
            loadData();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            loadData();
        }

        void loadData()
        {
            grdData.DataSource = TextUtils.LoadDataFromSP("spGetProjectIsBehindSchedule", "A", new string[] { "@DateStart", "@DateEnd", "@FilterText" }, new object[] { dtpStartDate.EditValue, dtpEndDate.EditValue, txtFind.Text.Trim() });
        }
    }
}