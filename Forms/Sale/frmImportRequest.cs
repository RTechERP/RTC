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
    public partial class frmImportRequest : _Forms
    {
        DataTable dttt = new DataTable();
        public frmImportRequest()
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
            dt = TextUtils.Select("SELECT po.*,s.SupplierName FROM dbo.PONCC as po Left JOIN  dbo.Supplier s on s.ID = po.SupplierID Order by ID desc");
            treeData.DataSource = dt;
        }

        void loadDataDetail()
        {
            if (treeData.AllNodesCount <= 0) return;
            int id = TextUtils.ToInt(treeData.FocusedNode.GetValue(colBillID));
            DataTable dt = TextUtils.LoadDataFromSP("spLoadPONCCDetail", "A"
                , new string[] { "@ID" }
                , new object[] { id });
            dttt = dt;
            DataColumn data = new DataColumn("Location", typeof(Byte[]));
            dt.Columns.Add(data);
            grdData.DataSource = dt;

        }

        private void treeData_FocusedNodeChanged_1(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (treeData.AllNodesCount <= 0) return;
            int GroupId = TextUtils.ToInt(treeData.FocusedNode.GetValue(colBillID));
            if (GroupId > 0)
            {
                loadDataDetail();
            }
        }
        /// <summary>
        /// click button tạo nhóm sản phẩm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewGroup_Click(object sender, EventArgs e)
        {
            frmPONCCDetail frm = new frmPONCCDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadDataDetail();
                loadData();
            }
        }
        public static int EditClick = 0;
        private void editDataGroup()
        {
            if (treeData.AllNodesCount <= 0) return;
            EditClick = 1;
            int id = TextUtils.ToInt(treeData.FocusedNode.GetValue(colBillID));
            if (id == 0) return;
            PONCCModel model = (PONCCModel)PONCCBO.Instance.FindByPK(id);
            frmPONCCDetail frm = new frmPONCCDetail();
            frm.oPONCC = model;
           
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
            if (treeData.AllNodesCount <= 0) return;
            bool isApproved = TextUtils.ToBoolean(treeData.FocusedNode.GetValue(colIsApproved));
            if (isApproved == true)
            {
                string billCode = TextUtils.ToString(treeData.FocusedNode.GetValue(colBillCode));
                MessageBox.Show(String.Format("Bạn không thể xóa phiếu [{0}] này? Xin vui lòng kiểm tra lại.", billCode), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Question);
                return;
            }
            if (treeData.AllNodesCount > 0)
            {
                int ID = TextUtils.ToInt(treeData.FocusedNode.GetValue(colBillID));
                string poCode = TextUtils.ToString(treeData.FocusedNode.GetValue(colPOCode));
                if (ID == 0) return;

                int strID = TextUtils.ToInt(treeData.FocusedNode.GetValue("ID"));

                DataTable dttt = new DataTable();
                dttt = TextUtils.LoadDataFromSP("spLoadPONCCDetail", "A", new string[] { "@ID" }, new object[] { ID });
                if (MessageBox.Show(string.Format("Bạn có muốn xóa phiếu [{0}] hay không ?", poCode), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    if (dttt.Rows.Count > 0)
                    {
                        PONCCBO.Instance.Delete(strID);
                        PONCCDetailBO.Instance.DeleteByAttribute("PONCCID", strID);
                        grvData.DeleteSelectedRows();
                    }
                    PONCCBO.Instance.Delete(ID);
                    treeData.DeleteNode(treeData.FocusedNode);
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
            if (treeData.AllNodesCount > 0)
            {
                PONCCModel _BillXuat = new PONCCModel();
                if (MessageBox.Show(string.Format("Bạn có chắc muốn {0} duyệt phiếu này?", isApproved ? "" : "bỏ"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                int groupID = TextUtils.ToInt(treeData.FocusedNode.GetValue(colBillID));
                string sql = string.Format("UPDATE dbo.PONCC SET IsApproved = {0} WHERE ID = {1}", isApproved ? 1 : 0, groupID);
                TextUtils.ExcuteSQL(sql);
                //soluongduyet();
                loadData();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //soluongduyet();
            approved(false);
        }
        private void grdData_Click(object sender, EventArgs e)
        {

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
                dt = TextUtils.LoadDataFromSP("spFindPONCC", "A", new string[] { "@Find" }, new object[] { _data });
                DataColumn data = new DataColumn("Location", typeof(Byte[]));
                dt.Columns.Add(data);
                treeData.DataSource = dt;
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
            if (treeData.AllNodesCount <= 0) return;
            bool isApproved = TextUtils.ToBoolean(treeData.FocusedNode.GetValue(colIsApproved));
            if (isApproved == true)
            {
                string codeBill = TextUtils.ToString(treeData.FocusedNode.GetValue(colBillCode));
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
    }
}


