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
    public partial class frmKPIErrorEmployeeImportExcel : _Forms
    {
        DateTime start;
        DataSet ds = new DataSet();
        public frmKPIErrorEmployeeImportExcel()
        {
            InitializeComponent();
        }

        private void frmKPIErrorEmployeeImportExcel_Load(object sender, EventArgs e)
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
        void enableControl(bool enable)
        {
            btnSave.Enabled = enable;
            grdData.Enabled = enable;
            cboSheet.Enabled = enable;
            btnBrowse.Enabled = enable;
        }
        private void btnMauExcel_Click(object sender, EventArgs e)
        {
            try
            {
                FileInfo fi = new FileInfo("Mau_Nhan_Vien_Vi_Pham.xlsx");
                if (fi.Exists)
                {
                    System.Diagnostics.Process.Start("Mau_Nhan_Vien_Vi_Pham.xlsx");
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
            List<KPIErrorEmployeeModel> lstMaster = new List<KPIErrorEmployeeModel>();

            for (int i = 0; i < grvData.RowCount; i++)
            {
                try
                {

                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i + 1; }));
                    txtRate.Invoke((Action)(() => { txtRate.Text = $"{i + 1}/{grvData.RowCount}"; }));

                    int stt = TextUtils.ToInt(grvData.GetRowCellValue(i, "F1"));
                    if (stt <= 0) continue;

                    string employeeCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F2")).Trim();
                    string kpiErrorCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F4")).Trim();
                    DateTime? errorDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F5"));
                    int errorNumber = TextUtils.ToInt(grvData.GetRowCellValue(i, "F6"));
                    decimal totalMoney = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F7"));
                    string note = TextUtils.ToString(grvData.GetRowCellValue(i, "F8")).Trim();

                    EmployeeModel employee = SQLHelper<EmployeeModel>.FindByAttribute("Code", employeeCode).FirstOrDefault() ?? new EmployeeModel();
                    KPIErrorModel kpiError = SQLHelper<KPIErrorModel>.FindByAttribute("Code", kpiErrorCode).FirstOrDefault() ?? new KPIErrorModel();
                    //if (employee.ID <= 0) continue;
                    //if (kpiError.ID <= 0) continue;
                    //if (!errorDate.HasValue) continue;

                    KPIErrorEmployeeModel model = new KPIErrorEmployeeModel();
                    model.EmployeeID = employee.ID;
                    model.KPIErrorID = kpiError.ID;
                    model.ErrorDate = errorDate;
                    model.ErrorNumber = errorNumber;
                    model.TotalMoney = totalMoney;
                    model.Note = note;
                    SQLHelper<KPIErrorEmployeeModel>.Insert(model);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi dữ liệu tại dòng {i + 1} : " + ex.Message);
                    continue;
                }

            }
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
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            SaveData();
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
    }
}