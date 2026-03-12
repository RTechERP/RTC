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
    public partial class frmCustomerDetail : _Forms
    {
        public CustomerModel customer = new CustomerModel();

        public frmCustomerDetail()
        {
            InitializeComponent();
        }
        private void frmCustomerDetail_Load(object sender, EventArgs e)
        {
            txtCustomerShortName.GotFocus += TxtCustomerShortName_GotFocus;
            loadBusinessField();
            loadCustomerDetail();
        }
        /// <summary>
        /// load Data
        /// </summary>
        private void loadCustomerDetail()
        {
            txtCustomerCode.Text = customer.CustomerCode;
            txtCustomerShortName.Text = customer.CustomerShortName;
            txtCustomerName.Text = customer.CustomerName;
            txtAddress.Text = customer.Address;
            cbType.SelectedIndex = customer.CustomerType;
            txtNoteDelivery.Text = customer.NoteDelivery;
            txtNoteVoucher.Text = customer.NoteVoucher;
            txtCheckVoucher.Text = customer.CheckVoucher;
            txtHardCopyVoucher.Text = customer.HardCopyVoucher;
            txtClosingDateDebt.Text = TextUtils.ToString(customer.ClosingDateDebt);
            txtDebt.Text = customer.Debt;

            DataTable dt = TextUtils.Select("SELECT * FROM dbo.CustomerContact WHERE CustomerID = " + customer.ID);
            grdData.DataSource = dt;
            DataTable dtAddress = TextUtils.Select("SELECT * FROM dbo.AddressStock WHERE CustomerID = " + customer.ID);
            grdAddress.DataSource = dtAddress;
        }

        /// <summary>
        /// click button save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// hàm save
        /// </summary>
        bool saveData()
        {
            if (!ValidateForm()) return false;
            customer.CustomerCode = txtCustomerCode.Text.Trim();
            customer.CustomerShortName = txtCustomerShortName.Text.Trim().ToUpper();
            customer.CustomerName = txtCustomerName.Text.Trim();
            customer.Address = txtAddress.Text.Trim();
            customer.CustomerType = TextUtils.ToInt(cbType.Text);
            customer.NoteDelivery = txtNoteDelivery.Text.Trim();
            customer.NoteVoucher = txtNoteVoucher.Text.Trim();
            customer.CheckVoucher = txtCheckVoucher.Text.Trim();
            customer.HardCopyVoucher = txtHardCopyVoucher.Text.Trim();
            customer.ClosingDateDebt = TextUtils.ToDate3(txtClosingDateDebt.Value);
            customer.Debt = txtDebt.Text.Trim();
            customer.TaxCode = txtTaxCode.Text.Trim();
            customer.IsDeleted = false;

            if (customer.ID > 0)
            {
                CustomerBO.Instance.Update(customer);
            }
            else
            {
                customer.ID = (int)CustomerBO.Instance.Insert(customer);
            }

            int count = grvData.RowCount;
            for (int i = 0; i < count; i++)
            {
                CustomerContactModel detail = new CustomerContactModel();
                int ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                if (ID > 0)
                {
                    detail = (CustomerContactModel)CustomerContactBO.Instance.FindByPK(ID);
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
                    CustomerContactBO.Instance.Insert(detail);
                }
                else
                {
                    CustomerContactBO.Instance.Update(detail);
                }
            }
            for (int i = 0; i < grvAddress.RowCount; i++)
            {
                AddressStockModel add = new AddressStockModel();
                int ID = TextUtils.ToInt(grvAddress.GetRowCellValue(i, colID));
                if (ID > 0)
                {
                    add = (AddressStockModel)AddressStockBO.Instance.FindByPK(ID);
                }
                add.CustomerID = customer.ID;
                add.Address = TextUtils.ToString(grvAddress.GetRowCellValue(i, colAddress));

                if (ID == 0)
                {
                    AddressStockBO.Instance.Insert(add);
                }
                else
                {
                    AddressStockBO.Instance.Update(add);
                }
            }


            //Add lĩnh vực kinh doanh
            var exp1 = new Expression("BusinessFieldID", TextUtils.ToInt(cboBusinessField.EditValue));
            var exp2 = new Expression("CustomerID", customer.ID);

            BusinessFieldLinkModel business = SQLHelper<BusinessFieldLinkModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
            if (business == null)
            {
                business = new BusinessFieldLinkModel();
            }

            business.BusinessFieldID = TextUtils.ToInt(cboBusinessField.EditValue);
            business.CustomerID = customer.ID;
            if (business.ID <= 0)
            {

                SQLHelper<BusinessFieldLinkModel>.Insert(business);
            }
            else
            {
                SQLHelper<BusinessFieldLinkModel>.Update(business);
            }

            return true;
        }

        /// <summary>
        /// click button lưu và thêm mới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            saveData();
            txtCustomerCode.Clear();
            txtCustomerShortName.Clear();
            txtCustomerName.Clear();
            txtAddress.Clear();
            cbType.Items.Clear();
            txtNoteDelivery.Clear();
            txtNoteVoucher.Clear();
            txtCheckVoucher.Clear();
            txtHardCopyVoucher.Clear();
            txtDebt.Clear();
            for (int i = grvData.RowCount - 1; i >= 0; i--)
            {
                grvData.DeleteRow(i);
            }
            customer = new CustomerModel();
            loadCustomerDetail();
        }

        /// <summary>
        /// tắt form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmProductKHACHHANG_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// click chuột phải để save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveData();
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// check lỗi
        /// </summary>
        /// <returns></returns>
        /// 
        bool ValidateForm()
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

        private void txtCustomerShortName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = e.KeyChar == (char)Keys.Space;
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtCustomerShortName_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtCustomerShortName_GotFocus(object sender, EventArgs e)
        {
            txtCustomerShortName.SelectAll();
        }

        private void txtCustomerShortName_KeyUp(object sender, KeyEventArgs e)
        {
            //txtCustomerShortName.Text = Regex.Replace(txtCustomerShortName.Text, @"[^a-zA-Z0-9]", string.Empty);

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAddBusinessField_Click(object sender, EventArgs e)
        {
            frmBusinessField frm = new frmBusinessField();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadBusinessField();
            }
        }

        void loadBusinessField()
        {
            List<BusinessFieldModel> list = SQLHelper<BusinessFieldModel>.FindAll();

            cboBusinessField.Properties.ValueMember = "ID";
            cboBusinessField.Properties.DisplayMember = "Name";
            cboBusinessField.Properties.DataSource = list;
        }
    }
}
