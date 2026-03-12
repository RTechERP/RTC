using BaseBusiness.DTO;
using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraTab;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmProjectPartlistPurchaseRequest : _Forms
    {
        public bool isSelectedPO = false;
        public int supplierSaleId = 0;
        DataTable dtPurchaseRequest = new DataTable();
        DataTable dtProductRTC = new DataTable();
        DataTable dtProductRTCBorrow = new DataTable();
        DataTable dtPurchaseMarketing = new DataTable();

        public int poKHID = 0;
        public bool listRequestBuySelect = false;

        public bool isYCMH = false;
        public bool isApprovedTBP = false;//PQ.Chien - UPDATE - 17 / 04 / 2025

        public List<string> lstYCMHCode = new List<string>();
        public List<int> lstYCMH = new List<int>();

        List<GridColumn> columnFixeds = new List<GridColumn>();

        public bool isPurchaseRequestDemo = false;
        public string productCode = "";

        public frmProjectPartlistPurchaseRequest()
        {
            InitializeComponent();
        }

        private void frmProjectPartlistPurchaseRequest_Load(object sender, EventArgs e)
        {

            //xtraTabPage4.PageVisible = Global.IsAdmin;
            Lib.LockEvents = true;
            LoadPOCode();

            grvData.OptionsBehavior.Editable = poKHID == 0;
            grvData.OptionsBehavior.ReadOnly = poKHID == 0;
            chkIsCommercialProduct.Checked = poKHID > 0;
            if (poKHID > 0)
            {
                this.WindowState = FormWindowState.Maximized;
                foreach (ToolStripItem item in toolStrip2.Items)
                {
                    //if (item == btnDelete) continue;
                    item.Visible = false;
                }

                btnDelete.Visible = true;
            }
            else if (isPurchaseRequestDemo)
            {
                xtraTabPage1.PageVisible = xtraTabPage2.PageVisible = !isPurchaseRequestDemo;
                foreach (ToolStripItem item in toolStrip2.Items)
                {
                    item.Visible = false;
                }
                btnDelete.Visible = true;
                txtKeyword.Text = productCode;
            }
            else
            {
                btnAddUnitPrice.Visible = btnBGDApproved.Visible = btnBGDUnApproved.Visible = !isSelectedPO;
                grvData.OptionsBehavior.Editable = !isSelectedPO;
                grvData.OptionsBehavior.ReadOnly = isSelectedPO;
            }


            dtpDateStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDateEnd.Value = dtpDateStart.Value.AddMonths(+2).AddDays(-1);
            cboStatusRequest.SelectedIndex = 1;

            cboIsApprovedTBP.SelectedIndex = isSelectedPO ? 2 : 0;
            cboIsApprovedBGD.SelectedIndex = isSelectedPO ? 2 : 0;
            cboIsDeleted.SelectedIndex = 1;


            if (listRequestBuySelect == true)
            {
                grvData.Columns[$"{colPOKHCode.FieldName}"].GroupIndex = 1;
                grvData.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            }


            //PQ.Chien - UPDATE - 17 / 04 / 2025
            if (isApprovedTBP)
            {
                foreach (ToolStripItem item in toolStrip2.Items)
                {
                    item.Visible = !isApprovedTBP;
                }

                btnPurchaseUnApproved.Visible = isApprovedTBP;
                btnPurchaseApproved.Visible = isApprovedTBP;

                xtraTabPage1.PageVisible = !isApprovedTBP;
                xtraTabPage2.PageVisible = !isApprovedTBP;
                xtraTabPage3.PageVisible = !isApprovedTBP;
                xtraTabPage5.PageVisible = !isApprovedTBP;
            }

            LoadProject();
            LoadSupplierSale();
            LoadProductGroup();
            LoadCurrency();
            loadUnitMoney();
            LoadEmployeeAprrove();//PQ.Chien - UPDATE - 17 / 04 / 2025
            //LoadProductRTC();
            LoadData();

            if (isYCMH)
            {
                //lee min khooi update 27/09/2024
                foreach (ToolStripItem item in toolStrip2.Items)
                {
                    if (item == btnAddPONCC) continue;
                    item.Visible = !isYCMH;
                }
                btnAddPONCC.Text = !isYCMH ? btnAddPONCC.Text : "Chọn YCMH";
            }
            else if (poKHID > 0)
            {
                foreach (ToolStripItem item in toolStrip2.Items)
                {
                    if (item == btnDelete) continue;
                    item.Visible = !(poKHID > 0);
                }
            }

            columnFixeds = grvData.Columns.Where(x => x.Fixed == FixedStyle.Left).ToList();
            colCurrencyRate.OptionsColumn.AllowEdit = (Global.IsAdmin && Global.EmployeeID <= 0);
            colCurrencyRate.OptionsColumn.ReadOnly = !(Global.IsAdmin && Global.EmployeeID <= 0);

            colQuantity.OptionsColumn.AllowEdit = (Global.IsAdmin && Global.EmployeeID <= 0);
            colQuantity.OptionsColumn.ReadOnly = !(Global.IsAdmin && Global.EmployeeID <= 0);

            gridColumn284.OptionsColumn.AllowEdit = (Global.IsAdmin && Global.EmployeeID <= 0);
            gridColumn284.OptionsColumn.ReadOnly = !(Global.IsAdmin && Global.EmployeeID <= 0);

            btnUpdateRequestType.Visible = (Global.EmployeeID <= 0 && Global.IsAdmin);
            Lib.LockEvents = false;
        }

        void LoadProject()
        {
            List<ProjectModel> list = SQLHelper<ProjectModel>.FindAll().OrderByDescending(x => x.ID).ToList();

            //DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetProjectPartlistRequest", new string[] { }, new object[] { });
            //DataTable dt = dataSet.Tables[1];
            cboProject.Properties.ValueMember = "ID";
            cboProject.Properties.DisplayMember = "ProjectCode";
            cboProject.Properties.DataSource = list;
        }
        //ndNhat update 26/03/2025
        void loadUnitMoney()
        {
            List<CurrencyModel> lstUnit = SQLHelper<CurrencyModel>.FindAll();
            repositoryItemSearchLookUpEdit8.DataSource = lstUnit;
            repositoryItemSearchLookUpEdit8.ValueMember = "ID";
            repositoryItemSearchLookUpEdit8.DisplayMember = "Code";
        }

        void LoadSupplierSale()
        {
            List<SupplierSaleModel> list = SQLHelper<SupplierSaleModel>.FindAll().OrderByDescending(x => x.ID).ToList();
            cboSupplierSale.ValueMember = "ID";
            cboSupplierSale.DisplayMember = "NameNCC";
            cboSupplierSale.DataSource = list;

            cboSupplier.Properties.ValueMember = "ID";
            cboSupplier.Properties.DisplayMember = "NameNCC";
            cboSupplier.Properties.DataSource = list;
            cboSupplier.EditValue = supplierSaleId;
            //ndNhat update 27/03/2025
            repositoryItemSearchLookUpEdit5.ValueMember = "ID";
            repositoryItemSearchLookUpEdit5.DisplayMember = "NameNCC";
            repositoryItemSearchLookUpEdit5.DataSource = list;


            //PQ.Chien 19/04/2025====================
            cboSupplierSaleDemo.ValueMember = "ID";
            cboSupplierSaleDemo.DisplayMember = "NameNCC";
            cboSupplierSaleDemo.DataSource = list;

            //HuyNT - 14/06/2025
            cboNCCMkt.ValueMember = "ID";
            cboNCCMkt.DisplayMember = "NameNCC";
            cboNCCMkt.DataSource = list;
        }

        void LoadProductGroup()
        {
            List<ProductGroupModel> list = SQLHelper<ProductGroupModel>.FindByAttribute("IsVisible", 1);
            cboProductGroup.ValueMember = "ID";
            cboProductGroup.DisplayMember = "ProductGroupName";
            cboProductGroup.DataSource = list;


            var exp1 = new Expression("WarehouseID", 1);
            var exp2 = new Expression("ProductGroupNo", "DBH", "NOT LIKE");
            var exp3 = new Expression("ProductGroupNo", "CCDC", "<>");

            var listDemo = SQLHelper<ProductGroupRTCModel>.FindByExpression(exp1.And(exp2).And(exp3));
            cboProductGroupRTC.ValueMember = "ID";
            cboProductGroupRTC.DisplayMember = "ProductGroupName";
            cboProductGroupRTC.DataSource = listDemo;


            cboProductGroupMKT.ValueMember = "ID";
            cboProductGroupMKT.DisplayMember = "ProductGroupName";
            cboProductGroupMKT.DataSource = list;
        }

        void LoadCurrency()
        {
            List<CurrencyModel> list = SQLHelper<CurrencyModel>.FindAll();
            cboCurrency.ValueMember = "ID";
            cboCurrency.DisplayMember = "Code";
            cboCurrency.DataSource = list;

            //PQ.Chien 19/04/2025====================
            cboCurrencyDemo.ValueMember = "ID";
            cboCurrencyDemo.DisplayMember = "Code";
            cboCurrencyDemo.DataSource = list;

            //HuyNT - Update 14/06/2025
            cboCurrencyMKT.ValueMember = "ID";
            cboCurrencyMKT.DisplayMember = "Code";
            cboCurrencyMKT.DataSource = list;
        }

        void LoadPOCode()
        {
            List<POKHModel> dt = SQLHelper<POKHModel>.FindAll();
            cboPOCode.Properties.DisplayMember = "POCode";
            cboPOCode.Properties.ValueMember = "ID";
            cboPOCode.Properties.DataSource = dt;
            cboPOCode.EditValue = poKHID;
        }


        void LoadProductRTC()
        {
            List<ProductRTCModel> dt = SQLHelper<ProductRTCModel>.FindAll();
            cboProductRTC.Properties.DisplayMember = "ProductCode";
            cboProductRTC.Properties.ValueMember = "ID";
            cboProductRTC.Properties.DataSource = dt;
        }

        //PQ.Chien - UPDATE - 17 / 04 / 2025
        void LoadEmployeeAprrove()
        {
            List<EmployeeApproveModel> list = SQLHelper<EmployeeApproveModel>.FindAll();
            cboEmployeeApprove.ValueMember = "ID";
            cboEmployeeApprove.DisplayMember = "FullName";
            cboEmployeeApprove.DataSource = list;
        }


        //PQ.Chien - UPDATE - 17 / 04 / 2025
        private void cboEmployeeApprove_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit lookUpEdit = sender as SearchLookUpEdit;
            EmployeeApproveModel model = lookUpEdit.GetSelectedDataRow() as EmployeeApproveModel;
            if (model == null) return;
            int rowHandle = grvBorrowProduct.FocusedRowHandle;
            grvBorrowProduct.SetRowCellValue(rowHandle, colEmployeeApproveID, model.FullName);
        }



        //PQ.Chien - UPDATE - 17 / 04 / 2025

        Stopwatch stopwatch= new Stopwatch();
        Stopwatch stopwatch2= new Stopwatch();
        void LoadData()
        {
            //if (Lib.LockEvents) return;
            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang load dữ liệu"))
            {
                try
                {
                    Lib.LockEvents = true;
                    //if (!SaveData("load dữ liệu")) return;
                    if (!SaveData($"Bạn có muốn lưu lại thay đổi không?")) return;

                    DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day, 0, 0, 0);
                    DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59);
                    int statusRequest = cboStatusRequest.SelectedIndex;
                    int projectId = TextUtils.ToInt(cboProject.EditValue);
                    int productrtcId = TextUtils.ToInt(cboProductRTC.EditValue);
                    string keyword = txtKeyword.Text.Trim();

                    int supplierSaleId = TextUtils.ToInt(cboSupplier.EditValue);
                    int isApprovedTBP = cboIsApprovedTBP.SelectedIndex - 1;
                    int isApprovedBGD = cboIsApprovedBGD.SelectedIndex - 1;
                    int isCommercialProduct = chkIsCommercialProduct.Checked ? 1 : 0;
                    poKHID = TextUtils.ToInt(cboPOCode.EditValue);
                    int isDeleted = cboIsDeleted.SelectedIndex - 1;
                    int isJobRequirement = chkIsJobRequirement.Checked ? 1 : 0;

                    ////ds hàng dự án
                    //dtPurchaseRequest = TextUtils.LoadDataFromSP("spGetProjectPartlistPurchaseRequest_New", "A",
                    //    new string[] { "@DateStart", "@DateEnd", "@StatusRequest", "@ProjectID", "@Keyword", "@SupplierSaleID", "@IsApprovedTBP", "@IsApprovedBGD", "@IsCommercialProduct", "@POKHID", "@ProductRTCID", "@IsDeleted", "@IsTechBought" },
                    //    new object[] { dateStart, dateEnd, statusRequest, projectId, keyword, supplierSaleId, isApprovedTBP, isApprovedBGD, isCommercialProduct, poKHID, 0, isDeleted, 0 });

                    //grdData.DataSource = dtPurchaseRequest;

                    ////ds hàng kt đã mua
                    //DataTable dtTechBought = TextUtils.LoadDataFromSP("spGetProjectPartlistPurchaseRequest_New", "A",
                    //new string[] { "@DateStart", "@DateEnd", "@StatusRequest", "@ProjectID", "@Keyword", "@SupplierSaleID", "@IsApprovedTBP", "@IsApprovedBGD", "@IsCommercialProduct", "@POKHID", "@ProductRTCID", "@IsDeleted", "@IsTechBought" },
                    //new object[] { dateStart, dateEnd, statusRequest, projectId, keyword, supplierSaleId, isApprovedTBP, isApprovedBGD, isCommercialProduct, poKHID, productrtcId, isDeleted, 1 });

                    //gridControl1.DataSource = dtTechBought;


                    ////ds hàng demo
                    //dtProductRTC = TextUtils.LoadDataFromSP("spGetProjectPartlistPurchaseRequest_New", "A",
                    //    new string[] { "@DateStart", "@DateEnd", "@StatusRequest", "@ProjectID", "@Keyword", "@SupplierSaleID", "@IsApprovedTBP", "@IsApprovedBGD", "@IsCommercialProduct", "@POKHID", "@ProductRTCID", "@IsDeleted", "@IsTechBought" },
                    //    new object[] { dateStart, dateEnd, statusRequest, projectId, keyword, supplierSaleId, isApprovedTBP, isApprovedBGD, isCommercialProduct, poKHID, productrtcId, isDeleted, 0 });

                    //grdProductRTC.DataSource = dtProductRTC;


                    //stopwatch.Reset();
                    //stopwatch.Start();

                    DataTable dtAll = TextUtils.LoadDataFromSP("spGetProjectPartlistPurchaseRequest_New_Khanh", "A",
                        new string[] { "@DateStart", "@DateEnd", "@StatusRequest", "@ProjectID", "@Keyword", "@SupplierSaleID", "@IsApprovedTBP", "@IsApprovedBGD", "@IsCommercialProduct", "@POKHID", "@ProductRTCID", "@IsDeleted", "@IsTechBought", "@IsJobRequirement" },
                        new object[] { dateStart, dateEnd, statusRequest, projectId, keyword, supplierSaleId, isApprovedTBP, isApprovedBGD, isCommercialProduct, poKHID, -1, isDeleted, -1, isJobRequirement });
                    //stopwatch.Stop();

                    //MessageBox.Show($"{stopwatch.ElapsedMilliseconds}", "exec store DataTable: ");

                    //stopwatch2.Reset();
                    //stopwatch2.Start();
                    ////var datas = SQLHelper<spGetProjectPartlistPurchaseRequest_New_Khanh>.ProcedureToList("spGetProjectPartlistPurchaseRequest_New_Khanh",
                    ////    new string[] { "@DateStart", "@DateEnd", "@StatusRequest", "@ProjectID", "@Keyword", "@SupplierSaleID", "@IsApprovedTBP", "@IsApprovedBGD", "@IsCommercialProduct", "@POKHID", "@ProductRTCID", "@IsDeleted", "@IsTechBought", "@IsJobRequirement" },
                    ////    new object[] { dateStart, dateEnd, statusRequest, projectId, keyword, supplierSaleId, isApprovedTBP, isApprovedBGD, isCommercialProduct, poKHID, -1, isDeleted, -1, isJobRequirement });


                    //var dataDapper = SQLHelper<object>.GetProjectPartlistPurchaseRequest(dateStart, dateEnd, statusRequest, projectId, keyword, supplierSaleId, isApprovedTBP, isApprovedBGD, isCommercialProduct, poKHID, -1, isDeleted, -1, isJobRequirement,-1,0,0);
                    //stopwatch2.Stop();

                    //MessageBox.Show($"{stopwatch2.ElapsedMilliseconds}", "exec store l: ");


                    string filterNotMKT = $"{ProjectPartlistPurchaseRequestModel_Enum.ProjectPartlistPurchaseRequestTypeID} <> 7";

                    // ndnhat update 08/09/2025
                    AddWarehouseColumns();
                    //AddWarehouseColumns(grdProductRTC);
                    //AddWarehouseColumns(gridControl1);
                    //AddWarehouseColumns(grdData);
                    //AddWarehouseColumns(grdBorrowProduct);
                    //AddWarehouseColumns(grdProductMarketing);

                    // Lọc PurchaseRequest
                    dtPurchaseRequest = dtAll.Clone();
                    var dataPurchaseRequest = dtAll.Select($"([ProductRTCID] <= 0 OR [ProductRTCID] IS NULL) AND {filterNotMKT}");
                    if (dataPurchaseRequest.Length > 0) dtPurchaseRequest = dataPurchaseRequest.CopyToDataTable();
                    grdData.DataSource = dtPurchaseRequest;

                    // Lọc ProductRTC
                    dtProductRTC = dtAll.Clone();
                    var dataRTC = dtAll.Select($"[ProductRTCID] > 0 AND TicketType = 0 AND {filterNotMKT}");
                    if (dataRTC.Length > 0) dtProductRTC = dataRTC.CopyToDataTable();
                    grdProductRTC.DataSource = dtProductRTC;

                    // Lọc kt đã mua
                    DataTable dtTechBought = dtAll.Clone();
                    var dataTechBought = dtAll.Select($"[IsTechBought] = true AND {filterNotMKT}");
                    if (dataTechBought.Length > 0) dtTechBought = dataTechBought.CopyToDataTable();
                    gridControl1.DataSource = dtTechBought;


                    //PQ.Chien - UPDATE - 17 / 04 / 2025====================
                    dtProductRTCBorrow = dtAll.Clone();

                    string filter = $"[TicketType] = 1 AND {filterNotMKT}";
                    if (this.isApprovedTBP) filter += $" AND ApprovedTBP = {Global.EmployeeID}";
                    if (dtAll.Select(filter).Length > 0)
                    {
                        DataRow[] dt = dtAll.Select(filter);
                        dtProductRTCBorrow = dt.CopyToDataTable();
                    }
                    grdBorrowProduct.DataSource = dtProductRTCBorrow;

                    //HuyNT - Update 14062025 - Hiển thị danh sách yêu cầu mua hàng Marketing
                    //ProductGroupModel marketingGroup = SQLHelper<ProductGroupModel>.FindByAttribute("ProductGroupID", "MK").FirstOrDefault();
                    //if (marketingGroup != null && marketingGroup.ID > 0)
                    //{
                    //    dtPurchaseMarketing = dtAll.Clone();
                    //    if (dtAll.Select($"[ProductGroupID] = {marketingGroup.ID}").Length > 0)
                    //    {
                    //        dtPurchaseMarketing = dtAll.Select($"ProductGroupID = {marketingGroup.ID}").CopyToDataTable();
                    //    }
                    //    grdProductMarketing.DataSource = dtPurchaseMarketing;
                    //}

                    dtPurchaseMarketing = dtAll.Clone();
                    var dataMKT = dtAll.Select($"{ProjectPartlistPurchaseRequestModel_Enum.ProjectPartlistPurchaseRequestTypeID} = 7");
                    if (dataMKT.Length > 0) dtPurchaseMarketing = dataMKT.CopyToDataTable();
                    grdProductMarketing.DataSource = dtPurchaseMarketing;
                    //==================================================================================== 

                    //Get scale screen
                    var scale = TextUtils.GetScaleScreen(this);
                    if (scale > 1)
                    {

                        foreach (var col in columnFixeds.OrderByDescending(x => x.VisibleIndex))
                        {
                            if (col.VisibleIndex <= 6) continue;
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
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
                }
                finally
                {
                    Lib.LockEvents = false;
                }

            }
        }



        List<GridView> gridViews = new List<GridView>();
        
        private void AddWarehouseColumns()
        {
            try
            {
                List<GridView> gridViews = new List<GridView>() { grvData, grvBorrowProduct, grvProductRTC, grvProductMarketing, gridView6 };

                string[] codes = SQLHelper<WarehouseModel>.FindAll().Select(x => x.WarehouseCode).ToArray();

                foreach (string wh in codes)
                {
                    string colName = "Total" + wh;

                    foreach (var view in gridViews)
                    {
                        if (view.Columns[colName] == null)
                        {
                            GridColumn col = new GridColumn
                            {
                                FieldName = colName,
                                Caption = "Tồn được sử dụng " + wh,
                                Visible = true,
                                VisibleIndex = view.Columns.Count,
                                DisplayFormat = { FormatType = DevExpress.Utils.FormatType.Numeric, FormatString = "n2" },
                                Width = 100,
                                MinWidth = 80,
                                OptionsColumn = { AllowEdit = false, ReadOnly = true }
                            };

                            view.Columns.Add(col);

                            col.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                            col.SummaryItem.DisplayFormat = "{0:n2}";


                            view.GroupSummary.Add(new GridGroupSummaryItem
                            {
                                FieldName = colName,
                                SummaryType = DevExpress.Data.SummaryItemType.Sum,
                                DisplayFormat = "{0:n2}",
                                ShowInGroupColumnFooter = col
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }
        void Approved(bool isApproved, bool type)
        {
            string isApprovedText = isApproved ? "duyệt" : "huỷ duyệt";

            SaveData($"Bạn có muốn lưu lại thay đổi trước khi {isApprovedText} không?");
            int isApprovedValue = isApproved ? 1 : 0;
            string typeText = type ? "TBP" : "BGD";

            var tabSelected = xtraTabControl1.SelectedTabPage;

            if (tabSelected.Controls.Count <= 0) return;
            GridControl gridControl = (GridControl)tabSelected.Controls[0];
            GridView gridView = gridControl.MainView as GridView;

            int[] selectedRows = gridView.GetSelectedRows();
            if (selectedRows.Length <= 0)
            {
                MessageBox.Show($"Vui lòng chọn sản phẩm muốn {isApprovedText}!", "Thông báo");
                return;
            }

            string message = "";
            //if (type && !isApproved) message = $"Những sản phẩm đã được BGĐ duyệt sẽ không thể {isApprovedText}!";
            //if (!type && isApproved) message = $"Những sản phẩm chưa được TBP duyệt sẽ không thể {isApprovedText}!";
            DialogResult dialogResult = MessageBox.Show($"Bạn có chắc muốn {isApprovedText} danh sách sản phẩm đã chọn không?\n{message}", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult != DialogResult.Yes) return;

            //if (isApproved)
            //{
            //    if (!SaveData(isApprovedText)) return;
            //}

            foreach (int row in selectedRows)
            {
                int id = TextUtils.ToInt(gridView.GetRowCellValue(row, colID.FieldName));
                if (id <= 0) continue;
                int productSaleId = TextUtils.ToInt(gridView.GetRowCellValue(row, colProductSaleID.FieldName));
                int productRTCId = TextUtils.ToInt(gridView.GetRowCellValue(row, colProductRTCID.FieldName));
                string productNewCode = TextUtils.ToString(gridView.GetRowCellValue(row, colProductNewCode.FieldName));
                string productCode = TextUtils.ToString(gridView.GetRowCellValue(row, colProductCode.FieldName));
                if (productSaleId <= 0 && isApproved && productNewCode == null)
                {
                    MessageBox.Show($"Vui lòng tạo Mã nội bộ cho sản phẩm [{productCode}].\nChọn Loại kho sau đó chọn Lưu thay đổi để tạo Mã nội bộ!", "Thông báo");
                    return;
                }
                ;
            }

            List<int> listId = new List<int>();
            foreach (int row in selectedRows)
            {
                int id = TextUtils.ToInt(gridView.GetRowCellValue(row, colID));
                //int productSaleId = TextUtils.ToInt(grvData.GetRowCellValue(row, colProductSaleID));
                bool isAprovedTBP = TextUtils.ToBoolean(gridView.GetRowCellValue(row, colIsApprovedTBP));
                bool isAprovedBGD = TextUtils.ToBoolean(gridView.GetRowCellValue(row, colIsApprovedBGD));
                if (id <= 0) continue;
                //if (type && !isApproved && isAprovedBGD) continue;//Nếu là TBP huỷ duyệt và BGĐ đã duyệt
                //if (!type && isApproved && !isAprovedTBP) continue; //Nếu là BGĐ duyệt và TBP chưa duyệt

                listId.Add(id);
            }

            if (listId.Count <= 0) return;
            string listIdText = string.Join(",", listId);

            var myDict = new Dictionary<string, object>();
            if (typeText == "TBP")
            {
                myDict = new Dictionary<string, object>()
                {
                    { ProjectPartlistPurchaseRequestModel_Enum.IsApprovedTBP.ToString(), isApprovedValue},
                    //{ ProjectPartlistPurchaseRequestModel_Enum.ApprovedTBP.ToString(), Global.EmployeeID},
                    { ProjectPartlistPurchaseRequestModel_Enum.DateApprovedTBP.ToString(), DateTime.Now},
                    { ProjectPartlistPurchaseRequestModel_Enum.UpdatedBy.ToString(),Global.LoginName},
                    { ProjectPartlistPurchaseRequestModel_Enum.UpdatedDate.ToString(), DateTime.Now },
                };
            }
            else
            {
                myDict = new Dictionary<string, object>()
                {
                    { ProjectPartlistPurchaseRequestModel_Enum.IsApprovedBGD.ToString(), isApprovedValue},
                    { ProjectPartlistPurchaseRequestModel_Enum.ApprovedBGD.ToString(), Global.EmployeeID},
                    { ProjectPartlistPurchaseRequestModel_Enum.DateApprovedBGD.ToString(), DateTime.Now},
                    { ProjectPartlistPurchaseRequestModel_Enum.UpdatedBy.ToString(),Global.LoginName},
                    { ProjectPartlistPurchaseRequestModel_Enum.UpdatedDate.ToString(), DateTime.Now },
                };
            }


            var exp = new Expression("ID", listIdText, "IN");
            SQLHelper<ProjectPartlistPurchaseRequestModel>.UpdateFields(myDict, exp);
            LoadData();
        }

        void UpdateStatusPurcharRequest(int status)
        {
            var tabSelected = xtraTabControl1.SelectedTabPage;

            if (tabSelected.Controls.Count <= 0) return;
            GridControl gridControl = (GridControl)tabSelected.Controls[0];
            GridView gridView = gridControl.MainView as GridView;

            string statusText = status == 3 ? "đặt hàng" : (status == 4 ? "đang về" : (status == 5 ? "đã về" : "không đặt hàng"));
            int[] selectedRows = gridView.GetSelectedRows();
            if (selectedRows.Length <= 0)
            {
                MessageBox.Show($"Vui lòng chọn sản phẩm muốn [{statusText}]", "Thông báo");
                return;
            }

            string question = $"Bạn có chắc muốn cập nhật thành trạng thái {statusText} danh sách sản phẩm đã chọn không?";
            string lblDate = status == 3 ? "Ngày đặt hàng" : (status == 4 ? "Ngày dự kiến hàng về" : (status == 5 ? "Ngày về" : "Lý do"));

            frmPriceRequestDetail frm = new frmPriceRequestDetail();
            frm.Text = $"CẬP NHẬT TRẠNG THÁI {statusText.ToUpper()}";
            frm.lblMessage.Text = question;
            frm.label1.Text = lblDate;
            frm.dtpDeadlinePriceRequest.Visible = status != 6;
            frm.txtNote.Visible = status == 6;
            frm.statusPurchaseRequest = status;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                foreach (int node in selectedRows)
                {
                    int id = TextUtils.ToInt(gridView.GetRowCellValue(node, colID.FieldName));
                    ProjectPartlistPurchaseRequestModel request = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByID(id);
                    request = request == null ? new ProjectPartlistPurchaseRequestModel() : request;
                    request.StatusRequest = status;
                    if (status == 3) request.DateOrder = frm.dtpDeadlinePriceRequest.Value;
                    else if (status == 4) request.DateEstimate = frm.dtpDeadlinePriceRequest.Value;
                    else if (status == 5) request.DateReturnActual = frm.dtpDeadlinePriceRequest.Value;

                    if (request.ID > 0)
                    {
                        SQLHelper<ProjectPartlistPurchaseRequestModel>.Update(request);
                    }
                }
                LoadData();
            }
        }


        //bool SaveData(string message)
        //{
        //    int id = 0;
        //    decimal unitPrice = 0;
        //    decimal totalPrice = 0;
        //    int supplierSaleId = 0;
        //    string unitMoney = "";
        //    int productGroupId = 0;
        //    int productSaleId = 0;

        //    string sql = "";
        //    grvData.CloseEditor();
        //    var dataChange = dtPurchaseRequest.GetChanges();
        //    if (dataChange != null)
        //    {
        //        if (string.IsNullOrEmpty(message))
        //        {
        //            foreach (DataRow row in dataChange.Rows)
        //            {
        //                id = TextUtils.ToInt(row["ID"]);
        //                unitPrice = TextUtils.ToInt(row["UnitPrice"]);
        //                totalPrice = TextUtils.ToInt(row["TotalPrice"]);
        //                supplierSaleId = TextUtils.ToInt(row["SupplierSaleID"]);
        //                unitMoney = TextUtils.ToString(row["UnitMoney"]).Trim();
        //                productGroupId = TextUtils.ToInt(row["ProductGroupID"]);
        //                productSaleId = TextUtils.ToInt(row["ProductSaleID"]);

        //                sql += $"UPDATE dbo.ProjectPartlistPurchaseRequest SET " +
        //                        $"UnitPrice = {unitPrice}," +
        //                        $"TotalPrice = {totalPrice}," +
        //                        $"SupplierSaleID = {supplierSaleId}," +
        //                        $"UnitMoney = '{unitMoney}'," +
        //                        $"ProductGroupID = {productGroupId}," +
        //                        $"ProductSaleID = {productSaleId}," +
        //                        $"UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
        //                        $"UpdatedBy = '{Global.LoginName}' WHERE ID = {id}\n";
        //            }

        //            if (!string.IsNullOrEmpty(sql))
        //            {
        //                TextUtils.ExcuteSQL(sql);
        //            }

        //            dtPurchaseRequest.AcceptChanges();

        //        }
        //        else
        //        {
        //            DialogResult dialog = MessageBox.Show($"Những thay đổi chưa được lưu.\nBạn có muốn lưu lại trước khi {message} không?", "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        //            if (dialog == DialogResult.Yes)
        //            {
        //                foreach (DataRow row in dataChange.Rows)
        //                {
        //                    id = TextUtils.ToInt(row["ID"]);
        //                    unitPrice = TextUtils.ToInt(row["UnitPrice"]);
        //                    totalPrice = TextUtils.ToInt(row["TotalPrice"]);
        //                    supplierSaleId = TextUtils.ToInt(row["SupplierSaleID"]);
        //                    unitMoney = TextUtils.ToString(row["UnitMoney"]).Trim();
        //                    productGroupId = TextUtils.ToInt(row["ProductGroupID"]);
        //                    productSaleId = TextUtils.ToInt(row["ProductSaleID"]);

        //                    sql += $"UPDATE dbo.ProjectPartlistPurchaseRequest SET " +
        //                            $"UnitPrice = {unitPrice}," +
        //                            $"TotalPrice = {totalPrice}," +
        //                            $"SupplierSaleID = {supplierSaleId}," +
        //                            $"UnitMoney = '{unitMoney}'," +
        //                            $"ProductGroupID = {productGroupId}," +
        //                            $"ProductSaleID = {productSaleId}," +
        //                            $"UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
        //                            $"UpdatedBy = '{Global.LoginName}' WHERE ID = {id}\n";


        //                }

        //                if (!string.IsNullOrEmpty(sql))
        //                {
        //                    TextUtils.ExcuteSQL(sql);
        //                }
        //            }
        //            else if (dialog == DialogResult.Cancel)
        //            {
        //                return false;
        //            }
        //            else
        //            {
        //                foreach (DataRow row in dataChange.Rows)
        //                {
        //                    var dataRow = dtPurchaseRequest.Select($"ID = {row["ID"]}")[0];
        //                    if (dataRow == null) continue;

        //                    int index = dtPurchaseRequest.Rows.IndexOf(dataRow);
        //                    dtPurchaseRequest.Rows[index]["UnitPrice"] = dtPurchaseRequest.Rows[index]["UnitPrice", DataRowVersion.Original];
        //                    dtPurchaseRequest.Rows[index]["TotalPrice"] = dtPurchaseRequest.Rows[index]["TotalPrice", DataRowVersion.Original];
        //                    dtPurchaseRequest.Rows[index]["SupplierSaleID"] = dtPurchaseRequest.Rows[index]["SupplierSaleID", DataRowVersion.Original];
        //                    dtPurchaseRequest.Rows[index]["UnitMoney"] = dtPurchaseRequest.Rows[index]["UnitMoney", DataRowVersion.Original];
        //                }
        //            }
        //        }
        //    }

        //    dtPurchaseRequest.AcceptChanges();
        //    return true;
        //}

        bool SaveData(string message)
        {
            try
            {
                var tabSelected = xtraTabControl1.SelectedTabPage;

                if (tabSelected.Controls.Count <= 0) return false;
                GridControl gridControl = (GridControl)tabSelected.Controls[0];
                GridView gridView = gridControl.MainView as GridView;

                grvData.FocusedRowHandle = -1;
                grvData.CloseEditor();

                grvProductRTC.FocusedRowHandle = -1;
                grvProductRTC.CloseEditor();

                grvBorrowProduct.FocusedRowHandle = -1;
                grvBorrowProduct.CloseEditor();

                grvProductMarketing.FocusedRowHandle = -1;
                grvProductMarketing.CloseEditor();

                //var columnEdits = grvData.Columns.Where(x => x.OptionsColumn.AllowEdit == true && x.Visible == true).ToList();
                var dataChange = dtPurchaseRequest.GetChanges();
                var dataChangeRTC = dtProductRTC.GetChanges();
                var dataChangeRTCBorrow = dtProductRTCBorrow.GetChanges();
                var datachangeMKT = dtPurchaseMarketing.GetChanges();

                if (dataChange != null && gridView.Name == grvData.Name)
                {
                    if (string.IsNullOrEmpty(message.Trim()))
                    {
                        UpdateData(dataChange);

                    }
                    else
                    {
                        DialogResult dialog = MessageBox.Show("Bạn có muốn lưu lại thay đổi không?\nNhững sản phẩm NV mua không phải bạn sẽ tự động được bỏ qua", "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (dialog == DialogResult.Yes)
                        {
                            UpdateData(dataChange);
                        }
                        else if (dialog == DialogResult.No)
                        {
                            foreach (DataRow row in dataChange.Rows)
                            {
                                var dataRow = dtPurchaseRequest.Select($"ID = {row["ID"]}")[0];
                                if (dataRow == null) continue;

                                int index = dtPurchaseRequest.Rows.IndexOf(dataRow);
                                foreach (GridColumn column in grvData.Columns)
                                {
                                    if (!dtPurchaseRequest.Columns.Contains(column.FieldName)) continue;
                                    dtPurchaseRequest.Rows[index][column.FieldName] = dtPurchaseRequest.Rows[index][column.FieldName, DataRowVersion.Original];
                                }
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }

                }

                //nếu là demo
                if (dataChangeRTC != null && gridView.Name == grvProductRTC.Name)
                {
                    if (string.IsNullOrEmpty(message.Trim()))
                    {
                        UpdateData(dataChangeRTC);

                    }
                    else
                    {
                        DialogResult dialog = MessageBox.Show("Bạn có muốn lưu lại thay đổi không?\nNhững sản phẩm NV mua không phải bạn sẽ tự động được bỏ qua", "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (dialog == DialogResult.Yes)
                        {
                            UpdateData(dataChangeRTC);
                        }
                        else if (dialog == DialogResult.No)
                        {
                            foreach (DataRow row in dataChangeRTC.Rows)
                            {
                                var dataRow = dtProductRTC.Select($"ID = {row["ID"]}")[0];
                                if (dataRow == null) continue;

                                int index = dtProductRTC.Rows.IndexOf(dataRow);
                                foreach (GridColumn column in grvProductRTC.Columns)
                                {
                                    if (!dtProductRTC.Columns.Contains(column.FieldName)) continue;
                                    dtProductRTC.Rows[index][column.FieldName] = dtProductRTC.Rows[index][column.FieldName, DataRowVersion.Original];
                                }
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }

                }

                //nếu là demo mượn
                if (dataChangeRTCBorrow != null && gridView.Name == grvBorrowProduct.Name)
                {
                    if (string.IsNullOrEmpty(message.Trim()))
                    {
                        UpdateData(dataChangeRTCBorrow);

                    }
                    else
                    {
                        DialogResult dialog = MessageBox.Show("Bạn có muốn lưu lại thay đổi không?\nNhững sản phẩm NV mua không phải bạn sẽ tự động được bỏ qua", "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (dialog == DialogResult.Yes)
                        {
                            UpdateData(dataChangeRTCBorrow);
                        }
                        else if (dialog == DialogResult.No)
                        {
                            foreach (DataRow row in dataChangeRTCBorrow.Rows)
                            {
                                var dataRow = dtProductRTCBorrow.Select($"ID = {row["ID"]}")[0];
                                if (dataRow == null) continue;

                                int index = dtProductRTCBorrow.Rows.IndexOf(dataRow);
                                foreach (GridColumn column in grvBorrowProduct.Columns)
                                {
                                    if (!dtProductRTCBorrow.Columns.Contains(column.FieldName)) continue;
                                    dtProductRTCBorrow.Rows[index][column.FieldName] = dtProductRTCBorrow.Rows[index][column.FieldName, DataRowVersion.Original];
                                }
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }

                }

                //TODO : HuyNT - Update 14/06/2025
                if (datachangeMKT != null && gridView.Name == grvProductMarketing.Name)
                {
                    if (string.IsNullOrEmpty(message.Trim()))
                    {
                        UpdateData(datachangeMKT);

                    }
                    else
                    {
                        DialogResult dialog = MessageBox.Show("Bạn có muốn lưu lại thay đổi không?\nNhững sản phẩm NV mua không phải bạn sẽ tự động được bỏ qua", "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (dialog == DialogResult.Yes)
                        {
                            UpdateData(datachangeMKT);
                        }
                        else if (dialog == DialogResult.No)
                        {
                            foreach (DataRow row in datachangeMKT.Rows)
                            {
                                var dataRow = dtPurchaseMarketing.Select($"ID = {row["ID"]}")[0];
                                if (dataRow == null) continue;

                                int index = dtPurchaseMarketing.Rows.IndexOf(dataRow);
                                foreach (GridColumn column in grvData.Columns)
                                {
                                    if (!dtPurchaseMarketing.Columns.Contains(column.FieldName)) continue;
                                    dtPurchaseMarketing.Rows[index][column.FieldName] = dtPurchaseMarketing.Rows[index][column.FieldName, DataRowVersion.Original];
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
            catch (Exception ex)
            {

                return false;
                throw;
            }
        }


        void UpdateData(DataTable dataSource)
        {
            try
            {
                var tabSelected = xtraTabControl1.SelectedTabPage;

                if (tabSelected.Controls.Count <= 0) return;
                GridControl gridControl = (GridControl)tabSelected.Controls[0];
                GridView gridView = gridControl.MainView as GridView;

                if (!checkValidate()) return;

                List<GridColumn> columnEdits = gridView.Columns.Where(x => x.OptionsColumn.AllowEdit == true && x.Visible == true).ToList();

                //string sql = "";
                foreach (DataRow row in dataSource.Rows)
                {
                    int id = TextUtils.ToInt(row["ID"]);
                    ProjectPartlistPurchaseRequestModel request = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByID(id);
                    if (request.EmployeeIDRequestApproved != Global.EmployeeID && !isAdmin) continue;

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

                    int productSaleID = TextUtils.ToInt(row["ProductSaleID"]);
                    int productRTCID = TextUtils.ToInt(row["ProductRTCID"]);

                    decimal targetPrice = TextUtils.ToDecimal(row["TargetPrice"]);//ndnhat-update 17/08/2025
                    int duplicateID = TextUtils.ToInt(row["DuplicateID"]);//ndnhat-update 17/08/2025
                    decimal originQuantity = TextUtils.ToDecimal(row["OriginQuantity"]);//ndnhat-update 17/08/2025

                    //string columnUpdate = "";

                    var myDict = new Dictionary<string, object>()
                    {
                        { ProjectPartlistPurchaseRequestModel_Enum.CurrencyRate.ToString(), currencyRate},
                        { ProjectPartlistPurchaseRequestModel_Enum.TotalPrice.ToString(), totalPrice},
                        { ProjectPartlistPurchaseRequestModel_Enum.TotalPriceExchange.ToString(), totalPriceExchange},
                        { ProjectPartlistPurchaseRequestModel_Enum.TotaMoneyVAT.ToString(), totalMoneyVAT},
                        { ProjectPartlistPurchaseRequestModel_Enum.ProductSaleID.ToString(), productSaleID},
                        { ProjectPartlistPurchaseRequestModel_Enum.ProductRTCID.ToString(), productRTCID},
                        { ProjectPartlistPurchaseRequestModel_Enum.UpdatedDate.ToString(), DateTime.Now },
                        { ProjectPartlistPurchaseRequestModel_Enum.UpdatedBy.ToString(), Global.LoginName },
                    };

                    //ndnhat-update 17/07/2025
                    if (duplicateID > 0)
                    {
                        myDict.Add(ProjectPartlistPurchaseRequestModel_Enum.Quantity.ToString(), quantity);
                        myDict.Add(ProjectPartlistPurchaseRequestModel_Enum.OriginQuantity.ToString(), originQuantity);
                    }
                    //end ndnhat-update 17/07/2025

                    foreach (GridColumn col in columnEdits)
                    {
                        //var format = col.DisplayFormat.Format;
                        var columnTypeName = col.ColumnType.Name.ToLower();
                        string value = TextUtils.ToString(row[col.FieldName]).Trim();

                        if (columnTypeName == "int32" || columnTypeName == "decimal") value = string.IsNullOrEmpty(value) ? "0" : value;
                        else if (columnTypeName == "boolean") value = value == "true" ? "1" : "0";
                        //else value = value;

                        //columnUpdate += $"{col.FieldName} = {value},";

                        //myDict.Add(col.FieldName, value);

                        myDict[col.FieldName] = value;
                    }

                    //var exp = new Expression("ID", id);

                    //var result = SQLHelper<ProjectPartlistPurchaseRequestModel>.UpdateFields(myDict, exp);
                    SQLHelper<ProjectPartlistPurchaseRequestModel>.UpdateFieldsByID(myDict, id);
                }


                dtPurchaseRequest.AcceptChanges();
                dtProductRTC.AcceptChanges();
                dtProductRTCBorrow.AcceptChanges();
                dataSource.AcceptChanges();
                dtPurchaseMarketing.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
                throw;
            }

        }

        bool isAdmin = (Global.IsAdmin && Global.EmployeeID <= 0);

        void CreateProduct()
        {

            try
            {
                //grvData.FocusedRowHandle = -1;
                //grvData.CloseEditor();

                var tabSelected = xtraTabControl1.SelectedTabPage;

                if (tabSelected.Controls.Count <= 0) return;
                GridControl gridControl = (GridControl)tabSelected.Controls[0];
                GridView gridView = gridControl.MainView as GridView;

                gridView.FocusedRowHandle = -1;
                gridView.CloseEditor();

                if (!validateManufacturer(gridView)) return; //ndnhat update 19/07/2025

                var expf1 = new Expression(FirmModel_Enum.FirmType, 1);
                var expf2 = new Expression(FirmModel_Enum.IsDelete, 0);
                var firms = SQLHelper<FirmModel>.FindByExpression(expf1.And(expf2));

                for (int i = 0; i < gridView.RowCount; i++)
                {
                    int id = TextUtils.ToInt(gridView.GetRowCellValue(i, colID.FieldName));
                    if (id <= 0) continue;

                    ProjectPartlistPurchaseRequestModel request = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByID(id);
                    if (request.EmployeeIDRequestApproved != Global.EmployeeID && !isAdmin) continue;

                    int productGroupId = TextUtils.ToInt(gridView.GetRowCellValue(i, colProductGroupID.FieldName));
                    int productGroupRTCId = TextUtils.ToInt(gridView.GetRowCellValue(i, colProductGroupRTCID.FieldName));

                    if (productGroupId <= 0 && productGroupRTCId <= 0) continue;

                    string productCode = TextUtils.ToString(gridView.GetRowCellValue(i, colProductCode.FieldName)).Trim();
                    //if (productCode != "testtttt") continue;
                    var exp1 = new Expression("ProductGroupID", productGroupId);
                    var exp2 = new Expression("ProductCode", productCode);
                    var exp3 = new Expression("IsDeleted", 0);

                    if (productGroupId > 0 && (gridView.Name == grvData.Name || gridView.Name == grvProductMarketing.Name))//Insert vào kho Sale
                    {
                        ProductSaleModel productSale = SQLHelper<ProductSaleModel>.FindByExpression(exp1.And(exp2).And(exp3)).FirstOrDefault();

                        productSale = productSale ?? new ProductSaleModel();

                        if (productSale.ID > 0)
                        {
                            //productSale.ProductNewCode = LoadNewCode(productGroupId);
                            //SQLHelper<ProductSaleModel>.Update(productSale);

                        }
                        else
                        {
                            productSale.ProductCode = productCode;
                            productSale.ProductName = TextUtils.ToString(gridView.GetRowCellValue(i, colProductName.FieldName)).Trim();
                            productSale.Unit = TextUtils.ToString(gridView.GetRowCellValue(i, colUnitName.FieldName)).Trim();
                            productSale.ProductGroupID = productGroupId;
                            productSale.ProductNewCode = LoadNewCode(productGroupId);
                            string maker = TextUtils.ToString(gridView.GetRowCellValue(i, colManufacturer.FieldName)).Trim();

                            FirmModel firm = firms.FirstOrDefault(x => x.FirmName.Trim().ToLower() == maker.Trim().ToLower()) ?? new FirmModel();
                            productSale.Maker = TextUtils.ToString(gridView.GetRowCellValue(i, colManufacturer.FieldName)).Trim();
                            productSale.FirmID = firm.ID;
                            productSale.ID = SQLHelper<ProductSaleModel>.Insert(productSale).ID;
                        }

                        gridView.SetRowCellValue(i, colProductSaleID, productSale.ID);
                        gridView.SetRowCellValue(i, colProductNewCode, productSale.ProductNewCode);

                        // Update ProductID cho các POKHDetail có cùng ProjectPartlistID // NTA B - update 101025
                        if (request.ProjectPartListID > 0)
                        {
                            var expPOKH = new Expression("ProjectPartListID", request.ProjectPartListID);
                            //var pokhDetails = SQLHelper<POKHDetailModel>.FindByExpression(expPOKH);

                            //foreach (var detail in pokhDetails)
                            //{
                            //    detail.ProductID = productSale.ID;
                            //    SQLHelper<POKHDetailModel>.Update(detail);
                            //}

                            var myDict = new Dictionary<string, object>()
                            {
                                { "ProductID" ,productSale.ID},
                            };
                            SQLHelper<RequestInvoiceModel>.UpdateFields(myDict, expPOKH);
                        }
                    }
                    //else //Insert vào kho Demo
                    //{
                    //    //exp1 = new Expression(ProductRTCModel_Enum.ProductGroupRTCID, productGroupRTCId);

                    //    ProductRTCModel productRTC = SQLHelper<ProductRTCModel>.FindByExpression(exp2).FirstOrDefault();

                    //    productRTC = productRTC ?? new ProductRTCModel();
                    //    productRTC.ProductCode = productCode;
                    //    productRTC.ProductName = TextUtils.ToString(gridView.GetRowCellValue(i, colProductName)).Trim();

                    //    string unitName = TextUtils.ToString(gridView.GetRowCellValue(i, colUnitName)).Trim();
                    //    UnitCountKTModel unitCountKT = SQLHelper<UnitCountKTModel>.FindByAttribute("UnitCountName", unitName).FirstOrDefault() ?? new UnitCountKTModel();
                    //    productRTC.UnitCountID = unitCountKT.ID;
                    //    productRTC.ProductGroupRTCID = productGroupRTCId;
                    //    if (productRTC.ID > 0)
                    //    {
                    //        //SQLHelper<ProductSaleModel>.Update(productSale);
                    //    }
                    //    else
                    //    {
                    //        productRTC.ProductCodeRTC = GetProductCodeRTC();
                    //        productRTC.ID = SQLHelper<ProductRTCModel>.Insert(productRTC).ID;
                    //    }

                    //    gridView.SetRowCellValue(i, colProductRTCID.FieldName, productRTC.ID);
                    //    gridView.SetRowCellValue(i, "ProductCodeRTC", productRTC.ProductCodeRTC);
                    //}


                }
                //LoadData();
            }
            catch (Exception ex)
            {
                //string a = "a";
                throw;
            }
        }

        string GetProductCodeRTC()
        {
            string numberCodeDefault = "00000001";
            string productCodeRTC = "Z";
            var listProducts = SQLHelper<ProductRTCModel>.FindAll();
            var listproductCodeRTCs = listProducts.Select(x => new
            {
                ProductCodeRTC = x.ProductCodeRTC,
                STT = string.IsNullOrWhiteSpace(x.ProductCodeRTC) ? 0 : TextUtils.ToInt(x.ProductCodeRTC.Substring(1))
            }).ToList();

            int numberCode = listproductCodeRTCs.Count <= 0 ? 0 : listproductCodeRTCs.Max(x => x.STT);
            string numberCodeText = (++numberCode).ToString();

            while (numberCodeText.Length < numberCodeDefault.Length)
            {
                numberCodeText = "0" + numberCodeText;
            }
            productCodeRTC += numberCodeText;

            return productCodeRTC;
        }

        string LoadNewCode(int productGroupId)
        {
            string newCodeRTC = "";
            if (productGroupId <= 0) return newCodeRTC;

            DataSet ds = TextUtils.LoadDataSetFromSP("spLoadNewCodeRTC", new string[] { "@Group" }, new object[] { productGroupId });
            string code = "";
            string codeRTC = TextUtils.ToString(ds.Tables[1].Rows[0][0]);

            if (ds.Tables[0].Rows.Count == 0)
            {
                newCodeRTC = codeRTC + "000000001";
            }
            else
            {
                if (!codeRTC.Contains("HCM"))
                {
                    code = TextUtils.ToString(ds.Tables[0].Rows[0][0]).Replace(codeRTC, "");
                    int stt = TextUtils.ToInt(code) + 1;
                    for (int i = 0; codeRTC.Length < (9 - stt.ToString().Length); i++)
                    {
                        codeRTC = codeRTC + "0";
                    }
                    newCodeRTC = codeRTC + stt.ToString();
                }
                else
                {
                    code = TextUtils.ToString(ds.Tables[0].Rows[0][0]).Replace(codeRTC, "");
                    int stt = TextUtils.ToInt(code) + 1;
                    string indexString = TextUtils.ToString(stt);
                    for (int i = 0; indexString.Length < code.Length; i++)
                    {
                        indexString = "0" + indexString;
                    }
                    newCodeRTC = codeRTC + indexString.ToString();
                }
            }

            return newCodeRTC;
        }


        decimal CalculatorTotalMoneyExchange(int rowHandle)
        {
            var tabSelected = xtraTabControl1.SelectedTabPage;

            if (tabSelected.Controls.Count <= 0) return 0;
            GridControl gridControl = (GridControl)tabSelected.Controls[0];
            GridView gridView = gridControl.MainView as GridView;

            decimal totalMoney = TextUtils.ToDecimal(gridView.GetRowCellValue(rowHandle, colTotalPrice));
            decimal currencyRate = TextUtils.ToDecimal(gridView.GetRowCellValue(rowHandle, colCurrencyRate));
            decimal totalMoneyExchange = totalMoney * currencyRate;
            return totalMoneyExchange;
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
                    sqlInsertIsImport += $"UPDATE dbo.ProjectPartlistPurchaseRequest SET IsImport = {isImportValue}, " +
                                            $"UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                                            $"UpdatedBy = '{Global.LoginName}'" +
                                            $"WHERE ID = {id}\n";
                }

                if (!string.IsNullOrEmpty(sqlInsertIsImport.Trim())) TextUtils.ExcuteSQL(sqlInsertIsImport);
                LoadData();
            }
            //int id = 0;


        }

        void RequestApproved(bool isRequestApproved)
        {
            var tabSelected = xtraTabControl1.SelectedTabPage;

            if (tabSelected.Controls.Count <= 0) return;
            GridControl gridControl = (GridControl)tabSelected.Controls[0];
            GridView gridView = gridControl.MainView as GridView;

            int[] selectedRows = gridView.GetSelectedRows();
            //grvData.CloseEditor();
            string isRequestApprovedText = isRequestApproved ? "yêu cầu duyệt" : "huỷ yêu cầu duyệt";
            if (selectedRows.Length <= 0)
            {
                MessageBox.Show($"Vui lòng chọn sản phẩm muốn {isRequestApprovedText}!", "Thông báo");
                return;
            }

            foreach (int row in selectedRows)
            {
                int id = TextUtils.ToInt(gridView.GetRowCellValue(row, colID.FieldName));
                if (id <= 0) continue;
                int productSaleId = TextUtils.ToInt(gridView.GetRowCellValue(row, colProductSaleID));
                int productRTCId = TextUtils.ToInt(gridView.GetRowCellValue(row, colProductRTCID));
                int supplierSaleId = TextUtils.ToInt(gridView.GetRowCellValue(row, colSupplierSaleID.FieldName));
                decimal unitPrice = TextUtils.ToDecimal(gridView.GetRowCellValue(row, colUnitPrice.FieldName));
                int currencyIDRequest = TextUtils.ToInt(gridView.GetRowCellValue(row, colCurrencyID.FieldName));

                string productCode = TextUtils.ToString(gridView.GetRowCellValue(row, colProductCode.FieldName));
                if (productRTCId <= 0)
                {
                    //if (productSaleId <= 0)
                    //{
                    //    MessageBox.Show($"Vui lòng tạo Mã nội bộ cho sản phẩm [{productCode}].\nChọn Loại kho sau đó chọn Lưu thay đổi để tạo Mã nội bộ!", "Thông báo");
                    //    return;
                    //}

                    if (supplierSaleId <= 0)
                    {
                        MessageBox.Show($"Vui lòng nhập Nhà cung cấp cho sản phẩm [{productCode}].\nChọn Nhà cung cấp sau đó chọn Lưu thay đổi!", "Thông báo");
                        return;
                    }

                    if (unitPrice <= 0)
                    {
                        MessageBox.Show($"Vui lòng nhập Đơn giá cho sản phẩm [{productCode}].\nChọn Nhà cung cấp sau đó chọn Lưu thay đổi!", "Thông báo");
                        return;
                    }

                    if (productRTCId <= 0 && productSaleId <= 0)
                    {
                        MessageBox.Show($"Vui lòng tạo Mã nội bộ cho sản phẩm [{productCode}].\nChọn Loại kho sau đó chọn Lưu thay đổi để tạo Mã nội bộ!", "Thông báo");
                        return;
                    }

                    if (currencyIDRequest <= 0)
                    {
                        MessageBox.Show($"Vui lòng chọn loại tiền tệ cho sản phẩm [{productCode}].\nChọn Loại tiền sau đó chọn Lưu thay đổi!", "Thông báo");
                        return;
                    }
                }

            }

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn {isRequestApprovedText} danh sách sản phẩm đã chọn không?\nNhững sản phẩm NV mua không phải bạn sẽ tự động được bỏ qua!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                DataTable data = new DataTable();
                dtPurchaseRequest.AcceptChanges();
                data = dtPurchaseRequest.Clone();

                foreach (int row in selectedRows)
                {

                    int id = TextUtils.ToInt(gridView.GetRowCellValue(row, colID.FieldName));
                    if (id <= 0) continue;

                    ProjectPartlistPurchaseRequestModel request = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByID(id);
                    if (request.EmployeeIDRequestApproved != Global.EmployeeID && !isAdmin) continue;

                    if (row < 0) continue;

                    DataRow dataRow = data.NewRow();

                    dataRow = gridView.GetDataRow(row);
                    data.ImportRow(dataRow);
                }

                if (data.Rows.Count > 0)
                {
                    UpdateData(data);
                }

                List<int> listId = new List<int>();
                foreach (int row in selectedRows)
                {
                    int id = TextUtils.ToInt(gridView.GetRowCellValue(row, colID.FieldName));
                    if (id <= 0) continue;

                    ProjectPartlistPurchaseRequestModel request = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByID(id);
                    if (request.EmployeeIDRequestApproved != Global.EmployeeID && !isAdmin) continue;

                    listId.Add(id);
                    //grvData.SetRowCellValue(row, colIsImport, 0);
                    //int isRequestApprovedValue = isRequestApproved ? 1 : 0;
                    //sqlInsertIsImport += $"UPDATE dbo.ProjectPartlistPurchaseRequest SET IsImport = {isImportValue}, " +
                    //                        $"UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                    //                        $"UpdatedBy = '{Global.LoginName}'" +
                    //                        $"WHERE ID = {id}\n";
                }

                if (listId.Count <= 0) return;

                int isRequestApprovedValue = isRequestApproved ? 1 : 0;
                string idText = string.Join(",", listId);

                var myDict = new Dictionary<string, object>()
                {
                    { ProjectPartlistPurchaseRequestModel_Enum.IsRequestApproved.ToString(), isRequestApprovedValue},
                    { ProjectPartlistPurchaseRequestModel_Enum.EmployeeIDRequestApproved.ToString(), Global.EmployeeID},
                    { ProjectPartlistPurchaseRequestModel_Enum.UpdatedBy.ToString(),Global.LoginName},
                    { ProjectPartlistPurchaseRequestModel_Enum.UpdatedDate.ToString(), DateTime.Now },
                };

                var exp = new Expression("ID", idText, "IN");
                SQLHelper<ProjectPartlistPurchaseRequestModel>.UpdateFields(myDict, exp);
                LoadData();
            }
        }

        private void cboStatusRequest_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadData();
        }

        private void cboProject_EditValueChanged(object sender, EventArgs e)
        {
            //LoadData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnAddUnitPrice_Click(object sender, EventArgs e)
        {
            using (WaitDialogForm fWait = new WaitDialogForm())
            {
                CreateProduct();
                SaveData("");
            }

        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            UpdateStatusPurcharRequest(3);
        }

        private void btnReturning_Click(object sender, EventArgs e)
        {
            UpdateStatusPurcharRequest(4);
        }

        private void btnReturned_Click(object sender, EventArgs e)
        {
            UpdateStatusPurcharRequest(5);
        }

        private void btnNotOrder_Click(object sender, EventArgs e)
        {
            UpdateStatusPurcharRequest(6);
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            GridView gridView = sender as GridView;
            if (gridView == null) return;
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(gridView.GetFocusedRowCellValue(gridView.FocusedColumn)).Trim();
                if (string.IsNullOrEmpty(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void btnDuplicateContext_Click(object sender, EventArgs e)
        {
            Duplicate();
            return;

            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id <= 0) return;
            string productCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductCode));
            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn duplicate yêu cầu mua vật tư [{productCode}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog != DialogResult.Yes) return;
            ProjectPartlistPurchaseRequestModel purchase = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByID(id);
            if (purchase == null || purchase.ID <= 0) return;

            purchase.ID = 0;
            purchase.Quantity = 0;
            SQLHelper<ProjectPartlistPurchaseRequestModel>.Insert(purchase);
        }

        private void btnPurchaseApproved_Click(object sender, EventArgs e)
        {
            Approved(true, true);
        }

        private void btnPurchaseUnApproved_Click(object sender, EventArgs e)
        {
            Approved(false, true);
        }

        private void btnBGDApproved_Click(object sender, EventArgs e)
        {
            Approved(true, false);
        }

        private void btnBGDUnApproved_Click(object sender, EventArgs e)
        {
            Approved(false, false);
        }

        void UpdateValue(int row)
        {
            var tabSelected = xtraTabControl1.SelectedTabPage;

            if (tabSelected.Controls.Count <= 0) return;
            GridControl gridControl = (GridControl)tabSelected.Controls[0];
            GridView gridView = gridControl.MainView as GridView;

            decimal quantity = TextUtils.ToDecimal(gridView.GetRowCellValue(row, colQuantity));
            decimal unitPrice = TextUtils.ToDecimal(gridView.GetRowCellValue(row, colUnitPrice));
            decimal currencyRate = TextUtils.ToDecimal(gridView.GetRowCellValue(row, colCurrencyRate));

            decimal totalPrice = quantity * unitPrice;
            gridView.SetRowCellValue(row, colTotalPrice, totalPrice);

            decimal totalPriceExchange = quantity * unitPrice * currencyRate;
            gridView.SetRowCellValue(row, colTotalPriceExchange, totalPriceExchange);

            decimal unitImportPrice = TextUtils.ToDecimal(gridView.GetRowCellValue(row, colUnitImportPrice));
            decimal totalImportPrice = unitImportPrice * quantity;
            gridView.SetRowCellValue(row, colTotalImportPrice, totalImportPrice);

            decimal vat = TextUtils.ToDecimal(gridView.GetRowCellValue(row, colVAT));
            decimal totalMoneyVAT = totalPrice + ((totalPrice * vat) / 100);
            gridView.SetRowCellValue(row, colTotaMoneyVAT, totalMoneyVAT);
        }

        bool isRecallCellValueChanged = false;
        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //if (grvData.FocusedColumn == colCurrencyID) return;
            if (isRecallCellValueChanged == true) return;
            try
            {
                if (e.Column != colProductSaleID && e.Column != colProductNewCode)
                {
                    using (WaitDialogForm fWait = new WaitDialogForm())
                    {
                        grvData.CloseEditor();
                        isRecallCellValueChanged = true;
                        if (grvData.SelectedRowsCount > 0)
                        {
                            if (e.Value == null) return;
                            foreach (int row in grvData.GetSelectedRows())
                            {
                                if (e.Column == colProductSaleID) continue;
                                if (e.Column == colProductNewCode) continue;
                                if (e.Column == colInventoryProjectID) continue;
                                grvData.SetRowCellValue(row, grvData.Columns[e.Column.FieldName], e.Value);


                                if (grvData.FocusedColumn != colUnitPrice
                                    && grvData.FocusedColumn != colUnitImportPrice
                                    && grvData.FocusedColumn != colVAT
                                    && grvData.FocusedColumn != colTargetPrice)
                                {
                                    continue;
                                }

                                UpdateValue(row);
                            }
                        }
                        else UpdateValue(grvData.FocusedRowHandle);
                    }
                }
            }
            finally
            {
                isRecallCellValueChanged = false;
            }


        }

        private void frmProjectPartlistPurchaseRequest_FormClosing(object sender, FormClosingEventArgs e)
        {
            //SaveData("đóng");
            e.Cancel = !SaveData($"Bạn có muốn lưu lại thay đổi không?");
        }

        //private void btnAddPONCC_Click(object sender, EventArgs e)
        //{
        //    int[] selectedRows = grvData.GetSelectedRows();
        //    int id = 0;

        //    if (selectedRows.Length <= 0)
        //    {
        //        MessageBox.Show($"Vui lòng chọn sản phẩm muốn Tạo PO NCC!", "Thông báo");
        //        return;
        //    }

        //    var dataChange = dtPurchaseRequest.GetChanges();
        //    if (dataChange != null)
        //    {
        //        MessageBox.Show($"Vui lòng Lưu thay đổi trước kho Tạo PO NCC!", "Thông báo");
        //        return;
        //    }

        //    foreach (int row in selectedRows)
        //    {
        //        id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
        //        if (id <= 0) continue;
        //        int productSaleID = TextUtils.ToInt(grvData.GetRowCellValue(row, colProductSaleID));
        //        int supplierSaleId = TextUtils.ToInt(grvData.GetRowCellValue(row, colSupplierSaleID));
        //        string productCode = TextUtils.ToString(grvData.GetRowCellValue(row, colProductCode));
        //        if (productSaleID <= 0)
        //        {
        //            MessageBox.Show($"Vui lòng tạo Mã nội bộ cho sản phẩm [{productCode}].\nChọn Loại kho sau đó chọn Lưu thay đổi để tạo Mã nội bộ!", "Thông báo");
        //            return;
        //        }

        //        if (supplierSaleId <= 0)
        //        {
        //            MessageBox.Show($"Vui lòng nhập Nhà cung cấp cho sản phẩm [{productCode}].\nChọn Nhà cung cấp sau đó chọn Lưu thay đổi!", "Thông báo");
        //            return;
        //        }
        //    }

        //    //Check validate danh sách sản phẩm có cùng nhà cung cấp không
        //    List<int> listSupplierSale = new List<int>();
        //    foreach (int row in selectedRows)
        //    {
        //        id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
        //        if (id <= 0) continue;
        //        int supplierSaleId = TextUtils.ToInt(grvData.GetRowCellValue(row, colSupplierSaleID));
        //        listSupplierSale.Add(supplierSaleId);
        //    }

        //    listSupplierSale = listSupplierSale.Distinct().ToList();
        //    if (listSupplierSale.Count > 1)
        //    {
        //        MessageBox.Show("Vui lòng chỉ chọn sản phầm từ 1 Nhà cung cấp!", "Thông báo");
        //        return;
        //    }

        //    DialogResult dialog = MessageBox.Show("Bạn có chắc muốn tạo PO NCC danh sách sản phẩm đã chọn không.\n" +
        //                                          "Những sản phẩm chưa được BGĐ duyệt sẽ bỏ qua!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //    if (dialog != DialogResult.Yes) return;
        //    //if (!SaveData("")) return;

        //    //CreateProduct();

        //    //List<ProjectPartlistPurchaseRequestModel> listRequest = new List<ProjectPartlistPurchaseRequestModel>();
        //    List<ProjectPartlistPurchaseRequestDTO> listRequest = new List<ProjectPartlistPurchaseRequestDTO>();
        //    List<int> currencys = new List<int>();
        //    foreach (int row in selectedRows)
        //    {
        //        id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
        //        if (id <= 0) continue;
        //        bool isApprovedBGD = TextUtils.ToBoolean(grvData.GetRowCellValue(row, colIsApprovedBGD));
        //        bool isCommercialProduct = TextUtils.ToBoolean(grvData.GetRowCellValue(row, colIsCommercialProduct));
        //        if (!isApprovedBGD && !isCommercialProduct) continue;
        //        int productId = TextUtils.ToInt(grvData.GetRowCellValue(row, colProductSaleID));
        //        decimal quantity = TextUtils.ToDecimal(grvData.GetRowCellValue(row, colQuantity));
        //        decimal unitPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(row, colUnitPrice));
        //        int projectId = TextUtils.ToInt(grvData.GetRowCellValue(row, colProjectID));
        //        string projectName = TextUtils.ToString(grvData.GetRowCellValue(row, colProjectName));
        //        DateTime? deadline = TextUtils.ToDate4(grvData.GetRowCellValue(row, colDateReturnExpected));
        //        string productCode = TextUtils.ToString(grvData.GetRowCellValue(row, colProductCode));
        //        decimal vat = TextUtils.ToDecimal(grvData.GetRowCellValue(row, colVAT));
        //        decimal totaMoneyVAT = TextUtils.ToDecimal(grvData.GetRowCellValue(row, colTotaMoneyVAT));

        //        if (isCommercialProduct && projectId <= 0)
        //        {
        //            string customerCode = TextUtils.ToString(grvData.GetRowCellValue(row, colCustomerCode));
        //            string poNumber = TextUtils.ToString(grvData.GetRowCellValue(row, colPONumber));
        //            projectName = $"{customerCode}_{poNumber}";
        //        }


        //        ProjectPartlistPurchaseRequestDTO request = new ProjectPartlistPurchaseRequestDTO()
        //        {
        //            ID = id,
        //            ProductSaleID = productId,
        //            Quantity = quantity,
        //            UnitPrice = unitPrice,
        //            EmployeeID = Global.EmployeeID,
        //            ProjectID = projectId,
        //            //ProjectID = 1,
        //            ProjectName = projectName,
        //            Deadline = deadline,
        //            ProductCode = productCode,
        //            VAT = vat,
        //            TotaMoneyVAT = totaMoneyVAT,
        //            GuestCode = TextUtils.ToString(grvData.GetRowCellValue(row, colGuestCode)),
        //            //GuestCode = "adasdas",
        //            IsCommercialProduct = isCommercialProduct,
        //        };
        //        listRequest.Add(request);
        //        currencys.Add(TextUtils.ToInt(grvData.GetRowCellValue(row, colCurrencyID)));
        //    }

        //    //List<ProjectPartlistPurchaseRequestDTO> result = listRequest.GroupBy(l => l.ProductCode)
        //    //                                                            .Select(cl => new ProjectPartlistPurchaseRequestDTO
        //    //                                                            {
        //    //                                                                ID = cl.First().ID,
        //    //                                                                ProductSaleID = cl.First().ProductSaleID,
        //    //                                                                Quantity = cl.Sum(q => q.Quantity),
        //    //                                                                UnitPrice = cl.First().UnitPrice,
        //    //                                                                EmployeeID = cl.First().EmployeeID,
        //    //                                                                ProjectID = cl.First().ProjectID,
        //    //                                                                ProjectName = cl.First().ProjectName,
        //    //                                                                Deadline = cl.First().Deadline,
        //    //                                                                ProductCode = cl.First().ProductCode,
        //    //                                                                VAT = cl.First().VAT
        //    //                                                            }).ToList();


        //    List<ProjectPartlistPurchaseRequestDTO> result = listRequest.GroupBy(item => new { item.ProductCode, item.ProjectID })
        //                                                                .Select(cl => new ProjectPartlistPurchaseRequestDTO
        //                                                                {
        //                                                                    ID = cl.First().ID,
        //                                                                    ProductSaleID = cl.First().ProductSaleID,
        //                                                                    Quantity = cl.Sum(q => q.Quantity),
        //                                                                    UnitPrice = cl.First().UnitPrice,
        //                                                                    EmployeeID = cl.First().EmployeeID,
        //                                                                    ProjectID = cl.First().ProjectID,
        //                                                                    ProjectName = cl.First().ProjectName,
        //                                                                    Deadline = cl.First().Deadline,
        //                                                                    ProductCode = cl.First().ProductCode,
        //                                                                    VAT = cl.First().VAT,
        //                                                                    PONCCDetailRequestBuyID = string.Join(";", cl.Select(i => i.ID)),
        //                                                                    GuestCode = cl.First().GuestCode,
        //                                                                    IsCommercialProduct = cl.First().IsCommercialProduct,
        //                                                                }).ToList();

        //    //Distint loại tiền
        //    int currencyID = currencys.Distinct().ToList().Count > 1 ? 0 : currencys.Distinct().FirstOrDefault();

        //    frmPONCCDetailNew frm = new frmPONCCDetailNew();
        //    //frm.listRequest = listRequest;
        //    frm.listRequest = result;
        //    //frm.cboSupplierSale.EditValue = listSupplierSale.FirstOrDefault();
        //    frm.supplierSaleID = listSupplierSale.FirstOrDefault();
        //    SupplierSaleModel supplier = SQLHelper<SupplierSaleModel>.FindByID(listSupplierSale.FirstOrDefault());
        //    frm.txtAccountNumberSupplier.Text = supplier.SoTK;
        //    frm.txtBankSupplier.Text = supplier.SoTK;
        //    frm.txtAddressSupplier.Text = supplier.AddressNCC;
        //    frm.txtMaSoThueNCC.Text = supplier.MaSoThue;
        //    frm.cboEmployee.EditValue = Global.EmployeeID;
        //    //frm.cboCurrency.EditValue = currencyID;
        //    frm.currencyID = currencyID;
        //    if (isSelectedPO)
        //    {
        //        this.DialogResult = DialogResult.OK;
        //    }
        //    else
        //    {
        //        PONCCModel po = new PONCCModel();
        //        frm.po = po;
        //        frm.Tag = po.BillCode;
        //        TextUtils.OpenChildForm(frm, null);
        //    }

        //}

        private void btnAddPONCC_Click(object sender, EventArgs e)
        {
            var tabSelected = xtraTabControl1.SelectedTabPage;

            if (tabSelected.Controls.Count <= 0) return;
            GridControl gridControl = (GridControl)tabSelected.Controls[0];
            GridView gridView = gridControl.MainView as GridView;

            int[] selectedRows = gridView.GetSelectedRows();
            int id = 0;
            string message = isYCMH ? "YCM" : "PO NCC";
            if (selectedRows.Length <= 0)
            {
                MessageBox.Show($"Vui lòng chọn sản phẩm muốn Tạo {message}!", "Thông báo");
                return;
            }
            //Check validate danh sách sản phẩm có cùng nhà cung cấp không
            List<int> listSupplierSale = new List<int>();
            foreach (int row in selectedRows)
            {
                id = TextUtils.ToInt(gridView.GetRowCellValue(row, colID.FieldName));
                if (id <= 0) continue;
                int supplierSaleId = TextUtils.ToInt(gridView.GetRowCellValue(row, colSupplierSaleID.FieldName));
                listSupplierSale.Add(supplierSaleId);
            }

            listSupplierSale = listSupplierSale.Distinct().ToList();
            if (listSupplierSale.Count > 1)
            {
                MessageBox.Show("Vui lòng chỉ chọn sản phầm từ 1 Nhà cung cấp!", "Thông báo");
                return;
            }


            // =============================================== lee min khooi update 27/09/2024 ===============================================
            if (isYCMH)
            {
                foreach (int row in selectedRows)
                {
                    id = TextUtils.ToInt(gridView.GetRowCellValue(row, colID.FieldName));
                    if (id <= 0) continue;
                    string code = TextUtils.ToString(gridView.GetRowCellValue(row, colProductNewCode.FieldName));
                    if (!chkIsCommercialProduct.Checked && !chkIsJobRequirement.Checked)
                    {
                        bool isBGDAprroved = TextUtils.ToBoolean(gridView.GetRowCellValue(row, colIsApprovedBGD.FieldName));
                        bool isTechBought = TextUtils.ToBoolean(gridView.GetRowCellValue(row, "IsTechBought"));
                        //int jobRequirementID = TextUtils.ToInt(gridView.GetRowCellValue(row, colJobRequirementID.FieldName));
                        if (!isBGDAprroved && !isTechBought)
                        {
                            MessageBox.Show("Sản phẩm chưa được BGĐ duyệt!", "Thông báo");
                            return;
                        }
                        if (string.IsNullOrWhiteSpace(code))
                        {
                            MessageBox.Show("Sản phẩm chưa có mã nội bộ!", "Thông báo");
                            return;
                        }
                    }
                }


                foreach (int row in selectedRows)
                {
                    id = TextUtils.ToInt(gridView.GetRowCellValue(row, colID.FieldName));
                    string code = TextUtils.ToString(gridView.GetRowCellValue(row, colProductNewCode.FieldName));

                    if (id <= 0) continue;
                    //Kiểm tra xem có phải hàng thương mại không
                    if (!chkIsCommercialProduct.Checked && !chkIsJobRequirement.Checked)
                    {
                        bool isBGDAprroved = TextUtils.ToBoolean(gridView.GetRowCellValue(row, colIsApprovedBGD.FieldName));
                        if (!isBGDAprroved) continue;
                        if (string.IsNullOrWhiteSpace(code)) continue;
                    }

                    if (!lstYCMH.Contains(id))
                    {
                        lstYCMH.Add(id);
                        lstYCMHCode.Add(code);
                    }
                }


                this.DialogResult = DialogResult.OK;
            }
            else
            {
                var dataChange = dtPurchaseRequest.GetChanges();
                var dataChangeRTC = dtProductRTC.GetChanges();
                var dataChangeRTCBorrow = dtProductRTCBorrow.GetChanges();
                if (dataChange != null || dataChangeRTC != null || dataChangeRTCBorrow != null)
                {
                    MessageBox.Show($"Vui lòng Lưu thay đổi trước kho Tạo PO NCC!", "Thông báo");
                    return;
                }

                foreach (int row in selectedRows)
                {


                    id = TextUtils.ToInt(gridView.GetRowCellValue(row, colID.FieldName));
                    if (id <= 0) continue;
                    int supplierSaleId = TextUtils.ToInt(gridView.GetRowCellValue(row, colSupplierSaleID.FieldName));
                    string productCode = TextUtils.ToString(gridView.GetRowCellValue(row, colProductCode.FieldName));
                    int productRtcId = TextUtils.ToInt(gridView.GetRowCellValue(row, colProductRTCID.FieldName));
                    int productSaleId = TextUtils.ToInt(gridView.GetRowCellValue(row, colProductSaleID.FieldName));
                    int currencyIDRequest = TextUtils.ToInt(gridView.GetRowCellValue(row, colCurrencyID.FieldName));
                    decimal unitPrice = TextUtils.ToDecimal(gridView.GetRowCellValue(row, colUnitPrice.FieldName));

                    ProjectPartlistPurchaseRequestModel requestModel = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByID(id);

                    bool isTBPAprroved = TextUtils.ToBoolean(requestModel.IsApprovedTBP);
                    int isBorrowProduct = TextUtils.ToInt(gridView.GetRowCellValue(row, "TicketType")); //0:mua,1:mượn

                    string parentProductCode = TextUtils.ToString(gridView.GetRowCellValue(row, colParentProductCode.FieldName));

                    if (productRtcId <= 0 && productSaleId <= 0)
                    {
                        MessageBox.Show($"Vui lòng tạo Mã nội bộ cho sản phẩm [{productCode}].\nChọn Loại kho sau đó chọn Lưu thay đổi để tạo Mã nội bộ!", "Thông báo");
                        return;
                    }

                    if (currencyIDRequest <= 0 /*&& isBorrowProduct == 0*/)
                    {
                        MessageBox.Show($"Vui lòng chọn loại tiền tệ cho sản phẩm [{productCode}].\nChọn Loại tiền sau đó chọn Lưu thay đổi!", "Thông báo");
                        return;
                    }

                    if (!string.IsNullOrWhiteSpace(parentProductCode))
                    {
                        if (supplierSaleId <= 0)
                        {
                            MessageBox.Show($"Vui lòng nhập Nhà cung cấp cho sản phẩm con [{productCode}].\nChọn Nhà cung cấp sau đó chọn Lưu thay đổi!", "Thông báo");
                            return;
                        }
                        // Bỏ qua validate khác
                        continue;

                    }
                    if (supplierSaleId <= 0)
                    {
                        MessageBox.Show($"Vui lòng nhập Nhà cung cấp cho sản phẩm [{productCode}].\nChọn Nhà cung cấp sau đó chọn Lưu thay đổi!", "Thông báo");
                        return;
                    }

                    if (unitPrice <= 0 && isBorrowProduct == 0)
                    {
                        MessageBox.Show($"Vui lòng nhập Đơn giá cho sản phẩm [{productCode}]!", "Thông báo");
                        return;
                    }

                    if (!isTBPAprroved && isBorrowProduct == 1)
                    {
                        MessageBox.Show("Sản phẩm chưa được TBP duyệt!", "Thông báo");
                        return;
                    }
                }



                DialogResult dialog = MessageBox.Show("Bạn có chắc muốn tạo PO NCC danh sách sản phẩm đã chọn không.\n" +
                                                    "Những sản phẩm chưa được BGĐ duyệt sẽ tự động được bỏ qua!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog != DialogResult.Yes) return;
                List<ProjectPartlistPurchaseRequestDTO> listRequest = new List<ProjectPartlistPurchaseRequestDTO>();
                List<int> currencys = new List<int>();
                foreach (int row in selectedRows)
                {
                    id = TextUtils.ToInt(gridView.GetRowCellValue(row, colID.FieldName));
                    if (id <= 0) continue;

                    //TODO : HuyNT Update 14/06/2025 
                    int requestTypeID = TextUtils.ToInt(gridView.GetRowCellValue(row, colProjectPartlistPurchaseRequestTypeID.FieldName));
                    ProjectPartlistPurchaseRequestTypeModel requestType = SQLHelper<ProjectPartlistPurchaseRequestTypeModel>.FindByID(requestTypeID) ?? new ProjectPartlistPurchaseRequestTypeModel();
                    bool IsIgnoreBGD = TextUtils.ToBoolean(requestType.IsIgnoreBGD);
                    //======================================

                    ProjectPartlistPurchaseRequestModel requestModel = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByID(id);
                    //if (requestModel.EmployeeIDRequestApproved != Global.EmployeeID && !isAdmin) continue;

                    bool isApprovedBGD = TextUtils.ToBoolean(gridView.GetRowCellValue(row, colIsApprovedBGD.FieldName));
                    bool isCommercialProduct = TextUtils.ToBoolean(gridView.GetRowCellValue(row, colIsCommercialProduct.FieldName));
                    bool isTechBought = TextUtils.ToBoolean(gridView.GetRowCellValue(row, "IsTechBought"));

                    //bool isTBPAprroved = TextUtils.ToBoolean(gridView.GetRowCellValue(row, colIsApprovedTBP.FieldName));
                    bool isTBPAprroved = TextUtils.ToBoolean(requestModel.IsApprovedTBP);
                    int isBorrowProduct = TextUtils.ToInt(gridView.GetRowCellValue(row, "TicketType")); //0:mua,1:mượn
                    int productRtcId = TextUtils.ToInt(gridView.GetRowCellValue(row, colProductRTCID.FieldName));
                    int jobRequirementID = TextUtils.ToInt(gridView.GetRowCellValue(row, colJobRequirementID.FieldName));

                    if (jobRequirementID <= 0)
                    {
                        //ndNhat update 27/03/2025
                        if (isBorrowProduct == 0)//mua
                        {
                            if (!isApprovedBGD && !isCommercialProduct && !isTechBought && productRtcId <= 0 && !IsIgnoreBGD) continue;
                        }
                        else
                        {
                            if (!isTBPAprroved) continue;
                        }
                    }

                    //end ndNhat update 27/03/2025
                    int productId = TextUtils.ToInt(gridView.GetRowCellValue(row, colProductSaleID.FieldName));
                    int productRTCID = TextUtils.ToInt(gridView.GetRowCellValue(row, colProductRTCID.FieldName));
                    decimal quantity = TextUtils.ToDecimal(gridView.GetRowCellValue(row, colQuantity.FieldName));
                    decimal unitPrice = TextUtils.ToDecimal(gridView.GetRowCellValue(row, colUnitPrice.FieldName));
                    int projectId = TextUtils.ToInt(gridView.GetRowCellValue(row, colProjectID.FieldName));
                    string projectName = TextUtils.ToString(gridView.GetRowCellValue(row, colProjectName.FieldName));
                    DateTime? deadline = TextUtils.ToDate4(gridView.GetRowCellValue(row, colDateReturnExpected.FieldName));
                    string productCode = TextUtils.ToString(gridView.GetRowCellValue(row, colProductCode.FieldName));
                    decimal vat = TextUtils.ToDecimal(gridView.GetRowCellValue(row, colVAT.FieldName));
                    decimal totaMoneyVAT = TextUtils.ToDecimal(gridView.GetRowCellValue(row, colTotaMoneyVAT.FieldName));
                    int ticketType = TextUtils.ToInt(gridView.GetRowCellValue(row, ProjectPartlistPurchaseRequestModel_Enum.TicketType.ToString()));


                    DateTime? dateReturnEstimated = TextUtils.ToDate4(gridView.GetRowCellValue(row, colDateReturnEstimated.FieldName));

                    if (isCommercialProduct && projectId <= 0)
                    {
                        string customerCode = TextUtils.ToString(gridView.GetRowCellValue(row, colCustomerCode.FieldName));
                        string poNumber = TextUtils.ToString(gridView.GetRowCellValue(row, colPONumber.FieldName));
                        projectName = $"{customerCode}_{poNumber}";
                    }


                    ProjectPartlistPurchaseRequestDTO request = new ProjectPartlistPurchaseRequestDTO()
                    {
                        ID = id,
                        ProductSaleID = productId,
                        ProductRTCID = productRTCID,//ndNhat update 27/03/2025
                        Quantity = quantity,
                        UnitPrice = unitPrice,
                        EmployeeID = Global.EmployeeID,
                        ProjectID = projectId,
                        //ProjectID = 1,
                        ProjectName = projectName,
                        Deadline = deadline,
                        ProductCode = productCode,
                        VAT = vat,
                        TotaMoneyVAT = totaMoneyVAT,
                        GuestCode = TextUtils.ToString(gridView.GetRowCellValue(row, colGuestCode.FieldName)),
                        //GuestCode = "adasdas",
                        IsCommercialProduct = isCommercialProduct,
                        HistoryPrice = TextUtils.ToDecimal(gridView.GetRowCellValue(row, colHistoryPrice.FieldName)),
                        PONCCDetailRequestBuyID = id.ToString(),
                        IsBill = vat > 0,
                        DateReturnEstimated = dateReturnEstimated,
                        TicketType = ticketType,
                        IsStock = TextUtils.ToBoolean(gridView.GetRowCellValue(row, colIsStock.FieldName)),
                        ProductGroupID = TextUtils.ToInt(gridView.GetRowCellValue(row, colProductGroupID.FieldName)),
                        UnitName = TextUtils.ToString(gridView.GetRowCellValue(row, ProjectPartlistPurchaseRequestModel_Enum.UnitName.ToString())),
                        SpecialCode = TextUtils.ToString(gridView.GetRowCellValue(row, colSpecialCode.FieldName)),
                        ParentProductCode = TextUtils.ToString(gridView.GetRowCellValue(row, colParentProductCode)),//ndnhat update 14/10/2025
                        IsPurchase = false//ndnhat update 14/10/2025
                    };
                    listRequest.Add(request);
                    currencys.Add(TextUtils.ToInt(gridView.GetRowCellValue(row, colCurrencyID.FieldName)));
                }


                //Distint loại tiền
                int currencyID = currencys.Distinct().ToList().Count > 1 ? 0 : currencys.Distinct().FirstOrDefault();

                frmPONCCDetailNew frm = new frmPONCCDetailNew();
                //frm.listRequest = result;
                frm.listRequest = listRequest;
                frm.supplierSaleID = listSupplierSale.FirstOrDefault();
                SupplierSaleModel supplier = SQLHelper<SupplierSaleModel>.FindByID(listSupplierSale.FirstOrDefault());
                frm.txtAccountNumberSupplier.Text = supplier.SoTK;
                frm.txtBankSupplier.Text = supplier.SoTK;
                frm.txtAddressSupplier.Text = supplier.AddressNCC;
                frm.txtMaSoThueNCC.Text = supplier.MaSoThue;
                frm.cboEmployee.EditValue = Global.EmployeeID;

                frm.currencyID = currencyID;
                if (isSelectedPO)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    PONCCModel po = new PONCCModel();
                    frm.po = po;
                    frm.Tag = po.BillCode;
                    TextUtils.OpenChildForm(frm, null);
                }
            }
        }

        private void grvData_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            return;
            var tabSelected = xtraTabControl1.SelectedTabPage;

            if (tabSelected.Controls.Count <= 0) return;
            GridControl gridControl = (GridControl)tabSelected.Controls[0];
            GridView gridView = gridControl.MainView as GridView;

            bool isApprovedTBP = TextUtils.ToBoolean(gridView.GetFocusedRowCellValue(colIsApprovedTBP.FieldName));
            bool isApprovedBGD = TextUtils.ToBoolean(gridView.GetFocusedRowCellValue(colIsApprovedBGD.FieldName));
            if (gridView.FocusedColumn == colProductGroupID || gridView.FocusedColumn == colNote) return;

            if (isApprovedTBP && !Global.IsAdmin)
            {
                gridView.BeginUpdate();
                e.Valid = false;
                e.ErrorText = $"Bạn không thể sửa [{gridView.FocusedColumn.Caption}] vì đã được TBP duyệt!";
                gridView.EndUpdate();
            }

            if (isApprovedBGD && !Global.IsAdmin)
            {
                gridView.BeginUpdate();
                e.Valid = false;
                e.ErrorText = $"Bạn không thể sửa [{gridView.FocusedColumn.Caption}] vì đã được BGĐ duyệt!";
                gridView.EndUpdate();
            }
        }

        private void cboSupplier_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboIsApprovedTBP_SelectedIndexChanged(object sender, EventArgs e)
        {
            // LoadData();
        }

        private void cboIsApprovedBGD_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboUnit_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit lookUpEdit = (SearchLookUpEdit)sender;
            CurrencyModel currency = (CurrencyModel)lookUpEdit.GetSelectedDataRow();

            grvData.SetFocusedRowCellValue(colUnitMoney, currency.Code);
            //grvData.SetFocusedRowCellValue(colCurrencyRate, currency.CurrencyRate);
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = Path.Combine(f.SelectedPath, $"YeuCauMua_{DateTime.Now.ToString("ddMMyy")}.xlsx");

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
            CurrencyModel currency = (CurrencyModel)lookUpEdit.GetSelectedDataRow() ?? new CurrencyModel();

            bool isExpried = true;
            if (currency.DateExpried.HasValue && currency.DateStart.HasValue && !string.IsNullOrWhiteSpace(currency.Code))
            {
                //validate ngày bắt đầu < ngày hết hạn
                isExpried = ((currency.DateExpried.Value.Date < DateTime.Now.Date.Date || currency.DateStart.Value.Date > DateTime.Now.Date) && currency.Code.ToLower().Trim() != "vnd");
            }


            int[] rowSelecteds = grvData.GetSelectedRows();
            if (rowSelecteds.Length <= 0)
            {
                grvData.SetRowCellValue(grvData.FocusedRowHandle, colCurrencyID, currency.ID);
                grvData.SetRowCellValue(grvData.FocusedRowHandle, colCurrencyRate, currency.CurrencyRate);
                if (isExpried) grvData.SetRowCellValue(grvData.FocusedRowHandle, colCurrencyRate, 0);
                grvData.SetRowCellValue(grvData.FocusedRowHandle, colTotalPriceExchange, CalculatorTotalMoneyExchange(grvData.FocusedRowHandle));
                //grvData.SetRowCellValue(grvData.FocusedRowHandle, colStatusUpdate, 2);
            }
            else
            {
                using (WaitDialogForm fWait = new WaitDialogForm())
                {
                    foreach (int row in rowSelecteds)
                    {
                        grvData.SetRowCellValue(row, colCurrencyID, currency.ID);
                        grvData.SetRowCellValue(row, colCurrencyRate, currency.CurrencyRate);
                        if (isExpried) grvData.SetRowCellValue(row, colCurrencyRate, 0);
                        grvData.SetRowCellValue(row, colTotalPriceExchange, CalculatorTotalMoneyExchange(row));
                        //grvData.SetRowCellValue(row, colStatusUpdate, 2);
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
            UpdateProductImport(true);
        }

        private void btnRequestApproved_Click(object sender, EventArgs e)
        {
            RequestApproved(true);
        }

        private void btnCancelRequestApproved_Click(object sender, EventArgs e)
        {
            RequestApproved(false);
        }

        private void btnImportContext_Click(object sender, EventArgs e)
        {
            //Tạo phiếu nhập kho trạng thái yc nhập kho
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
            var tabSelected = xtraTabControl1.SelectedTabPage;

            if (tabSelected.Controls.Count <= 0) return;
            GridControl gridControl = (GridControl)tabSelected.Controls[0];
            GridView gridView = gridControl.MainView as GridView;

            int[] rowSelected = gridView.GetSelectedRows();
            if (rowSelected.Length <= 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm muốn xoá!", "Thông báo");
                return;
            }


            //Check valiadate
            if (!Global.IsAdmin)
            {
                foreach (int row in rowSelected)
                {
                    int id = TextUtils.ToInt(gridView.GetRowCellValue(row, "ID"));
                    if (id <= 0) continue;
                    bool isCommercialProduct = TextUtils.ToBoolean(gridView.GetRowCellValue(row, colIsCommercialProduct.FieldName));
                    int poNCC = TextUtils.ToInt(gridView.GetRowCellValue(row, colPONCCID.FieldName));

                    string productCode = TextUtils.ToString(gridView.GetRowCellValue(row, colProductCode.FieldName));

                    if (!isCommercialProduct)
                    {
                        MessageBox.Show($"Sản phẩm mã [{productCode}] không phải hàng thương mại.\nBạn không thể xoá!", "Thông báo");
                        return;
                    }

                    if (poNCC > 0)
                    {
                        MessageBox.Show($"Sản phẩm mã [{productCode}] đã có PO Nhà cung cấp.\nBạn không thể xoá!", "Thông báo");
                        return;
                    }

                    if (isPurchaseRequestDemo)
                    {
                        string updateName = TextUtils.ToString(gridView.GetRowCellValue(row, colUpdatedName2));
                        int requestStatus = TextUtils.ToInt(gridView.GetRowCellValue(row, colStatusRequest2));
                        bool isApprovedTBP = TextUtils.ToBoolean(gridView.GetRowCellValue(row, colIsApprovedTBP2));
                        bool isApprovedBGD = TextUtils.ToBoolean(gridView.GetRowCellValue(row, colIsApprovedBGD2));

                        if (updateName != "" && requestStatus != 1)
                        {
                            MessageBox.Show($"Sản phẩm mã [{productCode}] đã nhân viên mua.\nBạn không thể hủy yêu cầu!", "Thông báo");
                            return;
                        }

                        if (isApprovedTBP)
                        {
                            MessageBox.Show($"Sản phẩm mã [{productCode}] đã được TBP duyệt.\nBạn không thể hủy yêu cầu!", "Thông báo");
                            return;
                        }

                        if (isApprovedBGD)
                        {
                            MessageBox.Show($"Sản phẩm mã [{productCode}] đã được BGD duyệt.\nBạn không thể hủy yêu cầu!", "Thông báo");
                            return;
                        }
                    }
                }
            }

            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn xoá danh sách đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                List<int> inventoryProjects = new List<int>();
                foreach (int row in rowSelected)
                {
                    int id = TextUtils.ToInt(gridView.GetRowCellValue(row, "ID"));
                    if (id <= 0) continue;
                    //idDeletes.Add(id);

                    ProjectPartlistPurchaseRequestModel request = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByID(id);
                    request.IsDeleted = true;

                    //SQLHelper<ProjectPartlistPurchaseRequestModel>.DeleteModelByID(id);
                    SQLHelper<ProjectPartlistPurchaseRequestModel>.Update(request);


                    int inventoryProjectID = TextUtils.ToInt(gridView.GetRowCellValue(row, colInventoryProjectID.FieldName));
                    if (inventoryProjectID > 0) inventoryProjects.Add(inventoryProjectID);
                }


                if (inventoryProjects.Count > 0)
                {
                    var myDict = new Dictionary<string, object>()
                    {
                        {InventoryProjectModel_Enum.IsDeleted.ToString(),true },
                        {InventoryProjectModel_Enum.UpdatedBy.ToString(),Global.AppUserName },
                        {InventoryProjectModel_Enum.UpdatedDate.ToString(),DateTime.Now },
                    };
                    string idInventoryProject = string.Join(",", inventoryProjects);
                    var exp = new Expression(InventoryProjectModel_Enum.ID.ToString(), idInventoryProject, "IN");

                    SQLHelper<InventoryProjectModel>.UpdateFields(myDict, exp);
                }
                //if (idDeletes.Count <= 0) return;
                //string idDelete = string.Join(",", idDeletes);
                //string sql = $"UPDATE ProjectPartlistPriceRequest SET IsDeleted = 1 WHERE ID IN ({idDelete})";
                //TextUtils.ExcuteSQL(sql);

                LoadData();
            }
        }

        private void btnRequestApproved_Click_1(object sender, EventArgs e)
        {
            RequestApproved(true);
        }

        private void btnCancelRequestApproved_Click_1(object sender, EventArgs e)
        {
            RequestApproved(false);
        }

        private void btnShowPODetail_Click(object sender, EventArgs e)
        {
            var tabSelected = xtraTabControl1.SelectedTabPage;

            if (tabSelected.Controls.Count <= 0) return;
            GridControl gridControl = (GridControl)tabSelected.Controls[0];
            GridView gridView = gridControl.MainView as GridView;

            int poID = TextUtils.ToInt(gridView.GetFocusedRowCellValue("PONCCID"));
            //var poDetails = SQLHelper<PONCCDetailModel>.FindByAttribute("ProjectPartlistPurchaseRequestID", id);

            if (poID <= 0) return;
            PONCCModel po = SQLHelper<PONCCModel>.FindByID(poID);
            frmPONCCDetailNew frm = new frmPONCCDetailNew();
            frm.po = po;
            frm.Tag = po.BillCode;
            TextUtils.OpenChildForm(frm, null);
            //frm.FormClosed += Frm_FormClosed;
        }

        private void chkIsCommercialProduct_CheckedChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            var view = sender as GridView;
            if (view == null) return;

            if (e.RowHandle >= 0)
            {
                bool isDeleted = TextUtils.ToBoolean(view.GetRowCellValue(e.RowHandle, colIsDeleted));
                if (isDeleted)
                {
                    e.Appearance.BackColor = System.Drawing.Color.Red;
                    e.Appearance.ForeColor = System.Drawing.Color.White;
                    e.HighPriority = true;
                }
                else
                {
                    //var view = sender as GridView;
                    if (view.FocusedRowHandle == e.RowHandle)
                    {
                        e.Appearance.BackColor = System.Drawing.Color.LightYellow;
                        e.Appearance.ForeColor = System.Drawing.Color.Black;
                        //e.HighPriority = true;
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var tabSelected = xtraTabControl1.SelectedTabPage;

            if (tabSelected.Controls.Count <= 0) return;
            GridControl gridControl = (GridControl)tabSelected.Controls[0];
            GridView gridView = gridControl.MainView as GridView;
            try
            {
                int ID = TextUtils.ToInt(gridView.GetFocusedRowCellValue(colID.FieldName));
                if (ID == 0)
                {
                    MessageBox.Show("Vui lòng chọn yêu cầu muốn sửa!", "Thông báo");
                    return;
                }
                bool isCommercialProduct = TextUtils.ToBoolean(gridView.GetFocusedRowCellValue(colIsCommercialProduct.FieldName));
                int poNCC = TextUtils.ToInt(gridView.GetFocusedRowCellValue(colPONCCID.FieldName));
                if (!isCommercialProduct || poNCC > 0)
                {
                    MessageBox.Show("Sửa Y/C chỉ áp dụng với [Hàng thương mại] và yêu cầu [Chưa có PO]!", "Thông báo");
                    return;
                }



                int customerID = TextUtils.ToInt(gridView.GetFocusedRowCellValue(colCustomerID.FieldName));
                int quantity = TextUtils.ToInt(gridView.GetFocusedRowCellValue(colQuantity.FieldName));
                string statusRequest = TextUtils.ToString(gridView.GetFocusedRowCellValue(colStatusRequestText.FieldName));

                frmProjectPartlistPurchaseRequestDetail frm = new frmProjectPartlistPurchaseRequestDetail();
                frm.model = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByID(ID);
                frm.customerID = customerID;
                frm.quantity = quantity;
                frm.statusRequest = statusRequest;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        void CheckOrder(bool isCheckOrder)
        {
            var tabSelected = xtraTabControl1.SelectedTabPage;

            if (tabSelected.Controls.Count <= 0) return;
            GridControl gridControl = (GridControl)tabSelected.Controls[0];
            GridView gridView = gridControl.MainView as GridView;
            string isCheckOrderText = isCheckOrder ? "check" : "hủy check";
            int[] selectedRows = gridView.GetSelectedRows();
            if (selectedRows.Length <= 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm muốn check!", "Thông báo");
                return;
            }


            //Check Validate
            //foreach (var row in selectedRows)
            //{
            //    int id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
            //    if (id <= 0) continue;

            //    ProjectPartlistPurchaseRequestModel request = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByID(id);

            //    if (request.EmployeeIDRequestApproved != Global.EmployeeID)
            //    {

            //    }
            //}


            DialogResult dialogResult = MessageBox.Show($"Bạn có chắc muốn {isCheckOrderText} danh sách đang chọn không?\nNhững sản phẩm đã có NV mua check sẽ tự động được bỏ qua!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                List<int> listIDs = new List<int>();
                foreach (var row in selectedRows)
                {
                    int id = TextUtils.ToInt(gridView.GetRowCellValue(row, colID.FieldName));
                    if (id <= 0) continue;


                    ProjectPartlistPurchaseRequestModel request = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByID(id);
                    if (request.EmployeeIDRequestApproved != Global.EmployeeID && request.EmployeeIDRequestApproved > 0) continue;


                    listIDs.Add(id);
                }

                if (listIDs.Count <= 0) return;

                string idsText = string.Join(",", listIDs);
                var myDict = new Dictionary<string, object>()
                {
                    {"EmployeeIDRequestApproved",isCheckOrder ? Global.EmployeeID:0 },
                };

                var exp = new Expression("ID", idsText, "IN");
                var result = SQLHelper<ProjectPartlistPurchaseRequestModel>.UpdateFields(myDict, exp);

                if (result.IsSuccess) LoadData();
                else MessageBox.Show(result.ErrorText, "Thông báo");

            }
        }
        private void btnIsCheckOrder_Click(object sender, EventArgs e)
        {
            CheckOrder(true);
        }

        private void btnUnCheckOrder_Click(object sender, EventArgs e)
        {
            CheckOrder(false);
        }

        private void btnExportExcelAll_Click(object sender, EventArgs e)
        {
            ExportExcel(false);
        }

        private void btnExportExcelChoise_Click(object sender, EventArgs e)
        {
            ExportExcel(true);
        }

        void ExportExcel(bool choise)
        {
            try
            {
                var tab = xtraTabControl1.SelectedTabPage;
                if (tab == null) return;
                GridControl grdExport = (GridControl)tab.Controls[0];
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
                f.FileName = $"YeuCauMua_{tab.Text}{name}_{DateTime.Now.ToString("ddMMyy")}.xlsx";
                if (f.ShowDialog() == DialogResult.OK)
                {
                    string filepath = f.FileName;

                    XlsxExportOptions optionsEx = new XlsxExportOptions();
                    PrintingSystem printingSystem = new PrintingSystem();

                    PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                    if (choise) grdExport.DataSource = dtNew;
                    printableComponentLink1.Component = grdExport;


                    CompositeLink compositeLink = new CompositeLink(printingSystem);
                    compositeLink.Links.Add(printableComponentLink1);

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


        void CompleteRequestBuy(int status)
        {
            var tabSelected = xtraTabControl1.SelectedTabPage;

            if (tabSelected.Controls.Count <= 0) return;
            GridControl gridControl = (GridControl)tabSelected.Controls[0];
            GridView gridView = gridControl.MainView as GridView;

            string statusText = status == 7 ? "hoàn thành" : "hủy hoàn thành";
            int[] selectedRows = gridView.GetSelectedRows();
            if (selectedRows.Length <= 0)
            {
                MessageBox.Show($"Vui lòng chọn sản phẩm muốn {statusText} yêu cầu mua!", "Thông báo");
                return;
            }

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn {statusText} danh sách sản phẩm đã chọn không?\nNhững sản phẩm NV mua không phải bạn sẽ tự động được bỏ qua!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                dtPurchaseRequest.AcceptChanges();
                DataTable data = dtPurchaseRequest.Clone();
                foreach (int row in selectedRows)
                {
                    int id = TextUtils.ToInt(gridView.GetRowCellValue(row, colID.FieldName));
                    if (id <= 0) continue;

                    ProjectPartlistPurchaseRequestModel request = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByID(id);
                    if (request.EmployeeIDRequestApproved != Global.EmployeeID && !isAdmin) continue;

                    if (row < 0) continue;

                    DataRow dataRow = data.NewRow();

                    dataRow = gridView.GetDataRow(row);
                    data.ImportRow(dataRow);
                }
                UpdateData(data);

                List<int> listId = new List<int>();
                foreach (int row in selectedRows)
                {
                    int id = TextUtils.ToInt(gridView.GetRowCellValue(row, colID.FieldName));
                    if (id <= 0) continue;

                    ProjectPartlistPurchaseRequestModel request = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByID(id);
                    if (request.EmployeeIDRequestApproved != Global.EmployeeID && !isAdmin) continue;

                    listId.Add(id);
                }

                if (listId.Count <= 0) return;

                var myDict = new Dictionary<string, object>()
                {
                    { "EmployeeIDRequestApproved",Global.EmployeeID},
                    { "StatusRequest",status},
                    { "UpdatedDate",DateTime.Now},
                    { "UpdatedBy",Global.AppCodeName},
                };

                var exp = new Expression("ID", string.Join(",", listId), "IN");
                SQLHelper<ProjectPartlistPurchaseRequestModel>.UpdateFields(myDict, exp);

                LoadData();
            }


        }


        void KeepProduct(bool isKeep)
        {
            string isKeepText = isKeep == true ? "giữ hàng" : "hủy giữ hàng";
            int[] selectedRows = grvData.GetSelectedRows();
            if (selectedRows.Length <= 0)
            {
                MessageBox.Show($"Vui lòng chọn sản phẩm muốn {isKeepText}!", "Thông báo");
                return;
            }

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn {isKeepText} danh sách sản phẩm đã chọn không?\nNhững sản phẩm NV mua không phải bạn sẽ tự động được bỏ qua!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                foreach (int row in selectedRows)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
                    if (id <= 0) continue;

                    int productID = TextUtils.ToInt(grvData.GetRowCellValue(row, colProductSaleID.FieldName));
                    int projectID = TextUtils.ToInt(grvData.GetRowCellValue(row, colProjectID.FieldName));
                    if (productID <= 0 /*|| projectID <= 0*/) continue;

                    //check sl tồn kho
                    DataTable dt = TextUtils.LoadDataFromSP("spGetInventory", "A", new string[] { "@ProductSaleID" }, new object[] { productID });
                    if (dt.Rows.Count <= 0) continue;
                    decimal totalQuantityLast = TextUtils.ToDecimal(dt.Rows[0]["TotalQuantityLast"]);
                    if (totalQuantityLast <= 0) continue;

                    int inventoryProjectID = TextUtils.ToInt(grvData.GetRowCellValue(row, colInventoryProjectID.FieldName));
                    //int inventoryProjectIDq = TextUtils.ToInt(grvData.GetRowCellValue(18, colInventoryProjectID));

                    ProjectPartlistPurchaseRequestModel request = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByID(id);
                    if (request.EmployeeIDRequestApproved != Global.EmployeeID && !isAdmin) continue;

                    InventoryProjectModel inventoryProject = SQLHelper<InventoryProjectModel>.FindByID(inventoryProjectID);
                    inventoryProject.ProjectID = projectID;
                    inventoryProject.ProductSaleID = productID;
                    inventoryProject.EmployeeID = request.EmployeeIDRequestApproved;
                    inventoryProject.WarehouseID = 1;
                    inventoryProject.Quantity = TextUtils.ToDecimal(grvData.GetRowCellValue(row, colQuantity.FieldName));

                    inventoryProject.CustomerID = TextUtils.ToInt(grvData.GetRowCellValue(row, colCustomerID.FieldName));
                    inventoryProject.POKHDetailID = TextUtils.ToInt(grvData.GetRowCellValue(row, colPOKHDetailID.FieldName));

                    if (inventoryProject.ID <= 0)
                    {
                        inventoryProject.ID = SQLHelper<InventoryProjectModel>.Insert(inventoryProject).ID;
                    }
                    else
                    {
                        SQLHelper<InventoryProjectModel>.Update(inventoryProject);
                    }

                    request.InventoryProjectID = inventoryProject.ID;
                    SQLHelper<ProjectPartlistPurchaseRequestModel>.Update(request);
                    grvData.SetRowCellValue(row, colInventoryProjectID.FieldName, inventoryProject.ID);

                }

            }
        }
        private void btnCompleteRequestBuy_Click(object sender, EventArgs e)
        {
            CompleteRequestBuy(7);
        }

        private void btnUnCompleteRequestBuy_Click(object sender, EventArgs e)
        {
            CompleteRequestBuy(1);
        }

        private void btnKeepProduct_Click(object sender, EventArgs e)
        {
            KeepProduct(true);
        }

        private void btnUnKeepProduct_Click(object sender, EventArgs e)
        {

        }

        private void cboProductRTC_EditValueChanged(object sender, EventArgs e)
        {
            //LoadData();
        }
        //ndNhat update 26/03/2025
        private void repositoryItemSearchLookUpEdit8_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit lookUpEdit = sender as SearchLookUpEdit;
            CurrencyModel model = lookUpEdit.GetSelectedDataRow() as CurrencyModel;
            if (model == null) return;
            int rowHandle = grvProductRTC.FocusedRowHandle;
            grvProductRTC.SetRowCellValue(rowHandle, colCurrencyRate2, model.CurrencyRate);
            grvProductRTC.SetRowCellValue(rowHandle, colCurrencyID2, model.ID);

        }

        private void repositoryItemSearchLookUpEdit5_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit lookUpEdit = sender as SearchLookUpEdit;
            SupplierSaleModel model = lookUpEdit.GetSelectedDataRow() as SupplierSaleModel;
            if (model == null) return;
            int rowHandle = grvProductRTC.FocusedRowHandle;
            grvProductRTC.SetRowCellValue(rowHandle, colSupplierSaleID2, model.ID);
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
                foreach (int row in rowSelecteds)
                {
                    try
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
                    catch (Exception ex)
                    {
                        continue;
                        //MessageBox.Show($"File [{productCode}.pdf] không tồn tại!\r\n{ex.ToString()}", "Thông báo");
                    }

                }

            }
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void cboCurrencyDemo_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit lookUpEdit = (SearchLookUpEdit)sender;
            CurrencyModel currency = (CurrencyModel)lookUpEdit.GetSelectedDataRow() ?? new CurrencyModel();

            bool isExpried = true;
            if (currency.DateExpried.HasValue && currency.DateStart.HasValue && !string.IsNullOrWhiteSpace(currency.Code))
            {
                //validate ngày bắt đầu < ngày hết hạn
                isExpried = ((currency.DateExpried.Value.Date < DateTime.Now.Date.Date || currency.DateStart.Value.Date > DateTime.Now.Date) && currency.Code.ToLower().Trim() != "vnd");
            }
            int[] rowSelecteds = grvBorrowProduct.GetSelectedRows();
            if (rowSelecteds.Length <= 0)
            {
                grvBorrowProduct.SetRowCellValue(grvBorrowProduct.FocusedRowHandle, colCurrencyID, currency.ID);
                grvBorrowProduct.SetRowCellValue(grvBorrowProduct.FocusedRowHandle, colCurrencyRate, currency.CurrencyRate);
                if (isExpried) grvBorrowProduct.SetRowCellValue(grvBorrowProduct.FocusedRowHandle, colCurrencyRate, 0);
                grvBorrowProduct.SetRowCellValue(grvBorrowProduct.FocusedRowHandle, colTotalPriceExchange, CalculatorTotalMoneyExchange(grvBorrowProduct.FocusedRowHandle));
                //grvData.SetRowCellValue(grvData.FocusedRowHandle, colStatusUpdate, 2);
            }
            else
            {
                using (WaitDialogForm fWait = new WaitDialogForm())
                {
                    foreach (int row in rowSelecteds)
                    {
                        grvBorrowProduct.SetRowCellValue(row, colCurrencyID, currency.ID);
                        grvBorrowProduct.SetRowCellValue(row, colCurrencyRate, currency.CurrencyRate);
                        if (isExpried) grvBorrowProduct.SetRowCellValue(row, colCurrencyRate, 0);
                        grvBorrowProduct.SetRowCellValue(row, colTotalPriceExchange, CalculatorTotalMoneyExchange(row));
                        //grvData.SetRowCellValue(row, colStatusUpdate, 2);
                    }
                }
            }
        }

        private void cboSupplierSaleDemo_EditValueChanged(object sender, EventArgs e)
        {
            //LoadData();
        }

        private void grvBorrowProduct_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //if (grvBorrowProduct.FocusedColumn.FieldName == colCurrencyID.FieldName) return;
            if (isRecallCellValueChanged == true) return;
            try
            {
                if (e.Column.FieldName != colProductRTCID.FieldName && e.Column.FieldName != "ProductCodeRTC")
                {
                    using (WaitDialogForm fWait = new WaitDialogForm())
                    {
                        grvBorrowProduct.CloseEditor();
                        isRecallCellValueChanged = true;
                        if (grvBorrowProduct.SelectedRowsCount > 0)
                        {
                            if (e.Value == null) return;
                            foreach (int row in grvBorrowProduct.GetSelectedRows())
                            {
                                if (e.Column.FieldName == colProductSaleID.FieldName) continue;
                                if (e.Column.FieldName == colProductNewCode.FieldName) continue;
                                if (e.Column.FieldName == colInventoryProjectID.FieldName) continue;
                                grvBorrowProduct.SetRowCellValue(row, grvBorrowProduct.Columns[e.Column.FieldName], e.Value);


                                if (grvBorrowProduct.FocusedColumn.FieldName != colUnitPrice.FieldName
                                    && grvBorrowProduct.FocusedColumn.FieldName != colUnitImportPrice.FieldName
                                    && grvBorrowProduct.FocusedColumn.FieldName != colVAT.FieldName)
                                {
                                    continue;
                                }

                                UpdateValue(row);
                            }
                        }
                        else
                        {
                            UpdateValue(grvBorrowProduct.FocusedRowHandle);
                        }


                    }
                }
            }
            finally
            {
                isRecallCellValueChanged = false;
            }
        }

        private void grvProductRTC_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //if (grvProductRTC.FocusedColumn.FieldName == colCurrencyID.FieldName) return;
            if (isRecallCellValueChanged == true) return;
            try
            {
                if (e.Column.FieldName != colProductSaleID.FieldName && e.Column.FieldName != colProductNewCode.FieldName)
                {
                    using (WaitDialogForm fWait = new WaitDialogForm())
                    {
                        grvProductRTC.CloseEditor();
                        isRecallCellValueChanged = true;
                        if (grvProductRTC.SelectedRowsCount > 0)
                        {
                            if (e.Value == null) return;
                            foreach (int row in grvProductRTC.GetSelectedRows())
                            {
                                if (e.Column.FieldName == colProductSaleID.FieldName) continue;
                                if (e.Column.FieldName == colProductNewCode.FieldName) continue;
                                if (e.Column.FieldName == colInventoryProjectID.FieldName) continue;
                                grvProductRTC.SetRowCellValue(row, grvProductRTC.Columns[e.Column.FieldName], e.Value);


                                if (grvProductRTC.FocusedColumn.FieldName != colUnitPrice.FieldName
                                    && grvProductRTC.FocusedColumn.FieldName != colUnitImportPrice.FieldName
                                    && grvProductRTC.FocusedColumn.FieldName != colVAT.FieldName)
                                {
                                    continue;
                                }

                                UpdateValue(row);


                            }
                        }
                        else
                        {
                            UpdateValue(grvProductRTC.FocusedRowHandle);
                        }


                    }
                }
            }
            finally
            {
                isRecallCellValueChanged = false;
            }
        }

        private void btnUpdateProductCodeRTC_Click(object sender, EventArgs e)
        {
            var tabSelected = xtraTabControl1.SelectedTabPage;

            if (tabSelected.Controls.Count <= 0) return;
            GridControl gridControl = (GridControl)tabSelected.Controls[0];
            GridView gridView = gridControl.MainView as GridView;


            gridView.FocusedRowHandle = -1;
            gridView.CloseEditor();

            using (WaitDialogForm fWait = new WaitDialogForm())
            {
                for (int i = 0; i < gridView.RowCount; i++)
                {
                    int id = TextUtils.ToInt(gridView.GetRowCellValue(i, colID));
                    if (id <= 0) continue;

                    ProjectPartlistPurchaseRequestModel request = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByID(id);
                    if (request.EmployeeIDRequestApproved != Global.EmployeeID && !isAdmin) continue;

                    int productGroupId = TextUtils.ToInt(gridView.GetRowCellValue(i, colProductGroupID));
                    int productGroupRTCId = TextUtils.ToInt(gridView.GetRowCellValue(i, colProductGroupRTCID));

                    if (productGroupId <= 0 && productGroupRTCId <= 0) continue;
                    string productCode = TextUtils.ToString(gridView.GetRowCellValue(i, colProductCode)).Trim();
                    //var exp1 = new Expression("ProductGroupID", productGroupId);
                    var exp2 = new Expression("ProductCode", productCode);
                    var exp3 = new Expression("IsDeleted", 0);

                    //if (productGroupId > 0 && gridView.Name == grvData.Name)//Insert vào kho Sale
                    //{
                    //    ProductSaleModel productSale = SQLHelper<ProductSaleModel>.FindByExpression(exp1.And(exp2).And(exp3)).FirstOrDefault();

                    //    productSale = productSale ?? new ProductSaleModel();

                    //    if (productSale.ID > 0)
                    //    {
                    //        //productSale.ProductNewCode = LoadNewCode(productGroupId);
                    //        //SQLHelper<ProductSaleModel>.Update(productSale);
                    //    }
                    //    else
                    //    {
                    //        productSale.ProductCode = productCode;
                    //        productSale.ProductName = TextUtils.ToString(gridView.GetRowCellValue(i, colProductName)).Trim();
                    //        productSale.Unit = TextUtils.ToString(gridView.GetRowCellValue(i, colUnitName)).Trim(); productSale.ProductGroupID = productGroupId;
                    //        productSale.ProductNewCode = LoadNewCode(productGroupId);
                    //        productSale.ID = SQLHelper<ProductSaleModel>.Insert(productSale).ID;
                    //    }

                    //    gridView.SetRowCellValue(i, colProductSaleID, productSale.ID);
                    //    gridView.SetRowCellValue(i, colProductNewCode, productSale.ProductNewCode);
                    //}
                    if (productGroupRTCId > 0 && gridView.Name == grvBorrowProduct.Name) //Insert vào kho Demo
                    {
                        //exp1 = new Expression(ProductRTCModel_Enum.ProductGroupRTCID, productGroupRTCId);

                        ProductRTCModel productRTC = SQLHelper<ProductRTCModel>.FindByExpression(exp2).FirstOrDefault();

                        productRTC = productRTC ?? new ProductRTCModel();
                        productRTC.ProductCode = productCode;
                        productRTC.ProductName = TextUtils.ToString(gridView.GetRowCellValue(i, colProductName)).Trim();

                        string unitName = TextUtils.ToString(gridView.GetRowCellValue(i, colUnitName)).Trim();
                        string firmName = TextUtils.ToString(gridView.GetRowCellValue(i, "Manufacturer")).Trim();
                        UnitCountKTModel unitCountKT = SQLHelper<UnitCountKTModel>.FindByAttribute("UnitCountName", unitName).FirstOrDefault() ?? new UnitCountKTModel();
                        FirmModel firm = SQLHelper<FirmModel>.FindByExpression(new Expression(FirmModel_Enum.FirmCode.ToString(), firmName)
                                                                                                    .And(new Expression(FirmModel_Enum.FirmType.ToString(), 2)))
                                                                .FirstOrDefault() ?? new FirmModel();
                        productRTC.UnitCountID = unitCountKT.ID;
                        productRTC.ProductGroupRTCID = productGroupRTCId;
                        productRTC.Maker = firmName;
                        productRTC.FirmID = firm.ID;
                        productRTC.CreateDate = DateTime.Now;
                        if (productRTC.ID > 0)
                        {
                            //SQLHelper<ProductSaleModel>.Update(productSale);
                        }
                        else
                        {
                            productRTC.ProductCodeRTC = GetProductCodeRTC();
                            productRTC.ID = SQLHelper<ProductRTCModel>.Insert(productRTC).ID;
                        }

                        gridView.SetRowCellValue(i, colProductRTCID.FieldName, productRTC.ID);
                        gridView.SetRowCellValue(i, "ProductCodeRTC", productRTC.ProductCodeRTC);


                        request.ProductRTCID = productRTC.ID;
                        SQLHelper<ProjectPartlistPurchaseRequestModel>.Update(request);
                    }
                }

                dtProductRTCBorrow.AcceptChanges();
            }
        }

        private void contextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var tabSelected = xtraTabControl1.SelectedTabPage;

            if (tabSelected.Controls.Count <= 0) return;
            GridControl gridControl = (GridControl)tabSelected.Controls[0];
            GridView gridView = gridControl.MainView as GridView;


            btnUpdateProductCodeRTC.Visible = gridView.Name == grvBorrowProduct.Name;
        }

        private void grvProductMarketing_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            GridView gridView = (GridView)sender;
            if (gridView == null) return;

            if (isRecallCellValueChanged == true) return;
            try
            {
                if (e.Column.FieldName != colProductSaleID.FieldName && e.Column.FieldName != colProductNewCode.FieldName)
                {
                    using (WaitDialogForm fWait = new WaitDialogForm())
                    {
                        gridView.CloseEditor();
                        isRecallCellValueChanged = true;
                        if (gridView.SelectedRowsCount > 0)
                        {
                            if (e.Value == null) return;
                            foreach (int row in gridView.GetSelectedRows())
                            {
                                if (e.Column.FieldName == colProductSaleID.FieldName) continue;
                                if (e.Column.FieldName == colProductNewCode.FieldName) continue;
                                if (e.Column.FieldName == colInventoryProjectID.FieldName) continue;
                                gridView.SetRowCellValue(row, gridView.Columns[e.Column.FieldName], e.Value);

                                if (gridView.FocusedColumn.FieldName != colUnitPrice.FieldName
                                    && gridView.FocusedColumn.FieldName != colUnitImportPrice.FieldName
                                    && gridView.FocusedColumn.FieldName != colVAT.FieldName)
                                {
                                    continue;
                                }

                                UpdateValue(row);


                            }
                        }
                        else
                        {
                            UpdateValue(gridView.FocusedRowHandle);
                        }


                    }
                }
            }
            finally
            {
                isRecallCellValueChanged = false;
            }
        }

        private void cboCurrencyMKT_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit lookUpEdit = (SearchLookUpEdit)sender;
            CurrencyModel currency = (CurrencyModel)lookUpEdit.GetSelectedDataRow() ?? new CurrencyModel();

            bool isExpried = true;
            if (currency.DateExpried.HasValue && currency.DateStart.HasValue && !string.IsNullOrWhiteSpace(currency.Code))
            {
                //validate ngày bắt đầu < ngày hết hạn
                isExpried = ((currency.DateExpried.Value.Date < DateTime.Now.Date.Date || currency.DateStart.Value.Date > DateTime.Now.Date) && currency.Code.ToLower().Trim() != "vnd");
            }

            int[] rowSelecteds = grvProductMarketing.GetSelectedRows();
            if (rowSelecteds.Length <= 0)
            {
                grvProductMarketing.SetRowCellValue(grvProductMarketing.FocusedRowHandle, colCurrencyID, currency.ID);
                grvProductMarketing.SetRowCellValue(grvProductMarketing.FocusedRowHandle, colCurrencyRate, currency.CurrencyRate);
                if (isExpried) grvProductMarketing.SetRowCellValue(grvProductMarketing.FocusedRowHandle, colCurrencyRate, 0);
                grvProductMarketing.SetRowCellValue(grvProductMarketing.FocusedRowHandle, colTotalPriceExchange, CalculatorTotalMoneyExchange(grvProductMarketing.FocusedRowHandle));
            }
            else
            {
                using (WaitDialogForm fWait = new WaitDialogForm())
                {
                    foreach (int row in rowSelecteds)
                    {
                        grvProductMarketing.SetRowCellValue(row, colCurrencyID, currency.ID);
                        grvProductMarketing.SetRowCellValue(row, colCurrencyRate, currency.CurrencyRate);
                        if (isExpried) grvProductMarketing.SetRowCellValue(row, colCurrencyRate, 0);
                        grvProductMarketing.SetRowCellValue(row, colTotalPriceExchange, CalculatorTotalMoneyExchange(row));
                    }
                }
            }
        }


        #region Nhật update check validate bắt buộc có hãng khi tạo mã sp kho vision (19/07/2025)
        const int PRODUC_GROUP_TVISION = 4;
        bool validateManufacturer(GridView gridView)
        {
            for (int i = 0; i < gridView.RowCount; i++)
            {
                string manufacturer = TextUtils.ToString(gridView.GetRowCellValue(i, colManufacturer.FieldName)).Trim();
                int productGroupID = TextUtils.ToInt(gridView.GetRowCellValue(i, colProductGroupID.FieldName));
                string tt = TextUtils.ToString(gridView.GetRowCellValue(i, colTT.FieldName)).Trim();
                string productCode = TextUtils.ToString(gridView.GetRowCellValue(i, colProductCode.FieldName)).Trim();
                int productSaleID = TextUtils.ToInt(gridView.GetRowCellValue(i, colProductSaleID.FieldName));
                if (productSaleID <= 0)
                {
                    if (string.IsNullOrEmpty(manufacturer) && productGroupID == PRODUC_GROUP_TVISION)
                    {
                        MessageBox.Show($"Yêu cầu mua hàng kho vision có mã sản phẩm {productCode} ở vị trí {tt} phải có hãng!", "Thông báo");
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion


        #region Nhật update check cho sửa SL khi Duplicate (18/07/2025)
        void Duplicate()
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            int duplicateID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colDuplicateID));
            decimal quantity = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colOriginQuantity));
            if (id <= 0) return;

            string productCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductCode));
            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn duplicate yêu cầu mua vật tư [{productCode}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog != DialogResult.Yes) return;

            ProjectPartlistPurchaseRequestModel purchase = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByID(id);
            if (purchase == null || purchase.ID <= 0) return;

            // Store the original row handle for later use
            int originalRowHandle = grvData.FocusedRowHandle;

            // Create a new record for duplication
            purchase.ID = 0;
            if (duplicateID > 0)
            {
                purchase.DuplicateID = duplicateID;
                purchase.OriginQuantity = quantity;
            }
            else
            {
                purchase.DuplicateID = id;
                purchase.OriginQuantity = purchase.Quantity;
            }
            //purchase.OriginQuantity = purchase.Quantity;
            purchase.Quantity = 0;

            //purchase.DuplicateID = id;


            SQLHelper<ProjectPartlistPurchaseRequestModel>.Insert(purchase);
            Dictionary<string, object> updateFields = new Dictionary<string, object>
            {
                { ProjectPartlistPurchaseRequestModel_Enum.DuplicateID.ToString(), purchase.DuplicateID },
                { ProjectPartlistPurchaseRequestModel_Enum.OriginQuantity.ToString(), purchase.OriginQuantity },
                {ProjectPartlistPurchaseRequestModel_Enum.UpdatedDate.ToString(), DateTime.Now},
                {ProjectPartlistPurchaseRequestModel_Enum.UpdatedBy.ToString(), Global.AppCodeName}
            };
            SQLHelper<ProjectPartlistPurchaseRequestModel>.UpdateFieldsByID(updateFields, id);
        }

        bool checkValidate()
        {
            var gridView = (xtraTabControl1.SelectedTabPage.Controls[0] as GridControl).MainView as GridView;

            // Kiểm tra nếu lưới rỗng
            if (gridView.RowCount == 0)
            {
                MessageBox.Show("Lưới không có dữ liệu để kiểm tra!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Thu thập tất cả các DuplicateID duy nhất
            var duplicateIDs = new List<int>();
            for (int i = 0; i < gridView.RowCount; i++)
            {
                int duplicateID = TextUtils.ToInt(gridView.GetRowCellValue(i, "DuplicateID"));
                if (duplicateID > 0 && !duplicateIDs.Contains(duplicateID))
                {
                    duplicateIDs.Add(duplicateID);
                }
            }

            // Nếu không có DuplicateID nào, trả về true
            if (duplicateIDs.Count == 0)
            {
                return true;
            }

            // Kiểm tra từng nhóm DuplicateID
            foreach (int duplicateID in duplicateIDs)
            {
                // Lấy OriginQuantity từ cột OriginQuantity của một dòng có DuplicateID phù hợp
                decimal originQuantity = 0;
                bool foundOrigin = false;
                for (int i = 0; i < gridView.RowCount; i++)
                {
                    if (TextUtils.ToInt(gridView.GetRowCellValue(i, "DuplicateID")) == duplicateID)
                    {
                        originQuantity = TextUtils.ToDecimal(gridView.GetRowCellValue(i, "OriginQuantity"));
                        foundOrigin = true;
                        break; // Lấy giá trị OriginQuantity từ dòng đầu tiên tìm thấy
                    }
                }

                if (!foundOrigin)
                {
                    MessageBox.Show($"Không tìm thấy OriginQuantity cho DuplicateID {duplicateID}!",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // Tính tổng Quantity của các dòng có DuplicateID này
                decimal totalQuantity = 0;
                for (int i = 0; i < gridView.RowCount; i++)
                {
                    if (TextUtils.ToInt(gridView.GetRowCellValue(i, "DuplicateID")) == duplicateID)
                    {
                        totalQuantity += TextUtils.ToDecimal(gridView.GetRowCellValue(i, "Quantity"));
                    }
                }

                // Kiểm tra tổng Quantity có bằng OriginQuantity hay không
                if (totalQuantity != originQuantity)
                {
                    MessageBox.Show($"Tổng Quantity ({totalQuantity}) cho DuplicateID {duplicateID} không khớp với OriginQuantity ({originQuantity})!",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }
        #endregion

        private void btnUpdateRequestType_Click(object sender, EventArgs e)
        {
            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang load dữ liệu"))
            {
                var items = new List<(int, int)>();

                int typeData = chkIsCommercialProduct.Checked ? 5 : (chkIsJobRequirement.Checked ? 6 : 1);
                items.AddRange(GetIDsFromGrid(grvData, typeData));
                items.AddRange(GetIDsFromGrid(grvBorrowProduct, 4));
                items.AddRange(GetIDsFromGrid(grvProductRTC, 3));
                items.AddRange(GetIDsFromGrid(gridView6, 2));
                items.AddRange(GetIDsFromGrid(grvProductMarketing, 7));

                UpdateRequestType(items);
            }

        }

        private void UpdateRequestType(List<(int ID, int TypeID)> items)
        {
            var grouped = items.GroupBy(x => x.TypeID);

            foreach (var g in grouped)
            {
                Dictionary<string, object> updateFields = new Dictionary<string, object>
                {
                    { ProjectPartlistPurchaseRequestModel_Enum.ProjectPartlistPurchaseRequestTypeID.ToString(), g.Key }
                };

                var exp = new Expression("ID", string.Join(",", g.Select(x => x.ID)), "IN");
                SQLHelper<ProjectPartlistPurchaseRequestModel>.UpdateFields(updateFields, exp);
            }
        }

        private List<(int, int)> GetIDsFromGrid(GridView gridView, int typeID)
        {
            var list = new List<(int, int)>();
            for (int i = 0; i < gridView.RowCount; i++)
            {
                int val = TextUtils.ToInt(gridView.GetRowCellValue(i, "ID"));
                if (val > 0)
                    list.Add((val, typeID));
            }
            return list;
        }

    }
}
