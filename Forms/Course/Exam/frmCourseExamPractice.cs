using BMS.Business;
using BMS.Model;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmCourseExamPractice : _Forms
    {
        public frmCourseExamPractice()
        {
            InitializeComponent();
        }

        private void frmCourseExamPractice_Load(object sender, EventArgs e)
        {
            LoadEmployee();
            loadCourse();
        }

        void loadCourse()
        {
            grdCourse.DataSource = null;
            int employeeID = TextUtils.ToInt(cboEmployee.EditValue);
            DataTable dt = TextUtils.LoadDataFromSP("spGetDataCourse", "A",
                                                        new string[] { "@EmployeeID" },
                                                        new object[] { employeeID });
            //DataTable dt = TextUtils.GetTable("spGetDataCourse");
            grdCourse.DataSource = dt;

            grvCourse.ExpandGroupLevel(0);
            grvCourse.ExpandGroupLevel(1);
        }

        void LoadEmployee()
        {
            DataTable list = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = list;
        }

        //void loadCourseExamResult() // kết quả thi trắc nghiệm
        //{
        //    DataSet dts = new DataSet();
        //    int courseExamID = TextUtils.ToInt(grvCourse.GetFocusedRowCellValue(colID));
        //    if (courseExamID <= 0) return;
        //    DataTable dt = TextUtils.LoadDataFromSP("spGetCourseExamResult", "A",
        //                                                new string[] { "@CourseExamID", "@EmployeeID", "@OrderNumber" },
        //                                                new object[] { courseExamID, 0, 1 });

        //    checkExam(courseExamID);
        //    grdData_TN.DataSource = dt;

        //}

        void checkExam(int CourseID)
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetCourseExam", "A", new string[] { "@CourseID" }, new object[] { CourseID });
            if (dt.Select("ExamType = 1").Length <= 0)
            {
                xtraTabControl1.TabPages[0].PageVisible = false;
            }
            else
            {
                xtraTabControl1.TabPages[0].PageVisible = true;
            }
            if (dt.Select("ExamType = 2").Length <= 0)
            {
                xtraTabControl1.TabPages[1].PageVisible = false;
            }
            else
            {
                xtraTabControl1.TabPages[1].PageVisible = true;
            }
            if (dt.Select("ExamType = 3").Length <= 0)
            {
                xtraTabControl1.TabPages[2].PageVisible = false;
            }
            else
            {
                xtraTabControl1.TabPages[2].PageVisible = true;
            }
        }

        void loadData()
        {
            grdData_TN.DataSource = null;
            grdData.DataSource = null;
            grdData_BT.DataSource = null;

            //DataTable dt = new DataTable();
            int courseID = TextUtils.ToInt(grvCourse.GetFocusedRowCellValue(colID));
            checkExam(courseID);
            //int courseExamID = TextUtils.ToInt(grvCourse.GetFocusedRowCellValue(colID));
            DataSet dts = TextUtils.LoadDataSetFromSP("spGetCourseExamPractice",
                                                        new string[] { "@CourseID" },
                                                        new object[] { courseID });

            List<CourseExamModel> listData = SQLHelper<CourseExamModel>.FindByAttribute("CourseId", courseID).ToList();
            //Tìm ID bài trắc nghiệm ---------- LMK
            CourseExamModel examResult = listData.FirstOrDefault(p => p.ExamType == 1);
            if (examResult != null && examResult.ID > 0)
            {
                int employeeID = TextUtils.ToInt(cboEmployee.EditValue);
                DataTable dtTN = TextUtils.LoadDataFromSP("spGetCourseExamResult", "A",
                                                   new string[] { "@CourseExamID", "@EmployeeID", "@OrderNumber" },
                                                   new object[] { examResult.ID, employeeID, 1 });

                grdData_TN.DataSource = dtTN;
            }

            //Tìm ID bài thực hành---------- LMK
            CourseExamModel examPractice = listData.FirstOrDefault(p => p.ExamType == 2);
            if (examPractice != null && examPractice.ID > 0)
            {
                DataTable dtTH = TextUtils.LoadDataFromSP("spGetCourseExamPracticeResult", "A",
                                                  new string[] { "@CourseExamID" },
                                                  new object[] { examPractice.ID });
                grdData.DataSource = dtTH;
            }

            //Tìm ID bài thi bài tập ---------- LMK
            CourseExamModel examExcercise = listData.FirstOrDefault(p => p.ExamType == 3);
            if (examExcercise != null && examExcercise.ID > 0)
            {
                DataTable dtBt = TextUtils.LoadDataFromSP("spGetCourseExamPracticeResult", "A",
                                                  new string[] { "@CourseExamID" },
                                                  new object[] { examExcercise.ID });
                grdData_BT.DataSource = dtBt;
            }

        }

        int id = 0;
        int type = 0;
        string fullName = "";
        private void btnNew_Click(object sender, EventArgs e)
        {
            int a = xtraTabControl1.SelectedTabPageIndex;
            if (!checkType()) return;
            int courseID = TextUtils.ToInt(grvCourse.GetFocusedRowCellValue(colID));
            frmCourseExamResultPractice frm = new frmCourseExamResultPractice();
            frm.courseID = courseID;
            frm.type = type;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
                //grvCourse.FocusedRowHandle = rowHandleCourse;
            }
        }
        bool checkType()
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0) return false;
            else if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID_TH));
                fullName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
                type = 2;
            }
            else
            {
                id = TextUtils.ToInt(grvData_BT.GetFocusedRowCellValue(colID_BT));
                fullName = TextUtils.ToString(grvData_BT.GetFocusedRowCellValue(colFullName));
                type = 3;
            }
            if (id < 0)
            {
                return false;
            }
            return true;
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;

            if (!checkType()) return;
            int courseID = TextUtils.ToInt(grvCourse.GetFocusedRowCellValue(colID));
            CourseExamResultModel courseExamResult = SQLHelper<CourseExamResultModel>.FindByID(id);
            frmCourseExamResultPractice frm = new frmCourseExamResultPractice();
            frm.courseID = courseID;
            frm.courseExamResult = courseExamResult;
            frm.type = type;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
                grvData.FocusedRowHandle = rowHandle;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (!checkType()) return;
            if (id <= 0)
            {
                return;
            }
            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá kết quả thi của nhân viên [{fullName}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                CourseExamPracticeBO.Instance.Delete(id);
                loadData();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            int rowHandleCourse = grvCourse.FocusedRowHandle;
            int rowHandleData = grvData.FocusedRowHandle;

            loadCourse();
            loadData();

            grvCourse.FocusedRowHandle = rowHandleCourse;
            grvData.FocusedRowHandle = rowHandleData;
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                XlsExportOptions optionsEx = new XlsExportOptions();
                //optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                //optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                grvData.OptionsPrint.PrintSelectedRowsOnly = false;
                try
                {
                    string code = TextUtils.ToString(grvCourse.GetFocusedRowCellValue(colCode));
                    string filepath = "";

                    if (xtraTabControl1.SelectedTabPageIndex == 0)
                    {
                        filepath = $"{f.SelectedPath}/DanhSachKetQuaThiTracNghiem_{code}.xls";
                        grvData_TN.ExportToXls(filepath, optionsEx);
                    }

                    else if (xtraTabControl1.SelectedTabPageIndex == 1)
                    {
                        filepath = $"{f.SelectedPath}/DanhSachKetQuaThiThucHanh_{code}.xls";
                        grvData.ExportToXls(filepath, optionsEx);
                    }
                    else
                    {
                        filepath = $"{f.SelectedPath}/DanhSachKetQuaThiBaiTap_{code}.xls";
                        grvData_BT.ExportToXls(filepath, optionsEx);
                    }

                    Process.Start(filepath);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo");
                }
                grvData.ClearSelection();
                grvData_TN.ClearSelection();
                grvData_BT.ClearSelection();
            }
        }

        private void grvCourse_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //loadCourseExamResult();
            groupControl1.Text = "KẾT QUẢ THI KHÓA HỌC - " + TextUtils.ToString(grvCourse.GetFocusedRowCellValue(colCode));
            //========================== lee min khooi update 26/09/2024 ==========================
            LoadLesson();
            loadData();
        }

        private void grvData_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                int status = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, "Status"));
                if (e.Column == colStatus_TH)
                {
                    if (status == 3)
                    {
                        e.Appearance.BackColor = Color.OrangeRed;
                        e.Appearance.ForeColor = Color.White;
                    }
                    else if (status == 1)
                    {
                        e.Appearance.BackColor = Color.Yellow;
                        e.Appearance.ForeColor = Color.Black;
                    }
                    else if (status == 2)
                    {
                        e.Appearance.BackColor = Color.Green;
                        e.Appearance.ForeColor = Color.White;
                    }
                }
            }
        }
        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            frmPracticeDetails frm = new frmPracticeDetails();
            int examResultID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID_TH));
            frm.examResult = SQLHelper<CourseExamResultModel>.FindByID(examResultID);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();

                grvData.FocusedRowHandle = rowHandle;
            }
        }
        private void grvBT_DoubleClick(object sender, EventArgs e)
        {
            int rowHandle = grvData_BT.FocusedRowHandle;
            frmPracticeDetails frm = new frmPracticeDetails();
            int examResultID = TextUtils.ToInt(grvData_BT.GetFocusedRowCellValue(colID_BT));
            frm.examResult = SQLHelper<CourseExamResultModel>.FindByID(examResultID);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();

                grvData.FocusedRowHandle = rowHandle;
            }
        }
        List<int> lstID = new List<int>();
        void setEvaluate(bool Evaluate)
        {
            ArrayList arrIndex = new ArrayList();
            if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                if (grvData.DataRowCount <= 0) return;
                arrIndex.AddRange(grvData.GetSelectedRows());
                foreach (int index in arrIndex)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(index, colID_TH));
                    lstID.Add(id);
                }
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 2)
            {
                if (grvData_BT.DataRowCount <= 0) return;
                arrIndex.AddRange(grvData_BT.GetSelectedRows());
                foreach (int index in arrIndex)
                {
                    int id = TextUtils.ToInt(grvData_BT.GetRowCellValue(index, colID_TH));
                    lstID.Add(id);
                }
            }
            string status = string.Empty;
            if (Evaluate)
                status = "đạt";
            else
                status = "không đạt";

            if (MessageBox.Show($"Bạn có chắc muốn đánh giá tất cả nhưng người được chọn là {status} hay không", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                string lst_id = string.Join(";", lstID);
                TextUtils.ExcuteProcedure("spUpdateEvaluate", new string[] { "@LstID", "@Evaluate" }, new object[] { lst_id, Evaluate });
                loadData();
            }
            arrIndex.Clear();
            lstID.Clear();
        }

        private void btnDat_Click(object sender, EventArgs e)
        {
            setEvaluate(true);
        }

        private void btnKhongDat_Click(object sender, EventArgs e)
        {
            setEvaluate(false);
        }

        private void btnHistoryExam_Click(object sender, EventArgs e)
        {
            int courseID = TextUtils.ToInt(grvCourse.GetFocusedRowCellValue(colID));
            int courseExamResultID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIDDetail));
            int employeeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
            int courseExamID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colCourseExamId));

            frmHistoryExam frm = new frmHistoryExam();
            frm.employeeID = employeeID;
            frm.courseID = courseID;
            frm.courseExamResultID = courseExamResultID;
            frm.courseExamID = courseExamID;
            frm.Show();
        }

        private void grvData_TN_DoubleClick(object sender, EventArgs e)
        {
            //return;
            //====================lee min khooi update 22/10/2024=============
            int courseID = TextUtils.ToInt(grvCourse.GetFocusedRowCellValue(colID));
            List<CourseExamModel> listData = SQLHelper<CourseExamModel>.FindByAttribute("CourseId", courseID).ToList();
            // tìm ID bài thi trác nghiệm
            CourseExamModel courseExam = listData.FirstOrDefault(p => p.ExamType == 1);
            ///======================End udpate=================================================

            int courseExamDetailID = Lib.ToInt(grvData_TN.GetFocusedRowCellValue(colIDDetail));
            if (courseExamDetailID == 0) return;
            int courseResultID = Lib.ToInt(grvData_TN.GetFocusedRowCellValue(colIDDetail));
            int employeeID = Lib.ToInt(grvData_TN.GetFocusedRowCellValue(colEmployeeID));
            string examCode = Lib.ToString(grvData_TN.GetFocusedRowCellValue(colCodeExamResult));
            int testTime = Lib.ToInt(grvData_TN.GetFocusedRowCellValue(colTestTime));
            frmCourseExamPracticeDetail frm = new frmCourseExamPracticeDetail();
            frm.courseID = courseID;
            frm.courseResultID = courseResultID;
            frm.employeeID = employeeID;
            frm.examCode = examCode;
            frm.testTime = testTime;
            frm.courseExamID = courseExam.ID;
            frm.Show();
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            loadCourse();
            loadData();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadCourse();
            loadData();
        }

        private void grvData_BT_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                int status = TextUtils.ToInt(grvData_BT.GetRowCellValue(e.RowHandle, "Status"));
                if (e.Column == colStatus_BT)
                {
                    if (status == 3)
                    {
                        e.Appearance.BackColor = Color.OrangeRed;
                        e.Appearance.ForeColor = Color.White;
                    }
                    else if (status == 1)
                    {
                        e.Appearance.BackColor = Color.Yellow;
                        e.Appearance.ForeColor = Color.Black;
                    }
                    else if (status == 2)
                    {
                        e.Appearance.BackColor = Color.Green;
                        e.Appearance.ForeColor = Color.White;
                    }
                }
            }
        }

        private void grvData_TN_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            var view = sender as GridView;
            if (view.FocusedRowHandle == e.RowHandle)
            {
                e.Appearance.BackColor = Color.LightYellow;
                e.Appearance.TextOptions.HAlignment = e.Appearance.TextOptions.HAlignment;
                //e.HighPriority = true;
            }
        }

        private void grvData_TN_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                int status = TextUtils.ToInt(grvData_TN.GetRowCellValue(e.RowHandle, colStatus));
                if (e.Column == colStatusText)
                {
                    if (status == 0)
                    {
                        e.Appearance.BackColor = Color.OrangeRed;
                        e.Appearance.ForeColor = Color.White;
                    }
                    else if (status == 1)
                    {
                        e.Appearance.BackColor = Color.Green;
                        e.Appearance.ForeColor = Color.White;
                    }
                }
            }
        }
        //====================================== lee min khooi update 26/09/2024 ====================================================
        private void LoadLesson()
        {
            grdLesoon.DataSource = null;
            int courseID = TextUtils.ToInt(grvCourse.GetFocusedRowCellValue(colID));
            List<CourseLessonModel> lst = SQLHelper<CourseLessonModel>.FindByAttribute("CourseID", courseID);
            grdLesoon.DataSource = lst;
            LoadLessonExam();
        }
        private void LoadLessonExam()
        {

            grdDataLesson_TN.DataSource = null;
            grdDataLesson_TH.DataSource = null;
            grdDataLesson_BT.DataSource = null;

            int lessonID = TextUtils.ToInt(grvLesson.GetFocusedRowCellValue(colLessonID));
            if (lessonID <= 0) return;

            List<CourseExamModel> listData = SQLHelper<CourseExamModel>.FindByAttribute("LessonID", lessonID);
            LessonExamCheck(listData);
            CourseExamModel examResult = listData.FirstOrDefault(p => p.ExamType == 1);
            if (examResult != null && examResult.ID > 0)
            {
                int employeeID = TextUtils.ToInt(cboEmployee.EditValue);
                DataTable dtTN = TextUtils.LoadDataFromSP("spGetCourseExamResult", "A",
                                                   new string[] { "@CourseExamID", "@EmployeeID", "@OrderNumber" },
                                                   new object[] { examResult.ID, employeeID, 1 });

                grdDataLesson_TN.DataSource = dtTN;
            }

            CourseExamModel examPractice = listData.FirstOrDefault(p => p.ExamType == 2);
            if (examPractice != null && examPractice.ID > 0)
            {
                DataTable dtTH = TextUtils.LoadDataFromSP("spGetCourseExamPracticeResult", "A",
                                                  new string[] { "@CourseExamID" },
                                                  new object[] { examPractice.ID });
                grdDataLesson_TH.DataSource = dtTH;
            }

            CourseExamModel examExcercise = listData.FirstOrDefault(p => p.ExamType == 3);
            if (examExcercise != null && examExcercise.ID > 0)
            {
                DataTable dtBt = TextUtils.LoadDataFromSP("spGetCourseExamPracticeResult", "A",
                                                  new string[] { "@CourseExamID" },
                                                  new object[] { examExcercise.ID });
                grdDataLesson_BT.DataSource = dtBt;
            }
        }
        private void LessonExamCheck(List<CourseExamModel> lstExam)
        {
            xtraTabPage4.PageVisible = lstExam.Any(p => p.ExamType == 1);
            xtraTabPage5.PageVisible = lstExam.Any(p => p.ExamType == 2);
            xtraTabPage6.PageVisible = lstExam.Any(p => p.ExamType == 3);
        }

        private void grvLesson_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            groupControl2.Text = "KẾT QUẢ THI BÀI HỌC - " + TextUtils.ToString(grvLesson.GetFocusedRowCellValue(colLessonCode));
            LoadLessonExam();
        }

        private void grvDataLesson_TN_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                int status = TextUtils.ToInt(grvDataLesson_TN.GetRowCellValue(e.RowHandle, "Status"));
                if (e.Column == gridColumn21)
                {
                    if (status == 0)
                    {
                        e.Appearance.BackColor = Color.OrangeRed;
                        e.Appearance.ForeColor = Color.White;
                    }
                    else if (status == 1)
                    {
                        e.Appearance.BackColor = Color.Green;
                        e.Appearance.ForeColor = Color.White;
                    }
                }
            }
        }

        private void grvDataLesson_TH_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                int status = TextUtils.ToInt(grvDataLesson_TH.GetRowCellValue(e.RowHandle, "Status"));
                if (e.Column == gridColumn33)
                {
                    if (status == 3)
                    {
                        e.Appearance.BackColor = Color.OrangeRed;
                        e.Appearance.ForeColor = Color.White;
                    }
                    else if (status == 1)
                    {
                        e.Appearance.BackColor = Color.Yellow;
                        e.Appearance.ForeColor = Color.Black;
                    }
                    else if (status == 2)
                    {
                        e.Appearance.BackColor = Color.Green;
                        e.Appearance.ForeColor = Color.White;
                    }
                }
            }
        }

        private void grvDataLesson_BT_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                int status = TextUtils.ToInt(grvDataLesson_BT.GetRowCellValue(e.RowHandle, "Status"));
                if (e.Column == gridColumn47)
                {
                    if (status == 3)
                    {
                        e.Appearance.BackColor = Color.OrangeRed;
                        e.Appearance.ForeColor = Color.White;
                    }
                    else if (status == 1)
                    {
                        e.Appearance.BackColor = Color.Yellow;
                        e.Appearance.ForeColor = Color.Black;
                    }
                    else if (status == 2)
                    {
                        e.Appearance.BackColor = Color.Green;
                        e.Appearance.ForeColor = Color.White;
                    }
                }
            }
        }

        private void grvDataLesson_TH_DoubleClick(object sender, EventArgs e)
        {
            int rowHandle = grvDataLesson_TH.FocusedRowHandle;
            frmPracticeDetails frm = new frmPracticeDetails();
            int examResultID = TextUtils.ToInt(grvDataLesson_TH.GetFocusedRowCellValue("ID"));
            frm.examResult = SQLHelper<CourseExamResultModel>.FindByID(examResultID);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadLessonExam();
                grvDataLesson_TH.FocusedRowHandle = rowHandle;
            }
        }

        private void grvDataLesson_BT_DoubleClick(object sender, EventArgs e)
        {
            int rowHandle = grvDataLesson_BT.FocusedRowHandle;
            frmPracticeDetails frm = new frmPracticeDetails();
            int examResultID = TextUtils.ToInt(grvDataLesson_BT.GetFocusedRowCellValue("ID"));
            frm.examResult = SQLHelper<CourseExamResultModel>.FindByID(examResultID);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadLessonExam();

                grvDataLesson_BT.FocusedRowHandle = rowHandle;
            }
        }

        private void grvDataLesson_TN_DoubleClick(object sender, EventArgs e)
        {

            int courseExamDetailID = Lib.ToInt(grvDataLesson_TN.GetFocusedRowCellValue("ID"));
            if (courseExamDetailID == 0) return;


            int employeeID = Lib.ToInt(grvDataLesson_TN.GetFocusedRowCellValue("EmployeeId"));
            string examCode = Lib.ToString(grvDataLesson_TN.GetFocusedRowCellValue("CodeExam"));
            int testTime = Lib.ToInt(grvDataLesson_TN.GetFocusedRowCellValue("TestTime"));

            int lessonID = TextUtils.ToInt(grvLesson.GetFocusedRowCellValue(colLessonID));
            List<CourseExamModel> listData = SQLHelper<CourseExamModel>.FindByAttribute("LessonID", lessonID);
            CourseExamModel examResult = listData.FirstOrDefault(p => p.ExamType == 1);

            frmCourseExamPracticeDetail frm = new frmCourseExamPracticeDetail();
            frm.courseID = 0;
            frm.courseResultID = courseExamDetailID;
            frm.employeeID = employeeID;
            frm.examCode = examCode;
            frm.testTime = testTime;
            frm.courseExamID = examResult != null ? examResult.ID : 0;
            frm.Show();
        }
    }
}
