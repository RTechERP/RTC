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
    public partial class frmAddGroupSalesUser : _Forms
    {
        public GroupSalesUserModel SalesUserModel = new GroupSalesUserModel();
        public int group;
        public frmAddGroupSalesUser()
        {
            InitializeComponent();
        }

        private void frmAddGroupSalesUser_Load(object sender, EventArgs e)
        {
            loadUser();
            loadGroupSale();
            loadSaleUserType();
            cbGroupSale.EditValue = group;
            if (SalesUserModel.ID > 0)
            {
                cbSaleUserType.EditValue = SalesUserModel.SaleUserTypeID;
                cbGroupSale.EditValue = SalesUserModel.GroupSalesID;
                cbUser.EditValue = SalesUserModel.UserID;
                cbUser.ReadOnly = cbGroupSale.ReadOnly = true;
            }
            else addStaff();
        }

        void addStaff()
        {
            string ID = TextUtils.ToString(grvData.GetRowCellValue(0, colID));
            txtID.Text = ID;

        }
        void loadUser()
        {
            DataTable dt = TextUtils.Select("Select ID,FullName From Users Order By ID desc");
            cbUser.Properties.ValueMember = "ID";
            cbUser.Properties.DisplayMember = "FullName";
            cbUser.Properties.DataSource = dt;

        }
        void loadGroupSale()
        {
            DataTable dt = TextUtils.Select("Select ID,GroupSalesName From GroupSales Order By ID desc");
            cbGroupSale.Properties.ValueMember = "ID";
            cbGroupSale.Properties.DisplayMember = "GroupSalesName";
            cbGroupSale.Properties.DataSource = dt;
        }
        void loadSaleUserType()
        {
            DataTable dt = TextUtils.Select("Select ID,SaleUserTypeName From SaleUserType Order By ID desc");
            cbSaleUserType.Properties.ValueMember = "ID";
            cbSaleUserType.Properties.DisplayMember = "SaleUserTypeName";
            cbSaleUserType.Properties.DataSource = dt;
        }
        bool Validate()
        {
            if (cbGroupSale.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng chọn nhóm Sale !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (cbSaleUserType.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng chọn loại nhân viên !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (cbUser.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng chọn nhân viên !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }
        void Save()
        {
            if (!Validate()) return;
            GroupSalesUserModel model = new GroupSalesUserModel();
            if (SalesUserModel.ID > 0)
            {
                model = (GroupSalesUserModel)GroupSalesUserBO.Instance.FindByPK(SalesUserModel.ID);
            }

            model.UserID = TextUtils.ToInt(cbUser.EditValue);
            model.GroupSalesID = TextUtils.ToInt(cbGroupSale.EditValue);
            model.SaleUserTypeID = TextUtils.ToInt(cbSaleUserType.EditValue);
            model.ParentID = TextUtils.ToInt(txtID.Text);
            if (SalesUserModel.ID > 0)
            {
                GroupSalesUserBO.Instance.Update(model);
            }
            else
            {
                GroupSalesUserBO.Instance.Insert(model);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
            this.Close();
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            Save();
            cbUser.Text = "";
            cbGroupSale.Text = "";
            cbSaleUserType.Text = "";
            txtID.Text = "";
        }

        private void frmAddGroupSalesUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void cbGroupSale_EditValueChanged(object sender, EventArgs e)
        {

            DataSet ds = TextUtils.LoadDataSetFromSP("spGetEmployeeManager", new string[] { "@group" }, new object[] { TextUtils.ToInt(cbGroupSale.EditValue) });
            grdData.DataSource = ds.Tables[1];
        }

        private void mnuMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
