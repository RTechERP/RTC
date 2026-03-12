using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraTab;
using Forms.Purchase;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BMS
{
    public partial class frmProjectPartlistPriceRequestNew : _Forms
    {
        public int poKHID = 0;

        List<GridColumn> columnFixeds = new List<GridColumn>();

        //public bool isJobRequirementShow = false;
        public int jobRequirementID = 0;
        public bool isVPP = false;


        int _projectPartlistPriceRequestTypeID = 0;
        public frmProjectPartlistPriceRequestNew(int projectPartlistPriceRequestTypeID)
        {
            InitializeComponent();
            _projectPartlistPriceRequestTypeID = projectPartlistPriceRequestTypeID;
        }

        private void frmProjectPartlistPriceRequestNew_Load(object sender, EventArgs e)
        {
            dtpDateStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDateEnd.Value = dtpDateStart.Value.AddMonths(+1).AddDays(-1);
            cboStatusRequest.SelectedIndex = 1;
            cboIsDeleted.SelectedIndex = 1;

            columnFixeds = grvData.Columns.Where(x => x.Fixed == FixedStyle.Left).ToList();


            //colDeadline.OptionsColumn.AllowEdit = Global.IsAdmin && Global.EmployeeID <= 0;
            //colDeadline.OptionsColumn.ReadOnly = !(Global.IsAdmin && Global.EmployeeID <= 0);

            LoadProject();
            LoadSupplierSale();
            LoadCurrency();
            LoadPOCode();
            LoadData();
            LoadViewToJobRequirement();


            
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

        void LoadPOCode()
        {
            List<POKHModel> dt = SQLHelper<POKHModel>.FindAll();
            cboPOCode.Properties.DisplayMember = "POCode";
            cboPOCode.Properties.ValueMember = "ID";
            cboPOCode.Properties.DataSource = dt;
            cboPOCode.EditValue = poKHID;
        }



        void LoadData()
        {
            if (Global.DepartmentID != 4 && !Global.IsAdmin)
            {
                btnSaveData.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                btnCheckPrice.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                btnUnCheckPrice.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                btnQuotePrice.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                btnUnQuotePrice.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                btnCompleteQuotePrice.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                btnUnCompleteQuotePrice.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                btnUnCompleteQuotePrice.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                btnAddSupplierSale.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                btnDownloadFile.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            var tabSelected = xtraTabControl1.SelectedTabPage;
            if (tabSelected.Controls.Count > 0)
            {
                GetDataChange((GridControl)tabSelected.Controls[0]);
            }


            int projectTypeIdHR = 0;
            if (jobRequirementID > 0 || isVPP) projectTypeIdHR = -2; //HCNS

            DataTable dtType = TextUtils.LoadDataFromSP("spGetProjectTypeAssignByEmployeeID", "A",
                                                    new string[] { "@EmployeeID", "@ProjectTypeID" },
                                                    new object[] { Global.EmployeeID, projectTypeIdHR });

            //Get scale screen
            var scale = TextUtils.GetScaleScreen(this);
            if (scale > 1)
            {

                foreach (var col in columnFixeds.OrderByDescending(x => x.VisibleIndex))
                {
                    if (col.VisibleIndex <= 4) continue;
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

            for (int i = 0; i < dtType.Rows.Count; i++)
            {
                int tabCount = xtraTabControl1.TabPages.Count;
                if (tabCount > dtType.Rows.Count)
                {
                    int indexRemoveTab = xtraTabControl1.TabPages.Count - 1;
                    xtraTabControl1.TabPages.RemoveAt(indexRemoveTab);
                }
                else if (tabCount < dtType.Rows.Count)
                {
                    xtraTabControl1.TabPages.Add(new XtraTabPage());
                }


                DataRow row = dtType.Rows[i];
                int projectTypeID = TextUtils.ToInt(row["ProjectTypeID"]);
                string typeCode = TextUtils.ToString(row["ProjectTypeCode"]);
                xtraTabControl1.TabPages[i].Text = TextUtils.ToString(row["ProjectTypeName"]);
                xtraTabControl1.TabPages[i].Name = $"tab{typeCode}";


                GridControl gridControl = new GridControl();
                GridView gridView = new GridView(gridControl);
                gridView.Name = $"grvData{typeCode}";
                gridControl.MainView = gridView;
                gridControl.Dock = DockStyle.Fill;
                gridControl.Name = $"grdData{typeCode}";
                gridControl.Tag = $"{projectTypeID}";

                gridView.Assign(grdData.MainView, true);

                if (typeCode == "IsJobRequirement")
                {
                    gridView.Columns[colProjectFullName.FieldName].Caption = "Mã YCCV";
                    gridView.Columns[colProjectCode.FieldName].Caption = "Mã YCCV";
                }


                xtraTabControl1.TabPages[i].Controls.Clear();

                if (true)
                {

                }
                xtraTabControl1.TabPages[i].Controls.Add(gridControl);


                //xtraTabControl1.TabPages[i].PageVisible = poKHID > 0
                if (poKHID > 0 && projectTypeID != -1)
                {
                    xtraTabControl1.TabPages[i].PageVisible = false;
                }

                if (_projectPartlistPriceRequestTypeID == 3 && projectTypeID != -2)
                {
                    xtraTabControl1.TabPages[i].PageVisible = false;
                }

                if (_projectPartlistPriceRequestTypeID == 4 && projectTypeID != -3)
                {
                    xtraTabControl1.TabPages[i].PageVisible = false;
                }
                if (_projectPartlistPriceRequestTypeID == 4 && projectTypeID == -3) tabSelected = xtraTabControl1.TabPages[i];

            }

            //xtraTabControl1.SelectedTabPage = xtraTabControl1.TabPages[0];
            if (tabSelected.Controls.Count > 0) LoadData((GridControl)tabSelected.Controls[0]);
        }



        void LoadData(GridControl gridControl)
        {
            //var tabSelected = xtraTabControl1.SelectedTabPage;

            if (gridControl == null) return;
            DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59);
            int statusRequest = cboStatusRequest.SelectedIndex;
            int projectId = TextUtils.ToInt(cboProject.EditValue);
            string keyword = txtKeyword.Text.Trim();
            int isDeleted = cboIsDeleted.SelectedIndex - 1;
            int projectTypeID = TextUtils.ToInt(gridControl.Tag);
            int isCommercialProduct = -1;
            int poKHID = TextUtils.ToInt(cboPOCode.EditValue);
            int isJobRequirement = -1;
            //if (projectTypeID < 0) isCommercialProduct = 1;
            //else poKHID = 0;
            int projectPartlistPriceRequestTypeID = -1;
            int employeeID = 0;

            if (projectTypeID == -1) isCommercialProduct = 1; // VTN update 18225
            else if (projectTypeID == -2)
            {
                projectTypeID = -1;
                isJobRequirement = 1;
            }
            else if (projectTypeID == -3) //Hàng MKT
            {
                projectPartlistPriceRequestTypeID = 4;
                isCommercialProduct = 0;
                //employeeID = Global.EmployeeID;
            }
            else if (projectTypeID == 0)
            {
                isCommercialProduct = 0;
                isJobRequirement = 0;
                //projectPartlistPriceRequestTypeID = -1;
            }
            poKHID = 0;

            //int isJobRequirement = -1;
            if (jobRequirementID > 0 || isVPP) isJobRequirement = 1;
            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang load dữ liệu..."))
            {
                DataTable dtPriceRequest = TextUtils.LoadDataFromSP("spGetProjectPartlistPriceRequest_New", "A",
                    new string[] { "@DateStart", "@DateEnd", "@StatusRequest", "@ProjectID", "@Keyword", "@IsDeleted", "@ProjectTypeID", "@IsCommercialProduct", "@POKHID", "@IsJobRequirement", "@ProjectPartlistPriceRequestTypeID", "@EmployeeID" },
                    new object[] { dateStart, dateEnd, statusRequest, projectId, keyword, isDeleted, projectTypeID, isCommercialProduct, poKHID, isJobRequirement, projectPartlistPriceRequestTypeID, employeeID });

                gridControl.DataSource = dtPriceRequest;
            }

        }

        void GetDataChange(GridControl gridControl)
        {
            var text = xtraTabControl1.SelectedTabPage.Text;
            if (gridControl == null) return;

            GridView gridView = gridControl.MainView as GridView;
            gridView.CloseEditor();
            gridView.FocusedRowHandle = -1;

            DataTable dt = (DataTable)gridControl.DataSource;
            if (dt == null) return;
            DataTable dtChange = dt.GetChanges();

            if (dtChange != null)
            {
                //Lưu dữ liệu thay đổi
                UpdateData(dtChange);
                LoadData(gridControl);
            }
        }
        bool isAdmin = (Global.IsAdmin && Global.EmployeeID <= 0);

        void UpdateData(DataTable dt)
        {

            var columnEdits = grvData.Columns.Where(x => x.OptionsColumn.AllowEdit == true && x.Visible == true).ToList();
            foreach (DataRow row in dt.Rows)
            {
                int id = TextUtils.ToInt(row["ID"]);
                if (id <= 0) continue;

                ProjectPartlistPriceRequestModel requestModel = SQLHelper<ProjectPartlistPriceRequestModel>.FindByID(id);
                if (requestModel.QuoteEmployeeID != Global.EmployeeID && !isAdmin) continue;

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
                DateTime dateExpected = AddWeekdays(DateTime.Now, leadTime);
                //DateTime dateExpected = AddWeekdays(new DateTime(2024, 10, 22), leadTime);

                var myDict = new Dictionary<string, object>()
                {
                    {ProjectPartlistPriceRequestModel_Enum.CurrencyRate.ToString(), currencyRate},
                    {ProjectPartlistPriceRequestModel_Enum.TotalPrice.ToString(), totalPrice},
                    {ProjectPartlistPriceRequestModel_Enum.TotalPriceExchange.ToString(), totalPriceExchange},
                    {ProjectPartlistPriceRequestModel_Enum.TotaMoneyVAT.ToString(), totalMoneyVAT},
                    {ProjectPartlistPriceRequestModel_Enum.DateExpected.ToString(), dateExpected},
                    {ProjectPartlistPriceRequestModel_Enum.UpdatedDate.ToString(), DateTime.Now},
                };

                if (!Global.IsAdmin)
                {
                    myDict.Add(ProjectPartlistPriceRequestModel_Enum.QuoteEmployeeID.ToString(), Global.EmployeeID);
                    myDict.Add(ProjectPartlistPriceRequestModel_Enum.UpdatedBy.ToString(), Global.AppCodeName);
                }

                foreach (GridColumn col in columnEdits)
                {
                    //var value = TextUtils.ToString(row[col.FieldName]).Trim().ToLower();
                    var value = row[col.FieldName];
                    myDict.Add(col.FieldName, value);
                }

                SQLHelper<ProjectPartlistPriceRequestModel>.UpdateFieldsByID(myDict, id);
            }
        }

        static DateTime AddWeekdays(DateTime date, int days)
        {
            int count = 0;
            //date = date.AddDays(+1);
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


        void CheckPrice(bool isCheckPrice)
        {
            var tabSelected = xtraTabControl1.SelectedTabPage;

            if (tabSelected.Controls.Count <= 0) return;
            GridControl gridControl = (GridControl)tabSelected.Controls[0];
            GridView gridView = gridControl.MainView as GridView;

            string isCheckText = isCheckPrice ? "Check giá" : "Huỷ check giá";
            var selectedRows = gridView.GetSelectedRows();
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
                    int id = TextUtils.ToInt(gridView.GetRowCellValue(row, colID));
                    if (id <= 0) continue;

                    //if (isCheckPrice)
                    {
                        ProjectPartlistPriceRequestModel requestModel = SQLHelper<ProjectPartlistPriceRequestModel>.FindByID(id);
                        if (requestModel.QuoteEmployeeID != Global.EmployeeID && requestModel.QuoteEmployeeID > 0) continue;
                    }

                    listIDs.Add(id);
                }

                if (listIDs.Count <= 0) return;
                string idText = string.Join(",", listIDs);
                int isCheckPriceValue = isCheckPrice ? 1 : 0;

                var myDict = new Dictionary<string, object>()
                {
                    { ProjectPartlistPriceRequestModel_Enum.IsCheckPrice.ToString(),isCheckPriceValue},
                    { ProjectPartlistPriceRequestModel_Enum.UpdatedBy.ToString(),Global.LoginName},
                    { ProjectPartlistPriceRequestModel_Enum.QuoteEmployeeID.ToString(), isCheckPrice ? Global.EmployeeID:0},
                };

                var exp = new Expression("ID", idText, "IN");
                SQLHelper<ProjectPartlistPriceRequestModel>.UpdateFields(myDict, exp);

                LoadData(gridControl);
            }

        }

        void QuotePrice(int status)
        {
            var tabSelected = xtraTabControl1.SelectedTabPage;

            if (tabSelected.Controls.Count <= 0) return;
            GridControl gridControl = (GridControl)tabSelected.Controls[0];
            GridView gridView = gridControl.MainView as GridView;

            gridView.CloseEditor();

            string statusText = status == 0 ? "Hủy hoàn thành" : (status == 1 ? "Hủy báo giá" : (status == 2 ? "Báo giá" : (status == 3 ? "Hoàn thành" : "")));
            int[] rowSelecteds = gridView.GetSelectedRows();
            if (rowSelecteds.Length <= 0)
            {
                MessageBox.Show($"Vui lòng chọn sản phẩm muốn {statusText}!", "Thông báo");
                return;
            }


            //Check validate
            foreach (int row in rowSelecteds)
            {
                if (status == 1 || status == 3) continue;

                int id = TextUtils.ToInt(gridView.GetRowCellValue(row, colID));
                if (id <= 0) continue;

                ProjectPartlistPriceRequestModel requestModel = SQLHelper<ProjectPartlistPriceRequestModel>.FindByID(id);
                if (requestModel.QuoteEmployeeID != Global.EmployeeID && !isAdmin) continue;

                string productCode = TextUtils.ToString(gridView.GetRowCellValue(row, colProductCode));

                int currencyId = TextUtils.ToInt(gridView.GetRowCellValue(row, colCurrencyID));
                string currencyCode = TextUtils.ToString(gridView.GetRowCellDisplayText(row, colCurrencyID)).Trim();
                decimal currencyRate = TextUtils.ToInt(gridView.GetRowCellValue(row, colCurrencyRate));
                decimal unitPrice = TextUtils.ToDecimal(gridView.GetRowCellValue(row, colUnitPrice));
                decimal supplierSaleId = TextUtils.ToInt(gridView.GetRowCellValue(row, colSupplierSaleID));

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

            //Update dữ liệu những mã chọn báo góa
            DataTable dataSource = (DataTable)gridControl.DataSource;
            dataSource.AcceptChanges();
            DataTable data = dataSource.Clone();
            foreach (int row in rowSelecteds)
            {
                int id = TextUtils.ToInt(gridView.GetRowCellValue(row, colID));
                ProjectPartlistPriceRequestModel requestModel = SQLHelper<ProjectPartlistPriceRequestModel>.FindByID(id);
                if (requestModel.QuoteEmployeeID != Global.EmployeeID && !isAdmin) continue;

                if (row < 0) continue;
                DataRow dataRow = data.NewRow();

                dataRow = gridView.GetDataRow(row);
                data.ImportRow(dataRow);
            }
            UpdateData(data);

            status = status == 0 ? 1 : status;
            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn {statusText} danh sách sản phẩm đã chọn không?\nNhững sản phẩm NV mua không phải bạn sẽ tự động được bỏ qua!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                List<int> listUpdates = new List<int>();
                foreach (int row in rowSelecteds)
                {
                    int id = TextUtils.ToInt(gridView.GetRowCellValue(row, "ID"));
                    if (id <= 0) continue;

                    ProjectPartlistPriceRequestModel requestModel = SQLHelper<ProjectPartlistPriceRequestModel>.FindByID(id);
                    if (requestModel.QuoteEmployeeID != Global.EmployeeID && !isAdmin) continue;

                    listUpdates.Add(id);
                }

                if (listUpdates.Count <= 0) return;
                string idUpdateText = string.Join(",", listUpdates);

                var myDict = new Dictionary<string, object>()
                {
                    {ProjectPartlistPriceRequestModel_Enum.StatusRequest.ToString(),status},
                    {ProjectPartlistPriceRequestModel_Enum.UpdatedBy.ToString() ,Global.LoginName},
                    {ProjectPartlistPriceRequestModel_Enum.UpdatedDate.ToString(),DateTime.Now},
                };

                if (!Global.IsAdmin)
                {
                    myDict.Add(ProjectPartlistPriceRequestModel_Enum.QuoteEmployeeID.ToString(), Global.EmployeeID);
                }

                if (status == 1)
                {
                    myDict.Add(ProjectPartlistPriceRequestModel_Enum.DatePriceQuote.ToString(), null);
                }
                else if (status == 2)
                {
                    myDict.Add(ProjectPartlistPriceRequestModel_Enum.DatePriceQuote.ToString(), DateTime.Now);
                }

                var exp = new Expression("ID", idUpdateText, "IN");
                var result = SQLHelper<ProjectPartlistPriceRequestModel>.UpdateFields(myDict, exp);
                if (result.IsSuccess)
                {
                    SendMail(rowSelecteds, gridView);
                    LoadData(gridControl);
                }
                else
                {
                    MessageBox.Show(result.ErrorText, "Thông báo");
                }
            }
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
        decimal CalculatorTotalMoneyExchange(GridView gridView, int rowHandle)
        {
            decimal totalMoney = TextUtils.ToDecimal(gridView.GetRowCellValue(rowHandle, colTotalPrice));
            decimal currencyRate = TextUtils.ToDecimal(gridView.GetRowCellValue(rowHandle, colCurrencyRate));
            decimal totalMoneyExchange = totalMoney * currencyRate;
            return totalMoneyExchange;
        }

        void UpdateValue(GridView gridView, int row)
        {
            decimal quantity = TextUtils.ToDecimal(gridView.GetRowCellValue(row, colQuantity));
            decimal unitPrice = TextUtils.ToDecimal(gridView.GetRowCellValue(row, colUnitPrice));
            decimal unitImportPrice = TextUtils.ToDecimal(gridView.GetRowCellValue(row, colUnitImportPrice));
            decimal vat = TextUtils.ToDecimal(gridView.GetRowCellValue(row, colVAT));

            decimal totalPrice = quantity * unitPrice;

            //Tính thành tiền
            gridView.SetRowCellValue(row, colTotalPrice, totalPrice);
            gridView.SetRowCellValue(row, colTotalPriceExchange, CalculatorTotalMoneyExchange(gridView, row));

            //Tính đơn giá nhập khẩu
            decimal totalPriceImport = quantity * unitImportPrice;
            gridView.SetRowCellValue(row, colTotalImportPrice, totalPriceImport);

            //Tính thành tiền có VAT
            decimal totalMoneyVAT = totalPrice + ((totalPrice * vat) / 100);
            gridView.SetRowCellValue(row, colTotaMoneyVAT, totalMoneyVAT);

            //Tính ngày về dự kiến
            if (gridView.FocusedColumn.FieldName == colTotalDayLeadTime.FieldName)
            {
                int leadTime = TextUtils.ToInt(gridView.GetRowCellValue(row, colTotalDayLeadTime));
                gridView.SetRowCellValue(row, colDateExpected, AddWeekdays(DateTime.Now, leadTime));
                //gridView.SetRowCellValue(row, colDateExpected, AddWeekdays(new DateTime(2024,10,22), leadTime));
            }
        }

        void LoadViewToJobRequirement()
        {
            if (jobRequirementID > 0 || isVPP || _projectPartlistPriceRequestTypeID == 4)
            {
                foreach (DevExpress.XtraBars.BarItemLink itemLink in bar2.ItemLinks)
                {
                    if (itemLink.Item == btnAddRequest || itemLink.Item == btnDelete || itemLink.Item == btnEditRequest || itemLink.Item == btnRequestBuy || itemLink.Item == btnImportExcel) continue;

                    itemLink.Item.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }

            }
            else if (Global.DepartmentID == 4)
            {
                btnRequestBuy.Visibility = btnImportExcel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                stackPanel1.Visible = false;
            }

            var tabSelected = xtraTabControl1.SelectedTabPage;
            if (tabSelected.Controls.Count > 0)
            {
                GridControl gridControl = (GridControl)tabSelected.Controls[0];
                GridView gridview = gridControl.MainView as GridView;
                int rowHandle = gridview.LocateByValue(colJobRequirementID.FieldName, jobRequirementID);
                if (rowHandle != GridControl.InvalidRowHandle)
                {
                    gridview.FocusedRowHandle = rowHandle;  // Focus vào hàng
                    gridview.MakeRowVisible(rowHandle);     // Cuộn đến hàng nếu cần
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            try
            {
                if (e.Page == null) return;
                if (e.Page.Controls.Count <= 0) return;
                var prevPagetabName = e.PrevPage.Text;
                var tabName = e.Page.Text;
                if (e.PrevPage.Controls.Count > 0)
                {
                    GetDataChange((GridControl)e.PrevPage.Controls[0]);
                }
                LoadData((GridControl)e.Page.Controls[0]);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo");
            }
        }

        private void btnSaveData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            var tabSelected = xtraTabControl1.SelectedTabPage;
            if (tabSelected.Controls.Count > 0)
            {
                GetDataChange((GridControl)tabSelected.Controls[0]);
            }
        }

        private void btnCheckPrice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CheckPrice(true);
        }

        private void btnUnCheckPrice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CheckPrice(false);
        }

        private void btnQuotePrice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            QuotePrice(2);

        }

        private void btnUnQuotePrice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            QuotePrice(1);
        }

        private void btnCompleteQuotePrice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            QuotePrice(3);
        }

        private void btnUnCompleteQuotePrice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            QuotePrice(0);
        }

        private void btnExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            f.FileName = $"YeuCauBaoGia_{dtpDateStart.Value.ToString("ddMMyy")}_{dtpDateEnd.Value.ToString("ddMMyy")}.xlsx";
            if (f.ShowDialog() == DialogResult.OK)
            {
                //string filepath = Path.Combine(f.SelectedPath, $"BaoCaoCongTac_T{txtMonth.Text}_{txtYear.Value}.xlsx");
                string filepath = f.FileName;
                //string filepath = @"C:\Users\Admin\Desktop\Bảng công Công ty RTC - APR - MVI - YONKO FINAL Tháng 8.2023 FINAL.xlsx";

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();

                try
                {
                    using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
                    {
                        CompositeLink compositeLink = new CompositeLink(printingSystem);
                        foreach (XtraTabPage item in xtraTabControl1.TabPages)
                        {
                            if (item.Controls.Count <= 0) continue;
                            GridControl gridControl = (GridControl)item.Controls[0];
                            LoadData(gridControl);

                            PrintableComponentLink printableComponentLink = new PrintableComponentLink(printingSystem);
                            printableComponentLink.Component = gridControl;

                            compositeLink.Links.Add(printableComponentLink);
                        }
                        compositeLink.PrintingSystem.XlSheetCreated += PrintingSystem_XlSheetCreated;

                        compositeLink.CreatePageForEachLink();
                        optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;

                        compositeLink.PrintingSystem.SaveDocument(filepath);
                        compositeLink.ExportToXlsx(filepath, optionsEx);
                        Process.Start(filepath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void PrintingSystem_XlSheetCreated(object sender, XlSheetCreatedEventArgs e)
        {
            //throw new NotImplementedException();

            try
            {
                e.SheetName = xtraTabControl1.TabPages[e.Index].Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo");
            }
        }

        private void btnAddSupplierSale_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmSupplierSaleDetail frm = new frmSupplierSaleDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadSupplierSale();
            }
        }

        private void btnDownloadFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var tabSelected = xtraTabControl1.SelectedTabPage;

            if (tabSelected.Controls.Count <= 0) return;
            GridControl gridControl = (GridControl)tabSelected.Controls[0];
            GridView gridView = gridControl.MainView as GridView;

            int[] rowSelecteds = gridView.GetSelectedRows();
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
                        int projectId = TextUtils.ToInt(gridView.GetRowCellValue(row, colProjectID));

                        ProjectModel project = SQLHelper<ProjectModel>.FindByID(projectId);
                        if (project == null) continue;
                        if (!project.CreatedDate.HasValue) continue;

                        int projectPartlistId = TextUtils.ToInt(gridView.GetRowCellValue(row, colProjectPartListID));

                        ProjectSolutionModel solution = SQLHelper<ProjectSolutionModel>.ProcedureToList("spGetProjectSolutionByProjectPartListID",
                                                            new string[] { "@ProjectPartListID" }, new object[] { projectPartlistId }).FirstOrDefault();
                        if (solution == null) continue;
                        if (string.IsNullOrEmpty(solution.CodeSolution)) continue;
                        string pathPattern = $@"{project.CreatedDate.Value.Year}/{project.ProjectCode.Trim()}/THIETKE.Co/{solution.CodeSolution.Trim()}/2D/GC/DH";

                        productCode = TextUtils.ToString(gridView.GetRowCellValue(row, colProductCode));
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

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var tabSelected = xtraTabControl1.SelectedTabPage;

            if (tabSelected.Controls.Count <= 0) return;
            GridControl gridControl = (GridControl)tabSelected.Controls[0];
            GridView gridView = gridControl.MainView as GridView;




            int projectTypeID = TextUtils.ToInt(gridControl.Tag);
            //============================== lee min khooi update 21/11/2024 =========================================
            //if (!gridControl.Name.Contains("IsCommercial") && !gridControl.Name.Contains("IsJobRequirement") && !Global.IsAdmin)
            if (projectTypeID >= 0 && !Global.IsAdmin)
            {
                MessageBox.Show("Chỉ có thể xóa Yêu cầu báo giá thương mại hoặc Yêu cầu công việc", "Thông báo");
                return;
            }


            int[] rowSelected = gridView.GetSelectedRows();
            if (rowSelected.Length <= 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm muốn xoá!", "Thông báo");
                return;
            }

            foreach (int row in rowSelected)
            {
                int statusRequest = TextUtils.ToInt(grvData.GetRowCellValue(row, colStatusRequest));
                bool checkPrice = TextUtils.ToBoolean(grvData.GetRowCellValue(row, colIsCheckPrice));
                string statusRequestText = TextUtils.ToString(grvData.GetRowCellValue(row, colStatusRequestText));

                if (statusRequest > 0 && statusRequest != 1)
                {
                    MessageBox.Show($"Yêu cầu đã được {statusRequestText}. Không thể xóa!");
                }

                if (checkPrice)
                {
                    MessageBox.Show($"Yêu cầu đã được {statusRequestText}. Không thể xóa!");
                }
            }

            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn xoá danh sách đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                int[] statusRequests = new int[] { 2, 3 };
                List<int> idDeletes = new List<int>();
                foreach (int row in rowSelected)
                {
                    int id = TextUtils.ToInt(gridView.GetRowCellValue(row, "ID"));
                    if (id <= 0) continue;


                    ProjectPartlistPriceRequestModel priceRequest = SQLHelper<ProjectPartlistPriceRequestModel>.FindByID(id);
                    if (statusRequests.Contains(TextUtils.ToInt(priceRequest.StatusRequest)) || priceRequest.IsCheckPrice == true) continue;

                    idDeletes.Add(id);
                }

                if (idDeletes.Count <= 0) return;
                string idDeleteText = string.Join(",", idDeletes);
                //string sql = $"UPDATE ProjectPartlistPriceRequest SET IsDeleted = 1 WHERE ID IN ({idDelete})";
                //TextUtils.ExcuteSQL(sql);

                var myDict = new Dictionary<string, object>()
                {
                    { ProjectPartlistPriceRequestModel_Enum.IsDeleted.ToString(),1},
                };

                var exp = new Expression("ID", idDeleteText, "IN");
                SQLHelper<ProjectPartlistPriceRequestModel>.UpdateFields(myDict, exp);

                foreach (int row in rowSelected)
                {
                    gridView.DeleteRow(row);
                }
                //LoadData();

                var dataSoure = (DataTable)gridControl.DataSource;
                if (dataSoure != null) dataSoure.AcceptChanges();
            }
        }

        bool isRecallCellValueChanged = false;
        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gridView = (GridView)sender;
            if (gridView == null) return;

            //int id = TextUtils.ToInt(gridView.GetFocusedRowCellValue(colID));
            //MessageBox.Show(gridView.Name, id.ToString());


            if (gridView.FocusedColumn == colCurrencyID) return;
            if (isRecallCellValueChanged == true) return;
            try
            {

                using (WaitDialogForm fWait = new WaitDialogForm())
                {
                    isRecallCellValueChanged = true;
                    gridView.CloseEditor();

                    int[] selectedRows = gridView.GetSelectedRows();

                    if (selectedRows.Length > 0)
                    {
                        if (e.Value == null) return;
                        foreach (int row in selectedRows)
                        {
                            if (e.Column.FieldName != colTotalPriceExchange.FieldName && e.Column.FieldName != colTotalPrice.FieldName)
                            {
                                gridView.SetRowCellValue(row, gridView.Columns[e.Column.FieldName], e.Value);
                            }

                            if (e.Column.FieldName != colUnitPrice.FieldName
                                && e.Column.FieldName != colUnitImportPrice.FieldName
                                && e.Column.FieldName != colVAT.FieldName
                                && e.Column.FieldName != colTotalDayLeadTime.FieldName
                                && e.Column.FieldName != colDeadline.FieldName) continue;

                            UpdateValue(gridView, row);

                        }
                    }
                    else
                    {
                        UpdateValue(gridView, gridView.FocusedRowHandle);
                    }
                }
            }
            finally
            {
                isRecallCellValueChanged = false;
            }
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            GridView gridView = (GridView)sender;
            if (gridView == null) return;

            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(gridView.GetFocusedRowCellValue(gridView.FocusedColumn));
                if (string.IsNullOrWhiteSpace(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void grvData_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            GridView gridView = (GridView)sender;
            bool isDeleted = TextUtils.ToBoolean(gridView.GetRowCellValue(e.RowHandle, colIsDeleted.FieldName));
            bool isJobRequirement = TextUtils.ToBoolean(gridView.GetRowCellValue(e.RowHandle, colIsJobRequirement.FieldName));
            bool isRequestBuy = TextUtils.ToBoolean(gridView.GetRowCellValue(e.RowHandle, colIsRequestBuy.FieldName));

            if (isDeleted)
            {
                e.Appearance.BackColor = Color.Red;
                e.Appearance.ForeColor = Color.White;
            }

            if (/*isJobRequirement && */isRequestBuy && Global.EmployeeID != 4)
            {
                e.Appearance.BackColor = Color.Green;
                e.Appearance.ForeColor = Color.White;
            }


            //var view = sender as GridView;
            if (gridView.FocusedRowHandle == e.RowHandle)
            {
                e.Appearance.BackColor = Color.LightYellow;
                e.Appearance.ForeColor = Color.Black;
                //e.HighPriority = true;
            }
        }

        private void cboCurrency_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit lookUpEdit = (SearchLookUpEdit)sender;
            CurrencyModel currency = (CurrencyModel)lookUpEdit.GetSelectedDataRow();
            if (currency == null) return;

            GridControl gridControl = (GridControl)lookUpEdit.Parent;
            GridView gridView = gridControl.MainView as GridView;
            if (gridView == null) return;
            int[] rowSelecteds = gridView.GetSelectedRows();
            bool isExpried = false;

            if (rowSelecteds.Length <= 0)
            {
                int row = gridView.FocusedRowHandle;
                gridView.SetRowCellValue(row, colCurrencyID, currency.ID);
                gridView.SetRowCellValue(row, colCurrencyRate, currency.CurrencyRate);
                if (isExpried) gridView.SetRowCellValue(row, colCurrencyRate, 0);
                gridView.SetRowCellValue(row, colTotalPriceExchange, CalculatorTotalMoneyExchange(gridView, row));
            }
            else
            {
                using (WaitDialogForm fWait = new WaitDialogForm())
                {
                    foreach (int row in rowSelecteds)
                    {
                        gridView.SetRowCellValue(row, colCurrencyID, currency.ID);
                        gridView.SetRowCellValue(row, colCurrencyRate, currency.CurrencyRate);
                        if (isExpried) gridView.SetRowCellValue(row, colCurrencyRate, 0);
                        gridView.SetRowCellValue(row, colTotalPriceExchange, CalculatorTotalMoneyExchange(gridView, row));
                    }
                }
            }
        }

        private void txtUnitPrice_EditValueChanged(object sender, EventArgs e)
        {
            //TextEdit textEdit = (TextEdit)sender;
            //GridControl gridControl = (GridControl)textEdit.Parent;
            //GridView gridView = gridControl.MainView as GridView;
            //if (gridView == null) return;
            //int[] rowSelecteds = gridView.GetSelectedRows();


            //decimal quantity = TextUtils.ToDecimal(gridView.GetFocusedRowCellValue(colQuantity));
            //decimal unitPrice = TextUtils.ToDecimal(textEdit.EditValue);
            //decimal totalPrice = quantity * unitPrice;

            //if (rowSelecteds.Length <= 0)
            //{
            //    int row = gridView.FocusedRowHandle;
            //    gridView.SetRowCellValue(row, colUnitPrice, unitPrice);
            //    gridView.SetRowCellValue(row, colTotalPrice, totalPrice);
            //    gridView.SetRowCellValue(row, colTotalPriceExchange, CalculatorTotalMoneyExchange(gridView, row));
            //}
            //else
            //{
            //    using (WaitDialogForm fWait = new WaitDialogForm())
            //    {
            //        foreach (int row in rowSelecteds)
            //        {
            //            gridView.SetRowCellValue(row, colUnitPrice, unitPrice);
            //            gridView.SetRowCellValue(row, colTotalPrice, totalPrice);
            //            gridView.SetRowCellValue(row, colTotalPriceExchange, CalculatorTotalMoneyExchange(gridView, row));
            //        }
            //    }
            //}
        }

        Dictionary<int, int> projectPartlistPriceRequestType = new Dictionary<int, int>()
        {
            { 0, 5 },
            { -1, 2 },
            { -2, 3 },
            { -3, 4 }
        };

        private void btnAddRequest_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var tabSelected = xtraTabControl1.SelectedTabPage;
            if (tabSelected.Controls.Count <= 0) return;
            GridControl gridControl = (GridControl)tabSelected.Controls[0];
            GridView gridView = gridControl.MainView as GridView;

            int projectTypeID = TextUtils.ToInt(gridControl.Tag);
            int projectPartlistPriceRequestTypeID = projectTypeID > 0 ? 1 : projectPartlistPriceRequestType.GetValueOrDefault(projectTypeID, 0);

            // Kiểm tra instance hiện có
            frmProjectPartlistPriceRequestDetailNew existingFrm = Application.OpenForms.OfType<frmProjectPartlistPriceRequestDetailNew>().FirstOrDefault();
            if (existingFrm != null)
            {
                existingFrm.Activate();
                existingFrm.BringToFront();
                return;
            }

            frmProjectPartlistPriceRequestDetailNew frm = new frmProjectPartlistPriceRequestDetailNew(jobRequirementID, projectPartlistPriceRequestTypeID);
            frm.isVPP = isVPP;

            //ndnhat update 21/10/2025
            frm.SaveEvent += () =>
            {
                LoadData();
            };
            frm.Show();

            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    LoadData();
            //}
        }


        //================== lee min khooi update 21/11/2024 ==========================
        private void btnEditRequest_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var tabSelected = xtraTabControl1.SelectedTabPage;
            if (tabSelected.Controls.Count <= 0) return;
            GridControl gridControl = (GridControl)tabSelected.Controls[0];
            GridView gridView = gridControl.MainView as GridView;

            int projectTypeID = TextUtils.ToInt(gridControl.Tag);
            int projectPartlistPriceRequestTypeID = projectTypeID > 0 ? 1 : projectPartlistPriceRequestType.GetValueOrDefault(projectTypeID, 0);

            //if (!gridControl.Name.Contains("IsCommercial") && !gridControl.Name.Contains("IsJobRequirement") && !Global.IsAdmin)
            if (projectTypeID >= 0 && !Global.IsAdmin)
            {
                MessageBox.Show("Chỉ có thể sửa Yêu cầu báo giá thương mại hoặc Yêu cầu công việc", "Thông báo");
                return;
            }


            int[] rowsSelected = gridView.GetSelectedRows();
            if (rowsSelected.Length <= 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất 1 yêu cầu báo giá!", "Thông báo");
                return;
            }

            // Kiểm tra instance hiện có
            frmProjectPartlistPriceRequestDetailNew existingFrm = Application.OpenForms.OfType<frmProjectPartlistPriceRequestDetailNew>().FirstOrDefault();
            if (existingFrm != null)
            {
                existingFrm.Activate();
                existingFrm.BringToFront();
                return;
            }

            frmProjectPartlistPriceRequestDetailNew frm = new frmProjectPartlistPriceRequestDetailNew(jobRequirementID, projectPartlistPriceRequestTypeID);

            frm.isVPP = isVPP;
            int empID = 0;

            DataTable dt = (DataTable)gridControl.DataSource;
            if (dt == null) return;

            frm.dt = dt.Clone();
            //frm.isJobRequirement = jobRequirementID > 0;
            //foreach (int rowIndex in rowsSelected) //lee min khooi
            //{

            //}

            //lt.Anh 
            for (int i = 0; i < rowsSelected.Length; i++)
            {
                int rowIndex = rowsSelected[i];
                if (rowIndex < 0) continue;
                if (empID != 0)
                {
                    int empID1 = TextUtils.ToInt(gridView.GetRowCellValue(rowIndex, colEmployeeID));
                    if (empID != empID1)
                    {
                        MessageBox.Show("Vui lòng chọn yêu cầu báo giá có cùng Người yêu cầu!", "Thông báo");
                        return;
                    }
                }
                empID = TextUtils.ToInt(gridView.GetRowCellValue(rowIndex, colEmployeeID));
                int id = TextUtils.ToInt(gridView.GetRowCellValue(rowIndex, colID));
                int statusRequest = TextUtils.ToInt(gridView.GetRowCellValue(rowIndex, colStatusRequest));
                bool isCheckPrice = TextUtils.ToBoolean(gridView.GetRowCellValue(rowIndex, colIsCheckPrice));
                if (statusRequest == 2 || statusRequest == 3 || isCheckPrice) continue;
                DataRow row = gridView.GetDataRow(rowIndex);
                if (row == null) continue;
                row["STT"] = i + 1;

                frm.dt.ImportRow(row);
            }

            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    LoadData();
            //}

            //ndnhat update 21/10/2025


            frm.SaveEvent += () =>
            {
                LoadData();
            };
            frm.Show();

        }

        private void btnExportExcelAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExportExcel(false);
        }

        private void btnExportExcelChoise_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExportExcel(true);
        }

        void ExportExcel(bool choise)
        {
            try
            {
                var tabSelected = xtraTabControl1.SelectedTabPage;

                if (tabSelected.Controls.Count <= 0) return;
                GridControl gridControl = (GridControl)tabSelected.Controls[0];

                DataTable dtNew = ((DataTable)gridControl.DataSource).Clone();
                DataTable dtOld = ((DataTable)gridControl.DataSource);
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
                        if (rowIndex <= 0) continue;

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
                    if (choise) gridControl.DataSource = dtNew;
                    printableComponentLink1.Component = gridControl;


                    CompositeLink compositeLink = new CompositeLink(printingSystem);
                    compositeLink.Links.Add(printableComponentLink1);
                    compositeLink.CreatePageForEachLink();
                    optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;
                    compositeLink.PrintingSystem.SaveDocument(filepath);
                    compositeLink.ExportToXlsx(filepath, optionsEx);

                    if (choise)
                    {
                        gridControl.DataSource = dtOld;
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

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();

            var a = this.DeviceDpi;

            float dpiX = g.DpiX;
            float dpiY = g.DpiY;

            // Tính tỷ lệ DPI (DPI chuẩn là 96)
            float scaleFactorX = (dpiX / 96) * 100;
            float scaleFactorY = (dpiY / 96) * 100;

            // In ra tỷ lệ DPI dưới dạng phần trăm
            //Console.WriteLine($"DPI Scale (X): {scaleFactorX * 100}%");
            //Console.WriteLine($"DPI Scale (Y): {scaleFactorY * 100}%");
        }

        private void btnExportAllExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnExportExcel_ItemClick(null, null);
        }

        private void btnExportExcelFocus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExportExcelChoise(false);
        }


        void ExportExcelChoise(bool choise)
        {
            var tabSelected = xtraTabControl1.SelectedTabPage;
            if (tabSelected.Controls.Count <= 0) return;

            GridControl grdExport = (GridControl)tabSelected.Controls[0];
            GridView grvExport = grdExport.MainView as GridView;
            if (grdExport == null || grvExport == null)
            {
                //MessageBox.Show("Không tìm thấy GridControl hoặc GridView trong tab hiện tại!", "Thông báo");
                return;
            }

            DataTable dtNew = ((DataTable)grdExport.DataSource).Clone();
            DataTable dtOld = ((DataTable)grdExport.DataSource);
            int[] listFocus = { };
            if (choise)
            {
                if (grvExport.RowCount <= 0) return;
                if (grvExport.SelectedRowsCount <= 0)
                {
                    MessageBox.Show($"Vui lòng chọn sản phẩm cần xuất excel!", "Thông báo");
                    return;
                }
                listFocus = grvExport.GetSelectedRows();
                foreach (int rowIndex in listFocus)
                {
                    if (rowIndex < 0) continue;
                    DataRow rowObj = grvExport.GetDataRow(rowIndex);
                    if (rowObj == null) continue;
                    //DataRow row = rowView.Row;
                    dtNew.ImportRow(rowObj);
                }
            }

            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            string name = !choise ? "_All" : "";
            f.FileName = $"YeuCauBaoGia_{tabSelected.Text}{name}_{dtpDateStart.Value.ToString("ddMMyy")}_{dtpDateEnd.Value.ToString("ddMMyy")}.xlsx";
            if (f.ShowDialog() == DialogResult.OK)
            {
                //string filepath = Path.Combine(f.SelectedPath, $"BaoCaoCongTac_T{txtMonth.Text}_{txtYear.Value}.xlsx");
                string filepath = f.FileName;
                //string filepath = @"C:\Users\Admin\Desktop\Bảng công Công ty RTC - APR - MVI - YONKO FINAL Tháng 8.2023 FINAL.xlsx";

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();

                try
                {
                    using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
                    {
                        CompositeLink compositeLink = new CompositeLink(printingSystem);

                        //if (item.Controls.Count <= 0) continue;

                        GridControl gridControl = (GridControl)tabSelected.Controls[0];
                        //GridControl gridControl = (GridControl)item.Controls[0];
                        if (gridControl == null) return;
                        LoadData(gridControl);

                        PrintableComponentLink printableComponentLink = new PrintableComponentLink(printingSystem);

                        if (choise) grdExport.DataSource = dtNew;

                        printableComponentLink.Component = grdExport;

                        compositeLink.Links.Add(printableComponentLink);

                        foreach (XtraTabPage item in xtraTabControl1.TabPages)
                        {

                        }
                        compositeLink.PrintingSystem.XlSheetCreated += PrintingSystem_XlSheetCreated;

                        compositeLink.CreatePageForEachLink();
                        optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;

                        compositeLink.PrintingSystem.SaveDocument(filepath);
                        compositeLink.ExportToXlsx(filepath, optionsEx);

                        if (choise)
                        {
                            grdExport.DataSource = dtOld;
                            foreach (int rowIndex in listFocus) grvExport.SelectRow(rowIndex);
                            grvExport.MakeRowVisible(listFocus.Min());
                        }

                        Process.Start(filepath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnRequestBuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var tabSelected = xtraTabControl1.SelectedTabPage;

                if (tabSelected.Controls.Count <= 0) return;
                GridControl gridControl = (GridControl)tabSelected.Controls[0];
                GridView gridView = gridControl.MainView as GridView;


                int[] rowSelected = gridView.GetSelectedRows();
                if (rowSelected.Length <= 0)
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm muốn yêu cầu mua!", "Thông báo");
                    return;
                }

                //Kiểm tra nhập Deadline - start
                DateTime deadline = dtpDeadline.Value;
                DateTime dateNow = DateTime.Now;

                double timeSpan = (deadline.Date - dateNow.Date).TotalDays + 1;
                if (dateNow.Hour < 15)
                {
                    if (timeSpan < 2)
                    {
                        MessageBox.Show("Deadline tối thiếu là 2 ngày từ ngày hiện tại!", "Thông báo");
                        return;
                    }
                }
                else if (timeSpan < 3)
                {
                    MessageBox.Show("Yêu cầu từ sau 15h nên ngày Deadline sẽ bắt đầu tính từ ngày hôm sau và tối thiểu là 2 ngày!", "Thông báo");
                    return;
                }

                if (deadline.DayOfWeek == DayOfWeek.Sunday || deadline.DayOfWeek == DayOfWeek.Saturday)
                {
                    MessageBox.Show("Deadline phải là ngày làm việc (T2 - T6)!", "Thông báo");
                    return;
                }

                int coutWeekday = 0;
                for (int i = 0; i < timeSpan; i++)
                {
                    DateTime dateValue = dateNow.Date.AddDays(i);
                    if (dateValue.DayOfWeek == DayOfWeek.Sunday || dateValue.DayOfWeek == DayOfWeek.Saturday)
                    {
                        coutWeekday++;
                    }
                }

                if (coutWeekday > 0)
                {
                    DialogResult dialog = MessageBox.Show($"Deadline sẽ không tính Thứ 7 và Chủ nhật.\nBạn có chắc muốn chọn Deadline là ngày [{deadline.ToString("dd/MM/yyyy")}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialog != DialogResult.Yes) return;
                }
                //Kiểm tra nhập Deadline - end


                //DialogResult 
                foreach (int row in rowSelected)
                {
                    if (row < 0) continue;


                    //string productCode = TextUtils.ToString(gridView.GetRowCellValue(row, colProductCode.FieldName));
                    //var exp1 = new Expression(ProjectPartlistPurchaseRequestModel_Enum.JobRequirementID, jobRequirementID);
                    //var exp2 = new Expression(ProjectPartlistPurchaseRequestModel_Enum.IsDeleted, 1, "<>");
                    //var exp3 = new Expression(ProjectPartlistPurchaseRequestModel_Enum.ProductCode, productCode);
                    ////ProjectPartlistPurchaseRequestModel request = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByAttribute("ProjectPartListID", item.ID).FirstOrDefault();
                    //List<ProjectPartlistPurchaseRequestModel> requests = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByExpression(exp1.And(exp2).And(exp3));

                    //ProjectPartlistPurchaseRequestModel request = requests.FirstOrDefault() ?? new ProjectPartlistPurchaseRequestModel();
                    ProjectPartlistPurchaseRequestModel request = new ProjectPartlistPurchaseRequestModel();
                    //request = request == null ? new ProjectPartlistPurchaseRequestModel() : request;

                    //if (request.EmployeeApproveID > 0) continue;
                    request.JobRequirementID = jobRequirementID;
                    //if (isVPP) request.JobRequirementID = 999999;

                    request.EmployeeID = Global.EmployeeID;
                    request.ProductCode = TextUtils.ToString(gridView.GetRowCellValue(row, colProductCode.FieldName));
                    request.ProductName = TextUtils.ToString(gridView.GetRowCellValue(row, colProductName.FieldName));
                    request.StatusRequest = 1;
                    request.DateRequest = DateTime.Now;
                    request.DateReturnExpected = dtpDeadline.Value;
                    request.Quantity = TextUtils.ToInt(gridView.GetRowCellValue(row, colQuantity.FieldName));
                    request.NoteHR = TextUtils.ToString(gridView.GetRowCellValue(row, colNoteHR.FieldName));
                    //request.UnitMoney = item.UnitMoney;

                    //request.Quantity = item.QtyFull;
                    //request.UnitPrice = item.PriceOrder;
                    //request.TotalPrice = item.TotalPriceOrder;

                    int productGroupID = isVPP ? 80 : (jobRequirementID > 0 ? 77 : 0); //89: Kho vật tư tiêu hao HR; 77: Kho Công cụ dụng cụ\Tài sản
                    if (_projectPartlistPriceRequestTypeID == 4) productGroupID = 0;
                    var exp4 = new Expression("ProductCode", request.ProductCode);
                    var exp5 = new Expression("ProductGroupID", productGroupID);
                    var exp6 = new Expression("IsDeleted", 0);
                    ProductSaleModel productSale = SQLHelper<ProductSaleModel>.FindByExpression(exp4.And(exp5).And(exp6)).FirstOrDefault() ?? new ProductSaleModel();
                    request.ProductSaleID = productSale.ID;
                    request.ProductGroupID = productSale.ProductGroupID;

                    string unitName = TextUtils.ToString(gridView.GetRowCellValue(row, colUnitRequest.FieldName));
                    UnitCountModel unit = SQLHelper<UnitCountModel>.FindByAttribute("UnitName", unitName.Trim()).FirstOrDefault();
                    if (unit != null) request.UnitCountID = unit.ID;

                    request.UnitName = unitName;
                    request.Maker = TextUtils.ToString(gridView.GetRowCellValue(row, colManufacturer.FieldName));
                    request.ProjectPartlistPurchaseRequestTypeID = 0;
                    if (_projectPartlistPriceRequestTypeID == 4) request.ProjectPartlistPurchaseRequestTypeID = 7; //Nếu là hàng MKT
                    if (_projectPartlistPriceRequestTypeID == 3) request.ProjectPartlistPurchaseRequestTypeID = 6;//Nếu là hàng HR

                    if (request.ID <= 0 || request.ProjectPartlistPurchaseRequestTypeID == 7)
                    {
                        request.ID = SQLHelper<ProjectPartlistPurchaseRequestModel>.Insert(request).ID;

                        bool isRequestBuy = request.ID > 0;
                        int id = TextUtils.ToInt(gridView.GetRowCellValue(row, colID.FieldName));

                        //Update trạng thái đã yc mua
                        var myDict = new Dictionary<string, object>()
                        {
                            { ProjectPartlistPriceRequestModel_Enum.IsRequestBuy.ToString(),isRequestBuy},
                            { ProjectPartlistPriceRequestModel_Enum.JobRequirementID.ToString(),jobRequirementID},
                            //{ ProjectPartlistPriceRequestModel_Enum.UpdatedBy.ToString(),Global.AppUserName},
                            //{ ProjectPartlistPriceRequestModel_Enum.UpdatedDate.ToString(),DateTime.Now},
                        };

                        SQLHelper<ProjectPartlistPriceRequestModel>.UpdateFieldsByID(myDict, id);
                    }
                    else
                    {
                        if (request.StatusRequest > 2) continue;

                        //if (requests.Count > 0)
                        //{

                        //}
                        SQLHelper<ProjectPartlistPurchaseRequestModel>.Update(request);
                    }

                }


                MessageBox.Show("Yêu cầu mua thành công!", "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
                //throw;
            }
        }

        private void btnImportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmProjectPartlistPriceRequestImport frm = new frmProjectPartlistPriceRequestImport(jobRequirementID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnUnPrice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdatePriceRequestStatus(5, "Từ chối báo giá", true);
        }

        #region Nhật update từ chối báo giá
        private void UpdatePriceRequestStatus(int newStatus, string actionName, bool requireReason)
        {
            var tabSelected = xtraTabControl1.SelectedTabPage;
            if (tabSelected.Controls.Count <= 0) return;

            GridControl gridControl = (GridControl)tabSelected.Controls[0];
            GridView gridView = (GridView)gridControl.MainView;

            int[] selectedRows = gridView.GetSelectedRows();
            if (selectedRows.Length <= 0)
            {
                MessageBox.Show($"Vui lòng chọn sản phẩm muốn {actionName}!", "Thông báo");
                return;
            }

            List<int> listIDs = new List<int>();
            List<DataRow> listDataMail = new List<DataRow>();

            foreach (int row in selectedRows)
            {
                int id = TextUtils.ToInt(gridView.GetRowCellValue(row, colID));
                string productCode = TextUtils.ToString(gridView.GetRowCellValue(row, colProductCode)).Trim();
                int statusRequest = TextUtils.ToInt(gridView.GetRowCellValue(row, colStatusRequest));

                if (newStatus == 3) // Từ chối
                {
                    if (statusRequest == 3)
                    {
                        MessageBox.Show($"Sản phẩm [{productCode}] đã bị Từ chối báo giá, không thể Từ chối lại!", "Thông báo");
                        continue;
                    }
                    if (statusRequest == 2)
                    {
                        MessageBox.Show($"Sản phẩm [{productCode}] đã ở trạng thái [Đã báo giá], không thể Từ chối!", "Thông báo");
                        listIDs.Clear();
                        break;
                    }
                }
                else if (newStatus == 1) // Hủy từ chối
                {
                    if (statusRequest != 3)
                    {
                        MessageBox.Show($"Sản phẩm [{productCode}] chưa bị Từ chối báo giá, không thể Hủy!", "Thông báo");
                        continue;
                    }
                }

                if (id > 0)
                {
                    listIDs.Add(id);
                    DataRow dr = gridView.GetDataRow(row);
                    if (dr != null) listDataMail.Add(dr);
                }
            }

            if (listIDs.Count <= 0) return;

            DialogResult dialog = MessageBox.Show(
                $"Bạn có chắc muốn {actionName} danh sách sản phẩm đã chọn không?",
                "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialog != DialogResult.Yes) return;

            string reason = "";

            // Nếu cần nhập lý do (chỉ khi Từ chối)
            if (requireReason)
            {
                frmReasonUnPrice frm = new frmReasonUnPrice();
                if (frm.ShowDialog() != DialogResult.OK) return;
                reason = frm.ReasonUnPrice;
            }

            string idText = string.Join(",", listIDs);
            var expression = new Expression("ID", idText, "IN");

            var myDict = new Dictionary<string, object>()
      {
          { ProjectPartlistPriceRequestModel_Enum.StatusRequest.ToString(), newStatus },
          { ProjectPartlistPriceRequestModel_Enum.UpdatedBy.ToString(), Global.LoginName },
          { ProjectPartlistPriceRequestModel_Enum.UpdatedDate.ToString(), DateTime.Now },
          { ProjectPartlistPriceRequestModel_Enum.EmployeeIDUnPrice.ToString(), Global.EmployeeID },
          { ProjectPartlistPriceRequestModel_Enum.ReasonUnPrice.ToString(), reason }
      };

            SQLHelper<ProjectPartlistPriceRequestModel>.UpdateFields(myDict, expression);

            // Sau khi cập nhật DB thì gửi mail
            if (newStatus == 3) // chỉ gửi mail khi Từ chối
            {
                SendMail(listDataMail, reason);
            }

            LoadData();
        }

        private void SendMail(List<DataRow> listDataMail, string reason)
        {
            if (listDataMail == null || listDataMail.Count == 0) return;

            int employeeId = Global.EmployeeID;
            EmployeeModel sender = SQLHelper<EmployeeModel>.FindByID(employeeId); // Người gửi = Global.EmployeeID

            // Nhóm theo FullName (người yêu cầu)
            var groups = listDataMail
                .GroupBy(r => TextUtils.ToString(r["FullName"]))
                .ToList();

            foreach (var group in groups)
            {
                // Người nhận = người yêu cầu
                string fullNameRequest = group.Key;
                EmployeeModel receiver = SQLHelper<EmployeeModel>
                    .FindByAttribute("FullName", fullNameRequest)
                    .FirstOrDefault();

                // Tạo bảng HTML cho từng người
                StringBuilder tableBuilder = new StringBuilder();
                tableBuilder.Append(@"
                  <table border='1' cellspacing='0' cellpadding='5' style='border-collapse:collapse; font-family:Arial; font-size:13px; text-align:center;'>
                      <thead style='background-color:#f2f2f2; font-weight:bold;'>
                          <tr>
                              <th>Mã dự án</th>
                              <th>Mã SP</th>
                              <th>Tên SP</th>
                              <th>Hãng</th>
                              <th>ĐVị</th>
                              <th>Số lượng</th>
                              <th>Ngày YC</th>
                              <th>Deadline</th>
                          </tr>
                      </thead>
                      <tbody>");

                foreach (DataRow row in group)
                {
                    string projectCode = TextUtils.ToString(row["ProjectCode"]);
                    string productCode = TextUtils.ToString(row["ProductCode"]);
                    string productName = TextUtils.ToString(row["ProductName"]);
                    string manufacturer = TextUtils.ToString(row["Manufacturer"]);
                    string unitCount = TextUtils.ToString(row["UnitCount"]);
                    int quantity = TextUtils.ToInt(row["Quantity"]);
                    DateTime dateRequest = TextUtils.ToDate5(row["DateRequest"]);
                    DateTime deadline = TextUtils.ToDate5(row["Deadline"]);

                    tableBuilder.Append($@"
              <tr>
                  <td>{projectCode}</td>
                  <td>{productCode}</td>
                  <td>{productName}</td>
                  <td>{manufacturer}</td>
                  <td>{unitCount}</td>
                  <td>{quantity}</td>
                  <td>{dateRequest:dd/MM/yyyy}</td>
                  <td>{deadline:dd/MM/yyyy}</td>
              </tr>");
                }

                tableBuilder.Append("</tbody></table>");

                // Tạo record gửi mail
                EmployeeSendEmailModel sendEmail = new EmployeeSendEmailModel();
                sendEmail.Subject = $"[THÔNG BÁO] TỪ CHỐI BÁO GIÁ - {DateTime.Now:dd/MM/yyyy}";
                sendEmail.EmailTo = receiver?.EmailCongTy ?? "";
                sendEmail.EmailCC = ""; // cc người gửi nếu cần
                sendEmail.Body = $@"
              <div> 
                  <p style=""font-weight: bold; color: red;"">[NO REPLY]</p> 
                  <p> Dear anh/chị {(receiver != null ? receiver.FullName : "phụ trách")}, </p>
              </div>
              <div style=""margin-top: 20px;"">
                  <p><b>Lý do từ chối:</b> {reason}</p>
              <br/>
              <p>Danh sách sản phẩm yêu cầu báo giá đã bị từ chối:</p>
                  {tableBuilder}
              </div>
              <div style=""margin-top: 20px;"">
                  
                  <p>Trân trọng,</p>
              </div>";

                sendEmail.StatusSend = 1;
                sendEmail.EmployeeID = employeeId;          // Người gửi = Global.EmployeeID
                sendEmail.Receiver = receiver?.ID ?? 0;     // Người yêu cầu
                sendEmail.DateSend = DateTime.Now;

                SQLHelper<EmployeeSendEmailModel>.Insert(sendEmail);
            }
        }

        private void btnCancelUnPrice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdatePriceRequestStatus(1, "Hủy từ chối báo giá", false);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExportExcelChoise(true);
        }
        #endregion


        //Chọn loại tiền --> Update tỷ giá --> Tính thành tiền quy đổi
        //Nhập đơn giá --> Tính thành tiền chưa VAT  --> Tính thành tiền quy đổi
        //Nhập %VAT --> Tính Thành tiền có VAT
        //Nhập Leader time (Ngày làm việc) --> Tính Ngày dự kiến hàng về
        //Nhập đơn giá nhập khẩu --> Thành tiền nhập khẩu
    }
}
