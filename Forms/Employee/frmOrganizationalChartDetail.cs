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
    public partial class frmOrganizationalChartDetail : _Forms
    {
        public Action SaveEvent;
        public OrganizationalChartModel organizational = new OrganizationalChartModel();
        public frmOrganizationalChartDetail()
        {
            InitializeComponent();
        }

        private void frmOrganizationalChartDetail_Load(object sender, EventArgs e)
        {
            LoadTaxCompany();
            LoadDepatment();
            LoadTeam();
            LoadEmployee();

            LoadData();
        }


        void LoadData()
        {
            cboTaxCompany.EditValue = organizational.TaxCompanyID;
            cboDepartment.EditValue = organizational.DepartmentID;
            txtCode.Text = organizational.Code;
            cboOrganizationalChart.EditValue = organizational.ParentID;
            txtName.Text = organizational.Name;
            cboEmployee.EditValue = organizational.EmployeeID;
            txtSTT.Value = organizational.STT ?? 0;
        }

        void LoadTaxCompany()
        {
            var list = SQLHelper<TaxCompanyModel>.FindAll();

            cboTaxCompany.Properties.ValueMember = "ID";
            cboTaxCompany.Properties.DisplayMember = "Name";
            cboTaxCompany.Properties.DataSource = list;
        }


        void LoadDepatment()
        {
            var list = SQLHelper<DepartmentModel>.FindAll();

            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.DataSource = list;
        }

        void LoadTeam()
        {
            int taxCompanyID = TextUtils.ToInt(cboTaxCompany.EditValue); ;
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            DataTable dt = TextUtils.LoadDataFromSP("spGetOrganizationalChart", "A",
                new string[] { "@TaxCompanyID", "@DepartmentID" },
                new object[] { taxCompanyID, departmentID });

            cboOrganizationalChart.Properties.ValueMember = "ID";
            cboOrganizationalChart.Properties.DisplayMember = "TeamName";
            cboOrganizationalChart.Properties.DataSource = dt;
        }

        void LoadEmployee()
        {
            //int taxCompanyID = TextUtils.ToInt(cboTaxCompany.EditValue); ;
            int departmentID = 0;
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A",
                new string[] { "@Status", "@DepartmentID" },
                new object[] { 0, departmentID });

            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;
        }


        bool CheckValidate()
        {
            string code = txtCode.Text.Trim();

            if (TextUtils.ToInt(cboTaxCompany.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Công ty!", "Thông báo");
                return false;
            }

            if (TextUtils.ToInt(cboDepartment.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Phòng ban!", "Thông báo");
                return false;
            }

            //if (string.IsNullOrWhiteSpace(code))
            //{
            //    MessageBox.Show("Vui lòng nhập Mã!", "Thông báo");
            //    return false;
            //}

            //if (string.IsNullOrWhiteSpace(txtName.Text.Trim()))
            //{
            //    MessageBox.Show("Vui lòng nhập Tên!", "Thông báo");
            //    return false;
            //}


            return true;
        }


        bool SaveData()
        {
            try
            {
                if (!CheckValidate()) return false;

                organizational.TaxCompanyID = TextUtils.ToInt(cboTaxCompany.EditValue);
                organizational.DepartmentID = TextUtils.ToInt(cboDepartment.EditValue);
                organizational.Code = txtCode.Text.Trim();
                organizational.ParentID = TextUtils.ToInt(cboOrganizationalChart.EditValue);
                organizational.Name = txtName.Text.Trim();
                organizational.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
                organizational.STT = TextUtils.ToInt(txtSTT.Value);

                if (organizational.ID <= 0) SQLHelper<OrganizationalChartModel>.Insert(organizational);
                else SQLHelper<OrganizationalChartModel>.Update(organizational);

                SaveEvent();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
                return false;
            }
        }

        private void btnSaveAndClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (SaveData())
            {
                this.Close();
            }
        }

        private void btnSaveAndNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (SaveData())
            {
                organizational = new OrganizationalChartModel();
                txtCode.Clear();
                txtName.Clear();
                cboEmployee.EditValue = 0;

                LoadTeam();
            }
        }
    }
}
