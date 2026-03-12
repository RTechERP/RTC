using BMS.Business;
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
    public partial class frmTaxCompany : _Forms
    {
        public frmTaxCompany()
        {
            InitializeComponent();
        }

        private void frmTaxCompany_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            List<TaxCompanyModel> listDT = SQLHelper<TaxCompanyModel>.FindByAttribute(TaxCompanyModel_Enum.IsDeleted.ToString(), 0);
            grdData.DataSource = listDT;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmTaxCompanyDetail frm = new frmTaxCompanyDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int handle = grvData.FocusedRowHandle;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id <= 0) return;

            TaxCompanyModel model = SQLHelper<TaxCompanyModel>.FindByID(id);
            if (model == null) return;

            frmTaxCompanyDetail frm = new frmTaxCompanyDetail();
            frm.taxcModel = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
                grvData.FocusedRowHandle = handle;
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id <= 0) return;
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));

            //var exp1 = new Expression(TaxCompanyModel_Enum.ID.ToString(), id, "<>");
            //var exp2 = new Expression(TaxCompanyModel_Enum.IsDeleted.ToString(), 1, "<>");
            //var exp3 = new Expression(TaxCompanyModel_Enum.Ta.ToString(), 1, "<>");
            if (TaxEmployeeBO.Instance.CheckExist("TaxCompanyID", id))
            {
                MessageBox.Show($"Công ty có mã [{code}] đang được sử dụng nên không thể xóa được!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            TaxCompanyModel model = SQLHelper<TaxCompanyModel>.FindByID(id);
            if (model == null) return;
            if (MessageBox.Show($"Bạn có muốn xóa công ty có mã [{code}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //TaxCompanyBO.Instance.Delete(id);
                model.IsDeleted = true;
                SQLHelper<TaxCompanyModel>.Update(model);
                grvData.DeleteSelectedRows();
                loadData();
            }
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
