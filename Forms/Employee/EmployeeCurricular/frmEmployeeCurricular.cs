using DevExpress.XtraEditors;
using System;
using BMS;
using BMS.Business;
using BMS.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Forms.Classes.cGlobVar;
using DevExpress.DataAccess.Excel;


namespace BMS
{
    public partial class frmEmployeeCurricular : _Forms
    {
        public frmEmployeeCurricular()
        {
            InitializeComponent();
        }

        private void frmEmployeeCurricular_Load(object sender, EventArgs e)
        {
            nmrYear.Value = DateTime.Now.Year;
            nmrMonth.Value = DateTime.Now.Month;

            loadEmployee();
            loadDepartment();
            loadCurricular();

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmEmployeeCurricularDetails frm = new frmEmployeeCurricularDetails();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadCurricular();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = gridView.FocusedRowHandle;
            int ID = TextUtils.ToInt(gridView.GetFocusedRowCellValue(colID));
            EmployeeCurricularModel model = (EmployeeCurricularModel)EmployeeCurricularBO.Instance.FindByPK(ID);
            frmEmployeeCurricularDetails frm = new frmEmployeeCurricularDetails();
            frm.employeeCurricularModel = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadCurricular();
                gridView.FocusedRowHandle = focusedRowHandle;
            }
        }
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(gridView.GetFocusedRowCellValue(colID));
            if (id <= 0) return;

            string fullName = TextUtils.ToString(gridView.GetFocusedRowCellValue(colFullName));
            if (MessageBox.Show($"Bạn có thực sự muốn xóa ngoại khoá của nhân viên [{fullName}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                EmployeeCurricularBO.Instance.Delete(id);
                gridView.DeleteSelectedRows();
            }

        }
        private void loadCurricular()
        {
            int employeeID = TextUtils.ToInt(cboEmployee.EditValue);
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeCurricular", "Curricular",
                                                    new string[] { "@Month", "@Year", "@DepartmentID", "@EmployeeID" },
                                                    new object[] { nmrMonth.Value, nmrYear.Value, departmentID, employeeID });

            grdData.DataSource = dt;
        }
        void loadDepartment()
        {
            //List<DepartmentModel> listDepartment = SQLHelper<DepartmentModel>.SqlToList($"select ID, Code, Name from Department");
            List<DepartmentModel> listDepartment = SQLHelper<DepartmentModel>.FindAll();
            cboDepartment.Properties.DataSource = listDepartment;
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.ValueMember = "ID";
        }
        void loadEmployee()
        {
            DataTable list = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = list;
        }
        private void nmrYear_ValueChanged(object sender, EventArgs e)
        {

            loadCurricular();
        }
        private void nmrMonth_ValueChanged(object sender, EventArgs e)
        {
            loadCurricular();
        }
        private void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {
            loadCurricular();
        }
        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            loadCurricular();
        }
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadCurricular();
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            frmExcelEmployeeCurricular frm = new frmExcelEmployeeCurricular();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadCurricular();
            }
        }
    }
}