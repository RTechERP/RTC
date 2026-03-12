using BMS.Business;
using BMS.Model;
using Forms.Classes;
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
	public partial class frmProductHistoryReturnDetail : _Forms
	{
		DataTable dtAll = new DataTable();

		public bool isLogout = false;
		int warehouseID;
		public frmProductHistoryReturnDetail()
		{
			InitializeComponent();
		}
		public frmProductHistoryReturnDetail(int WarehouseID)
		{
			InitializeComponent();
			warehouseID = WarehouseID;
		}

		private void frmProductHistoryReturnDetail_Load(object sender, EventArgs e)
		{
			loadData();
			loadCB();
			cbUser.EditValue = Global.UserID;
		}

		void loadData()
		{
			txtQrCode.SelectAll();
			txtQrCode.Focus();
			dtAll = TextUtils.LoadDataFromSP(StoreProcedures.spGetProductQrCode, "A", new string[] { }, new object[] { });//spGetProductQrCode
			grdData.DataSource = dtAll;

		}
		void loadCB()
		{
			DataTable dt = TextUtils.Select("Select ID,FullName,Code from Users");
			cbUser.Properties.DataSource = dt;
			cbUser.Properties.DisplayMember = "FullName";
			cbUser.Properties.ValueMember = "ID";

		}
		List<string> Listqrcode = new List<string>();

		void loadQrCode()

		{
			if (txtQrCode.Text.Trim().ToLower() == "ok")
			{
				btnSave_Click(null, null);
				return;
			}

			if (txtQrCode.Text.Trim().ToLower() == "logout")
			{
				isLogout = true;
			}

			DataTable dt = TextUtils.LoadDataFromSP("spGetProductRTCByQrCode_Return", "A",
				new string[] { "@ProductRTCQRCode", "@PeopleID", "@WarehouseID" },
				new object[] { txtQrCode.Text.Trim(), TextUtils.ToInt(cbUser.EditValue), warehouseID });
			if (dt.Rows.Count == 0 || dt == null)
			{
				//txtQrCode.SelectAll();
				return;
			}
			grvData.BeginDataUpdate();
			DataRow dr = dtAll.NewRow();
			dr.BeginEdit();
			dr["ID"] = dt.Rows[0]["ID"];//ID của ProductRTCQRCode
			dr["ProductRTCID"] = dt.Rows[0]["ProductRTCID"];
			dr["ProductQRCode"] = dt.Rows[0]["ProductQRCode"];
			dr["ProductCode"] = dt.Rows[0]["ProductCode"];
			dr["ProductName"] = dt.Rows[0]["ProductName"];
			dr["ProductCodeRTC"] = dt.Rows[0]["ProductCodeRTC"];
			dr["AddressBox"] = dt.Rows[0]["AddressBox"];
			dr["Note"] = dt.Rows[0]["Note"];
			dr["Soluong"] = 1;
			dr["HistoryProductRTCID"] = dt.Rows[0]["HistoryProductRTCID"];
			dr.EndEdit();
			if (Listqrcode.Contains(TextUtils.ToString(dt.Rows[0]["ProductQRCode"])))
			{
				grdData.DataSource = dtAll;
				grvData.EndDataUpdate();

			}
			else
			{
				dtAll.Rows.Add(dr);
				grdData.DataSource = dtAll;
				grvData.EndDataUpdate();
			}
			Listqrcode.Add(TextUtils.ToString(dt.Rows[0]["ProductQRCode"]).Trim());

			txtQrCode.SelectAll();
			//txtQrCode.Clear();
		}
		private void txtQrCode_TextChanged(object sender, EventArgs e)
		{
			loadQrCode();
		}

		bool Save()
		{
			//int PeopleID =TextUtils.ToInt(cbUser.EditValue);
			for (int i = 0; i < grvData.RowCount; i++)
			{
				//int ProductRTCQRCodeID = TextUtils.ToInt(grvData.GetRowCellValue(i,colId));
				//  int HistoryProductRTCID =TextUtils.ToInt(TextUtils.ExcuteScalar($"spFindHistoryProductID {PeopleID},{ProductRTCQRCodeID}"));
				int HistoryProductRTCID = TextUtils.ToInt(grvData.GetRowCellValue(i, colHistoryProductRTCID));


				//Update status của bảng HistoryProductRTC status=4 đăng kí trả.
				HistoryProductRTCModel historyProductRTCModel = (HistoryProductRTCModel)HistoryProductRTCBO.Instance.FindByPK(HistoryProductRTCID);
				historyProductRTCModel.Status = 4;
				historyProductRTCModel.WarehouseID = warehouseID;
				HistoryProductRTCBO.Instance.Update(historyProductRTCModel);
			}

			return true;
		}
		private void btnSave_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Bạn có chắc chắn muốn Đăng kí trả thiết bị không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
			{
				if (Save())
				{
					this.DialogResult = DialogResult.OK;

				}
			}

		}

		private void txtQrCode_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				loadQrCode();
			}
		}

		private void frmProductHistoryReturnDetail_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 27)
			{
				this.Close();
			}
		}


	}
}
