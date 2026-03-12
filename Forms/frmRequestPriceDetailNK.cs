using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmRequestPriceDetailNK : _Forms
    {
        public RequestPriceModel dModel = new RequestPriceModel();

        public bool IsCopy;

        public frmRequestPriceDetailNK()
        {
            InitializeComponent();
        }

        private void frmRequestPriceDetail_Load(object sender, EventArgs e)
        {
            loadUser();
            loadCustomer();
            loadProject();
            loadCurrency();
            loadNCC();
            loadHang();

            loadData();

            if (dModel.ID == 0)
            {
                cboUser.EditValue = Global.UserID;
                txtCode.Text = TextUtils.CreateNewCode("RequestPrice", "RequestPriceCode", "YCHG");
            }
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
            DataTable dt = TextUtils.Select("SELECT KeyName,KeyValue FROM dbo.ConfigSystem where ConfigType = 1");
            cboCurrency.DisplayMember = "KeyName";
            cboCurrency.ValueMember = "KeyValue";
            cboCurrency.DataSource = dt;
        }

        /// <summary>
        /// Lấy danh sách người phụ trách lên combo
        /// </summary>
        void loadUser()
        {
            DataTable dt = TextUtils.Select("SELECT ID,Code,FullName,Code+'-'+FullName AS UserInfo FROM dbo.Users");
            cboUser.Properties.DisplayMember = "FullName";
            cboUser.Properties.ValueMember = "ID";
            cboUser.Properties.DataSource = dt;

            repositoryItemSearchLookUpEdit3.DisplayMember = "FullName";
            repositoryItemSearchLookUpEdit3.ValueMember = "ID";
            repositoryItemSearchLookUpEdit3.DataSource = dt;
        }
        /// <summary>
        /// Lấy danh sách khách hàng lên combo chọn
        /// </summary>
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

        private bool checkValid()
        {
            if (cboCustomer.EditValue == null)
            {
                MessageBox.Show("Xin hãy chọn một khách hàng.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (cboUser.EditValue == null)
            {
                MessageBox.Show("Xin hãy chọn một người yêu cầu.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (cboStatus.SelectedIndex < 0)
            {
                MessageBox.Show("Xin hãy chọn trạng thái yêu cầu.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (dteDeadLine.EditValue == null)
            {
                MessageBox.Show("Xin hãy chọn ngày cần giá.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (TextUtils.ToDecimal( txtPrice.EditValue )== 0)
            {
                MessageBox.Show("Xin hãy điền số lượng set.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (grvData.RowCount <= 0)
            {
                MessageBox.Show("Xin hãy chọn vật tư yêu cầu vào danh sách.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (!checkDetail())
            {
                MessageBox.Show("Xin hãy điền đầy đủ thông tin vật tư(mã, tên).", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

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
                cboProject.EditValue = dModel.ProjectID;
                cboCustomer.EditValue = dModel.CustomerID;
                cboStatus.SelectedIndex = dModel.RequestStatus;
                cboType.SelectedIndex = dModel.RequestType;
                cboUser.EditValue = dModel.RequestPersonID;

                txtBankCost.EditValue = dModel.BankCost;
                txtCode.Text = dModel.RequestPriceCode;
                txtCustomsCost.EditValue = dModel.CustomsCost;
                txtDeliveryCost.EditValue = dModel.DeliveryCost;
                txtTotal.EditValue = dModel.TotalPrice;
                txtPurpose.Text = dModel.Purpose;
                txtNote.Text = dModel.Note;

                dteDeadLine.EditValue = dModel.DeadLine;

                //chkImport.Checked = dModel.IsImport;
                txtQtySet.EditValue = dModel.QtySet;
                txtPrice.EditValue = dModel.Price;

                DataTable dt = TextUtils.LoadDataFromSP("spGetRequestPriceDetail_ByMasterID", "A"
                   , new string[] { "@RequestPriceID" }
                   , new object[] { dModel.ID });

                if (IsCopy)
                {
                    dModel.ID = 0;
                    int count = dt.Rows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        dt.Rows[i]["ID"] = 0;
                    }
                }

                grdData.DataSource = dt;
            }
            catch (Exception)
            {
                grdData.DataSource = null;
            }
        }
        #endregion

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!grvData.IsDataRow(grvData.FocusedRowHandle))
                return;

            int strID = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));

            string strName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));

            if (MessageBox.Show(String.Format("Bạn có chắc muốn xóa vật tư [{0}] không?", strName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            try
            {
                if (strID > 0)
                {
                    RequestPriceDetailBO.Instance.Delete(strID);
                }
                
                grvData.DeleteSelectedRows();
                //calculateFinishTotal();
            }
            catch
            {
                MessageBox.Show("Có lỗi xảy ra khi thực hiện thao tác, xin vui lòng thử lại sau.");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkValid()) return;

                grvData.FocusedRowHandle = -1;

                calculateFinishTotal();

                if (dModel.ID == 0)
                {
                    dModel = new RequestPriceModel();
                }
                else
                {
                    dModel = (RequestPriceModel)RequestPriceBO.Instance.FindByPK(dModel.ID);
                }

                dModel.ProjectID = TextUtils.ToInt(cboProject.EditValue);
                dModel.CustomerID = TextUtils.ToInt(cboCustomer.EditValue);
                dModel.RequestStatus = cboStatus.SelectedIndex;
                dModel.RequestType = cboType.SelectedIndex;
                dModel.RequestPersonID = TextUtils.ToInt(cboUser.EditValue);

                dModel.BankCost = TextUtils.ToDecimal(txtBankCost.EditValue);
                dModel.CustomsCost = TextUtils.ToDecimal(txtCustomsCost.EditValue);
                dModel.DeliveryCost = TextUtils.ToDecimal(txtDeliveryCost.EditValue);
                
                dModel.Purpose = txtPurpose.Text.Trim();
                dModel.Note = txtNote.Text.Trim();
                dModel.RequestPriceCode = txtCode.Text.Trim();

                dModel.Price = TextUtils.ToDecimal(txtPrice.EditValue);
                dModel.QtySet = TextUtils.ToDecimal(txtQtySet.EditValue);
                dModel.TotalPrice = TextUtils.ToDecimal(txtTotal.EditValue);

                dModel.DeadLine = TextUtils.ToDate2(dteDeadLine.EditValue);
                dModel.IsImport = true;//chkImport.Checked;

                if (dModel.ID == 0)
                {
                    dModel.ID = (int)RequestPriceBO.Instance.Insert(dModel);
                }
                else
                {
                    RequestPriceBO.Instance.Update(dModel);
                }

                int count = grvData.RowCount;
                for (int i = 0; i < count; i++)
                {
                    long id = TextUtils.ToInt64(grvData.GetRowCellValue(i, colID));
                    RequestPriceDetailModel detail = new RequestPriceDetailModel();
                    if (id > 0)
                    {
                        detail = (RequestPriceDetailModel)RequestPriceDetailBO.Instance.FindByPK(id);
                    }

                    detail.RequestPriceID = dModel.ID;
                    detail.PartCode = TextUtils.ToString(grvData.GetRowCellValue(i, colCode));
                    detail.PartName = TextUtils.ToString(grvData.GetRowCellValue(i, colName));
                    detail.Unit = TextUtils.ToString(grvData.GetRowCellValue(i, colUnit));
                    detail.Qty = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQty));
                    detail.VAT = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colVAT));
                    detail.Price = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colPrice));
                    detail.TotalPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalPrice));
                    detail.PriceCurrency = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colPriceCurrency));
                    detail.TotalPriceCurrency = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalPriceCurrency));
                    detail.TaxImportPercent = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTaxImportPercent));
                    detail.TaxImporPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTaxImporPrice));
                    detail.TaxImporTotal = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTaxImporTotal));
                    detail.DeliveryCost = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colDeliveryCost));
                    detail.BankCost = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colBankCost));
                    detail.CustomsCost = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colCustomsCost));
                    detail.FinishPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colFinishPrice));
                    detail.FinishTotalPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colFinishTotalPrice));
                    detail.PriceVAT = detail.VAT * detail.Price / 100;
                    detail.TotalVAT = detail.Qty * detail.PriceVAT;
                    detail.CurrencyUnit = TextUtils.ToString(cboCurrency.Text);
                    detail.CurrencyRate = TextUtils.ToDecimal(cboCurrency.SelectedValue);

                    //detail.Supplier = TextUtils.ToInt(grvData.GetRowCellValue(i, colSupplierID));
                    //detail.Manufacturer = TextUtils.ToInt(grvData.GetRowCellValue(i, colManufacturerID));
                    detail.AskPriceID = TextUtils.ToInt(grvData.GetRowCellValue(i, colAskPriceID));

                    detail.QtySet = dModel.QtySet;
                    detail.QtyPS = detail.Qty / dModel.QtySet;
                    detail.PricePS = detail.FinishPrice * detail.QtyPS;

                    if (detail.ID == 0)
                    {
                        RequestPriceDetailBO.Instance.Insert(detail);
                    }
                    else
                    {
                        RequestPriceDetailBO.Instance.Update(detail);
                    }
                }

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void calculate(int rowIndex)
        {
            decimal price = TextUtils.ToDecimal(grvData.GetRowCellValue(rowIndex, colPrice));
            decimal qty = TextUtils.ToDecimal(grvData.GetRowCellValue(rowIndex, colQty));
            decimal vat = TextUtils.ToDecimal(grvData.GetRowCellValue(rowIndex, colVAT));
            decimal tax = TextUtils.ToDecimal(grvData.GetRowCellValue(rowIndex, colTaxImportPercent));
            decimal delivery = TextUtils.ToDecimal(grvData.GetRowCellValue(rowIndex, colDeliveryCost));
            decimal bank = TextUtils.ToDecimal(grvData.GetRowCellValue(rowIndex, colBankCost));
            decimal customs = TextUtils.ToDecimal(grvData.GetRowCellValue(rowIndex, colCustomsCost));

            decimal totalPrice = qty * price;
            decimal totalTax = qty * price * tax / 100;
            decimal totalVAT = qty * price * vat / 100;
            decimal fTotalPrice = totalPrice + totalTax + totalVAT + delivery + bank + customs;
            decimal fPrice = qty > 0 ? fTotalPrice / qty : 0;

            grvData.SetRowCellValue(rowIndex, colTotalPrice, totalPrice);
            grvData.SetRowCellValue(rowIndex, colTaxImporPrice, qty > 0 ? totalTax / qty : 0);
            grvData.SetRowCellValue(rowIndex, colTaxImporTotal, totalTax);
            grvData.SetRowCellValue(rowIndex, colTotalVAT, totalVAT);
            grvData.SetRowCellValue(rowIndex, colFinishPrice, fPrice);
            grvData.SetRowCellValue(rowIndex, colFinishTotalPrice, fTotalPrice);
        }

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == colPrice || e.Column == colQty || e.Column == colTaxImportPercent || e.Column == colVAT
               || e.Column == colDeliveryCost || e.Column == colBankCost || e.Column == colCustomsCost)
            {
                calculate(e.RowHandle);
            }

            if (e.Column == colPriceCurrency)
            {
                decimal price = TextUtils.ToDecimal(grvData.GetRowCellValue(e.RowHandle, colPriceCurrency));
                decimal qty = TextUtils.ToDecimal(grvData.GetRowCellValue(e.RowHandle, colQty));

                grvData.SetRowCellValue(e.RowHandle, colTotalPriceCurrency, qty*price);
                grvData.SetRowCellValue(e.RowHandle, colPrice, TextUtils.ToDecimal(cboCurrency.SelectedValue) * price);
            }

            if (e.Column == colFinishTotalPrice)
            {
                calculateFinishTotal();
            }
        }

        void calculateFinishTotal()
        {
            decimal total = 0;
            int count = grvData.RowCount;
            for (int i = 0; i < count; i++)
            {
                total += TextUtils.ToDecimal(grvData.GetRowCellValue(i, colFinishTotalPrice));
            }
            txtTotal.EditValue = total;
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
                decimal price = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colPrice));
                decimal qty = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQty));

                sumVT += price * qty;
            }

            if (sumVT == 0)
            {
                sumVT = 1;
            }

            for (int i = 0; i < count; i++)
            {
                decimal price = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colPrice));
                decimal qty = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQty));

                decimal rate = qty * price / sumVT;

                grvData.SetRowCellValue(i, colDeliveryCost, rate * delivery);
                grvData.SetRowCellValue(i, colBankCost, rate * bank);
                grvData.SetRowCellValue(i, colCustomsCost, rate * customs);
            }
        }

        private void btnSetCost_Click(object sender, EventArgs e)
        {
            splitCost();
        }

        private void cboProject_EditValueChanged(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvProject.GetFocusedRowCellValue(colCustomerID));
            cboCustomer.EditValue = id;
        }

        private void txtTotal_EditValueChanged(object sender, EventArgs e)
        {
            txtPrice.EditValue = TextUtils.ToDecimal(txtQtySet.EditValue) > 0 ? TextUtils.ToDecimal(txtTotal.EditValue) / TextUtils.ToDecimal(txtQtySet.EditValue) : 0;
        }

        private void txtQtySet_EditValueChanged(object sender, EventArgs e)
        {
            txtPrice.EditValue = TextUtils.ToDecimal(txtQtySet.EditValue) > 0 ? TextUtils.ToDecimal(txtTotal.EditValue) / TextUtils.ToDecimal(txtQtySet.EditValue) : 0;
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
            calculateFinishTotal();
        }
    }
}
