using BMS;
using DevExpress.Utils;
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

namespace Forms.ListTest
{
    public partial class frmExamTestResultDetail : _Forms
    {
        public string examCateCode;
        public int employeeid;
        public int examListTestId;
        public frmExamTestResultDetail()
        {
            InitializeComponent();
        }

        private void frmExamTestResultDetail_Load(object sender, EventArgs e)
        {
            txtExamCateCode.Text = examCateCode;
            cboEmployee.EditValue = employeeid;
            cboListTest.EditValue = examListTestId;

            loadListTest();
            loadEmployee();
            loadData();
        }

        void loadData()
        {
            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetExamTestResultDetail",
                            new string[] { "@ExamCateCode", "@EmployeeID", "@ExamListTestID" },
                            new object[] { txtExamCateCode.Text.Trim(), TextUtils.ToInt(cboEmployee.EditValue), TextUtils.ToInt(cboListTest.EditValue) });
            DataTable dt = dataSet.Tables[0];
            grdData.DataSource = dt;
            //grvData.ExpandAllGroups();
            if (dt.Rows.Count > 0)
            {
                lblExamCategory.Text = TextUtils.ToString(dt.Rows[0]["CatCode"]) + " - " + TextUtils.ToString(dt.Rows[0]["CatName"]);
                lblExamListTest.Text = TextUtils.ToString(dt.Rows[0]["CodeTest"]) + " - " + TextUtils.ToString(dt.Rows[0]["NameTest"]);
                lblExamTestTime.Text = TextUtils.ToString(dt.Rows[0]["TestTime"]) + " phút";
                lblExamEmployee.Text = TextUtils.ToString(dt.Rows[0]["FullName"]);
            }

            grdResultType.DataSource = dataSet.Tables[1];
        }

        /// <summary>
        /// Load danh sách nhân viên vào cboEmployee
        /// </summary>
        void loadEmployee()
        {
            DataTable dt = TextUtils.Select("SELECT ID, Code, FullName FROM Employee");

            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;
        }

        /// <summary>
        /// Load danh sách đề thi vào cboListTest
        /// </summary>
        void loadListTest()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetExamListTestByCate", "A",
                            new string[] { "@ExamCategoryCode" },
                            new object[] { txtExamCateCode.Text.Trim() });

            cboListTest.Properties.ValueMember = "ID";
            cboListTest.Properties.DisplayMember = "CodeTest";
            cboListTest.Properties.DataSource = dt;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void txtExamCateCode_TextChanged(object sender, EventArgs e)
        {
            loadListTest();
        }

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            //if (e.RowHandle >= 0)
            //{
            //    bool checkResult = TextUtils.ToBoolean(grvData.GetRowCellValue(e.RowHandle, colCheckResult));
            //    if (checkResult)
            //    {
            //        e.Appearance.BackColor = Color.Green;
            //        e.Appearance.ForeColor = Color.White;
            //        e.HighPriority = true;
            //    }
            //    else
            //    {
            //        e.Appearance.BackColor = Color.OrangeRed;
            //        e.Appearance.ForeColor = Color.White;
            //        e.HighPriority = true;
            //    }
            //}
        }

        private void grvData_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == colCheckResult)
            {
                if (TextUtils.ToInt(e.Value) == 1)
                {
                    e.DisplayText = "Đúng";
                }
                else
                {
                    e.DisplayText = "Sai";
                }
            }
        }

        private void grvData_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (e.Column == colCheckResult)
                {
                    if (TextUtils.ToBoolean(e.CellValue))
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
            string fileSourceName = "TestResult.xls";

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

                    
                    workSheet.Cells[2, 3] = lblExamCategory.Text.Trim();
                    workSheet.Cells[3, 3] = lblExamListTest.Text.Trim();
                    workSheet.Cells[4, 3] = lblExamTestTime.Text.Trim();
                    workSheet.Cells[5, 3] = lblExamEmployee.Text.Trim();


                    for (int i = grvData.RowCount - 1; i >= 0; i--)
                    {
                        workSheet.Cells[9, 1] = i + 1;
                        workSheet.Cells[9, 2] = TextUtils.ToString(grvData.GetRowCellValue(i, colCateCode));
                        workSheet.Cells[9, 3] = TextUtils.ToString(grvData.GetRowCellValue(i, colCodeTest));
                        workSheet.Cells[9, 4] = TextUtils.ToString(grvData.GetRowCellValue(i, colFullName));
                        workSheet.Cells[9, 5] = TextUtils.ToString(grvData.GetRowCellValue(i, colContentTest));
                        workSheet.Cells[9, 6] = TextUtils.ToString(grvData.GetRowCellValue(i, colCorrectAnswer));
                        workSheet.Cells[9, 7] = TextUtils.ToString(grvData.GetRowCellValue(i, colResultChose));
                        if (TextUtils.ToBoolean(grvData.GetRowCellValue(i, colCheckResult)))
                        {
                            workSheet.Cells[9, 8] = "Đúng";
                        }else
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
    }
}
