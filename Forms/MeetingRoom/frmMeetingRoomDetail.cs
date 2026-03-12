using BMS.Business;
using BMS.Model;
using BMS.Utils;
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
    public partial class frmMeetingRoomDetail : _Forms
    {
        public int ID = 0;
        public frmMeetingRoomDetail()
        {
            InitializeComponent();
        }

        private void frmMeetingRoomDetail_Load(object sender, EventArgs e)
        {
            dtpDateRegister.Value = DateTime.Now;
            cboMeetingRoom.SelectedIndex = 0;


            loadEmployee();
            LoadData();
        }
        private void loadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.DataSource = dt;
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.EditValue = Global.EmployeeID;
        }

        private void LoadData()
        {
            if (Global.IsAdmin)
            {
                cboEmployee.Enabled = true;
            }

            //dtpDateRegister.MinDate = DateTime.Now;
            //dtpDateRegister.MaxDate = DateTime.Now.AddMonths(+1);

            if (ID > 0)
            {
                BookingRoomModel model = SQLHelper<BookingRoomModel>.FindByID(ID);
                txtContent.Text = model.Content;
                cboMeetingRoom.SelectedIndex = model.MeetingRoomID;
                cboEmployee.EditValue = model.EmployeeID;
                dtpDateRegister.Value = (DateTime)model.DateRegister;
                dtpTimeStart.EditValue = model.StartTime.Value.TimeOfDay;
                dtpTimeEnd.EditValue = model.EndTime.Value.TimeOfDay;

                int checkRole = TextUtils.ToInt(TextUtils.ExcuteScalar($"SELECT TOP 1 ID FROM dbo.vUserGroupLink WHERE UserID = {Global.UserID} AND Code = 'N46'"));
                if (!(model.EmployeeID == Global.EmployeeID || checkRole > 0 || Global.IsAdmin))
                {
                    btnSave.Enabled = false;
                    btnSaveAndNew.Enabled = false;
                }

            }
        }

        List<UserGroupLinkModel> usesGroupLink = SQLHelper<UserGroupLinkModel>.FindByAttribute("UserGroupID", 52);
        bool isAuthorized(BookingRoomModel model)
        {

            //var isFocus1 = grvRoomOne.IsFocusedView;
            //var isFocus2 = grvRoomTwo.IsFocusedView;
            //var isFocus3 = grvRoomThree.IsFocusedView;

            //string[] values = new string[10];

            //if (isFocus1)
            //{
            //    string data = TextUtils.ToString(grvRoomOne.GetFocusedValue()).Trim();
            //    if (!string.IsNullOrEmpty(data))
            //    {
            //        values = data.Split('#')[1].Split('-');
            //    }
            //}
            //else if (isFocus2)
            //{
            //    string data = TextUtils.ToString(grvRoomOne.GetFocusedValue()).Trim();
            //    if (!string.IsNullOrEmpty(data))
            //    {
            //        values = data.Split('#')[1].Split('-');
            //    }
            //}
            //else if (isFocus3)
            //{
            //    string data = TextUtils.ToString(grvRoomThree.GetFocusedValue()).Trim();
            //    if (!string.IsNullOrEmpty(data))
            //    {
            //        values = data.Split('#')[1].Split('-');
            //    }
            //}
            //else
            //{
            //    return false;
            //}


            //int employeeID = TextUtils.ToInt(values[1]);
            //List<int> userIDs = usesGroupLink.Select(c => c.UserID).ToList();

            if (Global.EmployeeID != model.EmployeeID && (Global.EmployeeID > 0 || !Global.IsAdmin) && !usesGroupLink.Any(x => x.UserID == Global.UserID) && Global.EmployeeID != 156)
            {
                //MessageBox.Show($"Bạn không có quyền chỉnh sửa hay xóa lịch đặt phòng họp của người khác!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool validate()
        {
            try
            {
                txtContent.Focus();

                var start = TextUtils.ToDate5(dtpTimeStart.EditValue).TimeOfDay;
                var end = TextUtils.ToDate5(dtpTimeEnd.EditValue).TimeOfDay;
                var endCheck = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 30, 0).TimeOfDay;

                if (string.IsNullOrEmpty(txtContent.Text.Trim()))
                {
                    MessageBox.Show("Vui lòng nhập Nội dung!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (TextUtils.ToInt(cboMeetingRoom.SelectedIndex) <= 0)
                {
                    MessageBox.Show("Vui lòng chọn Phòng họp!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (TextUtils.ToInt(cboEmployee.EditValue) <= 0)
                {
                    MessageBox.Show("Vui lòng chọn Người đăng ký!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (string.IsNullOrEmpty(dtpDateRegister.Text))
                {
                    MessageBox.Show("Vui lòng nhập Ngày đăng ký!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (dtpDateRegister.Value.Date < DateTime.Now.Date)
                {
                    MessageBox.Show("Bạn không được đăng ký trước ngày hiện tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if ((dtpDateRegister.Value - DateTime.Now.AddMonths(+1)).TotalDays > 0)
                {
                    MessageBox.Show("Ngày đăng ký phải là <= 1 tháng!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (start.Hours < 8)
                {
                    MessageBox.Show("Thời gian bắt đầu không được nhỏ hơn 8h !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (end > endCheck)
                {
                    MessageBox.Show("Thời gian kết thúc không được lớn hơn 17h30 !", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (start >= end)
                {
                    MessageBox.Show("Thời gian bắt đầu không được lớn hơn thời gian kết thúc!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                if (!(start.Minutes == 0 || start.Minutes == 30) || !(end.Minutes == 0 || end.Minutes == 30))
                {
                    MessageBox.Show("Số phút phải là 0 hoặc 30 phút!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                List<BookingRoomModel> lst = SQLHelper<BookingRoomModel>.FindAll();

                var check = lst.Where(p => p.ID != ID && p.DateRegister.Value.Date == dtpDateRegister.Value.Date
                                            && p.MeetingRoomID == cboMeetingRoom.SelectedIndex
                                            && ((p.StartTime.Value.TimeOfDay <= start && p.EndTime.Value.TimeOfDay >= start)
                                            || (p.StartTime.Value.TimeOfDay <= end && p.EndTime.Value.TimeOfDay >= end))).ToList();

                if (check.Count > 0)
                {
                    MessageBox.Show($"Khoảng thời gian đã có người đăng ký [{cboMeetingRoom.Text}]! Vui lòng nhập khoảng thời gian khác", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!Save()) return;
            this.DialogResult = DialogResult.OK;
        }

        private bool Save()
        {
            if (!validate()) return false;
            BookingRoomModel model = new BookingRoomModel();
            model.Content = txtContent.Text.Trim();
            model.MeetingRoomID = cboMeetingRoom.SelectedIndex;
            model.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
            model.DateRegister = dtpDateRegister.Value;
            var start = TextUtils.ToDate5(dtpTimeStart.EditValue);
            var end = TextUtils.ToDate5(dtpTimeEnd.EditValue);
            model.StartTime = new DateTime(dtpDateRegister.Value.Year, dtpDateRegister.Value.Month, dtpDateRegister.Value.Day, start.Hour, start.Minute, 0);
            model.EndTime = new DateTime(dtpDateRegister.Value.Year, dtpDateRegister.Value.Month, dtpDateRegister.Value.Day, end.Hour, end.Minute, 0);
            if (ID <= 0)
            {
                BookingRoomBO.Instance.Insert(model);
                sendEmail(true);
            }
            else
            {
                model.ID = ID;
                UpdateBookingRoomLog(model);

                BookingRoomBO.Instance.Update(model);
                sendEmail(false);
            }
            return true;
        }

        private void btnSaveAndNew_Click(object sender, EventArgs e)
        {
            if (!Save()) return;
            ID = 0;
            txtContent.Clear();
            dtpTimeStart.EditValue = null;
            dtpTimeEnd.EditValue = null;
            //cboMeetingRoom.SelectedIndex = 0;
        }


        void sendEmail(bool isAdd)
        {
            string status = isAdd ? "ĐĂNG KÝ" : "CẬP NHẬT";
            string startTime = TextUtils.ToDate5(dtpTimeStart.EditValue).ToString("HH:mm");
            string endTime = TextUtils.ToDate5(dtpTimeEnd.EditValue).ToString("HH:mm");
            EmployeeSendEmailModel e = new EmployeeSendEmailModel();

            e.Subject = $"{status} PHÒNG HỌP - {cboEmployee.Text.ToUpper()} - {dtpDateRegister.Value.ToString("dd/MM/yyyy")}";
            e.EmailTo = "hanhchinh@rtc.edu.vn";
            e.Body = $@"<div> <p style=""font-weight: bold; color: red;"">[NO REPLY]</p> <p> Dear Phòng Hành chính nhân sự </p ></div >
                       <div style = ""margin-top: 30px;"">
                        <p> Em xin phép anh / chị cho em đăng kí {cboMeetingRoom.Text} ngày {dtpDateRegister.Value.ToString("dd/MM/yyyy")} từ: {startTime} - đến: {endTime} </p>
                        <p> Nội dung: {txtContent.Text.Trim()}</p>
                        <p> Anh / chị duyệt giúp em với ạ.Em cảm ơn! </p>
                       </div>
                       <div style = ""margin-top: 30px;"">
                        <p> Thanks </p>
                        <p> {cboEmployee.Text}</p>
                       </div>";
            e.StatusSend = 1;
            e.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
            e.Receiver = 156;
            EmployeeSendEmailBO.Instance.Insert(e);
        }

        private void UpdateBookingRoomLog(BookingRoomModel updatedModel)
        {
            string roomName1 = "Phòng họp 1";
            string roomName2 = "Phòng họp 2";
            string roomName3 = "Phòng họp dự án";
            var originalModel = SQLHelper<BookingRoomModel>.FindByID(updatedModel.ID);
            StringBuilder logContent = new StringBuilder();
            logContent.Append($"Nhân viên {Global.AppFullName} đã ");
            if (originalModel != null)
            {
                string oldEmpCode = SQLHelper<EmployeeModel>.FindByID(originalModel.EmployeeID).FullName;
                logContent.Append($"cập nhật lịch đặt phòng họp của {oldEmpCode}, {Environment.NewLine}");

                string GetRoomName(int roomID)
                {
                    switch (roomID)
                    {
                        case 1: return roomName1;
                        case 2: return roomName2;
                        case 3: return roomName3;
                        default: return string.Empty;
                    }
                }
                if (originalModel.MeetingRoomID != updatedModel.MeetingRoomID)
                {
                    string oldRoom = GetRoomName(originalModel.MeetingRoomID);
                    string newRoom = GetRoomName(updatedModel.MeetingRoomID);
                    logContent.Append($"phòng họp từ '{oldRoom}' đến '{newRoom}',{Environment.NewLine}");
                }
                if (originalModel.StartTime != updatedModel.StartTime)
                {
                    logContent.Append($"thời gian bắt đầu {originalModel.StartTime.Value.ToString("HH:mm")} thành {updatedModel.StartTime.Value.ToString("HH:mm")}, {Environment.NewLine}");
                }
                if (originalModel.EndTime != updatedModel.EndTime)
                {
                    logContent.Append($"thời gian kết thúc {originalModel.EndTime.Value.ToString("HH:mm")} thành {updatedModel.EndTime.Value.ToString("HH:mm")}, {Environment.NewLine}");
                }
                if (originalModel.DateRegister != updatedModel.DateRegister)
                {
                    logContent.Append($"ngày đăng ký {originalModel.DateRegister.Value.ToString("dd/MM/yyyy")} thành ngày {updatedModel.DateRegister.Value.ToString("dd/MM/yyyy")}, {Environment.NewLine}");
                }
                if (originalModel.EmployeeID != updatedModel.EmployeeID)
                {
                    string newEmpCode = SQLHelper<EmployeeModel>.FindByID(updatedModel.EmployeeID).FullName;
                    logContent.Append($"người đăng ký chuyển từ {oldEmpCode} thành {newEmpCode}, {Environment.NewLine}");
                }
                if (originalModel.Content != updatedModel.Content)
                {
                    logContent.Append($"nội dung từ '{originalModel.Content}' thành '{updatedModel.Content}'.");
                }
                if (logContent.Length > 0)
                {
                    logContent.Length -= 2;

                    BookingRoomLogModel logModel = new BookingRoomLogModel
                    {
                        ContentLog = logContent.ToString(),
                        CreatedBy = Global.AppUserName,
                        CreatedDate = DateTime.Now,
                        DateLog = DateTime.Now,
                        UpdatedBy = Global.AppUserName,
                        UpdatedDate = DateTime.Now
                    };
                    SQLHelper<BookingRoomLogModel>.Insert(logModel);
                }
            }
        }
    }
}