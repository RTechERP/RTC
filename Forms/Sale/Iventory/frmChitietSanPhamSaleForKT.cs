using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static Forms.Classes.cGlobVar;

namespace BMS
{
    public partial class frmChitietSanPhamSaleForKT : _Forms
    {
        public decimal NumberDauKy ;
        public DateTime values;
        

        public ProductSaleModel oProductSaleModel = new ProductSaleModel();

        public int productSaleID;
        DataSet dsHistory = new DataSet();

        public string WarehouseCode;
       
        public frmChitietSanPhamSaleForKT()
        {
            InitializeComponent();
        }

        private void frmChiTietSanPhamSale_Load(object sender, EventArgs e)
        {
            cbProductCodeNew.EditValue = productSaleID;
            txtNumberDauKy.Text = TextUtils.ToString(NumberDauKy);
            LoadData();
            loadcbProductCode();
        }

        void LoadData()
        {
            productSaleID = TextUtils.ToInt(cbProductCodeNew.EditValue);
            dsHistory = TextUtils.LoadDataSetFromSP("spGetHistoryImportExportInventory_ByDate",
                                    new string[] { "@ProductSaleID", "@WarehouseCode", "@DateValues" },
                                    new object[] { productSaleID, WarehouseCode, values });

            grdDataImport.DataSource = dsHistory.Tables[0];
            grdDataExport.DataSource = dsHistory.Tables[1];
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
            DataTable dtProduct = dsHistory.Tables[2];
            cbProductCodeNew.Properties.DisplayMember = "ProductNewCode";
            cbProductCodeNew.Properties.ValueMember = "ProductSaleID";
            cbProductCodeNew.Properties.DataSource = dtProduct;


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
            int BillID = TextUtils.ToInt(grvDataImport.GetFocusedRowCellValue(colImportID));
            BillImportModel model = (BillImportModel)BillImportBO.Instance.FindByPK(BillID);
            frmBillImportDetail frm = new frmBillImportDetail();
            frm.IDDetail = BillID;
            frm.billImport = model;
            frm.WarehouseCode = WarehouseCode;
            if (frm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void grvDataExport_DoubleClick(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvDataExport.GetFocusedRowCellValue(colExportID));
            BillExportModel model = (BillExportModel)BillExportBO.Instance.FindByPK(id);
            frmBillExportDetailNew frm = new frmBillExportDetailNew(false);
            frm.IDDetail = id;
            frm.billExport = model;
            frm.WarehouseCode = WarehouseCode;
            if (frm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        
    }
}
