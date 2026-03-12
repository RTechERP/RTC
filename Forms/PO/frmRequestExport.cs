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
using DevExpress.XtraEditors.Controls;
using Excel = Microsoft.Office.Interop.Excel;

namespace BMS
{
    public partial class frmRequestExport : _Forms
    {
        public frmRequestExport()
        {
            InitializeComponent();
        }

        private void frmListTool_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            DataTable dt = new DataTable();
            dt = TextUtils.Select("SELECT * FROM [dbo].[RequestExport] Order by ID desc");
            grdMaster.DataSource = dt;
        }

        void loadDataDetail()
        {
            if (grvMaster.RowCount <= 0) return;
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            DataTable dt = TextUtils.LoadDataFromSP("spLoadRequestExportDetail", "A"
                , new string[] { "@ID" }
                , new object[] { id });
            grdData.DataSource = dt;
        }

        /// <summary>
        /// click button tạo nhóm sản phẩm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewGroup_Click(object sender, EventArgs e)
        {
            frmRequestExportDetail frm = new frmRequestExportDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadDataDetail();
                loadData();
            }
        }
        public static int EditClick = 0;
        private void editDataGroup()
        {
            if (grvMaster.RowCount <= 0) return;
            EditClick = 1;
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (id == 0) return;
            RequestExportModel model = (RequestExportModel)RequestExportBO.Instance.FindByPK(id);
            frmRequestExportDetail frm = new frmRequestExportDetail();
            frm.oRequestExport = model;
            frm.GroupId = id;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
                loadDataDetail();
            }
        }

        private void btnEditGroup_Click(object sender, EventArgs e)
        {
            editDataGroup();
        }
        private void btnDeleteGroup_Click(object sender, EventArgs e)
        {
            if (grvMaster.RowCount <= 0) return;
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproved));
            if (isApproved == true)
            {
                string billCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colRequestCode));
                MessageBox.Show(String.Format("Bạn không thể xóa phiếu [{0}] này? Xin vui lòng kiểm tra lại.", billCode), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Question);
                return;
            }
            if (grvMaster.RowCount > 0)
            {
                int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
                string poCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colRequestCode));
                if (ID == 0) return;

                int strID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue("ID"));

                DataTable dttt = new DataTable();
                dttt = TextUtils.LoadDataFromSP("spLoadRequestExportDetail", "A", new string[] { "@ID" }, new object[] { ID });
                if (MessageBox.Show(string.Format("Bạn có muốn xóa phiếu [{0}] hay không ?", poCode), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    RequestExportBO.Instance.Delete(strID);
                    RequestExportDetailBO.Instance.DeleteByAttribute("RequestID", strID);
                    grvMaster.DeleteSelectedRows();
                }
            }
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnDeleteGroup_Click(null, null);
        }
        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {

        }

        private void cbStatus_TextChanged(object sender, EventArgs e)
        {
            loadData();
        }


        void approved(bool isApproved)
        {
            if (grvMaster.RowCount > 0)
            {
                RequestExportModel quoteNCC = new RequestExportModel();
                if (MessageBox.Show(string.Format("Bạn có chắc muốn {0} duyệt phiếu này?", isApproved ? "" : "bỏ"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                int groupID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
                string sql = string.Format("UPDATE dbo.RequestExport SET IsApproved = {0} WHERE ID = {1}", isApproved ? 1 : 0, groupID);
                TextUtils.ExcuteSQL(sql);
                loadData();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            approved(false);
        }
        private void treeData_DoubleClick(object sender, EventArgs e)
        {
            editDataGroup();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            findData();
        }
        /// <summary>
		/// hàm tìm kiếm sản phẩm
		/// </summary>
		private void findData()
        {
            string _data = TextUtils.ToString(txtFilterText.Text.Trim());
            if (_data == "")
            {
                loadData();
            }
            else
            {
                DataTable dt = new DataTable();
                dt = TextUtils.LoadDataFromSP("spFindRequestExport", "A", new string[] { "@Find" }, new object[] { _data });
                DataColumn data = new DataColumn("Location", typeof(Byte[]));
                dt.Columns.Add(data);
                grdMaster.DataSource = dt;
            }
        }

        private void txtFilterText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                findData();
            }
        }

        private void btnIsApproved_Click(object sender, EventArgs e)
        {
            if (grvMaster.RowCount <= 0) return;
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproved));
            if (isApproved == true)
            {
                string codeBill = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colRequestCode));
                MessageBox.Show(String.Format("Sản phẩm này đã được duyệt [{0}]. Xin vui lòng kiểm tra lại!", codeBill), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Question);
                return;
            }
            approved(true);
        }

        private void btnHuyDuyet_Click(object sender, EventArgs e)
        {
            approved(false);
        }

        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (grvMaster.RowCount <= 0) return;
            int GroupId = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (GroupId > 0)
            {
                loadDataDetail();
            }
        }
    }
}


