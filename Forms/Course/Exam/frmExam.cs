using BMS.Business;
using BMS.Model;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmExam : _Forms
    {
        DataSet dataSet = new DataSet();
        public frmExam()
        {
            InitializeComponent();
        }
        private void frmExam_Load(object sender, EventArgs e)
        {
            loadExam();
        }
        void loadExam() //load khoá học
        {
            DataTable dt = TextUtils.GetTable("spGetDataCourse");
            //DataTable dt = TextUtils.GetTable("spGetCourseExam");
            //dt = TextUtils.Select("SELECT * FROM dbo.CourseExam");
            grdData.DataSource = dt;
            //grvData.FocusedRowHandle = 0;

            grvData.ExpandGroupLevel(0);
            grvData.ExpandGroupLevel(1);
        }
        void loadCourseExam() //load đề thi
        {
            int CourseID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //DataTable dt = new DataTable();
            DataTable dt = TextUtils.LoadDataFromSP("spGetCourseExam", "A", new string[] { "@CourseID" }, new object[] { CourseID });
            grdExam.DataSource = dt;

            grvExam.FocusedRowHandle = 0;

            
        }
        void loadQuestion() //load câu hỏi
        {
            //====================== lee min khooi update 18/9/2024 ===============================
            bool isTrueLesson = xtraTabControl1.SelectedTabPage == xtraTabPage1 ? false : true;
            DevExpress.XtraGrid.Views.Grid.GridView grv = isTrueLesson ? grvLesson : grvExam;

            var examID = TextUtils.ToInt(grv.GetFocusedRowCellValue(isTrueLesson ? colLessonExamID : colID));
            //DataTable dt = new DataTable();
            dataSet = TextUtils.LoadDataSetFromSP("spGetCourseQuestion", new string[] { "@ExamID" }, new object[] { examID });
            DataTable dt = dataSet.Tables[0];
            grdQuestion.DataSource = dt;
        }
        void loadRightAnswer() // đáp án trắc nghiệm
        {
            int quesID = TextUtils.ToInt(grvQuestion.GetFocusedRowCellValue(colCourseQuestionID));
            //var dt = TextUtils.Select($"SELECT a.AnswerText,ra.CourseQuestionID FROM dbo.CourseRightAnswers AS ra JOIN dbo.CourseAnswers AS a ON ra.CourseAnswerID = a.ID JOIN dbo.CourseQuestion AS q ON ra.CourseQuestionID = q.ID WHERE ra.CourseQuestionID = {quesID}");
            DataTable dt = TextUtils.LoadDataFromSP("spGetCourseAnswersByCourseQuestionID", "A",
                            new string[] { "@CourseQuestionID" }
                            , new object[] { quesID });

            grdRightAnswer.DataSource = dt;
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            //================== lee min khooi 18/09/2024 ==================================
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));

            bool isTrueLesson = xtraTabControl1.SelectedTabPage == xtraTabPage1 ? false : true;
            if (isTrueLesson)
            {
                int lessonId = TextUtils.ToInt(grvLesson.GetFocusedRowCellValue(colLessonID));
                frmLessonExam frm = new frmLessonExam();
                frm.CourseID = id;
                frm.LessonID = lessonId;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadLessonExam();
                }
            }
            else
            {
                frmAddExam frm = new frmAddExam();
                frm._courseID = id;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    loadCourseExam();
                }
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //========== lee min khooi update 18/9/2024 ==================
            bool isTrueLesson = xtraTabControl1.SelectedTabPage == xtraTabPage1 ? false : true;
            DevExpress.XtraGrid.Views.Grid.GridView grv = isTrueLesson ? grvLesson : grvExam;

            int courseId = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            int rowhandle = grv.FocusedRowHandle;
            int examId = TextUtils.ToInt(grv.GetFocusedRowCellValue("ID"));

            if (examId <= 0) return;
            if (isTrueLesson)
            {
                frmLessonExam frm = new frmLessonExam();
                frm.CourseID = courseId;
                frm.ExamID = examId;
                if (frm.ShowDialog() == DialogResult.OK) LoadLessonExam();
                
            }
            else
            {
                frmAddExam frm = new frmAddExam();
                frm._examID = examId;
                if (frm.ShowDialog() == DialogResult.OK) loadCourseExam(); 
            }
            grv.FocusedRowHandle = rowhandle;

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            //========== lee min khooi update 18/09/2024 ==============
            bool isTrueLesson = xtraTabControl1.SelectedTabPage == xtraTabPage1 ? false : true;
            DevExpress.XtraGrid.Views.Grid.GridView grv = isTrueLesson ? grvLesson : grvExam;

            int id = TextUtils.ToInt(grv.GetFocusedRowCellValue("ID"));
            string nameExam = TextUtils.ToString(grv.GetFocusedRowCellValue("NameExam"));

            if (id <= 0) return;

            int countQuestion = grvQuestion.RowCount;
            if (countQuestion > 0)
            {
                MessageBox.Show($"Không thể xóa Đề thi [{nameExam}].\nVui lòng xóa câu hỏi trước!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                DialogResult result = MessageBox.Show($"Bạn có chắc muốn xóa đề thi {nameExam} !", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {

                    CourseExamBO.Instance.Delete(id);
                }
            }

            btnRefreshExam_Click(null, null);

        }

        private void btnNewQuestion_Click(object sender, EventArgs e)
        {
            //=================== lee min khooi update 18/09/2024 =========================
            bool isTrueLesson = xtraTabControl1.SelectedTabPage == xtraTabPage1 ? false : true;
            DevExpress.XtraGrid.Views.Grid.GridView grv = isTrueLesson ? grvLesson : grvExam;


            int examID = TextUtils.ToInt(grv.GetFocusedRowCellValue("ID"));
            if (examID < 0) return;
            int examType = TextUtils.ToInt(grv.GetFocusedRowCellValue("ExamType"));
            frmAddQuestion frm = new frmAddQuestion();
            frm.examID = examID;
            frm.examType = examType;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadQuestion();
                loadRightAnswer();
            }
        }

        private void btnEditQuestion_Click(object sender, EventArgs e)
        {
            //=================== lee min khooi update 18/09/2024 =========================
            bool isTrueLesson = xtraTabControl1.SelectedTabPage == xtraTabPage1 ? false : true;
            DevExpress.XtraGrid.Views.Grid.GridView grv = isTrueLesson ? grvLesson : grvExam;

            int rowHandle = grvQuestion.FocusedRowHandle;
            if (rowHandle < 0) return;
            int examID = TextUtils.ToInt(grv.GetFocusedRowCellValue("ID"));
            int quesID = TextUtils.ToInt(grvQuestion.GetFocusedRowCellValue(colCourseQuestionID));
            int examType = TextUtils.ToInt(grv.GetFocusedRowCellValue("ExamType"));
            frmAddQuestion frm = new frmAddQuestion();
            frm.examID = examID;
            frm.quesID = quesID;
            frm.examType = examType;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadQuestion();
                loadRightAnswer();

                grvQuestion.FocusedRowHandle = rowHandle;
            }
        }

        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadCourseExam();

            //============== lee min khooi update 18/09/2024 ============  
            LoadLessonExam();
        }

        private void btnDeleteQuestion_Click(object sender, EventArgs e)
        {
            List<int> listID = new List<int>();
            int[] rowSelected = grvQuestion.GetSelectedRows();
            if (rowSelected.Length > 0)
            {
                DialogResult result = MessageBox.Show($"Bạn có chắc muốn xóa danh sách câu hỏi đã chọn không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    foreach (int row in rowSelected)
                    {
                        int courseQuestionID = TextUtils.ToInt(grvQuestion.GetRowCellValue(row, colCourseQuestionID));
                        //var nameQuestion = TextUtils.ToString(grvQuestion.GetRowCellValue(row, colQuestionText));
                        if (courseQuestionID == 0) continue;

                        listID.Add(courseQuestionID);

                    }

                    string id = string.Join(",", listID);
                    string query = $"DELETE dbo.CourseAnswers WHERE CourseQuestionID IN ({id})\n" +
                            $"DELETE dbo.CourseRightAnswers WHERE CourseQuestionID IN ({id})\n" +
                            $"DELETE dbo.CourseQuestion WHERE ID IN ({id})";

                    TextUtils.ExcuteSQL(query);

                }

            }

            btnRefreshQuestion_Click(null, null);
        }

        private void grvQuestion_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadRightAnswer();
        }

        private void grvQuestion_DoubleClick(object sender, EventArgs e)
        {
            btnEditQuestion_Click(null, null);
        }
        private void btnRefreshExam_Click(object sender, EventArgs e)
        {
            loadCourseExam();
            loadQuestion();
            loadRightAnswer();
            LoadLessonExam();
        }

        private void btnRefreshQuestion_Click(object sender, EventArgs e)
        {
            loadQuestion();
            loadRightAnswer();
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {

            var tabSelected = xtraTabControl1.SelectedTabPage;

            if (tabSelected.Controls.Count <= 0) return;
            GridControl gridControl = (GridControl)tabSelected.Controls[0];
            GridView gridView = gridControl.MainView as GridView;
            if (gridView == null) return;
            int rowHandle = gridView.FocusedRowHandle;
            if (rowHandle < 0) return;


            int courseExamID = TextUtils.ToInt(gridView.GetFocusedRowCellValue(colID));
            frmInputExcelCourseQuestion frm = new frmInputExcelCourseQuestion();
            frm.examID = courseExamID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                btnRefreshExam_Click(null, null);
                gridView.FocusedRowHandle = rowHandle;
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void grvExam_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadQuestion();
            loadRightAnswer();

            //================= lee min khooi update 18/09/2024 ==============================
            bool isTrueLesson = xtraTabControl1.SelectedTabPage == xtraTabPage1 ? false : true;
            DevExpress.XtraGrid.Views.Grid.GridView grv = isTrueLesson ? grvLesson : grvExam;
            int examType = TextUtils.ToInt(grv.GetFocusedRowCellValue("ExamType"));



            if (examType <= 0) return;
            grvQuestion.Columns[colDapAnA.FieldName].VisibleIndex = examType == 1 ? 4 : -1;
            grvQuestion.Columns[colDapAnB.FieldName].VisibleIndex = examType == 1 ? 5 : -1;
            grvQuestion.Columns[colDapAnC.FieldName].VisibleIndex = examType == 1 ? 6 : -1;
            grvQuestion.Columns[colDapAnD.FieldName].VisibleIndex = examType == 1 ? 7 : -1;

            groupControl1.Visible = examType == 1;

            splitContainerControl3.PanelVisibility = examType == 1 ? SplitPanelVisibility.Both : SplitPanelVisibility.Panel1;

            //if (courseID != 1) //Thực hành - Bài tập
            //{
            //    //groupControl1.Dock = DockStyle.None;
            //    grvQuestion.Columns[3].Visible = false;
            //    grvQuestion.Columns[4].Visible = false;
            //    grvQuestion.Columns[5].Visible = false;
            //    grvQuestion.Columns[6].Visible = false;

            //    groupControl1.Visible = courseID == 1;
            //}
            //else // Trắc nghiệm
            //{
            //    //groupControl1.Dock = DockStyle.Bottom;
            //    grvQuestion.Columns[3].Visible = true;
            //    grvQuestion.Columns[4].Visible = true;
            //    grvQuestion.Columns[5].Visible = true;
            //    grvQuestion.Columns[6].Visible = true;
            //}
        }

        private void grvExam_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
                {
                    DataTable dtExport = dataSet.Tables[1];
                    grdDataExport.DataSource = dtExport;
                    grdDataExport.Visible = true;
                    //return;
                    string examCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colNameCourse));
                    string filepath = Path.Combine(f.SelectedPath, $"DanhSachCauHoi_{examCode}.xlsx");

                    XlsxExportOptions optionsEx = new XlsxExportOptions();
                    PrintingSystem printingSystem = new PrintingSystem();

                    PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                    printableComponentLink1.Component = grdDataExport;

                    try
                    {
                        CompositeLink compositeLink = new CompositeLink(printingSystem);
                        compositeLink.Links.Add(printableComponentLink1);

                        compositeLink.CreatePageForEachLink();
                        optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;

                        compositeLink.PrintingSystem.SaveDocument(filepath);
                        compositeLink.ExportToXlsx(filepath, optionsEx);
                        Process.Start(filepath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        grdDataExport.Visible = false;
                    }

                }

            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
                {
                    //DataTable dtExport = dataSet.Tables[1];
                    //grdDataExport.DataSource = dtExport;
                    //grdDataExport.Visible = true;
                    //return;
                    string examCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colNameCourse));
                    string filepath = Path.Combine(f.SelectedPath, $"DanhSachCauHoi_{examCode}.xlsx");

                    XlsxExportOptions optionsEx = new XlsxExportOptions();
                    PrintingSystem printingSystem = new PrintingSystem();

                    PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                    printableComponentLink1.Component = grdQuestion;

                    try
                    {
                        CompositeLink compositeLink = new CompositeLink(printingSystem);
                        compositeLink.Links.Add(printableComponentLink1);

                        compositeLink.CreatePageForEachLink();
                        optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;

                        compositeLink.PrintingSystem.SaveDocument(filepath);
                        compositeLink.ExportToXlsx(filepath, optionsEx);
                        Process.Start(filepath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        grdDataExport.Visible = false;
                    }

                }

            }
        }

        private void btnCopyQuestion_Click(object sender, EventArgs e)
        {
            //=================== lee min khooi update 18/09/2024 =========================
            bool isTrueLesson = xtraTabControl1.SelectedTabPage == xtraTabPage1 ? false : true;
            DevExpress.XtraGrid.Views.Grid.GridView grv = isTrueLesson ? grvLesson : grvExam;


            frmCopyQuestions frm = new frmCopyQuestions();
            //frm.examType = 1;
            //frm.courseExamID = TextUtils.ToInt(grvExam.GetFocusedRowCellValue("ID"));
            frm.cboCourseExam.EditValue = TextUtils.ToInt(grv.GetFocusedRowCellValue("ID"));
            frm.cboExamType.SelectedIndex = TextUtils.ToInt(grv.GetFocusedRowCellValue("ExamType"));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                btnRefreshQuestion_Click(null, null);
            }
        }


        //======================== lee min khooi update 18/09/2024 ==============================================

        private void LoadLessonExam()
        {
            int courseId = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            DataTable dt = TextUtils.LoadDataFromSP("spGetLessonExamByLessonID", "lmkTable",
                                                    new string[] { "@CourseID" },
                                                    new object[] { courseId });
            grdLesson.DataSource = dt;
        }
        private void btnAddLessonExam_Click(object sender, EventArgs e)
        {
            frmLessonExam frm = new frmLessonExam();
            frm.ShowDialog();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            grvExam_FocusedRowChanged(null, null);
        }

        private DevExpress.XtraGrid.Views.Grid.GridView GetDataSelectedTab()
        {
            bool isTrueLesson = xtraTabControl1.SelectedTabPage == xtraTabPage1 ? false : true;
            DevExpress.XtraGrid.Views.Grid.GridView grv = isTrueLesson ? grvLesson : grvExam;
            return grv;
        }
    }
}