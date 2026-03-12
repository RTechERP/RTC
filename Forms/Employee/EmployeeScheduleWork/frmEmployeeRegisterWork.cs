using BMS.Business;
using BMS.Model;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmEmployeeRegisterWork : _Forms
    {
        public frmEmployeeRegisterWork()
        {
            InitializeComponent();
        }
        private void EmployeeRegisterWork_Load(object sender, EventArgs e)
        {
            nmrYearCL.Value = nmrYearRW.Value = DateTime.Now.Year;
            nmrMonthCL.Value = nmrMonthRW.Value = DateTime.Now.Month;
            LoadDepartment();
            LoadDataRegisterWork();
            LoadDataCompensatoryLeave();
        }
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

            if(dtRegisterWork.Tables[0].Rows.Count >= 5)
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
        void LoadDepartment()
        {
            DataTable dtDp = TextUtils.Select("Select ID, Code, Name from dbo.Department");
            cbDepartmentRW.Properties.DataSource = dtDp;
            cbDepartmentRW.Properties.DisplayMember = "Name";
            cbDepartmentRW.Properties.ValueMember = "ID";
        }
        #endregion

        #region Lấy dữ liệu CompensatoryLeave
        void LoadDataCompensatoryLeave()
        {
            DataTable dtCompensatoryLeave = TextUtils.LoadDataFromSP("spGetEmployeeCompensatoryLeave", "B", 
                new string[] { "@Year", "@Month", "@DepartmentID", "@FilterText" }, 
                new object[] { nmrYearCL.Value, nmrMonthCL.Value, TextUtils.ToInt(cboDepartmentCL.EditValue), txtFindCL.Text.Trim() });
            grdCompensatoryLeave.DataSource = dtCompensatoryLeave;
        }
        #endregion

        #region Buttons RegisterWork
        private void btnFindRW_Click(object sender, EventArgs e)
        {
            LoadDataRegisterWork();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool isApproved = TextUtils.ToBoolean(grvRegisterWork.GetFocusedRowCellValue(colIsApprovedRW));

            int id = TextUtils.ToInt(grvRegisterWork.GetFocusedRowCellValue(colIDRW));
            string fullname = TextUtils.ToString(grvRegisterWork.GetFocusedRowCellValue(colFullNameRW));
            if (!isApproved)
            {
                if (MessageBox.Show(String.Format($"Bạn có chắc muốn xóa đăng ký làm việc của nhân viên [{fullname}] không?"), "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    int[] rowIndex = grvRegisterWork.GetSelectedRows();
                    for (int i = 0; i < rowIndex.Length; i++)
                    {
                        int ID = TextUtils.ToInt(grvRegisterWork.GetRowCellValue(rowIndex[i], colIDRW));
                        EmployeeRegisterWorkModel model = (EmployeeRegisterWorkModel)EmployeeRegisterWorkBO.Instance.FindByPK(ID);
                        model.DeleteFlag = true;
                        EmployeeRegisterWorkBO.Instance.Update(model);
                    }
                    grvRegisterWork.DeleteSelectedRows();
                }
            }
            else
            {
                MessageBox.Show(String.Format($"Nhân viên [{fullname}] đã được duyệt. \nVui lòng hủy duyệt trước khi xóa!"), "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }

        private void btnIsApprovedRW_Click(object sender, EventArgs e)
        {
            approvedRW(true, "IsApproved");
        }

        private void btnCancelApprovedRW_Click(object sender, EventArgs e)
        {
            approvedRW(false, "IsApproved");
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
        private void btnIsApprovedCL_Click(object sender, EventArgs e)
        {
            approvedCL(true, "IsApproved");
        }

        private void btnCancelApprovedCL_Click(object sender, EventArgs e)
        {
            approvedCL(false, "IsApproved");
        }

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

        #region Event RegisterWork
        void approvedRW(bool IsApproved, string fieldName)
        {
            string approved = IsApproved == true ? "duyệt" : "hủy duyệt";
            int[] rowIndex = grvRegisterWork.GetSelectedRows();
            List<int> listID = new List<int>();
            if(rowIndex.Length == 0)
            {
                MessageBox.Show($"Bạn chưa chọn nhân viên để [{approved}].\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            for (int i = 0; i < rowIndex.Length; i++)
            {
                bool isApproved = TextUtils.ToBoolean(grvRegisterWork.GetRowCellValue(rowIndex[i], colIsApprovedRW));
                int ApproverID = TextUtils.ToInt(grvRegisterWork.GetRowCellValue(rowIndex[i], colApproverRW));
                string Name = TextUtils.ToString(grvRegisterWork.GetRowCellValue(rowIndex[i], colFullNameRW));
                if (ApproverID == 0) continue;

                if (!isApproved && !IsApproved)
                {
                    MessageBox.Show($"Bạn không thể hủy duyệt vì: Nhân viên [{Name}] chưa được duyệt.\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (isApproved & IsApproved)
                {
                    MessageBox.Show($"Bạn không thể duyệt vì: Nhân viên [{Name}] đã được duyệt.\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (MessageBox.Show(string.Format($"Bạn có chắc muốn {approved} danh sách nhân viên đã chọn không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (IsApproved)
                {
                    for (int i = 0; i < rowIndex.Length; i++)
                    {
                        listID.Add(TextUtils.ToInt(grvRegisterWork.GetRowCellValue(rowIndex[i], colIDRW)));
                        string Name = TextUtils.ToString(grvRegisterWork.GetRowCellValue(rowIndex[i], colFullNameRW));
                        int ID = TextUtils.ToInt(grvRegisterWork.GetRowCellValue(rowIndex[i], colIDRW));
                        EmployeeRegisterWorkModel model = (EmployeeRegisterWorkModel)EmployeeRegisterWorkBO.Instance.FindByPK(ID);
                        if (model != null)
                        {
                            model.Approver = Global.EmployeeID;
                            model.IsApproved = IsApproved;
                            EmployeeRegisterWorkBO.Instance.Update(model);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < rowIndex.Length; i++)
                    {
                        listID.Add(TextUtils.ToInt(grvRegisterWork.GetRowCellValue(rowIndex[i], colIDRW)));
                        int ID = TextUtils.ToInt(grvRegisterWork.GetRowCellValue(rowIndex[i], colIDRW));
                        EmployeeRegisterWorkModel model = (EmployeeRegisterWorkModel)EmployeeRegisterWorkBO.Instance.FindByPK(ID);
                        if (model != null)
                        {
                            model.Approver = 0;
                            model.IsApproved = IsApproved;
                            EmployeeRegisterWorkBO.Instance.Update(model);
                        }
                    }
                }
            }
            LoadDataRegisterWork();
        }
        private void nmrYearRW_ValueChanged(object sender, EventArgs e)
        {
            LoadDataRegisterWork();
        }

        private void nmrMonthRW_ValueChanged(object sender, EventArgs e)
        {
            LoadDataRegisterWork();
        }

        private void grvData_CustomDrawBandHeader(object sender, DevExpress.XtraGrid.Views.BandedGrid.BandHeaderCustomDrawEventArgs e)
        {
            if (e.Band != null && e.Band.Caption == "Selection")
            {
                e.Info.Caption = "";
            }
        }
        #endregion

        #region Event CompensatoryLeave
        void approvedCL(bool IsApproved, string fieldName)
        {
            string approved = IsApproved == true ? "duyệt" : "hủy duyệt";
            int[] rowIndex = grvCompensatoryLeave.GetSelectedRows();
            List<int> listID = new List<int>();
            if (rowIndex.Length == 0)
            {
                MessageBox.Show($"Bạn chưa chọn nhân viên để [{approved}].\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            for (int i = 0; i < rowIndex.Length; i++)
            {
                bool isApproved = TextUtils.ToBoolean(grvCompensatoryLeave.GetRowCellValue(rowIndex[i], colIsApprovedCL));
                int ApproverID = TextUtils.ToInt(grvCompensatoryLeave.GetRowCellValue(rowIndex[i], colApproverCL));
                string Name = TextUtils.ToString(grvCompensatoryLeave.GetRowCellValue(rowIndex[i], colFullNameCL));
                if (ApproverID == 0) continue;

                if (!isApproved && !IsApproved)
                {
                    MessageBox.Show($"Bạn không thể hủy duyệt vì: Nhân viên [{Name}] chưa được duyệt.\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (isApproved & IsApproved)
                {
                    MessageBox.Show($"Bạn không thể duyệt vì: Nhân viên [{Name}] đã được duyệt.\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (MessageBox.Show(string.Format($"Bạn có chắc muốn {approved} danh sách nhân viên đã chọn không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (IsApproved)
                {
                    for (int i = 0; i < rowIndex.Length; i++)
                    {
                        listID.Add(TextUtils.ToInt(grvCompensatoryLeave.GetRowCellValue(rowIndex[i], colIDCL)));
                        int ID = TextUtils.ToInt(grvCompensatoryLeave.GetRowCellValue(rowIndex[i], colIDCL));
                         EmployeeCompensatoryLeaveModel model = (EmployeeCompensatoryLeaveModel)EmployeeCompensatoryLeaveBO.Instance.FindByPK(ID);
                        if (model != null)
                        {
                            model.Approver = Global.EmployeeID;
                            model.IsApproved = IsApproved;
                            EmployeeCompensatoryLeaveBO.Instance.Update(model);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < rowIndex.Length; i++)
                    {
                        listID.Add(TextUtils.ToInt(grvCompensatoryLeave.GetRowCellValue(rowIndex[i], colIDCL)));
                        int ID = TextUtils.ToInt(grvCompensatoryLeave.GetRowCellValue(rowIndex[i], colIDCL));
                        EmployeeCompensatoryLeaveModel model = (EmployeeCompensatoryLeaveModel)EmployeeCompensatoryLeaveBO.Instance.FindByPK(ID);
                        if (model != null)
                        {
                            model.Approver = 0;
                            model.IsApproved = IsApproved;
                            EmployeeCompensatoryLeaveBO.Instance.Update(model);
                        }
                    }
                }
                //TextUtils.ExcuteProcedure("spUpdateTableByFieldNameAndID",
                //    new string[] { "@TableName", "@FieldName", "@Value", "@ID", "@ValueUpdatedBy", "@ValueUpdatedDate" },
                //    new object[] { "EmployeeScheduleWork", fieldName, IsApproved ? 1 : 0, string.Join(",", listID), Global.LoginName, DateTime.Now });
            }
            LoadDataCompensatoryLeave();
        }
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
