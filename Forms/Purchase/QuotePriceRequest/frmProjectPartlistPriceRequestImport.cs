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
using DevExpress.Utils;
using BMS.Utils;

namespace BMS
{
    public partial class frmProjectPartlistPriceRequestImport : _Forms
    {
        DateTime start;
        DataSet ds = new DataSet();


        int _jobRequirementID = 0;

        public frmProjectPartlistPriceRequestImport(int jobRequirementID)
        {
            InitializeComponent();
            _jobRequirementID = jobRequirementID;
        }

        private void frmProjectPartlistPriceRequestImport_Load(object sender, EventArgs e)
        {

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


        bool CheckValidate()
        {
            for (int i = 0; i < grvData.RowCount; i++)
            {
                int stt = TextUtils.ToInt(grvData.GetRowCellValue(i, "F1"));
                if (stt <= 0) continue;
                string productCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));
                string productName = TextUtils.ToString(grvData.GetRowCellValue(i, "F3"));
                DateTime? deadline = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F5"));
                decimal quantity = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F6"));

                if (string.IsNullOrWhiteSpace(productCode))
                {
                    MessageBox.Show($"Vui lòng nhập Mã sản phẩm!\n(Dòng stt: {stt})", "Thông báo");
                    return false;
                }


                if (string.IsNullOrWhiteSpace(productName))
                {
                    MessageBox.Show($"Vui lòng nhập Tên sản phẩm! (Dòng stt: {stt})", "Thông báo");
                    return false;
                }

                if (!deadline.HasValue)
                {
                    MessageBox.Show($"Vui lòng nhập Deadline!\n(Dòng stt: {stt})", "Thông báo");
                    return false;
                }
                else if (!CheckDeadLine(deadline.Value, stt))
                {
                    return false;
                }


                if (quantity <= 0)
                {
                    MessageBox.Show($"Vui lòng nhập SL yêu cầu!\n(Dòng stt: {stt})", "Thông báo");
                    return false;
                }
            }

            return true;
        }


        bool CheckDeadLine(DateTime deadline, int stt)
        {
            //Nếu ngày yêu cầu từ sau 15h, thì bắt đầu tính từ ngày hôm sau
            //Nếu ngày yêu cầu là ngày T7 hoặc CN thì bắt đầu tính từ t2
            //Ngày deadline phải lơn hơn ngày yêu cầu từ 2 ngày trở lên
            //Và không tính T7, CN


            if (Global.IsAdmin) return true;

            TimeSpan time = new TimeSpan(15, 0, 0);
            DateTime dateRequest = DateTime.Now;
            TimeSpan timeRequest = TimeSpan.Parse(dateRequest.ToString("HH:mm:ss"));
            if (timeRequest >= time)
            {
                dateRequest = dateRequest.AddDays(+1);
            }

            if (dateRequest.DayOfWeek == DayOfWeek.Saturday)
            {
                dateRequest = dateRequest.AddDays(+1);
            }

            if (dateRequest.DayOfWeek == DayOfWeek.Sunday)
            {
                dateRequest = dateRequest.AddDays(+1);
            }

            List<DateTime> listDates = new List<DateTime>();
            double totalDays = (deadline.Date - dateRequest.Date).TotalDays;
            for (int i = 0; i <= totalDays; i++)
            {
                var date = dateRequest.AddDays(i).Date;
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    continue;
                }

                if (!listDates.Contains(date))
                {
                    listDates.Add(date);
                }
            }

            if (listDates.Count < 2)
            {
                MessageBox.Show($"Dealine phải ít nhất là 2 ngày tính từ [{dateRequest.ToString("dd/MM/yyyy")}] và KHÔNG tính Thứ 7, Chủ nhật!\n(Dòng stt: {stt})", "Thông báo");
                return false;
            }

