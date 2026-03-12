using BMS.Business;
using BMS.Model;
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
    public partial class frmTaxDepartment : _Forms
    {
        public frmTaxDepartment()
        {
            InitializeComponent();
        }

        private void frmDepartmentTax_Load(object sender, EventArgs e)
        {
            loadData();
        }

        void loadData()
        {
            List<TaxDepartmentModel> listData = SQLHelper<TaxDepartmentModel>.SqlToList("SELECT * FROM TaxDepartment ORDER BY STT ASC");
            grdData.DataSource = listData;
        }

        #region ADD, EDIT, DELETE
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmTaxDepartmentDetail frm = new frmTaxDepartmentDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id == 0) return;

            frmTaxDepartmentDetail frm = new frmTaxDepartmentDetail();

            TaxDepartmentModel model = (TaxDepartmentModel)TaxDepartmentBO.Instance.FindByPK(id);
            frm.taxModel = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            if (id == 0) return;

            if (TaxEmployeeBO.Instance.CheckExist("TaxDepartmentID", id))
            {
                MessageBox.Show("Phòng thuế này đang được sử dụng nên không thể xóa được!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (MessageBox.Show($"Bạn có muốn xóa phòng ban [{code}] không ?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                TaxDepartmentBO.Instance.Delete(id);
                grvData.DeleteSelectedRows();
                loadData();
            }
        }
        #endregion

        private void grdData_Click(object sender, EventArgs e)
        {
            try
            {
                if (grvData.RowCount > 0)
                {
                    lblCreatedBy.Text = TextUtils.ToString(grvData.GetRowCellValue(grvData.FocusedRowHandle, "CreatedBy"));
                    lblCreatedDate.Text = ((DateTime)grvData.GetRowCellValue(grvData.FocusedRowHandle, "CreatedDate")).ToString(TextUtils.FomatShortDate);
                    lblUpdatedBy.Text = TextUtils.ToString(grvData.GetRowCellValue(grvData.FocusedRowHandle, "UpdatedBy"));
                    lblUpdatedDate.Text = ((DateTime)grvData.GetRowCellValue(grvData.FocusedRowHandle, "UpdatedDate")).ToString(TextUtils.FomatShortDate);
                }
                else
                {
                    lblCreatedBy.Text = "";
                    lblCreatedDate.Text = "";
                    lblUpdatedBy.Text = "";
                    lblUpdatedDate.Text = "";
                }
            }
            catch
            {
                lblCreatedBy.Text = "";
                lblCreatedDate.Text = "";
                lblUpdatedBy.Text = "";
                lblUpdatedDate.Text = "";
            }
        }

        private void txtEditStatus_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            DevExpress.XtraEditors.Repository.RepositoryItemTextEdit edit = sender as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;

            if (e.Value != null && e.Value.ToString() == "0")
            {
                e.DisplayText = "Ngừng hoạt động";
            }
            else
            {
                e.DisplayText = "Hoạt động";
            }
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }
    }
}
