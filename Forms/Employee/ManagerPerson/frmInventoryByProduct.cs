using BMS.Business;
using BMS.Model;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;

namespace BMS
{
    public partial class frmInventoryByProduct : _Forms
    {
        public frmInventoryByProduct()
        {
            InitializeComponent();
        }

        private void frmInventoryByDate_Load(object sender, EventArgs e)
        {
            loadData();
        }
        void loadData()
        {
            grdData.DataSource = null;
            string keyWord = txtProductCode.Text.ToString();

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang load..."))
            {
                DataTable data = TextUtils.LoadDataFromSP("spGetProductInventoryByKeyword", "spGetProductInventoryByKeyword",
                                        new string[] { "Keyword" },
                                        new object[] { keyWord });
                grdData.DataSource = data;
            }

        }




        private void btnLoad_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void chiTiếtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(productID));
            if (ID == 0) return;
            string warehouseType = TextUtils.ToString(grvData.GetFocusedRowCellValue(WarehouseType));
            string wareHouseName = TextUtils.ToString(grvData.GetFocusedRowCellValue(WarehouseName));
            List<WarehouseModel> wareHouse = SQLHelper<WarehouseModel>.FindAll().Where(p => p.WarehouseName == wareHouseName).ToList();
            switch (warehouseType)
            {
                case "Sale":
                    string NumberDauKy1 = TextUtils.ToString(0);
                    string NumberCuoiKy1 = TextUtils.ToString(grvData.GetFocusedRowCellValue(InventoryTotal));
                    frmChiTietSanPhamSale frm = new frmChiTietSanPhamSale();
                    frm.WarehouseCode = wareHouse[0].WarehouseCode;
                    frm.productSaleID = ID;
                    frm.NumberDauKy = NumberDauKy1;
                    frm.NumberCuoiKy = NumberCuoiKy1;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        loadData();
                    }
                    break;
                case "Demo":


                    string productName = TextUtils.ToString(grvData.GetFocusedRowCellValue(ProductName));
                    string productCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(ProductCode));
                    string NumberDauKy = TextUtils.ToString(0);
                    string NumberCuoiKy = TextUtils.ToString(grvData.GetFocusedRowCellValue(InventoryTotal));
                    string Import = TextUtils.ToString(0);
                    string Export = TextUtils.ToString(0);
                    string Borrowing = TextUtils.ToString(grvData.GetFocusedRowCellValue(NumberBorrowing));
                    string NumberReal = TextUtils.ToString(grvData.GetFocusedRowCellValue(InventoryReal));

                    frmMaterialDetailOfProductRTC frmDemo = new frmMaterialDetailOfProductRTC(wareHouse[0].ID);
                    frmDemo.ProductRTCID = ID;
                    frmDemo.ProductName = productName;
                    frmDemo.ProductCode = productCode;
                    frmDemo.NumberDauKy = NumberDauKy;
                    frmDemo.NumberCuoiKy = NumberCuoiKy;
                    frmDemo.NumberReal = NumberReal;
                    frmDemo.Borrowing = Borrowing;
                    frmDemo.Import = Import;
                    frmDemo.Export = Export;


                    if (frmDemo.ShowDialog() == DialogResult.OK)
                    {
                        loadData();
                    }
                    break;
                default:
                    MessageBox.Show("Hãy chọn sản phẩm!", "Thông báo");
                    break;
            }
        }

        private void grdData_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip2.Show(this, e.Location);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = Path.Combine(f.SelectedPath, $"DanhSachTonKho.xlsx");

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

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn));
                if (string.IsNullOrEmpty(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }
    }
}
