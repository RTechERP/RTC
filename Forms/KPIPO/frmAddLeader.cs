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
    public partial class frmAddLeader : _Forms
    {
        public GroupSalesUserModel SalesUserModel = new GroupSalesUserModel();
        public int ID;
        public int IDuser;
        
        public frmAddLeader()
        {
            InitializeComponent();
        }

        private void frmAddGroupSalesUser_Load(object sender, EventArgs e)
        {
            loadcbStaff();
        }
 
        void loadcbStaff()
        {
            //ID = TextUtils.ToInt($"Select GroupSalesID From GroupSalesUser where UserID={cb}")
            DataTable dt = TextUtils.Select($"select g.*, u.FullName, s.GroupSalesName, t.SaleUserTypeName From GroupSalesUser g Inner join Users u on u.ID = g.UserID Inner join GroupSales s on s.ID = g.GroupSalesID Inner join SaleUserType t on t.ID = g.SaleUserTypeID where g.GroupSalesID ={ID} And ParentID=0 and t.SaleUserTypeCode='Sta'");
            cbStaff.Properties.ValueMember = "ID";
            cbStaff.Properties.DisplayMember = "FullName";
            cbStaff.Properties.DataSource = dt;
        }

        private void frmAddGroupSalesUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            TextUtils.ExcuteSQL($"Update GroupSalesUser set ParentID ={IDuser} where ID In ({cbStaff.EditValue})");
            DialogResult = DialogResult.OK;
        }
    }
}
