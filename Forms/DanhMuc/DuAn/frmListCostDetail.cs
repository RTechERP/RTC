using BMS.Business;
using BMS.Model;
using ExcelDataReader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;


namespace BMS
{
    public partial class frmListCostDetail : _Forms
    {
        public ListCostModel listCost = new ListCostModel();

        public frmListCostDetail()
        {
            InitializeComponent();

        }
        /// <summary>
        /// load dữ liệu lên khi load form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmGroupFileDetail_Load(object sender, EventArgs e)
        {
            loadProductTestDetail();
        }

        #region Methods
        /// <summary>
        /// load ProductTestDetail
        /// </summary>
        private void loadProductTestDetail()
        {
            if (listCost.ID > 0)
            {
                txtCostCode.Text = TextUtils.ToString(listCost.CostCode);
                txtCostName.Text = TextUtils.ToString(listCost.CostName);
            }
        }
        #endregion

        #region Buttons Events
        /// <summary>
        /// click button save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData()) this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// click button sve new
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            saveData();
            txtCostCode.Clear();
            txtCostName.Clear();
            listCost = new ListCostModel();
        }
        #endregion

        /// <summary>
        /// hàm save
        /// </summary>
        /// <returns></returns>
        bool saveData()
        {
            if (!ValidateForm()) return false;
            try
            {
                listCost.CostCode = TextUtils.ToString(txtCostCode.Text.Trim());
                listCost.CostName = TextUtils.ToString(txtCostName.Text.Trim());
                if (listCost.ID > 0)
                {
                    ListCostBO.Instance.Update(listCost);
                }
                else
                {
                    ListCostBO.Instance.Insert(listCost);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi Update", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return true;
        }

        /// <summary>
        /// check lỗi
        /// </summary>
        /// <returns></returns>
        bool ValidateForm()
        {
            if (txtCostCode.Text.Trim() == "")
            {
                MessageBox.Show("Xin vui lòng điền mã nhóm file.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (txtCostName.Text.Trim() == "")
            {
                MessageBox.Show("Xin vui lòng điền tên nhóm file.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            DataTable dt;
            if (listCost.ID > 0)
            {
                dt = TextUtils.Select("Select top 1 CostCode From ListCost Where CostCode = '" + txtCostCode.Text + "' and ID <> " + listCost.ID);
            }
            else
            {
                dt = TextUtils.Select("Select top 1 CostCode From ListCost Where CostCode = '" + txtCostCode.Text + "'");
            }
            return true;
        }

        private void frmGroupFileDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
