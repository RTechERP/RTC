using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMS.Model;
using BMS.Business;
using BMS.BO;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace BMS
{
    public partial class frmEmployeePOcontactDetail : _Forms
    {
        public EmployeePOContactModel model = new EmployeePOContactModel();
        public frmEmployeePOcontactDetail()
        {
            InitializeComponent();
        }
        private void frmEmployeePOcontactDetail_Load(object sender, EventArgs e)
        {
            LoadEmployee();
            LoadForm();
        }
        void LoadEmployee()
        {
            DataTable dtType = TextUtils.Select("Select ID,Code,FullName from Users");
            cboEmployee.Properties.DataSource = dtType;
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.ValueMember = "ID";
        }
        void LoadForm()
        {
            cboEmployee.EditValue = model.EmployeeID;
            txtSDT.Text = model.Phone;
            txtEmail.Text = model.Email;
            cbbComCode.SelectedIndex = model.ComCode;
        }
        bool validateForm()
        {
            //Regex regex = new Regex(@"\d");
            //Regex regexEmail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            if (cboEmployee.Text == "")
            {
                MessageBox.Show("Nhân viên không được để trống. Vui lòng chọn 1 nhân viên!", TextUtils.Caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return false;
            }
            else if (txtSDT.Text.Trim() == "")
            {
                MessageBox.Show("SĐT không được để trống. Vui lòng nhập SĐT!", TextUtils.Caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return false;
            }
            //else if (!regex.IsMatch(txtSDT.Text.Trim()))
            //{
            //    MessageBox.Show("SĐT không đúng định dạng. Vui lòng chỉ nhập số!", TextUtils.Caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            //    return false;
            //}
            else if (txtEmail.Text.Trim() == "")
            {
                MessageBox.Show("Email không được để trống. Vui lòng nhập Email!", TextUtils.Caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return false;
            }
            //else if (!regexEmail.IsMatch(txtEmail.Text.Trim()))
            //{
            //    MessageBox.Show("Email không đúng định dạng. Vui lòng nhập đúng email (VD: abc@gmail.com) !", TextUtils.Caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            //    return false;
            //}
            else if (cbbComCode.Text == "")
            {
                MessageBox.Show("Mã công ty không được để trống. Vui lòng chọn 1 mã công ty!", TextUtils.Caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return false;
            }
            else
                return true;
        }
        bool save()
        {
            if (!validateForm()) return false;
            DataTable dt = TextUtils.Select($"Select * from EmployeePOContact where EmployeeID={cboEmployee.EditValue} and ComCode={cbbComCode.SelectedIndex}");
            if (model.ID <= 0 && dt.Rows.Count >= 1)
            {
                MessageBox.Show($"Nhân viên này đã có thông tin trong công ty {cbbComCode.Text}", TextUtils.Caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return false;
            }
            model.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
            model.Phone = txtSDT.Text;
            model.Email = txtEmail.Text;
            model.ComCode = cbbComCode.SelectedIndex;
            if (model.ID > 0)
            {
                EmployeePOContactBO.Instance.Update(model);
            }
            else
                EmployeePOContactBO.Instance.Insert(model);
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (save())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSave_New_Click(object sender, EventArgs e)
        {
            if (save())
            {
                txtEmail.Text = "";
                txtSDT.Text = "";
                model.ID = -1;
            }
        }

        private void frmEmployeePOcontactDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
