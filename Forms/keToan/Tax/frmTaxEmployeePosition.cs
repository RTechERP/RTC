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
    public partial class frmTaxEmployeePosition : _Forms
    {
        public frmTaxEmployeePosition()
        {
            InitializeComponent();
        }

        private void frmEmployeePositionTax_Load(object sender, EventArgs e)
        {
            loadData();
        }


        void loadData()
        {
            List<TaxEmployeePositionModel> listDT = SQLHelper<TaxEmployeePositionModel>.SqlToList("SELECT * FROM dbo.TaxEmployeePosition WITH(NOLOCK) ORDER BY PriorityOrder");
            grdData.DataSource = listDT;
        }
        #region THÊM, SỬA, XÓA
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmTaxEmployeePositionDetail frm = new frmTaxEmployeePositionDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id == 0) return;

            TaxEmployeePositionModel eTaxmodel = (TaxEmployeePositionModel)TaxEmployeePositionBO.Instance.FindByPK(id);

            frmTaxEmployeePositionDetail frm = new frmTaxEmployeePositionDetail();
            frm.taxEPModel = eTaxmodel;
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

            if (TaxEmployeeBO.Instance.CheckExist("TaxEmployeePositionID", id))
            {
                MessageBox.Show($"Chức vụ [{code}] đang được sử dụng nên không thể xóa được!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có muốn xóa chức vụ [{code}] không ?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                TaxEmployeePositionBO.Instance.Delete(id);
                grvData.DeleteSelectedRows();
                loadData();
            }      
        }
        #endregion

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }
    }
}
