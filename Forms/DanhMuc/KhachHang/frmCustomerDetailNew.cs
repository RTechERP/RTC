using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmCustomerDetailNew : _Forms
    {
        int warehouseID = 0;
        public CustomerModel customer = new CustomerModel();
        private static string json = File.ReadAllText(@"jsonProvinces.txt");
        private static List<Provinces> provinces = JsonConvert.DeserializeObject<List<Provinces>>(json);
        private ToolTip toolTipValidateCode;

        ArrayList lsCustomerIDDelete = new ArrayList();
        ArrayList lsAddressDelete = new ArrayList();
        ArrayList lsEmployeeSaleDelete = new ArrayList();
        public frmCustomerDetailNew(int warehouseID)
        {

            InitializeComponent();
            this.warehouseID = warehouseID;
        }

        public class Provinces
        {
            public int STT { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }
        }
        private void frmCustomerDetailNew_Load(object sender, EventArgs e)
        {
            LoadData();
            //textBox1.AutoCompleteSource = new AutoCompleteSource.FileSystem;
        }

        private void LoadData()
        {
            LoadProvinces();
            LoadDataCustomerMajor();
            LoadBusinessField();
            LoadEmployeeSale();
            LoadEmployee();
            loadCustomerDetail();

            toolTipValidateCode = new ToolTip();
            toolTipValidateCode.SetToolTip(label23, "Vui lòng chỉ nhập chữ cái tiếng Anh và số!");
            toolTipValidateCode.SetToolTip(label24, "Vui lòng chỉ nhập chữ cái tiếng Anh và số!");

            //bool isAdmin = (!Global.IsAdmin && !Global.IsAdminSale && Global.UserID != 1177 && Global.UserID != 1313 && Global.UserID != 23 && Global.UserID != 1380);
            //btnSave.Enabled = btnSaveNew.Enabled = !isAdmin;

            //btnSave.Enabled = btnSaveNew.Enabled = IsPermission();

        }

        bool isShowCustomHeaderButtons()
        {
            int[] idAdminSale = new int[] { 1177, 1313, 23, 1380, 88, 15 };

            //Check là admin sale
            UsersModel users = SQLHelper<UsersModel>.FindByID(Global.UserID);
            if (customer.ID <= 0) return true;

            if (Global.IsAdmin) return true;
            else if (Global.IsAdminSale) return true;
            else if (idAdminSale.Contains(users.ID)) return true;
            else
            {
                return false;
            }
        }

        bool IsPermission()
        {
            int[] idAdminSale = new int[] { 1177, 1313, 23, 1380, 88, 15 };
            int[] idPM = new int[] { 12, 42, 1381 };

            //Check là admin sale
            UsersModel users = SQLHelper<UsersModel>.FindByID(Global.UserID);
            if (customer.ID <= 0) return true;


            if (Global.IsAdmin) return true;
            else if (Global.IsAdminSale) return true;
            else if (idAdminSale.Contains(users.ID)) return true;
            else if (idPM.Contains(users.ID)) return true;
            else
            {
                //Get danh sách sale phụ trách
                var exp1 = new Expression("CustomerID", customer.ID);
                var exp2 = new Expression("EmployeeID", Global.EmployeeID);
                var cusEmp = SQLHelper<CustomerEmployeeModel>.FindByExpression(exp1.And(exp2));

                // Lh Phuc chỉnh sửa
                // check quyền xóa các nút
                groupControl1.CustomHeaderButtons[0].Properties.Enabled = false;
                groupControl2.CustomHeaderButtons[0].Properties.Enabled = false;
                groupControl3.CustomHeaderButtons[0].Properties.Enabled = false;

                //isPermission = cusEmp.Count > 0;
                return cusEmp.Count > 0;
            }
        }


        private void cboProvinces_EditValueChanged(object sender, EventArgs e)
        {
            Provinces provinces = (Provinces)cboProvinces.GetSelectedDataRow();
            if (provinces == null) return;
            txtCodeProvinces.Text = provinces.Code;
        }


        private bool SaveData()
        {

            grvData.FocusedRowHandle = -1;
            grvAddress.FocusedRowHandle = -1;
            grvSale.FocusedRowHandle = -1;

            if (!ValidateForm()) return false;

            customer.Province = cboProvinces.EditValue.ToString();
            customer.CustomerCode = $"{txtCodeProvinces.Text.Trim()}-{txtCustomerCode.Text.Trim()}";
            customer.CustomerShortName = txtCustomerShortName.Text.Trim().ToUpper();
            customer.CustomerName = txtCustomerName.Text.Trim();
            customer.Address = txtAddress.Text.Trim();
            customer.CustomerType = cboCustomerType.SelectedIndex;
            customer.ProductDetails = textProductDetails.Text.Trim();
            customer.NoteDelivery = txtNoteDelivery.Text.Trim();
            customer.NoteVoucher = txtNoteVoucher.Text.Trim();
            customer.CheckVoucher = txtCheckVoucher.Text.Trim();
            customer.HardCopyVoucher = txtHardCopyVoucher.Text.Trim();
            customer.CustomerSpecializationID = TextUtils.ToInt(cboMajor.EditValue);
            customer.ClosingDateDebt = TextUtils.ToDate3(txtClosingDateDebt.Value);
            customer.Debt = txtDebt.Text.Trim();
            customer.TaxCode = txtTaxCode.Text.Trim();
            customer.IsDeleted = false;
            customer.BigAccount = chkBigAccount.Checked;


            if (customer.ID > 0)
            {
                SQLHelper<CustomerModel>.Update(customer);
            }
            else
            {
                customer.ID = SQLHelper<CustomerModel>.Insert(customer).ID;
            }


            // Lưu thông tin liên hệ
            for (int i = 0; i < grvData.RowCount; i++)
            {
                CustomerContactModel detail = new CustomerContactModel();
                int ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                if (ID > 0)
                {
                    detail = SQLHelper<CustomerContactModel>.FindByID(ID);
                }
                detail.ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                detail.CustomerID = customer.ID;
                detail.ContactEmail = TextUtils.ToString(grvData.GetRowCellValue(i, colContactEmail));
                detail.ContactName = TextUtils.ToString(grvData.GetRowCellValue(i, colContactName));
                detail.ContactPhone = TextUtils.ToString(grvData.GetRowCellValue(i, colContactPhone));
                detail.CustomerPart = TextUtils.ToString(grvData.GetRowCellValue(i, colCustomerPart));
                detail.CustomerPosition = TextUtils.ToString(grvData.GetRowCellValue(i, colCustomerPosition));
                detail.CustomerTeam = TextUtils.ToString(grvData.GetRowCellValue(i, colCustomerTeam));
                if (ID == 0)
                {
                    // phúc thấy lỗi ở đây này
                    //customer.ID = SQLHelper<CustomerContactModel>.Insert(detail).ID;
                    SQLHelper<CustomerContactModel>.Insert(detail);
                }
                else
                {
                    SQLHelper<CustomerContactModel>.Update(detail);
                }
            }


            // Địa chỉ khách hàng
            for (int i = 0; i < grvAddress.RowCount; i++)
            {
                AddressStockModel add = new AddressStockModel();
                int ID = TextUtils.ToInt(grvAddress.GetRowCellValue(i, colID));
                if (ID > 0)
                {
                    add = SQLHelper<AddressStockModel>.FindByID(ID);
                }
                add.CustomerID = customer.ID;
                add.Address = TextUtils.ToString(grvAddress.GetRowCellValue(i, colAddress));

                if (ID == 0)
                {
                    SQLHelper<AddressStockModel>.Insert(add);
                }
                else
                {
                    SQLHelper<AddressStockModel>.Update(add);
                }
            }

            // lưu nhân viên Sale
            for (int i = 0; i < grvSale.RowCount; i++)
            {
                CustomerEmployeeModel add = new CustomerEmployeeModel();
                int ID = TextUtils.ToInt(grvSale.GetRowCellValue(i, colSaleID));
                if (ID > 0)
                {
                    add = SQLHelper<CustomerEmployeeModel>.FindByID(ID);
                }
                add.CustomerID = customer.ID;
                add.EmployeeID = TextUtils.ToInt(grvSale.GetRowCellValue(i, colEmployeeSale));

                if (ID <= 0)
                {
                    SQLHelper<CustomerEmployeeModel>.Insert(add);
                }
                else
                {
                    SQLHelper<CustomerEmployeeModel>.Update(add);
                }
            }

            BusinessFieldLinkModel business = SQLHelper<BusinessFieldLinkModel>.FindByAttribute("CustomerID", customer.ID).FirstOrDefault();
            if (business == null) business = new BusinessFieldLinkModel();

            business.BusinessFieldID = TextUtils.ToInt(cboBusinessField.EditValue);
            business.CustomerID = customer.ID;
            // Lưu sản phẩm đã bán!
            if (business.ID <= 0) SQLHelper<BusinessFieldLinkModel>.Insert(business);
            else SQLHelper<BusinessFieldLinkModel>.Update(business);

            DeleteIDItemRow();

            return true;
        }

        bool ValidateForm()
        {
            string pattern = @"^[a-zA-Z0-9]+$";
            Regex regex = new Regex(pattern);

            if (string.IsNullOrEmpty(TextUtils.ToString(cboProvinces.EditValue).Trim()))
            {
                MessageBox.Show("Xin vui lòng chọn Tỉnh!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (string.IsNullOrEmpty(txtCustomerCode.Text.Trim()))
            {
                MessageBox.Show("Xin vui lòng nhập Mã khách!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                bool isCheck = regex.IsMatch(txtCustomerCode.Text.Trim());
                //bool isCheck = Encoding.ASCII.GetByteCount(txtCustomerCode.Text.Trim()) != Encoding.UTF8.GetByteCount(txtCustomerCode.Text.Trim());
                if (!isCheck)
                {
                    MessageBox.Show("Mã khách hàng chỉ chứa chữ cái tiếng Anh và số!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtCustomerShortName.Text.Trim()))
            {
                MessageBox.Show("Xin vui lòng nhập Ký hiệu!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                bool isCheck = regex.IsMatch(txtCustomerShortName.Text.Trim());
                //bool isCheck = Encoding.ASCII.GetByteCount(txtCustomerCode.Text.Trim()) != Encoding.UTF8.GetByteCount(txtCustomerCode.Text.Trim());
                if (!isCheck)
                {
                    MessageBox.Show("Ký hiệu chỉ chứa chữ cái tiếng Anh và số!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtCustomerName.Text.Trim()))
            {
                MessageBox.Show("Xin vui lòng nhập Tên khách hàng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (string.IsNullOrEmpty(txtAddress.Text.Trim()))
            {
                MessageBox.Show("Xin vui lòng nhập Địa chỉ!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (TextUtils.ToInt(cboMajor.EditValue) <= 0)
            {
                MessageBox.Show("Xin vui lòng chọn Ngành nghề!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (txtCustomerCode.Text.Length > 10)
            {
                MessageBox.Show("Mã khách hàng không nhập quá 10 ký tự. Vui lòng kiểm tra lại !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtCustomerCode.Focus();
                return false;
            }

            if (txtCustomerShortName.Text.Length > 5)
            {
                MessageBox.Show("Mã khách hàng không nhập quá 5 ký tự. Vui lòng kiểm tra lại !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtCustomerShortName.Focus();
                return false;
            }


            //int cusID = -1;
            //if (customer.ID > 0) cusID = customer.ID;
            string customerCode = $"{txtCodeProvinces.Text.Trim()}-{txtCustomerCode.Text.Trim()}";
            var exp1 = new Expression("CustomerCode", customerCode);
            var exp2 = new Expression("CustomerShortName", txtCustomerShortName.Text.Trim());
            var exp3 = new Expression("ID", customer.ID, "<>");
            var exp4 = new Expression("IsDeleted", 1, "<>");

            //List<CustomerModel> listCode = SQLHelper<CustomerModel>.FindAll().Where(p => p.CustomerCode == txtCustomerCode.Text.Trim() && p.ID != cusID).ToList();
            List<CustomerModel> listCode = SQLHelper<CustomerModel>.FindByExpression(exp1.And(exp3).And(exp4));
            if (listCode.Count > 0)
            {
                MessageBox.Show($"Mã khách {customerCode} đã tồn tại, vui lòng kiểm tra lại", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            //List<CustomerModel> listShortName = SQLHelper<CustomerModel>.FindAll().Where(p => p.CustomerShortName == txtCustomerShortName.Text.Trim() && p.ID != cusID).ToList();
            List<CustomerModel> listShortName = SQLHelper<CustomerModel>.FindByExpression(exp2.And(exp3).And(exp4)).ToList();
            if (listShortName.Count > 0)
            {
                MessageBox.Show($"Tên kí hiệu {txtCustomerShortName.Text.Trim()} đã tồn tại, vui lòng kiểm tra lại", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }


            if (warehouseID == 1)
            {
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    if (string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, colContactName))))
                    {
                        MessageBox.Show("Xin vui lòng nhập Họ tên THÔNG TIN LIÊN HỆ!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }

                    if (string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, colCustomerPart))))
                    {
                        MessageBox.Show("Xin vui lòng nhập Bộ phận THÔNG TIN LIÊN HỆ!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }

                    if (string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, colCustomerPosition))))
                    {
                        MessageBox.Show("Xin vui lòng nhập Chức vụ THÔNG TIN LIÊN HỆ!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }

                    if (string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, colContactPhone))))
                    {
                        MessageBox.Show("Xin vui lòng nhập Số điện thoại THÔNG TIN LIÊN HỆ!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }

                    if (string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, colContactEmail))))
                    {
                        MessageBox.Show("Xin vui lòng nhập Email THÔNG TIN LIÊN HỆ!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }

                }

            }

            return true;
        }

        bool ValidateFormOld()
        {
            DataTable dtCode;
            DataTable dtShortName;
            if (customer.ID > 0) dtCode = TextUtils.Select("select top 1 CustomerCode from Customer where CustomerCode = '" + txtCustomerCode.Text.Trim() + "' and ID <> " + customer.ID);
            else dtCode = TextUtils.Select("select top 1 CustomerCode from Customer where CustomerCode = '" + txtCustomerCode.Text.Trim() + "'");
            if (dtCode.Rows.Count > 0)
            {
                MessageBox.Show("Mã khách hàng đã tồn tại, vui lòng kiểm tra lại", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            // TODO: 18022022
            // KIỂM TRA TỒN TẠI TÊN KÍ HIỆU CHƯA
            if (customer.ID > 0) dtShortName = TextUtils.Select("select top 1 CustomerShortName from Customer where CustomerShortName = '" + txtCustomerShortName.Text.Trim().ToUpper() + "' and ID <> " + customer.ID);
            else dtShortName = TextUtils.Select("select top 1 CustomerShortName from Customer where CustomerShortName = '" + txtCustomerShortName.Text.Trim().ToUpper() + "'");
            if (dtShortName.Rows.Count > 0)
            {
                MessageBox.Show("Tên kí hiệu khách hàng đã tồn tại, vui lòng kiểm tra lại", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (string.IsNullOrEmpty(txtCustomerCode.Text.Trim()))
            {
                MessageBox.Show("Xin vui lòng nhập mã khách hàng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(txtCustomerShortName.Text.Trim()))
            {
                MessageBox.Show("Xin vui lòng nhập tên kí hiệu!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(txtCustomerName.Text.Trim()))
            {
                MessageBox.Show("Xin vui lòng nhập tên khách hàng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(txtAddress.Text.Trim()))
            {
                MessageBox.Show("Xin vui lòng nhập địa chỉ khách hàng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (txtCustomerShortName.TextLength > 5)
            {
                MessageBox.Show("Tên ký hiệu không nhập quá 5 ký tự. Vui lòng kiểm tra lại !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtCustomerShortName.Focus();
                return false;
            }
            return true;
        }

        private void btnAddMajoy_Click(object sender, EventArgs e)
        {
            frmCustomerMajor frm = new frmCustomerMajor();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDataCustomerMajor();
            }
        }

        private void btnAddBusinessField_Click(object sender, EventArgs e)
        {

            frmBusinessField frm = new frmBusinessField();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadBusinessField();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
            //else
            //{
            //    MessageBox.Show("Lưu thất bại!");
            //}
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                Reset();
            }
            //else
            //{
            //    MessageBox.Show("Lưu thất bại!");
            //}
        }


        void LoadProvinces()
        {
            cboProvinces.Properties.ValueMember = "Name";
            cboProvinces.Properties.DisplayMember = "Name";
            cboProvinces.Properties.DataSource = provinces.OrderBy(x => x.STT).ToList();
        }
        private void loadCustomerDetail()
        {

            string provinceCode = "";
            string customerCode = "";
            //if (!string.IsNullOrEmpty(customer.CustomerCode))
            //{
            //    provinceCode = customer.CustomerCode.Substring(0, 3);
            //    customerCode = customer.CustomerCode.Substring(4, customer.CustomerCode.Length - 4);
            //}

            if (!string.IsNullOrEmpty(customer.CustomerCode))
            {
                if (customer.CustomerCode.Trim().Length <= 4) customerCode = customer.CustomerCode;
                else customerCode = customer.CustomerCode.Substring(4, customer.CustomerCode.Length - 4);

                if (customer.CustomerCode.Trim().Length >= 3) provinceCode = customer.CustomerCode.Substring(0, 3);
            }


            txtCodeProvinces.Text = provinceCode;
            txtCustomerCode.Text = customerCode;

            cboProvinces.EditValue = customer.Province;
            textProductDetails.Text = customer.ProductDetails;
            txtCustomerShortName.Text = customer.CustomerShortName;
            txtCustomerName.Text = customer.CustomerName;
            txtAddress.Text = customer.Address;
            cboMajor.EditValue = customer.CustomerSpecializationID;


            txtNoteDelivery.Text = customer.NoteDelivery;
            txtDebt.Text = customer.Debt;
            txtTaxCode.Text = customer.TaxCode;
            cboCustomerType.SelectedIndex = customer.CustomerType;
            txtNoteVoucher.Text = customer.NoteVoucher;
            txtCheckVoucher.Text = customer.CheckVoucher;
            txtHardCopyVoucher.Text = customer.HardCopyVoucher;
            chkBigAccount.Checked = customer.BigAccount;
            txtClosingDateDebt.Text = TextUtils.ToString(customer.ClosingDateDebt);
            txtDebt.Text = customer.Debt;

            //Get BusinessField
            BusinessFieldLinkModel business = SQLHelper<BusinessFieldLinkModel>.SqlToModel($"SELECT top 1 * FROM BusinessFieldLink WHERE CustomerID = {customer.ID}");
            if (business != null) cboBusinessField.EditValue = business.BusinessFieldID;


            DataTable dt = TextUtils.Select("SELECT * FROM dbo.CustomerContact WHERE CustomerID = " + customer.ID);
            grdData.DataSource = dt;
            DataTable dtAddress = TextUtils.Select("SELECT * FROM dbo.AddressStock WHERE CustomerID = " + customer.ID);
            grdAddress.DataSource = dtAddress;


            if (warehouseID == 3)
            {
                colCustomerPart.Caption = "Bộ phận";
                colContactPhone.Caption = "SĐT";
                colContactEmail.Caption = "Email";
            }
        }
        private void LoadDataCustomerMajor()
        {
            List<CustomerSpecializationModel> listData = SQLHelper<CustomerSpecializationModel>.FindAll().OrderBy(p => p.STT).ToList();
            cboMajor.Properties.ValueMember = "ID";
            cboMajor.Properties.DisplayMember = "Name";
            cboMajor.Properties.DataSource = listData;
        }
        private void LoadBusinessField()
        {
            List<BusinessFieldModel> list = SQLHelper<BusinessFieldModel>.FindAll();

            cboBusinessField.Properties.ValueMember = "ID";
            cboBusinessField.Properties.DisplayMember = "Name";
            cboBusinessField.Properties.DataSource = list;
        }


        private void LoadEmployee()
        {
            DataTable list = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] {0 });
            cboEmployeeSale.ValueMember = "ID";
            cboEmployeeSale.DisplayMember = "FullName";
            cboEmployeeSale.DataSource = list;
        }
        private void LoadEmployeeSale()
        {
            DataTable dt = TextUtils.Select($"SELECT * FROM CustomerEmployee WHERE CustomerID = {customer.ID}");
            if (customer.ID <= 0)
            {
                DataRow dataRow = dt.NewRow();
                dataRow["EmployeeID"] = Global.EmployeeID;
                dt.Rows.Add(dataRow);
            }

            grdSale.DataSource = dt;
        }

        private void Reset()
        {
            customer = new CustomerModel();
            txtCodeProvinces.Clear();
            txtCustomerName.Clear();
            textProductDetails.Clear();
            txtCustomerShortName.Clear();
            txtCustomerName.Clear();
            txtAddress.Clear();
            cboMajor.EditValue = 0;
            txtNoteDelivery.Clear();
            txtDebt.Clear();
            txtTaxCode.Clear();
            cboCustomerType.SelectedIndex = 0;
            txtNoteVoucher.Clear();
            txtCheckVoucher.Clear();
            txtHardCopyVoucher.Clear();
            chkBigAccount.Checked = false;
            txtDebt.Clear();
            txtClosingDateDebt.Value = DateTime.Now;
            cboBusinessField.EditValue = 0;

            DataTable dt = TextUtils.Select("SELECT * FROM dbo.CustomerContact WHERE CustomerID = " + customer.ID);
            grdData.DataSource = dt;
            DataTable dtAddress = TextUtils.Select("SELECT * FROM dbo.AddressStock WHERE CustomerID = " + customer.ID);
            grdAddress.DataSource = dtAddress;
            LoadEmployeeSale();
        }

        private void txtCustomerCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            //MessageBox.Show(Keys.Control);

            //if (!(e.KeyChar == 8 || (e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar >= 65 && e.KeyChar <= 89) || (e.KeyChar >= 97 && e.KeyChar <= 121)))
            //{
            //    // MessageBox.Show("Vui lòng không nhập Ký tự đặc biệt và Chữ có dấu!");
            //    e.Handled = true;
            //}


        }

        private void txtCustomerCode_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode !=  Keys.Space)
            //{

            //}
        }

        private void frmCustomerDetailNew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void frmCustomerDetailNew_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }


        private void DeleteIDItemRow()
        {
            //delete customer
            if (lsCustomerIDDelete.Count > 0)
            {
                CustomerContactBO.Instance.Delete(lsCustomerIDDelete);
            }

            //delete address
            if (lsAddressDelete.Count > 0)
            {
                AddressStockBO.Instance.Delete(lsAddressDelete);
            }

            //delete employeeSale
            if (lsEmployeeSaleDelete.Count > 0)
            {
                CustomerEmployeeBO.Instance.Delete(lsEmployeeSaleDelete);
            }
        }

        private void groupControl1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (grdData.DataSource == null || groupControl1.CustomHeaderButtons[0].Properties.Enabled == false)
                return;
            int customerID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));

            string strName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colContactName));

            string txtName = customerID <= 0 ? "này" : $"của '{strName}'";
            if (MessageBox.Show(String.Format($"Bạn có chắc muốn xóa thông tin liên hệ {txtName} không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvData.DeleteSelectedRows();
                if (customerID > 0)
                {
                    lsCustomerIDDelete.Add(customerID);
                }

            }
        }

        private void groupControl2_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (grdAddress.DataSource == null || groupControl2.CustomHeaderButtons[0].Properties.Enabled == false)
                return;
            int addressID = TextUtils.ToInt(grvAddress.GetFocusedRowCellValue(colIDShip));
            string strAddress = TextUtils.ToString(grvAddress.GetFocusedRowCellValue(colAddress));

            string txtName = addressID <= 0 ? "này" : $"'{strAddress}'";
            if (MessageBox.Show(String.Format($"Bạn có chắc muốn xóa địa chỉ {txtName} không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvAddress.DeleteSelectedRows();
                if (addressID > 0)
                {
                    lsAddressDelete.Add(addressID);
                }
            }
        }

        private void groupControl3_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (grdSale.DataSource == null || groupControl3.CustomHeaderButtons[0].Properties.Enabled == false)
                return;
            int saleID = TextUtils.ToInt(grvSale.GetFocusedRowCellValue(colSaleID));
            string strEmployeeSale = TextUtils.ToString(grvSale.GetFocusedRowCellDisplayText(colEmployeeSale));

            string txtName = saleID <= 0 ? "này" : $"'{strEmployeeSale}'";
            if (MessageBox.Show(String.Format($"Bạn có chắc muốn xóa nhân viên sale {txtName} không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvSale.DeleteSelectedRows();
                if (saleID > 0)
                {
                    lsEmployeeSaleDelete.Add(saleID);
                }
            }
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
