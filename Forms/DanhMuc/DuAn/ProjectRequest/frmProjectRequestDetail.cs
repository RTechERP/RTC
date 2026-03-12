using BMS.Model;
using BMS.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmProjectRequestDetail : _Forms
    {
        public ProjectRequestModel projectRequest = new ProjectRequestModel();
        public int projectId = 0;

        //string[] listFileUpload;

        List<FileInfo> listFileUpload = new List<FileInfo>();
        List<ProjectRequestFileModel> listFiles = new List<ProjectRequestFileModel>();
        List<ProjectRequestFileModel> listIdDel = new List<ProjectRequestFileModel>();
        public frmProjectRequestDetail()
        {
            InitializeComponent();
        }

        private void frmProjectRequestDetail_Load(object sender, EventArgs e)
        {
            LoadProject();
            LoadData();
        }

        void LoadProject()
        {
            List<ProjectModel> list = SQLHelper<ProjectModel>.FindAll().OrderByDescending(x => x.ID).ToList();
            cboProject.Properties.ValueMember = "ID";
            cboProject.Properties.DisplayMember = "ProjectCode";
            cboProject.Properties.DataSource = list;
            cboProject.EditValue = projectId;
        }

        void LoadData()
        {
            if (projectRequest.ID > 0)
            {
                cboProject.EditValue = projectRequest.ProjectID;
                txtCodeRequest.Text = projectRequest.CodeRequest;
                txtStt.Value = projectRequest.STT;

            }
            else
            {
                GetRequestCode();
            }

            txtContentRequest.Text = projectRequest.ContentRequest;
            dtpDateRequest.Value = projectRequest.DateRequest.HasValue ? projectRequest.DateRequest.Value : DateTime.Now;


            listFiles = SQLHelper<ProjectRequestFileModel>.FindByAttribute("ProjectRequestID", projectRequest.ID);
            LoadFile(listFiles);
        }

        void GetRequestCode()
        {
            projectId = TextUtils.ToInt(cboProject.EditValue);
            var requests = SQLHelper<ProjectRequestModel>.FindAll();
            var requestByProject = requests.Where(x => x.ProjectID == projectId).ToList();
            txtStt.Value = requestByProject.Count > 0 ? requestByProject.Max(x => x.STT) + 1 : 1;
            txtCodeRequest.Text = $"PRQ{requestByProject.Count + 1}";
        }
        bool CheckValidate()
        {
            int projectId = TextUtils.ToInt(cboProject.EditValue);
            if (projectId <= 0)
            {
                MessageBox.Show("Vui lòng nhập Dự án!", "Thông báo");
                return false;
            }

            if (string.IsNullOrEmpty(txtCodeRequest.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Mã yêu cầu!", "Thông báo");
                return false;
            }
            else
            {
                var exp1 = new Expression("ID", projectRequest.ID, "<>");
                var exp2 = new Expression("CodeRequest", txtCodeRequest.Text.Trim());
                var exp3 = new Expression("ProjectID", projectId);
                var requests = SQLHelper<ProjectRequestModel>.FindByExpression(exp1.And(exp2).And(exp3));
                if (requests.Count > 0)
                {
                    MessageBox.Show($"Mã yêu cầu [{txtCodeRequest.Text}] đã tồn tại!", "Thông báo");
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtContentRequest.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Nội dung!", "Thông báo");
                return false;
            }
            return true;
        }

        bool SaveData()
        {
            if (!CheckValidate()) return false;

            projectRequest.ProjectID = TextUtils.ToInt(cboProject.EditValue);
            projectRequest.DateRequest = dtpDateRequest.Value;
            projectRequest.CodeRequest = txtCodeRequest.Text.Trim();
            projectRequest.STT = (int)txtStt.Value;
            projectRequest.ContentRequest = txtContentRequest.Text.Trim();
            if (projectRequest.ID > 0)
            {
                SQLHelper<ProjectRequestModel>.Update(projectRequest);
            }
            else
            {
                projectRequest.ID = SQLHelper<ProjectRequestModel>.Insert(projectRequest).ID;
            }

            UploadFile(projectRequest.ID);

            if (listIdDel.Count > 0)
            {
                SQLHelper<ProjectRequestFileModel>.DeleteListModel(listIdDel);
                listIdDel.Clear();
            }
            return true;

        }

        void LoadFile(List<ProjectRequestFileModel> listFiles)
        {
            grdDataRequestFile.DataSource = listFiles;
            grvDataRequestFile.RefreshData();
        }

        async void UploadFile(int requestId)
        {
            try
            {
                if (listFileUpload.Count() <= 0) return;
                //string pathUpload = @"\\192.168.1.190\Common\08. SOFTWARES\LeTheAnh\DemoUploadFile";
                string pathUpload = @"\\192.168.1.190\duan\Projects";
                if (Global.IsOnline) pathUpload = @"\\113.190.234.64\duan\Projects";

                int projectId = TextUtils.ToInt(cboProject.EditValue);
                ProjectModel project = SQLHelper<ProjectModel>.FindByID(projectId);
                if (project == null) return;
                string pathPatern = $@"{project.CreatedDate.Value.Year}\{project.ProjectCode}\TaiLieuChung\YeuCauDuAn(REV02)";

                pathUpload = Path.Combine(pathUpload, pathPatern);
                if (!Directory.Exists(pathUpload))
                {
                    Directory.CreateDirectory(pathUpload);
                }

                string urlUpload = $"http://113.190.234.64:8083/api/Home/uploadfile?path={pathUpload}";

                foreach (FileInfo file in listFileUpload)
                {
                    ProjectRequestFileModel requestFile = SQLHelper<ProjectRequestFileModel>.FindByID(0);
                    if (requestFile == null) requestFile = new ProjectRequestFileModel();

                    requestFile.ProjectRequestID = requestId;
                    requestFile.FileNameOrigin = file.Name;
                    requestFile.Extension = file.Extension;
                    requestFile.OriginPath = file.DirectoryName;
                    requestFile.ServerPath = pathUpload;

                    if (requestFile.ID > 0)
                    {
                        SQLHelper<ProjectRequestFileModel>.Update(requestFile);
                    }
                    else
                    {
                        requestFile.ID = SQLHelper<ProjectRequestFileModel>.Insert(requestFile).ID;
                    }


                    HttpClient client = new HttpClient();
                    MultipartFormDataContent formData = new MultipartFormDataContent();

                    formData.Add(new StreamContent(File.OpenRead(file.FullName)), "file", $"{requestFile.ID}{file.Extension}");
                    var postData = await client.PostAsync(urlUpload, formData);
                    var result = postData.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                projectRequest = new ProjectRequestModel();
                LoadData();
                txtContentRequest.Focus();
            }
        }

        private void frmProjectRequestDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }


        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in dialog.FileNames)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    ProjectRequestFileModel fileRequest = new ProjectRequestFileModel()
                    {
                        FileNameOrigin = fileInfo.Name,
                        OriginPath = fileInfo.DirectoryName
                    };

                    listFiles.Insert(0, fileRequest);
                    listFileUpload.Add(fileInfo);
                }

                LoadFile(listFiles);
            }
        }



        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvDataRequestFile.GetFocusedRowCellValue("ID"));
            if (id <= 0) return;
            string fileName = TextUtils.ToString(grvDataRequestFile.GetFocusedRowCellValue("FileNameOrigin"));
            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá file [{fileName}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                ProjectRequestFileModel file = SQLHelper<ProjectRequestFileModel>.FindByID(id);
                if (file == null) return;
                listIdDel.Add(file);
                grvDataRequestFile.DeleteSelectedRows();

            }
        }

        private void cboProject_EditValueChanged(object sender, EventArgs e)
        {
            GetRequestCode();
        }
    }
}
