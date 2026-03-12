using BMS.Business;
using BMS.Model;
using DevExpress.XtraPrinting;
using Forms.Classes;
using System;
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
    public partial class frmExamQuestion_New : _Forms
    {
        public frmExamQuestion_New()
        {
            InitializeComponent();
        }

        private void frmExamQuestion_New_Load(object sender, EventArgs e)
        {
            LoadcboQuestionGroup();
            LoadQuestionType();
            LoadQuestion();
        }
        #region Load Data
        void LoadQuestionType()
        {
            int GroupID = TextUtils.ToInt(cboQuestionGroup.EditValue);
            DataTable questionType = TextUtils.LoadDataFromSP(StoreProcedures.spGetExamQuestionTypeByGroup, "A", new string[] { "@GroupID" }, new object[] { GroupID });
            grdData.DataSource = questionType;
        }
        void LoadQuestion()
        {
            int typeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colTypeID));
            DataTable question = TextUtils.LoadDataFromSP(StoreProcedures.spGetExamQuestionByType, "A", new string[] { "@TypeID", "@CheckAll", "@GroupID" }, new object[] { typeID,TextUtils.ToInt(checkAll.Checked),TextUtils.ToInt(cboQuestionGroup.EditValue) });
            grdQuestion.DataSource = question;
        }

        void LoadcboQuestionGroup()
        {
            DataTable EXQGroup = TextUtils.LoadDataFromSP("spGetExamQuestionGroup", "A", new string[] { }, new object[] { });
            cboQuestionGroup.Properties.DataSource = EXQGroup;
            cboQuestionGroup.Properties.DisplayMember = "GroupName";
            cboQuestionGroup.Properties.ValueMember = "ID";
        }
        #endregion

        #region Button
        private void cboQuestionGroup_EditValueChanged(object sender, EventArgs e)
        {
            LoadQuestionType();
            LoadQuestion();
        }

        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadQuestion();
        }

        private void btnNewType_Click(object sender, EventArgs e)
        {
            if (cboQuestionGroup.Text == "")
            {
                MessageBox.Show(string.Format("Chưa chọn nhóm câu hỏi. \nVui lòng chọn nhóm câu hỏi và thử lại!"), TextUtils.Caption, MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            int IDquestion = TextUtils.ToInt(cboQuestionGroup.EditValue);
            frmExamQuestionTypeDetail frm = new frmExamQuestionTypeDetail();
            frm.ID = IDquestion;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadQuestionType();
                LoadQuestion();
            }
        }

        private void btnEditType_Click(object sender, EventArgs e)
        {
            var rowfcMT = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colTypeID));
            if (ID == 0)
            {
                MessageBox.Show(string.Format("Chưa chọn loại câu hỏi. \nVui lòng chọn loại câu hỏi và thử lại!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            frmExamQuestionTypeDetail frm = new frmExamQuestionTypeDetail();
            ExamQuestionTypeModel model = (ExamQuestionTypeModel)ExamQuestionTypeBO.Instance.FindByPK(ID);
            frm.questionType = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadQuestionType();
                LoadQuestion();
                grvData.FocusedRowHandle = rowfcMT;
            }
        }

        private void btnDeleteType_Click(object sender, EventArgs e)
        {
            var rowfcMT = grvData.FocusedRowHandle;
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colTypeCode));
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colTypeName));
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colTypeID));
            if (ID == 0)
            {
                MessageBox.Show(string.Format("Chưa chọn loại câu hỏi. \nVui lòng chọn loại câu hỏi và thử lại!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(grvQuestion.RowCount > 0)
            {
                MessageBox.Show(string.Format($"Vui lòng xóa tất cả các câu hỏi của loại câu hỏi {code} - {name} !"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }    
            if (MessageBox.Show(string.Format("Bạn có muốn xóa loại câu hỏi [{0}-{1}] không?", code, name), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ExamQuestionBO.Instance.DeleteByAttribute("TypeID", ID);
                ExamQuestionTypeBO.Instance.Delete(ID);
                LoadQuestionType();
                LoadQuestion();
                grvData.FocusedRowHandle = rowfcMT;
            }
        }

        private void btnAddQuestion_Click(object sender, EventArgs e)
        {
            if (cboQuestionGroup.Text == "")
            {
                MessageBox.Show(string.Format("Chưa chọn nhóm câu hỏi. \nVui lòng chọn nhóm câu hỏi và thử lại!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colTypeID));
            if (ID == 0)
            {
                MessageBox.Show(string.Format("Chưa chọn loại câu hỏi. \nVui lòng chọn loại câu hỏi và thử lại!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            frmExamQuestionDetail frm = new frmExamQuestionDetail();
            frm.TypeID = ID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadQuestion();
            }
        }

        private void btnEditQuestion_Click(object sender, EventArgs e)
        {
            var rowfcMT = grvQuestion.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvQuestion.GetFocusedRowCellValue(colQuestionID));
            if (ID == 0)
            {
                MessageBox.Show(string.Format("Chưa chọn câu hỏi. \nVui lòng chọn câu hỏi và thử lại!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            frmExamQuestionDetail frm = new frmExamQuestionDetail();
            ExamQuestionModel Question = (ExamQuestionModel)ExamQuestionBO.Instance.FindByPK(ID);
            frm.question = Question;
            frm.TypeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colTypeID));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadQuestion();
                grvQuestion.FocusedRowHandle = rowfcMT;
            }
        }

        private void btnDeleteQuestion_Click(object sender, EventArgs e)
        {
            int[] rowSelected = grvQuestion.GetSelectedRows();
            if (rowSelected.Length > 1)
            {
                DialogResult dialog = MessageBox.Show("Bạn có chắc muốn danh sách câu hỏi đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    foreach (var row in rowSelected)
                    {
                        int id = TextUtils.ToInt(grvQuestion.GetRowCellValue(row, colQuestionID));
                        if (id <= 0)
                        {
                            continue;
                        }
                        ExamQuestionBO.Instance.Delete(id);
                    }

                }
            }
            else if (rowSelected.Length == 1)
            {
                string stt = TextUtils.ToString(grvQuestion.GetFocusedRowCellValue(colSTT));
                int ID = TextUtils.ToInt(grvQuestion.GetFocusedRowCellValue(colQuestionID));
                if (ID == 0) return;
                if (MessageBox.Show(string.Format($"Bạn có muốn xóa câu hỏi mang STT [{stt}] không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ExamQuestionBO.Instance.Delete(ID);
                }
            }
            else
            {
                return;
            }

            //var rowfcMT = grvQuestion.FocusedRowHandle;
            //string stt = TextUtils.ToString(grvQuestion.GetFocusedRowCellValue(colSTT));
            //int ID = TextUtils.ToInt(grvQuestion.GetFocusedRowCellValue(colQuestionID));
            //if (ID == 0)
            //{
            //    MessageBox.Show(string.Format("Chưa chọn câu hỏi. \nVui lòng chọn câu hỏi và thử lại!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //if (MessageBox.Show(string.Format($"Bạn có muốn xóa câu hỏi mang STT [{stt}] không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    ExamQuestionBO.Instance.Delete(ID);
            //    LoadQuestion();
            //    grvQuestion.FocusedRowHandle = rowfcMT;
            //}

            LoadQuestion();
        }

        private void btnNhapExcel_Click(object sender, EventArgs e)
        { 
            var rowfcMT = grvQuestion.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colTypeID));
            int ScoreRating = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colScoreRating));
            //if (ID == 0)
            //{
            //    MessageBox.Show(string.Format("Vui lòng chọn nhóm và loại câu hỏi để nhập Excel!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            frmExamQuestionExcel frm = new frmExamQuestionExcel();
            frm.typeID = ID;
            frm.Score= ScoreRating;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadQuestion();
                grvQuestion.FocusedRowHandle = rowfcMT;
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            var typeCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colTypeCode));
            var typeName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colTypeName));
            
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
                optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
               // grvQuestion.OptionsPrint.PrintSelectedRowsOnly = false;
                try
                {
                    string filepath = $"{f.SelectedPath}/CauHoi_Loai[{typeCode}-{typeName}].xls";
                    grvQuestion.ExportToXls(filepath, optionsEx);
                    

                    Process.Start(filepath);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                grvQuestion.ClearSelection();
            }
        }

        private void btnNewForm_Click(object sender, EventArgs e)
        {
            frmExamQuestionGroup frm = new frmExamQuestionGroup();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadcboQuestionGroup();
            }
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            btnEditType_Click(null,null);
        }

        private void grdQuestion_DoubleClick(object sender, EventArgs e)
        {
            btnEditQuestion_Click(null,null);
        }
        #endregion

        private void checkAll_CheckedChanged(object sender, EventArgs e)
        {
            LoadQuestion();
            
        }
    }
}
