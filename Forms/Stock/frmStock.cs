using BMS.Business;
using BMS.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BMS
{
	public partial class frmStock : _Forms
	{
		public frmStock()
		{
			InitializeComponent();
		}

		private void frmSupplier_Load(object sender, EventArgs e)
		{

			loadData();

		}

		#region Methods
		private void loadData()
		{
			DataTable dt = new DataTable();
			dt = TextUtils.LoadDataFromSP("spLoadStock", "A"
				, new string[] { "@Find" }
				, new object[] { txtFilterText.Text.Trim() });
			grdData.DataSource = dt;
		}
		#endregion

		#region Buttons Events
		private void btnNew_Click(object sender, EventArgs e)
		{
			frmStockDetail frm = new frmStockDetail();
			//frm.check = 0;
			if (frm.ShowDialog() == DialogResult.OK)
			{
				loadData();
			}
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			try
			{
				frmStockDetail frm = new frmStockDetail();
				int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
				frm.Stock = (StockModel)StockBO.Instance.FindByPK(id);
				if (frm.ShowDialog() == DialogResult.OK)
				{
					loadData();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (!grvData.IsDataRow(grvData.FocusedRowHandle))
				return;

			int strID = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));

			string strName = TextUtils.ToString(grvData.GetFocusedRowCellValue("StockName"));

			if (RequestPriceDetailBO.Instance.CheckExist("StockName", strID))
			{
				MessageBox.Show("Kho này đã phát sinh ở các nghiệp vụ khác nên không thể xóa được!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return;
			}

			if (MessageBox.Show(String.Format("Bạn có chắc muốn xóa [{0}] không?", strName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				try
				{
					//SupplierBO.Instance.Delete(strID);
					StockBO.Instance.Delete(strID);
					grvData.DeleteSelectedRows();
				}
				catch
				{
					MessageBox.Show("Có lỗi xảy ra khi thực hiện thao tác, xin vui lòng thử lại sau.");
				}
			}
		}

		#endregion

		private void grdData_DoubleClick(object sender, EventArgs e)
		{
			if (grvData.RowCount > 0 && btnEdit.Enabled == true)
				btnEdit_Click(null, null);
		}

		private void btnFind_Click(object sender, EventArgs e)
		{
			loadData();
		}



	}
}
