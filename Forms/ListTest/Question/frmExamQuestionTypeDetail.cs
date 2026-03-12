using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BMS.Model;
using BMS.Business;
using BMS.BO;

namespace BMS
{
    public partial class frmExamQuestionTypeDetail : _Forms
    {
        public ExamQuestionTypeModel questionType = new ExamQuestionTypeModel();
        public int ID = 0;
        public frmExamQuestionTypeDetail()
        {
            InitializeComponent();
        }

        private void frmExamQuestionTypeDetail_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        void LoadData()
        {
            if(questionType.ID > 0)
            {
                txtCode.Text = questionType.TypeCode;
                txtName.Text = questionType.TypeName;
                txtScoreRating.EditValue = questionType.ScoreRating;
                ID = questionType.ExamQuestionGroupID;
            }    
        }
        bool validateForm()
        {
            if (txtCode.Text.Trim() == "")
            {
                MessageBox.Show(string.Format("Chưa nhập mã loại câu hỏi. \nVui lòng nhập mã loại câu hỏi!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            DataTable dataTable = TextUtils.Select($"SELECT TOP 1 ID FROM ExamQuestionType WHERE TypeCode = '{txtCode.Text.Trim()}' AND ID <> "+ questionType.ID);
            if (dataTable.Rows.Count > 0)
            {
                MessageBox.Show(string.Format("Mã loại câu hỏi đã tồn tại . \nVui lòng nhập mã khác!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show(string.Format("Chưa nhập tên loại câu hỏi. \nVui lòng nhập tên loại câu hỏi!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            DataTable data = TextUtils.Select($"SELECT TOP 1 ID FROM ExamQuestionType WHERE TypeName = N'{txtName.Text.Trim()}' AND ID <> " + questionType.ID);
            if (data.Rows.Count > 0)
            {
                MessageBox.Show(string.Format("Tên loại câu hỏi đã tồn tại .\nVui lòng nhập tên khác!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if(txtScoreRating.Text == "")
            {
                MessageBox.Show(string.Format("Chưa nhập điểm cho loại câu hỏi . \nVui lòng nhập điểm vào!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }    
            return true;
        }
        bool saveData()
        {
            if (!validateForm()) return false;

            questionType.TypeCode = txtCode.Text.Trim();
            questionType.TypeName = txtName.Text.Trim();
            questionType.ScoreRating = TextUtils.ToDecimal(txtScoreRating.Text.Trim());
            questionType.ExamQuestionGroupID = ID;
            if (questionType.ID > 0)
            {
                ExamQuestionTypeBO.Instance.Update(questionType);
            }
            else
            {
                questionType.ID = (int)ExamQuestionTypeBO.Instance.Insert(questionType);
            }    
            return true;
        }

        private void btnSaveVSClose_Click(object sender, EventArgs e)
        {
            if (!saveData()) return;
            this.DialogResult = DialogResult.OK;
        }
    }
}
