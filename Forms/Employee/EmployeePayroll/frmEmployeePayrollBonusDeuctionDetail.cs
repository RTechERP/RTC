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
    public partial class frmEmployeePayrollBonusDeuctionDetail : _Forms
    {
        public EmployeePayrollBonusDeuctionModel employee = new EmployeePayrollBonusDeuctionModel();
        public frmEmployeePayrollBonusDeuctionDetail()
        {
            InitializeComponent();
        }

        private void frmThuHoPhongBanDetail_Load(object sender, EventArgs e)
        {
            LoadEmployee();
            loadDetail();
        }
        private void loadDetail()
        {
            if (employee.ID > 0)
            {
                cbName.EditValue = TextUtils.ToInt(employee.EmployeeID);
                txtYear.Text = TextUtils.ToString(employee.YearValue);
                txtMonth.Text = TextUtils.ToString(employee.MonthValue);
                txtKPIBonus.Text = TextUtils.ToString(employee.KPIBonus);
                txtOtherBonus.Text = TextUtils.ToString(employee.OtherBonus);
                txtPankingCar.Text = TextUtils.ToString(employee.ParkingMoney);
                txtPunish5S.Text = TextUtils.ToString(employee.Punish5S);
                txtOtherDeduction.Text = TextUtils.ToString(employee.OtherDeduction);
                txtBHXH.Text = TextUtils.ToString(employee.BHXH);
                txtSalaryAdvance.Text = TextUtils.ToString(employee.SalaryAdvance);
                txtNote.Text = TextUtils.ToString(employee.Note);
            }
            else
            {
                txtYear.Value = DateTime.Now.Year;
                txtMonth.Value = DateTime.Now.Month;
            }
        }
        void LoadEmployee()
        {
            DataTable dt = TextUtils.Select($"Select ID ,Code ,FullName from Employee");
            cbName.Properties.DataSource = dt;
            cbName.Properties.DisplayMember = "FullName";
            cbName.Properties.ValueMember = "ID";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }
        bool saveData()
        {
            if (!ValidateForm()) return false;
            employee.EmployeeID = TextUtils.ToInt(cbName.EditValue);
            employee.YearValue = TextUtils.ToInt(txtYear.Value);
            employee.MonthValue = TextUtils.ToInt(txtMonth.Value);
            employee.KPIBonus = TextUtils.ToDecimal(txtKPIBonus.Text.Trim());
            employee.OtherBonus = TextUtils.ToDecimal(txtOtherBonus.Text.Trim());
            employee.ParkingMoney = TextUtils.ToDecimal(txtPankingCar.Text.Trim());
            employee.Punish5S = TextUtils.ToDecimal(txtPunish5S.Text.Trim());
            employee.OtherDeduction = TextUtils.ToDecimal(txtOtherDeduction.Text.Trim());
            employee.BHXH = TextUtils.ToDecimal(txtBHXH.Text.Trim());
            employee.SalaryAdvance = TextUtils.ToDecimal(txtSalaryAdvance.Text.Trim());
            employee.Note = TextUtils.ToString(txtNote.Text.Trim());
            if (employee.ID > 0)
            {
                EmployeePayrollBonusDeuctionBO.Instance.Update(employee);
            }
            else
            {
                employee.ID = (int)EmployeePayrollBonusDeuctionBO.Instance.Insert(employee);
            }
            return true;
        }
        bool ValidateForm()
        {
            if (cbName.Text == "")
            {
                MessageBox.Show("Vui lòng điền thông tin họ và tên. ", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (txtKPIBonus.Text == ""&& txtOtherBonus.Text == "" && txtPankingCar.Text == "" && txtPunish5S.Text == "" && txtOtherDeduction.Text == "")
            {
                MessageBox.Show("Bạn không thể để tất cả thông tin đều trống! ", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }

        private void txtKPIBonus_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
