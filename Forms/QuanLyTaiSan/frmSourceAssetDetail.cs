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
    public partial class frmSourceAssetDetail : _Forms
    {
        public TSSourceAssetModel source = new TSSourceAssetModel();
        public frmSourceAssetDetail()
        {
            InitializeComponent();
        }

        private void frmSourceAssetDetail_Load(object sender, EventArgs e)
        {
            LoadDataSource();
        }
        void LoadDataSource()
        {
            if(source.ID > 0)
            {
                txtCode.Text = source.SourceCode;
                txtName.Text = source.SourceName;
            }
        }
        public bool ValidateForm()
        {
            if(txtCode.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập vào mã nguồn gốc !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                if (TextUtils.CheckExistTable(source.ID, "SourceCode", txtCode.Text.Trim(), "TSSourceAsset"))
                {
                    MessageBox.Show("Mã này đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }
            if (txtName.Text == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập vào tên nguồn gốc !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        bool saveData()
        {
            if (!ValidateForm()) return false;

            source.SourceCode = txtCode.Text.Trim();
            source.SourceName = txtName.Text.Trim();

            if (source.ID > 0)
            {
                TSSourceAssetBO.Instance.Update(source);
            }
            else
                source.ID = (int)TSSourceAssetBO.Instance.Insert(source);
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
