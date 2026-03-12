using BMS.Business;
using BMS.Model;
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
using static Forms.Classes.cGlobVar;

namespace BMS
{
    public partial class frmVehicleSchedule : _Forms
    {
        public List<VehicleBookingManagementModel> lisVehicleBookingManagementModel = new List<VehicleBookingManagementModel>();
        public frmVehicleSchedule()
        {
            InitializeComponent();
        }

        private void frmVehicleSchedule_Load(object sender, EventArgs e)
        {
            loadVehicle();
            loadVehicleSchedule();
            //cboDepartureAddressActual.SelectedIndex = 0;
        }
        void loadVehicleSchedule()
        {
            dteDepartureDateActual.EditValue = lisVehicleBookingManagementModel.Min(p => p.DepartureDate);
            var model = lisVehicleBookingManagementModel.FirstOrDefault();
            cboDepartureAddressActual.SelectedIndex = model.DepartureAddressStatus;
            if (model.Status == 2 || model.Status == 4)
            {
                cboDepartureAddressActual.SelectedIndex = model.DepartureAddressStatusActual;
            }
            if (lisVehicleBookingManagementModel.Count == 1)
            {
                if (model.Status == 2)
                {
                    txtDepartureAddressActual.Text = model.DepartureAddressActual;
                    dteDepartureDateActual.EditValue = model.DepartureDateActual;

                    cboNameVehicleCharge.EditValue = model.VehicleManagementID;
                    txtDriverName.Text = model.DriverName;
                    txtLicensePlate.Text = model.LicensePlate;
                    txtPhoneNumber.Text = model.DriverPhoneNumber;
                }
                else if (model.Status == 4)//LinhTN update 05/06/2024
                {
                    //cboDepartureAddressActual.SelectedIndex = model.DepartureAddressStatusActual - 1;
                    txtDepartureAddressActual.Text = model.DepartureAddressActual;
                    dteDepartureDateActual.EditValue = model.DepartureDateActual;

                    chkStatus.Checked = true;
                }
            }
        }
        void loadVehicle()
        {
            //var dt = SQLHelper<VehicleManagementModel>.FindAll();

            DataTable dt = TextUtils.LoadDataFromSP("spGetVehicleManagement", "VehicleManagement", new string[] { }, new object[] { });
            cboNameVehicleCharge.Properties.DataSource = dt;
            cboNameVehicleCharge.Properties.DisplayMember = "VehicleName";
            cboNameVehicleCharge.Properties.ValueMember = "ID";
            cboNameVehicleCharge.EditValue = 0;
        }
        bool SaveData()
        {
            //if (!CheckValidate()) return false;
            foreach (var vehicleBookingManagementModel in lisVehicleBookingManagementModel)
            {
                ////LinhTN update 05/06/2024
                //vehicleBookingManagementModel.DepartureAddressStatus = cboDepartureAddressActual.SelectedIndex + 1;
                //vehicleBookingManagementModel.DepartureAddress = txtDepartureAddressActual.Text;
                //vehicleBookingManagementModel.DepartureDate = dteDepartureDateActual.DateTimeOffset.DateTime;
                ////
                vehicleBookingManagementModel.DepartureAddressStatusActual = cboDepartureAddressActual.SelectedIndex;
                vehicleBookingManagementModel.DepartureAddressActual = txtDepartureAddressActual.Text;
                //vehicleBookingManagementModel.DepartureDateActual = dteDepartureDateActual.DateTimeOffset.DateTime;
                vehicleBookingManagementModel.DepartureDateActual = vehicleBookingManagementModel.DepartureDate;
                vehicleBookingManagementModel.VehicleManagementID = Convert.ToInt32(cboNameVehicleCharge.EditValue);
                vehicleBookingManagementModel.NameVehicleCharge = cboNameVehicleCharge.Text;
                vehicleBookingManagementModel.DriverName = txtDriverName.Text;
                vehicleBookingManagementModel.LicensePlate = txtLicensePlate.Text;
                vehicleBookingManagementModel.DriverPhoneNumber = txtPhoneNumber.Text;
                vehicleBookingManagementModel.Status = 2;
                vehicleBookingManagementModel.IsCancel = false;
                vehicleBookingManagementModel.VehicleMoney = TextUtils.ToDecimal(txtVehicleMoney.Text); //LinhTN update 12/08/2024

                //LinhTN update 05/06/2024
                if (chkStatus.Checked)
                {
                    vehicleBookingManagementModel.VehicleManagementID = 0;
                    vehicleBookingManagementModel.Status = 4; //Chủ động phương tiện
                }
                //else
                //{
                //    vehicleBookingManagementModel.Status = 2;
                //}

                if (vehicleBookingManagementModel.ID > 0)
                {
                    //VehicleBookingManagementBO.Instance.Update(vehicleBookingManagementModel);
                    SQLHelper<VehicleBookingManagementModel>.Update(vehicleBookingManagementModel);
                    //var exsist = SQLHelper<VehicleBookingManagementModel>.FindByAttribute("ParentID", vehicleBookingManagementModel.ID);
                    //if (vehicleBookingManagementModel.Category == 1 && vehicleBookingManagementModel.TimeReturn != null && exsist.Count == 0)
                    //{
                    //    var timeNeedPresent = vehicleBookingManagementModel.TimeReturn + (vehicleBookingManagementModel.TimeNeedPresent - vehicleBookingManagementModel.DepartureDate);

                    //    vehicleBookingManagementModel.DepartureAddress = vehicleBookingManagementModel.CompanyNameArrives;
                    //    vehicleBookingManagementModel.DepartureDate = vehicleBookingManagementModel.TimeReturn;
                    //    vehicleBookingManagementModel.TimeNeedPresent = timeNeedPresent;
                    //    vehicleBookingManagementModel.TimeReturn = null;
                    //    vehicleBookingManagementModel.Category = 3;
                    //    vehicleBookingManagementModel.ParentID = vehicleBookingManagementModel.ID;
                    //    vehicleBookingManagementModel.DepartureAddressActual = vehicleBookingManagementModel.CompanyNameArrives;
                    //    vehicleBookingManagementModel.DepartureDateActual = vehicleBookingManagementModel.TimeReturn;
                    //    VehicleBookingManagementBO.Instance.Insert(vehicleBookingManagementModel);
                    //}
                }
            }
            return true;
        }


