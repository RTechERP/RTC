using BMS.Business;
using BMS.Model;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmDeclareDayOff : _Forms
    {
        public frmDeclareDayOff()
        {
            InitializeComponent();
        }

        private void frmDeclareDayOff_Load(object sender, EventArgs e)
        {
            loadMaster();
        }
        private void loadMaster()
        {
            DataTable dt = TextUtils.GetTable("spGetEmployeeOnLeaveMaster", "A");
            grdMaster.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmDeclareDayOffDetail frm = new frmDeclareDayOffDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadMaster();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int focusRow = grvMaster.FocusedRowHandle;
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            if (id == 0) return;
            EmployeeOnLeaveMasterModel model = (EmployeeOnLeaveMasterModel)EmployeeOnLeaveMasterBO.Instance.FindByPK(id);
            frmDeclareDayOffDetail frm = new frmDeclareDayOffDetail();
            frm.model = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadMaster();
                grvMaster.FocusedRowHandle = focusRow;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
            string fullname = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colEmployeeName));
            if (MessageBox.Show(String.Format($"Bạn có chắc muốn xóa đăng ký nghỉ của nhân viên [{fullname}] không?"), "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                EmployeeOnLeaveMasterBO.Instance.Delete(id);
                grvMaster.DeleteSelectedRows();
            }
        }

        private void grvMaster_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            frmImportExcelDecalareDayOff frm = new frmImportExcelDecalareDayOff();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadMaster();
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();

                optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;

                grvMaster.OptionsPrint.PrintSelectedRowsOnly = false;
                try
                {
                    string filepath = $"{f.SelectedPath}/KhaiBaoNgayNghiPhep.xls";
                    grvMaster.ExportToXls(filepath, optionsEx);
                    Process.Start(filepath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                grvMaster.ClearSelection();
            }
        }
    }
}
