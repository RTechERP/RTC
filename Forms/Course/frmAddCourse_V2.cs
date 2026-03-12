using BMS;
using BMS.Business;
using BMS.Model;
using BMS.Utils;
using Forms.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmAddCourse_V2 : _Forms
    {
        public CourseModel Group = new CourseModel();
        public int TotalAmountQuestion = 0;
        int flag;

        bool _lockEvent = false;
        public frmAddCourse_V2()
        {
            InitializeComponent();
        }
        private bool ValidateForm()
        {
            if (TextUtils.ToInt(cboCourseCatalog.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Danh mục!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (string.IsNullOrEmpty(txtCodeCourse.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Mã khoá học!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else
            {
                var exp1 = new Expression("Code", txtCodeCourse.Text.Trim());
                var exp2 = new Expression("ID", Group.ID, "<>");
                var exp3 = new Expression("CourseCatalogID", TextUtils.ToInt(cboCourseCatalog.EditValue));
                var exp4 = new Expression(CourseModel_Enum.DeleteFlag, 0, "<>");

                var course = SQLHelper<CourseModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4));
                if (course.Count > 0)
                {
                    MessageBox.Show($"Mã khoá học [{txtCodeCourse.Text.Trim()}] đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtCourseName.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Tên khoá học!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            //Khánh update 07/02/2025
            //if (TextUtils.ToInt(cboCourseType.EditValue) <= 0)
            //{
            //    MessageBox.Show("Vui lòng chọn type!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}


            //int totalQuestionCount = TextUtils.ToInt(txtQuestionCount.Value);
            //if ((TotalAmountQuestion == 0 && totalQuestionCount != 0)
            //    || (TotalAmountQuestion > 0 && (totalQuestionCount < 0 || totalQuestionCount > TotalAmountQuestion)))
            //{
            //    MessageBox.Show($"Số câu trắc nghiệm không hợp lệ", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}
            //decimal duration = TextUtils.ToDecimal(txtQuestionDuration.Value);
            //if ((totalQuestionCount == 0 && duration != 0) || (totalQuestionCount > 0 && duration <= 0))
            //{
            //    MessageBox.Show($"Thời lượng câu hỏi không hợp lệ", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}
            //if (pictureBox1.Image == null)
            //{
            //    MessageBox.Show("Vui lòng chọn ảnh khóa học", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}
            return true;

        }

        private void frmAddCourse_Load(object sender, EventArgs e)
        {

            _lockEvent = true;

            //if (Group.ID <= 0)
            //{
            //    txtInstructor.Text = Global.AppFullName;
            //}

            //LOAD CBX phòng ban
            DataTable dt = TextUtils.Select("SELECT * FROM dbo.CourseCatalog");
            //cbxCourseCatalog.DataSource = dt;
            //cbxCourseCatalog.DisplayMember = "Name";
            //cbxCourseCatalog.ValueMember = "ID";
            //cbxCourseCatalog.SelectedValue = Group.CourseCatalogID;
            //txtQuestionCount.Maximum = TotalAmountQuestion;
            loadCourseCatalog();
            LoadIdea();
            //Khánh update 07/02/2025
            loadCourseType();

            loadEmployee();
            loadData();

            btnSave.Enabled = btnSaveAndClose.Enabled = ValidateUser();
            _lockEvent = false;
        }


        void loadCourseType()
        {
            List<CourseTypeModel> lstCourseType = SQLHelper<CourseTypeModel>.FindByAttribute(CourseTypeModel_Enum.IsDeleted.ToString(), 0)
                                                                            .OrderByDescending(x => x.STT).ToList();
            cboCourseType.Properties.ValueMember = "ID";
            cboCourseType.Properties.DisplayMember = "CourseTypeName";
            cboCourseType.Properties.DataSource = lstCourseType;
        }


        private void loadEmployee()
        {

            //DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@DepartmentID", "@Status" }, new object[] { 0, 0 });
            //cboEmployee.Properties.DataSource = dt;
            //cboEmployee.Properties.DisplayMember = "FullName";
            //cboEmployee.Properties.ValueMember = "ID";

            //cboEmployee.EditValue = Global.EmployeeID;
        }

        //Load combo danh mục khoá học
        void loadCourseCatalog()
        {
            DataTable dt = TextUtils.GetTable("spGetCourseCatalog");
            cboCourseCatalog.Properties.ValueMember = "ID";
            cboCourseCatalog.Properties.DisplayMember = "Name";
            cboCourseCatalog.Properties.DataSource = dt;
            cboCourseCatalog.EditValue = Group.CourseCatalogID;


            //=============== lee min khooi update 31/10/2024======================
            cboCourseCatalogCopy.Properties.ValueMember = "ID";
            cboCourseCatalogCopy.Properties.DisplayMember = "Name";
            cboCourseCatalogCopy.Properties.DataSource = dt;
            cboCourseCatalogCopy.EditValue = Group.CourseCatalogID;
            LoadCourseCopy();

        }

        void LoadIdea()
        {
            DateTime dateEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            int registerTypeID = TextUtils.ToInt(cboCourseCatalog.EditValue);
            DataTable dt = TextUtils.LoadDataFromSP("spGetRegisterIdea", "A",
                new string[] { "@EmployeeID", "@DepartmentID", "@AuthorID", "@DateStart", "@DateEnd", "@FilterText", "@PageNumber", "@PageSize", "@RegisterTypeID" },
                new object[] { 0, 0, Global.EmployeeID, TextUtils.MIN_DATE, dateEnd, "", 1, 999999999, registerTypeID });

            cboIdea.Properties.ValueMember = "ID";
            cboIdea.Properties.DisplayMember = "Description";
            cboIdea.Properties.DataSource = dt;
        }

        //Load data cho sửa
        private void loadData()
        {
            try
            {
                if (Group.ID > 0)
                {
                    //var fileCourse = (CourseFileModel)CourseFileBO.Instance.FindByPK(Group.FileCourseID);
                    var fileCourse = SQLHelper<CourseFileModel>.FindByID(TextUtils.ToInt(Group.FileCourseID));
                    txtCodeCourse.Text = Group.Code.ToString();
                    txtCourseName.Text = Group.NameCourse;
                    txtInstructor.Text = Group.Instructor;
                    //txtImgCourse.Text = fileCourse.NameFile;
                    cbxCourseCatalog.SelectedValue = Group.CourseCatalogID;

                    txtSTT.Value = TextUtils.ToInt(Group.STT);
                    //btnSave.Text = "Cất và sửa";
                    //await LoadImageAsync(fileCourse.NameFile);
                    txtQuestionCount.Value = TextUtils.ToDecimal(Group.QuestionCount);
                    txtQuestionDuration.Value = TextUtils.ToDecimal(Group.QuestionDuration);
                    chkDeleteFlag.Checked = TextUtils.ToBoolean(Group.DeleteFlag);
                    txtLeadTime.Value = TextUtils.ToDecimal(Group.LeadTime);
                    cboCourseType.EditValue = Group.CourseTypeID;
                    //cboEmployee.EditValue = Group.EmployeeID;
                }
                else
                {
                    //cbxCourseCatalog.SelectedValue = Group.CourseCatalogID;
                    //CourseModel course = SQLHelper<CourseModel>.SqlToModel($"SELECT TOP 1 * FROM dbo.Course WHERE CourseCatalogID = {TextUtils.ToInt(cbxCourseCatalog.SelectedValue)} ORDER BY STT DESC");
                    //txtSTT.Value = TextUtils.ToInt(course.STT) + 1;
                    //int courseCatalogID = TextUtils.ToInt(cbxCourseCatalog.SelectedValue);
                    //List<CourseModel> lstCourse = SQLHelper<CourseModel>.FindByAttribute("CourseCatalogID", courseCatalogID);
                    //txtSTT.Value = TextUtils.ToInt(lstCourse.Max(p => p.STT)) + 1;
                    txtSTT.Value = LoadMaxSTT();
                    //txtCodeCourse.Text = "";
                    txtCourseName.Text = "";
                    chkDeleteFlag.Checked = true;

                }


                //Binding cbo idea
                RegisterIdeaModel idea = SQLHelper<RegisterIdeaModel>.FindByAttribute(RegisterIdeaModel_Enum.CourseID.ToString(), Group.ID).FirstOrDefault();
                idea = idea ?? new RegisterIdeaModel();

                cboIdea.EditValue = idea.ID;
            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR " + ex.Message, "Thông báo");
            }
        }

        //Load image lúc sửa
        //private async Task LoadImageAsync(string fileName)
        //{
        //    string url = "https://taimienphi.vn/tmp/cf/aut/anh-gai-xinh-1.jpg";
        //    using (var client = new HttpClient())
        //    {
        //        var response = await client.GetAsync(url);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var stream = await response.Content.ReadAsStreamAsync();
        //            var image = Image.FromStream(stream);

        //            // Hiển thị ảnh trong PictureBox
        //            pictureBox1.Image = image;
        //        }
        //    }
        //}

        private void frmAddCourse_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        private bool SaveGroup()
        {
            if (!ValidateForm()) return false;
            bool isCopy = Group.CourseCopyID > 0 || TextUtils.ToBoolean(tglIsCopy.EditValue);
            int courseCopyID = Group.CourseCopyID > 0 ? TextUtils.ToInt(Group.CourseCopyID) : TextUtils.ToInt(cboCourseCopy.EditValue);

            Group.Code = txtCodeCourse.Text.Trim();
            Group.NameCourse = txtCourseName.Text;
            //Group.ImgCourse = txtImgCourse.Text;
            //Group.Instructor = txtInstructor.Text;
            Group.Instructor = Global.AppFullName;
            //Group.CourseCatalogID = TextUtils.ToInt(cbxCourseCatalog.SelectedValue);
            Group.CourseCatalogID = TextUtils.ToInt(cboCourseCatalog.EditValue);
            //Group.DeleteFlag = true;
            Group.QuestionCount = TextUtils.ToInt(txtQuestionCount.Value);
            Group.QuestionDuration = TextUtils.ToDecimal(txtQuestionDuration.Text);
            Group.DeleteFlag = chkDeleteFlag.Checked;

            Group.STT = TextUtils.ToInt(txtSTT.Value);
            Group.LeadTime = txtLeadTime.Value;

            Group.CourseCopyID = isCopy ? courseCopyID : 0;

            Group.CourseTypeID = TextUtils.ToInt(cboCourseType.EditValue);
            //Group.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);

            if (Group.ID > 0)
            {
                SaveLog(Group);
                Group.UpdatedDate = DateTime.Now;
                Group.UpdatedBy = Global.AppUserName;
                SQLHelper<CourseModel>.Update(Group);
            }
            else
            {
                Group.CreatedDate = DateTime.Now;
                Group.CreatedBy = Global.AppUserName;
                Group.ID = SQLHelper<CourseModel>.Insert(Group).ID;
            }

            // lee min khooi update 1/11/2024
            UpdateDataCopy(Group.ID);
            if (isCopy)
            {
                UpdateCopyDetails(TextUtils.ToInt(Group.CourseCopyID));

                string contentLog = $"Đã copy Khóa học {Group.CourseCopyID} --> {Group.ID}";
                CourseLessonLogModel log = new CourseLessonLogModel();
                log.CourseLessonID = Group.ID;
                log.DateLog = DateTime.Now;
                log.ContentLog = contentLog;
                SQLHelper<CourseLessonLogModel>.Insert(log);
            }


            //Update tiptrick
            int ideaID = TextUtils.ToInt(cboIdea.EditValue);
            //RegisterIdeaModel idea = SQLHelper<RegisterIdeaModel>.FindByID(ideaID);

            var myDict = new Dictionary<string, object>()
            {
                { RegisterIdeaModel_Enum.CourseID.ToString(),Group.ID},
                { RegisterIdeaModel_Enum.UpdatedBy.ToString(),Global.AppCodeName},
                { RegisterIdeaModel_Enum.UpdatedDate.ToString(),DateTime.Now},
            };
            SQLHelper<RegisterIdeaModel>.UpdateFieldsByID(myDict, ideaID);
            return true;
        }

        //Cất và đóng
        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            if (SaveGroup())
            {
                this.DialogResult = DialogResult.OK;
            }

        }
        //Cất và thêm mới
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveGroup())
            {
                Group = new CourseModel();
                loadData();
            }
        }

        private void cbxCourseCatalog_SelectedIndexChanged(object sender, EventArgs e)
        {
            CourseModel course = SQLHelper<CourseModel>.SqlToModel($"SELECT TOP 1 * FROM dbo.Course WHERE CourseCatalogID = {TextUtils.ToInt(cbxCourseCatalog.SelectedValue)} ORDER BY ID DESC");
            txtSTT.Value = TextUtils.ToInt(course.STT) + 1;
        }

        private void cboCourseCatalog_EditValueChanged(object sender, EventArgs e)
        {
            if (_lockEvent) return;
            //CourseModel course = SQLHelper<CourseModel>.SqlToModel($"SELECT TOP 1 * FROM dbo.Course WHERE CourseCatalogID = {TextUtils.ToInt(cboCourseCatalog.EditValue)} ORDER BY ID DESC");
            //txtSTT.Value = TextUtils.ToInt(course.STT + 1);
            bool isCopy = TextUtils.ToBoolean(tglIsCopy.EditValue);
            if (!isCopy)
            {
                LoadMaxSTT();
                LoadCode();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtQuestionDuration_ValueChanged(object sender, EventArgs e)
        {

        }

        //private void btnOpenImg_Click(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        // Tạo hộp thoại Open File Dialog
        //        OpenFileDialog openFileDialog1 = new OpenFileDialog();

        //        // Thiết lập các tùy chọn cho hộp thoại
        //        openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif";
        //        openFileDialog1.Title = "Chọn ảnh";

        //        // Nếu người dùng chọn một tệp tin
        //        if (openFileDialog1.ShowDialog() == DialogResult.OK)
        //        {
        //            // Đọc ảnh từ tệp tin
        //            Image image = Image.FromFile(openFileDialog1.FileName);
        //            txtImgCourse.Text = openFileDialog1.FileName;

        //            // Thiết lập ảnh cho PictureBox
        //            pictureBox1.Image = image;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);

        //    }


        //}
        //=================== lee min khooi update 12/08/2024 ===============================================
        private bool ValidateUser()
        {
            var ex1 = new Expression("PositionCode", "TBP/PP", "<>");
            List<KPIPositionModel> listPositions = SQLHelper<KPIPositionModel>.FindByExpression(ex1);
            string lstCode = string.Join(",", listPositions.Select(x => x.ID.ToString()));

            List<EmployeeModel> lstPro = SQLHelper<EmployeeModel>.ProcedureToList("spGetAllEmployeePositionID", new string[] { "@KPIPostionID" },
                                                                                      new object[] { lstCode });
            bool isProSen = lstPro.Any(p => p.ID == Global.EmployeeID);
            bool isCreated = TextUtils.ToString(Global.AppUserName) != Group.CreatedBy && Group.ID > 0;

            //bool isEmployee = !(Group.EmployeeID == Global.EmployeeID && Group.ID > 0);

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
                //MessageBox.Show("Bạn không thể cập nhật!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        //================= end update 12/09/2024 =============================================================



        //=================================== lee min khooi update 31/10/2024 ======================================
        private void tglIsCopy_Toggled(object sender, EventArgs e)
        {
            bool isActive = TextUtils.ToBoolean(tglIsCopy.EditValue);
            cboCourseCatalogCopy.Enabled = cboCourseCopy.Enabled = isActive;
        }

        private void LoadCourseCopy()
        {
            int ID = TextUtils.ToInt(cboCourseCatalogCopy.EditValue);
            if (ID == 0) return;

            DataTable dt = TextUtils.LoadDataFromSP("spGetCourseNew", "A", new string[] { "@CourseCatalogID", "@UserID", "@Status" }, new object[] { ID, Global.UserID, -1 });
            cboCourseCopy.Properties.DataSource = dt;
            cboCourseCopy.Properties.DisplayMember = "NameCourse";
            cboCourseCopy.Properties.ValueMember = "ID";
        }

        private void cboCourseCatalogCopy_EditValueChanged(object sender, EventArgs e)
        {
            LoadCourseCopy();
        }


        private void UpdateDataCopy(int courseID)
        {
            //Kiểm tra xem có các bản copy hay không
            Expression eprs2 = new Expression("CourseCopyID", courseID);
            List<CourseModel> lstCpy = SQLHelper<CourseModel>.FindByExpression(eprs2);
            string lstIDs = string.Join(",", lstCpy.Select(p => p.ID));

            Dictionary<string, object> newDict = new Dictionary<string, object>()
                {
                    {"NameCourse", txtCourseName.Text},
                    {"Instructor", Global.AppFullName},
                    //{"CourseCatalogID", TextUtils.ToInt(cboCourseCatalog.EditValue)},
                    {"QuestionCount", TextUtils.ToInt(txtQuestionCount.Value)},
                    {"QuestionDuration", TextUtils.ToDecimal(txtQuestionDuration.Text)},
                    {"DeleteFlag", chkDeleteFlag.Checked},
                    {"LeadTime", txtLeadTime.Value},
                    {"UpdatedBy", Global.AppFullName },
                    {"UpdatedDate", DateTime.Now }
                };

            Expression ex1 = new Expression("ID", lstIDs, "IN");
            //SQLHelper<CourseModel>.UpdateFields(newDict, ex1);
        }

        //Update lại file của các lessonID
        private void UpdateCopyDetails(int courseCopyID)
        {
            Expression ex1 = new Expression("CourseCopyID", courseCopyID); // Tìm kiếm lại các bản copy
            Expression ex2 = new Expression("ID", courseCopyID); // Tìm kiếm lại bản gốc
            List<CourseModel> lstCourse = SQLHelper<CourseModel>.FindByExpression(ex1.Or(ex2));
            //foreach (CourseModel item in lstCourse) // update lại dữ liệu mới
            //{
            //    item.NameCourse = txtCourseName.Text;
            //    item.Instructor = Global.AppFullName;
            //    //item.CourseCatalogID = TextUtils.ToInt(cboCourseCatalog.EditValue);
            //    item.QuestionCount = TextUtils.ToInt(txtQuestionCount.Value);
            //    item.QuestionDuration = TextUtils.ToDecimal(txtQuestionDuration.Text);
            //    item.DeleteFlag = chkDeleteFlag.Checked;
            //    item.LeadTime = txtLeadTime.Value;
            //    SQLHelper<CourseModel>.Update(item);
            //}



            List<CourseModel> lstCopy = lstCourse.Where(p => p.ID != courseCopyID).ToList(); //Các bản sao chép
            Expression ex3 = new Expression("CourseID", courseCopyID);
            Expression ex4 = new Expression("ISNULL(IsDeleted,0)", 1, "<>");
            List<CourseLessonModel> lstLessonCopy = SQLHelper<CourseLessonModel>.FindByExpression(ex3.And(ex4)); //Các lesson từ course copy


            string fileIds = string.Join(",", lstCopy.Select(p => p.ID));
            Expression epr1 = new Expression("CourseID", fileIds, "IN");
            Dictionary<string, object> newDict = new Dictionary<string, object>()
                {
                    {"IsDeleted", 1 },
                    {"UpdatedBy", Global.AppFullName },
                    {"UpdatedDate", DateTime.Now }
                };
            //SQLHelper<CourseLessonModel>.UpdateFields(newDict, epr1);

            foreach (CourseModel item in lstCopy)
            {
                foreach (CourseLessonModel itemLesson in lstLessonCopy)
                {
                    itemLesson.LessonCopyID = itemLesson.ID;
                    itemLesson.ID = 0;
                    itemLesson.CourseID = item.ID;
                    itemLesson.UpdatedBy = Global.AppFullName;
                    itemLesson.UpdatedDate = DateTime.Now;
                    SQLHelper<CourseLessonModel>.Insert(itemLesson);
                }
            }
        }

        void SaveLog(CourseModel current)
        {
            CourseModel oldModel = SQLHelper<CourseModel>.FindByID(current.ID);
            var resultCompare = TextUtils.DeepEquals(oldModel, current);
            bool equal = TextUtils.ToBoolean(resultCompare.GetType().GetProperty("equal").GetValue(resultCompare));

            if (!equal)
            {
                string contentLog = $"Sửa khóa học (Course) {current.ID}:\n";
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

        private void cboCourseCopy_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataRowView courseCopy = (DataRowView)cboCourseCopy.GetSelectedDataRow();
                if (courseCopy == null)
                {
                    LoadCode();
                }
                else
                {
                    txtCodeCourse.Text = TextUtils.ToString(courseCopy["Code"]);
                    txtCourseName.Text = TextUtils.ToString(courseCopy["NameCourse"]);

                    //decimal tesst = TextUtils.ToDecimal(courseCopy["QuestionCount"]);
                    //txtQuestionCount.Value = tesst;
                    //txtQuestionDuration.Value = TextUtils.ToDecimal(courseCopy["QuestionDuration"]);
                    txtLeadTime.Value = TextUtils.ToDecimal(courseCopy["LeadTime"]);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return;
            }

        }

        private void LoadCode()
        {
            DataRowView courseCatalog = (DataRowView)cboCourseCatalog.GetSelectedDataRow();
            if (courseCatalog == null) return;
            string code = TextUtils.ToString(courseCatalog["Code"]);
            txtCodeCourse.Text = $"{code}_{LoadMaxSTT()}";
        }

        private int LoadMaxSTT()
        {
            int courseCatalogID = TextUtils.ToInt(cboCourseCatalog.EditValue);
            List<CourseModel> lstCourse = SQLHelper<CourseModel>.FindByAttribute("CourseCatalogID", courseCatalogID);
            int stt = TextUtils.ToInt(lstCourse.Max(p => p.STT)) + 1;
            return stt;
        }

        private void btnAddCourseType_Click(object sender, EventArgs e)
        {
            frmCourseTypeDetail frm = new frmCourseTypeDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadCourseType();
            }
        }
    }

}
