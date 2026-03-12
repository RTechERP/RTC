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

namespace BMS
{
    public partial class frmAccountingBill : _Forms
    {
        public frmAccountingBill()
        {
            InitializeComponent();
        }
        private void frmAccountingBill_Load(object sender, EventArgs e)
        {
            dtpDateStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDateEnd.Value = dtpDateStart.Value.AddMonths(+1).AddSeconds(-1);
            LoadPUR();
            LoadSuplierSale();
            LoadTaxCompany();
            LoadStatus();
            LoadData();
        }
        private void LoadPUR()
        {
            Expression ex1 = new Expression("DepartmentID", 4);
            Expression ex2 = new Expression("Status", 1, "<>");
            List<EmployeeModel> lst = SQLHelper<EmployeeModel>.FindByExpression(ex1.And(ex2));
            lst.Insert(0, new EmployeeModel()
            {
                ID = 0,
                FullName = "---Tất cả---"
            });
            cboPur.Properties.DataSource = lst;
            cboPur.Properties.DisplayMember = "FullName";
            cboPur.Properties.ValueMember = "ID";
        }
        private void LoadTaxCompany()
        {
            Expression ex2 = new Expression("IsDeleted", 1, "<>");
            List<TaxCompanyModel> lst = SQLHelper<TaxCompanyModel>.FindByExpression(ex2);
            lst.Insert(0, new TaxCompanyModel()
            {
                ID = 0,
                Code = "---Tất cả---"
            });
            cboTaxCompany.Properties.DataSource = lst;
            cboTaxCompany.Properties.DisplayMember = "Code";
            cboTaxCompany.Properties.ValueMember = "ID";
        }
        private void LoadSuplierSale()
        {
            Expression ex2 = new Expression("IsDeleted", 1, "<>");
            List<SupplierSaleModel> lst = SQLHelper<SupplierSaleModel>.FindByExpression(ex2).OrderByDescending(p => p.NgayUpdate).ToList();
            lst.Insert(0, new SupplierSaleModel()
            {
                ID = 0,
                NameNCC = "---Tất cả---"
            });
            cboSuplierSale.Properties.DataSource = lst;
            cboSuplierSale.Properties.DisplayMember = "NameNCC";
            cboSuplierSale.Properties.ValueMember = "ID";
            cboSuplierSale.EditValue = 0;
        }
        private void LoadStatus()
        {
            List<object> lst = new List<object>()
            {
                new {ID = -1, Status = "---Tất cả---"},
                new {ID = 0, Status = "Chờ xác nhận"},
                new {ID = 1, Status = "Đã xác nhận"},
                new {ID = 2, Status = "Hủy"},
            };
            cboPurStatus.DataSource = lst;
            cboPurStatus.DisplayMember = "Status";
            cboPurStatus.ValueMember = "ID";
        }
        private void LoadData()
        {
            DateTime startDate = dtpDateStart.Value;
            DateTime endDate = dtpDateEnd.Value;
            string keyWords = txtKeyword.Text.Trim();
            int supplierSaleId = TextUtils.ToInt(cboSuplierSale.EditValue);
            int purID = TextUtils.ToInt(cboPur.EditValue);
            int purApproved = TextUtils.ToInt(cboPurStatus.SelectedValue);
            int taxCompanyID = TextUtils.ToInt(cboTaxCompany.EditValue);
            DataTable dt = SQLHelper<SupplierSaleModel>.LoadDataFromSP("spGetAllAccountingBill",
                                                                        new string[] { "@startDate", "@endDate", "@keyWords", "@supplierSaleId", "@purID", "@purApproved", "@taxCompanyID" },
                                                                        new object[] { startDate, endDate, keyWords, supplierSaleId , purID, purApproved, taxCompanyID });
            grdData.DataSource = dt;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            frmAccountingBillDetails frm = new frmAccountingBillDetails();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                btnFind_Click(null, null);
                grvData.FocusedRowHandle = rowHandle;
            };
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            AccountingBillModel model = SQLHelper<AccountingBillModel>.FindByID(ID);
            frmAccountingBillDetails frm = new frmAccountingBillDetails();
            if(model.ID > 0)
            {
                frm.accBill = model;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    btnFind_Click(null, null);
                    grvData.FocusedRowHandle = rowHandle;
                };

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colBillNumber));
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (MessageBox.Show(string.Format("Bạn có chác chắn muốn xóa hoá đơn thanh toán [{0}] không ?", code), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                AccountingBillModel bill = SQLHelper<AccountingBillModel>.FindByID(ID);
                if (bill.ID <= 0) return;

                bill.IsDeleted = true;
                SQLHelper<AccountingBillModel>.Update(bill);
                btnFind_Click(null, null);
            }
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnBillImportDetailsView_Click(object sender, EventArgs e)
        {
            frmBillImportDetailsAccouting frm = new frmBillImportDetailsAccouting();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            frmAccountingBillImportExcel frm = new frmAccountingBillImportExcel();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }
    }
}
