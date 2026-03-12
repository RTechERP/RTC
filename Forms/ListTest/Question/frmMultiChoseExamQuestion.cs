using BMS;
using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Forms.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms.ListTest.Question
{
    public partial class frmMultiChoseExamQuestion : _Forms
    {
        public List<Dictionary<int, string>> examListTest;

        public string cateCode;
        public string cateName;
        DataTable dataTable;

        public DataTable dtChose;
        public frmMultiChoseExamQuestion()
        {
            InitializeComponent();
        }

        private void frmMultiChoseExamQuestion_Load(object sender, EventArgs e)
        {
            loadcboType();

            lblCatCode.Text = cateCode.Trim() + ": ";
            lblCatName.Text = cateName.Trim();

            for (int i = 0; i < examListTest.Count; i++)
            {
                Dictionary<int, string> dict = examListTest[i];

                int id = dict.Keys.ToArray()[0];
                string value = dict.Values.ToArray()[0];

                GridColumn col = new GridColumn();
                col.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                col.AppearanceCell.Options.UseFont = true;
                col.AppearanceCell.Options.UseTextOptions = true;
                col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                col.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                col.AppearanceHeader.Options.UseFont = true;
                col.AppearanceHeader.Options.UseForeColor = true;
                col.AppearanceHeader.Options.UseTextOptions = true;
                col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                col.Caption = value;
                col.FieldName = id.ToString();
                col.Name = $"col{id}";
                col.Visible = true;
                col.MinWidth = value.Length * 10 <= 100 ? 100 : value.Length * 10;
                col.MaxWidth = col.MinWidth;

                col.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                col.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;


                repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
                col.ColumnEdit = repositoryItemCheckEdit1;

                grvQuestion.Columns.Add(col);

            }

            //LoadQuestion();
        }


        void loadcboType()
        {
            DataTable data = TextUtils.Select("Select * from ExamQuestionGroup ");
            cboType.Properties.DataSource = data;
            cboType.Properties.DisplayMember = "GroupName";
            cboType.Properties.ValueMember = "ID";
        }

        void LoadQuestion()
        {
            int GroupID = TextUtils.ToInt(cboType.EditValue);
            dataTable = TextUtils.LoadDataFromSP(StoreProcedures.spGetExamQuestionByType_v1, "A", new string[] { "@GroupID" }, new object[] { GroupID });

            //Add cột vào Datatable
            foreach (var item in examListTest)
            {
                DataColumn dataColumn = dataTable.Columns.Add(TextUtils.ToString(item.Keys.ToArray()[0]), typeof(bool));
                dataColumn.Caption = "Unbound";
            }

            //set value true những câu hỏi đã được chọn
            for (int i = 0; i < dtChose.Rows.Count; i++)
            {
                int idExam = TextUtils.ToInt(dtChose.Rows[i]["ExamListTestID"]);
                int idQuestion = TextUtils.ToInt(dtChose.Rows[i]["ExamQuestionID"]);

                foreach (DataRow item in dataTable.Rows)
                {
                    int id = TextUtils.ToInt(item["ID"]);

                    if (id == idQuestion)
                    {
                        item[idExam.ToString()] = true;
                    }

                }
            }


            var columns = grvQuestion.Columns.Where(x => x.Visible && x.VisibleIndex > 3 && x.GroupIndex == -1).ToList();
            //int countQuestion = 0;
            //int sumScore = 0;

            foreach (var item in columns)
            {
                var rowIsCheck = dataTable.Select($"[{item.FieldName}] = True");
                int countQuestion = TextUtils.ToInt(rowIsCheck.Length);
                int sumScore = TextUtils.ToInt(dataTable.Compute("Sum(Score)", $"[{item.FieldName}] = True"));

                item.Caption = item.Caption.Split('\n')[0] + $"\n{countQuestion} câu - {sumScore} điểm";

            }

            grdQuestion.DataSource = dataTable;
            grvQuestion.ExpandAllGroups();
        }

        private void cboType_EditValueChanged(object sender, EventArgs e)
        {
            //SaveData();
            LoadQuestion();

            //try
            //{
            //    MyLib.ShowWaitForm("Load data...");

            //}
            //finally
            //{
            //    MyLib.CloseWaitForm();
            //}

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                DialogResult = DialogResult.OK;

            }

        }
        private void CheckedChanged(object sender, EventArgs e)
        {
            grvQuestion.CloseEditor();
            dataTable.AcceptChanges();
            grvQuestion.UpdateTotalSummary();

            string caption = "";
            int sumScore = 0;
            if (grvQuestion.FocusedColumn != null && dataTable != null)
            {
                caption = grvQuestion.FocusedColumn.Caption.Split('\n')[0];
                grvQuestion.CloseEditor();
                dataTable.AcceptChanges();
                DataColumn dataColumn = dataTable.Columns[grvQuestion.FocusedColumn.FieldName];
                DataRow[] rows = dataTable.Select($"[{dataColumn.ColumnName}] = True");

                foreach (var item in rows)
                {
                    sumScore += TextUtils.ToInt(item["Score"]);
                }

                //MessageBox.Show(rows.Length.ToString());
                grvQuestion.FocusedColumn.Caption = caption + $"\n{rows.Length} câu - {sumScore} điểm";

            }
        }
        private void grvQuestion_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (dataTable != null && dataTable.Columns.Contains(e.Column.FieldName))
            {
                DataColumn dataColumn = dataTable.Columns[e.Column.FieldName];
                if (dataColumn.Caption != "Unbound")
                    return;
                repositoryItemCheckEdit1.CheckedChanged -= CheckedChanged;
                repositoryItemCheckEdit1.CheckedChanged += CheckedChanged;
            }
        }

        private void grvQuestion_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            //if (e.IsTotalSummary && (e.Item as GridSummaryItem).FieldName == "Score")
            //{
            //    GridSummaryItem item = e.Item as GridSummaryItem;
            //    if (item.FieldName == "Score")
            //    {
            //        switch (e.SummaryProcess)
            //        {
            //            case CustomSummaryProcess.Start:
            //                sum = 0;
            //                break;
            //            case CustomSummaryProcess.Calculate:
            //                bool shouldSum = TextUtils.ToBoolean(grvQuestion.GetRowCellValue(e.RowHandle, "23"));
            //                if (shouldSum)
            //                {
            //                    sum += TextUtils.ToInt(e.FieldValue);
            //                }
            //                break;
            //            case CustomSummaryProcess.Finalize:
            //                e.TotalValue = sum;
            //                break;
            //        }
            //    }
            //}

            //if(e.RowHandle>0)
            //{
            //    GridSummaryItem item = (GridSummaryItem)e.Item;
            //    item.SummaryType = DevExpress.Data.SummaryItemType.None;
            //    if (item.FieldName == "Score")
            //    {
            //        if (Lib.ToBoolean(grvQuestion.GetRowCellValue(e.RowHandle, item.FieldName)))
            //        {
            //            item.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //        }
            //    }
            //}
        }

        private void grvQuestion_CustomSummaryExists(object sender, DevExpress.Data.CustomSummaryExistEventArgs e)
        {
        }

        private void grvQuestion_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //grvQuestion.CloseEditor();
            //if (e.Column.FieldName == "23")
            //{
            //    if (TextUtils.ToBoolean(grvQuestion.GetRowCellValue(e.RowHandle, e.Column.FieldName)))
            //    {
            //        count ++;
            //    }
            //    else
            //    {
            //        count--;
            //    }
            //    e.Column.Caption += $"\n{count}";
            //}
        }

        private void grvQuestion_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            //if (e.RowHandle >= 0)
            //{
            //    if (e.Column.FieldName == "23")
            //    {
            //        if (TextUtils.ToBoolean(e.CellValue))
            //        {
            //            e.Appearance.BackColor = Color.Pink;
            //        }
            //    }
            //}
        }

        /// <summary>
        /// Lưu câu hỏi đã chọn
        /// </summary>
        /// <returns></returns>
        private bool SaveData()
        {
            try
            {
                MyLib.ShowWaitForm("Save data...");
                grvQuestion.CloseEditor();
                var columns = grvQuestion.Columns.Where(x => x.Visible && x.VisibleIndex > 3 && x.GroupIndex == -1).ToList();


                foreach (var item in columns)
                {
                    int idExam = TextUtils.ToInt(item.FieldName);

                    for (int i = 0; i < grvQuestion.RowCount; i++)
                    {
                        int idQuestion = TextUtils.ToInt(grvQuestion.GetRowCellValue(i, colQuestionID));

                        bool check = TextUtils.ToBoolean(grvQuestion.GetRowCellValue(i, item));

                        DataRow[] match = dtChose.Select($"ExamQuestionID = {idQuestion} AND ExamListTestID = {idExam}");

                        if (check && match.Length <= 0)
                        {

                            ExamQuestionListTestModel examQuestionList = new ExamQuestionListTestModel();

                            examQuestionList.ExamQuestionID = idQuestion;
                            examQuestionList.ExamListTestID = idExam;
                            examQuestionList.STT = (i + 1);

                            ExamQuestionListTestBO.Instance.Insert(examQuestionList);
                        }

                        if (!check && match.Length > 0)
                        {
                            int id = TextUtils.ToInt(match[0]["ID"]);
                            ExamQuestionListTestBO.Instance.Delete(id);
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                MyLib.CloseWaitForm();
            }


        }

        private void grvQuestion_DoubleClick(object sender, EventArgs e)
        {
            int questionId = TextUtils.ToInt(grvQuestion.GetFocusedRowCellValue(colQuestionID));
            if (questionId <= 0)
            {
                return;
            }

            ExamQuestionModel question = (ExamQuestionModel)ExamQuestionBO.Instance.FindByPK(questionId);

            frmExamQuestionDetail frm = new frmExamQuestionDetail();
            frm.question = question;
            if (frm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private int _hoverRow = -1;
        private int _picImageQuestionHeightDefault = 700;
        private int _picImageQuestionWidthDefault = 700;
        private GridCellInfo _oldCellInfo = null;

        private void grvQuestion_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                GridViewInfo viewInfo = (grvQuestion.GetViewInfo() as GridViewInfo);
                if (viewInfo == null)
                {
                    picImageQuestion.Visible = false;
                    return;
                }
                GridHitInfo gridHitInfo = grvQuestion.CalcHitInfo(e.Location);
                GridCellInfo cellInfo = viewInfo.GetGridCellInfo(gridHitInfo);
                if (gridHitInfo != null && gridHitInfo.RowHandle >= 0 && _hoverRow != gridHitInfo.RowHandle && gridHitInfo.Column == colContentTest)//
                {
                    //grvQuestion.SelectRow(gridHitInfo.RowHandle);
                    //grvQuestion.FocusedRowHandle = gridHitInfo.RowHandle;
                    //if (gridHitInfo.Column != colContentTest)
                    //    return;

                    //if (_oldCellInfo != null)
                    //    _oldCellInfo.Appearance.BackColor = Color.White;
                    //_oldCellInfo = null;

                    int questionId = TextUtils.ToInt(grvQuestion.GetRowCellValue(gridHitInfo.RowHandle, colQuestionID));

                    ExamQuestionModel question = (ExamQuestionModel)ExamQuestionBO.Instance.FindByPK(questionId);
                    if (!string.IsNullOrEmpty(question.Image))
                    {
                        if (_oldCellInfo != null)
                            _oldCellInfo.Appearance.BackColor = Color.White;
                        _oldCellInfo = cellInfo;
                        _hoverRow = gridHitInfo.RowHandle;
                        picImageQuestion.Location = new Point(cellInfo.Bounds.Width - picImageQuestion.Width, grdQuestion.Location.Y + grvQuestion.ColumnPanelRowHeight);
                        picImageQuestion.Visible = true;
                        cellInfo.Appearance.BackColor = Color.Yellow;
                        loadImage(question.Image);
                        return;
                    }
                }
                else if (gridHitInfo != null && _hoverRow == gridHitInfo.RowHandle)
                    return;

                if (_oldCellInfo != null)
                    _oldCellInfo.Appearance.BackColor = Color.White;
                _oldCellInfo = null;
                picImageQuestion.Visible = false;
            }
            finally
            {
                if (!picImageQuestion.Visible)
                {
                    if (_oldCellInfo != null)
                        _oldCellInfo.Appearance.BackColor = Color.White;
                    _oldCellInfo = null;
                }
            }
        }

        void loadImage(string imageName)
        {
            try
            {
                var request = WebRequest.Create("http://192.168.1.2:8083/api/Upload/Images/" + imageName);
                var response = request.GetResponse();
                var stream = response.GetResponseStream();

                picImageQuestion.Image = Image.FromStream(stream);
                picImageQuestion.SizeMode = PictureBoxSizeMode.StretchImage;
                picImageQuestion.Width = 700;
                picImageQuestion.Height = 700;

            }
            catch (Exception)
            {
                return;
            }
        }

        private void grvQuestion_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //grvQuestion.CloseEditor();
            //if (e.FocusedRowHandle >= 0 && grvQuestion.FocusedColumn == colContentTest)//
            //{
            //    int questionId = TextUtils.ToInt(grvQuestion.GetRowCellValue(e.FocusedRowHandle, colQuestionID));

            //    ExamQuestionModel question = (ExamQuestionModel)ExamQuestionBO.Instance.FindByPK(questionId);
            //    if (!string.IsNullOrEmpty(question.Image))
            //    {
            //        picImageQuestion.Location = new Point(colContentTest.Width + colSTT.Width - picImageQuestion.Width, grdQuestion.Location.Y + grvQuestion.ColumnPanelRowHeight);
            //        picImageQuestion.Visible = true;

            //        grvQuestion.Appearance.FocusedCell.BackColor = Color.Yellow;
            //        loadImage(question.Image);
            //        //grvQuestion_MouseMove(null, null);
            //    }
            //    else
            //    {
            //        picImageQuestion.Visible = false;
            //    }
            //}
            //else
            //{
            //    picImageQuestion.Visible = false;
            //}
        }
    }
}
