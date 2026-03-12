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
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmQuotationDetail : _Forms
    {
        public RequestPriceModel oRequestPrice = new RequestPriceModel();
        public QuotationModel oQuotation = new QuotationModel();
        bool _isRateSet = true;
        public bool IsCopy = false;

        public frmQuotationDetail()
        {
            InitializeComponent();
        }

        private void frmQuotationDetail_Load(object sender, EventArgs e)
        {
            loadUser();
            loadCustomer();
            loadProject();
            loadHang();

            if (oQuotation.ID > 0)
            {
                oRequestPrice.ID = oQuotation.RequestPriceID;
            }

            loadData();

            btnSave.Enabled = btnNew.Enabled = btnDelete.Enabled = !oQuotation.IsApproved;
        }

        #region Methods
        void loadHang()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.Manufacturer");
            repositoryItemSearchLookUpEdit2.DataSource = dt;
            repositoryItemSearchLookUpEdit2.ValueMember = "ID";
            repositoryItemSearchLookUpEdit2.DisplayMember = "ManufacturerCode";
        }
        /// <summary>
        /// Lấy danh sách người phụ trách lên combo
        /// </summary>
        void loadUser()
        {
            DataTable dt = TextUtils.Select("SELECT ID,Code,FullName,Code+'-'+FullName AS UserInfo FROM dbo.Users");
            cboSale.Properties.DisplayMember = "FullName";
            cboSale.Properties.ValueMember = "ID";
            cboSale.Properties.DataSource = dt;

            repositoryItemSearchLookUpEdit3.DisplayMember = "FullName";
            repositoryItemSearchLookUpEdit3.ValueMember = "ID";
            repositoryItemSearchLookUpEdit3.DataSource = dt;
        }
        /// <summary>
        /// Lấy danh sách khách hàng lên combo chọn
        /// </summary>
        void loadCustomer()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.Customer where IsDeleted <> 1 ORDER BY CreatedDate DESC");
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

        void loadNCC()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.Supplier");
            repositoryItemSearchLookUpEdit1.DataSource = dt;
            repositoryItemSearchLookUpEdit1.ValueMember = "ID";
            repositoryItemSearchLookUpEdit1.DisplayMember = "SupplierShortName";
        }

        private bool checkValid()
        {
            if (txtCode.Text.Trim()=="")
            {
                MessageBox.Show("Xin hãy điền mã báo giá.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

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
            //    MessageBox.Show("Xin hãy chọn ngày báo giá.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}

            //if (grvData.RowCount <= 0)
            //{
            //    MessageBox.Show("Xin hãy chọn vật tư báo giá vào danh sách.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
                dteQuotationDate.EditValue = oQuotation.QuotationDate;

                txtCode.Text = oQuotation.QuotationCode;
                txtTotalName.Text = oQuotation.TotalName;
                txtDelivery.Text = oQuotation.DeliveryPeriod;
                txtContactEmail.Text = oQuotation.ContactEmail;
                txtContactName.Text = oQuotation.ContactName;
                txtContactPhone.Text = oQuotation.ContactPhone;

                cboProject.EditValue = oQuotation.ProjectID;
                cboCustomer.EditValue = oQuotation.CustomerID;
                cboSale.EditValue = oQuotation.SaleID;

                txtQtySet.EditValue = oQuotation.QtySet;
                txtTotalPrice.EditValue = oQuotation.TotalPrice;
                txtTotalVT.EditValue = oQuotation.TotalVT;
                //txtRate.EditValue = oQuotation.Rate;
                this.setRate(oQuotation.Rate);
                txtPricePS.EditValue = oQuotation.PricePS;
                txtVAT.EditValue = oQuotation.VAT;
                txtTotalPriceVAT.EditValue = oQuotation.TotalPriceVAT;
                txtPricePSVAT.EditValue = oQuotation.PricePSVAT;

                txtDeliveryFees.Text = oQuotation.DeliveryFees;
                txtPayment.Text = oQuotation.Payment;
                txtPlaceDelivery.Text = oQuotation.PlaceDelivery;

                DataTable dtDetail = new DataTable();
                if (oRequestPrice.ID > 0 && oQuotation.ID == 0)
                {
                    txtQtySet.EditValue = oRequestPrice.QtySet;
                    cboProject.EditValue = oRequestPrice.ProjectID;
                    cboCustomer.EditValue = oRequestPrice.CustomerID;
                    cboSale.EditValue = oRequestPrice.RequestPersonID;
                    txtTotalVT.EditValue = oRequestPrice.TotalPrice;
                    txtTotalPrice .EditValue = oRequestPrice.TotalPrice;
                    txtPricePS.EditValue = oRequestPrice.TotalPrice / (oRequestPrice.QtySet == 0 ? 1 : oRequestPrice.QtySet);
                    //txtRate.EditValue = 1;
                    this.setRate(1);

                    dtDetail = TextUtils.LoadDataFromSP("spGetQuotationDetail_ByRequestPriceID", "A"
                       , new string[] { "@RequestPriceID", "@UserName" }
                       , new object[] { oRequestPrice.ID, Global.AppUserName });
                }
                else
                {
                    dtDetail = TextUtils.LoadDataFromSP("spGetQuotationDetail_ByMasterID", "A"
                         , new string[] { "@QuotationID" }
                         , new object[] { oQuotation.ID });
                }

                if (IsCopy)
                {
                    oQuotation.ID = 0;
                    oQuotation.IsApproved = false;
                    for (int i = 0; i < dtDetail.Rows.Count; i++)
                    {
                        dtDetail.Rows[i]["ID"] = 0;
                    }
                }

                grdData.DataSource = dtDetail;

                if (oQuotation.ID == 0)
                {
                    txtCode.Text = TextUtils.CreateNewCode("Quotation", "QuotationCode", "BG");
                }
            }
            catch (Exception ex)
            {
                grdData.DataSource = null;
            }
        }

        void calculateFinishTotal()
        {
            decimal total = 0;
            decimal totalVT = 0;
            int count = grvData.RowCount;
            for (int i = 0; i < count; i++)
            {
                total += TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalPrice));
                totalVT += TextUtils.ToDecimal(grvData.GetRowCellValue(i, colFinishTotalPrice));
            }
            txtTotalVT.EditValue = totalVT;
            txtTotalPrice.EditValue = total;

            if (TextUtils.ToDecimal(txtTotalVT.EditValue) == 0)
            {
                return;
            }
            setRate(total / totalVT);
        }

        void setRate(decimal value)
        {
            _isRateSet = false;
            txtRateSet.EditValue = value;
            _isRateSet = true;
        }

        void calculate(int rowIndex)
        {
            decimal price = TextUtils.ToDecimal(grvData.GetRowCellValue(rowIndex, colPrice));
            decimal priceVT = TextUtils.ToDecimal(grvData.GetRowCellValue(rowIndex, colFinishPrice));
            decimal qty = TextUtils.ToDecimal(grvData.GetRowCellValue(rowIndex, colQty));

            decimal totalPrice = qty * price;
            decimal totalVT = qty * priceVT;

            grvData.SetRowCellValue(rowIndex, colTotalPrice, totalPrice);
            grvData.SetRowCellValue(rowIndex, colFinishTotalPrice, totalVT);

            calculateFinishTotal();
        }
        #endregion

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!grvData.IsDataRow(grvData.FocusedRowHandle))
                return;
            if (oQuotation.IsApproved) return;

            int strID = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));

            string strName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));

            if (MessageBox.Show(String.Format("Bạn có chắc muốn xóa vật tư [{0}] không?", strName), 
                TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            try
            {
                if (strID > 0)
                {
                    QuotationDetailBO.Instance.Delete(strID);
                }
                
                grvData.DeleteSelectedRows();
                calculateFinishTotal();
            }
            catch
            {
                MessageBox.Show("Có lỗi xảy ra khi thực hiện thao tác, xin vui lòng thử lại sau.");
            }
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            grvData.FocusedRowHandle = -1;
            
            try
            {
                if (!checkValid()) return;

                oQuotation.RequestPriceID = oRequestPrice.ID;

                oQuotation.ProjectID = TextUtils.ToInt(cboProject.EditValue);
                oQuotation.CustomerID = TextUtils.ToInt(cboCustomer.EditValue);
                oQuotation.QuotationStatus = cboStatus.SelectedIndex;
                oQuotation.SaleID = TextUtils.ToInt(cboSale.EditValue);
                oQuotation.TotalName = txtTotalName.Text.Trim();
                
                oQuotation.QuotationDate = TextUtils.ToDate2(dteQuotationDate.EditValue);
                oQuotation.ContactEmail = txtContactEmail.Text.Trim();
                oQuotation.ContactName = txtContactName.Text.Trim();
                oQuotation.ContactPhone = txtContactPhone.Text.Trim();
                oQuotation.DeliveryPeriod = txtDelivery.Text.Trim();
                oQuotation.POCode = txtPOCode.Text.Trim();
                
                oQuotation.Rate = TextUtils.ToDecimal(txtRateSet.EditValue);
                oQuotation.QtySet = TextUtils.ToDecimal(txtQtySet.EditValue);
                oQuotation.VAT = TextUtils.ToDecimal(txtVAT.EditValue);

                oQuotation.PricePS = TextUtils.ToDecimal(txtPricePS.EditValue);
                oQuotation.TotalPrice = TextUtils.ToDecimal(txtTotalPrice.EditValue);
                oQuotation.PricePSVAT = TextUtils.ToDecimal(txtPricePSVAT.EditValue);
                oQuotation.TotalPriceVAT = TextUtils.ToDecimal(txtTotalPriceVAT.EditValue);
                oQuotation.TotalVT = TextUtils.ToDecimal(txtTotalVT.EditValue);

                oQuotation.QuotationType = 0;

                oQuotation.Payment = txtPayment.Text.Trim();
                oQuotation.DeliveryFees = txtDeliveryFees.Text.Trim();
                oQuotation.PlaceDelivery = txtPlaceDelivery.Text.Trim();

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
                    detail.RequestPriceDetailID = TextUtils.ToInt(grvData.GetRowCellValue(i, colRequestPriceDetailID)); 
                    detail.PartCode = TextUtils.ToString(grvData.GetRowCellValue(i, colCode));
                    detail.PartName = TextUtils.ToString(grvData.GetRowCellValue(i, colName));
                    detail.PartCodeRTC = TextUtils.ToString(grvData.GetRowCellValue(i, colPartCodeRTC));
                    detail.PartNameRTC = TextUtils.ToString(grvData.GetRowCellValue(i, colPartNameRTC));
                    detail.Unit = TextUtils.ToString(grvData.GetRowCellValue(i, colUnit));

                    detail.Qty = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQty));
                    detail.QtyPS = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQtyPS));
                    detail.QtySet = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQtySet));

                    detail.Price = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colPrice));
                    detail.TotalPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalPrice));
                    detail.PriceVT = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colFinishPrice));
                    detail.TotalVT = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colFinishTotalPrice));
                    detail.FinishPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colFinishPrice));
                    detail.FinishTotalPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colFinishTotalPrice));

                    detail.ManufacturerID = TextUtils.ToInt(grvData.GetRowCellValue(i, colManufacturerID));
                    
                    if (detail.ID == 0)
                    {
                        //pt.Insert(detail);
                        QuotationDetailBO.Instance.Insert(detail);
                    }
                    else
                    {
                        //pt.Update(detail);
                        QuotationDetailBO.Instance.Update(detail);
                    }
                }

                //pt.CommitTransaction();
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //pt.CloseConnection();
            }
        }

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == colQty || e.Column == colPrice || e.Column == colFinishPrice)
            {
                calculate(e.RowHandle);
            }
            //if (e.Column == colQty)
            //{
            //    decimal qty = TextUtils.ToDecimal(grvData.GetRowCellValue(e.RowHandle, colQty));
            //    grvData.SetRowCellValue(e.RowHandle, colQtyPS, qty/TextUtils.ToDecimal(txtQtySet.EditValue));
            //}

            if (e.Column == colCode)
            {
                string code = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, colCode));
                string codeR = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, colPartCodeRTC));
                if (string.IsNullOrEmpty(codeR))
                {
                    grvData.SetRowCellValue(e.RowHandle, colPartCodeRTC, code);
                }
            }
            if (e.Column == colName)
            {
                string name = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, colName));
                string nameR = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, colPartNameRTC));
                if (string.IsNullOrEmpty(nameR))
                {
                    grvData.SetRowCellValue(e.RowHandle, colPartNameRTC, name);
                }
            }

            if (e.Column == colPartCodeRTC)
            {
                string code = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, colCode));
                string codeR = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, colPartCodeRTC));
                if (string.IsNullOrEmpty(code))
                {
                    grvData.SetRowCellValue(e.RowHandle, colCode, codeR);
                }
            }
            if (e.Column == colPartNameRTC)
            {
                string name = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, colName));
                string nameR = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, colPartNameRTC));
                if (string.IsNullOrEmpty(name))
                {
                    grvData.SetRowCellValue(e.RowHandle, colName, nameR);
                }
            }
        }

        private void cboProject_EditValueChanged(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvProject.GetFocusedRowCellValue(colCustomerID));
            cboCustomer.EditValue = id;
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            decimal rate = TextUtils.ToDecimal(txtRateSet.EditValue);
            int count = grvData.RowCount;
            for (int i = 0; i < count; i++)
            {
                decimal priceVT = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colFinishPrice));
                grvData.SetRowCellValue(i, colPrice, rate * priceVT);
            }
        }

        private void txtTotalVT_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void txtTotal_EditValueChanged(object sender, EventArgs e)
        {
            if (TextUtils.ToDecimal(txtTotalVT.EditValue) == 0)
            {
                this.setRate(1);
                return;
            }
            //txtRate.EditValue = TextUtils.ToDecimal(txtTotalPrice.EditValue) / TextUtils.ToDecimal(txtTotalVT.EditValue);
            this.setRate(TextUtils.ToDecimal(txtTotalPrice.EditValue) / TextUtils.ToDecimal(txtTotalVT.EditValue));
            txtPricePS.EditValue = TextUtils.ToDecimal(txtTotalPrice.EditValue) / TextUtils.ToDecimal(txtQtySet.EditValue);
            txtTotalPriceVAT.EditValue = TextUtils.ToDecimal(txtTotalPrice.EditValue) * ((100 + TextUtils.ToDecimal(txtVAT.EditValue)) / 100);
        }

        private void grdData_Click(object sender, EventArgs e)
        {
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

        private void grvData_RowCountChanged(object sender, EventArgs e)
        {
            calculateFinishTotal();
        }

        private void txtQtySet_EditValueChanged(object sender, EventArgs e)
        {
            int count = grvData.RowCount;
            decimal qtySet = TextUtils.ToDecimal(txtQtySet.EditValue);
            if (qtySet == 0)
            {
                txtQtySet.EditValue = 1;
                qtySet = 1;
            }
            for (int i = 0; i < count; i++)
            {
                decimal qty = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQty));
                grvData.SetRowCellValue(i, colQtyPS, qty/qtySet);
                grvData.SetRowCellValue(i, colQtySet, qtySet);
            }

            txtPricePS.EditValue = TextUtils.ToDecimal(txtQtySet.EditValue) > 0 ?
                TextUtils.ToDecimal(txtTotalPrice.EditValue) / TextUtils.ToDecimal(txtQtySet.EditValue) : 0;
            txtPricePSVAT.EditValue = TextUtils.ToDecimal(txtQtySet.EditValue) > 0 ?
                TextUtils.ToDecimal(txtTotalPriceVAT.EditValue) / TextUtils.ToDecimal(txtQtySet.EditValue) : 0;
        }
     
        private void txtVAT_EditValueChanged(object sender, EventArgs e)
        {
            txtTotalPriceVAT.EditValue = TextUtils.ToDecimal(txtTotalPrice.EditValue) * ((100 + TextUtils.ToDecimal(txtVAT.EditValue)) / 100);               
        }

        private void txtTotalPriceVAT_EditValueChanged(object sender, EventArgs e)
        {
            txtPricePSVAT.EditValue = TextUtils.ToDecimal(txtQtySet.EditValue) > 0 ?
                TextUtils.ToDecimal(txtTotalPriceVAT.EditValue) / TextUtils.ToDecimal(txtQtySet.EditValue) :
                TextUtils.ToDecimal(txtTotalPriceVAT.EditValue);
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
            btnDelete_Click(null, null);
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
    }
}
