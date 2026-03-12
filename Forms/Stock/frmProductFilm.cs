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
    public partial class frmProductFilm : _Forms
    {
        public frmProductFilm()
        {
            InitializeComponent();
            LoadTree();

        }
        bool isParent(int id)
        {
            try
            {
                DataTable tbl = TextUtils.Select(@"select * from dbo.ProductFilm a WITH(NOLOCK) where ParentID = " + id + " ORDER BY Name");
                if (tbl.Rows.Count > 0)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        //void loadTree()
        //{
        //    try
        //    {
        //        DataTable tbl = TextUtils.Select("Select ID,Name,ParentID from FormAndFunctionGroup with(nolock) order by Name");

        //        DataRow row = tbl.NewRow();
        //        row["ID"] = 0;
        //        row["Name"] = "--Tất cả các nhóm--";
        //        tbl.Rows.InsertAt(row, 0);

        //        treeData.DataSource = tbl;
        //        treeData.KeyFieldName = "ID";
        //        treeData.PreviewFieldName = "Name";
        //        treeData.ParentFieldName = "ParentID";

        //        treeData.ExpandAll();
               
        //        DevExpress.XtraTreeList.Nodes.TreeListNode currentNode = treeData.FindNodeByFieldValue("ID", _curentNode);
        //        treeData.SetFocusedNode(currentNode);
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show(ex.ToString());
        //    }
        //}

        private void frmPart_Load(object sender, EventArgs e)
        {
            //loadData();
            

            grdData.Columns[8].Caption = "Số Lượng " + Environment.NewLine + "Tồn Kho";
            LoadTree();

            
        }

        private void LoadTree()
        {
            DataTable dt = new DataTable();
            dt = TextUtils.LoadDataFromSP("spLoadProductFilm", "A"
                , new string[] { "@Find" }
                , new object[] { txtFilterText.Text.Trim() });
            grdData.DataSource = dt;
        }

        #region Methods
        

        
        
        #endregion

        #region Buttons Events
        private void btnNew_Click(object sender, EventArgs e)
        {
            frmFilmProductsDetails frm = new frmFilmProductsDetails();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadTree();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int id = TextUtils.ToInt(grdData.FocusedNode.GetValue(colID));
                if (id == 0) return;
                ProductFilmModel model = (ProductFilmModel)ProductFilmBO.Instance.FindByPK(id);
                frmFilmProductsDetails frm = new frmFilmProductsDetails();
                frm.ProductFilm = model;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadTree();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grdData.DataSource==null)
                return;
            int strID = TextUtils.ToInt(grdData.FocusedNode.GetValue(colID));

            string strName = TextUtils.ToString(grdData.FocusedNode.GetValue("Name"));

            if (RequestPriceDetailBO.Instance.CheckExist("Name", strID))
            {
                MessageBox.Show("Sản phẩm này đã phát sinh ở các nghiệp vụ khác nên không thể xóa được!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (MessageBox.Show(String.Format($"Bạn có chắc muốn xóa '{0}' không?", strName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    ProductFilmBO.Instance.Delete(strID);
                    grdData.DeleteSelectedNodes();
                }
                catch
                {
                    MessageBox.Show("Có lỗi xảy ra khi thực hiện thao tác, xin vui lòng thử lại sau.");
                }
            }
        }

        #endregion

        //private void grdData_DoubleClick(object sender, EventArgs e)
        //{
        //    if (grvData.RowCount > 0 && btnEdit.Enabled == true)
        //        btnEdit_Click(null, null);
        //}

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadTree();
        }

		private void btnStock_Click(object sender, EventArgs e)
		{
            frmStock frm = new frmStock();
            frm.Show();
		}

		private void btnStockLocation_Click(object sender, EventArgs e)
		{
            frmStockLocation frm = new frmStockLocation();
            frm.Show();
		}
	}
}
