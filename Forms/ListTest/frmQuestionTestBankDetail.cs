using BMS.Business;
using BMS.Model;
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
    public partial class frmQuestionTestBankDetail : _Forms
    {
        public int catID;
        public ExamQuestionModel question = new ExamQuestionModel();
        public int TypeID;
        public int idListTest;

        string ImagePathSave;

        string fileCopy;
        public frmQuestionTestBankDetail()
        {
            InitializeComponent();
        }

        private void frmQuestionTestBankDetail_Load(object sender, EventArgs e)
        {
            cboTest.EditValue = idListTest;
            loadListTest();
            loadForm();

        }
        void loadListTest()
        {
            DataTable data = TextUtils.Select("SELECT ID,NameTest,CodeTest FROM ExamListTest WHERE ExamListTest.ExamCategoryID = " + catID);
            cboTest.Properties.DataSource = data;
            cboTest.Properties.DisplayMember = "NameTest";
            cboTest.Properties.ValueMember = "ID";
        }
        void loadForm()
        {
            if (question.ID > 0)
            {
                //cboTest.EditValue = question.ExamListTestID;
                txtSTT.EditValue = question.STT;
                txtContentQuestions.Text = question.ContentTest;
                for(int i = 0; i < question.CorrectAnswer.Length; i++)
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
            //if (idListTest > 0)
            //{
            //    cboTest.EditValue = idListTest;
            //}
        }
        bool ValidateForm()
        {
            if(cboTest.Text.Trim() == "")
            {
                MessageBox.Show(string.Format("Vui lòng chọn bài kiểm tra!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }    
            else if(txtSTT.Text.Trim() == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập STT của câu hỏi! "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }  
            else if(txtContentQuestions.Text.Trim() == "")
            {
                MessageBox.Show(string.Format("Vui lòng nhập nội dung câu hỏi và danh sách câu trả lời! "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }else if(!ceA.Checked && !ceB.Checked && !ceC.Checked && !ceD.Checked)
            {
                MessageBox.Show(string.Format("Vui lòng nhập đáp án đúng"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        bool saveData()
        {
            if (!ValidateForm()) return false;

            //question.ExamListTestID = TextUtils.ToInt(cboTest.EditValue);
            question.STT = TextUtils.ToInt(txtSTT.Text.Trim());
            question.ContentTest = txtContentQuestions.Text.Trim();
            question.CorrectAnswer = "";
            question.Image = ImagePathSave;
            if(ceA.Checked == true)
            {
                question.CorrectAnswer += "A";
            } 
            if(ceB.Checked == true)
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

            if(question.ID > 0)
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
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSaveVsNew_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                txtSTT.Text = (TextUtils.ToInt(txtSTT.Text.Trim()) + 1)+"";
                txtContentQuestions.Text = "";
                ceA.Checked = false;
                ceB.Checked = false;
                ceC.Checked = false;
                ceD.Checked = false;
                question.ID = 0;

            }
        }

        private void frmQuestionTestBankDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Pictures files |*.png;*.jpge;*.jpeg;*.jpg";
           
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

                //PbImage.Image = Image.FromFile(TextUtils.pathImage + question.Image);

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

        private void PbImage_Click(object sender, EventArgs e)
        {

        }
    }
}
