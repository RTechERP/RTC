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
    public partial class frmEmployeeDelete : _Forms
    {
        public EmployeeModel employee = new EmployeeModel();
        public frmEmployeeDelete()
        {
            InitializeComponent();
        }

        private void frmEmployeeDelete_Load(object sender, EventArgs e)
        {
            dtpEndWorking.Value = DateTime.Now;
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            try
            {
                if (employee != null)
                {
                    employee.Status = 1;
                    employee.EndWorking = dtpEndWorking.Value;
                    employee.ReasonDeleted = txtReasonDeleted.Text.Trim();
                    //EmployeeBO.Instance.Update(employee);
                    SQLHelper<EmployeeModel>.Update(employee);

                    //UsersModel user = (UsersModel)UsersBO.Instance.FindByPK(TextUtils.ToInt(employee.UserID));
                    UsersModel user = SQLHelper<UsersModel>.FindByID(TextUtils.ToInt(employee.UserID));
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

        private void frmEmployeeDelete_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
