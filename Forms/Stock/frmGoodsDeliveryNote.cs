using BMS.Business;
using BMS.Model;
using BMS.Utils;
//using MSScriptControl;
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
	public partial class frmGoodsDeliveryNote : _Forms
	{
		public BillFilmDetailModel _BillFilmDetail = new BillFilmDetailModel();
		public BillFilmDetailModel _BillFilmDetailPN = new BillFilmDetailModel();
		public BillFilmModel _BillFilm = new BillFilmModel();
		public BillFilmModel _BillFilmPN = new BillFilmModel();
		DataTable dtt = new DataTable();
		decimal NumberBox;
		decimal _AreaCon;
		decimal _AreaCha;
		decimal _PcsPerBoxCon;
		string _Code = "";
		public frmGoodsDeliveryNote()
		{
			InitializeComponent();
		}
		private void frmQuotationDetail_Load(object sender, EventArgs e)
		{
			loadCustomer();
			loadUsers();
			loadData();
			loadCbName();
			loadCode();
			Delete.Click += Delete_Click; ;
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

		void loadCode()
		{
			if (txtCode.Text.Trim() == "")
			{
				string code = TextUtils.ToString(TextUtils.ExcuteScalar("SELECT top 1 Code FROM BillFilm where TypeBill='False' ORDER BY ID DESC"));
				string[] arr = code.Split('.');
				if (arr.Count() < 2)
				{
					txtCode.Text = "PX.0000001";
					return;
				}
				string so = TextUtils.ToString(TextUtils.ToInt(arr[1]) + 1);
				for (int i = 0; so.Length < 7; i++)
				{
					so = TextUtils.ToString("0" + so);
				}

				txtCode.Text = "PX." + TextUtils.ToString(so);
			}
		}

		private void loadCustomer()
		{
			DataTable dt = new DataTable();
			dt = TextUtils.Select("SELECT * FROM Customer where IsDeleted <> 1");
			cboCustomer.Properties.DisplayMember = "CustomerName";
			cboCustomer.Properties.ValueMember = "ID";
			cboCustomer.Properties.DataSource = dt;
		}
		public void loadUsers()
		{
			DataTable dt = new DataTable();
			dt = TextUtils.Select("SELECT * FROM Users");
			cboUser.Properties.DisplayMember = "FullName";
			cboUser.Properties.ValueMember = "ID";
			cboUser.Properties.DataSource = dt;
		}

		private void loadData()
		{
			DataTable dt = TextUtils.GetDataTableFromSP("spLoadDataGoodsDeliveryNote", new string[] { "@BillID" }, new object[] { _BillFilm.ID });
			grdData.DataSource = dt;
			if (dt.Rows.Count == 0) return;
			txtCode.Text = TextUtils.ToString(dt.Rows[0]["Code"]);
			cboCustomer.EditValue = TextUtils.ToInt(dt.Rows[0]["CustomerID"]);
			cboUser.EditValue = TextUtils.ToInt(dt.Rows[0]["UserID"]);
			txtDateTime.Text = TextUtils.ToString(dt.Rows[0]["CreatDate"]);
		}
		private void loadCbName()
		{
			dtt = TextUtils.Select("SELECT DISTINCT pf.ID, pf.Name,pf.Area AS AreaCon,pf.PcsPerBox AS PcsPerBoxCon,p.Name AS Name1, p.Area AS Areacha,p.PcsPerBox AS PcsPerBoxCha FROM ProductFilm pf LEFT JOIN dbo.ProductFilm p ON pf.ParentID= p.ID ");
			cbName1.DataSource = dtt;
			cbName1.ValueMember = "ID";
			cbName1.DisplayMember = "Name";

			colName.ColumnEdit = cbName1;
		}
		void loadCodePN()
		{
			if (_Code == "")
			{
				string code = TextUtils.ToString(TextUtils.ExcuteScalar("SELECT top 1 Code FROM BillFilm where TypeBill='True' ORDER BY ID DESC"));
				string[] arr = code.Split('.');
				if (arr.Count() < 2)
				{
					_Code = "PN.0000001";
					return;
				}
				string so = TextUtils.ToString(TextUtils.ToInt(arr[1]) + 1);
				for (int i = 0; so.Length < 7; i++)
				{
					so = TextUtils.ToString("0" + so);
				}

				_Code = "PN." + TextUtils.ToString(so);
			}

		}
		bool saveData()
		{
			if (!ValidateForm())
				return false;
			_BillFilm.TypeBill = false;
			_BillFilm.Code = txtCode.Text.Trim();
			_BillFilm.CustomerID = TextUtils.ToInt(cboCustomer.EditValue);
			_BillFilm.UserID = TextUtils.ToInt(cboUser.EditValue);
			_BillFilm.CreatDate = TextUtils.ToDate3(txtDateTime.Value);
			if (_BillFilm.ID > 0)
			{
				BillFilmBO.Instance.Update(_BillFilm);
			}
			else
			{
				_BillFilm.ID = (int)BillFilmBO.Instance.Insert(_BillFilm);
			}
			for (int i = 0; i < grvData.RowCount; i++)
			{
				_BillFilmDetail.ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
				_BillFilmDetail.BillID = _BillFilm.ID;
				_BillFilmDetailPN.ProductFimID = _BillFilmDetail.ProductFimID = TextUtils.ToInt(grvData.GetRowCellValue(i, colName));
				_BillFilmDetail.Qty = TextUtils.ToInt(grvData.GetRowCellValue(i, colQty));
				_BillFilmDetail.NeededBigBoxQty = TextUtils.ToInt(grvData.GetRowCellValue(i, colNeededBigBoxQty));
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

				//Sinh ra phiếu nhập khi con hàng còn thừa
				_BillFilmDetailPN.Qty = TextUtils.ToInt(grvData.GetRowCellValue(i, colQtyCon));
				if (_BillFilmDetailPN.Qty > 0)
				{
					loadCodePN();
					InsertPN();
					BillFilmDetailBO.Instance.Insert(_BillFilmDetailPN);
					_BillFilmDetailPN.Qty = 0;
				}
			}
			_Code = "";
			_BillFilmPN.ID = 0;
			return true;
		}

		void InsertPN()
		{
			if (_BillFilmPN.ID == 0)
			{
				_BillFilmPN.Code = _Code.Trim();
				_BillFilmPN.UserID = TextUtils.ToInt(cboUser.EditValue);
				_BillFilmPN.TypeBill = true;
				_BillFilmPN.CreatDate = DateTime.Today;
				_BillFilmPN.ID = (int)BillFilmBO.Instance.Insert(_BillFilmPN);
				_BillFilmDetailPN.BillID = _BillFilmPN.ID;
			}
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
				if (_BillFilm.ID > 0)
				{
					int strID = _BillFilm.ID;
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
			if (cboCustomer.Text.Trim() == "")
			{
				MessageBox.Show("Xin hãy chọn khách hàng.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return false;
			}
			if (cboUser.Text.Trim() == "")
			{
				MessageBox.Show("Xin hãy chọn người nhập.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return false;
			}
			return true;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (saveData())
			{
				_BillFilm = new BillFilmModel();
				cboCustomer.Text = "";
				cboUser.Text = "";
			}
			loadCode();
			loadData();
		}

		private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
			int id = Lib.ToInt(grvData.GetFocusedRowCellValue(colName));
			if (id == 0) return;
			DataTable dt = dtt.Select($"ID={id}").CopyToDataTable();
			_AreaCon = TextUtils.ToDecimal(dt.Rows[0]["AreaCon"]);
			_AreaCha = TextUtils.ToDecimal(dt.Rows[0]["Areacha"]);
			_PcsPerBoxCon = TextUtils.ToDecimal(dt.Rows[0]["PcsPerBoxCon"]);
			if (e.Column == colQty)
			{
				NumberBox = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colQty));
				if (_AreaCha == 0) return;
				decimal valueFormula = NumberBox / (_AreaCha / _AreaCon / _PcsPerBoxCon);
				decimal formula = Math.Round(Convert.ToDecimal(valueFormula), 0);
				grvData.SetFocusedRowCellValue(colNeededBigBoxQty, formula);
				_AreaCha = 0;
			}
		}

		private void frmGoodsDeliveryNote_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}
	}
}
