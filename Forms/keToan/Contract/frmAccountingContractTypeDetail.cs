using BMS.Business;
using BMS.Model;
using BMS.Utils;
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
    public partial class frmAccountingContractTypeDetail : _Forms
    {
        public AccountingContractTypeModel contractType = new AccountingContractTypeModel();
        public frmAccountingContractTypeDetail()
        {
            InitializeComponent();
        }

        private void frmAccountingContractTypeDetail_Load(object sender, EventArgs e)
        {
            
            LoadData();
        }

        void LoadData()
        {
            List<AccountingContractTypeModel> listType = SQLHelper<AccountingContractTypeModel>.FindAll().OrderBy(x => x.STT).ToList();
            cbCurrentcyUnit.Checked = contractType.ID > 0 ? contractType.IsContractValue : true;
            txtTypeCode.Text = contractType.TypeCode;
            txtTypeName.Text = contractType.TypeName;
            txtSTT.Value = contractType.ID > 0 ? contractType.STT : (listType.Count > 0 ? listType.Max(x => x.STT) + 1 : 1);
        }


        bool SaveData()
        {
            if (!CheckValidate())
            {
                return false;
            }
            contractType.STT = TextUtils.ToInt(txtSTT.Value);
            contractType.TypeCode = txtTypeCode.Text.Trim();
            contractType.TypeName = txtTypeName.Text.Trim();
            contractType.IsContractValue = cbCurrentcyUnit.Checked;
            if (contractType.ID > 0)
            {
                SQLHelper<AccountingContractTypeModel>.Update(contractType);
            }
            else
            {
                //SQLHelper<AccountingContractTypeModel>.Insert(contractType);
                contractType.ID = (int)AccountingContractTypeBO.Instance.Insert(contractType);
            }

            return true;
        }

        bool CheckValidate()
        {
            if (string.IsNullOrEmpty(txtTypeCode.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Mã loại!", "Thông báo");
                return false;
            }
            else
            {
                var exp1 = new Expression("TypeCode", txtTypeCode.Text.Trim());
                var exp2 = new Expression("ID", contractType.ID, "<>");
                var list = SQLHelper<AccountingContractTypeModel>.FindByExpression(exp1.And(exp2));
                if (list.Count > 0)
                {
                    MessageBox.Show($"Mã loại [{txtTypeCode.Text.Trim()}] đã tồn tại!", "Thông báo");
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtTypeName.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Tên loại!", "Thông báo");
                return false;
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
            if (SaveData())
            {
                contractType = new AccountingContractTypeModel();
                LoadData();
            }
        }

        private void frmAccountingContractTypeDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void frmAccountingContractTypeDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
