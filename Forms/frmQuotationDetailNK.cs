using BMS.Business;
using BMS.Model;
using DevExpress.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace BMS
{
    public partial class frmQuotationDetailNK : _Forms
    {
        public QuotationModel oQuotation = new QuotationModel();
        bool _isRateSet = true;
        public bool IsCopy;

        public frmQuotationDetailNK()
        {
            InitializeComponent();
        }

        private void frmQuotationDetailNK_Load(object sender, EventArgs e)
        {
            loadUser();
            loadCustomer();
            loadProject();
            loadCurrency();
            loadNCC();
            loadHang();

            loadData();

            if (oQuotation.ID == 0)
            {
                cboSale.EditValue = Global.UserID;
                txtCode.Text = TextUtils.CreateNewCode("Quotation", "QuotationCode", "BG");
            }

            //btnSave.Enabled = btnNew.Enabled = btnDelete.Enabled = !oQuotation.IsApproved;
        }

        #region Methods
        void loadNCC()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.Supplier");
            repositoryItemSearchLookUpEdit1.DataSource = dt;
            repositoryItemSearchLookUpEdit1.ValueMember = "ID";
            repositoryItemSearchLookUpEdit1.DisplayMember = "SupplierShortName";
        }

        void loadHang()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.Manufacturer");
            repositoryItemSearchLookUpEdit2.DataSource = dt;
            repositoryItemSearchLookUpEdit2.ValueMember = "ID";
            repositoryItemSearchLookUpEdit2.DisplayMember = "ManufacturerCode";
        }

        void loadCurrency()
        {
            DataTable dt = TextUtils.Select("SELECT ID,KeyName,KeyValue FROM dbo.ConfigSystem where ConfigType = 1");
            cboCurrency.Properties.DisplayMember = "KeyName";
            cboCurrency.Properties.ValueMember = "KeyValue";
            cboCurrency.Properties.DataSource = dt;
        }

       
        void loadUser()
        {
            DataTable dt = TextUtils.Select("SELECT ID,Code,FullName FROM dbo.Users");
            cboSale.Properties.DisplayMember = "FullName";
            cboSale.Properties.ValueMember = "ID";
            cboSale.Properties.DataSource = dt;

            repositoryItemSearchLookUpEdit3.DisplayMember = "FullName";
            repositoryItemSearchLookUpEdit3.ValueMember = "ID";
            repositoryItemSearchLookUpEdit3.DataSource = dt;
        }
        
        void loadCustomer()
        {
            DataTable dt = TextUtils.Select("SELECT ID,CustomerCode,CustomerShortName FROM dbo.Customer where IsDeleted <> 1 ORDER BY CreatedDate DESC");
            cboCustomer.Properties.DisplayMember = "CustomerShortName";
            cboCustomer.Properties.ValueMember = "ID";
            cboCustomer.Properties.DataSource = dt;
        }

        void loadProject()
        {
            DataTable dt = TextUtils.Select("exec spGetAllProject");
            cboProject.Properties.DisplayMember = "ProjectCode";
            cboProject.Properties.ValueMember = "ID";
            cboProject.Properties.DataSource = dt;
        }

        void setRate(decimal value)
        {
            _isRateSet = false;
            txtRateSet.EditValue = value;
            _isRateSet = true;
        }

        private bool checkValid()
        {
            if (txtCode.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền mã báo giá.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            //else
            //{
            //    int strID = oQuotation.ID;
            //    if (TextUtils.CheckExistTable(strID, "QuotationCode", txtCode.Text.Trim(), "Quotation"))
            //    {
            //        MessageBox.Show("Mã này đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //        return false;
            //    }
            //}

            if (TextUtils.ToInt(cboCustomer.EditValue) == 0)
            {
                MessageBox.Show("Xin hãy chọn một khách hàng.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (TextUtils.ToInt(cboSale.EditValue) == 0)
            {
                MessageBox.Show("Xin hãy chọn một người phụ trách.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (cboStatus.SelectedIndex < 0)
            {
                MessageBox.Show("Xin hãy chọn trạng thái báo giá.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            //if (dteQuotationDate.EditValue == null)
            //{
            //    MessageBox.Show("Xin hãy chọn ngày b giá.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}

            if (TextUtils.ToDecimal( txtPricePS.EditValue )== 0)
            {
                MessageBox.Show("Xin hãy điền số lượng set.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            //if (grvData.RowCount <= 0)
            //{
            //    MessageBox.Show("Xin hãy chọn vật tư yêu cầu vào danh sách.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}
            //if (!checkDetail())
            //{
            //    MessageBox.Show("Xin hãy điền đầy đủ thông tin vật tư(mã, tên).", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}

            return true;
        }

        bool checkDetail()
        {
            int count = grvData.RowCount;
            for (int i = 0; i < count; i++)
            {
                string code = TextUtils.ToString(grvData.GetRowCellValue(i, colCode));
                string name = TextUtils.ToString(grvData.GetRowCellValue(i, colName));
                //string supplier = TextUtils.ToString(grvData.GetRowCellDisplayText(i, colSupplierName));
                if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(name))//|| string.IsNullOrWhiteSpace(supplier))
                {
                    return false;
                }
            }
            return true;
        }

        private void loadData()
        {
            try
            {
                cboStatus.SelectedIndex = oQuotation.QuotationStatus;
                cboProject.EditValue = oQuotation.ProjectID;
                cboCustomer.EditValue = oQuotation.CustomerID;
                txtContactEmail.Text = oQuotation.ContactEmail;
                txtContactName.Text = oQuotation.ContactName;
                txtContactPhone.Text = oQuotation.ContactPhone;

                txtCode.Text = oQuotation.QuotationCode;
                dteQuotationDate.EditValue = oQuotation.QuotationDate;
                cboSale.EditValue = oQuotation.SaleID;
                txtPOCode.Text = oQuotation.POCode;
                txtTotalName.Text = oQuotation.TotalName;

                txtDeliveryFees.Text = oQuotation.DeliveryFees;
                txtPayment.Text = oQuotation.Payment;
                txtPlaceDelivery.Text = oQuotation.PlaceDelivery;

                txtBankCost.EditValue = oQuotation.BankCost;
                txtCustomsCost.EditValue = oQuotation.CustomsCost;
                txtDeliveryCost.EditValue = oQuotation.DeliveryCost;
                
                txtVAT.EditValue = oQuotation.VAT;
                
                cboCurrency.EditValue = oQuotation.CurrencyID;

                txtPricePS.EditValue = oQuotation.PricePS;
                txtPriceVATPS.EditValue = oQuotation.PriceVAT;
                txtTotalVT.EditValue = oQuotation.TotalVT;
                txtTotalPrice.EditValue = oQuotation.TotalPrice;
                txtTotalPriceVAT.EditValue = oQuotation.TotalPriceVAT;

                txtDelivery.Text = oQuotation.DeliveryPeriod;

                DataTable dt = TextUtils.LoadDataFromSP("spGetQuotationDetail_ByMasterID", "A"
                         , new string[] { "@QuotationID" }
                         , new object[] { oQuotation.ID });

                if (IsCopy)
                {
                    oQuotation.ID = 0;
                    oQuotation.IsApproved = false;
                    int count = dt.Rows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        dt.Rows[i]["ID"] = 0;
                    }
                }

                //txtRate.EditValue = txtRateSet.EditValue = dModel.ID == 0 ? 1 : dModel.Rate;
                this.setRate(oQuotation.ID == 0 ? 1 : oQuotation.Rate);
                txtQtySet.EditValue = oQuotation.ID == 0 ? 1 : oQuotation.QtySet;

                grdData.DataSource = dt;
            }
            catch (Exception)
            {
                grdData.DataSource = null;
            }
        }

        void calculate(int rowIndex)
        {
            decimal priceVT = TextUtils.ToDecimal(grvData.GetRowCellValue(rowIndex, colPriceVT));
            decimal qty = TextUtils.ToDecimal(grvData.GetRowCellValue(rowIndex, colQty));
            //decimal vat = TextUtils.ToDecimal(grvData.GetRowCellValue(rowIndex, colVAT));
            decimal tax = TextUtils.ToDecimal(grvData.GetRowCellValue(rowIndex, colTaxImportPercent));
            decimal delivery = TextUtils.ToDecimal(grvData.GetRowCellValue(rowIndex, colDeliveryCost));
            decimal bank = TextUtils.ToDecimal(grvData.GetRowCellValue(rowIndex, colBankCost));
            decimal customs = TextUtils.ToDecimal(grvData.GetRowCellValue(rowIndex, colCustomsCost));

            decimal totalPriceVT = qty * priceVT;
            decimal totalTax = qty * priceVT * tax / 100;
            //decimal totalVAT = qty * priceVT * vat / 100;
            decimal fTotalPrice = totalPriceVT + totalTax + delivery + bank + customs;
            decimal fPrice = qty > 0 ? fTotalPrice / qty : 0;
            decimal price = fPrice * TextUtils.ToDecimal(txtRateSet.EditValue);

            grvData.SetRowCellValue(rowIndex, colTotalPriceVT, totalPriceVT);
            grvData.SetRowCellValue(rowIndex, colTaxImporPrice, qty > 0 ? totalTax / qty : 0);
            grvData.SetRowCellValue(rowIndex, colTaxImporTotal, totalTax);
            //grvData.SetRowCellValue(rowIndex, colTotalVAT, totalVAT);
            grvData.SetRowCellValue(rowIndex, colFinishPrice, fPrice);
            grvData.SetRowCellValue(rowIndex, colFinishTotalPrice, fTotalPrice);

            grvData.SetRowCellValue(rowIndex, colPrice, price);
            grvData.SetRowCellValue(rowIndex, colTotalPrice, price * qty);
        }

        void calculateFinishTotal()
        {
            //splitCost();

            decimal totalF = 0;
            decimal total = 0;
            int count = grvData.RowCount;
            for (int i = 0; i < count; i++)
            {
                totalF += TextUtils.ToDecimal(grvData.GetRowCellValue(i, colFinishTotalPrice));
                total += TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalPrice));
            }
            txtTotalVT.EditValue = totalF;
            txtTotalPrice.EditValue = total;

            if (totalF == 0)
            {
                //txtRate.EditValue = 1;
                return;
            }
            //txtRate.EditValue = total / totalF;
            this.setRate(total / totalF);
        }

        void splitCost()
        {
            decimal delivery = TextUtils.ToDecimal(txtDeliveryCost.EditValue);
            decimal bank = TextUtils.ToDecimal(txtBankCost.EditValue);
            decimal customs = TextUtils.ToDecimal(txtCustomsCost.EditValue);

            DataTable dt = (DataTable)grdData.DataSource;
            decimal sumVT = 0;

            int count = grvData.RowCount;

            for (int i = 0; i < count; i++)
            {
                decimal price = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colPriceVT));
                decimal qty = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQty));

                sumVT += price * qty;
            }

            if (sumVT == 0)
            {
                sumVT = 1;
            }

            for (int i = 0; i < count; i++)
            {
                decimal price = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colPriceVT));
                decimal qty = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQty));

                decimal rate = qty * price / sumVT;

                grvData.SetRowCellValue(i, colDeliveryCost, rate * delivery);
                grvData.SetRowCellValue(i, colBankCost, rate * bank);
                grvData.SetRowCellValue(i, colCustomsCost, rate * customs);
            }
        }

        private void deleteRow()
        {
            if (!grvData.IsDataRow(grvData.FocusedRowHandle))
                return;
            //if (oQuotation.IsApproved) return;

            int strID = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));

            string strName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));

            if (MessageBox.Show(String.Format("Bạn có chắc muốn xóa vật tư [{0}] không?", strName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            try
            {
                if (strID > 0)
                {
                    QuotationDetailBO.Instance.Delete(strID);
                }

                grvData.DeleteSelectedRows();
                //calculateFinishTotal();
            }
            catch
            {
                MessageBox.Show("Có lỗi xảy ra khi thực hiện thao tác, xin vui lòng thử lại sau.");
            }
        }
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkValid()) return;

                grvData.FocusedRowHandle = -1;

                calculateFinishTotal();

                if (oQuotation.ID == 0)
                {
                    oQuotation = new QuotationModel();
                }
                else
                {
                    oQuotation = (QuotationModel)QuotationBO.Instance.FindByPK(oQuotation.ID);
                }

                oQuotation.QuotationStatus = cboStatus.SelectedIndex;
                oQuotation.ProjectID = TextUtils.ToInt(cboProject.EditValue);
                oQuotation.CustomerID = TextUtils.ToInt(cboCustomer.EditValue);
                oQuotation.ContactEmail = txtContactEmail.Text.Trim();
                oQuotation.ContactName = txtContactName.Text.Trim();
                oQuotation.ContactPhone = txtContactPhone.Text.Trim();
                
                oQuotation.QuotationDate = TextUtils.ToDate2(dteQuotationDate.EditValue);
                oQuotation.SaleID = TextUtils.ToInt(cboSale.EditValue);
                oQuotation.POCode = txtPOCode.Text.Trim();
                oQuotation.TotalName = txtTotalName.Text.Trim();

                oQuotation.BankCost = TextUtils.ToDecimal(txtBankCost.EditValue);
                oQuotation.CustomsCost = TextUtils.ToDecimal(txtCustomsCost.EditValue);
                oQuotation.DeliveryCost = TextUtils.ToDecimal(txtDeliveryCost.EditValue);
                oQuotation.Rate = TextUtils.ToDecimal(txtRateSet.EditValue);
                oQuotation.VAT = TextUtils.ToDecimal(txtVAT.EditValue);
                oQuotation.QtySet = TextUtils.ToDecimal(txtQtySet.EditValue);
                oQuotation.CurrencyID = TextUtils.ToInt(cboCurrency.EditValue);

                oQuotation.PricePS = TextUtils.ToDecimal(txtPricePS.EditValue);
                oQuotation.PricePSVAT = TextUtils.ToDecimal(txtPriceVATPS.EditValue);
                oQuotation.TotalVT = TextUtils.ToDecimal(txtTotalVT.EditValue);
                oQuotation.TotalPrice = TextUtils.ToDecimal(txtTotalPrice.EditValue);
                oQuotation.TotalPriceVAT = TextUtils.ToDecimal(txtTotalPriceVAT.EditValue);

                oQuotation.Payment = txtPayment.Text.Trim();
                oQuotation.DeliveryFees = txtDeliveryFees.Text.Trim();
                oQuotation.PlaceDelivery = txtPlaceDelivery.Text.Trim();

                oQuotation.QuotationType = 1;
                oQuotation.DeliveryPeriod = txtDelivery.Text.Trim();

                if (oQuotation.ID == 0)
                {
                    oQuotation.QuotationCode = TextUtils.CreateNewCode("Quotation", "QuotationCode", "BG");
                    oQuotation.ID = (int)QuotationBO.Instance.Insert(oQuotation);
                }
                else
                {
                    QuotationBO.Instance.Update(oQuotation);
                }

                int count = grvData.RowCount;
                for (int i = 0; i < count; i++)
                {
                    long id = TextUtils.ToInt64(grvData.GetRowCellValue(i, colID));
                    QuotationDetailModel detail = new QuotationDetailModel();
                    if (id > 0)
                    {
                        detail = (QuotationDetailModel)QuotationDetailBO.Instance.FindByPK(id);
                    }

                    detail.QuotationID = oQuotation.ID;
                    detail.SupplierID = TextUtils.ToInt(grvData.GetRowCellValue(i, colSupplierID));
                    detail.ManufacturerID = TextUtils.ToInt(grvData.GetRowCellValue(i, colManufacturerID));
                    detail.CurrencyUnit = TextUtils.ToString(grvCurrency.GetFocusedRowCellValue("KeyName"));
                    detail.CurrencyRate = TextUtils.ToDecimal(grvCurrency.GetFocusedRowCellValue("KeyValue"));
                    detail.PartCode = TextUtils.ToString(grvData.GetRowCellValue(i, colCode));
                    detail.PartName = TextUtils.ToString(grvData.GetRowCellValue(i, colName));
                    detail.PartCodeRTC = TextUtils.ToString(grvData.GetRowCellValue(i, colPartCodeRTC));
                    detail.PartNameRTC = TextUtils.ToString(grvData.GetRowCellValue(i, colPartNameRTC));
                    detail.Unit = TextUtils.ToString(grvData.GetRowCellValue(i, colUnit));
                    detail.Qty = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQty));
                    detail.VAT = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colVAT));
                    /*
                     * Giá theo nhà cung cấp
                     */
                    detail.PriceVT = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colPriceVT));
                    detail.TotalVT = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalPriceVT));
                    detail.PriceCurrency = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colPriceCurrency));
                    detail.TotalPriceCurrency = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalPriceCurrency));
                    /*
                     * Chi phí mua hàng
                     */
                    detail.TaxImportPercent = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTaxImportPercent));
                    detail.TaxImporPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTaxImporPrice));
                    detail.TaxImporTotal = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTaxImporTotal));
                    detail.DeliveryCost = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colDeliveryCost));
                    detail.BankCost = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colBankCost));
                    detail.CustomsCost = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colCustomsCost));
                    /*
                    * Tổng tiền gốc = tiền hàng + chi phí
                    */
                    detail.FinishPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colFinishPrice));
                    detail.FinishTotalPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colFinishTotalPrice));
                    /*
                    * Giá báo cho khách hàng
                    */
                    detail.Price = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colPrice));
                    detail.TotalPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalPrice));
                    detail.PriceOld = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colPriceOld));

                    detail.QtySet = oQuotation.QtySet;
                    detail.QtyPS = detail.Qty / oQuotation.QtySet;
                    detail.PricePS = detail.FinishPrice * detail.QtyPS;

                    if (detail.ID == 0)
                    {
                        QuotationDetailBO.Instance.Insert(detail);
                    }
                    else
                    {
                        QuotationDetailBO.Instance.Update(detail);
                    }
                }

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == colPriceVT || e.Column == colQty || e.Column == colTaxImportPercent 
                || e.Column == colDeliveryCost || e.Column == colBankCost || e.Column == colCustomsCost)
            {
                calculate(e.RowHandle);
            }
            if (e.Column == colPriceVT)
            {
                splitCost();
            }

            if (e.Column == colPriceCurrency)
            {
                decimal price = TextUtils.ToDecimal(grvData.GetRowCellValue(e.RowHandle, colPriceCurrency));
                decimal qty = TextUtils.ToDecimal(grvData.GetRowCellValue(e.RowHandle, colQty));

                grvData.SetRowCellValue(e.RowHandle, colTotalPriceCurrency, qty*price);
                grvData.SetRowCellValue(e.RowHandle, colPriceVT, TextUtils.ToDecimal(TextUtils.GetConfigValue(cboCurrency.Text)) * price);
            }

            if (e.Column == colPrice)
            {
                decimal price = TextUtils.ToDecimal(grvData.GetRowCellValue(e.RowHandle, colPrice));
                decimal qty = TextUtils.ToDecimal(grvData.GetRowCellValue(e.RowHandle, colQty));

                grvData.SetRowCellValue(e.RowHandle, colTotalPrice, qty * price);
            }

            if (e.Column == colFinishTotalPrice || e.Column == colTotalPrice)
            {
                calculateFinishTotal();
            }
        }

        private void cboProject_EditValueChanged(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvProject.GetFocusedRowCellValue(colCustomerID));
            cboCustomer.EditValue = id;
        }

        private void txtTotal_EditValueChanged(object sender, EventArgs e)
        {
            txtPricePS.EditValue = TextUtils.ToDecimal(txtQtySet.EditValue) > 0 ? 
                TextUtils.ToDecimal(txtTotalPrice.EditValue) / TextUtils.ToDecimal(txtQtySet.EditValue) : 0;

            txtTotalPriceVAT.EditValue = TextUtils.ToDecimal(txtTotalPrice.EditValue) * ((TextUtils.ToDecimal(txtVAT.EditValue) + 100) / 100);
        }

        private void txtQtySet_EditValueChanged(object sender, EventArgs e)
        {
            txtPricePS.EditValue = TextUtils.ToDecimal(txtQtySet.EditValue) > 0 ? 
                TextUtils.ToDecimal(txtTotalPrice.EditValue) / TextUtils.ToDecimal(txtQtySet.EditValue) : 0;
            txtPriceVATPS.EditValue = TextUtils.ToDecimal(txtQtySet.EditValue) > 0 ? 
                TextUtils.ToDecimal(txtTotalPriceVAT.EditValue) / TextUtils.ToDecimal(txtQtySet.EditValue) : 0;
        }

        private void txtDeliveryCost_EditValueChanged(object sender, EventArgs e)
        {
            splitCost();
        }

        private void txtCustomsCost_EditValueChanged(object sender, EventArgs e)
        {
            splitCost();
        }

        private void txtBankCost_EditValueChanged(object sender, EventArgs e)
        {
            splitCost();
        }

        private void grvData_RowCountChanged(object sender, EventArgs e)
        {
            splitCost();
            calculateFinishTotal();
        }

        private void txtRateSet_EditValueChanged(object sender, EventArgs e)
        {
            if (!_isRateSet) return;
            decimal rate = TextUtils.ToDecimal(txtRateSet.EditValue);
            //txtRate.EditValue = rate;
            int count = grvData.RowCount;
            for (int i = 0; i < count; i++)
            {
                decimal priceVT = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colFinishPrice));
                grvData.SetRowCellValue(i, colPrice, rate * priceVT);
            }
        }

        private void txtVAT_EditValueChanged(object sender, EventArgs e)
        {
            txtTotalPriceVAT.EditValue = TextUtils.ToDecimal(txtTotalPrice.EditValue) * ((100 + TextUtils.ToDecimal(txtVAT.EditValue)) / 100);
        }

        private void txtTotalPriceVAT_EditValueChanged(object sender, EventArgs e)
        {
            txtPriceVATPS.EditValue = TextUtils.ToDecimal(txtQtySet.EditValue) > 0 ? 
                TextUtils.ToDecimal(txtTotalPriceVAT.EditValue) / TextUtils.ToDecimal(txtQtySet.EditValue) : 
                TextUtils.ToDecimal(txtTotalPriceVAT.EditValue);
        }

        private void cboCurrency_EditValueChanged(object sender, EventArgs e)
        {
            int count = grvData.RowCount;
            for (int i = 0; i < count; i++)
            {
                decimal price = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colPriceCurrency));
                grvData.SetRowCellValue(i, colPriceVT, TextUtils.ToDecimal(grvCurrency.GetFocusedRowCellValue("KeyValue")) * price);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            deleteRow();
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void addRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //grvData.FocusedRowHandle = -1;
            grvData.AddNewRow();
            grvData.FocusedColumn = grvData.VisibleColumns[0];
            grvData.Focus();
            //grvData.FocusedRowHandle = -1;
        }

        private void deleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deleteRow();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            addRowToolStripMenuItem_Click(null, null);
        }

        private void btnShowHistoryPrice_Click(object sender, EventArgs e)
        {
            frmQuotationDetailHistory frm = new frmQuotationDetailHistory();
            frm.PartCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            frm.PartCodeRTC = TextUtils.ToString(grvData.GetFocusedRowCellValue(colPartCodeRTC));
            frm.CustomerID = TextUtils.ToInt(cboCustomer.EditValue);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                grvData.SetFocusedRowCellValue(colPriceOld, frm.Value);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            
        }
    }
}
