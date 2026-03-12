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
using Forms;
using System.Diagnostics;
using System.Collections;
using DevExpress.Data.Linq;
using Microsoft.VisualBasic.ApplicationServices;
using DevExpress.XtraGrid.Views.Grid;

namespace BMS
{
    public partial class frmSummaryReturnDetail : _Forms
    {
        public int _exportDetailID;
        private int warehouseID;

        public frmSummaryReturnDetail()
        {
            InitializeComponent();
        }

        public frmSummaryReturnDetail(int WarehouseID)
        {
            InitializeComponent();
            warehouseID = WarehouseID;
        }

        private void frmSummaryReturnDetail_Load(object sender, EventArgs e)
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetReturnDetailSummaryByExportDetailID", "A", new string[] { "@ExportDetailID" }, new object[] { _exportDetailID });
            dt.Columns.Add("No", typeof(int));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["No"] = i + 1;
            }
            grdData.DataSource = dt;
        }

        private void frmSummaryReturnDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void grvData_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                e.Value = grvData.GetRowHandle(e.ListSourceRowIndex) + 1;
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(Lib.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn)));
            }
            catch
            {
                Clipboard.SetText("");
            }
        }

        private void frmBillImportDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && Keys.KeyCode == Keys.C)
            {
                btnCopy_Click(null, null);
            }
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            int _importID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colBillImportID));
            if (_importID == 0) return;
            string WareHouseCode = ((WarehouseModel)WarehouseBO.Instance.FindByPK(warehouseID)).WarehouseCode;
            BillImportModel billImport = (BillImportModel)BillImportBO.Instance.FindByPK(_importID);
            frmBillImportDetail frm = new frmBillImportDetail();
            frm.billImport = billImport;
            frm.WarehouseCode = WareHouseCode;
            frm.ShowDialog();
            frmSummaryReturnDetail_Load(null, null);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                grvData.OptionsSelection.MultiSelect = false;
                grvData.OptionsPrint.AutoWidth = false;
                grvData.OptionsPrint.ExpandAllDetails = false;
                grvData.OptionsPrint.PrintDetails = true;
                grvData.OptionsPrint.UsePrintStyles = true;
                try
                {
                    grvData.ExportToXls(sfd.FileName);
                    Process.Start(sfd.FileName);
                }
                catch (Exception)
                {
                }
                grvData.OptionsSelection.MultiSelect = true;
            }
        }
    }
}

