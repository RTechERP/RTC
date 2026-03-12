using BMS;
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
    public partial class frmTypeBusiness : _Forms
    {
        public EmployeeTypeBussinessModel model = new EmployeeTypeBussinessModel();
        public frmTypeBusiness()
        {
            InitializeComponent();
        }

        private void frmVehicleBussiness_Load(object sender, EventArgs e)
        {
            loadData();
        }
        void loadData()
        {
            txtCode.Text = model.TypeCode;
            txtName.Text = model.TypeName;
            txtCost.Text = TextUtils.ToString(model.Cost);
           
        }
       
        bool saveData()
        {
            if (!ValidateForm()) return false;

            model.TypeCode = txtCode.Text;
            model.TypeName = txtName.Text;
            model.Cost = TextUtils.ToDecimal(txtCost.Text);
          
            if (model.ID > 0)
            {
                EmployeeTypeBussinessBO.Instance.Update(model);
            }
            else
            {
                EmployeeTypeBussinessBO.Instance.Insert(model);
            }
            return true;
        }
        bool ValidateForm()
        {
            if (txtCode.Text == "")
            {
                MessageBox.Show("Vui lòng điền phí đi làm sớm trước 7H15", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (txtName.Text == "")
            {
                MessageBox.Show("Vui lòng điền phụ phí qua đêm", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(saveData())
            {
                this.DialogResult = DialogResult.OK;
            }    
        }

    }
}
