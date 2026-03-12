using BMS;
using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms.DanhMuc.DuAn
{
    public partial class frmProjectTreeFolder : _Forms
    {
        public ProjectTreeFolderModel model;
        public int projectTypeID;

        public int idFolder;
        public frmProjectTreeFolder()
        {
            InitializeComponent();
        }

        private void frmProjectTreeFolder_Load(object sender, EventArgs e)
        {
            loadProjectTreeFolder();
            loadParentID();
        }

        void loadProjectTreeFolder()
        {
            if (model.ID > 0)
            {
                cboParentID.EditValue = model.ParentID;
                txtFolderName.Text = model.FolderName;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData()) this.DialogResult = DialogResult.OK;
        }


        /// <summary>
        /// Hàm save data
        /// </summary>
        /// <returns></returns>
        bool SaveData()
        {
            if (!validate())
            {
                return false;
            }
            try
            {
                model.ProjectTypeID = projectTypeID;
                model.ParentID = TextUtils.ToInt(cboParentID.EditValue);
                model.FolderName = txtFolderName.Text.Trim();
                if (model.ID > 0)
                {
                    ProjectTreeFolderBO.Instance.Update(model);

                    //string oldFolder = @"D:\RTCTechnical\Projects\2022\EMTEK.22.048\VISION\Images"; //Thư mục test local
                    //string newFolder = @"D:\RTCTechnical\Projects\2022\EMTEK.22.048\VISION\ImageNew";
                    //Directory.Move(oldFolder, newFolder);
                }
                else
                {
                    ProjectTreeFolderBO.Instance.Insert(model);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Lỗi Update", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return true;
        }


        bool validate()
        {
            if (string.IsNullOrEmpty(txtFolderName.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập tên folder !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Load folder name
        /// </summary>
        void loadParentID()
        {
            DataTable dt = TextUtils.Select("Select * from ProjectTreeFolder");
            cboParentID.Properties.ValueMember = "ID";
            cboParentID.Properties.DisplayMember = "FolderName";
            cboParentID.Properties.DataSource = dt;

            cboParentID.EditValue = idFolder;
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            SaveData();

            loadParentID();
            txtFolderName.Clear();
            //cboParentID.EditValue = 0;
            model = new ProjectTreeFolderModel();
        }
    }
}
