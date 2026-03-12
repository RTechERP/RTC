using BMS.Business;
using BMS.Model;
using BMS.Utils;
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
    public partial class frmTradePriceImportExcel : _Forms
    {
        public TradePriceModel tradePrice = new TradePriceModel();
        DateTime start;
        DataSet ds;
        public frmTradePriceImportExcel()
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

                    if (tablenames.Count > 0)
                        cboSheet.SelectedIndex = 0;

                    btnSave.Enabled = true;
                    var tablename = cboSheet.SelectedItem.ToString();

                    grdData.DataSource = ds; // dataset
                    grdData.DataMember = tablename;
                    grvData.Columns["F1"].Width = 200;
                    grvData.Columns["F1"].AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
                    for(int i = 31; i < grvData.Columns.Count; i++)
                    { 
                        grvData.Columns.Remove(grvData.Columns["F"+i]);
                    }

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


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            UpdateTradePrice();
        }
        void UpdateTradePrice()
        {
            try
            {
                Regex regex = new Regex(@"^-?[\d\.]+$");
                string projectCode = TextUtils.ToString(grvData.GetRowCellValue(0, "F1")).Trim();
                ProjectModel project = SQLHelper<ProjectModel>.FindByAttribute("ProjectCode", projectCode).FirstOrDefault();
                tradePrice.ProjectID = project.ID;
                tradePrice.CustomerID = project.CustomerID;
                if (tradePrice.ID > 0)
                {
                    SQLHelper<TradePriceModel>.Update(tradePrice);
                }
                else
                {
                    tradePrice.ID = SQLHelper<TradePriceModel>.Insert(tradePrice).ID;
                }
                for (int i = 4; i < grvData.RowCount; i++)
                {
                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i; }));
                    txtRate.Invoke((Action)(() => { txtRate.Text = $"{i}/{grvData.RowCount - 1}"; }));

                    string stt = TextUtils.ToString(grvData.GetRowCellValue(i, "F2")).Trim();
                    if (string.IsNullOrEmpty(stt)) continue;
                    if (!regex.IsMatch(stt)) continue;

                    TradePriceDetailModel detail = new TradePriceDetailModel();

                    detail.TradePriceID = tradePrice.ID;
                    detail.STT = stt;
                    detail.ParentID = GetParentId(stt, tradePrice.ID);
                    detail.Maker = TextUtils.ToString(grvData.GetRowCellValue(i, "F3")).Trim();
                    string productCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F4")).Trim();
                    ProductSaleModel product = SQLHelper<ProductSaleModel>.FindByAttribute("ProductCode", productCode).FirstOrDefault();
                    detail.ProductID = product != null ? product.ID : 0;
                    detail.Quantity = TextUtils.ToInt(grvData.GetRowCellValue(i, "F7"));
                    detail.Unit = TextUtils.ToString(grvData.GetRowCellValue(i, "F8")).Trim();
                    detail.UnitImportPriceUSD = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F9"));
                    detail.TotalImportPriceUSD = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F10"));
                    detail.UnitImportPriceVND = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F11"));
                    detail.TotalImportPriceVND = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F12"));
                    detail.BankCharge = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F13"));
                    detail.ProtectiveTariff = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F14"));
                    detail.ProtectiveTariffPerPcs = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F15"));
                    detail.TotalProtectiveTariff = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F16"));
                    detail.OrtherFees = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F17"));
                    detail.CustomFees = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F18"));
                    detail.TotalImportPriceIncludeFees = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F19"));
                    detail.UnitPriceIncludeFees = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F20"));
                    detail.CMPerSet = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F21"));
                    detail.UnitPriceExpectCustomer = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F22"));
                    detail.TotalPriceExpectCustomer = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F23"));
                    detail.Profit = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F24"));
                    detail.ProfitPercent = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F25"));
                    detail.LeadTime = TextUtils.ToString(grvData.GetRowCellValue(i, "F26")).Trim();
                    detail.TotalPriceLabor = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F27"));
                    detail.TotalPriceRTCVision = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F28"));
                    detail.TotalPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F29"));
                    detail.UnitPricePerCOM = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F30"));
                    detail.Note = TextUtils.ToString(grvData.GetRowCellValue(i, "F31")).Trim();

                    if (detail.ID > 0)
                    {
                        SQLHelper<TradePriceDetailModel>.Update(detail);
                    }
                    else
                    {
                        SQLHelper<TradePriceDetailModel>.Insert(detail);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        int GetParentId(string tt, int tradePriceID)
        {
            int parentId = 0;
            if (!tt.Contains(".")) return parentId;

            string parentTt = tt.Substring(0, tt.LastIndexOf(".")).Trim();

            var exp1 = new Expression("STT", parentTt);
            var exp2 = new Expression("TradePriceID", tradePriceID);
            TradePriceDetailModel parent = SQLHelper<TradePriceDetailModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
            if (parent != null && parent.ID > 0)
            {
                parentId = parent.ID;
            }
            return parentId;
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
            MessageBox.Show($"Cập nhật thành công!\n{start.ToString()} - {DateTime.Now.ToString()}", "Thông báo", MessageBoxButtons.OK);
            enableControl(true);
            this.DialogResult = DialogResult.OK;
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
        bool validate()
        {
            return true;
        }

        private void btnMauExcel_Click(object sender, EventArgs e)
        {
            try
            {
                FileInfo fi = new FileInfo("TinhGiaThuongMai.xlsx");
                if (fi.Exists)
                {
                    System.Diagnostics.Process.Start("TinhGiaThuongMai.xlsx");
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
    }
}