using BMS.Business;
using BMS.Model;
using BMS.Utils;
using ExcelDataReader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmImportExcelEmployeePayrollBonusDeuction : _Forms
    {
        DateTime start;
        DataSet ds;
        public frmImportExcelEmployeePayrollBonusDeuction()
        {
            InitializeComponent();
        }

        private void frmImportExcelEmployeeCollectMoney_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
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
                    progressBar1.Maximum = grvData.RowCount;
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
            string type = "";
            cboSheet.Invoke((Action)(() => { type = cboSheet.Text; }));
            UpdateBonusDeuction(type);
            //int rowCount = grvData.RowCount;
            ////bool IsAdd;
            //for (int i = 2; i < rowCount; i++)
            //{
            //    try
            //    {
            //        progressBar1.Invoke((Action)(() => { progressBar1.Value = i; }));
            //        txtRate.Invoke((Action)(() => { txtRate.Text = string.Format("{0}/{1}", i, rowCount - 1); }));


            //        string employeeCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F2").ToString());
            //        if (string.IsNullOrEmpty(employeeCode)) continue;
            //        int yearValue = TextUtils.ToInt(grvData.GetRowCellValue(i, "F4"));
            //        int monValue = TextUtils.ToInt(grvData.GetRowCellValue(i, "F5"));
            //        if (yearValue == 0 || monValue == 0) continue;
            //        int employeeID = TextUtils.ToInt(TextUtils.ExcuteScalar($"SELECT TOP 1 ID FROM dbo.Employee WHERE Code = '{employeeCode}'"));
            //        if (employeeID == 0) continue;
            //        EmployeePayrollBonusDeuctionModel model = new EmployeePayrollBonusDeuctionModel();
            //        Expression exp1 = new Expression("EmployeeID", employeeID);
            //        Expression exp2 = new Expression("YearValue", yearValue);
            //        Expression exp3 = new Expression("MonthValue", monValue);
            //        List<EmployeePayrollBonusDeuctionModel> lstModel = SQLHelper<EmployeePayrollBonusDeuctionModel>.FindByExpression(exp1.And(exp2).And(exp3));
            //        if (lstModel.Count > 0)
            //        {
            //            model = lstModel.FirstOrDefault();
            //            //IsAdd = false;
            //        }
            //        //else
            //        //{
            //        //    IsAdd = true;
            //        //}
            //        int STT = TextUtils.ToInt(grvData.GetRowCellValue(i, "F1"));
            //        if (STT <= 0) continue;

            //        model.EmployeeID = employeeID;
            //        model.YearValue = yearValue;
            //        model.MonthValue = monValue;
            //        model.KPIBonus = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F7"));
            //        model.OtherBonus = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F8"));
            //        model.ParkingMoney = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F9"));
            //        model.Punish5S = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F10"));
            //        model.OtherDeduction = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F11"));
            //        model.BHXH = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F12"));
            //        model.SalaryAdvance = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F13"));
            //        model.Note = TextUtils.ToString(grvData.GetRowCellValue(i, "F14"));

            //        if (model.ID > 0)
            //        {
            //            EmployeePayrollBonusDeuctionBO.Instance.Update(model);
            //        }
            //        else
            //        {
            //            EmployeePayrollBonusDeuctionBO.Instance.Insert(model);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Lỗi lưu dữ liệu tại dòng " + i + Environment.NewLine + ex.ToString());
            //    }

            //}
        }
        private void cboSheet_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //grvData.Columns.Clear();
            //try
            //{
            //    MyLib.ShowWaitForm("Loading data ...");
            //    DataTable dt = TextUtils.ExcelToDatatableNoHeader(btnBrowse.Text, cboSheet.SelectedValue.ToString());

            //    grdData.DataSource = dt;
            //    grvData.PopulateColumns();
            //    grvData.BestFitColumns();
            //    grdData.Focus();
            //}
            //catch (Exception ex)
            //{
            //    TextUtils.ShowError(ex);
            //    grdData.DataSource = null;
            //}
            //finally
            //{
            //    MyLib.CloseWaitForm();
            //}

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

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show($"Cập nhật thành công!\n{start.ToString()} - {DateTime.Now.ToString()}", "Thông báo");
            enableControl(true);
            //this.Close();
            cboSheet.Focus();
        }

        private void btnBrowse_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //grvData.Columns.Clear();
            //OpenFileDialog ofd = new OpenFileDialog();
            //if (ofd.ShowDialog() == DialogResult.OK)
            //{
            //    btnBrowse.Text = ofd.FileName;
            //    cboSheet.DataSource = null;
            //    cboSheet.DataSource = TextUtils.ListSheetInExcel(ofd.FileName);
            //    cboSheet_SelectionChangeCommitted(null, null);
            //    btnSave.Enabled = true;
            //}

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
                    //toolStripStatusLabel1.Text = "Elapsed: " + sw.ElapsedMilliseconds.ToString() + " ms (" + openTiming.ToString() + " ms to open)";

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

        private void frmImportExcelEmployeeCollectMoney_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void txtMau_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Path.Combine(Application.StartupPath, "Mau_Khau_Tru_Luong.xlsx");
                if (!File.Exists(path))
                {
                    MessageBox.Show("File không tồn tại!");
                }
                Process.Start(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }



        void UpdateBonusDeuction(string type)
        {
            int i = 0;
            int totalRowSkip = 0;
            DataSet dataSet = (DataSet)grdData.DataSource;
            DataTable dataSource = dataSet.Tables[type];
            foreach (DataRow row in dataSource.Rows)
            {
                i++;
                int stt = TextUtils.ToInt(row["F1"]);
                if (stt <= 0)
                {
                    totalRowSkip++;
                    continue;
                }

                progressBar1.Invoke((Action)(() => { progressBar1.Value = i; }));
                txtRate.Invoke((Action)(() => { txtRate.Text = $"{i}/{dataSource.Rows.Count}"; }));

                string employeeCode = TextUtils.ToString(row["F2"]).Trim();
                EmployeeModel employee = SQLHelper<EmployeeModel>.FindByAttribute("Code", employeeCode).FirstOrDefault();
                if (employee == null)
                {
                    continue;
                }

                int year = TextUtils.ToInt(row["F4"]);
                int month = TextUtils.ToInt(row["F5"]);
                int value = TextUtils.ToInt(row["F6"]);

                var exp1 = new Expression("EmployeeID", employee.ID);
                var exp2 = new Expression("YearValue", year);
                var exp3 = new Expression("MonthValue", month);
                EmployeePayrollBonusDeuctionModel model = SQLHelper<EmployeePayrollBonusDeuctionModel>.FindByExpression(exp1.And(exp2).And(exp3)).OrderByDescending(x => x.ID).FirstOrDefault();
                model = model == null ? new EmployeePayrollBonusDeuctionModel() : model;
                model.EmployeeID = employee.ID;
                model.YearValue = year;
                model.MonthValue = month;

                if (type.ToLower().Trim() == "ThuongKPISDS".ToLower().Trim())
                {
                    model.KPIBonus = value;
                }
                else if (type.ToLower().Trim() == "Thuongkhac".ToLower().Trim())
                {
                    model.OtherBonus = value;
                }
                else if (type.ToLower().Trim() == "Guixe".ToLower().Trim())
                {
                    model.ParkingMoney = value;
                }
                else if (type.ToLower().Trim() == "5s".ToLower().Trim())
                {
                    model.Punish5S = value;
                }
                else if (type.ToLower().Trim() == "Trukhac".ToLower().Trim())
                {
                    model.OtherDeduction = value;
                }
                else if (type.ToLower().Trim() == "Phaithubhxh".ToLower().Trim())
                {
                    model.Insurances = value;
                }
                else if (type.ToLower().Trim() == "Ungluong".ToLower().Trim())
                {
                    model.SalaryAdvance = value;
                }
                else if (type.ToLower().Trim() == "cong".ToLower().Trim())
                {
                    model.TotalWorkDay = value;
                }

                if (model.ID > 0)
                {
                    SQLHelper<EmployeePayrollBonusDeuctionModel>.Update(model);
                }
                else
                {
                    SQLHelper<EmployeePayrollBonusDeuctionModel>.Insert(model);
                }
            }
        }
    }
}
