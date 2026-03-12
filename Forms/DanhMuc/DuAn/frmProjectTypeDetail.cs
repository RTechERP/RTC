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
    public partial class frmProjectTypeDetail : _Forms
    {
        public ProjectTypeModel projectType = new ProjectTypeModel();

        public frmProjectTypeDetail()
        {
            InitializeComponent();

        }
        /// <summary>
        /// load dữ liệu lên khi load form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmProjectTypeDetail_Load(object sender, EventArgs e)
        {
            loadProductTestDetail();
            loadParentID();
        }

        #region Methods
        /// <summary>
        /// load ProductTestDetail
        /// </summary>
        private void loadProductTestDetail()
        {
            if (projectType.ID > 0)
            {
                txtProjectTypeCode.Text = TextUtils.ToString(projectType.ProjectTypeCode);
                txtProjectTypeName.Text = TextUtils.ToString(projectType.ProjectTypeName);
                cboParentID.EditValue = projectType.ParentID;
                txtRootFolder.Text = TextUtils.ToString(projectType.RootFolder);
            }
        }
        #endregion


        void loadParentID()
        {
            DataTable dt = TextUtils.Select("Select * from ProjectType Where ParentID = 0");
            cboParentID.Properties.ValueMember = "ID";
            cboParentID.Properties.DisplayMember = "ProjectTypeName";
            cboParentID.Properties.DataSource = dt;
        }
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
            txtProjectTypeCode.Clear();
            txtProjectTypeName.Clear();
            projectType = new ProjectTypeModel();
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
                projectType.ProjectTypeCode = TextUtils.ToString(txtProjectTypeCode.Text.Trim());
                projectType.ProjectTypeName = TextUtils.ToString(txtProjectTypeName.Text.Trim());
                projectType.ParentID = TextUtils.ToInt(cboParentID.EditValue);
                projectType.RootFolder = TextUtils.ToString(txtRootFolder.Text);
                if (projectType.ID > 0)
                {
                    //ProjectTypeBO.Instance.Update(projectType);
                    SQLHelper<ProjectTypeModel>.Update(projectType);
                }
                else
                {
                    //ProjectTypeBO.Instance.Insert(projectType);
                    SQLHelper<ProjectTypeModel>.Insert(projectType);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi Update {ex.Message}\r\n{ex.ToString()}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return true;
        }

        /// <summary>
        /// check lỗi
        /// </summary>
        /// <returns></returns>
        bool ValidateForm()
        {
            if (txtProjectTypeCode.Text.Trim() == "")
            {
                MessageBox.Show("Xin vui lòng điền mã kiểu dự án", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (txtProjectTypeName.Text.Trim() == "")
            {
                MessageBox.Show("Xin vui lòng điền tên kiểu dự án", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            DataTable dt;
            if (projectType.ID > 0)
            {
                dt = TextUtils.Select("select top 1 ProjectTypeCode from ProjectType where ProjectTypeCode = '" + txtProjectTypeCode.Text + "' and ID <> " + projectType.ID);
            }
            else
            {
                dt = TextUtils.Select("select top 1 ProjectTypeCode from ProjectType where ProjectTypeCode = '" + txtProjectTypeCode.Text + "'");
            }
            return true;
        }

        private void frmProjectTypeDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnSelectRootFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browserDialog = new FolderBrowserDialog();
            if (browserDialog.ShowDialog() == DialogResult.OK)
            {
                txtRootFolder.Text = browserDialog.SelectedPath;
            }

        }
    }
}
