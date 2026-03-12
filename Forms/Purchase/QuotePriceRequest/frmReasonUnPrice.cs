using BMS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms.Purchase
{
    public partial class frmReasonUnPrice : _Forms
    {
        public string ReasonUnPrice = "";
        public frmReasonUnPrice()
        {
            InitializeComponent();
        }

        private void frmReasonUnPrice_Load(object sender, EventArgs e)
        {
            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReason.Text)) { 
                MessageBox.Show("Hãy nhập lý do Từ chối báo giá!");
                return;
            }
            ReasonUnPrice = txtReason.Text.Trim();
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void frmReasonUnPrice_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
