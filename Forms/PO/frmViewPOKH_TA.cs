using BMS;
using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DocumentFormat.OpenXml.Office2010.Excel;
using Forms.Sale.HandoverMinutes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using Color = System.Drawing.Color;

namespace Forms.PO
{
    public partial class frmViewPOKH_TA : _Forms
    {
        private WarehouseModel _warehouse = new WarehouseModel();

        List<int> listSelected = new List<int>(); // NTA B - update 15/09/25


        DataTable dt = new DataTable();           // bảng cha
        DataTable dtExport = new DataTable();         // bảng con 1
        DataTable dtInvoice = new DataTable();         // bảng con 2

        public frmViewPOKH_TA(int warehouseID)
        {
            InitializeComponent();
            _warehouse = SQLHelper<WarehouseModel>.FindByID(warehouseID);
        }

        private void frmViewPOKH_Load(object sender, EventArgs e)
        {
            this.Text = $"CHI TIẾT PO KHÁCH HÀNG - {_warehouse.WarehouseCode}";
            DateTime datenow = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, dtpStartDate.Value.Day, 0, 0, 0);
            dtpStartDate.Value = datenow.AddMonths(-1);
            loadcbColor();
            txtPageNumber.Text = "1";
            loadgroupSale();
            loadCustomer();
            loadUser();
            loadMainIndex();

            LoadEmployeeTeamSale();
            loadPOKH();

            //NTA B update IsSelected sorting 161025
            //RepositoryItemCheckEdit check = new RepositoryItemCheckEdit();
            //colIsSelected.ColumnEdit = check;
            //colIsSelected.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.Value;
            //colIsSelected.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            //END NTA B update


            grdMaster.ContextMenuStrip = contextMenuStrip1;
        }
        void loadgroupSale()
        {
            DataTable dt = TextUtils.Select("Select ID,[GroupSalesName] From [GroupSales] ");
            cbGroup.Properties.DisplayMember = "GroupSalesName";
            cbGroup.Properties.ValueMember = "ID";
            cbGroup.Properties.DataSource = dt;
        }

        /// <summary>
        /// load khách hàng
        /// </summary>
        void loadCustomer()
        {
            DataTable dt = TextUtils.Select("SELECT ID,CustomerName FROM dbo.Customer where IsDeleted <> 1 Order By CreatedDate DESC");
            cbCustomer.Properties.DisplayMember = "CustomerName";
            cbCustomer.Properties.ValueMember = "ID";
            cbCustomer.Properties.DataSource = dt;
        }

        /// <summary>
        /// load ng phụ trách
        /// </summary>
        void loadUser()
        {
            //DataTable dt = TextUtils.Select("SELECT ID,FullName FROM dbo.Users");

            var users = SQLHelper<EmployeeModel>.FindByExpression(new Expression("UserID", 0, "<>"));
            cbUser.Properties.DisplayMember = "FullName";
            //cbUser.Properties.ValueMember = "ID";
            cbUser.Properties.ValueMember = "UserID";
            cbUser.Properties.DataSource = users;

        }
        void loadcbColor()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            for (int i = 0; i < 5; i++)
            {
                dt.Rows.Add(i);
            }

