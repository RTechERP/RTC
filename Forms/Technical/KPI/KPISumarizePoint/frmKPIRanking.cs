using BMS;
using BMS.Business;
using BMS.Model;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraRichEdit.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Charts.Model;
using DevExpress.Utils;

namespace BMS
{
    public partial class frmKPIRanking : _Forms
    {
        public DataTable dtData = new DataTable();
        public DataTable dtSummary = new DataTable();
        public int year;
        public int quarter;
        public int departmentID;
        public frmKPIRanking()
        {
            InitializeComponent();
            this.AcceptButton = btnLoad;
        }
        void loadDepartment()
        {
            List<DepartmentModel> list = SQLHelper<DepartmentModel>.FindAll().ToList();
            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.DataSource = list;
            cboDepartment.EditValue = 2;
        }

        public void loadData()
        {
            //ndnhat 14/04/2025
            string[] kpiOrder = new string[] { "A+", "A", "A-", "B+", "B", "B-", "C+", "C", "C-", "D" };

            if (dtSummary.Rows.Count > 0)
            {
                grdData.DataSource = null;
                chartKPIRanking.DataSource = null;

                chartKPIRanking.DataSource = dtSummary;
                txtYear.Value = year;
                txtQuarter.Value = quarter;
                cboDepartment.EditValue = departmentID;
            }
            else
            {
                DataSet ds = TextUtils.LoadDataSetFromSP("spGetKPIRanking",
                    new string[] { "@Year", "@Quarter", "@DepartmentID", "@KpiLevel" },
                    new object[] {
                TextUtils.ToInt(txtYear.Value),
                TextUtils.ToInt(txtQuarter.Value),
                TextUtils.ToInt(cboDepartment.EditValue),
                null
                    });

                DataTable dt = ds.Tables[0];
                //chartKPIRanking.Series.Clear();

                var summary = dt.AsEnumerable()
                    .GroupBy(r => r.Field<string>("KPILevel"))
                    .Select(g => new
                    {
                        KPILevel = g.Key,
                        SoLuongExpected = g.Sum(r => r.Field<int>("SoLuongExpected")),
                        SoLuongActual = g.Sum(r => r.Field<int>("SoLuongActual")),
                        SortOrder = Array.IndexOf(kpiOrder, g.Key) // gắn thứ tự
                    })
                    .OrderBy(x => x.SortOrder) // Sắp xếp đúng thứ tự
                    .ToList();

                chartKPIRanking.DataSource = summary;
            }
        }


        private void frmKPIRanking_Load(object sender, EventArgs e)
        {
            txtYear.Value = DateTime.Now.Year;
            txtYear.Minimum = DateTime.MinValue.Year;
            txtQuarter.Value = (DateTime.Now.Month - 1) / 3 + 1;

            loadDepartment();
            loadData();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", ""))
            {

                dtSummary.Clear();
                dtData.Clear();
                loadData();
            }
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }
        private DataTable GetDetailByKpiLevel(DataTable tempKPI, string kpiLevel, bool isActual)
        {
            DataTable dtDetail = new DataTable();
            dtDetail.Columns.Add("FullName", typeof(string));
            dtDetail.Columns.Add("Code", typeof(string));
            dtDetail.Columns.Add(isActual ? "TotalPercentActual" : "TotalPercent", typeof(decimal));
            dtDetail.Columns.Add("DepartmentName", typeof(string));

            string filterCol = isActual ? "TotalPercentActualText" : "TotalPercentText";

            DataRow[] rows = tempKPI.Select($"{filterCol} = '{kpiLevel}'");

            foreach (var r in rows)
            {
                DataRow newRow = dtDetail.NewRow();
                newRow["FullName"] = r["FullName"];
                newRow["Code"] = r["Code"];
                newRow[isActual ? "TotalPercentActual" : "TotalPercent"] = r[isActual ? "TotalPercentActual" : "TotalPercent"];
                newRow["DepartmentName"] = r["DepartmentName"];
                dtDetail.Rows.Add(newRow);
            }

            return dtDetail;
        }

        void LoadDataProject(int x, int y, ChartControl chartControl, GridControl gridControl, GridView gridView)
        {

            ChartHitInfo hit = chartControl.CalcHitInfo(x, y);
            SeriesPoint seriesPoint = hit.SeriesPoint;
            Series series = hit.Series as Series;
            if (seriesPoint != null)
            {
                string level = seriesPoint.Argument;
                string name = series.Name;
                string tag = TextUtils.ToString(series.Tag);

                if (tag == "TotalPoinKPI")
                {
                    //ndnhat 14/04/2025
                    if (dtSummary.Rows.Count > 0)
                    {
                        DataTable dt = GetDetailByKpiLevel(dtData, level, false);
                        gridControl.DataSource = dt;
                        gridView.FocusedRowHandle = 0;
                        gridView.Columns["TotalPercent"].Visible = true;
                        gridView.Columns["TotalPercentActual"].Visible = false;
                    }
                    else
                    {
                        DataSet dt = TextUtils.LoadDataSetFromSP("spGetKPIRanking",
                        new string[] { "@Year", "@Quarter", "@DepartmentID", "@KpiLevel" },
                        new object[] { TextUtils.ToInt(txtYear.Value), TextUtils.ToInt(txtQuarter.Value), TextUtils.ToInt(cboDepartment.EditValue), level });
                        gridControl.DataSource = dt.Tables[2];
                        gridView.FocusedRowHandle = 0;
                        gridView.Columns["TotalPercent"].Visible = true;
                        gridView.Columns["TotalPercentActual"].Visible = false;

                    }

                }
                else if (tag == "TotalPoinKPILast")
                {
                    //ndnhat 14/04/2025
                    if (dtSummary.Rows.Count > 0)
                    {
                        DataTable dt = GetDetailByKpiLevel(dtData, level, true);
                        gridControl.DataSource = dt;
                        gridView.FocusedRowHandle = 0;
                        gridView.Columns["TotalPercent"].Visible = false;
                        gridView.Columns["TotalPercentActual"].Visible = true;
                    }
                    else
                    {
                        DataSet dt = TextUtils.LoadDataSetFromSP("spGetKPIRanking",
                    new string[] { "@Year", "@Quarter", "@DepartmentID", "@KpiLevel" },
                    new object[] { TextUtils.ToInt(txtYear.Value), TextUtils.ToInt(txtQuarter.Value), TextUtils.ToInt(cboDepartment.EditValue), level });
                        gridControl.DataSource = dt.Tables[1];
                        gridView.FocusedRowHandle = 0;
                        gridView.Columns["TotalPercent"].Visible = false;
                        gridView.Columns["TotalPercentActual"].Visible = true;
                    }

                }


            }
        }
        private void chartKPIRanking_MouseClick(object sender, MouseEventArgs e)
        {
            LoadDataProject(e.X, e.Y, chartKPIRanking, grdData, grvData);
        }

        private void chartKPIRanking_ObjectSelected(object sender, HotTrackEventArgs e)
        {

        }

        private void Radio_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton selectedRadio = sender as RadioButton;

            if (selectedRadio != null && selectedRadio.Checked)
            {
                foreach (Series series in chartKPIRanking.Series)
                {
                    series.Visible = (series.Tag == selectedRadio.Tag);
                }
            }
        }
    }
}
