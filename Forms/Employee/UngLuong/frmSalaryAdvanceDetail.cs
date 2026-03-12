using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmSalaryAdvanceDetail : _Forms
    {
        public EmployeeSalaryAdvanceModel employeeSalary = new EmployeeSalaryAdvanceModel();
        public frmSalaryAdvanceDetail()
        {
            InitializeComponent();
        }

        private void frmSalaryAdvanceDetail_Load(object sender, EventArgs e)
        {
            LoadcboEmployee();
            LoadData();
        }
        void LoadData()
        {
            if(employeeSalary.ID > 0)
            {
                cboEmployee.EditValue = employeeSalary.EmployeeID;
                dtpDateRequest.Value = employeeSalary.DateRequest.HasValue == true ? employeeSalary.DateRequest.Value : DateTime.Now;
                txtMoney.EditValue = employeeSalary.Money;
                //dtpDatePayed.Value = employeeSalary.DatePayed.HasValue == true ? employeeSalary.DatePayed.Value : DateTime.Now;
                txtReason.Text = employeeSalary.Reason;
            }    
        }
        void LoadcboEmployee()
        {
            DataTable dtEmployee = TextUtils.Select("Select * from Employee WHERE Status <> 1");
            cboEmployee.Properties.DataSource = dtEmployee;
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.ValueMember = "ID";
        }
        bool ValidateForm()
        {
            if(cboEmployee.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập Họ và tên! "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                var ID = TextUtils.ExcuteScalar($"EXEC spGetEmployeeSalaryAdvanceByDate '{dtpDateRequest.Value.ToString("yyyy-MM-dd")}',{TextUtils.ToInt(cboEmployee.EditValue)},{employeeSalary.ID}");   
                if(TextUtils.ToInt(ID) > 0)
                {
                    MessageBox.Show(string.Format($"Nhân viên [{cboEmployee.Text}] đã ứng tiền trong ngày {dtpDateRequest.Value.ToString("dd/MM/yyyy")}! "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }    
            }
            if (string.IsNullOrEmpty(txtMoney.Text.Trim()))
            {
                MessageBox.Show(string.Format("Vui lòng nhập Mức cần ứng! "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if(txtReason.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập Lý do! "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }    
            return true;
        }
        bool saveData()
        {
            if (!ValidateForm()) return false;

            employeeSalary.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
            employeeSalary.DateRequest = new DateTime (dtpDateRequest.Value.Year, dtpDateRequest.Value.Month, dtpDateRequest.Value.Day);
            employeeSalary.Money = TextUtils.ToDecimal(txtMoney.EditValue);
            //employeeSalary.DatePayed = new DateTime(dtpDatePayed.Value.Year, dtpDatePayed.Value.Month, dtpDatePayed.Value.Day);
            employeeSalary.Reason = txtReason.Text.Trim();
            //employeeSalary.ApprovedTP = 0;
            //employeeSalary.ApprovedKT = 0;
            //employeeSalary.ApprovedHR = 0;

            if (employeeSalary.ID > 0)
            {
                EmployeeSalaryAdvanceBO.Instance.Update(employeeSalary);
            }
            else
            {
                employeeSalary.ID = (int)EmployeeSalaryAdvanceBO.Instance.Insert(employeeSalary);
            }    
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(saveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
