using BMS.Business;
using BMS.Model;
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
    public partial class frmSupplierDetail : _Forms
    {
        public SupplierModel Supplier = new SupplierModel();
        public frmSupplierDetail()
        {
            InitializeComponent();
        }

        private void frmSupplierDetail_Load(object sender, EventArgs e)
        {
            loadData();
        }
        /// <summary>
        /// Load dữ liệu vào form
        /// </summary>
        void loadData()
        {
            txtAddress.Text = Supplier.Address;
            txtBankName.Text = Supplier.BankName;
            txtCode.Text = Supplier.SupplierCode;
            txtContactEmail.Text = Supplier.ContactEmail;
            txtContactName.Text = Supplier.ContactName;
            txtContactPhone.Text = Supplier.ContactPhone;
            txtEmail.Text = Supplier.Email;
            txtMST.Text = Supplier.MST;
            txtName.Text = Supplier.SupplierName;
            txtShortName.Text = Supplier.SupplierShortName;
            txtNote.Text = Supplier.Note;
            txtOffice.Text = Supplier.Office;
            txtPhoneNumber.Text = Supplier.Phone;
            txtSTK.Text = Supplier.BankAcount;
            txtWebsite.Text = Supplier.Website;
            txtMainProduct.Text = Supplier.MainProduct;
            txtDebtLimit.EditValue = Supplier.DebtLimit;

            if (Supplier.ID == 0)
            {
                txtCode.Text = TextUtils.CreateNewCode("Supplier", "SupplierCode", "S");
            }

            DataTable dt = TextUtils.Select("SELECT * FROM dbo.SupplierContact WHERE SupplierID = " + Supplier.ID);
            grdData.DataSource = dt;
        }

        /// <summary>
        /// Validate trước khi cất dữ liệu
        /// </summary>
        /// <returns></returns>
        private bool ValidateForm()
        {
            if (txtCode.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền mã nhà cung cấp.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                DataTable dt;
                if (Supplier.ID > 0)
                {
                    int strID = Supplier.ID;
                    dt = TextUtils.Select("select top 1 SupplierCode from Supplier where SupplierCode = '" + txtCode.Text.Trim() + "' and ID <> " + strID);
                }
                else
                {
                    dt = TextUtils.Select("select top 1 SupplierCode from Supplier where SupplierCode = '" + txtCode.Text.Trim() + "'");
                }
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Mã nhà cung cấp này đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }
                }
            }
            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền tên nhà cung cấp.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            
            return true;
        }

        /// <summary>
        /// Cất dữ liệu
        /// </summary>
        /// <returns></returns>
        void saveData()
        {
            if (!ValidateForm())
                return;

            Supplier.SupplierCode = txtCode.Text.Trim();
            Supplier.SupplierName = txtName.Text.Trim();
            Supplier.SupplierShortName = txtShortName.Text.Trim();
            Supplier.Phone = txtPhoneNumber.Text.Trim();
            Supplier.Email = txtEmail.Text.Trim();
            Supplier.Website = txtWebsite.Text.Trim();
            Supplier.MST = txtMST.Text.Trim();
            Supplier.BankName = txtBankName.Text.Trim();
            Supplier.BankAcount = txtSTK.Text.Trim();
            Supplier.Office = txtOffice.Text.Trim();
            Supplier.Address = txtAddress.Text.Trim();
            Supplier.Note = txtNote.Text.Trim();

            Supplier.ContactName = txtContactName.Text.Trim();
            Supplier.ContactPhone = txtContactPhone.Text.Trim();
            Supplier.ContactEmail = txtContactEmail.Text.Trim();

            Supplier.DebtLimit = TextUtils.ToDecimal(txtDebtLimit.EditValue);
            Supplier.MainProduct = txtMainProduct.Text.Trim();

            if (Supplier.ID > 0)
            {
                SupplierBO.Instance.Update(Supplier);
            }
            else
            {
                Supplier.ID = (int)SupplierBO.Instance.Insert(Supplier);
            }

            int count = grvData.RowCount;
            for (int i = 0; i < count; i++)
            {
                SupplierContactModel detail = new SupplierContactModel();
                int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                if (id > 0)
                {
                    detail = (SupplierContactModel)SupplierContactBO.Instance.FindByPK(id);
                }
                detail.SupplierID = Supplier.ID;
                detail.ContactEmail = TextUtils.ToString(grvData.GetRowCellValue(i, colContactEmail));
                detail.ContactName = TextUtils.ToString(grvData.GetRowCellValue(i, colContactName));
                detail.ContactPhone = TextUtils.ToString(grvData.GetRowCellValue(i, colContactPhone));
                detail.ProductSale = TextUtils.ToString(grvData.GetRowCellValue(i, colProductSale));

                if (id == 0)
                {
                    SupplierContactBO.Instance.Insert(detail);
                }
                else
                {
                    SupplierContactBO.Instance.Update(detail);
                }
            }
        } 

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveData();
            this.DialogResult = DialogResult.OK;
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            saveData();
            Supplier = new SupplierModel();
            loadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSupplierDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
      
    }
}