        bool CheckValidate()
        {
            //if (string.IsNullOrEmpty(txtDepartureAddressActual.Text.Trim()))
            //{
            //    MessageBox.Show("Vui lòng nhập địa điểm xuất phát!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}
            //if (string.IsNullOrEmpty(dteDepartureDateActual.Text.Trim()))
            //{
            //    MessageBox.Show("Vui lòng nhập thời gian xuất phát!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}
            //if (Convert.ToInt32(cboNameVehicleCharge.EditValue) == 0)
            //{
            //    MessageBox.Show("Vui lòng chọn xe phụ trách!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}

            if (!chkStatus.Checked) //LinhTN update 05/06/2024
            {
                if (string.IsNullOrEmpty(txtDriverName.Text.Trim()))
                {
                    MessageBox.Show("Vui lòng nhập người lái xe!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (string.IsNullOrEmpty(txtLicensePlate.Text.Trim()))
                {
                    MessageBox.Show("Vui lòng nhập biển số xe!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                string regexPatternLicensePlate = @"^\d{2}-?\s?[A-Z0-9]{1,2}[-\s]\d{3}\.\d{2}$";
                if (!Regex.IsMatch(txtLicensePlate.Text, regexPatternLicensePlate))
                {
                    MessageBox.Show("Biển số xe không đúng định dạng. VD: 89F-118.52 hoặc 89-F1 118.52!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (string.IsNullOrEmpty(txtPhoneNumber.Text.Trim()))
                {
                    MessageBox.Show("Vui lòng nhập SDT liên hệ!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (!Regex.IsMatch(txtPhoneNumber.Text.Trim(), @"^0\d{3}[ .]?\d{3}[ .]?\d{3}$"))
                {
                    MessageBox.Show("SDT liên hệ không đúng định dạng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
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

        private void cboNameVehicleCharge_EditValueChanged(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(cboNameVehicleCharge.EditValue);
            if (id > 0)
            {
                //var dt = SQLHelper<VehicleManagementModel>.FindByID(id);
                //txtDriverName.Text = dt.DriverName;
                //txtLicensePlate.Text = dt.LicensePlate;
                //txtPhoneNumber.Text = dt.PhoneNumber;

                //LinhTN update 12/08/2024 - start
                SearchLookUpEdit lookUpEdit = (SearchLookUpEdit)sender;
                DataRowView dataRow = (DataRowView)lookUpEdit.GetSelectedDataRow();
                if (dataRow != null)
                {
                    txtDriverName.Text = dataRow.Row.Field<string>("DriverName");
                    txtLicensePlate.Text = dataRow.Row.Field<string>("LicensePlate");
                    txtPhoneNumber.Text = dataRow.Row.Field<string>("PhoneNumber");
                    txtVehicleCategory.Text = dataRow.Row.Field<string>("VehicleCategoryText");
                }
                //LinhTN update 12/08/2024 - end
                txtDriverName.Enabled = false;
                txtLicensePlate.Enabled = false;
            }
            else
            {
                txtDriverName.Enabled = true;
                txtLicensePlate.Enabled = true;
            }
        }

        //LinhTN update 27/05/2024
        private void cboDepartureAddressActual_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDepartureAddressActual.SelectedIndex == 0)
            {
                txtDepartureAddressActual.Text = "";
                txtDepartureAddressActual.Enabled = true;
            }
            else
            {
                txtDepartureAddressActual.Text = cboDepartureAddressActual.Text;
                txtDepartureAddressActual.Enabled = false;
            }
        }

        private void chkStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (chkStatus.Checked)
            {
                cboNameVehicleCharge.Enabled = false;
                txtDriverName.Enabled = false;
                txtLicensePlate.Enabled = false;
                txtPhoneNumber.Enabled = false;
            }
            else
            {
                cboNameVehicleCharge.Enabled = true;
                txtDriverName.Enabled = true;
                txtLicensePlate.Enabled = true;
                txtPhoneNumber.Enabled = true;
            }
        }
    }
}