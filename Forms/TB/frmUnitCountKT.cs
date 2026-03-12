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
using System.CodeDom.Compiler;
using Forms;
using QRCoder;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace BMS
{
	public partial class frmUnitCountKT : _Forms
	{
		public frmUnitCountKT()
		{
			InitializeComponent();
		}

		private void frmUnitCount_Load(object sender, EventArgs e)
		{
			loadUnitCount();
		}

		private void loadUnitCount()
		{
			DataTable dt = TextUtils.Select($"SELECT * FROM dbo.UnitCountKT");
			grdData.DataSource = dt;
		}

        /// <summary>
        /// creat tool
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
		{
            UnitCountKTModel model = new UnitCountKTModel();
			frmUnitCountKTDetail frm = new frmUnitCountKTDetail();
            frm.unitCountKT = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
				loadUnitCount();
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
			UnitCountKTModel model = (UnitCountKTModel)UnitCountKTBO.Instance.FindByPK(ID);
			frmUnitCountKTDetail frm = new frmUnitCountKTDetail();
			frm.unitCountKT = model;
			if (frm.ShowDialog() == DialogResult.OK)
			{
				loadUnitCount();
			}
		}

		/// <summary>
		/// delete 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDel_Click(object sender, EventArgs e)
		{
			int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));                                                                                                                         
			string unitCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colUnitCode));
			if (ID == 0) return;

            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa : {0} không?", unitCode), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				UnitCountKTBO.Instance.Delete(ID);
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


