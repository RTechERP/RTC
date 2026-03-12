using BMS.Business;
using BMS.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Forms.Classes.cGlobVar;

namespace BMS
{
    public partial class frmUploadFile : _Forms
    {
        //public int projectID = 1;
        public ProjectModel project = new ProjectModel();

        List<ProjectFileModel> listFile = new List<ProjectFileModel>();
        DataTable dt = new DataTable();


        public frmUploadFile()
        {
            InitializeComponent();
        }

        private void frmUploadFile_Load(object sender, EventArgs e)
        {
            this.Text += $" - {project.ProjectCode}";
            loadData();
        }

        void loadData()
        {
            //listFile = SQLHelper<ProjectFileModel>.FindByAttribute("ProjectID", project.ID);
            dt = TextUtils.LoadDataFromSP("spGetProjectFile", "A", new string[] { "@ProjectID" }, new object[] { project.ID });
            grdData.DataSource = dt;
            grvData.RefreshData();
        }

        void saveData(List<ProjectFileModel> fileInsert, List<ProjectFileModel> fileUpdate)
        {
            string folderUpload = project.CreatedDate.Value.Year + "/" + project.ProjectCode;
            //for (int i = 0; i < fileInsert.; i++)
            //{
            //    fileInsert[i].ID = (int)ProjectFileBO.Instance.Insert(fileInsert[i]);
            //    string folder = fileInsert[i].FileTypeFolder == 1 ? $@"{folderUpload}/Video" : $@"{folderUpload}/Image";
            //    UploadFile(fileInsert[i].OriginPath, fileInsert[i].ID, folder);
            //    progressBar1.Invoke((Action)(() => { progressBar1.Value = i; }));
            //}

            foreach (ProjectFileModel item in fileInsert)
            {

                item.ID = (int)ProjectFileBO.Instance.Insert(item);
                string folder = item.FileTypeFolder == 1 ? $@"{folderUpload}/Video" : $@"{folderUpload}/Image";
                UploadFile(item.OriginPath, item.ID,folder);
            }


            if (fileUpdate.Count > 0)
            {
                frmUploadFileExist frm = new frmUploadFileExist();
                frm.listFileExist = fileUpdate;
                frm.Show();
            }
        }

        void UploadFile(string filePath, int fileId,string folderUpload)
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

        private void btnUpload_Click(object sender, EventArgs e)
        {
            var regex = new Regex(@"^.*\.(mp4|mov|wmv)$");
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            
            dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png" +
                            "|Video Files|*.mp4;*.mov;*.wmv";

            List<ProjectFileModel> listFileInsert = new List<ProjectFileModel>();
            List<ProjectFileModel> listFileExist = new List<ProjectFileModel>();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var listSelected = dialog.FileNames;
                try
                {
                    foreach (var file in listSelected)
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        if (fileInfo.Length > 200000000)
                        {
                            MessageBox.Show($"File [{fileInfo.Name}] vượt quá kích thước cho phép.\nVui lòng kiểm tra lại!", "Thông báo");
                            continue;
                        }

                        ProjectFileModel projectFile = new ProjectFileModel();
                        projectFile.ProjectID = project.ID;
                        projectFile.FileName = fileInfo.Name;
                        projectFile.FileType = fileInfo.Extension;
                        projectFile.Size = fileInfo.Length;
                        projectFile.OriginPath = file;
                        projectFile.FileTypeFolder = regex.IsMatch(fileInfo.Extension) ? 1 : 2;

                        string folderUpload = project.CreatedDate.Value.Year + "/" + project.ProjectCode;
                        string folder = projectFile.FileTypeFolder == 1 ? $@"{folderUpload}/Video" : $@"{folderUpload}/Image";
                        projectFile.PathServer = "ftp://192.168.1.2:22/" + folder;

                        var fileExist = listFile.Where(x => x.FileName == fileInfo.Name).ToList();

                        var row = dt.Select($"FileName = '{fileInfo.Name}'");
                        if (row.Length > 0)
                        {
                            listFileExist.Add(projectFile);
                        }
                        else
                        {
                            listFileInsert.Insert(0, projectFile);
                        }

                    }

                    saveData(listFileInsert, listFileExist);
                    loadData();
                    MessageBox.Show("Upload thành công!", "Thông báo");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo");
                }
            }
        }

        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            int[] rowSelected = grvData.GetSelectedRows();
            if (rowSelected.Length <= 0)
            {
                return;
            }

            string pathDelete = project.CreatedDate.Value.Year + "/" + project.ProjectCode;

            List<int> listID = new List<int>();
            List<string> filePath = new List<string>();
            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn xoá file đã chọn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                foreach (int row in rowSelected)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));


                    string folderName = TextUtils.ToString(grvData.GetRowCellValue(row, colFileTypeFolderText));
                    string extension = TextUtils.ToString(grvData.GetRowCellValue(row, colFileType));
                    if (id <= 0)
                    {
                        continue;
                    }

                    listID.Add(id);

                    pathDelete += $"/{folderName}/{id}{extension}";
                    filePath.Add(pathDelete);
                }

                string sql = $"DELETE dbo.ProjectFile WHERE ID IN ({string.Join(",", listID)})";
                TextUtils.ExcuteSQL(sql);

                //ftp.InitFTPQLSX();
                foreach (var item in filePath)
                {
                    ftp.DeleteFile(item);
                }

                loadData();
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            btnDeleteFile_Click(null, null);
        }

        private void btnCopyFileName_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(TextUtils.ToString(grvData.GetFocusedRowCellValue(colFileName)));
        }

        private void btnCopyPath_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(TextUtils.ToString(grvData.GetFocusedRowCellValue(colOriginPath)));
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn)));
                e.Handled = true;
            }
        }


        private void btnDownload_Click(object sender, EventArgs e)
        {
            int[] rowSelected = grvData.GetSelectedRows();
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string folderUpload = project.CreatedDate.Value.Year + "/" + project.ProjectCode;
                try
                {
                    foreach (int row in rowSelected)
                    {
                        string fileName = TextUtils.ToString(grvData.GetRowCellValue(row, colFileName));

                        string id = TextUtils.ToString(grvData.GetRowCellValue(row, colID));
                        string extension = TextUtils.ToString(grvData.GetRowCellValue(row, colFileType));
                        string folderType = TextUtils.ToString(grvData.GetRowCellValue(row, colFileTypeFolderText));
                        string folder = $@"{folderUpload}/{folderType}";

                        string fileDownload = folder + $"/{id}{extension}";

                        if (!ftp.CheckExits(fileDownload))
                        {
                            continue;
                        }
                        ftp.DownloadFile(dialog.SelectedPath, fileName, fileDownload, progressBar1, new Label());
                    }
                    MessageBox.Show("Tải thành công!", "Thông báo");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo");
                }
            }
        }

        private void btnDownloadFile_Click(object sender, EventArgs e)
        {
            btnDownload_Click(null, null);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
