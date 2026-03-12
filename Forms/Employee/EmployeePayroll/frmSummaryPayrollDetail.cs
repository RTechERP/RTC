using BMS;
using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmSummaryPayrollDetail : _Forms
    {
        public EmployeePayrollDetailModel employeePayroll = new EmployeePayrollDetailModel();
        public int payrollID;
        public frmSummaryPayrollDetail()
        {
            InitializeComponent();
        }

        private void frmSummaryPayrollDetail_Load(object sender, EventArgs e)
        {
            cboEmployee.EditValue = employeePayroll.EmployeeID;
            loadEmployee();
            loadData();
        }

        void loadData()
        {
            //employeeID = TextUtils.ToInt(cboEmployee.EditValue);

            //EmployeePayrollDetailModel employeePayroll = (EmployeePayrollDetailModel)EmployeePayrollDetailBO.Instance.FindByPK(id);

            if (employeePayroll.ID <= 0)
            {
                MessageBox.Show($"Nhân viên [{cboEmployee.Text}] chưa có bảng lương.\nVui lòng kiểm tra lại!", "Thông báo");
                return;
            }

            txtTotalWorkday.EditValue = employeePayroll.TotalWorkday;
            txtBonus.EditValue = employeePayroll.Bonus;
            txtOther.EditValue = employeePayroll.Other;

            txtParkingMoney.EditValue = employeePayroll.ParkingMoney;
            txtOtherDeduction.EditValue = employeePayroll.OtherDeduction;

            txtNote.Text = employeePayroll.Note;
        }

        void loadEmployee()
        {
            //DataTable dt = TextUtils.Select("SELECT ID, Code, FullName FROM Employee");
            //DataTable dt = TextUtils.GetTable("spGetEmployeeIDPayrollDetail");

            List<EmployeeModel> listData = SQLHelper<EmployeeModel>.SqlToList("SELECT ID, Code, FullName FROM Employee");

            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DataSource = listData;
        }


        /// <summary>
        /// Check validate form
        /// </summary>
        /// <returns></returns>
        bool validate()
        {
            if (TextUtils.ToInt(cboEmployee.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }


        bool saveData()
        {
            if (!validate())
            {
                return false;
            }

            //EmployeePayrollDetailModel employeePayroll = (EmployeePayrollDetailModel)EmployeePayrollDetailBO.Instance.FindByPK(id);

            //int employeeId = TextUtils.ToInt(cboEmployee.EditValue);
            //int payrollID = payroll.PayrollID;
            //decimal bonus = TextUtils.ToDecimal(txtBonus.EditValue);
            //decimal other = TextUtils.ToDecimal(txtOther.EditValue);



            //Tính lại tổng các khoản cộng
            employeePayroll.Bonus = TextUtils.ToDecimal(txtBonus.EditValue);
            employeePayroll.Other = TextUtils.ToDecimal(txtOther.EditValue);

            //decimal totalBonus = employeePayroll.BussinessMoney + employeePayroll.NightShiftMoney + employeePayroll.CostVehicleBussiness + bonus + other;

            //Tính tổng thu nhập thực tế
            employeePayroll.RealSalary = employeePayroll.RealSalary + employeePayroll.Bonus + employeePayroll.Other;

            //Tính tổng các khoản trừ
            employeePayroll.ParkingMoney = TextUtils.ToDecimal(txtParkingMoney.EditValue);
            employeePayroll.OtherDeduction = TextUtils.ToDecimal(txtOtherDeduction.EditValue);

            decimal totalDeduction = employeePayroll.Insurances + employeePayroll.UnionFees + employeePayroll.AdvancePayment + employeePayroll.DepartmentalFees + employeePayroll.ParkingMoney + employeePayroll.Punish5S + employeePayroll.OtherDeduction;

            //Tính thực lĩnh
            employeePayroll.ActualAmountReceived = employeePayroll.RealSalary - totalDeduction;

            employeePayroll.Note = txtNote.Text.Trim();

            EmployeePayrollDetailBO.Instance.Update(employeePayroll);

            //TextUtils.ExcuteProcedure("spUpdateEmployeePayrollDetail",
            //            new string[] { "@EmployeeID", "@PayrollID", "@Bonus", "@Other"},
            //            new object[] { employeeId, payrollID, bonus, other });

            return true;
        }

        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            //if (saveData())
            //{
            //    cboEmployee.EditValue = 0;

            //    payroll = new EmployeePayrollDetailModel();
            //}
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            employeePayroll = SQLHelper<EmployeePayrollDetailModel>
                .SqlToModel($"SELECT * FROM dbo.EmployeePayrollDetail WHERE PayrollID = {payrollID} AND EmployeeID = {TextUtils.ToInt(cboEmployee.EditValue)}");

            loadData();
        }
    }
}
