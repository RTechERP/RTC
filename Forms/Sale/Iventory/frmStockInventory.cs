using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using System.IO;
using Forms.Classes;
using System.Diagnostics;
using Forms.DanhMuc;
using BaseBusiness.DTO;

namespace BMS
{
    public partial class frmStockInventory : _Forms
    {
        public InventoryModel inventory = new InventoryModel();
        public string VP;
        int Show;

        DataTable lstTonCk = new DataTable();

        public frmStockInventory()
        {
            InitializeComponent();
        }

        private void frmStockInventory_Load(object sender, EventArgs e)
        {
            //ViewColumn();d
            //btnGetProduct.Enabled = Global.IsAdmin;
            //btnGetProduct.Visible = (Global.IsAdmin && Global.EmployeeID <= 0);
            //btnConvertProduct.Visible = (Global.IsAdmin && Global.EmployeeID <= 0);

            this.Text += " - " + VP;
            Show = TextUtils.ToInt(File.ReadAllText(Path.Combine(Application.StartupPath, "UnHideNCC.txt")));
            if (Show == 0)
            {
                //grdData.Visible = false;
                //tableLayoutPanel2.SetColumnSpan(grdMaster, 2);
            }
            else
            {
                //tableLayoutPanel2.SetColumnSpan(grdMaster, 1);
                // grdData.Visible = true;
            }

            Lib.LockEvents = true;
            loadData();
            LoadtreeData();
            chkAllProduct.Checked = true;
            Lib.LockEvents = false;
        }

        string[] columns = new[] { "TotalQuantityFirst", "Import", "Export", "ImportPT", "ExportPM", "StillBorrowed", "TotalQuantityKeep", "TotalQuantityExportKeep", "TotalQuantityLastActual", "TotalQuantityNotRequestExport", "TotalQuantityReturnNCC", "MinQuantityActual", "MinQuantity" };
        void ViewColumn()
        {
            if (Global.IsAdmin && Global.EmployeeID <= 0) return; //Nếu là admin

            var productGroupWarehouses = SQLHelper<ProductGroupWarehouseModel>.FindByAttribute("EmployeeID", Global.EmployeeID);

            if (productGroupWarehouses.Count > 0) return; //Nếu là admin kho
            if (Global.DepartmentID == 4) return; //Nếu là nv mua

            foreach (string column in columns)
            {
                var col = grvMaster.Columns[column];
                if (col == null) continue;
                col.Visible = false;
            }
        }

        private void loadData()
        {
            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tải..."))
            {
                int IDTree = -1;
                var node = treeData.FocusedNode;
                if (node != null) IDTree = TextUtils.ToInt(treeData.FocusedNode.GetValue(colIDTree));

                if (IDTree == 4) colDetail.Visible = false; //nếu là kho vision
                else grvMaster.Columns[colDetail.FieldName].VisibleIndex = grvMaster.VisibleColumns.Count - 1;

                if (chkAllProduct.Checked) IDTree = 0;
                DataTable dt = TextUtils.LoadDataFromSP($"spGetInventoryStock", "A", new string[] { "@ID", "@Find", "@WarehouseCode", "@IsStock" },
                    new object[] { IDTree, txtFilterText.Text.Trim(), VP, 1 });
                grdMaster.DataSource = dt;

                //LoadDataDetail();
                LoadProductGroupWarehouse();
            }
        }

        private void LoadtreeData()
        {

            string sql = "SELECT * FROM dbo.ProductGroup";
            if (VP == "HCM") sql = "SELECT * FROM dbo.ProductGroup WHERE ID NOT IN (73, 74, 75, 77)";

            DataTable dt = TextUtils.Select(sql);
            treeData.DataSource = dt;

            if (treeData.Nodes.Count > 0)
            {
                treeData.FocusedNode = treeData.Nodes[0];
            }
        }

