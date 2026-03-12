using BMS.Model;
using DevExpress.Utils;
using DevExpress.XtraEditors;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmDocumentImportExcel : _Forms
    {
        DateTime start;
        DataSet ds;
        public frmDocumentImportExcel()
        {
            InitializeComponent();
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
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            grvData.Columns.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            var result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                btnBrowse.Text = ofd.FileName;

                using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát", ""))
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
                    var tablename = cboSheet.SelectedItem.ToString();

                    grdData.DataSource = ds; // dataset
                    grdData.DataMember = tablename;

                }

            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
            try
            {
                try
                {

                    using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát", ""))
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
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    btnBrowse.Text = ofd.FileName;
                    cboSheet.DataSource = null;
                    cboSheet.DataSource = TextUtils.ListSheetInExcel(ofd.FileName);

                    cboSheet_SelectionChangeCommitted(null, null);
                }
            }
        }

        private void cboSheet_SelectionChangeCommitted(object sender, EventArgs e)
        {
            grvData.Columns.Clear();
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int rowCount = grvData.RowCount;
            int colCount = grvData.Columns.Count;
            var departments = SQLHelper<DepartmentModel>.FindAll();
            var employees = SQLHelper<EmployeeModel>.FindAll();
            var docTypes = SQLHelper<DocumentTypeModel>.FindAll();
            var docs = SQLHelper<DocumentModel>.FindAll();
            for (int i = 0; i < rowCount; i++)
            {
                int stt = TextUtils.ToInt(grvData.GetRowCellValue(i, "F1"));
                if (stt <= 0) continue;

                try
                {
                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i; }));
                    txtRate.Invoke((Action)(() => { txtRate.Text = string.Format("{0}/{1}", i, rowCount - 1); }));
                    var model = new DocumentModel();

                    model.STT = stt;
                    string docCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));
                    model.Code = docCode;
                    model.ID = docs.Where(d => d.Code == docCode).Select(d => d.ID).FirstOrDefault();
                    model.CreatedBy = docs.Where(d => d.Code == docCode).Select(d => d.CreatedBy).FirstOrDefault();
                    model.CreatedDate = docs.Where(d => d.Code == docCode).Select(d => (DateTime?)d.CreatedDate).FirstOrDefault();

                    string docTypeCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F3"));
                    model.DocumentTypeID = docTypes.Where(d => d.Code == docTypeCode).Select(d => d.ID).FirstOrDefault();

                    model.NameDocument = TextUtils.ToString(grvData.GetRowCellValue(i, "F4"));

                    string departmentCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F5"));
                    model.DepartmentID = departments.Where(d => d.Code == departmentCode).Select(d => d.ID).FirstOrDefault();

                    string employeeCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F7"));
                    model.SignedEmployeeID = employees.Where(em => em.Code == employeeCode).Select(em => em.ID).FirstOrDefault();

                    var promulgateDate = TextUtils.ToInt(grvData.GetRowCellValue(i, "F9"));
                    var promulgateMonth = TextUtils.ToInt(grvData.GetRowCellValue(i, "F10"));
                    var promulgateYear = TextUtils.ToInt(grvData.GetRowCellValue(i, "F11"));
                    var effectiveDate = TextUtils.ToInt(grvData.GetRowCellValue(i, "F12"));
                    var effectiveMonth = TextUtils.ToInt(grvData.GetRowCellValue(i, "F13"));
                    var effectiveYear = TextUtils.ToInt(grvData.GetRowCellValue(i, "F14"));

                    model.DatePromulgate = new DateTime(promulgateYear, promulgateMonth, promulgateDate);
                    model.DateEffective = new DateTime(effectiveYear, effectiveMonth, effectiveDate);
                    model.IsPromulgated = model.DatePromulgate.HasValue;

                    model.AffectedScope = TextUtils.ToString(grvData.GetRowCellValue(i, "F15"));
                    model.GroupType = 1;
                    if (model.ID > 0) SQLHelper<DocumentModel>.Update(model);
                    else SQLHelper<DocumentModel>.Insert(model);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(i.ToString() + Environment.NewLine + ex.ToString());
                }
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show($"Cập nhật thành công!\n{start.ToString()} - {DateTime.Now.ToString()}", "Thông báo");
            enableControl(true);
            this.DialogResult = DialogResult.OK;
        }
        void enableControl(bool enable)
        {
            btnSave.Enabled = enable;
            grdData.Enabled = enable;
            cboSheet.Enabled = enable;
            btnBrowse.Enabled = enable;
        }
        bool validate()
        {
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
            else
            {
                if (!validate()) return;
                progressBar1.Minimum = 1;
                if (grvData.RowCount == 0)
                {
                    MessageBox.Show(String.Format("Bạn chưa chọn đường dẫn file hoặc tên sheet. Vui lòng chọn và thử lại!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    progressBar1.Maximum = grvData.RowCount - 1;
                    txtRate.Text = "";
                    start = DateTime.Now;
                    enableControl(false);
                    backgroundWorker1.RunWorkerAsync();
                }
            }
        }

        private void btnMauExcel_Click(object sender, EventArgs e)
        {
            FileInfo fi = new FileInfo("MauVBPhatHanh.xlsx");
            if (fi.Exists)
            {
                System.Diagnostics.Process.Start("MauVBPhatHanh.xlsx");
            }
            else
            {
                MessageBox.Show("file doesn't exist", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
        }
    }
}