using BMS.Model;
using System;
using BMS.Utils;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMS.Business;

namespace BMS
{
    public partial class frmTaxCompanyDetail : _Forms
    {
        public TaxCompanyModel taxcModel = new TaxCompanyModel();
        public frmTaxCompanyDetail()
        {
            InitializeComponent();
        }

        private void frmTaxCompanyDetail_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            txtCode.Text = taxcModel.Code;
            txtName.Text = taxcModel.Name;

            txtFullName.Text = taxcModel.FullName;
            txtTaxCode.Text = taxcModel.TaxCode;
            txtPhoneNumber.Text = taxcModel.PhoneNumber;
            txtAddress.Text = taxcModel.Address;
            txtDirector.Text = taxcModel.Director;
            txtPosition.Text = taxcModel.Position;

            // Gán thông tin tiếng Anh
            txtBuyerEnglish.Text = taxcModel.BuyerEnglish;
            txtAddressBuyerEnglish.Text = taxcModel.AddressBuyerEnglish;
            txtLegalRepresentativeEnglish.Text = taxcModel.LegalRepresentativeEnglish;

            // Gán thông tin tiếng Việt
            txtBuyerVietnamese.Text = taxcModel.BuyerVietnamese;
            txtAddressBuyerVienamese.Text = taxcModel.AddressBuyerVienamese;
            txtTaxVietnamese.Text = taxcModel.TaxVietnamese;
        }

        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            if (save())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private bool save()
        {
            if (!checkValidate()) return false;

            taxcModel.Code = TextUtils.ToString(txtCode.Text.Trim());
            taxcModel.Name = TextUtils.ToString(txtName.Text.Trim());

            taxcModel.FullName = txtFullName.Text.Trim();
            taxcModel.TaxCode = txtTaxCode.Text.Trim();
            taxcModel.PhoneNumber = txtPhoneNumber.Text.Trim();
            taxcModel.Address = txtAddress.Text.Trim();
            taxcModel.Director = txtDirector.Text.Trim();
            taxcModel.Position = txtPosition.Text.Trim();

            //Lưu thông tin tiếng anh
            taxcModel.BuyerEnglish = txtBuyerEnglish.Text.Trim();
            taxcModel.AddressBuyerEnglish = txtAddressBuyerEnglish.Text.Trim();
            taxcModel.LegalRepresentativeEnglish = txtLegalRepresentativeEnglish.Text.Trim();

            //Lưu thông tin tiếng việt
            taxcModel.BuyerVietnamese = txtBuyerVietnamese.Text.Trim();
            taxcModel.AddressBuyerVienamese = txtAddressBuyerVienamese.Text.Trim();
            taxcModel.TaxVietnamese = txtTaxVietnamese.Text.Trim();

            if (taxcModel.ID > 0)
            {
                SQLHelper<TaxCompanyModel>.Update(taxcModel);
            }
            else
            {
                var exp1 = new Expression("Code", txtCode.Text.Trim());
                var exp2 = new Expression("ID", taxcModel.ID, "<>");
                var exp4 = new Expression("IsDeleted", 0, "<>");
                List<TaxCompanyModel> modelDeleted = SQLHelper<TaxCompanyModel>.FindByExpression(exp1.And(exp2).And(exp4));
                if (modelDeleted.Count > 0)
                {
                    if (MessageBox.Show($"Đã tồn tại!\nMã công ty: [{txtCode.Text}]\nTên công ty: [{modelDeleted[0].Name}].\n\nBạn có muốn sử dụng lại công ty [{modelDeleted[0].Name}] không?", "Thông Báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        modelDeleted[0].IsDeleted = false;
                        SQLHelper<TaxCompanyModel>.Update(modelDeleted[0]);
                    }
                }
                else
                {
                    SQLHelper<TaxCompanyModel>.Insert(taxcModel);
                }
            }
            return true;
        }

        private bool checkValidate()
        {
            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                MessageBox.Show("Mã công ty không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                var exp1 = new Expression("Code", txtCode.Text.Trim());
                var exp2 = new Expression("ID", taxcModel.ID, "<>");
                var exp3 = new Expression("IsDeleted", 1, "<>");
                var models = SQLHelper<TaxCompanyModel>.FindByExpression(exp1.And(exp2).And(exp3));
                if (models.Count > 0)
                {
                    MessageBox.Show($"Mã công ty [{txtCode.Text}] đã tồn tại.\nVui lòng nhập mã công ty khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }

            }
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Tên công ty không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            save();
            txtCode.Text = "";
            txtName.Text = "";
            taxcModel = new TaxCompanyModel();
        }

        private void frmTaxCompanyDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
