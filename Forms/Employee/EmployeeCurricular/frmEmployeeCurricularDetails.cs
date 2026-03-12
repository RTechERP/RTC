using DevExpress.XtraEditors;
using System;
using BMS;
using BMS.Business;
using BMS.Model;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq.Expressions;

namespace BMS
{
    public partial class frmEmployeeCurricularDetails : _Forms
    {
        public EmployeeCurricularModel employeeCurricularModel = new EmployeeCurricularModel();
        public frmEmployeeCurricularDetails()
        {
            InitializeComponent();
        }

        private void frmEmployeeCurricularDetails_Load(object sender, EventArgs e)
        {
            dtpCurricularDate.Value = DateTime.Now;
            loadCurricularDetails();
        }
        void loadCurricularDetails()
        {
            LoadEmployee();
            loadData();
        }
        void loadData()
        {
            if (employeeCurricularModel.ID > 0)
            {
                DateTime dateTime = new DateTime(employeeCurricularModel.CurricularYear, employeeCurricularModel.CurricularMonth, employeeCurricularModel.CurricularDay);
                dtpCurricularDate.Value = dateTime;
                txtCurricularCode.Text = employeeCurricularModel.CurricularCode;
                txtCurricularName.Text = employeeCurricularModel.CurricularName;
                cboEmployee.EditValue = employeeCurricularModel.EmployeeID;
            }
        }
        void LoadEmployee()
        {
            DataTable list = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] {0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = list;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }
        bool SaveData()
        {
                if (!validate()) return false;
                employeeCurricularModel.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
                employeeCurricularModel.CurricularCode = txtCurricularCode.Text;
                employeeCurricularModel.CurricularName = txtCurricularName.Text;
                employeeCurricularModel.CurricularDay = dtpCurricularDate.Value.Day;
                employeeCurricularModel.CurricularMonth = dtpCurricularDate.Value.Month;
                employeeCurricularModel.CurricularYear = dtpCurricularDate.Value.Year;
                if (employeeCurricularModel.ID > 0)
                {
                    EmployeeCurricularBO.Instance.Update(employeeCurricularModel);
                }
                else
                {
                    EmployeeCurricularBO.Instance.Insert(employeeCurricularModel);
                }
   
            return true;
        }
        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                employeeCurricularModel = new EmployeeCurricularModel();
            }
            this.Controls.Clear();
            this.InitializeComponent();
        }
        bool validate()
        {
            int empID = Convert.ToInt32(cboEmployee.EditValue);
            object empName = cboEmployee.Properties.GetDisplayValueByKeyValue(cboEmployee.EditValue);
            string date = dtpCurricularDate.Value.ToString("dd-MM-yyyy");
            DataTable dt = TextUtils.Select($"select EmployeeID, CONCAT(FORMAT(CurricularDay, '00'), '-', FORMAT(CurricularMonth, '00'), '-', CurricularYear)  from EmployeeCurricular where EmployeeID = '{empID}' and CONCAT(FORMAT(CurricularDay, '00'), '-', FORMAT(CurricularMonth, '00'), '-', CurricularYear) = '{date}'");
            if(dt.Rows.Count > 0 && employeeCurricularModel.ID <= 0)
            {
                MessageBox.Show("Nhân viên " + empName + " đã đăng ký ngoại khóa ngày " + date + "!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (
                string.IsNullOrEmpty(txtCurricularCode.Text)
                || string.IsNullOrEmpty(txtCurricularName.Text)
                || string.IsNullOrEmpty(dtpCurricularDate.Value.ToString())
            )
            {
                MessageBox.Show("Vui lòng không để trống các trường thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void cboEmployee_EditValueChanged_1(object sender, EventArgs e)
        {

        }
    }
}