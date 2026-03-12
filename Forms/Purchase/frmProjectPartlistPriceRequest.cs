using BaseBusiness.DTO;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmProjectPartlistPriceRequest : _Forms
    {

        DataTable dtPriceRequest = new DataTable();
        public frmProjectPartlistPriceRequest()
        {
            InitializeComponent();
        }

        private void frmProjectPartlistPriceRequest_Load(object sender, EventArgs e)
        {
            dtpDateStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDateEnd.Value = dtpDateStart.Value.AddMonths(+2).AddDays(-1);
            cboStatusRequest.SelectedIndex = 1;

            LoadProject();
            LoadSupplierSale();
            LoadCurrency();
            LoadData();
        }

        void LoadProject()
        {
            //List<ProjectModel> list = SQLHelper<ProjectModel>.FindAll().OrderByDescending(x => x.ID).ToList();

            DataTable dt = TextUtils.GetTable("spGetProjectPartlistRequest");
            cboProject.Properties.ValueMember = "ID";
            cboProject.Properties.DisplayMember = "ProjectCode";
            cboProject.Properties.DataSource = dt;
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

        void LoadData()
        {
            try
            {
                Lib.LockEvents = true;
                if (!SaveData($"Bạn có muốn lưu lại thay đổi không?")) return;

                DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Month, 0, 0, 0);
                DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59);
                int statusRequest = cboStatusRequest.SelectedIndex;
                int projectId = TextUtils.ToInt(cboProject.EditValue);
                string keyword = txtKeyword.Text.Trim();

                dtPriceRequest = TextUtils.LoadDataFromSP("spGetProjectPartlistPriceRequest", "A",
                                                        new string[] { "@DateStart", "@DateEnd", "@StatusRequest", "@ProjectID", "@Keyword" },
                                                        new object[] { dateStart, dateEnd, statusRequest, projectId, keyword });

                grdData.DataSource = dtPriceRequest;

                LoadTabControl();
                //LoadDataHistory();
            }
            finally
            {
                Lib.LockEvents = false;
            }
        }

        //TODO: 18/05/2024 lhPhuc add new
        void LoadDataHistory(GridView gridView)
        {
            int ID = TextUtils.ToInt(gridView.GetFocusedRowCellValue(colID));
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectPartlistPriceRequestHistory", "A", new string[] { "@ProjectPartlistPriceRequestID" }, new object[] { ID });
            grdDataHistory.DataSource = dt;
        }

        int tabSelected = 0;
        void LoadTabControl()
        {
            xtraTabControl1.TabPages.Clear();
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectTypeAssignByEmployeeID", "A", new string[] { "@EmployeeID" }, new object[] { Global.EmployeeID });
            foreach (DataRow row in dt.Rows)
            {
                int typeId = TextUtils.ToInt(row["ID"]);
                //if (typeId == 7) continue;
                string typeName = TextUtils.ToString(row["ProjectTypeName"]).ToUpper();

                DataTable dtType = dtPriceRequest.Clone();

                var dataRow = dtPriceRequest.Select($"ProjectTypeID = {typeId}");
                if (dataRow.Length <= 0) continue;
                dtType = dataRow.CopyToDataTable();
                XtraTabPage tabPage = new XtraTabPage();
                tabPage.Text = typeName;

                tabPage.Controls.Add(CloneGridControl(grdData, dtType));
                xtraTabControl1.TabPages.Add(tabPage);
            }

            xtraTabControl1.SelectedTabPageIndex = tabSelected;
        }


        GridControl CloneGridControl(GridControl template, DataTable dataSource)
        {
            GridControl gridControl = new GridControl();
            GridView gridView = new GridView(gridControl);

            gridView.OptionsBehavior.Editable = false;
            gridView.OptionsBehavior.ReadOnly = true;

            gridControl.MainView = gridView;
            gridControl.Dock = template.Dock;
            gridControl.DataSource = dataSource;

            gridView.Assign(template.MainView as GridView, false);

            gridView.KeyDown += grvData_KeyDown;
            gridView.FocusedRowChanged += grvData_FocusedRowChanged;
            gridView.RowStyle += grvData_RowStyle;

            return gridControl;
        }

        //bool SaveData(string message)
        //{
        //    int id = 0;
        //    decimal unitPrice = 0;
        //    decimal totalPrice = 0;
        //    int supplierSaleId = 0;
        //    string unit = "";
        //    decimal totalPriceExchange = 0;
        //    decimal currencyRate = 0;

        //    string sql = "";
        //    grvData.CloseEditor();
        //    //var dataChange = dtPriceRequest.GetChanges();
        //    if (dtPriceRequest.Rows.Count > 0)
        //    {
        //        var dataChange = dtPriceRequest.Select("StatusUpdate = 2");
        //        if (dataChange.Length > 0)
        //        {
        //            if (string.IsNullOrEmpty(message))
        //            {
        //                foreach (DataRow row in dataChange)
        //                {
        //                    id = TextUtils.ToInt(row["ID"]);
        //                    unitPrice = TextUtils.ToInt(row["UnitPrice"]);
        //                    totalPrice = TextUtils.ToInt(row["TotalPrice"]);
        //                    supplierSaleId = TextUtils.ToInt(row["SupplierSaleID"]);
        //                    unit = TextUtils.ToString(row["Unit"]).Trim();
        //                    totalPriceExchange = TextUtils.ToDecimal(row["TotalPriceExchange"]);
        //                    currencyRate = TextUtils.ToDecimal(row["CurrencyRate"]);

        //                    sql += $"UPDATE dbo.ProjectPartlistPriceRequest SET " +
        //                            $"UnitPrice = {unitPrice}," +
        //                            $"TotalPrice = {totalPrice}," +
        //                            $"SupplierSaleID = {supplierSaleId}," +
        //                            $"Unit = '{unit}'," +
        //                            $"UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
        //                            $"UpdatedBy = '{Global.LoginName}'," +
        //                            $"TotalPriceExchange = {totalPriceExchange}," +
        //                            $"CurrencyRate = {currencyRate}" +
        //                            $"WHERE ID = {id}\n";

        //                    var dataRow = dtPriceRequest.Select($"ID = {row["ID"]}")[0];
        //                    if (dataRow == null) continue;
        //                    int index = dtPriceRequest.Rows.IndexOf(dataRow);
        //                    dtPriceRequest.Rows[index]["StatusUpdate"] = 0;
        //                }

        //                if (!string.IsNullOrEmpty(sql))
        //                {
        //                    TextUtils.ExcuteSQL(sql);
        //                }

        //                //dtPriceRequest.AcceptChanges();
        //            }
        //            else
        //            {
        //                DialogResult dialog = MessageBox.Show($"Những thay đổi chưa được lưu.\nBạn có muốn lưu lại trước khi {message} không?", "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        //                if (dialog == DialogResult.Yes)
        //                {
        //                    foreach (DataRow row in dataChange)
        //                    {
        //                        id = TextUtils.ToInt(row["ID"]);
        //                        unitPrice = TextUtils.ToInt(row["UnitPrice"]);
        //                        totalPrice = TextUtils.ToInt(row["TotalPrice"]);
        //                        supplierSaleId = TextUtils.ToInt(row["SupplierSaleID"]);
        //                        unit = TextUtils.ToString(row["Unit"]).Trim();
        //                        totalPriceExchange = TextUtils.ToDecimal(row["TotalPriceExchange"]);
        //                        currencyRate = TextUtils.ToDecimal(row["CurrencyRate"]);

        //                        sql += $"UPDATE dbo.ProjectPartlistPriceRequest SET " +
        //                                $"UnitPrice = {unitPrice}," +
        //                                $"TotalPrice = {totalPrice}," +
        //                                $"SupplierSaleID = {supplierSaleId}," +
        //                                $"Unit = '{unit}'," +
        //                                $"UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
        //                                $"UpdatedBy = '{Global.LoginName}'," +
        //                                $"TotalPriceExchange = {totalPriceExchange}," +
        //                                $"CurrencyRate = {currencyRate} " +
        //                                $"WHERE ID = {id}\n";

        //                        row["StatusUpdate"] = 0;

        //                        var dataRow = dtPriceRequest.Select($"ID = {row["ID"]}")[0];
        //                        if (dataRow == null) continue;
        //                        int index = dtPriceRequest.Rows.IndexOf(dataRow);
        //                        dtPriceRequest.Rows[index]["StatusUpdate"] = 0;
        //                    }

        //                    if (!string.IsNullOrEmpty(sql))
        //                    {
        //                        TextUtils.ExcuteSQL(sql);
        //                    }
        //                }
        //                else if (dialog == DialogResult.Cancel)
        //                {
        //                    return false;
        //                }
        //                else
        //                {
        //                    foreach (DataRow row in dataChange)
        //                    {
        //                        var dataRow = dtPriceRequest.Select($"ID = {row["ID"]}")[0];
        //                        if (dataRow == null) continue;

        //                        int index = dtPriceRequest.Rows.IndexOf(dataRow);
        //                        dtPriceRequest.Rows[index]["UnitPrice"] = dtPriceRequest.Rows[index]["UnitPrice", DataRowVersion.Original];
        //                        dtPriceRequest.Rows[index]["TotalPrice"] = dtPriceRequest.Rows[index]["TotalPrice", DataRowVersion.Original];
        //                        dtPriceRequest.Rows[index]["SupplierSaleID"] = dtPriceRequest.Rows[index]["SupplierSaleID", DataRowVersion.Original];
        //                        dtPriceRequest.Rows[index]["Unit"] = dtPriceRequest.Rows[index]["Unit", DataRowVersion.Original];
        //                        dtPriceRequest.Rows[index]["TotalPriceExchange"] = dtPriceRequest.Rows[index]["TotalPriceExchange", DataRowVersion.Original];
        //                        dtPriceRequest.Rows[index]["CurrencyRate"] = dtPriceRequest.Rows[index]["CurrencyRate", DataRowVersion.Original];
        //                        dtPriceRequest.Rows[index]["StatusUpdate"] = dtPriceRequest.Rows[index]["StatusUpdate", DataRowVersion.Original];
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    //dtPriceRequest.AcceptChanges();
        //    return true;
        //}

        //void QuotePrice(bool isQuotePrice)
        //{
        //    int quotePriceValue = isQuotePrice ? 2 : 1;
        //    string isQuotePriceText = isQuotePrice ? "báo giá" : "huỷ báo giá";

        //    var selectedRows = grvData.GetSelectedRows();
        //    if (selectedRows.Length <= 0)
        //    {
        //        MessageBox.Show($"Vui lòng chọn sản phẩm muốn {isQuotePriceText}!", "Thông báo");
        //        return;
        //    }
        //    int id = 0;
        //    int partListId = 0;
        //    decimal quantity = 0;

        //    decimal unitPrice = 0;
        //    decimal totalPrice = 0;
        //    int supplierSaleId = 0;
        //    string unit = "";
        //    string nameNCC = "";
        //    if (!SaveData(isQuotePriceText)) return;

        //    //Check validate 
        //    foreach (int row in selectedRows)
        //    {
        //        id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
        //        if (id <= 0) continue;
        //        string productCode = TextUtils.ToString(grvData.GetRowCellValue(row, colProductCode));

        //        unit = TextUtils.ToString(grvData.GetRowCellValue(row, colUnit));
        //        unitPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(row, colUnitPrice));
        //        supplierSaleId = TextUtils.ToInt(grvData.GetRowCellValue(row, colSupplierSaleID));

        //        if (string.IsNullOrEmpty(unit))
        //        {
        //            MessageBox.Show($"Vui lòng nhập ĐVT mã sản phẩm [{productCode}]!", "Thông báo");
        //            return;
        //        }

        //        if (unitPrice <= 0)
        //        {
        //            MessageBox.Show($"Vui lòng nhập Đơn giá mã sản phẩm [{productCode}]!", "Thông báo");
        //            return;
        //        }

        //        if (supplierSaleId <= 0)
        //        {
        //            MessageBox.Show($"Vui lòng nhập Nhà cung cấp mã sản phẩm [{productCode}]!", "Thông báo");
        //            return;
        //        }
        //    }

        //    DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn {isQuotePriceText} danh sách sản phẩm đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //    if (dialog == DialogResult.No) return;

        //    string sql = "";
        //    foreach (int row in selectedRows)
        //    {
        //        id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
        //        unitPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(row, colUnitPrice));
        //        supplierSaleId = TextUtils.ToInt(grvData.GetRowCellValue(row, colSupplierSaleID));
        //        unit = TextUtils.ToString(grvData.GetRowCellValue(row, colUnit)).Trim();
        //        if (id <= 0) continue;
        //        //if (unitPrice <= 0 && isQuotePrice && !node.HasChildren) continue;

        //        partListId = TextUtils.ToInt(grvData.GetRowCellValue(row, colProjectPartListID));
        //        quantity = TextUtils.ToDecimal(grvData.GetRowCellValue(row, colQuantity));
        //        totalPrice = unitPrice * quantity;
        //        nameNCC = TextUtils.ToString(grvData.GetRowCellDisplayText(row, colSupplierSaleID));

        //        string sqlPriceRequest = $"UPDATE dbo.ProjectPartlistPriceRequest SET StatusRequest = {quotePriceValue}," +
        //                                    $"UnitPrice = {unitPrice}," +
        //                                    $"TotalPrice = {totalPrice}," +
        //                                    $"Unit = N'{unit}'," +
        //                                    $"SupplierSaleID = {supplierSaleId}," +
        //                                    $"UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
        //                                    $"UpdatedBy = N'{Global.LoginName}'," +
        //                                    $"DatePriceQuote = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' WHERE ProjectPartListID = {partListId}\n";

        //        string sqlPartlist = $"UPDATE dbo.ProjectPartList SET StatusPriceRequest = {quotePriceValue}," +
        //                                $"Price = {unitPrice}," +
        //                                $"Amount = {totalPrice}," +
        //                                $"SupplierSaleID = {supplierSaleId}," +
        //                                $"UnitMoney = N'{unit}'," +
        //                                $"UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
        //                                $"UpdatedBy = N'{Global.LoginName}'," +
        //                                $"DatePriceQuote = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
        //                                $"NCC = N'{nameNCC}'" +
        //                                $"WHERE ID = {partListId}\n\n";

        //        sql += (sqlPriceRequest + sqlPartlist);
        //    }

        //    if (!string.IsNullOrEmpty(sql))
        //    {
        //        TextUtils.ExcuteSQL(sql);
        //    }

        //    LoadData();
        //}
        bool SaveData(string message)
        {
            grvData.FocusedRowHandle = -1;

            var columnEdits = grvData.Columns.Where(x => x.OptionsColumn.AllowEdit == true && x.Visible == true).ToList();
            var dataChange = dtPriceRequest.GetChanges();
            if (dataChange != null)
            {
                if (string.IsNullOrEmpty(message.Trim()))
                {
                    string sql = "";
                    foreach (DataRow row in dataChange.Rows)
                    {
                        int id = TextUtils.ToInt(row["ID"]);
                        //decimal currencyRate = TextUtils.ToInt(row["ID"]);

                        //Tính thành tiền

                        decimal quantity = TextUtils.ToDecimal(row["Quantity"]);
                        decimal unitPrice = TextUtils.ToDecimal(row["UnitPrice"]);
                        decimal totalPrice = quantity * unitPrice;

                        //Tính thành tiền quy đổi
                        decimal currencyRate = TextUtils.ToDecimal(row["CurrencyRate"]);
                        decimal totalPriceExchange = totalPrice * currencyRate;

                        //Tính thành tiền có VAT
                        decimal vat = TextUtils.ToDecimal(row["VAT"]);
                        decimal totalMoneyVAT = totalPrice + ((totalPrice * vat) / 100);

                        string columnUpdate = "";
                        foreach (GridColumn col in columnEdits)
                        {
                            //var format = col.DisplayFormat.Format;
                            var columnTypeName = col.ColumnType.Name.ToLower();
                            string value = TextUtils.ToString(row[col.FieldName]).Trim().ToLower();

                            if (columnTypeName == "int32" || columnTypeName == "decimal") value = string.IsNullOrEmpty(value) ? "0" : value;
                            else if (columnTypeName == "boolean") value = value == "true" ? "1" : "0";
                            else value = $"N'{value}'";

                            columnUpdate += $"{col.FieldName} = {value},";
                        }

                        sql += $"UPDATE dbo.ProjectPartlistPriceRequest SET {columnUpdate}" +
                                $"CurrencyRate = {currencyRate}," +
                                $"TotalPrice = {totalPrice}," +
                                $"TotalPriceExchange = {totalPriceExchange}," +
                                $"TotaMoneyVAT = {totalMoneyVAT}," +
                                $"UpdatedDate = N'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                                $"UpdatedBy = N'{Global.LoginName}'," +
                                $"QuoteEmployeeID = {Global.EmployeeID}" +
                                $"WHERE ID = {id}\n";
                    }

                    if (!string.IsNullOrEmpty(sql.Trim())) TextUtils.ExcuteSQL(sql);
                    dtPriceRequest.AcceptChanges();
                }
                else
                {
                    DialogResult dialog = MessageBox.Show("Bạn có muốn lưu lại thay đổi không?", "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (dialog == DialogResult.Yes)
                    {
                        string sql = "";
                        foreach (DataRow row in dataChange.Rows)
                        {
                            int id = TextUtils.ToInt(row["ID"]);
                            //decimal currencyRate = TextUtils.ToInt(row["ID"]);

                            //Tính thành tiền

                            decimal quantity = TextUtils.ToDecimal(row["Quantity"]);
                            decimal unitPrice = TextUtils.ToDecimal(row["UnitPrice"]);
                            decimal totalPrice = quantity * unitPrice;

                            //Tính thành tiền quy đổi
                            decimal currencyRate = TextUtils.ToDecimal(row["CurrencyRate"]);
                            decimal totalPriceExchange = totalPrice * currencyRate;

                            //Tính thành tiền có VAT
                            decimal vat = TextUtils.ToDecimal(row["VAT"]);
                            decimal totalMoneyVAT = totalPrice + ((totalPrice * vat) / 100);

                            string columnUpdate = "";
                            foreach (GridColumn col in columnEdits)
                            {
                                //var format = col.DisplayFormat.Format;
                                var columnTypeName = col.ColumnType.Name.ToLower();
                                string value = TextUtils.ToString(row[col.FieldName]).Trim().ToLower();

                                if (columnTypeName == "int32" || columnTypeName == "decimal") value = string.IsNullOrEmpty(value) ? "0" : value;
                                else if (columnTypeName == "boolean") value = value == "true" ? "1" : "0";
                                else value = $"N'{value}'";

                                columnUpdate += $"{col.FieldName} = {value},";
                            }

                            sql += $"UPDATE dbo.ProjectPartlistPriceRequest SET {columnUpdate}" +
                                    $"CurrencyRate = {currencyRate}," +
                                    $"TotalPrice = {totalPrice}," +
                                    $"TotalPriceExchange = {totalPriceExchange}," +
                                    $"TotaMoneyVAT = {totalMoneyVAT}," +
                                    $"UpdatedDate = N'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                                    $"UpdatedBy = N'{Global.LoginName}'," +
                                    $"QuoteEmployeeID = {Global.EmployeeID}" +
                                    $"WHERE ID = {id}\n";
                        }

                        if (!string.IsNullOrEmpty(sql.Trim())) TextUtils.ExcuteSQL(sql);
                        dtPriceRequest.AcceptChanges();
                    }
                    else if (dialog == DialogResult.No)
                    {
                        foreach (DataRow row in dataChange.Rows)
                        {
                            var dataRow = dtPriceRequest.Select($"ID = {row["ID"]}")[0];
                            if (dataRow == null) continue;

                            int index = dtPriceRequest.Rows.IndexOf(dataRow);
                            foreach (GridColumn column in grvData.Columns)
                            {
                                if (!dtPriceRequest.Columns.Contains(column.FieldName)) continue;
                                dtPriceRequest.Rows[index][column.FieldName] = dtPriceRequest.Rows[index][column.FieldName, DataRowVersion.Original];
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            return true;
        }


        void QuotePrice(int statusRequest)
        {
            string statusRequestText = statusRequest == 1 ? "huỷ báo giá" : "báo giá";
            SaveData($"Bạn có muốn lưu lại thay đổi trước khi {statusRequestText} không?");

            //Check validate
            var selectedRows = grvData.GetSelectedRows();
            if (selectedRows.Length <= 0)
            {
                MessageBox.Show($"Vui lòng chọn sản phẩm muốn {statusRequestText}!", "Thông báo");
                return;
            }

            foreach (int row in selectedRows)
            {
                if (statusRequest == 1) continue;

                int id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
                if (id <= 0) continue;
                string productCode = TextUtils.ToString(grvData.GetRowCellValue(row, colProductCode));

                int currencyId = TextUtils.ToInt(grvData.GetRowCellValue(row, colCurrencyID));
                string currencyCode = TextUtils.ToString(grvData.GetRowCellDisplayText(row, colCurrencyID)).Trim();
                decimal currencyRate = TextUtils.ToInt(grvData.GetRowCellValue(row, colCurrencyRate));
                decimal unitPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(row, colUnitPrice));
                decimal supplierSaleId = TextUtils.ToInt(grvData.GetRowCellValue(row, colSupplierSaleID));

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

                if (supplierSaleId <= 0)
                {
                    MessageBox.Show($"Vui lòng nhập Nhà cung cấp mã sản phẩm [{productCode}]!", "Thông báo");
                    return;
                }
            }

            List<int> listIDs = new List<int>();
            foreach (int row in selectedRows)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
                if (id <= 0) continue;
                listIDs.Add(id);
            }

            if (listIDs.Count <= 0) return;

            int isCheckPriceValue = statusRequest == 1 ? 1 : 0;
            string idText = string.Join(",", listIDs);
            string sql = $"UPDATE ProjectPartlistPriceRequest SET StatusRequest = {statusRequest}," +
                            $"DatePriceQuote = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                            $"UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                            $"UpdatedBy = '{Global.LoginName}'," +
                            $"QuoteEmployeeID = {Global.EmployeeID}," +
                            $"IsCheckPrice = {isCheckPriceValue}" +
                            $"WHERE ID IN({idText})";

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn {statusRequestText} danh sách sản phẩm đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                TextUtils.ExcuteSQL(sql);
                LoadData();
            }

            //var columnEdits = grvData.Columns.Where(x => x.OptionsColumn.AllowEdit == true && x.Visible == true).ToList();
            //var dataChange = dtPriceRequest.GetChanges();
            //if (dataChange != null)
            //{
            //    string sql = "";
            //    foreach (DataRow row in dataChange.Rows)
            //    {
            //        int id = TextUtils.ToInt(row["ID"]);

            //        //Tính thành tiền

            //        decimal quantity = TextUtils.ToDecimal(row["Quantity"]);
            //        decimal unitPrice = TextUtils.ToDecimal(row["UnitPrice"]);
            //        decimal totalPrice = quantity * unitPrice;

            //        //Tính thành tiền quy đổi
            //        decimal currencyRate = TextUtils.ToDecimal(row["CurrencyRate"]);
            //        decimal totalPriceExchange = totalPrice * currencyRate;

            //        string columnUpdate = "";
            //        foreach (GridColumn col in columnEdits)
            //        {
            //            //var format = col.DisplayFormat.Format;
            //            var columnTypeName = col.ColumnType.Name.ToLower();
            //            string value = TextUtils.ToString(row[col.FieldName]).Trim().ToLower();

            //            if (columnTypeName == "int32" || columnTypeName == "decimal") value = string.IsNullOrEmpty(value) ? "0" : value;
            //            else if (columnTypeName == "boolean") value = value == "true" ? "1" : "0";
            //            else value = $"N'{value}'";

            //            columnUpdate += $"{col.FieldName} = {value},";
            //        }

            //        sql += $"UPDATE dbo.ProjectPartlistPriceRequest SET {columnUpdate}" +
            //                $"TotalPrice = {totalPrice}," +
            //                $"TotalPriceExchange = {totalPriceExchange}," +
            //                $"UpdatedDate = N'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
            //                $"UpdatedBy = N'{Global.LoginName}'" +
            //                $"WHERE ID = {id}\n";
            //    }

            //    if (!string.IsNullOrEmpty(sql.Trim())) TextUtils.ExcuteSQL(sql);
            //    dtPriceRequest.AcceptChanges();
            //}
        }


        void CheckPrice(bool isCheckPrice)
        {
            string isCheckText = isCheckPrice ? "Check giá" : "Huỷ check giá";
            var selectedRows = grvData.GetSelectedRows();
            if (selectedRows.Length <= 0)
            {
                MessageBox.Show($"Vui lòng chọn sản phẩm muốn {isCheckText}!", "Thông báo");
                return;
            }

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn {isCheckText} danh sách sản phẩm đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                List<int> listIDs = new List<int>();
                int isCheckPriceValue = isCheckPrice ? 1 : 0;
                foreach (int row in selectedRows)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
                    if (id <= 0) continue;
                    listIDs.Add(id);



                }

                if (listIDs.Count <= 0) return;
                string idText = string.Join(",", listIDs);

                var myDict = new Dictionary<string, object>()
                    {
                        { "IsCheckPrice",isCheckPriceValue}
                    };

                var exp = new Expression("ID", idText, "IN");
                SQLHelper<ProjectPartlistPriceRequestModel>.UpdateFields(myDict, exp);


                //string updatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                //string sql = $"UPDATE ProjectPartlistPriceRequest " +
                //            $"SET IsCheckPrice = {isCheckPriceValue}, " +
                //            $"UpdatedBy = N'{Global.LoginName}', " +
                //            $"UpdatedDate = '{updatedDate}' " +
                //            $"WHERE ID IN ({idText})";
                //TextUtils.ExcuteSQL(sql);

                LoadData();
            }

        }


        void CalculatorValueParentNode(TreeListNode node)
        {
            //var parentNode = node.ParentNode;
            //if (parentNode == null) return;
            //var listChildNodes = tlData.FindNodeByKeyID(parentNode.GetValue(colProjectPartListID));

            //if (listChildNodes == null) return;
            //decimal totalPrice = listChildNodes.Nodes.Sum(x => (decimal)x["TotalPrice"]);
            //parentNode.SetValue(colTotalPrice, totalPrice);
            //CalculatorValueParentNode(parentNode);
        }

        void Calculate(DataTable dataTable, int rowIndex = 0)
        {
            //decimal quantity = TextUtils.ToDecimal(dataTable.Rows[rowIndex]["Quantity"]);
            decimal price = TextUtils.ToDecimal(dataTable.Rows[rowIndex]["TotalPrice"]);

            decimal totalPriceFromChildren = 0;
            //decimal totalAmountFromChildren = 0;

            foreach (DataRow row in dataTable.Rows)
            {
                int childRowIndex = dataTable.Rows.IndexOf(row);

                if (row["ParentID"] != DBNull.Value &&
                    TextUtils.ToInt(row["ParentID"]) == TextUtils.ToInt(dataTable.Rows[rowIndex]["ProjectPartListID"]))
                {
                    Calculate(dataTable, childRowIndex);


                    //decimal laborFromChild = TextUtils.ToDouble(row["Price"]);
                    decimal costFromChild = TextUtils.ToDecimal(row["TotalPrice"]);

                    //totalPriceFromChildren += laborFromChild;
                    totalPriceFromChildren += costFromChild;
                }
            }


            //decimal totalLabor = price + totalPriceFromChildren;
            decimal totalCost = price + totalPriceFromChildren;

            dataTable.Rows[rowIndex]["TotalPrice"] = totalCost;
            //dataTable.Rows[rowIndex]["Amount"] = totalCost;

            dataTable.AcceptChanges();
        }

        void UpdateProductImport(bool isImport)
        {
            string isImportText = isImport ? "hàng nhập khẩu" : "hàng nội địa";
            string sqlInsertIsImport = "";
            var selectedRows = grvData.GetSelectedRows();
            if (selectedRows.Length <= 0)
            {
                MessageBox.Show($"Vui lòng chọn sản phẩm muốn chuyển thành {isImportText}!", "Thông báo");
                return;
            }

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn chuyển sản phẩm đã chọn thành {isImportText} không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                foreach (int row in selectedRows)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
                    if (id <= 0) continue;
                    grvData.SetRowCellValue(row, colIsImport, 0);
                    int isImportValue = isImport ? 1 : 0;
                    sqlInsertIsImport += $"UPDATE dbo.ProjectPartlistPriceRequest SET IsImport = {isImportValue}, " +
                                            $"UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                                            $"UpdatedBy = '{Global.LoginName}'" +
                                            $"WHERE ID = {id}\n";
                }

                if (!string.IsNullOrEmpty(sqlInsertIsImport.Trim())) TextUtils.ExcuteSQL(sqlInsertIsImport);
                LoadData();
            }
            //int id = 0;


        }

        private void cboStatusRequest_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboProject_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnAddUnitPrice_Click(object sender, EventArgs e)
        {
            //SaveData("");

            SaveData("");
        }

        private void btnQuotePrice_Click(object sender, EventArgs e)
        {
            //QuotePrice(true);
            QuotePrice(2);
        }

        private void btnCancelQuotePrice_Click(object sender, EventArgs e)
        {
            //QuotePrice(false);
            QuotePrice(1);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = Path.Combine(f.SelectedPath, $"YeuCauBaoGia_{DateTime.Now.ToString("ddMMyy")}.xlsx");

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdData;

                try
                {
                    CompositeLink compositeLink = new CompositeLink(printingSystem);
                    compositeLink.Links.Add(printableComponentLink1);

                    compositeLink.CreatePageForEachLink();
                    optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;

                    compositeLink.PrintingSystem.SaveDocument(filepath);
                    compositeLink.ExportToXlsx(filepath, optionsEx);
                    Process.Start(filepath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void tlData_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            //if (e.Node == null) return;
            //int status = TextUtils.ToInt(tlData.GetRowCellValue(e.Node, colStatusRequest));
            //DateTime deadline = TextUtils.ToDate5(tlData.GetRowCellValue(e.Node, colDeadline)).Date;
            //DateTime dateNow = DateTime.Now.Date;
            //if (status == 2)
            //{
            //    e.Appearance.BackColor = Color.Lime;
            //    //e.Appearance.ForeColor = Color.White;
            //}
            //else if ((deadline - dateNow).TotalDays < 0) //Quá hạn
            //{
            //    e.Appearance.BackColor = Color.Red;
            //    e.Appearance.ForeColor = Color.White;
            //}
            //else if ((deadline - dateNow).TotalDays <= 1) //Sắp hết hạn
            //{
            //    e.Appearance.BackColor = Color.Orange;
            //}

            if (e.Node.HasChildren)
            {
                e.Appearance.BackColor = Color.LightGray;
            }
        }


        private void tlData_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            //if (_lockEvent)
            //    return;

            //tlData.CloseEditor();

            //decimal quantity = TextUtils.ToDecimal(tlData.GetFocusedRowCellValue(colQuantity));
            //decimal unitPrice = TextUtils.ToDecimal(tlData.GetFocusedRowCellValue(colUnitPrice));
            //decimal totalPrice = quantity * unitPrice;

            //tlData.SetFocusedRowCellValue(colTotalPrice, totalPrice);
            ////CalculateValue(tlData.FocusedNode.ParentNode);
            //CalculatorValueParentNode(tlData.FocusedNode);
        }


        private void txtUnitPrice_Enter(object sender, EventArgs e)
        {
            //txtUnitPrice.ReadOnly = false;
            //if (tlData.FocusedNode != null && tlData.FocusedColumn == colUnitPrice)
            //{
            //    int status = TextUtils.ToInt(tlData.FocusedNode.GetValue(colStatusRequest));
            //    if (status == 2 || tlData.FocusedNode.HasChildren)
            //    {
            //        txtUnitPrice.ReadOnly = true;
            //    }
            //}
        }

        private void frmProjectPartlistPriceRequest_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                btnAddUnitPrice_Click(null, null);
            }
        }

        private void frmProjectPartlistPriceRequest_FormClosing(object sender, FormClosingEventArgs e)
        {
            //SaveData("đóng");

            e.Cancel = !SaveData($"Bạn có muốn lưu lại thay đổi không?");
        }

        bool isRecallCellValueChanged = false;
        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (isRecallCellValueChanged == true) return;
            try
            {
                isRecallCellValueChanged = true;
                if (e.Value != null && grvData.SelectedRowsCount > 0)
                {
                    foreach (int row in grvData.GetSelectedRows())
                    {
                        grvData.SetRowCellValue(row, grvData.Columns[e.Column.FieldName], e.Value);

                    }
                }

                if (e.Column == colCurrencyID) return;

                grvData.CloseEditor();
                if (e.Column != colSupplierSaleID
                    && e.Column != colUnitPrice
                    && e.Column != colUnitImportPrice
                    && e.Column != colCurrencyID
                    && e.Column != colVAT) return;

                decimal quantity = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colQuantity));
                decimal unitPrice = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colUnitPrice));

                decimal totalPrice = quantity * unitPrice;
                grvData.SetFocusedRowCellValue(colTotalPrice, totalPrice);
                grvData.SetFocusedRowCellValue(colTotalPriceExchange, CalculatorTotalMoneyExchange(grvData.FocusedRowHandle));

                if (e.Column == colUnitImportPrice)
                {
                    decimal quantityImport = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colQuantity));
                    decimal unitImportPrice = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colUnitImportPrice));
                    decimal totalPriceImport = quantityImport * unitImportPrice;

                    grvData.SetFocusedRowCellValue(colTotalImportPrice, totalPriceImport);
                }

                //Tính thành tiền có VAT
                decimal vat = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colVAT));
                decimal totalMoneyVAT = totalPrice + ((totalPrice * vat) / 100);
                grvData.SetFocusedRowCellValue(colTotaMoneyVAT, totalMoneyVAT);
                grvData.SetFocusedRowCellValue(colStatusUpdate, 2);

            }
            finally
            {
                isRecallCellValueChanged = false;
            }
        }

        private void cboUnit_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit lookUpEdit = (SearchLookUpEdit)sender;
            CurrencyModel currency = (CurrencyModel)lookUpEdit.GetSelectedDataRow();

            grvData.SetFocusedRowCellValue(colUnit, currency.Code);
            grvData.SetFocusedRowCellValue(colCurrencyRate, currency.CurrencyRate);
            grvData.SetFocusedRowCellValue(colTotalPriceExchange, CalculatorTotalMoneyExchange(grvData.FocusedRowHandle));
            grvData.SetFocusedRowCellValue(colStatusUpdate, 2);
        }


        private void txtUnitPrice_EditValueChanged(object sender, EventArgs e)
        {
            //TextEdit textEdit = (TextEdit)sender;
            //decimal unitPrice = TextUtils.ToDecimal(textEdit.EditValue);
            //decimal quantity = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colQuantity));
            //decimal totalPrice = unitPrice * quantity;

            //grvData.SetFocusedRowCellValue(colUnitPrice, unitPrice);
            //grvData.SetFocusedRowCellValue(colTotalPrice, totalPrice);
            //grvData.SetFocusedRowCellValue(colTotalPriceExchange, CalculatorTotalMoneyExchange(grvData.FocusedRowHandle));
            //grvData.SetFocusedRowCellValue(colStatusUpdate, 2);
        }

        decimal CalculatorTotalMoneyExchange(int rowHandle)
        {
            decimal totalMoney = TextUtils.ToDecimal(grvData.GetRowCellValue(rowHandle, colTotalPrice));
            decimal currencyRate = TextUtils.ToDecimal(grvData.GetRowCellValue(rowHandle, colCurrencyRate));
            decimal totalMoneyExchange = totalMoney * currencyRate;
            return totalMoneyExchange;
        }

        private void btnHistoryPriceContext_Click(object sender, EventArgs e)
        {
            string productCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductCode)).Trim();
            frmHistoryPrice frm = new frmHistoryPrice();
            frm.txtKeyword.Text = productCode;
            frm.Show();
        }

        private void cboCurrency_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit lookUpEdit = (SearchLookUpEdit)sender;
            CurrencyModel currency = (CurrencyModel)lookUpEdit.GetSelectedDataRow();

            //validate ngày bắt đầu < ngày hết hạn

            //else
            //{
            //    grvData.SetFocusedRowCellValue(colCurrencyRate, currency.CurrencyRate);
            //}

            grvData.SetFocusedRowCellValue(colCurrencyID, currency.ID);
            grvData.SetFocusedRowCellValue(colCurrencyRate, currency.CurrencyRate);
            if ((currency.DateExpried < DateTime.Now || currency.DateStart > DateTime.Now) && currency.Code.ToLower().Trim() != "vnd")
            {
                grvData.SetFocusedRowCellValue(colCurrencyRate, 0);
            }
            grvData.SetFocusedRowCellValue(colTotalPriceExchange, CalculatorTotalMoneyExchange(grvData.FocusedRowHandle));
            grvData.SetFocusedRowCellValue(colStatusUpdate, 2);
        }

        private void btnUpdateImport_Click(object sender, EventArgs e)
        {
            UpdateProductImport(true);
        }

        private void btnCancelImport_Click(object sender, EventArgs e)
        {
            UpdateProductImport(false);
        }

        private void grvData_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            //if (!grvData.IsRowSelected(e.RowHandle)) return;

            //var Value = e.Value;
            //int[] rowSelect = grvData.GetSelectedRows();
            //if (rowSelect.Length == 0)
            //{
            //    grvData.SetRowCellValue(e.RowHandle, e.Column, Value);
            //}
            //else
            //{
            //    for (int i = 0; i < rowSelect.Length; i++)
            //    {
            //        grvData.SetRowCellValue(rowSelect[i], e.Column, Value);
            //        if (e.Column == colCurrencyID)
            //        {
            //            CurrencyModel currency = SQLHelper<CurrencyModel>.FindByID(TextUtils.ToInt(Value));
            //            if (currency == null) continue;
            //            grvData.SetRowCellValue(rowSelect[i], colCurrencyID, currency.ID);
            //            grvData.SetRowCellValue(rowSelect[i], colCurrencyRate, currency.CurrencyRate);
            //            if ((currency.DateExpried < DateTime.Now || currency.DateStart > DateTime.Now) && currency.Code.ToLower().Trim() != "vnd")
            //            {
            //                grvData.SetRowCellValue(rowSelect[i], colCurrencyRate, 0);
            //            }
            //            grvData.SetRowCellValue(rowSelect[i], colTotalPriceExchange, CalculatorTotalMoneyExchange(grvData.FocusedRowHandle));
            //        }
            //    }

            //}

            //grvData.SetFocusedRowCellValue(colStatusUpdate, 2);

        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                GridView gridView = (GridView)sender;
                string value = TextUtils.ToString(gridView.GetFocusedRowCellValue(gridView.FocusedColumn));
                if (string.IsNullOrEmpty(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void btnAddSupplierSale_Click(object sender, EventArgs e)
        {
            frmSupplierSaleDetail frm = new frmSupplierSaleDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadSupplierSale();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int[] rowSelected = grvData.GetSelectedRows();
            if (rowSelected.Length <= 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm muốn xoá!", "Thông báo");
                return;
            }

            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn xoá danh sách đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                List<int> idDeletes = new List<int>();
                foreach (int row in rowSelected)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(row, "ID"));
                    if (id <= 0) continue;
                    idDeletes.Add(id);
                }

                if (idDeletes.Count <= 0) return;
                string idDelete = string.Join(",", idDeletes);
                string sql = $"UPDATE ProjectPartlistPriceRequest SET IsDeleted = 1 WHERE ID IN ({idDelete})";
                TextUtils.ExcuteSQL(sql);

                LoadData();
            }
        }

        private void toolStripSeparator3_Click(object sender, EventArgs e)
        {

        }

        private void btnDownloadFile_Click(object sender, EventArgs e)
        {
            int projectId = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProjectID));

            ProjectModel project = SQLHelper<ProjectModel>.FindByID(projectId);
            if (project == null) return;
            if (!project.CreatedDate.HasValue) return;

            int projectPartlistId = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProjectPartListID));

            ProjectSolutionModel solution = SQLHelper<ProjectSolutionModel>.ProcedureToList("spGetProjectSolutionByProjectPartListID",
                                                new string[] { "@ProjectPartListID" }, new object[] { projectPartlistId }).FirstOrDefault();
            if (solution == null) return;
            if (string.IsNullOrEmpty(solution.CodeSolution)) return;
            string pathPattern = $@"{project.CreatedDate.Value.Year}/{project.ProjectCode.Trim()}/THIETKE.Co/{solution.CodeSolution.Trim()}/2D/GC/DH";

            int[] rowSelecteds = grvData.GetSelectedRows();
            if (rowSelecteds.Length <= 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm muốn tải file!", "Thông báo");
                return;
            }
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string productCode = "";
                try
                {
                    foreach (int row in rowSelecteds)
                    {
                        productCode = TextUtils.ToString(grvData.GetRowCellValue(row, colProductCode));
                        if (string.IsNullOrEmpty(productCode)) continue;
                        string pathDowload = Path.Combine(fbd.SelectedPath, $"{productCode}.pdf");
                        string url = $"http://113.190.234.64:8083/api/project/{pathPattern}/{productCode}.pdf";

                        WebClient webClient = new WebClient();
                        webClient.DownloadFile(url, pathDowload);
                        Process.Start(pathDowload);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"File [{productCode}.pdf] không tồn tại!\n{ex.Message}", "Thông báo");
                }
            }
        }

        private void btnDownloadFileContext_Click(object sender, EventArgs e)
        {
            btnDownloadFile_Click(null, null);
        }

        private void btnCheckPrice_Click(object sender, EventArgs e)
        {
            CheckPrice(true);
        }

        private void btnCancelCheckPrice_Click(object sender, EventArgs e)
        {
            CheckPrice(false);
        }

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            GridView gridView = (GridView)sender;
            bool isDeleted = TextUtils.ToBoolean(gridView.GetRowCellValue(e.RowHandle, colIsDeleted));
            if (isDeleted)
            {
                e.Appearance.BackColor = Color.Red;
                e.Appearance.ForeColor = Color.White;
            }
        }



        private void xtraTabControl1_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            tabSelected = xtraTabControl1.SelectedTabPageIndex;
        }

        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GridView gridView = (GridView)sender;
            LoadDataHistory(gridView);
        }

        private void btnQuotePriceHistory_Click(object sender, EventArgs e)
        {
            var tabSelected = xtraTabControl1.SelectedTabPage;
            var controls = tabSelected.Controls;

            if (controls.Count <= 0) return;
            GridControl gridControl = (GridControl)controls[0];
            GridView gridView = (GridView)gridControl.MainView;

            int rowHandle = gridView.FocusedRowHandle;
            int id = TextUtils.ToInt(gridView.GetFocusedRowCellValue(colID));
            string tt = TextUtils.ToString(gridView.GetFocusedRowCellValue(colTT));
            string unitCount = TextUtils.ToString(gridView.GetFocusedRowCellValue(colUnitCount));
            if (id > 0)
            {
                ProjectPartlistPriceRequestModel model = SQLHelper<ProjectPartlistPriceRequestModel>.FindByID(id);
                if (model == null) return;

                frmQuotePriceHistory frm = new frmQuotePriceHistory();
                frm.modelPPLPR = model;
                frm.tt = tt;
                frm.unitCount = unitCount;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                    gridView.FocusedRowHandle = rowHandle;
                }
            }
        }
    }
}
