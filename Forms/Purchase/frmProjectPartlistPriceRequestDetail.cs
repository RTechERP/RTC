using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
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
    public partial class frmProjectPartlistPriceRequestDetail : _Forms
    {
        public ProjectPartlistPriceRequestModel priceRequest = new ProjectPartlistPriceRequestModel();
        public int projectID = 0;

        //public List<ProjectPartlistPriceRequestModel> priceRequests = new List<ProjectPartlistPriceRequestModel>();

        public DataTable dtEdit = new DataTable();
        public frmProjectPartlistPriceRequestDetail()
        {
            InitializeComponent();
        }

        private void frmProjectPartlistPriceRequestDetail_Load(object sender, EventArgs e)
        {
            //LoadProject();
            //LoadProduct();
            //LoadEmployee();
            LoadSupplierSale();
            LoadCurrency();

            LoadData();
            //LoadData();

            //var items = bar2.ItemLinks;
            //var items = barManager1.doc.Items;
        }

        void LoadData(DataRowView data)
        {
            try
            {
                ////cboProductCode.EditValue = priceRequest.ID;
                //chkIsCheckPrice.Checked = TextUtils.ToBoolean(priceRequest.IsCheckPrice);
                //txtProductName.Text = priceRequest.ProductName;
                //txtQuantity.EditValue = TextUtils.ToDecimal(priceRequest.Quantity);
                //cboStatusRequest.SelectedIndex = TextUtils.ToInt(priceRequest.StatusRequest);
                //cboEmployee.EditValue = priceRequest.EmployeeID;
                //dtpDateRequest.Value = priceRequest.DateRequest.HasValue ? priceRequest.DateRequest.Value : DateTime.Now;
                //dtpDeadline.Value = priceRequest.Deadline.HasValue ? priceRequest.Deadline.Value : DateTime.Now;
                ////
                //dtpDatePriceQuote.Value = priceRequest.DatePriceQuote.HasValue ? priceRequest.DatePriceQuote.Value : DateTime.Now;
                //cboQuoteEmployee.EditValue = priceRequest.QuoteEmployeeID;
                //cboCurrency.EditValue = priceRequest.CurrencyID;
                //txtCurrencyRate.EditValue = priceRequest.CurrencyRate;
                //txtHistoryPrice.EditValue = TextUtils.ToDecimal(priceRequest.HistoryPrice);
                //txtUnitPrice.EditValue = TextUtils.ToDecimal(priceRequest.UnitPrice);
                //txtTotalPrice.EditValue = TextUtils.ToDecimal(priceRequest.TotalPrice);
                //txtTotalPriceExchange.EditValue = TextUtils.ToDecimal(priceRequest.TotalPriceExchange);
                //txtVAT.Value = TextUtils.ToDecimal(priceRequest.VAT);
                //txtTotaMoneyVAT.EditValue = TextUtils.ToDecimal(priceRequest.TotaMoneyVAT);
                //cboSupplierSale.EditValue = priceRequest.SupplierSaleID;
                //txtTotalDayLeadTime.Value = TextUtils.ToDecimal(priceRequest.TotalDayLeadTime);
                //dtpDateExpected.Value = priceRequest.DateExpected.HasValue ? priceRequest.DateExpected.Value : DateTime.Now;
                //txtNote.Text = priceRequest.Note;
                ////
                //chkIsImport.Checked = TextUtils.ToBoolean(priceRequest.IsImport);
                //txtUnitFactoryExportPrice.EditValue = TextUtils.ToDecimal(priceRequest.UnitFactoryExportPrice);
                //txtUnitImportPrice.EditValue = TextUtils.ToDecimal(priceRequest.UnitImportPrice);
                //txtTotalImportPrice.EditValue = TextUtils.ToDecimal(priceRequest.TotalImportPrice);
                //txtLeadTime.Text = priceRequest.LeadTime;

                //using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang load dữ liệu"))
                //{

                if (data != null)
                {
                    //cboProductCode.EditValue = priceRequest.ID;
                    chkIsCheckPrice.Checked = TextUtils.ToBoolean(data["IsCheckPrice"]);
                    txtProductName.Text = TextUtils.ToString(data["ProductName"]);
                    txtQuantity.EditValue = TextUtils.ToDecimal(data["Quantity"]);
                    cboStatusRequest.SelectedIndex = TextUtils.ToInt(data["StatusRequest"]);
                    cboEmployee.EditValue = TextUtils.ToInt(data["EmployeeID"]);
                    dtpDateRequest.Value = TextUtils.ToDate5(data["DateRequest"]);
                    dtpDeadline.Value = TextUtils.ToDate5(data["Deadline"]);
                    //
                    dtpDatePriceQuote.Value = TextUtils.ToDate4(data["DatePriceQuote"]).HasValue ? TextUtils.ToDate4(data["DatePriceQuote"]).Value : DateTime.Now;
                    cboQuoteEmployee.EditValue = TextUtils.ToInt(data["QuoteEmployeeID"]);
                    cboCurrency.EditValue = TextUtils.ToInt(data["CurrencyID"]);
                    txtCurrencyRate.EditValue = TextUtils.ToBoolean(data["CurrencyRate"]);
                    txtHistoryPrice.EditValue = TextUtils.ToDecimal(data["HistoryPrice"]);
                    txtUnitPrice.EditValue = TextUtils.ToDecimal(data["UnitPrice"]);
                    txtTotalPrice.EditValue = TextUtils.ToDecimal(data["TotalPrice"]);
                    txtTotalPriceExchange.EditValue = TextUtils.ToDecimal(data["TotalPriceExchange"]);
                    txtVAT.Value = TextUtils.ToDecimal(data["VAT"]);
                    txtTotaMoneyVAT.EditValue = TextUtils.ToDecimal(data["TotaMoneyVAT"]);
                    cboSupplierSale.EditValue = TextUtils.ToInt(data["SupplierSaleID"]);
                    txtTotalDayLeadTime.Value = TextUtils.ToDecimal(data["TotalDayLeadTime"]);
                    dtpDateExpected.Value = TextUtils.ToDate5(data["DateExpected"]);
                    txtNote.Text = TextUtils.ToString(data["Note"]);
                    //
                    chkIsImport.Checked = TextUtils.ToBoolean(data["IsImport"]);
                    txtUnitFactoryExportPrice.EditValue = TextUtils.ToDecimal(data["UnitFactoryExportPrice"]);
                    txtUnitImportPrice.EditValue = TextUtils.ToDecimal(data["UnitImportPrice"]);
                    txtTotalImportPrice.EditValue = TextUtils.ToDecimal(data["TotalImportPrice"]);
                    txtLeadTime.Text = TextUtils.ToString(data["LeadTime"]);
                    //
                    //cboProductCode.EditValue = TextUtils.ToString(data["LeadTime"]);
                    txtTT.Text = TextUtils.ToString(data["TT"]);
                    txtUnitCount.Text = TextUtils.ToString(data["UnitCount"]);
                    txtReasonDeleted.Text = TextUtils.ToString(data["ReasonDeleted"]);
                    txtModel.Text = TextUtils.ToString(data["Model"]);
                    txtManufacturer.Text = TextUtils.ToString(data["Manufacturer"]);
                    cboUserSale.EditValue = TextUtils.ToInt(data["UserID"]);
                    txtQuantity.EditValue = TextUtils.ToString(data["Quantity"]);
                    txtCurrencyRate.EditValue = TextUtils.ToString(data["CurrencyRate"]);
                    txtNotePartlist.Text = TextUtils.ToString(data["NotePartlist"]);


                    this.Text = $"CHI TIẾT YÊU CẦU BÁO GIÁ - {TextUtils.ToString(data["ProductCode"])}";
                }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo");
            }
        }

        void LoadData()
        {
            grdData.DataSource = dtEdit;

            var currencyID = dtEdit.Compute("max([CurrencyID])", "");
            var historyPrice = dtEdit.Compute("max([HistoryPrice])", "");
            var unitPrice = dtEdit.Compute("max([UnitPrice])", "");
            var vat = dtEdit.Compute("max([VAT])", "");
            var totalDayLeadTime = dtEdit.Compute("max([TotalDayLeadTime])", "");
            var supplierSaleID = dtEdit.Compute("max([SupplierSaleID])", "");
            var note = dtEdit.Compute("max([Note])", "");

            cboCurrency.EditValue = currencyID;
            //txtCurrencyRate.EditValue = 1;
            txtHistoryPrice.EditValue = historyPrice;
            txtUnitPrice.EditValue = unitPrice;
            txtVAT.Value = TextUtils.ToDecimal(vat);
            txtTotalDayLeadTime.Value = TextUtils.ToDecimal(totalDayLeadTime);
            cboSupplierSale.EditValue = TextUtils.ToInt(supplierSaleID);
            txtNote.Text = TextUtils.ToString(note);


            cboCurrency.Focus();
        }

        void LoadProject()
        {
            var list = SQLHelper<ProjectModel>.FindAll().OrderByDescending(x => x.CreatedDate).ToList();
            cboProject.Properties.ValueMember = "ID";
            cboProject.Properties.DisplayMember = "ProjectName";
            cboProject.Properties.DataSource = list;

            cboProject.EditValue = projectID;
        }

        void LoadProduct()
        {

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang load dữ liệu"))
            {


                DateTime dateStart = new DateTime(2024, 1, 1, 0, 0, 0);
                DateTime dateEnd = DateTime.Now;
                int statusRequest = 0;
                int projectId = TextUtils.ToInt(cboProject.EditValue);
                string keyword = "";
                int isDeleted = 0;

                DataTable dt = TextUtils.LoadDataFromSP("spGetProjectPartlistPriceRequest_New", "A",
                                                                new string[] { "@DateStart", "@DateEnd", "@StatusRequest", "@ProjectID", "@Keyword", "@IsDeleted" },
                                                                new object[] { dateStart, dateEnd, statusRequest, projectId, keyword, isDeleted });

                cboProductCode.Properties.ValueMember = "ID";
                cboProductCode.Properties.DisplayMember = "ProductCode";
                cboProductCode.Properties.DataSource = dt;

                cboProductCode.EditValue = priceRequest.ID;
            }
        }

        void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });

            //Load người phụ trách Sale
            cboUserSale.Properties.ValueMember = "UserID";
            cboUserSale.Properties.DisplayMember = "FullName";
            cboUserSale.Properties.DataSource = dt;

            //Load nv báo giá
            cboQuoteEmployee.Properties.ValueMember = "ID";
            cboQuoteEmployee.Properties.DisplayMember = "FullName";
            cboQuoteEmployee.Properties.DataSource = dt;

            //Load người yêu cầu
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;
        }


        void LoadSupplierSale()
        {
            List<SupplierSaleModel> list = SQLHelper<SupplierSaleModel>.FindAll().OrderByDescending(x => x.NgayUpdate).ToList();
            cboSupplierSale.Properties.ValueMember = "ID";
            cboSupplierSale.Properties.DisplayMember = "NameNCC";
            cboSupplierSale.Properties.DataSource = list;
        }

        void LoadCurrency()
        {
            List<CurrencyModel> list = SQLHelper<CurrencyModel>.FindAll();
            cboCurrency.Properties.ValueMember = "ID";
            cboCurrency.Properties.DisplayMember = "Code";
            cboCurrency.Properties.DataSource = list;
        }


        decimal CalculatorTotalMoneyExchange(int rowHandle)
        {
            decimal totalMoney = TextUtils.ToDecimal(grvData.GetRowCellValue(rowHandle,colTotalPrice));
            decimal currencyRate = TextUtils.ToDecimal(txtCurrencyRate.EditValue);
            decimal totalMoneyExchange = totalMoney * currencyRate;
            return totalMoneyExchange;
        }

        DateTime AddWeekdays(DateTime date, int days)
        {
            int count = 0;
            while (count < days)
            {
                date = date.AddDays(1);
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    count++;
                }
            }
            return date;
        }

        private bool SaveData()
        {
            try
            {
                //int priceID = TextUtils.ToInt(cboProductCode.EditValue);
                ////ProjectPartlistPriceRequestModel priceRequest = SQLHelper<ProjectPartlistPriceRequestModel>.FindByID(priceID);
                //priceRequest.IsCheckPrice = TextUtils.ToBoolean(chkIsCheckPrice.Checked);
                //priceRequest.ProductName = txtProductName.Text.Trim();
                //priceRequest.Quantity = TextUtils.ToDecimal(txtQuantity.EditValue);
                //priceRequest.StatusRequest = TextUtils.ToInt(cboStatusRequest.SelectedIndex);
                //priceRequest.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
                //priceRequest.DateRequest = dtpDateRequest.Value;
                //priceRequest.Deadline = dtpDeadline.Value;
                //priceRequest.Deadline = dtpDatePriceQuote.Value;
                //priceRequest.QuoteEmployeeID = TextUtils.ToInt(cboQuoteEmployee.EditValue);
                //priceRequest.CurrencyID = TextUtils.ToInt(cboCurrency.EditValue);
                //priceRequest.CurrencyRate = TextUtils.ToDecimal(txtCurrencyRate.EditValue);
                //priceRequest.HistoryPrice = TextUtils.ToDecimal(txtHistoryPrice.EditValue);
                //priceRequest.UnitPrice = TextUtils.ToDecimal(txtUnitPrice.EditValue);
                //priceRequest.TotalPrice = TextUtils.ToDecimal(txtTotalPrice.EditValue);
                //priceRequest.TotalPriceExchange = TextUtils.ToDecimal(txtTotalPriceExchange.EditValue);
                //priceRequest.VAT = TextUtils.ToDecimal(txtVAT.Value);
                //priceRequest.TotaMoneyVAT = TextUtils.ToDecimal(txtTotaMoneyVAT.EditValue);
                //priceRequest.SupplierSaleID = TextUtils.ToInt(cboSupplierSale.EditValue);
                //priceRequest.TotalDayLeadTime = TextUtils.ToInt(txtTotalDayLeadTime.Value);
                //priceRequest.DateExpected = dtpDateExpected.Value;
                //priceRequest.Note = txtNote.Text.Trim();
                //priceRequest.IsImport = TextUtils.ToBoolean(chkIsImport.Checked);
                //priceRequest.UnitFactoryExportPrice = TextUtils.ToDecimal(txtUnitFactoryExportPrice.EditValue);
                //priceRequest.UnitImportPrice = TextUtils.ToDecimal(txtUnitImportPrice.EditValue);
                //priceRequest.TotalImportPrice = TextUtils.ToDecimal(txtTotalImportPrice.EditValue);
                //priceRequest.LeadTime = txtLeadTime.Text.Trim();


                //if (priceRequest.ID > 0) SQLHelper<ProjectPartlistPriceRequestModel>.Update(priceRequest);
                //else priceRequest.ID = SQLHelper<ProjectPartlistPriceRequestModel>.Insert(priceRequest);

                foreach (DataRow row in dtEdit.Rows)
                {
                    decimal unitPrice = TextUtils.ToDecimal(txtUnitPrice.EditValue);
                    decimal totalPrice = TextUtils.ToDecimal(row["Quantity"]) * unitPrice;

                    decimal currencyRate = TextUtils.ToDecimal(txtCurrencyRate.EditValue);
                    decimal totalPriceExchange = totalPrice * currencyRate;

                    decimal vat = TextUtils.ToDecimal(txtVAT.Value);
                    decimal totalMoneyVAT = totalPrice + ((totalPrice * vat) / 100);
                    var myDict = new Dictionary<string, object>()
                    {
                        {ProjectPartlistPriceRequestModel_Enum.UnitPrice.ToString(),  unitPrice},
                        {ProjectPartlistPriceRequestModel_Enum.Note.ToString(), txtNote.Text.Trim() },
                        {ProjectPartlistPriceRequestModel_Enum.SupplierSaleID.ToString(), TextUtils.ToInt(cboSupplierSale.EditValue) },
                        {ProjectPartlistPriceRequestModel_Enum.IsImport.ToString(), chkIsImport.Checked },
                        {ProjectPartlistPriceRequestModel_Enum.CurrencyID.ToString(), TextUtils.ToInt(cboCurrency.EditValue) },
                        {ProjectPartlistPriceRequestModel_Enum.HistoryPrice.ToString(), TextUtils.ToDecimal(txtHistoryPrice.EditValue) },
                        {ProjectPartlistPriceRequestModel_Enum.UnitFactoryExportPrice.ToString(), TextUtils.ToDecimal(txtUnitFactoryExportPrice.EditValue) },
                        {ProjectPartlistPriceRequestModel_Enum.UnitImportPrice.ToString(), TextUtils.ToDecimal(txtUnitImportPrice.EditValue) },
                        {ProjectPartlistPriceRequestModel_Enum.TotalImportPrice.ToString(), TextUtils.ToDecimal(txtTotalImportPrice.EditValue) },
                        {ProjectPartlistPriceRequestModel_Enum.VAT.ToString(), vat },
                        {ProjectPartlistPriceRequestModel_Enum.TotalDayLeadTime.ToString(), TextUtils.ToInt(txtTotalDayLeadTime.Value) },
                        {ProjectPartlistPriceRequestModel_Enum.CurrencyRate.ToString(),  currencyRate},
                        {ProjectPartlistPriceRequestModel_Enum.TotalPrice.ToString(), totalPrice },
                        {ProjectPartlistPriceRequestModel_Enum.TotalPriceExchange.ToString(), totalPriceExchange },
                        {ProjectPartlistPriceRequestModel_Enum.TotaMoneyVAT.ToString(),totalMoneyVAT },
                        {ProjectPartlistPriceRequestModel_Enum.UpdatedDate.ToString(), DateTime.Now },
                        {ProjectPartlistPriceRequestModel_Enum.UpdatedBy.ToString(), Global.LoginName },
                        {ProjectPartlistPriceRequestModel_Enum.DateExpected.ToString(), dtpDateExpected.Value},
                    };

                    if (!Global.IsAdmin) myDict.Add(ProjectPartlistPriceRequestModel_Enum.QuoteEmployeeID.ToString(), Global.EmployeeID);

                    int id = TextUtils.ToInt(row["ID"]);
                    SQLHelper<ProjectPartlistPriceRequestModel>.UpdateFieldsByID(myDict, id);
                }
                


                //string idText = string.Join(",", priceRequests.Select(x => x.ID));
                //var exp = new Expression(ProjectPartlistPriceRequestModel_Enum.ID, idText, "IN");
                //SQLHelper<ProjectPartlistPriceRequestModel>.UpdateFields(myDict, exp);


                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo");
                return false;
            }
        }


        void QuotePrice(int statusRequest)
        {

            //grvData.CloseEditor();
            //grvData.FocusedRowHandle = -1;
            string statusRequestText = statusRequest == 1 ? "huỷ báo giá" : "báo giá";


            //Check validate

            int id = TextUtils.ToInt(cboProductCode.EditValue);
            if (id <= 0) return;
            string productCode = cboProductCode.Text;

            int currencyId = TextUtils.ToInt(cboCurrency.EditValue);
            string currencyCode = cboCurrency.Text;
            decimal currencyRate = TextUtils.ToDecimal(txtCurrencyRate.EditValue);
            decimal unitPrice = TextUtils.ToDecimal(txtUnitPrice.EditValue);
            int supplierSaleId = TextUtils.ToInt(cboSupplierSale.EditValue);
            string supplierSaleName = cboSupplierSale.Text.Trim();

            if (currencyId <= 0)
            {
                MessageBox.Show($"Vui lòng nhập Loại tiền mã sản phẩm [{productCode}]!", "Thông báo");
                return;
            }
            else if (currencyRate <= 0)
            {
                MessageBox.Show($"Tỷ giá của [{currencyCode}] phải > 0.\nVui lòng kiểm tra lại Ngày hết hạn!", "Thông báo");
                return;
            }
            if (unitPrice <= 0)
            {
                MessageBox.Show($"Vui lòng nhập Đơn giá mã sản phẩm [{productCode}]!", "Thông báo");
                return;
            }
            if (supplierSaleId <= 0 || string.IsNullOrWhiteSpace(supplierSaleName))
            {
                MessageBox.Show($"Vui lòng nhập Nhà cung cấp mã sản phẩm [{productCode}]!", "Thông báo");
                return;
            }



            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn {statusRequestText} sản phẩm [{productCode}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                SaveData();

                //Add thông báo
                string productName = txtProductName.Text.Trim();
                string project = cboProject.Text;
                string textNotify = $"Dự án: {project}" +
                                    $"Mã sản phầm: {productCode}\n" +
                                    $"Tên sản phầm: {productName}";

                int employee = TextUtils.ToInt(cboEmployee.EditValue);
                TextUtils.AddNotify($"{statusRequestText} PARTLIST".ToUpper(), textNotify, employee);


                //Update bảng báo giá
                var myDict = new Dictionary<string, object>()
                    {
                        {ProjectPartlistPriceRequestModel_Enum.StatusRequest.ToString(),statusRequest},
                        {ProjectPartlistPriceRequestModel_Enum.DatePriceQuote.ToString(),DateTime.Now},
                        {ProjectPartlistPriceRequestModel_Enum.UpdatedDate.ToString(),DateTime.Now},
                        {ProjectPartlistPriceRequestModel_Enum.UpdatedBy.ToString(),Global.LoginName}
                    };

                bool isCheckPrice = statusRequest == 2;
                if (isCheckPrice) myDict.Add(ProjectPartlistPriceRequestModel_Enum.IsCheckPrice.ToString(), isCheckPrice);
                if (!Global.IsAdmin) myDict.Add(ProjectPartlistPriceRequestModel_Enum.QuoteEmployeeID.ToString(), Global.EmployeeID);
                SQLHelper<ProjectPartlistPriceRequestModel>.UpdateFieldsByID(myDict, id);

            }
        }

        private void cboProductCode_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView data = (DataRowView)cboProductCode.GetSelectedDataRow();

            //string tt = "", unitCount = "", reasonDeleted = "", model = "", manufacturer = "";
            //int userID = 0;
            //decimal quantity = 0;
            //int id = 0;

            //if (data != null)
            //{
            //    tt = TextUtils.ToString(data["TT"]);
            //    unitCount = TextUtils.ToString(data["UnitCount"]);
            //    reasonDeleted = TextUtils.ToString(data["ReasonDeleted"]);
            //    model = TextUtils.ToString(data["Model"]);
            //    manufacturer = TextUtils.ToString(data["Manufacturer"]);
            //    userID = TextUtils.ToInt(data["UserID"]);
            //    quantity = TextUtils.ToInt(data["Quantity"]);
            //    id = TextUtils.ToInt(data["ID"]);


            //}
            //cboProductCode.EditValue = id;
            //txtTT.Text = tt;
            //txtUnitCount.Text = unitCount;
            //txtReasonDeleted.Text = reasonDeleted;
            //txtModel.Text = model;
            //txtManufacturer.Text = manufacturer;
            //cboUserSale.EditValue = userID;
            //txtQuantity.EditValue = quantity;

            //priceRequest = SQLHelper<ProjectPartlistPriceRequestModel>.FindByID(id);
            //LoadData();

            LoadData(data);
        }

        private void cboCurrency_EditValueChanged(object sender, EventArgs e)
        {
            CurrencyModel currency = (CurrencyModel)cboCurrency.GetSelectedDataRow();
            currency = currency ?? new CurrencyModel();
            //validate ngày bắt đầu < ngày hết hạn

            if (currency.DateExpried.HasValue && currency.DateStart.HasValue)
            {
                bool isExpried = ((currency.DateExpried.Value.Date < DateTime.Now.Date || currency.DateStart.Value.Date > DateTime.Now.Date) && currency.Code.ToLower().Trim() != "vnd");

                txtCurrencyRate.EditValue = currency.CurrencyRate;
                if (isExpried) txtCurrencyRate.EditValue = 0;
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    //txtTotalPriceExchange.EditValue = CalculatorTotalMoneyExchange(i);

                    grvData.SetRowCellValue(i, colTotalPriceExchange, CalculatorTotalMoneyExchange(i));
                }
            }

        }

        private void cboCurrency_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Search)
            {
                LoadCurrency();
                cboCurrency_EditValueChanged(null, null);
                //CalculatorTotalPrice();
            }
        }

        private void txtUnitPrice_ValueChanged(object sender, EventArgs e)
        {
            decimal unitPrice = TextUtils.ToDecimal(txtUnitPrice.EditValue);
            decimal vat = TextUtils.ToDecimal(txtVAT.Value);

            for (int i = 0; i < grvData.RowCount; i++)
            {
                decimal quantity = TextUtils.ToDecimal(grvData.GetRowCellValue(i,colQuantity));

                decimal totalPrice = quantity * unitPrice;

                decimal totalMoneyVAT = totalPrice + ((totalPrice * vat) / 100);
                //txtTotaMoneyVAT.EditValue = totalMoneyVAT;
                //txtTotalPrice.EditValue = totalPrice;


                grvData.SetRowCellValue(i, colTotalPrice, totalPrice);
                grvData.SetRowCellValue(i, colTotaMoneyVAT, totalMoneyVAT);
                grvData.SetRowCellValue(i, colTotalPriceExchange, CalculatorTotalMoneyExchange(i));
            }
        }

        private void txtVAT_ValueChanged(object sender, EventArgs e)
        {

            //decimal totalPrice = TextUtils.ToDecimal(txtTotalPrice.EditValue);
            decimal unitPrice = TextUtils.ToDecimal(txtUnitPrice.EditValue);
            decimal vat = TextUtils.ToDecimal(txtVAT.Value);
            //decimal totalMoneyVAT = totalPrice + ((totalPrice * vat) / 100);
            //txtTotaMoneyVAT.EditValue = totalMoneyVAT;


            for (int i = 0; i < grvData.RowCount; i++)
            {
                decimal quantity = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQuantity));

                decimal totalPrice = quantity * unitPrice;

                decimal totalMoneyVAT = totalPrice + ((totalPrice * vat) / 100);
                //txtTotaMoneyVAT.EditValue = totalMoneyVAT;
                //txtTotalPrice.EditValue = totalPrice;


                grvData.SetRowCellValue(i, colTotaMoneyVAT, totalMoneyVAT);
            }
        }

        private void txtTotalDayLeadTime_ValueChanged(object sender, EventArgs e)
        {
            int leadTime = TextUtils.ToInt(txtTotalDayLeadTime.Value);
            dtpDateExpected.Value = AddWeekdays(DateTime.Now.AddDays(+1), leadTime);
        }

        private void txtUnitImportPrice_ValueChanged(object sender, EventArgs e)
        {
            decimal quantity = TextUtils.ToDecimal(txtQuantity.EditValue);
            decimal unitImportPrice = TextUtils.ToDecimal(txtUnitImportPrice.EditValue);
            decimal totalPriceImport = quantity * unitImportPrice;

            txtTotalImportPrice.EditValue = totalPriceImport;
        }

        private void btnAddSupplierSale_Click(object sender, EventArgs e)
        {
            frmSupplierSaleDetail frm = new frmSupplierSaleDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadSupplierSale();
            }
        }

        private void cboProject_EditValueChanged(object sender, EventArgs e)
        {
            LoadProduct();
            //cboProductCode.EditValue = 0;
        }

        private void btnSaveClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (SaveData())
            {
                //this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnSaveNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (SaveData())
            {
                LoadProduct();
                cboProductCode.EditValue = 0;
                //LoadData();
            }
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveData();
        }

        private void btnQuotePrice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            QuotePrice(2);
        }

        private void btnUnQuotePrice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            QuotePrice(1);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int[] rowSelecteds = grvData.GetSelectedRows();
            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn xóa sản phẩm đã chọn khỏi danh sách không?", "Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                if (rowSelecteds.Length <= 0)
                {
                    grvData.DeleteRow(grvData.FocusedRowHandle);
                }
                else
                {
                    //foreach (int row in rowSelecteds)
                    //{

                    //    grvData.DeleteRow(row);
                    //}


                    for (int i = rowSelecteds.Length-1; i >= 0; i--)
                    {
                        grvData.DeleteRow(rowSelecteds[i]);
                    }
                }

                dtEdit.AcceptChanges();
            }
            
        }
    }
}
