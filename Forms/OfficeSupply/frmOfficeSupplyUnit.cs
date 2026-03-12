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
using BMS.Model;

namespace BMS
{ 
    public partial class frmOfficeSupplyUnit : _Forms
    {
        OfficeSupplyUnitModel model = new OfficeSupplyUnitModel();
        public frmOfficeSupplyUnit()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            List<OfficeSupplyUnitModel> dt = SQLHelper<OfficeSupplyUnitModel>.FindAll();
            grdData.DataSource = dt;
        }
        private void ResetInputs()
        {
            model.ID = 0;
            model.Name = "";
            txtName.Text = "";
            btnUpdate.Text = "Thêm";
        }
        private void UpdateOfficeSupplyUnit()
        {
            if (model.ID > 0)
                SQLHelper<OfficeSupplyUnitModel>.Update(model);
            else
                SQLHelper<OfficeSupplyUnitModel>.Insert(model);
            ResetInputs();
            LoadData();
        }
        private void frmOfficeSupplyUnit_Load(object sender, EventArgs e)
        {
            btnUpdate.Text = "Thêm";
            LoadData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateOfficeSupplyUnit();
        }

        private void grvData_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Column != null)
            {
                model.ID = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, "ID"));
                model.Name = TextUtils.ToString(grvData.GetRowCellValue(e.RowHandle, "Name"));
                txtName.Text = model.Name;
                btnUpdate.Text = "Sửa";
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                ResetInputs();
            }
            else
                model.Name = txtName.Text;
        }
        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
                UpdateOfficeSupplyUnit();
        }

        private void frmOfficeSupplyUnit_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}