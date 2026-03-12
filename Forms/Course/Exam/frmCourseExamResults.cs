using BMS.Business;
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
    public partial class frmCourseExamResults : _Forms
    {
        public frmCourseExamResults()
        {
            InitializeComponent();
        }

        private void frmCourseExamResults_Load(object sender, EventArgs e)
        {
            loadExam();
            loadExamResult();
        }

        //Load danh sách đề thi
        void loadExam()
        {
            DataTable dt = TextUtils.GetTable("spGetCourseExam");
            grdExam.DataSource = dt;
        }

        //Load danh sách kết quả
        void loadExamResult()
        {
            int courseID = TextUtils.ToInt(grvExam.GetFocusedRowCellValue(colCourseId));
            //int examtype = TextUtils.ToInt(grvExam.GetFocusedRowCellValue(colExamType));
            int courseExamID = TextUtils.ToInt(grvExam.GetFocusedRowCellValue(colID));

            if (courseID <= 0)
            {
                return;
            }
            DataTable dt = TextUtils.LoadDataFromSP("spGetCourseExamResult", "A",
                                                        new string[] { "@CourseExamID", "@EmployeeID", "@OrderNumber"},
                                                        new object[] { courseExamID, 0, 1 });

            grdData.DataSource = dt;
        }

        private void grvExam_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadExamResult();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            int rowHandledExam = grvExam.FocusedRowHandle;
            int rowHandledResult = grvData.FocusedRowHandle;

            loadExam();
            loadExamResult();

            grvExam.FocusedRowHandle = rowHandledExam;
            grvData.FocusedRowHandle = rowHandledResult;
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            //btnEdit_Click(null, null);
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
                    string code = TextUtils.ToString(grvExam.GetFocusedRowCellValue(colCode));

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

        private void grdExam_Click(object sender, EventArgs e)
        {

        }

        private void btnHistoryExam_Click(object sender, EventArgs e)
        {
            int courseID = TextUtils.ToInt(grvExam.GetFocusedRowCellValue(colCourseId));
            int courseExamResultID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIDDetail));
            int employeeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
            int courseExamID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colCourseExamId));

            frmHistoryExam frm = new frmHistoryExam();
            frm.employeeID = employeeID;
            frm.courseID = courseID;
            frm.courseExamResultID = courseExamResultID;
            frm.courseExamID = courseExamID;
            frm.Show();
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            int courseID = TextUtils.ToInt(grvExam.GetFocusedRowCellValue(colCourseId));
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIDDetail));
            int employeeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
            int examType = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colExamType));

            if (id <= 0)
            {
                return;
            }

            frmCourseExamResultDetail frm = new frmCourseExamResultDetail();
            frm.courseID = courseID;
            frm.courseExamResultID = id;
            frm.employeeID = employeeID;
            frm.examType = examType;

            frm.ShowDialog();
        }


        private void btnNew_Click(object sender, EventArgs e)
        {
            frmCourseExamResultPractice frm = new frmCourseExamResultPractice();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadExamResult();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            int id  = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIDDetail));
            if (id <= 0)
            {
                return;
            }

            CourseExamResultModel examResult = SQLHelper<CourseExamResultModel>.FindByID(id);

            frmCourseExamResultPractice frm = new frmCourseExamResultPractice();
            //frm.examResult = examResult;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadExamResult();
                grvData.FocusedRowHandle = rowHandle;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIDDetail));
            if (id <= 0)
            {
                return;
            }

            string fullName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá kết quả thi của nhân viên [{fullName}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                CourseExamResultBO.Instance.Delete(id);
                grvData.DeleteSelectedRows();
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
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
                    string code = TextUtils.ToString(grvExam.GetFocusedRowCellValue(colCode));

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

        private void btnRefreshExamResult_Click(object sender, EventArgs e)
        {
            int rowHandledResult = grvData.FocusedRowHandle;
            loadExamResult();

            grvData.FocusedRowHandle = rowHandledResult;
        }

       
    }
}
