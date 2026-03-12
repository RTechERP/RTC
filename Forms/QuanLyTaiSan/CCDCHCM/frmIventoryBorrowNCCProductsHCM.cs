using BMS.Business;
using BMS.Model;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using Forms.Technical;
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
	public partial class frmIventoryBorrowNCCProductsHCM : _Forms
	{
		public int warehouseID;
		int totalReturnNCC = 0;
		public frmIventoryBorrowNCCProductsHCM()
		{
			InitializeComponent();
		}

		private void frmIventoryBorrowNCCProductsHCM_Load(object sender, EventArgs e)
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

            DataTable dt = TextUtils.LoadDataFromSP("spGetInventoryBorrowSupplierDemo", "A",
                new string[] { "@FilterText", "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@SupplierDemoID", "@WarehouseID" },
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
        }

		private void btnFind_Click(object sender, EventArgs e)
		{
			LoadData();
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

				grvMaster.OptionsPrint.UsePrintStyles = true;

				try
				{
					grvMaster.ExportToXls(sfd.FileName, optionsEx);
					Process.Start(sfd.FileName);
				}
				catch (Exception)
				{
				}
			}
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

		private void btnNext_Click(object sender, EventArgs e)
		{
			if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
			txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
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

		private void grvMaster_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
		{
			var item = e.Item as DevExpress.XtraGrid.GridColumnSummaryItem;
			if (item == null || item.FieldName != "TotalQuantityReturnNCC") return;

			if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
			{
				e.TotalValue = totalReturnNCC;
			}
		}

		private void grvMaster_CellMerge(object sender, CellMergeEventArgs e)
		{
			GridView view = sender as GridView;
			try
			{
				if (e.Column.FieldName == "TotalQuantityReturnNCC" || e.Column.FieldName == "ProductGroupName"
					|| e.Column.FieldName == "ProductCode" || e.Column.FieldName == "ProductName" || e.Column.FieldName == "ProductCodeRTC"
					|| e.Column.FieldName == "SuplierSaleName" || e.Column.FieldName == "CustomerName")
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
			int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colProductRTCID));
			string ProductName = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colProductName));
			string ProductCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colProductCode));
			string NumberDauKy = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colNumber));
			string NumberCuoiKy = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colInventoryLate));
			string Import = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colImport));
			string Export = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colExport));
			string Borrowing = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colBorrowing));
			string NumberReal = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colNumberReal));
			if (ID == 0) return;
			ProductRTCModel model = SQLHelper<ProductRTCModel>.FindByID(ID);
			frmMaterialDetailOfProductRTC frm = new frmMaterialDetailOfProductRTC(warehouseID);
			frm.ProductRTCID = model.ID;
			frm.ProductName = ProductName;
			frm.ProductCode = ProductCode;
			frm.NumberDauKy = NumberDauKy;
			frm.NumberCuoiKy = NumberCuoiKy;
			frm.NumberReal = NumberReal;
			frm.Borrowing = Borrowing;
			frm.Import = Import;
			frm.Export = Export;
			if (frm.ShowDialog() == DialogResult.OK)
			{
				LoadData();
			}
		}

		private void chiTiếtPhiếuNhậpToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int ImportID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colBillImportID));
			if (ImportID <= 0)
			{
				return;
			}
			BillImportTechnicalModel model = SQLHelper<BillImportTechnicalModel>.FindByID(ImportID);
			frmBillImportTechDetail_New frm = new frmBillImportTechDetail_New(warehouseID);
			frm.IDDetail = ImportID;
			frm.billImport = model;
			if (frm.ShowDialog() == DialogResult.OK)
			{
				LoadData();
			}
		}

		private void chiTiếtPhiếuXuấtToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int ExportID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colBillExportID));
			if (ExportID <= 0)
			{
				return;
			}
			BillExportTechnicalModel model = SQLHelper<BillExportTechnicalModel>.FindByID(ExportID);
			frmBillExportTechDetail_New frm = new frmBillExportTechDetail_New(warehouseID);
			frm.IDDetail = ExportID;
			frm.billExport = model;
			if (frm.ShowDialog() == DialogResult.OK)
			{
				LoadData();
			}
		}

		private void chiTiếtPhiếuMượnToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int BorrowID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDBorrow));
			if (BorrowID <= 0)
			{
				return;
			}
			HistoryProductRTCModel model = SQLHelper<HistoryProductRTCModel>.FindByID(BorrowID);
			frmProductHistoryBorrowDetailAdmin frm = new frmProductHistoryBorrowDetailAdmin(warehouseID);
			frm._id = BorrowID;
			frm.historyProductRTC = model;
			if (frm.ShowDialog() == DialogResult.OK)
			{
				LoadData();
			}
		}
	}
}
