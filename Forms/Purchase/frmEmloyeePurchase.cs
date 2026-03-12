using BMS.Business;
using BMS.Model;
using BMS.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syroot.Windows.IO;
using System.Diagnostics;
using System.IO;
using DevExpress.Utils;
using System.Net;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;


namespace BMS
{
    public partial class frmEmloyeePurchase : _Forms
    {
        public List<EmployeePurchaseModel> list = new List<EmployeePurchaseModel>();
        public frmEmloyeePurchase()
        {
            InitializeComponent();
        }
        private void frmEmloyeePurchase_Load(object sender, EventArgs e)
        {
            loadEmployee();
            loadTaxCompany();
            LoadData();
        }
        void LoadData()
        {
            string keyWord = txtFilterText.Text.Trim();
            int  empId = TextUtils.ToInt(cboEmployee.EditValue);
            int comID = TextUtils.ToInt(cboTaxCompany.EditValue);
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeePurchase", "A",
                                                    new string[] { "@Keyword", "@EmployeeID", "@TaxCompanyID" },
                                                    new object[] { keyWord, empId, comID }) ;
            grdData.DataSource = dt;
        }


        void loadEmployee()
        {
            DataTable list = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = list;

        }

        void loadTaxCompany()
        {
            List<TaxCompanyModel> list = SQLHelper<TaxCompanyModel>.FindAll().OrderByDescending(x => x.ID).ToList();
            cboTaxCompany.Properties.DisplayMember = "Name";
            cboTaxCompany.Properties.ValueMember = "ID";
            cboTaxCompany.Properties.DataSource = list;
        }

        //private void LoadDetail()
        //{
        //    int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
        //    List<EmployeePurchaseModel> list = SQLHelper<EmployeePurchaseModel>.FindByAttribute("ID", ID);
        //    grdData.DataSource = list;
        //}

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmEmloyeePurchaseDetail frm = new frmEmloyeePurchaseDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                //LoadDetail();
            }    
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            //EmployeePurchaseModel model = (EmployeePurchaseModel)EmployeePurchaseBO.Instance.FindByPK(ID);
            EmployeePurchaseModel model = SQLHelper<EmployeePurchaseModel>.FindByID(ID);
            frmEmloyeePurchaseDetail frm = new frmEmloyeePurchaseDetail();
            frm.employeepurchase = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                grvData.FocusedRowHandle = focusedRowHandle;
                //LoadDetail();
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colName));
            string companyName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCompany));

            string fullName = $"{code} - {name}";
            if(string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(name))
            {
                fullName = fullName.Replace("-","").Trim();
            }


            if (MessageBox.Show($"Bạn có muốn xóa thông tin nhân viên [{fullName}] của công ty [{companyName}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                EmployeePurchaseModel model = SQLHelper<EmployeePurchaseModel>.FindByID(ID);
                if(model.ID > 0)
                {
                    SQLHelper<EmployeePurchaseModel>.Delete(model);
                    LoadData();
                }    
            }
        }
        private void txtFilterText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadData();
            }
        }
        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            //LoadData();
        }

        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            btnEdit_Click(null, null);
        }
    }
}
