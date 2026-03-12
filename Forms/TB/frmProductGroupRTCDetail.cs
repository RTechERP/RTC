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
    public partial class frmProductGroupRTCDetail : _Forms
    {
        public ProductGroupRTCModel Group = new ProductGroupRTCModel();
        public int warehouseID;
        public frmProductGroupRTCDetail(int WarehouseID)
        {
            InitializeComponent();
            this.warehouseID = WarehouseID;
        }
        /// <summary>
        /// load dữ liễu lên khi mở form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmGroupRTC_Load(object sender, EventArgs e)
        {
            if (frmProductRTC.EditClick == 1)
            {
                btnSaveNew.Visible = false;
            }
            loadData();
        }
        /// <summary>
        /// void loadData
        /// </summary>
        private void loadData()
        {
            if (Group.ID > 0)
            {
                txtCode.Text = Group.ProductGroupNo.ToString();
                txtName.Text = Group.ProductGroupName;
                txtSTT.Value = Group.NumberOrder;
                if (txtCode.Text == "0")
                {
                    txtCode.Clear();
                }
            }
        }
        /// <summary>
        /// void lưu dữ liệu
        /// </summary>
        private void SaveGroup()
        {
            if (!ValidateForm()) return;
            Group.ProductGroupName = txtName.Text;
            Group.ProductGroupNo = txtCode.Text.Trim();
            Group.NumberOrder = TextUtils.ToInt(txtSTT.Value);
            Group.WarehouseID = 1;
            if (Group.ID > 0 )
            {
                ProductGroupRTCBO.Instance.Update(Group);
            }
            else
            {
                ProductGroupRTCBO.Instance.Insert(Group);
            }
        }
        /// <summary>
        /// validate trước khi cất dữ liệu
        /// </summary>
        /// <returns></returns>
        bool ValidateForm()
        {
            if (txtCode.Text == "" || txtName.Text == "")
            {
                MessageBox.Show("Cần nhập đầy đủ thông tin vào các ô còn trống!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            DataTable dt;
            if (Group.ID > 0)
            {
                dt = TextUtils.Select("select top 1 ProductGroupName from ProductGroupNo where ProductGroupName = '" + txtCode.Text.Trim() + "' and ID <> " + Group.ID + $" AND WarehouseID = {warehouseID}");
            }
            else
            {
                dt = TextUtils.Select("select top 1 ProductGroupName from ProductGroupRTC where ProductGroupNo = '" + txtCode.Text.Trim() + "'" + $" AND WarehouseID = {warehouseID}");
            }
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Mã nhóm đã tồn tại, xin hãy nhập tên khác!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            return true;
        }
        
        /// <summary>
        /// event save dữ liệu group to database
        /// </summary>
        private void eventSaveGroup()
        {
            SaveGroup();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        /// <summary>
        /// event save when used btnSave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            eventSaveGroup();
        }
        /// <summary>
        /// event save when used shortcut keys ctrl+S
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eventSaveGroup();
        }
        /// <summary>
        /// save and creat new txtbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            SaveGroup();
            txtCode.Clear();
            txtName.Clear();
        }

        private void frmGroupRTC_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
