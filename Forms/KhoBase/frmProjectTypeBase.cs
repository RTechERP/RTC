using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BMS
{
	public partial class frmProjectTypeBase : _Forms
	{
		public frmProjectTypeBase()
		{
			InitializeComponent();
		}

		private void frmProjectTypeBase_Load(object sender, EventArgs e)
		{
			loadData();
		}
		
		private void loadData()
		{
			DataTable dt = TextUtils.Select($"SELECT * FROM dbo.ProjectTypeBase");
			grdData.DataSource = dt;
		}

        /// <summary>
        /// cliick add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
		{
			ProjectTypeBaseModel model = new ProjectTypeBaseModel();
			frmProjectTypeBaseDetail frm = new frmProjectTypeBaseDetail();
            frm.projectTypeBaseModel = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
				loadData();
            }
        }
		/// <summary>
		/// fix tool
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnEdit_Click(object sender, EventArgs e)
		{
			editDataProduct();
		}
		/// <summary>
		/// void edit data
		/// </summary>
		private void editDataProduct()
		{
			int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
			if (ID == 0) return;
			ProjectTypeBaseModel model = (ProjectTypeBaseModel)ProjectTypeBaseBO.Instance.FindByPK(ID);
			frmProjectTypeBaseDetail frm = new frmProjectTypeBaseDetail();
			frm.projectTypeBaseModel = model;
			if (frm.ShowDialog() == DialogResult.OK)
			{
				loadData();
			}
		}
		/// <summary>
		/// delete sản phẩm khỏi danh sách
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDel_Click(object sender, EventArgs e)
		{
			int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
			string firmCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProjectTypeCode));
			if (ID == 0) return;

            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa có mã: {0} không?", firmCode), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				ProjectTypeBaseBO.Instance.Delete(ID);
				grvData.DeleteSelectedRows();
			}
		} 
		/// <summary>
		/// event editData by doubleClick
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void grdData_DoubleClick(object sender, EventArgs e)
		{
			editDataProduct();
		}
    }
}


