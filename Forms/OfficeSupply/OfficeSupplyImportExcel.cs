using BMS.Model;
using BMS.Business;
using BMS.Utils;
using DevExpress.XtraEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using ExcelDataReader;
using System.Windows.Forms;

namespace BMS
{
    public partial class OfficeSupplyImportExcel : _Forms
    {
        public OfficeSupplyImportExcel()
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
            btnSave.Enabled = false;
        }

        private void cboSheet_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboSheet.SelectedItem.ToString() == grdData.DataMember)
            {
                return;
            }
            grvData.Columns.Clear();
            if (chkAutoCheck.Checked)
            {
                try
                {
                    var tablename = cboSheet.SelectedItem.ToString();

                    grdData.DataSource = ds; // dataset
                    grdData.DataMember = tablename;
                    btnSave.Enabled = true;
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
                    btnSave.Enabled = true;
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
            if (Worker.IsBusy)
            {
                Worker.CancelAsync();
            }
            else
            {
                progressBar1.Minimum = 1;
                progressBar1.Maximum = grvData.RowCount - 1;
                txtRate.Text = "";
                start = DateTime.Now;
                enableControl(false);
                Worker.RunWorkerAsync();
            }
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                int rowCount = grvData.RowCount;
                int colCount = grvData.Columns.Count;
                var listRequest = new List<OfficeSupplyRequestModel>();
                for (int i = 1; i < rowCount; i++)
                {
                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i; }));
                    txtRate.Invoke((Action)(() => { txtRate.Text = string.Format("{0}/{1}", i, rowCount - 1); }));
                    OfficeSupplyRequestModel rq = new OfficeSupplyRequestModel();
                    if (grvData.GetRowCellValue(i, "F2").ToString() != "" && grvData.GetRowCellValue(i, "F3").ToString() != "")
                    {
                        string received = grvData.GetRowCellValue(i, "F9").ToString();
                        string exceedsLimit = grvData.GetRowCellValue(i, "F7").ToString();
                        object irrelevantColumn = grvData.GetRowCellValue(i, "F10");
                        if (irrelevantColumn != null) throw new Exception("Dữ liệu excel không đúng format");

                        string CodeNV = TextUtils.ToString(grvData.GetRowCellValue(i, "F1").ToString());
                        string CodeRTC = TextUtils.ToString(grvData.GetRowCellValue(i, "F2").ToString());
                        int quantity = TextUtils.ToInt(grvData.GetRowCellValue(i, "F3").ToString());
                        int quantityReceived = TextUtils.ToInt(grvData.GetRowCellValue(i, "F4").ToString());
                        string note = TextUtils.ToString(grvData.GetRowCellValue(i, "F5").ToString());
                        DateTime dateRequest = TextUtils.ToDate5(grvData.GetRowCellValue(i, "F6").ToString());
                        bool isExceededLimit = TextUtils.ToBoolean(exceedsLimit) || exceedsLimit == "x" || exceedsLimit == "1";
                        string reason = TextUtils.ToString(grvData.GetRowCellValue(i, "F8").ToString());
                        bool isReceived = TextUtils.ToBoolean(received) || received == "x" || received == "1";

                        List<EmployeeModel> employees = SQLHelper<EmployeeModel>.FindByAttribute("Code", CodeNV);
                        List<OfficeSupplyModel> supplies = SQLHelper<OfficeSupplyModel>.FindByAttribute("CodeRtc", CodeRTC);
                        if (employees.Count == 0 || supplies.Count == 0) throw new Exception("Dữ liệu người dùng/ vpp sai");
                        var employee = employees[0];
                        var os = supplies[0];

                        rq.UserID = TextUtils.ToInt(employee.UserID);
                        rq.OfficeSupplyID = os.ID;
                        rq.Quantity = quantity;
                        rq.QuantityReceived = quantityReceived;
                        rq.Note = note;
                        rq.UserIDReceive = isReceived ? TextUtils.ToInt(employee.UserID) : 0;
                        rq.DateRequest = dateRequest;
                        rq.ExceedsLimit = isExceededLimit;
                        rq.Reason = isExceededLimit ? reason : "";

                        listRequest.Add(rq);
                    }
                }
                foreach (OfficeSupplyRequestModel rq in listRequest)
                {
                    SQLHelper<OfficeSupplyRequestModel>.Insert(rq);
                }
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //MessageBox.Show(start.ToString() + " - " + DateTime.Now.ToString());
            enableControl(true);
        }
    }
}