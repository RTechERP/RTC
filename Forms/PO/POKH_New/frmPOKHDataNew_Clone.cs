using BMS.Model;
using BMS.Utils;
using DevExpress.XtraTreeList.Nodes;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
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
    public partial class frmPOKHDataNew_Clone : _Forms
    {
        int wareHouseID = 0;
        public DataTable dt = new DataTable();
        List<int> listSelected = new List<int>();

        public frmPOKHDataNew_Clone(int wareHouseID)
        {
            InitializeComponent();
            this.wareHouseID = wareHouseID;
        }

        private void frmPOKHDataNew_Load(object sender, EventArgs e)
        {
            this.Text += $" - {this.Tag}";
            LoadCustomer();
            LoadProject();
            LoadProductGroup();
            LoadWarehouse();

            LoadData();
        }


        void LoadCustomer()
        {
            var exp1 = new Expression("IsDeleted", 1, "<>");
            List<CustomerModel> list = SQLHelper<CustomerModel>.FindByExpression(exp1).ToList();
            cboCustomer.Properties.ValueMember = "ID";
            cboCustomer.Properties.DisplayMember = "CustomerName";
            cboCustomer.Properties.DataSource = list;
        }

        void LoadProject()
        {
            List<ProjectModel> list = SQLHelper<ProjectModel>.FindAll().OrderByDescending(x => x.CreatedDate).ToList();
            cboProject.Properties.ValueMember = "ID";
            cboProject.Properties.DisplayMember = "ProjectName";
            cboProject.Properties.DataSource = list;
        }

        void LoadProductGroup()
        {
            List<ProductGroupModel> list = SQLHelper<ProductGroupModel>.FindAll();
            cboProductGroup.Properties.ValueMember = "ID";
            cboProductGroup.Properties.DisplayMember = "ProductGroupName";
            cboProductGroup.Properties.DataSource = list;
        }

        void LoadWarehouse()
        {
            List<WarehouseModel> listWarehouses = SQLHelper<WarehouseModel>.FindAll();
            //cboWarehouse.Properties.ValueMember = "WarehouseCode";
            //cboWarehouse.Properties.DisplayMember = "WarehouseName";
            //cboWarehouse.Properties.DataSource = listWarehouses;
            //cboWarehouse.EditValue = this.Tag;

            toolStripDropDownButton1.DropDownItems.Clear();
            btnDropdownTranferWarehouse.DropDownItems.Clear();
            foreach (var item in listWarehouses)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem()
                {
                    Tag = item.WarehouseCode,
                    Text = item.WarehouseName,
                };
                menuItem.Click += btnBillExportRequest_Click;
                toolStripDropDownButton1.DropDownItems.Add(menuItem);


                ToolStripMenuItem menuItemTranfer = new ToolStripMenuItem()
                {
                    Tag = item.WarehouseCode,
                    Text = item.WarehouseName,
                };
                menuItemTranfer.Click += MenuItemTranfer_Click;
                btnDropdownTranferWarehouse.DropDownItems.Add(menuItemTranfer);
            }
        }

        private void MenuItemTranfer_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            BillExportRequestNew(true, menuItem);
        }

        void LoadData()
        {
            int customerID = TextUtils.ToInt(cboCustomer.EditValue);
            int projectID = TextUtils.ToInt(cboProject.EditValue);
            int productGroupID = TextUtils.ToInt(cboProductGroup.EditValue);
            dt = TextUtils.LoadDataFromSP("spGetPOKHRequestExport", "A",
                                                   new string[] { "@WarehouseID", "@CustomerID", "@ProjectID", "@ProductGroupID", "@Keyword" },
                                                   new object[] { wareHouseID, customerID, projectID, productGroupID, txtKeyword.Text.Trim() });
            //dt.Columns.Add("IsSelected", typeof(bool));

            foreach (DataRow row in dt.Rows)
            {
                int id = TextUtils.ToInt(row["POKHDetailID"]);
                row["IsSelected"] = listSelected.Contains(id);
            }
            treeList.KeyFieldName = "POKHDetailID";
            treeList.ParentFieldName = "ParentID";
            treeList.DataSource = dt;
            treeList.ExpandAll();
        }

        private void AddFormNew(DataTable dtDetailForForm, BillExportModel model, List<frmBillExportDetailNew> formList)
        {
            frmBillExportDetailNew frmEnd = new frmBillExportDetailNew(false);
            //frmEnd.customerID = customerID;
            //frmEnd.KhoTypeID = productGroupID;
            //frmEnd.saleAdminID = saleAdminID;
            frmEnd.billExport = model;
            frmEnd.dtDetail = dtDetailForForm.Copy();
            frmEnd.WarehouseCode = TextUtils.ToString(this.Tag);
            frmEnd.isPOKH = true;
            formList.Add(frmEnd);
            dtDetailForForm.Clear();
        }

        private void btnBillExportRequest_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            BillExportRequestNew(false, menuItem);


            //try
            //{
            //    ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;

            //    treeList.CloseEditor();
            //    dt.AcceptChanges();
            //    //int[] selectedRows = grvData.GetSelectedRows();
            //    if (listSelected.Count() <= 0)
            //    {
            //        MessageBox.Show("Vui lòng chọn sản phẩm muốn Yêu cầu xuất kho!", "Thông báo");
            //        return;
            //    }

            //    string warehouseCode = TextUtils.ToString(menuItem.Tag);
            //    WarehouseModel warehouse = SQLHelper<WarehouseModel>.FindByAttribute("WarehouseCode", warehouseCode).FirstOrDefault() ?? new WarehouseModel();

            //    List<string> productNewCodes = new List<string>();
            //    for (int i = 0; i < listSelected.Count; i++)
            //    {
            //        DataRow[] dateRows = dt.Select($"POKHDetailID = {listSelected[i]}");
            //        DataRow dataRowSelect = dateRows.Length > 0 ? dateRows[0] : null;

            //        if (dataRowSelect == null) continue;

            //        //if (!ValidateKeep(dataRowSelect, warehouse.ID, out string productNewCode)) return;
            //        if (!ValidateKeep(dataRowSelect, warehouse.ID, out string productNewCode))
            //        {
            //            if (productNewCodes.Contains(productNewCode) || string.IsNullOrWhiteSpace(productNewCode)) continue;
            //            productNewCodes.Add(productNewCode);
            //        }
            //    }

            //    DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn yêu cầu xuất kho danh sách sản phẩm đã chọn từ [{menuItem.Text}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (dialog == DialogResult.No) return;

            //    if (productNewCodes.Count > 0)
            //    {
            //        MessageBox.Show($"Các sản phẩm có mã nội bộ [{string.Join(";", productNewCodes)}] sẽ không được yêu cầu xuất kho vì không đủ số lượng!", "Thông báo");
            //    }

            //    //Add thông tin chi tiết
            //    //DataTable dtDetail = dt.Clone();
            //    DataTable dtDetail = TextUtils.LoadDataFromSP("spGetBillExportDetail", "A", new string[] { "@BillID" }, new object[] { 0 });
            //    int stt = 0;
            //    for (int i = 0; i < listSelected.Count; i++)
            //    {
            //        //int row = selectedRows[i];
            //        //var dataRowSelect = grvData.GetDataRow(row);
            //        DataRow[] dateRows = dt.Select($"POKHDetailID = {listSelected[i]}");
            //        DataRow dataRowSelect = dateRows.Length > 0 ? dateRows[0] : null;

            //        if (dataRowSelect == null) continue;
            //        int productID = TextUtils.ToInt(dataRowSelect["ProductID"]);
            //        string productNewCode = TextUtils.ToString(dataRowSelect["ProductNewCode"]);
            //        decimal quantityRemain = TextUtils.ToDecimal(dataRowSelect["QuantityRemain"]);
            //        if (productID <= 0 || string.IsNullOrWhiteSpace(productNewCode) || quantityRemain <= 0) continue;

            //        if (!ValidateKeep(dataRowSelect, warehouse.ID, out string productNewCodeShow)) continue;

            //        stt++;
            //        DataRow dataRow = dtDetail.NewRow();
            //        dataRow["ProductID"] = productID;
            //        //dataRow["Qty"] = TextUtils.ToDecimal(dataRowSelect["QuantityRemain"]);
            //        dataRow["Qty"] = TextUtils.ToDecimal(dataRowSelect["QuantityRequestExport"]);
            //        dataRow["ProjectID"] = TextUtils.ToInt(dataRowSelect["ProjectID"]);
            //        dataRow["Note"] = TextUtils.ToString(dataRowSelect["PONumber"]);
            //        dataRow["ProductCode"] = TextUtils.ToString(dataRowSelect["ProductCode"]);
            //        dataRow["ProductNewCode"] = productNewCode;
            //        dataRow["ProductName"] = TextUtils.ToString(dataRowSelect["ProductName"]);
            //        dataRow["Unit"] = TextUtils.ToString(dataRowSelect["Unit"]);
            //        dataRow["ProductGroupName"] = TextUtils.ToString(dataRowSelect["ProductGroupName"]);
            //        dataRow["ItemType"] = TextUtils.ToString(dataRowSelect["ItemType"]);
            //        dataRow["ProjectNameText"] = TextUtils.ToString(dataRowSelect["ProjectName"]);
            //        dataRow["ProjectCodeText"] = TextUtils.ToString(dataRowSelect["ProjectCode"]);
            //        dataRow["ProjectCodeExport"] = TextUtils.ToString(dataRowSelect["ProjectCodeExport"]);
            //        dataRow["productGroupID"] = TextUtils.ToInt(dataRowSelect["productGroupID"]);
            //        dataRow["POKHID"] = TextUtils.ToInt(dataRowSelect["POKHID"]);
            //        dataRow["POKHDetailID"] = TextUtils.ToInt(dataRowSelect["POKHDetailID"]); ;
            //        dataRow["STT"] = stt;
            //        dataRow["UserReceiver"] = TextUtils.ToString(dataRowSelect["UserReceiver"]);
            //        dataRow["QuantityRemain"] = TextUtils.ToInt(dataRowSelect["QuantityRemain"]);

            //        dataRow["CustomerID"] = TextUtils.ToInt(dataRowSelect["CustomerID"]);
            //        dataRow["KhoTypeID"] = TextUtils.ToInt(dataRowSelect["KhoTypeID"]);
            //        dataRow["UserID"] = TextUtils.ToInt(dataRowSelect["UserID"]);
            //        dataRow["IsMerge"] = TextUtils.ToBoolean(dataRowSelect["IsMerge"]);
            //        dataRow["ProductFullName"] = TextUtils.ToString(dataRowSelect["GuestCode"]);
            //        //
            //        dataRow["ParentID"] = TextUtils.ToString(dataRowSelect["ParentID"]);
            //        dataRow["TotalInventory"] = TextUtils.ToString(dataRowSelect["TotalInventory"]);
            //        dataRow["UnitPricePurchase"] = TextUtils.ToString(dataRowSelect["UnitPricePurchase"]);
            //        dataRow["BillCode"] = TextUtils.ToString(dataRowSelect["BillCode"]);
            //        dataRow["UnitPricePOKH"] = TextUtils.ToString(dataRowSelect["UnitPricePOKH"]);
            //        dataRow["ChildID"] = TextUtils.ToString(dataRowSelect["POKHDetailID"]);
            //        dataRow["POKHDetailIDActual"] = TextUtils.ToString(dataRowSelect["POKHDetailID"]);
            //        dataRow["PONumber"] = TextUtils.ToString(dataRowSelect["PONumber"]);
            //        dataRow["POCode"] = TextUtils.ToString(dataRowSelect["POCode"]);
            //        dtDetail.Rows.Add(dataRow);
            //    }

            //    DataView view = new DataView(dtDetail);
            //    DataTable distinctValues = view.ToTable(true, new string[] { "CustomerID", "KhoTypeID" });
            //    var listBillExports = distinctValues.AsEnumerable().Where(row => !row.IsNull("CustomerID") && !row.IsNull("KhoTypeID")).ToList();
            //    if (listBillExports.Count > 1)
            //    {
            //        MessageBox.Show($"Bạn chọn sản phẩm từ {listBillExports.Count} Khách hàng hoặc Loại kho.\nNên phần mềm sẽ tự động tạo {listBillExports.Count} phiếu xuất.", "Thông báo");
            //    }

            //    for (int i = 0; i < listBillExports.Count; i++)
            //    {
            //        int customerID = TextUtils.ToInt(listBillExports[i]["CustomerID"]);
            //        int khoTypeID = TextUtils.ToInt(listBillExports[i]["KhoTypeID"]);

            //        DataRow[] dataExport = dtDetail.Select($"CustomerID = {customerID} AND KhoTypeID = {khoTypeID}");
            //        if (dataExport.Length <= 0) continue;
            //        BillExportModel billExport = new BillExportModel();
            //        billExport.CustomerID = customerID;
            //        //billExport.UserID = TextUtils.ToInt(dataExport[0]["UserID"]);
            //        billExport.UserID = Global.UserID;
            //        billExport.KhoTypeID = khoTypeID;
            //        billExport.ProductType = 1;
            //        billExport.IsMerge = TextUtils.ToBoolean(dataExport[0]["IsMerge"]); ;
            //        billExport.Status = 6;
            //        billExport.RequestDate = DateTime.Now;

            //        frmBillExportDetailNew frm = new frmBillExportDetailNew();
            //        frm.billExport = billExport;
            //        frm.dtDetail = dataExport.CopyToDataTable();
            //        //frm.WarehouseCode = TextUtils.ToString(this.Tag);
            //        frm.WarehouseCode = warehouse.WarehouseCode;
            //        frm.isPOKH = true;
            //        if (frm.ShowDialog() == DialogResult.OK)
            //        {
            //            if (i == listBillExports.Count - 1)
            //            {
            //                LoadData();
            //                listSelected.Clear();
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            ////}
        }


        void BillExportRequest(bool isTranfer, ToolStripMenuItem menuItem)
        {
            try
            {
                //ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;

                treeList.CloseEditor();
                dt.AcceptChanges();
                //int[] selectedRows = grvData.GetSelectedRows();
                if (listSelected.Count() <= 0)
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm muốn Yêu cầu xuất kho!", "Thông báo");
                    return;
                }

                string warehouseCode = TextUtils.ToString(menuItem.Tag);
                WarehouseModel warehouse = SQLHelper<WarehouseModel>.FindByAttribute("WarehouseCode", warehouseCode).FirstOrDefault() ?? new WarehouseModel();

                List<string> productNewCodes = new List<string>();
                for (int i = 0; i < listSelected.Count; i++)
                {
                    DataRow[] dateRows = dt.Select($"POKHDetailID = {listSelected[i]}");
                    DataRow dataRowSelect = dateRows.Length > 0 ? dateRows[0] : null;

                    if (dataRowSelect == null) continue;

                    //if (!ValidateKeep(dataRowSelect, warehouse.ID, out string productNewCode)) return;
                    if (!ValidateKeep(dataRowSelect, warehouse.ID, out string productNewCode))
                    {
                        if (productNewCodes.Contains(productNewCode) || string.IsNullOrWhiteSpace(productNewCode)) continue;
                        productNewCodes.Add(productNewCode);
                    }
                }

                DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn yêu cầu xuất kho danh sách sản phẩm đã chọn từ [{menuItem.Text}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.No) return;

                if (productNewCodes.Count > 0)
                {
                    MessageBox.Show($"Các sản phẩm có mã nội bộ [{string.Join(";", productNewCodes)}] sẽ không được yêu cầu xuất kho vì không đủ số lượng!", "Thông báo");
                }

                //Add thông tin chi tiết
                //DataTable dtDetail = dt.Clone();
                DataTable dtDetail = TextUtils.LoadDataFromSP("spGetBillExportDetail", "A", new string[] { "@BillID" }, new object[] { 0 });
                int stt = 0;
                for (int i = 0; i < listSelected.Count; i++)
                {
                    //int row = selectedRows[i];
                    //var dataRowSelect = grvData.GetDataRow(row);
                    DataRow[] dateRows = dt.Select($"POKHDetailID = {listSelected[i]}");
                    DataRow dataRowSelect = dateRows.Length > 0 ? dateRows[0] : null;

                    if (dataRowSelect == null) continue;
                    int productID = TextUtils.ToInt(dataRowSelect["ProductID"]);
                    string productNewCode = TextUtils.ToString(dataRowSelect["ProductNewCode"]);
                    decimal quantityRemain = TextUtils.ToDecimal(dataRowSelect["QuantityRemain"]);
                    if (productID <= 0 || string.IsNullOrWhiteSpace(productNewCode) || quantityRemain <= 0) continue;

                    if (!ValidateKeep(dataRowSelect, warehouse.ID, out string productNewCodeShow)) continue;

                    stt++;
                    DataRow dataRow = dtDetail.NewRow();
                    dataRow["ProductID"] = productID;
                    //dataRow["Qty"] = TextUtils.ToDecimal(dataRowSelect["QuantityRemain"]);
                    dataRow["Qty"] = TextUtils.ToDecimal(dataRowSelect["QuantityRequestExport"]);
                    dataRow["ProjectID"] = TextUtils.ToInt(dataRowSelect["ProjectID"]);
                    dataRow["Note"] = TextUtils.ToString(dataRowSelect["PONumber"]);
                    dataRow["ProductCode"] = TextUtils.ToString(dataRowSelect["ProductCode"]);
                    dataRow["ProductNewCode"] = productNewCode;
                    dataRow["ProductName"] = TextUtils.ToString(dataRowSelect["ProductName"]);
                    dataRow["Unit"] = TextUtils.ToString(dataRowSelect["Unit"]);
                    dataRow["ProductGroupName"] = TextUtils.ToString(dataRowSelect["ProductGroupName"]);
                    dataRow["ItemType"] = TextUtils.ToString(dataRowSelect["ItemType"]);
                    dataRow["ProjectNameText"] = TextUtils.ToString(dataRowSelect["ProjectName"]);
                    dataRow["ProjectCodeText"] = TextUtils.ToString(dataRowSelect["ProjectCode"]);
                    dataRow["ProjectCodeExport"] = TextUtils.ToString(dataRowSelect["ProjectCodeExport"]);
                    dataRow["productGroupID"] = TextUtils.ToInt(dataRowSelect["productGroupID"]);
                    dataRow["POKHID"] = TextUtils.ToInt(dataRowSelect["POKHID"]);
                    dataRow["POKHDetailID"] = TextUtils.ToInt(dataRowSelect["POKHDetailID"]); ;
                    dataRow["STT"] = stt;
                    dataRow["UserReceiver"] = TextUtils.ToString(dataRowSelect["UserReceiver"]);
                    dataRow["QuantityRemain"] = TextUtils.ToInt(dataRowSelect["QuantityRemain"]);

                    dataRow["CustomerID"] = TextUtils.ToInt(dataRowSelect["CustomerID"]);
                    dataRow["KhoTypeID"] = TextUtils.ToInt(dataRowSelect["KhoTypeID"]);
                    dataRow["UserID"] = TextUtils.ToInt(dataRowSelect["UserID"]);
                    dataRow["IsMerge"] = TextUtils.ToBoolean(dataRowSelect["IsMerge"]);
                    dataRow["ProductFullName"] = TextUtils.ToString(dataRowSelect["GuestCode"]);
                    //
                    dataRow["ParentID"] = TextUtils.ToString(dataRowSelect["ParentID"]);
                    dataRow["TotalInventory"] = TextUtils.ToString(dataRowSelect["TotalInventory"]);
                    dataRow["UnitPricePurchase"] = TextUtils.ToString(dataRowSelect["UnitPricePurchase"]);
                    dataRow["BillCode"] = TextUtils.ToString(dataRowSelect["BillCode"]);
                    dataRow["UnitPricePOKH"] = TextUtils.ToString(dataRowSelect["UnitPricePOKH"]);
                    dataRow["ChildID"] = TextUtils.ToString(dataRowSelect["POKHDetailID"]);
                    dataRow["POKHDetailIDActual"] = TextUtils.ToString(dataRowSelect["POKHDetailID"]);
                    dataRow["PONumber"] = TextUtils.ToString(dataRowSelect["PONumber"]);
                    dataRow["POCode"] = TextUtils.ToString(dataRowSelect["POCode"]);
                    dtDetail.Rows.Add(dataRow);
                }

                DataView view = new DataView(dtDetail);
                DataTable distinctValues = view.ToTable(true, new string[] { "CustomerID", "KhoTypeID" });
                var listBillExports = distinctValues.AsEnumerable().Where(row => !row.IsNull("CustomerID") && !row.IsNull("KhoTypeID")).ToList();
                if (listBillExports.Count > 1)
                {
                    MessageBox.Show($"Bạn chọn sản phẩm từ {listBillExports.Count} Khách hàng hoặc Loại kho.\nNên phần mềm sẽ tự động tạo {listBillExports.Count} phiếu xuất.", "Thông báo");
                }

                for (int i = 0; i < listBillExports.Count; i++)
                {
                    int customerID = TextUtils.ToInt(listBillExports[i]["CustomerID"]);
                    int khoTypeID = TextUtils.ToInt(listBillExports[i]["KhoTypeID"]);

                    DataRow[] dataExport = dtDetail.Select($"CustomerID = {customerID} AND KhoTypeID = {khoTypeID}");
                    if (dataExport.Length <= 0) continue;
                    BillExportModel billExport = new BillExportModel();
                    billExport.CustomerID = customerID;
                    //billExport.UserID = TextUtils.ToInt(dataExport[0]["UserID"]);
                    billExport.UserID = Global.UserID;
                    billExport.KhoTypeID = khoTypeID;
                    billExport.ProductType = 1;
                    billExport.IsMerge = TextUtils.ToBoolean(dataExport[0]["IsMerge"]); ;
                    billExport.Status = isTranfer ? 2 : 6;
                    billExport.RequestDate = DateTime.Now;

                    frmBillExportDetailNew frm = new frmBillExportDetailNew(isTranfer);
                    frm.billExport = billExport;
                    frm.dtDetail = dataExport.CopyToDataTable();
                    //frm.WarehouseCode = TextUtils.ToString(this.Tag);
                    frm.WarehouseCode = warehouse.WarehouseCode;
                    frm.isPOKH = true;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (i == listBillExports.Count - 1)
                        {
                            LoadData();
                            listSelected.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }

        string[] unitNames = new string[] { "m", "mét" };
        /// <summary>
        /// Check validate hàng giữ
        /// </summary>
        bool ValidateKeep(DataRow dataRowSelect, int warehouseID, out string productNewCode)
        {
            productNewCode = "";
            if (dataRowSelect == null) return false;

            string unitName = TextUtils.ToString(dataRowSelect["Unit"]);
            if (unitNames.Contains(unitName.Trim().ToLower())) return true;
            //int billExportDetailID = TextUtils.ToInt(dataRowSelect["ID"]);
            int billExportDetailID = 0;
            int productID = TextUtils.ToInt(dataRowSelect["ProductID"]);
            //int projectID = TextUtils.ToInt(dataRowSelect["ProjectID"]);
            int projectID = 0;
            int pokhDetailID = TextUtils.ToInt(dataRowSelect["POKHDetailID"]);
            //decimal qty = TextUtils.ToInt(dataRowSelect["Qty"]);
            decimal totalQty = TextUtils.ToInt(dataRowSelect["QuantityRequestExport"]);

            string productCode = TextUtils.ToString(dataRowSelect["ProductNewCode"]);
            string projectCode = TextUtils.ToString(dataRowSelect["ProjectCode"]);
            string poNumber = TextUtils.ToString(dataRowSelect["PONumber"]);

            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetInventoryProjectImportExport",
                                    new string[] { "@WarehouseID", "@ProductID", "@ProjectID", "@POKHDetailID", "@BillExportDetailID" },
                                    new object[] { warehouseID, productID, projectID, pokhDetailID, billExportDetailID });

            var inventoryProjects = dataSet.Tables[0];

            var dtImport = dataSet.Tables[1];
            var dtExport = dataSet.Tables[2];
            var dtStock = dataSet.Tables[3];

            decimal totalQuantityKeep = inventoryProjects.Rows.Count <= 0 ? 0 : TextUtils.ToDecimal(inventoryProjects.Rows[0]["TotalQuantity"]); //Sl giữ
            totalQuantityKeep = Math.Max(totalQuantityKeep, 0);

            decimal totalQuantityLast = dtStock.Rows.Count <= 0 ? 0 : TextUtils.ToDecimal(dtStock.Rows[0]["TotalQuantityLast"]); ; //SL tồn CK
            totalQuantityLast = Math.Max(totalQuantityLast, 0);

            decimal totalImport = dtImport.Rows.Count <= 0 ? 0 : TextUtils.ToDecimal(dtImport.Rows[0]["TotalImport"]);
            decimal totalExport = dtExport.Rows.Count <= 0 ? 0 : TextUtils.ToDecimal(dtExport.Rows[0]["TotalExport"]);

            decimal quantityRemain = totalImport - totalExport;
            quantityRemain = Math.Max(quantityRemain, 0);

            decimal totalStock = totalQuantityKeep + quantityRemain + totalQuantityLast;
            if (totalQty > totalStock)
            {
                //MessageBox.Show($"Số lượng còn lại của sản phẩm [{productCode}] không đủ!\n" +
                //    $"SL xuất: {totalQty}\n" +
                //    $"SL giữ: {totalQuantityKeep} | Tồn CK: {totalQuantityLast} | Tổng: {totalStock}", "Thông báo");

                productNewCode = $"{productCode} ({totalQty} - {totalStock})\n";
                return false;
            }

            return true;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboCustomer_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboProject_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboProductGroup_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < grvData.RowCount; i++)
            //{
            //    grvData.SetRowCellValue(i, colIsSelected, true);

            //    int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colPOKHDetailID));
            //    if (!listSelected.Contains(id)) listSelected.Add(id);

            //}
        }

        private void bntCancelSelect_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < grvData.RowCount; i++)
            //{
            //    grvData.SetRowCellValue(i, colIsSelected, false);

            //    int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colPOKHDetailID));
            //    listSelected.Remove(id);
            //}
        }

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            //if (e.RowHandle < 0) return;
            //bool isSelected = TextUtils.ToBoolean(treeList.GetRowCellValue(e.RowHandle, colIsSelected));
            //if (isSelected)
            //{
            //    e.Appearance.BackColor = Color.LightYellow;
            //    e.HighPriority = true;
            //}
        }

        private void grvData_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            //grvData.CloseEditor();
            if (treeList.FocusedColumn != colQuantityRequestExport) return;
            decimal qty = TextUtils.ToDecimal(e.Value);
            decimal qtyRemain = TextUtils.ToDecimal(treeList.GetFocusedRowCellValue(colQuantityRemain));

            if (qty > qtyRemain)
            {
                treeList.BeginUpdate();
                e.Valid = false;
                e.ErrorText = $"Số lượng yêu cầu xuất ({qty}) không được nhiều hơn Số lượng còn lại ({qtyRemain})!";
                treeList.EndUpdate();
            }
        }

        private void btnInventoryProject_Click(object sender, EventArgs e)
        {
            if (listSelected.Count() <= 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm muốn giữ!", "Thông báo");
                return;
            }

            for (int i = 0; i < listSelected.Count; i++)
            {
                int pokhDetailID = listSelected[i];
                DataRow[] dateRows = dt.Select($"POKHDetailID = {pokhDetailID}");
                DataRow dataRowSelect = dateRows.Length > 0 ? dateRows[0] : null;

                if (dataRowSelect == null) continue;

                int projectID = TextUtils.ToInt(dataRowSelect["ProjectID"]);
                int productSaleID = TextUtils.ToInt(dataRowSelect["ProductID"]);
                decimal qty = TextUtils.ToDecimal(dataRowSelect["Qty"]);
                string productNewCode = TextUtils.ToString(dataRowSelect["ProductNewCode"]);

                DataTable dtStock = TextUtils.LoadDataFromSP("spGetInventory", "A", new string[] { "@ProductSaleID" }, new object[] { productSaleID });
                if (dtStock.Rows.Count <= 0)
                {
                    MessageBox.Show($"Sản phẩm [{productNewCode}] không có tồn kho!", "Thông báo");
                    return;
                }
                decimal totalQuantityLast = TextUtils.ToDecimal(dtStock.Rows[0]["TotalQuantityLast"]);
                if (totalQuantityLast < qty)
                {
                    MessageBox.Show($"Số lượng Tồn CK của sản phẩm [{productNewCode}] không đủ!\nTồn CK: {totalQuantityLast} | SL PO: {qty}", "Thông báo");
                    return;
                }

                var exp1 = new Expression(InventoryProjectModel_Enum.ProjectID.ToString(), projectID);
                var exp2 = new Expression(InventoryProjectModel_Enum.ProductSaleID.ToString(), productSaleID);
                var exp3 = new Expression(InventoryProjectModel_Enum.POKHDetailID.ToString(), pokhDetailID);
                var exp4 = new Expression(InventoryProjectModel_Enum.WarehouseID.ToString(), wareHouseID);
                var exp5 = new Expression(InventoryProjectModel_Enum.IsDeleted.ToString(), 0);

                InventoryProjectModel inventory = SQLHelper<InventoryProjectModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4).And(exp5)).FirstOrDefault() ?? new InventoryProjectModel();

                inventory.ProjectID = projectID;
                inventory.ProductSaleID = productSaleID;
                inventory.EmployeeID = Global.EmployeeID;
                inventory.WarehouseID = wareHouseID;
                //inventory.Quantity = TextUtils.ToDecimal(dataRowSelect["QuantityRequestExport"]);
                inventory.Quantity = qty;
                inventory.POKHDetailID = pokhDetailID;
                inventory.CustomerID = TextUtils.ToInt(dataRowSelect["CustomerID"]); ;

                if (inventory.Quantity <= 0) continue;
                if (inventory.ID <= 0)
                {
                    SQLHelper<InventoryProjectModel>.Insert(inventory);
                }
                else
                {
                    SQLHelper<InventoryProjectModel>.Update(inventory);
                }
            }

            MessageBox.Show("Giữ hàng thành công!", "Thông báo");
        }

        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void chkIsSelected_EditValueChanged_1(object sender, EventArgs e)
        {
            treeList.CloseEditor();
            bool isSelected = TextUtils.ToBoolean(treeList.GetFocusedRowCellValue(colIsSelected));
            int id = TextUtils.ToInt(treeList.GetFocusedRowCellValue(colPOKHDetailID));

            if (isSelected)
            {
                if (!listSelected.Contains(id)) listSelected.Add(id);
            }
            else
            {
                listSelected.Remove(id);
            }
        }

        private void treeList_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node == null) return;
            bool isSelected = TextUtils.ToBoolean(e.Node.GetValue(colIsSelected));
            if (isSelected)
            {
                e.Appearance.BackColor = Color.LightYellow;
                e.Appearance.Options.UseBackColor = true;
            }
        }

        private void treeList_MouseDown(object sender, MouseEventArgs e)
        {
            var hitInfo = treeList.CalcHitInfo(e.Location);
            if (hitInfo.Node == null) return;

            if (e.Button == MouseButtons.Left)
            {
                TreeListNode parentNode = hitInfo.Node;
                if (parentNode.HasChildren)
                {
                    bool isAllSelected = true;

                    // kiểm tra xem tất cả con đã chọn chưa
                    foreach (TreeListNode childNode in parentNode.Nodes)
                    {
                        if (!TextUtils.ToBoolean(childNode.GetValue(colIsSelected)))
                        {
                            isAllSelected = false;
                            break;
                        }
                    }

                    // cập nhật lại toàn bộ node con
                    foreach (TreeListNode childNode in parentNode.Nodes)
                    {
                        childNode.SetValue(colIsSelected, !isAllSelected);

                        int id = TextUtils.ToInt(childNode.GetValue(colPOKHDetailID));
                        if (!isAllSelected)
                        {
                            if (!listSelected.Contains(id))
                                listSelected.Add(id);
                        }
                        else
                        {
                            listSelected.Remove(id);
                        }
                    }
                }
            }
        }

        private void treeList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(treeList.GetFocusedRowCellValue(treeList.FocusedColumn));
                if (string.IsNullOrWhiteSpace(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        #region Fix lỗi yêu cầu xuất kho - ndnhat update 27/09/2025


        void BillExportRequestNew(bool isTranfer, ToolStripMenuItem menuItem)
        {
            try
            {
                //ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
                treeList.CloseEditor();
                dt.AcceptChanges();
                if (listSelected.Count <= 0)
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm muốn Yêu cầu xuất kho!", "Thông báo");
                    return;
                }
                string warehouseCode = TextUtils.ToString(menuItem.Tag);
                WarehouseModel warehouse = SQLHelper<WarehouseModel>
                    .FindByAttribute("WarehouseCode", warehouseCode)
                    .FirstOrDefault() ?? new WarehouseModel();

                // --- 1. Sắp xếp listSelected theo QuantityRequestExport tăng dần --- //ndnhat update 27/09/2025
                var sortedSelected = new List<int>();
                var productQuantities = new Dictionary<int, decimal>(); // Lưu POKHDetailID và QuantityRequestExport
                foreach (var pokhDetailID in listSelected)
                {
                    DataRow[] dateRows = dt.Select($"POKHDetailID = {pokhDetailID}");
                    DataRow dataRowSelect = dateRows.Length > 0 ? dateRows[0] : null;
                    if (dataRowSelect == null) continue;
                    decimal qtyReq = TextUtils.ToDecimal(dataRowSelect["QuantityRequestExport"]);
                    productQuantities[pokhDetailID] = qtyReq;
                }
                sortedSelected = productQuantities.OrderBy(x => x.Value).Select(x => x.Key).ToList(); //ndnhat update 27/09/2025

                // --- 2. Tính tổng QuantityRequestExport cho mỗi ProductID ---
                var productRequestedTotals = new Dictionary<int, decimal>();
                var productRemainingStock = new Dictionary<int, decimal>(); // Theo dõi tồn kho tạm thời //ndnhat update 27/09/2025

                //ndnhat update 27/09/2025
                /*
                for (int i = 0; i < listSelected.Count; i++)
                {
                    DataRow[] dateRows = dt.Select($"POKHDetailID = {listSelected[i]}");
                    DataRow dataRowSelect = dateRows.Length > 0 ? dateRows[0] : null;
                    if (dataRowSelect == null) continue;
                    int productID = TextUtils.ToInt(dataRowSelect["ProductID"]);
                    decimal qtyReq = TextUtils.ToDecimal(dataRowSelect["QuantityRequestExport"]);
                    if (productID <= 0) continue;
                    if (productRequestedTotals.ContainsKey(productID))
                        productRequestedTotals[productID] += qtyReq;
                    else
                        productRequestedTotals[productID] = qtyReq;
                }
                */
                //ndnhat update 27/09/2025
                foreach (var pokhDetailID in sortedSelected)
                {
                    DataRow[] dateRows = dt.Select($"POKHDetailID = {pokhDetailID}");
                    DataRow dataRowSelect = dateRows.Length > 0 ? dateRows[0] : null;
                    if (dataRowSelect == null) continue;
                    int productID = TextUtils.ToInt(dataRowSelect["ProductID"]);
                    decimal qtyReq = TextUtils.ToDecimal(dataRowSelect["QuantityRequestExport"]);

                    string unitName = TextUtils.ToString(dataRowSelect["Unit"]);
                    if (unitNames.Contains(unitName.Trim().ToLower())) continue;

                    if (productID <= 0) continue;
                    if (productRequestedTotals.ContainsKey(productID))
                        productRequestedTotals[productID] += qtyReq;
                    else
                        productRequestedTotals[productID] = qtyReq;
                }

                // --- 3. Validate danh sách sản phẩm theo thứ tự --- //ndnhat update 27/09/2025
                List<string> productNewCodes = new List<string>();
                List<int> validSelected = new List<int>(); // Danh sách POKHDetailID hợp lệ //ndnhat update 27/09/2025

                // Code cũ bị sửa đổi //ndnhat modified 27/09/2025
                /*
                for (int i = 0; i < listSelected.Count; i++)
                {
                    DataRow[] dateRows = dt.Select($"POKHDetailID = {listSelected[i]}");
                    DataRow dataRowSelect = dateRows.Length > 0 ? dateRows[0] : null;
                    if (dataRowSelect == null) continue;
                    int productID = TextUtils.ToInt(dataRowSelect["ProductID"]);
                    decimal totalRequestedForProduct = productRequestedTotals.ContainsKey(productID)
                        ? productRequestedTotals[productID]
                        : TextUtils.ToDecimal(dataRowSelect["QuantityRequestExport"]);
                    if (!ValidateKeep(dataRowSelect, warehouse.ID, totalRequestedForProduct, out string productNewCode))
                    {
                        if (!string.IsNullOrWhiteSpace(productNewCode) && !productNewCodes.Contains(productNewCode))
                        {
                            productNewCodes.Add(productNewCode);
                        }
                    }
                }
                */
                // Code mới thay thế //ndnhat update 27/09/2025


                Dictionary<int, decimal> totalQuantityRequest = new Dictionary<int, decimal>();
                Dictionary<int, decimal> totalQuantityStock = new Dictionary<int, decimal>();
                foreach (var pokhDetailID in sortedSelected)
                {
                    DataRow[] dateRows = dt.Select($"POKHDetailID = {pokhDetailID}");
                    DataRow dataRowSelect = dateRows.Length > 0 ? dateRows[0] : null;
                    if (dataRowSelect == null) continue;
                    int productID = TextUtils.ToInt(dataRowSelect["ProductID"]);
                    decimal qtyReq = TextUtils.ToDecimal(dataRowSelect["QuantityRequestExport"]);
                    decimal totalRequestedForProduct = productRequestedTotals.ContainsKey(productID)
                        ? productRequestedTotals[productID]
                        : qtyReq;


                    if (totalQuantityRequest.ContainsKey(productID)) totalQuantityRequest[productID] += qtyReq;
                    else totalQuantityRequest.Add(productID, qtyReq);

                    // Khởi tạo tồn kho tạm thời nếu chưa có //ndnhat update 27/09/2025
                    //if (!productRemainingStock.ContainsKey(productID))

                    DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetInventoryProjectImportExport",
                        new string[] { "@WarehouseID", "@ProductID", "@ProjectID", "@POKHDetailID", "@BillExportDetailID" },
                        new object[] { warehouse.ID, productID, 0, pokhDetailID, 0 });
                    var inventoryProjects = dataSet.Tables[0];
                    var dtImport = dataSet.Tables[1];
                    var dtExport = dataSet.Tables[2];
                    var dtStock = dataSet.Tables[3];
                    decimal totalQuantityKeep = inventoryProjects.Rows.Count <= 0 ? 0 : TextUtils.ToDecimal(inventoryProjects.Rows[0]["TotalQuantity"]);
                    totalQuantityKeep = Math.Max(totalQuantityKeep, 0);

                    decimal totalQuantityLast = dtStock.Rows.Count <= 0 ? 0 : TextUtils.ToDecimal(dtStock.Rows[0]["TotalQuantityLast"]);
                    totalQuantityLast = Math.Max(totalQuantityLast, 0);

                    decimal totalImport = dtImport.Rows.Count <= 0 ? 0 : TextUtils.ToDecimal(dtImport.Rows[0]["TotalImport"]);
                    totalImport = Math.Max(totalImport, 0);

                    decimal totalExport = dtExport.Rows.Count <= 0 ? 0 : TextUtils.ToDecimal(dtExport.Rows[0]["TotalExport"]);
                    totalImport = Math.Max(totalImport, 0);

                    decimal quantityRemain = totalImport - totalExport;
                    decimal quantityStock = Math.Max(totalQuantityKeep + quantityRemain + totalQuantityLast, 0);
                    //productRemainingStock[productID] = Math.Max(totalQuantityKeep + quantityRemain + totalQuantityLast, 0);

                    if (totalQuantityStock.ContainsKey(productID)) totalQuantityStock[productID] += quantityStock;
                    else totalQuantityStock.Add(productID, quantityStock);


                    var diffKeys = totalQuantityRequest.Keys.Intersect(totalQuantityStock.Keys)
                                                        .Where(k => totalQuantityRequest[k] > totalQuantityStock[k]);

                    if (diffKeys.Count() > 0)
                    {
                        // Kiểm tra tồn kho //ndnhat update 27/09/2025
                        if (ValidateKeepNew(dataRowSelect, warehouse.ID, qtyReq, quantityStock, out string productNewCode))
                        {
                            validSelected.Add(pokhDetailID);
                            //productRemainingStock[productID] -= qtyReq; // Cập nhật tồn kho tạm thời
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(productNewCode) && !productNewCodes.Contains(productNewCode))
                            {
                                productNewCodes.Add(productNewCode);
                            }
                        }
                    }
                    else validSelected.Add(pokhDetailID);
                    

                }

                // --- 4. Thông báo xác nhận ---
                DialogResult dialog = MessageBox.Show(
                    $"Bạn có chắc muốn yêu cầu xuất kho danh sách sản phẩm đã chọn từ [{menuItem.Text}] không?",
                    "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.No) return;
                if (productNewCodes.Count > 0)
                {
                    MessageBox.Show(
                        $"Các sản phẩm có mã nội bộ:{string.Join(";", productNewCodes)}sẽ không được xuất kho vì không đủ số lượng!",
                        "Thông báo");
                }
                // Kiểm tra nếu không có sản phẩm hợp lệ //ndnhat update 27/09/2025
                if (validSelected.Count == 0)
                {
                    MessageBox.Show("Không có sản phẩm nào đủ tồn kho để xuất!", "Thông báo");
                    return;
                }

                // --- 5. Tạo chi tiết xuất kho ---
                DataTable dtDetail = TextUtils.LoadDataFromSP("spGetBillExportDetail", "A",
                    new string[] { "@BillID" }, new object[] { 0 });
                int stt = 0;

                //ndnhat modified 27/09/2025
                /*
                for (int i = 0; i < listSelected.Count; i++)
                {
                    DataRow[] dateRows = dt.Select($"POKHDetailID = {listSelected[i]}");
                    DataRow dataRowSelect = dateRows.Length > 0 ? dateRows[0] : null;
                    if (dataRowSelect == null) continue;
                    int productID = TextUtils.ToInt(dataRowSelect["ProductID"]);
                    string productNewCode = TextUtils.ToString(dataRowSelect["ProductNewCode"]);
                    decimal quantityRemain = TextUtils.ToDecimal(dataRowSelect["QuantityRemain"]);
                    if (productID <= 0 || string.IsNullOrWhiteSpace(productNewCode) || quantityRemain <= 0) continue;
                    decimal totalRequestedForProduct = productRequestedTotals.ContainsKey(productID)
                        ? productRequestedTotals[productID]
                        : TextUtils.ToDecimal(dataRowSelect["QuantityRequestExport"]);
                    if (!ValidateKeep(dataRowSelect, warehouse.ID, totalRequestedForProduct, out string productNewCodeShow))
                        continue;
                    stt++;
                    // ... (thêm vào dtDetail)
                */
                //ndnhat update 27/09/2025
                foreach (var pokhDetailID in validSelected)
                {
                    DataRow[] dateRows = dt.Select($"POKHDetailID = {pokhDetailID}");
                    DataRow dataRowSelect = dateRows.Length > 0 ? dateRows[0] : null;
                    if (dataRowSelect == null) continue;
                    int productID = TextUtils.ToInt(dataRowSelect["ProductID"]);
                    string productNewCode = TextUtils.ToString(dataRowSelect["ProductNewCode"]);
                    decimal quantityRemain = TextUtils.ToDecimal(dataRowSelect["QuantityRemain"]);
                    if (productID <= 0 || string.IsNullOrWhiteSpace(productNewCode) || quantityRemain <= 0) continue;

                    stt++;
                    DataRow dataRow = dtDetail.NewRow();
                    dataRow["ProductID"] = productID;
                    dataRow["Qty"] = TextUtils.ToDecimal(dataRowSelect["QuantityRequestExport"]);
                    dataRow["ProjectID"] = TextUtils.ToInt(dataRowSelect["ProjectID"]);
                    dataRow["Note"] = TextUtils.ToString(dataRowSelect["PONumber"]);
                    dataRow["ProductCode"] = TextUtils.ToString(dataRowSelect["ProductCode"]);
                    dataRow["ProductNewCode"] = productNewCode;
                    dataRow["ProductName"] = TextUtils.ToString(dataRowSelect["ProductName"]);
                    dataRow["Unit"] = TextUtils.ToString(dataRowSelect["Unit"]);
                    dataRow["ProductGroupName"] = TextUtils.ToString(dataRowSelect["ProductGroupName"]);
                    dataRow["ItemType"] = TextUtils.ToString(dataRowSelect["ItemType"]);
                    dataRow["ProjectNameText"] = TextUtils.ToString(dataRowSelect["ProjectName"]);
                    dataRow["ProjectCodeText"] = TextUtils.ToString(dataRowSelect["ProjectCode"]);
                    dataRow["ProjectCodeExport"] = TextUtils.ToString(dataRowSelect["ProjectCodeExport"]);
                    dataRow["productGroupID"] = TextUtils.ToInt(dataRowSelect["productGroupID"]);
                    dataRow["POKHID"] = TextUtils.ToInt(dataRowSelect["POKHID"]);
                    dataRow["POKHDetailID"] = TextUtils.ToInt(dataRowSelect["POKHDetailID"]);
                    dataRow["STT"] = stt;
                    dataRow["UserReceiver"] = TextUtils.ToString(dataRowSelect["UserReceiver"]);
                    dataRow["QuantityRemain"] = TextUtils.ToInt(dataRowSelect["QuantityRemain"]);
                    dataRow["CustomerID"] = TextUtils.ToInt(dataRowSelect["CustomerID"]);
                    dataRow["KhoTypeID"] = TextUtils.ToInt(dataRowSelect["KhoTypeID"]);
                    dataRow["UserID"] = TextUtils.ToInt(dataRowSelect["UserID"]);
                    dataRow["IsMerge"] = TextUtils.ToBoolean(dataRowSelect["IsMerge"]);
                    dataRow["ProductFullName"] = TextUtils.ToString(dataRowSelect["GuestCode"]);
                    dataRow["ParentID"] = TextUtils.ToString(dataRowSelect["ParentID"]);
                    dataRow["TotalInventory"] = TextUtils.ToString(dataRowSelect["TotalInventory"]);
                    dataRow["UnitPricePurchase"] = TextUtils.ToString(dataRowSelect["UnitPricePurchase"]);
                    dataRow["BillCode"] = TextUtils.ToString(dataRowSelect["BillCode"]);
                    dataRow["UnitPricePOKH"] = TextUtils.ToString(dataRowSelect["UnitPricePOKH"]);
                    dataRow["ChildID"] = TextUtils.ToString(dataRowSelect["POKHDetailID"]);
                    dataRow["POKHDetailIDActual"] = TextUtils.ToString(dataRowSelect["POKHDetailID"]);
                    dataRow["PONumber"] = TextUtils.ToString(dataRowSelect["PONumber"]);
                    dataRow["POCode"] = TextUtils.ToString(dataRowSelect["POCode"]);
                    dtDetail.Rows.Add(dataRow);
                }

                // --- 6. Gom theo CustomerID + KhoTypeID ---
                DataView view = new DataView(dtDetail);
                DataTable distinctValues = view.ToTable(true, new string[] { "CustomerID", "KhoTypeID" });
                var listBillExports = distinctValues.AsEnumerable()
                    .Where(row => !row.IsNull("CustomerID") && !row.IsNull("KhoTypeID"))
                    .ToList();
                if (listBillExports.Count > 1)
                {
                    MessageBox.Show($"Bạn chọn sản phẩm từ {listBillExports.Count} Khách hàng hoặc Loại kho.\nNên phần mềm sẽ tự động tạo {listBillExports.Count} phiếu xuất.",
                        "Thông báo");
                }
                for (int i = 0; i < listBillExports.Count; i++)
                {
                    int customerID = TextUtils.ToInt(listBillExports[i]["CustomerID"]);
                    int khoTypeID = TextUtils.ToInt(listBillExports[i]["KhoTypeID"]);
                    DataRow[] dataExport = dtDetail.Select($"CustomerID = {customerID} AND KhoTypeID = {khoTypeID}");
                    if (dataExport.Length <= 0) continue;
                    BillExportModel billExport = new BillExportModel();
                    billExport.CustomerID = customerID;
                    billExport.UserID = Global.UserID;
                    billExport.KhoTypeID = khoTypeID;
                    billExport.ProductType = 1;
                    billExport.IsMerge = TextUtils.ToBoolean(dataExport[0]["IsMerge"]);
                    billExport.Status = 6;
                    billExport.RequestDate = DateTime.Now;
                    frmBillExportDetailNew frm = new frmBillExportDetailNew(isTranfer);
                    frm.billExport = billExport;
                    frm.dtDetail = dataExport.CopyToDataTable();
                    frm.WarehouseCode = warehouse.WarehouseCode;
                    frm.isPOKH = true;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (i == listBillExports.Count - 1)
                        {
                            LoadData();
                            listSelected.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }


        bool ValidateKeepNew(DataRow dataRowSelect, int warehouseID, decimal qtyRequested, decimal availableStock, out string productNewCode)
        {
            productNewCode = "";
            if (dataRowSelect == null) return false;
            string unitName = TextUtils.ToString(dataRowSelect["Unit"]);
            if (unitNames.Contains(unitName.Trim().ToLower())) return true;


            string poNumber = TextUtils.ToString(dataRowSelect["PONumber"]);
            decimal quantity = TextUtils.ToDecimal(dataRowSelect["Qty"]);

            // Kiểm tra số lượng yêu cầu so với tồn kho tạm thời //ndnhat update 27/09/2025
            if (qtyRequested > availableStock)
            {
                productNewCode = $"\n[{TextUtils.ToString(dataRowSelect["ProductNewCode"])} ở PO: {poNumber} - SL: {qtyRequested}]\n";
                return false;
            }
            return true;
        }

        #endregion

        private void btnDropdownTranferWarehouse_Click(object sender, EventArgs e)
        {

        }
    }
}
