using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmProjectSurveyDetail : _Forms
    {
        public List<int> totalRecords = new List<int>();

        public ProjectSurveyModel projectSurvey = new ProjectSurveyModel();

        //==============lee min khooi update 22/10/2024 ===========================
        List<ProjectSurveyFileModel> listFiles = new List<ProjectSurveyFileModel>();
        List<ProjectSurveyFileModel> listFileDelete = new List<ProjectSurveyFileModel>();
        List<FileInfo> listFileUpload = new List<FileInfo>();

        public frmProjectSurveyDetail()
        {
            InitializeComponent();
        }

        private void frmProjectSurveyDetail_Load(object sender, EventArgs e)
        {
            dtpDateStart.Value = dtpDateEnd.Value = DateTime.Now.AddDays(+1);
            grdFileData.ContextMenuStrip = contextMenuStrip2;
            LoadProject();
            LoadCustomer();
            LoadEmployee();
            LoadProjectStatus();
            LoadTBP(); //LinhTN update 28/10/2024
            LoadData();
        }


        void LoadData()
        {

            //=============== lee min khooi update 22/10/2024 reset file ================
            listFileUpload = new List<FileInfo>();
            listFileDelete = new List<ProjectSurveyFileModel>();
            listFiles = SQLHelper<ProjectSurveyFileModel>.FindByAttribute("ProjectSurveyID", projectSurvey.ID);
            grdFileData.DataSource = listFiles;
            //=============== end update reset file ================


            cboProject.EditValue = projectSurvey.ProjectID;

            cboApprovedUrgent.EditValue = projectSurvey.ApprovedUrgentID;
            chkIsUrgent.Checked = TextUtils.ToBoolean(projectSurvey.IsUrgent);
            txtReasonUrgent.Text = projectSurvey.ReasonUrgent;
            txtAddress.Text = projectSurvey.Address;
            txtPIC.Text = projectSurvey.PIC;
            dtpDateStart.Value = !projectSurvey.DateStart.HasValue ? DateTime.Now.AddDays(+1) : projectSurvey.DateStart.Value;
            dtpDateEnd.Value = !projectSurvey.DateEnd.HasValue ? DateTime.Now.AddDays(+1) : projectSurvey.DateEnd.Value;
            txtDescription.Text = projectSurvey.Description;
            txtNote.Text = projectSurvey.Note;
            txtPhoneNumber.Text = projectSurvey.PhoneNumber;

            if (projectSurvey.ID > 0)
            {
                cboEmployee.EditValue = projectSurvey.EmployeeID;
            }

            bool isPermisions = (projectSurvey.EmployeeID == Global.EmployeeID || projectSurvey.ID <= 0 || Global.IsAdmin);
            btnSaveClose.Enabled = btnSaveNew.Enabled = isPermisions;
            LoadDetail();
        }


        void LoadDetail()
        {
            int projectID = TextUtils.ToInt(cboProject.EditValue);
            DataTable dt = TextUtils.LoadDataFromSP("spGetProjectSurveyDetail", "A",
                                                    new string[] { "@ProjectSurveyID", "@ProjectID" },
                                                    new object[] { projectSurvey.ID, projectID });
            tlProjectType.DataSource = dt;

            tlProjectType.ExpandAll();
        }


        void LoadProject()
        {
            var list = SQLHelper<ProjectModel>.FindAll().OrderByDescending(x => x.CreatedDate).ToList();
            cboProject.Properties.ValueMember = "ID";
            cboProject.Properties.DisplayMember = "ProjectName";
            cboProject.Properties.DataSource = list;
        }


        void LoadCustomer()
        {
            var exp1 = new Expression("IsDeleted", 1, "<>");
            var list = SQLHelper<CustomerModel>.FindByExpression(exp1);
            cboCustomer.Properties.ValueMember = "ID";
            cboCustomer.Properties.DisplayMember = "CustomerName";
            cboCustomer.Properties.DataSource = list;

            cboEndUser.Properties.ValueMember = "ID";
            cboEndUser.Properties.DisplayMember = "CustomerName";
            cboEndUser.Properties.DataSource = list;
        }


        void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });

            //Load người phụ trách Sale
            cboUser.Properties.ValueMember = "UserID";
            cboUser.Properties.DisplayMember = "FullName";
            cboUser.Properties.DataSource = dt;

            //Load người phụ trách Technical
            cboUserTechnical.Properties.ValueMember = "UserID";
            cboUserTechnical.Properties.DisplayMember = "FullName";
            cboUserTechnical.Properties.DataSource = dt;

            //Load người PM
            cboProjectManager.Properties.ValueMember = "ID";
            cboProjectManager.Properties.DisplayMember = "FullName";
            cboProjectManager.Properties.DataSource = dt;

            //Load leader duyệt gấp
            cboApprovedUrgent.Properties.ValueMember = "ID";
            cboApprovedUrgent.Properties.DisplayMember = "FullName";
            cboApprovedUrgent.Properties.DataSource = dt;

            //Load người yêu cầu
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;
            cboEmployee.EditValue = Global.EmployeeID;
            cboEmployee.Enabled = Global.IsAdmin;
        }


        void LoadProjectStatus()
        {
            List<ProjectStatusModel> list = SQLHelper<ProjectStatusModel>.FindAll().OrderBy(x => x.STT).ToList();
            cboProjectStatus.Properties.DataSource = list;
            cboProjectStatus.Properties.DisplayMember = "StatusName";
            cboProjectStatus.Properties.ValueMember = "ID";
        }

        //LinhTN update 28/10/2024
        void LoadTBP()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeProjectType", "A", new string[] { }, new object[] { });
            cboTBP.ValueMember = "EmployeeID";
            cboTBP.DisplayMember = "FullName";
            cboTBP.DataSource = dt;
        }

        bool CheckValidate()
        {
            tlProjectType.CloseEditor();

            if (TextUtils.ToInt(cboProject.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng chọn Dự án!", "Thông báo");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAddress.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Địa chỉ!", "Thông báo");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPIC.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập PIC!", "Thông báo");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPhoneNumber.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập SĐT!", "Thông báo");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescription.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Mô tả!", "Thông báo");
                return false;
            }


            DateTime dateNow = DateTime.Now;
            DateTime dateStart = dtpDateStart.Value.Date;
            double timeSpan = (dateStart - dateNow.Date).TotalDays;
            if (timeSpan < 1)
            {
                MessageBox.Show("Bạn không thể đăng ký trước ngày hiện tại!", "Thông báo");
                return false;
            }

            if (!chkIsUrgent.Checked)
            {
                //DateTime dateNow = DateTime.Now;
                //DateTime dateStart = dtpDateStart.Value.Date;
                //double timeSpan = (dateStart - dateNow.Date).TotalDays;
                if (timeSpan > 1)
                {

                }
                else if (timeSpan == 1)
                {
                    var time = new TimeSpan(17, 0, 0);
                    if (dateNow.TimeOfDay > time)
                    {
                        MessageBox.Show("Bạn phải đăng ký trước 17h!", "Thông báo");
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Bạn phải đăng ký trước ít nhất 1 ngày!", "Thông báo");
                    return false;
                }
            }
            else
            {
                if (TextUtils.ToInt(cboApprovedUrgent.EditValue) <= 0)
                {
                    MessageBox.Show("Vui lòng chọn Leader!", "Thông báo");
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtReasonUrgent.Text.Trim()))
                {
                    MessageBox.Show("Vui lòng nhập Lý do gấp!", "Thông báo");
                    return false;
                }
            }


            //Check có chọn kiểu dự án khảo sát không
            var listSelectedTypes = tlProjectType.GetNodeList().Where(x => TextUtils.ToBoolean(x.GetValue(colIsSelectedSurvey)) == true).ToList();
            if (listSelectedTypes.Count <= 0)
            {
                MessageBox.Show("Bạn phải chọn ít nhất 1 kiểu dự án đi khảo sát!", "Thông báo");
                return false;

            }

            //Check bắt buộc chọn Leader
            foreach (TreeListNode node in tlProjectType.GetNodeList())
            {
                bool isSelectedSurvey = TextUtils.ToBoolean(node.GetValue(colIsSelectedSurvey));
                if (!isSelectedSurvey) continue;
                int leader = TextUtils.ToInt(node.GetValue(colLeaderID));
                string typeName = TextUtils.ToString(node.GetValue(colProjectTypeName));
                if (leader <= 0)
                {
                    MessageBox.Show($"Vui lòng chọn Leader cho kiểu dự án [{typeName}]!", "Thông báo");
                    return false;
                }
            }

            return true;
        }


        bool SaveData()
        {
            tlProjectType.CloseEditor();
            if (!CheckValidate()) return false;

            projectSurvey.ProjectID = TextUtils.ToInt(cboProject.EditValue);
            projectSurvey.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
            projectSurvey.ApprovedUrgentID = TextUtils.ToInt(cboApprovedUrgent.EditValue);
            projectSurvey.IsUrgent = chkIsUrgent.Checked;
            projectSurvey.ReasonUrgent = txtReasonUrgent.Text.Trim();
            projectSurvey.Address = txtAddress.Text.Trim();
            projectSurvey.PIC = txtPIC.Text.Trim();
            projectSurvey.DateStart = dtpDateStart.Value;
            projectSurvey.DateEnd = dtpDateEnd.Value;
            projectSurvey.Description = txtDescription.Text.Trim();
            projectSurvey.Note = txtNote.Text.Trim();
            projectSurvey.PhoneNumber = txtPhoneNumber.Text.Trim();

            if (projectSurvey.ID <= 0)
            {
                var result = SQLHelper<ProjectSurveyModel>.Insert(projectSurvey);
                totalRecords.Add(result.TotalRow);

                projectSurvey.ID = result.ID;
            }
            else
            {
                var result = SQLHelper<ProjectSurveyModel>.Update(projectSurvey);
                totalRecords.Add(result.TotalRow);
            }

            //Insert detail
            List<TreeListNode> projectTypeSelectes = tlProjectType.GetNodeList();
            foreach (TreeListNode node in projectTypeSelectes)
            {
                bool isSelected = TextUtils.ToBoolean(node.GetValue(colIsSelectedSurvey));
                int id = TextUtils.ToInt(node.GetValue(colID));
                if (isSelected)
                {

                    ProjectSurveyDetailModel detail = SQLHelper<ProjectSurveyDetailModel>.FindByID(id);

                    detail.ProjectSurveyID = projectSurvey.ID;
                    detail.ProjectTypeID = TextUtils.ToInt(node.GetValue(colProjectTypeID));
                    detail.Note = TextUtils.ToString(node.GetValue(colNote));
                    detail.LeaderID = TextUtils.ToInt(node.GetValue(colLeaderID)); //LinhTN update 28/10/2024

                    if (detail.ID <= 0)
                    {
                        var result = SQLHelper<ProjectSurveyDetailModel>.Insert(detail);
                        totalRecords.Add(result.TotalRow);
                    }
                    else
                    {
                        var result = SQLHelper<ProjectSurveyDetailModel>.Update(detail);
                        totalRecords.Add(result.TotalRow);
                    }
                }
                else if (id > 0)
                {
                    SQLHelper<ProjectSurveyDetailModel>.DeleteModelByID(id);
                    totalRecords.Add(1);
                }
            }

            // lee min khooi update 22/10/2024
            UploadFile(projectSurvey.ID);
            RemoveFile();
            return true;
        }

        private void btnSaveClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSaveNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (SaveData())
            {
                projectSurvey = new ProjectSurveyModel();
                LoadData();
            }
        }

        private void cboProject_EditValueChanged(object sender, EventArgs e)
        {
            ProjectModel project = (ProjectModel)cboProject.GetSelectedDataRow() ?? new ProjectModel();

            cboUser.EditValue = project.UserID;
            cboUserTechnical.EditValue = project.UserTechnicalID;
            cboProjectStatus.EditValue = project.ProjectStatus;
            cboProjectManager.EditValue = project.ProjectManager;
            cboCustomer.EditValue = project.CustomerID;
            cboEndUser.EditValue = project.EndUser;

            LoadDetail();
        }

        private void chkIsUrgent_CheckedChanged(object sender, EventArgs e)
        {
            label17.Visible = label18.Visible = chkIsUrgent.Checked;
        }

        private void repositoryItemCheckEdit2_EditValueChanged(object sender, EventArgs e)
        {
            //tlProjectType.CloseEditor();
            //TreeListNode node = tlProjectType.FocusedNode;

            //bool isSelected = TextUtils.ToBoolean(node.GetValue(colIsSelectedSurvey));
            //bool selected = TextUtils.ToBoolean(node.GetValue(colSelected));
            //int status = TextUtils.ToInt(node.GetValue(colIsSelectedSurvey));

            //if (!isSelected) return;

            //if (status == 2)
            //{
            //    MessageBox.Show("Leader Kỹ thuật đã duyệt. Bạn không thể sửa loại khảo sát!", "Thông báo");
            //    node.SetValue(colIsSelectedSurvey, false);
            //    return;
            //}
            //else if (!selected)
            //{
            //    MessageBox.Show("Bạn không thể yêu cầu khảo sát cho loại khác!", "Thông báo");
            //    node.SetValue(colIsSelectedSurvey, false);
            //    return;
            //}
        }

        private void tlProjectType_ShowingEditor(object sender, CancelEventArgs e)
        {
            //bool isSelected = TextUtils.ToBoolean(tlProjectType.GetFocusedRowCellValue(colIsSelectedSurvey));
            bool selected = TextUtils.ToBoolean(tlProjectType.GetFocusedRowCellValue(colSelected));
            int status = TextUtils.ToInt(tlProjectType.GetFocusedRowCellValue(colIsSelectedSurvey));
            //if(selected && status != 2)
            //{
            //    e.Cancel = false;
            //}
            //else
            //{
            //    e.Cancel = true;
            //}

            e.Cancel = !(selected && status != 2);
        }

        private void frmProjectSurveyDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void frmProjectSurveyDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        //========================================= lee min khooi update 22/10/2024 ===================================================
        void LoadFile(List<ProjectSurveyFileModel> listFiles)
        {
            grdFileData.DataSource = listFiles;
            grvFileData.RefreshData();
        }

        //Mở file
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
            int id = TextUtils.ToInt(grvFileData.GetFocusedRowCellValue("ID"));
            string fileName = TextUtils.ToString(grvFileData.GetFocusedRowCellValue("FileName"));

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá file đính kèm [{fileName}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                grvFileData.DeleteSelectedRows();
                if (id <= 0) return;
                ProjectSurveyFileModel file = SQLHelper<ProjectSurveyFileModel>.FindByID(id);
                listFileDelete.Add(file);
            }
        }

        public void RemoveFile()
        {
            if (listFileDelete.Count <= 0) return;
            var url = $"http://113.190.234.64:8083/api/Home/removefile?path=";
            var client = new HttpClient();
            foreach (var item in listFileDelete)
            {
                //url += $@"{item.ServerPath}\{item.FileName}";
                //var result = await client.GetAsync(url);

                //if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    SQLHelper<ProjectSurveyFileModel>.Delete(item);
                }
            }
            listFileDelete.Clear();
        }


        public async void UploadFile(int modelID)
        {
            try
            {
                if (listFileUpload.Count <= 0) return;

                //ConfigSystemModel config = SQLHelper<ConfigSystemModel>.FindByAttribute("KeyName", "PathPaymentOrder").FirstOrDefault();
                //if (config == null || string.IsNullOrEmpty(config.KeyValue))
                //{
                //    MessageBox.Show("Vui lòng chọn đường dẫn lưu trên server!", "Thông báo");
                //    return;
                //}

                ProjectSurveyModel model = SQLHelper<ProjectSurveyModel>.FindByID(modelID);
                if (model.ID <= 0) return;

                ProjectModel prj = (ProjectModel)cboProject.GetSelectedDataRow();
                if (prj == null || !prj.CreatedDate.HasValue) return;

                string pathServer = @"\\192.168.1.190\duan\Projects\";
                //string pathPattern = $@"duan\Projects\{prj.CreatedDate.Value.Year}\{prj.ProjectCode}\TaiLieuChung\ThongTinKhaoSat";
                string pathPattern = $@"{prj.CreatedDate.Value.Year}\{prj.ProjectCode}\TaiLieuChung\ThongTinKhaoSat";
                string pathUpload = Path.Combine(pathServer, pathPattern);
                //if (!Directory.Exists(pathUpload))
                //{
                //    Directory.CreateDirectory(pathUpload);
                //}
                var client = new HttpClient();

                List<ProjectSurveyFileModel> listFiles = new List<ProjectSurveyFileModel>();
                foreach (var file in listFileUpload.ToList())
                {
                    ProjectSurveyFileModel fileOrder = new ProjectSurveyFileModel();
                    fileOrder.ProjectSurveyID = model.ID;
                    fileOrder.FileName = file.Name;
                    fileOrder.OriginPath = file.DirectoryName;
                    fileOrder.ServerPath = pathUpload;

                    if (file.Length < 0) continue;

                    var fileStream = new FileStream(file.FullName, FileMode.Open);
                    byte[] bytes = new byte[file.Length];
                    fileStream.Read(bytes, 0, (int)file.Length);
                    var byteArrayContent = new ByteArrayContent(bytes);

                    MultipartFormDataContent content = new MultipartFormDataContent();
                    content.Add(byteArrayContent, "file", file.Name);

                    var url = $"http://113.190.234.64:8083/api/Home/uploadfile?path={pathUpload}";
                    var result = await client.PostAsync(url, content);
                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        SQLHelper<ProjectSurveyFileModel>.Insert(fileOrder);
                    }
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo");
            }
        }

        private void tlProjectType_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {

        }

        private void tlProjectType_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (e.Node == null) return;
            //if (e.Column != colLeaderID && e.Column != colIsSelectedSurvey) return;
            bool isSelectType = TextUtils.ToBoolean(e.Node.GetValue(colSelected));
            //bool isSelectType = TextUtils.ToBoolean(e.Node["Selected"]);

            //if (e.Column != colLeaderID) return;

            if (isSelectType)
            {
                e.Appearance.BackColor = Color.LightYellow;
            }

        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                //ProcessFolde();

                ProjectModel project = (ProjectModel)cboProject.GetSelectedDataRow() ?? new ProjectModel();
                if (!project.CreatedDate.HasValue) return;
                int year = project.CreatedDate.Value.Year;
                string code = project.ProjectCode;
                string pathLocation = Global.PathLocationProject;
                if (Global.IsOnline)
                {
                    pathLocation = @"\\113.190.234.64\duan\Projects";
                }

                string path = $@"{pathLocation}\{year}\{code}";

                int projectTypeID = TextUtils.ToInt(tlProjectType.GetFocusedRowCellValue(colProjectTypeID));
                //string fileName = TextUtils.ToString(tlProjectType.GetFocusedRowCellValue(colFileNameResult));
                //fileName = "a.txt";
                List<ProjectTreeFolderModel> folders = SQLHelper<ProjectTreeFolderModel>.ProcedureToList("sp_GetProjectTypeTreeFolder",
                                                        new string[] { "@ProjectTypeID" }, new object[] { projectTypeID });

                ProjectTreeFolderModel parentFolder = folders.Where(x => x.ParentID == 0).FirstOrDefault() ?? new ProjectTreeFolderModel();
                if (string.IsNullOrWhiteSpace(parentFolder.FolderName)) return;
                string folder = Path.Combine(path, parentFolder.FolderName, "KetQuaKhaoSat");



                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                Process.Start(folder);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo");
            }
        }

        private void cboTBP_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit lookUpEdit = (SearchLookUpEdit)sender;
            DataRowView dataRow = (DataRowView)lookUpEdit.GetSelectedDataRow();
            //return;
            int employeeID = 0;
            if (dataRow != null) employeeID = TextUtils.ToInt(dataRow["EmployeeID"]);

            tlProjectType.SetFocusedRowCellValue(colIsSelectedSurvey, (employeeID > 0));
        }

        private void btnDownloadFile_Click(object sender, EventArgs e)
        {
            downloadFile();
        }

        //private void downloadFile()
        //{

        //    try
        //    {
        //        int id = TextUtils.ToInt(grvFileData.GetFocusedRowCellValue("ID"));
        //        if (id <= 0)
        //        {
        //            if (grvFileData.RowCount <= 0) return;
        //            string name = TextUtils.ToString(grvFileData.GetFocusedRowCellValue(colFileName));
        //            string originPath = TextUtils.ToString(grvFileData.GetFocusedRowCellValue(colOriginPath));
        //            string filePath = Path.Combine(originPath, name);
        //            if (File.Exists(filePath))
        //            {
        //                Process.Start(filePath);
        //            }

        //        }
        //        else
        //        {
        //            ProjectModel project = (ProjectModel)cboProject.GetSelectedDataRow() ?? new ProjectModel();
        //            if (!project.CreatedDate.HasValue) return;
        //            int year = project.CreatedDate.Value.Year;
        //            string code = project.ProjectCode;

        //            string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        //            string pathDownload = Path.Combine(userFolder, "Downloads", "ThongTinKhaoSat");

        //            if (!Directory.Exists(pathDownload))
        //            {
        //                Directory.CreateDirectory(pathDownload);
        //            }

        //            string pathPattern = $@"{year}/{code}/TaiLieuChung/ThongTinKhaoSat";

        //            string fileName = TextUtils.ToString(grvFileData.GetFocusedRowCellValue(colFileName));
        //            string folderDownload = Path.Combine(pathDownload, fileName);
        //            string url = $"http://113.190.234.64:" + $"8083/api/projects/{pathPattern}/{fileName}";
        //            //string url = $"http://113.190.234.64:8083/api/projects/{pathPattern}/{fileName}";

        //            if (File.Exists(folderDownload))
        //            {
        //                folderDownload = Path.Combine(pathDownload, $"{Path.GetFileNameWithoutExtension(fileName)}_{DateTime.Now.ToString("HHmmss")}.{Path.GetExtension(fileName)}");
        //            }


        //            WebClient webClient = new WebClient();
        //            webClient.DownloadFile(url, folderDownload);
        //            Process.Start(folderDownload);
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Thông báo");
        //    }
        //}
        private void downloadFile()
        {
            try
            {
                int id = TextUtils.ToInt(grvFileData.GetFocusedRowCellValue("ID"));
                if (id <= 0)
                {
                    if (grvFileData.RowCount <= 0) return;
                    string name = TextUtils.ToString(grvFileData.GetFocusedRowCellValue(colFileName));
                    string originPath = TextUtils.ToString(grvFileData.GetFocusedRowCellValue(colOriginPath));
                    string filePath = Path.Combine(originPath, name);
                    if (File.Exists(filePath))
                    {
                        Process.Start(filePath);
                    }

                }
                else
                {
                    ProjectModel project = (ProjectModel)cboProject.GetSelectedDataRow() ?? new ProjectModel();
                    if (!project.CreatedDate.HasValue) return;

                    int year = project.CreatedDate.Value.Year;
                    string code = project.ProjectCode;
                    //string pathLocation = Global.PathLocationProject;
                    //if (Global.IsOnline)
                    //{
                    //    //pathLocation = @"\\rtctechnologydata.ddns.net\DUAN\Projects\";
                    //    pathLocation = @"\\113.190.234.64\DUAN\Projects\";
                    //}

                    //try
                    //{
                    //    Directory.CreateDirectory(pathLocation);
                    //}
                    //catch (Exception)
                    //{
                    //    pathLocation = @"\\rtctechnologydata.ddns.net\DUAN\Projects\";
                    //}
                    //string pathLocation = @"\\113.190.234.64\DUAN\Projects\";
                    string pathLocation = @"\\192.168.1.190\DUAN\Projects\";
                    string serverPath = $@"{pathLocation}\{year}\{code}\TaiLieuChung\ThongTinKhaoSat";

                    string fileName = TextUtils.ToString(grvFileData.GetFocusedRowCellValue(colFileName));
                    string sourceFile = Path.Combine(serverPath, fileName);

                    if (!File.Exists(sourceFile))
                    {
                        MessageBox.Show("Không tìm thấy file trên server.", "Thông báo");
                        return;
                    }

                    string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    string destFolder = Path.Combine(userFolder, "Downloads", "ThongTinKhaoSat");
                    if (!Directory.Exists(destFolder))
                        Directory.CreateDirectory(destFolder);

                    string destFile = Path.Combine(destFolder, fileName);
                    if (File.Exists(destFile))
                    {
                        destFile = Path.Combine(destFolder, $"{Path.GetFileNameWithoutExtension(fileName)}_{DateTime.Now:HHmmss}{Path.GetExtension(fileName)}");
                    }

                    File.Copy(sourceFile, destFile, true);

                    //MessageBox.Show($"File đã được tải về: {destFile}", "Thông báo");

                    Process.Start(destFile);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }
        //private void ProcessFolde()
        //{
        //    string pathLocation = @"\\113.190.234.64";
        //    //string pathOnline = @"\\rtctechnologydata.ddns.net";
        //    System.Net.IPAddress remoteIp = new System.Net.IPAddress(pathLocation);

        //    try
        //    {
        //        Ping ping = new Ping();
        //        PingReply reply = ping.Send(pathLocation, 3000); // Timeout sau 3000ms (3 giây)
        //    }
        //    catch
        //    {
        //        try
        //        {
        //            pathLocation = @"\\rtctechnologydata.ddns.net\";
        //            Ping ping = new Ping();
        //            PingReply reply = ping.Send(pathLocation, 3000); // Timeout sau 3000ms (3 giây)
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.ToString(), "Thông báo");
        //        }

        //        // Bắt lỗi nếu có vấn đề khi ping
        //        //Console.WriteLine($"An error occurred: {ex.Message}");
        //        //return false;
        //    }


        //    //try
        //    //{
        //    //    if (Directory.Exists(pathLocation + "DUAN")) return pathLocation;
        //    //    else return pathOnline;
        //    //}
        //    //catch
        //    //{
        //    //    try
        //    //    {
        //    //        pathLocation = @"\\rtctechnologydata.ddns.net\";
        //    //        Process.Start(pathLocation);
        //    //    }
        //    //    catch (Exception ex)
        //    //    {
        //    //        MessageBox.Show(ex.ToString(), "Thông báo");
        //    //    }
        //    //}

        //    //if (Global.IsOnline)
        //    //{


        //    //}

        //    //Process.Start(pathLocation)
        //}
    }
}


