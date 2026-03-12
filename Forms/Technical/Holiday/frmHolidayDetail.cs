using BMS;
using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms.Technical.Holiday
{
    public partial class frmHolidayDetail : _Forms
    {

        public HolidayModel holidayModel = new HolidayModel();
        public frmHolidayDetail()
        {
            InitializeComponent();
        }

        private void frmHolidayDetail_Load(object sender, EventArgs e)
        {
            dtpHolidayDate.Value = DateTime.Now;
            loadHolidayDetail();
        }

        void loadHolidayDetail()
        {
            if (holidayModel.ID > 0)
            {
                dtpHolidayDate.Value = holidayModel.HolidayDate;
                txtHolidayName.Text = holidayModel.HolidayName;
                txtNote.Text = holidayModel.Note;
                cbTypeHoliday.SelectedIndex = holidayModel.TypeHoliday - 1;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        bool SaveData()
        {
            if (!Validate()) return false;

            holidayModel.HolidayDate = dtpHolidayDate.Value;
            holidayModel.HolidayName = txtHolidayName.Text;
            holidayModel.Note = txtNote.Text;
            holidayModel.TypeHoliday = cbTypeHoliday.SelectedIndex + 1;
            holidayModel.HolidayYear = dtpHolidayDate.Value.Year;
            holidayModel.HolidayMonth = dtpHolidayDate.Value.Month;
            holidayModel.HolidayDay = dtpHolidayDate.Value.Day;
            holidayModel.DayValue = "H";
            if (holidayModel.ID > 0)
            {
                HolidayBO.Instance.Update(holidayModel);
            }
            else
            {
                HolidayBO.Instance.Insert(holidayModel);
            }

            return true;
        }

        bool Validate()
        {
            string date = dtpHolidayDate.Value.ToString("yyyy-MM-dd");
            DataTable dt = TextUtils.Select($"select HolidayDate from Holiday where HolidayDate = '{date}'");
            if (dt.Rows.Count > 0 && holidayModel.ID<=0)
            {
                MessageBox.Show("Ngày nghỉ này đã có. Vui lòng chọn ngày khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                holidayModel = new HolidayModel();
            }
            //dtpHolidayDate.Value = DateTime.Now;
            //cbTypeHoliday.SelectedIndex =-1;

            
            //if (MessageBox.Show("Bạn có chắc muốn thêm mới ngày nghỉ", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
                
            //}
        }
    }
}
