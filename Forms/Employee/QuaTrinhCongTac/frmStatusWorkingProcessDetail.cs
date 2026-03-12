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
    public partial class frmStatusWorkingProcessDetail : _Forms
    {
        public EmployeeStatusModel status = new EmployeeStatusModel();
        public frmStatusWorkingProcessDetail()
        {
            InitializeComponent();
        }

        private void frmStatusWorkingProcessDetail_Load(object sender, EventArgs e)
        {
            LoadStatus();
        }
        void LoadStatus()
        {
            txtCode.Text = status.StatusCode;
            txtName.Text = status.StatusName;
        }
        public bool validateForm()
        {
            if(txtCode.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập mã trạng thái."),TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtName.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập tên trạng thái."), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        void saveData()
        {
            if (!validateForm()) return;

            status.StatusCode = txtCode.Text.Trim();
            status.StatusName = txtName.Text.Trim();

            if (status.ID > 0)
            {
                EmployeeStatusBO.Instance.Update(status);
            }
            else
                status.ID = (int)EmployeeStatusBO.Instance.Insert(status);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveData();
            this.DialogResult = DialogResult.OK;
        }
    }
}
