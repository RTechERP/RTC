using BMS.Business;
using BMS.Model;
using System;
using System.Collections;
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
    public partial class frmContractDetail : _Forms
    {
        public EmployeeLoaiHDLDModel Model = new EmployeeLoaiHDLDModel();
        public frmContractDetail()
        {
            InitializeComponent();
        }
        private void frmContractDetail_Load(object sender, EventArgs e)
        {
            loadData();
        }
        void loadData()
        {
            if(Model.ID>0)
            {
                txtCode.Text = TextUtils.ToString(Model.Code);
                txtName.Text = TextUtils.ToString(Model.Name);
            }    
        }
        bool validate()
        {
            if (string.IsNullOrEmpty(txtCode.Text.Trim()))
            {
                MessageBox.Show("Vui nhập mã hợp đồng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                EmployeeLoaiHDLDModel model = (EmployeeLoaiHDLDModel)EmployeeLoaiHDLDBO.Instance.FindByCode("Code", $"{txtCode.Text}");
                if (model!=null && Model.ID == 0)
                {
                    MessageBox.Show("Mã hợp đồng đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
                
            }
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập tên hợp đồng lao động!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }

        bool SaveData()
        {
            if (!validate())
            {
                return false;
            }
            Model.Code = txtCode.Text;
            Model.Name = txtName.Text;
            if(Model.ID>0)
            {
                EmployeeLoaiHDLDBO.Instance.Update(Model);
            }
            else
            {
                EmployeeLoaiHDLDBO.Instance.Insert(Model);
            }    
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

       
    }
}


