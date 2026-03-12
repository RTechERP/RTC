using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
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
    public partial class frmAccountingBillImportExcel : _Forms
    {
        DateTime start;
        DataSet ds;
        public frmAccountingBillImportExcel()
        {
            InitializeComponent();
        }
        private void frmAccountingBillImportExcel_Load(object sender, EventArgs e)
        {

            grvData.OptionsBehavior.Editable = Global.IsAdmin;
            grvData.OptionsBehavior.ReadOnly = !Global.IsAdmin;
        }
        private void btnMauExcel_Click(object sender, EventArgs e)
        {
            try
            {
                FileInfo fi = new FileInfo("HoaDonKeToan.xlsx");
                if (fi.Exists)
                {
                    System.Diagnostics.Process.Start("HoaDonKeToan.xlsx");
                }
                else
                {
                    MessageBox.Show("file doesn't exist", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }
        private static IList<string> GetTablenames(DataTableCollection tables)
        {
            var tableList = new List<string>();
            foreach (var table in tables)
            {
                if (table.ToString().Contains('.')) continue;
                tableList.Add(table.ToString());
            }

            return tableList;
        }
        private void btnBrowse_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
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

                    if (tablenames.Count > 0) cboSheet.SelectedIndex = 0;

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

                grdData.DataSource = ds; // dataset
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
        void enableControl(bool enable)
        {
            btnSave.Enabled = enable;
            grdData.Enabled = enable;
            cboSheet.Enabled = enable;
            btnBrowse.Enabled = enable;
        }
        private bool CheckValidate()
        {
            for (int i = 5; i < grvData.RowCount; i++)
            {
                progressBar1.Invoke((Action)(() => { progressBar1.Value = i; }));
                txtRate.Invoke((Action)(() => { txtRate.Text = $"{i}/{grvData.RowCount - 1}"; }));
                string billNumber = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));
                string STT = TextUtils.ToString(grvData.GetRowCellValue(i, "F1"));
                if (string.IsNullOrWhiteSpace(TextUtils.ToDate5(grvData.GetRowCellValue(i, "F3")).ToString()))
                {
                    MessageBox.Show($"Hóa đơn có STT = [{STT}] KHÔNG có NGÀY HĐ!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }
        private void SaveData()
        {
            try
            {
               
                for (int i = 5; i < grvData.RowCount; i++)
                {
                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i; }));
                    txtRate.Invoke((Action)(() => { txtRate.Text = $"{i}/{grvData.RowCount - 1}"; }));
                    string billNumber = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));
                    string txCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F6"));
                    Expression exx1 = new Expression("Code", billNumber);
                    Expression exx2 = new Expression("IsDeleted", 0);
                    TaxCompanyModel taxCompany = SQLHelper<TaxCompanyModel>.FindByExpression(exx1.And(exx2)).FirstOrDefault();


                    Expression ex1 = new Expression("BillNumber", billNumber);
                    Expression ex2 = new Expression("IsDeleted", 0);
                    AccountingBillModel model = SQLHelper<AccountingBillModel>.FindByExpression(ex1.And(ex2)).FirstOrDefault() ?? new AccountingBillModel();


                    model.BillNumber = billNumber;
                    model.BillDate = TextUtils.ToDate5(grvData.GetRowCellValue(i, "F3"));
                    model.SupplierSale = TextUtils.ToString(grvData.GetRowCellValue(i, "F4"));
                    model.SupplierSaleID = 0;
                    model.TotalMoney = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F5"));
                    model.TaxCompanyID = taxCompany != null ? taxCompany.ID : 0;
                    model.EmployeeStatus = string.IsNullOrWhiteSpace(TextUtils.ToString(grvData.GetRowCellValue(i, "F17"))) ? 0 : 1;
                    model.DeliverTaxStatus = string.IsNullOrWhiteSpace(TextUtils.ToString(grvData.GetRowCellValue(i, "F18"))) ? 0 : 1;
                    model.DeliverTaxDate = TextUtils.ToDate5(grvData.GetRowCellValue(i, "F19"));

                    if (model.ID > 0) SQLHelper<AccountingBillModel>.Update(model);
                    else model.ID = SQLHelper<AccountingBillModel>.Insert(model).ID;

                    List<DocumentImportModel> lstDocument = SQLHelper<DocumentImportModel>.FindByAttribute("IsDeleted",0);
                    List<int> listDocumentID = new List<int>();
                    listDocumentID.Add(lstDocument.FirstOrDefault(p => p.DocumentImportCode.Trim() == "PO").ID);
                    listDocumentID.Add(lstDocument.FirstOrDefault(p => p.DocumentImportCode.Trim() == "PXK").ID);
                    listDocumentID.Add(lstDocument.FirstOrDefault(p => p.DocumentImportCode.Trim() == "BBBG").ID);


                    List<AccountingBillApprovedModel> lst = SQLHelper<AccountingBillApprovedModel>.FindByAttribute("AccountingBillID", model.ID);
                    List<string> lstNoteDocument = new List<string>();
                    lstNoteDocument.Add(TextUtils.ToString(grvData.GetRowCellValue(i, "F8")));
                    lstNoteDocument.Add(TextUtils.ToString(grvData.GetRowCellValue(i, "F10")));
                    lstNoteDocument.Add(TextUtils.ToString(grvData.GetRowCellValue(i, "F12")));
                    if (lst.Count > 0)
                    {
                        foreach (AccountingBillApprovedModel item in lst)
                        {
                            if(item.DocumentImportID == listDocumentID[0]) item.Note = lstNoteDocument[0];
                            if (item.DocumentImportID == listDocumentID[1]) item.Note = lstNoteDocument[1];
                            if (item.DocumentImportID == listDocumentID[2]) item.Note = lstNoteDocument[2];

                            SQLHelper<AccountingBillApprovedModel>.Update(item);
                        }
                    }
                    else
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            AccountingBillApprovedModel newItem = new AccountingBillApprovedModel();
                            newItem.AccountingBillID = model.ID;
                            newItem.Status = 0;
                            newItem.DocumentImportID = listDocumentID[j];
                            newItem.Note = lstNoteDocument[j];
                            SQLHelper<AccountingBillApprovedModel>.Insert(newItem);
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                if (!CheckValidate()) return;
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            SaveData();
            if (Global.DebugFlag)
            {
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show($"Cập nhật thành công!\n{start.ToString()} - {DateTime.Now.ToString()}", "Thông báo");
            enableControl(true);
            this.Close();
        }

        private void frmAccountingBillImportExcel_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
