using BMS.Model;
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
    public partial class frmProjectSurveyContentResult : _Forms
    {
        public ProjectSurveyDetailModel prjSurveyDetail = new ProjectSurveyDetailModel();
        List<ProjectSurveyFileModel> listFiles = new List<ProjectSurveyFileModel>();
        List<FileInfo> listFileUpload = new List<FileInfo>();
        public frmProjectSurveyContentResult()
        {
            InitializeComponent();
        }

        private void frmProjectSurveyContentResult_Load(object sender, EventArgs e)
        {
            LoadEmployee();
            LoadData();
        }
      
        private void LoadData()
        {
            cboEmployee.EditValue = prjSurveyDetail.EmployeeID;
            dtpDateSurvey.Value = TextUtils.ToDate5(prjSurveyDetail.DateSurvey);
            txtResult.Text = prjSurveyDetail.Result;
            LoadFile();
        }
        void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });

            //Load người Phụ trách Sale
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;

        }
        private void LoadFile()
        {
            listFiles = SQLHelper<ProjectSurveyFileModel>.FindByAttribute(ProjectSurveyFileModel_Enum.ProjectSurveyDetailID.ToString(), prjSurveyDetail.ID);
            grdFileData.DataSource = listFiles;
        }
        private bool SaveData()
        {
            if (prjSurveyDetail.ID <= 0)
            {
                //MessageBox.Show("Không tìm thấy ProjectSurveyDetail", "Thông báo");
                return false;
            }
            string contentResult = txtResult.Text.Trim();
            //if (string.IsNullOrWhiteSpace(contentResult))
            //{
            //    MessageBox.Show("Vui lòng nhập kết quả khảo sát", "Thông báo");
            //    return false;
            //}

            prjSurveyDetail.Result = contentResult;
            SQLHelper<ProjectSurveyDetailModel>.Update(prjSurveyDetail);

            UploadFile();
            return true;
        }
        void LoadFile(List<ProjectSurveyFileModel> listFiles)
        {
            grdFileData.DataSource = listFiles;
            grvFileData.RefreshData();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in dialog.FileNames)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    ProjectSurveyFileModel fileRequest = new ProjectSurveyFileModel()
                    {
                        FileName = fileInfo.Name,
                        OriginPath = fileInfo.DirectoryName
                    };

                    listFiles.Insert(0, fileRequest);
                    listFileUpload.Add(fileInfo);
                }
                LoadFile(listFiles);
            }
        }

        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            //int id = TextUtils.ToInt(grvFileData.GetFocusedRowCellValue("ID"));
            string fileName = TextUtils.ToString(grvFileData.GetFocusedRowCellValue("FileName"));

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá file đính kèm [{fileName}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                grvFileData.DeleteSelectedRows();
                //if (id <= 0) return;
                //PaymentOrderFileModel file = SQLHelper<PaymentOrderFileModel>.FindByID(id);
                //listFileDelete.Add(file);
            }
        }

        public async void UploadFile()
        {
            try
            {
                // nhớ sửa lại đường dẫn này
                //ConfigSystemModel config = SQLHelper<ConfigSystemModel>.FindByAttribute("KeyName", "PathPaymentOrder").FirstOrDefault();
                //if (config == null || string.IsNullOrEmpty(config.KeyValue))
                //{
                //    MessageBox.Show("Vui lòng chọn đường dẫn lưu trên server!", "Thông báo");
                //    return;
                //}
                ProjectSurveyModel master = SQLHelper<ProjectSurveyModel>.FindByID(TextUtils.ToInt(prjSurveyDetail.ProjectSurveyID));
                ProjectModel prj = SQLHelper<ProjectModel>.FindByID(TextUtils.ToInt(master.ProjectID));
                if (prjSurveyDetail.ID <= 0) return;
                ProjectTypeFilePath lstPath = SQLHelper<ProjectTypeFilePath>.ProcedureToList("sp_GetProjectTypeTreeFolder",
                                                                                    new string[] { "@ProjectTypeID" },
                                                                                    new object[] { prjSurveyDetail.ProjectTypeID }).FirstOrDefault(p => p.ParentID == 0) ?? new ProjectTypeFilePath();
                if (string.IsNullOrWhiteSpace(lstPath.FolderName)) return;

                string pathPattern = $@"\\192.168.1.190\duan\Projects\{prj.CreatedDate.Value.Year}\{prj.ProjectCode}\{lstPath.FolderName}\KetQuaKhaoSat";


                var client = new HttpClient();

                List<ProjectSurveyFileModel> listFiles = new List<ProjectSurveyFileModel>();
                foreach (var file in listFileUpload)
                {
                    ProjectSurveyFileModel fileModel = new ProjectSurveyFileModel();
                    fileModel.ProjectSurveyDetailID = prjSurveyDetail.ID;
                    fileModel.FileName = file.Name;
                    fileModel.OriginPath = file.DirectoryName;
                    fileModel.ServerPath = pathPattern;

                    if (file.Length < 0) continue;

                    var fileStream = new FileStream(file.FullName, FileMode.Open);
                    byte[] bytes = new byte[file.Length];
                    fileStream.Read(bytes, 0, (int)file.Length);
                    var byteArrayContent = new ByteArrayContent(bytes);

                    MultipartFormDataContent content = new MultipartFormDataContent();
                    content.Add(byteArrayContent, "file", file.Name);

                    var url = $"http://113.190.234.64:8083/api/Home/uploadfile?path={pathPattern}";
                    var result = await client.PostAsync(url, content);
                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        SQLHelper<ProjectSurveyFileModel>.Insert(fileModel);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void btnSaveAndClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        class ProjectTypeFilePath
        {
            public int ID { get; set; }
            public int ParentID { get; set; }
            public string FolderName { get; set; }
        }
    }
}
