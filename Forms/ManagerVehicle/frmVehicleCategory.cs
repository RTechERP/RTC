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
    public partial class frmVehicleCategory : _Forms
    {
        public frmVehicleCategory()
        {
            InitializeComponent();
        }

        private void frmVehicleCategory_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        void LoadData()
        {
            List<VehicleCategoryModel> list = SQLHelper<VehicleCategoryModel>.FindByAttribute("IsDelete", 0).OrderBy(p=>p.STT).ToList();
            grdData.DataSource = list;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmVehicleCategoryDetail frm = new frmVehicleCategoryDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID > 0)
            {
                VehicleCategoryModel model = SQLHelper<VehicleCategoryModel>.FindByID(ID);
                frmVehicleCategoryDetail frm = new frmVehicleCategoryDetail();
                frm.vehicleCategory = model;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                    grvData.FocusedRowHandle = focusedRowHandle;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID > 0)
            {
                VehicleCategoryModel model = SQLHelper<VehicleCategoryModel>.FindByID(ID);
                model.IsDelete = true;
                if (MessageBox.Show($"Bạn có chắc chắn muốn xóa loại xe với mã [{model.CategoryCode}] không", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SQLHelper<VehicleCategoryModel>.Update(model);
                    LoadData();
                }
            }
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }
    }
}