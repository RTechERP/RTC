using BMS.Business;
using BMS.Model;
using DevExpress.DataProcessing.InMemoryDataProcessor;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static Forms.Classes.cGlobVar;
using DevExpress.Utils;

namespace BMS
{
    public partial class frmChiTietSanPhamSale : _Forms
    {
        public string code = "";
        public string suplier = "";
        public string ProductName = "";
        public string NumberDauKy = "";
        public string NumberCuoiKy = "";
        public string Import = "";
        public string Export = "";
        public int productSaleID;

        public ProductSaleModel oProductSaleModel = new ProductSaleModel();

        public string WarehouseCode;

        DataSet dsHistory = new DataSet();
        WarehouseModel _warehouse = new WarehouseModel();
        //DataRow _datarow;

        public frmChiTietSanPhamSale()
        {
            InitializeComponent();
            //_warehouse = warehouse;
            //_datarow = datarow;
        }

        private void frmChiTietSanPhamSale_Load(object sender, EventArgs e)
        {
            this.Text += $" - {WarehouseCode}";
            _warehouse = SQLHelper<WarehouseModel>.FindByAttribute("WarehouseCode", WarehouseCode).FirstOrDefault() ?? new WarehouseModel();
            cbProductCode.EditValue = productSaleID;
            LoadData();
            loadcbProductCode();
        }

        void LoadData()
        {
            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tải..."))
            {


                grdDataImport.DataSource = null;
                grdDataExport.DataSource = null;
                grdData.DataSource = null;
                grdDataRequestImport.DataSource = null;
                grdDataRequestExport.DataSource = null;

                int productSaleID = TextUtils.ToInt(cbProductCode.EditValue);
                dsHistory = TextUtils.LoadDataSetFromSP(StoreProcedure.spGetHistoryImportExportInventory,
                                        new string[] { "@ProductSaleID", "@WarehouseCode" },
                                        new object[] { productSaleID, WarehouseCode });


                DataTable dtProduct = dsHistory.Tables[0];
                if (dtProduct.Rows.Count > 0)
                {

                    txtProductName.Text = TextUtils.ToString(dtProduct.Rows[0]["ProductName"]);

                    //txtNumberDauKy.Text =  NumberDauKy;
                    //txtNumberCuoiKy.Text = NumberCuoiKy;
                    //txtImport.Text = TextUtils.ToString(dtProduct.Rows[0]["TotalImport"]);
                    //txtExport.Text = TextUtils.ToString(dtProduct.Rows[0]["TotalExport"]);

                    //txtTotalQuantityKeep.Text = TextUtils.ToString(_datarow["TotalQuantityKeep"]);
                    //txtMinQuantity.Text = TextUtils.ToString(_datarow["MinQuantity"]);
                }

                grdDataImport.DataSource = dsHistory.Tables[1];
                grdDataExport.DataSource = dsHistory.Tables[2];


                //Load data giữ
                DataTable dt = TextUtils.LoadDataFromSP("spGetInventoryProject", "A",
                new string[] { "@ProjectID", "@EmployeeID", "@ProductSaleID", "@Keyword", "@WarehouseID" },
                new object[] { 0, 0, productSaleID, "", _warehouse.ID });

                grdData.DataSource = dt;

                //Load data yêu cầu nhập
                grdDataRequestImport.DataSource = dsHistory.Tables[4];

                //Load data yêu cầu xuatas
                grdDataRequestExport.DataSource = dsHistory.Tables[5];

                Caculator();
            }
        }


        void Caculator()
        {
            decimal totalImport = TextUtils.ToDecimal(grvDataImport.Columns["Qty"].SummaryItem.SummaryValue);
            decimal totalExport = TextUtils.ToDecimal(grvDataExport.Columns["Qty"].SummaryItem.SummaryValue);
            decimal totalRequestExport = TextUtils.ToDecimal(grvDataRequestExport.Columns["Qty"].SummaryItem.SummaryValue);
            decimal totalKeep = TextUtils.ToDecimal(grvData.Columns["TotalQuantityRemain"].SummaryItem.SummaryValue);
            //decimal totalMinquantity = TextUtils.ToDecimal(grvDataExport.Columns["Qty"].SummaryItem.SummaryValue);

            decimal totalLast = TextUtils.ToDecimal(NumberDauKy) + totalImport - totalExport - totalRequestExport - totalKeep;

            txtNumberDauKy.Text = NumberDauKy;
            txtImport.Text = totalImport.ToString("n2");
            txtExport.Text = totalExport.ToString("n2");
            txtRequestExport.Text = totalRequestExport.ToString("n2");
            txtTotalQuantityKeep.Text = totalKeep.ToString("n2");
            txtNumberCuoiKy.Text = totalLast.ToString("n2");
        }

