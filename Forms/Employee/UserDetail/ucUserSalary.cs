using BMS;
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

namespace Forms.Employee
{
    public partial class ucUserSalary : UserControl
    {

        public ucUserSalary()
        {
            InitializeComponent();
        }

        private void ucUserSalary_Load(object sender, EventArgs e)
        {
            loadEmployee();
        }

        void loadEmployee()
        {
            DataTable dt = TextUtils.Select("select ID, Code, FullName from Employee");

            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;
        }
        public bool save(ref EmployeeModel Usermodel)
        {
            //if (!validate()) return false;
            Usermodel.SoSoBHXH = txtSoSo.Text;
            Usermodel.NguoiGiuSoBHXH = TextUtils.ToInt(cboEmployee.EditValue);
            Usermodel.NgayBatDauBHXHCty = TextUtils.ToDate4(dtpNgayBatDauCTY.EditValue);
            Usermodel.MucDongBHXHHienTai = TextUtils.ToDecimal(txtMucDong.EditValue);

            Usermodel.LuongThuViec = TextUtils.ToDecimal(txtLTV.EditValue);
            Usermodel.LuongCoBan = TextUtils.ToDecimal(txtLCB.EditValue);
            Usermodel.LuongCoBan = TextUtils.ToDecimal(txtLCB.EditValue);
            Usermodel.AnCa = TextUtils.ToDecimal(txtEat.EditValue);
            Usermodel.XangXe = TextUtils.ToDecimal(txtGas.EditValue);
            Usermodel.DienThoai = TextUtils.ToDecimal(txtPhoneNumber.EditValue);
            Usermodel.NhaO = TextUtils.ToDecimal(txtHouse.EditValue);
            Usermodel.TrangPhuc = TextUtils.ToDecimal(txtSkin.EditValue);
            Usermodel.ChuyenCan = TextUtils.ToDecimal(txtChuyenCan.EditValue);
            Usermodel.Khac = TextUtils.ToDecimal(txtKhac.EditValue);
            Usermodel.TongPhuCap = TextUtils.ToDecimal(txtTotalPhuCap.EditValue);
            Usermodel.TongLuong = TextUtils.ToDecimal(txtTotalSalary.EditValue);

            Usermodel.GiamTruBanThan = TextUtils.ToDecimal(txtGiamTruBanThan.Text);
            Usermodel.SoNguoiPT = TextUtils.ToInt(txtGiaCanhPT.Text);
            Usermodel.TongTien = TextUtils.ToDecimal(txtTonggiamtru.EditValue);
            Usermodel.MST = TextUtils.ToString(txtMSTCaNhan.Text);
            Usermodel.STKChuyenLuong = TextUtils.ToString(txtSTK.Text);
            return true;
        }
        public void loadData(ref EmployeeModel model)
        {
            DateTime? date = null;
            if (model.ID > 0)
            {
                txtSoSo.Text = model.SoSoBHXH;
                cboEmployee.EditValue = model.NguoiGiuSoBHXH;
                dtpNgayBatDauCTY.EditValue = model.NgayBatDauBHXHCty.HasValue ? model.NgayBatDauBHXHCty.Value : date;
                //dtpStartDateHĐ.EditValue = model.NgayBatDauBHXH.HasValue ? model.NgayBatDauBHXH.Value : date;
                //dtpEndDateHĐ.EditValue = model.NgayKetThucBHXH.HasValue ? model.NgayKetThucBHXH.Value : date;
                txtMucDong.EditValue = model.MucDongBHXHHienTai;
                txtLTV.EditValue = model.LuongThuViec;
                txtLCB.EditValue = model.LuongCoBan;
                txtEat.EditValue = model.AnCa;
                txtGas.EditValue = model.XangXe;
                txtPhoneNumber.EditValue = model.DienThoai;
                txtHouse.EditValue = model.NhaO;
                txtSkin.EditValue = model.TrangPhuc;
                txtChuyenCan.EditValue = model.ChuyenCan;
                txtKhac.EditValue = model.Khac;
                txtTotalPhuCap.EditValue = model.TongPhuCap;
                txtTotalSalary.EditValue = model.TongLuong;
                txtGiaCanhPT.Text = TextUtils.ToString(model.SoNguoiPT);
                txtGiamTruBanThan.Text = string.Format("{0:0.##}", model.GiamTruBanThan);
                txtTonggiamtru.EditValue = model.TongTien;
                txtMSTCaNhan.Text = TextUtils.ToString(model.MST);
                txtSTK.Text = TextUtils.ToString(model.STKChuyenLuong);
            }
        }

