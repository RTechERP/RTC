using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
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
    public partial class frmProjectPartlistPriceRequestOld : _Forms
    {
        DataTable dtPriceRequest = new DataTable();
        List<GridColumn> columnFixeds = new List<GridColumn>();
        public frmProjectPartlistPriceRequestOld()
        {
            InitializeComponent();
        }

        private void frmProjectPartlistPriceRequestOld_Load(object sender, EventArgs e)
        {
            Lib.LockEvents = true;
            dtpDateStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDateEnd.Value = dtpDateStart.Value.AddMonths(+1).AddDays(-1);
            cboStatusRequest.SelectedIndex = 1;
            cboIsDeleted.SelectedIndex = 1;

            LoadProject();
            LoadSupplierSale();
            LoadCurrency();
            LoadData();

            columnFixeds = grvData.Columns.Where(x => x.Fixed == FixedStyle.Left).ToList();

            Lib.LockEvents = false;
        }

        void LoadProject()
        {
            List<ProjectModel> list = SQLHelper<ProjectModel>.FindAll().OrderByDescending(x => x.ID).ToList();

            //DataTable dt = TextUtils.GetTable("spGetProjectPartlistRequest");
            cboProject.Properties.ValueMember = "ID";
            cboProject.Properties.DisplayMember = "ProjectCode";
            cboProject.Properties.DataSource = list;
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
            //if (!Lib.LockEvents) return;
            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang load dữ liệu"))
            {
                try
                {
                    //Lib.LockEvents = true;
                    //if (!SaveData($"Bạn có muốn lưu lại thay đổi không?")) return;

                    DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day, 0, 0, 0);
                    DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59);
                    int statusRequest = cboStatusRequest.SelectedIndex;
                    int projectId = TextUtils.ToInt(cboProject.EditValue);
                    string keyword = txtKeyword.Text.Trim();
                    int isDeleted = cboIsDeleted.SelectedIndex - 1;

                    dtPriceRequest = TextUtils.LoadDataFromSP("spGetProjectPartlistPriceRequest_New", "A",
                                                            new string[] { "@DateStart", "@DateEnd", "@StatusRequest", "@ProjectID", "@Keyword", "@IsDeleted" },
                                                            new object[] { dateStart, dateEnd, statusRequest, projectId, keyword, isDeleted });

                    grdData.DataSource = dtPriceRequest;

                    //Mặc định lọc những yêu cầu chưa xoá
                    //string filterString = $"([IsDeleted] ='x')";
                    //grvData.Columns["IsDeleted"].FilterInfo = new ColumnFilterInfo(filterString);


                    //LoadTabControl();
                    //LoadDataHistory();

                    //Get scale screen
                    var scale = TextUtils.GetScaleScreen(this);
                    if (scale > 1)
                    {

                        foreach (var col in columnFixeds.OrderByDescending(x => x.VisibleIndex))
                        {
                            if (col.VisibleIndex <= 5) continue;
                            col.Fixed = FixedStyle.None;
                        }
                    }
                    else
                    {
                        foreach (var col in columnFixeds.OrderBy(x => x.VisibleIndex))
                        {
                            col.Fixed = FixedStyle.Left;
                        }
                    }
                }
                finally
                {
                    //Lib.LockEvents = false;
                }
            }
        }

        //TODO: 18/05/2024 lhPhuc add new
        void LoadDataHistory(GridView gridView)
        {
            //int ID = TextUtils.ToInt(gridView.GetFocusedRowCellValue(colID));
            //DataTable dt = TextUtils.LoadDataFromSP("spGetProjectPartlistPriceRequestHistory", "A", new string[] { "@ProjectPartlistPriceRequestID" }, new object[] { ID });
            //grdDataHistory.DataSource = dt;
        }

        int tabSelected = 0;
        void LoadTabControl()
        {
            //xtraTabControl1.TabPages.Clear();
            //DataTable dt = TextUtils.LoadDataFromSP("spGetProjectTypeAssignByEmployeeID", "A", new string[] { "@EmployeeID" }, new object[] { Global.EmployeeID });
            //foreach (DataRow row in dt.Rows)
            //{
            //    int typeId = TextUtils.ToInt(row["ID"]);
            //    //if (typeId == 7) continue;
            //    string typeName = TextUtils.ToString(row["ProjectTypeName"]).ToUpper();

            //    DataTable dtType = dtPriceRequest.Clone();

            //    var dataRow = dtPriceRequest.Select($"ProjectTypeID = {typeId}");
            //    if (dataRow.Length <= 0) continue;
            //    dtType = dataRow.CopyToDataTable();
            //    XtraTabPage tabPage = new XtraTabPage();
            //    tabPage.Text = typeName;

            //    tabPage.Controls.Add(CloneGridControl(grdData, dtType));
            //    xtraTabControl1.TabPages.Add(tabPage);
            //}

            //xtraTabControl1.SelectedTabPageIndex = tabSelected;
        }


        //GridControl CloneGridControl(GridControl template, DataTable dataSource)
        //{
        //    GridControl gridControl = new GridControl();
        //    GridView gridView = new GridView(gridControl);

        //    gridView.OptionsBehavior.Editable = false;
        //    gridView.OptionsBehavior.ReadOnly = true;

        //    gridControl.MainView = gridView;
        //    gridControl.Dock = template.Dock;
        //    gridControl.DataSource = dataSource;

        //    gridView.Assign(template.MainView as GridView, false);

        //    gridView.KeyDown += grvData_KeyDown;
        //    gridView.FocusedRowChanged += grvData_FocusedRowChanged;
        //    gridView.RowStyle += grvData_RowStyle;

        //    return gridControl;
        //}

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
            //var dataChange = dtPriceRequest;
            if (dataChange != null)
            {
                //if (string.IsNullOrEmpty(message.Trim()))
                {
                    UpdateData(dataChange);
                    //string sql = "";
                    //foreach (DataRow row in dataChange.Rows)
                    //{
                    //    int id = TextUtils.ToInt(row["ID"]);
                    //    //decimal currencyRate = TextUtils.ToInt(row["ID"]);

                    //    //Tính thành tiền

                    //    decimal quantity = TextUtils.ToDecimal(row["Quantity"]);
                    //    decimal unitPrice = TextUtils.ToDecimal(row["UnitPrice"]);
                    //    decimal totalPrice = quantity * unitPrice;

                    //    //Tính thành tiền quy đổi
                    //    decimal currencyRate = TextUtils.ToDecimal(row["CurrencyRate"]);
                    //    decimal totalPriceExchange = totalPrice * currencyRate;

                    //    //Tính thành tiền có VAT
                    //    decimal vat = TextUtils.ToDecimal(row["VAT"]);
                    //    decimal totalMoneyVAT = totalPrice + ((totalPrice * vat) / 100);


                    //    string columnUpdate = "";
                    //    foreach (GridColumn col in columnEdits)
                    //    {
                    //        //var format = col.DisplayFormat.Format;
                    //        var columnTypeName = col.ColumnType.Name.ToLower();
                    //        string value = TextUtils.ToString(row[col.FieldName]).Trim().ToLower();

                    //        if (columnTypeName == "int32" || columnTypeName == "decimal") value = string.IsNullOrEmpty(value) ? "0" : value;
                    //        else if (columnTypeName == "boolean") value = value == "true" ? "1" : "0";
                    //        else value = $"N'{value}'";

                    //        columnUpdate += $"{col.FieldName} = {value},";
                    //    }

                    //    sql += $"UPDATE dbo.ProjectPartlistPriceRequest SET {columnUpdate}" +
                    //            $"CurrencyRate = {currencyRate}," +
                    //            $"TotalPrice = {totalPrice}," +
                    //            $"TotalPriceExchange = {totalPriceExchange}," +
                    //            $"TotaMoneyVAT = {totalMoneyVAT}," +
                    //            $"UpdatedDate = N'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                    //            $"UpdatedBy = N'{Global.LoginName}'," +
                    //            $"QuoteEmployeeID = {Global.EmployeeID}" +
                    //            $"WHERE ID = {id}\n";
                    //}

                    //if (!string.IsNullOrEmpty(sql.Trim())) TextUtils.ExcuteSQL(sql);
                    //dtPriceRequest.AcceptChanges();
                }
                //else
                //{
                //    DialogResult dialog = MessageBox.Show("Bạn có muốn lưu lại thay đổi không?", "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                //    if (dialog == DialogResult.Yes)
                //    {
                //        UpdateData(dataChange);
                //        //string sql = "";
                //        //foreach (DataRow row in dataChange.Rows)
                //        //{
                //        //    int id = TextUtils.ToInt(row["ID"]);
                //        //    //decimal currencyRate = TextUtils.ToInt(row["ID"]);

                //        //    //Tính thành tiền

                //        //    decimal quantity = TextUtils.ToDecimal(row["Quantity"]);
                //        //    decimal unitPrice = TextUtils.ToDecimal(row["UnitPrice"]);
                //        //    decimal totalPrice = quantity * unitPrice;

                //        //    //Tính thành tiền quy đổi
                //        //    decimal currencyRate = TextUtils.ToDecimal(row["CurrencyRate"]);
                //        //    decimal totalPriceExchange = totalPrice * currencyRate;

                //        //    //Tính thành tiền có VAT
                //        //    decimal vat = TextUtils.ToDecimal(row["VAT"]);
                //        //    decimal totalMoneyVAT = totalPrice + ((totalPrice * vat) / 100);

                //        //    string columnUpdate = "";
                //        //    foreach (GridColumn col in columnEdits)
                //        //    {
                //        //        //var format = col.DisplayFormat.Format;
                //        //        var columnTypeName = col.ColumnType.Name.ToLower();
                //        //        string value = TextUtils.ToString(row[col.FieldName]).Trim().ToLower();

                //        //        if (columnTypeName == "int32" || columnTypeName == "decimal") value = string.IsNullOrEmpty(value) ? "0" : value;
                //        //        else if (columnTypeName == "boolean") value = value == "true" ? "1" : "0";
                //        //        else value = $"N'{value}'";

                //        //        columnUpdate += $"{col.FieldName} = {value},";
                //        //    }

                //        //    sql += $"UPDATE dbo.ProjectPartlistPriceRequest SET {columnUpdate}" +
                //        //            $"CurrencyRate = {currencyRate}," +
                //        //            $"TotalPrice = {totalPrice}," +
                //        //            $"TotalPriceExchange = {totalPriceExchange}," +
                //        //            $"TotaMoneyVAT = {totalMoneyVAT}," +
                //        //            $"UpdatedDate = N'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                //        //            $"UpdatedBy = N'{Global.LoginName}'," +
                //        //            $"QuoteEmployeeID = {Global.EmployeeID}" +
                //        //            $"WHERE ID = {id}\n";
                //        //}

                //        //if (!string.IsNullOrEmpty(sql.Trim())) TextUtils.ExcuteSQL(sql);
                //        //dtPriceRequest.AcceptChanges();
                //    }
                //    else if (dialog == DialogResult.No)
                //    {
                //        foreach (DataRow row in dataChange.Rows)
                //        {
                //            var dataRow = dtPriceRequest.Select($"ID = {row["ID"]}")[0];
                //            if (dataRow == null) continue;

                //            int index = dtPriceRequest.Rows.IndexOf(dataRow);
                //            foreach (GridColumn column in grvData.Columns)
                //            {
                //                if (!dtPriceRequest.Columns.Contains(column.FieldName)) continue;
                //                dtPriceRequest.Rows[index][column.FieldName] = dtPriceRequest.Rows[index][column.FieldName, DataRowVersion.Original];
                //            }
                //        }
                //    }
                //    else
                //    {
                //        return false;
                //    }
                //}

            }
            return true;

        }


        void UpdateData(DataTable dataSoure)
        {

            var columnEdits = grvData.Columns.Where(x => x.OptionsColumn.AllowEdit == true && x.Visible == true).ToList();
            string sql = "";

            using (WaitDialogForm fWait = new WaitDialogForm())
            {
                foreach (DataRow row in dataSoure.Rows)
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

                    int leadTime = TextUtils.ToInt(row["TotalDayLeadTime"]);
                    DateTime dateExpec = AddWeekdays(DateTime.Now, leadTime);

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


                    //sql += $"UPDATE dbo.ProjectPartlistPriceRequest SET {columnUpdate}" +
                    //        $"CurrencyRate = {currencyRate}," +
                    //        $"TotalPrice = {totalPrice}," +
                    //        $"TotalPriceExchange = {totalPriceExchange}," +
                    //        $"TotaMoneyVAT = {totalMoneyVAT}," +
                    //        $"UpdatedDate = N'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                    //        $"UpdatedBy = N'{Global.LoginName}'," +
                    //        $"QuoteEmployeeID = {Global.EmployeeID}," +
                    //        $"DateExpected = '{dateExpec.ToString("yyyy-MM-dd HH:mm:ss")}'" +
                    //        $"WHERE ID = {id}\n";

                    if (Global.IsAdmin)
                    {
                        sql += $"UPDATE dbo.ProjectPartlistPriceRequest SET {columnUpdate}" +
                            $"CurrencyRate = {currencyRate}," +
                            $"TotalPrice = {totalPrice}," +
                            $"TotalPriceExchange = {totalPriceExchange}," +
                            $"TotaMoneyVAT = {totalMoneyVAT}," +
                            $"UpdatedDate = N'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                            $"UpdatedBy = N'{Global.LoginName}'," +
                            //$"QuoteEmployeeID = {Global.EmployeeID}," +
                            $"DateExpected = '{dateExpec.ToString("yyyy-MM-dd HH:mm:ss")}'" +
                            $"WHERE ID = {id}\n";
                    }
                    else
                    {
                        sql += $"UPDATE dbo.ProjectPartlistPriceRequest SET {columnUpdate}" +
                            $"CurrencyRate = {currencyRate}," +
                            $"TotalPrice = {totalPrice}," +
                            $"TotalPriceExchange = {totalPriceExchange}," +
                            $"TotaMoneyVAT = {totalMoneyVAT}," +
                            $"UpdatedDate = N'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                            $"UpdatedBy = N'{Global.LoginName}'," +
                            $"QuoteEmployeeID = {Global.EmployeeID}," +
                            $"DateExpected = '{dateExpec.ToString("yyyy-MM-dd HH:mm:ss")}'" +
                            $"WHERE ID = {id}\n";
                    }

                }

                if (!string.IsNullOrEmpty(sql.Trim())) TextUtils.ExcuteSQL(sql);
                dtPriceRequest.AcceptChanges();
            }
        }

        static DateTime AddWeekdays(DateTime date, int days)
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

        void QuotePrice(int statusRequest)
        {

            grvData.CloseEditor();
            grvData.FocusedRowHandle = -1;
            string statusRequestText = statusRequest == 1 ? "huỷ báo giá" : "báo giá";


            //Check validate
            var selectedRows = grvData.GetSelectedRows();
            if (selectedRows.Length <= 0)
            {
                MessageBox.Show($"Vui lòng chọn sản phẩm muốn {statusRequestText}!", "Thông báo");
                return;
            }

            //SaveData($"Bạn có muốn lưu lại thay đổi trước khi {statusRequestText} không?");

            foreach (int row in selectedRows)
            {
                if (statusRequest == 1) continue;

                int id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
                if (id <= 0) continue;


                ProjectPartlistPriceRequestModel requestModel = SQLHelper<ProjectPartlistPriceRequestModel>.FindByID(id);
                if (requestModel.QuoteEmployeeID != Global.EmployeeID) continue;

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


            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn {statusRequestText} danh sách sản phẩm đã chọn không?\nNhững sản phẩm NV mua không phải bạn sẽ tự động được bỏ qua!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                DataTable data = dtPriceRequest.Clone();
                foreach (int row in selectedRows)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
                    ProjectPartlistPriceRequestModel requestModel = SQLHelper<ProjectPartlistPriceRequestModel>.FindByID(id);
                    if (requestModel.QuoteEmployeeID != Global.EmployeeID) continue;

                    if (row < 0) continue;
                    DataRow dataRow = data.NewRow();

                    dataRow = grvData.GetDataRow(row);
                    data.ImportRow(dataRow);
                }
                UpdateData(data);

                List<int> listIDs = new List<int>();
                foreach (int row in selectedRows)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
                    if (id <= 0) continue;

                    ProjectPartlistPriceRequestModel requestModel = SQLHelper<ProjectPartlistPriceRequestModel>.FindByID(id);
                    if (requestModel.QuoteEmployeeID != Global.EmployeeID) continue;

                    listIDs.Add(id);


                    string productCode = TextUtils.ToString(grvData.GetRowCellValue(row, colProductCode));
                    string productName = TextUtils.ToString(grvData.GetRowCellValue(row, colProductName));
                    string project = TextUtils.ToString(grvData.GetRowCellValue(row, colProjectFullName));
                    string textNotify = $"Dự án: {project}" +
                                        $"Mã sản phầm: {productCode}\n" +
                                        $"Tên sản phầm: {productName}";

                    int employee = TextUtils.ToInt(grvData.GetRowCellValue(row, colEmployeeID));
                    TextUtils.AddNotify("BÁO GIÁ PARTLIST", textNotify, employee);
                }

                if (listIDs.Count <= 0) return;
                int isCheckPriceValue = statusRequest == 2 ? 1 : 0;

                string idText = string.Join(",", listIDs);
                string sql = $"UPDATE ProjectPartlistPriceRequest SET StatusRequest = {statusRequest}," +
                                $"DatePriceQuote = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                                $"UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                                $"UpdatedBy = '{Global.LoginName}'," +
                                $"QuoteEmployeeID = {Global.EmployeeID}," +
                                $"IsCheckPrice = {isCheckPriceValue}" +
                                $"WHERE ID IN({idText})";

                if (statusRequest != 2)
                {
                    sql = $"UPDATE ProjectPartlistPriceRequest SET StatusRequest = {statusRequest}," +
                                $"DatePriceQuote = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                                $"UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                                $"UpdatedBy = '{Global.LoginName}'," +
                                $"QuoteEmployeeID = {Global.EmployeeID}" +
                                $"WHERE ID IN({idText})";
                }


                TextUtils.ExcuteSQL(sql);

                SendMail(selectedRows, grvData);
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

        void SendMail(int[] rowSelecteds, GridView gridView) // NTA B update 15/09/25
        {
            //int[] rows = grvData.GetSelectedRows();
            EmployeeSendEmailModel sendEmail = new EmployeeSendEmailModel();
            List<EmployeeModel> employees = SQLHelper<EmployeeModel>.FindByAttribute(EmployeeModel_Enum.Status.ToString(), 0);
            List<CurrencyModel> currencies = SQLHelper<CurrencyModel>.FindAll();
            try
            {
                if (rowSelecteds.Length <= 0) return;

                List<DataRow> lstData = new List<DataRow>();
                foreach (int item in rowSelecteds)
                {
                    DataRow d = gridView.GetDataRow(item);
                    if (d == null) continue;
                    lstData.Add(d);
                }
                DataTable dt = lstData.CopyToDataTable();
                if (dt != null)
                {
                    var grouped = dt.AsEnumerable()
                        .GroupBy(r => new
                        {
                            ID = r.IsNull("EmployeeID") ? 0 : Convert.ToInt32(r["EmployeeID"])
                        })
                        .Select(g =>
                        {
                            var emp = employees.FirstOrDefault(x => x.ID == g.Key.ID);
                            return new
                            {
                                g.Key.ID,
                                FullName = emp?.FullName ?? "Không rõ",
                                Email = !string.IsNullOrEmpty(emp?.EmailCongTy) ? emp.EmailCongTy : emp?.EmailCaNhan,
                                QuoteEmployee = g.Select(x => x.Field<string>("QuoteEmployee")),
                                ListQuotePrice = g.Select(x => new
                                {
                                    ProjectCode = x.Field<string>("ProjectCode"),
                                    ProductCode = x.Field<string>("ProductCode"),
                                    ProductName = x.Field<string>("ProductName"),
                                    Manufacturer = x.Field<string>("Manufacturer"),
                                    Quantity = x.Field<decimal>("Quantity"),
                                    Unit = x.Field<string>("Unit"),
                                    DateRequest = x.Field<DateTime?>("DateRequest"),
                                    Deadline = x.Field<DateTime?>("Deadline"),
                                    DatePriceQuote = x.Field<DateTime?>("DatePriceQuote"),
                                    //CurrencyType = x.Field<string>("CurrencyID"),
                                    CurrencyType = TextUtils.ToString(currencies.FirstOrDefault(c => c.ID == TextUtils.ToInt(x["CurrencyID"]))?.Code),
                                    UnitPrice = x.Field<decimal>("UnitPrice"),
                                    TotalPriceExchange = x.Field<decimal>("TotalPriceExchange"), // Thành tiền quy đổi
                                    TotalPrice = x.Field<decimal>("TotalPrice"), // Thành tiền chưa VAT

                                }).Distinct().ToList()
                            };
                        }).ToList();

                    foreach (var row in grouped)
                    {
                        sendEmail.Subject = $"BÁO GIÁ SẢN PHẨM NGÀY: {DateTime.Now:dd/MM/yyyy}";
                        sendEmail.EmailTo = row.Email;
                        sendEmail.StatusSend = 1;
                        sendEmail.DateSend = DateTime.Now;
                        sendEmail.EmployeeID = Global.EmployeeID;
                        sendEmail.Receiver = row.ID;
                        sendEmail.TableInfor = "";
                        sendEmail.Body = $@"
                                            <div style='font-family: Arial; font-size: 14px;'>
                                                Dear <b>{row.FullName}</b>,<br/><br/>
                                                Nhân viên <b>{string.Join(", ", row.QuoteEmployee.Distinct())}</b> đã báo giá danh sách sản phẩm sau:<br/><br/>
                                                <table border='1' cellspacing='0' cellpadding='5' style='border-collapse:collapse; width:100%;'>
                                                    <thead style='background-color:#f2f2f2;'>
                                                        <tr>
                                                            <th>Dự án</th>
                                                            <th>Mã sản phẩm</th>
                                                            <th>Tên sản phẩm</th>
                                                            <th>Hãng</th>
                                                            <th>Số lượng / Đơn vị</th>
                                                            <th>Đơn giá / Loại tiền</th>
                                                            <th>Thành tiền chưa VAT</th>
                                                            <th>Thành tiền quy đổi (VND)</th>
                                                            <th>Ngày yêu cầu</th>
                                                            <th>Deadline</th>
                                                            <th>Ngày báo giá</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        {string.Join("", row.ListQuotePrice.Select(d => $@"
                                                            <tr>
                                                                <td>{d.ProjectCode}</td>
                                                                <td>{d.ProductCode}</td>
                                                                <td>{d.ProductName}</td>
                                                                <td>{d.Manufacturer}</td>
                                                                <td style='text-align:right;'>{d.Quantity} {d.Unit}</td>
                                                                <td style='text-align:right;'>{d.UnitPrice} {d.CurrencyType}</td>
                                                                <td style='text-align:right;'>{d.TotalPrice:N2} VND</td>
                                                                <td style='text-align:right;'>{d.TotalPriceExchange:N2} VND</td>
                                                                <td style='text-align:center;'>{d.DateRequest:dd/MM/yyyy HH:mm}</td>
                                                                <td style='text-align:center;'>{d.Deadline:dd/MM/yyyy}</td>
                                                                <td style='text-align:center;'>{d.DatePriceQuote:dd/MM/yyyy HH:mm}</td>
                                                            </tr>
                                                        "))}
                                                    </tbody>
                                                </table>
                                                <br/>
                                                Trân trọng.
                                            </div>";
                        SQLHelper<EmployeeSendEmailModel>.Insert(sendEmail);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message} \r\n(SendMail)\r\n {ex.ToString()}");
            }
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

            string message = isCheckPrice ? "\nNhững sản phẩm đã có NV mua check sẽ tự động được bỏ qua!" : "";

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn {isCheckText} danh sách sản phẩm đã chọn không?{message}", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                List<int> listIDs = new List<int>();
                foreach (int row in selectedRows)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
                    if (id <= 0) continue;

                    if (isCheckPrice)
                    {
                        ProjectPartlistPriceRequestModel requestModel = SQLHelper<ProjectPartlistPriceRequestModel>.FindByID(id);
                        if (requestModel.QuoteEmployeeID != Global.EmployeeID && requestModel.QuoteEmployeeID > 0) continue;
                    }

                    listIDs.Add(id);
                }

                if (listIDs.Count <= 0) return;
                string idText = string.Join(",", listIDs);
                //string updatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                int isCheckPriceValue = isCheckPrice ? 1 : 0;


                var myDict = new Dictionary<string, object>()
                {
                    { "IsCheckPrice",isCheckPriceValue},
                    { "UpdatedBy",Global.LoginName},
                    { ProjectPartlistPriceRequestModel_Enum.QuoteEmployeeID.ToString(), isCheckPrice ? Global.EmployeeID:0}
                };

                //if (isCheckPrice)
                //{
                //    myDict.Add(ProjectPartlistPriceRequestModel_Enum.QuoteEmployeeID.ToString(), Global.EmployeeID);
                //}
                //else
                //{
                //    myDict.Add(ProjectPartlistPriceRequestModel_Enum.QuoteEmployeeID.ToString(), 0);
                //}

                var exp = new Expression("ID", idText, "IN");
                SQLHelper<ProjectPartlistPriceRequestModel>.UpdateFields(myDict, exp);

                //string sql = $"UPDATE ProjectPartlistPriceRequest " +
                //            $"SET IsCheckPrice = {isCheckPriceValue}, " +
                //            $"IsDeleted = 0," +
                //            $"UpdatedBy = N'{Global.LoginName}', " +
                //            $"UpdatedDate = '{updatedDate}' " +
                //            $"WHERE ID IN ({idText})";
                //TextUtils.ExcuteSQL(sql);
                LoadData();
            }

        }

        void CompleteQuotePrice(int status)
        {
            string statusText = status == 3 ? "hoàn thành" : "hủy hoàn thành";
            SaveData($"Bạn có muốn lưu lại thay đổi trước khi {statusText} không?");

            int[] rowSelecteds = grvData.GetSelectedRows();
            if (rowSelecteds.Length <= 0)
            {
                MessageBox.Show($"Vui lòng chọn sản phẩm muốn {statusText}!", "Thông báo");
                return;
            }

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn {statusText} danh sách sản phẩm đã chọn không?\nNhững sản phẩm NV mua không phải bạn sẽ tự động được bỏ qua!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                List<int> listUpdates = new List<int>();
                foreach (int row in rowSelecteds)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(row, "ID"));
                    if (id <= 0) continue;

                    ProjectPartlistPriceRequestModel requestModel = SQLHelper<ProjectPartlistPriceRequestModel>.FindByID(id);
                    if (requestModel.QuoteEmployeeID != Global.EmployeeID) continue;

                    listUpdates.Add(id);
                }

                if (listUpdates.Count <= 0) return;
                string idUpdateText = string.Join(",", listUpdates);

                var myDict = new Dictionary<string, object>()
                {
                    { "StatusRequest",status},
                    { "UpdatedBy",Global.LoginName},
                    { "UpdatedDate",DateTime.Now},
                    { "QuoteEmployeeID",Global.EmployeeID},
                };

                var exp = new Expression("ID", idUpdateText, "IN");
                SQLHelper<ProjectPartlistPriceRequestModel>.UpdateFields(myDict, exp);
                btnSearch_Click(null, null);
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


        void UpdateValue(int row)
        {
            decimal quantity = TextUtils.ToDecimal(grvData.GetRowCellValue(row, colQuantity));
            decimal unitPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(row, colUnitPrice));
            decimal unitImportPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(row, colUnitImportPrice));
            decimal vat = TextUtils.ToDecimal(grvData.GetRowCellValue(row, colVAT));

            decimal totalPrice = quantity * unitPrice;

            //Tính thành tiền
            grvData.SetRowCellValue(row, colTotalPrice, totalPrice);
            grvData.SetRowCellValue(row, colTotalPriceExchange, CalculatorTotalMoneyExchange(row));

            //Tính đơn giá nhập khẩu
            decimal totalPriceImport = quantity * unitImportPrice;
            grvData.SetRowCellValue(row, colTotalImportPrice, totalPriceImport);

            //Tính thành tiền có VAT
            decimal totalMoneyVAT = totalPrice + ((totalPrice * vat) / 100);
            grvData.SetRowCellValue(row, colTotaMoneyVAT, totalMoneyVAT);

            //Tính ngày về dự kiến
            if (grvData.FocusedColumn == colTotalDayLeadTime)
            {
                int leadTime = TextUtils.ToInt(grvData.GetRowCellValue(row, colTotalDayLeadTime));
                grvData.SetRowCellValue(row, colDateExpected, AddWeekdays(DateTime.Now, leadTime));
            }
        }

        //void Calutor

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
            //FolderBrowserDialog f = new FolderBrowserDialog();

            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            f.FileName = $"YeuCauBaoGia_{dtpDateStart.Value.ToString("ddMMyy")}_{dtpDateEnd.Value.ToString("ddMMyy")}.xlsx";
            if (f.ShowDialog() == DialogResult.OK)
            {
                //string filepath = Path.Combine(f.SelectedPath, $"YeuCauBaoGia_{DateTime.Now.ToString("ddMMyy")}.xlsx");
                string filepath = f.FileName;

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
            //return;
            if (grvData.FocusedColumn == colCurrencyID) return;
            if (isRecallCellValueChanged == true) return;
            try
            {

                using (WaitDialogForm fWait = new WaitDialogForm())
                {
                    isRecallCellValueChanged = true;
                    grvData.CloseEditor();

                    if (grvData.SelectedRowsCount > 0)
                    {
                        if (e.Value == null) return;
                        foreach (int row in grvData.GetSelectedRows())
                        {
                            //if (e.Column == colTotalPriceExchange) continue;
                            //if (e.Column == colTotalPrice) continue;
                            if (e.Column != colTotalPriceExchange && e.Column != colTotalPrice)
                            {
                                grvData.SetRowCellValue(row, grvData.Columns[e.Column.FieldName], e.Value);
                            }

                            if (grvData.FocusedColumn != colUnitPrice
                                && grvData.FocusedColumn != colUnitImportPrice
                                && grvData.FocusedColumn != colVAT
                                && grvData.FocusedColumn != colTotalDayLeadTime) continue;

                            UpdateValue(row);
                            //decimal quantity = TextUtils.ToDecimal(grvData.GetRowCellValue(row, colQuantity));
                            //decimal unitPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(row, colUnitPrice));
                            //decimal unitImportPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(row, colUnitImportPrice));
                            //decimal vat = TextUtils.ToDecimal(grvData.GetRowCellValue(row, colVAT));

                            //decimal totalPrice = quantity * unitPrice;

                            ////Tính thành tiền
                            //grvData.SetRowCellValue(row, colTotalPrice, totalPrice);
                            //grvData.SetRowCellValue(row, colTotalPriceExchange, CalculatorTotalMoneyExchange(row));

                            ////Tính đơn giá nhập khẩu
                            //decimal totalPriceImport = quantity * unitImportPrice;
                            //grvData.SetRowCellValue(row, colTotalImportPrice, totalPriceImport);

                            ////Tính thành tiền có VAT
                            //decimal totalMoneyVAT = totalPrice + ((totalPrice * vat) / 100);
                            //grvData.SetRowCellValue(row, colTotaMoneyVAT, totalMoneyVAT);


                            ////Tính ngày về dự kiến
                            //if (grvData.FocusedColumn == colTotalDayLeadTime)
                            //{
                            //    int leadTime = TextUtils.ToInt(grvData.GetRowCellValue(row, colTotalDayLeadTime));
                            //    grvData.SetRowCellValue(row, colDateExpected, AddWeekdays(DateTime.Now, leadTime));
                            //}

                            //if (e.Column != colSupplierSaleID
                            //    && e.Column != colUnitPrice
                            //    && e.Column != colUnitImportPrice
                            //    //&& e.Column != colCurrencyID
                            //    && e.Column != colVAT
                            //    && e.Column != colTotalDayLeadTime) continue;



                            //decimal totalPrice = quantity * unitPrice;
                            //grvData.GetRowCellValue(row, colTotalPrice, totalPrice);
                            //grvData.GetRowCellValue(row, colTotalPriceExchange, CalculatorTotalMoneyExchange(row));

                            //if (e.Column == colUnitImportPrice)
                            //{
                            //    decimal quantityImport = TextUtils.ToDecimal(grvData.GetRowCellValue(row, colQuantity));
                            //    decimal unitImportPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(row, colUnitImportPrice));
                            //    decimal totalPriceImport = quantityImport * unitImportPrice;

                            //    grvData.SetFocusedRowCellValue(colTotalImportPrice, totalPriceImport);
                            //}

                            ////Tính thành tiền có VAT
                            //decimal vat = TextUtils.ToDecimal(grvData.GetRowCellValue(row, colVAT));
                            //decimal totalMoneyVAT = totalPrice + ((totalPrice * vat) / 100);
                            //grvData.SetRowCellValue(row, colTotaMoneyVAT, totalMoneyVAT);


                            ////Tính ngày về dự kiến
                            //if (e.Column == colTotalDayLeadTime)
                            //{
                            //    int leadTime = TextUtils.ToInt(grvData.GetRowCellValue(rơcolTotalDayLeadTime));
                            //    grvData.SetRowCellValue(row, colDateExpected, AddWeekdays(DateTime.Now, leadTime));
                            //}

                            //grvData.SetRowCellValue(row,colStatusUpdate, 2);
                        }
                    }
                    else
                    {
                        UpdateValue(grvData.FocusedRowHandle);
                    }
                    //if (e.Value != null && grvData.SelectedRowsCount > 0)
                    //{

                    //}
                }
            }
            finally
            {
                isRecallCellValueChanged = false;
            }



        }

        private void cboUnit_EditValueChanged(object sender, EventArgs e)
        {
            //SearchLookUpEdit lookUpEdit = (SearchLookUpEdit)sender;
            //CurrencyModel currency = (CurrencyModel)lookUpEdit.GetSelectedDataRow();

            //grvData.SetFocusedRowCellValue(colUnit, currency.Code);
            //grvData.SetFocusedRowCellValue(colCurrencyRate, currency.CurrencyRate);
            //grvData.SetFocusedRowCellValue(colTotalPriceExchange, CalculatorTotalMoneyExchange(grvData.FocusedRowHandle));
            //grvData.SetFocusedRowCellValue(colStatusUpdate, 2);
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
            if (currency == null) return;

            bool isExpried = ((currency.DateExpried.Value.Date < DateTime.Now.Date || currency.DateStart.Value.Date > DateTime.Now.Date) && currency.Code.ToLower().Trim() != "vnd");

            int[] rowSelecteds = grvData.GetSelectedRows();
            if (rowSelecteds.Length <= 0)
            {
                grvData.SetRowCellValue(grvData.FocusedRowHandle, colCurrencyID, currency.ID);
                grvData.SetRowCellValue(grvData.FocusedRowHandle, colCurrencyRate, currency.CurrencyRate);
                if (isExpried)
                {
                    grvData.SetRowCellValue(grvData.FocusedRowHandle, colCurrencyRate, 0);
                }
                grvData.SetRowCellValue(grvData.FocusedRowHandle, colTotalPriceExchange, CalculatorTotalMoneyExchange(grvData.FocusedRowHandle));
                grvData.SetRowCellValue(grvData.FocusedRowHandle, colStatusUpdate, 2);
            }
            else
            {
                using (WaitDialogForm fWait = new WaitDialogForm())
                {
                    foreach (int row in rowSelecteds)
                    {
                        grvData.SetRowCellValue(row, colCurrencyID, currency.ID);
                        grvData.SetRowCellValue(row, colCurrencyRate, currency.CurrencyRate);
                        if (isExpried)
                        {
                            grvData.SetRowCellValue(row, colCurrencyRate, 0);
                        }
                        grvData.SetRowCellValue(row, colTotalPriceExchange, CalculatorTotalMoneyExchange(row));
                        grvData.SetRowCellValue(row, colStatusUpdate, 2);
                    }
                }
            }

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
                string value = TextUtils.ToString(gridView.GetFocusedRowCellDisplayText(gridView.FocusedColumn));
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
                        int projectId = TextUtils.ToInt(grvData.GetRowCellValue(row, colProjectID));

                        ProjectModel project = SQLHelper<ProjectModel>.FindByID(projectId);
                        if (project == null) continue;
                        if (!project.CreatedDate.HasValue) continue;

                        int projectPartlistId = TextUtils.ToInt(grvData.GetRowCellValue(row, colProjectPartListID));

                        ProjectSolutionModel solution = SQLHelper<ProjectSolutionModel>.ProcedureToList("spGetProjectSolutionByProjectPartListID",
                                                            new string[] { "@ProjectPartListID" }, new object[] { projectPartlistId }).FirstOrDefault();
                        if (solution == null) continue;
                        if (string.IsNullOrEmpty(solution.CodeSolution)) continue;
                        string pathPattern = $@"{project.CreatedDate.Value.Year}/{project.ProjectCode.Trim()}/THIETKE.Co/{solution.CodeSolution.Trim()}/2D/GC/DH";

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
                    MessageBox.Show($"File [{productCode}.pdf] không tồn tại!\r\n{ex.ToString()}", "Thông báo");
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


            //var view = sender as GridView;
            if (gridView.FocusedRowHandle == e.RowHandle)
            {
                e.Appearance.BackColor = Color.LightYellow;
                e.HighPriority = true;
            }
        }

        private void frmProjectPartlistPriceRequestOld_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !SaveData($"Bạn có muốn lưu lại thay đổi không?");
        }

        private void btnCompleteQuotePrice_Click(object sender, EventArgs e)
        {
            CompleteQuotePrice(3);
        }

        private void btnUnCompleteQuotePrice_Click(object sender, EventArgs e)
        {
            CompleteQuotePrice(1);
        }

        private void cboStatusRequest_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void chkIsCommercialProduct_CheckedChanged(object sender, EventArgs e)
        {

        }

        int rowHandle = 0;
        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }


        private void Frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            btnSearch_Click(null, null);
            grvData.FocusedRowHandle = rowHandle;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {


            //int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            int projectID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProjectID));
            //rowHandle = grvData.FocusedRowHandle;
            //if (id <= 0) return;


            frmProjectPartlistPriceRequestDetail frm = new frmProjectPartlistPriceRequestDetail();
            //frm.priceRequest = requestModel;
            frm.projectID = projectID;
            //frm.Tag = requestModel.ID;
            //frm.Text += $" - {requestModel.ProductCode}";


            int[] rowSelecteds = grvData.GetSelectedRows();
            DataTable dataEdit = dtPriceRequest.Clone();
            if (rowSelecteds.Length <= 0)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(grvData.FocusedRowHandle, colID));
                if (id > 0)
                {
                    //ProjectPartlistPriceRequestModel requestModel = SQLHelper<ProjectPartlistPriceRequestModel>.FindByID(id);
                    //frm.priceRequests.Add(requestModel);

                    DataRow dataRow = dataEdit.NewRow();

                    dataRow = grvData.GetDataRow(grvData.FocusedRowHandle);
                    dataEdit.ImportRow(dataRow);

                }
            }
            else
            {

                foreach (int row in rowSelecteds)
                {
                    if (row < 0) continue;


                    DataRow dataRow = dataEdit.NewRow();

                    dataRow = grvData.GetDataRow(row);
                    dataEdit.ImportRow(dataRow);
                }

                //foreach (int row in rowSelecteds)
                //{
                //    int id = TextUtils.ToInt(grvData.GetRowCellValue(row,colID));
                //    if (id <= 0) continue;
                //    ProjectPartlistPriceRequestModel requestModel = SQLHelper<ProjectPartlistPriceRequestModel>.FindByID(id);

                //    frm.priceRequests.Add(requestModel);
                //}
            }

            frm.dtEdit = dataEdit;

            TextUtils.OpenChildForm(frm, null);
            frm.FormClosed += Frm_FormClosed;
        }

        private void btnExportExcelAll_Click(object sender, EventArgs e)
        {
            ExportExcel(false);
        }

        private void ExportExcel(bool choise)
        {
            try
            {

                DataTable dtNew = ((DataTable)grdData.DataSource).Clone();
                DataTable dtOld = ((DataTable)grdData.DataSource);
                int[] listFocus = { };
                if (choise)
                {
                    if (grvData.RowCount <= 0) return;
                    if (grvData.SelectedRowsCount <= 0)
                    {
                        MessageBox.Show($"Vui lòng chọn sản phẩm cần xuất excel!", "Thông báo");
                        return;
                    }

                    listFocus = grvData.GetSelectedRows();
                    foreach (int rowIndex in listFocus)
                    {
                        if (rowIndex < 0) continue;

                        DataRow rowObj = grvData.GetDataRow(rowIndex);
                        if (rowObj == null) continue;
                        //DataRow row = rowView.Row;
                        dtNew.ImportRow(rowObj);
                    }
                }

                SaveFileDialog f = new SaveFileDialog();
                f.Filter = "Excel Files|*.xlsx";
                string name = !choise ? "All" : "";
                f.FileName = $"YeuCauBaoGia_${name}_{dtpDateStart.Value.ToString("ddMMyy")}_{dtpDateEnd.Value.ToString("ddMMyy")}.xlsx";
                if (f.ShowDialog() == DialogResult.OK)
                {
                    string filepath = f.FileName;

                    XlsxExportOptions optionsEx = new XlsxExportOptions();
                    PrintingSystem printingSystem = new PrintingSystem();

                    PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                    if (choise) grdData.DataSource = dtNew;
                    printableComponentLink1.Component = grdData;


                    CompositeLink compositeLink = new CompositeLink(printingSystem);
                    compositeLink.Links.Add(printableComponentLink1);
                    compositeLink.CreatePageForEachLink();
                    optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;
                    compositeLink.PrintingSystem.SaveDocument(filepath);
                    compositeLink.ExportToXlsx(filepath, optionsEx);

                    if (choise)
                    {
                        grdData.DataSource = dtOld;
                        foreach (int rowIndex in listFocus) grvData.SelectRow(rowIndex);
                        grvData.MakeRowVisible(listFocus.Min());
                    }

                    Process.Start(filepath);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExportExcelChoise_Click(object sender, EventArgs e)
        {
            ExportExcel(true);
        }


        //private void xtraTabControl1_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        //{
        //    tabSelected = xtraTabControl1.SelectedTabPageIndex;
        //}

        //private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        //{
        //    GridView gridView = (GridView)sender;
        //    LoadDataHistory(gridView);
        //}

        //private void btnQuotePriceHistory_Click(object sender, EventArgs e)
        //{
        //    var tabSelected = xtraTabControl1.SelectedTabPage;
        //    var controls = tabSelected.Controls;

        //    if (controls.Count <= 0) return;
        //    GridControl gridControl = (GridControl)controls[0];
        //    GridView gridView = (GridView)gridControl.MainView;

        //    int rowHandle = gridView.FocusedRowHandle;
        //    int id = TextUtils.ToInt(gridView.GetFocusedRowCellValue(colID));
        //    string tt = TextUtils.ToString(gridView.GetFocusedRowCellValue(colTT));
        //    string unitCount = TextUtils.ToString(gridView.GetFocusedRowCellValue(colUnitCount));
        //    if (id > 0)
        //    {
        //        ProjectPartlistPriceRequestModel model = SQLHelper<ProjectPartlistPriceRequestModel>.FindByID(id);
        //        if (model == null) return;

        //        frmQuotePriceHistory frm = new frmQuotePriceHistory();
        //        frm.modelPPLPR = model;
        //        frm.tt = tt;
        //        frm.unitCount = unitCount;
        //        if (frm.ShowDialog() == DialogResult.OK)
        //        {
        //            LoadData();
        //            gridView.FocusedRowHandle = rowHandle;
        //        }
        //    }
        //}
    }
}
