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
    public partial class frmAccountingBillDetails : _Forms
    {
        public AccountingBillModel accBill = new AccountingBillModel();
        List<AccountingBillApprovedModel> lstDeletedDetails = new List<AccountingBillApprovedModel>();
        public frmAccountingBillDetails()
        {
            InitializeComponent();
        }
        private void frmAccountingBillDetails_Load(object sender, EventArgs e)
        {
            //LoadPUR();
            LoadCurrency();
            LoadTaxCompany();
            LoadSuplierSale();
            LoadStatus();
            LoadData();
        }
        private void LoadCurrency()
        {
            List<CurrencyModel> lst = SQLHelper<CurrencyModel>.FindAll();
            cboCurrency.Properties.DataSource = lst;
            cboCurrency.Properties.DisplayMember = "Code";
            cboCurrency.Properties.ValueMember = "ID";
        }
        //private void LoadPUR()
        //{
        //    Expression ex1 = new Expression("DepartmentID", 4);
        //    Expression ex2 = new Expression("Status", 1, "<>");
        //    List<EmployeeModel> lst = SQLHelper<EmployeeModel>.FindByExpression(ex1.And(ex2));
        //    cboPur.Properties.DataSource = lst;
        //    cboPur.Properties.DisplayMember = "FullName";
        //    cboPur.Properties.ValueMember = "ID";
        //}
        private void LoadTaxCompany()
        {
            Expression ex2 = new Expression("IsDeleted", 1, "<>");
            List<TaxCompanyModel> lst = SQLHelper<TaxCompanyModel>.FindByExpression(ex2);
            cboTaxCompany.Properties.DataSource = lst;
            cboTaxCompany.Properties.DisplayMember = "Code";
            cboTaxCompany.Properties.ValueMember = "ID";
        }
        private void LoadSuplierSale()
        {
            Expression ex2 = new Expression("IsDeleted", 1, "<>");
            List<SupplierSaleModel> lst = SQLHelper<SupplierSaleModel>.FindByExpression(ex2).OrderByDescending(p => p.NgayUpdate).ToList();
            cboSuplierSale.Properties.DataSource = lst;
            cboSuplierSale.Properties.DisplayMember = "CodeNCC";
            cboSuplierSale.Properties.ValueMember = "ID";
        }
        private void LoadStatus()
        {
            List<object> lst = new List<object>()
            {
                new {ID = 0, Status = "Chờ xác nhận"},
                new {ID = 1, Status = "Đã xác nhận"},
                new {ID = 2, Status = "Hủy"},
            };
            cboPurStatus.DataSource = lst;
            cboPurStatus.DisplayMember = "Status";
            cboPurStatus.ValueMember = "ID";
            List<object> lstTax = new List<object>()
            {
                new {ID = 0, Status = "Chưa bàn giao"},
                new {ID = 1, Status = "Đã bàn giao"},
                new {ID = 2, Status = "Hủy"},
            };
            cboTaxStatus.DataSource = lstTax;
            cboTaxStatus.DisplayMember = "Status";
            cboTaxStatus.ValueMember = "ID";
            List<object> lstDocument = new List<object>()
            {
                new {ID = 0, Status = "Chưa hoàn thành"},
                new {ID = 1, Status = "Hoàn thành"},
                new {ID = 2, Status = "Hủy"},
            };

            cboStatusDocumentImport.DataSource = lstDocument;
            cboStatusDocumentImport.DisplayMember = "Status";
            cboStatusDocumentImport.ValueMember = "ID";
        }
        private void LoadDetails()
        {
            DataTable dt = SQLHelper<AccountingBillApprovedModel>.LoadDataFromSP("spGetAccoutingBillApproved", new string[] { "@AccountingBillID" }, new object[] { accBill.ID });
            grdData.DataSource = dt;
        }
        private void LoadData()
        {
            txtSupplyerSale.Text = accBill.SupplierSale;
            txtBillNumber.Text = accBill.BillNumber;
            txtTotalMoney.EditValue = accBill.TotalMoney;
            cboCurrency.EditValue = accBill.CurrencyID;
            //cboPur.EditValue = accBill.EmployeeID;
            cboPurStatus.SelectedValue = accBill.EmployeeStatus;
            dtpBillDate.Value = accBill.BillDate ?? DateTime.Now;
            cboTaxCompany.EditValue = accBill.TaxCompanyID;
            cboSuplierSale.EditValue = accBill.SupplierSaleID;
            cboTaxStatus.SelectedValue = accBill.DeliverTaxStatus;
            dtpDeliverTaxDate.Value = accBill.DeliverTaxDate ?? DateTime.Now;
            LoadDetails();
        }
        private bool SaveData()
        {
            if (!CheckValidate()) return false;
            AccountingBillModel newModel = SQLHelper<AccountingBillModel>.FindByID(accBill.ID);
            newModel.BillNumber = txtBillNumber.Text.Trim();
            newModel.TotalMoney = TextUtils.ToDecimal(txtTotalMoney.EditValue);
            newModel.CurrencyID = TextUtils.ToInt(cboCurrency.EditValue);
            //newModel.EmployeeID = TextUtils.ToInt(cboPur.EditValue);
            newModel.EmployeeStatus = TextUtils.ToInt(cboPurStatus.SelectedValue);
            newModel.BillDate = dtpBillDate.Value;
            newModel.TaxCompanyID = TextUtils.ToInt(cboTaxCompany.EditValue);
            newModel.SupplierSaleID = TextUtils.ToInt(cboSuplierSale.EditValue);
            newModel.DeliverTaxStatus = TextUtils.ToInt(cboTaxStatus.SelectedValue);
            newModel.DeliverTaxDate = dtpDeliverTaxDate.Value;
            newModel.SupplierSale = txtSupplyerSale.Text.Trim();
            if (newModel.ID > 0) SQLHelper<AccountingBillModel>.Update(newModel);
            else newModel.ID = SQLHelper<AccountingBillModel>.Insert(newModel).ID;

            if (lstDeletedDetails.Count > 0) SQLHelper<AccountingBillApprovedModel>.DeleteListModel(lstDeletedDetails);
            lstDeletedDetails = new List<AccountingBillApprovedModel>();
            for (int i = 0; i < grvData.RowCount; i++)
            {
                bool isCheck = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsCheck));
                if (!isCheck) continue;

                AccountingBillApprovedModel detail = SQLHelper<AccountingBillApprovedModel>.FindByID(TextUtils.ToInt(grvData.GetRowCellValue(i, colBillApprovedID)));
                detail.AccountingBillID = newModel.ID;
                detail.DocumentImportID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                detail.Status = TextUtils.ToInt(grvData.GetRowCellValue(i, colStatus));
                detail.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));

                if (detail.ID > 0) SQLHelper<AccountingBillApprovedModel>.Update(detail);
                else SQLHelper<AccountingBillApprovedModel>.Insert(detail);
            }


            return true;
        }
        private bool CheckValidate()
        {
            grvData.FocusedRowHandle = -1;
            if (string.IsNullOrWhiteSpace(txtBillNumber.Text))
            {
                MessageBox.Show("Vui lòng nhập Số hóa đơn!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (TextUtils.ToDecimal(txtTotalMoney.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Tổng số tiền!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (TextUtils.ToDecimal(cboCurrency.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Đơn vị tính!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            /*if (TextUtils.ToDecimal(cboPur.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Nhân viên PUR!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }*/
            if (TextUtils.ToDecimal(cboTaxCompany.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Công ty!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            for (int i = 0; i < grvData.RowCount; i++)
            {
                bool isCheck = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsCheck));
                if (!isCheck) continue;

                string code = TextUtils.ToString(grvData.GetRowCellValue(i, colDocumentImportCode));
                int status = TextUtils.ToInt(grvData.GetRowCellValue(i, colStatus));
                string note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));
                AccountingBillApprovedModel detail = SQLHelper<AccountingBillApprovedModel>.FindByID(TextUtils.ToInt(grvData.GetRowCellValue(i, colBillApprovedID)));
                bool isValid = string.IsNullOrWhiteSpace(note) || (detail.Note.Equals(note) && detail.Status != 2);
                if (status == 2 && isValid)
                {
                    MessageBox.Show(string.Format("Hãy nhập Lý do hủy cho Hồ sơ chứng từ [{0}]", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }

        private void ckbGrvData_EditValueChanged(object sender, EventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            grvData.FocusedRowHandle = -1;
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colDocumentImportCode));
            int detailID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colBillApprovedID));
            bool isCheck = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsCheck));
            AccountingBillApprovedModel detail = SQLHelper<AccountingBillApprovedModel>.FindByID(detailID);
            if (detail.Status > 0)
            {
                MessageBox.Show($"Hồ sơ chứng từ [{code}] đã được xác nhận! Không thể xóa", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                grvData.SetFocusedRowCellValue(colIsCheck, !isCheck);
                return;
            }
            if (!isCheck)
            {
                if (MessageBox.Show(string.Format("Bạn có chác chắn muốn xóa hoá đơn chứng từ [{0}] không ?", code), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (detailID > 0)
                    {
                        lstDeletedDetails.Add(detail);
                    }
                }
                else grvData.SetFocusedRowCellValue(colIsCheck, !isCheck);
                grvData.FocusedRowHandle = rowHandle;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                accBill = new AccountingBillModel();
                LoadData();
            }
        }

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            bool isCheck = TextUtils.ToBoolean(grvData.GetRowCellValue(e.RowHandle, colIsCheck));
            if (isCheck)
            {
                e.Appearance.BackColor = Color.LightYellow;
                e.Appearance.ForeColor = Color.Black;
            }
            e.HighPriority = true;
        }

        private void frmAccountingBillDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void cboSuplierSale_EditValueChanged(object sender, EventArgs e)
        {
            SupplierSaleModel supplierSale = (SupplierSaleModel)cboSuplierSale.GetSelectedDataRow();
            supplierSale = supplierSale ?? new SupplierSaleModel();
            txtSupplyerSale.Text = supplierSale.NameNCC;
        }
    }
}
