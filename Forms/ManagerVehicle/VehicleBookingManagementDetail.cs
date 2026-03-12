using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections;
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
    public partial class frmVehicleBookingManagementDetail : _Forms
    {
        public VehicleBookingManagementModel vehicleBookingManagementModel = new VehicleBookingManagementModel();
        string[] arrParamName = null;
        object[] arrParamValue = null;
        int count1 = 0;
        int count2 = 0;
        public frmVehicleBookingManagementDetail()
        {
            InitializeComponent();
        }

        private void frmVehicleBookingManagementDetail_Load(object sender, EventArgs e)
        {

            cboDepartureAddress.SelectedIndex = 0;
            //if (IsProblem())
            //{

            //}

            cboEmployeeApprove.Enabled = IsProblem();
            txtProblemArises.Enabled = IsProblem();

            loadPassenger();
            loadAttachedGoods();
            LoadEmployee();
            LoadEmployeeApprove();
            LoadCategory();

            LoadProject();
            loadVehicleBookingManagement();
        }

        bool IsProblem()
        {
            DateTime dateRegister = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);
            DateTime minDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 30, 0);
            DateTime maxDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 21, 00, 0);

            bool isProblem = !(dateRegister >= minDate && dateRegister <= maxDate);

            return isProblem;
        }

        void LoadEmployee()
        {
            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetEmployee", new string[] { "@Status" }, new object[] { 0});
            cboBookerVehicles.Properties.DataSource = dataSet.Tables[0];
            cboBookerVehicles.Properties.DisplayMember = "FullName";
            cboBookerVehicles.Properties.ValueMember = "ID";
            cboBookerVehicles.EditValue = vehicleBookingManagementModel.EmployeeID;

            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "Employee", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee1.DataSource = dt;
            cboEmployee1.DisplayMember = "Code";
            cboEmployee1.ValueMember = "ID";

            cboEmployee2.DataSource = dt;
            cboEmployee2.DisplayMember = "Code";
            cboEmployee2.ValueMember = "ID";
        }
        void LoadEmployeeApprove()
        {
            DataSet dt = TextUtils.LoadDataSetFromSP("spGetEmployeeAndEmployeeApprover", new string[] { }, new object[] { });

            cboEmployeeApprove.Properties.DataSource = dt.Tables[1];
            cboEmployeeApprove.Properties.DisplayMember = "FullName";
            cboEmployeeApprove.Properties.ValueMember = "EmployeeID";
        }
        void loadVehicleBookingManagement()
        {
            if (vehicleBookingManagementModel.ID > 0)
            {
                txtPhoneNumber.Text = vehicleBookingManagementModel.PhoneNumber;
                cboCategory.SelectedValue = vehicleBookingManagementModel.Category;

                txtCompanyNameArrives.Text = vehicleBookingManagementModel.CompanyNameArrives;
                txtProvince.Text = vehicleBookingManagementModel.Province;
                dteTimeNeedPresent.EditValue = vehicleBookingManagementModel.TimeNeedPresent;
                dteTimeReturn.EditValue = vehicleBookingManagementModel.TimeReturn;
                txtSpecificDestinationAddress.Text = vehicleBookingManagementModel.SpecificDestinationAddress;

                cboProject.EditValue = vehicleBookingManagementModel.ProjectID;
            }
        }
        void loadPassenger()
        {
            arrParamName = new string[] { "@StartDate", "@EndDate", "@Keyword", "Category", "@Status", "@EmployeeID" };
            arrParamValue = new object[] { "", "", "", 0, 0, 0 };
            DataTable dt = TextUtils.LoadDataFromSP("spGetVehicleBookingManagement", "VehicleBookingManagement", arrParamName, arrParamValue);
            grdPassenger.DataSource = dt;
        }
        void loadAttachedGoods()
        {
            arrParamName = new string[] { "@StartDate", "@EndDate", "@Keyword", "Category", "@Status", "@EmployeeID" };
            arrParamValue = new object[] { "", "", "", 0, 0, 0 };
            DataTable dt = TextUtils.LoadDataFromSP("spGetVehicleBookingManagement", "VehicleBookingManagement", arrParamName, arrParamValue);
            grdAttachedGoods.DataSource = dt;
        }

        void LoadCategory()
        {
            List<object> lstCategory = new List<object>() {
                new {Category = 0, CategoryText = "--Chọn hình thức đặt--"},
                new {Category = 1, CategoryText = "Đăng ký đi"},
                new {Category = 5, CategoryText = "Đăng ký về"},
                new {Category = 4, CategoryText = "Chủ động phương tiện"},
                new {Category = 2, CategoryText = "Đăng ký giao hàng"},
                new {Category = 6, CategoryText = "Đăng ký lấy hàng"}
            };
            cboCategory.DataSource = lstCategory;
            cboCategory.ValueMember = "Category";
            cboCategory.DisplayMember = "CategoryText";

            cboCategory.SelectedValue = 1;
        }


        void LoadProject()
        {
            List<ProjectModel> list = SQLHelper<ProjectModel>.FindAll().OrderByDescending(x => x.ID).ToList();
            cboProject.Properties.ValueMember = "ID";
            cboProject.Properties.DisplayMember = "ProjectName";
            cboProject.Properties.DataSource = list;
        }
        bool SaveData()
        {
            grvPassenger.CloseEditor();
            grvAttachedGoods.CloseEditor();

            if (!CheckValidate()) return false;
            vehicleBookingManagementModel.EmployeeID = Convert.ToInt32(cboBookerVehicles.EditValue);
            vehicleBookingManagementModel.BookerVehicles = cboBookerVehicles.Text;
            vehicleBookingManagementModel.PhoneNumber = txtPhoneNumber.Text;
            vehicleBookingManagementModel.CompanyNameArrives = txtCompanyNameArrives.Text;
            vehicleBookingManagementModel.Province = txtProvince.Text;
            vehicleBookingManagementModel.SpecificDestinationAddress = txtSpecificDestinationAddress.Text;
            vehicleBookingManagementModel.TimeNeedPresent = dteTimeNeedPresent.DateTimeOffset.DateTime;
            //LinhTN update 22/05/2024
            vehicleBookingManagementModel.DepartureDate = dteDepartureDate.DateTimeOffset.DateTime;
            vehicleBookingManagementModel.DepartureAddressStatus = cboDepartureAddress.SelectedIndex + 1 == 5 ? 0 : cboDepartureAddress.SelectedIndex + 1;
            vehicleBookingManagementModel.DepartureAddress = txtDepartureAddress.Text;
            vehicleBookingManagementModel.Category = TextUtils.ToInt(cboCategory.SelectedValue);
            vehicleBookingManagementModel.ProjectID = TextUtils.ToInt(cboProject.EditValue);


            if (!string.IsNullOrEmpty(dteTimeReturn.Text.Trim()))
                vehicleBookingManagementModel.TimeReturn = dteTimeReturn.DateTimeOffset.DateTime;
            vehicleBookingManagementModel.Status = 1;
            if (IsProblem())
            {
                vehicleBookingManagementModel.ProblemArises = txtProblemArises.Text;
                vehicleBookingManagementModel.ApprovedTBP = TextUtils.ToInt(cboEmployeeApprove.EditValue);
                vehicleBookingManagementModel.IsProblemArises = IsProblem();
            }
            for (int i = 0; i < grvPassenger.RowCount; i++)
            {
                vehicleBookingManagementModel.Category = 1;
                vehicleBookingManagementModel.PassengerEmployeeID = TextUtils.ToInt(grvPassenger.GetRowCellValue(i, colPassengerEmployeeID));
                vehicleBookingManagementModel.PassengerCode = TextUtils.ToString(grvPassenger.GetRowCellValue(i, colPassengerCode));
                vehicleBookingManagementModel.PassengerName = TextUtils.ToString(grvPassenger.GetRowCellValue(i, colPassengerName));
                vehicleBookingManagementModel.PassengerDepartment = TextUtils.ToString(grvPassenger.GetRowCellValue(i, colPassengerDepartment));
                vehicleBookingManagementModel.PassengerPhoneNumber = TextUtils.ToString(grvPassenger.GetRowCellValue(i, colPassengerPhoneNumber));
                vehicleBookingManagementModel.Note = TextUtils.ToString(grvPassenger.GetRowCellValue(i, colPassengerNote));
                vehicleBookingManagementModel.ProjectID = TextUtils.ToInt(cboProject.EditValue);
                //if (String.IsNullOrEmpty(vehicleBookingManagementModel.PassengerName.Trim()))
                //{
                //    MessageBox.Show(string.Format("Thông tin người đi cột [Họ và tên] hàng số [{0}] không được để trống!", grvPassenger.GetRowCellValue(i, colPassengerSTT)), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return false;
                //}
                //if (String.IsNullOrEmpty(vehicleBookingManagementModel.PassengerPhoneNumber.Trim()))
                //{
                //    MessageBox.Show(string.Format("Thông tin người đi cột [SDT liên hệ] hàng [{0}] không được để trống!", grvPassenger.GetRowCellValue(i, colPassengerSTT)), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return false;
                //}
                //VehicleBookingManagementBO.Instance.Insert(vehicleBookingManagementModel);


                //LinhTN update 27/05/2024
                //int id = (int)VehicleBookingManagementBO.Instance.Insert(vehicleBookingManagementModel);

                //LinhTN update 06/11/2024
                int id = SQLHelper<VehicleBookingManagementModel>.Insert(vehicleBookingManagementModel).ID;
                //
                //LinhTN update 11/06/2024
                if (vehicleBookingManagementModel.Category == 1)
                {
                    var exsist = SQLHelper<VehicleBookingManagementModel>.FindByAttribute("ParentID", id);
                    if (vehicleBookingManagementModel.TimeReturn != null && exsist.Count == 0)
                    {
                        var companyNameArrives = vehicleBookingManagementModel.DepartureAddress;
                        var departureAddress = vehicleBookingManagementModel.CompanyNameArrives;

                        vehicleBookingManagementModel.ParentID = id;
                        vehicleBookingManagementModel.Category = 3;
                        vehicleBookingManagementModel.CompanyNameArrives = companyNameArrives;
                        vehicleBookingManagementModel.DepartureAddress = departureAddress;
                        vehicleBookingManagementModel.DepartureDate = vehicleBookingManagementModel.TimeReturn;
                        vehicleBookingManagementModel.Province = "";
                        vehicleBookingManagementModel.SpecificDestinationAddress = "";
                        vehicleBookingManagementModel.TimeReturn = null;

                        //VehicleBookingManagementBO.Instance.Insert(vehicleBookingManagementModel);
                        SQLHelper<VehicleBookingManagementModel>.Insert(vehicleBookingManagementModel);
                    }
                }
            }
            for (int i = 0; i < grvAttachedGoods.RowCount; i++)
            {
                //LinhTN update 11/06/2024
                vehicleBookingManagementModel.ParentID = 0;
                //
                vehicleBookingManagementModel.DeliverName = cboBookerVehicles.Text;
                vehicleBookingManagementModel.DeliverPhoneNumber = txtPhoneNumber.Text;
                vehicleBookingManagementModel.Category = 2;
                vehicleBookingManagementModel.PassengerEmployeeID = 0;
                vehicleBookingManagementModel.PassengerCode = "";
                vehicleBookingManagementModel.PassengerName = "";
                vehicleBookingManagementModel.PassengerPhoneNumber = "";

                vehicleBookingManagementModel.ReceiverEmployeeID = TextUtils.ToInt(grvAttachedGoods.GetRowCellValue(i, colAttachedGoodsEmployeeID));
                vehicleBookingManagementModel.ReceiverCode = TextUtils.ToString(grvAttachedGoods.GetRowCellValue(i, colAttachedGoodsCode));
                vehicleBookingManagementModel.ReceiverName = TextUtils.ToString(grvAttachedGoods.GetRowCellValue(i, colAttachedGoodsName));
                vehicleBookingManagementModel.ReceiverPhoneNumber = TextUtils.ToString(grvAttachedGoods.GetRowCellValue(i, colAttachedGoodsPhoneNumber));
                vehicleBookingManagementModel.PackageName = TextUtils.ToString(grvAttachedGoods.GetRowCellValue(i, colAttachedGoodsPackage));
                //LinhTN update 06/11/2024
                vehicleBookingManagementModel.PackageSize = TextUtils.ToString(grvAttachedGoods.GetRowCellValue(i, colAttachedGoodsPackageSize));
                vehicleBookingManagementModel.PackageWeight = TextUtils.ToString(grvAttachedGoods.GetRowCellValue(i, colAttachedGoodsPackageWeight));
                vehicleBookingManagementModel.PackageQuantity = TextUtils.ToInt(grvAttachedGoods.GetRowCellValue(i, colAttachedGoodsPackageQuantity));
                //
                vehicleBookingManagementModel.Note = TextUtils.ToString(grvAttachedGoods.GetRowCellValue(i, colAttachedGoodsNote));

                vehicleBookingManagementModel.ProjectID = TextUtils.ToInt(cboProject.EditValue);
                //if (String.IsNullOrEmpty(vehicleBookingManagementModel.ReceiverName.Trim()))
                //{
                //    MessageBox.Show(string.Format("Thông tin người nhận hàng cột [Họ và tên] hàng số [{0}] không được để trống!", grvAttachedGoods.GetRowCellValue(i, colAttachedGoodsSTT)), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return false;
                //}
                //if (String.IsNullOrEmpty(vehicleBookingManagementModel.ReceiverPhoneNumber.Trim()))
                //{
                //    MessageBox.Show(string.Format("Thông tin người nhận hàng cột [SDT liên hệ] hàng [{0}] không được để trống!", grvAttachedGoods.GetRowCellValue(i, colAttachedGoodsSTT)), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return false;
                //}
                //if (String.IsNullOrEmpty(vehicleBookingManagementModel.PackageName.Trim()))
                //{
                //    MessageBox.Show(string.Format("Thông tin người nhận hàng cột [Tên kiện hàng] hàng [{0}] không được để trống!", grvAttachedGoods.GetRowCellValue(i, colAttachedGoodsSTT)), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return false;
                //}
                //var exp1 = new Expression("VehicleBookingID", 0);
                //var exp2 = new Expression("STT", TextUtils.ToInt(grvAttachedGoods.GetRowCellValue(i, colAttachedGoodsSTT)));
                //List<VehicleBookingFileModel> listFiles = SQLHelper<VehicleBookingFileModel>.FindByExpression(exp1.And(exp2));
                //if (listFiles.Count <= 0)
                //{
                //    MessageBox.Show(string.Format("Thông tin người nhận hàng cột [File ảnh] hàng [{0}] không được để trống. Vui lòng chọn thêm file ảnh!", grvAttachedGoods.GetRowCellValue(i, colAttachedGoodsSTT)), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return false;
                //}

                //int ID = (int)VehicleBookingManagementBO.Instance.Insert(vehicleBookingManagementModel);
                //LinhTN update 06/11/2024
                int ID = SQLHelper<VehicleBookingManagementModel>.Insert(vehicleBookingManagementModel).ID;
                //
                FileCheck(ID, TextUtils.ToInt(grvAttachedGoods.GetRowCellValue(i, colAttachedGoodsSTT)));
            }

            //Add notify
            if (IsProblem())
            {
                string text = $"Mục đích: {cboBookerVehicles.Text}\n" +
                                $"Lý do: {txtProblemArises.Text.Trim()}\n" +
                                $"Thời gian xuất phát: {vehicleBookingManagementModel.DepartureDate.Value.ToString("dd/MM/yyyy HH:mm")}";
                TextUtils.AddNotify("ĐĂNG KÝ XE", text, TextUtils.ToInt(cboEmployeeApprove.EditValue));
            }

            return true;
        }
        void FileCheck(int vehicleBookingID, int stt)
        {
            List<VehicleBookingFileModel> listFiles = SQLHelper<VehicleBookingFileModel>.FindByAttribute("VehicleBookingID", 0);
            foreach (var item in listFiles)
            {
                if (item.STT == stt)
                {
                    item.STT = 0;
                    item.VehicleBookingID = vehicleBookingID;
                    VehicleBookingFileBO.Instance.Update(item);
                }
            }
        }
        bool CheckValidate()
        {
            int category = TextUtils.ToInt(cboCategory.SelectedValue);
            if (Convert.ToInt32(cboBookerVehicles.EditValue) == 0)
            {
                MessageBox.Show("Vui lòng chọn người đặt xe!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (String.IsNullOrEmpty(txtPhoneNumber.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập SDT liên hệ cùa người đặt!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (!Regex.IsMatch(txtPhoneNumber.Text.Trim(), @"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$"))
            {
                MessageBox.Show("SDT liên hệ của người đặt không đúng định dạng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (IsProblem())
            {
                if (Convert.ToInt32(cboEmployeeApprove.EditValue) == 0)
                {
                    MessageBox.Show("Vui lòng chọn người duyệt!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (String.IsNullOrEmpty(txtProblemArises.Text.Trim()))
                {
                    MessageBox.Show("Vui lòng nhập vấn đề phát sinh!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            if (category == 0)
            {
                MessageBox.Show("Vui lòng chọn hình thức đặt!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (String.IsNullOrEmpty(txtCompanyNameArrives.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập tên công ty đến!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (String.IsNullOrEmpty(txtProvince.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập tỉnh thành đến!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (String.IsNullOrEmpty(dteTimeNeedPresent.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập thời gian cần đến!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (String.IsNullOrEmpty(txtSpecificDestinationAddress.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ cụ thể điểm đến!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (String.IsNullOrEmpty(dteDepartureDate.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập thời gian xuất phát!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (String.IsNullOrEmpty(txtDepartureAddress.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập điểm xuất phát cụ thể!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (category == 3 && (grvPassenger.RowCount == 0 || grvAttachedGoods.RowCount == 0))
            {
                MessageBox.Show("Vui lòng thêm người đi và người nhận hàng khi hình thức đặt xe là [Đăng ký đi và giao thêm hàng]!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (category == 1 && grvPassenger.RowCount == 0)
            {
                MessageBox.Show("Vui lòng thêm người đi khi hình thức đặt xe là [Đăng ký đi]!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (category == 2 && grvAttachedGoods.RowCount == 0)
            {
                MessageBox.Show("Vui lòng thêm người nhận hàng khi hình thức đặt xe là [Đăng ký giao hàng]!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            for (int i = 0; i < grvPassenger.RowCount; i++)
            {
                if (String.IsNullOrEmpty(TextUtils.ToString(grvPassenger.GetRowCellValue(i, colPassengerName)).Trim()))
                {
                    MessageBox.Show(string.Format("Thông tin người đi cột [Họ và tên] hàng số [{0}] không được để trống!", grvPassenger.GetRowCellValue(i, colPassengerSTT)), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (String.IsNullOrEmpty(TextUtils.ToString(grvPassenger.GetRowCellValue(i, colPassengerPhoneNumber)).Trim()))
                {
                    MessageBox.Show(string.Format("Thông tin người đi cột [SDT liên hệ] hàng [{0}] không được để trống!", grvPassenger.GetRowCellValue(i, colPassengerSTT)), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            for (int i = 0; i < grvAttachedGoods.RowCount; i++)
            {
                if (String.IsNullOrEmpty(TextUtils.ToString(grvAttachedGoods.GetRowCellValue(i, colAttachedGoodsName)).Trim()))
                {
                    MessageBox.Show(string.Format("Thông tin người nhận hàng cột [Họ và tên] hàng số [{0}] không được để trống!", grvAttachedGoods.GetRowCellValue(i, colAttachedGoodsSTT)), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (String.IsNullOrEmpty(TextUtils.ToString(grvAttachedGoods.GetRowCellValue(i, colAttachedGoodsPhoneNumber)).Trim()))
                {
                    MessageBox.Show(string.Format("Thông tin người nhận hàng cột [SDT liên hệ] hàng [{0}] không được để trống!", grvAttachedGoods.GetRowCellValue(i, colAttachedGoodsSTT)), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (String.IsNullOrEmpty(TextUtils.ToString(grvAttachedGoods.GetRowCellValue(i, colAttachedGoodsPackage)).Trim()))
                {
                    MessageBox.Show(string.Format("Thông tin người nhận hàng cột [Tên kiện hàng] hàng [{0}] không được để trống!", grvAttachedGoods.GetRowCellValue(i, colAttachedGoodsSTT)), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                //LinhTN update 06/11/2024
                if (String.IsNullOrEmpty(TextUtils.ToString(grvAttachedGoods.GetRowCellValue(i, colAttachedGoodsPackageSize)).Trim()))
                {
                    MessageBox.Show(string.Format("Thông tin người nhận hàng cột [Kích thước] hàng [{0}] không được để trống!", grvAttachedGoods.GetRowCellValue(i, colAttachedGoodsSTT)), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (String.IsNullOrEmpty(TextUtils.ToString(grvAttachedGoods.GetRowCellValue(i, colAttachedGoodsPackageWeight)).Trim()))
                {
                    MessageBox.Show(string.Format("Thông tin người nhận hàng cột [Cân nặng] hàng [{0}] không được để trống!", grvAttachedGoods.GetRowCellValue(i, colAttachedGoodsSTT)), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (String.IsNullOrEmpty(TextUtils.ToString(grvAttachedGoods.GetRowCellValue(i, colAttachedGoodsPackageQuantity)).Trim()))
                {
                    MessageBox.Show(string.Format("Thông tin người nhận hàng cột [Số lượng kiện hàng] hàng [{0}] không được để trống!", grvAttachedGoods.GetRowCellValue(i, colAttachedGoodsSTT)), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (TextUtils.ToInt(grvAttachedGoods.GetRowCellValue(i, colAttachedGoodsPackageQuantity)) <= 0)
                {
                    MessageBox.Show(string.Format("Thông tin người nhận hàng cột [Số lượng kiện hàng] hàng [{0}] phải lớn hơn 0!", grvAttachedGoods.GetRowCellValue(i, colAttachedGoodsSTT)), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                //
                //var exp1 = new Expression("VehicleBookingID", 0);
                //var exp2 = new Expression("STT", TextUtils.ToInt(grvAttachedGoods.GetRowCellValue(i, colAttachedGoodsSTT)));
                //List<VehicleBookingFileModel> listFiles = SQLHelper<VehicleBookingFileModel>.FindByExpression(exp1.And(exp2));
                //if (listFiles.Count <= 0)
                //{
                //    MessageBox.Show(string.Format("Thông tin người nhận hàng cột [File ảnh] hàng [{0}] không được để trống. Vui lòng chọn thêm file ảnh!", grvAttachedGoods.GetRowCellValue(i, colAttachedGoodsSTT)), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return false;
                //}
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
            vehicleBookingManagementModel = new VehicleBookingManagementModel();
            for (int i = grvPassenger.RowCount - 1; i >= 0; i--)
            {
                grvPassenger.DeleteRow(i);
            }
            for (int i = grvAttachedGoods.RowCount - 1; i >= 0; i--)
            {
                grvAttachedGoods.DeleteRow(i);
            }
            loadVehicleBookingManagement();
        }

        private void grvPassenger_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo info = grvPassenger.CalcHitInfo(new Point(e.X, e.Y));
            if (info.Column == colPassengerSTT && e.Y < 40)
            {
                MyLib.AddNewRow(grdPassenger, grvPassenger);
            }
        }
        private void grvAttachedGoods_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo info = grvAttachedGoods.CalcHitInfo(new Point(e.X, e.Y));
            if (info.Column == colAttachedGoodsSTT && e.Y < 40)
            {
                MyLib.AddNewRow(grdAttachedGoods, grvAttachedGoods);
            }
        }

        private void btnDelete5_Click(object sender, EventArgs e)
        {
            string code = TextUtils.ToString(grvPassenger.GetFocusedRowCellDisplayText(colPassengerCode));
            string name = TextUtils.ToString(grvPassenger.GetFocusedRowCellDisplayText(colPassengerName));
            //int rowIndex = grvPassenger.GetSelectedRows()[0];
            if (String.IsNullOrEmpty(code.Trim()))
            {
                if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa người đi tên [{0}]?", name), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    grvPassenger.DeleteSelectedRows();
                }
            }
            else
            {
                if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa người đi mã [{0}] và tên [{1}]?", code, name), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    grvPassenger.DeleteSelectedRows();
                }
            }
        }
        private void btnDelete6_Click(object sender, EventArgs e)
        {
            string code = TextUtils.ToString(grvAttachedGoods.GetFocusedRowCellDisplayText(colAttachedGoodsCode));
            string name = TextUtils.ToString(grvAttachedGoods.GetFocusedRowCellDisplayText(colAttachedGoodsName));
            int stt = TextUtils.ToInt(grvAttachedGoods.GetFocusedRowCellDisplayText(colAttachedGoodsSTT));
            VehicleBookingFileBO.Instance.DeleteByAttribute("STT", stt);
            //int rowIndex = grvPassenger.GetSelectedRows()[0];
            if (String.IsNullOrEmpty(code.Trim()))
            {
                if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa người đi tên [{0}]?", name), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    grvAttachedGoods.DeleteSelectedRows();
                }
            }
            else
            {
                if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa người đi mã [{0}] và tên [{1}]?", code, name), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    grvAttachedGoods.DeleteSelectedRows();
                }
            }
        }
        private void grvPassenger_RowCountChanged(object sender, EventArgs e)
        {
            count1 = grvPassenger.RowCount;
            count2 = grvAttachedGoods.RowCount;
            if (count1 == 0 && count2 > 0)
            {
                cboCategory.SelectedValue = 2;
            }
            else if (count1 > 0 && count2 == 0)
            {
                cboCategory.SelectedValue = 1;
            }
            else if (count1 > 0 && count2 > 0)
            {
                cboCategory.SelectedValue = 3;
            }
        }

        private void grvAttachedGoods_RowCountChanged(object sender, EventArgs e)
        {
            count1 = grvPassenger.RowCount;
            count2 = grvAttachedGoods.RowCount;
            if (count1 == 0 && count2 > 0)
            {
                cboCategory.SelectedValue = 2;
            }
            else if (count1 > 0 && count2 == 0)
            {
                cboCategory.SelectedValue = 1;
            }
            else if (count1 > 0 && count2 > 0)
            {
                cboCategory.SelectedValue = 3;
            }
        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int category = TextUtils.ToInt(cboCategory.SelectedValue);
            if (category == 1 && grvAttachedGoods.RowCount > 0)
            {
                if (MessageBox.Show("Hình thức đặt là [Đăng ký đi] thì tất cả các [Thông tin người nhận hàng] sẽ bị xóa. Bạn có muốn thay đổi?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    for (int i = 0; i < grvAttachedGoods.RowCount; i++)
                    {
                        int ID = TextUtils.ToInt(grvAttachedGoods.GetRowCellValue(i, colPassengerEmployeeID));
                    }
                    for (int i = grvAttachedGoods.RowCount - 1; i >= 0; i--)
                    {
                        grvAttachedGoods.DeleteRow(i);
                    }
                }

            }
            if (category == 2 && grvPassenger.RowCount > 0)
            {
                if (MessageBox.Show("Hình thức đặt là [Đăng ký giao hàng] thì tất cả các [Thông tin người đi] sẽ bị xóa. Bạn có muốn thay đổi?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    for (int i = 0; i < grvPassenger.RowCount; i++)
                    {
                        int ID = TextUtils.ToInt(grvPassenger.GetRowCellValue(i, colPassengerEmployeeID));
                    }
                    for (int i = grvPassenger.RowCount - 1; i >= 0; i--)
                    {
                        grvPassenger.DeleteRow(i);
                    }

                }
            }
        }
        private void cboEmployee1_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit lookUpEdit = (SearchLookUpEdit)sender;
            DataRowView dataRow = (DataRowView)lookUpEdit.GetSelectedDataRow();
            int employeeID = 0;
            string employeeName = "";
            string employeeCode = "";
            string employeeDepartment = "";
            string employeePhoneNumber = "";
            if (dataRow != null)
            {
                if (IsDuplicateRecordPassenger(TextUtils.ToInt(lookUpEdit.EditValue)))
                {
                    MessageBox.Show("Người đi này đã tồn tại trong danh sách. Vui lòng người khác!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    employeeID = TextUtils.ToInt(lookUpEdit.EditValue);
                    employeeName = dataRow.Row.Field<string>("FullName");
                    employeeCode = dataRow.Row.Field<string>("Code");
                    employeeDepartment = dataRow.Row.Field<string>("DepartmentName");
                    employeePhoneNumber = dataRow.Row.Field<string>("SDTCaNhan");
                }
            }
            grvPassenger.SetFocusedRowCellValue(colPassengerEmployeeID, employeeID);
            grvPassenger.SetFocusedRowCellValue(colPassengerName, employeeName);
            grvPassenger.SetFocusedRowCellValue(colPassengerCode, employeeCode);
            grvPassenger.SetFocusedRowCellValue(colPassengerDepartment, employeeDepartment);
            grvPassenger.SetFocusedRowCellValue(colPassengerPhoneNumber, employeePhoneNumber);
        }
        private void cboEmployee2_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit lookUpEdit = (SearchLookUpEdit)sender;
            DataRowView dataRow = (DataRowView)lookUpEdit.GetSelectedDataRow();
            int employeeID = 0;
            string employeeName = "";
            string employeeCode = "";
            string employeePhoneNumber = "";
            if (dataRow != null)
            {
                if (IsDuplicateRecordAttachedGoodsr(TextUtils.ToInt(lookUpEdit.EditValue)))
                {
                    MessageBox.Show("Người đi này đã tồn tại trong danh sách. Vui lòng người khác!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    employeeID = TextUtils.ToInt(lookUpEdit.EditValue);
                    employeeName = dataRow.Row.Field<string>("FullName");
                    employeeCode = dataRow.Row.Field<string>("Code");
                    employeePhoneNumber = dataRow.Row.Field<string>("SDTCaNhan");
                }
            }
            grvAttachedGoods.SetFocusedRowCellValue(colAttachedGoodsEmployeeID, employeeID);
            grvAttachedGoods.SetFocusedRowCellValue(colAttachedGoodsName, employeeName);
            grvAttachedGoods.SetFocusedRowCellValue(colAttachedGoodsCode, employeeCode);
            grvAttachedGoods.SetFocusedRowCellValue(colAttachedGoodsPhoneNumber, employeePhoneNumber);
        }

        private void cboBookerVehicles_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit lookUpEdit = (SearchLookUpEdit)sender;
            DataRowView dataRow = (DataRowView)lookUpEdit.GetSelectedDataRow();
            string phoneNumber = "";
            if (dataRow != null)
            {
                phoneNumber = dataRow.Row.Field<string>("SDTCaNhan");

            }
            txtPhoneNumber.Text = phoneNumber;
        }
        private void grvPassenger_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (grvPassenger.FocusedColumn == colPassengerSTT)
            {
                grvPassenger.BeginUpdate();
                e.Valid = false;
                e.ErrorText = "Vui lòng không sửa STT!";
                grvPassenger.EndUpdate();
            }
            if (grvPassenger.FocusedColumn == colPassengerDepartment || grvPassenger.FocusedColumn == colPassengerName)
            {
                int id = TextUtils.ToInt(grvPassenger.GetFocusedRowCellValue(colPassengerEmployeeID));
                if (id > 0)
                {
                    grvPassenger.BeginUpdate();
                    e.ErrorText = "Thông tin này được lấy ra từ nhân viên, không được sửa!";
                    e.Valid = false;
                    grvPassenger.EndUpdate();
                }

            }
            if (grvPassenger.FocusedColumn == colPassengerPhoneNumber)
            {
                string sdtCanhan = e.Value as string;
                if (!Regex.IsMatch(sdtCanhan, @"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$"))
                {
                    grvPassenger.BeginUpdate();
                    e.Valid = false;
                    e.ErrorText = "Số điện thoại không đúng định dạng!";
                    grvPassenger.EndUpdate();
                }
            }
        }
        private void grvAttachedGoods_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (grvAttachedGoods.FocusedColumn == colAttachedGoodsSTT)
            {
                grvAttachedGoods.BeginUpdate();
                e.Valid = false;
                e.ErrorText = "Vui lòng không sửa STT!";
                grvAttachedGoods.EndUpdate();
            }
            if (grvAttachedGoods.FocusedColumn == colAttachedGoodsName)
            {
                int id = TextUtils.ToInt(grvAttachedGoods.GetFocusedRowCellValue(colAttachedGoodsEmployeeID));
                if (id > 0)
                {
                    grvAttachedGoods.BeginUpdate();
                    e.ErrorText = "Thông tin này được lấy ra từ nhân viên, không được sửa!";
                    e.Valid = false;
                    grvAttachedGoods.EndUpdate();
                }

            }
            if (grvAttachedGoods.FocusedColumn == colAttachedGoodsPhoneNumber)
            {
                string sdtCanhan = e.Value as string;

                if (!Regex.IsMatch(sdtCanhan, @"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$"))
                {
                    grvAttachedGoods.BeginUpdate();
                    e.Valid = false;
                    e.ErrorText = "Số điện thoại không đúng định dạng!";
                    grvAttachedGoods.EndUpdate();
                }
            }
        }
        private bool IsDuplicateRecordPassenger(int employeeID)
        {
            for (int i = 0; i < grvPassenger.RowCount; i++)
            {
                if (TextUtils.ToInt(grvPassenger.GetRowCellValue(i, colPassengerEmployeeID)) == employeeID)
                {
                    return true;
                }
            }
            return false;
        }
        private bool IsDuplicateRecordAttachedGoodsr(int employeeID)
        {
            for (int i = 0; i < grvAttachedGoods.RowCount; i++)
            {
                if (TextUtils.ToInt(grvAttachedGoods.GetRowCellValue(i, colAttachedGoodsEmployeeID)) == employeeID)
                {
                    return true;
                }
            }
            return false;
        }

        private void btnUploadFile_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvAttachedGoods.FocusedRowHandle;
            int stt = TextUtils.ToInt(grvAttachedGoods.GetFocusedRowCellValue(colSTT));
            frmUploadFileImages frm = new frmUploadFileImages();
            frm.stt = stt;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                grvAttachedGoods.FocusedRowHandle = focusedRowHandle;
            }
        }

        //LinhTN update 22/05/2024
        private void cboDepartureAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDepartureAddress.SelectedIndex == 4)
            {
                txtDepartureAddress.Text = "";
                txtDepartureAddress.Enabled = true;
            }
            else
            {
                txtDepartureAddress.Text = cboDepartureAddress.Text;
                txtDepartureAddress.Enabled = false;
            }
        }
    }
}