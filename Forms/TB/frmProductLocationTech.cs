using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMS;
using BMS.Business;
using BMS.Model;

namespace Forms.TB
{
    public partial class frmProductLocationTech : _Forms
    {
        public int warehouseID;
        public frmProductLocationTech(int WarehouseID)
        {
            InitializeComponent();
            warehouseID = WarehouseID;
        }

        private void frmDeviceLocation_Load(object sender, EventArgs e)
        {
            //this.Text += warehouseID == 1 ? " - HN" : (warehouseID == 2 ? " - HCM" : " - BN");
            this.Text += $" - {this.Tag}";
            loadData();
        }
        void loadData()
        {
            //DataTable dt = TextUtils.Select($"SELECT * FROM ProductLocation WHERE WarehouseID = {warehouseID}");
            //List<ProductLocationModel> list = SQLHelper<ProductLocationModel>.FindByAttribute(ProductLocationModel_Enum.WarehouseID.ToString(), warehouseID).OrderBy(x => x.STT).ToList();
            DataTable dt = TextUtils.LoadDataFromSP("spGetProductLocation", "A", new string[] { "@WarehouseID" }, new object[] { warehouseID });
            grdData.DataSource = dt;
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            frmProductLocationDetailTech frm = new frmProductLocationDetailTech(warehouseID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            //ProductLocationModel model = (ProductLocationModel)ProductLocationBO.Instance.FindByPK(ID);
            ProductLocationModel model = SQLHelper<ProductLocationModel>.FindByID(ID);
            frmProductLocationDetailTech frm = new frmProductLocationDetailTech(warehouseID);
            frm.location = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));

            if (id <= 0)
            {
                return;
            }

            int productId = TextUtils.ToInt(TextUtils.ExcuteScalar($"SELECT TOP 1 ID FROM ProductRTC WHERE ProductLocationID = {id}"));
            if (productId > 0)
            {
                MessageBox.Show("Bạn không thể xoá vị trí này. Vì đã được sử dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string locationCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colLocationCode));
            if (MessageBox.Show($"Bạn có chắc muốn xoá vị trí có mã [{locationCode}] này không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ProductLocationBO.Instance.Delete(id);
                grvData.DeleteSelectedRows();
            }
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void frmProductLocationTech_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
