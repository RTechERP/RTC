using BMS;
using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.XtraEditors;
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
    public partial class frmInputExcelEmployeeAttendance : _Forms
    {
        public frmInputExcelEmployeeAttendance()
        {
            InitializeComponent();
        }
        void enableControl(bool enable)
        {
            btnSave.Enabled = enable;
            grdData.Enabled = enable;
            cboSheet.Enabled = enable;
            btnBrowse.Enabled = enable;
        }
        DateTime start;
        DataSet ds;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
            else
            {
                if (grvData.RowCount == 0)
                {
                    MessageBox.Show(String.Format("Bạn chưa chọn đường dẫn file hoặc tên sheet. Vui lòng chọn và thử lại!"), TextUtils.Caption);
                    return;
                }
                else if (string.IsNullOrEmpty(cboDepartment.Text.Trim()))
                {
                    MessageBox.Show(String.Format("Vui lòng chọn Văn phòng!"), TextUtils.Caption);
                    return;
                }
                else
                {
                    //Xoá hết dữ  liệu cũ theo ngày
                    //var expressionYear = new Utils.Expression("YEAR(AttendanceDate)", dtpDateStart.Value.Year);
                    //var expressionMonth = new Utils.Expression("MONTH(AttendanceDate)", dtpDateStart.Value.Month);
                    //var expressionDay = new Utils.Expression("DAY(AttendanceDate)", dtpDateStart.Value.Day);

                    DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day, 0, 0, 0);
                    DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59);
                    int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
                    string department = "";
                    if (departmentID == 0)
                    {
                        List<DepartmentModel> listDepartment = (List<DepartmentModel>)cboDepartment.Properties.DataSource;
                        department = string.Join(";", listDepartment.Select(x=>x.ID));
                    }

                    //var exp1 = new Expression("AttendanceDate", dateStart.ToString("yyyy-MM-dd HH:mm:ss"), ">=");
                    //var exp2 = new Expression("AttendanceDate", dateEnd.ToString("yyyy-MM-dd HH:mm:ss"), "<=");

                    //EmployeeAttendanceBO.Instance.DeleteByExpression(expressionYear.And(expressionMonth).And(expressionDay));
                    //EmployeeAttendanceBO.Instance.DeleteByExpression(exp1.And(exp2));

                    List<EmployeeAttendanceModel> list = SQLHelper<EmployeeAttendanceModel>.ProcedureToList("spGetEmployeeAttendanceByDepartment",
                                                new string[] { "@DateStart", "@DateEnd", "@DepartmentID", "@Department" },
                                                new object[] { dateStart, dateEnd, departmentID, department });

                    if (list.Count > 0)
                    {
                        DialogResult dialog = MessageBox.Show($"Đã tồn tại danh sách vân tay của [{cboDepartment.Text}].\nBạn có muốn ghi đè không?","Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialog == DialogResult.Yes)
                        {
                            SQLHelper<EmployeeAttendanceModel>.DeleteListModel(list);
                        }
                        else
                        {
                            return;
                        }
                    }

                    progressBar1.Minimum = 1;
                    progressBar1.Maximum = grvData.RowCount;
                    txtRate.Text = "";
                    start = DateTime.Now;
                    enableControl(false);
                    backgroundWorker1.RunWorkerAsync();
                }
            }
        }

        private void cboSheet_SelectionChangeCommitted(object sender, EventArgs e)
        {
            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát", ""))
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
                    //toolStripStatusLabel1.Text = "Elapsed: " + sw.ElapsedMilliseconds.ToString() + " ms (" + openTiming.ToString() + " ms to open)";

                    var tablenames = GetTablenames(ds.Tables);

                    cboSheet.DataSource = tablenames;

                    if (tablenames.Count > 0)
                        cboSheet.SelectedIndex = 0;

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
                        //toolStripStatusLabel1.Text = "Elapsed: " + sw.ElapsedMilliseconds.ToString() + " ms (" + openTiming.ToString() + " ms to open)";

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
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int rowCount = grvData.RowCount;
            int colCount = grvData.Columns.Count;

            DateTime dateFix = new DateTime(2023, 12, 1, 0, 0, 0);

            for (int i = 0; i < rowCount; i++)
            {
                int stt = TextUtils.ToInt(grvData.GetRowCellValue(i, "F1"));
                if (stt <= 0)
                {
                    continue;
                }

                try
                {
                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i + 1; }));
                    txtRate.Invoke((Action)(() => { txtRate.Text = string.Format("{0}/{1}", i, rowCount - 1); }));
                    EmployeeAttendanceModel model = new EmployeeAttendanceModel();
                    if (stt > 0)
                    {
                        //var checkInValue = grvData.GetRowCellValue(i, "F9");
                        model.STT = stt;
                        //model.IDChamCongMoi = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));
                        model.AttendanceDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F5"));
                        model.DayWeek = TextUtils.ToString(grvData.GetRowCellValue(i, "F6"));
                        //model.Interval = TextUtils.ToString(grvData.GetRowCellValue(i, "F7"));
                        model.Interval = "(00:00:00-23:59:00)";

                        DateTime? datetimeCheckIn = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F7"));
                        DateTime? datetimeCheckOut = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F8"));
                        //model.CheckIn = TextUtils.ToString(grvData.GetRowCellValue(i, "F7"));
                        //model.CheckOut = TextUtils.ToString(grvData.GetRowCellValue(i, "F8"));

                        model.CheckIn = datetimeCheckIn.HasValue ? datetimeCheckIn.Value.ToString("HH:mm") : "";
                        model.CheckOut = datetimeCheckOut.HasValue ? datetimeCheckOut.Value.ToString("HH:mm") : "";

                        string datetimeCheckin = $"{model.AttendanceDate.Value.ToString("yyyy-MM-dd")} {model.CheckIn}";
                        string datetimeCheckout = $"{model.AttendanceDate.Value.ToString("yyyy-MM-dd")} {model.CheckOut}";

                        model.CheckInDate = TextUtils.ToDate4(datetimeCheckin);
                        model.CheckOutDate = TextUtils.ToDate4(datetimeCheckout);

                        DateTime timeStartMorning = new DateTime(model.AttendanceDate.Value.Year, model.AttendanceDate.Value.Month, model.AttendanceDate.Value.Day, 8, 1, 0);
                        DateTime timeEndMorning = new DateTime(model.AttendanceDate.Value.Year, model.AttendanceDate.Value.Month, model.AttendanceDate.Value.Day, 12, 0, 0);

                        DateTime timeStartAffternoon = new DateTime(model.AttendanceDate.Value.Year, model.AttendanceDate.Value.Month, model.AttendanceDate.Value.Day, 13, 31, 0);
                        DateTime timeEndAffternoon = new DateTime(model.AttendanceDate.Value.Year, model.AttendanceDate.Value.Month, model.AttendanceDate.Value.Day, 17, 30, 0);

                        double timeSpanFix = (model.AttendanceDate.Value - dateFix).TotalDays;
                        //EmployeeModel employee = SQLHelper<EmployeeModel>.FindByAttribute("IDChamCongMoi", model.IDChamCongMoi.Trim()).FirstOrDefault();

                        string code = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));
                        EmployeeModel employee = SQLHelper<EmployeeModel>.FindByAttribute(EmployeeModel_Enum.Code.ToString(), code).FirstOrDefault() ?? new EmployeeModel();
                        if ((employee.ID > 0 && employee.DepartmentID == 11) && timeSpanFix >= 0)
                        {
                            timeStartAffternoon = new DateTime(model.AttendanceDate.Value.Year, model.AttendanceDate.Value.Month, model.AttendanceDate.Value.Day, 13, 01, 0);
                            timeEndAffternoon = new DateTime(model.AttendanceDate.Value.Year, model.AttendanceDate.Value.Month, model.AttendanceDate.Value.Day, 17, 0, 0);
                        }

                        model.IDChamCongMoi = employee.IDChamCongMoi;

                        bool isWorkMorning = false;
                        bool isWorkAffternoon = false;

                        if (model.CheckInDate.HasValue && model.CheckOutDate.HasValue)
                        {


                            //if (model.CheckInDate.Value <= timeStartMorning && model.CheckOutDate.Value >= timeEndAffternoon)
                            //{
                            //    model.TotalHour = 8;
                            //}
                            //else
                            //{
                            //    decimal hourLunch = (decimal)(timeStartAffternoon.AddMinutes(-1) - timeEndMorning).TotalHours;
                            //    model.TotalHour = TextUtils.ToDecimal((model.CheckOutDate.Value - model.CheckInDate.Value).TotalHours) - hourLunch;
                            //}


                            decimal timeSpanIn = TextUtils.ToDecimal((model.CheckInDate.Value - timeStartMorning).TotalMinutes);
                            decimal timeSpanOut = TextUtils.ToDecimal((model.CheckOutDate.Value - timeEndAffternoon).TotalMinutes);
                            //Check thời gian làm buổi sáng
                            if (timeSpanIn <= 60)
                            {
                                model.IsLate = timeSpanIn > 0 ? true : false;
                                model.TimeLate = timeSpanIn > 0 ? timeSpanIn : 0;

                                isWorkMorning = true;
                            }
                            else
                            {
                                timeSpanIn = TextUtils.ToDecimal((model.CheckInDate.Value - timeStartAffternoon).TotalMinutes);
                                if (timeSpanIn <= 60)
                                {
                                    model.IsLate = timeSpanIn > 0 ? true : false;
                                    model.TimeLate = timeSpanIn;

                                    isWorkAffternoon = true;
                                }
                            }

                            //Check thời gian làm buổi chiều
                            if (timeSpanOut >= -60)
                            {
                                model.IsEarly = timeSpanOut < 0 ? true : false;
                                model.TimeLate = timeSpanOut < 0 ? Math.Abs(timeSpanOut) : 0;

                                isWorkAffternoon = true;
                            }
                            else
                            {
                                timeSpanOut = TextUtils.ToDecimal((model.CheckOutDate.Value - timeEndMorning).TotalMinutes);
                                if (timeSpanOut >= -60)
                                {
                                    model.IsEarly = timeSpanOut < 0 ? true : false;
                                    model.TimeLate = timeSpanOut < 0 ? Math.Abs(timeSpanOut) : 0;

                                    isWorkMorning = true;
                                }
                            }
                        }
                        

                        if (isWorkMorning && isWorkAffternoon)
                        {
                            decimal hourLunch = (decimal)(timeStartAffternoon.AddMinutes(-1) - timeEndMorning).TotalHours;

                            model.TotalHour = 0;
                            if (model.CheckOutDate.HasValue && model.CheckInDate.HasValue)
                            {
                                model.TotalHour = TextUtils.ToDecimal((model.CheckOutDate.Value - model.CheckInDate.Value).TotalHours) - hourLunch;
                            }

                            
                            model.TotalDay = model.TotalHour >= 6 ? 1 : 0;
                        }
                        else
                        {
                            model.TotalHour = 0;
                            if (model.CheckOutDate.HasValue && model.CheckInDate.HasValue)
                            {
                                model.TotalHour = TextUtils.ToDecimal((model.CheckOutDate.Value - model.CheckInDate.Value).TotalHours);
                            }
                            model.TotalDay = model.TotalHour >= 3 ? 0.5M : 0;
                        }

                        model.IsLunch = (model.TotalHour >= 6 && model.TotalDay > 0.5M) ? true : false;

                        //model.EmployeeID = TextUtils.ToInt(TextUtils.ExcuteScalar($"SELECT TOP 1 ID FROM dbo.Employee WHERE IDChamCongMoi = '{TextUtils.ToString(grvData.GetRowCellValue(i, "F2"))}'"));
                        //model.CreatedDate = DateTime.Now;
                        //model.UpdatedDate = DateTime.Now;



                        //string checkIn = TextUtils.ToString(grvData.GetRowCellValue(i, "F9"));
                        //string checkOut = TextUtils.ToString(grvData.GetRowCellValue(i, "F10"));

                        //if (checkIn.Contains("-") || checkOut.Contains("-"))
                        //{
                        //    model.TotalDay = 0;

                        //    model.CheckIn = checkIn;
                        //    model.CheckOut = checkOut;
                        //}
                        //else
                        //{
                        //    model.CheckIn = TextUtils.ToDate5(grvData.GetRowCellValue(i, "F9")).ToString("HH:mm:ss");
                        //    model.CheckOut = TextUtils.ToDate5(grvData.GetRowCellValue(i, "F10")).ToString("HH:mm:ss");

                        //    DateTime CheckIn = DateTime.ParseExact((model.AttendanceDate.Value.ToString("yyyy-MM-dd") + ' ' + model.CheckIn), "yyyy-MM-dd H:mm:ss", CultureInfo.InvariantCulture);
                        //    DateTime CheckOut = DateTime.ParseExact((model.AttendanceDate.Value.ToString("yyyy-MM-dd") + ' ' + model.CheckOut), "yyyy-MM-dd H:mm:ss", CultureInfo.InvariantCulture);

                        //    TimeSpan durationCheckIn = CheckIn - timeStartMorning;
                        //    TimeSpan durationCheckOut = timeEndMorning - CheckOut;

                        //    durationCheckIn = CheckIn - timeStartMorning;
                        //    durationCheckOut = timeEndAffternoon - CheckOut;

                        //    var timeIn = durationCheckIn.TotalHours;
                        //    var timeOut = durationCheckOut.TotalHours;

                        //    bool timeWorking = false; // Check thời gian làm nửa ngày
                        //    if (timeIn >= 4)
                        //    {
                        //        //MessageBox.Show("Làm ca chiều!");
                        //        durationCheckIn = CheckIn - timeStartAffternoon;
                        //    }
                        //    else if (timeOut >= 4)
                        //    {
                        //        //MessageBox.Show("Làm ca sáng!");
                        //        durationCheckOut = timeEndMorning - CheckOut;
                        //    }
                        //    else
                        //    {
                        //        //MessageBox.Show("Làm cả ngày");
                        //        timeWorking = true;
                        //    }

                        //    timeIn = durationCheckIn.TotalMinutes;
                        //    timeOut = durationCheckOut.TotalMinutes;
                        //    if (timeIn < 1) //khánh
                        //    {
                        //        timeIn = 0;
                        //    }

                        //    model.IsLate = timeIn > 0 ? true : false;
                        //    model.IsEarly = timeOut > 0 ? true : false;
                        //    var timeReal = (timeWorking ? 8 : 4) - (durationCheckIn.TotalHours <= 0 ? 0 : durationCheckIn.TotalHours) - (durationCheckOut.TotalHours <= 0 ? 0 : durationCheckOut.TotalHours);
                        //    model.TotalHour = (decimal)timeReal;
                        //    model.IsLunch = model.TotalHour >= 6 ? true : false;
                        //    decimal TimeLate = model.IsLate == true ? (decimal)timeIn : 0;
                        //    decimal TimeEarly = model.IsEarly == true ? (decimal)timeOut : 0;

                        //    model.TimeEarly = TimeEarly;
                        //    model.TimeLate = TimeLate;

                        //    if (!timeWorking)
                        //    {
                        //        model.TotalDay = (TimeLate + TimeEarly) > 60 ? 0 : 0.5M;
                        //    }
                        //    else
                        //    {
                        //        //Khánh update cái nhẹ 04/08/2023
                        //        if ((TimeLate + TimeEarly) <= 120 && TimeLate <=60 && TimeEarly <=60)
                        //        {
                        //            model.TotalDay = 1;
                        //        }
                        //        else
                        //        {
                        //            model.TotalDay = 0;
                        //        }
                        //        //else if ((TimeLate + TimeEarly) > 120)
                        //        //{
                        //        //    model.TotalDay = 0;
                        //        //}
                        //    }


                        //    int workSaturday = -1;
                        //    if (model.AttendanceDate.Value.DayOfWeek == DayOfWeek.Saturday)
                        //    {
                        //        workSaturday = TextUtils.ToInt(TextUtils.ExcuteScalar($"SELECT Status FROM dbo.EmployeeScheduleWork WHERE WorkYear = {model.AttendanceDate.Value.Year} AND WorkMonth = {model.AttendanceDate.Value.Month} AND  WorkDay = {model.AttendanceDate.Value.Day} "));
                        //    }

                        //    if ((model.TotalDay > 0 && workSaturday == 0) || (model.AttendanceDate.Value.DayOfWeek == DayOfWeek.Sunday && model.TotalDay > 0))
                        //    {
                        //        model.Note = "Làm thêm";
                        //    }

                        //    model.CheckInDate = CheckIn;
                        //    model.CheckOutDate = CheckOut;
                        //}
                    }

                    EmployeeAttendanceBO.Instance.Insert(model);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(i.ToString() + Environment.NewLine + ex.ToString());
                }
            }

        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Cập nhật thành công!\n" + start.ToString() + " - " + DateTime.Now.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            enableControl(true);
        }

        private void mnuMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void frmInputExcelEmployeeAttendance_Load(object sender, EventArgs e)
        {
            LoadDepartment();
        }

        void LoadDepartment()
        {
            List<DepartmentModel> list = SQLHelper<DepartmentModel>.FindAll().Where(x => x.ID > 10).ToList();
            list.Add(new DepartmentModel()
            {
                Code = "HN",
                Name = "Văn phòng Hà Nội"
            });

            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.Properties.DataSource = list;
        }

        private void btnBrowse_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}