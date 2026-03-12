using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmGroup : _Forms
    {
        public GroupProductSaleModel productGroup = new GroupProductSaleModel();
        public frmGroup()
        {
            InitializeComponent();
        }
        /// <summary>
        /// load dữ liễu lên khi mở form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmProductGroupDetail_Load(object sender, EventArgs e)
        {
            loadData();
        }
        /// <summary>
        /// void loadData
        /// </summary>
        private void loadData()
        {
            DataTable dt = TextUtils.Select("Select * From ProductGroup ");
            cbGroup.Properties.DisplayMember = "ProductGroupName";
            cbGroup.Properties.ValueMember = "ID";
            cbGroup.Properties.DataSource = dt;
            txtName.Text = productGroup.GroupName;
            cbGroup.EditValue = productGroup.ProductGroupID;
        }
        /// <summary>
        /// void lưu dữ liệu
        /// </summary>
        private void SaveGroup()
        {
            if (!ValidateForm()) return;
            productGroup.GroupName = txtName.Text;
            productGroup.ProductGroupID =TextUtils.ToInt(cbGroup.EditValue);            
            if (productGroup.ID > 0 )
            {
                GroupProductSaleBO.Instance.Update(productGroup);
            }
            else
            {
                GroupProductSaleBO.Instance.Insert(productGroup);
            }
        }
        /// <summary>
        /// validate trước khi cất dữ liệu
        /// </summary>
        /// <returns></returns>
        bool ValidateForm()
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Cần nhập đầy đủ thông tin vào các ô còn trống!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }
      
        /// <summary>
        /// event save when used btnSave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveGroup();
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// save and creat new txtbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            SaveGroup();
            txtName.Clear();
        }

        private void frmProductGroupDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

    }
}
