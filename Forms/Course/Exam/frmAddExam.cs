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
    public partial class frmAddExam : _Forms
    {
        public int _examID;
        public int _courseID;
        public frmAddExam()
        {
            InitializeComponent();
        }

        private void frmAddExam_Load(object sender, EventArgs e)
        {
            //cboExamType.SelectedIndex = 1;
            loadCourse();
            loadExam();
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                this.DialogResult = DialogResult.OK;
            }

        }

        void loadExam()
        {
            if (_examID > 0)
            {
                //var model = (CourseExamModel)CourseExamBO.Instance.FindByPK(_examID);
                var model = SQLHelper<CourseExamModel>.FindByID(_examID);
                txtCodeExam.Text = model.CodeExam;
                txtNameExam.Text = model.NameExam;
                cboCourse.EditValue = model.CourseId;
                txtGoal.Text = TextUtils.ToString(model.Goal);
                txtTestTime.Text = TextUtils.ToString(model.TestTime);
                cboExamType.SelectedIndex = model.ExamType;
                //this.Text = "SỬA ĐỀ THI";
            }
            else
            {
                cboExamType.SelectedIndex = 1;
                cboCourse.EditValue = _courseID;
                //var model = ()CourseBO.Instance.FindByPK(_courseID);
                var model = SQLHelper<CourseModel>.FindByID(_courseID);
                txtCodeExam.Text = model == null ? "" : model.Code + "_TN";


                txtNameExam.Clear();
            }
        }
        bool saveData()
        {
            if (!validate()) return false;
            CourseExamModel model = new CourseExamModel();

            if (_examID > 0)
            {
                //model = (CourseExamModel)CourseExamBO.Instance.FindByPK(_examID);
                model = SQLHelper<CourseExamModel>.FindByID(_examID);
            }
            model.CodeExam = txtCodeExam.Text;
            model.NameExam = txtNameExam.Text;
            model.CourseId = TextUtils.ToInt(cboCourse.EditValue);
            model.Goal = TextUtils.ToDecimal(txtGoal.Text);
            model.ExamType = cboExamType.SelectedIndex;
            //model.TestTime = cboExamType.SelectedIndex == 2 ? 0 : TextUtils.ToInt(txtTestTime.Text);
            model.TestTime = TextUtils.ToInt(txtTestTime.Text);
            if (model.ID > 0)
            {
                //model.ID = id;

                //CourseExamBO.Instance.Update(model);
                SQLHelper<CourseExamModel>.Update(model);
            }
            else
            {

                //CourseExamBO.Instance.Insert(model);
                SQLHelper<CourseExamModel>.Insert(model);
            }
            return true;
        }
        bool validate()
        {
            int courseID = TextUtils.ToInt(cboCourse.EditValue);
            if (courseID <= 0)
            {
                MessageBox.Show($"Vui lòng nhập Khoá học!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                var exp1 = new Expression("CourseId", courseID);
                var exp2 = new Expression("ExamType", cboExamType.SelectedIndex);
                var exp3 = new Expression("ID", _examID, "<>");
                var courseExam = SQLHelper<CourseExamModel>.FindByExpression(exp1.And(exp2).And(exp3));
                if (courseExam.Count > 0)
                {
                    MessageBox.Show($"Bạn đã tạo đề thi [{cboExamType.Text}] cho khoá học [{cboCourse.Text}]!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtCodeExam.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Mã đề thi!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                var exp1 = new Expression("CourseId", courseID);
                var exp2 = new Expression("CodeExam", txtCodeExam.Text.Trim());
                var exp3 = new Expression("ID", _examID, "<>");

                var courseExam = SQLHelper<CourseExamModel>.FindByExpression(exp1.And(exp2).And(exp3));
                if (courseExam.Count > 0)
                {
                    MessageBox.Show($"Mã đề thi [{txtCodeExam.Text.Trim()}] đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
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

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                loadExam();
            }
        }



        void loadCourse()
        {
            //List<CourseModel> courses = SQLHelper<CourseModel>.SqlToList("SELECT * FROM dbo.Course");
            //DataTable dt = TextUtils.LoadDataFromSP("spGetCourseNew", "A", new string[] { "@DepartmentID", "@Status" }, new object[] { Global.DepartmentID, -1 });
            int department = Global.DepartmentID;
            if (Global.EmployeeID == 54) department = 0;
            DataTable dt = TextUtils.LoadDataFromSP("spGetCourseNew", "A", new string[] { "@DepartmentID", "@Status" }, new object[] { department, -1 });

            cboCourse.Properties.DataSource = dt;
            cboCourse.Properties.DisplayMember = "NameCourse";
            cboCourse.Properties.ValueMember = "ID";
        }

        private void cboCourse_EditValueChanged(object sender, EventArgs e)
        {
            var course = (DataRowView)cboCourse.GetSelectedDataRow();
            txtCodeExam.Text = course == null ? "" : TextUtils.ToString(course["Code"]);
            setCode("", txtCodeExam.Text);

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

        private void frmAddExam_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}