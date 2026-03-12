using BMS.Business;
using BMS.Model;
using ExcelDataReader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmFingerprintExcel : _Forms
    {
        EmployeeFingerprintMasterModel finmt = new EmployeeFingerprintMasterModel();
        public frmFingerprintExcel()
        {
            InitializeComponent();
        }

        private void frmFingerprintExcel_Load(object sender, EventArgs e)
        {
            nbrYear.Value = DateTime.Now.Year;
            nbrMonth.Value = DateTime.Now.Month;
        }
        void enableControl(bool enable)
        {
            btnSave.Enabled = enable;
            grdData.Enabled = enable;
            cboSheet.Enabled = enable;
            btnBrowse.Enabled = enable;
            nbrYear.Enabled = enable;
            nbrMonth.Enabled = enable;
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
                    MessageBox.Show(String.Format("Bạn chưa chọn đường dẫn file hoặc tên sheet.\nVui lòng chọn và thử lại!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
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
                cboSheet.DataSource = null;
                cboSheet.DataSource = TextUtils.ListSheetInExcel(ofd.FileName);
                cboSheet_SelectionChangeCommitted(null, null);
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

        int IDFingerprintMaster = 0;
        bool IsBrowser = false;
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int rowCount = grvData.RowCount;
            int colCount = grvData.Columns.Count;
            int month = TextUtils.ToInt(nbrMonth.Value);
            int year = TextUtils.ToInt(nbrYear.Value);
            string sql = $"select TOP 1 * from dbo.EmployeeFingerprintMaster Where [month] = {month} and [year] = {year}";
            finmt.Year = TextUtils.ToInt(nbrYear.Value);
            finmt.Month = TextUtils.ToInt(nbrMonth.Value);
            finmt.Note = "Bảng chấm công tháng " + month + " năm " + year;
            DataTable dt = TextUtils.Select(sql);
            if (dt.Rows.Count == 0)
            {
                finmt.ID = (int)EmployeeFingerprintMasterBO.Instance.Insert(finmt);
                IDFingerprintMaster = finmt.ID;
            }
            else
            {
                IsBrowser = TextUtils.ToBoolean(dt.Rows[0]["IsBrowser"]);
                if (IsBrowser == true)
                {
                    frmFingerprintExcel frm = new frmFingerprintExcel();
                    if (MessageBox.Show(string.Format("Tháng {0} của năm {1} đã được duyệt. Vui lòng hủy duyệt trước khi update", month, year), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        frm.Close();
                        return;
                    }
                    else
                        return;
                }
                else
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (MessageBox.Show(string.Format("Tháng {0} của năm {1} đã tồn tại, bạn có muốn update không ?", month, year), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            IDFingerprintMaster = TextUtils.ToInt(dt.Rows[0]["ID"]);
                            int ID = TextUtils.ToInt(IDFingerprintMaster);
                            EmployeeFingerprintBO.Instance.DeleteByAttribute("IDFingerprintMaster", ID);
                        }
                        else
                            return;
                    }
                }
            }
            for (int i = 5; i < rowCount; i++)
            {
                try
                {
                    progressBar1.Invoke((Action)(() => { progressBar1.Value = i - 1; }));
                    txtRate.Invoke((Action)(() => { txtRate.Text = string.Format("{0}/{1}", i, rowCount + 1); }));

                    EmployeeFingerprintModel model = new EmployeeFingerprintModel();
                    string value = TextUtils.ToString(grvData.GetRowCellValue(i, "F2")).Trim();
                    if (!string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, "F2")).Trim()))
                    {
                        DateTime? dateNull = null;
                        DateTime? TimeCheckIn = string.IsNullOrEmpty(TextUtils.ToString(grvData.GetRowCellValue(i, "F8")).Trim()) ? null : TextUtils.ToDate4(grvData.GetRowCellValue(i, "F8"));
                        DateTime? TimeCheckOut = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F9"));



                        model.IDFingerprintMaster = IDFingerprintMaster;
                        model.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, "F1"));
                        model.IDChamCong = TextUtils.ToString(grvData.GetRowCellValue(i, "F2"));
                        model.UserID = 0;
                        model.Organization = TextUtils.ToString(grvData.GetRowCellValue(i, "F4")); ;
                        model.Day = TextUtils.ToDate4(grvData.GetRowCellValue(i, "F5"));
                        model.DayOfWeek = TextUtils.ToString(grvData.GetRowCellValue(i, "F6"));
                        model.Period = TextUtils.ToString(grvData.GetRowCellValue(i, "F7"));
                        model.CheckIn = TimeCheckIn.HasValue == false ? dateNull : new DateTime(model.Day.Value.Year, model.Day.Value.Month, model.Day.Value.Day, TimeCheckIn.Value.Hour, TimeCheckIn.Value.Minute, 0);
                        model.CheckOut = TimeCheckOut.HasValue == false ? dateNull : new DateTime(model.Day.Value.Year, model.Day.Value.Month, model.Day.Value.Day, TimeCheckOut.Value.Hour, TimeCheckOut.Value.Minute, 0);

                        if (model.CheckIn.HasValue && model.CheckOut.HasValue)
                        {
                            model.TotalTime = TextUtils.ToDecimal((model.CheckOut.Value - model.CheckIn.Value).TotalHours);
                        }
                        else
                        {
                            model.WorkTime = 0;
                        }


                        DateTime timeStartMorning = new DateTime(model.Day.Value.Year, model.Day.Value.Month, model.Day.Value.Day, 8, 0, 0);
                        DateTime timeEndMorning = new DateTime(model.Day.Value.Year, model.Day.Value.Month, model.Day.Value.Day, 12, 0, 0);

                        DateTime timeStartAffternoon = new DateTime(model.Day.Value.Year, model.Day.Value.Month, model.Day.Value.Day, 13, 30, 0);
                        DateTime timeEndAffternoon = new DateTime(model.Day.Value.Year, model.Day.Value.Month, model.Day.Value.Day, 17, 30, 0);

                        if (!model.CheckIn.HasValue || !model.CheckOut.HasValue)
                        {
                            model.WorkTime = 0;
                        }
                        else
                        {
                            TimeSpan durationCheckIn = model.CheckIn.Value - timeStartMorning;
                            TimeSpan durationCheckOut = timeEndMorning - model.CheckOut.Value;

                            //TÍNH CÔNG NGÀY THỨ BẢY
                            if (model.Day.Value.DayOfWeek == DayOfWeek.Saturday)
                            {
                                durationCheckIn = model.CheckIn.Value - timeStartMorning;
                                durationCheckOut = timeEndMorning - model.CheckOut.Value;

                                model.IsLate = durationCheckIn.TotalMinutes > 0 ? true : false;
                                model.IsEarly = durationCheckOut.TotalMinutes > 0 ? true : false;

                                model.SumLate = model.IsLate == true ? (decimal)durationCheckIn.TotalMinutes : 0;
                                model.SumEarly = model.IsEarly == true ? (decimal)durationCheckOut.TotalMinutes : 0;
                                model.IsLunch = 0;

                                var timeReal = 4 - (durationCheckIn.TotalHours <= 0 ? 0 : durationCheckIn.TotalHours) - (durationCheckOut.TotalHours <= 0 ? 0 : durationCheckOut.TotalHours);
                                model.TimeReal = (decimal)timeReal;

                                if ((model.SumLate + model.SumEarly) <= 60)
                                {
                                    model.WorkTime = 0.5M;
                                }
                                else
                                {
                                    model.WorkTime = 0;
                                }
                            }

                            if (model.Day.Value.DayOfWeek != DayOfWeek.Saturday && model.Day.Value.DayOfWeek != DayOfWeek.Sunday)
                            {
                                durationCheckIn = model.CheckIn.Value - timeStartMorning;
                                durationCheckOut = timeEndAffternoon - model.CheckOut.Value;

                                var timeIn = durationCheckIn.TotalHours;
                                var timeOut = durationCheckOut.TotalHours;

                                bool timeWorking = false; // Check thời gian làm nửa ngày

                                if (timeIn >= 4)
                                {
                                    //MessageBox.Show("Làm ca chiều!");
                                    durationCheckIn = model.CheckIn.Value - timeStartAffternoon;
                                }
                                else if (timeOut >= 4)
                                {
                                    //MessageBox.Show("Làm ca sáng!");
                                    durationCheckOut = timeEndMorning - model.CheckOut.Value;
                                }
                                else
                                {
                                    //MessageBox.Show("Làm cả ngày");
                                    timeWorking = true;
                                }

                                timeIn = durationCheckIn.TotalMinutes;
                                timeOut = durationCheckOut.TotalMinutes;

                                model.IsLate = timeIn > 0 ? true : false;
                                model.IsEarly = timeOut > 0 ? true : false;

                                model.SumLate = model.IsLate == true ? (decimal)timeIn : 0;
                                model.SumEarly = model.IsEarly == true ? (decimal)timeOut : 0;

                                var timeReal = (timeWorking ? 8 : 4) - (durationCheckIn.TotalHours <= 0 ? 0 : durationCheckIn.TotalHours) - (durationCheckOut.TotalHours <= 0 ? 0 : durationCheckOut.TotalHours);
                                model.TimeReal = (decimal)timeReal;
                                model.IsLunch = model.TimeReal >= 6 ? 1 : 0;

                                if (!timeWorking)
                                {
                                    model.WorkTime = (model.SumLate + model.SumEarly) > 60 ? 0 : 0.5M;
                                }
                                else
                                {
                                    if ((model.SumLate + model.SumEarly) <= 60)
                                    {
                                        model.WorkTime = 1;
                                    }
                                    else if ((model.SumLate + model.SumEarly) > 120)
                                    {
                                        model.WorkTime = 0;
                                    }
                                    else
                                    {
                                        model.WorkTime = model.TimeReal / 8;
                                    }
                                }
                            }
                        }


                        EmployeeFingerprintBO.Instance.Insert(model);
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
            //MessageBox.Show("Cập nhật thành công!\n" + start.ToString() + " - " + DateTime.Now.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //enableControl(true);
            if (MessageBox.Show("Cập nhật thành công!\n" + start.ToString() + " - " + DateTime.Now.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
            {
                enableControl(true);
                this.Close();
            }
        }

        private void frmFingerprintExcel_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
