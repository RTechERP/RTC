using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmInventoryProject : _Forms
    {
        public List<InventoryProjectModel> inventoryProjects = new List<InventoryProjectModel>();
        public int productSaleID = 0;


        DataTable dt = new DataTable();
        public frmInventoryProject()
        {
            InitializeComponent();
        }

        private void frmInventoryProject_Load(object sender, EventArgs e)
        {
            LoadProject();
            LoadEmployee();

            LoadData();
        }


        void LoadData()
        {
            grdData.DataSource = null;
            int projectID = TextUtils.ToInt(cboProject.EditValue);
            int employeeID = TextUtils.ToInt(cboEmployee.EditValue);
            string keyword = txtKeyword.Text.Trim();
            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
            {
                this.dt = TextUtils.LoadDataFromSP("spGetInventoryProject", "A",
                                                        new string[] { "@ProjectID", "@EmployeeID", "@ProductSaleID", "@Keyword" },
                                                        new object[] { projectID, employeeID, productSaleID, keyword });

                grdData.DataSource = dt;
            }
        }



        void LoadPOKH()
        {
            int productSaleID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductSaleID));
            var list = TextUtils.LoadDataFromSP("spGetPOKHDetailForInventoryProject", "spGetPOKHDetailForInventoryProject",
                                                    new string[] { "@ProductID" }, new object[] { productSaleID });
            cboPOKHDetailFrom.Properties.DisplayMember = "DisplayText";
            cboPOKHDetailFrom.Properties.ValueMember = "ID";
            cboPOKHDetailFrom.Properties.DataSource = list;

            cboPOKHDetailTo.Properties.DisplayMember = "DisplayText";
            cboPOKHDetailTo.Properties.ValueMember = "ID";
            cboPOKHDetailTo.Properties.DataSource = list;
        }


        void LoadDataRejectKeep()
        {
            bool isAdmin = (Global.IsAdmin && Global.EmployeeID <= 0);
            //int[] rowSelecteds = grvData.GetSelectedRows();
            //if (rowSelecteds.Length <= 0)
            //{
            //    MessageBox.Show("Vui lòng chọn sản phẩm muốn nhả giữ!", "Thông báo");
            //    return;
            //}

            //var dataReject = this.dt.Clone();
            //foreach (int row in rowSelecteds)
            //{
            //    DataRow dataRow = grvData.GetDataRow(row);
            //    if (dataRow == null) return;

            //    dataReject.ImportRow(dataRow);
            //}

            //grdDataRejectKeep.DataSource = dataReject;

            int row = grvData.FocusedRowHandle;
            var employeeIDs = TextUtils.ToString(grvData.GetRowCellValue(row, colEmployeeIDs)).Split(';').ToList();

            if (!employeeIDs.Contains(Global.EmployeeID.ToString()) && !isAdmin)
            {
                MessageBox.Show($"Bạn không có quyền nhả giữ vì bạn không phải người yêu cầu giữ hoặc người giữ!", "Thông báo");
                return;
            }
            LoadPOKH();

            txtProductCode.Text = TextUtils.ToString(grvData.GetRowCellValue(row, colProductCode));
            txtProductNewCode.Text = TextUtils.ToString(grvData.GetRowCellValue(row, colProductNewCode));
            txtUnit.Text = TextUtils.ToString(grvData.GetRowCellValue(row, colUnit));
            txtProductName.Text = TextUtils.ToString(grvData.GetRowCellValue(row, colProductName));
            txtQuantityOrigin.Value = TextUtils.ToDecimal(grvData.GetRowCellValue(row, colQuantity));
            cboPOKHDetailFrom.Text = TextUtils.ToString(grvData.GetRowCellValue(row, colPOKHDetailID));


            flyoutPanel1.Options.AnchorType = DevExpress.Utils.Win.PopupToolWindowAnchor.Manual;

            int x = (Screen.PrimaryScreen.Bounds.Width / 2) - (flyoutPanel1.Width / 2);
            int y = (Screen.PrimaryScreen.Bounds.Height / 2) - (flyoutPanel1.Height / 2) - 300;
            flyoutPanel1.Options.Location = new System.Drawing.Point(x, y);
            flyoutPanel1.ShowPopup();
        }


        bool RejectKeep()
        {
            try
            {
                //2 TH nhả giữ:
                //1. Nếu người dùng nhả giữ mà ko chọn PO (1 phần hoặc tất cả) --> giống như xóa
                //2. Nếu người dùng nhả giữ từ PO này sang PO khác --> update lại bản ghi cũ và thêm 1 bản ghi giữ cho POKH mới
                int row = grvData.FocusedRowHandle;

                int id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
                if (id <= 0) return false;

                int pokhDetailIDTo = TextUtils.ToInt(cboPOKHDetailTo.EditValue);
                decimal quantity = txtQuantity.Value;
                decimal quantityOrigin = txtQuantityOrigin.Value;

                //Check validate
                if (quantity <= 0) return false;
                else if (quantity > quantityOrigin)
                {
                    MessageBox.Show($"Bạn không thể nhả giữ lớn hơn số lượng giữ!\n Số lượng giữ: {quantityOrigin}\nSố lượng nhả: {quantity}", "Thông báo");
                    return false;
                }

                if (TextUtils.ToInt(cboPOKHDetailFrom.EditValue) <= 0)
                {
                    MessageBox.Show($"Sẩn phẩm đang giữ cho dự án. Bạn không thể nhả giữ sang hàng thương mại!", "Thông báo");
                    return false;
                }

                if (pokhDetailIDTo <= 0)
                {
                    MessageBox.Show($"Vui lòng chọn Đến POKH!", "Thông báo");
                    return false;
                }

                //var myDict = new Dictionary<string, object>()
                //{
                //    {InventoryProjectModel_Enum.UpdatedDate.ToString(),DateTime.Now },
                //    {InventoryProjectModel_Enum.UpdatedBy.ToString(),Global.AppCodeName },
                //};

                InventoryProjectModel inventory = SQLHelper<InventoryProjectModel>.FindByID(id);
                //Update thông tin giữ
                inventory.Quantity = quantityOrigin - quantity;
                //inventory.QuantityOrigin = quantityOrigin;
                SQLHelper<InventoryProjectModel>.Update(inventory);

                //Insert thêm bản ghi giữ
                if (pokhDetailIDTo > 0)
                {
                    InventoryProjectModel inventoryNew = inventory;
                    inventoryNew.ParentID = inventory.ID;
                    inventoryNew.Quantity = inventoryNew.QuantityOrigin = quantity;
                    inventoryNew.POKHDetailID = TextUtils.ToInt(cboPOKHDetailTo.EditValue);

                    SQLHelper<InventoryProjectModel>.Insert(inventoryNew);
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
                return false;
            }
        }

        void LoadProject()
        {
            List<ProjectModel> list = SQLHelper<ProjectModel>.FindAll().OrderByDescending(x => x.ID).ToList();

            cboProject.Properties.ValueMember = "ID";
            cboProject.Properties.DisplayMember = "ProjectCode";
            cboProject.Properties.DataSource = list;
        }

        void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;

            //cboEmployee.EditValue = Global.EmployeeID;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var productSaleLinks = SQLHelper<InventoryProjectProductSaleLinkModel>.FindByAttribute(InventoryProjectProductSaleLinkModel_Enum.IsDeleted.ToString(), 0)
                                                                                      .Select(x => x.ProductSaleID).ToList();
                int[] selectedRows = grvData.GetSelectedRows();
                if (selectedRows.Length <= 0)
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm muốn xóa!", "Thông báo");
                    return;
                }

                List<string> productNewCodes = new List<string>();
                foreach (int row in selectedRows)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
                    if (id <= 0) continue;
                    int productSaleID = TextUtils.ToInt(grvData.GetRowCellValue(row, colProductSaleID));
                    string productNewCode = TextUtils.ToString(grvData.GetRowCellValue(row, colProductNewCode));

                    if (!productSaleLinks.Contains(productSaleID) &&
                        !productNewCodes.Contains(productNewCode) &&
                        !(Global.IsAdmin && Global.EmployeeID <= 0)) productNewCodes.Add(productNewCode);
                }

                DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xóa danh sách sản phẩm đã chọn khỏi kho giữ không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    List<int> ids = new List<int>();
                    foreach (int row in selectedRows)
                    {
                        int id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
                        if (id <= 0) continue;

                        int productSaleID = TextUtils.ToInt(grvData.GetRowCellValue(row, colProductSaleID));
                        if (!productSaleLinks.Contains(productSaleID) &&
                            !(Global.IsAdmin && Global.EmployeeID <= 0)) continue;

                        ids.Add(id);
                    }
                    //string productCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductCode));

                    if (ids.Count <= 0) return;

                    if (productNewCodes.Count > 0)
                    {
                        MessageBox.Show($"Những sản phẩm [{string.Join(",", productNewCodes)}] không có trong danh sách được hủy giữ sẽ tự động được bỏ qua.", "Thông báo");
                    }

                    var myDict = new Dictionary<string, object>()
                    {
                        {InventoryProjectModel_Enum.IsDeleted.ToString(),true },
                        {InventoryProjectModel_Enum.UpdatedBy.ToString(),Global.AppUserName },
                        {InventoryProjectModel_Enum.UpdatedDate.ToString(),DateTime.Now },
                    };

                    var exp = new Expression(InventoryProjectModel_Enum.ID, string.Join(",", ids), "IN");

                    SQLHelper<InventoryProjectModel>.UpdateFields(myDict, exp);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }

        private void btnExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            f.FileName = $"DanhSachHangGiu_{DateTime.Now.ToString("ddMMyy")}.xlsx";
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = f.FileName;

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();
                try
                {
                    using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
                    {
                        CompositeLink compositeLink = new CompositeLink(printingSystem);
                        //foreach (XtraTabPage item in xtraTabControl1.TabPages)
                        //{
                        //    if (item.Controls.Count <= 0) continue;
                        //    GridControl gridControl = (GridControl)item.Controls[0];
                        //    LoadData(gridControl);


                        //}

                        PrintableComponentLink printableComponentLink = new PrintableComponentLink(printingSystem);
                        printableComponentLink.Component = grdData;

                        compositeLink.Links.Add(printableComponentLink);

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

            if (gridView.FocusedRowHandle == e.RowHandle)
            {
                e.Appearance.BackColor = Color.LightYellow;
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            int productSaleID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductSaleID));
            string productName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductName));
            string warehouseCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colWarehouseCode));
            string totalQuantityFirst = TextUtils.ToString(grvData.GetFocusedRowCellValue(colTotalQuantityFirst));
            string totalQuantityLast = TextUtils.ToString(grvData.GetFocusedRowCellValue(colTotalQuantityLast));
            if (productSaleID <= 0) return;

            frmChiTietSanPhamSale frm = new frmChiTietSanPhamSale();
            frm.productSaleID = productSaleID;
            frm.ProductName = productName;
            frm.NumberDauKy = totalQuantityFirst;
            frm.NumberCuoiKy = totalQuantityLast;
            //frm.txtTotalQuantityKeep.Text = TextUtils.ToString(grvData.GetFocusedRowCellValue(colTotalQuantityRemain));

            frm.WarehouseCode = warehouseCode;
            frm.Show();

        }

        private void btnChosen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] selectedRows = grvData.GetSelectedRows();
            if (selectedRows.Length <= 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm!", "Thông báo");
                return;
            }

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn chọn danh sách sản phẩm để xuất kho không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                foreach (int row in selectedRows)
                {

                    int id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
                    if (id <= 0) return;

                    InventoryProjectModel inventoryProject = new InventoryProjectModel()
                    {
                        ID = id,
                        CreatedBy = TextUtils.ToString(grvData.GetRowCellValue(row, colProductCode))
                    };
                    inventoryProjects.Add(inventoryProject);
                }

                this.DialogResult = DialogResult.OK;

            }
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            //if (!Global.IsAdmin) return;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));

            if (id <= 0) return;

            WarehouseModel warehouse = SQLHelper<WarehouseModel>.FindByID(1);
            frmInventoryProjectDetail frm = new frmInventoryProjectDetail(warehouse);
            frm.inventoryProject = SQLHelper<InventoryProjectModel>.FindByID(id);
            frm.Show();
        }

        private void btnRejectKeep_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadDataRejectKeep();
        }


        bool CheckValidateRejectKeep()
        {
            grvDataRejectKeep.CloseEditor();
            for (int i = 0; i < grvDataRejectKeep.RowCount; i++)
            {
                int id = TextUtils.ToInt(grvDataRejectKeep.GetRowCellValue(i, colID.FieldName));
                if (id <= 0) continue;


                decimal quantityRemain = TextUtils.ToInt(grvDataRejectKeep.GetRowCellValue(i, colTotalQuantityRemain.FieldName));
                decimal quantityReject = TextUtils.ToInt(grvDataRejectKeep.GetRowCellValue(i, colQuantityRejectKeep.FieldName));
                string productCode = TextUtils.ToString(grvDataRejectKeep.GetRowCellValue(i, colProductCode.FieldName));

                if (quantityReject > quantityRemain)
                {
                    MessageBox.Show($"SL nhả giữ sản phẩm [{productCode}] không được lớn hơn SL còn lại!", "Thông báo");
                    return false;
                }
            }

            return true;
        }
        private void flyoutPanel1_ButtonClick(object sender, FlyoutPanelButtonClickEventArgs e)
        {
            string tag = e.Button.Tag.ToString();
            switch (tag)
            {
                case "btnOK":

                    //grvDataRejectKeep.CloseEditor();
                    //if (!CheckValidateRejectKeep()) break;

                    //for (int i = 0; i < grvDataRejectKeep.RowCount; i++)
                    //{
                    //    int id = TextUtils.ToInt(grvDataRejectKeep.GetRowCellValue(i, colID.FieldName));
                    //    if (id <= 0) continue;


                    //    decimal quantity = TextUtils.ToInt(grvDataRejectKeep.GetRowCellValue(i, colQuantity.FieldName));
                    //    decimal quantityReject = TextUtils.ToInt(grvDataRejectKeep.GetRowCellValue(i, colQuantityRejectKeep.FieldName));

                    //    var myDict = new Dictionary<string, object>()
                    //    {
                    //        {InventoryProjectModel_Enum.Quantity.ToString(), quantity - quantityReject },
                    //        {InventoryProjectModel_Enum.UpdatedDate.ToString(), DateTime.Now },
                    //        {InventoryProjectModel_Enum.UpdatedBy.ToString(), Global.AppUserName },
                    //    };

                    //    SQLHelper<InventoryProjectModel>.UpdateFieldsByID(myDict, id);
                    //}

                    if (RejectKeep())
                    {
                        (sender as FlyoutPanel).HidePopup();
                        LoadData();

                        txtProductCode.Clear();
                        txtProductNewCode.Clear();
                        txtUnit.Clear();
                        txtProductName.Clear();
                        txtQuantityOrigin.Value = 0;
                        txtQuantity.Value = 0;
                        cboPOKHDetailFrom.EditValue = 0;
                        cboPOKHDetailTo.EditValue = 0;
                    }
                    break;
                case "btnCancel":
                    (sender as FlyoutPanel).HidePopup();
                    break;
            }
        }

        private void grvDataRejectKeep_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (grvDataRejectKeep.FocusedColumn != colQuantityRejectKeep) return;

            decimal quantityRemain = TextUtils.ToDecimal(grvDataRejectKeep.GetFocusedRowCellValue(colTotalQuantityRemain.FieldName));
            decimal quantityReject = TextUtils.ToDecimal(e.Value);

            if (quantityReject > quantityRemain)
            {
                e.Valid = false;
                e.ErrorText = "SL nhả giữ không được lớn hơn SL còn lại!";
            }
        }
    }
}
