using BMS.Model;
using BMS.Utils;
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
    public partial class frmProductRTCProtectiveGear : _Forms
    {
        int warehouseID = 5;
        public frmProductRTCProtectiveGear()
        {
            InitializeComponent();
        }

        private void frmProductRTCProtectiveGear_Load(object sender, EventArgs e)
        {
            LoadProductGroup();
            LoadData();
        }

        private void LoadData()
        {
            int productGroupID = TextUtils.ToInt(grvDataGroup.GetFocusedRowCellValue("ID"));

            DataTable dt = TextUtils.LoadDataFromSP("spGetProductRTC", "A",
                                new string[] { "@ProductGroupID", "@Keyword", "@CheckAll", "@WarehouseID" },
                                new object[] { productGroupID, txtKeyword.Text.Trim(), 1, warehouseID });

            grdData.DataSource = dt;
        }

        void LoadProductGroup()
        {
            var exp1 = new Expression("WarehouseID", 1);
            var exp2 = new Expression("ProductGroupNo", "DBH","like");
            var list = SQLHelper<ProductGroupRTCModel>.FindByExpression(exp1.And(exp2)).ToList();
            grdDataGroup.DataSource = list;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmProductRTCProtectiveGearDetails frm = new frmProductRTCProtectiveGearDetails(warehouseID);
            frm.productGroupRTCID = TextUtils.ToInt(grvDataGroup.GetFocusedRowCellValue("ID"));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.totalRecords.Count > 0) LoadData();
            }
        }

        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            if (id <= 0) return;
            ProductRTCModel product = SQLHelper<ProductRTCModel>.FindByID(id);
            frmProductRTCProtectiveGearDetails frm = new frmProductRTCProtectiveGearDetails(warehouseID);
            frm.product = product;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.totalRecords.Count > 0)
                {

                    LoadData();
                    grvData.FocusedRowHandle = rowHandle;
                }
            }
        }

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            if (id <= 0) return;
            string productCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductCode));
            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá sản phẩm [{productCode}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                var myDict = new Dictionary<string, object>()
                {
                    {"IsDelete",true },
                };

                SQLHelper<ProductRTCModel>.UpdateFieldsByID(myDict, id);

                grvData.DeleteSelectedRows();
            }
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_ItemClick(null, null);
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn));
                if (string.IsNullOrWhiteSpace(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void grvDataGroup_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadData();
        }
    }
}
