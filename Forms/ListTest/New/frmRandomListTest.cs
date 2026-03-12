using BMS.Business;
using BMS.Model;
using DevExpress.XtraGrid.Views.Grid;
using Forms.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmRandomListTest : _Forms
    {
        public int CategoryID;
        public string CategoryName;
        public frmRandomListTest()
        {
            InitializeComponent();
        }

        private void frmRandomListTest_Load(object sender, EventArgs e)
        {
            LoadcbExamQuestionGroup();
            // loadData();
            heigth = Size.Height;
            width = Size.Width;
        }

        void loadData(string values)
        {

            DataTable dt = TextUtils.LoadDataFromSP(StoreProcedures.spGetExamQuestionTypeByListIDExamQuestionGroup, "A", new string[] { "@ListID" }, new object[] { values });
            cbQuestionType.Properties.DataSource = dt;
            cbQuestionType.Properties.ValueMember = "ID";
            cbQuestionType.Properties.DisplayMember = "TypeName";
            cbQuestionType.Properties.DropDownRows = 10;

            cbQuestionType.Text = null;

        }

        void LoadcbExamQuestionGroup()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM ExamQuestionGroup");
            cbExamQuestionGroup.Properties.DataSource = dt;
            cbExamQuestionGroup.Properties.ValueMember = "ID";
            cbExamQuestionGroup.Properties.DisplayMember = "GroupName";
        }
        ArrayList ListIDGroup = new ArrayList();
        private void cbExamQuestionGroup_EditValueChanged(object sender, EventArgs e)
        {


            //if (cbExamQuestionGroup.Text!="")
            //{
            //    var values = cbExamQuestionGroup.EditValue;
            //    loadData(values.ToString());
            //}    
            var values = cbExamQuestionGroup.EditValue;
            loadData(values.ToString());

        }

        bool Validate()
        {

            if (cbQuestionType.Text == "")
            {
                MessageBox.Show("Loại câu hỏi không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (TextUtils.ToInt(txtNumber.Text) <= 0)
            {
                MessageBox.Show("Số câu hỏi dễ phải lớn hơn 0 !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            //if (TextUtils.ToInt(txtnumberl2.Text) < 0)
            //{
            //    MessageBox.Show("Số câu hỏi trung bình phải lớn hơn 0 !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}
            //if (TextUtils.ToInt(txtnumberl3.Text) < 0)
            //{
            //    MessageBox.Show("Số câu hỏi khó phải lớn hơn 0 !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}
            return true;
        }
        bool save()
        {
            grvData.OptionsSelection.EnableAppearanceFocusedRow = false;

            if (!Validate()) return false;

            if (MessageBox.Show($"Bạn có chắc chắn muốn Random {txtNumberListTest.Value} đề không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                //Insert ExamListTest
                int NumberListTest = TextUtils.ToInt(txtNumberListTest.Value);
                for (int i = 0; i < NumberListTest; i++)
                {
                    ExamListTestModel model = new ExamListTestModel();
                    model.ExamTypeTestID = 1;//Loại 
                    model.ExamCategoryID = CategoryID;
                    model.CodeTest = CategoryName + "-D" + (i + 1);
                    model.NameTest = "Đề " + (i + 1);
                    model.TestTime = TextUtils.ToInt(txtTime.Value);
                    int ID = (int)ExamListTestBO.Instance.Insert(model);
                    DataTable dtRandom = new DataTable();
                    if (ckQuestion.Checked == false)
                    {
                        dtRandom = TextUtils.LoadDataFromSP(StoreProcedures.spInsertQuestionFromExamQuestionBankByRandom, "A",
                                                                  new string[] { "@TypeQuestion", "@Number" },
                                                                  new object[] { cbQuestionType.EditValue, txtNumber.Value });
                    }

                    else
                    {
                        for (int h = 0; h < grvData.RowCount; h++)
                        {
                            DataTable dtRandom_i = TextUtils.LoadDataFromSP(StoreProcedures.spRandomQuestionFromExamQuestionBank, "A",
                                                            new string[] { "@TypeQuestion", "@numberl3", "@numberl2", "@numberl1" },
                                                            new object[] { TextUtils.ToString(grvData.GetRowCellValue(h, colTypeQuestionID)),
                                                                           TextUtils.ToInt(grvData.GetRowCellValue(h, colNumberl3)),
                                                                           TextUtils.ToInt(grvData.GetRowCellValue(h, colNumberl2)),
                                                                           TextUtils.ToInt(grvData.GetRowCellValue(h,colNumberl1)) });
                            dtRandom = AddTwoTables(dtRandom_i, dtRandom);

                        }

                    }

                    //Insert QuestionOfExam

                    for (int j = 0; j < dtRandom.Rows.Count; j++)
                    {
                        ExamQuestionListTestModel examModel = new ExamQuestionListTestModel();
                        examModel.ExamListTestID = ID;
                        examModel.ExamQuestionID = TextUtils.ToInt(dtRandom.Rows[j]["ID"]);
                        examModel.STT = j + 1;
                        ExamQuestionListTestBO.Instance.Insert(examModel);
                    }
                }
                return true;
            }
            else
            {
                return false;
            }

            // return true;
        }
        /// <summary>
        /// Add 2 DataTable (DataTable phải giống nhau)
        /// </summary>
        /// <param name="innerTable"></param>
        /// <param name="outerTable"></param>
        /// <returns>DataTable</returns>
        public static DataTable AddTwoTables(DataTable innerTable, DataTable outerTable)
        {
            if (outerTable.Rows.Count == 0)
            {
                return innerTable;
            }
            if (innerTable.Rows.Count == 0)
            {
                return outerTable;
            }

            DataTable resultTable = new DataTable();
            var innerTableColumns = new List<string>();
            var outerTableColumns = new List<string>();
            foreach (DataColumn column in innerTable.Columns)
            {
                innerTableColumns.Add(column.ColumnName);
                outerTableColumns.Add(column.ColumnName);
                resultTable.Columns.Add(column.ColumnName);
            }

            for (int i = 0; i < innerTable.Rows.Count; i++)
            {
                var row = resultTable.NewRow();
                innerTableColumns.ForEach(x =>
                {
                    row[x] = innerTable.Rows[i][x];
                });
                resultTable.Rows.Add(row);
            }
            for (int i = 0; i < outerTable.Rows.Count; i++)
            {
                var row = resultTable.NewRow();
                outerTableColumns.ForEach(x =>
                {
                    row[x] = outerTable.Rows[i][x];
                });
                resultTable.Rows.Add(row);
            }
            return resultTable;


        }


        private void btnSaveVSClose_Click(object sender, EventArgs e)
        {

            if (save())
            {
                this.DialogResult = DialogResult.OK;
            }

        }
     
        void AddTypeQuestion()
        {
            var List = cbQuestionType.EditValue;
            string list = List.ToString().Trim();
            string[] ID = list.Split(',');
            string[] Text = (TextUtils.ToString(cbQuestionType.Text)).Split(',');

            this.Height = heigth + 40 * (ID.Length);
            this.Width = width;
            //grdData.Height = 100 * (ID.Length);

            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("TypeQuestion", typeof(string));
            dt.Columns.Add("Numberl1", typeof(int));
            dt.Columns.Add("Numberl2", typeof(int));
            dt.Columns.Add("Numberl3", typeof(int));
            dt.Columns.Add("Total", typeof(int));

            grdData.DataSource = dt;

            for (int i = 0; i < ID.Length; i++)
            {
                DataRow row = dt.NewRow();
                row["ID"] = TextUtils.ToInt(ID[i]);
                row["TypeQuestion"] = TextUtils.ToString(Text[i]);
                //row["ID"] = TextUtils.ToInt(ID[i]);
                //row["ID"] = TextUtils.ToInt(ID[i]);
                dt.Rows.Add(row);
            }
            grdData.DataSource = dt;


        }
        int heigth;
        int width;
        private void ckQuestion_CheckedChanged(object sender, EventArgs e)
        {

            if (ckQuestion.Checked == true && cbQuestionType.Text != "")
            {
                grdData.Visible = true;
                AddTypeQuestion();
                txtNumber.Enabled = false;
                txtNumber.Value = 0;
            }
            else
            {
                grdData.Visible = false;
                grdData.DataSource = null;
                this.Height = heigth;
                this.Width = width;
                txtNumber.Enabled = true;
            }
        }
        void Calculate()
        {
            // txtNumber.Text = grvData.Columns.ColumnByFieldName("colTotal").SummaryText;
            Decimal Number = 0;
            for (int i = 0; i < grvData.RowCount; i++)
            {
                Number += TextUtils.ToDecimal(grvData.GetRowCellValue(i, colTotal));
            }
            txtNumber.Value = Number;
        }



        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (grdData.Visible == true)
            {
                if (e.Column == colNumberl1 || e.Column == colNumberl2 || e.Column == colNumberl3)
                {
                    int total = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colNumberl1)) +
                                TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colNumberl2)) +
                                TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colNumberl3));
                    grvData.SetRowCellValue(e.RowHandle, colTotal, total);
                    //txtNumber.Value = TextUtils.ToDecimal(grvData.Columns.ColumnByName("colTotal").SummaryItem.SummaryValue);
                    Calculate();
                }
                //grvData.FocusedRowHandle = e.RowHandle;

            }

        }

        private void cbQuestionType_EditValueChanged(object sender, EventArgs e)
        {
            ckQuestion.Checked = false;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            DataTable dtRandom = new DataTable();
            if (ckQuestion.Checked == false)
            {
                dtRandom = TextUtils.LoadDataFromSP(StoreProcedures.spInsertQuestionFromExamQuestionBankByRandom, "A",
                                                          new string[] { "@TypeQuestion", "@Number" },
                                                          new object[] { cbQuestionType.EditValue, txtNumber.Value });
            }

            else
            {
                for (int h = 0; h < grvData.RowCount; h++)
                {
                    DataTable dtRandom_i = TextUtils.LoadDataFromSP(StoreProcedures.spRandomQuestionFromExamQuestionBank, "A",
                                                    new string[] { "@TypeQuestion", "@numberl3", "@numberl2", "@numberl1" },
                                                    new object[] { TextUtils.ToString(grvData.GetRowCellValue(h, colTypeQuestionID))
                                                                  ,TextUtils.ToInt(grvData.GetRowCellValue(h, colNumberl3))
                                                                  ,TextUtils.ToInt(grvData.GetRowCellValue(h, colNumberl2))
                                                                  ,TextUtils.ToInt(grvData.GetRowCellValue(h,colNumberl1)) });
                    dtRandom = AddTwoTables(dtRandom_i, dtRandom);
                }

            }
            //grdTest.DataSource = dtRandom;

        }
    }
}
