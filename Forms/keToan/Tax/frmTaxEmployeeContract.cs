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
    public partial class frmTaxEmployeeContract : _Forms
    {
        public int taxEmployeeID; 
        public frmTaxEmployeeContract()
        {
            InitializeComponent();
        }

        private void frmTaxEmployeeContract_Load(object sender, EventArgs e)
        {
            loadTaxEmployee();
            loadLoaiHDLD();
            loadData();
        }
        private void loadData()
        {
            grdData.DataSource = TextUtils.LoadDataFromSP("spGetTaxEmployeeContract", "A",
                new string[] { "@TaxEmployeeID", "@LoaiHDLDID", "@FilterText" },
                new object[] { TextUtils.ToInt(cboTaxEmployee.EditValue), TextUtils.ToInt(cboLoaiHDLD.EditValue), txtFilterText.Text.Trim() });
        }
        private void loadTaxEmployee()
        {
            cboTaxEmployee.Properties.DataSource = SQLHelper<TaxEmployeeModel>.SqlToList("SELECT ID,Code,FullName FROM dbo.TaxEmployee");
            cboTaxEmployee.Properties.ValueMember = "ID";
            cboTaxEmployee.Properties.DisplayMember = "FullName";
            cboTaxEmployee.EditValue = taxEmployeeID;
        }
        private void loadLoaiHDLD()
        {
            cboLoaiHDLD.Properties.DataSource = SQLHelper<EmployeeLoaiHDLDModel>.SqlToList("SELECT ID,Code,Name FROM dbo.EmployeeLoaiHDLD");
            cboLoaiHDLD.Properties.ValueMember = "ID";
            cboLoaiHDLD.Properties.DisplayMember = "Name";
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmTaxEmployeeContractDetail frm = new frmTaxEmployeeContractDetail();
            frm.taxEmployeeID = TextUtils.ToInt(cboTaxEmployee.EditValue);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int handle = grvData.FocusedRowHandle;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            int taxEmployeeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colTaxEmployeeID));
            if (id <= 0) return;

            frmTaxEmployeeContractDetail frm = new frmTaxEmployeeContractDetail();
            frm.taxContractID = id;
            frm.taxEmployeeID = taxEmployeeID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
                grvData.FocusedRowHandle = handle;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int handle = grvData.FocusedRowHandle;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id <= 0) return;
            string contractNumber = TextUtils.ToString(grvData.GetFocusedRowCellValue(colContractNumber));
            int taxEmployeeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colTaxEmployeeID));

            if (MessageBox.Show($"Bạn có muốn xóa hợp đồng [{contractNumber}]", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                TaxEmployeeContractModel taxECModel = SQLHelper<TaxEmployeeContractModel>.FindByID(id);
                if (taxECModel == null) return;
                taxECModel.IsDelete = true;
                if (taxECModel.ID > 0)
                {
                    SQLHelper<TaxEmployeeContractModel>.Update(taxECModel);
                }
                loadData();
                grvData.FocusedRowHandle = handle;

                //Update lại loại hợp đồng hiện tại
                var exp1 = new Expression("TaxEmployeeID", taxEmployeeID);
                var exp2 = new Expression("IsDelete", 1, "<>");
                List<TaxEmployeeContractModel> list = SQLHelper<TaxEmployeeContractModel>.FindByExpression(exp1.And(exp2)).OrderByDescending(x => x.CreatedDate).ToList();
                if (list.Count > 0)
                {
                    int currentContract = list.FirstOrDefault() == null ? 0 : list.FirstOrDefault().EmployeeLoaiHDLDID;
                    TaxEmployeeModel taxEmployee = SQLHelper<TaxEmployeeModel>.FindByID(taxEmployeeID);
                    if (taxEmployee == null) return;
                    taxEmployee.LoaiHDLDID = currentContract;
                    SQLHelper<TaxEmployeeModel>.Update(taxEmployee);
                }
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {

        }

        private void cboTaxEmployee_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void cboLoaiHDLD_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void grvData_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (TextUtils.ToBoolean(grvData.GetRowCellValue(e.RowHandle, colIsDelete)))
            {
                e.Appearance.BackColor = Color.Red;
                e.Appearance.ForeColor = Color.White;
            }
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }
    }
}
