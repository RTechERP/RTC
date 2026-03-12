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
    public partial class frmAdditionalReasonProblem : _Forms
    {

        public string resionProblem = "";
        public frmAdditionalReasonProblem()
        {
            InitializeComponent();
        }

        private void ckResionProblem_CheckedChanged(object sender, EventArgs e)
        {
            label1.Enabled = label2.Enabled = txtReasonProblem.Enabled = ckResionProblem.Checked;
            //btnNo.Enabled = !ckResionProblem.Checked;
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            if (ckResionProblem.Checked)
            {
                if(txtReasonProblem.Text.Trim() == "")
                {
                    MessageBox.Show($"Vui lòng nhập lý do phát sinh!", "Thông báo");
                    return;
                }
                else
                {
                    resionProblem = txtReasonProblem.Text.Trim();
                    this.DialogResult = DialogResult.OK;
                }
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void frmAdditionalReasonProblem_Load(object sender, EventArgs e)
        {
            ckResionProblem.Checked = true;
        }
    }
}
