using BMS.Business;
using BMS.Model;
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
    public partial class frmProjectWorkerTypeDetail : _Forms
    {
        public frmProjectWorkerTypeDetail()
        {
            InitializeComponent();
        }

        private void frmProjectWorkerTypeDetail_Load(object sender, EventArgs e)
        {

        }
        bool validate()
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã loại nhân công!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên loại nhân công!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        bool save()
        {
            if (!validate()) return false;
            ProjectWorkerTypeModel model = new ProjectWorkerTypeModel();
            model.Code = txtCode.Text.Trim();
            model.Name = txtName.Text.Trim();
            ProjectWorkerTypeBO.Instance.Insert(model);
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!save()) return;
            this.DialogResult = DialogResult.OK;
        }

        private void btnSaveAndNew_Click(object sender, EventArgs e)
        {
            if (!save()) return;
            txtCode.Clear();
            txtName.Clear();
        }
    }
}