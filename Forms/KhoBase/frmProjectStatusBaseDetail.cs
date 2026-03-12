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
	public partial class frmProjectStatusBaseDetail : _Forms
	{
		public ProjectStatusBaseModel projectStatusModel = new ProjectStatusBaseModel();

		public frmProjectStatusBaseDetail()
		{
			InitializeComponent();
		}
		/// <summary>
		/// load dữ liệu lên khi load form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmProjectStatusBaseDetail_Load(object sender, EventArgs e)
		{
			loadFirmDetail();
		}
        private void loadFirmDetail()
		{
			txtProjectStatusCode.Text = projectStatusModel.ProjectStatusCode;
			txtProjectStatusName.Text = projectStatusModel.ProjectStatusName;
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
				projectStatusModel.ProjectStatusCode = txtProjectStatusCode.Text.Trim();
				projectStatusModel.ProjectStatusName = txtProjectStatusName.Text.Trim();

				if (projectStatusModel.ID > 0)
				{
					FirmBaseBO.Instance.Update(projectStatusModel);
				}
				else
				{
					FirmBaseBO.Instance.Insert(projectStatusModel);
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
			txtProjectStatusCode.Clear();
			txtProjectStatusName.Clear();
			projectStatusModel = new ProjectStatusBaseModel();
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
			if (txtProjectStatusName.Text == "" || txtProjectStatusCode.Text == "")
			{
				MessageBox.Show("Cần nhập đầy đủ thông tin vào các ô còn trống!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return false;
			}
			DataTable dt;
			if (projectStatusModel.ID > 0)
			{
				dt = TextUtils.Select("select top 1 ProjectStatusCode from ProjectStatusBase where ProjectStatusCode = '" + txtProjectStatusCode.Text.Trim() + "' and ID <> " + projectStatusModel.ID);
			}
			else
			{
				dt = TextUtils.Select("select top 1 ProjectStatusCode from ProjectStatusBase where ProjectStatusCode = '" + txtProjectStatusCode.Text.Trim() + "'");

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

        private void labelControl3_Click(object sender, EventArgs e)
        {

        }
    }
}
