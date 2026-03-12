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

namespace Forms.Sale.RequestInvoice
{
    public partial class frmAmendReasonRequestInvoice: _Forms
    {
        public int RequestInvoiceID = 0;
        public string AmendReason = "";
        public frmAmendReasonRequestInvoice()
        {
            InitializeComponent();
        }

        private void frmAmendReasonRequestInvoice_Load(object sender, EventArgs e)
        {
            txtAmendReason.Text = AmendReason;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            AmendReason = txtAmendReason.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCloseNew_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
