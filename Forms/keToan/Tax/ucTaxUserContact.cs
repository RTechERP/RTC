using System;
using BMS.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMS;
using System.Text.RegularExpressions;

namespace Forms.keToan.Tax.TaxUserDetail
{
    public partial class ucTaxUserContact : UserControl
    {
        public UsersModel usersModel;

        string addressOften = "";
        string addressTemporary = "";
        public ucTaxUserContact()
        {
            InitializeComponent();
        }

        private void ucTaxUserContact_Load(object sender, EventArgs e)
        {
            lblDcThuongTru.Text = "Đ/c Tạm trú \n(Đ/c hiện tại sinh sống)";
        }

        public void loadData(ref TaxEmployeeModel usersModel)
        {

            txtSoCMNDorCCCD.Text = TextUtils.ToString(usersModel.CMTND);
            dtpNgayCap.EditValue = usersModel.NgayCap;
            txtNoiCap.Text = TextUtils.ToString(usersModel.NoiCap);

            txtSDTCaNhan.Text = TextUtils.ToString(usersModel.SDTCaNhan);
            txtSDTCongTy.Text = TextUtils.ToString(usersModel.SDTCongTy);


            txtEmailCaNhan.Text = TextUtils.ToString(usersModel.EmailCaNhan);
            txtEmailCongTy.Text = TextUtils.ToString(usersModel.EmailCongTy);
            txtHoTenNguoiLienHe.Text = TextUtils.ToString(usersModel.NguoiLienHeKhiCan);
            txtMoiQuanHe.Text = TextUtils.ToString(usersModel.MoiQuanHe);
            txtSDTNguoiThan.Text = TextUtils.ToString(usersModel.SDTNguoiThan);

            txtNumberHouse_ThuongTru.Text = usersModel.SoNhaDcThuongTru;
            txtStreet_ThuongTru.Text = usersModel.DuongDcThuongTru;
            txtWard_ThuongTru.Text = usersModel.PhuongDcThuongTru;
            txtDistrict_ThuongTru.Text = usersModel.QuanDcThuongTru;
            txtCity_ThuongTru.Text = usersModel.TinhDcThuongTru;

            txtNumberHouse_TamTru.Text = usersModel.SoNhaDcTamTru;
            txtStreet_TamTru.Text = usersModel.DuongDcTamTru;
            txtWard_TamTru.Text = usersModel.PhuongDcTamTru;
            txtDistrict_TamTru.Text = usersModel.QuanDcTamTru;
            txtCity_TamTru.Text = usersModel.TinhDcTamTru;

            txtDcThuongTru.Text = usersModel.DcThuongTru;
            txtDcTamTru.Text = usersModel.DcTamTru;

            addressOften = usersModel.DcThuongTru;
            addressTemporary = usersModel.DcTamTru;

        }
        static bool isEmail(string inputEmail)
        {
            inputEmail = inputEmail ?? string.Empty;
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return true;
            else
                return false;
        }
        bool validate()
        {
            string regexPhone = "^\\+?\\d{1,4}?[-.\\s]?\\(?\\d{1,3}?\\)?[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,9}$";
            Regex regex = new Regex(regexPhone);
            //CCCD
            if (string.IsNullOrEmpty(txtSoCMNDorCCCD.Text))
            {
                MessageBox.Show("Vui lòng nhập Số CMND/CCCD", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (string.IsNullOrEmpty(dtpNgayCap.Text))
            {
                MessageBox.Show("Vui lòng nhập Ngày cấp CMND/CCCD", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(txtNoiCap.Text))
            {
                MessageBox.Show("Vui lòng nhập Ngày cấp CMND/CCCD", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            //Thông tin liên hệ
            if (string.IsNullOrEmpty(txtSDTCaNhan.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Số điện thoại cá nhân!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else if (!regex.IsMatch(txtSDTCaNhan.Text.Trim()))
            {
                MessageBox.Show("Số điện thoại cá nhân không hợp lệ", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (!isEmail(txtEmailCaNhan.Text.Trim()))
            {
                MessageBox.Show("Email cá nhân không hợp lệ", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (string.IsNullOrEmpty(txtHoTenNguoiLienHe.Text))
            {
                MessageBox.Show("Vui lòng nhập Họ tên người thân", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(txtMoiQuanHe.Text))
            {
                MessageBox.Show("Vui lòng nhập Mối quan hệ với người thân", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (string.IsNullOrEmpty(txtSDTNguoiThan.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Số điện thoại người thân!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else if (!regex.IsMatch(txtSDTNguoiThan.Text.Trim()))
            {
                MessageBox.Show("Số điện thoại người thân không hợp lệ!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            //ĐC thường trú
            if (string.IsNullOrEmpty(txtDcThuongTru.Text.Trim()))
            {
                if (string.IsNullOrEmpty(txtWard_ThuongTru.Text))
                {
                    MessageBox.Show("Vui lòng nhập Phường / Xã thường trú", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }

                if (string.IsNullOrEmpty(txtDistrict_ThuongTru.Text))
                {
                    MessageBox.Show("Vui lòng nhập Quận / Huyện thường trú", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
                if (string.IsNullOrEmpty(txtCity_ThuongTru.Text))
                {
                    MessageBox.Show("Vui lòng nhập Tỉnh / Thành phố thường trú", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
                if (string.IsNullOrEmpty(txtDcThuongTru.Text))
                {
                    MessageBox.Show("Vui lòng nhập Địa chỉ thường trú", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }

            //ĐC Tạm trú
            if (string.IsNullOrEmpty(txtDcTamTru.Text.Trim()))
            {
                if (string.IsNullOrEmpty(txtNumberHouse_TamTru.Text))
                {
                    MessageBox.Show("Vui lòng nhập Số nhà / Số phòng tạm trú", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
                if (string.IsNullOrEmpty(txtStreet_TamTru.Text))
                {
                    MessageBox.Show("Vui lòng nhập Đường / Thôn / Tổ dân phố tạm trú", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
                if (string.IsNullOrEmpty(txtWard_TamTru.Text))
                {
                    MessageBox.Show("Vui lòng nhập Phường / Xã tạm trú", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }

                if (string.IsNullOrEmpty(txtDistrict_TamTru.Text))
                {
                    MessageBox.Show("Vui lòng nhập Quận / Huyện tạm trú", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
                if (string.IsNullOrEmpty(txtCity_TamTru.Text))
                {
                    MessageBox.Show("Vui lòng nhập Tỉnh / Thành phố tạm trú", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
                if (string.IsNullOrEmpty(txtDcTamTru.Text))
                {
                    MessageBox.Show("Vui lòng nhập Địa chỉ tạm trú", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }

            return true;
        }

        public bool save(ref TaxEmployeeModel model)
        {
            if (!validate()) return false;

            model.CMTND = txtSoCMNDorCCCD.Text;
            model.NgayCap = TextUtils.ToDate(dtpNgayCap.Text);
            model.NoiCap = txtNoiCap.Text.Trim();
            model.DcThuongTru = txtDcThuongTru.Text.Trim();
            model.DcTamTru = txtDcTamTru.Text.Trim();
            model.SDTCaNhan = txtSDTCaNhan.Text.Trim();
            model.SDTCongTy = txtSDTCongTy.Text.Trim();
            model.SDTNguoiThan = txtSDTNguoiThan.Text.Trim();
            model.EmailCaNhan = txtEmailCaNhan.Text.Trim();
            model.EmailCongTy = txtEmailCongTy.Text.Trim();
            model.NguoiLienHeKhiCan = txtHoTenNguoiLienHe.Text.Trim();
            model.MoiQuanHe = txtMoiQuanHe.Text.Trim();

            model.SoNhaDcTamTru = txtNumberHouse_TamTru.Text.Trim();
            model.DuongDcTamTru = txtStreet_TamTru.Text.Trim();
            model.PhuongDcTamTru = txtWard_TamTru.Text.Trim();
            model.QuanDcTamTru = txtDistrict_TamTru.Text.Trim();
            model.TinhDcTamTru = txtCity_TamTru.Text.Trim();

            model.SoNhaDcThuongTru = txtNumberHouse_ThuongTru.Text.Trim();
            model.DuongDcThuongTru = txtStreet_ThuongTru.Text.Trim();
            model.PhuongDcThuongTru = txtWard_ThuongTru.Text.Trim();
            model.QuanDcThuongTru = txtDistrict_ThuongTru.Text.Trim();
            model.TinhDcThuongTru = txtCity_ThuongTru.Text.Trim();

            return true;
        }


        void InputAddress()
        {
            var controlOften = groupBox1.Controls.OfType<TextBox>().OrderBy(x => x.TabIndex).ToList();
            controlOften.RemoveAt(controlOften.Count - 1);

            var controlTemporary = groupBox2.Controls.OfType<TextBox>().OrderBy(x => x.TabIndex).ToList();
            controlTemporary.RemoveAt(controlTemporary.Count - 1);

            List<string> addressOftenDetail = new List<string>();
            List<string> addressTemporaryDetail = new List<string>();

            if (TextUtils.ToString(addressOften).Contains(","))
            {
                addressOftenDetail = addressOften.Split(',').ToList();
            }

            if (TextUtils.ToString(addressTemporary).Contains(","))
            {
                addressTemporaryDetail = addressTemporary.Split(',').ToList();
            }

            for (int i = addressOftenDetail.Count; i < 5; i++)
            {
                addressOftenDetail.Insert(0, "");
            }

            for (int i = addressTemporaryDetail.Count; i < 5; i++)
            {
                addressTemporaryDetail.Insert(0, "");
            }

            //Update địa chỉ thường trú
            for (int i = 0; i < controlOften.Count(); i++)
            {
                string value = controlOften[i].Text.Trim();
                if (!string.IsNullOrEmpty(value))
                {
                    for (int j = 0; j < addressOftenDetail.Count; j++)
                    {
                        if (i == j)
                        {
                            addressOftenDetail[j] = value;
                        }
                    }
                }
            }

            //Update địa chỉ tạm trú
            for (int i = 0; i < controlTemporary.Count(); i++)
            {
                string value = controlTemporary[i].Text.Trim();
                if (!string.IsNullOrEmpty(value))
                {
                    for (int j = 0; j < addressTemporaryDetail.Count; j++)
                    {
                        if (i == j)
                        {
                            addressTemporaryDetail[j] = value;
                        }
                    }
                }

            }

            txtDcThuongTru.Text = string.Join(",", addressOftenDetail);
            txtDcTamTru.Text = string.Join(",", addressTemporaryDetail);
        }

        private void txtNumberHouse_ThuongTru_TextChanged(object sender, EventArgs e)
        {
            InputAddress();
        }

        private void txtStreet_ThuongTru_TextChanged(object sender, EventArgs e)
        {
            InputAddress();
        }

        private void txtWard_ThuongTru_TextChanged(object sender, EventArgs e)
        {
            InputAddress();
        }

        private void txtDistrict_ThuongTru_TextChanged(object sender, EventArgs e)
        {
            InputAddress();
        }

        private void txtCity_ThuongTru_TextChanged(object sender, EventArgs e)
        {
            InputAddress();
        }

        private void txtNumberHouse_TamTru_TextChanged(object sender, EventArgs e)
        {
            InputAddress();
        }

        private void txtStreet_TamTru_TextChanged(object sender, EventArgs e)
        {
            InputAddress();
        }

        private void txtWard_TamTru_TextChanged(object sender, EventArgs e)
        {
            InputAddress();
        }

        private void txtDistrict_TamTru_TextChanged(object sender, EventArgs e)
        {
            InputAddress();
        }

        private void txtCity_TamTru_TextChanged(object sender, EventArgs e)
        {
            InputAddress();
        }
    }
}
