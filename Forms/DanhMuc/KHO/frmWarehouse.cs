using BMS.Business;
using BMS.Model;
using System;
using System.Collections;
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
    public partial class frmWarehouse : _Forms
    {
        public frmWarehouse()
        {
            InitializeComponent();
        }

        private void frmWarehouse_Load(object sender, EventArgs e)
        {
            loadWarehouse();
        }
        private void loadWarehouse()
        {
            DataTable dt = TextUtils.Select($"SELECT * FROM Warehouse");
            grdData.DataSource = dt;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmWarehouseDetail frm = new frmWarehouseDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadWarehouse();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id == 0) return;
            WarehouseModel model = (WarehouseModel)WarehouseBO.Instance.FindByPK(id);
            frmWarehouseDetail frm = new frmWarehouseDetail();
            frm.warehouse = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadWarehouse();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grdData.DataSource == null)
                return;
            int strID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colWarehouseCode));
            if (MessageBox.Show(string.Format($"Bạn có chắc muốn xóa mã kho [{code}] không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                WarehouseBO.Instance.Delete(strID);
                grvData.DeleteSelectedRows();
            }
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }
    }
}
