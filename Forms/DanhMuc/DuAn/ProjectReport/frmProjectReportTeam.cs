using BMS.Model;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class frmProjectReportTeam : _Forms
    {
        public frmProjectReportTeam()
        {
            InitializeComponent();
        }

        private void frmProjectReportTeam_Load(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            int currentQuarter = (today.Month - 1) / 3 + 1;

            DateTime firstDayOfQuarter = new DateTime(today.Year, (currentQuarter - 1) * 3 + 1, 1);
            DateTime lastDayOfQuarter = firstDayOfQuarter.AddMonths(3).AddDays(-1);

            dtpDateStart.Value = firstDayOfQuarter;
            dtpDateEnd.Value = lastDayOfQuarter;

            LoadTeam();
            LoadData();
        }

        void LoadTeam()
        {
            List<UserTeamModel> list = SQLHelper<UserTeamModel>.FindByAttribute("DepartmentID", 2).ToList();
            cboUserTeam.Properties.ValueMember = "ID";
            cboUserTeam.Properties.DisplayMember = "Name";
            cboUserTeam.Properties.DataSource = list;
        }


        void LoadData()
        {
            DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59);
            string userTeamID = TextUtils.ToString(cboUserTeam.EditValue);


            if (chkAllDay.Checked)
            {
                dateStart = TextUtils.MIN_DATE;
            }

            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectTeamReport", "A",
                                                           new string[] { "@DateStart", "@DateEnd", "@UserTeam" },
                                                           new object[] { dateStart, dateEnd, userTeamID });

            if (chartProjectReport.Series.Count > 0) chartProjectReport.Series.Clear();

            Series seriesAll = new Series();
            seriesAll.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            seriesAll.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            seriesAll.Name = $"Chọn tất cả";
            seriesAll.View = new DevExpress.XtraCharts.StackedBarSeriesView();
            seriesAll.View.Color = Color.Gray;

            chartProjectReport.Series.Add(seriesAll);

            string[] colors = new string[] {"#008FFB", "#00E396", "#FEB019", "#FF4560", "#775DD0",
                                                    "#3F51B5", "#03A9F4", "#4CAF50", "#F9CE1D", "#FF9800",
                                                    "#33B2DF", "#546E7A", "#D4526E", "#13D8AA", "#A5978B",
                                                    "#4ECDC4", "#C7F464", "#81D4FA", "#546E7A", "#FD6A6A",
                                                    "#2B908F", "#F9A3A4", "#90EE7E", "#FA4443", "#69D2E7",
                                                    "#449DD1", "#F86624", "#EA3546", "#662E9B", "#C5D86D",
                                                    "#D7263D", "#1B998B", "#2E294E", "#F46036", "#E2C044",
                                                    "#662E9B", "#F86624", "#F9C80E", "#EA3546", "#43BCCD",
                                                    "#5C4742", "#A5978B", "#8D5B4C", "#5A2A27", "#C4BBAF",
                                                    "#A300D6", "#7D02EB", "#5653FE", "#2983FF", "#00B1F2"};
            List<ProjectStatusModel> listStatus = SQLHelper<ProjectStatusModel>.FindAll().OrderBy(x => x.STT).ToList();
            //foreach (ProjectStatusModel item in listStatus)
            //{
                
            //}

            for (int i = 0; i < listStatus.Count; i++)
            {
                ProjectStatusModel item = listStatus[i];
                Series series = new Series();
                series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                series.ArgumentDataMember = $"Name";
                series.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                series.Name = $"{item.StatusName}";
                series.LegendTextPattern = "{V}";
                series.View = new DevExpress.XtraCharts.StackedBarSeriesView();
                series.ValueDataMembers.AddRange(new string[] { $"D{item.ID}" });
                //series.View.Color = ColorTranslator.FromHtml(colors[i]);
                chartProjectReport.Series.Add(series);
            }


            chartProjectReport.DataSource = dt;
            chartProjectReportPO.DataSource = dt;


            grdData.DataSource = null;
            grdDataPO.DataSource = null;
        }


        void LoadDataProject(int x, int y, ChartControl chartControl, GridControl gridControl, GridView gridView)
        {
            DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, 1, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, 1, 0, 0, 0).AddMonths(+1).AddSeconds(-1);

            if (chkAllDay.Checked)
            {
                dateStart = TextUtils.MIN_DATE;
            }

            ChartHitInfo hit = chartControl.CalcHitInfo(x, y);
            SeriesPoint seriesPoint = hit.SeriesPoint;
            Series series = (Series)hit.Series;
            if (seriesPoint != null)
            {

                DataRowView dataRowView = (DataRowView)seriesPoint.Tag;
                //DataRow dataRow = dataRowView.Row;


                int userTeamID = TextUtils.ToInt(dataRowView["ID"]);

                string valueArgument = series.ValueDataMembers.Count > 0 ? series.ValueDataMembers[0] : "";
                int projectStatus = string.IsNullOrWhiteSpace(valueArgument) ? 0 : TextUtils.ToInt(valueArgument.Replace("D", ""));

                //MessageBox.Show($"Bạn chọn xem dự án trạng thái [{projectStatus}.{series.Name}] của team [{userTeamID}.{seriesPoint.Argument}].", "Thông báo");


                DataTable dt = TextUtils.LoadDataFromSP("spGetProjectByTeamAndStatus", "A",
                                                        new string[] { "@DateStart", "@DateEnd", "@UserTeamID", "@ProjectStatus" },
                                                        new object[] { dateStart, dateEnd, userTeamID, projectStatus });
                gridControl.DataSource = dt;
                gridView.FocusedRowHandle = 0;
            }
        }

        private void cboProjectType_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void chkAllDay_CheckedChanged(object sender, EventArgs e)
        {
            dtpDateStart.Enabled = dtpDateEnd.Enabled = !chkAllDay.Checked;
            LoadData();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            chkAllDay.Checked = e.Page == xtraTabPage2;
        }

        private void chartProjectReport_MouseClick(object sender, MouseEventArgs e)
        {
            LoadDataProject(e.X, e.Y, chartProjectReport, grdData, grvData);
        }

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            var view = sender as GridView;
            if (view.FocusedRowHandle == e.RowHandle)
            {
                e.Appearance.BackColor = Color.LightYellow;
                e.HighPriority = true;
            }
        }

        private void chartProjectReportPO_MouseClick(object sender, MouseEventArgs e)
        {
            LoadDataProject(e.X, e.Y, chartProjectReportPO, grdDataPO, grvDataPO);
        }

        private void grvDataPO_RowStyle(object sender, RowStyleEventArgs e)
        {
            var view = sender as GridView;
            if (view.FocusedRowHandle == e.RowHandle)
            {
                e.Appearance.BackColor = Color.LightYellow;
                e.HighPriority = true;
            }
        }

        bool initializationFlag = false;
        private void chartProjectReport_LegendItemChecked(object sender, LegendItemCheckedEventArgs e)
        {
            //if (initializationFlag == true)
            //    return;
            //initializationFlag = true;
            //{
            //    Series checkedSeries = e.CheckedElement as Series;
            //    if (checkedSeries == null)
            //        throw new Exception("Expected series only");
            //    foreach (Series series in chartProjectReport.Series)
            //        series.CheckedInLegend = false;
            //    checkedSeries.CheckedInLegend = true;
            //    chartProjectReport.Titles[0].Text = checkedSeries.Name;
            //}
            //initializationFlag = false;



            Series checkedSeries = e.CheckedElement as Series;
            if (checkedSeries.Name != "Chọn tất cả") return;
            foreach (Series series in chartProjectReport.Series)
            {
                series.CheckedInLegend = checkedSeries.CheckedInLegend;
            }
        }
    }
}
