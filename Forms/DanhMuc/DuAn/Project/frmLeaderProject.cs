using BMS.Business;
using BMS.Model;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class frmLeaderProject : _Forms
    {
        public frmLeaderProject()
        {
            InitializeComponent();
        }

        private void frmLeaderProject_Load(object sender, EventArgs e)
        {
            loadData();
        }
        void loadData()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeApprove", "A", new string[] { "@Type" }, new object[] { 2 });
            grdData.DataSource = dt;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmLeaderProjectDetail frm = new frmLeaderProjectDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }

        private void grvData_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;
            try
            {
                if (e.Column.FieldName == "DepartmentName")
                {
                    object val1 = view.GetListSourceRowCellValue(e.ListSourceRowIndex1, "DepartmentID");
                    object val2 = view.GetListSourceRowCellValue(e.ListSourceRowIndex2, "DepartmentID");
                    e.Handled = true;
                    e.Result = System.Collections.Comparer.Default.Compare(val1, val2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id <= 0)
            {
                return;
            }
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            string fullName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
            if (MessageBox.Show($"Bạn có chắc muốn xóa nhân viên [{code} - {fullName}] không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                EmployeeApproveBO.Instance.Delete(id);
                grvData.DeleteSelectedRows();
            }
        }

        private void frmLeaderProject_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void thêmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnNew_Click(null, null);
        }
    }
}