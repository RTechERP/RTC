using BMS;
using BMS.Model;
using DevExpress.XtraEditors;
using Forms.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmHolidayCalendar : _Forms
    {
        int month, year;
        public int typeDay = -1;
        public static int static_month, static_year;

        public static List<DataHoliday> listDate = new List<DataHoliday>();
        public static int typeCode = 0;
        public static string typeName = "";

        List<HolidayType> listType = new List<HolidayType>
            {
                new HolidayType{TypeCode = 1, TypeName = "Chủ nhật"},
                new HolidayType{TypeCode = 1, TypeName = "Nghỉ lễ, Tết"},
                new HolidayType{TypeCode = 1, TypeName = "Du lịch"},
                new HolidayType{TypeCode = 2, TypeName = "Thứ bảy"},
            };

        int width = 0;
        public frmHolidayCalendar()
        {
            InitializeComponent();
        }

        private void frmHolidayCalendar_Load(object sender, EventArgs e)
        {
            

            width = pnDayContainer.Width - 39;

            displayDays();
            loadCboType();

            typeCode = TextUtils.ToInt(cboTypeHoliday.SelectedValue);
            typeName = cboTypeHoliday.Text;
        }

        void loadCboType()
        {
            cboTypeHoliday.DataSource = listType;
            cboTypeHoliday.ValueMember = "TypeCode";
            cboTypeHoliday.DisplayMember = "TypeName";
        }

        void loadDay()
        {
            pnDayContainer.Controls.Clear();
            //Lấy ngày đầu tiên trong tháng
            //string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            lbMonthYear.Text = $"Tháng {month}/{year}";
            static_month = month;
            static_year = year;
            DateTime startOfTheMonth = new DateTime(year, month, 1);
            //Lấy tổng số ngày trong tháng
            int days = DateTime.DaysInMonth(year, month);

            //Convert  ngày đầu tiên trong tháng sang int
            int dayOfTheWeek = TextUtils.ToInt(startOfTheMonth.DayOfWeek.ToString("d"));

            int lastDayPreMonth = startOfTheMonth.AddDays(-1).Day;
            for (int i = dayOfTheWeek; i >= 1; i--)
            {
                ucBlank blank = new ucBlank();
                blank.Width = (width / 7);

                string textDisable = (lastDayPreMonth - (i - 1)).ToString();

                blank.setText(textDisable);
                pnDayContainer.Controls.Add(blank);
            }
            List<int> listSat = new List<int>();
            List<int> listSun = new List<int>();

            for (int i = 1; i <= days; i++)
            {
                DateTime day = new DateTime(year, month, i);
                if (day.DayOfWeek == DayOfWeek.Saturday)
                {
                    listSat.Add(i);
                }
                else if (day.DayOfWeek == DayOfWeek.Sunday)
                {
                    listSun.Add(i);
                }
            }

            //Insert day 
            for (int i = 1; i <= days; i++)
            {
                
                ucDays ucD = new ucDays();
                ucD.days(i);
                ucD.Width = (width / 7);
                pnDayContainer.Controls.Add(ucD);
            }

            int countControl = 42 - pnDayContainer.Controls.Count;
            for (int i = 0; i < countControl; i++)
            {
                ucBlank blank = new ucBlank();
                blank.Width = (width / 7);

                string textDisable = (i + 1).ToString();

                blank.setText(textDisable);
                pnDayContainer.Controls.Add(blank);
            }
        }

        private void displayDays()
        {
            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;
            string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            lbMonthYear.Text = $"Tháng {month}/{year}";

            loadDay();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
           
            if (month == 1)
            {
                month = 13;
                year--;
            }
            month--;
           
            loadDay();
        }

        private void cboTypeHoliday_SelectedIndexChanged(object sender, EventArgs e)
        {
            typeCode = TextUtils.ToInt(cboTypeHoliday.SelectedValue);
            typeName = cboTypeHoliday.Text;
        }

        private void frmHolidayCalendar_SizeChanged(object sender, EventArgs e)
        {

            width = pnDayContainer.Width - 39;
            loadDay();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            foreach (var item in listDate)
            {
                if (item.TypeCode == 1)
                {
                    //Insert bảng Holiday
                }
                else
                {
                    //Insert bảng Lịch thứ bảy
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            
            if (month == 12)
            {
                month = 0;
                year++;
            }
            month++;
            loadDay();
        }

        public class DataHoliday
        {
            public int TypeHoliday { get; set; }
            public string HolidayName { get; set; }
            public DateTime DateValue { get; set; }
            public int TypeCode { get; set; }
        }
    }
}