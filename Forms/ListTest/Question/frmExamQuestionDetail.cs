using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class frmExamQuestionDetail : _Forms
    {
        public int TypeID;
        public ExamQuestionModel question = new ExamQuestionModel();
        DataTable data;//= new DataTable();

        string ImagePathSave;

        string fileCopy;
        public frmExamQuestionDetail()
        {
            InitializeComponent();
        }
        private void frmExamQuestionDetail_Load(object sender, EventArgs e)
        {
            loadType();
            loadForm();
        }

        void loadType()
        {
            searchLookUpEdit1View.Columns.ColumnByFieldName("GroupName").Group();
            searchLookUpEdit1View.CollapseAllDetails();
            data = TextUtils.Select("SELECT  et.*,eg.GroupName,eg.ID AS GroupID FROM ExamQuestionType et LEFT JOIN ExamQuestionGroup eg ON eg.ID = et.ExamQuestionGroupID ");
            cboType.Properties.DataSource = data;
            cboType.Properties.DisplayMember = "TypeName";
            cboType.Properties.ValueMember = "ID";

        }

        void loadForm()
        {
            if (question.ID > 0)
            {
                //cboType.EditValue = question.ExamQuestionTypeID;
                //txtSTT.EditValue = question.STT;
                //string s = question.ContentTest.Trim().Replace("\r\n", " ");
                //int sA = s.LastIndexOf("A."); string sAs = s.Substring(0, sA);
                //int sB = s.LastIndexOf("B."); string sBs = s.Substring(sA, sB - sA);
                //int sC = s.LastIndexOf("C."); string sCs = s.Substring(sB, sC - sB);
                //int sD = s.LastIndexOf("D."); string sDs = s.Substring(sC, sD - sC);
                //string sEs = s.Substring(sD, s.Length - sD);

                //txtContentQuestions.Text = sAs + Environment.NewLine + sBs + Environment.NewLine + sCs + Environment.NewLine + sDs + Environment.NewLine + sEs;
                //txtScore.Value = TextUtils.ToDecimal(question.Score);
                cboType.EditValue = question.ExamQuestionTypeID;
                txtSTT.EditValue = question.STT;
                txtContentQuestions.Text = question.ContentTest.Replace("\r","").Replace("\n", "\r\n");
                txtScore.Value = TextUtils.ToDecimal(question.Score);

                for (int i = 0; i < question.CorrectAnswer.Length; i++)
                {
                    if (question.CorrectAnswer[i].ToString().ToLower() == "a")
                    {
                        ceA.Checked = true;
                    }
                    else if (question.CorrectAnswer[i].ToString().ToLower() == "b")
                    {
                        ceB.Checked = true;
                    }
                    else if (question.CorrectAnswer[i].ToString().ToLower() == "c")
                    {
                        ceC.Checked = true;
                    }
                    else if (question.CorrectAnswer[i].ToString().ToLower() == "d")
                    {
                        ceD.Checked = true;
                    }
                }

                loadImage();
            }
            else cboType.EditValue = TypeID;
        }
        bool ValidateForm()
        {
            if (cboType.Text.Trim() == "")
            {
                MessageBox.Show(string.Format("Vui lòng chọn bài kiểm tra!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (txtSTT.Text.Trim() == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập STT của câu hỏi! "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }


            //DataRow[] row = data.Select($"ID={cboType.EditValue}");

            //int ExamQuestionGroupID =TextUtils.ToInt(row[0]["GroupID"]);

            //DataTable data1 = TextUtils.Select($"SELECT TOP 1 eq.ID FROM ExamQuestion eq LEFT JOIN dbo.ExamQuestionType et ON et.ID = eq.ExamQuestionTypeID WHERE eq.STT = {txtSTT.Text.Trim()} AND et.ExamQuestionGroupID = {ExamQuestionGroupID} AND eq.ID <> " + question.ID);
            //if (data1.Rows.Count > 0)
            //{
            //    MessageBox.Show(string.Format("STT đã tồn tại .Vui lòng nhập STT khác!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}
            var exp1 = new Expression("ExamQuestionTypeID", TextUtils.ToInt(cboType.EditValue));
            var exp2 = new Expression("STT", txtSTT.EditValue);
            var exp3 = new Expression("ID", question.ID,"<>");

            var questions = SQLHelper<ExamQuestionModel>.FindByExpression(exp1.And(exp2).And(exp3));
            if (questions.Count > 0)
            {
                MessageBox.Show(string.Format($"STT [{txtSTT.EditValue}] đã tồn tại .Vui lòng nhập STT khác!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }


            if (txtContentQuestions.Text.Trim() == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập nội dung câu hỏi và danh sách câu trả lời! "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (!ceA.Checked && !ceB.Checked && !ceC.Checked && !ceD.Checked)
            {
                MessageBox.Show(string.Format("Vui lòng nhập đáp án đúng"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        bool saveData()
        {
            if (!ValidateForm()) return false;

            question.ExamQuestionTypeID = TextUtils.ToInt(cboType.EditValue);
            question.STT = TextUtils.ToInt(txtSTT.Text.Trim());
            question.ContentTest = txtContentQuestions.Text.Trim();
            question.CorrectAnswer = "";
            question.Image = ImagePathSave;
            question.Score = TextUtils.ToInt(txtScore.Value);
            if (ceA.Checked == true)
            {
                question.CorrectAnswer += "A";
            }
            if (ceB.Checked == true)
            {
                question.CorrectAnswer += "B";
            }
            if (ceC.Checked == true)
            {
                question.CorrectAnswer += "C";
            }
            if (ceD.Checked == true)
            {
                question.CorrectAnswer += "D";
            }

            if (question.ID > 0)
            {
                ExamQuestionBO.Instance.Update(question);
            }
            else
            {
                ExamQuestionBO.Instance.Insert(question);
            }
            if (!string.IsNullOrEmpty(fileCopy))
            {
                UploadFile(fileCopy);
            }


            return true;
        }
        private void btnSaveVSClose_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                //if (!string.IsNullOrEmpty(fileCopy))
                //{
                //    string sourcePath = fileCopy;
                //    string destinationPath = @"D:\ThisPC\Hình ảnh\anhdownload\UPLOAD\" + ImagePathSave;
                //    File.Move(sourcePath, destinationPath);
                //}
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSaveVsNew_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                txtSTT.Text = (TextUtils.ToInt(txtSTT.Text.Trim()) + 1) + "";
                txtContentQuestions.Text = "";
                txtScore.Value = 1;
                ceA.Checked = false;
                ceB.Checked = false;
                ceC.Checked = false;
                ceD.Checked = false;

                if (!string.IsNullOrEmpty(fileCopy))
                {
                    UploadFile(fileCopy);
                }
                question.ID = 0;
            }
        }

        private void frmExamQuestionDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (!string.IsNullOrEmpty(fileCopy))
            //{
            //    string sourcePath = fileCopy;
            //    File.Delete(sourcePath);
            //}
            this.DialogResult = DialogResult.OK;
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

                    string newfileImage = name + "_" + DateTime.Now.ToString("ddMMyyHHmm") + extension;

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
                var request = WebRequest.Create("http://192.168.1.2:8083/api/Upload/Images/" + question.Image);
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
            string API_UPLOAD = "http://192.168.1.2:8083/api/Home/upload";
            var client = new WebClient();
            client.Headers.Add("Content-Type", "binary/octet-stream");
            client.UploadFileAsync(new Uri(API_UPLOAD), fileName);
            client.UploadFileCompleted += Client_UploadFileCompleted;
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
    }
}
