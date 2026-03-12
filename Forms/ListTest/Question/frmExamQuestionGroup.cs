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
    public partial class frmExamQuestionGroup : _Forms
    {
        public frmExamQuestionGroup()
        {
            InitializeComponent();
        }

        private void ExamQuestionGroup_Load(object sender, EventArgs e)
        {
            LoadQuestionGroup();
        }
        void LoadQuestionGroup()
        {
            DataTable QuestionGroup = TextUtils.LoadDataFromSP("spGetExamQuestionGroup", "A", new string[] { }, new object[] { });
            grdData.DataSource = QuestionGroup;
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            frmExamQuestionGroupDetail frm = new frmExamQuestionGroupDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadQuestionGroup();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var rowfcMT = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            ExamQuestionGroupModel model = (ExamQuestionGroupModel)ExamQuestionGroupBO.Instance.FindByPK(ID);
            frmExamQuestionGroupDetail questionDetail = new frmExamQuestionGroupDetail();
            questionDetail.questiongroup = model;
            if (questionDetail.ShowDialog() == DialogResult.OK)
            {
                LoadQuestionGroup();
                grvData.FocusedRowHandle = rowfcMT;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var rowfcMT = grvData.FocusedRowHandle;
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colGroupCode));
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colGroupName));
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            DataTable Delete = TextUtils.LoadDataFromSP("spGetDeleteGroup", "A", new string[] { "@ID" }, new object[] { ID });
            if (Delete.Rows.Count > 0)
            {
                MessageBox.Show(string.Format($"Vui lòng xóa tất cả loại câu hỏi của nhóm {code} - {name} và thử lại sau!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show(string.Format("Bạn có muốn xóa nhóm câu hỏi [{0}-{1}] không?", code, name), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ExamQuestionGroupBO.Instance.Delete(ID);
                LoadQuestionGroup();
                grvData.FocusedRowHandle = rowfcMT;
            }
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void frmExamQuestionGroup_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
