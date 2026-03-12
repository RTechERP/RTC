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

namespace BMS
{
    public partial class frmToolGetEmailEmployee : _Forms
    {
        public frmToolGetEmailEmployee()
        {
            InitializeComponent();
        }

        private void frmToolGetEmailEmployee_Load(object sender, EventArgs e)
        {
            //LOAD CBX phòng ban
            DataTable dt = new DataTable();
            dt = TextUtils.Select("SELECT * FROM dbo.Department");
            cbxDepartment.DataSource = dt;
            cbxDepartment.DisplayMember = "Name";
            cbxDepartment.ValueMember = "ID";
        }

        private void GetEmailEmployee_Click(object sender, EventArgs e)
        {
            int idDepartment = TextUtils.ToInt(cbxDepartment.SelectedValue);
            DataTable dt = new DataTable();
            dt = TextUtils.LoadDataFromSP("spGetEmailEmployeeFromDepartment", "A", new string[] { "@IDDepartment" }, new object[] { idDepartment });
          
            if (dt.Rows.Count > 0)
            {
                rtbValueEmail.Text = TextUtils.ToString(dt.Rows[0]["EmailSum"]);
            }
        }
    }
}
