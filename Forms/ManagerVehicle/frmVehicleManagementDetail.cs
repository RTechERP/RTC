using BMS.Business;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmVehicleManagementDetail : _Forms
    {
        public VehicleManagementModel vehicleManagementModel = new VehicleManagementModel();
        public frmVehicleManagementDetail()
        {
            InitializeComponent();
        }

        private void frmVehicleManagementDetail_Load(object sender, EventArgs e)
        {
            ////LinhTN update 05/06/2024
            //string[] vehicleCategory = {
            //    "Xe nội bộ",
            //    "Grab/taxi ngoài",
            //    "Limousine/taxi ghép", 
            //    "Xe ôm giao hàng",
            //    "Xe tải ngoài"
            //};
            ////
            //cboVehicleCategory.Items.AddRange(vehicleCategory);
            LoadVehicleCategory(); //LinhTN update 12/08/2024
            LoadEmployee();
            loadVehicleManagementDetail();
        }
        void LoadVehicleCategory() //LinhTN update 12/08/2024
        {
            List<VehicleCategoryModel> list = SQLHelper<VehicleCategoryModel>.FindByAttribute("IsDelete", 0).OrderBy(p => p.STT).ToList();
            cboVehicleCategory.Properties.DataSource = list;
            cboVehicleCategory.Properties.DisplayMember = "CategoryName";
            cboVehicleCategory.Properties.ValueMember = "ID";
            
        }
        void LoadEmployee()
        {
            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetEmployeeAndEmployeeApprover", new string[] { }, new object[] { });
            cboDriver.Properties.DataSource = dataSet.Tables[0];
            cboDriver.Properties.DisplayMember = "Code";
            cboDriver.Properties.ValueMember = "ID";
            cboDriver.EditValue = vehicleManagementModel.EmployeeID;
        }


        void loadVehicleManagementDetail()
        {
            txtVehicleName.Text = vehicleManagementModel.VehicleName;
            txtLicensePlate.Text = vehicleManagementModel.LicensePlate;
            txtSlot.Value = vehicleManagementModel.Slot;
            //cboVehicleCategory.SelectedIndex = vehicleManagementModel.VehicleCategory;
            txtDriverName.Text = vehicleManagementModel.DriverName;
            txtPhoneNumber.Text = vehicleManagementModel.PhoneNumber;
            txtSTT.Value = vehicleManagementModel.STT;

            cboVehicleCategory.EditValue = vehicleManagementModel.VehicleCategoryID;
        }
        private void cboVehicleCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cboVehicleCategory.SelectedIndex == 1)
            //{
            //    txtSlot.Value = 2;
            //    txtSlot.Enabled = false;
            //}
            //else
            //{
            //    txtSlot.Value = vehicleManagementModel.Slot;
            //    txtSlot.Enabled = true;
            //}
        }
        private void cboDriver_EditValueChanged(object sender, EventArgs e)
        {
            vehicleManagementModel.EmployeeID = TextUtils.ToInt(cboDriver.EditValue);
            EmployeeModel employee = SQLHelper<EmployeeModel>.FindByID(vehicleManagementModel.EmployeeID);
            txtDriverName.Text = employee.FullName;
            txtPhoneNumber.Text = employee.SDTCaNhan;
            if (vehicleManagementModel.EmployeeID > 0)
            {
                txtDriverName.Enabled = false;
                txtPhoneNumber.Enabled = false;
            }
            else
            {
                txtDriverName.Enabled = true;
                txtPhoneNumber.Enabled = true;
            }
        }
        bool SaveData()
        {
            //if (!Validate()) return false;
            vehicleManagementModel.VehicleName = txtVehicleName.Text;
            vehicleManagementModel.LicensePlate = txtLicensePlate.Text;
            vehicleManagementModel.Slot = TextUtils.ToInt(txtSlot.Value);
            //vehicleManagementModel.VehicleCategory = cboVehicleCategory.SelectedIndex;
            //vehicleManagementModel.VehicleCategory = TextUtils.ToInt(cboVehicleCategory.EditValue) - 1; //LinhTN update 12/08/2024
            vehicleManagementModel.DriverName = txtDriverName.Text.Trim();
            vehicleManagementModel.PhoneNumber = txtPhoneNumber.Text.Trim();
            vehicleManagementModel.STT = TextUtils.ToInt(txtSTT.Value);

            vehicleManagementModel.VehicleCategoryID = TextUtils.ToInt(cboVehicleCategory.EditValue);
            if (vehicleManagementModel.ID > 0)
            {
                VehicleManagementBO.Instance.Update(vehicleManagementModel);
            }
            else
            {
                VehicleManagementBO.Instance.Insert(vehicleManagementModel);
            }

            return true;
        }

        bool Validate()
        {
            if (String.IsNullOrEmpty(txtLicensePlate.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Biển số xe!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (String.IsNullOrEmpty(txtVehicleName.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập tên xe!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            //if (String.IsNullOrEmpty(txtDriverName.Text.Trim()))
            //{
            //    MessageBox.Show("Vui lòng nhập tên người lái xe!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}
            if (String.IsNullOrEmpty(txtPhoneNumber.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập SDT liên hệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            //if(txtSlot.Value < 1 || txtSlot.Value > 45)
            //{
            //    MessageBox.Show("Chỗ ngồi của xe phải nằm trong khoảng từ 1 đến 45!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}

            int id = vehicleManagementModel.ID;
            var exp1 = new Expression("PhoneNumber", txtPhoneNumber.Text);
            var exp2 = new Expression("ID", id, "<>");
            List<VehicleManagementModel> vm1 = SQLHelper<VehicleManagementModel>.FindByExpression(exp1.And(exp2));
            if (vm1.Count > 0)
            {
                MessageBox.Show("SDT liên hệ đã tồn tại. Vui lòng nhập SDT liên hệ khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            var exp3 = new Expression("LicensePlate", txtLicensePlate.Text);
            var exp4 = new Expression("ID", id, "<>");
            List<VehicleManagementModel> vm2 = SQLHelper<VehicleManagementModel>.FindByExpression(exp3.And(exp4));
            if (vm2.Count > 0)
            {
                MessageBox.Show("Biển số xe [] đã tồn tại. Vui lòng nhập biển số xe khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            string regexPatternLicensePlate = @"^\d{2}-?\s?[A-Z0-9]{1,2}[-\s]\d{3}\.\d{2}$";
            if (!Regex.IsMatch(txtLicensePlate.Text, regexPatternLicensePlate))
            {
                MessageBox.Show("Biển số xe không đúng định dạng. VD: 89F-118.52 hoặc 89-F1 118.52!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            //string regexPatternPhoneNumber = @"^(?:0[1-9]\d{2}[.\s]?\d{3}[.\s]?\d{3})$";
            string regexPatternPhoneNumber = @"^0\d{3}[ .]?\d{3}[ .]?\d{3}$";
            if (!Regex.IsMatch(txtPhoneNumber.Text, regexPatternPhoneNumber))
            {
                MessageBox.Show("SDT liên hệ không đúng định dạng. Vui lòng nhập lại SDT liên hệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                vehicleManagementModel = new VehicleManagementModel();
                loadVehicleManagementDetail();
            }
        }

        private void btnAddVehicleCategory_Click(object sender, EventArgs e)
        {
            frmVehicleCategoryDetail frm = new frmVehicleCategoryDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadVehicleCategory();
            }
        }
    }
}