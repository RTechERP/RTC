using BMS.Business;
using BMS.Model;
using BMS.Utils;
using ExcelDataReader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BMS
{
    public partial class frmSupplierSaleDetail : _Forms
    {
        public SupplierSaleModel supplier = new SupplierSaleModel();

        public frmSupplierSaleDetail()
        {
            InitializeComponent();
        }
        /// <summary>
        /// load dữ liệu lên khi load form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSupplierSaleDetail_Load(object sender, EventArgs e)
        {
            LoadEmployee();
            LoadRulePay();
            loadSupplierSaleDetail();
            //label22.Visible = label

        }
        private void loadSupplierSaleDetail()
        {
            txtMaNCC.Text = supplier.CodeNCC;
            txtTenNCC.Text = supplier.NameNCC;
            txtTenTiengAnh.Text = supplier.TenTiengAnh;
            txtAddressNCC.Text = supplier.AddressNCC;
            txtNVPhuTrach.Text = supplier.NVPhuTrach;
            txtLoaiHangHoa.Text = supplier.LoaiHangHoa;
            txtBrand.Text = supplier.Brand;
            txtMaNhom.Text = supplier.MaNhom;
            txtPhoneNCC.Text = supplier.PhoneNCC;
            txtMaSoThue.Text = supplier.MaSoThue;
            txtWebsite.Text = supplier.Website;
            txtOrdererNCC.Text = supplier.OrdererNCC;
            txtDebt.Text = supplier.Debt;
            txtSoTK.Text = supplier.SoTK;
            txtNganHang.Text = supplier.NganHang;
            txtNote.Text = supplier.Note;

            cboCompany.SelectedIndex = supplier.Company;
            txtShortNameSupplier.Text = supplier.ShortNameSupplier;
            dtpNgayUpdate.Value = supplier.NgayUpdate.HasValue ? supplier.NgayUpdate.Value : DateTime.Now;

            cboRulePay.EditValue = supplier.RulePayID;
            chkIsDebt.Checked = supplier.IsDebt;
            txtFedexAccount.Text = supplier.FedexAccount;
            txtOriginItem.Text = supplier.OriginItem;
            txtBankCharge.Text = supplier.BankCharge;
            txtAddressDelivery.Text = supplier.AddressDelivery;
            txtDescription.Text = supplier.Description;
            txtRuleIncoterm.Text = supplier.RuleIncoterm;

            if (supplier.ID > 0)
            {
                cboEmployee.EditValue = supplier.EmployeeID;
            }

            DataTable dt = TextUtils.Select($"SELECT * FROM dbo.SupplierSaleContact WHERE SupplierID = {supplier.ID} AND SupplierID <> 0");
            //var exp1 = new Expression("SupplierID", supplier.ID);
            //var exp2 = new Expression("SupplierID", 0,"<>");
            //List<SupplierSaleContactModel> listContact = SQLHelper<SupplierSaleContactModel>.FindByExpression(exp1.And(exp2));
            grdData.DataSource = dt;
        }

        void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;

            cboEmployee.EditValue = Global.EmployeeID;
        }

        void LoadRulePay()
        {
            List<RulePayModel> list = SQLHelper<RulePayModel>.FindAll();
            cboRulePay.Properties.ValueMember = "ID";
            cboRulePay.Properties.DisplayMember = "Note";
            cboRulePay.Properties.DataSource = list;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())this.DialogResult = DialogResult.OK;
        }

        bool SaveData()
        {
            grvData.FocusedRowHandle = -1;
            if (!ValidateForm()) return false;
            supplier.NgayUpdate = DateTime.Now;
            supplier.LoaiHangHoa = txtLoaiHangHoa.Text.Trim();
            //supplier.NVPhuTrach = txtNVPhuTrach.Text.Trim();
            supplier.NVPhuTrach = cboEmployee.Text;
            supplier.Brand = txtBrand.Text.Trim();
            supplier.MaNhom = txtMaNhom.Text.Trim();
            supplier.CodeNCC = txtMaNCC.Text.Trim();
            supplier.NameNCC = txtTenNCC.Text.Trim();
            supplier.TenTiengAnh = txtTenTiengAnh.Text.Trim();
            supplier.AddressNCC = txtAddressNCC.Text.Trim();
            supplier.MaSoThue = txtMaSoThue.Text.Trim();
            supplier.Website = txtWebsite.Text.Trim();
            supplier.Debt = txtDebt.Text.Trim();
            supplier.SoTK = txtSoTK.Text.Trim();
            supplier.NganHang = txtNganHang.Text.Trim();
            supplier.Note = txtNote.Text.Trim();
            supplier.PhoneNCC = txtPhoneNCC.Text.Trim();
            supplier.OrdererNCC = txtOrdererNCC.Text.Trim();

            supplier.ShortNameSupplier = txtShortNameSupplier.Text.Trim();
            supplier.Company = cboCompany.SelectedIndex;
            supplier.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
            supplier.NgayUpdate = dtpNgayUpdate.Value;

            supplier.RulePayID = TextUtils.ToInt(cboRulePay.EditValue);
            supplier.IsDebt = chkIsDebt.Checked;
            supplier.FedexAccount = txtFedexAccount.Text.Trim();
            supplier.OriginItem = txtOriginItem.Text.Trim();
            supplier.BankCharge = txtBankCharge.Text.Trim();
            supplier.AddressDelivery = txtAddressDelivery.Text.Trim();
            supplier.Description = txtDescription.Text.Trim();
            supplier.RuleIncoterm = txtRuleIncoterm.Text.Trim();

            if (supplier.ID > 0)
            {
                SupplierSaleBO.Instance.Update(supplier);
            }
            else
            {
                supplier.ID = (int)SupplierSaleBO.Instance.Insert(supplier);
            }
            int count = grvData.RowCount;
            for (int i = 0; i < count; i++)
            {
                SupplierSaleContactModel detail = new SupplierSaleContactModel();
                int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                if (id > 0)
                {
                    detail = (SupplierSaleContactModel)SupplierSaleContactBO.Instance.FindByPK(id);
                }

                detail.ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                detail.SupplierID = supplier.ID;
                detail.SupplierName = TextUtils.ToString(grvData.GetRowCellValue(i, colSupplierName));
                detail.SupplierPhone = TextUtils.ToString(grvData.GetRowCellValue(i, colSupplierPhone));
                detail.SupplierEmail = TextUtils.ToString(grvData.GetRowCellValue(i, colSupplierEmail));
                detail.Describe = TextUtils.ToString(grvData.GetRowCellValue(i, colDescribe));

                if (id == 0)
                {
                    SupplierSaleContactBO.Instance.Insert(detail);
                }
                else
                {
                    SupplierSaleContactBO.Instance.Update(detail);
                }
            }

            return true;
        }
        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            SaveData();
            txtMaNCC.Clear();
            txtTenNCC.Clear();
            txtOrdererNCC.Clear();
            txtAddressNCC.Clear();
            txtPhoneNCC.Clear();
            supplier = new SupplierSaleModel();
        }

        private void frmProductKHACHHANG_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveData();
            if (this.DialogResult == DialogResult.OK)
            {
                loadSupplierSaleDetail();
            }
        }
        /// <summary>
        /// check lỗi
        /// </summary>
        /// <returns></returns>
        /// 

        bool ValidateForm()
        {
            //string patternCode = @"^[a-zA-Z0-9_-]+$";
            string patternCode = @"^[^àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴ]+$";
            string patternPhone = @"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$";
            Regex regexCode = new Regex(patternCode);
            Regex regexPhone = new Regex(patternPhone);

            //if (TextUtils.ToString(this.Tag).ToLower() == "demo")
            //{
            //    if (string.IsNullOrEmpty(txtMaNCC.Text.Trim()))
            //    {
            //        MessageBox.Show("Vui lòng nhập Mã NCC!", "Thông báo");
            //        return false;
            //    }
            //    else
            //    {
            //        bool isCheck = regexCode.IsMatch(txtMaNCC.Text.Trim());
            //        if (!isCheck)
            //        {
            //            MessageBox.Show("Mã NCC chỉ chứa chữ cái tiếng Anh và số!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //            return false;
            //        }

            //        var exp1 = new Expression("CodeNCC", txtMaNCC.Text.Trim());
            //        var exp2 = new Expression("ID", supplier.ID, "<>");
            //        var suppliers = SQLHelper<SupplierSaleModel>.FindByExpression(exp1.And(exp2));
            //        if (suppliers.Count > 0)
            //        {
            //            MessageBox.Show($"Mã NCC [{txtMaNCC.Text}] đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //            return false;
            //        }
            //    }

            //    if (string.IsNullOrEmpty(txtTenNCC.Text.Trim()))
            //    {
            //        MessageBox.Show("Vui lòng nhập Tên NCC!", "Thông báo");
            //        return false;
            //    }

            //    return true;
            //}
            //else
            {
                if (cboCompany.SelectedIndex <= 0)
                {
                    MessageBox.Show("Vui lòng nhập Công ty nhập!", "Thông báo");
                    return false;
                }

                if (string.IsNullOrEmpty(txtMaNCC.Text.Trim()))
                {
                    MessageBox.Show("Vui lòng nhập Mã NCC!", "Thông báo");
                    return false;
                }
                else
                {
                    bool isCheck = regexCode.IsMatch(txtMaNCC.Text.Trim());
                    if (!isCheck)
                    {
                        MessageBox.Show("Mã NCC chỉ chứa chữ cái tiếng Anh và số!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }

                    var exp1 = new Expression("CodeNCC", txtMaNCC.Text.Trim());
                    var exp2 = new Expression("ID", supplier.ID, "<>");
                    var suppliers = SQLHelper<SupplierSaleModel>.FindByExpression(exp1.And(exp2));
                    if (suppliers.Count > 0)
                    {
                        MessageBox.Show($"Mã NCC [{txtMaNCC.Text}] đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }
                }

                if (string.IsNullOrEmpty(txtTenNCC.Text.Trim()))
                {
                    MessageBox.Show("Vui lòng nhập Tên NCC!", "Thông báo");
                    return false;
                }

                //if (string.IsNullOrEmpty(txtPhoneNCC.Text.Trim()))
                //{
                //    MessageBox.Show("Vui lòng nhập Số điện thoại!", "Thông báo");
                //    return false;
                //}
                //else
                //{
                //    bool isCheck = regexPhone.IsMatch(txtPhoneNCC.Text.Trim());
                //    if (!isCheck)
                //    {
                //        MessageBox.Show("Số điện thoại không hợp lệ.\nVui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //        return false;
                //    }
                //}

                //if (string.IsNullOrEmpty(txtSoTK.Text.Trim()))
                //{
                //    MessageBox.Show("Vui lòng nhập Số tài khoản!", "Thông báo");
                //    return false;
                //}

                if (string.IsNullOrEmpty(txtAddressNCC.Text.Trim()))
                {
                    MessageBox.Show("Vui lòng nhập Địa chỉ!", "Thông báo");
                    return false;
                }

                if (string.IsNullOrEmpty(txtNganHang.Text.Trim()))
                {
                    MessageBox.Show("Vui lòng nhập Ngân hàng!", "Thông báo");
                    return false;
                }
                return true;
            }
        }

        private void txtPhoneNCC_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch))
            {
                e.Handled = true;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void txtMaNCC_TextChanged(object sender, EventArgs e)
        {
            if (supplier.ID <= 0)
            {
                txtShortNameSupplier.Text = txtMaNCC.Text.Trim();
            }
        }

        private void frmSupplierSaleDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
