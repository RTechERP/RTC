using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMS;
using BMS.Business;
using BMS.Model;

namespace Forms.TB
{
    public partial class frmProductLocationDetails : _Forms
    {
        public ProductLocationModel location = new ProductLocationModel(); 
        public frmProductLocationDetails()
        {
            InitializeComponent();
        }

        private void frmProductLocationDetails_Load(object sender, EventArgs e)
        {
            if(location.ID > 0)
            {
                txtLocationCode.Text = location.LocationCode;
                txtlocationName.Text = location.LocationName;
            }
        }
        bool ValidateForm()
        {
            if(string.IsNullOrEmpty(txtLocationCode.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {

                int id = TextUtils.ToInt(TextUtils.ExcuteScalar($"Select top 1 ID from ProductLocation Where LocationCode = '{txtLocationCode.Text.Trim()}'"));
                if (id > 0)
                {
                    MessageBox.Show($"Đã tồn tại mã vị trí {txtLocationCode.Text}!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }


            }
            if (string.IsNullOrEmpty(txtlocationName.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }
        bool save()
        {
            if (!ValidateForm()) return false;

            location.LocationCode = txtLocationCode.Text.Trim();
            location.LocationName = txtlocationName.Text.Trim();

            if(location.ID > 0)
            {
                ProductLocationBO.Instance.Update(location);
            }
            else
            {
                ProductLocationBO.Instance.Insert(location);
            }

            return true;

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!save()) return;
            MessageBox.Show("Lưu thông tin thành công!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
        }
    }
}