        void loadText()
        {
            //try
            //{
            //    txtProductName.Text = TextUtils.ToString(dtInventory.Rows[0]["ProductName"]);
            //    txtNumberDauKy.Text = TextUtils.ToString(inventory.TotalQuantityFirst);
            //    txtNumberCuoiKy.Text = TextUtils.ToString(inventory.TotalQuantityLast);
            //    txtImport.Text = TextUtils.ToString(inventory.Import);
            //    txtExport.Text = TextUtils.ToString(inventory.Export);
            //}
            //catch(Exception ex)
            //{

            //}
        }
        void loadcbProductCode()
        {
            DataTable dtProduct = dsHistory.Tables[3];
            cbProductCode.Properties.DisplayMember = "ProductCode";
            cbProductCode.Properties.ValueMember = "ProductSaleID";
            cbProductCode.Properties.DataSource = dtProduct;


            ////oProductSaleModel = (ProductSaleModel)ProductSaleBO.Instance.FindByPK(TextUtils.ToInt( cbProductCode.EditValue));
            //inventory = (InventoryModel)InventoryBO.Instance.FindByPK(TextUtils.ToInt( cbProductCode.EditValue));
            //loadGrdData();
            //loadText();
        }
        void loadGrdData()
        {
            //DataTable dtnhap = new DataTable();
            //dtnhap = TextUtils.LoadDataFromSP(StoreProcedure.usp_LoadBill_New, "A", 
            //            new string[] { "@ProductSaleID", "@WarehouseCode" }, 
            //            new object[] { inventory.ProductSaleID, WarehouseCode });          

            //grdDataImport.DataSource = dtnhap;

        }
        void loaddataxuat()
        {
            //DataTable dtxuat = new DataTable();
            //dtxuat = TextUtils.LoadDataFromSP(StoreProcedure.spLoadDataXuatNote_New, "A",
            //            new string[] { "@ProductSaleID", "@WarehouseCode" },
            //            new object[] { inventory.ProductSaleID, WarehouseCode });
            //grdDataExport.DataSource = dtxuat;
        }

        private void cbProductCode_EditValueChanged(object sender, EventArgs e)
        {
            //DataTable dt = new DataTable();
            //dt = TextUtils.Select($"select ProductCode,ID from ProductSale");
            //cbProductCode.Properties.DisplayMember = "ProductCode";
            //cbProductCode.Properties.ValueMember = "ID";
            //cbProductCode.Properties.DataSource = dt;
            //oProductSaleModel = (ProductSaleModel)ProductSaleBO.Instance.FindByPK(TextUtils.ToInt(cbProductCode.EditValue));
            //loadGrdData();
            //loadText();

            LoadData();
        }

        private void grvDataImport_DoubleClick(object sender, EventArgs e)
        {

            GridView view = (GridView)sender;
            if (view == null) return;

            int BillID = TextUtils.ToInt(view.GetFocusedRowCellValue(colImportID.FieldName));
            BillImportModel model = SQLHelper<BillImportModel>.FindByID(BillID);
            frmBillImportDetail frm = new frmBillImportDetail();
            frm.IDDetail = BillID;
            frm.billImport = model;
            frm.WarehouseCode = _warehouse.WarehouseCode;
            frm.Show();
            //if (frm.ShowDialog() == DialogResult.OK)
            //{

            //}
        }

        private void grvDataExport_DoubleClick(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            if (view == null) return;

            int id = TextUtils.ToInt(view.GetFocusedRowCellValue(colExportID.FieldName));
            BillExportModel model = SQLHelper<BillExportModel>.FindByID(id);
            frmBillExportDetailNew frm = new frmBillExportDetailNew(false);
            frm.IDDetail = id;
            frm.billExport = model;
            frm.WarehouseCode = _warehouse.WarehouseCode;
            frm.Show();
            //if (frm.ShowDialog() == DialogResult.OK)
            //{

            //}
        }

        private void grvDataExport_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                int remain = TextUtils.ToInt(grvDataExport.GetRowCellValue(e.RowHandle, colRemain));
                int status = TextUtils.ToInt(grvDataExport.GetRowCellValue(e.RowHandle, colStatusBillExport));


                if (remain != 0 && status == 0)
                {
                    e.Appearance.BackColor = Color.Yellow;
                    e.HighPriority = true;
                }
            }
        }

        private void grvDataExport_KeyDown(object sender, KeyEventArgs e)
        {
            GridView view = (GridView)sender;
            if (view == null) return;
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(view.GetFocusedRowCellValue(view.FocusedColumn)));
                e.Handled = true;
            }
        }

        private void grvDataImport_KeyDown(object sender, KeyEventArgs e)
        {
            GridView view = (GridView)sender;
            if (view == null) return;
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(view.GetFocusedRowCellValue(view.FocusedColumn)));
                e.Handled = true;
            }
        }

        private void btnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }
    }
}
