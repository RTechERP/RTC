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
    public partial class frmFollowProjectBaseExcel : _Forms
    {
        public frmFollowProjectBaseExcel()
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
                    var tablename = cboSheet.SelectedItem.ToString();

                    grdData.DataSource = ds; // dataset
                    grdData.DataMember = tablename;
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
                progressBar1.Maximum = grvData.RowCount - 1;
                txtRate.Text = "";
                start = DateTime.Now;
                enableControl(false);
                backgroundWorker1.RunWorkerAsync();
            }
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int rowCount = grvData.RowCount;

            for (int i = 1; i < rowCount; i++)
            {
                try
                {
                    FollowProjectBaseModel followProjectBase = new FollowProjectBaseModel();
                    ArrayList array = FollowProjectBaseBO.Instance.FindByAttribute("ProjectID", getValueDataBase("Project", "ProjectName", grvData.GetRowCellValue(i, "F2").ToString(), 0));
                    if (array.Count > 0)
                    {
                        followProjectBase = (FollowProjectBaseModel)array[0];
                    }
                    if (grvData.GetRowCellValue(i, "F2").ToString() != "" && getValueDataBase("Project", "ProjectName", grvData.GetRowCellValue(i, "F2").ToString(), 0) != 0)
                    {
                        followProjectBase.ProjectID = TextUtils.ToInt(getValueDataBase("Project", "ProjectName", grvData.GetRowCellValue(i, "F2").ToString(), 0));// dự án
                        followProjectBase.UserID = TextUtils.ToInt(getValueDataBase("Users", "FullName", grvData.GetRowCellValue(i, "F3").ToString(), 0));// sales phụ trách
                        followProjectBase.CustomerBaseID = TextUtils.ToInt(getValueDataBase("CustomerBase", "CustomerName", grvData.GetRowCellValue(i, "F4").ToString(), 0));// khách hàng
                        followProjectBase.EndUserID = TextUtils.ToInt(getValueDataBase("CustomerBase", "CustomerName", grvData.GetRowCellValue(i, "F5").ToString(), 0));// end user
                        followProjectBase.ProjectStatusBaseID = TextUtils.ToInt(getValueDataBase("ProjectStatusBase", "ProjectStatusName", grvData.GetRowCellValue(i, "F6").ToString(), 0));// trạng thái dự án
                        followProjectBase.ProjectStartDate = TextUtils.ToDate2(grvData.GetRowCellValue(i, "F7").ToString());// ngày bắt đầu
                        followProjectBase.ProjectTypeBaseID = TextUtils.ToInt(getValueDataBase("ProjectTypeBase", "ProjectTypeName", grvData.GetRowCellValue(i, "F8").ToString(), 0));// loại dự án
                        followProjectBase.FirmBaseID = TextUtils.ToInt(getValueDataBase("FirmBase", "FirmName", grvData.GetRowCellValue(i, "F9").ToString(), 0));// hãng
                        followProjectBase.WorkDone = TextUtils.ToString(grvData.GetRowCellValue(i, "F10").ToString());// việc đã làm
                        followProjectBase.ImplementationDate = TextUtils.ToDate2(grvData.GetRowCellValue(i, "F11").ToString());// ngày thực hiện gần nhất
                        followProjectBase.WorkWillDo = TextUtils.ToString(grvData.GetRowCellValue(i, "F12").ToString());// việc sẽ làm
                        followProjectBase.ExpectedDate = TextUtils.ToDate2(grvData.GetRowCellValue(i, "F13").ToString());// ngày thực hiện dự kiến
                        decimal a = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F14").ToString()) * 100;
                        followProjectBase.PossibilityPO = TextUtils.ToString(TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F14").ToString()) * 100);// khả năng có PO
                        followProjectBase.Fail = TextUtils.ToString(grvData.GetRowCellValue(i, "F15").ToString());// Fail
                        followProjectBase.ExpectedPlanDate = TextUtils.ToDate2(grvData.GetRowCellValue(i, "F16").ToString());// dự kiến ngày lên phương án
                        followProjectBase.ExpectedQuotationDate = TextUtils.ToDate2(grvData.GetRowCellValue(i, "F17").ToString());// dự kiến ngày báo giá
                        followProjectBase.ExpectedPODate = TextUtils.ToDate2(grvData.GetRowCellValue(i, "F18").ToString());// dự kiến ngày po
                        followProjectBase.ExpectedProjectEndDate = TextUtils.ToDate2(grvData.GetRowCellValue(i, "F19").ToString());// dự kiến ngày kết thúc dự án
                        followProjectBase.RealityPlanDate = TextUtils.ToDate2(grvData.GetRowCellValue(i, "F20").ToString());// thực tế ngày lên phương án
                        followProjectBase.RealityQuotationDate = TextUtils.ToDate2(grvData.GetRowCellValue(i, "F21").ToString());// thực tế ngày báo giá
                        followProjectBase.RealityPODate = TextUtils.ToDate2(grvData.GetRowCellValue(i, "F22").ToString());// thực tế ngày po
                        followProjectBase.RealityProjectEndDate = TextUtils.ToDate2(grvData.GetRowCellValue(i, "F23").ToString());// thực tế ngày kết thúc dự án
                        followProjectBase.TotalWithoutVAT = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F24").ToString());// tổng giá chưa Vat
                        followProjectBase.ProjectContactName = TextUtils.ToString(grvData.GetRowCellValue(i, "F25").ToString());// người phụ trách chính
                        followProjectBase.Note = TextUtils.ToString(grvData.GetRowCellValue(i, "F26").ToString());// ghi chú

                        if (array.Count > 0) FollowProjectBaseBO.Instance.Update(followProjectBase);
                        else followProjectBase.ID = (int)FollowProjectBaseBO.Instance.Insert(followProjectBase);
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
            if (MessageBox.Show(start.ToString() + " - " + DateTime.Now.ToString(), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Question) == DialogResult.OK)
            {
                enableControl(true);
                this.Close();
            }
            //MessageBox.Show(start.ToString() + " - " + DateTime.Now.ToString());
        }

        private static int getValueDataBase(string dataBase, string columnName, string value, int result)
        {
            //int result;
            DataTable dt = TextUtils.Select($"Select * From {dataBase} Where {columnName.Trim()} = N'{value.Trim()}' ");
            if (dt.Rows.Count > 0)
            {
                DataRow[] dr = dt.Select();
                result = TextUtils.ToInt(dr[0]["ID"]);
            }
            return result;
        }
    }
}
