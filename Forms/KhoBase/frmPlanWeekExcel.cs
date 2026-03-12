using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Commands;
using DevExpress.XtraSplashScreen;
using ExcelDataReader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmPlanWeekExcel : _Forms
    {
        public frmPlanWeekExcel()
        {
            InitializeComponent();
        }
        DateTime start;
        DataSet ds;
        private void btnBrowse_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            grvData.Columns.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            if (chkAutoCheck.Checked)
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                var result = openFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    btnBrowse.Text = openFileDialog1.FileName;
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    btnBrowse.Text = ofd.FileName;
                    cboSheet.DataSource = null;
                    cboSheet.DataSource = TextUtils.ListSheetInExcel(ofd.FileName);
                    btnSave.Enabled = true;
                }
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

        private void frmImportCheckForceExcel_Load(object sender, EventArgs e)
        {

            //ArrayList
            btnSave.Enabled = false;
        }

        private void cboSheet_SelectionChangeCommitted(object sender, EventArgs e)
        {
            grvData.Columns.Clear();
            if (chkAutoCheck.Checked)
            {
                try
                {
                    grdData.DataSource = ds.Tables[cboSheet.SelectedIndex]; // dataset
                }
                catch (Exception ex)
                {
                    TextUtils.ShowError(ex);
                    grdData.DataSource = null;
                }
            }
            else
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
        void enableControl(bool enable)
        {
            btnSave.Enabled = enable;
            grdData.Enabled = enable;
            cboSheet.Enabled = enable;
            btnBrowse.Enabled = enable;
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
                progressBar1.Maximum = ds.Tables[cboSheet.SelectedIndex].Rows.Count-1;
                txtRate.Text = "";
                start = DateTime.Now;
                enableControl(false);
                backgroundWorker1.RunWorkerAsync();
            }
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable dt = (DataTable)grdData.DataSource;
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                try
                {
                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i; }));
                    txtRate.Invoke((Action)(() => { txtRate.Text = string.Format("{0}/{1}", i, dt.Rows.Count-1); }));
                    WeekPlanModel model = new WeekPlanModel();
                    model.DatePlan = TextUtils.ToDate2(dt.Rows[i]["F1"]);
                    model.UserID = TextUtils.ToInt(dt.Rows[i]["F2"]);
                    model.ContentPlan = TextUtils.ToString(dt.Rows[i]["F4"]);
                    model.Result = TextUtils.ToString(dt.Rows[i]["F5"]);
                    WeekPlanBO.Instance.Insert(model);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(i.ToString() + Environment.NewLine + ex.ToString());
                }
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show(start.ToString() + " - " + DateTime.Now.ToString());
            enableControl(false);
        }
    }
}
