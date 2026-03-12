using BMS.Business;
using BMS.Model;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
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
using BMS.Utils;

namespace BMS
{
    public partial class frmInventoryBorrowNCC : _Forms
    {
        public int warehouseID;
        int totalReturnNCC = 0;
        List<int> selectedRows = new List<int>(); // lee min khooi update 11/11/2024

        public frmInventoryBorrowNCC()
        {
            InitializeComponent();
        }

        private void frmInventoryBorrowNCC_Load(object sender, EventArgs e)
        {
            DateTime datenow = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            dtpFromDate.Value = datenow.AddMonths(-1);
            txtPageNumber.Text = "1";
            LoadData();
            LoadNCC();
        }

        private void LoadNCC()
        {
            List<SupplierSaleModel> listSupplier = SQLHelper<SupplierSaleModel>.FindAll();
            cboNCC.Properties.DataSource = listSupplier;
            cboNCC.Properties.DisplayMember = "NameNCC";
            cboNCC.Properties.ValueMember = "ID";
        }

        private void LoadData()
        {
            int supplierID = TextUtils.ToInt(cboNCC.EditValue);
            DateTime dateTimeS = TextUtils.ToDate(dtpFromDate.Value.ToString());
            DateTime dateTimeE = TextUtils.ToDate(dtpEndDate.Value.ToString());

            DataTable dt = TextUtils.LoadDataFromSP("spGetInventoryBorrowSupllier", "A",
                new string[] { "@FilterText", "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@SupplierSaleID", "@WarehouseID" },
                new object[] {txtFilterText.Text,TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value),
                     dateTimeS, dateTimeE, supplierID, warehouseID });

