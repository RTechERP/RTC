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
    public partial class frmHistoryExam : _Forms
    {
        public int employeeID = 0;
        public int courseID = 0;
        public int courseExamResultID = 0;
        public int courseExamID = 0;
        public frmHistoryExam()
        {
            InitializeComponent();
        }

        private void frmHistoryExam_Load(object sender, EventArgs e)
        {
            loadData();
        }

        void loadData()
        {

            DataTable dt = TextUtils.LoadDataFromSP("spGetCourseExamResult", "A",
                                                         new string[] { "@CourseExamID", "@EmployeeID", "@OrderNumber" },
                                                         new object[] { courseExamID, employeeID, 0 });

            grdData.DataSource = dt;
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            courseExamResultID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIDDetail));
            int examType = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colExamType));
            if (courseExamResultID <= 0)
            {
                return;
            }
            frmCourseExamResultDetail frm = new frmCourseExamResultDetail();
            frm.courseID = courseID;
            frm.courseExamResultID = courseExamResultID;
            frm.employeeID = employeeID;
            frm.examType = examType;
            frm.ShowDialog();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                XlsExportOptions optionsEx = new XlsExportOptions();
                //optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                grvData.OptionsPrint.PrintSelectedRowsOnly = false;
                
                try
                {
                    string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));

                    string filepath = $"{f.SelectedPath}/DanhSachKetQuaThi_{code}.xls";
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
