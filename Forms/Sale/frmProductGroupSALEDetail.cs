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
    public partial class frmProductGroupSALEDetail : _Forms
    {
        public ProductGroupSALEModel _productGroupSALEModel = new ProductGroupSALEModel();
        public frmProductGroupSALEDetail()
        {
            InitializeComponent();
        }
        /// <summary>
        /// load dữ liễu lên khi mở form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmGroupSALE_Load(object sender, EventArgs e)
        {
            if (frmProductSale.EditClick == 1)
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
            txtCode.Text = _productGroupSALEModel.MaNhom.ToString();
            txtName.Text = _productGroupSALEModel.TenNhom;
            if (txtCode.Text == "0")
            {
                txtCode.Clear();
            }
        }
        /// <summary>
        /// void lưu dữ liệu
        /// </summary>
        private void SaveGroup()
        {
            if (!ValidateForm()) return;
            _productGroupSALEModel.TenNhom = txtName.Text;
            _productGroupSALEModel.MaNhom = TextUtils.ToDecimal(txtCode.Text.Trim());
            if (_productGroupSALEModel.ID > 0 )
            {
                ProductGroupSALEBO.Instance.Update(_productGroupSALEModel);
            }
            else
            {
                ProductGroupSALEBO.Instance.Insert(_productGroupSALEModel);
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
            return true;
        }
        //private bool CheckGroup()
        //{
        //    if (txtCode.Text == "" || txtName.Text == "")
        //    {
        //        MessageBox.Show("Cần nhập đầy đủ thông tin vào các ô còn trống!",TextUtils.Caption,MessageBoxButtons.OK,MessageBoxIcon.Information);
        //        return false;
        //    }
        //    DataTable dt;
        //    if (_productGroupSALEModel.ID > 0)
        //    {
        //        dt = TextUtils.Select("select top 1 ProductGroupName from ProductGroupRTC where ProductGroupName = '" + txtName.Text.Trim() + "' and ID <> " + _productGroupSALEModel.ID);
        //    }
        //    else
        //    {
        //        dt = TextUtils.Select("select top 1 ProductGroupName from ProductGroupRTC where ProductGroupName = '" + txtName.Text.Trim() + "'");
        //    }
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            MessageBox.Show("Tên nhóm đã tồn tại, xin hãy nhập tên khác!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            return false;
        //        }
        //    }
        //    return true;
        //}
        //private bool CheckSameDataGroup()
        //{
        //    DataTable dt;
        //    if (_productGroupSALEModel.ID > 0 )
        //    {
        //        dt = TextUtils.Select("select top 1 ProductGroupName from ProductGroupRTC where ProductGroupName = '" + txtName.Text.Trim() + "' and ID <> " + _productGroupSALEModel.ID);
        //    }
        //    else
        //    {
        //        dt = TextUtils.Select("select top 1 ProductGroupName from ProductGroupRTC where ProductGroupName = '" + txtName.Text.Trim() + "'");
        //    }
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            MessageBox.Show("Tên nhóm đã tồn tại, xin hãy nhập tên khác!",TextUtils.Caption,MessageBoxButtons.OK,MessageBoxIcon.Information);
        //            return false;
        //        }
        //    }
        //    return true;
        //}
        /// <summary>
        /// event save dữ liệu _productGroupSALEModel to database
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

        private void frmGroupSALE_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

    }
}
