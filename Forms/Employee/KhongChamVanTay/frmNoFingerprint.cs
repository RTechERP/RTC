using BMS.Business;
using BMS.Model;
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
    public partial class frmNoFingerprint : _Forms
    {
        int StatusApprove, IDApproved;
        string[] arrParamName = null;
        object[] arrParamValue = null;
        public frmNoFingerprint()
        {
            InitializeComponent();
        }

        private void frmNoFingerprint_Load(object sender, EventArgs e)
        {
            LoadDepartment();
            IDApproved = Global.EmployeeID;
            btnApprovedHR.Visible = false;

            if (Global.IsAdmin)
            {
                btnApprovedHR.Visible = true;
            }

            if (Global.DepartmentID == 6)//phòng nhân sự
            {
                cbApprovedStatusTBP.SelectedIndex = 0;//tất cả
                IDApproved = 0;
                btnApprovedHR.Visible = true;
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
            //if (cbApprovedStatusTBP.SelectedIndex == 1)
            //    StatusApprove = 1;
            //else if (cbApprovedStatusTBP.SelectedIndex == 2)
            //    StatusApprove = 0;

            StatusApprove = cbApprovedStatusTBP.SelectedIndex - 1;
            int departmentId = TextUtils.ToInt(cbDepartment.EditValue);
            DateTime dateTimeS = new DateTime(dtpStart.Value.Year, dtpStart.Value.Month, dtpStart.Value.Day, 00, 00, 00).AddSeconds(-1);
            DateTime dateTimeE = new DateTime(dtpEnd.Value.Year, dtpEnd.Value.Month, dtpEnd.Value.Day, 23, 59, 59).AddSeconds(+1);
            //if(cbApprovedStatusTBP.SelectedIndex == 0)
            //{
            //    arrParamName = new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@Keyword", "@DepartmentID", "@IDApprovedTP" };
            //    arrParamValue = new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), dateTimeS, dateTimeE,
            //        txtFilterText.Text, departmentId, IDApproved };
            //}    
            //else
            //{
            //    arrParamName = new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@Keyword", "@DepartmentID", "@IDApprovedTP", "@Status" };
            //    arrParamValue = new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), dateTimeS, dateTimeE,
            //        txtFilterText.Text, departmentId, IDApproved,StatusApprove };
            //}
            //
            arrParamName = new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@Keyword", "@DepartmentID", "@IDApprovedTP", "@Status" };
            arrParamValue = new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), dateTimeS, dateTimeE,
                    txtFilterText.Text, departmentId, IDApproved,StatusApprove };
            DataSet dts = TextUtils.LoadDataSetFromSP("spGetEmployeeNoFingerprint", arrParamName, arrParamValue);
            grdData.DataSource = dts.Tables[0];
            if (dts.Tables[0].Rows.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(dts.Tables[1].Rows[0]["TotalPage"]);
        }
        void LoadDepartment()
        {
            DataTable dtDepartment = TextUtils.Select($"SELECT ID, Code, Name FROM Department");
            cbDepartment.Properties.DataSource = dtDepartment;
            cbDepartment.Properties.DisplayMember = "Name";
            cbDepartment.Properties.ValueMember = "ID";
        }


        private void btnNew_Click(object sender, EventArgs e)
        {
            frmNoFingerprintDetail frm = new frmNoFingerprintDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //if (TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApprovedTP)) == true)
            //{
            //    MessageBox.Show(string.Format("Đã duyệt không thể sửa"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //else
            //{
            //    int rowHandle = grvData.FocusedRowHandle;
            //    int id = (int)grvData.GetFocusedRowCellValue(colID);
            //    EmployeeNoFingerprintModel Model = (EmployeeNoFingerprintModel)EmployeeNoFingerprintBO.Instance.FindByPK(id);
            //    frmNoFingerprintDetail frm = new frmNoFingerprintDetail();
            //    frm.nofinger = Model;
            //    if (frm.ShowDialog() == DialogResult.OK)
            //    {
            //        loadData();
            //        grvData.FocusedRowHandle = rowHandle;
            //    }
            //}

            int rowHandle = grvData.FocusedRowHandle;
            int id = (int)grvData.GetFocusedRowCellValue(colID);
            EmployeeNoFingerprintModel Model = (EmployeeNoFingerprintModel)EmployeeNoFingerprintBO.Instance.FindByPK(id);
            frmNoFingerprintDetail frm = new frmNoFingerprintDetail();
            frm.nofinger = Model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
                grvData.FocusedRowHandle = rowHandle;
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int strID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApprovedTP)) == true)
            {
                MessageBox.Show(string.Format("Đã duyệt không thể xoá"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (MessageBox.Show(string.Format("Bạn có chắc muốn xóa  không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    grvData.DeleteSelectedRows();
                    EmployeeNoFingerprintBO.Instance.Delete(strID);
                }
            }
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
                EmployeeNoFingerprintModel EmployeeNoFingerprintModel = (EmployeeNoFingerprintModel)EmployeeNoFingerprintBO.Instance.FindByPK(id);
                EmployeeNoFingerprintModel.IsApprovedTP = true;
                EmployeeOnLeaveBO.Instance.Update(EmployeeNoFingerprintModel);
                
            }
            loadData();
        }


        private void btnUnapproved_Click(object sender, EventArgs e)
        {
            int[] rowIndex = grvData.GetSelectedRows();
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));

            if (rowIndex.Length == 0)
                MessageBox.Show(String.Format("Vui lòng chọn nhân viên để hủy duyệt!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            else if (rowIndex.Length == 1)
                MessageBox.Show(String.Format("Bạn có chắc muốn hủy duyệt cho nhân viên [{0}] hay không!", name), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Question);
            else
                MessageBox.Show(String.Format("Bạn có chắc muốn hủy duyệt cho những nhân viên này hay không!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Question);
            for (int i = 0; i < rowIndex.Length; i++)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colID));
                if (id == 0) continue;
                bool IsApprovedHR = TextUtils.ToBoolean(grvData.GetRowCellValue(rowIndex[i], colIsApprovedHR));
                if (!IsApprovedHR) continue;
                EmployeeNoFingerprintModel EmployeeNoFingerprintModel = (EmployeeNoFingerprintModel)EmployeeNoFingerprintBO.Instance.FindByPK(id);
                EmployeeNoFingerprintModel.IsApprovedHR = false;
                EmployeeOnLeaveBO.Instance.Update(EmployeeNoFingerprintModel);

            }
            loadData();
        }
       
        void Approved(bool isApproved)
        {
            string approved = isApproved == true ? "duyệt" : "hủy duyệt";
            int[] RowIndex = grvData.GetSelectedRows();

            for (int i = 0; i < RowIndex.Length; i++)
            {
                string fullName = TextUtils.ToString(grvData.GetRowCellValue(RowIndex[i], colFullName));
                int departmentId = TextUtils.ToInt(grvData.GetRowCellValue(RowIndex[i], colDepartmentID));
                int employeeId = TextUtils.ToInt(grvData.GetRowCellValue(RowIndex[i], colEmployeeID));
                if (departmentId == 0 && employeeId == 0) continue;
                if (departmentId != Global.DepartmentID && Global.DepartmentID != 1)
                {
                    MessageBox.Show($"Nhân viên [{fullName}] không thuộc phòng [{Global.DepartmentName.ToUpper()}].\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (employeeId == Global.EmployeeID)
                {
                    MessageBox.Show($"Bạn không được {approved} cho chính mình.\nVui lòng liên hệ cấp cao hơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show(string.Format($"Bạn có chắc muốn {approved} nhân viên {fullName} không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int ID = TextUtils.ToInt(grvData.GetRowCellValue(RowIndex[i],colID));
                    string sql = string.Format("UPDATE dbo.EmployeeNoFingerprint SET IsApprovedTP = {0} , ApprovedTP = {1} WHERE ID = {2}", isApproved ? 1 : 0, Global.UserID, ID);
                    TextUtils.ExcuteSQL(sql);
                    loadData();
                }
            }       
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            loadData();
            
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadData();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {

            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadData();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadData();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {

            txtPageNumber.Text = "1";

            loadData();
        }

        private void tBPHuỷDuyệtToolStripMenuItem_Click(object sender, EventArgs e)
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
                EmployeeNoFingerprintModel EmployeeNoFingerprintModel = (EmployeeNoFingerprintModel)EmployeeNoFingerprintBO.Instance.FindByPK(id);
                bool IsApprovedHR = TextUtils.ToBoolean(grvData.GetRowCellValue(rowIndex[i], colIsApprovedHR));
                if (IsApprovedHR)
                {
                    MessageBox.Show($"HR chưa huỷ duyệt.\nVui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    EmployeeNoFingerprintModel.IsApprovedTP = false;
                    EmployeeNoFingerprintBO.Instance.Update(EmployeeNoFingerprintModel);
                    
                }
                
            }
            loadData();
        }

        private void btnApprovedHR_Click(object sender, EventArgs e)
        {
            int[] RowIndex = grvData.GetSelectedRows();
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
            if (RowIndex.Length == 0)
                MessageBox.Show(String.Format("Vui lòng chọn nhân viên để duyệt!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            else if (RowIndex.Length == 1)
                MessageBox.Show(String.Format("Bạn có chắc muốn duyệt cho nhân viên [{0}] hay không!", name), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Question);
            else
                MessageBox.Show(String.Format("Bạn có chắc muốn duyệt cho những nhân viên này hay không!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Question);
            for (int i = 0; i < RowIndex.Length; i++)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(RowIndex[i], colID));
                if (id == 0) continue;
                EmployeeNoFingerprintModel EmployeeNoFingerprintModel = (EmployeeNoFingerprintModel)EmployeeNoFingerprintBO.Instance.FindByPK(id);
                bool IsApprovedTP = TextUtils.ToBoolean(grvData.GetRowCellValue(RowIndex[i], colIsApprovedTP));
                if (!IsApprovedTP)
                {
                    MessageBox.Show($"Trưởng phòng chưa duyệt.\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    EmployeeNoFingerprintModel.IsApprovedHR = true;
                    EmployeeNoFingerprintModel.ApprovedHR = Global.EmployeeID;
                    EmployeeNoFingerprintBO.Instance.Update(EmployeeNoFingerprintModel);
                    
                }
            }
            loadData();
        }

        private void hRHuỷDuyệtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] RowIndex = grvData.GetSelectedRows();
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
            if (RowIndex.Length == 0)
                MessageBox.Show(String.Format("Vui lòng chọn nhân viên để huỷ duyệt!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            else if (RowIndex.Length == 1)
                MessageBox.Show(String.Format("Bạn có chắc muốn duyệt cho nhân viên [{0}] hay không!", name), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Question);
            else
                MessageBox.Show(String.Format("Bạn có chắc muốn duyệt cho những nhân viên này hay không!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Question);
            for (int i = 0; i < RowIndex.Length; i++)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(RowIndex[i], colID));
                if (id == 0) continue;
                EmployeeNoFingerprintModel EmployeeNoFingerprintModel = (EmployeeNoFingerprintModel)EmployeeNoFingerprintBO.Instance.FindByPK(id);
                EmployeeNoFingerprintModel.IsApprovedHR = false;
                EmployeeNoFingerprintModel.ApprovedHR = 0;
                EmployeeNoFingerprintBO.Instance.Update(EmployeeNoFingerprintModel);
                
            }
            loadData();
        }

        private void cbApprovedStatusTBP_SelectedIndexChanged(object sender, EventArgs e)
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

        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            sfd.FileName = $"DanhSachQuenChamCong_T{dtpStart.Value.Month}_{dtpStart.Value.Year}";
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
    }
}
