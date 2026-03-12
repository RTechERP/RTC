using BMS.Model;
using DevExpress.XtraCharts;
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
    public partial class frmProjectTypeReport : _Forms
    {
        public frmProjectTypeReport()
        {
            InitializeComponent();
        }

        private void frmProjectTypeReport_Load(object sender, EventArgs e)
        {
            dtpDateStart.Value = DateTime.Now;
            dtpDateEnd.Value = DateTime.Now;
            loadData();
        }
        void loadData()
        {
            DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, 1, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, 1, 0, 0, 0).AddMonths(+1).AddSeconds(-1);

            DataSet dt = TextUtils.LoadDataSetFromSP("spGetProjectTypeReport",
                                new string[] { "@StartDate", "@EndDate" },
                                new object[] { dateStart, dateEnd });
            chartProjectTypeReport.Series[0].DataSource = dt.Tables[0];
            chartProjectTypeReport.Series[1].DataSource = dt.Tables[1];
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void chartProjectTypeReport_MouseClick(object sender, MouseEventArgs e)
        {
            int typeID = 0;
            string typeName = "";
            int status = 0;
            string seriesName = "";

            DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, 1, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, 1, 0, 0, 0).AddMonths(+1).AddSeconds(-1);

            ChartHitInfo hit = chartProjectTypeReport.CalcHitInfo(e.X, e.Y);
            SeriesPoint seriesPoint = hit.SeriesPoint;
            Series series = (Series)hit.Series;
            if (seriesPoint != null)
            {

                DataRowView dataRowView = (DataRowView)seriesPoint.Tag;
                DataRow dataRow = dataRowView.Row;

                status = series.Name == "Đã PO" ? 1 : 0;

                typeID = TextUtils.ToInt(dataRow["ID"]);
                typeName = TextUtils.ToString(dataRow["ProjectTypeName"]).ToUpper();
                seriesName = series.Name.ToUpper();
            }


            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectByType", "A",
                                                new string[] { "@ProjectTypeID", "@DateStart", "@DateEnd", "@Status" },
                                                new object[] { typeID, dateStart, dateEnd, status });
            grdData.DataSource = dt;
            groupControl1.Text = typeName + " - " + seriesName;
        }
    }
}