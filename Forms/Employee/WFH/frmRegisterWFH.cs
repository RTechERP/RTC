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
    public partial class frmRegisterWFH : _Forms
    {
        int StatusApprove, IDApproved;
        string[] arrParamName = null;
        object[] arrParamValue = null;
        public frmRegisterWFH()
        {
            InitializeComponent();
        }

        private void frmRegisterWFH_Load(object sender, EventArgs e)
        {
            nmrYear.Value = DateTime.Now.Year;
            nmrMonth.Value = DateTime.Now.Month;
            txtPageNumber.Text = "1";
            LoadDepartment();
            IDApproved = Global.EmployeeID;
            btnIsApprovedHR.Visible = false;
            if (Global.DepartmentID == 6)//phòng nhân sự
            {
                cbApprovedStatusTBP.SelectedIndex = 0;//tất cả
                IDApproved = 0;
                btnIsApprovedHR.Visible = true;
                cbDepartment.EditValue = 0;
            }
            else if (Global.DepartmentID == 1)//phòng giám đốc
            {
                cbApprovedStatusTBP.SelectedIndex = 1;//chưa duyệt
                IDApproved = 0;
                cbDepartment.EditValue = 0;
            }
            else if (Global.IsAdmin)
            {
                cbApprovedStatusTBP.SelectedIndex = 0;//tất cả
                IDApproved = 0;
                btnIsApprovedHR.Visible = true;
            }
            else
            {
                cbDepartment.EditValue = Global.DepartmentID;
                cbApprovedStatusTBP.SelectedIndex = 1;
            }
            loadData();

            btnDel.Click += new EventHandler(btnDelete_Click);
            btnNew.Click += new EventHandler(btnEdit_Click);
        }
        private void loadData()
        {
            //if (cbApprovedStatusTBP.SelectedIndex == 1)
            //    StatusApprove = 1;
            //else if (cbApprovedStatusTBP.SelectedIndex == 2)
            //    StatusApprove = 0;
            StatusApprove = cbApprovedStatusTBP.SelectedIndex - 1;
            int departmentId = TextUtils.ToInt(cbDepartment.EditValue);
            //if(cbApprovedStatusTBP.SelectedIndex == 0)
            //{
            //    arrParamName = new string[] { "@PageNumber", "@PageSize", "@Year", "@Month", "@Keyword", "@DepartmentID", "@IDApprovedTP" };
            //    arrParamValue = new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), nmrYear.Value,
            //        nmrMonth.Value, txtSearch.Text.Trim(), departmentId, IDApproved };
            //}   
            //else
            //{
            //    arrParamName = new string[] { "@PageNumber", "@PageSize", "@Year", "@Month", "@Keyword", "@DepartmentID", "@IDApprovedTP", "@Status" };
            //    arrParamValue = new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), nmrYear.Value,
            //        nmrMonth.Value, txtSearch.Text.Trim(), departmentId, IDApproved, StatusApprove };
            //}
            //
            arrParamName = new string[] { "@PageNumber", "@PageSize", "@Year", "@Month", "@Keyword", "@DepartmentID", "@IDApprovedTP", "@Status" };
            arrParamValue = new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), nmrYear.Value,
                    nmrMonth.Value, txtSearch.Text.Trim(), departmentId, IDApproved, StatusApprove };
            DataSet ds = TextUtils.LoadDataSetFromSP("spGetWFH", arrParamName, arrParamValue);
            grdData.DataSource = ds.Tables[0];
            if (ds.Tables.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(ds.Tables[1].Rows[0]["TotalPage"]);
        }
        void LoadDepartment()
        {
            DataTable dtDepartmentID = TextUtils.Select($"SELECT ID, Code, Name FROM Department");
            cbDepartment.Properties.DataSource = dtDepartmentID;
            cbDepartment.Properties.DisplayMember = "Name";
            cbDepartment.Properties.ValueMember = "ID";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            EmployeeWFHModel model = new EmployeeWFHModel();
            frmWFHDetail frm = new frmWFHDetail();
            frm.wfh = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            bool isApproved = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApprovedTP));
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colEmployeeName));
            if (ID == 0) return;
            //if (isApproved)
            //{
            //    MessageBox.Show(String.Format("Nhân viên [{0}-{1}] đã được duyệt.\nVui lòng huỷ duyệt trước khi sửa!", code, name), TextUtils.Caption, MessageBoxButtons.OK,
            //            MessageBoxIcon.Question);
            //    return;
            //}
            //EmployeeWFHModel model = (EmployeeWFHModel)EmployeeWFHBO.Instance.FindByPK(ID);
            EmployeeWFHModel model = SQLHelper<EmployeeWFHModel>.FindByID(ID);
            frmWFHDetail frm = new frmWFHDetail();
            frm.wfh = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
                grvData.FocusedRowHandle = focusedRowHandle;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool isApproved = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApprovedTP));
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colEmployeeName));
            if (isApproved == true)
            {
                MessageBox.Show(String.Format("Nhân viên [{0}-{1}] đã được duyệt.\nVui lòng huỷ duyệt trước khi xóa!", code, name), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Question);
                return;
            }


            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn xóa nhân viên [{0}-{1}] không?", code, name), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvData.DeleteSelectedRows();
                //EmployeeWFHBO.Instance.Delete(ID);
                SQLHelper<EmployeeWFHModel>.DeleteModelByID(ID);
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

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
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

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFind_Click(null, null);
            }
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnIsApproved_Click(object sender, EventArgs e)
        {
            int[] rowIndex = grvData.GetSelectedRows();
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colEmployeeName));
            if (rowIndex.Length == 0)
                MessageBox.Show(String.Format("Vui lòng chọn nhân viên để duyệt!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            else if (rowIndex.Length == 1)
                MessageBox.Show(String.Format("Bạn có chắc muốn duyệt cho nhân viên [{0}] hay không!", name), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Question);
            else
                MessageBox.Show(String.Format("Bạn có chắc muốn duyệt cho những nhân viên này hay không!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Question);
            for (int i = 0; i < rowIndex.Length; i++)
            {
                int departmentId = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colDepartmentID));
                string employeeName = TextUtils.ToString(grvData.GetRowCellValue(rowIndex[i], colEmployeeName));
                int ApprovedTP = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colApprovedID));
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
                EmployeeWFHModel EmployeeWFHModel = (EmployeeWFHModel)EmployeeWFHBO.Instance.FindByPK(id);
                bool IsApprovedTP = TextUtils.ToBoolean(grvData.GetRowCellValue(rowIndex[i], colIsApprovedTP));
                if (IsApprovedTP) continue;
                EmployeeWFHModel.IsApproved = true;
                EmployeeWFHBO.Instance.Update(EmployeeWFHModel);

            }
            loadData();
        }
        void approved(bool isApproved)
        {
            string approved = isApproved == true ? "duyệt" : "hủy duyệt";
            int[] RowIndex = grvData.GetSelectedRows();
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colEmployeeName));
            if (RowIndex.Length == 1)
                MessageBox.Show(string.Format("Bạn có chắc muốn {0} duyệt nhân viên [{1}]?", isApproved ? "" : "bỏ", name), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            else
                MessageBox.Show(string.Format("Bạn có chắc muốn {0} duyệt những nhân viên này không?", isApproved ? "" : "bỏ"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            for (int i = 0; i < RowIndex.Length; i++)
            {
                int departmentId = TextUtils.ToInt(grvData.GetRowCellValue(RowIndex[i], colDepartmentID));
                int ApprovedTP = TextUtils.ToInt(grvData.GetRowCellValue(RowIndex[i], colApprovedID));
                if (departmentId == 0 && ApprovedTP == 0) continue;
                if (departmentId != Global.DepartmentID && Global.DepartmentID != 1)
                {
                    MessageBox.Show($"Nhân viên [{name}] không thuộc phòng [{Global.DepartmentName.ToUpper()}].\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (ApprovedTP != Global.EmployeeID)
                {
                    MessageBox.Show($"Bạn không được {approved} cho nhân viên thuộc nhóm khác.\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                bool isApprovedHR = TextUtils.ToBoolean(grvData.GetRowCellValue(RowIndex[i], colIsApprovedHR));
                if (isApproved == false && isApprovedHR == true) continue;
                int ID = TextUtils.ToInt(grvData.GetRowCellValue(RowIndex[i], colID));
                bool isApprovedTP = TextUtils.ToBoolean(grvData.GetRowCellValue(RowIndex[i], colIsApprovedTP));
                //if (isApprovedTP) continue;
                string sql = string.Format(@"UPDATE EmployeeWFH SET IsApproved = {0} WHERE ID = {1}", isApproved ? 1 : 0, ID);
                TextUtils.ExcuteSQL(sql);
                loadData();
            }
        }

        private void btnCancelApproved_Click(object sender, EventArgs e)
        {
            approved(false);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            sfd.FileName = $"DanhSachDangKyWFH_T{nmrMonth.Value}_{nmrYear.Value}";
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

        private void tBPHuỷDuyệtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] rowIndex = grvData.GetSelectedRows();
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colEmployeeName));
            if (rowIndex.Length == 0)
                MessageBox.Show(String.Format("Vui lòng chọn nhân viên để huỷ duyệt!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            else if (rowIndex.Length == 1)
                MessageBox.Show(String.Format("Bạn có chắc muốn huỷ duyệt cho nhân viên [{0}] hay không!", name), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Question);
            else
                MessageBox.Show(String.Format("Bạn có chắc muốn huỷ duyệt cho những nhân viên này hay không!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Question);
            for (int i = 0; i < rowIndex.Length; i++)
            {
                int departmentId = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colDepartmentID));
                string employeeName = TextUtils.ToString(grvData.GetRowCellValue(rowIndex[i], colEmployeeName));
                int ApprovedTP = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colApprovedID));
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
                EmployeeWFHModel EmployeeWFHModel = (EmployeeWFHModel)EmployeeWFHBO.Instance.FindByPK(id);
                bool IsApprovedTP = TextUtils.ToBoolean(grvData.GetRowCellValue(rowIndex[i], colIsApprovedTP));
                if (!IsApprovedTP) continue;
                bool IsApprovedHR = TextUtils.ToBoolean(grvData.GetRowCellValue(rowIndex[i], colIsApprovedHR));
                if (IsApprovedHR)
                {
                    MessageBox.Show($"HR chưa huỷ duyệt.\nVui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    EmployeeWFHModel.IsApproved = false;
                    EmployeeWFHBO.Instance.Update(EmployeeWFHModel);

                }

            }
            loadData();
        }
        private void btnIsApprovedHR_Click(object sender, EventArgs e)
        {
            int[] RowIndex = grvData.GetSelectedRows();
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colEmployeeName));
            if (RowIndex.Length == 0)
                MessageBox.Show(String.Format("Vui lòng chọn nhân viên để duyệt!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            else if (RowIndex.Length == 1)
                MessageBox.Show(string.Format("Bạn có chắc muốn duyệt nhân viên [{0}]?", name), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            else
                MessageBox.Show(string.Format("Bạn có chắc muốn duyệt những nhân viên này không ?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            for (int i = 0; i < RowIndex.Length; i++)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(RowIndex[i], colID));
                if (id == 0) continue;
                //EmployeeWFHModel EmployeeWFHModel = (EmployeeWFHModel)EmployeeWFHBO.Instance.FindByPK(id);
                EmployeeWFHModel EmployeeWFHModel = SQLHelper<EmployeeWFHModel>.FindByID(id);
                bool IsApprovedTP = TextUtils.ToBoolean(grvData.GetRowCellValue(RowIndex[i], colIsApprovedTP));
                if (!IsApprovedTP)
                {
                    MessageBox.Show($"Trưởng phòng chưa duyệt.\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                EmployeeWFHModel.IsApprovedHR = true;
                EmployeeWFHModel.ApprovedHR = Global.EmployeeID;
                //EmployeeWFHBO.Instance.Update(EmployeeWFHModel);
                SQLHelper<EmployeeWFHModel>.Update(EmployeeWFHModel);

            }
            loadData();
        }

        private void hRHuỷDuyệtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] RowIndex = grvData.GetSelectedRows();
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colEmployeeName));
            if (RowIndex.Length == 0)
                MessageBox.Show(String.Format("Vui lòng chọn nhân viên để huỷ duyệt!"), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            else if (RowIndex.Length == 1)
                MessageBox.Show(string.Format("Bạn có chắc muốn duyệt nhân viên [{0}]?", name), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            else
                MessageBox.Show(string.Format("Bạn có chắc muốn duyệt những nhân viên này không ?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            for (int i = 0; i < RowIndex.Length; i++)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(RowIndex[i], colID));
                if (id == 0) continue;
                EmployeeWFHModel EmployeeWFHModel = (EmployeeWFHModel)EmployeeWFHBO.Instance.FindByPK(id);
                EmployeeWFHModel.IsApprovedHR = false;
                EmployeeWFHModel.ApprovedHR = 0;
                EmployeeWFHBO.Instance.Update(EmployeeWFHModel);

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

        private void nmrMonth_ValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void grvData_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }
    }
}
