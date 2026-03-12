using BMS.Business;
using BMS.Model;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class frmThuHoPhongBan : _Forms
    {
        public frmThuHoPhongBan()
        {
            InitializeComponent();
        }

        private void frmThuHoPhongBan_Load(object sender, EventArgs e)
        {
            dtpStartDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpEndDate.Value = dtpStartDate.Value.AddMonths(+1).AddDays(-1);
            txtPageNumber.Text = "1";

            loadDepartment();
            loadEmployee();
            loadData();
        }
        private void loadData()
        {
            DateTime dateTimeS = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, dtpStartDate.Value.Day, 00, 00, 00);
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            int employeeID = TextUtils.ToInt(cboEmployee.EditValue);

            DataSet ds = TextUtils.LoadDataSetFromSP("spGetEmployeeCollectMoney"
                , new string[] { "@PageNumber", "@PageSize", "@StartDate", "@EndDate", "@Keyword", "@DepartmentID", "@EmployeeID" }
                , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), dateTimeS, dateTimeE, txtSearch.Text.Trim(),departmentID,employeeID });
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
            EmployeeCollectMoneyModel model = new EmployeeCollectMoneyModel();
            frmThuHoPhongBanDetail frm = new frmThuHoPhongBanDetail();
            frm.employee = model;
            if(frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }    
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            bool isApproved = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApproved));
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            if (isApproved == true)
            {
                MessageBox.Show(string.Format("Bạn không thể sửa mã nhân viên {0}. ", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var focusedRowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            EmployeeCollectMoneyModel model = (EmployeeCollectMoneyModel)EmployeeCollectMoneyBO.Instance.FindByPK(ID);
            frmThuHoPhongBanDetail frm = new frmThuHoPhongBanDetail();
            frm.employee = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
                grvData.FocusedRowHandle = focusedRowHandle;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool isApproved = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApproved));
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            if(isApproved == true)
            {
                MessageBox.Show(string.Format("Bạn không thể xóa mã nhân viên {0}. ", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            string employee = TextUtils.ToString(grvData.GetFocusedRowCellValue(colEmployee));
            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa nhân viên [{0}] không?", employee), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                EmployeeCollectMoneyBO.Instance.Delete(ID);
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
            frmImportExcelEmployeeCollectMoney frm = new frmImportExcelEmployeeCollectMoney();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }    
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null,null);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                grvData.OptionsPrint.AutoWidth = false;
                grvData.OptionsPrint.ExpandAllDetails = false;
                grvData.OptionsPrint.PrintDetails = true;
                grvData.OptionsPrint.UsePrintStyles = true;
                try
                {
                    grvData.ExportToXls(sfd.FileName);
                    Process.Start(sfd.FileName);
                }
                catch (Exception)
                {
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

        private void btnApproved_Click(object sender, EventArgs e)
        {
            bool isApproved = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApproved));
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            if (isApproved == true)
            {
                MessageBox.Show(String.Format("Mã nhân viên {0} đã được duyệt.", code), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                return;
            }
            approved(true);
        }

        private void btnUnapproved_Click(object sender, EventArgs e)
        {
            bool isApproved = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApproved));
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            if (isApproved == false)
            {
                MessageBox.Show(String.Format("Mã nhân viên {0} chưa được duyệt.", code), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                return;
            }
            approved(false);
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
