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
    public partial class frmUpdateVehicleMoney : _Forms
    {
        public frmUpdateVehicleMoney()
        {
            InitializeComponent();
        }

        private void frmUpdateVehicleMoney_Load(object sender, EventArgs e)
        {

        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToDecimal(textEdit2.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhâp Số tiền!", "Thông báo");
                return;
            }

            this.DialogResult = DialogResult.OK;
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUpdateVehicleMoney_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void frmUpdateVehicleMoney_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
