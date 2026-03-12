using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmSummaryDailyReportProjectItem : _Forms
    {
        //DataSet dataSet = new DataSet();
        public frmSummaryDailyReportProjectItem()
        {
            InitializeComponent();
        }

        private void frmSummaryDailyReportProjectItem_Load(object sender, EventArgs e)
        {
            dtpDateStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            dtpDateEnd.Value = dtpDateStart.Value.AddMonths(+1).AddSeconds(-1);
            LoadUser();
        }

        void LoadUser()
        {
            DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59);

            //using (WaitDialogForm fWait = new WaitDialogForm())
            //{
            DataTable dt = TextUtils.LoadDataFromSP("spGetSummaryDailyReportProjectItem", "A",
                                                        new string[] { "@DateStart", "@DateEnd", "@UserReport" },
                                                        new object[] { dateStart, dateEnd, 0 });

            grdData.DataSource = dt;
            //}

        }

        void LoadProject()
        {
            //using (WaitDialogForm fWait = new WaitDialogForm())
            //{

            DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59);
            int userReport = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colUserReport));
            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetSummaryDailyReportProjectItem",
                                                             new string[] { "@DateStart", "@DateEnd", "@UserReport" },
                                                             new object[] { dateStart, dateEnd, userReport });

            grdProject.DataSource = dataSet.Tables[1];
            //}
        }

        void LoadDailyReport()
        {
            //using (WaitDialogForm fWait = new WaitDialogForm())
            //{

            DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59);
            int userReport = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colUserReport));
            int projectId = TextUtils.ToInt(grvProject.GetFocusedRowCellValue(colProjectID));

            var exp1 = new Expression("UserReport", userReport);
            var exp2 = new Expression("DateReport", dateStart.ToString("yyyy-MM-dd HH:mm:ss"), ">=");
            var exp3 = new Expression("DateReport", dateEnd.ToString("yyyy-MM-dd HH:mm:ss"), "<=");
            var exp4 = new Expression("ProjectID", projectId);
            List<DailyReportTechnicalModel> list = SQLHelper<DailyReportTechnicalModel>.FindByExpression(exp1.And(exp2.And(exp3)).And(exp4));
            //DataTable dt = TextUtils.Select($"SELECT * FROM DailyReportTechnical WHERE UserReport = {userReport} AND (DateReport BETWEEN '{dateStart.ToString("yyyy-MM-dd HH:ss:mm")}' AND '{dateEnd.ToString("yyyy-MM-dd HH:ss:mm")}') AND ProjectID = {projectId}");


            grdReport.DataSource = list;
            //}
        }

        void LoadProjectItem()
        {
            //using (WaitDialogForm fWait = new WaitDialogForm())
            //{

            DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59);
            int userReport = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colUserReport));
            int projectId = TextUtils.ToInt(grvProject.GetFocusedRowCellValue(colProjectID));

            var exp1 = new Expression("UserID", userReport);
            var exp2 = new Expression("ProjectID", projectId);
            List<ProjectItemModel> list = SQLHelper<ProjectItemModel>.FindByExpression(exp1.And(exp2));

            //DataTable dt = TextUtils.Select($"SELECT * FROM dbo.ProjectItem WHERE UserID = {userReport} AND ProjectID = {projectId}");

            grdProjectItem.DataSource = list;
            //}
        }


        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadProject();
            //LoadDailyReport();
            //LoadProjectItem();
        }

        private void grvProject_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDailyReport();
            LoadProjectItem();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadUser();
            LoadProject();
            LoadDailyReport();
            LoadProjectItem();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            grvData.OptionsPrint.AutoWidth = false;
            grvProject.OptionsPrint.AutoWidth = false;
            grvReport.OptionsPrint.AutoWidth = false;
            grvProjectItem.OptionsPrint.AutoWidth = false;


            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = Path.Combine(f.SelectedPath, $"KhongLapKeHoach_{dtpDateStart.Value.ToString("ddMMyy")}_{dtpDateEnd.Value.ToString("ddMMyy")}.xlsx");
                //string filepath = @"C:\Users\Admin\Desktop\Bảng công Công ty RTC - APR - MVI - YONKO FINAL Tháng 8.2023 FINAL.xlsx";

                XlsxExportOptions optionsEx = new XlsxExportOptions ();
                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdData;

                PrintableComponentLink printableComponentLink2 = new PrintableComponentLink(printingSystem);
                printableComponentLink2.Component = grdProject;

                PrintableComponentLink printableComponentLink3 = new PrintableComponentLink(printingSystem);
                printableComponentLink3.Component = grdReport;

                PrintableComponentLink printableComponentLink4 = new PrintableComponentLink(printingSystem);
                printableComponentLink4.Component = grdProjectItem;

                try
                {
                    CompositeLink compositeLink = new CompositeLink(printingSystem);
                    compositeLink.Links.Add(printableComponentLink1);
                    compositeLink.Links.Add(printableComponentLink2);
                    //compositeLink.Links.Add(printableComponentLink3);
                    //compositeLink.Links.Add(printableComponentLink4);

                    //compositeLink.PrintingSystem.XlSheetCreated += PrintingSystem_XlSheetCreated;

                    compositeLink.CreatePageForEachLink();
                    optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;

                    compositeLink.PrintingSystem.SaveDocument(filepath);
                    compositeLink.ExportToXlsx(filepath, optionsEx);
                    Process.Start(filepath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn));
                if (string.IsNullOrEmpty(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void grvProject_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(grvProject.GetFocusedRowCellValue(grvProject.FocusedColumn));
                if (string.IsNullOrEmpty(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void grvReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(grvReport.GetFocusedRowCellValue(grvReport.FocusedColumn));
                if (string.IsNullOrEmpty(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void grvProjectItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(grvProjectItem.GetFocusedRowCellValue(grvProjectItem.FocusedColumn));
                if (string.IsNullOrEmpty(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }
    }
}
