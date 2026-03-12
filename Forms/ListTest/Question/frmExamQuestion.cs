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
using DevExpress.XtraPrinting;
using System.Diagnostics;

namespace BMS
{
    public partial class frmExamQuestion : _Forms
    {
        public frmExamQuestion()
        {
            InitializeComponent();
        }
        void LoadQuestionType()
        {
            DataTable dataTable = TextUtils.Select("SELECT * FROM ExamQuestionType ORDER BY ID DESC");
            grdData.DataSource = dataTable;
        }
        void LoadQuestion()
        {
            int typeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colTypeID));
            //DataTable dataTable = TextUtils.Select("EXEC spGetExamQuestionByType " + typeID);
            DataTable dataTable = TextUtils.LoadDataFromSP("spGetExamQuestionByType", "A", new string[] { "TypeID" }, new object[] { typeID });
            grdQuestion.DataSource = dataTable;
        }
        private void frmExamQuestion_Load(object sender, EventArgs e)
        {
            LoadQuestionType();
            LoadQuestion();
        }

        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadQuestion();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmExamQuestionTypeDetail frm = new frmExamQuestionTypeDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                frmExamQuestion_Load(null, null);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int row = grvData.FocusedRowHandle;
            if (row < 0)
            {
                MessageBox.Show("Chưa có loại câu hỏi để sửa. Vui lòng thêm loại câu hỏi", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                return;
            }
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colTypeID));
            frmExamQuestionTypeDetail frm = new frmExamQuestionTypeDetail();
            frm.questionType = (ExamQuestionTypeModel)ExamQuestionTypeBO.Instance.FindByPK(ID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadQuestionType();
                grvData.FocusedRowHandle = row;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colTypeCode));
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colTypeName));
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colTypeID));
            if (ID <= 0)
            {
                MessageBox.Show("Chưa có loại câu hỏi để xóa.", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show(string.Format("Bạn có muốn xóa loại câu hỏi [{0}-{1}] không?", code, name), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ExamQuestionBO.Instance.DeleteByAttribute("TypeID",ID);
                ExamQuestionTypeBO.Instance.Delete(ID);
                frmExamQuestion_Load(null, null);
            }
        }

        private void btnDetailAdd_Click(object sender, EventArgs e)
        {
            var rowfcMT = grvData.FocusedRowHandle;
            frmExamQuestionDetail frm = new frmExamQuestionDetail();
            frm.TypeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colTypeID));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                grvData.FocusedRowHandle = rowfcMT;
            }
        }

        private void btnDetailEdit_Click(object sender, EventArgs e)
        {
            var rowfcMT = grvData.FocusedRowHandle;
            var focusedrowhandle = grvQuestion.FocusedRowHandle;
            if (rowfcMT < 0)
            {
                MessageBox.Show("Chưa có loại câu hỏi nào để lưu câu hỏi. \nVui lòng tạo loại câu hỏi trước, sau đó tạo câu hỏi!");
            }
            if (focusedrowhandle < 0)
            {
                MessageBox.Show("Chưa có câu hỏi nào để sửa. Vui lòng tạo câu hỏi trước!");
            }
            int ID = TextUtils.ToInt(grvQuestion.GetFocusedRowCellValue(colQuestionID));
            if (ID == 0) return;
            frmExamQuestionDetail frm = new frmExamQuestionDetail();
            //frm.question = (ExamQuestionModel)ExamQuestionBO.Instance.FindByPK(ID);
            frm.TypeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colTypeID));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                grvData.FocusedRowHandle = rowfcMT;
                grvData_FocusedRowChanged(null, null);
                grvQuestion.FocusedRowHandle = focusedrowhandle;
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
                        int id = TextUtils.ToInt(grvQuestion.GetRowCellValue(row,colQuestionID));
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

            LoadQuestion();
        }

        private void btnNhapExcel_Click(object sender, EventArgs e)
        {
            var rowfcMT = grvData.FocusedRowHandle;
            frmExamQuestionExcel frm = new frmExamQuestionExcel();
            frm.typeID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colTypeID));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                grvData.FocusedRowHandle = rowfcMT;
                grvData_FocusedRowChanged(null, null);
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
                grvQuestion.OptionsPrint.PrintSelectedRowsOnly = false;
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
    }
}
