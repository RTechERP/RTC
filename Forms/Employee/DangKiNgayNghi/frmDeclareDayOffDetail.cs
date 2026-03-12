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
    public partial class frmDeclareDayOffDetail : _Forms
    {
        public EmployeeOnLeaveMasterModel model = new EmployeeOnLeaveMasterModel();
        public frmDeclareDayOffDetail()
        {
            InitializeComponent();
        }

        private void frmDeclareDayOffDetail_Load(object sender, EventArgs e)
        {
            txtYear.Value = DateTime.Now.Year;
            loadEmployee();
            loadData();
        }
        void loadEmployee()
        {
            DataTable dt = TextUtils.Select("select ID,Code,FullName from Employee ");
            cboEmployee.Properties.DataSource = dt;
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.ValueMember = "ID";
        }
        void loadData()
        {
            if (model.ID > 0)
            {
                cboEmployee.EditValue = model.EmployeeID;
                txtTotalDayInYear.Text = TextUtils.ToString(model.TotalDayInYear);
                txtTotalDayNoOnLeave.Text = TextUtils.ToString(model.TotalDayNoOnLeave);
                txtTotalDayOnLeave.Text = TextUtils.ToString(model.TotalDayOnLeave);
                txtTotalDayRemain.Text = TextUtils.ToString(model.TotalDayRemain);
                txtYear.Text = TextUtils.ToString(model.YearOnleave);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (save())
                this.DialogResult = DialogResult.OK;
        }
        bool save()
        {
            if (!Validate()) return false;
            model.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
            model.TotalDayInYear = TextUtils.ToDecimal(txtTotalDayInYear.Text);
            model.TotalDayNoOnLeave = TextUtils.ToDecimal(txtTotalDayNoOnLeave.Text);
            model.TotalDayOnLeave = TextUtils.ToDecimal(txtTotalDayOnLeave.Text);
            model.TotalDayRemain = TextUtils.ToDecimal(txtTotalDayRemain.Text);
            model.YearOnleave = TextUtils.ToInt(txtYear.Text);
            if (model.ID > 0)
                EmployeeOnLeaveMasterBO.Instance.Update(model);
            else
                EmployeeOnLeaveMasterBO.Instance.Insert(model);
            return true;
        }
        bool Validate()
        {
            if (cboEmployee.Text == "")
            {
                MessageBox.Show("Vui lòng nhập Họ và tên!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                DataTable employee = TextUtils.LoadDataFromSP("spGetCheckDeclareDayOff", "A",new string[] { "@EmployeeID", "ID", "@Year" }, new object[] { cboEmployee.EditValue, model.ID, txtYear.Value });

                if(employee.Rows.Count > 0)
                {
                    MessageBox.Show($"Nhân viên này đã tồn tại trong năm {txtYear.Value} !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }      
            }
            if (txtTotalDayInYear.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tổng số ngày phép!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            //if (txtTotalDayNoOnLeave.Text == "")
            //{
            //    MessageBox.Show("Vui lòng nhập tổng số ngày nghỉ không phép!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}
            //if (txtTotalDayOnLeave.Text == "")
            //{
            //    MessageBox.Show("Vui lòng nhập tổng số ngày phép đã nghỉ!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}
            return true;
        }

        private void txtTotalDayOnLeave_TextChanged(object sender, EventArgs e)
        {
            decimal TotalDayOnLeave = TextUtils.ToDecimal(txtTotalDayOnLeave.Text);
            decimal TotalDayInYear = TextUtils.ToDecimal(txtTotalDayInYear.Text);
            if (TotalDayInYear != 0)
            {
                decimal TotalDayRemain = TotalDayInYear - TotalDayOnLeave;
                txtTotalDayRemain.Text= TextUtils.ToString(TotalDayRemain);
            }
        }

        private void btnSaveAndNew_Click(object sender, EventArgs e)
        {
            if (save())
            {
                cboEmployee.EditValue = "";
                txtTotalDayInYear.Text = "";
                txtTotalDayNoOnLeave.Text = "";
                txtTotalDayOnLeave.Text = "";
                txtTotalDayRemain.Text = "";
            }    
        }

        private void frmDeclareDayOffDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
