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
    public partial class frmErrorDetail : _Forms
    {
        public EmployeeErrorModel error = new EmployeeErrorModel();
        public frmErrorDetail()
        {
            InitializeComponent();
        }

        private void frmErrorDetail_Load(object sender, EventArgs e)
        {
            loadcboName();
            LoadErrorDetail();
        }
        void loadcboName()
        {
            DataTable dt = TextUtils.Select("Select * from dbo.Employee");
            cboName.Properties.DataSource = dt;
            cboName.Properties.DisplayMember = "FullName";
            cboName.Properties.ValueMember = "ID";
        }
        void LoadErrorDetail()
        {
            if (error.ID > 0)
            {
                cboName.EditValue = TextUtils.ToInt(error.EmployeeID);
                txtMoney.Text = TextUtils.ToString(error.Money);
                dtpDateError.Value = TextUtils.ToDate5(error.DateError);
                txtNote.Text = error.Note;
            }
        }
        public bool ValidateForm()
        {
            if(cboName.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập vào tên !"),TextUtils.Caption, MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
            if (txtMoney.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập vào số tiền !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        void saveData()
        {
            if (!ValidateForm()) return;

            error.EmployeeID = TextUtils.ToInt(cboName.EditValue);
            error.Money = TextUtils.ToDecimal(txtMoney.Text.Trim());
            error.DateError = dtpDateError.Value;
            error.Note = txtNote.Text.Trim();

            if (error.ID > 0)
            {
                EmployeeErrorBO.Instance.Update(error);
            }
            else
                error.ID = (int)EmployeeErrorBO.Instance.Insert(error);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            saveData();
            this.DialogResult = DialogResult.OK;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