            cbColor.Properties.DisplayMember = "ID";
            cbColor.Properties.ValueMember = "ID";
            cbColor.Properties.DataSource = dt;
        }
        /// <summary>
        /// load type
        /// </summary>
        public void loadMainIndex()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM MainIndex where ID IN(8,9,10,11,12,13)");
            cbStatus.Properties.DisplayMember = "MainIndex";
            cbStatus.Properties.ValueMember = "ID";
            cbStatus.Properties.DataSource = dt;
        }


        void LoadEmployeeTeamSale()
        {
            List<EmployeeTeamSaleModel> teams = SQLHelper<EmployeeTeamSaleModel>.FindByAttribute(EmployeeTeamSaleModel_Enum.ParentID.ToString(), 0);

            cboEmployeeTeamSale.Properties.DisplayMember = "Name";
            cboEmployeeTeamSale.Properties.ValueMember = "ID";
            cboEmployeeTeamSale.Properties.DataSource = teams;
        }

        /// <summary>
        /// load POKH
        /// </summary>
        private void loadPOKH()
        {
            try
            {
                DateTime dateTimeS = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, dtpStartDate.Value.Day, 0, 0, 0);
                DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);

                //DataSet ds = TextUtils.LoadDataSetFromSP("spGetViewPOKH"
                //         , new string[] { "@PageNumber", "@PageSize", "@FilterText", "@CustomerID", "@UserID", "@POType", "@Status", "@Group", "@StartDate", "@EndDate" }
                //         , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value),
                //        txtFilterText.Text.Trim(), TextUtils.ToInt(cbCustomer.EditValue), TextUtils.ToInt(cbUser.EditValue), TextUtils.ToInt(cbStatus.EditValue),TextUtils.ToInt(cbColor.EditValue),TextUtils.ToInt( cbGroup.EditValue),dateTimeS,dateTimeE});

                int employeeTeamSaleID = TextUtils.ToInt(cboEmployeeTeamSale.EditValue);
                int userID = TextUtils.ToInt(cbUser.EditValue);
                int poType = TextUtils.ToInt(cbStatus.EditValue);
                int status = TextUtils.ToInt(cbColor.EditValue);
                int customerID = TextUtils.ToInt(cbCustomer.EditValue);

                using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
                {


                    //updateN xóa datatable cũ 
                    DataSet ds = TextUtils.LoadDataSetFromSP("spGetViewPOKHDetail",
                                                new string[] { "@DateStart", "@DateEnd", "@EmployeeTeamSaleID", "@UserID", "@POType", "@Status", "@CustomerID", "@Keyword", "@WarehouseID" },
                                                new object[] { dateTimeS, dateTimeE, employeeTeamSaleID, userID, poType, status, customerID, txtFilterText.Text.Trim(), _warehouse.ID });


                    dt = ds.Tables[0];
                    dtExport = ds.Tables[1];
                    dtInvoice = ds.Tables[2];

                    grdMaster.DataSource = dt;
                    grvMaster.ExpandAllGroups();

                    //updateN bỏ comment
                    if (dtExport.Rows.Count > 0)
                    {
                        //txtTotalPage.Text = TextUtils.ToString(ds.Tables[0].Rows[0]["TotalPage"]);
                        DataColumn keyColumn = dt.Columns["ID"];
                        DataColumn foreignKeyColumn = dtExport.Columns["POKHDetailID"];
                        dt.PrimaryKey = new[] { dt.Columns["ID"] };
                        ds.Relations.Add("grvDetail", keyColumn, foreignKeyColumn, false);
                        //grdMaster.DataSource = ds.Tables[0];
                        //grvMaster.ExpandAllGroups();
                        grdMaster.LevelTree.Nodes.Add("grvDetail", grvDetail);
                        grvDetail.ViewCaption = "XUẤT KHO";
                    }

                    if (dtInvoice.Rows.Count > 0)
                    {
                        DataColumn keyColumn2 = dt.Columns["ID"];
                        DataColumn foreignKeyColumn2 = dtInvoice.Columns["POKHDetailID"];
                        dt.PrimaryKey = new[] { dt.Columns["ID"] };
                        ds.Relations.Add("grvDataRequestInvoice", keyColumn2, foreignKeyColumn2, false);
                        //grdMaster.DataSource = ds.Tables[0];
                        //grvMaster.ExpandAllGroups();
                        grdMaster.LevelTree.Nodes.Add("grvDataRequestInvoice", grvDataRequestInvoice);
                        grvDataRequestInvoice.ViewCaption = "YÊU CẦU XUẤT HÓA ĐƠN";
                    }
                    
                    //{
                    //    //grdMaster.DataSource = ds.Tables[0];
                    //    //grdMaster.DataSource = dt; UpdateN
                    //    grdMaster.DataSource = ds.Tables[0];
                    //    grvMaster.ExpandAllGroups();
                    //}


                   
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadPOKH();
        }

        private void grvMaster_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            int status = TextUtils.ToInt(grvMaster.GetRowCellValue(e.RowHandle, colColorStatus));
            if (e.Column == colStatusText)
            {
                switch (status)
                {
                    case 0:
                        e.Appearance.BackColor = Color.FromArgb(255, 255, 0);
                        break;
                    case 1:
                        break;
                    case 2:
                        e.Appearance.BackColor = Color.FromArgb(244, 176, 132);
                        break;
                    case 3:
                        e.Appearance.BackColor = Color.FromArgb(155, 194, 230);
                        break;
                    case 4:
                        e.Appearance.BackColor = Color.FromArgb(169, 208, 142);
                        break;
                    default:
                        break;
                }
            }
        }

        private void cbvColor_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            int status = TextUtils.ToInt(cbvColor.GetRowCellValue(e.RowHandle, colColor));

            switch (status)
            {
                case 0:
                    e.Appearance.BackColor = Color.FromArgb(255, 255, 0);
                    break;
                case 1:
                    break;
                case 2:
                    e.Appearance.BackColor = Color.FromArgb(244, 176, 132);
                    break;
                case 3:
                    e.Appearance.BackColor = Color.FromArgb(155, 194, 230);
                    break;
                case 4:
                    e.Appearance.BackColor = Color.FromArgb(169, 208, 142);
                    break;
                default:
                    break;

            }
        }

        private void cbColor_EditValueChanged(object sender, EventArgs e)
        {


            int color = TextUtils.ToInt(cbColor.EditValue);
            switch (color)
            {
                case 0:
                    cbColor.BackColor = Color.FromArgb(255, 255, 0);
                    break;
                case 1:
                    cbColor.BackColor = Color.FromArgb(255, 255, 255);
                    break;
                case 2:
                    cbColor.BackColor = Color.FromArgb(244, 176, 132);
                    break;
                case 3:
                    cbColor.BackColor = Color.FromArgb(155, 194, 230);
                    break;
                case 4:
                    cbColor.BackColor = Color.FromArgb(169, 208, 142);
                    break;
                default:
                    break;
            }
            loadPOKH();
        }

        private void cbStatus_EditValueChanged(object sender, EventArgs e)
        {
            loadPOKH();
        }

        private void cbGroup_EditValueChanged(object sender, EventArgs e)
        {
            loadPOKH();
        }

        private void cbUser_EditValueChanged(object sender, EventArgs e)
        {
            loadPOKH();
        }

        private void cbCustomer_EditValueChanged(object sender, EventArgs e)
        {
            loadPOKH();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadPOKH();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {

            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadPOKH();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadPOKH();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadPOKH();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            loadPOKH();
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
        }

        private void grvMaster_MasterRowGetRelationName(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventArgs e)
        {
            //e.RelationName = "grvDetail";
            //e.RelationName = grvDataRequestInvoice.Name;


            if (e.RelationIndex == 0)
                e.RelationName = grvDetail.Name;
            else if (e.RelationIndex == 1)
                e.RelationName = grvDataRequestInvoice.Name;
        }

        private void grvMaster_MasterRowGetRelationDisplayCaption(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventArgs e)
        {

        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            //SaveFileDialog sfd = new SaveFileDialog();
            //sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            //if (sfd.ShowDialog() == DialogResult.OK)
            //{
            //    grvMaster.OptionsPrint.AutoWidth = false;
            //    grvMaster.OptionsPrint.ExpandAllDetails = false;
            //    grvMaster.OptionsPrint.PrintDetails = true;
            //    grvMaster.OptionsPrint.UsePrintStyles = true;
            //    try
            //    {
            //        grvMaster.ExportToXls(sfd.FileName);
            //        Process.Start(sfd.FileName);
            //    }
            //    catch (Exception)
            //    {
            //    }
            //}

            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = Path.Combine(f.SelectedPath, $"ChiTietPOKH_{dtpStartDate.Value.ToString("ddMMyy")}_{dtpEndDate.Value.ToString("ddMMyy")}.xlsx");
                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdMaster;

                try
                {
                    CompositeLink compositeLink = new CompositeLink(printingSystem);
                    compositeLink.Links.Add(printableComponentLink1);

                    compositeLink.CreatePageForEachLink();

                    XlsxExportOptions optionsEx = new XlsxExportOptions();
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

        private void txtFilterText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFind_Click(null, null);
            }
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        //----------Ninh Duy Nhật 18-02-2025------------
        private Dictionary<string, DataTable> GetSelectedRowsDataGrouped()
        {
            Dictionary<string, DataTable> groupedData = new Dictionary<string, DataTable>();

            //int[] selectedRowHandles = grvMaster.GetSelectedRows();

            List<int> selectedIds = listSelected; // NTA B - update 15/09/25
            if (selectedIds.Count == 0)
            {
                TextUtils.ShowError("Không có dòng nào được chọn!");
                //MessageBox.Show("Không có dòng nào được chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            foreach (int id in selectedIds)
            {
                int rowHandle = grvMaster.LocateByValue("ID", id); // NTA B - update 15/09/25
                if (rowHandle >= 0)
                {
                    int customerID = TextUtils.ToInt(grvMaster.GetRowCellValue(rowHandle, "CustomerID"));
                    int eid = TextUtils.ToInt(grvMaster.GetRowCellValue(rowHandle, "EID"));
                    string key = $"{customerID}_{eid}";

                    decimal quantityPending = TextUtils.ToInt(grvMaster.GetRowCellValue(rowHandle, colQuantityPending));

                    if (quantityPending <= 0) continue;

                    // Kiểm tra nếu nhóm dữ liệu đã tồn tại hay chưa
                    if (!groupedData.ContainsKey(key))
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("POKHID", typeof(int));
                        dt.Columns.Add("POKHDetailID", typeof(int));
                        dt.Columns.Add("STT", typeof(int));
                        dt.Columns.Add("Maker", typeof(string));
                        dt.Columns.Add("CustomerID", typeof(int));
                        dt.Columns.Add("Quantity", typeof(int));
                        dt.Columns.Add("ProductName", typeof(string));
                        dt.Columns.Add("ProductCode", typeof(string));
                        dt.Columns.Add("CustomerName", typeof(string));
                        dt.Columns.Add("POCode", typeof(string));
                        dt.Columns.Add("FullName", typeof(string));
                        dt.Columns.Add("Unit", typeof(string));
                        dt.Columns.Add("ProductStatus", typeof(string));
                        dt.Columns.Add("Guarantee", typeof(string));
                        dt.Columns.Add("DeliveryStatus", typeof(string));
                        dt.Columns.Add("EID", typeof(int));
                        dt.Columns.Add("QuantityPending", typeof(decimal));

                        groupedData[key] = dt;
                    }

                    // Thêm dữ liệu vào nhóm tương ứng
                    DataTable targetTable = groupedData[key];
                    DataRow row = targetTable.NewRow();
                    row["POKHID"] = TextUtils.ToInt(grvMaster.GetRowCellValue(rowHandle, colPOKHID));
                    row["POKHDetailID"] = TextUtils.ToInt(grvMaster.GetRowCellValue(rowHandle, "ID"));
                    row["Maker"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "Maker"));
                    //row["Quantity"] = TextUtils.ToInt(grvMaster.GetRowCellValue(rowHandle, "Qty"));
                    row["Quantity"] = quantityPending;
                    row["ProductName"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProductName"));
                    row["ProductCode"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProductCode"));
                    row["POCode"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "POCode"));
                    row["Unit"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "Unit"));
                    row["CustomerName"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "CustomerName"));
                    row["FullName"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "EFullName"));
                    row["EID"] = eid;
                    row["CustomerID"] = customerID;
                    row["STT"] = targetTable.Rows.Count + 1;
                    row["QuantityPending"] = quantityPending;

                    targetTable.Rows.Add(row);
                }
            }

            return groupedData;
        }
        private void btnHandoverMinute_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Dictionary<string, DataTable> groupedData = GetSelectedRowsDataGrouped();
            if (groupedData == null || groupedData.Count == 0) return;

            foreach (var kvp in groupedData)
            {
                string key = kvp.Key;
                DataTable data = kvp.Value;

                frmHandoverMinutesDetail frm = new frmHandoverMinutesDetail();
                frm.dtContent = data;
                string cname = TextUtils.ToString(data.Rows[0]["CustomerName"]);
                frm.Text = $"Biên bản bàn giao {cname}";
                frm.Show();
            }
        }


        //updateN//NXT091025
        //private Dictionary<string, DataTable> GetSelectedRowsDataGroupedRequestInvoice()
        //{
        //    Dictionary<string, DataTable> groupedData = new Dictionary<string, DataTable>();

        //    try
        //    {
        //        //int[] selectedRowHandles = grvMaster.GetSelectedRows();
        //        //if (selectedRowHandles.Length == 0)

        //        List<int> selectedIds = listSelected; // NTA B - update 15/09/25
        //        if (selectedIds.Count == 0 || selectedIds == null)
        //        {
        //            TextUtils.ShowError("Không có dòng nào được chọn!");
        //            return null;
        //        }

        //        foreach (int id in selectedIds)
        //        {
        //            int rowHandle = grvMaster.LocateByValue("ID", id);
        //            if (rowHandle >= 0)
        //            {
        //                int customerID = TextUtils.ToInt(grvMaster.GetRowCellValue(rowHandle, "CustomerID"));
        //                int productID = TextUtils.ToInt(grvMaster.GetRowCellValue(rowHandle, "ProductID"));
        //                //string key = $"{customerID}_{productID}";
        //                string key = $"{customerID}";


        //                // Kiểm tra nếu nhóm dữ liệu đã tồn tại hay chưa
        //                if (!groupedData.ContainsKey(key))
        //                {
        //                    DataTable dt = new DataTable();
        //                    dt.Columns.Add("POKHID", typeof(int));
        //                    dt.Columns.Add("POKHDetailID", typeof(int));
        //                    dt.Columns.Add("STT", typeof(int));
        //                    dt.Columns.Add("ProductSaleID", typeof(string));
        //                    dt.Columns.Add("CustomerID", typeof(int));
        //                    dt.Columns.Add("Quantity", typeof(int));
        //                    dt.Columns.Add("ProductName", typeof(string));
        //                    dt.Columns.Add("ProductCode", typeof(string));
        //                    dt.Columns.Add("CustomerName", typeof(string));
        //                    dt.Columns.Add("ProductNewCode", typeof(string));
        //                    dt.Columns.Add("ProjectCode", typeof(string));
        //                    dt.Columns.Add("ProjectID", typeof(string));
        //                    dt.Columns.Add("ProjectName", typeof(string));
        //                    dt.Columns.Add("POCode", typeof(string));
        //                    dt.Columns.Add("Address", typeof(string));
        //                    dt.Columns.Add("Unit", typeof(string));
        //                    dt.Columns.Add("InvoiceDate", typeof(DateTime));
        //                    dt.Columns.Add("InvoiceNumber", typeof(string));
        //                    //ndnhat update 13/08/2025
        //                    dt.Columns.Add("RequestDate", typeof(DateTime));
        //                    dt.Columns.Add("DateRequestImport", typeof(DateTime));
        //                    dt.Columns.Add("ExpectedDate", typeof(DateTime));
        //                    dt.Columns.Add("SupplierName", typeof(string));
        //                    dt.Columns.Add("SomeBill", typeof(string));
        //                    dt.Columns.Add("BillImportCode", typeof(string));
        //                    dt.Columns.Add("IsStock", typeof(bool)); //NTA B - update 17/09/25 
        //                    dt.Columns.Add("GuestCode", typeof(string)); //NTA B - update 29/09/25 
        //                    dt.Columns.Add("Note", typeof(string)); //NTA B - update 29/09/25 

        //                    dt.Columns.Add("Code", typeof(string)); //grvDetail
        //                    dt.Columns.Add("Qty", typeof(decimal)); //grvDetail
        //                    dt.Columns.Add("TotalQty", typeof(decimal)); //grvDetail
        //                    dt.Columns.Add("PONumber", typeof(string)); //grvDetail
        //                    dt.Columns.Add("BillExportCode", typeof(string)); //grvDetail

        //                    groupedData[key] = dt;
        //                }

        //                // Thêm dữ liệu vào nhóm tương ứng
        //                //DataTable targetTable = groupedData[key];
        //                //DataRow row = targetTable.NewRow();
        //                //row["POKHID"] = TextUtils.ToInt(grvMaster.GetRowCellValue(rowHandle, colPOKHID));
        //                //row["POKHDetailID"] = TextUtils.ToInt(grvMaster.GetRowCellValue(rowHandle, "ID"));
        //                ////row["Maker"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "Maker"));
        //                //row["Quantity"] = TextUtils.ToInt(grvDetail.GetRowCellValue(rowHandle, "Qty"));
        //                //row["ProductName"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProductName"));
        //                //row["ProductSaleID"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProductID"));
        //                ////row["ProductCode"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProductCode"));
        //                //row["ProjectCode"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProjectCode"));
        //                //row["ProjectName"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProjectName"));
        //                //row["ProductNewCode"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProductNewCode"));
        //                //row["POCode"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "POCode"));
        //                //row["Unit"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "Unit"));
        //                //row["CustomerName"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "CustomerName"));
        //                //row["Address"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "Address"));
        //                ////row["EID"] = eid;
        //                //row["CustomerID"] = customerID;
        //                ////dnhat update 13/08/2025
        //                //DateTime? date = TextUtils.ToDate4(grvMaster.GetRowCellValue(rowHandle, colRequestDate));
        //                //row["RequestDate"] = date.HasValue ? (object)date.Value : DBNull.Value;
        //                //DateTime? dateRequestImport = TextUtils.ToDate4(grvMaster.GetRowCellValue(rowHandle, colDateRequestImport));
        //                //row["DateRequestImport"] = dateRequestImport.HasValue ? (object)dateRequestImport.Value : DBNull.Value;
        //                //DateTime? dateExpected = TextUtils.ToDate4(grvMaster.GetRowCellValue(rowHandle, colExpectedDate));
        //                //row["ExpectedDate"] = dateExpected.HasValue ? (object)dateExpected.Value : DBNull.Value;
        //                //row["SupplierName"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, colSupplierName));
        //                //row["SomeBill"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, colSomeBill));
        //                //row["BillImportCode"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, colBillImportCode));
        //                //row["ProjectID"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, colProjectID));
        //                //row["Code"] = TextUtils.ToString(grvDetail.GetRowCellValue(rowHandle, colCode));
        //                //row["STT"] = targetTable.Rows.Count + 1;

        //                //targetTable.Rows.Add(row);
        //                DataTable targetTable = groupedData[key];

        //                // ====== LẤY DETAIL VIEW CỦA MASTER ======
             
        //                GridView detailView = grvMaster.GetDetailView(rowHandle, 0) as GridView;
        //                detailView.PostEditor();
        //                detailView.UpdateCurrentRow();

        //                grvMaster.PostEditor();
        //                grvMaster.UpdateCurrentRow();
        //                if (detailView == null || detailView.DataRowCount == 0)
        //                    continue;

        //                for (int i = 0; i < detailView.DataRowCount; i++)
        //                {
        //                    int dHandle = detailView.GetVisibleRowHandle(i);
        //                    if (!detailView.IsDataRow(dHandle)) continue;
        //                    bool isSelected = TextUtils.ToBoolean(detailView.GetRowCellValue(dHandle, "IsSelected"));
        //                    if (!isSelected) continue;

        //                    DataRow row = targetTable.NewRow();
        //                    row["POKHID"] = TextUtils.ToInt(grvMaster.GetRowCellValue(rowHandle, colPOKHID));
        //                    row["POKHDetailID"] = TextUtils.ToInt(grvMaster.GetRowCellValue(rowHandle, "ID"));

        //                    // ====== CÒN LẠI TỪ MASTER ======
        //                    row["ProductName"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProductName"));
        //                    row["ProductSaleID"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProductID"));
        //                    row["ProjectCode"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProjectCode"));
        //                    row["ProjectName"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProjectName"));
        //                    row["ProductNewCode"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProductNewCode"));
        //                    row["POCode"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "POCode"));
        //                    row["Unit"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "Unit"));
        //                    row["CustomerName"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "CustomerName"));
        //                    row["Address"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "Address"));
        //                    row["CustomerID"] = customerID;

        //                    DateTime? date = TextUtils.ToDate4(grvMaster.GetRowCellValue(rowHandle, colRequestDate));
        //                    row["RequestDate"] = date.HasValue ? (object)date.Value : DBNull.Value;

        //                    DateTime? dateRequestImport = TextUtils.ToDate4(grvMaster.GetRowCellValue(rowHandle, colDateRequestImport));
        //                    row["DateRequestImport"] = dateRequestImport.HasValue ? (object)dateRequestImport.Value : DBNull.Value;

        //                    DateTime? dateExpected = TextUtils.ToDate4(grvMaster.GetRowCellValue(rowHandle, colExpectedDate));
        //                    row["ExpectedDate"] = dateExpected.HasValue ? (object)dateExpected.Value : DBNull.Value;

        //                    row["SupplierName"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, colSupplierName));
        //                    row["SomeBill"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, colSomeBill));
        //                    row["BillImportCode"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, colBillImportCode));
        //                    row["ProjectID"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, colProjectID));
        //                    row["PONumber"] = TextUtils.ToString(grvMaster.GetRowCellValue(dHandle, "PONumber"));

        //                    // ====== DETAIL FIELDS ====== // NTA B - update 141025
        //                    row["Quantity"] = TextUtils.ToInt(detailView.GetRowCellValue(dHandle, "Qty"));
        //                    row["Code"] = TextUtils.ToString(detailView.GetRowCellValue(dHandle, "Code"));
        //                    row["TotalQty"] = TextUtils.ToDecimal(detailView.GetRowCellValue(dHandle, "TotalQty"));
        //                    row["PONumber"] = TextUtils.ToString(detailView.GetRowCellValue(dHandle, "PONumber"));
        //                    row["BillExportCode"] = TextUtils.ToString(detailView.GetRowCellValue(dHandle, "Code"));
        //                    // ===========================

        //                    row["STT"] = targetTable.Rows.Count + 1;
        //                    targetTable.Rows.Add(row);
        //                }
        //            }
        //        }

        //        if (groupedData.Values.Count == 0 || groupedData.Values == null)
        //        {
        //            TextUtils.ShowError("Không có dòng detail nào được chọn!");
        //            return null;
        //        }

        //        return groupedData;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Lỗi khi lấy dữ liệu nhóm Request Invoice:\n" + ex.Message);
        //        return null;
        //    }

        //}


        private Dictionary<string, DataTable> GetSelectedRowsDataGroupedRequestInvoice()
        {
            Dictionary<string, DataTable> groupedData = new Dictionary<string, DataTable>();

            try
            {
                //int[] selectedRowHandles = grvMaster.GetSelectedRows();
                //if (selectedRowHandles.Length == 0)

                List<int> selectedIds = listSelected; // NTA B - update 15/09/25
                if (selectedIds.Count == 0 || selectedIds == null)
                {
                    TextUtils.ShowError("Không có dòng nào được chọn!");
                    return null;
                }

                foreach (int id in selectedIds)
                {
                    int rowHandle = grvMaster.LocateByValue("ID", id);
                    if (rowHandle >= 0)
                    {
                        int customerID = TextUtils.ToInt(grvMaster.GetRowCellValue(rowHandle, "CustomerID"));
                        int productID = TextUtils.ToInt(grvMaster.GetRowCellValue(rowHandle, "ProductID"));
                        //string key = $"{customerID}_{productID}";
                        string key = $"{customerID}";


                        // Kiểm tra nếu nhóm dữ liệu đã tồn tại hay chưa
                        if (!groupedData.ContainsKey(key))
                        {
                            DataTable dt = new DataTable();
                            dt.Columns.Add("POKHID", typeof(int));
                            dt.Columns.Add("POKHDetailID", typeof(int));
                            dt.Columns.Add("STT", typeof(int));
                            dt.Columns.Add("ProductSaleID", typeof(string));
                            dt.Columns.Add("CustomerID", typeof(int));
                            dt.Columns.Add("Quantity", typeof(int));
                            dt.Columns.Add("ProductName", typeof(string));
                            dt.Columns.Add("ProductCode", typeof(string));
                            dt.Columns.Add("CustomerName", typeof(string));
                            dt.Columns.Add("ProductNewCode", typeof(string));
                            dt.Columns.Add("ProjectCode", typeof(string));
                            dt.Columns.Add("ProjectID", typeof(string));
                            dt.Columns.Add("ProjectName", typeof(string));
                            dt.Columns.Add("POCode", typeof(string));
                            dt.Columns.Add("Address", typeof(string));
                            dt.Columns.Add("Unit", typeof(string));
                            dt.Columns.Add("InvoiceDate", typeof(DateTime));
                            dt.Columns.Add("InvoiceNumber", typeof(string));
                            //ndnhat update 13/08/2025
                            dt.Columns.Add("RequestDate", typeof(DateTime));
                            dt.Columns.Add("DateRequestImport", typeof(DateTime));
                            dt.Columns.Add("ExpectedDate", typeof(DateTime));
                            dt.Columns.Add("SupplierName", typeof(string));
                            dt.Columns.Add("SomeBill", typeof(string));
                            dt.Columns.Add("BillImportCode", typeof(string));
                            dt.Columns.Add("IsStock", typeof(bool)); //NTA B - update 17/09/25 
                            dt.Columns.Add("GuestCode", typeof(string)); //NTA B - update 29/09/25 
                            dt.Columns.Add("Note", typeof(string)); //NTA B - update 29/09/25 

                            dt.Columns.Add("Code", typeof(string)); //grvDetail
                            dt.Columns.Add("Qty", typeof(decimal)); //grvDetail
                            dt.Columns.Add("TotalQty", typeof(decimal)); //grvDetail
                            dt.Columns.Add("PONumber", typeof(string)); //grvDetail
                            dt.Columns.Add("BillExportCode", typeof(string)); //grvDetail
                            dt.Columns.Add("IsSelected", typeof(bool)); //NTA B - update 17/10/25


                            groupedData[key] = dt;
                        }

                        // Thêm dữ liệu vào nhóm tương ứng
                        //DataTable targetTable = groupedData[key];
                        //DataRow row = targetTable.NewRow();
                        //row["POKHID"] = TextUtils.ToInt(grvMaster.GetRowCellValue(rowHandle, colPOKHID));
                        //row["POKHDetailID"] = TextUtils.ToInt(grvMaster.GetRowCellValue(rowHandle, "ID"));
                        ////row["Maker"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "Maker"));
                        //row["Quantity"] = TextUtils.ToInt(grvDetail.GetRowCellValue(rowHandle, "Qty"));
                        //row["ProductName"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProductName"));
                        //row["ProductSaleID"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProductID"));
                        ////row["ProductCode"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProductCode"));
                        //row["ProjectCode"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProjectCode"));
                        //row["ProjectName"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProjectName"));
                        //row["ProductNewCode"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProductNewCode"));
                        //row["POCode"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "POCode"));
                        //row["Unit"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "Unit"));
                        //row["CustomerName"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "CustomerName"));
                        //row["Address"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "Address"));
                        ////row["EID"] = eid;
                        //row["CustomerID"] = customerID;
                        ////dnhat update 13/08/2025
                        //DateTime? date = TextUtils.ToDate4(grvMaster.GetRowCellValue(rowHandle, colRequestDate));
                        //row["RequestDate"] = date.HasValue ? (object)date.Value : DBNull.Value;
                        //DateTime? dateRequestImport = TextUtils.ToDate4(grvMaster.GetRowCellValue(rowHandle, colDateRequestImport));
                        //row["DateRequestImport"] = dateRequestImport.HasValue ? (object)dateRequestImport.Value : DBNull.Value;
                        //DateTime? dateExpected = TextUtils.ToDate4(grvMaster.GetRowCellValue(rowHandle, colExpectedDate));
                        //row["ExpectedDate"] = dateExpected.HasValue ? (object)dateExpected.Value : DBNull.Value;
                        //row["SupplierName"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, colSupplierName));
                        //row["SomeBill"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, colSomeBill));
                        //row["BillImportCode"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, colBillImportCode));
                        //row["ProjectID"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, colProjectID));
                        //row["Code"] = TextUtils.ToString(grvDetail.GetRowCellValue(rowHandle, colCode));
                        //row["STT"] = targetTable.Rows.Count + 1;

                        //targetTable.Rows.Add(row);
                        DataTable targetTable = groupedData[key];

                        // ====== LẤY DETAIL VIEW CỦA MASTER ======

                        bool expandedHere;
                        GridView detailView = GetLevel1DetailView(grvMaster, rowHandle, 0, out expandedHere);
                        //if (detailView == null)
                        //    continue;

                        detailView?.PostEditor();
                        detailView?.UpdateCurrentRow();

                        grvMaster.PostEditor();
                        grvMaster.UpdateCurrentRow();

                        //if (detailView.DataRowCount == 0)
                        //{
                        //    if (expandedHere)
                        //        grvMaster.CollapseMasterRow(rowHandle);
                        //    continue;
                        //}
                        if (detailView != null && detailView.DataRowCount > 0)
                        {
                            for (int i = 0; i < detailView.DataRowCount; i++)
                            {
                                int dHandle = detailView.GetVisibleRowHandle(i);
                                if (!detailView.IsDataRow(dHandle)) continue;
                                bool isSelected = TextUtils.ToBoolean(detailView.GetRowCellValue(dHandle, "IsSelected"));
                                if (!isSelected) continue;

                                DataRow row = targetTable.NewRow();
                                row["POKHID"] = TextUtils.ToInt(grvMaster.GetRowCellValue(rowHandle, colPOKHID));
                                row["POKHDetailID"] = TextUtils.ToInt(grvMaster.GetRowCellValue(rowHandle, "ID"));

                                row["ProductName"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProductName"));
                                row["ProductSaleID"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProductID"));
                                row["ProjectCode"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProjectCode"));
                                row["ProjectName"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProjectName"));
                                row["ProductNewCode"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProductNewCode"));
                                row["POCode"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "POCode"));
                                row["Unit"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "Unit"));
                                row["CustomerName"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "CustomerName"));
                                row["Address"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "Address"));
                                row["CustomerID"] = customerID;

                                DateTime? date = TextUtils.ToDate4(grvMaster.GetRowCellValue(rowHandle, colRequestDate));
                                row["RequestDate"] = date.HasValue ? (object)date.Value : DBNull.Value;
                                DateTime? dateRequestImport = TextUtils.ToDate4(grvMaster.GetRowCellValue(rowHandle, colDateRequestImport));
                                row["DateRequestImport"] = dateRequestImport.HasValue ? (object)dateRequestImport.Value : DBNull.Value;
                                DateTime? dateExpected = TextUtils.ToDate4(grvMaster.GetRowCellValue(rowHandle, colExpectedDate));
                                row["ExpectedDate"] = dateExpected.HasValue ? (object)dateExpected.Value : DBNull.Value;

                                row["SupplierName"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, colSupplierName));
                                row["SomeBill"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, colSomeBill));
                                row["BillImportCode"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, colBillImportCode));
                                row["ProjectID"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, colProjectID));

                                // ===== DETAIL FIELDS =====
                                row["Quantity"] = TextUtils.ToInt(detailView.GetRowCellValue(dHandle, "Qty"));
                                row["Code"] = TextUtils.ToString(detailView.GetRowCellValue(dHandle, "Code"));
                                row["TotalQty"] = TextUtils.ToInt(detailView.GetRowCellValue(dHandle, "TotalQty"));

                                row["BillExportCode"] = TextUtils.ToString(detailView.GetRowCellValue(dHandle, "Code"));
                                row["PONumber"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "PONumber"));
                                row["GuestCode"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "GuestCode"));

                                row["STT"] = targetTable.Rows.Count + 1;
                                targetTable.Rows.Add(row);
                            }

                            if (expandedHere)
                                grvMaster.CollapseMasterRow(rowHandle);
                        }
                        else
                        {
                            DataRow row = targetTable.NewRow();
                            row["POKHID"] = TextUtils.ToInt(grvMaster.GetRowCellValue(rowHandle, colPOKHID));
                            row["POKHDetailID"] = TextUtils.ToInt(grvMaster.GetRowCellValue(rowHandle, "ID"));
                            row["ProductName"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProductName"));
                            row["ProductSaleID"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProductID"));
                            row["ProjectCode"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProjectCode"));
                            row["ProjectName"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProjectName"));
                            row["ProductNewCode"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProductNewCode"));
                            row["POCode"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "POCode"));
                            row["Unit"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "Unit"));
                            row["CustomerName"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "CustomerName"));
                            row["Address"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "Address"));
                            row["CustomerID"] = customerID;
                            row["Quantity"] = TextUtils.ToInt(grvMaster.GetRowCellValue(rowHandle, "Qty")); // fallback

                            DateTime? date = TextUtils.ToDate4(grvMaster.GetRowCellValue(rowHandle, colRequestDate));
                            row["RequestDate"] = date.HasValue ? (object)date.Value : DBNull.Value;
                            DateTime? dateRequestImport = TextUtils.ToDate4(grvMaster.GetRowCellValue(rowHandle, colDateRequestImport));
                            row["DateRequestImport"] = dateRequestImport.HasValue ? (object)dateRequestImport.Value : DBNull.Value;
                            DateTime? dateExpected = TextUtils.ToDate4(grvMaster.GetRowCellValue(rowHandle, colExpectedDate));
                            row["ExpectedDate"] = dateExpected.HasValue ? (object)dateExpected.Value : DBNull.Value;

                            row["SupplierName"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, colSupplierName));
                            row["SomeBill"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, colSomeBill));
                            row["BillImportCode"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, colBillImportCode));
                            row["ProjectID"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, colProjectID));

                            row["BillExportCode"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "Code"));
                            row["PONumber"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "PONumber"));
                            row["GuestCode"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "GuestCode"));

                            row["STT"] = targetTable.Rows.Count + 1;
                            targetTable.Rows.Add(row);
                        }
                    }
                }

                if (groupedData.Values.Count == 0 || groupedData.Values == null)
                {
                    TextUtils.ShowError("Không có dòng detail nào được chọn!");
                    return null;
                }

                return groupedData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy dữ liệu nhóm Request Invoice:\n{ex.Message}\r\n{ex.ToString()}");
                return null;
            }

        }
        private void btnRequestInvoice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //grvMaster.CloseEditor();
            //grvMaster.UpdateCurrentRow();

            //grvDetail.focusedro
            try
            {
                Dictionary<string, DataTable> groupedData = GetSelectedRowsDataGroupedRequestInvoice();
                if (groupedData == null || groupedData.Count == 0) return;
                if (groupedData.Count > 1)
                {
                    MessageBox.Show($"Bạn chọn sản phẩm từ {groupedData.Count} khách hàng\nNên phần mềm sẽ tự động tạo {groupedData.Count} hóa đơn xuất", "Thông báo");
                }
                foreach (var kvp in groupedData)
                {
                    string key = kvp.Key;
                    DataTable data = kvp.Value;
                    frmRequestInvoiceDetail frm = new frmRequestInvoiceDetail();
                    RequestInvoiceModel requestInvoice = new RequestInvoiceModel();
                    //string cname = TextUtils.ToString(data.Rows[0]["CustomerName"]);
                    requestInvoice.CustomerID = TextUtils.ToInt(data.Rows[0]["CustomerID"]);
                    //TextUtils.ToInt(data.Rows[0]["Address"]);
                    frm.dtDetail = data;
                    frm.requestInvoice = requestInvoice;
                    //frm.Text = $"Yêu cầu xuất hóa đơn {cname}";
                    frm.isPOKH = true;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"btnRequestInvoice_ItemClick\r\n{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }

        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            grvMaster.CloseEditor();
            grvMaster.UpdateCurrentRow(); //NTA B update171025

            GridView detailView = grvMaster.GetDetailView(grvMaster.FocusedRowHandle, 1) as GridView;

            detailView?.PostEditor();
            detailView?.UpdateCurrentRow();

            //DataTable dtSource = (DataTable)grdMaster.DataSource;
            //if (dtSource == null) return;
            //DataTable dtChange = dtSource.GetChanges();
            DataTable dtChange = dtInvoice.GetChanges();
            //if (dtChange == null) return;

            DataTable dtMasterChange = dt.GetChanges();
            //if (dtChange == null && dtMasterChange == null) return;

            if (dtChange != null)
            {
                foreach (DataRow row in dtChange.Rows)
                {
                    //int pokhDetailID = TextUtils.ToInt(row["ID"]);
                    //var myDict = new Dictionary<string, object>()
                    //{
                    //    {"BillNumber" ,TextUtils.ToString(row["BillNumber"])},
                    //    {"BillDate" ,TextUtils.ToDate4(row["BillDate"])},
                    //    {"DeliveryRequestedDate" ,TextUtils.ToDate4(row["DeliveryRequestedDate"])},
                    //    {"UpdatedDate" ,DateTime.Now},
                    //};

                    //SQLHelper<POKHDetailModel>.UpdateFieldsByID(myDict, pokhDetailID);


                    //NTA B update 181025
                    string billNumber = TextUtils.ToString(row["InvoiceNumber"]);
                    DateTime? invoiceDate = TextUtils.ToDate4(row["InvoiceDate"]);
                    int id = TextUtils.ToInt(row["RequestInvoiceDetailID"]);
                    var myDict2 = new Dictionary<string, object>()
                    {
                        {RequestInvoiceDetailModel_Enum.InvoiceNumber.ToString(),billNumber },
                        {RequestInvoiceDetailModel_Enum.InvoiceDate.ToString(),invoiceDate },
                        {RequestInvoiceDetailModel_Enum.UpdatedBy.ToString(),Global.AppCodeName },
                        {RequestInvoiceDetailModel_Enum.UpdatedDate.ToString(),DateTime.Now },
                    };
                    //var exp = new Expression(RequestInvoiceDetailModel_Enum.POKHDetailID, pokhDetailID);
                    SQLHelper<RequestInvoiceDetailModel>.UpdateFieldsByID(myDict2, id);
                    //END NTA B update 181025
                }

                loadPOKH();
            }

            if (dtMasterChange != null)
            {
                foreach (DataRow row in dtMasterChange.Rows)
                {
                    int pokhDetailID = TextUtils.ToInt(row["ID"]);
                    var myDict = new Dictionary<string, object>()
                    {
                        {"BillNumber" ,TextUtils.ToString(row["BillNumber"])},
                        {"BillDate" ,TextUtils.ToDate4(row["BillDate"])},
                        //{"DeliveryRequestedDate" ,TextUtils.ToDate4(row["DeliveryRequestedDate"])},
                        {"UpdatedDate" ,DateTime.Now},
                    };

                    SQLHelper<POKHDetailModel>.UpdateFieldsByID(myDict, pokhDetailID);
                }
            }
            
            MessageBox.Show("Lưu thành công!", "Thông báo");

            dt.AcceptChanges();
            dtInvoice.AcceptChanges();
            //loadPOKH();
        }

        bool isRecallCellValueChanged = false;
        private void grvMaster_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (isRecallCellValueChanged == true) return;
            try
            {

                using (WaitDialogForm fWait = new WaitDialogForm())
                {
                    isRecallCellValueChanged = true;
                    grvMaster.CloseEditor();

                    if (e.Column.FieldName == colBillNumber.FieldName || e.Column.FieldName == colBillDate.FieldName || e.Column.FieldName == "DeliveryRequestedDate")
                    {
                        if (e.Value == null) return;
                        //int[] selectedRows = grvMaster.GetSelectedRows();
                        List<int> selectedIds = listSelected; // NTA B - update 15/09/25
                        if (selectedIds.Count > 0)
                        {
                            foreach (int id in selectedIds)
                            {
                                int row = grvMaster.LocateByValue("ID", id);
                                grvMaster.SetRowCellValue(row, grvMaster.Columns[e.Column.FieldName], e.Value);

                            }
                        }
                        //grvMaster.SetRowCellValue(e.RowHandle, colIsUpdated, 1);
                    }

                }
            }
            finally
            {
                isRecallCellValueChanged = false;
            }
        }

        private void grvMaster_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(grvMaster.FocusedColumn));
                if (string.IsNullOrWhiteSpace(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void btnBanGiaoPO_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //grvMaster.SetRowCellValue(grvMaster.FocusedRowHandle, colIsPaid, true);
            //int[] selectedRows = grvMaster.GetSelectedRows();
            List<int> selectedIds = listSelected; // NTA B - update 15/09/25
            foreach (int item in selectedIds)
            {
                int id = TextUtils.ToInt(grvMaster.GetRowCellValue(item, colID));
                Dictionary<string, object> myDict = new Dictionary<string, object>()
                {
                    {"IsDelivered" ,1},
                    {"UpdatedDate" ,DateTime.Now},
                };
                SQLHelper<POKHDetailModel>.UpdateFieldsByID(myDict, id);
            }
            loadPOKH();
        }


        //NXT091025
        private GridView GetLevel1DetailView(GridView masterView, int masterRowHandle, int relationIndex, out bool expandedHere)
        {
            expandedHere = false;
            var detailView = masterView.GetDetailView(masterRowHandle, relationIndex) as GridView;
            if (detailView == null)
            {

                if (!masterView.GetMasterRowExpanded(masterRowHandle))
                {
                    masterView.ExpandMasterRow(masterRowHandle);
                    expandedHere = true;
                }
                detailView = masterView.GetDetailView(masterRowHandle, relationIndex) as GridView;
            }
            return detailView;
        }

        //NXT091025
        private void chkIsSelected_EditValueChanged(object sender, EventArgs e)
        {
            //grvMaster.CloseEditor();
            //bool isSelected = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsSelected));
            //int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));

            //if (isSelected)
            //{
            //    if (!listSelected.Contains(id)) listSelected.Add(id);
            //}
            //else
            //{
            //    listSelected.Remove(id);
            //}

            grvMaster.CloseEditor();
            grvMaster.UpdateCurrentRow();

            bool isSelected = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsSelected));
            int masterId = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));


            if (isSelected) { if (!listSelected.Contains(masterId)) listSelected.Add(masterId); }
            else { listSelected.Remove(masterId); }

            int masterRowHandle = grvMaster.LocateByValue("ID", masterId);
            if (masterRowHandle < 0) return;


            bool expandedHere;
            int relationIndex = 0; // đổi nếu detail không phải index 0
            var detailView = GetLevel1DetailView(grvMaster, masterRowHandle, relationIndex, out expandedHere);
            if (detailView == null) { if (expandedHere) grvMaster.CollapseMasterRow(masterRowHandle); return; }

            detailView.BeginUpdate();
            try
            {
                for (int i = 0; i < detailView.DataRowCount; i++)
                {
                    int handle = detailView.GetVisibleRowHandle(i);
                    if (handle < 0) continue;
                    detailView.SetRowCellValue(handle, "IsSelected", isSelected);
                }
            }
            finally
            {
                detailView.EndUpdate();
            }


            if (expandedHere) grvMaster.CollapseMasterRow(masterRowHandle);
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grvMaster.RowCount; i++)
            {
                grvMaster.SetRowCellValue(i, colIsSelected, true);

                int id = TextUtils.ToInt(grvMaster.GetRowCellValue(i, colID));
                if (!listSelected.Contains(id))
                {
                    listSelected.Add(id);
                }
            }
        }

        private void bntCancelSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grvMaster.RowCount; i++)
            {
                grvMaster.SetRowCellValue(i, colIsSelected, false);

                int id = TextUtils.ToInt(grvMaster.GetRowCellValue(i, colID));
                listSelected.Remove(id);
            }
        }

        private void grdMaster_MouseDown(object sender, MouseEventArgs e)
        {
            var hitInfo = grvMaster.CalcHitInfo(e.Location);
            if (hitInfo.InGroupRow && e.Button == MouseButtons.Left)
            {
                int childCount = grvMaster.GetChildRowCount(hitInfo.RowHandle);
                bool isAllSelected = true;

                for (int i = 0; i < childCount; i++)
                {
                    int childRowHandle = grvMaster.GetChildRowHandle(hitInfo.RowHandle, i);
                    if (!TextUtils.ToBoolean(grvMaster.GetRowCellValue(childRowHandle, colIsSelected)))
                    {
                        isAllSelected = false;
                        break;
                    }
                }

                for (int i = 0; i < childCount; i++)
                {
                    int childRowHandle = grvMaster.GetChildRowHandle(hitInfo.RowHandle, i);
                    grvMaster.SetRowCellValue(childRowHandle, colIsSelected, !isAllSelected);

                    int masterId = TextUtils.ToInt(grvMaster.GetRowCellValue(childRowHandle, colID));
                    if (!isAllSelected)
                    {
                        if (!listSelected.Contains(masterId)) listSelected.Add(masterId);
                    }
                    else
                    {
                        listSelected.Remove(masterId);
                    }

                    bool expandedHere;
                    var detailView = GetLevel1DetailView(grvMaster, childRowHandle, 0, out expandedHere);
                    if (detailView != null)
                    {
                        detailView.BeginUpdate();
                        try
                        {
                            for (int j = 0; j < detailView.DataRowCount; j++)
                            {
                                int detailHandle = detailView.GetVisibleRowHandle(j);
                                if (detailHandle < 0) continue;
                                detailView.SetRowCellValue(detailHandle, "IsSelected", !isAllSelected);
                            }
                        }
                        finally
                        {
                            detailView.EndUpdate();
                        }

                        if (expandedHere) grvMaster.CollapseMasterRow(childRowHandle);
                    }
                }
            }
        }

        private void grvMaster_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;

            bool isSelected = TextUtils.ToBoolean(grvMaster.GetRowCellValue(e.RowHandle, colIsSelected));
            if (isSelected)
            {
                e.Appearance.BackColor = Color.LightYellow;
                e.HighPriority = true;
            }
        }

        private void btnHistoryMoney_Click(object sender, EventArgs e)
        {
            frmHistoryMoney_New frm = new frmHistoryMoney_New();
            frm.txtFilter.Text = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colPOCode));
            frm.Show();
        }

        private void cboEmployeeTeamSale_EditValueChanged(object sender, EventArgs e)
        {
            if (cboEmployeeTeamSale.EditValue != null)
            {
                int teamID = TextUtils.ToInt(cboEmployeeTeamSale.EditValue);
                var employee = TextUtils.GetDataTableFromSP("spGetEmployeeByTeamSale", new string[] { "@EmployeeTeamSaleID" },
                                                new object[] { teamID });
                cbUser.Properties.DataSource = employee;
                cbUser.Properties.DisplayMember = "FullName";
                cbUser.Properties.ValueMember = "UserID"; //NTA B update 041025
                cbUser.EditValue = 0;
            }
            else
            {
                loadUser();
                cbUser.EditValue = 0;
            }
        }

        private void grdMaster_Click(object sender, EventArgs e)
        {

        }

        private void grvDetail_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e) 
        {
            if (e.Column.FieldName == "IsSelected")
            {
                GridView detailView = sender as GridView;
                if (detailView == null) return;

                // Lấy master row handle đúng cách
                GridView masterView = detailView.ParentView as GridView;
                int masterRowHandle = detailView.SourceRowHandle;
                if (masterRowHandle < 0) return;

                // Kiểm tra có dòng detail nào được chọn không
                bool hasSelected = false;
                for (int i = 0; i < detailView.DataRowCount; i++)
                {
                    int rowHandle = detailView.GetVisibleRowHandle(i);
                    if (TextUtils.ToBoolean(detailView.GetRowCellValue(rowHandle, "IsSelected")))
                    {
                        hasSelected = true;
                        break;
                    }
                }

                // Cập nhật trạng thái chọn của master
                masterView.SetRowCellValue(masterRowHandle, colIsSelected, hasSelected);
                int masterId = TextUtils.ToInt(masterView.GetRowCellValue(masterRowHandle, colID));
                if (hasSelected)
                {
                    if (!listSelected.Contains(masterId)) listSelected.Add(masterId);
                }
                else
                {
                    listSelected.Remove(masterId);
                }
            }
        }

        private void grvDetail_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "IsSelected")
            {
                GridView detailView = sender as GridView;
                if (detailView == null) return;

                bool newValue = TextUtils.ToBoolean(e.Value);

                GridView masterView = detailView.ParentView as GridView;
                int masterRowHandle = detailView.SourceRowHandle;
                if (masterRowHandle < 0) return;

                // Kiểm tra có dòng detail nào được chọn (tính cả dòng hiện tại)
                bool hasSelected = newValue;
                if (!hasSelected)
                {
                    for (int i = 0; i < detailView.DataRowCount; i++)
                    {
                        if (i == e.RowHandle) continue;
                        int rowHandle = detailView.GetVisibleRowHandle(i);
                        if (TextUtils.ToBoolean(detailView.GetRowCellValue(rowHandle, "IsSelected")))
                        {
                            hasSelected = true;
                            break;
                        }
                    }
                }

                // Cập nhật trạng thái chọn của master
                masterView.SetRowCellValue(masterRowHandle, colIsSelected, hasSelected);
                int masterId = TextUtils.ToInt(masterView.GetRowCellValue(masterRowHandle, colID));
                if (hasSelected)
                {
                    if (!listSelected.Contains(masterId)) listSelected.Add(masterId);
                }
                else
                {
                    listSelected.Remove(masterId);
                }
            }
        }

        private void grvDetail_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(grvDetail.GetFocusedRowCellValue(grvDetail.FocusedColumn));
                if (string.IsNullOrWhiteSpace(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void grvMaster_MasterRowGetRelationCount(object sender, MasterRowGetRelationCountEventArgs e)
        {
            e.RelationCount = 2;
        }

        private void grvMaster_MasterRowGetChildList(object sender, MasterRowGetChildListEventArgs e)
        {
            //var masterRow = ((DataRowView)grvMaster.GetRow(e.RowHandle)).Row;
            //var masterID = masterRow["ID"];

            //if (e.RelationIndex == 0)
            //{
            //    e.ChildList = dtExport.AsEnumerable()
            //                         .Where(r => r["MasterID"].Equals(masterID))
            //                         .CopyToDataTable();
            //}
            //else if (e.RelationIndex == 1)
            //{
            //    e.ChildList = dtInvoice.AsEnumerable()
            //                         .Where(r => r["MasterID"].Equals(masterID))
            //                         .CopyToDataTable();
            //}
        }

        private void grvDataRequestInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(grvDataRequestInvoice.GetFocusedRowCellValue(grvDataRequestInvoice.FocusedColumn));
                if (string.IsNullOrWhiteSpace(value)) return;

                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void grvMaster_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (grvMaster.FocusedColumn == colBillDate || grvMaster.FocusedColumn == colBillNumber)
            {
                int pokhdetailID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
                //var requestInvoices = dtInvoice.AsEnumerable().Where(x => x.Field<int>("POKHDetailID") == pokhdetailID).ToList();
                var requestInvoices = dtInvoice.Select($"POKHDetailID = {pokhdetailID}");

                e.Cancel = requestInvoices.Count() > 0;
            }

            
        }
    }
}
