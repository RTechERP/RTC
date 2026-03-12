using BMS;
using BMS.Business;
using BMS.Model;
using ExcelDataReader;
using System;
using System.Collections;
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
    public partial class frmQuestionListExcel : _Forms
    {
        int temp;
        public int listID;
        int quantity;
        public frmQuestionListExcel()
        {
            InitializeComponent();
        }
        DateTime start;
        DataSet ds;
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
                grvData.Columns[1].Caption = "Câu hỏi";
                grvData.Columns[2].Caption = "Đáp án";
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
            //grvData.Columns.
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
                txtRate.Text = "";
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
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int rowCount = grvData.RowCount;
            int colCount = grvData.Columns.Count;
            for (int i = 1; i < rowCount; i++)
            {
                try
                {
                    
                    if (string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, "F1")))) continue;
                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i + 1; }));
                    txtRate.Invoke((Action)(() => { txtRate.Text = string.Format("{0}/{1}", i, rowCount - 1); }));
                    ExamQuestionBankModel model = new ExamQuestionBankModel();
                    model.ExamListTestID = listID;
                    int stt = TextUtils.ToInt(grvData.GetRowCellValue(i, "F1"));
                    DataTable st = TextUtils.Select($"Select TOP 1 ID from ExamQuestionBank where ExamListTestID = {model.ExamListTestID} AND STT = {stt}");
                    if (st.Rows.Count > 0)
                    {
                        model.ID = TextUtils.ToInt(st.Rows[0]["ID"]);
                        model.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, "F1"));
                        model.ContentTest = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));
                        model.CorrectAnswer = grvData.GetRowCellValue(i, "F3").ToString();
                        ExamQuestionBankBO.Instance.Update(model);
                    }
                    else
                    {
                        model.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, "F1"));
                        model.ContentTest = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));
                        model.CorrectAnswer = TextUtils.ToString(grvData.GetRowCellValue(i, "F3"));
                        model.ID = (int)ExamQuestionBankBO.Instance.Insert(model);
                    }
                    quantity++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(i.ToString() + Environment.NewLine + ex.ToString());
                }
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