            return true;
        }

        bool SaveData()
        {
            int stt = 0;

            try
            {
                if (!CheckValidate()) return false;

                for (int i = 0; i < grvData.RowCount; i++)
                {
                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i; }));
                    txtRate.Invoke((Action)(() => { txtRate.Text = $"{i + 1}/{grvData.RowCount}"; }));

                    stt = TextUtils.ToInt(grvData.GetRowCellValue(i, "F1"));
                    if (stt <= 0) continue;

                    ProjectPartlistPriceRequestModel priceRequest = new ProjectPartlistPriceRequestModel();

                    string productCode = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));

                    var exp1 = new Expression(ProjectPartlistPriceRequestModel_Enum.ProductCode.ToString(), productCode);
                    var exp2 = new Expression(ProjectPartlistPriceRequestModel_Enum.JobRequirementID.ToString(), _jobRequirementID);
                    var exp3 = new Expression(ProjectPartlistPriceRequestModel_Enum.IsDeleted.ToString(), 0);
                    var exp4 = new Expression(ProjectPartlistPriceRequestModel_Enum.StatusRequest.ToString(), 1);

                    var priceRequests = SQLHelper<ProjectPartlistPriceRequestModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4));

                    priceRequest.ProductCode = productCode;
                    priceRequest.ProductName = TextUtils.ToString(grvData.GetRowCellValue(i, "F3"));
                    priceRequest.Maker = TextUtils.ToString(grvData.GetRowCellValue(i, "F4"));
                    priceRequest.Deadline = TextUtils.ToDate5(grvData.GetRowCellValue(i, "F5"));
                    priceRequest.Quantity = TextUtils.ToDecimal(grvData.GetRowCellValue(i, "F6"));
                    priceRequest.Unit = TextUtils.ToString(grvData.GetRowCellValue(i, "F7"));
                    priceRequest.NoteHR = TextUtils.ToString(grvData.GetRowCellValue(i, "F8"));
                    priceRequest.JobRequirementID = _jobRequirementID;
                    priceRequest.IsJobRequirement = _jobRequirementID > 0;
                    priceRequest.StatusRequest = 1;
                    priceRequest.DateRequest = DateTime.Now;
                    priceRequest.EmployeeID = Global.EmployeeID;

                    if (priceRequests.Count > 0) //Update giá trị cho tất cả list này
                    {
                        var myDict = new Dictionary<string, object>()
                        {
                            {ProjectPartlistPriceRequestModel_Enum.ProductCode.ToString(),priceRequest.ProductCode },
                            {ProjectPartlistPriceRequestModel_Enum.ProductName.ToString(),priceRequest.ProductName },
                            {ProjectPartlistPriceRequestModel_Enum.Maker.ToString(),priceRequest.Maker },
                            {ProjectPartlistPriceRequestModel_Enum.Deadline.ToString(),priceRequest.Deadline },
                            {ProjectPartlistPriceRequestModel_Enum.Quantity.ToString(),priceRequest.Quantity },
                            {ProjectPartlistPriceRequestModel_Enum.Unit.ToString(),priceRequest.Unit },
                            {ProjectPartlistPriceRequestModel_Enum.NoteHR.ToString(),priceRequest.Note },
                            {ProjectPartlistPriceRequestModel_Enum.JobRequirementID.ToString(),priceRequest.JobRequirementID },
                            {ProjectPartlistPriceRequestModel_Enum.IsJobRequirement.ToString(),priceRequest.IsJobRequirement },
                            //{ProjectPartlistPriceRequestModel_Enum.StatusRequest.ToString(),productCode },
                            //{ProjectPartlistPriceRequestModel_Enum.DateRequest.ToString(),productCode },
                            {ProjectPartlistPriceRequestModel_Enum.EmployeeID.ToString(),priceRequest.EmployeeID },
                        };


                        string ids = string.Join(",", priceRequests.Select(x => x.ID).ToList());
                        var exp = new Expression(ProjectPartlistPriceRequestModel_Enum.ID, ids, "IN");
                        SQLHelper<ProjectPartlistPriceRequestModel>.UpdateFields(myDict, exp);
                    }
                    else
                    {
                        SQLHelper<ProjectPartlistPriceRequestModel>.Insert(priceRequest);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + $"\nStt: {stt}", "Thông báo");
                return false;
            }
        }

        void enableControl(bool enable)
        {
            btnSaveAndClose.Enabled = enable;
            grdData.Enabled = enable;
            cboSheet.Enabled = enable;
            btnBrowse.Enabled = enable;
        }

        private void btnSaveAndClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
            else
            {
                //if (!CheckValidate()) return;
                progressBar1.Minimum = 0;
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

        private void btnShowTemplate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                string fileName = "TemplateProjectPartlistPriceRequest.xlsx";

                FileInfo fi = new FileInfo(fileName);
                if (fi.Exists)
                {
                    System.Diagnostics.Process.Start(fileName);
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

        private void btnBrowse_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            grvData.Columns.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            ofd.Multiselect = false;
            ofd.Title = "Chọn file excel";

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

                    btnSaveAndClose.Enabled = true;
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

                        btnSaveAndClose.Enabled = true;
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
            //if (!CheckValidate())
            //{
            //    e.Cancel = true;
            //    return;
            //}

            if (!SaveData())
            {
                e.Cancel = true;
                return;
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                enableControl(true);
                return;
            }

            MessageBox.Show($"Cập nhật thành công!\n{start.ToString()} - {DateTime.Now.ToString()}", "Thông báo");

            this.DialogResult = DialogResult.OK;
            //enableControl(true);
        }
    }
}
