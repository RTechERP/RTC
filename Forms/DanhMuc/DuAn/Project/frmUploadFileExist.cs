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

namespace BMS
{
    public partial class frmUploadFileExist : _Forms
    {
        public List<ProjectFileModel> listFileExist = new List<ProjectFileModel>();
        public ProjectModel project = new ProjectModel();

        
        public frmUploadFileExist()
        {
            InitializeComponent();
        }

        private void frmUploadFileExist_Load(object sender, EventArgs e)
        {
            this.Text += $" - {project.ProjectCode}";
            loadData();
        }

        void loadData()
        {
            checkedListBoxControl1.DataSource = listFileExist;
            checkedListBoxControl1.DisplayMember = "FileName";
            checkedListBoxControl1.ValueMember = "ID";
        }

        void saveData(List<ProjectFileModel> listFile)
        {
            if (listFile.Count <= 0)
            {
                return;
            }

            string folderUpload = project.CreatedDate.Value.Year + "/" + project.ProjectCode;
            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn ghi đè file đã chọn không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialog == DialogResult.OK)
            {
                foreach (ProjectFileModel file in listFile)
                {
                    SQLHelper<ProjectFileModel>.Update(file);

                    string folder = file.FileTypeFolder == 1 ? $@"{folderUpload}/Video" : $@"{folderUpload}/Image";
                    UploadFile(file.OriginPath, file.ID, folder);
                }
            }
        }

        void UploadFile(string filePath, int fileId, string folderUpload)
        {
            //ftp.InitFTPQLSX();
            try
            {
                string saveName = $"{fileId}{Path.GetExtension(filePath)}";
                string fileUpload = Path.Combine(Path.GetDirectoryName(filePath), saveName);
                File.Copy(filePath, fileUpload);

                if (ftp.UploadFileWithStatus(fileUpload, folderUpload))
                {
                    File.Delete(fileUpload);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            saveData(listFileExist);
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            var selectedFile = checkedListBoxControl1.CheckedItems;
            List<ProjectFileModel> listFileSelected = new List<ProjectFileModel>();
            foreach (ProjectFileModel item in selectedFile)
            {
                listFileSelected.Add(item);
            }
            saveData(listFileSelected);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
