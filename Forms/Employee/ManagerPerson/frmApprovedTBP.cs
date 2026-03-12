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
    public partial class frmApprovedTBP : _Forms
    {

        public DataTable dtWFHUpdated = new DataTable();
        public frmApprovedTBP()
        {
            InitializeComponent();
        }

        private void frmApprovedTBP_Load(object sender, EventArgs e)
        {
            //grdData.DataSource = dtWFHs;
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            //grvData.BeginUpdate();

            this.Close();
        }

        private void frmApprovedTBP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmApprovedTBP_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
    }
}
