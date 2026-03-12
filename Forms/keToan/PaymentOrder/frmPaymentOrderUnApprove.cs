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
    public partial class frmPaymentOrderUnApprove : _Forms
    {
        public frmPaymentOrderUnApprove()
        {
            InitializeComponent();
        }

        private void frmPaymentOrderUnApprove_Load(object sender, EventArgs e)
        {

        }

        private void btnYes_Click(object sender, EventArgs e)
        {

            if (label2.Visible && string.IsNullOrEmpty(txtReasonCancel.Text.Trim()))
            {
                MessageBox.Show($"Vui lòng nhập {label2.Text}!", "Thông báo");
                return;
            }

            this.DialogResult = DialogResult.OK;
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
