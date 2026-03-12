using BMS;
using BMS.Business;
using BMS.Model;
using BMS.Utils;
using Forms.Classes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Forms.Classes.cGlobVar;

namespace BMS
{
    public partial class frmAddLesson : _Forms
    {
        public CourseLessonModel courseLesson = new CourseLessonModel();
        List<CourseFileModel> listFile = new List<CourseFileModel>();
        public int courseCatalogID = 0;
        //int flag;
        bool isUpdate = false;

        //============ lee min khooi 30/10/2024 =======================
        List<int> lstFileDeleted = new List<int>();


        public frmAddLesson()
        {
            InitializeComponent();
        }

        private void frmAddLesson_Load(object sender, EventArgs e)
        {
            //LOAD Khóa học           
            //DataTable dt = new DataTable();
            //dt = TextUtils.Select("SELECT * FROM dbo.Course");
            //cbxCourse.DataSource = dt;
            //cbxCourse.DisplayMember = "NameCourse";
            //cbxCourse.ValueMember = "ID";
            //cbxCourse.SelectedValue = Group.CourseID;

            //txtUrlPDF.Text = Global.PathCoursePDF + "\n" + Global.PathCourseFile;

            btnSave.Enabled = btnSaveAndClose.Enabled = ValidateUser();
            loadCboCourse();
            loadEmployee();
            loadData();
            LoadCourseCatalog();
        }

        //Load combo khoá học
        void loadCboCourse()
        {
            int department = Global.DepartmentID;
            if (Global.EmployeeID == 54) department = 0;
            DataTable dt = TextUtils.LoadDataFromSP("spGetCourseNew", "A", new string[] { "@DepartmentID", "@Status" }, new object[] { department, -1 });

            cboCourse.Properties.ValueMember = "ID";
            cboCourse.Properties.DisplayMember = "NameCourse";
            cboCourse.Properties.DataSource = dt;
            cboCourse.EditValue = courseLesson.CourseID;
        }

        private void loadEmployee()
        {

            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@DepartmentID", "@Status" }, new object[] { 0, 0 });
            cboEmployee.Properties.DataSource = dt;
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.ValueMember = "ID";

            cboEmployee.EditValue = Global.EmployeeID;
        }

        //Load data
        private void loadData()
        {
            txtLessonName.Focus();
            if (courseLesson.ID > 0)
            {
                isUpdate = true;
                //CourseFileModel CourseFileModel = (CourseFileModel)CourseFileBO.Instance.FindByPK(Group.FileCourseID);

                txtCodeLesson.Text = courseLesson.Code.ToString();
                nupSTT.Value = TextUtils.ToInt(courseLesson.STT);
                txtLessonName.Text = courseLesson.LessonTitle;
                rtbAddLesson.HtmlText = courseLesson.LessonContent;
                txtVideoUrl.Text = courseLesson.VideoURL;
                txtUrlPDF.Text = courseLesson.UrlPDF;
                cboEmployee.EditValue = courseLesson.EmployeeID;

                //DataTable dt = new DataTable();
                //dt = TextUtils.LoadDataFromSP("spGetNameFileLesson", "A", new string[] { "@LessonID" }, new object[] { courseLesson.ID });
                //if (dt.Rows.Count > 0)
                //{
                //    txtFiLeUrlShow.Text = dt.Rows[0]["NameFile"].ToString();
                //}

                //btnSave.Text = "Cất và sửa";
            }
            else
            {
                isUpdate = false;
                int stt = TextUtils.ToInt(TextUtils.ExcuteScalar($"SELECT TOP 1 STT FROM dbo.CourseLesson WHERE CourseID = {TextUtils.ToInt(cboCourse.EditValue)} ORDER BY STT DESC"));
                var rowDataSelect = (DataRowView)cboCourse.GetSelectedDataRow();
                if (rowDataSelect == null) return;

                txtCodeLesson.Text = $"{TextUtils.ToString(rowDataSelect["Code"])}-{stt + 1}";
                nupSTT.Value = TextUtils.ToInt(stt + 1);
                txtLessonName.Text = "";
                txtVideoUrl.Text = "";
                txtUrlPDF.Text = "";
                rtbAddLesson.HtmlText = "";

                //txtFiLeUrlShow.Text = "";
                //txtFiLeUrlHide.Text = "";
                //txtPdfShow.Text = "";
                //txtPdfHide.Text = "";

            }

            loadCourseFile();

            textBox1.Text = isUpdate.ToString();
        }

