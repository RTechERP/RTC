using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
using ExcelDataReader;
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
    public partial class frmInputExcelCourseQuestion : _Forms
    {
        DataSet ds;
        private DateTime start;
        public int examID;
        //int quantity;
        //int stt;
        public frmInputExcelCourseQuestion()
        {
            InitializeComponent();
        }

        private void frmInputExcelCourseQuestion_Load(object sender, EventArgs e)
        {
            //stt = TextUtils.ToInt(TextUtils.ExcuteScalar($"SELECT TOP 1 STT FROM CourseQuestion WHERE CourseExamId = {examID} ORDER BY STT DESC"));
        }

        private void btnMauExcel_Click(object sender, EventArgs e)
        {
            FileInfo fi = new FileInfo("MauCauHoi.xlsx");
            if (fi.Exists)
            {
                System.Diagnostics.Process.Start("MauCauHoi.xlsx");
            }
            else
            {
                MessageBox.Show("file doesn't exist", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
        }
        private void btnBrowse_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            var result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                btnBrowse.Text = ofd.FileName;
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
            try
            {
                var stream = new FileStream(btnBrowse.Text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                var sw = new Stopwatch();
                sw.Start();

                IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);

                var openTiming = sw.ElapsedMilliseconds;

                ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    UseColumnDataType = false,
                    ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = false
                    }
                });

                var tablenames = GetTablenames(ds.Tables);

                cboSheet.DataSource = tablenames;

                if (tablenames.Count > 0)
                    cboSheet.SelectedIndex = 0;
                btnSave.Enabled = true;
                cboSheet_SelectionChangeCommitted(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        private static IList<string> GetTablenames(DataTableCollection tables)
        {
            var tableList = new List<string>();
            foreach (var table in tables)
            {
                tableList.Add(table.ToString());
            }

            return tableList;
        }
        private void cboSheet_SelectionChangeCommitted(object sender, EventArgs e)
        {
            grdData.DataSource = null;
            try
            {
                var tablename = cboSheet.SelectedItem.ToString();
                grdData.DataSource = ds;
                grdData.DataMember = tablename;

            }
            catch (Exception ex)
            {
                TextUtils.ShowError(ex);
                grdData.DataSource = null;
            }
            if (grdData.DataSource == null)
            {
                try
                {
                    DataTable dt = TextUtils.ExcelToDatatableNoHeader(btnBrowse.Text, cboSheet.SelectedValue.ToString());

                    grdData.DataSource = dt;
                    grvData.PopulateColumns();
                    grvData.BestFitColumns();
                    grvData.Focus();
                    grvData.OptionsBehavior.Editable = false;
                    grvData.OptionsBehavior.ReadOnly = true;
                }
                catch (Exception ex)
                {
                    TextUtils.ShowError(ex);
                    grdData.DataSource = null;
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
            else
            {
                progressBar1.Minimum = 1;
                progressBar1.Maximum = grvData.RowCount;
                start = DateTime.Now;
                enableControl(false);
                backgroundWorker1.RunWorkerAsync();
            }
        }
       
        //private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    int rowCount = grvData.RowCount;
        //    int questionId = 0;
        //    int sttAnswer = 0;

        //    //Xoá danh sách câu hỏi
        //    //TextUtils.ExcuteProcedure("spDeleteCourseQuestionByCourseExamID", new string[] { "@CourseExamID" }, new object[] { examID });

        //    for (int i = 1; i < rowCount; i++)
        //    {
        //        try
        //        {
        //            progressBar1.Invoke((Action)(() => { progressBar1.Value = i + 1; }));

        //            int stt = TextUtils.ToInt(grvData.GetRowCellValue(i, "F1"));
        //            if (stt > 0)
        //            {
        //                CourseQuestionModel questionModel = new CourseQuestionModel();
        //                questionModel.CourseExamId = examID;
        //                questionModel.STT = stt;
        //                questionModel.QuestionText = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));

        //                questionId = (int)CourseQuestionBO.Instance.Insert(questionModel);

        //                sttAnswer = 0;
        //            }

        //            string answerContent = TextUtils.ToString(grvData.GetRowCellValue(i, "F3")).Trim();
        //            if (!string.IsNullOrEmpty(answerContent))
        //            {
        //                sttAnswer++;
        //                CourseAnswersModel answerModel = new CourseAnswersModel();
        //                answerModel.AnswerText = answerContent;
        //                answerModel.AnswerNumber = sttAnswer;
        //                answerModel.CourseQuestionId = questionId;
        //                int answerId = (int)CourseAnswersBO.Instance.Insert(answerModel);

        //                string rightAnswer = TextUtils.ToString(grvData.GetRowCellValue(i, "F4")).Trim();
        //                if (!string.IsNullOrEmpty(rightAnswer))
        //                {
        //                    CourseRightAnswersModel rightAnswersModel = new CourseRightAnswersModel();
        //                    rightAnswersModel.CourseAnswerID = answerId;
        //                    rightAnswersModel.CourseQuestionID = questionId;
        //                    CourseRightAnswersBO.Instance.Insert(rightAnswersModel);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(i.ToString() + Environment.NewLine + ex.ToString());
        //        }
        //    }
        //}

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int rowCount = grvData.RowCount;
            int questionId = 0;
            int answerId = 0;
            int sttAnswer = 0;

            //Xoá danh sách câu hỏi
            //TextUtils.ExcuteProcedure("spDeleteCourseQuestionByCourseExamID", new string[] { "@CourseExamID" }, new object[] { examID });

            for (int i = 1; i < rowCount; i++)
            {
                try
                {
                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i + 1; }));

                    int stt = TextUtils.ToInt(grvData.GetRowCellValue(i, "F1"));
                    if (stt > 0)
                    {
                        var exp1 = new Expression("STT", stt);
                        var exp2 = new Expression("CourseExamId", examID);

                        //var existQuestionModel = SQLHelper<CourseQuestionModel>.FindAll().FirstOrDefault(p => p.STT == stt && p.CourseExamId == examID);
                        var existQuestionModel = SQLHelper<CourseQuestionModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
                        if (existQuestionModel != null)
                        {
                            existQuestionModel.QuestionText = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));
                            CourseQuestionBO.Instance.Update(existQuestionModel);

                            questionId = existQuestionModel.ID;
                        }
                        else
                        {
                            CourseQuestionModel questionModel = new CourseQuestionModel();
                            questionModel.CourseExamId = examID;
                            questionModel.STT = stt;
                            questionModel.QuestionText = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));

                            questionId = (int)CourseQuestionBO.Instance.Insert(questionModel);
                        }

                        sttAnswer = 0;
                    }

                    string answerContent = TextUtils.ToString(grvData.GetRowCellValue(i, "F3")).Trim();
                    if (!string.IsNullOrEmpty(answerContent))
                    {
                        sttAnswer++;

                        var exp1 = new Expression("AnswerNumber", sttAnswer);
                        var exp2 = new Expression("CourseQuestionId", questionId);

                        //var existAnswerModel = SQLHelper<CourseAnswersModel>.FindAll().FirstOrDefault(p => p.AnswerNumber == sttAnswer && p.CourseQuestionId == questionId);
                        var existAnswerModel = SQLHelper<CourseAnswersModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
                        if (existAnswerModel != null)
                        {
                            existAnswerModel.AnswerText = answerContent;
                            CourseAnswersBO.Instance.Update(existAnswerModel);

                            answerId = existAnswerModel.ID;
                        }
                        else
                        {
                            CourseAnswersModel answerModel = new CourseAnswersModel();
                            answerModel.AnswerText = answerContent;
                            answerModel.AnswerNumber = sttAnswer;
                            answerModel.CourseQuestionId = questionId;
                            answerId = (int)CourseAnswersBO.Instance.Insert(answerModel);
                        }

                        //var existCourseRightAnswersModel = SQLHelper<CourseRightAnswersModel>.FindAll().FirstOrDefault(p => p.CourseAnswerID == answerId);
                        //var existCourseRightAnswersModel = SQLHelper<CourseRightAnswersModel>.FindByAttribute("CourseAnswerID", answerId).FirstOrDefault();
                        //if (existCourseRightAnswersModel != null)
                        //{
                        //    CourseRightAnswersBO.Instance.DeleteByAttribute("CourseAnswerID", answerId);
                        //}
                        CourseRightAnswersBO.Instance.DeleteByAttribute("CourseAnswerID", answerId);

                        int rightAnswer = TextUtils.ToInt(grvData.GetRowCellValue(i, "F4"));
                        if (rightAnswer == 1)
                        {
                            CourseRightAnswersModel rightAnswersModel = new CourseRightAnswersModel();
                            rightAnswersModel.CourseAnswerID = answerId;
                            rightAnswersModel.CourseQuestionID = questionId;
                            CourseRightAnswersBO.Instance.Insert(rightAnswersModel);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(i.ToString() + Environment.NewLine + ex.ToString());
                }
            }


        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //if (quantity != 0)
            //{
            //}
            MessageBox.Show($"Cập nhật thành công!\n{start.ToString() + " - " + DateTime.Now.ToString()}","Thông báo");
            enableControl(true);
            frmInputExcelCourseQuestion_FormClosed(null, null);
        }
        void enableControl(bool enable)
        {
            btnSave.Enabled = enable;
            grdData.Enabled = enable;
            cboSheet.Enabled = enable;
            btnBrowse.Enabled = enable;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            grvData.DeleteSelectedRows();

        }

     

        private void frmInputExcelCourseQuestion_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}