        void LoadProductGroupWarehouse()
        {
            int productGroupID = TextUtils.ToInt(treeData.GetFocusedRowCellValue(colIDTree));

            WarehouseModel warehouse = SQLHelper<WarehouseModel>.FindByAttribute("WarehouseCode", VP).FirstOrDefault();
            warehouse = warehouse ?? new WarehouseModel();

            DataTable dt = TextUtils.LoadDataFromSP("spGetProductGroupWarehouse", "A",
                                                    new string[] { "@WarehouseID", "@ProductGroupID" }, new object[] { warehouse.ID, productGroupID });
            grdProductGroupWarehouse.DataSource = dt;

        }

        private void treeData_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (Lib.LockEvents) return;
            loadData();
        }

        void LoadDataDetail()
        {
            int WarehouseID = VP.ToUpper() == "HN" ? 1 :
                             VP.ToUpper() == "HCM" ? 2 : 3;

            DateTime time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-3);
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colProductSaleID));
            DataTable dt = TextUtils.LoadDataFromSP(StoreProcedures.spLoadBillImportInTwoMonth, "A", new string[] { "@ID", "@First", "@WarehouseID" }, new object[] { id, time, WarehouseID }); //1 : VP HN
            grdDetail.DataSource = dt;
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            sfd.FileName = $"DanhSachTonKho_HN_{DateTime.Now.ToString("ddMMyy")}.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;

                grvMaster.OptionsPrint.UsePrintStyles = true;

                try
                {
                    grvMaster.ExportToXls(sfd.FileName, optionsEx);
                    Process.Start(sfd.FileName);
                }
                catch (Exception)
                {
                }
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int rowHandle = grvMaster.FocusedRowHandle;
            int ProductSaleId = TextUtils.ToInt(grvMaster.GetRowCellValue(rowHandle, colProductSaleID));
            int wareHouseID = TextUtils.ToInt(grvMaster.GetRowCellValue(rowHandle, colWarehouseID));

            int id = TextUtils.ToInt(grvMaster.GetRowCellValue(rowHandle, colIDMaster));
            //InventoryModel inventory = SQLHelper<InventoryModel>.FindByID(id);
            InventoryStockModel inventory = SQLHelper<InventoryStockModel>.FindByID(id);

            frmInventoryStock frm = new frmInventoryStock();
            //frm.productSaleID = ProductSaleId;
            //frm.warehouseID = wareHouseID;
            //frm.inventory = inventory;
            frm.inventoryStock = inventory;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadtreeData();
                grvMaster.FocusedRowHandle = rowHandle;
            }
        }

        private void grdMaster_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDataDetail();
        }

        private void grvDetail_KeyDown(object sender, KeyEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(view.GetFocusedRowCellValue(view.FocusedColumn)));
                e.Handled = true;
            }
        }

        private void grvMaster_KeyDown(object sender, KeyEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(view.GetFocusedRowCellValue(view.FocusedColumn)));
                e.Handled = true;
            }
        }

        private void chkAllProduct_CheckedChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            var focusedrowhandle = grvMaster.FocusedRowHandle;
            frmInventoryStockImportExcel frm = new frmInventoryStockImportExcel();
            WarehouseModel wareHouse = SQLHelper<WarehouseModel>.FindByAttribute("WarehouseCode", VP).FirstOrDefault();
            frm.wareHouseID = wareHouse.ID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
                grvMaster.FocusedRowHandle = focusedrowhandle;
            }
        }

        private void grvMaster_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                
                
                    GridView gridView = (GridView)sender;
                    if (gridView.FocusedRowHandle == e.RowHandle)
                    {
                        e.Appearance.BackColor = Color.LightYellow;
                        e.Appearance.ForeColor = Color.Black;
                    }
                
            }
        }

        private void btnPOStock_Click(object sender, EventArgs e)
        {
            List<ProjectPartlistPurchaseRequestDTO> listData = new List<ProjectPartlistPurchaseRequestDTO>();
            int NumberInStoreLast = 0;
            int MinQuantity = 0;
            int[] listRow = grvMaster.GetSelectedRows();
            foreach (int item in listRow)
            {
                int isEnough = TextUtils.ToInt(grvMaster.GetRowCellValue(item, colIsEnough));
                if (isEnough != 0)
                {
                    MessageBox.Show($"Vui lòng chỉ chọn những sản phẩm [Tồn CK] dưới mức [Tồn tối thiểu]", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                //ProjectPartlistPurchaseRequestModel prjPartList = new ProjectPartlistPurchaseRequestModel();
                ProjectPartlistPurchaseRequestDTO prjPartList = new ProjectPartlistPurchaseRequestDTO();
                NumberInStoreLast = TextUtils.ToInt(grvMaster.GetRowCellValue(item, colNumberInStoreCuoiKy));
                MinQuantity = TextUtils.ToInt(grvMaster.GetRowCellValue(item, colMinQuantity));

                prjPartList.ProductSaleID = TextUtils.ToInt(grvMaster.GetRowCellValue(item, colProductSaleID));
                prjPartList.Quantity = MinQuantity - NumberInStoreLast;
                listData.Add(prjPartList);
            }

            frmPONCCDetailNew frm = new frmPONCCDetailNew();
            frm.listRequest = listData;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }

        private void btnBorrowNCCReport_Click(object sender, EventArgs e)
        {
            frmInventoryBorrowNCC frm = new frmInventoryBorrowNCC();
            frm.warehouseID = 1;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }

        private void btnRequestBorrow_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, DataTable> groupedData = GetSelectedRowsDataGrouped();
                if (groupedData == null || groupedData.Count == 0) return;
                if (groupedData.Count > 1)
                {
                    MessageBox.Show($"Bạn chọn sản phẩm từ {groupedData.Count} kho\nNên phần mềm sẽ tự động tạo {groupedData.Count} phiếu mượn", "Thông báo");
                }
                foreach (var kvp in groupedData)
                {
                    string key = kvp.Key;
                    DataTable data = kvp.Value;

                    frmBillExportDetailNew frm = new frmBillExportDetailNew(false);
                    BillExportModel billExport = new BillExportModel();
                    BillExportDetailModel billExportDetail = new BillExportDetailModel();
                    //string cname = TextUtils.ToString(data.Rows[0]["CustomerName"]);

                    billExport.WarehouseID = TextUtils.ToInt(data.Rows[0]["WarehouseID"]);
                    billExport.KhoTypeID = TextUtils.ToInt(data.Rows[0]["KhoTypeID"]);

                    lstTonCk = new DataTable();
                    lstTonCk.Columns.Add("ProductSaleID", typeof(int));
                    lstTonCk.Columns.Add("TotalQuantityLast", typeof(decimal));
                    DataRow dataRow = lstTonCk.NewRow();
                    dataRow["ProductSaleID"] = TextUtils.ToInt(data.Rows[0]["ProductID"]);
                    dataRow["TotalQuantityLast"] = TextUtils.ToDecimal(data.Rows[0]["TotalQuantityLast"]);

                    lstTonCk.Rows.Add(dataRow);
                    frm.lstTonCk = lstTonCk;

                    frm.dtDetail = data;
                    frm.billExport = billExport;

                    //frm.Text = $"Yêu cầu xuất hóa đơn {cname}";
                    frm.isBorrow = true;
                    frm.WarehouseCode = VP;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }

        private Dictionary<string, DataTable> GetSelectedRowsDataGrouped()
        {
            Dictionary<string, DataTable> groupedData = new Dictionary<string, DataTable>();
            try
            {

                int[] selectedRowHandles = grvMaster.GetSelectedRows();
                if (selectedRowHandles.Length == 0)
                {
                    TextUtils.ShowError("Không có dòng nào được chọn!");
                    return null;
                }

                foreach (int rowHandle in selectedRowHandles)
                {
                    if (rowHandle >= 0)
                    {
                        string productNewCode = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProductNewCode"));
                        decimal tonCK = TextUtils.ToInt(grvMaster.GetRowCellValue(rowHandle, "TotalQuantityLast"));
                        if (tonCK <= 0)
                        {
                            MessageBox.Show($"Danh sách bạn chọn có sản phẩm không đủ số lượng tồn kho", "Thông báo");
                            break;
                        }
                        ;
                        int productGroupID = TextUtils.ToInt(grvMaster.GetRowCellValue(rowHandle, "ProductGroupID"));
                        int warehouseID = TextUtils.ToInt(grvMaster.GetRowCellValue(rowHandle, "WarehouseID"));
                        string warehouseCode = SQLHelper<WarehouseModel>.FindByID(warehouseID).WarehouseCode;
                        string productGroupName = SQLHelper<ProductGroupModel>.FindByID(productGroupID).ProductGroupName;
                        //string key = $"{customerID}_{productID}";
                        string key = $"{warehouseID}_{productGroupID}";




                        // Kiểm tra nếu nhóm dữ liệu đã tồn tại hay chưa
                        if (!groupedData.ContainsKey(key))
                        {
                            DataTable dt = new DataTable();
                            //dt.Columns.Add("POKHDetailID", typeof(int));
                            dt.Columns.Add("ProductGroupName", typeof(string));
                            dt.Columns.Add("STT", typeof(int));
                            dt.Columns.Add("ProductSaleID", typeof(int));
                            dt.Columns.Add("CustomerID", typeof(int));
                            dt.Columns.Add("Qty", typeof(int));
                            dt.Columns.Add("ProductName", typeof(string));
                            dt.Columns.Add("ProductID", typeof(string));
                            dt.Columns.Add("CustomerName", typeof(string));
                            dt.Columns.Add("ProductNewCode", typeof(string));
                            dt.Columns.Add("ProductCode", typeof(string));
                            dt.Columns.Add("ProjectID", typeof(string));
                            dt.Columns.Add("ProjectName", typeof(string));
                            dt.Columns.Add("ProjectCode", typeof(string));
                            dt.Columns.Add("Deliver", typeof(string));
                            dt.Columns.Add("Address", typeof(string));
                            dt.Columns.Add("Unit", typeof(string));
                            dt.Columns.Add("KhoTypeID", typeof(int));
                            dt.Columns.Add("NameNCC", typeof(string));
                            dt.Columns.Add("ExpectReturnDate", typeof(DateTime));
                            dt.Columns.Add("WarehouseID", typeof(int));
                            dt.Columns.Add("WarehouseCode", typeof(string));
                            dt.Columns.Add("TotalQuantityLast", typeof(decimal));
                            dt.Columns.Add("TotalInventory", typeof(decimal));
                            dt.Columns.Add("TotalQty", typeof(decimal));
                            dt.Columns.Add("ID", typeof(decimal));
                            dt.Columns.Add("ProjectCodeExport", typeof(string));

                            groupedData[key] = dt;
                        }

                        // Thêm dữ liệu vào nhóm tương ứng
                        DataTable targetTable = groupedData[key];
                        DataRow row = targetTable.NewRow();
                        List<ProductGroupModel> productGroup = new List<ProductGroupModel>();
                        row["ProductGroupName"] = productGroupName;
                        //productGroup = SQLHelper<ProductGroupModel>.FindByAttribute("ProductGroupName", productGroupName);
                        row["KhoTypeID"] = productGroupID;
                        row["Qty"] = TextUtils.ToInt(grvMaster.GetRowCellValue(rowHandle, "Qty"));
                        row["ProductName"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProductName"));
                        row["ProductID"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProductSaleID"));
                        row["ProductCode"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProductCode"));
                        //row["ExpectReturnDate"] = TextUtils.ToDate2(grvMaster.GetRowCellValue(rowHandle, "ExpectReturnDate"));
                        row["ProjectName"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProjectName"));
                        row["ProductNewCode"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProductNewCode"));
                        row["ProjectCode"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "ProjectCode"));
                        row["Unit"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "Unit"));
                        row["Deliver"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "Deliver"));
                        row["TotalQuantityLast"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "TotalQuantityLast"));
                        row["TotalInventory"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "TotalQuantityLast"));
                        row["WarehouseID"] = warehouseID;
                        row["WarehouseCode"] = warehouseCode;
                        //row["CustomerName"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "CustomerName"));
                        //row["Address"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "Address"));
                        //row["SupplierName"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "SupplierName"));
                        //row["EID"] = eid;
                        //row["CustomerID"] = customerID;

                        row["STT"] = targetTable.Rows.Count + 1;
                        row["TotalQty"] = TextUtils.ToString(grvMaster.GetRowCellValue(rowHandle, "TotalQuantityLast"));

                        targetTable.Rows.Add(row);
                    }
                }

                return groupedData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
                return groupedData = new Dictionary<string, DataTable>();
            }
        }

        private void treeData_DoubleClick(object sender, EventArgs e)
        {
            btnEditProductGroup_Click(null, null);
        }

        private void btnEditProductGroup_Click(object sender, EventArgs e)
        {
            WarehouseModel warehouse = SQLHelper<WarehouseModel>.FindByAttribute("WarehouseCode", VP).FirstOrDefault();
            warehouse = warehouse ?? new WarehouseModel();
            //EditClick = 1;
            int ID = TextUtils.ToInt(treeData.FocusedNode.GetValue(colIDTree));
            if (ID == 0) return;
            ProductGroupModel model = (ProductGroupModel)ProductGroupBO.Instance.FindByPK(ID);
            frmProductGroupDetail frm = new frmProductGroupDetail(warehouse.ID);
            frm.productGroup = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadtreeData();
            }
        }

        private void btnGetProduct_Click(object sender, EventArgs e)
        {
            var listTSAsset = SQLHelper<TSAssetManagementModel>.FindAll();
            //var listTSAsset = SQLHelper<TSAssetManagementModel>.FindAll().Where(x => x.TSCodeNCC == "KES036").ToList();
            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát", ""))
            {
                foreach (TSAssetManagementModel item in listTSAsset)
                {
                    AutoInsertProductSale(item);
                }
            }


            loadData();
        }

        void AutoInsertProductSale(TSAssetManagementModel tSAsset)
        {
            var productgroupModel = SQLHelper<ProductGroupModel>.FindByAttribute("ProductGroupID", "C").FirstOrDefault();
            int groupID = 0;
            if (productgroupModel != null) groupID = productgroupModel.ID;


            string productCode = tSAsset.TSCodeNCC;
            var exp1 = new Expression("ProductGroupID", groupID);
            var exp2 = new Expression("ProductCode", productCode);
            ProductSaleModel product = SQLHelper<ProductSaleModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
            product = product ?? new ProductSaleModel();
            //if (product.ID > 0) return;
            product.ProductGroupID = groupID;
            product.ProductCode = productCode;
            product.ProductName = tSAsset.TSAssetName;

            UnitCountModel unit = SQLHelper<UnitCountModel>.FindByID(TextUtils.ToInt(tSAsset.UnitID));
            product.Unit = unit == null ? "" : unit.UnitName;

            if (product.ID > 0)
            {
                SQLHelper<ProductSaleModel>.Update(product);
            }
            else
            {
                SupplierModel supplier = SQLHelper<SupplierModel>.FindByID(TextUtils.ToInt(tSAsset.SupplierID));
                product.SupplierName = supplier == null ? "" : supplier.SupplierName;

                product.ProductNewCode = loadNewCode(groupID);
                product.ID = SQLHelper<ProductSaleModel>.Insert(product).ID;

                //productSale.ID = (int)ProductSaleBO.Instance.Insert(productSale);
                InventoryModel inventoryModel = new InventoryModel();
                inventoryModel.WarehouseID = 1;
                inventoryModel.ProductSaleID = product.ID;
                InventoryBO.Instance.Insert(inventoryModel);
            }
        }


        string loadNewCode(int groupID)
        {
            string _NewCodeRTC;

            DataSet ds = TextUtils.LoadDataSetFromSP("spLoadNewCodeRTC", new string[] { "@Group" }, new object[] { groupID });
            string code = "";
            string codeRTC = TextUtils.ToString(ds.Tables[1].Rows[0][0]);

            if (ds.Tables[0].Rows.Count == 0)
            {
                _NewCodeRTC = codeRTC + "000000001";
            }
            else
            {
                if (!codeRTC.Contains("HCM"))
                {
                    code = TextUtils.ToString(ds.Tables[0].Rows[0][0]).Replace(codeRTC, "");
                    int stt = TextUtils.ToInt(code) + 1;
                    for (int i = 0; i < (9 - stt.ToString().Length); i++)
                    {
                        codeRTC = codeRTC + "0";
                    }
                    _NewCodeRTC = codeRTC + stt.ToString();
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
                    _NewCodeRTC = codeRTC + indexString.ToString();
                }
            }
            return _NewCodeRTC;
        }

        private void grvMaster_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnConvertProduct_Click(object sender, EventArgs e)
        {
            try
            {

                ProductGroupModel vhProductGroup = SQLHelper<ProductGroupModel>.FindByAttribute("ProductGroupID", "TH").FirstOrDefault() ?? new ProductGroupModel();
                if (vhProductGroup.ID <= 0)
                {
                    MessageBox.Show("Không tồn tại ProductGroup có mã là TH");
                    return;
                }
                List<ProductSaleModel> _lstAllProductSale = SQLHelper<ProductSaleModel>.FindAll();
                using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát", ""))
                {

                    DataTable dt = TextUtils.GetDataTableFromSP("spGetOfficeSupply", new string[] { "@KeyWord" }, new object[] { "" });
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ProductSaleModel ps = _lstAllProductSale.Where(p => p.ProductCode == dt.Rows[i]["CodeRTC"].ToString()).FirstOrDefault() ?? new ProductSaleModel();
                        //if (ps.ID < =) continue;
                        //ps = new ProductSaleModel();
                        ps.ProductCode = dt.Rows[i]["CodeRTC"].ToString();
                        ps.ProductName = dt.Rows[i]["NameNCC"].ToString();
                        ps.Unit = dt.Rows[i]["Unit"].ToString();
                        ps.ProductGroupID = vhProductGroup.ID;
                        //ps.CreatedDate = DateTime.Now;


                        if (ps.ID <= 0)
                        {
                            ps.ProductNewCode = loadNewCode(vhProductGroup.ID);
                            SQLHelper<ProductSaleModel>.Insert(ps);


                            InventoryModel inventoryModel = new InventoryModel();
                            inventoryModel.WarehouseID = 1;
                            inventoryModel.ProductSaleID = ps.ID;
                            InventoryBO.Instance.Insert(inventoryModel);
                        }
                        else SQLHelper<ProductSaleModel>.Update(ps);

                    }
                }

                //  Lib.ExcuteSQL(strInsert);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        //PQ.Chien - update - 09/08/2025
        private void btnAddStock_Click(object sender, EventArgs e)
        {
            int rowHandle = grvMaster.FocusedRowHandle;
            int ProductSaleId = TextUtils.ToInt(grvMaster.GetRowCellValue(rowHandle, colProductSaleID));
            int wareHouseID = TextUtils.ToInt(grvMaster.GetRowCellValue(rowHandle, colWarehouseID));
            frmInventoryStock frm = new frmInventoryStock();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
                grvMaster.FocusedRowHandle = rowHandle;
            }
        }

        private void btnDeleteStock_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            string productCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(gridColumn2));
            //InventoryModel model = SQLHelper<InventoryModel>.FindByID(id);

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xóa hàng tồn kho stock có mã {productCode} không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                //model.IsStock = false;
                //model.MinQuantity = 0;
                //SQLHelper<InventoryModel>.Update(model);


                var myDict = new Dictionary<string, object>()
                {
                    {InventoryStockModel_Enum.IsDeleted.ToString(), true},
                    {InventoryStockModel_Enum.UpdatedBy.ToString(), Global.AppUserName},
                    {InventoryStockModel_Enum.UpdatedDate.ToString(), DateTime.Now},
                };

                SQLHelper<InventoryStockModel>.UpdateFieldsByID(myDict, id);
                loadData();
            }
        }
        //END
    }
}