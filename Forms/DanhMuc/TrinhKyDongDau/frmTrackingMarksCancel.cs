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
    public partial class frmTrackingMarksCancel : _Forms
    {
        public frmTrackingMarksCancel()
        {
            InitializeComponent();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            string _str = txtReasonCancel.Text;
            if (string.IsNullOrWhiteSpace(_str))
            {
                MessageBox.Show("Vui lòng nhập Lý do hủy duyệt!", "Thông báo");
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
