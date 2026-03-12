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
    public partial class frmAddMainIndex : _Forms
    {
        public frmAddMainIndex()
        {
            InitializeComponent();
        }
        bool Save()
        {
            if (string.IsNullOrEmpty(txtMainIndex.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên loại nhóm!");
                return false;
            }
            else
            {
                MainIndexModel model = new MainIndexModel();
                model.MainGroup = 8;
                model.MainIndex = txtMainIndex.Text;
                SQLHelper<MainIndexModel>.Insert(model);
                return true;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save()) this.DialogResult = DialogResult.OK;
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (Save()) txtMainIndex.Text = "";
        }
    }
}