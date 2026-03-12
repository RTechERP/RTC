using DevExpress.Spreadsheet;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Forms.Classes.cGlobVar;

namespace BMS
{
    public partial class frmDailyReportHR : _Forms
    {
        public frmDailyReportHR()
        {
            InitializeComponent();
        }

        private void frmDailyReportHR_Load(object sender, EventArgs e)
        {
            dtpDateStart.Value = DateTime.Now.AddDays(-1);
            LoadCboUser();
            LoadData();
        }

        void LoadData()
        {
            int userID = TextUtils.ToInt(cboUser.EditValue);
            int employeeID = 0;
            var rowData = (DataRowView)cboUser.GetSelectedDataRow();
            if (rowData != null)
            {
                employeeID = TextUtils.ToInt(rowData["ID"]);
            }

            //string positionID = xtraTabControl1.SelectedTabPageIndex == 1 ? "7;72" : (xtraTabControl1.SelectedTabPageIndex == 2 ? "6" : "");

            DateTime ds = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day).AddSeconds(-1);
            DateTime de = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59);

            List<DailyReportHCNSIT> list = SQLHelper<DailyReportHCNSIT>.ProcedureToList("spGetDailyReportTechnical",
                                                                            new string[] { "@DateStart", "@DateEnd", "@UserID", "@Keyword", "@DepartmentID" },
                                                                            new object[] { ds, de, userID, txtKeyword.Text.Trim(), 6 });

            //DataTable dt = TextUtils.LoadDataFromSP("spGetDailyReportHR", "A",
            //                                new string[] { "@DateStart", "@DateEnd", "@Keyword", "@EmployeeID", "@PositionID" },
            //                                new object[] { ds, de, txtKeyword.Text.Trim(), employeeID, positionID });
            
            List<DailyReportHR> listDataHR = SQLHelper<DailyReportHR>.ProcedureToList("spGetDailyReportHR",
                                            new string[] { "@DateStart", "@DateEnd", "@Keyword", "@EmployeeID"},
                                            new object[] { ds, de, txtKeyword.Text.Trim(), employeeID});


            var dataFilm = listDataHR.Where(x => x.ChucVuHDID == 7 || x.ChucVuHDID == 72).ToList();
            var dataDriver = listDataHR.Where(x => x.ChucVuHDID == 6).ToList();

            grdData.DataSource = list;
            grdDataFilm.DataSource = dataFilm;
            grdDataDriver.DataSource = dataDriver;
        }

        void LoadCboUser()
        {
            DataTable dt = TextUtils.GetDataTableFromSP("spGetUserProjectItem", new string[] { "@DepartmentID" }, new object[] { 6 });

            cboUser.Properties.DisplayMember = "FullName";
            cboUser.Properties.ValueMember = "UserID";
            cboUser.Properties.DataSource = dt;
        }

        

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboUser_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void ExportGridToSheet(GridView gridView, Microsoft.Office.Interop.Excel.Workbook workbook, string sheetName)
        {
            Microsoft.Office.Interop.Excel.Worksheet worksheet = workbook.Sheets.Add();
            worksheet.Name = sheetName;

            // Export visible columns
            int columnIndex = 0;
            foreach (GridColumn column in gridView.VisibleColumns)
            {
                worksheet.Cells[1, columnIndex + 1] = column.Caption;
                columnIndex++;
            }

            // Apply border style
            Microsoft.Office.Interop.Excel.Range headerRange = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, columnIndex]];
            headerRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            headerRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            headerRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            headerRange.Font.Bold = true;

            // Export row data
            for (int rowIndex = 0; rowIndex < gridView.RowCount; rowIndex++)
            {
                int visibleColumnIndex = 0;
                foreach (GridColumn column in gridView.VisibleColumns)
                {
                    object cellValue = gridView.GetRowCellValue(rowIndex, column);
                    worksheet.Cells[rowIndex + 2, visibleColumnIndex + 1] = cellValue;
                    visibleColumnIndex++;
                }
            }

            Microsoft.Office.Interop.Excel.Range dataRange = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[gridView.RowCount + 1, gridView.Columns.Count]];
            dataRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            dataRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            // Auto-fit columns
            worksheet.UsedRange.Columns.AutoFit();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string date = "";
            if (dtpDateStart.Value == dtpDateEnd.Value)
            {
                date = dtpDateStart.Value.ToString("ddMMyy");
            }
            else
            {
                date = $"{dtpDateStart.Value.ToString("ddMMyy")}_{dtpDateEnd.Value.ToString("ddMMyy")}";
            }

            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook workbook = excelApp.Workbooks.Add();

                    ExportGridToSheet(grvData, workbook, "HCNS-IT");
                    ExportGridToSheet(grvDataFilm, workbook, "CẮT PHIM");
                    ExportGridToSheet(grvDataDriver, workbook, "NÁI XE");


                    //string filePath = sfd.FileName;
                    string filepath = $"{f.SelectedPath}/BaoCaoCongViec_{date}.xlsx";
                    workbook.SaveAs(filepath);
                    workbook.Close();
                    excelApp.Quit();

                    Marshal.ReleaseComObject(workbook);
                    Marshal.ReleaseComObject(excelApp);

                    Process.Start(filepath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                //XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
                //XlsExportOptionsEx optionsExFilm = new XlsExportOptionsEx();
                //XlsExportOptionsEx optionsExDriver = new XlsExportOptionsEx();

                //optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                //optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                //optionsEx.ExportMode = XlsExportMode.SingleFilePageByPage;


                ////optionsExFilm.ExportMode = XlsExportMode.SingleFilePageByPage;
                ////optionsExFilm.SheetName = "CẮT FILM";
                //optionsEx.SheetName = "HCNS-IT";
                //grvData.OptionsPrint.PrintSelectedRowsOnly = false;
                //try
                //{
                //    string filepath = $"{f.SelectedPath}/BaoCaoCongViec_{date}.xls";
                //    grvData.ExportToXls(filepath, optionsEx);

                //    Process.Start(filepath);

                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message.ToString());
                //    //grvData.ExportToExcelOld($"{f.SelectedPath}/KeHoachDuAn_{cboProject.Text}.xls");
                //}
                //grvData.ClearSelection();

            }
        }

        private void grdDataFilm_Click(object sender, EventArgs e)
        {

        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            //var tabSelected = xtraTabControl1.SelectedTabPageIndex;
            //MessageBox.Show(tabSelected.ToString());

            //LoadData();
        }

        private void mnuMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
