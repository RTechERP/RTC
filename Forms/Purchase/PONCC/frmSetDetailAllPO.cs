using BaseBusiness.DTO;
using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Forms.Classes;
using Forms.Technical;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmSetDetailAllPO : _Forms
    {
        public PONCCHistoryModel poh = new PONCCHistoryModel();
        public frmSetDetailAllPO()
        {
            InitializeComponent();
        }
        private void frmSetDetailAllPO_Load(object sender, EventArgs e)
        {
            LoadCurrency();
            loadEmployee();
            LoadSupplierSale();
            loadUnit();
            LoadData();
        }

        void LoadSupplierSale()
        {
            List<SupplierSaleModel> list = SQLHelper<SupplierSaleModel>.FindAll().ToList();
            var supplierSale = list.Where(i => i.NameNCC == poh.NameNCC).FirstOrDefault();
            cboSupplierSale.Properties.ValueMember = "ID";
            cboSupplierSale.Properties.DisplayMember = "NameNCC";
            cboSupplierSale.Properties.DataSource = list;
            if (supplierSale != null) cboSupplierSale.EditValue = supplierSale.ID;
            else cboSupplierSale.EditValue = -1;
        }

        void LoadCurrency()
        {
            List<CurrencyModel> list = SQLHelper<CurrencyModel>.FindAll();
            var currencyName = list.Where(i => i.Code == poh.CurrencyName).FirstOrDefault();
            cboCurrencyName.Properties.ValueMember = "ID";
            cboCurrencyName.Properties.DisplayMember = "Code";
            cboCurrencyName.Properties.DataSource = list;
            if (currencyName != null)
            {
                cboCurrencyName.EditValue = currencyName.ID;
                txtCurrencyRate.Text = TextUtils.ToString(poh.CurrencyRate);
            }
            else
            {
                cboCurrencyName.EditValue = -1;
                txtCurrencyRate.Text = "";
            }


        }

        void loadEmployee()
        {
            List<EmployeeModel> list = SQLHelper<EmployeeModel>.FindAll();
            var employee = list.Where(i => i.FullName == poh.FullName).FirstOrDefault();
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = list;
            if (employee != null) cboEmployee.EditValue = employee.ID;
            else cboEmployee.EditValue = -1;


        }

        private void loadUnit()
        {
            List<UnitCountModel> list = SQLHelper<UnitCountModel>.FindAll();
            var units = list.Where(i => i.UnitName == poh.Unit).FirstOrDefault();
            cboUnits.Properties.ValueMember = "ID";
            cboUnits.Properties.DisplayMember = "UnitName";
            cboUnits.Properties.DataSource = list;
            if (units != null) cboUnits.EditValue = units.ID;
            else cboUnits.EditValue = -1;
        }

        private void LoadData()
        {
            if (poh.ID > 0)
            {
                cboCompany.SelectedItem = TextUtils.ToString(poh.CompanyText);
                txtProjectCode.Text = TextUtils.ToString(poh.ProjectCode);
                txtProjectName.Text = TextUtils.ToString(poh.ProjectName);
                txtProductNewCode.Text = TextUtils.ToString(poh.ProductNewCode);
                txtProductCode.Text = TextUtils.ToString(poh.ProductCode);
                txtProductCodeOfSupplier.Text = TextUtils.ToString(poh.ProductCodeOfSupplier);
                txtTotalMoneyChangePO.Text = TextUtils.ToString(poh.TotalMoneyChangePO);
                txtTotalPrice.Text = TextUtils.ToString(poh.TotalPrice);
                txtFeeShip.Text = TextUtils.ToString(poh.FeeShip);
                txtPriceSale.Text = TextUtils.ToString(poh.PriceSale);
                txtPriceHistory.Text = TextUtils.ToString(poh.PriceHistory);
                txtBiddingPrice.Text = TextUtils.ToString(poh.BiddingPrice);
                txtVat.Text = TextUtils.ToString(poh.VAT);
                txtUnitPrice.Text = TextUtils.ToString(poh.UnitPrice);
                txtUnitPriceVat.Text = TextUtils.ToString(poh.UnitPriceVAT);
                txtSupplierVoucher.Text = TextUtils.ToString(poh.SupplierVoucher);
                cbDeptSupplier.Checked = TextUtils.ToBoolean(poh.DeptSupplier);
                cbNCCNew.Checked = TextUtils.ToBoolean(poh.NCCNew);
                txtQuantityOrder.Text = TextUtils.ToString(poh.QtyRequest);
                txtQuantityReturn.Text = TextUtils.ToString(poh.QuantityReturn);
                txtQuantityRemain.Text = TextUtils.ToString(poh.QuantityRemain);
                txtMinQuantity.Text = TextUtils.ToString(poh.MinQuantity);
                txtBillCode.Text = TextUtils.ToString(poh.BillCode);
                cboStatus.SelectedItem = TextUtils.ToString(poh.StatusText);
                txtPOCode.Text = TextUtils.ToString(poh.POCode);
                if (poh.RequestDate == null) dtpRequestDate.EditValue = "";
                else dtpRequestDate.EditValue = poh.RequestDate.Value;
                if (poh.DeliveryDate == null) dtpDeliveryDate.EditValue = "";
                else dtpDeliveryDate.EditValue = poh.DeliveryDate.Value;
                if (poh.DeadlineDelivery == null) dtpDatelineShip.EditValue = "";
                else dtpDatelineShip.EditValue = poh.DeadlineDelivery.Value;
                txtTotalQuantityLast.Text = TextUtils.ToString(poh.TotalQuantityLast);
                txtProductName.Text = TextUtils.ToString(poh.ProductName);
                txtNote.Text = TextUtils.ToString(poh.Note);
            }
            else
            {
                Reset();
            }

        }

        void Reset()
        {
            txtProjectCode.Text = txtProjectName.Text = txtProductNewCode.Text = txtProductCode.Text = txtProductCodeOfSupplier.Text =
            txtTotalMoneyChangePO.Text = txtTotalPrice.Text = txtFeeShip.Text = txtPriceSale.Text = txtPriceHistory.Text =
            txtBiddingPrice.Text = txtVat.Text = txtUnitPrice.Text = txtUnitPriceVat.Text =
            txtSupplierVoucher.Text = txtQuantityOrder.Text = txtQuantityReturn.Text = txtQuantityRemain.Text =
            txtMinQuantity.Text = txtBillCode.Text = txtPOCode.Text =
            txtCurrencyRate.Text = txtTotalQuantityLast.Text = txtProductName.Text = txtNote.Text = null;

            cboUnits.EditValue = -1;
            cboCompany.SelectedIndex = -1;
            cboSupplierSale.EditValue = -1;
            cboCurrencyName.EditValue = -1;
            cboEmployee.EditValue = -1;
            cboStatus.SelectedIndex = -1;

            dtpRequestDate.EditValue = null;
            dtpDeliveryDate.EditValue = null;
            dtpDatelineShip.EditValue = null;
            cbDeptSupplier.Checked = false;
            cbNCCNew.Checked = false;
        }

        bool SaveData()
        {
            try
            {
                poh.ProjectCode = TextUtils.ToString(txtProjectCode.Text);
                poh.ProjectName = TextUtils.ToString(txtProjectName.Text);
                poh.ProductNewCode = TextUtils.ToString(txtProductNewCode.Text);
                poh.ProductCode = TextUtils.ToString(txtProductCode.Text);
                poh.ProductCodeOfSupplier = TextUtils.ToString(txtProductCodeOfSupplier.Text);
                poh.TotalMoneyChangePO = TextUtils.ToDecimal(txtTotalMoneyChangePO.Text);
                poh.TotalPrice = TextUtils.ToDecimal(txtTotalPrice.Text);
                poh.FeeShip = TextUtils.ToDecimal(txtFeeShip.Text);
                poh.PriceSale = TextUtils.ToDecimal(txtPriceSale.Text);
                poh.PriceHistory = TextUtils.ToDecimal(txtPriceHistory.Text);
                poh.BiddingPrice = TextUtils.ToDecimal(txtBiddingPrice.Text);
                poh.Unit = TextUtils.ToString(cboUnits.Text);
                poh.VAT = TextUtils.ToDecimal(txtVat.Text);
                poh.UnitPrice = TextUtils.ToDecimal(txtUnitPrice.Text);
                poh.UnitPriceVAT = TextUtils.ToDecimal(txtUnitPriceVat.Text);
                poh.SupplierVoucher = TextUtils.ToString(txtSupplierVoucher.Text);
                poh.DeptSupplier = TextUtils.ToBoolean(cbDeptSupplier.Checked);
                poh.NCCNew = TextUtils.ToBoolean(cbNCCNew.Checked);
                poh.QtyRequest = TextUtils.ToDecimal(txtQuantityOrder.Text);
                poh.QuantityReturn = TextUtils.ToDecimal(txtQuantityReturn.Text);
                poh.QuantityRemain = TextUtils.ToDecimal(txtQuantityRemain.Text);
                poh.MinQuantity = TextUtils.ToDecimal(txtMinQuantity.Text);
                poh.BillCode = TextUtils.ToString(txtBillCode.Text);
                poh.StatusText = TextUtils.ToString(cboStatus.Text);
                poh.POCode = TextUtils.ToString(txtPOCode.Text);
                poh.CurrencyRate = TextUtils.ToDecimal(txtCurrencyRate.Text);
                //if (dtpRequestDate.Text != "") poh.RequestDate = TextUtils.ToDate(dtpRequestDate.Text.Trim());
                //else poh.RequestDate = null;
                //if (dtpDeliveryDate.Text != "") poh.DeliveryDate = TextUtils.ToDate(dtpDeliveryDate.Text.Trim());
                //else poh.DeliveryDate = null;
                //if (dtpDatelineShip.Text != "") poh.DeadlineDelivery = TextUtils.ToDate(dtpDatelineShip.Text.Trim());
                //else poh.DeadlineDelivery = null;


                poh.RequestDate = TextUtils.ToDate4(dtpRequestDate.Text.Trim());
                poh.DeliveryDate = TextUtils.ToDate4(dtpDeliveryDate.Text.Trim());
                poh.DeadlineDelivery = TextUtils.ToDate4(dtpDatelineShip.Text.Trim());

                poh.TotalQuantityLast = TextUtils.ToDecimal(txtTotalQuantityLast.Text);
                poh.ProductName = TextUtils.ToString(txtProductName.Text);
                poh.Note = TextUtils.ToString(txtNote.Text);
                poh.FullName = cboEmployee.Text;
                poh.NameNCC = cboSupplierSale.Text;
                poh.CurrencyName = cboCurrencyName.Text;
                poh.CompanyText = TextUtils.ToString(cboCompany.Text);
                poh.CurrencyRate = TextUtils.ToDecimal(txtCurrencyRate.Text);

                SupplierSaleModel supplierSale = SQLHelper<SupplierSaleModel>.FindAll().Where(i => i.ID == Lib.ToInt(cboSupplierSale.EditValue)).FirstOrDefault();
                supplierSale = supplierSale ?? new SupplierSaleModel();
                poh.CodeNCC = supplierSale.CodeNCC;

                if (poh.ID > 0)
                {
                    SQLHelper<PONCCHistoryModel>.Update(poh);
                }
                else
                {
                    SQLHelper<PONCCHistoryModel>.Insert(poh);
                    //DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm mới không!", "Thông báo", MessageBoxButtons.OKCancel);
                    //if (result == DialogResult.OK)
                    //{

                    //}
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
                return false;
            }

        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
            //this.Close();
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                Reset();

            }
        }


        private void frmSetDetailAllPO_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void cboCurrencyName_EditValueChanged(object sender, EventArgs e)
        {
            var currentRate = SQLHelper<CurrencyModel>.FindByID(TextUtils.ToInt(cboCurrencyName.EditValue));
            txtCurrencyRate.Text = TextUtils.ToString(currentRate.CurrencyRate);
        }

    }
}
