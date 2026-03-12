using BMS.Business;
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
    public partial class frmCourseExamResultPractice : _Forms
    {
        //public CourseExamResultModel examResult = new CourseExamResultModel();
        public CourseExamResultModel courseExamResult = new CourseExamResultModel();
        public int courseID = 0;
        public int courseExamID = 0;
        public int type = 0;
        public frmCourseExamResultPractice()
        {
            InitializeComponent();
        }

        private void frmCourseExamResultPractice_Load(object sender, EventArgs e)
        {
            loadCourse();
            loadEmployee();
            loadData();
        }

        void loadCourse()
        {
            int department = Global.DepartmentID;
            if (Global.EmployeeID == 54) department = 2;
            //DataTable dt = TextUtils.LoadDataFromSP("spGetCourseNew", "A", new string[] { "@DepartmentID", "@Status" }, new object[] { department, -1 });

            DataTable dt = TextUtils.LoadDataFromSP("spGetCourseNew", "A",
                                                        new string[] { "@UserID", "@Status", "@DepartmentID" },
                                                        new object[] { Global.UserID, -1, department });



            cboCourse.Properties.ValueMember = "ID";
            cboCourse.Properties.DisplayMember = "NameCourse";
            cboCourse.Properties.DataSource = dt;
            cboCourse.EditValue = courseID;
        }

        void loadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee","A",new string[] { "@Status" }, new object[] { 0});
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;
        }

        void loadData()
        {
            if (type == 2)
                cbType.SelectedIndex = 0;
            else
                cbType.SelectedIndex = 1;
            cboCourse.EditValue = courseID;
            if (courseExamResult.ID > 0)
            { 
                cboEmployee.EditValue = courseExamResult.EmployeeId;
                txtPracticePoints.Text = TextUtils.ToString(courseExamResult.PracticePoints);
                chkEvaluate.Checked = courseExamResult.Evaluate;
                //dtpDateStart.EditValue = courseExamResult.DateStart;
                //dtpDateEnd.EditValue = courseExamResult.DateEnd;
                txtNote.Text = courseExamResult.Note;
            }
            else
            {
                //cboCourse.EditValue = 0;
                cboEmployee.EditValue = 0;
                txtPracticePoints.Text = "";
                chkEvaluate.Checked = false;
                //dtpDateStart.EditValue = null;
                //dtpDateEnd.EditValue = null;
                txtNote.Text = "";
            }
        }

        bool validate()
        {
            if (TextUtils.ToInt(cboCourse.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Đề thi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (TextUtils.ToInt(cboEmployee.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            //if (TextUtils.ToDecimal(txtPracticePoints.Text) <= 0)
            //{
            //    MessageBox.Show("Vui lòng nhập Điểm thi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}
            return true;
        }

        bool saveData()
        {
            if (!validate())
            {
                return false;
            }
            var exp1 = new Expression("CourseId", courseID);
            var exp2 = new Expression("ExamType", type);
            var _courseExam = SQLHelper<CourseExamModel>.FindByExpression(exp1.And(exp2));

            if (_courseExam == null && _courseExam.Count <=0) return false;
            courseExamResult.CourseExamId = TextUtils.ToInt(_courseExam[0].ID);
            courseExamResult.EmployeeId = TextUtils.ToInt(cboEmployee.EditValue);
            courseExamResult.PracticePoints = TextUtils.ToDecimal(txtPracticePoints.Text);
            courseExamResult.Evaluate = chkEvaluate.Checked;
            //courseExamResult.DateStart = TextUtils.ToDate4(dtpDateStart.EditValue);
            //courseExamResult.DateEnd = TextUtils.ToDate4(dtpDateEnd.EditValue);
            courseExamResult.Note = txtNote.Text;
            if (courseExamResult.ID > 0)
            {
               var response = SQLHelper<CourseExamResultModel>.Update(courseExamResult);
                if (!response.IsSuccess)
                {
                    MessageBox.Show(response.ErrorText, "Thông báo");
                    return response.IsSuccess;
                }
            }
            else
            {
                SQLHelper<CourseExamResultModel>.Insert(courseExamResult);
            }

            return true;
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                courseExamResult = new CourseExamResultModel();
                loadData();
            }
        }
    }
}
