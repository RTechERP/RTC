using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.Office.Import.OpenXml;
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
    public partial class frmDailyReportExcel : _Forms
    {
        public frmDailyReportExcel()
        {
            InitializeComponent();
        }
        DateTime start;
        DataSet ds;
        DataTable users = new DataTable();
        DataTable firmBases = new DataTable();
        List<ProjectModel> projects = new List<ProjectModel>();
        DataTable projectTypeBases = new DataTable();
        DataTable customers = new DataTable();
        DataTable contacts = new DataTable();
        List<MainIndexModel> mainIndexes = new List<MainIndexModel>();
        DataTable customerParts = new DataTable();

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
            users =  TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            projects = SQLHelper<ProjectModel>.FindAll().OrderByDescending(x => x.ID).ToList();
            firmBases = TextUtils.Select("SELECT * FROM FirmBase");
            projectTypeBases = TextUtils.Select("SELECT * FROM ProjectTypeBase");
            customers = TextUtils.Select("SELECT * FROM Customer where IsDeleted <> 1");
            contacts = TextUtils.Select($"SELECT ContactPhone,ContactEmail,ContactName,ID FROM dbo.CustomerContact");
            mainIndexes = SQLHelper<MainIndexModel>.ProcedureToList("spGetMainIndex", new string[] { "@Type" }, new object[] { 2 });
            customerParts = TextUtils.Select($"SELECT  cp.*, c.CustomerCode FROM dbo.CustomerPart cp INNER JOIN dbo.Customer c ON cp.CustomerID = c.ID");
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
                    DailyReportSaleModel model = new DailyReportSaleModel();
                    model.DateStart = TextUtils.ToDate4(dt.Rows[i]["F1"]); //Ngày thực hiện gần nhất
                    model.DateEnd = TextUtils.ToDate4(dt.Rows[i]["F2"]); //Ngày dự kiến thực hiện

                    var name = TextUtils.ToString(dt.Rows[i]["F3"]);
                    var nameRow = users.AsEnumerable()
                                   .FirstOrDefault(r => r.Field<string>("FullName") == name);
                    model.UserID = nameRow != null ? nameRow.Field<int>("UserID") : 0; // Người phụ trách

                    var project = TextUtils.ToString(dt.Rows[i]["F4"]);
                    var projectID = projects.Where(x => x.ProjectCode == project).Select(x => x.ID).FirstOrDefault();
                    model.ProjectID =  projectID; // Mã dự án, Tên dự án

                    var firmBase = TextUtils.ToString(dt.Rows[i]["F6"]);
                    var firmBaseRow = firmBases.AsEnumerable()
                        .FirstOrDefault(r => r.Field<string>("FirmName") == firmBase);
                    model.FirmBaseID = firmBaseRow != null ? firmBaseRow.Field<int>("ID") : 0; // Hãng

                    var projectTypeBase = TextUtils.ToString(dt.Rows[i]["F7"]);
                    var projectTypeBaseRow = projectTypeBases.AsEnumerable()
                        .FirstOrDefault(r => r.Field<string>("ProjectTypeName") == projectTypeBase);
                    model.ProjectTypeBaseID = projectTypeBaseRow != null ? projectTypeBaseRow.Field<int>("ID") : 0; // Loại dự án

                    var customer = TextUtils.ToString(dt.Rows[i]["F8"]);
                    var customerRow = customers.AsEnumerable()
                        .FirstOrDefault(r => r.Field<string>("CustomerName") == customer);
                    model.CustomerID = customerRow != null ? customerRow.Field<int>("ID") : 0; // Khách hàng

                    model.ProductOfCustomer = TextUtils.ToString(dt.Rows[i]["F10"]); // Sản phẩm của KH

                    var contact = TextUtils.ToString(dt.Rows[i]["F11"]);
                    var contactRow = contacts.AsEnumerable()
                        .FirstOrDefault(r => r.Field<string>("ContactName") == contact);
                    model.ContacID = contactRow != null ? contactRow.Field<int>("ID") : 0; // Người liên hệ

                    var mainIndex = TextUtils.ToString(dt.Rows[i]["F12"]);
                    var mainIndexID = mainIndexes.Where(x => x.MainIndex == mainIndex).Select(x => x.ID).FirstOrDefault();
                    model.GroupType = mainIndexID; // Loại nhóm

                    model.Content = TextUtils.ToString(dt.Rows[i]["F13"]); // Việc đã làm

                    model.Result = TextUtils.ToString(dt.Rows[i]["F14"]); // Kết quả 

                    model.ProblemBacklog = TextUtils.ToString(dt.Rows[i]["F15"]); // Vấn đề tồn đọng

                    model.PlanNext = TextUtils.ToString(dt.Rows[i]["F16"]); // Kế hoạch tiếp theo

                    var partCode = TextUtils.ToString(dt.Rows[i]["F17"]);
                    var customerCode = TextUtils.ToString(dt.Rows[i]["F9"]);
                    var endUserRow = customerParts.AsEnumerable()
                        .FirstOrDefault(r => r.Field<string>("PartCode") == partCode && r.Field<string>("CustomerCode") == customerCode);

                    string bigAccount = TextUtils.ToString(dt.Rows[i]["F18"]).Trim().ToLower();
                    if (bigAccount == "x")
                    {
                        model.BigAccount = true;
                    }
                    else
                    {
                        model.BigAccount = false;
                    }

                    string saleOpportunity = TextUtils.ToString(dt.Rows[i]["F19"]).Trim().ToLower();
                    if (saleOpportunity == "x")
                    {
                        model.SaleOpportunity = true;
                    }
                    else
                    {
                        model.SaleOpportunity = false;
                    }

                    //model.EndUser = TextUtils.ToInt(dt.Rows[i]["F4"]);
                    //model.UserID = TextUtils.ToInt(dt.Rows[i]["F6"]);
                    //model.ContacID = TextUtils.ToInt(dt.Rows[i]["F8"]);
                    //model.GroupType = TextUtils.ToInt(dt.Rows[i]["F10"]);
                    //model.DateEnd = TextUtils.ToDate2(dt.Rows[i]["F11"]);
                    //model.ProblemBacklog = TextUtils.ToString(dt.Rows[i]["F12"]);
                    //model.PlanNext = TextUtils.ToString(dt.Rows[i]["F13"]);
                    //model.Result = TextUtils.ToString(dt.Rows[i]["F14"]);
                    //model.Note = TextUtils.ToString(dt.Rows[i]["F15"]);
                    //model.BigAccount = TextUtils.ToBoolean(dt.Rows[i]["F16"]);
                    //model.Content = TextUtils.ToString(dt.Rows[i]["F17"]);
                    //model.Month = TextUtils.ToInt(dt.Rows[i]["F18"]);
                    //model.Year = TextUtils.ToInt(dt.Rows[i]["F19"]);
                    //model.DateStart = TextUtils.ToDate2(dt.Rows[i]["F20"]);
                    DailyReportSaleBO.Instance.Insert(model);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(i.ToString() + Environment.NewLine + ex.ToString());
                }
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show(start.ToString() + " - " + DateTime.Now.ToString(),"Lưu thành công");
            enableControl(false);
        }
    }
}
