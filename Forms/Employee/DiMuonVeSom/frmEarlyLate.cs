using BMS.Business;
using BMS.Model;
using DevExpress.Utils;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmEarlyLate : _Forms
    {
        int StatusApprove, IDApproved;
        string[] arrParamName = null;
        object[] arrParamValue = null;
        EmployeeEarlyLateModel employeeEarlyLate = new EmployeeEarlyLateModel();
        public frmEarlyLate()
        {
            InitializeComponent();
        }

        private void frmEarlyLate_Load(object sender, EventArgs e)
        {
            //dtpStartDate.Value = DateTime.Today.AddDays(-1);
            txtPageNumber.Text = "1";
            txtYear.Value = DateTime.Now.Year;
            txtMonth.Value = DateTime.Now.Month;

            loadDepartment();
            IDApproved = Global.EmployeeID;
            btnApprovedHR.Visible = false;
            if (Global.DepartmentID == 6)//phòng nhân sự
            {
                cbApprovedStatusTBP.SelectedIndex = 0;//tất cả
                IDApproved = 0;
                btnApprovedHR.Visible = true;
                toolStripSeparator1.Visible = true;
                cboDepartment.EditValue = 0;
            }
            else if (Global.DepartmentID == 1)//phòng giám đốc
            {
                cbApprovedStatusTBP.SelectedIndex = 1;//chưa duyệt
                IDApproved = 0;
                cboDepartment.EditValue = 0;
            }
            else
            {
                cboDepartment.EditValue = Global.DepartmentID;
                cbApprovedStatusTBP.SelectedIndex = 1;
            }
            // int a = Global.DepartmentID;
            LoadEmployeeEarlyLate();
        }

        /// <summary>
        /// Load phòng ban vào commbo box
        /// </summary>
        void loadDepartment()
        {
            DataTable dt = TextUtils.Select($"Select ID, Code, Name From Department");
            cboDepartment.Properties.DataSource = dt;
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.ValueMember = "ID";
        }
        void LoadEmployeeEarlyLate()
        {
            //if (cbApprovedStatusTBP.SelectedIndex == 1)
            //    StatusApprove = 1;
            //else if (cbApprovedStatusTBP.SelectedIndex == 2)
            //    StatusApprove = 0;
            StatusApprove = cbApprovedStatusTBP.SelectedIndex - 1;
            int departmentId = TextUtils.ToInt(cboDepartment.EditValue);
            //Thật sự là thừa thãi khánh bỏ
            //if (cbApprovedStatusTBP.SelectedIndex == 0)
            //{
            //    arrParamName = new string[] { "@FilterText", "@PageNumber", "@PageSize", "@Month", "@Year", "@DepartmentID", "@IDApprovedTP" };
            //    arrParamValue = new object[] { txtKeyword.Text, TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), (int)txtMonth.Value, (int)txtYear.Value, departmentId, IDApproved };
            //}
            //else
            //{
            //    arrParamName = new string[] { "@FilterText", "@PageNumber", "@PageSize", "@Month", "@Year", "@DepartmentID", "@IDApprovedTP", "@Status" };
            //    arrParamValue = new object[] { txtKeyword.Text, TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), (int)txtMonth.Value, (int)txtYear.Value, departmentId, IDApproved, StatusApprove };
            //}

            arrParamName = new string[] { "@FilterText", "@PageNumber", "@PageSize", "@Month", "@Year", "@DepartmentID", "@IDApprovedTP", "@Status" };
            arrParamValue = new object[] { txtKeyword.Text, TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), (int)txtMonth.Value, (int)txtYear.Value, departmentId, IDApproved, StatusApprove };
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeEarlyLate", "A", arrParamName, arrParamValue);
            grdData.DataSource = dt;
            if (dt.Rows.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(dt.Rows[0]["TotalPage"]);
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            frmEarlyLateDetail frm = new frmEarlyLateDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadEmployeeEarlyLate();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            string fullName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
            bool isApproved = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApprovedTP));
            //if (isApproved == true)
            //{
            //    MessageBox.Show(String.Format($"Nhân viên [{code} - {fullName}] đã được duyệt.\nVui lòng hủy duyệt trước khi sửa!"), TextUtils.Caption, MessageBoxButtons.OK,
            //            MessageBoxIcon.Information);
            //    return;
            //}
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            EmployeeEarlyLateModel model = (EmployeeEarlyLateModel)EmployeeEarlyLateBO.Instance.FindByPK(ID);
            frmEarlyLateDetail frm = new frmEarlyLateDetail();
            frm.earlylate = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadEmployeeEarlyLate();
                grvData.FocusedRowHandle = focusedRowHandle;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            string fullName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
            bool isApproved = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApprovedTP));
            if (isApproved == true)
            {
                MessageBox.Show(String.Format($"Nhân viên [{code} - {fullName}] đã được duyệt.\nVui lòng hủy duyệt trước khi xóa!"), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                return;
            }

            if (!grvData.IsDataRow(grvData.FocusedRowHandle)) return;

            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            if (ID == 0) return;

            if (MessageBox.Show(String.Format($"Bạn có chắc muốn xóa nhân viên [{code} - {fullName}] không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int[] rowIndex = grvData.GetSelectedRows();
                for (int i = 0; i < rowIndex.Length; i++)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colID));
                    EmployeeEarlyLateBO.Instance.Delete(id);
                }
                grvData.DeleteSelectedRows();
                grvData.FocusedRowHandle = focusedRowHandle;
            }
        }
        void approved(bool isApproved)
        {
            //string approved = isApproved == true ? "duyệt" : "hủy duyệt";
            //string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            //string fullName = code + " - " + TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));

            //if (MessageBox.Show(string.Format("Bạn có chắc muốn {0}  nhân viên [{1}] không ?", approved, fullName), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //    string sql = string.Format("UPDATE dbo.EmployeeEarlyLate SET IsApproved = {0} WHERE ID = {1}", isApproved ? 1 : 0, ID);
            //    TextUtils.ExcuteSQL(sql);
            //    if (isApproved == true)
            //        grvData.SetFocusedRowCellValue(colIsApproved, 1);
            //    else
            //        grvData.SetFocusedRowCellValue(colIsApproved, 0);
            //}
            string approved = isApproved == true ? "duyệt" : "hủy duyệt";
            int[] RowIndex = grvData.GetSelectedRows();

            for (int i = 0; i < RowIndex.Length; i++)
            {
                int departmentId = TextUtils.ToInt(grvData.GetRowCellValue(RowIndex[i], colDepartmentID));
                int employeeId = TextUtils.ToInt(grvData.GetRowCellValue(RowIndex[i], colEmployeeID));
                string employeeName = TextUtils.ToString(grvData.GetRowCellValue(RowIndex[i], colFullName));
                if (departmentId == 0 && employeeId == 0) continue;
                if (departmentId != Global.DepartmentID && Global.DepartmentID != 1)
                {
                    MessageBox.Show($"Nhân viên [{employeeName}] không thuộc phòng [{Global.DepartmentName.ToUpper()}].\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (employeeId == Global.EmployeeID)
                {
                    MessageBox.Show($"Bạn không được {approved} cho chính mình.\nVui lòng liên hệ cấp cao hơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }


            if (MessageBox.Show(string.Format("Bạn có chắc muốn {0} không ?", approved, Name), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (int i = 0; i < RowIndex.Length; i++)
                {
                    bool IsApprovedTP = TextUtils.ToBoolean(grvData.GetRowCellValue(RowIndex[i], colIsApprovedTP));
                    int ID = TextUtils.ToInt(grvData.GetRowCellValue(RowIndex[i], colID));
                    //if (IsApproved == true)
                    //{
                    //    epl.IsApproved = true;
                    //    //EmployeeEarlyLateBO.Instance.Update(epl);
                    //}
                    //string sql = string.Format($"UPDATE dbo.EmployeeEarlyLate SET IsApprovedTP = {IsApprovedTP?1:0} WHERE ID = {ID}");
                    string sql = string.Format("UPDATE dbo.EmployeeEarlyLate SET IsApprovedTP = {0} WHERE ID = {1}", isApproved ? 1 : 0, ID);
                    TextUtils.ExcuteSQL(sql);
                    LoadEmployeeEarlyLate();
                }
            }

            //string approved = isApproved == true ? "duyệt" : "hủy duyệt";
            //if (MessageBox.Show(string.Format("Bạn có chắc muốn {0} không ?", approved, Name), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //string sql = string.Format("UPDATE dbo.EmployeeEarlyLate SET IsApproved = {0} WHERE ID = {1}", isApproved ? 1 : 0, ID);
            //    TextUtils.ExcuteSQL(sql);
            //    //if (isApproved == true)
            //    //    grvData.SetFocusedRowCellValue(colIsApproved, 1);
            //    //else
            //    //    grvData.SetFocusedRowCellValue(colIsApproved, 0);
            //    LoadEmployeeEarlyLate();
            //}
        }
        private void btnUnapproved_Click(object sender, EventArgs e)
        {
            int[] rowIndex = grvData.GetSelectedRows();
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
            if (rowIndex.Length == 0)
                MessageBox.Show(String.Format("Vui lòng chọn nhân viên để huỷ duyệt!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            else if (rowIndex.Length == 1)
                MessageBox.Show(String.Format("Bạn có chắc muốn duyệt cho nhân viên [{0}] hay không!", name), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Question);
            else
                MessageBox.Show(String.Format("Bạn có chắc muốn duyệt cho những nhân viên này hay không!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Question);
            for (int i = 0; i < rowIndex.Length; i++)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colID));
                if (id == 0) continue;
                EmployeeEarlyLateModel employeeEarlyLate = (EmployeeEarlyLateModel)EmployeeEarlyLateBO.Instance.FindByPK(id);
                employeeEarlyLate.IsApproved = false;
                EmployeeEarlyLateBO.Instance.Update(employeeEarlyLate);

            }
            LoadEmployeeEarlyLate();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            LoadEmployeeEarlyLate();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            LoadEmployeeEarlyLate();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            LoadEmployeeEarlyLate();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            LoadEmployeeEarlyLate();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            LoadEmployeeEarlyLate();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadEmployeeEarlyLate();
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void grvData_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //if (e.Column == colTimeRegister)
            //{
            //    e.DisplayText = Decimal.Round(TextUtils.ToDecimal(e.Value), 0).ToString() + " (p)";
            //}
        }

        private void btnApproved_Click(object sender, EventArgs e)
        {
            int[] rowIndex = grvData.GetSelectedRows();
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
            if (rowIndex.Length == 0)
                MessageBox.Show(String.Format("Vui lòng chọn nhân viên để duyệt!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            else if (rowIndex.Length == 1)
                MessageBox.Show(String.Format("Bạn có chắc muốn duyệt cho nhân viên [{0}] hay không!", name), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Question);
            else
                MessageBox.Show(String.Format("Bạn có chắc muốn duyệt cho những nhân viên này hay không!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Question);
            for (int i = 0; i < rowIndex.Length; i++)
            {
                int departmentId = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colDepartmentID));
                string employeeName = TextUtils.ToString(grvData.GetRowCellValue(rowIndex[i], colFullName));
                int ApprovedTP = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colApprovedTP));
                if (departmentId == 0) continue;
                if (!Global.IsAdmin)
                {
                    if (departmentId != Global.DepartmentID && Global.DepartmentID != 1)
                    {
                        MessageBox.Show($"Nhân viên [{employeeName}] không thuộc phòng [{Global.DepartmentName.ToUpper()}].\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (ApprovedTP != Global.EmployeeID)
                    {
                        MessageBox.Show($"Bạn không thể duyệt cho nhân viên thuộc nhóm khác.\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                int id = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colID));
                if (id == 0) continue;
                bool IsApprovedTP = TextUtils.ToBoolean(grvData.GetRowCellValue(rowIndex[i], colIsApprovedTP));
                if (IsApprovedTP) continue;
                EmployeeEarlyLateModel employeeEarlyLate = (EmployeeEarlyLateModel)EmployeeEarlyLateBO.Instance.FindByPK(id);
                employeeEarlyLate.IsApprovedTP = true;
                EmployeeOnLeaveBO.Instance.Update(employeeEarlyLate);
            }
            LoadEmployeeEarlyLate();
        }
        private void tBPHuỷDuyệtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] rowIndex = grvData.GetSelectedRows();
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
            if (rowIndex.Length == 0)
                MessageBox.Show(String.Format("Vui lòng chọn nhân viên để huỷ duyệt!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            else if (rowIndex.Length == 1)
                MessageBox.Show(String.Format("Bạn có chắc muốn huỷ duyệt cho nhân viên [{0}] hay không!", name), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Question);
            else
                MessageBox.Show(String.Format("Bạn có chắc muốn huỷ duyệt cho những nhân viên này hay không!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Question);
            for (int i = 0; i < rowIndex.Length; i++)
            {
                int departmentId = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colDepartmentID));
                string employeeName = TextUtils.ToString(grvData.GetRowCellValue(rowIndex[i], colFullName));
                int ApprovedTP = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colApprovedTP));
                if (departmentId == 0) continue;
                if (!Global.IsAdmin)
                {
                    if (departmentId != Global.DepartmentID && Global.DepartmentID != 1)
                    {
                        MessageBox.Show($"Nhân viên [{employeeName}] không thuộc phòng [{Global.DepartmentName.ToUpper()}].\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (ApprovedTP != Global.EmployeeID)
                    {
                        MessageBox.Show($"Bạn không thể duyệt cho nhân viên thuộc nhóm khác.\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                int id = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colID));
                if (id == 0) continue;
                bool IsApprovedTP = TextUtils.ToBoolean(grvData.GetRowCellValue(rowIndex[i], colIsApprovedTP));
                if (!IsApprovedTP) continue;
                EmployeeEarlyLateModel employeeEarlyLate = (EmployeeEarlyLateModel)EmployeeEarlyLateBO.Instance.FindByPK(id);
                bool IsApproved = TextUtils.ToBoolean(grvData.GetRowCellValue(rowIndex[i], colIsApproved));
                if (IsApproved)
                {
                    MessageBox.Show($"HR chưa huỷ duyệt.\nVui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    employeeEarlyLate.IsApprovedTP = false;
                    EmployeeOnLeaveBO.Instance.Update(employeeEarlyLate);

                }
            }
            LoadEmployeeEarlyLate();
        }

        private void btnApprovedHR_Click(object sender, EventArgs e)
        {
            int[] rowIndex = grvData.GetSelectedRows();
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
            if (rowIndex.Length == 0)
                MessageBox.Show(String.Format("Vui lòng chọn nhân viên để duyệt!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            else if (rowIndex.Length == 1)
                MessageBox.Show(String.Format("Bạn có chắc muốn duyệt cho nhân viên [{0}] không!", name), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Question);
            else
                MessageBox.Show(String.Format("Bạn có chắc muốn duyệt cho những nhân viên này không!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Question);
            for (int i = 0; i < rowIndex.Length; i++)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colID));
                if (id == 0) continue;
                EmployeeEarlyLateModel employeeEarlyLate = (EmployeeEarlyLateModel)EmployeeEarlyLateBO.Instance.FindByPK(id);
                bool IsApprovedTP = TextUtils.ToBoolean(grvData.GetRowCellValue(rowIndex[i], colIsApprovedTP));
                if (!IsApprovedTP)
                {
                    MessageBox.Show($"Trưởng bộ phận chưa duyệt.\nVui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    employeeEarlyLate.IsApproved = true;
                    employeeEarlyLate.ApprovedID = Global.EmployeeID;
                    EmployeeEarlyLateBO.Instance.Update(employeeEarlyLate);
                }
            }
            LoadEmployeeEarlyLate();
        }

        private void HRHuỷDuyệtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] rowIndex = grvData.GetSelectedRows();
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
            if (rowIndex.Length == 0)
                MessageBox.Show(String.Format("Vui lòng chọn nhân viên để huỷ duyệt!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            else if (rowIndex.Length == 1)
                MessageBox.Show(String.Format("Bạn có chắc muốn duyệt cho nhân viên [{0}] hay không!", name), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Question);
            else
                MessageBox.Show(String.Format("Bạn có chắc muốn duyệt cho những nhân viên này hay không!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Question);
            for (int i = 0; i < rowIndex.Length; i++)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colID));
                if (id == 0) continue;
                EmployeeEarlyLateModel employeeEarlyLate = (EmployeeEarlyLateModel)EmployeeEarlyLateBO.Instance.FindByPK(id);
                employeeEarlyLate.IsApproved = false;
                EmployeeEarlyLateBO.Instance.Update(employeeEarlyLate);

            }
            LoadEmployeeEarlyLate();
        }

        private void cbApprovedStatusTBP_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadEmployeeEarlyLate();
        }

        private void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {
            LoadEmployeeEarlyLate();
        }

        private void txtMonth_ValueChanged(object sender, EventArgs e)
        {
            LoadEmployeeEarlyLate();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = Path.Combine(f.SelectedPath, $"DanhSachDiMuonVeSom_T{txtMonth.Value}_{txtYear.Value}.xlsx");
                //string filepath = @"C:\Users\Admin\Desktop\Bảng công Công ty RTC - APR - MVI - YONKO FINAL Tháng 8.2023 FINAL.xlsx";

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdData;

                try
                {
                    using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
                    {
                        CompositeLink compositeLink = new CompositeLink(printingSystem);
                        compositeLink.Links.Add(printableComponentLink1);

                        //compositeLink.PrintingSystem.XlSheetCreated += PrintingSystem_XlSheetCreated;

                        compositeLink.CreatePageForEachLink();
                        optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;

                        compositeLink.PrintingSystem.SaveDocument(filepath);
                        compositeLink.ExportToXlsx(filepath, optionsEx);
                        Process.Start(filepath);
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            //SaveFileDialog sfd = new SaveFileDialog();
            //sfd.Filter = "Excel Files (*.xls, *.xls)|*.xls;*.xls";
            //sfd.FileName = $"DanhSachDiMuonVeSom_T{txtMonth.Value}_{txtYear.Value}";

            //if (sfd.ShowDialog() == DialogResult.OK)
            //{
            //    XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
            //    optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
            //    grvData.OptionsPrint.AutoWidth = false;
            //    grvData.OptionsPrint.ExpandAllDetails = false;
            //    grvData.OptionsPrint.PrintDetails = true;
            //    grvData.OptionsPrint.UsePrintStyles = true;
            //    try
            //    {
            //        grvData.ExportToXls(sfd.FileName, optionsEx);
            //        Process.Start(sfd.FileName);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }

            //}
        }
    }
}
