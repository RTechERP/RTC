using BMS.Business;
using BMS.Model;
using ExcelDataReader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BMS
{
	public partial class frmFirmBaseDetail : _Forms
	{
		public FirmBaseModel firmModel = new FirmBaseModel();

		public frmFirmBaseDetail()
		{
			InitializeComponent();
		}
		/// <summary>
		/// load dữ liệu lên khi load form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmFirmBaseDetail_Load(object sender, EventArgs e)
		{
			loadFirmDetail();
		}
        private void loadFirmDetail()
		{
			txtFirmCode.Text = firmModel.FirmCode;
			txtFirmName.Text = firmModel.FirmName;
		}

        private void btnSave_Click(object sender, EventArgs e)
		{
			SaveData();
			this.DialogResult = DialogResult.OK;

		}
		bool SaveData()
		{
			if (!ValidateForm()) return false;
			try
			{
				firmModel.FirmName = txtFirmName.Text.Trim();
				firmModel.FirmCode = txtFirmCode.Text.Trim();

				if (firmModel.ID > 0)
				{
					FirmBaseBO.Instance.Update(firmModel);
				}
				else
				{
					FirmBaseBO.Instance.Insert(firmModel);
				}
			}
			catch (Exception)
			{
				MessageBox.Show("Lỗi Update vị trí", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			return true;
		}

		private void btnSaveNew_Click(object sender, EventArgs e)
		{
			SaveData();
			txtFirmCode.Clear();
			txtFirmName.Clear();
			firmModel = new FirmBaseModel();
		}

        private void frmFirmDetail_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}

		/// <summary>
		/// check lỗi
		/// </summary>
		/// <returns></returns>
		/// 
		bool ValidateForm()
		{
			if (txtFirmName.Text == "" || txtFirmCode.Text == "")
			{
				MessageBox.Show("Cần nhập đầy đủ thông tin vào các ô còn trống!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return false;
			}
			DataTable dt;
			if (firmModel.ID > 0)
			{
				dt = TextUtils.Select("select top 1 FirmCode from FirmBase where FirmCode = '" + txtFirmCode.Text.Trim() + "' and ID <> " + firmModel.ID);
			}
			else
			{
				dt = TextUtils.Select("select top 1 FirmCode from FirmBase where FirmCode = '" + txtFirmCode.Text.Trim() + "'");

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
    }
}
