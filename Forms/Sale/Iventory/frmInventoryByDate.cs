using BMS.Business;
using BMS.Model;
using DevExpress.XtraGrid.Columns;
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

namespace BMS
{
    public partial class frmInventoryByDate : _Forms
    {
        public frmInventoryByDate()
        {
            InitializeComponent();
        }

        private void frmInventoryByDate_Load(object sender, EventArgs e)
        {
            loadData();
        }
        void loadData()
        {

            DateTime dateTime = dtpDate.Value.Date.AddDays(+1);
            DataTable dt = TextUtils.LoadDataFromSP("spGetInventoryByDate", "A", new string[] { "@DateValues", "@WarehouseCode" }, new object[] { dateTime, "HN" });
            grdData.DataSource = dt;

            for (int i = 0; i < grvData.RowCount; i++)
            {
                int export;
                int import;
                int ID1 = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductID));
                int ID2 = i == 0 ? 0 : TextUtils.ToInt(grvData.GetRowCellValue(i - 1, colProductID));

                //--//
                export = TextUtils.ToInt(grvData.GetRowCellValue(i, colExport));
                import = i == 0 || ID1 != ID2 ? TextUtils.ToInt(grvData.GetRowCellValue(i, colQtyImport)) : TextUtils.ToInt(grvData.GetRowCellValue(i, colQtyImport)) + TextUtils.ToInt(grvData.GetRowCellValue(i - 1, colQtyValues2));
                grvData.SetRowCellValue(i, colQtyValues2, import);
                //---// cột tồn kho

                grvData.SetRowCellValue(i, colQtyValues1, (export >= import ? 0 : import - export));

                //---// cột số lượng
                int values1 = TextUtils.ToInt(grvData.GetRowCellValue(i, colQtyImport));
                int values2 = TextUtils.ToInt(grvData.GetRowCellValue(i, colQtyValues1));
                grvData.SetRowCellValue(i, colQtyValues3, (values1 >= values2 ? values2 : values2 - (TextUtils.ToInt(grvData.GetRowCellValue(i - 1, colQtyValues1)))));

            }
            //string filterString = "QtyValues3 > 0 ";
            //grvData.Columns["QtyValues3"].FilterInfo = new ColumnFilterInfo(filterString);


           grvData.ActiveFilterString = "QtyValues1 > 0 ";

        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            grvData.ActiveFilterString = "";
            try
            {
                MyLib.ShowWaitForm("Load data ...");
                loadData();
            }
            finally { MyLib.CloseWaitForm(); }

        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            sfd.FileName = $"DanhSachTonKho_TheoNgayNhap_{DateTime.Now.ToString("ddMMyy")}.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                DevExpress.XtraPrinting.XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
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

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(view.GetFocusedRowCellValue(view.FocusedColumn)));
                e.Handled = true;
            }
        }

        private void chiTiếtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime dateTime = dtpDate.Value.Date.AddDays(+1);
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductID));
            if (ID == 0) return;
            InventoryModel model = (InventoryModel)InventoryBO.Instance.FindByCode("ProductSaleID", $"{ID}");
            frmChitietSanPhamSaleForKT frm = new frmChitietSanPhamSaleForKT();
            frm.values = dateTime;
            frm.productSaleID = ID;
            frm.NumberDauKy = model.TotalQuantityFirst;
            frm.WarehouseCode = "HN";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //loadGrdData();
            }
        }
    }
}