        //Check validate
        private bool ValidateForm()
        {
            if (TextUtils.ToInt(cboCourse.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Khoá hoc!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (string.IsNullOrEmpty(txtCodeLesson.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Mã bài học!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else
            {
                var exp1 = new Expression("Code", txtCodeLesson.Text.Trim());
                var exp2 = new Expression("ID", courseLesson.ID, "<>");
                var exp3 = new Expression("CourseID", TextUtils.ToInt(cboCourse.EditValue));
                var lesson = SQLHelper<CourseLessonModel>.FindByExpression(exp1.And(exp2).And(exp3));
                if (lesson.Count > 0)
                {
                    MessageBox.Show($"Mã bài học [{txtCodeLesson.Text.Trim()}] đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtLessonName.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Tên bài học!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            //if (nupSTT.Value < 1)
            //{
            //    MessageBox.Show("Số thứ tự không hợp lệ", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}

            //if (txtFiLeUrlHide.Text == "")
            //{
            //    MessageBox.Show("Vui lòng chọn file", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}
            //if (txtVideoUrl.Text == "")
            //{
            //    MessageBox.Show("Vui lòng nhập đường dẫn video", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}
            if (rtbAddLesson.HtmlText == "")
            {
                MessageBox.Show("Vui lòng nhập Nội dung cho bài học", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;

        }

        //Load danh sách file đính kèm
        void loadCourseFile()
        {
            int lessonID = courseLesson.LessonCopyID > 0 ? TextUtils.ToInt(courseLesson.LessonCopyID) : courseLesson.ID; // lee min khooi update 30/10/2024
            Expression ex1 = new Expression("LessonID", lessonID);
            Expression ex2 = new Expression("IsDeleted", 0);

            listFile = SQLHelper<CourseFileModel>.FindByExpression(ex1.And(ex2)).OrderByDescending(x => x.ID).ToList();
            grdCourseFile.DataSource = listFile;
            grvCourseFile.RefreshData();
        }

        //Update file lên server
        async void UploadFile(int fileID, string coursFile, string pdfFile)
        {
            string url = "http://192.168.1.2:8083/api/Home/uploadfile?path=";
            string newFile = "";

            HttpClient client = new HttpClient();
            MultipartFormDataContent formData = new MultipartFormDataContent();
            if (fileID > 0) //Update file đính kèm
            {
                if (!File.Exists(coursFile))
                {
                    MessageBox.Show($"File không tồn tại hoặc đường dẫn không đúng.\nVui lòng kiểm tra lại!\n{coursFile}", "Thông báo");
                    return;
                }

                try
                {
                    url += Global.PathCourseFile;
                    string fileName = fileID.ToString() + Path.GetExtension(coursFile);
                    newFile = Path.Combine(Path.GetDirectoryName(coursFile), fileName);
                    File.Copy(coursFile, newFile);


                    formData.Add(new StreamContent(File.OpenRead(newFile)), "file", fileName);
                    var postData = await client.PostAsync(url, formData);
                    var response = JsonConvert.DeserializeObject<Response>(postData.Content.ReadAsStringAsync().Result);
                    if (response.status != 1)
                    {
                        MessageBox.Show(response.message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        File.Delete(newFile);
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message, "Thông báo");
                    MessageBox.Show(ex.Message + "\r\n" + ex.ToString(), "Thông báo");
                }
                finally
                {
                    File.Delete(newFile);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(pdfFile)) return;

                if (!File.Exists(pdfFile))
                {
                    MessageBox.Show($"File không tồn tại hoặc đường dẫn không đúng.\nVui lòng kiểm tra lại!\n{pdfFile}", "Thông báo");
                    return;
                }

                try
                {
                    url += Global.PathCoursePDF;
                    formData.Add(new StreamContent(File.OpenRead(pdfFile)), "file", Path.GetFileName(pdfFile));
                    var postData = await client.PostAsync(url, formData);
                    var response = JsonConvert.DeserializeObject<Response>(postData.Content.ReadAsStringAsync().Result);
                    if (response.status != 1)
                    {
                        MessageBox.Show(response.message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\r\n" + ex.ToString(), "Thông báo");
                }
            }
        }

        bool saveData()
        {
            try
            {
                if (!ValidateForm())
                {
                    return false;
                }
                //=========== lee min khooi update 30/10/2024 ===================
                bool isCopy = TextUtils.ToBoolean(tglIsCopy.EditValue) || courseLesson.LessonCopyID > 0;

                //Update bài học
                courseLesson.CourseID = TextUtils.ToInt(cboCourse.EditValue);
                courseLesson.Code = txtCodeLesson.Text.Trim();
                courseLesson.STT = TextUtils.ToInt(nupSTT.Value);
                courseLesson.LessonTitle = txtLessonName.Text.Trim();
                courseLesson.VideoURL = txtVideoUrl.Text.Trim();
                courseLesson.UrlPDF = Path.GetFileName(txtUrlPDF.Text.Trim());
                courseLesson.LessonContent = rtbAddLesson.HtmlText;
                courseLesson.LessonCopyID = courseLesson.LessonCopyID;
                courseLesson.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);

                int cpyID = TextUtils.ToInt(cboLessonCopy.EditValue);
                if (isCopy && cpyID > 0) courseLesson.LessonCopyID = cpyID;

                if (courseLesson.ID > 0)
                {
                    SaveLog(courseLesson);
                    //CourseLessonBO.Instance.Update(courseLesson);
                    SQLHelper<CourseLessonModel>.Update(courseLesson);
                }
                else
                {
                    //courseLesson.ID = (int)CourseLessonBO.Instance.Insert(courseLesson);
                    courseLesson.ID = SQLHelper<CourseLessonModel>.Insert(courseLesson).ID;
                }

                if (!isUpdate && courseLesson.LessonCopyID <= 0) UploadFile(0, "", txtUrlPDF.Text.Trim());

                int courseMasterID = isCopy ? TextUtils.ToInt(courseLesson.LessonCopyID) : courseLesson.ID;
                foreach (CourseFileModel item in listFile)
                {
                    CourseFileModel courseFile = SQLHelper<CourseFileModel>.FindByID(item.ID);
                    courseFile.NameFile = item.NameFile;
                    courseFile.LessonID = courseMasterID;
                    courseFile.OriginPath = item.OriginPath;

                    if (courseFile.ID > 0) SQLHelper<CourseFileModel>.Update(courseFile);
                    else courseFile.ID = SQLHelper<CourseFileModel>.Insert(courseFile).ID;

                    if (item.ID <= 0) UploadFile(courseFile.ID, courseFile.OriginPath, "");
                }

                if (lstFileDeleted.Count > 0)
                {
                    string fileIds = string.Join(",", lstFileDeleted);
                    Expression epr1 = new Expression("ID", fileIds, "IN");
                    Dictionary<string, object> newDict = new Dictionary<string, object>()
                    {
                        {"IsDeleted", 1 },
                        {"UpdatedBy", Global.AppFullName },
                        {"UpdatedDate", DateTime.Now }
                    };
                    SQLHelper<CourseFileModel>.UpdateFields(newDict, epr1);
                }

                UpdateDataCopy(courseLesson.ID);
                if (isCopy)
                {
                    UpdateCopyFile(TextUtils.ToInt(courseLesson.LessonCopyID));
                    string contentLog = $"Đã copy Bài học {courseLesson.LessonCopyID} --> {courseLesson.ID}";
                    CourseLessonLogModel log = new CourseLessonLogModel();
                    log.CourseLessonID = courseLesson.ID;
                    log.DateLog = DateTime.Now;
                    log.ContentLog = contentLog;
                    SQLHelper<CourseLessonLogModel>.Insert(log);
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.ToString(), "Thông báo");
                return true;
            }
        }

        //Cất và đóng
        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            //File.Delete(@"D:\RTCNoiBo (1)\RTCNoiBo\cung cap giai phap 251122\48.pdf");
            //flag = 1;
            //SaveGroup();

            if (saveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {

            if (saveData())
            {
                courseLesson = new CourseLessonModel();
                lstFileDeleted = new List<int>(); // lee min khooi update 30/10/2024
                loadData();
            }

        }
        private void btnAddContent_Click(object sender, EventArgs e)
        {
            frmContentLesson frmContentLesson = new frmContentLesson();
            frmContentLesson.HtmlText = rtbAddLesson.HtmlText;
            frmContentLesson.ShowDialog();

            rtbAddLesson.HtmlText = frmContentLesson.HtmlText;
        }
        private void frmAddLesson_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        //private void SaveGroup()
        //{
        //    if (!ValidateForm())
        //    {
        //        frmAddLesson frmAddLesson = new frmAddLesson();
        //        return;
        //    }

        //    courseLesson.Code = txtCodeLesson.Text.Trim();
        //    courseLesson.LessonTitle = txtLessonName.Text;
        //    courseLesson.LessonContent = rtbAddLesson.HtmlText;
        //    courseLesson.STT = TextUtils.ToInt(nupSTT.Value);
        //    courseLesson.VideoURL = txtVideoUrl.Text;
        //    //Group.CourseID = TextUtils.ToInt(cbxCourse.SelectedValue);
        //    courseLesson.CourseID = TextUtils.ToInt(cboCourse.EditValue);
        //    courseLesson.UrlPDF = txtPdfShow.Text;

        //    if (courseLesson.ID > 0)
        //    {
        //        courseLesson.UpdatedDate = DateTime.Now;
        //        courseLesson.UpdatedBy = Global.AppUserName;

        //        AddAndUploadFile(courseLesson);

        //        CourseLessonBO.Instance.Update(courseLesson);
        //        if (txtCheckUpDatePDF.Text == "Update")
        //        {
        //            UploadFilePDF(txtPdfHide.Text.Trim());
        //        }
        //        //MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        if (flag == 1)
        //        {
        //            this.DialogResult = DialogResult.OK;
        //        }
        //    }
        //    else
        //    {
        //        courseLesson.CreatedDate = DateTime.Now;
        //        courseLesson.CreatedBy = Global.AppUserName;
        //        //Check trùng mã khóa học
        //        CourseLessonModel CourseLessonModel = (CourseLessonModel)CourseLessonBO.Instance.FindByCode("Code", txtCodeLesson.Text.Trim());
        //        if (CourseLessonModel != null)
        //        {
        //            MessageBox.Show("Mã code đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;

        //        }

        //        int idLesson = (int)CourseLessonBO.Instance.Insert(courseLesson);
        //        courseLesson.ID = idLesson;
        //        if (File.Exists(txtPdfHide.Text))
        //        {
        //            UploadFilePDF(txtPdfHide.Text.Trim());
        //        }
        //        AddAndUploadFile(courseLesson);

        //        //MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        txtCodeLesson.Text = "";
        //        txtFiLeUrlHide.Text = "";
        //        txtFiLeUrlShow.Text = "";
        //        txtLessonName.Text = "";
        //        txtVideoUrl.Text = "";
        //        txtPdfShow.Text = "";
        //        txtPdfHide.Text = "";
        //        rtbAddLesson.HtmlText = "";

        //        if (flag == 1)
        //        {
        //            this.DialogResult = DialogResult.OK;
        //        }
        //    }
        //}

        //private void AddAndUploadFile(CourseLessonModel CourseLessonModel)
        //{

        //    try
        //    {
        //        if (txtTemp.Text == "Update")
        //        {
        //            CourseFileBO.Instance.DeleteByAttribute("LessonID", CourseLessonModel.ID);
        //        }

        //        string allfilepath = txtFiLeUrlHide.Text;
        //        string[] files = allfilepath.Split(';');

        //        for (int i = 0; i < files.Length; i++)
        //        {
        //            string nameFile = Path.GetFileName(files[i]);
        //            if (txtTemp.Text == "Update")
        //            {
        //                //Thêm file vào database
        //                CourseFileModel CourseFileModel = new CourseFileModel();
        //                CourseFileModel.LessonID = CourseLessonModel.ID;
        //                CourseFileModel.NameFile = nameFile;

        //                int idFileCourse = (int)CourseFileBO.Instance.Insert(CourseFileModel);
        //                //Đẩy file lên api
        //                UploadFileToAPI(idFileCourse, files[i]);
        //            }
        //            else
        //            {
        //                if (nameFile != "")
        //                {
        //                    //Thêm file vào database
        //                    CourseFileModel CourseFileModel = new CourseFileModel();
        //                    CourseFileModel.LessonID = CourseLessonModel.ID;
        //                    CourseFileModel.NameFile = nameFile;

        //                    int idFileCourse = (int)CourseFileBO.Instance.Insert(CourseFileModel);


        //                    //Đẩy file lên api
        //                    UploadFileToAPI(idFileCourse, files[i]);

        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error : " + ex.Message);
        //    }


        //}

        //private async void UploadFileToAPI(int idFileCourse, string pathfile)
        //{
        //    //pathfile = @"D:\RTCNoiBo (1)\RTCNoiBo\ cung cap giai phap 251122\RTC.HR-QĐ01_QUY ĐỊNH QUẢN LÝ CHẤM CÔNG LAO ĐỘNG - Copy.pdf";
        //    if (!File.Exists(pathfile))
        //    {
        //        MessageBox.Show($"File không tồn tại hoặc đường dẫn không đúng.\nVui lòng kiểm tra lại!\n{pathfile}", "Thông báo");
        //        flag = 0;
        //        return;
        //    }

        //    //string pathFoderApi = @"\\192.168.1.2\ftp\Upload\Course\";
        //    //string pathFoderApi = @"\\192.168.1.2\ftp\Upload\Course\Test\";
        //    string pathFoderApi = Global.PathCourseFile;

        //    string newFilePath = "";
        //    try
        //    {
        //        string apiEndpoint = " http://192.168.1.2:8083/api/Home/uploadfile?path=" + pathFoderApi;

        //        string filePath = pathfile;

        //        string extensionfile = Path.GetExtension(filePath);

        //        string newFileName = idFileCourse.ToString() + extensionfile;

        //        newFilePath = Path.Combine(Path.GetDirectoryName(filePath), newFileName);

        //        //newFilePath = @"D:\RTCNoiBo (1)\RTCNoiBo\ cung cap giai phap 251122\35.pdf";
        //        File.Copy(filePath, newFilePath);
        //        using (var client = new HttpClient())
        //        {
        //            using (var formData = new MultipartFormDataContent())
        //            {

        //                if (File.Exists(newFilePath))
        //                {

        //                    formData.Add(new StreamContent(File.OpenRead(newFilePath)), "file", newFileName);
        //                }
        //                else
        //                {

        //                    MessageBox.Show($"Tệp tin không tồn tại: {newFilePath}");
        //                }

        //                var response = await client.PostAsync(apiEndpoint, formData);
        //                var content = JsonConvert.DeserializeObject<Response>(response.Content.ReadAsStringAsync().Result);
        //                if (content.status != 1)
        //                {
        //                    MessageBox.Show(content.message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                }
        //                //if (response.IsSuccessStatusCode == true)
        //                //{
        //                //    File.Delete(newFilePath);
        //                //}
        //                //else
        //                //{
        //                //    //var content = response.Content.ReadAsStringAsync();
        //                //    MessageBox.Show("Upload file thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                //}


        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Upload file thất bại. ({ex.Message})!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //}

        //private async void UploadFilePDF(string path)
        //{
        //    if (!File.Exists(path))
        //    {
        //        MessageBox.Show($"File không tồn tại hoặc đường dẫn không đúng.\nVui lòng kiểm tra lại!\n{path}");
        //        return;
        //    }

        //    //string pathFoderApi = @"\\192.168.1.2\ftp\Upload\Course\PDFFileLesson\";
        //    //string pathFoderApi = @"\\192.168.1.2\ftp\Upload\Course\Test\";
        //    string pathFoderApi = Global.PathCoursePDF;

        //    try
        //    {
        //        string apiEndpoint = "http://192.168.1.2:8083/api/Home/uploadfile?path=" + pathFoderApi;

        //        using (var client = new HttpClient())
        //        {
        //            using (var formData = new MultipartFormDataContent())
        //            {
        //                formData.Add(new StreamContent(File.OpenRead(path)), "file", Path.GetFileName(path));

        //                //if (File.Exists(path))
        //                //{

        //                //}
        //                //else
        //                //{

        //                //    MessageBox.Show($"Tệp tin không tồn tại: {path}");
        //                //}

        //                var response = await client.PostAsync(apiEndpoint, formData);
        //                var content = JsonConvert.DeserializeObject<Response>(response.Content.ReadAsStringAsync().Result);

        //                if (content.status != 1)
        //                {
        //                    MessageBox.Show(content.message, "Thông báo");
        //                }
        //                //if (response.IsSuccessStatusCode == false)
        //                //{
        //                //    //MessageBox.Show("Upload file thất bại ! Lỗi do không đẩy file lên được api vui lòng chụp lại và gửi cho team phần mềm =.= ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                //    MessageBox.Show("Upload file thất bại!", "Thông báo");
        //                //}


        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Upload file thất bại.\n({ex.Message})!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //}

        private void btnAddFileLesson_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var fileSelected = dialog.FileNames;
                foreach (var file in fileSelected)
                {
                    CourseFileModel courseFile = new CourseFileModel();
                    courseFile.NameFile = Path.GetFileName(file);
                    courseFile.OriginPath = file;

                    listFile.Insert(0, courseFile);
                }
            }
            grdCourseFile.DataSource = listFile;
            grvCourseFile.RefreshData();

            //loadCourseFile();

            //using (var openFileDialog = new OpenFileDialog())
            //{
            //    openFileDialog.Multiselect = true;


            //    if (openFileDialog.ShowDialog() == DialogResult.OK)
            //    {
            //        string[] selectedFiles = openFileDialog.FileNames;
            //        string allFiles = string.Join(";", selectedFiles);
            //        List<string> allnamefile = new List<string>();
            //        foreach (var item in selectedFiles)
            //        {
            //            allnamefile.Add(Path.GetFileName(item));
            //        }
            //        txtFiLeUrlHide.Text = allFiles;
            //        txtFiLeUrlShow.Text = string.Join(";", allnamefile);
            //        txtTemp.Text = "Update";
            //    }
            //}
        }

        private void btnAddPdf_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "PDF files (*.pdf)|*.pdf";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                //txtPdfHide.Text = openFile.FileName;
                //txtCheckUpDatePDF.Text = "Update";
                //txtUrlPDF.Text = Path.GetFileName(openFile.FileName);

                txtUrlPDF.Text = openFile.FileName;
                isUpdate = false;
            }

            //using (var openFileDialog = new OpenFileDialog())
            //{
            //    openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            //    openFileDialog.FilterIndex = 1;
            //    openFileDialog.Multiselect = false;


            //    if (openFileDialog.ShowDialog() == DialogResult.OK)
            //    {

            //        txtPdfHide.Text = openFileDialog.FileName;
            //        txtUrlPDF.Text = Path.GetFileName(openFileDialog.FileName);
            //        txtCheckUpDatePDF.Text = "Update";

            //    }
            //}
        }

        private void cbxCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            int stt = TextUtils.ToInt(TextUtils.ExcuteScalar($"SELECT TOP 1 STT FROM dbo.CourseLesson WHERE CourseID = {TextUtils.ToInt(cbxCourse.SelectedValue)} ORDER BY STT DESC"));
            nupSTT.Value = TextUtils.ToInt(stt + 1);
        }

        private void txtVideoUrl_TextChanged(object sender, EventArgs e)
        {
            string input = txtVideoUrl.Text;

            // sử dụng biểu thức chính quy để lấy giá trị của thuộc tính src
            string pattern = @"src=\""(.*?)\""";

            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                string src = match.Groups[1].Value;
                txtVideoUrl.Text = src;
            }
        }

        private void cboCourse_EditValueChanged(object sender, EventArgs e)
        {

            int stt = TextUtils.ToInt(TextUtils.ExcuteScalar($"SELECT TOP 1 STT FROM dbo.CourseLesson WHERE CourseID = {TextUtils.ToInt(cboCourse.EditValue)} ORDER BY STT DESC"));
            nupSTT.Value = TextUtils.ToInt(stt + 1);
        }

        private void btnDeleteCourseFile_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvCourseFile.GetFocusedRowCellValue(colID));
            string fileName = TextUtils.ToString(grvCourseFile.GetFocusedRowCellValue(colNameFile));
            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá file đính kèm:\n[{fileName}]\nkhông?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                //============ lee min khooi 30/10/2024 =======================
                if (!lstFileDeleted.Contains(id)) lstFileDeleted.Add(id);
                grvCourseFile.DeleteSelectedRows();
            }
        }


        private void grvCourseFile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(grvCourseFile.GetFocusedRowCellValue(grvCourseFile.FocusedColumn)));
                e.Handled = true;
            }
        }

        //=================== lee min khooi update 12/08/2024 ===============================================
        private bool ValidateUser()
        {
            var ex1 = new Expression("PositionCode", "TBP/PP", "<>");
            List<KPIPositionModel> listPositions = SQLHelper<KPIPositionModel>.FindByExpression(ex1);
            string lstCode = string.Join(",", listPositions.Select(x => x.ID.ToString()));

            List<EmployeeModel> lstPro = SQLHelper<EmployeeModel>.ProcedureToList("spGetAllEmployeePositionID", new string[] { "@KPIPostionID" },
                                                                                      new object[] { lstCode });
            bool isProSen = lstPro.Any(p => p.ID == Global.EmployeeID);
            bool isCreated = TextUtils.ToString(Global.AppUserName) != courseLesson.CreatedBy && courseLesson.ID > 0;

            if (Global.IsAdmin)
            {
                return true;
            }
            else if (Global.EmployeeID == 54) //Trưởng phòng KT Phạm Văn Quyền
            {
                return true;
            }
            else if (!isProSen && isCreated)
            {
                //MessageBox.Show("Bạn không thể cập nhật bài học của người khác!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        //================= end update 12/09/2024 =============================================================



        // ========================== lee min khooi update 29/10/2024 =======================================
        private void LoadCourseCatalog()
        {
            DataTable dt = TextUtils.GetTable("spGetCourseCatalog");
            cboCourseCatalog.Properties.DataSource = dt;
            cboCourseCatalog.Properties.DisplayMember = "Name";
            cboCourseCatalog.Properties.ValueMember = "ID";
            cboCourseCatalog.EditValue = courseCatalogID;
            LoadDataLessonCopy();
        }
        private void cboCourseCatalog_EditValueChanged(object sender, EventArgs e)
        {
            LoadDataLessonCopy();

        }
        private void LoadDataLessonCopy()
        {
            int ID = TextUtils.ToInt(cboCourseCatalog.EditValue);
            if (ID == 0) ID = -1;
            DataTable dt = TextUtils.LoadDataFromSP("spGetLessonByCourseCatalogID", "A", new string[] { "@CourseCatalogID" }, new object[] { ID });
            cboLessonCopy.Properties.DataSource = dt;
            cboLessonCopy.Properties.DisplayMember = "LessonTitle";
            cboLessonCopy.Properties.ValueMember = "ID";

        }
        private void tgwIsCopy_Toggled(object sender, EventArgs e)
        {
            bool isActive = TextUtils.ToBoolean(tglIsCopy.EditValue);
            cboCourseCatalog.Enabled = cboLessonCopy.Enabled = isActive;
        }

        private void cboLessonCopy_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                int lessonID = TextUtils.ToInt(cboLessonCopy.EditValue);
                if (lessonID <= 0) return;

                CourseLessonModel lessonModel = SQLHelper<CourseLessonModel>.FindByID(lessonID);
                if (lessonModel.ID <= 0) return;

                txtLessonName.Text = lessonModel.LessonTitle;
                txtVideoUrl.Text = lessonModel.VideoURL;
                txtUrlPDF.Text = lessonModel.UrlPDF;
                rtbAddLesson.HtmlText = lessonModel.LessonContent;

                listFile = SQLHelper<CourseFileModel>.FindByAttribute("LessonID", lessonID).OrderByDescending(x => x.ID).ToList();
                grdCourseFile.DataSource = listFile;
                grvCourseFile.RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.ToString(), "Thông báo");
            }
        }
        private void UpdateDataCopy(int courseLessonID)
        {
            try
            {
                //Kiểm tra xem có các bản copy hay không
                Expression eprs1 = new Expression("IsDeleted", 0);
                Expression eprs2 = new Expression("LessonCopyID", courseLessonID);
                List<CourseLessonModel> lstCpy = SQLHelper<CourseLessonModel>.FindByExpression(eprs1.And(eprs2));
                string lstIDs = string.Join(",", lstCpy.Select(p => p.ID));


                Dictionary<string, object> newDict = new Dictionary<string, object>()
                {
                    {"LessonTitle", txtLessonName.Text.Trim()},
                    {"VideoURL", txtVideoUrl.Text.Trim()},
                    {"UrlPDF", Path.GetFileName(txtUrlPDF.Text.Trim())},
                    {"LessonContent", rtbAddLesson.HtmlText},
                    {"UpdatedBy", Global.AppFullName },
                    {"UpdatedDate", DateTime.Now }
                };

                Expression ex1 = new Expression("ID", lstIDs, "IN");
                //SQLHelper<CourseLessonModel>.UpdateFields(newDict, ex1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.ToString(), "Thông báo");
            }
        }

        //Update lại file của các lessonID
        private void UpdateCopyFile(int lessonCopyID)
        {
            try
            {
                Expression ex1 = new Expression("LessonCopyID", lessonCopyID); // Tìm kiếm lại các bản copy
                Expression ex2 = new Expression("ID", lessonCopyID); // Tìm kiếm lại bản gốc
                List<CourseLessonModel> lstLesson = SQLHelper<CourseLessonModel>.FindByExpression(ex1.Or(ex2));
                //foreach (CourseLessonModel item in lstLesson) // update lại dữ liệu mới
                //{
                //    item.LessonTitle = txtLessonName.Text.Trim();
                //    item.VideoURL = txtVideoUrl.Text.Trim();
                //    item.UrlPDF = Path.GetFileName(txtUrlPDF.Text.Trim());
                //    item.LessonContent = rtbAddLesson.HtmlText;
                //    SQLHelper<CourseLessonModel>.Update(item);
                //}


                //Xóa file cũ rồi insert lại
                List<CourseLessonModel> lstCopy = lstLesson.Where(p => p.ID != lessonCopyID).ToList(); //Các bản sao chép

                Expression ex3 = new Expression("LessonID", lessonCopyID);
                Expression ex4 = new Expression("IsDeleted", 0);
                List<CourseFileModel> lstFile = SQLHelper<CourseFileModel>.FindByExpression(ex3.And(ex4)); //File cần lưu 


                string fileIds = string.Join(",", lstCopy.Select(p => p.ID));
                Expression epr1 = new Expression("LessonID", fileIds, "IN");
                Dictionary<string, object> newDict = new Dictionary<string, object>()
                {
                    {"IsDeleted", 1 },
                    {"UpdatedBy", Global.AppFullName },
                    {"UpdatedDate", DateTime.Now }
                };
                //SQLHelper<CourseFileModel>.UpdateFields(newDict, epr1);

                foreach (CourseLessonModel item in lstCopy)
                {
                    foreach (CourseFileModel itemFile in lstFile)
                    {
                        itemFile.ID = 0;
                        itemFile.LessonID = item.ID;
                        item.UpdatedBy = Global.AppFullName;
                        item.UpdatedDate = DateTime.Now;
                        SQLHelper<CourseFileModel>.Insert(itemFile);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.ToString(), "Thông báo");
            }
        }

        void SaveLog(CourseLessonModel current)
        {
            try
            {
                CourseLessonModel oldModel = SQLHelper<CourseLessonModel>.FindByID(current.ID);
                var resultCompare = TextUtils.DeepEquals(oldModel, current);
                bool equal = TextUtils.ToBoolean(resultCompare.GetType().GetProperty("equal").GetValue(resultCompare));

                if (!equal)
                {
                    string contentLog = $"Sửa bài học (CourseLesson) {current.ID}:\n";
                    List<string> propertys = (List<string>)resultCompare.GetType().GetProperty("property").GetValue(resultCompare);
                    foreach (string property in propertys)
                    {
                        string propertyText = TextUtils.ToString(cGlobVar.CourseLessonLog.GetType().GetProperty(property).GetValue(cGlobVar.CourseLessonLog));
                        contentLog += $"{propertyText.ToUpper()}:\n" +
                                      $"từ {oldModel.GetType().GetProperty(property).GetValue(oldModel)}\n" +
                                      $"thành {current.GetType().GetProperty(property).GetValue(current)}\n\n";
                    }

                    CourseLessonLogModel log = new CourseLessonLogModel();
                    log.CourseLessonID = current.ID;
                    log.DateLog = DateTime.Now;
                    log.ContentLog = contentLog;

                    SQLHelper<CourseLessonLogModel>.Insert(log);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.ToString(), "Thông báo");
            }
        }
    }
}
