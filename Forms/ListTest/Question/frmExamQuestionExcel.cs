using BMS.Business;
using BMS.Model;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmExamQuestionExcel : _Forms
    {
        int temp;
        public int typeID;
        public int Score;
        int quantity;
        public frmExamQuestionExcel()
        {
            InitializeComponent();
        }
        DateTime start;
        DataSet ds;
        private void frmExamQuestionExcel_Load(object sender, EventArgs e)
        {

            loadCB();
            if (typeID > 0)
            {
                cbType.SelectedValue = typeID;
            }
            else
            {
                cbType.SelectedValue = 0;
            }

        }
        void loadCB()
        {
            //DataTable dt = TextUtils.Select("Select ID,TypeCode from ExamQuestionType");
            //DataRow row = dt.NewRow();
            //row["ID"] = 0;
            //row["TypeCode"] = "Tất cả";
            //dt.Rows.Add(row);
            //cbType.DataSource = dt;
            //cbType.DisplayMember = "TypeCode";
            //cbType.ValueMember = "ID"; 
            DataTable dt = TextUtils.Select("SELECT * FROM ExamQuestionGroup");
            DataRow row = dt.NewRow();
            row["ID"] = 0;
            row["GroupName"] = "Tất cả";
            dt.Rows.Add(row);
            cbType.DataSource = dt;
            cbType.DisplayMember = "GroupName";
            cbType.ValueMember = "ID";


        }
        private void btnBrowse_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            grvData.Columns.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            var result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                btnBrowse.Text = ofd.FileName;
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
            try
            {
                var stream = new FileStream(btnBrowse.Text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                var sw = new Stopwatch();
                sw.Start();

                IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);

                var openTiming = sw.ElapsedMilliseconds;

                ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    UseColumnDataType = false,
                    ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = false
                    }
                });

                var tablenames = GetTablenames(ds.Tables);

                cboSheet.DataSource = tablenames;

                if (tablenames.Count > 0)
                    cboSheet.SelectedIndex = 0;
                btnSave.Enabled = true;
                cboSheet_SelectionChangeCommitted(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (temp == 0)
            {
                grvData.Columns[0].Caption = "STT";
                grvData.Columns[1].Caption = "Điểm";
                grvData.Columns[2].Caption = "Câu hỏi";
                grvData.Columns[3].Caption = "Đáp án";
                grvData.Columns[4].Caption = "Mã loại";
                grvData.Columns[0].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                grvData.Columns[2].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }
            else temp++;
        }
        private void cboSheet_SelectionChangeCommitted(object sender, EventArgs e)
        {
            grdData.DataSource = null;
            try
            {
                var tablename = cboSheet.SelectedItem.ToString();
                grdData.DataSource = ds;
                grdData.DataMember = tablename;
            }
            catch (Exception ex)
            {
                TextUtils.ShowError(ex);
                grdData.DataSource = null;
            }
            if (grdData.DataSource == null)
            {
                try
                {
                    DataTable dt = TextUtils.ExcelToDatatableNoHeader(btnBrowse.Text, cboSheet.SelectedValue.ToString());

                    grdData.DataSource = dt;
                    grvData.PopulateColumns();
                    grvData.BestFitColumns();
                    grdData.Focus();
                }
                catch (Exception ex)
                {
                    TextUtils.ShowError(ex);
                    grdData.DataSource = null;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
            else
            {
                progressBar1.Minimum = 1;
                progressBar1.Maximum = grvData.RowCount;
                start = DateTime.Now;
                enableControl(false);
                backgroundWorker1.RunWorkerAsync();
            }



        }
        private static IList<string> GetTablenames(DataTableCollection tables)
        {
            var tableList = new List<string>();
            foreach (var table in tables)
            {
                tableList.Add(table.ToString());
            }

            return tableList;
        }
        DataTable dt = TextUtils.Select("Select * from ExamQuestionType");
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                MyLib.ShowWaitForm("Đang nhập dữ liệu vui lòng chờ .......");


                int rowCount = grvData.RowCount;
                int colCount = grvData.Columns.Count;
                for (int i = 0; i <= rowCount; i++)
                {
                    try
                    {
                        int stt = TextUtils.ToInt(grvData.GetRowCellValue(i, "F1"));
                        if (stt <= 0) continue;
                        progressBar1.Invoke((Action)(() => { progressBar1.Value = i + 1; }));
                        ExamQuestionModel question = new ExamQuestionModel();

                        int typeID = 0;
                        //int s = TextUtils.ToInt(cbType.ValueMember);
                        if (TextUtils.ToInt(cbType.ValueMember) == 0)
                        {
                            string TypeCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F5")).Trim().ToUpper();
                            DataRow[] row = dt.Select($"TypeCode = '{TypeCode}'");
                            if (row.Count() == 0) continue;
                            question.ExamQuestionTypeID = TextUtils.ToInt(row[0]["ID"]);
                            typeID = TextUtils.ToInt(row[0]["ID"]);
                        }
                        else
                        {
                            string TypeCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F5")).Trim().ToUpper();
                            DataTable dt_by_Group = TextUtils.Select($"Select * from ExamQuestionType where ExamQuestionGroupID={cbType.ValueMember} ");
                            DataRow[] row = dt_by_Group.Select($"TypeCode = '{TypeCode}'");
                            //DataRow[] row1 = dt.Select($"ExamQuestionGroupID = '{cbType.SelectedValue}'");
                            if (row.Count() == 0) continue;
                            question.ExamQuestionTypeID = TextUtils.ToInt(row[0]["ID"]);
                            typeID = TextUtils.ToInt(row[0]["ID"]);

                            //question.ExamQuestionTypeID = typeID;
                        }



                        //int stt = TextUtils.ToInt(grvData.GetRowCellValue(i, "F1"));
                        DataTable st = TextUtils.Select($"Select TOP 1 ID from ExamQuestion where ExamQuestionTypeID = {question.ExamQuestionTypeID} AND STT = {stt}");
                        //question = SQLHelper<ExamQuestionModel>.SqlToModel($"Select TOP 1 ID from ExamQuestion where ExamQuestionTypeID = {typeID} AND STT = {stt}");
                        question.STT = stt;
                        question.ContentTest = TextUtils.ToString(grvData.GetRowCellValue(i, "F3"));
                        question.CorrectAnswer = grvData.GetRowCellValue(i, "F4").ToString();
                        //question.I

                        int score = TextUtils.ToInt(grvData.GetRowCellValue(i, "F2"));
                        if (score <= 0)
                        {
                            question.Score = TextUtils.ToInt(dt.Rows[0]["ScoreRating"]);
                        }
                        else
                        {
                            question.Score = score;// TextUtils.ToInt(grvData.GetRowCellValue(i, "F4"));
                        }

                        if (st.Rows.Count > 0)
                        {
                            question.ID = TextUtils.ToInt(st.Rows[0]["ID"]);
                            ExamQuestionBO.Instance.Update(question);
                        }
                        else
                        {
                            question.ID = (int)ExamQuestionBO.Instance.Insert(question);
                        }
                        quantity++;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(i.ToString() + Environment.NewLine + ex.ToString());
                    }
                }
            }
            finally
            {
                MyLib.CloseWaitForm();
            }
        }


        void enableControl(bool enable)
        {
            btnSave.Enabled = enable;
            grdData.Enabled = enable;
            cboSheet.Enabled = enable;
            btnBrowse.Enabled = enable;
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (quantity != 0)
            {
                MessageBox.Show(start.ToString() + " - " + DateTime.Now.ToString());
            }
            enableControl(true);
        }

        private void frmMachineListImportExcel_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }


    }
}
