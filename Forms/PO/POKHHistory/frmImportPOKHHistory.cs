using BMS.Model;
using BMS.Utils;
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
    public partial class frmImportPOKHHistory : _Forms
    {
        DateTime start;
        DataSet ds = new DataSet();
        public frmImportPOKHHistory()
        {
            InitializeComponent();
        }

        private void frmImportPOKHHistory_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
        }

        private void btnBrowse_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            grvData.Columns.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
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
                cboSheet_SelectionChangeCommitted(null, null);
                btnSave.Enabled = true;
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
        }

        private void btnMauExcel_Click(object sender, EventArgs e)
        {
            try
            {
                FileInfo fi = new FileInfo("MAU_POKH.xlsx");
                if (fi.Exists)
                {
                    System.Diagnostics.Process.Start("MAU_POKH.xlsx");
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
        private bool SaveData()
        {
            string pokhTypeCode = "";
            cboSheet.Invoke(new Action(() => { pokhTypeCode = cboSheet.Text.Trim(); }));
            for (int i = 2; i < grvData.RowCount; i++)
            {
                try
                {
                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i; }));
                    txtRate.Invoke((Action)(() => { txtRate.Text = $"{i}/{grvData.RowCount - 1}"; }));
                    POKHHistoryModel model = new POKHHistoryModel();

                    model.CustomerCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F1"));
                    if (string.IsNullOrWhiteSpace(model.CustomerCode)) continue;
                    model.IndexCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));
                    model.PONumber = TextUtils.ToString(grvData.GetRowCellValue(i, "F3"));
                    model.PODate = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F4"));
                    model.ProductCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F5"));
                    model.Model = TextUtils.ToString(grvData.GetRowCellValue(i, "F6"));
                    model.Quantity = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F7"));
                    model.QuantityDeliver = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F8"));
                    model.QuantityPending = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F9"));
                    model.Unit = TextUtils.ToString(grvData.GetRowCellValue(i, "F10"));
                    model.NetPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F11"));
                    model.UnitPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F12"));
                    model.TotalPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F13"));
                    model.VAT = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F14"));
                    model.TotalPriceVAT = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F15"));
                    model.DeliverDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F18"));
                    model.PaymentDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F120"));
                    model.BillDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F21"));
                    model.BillNumber = TextUtils.ToString(grvData.GetRowCellValue(i, "F22"));
                    model.Dept = TextUtils.ToString(grvData.GetRowCellValue(i, "F23"));
                    model.Sale = TextUtils.ToString(grvData.GetRowCellValue(i, "F24"));
                    model.Pur = TextUtils.ToString(grvData.GetRowCellValue(i, "F25"));
                    model.POTypeCode = pokhTypeCode;

                    SQLHelper<POKHHistoryModel>.Insert(model);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi dữ liệu tại dòng {i + 1} : " + ex.Message);
                    continue;
                }

            }

            return true;
            //}
        }



        void SaveDataPriceRequest()
        {
            for (int i = 1; i < grvData.RowCount; i++)
            {
                try
                {
                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i; }));
                    txtRate.Invoke((Action)(() => { txtRate.Text = $"{i}/{grvData.RowCount - 1}"; }));


                    int stt = TextUtils.ToInt(grvData.GetRowCellValue(i, "F1"));
                    if (stt <= 0) continue;
                    string tt = TextUtils.ToString(grvData.GetRowCellValue(i, "F4"));
                    if (tt == "2.1.9")
                    {
                        var a = 1;
                    }
                    string productCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F6"));
                    int projectID = TextUtils.ToInt(grvData.GetRowCellValue(i, "F15"));
                    int projectPartListID = TextUtils.ToInt(grvData.GetRowCellValue(i, "F16"));

                    var exp1 = new Expression("ProductCode", productCode);
                    //var exp2 = new Expression("ProjectID", projectID);
                    var exp2 = new Expression("ProjectPartListID", projectPartListID);
                    var listModel = SQLHelper<ProjectPartlistPriceRequestModel>.FindByExpression(exp1.And(exp2));
                    foreach (ProjectPartlistPriceRequestModel model in listModel)
                    {
                        //model = model ?? new ProjectPartlistPriceRequestModel();

                        model.UnitPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F22"));
                        model.Note = TextUtils.ToString(grvData.GetRowCellValue(i, "F31"));
                        model.SupplierSaleID = TextUtils.ToInt(grvData.GetRowCellValue(i, "F28"));
                        //model.IsImport = TextUtils.ToBoolean(grvData.GetRowCellValue(i, "15"));
                        model.CurrencyID = TextUtils.ToInt(grvData.GetRowCellValue(i, "F20"));
                        model.HistoryPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F23"));
                        //model.TotalImportPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F24"));
                        model.VAT = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F26"));
                        model.TotalDayLeadTime = TextUtils.ToInt(grvData.GetRowCellValue(i, "F29"));
                        model.CurrencyRate = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F21"));
                        model.TotalPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F24"));
                        model.TotalPrice = model.Quantity * model.UnitPrice;
                        //model.TotalPriceExchange = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F25"));
                        model.TotalPriceExchange = model.TotalPrice * model.CurrencyRate;
                        //model.TotaMoneyVAT = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F27"));
                        model.TotaMoneyVAT = model.TotalPrice + ((model.TotalPrice * model.VAT) / 100);
                        if (model.ID > 0)
                        {
                            SQLHelper<ProjectPartlistPriceRequestModel>.Update(model);
                        }
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi dữ liệu tại dòng {i + 1} : " + ex.Message);
                    continue;
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
        void enableControl(bool enable)
        {
            btnSave.Enabled = enable;
            grdData.Enabled = enable;
            cboSheet.Enabled = enable;
            btnBrowse.Enabled = enable;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            SaveData();
            //SaveDataPriceRequest();

            //ChangeEmployeeNewCode();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                enableControl(true);
                return;
            };

            MessageBox.Show($"Cập nhật thành công!\n{start.ToString()} - {DateTime.Now.ToString()}", "Thông báo");
            enableControl(true);
            this.Close();
        }

        private void ChangeEmployeeNewCode()
        {
            for (int i = 4; i < grvData.RowCount; i++)
            {
                try
                {
                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i; }));
                    txtRate.Invoke((Action)(() => { txtRate.Text = $"{i}/{grvData.RowCount - 1}"; }));
                    int stt = TextUtils.ToInt(grvData.GetRowCellValue(i, "F1"));
                    string empCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F3"));
                    string empOldCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));

                    if (stt <= 0) continue;
                    if (string.IsNullOrWhiteSpace(empOldCode))
                    {
                        //MessageBox.Show($"Mã nv mới tại vị trí KHÔNG được để trống [{empNewCode}] tại dòng:  [{i + 1}]!", "Thông báo");
                        continue;
                    }
                    EmployeeModel empModel = SQLHelper<EmployeeModel>.FindByAttribute("Code", empCode).FirstOrDefault() ?? new EmployeeModel();
                    if (empModel.ID <= 0)
                    {
                        //MessageBox.Show($"Không tìm thấy mã nv [{empCode}] tại dòng:  [{i + 1}]!", "Thông báo");
                        continue;
                    }

                    //empModel.Code = empNewCode;
                    empModel.CodeOld = empOldCode;

                    SQLHelper<EmployeeModel>.Update(empModel);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi dữ liệu tại dòng {i + 1} : " + ex.Message);
                    continue;
                }

            }
        }
    }
}
