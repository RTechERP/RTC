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
    public partial class frmModulaLocation: _Forms
    {
        public frmModulaLocation()
        {
            InitializeComponent();
        }

        private void frmModulaLocation_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            grdData.DataSource = null;
            //List<ModulaLocationModel> modulaLocations = SQLHelper<ModulaLocationModel>.FindByAttribute(ModulaLocationModel_Enum.IsDeleted.ToString(),0);
            List<ModulaLocationModel> modulaLocations = SQLHelper<ModulaLocationModel>.FindAll().OrderBy(x=>x.STT).ToList();
            grdData.DataSource = modulaLocations;
            LoadDetail();
        }

        void LoadDetail()
        {
            grdDetail.DataSource = null;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(ModulaLocationModel_Enum.ID.ToString()));

            var exp1 = new Expression(ModulaLocationDetailModel_Enum.ModulaLocationID, id);
            var exp2 = new Expression(ModulaLocationDetailModel_Enum.IsDeleted, 0);

            List<ModulaLocationDetailModel> details = SQLHelper<ModulaLocationDetailModel>.FindByExpression(exp1.And(exp2));
            grdDetail.DataSource = details;
        }

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmModulaLocationDetail frm = new frmModulaLocationDetail();
            frm.SaveEvent += LoadData;
            frm.Show();

            grvData.FocusedRowHandle = grvData.RowCount - 1;
        }

        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int focusRow = grvData.FocusedRowHandle;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(ModulaLocationModel_Enum.ID.ToString()));
            if (id <= 0) return;

            ModulaLocationModel modulaLocation = SQLHelper<ModulaLocationModel>.FindByID(id);
            frmModulaLocationDetail frm = new frmModulaLocationDetail();
            frm.modulaLocation = modulaLocation;
            frm.SaveEvent += LoadData;
            frm.Show();

            grvData.FocusedRowHandle = focusRow;
        }

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(ModulaLocationModel_Enum.ID.ToString()));
            if (id <= 0) return;

            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(ModulaLocationModel_Enum.Name.ToString()));
            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xóa Tray [{name}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                var myDict = new Dictionary<string, object>()
                {
                    { ModulaLocationModel_Enum.IsDeleted.ToString(),true},
                    { ModulaLocationModel_Enum.UpdatedDate.ToString(),DateTime.Now},
                    { ModulaLocationModel_Enum.UpdatedBy.ToString(),Global.AppUserName},
                };

                SQLHelper<ModulaLocationModel>.UpdateFieldsByID(myDict, id);

                LoadData();
            }
        }

        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_ItemClick(null, null);
        }

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;

            bool isDeleted = TextUtils.ToBoolean(grvData.GetRowCellValue(e.RowHandle, colIsDeleted));
            if (isDeleted)
            {
                e.Appearance.BackColor = Color.Red;
                e.Appearance.ForeColor = Color.White;
            }
        }

        private void btnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }
    }
}
