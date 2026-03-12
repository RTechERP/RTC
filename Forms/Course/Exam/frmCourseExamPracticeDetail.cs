using BaseBusiness.DTO;
using BMS;
using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting;
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
    public partial class frmCourseExamPracticeDetail : _Forms
    {
        public int courseID;
        public int courseExamID;
        public int courseResultID;
        public int employeeID;
        public string examCode;
        public int testTime;

        public frmCourseExamPracticeDetail()
        {
            InitializeComponent();
        }

        bool firstLoad = true;
        private void frmExamTestResultDetail_Load(object sender, EventArgs e)
        {
            loadCourse();
            loadEmployee();

            cbCourse.EditValue = courseID;
            cboEmployee.EditValue = employeeID;
            lblExamCode.Text = examCode;
            lblExamTestTime.Text = Lib.ToString(testTime) + " phút";

            //EmployeeModel _selectedModel = (EmployeeModel)cboEmployee.GetSelectedDataRow();
            lblExamEmployee.Text = cboEmployee.Text;

            loadData();
            grvData.RefreshData();
        }

        void loadData()
        {
            //grvData.Columns[colResultText.FieldName].Summary.Clear();
            lblExamEmployee.Text = cboEmployee.Text;

            employeeID = Lib.ToInt(cboEmployee.EditValue);
            courseID = Lib.ToInt(cbCourse.EditValue);

            CourseExamModel _latestExamResult = SQLHelper<CourseExamDTO>.ProcedureToList("spGetCourseExamResult",
                                                                                    new string[] { "@CourseID", "@EmployeeID", "@OrderNumber" },
                                                                                    new object[] { courseID, employeeID, 1 }).FirstOrDefault() 
                                                                                    //  =====================lee min khooi 26/09/2024======================
                                                                                    ?? SQLHelper<CourseExamModel>.FindByID(courseExamID);

            if (_latestExamResult.ID <= 0)
            {


                lblExamCode.Text = lblExamTestTime.Text = "";
                courseResultID = 0;
                //grvData.RefreshData();
                //return;
                //grdData.DataSource = null;
                //grvData.Columns[colResultText.FieldName].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, colResultText.FieldName, $"Tổng đúng = 0"));
                //grvData.Columns[colResultText.FieldName].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, colResultText.FieldName, $"Tổng sai = 0"));
            }
            else
            {
                courseResultID = _latestExamResult.ID;
                lblExamCode.Text = _latestExamResult.CodeExam;
                //lee min khooi update 26/09/2024
                lblExamTestTime.Text = Lib.ToString(testTime) + " phút";
                
            }

            //lee min khooi update 26/09/2024
            DataTable data = TextUtils.LoadDataFromSP("spGetCourseExamResultDetail", "A",
                            new string[] { "@CourseID", "@CourseExamResultID", "@EmployeeID", "@CourseExamID" },
                            new object[] { courseID, courseResultID, employeeID, courseExamID });
            grdData.DataSource = data;

            var summarys = grvData.Columns[colResultText.FieldName].Summary;
            if (summarys.Count > 0)
            {
                grvData.Columns[colResultText.FieldName].Summary.Clear();
            }

            var dataCorrect = data.Select("Result = 1");
            var dataIncorrect = data.Select("Result = 0");

            grvData.Columns[colResultText.FieldName].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, colResultText.FieldName, $"Tổng đúng = {dataCorrect.Length.ToString()}"));
            grvData.Columns[colResultText.FieldName].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, colResultText.FieldName, $"Tổng sai = {dataIncorrect.Length.ToString()}"));

            grvData.RefreshData();
        }

        void loadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });

            cboEmployee.Properties.DataSource = dt;
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
        }

        void loadCourse()
        {
            cbCourse.Properties.DataSource = SQLHelper<CourseModel>.FindAll();
            cbCourse.Properties.ValueMember = "ID";
            cbCourse.Properties.DisplayMember = "Code";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //EmployeeModel _selectedModel = (EmployeeModel)cboEmployee.GetSelectedDataRow();
            //lblExamEmployee.Text = _selectedModel == null ? string.Empty : _selectedModel.FullName;

            loadData();

        }

        private void grvData_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle == 0 || e.Column != colResultText) return;
            int score = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colResult));

            if (score == 1)
            {
                e.Appearance.BackColor = Color.Green;
                e.Appearance.ForeColor = Color.White;
            }
            else
            {
                e.Appearance.BackColor = Color.OrangeRed;
                e.Appearance.ForeColor = Color.White;
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            var codeMaster = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCateCode));
            if (codeMaster == "" || codeMaster == null) return;

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
            string fileSourceName = "ChiTietBaiThi.xls";

            string sourcePath = Application.StartupPath + "\\" + fileSourceName;
            string catcode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCateCode));
            string currentPath = path + "\\" + catcode + DateTime.Now.ToString("_dd_MM_yyyy_HH_mm_ss") + ".xls";
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


                    //workSheet.Cells[2, 3] = lblExamCategory.Text.Trim();
                    workSheet.Cells[3, 3] = lblExamCode.Text.Trim();
                    workSheet.Cells[4, 3] = lblExamTestTime.Text.Trim();
                    workSheet.Cells[5, 3] = lblExamEmployee.Text.Trim();


                    for (int i = grvData.RowCount - 1; i >= 0; i--)
                    {
                        workSheet.Cells[9, 1] = i + 1;
                        workSheet.Cells[9, 2] = TextUtils.ToString(grvData.GetRowCellValue(i, colCateCode));
                        workSheet.Cells[9, 3] = TextUtils.ToString(grvData.GetRowCellValue(i, colCodeTest));
                        workSheet.Cells[9, 4] = TextUtils.ToString(grvData.GetRowCellValue(i, colFullName));
                        workSheet.Cells[9, 5] = TextUtils.ToString(grvData.GetRowCellValue(i, colContentTest));
                        workSheet.Cells[9, 6] = TextUtils.ToString(grvData.GetRowCellValue(i, colCodeAnswerRight));
                        workSheet.Cells[9, 7] = TextUtils.ToString(grvData.GetRowCellValue(i, colCodeAnswerChosen));
                        if (TextUtils.ToBoolean(grvData.GetRowCellValue(i, colResultText)))
                        {
                            workSheet.Cells[9, 8] = "Đúng";
                        }
                        else
                            workSheet.Cells[9, 8] = "Sai";


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

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void stackPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
