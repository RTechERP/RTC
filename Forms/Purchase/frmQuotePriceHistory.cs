using BMS.Model;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmQuotePriceHistory : _Forms
    {
        public ProjectPartlistPriceRequestModel modelPPLPR = new ProjectPartlistPriceRequestModel();
        //public int STT = 0;
        ArrayList lstIDDelete = new ArrayList();
        private bool checkRole = false;

        public string tt = "";
        public string unitCount = "";

        //public object priceRequests = new
        //{
        //    request = new ProjectPartlistPriceRequestModel(),
        //    tt = "",
        //    unitCount = ""
        //};

        public frmQuotePriceHistory()
        {
            InitializeComponent();
        }

        private void frmQuotePriceHistory_Load(object sender, EventArgs e)
        {
            LoadSupplierSale();
            LoadCurrency();
            LoadEmployee();
            LoadData();
            LoadCheckRole();
        }

        private void LoadData()
        {
            grvData.FocusedRowHandle = -1;
            txtSTT.Text = tt;
            txtProductCode.Text = modelPPLPR.ProductCode;
            txtProductName.Text = modelPPLPR.ProductName;
            cboStatusRequest.SelectedIndex = TextUtils.ToInt(modelPPLPR.StatusRequest);
            dtpDateRequest.Value = (DateTime)modelPPLPR.DateRequest;
            dtpDeadline.Value = (DateTime)modelPPLPR.Deadline;

            txtQuantity.Value = TextUtils.ToDecimal(modelPPLPR.Quantity);
            txtUnitCount.Text = unitCount;

            int ProjectPartlistPriceRequestID = TextUtils.ToInt(modelPPLPR.ID);
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectPartlistPriceRequestHistory", "A", new string[] { "@ProjectPartlistPriceRequestID" }, new object[] { ProjectPartlistPriceRequestID });
            grdData.DataSource = dt;
        }

        void LoadSupplierSale()
        {
            List<SupplierSaleModel> list = SQLHelper<SupplierSaleModel>.FindAll().OrderByDescending(x => x.ID).ToList();
            cboSupplierSale.ValueMember = "ID";
            cboSupplierSale.DisplayMember = "NameNCC";
            cboSupplierSale.DataSource = list;
        }

        void LoadCurrency()
        {
            List<CurrencyModel> list = SQLHelper<CurrencyModel>.FindAll();
            cboCurrency.ValueMember = "ID";
            cboCurrency.DisplayMember = "Code";
            cboCurrency.DataSource = list;
        }

        void LoadEmployee()
        {
            List<EmployeeModel> list = SQLHelper<EmployeeModel>.FindByAttribute("Status", 0);
            cboEmployee.ValueMember = "ID";
            cboEmployee.DisplayMember = "FullName";
            cboEmployee.DataSource = list;

            int id = TextUtils.ToInt(modelPPLPR.EmployeeID);
            EmployeeModel modelEmployee = SQLHelper<EmployeeModel>.FindByID(id);
            if (modelEmployee != null)
            {
                txtFullName.Text = modelEmployee.FullName;
            }
        }

        private void LoadCheckRole()
        {
            checkRole = Global.IsAdmin ? true : Global.IsAdminSale ? true : false;

            if (!checkRole)
            {
                btnIsSelectedQuote.Visible = false;
                btnUnIsSelectedQuote.Visible = false;
                colIsSelectedQuote.Visible = false;
                toolStripSeparator1.Visible = false;
                toolStripSeparator3.Visible = false;

                foreach (DevExpress.XtraGrid.Columns.GridColumn column in grvData.Columns)
                {
                    if (column.FieldName != "Note")
                    {
                        column.OptionsColumn.AllowEdit = false;
                    }
                }
            }

        }


        private void grvData_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                e.Value = grvData.GetRowHandle(e.ListSourceRowIndex) + 1;
            }
        }

        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo info = grvData.CalcHitInfo(new Point(e.X, e.Y));
            if (info.Column == colSTT && e.Y < 40 && checkRole)
            {
                DataTable dt = (DataTable)grdData.DataSource;

                DataRow dtrow = dt.NewRow();
                if (dt.Rows.Count <= 0)
                {
                    dtrow["ProjectPartlistPriceRequestID"] = modelPPLPR.ID;
                    dtrow["UnitPrice"] = modelPPLPR.UnitPrice;
                    dtrow["TotalPrice"] = modelPPLPR.TotalPrice;
                    dtrow["Unit"] = modelPPLPR.Unit;
                    dtrow["SupplierSaleID"] = modelPPLPR.SupplierSaleID;
                    dtrow["Note"] = modelPPLPR.Note;
                    dtrow["DatePriceQuote"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dtrow["TotalPriceExchange"] = modelPPLPR.TotalPriceExchange;
                    dtrow["CurrencyRate"] = modelPPLPR.CurrencyRate;
                    dtrow["CurrencyID"] = modelPPLPR.CurrencyID;
                    dtrow["HistoryPrice"] = modelPPLPR.HistoryPrice;
                    dtrow["LeadTime"] = modelPPLPR.LeadTime;
                    dtrow["UnitImportPrice"] = modelPPLPR.UnitImportPrice;
                    dtrow["TotalImportPrice"] = modelPPLPR.TotalImportPrice;
                    dtrow["IsImport"] = modelPPLPR.IsImport;
                    dtrow["QuoteEmployeeID"] = Global.EmployeeID;
                }
                else
                {
                    dtrow["DatePriceQuote"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dtrow["QuoteEmployeeID"] = Global.EmployeeID;
                }
                dt.Rows.Add(dtrow);

                grdData.DataSource = dt;
            }
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                LoadData();
            }
        }

        private bool CheckValidate()
        {
            for (int i = 0; i < grvData.RowCount; i++)
            {
                int stt = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));
                decimal UnitPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colUnitPrice));
                int SupplierSaleID = TextUtils.ToInt(grvData.GetRowCellValue(i, colSupplierSaleID));
                string DatePriceQuote = TextUtils.ToDate5(grvData.GetRowCellValue(i, colDatePriceQuote)).ToString("yyyy-MM-dd HH:mm:ss");
                int CurrencyID = TextUtils.ToInt(grvData.GetRowCellValue(i, colCurrencyID));
                decimal currencyRate = TextUtils.ToInt(grvData.GetRowCellValue(i, colCurrencyRate));
                string currencyCode = TextUtils.ToString(grvData.GetRowCellDisplayText(i, colCurrencyID)).Trim();

                if (DatePriceQuote == "")
                {
                    MessageBox.Show($"Ngày báo giá dòng [{stt}] không được để trống!", "Thông báo");
                    return false;
                }
                if (CurrencyID <= 0)
                {
                    MessageBox.Show($"Loại tiền dòng [{stt}] không được để trống!", "Thông báo");
                    return false;
                }
                if (currencyRate <= 0)
                {
                    MessageBox.Show($"Tỷ giá của [{currencyCode}] phải > 0.\nVui lòng kiểm tra lại Ngày hết hạn!", "Thông báo");
                    return false;
                }
                if (UnitPrice <= 0)
                {
                    MessageBox.Show($"Đơn giá dòng [{stt}] không được để trống!", "Thông báo");
                    return false;
                }
                if (SupplierSaleID <= 0)
                {
                    MessageBox.Show($"Nhà cung cấp dòng [{stt}] không được để trống!", "Thông báo");
                    return false;
                }
            }
            return true;
        }

        private bool Save()
        {
            grvData.FocusedRowHandle = -1;
            if (!CheckValidate()) return false;
            grvData.Focus();

            for (int i = 0; i < grvData.RowCount; i++)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                ProjectPartlistPriceRequestHistoryModel model = new ProjectPartlistPriceRequestHistoryModel();

                if (id > 0)
                {
                    model = SQLHelper<ProjectPartlistPriceRequestHistoryModel>.FindByID(id);
                }

                model.ProjectPartlistPriceRequestID = modelPPLPR.ID;
                model.UnitPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colUnitPrice));
                model.TotalPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalPrice));
                model.Unit = TextUtils.ToString(grvData.GetRowCellValue(i, colUnit));
                model.SupplierSaleID = TextUtils.ToInt(grvData.GetRowCellValue(i, colSupplierSaleID));
                model.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));
                model.DatePriceQuote = TextUtils.ToDate5(grvData.GetRowCellValue(i, colDatePriceQuote));
                model.TotalPriceExchange = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalPriceExchange));
                model.CurrencyRate = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colCurrencyRate));
                model.CurrencyID = TextUtils.ToInt(grvData.GetRowCellValue(i, colCurrencyID));
                model.HistoryPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colHistoryPrice));
                model.LeadTime = TextUtils.ToString(grvData.GetRowCellValue(i, colLeadTime));
                model.UnitFactoryExportPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colUnitFactoryExportPrice));
                model.UnitImportPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colUnitImportPrice));
                model.TotalImportPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotalImportPrice));
                model.IsImport = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsImport));
                model.QuoteEmployeeID = TextUtils.ToInt(grvData.GetRowCellValue(i, colQuoteEmployeeID));

                if (model.ID > 0)
                {
                    SQLHelper<ProjectPartlistPriceRequestHistoryModel>.Update(model);
                }
                else
                {
                    model.ID = SQLHelper<ProjectPartlistPriceRequestHistoryModel>.Insert(model).ID;
                    grvData.SetRowCellValue(i, colID, model.ID);
                }
            }

            if (lstIDDelete.Count > 0)
            {
                for (int j = 0; j < lstIDDelete.Count; j++)
                {
                    int projectPartlistPriceRequestHistoryID = TextUtils.ToInt(lstIDDelete[j]);
                    ProjectPartlistPriceRequestHistoryModel model = SQLHelper<ProjectPartlistPriceRequestHistoryModel>.FindByID(projectPartlistPriceRequestHistoryID);
                    if (model != null && model.ID > 0)
                    {
                        model.IsDeleted = true;
                        SQLHelper<ProjectPartlistPriceRequestHistoryModel>.Update(model);
                    }
                }
            }
            return true;
        }

        private void btnDelete1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (grdData.DataSource == null || !checkRole) return;
            int strID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));

            if (MessageBox.Show(String.Format($"Bạn có chắc muốn xóa dòng này không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvData.DeleteSelectedRows();
                if (strID > 0)
                {
                    lstIDDelete.Add(strID);
                }
            }
        }

        decimal CalculatorTotalMoneyExchange(int rowHandle)
        {
            decimal totalMoney = TextUtils.ToDecimal(grvData.GetRowCellValue(rowHandle, colTotalPrice));
            decimal currencyRate = TextUtils.ToDecimal(grvData.GetRowCellValue(rowHandle, colCurrencyRate));
            decimal totalMoneyExchange = totalMoney * currencyRate;
            return totalMoneyExchange;
        }

        private void cboCurrency_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit lookUpEdit = (SearchLookUpEdit)sender;
            CurrencyModel currency = (CurrencyModel)lookUpEdit.GetSelectedDataRow();

            if (currency == null)
            {
                grvData.SetFocusedRowCellValue(colCurrencyRate, 0);
                grvData.SetFocusedRowCellValue(colTotalPriceExchange, 0);
                return;
            }

            grvData.SetFocusedRowCellValue(colCurrencyID, currency.ID);
            grvData.SetFocusedRowCellValue(colCurrencyRate, currency.CurrencyRate);
            if ((currency.DateExpried < DateTime.Now || currency.DateStart > DateTime.Now) && currency.Code.ToLower().Trim() != "vnd")
            {
                grvData.SetFocusedRowCellValue(colCurrencyRate, 0);
            }
            grvData.SetFocusedRowCellValue(colTotalPriceExchange, CalculatorTotalMoneyExchange(grvData.FocusedRowHandle));
        }

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            grvData.CloseEditor();
            if (e.Column != colSupplierSaleID && e.Column != colUnitPrice && e.Column != colUnitImportPrice) return;

            if (e.Column == colUnitPrice)
            {
                decimal quantity = TextUtils.ToDecimal(modelPPLPR.Quantity);
                decimal unitPrice = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colUnitPrice));

                decimal totalPrice = quantity * unitPrice;
                grvData.SetFocusedRowCellValue(colTotalPrice, totalPrice);
                grvData.SetFocusedRowCellValue(colTotalPriceExchange, CalculatorTotalMoneyExchange(grvData.FocusedRowHandle));
            }

            if (e.Column == colUnitImportPrice)
            {
                decimal quantity = TextUtils.ToDecimal(modelPPLPR.Quantity);
                decimal unitImportPrice = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colUnitImportPrice));
                decimal totalPrice = quantity * unitImportPrice;

                grvData.SetFocusedRowCellValue(colTotalImportPrice, totalPrice);

            }
        }

        private void btnIsSelectedQuote_Click(object sender, EventArgs e)
        {
            ApproveIsSelectedQuote(true);
        }

        private void btnUnIsSelectedQuote_Click(object sender, EventArgs e)
        {
            ApproveIsSelectedQuote(false);
        }

        private void ApproveIsSelectedQuote(bool isApprove)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id <= 0)
            {
                MessageBox.Show($"Vật tư này chưa được lưu!", "Thông báo");
                return;
            }
            string textMess = isApprove == true ? "chọn báo giá" : "hủy chọn báo giá";

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn {textMess} vật tư này hay không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                ProjectPartlistPriceRequestHistoryModel model = SQLHelper<ProjectPartlistPriceRequestHistoryModel>.FindByID(id);
                if(model.ID > 0)
                {
                    model.IsSelectedQuote = isApprove;
                    SQLHelper<ProjectPartlistPriceRequestHistoryModel>.Update(model);
                    grvData.SetFocusedRowCellValue(colIsSelectedQuote, model.IsSelectedQuote);
                }
            }
        }


    }
}
