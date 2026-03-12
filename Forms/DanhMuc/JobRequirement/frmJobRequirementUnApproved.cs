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
    public partial class frmJobRequirementUnApproved : _Forms
    {
        bool isComment = false;
        public frmJobRequirementUnApproved(bool isComment)
        {
            
            InitializeComponent();

            this.isComment = isComment;
        }

        private void frmJobRequirementUnApproved_Load(object sender, EventArgs e)
        {
            this.Text = isComment ? "GHI CHÚ" : "HỦY DUYỆT YÊU CẦU";
            label1.Text = isComment ? "Ghi chú":"Lý do huỷ";
            label2.Visible = !isComment;
            this.AcceptButton = isComment ? null : btnYes;

        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            if (!isComment)
            {
                if (label2.Visible && string.IsNullOrEmpty(txtReasonCancel.Text.Trim()))
                {
                    MessageBox.Show("Vui lòng nhập Lý do huỷ!", "Thông báo");
                    return;
                }
            }
            

            this.DialogResult = DialogResult.OK;
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        

        private void frmJobRequirementUnApproved_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void frmJobRequirementUnApproved_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
