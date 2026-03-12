using BMS;
using BMS.Business;
using BMS.Model;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmTypeDate : _Forms
    {
        int day = ucDays.static_day;
        int month = frmHolidayCalendar.static_month;
        int year = frmHolidayCalendar.static_year;
        private void frmTypeDate_Load(object sender, EventArgs e)
        {
            txtDate.Text = $"{day}-{month}-{year}";
            LoadData();
        }


        void LoadData()
        {
            var date = new DateTime(year, month, day);
            var scheduleStatus = TextUtils.ExcuteScalar($"SELECT Status FROM dbo.EmployeeScheduleWork WHERE WorkDay = { day } AND WorkMonth = { month } AND WorkYear = { year }");
            if (scheduleStatus != null)
            {
                if (TextUtils.ToInt(scheduleStatus) == 0)
                {
                    cboTypeDay.SelectedIndex = 1;
                }
                else
                {
                    cboTypeDay.SelectedIndex = 0;
                }
            }
            var holiday = TextUtils.ExcuteScalar($"SELECT HolidayName FROM dbo.Holiday WHERE HolidayDay = {day} AND HolidayMonth = {month} AND HolidayYear = {year}");
            if (holiday != null)
            {
                if (TextUtils.ToString(holiday).Contains("Nghỉ lễ"))
                {
                    cboTypeDay.SelectedIndex = 3;
                }
                else if (TextUtils.ToString(holiday).Contains("Du lịch"))
                {
                    cboTypeDay.SelectedIndex = 4;
                }
                else if (TextUtils.ToString(holiday).Contains("Chủ nhật"))
                {
                    cboTypeDay.SelectedIndex = 2;
                }
            }
        }
        public frmTypeDate()
        {
            InitializeComponent();
        }
        void saveData(bool status, string holidayName)
        {
          
            DateTime date = new DateTime(year, month, day);
            if(date.DayOfWeek == DayOfWeek.Saturday)
            {
                var scheduleID = TextUtils.ExcuteScalar($"SELECT ID FROM dbo.EmployeeScheduleWork WHERE WorkDay = { day } AND WorkMonth = { month } AND WorkYear = { year }");
                EmployeeScheduleWorkModel model = new EmployeeScheduleWorkModel();
                model.DateValue = date;
                model.Status = status;
                model.WorkDay = day;
                model.WorkMonth = month;
                model.WorkYear = year;
                if (scheduleID != null)
                {
                    model.ID = TextUtils.ToInt(scheduleID);
                    model.UpdatedDate = DateTime.Now;
                    model.UpdatedBy = Global.AppUserName;
                    EmployeeScheduleWorkBO.Instance.Update(model);
                }
                else
                {
                    model.CreatedDate = DateTime.Now;
                    model.CreatedBy = Global.AppUserName;
                    model.UpdatedDate = DateTime.Now;
                    model.UpdatedBy = Global.AppUserName;
                    EmployeeScheduleWorkBO.Instance.Insert(model);
                }
            }
            else
            {
                var holidayID = TextUtils.ExcuteScalar($"SELECT ID FROM dbo.Holiday WHERE HolidayDay = {day} AND HolidayMonth = {month} AND HolidayYear = {year}");
                HolidayModel model = new HolidayModel();
                model.HolidayDate = date;
                model.HolidayYear = year;
                model.HolidayMonth = month;
                model.HolidayDay = day;
                model.DayValue = "H";
                model.HolidayName = holidayName;
                model.HolidayCode = "";
                model.Note = "";
                model.TypeHoliday = 2;
                if(holidayID != null)
                {
                    model.ID = TextUtils.ToInt(holidayID);
                    HolidayBO.Instance.Update(model);
                }
                else
                {
                    HolidayBO.Instance.Insert(model);
                }
            }

            this.DialogResult = DialogResult.OK;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cboTypeDay.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn loại ngày");
            }
            else if (cboTypeDay.SelectedIndex == 0)
            {
                saveData(true,"7");
            }else if(cboTypeDay.SelectedIndex == 1)
            {
                saveData(false,"7");
            }else if(cboTypeDay.SelectedIndex == 2)
            {
                saveData(false, "Chủ nhật");
            }
            else if (cboTypeDay.SelectedIndex == 3)
            {
                saveData(false, "Nghỉ lễ");
            }
            else if (cboTypeDay.SelectedIndex == 4)
            {
                saveData(false, "DU lịch");
            }

        }
    }
}