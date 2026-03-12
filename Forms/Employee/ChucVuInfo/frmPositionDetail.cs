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
    public partial class frmPositionDetail : _Forms
    {
        public bool HD;
        public EmployeeChucVuHDModel ModelHD = new EmployeeChucVuHDModel();
        public bool NB;
        public EmployeeChucVuModel ModelNB = new EmployeeChucVuModel();

        public frmPositionDetail()
        {
            InitializeComponent();
        }
        
        private void frmPositionDetail_Load(object sender, EventArgs e)
        {
            laodData();
        }
        void laodData()
        {
            if(HD)
            {
                txtCode.Text = ModelHD.Code;
                txtName.Text = ModelHD.Name;
                //label1.Visible = true;
            }
            if (NB)
            {
                txtCode.Text = ModelNB.Code;
                txtName.Text = ModelNB.Name;
                label3.Text = "Chức vụ nội bộ";
            }
        }

        bool validate()
        {
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập tên chức vụ", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (string.IsNullOrEmpty(txtCode.Text.Trim()))
            {
                MessageBox.Show("Vui nhập Mã chức vụ", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }

        bool SaveData()
        {
            if (!validate())
            {
                return false;
            }
            if(HD)
            {
                ModelHD.Code = txtCode.Text;
                ModelHD.Name = txtName.Text;
                if (ModelHD.ID > 0)
                {
                    EmployeeChucVuHDBO.Instance.Update(ModelHD);
                }
                else
                {
                    EmployeeChucVuHDBO.Instance.Insert(ModelHD);
                }
            }
            if (NB)
            {
                ModelNB.Code = txtCode.Text;
                ModelNB.Name = txtName.Text;
                if (ModelNB.ID > 0)
                {
                    EmployeeChucVuBO.Instance.Update(ModelNB);
                }
                else
                {
                    EmployeeChucVuBO.Instance.Insert(ModelNB);
                }
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

        
    }
}
