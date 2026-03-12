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
using System.CodeDom.Compiler;
using Forms;
using QRCoder;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using DevExpress.XtraPrinting;
namespace BMS
{
    public partial class frmReportImportExport_KhoDemo : _Forms
    {
        public ProductGroupModel name = new ProductGroupModel();
        public delegate void Signal(string signal);
        public Signal GetSignal;
        int Show;
        string _warehouseCode = "";

        int wareHouseId = 0;
        public frmReportImportExport_KhoDemo()
        {
            InitializeComponent();
        }

        public frmReportImportExport_KhoDemo(string warehouseCode)
        {
            InitializeComponent();
            _warehouseCode = warehouseCode;

            WarehouseModel warehouse = SQLHelper<WarehouseModel>.FindByAttribute("WarehouseCode", warehouseCode).FirstOrDefault();
            if (warehouse != null)
            {
                wareHouseId = warehouse.ID;
            }
        }

        private void frmProductSale_Load(object sender, EventArgs e)
        {
            this.Text += " - " + _warehouseCode;
            dtpFromDate.Value.AddMonths(-1);
            //grdData.Visible = false;
            //tableLayoutPanel2.SetColumnSpan(grdMaster, 2);
            loadProductGroup();
        }

        private void loadProductGroup()
        {
            //treeData.DataSource = SQLHelper<ProductGroupRTCModel>.FindAll().Where(x => x.WarehouseID == 2).ToList();
            treeData.DataSource = SQLHelper<ProductGroupRTCModel>.FindByAttribute("WarehouseID", 1).OrderBy(x => x.NumberOrder).ToList();
        }

        void loadGrdData()
        {
            DateTime dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);
            int IDTree = TextUtils.ToInt(treeData.FocusedNode.GetValue(colIDTree));

            if (chkAll.Checked)
            {
                IDTree = 0;
            }
            DataTable dt = TextUtils.LoadDataFromSP("spGetDataReportImportExport_ProductRTC", "A",
                new string[] { "@StartDate", "@EndDate", "@Group", "@Find", "@WarehouseCode" },
                new object[] { dateTimeS, dateTimeE, IDTree, txtFilterText.Text.Trim(), _warehouseCode });
            grdData.DataSource = dt;
            //LoadDataDetail();
        }

        void LoadDataDetail()
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            DataTable dt = TextUtils.LoadDataFromSP("LoadProductRTCDetail", "A", new string[] { "@ID" }, new object[] { id });
            grdData.DataSource = dt;
        }

        /// <summary>
        /// load dữ liệu vào bảng grdData khi thay đổi tại bảng treeData
        /// </summary>
        //public string GroupName;
        private void treeData_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            string Id = TextUtils.ToString(treeData.FocusedNode.GetValue(colID));
            loadGrdData();
        }

        /// <summary>
        /// find data in DataBase
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFind_Click(object sender, EventArgs e)
        {
            loadGrdData();
        }
        /// <summary>
        /// sử dụng nút enter để tìm kiếm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFilterText_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    loadGrdData();
            //}
        }

        private void chiTiếtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string ProductName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductName));
            string ProductCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductCode));
            string NumberDauKy = TextUtils.ToString(grvData.GetFocusedRowCellValue(colNumberInStoreDauKy));
            string NumberCuoiKy = TextUtils.ToString(grvData.GetFocusedRowCellValue(colNumberInStoreCuoiKy));
            string Import = TextUtils.ToString(grvData.GetFocusedRowCellValue(colTotalImport));
            string Export = TextUtils.ToString(grvData.GetFocusedRowCellValue(colTotalExport));
            //string Borrowing = TextUtils.ToString(grvData.GetFocusedRowCellValue(colBorrowing));
            //string NumberReal = TextUtils.ToString(grvData.GetFocusedRowCellValue(colNumberReal));
            if (ID == 0) return;
            ProductRTCModel model = (ProductRTCModel)ProductRTCBO.Instance.FindByPK(ID);
            frmMaterialDetailOfProductRTC frm = new frmMaterialDetailOfProductRTC(wareHouseId);
            frm.ProductRTCID = model.ID;
            frm.ProductName = ProductName;
            frm.ProductCode = ProductCode;
            frm.NumberDauKy = NumberDauKy;
            frm.NumberCuoiKy = NumberCuoiKy;
            //frm.NumberReal = NumberReal;
            //frm.Borrowing = Borrowing;
            frm.Import = Import;
            frm.Export = Export;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //loadGrdData();
            }
        }


        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //LoadDataDetail();
        }

        private void showNCCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string arrSv = File.ReadAllText(Path.Combine(Application.StartupPath, "UnHideNCC.txt"));
            if (TextUtils.ToInt(arrSv) == 1)
            {
                grdData.Visible = false;
                //tableLayoutPanel2.SetColumnSpan(grdData, 2);
                Show = 0;
            }
            else
            {
                //tableLayoutPanel2.SetColumnSpan(grdData, 1);
                grdData.Visible = true;
                Show = 1;
            }

        }

        private void frmProductSale_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (Show == 0)
            //    File.WriteAllText(Path.Combine(Application.StartupPath, "UnHideNCC.txt"), "0");
            //else
            //    File.WriteAllText(Path.Combine(Application.StartupPath, "UnHideNCC.txt"), "1");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyLib.ExportExcelGrid(grvData);
        }

        private void treeData_FocusedNodeChanged_1(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            loadGrdData();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            loadGrdData();
        }
    }

}


