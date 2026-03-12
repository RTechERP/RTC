using BMS.Business;
using BMS.Model;
using DevExpress.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace BMS
{
	public partial class frmBillFilm : _Forms
	{
		int _rownIndex = 0;
		public frmBillFilm()
		{
			InitializeComponent();
		}

		private void frmQuotation_Load(object sender, EventArgs e)
		{
			loadData();
		}

		private void loadData()
		{
			try
			{
				txtPageNumber.Text = "1";
				txtTotalPage.Text = "1";
				//grvMaster.FocusedRowHandle = -1;
				DataSet oDataSet = loadDataSet();

				if (_rownIndex >= grvMaster.RowCount)
					_rownIndex = 0;
				if (_rownIndex > 0)
					grvMaster.FocusedRowHandle = _rownIndex;
				grvMaster.SelectRow(_rownIndex);

				txtTotalPage.Text = TextUtils.ToString(oDataSet.Tables[1].Rows[0][0]);
			}
			catch (Exception)
			{
			}
		}

		DataSet loadDataSet()
		{
			DataSet oDataSet = TextUtils.LoadDataSetFromSP("[sploadBill]"
					, new string[] { "@FilterText" }
					, new object[] { txtFilterText.Text.Trim() });

			grdMaster.DataSource = oDataSet.Tables[0];

			return oDataSet;
		}

		#region Buttons Events
		private void btnNew_Click(object sender, EventArgs e)
		{
			frmGoodsReceivedNote frm = new frmGoodsReceivedNote();
			if (frm.ShowDialog() == DialogResult.OK)
			{
				loadDataSet();
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (!grvMaster.IsDataRow(grvMaster.FocusedRowHandle))
				return;

			int strID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue("ID"));
			if (strID == 0) return;

			string strName = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCode));

			//if (QuotationDetailBO.Instance.CheckExist("QuotationID", strID))
			//{
			//    MessageBox.Show("Báo giá này đã có danh sách sản phẩm nên không thể xóa được!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
			//    return;
			//}

			if (MessageBox.Show(String.Format("Bạn có chắc muốn xóa [{0}] không?", strName), TextUtils.Caption,
				MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				try
				{
					BillFilmBO.Instance.Delete(strID);
					BillFilmDetailBO.Instance.DeleteByAttribute("BillID", strID);
					grvMaster.DeleteSelectedRows();
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
			int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
			BillFilmModel billFilmModel = (BillFilmModel)BillFilmBO.Instance.FindByPK(id);
			if (TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colTypeBill)) == false)
			{
				
				frmGoodsDeliveryNote frm = new frmGoodsDeliveryNote();
				frm._BillFilm = billFilmModel;
				if (frm.ShowDialog() == DialogResult.OK)
				{
					loadData();
					loadGrid();
				}
			}
			else
			{
				frmGoodsReceivedNote frm = new frmGoodsReceivedNote();
				frm._BillF = billFilmModel;

				if (frm.ShowDialog() == DialogResult.OK)
				{
					loadData();
					loadGrid();
				}
			}
		}

		private void btnFind_Click(object sender, EventArgs e)
		{
			loadData();
		}

		private void btnFirst_Click(object sender, EventArgs e)
		{
			if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
			txtPageNumber.Text = "1";
			loadDataSet();
		}

		private void btnPrev_Click(object sender, EventArgs e)
		{
			if (int.Parse(txtPageNumber.Text) == 1) return;
			txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
			loadDataSet();
		}

		private void btnNext_Click(object sender, EventArgs e)
		{
			if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
			txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
			loadDataSet();
		}

		private void btnLast_Click(object sender, EventArgs e)
		{
			if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
			txtPageNumber.Text = "" + txtTotalPage.Text;
			loadDataSet();
		}

		private void txtPageSize_ValueChanged(object sender, EventArgs e)
		{
			loadData();
		}

		void loadGrid()
		{
			DataTable dt = TextUtils.LoadDataFromSP("spGetBillFimDetail", "A"
				   , new string[] { "@BillFilmID" }
				   , new object[] { TextUtils.ToInt64(grvMaster.GetFocusedRowCellValue(colID)) });
			grdData.DataSource = dt;
		}
		private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			loadGrid();
			if (TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colTypeBill)) == true)
			{
				colNeededBigBoxQty.Visible = false;
			}
			else
			{
				colNeededBigBoxQty.Visible = true;
			}
		}

		void approved(bool isApproved)
		{
			//if (MessageBox.Show(string.Format("Bạn có chắc muốn{0} duyệt báo giá này?", isApproved ? "" : " bỏ"), TextUtils.Caption, MessageBoxButtons.YesNo,
			//	MessageBoxIcon.Question) == DialogResult.No) return;

			//int iD = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
			//_rownIndex = grvMaster.FocusedRowHandle;

			//string sql = string.Format(@"UPDATE dbo.Quotation SET IsApproved = {0} WHERE ID = {1}", isApproved ? 1 : 0, iD);

			//TextUtils.ExcuteSQL(sql);
			//loadData();
		}

		private void btnIsApproved_Click(object sender, EventArgs e)
		{
			approved(true);
		}

		private void btnCancelApproved_Click(object sender, EventArgs e)
		{
			approved(false);
		}

		private void btnGoodsDeliveryNote_Click(object sender, EventArgs e)
		{
			frmGoodsDeliveryNote frm = new frmGoodsDeliveryNote();
			if (frm.ShowDialog() == DialogResult.OK)
			{
				loadDataSet();
			}
		}
	}
}
