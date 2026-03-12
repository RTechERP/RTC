using BMS.Model;
using BMS.Utils;
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
    public partial class frmLessonExam : _Forms
    {
        public int CourseID = 0;
        public int LessonID = 0;
        public int ExamID = 0;
        public frmLessonExam()
        {
            InitializeComponent();
        }

        private void frmLessonExam_Load(object sender, EventArgs e)
        {
            LoadCourseLesson();
            loadExam();
        }
        void loadExam()
        {
            var model = SQLHelper<CourseExamModel>.FindByID(ExamID);
            txtCodeExam.Text = model.CodeExam;
            txtNameExam.Text = model.NameExam;
            cboLesson.EditValue = model.LessonID;
            txtGoal.Text = TextUtils.ToString(model.Goal);
            txtTestTime.Text = TextUtils.ToString(model.TestTime);
            cboExamType.SelectedIndex = model.ExamType;

            if (ExamID <= 0)
            {
                var modelLesson = SQLHelper<CourseLessonModel>.FindByID(LessonID);
                cboExamType.SelectedIndex = 1;
                cboLesson.EditValue = LessonID;
                txtCodeExam.Text = model == null ? "" : modelLesson.Code + "_TN";
                txtNameExam.Clear();
                txtGoal.Text = "100";
                txtTestTime.Text = "60";
            }
        }
        private void LoadCourseLesson()
        {
            Expression ex1 = new Expression("CourseID", CourseID);
            //Expression ex2 = new Expression();
            List<CourseLessonModel> lst = SQLHelper<CourseLessonModel>.FindByExpression(ex1);
            cboLesson.Properties.DataSource = lst;
            cboLesson.Properties.ValueMember = "ID";
            cboLesson.Properties.DisplayMember = "Code";
        }
        bool CheckValidate()
        {
            int lessonID = TextUtils.ToInt(cboLesson.EditValue);
            if (lessonID <= 0)
            {
                MessageBox.Show($"Vui lòng nhập Bài học!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }


            if (cboExamType.SelectedIndex <= 0)
            {
                MessageBox.Show("Vui lòng nhập Loại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else if (cboExamType.SelectedIndex < 2)
            {
                if (TextUtils.ToInt(txtTestTime.Text.Trim()) <= 0)
                {
                    MessageBox.Show("Vui lòng nhập Thời gian!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtNameExam.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Tên đề thi!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (TextUtils.ToDecimal(txtGoal.Text.Trim()) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Số điểm cần đạt!" + TextUtils.ToDecimal(txtGoal.Text.Trim()), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }


            var exp1 = new Expression("LessonID", lessonID);
            var exp2 = new Expression("ExamType", cboExamType.SelectedIndex);
            var exp3 = new Expression("ID", ExamID, "<>");
            var checkDuplicateCode = SQLHelper<CourseExamModel>.FindByExpression(exp1.And(exp2).And(exp3));
            if (checkDuplicateCode.Count > 0)
            {
                MessageBox.Show($"Bạn đã tạo đề thi [{cboExamType.Text}] cho bài học [{cboLesson.Text}]!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (string.IsNullOrEmpty(txtCodeExam.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Mã đề thi!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            var exp4 = new Expression("LessonID", lessonID);
            var exp5 = new Expression("CodeExam", txtCodeExam.Text.Trim());
            var exp6 = new Expression("ID", ExamID, "<>");

            var courseExam = SQLHelper<CourseExamModel>.FindByExpression(exp4.And(exp5).And(exp6));
            if (courseExam.Count > 0)
            {
                MessageBox.Show($"Mã đề thi [{txtCodeExam.Text.Trim()}] đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }


            return true;
        }
        private bool SaveData()
        {
            if (!CheckValidate()) return false;
            CourseExamModel model = SQLHelper<CourseExamModel>.FindByID(ExamID);


            model.CodeExam = txtCodeExam.Text;
            model.NameExam = txtNameExam.Text;
            model.LessonID = TextUtils.ToInt(cboLesson.EditValue);
            model.CourseId = -1;
            model.Goal = TextUtils.ToDecimal(txtGoal.Text);
            model.ExamType = cboExamType.SelectedIndex;
            model.TestTime = TextUtils.ToInt(txtTestTime.Text);

            if (model.ID > 0) SQLHelper<CourseExamModel>.Update(model);
            else SQLHelper<CourseExamModel>.Insert(model);


            return true;
        }
        void setCode(string valCode, string code)
        {
            txtCodeExam.Text = valCode == "" ? "A_B" : valCode;
            string[] valcode = txtCodeExam.Text.Trim().Split('_');
            if (valcode.Length < 2) return;

            if (!string.IsNullOrEmpty(code))
            {
                valcode[0] = code;
            }

            if (cboExamType.SelectedIndex == 3)
            {
                valcode[1] = "BT";
                label10.Visible = false;
            }
            else if (cboExamType.SelectedIndex == 2)
            {
                valcode[1] = "TH";
                label10.Visible = false;
            }
            else if (cboExamType.SelectedIndex == 1)
            {
                valcode[1] = "TN";
                label10.Visible = true;
            }
            else
            {
                label10.Visible = true;
            }
            txtCodeExam.Text = string.Join("_", valcode);
        }

        private void cboExamType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodeExam.Text.Trim()))
            {
                setCode(txtCodeExam.Text, "");
            }
        }

        private void cboLesson_EditValueChanged(object sender, EventArgs e)
        {
            string courseLesson = cboLesson.Text;
            txtCodeExam.Text = courseLesson == null ? "" : TextUtils.ToString(courseLesson);
            setCode("", txtCodeExam.Text);
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            if (SaveData()) this.DialogResult = DialogResult.OK;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                ExamID = 0;
                loadExam();
            }
        }

        private void frmLessonExam_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
