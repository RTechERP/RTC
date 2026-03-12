using BMS.Model;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmCourseExamResultDetail : _Forms
    {
        public int courseID;
        public int courseExamResultID;
        public int employeeID;
        public int examType;

        public frmCourseExamResultDetail()
        {
            InitializeComponent();
        }

        private void frmCourseExamResultDetail_Load(object sender, EventArgs e)
        {
            loadEmployee();
            loadData();
        }

        void loadData()
        {
            employeeID = TextUtils.ToInt(cboEmployee.EditValue);

            DataTable dt = TextUtils.LoadDataFromSP("spGetCourseExamResultDetail", "A",
                                                        new string[] { "@CourseID", "@CourseExamResultID", "@EmployeeID", "@ExamType" },
                                                        new object[] { courseID, courseExamResultID, employeeID, examType });

            grdData.DataSource = dt;
        }


        void loadEmployee()
        {
            List<EmployeeModel> list = SQLHelper<EmployeeModel>.SqlToList("SELECT ID, Code, FullName FROM Employee WHERE Status <> 1");
            cboEmployee.Properties.DataSource = list;
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";

            cboEmployee.EditValue = employeeID;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }

        private void grvData_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column == colResultText)
            {
                int result = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colResult));
                if (result == 1)
                {
                    e.Appearance.BackColor = Color.Lime;
                }
                else
                {
                    e.Appearance.BackColor = Color.Orange;
                }
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                XlsExportOptions optionsEx = new XlsExportOptions();
                //optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                //optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                grvData.OptionsPrint.PrintSelectedRowsOnly = false;
                try
                {
                    string code = TextUtils.ToString(grvData.GetRowCellValue(0, colCode));
                    string fullName = TextUtils.ToString(grvData.GetRowCellValue(0, colFullName));

                    string filepath = $"{f.SelectedPath}/DanhSachKetQuaThi_{code}_{fullName}.xls";
                    grvData.ExportToXls(filepath, optionsEx);

                    Process.Start(filepath);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo");
                }
                grvData.ClearSelection();
            }
        }
    }
}
