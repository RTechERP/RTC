using BMS.Business;
using BMS.Model;
using DevExpress.XtraEditors;
using System;
using System.Collections;
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
    public partial class frmEmployeeOvertimeDetail : _Forms
    {
        //EmployeeOvertimeModel overtime = new EmployeeOvertimeModel();
        //public List<EmployeeOvertimeModel> overtimes = new List<EmployeeOvertimeModel>();
        public DataTable dtOvertime = new DataTable();

        ArrayList listId = new ArrayList();
        public frmEmployeeOvertimeDetail()
        {
            InitializeComponent();
        }

        private void frmEmployeeOvertimeDetail_Load(object sender, EventArgs e)
        {
            LoadEmployee();
            LoadLocation();
            LoadTypeOvertime();
            LoadData();
        }

        void LoadEmployee()
        {
            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetEmployeeAndEmployeeApprover", new string[] { }, new object[] { });
            cboEmployee.Properties.DataSource = dataSet.Tables[0];
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.ValueMember = "ID";

            cboEmployeeApprove.Properties.DataSource = dataSet.Tables[1];
            cboEmployeeApprove.Properties.DisplayMember = "FullName";
            cboEmployeeApprove.Properties.ValueMember = "EmployeeID";
        }

        void LoadLocation()
        {
            List<object> list = new List<object>()
            {
                new {ID = 0,Location = "--Chọn địa điểm--"},
                new {ID = 1,Location = "Văn phòng"},
                new {ID = 2,Location = "Địa điểm công tác"},
                new {ID = 3,Location = "Tại nhà"},
            }.ToList();

            cboLocation.ValueMember = "ID";
            cboLocation.DisplayMember = "Location";
            cboLocation.DataSource = list;
        }

        void LoadTypeOvertime()
        {
            List<EmployeeTypeOvertimeModel> list = SQLHelper<EmployeeTypeOvertimeModel>.FindAll();
            cboType.DisplayMember = "Type";
            cboType.ValueMember = "ID";
            cboType.DataSource = list;
            cboType.CreateEditor();
        }

        void LoadData()
        {
            if (dtOvertime.Rows.Count > 0)
            {
                cboEmployee.EditValue = dtOvertime.Rows[0]["EmployeeID"];
                cboEmployeeApprove.EditValue = dtOvertime.Rows[0]["ApprovedID"];
                dtpDateRegister.Value = TextUtils.ToDate5(dtOvertime.Rows[0]["DateRegister"]);
            }
            else
            {
                cboEmployee.Focus();
                cboEmployee.EditValue = 0;
                cboEmployeeApprove.EditValue = 0;
                dtpDateRegister.Value = DateTime.Now; ;
            }

            cboEmployee.Enabled = cboEmployeeApprove.Enabled = !(dtOvertime.Rows.Count > 0);
            //dtOvertime.TableName = "TRUONGDEPTRAI";
            grdData.DataSource = dtOvertime;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                dtOvertime = new DataTable();
                listId.Clear();
                LoadData();
            }
        }

        private void dtpDateRegister_ValueChanged(object sender, EventArgs e)
        {
            repositoryItemDateEdit1.MinValue = new DateTime(dtpDateRegister.Value.Year, dtpDateRegister.Value.Month, dtpDateRegister.Value.Day, 0, 0, 0);
            repositoryItemDateEdit1.MaxValue = new DateTime(dtpDateRegister.Value.AddDays(+1).Year, dtpDateRegister.Value.AddDays(+1).Month, dtpDateRegister.Value.AddDays(+1).Day, 23, 59, 59);
        }

        bool CheckValidate()
        {
            if (TextUtils.ToInt(cboEmployee.EditValue) <= 0)
            {
                MessageBox.Show(string.Format("Vui lòng nhập Họ tên! "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (TextUtils.ToInt(cboEmployeeApprove.EditValue) <= 0)
            {
                MessageBox.Show(string.Format("Vui lòng nhập Người duyệt! "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            for (int i = 0; i < grvData.RowCount; i++)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                DateTime? timeStart = TextUtils.ToDate4(grvData.GetRowCellValue(i, colTimeStart));
                DateTime? timeEnd = TextUtils.ToDate4(grvData.GetRowCellValue(i, colEndTime));
                int location = TextUtils.ToInt(grvData.GetRowCellValue(i, colLocation));
                int type = TextUtils.ToInt(grvData.GetRowCellValue(i, colTypeID));
                string reason = TextUtils.ToString(grvData.GetRowCellValue(i, colReason)).Trim();
                string reasonEdit = TextUtils.ToString(grvData.GetRowCellValue(i, colReasonHREdit)).Trim();

                int stt = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));

                if (!timeStart.HasValue)
                {
                    MessageBox.Show($"Vui lòng nhập Thời gian bắt đầu [STT: {stt}]!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (!timeEnd.HasValue)
                {
                    MessageBox.Show($"Vui lòng nhập Thời gian kết thúc [STT: {stt}]!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                double timeSpan = (timeEnd.Value - timeStart.Value).TotalHours;
                if (timeSpan <= 0)
                {
                    MessageBox.Show($"Thời gian kết thúc phải lơn hơn thời gian bắt đầu [STT: {stt}]!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (location <= 0)
                {
                    MessageBox.Show($"Vui lòng nhập Địa điểm [STT: {stt}]!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (type <= 0)
                {
                    MessageBox.Show($"Vui lòng nhập Loại [STT: {stt}]!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (string.IsNullOrEmpty(reason))
                {
                    MessageBox.Show($"Vui lòng nhập Lý do [STT: {stt}]!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (id > 0)
                {
                    if (string.IsNullOrEmpty(reasonEdit))
                    {
                        MessageBox.Show($"Vui lòng nhập Lý do sửa [STT: {stt}]!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }


            return true;
        }

        bool SaveData()
        {
            grvData.CloseEditor();
            dtOvertime.AcceptChanges();
            if (!CheckValidate())
            {
                return false;
            }

            foreach (DataRow row in dtOvertime.Rows)
            {
                EmployeeOvertimeModel overtime = new EmployeeOvertimeModel();
                int id = TextUtils.ToInt(row["ID"]);
                if (id > 0)
                {
                    overtime = SQLHelper<EmployeeOvertimeModel>.FindByID(id);
                }
                overtime.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
                overtime.ApprovedID = TextUtils.ToInt(cboEmployeeApprove.EditValue);
                overtime.DateRegister = dtpDateRegister.Value.Date;
                overtime.TimeStart = TextUtils.ToDate5(row["TimeStart"]);
                overtime.EndTime = TextUtils.ToDate5(row["EndTime"]);
                overtime.Location = TextUtils.ToInt(row["Location"]);
                overtime.Overnight = TextUtils.ToBoolean(row["Overnight"]);
                overtime.TypeID = TextUtils.ToInt(row["TypeID"]);
                overtime.Reason = TextUtils.ToString(row["Reason"]);
                overtime.ReasonHREdit = TextUtils.ToString(row["ReasonHREdit"]);
                overtime.TimeReality = (decimal)(overtime.EndTime.Value - overtime.TimeStart.Value).TotalHours;
                EmployeeTypeOvertimeModel type = (EmployeeTypeOvertimeModel)cboType.GetRowByKeyValue(overtime.TypeID);
                overtime.TotalTime = overtime.TimeReality * (type.Ratio / 100);
                if (overtime.ID > 0)
                {
                    overtime.IsApproved = overtime.IsApprovedHR = false;
                    SQLHelper<EmployeeOvertimeModel>.Update(overtime);
                }
                else
                {
                    overtime.DecilineApprove = 1;
                    SQLHelper<EmployeeOvertimeModel>.Insert(overtime);
                }
            }

            if (listId.Count > 0)
            {
                EmployeeOvertimeBO.Instance.Delete(listId);
            }

            return true;
        }

        private void grvData_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                e.Value = grvData.GetRowHandle(e.ListSourceRowIndex) + 1;
            }
        }

        private void repositoryItemDateEdit1_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    grvData.CloseEditor();
            //    //dtOvertime.AcceptChanges();
            //    DateEdit dateEdit = (DateEdit)sender;
            //    DateTime dateValue = TextUtils.ToDate5(dateEdit.EditValue);
            //    DateTime dateOvernight = new DateTime(dtpDateRegister.Value.Year, dtpDateRegister.Value.Month, dtpDateRegister.Value.Day, 20, 00, 00);
            //    double timeSpan = (dateValue - dateOvernight).TotalHours;
            //    grvData.SetFocusedRowCellValue(colOvernight, (timeSpan >= 0));
            //    grvData.FocusedColumn = grvData.Columns[grvData.FocusedColumn.AbsoluteIndex + 1];
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Thông báo");
            //}
        }

        private void chkOvernight_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    grvData.CloseEditor();
            //    dtOvertime.AcceptChanges();
            //    CheckEdit checkEdit = (CheckEdit)sender;
            //    if (checkEdit.Checked)
            //    {
            //        int employeeID = TextUtils.ToInt(cboEmployee.EditValue);
            //        int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //        DataTable dt = TextUtils.LoadDataFromSP("spGetTotalDinnerByDate", "A",
            //                                             new string[] { "@Date", "@EmployeeID", "@ID" },
            //                                             new object[] { dtpDateRegister.Value.Date, employeeID, id });
            //        if (TextUtils.ToInt(dt.Rows[0]["Total"]) > 0)
            //        {
            //            MessageBox.Show($"Bạn đã chọn phụ cấp ăn tối ngày {dtpDateRegister.Value.ToString("dd/MM/yyyy")}");
            //            grvData.SetFocusedRowCellValue(colOvernight, false);
            //        }
            //        else
            //        {
            //            var dataOvernight = dtOvertime.Select("Overnight = 1");
            //            if (dataOvernight.Length > 1)
            //            {
            //                MessageBox.Show($"Bạn đã chọn phụ cấp ăn tối ngày {dtpDateRegister.Value.ToString("dd/MM/yyyy")}");
            //                grvData.SetFocusedRowCellValue(colOvernight, false);
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Thông báo");
            //}
        }

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == colTimeStart || e.Column == colEndTime)
            {
                grvData.CloseEditor();
                DateTime dateValue = TextUtils.ToDate5(grvData.GetFocusedRowCellValue(grvData.FocusedColumn));
                DateTime dateOvernight = new DateTime(dtpDateRegister.Value.Year, dtpDateRegister.Value.Month, dtpDateRegister.Value.Day, 20, 00, 00);
                double timeSpan = (dateValue - dateOvernight).TotalHours;
                grvData.SetFocusedRowCellValue(colOvernight, (timeSpan >= 0));
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            DateTime timeStart = TextUtils.ToDate5(grvData.GetFocusedRowCellValue(colTimeStart));
            DateTime timeEnd = TextUtils.ToDate5(grvData.GetFocusedRowCellValue(colEndTime));
            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá khai báo làm thêm ngày {dtpDateRegister.Value.ToString("dd/MM/yyyy")}\n" +
                                                  $"từ: {timeStart.ToString("dd/MM/yyyy HH:mm")}\n" +
                                                  $"đến: {timeEnd.ToString("dd/MM/yyyy HH:mm")}", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                listId.Add(id);
                grvData.DeleteSelectedRows();
            }
        }

        private void frmEmployeeOvertimeDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            grvData.FocusedRowHandle = -1;
            DataTable dataChange = dtOvertime.GetChanges();

            if (dataChange != null)
            {
                DialogResult dialog = MessageBox.Show("Bạn có muốn lưu những thay đổi không?", "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    if (SaveData())
                    {
                        e.Cancel = false;
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        e.Cancel = true;
                    }

                }
                else if (dialog == DialogResult.No)
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void frmEmployeeOvertimeDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                grvData.CloseEditor();
                DataTable dataChange = dtOvertime.GetChanges();
                if (dataChange != null)
                {
                    DialogResult dialog = MessageBox.Show("Bạn có muốn lưu những thay đổi không?", "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (dialog == DialogResult.Yes)
                    {
                        btnSave_Click(null, null);
                    }
                    else if (dialog == DialogResult.No)
                    {
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        
                    }
                }
            }
        }
    }
}
