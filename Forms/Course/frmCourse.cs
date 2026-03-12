using BMS;
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
    public partial class frmCourse : _Forms
    {

        public frmCourse()
        {
            InitializeComponent();
        }
        #region Load Data

        #region Load all


        //Lấy danh mục khóa học
        private void loadDataCourseCatalog()
        {
            DataTable dt = new DataTable();
            dt = TextUtils.GetTable("spGetCourseCatalog");
            grdCourseCatalog.DataSource = dt;

        }
        //Lấy danh sách khóa học
        private void loadDataCourse()
        {
            int ID = TextUtils.ToInt(grvCourseCatalog.GetFocusedRowCellValue(colIDCourseCatalog));
            if (ID == 0) { grdCourse.DataSource = null; return; }
            DataTable dt = TextUtils.LoadDataFromSP("spGetCourseNew", "A",
                                    new string[] { "@CourseCatalogID", "@UserID", "@Status" },
                                    new object[] { ID, Global.UserID, -1 });
            grdCourse.DataSource = dt;

        }
        //Lấy danh sách bài học
        private void loadDataLesson()
        {
            DataTable dt = new DataTable();
            int ID = TextUtils.ToInt(grvCourse.GetFocusedRowCellValue(colIDCourse));
            if (ID == 0) { grdLesson.DataSource = null; return; }
            dt = TextUtils.LoadDataFromSP("spGetLesson", "A", new string[] { "@CourseID" }, new object[] { ID });
            grdLesson.DataSource = dt;

        }
        #endregion
        //Sự kiện khi click vào danh mục khóa học
        private void grvCourseCatalog_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadDataCourse();
            loadDataLesson();


        }
        //Sự kiện khi click vào khóa học
        private void grvCourse_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadDataLesson();

        }
        #endregion

        #region CRUD Danh mục khóa học
        //Thêm danh mục khóa học
        private void btnAddCourseCatalog_Click(object sender, EventArgs e)
        {
            frmAddCourseCatalog frm = new frmAddCourseCatalog();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadDataCourseCatalog();

            }
        }
        //Sửa danh mục khóa học
        private void grdCourseCatalog_DoubleClick(object sender, EventArgs e)
        {
            btnEditCourseCatalog_Click(null, null);

        }
        private void btnDeleteCourseCatalog_Click(object sender, EventArgs e)
        {
            var id = TextUtils.ToInt(grvCourseCatalog.GetFocusedRowCellValue(colIDCourseCatalog));
            var nameCourseCatalog = TextUtils.ToString(grvCourseCatalog.GetFocusedRowCellValue(colNameCourseCatalog));
            if (id == 0) return;
            // lee min khooi update 16/09/2024 =================
            CourseCatalogModel model = SQLHelper<CourseCatalogModel>.FindByID(id);

            var ex1 = new Expression("PositionCode", "TBP/PP", "<>");
            List<KPIPositionModel> listPositions = SQLHelper<KPIPositionModel>.FindByExpression(ex1);
            string lstCode = string.Join(",", listPositions.Select(x => x.ID.ToString()));

            List<EmployeeModel> lstPro = SQLHelper<EmployeeModel>.ProcedureToList("spGetAllEmployeePositionID", new string[] { "@KPIPostionID" },
                                                                                      new object[] { lstCode });
            bool isProSen = lstPro.Any(p => p.ID == Global.EmployeeID);
            bool isCreated = TextUtils.ToString(Global.AppUserName) != model.CreatedBy && model.ID > 0;
            if (isProSen && isCreated)
            {
                MessageBox.Show("Bạn không thể thay đổi trạng thái danh mục của người khác!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // end update lee min khooi update 16/09/2024 =================
            DialogResult dr = MessageBox.Show($"Bạn có chắc chắn muốn đổi trạng thái danh mục [{nameCourseCatalog}] thành [Ngừng hoạt động] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                model.DeleteFlag = !model.DeleteFlag;
                CourseCatalogBO.Instance.Update(model);
                loadDataCourseCatalog();
            }
        }



        #endregion
        #region CRUD Khóa Học


        private void btnAddCourse_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvCourseCatalog.GetFocusedRowCellValue(colIDCourseCatalog));
            //if (id == 0) return;

            frmAddCourse_V2 frm = new frmAddCourse_V2();
            frm.Group.CourseCatalogID = id;
           
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadDataCourse();

            }
        }
        private void grdCourse_DoubleClick(object sender, EventArgs e)
        {
            btnEditCourse_Click(null, null);

        }
        private void btnDeleteCourse_Click(object sender, EventArgs e)
        {
            var id = TextUtils.ToInt(grvCourse.GetFocusedRowCellValue(colIDCourse));
            var nameCourse = TextUtils.ToString(grvCourse.GetFocusedRowCellValue(colCourseName));
            if (id == 0) return;

            if (!ValidateUser()) return;

            DialogResult dr = MessageBox.Show($"Bạn có chắc chắn muốn đổi trạng thái khoá học [{nameCourse}] thành [Ngừng hoạt động] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {

                Dictionary<string, object> newDict = new Dictionary<string, object>()
                {
                    {"DeleteFlag", false},
                    {"UpdatedBy", Global.AppFullName },
                    {"UpdatedDate", DateTime.Now },
                };
                SQLHelper<CourseModel>.UpdateFieldsByID(newDict, id);
                //CourseModel course = SQLHelper<CourseModel>.FindByID(id);
                //course.DeleteFlag = !course.DeleteFlag;
                //CourseBO.Instance.Update(course);

                loadDataCourse();
            }

        }


        #endregion

        #region CRUD Bài học
        private void btnAddLesson_Click(object sender, EventArgs e)
        {
            var id = TextUtils.ToInt(grvCourse.GetFocusedRowCellValue(colIDCourse));


            int courseCatalogID = TextUtils.ToInt(grvCourseCatalog.GetFocusedRowCellValue(colIDCourseCatalog));
            if (id == 0) return;
            frmAddLesson frmAddLesson = new frmAddLesson();
            frmAddLesson.courseLesson.CourseID = id;
            frmAddLesson.courseCatalogID = courseCatalogID;
            if (frmAddLesson.ShowDialog() == DialogResult.OK)
            {
                loadDataLesson();
            }
        }
        private void grdLesson_DoubleClick(object sender, EventArgs e)
        {
            btnEditLesson_Click(null, null);
        }
        private void btnDeleteLesson_Click(object sender, EventArgs e)
        {
            var id = TextUtils.ToInt(grvLesson.GetFocusedRowCellValue(colIDLesson));
            var nameCourse = TextUtils.ToString(grvLesson.GetFocusedRowCellValue(colLessonTitle));
            if (id == 0) return;

            // lee min khooi update 16/09/2024 =================
            CourseLessonModel model = SQLHelper<CourseLessonModel>.FindByID(id);

            var ex1 = new Expression("PositionCode", "TBP/PP", "<>");
            List<KPIPositionModel> listPositions = SQLHelper<KPIPositionModel>.FindByExpression(ex1);
            string lstCode = string.Join(",", listPositions.Select(x => x.ID.ToString()));

            List<EmployeeModel> lstPro = SQLHelper<EmployeeModel>.ProcedureToList("spGetAllEmployeePositionID", new string[] { "@KPIPostionID" },
                                                                                      new object[] { lstCode });
            bool isProSen = lstPro.Any(p => p.ID == Global.EmployeeID);
            bool isCreated = TextUtils.ToString(Global.AppUserName) != model.CreatedBy && model.ID > 0;
            if (isProSen && isCreated)
            {
                MessageBox.Show("Bạn không thể xóa bài học của người khác!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // end update lee min khooi update 16/09/2024 =================

            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa bài " + nameCourse + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {

                // lee min khooi update 01/11/2024
                Dictionary<string, object> newDict = new Dictionary<string, object>()
                {
                    {"IsDeleted", 1},
                    {"UpdatedBy", Global.AppFullName},
                    {"UpdatedDate", DateTime.Now}
                };
                SQLHelper<CourseLessonModel>.UpdateFieldsByID(newDict, id);
                loadDataLesson();
            }
        }


        #endregion

        private void btnGetFrmEmail_Click(object sender, EventArgs e)
        {
            frmToolGetEmailEmployee frm = new frmToolGetEmailEmployee();
            frm.ShowDialog();

        }

        private void bntResetCatalog_Click(object sender, EventArgs e)
        {
            int rowHandle = grvCourseCatalog.FocusedRowHandle;
            loadDataCourseCatalog();

            grvCourseCatalog.FocusedRowHandle = rowHandle;
        }

        private void btnResetCourse_Click(object sender, EventArgs e)
        {
            int rowHandle = grvCourse.FocusedRowHandle;
            loadDataCourse();

            grvCourse.FocusedRowHandle = rowHandle;
        }

        private void btnResetLesson_Click(object sender, EventArgs e)
        {
            int rowHandle = grvLesson.FocusedRowHandle;
            loadDataLesson();
            grvLesson.FocusedRowHandle = rowHandle;
        }

        private void btnDeleteCouse_Click(object sender, EventArgs e)
        {

            btnDeleteCourse_Click(null, null);

            //return;
            //var courseID = TextUtils.ToInt(grvCourse.GetFocusedRowCellValue(colIDCourse));
            //var nameCourse = TextUtils.ToString(grvCourse.GetFocusedRowCellValue(colCourseName));
            //if (courseID == 0) return;

            ////var dt = TextUtils.Select($"SELECT * FROM dbo.CourseLesson WHERE CourseID = {courseID}");
            //CourseLessonModel courseLesson = SQLHelper<CourseLessonModel>.SqlToModel($"SELECT * FROM dbo.CourseLesson WHERE CourseID = {courseID}");

            //if (courseLesson.ID > 0)
            //{
            //    MessageBox.Show($"Không thể xóa Khóa học [{nameCourse}].\nVui lòng xóa câu hỏi trước!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //else
            //{
            //    DialogResult result = MessageBox.Show($"Bạn có chắc muỗn xóa khóa học [{nameCourse}] !", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
            //    if (result == DialogResult.Yes)
            //    {
            //        CourseBO.Instance.Delete(courseID);
            //        grvCourse.DeleteSelectedRows();

            //        //loadDataCourse();
            //    }
            //}

        }

        private void btnDeleteDanhMuc_Click(object sender, EventArgs e)
        {
            btnDeleteCourseCatalog_Click(null, null);


            //var catalogID = TextUtils.ToInt(grvCourseCatalog.GetFocusedRowCellValue(colIDCourseCatalog));
            //var nameCatalog = TextUtils.ToString(grvCourseCatalog.GetFocusedRowCellValue(colNameCourseCatalog));
            //if (catalogID == 0) return;

            ////var dt = TextUtils.Select($"SELECT * FROM dbo.Course WHERE CourseCatalogID = {catalogID}");
            //CourseModel course = SQLHelper<CourseModel>.SqlToModel($"SELECT * FROM dbo.Course WHERE CourseCatalogID = {catalogID}");
            //if (course.ID > 0)
            //{
            //    MessageBox.Show($"Không thể xóa Danh mục [{nameCatalog}].\nVui lòng xóa khóa học trước!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //else
            //{
            //    DialogResult result = MessageBox.Show($"Bạn có chắc muỗn xóa danh mục [{nameCatalog}] !", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
            //    if (result == DialogResult.Yes)
            //    {
            //        CourseCatalogBO.Instance.Delete(catalogID);

            //        grvCourseCatalog.DeleteSelectedRows();

            //        //loadDataCourseCatalog();
            //    }
            //}

        }

        private void frmCourse_Load(object sender, EventArgs e)
        {
            loadDataCourseCatalog();
            loadDataCourse();
            loadDataLesson();
        }

        private void btnEditCourseCatalog_Click(object sender, EventArgs e)
        {
            int rowHandle = grvCourseCatalog.FocusedRowHandle;
            int id = TextUtils.ToInt(grvCourseCatalog.GetFocusedRowCellValue(colIDCourseCatalog));
            if (id == 0) return;
            CourseCatalogModel model = (CourseCatalogModel)CourseCatalogBO.Instance.FindByPK(id);
            frmAddCourseCatalog frm = new frmAddCourseCatalog();
            frm.Group = model;
            frm.Text = "SỬA DANH MỤC KHÓA HỌC  ";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadDataCourseCatalog();
                grvCourseCatalog.FocusedRowHandle = rowHandle;
            }
        }

        private void btnEditLesson_Click(object sender, EventArgs e)
        {
            int rowHandle = grvLesson.FocusedRowHandle;
            int id = TextUtils.ToInt(grvLesson.GetFocusedRowCellValue(colIDLesson));
            if (id == 0) return;

            CourseLessonModel model = SQLHelper<CourseLessonModel>.FindByID(id);
            frmAddLesson frm = new frmAddLesson();
            frm.courseLesson = model;
            //frm.courseLesson = model;

            frm.Text = "SỬA BÀI HỌC  " + model.LessonTitle;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadDataLesson();
                grvLesson.FocusedRowHandle = rowHandle;
            }
        }

        private void btnEditCourse_Click(object sender, EventArgs e)
        {
            int rowHandle = grvCourse.FocusedRowHandle;
            int id = TextUtils.ToInt(grvCourse.GetFocusedRowCellValue(colIDCourse));
            int totalAmountQuestion = TextUtils.ToInt(grvCourse.GetFocusedRowCellValue(colAmountMultiChoice));
            if (id == 0) return;

            CourseModel model = SQLHelper<CourseModel>.FindByID(id);

            frmAddCourse_V2 frm = new frmAddCourse_V2();

            frm.Group = model;
            frm.TotalAmountQuestion = totalAmountQuestion;
            frm.Text = "SỬA KHÓA HỌC  ";
            frm.txtTotalNumberQuestion.Value = TextUtils.ToInt(grvCourse.GetFocusedRowCellValue(colAmountMultiChoice));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadDataCourse();

                grvCourse.FocusedRowHandle = rowHandle;
            }
        }

        private void grdCourseCatalog_Click(object sender, EventArgs e)
        {

        }

        //=================== lee min khooi update 12/08/2024 ===============================================
        private bool ValidateUser()
        {
            int courseID = TextUtils.ToInt(grvCourse.GetFocusedRowCellValue(colIDCourse));
            CourseModel model = SQLHelper<CourseModel>.FindByID(courseID);

            var ex1 = new Expression("PositionCode", "TBP/PP", "<>");
            List<KPIPositionModel> listPositions = SQLHelper<KPIPositionModel>.FindByExpression(ex1);
            string lstCode = string.Join(",", listPositions.Select(x => x.ID.ToString()));

            List<EmployeeModel> lstPro = SQLHelper<EmployeeModel>.ProcedureToList("spGetAllEmployeePositionID", new string[] { "@KPIPostionID" },
                                                                                      new object[] { lstCode });
            bool isProSen = lstPro.Any(p => p.ID == Global.EmployeeID);
            bool isCreated = TextUtils.ToString(Global.AppUserName) != model.CreatedBy && model.ID > 0;
            if (isProSen && isCreated)
            {
                MessageBox.Show("Bạn không thể thay đổi trạng thái khoá học của người khác!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmAddExpertise frm = new frmAddExpertise();
            frm.Show();
        }
        //================= end update 12/09/2024 =============================================================

    }
}
