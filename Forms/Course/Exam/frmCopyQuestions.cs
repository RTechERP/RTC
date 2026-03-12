using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmCopyQuestions : _Forms
    {
        //public int examType;
        //public int courseExamID;


        //public int courseExamID = 0;
        public frmCopyQuestions()
        {
            InitializeComponent();
        }
        private void frmCopyQuestions_Load(object sender, EventArgs e)
        {
            //cboExamType.SelectedIndex = 0;

            loadQuestions();
            LoadCourseCatalog();
            loadCourse();
            LoadCourseExam();
        }

        void LoadCourseExam() //load đề thi
        {
            //int courseID = TextUtils.ToInt(cboCourse.EditValue);
            int courseID = 0;
            int courseCatalogID = TextUtils.ToInt(cboCourseCatalog.EditValue);
            courseCatalogID = 0;
            int examType = cboExamType.SelectedIndex;

            DataTable dt = TextUtils.LoadDataFromSP("spGetCourseExam", "A",
                new string[] { "@CourseID", "@CourseCatalogID", "@ExamType" },
                new object[] { courseID, courseCatalogID, examType });

            cboCourseExam.Properties.ValueMember = "ID";
            cboCourseExam.Properties.DisplayMember = "CodeExam";
            cboCourseExam.Properties.DataSource = dt;

            //cboCourseExam.EditValue = courseExamID;

        }

        private void LoadCourseCatalog()
        {
            var ct = TextUtils.Select("select ID,Code,Name from CourseCatalog");

            //DataRow defaultOption = ct.NewRow();
            //defaultOption["ID"] = 0;
            //defaultOption["Name"] = "- Tất cả -";
            //ct.Rows.InsertAt(defaultOption, 0);
            cboCourseCatalog.Properties.DataSource = ct;
            cboCourseCatalog.Properties.DisplayMember = "Name";
            cboCourseCatalog.Properties.ValueMember = "ID";
        }

        private void loadCourse()
        {
            int ctID = TextUtils.ToInt(cboCourseCatalog.EditValue);
            var courses = TextUtils.Select($"SELECT ID,Code,NameCourse FROM Course c WHERE c.CourseCatalogID = {ctID}");
            //if (courses.Rows.Count > 0)
            //{
            //    DataRow defaultOption = courses.NewRow();
            //    defaultOption["ID"] = 0;
            //    defaultOption["NameCourse"] = "- Tất cả -";
            //    courses.Rows.InsertAt(defaultOption, 0);
            //}
            cboCourse.Properties.DataSource = courses;
            cboCourse.Properties.DisplayMember = "NameCourse";
            cboCourse.Properties.ValueMember = "ID";

            //cboCourseTo.Properties.DataSource = courses;
            //cboCourseTo.Properties.DisplayMember = "NameCourse";
            //cboCourseTo.Properties.ValueMember = "ID";
        }
        private void loadQuestions()
        {
            //var IDs = string.Join(",", questionIDs);
            int examType = cboExamType.SelectedIndex;
            int catalogID = TextUtils.ToInt(cboCourseCatalog.EditValue);
            int courseID = TextUtils.ToInt(cboCourse.EditValue);
            string filterText = TextUtils.ToString(txtFilterText.Text);
            DataTable data = TextUtils.LoadDataFromSP("spGetCourseQuestionContent", "a",
                new string[] { "@ExamType", "@CatalogID", "@CourseID", "@FilterText" },
                new object[] { examType, catalogID, courseID, filterText });
            grdQuestion.DataSource = data;
            if (examType != 1) //Thực hành - Bài tập
            {
                colDapAnA.Visible = false;
                colDapAnB.Visible = false;
                colDapAnC.Visible = false;
                colDapAnD.Visible = false;
                colCorrectAnswers.Visible = false;
            }
            else // Trắc nghiệm
            {
                colDapAnA.Visible = true;
                colDapAnB.Visible = true;
                colDapAnC.Visible = true;
                colDapAnD.Visible = true;
                colCorrectAnswers.Visible = true;
                colDapAnA.VisibleIndex = 4;
                colDapAnB.VisibleIndex = 5;
                colDapAnC.VisibleIndex = 6;
                colDapAnD.VisibleIndex = 7;
                colCorrectAnswers.VisibleIndex = 8;
            }
            grvQuestion.ExpandAllGroups();
        }

        bool CheckValidate()
        {
            //if (cboExamType.SelectedIndex <= 0)
            //{
            //    MessageBox.Show("Vui lòng chọn Loại!", "Thông báo");
            //    return false;
            //}
            string courseExamCode = cboCourseExam.Text;

            if (TextUtils.ToInt(cboCourseExam.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng chọn Đến đề thi!", "Thông báo");
                return false;
            }

            if (string.IsNullOrWhiteSpace(courseExamCode))
            {
                MessageBox.Show("Vui lòng chọn Đến đề thi (Mã đề muốn copy đến)!", "Thông báo");
                return false;
            }

            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!CheckValidate()) return;

            List<int> questionIDs = new List<int>();
            var rows = grvQuestion.GetSelectedRows().ToList();
            //int examType = cboExamType.SelectedIndex;
            //int courseExamID = TextUtils.ToInt(cboCourseExam.EditValue);

            DataRowView dataCourseExam = (DataRowView)cboCourseExam.GetSelectedDataRow();
            if (dataCourseExam == null) return;
            int examType = TextUtils.ToInt(dataCourseExam["ExamType"]);
            int courseExamID = TextUtils.ToInt(dataCourseExam["ID"]);

            List<CourseQuestionModel> listQuestions = SQLHelper<CourseQuestionModel>.FindByAttribute("CourseExamId", courseExamID);
            int maxSttQuestion = listQuestions.Count <= 0 ? 0 : listQuestions.Max(x => x.STT);
            //int STT = TextUtils.ToInt(TextUtils.ExcuteScalar($"SELECT MAX(STT) FROM CourseQuestion WHERE CourseExamId = {courseExamID}"));
            rows.ForEach(r => questionIDs.Add(TextUtils.ToInt(grvQuestion.GetRowCellValue(r, colCourseQuestionID))));

            var exp1 = new Expression("CourseExamId", courseExamID);
            foreach (var id in questionIDs)
            {
                if (id <= 0) continue;
                //bool existed = TextUtils.ToBoolean(TextUtils.ExcuteScalar($"select 1 from CourseQuestion where ID={id} and CourseExamId={courseExamID}"));
                var exp2 = new Expression("ID", id);
                CourseQuestionModel questionExisted = SQLHelper<CourseQuestionModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault() ?? new CourseQuestionModel();
                //bool existed = questionExisted.ID > 0;
                if (questionExisted.ID > 0) continue;

                CourseQuestionModel question = SQLHelper<CourseQuestionModel>.FindByID(id);
                question.ID = 0;
                question.CourseExamId = courseExamID;
                question.STT = ++maxSttQuestion;
                var copiedQuestion = SQLHelper<CourseQuestionModel>.Insert(question);

                if (examType == 1) //trắc nghiệm
                {
                    List<CourseAnswersModel> answers = SQLHelper<CourseAnswersModel>.FindByAttribute("CourseQuestionId", id);
                    answers.ForEach(a => a.CourseQuestionId = copiedQuestion.ID);
                    foreach (var a in answers)
                    {
                        SQLHelper<CourseAnswersModel>.Insert(a);
                    }

                    //Get đáp án đúng của câu đáp án sau khi copy
                    List<CourseRightAnswersModel> righAnswers = SQLHelper<CourseRightAnswersModel>.FindByAttribute("CourseQuestionId", id);
                    foreach (var item in righAnswers)
                    {
                        var answ = SQLHelper<CourseAnswersModel>.FindByID(item.CourseAnswerID);
                    }


                    List<CourseRightAnswersModel> correctAnswers = SQLHelper<CourseRightAnswersModel>.FindByAttribute("CourseQuestionID", id);
                    foreach (var item in righAnswers)
                    {
                        var answOld = SQLHelper<CourseAnswersModel>.FindByID(item.CourseAnswerID);

                        var expAns1 = new Expression("AnswerNumber", answOld.AnswerNumber);
                        var expAns2 = new Expression("CourseQuestionId", copiedQuestion.ID);
                        var answNew = SQLHelper<CourseAnswersModel>.FindByExpression(expAns1.And(expAns2)).FirstOrDefault() ?? new CourseAnswersModel();

                        CourseRightAnswersModel a = new CourseRightAnswersModel()
                        {
                            CourseQuestionID = copiedQuestion.ID,
                            CourseAnswerID = answNew.ID
                        };
                        SQLHelper<CourseRightAnswersModel>.Insert(a);
                    }

                    //correctAnswers.ForEach(a => a.CourseQuestionID = copiedQuestion.ID);
                    //foreach (var a in correctAnswers)
                    //{
                    //    SQLHelper<CourseRightAnswersModel>.Insert(a);
                    //}
                }

            }
            this.DialogResult = DialogResult.OK;
        }
        //private void cboCourse_EditValueChanged(object sender, EventArgs e)
        //{
        //    LoadCourseExam();
        //    loadQuestions();
        //}

        private void cboCourse_EditValueChanged_1(object sender, EventArgs e)
        {
            loadQuestions();
            LoadCourseExam();
        }
        private void cboCourse_Popup(object sender, EventArgs e)
        {
            grvCatalog.GridControl.BeginInvoke(new Action(() =>
            {
                grvCatalog.ExpandAllGroups();
            }));
            //grvCourseExam.ExpandAllGroups();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadQuestions();
            LoadCourseExam();

        }

        private void txtFilterText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) loadQuestions();
        }

        private void cboCourseCatalog_EditValueChanged(object sender, EventArgs e)
        {
            loadCourse();
            loadQuestions();
        }

        private void cboExamType_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadQuestions();
            LoadCourseExam();
        }
    }
}