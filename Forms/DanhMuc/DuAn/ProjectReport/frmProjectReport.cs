using BMS.Model;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmProjectReport : _Forms
    {   
        public frmProjectReport()
        {
            InitializeComponent();
        }

        private void frmProjectReport_Load(object sender, EventArgs e)
        {

            //dtpStartDate.EditValue = DateTime.Now;
            //dtpEndDate.EditValue = DateTime.Now;
            dtpDateStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month,1);
            dtpDateEnd.Value = dtpDateStart.Value.AddMonths(+1).AddDays(-1);
            loadProjectType();
            loadData();
            //loadDataQuater();
        }

        void loadData()
        {
            string projectType = TextUtils.ToString(cboProjectType.EditValue);

            DataSet dt = TextUtils.LoadDataSetFromSP("spGetProjectReport",
                                new string[] { "@StartDate", "@EndDate", "@ProjectType" },
                                new object[] { dtpDateStart.Value, dtpDateEnd.Value, projectType });
            chartProjectReport.Series[0].DataSource = dt.Tables[0];
            chartProjectReport.Series[1].DataSource = dt.Tables[1];

            chartProjectReportQuater.Series[0].DataSource = dt.Tables[2];
            chartProjectReportQuater.Series[1].DataSource = dt.Tables[3];
        }

        //void loadDataQuater()
        //{
        //    string projectType = TextUtils.ToString(cboProjectType.EditValue);
        //    DataSet dt = TextUtils.LoadDataSetFromSP("spGetProjectReport",
        //                        new string[] { "@StartDate", "@EndDate", "@ProjectType" },
        //                        new object[] { dtpStartYear.EditValue, dtpEndYear.EditValue, projectType });

        //}

        void loadProjectType()
        {
            List<ProjectTypeModel> listType = SQLHelper<ProjectTypeModel>.FindAll();
            cboProjectType.Properties.ValueMember = "ID";
            cboProjectType.Properties.DisplayMember = "ProjectTypeName";
            cboProjectType.Properties.DataSource = listType;
            cboProjectType.CheckAll();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnLoadQuater_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void cboProjectType_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }
    }
}