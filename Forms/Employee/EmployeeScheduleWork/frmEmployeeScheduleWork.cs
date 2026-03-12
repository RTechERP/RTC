using BMS.Business;
using BMS.Model;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmEmployeeScheduleWork : _Forms
    {
        DataTable dtScheduleWork;
        //EmployeeScheduleWorkModel EmployeeSchedule = new EmployeeScheduleWorkModel();
        public frmEmployeeScheduleWork()
        {
            InitializeComponent();
        }
        private void frmEmployeeScheduleWork_Load(object sender, EventArgs e)
        {
            nmrYearSW.Value = nmrYearRW.Value = nmrYearCL.Value = DateTime.Now.Year;
            nmrMonthSW.Value = nmrMonthRW.Value = nmrMonthCL.Value = DateTime.Now.Month;
            //LoadDataScheduleWork();
            LoadGetEmployeeScheduleWork();
            LoadDepartment();
            LoadDataRegisterWork();
            LoadDataCompensatoryLeave();
        }
        void LoadDepartment()
        {
            DataTable dtDp = TextUtils.Select("Select ID, Code, Name from dbo.Department");
            cbDepartmentRW.Properties.DataSource = dtDp;
            cbDepartmentRW.Properties.DisplayMember = "Name";
            cbDepartmentRW.Properties.ValueMember = "ID";

            cboDepartmentCL.Properties.DataSource = dtDp;
            cboDepartmentCL.Properties.DisplayMember = "Name";
            cboDepartmentCL.Properties.ValueMember = "ID";
        }

        #region Lấy dữ liệu ScheduleWork
        //void LoadDataScheduleWork()
        //{
        //    DataTable dtScheduleWorkcheck = TextUtils.Select($"Select * from dbo.EmployeeScheduleWork Where WorkMonth = {nmrMonthSW.Value} AND WorkYear = {nmrYearSW.Value}");
        //    if (dtScheduleWorkcheck.Rows.Count == 0)
        //    {
        //        LoadGetEmployeeScheduleWork();
        //    }
        //    else
        //    {
        //        dtScheduleWork = TextUtils.LoadDataFromSP("spGetEmployeeScheduleWork", "B", new string[] { "@Year", "@Month" }, new object[] { nmrYearSW.Value, nmrMonthSW.Value });
        //        grdScheduleWork.DataSource = dtScheduleWork;
        //    }
        //}

        void LoadGetEmployeeScheduleWork()
        {
            dtScheduleWork = TextUtils.LoadDataFromSP("spGetEmployeeScheduleWorkByDate", "A", new string[] { "@Year", "@Month" }, new object[] { nmrYearSW.Value, nmrMonthSW.Value });
            grdScheduleWork.DataSource = dtScheduleWork;
        }
        #endregion

        #region Lấy dữ liệu RegisterWork
        void LoadDataRegisterWork()
        {
            DataSet dtRegisterWork = TextUtils.LoadDataSetFromSP("spGetEmployeeRegisterWork",
                new string[] { "@Year", "@Month", "@DepartmentID", "@FilterText" },
                new object[] { nmrYearRW.Value, nmrMonthRW.Value, TextUtils.ToInt(cbDepartmentRW.EditValue), txtFindRW.Text.Trim() });

            colSaturday1.FieldName = TextUtils.ToDate5(dtRegisterWork.Tables[0].Rows[0]["DateValue"]).ToString("yyyy-MM-dd");
            colSaturday1.Caption = TextUtils.ToDate5(dtRegisterWork.Tables[0].Rows[0]["DateValue"]).ToString("dd/MM/yyyy");
            colSaturday2.FieldName = TextUtils.ToDate5(dtRegisterWork.Tables[0].Rows[1]["DateValue"]).ToString("yyyy-MM-dd");
            colSaturday2.Caption = TextUtils.ToDate5(dtRegisterWork.Tables[0].Rows[1]["DateValue"]).ToString("dd/MM/yyyy");
            colSaturday3.FieldName = TextUtils.ToDate5(dtRegisterWork.Tables[0].Rows[2]["DateValue"]).ToString("yyyy-MM-dd");
            colSaturday3.Caption = TextUtils.ToDate5(dtRegisterWork.Tables[0].Rows[2]["DateValue"]).ToString("dd/MM/yyyy");
            colSaturday4.FieldName = TextUtils.ToDate5(dtRegisterWork.Tables[0].Rows[3]["DateValue"]).ToString("yyyy-MM-dd");
            colSaturday4.Caption = TextUtils.ToDate5(dtRegisterWork.Tables[0].Rows[3]["DateValue"]).ToString("dd/MM/yyyy");

            if (dtRegisterWork.Tables[0].Rows.Count >= 5)
            {
                colSaturday5.FieldName = TextUtils.ToDate5(dtRegisterWork.Tables[0].Rows[4]["DateValue"]).ToString("yyyy-MM-dd");
                colSaturday5.Caption = TextUtils.ToDate5(dtRegisterWork.Tables[0].Rows[4]["DateValue"]).ToString("dd/MM/yyyy");
            }
            else
            {
                colSaturday5.FieldName = " ";
                colSaturday5.Caption = " ";
            }
            grdRegisterWork.DataSource = dtRegisterWork.Tables[1];
        }

        #endregion

        #region Lấy dữ liệu CompensatoryLeave
        void LoadDataCompensatoryLeave()
        {
            DataTable dtCompensatoryLeave = TextUtils.LoadDataFromSP("spGetEmployeeCompensatoryLeave", "A", 
                new string[] { "@Year", "@Month", "@DepartmentID", "@FilterText" }, 
                new object[] { nmrYearCL.Value, nmrMonthCL.Value, TextUtils.ToInt(cboDepartmentCL.EditValue), txtFindCL.Text.Trim() });
            grdCompensatoryLeave.DataSource = dtCompensatoryLeave;
        }
        #endregion

        #region Button ScheduleWork
        private void btnSave_Click(object sender, EventArgs e)
        {
            //grvData.CloseEditForm();
            grvScheduleWork.FocusedRowHandle = -1;
            if (SaveData())
            {
                //MessageBox.Show(string.Format("Đã cất thành công!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //LoadDataScheduleWork();

                LoadGetEmployeeScheduleWork();
            }
        }
        private void btnIsApprovedSW_Click(object sender, EventArgs e)
        {
            approvedSW(true, "IsApproved");
        }

        private void btnCancelApprovedSW_Click(object sender, EventArgs e)
        {
            approvedSW(false, "IsApproved");
        }

        private void btnExcelSW_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
                sfd.FileName = $"LichLamViec_T{nmrMonthSW.Value}_{nmrYearSW.Value}";
                XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
                optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    grvScheduleWork.OptionsPrint.AutoWidth = false;
                    grvScheduleWork.OptionsPrint.ExpandAllDetails = false;
                    grvScheduleWork.OptionsPrint.PrintDetails = true;
                    grvScheduleWork.OptionsPrint.UsePrintStyles = true;
                    try
                    {
                        grvScheduleWork.ExportToXls(sfd.FileName);
                        Process.Start(sfd.FileName);
                    }
                    catch (Exception)
                    {
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Buttons RegisterWork
        private void btnFindRW_Click(object sender, EventArgs e)
        {
            LoadDataRegisterWork();
        }

        private void txtYear_ValueChanged(object sender, EventArgs e)
        {
            LoadDataRegisterWork();
        }

        private void txtMonth_ValueChanged(object sender, EventArgs e)
        {
            LoadDataRegisterWork();
        }

        private void btnExcelRW_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
                sfd.FileName = $"DangKiLamViec_T{nmrMonthRW.Value}_{nmrYearRW.Value}";
                XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
                optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    grvRegisterWork.OptionsPrint.AutoWidth = false;
                    grvRegisterWork.OptionsPrint.ExpandAllDetails = false;
                    grvRegisterWork.OptionsPrint.PrintDetails = true;
                    grvRegisterWork.OptionsPrint.UsePrintStyles = true;
                    try
                    {
                        grvRegisterWork.ExportToXls(sfd.FileName);
                        Process.Start(sfd.FileName);
                    }
                    catch (Exception)
                    {
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Button CompensatoryLeave
        private void btnExcelCL_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
                sfd.FileName = $"DangKiNghiBu_T{nmrMonthCL.Value}_{nmrYearCL.Value}";
                XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
                optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    grvCompensatoryLeave.OptionsPrint.AutoWidth = false;
                    grvCompensatoryLeave.OptionsPrint.ExpandAllDetails = false;
                    grvCompensatoryLeave.OptionsPrint.PrintDetails = true;
                    grvCompensatoryLeave.OptionsPrint.UsePrintStyles = true;
                    try
                    {
                        grvCompensatoryLeave.ExportToXls(sfd.FileName);
                        Process.Start(sfd.FileName);
                    }
                    catch (Exception)
                    {
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnFindCL_Click(object sender, EventArgs e)
        {
            LoadDataCompensatoryLeave();
        }
        #endregion

        #region Event ScheduleWork
        bool ValidateForm()
        {
            try
            {
                for (int i = 0; i < dtScheduleWork.Rows.Count; i++)
                {
                    bool IsApproved = TextUtils.ToBoolean(grvScheduleWork.GetRowCellValue(i, colIsApprovedSW));
                    DateTime date = TextUtils.ToDate5(grvScheduleWork.GetRowCellValue(i, colDateValueSW));
                    if (IsApproved)
                    {
                        MessageBox.Show(string.Format($"Ngày làm việc ngày {date} đã được duyệt !"));
                        continue;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        bool SaveData()
        {
            if (!ValidateForm())
            {
                return false;
            }
            try
            {
                for (int i = 0; i < dtScheduleWork.Rows.Count; i++)
                {
                    int ID = TextUtils.ToInt(grvScheduleWork.GetRowCellValue(i, colIDSW));
                    EmployeeScheduleWorkModel EmployeeSchedule = (EmployeeScheduleWorkModel)EmployeeScheduleWorkBO.Instance.FindByPK(ID);
                    if (EmployeeSchedule == null)
                    {
                        EmployeeSchedule = new EmployeeScheduleWorkModel();
                    }
                    EmployeeSchedule.Status = TextUtils.ToBoolean(grvScheduleWork.GetRowCellValue(i, colStatus));
                    EmployeeSchedule.WorkYear = TextUtils.ToInt(grvScheduleWork.GetRowCellValue(i, colWorkYear));
                    EmployeeSchedule.WorkMonth = TextUtils.ToInt(grvScheduleWork.GetRowCellValue(i, colWorkMonth));
                    EmployeeSchedule.WorkDay = TextUtils.ToInt(grvScheduleWork.GetRowCellValue(i, colWorkDay));
                    EmployeeSchedule.DateValue = TextUtils.ToDate5(grvScheduleWork.GetRowCellValue(i, colDateValueSW));


                    if (EmployeeSchedule.ID > 0)
                    {
                        EmployeeScheduleWorkBO.Instance.Update(EmployeeSchedule);
                    }
                    else
                    {
                        EmployeeScheduleWorkBO.Instance.Insert(EmployeeSchedule);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        void approvedSW(bool IsApproved, string fieldName)
        {
            string approved = IsApproved == true ? "duyệt" : "hủy duyệt";
            List<int> listID = new List<int>();
            int month = TextUtils.ToInt(nmrMonthSW.Value);

            for (int i = 0; i < grvScheduleWork.RowCount; i++)
            {
                bool isApproved = TextUtils.ToBoolean(grvScheduleWork.GetRowCellValue(i, colIsApprovedSW));
                int ApproverID = TextUtils.ToInt(grvScheduleWork.GetRowCellValue(i, colApproverSW));
                if (ApproverID == 0) continue;

                if (!isApproved && !IsApproved)
                {
                    MessageBox.Show($"Bạn không thể hủy duyệt vì: Lịch làm việc tháng [{month}] chưa được duyệt.\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (isApproved & IsApproved)
                {
                    MessageBox.Show($"Bạn không thể duyệt vì: Lịch làm việc tháng [{month}] đã được duyệt.\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (MessageBox.Show(string.Format($"Bạn có chắc muốn {approved} lịch làm việc tháng [{month}] không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (IsApproved)
                {
                    for (int i = grvScheduleWork.RowCount - 1; i >= 0; i--)
                    {
                        listID.Add(TextUtils.ToInt(grvScheduleWork.GetRowCellValue(i, colIDSW)));
                        int ID = TextUtils.ToInt(grvScheduleWork.GetRowCellValue(i, colIDSW));
                        EmployeeScheduleWorkModel model = (EmployeeScheduleWorkModel)EmployeeScheduleWorkBO.Instance.FindByPK(ID);
                        if (model != null)
                        {
                            model.Approver = Global.EmployeeID;
                            model.IsApproved = IsApproved;
                            EmployeeScheduleWorkBO.Instance.Update(model);
                        }
                    }
                }
                else
                {
                    for (int i = grvScheduleWork.RowCount - 1; i >= 0; i--)
                    {
                        listID.Add(TextUtils.ToInt(grvScheduleWork.GetRowCellValue(i, colIDSW)));
                        int ID = TextUtils.ToInt(grvScheduleWork.GetRowCellValue(i, colIDSW));
                        EmployeeScheduleWorkModel model = (EmployeeScheduleWorkModel)EmployeeScheduleWorkBO.Instance.FindByPK(ID);
                        if (model != null)
                        {
                            model.Approver = 0;
                            model.IsApproved = IsApproved;
                            EmployeeScheduleWorkBO.Instance.Update(model);
                        }
                    }
                }
                //TextUtils.ExcuteProcedure("spUpdateTableByFieldNameAndID",
                //    new string[] { "@TableName", "@FieldName", "@Value", "@ID", "@ValueUpdatedBy", "@ValueUpdatedDate" },
                //    new object[] { "EmployeeScheduleWork", fieldName, IsApproved ? 1 : 0, string.Join(",", listID), Global.LoginName, DateTime.Now });
            }
            //LoadDataScheduleWork();

            LoadGetEmployeeScheduleWork();
        }
        private void nmrYear_ValueChanged(object sender, EventArgs e)
        {
            //LoadDataScheduleWork();

            LoadGetEmployeeScheduleWork();
        }

        private void nmrMonth_ValueChanged(object sender, EventArgs e)
        {
            //LoadDataScheduleWork();

            LoadGetEmployeeScheduleWork();
        }
        #endregion

        #region Event RegisterWork
        private void grvData_CustomDrawBandHeader(object sender, DevExpress.XtraGrid.Views.BandedGrid.BandHeaderCustomDrawEventArgs e)
        {
            if (e.Band != null && e.Band.Caption == "Selection")
            {
                e.Info.Caption = "";
            }
        }
        #endregion

        #region Event CompensatoryLeave
        private void nmrYearCL_ValueChanged(object sender, EventArgs e)
        {
            LoadDataCompensatoryLeave();
        }

        private void nmrMonthCL_ValueChanged(object sender, EventArgs e)
        {
            LoadDataCompensatoryLeave();
        }
        #endregion
    }
}
