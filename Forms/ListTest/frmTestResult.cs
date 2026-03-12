using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMS.BO;
using BMS.Model;
using BMS.Business;
using Forms.ListTest;
using DevExpress.XtraPrinting;
using System.Diagnostics;

namespace BMS
{
    public partial class frmTestResult : _Forms
    {
        public frmTestResult()
        {
            InitializeComponent();
        }

        private void frmTestResult_Load(object sender, EventArgs e)
        {
            nudYear.Value = DateTime.Now.Year;
            nudQuy.Value = DateTime.Now.Month <= 3 ? 1 : (DateTime.Now.Month <= 6 ? 2 : (DateTime.Now.Month <= 9 ? 3 : 4));
            LoadDataCat();
            LoadDataTestResult();
        }
        void LoadDataCat()
        {
            DataTable dataTable = TextUtils.Select($"SELECT * FROM ExamCategory WHERE YEAR = {nudYear.Value} AND Quy = {nudQuy.Value} ORDER BY ID DESC");
            grdCategory.DataSource = dataTable;
            grvCategory_FocusedRowChanged(null, null);
        }
        void LoadDataTestResult()
        {
            int id = TextUtils.ToInt(grvCategory.GetFocusedRowCellValue(colCatID));
            DataTable dataTable = TextUtils.LoadDataFromSP("spGetExamTestResultMaster", "A",
                            new string[] { "@ExamCategoryID", "@YearCate" }, new object[] { id, nudYear.Value });
            grdTestResult.DataSource = dataTable;
        }
        private void grvCategory_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDataTestResult();
        }

        private void nudYear_ValueChanged(object sender, EventArgs e)
        {
            LoadDataCat();
        }

        private void grvTestResult_DoubleClick(object sender, EventArgs e)
        {
            // mở form master
        }

        private void grvTestResult_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            
        }

        private void grvTestResult_DoubleClick_1(object sender, EventArgs e)
        {
            string cateCode = TextUtils.ToString(grvTestResult.GetFocusedRowCellValue(colCatCodeResult));
            int employeeID = TextUtils.ToInt(grvTestResult.GetFocusedRowCellValue(colEmployeeID));
            int examListTestID = TextUtils.ToInt(grvTestResult.GetFocusedRowCellValue(colExamListTestID));
            if (examListTestID <= 0)
            {
                return;
            }

            frmExamTestResultDetail frm = new frmExamTestResultDetail();
            frm.examCateCode = cateCode;
            frm.employeeid = employeeID;
            frm.examListTestId = examListTestID;
            frm.ShowDialog();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            var codeCat = TextUtils.ToString(grvCategory.GetFocusedRowCellValue(colCatCode));
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
                optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                grvTestResult.OptionsPrint.PrintSelectedRowsOnly = false;
                try
                {
                    string filepath = $"{f.SelectedPath}/KetQua_MaKiThi-{codeCat}.xls";
                    grvTestResult.ExportToXls(filepath, optionsEx);

                    Process.Start(filepath);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                grvTestResult.ClearSelection();
            }
        }

        private void nudQuy_ValueChanged(object sender, EventArgs e)
        {
            LoadDataCat();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int cateId = TextUtils.ToInt(grvTestResult.GetFocusedRowCellValue(colCategoryID));
            int employeeId = TextUtils.ToInt(grvTestResult.GetFocusedRowCellValue(colEmployeeID));

            int rowhandle = grvTestResult.FocusedRowHandle;

            string fullname = TextUtils.ToString(grvTestResult.GetFocusedRowCellValue(colFullName));

            if (MessageBox.Show($"Bạn có chắc muốn xóa kết quả thi của nhân viên [{fullname}] không?","Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                TextUtils.ExcuteProcedure("spDeleteExamTestResult", new string[] { "@CategoryID", "@EmployeeID" }, new object[] { cateId, employeeId });
                grvTestResult.DeleteRow(rowhandle);
            }

        }

        private void grvTestResult_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataTestResult();
        }
    }
}
