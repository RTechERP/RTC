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
    public partial class frmProductLocation : _Forms
    {
        public frmProductLocation()
        {
            InitializeComponent();
        }

        private void frmLocation_Load(object sender, EventArgs e)
        {
            loadLocation();
        }

        void loadLocation()
        {
            int GroupId = TextUtils.ToInt(grdData.FocusedView);
            DataTable dt = TextUtils.LoadDataFromSP("spLoadLocation", "A", new string[] { "@GroupId" }, new object[] { @GroupId });
            grdData.DataSource = dt;
        }

        /// <summary>
        /// creat tool
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            LocationModel model = new LocationModel();
            frmProductLocationDetail frm = new frmProductLocationDetail();
            frm.oLocationModel = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadLocation();
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
        /// void edit data in productRTC
        /// </summary>
        public static int editGrv = 0;
        private void editDataProduct()
        {
            editGrv = 1;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            LocationModel model = (LocationModel)LocationBO.Instance.FindByPK(ID);
            frmProductLocationDetail frm = new frmProductLocationDetail();
            frm.oLocationModel = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadLocation();
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
            string Locationcode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colLocationCode));
            if (ID == 0) return;

            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa vị trí có mã: {1} không?", colLocationCode, Locationcode), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                LocationBO.Instance.Delete(ID);
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

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            frmProductLocationExcel frmExcel = new frmProductLocationExcel();
            if (frmExcel.ShowDialog() == DialogResult.OK)
            {
                loadLocation();
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            TextUtils.ExportExcel(grvData);
        }

        private void mnuMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}


