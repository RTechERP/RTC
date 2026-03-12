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
	public partial class frmUnitCountKTDetail : _Forms
	{
		public UnitCountKTModel unitCountKT = new UnitCountKTModel();

		public frmUnitCountKTDetail()
		{
			InitializeComponent();
		}
		/// <summary>
		/// load dữ liệu lên khi load form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmUnitCountDetail_Load(object sender, EventArgs e)
		{
			loadUnitCountDetail();
		}

        private void loadUnitCountDetail()
		{
			txtUnitCountCode.Text = unitCountKT.UnitCountCode;
			txtUnitCountName.Text = unitCountKT.UnitCountName;
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
				unitCountKT.UnitCountName = txtUnitCountName.Text.Trim();
				unitCountKT.UnitCountCode = txtUnitCountCode.Text.Trim();

				if (unitCountKT.ID > 0)
				{
					UnitCountKTBO.Instance.Update(unitCountKT);
				}
				else
				{
					UnitCountKTBO.Instance.Insert(unitCountKT);
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
			txtUnitCountCode.Clear();
			txtUnitCountName.Clear();
			unitCountKT = new UnitCountKTModel();
		}

        private void frmUnitCountDetail_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveData();
			this.DialogResult = DialogResult.OK;
		}

		/// <summary>
		/// check lỗi
		/// </summary>
		/// <returns></returns>
		/// 
		bool ValidateForm()
		{
			if (txtUnitCountName.Text == "" || txtUnitCountCode.Text == "")
			{
				MessageBox.Show("Cần nhập đầy đủ thông tin vào các ô còn trống!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return false;
			}
			DataTable dt;
			if (unitCountKT.ID > 0)
			{
				dt = TextUtils.Select("select top 1 UnitCode from UnitCountKT where UnitCode = '" + txtUnitCountCode.Text.Trim() + "' and ID <> " + unitCountKT.ID);
			}
			else
			{
				dt = TextUtils.Select("select top 1 UnitCode from UnitCountKT where UnitCode = '" + txtUnitCountCode.Text.Trim() + "'");

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
