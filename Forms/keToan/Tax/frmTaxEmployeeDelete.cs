using BMS.Business;
using BMS.Model;
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
    public partial class frmTaxEmployeeDelete : _Forms
    {
        public TaxEmployeeModel taxEmployee = new TaxEmployeeModel();
        public frmTaxEmployeeDelete()
        {
            InitializeComponent();
        }

        private void frmTaxEmployeeDelete_Load(object sender, EventArgs e)
        {
            dtpEndWorking.Value = DateTime.Now;
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            try
            {
                if (taxEmployee != null)
                {
                    taxEmployee.Status = 1;
                    taxEmployee.EndWorking = dtpEndWorking.Value;
                    TaxEmployeeBO.Instance.Update(taxEmployee);

                    UsersModel user = (UsersModel)UsersBO.Instance.FindByPK(taxEmployee.UserID);
                    if (user != null)
                    {
                        user.Status = 1;
                        UsersBO.Instance.Update(user);
                    }
                }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
