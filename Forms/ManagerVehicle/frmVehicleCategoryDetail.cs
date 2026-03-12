using BMS.Model;
using BMS.Utils;
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

namespace BMS
{
    public partial class frmVehicleCategoryDetail : _Forms
    {
        public VehicleCategoryModel vehicleCategory = new VehicleCategoryModel();
        public frmVehicleCategoryDetail()
        {
            InitializeComponent();
        }

        private void frmVehicleCategoryDetail_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        void LoadData()
        {
            txtSTT.Value = vehicleCategory.STT;
            txtCategoryCode.Text = vehicleCategory.CategoryCode;
            txtCategoryName.Text = vehicleCategory.CategoryName;

            if (vehicleCategory.ID == 0)
            {
                txtSTT.Value = SQLHelper<VehicleCategoryModel>.FindAll().Max(p => p.STT) + 1;
            }
        }
        bool CheckValidate()
        {
            if (string.IsNullOrEmpty(txtCategoryCode.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Mã loại xe!", "Thống báo");
                return false;
            }
            if (string.IsNullOrEmpty(txtCategoryName.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Tên loại xe!", "Thống báo");
                return false;
            }

            var ex1 = new Expression("ID", vehicleCategory.ID, "<>");
            var ex2 = new Expression("CategoryCode", TextUtils.ToString(txtCategoryCode.Text.Trim()));
            var ex3 = new Expression("STT", txtSTT.Value);
            var ex4 = new Expression("IsDelete", 0);
            var exist1 = SQLHelper<VehicleCategoryModel>.FindByExpression(ex1.And(ex2).And(ex4)).FirstOrDefault();
            if (exist1 != null)
            {
                MessageBox.Show($"Mã loại xe [{txtCategoryCode.Text.Trim()} đã tồn tại]!", "Thống báo");
                return false;
            }
            var exist2 = SQLHelper<VehicleCategoryModel>.FindByExpression(ex1.And(ex3).And(ex4)).FirstOrDefault();
            if (exist2 != null)
            {
                MessageBox.Show($"STT [{txtSTT.Value} đã tồn tại]!", "Thống báo");
                return false;
            }

            return true;
        }
        bool Save()
        {
            if (!CheckValidate()) return false;
            vehicleCategory.STT = TextUtils.ToInt(txtSTT.Value);
            vehicleCategory.CategoryCode = txtCategoryCode.Text.Trim();
            vehicleCategory.CategoryName = txtCategoryName.Text.Trim();
            if (vehicleCategory.ID > 0)
            {
                SQLHelper<VehicleCategoryModel>.Update(vehicleCategory);
            }
            else
            {
                SQLHelper<VehicleCategoryModel>.Insert(vehicleCategory);
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                vehicleCategory = new VehicleCategoryModel();
                LoadData();
            }
        }
    }
}