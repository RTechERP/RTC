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
    public partial class frmKPIErrorTypeDetail : _Forms
    {
        public KPIErrorTypeModel model = new KPIErrorTypeModel();
        public frmKPIErrorTypeDetail()
        {
            InitializeComponent();
        }

        private void frmKPIErrorTypeDetail_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        void LoadData()
        {
            txtSTT.Value = TextUtils.ToDecimal(model.STT);
            txtCode.Text = model.Code;
            txtName.Text = model.Name;

            if (model.ID == 0)
            {
                txtSTT.Value = TextUtils.ToDecimal(SQLHelper<KPIErrorTypeModel>.FindByAttribute("IsDelete", 0).Max(p => p.STT)) + 1;
            }
        }
        bool CheckValidate()
        {
            if (string.IsNullOrEmpty(txtCode.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Mã loại lỗi!", "Thông báo");
                return false;
            }
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Tên loại lỗi!", "Thông báo");
                return false;
            }
            var ex1 = new Expression("ID", model.ID, "<>");
            var ex2 = new Expression("Code", TextUtils.ToString(txtCode.Text.Trim()));
            var ex3 = new Expression("STT", txtSTT.Value);
            var ex4 = new Expression("IsDelete", 0);
            var exist1 = SQLHelper<KPIErrorTypeModel>.FindByExpression(ex1.And(ex2).And(ex4)).FirstOrDefault();
            if (exist1 != null)
            {
                MessageBox.Show($"Mã loại xe [{txtCode.Text.Trim()}] đã tồn tại!", "Thống báo");
                return false;
            }
            var exist2 = SQLHelper<KPIErrorTypeModel>.FindByExpression(ex1.And(ex3).And(ex4)).FirstOrDefault();
            if (exist2 != null)
            {
                MessageBox.Show($"STT [{txtSTT.Value}] đã tồn tại!", "Thống báo");
                return false;
            }
            return true;
        }
        bool Save()
        {
            if (!CheckValidate()) return false;
            model.STT = TextUtils.ToInt(txtSTT.Value);
            model.Code = txtCode.Text.Trim();
            model.Name = txtName.Text.Trim();
            if (model.ID > 0)
            {
                SQLHelper<KPIErrorTypeModel>.Update(model);
            }
            else
            {
                SQLHelper<KPIErrorTypeModel>.Insert(model);
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSaveAndReset_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                model = new KPIErrorTypeModel();
                LoadData();
            }
        }

        private void frmKPIErrorTypeDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}