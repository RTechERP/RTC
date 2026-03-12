using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
	public partial class frmInventoryProductsHCM : _Forms
	{
		public int warehouseID = 2;
		private int _hoverRow = -1;
		private GridCellInfo _oldCellInfo = null;

		public frmInventoryProductsHCM()
		{
			InitializeComponent();
		}

		private void frmInventoryProductsHCM_Load(object sender, EventArgs e)
		{
			LoadProductGroup();
			LoadData();
		}

		void LoadProductGroup()
		{
			//var exp1 = new Expression("WarehouseID", 2);
			var exp2 = new Expression("ProductGroupNo", "CCDC");
			var list = SQLHelper<ProductGroupRTCModel>.FindByExpression(exp2);
			grdDataGroup.DataSource = list;
		}

		void LoadData()
		{
			using (WaitDialogForm fWait = new WaitDialogForm())
			{
				int allProduct = 1;
				int productGroupID = TextUtils.ToInt(grvDataGroup.GetFocusedRowCellValue("ID"));
				if (productGroupID <= 0) return;
				DataTable dtProduct = TextUtils.LoadDataFromSP("spGetInventoryDemo", "A",
									new string[] { "@ProductGroupID", "@Keyword", "@CheckAll", "@WarehouseID" },
									new object[] { productGroupID, txtKeyword.Text.Trim(), allProduct, warehouseID });
				grdData.DataSource = dtProduct;
			}
		}

		private void grvData_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
		{

		}

		private void grvData_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.C)
			{
				Clipboard.SetText(TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn)));
				e.Handled = true;
			}
		}

		private void btnFind_Click(object sender, EventArgs e)
		{
			LoadData();
		}

		private void btnExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			SaveFileDialog f = new SaveFileDialog();
			f.Filter = "Excel Files|*.xlsx";
			f.FileName = $"DanhSachTonKhoHCM_{DateTime.Now.ToString("ddMMyy")}.xlsx";
			if (f.ShowDialog() == DialogResult.OK)
			{
				string filepath = f.FileName;

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

		private void btnAddQrCode_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int rowHandle = grvData.FocusedRowHandle;
			int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductRTCID));
			if (id <= 0) return;
			frmAddQRCodeProductsHCM frm = new frmAddQRCodeProductsHCM(warehouseID);
			frm.Edit = true;
			frm.id = id;
			if (frm.ShowDialog() == DialogResult.OK)
			{
			}
			grvData.FocusedRowHandle = rowHandle;
		}

		private void btnReportNCC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			frmIventoryBorrowNCCProductsHCM frm = new frmIventoryBorrowNCCProductsHCM();
			frm.warehouseID = 1;
			frm.ShowDialog();
		}

		private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
		{
			if (e.RowHandle < 0) return;
			int status = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, "Status"));
			if (status == 1)
			{

				e.Appearance.BackColor = Color.LightYellow;
			}
		}

		void UpdateStatus(int status)
		{
			string statusText = status == 0 ? "đưa vào sử dụng" : "đang giặt";
			int productRTCID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductRTCID));

			if (productRTCID <= 0) return;

			DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn cập nhật thành {statusText} không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dialog == DialogResult.Yes)
			{
				var myDict = new Dictionary<string, object>()
				{
					{ ProductRTCModel_Enum.Status.ToString(),status},
				};

				SQLHelper<ProductRTCModel>.UpdateFieldsByID(myDict, productRTCID);

				LoadData();
			}
		}

		private void btnWashing_Click(object sender, EventArgs e)
		{
			UpdateStatus(1);
		}

		private void btnBackToUse_Click(object sender, EventArgs e)
		{
			UpdateStatus(0);
		}

		private void grvDataGroup_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			LoadData();
		}

        private void btnDetail_Click(object sender, EventArgs e)
        {
			int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductRTCID));
			string ProductName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductName));
			string ProductCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductCode));
			string NumberDauKy = TextUtils.ToString(grvData.GetFocusedRowCellValue(colNumber));
			string NumberCuoiKy = TextUtils.ToString(grvData.GetFocusedRowCellValue(colInventoryLate));
			string Import = TextUtils.ToString(grvData.GetFocusedRowCellValue(colImport));
			string Export = TextUtils.ToString(grvData.GetFocusedRowCellValue(colExport));
			string Borrowing = TextUtils.ToString(grvData.GetFocusedRowCellValue(colBorrowing));
			string NumberReal = TextUtils.ToString(grvData.GetFocusedRowCellValue(colInventoryReal));
			if (ID == 0) return;
			//ProductRTCModel model = (ProductRTCModel)ProductRTCBO.Instance.FindByPK(ID);
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
			frm.Show();

			//if (frm.ShowDialog() == DialogResult.OK)
			//{
			//	//loadGrdData();
			//}
		}
    }
}
