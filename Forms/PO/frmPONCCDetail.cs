using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Forms.Classes;
using System;
using System.Collections;
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
    public partial class frmPONCCDetail : _Forms
    {
        public int IDDetail;
        public PONCCModel oPONCC = new PONCCModel();
        ArrayList lstIDDelete = new ArrayList();
        List<ArrayList> listRequestBuyRTCID = new List<ArrayList>();

        public List<RequestBuyRTCModel> List = new List<RequestBuyRTCModel>();

        public frmPONCCDetail()
        {
            InitializeComponent();
            cbTypeMoney.SelectedIndex = 0;
            cbCompany.SelectedIndex = 0;
        }

        private void frmBillImport_Load(object sender, EventArgs e)
        {
            loadRulePay();
            loadProduct();
            loadUser();
            LoadSupplier();
            loadPONCCDetail();
            btnSave.Enabled = btnSaveNew.Enabled = btnNewProduct.Enabled = btnDelete.Enabled = !TextUtils.ToBoolean(oPONCC.IsApproved);
            AddRequestBuyRTC();

        }

        void loadRulePay()
        {
            DataTable dt = TextUtils.Select("Select * from RulePay");
            ChkCbRulePay.Properties.DataSource = dt;
            ChkCbRulePay.Properties.DisplayMember = "Code";
            ChkCbRulePay.Properties.ValueMember = "Code";
        }
        #region Methods
        /// <summary>
        /// Load thông tin nhân viên 
        /// </summary>
        void loadUser()
        {
            DataTable dt = TextUtils.Select("SELECT ID,Code,FullName,Code+'-'+FullName AS UserInfo FROM Users");
            cboUser.Properties.DisplayMember = "FullName";
            cboUser.Properties.ValueMember = "ID";
            cboUser.Properties.DataSource = dt;
        }
        void loadPONCCDetail()

        {
            if (oPONCC.ID > 0)
            {
                cboSupplier.EditValue = oPONCC.SupplierID;
                txtPOCode.Text = oPONCC.POCode;
                dtpOrderDate.Value = TextUtils.ToDate5(oPONCC.ReceivedDatePO);
                txtTotalPO.EditValue = oPONCC.TotalMoneyPO;
                dtpActualDate.Value = TextUtils.ToDate5(oPONCC.DeliveryDate);
                cboUser.EditValue = oPONCC.EmployeeID;
                cbTT.SelectedIndex = TextUtils.ToInt(oPONCC.Status);

                //
                txtFedexAccount.Text = oPONCC.FedexAccount;
                txtRuleIncoterm.Text = oPONCC.RuleIncoterm;
                ChkCbRulePay.SetEditValue(oPONCC.RulePay);
                //txtRulePay.Text = oPONCC.RulePay;
                txtNote.Text = oPONCC.Note;
                txtSupplierVoucher.Text = oPONCC.SupplierVoucher;
                txtOriginItem.Text = oPONCC.OriginItem;
                txtAddressDelivery.Text = oPONCC.AddressDelivery;
                txtBankingFee.Text = oPONCC.BankingFee;
                cbTypeMoney.SelectedIndex = TextUtils.ToInt(oPONCC.Currency);
                txtRate.Text = TextUtils.ToString(oPONCC.CurrencyRate);
                cbCompany.SelectedIndex = TextUtils.ToInt(oPONCC.Company);


            }
            else
            {
                cboUser.EditValue = Global.EmployeeID;
                cbTT.SelectedIndex = 0;
            }
            DataTable dt = TextUtils.LoadDataFromSP("spGetPONCCDetail", "A", new string[] { "@PONCCID" }, new object[] { oPONCC.ID });
            grdData.DataSource = dt;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["STT"] = i + 1;
                }
            }
            //calculateTotal();
        }

        /// <summary>
        /// load nhà cung cấp
        /// </summary>
        void LoadSupplier()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM SupplierSale ");
            cboSupplier.Properties.DataSource = dt;
            cboSupplier.Properties.DisplayMember = "NameNCC";
            cboSupplier.Properties.ValueMember = "ID";

        }

        void loadProduct()
        {

            DataTable dtProduct = TextUtils.Select("SELECT * FROM ProductSale");
            cbProductNew.DataSource = dtProduct;
            cbProductNew.DisplayMember = "ProductCode";
            cbProductNew.ValueMember = "ID";
            colProductCode_.ColumnEdit = cbProductNew;
        }



        #endregion

        #region Buttons Events
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnNewSupplier_Click(object sender, EventArgs e)
        {

            frmSupplierSale frm = new frmSupplierSale();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadSupplier();
            }
        }

        private void btnNewProduct_Click(object sender, EventArgs e)
        {
            frmProductDetailSale frm = new frmProductDetailSale();


            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadProduct();
            }
        }

        /// <summary>
        /// Add from RequestBuyRTC 
        /// </summary>
        private void AddRequestBuyRTC()
        {
            if (List.Count == 0)
            {
                return;
            }
            else
            {
                int row = grvData.RowCount;
                foreach (var item in List)
                {
                    DataRow newRow = null;
                    //Kiểm tra dòng cuối cùng STT = bao nhiêu?
                    int STT;
                    DataTable dt = (DataTable)grdData.DataSource;
                    STT = dt.Rows.Count == 0 ? 1 : (TextUtils.ToInt(grvData.GetRowCellValue(dt.Rows.Count - 1, "STT")) + 1);
                    //if (dt.Rows.Count == 0)
                    //    STT = 1;
                    //else
                    //    STT = TextUtils.ToInt(grvData.GetRowCellValue(dt.Rows.Count - 1, "STT")) + 1;
                    newRow = dt.NewRow();
                    newRow["STT"] = STT;
                    newRow["ProductID"] = item.ProductID;
                    newRow["ProductName"] = item.ProductName_;
                    newRow["RequestBuyRTCID"] = item.ID;
                    newRow["QtyRequest"] = (item.Qty - item.QtyReal);
                    newRow["Unit"] = item.Unit;
                    newRow["PriceSale"] = item.PriceSale;
                    dt.Rows.Add(newRow);
                }
            }
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            saveData();
            loadBilllNumber();
            cboSupplier.EditValue = null;
            txtTotalPO.EditValue = "";
            for (int i = grvData.RowCount - 1; i >= 0; i--)
            {
                grvData.DeleteRow(i);
            }

            oPONCC = new PONCCModel();
        }



        #endregion

        bool saveData()
        {

            calculateTotal();
            //if (oPONCC.ID == 0) loadBilllNumber();
            if (!ValidateForm()) return false;


            //master
            cboSupplier.Focus();
            grvData.Focus();
            oPONCC.POCode = txtPOCode.Text;
            oPONCC.ReceivedDatePO = dtpOrderDate.Value;
            oPONCC.TotalMoneyPO = TextUtils.ToInt(txtTotalPO.EditValue);
            oPONCC.RequestDate = dtpOrderDate.Value;
            oPONCC.DeliveryDate = dtpActualDate.Value;
            oPONCC.SupplierID = TextUtils.ToInt(cboSupplier.EditValue);
            oPONCC.EmployeeID = TextUtils.ToInt(cboUser.EditValue);
            //Thông tin bổ xung
            oPONCC.FedexAccount = txtFedexAccount.Text;
            oPONCC.RuleIncoterm = txtRuleIncoterm.Text;

            //old
            //oPONCC.RulePay = txtRulePay.Text;
            //new
            oPONCC.RulePay = TextUtils.ToString(ChkCbRulePay.EditValue);
            oPONCC.Note = txtNote.Text;
            oPONCC.SupplierVoucher = txtSupplierVoucher.Text;
            oPONCC.OriginItem = txtOriginItem.Text;
            oPONCC.AddressDelivery = txtAddressDelivery.Text;
            oPONCC.BankingFee = txtBankingFee.Text;
            oPONCC.Currency = cbTypeMoney.SelectedIndex;
            oPONCC.CurrencyRate = TextUtils.ToDecimal(txtRate.Text);
            oPONCC.Status = cbTT.SelectedIndex;
            oPONCC.Company = cbCompany.SelectedIndex;
            oPONCC.Status_Old = cbTT.SelectedIndex;



            if (oPONCC.ID > 0)
            {
                PONCCBO.Instance.Update(oPONCC);
            }
            else
            {
                oPONCC.ID = (int)PONCCBO.Instance.Insert(oPONCC);
            }


            for (int i = 0; i < grvData.RowCount; i++)
            {
                long id = TextUtils.ToInt(grvData.GetRowCellValue(i, colIDDetail));
                PONCCDetailModel pomccdetai = new PONCCDetailModel();

                if (id > 0)
                {
                    pomccdetai = (PONCCDetailModel)PONCCDetailBO.Instance.FindByPK(id);
                }
                pomccdetai.PONCCID = oPONCC.ID; //oPONCC.ID
                pomccdetai.ProductSaleID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductCode_));
                pomccdetai.ProductCodeOfSupplier = TextUtils.ToString(grvData.GetRowCellValue(i, colProductCodeOfSupplier));
                pomccdetai.QtyRequest = TextUtils.ToInt(grvData.GetRowCellValue(i, colQtyRequest));
                pomccdetai.Soluongcon = TextUtils.ToInt(grvData.GetRowCellValue(i, colQtyRequest));
                pomccdetai.Price = TextUtils.ToInt(grvData.GetRowCellValue(i, colPrice));
                pomccdetai.TotalPrice = TextUtils.ToInt(grvData.GetRowCellValue(i, coltotalPrice));
                pomccdetai.OrderDate = dtpOrderDate.Value;
                pomccdetai.ActualDate = TextUtils.ToDate2(dtpActualDate.Value);
                //pomccdetai.ExpectedDate = dtpExpectedDate.Value;
                pomccdetai.FeeShip = TextUtils.ToInt(grvData.GetRowCellValue(i, colFeeShip));
                pomccdetai.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNoteDetail));
                pomccdetai.VATMoney = TextUtils.ToInt(grvData.GetRowCellValue(i, colVatMoney));
                pomccdetai.VAT = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colVat));
                pomccdetai.ThanhTien = TextUtils.ToInt(grvData.GetRowCellValue(i, colThanhTien));
                pomccdetai.Discount = TextUtils.ToInt(grvData.GetRowCellValue(i, colDiscount));
                pomccdetai.RequestBuyRTCID = TextUtils.ToInt(grvData.GetRowCellValue(i, colRequestBuyRTCID));
                pomccdetai.Status = 0;//Chưa hoàn thành
                pomccdetai.CurrencyExchange = TextUtils.ToInt(grvData.GetRowCellValue(i, colCurrencyExchange)) == 0 ? TextUtils.ToInt(grvData.GetRowCellValue(i, coltotalPrice)) : TextUtils.ToInt(grvData.GetRowCellValue(i, colCurrencyExchange));
                DataTable dt = TextUtils.Select($"Exec spGetPriceHistoryPONCCDetail {pomccdetai.ProductSaleID}");
                if (dt.Rows.Count > 0)
                {
                    pomccdetai.PriceHistory = TextUtils.ToDecimal(dt.Rows[0]["Price"]);
                }
                pomccdetai.PriceSale = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colPriceSale));
                if (pomccdetai.TotalPrice != 0)
                {
                    pomccdetai.ProfitRate = pomccdetai.PriceSale / pomccdetai.TotalPrice;
                }


                if (id > 0)
                {
                    PONCCDetailBO.Instance.Update(pomccdetai);
                }
                else
                {
                    PONCCDetailBO.Instance.Insert(pomccdetai);
                }
                //Update NgayDatHang,NgayDuKienVe trong RequestBuyRTC

                if (pomccdetai.RequestBuyRTCID > 0)
                {
                    RequestBuyRTCModel buyRTCModel = (RequestBuyRTCModel)RequestBuyRTCBO.Instance.FindByPK(TextUtils.ToInt(pomccdetai.RequestBuyRTCID));
                    buyRTCModel.NgayDatHang = pomccdetai.OrderDate;
                    buyRTCModel.NgayDuKienVe = pomccdetai.ActualDate;
                    buyRTCModel.IsApproved_Level1 = true;
                    RequestBuyRTCBO.Instance.Update(buyRTCModel);
                    //TextUtils.ExcuteSQL($"exec spUpdateRequestBuyRTCbyPONCCDetail '{orderdate}','{actualdate}','{qtyreal}','{supplierid}','{requestbuyrtcid}'");
                }
            }
            //Delete PONCCDetail
            if (lstIDDelete.Count > 0)
            {
                PONCCDetailBO.Instance.Delete(lstIDDelete);
            }
            //Update RequestBuyRTC When PONCCDetail is deleted
            if (listRequestBuyRTCID.Count > 0)
            {
                foreach (var item in listRequestBuyRTCID)
                {
                    TextUtils.ExcuteSQL($"exec spDateOrderUpdateRequestBuyRTC {item[0]}");
                }
            }
            return true;
        }




        private bool ValidateForm()
        {
            if (txtPOCode.Text == "")
            {
                MessageBox.Show("Mã PONCC không được để trống", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else
            {
                DataTable dt;
                if (oPONCC.ID > 0)
                {
                    int strID = oPONCC.ID;
                    dt = TextUtils.Select("select top 1 ID from PONCC  where POCode =  '" + txtPOCode.Text.Trim() + "'" + "' and ID <> " + strID);
                }
                else
                {
                    dt = TextUtils.Select("select top 1 ID from PONCC  where POCode = '" + txtPOCode.Text.Trim() + "'");
                }
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Số phiếu này đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
                if (cboUser.Text == "")
                {
                    MessageBox.Show("Nhân viên mua hàng không được để trống!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
                if (cboSupplier.Text == "")
                {
                    MessageBox.Show("Nhà cung cấp không được để trống!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
                //if (txtPhone.Text=="")
                //{
                //    MessageBox.Show("Xin vui lòng điền số điện thoại liên hệ", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //    return false;
                //}
            }
            return true;
        }


        // load tiền trong grvData
        void calculateTotal()
        {
            // if (cGlobVar.LockEvents) return;

            grvData.CloseEditor();
            int totalPO = 0;
            grvData.Focus();
            for (int i = 0; i < grvData.RowCount; i++)
            {
                totalPO += TextUtils.ToInt(grvData.GetRowCellValue(i, coltotalPrice));
            }
            txtTotalPO.EditValue = totalPO;
        }

        private void frmBillImport_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }


        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            int qtyRequest = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colQtyRequest));
            decimal unitPrice = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colPrice));
            decimal Vat = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colVat));
            decimal Discount = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colDiscount));
            // số lượng, đơn giá -> focus đến thành tiền
            decimal s = qtyRequest * unitPrice;
            if (unitPrice > 0 && qtyRequest > 0)
            {

                if (e.Column == colQtyRequest || e.Column == colPrice)
                {
                    int a = e.RowHandle;
                    grvData.SetFocusedRowCellValue(colThanhTien, s);

                }
            }
            if (TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colThanhTien)) > 0 && Vat >= 0)
            {
                if (e.Column == colThanhTien || e.Column == colVat)
                {
                    grvData.SetFocusedRowCellValue(colVatMoney, s * Vat / 100);

                }
            }
            if (TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colVatMoney)) >= 0 && TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colThanhTien)) > 0 && TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colDiscount)) >= 0)
            {
                decimal VatMoney = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colVatMoney));
                int freeShip = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colFeeShip));
                if (e.Column == colThanhTien || e.Column == colVatMoney || e.Column == colFeeShip || e.Column == colDiscount)
                {
                    grvData.SetFocusedRowCellValue(coltotalPrice, s + VatMoney + freeShip - Discount);

                }
                calculateTotal();
            }
            decimal totalprice = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(coltotalPrice));
            decimal rate = TextUtils.ToDecimal(txtRate.EditValue);
            if (totalprice > 0 && grvData.Columns["CurrencyExchange"].Visible == true)
            {
                if (e.Column == colThanhTien || e.Column == colVatMoney || e.Column == colFeeShip || e.Column == colDiscount)
                {
                    //decimal tr= Math.Round(totalprice / rate, 1);
                    grvData.SetFocusedRowCellValue(colCurrencyExchange, totalprice * rate);// Math.Round( Math.Round(totalprice / rate,1),1)

                }
            }
            //soluongcon

            //if (qtyRequest > 0 && qtyReal > 0)
            //{
            //    if (e.Column == colQtyReal || e.Column == colQtyRequest)
            //    {
            //        grvData.SetFocusedRowCellValue(colSoluongCon, qtyRequest - qtyReal);

            //    }
            //}
        }
        private void txtRate_TextChanged(object sender, EventArgs e)
        {

            for (int i = 0; i < grvData.RowCount; i++)
            {
                decimal totalprice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, coltotalPrice));
                decimal rate = TextUtils.ToDecimal(txtRate.EditValue);
                if (totalprice > 0 && grvData.Columns["CurrencyExchange"].Visible == true)
                {
                    //decimal tr= Math.Round(totalprice / rate, 1);
                    grvData.SetFocusedRowCellValue(colCurrencyExchange, totalprice * rate);// Math.Round( Math.Round(totalprice / rate,1),1)
                }
            }
            txtRate.Focus();
        }
        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvData.CalcHitInfo(new Point(e.X, e.Y));
                if (info.Column != null && info.Column == colSTT && info.InColumnPanel)
                {
                    AddNewRow();
                }
            }
        }
        DataRow AddNewRow()
        {
            DataRow newRow = null;
            //Kiểm tra dòng cuối cùng STT = bao nhiêu?
            int STT;
            DataTable dt = (DataTable)grdData.DataSource;

            STT = dt.Rows.Count == 0 ? 1 : (TextUtils.ToInt(grvData.GetRowCellValue(dt.Rows.Count - 1, "STT")) + 1);


            if (dt.Rows.Count == 0)
                STT = 1;
            else
                STT = TextUtils.ToInt(grvData.GetRowCellValue(dt.Rows.Count - 1, "STT")) + 1;

            newRow = dt.NewRow();
            newRow["STT"] = STT;
            dt.Rows.Add(newRow);
            //newRowDataSource = dt;

            return newRow;
        }

        private void grvData_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column == colDelete)
            {
                int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIDDetail));
                int RequestBuyRTC = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colRequestBuyRTCID));
                // int qtyreal = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colQtyReal));
                string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductCode_));
                //if (ID <= 0) return;
                if (MessageBox.Show($"Bạn có chắc muốn xoá sản phẩm có mã {code} không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (ID > 0)
                    {
                        lstIDDelete.Add(ID);

                    }

                    if (RequestBuyRTC > 0)
                    {
                        ArrayList l = new ArrayList();
                        l.Add(RequestBuyRTC);  //@RequestBuyRTCID 
                        //l.Add(qtyreal);        //@QtyReal 
                        listRequestBuyRTCID.Add(l);
                    }

                    grvData.DeleteSelectedRows();
                }
            }


        }

        private void cbProduct_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit edit = sender as SearchLookUpEdit;
            int ID = TextUtils.ToInt(edit.EditValue);
            if (ID <= 0)
            {
                grvData.SetFocusedRowCellValue(colProductName_, "");
                grvData.SetFocusedRowCellValue(colUnit, "");
                return;
            }

            int rowHandle = edit.Properties.GetIndexByKeyValue(ID);
            object row = edit.Properties.View.GetRow(rowHandle);
            string ProductName = (row as DataRowView).Row["ProductName"].ToString();
            string Unit = (row as DataRowView).Row["Unit"].ToString();
            grvData.SetFocusedRowCellValue(colProductName_, ProductName);
            grvData.SetFocusedRowCellValue(colUnit, Unit);
        }



        string SupplierCode = "";
        private void cboSupplier_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit edit = sender as SearchLookUpEdit;
            int IDSupplier = TextUtils.ToInt(edit.EditValue);
            int rowHandle = edit.Properties.GetIndexByKeyValue(IDSupplier);
            //object row = edit.Properties.GetRowByKeyValue(edit.EditValue);
            object row = edit.Properties.View.GetRow(rowHandle);


            //string PhoneNCC = (row as DataRowView).Row["PhoneNCC"].ToString();
            //string NVPhuTrach = (row as DataRowView).Row["NVPhuTrach"].ToString();
            // string PhoneNCC = (row as DataRowView).Row["PhoneNCC"].ToString();
            //string email= (row as DataRowView).Row["Email"].ToString();

            //txtPhone.Text = PhoneNCC;
            //txtEmail.Text = email;
            if (oPONCC.ID <= 0)
            {
                if (edit.EditValue == null)
                {
                    txtPOCode.Text = "";
                    return;
                }

                SupplierCode = (row as DataRowView).Row["CodeNCC"].ToString();
                loadBilllNumber();
            }
        }
        void loadBilllNumber()
        {

            string _year = DateTime.Now.Year.ToString();
            string _month = DateTime.Now.Month < 10 ? ("0" + DateTime.Now.Month.ToString()) : DateTime.Now.Month.ToString();

            string maPO = _month + _year + "-" + SupplierCode + "-";

            string code = TextUtils.ToString(TextUtils.ExcuteScalar($"Exec spGetPOCodeInPONCC '{maPO.Trim()}'"));
            // string[] arr = code.Split('.');
            if (code.Length <= 0)
            {
                txtPOCode.Text = maPO + "001";
                return;
            }
            string arr = code.Substring(code.Length - 3, 3);
            int number = TextUtils.ToInt(arr);
            string so = number + 1 < 10 ? so = ("00" + (number + 1).ToString()) :
                        number + 1 < 100 ? so = ("0" + (number + 1).ToString()) : so = (number + 1).ToString();
            txtPOCode.Text = maPO + TextUtils.ToString(so);
        }


        private void grvData_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == colVat)
            {
                e.DisplayText = TextUtils.ToDecimal(e.Value) + "%";
            }
            if (e.Column == colCurrencyExchange)
            {
                e.DisplayText = TextUtils.ToInt(e.Value) + " VNĐ";
            }
            if (e.Column == coltotalPrice)
            {
                e.DisplayText = cbTypeMoney.SelectedIndex == 1 ? ("$" + TextUtils.ToInt(e.Value)) :
                                 cbTypeMoney.SelectedIndex == 2 ? (TextUtils.ToInt(e.Value) + "€") :
                                  cbTypeMoney.SelectedIndex == 3 ? "¥" + (TextUtils.ToInt(e.Value)) :
                                  cbTypeMoney.SelectedIndex == 4 ? "¥" + (TextUtils.ToInt(e.Value)) : (TextUtils.ToInt(e.Value)) + "VND";
            }
        }

        private void grvData_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName == "QtyReal")
            {
                int qtyrequest = TextUtils.ToInt(view.GetFocusedRowCellValue(colQtyRequest));
                if (TextUtils.ToInt(e.Value) > qtyrequest)
                {
                    e.Valid = false;
                    e.ErrorText = "Số lượng thực phải nhỏ hơn số lượng yêu cầu";
                    Show();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbTypeMoney_SelectedIndexChanged(object sender, EventArgs e)
        {
            grvData.Columns["CurrencyExchange"].Visible = cbTypeMoney.SelectedIndex == 0 ? false : true;
        }


    }
}
