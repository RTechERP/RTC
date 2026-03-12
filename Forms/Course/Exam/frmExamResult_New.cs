using BMS.Model;
using DevExpress.Utils;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmExamResult : _Forms
    {
        public frmExamResult()
        {
            InitializeComponent();
        }

        private void frmExamResult_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            int currentYear = now.Year;
            int currentMonth = now.Month;
            int currentQuarter = (currentMonth - 1) / 3 + 1;

            nudYear.Value = currentYear;
            nudQuarter.Value = currentQuarter;

            loadDataCourseCatalog();
        }

        private void loadDataCourseCatalog()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ExamType", typeof(int));
            dt.Columns.Add("Code", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("NameDepartment", typeof(string));
            var data = new List<(int ExamType, string Code, string Name, string NameDepartment)>
            {
                (1, "VS/HL","Vision", "Kỹ thuật" ),
                (2, "PLC","Điện", "Kỹ thuật" ),
                (3, "K1","Phần mềm", "Kỹ thuật" ),
                (4, "NQKT","Nội quy", "Kỹ thuật" ),
                (5, "AGV","AGV", "AGV" ),
                (6, "T","Tester", "Kỹ thuật" ),
            };
            foreach (var d in data)
            {
                var row = dt.NewRow();
                row["ExamType"] = d.ExamType;
                row["Code"] = d.Code;
                row["Name"] = d.Name;
                row["NameDepartment"] = d.NameDepartment;
                dt.Rows.Add(row);
            }
            grdCourse.DataSource = dt;
            grvCourse.ExpandAllGroups();
        }

        private void loadData()
        {
            int year = (int)nudYear.Value;
            int season = (int)nudQuarter.Value;
            var testType = TextUtils.ToInt(grvCourse.GetFocusedRowCellValue(colExamType));
            //DataTable dt = TextUtils.Select(
            //    $@"select ex.*, e.FullName from ExamResult as ex
            //       left join Employee as e on ex.EmployeeID = e.ID
            //       where ex.YearValue={year} and ex.Season={quarter} and ex.TestType={testType}");
            //dt.Columns.Add("STT", typeof(int));
            //dt.Columns.Add("FinalMark", typeof(decimal));
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    dt.Rows[i]["STT"] = i + 1;
            //    var totalCorrect = TextUtils.ToDecimal(dt.Rows[i]["TotalCorrect"]);
            //    var totalQuestionCount = TextUtils.ToDecimal(dt.Rows[i]["TotalQuestion"]);
            //    var result = totalCorrect / totalQuestionCount;
            //    dt.Rows[i]["FinalMark"] = Math.Round(result, 2); ;
            //}

            DataTable dt = TextUtils.LoadDataFromSP("spGetExamResult", "A",
                                                    new string[] { "@YearValue", "@Season", "@TestType" },
                                                    new object[] { year, season, testType });

            grdTestResult.DataSource = dt;
        }

        private void grvCourse_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialog f = new FolderBrowserDialog();
            //if (f.ShowDialog() == DialogResult.OK)
            //{
            //    XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
            //    optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
            //    optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
            //    grvTestResult.OptionsPrint.PrintSelectedRowsOnly = false;
            //    try
            //    {
            //        var yearValue = TextUtils.ToString(grvTestResult.GetFocusedRowCellValue(colYear));
            //        var quarter = TextUtils.ToString(grvTestResult.GetFocusedRowCellValue(colQuarter));
            //        var examName = TextUtils.ToString(grvCourse.GetFocusedRowCellValue(colCourseName));
            //        string filepath = $"{f.SelectedPath}/Kết-quả-bài-thi-{examName}-quý-{quarter}-năm-{yearValue}.xls";
            //        grvTestResult.ExportToXls(filepath, optionsEx);

            //        Process.Start(filepath);

            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message.ToString());
            //    }
            //    grvTestResult.ClearSelection();
            //}

            var yearValue = TextUtils.ToString(grvTestResult.GetFocusedRowCellValue(colYear));
            var quarter = TextUtils.ToString(grvTestResult.GetFocusedRowCellValue(colQuarter));
            var examName = TextUtils.ToString(grvCourse.GetFocusedRowCellValue(colCourseName));
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = Path.Combine(f.SelectedPath, $"KetQuaThi_{examName}_Q{quarter}_{yearValue}.xlsx");

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdTestResult;

                //PrintableComponentLink printableComponentLink2 = new PrintableComponentLink(printingSystem);
                //printableComponentLink2.Component = grdDataMisa;

                try
                {
                    using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
                    {
                        CompositeLink compositeLink = new CompositeLink(printingSystem);
                        compositeLink.Links.Add(printableComponentLink1);
                        //compositeLink.Links.Add(printableComponentLink2);
                        //compositeLink.Links.Add(printableComponentLink3);

                        //compositeLink.PrintingSystem.XlSheetCreated += PrintingSystem_XlSheetCreated;

                        compositeLink.CreatePageForEachLink();
                        optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;

                        compositeLink.PrintingSystem.SaveDocument(filepath);
                        compositeLink.ExportToXlsx(filepath, optionsEx);
                        Process.Start(filepath);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void grdTestResult_DoubleClick(object sender, EventArgs e)
        {
            var examType = TextUtils.ToInt(grvCourse.GetFocusedRowCellValue(colExamType));
            var yearValue = TextUtils.ToInt(grvTestResult.GetFocusedRowCellValue(colYear));
            var quarter = TextUtils.ToInt(grvTestResult.GetFocusedRowCellValue(colQuarter));
            var employeeName = TextUtils.ToString(grvTestResult.GetFocusedRowCellValue(colFullName));
            var employeeID = TextUtils.ToInt(grvTestResult.GetFocusedRowCellValue(colEmployeeID));
            var frm = new frmExamResultDetail_New
            {
                employeeName = employeeName,
                employeeID = employeeID,
                yearValue = yearValue,
                quarter = quarter,
                examType = examType
            };
            frm.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int[] selectedRows = grvTestResult.GetSelectedRows();
            if (selectedRows.Length <= 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên muốn xóa!", "Thông báo");
                return;
            }


            //int id = TextUtils.ToInt(grvTestResult.GetFocusedRowCellValue(colID));
            //if (id <= 0) return;

            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn xoá kết quả thi của nhân viên đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                foreach (int row in selectedRows)
                {
                    int id = TextUtils.ToInt(grvTestResult.GetRowCellValue(row,colID));
                    if (id <= 0) continue;

                    SQLHelper<ExamResultModel>.DeleteModelByID(id);
                    SQLHelper<ExamResultDetailModel>.DeleteByAttribute("ExamResultID", id);
                }
                
                loadData();
            }


        }
    }
}