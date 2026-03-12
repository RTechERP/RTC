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
    public partial class frmGroupFileDetail : _Forms
    {
        public GroupFileModel groupFileModel = new GroupFileModel();

        public frmGroupFileDetail()
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
            if (groupFileModel.ID > 0)
            {
                txtGroupFileCode.Text = TextUtils.ToString(groupFileModel.GroupFileCode);
                txtGroupFileName.Text = TextUtils.ToString(groupFileModel.GroupFileName);
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
            txtGroupFileCode.Clear();
            txtGroupFileName.Clear();
            groupFileModel = new GroupFileModel();
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
                groupFileModel.GroupFileCode = TextUtils.ToString(txtGroupFileCode.Text.Trim());
                groupFileModel.GroupFileName = TextUtils.ToString(txtGroupFileName.Text.Trim());
                if (groupFileModel.ID > 0)
                {
                    GroupFileBO.Instance.Update(groupFileModel);
                }
                else
                {
                    GroupFileBO.Instance.Insert(groupFileModel);
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
            if (txtGroupFileCode.Text.Trim() == "")
            {
                MessageBox.Show("Xin vui lòng điền mã nhóm file.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (txtGroupFileName.Text.Trim() == "")
            {
                MessageBox.Show("Xin vui lòng điền tên nhóm file.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            DataTable dt;
            if (groupFileModel.ID > 0)
            {
                dt = TextUtils.Select("select top 1 GroupFileCode from GroupFile where GroupFileCode = '" + txtGroupFileCode.Text + "' and ID <> " + groupFileModel.ID);
            }
            else
            {
                dt = TextUtils.Select("select top 1 GroupFileCode from GroupFile where GroupFileCode = '" + txtGroupFileCode.Text + "'");
            }
            return true;
        }

        private void frmGroupFileDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
