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
    public partial class frmPayrollDetail : _Forms
    {
         public EmployeePayrollModel payrollMasterModel = new EmployeePayrollModel();
        public frmPayrollDetail()
        {
            InitializeComponent();
        }

        private void frmPayrollDetail_Load(object sender, EventArgs e)
        {
            txtYear.Value = DateTime.Now.Year;
            txtMonth.Value = DateTime.Now.Month;

            loadData();
        }

        void loadData()
        {
            if (payrollMasterModel.ID > 0)
            {
                txtMonth.Value = payrollMasterModel._Month;
                txtYear.Value = payrollMasterModel._Year;
                txtName.Text = payrollMasterModel.Name;
                txtNote.Text = payrollMasterModel.Note;
            }
            else
            {
                txtName.Text = $"Bảng lương tháng {txtMonth.Value}/{txtYear.Value}";
            }
        }
        bool validateForm()
        {
            if (txtMonth.Text == "")
            {
                MessageBox.Show("Vui lòng nhập Tháng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            else if (txtYear.Text == "")
            {
                MessageBox.Show("Vui lòng nhập Năm!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                DataTable dt = TextUtils.Select($"SELECT TOP 1 ID FROM dbo.EmployeePayroll WHERE [_Year] = {txtYear.Value} AND [_Month] = {txtMonth.Value} AND ID <> {payrollMasterModel.ID}");
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show($"Bảng lương tháng {txtMonth.Value} năm {txtYear.Value} đã tồn tại.\nVui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Tên bảng lương!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        bool save()
        {
            if (validateForm())
            {
                payrollMasterModel._Month = TextUtils.ToInt(txtMonth.Value);
                payrollMasterModel._Year = TextUtils.ToInt(txtYear.Value);
                payrollMasterModel.Name = TextUtils.ToString(txtName.Text);
                payrollMasterModel.Note = TextUtils.ToString(txtNote.Text);

                if (payrollMasterModel.ID > 0)
                {
                    payrollMasterModel.ID = payrollMasterModel.ID;
                    EmployeePayrollBO.Instance.Update(payrollMasterModel);
                }
                else
                {
                    EmployeePayrollBO.Instance.Insert(payrollMasterModel);
                }
                return true;
            }
            else return false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (save())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void txtMonth_ValueChanged(object sender, EventArgs e)
        {
            txtName.Text = $"Bảng lương tháng {txtMonth.Value}/{txtYear.Value}";
        }

        private void txtYear_ValueChanged(object sender, EventArgs e)
        {
            txtName.Text = $"Bảng lương tháng {txtMonth.Value}/{txtYear.Value}";
        }
    }
}
