using BMS;
using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmCustomerMajor : _Forms
    {
        public CustomerSpecializationModel major = new CustomerSpecializationModel();
        public frmCustomerMajor()
        {
            InitializeComponent();
            
        }
        private void frmCustomerMajor_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            List<CustomerSpecializationModel> listData = SQLHelper<CustomerSpecializationModel>.FindAll().ToList();
            txtSTT.Value = listData.Count() + 1;

            if (major.ID > 0) txtSTT.Value = major.STT;
            txtCode.Text = major.Code ?? "";
            txtName.Text = major.Name ?? "";
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
                major = new CustomerSpecializationModel();
                txtCode.Clear();
                txtName.Clear();
                txtSTT.Value = (TextUtils.ToInt(txtSTT.Text) + 1);
            }
        }


        private bool SaveData()
        {
            try
            {
                if (!CheckValidate()) return false;

                if (major.ID > 0) SQLHelper<CustomerSpecializationModel>.Update(major);
                else SQLHelper<CustomerSpecializationModel>.Insert(major);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private bool CheckValidate()
        {
            string pattern = @"^[a-zA-Z0-9_-]+$";
            Regex regex = new Regex(pattern);

            major.Code = txtCode.Text.ToString();
            major.Name = txtName.Text.ToString();
            major.STT = TextUtils.ToInt(txtSTT.Text);

            if (string.IsNullOrEmpty(txtCode.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Mã ngành nghề!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                bool isCheck = regex.IsMatch(txtCode.Text.Trim());
                if (!isCheck)
                {
                    MessageBox.Show("Mã ngành nghề chỉ chứa chữ cái tiếng Anh và số!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }

            List<CustomerSpecializationModel> checkList = SQLHelper<CustomerSpecializationModel>.FindAll().Where(p => p.ID != major.ID && p.Code == major.Code).ToList();
            if (checkList.Count > 0)
            {
                MessageBox.Show($"Mã ngành nghề {txtCode.Text.Trim()} đã được sử dụng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Tên ngành nghề!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (txtSTT.Value <= 0)
            {
                MessageBox.Show("Vui lòng nhập STT > 0!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            return true;
        }

        private void frmCustomerMajor_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

       
    }
}
