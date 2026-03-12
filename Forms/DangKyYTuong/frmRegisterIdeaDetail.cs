using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmRegisterIdeaDetail : _Forms
    {
        List<FileInfo> listFileUpload = new List<FileInfo>();
        List<RegisterIdeaFileModel> listFiles = new List<RegisterIdeaFileModel>();
        List<RegisterIdeaFileModel> listIdeaFileDelete = new List<RegisterIdeaFileModel>();
        List<string> listFileName = new List<string>();

        public RegisterIdeaModel registerIdea = new RegisterIdeaModel();

        DataTable dtType = new DataTable();
        public frmRegisterIdeaDetail()
        {
            InitializeComponent();
        }

        private void LoadRegisterIdeaType()//update
        {
            //List<RegisterIdeaTypeModel> ls = SQLHelper<RegisterIdeaTypeModel>.FindAll();

            dtType = TextUtils.LoadDataFromSP("spGetCourseCatalog", "A", new string[] { "@CatalogType" }, new object[] { 0 });
            cboRegisterIdeaType.Properties.DataSource = dtType;
            cboRegisterIdeaType.Properties.DisplayMember = "Name";
            cboRegisterIdeaType.Properties.ValueMember = "ID";
            if (registerIdea != null)
            {
                cboRegisterIdeaType.EditValue = TextUtils.ToInt(registerIdea.RegisterIdeaTypeID);
            }
        }

        private void LoadDepartment()
        {
            List<DepartmentModel> ls = SQLHelper<DepartmentModel>.FindAll();
            cboDepartmentOrganization.Properties.DataSource = ls;
            cboDepartmentOrganization.Properties.DisplayMember = "Name";
            cboDepartmentOrganization.Properties.ValueMember = "ID";
            if (registerIdea != null)
            {
                cboDepartmentOrganization.EditValue = TextUtils.ToInt(registerIdea.DepartmentOrganizationID);
            }
        }


        void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });


            //Load người yêu cầu
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;
            cboEmployee.EditValue = Global.EmployeeID;
            cboEmployee.Enabled = Global.IsAdmin;
        }


        void LoadCourse()
        {
            int courseCatalogID = TextUtils.ToInt(cboRegisterIdeaType.EditValue);
            var exp1 = new Expression(CourseModel_Enum.CourseCatalogID, courseCatalogID);
            var exp2 = new Expression(CourseModel_Enum.DeleteFlag, 1);
            var listCourses = SQLHelper<CourseModel>.FindByExpression(exp1.And(exp2));
            cboCourse.Properties.ValueMember = CourseModel_Enum.ID.ToString();
            cboCourse.Properties.DisplayMember = CourseModel_Enum.NameCourse.ToString();
            cboCourse.Properties.DataSource = listCourses;
        }
        private void frmRegisterIdea_Load(object sender, EventArgs e)
        {
            if (registerIdea != null)
            {
                List<RegisterIdeaDetailModel> ls = SQLHelper<RegisterIdeaDetailModel>.FindByAttribute("RegisterIdeaID", registerIdea.ID);
                if (ls.Count > 0)
                {
                    dtpDS.Value = Convert.ToDateTime(ls[0].DateStart);
                    dtpDE.Value = Convert.ToDateTime(ls[0].DateEnd);
                }
            }
            //LoadIdea();
            LoadListFile();
            LoadRegisterIdeaType();
            LoadDepartment();
            LoadEmployee();
            LoadCourse();

            LoadRegister();
        }

        private void LoadIdea()
        {
            if (registerIdea.ID > 0)
            {
                List<RegisterIdeaDetailModel> listIdeaDetail = SQLHelper<RegisterIdeaDetailModel>.FindByAttribute("RegisterIdeaID", registerIdea.ID);
                grdData.DataSource = listIdeaDetail;
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("STT", typeof(int));
                dt.Columns.Add("Category", typeof(string));
                dt.Columns.Add("Description", typeof(string));
                dt.Columns.Add("Note", typeof(string));

                dt.Rows.Add(0, 1, "Tên ý tưởng", "", "");
                dt.Rows.Add(0, 2, "Đối tượng áp dụng", "", "");
                dt.Rows.Add(0, 3, "Chi phí thực hiện", "", "");
                dt.Rows.Add(0, 4, "Địa điểm áp dụng", "", "");
                dt.Rows.Add(0, 5, "Thời gian triển khai", "", "");
                grdData.DataSource = dt;
            }
        }

        private void LoadRegister()
        {
            if (registerIdea.ID <= 0)
            {
                txtIdeaRegister.Text = Global.AppFullName;
                txtIdeaDepartment.Text = Global.DepartmentName;
                //return;
            }
            else
            {
                cboEmployee.EditValue = registerIdea.EmployeeID;
            }
            RegisterIdeaScoreModel ideaScor = SQLHelper<RegisterIdeaScoreModel>.FindByAttribute("RegisterIdeaID", registerIdea.ID).FirstOrDefault();
            EmployeeModel ideaEmployee = SQLHelper<EmployeeModel>.FindByID(TextUtils.ToInt(registerIdea.EmployeeID));
            //if (ideaEmployee == null) return;
            txtIdeaRegister.Text = ideaEmployee.FullName;
            DepartmentModel department = SQLHelper<DepartmentModel>.FindByID(TextUtils.ToInt(ideaEmployee.DepartmentID));
            //if (department == null) return;
            txtIdeaDepartment.Text = department.Name;

            dtpDateRegister.Value = registerIdea.DateRegister.HasValue ? registerIdea.DateRegister.Value : DateTime.Now;

            cboCourse.EditValue = registerIdea.CourseID;


            LoadIdea();

        }
        private void LoadListFile()
        {
            if (registerIdea.ID <= 0) return;
            listFiles = SQLHelper<RegisterIdeaFileModel>.FindByAttribute("RegisterIdeaID", registerIdea.ID).OrderByDescending(x => x.ID).ToList();
            LoadViewFile(listFiles);

        }
        void LoadViewFile(List<RegisterIdeaFileModel> listFiles)
        {
            grdFile.DataSource = listFiles;
            grvFile.RefreshData();
            for (int i = 0; i < grvFile.RowCount; i++)
            {
                string fileName = TextUtils.ToString(grvFile.GetRowCellValue(i, colFileName));
                listFileName.Add(fileName);
            }
        }

        private void btnSaveCLose_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {

                this.DialogResult = DialogResult.OK;
            }
        }

        private bool SaveData()
        {
            bool isUpdate = true;
            grvData.CloseEditor();
            //EmployeeModel e = SQLHelper<EmployeeModel>.FindByAttribute("FullName", txtIdeaRegister.Text.Trim()).FirstOrDefault();

            //if (e != null)
            //{
            //    RegisterIdeaModel ri = SQLHelper<RegisterIdeaModel>.FindByAttribute("EmployeeID", e.ID).FirstOrDefault();
            //    registerIdea.EmployeeID = e.ID;
            //    registerIdea.ApprovedTBPID = ri.ApprovedTBPID;
            //}
            //else
            //{
            //    registerIdea.EmployeeID = Global.EmployeeID;
            //    registerIdea.ApprovedTBPID = Global.HeadOfDepartment;
            //}

            registerIdea.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);

            EmployeeModel e = SQLHelper<EmployeeModel>.FindByID(TextUtils.ToInt(cboEmployee.EditValue));

            DepartmentModel department = SQLHelper<DepartmentModel>.FindByID(TextUtils.ToInt(e.DepartmentID));
            registerIdea.ApprovedTBPID = department.HeadofDepartment;

            registerIdea.DateRegister = dtpDateRegister.Value;
            //registerIdea.ApprovedTBPID = Global.HeadOfDepartment;
            registerIdea.RegisterIdeaTypeID = TextUtils.ToInt(cboRegisterIdeaType.EditValue);
            registerIdea.DepartmentOrganizationID = TextUtils.ToInt(cboDepartmentOrganization.EditValue);

            registerIdea.CourseID = TextUtils.ToInt(cboCourse.EditValue);
            if (registerIdea.ID <= 0)
            {
                //registerIdea.ID = TextUtils.ToInt(RegisterIdeaBO.Instance.Insert(registerIdea));
                registerIdea.ID = SQLHelper<RegisterIdeaModel>.Insert(registerIdea).ID;
                isUpdate = false;
            }
            else
            {
                SQLHelper<RegisterIdeaModel>.Update(registerIdea);
            }

            for (int i = 0; i < grvData.RowCount; i++)
            {
                RegisterIdeaDetailModel registerIdeaDetail = new RegisterIdeaDetailModel();
                registerIdeaDetail.ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                registerIdeaDetail.RegisterIdeaID = registerIdea.ID;
                registerIdeaDetail.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colTT));
                registerIdeaDetail.Category = TextUtils.ToString(grvData.GetRowCellValue(i, colCategory));
                registerIdeaDetail.Description = TextUtils.ToString(grvData.GetRowCellValue(i, colDescription));
                registerIdeaDetail.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));
                registerIdeaDetail.DateStart = dtpDS.Value.Date;
                registerIdeaDetail.DateEnd = dtpDE.Value.Date;
                if (isUpdate)
                {

                    //RegisterIdeaDetailBO.Instance.Update(registerIdeaDetail);
                    SQLHelper<RegisterIdeaDetailModel>.Update(registerIdeaDetail);
                }
                else
                {
                    //RegisterIdeaDetailBO.Instance.Insert(registerIdeaDetail);
                    SQLHelper<RegisterIdeaDetailModel>.Insert(registerIdeaDetail);
                }
            }
            UploadFile(registerIdea.ID);
            RemoveFile();

            return true;
        }



        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                registerIdea = new RegisterIdeaModel();
                LoadIdea();
            }

        }

        private void btnUploadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in dialog.FileNames)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    int checkExistName = listFileName.IndexOf(fileInfo.Name);
                    if (checkExistName > 0)
                    {
                        MessageBox.Show($" Tên file {fileInfo.Name} đã tồn tại. Vui lòng đổi tên file hoặc chọn file khác!", "Thông báo");
                        return;
                    }
                }

                foreach (string file in dialog.FileNames)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    RegisterIdeaFileModel fileIdea = new RegisterIdeaFileModel()
                    {
                        FileName = fileInfo.Name,
                        OriginPath = fileInfo.DirectoryName
                    };
                    listFileUpload.Add(fileInfo);
                    listFiles.Insert(0, fileIdea);
                }
                LoadViewFile(listFiles);
            }
        }
        public async void UploadFile(int registerIdeaID)
        {
            try
            {
                if (listFileUpload.Count <= 0) return;
                //ConfigSystemModel config = SQLHelper<ConfigSystemModel>.FindByAttribute("KeyName", "PathRegisterIdea").FirstOrDefault();
                //if (config == null || string.IsNullOrEmpty(config.KeyValue))
                //{
                //    MessageBox.Show("Vui lòng chọn đường dẫn lưu trên server!", "Thông báo");
                //    return;
                //}

                RegisterIdeaModel idea = SQLHelper<RegisterIdeaModel>.FindByID(registerIdeaID);
                if (idea.ID <= 0 || !idea.DateRegister.HasValue) return;
                // if (idea.EmployeeID != Global.EmployeeID) return;

                //string pathServer = $@"\\192.168.1.190\Technical\22. Tips and Tricks";

                //int quater = ((idea.DateRegister.Value.Month - 1) / 3) + 1;
                //string pathPattern = $@"{idea.DateRegister.Value.Year}\Q{quater}\{Global.AppFullName}";

                string pathServer = $@"\\192.168.1.190\duan\Tip Trick\{idea.DateRegister.Value.Year}"; // Update 2810
                string nameDepartment = "", code = "";
                int idType = TextUtils.ToInt(cboRegisterIdeaType.EditValue);
                DataRow[] rows = dtType.Select("ID = " + idType);
                if (rows.Length > 0)
                {
                    nameDepartment = rows[0]["NameDepartment"].ToString();
                    code = rows[0]["Code"].ToString();
                }
                string pathPattern = $@"P {nameDepartment}\{code}";

                string pathUpload = Path.Combine(pathServer, pathPattern);

                var client = new HttpClient();
                //var content = new MultipartFormDataContent();

                //  List<RegisterIdeaFileModel> listFiles = new List<RegisterIdeaFileModel>();
                foreach (var file in listFileUpload)
                {
                    RegisterIdeaFileModel fileIdea = new RegisterIdeaFileModel();
                    fileIdea.RegisterIdeaID = idea.ID;
                    fileIdea.FileName = file.Name;
                    fileIdea.OriginPath = file.DirectoryName;
                    fileIdea.ServerPath = pathUpload;
                    //SQLHelper<PaymentOrderFileModel>.Insert(fileOrder);

                    if (file.Length < 0) continue;

                    //using var fileStream = file.OpenReadStream();
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
                        SQLHelper<RegisterIdeaFileModel>.Insert(fileIdea);
                    }
                }

                //HttpClient client = new HttpClient();
                //MultipartFormDataContent form = new MultipartFormDataContent();
                ////HttpContent content = new StringContent("fileToUpload");
                ////HttpContent DictionaryItems = new FormUrlEncodedContent(parameters);
                //form.Add(content, "fileToUpload");
                //form.Add(DictionaryItems, "medicineOrder");

                //var stream = new FileStream("c:\\TemporyFiles\\test.jpg", FileMode.Open);
                //content = new StreamContent(stream);
                //content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                //{
                //    Name = "fileToUpload",
                //    FileName = "AFile.txt"
                //};
                //form.Add(content);

                //var url = $"http://113.190.234.64:8083/api/Home/uploadfile?path={pathUpload}";
                //var result = await client.PostAsync(url, content);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        public async void RemoveFile()
        {
            if (listIdeaFileDelete.Count <= 0) return;
            var url = $"http://113.190.234.64:8083/api/Home/removefile?path=";
            //var url = $"http://localhost:8390/api/Home/removefile?path=";
            var client = new HttpClient();
            foreach (var item in listIdeaFileDelete)
            {
                url += $@"{item.ServerPath}\{item.FileName}";
                var result = await client.GetAsync(url);

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    RegisterIdeaFileModel fileDelete = SQLHelper<RegisterIdeaFileModel>.FindByAttribute("FileName", item.FileName).FirstOrDefault();
                    SQLHelper<RegisterIdeaFileModel>.Delete(fileDelete);
                }
            }
        }
        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            int row = grvFile.FocusedRowHandle;
            string fileName = TextUtils.ToString(grvFile.GetFocusedRowCellValue(colFileName));
            if (MessageBox.Show($"Bạn có muốn xóa file [{fileName}] hay không ?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {


                FileInfo fileInforDelete = listFileUpload.Where(f => f.Name == fileName).FirstOrDefault();
                listFileUpload.Remove(fileInforDelete);

                int deleteID = TextUtils.ToInt(grvFile.GetFocusedRowCellValue(colFileID));
                if (deleteID > 0)
                {
                    RegisterIdeaFileModel ideaFileDelete = SQLHelper<RegisterIdeaFileModel>.FindByID(deleteID);
                    listIdeaFileDelete.Add(ideaFileDelete);
                }
                grvFile.DeleteSelectedRows();
                grvFile.FocusedRowHandle = row + 1;
            }

        }

        private void grvFile_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo info = grvFile.CalcHitInfo(new Point(e.X, e.Y));
            if (info.Column == colAddFile && e.Y < 25)
            {
                btnUploadFile_Click(null, null);
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnAddRegisterType_Click(object sender, EventArgs e)
        {
            //frmRegisterIdeaType frm = new frmRegisterIdeaType();
            //frm.ShowDialog();
            //if(frm.DialogResult == DialogResult.OK)
            //{
            //    LoadRegisterIdeaType();
            //}

            frmAddCourseCatalog frm = new frmAddCourseCatalog();
            frm.Text = "THÊN DANH MỤC ĐỀ TÀI";
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                LoadRegisterIdeaType();
            }
        }

        private void dtpDS_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dtpDE_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnViewCourse_Click(object sender, EventArgs e)
        {
            string url = "http://localhost:26179/Home/LoginToCourse";
            //string url = "http://113.190.234.64:8087/Home/LoginToCourse";
            string userName = Global.LoginName;
            string passwordHash = Global.AppPassword;
            int registerIdeaTypeID = TextUtils.ToInt(registerIdea.RegisterIdeaTypeID);
            int courseID = TextUtils.ToInt(registerIdea.CourseID);
            string htmlContent = $@"<!DOCTYPE html>
                                    <html lang=""en"">
                                    <head>
                                        <meta charset=""UTF-8"">
                                        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                        <title>Form Example</title>
                                    </head>
                                    <body style=""height: 100vh; width: 100%; display: flex; justify-content:  center; align-items: center; margin: 0;padding: 0;"">

                                        <img src=""./loading.gif"" alt="""">
                                       <form action=""{url}"" method=""post"" id=""frmSubmitLink"" style=""display: none;"">
                                            <input type=""text"" name=""userName"" value=""{userName}""/>
                                            <input type=""password"" name=""passwordHash"" value=""{passwordHash}"" />
                                            <input type=""number"" name=""registerIdeaTypeID"" value=""{registerIdeaTypeID}""/>
                                            <input type=""number"" name=""courseID"" value=""{courseID}""/>
                                            <button type=""submit"">Login</button>
                                        </form>
                                        <script>
                                           document.getElementById(""frmSubmitLink"").submit();
                                        </script>
                                    </body>
                                    </html>";

            string filePath = Path.Combine(Application.StartupPath, "logincourse.html");
            File.WriteAllText(filePath, htmlContent);

            Process.Start(filePath);
        }

        private void cboRegisterIdeaType_EditValueChanged(object sender, EventArgs e)
        {
            LoadCourse();
        }
    }
}