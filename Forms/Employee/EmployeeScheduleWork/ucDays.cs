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
    public partial class ucDays : DevExpress.XtraEditors.XtraUserControl
    {
        public static int static_day;

        public ucDays()
        {
            InitializeComponent();
        }

        private void ucDays_Load(object sender, EventArgs e)
        {
            loadData();

            DateTime date = new DateTime(frmHolidayCalendar.static_year, frmHolidayCalendar.static_month, TextUtils.ToInt(lbDay.Text));

            if (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday)
            {
                chkCheckSalary.Visible = false;
            }
            else
            {
                chkCheckSalary.Visible = true;
            }
        }
        void loadData()
        {
            lbText.Text = "";
            this.BackColor = Color.White;

            DateTime date = new DateTime(frmHolidayCalendar.static_year, frmHolidayCalendar.static_month, TextUtils.ToInt(lbDay.Text), 0, 0, 0);

            //if (frmHolidayCalendar.listDate.Contains(date))
            //{
            //    this.BackColor = Color.Orange;
            //}

            //int day = TextUtils.ToInt(lbDay.Text);
            //int month = frmHolidayCalendar.static_month;
            //int year = frmHolidayCalendar.static_year;
            //var date = new DateTime(year, month, day);

            //var scheduleStatus = TextUtils.ExcuteScalar($"SELECT Status FROM dbo.EmployeeScheduleWork WHERE WorkDay = { day } AND WorkMonth = { month } AND WorkYear = { year }");
            //if (scheduleStatus != null)
            //{

            //    if (TextUtils.ToInt(scheduleStatus) > 0)
            //    {
            //        lbText.Text = "Đi làm";
            //        this.BackColor = Color.Orange;
            //    }


            //}
            //var holiday = TextUtils.ExcuteScalar($"SELECT HolidayName FROM dbo.Holiday WHERE HolidayDay = {day} AND HolidayMonth = {month} AND HolidayYear = {year}");
            //if (holiday != null)
            //{
            //    if (TextUtils.ToString(holiday).Contains("Nghỉ lễ"))
            //    {
            //        lbText.Text = "Nghỉ lễ";
            //        this.BackColor = Color.Red;
            //    }
            //    else if (TextUtils.ToString(holiday).Contains("Du lịch"))
            //    {
            //        lbText.Text = "Du lịch";
            //        this.BackColor = Color.BlueViolet;
            //    }
            //    else if (TextUtils.ToString(holiday).Contains("Chủ nhật"))
            //    {
            //        lbText.Text = "Chủ nhật";
            //        this.BackColor = Color.OrangeRed;
            //    }

            //}
        }

        public void days(int numday)
        {
            lbDay.Text = numday.ToString();

        }

        private void ucDays_Click(object sender, EventArgs e)
        {
            frmHolidayCalendar.DataHoliday dataHoliday = new frmHolidayCalendar.DataHoliday();
            dataHoliday.DateValue = new DateTime(frmHolidayCalendar.static_year, frmHolidayCalendar.static_month, TextUtils.ToInt(lbDay.Text));
            dataHoliday.TypeHoliday = chkCheckSalary.Checked ? 1 : 2;
            dataHoliday.HolidayName = frmHolidayCalendar.typeName;
            dataHoliday.TypeCode = frmHolidayCalendar.typeCode;

            var matchData = frmHolidayCalendar.listDate.Where(x => x.DateValue == dataHoliday.DateValue);

            if (matchData.Count() > 0)
            {
                frmHolidayCalendar.listDate.RemoveAll(x => x.DateValue == dataHoliday.DateValue);
                this.BackColor = Color.White;
                chkCheckSalary.Checked = false;
            }
            else
            {
                frmHolidayCalendar.listDate.Add(dataHoliday);
                this.BackColor = Color.Orange;
            }
            
        }

        private void chkCheckSalary_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCheckSalary.Checked)
            {
                ucDays_Click(null, null);
            }
        }
    }
}
