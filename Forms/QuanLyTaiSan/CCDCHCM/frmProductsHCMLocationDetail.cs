using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMS;
using BMS.Business;
using BMS.Model;

namespace BMS
{
	public partial class frmProductsHCMLocationDetail : _Forms
	{
		public ProductLocationModel location = new ProductLocationModel();
		public int warehouseID;
		public frmProductsHCMLocationDetail(int WarehouseID)
		{
			InitializeComponent();
			warehouseID = WarehouseID;
		}

		private void frmProductsHCMLocationDetail_Load(object sender, EventArgs e)
		{
			if (location.ID > 0)
			{
				txtLocationCode.Text = location.LocationCode;
				txtlocationName.Text = location.LocationName;
				txtSTT.Value = TextUtils.ToInt(location.STT);
			}
			else
			{
				var listLocations = SQLHelper<ProductLocationModel>.FindByAttribute(ProductLocationModel_Enum.WarehouseID.ToString(), warehouseID);
				txtSTT.Value = TextUtils.ToInt(listLocations.Max(x => x.STT)) + 1;
			}
		}
		bool ValidateForm()
		{
			if (txtLocationCode.Text.Trim() == "" || txtlocationName.Text.Trim() == "")
			{
				MessageBox.Show("Cần nhập đầy đủ thông tin vào các ô còn trống!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return false;
			}
			DataTable dt;
			if (location.ID > 0)
			{
				dt = TextUtils.Select("select top 1 ID from ProductLocation where LocationCode = '" + txtLocationCode.Text.Trim() + "' and ID <> " + location.ID + $" AND WarehouseID = {warehouseID}");
			}
			else
			{
				dt = TextUtils.Select("select top 1 ID from ProductLocation where LocationCode = '" + txtLocationCode.Text.Trim() + "'" + $" AND WarehouseID = {warehouseID}");
			}
			if (dt != null)
			{
				if (dt.Rows.Count > 0)
				{
					MessageBox.Show("Mã đã tồn tại, vui lòng kiểm tra lại", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
					return false;
				}
			}
			return true;
		}

		bool save()
		{
			if (!ValidateForm()) return false;

			location.LocationCode = txtLocationCode.Text.Trim();
			location.LocationName = txtlocationName.Text.Trim();
			location.WarehouseID = warehouseID;
			location.STT = TextUtils.ToInt(txtSTT.Value);
			if (location.ID > 0)
			{
				SQLHelper<ProductLocationModel>.Update(location);
			}
			else
			{
				SQLHelper<ProductLocationModel>.Insert(location);
			}

			return true;

		}

		private void btnAddAndClose_Click(object sender, EventArgs e)
		{
			if (!save()) return;
			this.DialogResult = DialogResult.OK;
		}

		private void btnAddAndNew_Click(object sender, EventArgs e)
		{
			if (!save()) return;
			txtLocationCode.Clear();
			txtlocationName.Clear();
			var listLocations = SQLHelper<ProductLocationModel>.FindByAttribute(ProductLocationModel_Enum.WarehouseID.ToString(), warehouseID);
			txtSTT.Value = TextUtils.ToInt(listLocations.Max(x => x.STT)) + 1;
		}

		private void frmProductsHCMLocationDetail_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}
	}
}
