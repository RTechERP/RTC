using BMS.Business;
using BMS.Model;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using static Forms.Classes.cGlobVar;

namespace BMS
{
    public partial class frmVehicleManagement : _Forms
    {
        public frmVehicleManagement()
        {
            InitializeComponent();
        }

        private void frmVehicleManagement_Load(object sender, EventArgs e)
        {
            loadVehicleManagement();
        }
        void loadVehicleManagement()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetVehicleManagement", "VehicleManagement", new string[] { }, new object[] { });
            grdData.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmVehicleManagementDetail frm = new frmVehicleManagementDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadVehicleManagement();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //LinhTN update 05/06/2024
            if (ID > 0)
            {
                VehicleManagementModel model = (VehicleManagementModel)VehicleManagementBO.Instance.FindByPK(ID);
                frmVehicleManagementDetail frm = new frmVehicleManagementDetail();
                frm.vehicleManagementModel = model;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    loadVehicleManagement();
                    grvData.FocusedRowHandle = focusedRowHandle;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));

            string lp = TextUtils.ToString(grvData.GetFocusedRowCellValue(colLicensePlate));

            if (MessageBox.Show(string.Format("Bạn có thực sự muốn xóa xe với biển số [{0}] không?", lp), "Xóa xe", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                //VehicleManagementBO.Instance.Delete(id);

                var myDict = new Dictionary<string, object>()
                {
                    {"IsDeleted",1 }
                };

                SQLHelper<VehicleManagementModel>.UpdateFieldsByID(myDict, id);


                grvData.DeleteSelectedRows();
            }
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
                optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                grvData.OptionsPrint.PrintSelectedRowsOnly = false;
                try
                {
                    string filepath = $"{f.SelectedPath}/DanhSachXe{DateTime.Now.Ticks}.xls";
                    grvData.ExportToXls(filepath, optionsEx);

                    Process.Start(filepath);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo");
                }
                grvData.ClearSelection();
            }
        }

        private void btnAddVehicleCategory_Click(object sender, EventArgs e)
        {
            frmVehicleCategory frm = new frmVehicleCategory();
            frm.Show();
        }
    }
}