            totalReturnNCC = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0)
                {
                    totalReturnNCC += TextUtils.ToInt(dt.Rows[i]["TotalQuantityReturnNCC"]);
                }
                if (i > 0)
                {
                    if (TextUtils.ToString(dt.Rows[i]["ImportDetailID"]) != TextUtils.ToString(dt.Rows[i - 1]["ImportDetailID"]))
                    {
                        totalReturnNCC += TextUtils.ToInt(dt.Rows[i]["TotalQuantityReturnNCC"]);
                    }
                }
            }
            grdData.DataSource = dt;
            if (dt.Rows.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(dt.Rows[0]["TotalPage"]);
            // bool isExist = false;
            selectedRows = new List<int>();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            LoadData();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            LoadData();
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            LoadData();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            LoadData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            LoadData();
        }

        private void grvMaster_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            var item = e.Item as DevExpress.XtraGrid.GridColumnSummaryItem;
            if (item == null || item.FieldName != "TotalQuantityReturnNCC") return;

            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
            {
                e.TotalValue = totalReturnNCC;
            }

        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            sfd.FileName = $"DanhSachMuonNCC{DateTime.Now.ToString("ddMMyy")}.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;

                grvData.OptionsPrint.UsePrintStyles = true;

                try
                {
                    grvData.ExportToXls(sfd.FileName, optionsEx);
                    Process.Start(sfd.FileName);
                }
                catch (Exception)
                {
                }
            }
        }

        private void grvMaster_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            GridView view = sender as GridView;
            try
            {
                if ((e.Column.FieldName == "TotalQuantityReturnNCC"))
                {
                    int importDetailID1 = TextUtils.ToInt(view.GetRowCellValue(e.RowHandle1, "ImportDetailID"));
                    int importDetailID2 = TextUtils.ToInt(view.GetRowCellValue(e.RowHandle2, "ImportDetailID"));
                    e.Merge = (importDetailID1 == importDetailID2);
                    e.Handled = true;
                }
                else
                {
                    e.Merge = false;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void chiTiếtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //editGrv = 1;
            string warehouseCode = SQLHelper<WarehouseModel>.FindByID(warehouseID).WarehouseCode;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductSaleID));
            if (ID == 0) return;
            string ProductName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductName));
            string NumberDauKy = TextUtils.ToString(grvData.GetFocusedRowCellValue(colNumberInStoreDauKy));
            string NumberCuoiKy = TextUtils.ToString(grvData.GetFocusedRowCellValue(colNumberInStoreCuoiKy));

            //string Import = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colImport));
            //string Export = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colExport));


            frmChiTietSanPhamSale frm = new frmChiTietSanPhamSale();

            //InventoryModel model = (InventoryModel)InventoryBO.Instance.FindByPK(ID);

            //frm.productSaleID = model.ProductSaleID;
            frm.productSaleID = ID;
            //frm.ProductName = ProductName;
            frm.NumberDauKy = NumberDauKy;
            frm.NumberCuoiKy = NumberCuoiKy;
            //frm.Import = Import;
            //frm.Export = Export;


            frm.WarehouseCode = warehouseCode;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //loadGrdData();
            }
        }

        private void chiTiếtPhiếuNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string warehouseCode = SQLHelper<WarehouseModel>.FindByID(warehouseID).WarehouseCode;
            int BillID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colBillImportID));
            if (BillID == 0) return;
            BillImportModel model = (BillImportModel)BillImportBO.Instance.FindByPK(BillID);
            frmBillImportDetail frm = new frmBillImportDetail();
            frm.IDDetail = BillID;
            frm.billImport = model;
            frm.WarehouseCode = warehouseCode;
            if (frm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void chiTiếtPhiếuXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string warehouseCode = SQLHelper<WarehouseModel>.FindByID(warehouseID).WarehouseCode;
            int exportId = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colBillExportID));
            string productCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductCode));
            if (exportId == 0)
            {
                //MessageBox.Show(String.Format("Sản phẩm [{0}]? mượn từ nhà cung cấp chưa có phiếu xuất. Xin vui lòng kiểm tra lại.", productCode), TextUtils.Caption, MessageBoxButtons.OK,
                //         MessageBoxIcon.Information);
                return;
            }
            BillExportModel model = (BillExportModel)BillExportBO.Instance.FindByPK(exportId);
            frmBillExportDetailNew frm = new frmBillExportDetailNew(false);
            frm.IDDetail = exportId;
            frm.billExport = model;
            frm.WarehouseCode = warehouseCode;
            if (frm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        //Check focused column is colTotalQuantityReturnNCC or not
        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }

        private void grvMaster_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {

        }


        private void btnEditRowCheck_Click(object sender, EventArgs e)
        {
            grvData.CloseEditor();
            grvData.FocusedRowHandle = -1;
            List<int> listSupplierSales = new List<int>();
            //List<string> lsWarehouseType = new List<string>();
            List<int> productGroupIDs = new List<int>();


            int billImportId = 0;
            int stt = 0;

            //============================= lee min khooi update 11/11/2024 ==============================
            DataTable data = (DataTable)grdData.DataSource;
            DataTable dt = data.Clone();
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("ProductFullName", typeof(string));
            dt.Columns.Add("Specifications", typeof(string));
            dt.Columns.Add("GroupExport", typeof(string));
            dt.Columns.Add("UserReceiver", typeof(string));
            dt.Columns.Add("SerialNumber", typeof(string));


            //if (grvData.RowCount > 0)
            //{
            //    for (int i = 0; i < grvData.RowCount; i++)
            //    {
            //        if (grvData.IsRowSelected(i))
            //        {
            //            DataRow row = grvData.GetDataRow(i);
            //            int suplierId = TextUtils.ToInt(row[$"{colProductSaleID.FieldName}"]);
            //            //string warehouseType = TextUtils.ToString(row[$"{colProductGroupName.FieldName}"]);
            //            int productGroupID = TextUtils.ToInt(row[$"{colProductGroupID.FieldName}"]);
            //            billImportId = TextUtils.ToInt(row[$"{colBillImportID.FieldName}"]);
            //            if (!listSupplierSales.Contains(suplierId)) listSupplierSales.Add(suplierId);
            //            if (!productGroupIDs.Contains(productGroupID)) productGroupIDs.Add(productGroupID);
            //            //if (!lsWarehouseType.Contains(warehouseType)) lsWarehouseType.Add(warehouseType);


            //            if (listSupplierSales.Count() > 1)
            //            {
            //                MessageBox.Show("Bạn cần chọn các sản phẩm cùng NCC!", "Thông báo");
            //                return;
            //            }

            //            if (productGroupIDs.Count() > 1)
            //            {
            //                MessageBox.Show("Bạn cần chọn các sản phẩm cùng Kho!", "Thông báo");
            //                return;
            //            }

            //            dt.ImportRow(row);
            //            //dt.Rows[stt]["ID"] = TextUtils.ToInt(row[$"{colBillExportID.FieldName}"]);
            //            dt.Rows[stt]["ID"] = 0;
            //            dt.Rows[stt]["Qty"] = TextUtils.ToInt(row[$"{colTotalQuantityReturnNCC.FieldName}"]);
            //            dt.Rows[stt]["STT"] = stt + 1;
            //            stt++;
            //        }
            //    }
            //    if (stt == 0)
            //    {
            //        MessageBox.Show("Vui lòng chọn sản phẩm cần trả!", "Thông báo", MessageBoxButtons.OK);
            //        return;
            //    }
            //}else return;

            List<DataRow> lstData = data.Select().Where(p => selectedRows.Contains(TextUtils.ToInt(p["RowNum"]))).ToList();
            if (lstData.Count <= 0) return;
            foreach (DataRow row in lstData)
            {
                int suplierId = TextUtils.ToInt(row[$"{colSupplierSaleID.FieldName}"]);
                int productGroupID = TextUtils.ToInt(row[$"{colProductGroupID.FieldName}"]);
                billImportId = TextUtils.ToInt(row[$"{colBillImportID.FieldName}"]);
                if (!listSupplierSales.Contains(suplierId)) listSupplierSales.Add(suplierId);
                if (!productGroupIDs.Contains(productGroupID)) productGroupIDs.Add(productGroupID);

                if (listSupplierSales.Count() > 1)
                {
                    MessageBox.Show("Bạn cần chọn các sản phẩm cùng NCC!", "Thông báo");
                    return;
                }

                if (productGroupIDs.Count() > 1)
                {
                    MessageBox.Show("Bạn cần chọn các sản phẩm cùng Kho!", "Thông báo");
                    return;
                }

                dt.ImportRow(row);
                //dt.Rows[stt]["ID"] = TextUtils.ToInt(row[$"{colBillExportID.FieldName}"]);
                dt.Rows[stt]["ID"] = 0;
                dt.Rows[stt]["Qty"] = TextUtils.ToInt(row[$"{colTotalQuantityReturnNCC.FieldName}"]);
                dt.Rows[stt]["STT"] = stt + 1;
                dt.Rows[stt]["Note"] = TextUtils.ToString(row[$"{colImportCode.FieldName}"]);
                stt++;
            }
            selectedRows = new List<int>();
            if (stt == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần trả!", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            frmBillExportDetailNew frm = new frmBillExportDetailNew(false);
            frm.WarehouseCode = SQLHelper<WarehouseModel>.FindByID(warehouseID).WarehouseCode;
            if (billImportId != 0) frm.billImport = SQLHelper<BillImportModel>.FindByID(billImportId);
            if (listSupplierSales.Count() > 0) frm.suplierId = TextUtils.ToInt(listSupplierSales.FirstOrDefault());
            //if (lsWarehouseType.Count() > 0) frm.warehouseTypeId = SQLHelper<ProductGroupModel>.FindByAttribute("ProductGroupName", TextUtils.ToString(lsWarehouseType.First())).FirstOrDefault().ID;
            frm.dtClone = dt;
            frm.isAddExport = true;
            frm.warehouseTypeId = TextUtils.ToInt(productGroupIDs.FirstOrDefault());

            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }

        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(grvData.GetRowCellValue(rowHandle, grvData.FocusedColumn));
                if (string.IsNullOrWhiteSpace(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }


        // lee min khooi update 11/11/2024
        private void chkSelect_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            if (rowHandle < 0) return;
            bool isCheck = TextUtils.ToBoolean(e.NewValue);
            int id = TextUtils.ToInt(grvData.GetRowCellValue(rowHandle, colSTT));

            if (isCheck) selectedRows.Add(id);
            else selectedRows.Remove(id);
        }

        private void ApproveReturned(bool returned)
        {
            //1 true: đã trả, 0 false chưa trả
            string statusText = returned ? "Xác nhận trả" : "Hủy trả";
            DialogResult dialog = MessageBox.Show($"Bạn có chắc chắn muốn {statusText} danh sách đã chọn không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.No) return;


            DataTable data = (DataTable)grdData.DataSource;
            List<DataRow> lstData = data.Select().Where(p => selectedRows.Contains(TextUtils.ToInt(p["RowNum"]))).ToList();
            if (lstData.Count == 0) return;

            string strID = string.Join(",", lstData.Select(p => p["ImportDetailID"]).ToList());

            Expression ex1 = new Expression("ID", strID, "IN");
            Dictionary<string, object> newDict = new Dictionary<string, object>()
            {
                {BillImportDetailModel_Enum.ReturnedStatus.ToString(), returned },
                {BillImportDetailModel_Enum.UpdatedBy.ToString(), Global.AppFullName },
                {BillImportDetailModel_Enum.UpdatedDate.ToString(), DateTime.Now },

            };
            SQLHelper<BillImportDetailModel>.UpdateFields(newDict, ex1);
            LoadData();
        }

        private void grvData_RowStyle(object sender, RowStyleEventArgs e)
        {
            bool isCheck = TextUtils.ToBoolean(grvData.GetRowCellValue(e.RowHandle, colIsSelected));
            if (isCheck)
            {
                e.Appearance.BackColor = Color.LightGray;
            }
        }

        private void btnReturned_Click(object sender, EventArgs e)
        {

            ApproveReturned(true);
        }

        private void btnCancelReturned_Click(object sender, EventArgs e)
        {
            ApproveReturned(false);
        }

    }
}