        bool validate()
        {
            //BHXH
            if (string.IsNullOrEmpty(txtSoSo.Text))
            {
                MessageBox.Show("Vui lòng nhập Số sổ BHXH", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (string.IsNullOrEmpty(TextUtils.ToString(cboEmployee.EditValue)))
            {
                MessageBox.Show("Vui lòng chọn Người giữ sổ BHXH", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(dtpNgayBatDauCTY.Text))
            {
                MessageBox.Show("Vui lòng nhập ngày bắt đầu đóng BHXH tại công ty", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(dtpNgayBatDauCTY.Text))
            {
                MessageBox.Show("Vui lòng nhập Mức đóng hiện tại", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            //Khác
            if (string.IsNullOrEmpty(txtGiamTruBanThan.Text))
            {
                MessageBox.Show("Vui lòng nhập Giảm trừ bản thân", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(txtGiaCanhPT.Text))
            {
                MessageBox.Show("Vui lòng nhập Gia cảnh phụ thuộc", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(txtTonggiamtru.Text))
            {
                MessageBox.Show("Vui lòng nhập Tổng giảm trừ", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(txtMSTCaNhan.Text))
            {
                MessageBox.Show("Vui lòng nhập MST cá nhân", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(txtSTK.Text))
            {
                MessageBox.Show("Vui lòng nhập STK chuyển lương", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            //Lương
            if (string.IsNullOrEmpty(txtLTV.Text))
            {
                MessageBox.Show("Vui lòng nhập Lương thử việc", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(txtLCB.Text))
            {
                MessageBox.Show("Vui lòng nhập Lương cơ bản", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(txtEat.Text))
            {
                MessageBox.Show("Vui lòng nhập Phụ cấp ăn ca", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(txtGas.Text))
            {
                MessageBox.Show("Vui lòng nhập Phụ cấp xăng xe", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(txtPhoneNumber.Text))
            {
                MessageBox.Show("Vui lòng nhập Phụ cấp điện thoại", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(txtHouse.Text))
            {
                MessageBox.Show("Vui lòng nhập Phụ cấp nhà ở", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(txtSkin.Text))
            {
                MessageBox.Show("Vui lòng nhập Phụ cấp trang phục", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(txtChuyenCan.Text))
            {
                MessageBox.Show("Vui lòng nhập Phụ cấp chuyên cần", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(txtKhac.Text))
            {
                MessageBox.Show("Vui lòng nhập Phụ cấp khác", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(txtTotalPhuCap.Text))
            {
                MessageBox.Show("Vui lòng nhập Tổng phụ cấp", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(txtTotalSalary.Text))
            {
                MessageBox.Show("Vui lòng nhập Tổng lương", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }

        private void txtSoSo_TextChanged(object sender, EventArgs e)
        {
            txtSoSo.Invoke(new Action(() => txtSoSo.Text = txtSoSo.Text.Trim()));
        }

        private void dtpStartDateHĐ_ValueChanged(object sender, EventArgs e)
        {
            //if (dtpStartDateHĐ.Value > dtpEndDateHĐ.Value)
            //{
            //    MessageBox.Show("Ngày không hợp lệ");
            //    dtpStartDateHĐ.Text = "";
            //    return;
            //}    
        }

        private void dtpEndDateHĐ_ValueChanged(object sender, EventArgs e)
        {
            //if (dtpStartDateHĐ.Value > dtpEndDateHĐ.Value)
            //{
            //    MessageBox.Show("Ngày không hợp lệ");
            //    dtpEndDateHĐ.Value = "";
            //    return;
            //}
        }

        private void txtLTV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtMucDong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
