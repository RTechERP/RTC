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
	public partial class frmProjectTypeBaseDetail : _Forms
	{
		public ProjectTypeBaseModel projectTypeBaseModel = new ProjectTypeBaseModel();

		public frmProjectTypeBaseDetail()
		{
			InitializeComponent();
		}
		/// <summary>
		/// load dữ liệu lên khi load form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmProjectTypeBaseDetail_Load(object sender, EventArgs e)
		{
			loadProjectTypeBaseDetail();
		}
        private void loadProjectTypeBaseDetail()
		{
			txtProjectTypeCode.Text = projectTypeBaseModel.ProjectTypeCode;
			txtProjectTypeName.Text = projectTypeBaseModel.ProjectTypeName;
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
				projectTypeBaseModel.ProjectTypeCode = txtProjectTypeCode.Text.Trim();
				projectTypeBaseModel.ProjectTypeName = txtProjectTypeName.Text.Trim();

				if (projectTypeBaseModel.ID > 0)
				{
					ProjectTypeBaseBO.Instance.Update(projectTypeBaseModel);
				}
				else
				{
					ProjectTypeBaseBO.Instance.Insert(projectTypeBaseModel);
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
			txtProjectTypeCode.Clear();
			txtProjectTypeName.Clear();
			projectTypeBaseModel = new ProjectTypeBaseModel();
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
			if (txtProjectTypeName.Text == "" || txtProjectTypeCode.Text == "")
			{
				MessageBox.Show("Cần nhập đầy đủ thông tin vào các ô còn trống!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return false;
			}
			DataTable dt;
			if (projectTypeBaseModel.ID > 0)
			{
				dt = TextUtils.Select("select top 1 ProjectTypeCode from ProjectTypeBase where ProjectTypeCode = '" + txtProjectTypeCode.Text.Trim() + "' and ID <> " + projectTypeBaseModel.ID);
			}
			else
			{
				dt = TextUtils.Select("select top 1 ProjectTypeCode from ProjectTypeBase where ProjectTypeCode = '" + txtProjectTypeCode.Text.Trim() + "'");

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
