using BMS.Business;
using BMS.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using System.Diagnostics;
using DevExpress.XtraPrinting;

namespace BMS
{

    public partial class frmListTest : _Forms
    {
        Dictionary<string, Image> imageCache = new Dictionary<string, Image>(StringComparer.OrdinalIgnoreCase);
        public frmListTest()
        {
            InitializeComponent();
        }

        private void frmListTest_Load(object sender, EventArgs e)
        {
            nudYear.Value = DateTime.Now.Year;
            LoadDataCat();
            LoadDataListTest();
            loadDataQuestion();
        }
        void LoadDataCat()
        {
            DataTable dataTable = TextUtils.Select("SELECT *  FROM ExamCategory WHERE YEAR(CREATEDDATE) = " + nudYear.Value + "ORDER BY ID DESC");
            grdCategory.DataSource = dataTable;
            //grvCategory_FocusedRowChanged(null, null);
        }
        void LoadDataListTest()
        {
            int id = TextUtils.ToInt(grvCategory.GetFocusedRowCellValue(colCatID));
            DataTable dataTable = TextUtils.Select($"SELECT *,t.TypeName  FROM ExamListTest l LEFT JOIN dbo.ExamTypeTest t ON l.ExamTypeTestID = t.ID WHERE l.ExamCategoryID = {id} ORDER BY l.CodeTest");
            grdData.DataSource = dataTable;
            //grvData_FocusedRowChanged(null, null);
        }
        void loadDataQuestion()
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            DataTable data = TextUtils.Select($"SELECT * FROM ExamQuestionBank  WHERE ExamListTestID = {id} ORDER BY STT");

            grdQuestion.DataSource = data;
            //for(int i = 0; i< grvQuestion.RowCount; i++)
            //{
            //    var link = grvQuestion.GetRowCellValue(i, colLinkImg).ToString();
            //    Image image = Image.FromFile($@"{link}");

