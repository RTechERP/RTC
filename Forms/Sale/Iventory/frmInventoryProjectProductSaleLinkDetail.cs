using BMS;
using BMS.Model;
using BMS.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmInventoryProjectProductSaleLinkDetail : _Forms
    {
        List<int> listSelected = new List<int>();
        public InventoryProjectProductSaleLinkModel inventoryproductsale = new InventoryProjectProductSaleLinkModel();

        public frmInventoryProjectProductSaleLinkDetail()
        {
            InitializeComponent();
        }
        public List<int> SelectedIds
        {
            get { return listSelected; }
        }

        private void frmInventoryProjectProductSaleLinkDetail_Load(object sender, EventArgs e)
        {
            LoadProduct();
            LoadEmployee();
            grdData.ContextMenuStrip = contextMenuStrip1;
        }

        #region LOAD EMPLOYEE
        private void LoadEmployee()
        {
            var employee = SQLHelper<EmployeeModel>.FindAll().Where(x => x.ID == Global.EmployeeID);
            txtEmployee.Text = employee.FirstOrDefault()?.FullName;
        }
        #endregion

        #region LOAD PRODUCT
        private void LoadProduct()
        {
            DataTable dt = TextUtils.LoadDataFromSP(
                "spGetProductSaleWithGroup", "A", new string[] { "@Keyword" }, new object[] { "" }
            );

            grdData.DataSource = dt;
        }
        #endregion
        #region BUTTON SELECT/CANCEL
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grvData.RowCount; i++)
            {
                grvData.SetRowCellValue(i, colIsSelected, true);
            }
        }

        private void bntCancelSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grvData.RowCount; i++)
            {
                grvData.SetRowCellValue(i, colIsSelected, false);
            }
            listSelected.Clear();
        }
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }


        private void chkIsSelect_EditValueChanged(object sender, EventArgs e)
        {
            grvData.CloseEditor();
            bool isSelected = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsSelected));
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductSaleID));

            if (isSelected)
            {
                if (!listSelected.Contains(id)) listSelected.Add(id);
            }
            else
            {
                listSelected.Remove(id);
            }
        }

        private void menuSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grvData.RowCount; i++)
            {
                grvData.SetRowCellValue(i, colIsSelected, true);
            }
        }

        private void menuUnselectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grvData.RowCount; i++)
            {
                grvData.SetRowCellValue(i, colIsSelected, false);
            }
            listSelected.Clear();
        }
    }
}