//using DevExpress.Utils;
using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmExamResultDetail_New : _Forms
    {
        public string employeeName;
        public int employeeID;
        public int yearValue;
        public int quarter;
        public int examType;

        public frmExamResultDetail_New()
        {
            InitializeComponent();
        }

        private void frmExamResultDetail_New_Load(object sender, EventArgs e)
        {
            loadExamType();
            loadEmployee();
            loadData();
            nudYear.Value = yearValue;
            nudQuarter.Value = quarter;
            cboExamType.SelectedValue = examType;
            cboEmployee.EditValue = employeeID;
        }

        private void loadData()
        {
            var data = TextUtils.LoadDataSetFromSP("spGetExamResultDetail",
                new string[] { "@YearValue", "@Quarter", "@ExamType", "@EmployeeID" },
                new object[] { yearValue, quarter, examType, employeeID });
            grdData.DataSource = data.Tables[0];
            grdSummary.DataSource = data.Tables[1];

            lblYear.Text = TextUtils.ToString(yearValue);
            lblQuarter.Text = TextUtils.ToString(quarter);
            lblExamType.Text = examType == 1 ? "Vision" :
                               examType == 2 ? "PLC" :
                               examType == 3 ? "Phần mềm" :
                               examType == 4 ? "Nội quy" : "";
            lblEmployee.Text = employeeName;
            answerSummary();
        }

        /// <summary>
        /// Load danh sách nhân viên vào cboEmployee
        /// </summary>
        private void loadEmployee()
        {
            //DataTable dt = TextUtils.Select("SELECT ID, Code, FullName FROM Employee");
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });

            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;
        }

        /// <summary>
        /// Load danh sách đề thi vào cboExamType
        /// </summary>
        private void loadExamType()
        {
            cboExamType.DataSource = new[]
            {
                new {Value=1,Text="Vision"},
                new {Value=2,Text="PLC"},
                new {Value=3,Text="Phần mềm"},
                new {Value=4,Text="Nội quy"}
            };
            cboExamType.DisplayMember = "Text";
            cboExamType.ValueMember = "Value";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void nudYear_ValueChanged(object sender, EventArgs e)
        {
            yearValue = (int)nudYear.Value;
        }

        private void nudQuarter_ValueChanged(object sender, EventArgs e)
        {
            quarter = (int)nudQuarter.Value;
        }

        private void cboExamType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            examType = TextUtils.ToInt(cboExamType.SelectedValue);
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            employeeID = TextUtils.ToInt(cboEmployee.EditValue);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
             
             
            string path = "";
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                path = fbd.SelectedPath;
            }
            else
            {
                return;
            }
            string fileSourceName = "TestResult_New.xls";

            string sourcePath = Application.StartupPath + "\\" + fileSourceName;
            string currentPath = path + "\\" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss") + ".xls";
            try
            {
                File.Copy(sourcePath, currentPath, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi tạo phiếu!" + Environment.NewLine + ex.Message,
                    TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                Microsoft.Office.Interop.Excel.Application app = default(Microsoft.Office.Interop.Excel.Application);
                Microsoft.Office.Interop.Excel.Workbook workBoook = default(Microsoft.Office.Interop.Excel.Workbook);
                Microsoft.Office.Interop.Excel.Worksheet workSheet = default(Microsoft.Office.Interop.Excel.Worksheet);
                try
                {
                    app = new Microsoft.Office.Interop.Excel.Application();
                    app.Workbooks.Open(currentPath);
                    workBoook = app.Workbooks[1];
                    workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBoook.Worksheets[1];

                    workSheet.Cells[2, 2] = lblYear.Text.Trim();
                    workSheet.Cells[3, 2] = lblQuarter.Text.Trim();
                    workSheet.Cells[4, 2] = lblExamType.Text.Trim();
                    workSheet.Cells[5, 2] = lblEmployee.Text.Trim();

                    for (int i = grvData.RowCount - 1; i >= 0; i--)
                    {
                        workSheet.Cells[9, 1] = i + 1;
                        workSheet.Cells[9, 2] = TextUtils.ToString(grvData.GetRowCellValue(i, colContentTest));
                        workSheet.Cells[9, 3] = TextUtils.ToString(grvData.GetRowCellValue(i, colCorrectAnswer));
                        workSheet.Cells[9, 4] = TextUtils.ToString(grvData.GetRowCellValue(i, colResultChose));
                        if (TextUtils.ToBoolean(grvData.GetRowCellValue(i, colCheckResult)))
                        {
                            workSheet.Cells[9, 5] = "Đúng";
                        }
                        else
                            workSheet.Cells[9, 5] = "Sai";

                        ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[9]).Insert();
                    }
                    ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[8]).Delete();
                    ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[8]).Delete();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (app != null)
                    {
                        app.ActiveWorkbook.Save();
                        app.Workbooks.Close();
                        app.Quit();
                    }
                }
                Process.Start(currentPath);
            }
        }

        private void answerSummary()
        {
            GridView view = grvData;
            GridColumn colCorrectAnswer = view.Columns["CorrectAnswers"];
            GridColumn colPickedAnswer = view.Columns["PickedAnswers"];

            int correctCount = 0;
            int incorrectCount = 0;

            for (int i = 0; i < view.DataRowCount; i++)
            {
                object correctValue = view.GetRowCellValue(i, colCorrectAnswer);
                object pickedValue = view.GetRowCellValue(i, colPickedAnswer);

                if (correctValue != null && pickedValue != null &&
                    correctValue.ToString() == pickedValue.ToString())
                {
                    correctCount++;
                }
                else
                {
                    incorrectCount++;
                }
            }

            view.Columns["CorrectAnswers"].SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Count, $"{correctCount} đúng");
            view.Columns["PickedAnswers"].SummaryItem.SetSummary(DevExpress.Data.SummaryItemType.Count, $"{incorrectCount} sai");
        }

        private void grvData_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;

            if (e.Column.FieldName == "AnswerStatus")
            {
                bool cellValue = (bool)view.GetRowCellValue(e.RowHandle, e.Column);
                e.Appearance.BackColor = cellValue ? Color.FromArgb(144, 238, 144) : Color.FromArgb(255, 160, 160); // green - red (but better for the eyes)
                //e.Appearance.ForeColor = cellValue ? Color.Black : Color.White;
                e.DisplayText = cellValue ? "Đúng" : "Sai";
            }
        }
    }
}