            //}
        }
        #region Button
        private void btnNew_Click(object sender, EventArgs e)
        {
            int catID = TextUtils.ToInt(grvCategory.GetFocusedRowCellValue(colCatID));
            if (catID <= 0)
            {
                MessageBox.Show("Chưa có kì thi được chọn để tạo đề thi. Hãy tạo kì thi trước!", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                return;
            }
            frmListTestDetail frm = new frmListTestDetail();
            frm.listtest.ExamCategoryID = catID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDataListTest();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedrowhandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            int catID = TextUtils.ToInt(grvCategory.GetFocusedRowCellValue(colCatID));
            if (catID <= 0)
            {
                MessageBox.Show("Chưa có kì thi được chọn để tạo đề thi. Hãy tạo kì thi trước!", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                return;
            }
            if (ID == 0)
            {
                MessageBox.Show("Chưa có đề thi để sửa. Hãy tạo đề thi trước!", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                return;
            }
            ExamListTestModel model = (ExamListTestModel)ExamListTestBO.Instance.FindByPK(ID);
            frmListTestDetail frm = new frmListTestDetail();
            frm.listtest = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDataListTest();
                grvData.FocusedRowHandle = focusedrowhandle;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCodeTest));
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colNameTest));
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0)
            {
                MessageBox.Show("Chưa có đề thi để xóa.", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show(string.Format("Bạn có muốn xóa bài kiểm tra [{0}-{1}] không?", code, name), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ArrayList qbID = new ArrayList();
                for (int i = 0; i < grvQuestion.RowCount; i++)
                {
                    qbID.Add(TextUtils.ToInt(grvQuestion.GetRowCellValue(i, colQuestionID)));
                }
                ExamQuestionBankBO.Instance.Delete(qbID);
                ExamListTestBO.Instance.Delete(ID);
                grvData.DeleteSelectedRows();
                grvData_FocusedRowChanged(null, null);
            }
        }
        private void grvData_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == colTestTime)
            {
                if (TextUtils.ToDecimal(e.Value) > 0)
                {
                    e.DisplayText = Decimal.Round(TextUtils.ToDecimal(e.Value), 0) + " Phút";
                }
                else
                {
                    e.DisplayText = Decimal.Round(TextUtils.ToDecimal(e.Value), 0).ToString();
                }
            }
        }

        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadDataQuestion();
        }

        private void btnDetailAdd_Click(object sender, EventArgs e)
        {
            var rowfcMT = grvData.FocusedRowHandle;
            frmQuestionTestBankDetail frm = new frmQuestionTestBankDetail();
            frm.idListTest = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            frm.catID = TextUtils.ToInt(grvCategory.GetFocusedRowCellValue(colCatID));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                grvData.FocusedRowHandle = rowfcMT;
                grvData_FocusedRowChanged(null, null);
            }
        }

        private void btnDetailEdit_Click(object sender, EventArgs e)
        {
            var rowfcMT = grvData.FocusedRowHandle;
            var focusedrowhandle = grvQuestion.FocusedRowHandle;
            if (focusedrowhandle < 0)
            {
                MessageBox.Show("Chưa có câu hỏi nào được chọn để sửa. Vui lòng tạo câu hỏi trước!");
            }
            int ID = TextUtils.ToInt(grvQuestion.GetFocusedRowCellValue(colQuestionID));
            if (ID == 0) return;
            ExamQuestionBankModel model = (ExamQuestionBankModel)ExamQuestionBankBO.Instance.FindByPK(ID);
            frmQuestionTestBankDetail frm = new frmQuestionTestBankDetail();
            frm.catID = TextUtils.ToInt(grvCategory.GetFocusedRowCellValue(colCatID));
            //frm.question = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //grvData.FocusedRowHandle = rowfcMT;
                grvData_FocusedRowChanged(null, null);
                grvQuestion.FocusedRowHandle = focusedrowhandle;
            }


        }

        private void btndetailDelete_Click(object sender, EventArgs e)
        {
            string stt = TextUtils.ToString(grvQuestion.GetFocusedRowCellValue(colSTT));
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            if (MessageBox.Show(string.Format($"Bạn có muốn xóa câu hỏi mang STT [{stt}] không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ExamQuestionBankBO.Instance.Delete(ID);
                grvData_FocusedRowChanged(null, null);
            }
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            //btnEdit_Click(null, null);
        }

        private void grvQuestion_DoubleClick(object sender, EventArgs e)
        {
            //btnDetailEdit_Click(null, null);
        }
        // Thêm sửa xóa forcus cho bảng Category
        private void grvCategory_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDataListTest();
            loadDataQuestion();
        }

        private void btnCatNew_Click(object sender, EventArgs e)
        {
            frmCategory frm = new frmCategory();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDataCat();
            }
        }

        private void btnCatEdit_Click(object sender, EventArgs e)
        {
            var focusedrowhandle = grvCategory.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvCategory.GetFocusedRowCellValue(colCatID));
            if (ID == 0)
            {
                MessageBox.Show("Chưa có kì thi để sửa.", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                return;
            }
            ExamCategoryModel model = (ExamCategoryModel)ExamCategoryBO.Instance.FindByPK(ID);
            frmCategory frm = new frmCategory();
            frm.model = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadDataCat();
                grvCategory.FocusedRowHandle = focusedrowhandle;
            }
        }

        private void btnCatDelete_Click(object sender, EventArgs e)
        {
            string code = TextUtils.ToString(grvCategory.GetFocusedRowCellValue(colCatCode));
            string name = TextUtils.ToString(grvCategory.GetFocusedRowCellValue(colCatName));
            int ID = TextUtils.ToInt(grvCategory.GetFocusedRowCellValue(colCatID));
            if (ID == 0)
            {
                MessageBox.Show("Chưa có kì thi để xóa.", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show(string.Format("Bạn có muốn xóa kì thi [{0}-{1}] không?", code, name), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = 0; i < grvQuestion.RowCount; i++)
                {
                    int ltID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                    ExamQuestionBankBO.Instance.DeleteByAttribute("ExamListTestID", ltID);
                }
                ExamListTestBO.Instance.DeleteByAttribute("ExamCategoryID", ID);
                ExamCategoryBO.Instance.Delete(ID);
                grvCategory.DeleteSelectedRows();
                grvCategory_FocusedRowChanged(null, null);
            }
        }

        private void nudYear_ValueChanged(object sender, EventArgs e)
        {
            LoadDataCat();
        }

        private void btnNhapExcel_Click(object sender, EventArgs e)
        {
            var rowfcMT = grvData.FocusedRowHandle;
            frmQuestionListExcel frm = new frmQuestionListExcel();
            frm.listID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                grvData.FocusedRowHandle = rowfcMT;
                grvData_FocusedRowChanged(null, null);
            }
        }

        private void grvQuestion_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            //if (e.Column.FieldName == "Img" && e.IsGetData)
            //{
            //    GridView view = sender as GridView;
            //    string fileName = view.GetRowCellValue(view.GetRowHandle(e.ListSourceRowIndex), "Img") as string ?? string.Empty;
            //    if (!imageCache.ContainsKey(fileName))
            //    {
            //        Image img = null;
            //        if (File.Exists(fileName))
            //            img = Image.FromFile(fileName);
            //        else
            //            img = Image.FromFile(@"");

            //        imageCache.Add(fileName, img);
            //    }
            //    e.Value = imageCache[fileName];
            //}
        }

        private void btnDeleteQuestion_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvQuestion.GetFocusedRowCellValue(colQuestionID));
            int stt = TextUtils.ToInt(grvQuestion.GetFocusedRowCellValue(colSTT));
            if (id <= 0) return;

            if (MessageBox.Show($"Bạn có chắc muốn xóa câu hỏi số {stt} không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                ExamQuestionBankBO.Instance.Delete(id);
                grvQuestion.DeleteSelectedRows();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            var codeTest = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCodeTest));
            var codeCat = TextUtils.ToString(grvCategory.GetFocusedRowCellValue(colCatCode));
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog()==DialogResult.OK)
            {
                XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
                optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                grvQuestion.OptionsPrint.PrintSelectedRowsOnly = false;
                try
                {
                    string filepath = $"{f.SelectedPath}/CauHoi_MaDe-{codeTest}_MaKiThi-{codeCat}.xls";
                    grvQuestion.ExportToXls(filepath, optionsEx);
                    
                    Process.Start(filepath);
                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                grvQuestion.ClearSelection();
            }
        }

        private void colDetailAddMany_Click(object sender, EventArgs e)
        {
            var rowhandel = grvCategory.FocusedRowHandle;
            var rowfcMT = grvData.FocusedRowHandle;
            frmQuestionTest frm = new frmQuestionTest();
            frm.ExamListTestID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                grvData.FocusedRowHandle = rowfcMT;
                grvData_FocusedRowChanged(null, null);
            }
        }

        private void grdCategory_DoubleClick(object sender, EventArgs e)
        {
            btnCatEdit_Click(null, null);
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void grdQuestion_DoubleClick(object sender, EventArgs e)
        {
            btnDetailEdit_Click(null, null);
        }
    }
}
#endregion