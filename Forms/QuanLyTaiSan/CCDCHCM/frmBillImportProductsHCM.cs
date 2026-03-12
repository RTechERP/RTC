using BMS.Model;
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
	public partial class frmBillImportProductsHCM : _Forms
	{
		int warehouseID = 2;
		public frmBillImportProductsHCM()
		{
			InitializeComponent();
		}

		private void frmBillImportProductsHCM_Load(object sender, EventArgs e)
		{
			this.Text += " - HCM";
			cboStatus.SelectedIndex = 0;
			dtpDateStart.Value = dtpDateStart.Value.AddMonths(-1);
			LoadData();
		}

		void LoadData()
		{
			DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day, 0, 0, 0);
			DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59);
			int status = cboStatus.SelectedIndex - 1;

			DataTable dt = TextUtils.LoadDataFromSP("spGetBillImportTechnical", "A"
				, new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@Status", "@FilterText", "@WarehouseID" }
				, new object[] { 1, 999999, dateStart, dateEnd, status, txtKeyword.Text.Trim(), warehouseID });
			grdData.DataSource = dt;

			LoadDetail();
		}

		void LoadDetail()
		{
			int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
			DataTable dt = TextUtils.LoadDataFromSP("spGetBillImportDetailTechnical", "A", new string[] { "@ID" }, new object[] { id });
			grdDetail.DataSource = dt;
		}
		private void btnFind_Click(object sender, EventArgs e)
		{
			LoadData();
		}

		private void grdData_Click(object sender, EventArgs e)
		{

		}

		private void btnAddBillImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			//frmBillImportTechDetail frm = new frmBillImportTechDetail(warehouseID);
			//if (frm.ShowDialog() == DialogResult.OK)
			//{
			//	LoadData();
			//}

			frmBillImportDetailProductsHCM frm = new frmBillImportDetailProductsHCM(warehouseID);
			if (frm.ShowDialog() == DialogResult.OK)
			{
				if (frm.totalRecords.Count > 0) LoadData();
			}
		}

		private void btnEditBillImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int rowHandle = grvData.FocusedRowHandle;
			int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
			if (id <= 0) return;
			BillImportTechnicalModel billImport = SQLHelper<BillImportTechnicalModel>.FindByID(id);
			frmBillImportDetailProductsHCM frm = new frmBillImportDetailProductsHCM(warehouseID);
			frm.billImport = billImport;
			if (frm.ShowDialog() == DialogResult.OK)
			{
				if (frm.totalRecords.Count > 0)
				{
					LoadData();
					grvData.FocusedRowHandle = rowHandle;
				}
			}

			//bool isApproved = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colStatus));
			//string billCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colBillCode));

			//var focusedRowHandle = grvData.FocusedRowHandle;
			//int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
			//if (ID == 0) return;
			//BillImportTechnicalModel model = SQLHelper<BillImportTechnicalModel>.FindByID(ID);
			//frmBillImportTechDetail frm = new frmBillImportTechDetail(warehouseID);
			//if (isApproved)
			//{
			//	frm.IsEdit = true;
			//}
			//frm.billImport = model;
			//frm.IDDetail = ID;
			//if (frm.ShowDialog() == DialogResult.OK)
			//{
			//	LoadData();
			//	grvData.FocusedRowHandle = focusedRowHandle;
			//}
		}

		private void btnDeleteBillImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
			if (id <= 0) return;

			bool status = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colStatus));
			string billCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colBillCode));

			if (status)
			{
				MessageBox.Show($"Phiếu nhập [{billCode}] đã được duyệt.\nBạn không thể xóa phiếu này!)", "Thông báo");
				return;
			}

			DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá phiếu nhập [{billCode}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dialog == DialogResult.Yes)
			{
				var myDict = new Dictionary<string, object>()
				{
					{"IsDelete",true },
				};

				SQLHelper<BillImportTechnicalModel>.DeleteModelByID(id);
				SQLHelper<BillImportDetailTechnicalModel>.DeleteByAttribute("BillImportTechID", id);

				HistoryDeleteBillModel history = new HistoryDeleteBillModel()
				{
					BillID = id,
					UserID = Global.UserID,
					DeleteDate = DateTime.Now,
					Name = Global.AppUserName,
					TypeBill = billCode
				};

				SQLHelper<HistoryDeleteBillModel>.Insert(history);
				grvData.DeleteSelectedRows();
			}
		}

		private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			LoadDetail();
		}

		private void grvData_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.C)
			{
				string value = TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn));
				if (string.IsNullOrWhiteSpace(value)) return;
				Clipboard.SetText(value);
				e.Handled = true;
			}
		}

		private void grvData_DoubleClick(object sender, EventArgs e)
		{
			btnEditBillImport_ItemClick(null, null);
		}

		private void grvDetail_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{

		}

		private void grvDetail_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.C)
			{
				string value = TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn));
				if (string.IsNullOrWhiteSpace(value)) return;
				Clipboard.SetText(value);
				e.Handled = true;
			}
		}
	}
}
