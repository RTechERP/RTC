using BMS.Business;
using BMS.Model;
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
    public partial class frmExamQuestionGroupDetail : _Forms
    {
        public ExamQuestionGroupModel questiongroup = new ExamQuestionGroupModel();
        public frmExamQuestionGroupDetail()
        {
            InitializeComponent();
        }

        private void ExamQuestionGroupDetail_Load(object sender, EventArgs e)
        {
            loadDepartment();
            LoadData();
        }
        void loadDepartment()
        {
            List<DepartmentModel> lst = SQLHelper<DepartmentModel>.SqlToList("SELECT * FROM dbo.Department");
            cboDepartment.Properties.DataSource = lst;
            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.Properties.DisplayMember = "Name";
        }
        void LoadData()
        {
            if(questiongroup.ID > 0)
            {
                txtCode.Text = questiongroup.GroupCode;
                txtName.Text = questiongroup.GroupName;
                cboDepartment.EditValue = questiongroup.DepartmentID;
            }    
        }
        bool ValidateForm()
        {
            if(txtCode.Text == "")
            {
                MessageBox.Show(string.Format("Mã nhóm câu hỏi đang trống . \nVui lòng nhập vào!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }    
            else
            {
                DataTable dataTable = TextUtils.Select($"SELECT TOP 1 ID FROM ExamQuestionGroup WHERE GroupCode = '{txtCode.Text.Trim()}' AND ID <> " + questiongroup.ID);
                if (dataTable.Rows.Count > 0)
                {
                    MessageBox.Show(string.Format("Mã nhóm câu hỏi đã tồn tại . \nVui lòng nhập lại!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }    
            if(txtName.Text == "")
            {
                MessageBox.Show(string.Format("Tên nhóm câu hỏi đang trống . \nVui lòng nhập vào!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }    
            else
            {
                DataTable data = TextUtils.Select($"SELECT TOP 1 ID FROM ExamQuestionGroup WHERE GroupName = N'{txtName.Text.Trim()}' AND ID <> " + questiongroup.ID);
                if (data.Rows.Count > 0)
                {
                    MessageBox.Show(string.Format("Tên loại câu hỏi đã tồn tại . \nVui lòng nhập lại!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }    
            return true;
        }
        bool saveData()
        {
            if (!ValidateForm()) return true;

            questiongroup.GroupCode = txtCode.Text.Trim();
            questiongroup.GroupName = txtName.Text.Trim();
            questiongroup.DepartmentID = TextUtils.ToInt( cboDepartment.EditValue);
            if(questiongroup.ID > 0)
            {
                ExamQuestionGroupBO.Instance.Update(questiongroup);
            }
            else
            {
                questiongroup.ID = (int)ExamQuestionGroupBO.Instance.Insert(questiongroup);
            }    
            return true;
        }
        private void btnSaveVSClose_Click(object sender, EventArgs e)
        {
            if(saveData())
            {
                this.DialogResult = DialogResult.OK;
            }    
        }
    }
}
