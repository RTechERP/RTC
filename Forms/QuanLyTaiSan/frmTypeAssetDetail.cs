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

namespace BMS
{
    public partial class frmTypeAssetDetail : _Forms
    {
        public TSAssetModel tsas = new TSAssetModel();
        public frmTypeAssetDetail()
        {
            InitializeComponent();
        }

        private void frmTypeAssetDetail_Load(object sender, EventArgs e)
        {
            LoadDataAsset();
        }
        void LoadDataAsset()
        {
            if(tsas.ID > 0)
            {
                txtCode.Text = tsas.AssetCode;
                txtName.Text = tsas.AssetType;
            }    
        }
        public bool ValidateForm()
        {
            if(txtCode.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập vào mã tài sản !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;    
            }
            else
            {
                if (TextUtils.CheckExistTable(tsas.ID, "AssetCode", txtCode.Text.Trim(), "TSAsset"))
                {
                    MessageBox.Show("Mã này đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }
            if (txtName.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập vào tên tài sản !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        bool saveData()
        {
            if (!ValidateForm()) return false;

            tsas.AssetCode = txtCode.Text.Trim();
            tsas.AssetType = txtName.Text.Trim();

            if (tsas.ID > 0)
            {
                TSAssetBO.Instance.Update(tsas);
            }
            else
                tsas.ID = (int)TSAssetBO.Instance.Insert(tsas);
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
