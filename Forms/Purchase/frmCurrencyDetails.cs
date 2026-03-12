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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmCurrencyDetails : _Forms
    {
        public CurrencyModel currencyModel = new CurrencyModel();

        public frmCurrencyDetails()
        {
            InitializeComponent();

        }

        //load data to textbox if update 
        private void loadData()
        {
            if (currencyModel.ID != 0)
            {
                dtPExpire.Value = currencyModel.DateExpried.Value;
                dtPStartDate.Value = currencyModel.DateStart.Value;
                txtCode.Text = currencyModel.Code;
                txtMinunit.Text = currencyModel.MinUnit;
                txtNameEng.Text = currencyModel.NameEnglist;
                txtNameVie.Text = currencyModel.NameVietNamese;
                txtNote.Text = currencyModel.Note;
                //numRate.Text = currencyModel.CurrencyRate.ToString();
                txtRate.EditValue = currencyModel.CurrencyRate;
                txtCurrencyRateOfficialQuota.EditValue = currencyModel.CurrencyRateOfficialQuota;
                txtDateExpriedOfficialQuota.Value = currencyModel.DateExpriedOfficialQuota.HasValue ? currencyModel.DateExpriedOfficialQuota.Value : DateTime.Now;
                txtCurrencyRateUnofficialQuota.EditValue = currencyModel.CurrencyRateUnofficialQuota;
                txtDateExpriedUnofficialQuota.Value = currencyModel.DateExpriedUnofficialQuota.HasValue ? currencyModel.DateExpriedUnofficialQuota.Value : DateTime.Now;

            }

        }

        private void frmCurrencyDetails_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnSaveCLose_Click(object sender, EventArgs e)
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
                currencyModel = new CurrencyModel();
                this.Controls.Clear();
                loadData();
                this.InitializeComponent();
            }
        }

        bool ValidateData()
        {
            //decimal checkNumber;
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã tiền tệ!", "Thông báo");
                return false;
            }
            if (string.IsNullOrEmpty(txtMinunit.Text))
            {
                MessageBox.Show("Vui lòng nhập Đơn vị nhỏ nhất!", "Thông báo");
                return false;
            }

            if (TextUtils.ToDecimal(txtRate.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Tỉ giá (lớn hơn 0)!.", "Thông báo");
                return false;
            }
            if (string.IsNullOrEmpty(txtNameVie.Text))
            {
                MessageBox.Show("Vui lòng nhập tên tiếng Việt!", "Thông báo");
                return false;
            }
            if (string.IsNullOrEmpty(txtNameEng.Text))
            {
                MessageBox.Show("Vui lòng nhập tên tiếng Anh!", "Thông báo");
                return false;
            }
            if (dtPExpire.Value.Date < dtPStartDate.Value.Date)
            {
                MessageBox.Show("Vui lòng nhập ngày hết hạn lớn hơn hoặc bằng ngày bắt đầu!", "Thông báo");
                return false;
            }

            var exp1 = new Expression("Code", txtCode.Text.Trim());
            var exp2 = new Expression("ID", currencyModel.ID, "<>");
            var currencys = (SQLHelper<CurrencyModel>.FindByExpression(exp1.And(exp2)));
            if (currencys.Count > 0)
            {
                MessageBox.Show($"Mã tiền tệ [{txtCode.Text.Trim()}] đã tồn tại. Vui lòng nhập mã tiền tệ khác!", "Thông báo");
                return false;
            }
            return true;
        }

        bool SaveData()
        {
            if (!ValidateData()) return false;

            currencyModel.Code = txtCode.Text.Trim().ToUpper();
            currencyModel.MinUnit = txtMinunit.Text.Trim();
            currencyModel.NameEnglist = txtNameEng.Text.Trim();
            currencyModel.NameVietNamese = txtNameVie.Text.Trim();
            currencyModel.DateStart = dtPStartDate.Value;
            currencyModel.DateExpried = dtPExpire.Value;
            currencyModel.Note = txtNote.Text.Trim();
            //currencyModel.CreatedDate = DateTime.Now;
            //currencyModel.CurrencyRate = decimal.Parse(numRate.Text);
            currencyModel.CurrencyRate = TextUtils.ToDecimal(txtRate.EditValue);

            currencyModel.CurrencyRateOfficialQuota = TextUtils.ToDecimal(txtCurrencyRateOfficialQuota.EditValue);
            currencyModel.DateExpriedOfficialQuota = txtDateExpriedOfficialQuota.Value;
            currencyModel.CurrencyRateUnofficialQuota = TextUtils.ToDecimal(txtCurrencyRateUnofficialQuota.EditValue);
            currencyModel.DateExpriedUnofficialQuota = txtDateExpriedUnofficialQuota.Value;

            if (currencyModel.ID > 0)
            {
                CurrencyBO.Instance.Update(currencyModel);
            }
            else
            {
                CurrencyBO.Instance.Insert(currencyModel);
            }
            return true;

            //if (ValidateData(currencyModel) == true)
            //{
            //    i
            //}
            //else return false;

        }

        private void frmCurrencyDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void frmCurrencyDetails_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }

}