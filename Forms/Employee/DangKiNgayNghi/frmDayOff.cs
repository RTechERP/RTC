using BMS.Business;
using BMS.Model;
using DevExpress.XtraPrinting;
using Forms.Employee.DangKiNgayNghi;
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
    public partial class frmDayOff : _Forms
    {
        int StatusApprove, IDApproved, departmentId;
        DataSet dts;
        string[] arrParamName = null;
        object[] arrParamValue = null;
        public frmDayOff()
        {
            InitializeComponent();
        }
        private void frmDayOff_Load(object sender, EventArgs e)
        {
            txtYear.Value = DateTime.Now.Year;
            txtMonth.Value = DateTime.Now.Month;
            loadDepartment();
            IDApproved = Global.EmployeeID;

            btnIsApproved_HR.Visible = btnCancelApproved_HR.Visible = hRHuỷDuyệtHuỷĐăngKíToolStripMenuItem.Visible = false;

            if (Global.DepartmentID == 6 || Global.IsAdmin)//phòng nhân sự
            {
                cbApprovedStatusTBP.SelectedIndex = 0;//tất cả
                IDApproved = 0;
                btnIsApproved_HR.Visible = btnCancelApproved_HR.Visible = hRHuỷDuyệtHuỷĐăngKíToolStripMenuItem.Visible = true;
                cbDepartment.EditValue = 0;
            }
            else if (Global.DepartmentID == 1)//phòng giám đốc
            {
                cbApprovedStatusTBP.SelectedIndex = 2;//chưa duyệt
                IDApproved = 0;
                cbDepartment.EditValue = 0;
            }
            else
            {
                cbDepartment.EditValue = Global.DepartmentID;
                cbApprovedStatusTBP.SelectedIndex = 2;
            }

            loadData();
        }
        void loadData()
        {
            StatusApprove = cbApprovedStatusTBP.SelectedIndex - 1; //Khánh update
            departmentId = TextUtils.ToInt(cbDepartment.EditValue);
            //Khánh bỏ vì thấy Thừa thãi
            //if (cbApprovedStatusTBP.SelectedIndex == 0) ///Tất cả
            //{
            //    arrParamName = new string[] { "@PageNumber", "@PageSize", "@Keyword", "@Month", "@Year", "@IDApprovedTP", "@DepartmentID" };
            //    arrParamValue = new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), txtFilterText.Text, TextUtils.ToInt(txtMonth.Value),
            //        TextUtils.ToInt(txtYear.Value), IDApproved, departmentId };
            //}
            //else
            //{
            //    arrParamName = new string[] { "@PageNumber", "@PageSize", "@Keyword", "@Month", "@Year", "@IDApprovedTP", "@Status", "@DepartmentID" };
            //    arrParamValue = new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), txtFilterText.Text, TextUtils.ToInt(txtMonth.Value),
            //        TextUtils.ToInt(txtYear.Value), IDApproved, StatusApprove, departmentId };
            //}

            arrParamName = new string[] { "@PageNumber", "@PageSize", "@Keyword", "@Month", "@Year", "@IDApprovedTP", "@Status", "@DepartmentID" };
            arrParamValue = new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), txtFilterText.Text, TextUtils.ToInt(txtMonth.Value),
                    TextUtils.ToInt(txtYear.Value), IDApproved, StatusApprove, departmentId };

            dts = TextUtils.LoadDataSetFromSP("spGetDayOff", arrParamName, arrParamValue);
            grdData.DataSource = dts.Tables[0];
            if (dts.Tables[0].Rows.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(dts.Tables[1].Rows[0]["TotalPage"]);
        }
        void loadDepartment()
        {
            DataTable dt = TextUtils.Select($"Select ID, Code, Name From Department");
            cbDepartment.Properties.DataSource = dt;
            cbDepartment.Properties.DisplayMember = "Name";
            cbDepartment.Properties.ValueMember = "ID";
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmDayOffDetail frm = new frmDayOffDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }


        private void btnFind_Click(object sender, EventArgs e)
        {
            loadData();
            //txtFilterText.Text = "";
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) > TextUtils.ToInt(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) >= TextUtils.ToInt(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadData();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = TextUtils.ToInt(txtPageNumber.Text) > 1 ? (TextUtils.ToInt(txtPageNumber.Text) - 1).ToString() : "1";
            loadData();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) >= TextUtils.ToInt(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadData();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {

            txtPageNumber.Text = "1";
            loadData();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
                sfd.FileName = $"DangKyNghi_T{txtMonth.Value}_{txtYear.Value}";
                XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
                optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    grvData.OptionsPrint.AutoWidth = false;
                    grvData.OptionsPrint.ExpandAllDetails = false;
                    grvData.OptionsPrint.PrintDetails = true;
                    grvData.OptionsPrint.UsePrintStyles = true;
                    try
                    {
                        grvData.ExportToXls(sfd.FileName);
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

        private void txtYear_ValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool isApprovedTP = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApprovedTP));
            bool isApprovedHR = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApprovedHR));

            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string fullname = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullname));
            if (!isApprovedTP && !isApprovedHR)
            {
                if (MessageBox.Show(String.Format($"Bạn có chắc muốn xóa đăng ký nghỉ của nhân viên [{fullname}] không?"), "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    int[] rowIndex = grvData.GetSelectedRows();
                    for (int i = 0; i < rowIndex.Length; i++)
                    {
                        int ID = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colID));
                        //EmployeeOnLeaveBO.Instance.Delete(ID);
                        SQLHelper<EmployeeOnLeaveModel>.DeleteModelByID(ID);
                    }
                    grvData.DeleteSelectedRows();
                }
            }
            else
            {
                MessageBox.Show(String.Format($"Nhân viên [{fullname}] đã được duyệt. \nVui lòng hủy duyệt trước khi xóa!"), "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int focusRow = grvData.FocusedRowHandle;
            bool isApprovedTP = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApprovedTP));
            bool isApprovedHR = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApprovedHR));
            string fullname = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullname));
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id == 0)
            {
                return;
            }

            if (true)
            {
                //EmployeeOnLeaveModel employeeOnLeave = (EmployeeOnLeaveModel)EmployeeOnLeaveBO.Instance.FindByPK(id);
                EmployeeOnLeaveModel employeeOnLeave = SQLHelper<EmployeeOnLeaveModel>.FindByID(id);
                frmDayOffDetail frm = new frmDayOffDetail();
                frm.onleave = employeeOnLeave;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    loadData();
                    grvData.FocusedRowHandle = focusRow;
                }
            }
            else
            {
                MessageBox.Show(string.Format($"Nhân viên [{fullname}] đã được duyệt.\nVui lòng hủy duyệt trước khi sửa!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void approved(bool IsApproved, string fieldName, bool isTBP)
        {
            string approved = IsApproved == true ? "duyệt" : "hủy duyệt";
            int[] rowIndex = grvData.GetSelectedRows();
            List<int> listID = new List<int>();

            for (int i = 0; i < rowIndex.Length; i++)
            {
                int departmentId = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colDepartmentID));
                bool isApprovedTP = TextUtils.ToBoolean(grvData.GetRowCellValue(rowIndex[i], colIsApprovedTP));
                int employeeId = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colEmployeeID));
                if (employeeId == 0) continue;
                string employeeName = TextUtils.ToString(grvData.GetRowCellValue(rowIndex[i], colFullname));

                if (!isApprovedTP && !IsApproved)
                {
                    MessageBox.Show($"Bạn không thể hủy duyệt vì: nhân viên [{employeeName}] chưa được TBP duyệt.\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!Global.IsAdmin)
                {
                    if (departmentId != Global.DepartmentID && Global.DepartmentID != 1 && isTBP)
                    {
                        MessageBox.Show($"Nhân viên [{employeeName}] không thuộc phòng [{Global.DepartmentName.ToUpper()}].\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (employeeId == Global.EmployeeID && isTBP)
                {
                    MessageBox.Show($"Bạn không được {approved} cho chính mình.\nVui lòng liên hệ cấp cao hơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (MessageBox.Show(string.Format($"Bạn có chắc muốn {approved} danh sách nhân viên đã chọn không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                for (int i = 0; i < rowIndex.Length; i++)
                {

                    if (isTBP && IsApproved) //Trương phong duyet
                    {
                        listID.Add(TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colID)));
                    }
                    if (isTBP && !IsApproved) // Truong phong huy duyet
                    {
                        if (!TextUtils.ToBoolean(grvData.GetRowCellValue(rowIndex[i], colIsApprovedHR)))
                        {
                            listID.Add(TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colID)));
                        }
                    }
                }

                if (!isTBP && IsApproved) // Nhan su duyet
                {
                    for (int i = rowIndex.Length - 1; i >= 0; i--)
                    {
                        if (TextUtils.ToBoolean(grvData.GetRowCellValue(rowIndex[i], colIsApprovedTP)))
                        {
                            listID.Add(TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colID)));
                            int type = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colType));
                            //CalculateCoefficientOff(IsApproved, rowIndex[i]);
                            //CalculateDayOff(IsApproved, rowIndex[i]);
                            int ID = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colID));
                            EmployeeOnLeaveModel model = (EmployeeOnLeaveModel)EmployeeOnLeaveBO.Instance.FindByPK(ID);
                            if (model != null)
                            {
                                model.ApprovedHR = Global.EmployeeID;
                                EmployeeOnLeaveBO.Instance.Update(model);
                            }
                        }
                    }
                }

                if (!isTBP && !IsApproved) //Nhan su huy duyet
                {
                    for (int i = 0; i < rowIndex.Length; i++)
                    {
                        if (!isTBP && !IsApproved) //Nhan su huy duyet
                        {
                            listID.Add(TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colID)));
                            int type = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colType));
                            //CalculateCoefficientOff(IsApproved, rowIndex[i]);
                            //CalculateDayOff(IsApproved, rowIndex[i]);
                            //int ID = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colID));
                            //EmployeeOnLeaveModel model = (EmployeeOnLeaveModel)EmployeeOnLeaveBO.Instance.FindByPK(ID);
                            //if (model != null)
                            //{
                            //    model.ApprovedHR = 0;
                            //    EmployeeOnLeaveBO.Instance.Update(model);
                            //}
                        }
                    }
                }

                int value = IsApproved ? 1 : 0;
                string sql = $"UPDATE EmployeeOnLeave SET IsApprovedHR = {value},UpdatedBy ='{Global.LoginName}',UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' WHERE ID IN ({string.Join(",", listID)})";
                TextUtils.ExcuteSQL(sql);
                //TextUtils.ExcuteProcedure("spUpdateTableByFieldNameAndID",
                //    new string[] { "@TableName", "@FieldName", "@Value", "@ID", "@ValueUpdatedBy", "@ValueUpdatedDate" },
                //    new object[] { "EmployeeOnLeave", fieldName, IsApproved ? 1 : 0, string.Join(",", listID) });
            }
            loadData();
        }
        private void btnIsApproved_TBP_Click(object sender, EventArgs e)
        {
            //approved(true, "IsApprovedTP", true);
            Approved(true,true);
        }

        private void btnCancelApproved_TBP_Click(object sender, EventArgs e)
        {
            //int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            //string fullName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullname));

            //EmployeeOnLeaveModel employeeOnLeaveModel = (EmployeeOnLeaveModel)EmployeeOnLeaveBO.Instance.FindByPK(id);
            //if (employeeOnLeaveModel.IsApprovedHR)
            //{
            //    MessageBox.Show($"HR chưa hủy duyệt nhân viên [{code} - {fullName}] .\nVui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //else
            //{
            //    approved(false, "IsApprovedTP", true);
            //}

            //approved(false, "IsApprovedTP", true);
            Approved(false, true);
        }

        private void btnIsApproved_HR_Click(object sender, EventArgs e)
        {
            //approved(true, "IsApprovedHR", false);
            Approved(true, false);

            ////int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            ////string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            ////string fullName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullname));
            //bool isApprovedTP = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApprovedTP));
            ////bool isApprovedHR = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApprovedHR));

            //if (!isApprovedTP)
            //{
            //    MessageBox.Show($"Trưởng bộ phận chưa duyệt.\nVui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else
            //{
            //    approved(true, "IsApprovedHR", false);
            //}
        }

        private void btnCancelApproved_HR_Click(object sender, EventArgs e)
        {
            //int id = (int)grvData.GetFocusedRowCellValue(colID);
            //string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            //string fullName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullname));
            //EmployeeOnLeaveModel employeeOnLeaveModel = (EmployeeOnLeaveModel)EmployeeOnLeaveBO.Instance.FindByPK(id);
            //if (!employeeOnLeaveModel.IsApprovedTP)
            //{
            //    MessageBox.Show($"Nhân viên [{code} - {fullName}] chưa được duyệt.\nVui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //else
            //{
            //    approved(false, "IsApprovedHR", false);
            //}

            //approved(false, "IsApprovedHR", false);
            Approved(false, false);
        }

        private void btnSummaryOL_Click(object sender, EventArgs e)
        {
            frmSummaryEmployeeOnleave frm = new frmSummaryEmployeeOnleave();
            frm.month = txtMonth.Value;
            frm.year = txtYear.Value;
            frm.ShowDialog();
        }

        private void btnDeclareDayOff_Click(object sender, EventArgs e)
        {
            frmDeclareDayOff frm = new frmDeclareDayOff();
            frm.ShowDialog();
        }
        private void CalculateDayOff(bool Status, int i)
        {
            int employeeID = TextUtils.ToInt(grvData.GetRowCellValue(i, colEmployeeID));
            int ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
            DateTime startdate = TextUtils.ToDate5(grvData.GetRowCellValue(i, colStartDate));
            TextUtils.ExcuteProcedure("spCalculateDayOff", new string[] { "@ID", "@EmployeeID", "@Date", "@Status", "@Year" }, new object[] { ID, employeeID, startdate, Status, startdate.Year });
        }
        private void CalculateCoefficientOff(bool Status, int i)
        {
            int employeeID = TextUtils.ToInt(grvData.GetRowCellValue(i, colEmployeeID));
            int ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
            DateTime startdate = TextUtils.ToDate5(grvData.GetRowCellValue(i, colStartDate));
            TextUtils.ExcuteProcedure("spCalculateCoefficientOff", new string[] { "@ID", "@EmployeeID", "@Date", "@Status", "@Year" }, new object[] { ID, employeeID, startdate, Status, startdate.Year });
        }

        private void btnIsCancelHR_Click(object sender, EventArgs e)
        {
            int[] arrRowIndex = grvData.GetSelectedRows();
            if (arrRowIndex.Length <= 0)
            {
                MessageBox.Show($"Bạn chưa chọn nhân viên.\nVui lòng chọn nhân viên!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            for (int i = 0; i < arrRowIndex.Length; i++)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(arrRowIndex[i], colID));
                if (id <= 0)
                {
                    continue;
                }
                EmployeeOnLeaveModel employeeOnLeaveModel = (EmployeeOnLeaveModel)EmployeeOnLeaveBO.Instance.FindByPK(id);
                bool isCancelTP = TextUtils.ToBoolean(grvData.GetRowCellValue(arrRowIndex[i], colIsCancelTP));
                if (!isCancelTP)
                {
                    MessageBox.Show($"Trưởng bộ phận chưa duyệt.\nVui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    employeeOnLeaveModel.IsCancelHR = true;
                    EmployeeOnLeaveBO.Instance.Update(employeeOnLeaveModel);
                    loadData();
                }
            }
        }

        private void btnIsCancelTP_Click(object sender, EventArgs e)
        {
            int[] arrRowIndex = grvData.GetSelectedRows();
            if (arrRowIndex.Length <= 0)
            {
                MessageBox.Show($"Bạn chưa chọn nhân viên.\nVui lòng chọn nhân viên!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            for (int i = 0; i < arrRowIndex.Length; i++)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(arrRowIndex[i], colID));
                if (id <= 0)
                {
                    continue;
                }

                string code = TextUtils.ToString(grvData.GetRowCellValue(arrRowIndex[i], colCode));
                string fullName = TextUtils.ToString(grvData.GetRowCellValue(arrRowIndex[i], colFullname));
                EmployeeOnLeaveModel employeeOnLeaveModel = (EmployeeOnLeaveModel)EmployeeOnLeaveBO.Instance.FindByPK(id);
                if (!employeeOnLeaveModel.IsApprovedHR || !employeeOnLeaveModel.IsCancelRegister)
                {
                    MessageBox.Show($"Nhân viên [{code} - {fullName}] chưa đăng kí huỷ duyệt.\nVui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    employeeOnLeaveModel.IsCancelTP = true;
                    EmployeeOnLeaveBO.Instance.Update(employeeOnLeaveModel);
                    loadData();
                }
            }
        }

        private void TBPHuỷDuyệtHuỷĐăngKíToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] arrRowIndex = grvData.GetSelectedRows();
            if (arrRowIndex.Length <= 0)
            {
                MessageBox.Show($"Bạn chưa chọn nhân viên.\nVui lòng chọn nhân viên!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            for (int i = 0; i < arrRowIndex.Length; i++)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(arrRowIndex[i], colID));
                if (id <= 0)
                {
                    continue;
                }
                string code = TextUtils.ToString(grvData.GetRowCellValue(arrRowIndex[i], colCode));
                string fullName = TextUtils.ToString(grvData.GetRowCellValue(arrRowIndex[i], colFullname));
                EmployeeOnLeaveModel employeeOnLeaveModel = (EmployeeOnLeaveModel)EmployeeOnLeaveBO.Instance.FindByPK(id);
                if (employeeOnLeaveModel.IsCancelHR)
                {
                    MessageBox.Show($"HR chưa hủy duyệt nhân viên [{code} - {fullName}] .\nVui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    employeeOnLeaveModel.IsCancelTP = false;
                    EmployeeOnLeaveBO.Instance.Update(employeeOnLeaveModel);
                    loadData();
                }
            }
        }

        private void HRHuỷDuyệtHuỷĐăngKíToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] arrRowIndex = grvData.GetSelectedRows();
            if (arrRowIndex.Length <= 0)
            {
                MessageBox.Show($"Bạn chưa chọn nhân viên.\nVui lòng chọn nhân viên!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            for (int i = 0; i < arrRowIndex.Length; i++)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(arrRowIndex[i], colID));
                if (id <= 0)
                {
                    continue;
                }
                EmployeeOnLeaveModel employeeOnLeaveModel = (EmployeeOnLeaveModel)EmployeeOnLeaveBO.Instance.FindByPK(id);
                employeeOnLeaveModel.IsCancelHR = true;
                EmployeeOnLeaveBO.Instance.Update(employeeOnLeaveModel);
                //MessageBox.Show($"Huỷ duyệt cho nhân viên thành công!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadData();
            }
        }

        private void cbApprovedStatusTBP_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void cbApprovedStatusTBP_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            loadData();
        }

        private void cbDepartment_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }


        void Approved(bool isApprove, bool isTBP)
        {
            int[] selectedRows = grvData.GetSelectedRows();
            List<int> listID = new List<int>();
            string message = "";
            if (isTBP && isApprove) //Nếu là TBP duyệt
            {
                foreach (int row in selectedRows)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
                    if (id <= 0)
                    {
                        continue;
                    }
                    //int approveTbp = TextUtils.ToInt(grvData.GetRowCellValue(row, colApprovedTP));
                    //if (approveTbp != Global.EmployeeID)
                    //{
                    //    MessageBox.Show($"Bạn có chắc muốn {approveText} danh sách nhân viên đã chọn.\n{message}", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //}

                    listID.Add(id);
                }
            }
            else if (isTBP && !isApprove) //Nếu là TBP huỷ duyệt
            {
                message = "Nhân viên đã được HR duyệt sẽ không thể huỷ duyệt.\nBạn có muốn tiếp tục?"; 
                foreach (int row in selectedRows)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
                    bool isApprovedHR = TextUtils.ToBoolean(grvData.GetRowCellValue(row, colIsApprovedHR));
                    if (id <= 0 || isApprovedHR)
                    {
                        continue;
                    }

                    listID.Add(id);
                }
            }
            else if (!isTBP && isApprove) //Nếu là HR duyệt
            {
                message = "Nhân viên chưa được TBP duyệt sẽ không thể duyệt.\nBạn có muốn tiếp tục?";
                foreach (int row in selectedRows)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
                    bool isApprovedTP = TextUtils.ToBoolean(grvData.GetRowCellValue(row, colIsApprovedTP));
                    if (id <= 0 || !isApprovedTP)
                    {
                        continue;
                    }

                    listID.Add(id);
                }
            }
            else if (!isTBP && !isApprove) //Nếu là HR huỷ duyệt
            {
                foreach (int row in selectedRows)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
                    if (id <= 0)
                    {
                        continue;
                    }

                    listID.Add(id);
                }
            }

            string approveText = isApprove ? "duyệt" : "huỷ duyệt";
            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn {approveText} danh sách nhân viên đã chọn.\n{message}", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                if (listID.Count > 0)
                {
                    string fieldName = isTBP ? "IsApprovedTP" : "IsApprovedHR";
                    int valueApprove = isApprove ? 1 : 0;
                    string fieldNameApprove = isTBP ? "" : $"ApprovedHR = {Global.EmployeeID}, ";
                    string sql = $"UPDATE dbo.EmployeeOnLeave " +
                                $"SET {fieldName} = {valueApprove}, {fieldNameApprove}" +
                                $"UpdatedBy = '{Global.LoginName}'," +
                                $"UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' " +
                                $"WHERE ID IN({string.Join(",", listID)})";


                    TextUtils.ExcuteSQL(sql);
                }
            }
            loadData();
        }
    }
}

