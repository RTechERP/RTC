using BMS.Business;
using BMS.Model;
using BMS.Utils;
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmInputExcelFilm : _Forms
    {
        DataSet ds;
        private DateTime start;
        public frmInputExcelFilm()
        {
            InitializeComponent();
        }

        private void frmInputExcelFilm_Load(object sender, EventArgs e)
        {

        }

        private void btnMauExcel_Click(object sender, EventArgs e)
        {
            FileInfo fi = new FileInfo("TemplateImportFilm.xlsx");
            if (fi.Exists)
            {
                System.Diagnostics.Process.Start("TemplateImportFilm.xlsx");
            }
            else
            {
                MessageBox.Show("file doesn't exist", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
        }


        private void btnBrowse_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
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
                    grvData.Focus();
                    grvData.OptionsBehavior.Editable = false;
                    grvData.OptionsBehavior.ReadOnly = true;
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
        void enableControl(bool enable)
        {
            btnSave.Enabled = enable;
            grdData.Enabled = enable;
            cboSheet.Enabled = enable;
            btnBrowse.Enabled = enable;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int rowCount = grvData.RowCount;
            int sttDetail = 0;
            int filmId = 0;
            for (int i = 1; i < rowCount; i++)
            {
                //try
                //{
                //    progressBar1.Invoke((Action)(() => { progressBar1.Value = i + 1; }));

                //    int stt = TextUtils.ToInt(grvData.GetRowCellValue(i, "F1"));
                //    if (stt > 0)
                //    {
                //        string name = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));
                //        FilmManagementModel filmModel = SQLHelper<FilmManagementModel>.SqlToModel($"SELECT * FROM dbo.FilmManagement WHERE Name = '{name}'");
                //        if (filmModel.ID <= 0)
                //        {
                //            filmModel = new FilmManagementModel();
                //        }
                //        filmModel.STT = stt;
                //        filmModel.Code = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));
                //        filmModel.Name = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));

                //        if (filmModel.ID > 0)
                //        {
                //            FilmManagementBO.Instance.Update(filmModel);
                //            filmId = filmModel.ID;
                //        }
                //        else
                //        {
                //            filmId = (int)FilmManagementBO.Instance.Insert(filmModel);
                //        }
                //        FilmManagementDetailBO.Instance.DeleteByAttribute("FilmManagementID", filmId);
                //        sttDetail = 0;
                //    }


                //    string workContent = TextUtils.ToString(grvData.GetRowCellValue(i, "F3")).Trim();

                //    if (!string.IsNullOrEmpty(workContent))
                //    {
                //        string performance = TextUtils.ToString(grvData.GetRowCellValue(i, "F5")).Replace(',', '.');
                //        string unit = TextUtils.ToString(grvData.GetRowCellValue(i, "F4"));
                //        sttDetail++;
                //        FilmManagementDetailModel detail = new FilmManagementDetailModel();
                //        detail.WorkContent = workContent;
                //        detail.STT = sttDetail;
                //        detail.UnitID = TextUtils.ToInt(TextUtils.ExcuteScalar($"SELECT * FROM dbo.UnitCount WHERE UnitName = '{unit}'"));
                //        detail.PerformanceAVG = TextUtils.ToDecimal(performance);
                //        detail.FilmManagementID = filmId;
                //        FilmManagementDetailBO.Instance.Insert(detail);
                //    }
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(i.ToString() + Environment.NewLine + ex.ToString());
                //}

                try
                {
                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i + 1; }));

                    int stt = TextUtils.ToInt(grvData.GetRowCellValue(i, "F1"));
                    if (stt > 0)
                    {
                        string code = TextUtils.ToString(grvData.GetRowCellValue(i, "F2")).Trim();
                        FilmManagementModel filmModel = new FilmManagementModel();

                        var exp1 = new Expression("Code", code);
                        var exp2 = new Expression("IsDeleted", 1,"<>");

                        var matchValue = SQLHelper<FilmManagementModel>.FindByExpression(exp1.And(exp2)).ToList();
                        if (matchValue.Count > 0)
                        {
                            filmModel = matchValue.FirstOrDefault();
                        }

                        filmModel.STT = stt;
                        filmModel.Code = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));
                        filmModel.Name = TextUtils.ToString(grvData.GetRowCellValue(i, "F3"));
                        string requestResult = TextUtils.ToString(grvData.GetRowCellValue(i, "F4"));
                        filmModel.RequestResult = !string.IsNullOrEmpty(requestResult);

                        if (filmModel.ID > 0)
                        {
                            FilmManagementBO.Instance.Update(filmModel);
                            filmId = filmModel.ID;
                        }
                        else
                        {
                            filmId = (int)FilmManagementBO.Instance.Insert(filmModel);
                        }
                        TextUtils.ExcuteSQL($"UPDATE dbo.FilmManagementDetail SET IsDeleted = 1,UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}',UpdatedBy = '{Global.LoginName}' WHERE FilmManagementID = {filmModel.ID}");
                        sttDetail = 0;
                    }


                    string workContent = TextUtils.ToString(grvData.GetRowCellValue(i, "F5")).Trim();

                    if (!string.IsNullOrEmpty(workContent))
                    {
                        string unit = TextUtils.ToString(grvData.GetRowCellValue(i, "F6")).Trim();
                        string performance = TextUtils.ToString(grvData.GetRowCellValue(i, "F7")).Replace(',', '.').Trim();
                        sttDetail++;
                        FilmManagementDetailModel detail = new FilmManagementDetailModel();
                        detail.WorkContent = workContent;
                        detail.STT = sttDetail;
                        detail.UnitID = TextUtils.ToInt(TextUtils.ExcuteScalar($"SELECT * FROM dbo.UnitCount WHERE UnitName = '{unit}'"));
                        detail.PerformanceAVG = TextUtils.ToDecimal(performance);
                        detail.FilmManagementID = filmId;
                        FilmManagementDetailBO.Instance.Insert(detail);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(i.ToString() + Environment.NewLine + ex.ToString());
                }
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show($"Cập nhật thành công!\n{start.ToString() + " - " + DateTime.Now.ToString()}", "Thông báo");
            enableControl(true);
            frmInputExcelFilm_FormClosed(null, null);
        }

        private void frmInputExcelFilm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}