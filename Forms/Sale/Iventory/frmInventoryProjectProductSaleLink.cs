using BMS;
using BMS.Business;
using BMS.Model;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmInventoryProjectProductSaleLink : _Forms
    {
        public List<InventoryProjectProductSaleLinkModel> list = new List<InventoryProjectProductSaleLinkModel>();
        public frmInventoryProjectProductSaleLink()
        {
            InitializeComponent();
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }

        private void InventoryProjectProductSaleLink_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadProductGroups();
            grvData.ExpandAllGroups();
        }

        void LoadData()
        {
            int productGroupID = cboProductGroup.EditValue != null ? Convert.ToInt32(cboProductGroup.EditValue) : 0;
            string keyWord = txtFilterText.Text.Trim();
            int productSaleID = 0;
            int employeeID = 0;

            DataTable dt = TextUtils.LoadDataFromSP(
                "spGetInventoryProjectProductSaleLink",
                "A",
                new string[] { "@ProductSaleID", "@EmployeeID", "@Keyword", "@ProductGroupID" },
                new object[] { productSaleID, employeeID, keyWord, productGroupID });

            DataView dv = new DataView(dt);
            dv.RowFilter = "IsDeleted = 0 OR IsDeleted IS NULL";
            grdData.DataSource = dv;
        }
        private void LoadProductGroups()
        {
            List<ProductGroupModel> list = SQLHelper<ProductGroupModel>.FindAll().OrderByDescending(x => x.ID).ToList();
            cboProductGroup.Properties.DisplayMember = "ProductGroupName";
            cboProductGroup.Properties.ValueMember = "ID";
            cboProductGroup.Properties.DataSource = list;
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtFiltertext_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadData();
            }
        }

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int[] selectedRows = grvData.GetSelectedRows();
            if (selectedRows.Length == 0)
            {
                MessageBox.Show("Bạn chưa chọn bản ghi cần xóa!",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa các vật tư đã chọn không?",
                                "Xác nhận",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (int rowHandle in selectedRows)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(rowHandle, "ID"));
                    if (id > 0)
                    {
                        var model = SQLHelper<InventoryProjectProductSaleLinkModel>.FindByID(id);
                        if (model != null && model.ID > 0)
                        {
                            model.IsDeleted = true;
                            SQLHelper<InventoryProjectProductSaleLinkModel>.Update(model);
                        }
                    }
                }

                LoadData();
            }
        }
        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmInventoryProjectProductSaleLinkDetail frm = new frmInventoryProjectProductSaleLinkDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                List<int> selectedIds = frm.SelectedIds; 

                foreach (int id in selectedIds)
                {
                    var product = SQLHelper<ProductSaleModel>.FindByID(id);
                    if (product == null) continue;

                    InventoryProjectProductSaleLinkModel model = new InventoryProjectProductSaleLinkModel
                    {
                        ProductSaleID = product.ID,   
                        //IsDeleted = false,
                        //CreatedDate = DateTime.Now,
                        //CreatedBy = Global.AppUserName,
                        EmployeeID = Global.EmployeeID,
                    };

                    SQLHelper<InventoryProjectProductSaleLinkModel>.Insert(model);
                }

                LoadData();
            }
        }
    }
}