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
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmProjectSolutionDetail : _Forms
    {
        public ProjectSolutionModel projectSolution = new ProjectSolutionModel();
        public int projectId = 0;
        public int projectRequestId = 0;

        List<FileInfo> listFileUpload = new List<FileInfo>();
        List<ProjectSolutionFileModel> listFiles = new List<ProjectSolutionFileModel>();
        List<ProjectSolutionFileModel> listIdDel = new List<ProjectSolutionFileModel>();

        public Action SaveEvent;
        public frmProjectSolutionDetail()
        {
            InitializeComponent();
        }

        private void frmProjectSolutionDetail_Load(object sender, EventArgs e)
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

        void LoadProjectRequest()
        {
            projectId = TextUtils.ToInt(cboProject.EditValue);
            List<ProjectRequestModel> list = SQLHelper<ProjectRequestModel>.FindByAttribute("ProjectID", projectId).OrderByDescending(x => x.STT).ToList();
            cboProjectRequest.Properties.ValueMember = "ID";
            cboProjectRequest.Properties.DisplayMember = "CodeRequest";
            cboProjectRequest.Properties.DataSource = list;
            cboProjectRequest.EditValue = projectRequestId;
        }

        void LoadData()
        {
            if (projectSolution.ID > 0)
            {
                cboProjectRequest.EditValue = projectSolution.ProjectRequestID;
                txtCodeSolution.Text = projectSolution.CodeSolution;
                txtStt.Value = projectSolution.STT;
            }
            else
            {
                GetSolutionCode();
            }

            chkStatusSolution.Checked = projectSolution.StatusSolution == 1;
            txtContentSolution.Text = projectSolution.ContentSolution;
            dtpDateSolution.Value = projectSolution.DateSolution.HasValue ? projectSolution.DateSolution.Value : DateTime.Now;

            listFiles = SQLHelper<ProjectSolutionFileModel>.FindByAttribute("ProjectSolutionID", projectSolution.ID);
            LoadFile(listFiles);
        }


        void GetSolutionCode()
        {
            projectRequestId = TextUtils.ToInt(cboProjectRequest.EditValue);
            var solutions = SQLHelper<ProjectSolutionModel>.FindAll();
            var solutionRequests = solutions.Where(x => x.ProjectRequestID == projectRequestId).ToList();
            txtStt.Value = solutionRequests.Count > 0 ? solutionRequests.Max(x => x.STT) + 1 : 1;
            txtCodeSolution.Text = $"GP{solutionRequests.Count + 1}";
        }

        void LoadFile(List<ProjectSolutionFileModel> listFiles)
        {
            grdData.DataSource = listFiles;
            grvData.RefreshData();
        }

        bool CheckValidate()
        {
            int requestId = TextUtils.ToInt(cboProjectRequest.EditValue);
            if (TextUtils.ToInt(cboProject.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Dự án!", "Thông báo");
                return false;
            }

            if (requestId <= 0)
            {
                MessageBox.Show("Vui lòng nhập Yêu cầu dự án!", "Thông báo");
                return false;
            }

            if (string.IsNullOrEmpty(txtCodeSolution.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Mã giải pháp!", "Thông báo");
                return false;
            }
            else
            {
                var exp1 = new Expression("ID", projectSolution.ID, "<>");
                var exp2 = new Expression("CodeSolution", txtCodeSolution.Text.Trim());
                var exp3 = new Expression("ProjectRequestID", requestId);
                var requests = SQLHelper<ProjectSolutionModel>.FindByExpression(exp1.And(exp2).And(exp3));
                if (requests.Count > 0)
                {
                    MessageBox.Show($"Mã giải pháp [{txtCodeSolution.Text}] đã tồn tại!", "Thông báo");
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtContentSolution.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Nội dung!", "Thông báo");
                return false;
            }
            return true;
        }

        bool SaveData()
        {
            if (!CheckValidate()) return false;

            projectSolution.ProjectRequestID = TextUtils.ToInt(cboProjectRequest.EditValue);
            projectSolution.DateSolution = dtpDateSolution.Value;
            projectSolution.CodeSolution = txtCodeSolution.Text.Trim();
            projectSolution.STT = (int)txtStt.Value;
            projectSolution.ContentSolution = txtContentSolution.Text.Trim();
            projectSolution.StatusSolution = chkStatusSolution.Checked ? 1 : 0;

            projectSolution.PriceReportDeadline = dtpDeadlineReportPrice.Value;
            if (projectSolution.ID > 0)
            {
                SQLHelper<ProjectSolutionModel>.Update(projectSolution);
            }
            else
            {
                projectSolution.ID = SQLHelper<ProjectSolutionModel>.Insert(projectSolution).ID;
            }

            UploadFile(projectSolution.ID);

            if (listIdDel.Count > 0)
            {
                SQLHelper<ProjectSolutionFileModel>.DeleteListModel(listIdDel);
                listIdDel.Clear();
            }

            SaveEvent();
            return true;

        }

        async void UploadFile(int id)
        {
            try
            {
                if (listFileUpload.Count() <= 0) return;
                //string pathUpload = @"\\192.168.1.190\Common\08. SOFTWARES\LeTheAnh\DemoUploadFile\Solution";
                string pathUpload = @"\\192.168.1.190\duan\Projects";
                if (Global.IsOnline) pathUpload = @"\\113.190.234.64\duan\Projects";

                //if (Global.IsOnline) pathUpload = pathUpload.Replace("192.168.1.190", "113.190.234.64");
                //try
                //{
                //    //Prod
                //}
                //catch ()
                //{

                //    throw;
                //}

                int projectId = TextUtils.ToInt(cboProject.EditValue);
                ProjectModel project = SQLHelper<ProjectModel>.FindByID(projectId);
                if (project == null) return;
                string pathPatern = $@"{project.CreatedDate.Value.Year}\{project.ProjectCode}\TaiLieuChung\GiaiPhap";

                pathUpload = Path.Combine(pathUpload, pathPatern);
                if (!Directory.Exists(pathUpload))
                {
                    Directory.CreateDirectory(pathUpload);
                }

                string urlUpload = $"http://113.190.234.64:8083/api/Home/uploadfile?path={pathUpload}";

                foreach (FileInfo file in listFileUpload)
                {
                    ProjectSolutionFileModel solutionFile = SQLHelper<ProjectSolutionFileModel>.FindByID(0);
                    if (solutionFile == null) solutionFile = new ProjectSolutionFileModel();

                    solutionFile.ProjectSolutionID = id;
                    solutionFile.FileNameOrigin = file.Name;
                    solutionFile.Extension = file.Extension;
                    solutionFile.OriginPath = file.DirectoryName;
                    solutionFile.ServerPath = pathUpload;

                    if (solutionFile.ID > 0)
                    {
                        SQLHelper<ProjectSolutionFileModel>.Update(solutionFile);
                    }
                    else
                    {
                        solutionFile.ID = SQLHelper<ProjectSolutionFileModel>.Insert(solutionFile).ID;
                    }


                    HttpClient client = new HttpClient();
                    MultipartFormDataContent formData = new MultipartFormDataContent();

                    //formData.Add(new StreamContent(File.OpenRead(file.FullName)), "file", $"{solutionFile.ID}{file.Extension}");
                    formData.Add(new StreamContent(File.OpenRead(file.FullName)), "file", $"{file.Name}");
                    var postData = await client.PostAsync(urlUpload, formData);
                    var result = postData.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboProject_EditValueChanged(object sender, EventArgs e)
        {
            LoadProjectRequest();
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
                projectSolution = new ProjectSolutionModel();
                LoadData();
                txtContentSolution.Focus();
            }
        }

        private void frmProjectSolutionDetail_KeyDown(object sender, KeyEventArgs e)
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
                    ProjectSolutionFileModel fileRequest = new ProjectSolutionFileModel()
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
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            if (id <= 0) return;
            string fileName = TextUtils.ToString(grvData.GetFocusedRowCellValue("FileNameOrigin"));
            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá file [{fileName}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {

                ProjectSolutionFileModel file = SQLHelper<ProjectSolutionFileModel>.FindByID(id);
                if (file == null) return;
                listIdDel.Add(file);
                grvData.DeleteSelectedRows();

            }
        }

        private void btnAddRequest_Click(object sender, EventArgs e)
        {
            frmProjectRequestDetail frm = new frmProjectRequestDetail();
            frm.projectId = projectId;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadProjectRequest();
            }
        }

        private void cboProjectRequest_EditValueChanged(object sender, EventArgs e)
        {
            GetSolutionCode();
        }
    }
}
