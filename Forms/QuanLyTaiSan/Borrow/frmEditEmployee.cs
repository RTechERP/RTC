using BMS.Business;
using BMS.Model;
using DevExpress.XtraEditors;
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
    public partial class frmEditEmployee : _Forms
    {
        public int EmployeeID;
        public string TSAssetCode;
        public string TSAssetName;
        public List<TSAssetManagementHistoryModel> lst = new List<TSAssetManagementHistoryModel>();
        public frmEditEmployee()
        {
            InitializeComponent();
        }

        private void frmEditEmployee_Load(object sender, EventArgs e)
        {
            LoadEmployee();
            LoadData();
        }
        void LoadEmployee()
        {
            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetEmployeeAndEmployeeApprover", new string[] { }, new object[] { });
            cboNewEmployee.Properties.DataSource = dataSet.Tables[0];
            cboNewEmployee.Properties.DisplayMember = "FullName";
            cboNewEmployee.Properties.ValueMember = "ID";

            cboOldEmployee.DataSource = dataSet.Tables[0];
            cboOldEmployee.DisplayMember = "FullName";
            cboOldEmployee.ValueMember = "ID";
        }
        void LoadData()
        {
            txtName.Text = TSAssetName;
            txtCode.Text = TSAssetCode;
            cboOldEmployee.SelectedValue = EmployeeID;
        }

        bool SaveData()
        {
            int oldEmployee = TextUtils.ToInt(cboOldEmployee.SelectedValue);
            int newEmployee = TextUtils.ToInt(cboNewEmployee.EditValue);
            if (newEmployee <= 0)
            {
                MessageBox.Show("Vui lòng chọn Người mượn mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (oldEmployee == newEmployee)
            {
                MessageBox.Show("Vui lòng chọn Người mượn mới khác Người mượn cũ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtReason.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập Lý do đổi người!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            foreach (var item in lst)
            {
                item.EmployeeID = newEmployee;
                item.Reason = txtReason.Text;
                item.Note = txtNote.Text;
                if(item.ID > 0)
                {
                    TSAssetManagementHistoryBO.Instance.Update(item);
                }
            }
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
                this.DialogResult = DialogResult.OK;
        }
    }
}