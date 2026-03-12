using BMS.Business;
using BMS.Model;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmAddQuestion : _Forms
    {
        public int examID;
        public int quesID;
        public int examType;

        ArrayList listIdDelete = new ArrayList();
        string ImagePathSave;
        //string path = @"\\192.168.1.2\ftp\Upload\Images\Courses\";
        string path = @"\\113.190.234.64\Software\ftp\Upload\Images\Courses\";
        string fileCopy;
        public frmAddQuestion()
        {
            InitializeComponent();
        }

        private void frmAddQuestion_Load(object sender, EventArgs e)
        {
            if (examType != 1)
            {
                groupControl2.Dock = DockStyle.None;
                groupControl2.Visible = false;
            }    
            else
            {
                groupControl2.Dock = DockStyle.Bottom;
                groupControl2.Visible = true;
            }          
            loadData();
        }
        void loadData()
        {
            txtSTT.Value = loadSTT();

            if (quesID > 0)
            {
                CourseQuestionModel question = SQLHelper<CourseQuestionModel>.SqlToModel($"SELECT * FROM CourseQuestion WHERE ID = {quesID}");
                txtQuestionText.Text = question.QuestionText;
                txtSTT.Value = question.STT;
                loadImage();
            }
            loadAnswer();
            
        }

        void loadAnswer()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetCourseAnswers", "A", new string[] { "@QuestionID" }, new object[] { quesID });
            grdData.DataSource = dt;
        }
        int loadSTT()
        {
            int STT = TextUtils.ToInt(TextUtils.ExcuteScalar($"SELECT TOP 1 STT FROM CourseQuestion WHERE CourseExamId = {examID} ORDER BY STT DESC"));
            return STT + 1;
        }
        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            if (!saveData()) return;
            this.DialogResult = DialogResult.OK;
        }

        bool saveData()
        {
            grvData.CloseEditor();

            if (!validate()) return false;

            CourseQuestionModel question = new CourseQuestionModel();
            if (quesID > 0)
            {
                question = (CourseQuestionModel)CourseQuestionBO.Instance.FindByPK(quesID);
            }

            question.QuestionText = txtQuestionText.Text;
            question.STT = TextUtils.ToInt(txtSTT.Value);
            question.CourseExamId = examID;
            question.CheckInput = 1;
            question.Image = ImagePathSave;

            if (question.ID > 0)
            {
                //question.ID = quesID;
                CourseQuestionBO.Instance.Update(question);
            }
            else
            {
                question.ID = (int)CourseQuestionBO.Instance.Insert(question);
            }

            CourseRightAnswersBO.Instance.DeleteByAttribute("CourseQuestionID", question.ID);

            for (int i = 0; i < grvData.RowCount; i++)
            {
                CourseAnswersModel answer = new CourseAnswersModel();
                answer.AnswerText = TextUtils.ToString(grvData.GetRowCellValue(i, colAnswerText));
                answer.CourseQuestionId = question.ID;
                answer.AnswerNumber = i + 1;
                var ansID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                if (ansID > 0)
                {
                    answer.ID = ansID;
                    CourseAnswersBO.Instance.Update(answer);
                }
                else
                {
                    answer.ID = (int)CourseAnswersBO.Instance.Insert(answer);
                }

                bool rightAnswer = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colRightAnswer));
                if (rightAnswer)
                {
                    CourseRightAnswersModel rightAns = new CourseRightAnswersModel();
                    rightAns.CourseQuestionID = question.ID;
                    rightAns.CourseAnswerID = answer.ID;
                    CourseRightAnswersBO.Instance.Insert(rightAns);

                }
                if (grvData.RowCount == 1)
                {
                    question.CheckInput = 1;
                }
                else
                {
                    question.CheckInput = 1;
                }
            }

            if (!string.IsNullOrEmpty(fileCopy))
            {
                UploadFile(fileCopy);
            }
            if (listIdDelete.Count > 0)
                CourseAnswersBO.Instance.Delete(listIdDelete);
            return true;
        }
        bool check;
        bool validate()
        {
            if (string.IsNullOrEmpty(txtQuestionText.Text))
            {
                MessageBox.Show("Vui lòng nhập Nội dung câu hỏi !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            
            if (examType == 1)
            {
                if (grvData.RowCount <= 0)
                {
                    MessageBox.Show("Vui lòng nhập Nội dung đáp án!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
                else if (grvData.RowCount > 4)
                {
                    MessageBox.Show("Số lượng đáp án không được lớn hơn 4!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
                else
                {
                    check = false;
                    for (int i = 0; i < grvData.RowCount; i++)
                    {
                        string code = TextUtils.ToString(grvData.GetRowCellValue(i, colCode));
                        string content = TextUtils.ToString(grvData.GetRowCellValue(i, colAnswerText)).Trim();
                        bool rightAnswer = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colRightAnswer));
                        if (string.IsNullOrEmpty(content))
                        {
                            MessageBox.Show($"Vui lòng nhập nội dung đáp án [{code}]!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            grvData.FocusedRowHandle = i;
                            grvData.FocusedColumn = colAnswerText;
                            return false;
                        }
                        if (rightAnswer)
                            check = rightAnswer;
                    }
                    if (!check)
                    {
                        MessageBox.Show($"Vui lòng chọn ít nhất một đáp án đúng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }

                }
                
            }

            
            return true;
        }



        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo info = grvData.CalcHitInfo(new Point(e.X, e.Y));
            if (info.Column == colAnswerNumber && e.Y < 40)
            {
                if (grvData.RowCount >= 4)
                {
                    MessageBox.Show("Số lượng đáp án không được lớn hơn 4!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                int STT;
                string code = "";
                DataTable dt = (DataTable)grdData.DataSource;
                if (dt.Rows.Count == 0)
                {
                    STT = 1;
                    code = "A";
                }
                else
                {
                    STT = TextUtils.ToInt(grvData.GetRowCellValue(dt.Rows.Count - 1, "AnswerNumber")) + 1;
                    if (STT == 2)
                    {
                        code = "B";
                    }
                    else if (STT == 3)
                    {
                        code = "C";
                    }
                    else if (STT == 4)
                    {
                        code = "D";
                    }
                }
                DataRow dtrow = dt.NewRow();
                dtrow["AnswerNumber"] = STT;
                dtrow["Code"] = code;
                dt.Rows.Add(dtrow);
                grdData.DataSource = dt;
                //grvData.SetRowCellValue(STT - 1, colCode, code);


                //grvData.FocusedColumn = colAnswerText;
                //grvData.FocusedRowHandle = STT - 1;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string answercode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));

            if (MessageBox.Show(string.Format($"Bạn có chắc chắn muốn xóa đáp án [{answercode}] không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvData.DeleteSelectedRows();
                listIdDelete.Add(ID);

                for (int i = 0; i < grvData.RowCount; i++)
                {
                    string code = "";
                    switch ((i + 1))
                    {
                        case 1:
                            code = "A";
                            break;
                        case 2:
                            code = "B";
                            break;
                        case 3:
                            code = "C";
                            break;
                        case 4:
                            code = "D";
                            break;
                        default:
                            break;
                    }
                    grvData.SetRowCellValue(i, colAnswerNumber, i + 1);
                    grvData.SetRowCellValue(i, colCode, code);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                txtSTT.Value = loadSTT();
                txtQuestionText.Text = "";
                //for (int i = grvData.RowCount - 1; i >= 0; i--)
                //{
                //    grvData.DeleteRow(i);
                //}
                quesID = 0;
                loadAnswer();

                PbImage.Image = null;
                //fileCopy = "";
                //if (!string.IsNullOrEmpty(fileCopy))
                //{
                //    UploadFile(fileCopy);
                //}
            }


        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Pictures files |*.png;*.jpge;*.jpeg;*.jpg;*.tiff;*.nef;*.ai;*.jfif;";

            openFile.RestoreDirectory = true;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string path = System.IO.Path.GetDirectoryName(openFile.FileName);
                    string name = System.IO.Path.GetFileNameWithoutExtension(openFile.FileName);
                    string extension = System.IO.Path.GetExtension(openFile.FileName);

                    string newfileImage = name + "_" + DateTime.Now.ToString("ddMMyyHHmmss") + extension;

                    System.IO.File.Copy(openFile.FileName, path + "\\" + newfileImage);

                    // System.IO.File.Move();

                    ImagePathSave = newfileImage;
                    fileCopy = path + "\\" + newfileImage;

                    PbImage.Image = Image.FromFile($"{openFile.FileName}");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void loadImage()
        {
            try
            {
                CourseQuestionModel question = (CourseQuestionModel)CourseQuestionBO.Instance.FindByPK(quesID);
                var request = WebRequest.Create("http://192.168.1.2:8083/api/Upload/Images/Courses/" + question.Image);
                var response = request.GetResponse();
                var stream = response.GetResponseStream();
                PbImage.Image = Image.FromStream(stream);


                //string request = @"D:\ThisPC\Hình ảnh\anhdownload\UPLOAD\" + question.ImageName;
                //PbImage.Image = Image.FromFile(request);
                ImagePathSave = question.Image;
            }
            catch (Exception)
            {

                return;
            }
        }
        void UploadFile(string fileName)
        {
            try
            {
                string API_UPLOAD = $"http://192.168.1.2:8083/api/Home/uploadfile?path={path}";
                var client = new WebClient();
                client.Headers.Add("Content-Type", "binary/octet-stream");
                client.UploadFileAsync(new Uri(API_UPLOAD), fileName);
                client.UploadFileCompleted += Client_UploadFileCompleted;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Client_UploadFileCompleted(object sender, UploadFileCompletedEventArgs e)
        {
            try
            {
                System.IO.File.Delete(fileCopy);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmAddQuestion_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}