using BMS;
using BMS.Business;
using BMS.Model;
using Forms.Technical.Holiday;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Forms.Classes.cGlobVar;

namespace Forms.Technical
{
    public partial class frmHoliday : _Forms
    {
        public frmHoliday()
        {
            InitializeComponent();
        }

        private void frmHoliday_Load(object sender, EventArgs e)
        {
            nmrYear.Value = DateTime.Now.Year;
            nmrMonth.Value = DateTime.Now.Month;
            loadHoliday();
        }

        /// <summary>
        /// Load List Holiday
        /// </summary>
        void loadHoliday()
        {
            UpdateSunday();
            int month = ckAllDate.Checked ? 0 : (int)nmrMonth.Value;

            DataTable dt = TextUtils.LoadDataFromSP(StoreProcedure.spGetHoliday, "A", new string[] { "@Month", "@Year" },new object[] { month, nmrYear.Value });
            if (dt.Rows.Count < 0) return;
            grdData.DataSource = dt;
        }

        void UpdateSunday()
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            List<DateTime> listSunday = Enumerable.Range(1, DateTime.DaysInMonth(year, month))
                                                          .Select(day => new DateTime(year, month, day))
                                                          .Where(date => date.DayOfWeek == DayOfWeek.Sunday)
                                                          .ToList();

            foreach (var date in listSunday)
            {
                var holiday = SQLHelper<HolidayModel>.FindAll().FirstOrDefault(p => p.HolidayDate.Date == date.Date);

                HolidayModel holidayModel = new HolidayModel();
                if (holiday != null)
                {
                    holidayModel = holiday;
                }
                holidayModel.HolidayDate = date.Date;
                holidayModel.HolidayName = "Chủ nhật";
                holidayModel.Note = "";
                holidayModel.TypeHoliday = 2;
                holidayModel.HolidayYear = date.Year;
                holidayModel.HolidayMonth = date.Month;
                holidayModel.HolidayDay = date.Day;
                holidayModel.DayValue = "H";
                if (holidayModel.ID > 0)
                {
                    HolidayBO.Instance.Update(holidayModel);
                }
                else
                {
                    HolidayBO.Instance.Insert(holidayModel);
                }

            }
        }

        //void loadHolidayAll()
        //{
        //    DataTable dt = TextUtils.LoadDataFromSP(StoreProcedure.spGetHoliday, "A", new string[] { "@Year" }, new object[] { nmrYear.Value });
        //    if (dt.Rows.Count < 0) return;
        //    grdData.DataSource = dt;
        //}

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmHolidayDetail frm = new frmHolidayDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadHoliday();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            HolidayModel model = (HolidayModel)HolidayBO.Instance.FindByPK(ID);
            frmHolidayDetail frm = new frmHolidayDetail();
            frm.holidayModel = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadHoliday();
                grvData.FocusedRowHandle = focusedRowHandle;
            }
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));

            string date = TextUtils.ToString(grvData.GetFocusedRowCellValue(colHolidayDate)).Substring(0, 10);

            if (MessageBox.Show(string.Format("Bạn có thực sự muốn xóa ngày [{0}] không?",date), "Xóa ngày nghỉ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                HolidayBO.Instance.Delete(id);
                grvData.DeleteSelectedRows();
            }

        }

        private void btnAddSaturday_Click(object sender, EventArgs e)
        {
            frmEmployeeScheduleWork frm = new frmEmployeeScheduleWork();
            frm.Show();
        }

        private void nmrYear_ValueChanged(object sender, EventArgs e)
        {
            ckAllDate.Checked = false;
            loadHoliday();
        }

        private void nmrMonth_ValueChanged(object sender, EventArgs e)
        {
            ckAllDate.Checked = false;
            loadHoliday();
        }

        private void ckAllDate_CheckedChanged(object sender, EventArgs e)
        {

            loadHoliday();

        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadHoliday();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmHolidayCalendar frm = new frmHolidayCalendar();
            frm.Show();
        }
    }
}
