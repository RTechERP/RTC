using BMS.Business;
using BMS.Model;
using DevExpress.XtraGrid.Views.Grid;
using Forms.Technical;
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
    public partial class frmMaterialDetailOfProductRTC : _Forms
    {
        public string ProductName = "";
        public string ProductCode = "";
        public string NumberDauKy = "";
        public string NumberCuoiKy = "";
        public string Import = "";
        public string Export = "";
        public string Borrowing = "";
        public string NumberReal = "";
        public int ProductRTCID;

        int warehouseID;
        public frmMaterialDetailOfProductRTC(int WarehouseID)
        {
            InitializeComponent();

            warehouseID = WarehouseID;
        }

        private void frmMaterialDetailOfProductRTC_Load(object sender, EventArgs e)
        {
            //txtProductCode.Text = ProductCode;
            //txtProductName.Text = ProductName;
            //txtNumberDauKy.Text = NumberDauKy;
            //txtNumberCuoiKy.Text = NumberCuoiKy;
            //txtImport.Text = Import;
            //txtExport.Text = Export;
            //txtBorrowing.Text = Borrowing;
            //txtNumberReal.Text = NumberReal;

            LoadData();
        }

        private void LoadData()
        {
            DataSet dtb = TextUtils.LoadDataSetFromSP("spGetBorrowImportExportProductRTC"
                    , new string[] { "@ProductID", "@WarehouseID" }
                    , new object[] { ProductRTCID, warehouseID });
            grdDataBorrow.DataSource = dtb.Tables[2];
            grdDataExport.DataSource = dtb.Tables[1];
            grdDataImport.DataSource = dtb.Tables[0];


            DataTable dt = TextUtils.LoadDataFromSP("spGetInventoryDemo", "A",
                                            new string[] { "@ProductGroupID", "@Keyword", "@WarehouseID", "@ProductRTCID", "@CheckAll" },
                                            new object[] { 0, "", warehouseID, ProductRTCID, 1 });
            
            if (dt.Rows.Count <= 0)
            {
                return;
            }
            var row = dt.Rows[0];
            txtProductCode.Text = TextUtils.ToString(row["ProductCode"]);
            txtProductName.Text = TextUtils.ToString(row["ProductName"]);
            txtNumberDauKy.Text = TextUtils.ToString(row["Number"]);
            txtNumberCuoiKy.Text = TextUtils.ToString(row["InventoryLate"]);
            txtImport.Text = TextUtils.ToString(row["NumberImport"]);
            txtExport.Text = TextUtils.ToString(row["NumberExport"]);
            txtBorrowing.Text = TextUtils.ToString(row["NumberBorrowing"]);
            txtNumberReal.Text = TextUtils.ToString(row["InventoryReal"]);
        }

        private void grvDataBorrow_DoubleClick(object sender, EventArgs e)
        {
            int BorrowID = TextUtils.ToInt(grvDataBorrow.GetFocusedRowCellValue(colIDBorrow));
            if (BorrowID <= 0)
            {
                return;
            }
            //HistoryProductRTCModel model = (HistoryProductRTCModel)HistoryProductRTCBO.Instance.FindByPK(BorrowID);
            HistoryProductRTCModel model = SQLHelper<HistoryProductRTCModel>.FindByID(BorrowID);
            frmProductHistoryBorrowDetailAdmin frm = new frmProductHistoryBorrowDetailAdmin(warehouseID);
            frm._id = BorrowID;
            frm.historyProductRTC = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void grvDataImport_DoubleClick(object sender, EventArgs e)
        {
            int ImportID = TextUtils.ToInt(grvDataImport.GetFocusedRowCellValue(colImportID));
            if (ImportID <= 0)
            {
                return;
            }
            //BillImportTechnicalModel model = (BillImportTechnicalModel)BillImportTechnicalBO.Instance.FindByPK(ImportID);
            BillImportTechnicalModel model = SQLHelper<BillImportTechnicalModel>.FindByID(ImportID);
            frmBillImportTechDetail_New frm = new frmBillImportTechDetail_New(warehouseID);
            frm.IDDetail = ImportID;
            frm.billImport = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void grvDataExport_DoubleClick(object sender, EventArgs e)
        {
            int ExportID = TextUtils.ToInt(grvDataExport.GetFocusedRowCellValue(colExportID));
            if (ExportID <= 0)
            {
                return;
            }
            //BillExportTechnicalModel model = (BillExportTechnicalModel)BillExportTechnicalBO.Instance.FindByPK(ExportID);
            BillExportTechnicalModel model = SQLHelper<BillExportTechnicalModel>.FindByID(ExportID);
            frmBillExportTechDetail_New frm = new frmBillExportTechDetail_New(warehouseID);
            frm.IDDetail = ExportID;
            frm.billExport = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void grvDataBorrow_KeyDown(object sender, KeyEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(view.GetFocusedRowCellValue(view.FocusedColumn)));
                e.Handled = true;
            }
        }

        private void grvDataImport_KeyDown(object sender, KeyEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(view.GetFocusedRowCellValue(view.FocusedColumn)));
                e.Handled = true;
            }
        }

        private void grvDataExport_KeyDown(object sender, KeyEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(view.GetFocusedRowCellValue(view.FocusedColumn)));
                e.Handled = true;
            }
        }
    }
}
