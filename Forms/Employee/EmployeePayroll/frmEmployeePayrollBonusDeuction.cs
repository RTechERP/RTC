using BMS.Business;
using BMS.Model;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class frmEmployeePayrollBonusDeuction : _Forms
    {
        //public int year = 0;
        //public int month = 0;
        public frmEmployeePayrollBonusDeuction()
        {
            InitializeComponent();
        }

        private void frmEmployeePayrollBonusDeuction_Load(object sender, EventArgs e)
        {
            //txtYear.Value = year;
            //txtMonth.Value = month;
            txtPageNumber.Text = "1";

            loadDepartment();
            loadEmployee();
            loadData();
        }
        private void loadData()
        {
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            int employeeID = TextUtils.ToInt(cboEmployee.EditValue);

            DataSet ds = TextUtils.LoadDataSetFromSP("spGetEmployeePayrollBonusDeduction"
                , new string[] { "@PageNumber", "@PageSize", "@Year", "@Month", "@Keyword", "@DepartmentID", "@EmployeeID" }
                , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), TextUtils.ToInt(txtYear.Value), TextUtils.ToInt(txtMonth.Value), txtSearch.Text.Trim(),departmentID,employeeID });
            grdData.DataSource = ds.Tables[0];
            if (ds.Tables.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(ds.Tables[1].Rows[0]["TotalPage"]);
        }

        void loadDepartment()
        {
            List<DepartmentModel> listDepartment = SQLHelper<DepartmentModel>.FindAll();
            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.DataSource = listDepartment;
        }

        void loadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmEmployeePayrollBonusDeuctionDetail frm = new frmEmployeePayrollBonusDeuctionDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            var focusedRowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            EmployeePayrollBonusDeuctionModel model = (EmployeePayrollBonusDeuctionModel)EmployeePayrollBonusDeuctionBO.Instance.FindByPK(ID);
            frmEmployeePayrollBonusDeuctionDetail frm = new frmEmployeePayrollBonusDeuctionDetail();
            frm.employee = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
                grvData.FocusedRowHandle = focusedRowHandle;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            string employee = TextUtils.ToString(grvData.GetFocusedRowCellValue(colEmployee));
            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa khấu trừ của nhân viên [{0}] không?", employee), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                EmployeePayrollBonusDeuctionBO.Instance.Delete(ID);
                grvData.DeleteSelectedRows();
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadData();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadData();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadData();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            loadData();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFind_Click(null, null);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            frmImportExcelEmployeePayrollBonusDeuction frm = new frmImportExcelEmployeePayrollBonusDeuction();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string fileName = Path.Combine(f.SelectedPath, $"BangThanhToanLuong_T{txtMonth.Value}_{txtYear.Value}.xls");
                XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                grvData.OptionsPrint.AutoWidth = false;
                grvData.OptionsPrint.ExpandAllDetails = false;
                grvData.OptionsPrint.PrintDetails = true;
                grvData.OptionsPrint.UsePrintStyles = true;
                try
                {
                    grvData.ExportToXls(fileName, optionsEx);
                    Process.Start(fileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
        void approved(bool isApproved)
        {
            string approved = isApproved == true ? "duyệt" : "hủy duyệt";
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            if (MessageBox.Show(string.Format("Bạn có chắc muốn {0} mã nhân viên {1} không ?", approved, code), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
                string sql = string.Format("UPDATE dbo.EmployeeCollectMoney SET IsApproved = {0} WHERE ID = {1}", isApproved ? 1 : 0, ID);
                TextUtils.ExcuteSQL(sql);
                if (isApproved == true)
                    grvData.SetFocusedRowCellValue(colIsApproved, 1);
                else
                    grvData.SetFocusedRowCellValue(colIsApproved, 0);
            }
        }
        private void grvData_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;

            try
            {
                if (e.Column.FieldName == "DepartmentName")
                {
                    int val1 = TextUtils.ToInt(view.GetListSourceRowCellValue(e.ListSourceRowIndex1, "DepartmentSTT"));
                    int val2 = TextUtils.ToInt(view.GetListSourceRowCellValue(e.ListSourceRowIndex2, "DepartmentSTT"));
                    e.Handled = true;
                    e.Result = System.Collections.Comparer.Default.Compare(val1, val2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtYear_ValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void txtMonth_ValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
