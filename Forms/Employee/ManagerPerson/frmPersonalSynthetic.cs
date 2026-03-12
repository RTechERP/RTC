using System;
using System.Data;
using System.Drawing;

namespace BMS
{
    public partial class frmPersonalSynthetic : _Forms
    {
        public frmPersonalSynthetic()
        {
            InitializeComponent();
        }
        private void frmPersonalSynthetic_Load(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            txtMonth.Value = time.Month;
            txtYear.Value = time.Year;
            lbEmployee.Text = Global.AppCodeName +" - "+ Global.AppFullName;

            loadData();
            loadDataAttendance();
        }
        void loadData()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetPersonalSyntheticByMonth", "A",
                new string[] { "@Month", "@Year", "@EmployeeID" },
                new object[] { txtMonth.Value, txtYear.Value, Global.EmployeeID });
            grdData.DataSource = dt;
        }

        void loadDataAttendance()
        {
            DateTime dateStart = new DateTime((int)txtYear.Value, (int)txtMonth.Value, 1).AddSeconds(-1);
            DateTime dateEnd = new DateTime((int)txtYear.Value, (int)txtMonth.Value, 1, 23, 59, 59).AddMonths(+1).AddDays(-1).AddSeconds(+1);


            DataTable dt = TextUtils.GetDataTableFromSP("spGetEmployeeAttendance",
                                                new string[] { "@EmployeeID", "@DateStart", "@DateEnd" },
                                                new object[] { Global.EmployeeID, dateStart, dateEnd });
            grdAttendance.DataSource = dt;
        }

        private void txtMonth_ValueChanged(object sender, EventArgs e)
        {
            loadData();
            loadDataAttendance();
        }

        private void txtYear_ValueChanged(object sender, EventArgs e)
        {
            loadData();
            loadDataAttendance();
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            loadData();
            loadDataAttendance();
        }

        private void grvAttendance_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                int holidayDay = TextUtils.ToInt(grvAttendance.GetRowCellValue(e.RowHandle, colHolidayDay));
                if (e.Column == colCheckIn)
                {
                    bool isLate = TextUtils.ToBoolean(grvAttendance.GetRowCellValue(e.RowHandle, colIsLate));
                    if (isLate && holidayDay == 0)
                    {
                        e.Appearance.BackColor = Color.Red;
                        e.Appearance.ForeColor = Color.White;
                    }
                }

                if (e.Column == colCheckOut)
                {
                    bool isEarly = TextUtils.ToBoolean(grvAttendance.GetRowCellValue(e.RowHandle, colIsEarly));
                    if (isEarly && holidayDay == 0)
                    {
                        e.Appearance.BackColor = Color.Red;
                        e.Appearance.ForeColor = Color.White;
                    }
                }
            }
        }
    }
}
