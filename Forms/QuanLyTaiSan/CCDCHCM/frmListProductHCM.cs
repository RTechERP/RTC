using BMS.Model;
using BMS.Utils;
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
	public partial class frmListProductHCM : _Forms
	{
		int warehouseID = 2;
		public frmListProductHCM()
		{
			InitializeComponent();
		}
		private void frmListProductHCM_Load(object sender, EventArgs e)
		{
			this.Text += " - HCM";
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

		private void LoadData()
		{
			int productGroupID = TextUtils.ToInt(grvDataGroup.GetFocusedRowCellValue("ID"));
			if (productGroupID <= 0) return;

			DataTable dt = TextUtils.LoadDataFromSP("spGetProductRTC", "A",
								new string[] { "@ProductGroupID", "@Keyword", "@CheckAll", "@WarehouseID" },
								new object[] { productGroupID, txtKeyword.Text.Trim(), 1, warehouseID });

			grdData.DataSource = dt;
		}

		private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			frmListProductsHCMDetail frm = new frmListProductsHCMDetail(warehouseID);
			frm.productGroupId = TextUtils.ToInt(grvDataGroup.GetFocusedRowCellValue(colID));
			if (frm.ShowDialog() == DialogResult.OK)
			{
				if (frm.totalRecords.Count > 0) LoadData();
			}
		}

		private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int rowHandle = grvData.FocusedRowHandle;
			int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
			if (id <= 0) return;
			ProductRTCModel product = SQLHelper<ProductRTCModel>.FindByID(id);
			frmListProductsHCMDetail frm = new frmListProductsHCMDetail(warehouseID);
			frm.product = product;
			frm.productGroupId = TextUtils.ToInt(grvDataGroup.GetFocusedRowCellValue(colID));
			if (frm.ShowDialog() == DialogResult.OK)
			{
				if (frm.totalRecords.Count > 0)
				{
					LoadData();
					grvData.FocusedRowHandle = rowHandle;
				}
			}
		}

		private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
			if (id <= 0) return;
			string productCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductCode));
			DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá sản phẩm [{productCode}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dialog == DialogResult.Yes)
			{
				var myDict = new Dictionary<string, object>()
				{
					{"IsDelete",true },
				};
				SQLHelper<ProductRTCModel>.UpdateFieldsByID(myDict, id);
				grvData.DeleteSelectedRows();
			}
		}

		private void btnFind_Click(object sender, EventArgs e)
		{
			LoadData();
		}

		private void grvData_DoubleClick(object sender, EventArgs e)
		{
			btnEdit_ItemClick(null, null);
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

		private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			
		}

		private void grvDataGroup_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			LoadData();
		}
	}
}
