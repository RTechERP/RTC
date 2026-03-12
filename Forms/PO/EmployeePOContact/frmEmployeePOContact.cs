using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMS.Model;
using BMS.Business;
using BMS.BO;
using DevExpress.XtraPrinting;
using System.Diagnostics;

namespace BMS
{
    public partial class frmEmployeePOContact : _Forms
    {
        public frmEmployeePOContact()
        {
            InitializeComponent();
        }

        private void frmEmployeePOContact_Load(object sender, EventArgs e)
        {
            DataTable dataTable = TextUtils.Select("EXEC spGetEmployeePOContact");
            grdData.DataSource = dataTable;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmEmployeePOcontactDetail frm = new frmEmployeePOcontactDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                frmEmployeePOContact_Load(null, null);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedrowhandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0)
            {
                MessageBox.Show("Chưa có thông tin liên lạc của bất kì nhân viên nào để sửa. Vui lòng thêm thông tin liên lạc của nhân viên trước!", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                return;
            }
            EmployeePOContactModel model = (EmployeePOContactModel)EmployeePOContactBO.Instance.FindByPK(ID);
            frmEmployeePOcontactDetail frm = new frmEmployeePOcontactDetail();
            frm.model = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                frmEmployeePOContact_Load(null, null);
                grvData.FocusedRowHandle = focusedrowhandle;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colEmployeeCode));
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colEmployeeName));
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID <= 0)
            {
                MessageBox.Show("Chưa có thông tin liên lạc của bất kì nhân viên nào để xóa.", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show(string.Format("Bạn có muốn xóa thông tin liên lạc nhân viên [{0}-{1}] không?", name, code), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                EmployeePOContactBO.Instance.Delete(ID);
                grvData.DeleteSelectedRows();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colEmployeeCode));
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colEmployeeName));
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
                optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                grvData.OptionsPrint.PrintSelectedRowsOnly = false;
                try
                {
                    string filepath = $"{f.SelectedPath}/TTLL_NhanVien-{code}-{name}.xls";
                    grvData.ExportToXls(filepath, optionsEx);

                    Process.Start(filepath);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                grvData.ClearSelection();
            }
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }
    }
}
