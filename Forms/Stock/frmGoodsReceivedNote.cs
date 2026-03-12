using BMS.Business;
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
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
	public partial class frmGoodsReceivedNote : _Forms
	{
		public BillFilmDetailModel _BillFilmDetail = new BillFilmDetailModel();
		public BillFilmModel _BillF = new BillFilmModel();
		public frmGoodsReceivedNote()
		{
			InitializeComponent();
		}
		private void frmQuotationDetail_Load(object sender, EventArgs e)
		{
			loadSupplier();
			loadUsers();
			loadCbName();
			loadStock();
			loadData();
			loadCode();
			Delete.Click += Delete_Click;
		}

		private void Delete_Click(object sender, EventArgs e)
		{
			if (grdData.DataSource == null)
				return;
			int strID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));

			string strName = TextUtils.ToString(grvData.GetFocusedRowCellDisplayText(colName));

			if (MessageBox.Show(String.Format($"Bạn có chắc muốn xóa '{strName}' không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{

				try
				{
					if (strID > 0)
					{
						BillFilmDetailBO.Instance.Delete(strID);
					}
					grvData.DeleteSelectedRows();
				}
				catch
				{
					MessageBox.Show("Có lỗi xảy ra khi thực hiện thao tác, xin vui lòng thử lại sau.");
				}
			}
		}

		void loadStock()
		{
			DataTable dt = new DataTable();
			dt = TextUtils.Select("SELECT * FROM Stock ORDER BY ID");
			cboStock.DisplayMember = "StockName";
			cboStock.ValueMember = "ID";
			cboStock.DataSource = dt;
			if (dt.Rows.Count > 0)
				cboStock.SelectedIndex = 0;

		}
		void loadCode()
		{
			if (txtCode.Text.Trim() == "")
			{
				string code = TextUtils.ToString(TextUtils.ExcuteScalar("SELECT top 1 Code FROM BillFilm where TypeBill='True' ORDER BY ID DESC"));
				string[] arr = code.Split('.');
				if (arr.Count() < 2)
				{
					txtCode.Text = "PN.0000001";
					return;
				}
				string so = TextUtils.ToString(TextUtils.ToInt(arr[1]) + 1);
				for (int i = 0; so.Length < 7; i++)
				{
					so = TextUtils.ToString("0" + so);
				}

				txtCode.Text = "PN." + TextUtils.ToString(so);
			}
		}

		private void loadSupplier()
		{
			DataTable dt = new DataTable();
			dt = TextUtils.Select("SELECT * FROM Supplier");
			cboSupplier.Properties.DisplayMember = "SupplierName";
			cboSupplier.Properties.ValueMember = "ID";
			cboSupplier.Properties.DataSource = dt;
		}
		public void loadUsers()
		{
			DataTable dt = new DataTable();
			dt = TextUtils.Select("SELECT * FROM Users");
			cboUser.Properties.DisplayMember = "FullName";
			cboUser.Properties.ValueMember = "ID";
			cboUser.Properties.DataSource = dt;
		}
		private void loadCbName()
		{
			DataTable dt = TextUtils.Select("SELECT DISTINCT pf.ID, pf.Name,p.Name AS Name1 FROM ProductFilm pf LEFT JOIN dbo.ProductFilm p ON pf.ParentID= p.ID ");
			name.DataSource = dt;
			name.ValueMember = "ID";
			name.DisplayMember = "Name";

			colName.ColumnEdit = name;
		}
		void loadData()
		{
			DataTable dt = TextUtils.GetDataTableFromSP("spLoadDataGoodsReceivedNote", new string[] { "@BillID" }, new object[] { _BillF.ID });
			grdData.DataSource = dt;
			if (dt.Rows.Count == 0) return;
			txtCode.Text = TextUtils.ToString(dt.Rows[0]["Code"]);
			cboSupplier.EditValue = TextUtils.ToInt(dt.Rows[0]["SupplierID"]);
			cboUser.EditValue = TextUtils.ToInt(dt.Rows[0]["UserID"]);
			txtDateTime.Text = TextUtils.ToString(dt.Rows[0]["CreatDate"]);
			txtDescription.Text = TextUtils.ToString(dt.Rows[0]["Description"]);
		}
		bool saveData()
		{
			if (!ValidateForm())
				return false;
			_BillF.Code = txtCode.Text.Trim();
			_BillF.UserID = TextUtils.ToInt(cboUser.EditValue);
			_BillF.SupplierID = TextUtils.ToInt(cboSupplier.EditValue);
			_BillF.TypeBill = true;
			_BillF.StockID = TextUtils.ToInt(cboStock.SelectedValue);
			_BillF.Description = txtDescription.Text.Trim();
			_BillF.CreatDate = TextUtils.ToDate3(txtDateTime.Value);
			if (_BillF.ID > 0)
			{
				BillFilmBO.Instance.Update(_BillF);
			}
			else
			{
				_BillF.ID = (int)BillFilmBO.Instance.Insert(_BillF);
				_BillFilmDetail.BillID = _BillF.ID;
			}
			for (int i = 0; i < grvData.RowCount; i++)
			{
				_BillFilmDetail.ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
				_BillFilmDetail.BillID = _BillF.ID;
				_BillFilmDetail.ProductFimID = TextUtils.ToInt(grvData.GetRowCellValue(i, colName));
				_BillFilmDetail.Qty = TextUtils.ToInt(grvData.GetRowCellValue(i, colQty));
				if (_BillFilmDetail.ProductFimID == 0 && _BillFilmDetail.Qty == 0)
					continue;
				if (_BillFilmDetail.ID > 0)
				{

					BillFilmDetailBO.Instance.Update(_BillFilmDetail);
				}
				else
				{

					BillFilmDetailBO.Instance.Insert(_BillFilmDetail);
				}
			}
			return true;
		}
		private void btnSave_Click_1(object sender, EventArgs e)
		{
			if (saveData())
			{
				_BillFilmDetail = new BillFilmDetailModel();
				_BillF = new BillFilmModel();
				cboSupplier.Text = "";
				cboUser.Text = "";
				txtDescription.Text = "";
			}
			loadCode();
			loadData();
		}

		private bool ValidateForm()
		{
			if (txtCode.Text.Trim() == "")
			{
				MessageBox.Show("Xin hãy điền số phiếu.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return false;
			}
			else
			{
				DataTable dt;
				if (_BillF.ID > 0)
				{
					int strID = _BillF.ID;
					dt = TextUtils.Select("select top 1 ID from BillFilm where Code = '" + txtCode.Text.Trim() + "' and ID <> " + strID);
				}
				else
				{
					dt = TextUtils.Select("select top 1 ID from BillFilm where Code = '" + txtCode.Text.Trim() + "'");
				}
				if (dt != null)
				{
					if (dt.Rows.Count > 0)
					{
						MessageBox.Show("Số phiếu này đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
						return false;
					}
				}
			}
			if (cboSupplier.Text.Trim() == "")
			{
				MessageBox.Show("Xin hãy chọn nhà cung cấp.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return false;
			}
			if (cboUser.Text.Trim() == "")
			{
				MessageBox.Show("Xin hãy chọn người nhập.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return false;
			}

			return true;
		}

		private void frmGoodsReceivedNote_FormClosed(object sender, FormClosedEventArgs e)
		{
			DialogResult = DialogResult.OK;
		}
	}
}
