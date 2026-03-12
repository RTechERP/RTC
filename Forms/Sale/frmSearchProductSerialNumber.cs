using BMS.Model;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class frmSearchProductSerialNumber : _Forms
    {
        public frmSearchProductSerialNumber()
        {
            InitializeComponent();
        }

        private void frmSearchProductSerialNumber_Load(object sender, EventArgs e)
        {
            LoadData();
            grdExport.ContextMenuStrip = contextMenuStrip1;
            grdImport.ContextMenuStrip = contextMenuStrip2;
        }
        // PQ.Chien - UPDATE - 29/04/2025 - Input tìm kiếm theo nhiều trường dữ liệu
        private void LoadData()
        {
            DataSet dt = TextUtils.LoadDataSetFromSP("spGetSearchProductSerialNumber", new string[] { "@FilterText" }, new object[] { txtSerialNumber.Text.Trim() });
            grdImport.DataSource = dt.Tables[0];
            grdExport.DataSource = dt.Tables[1];
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void grvImport_KeyDown(object sender, KeyEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(view.GetFocusedRowCellValue(view.FocusedColumn)));
                e.Handled = true;
            }
        }

        private void grvExport_KeyDown(object sender, KeyEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(view.GetFocusedRowCellValue(view.FocusedColumn)));
                e.Handled = true;
            }
        }

        // PQ.Chien - UPDATE - 29/04/2025 - Xem chi tiết phiếu nhập, phiếu xuất

        private void btnBillExportDetail_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvExport.GetFocusedRowCellValue(colBillExportID));
            if (ID == 0) return;

            BillExportModel model = SQLHelper<BillExportModel>.FindByID(ID);
            frmBillExportDetailNew frm = new frmBillExportDetailNew(false);
            frm.billExport = model;
            frm.IDDetail = ID;
            frm.WarehouseCode = "HN";
            if(frm.ShowDialog() == DialogResult.OK)
            {

            }
            

        }

        private void btnBillImportDetail_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvImport.GetFocusedRowCellValue(colIDBillImport));
            if (ID == 0) return;

            BillImportModel model = SQLHelper<BillImportModel>.FindByID(ID);
            frmBillImportDetail frm = new frmBillImportDetail();
            frm.billImport = model;
            frm.IDDetail = ID;
            frm.WarehouseCode = "HN";
            if (frm.ShowDialog() == DialogResult.OK)
            {

            }
        }
    }
}
