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
using System.Windows.Forms;

namespace BMS
{
    public partial class frmProductGroupDetail : _Forms
    {
        public ProductGroupModel productGroup = new ProductGroupModel();

        int warehouseID = 0;
        public frmProductGroupDetail(int warehouseID)
        {
            InitializeComponent();
            this.warehouseID = warehouseID;
        }
        /// <summary>
        /// load dữ liễu lên khi mở form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmProductGroupDetail_Load(object sender, EventArgs e)
        {
            LoadWareHouse();
            LoadEmployee();
            loadData();
        }
        /// <summary>
        /// void loadData
        /// </summary>
        private void loadData()
        {
            txtName.Text = productGroup.ProductGroupName;
            txtCode.Text = TextUtils.ToString(productGroup.ProductGroupID);

            var exp1 = new Expression("ProductGroupID", productGroup.ID);
            var exp2 = new Expression("WarehouseID", TextUtils.ToInt(cboWarehouse.EditValue));
            ProductGroupWarehouseModel groupWarehouse = SQLHelper<ProductGroupWarehouseModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
            groupWarehouse = groupWarehouse ?? new ProductGroupWarehouseModel();
            cboEmployee.EditValue = groupWarehouse.EmployeeID;
        }

        void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;

            cboEmployee.EditValue = Global.EmployeeID;
        }

        private void LoadWareHouse()
        {
            List<WarehouseModel> lst = SQLHelper<WarehouseModel>.FindAll();
            cboWarehouse.Properties.DataSource = lst;
            cboWarehouse.Properties.ValueMember = "ID";
            cboWarehouse.Properties.DisplayMember = "WarehouseName";

            cboWarehouse.EditValue = warehouseID;
            cboWarehouse.Enabled = warehouseID == 0;
        }

        /// <summary>
        /// void lưu dữ liệu
        /// </summary>
        private void SaveGroup()
        {
            if (!ValidateForm()) return;
            productGroup.ProductGroupName = txtName.Text;
            productGroup.ProductGroupID = TextUtils.ToString(txtCode.Text);
            productGroup.IsVisible = true;
            //productGroup.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);

            if (productGroup.ID > 0)
            {
                ProductGroupBO.Instance.Update(productGroup);
            }
            else
            {
                ProductGroupBO.Instance.Insert(productGroup);
            }

            //Update người phụ trách kho
            warehouseID = TextUtils.ToInt(cboWarehouse.EditValue);
            if (warehouseID > 0)
            {
                var exp1 = new Expression("ProductGroupID", productGroup.ID);
                var exp2 = new Expression("WarehouseID", warehouseID);
                ProductGroupWarehouseModel groupWarehouse = SQLHelper<ProductGroupWarehouseModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
                groupWarehouse = groupWarehouse ?? new ProductGroupWarehouseModel();
                groupWarehouse.ProductGroupID = productGroup.ID;
                groupWarehouse.WarehouseID = warehouseID;
                groupWarehouse.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
                if (groupWarehouse.ID <= 0)
                {
                    SQLHelper<ProductGroupWarehouseModel>.Insert(groupWarehouse);
                }
                else
                {
                    SQLHelper<ProductGroupWarehouseModel>.Update(groupWarehouse);
                }
            }

        }
        /// <summary>
        /// validate trước khi cất dữ liệu
        /// </summary>
        /// <returns></returns>
        bool ValidateForm()
        {
            if (TextUtils.ToInt(cboWarehouse.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Kho!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (string.IsNullOrEmpty(txtCode.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Kí hiệu!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Tên nhóm!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        /// <summary>
        /// event save when used btnSave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveGroup();
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// save and creat new txtbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            SaveGroup();
            txtName.Clear();
        }

        private void frmProductGroupDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch))
            {
                e.Handled = true;
            }
        }

        private void frmProductGroupDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
