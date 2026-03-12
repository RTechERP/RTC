using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmSalaryAdvance : _Forms
    {
        int StatusApprove, IDApproved;
        string[] arrParamName = null;
        object[] arrParamValue = null;
        public frmSalaryAdvance()
        {
            InitializeComponent();
        }


        private void frmSalaryAdvance_Load(object sender, EventArgs e)
        {
            LoadDepartment();
            IDApproved = Global.EmployeeID;
            cbApprovedStatusTBP.SelectedIndex = 1; //Chưa duyệt
            btnIsApprovedTP.Visible = btnCancelApprovedTP.Visible = btnIsApprovedHR.Visible = btnCancelApprovedHR.Visible = btnIsApprovedKT.Visible = btnCancelApprovedKT.Visible = false;
            if (Global.DepartmentID == 6) //HCNS
            {
                cbDepartment.EditValue = 0;
                cbApprovedStatusTBP.SelectedIndex = 2;//Trưởng bộ phận đã duyệt
                btnIsApprovedHR.Visible = btnCancelApprovedHR.Visible = true;
                IDApproved = 0;
            }
            else if (Global.DepartmentID == 5) //Kế toán
            {
                cbDepartment.EditValue = 0;
                cbApprovedStatusTBP.SelectedIndex = 2;//Trưởng bộ phận đã duyệt
                btnIsApprovedKT.Visible = btnCancelApprovedKT.Visible = true;
                IDApproved = 0;
            }
            else
            {
                cbDepartment.EditValue = Global.DepartmentID;
                btnIsApprovedTP.Visible = btnCancelApprovedTP.Visible = true;
            }
            loadData();
        }
        void loadData()
        {
            //if (cbApprovedStatusTBP.SelectedIndex == 1) //Đã duyệt
            //    StatusApprove = 1;
            //else if (cbApprovedStatusTBP.SelectedIndex == 2)  //Chưa duyệt
            //    StatusApprove = 0;
            StatusApprove = cbApprovedStatusTBP.SelectedIndex - 1;
            int departmentId = TextUtils.ToInt(cbDepartment.EditValue);
            //if (cbApprovedStatusTBP.SelectedIndex == 0) //Tất cả
            //{
            //    arrParamName = new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@Keyword", "@DepartmentID", "@IDApprovedTP" };
            //    arrParamValue = new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), dtpStart.Value,
            //            dtpEnd.Value, txtFilterText.Text, departmentId, IDApproved };
            //}
            //else
            //{
            //    arrParamName = new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@Keyword", "@DepartmentID", "@IDApprovedTP", "@Status" };
            //    arrParamValue = new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), dtpStart.Value,
            //            dtpEnd.Value, txtFilterText.Text, departmentId, IDApproved, StatusApprove};
            //}
            arrParamName = new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@Keyword", "@DepartmentID", "@IDApprovedTP", "@Status" };
            arrParamValue = new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), dtpStart.Value,
                        dtpEnd.Value, txtFilterText.Text, departmentId, IDApproved, StatusApprove};
            DataSet dts = TextUtils.LoadDataSetFromSP("spGetSalaryAdvance", arrParamName, arrParamValue);
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

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadData();
            txtFilterText.Text = "";
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

        private void btnThanhToan_Click(object sender, EventArgs e)
        {

            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id <= 0) return;
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullname));
            EmployeeSalaryAdvanceModel salaryAdvanceModel = new EmployeeSalaryAdvanceModel();
            if (id > 0)
            {
                salaryAdvanceModel = (EmployeeSalaryAdvanceModel)EmployeeSalaryAdvanceBO.Instance.FindByPK(id);
            }
            if (!salaryAdvanceModel.IsApproved_KT || !salaryAdvanceModel.IsApproved_HR || !salaryAdvanceModel.IsApproved_TP)
            {
                MessageBox.Show(string.Format("Nhân viên [{0}-{1}] chưa được duyệt.\nVui lòng kiểm tra lại! ", code, name), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (salaryAdvanceModel.IsPayed)
                {
                    MessageBox.Show(string.Format("Nhân viên [{0}-{1}] đã được thanh toán.\nVui lòng thử lại sau! ", code, name), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {

                    if (MessageBox.Show(string.Format("Bạn có muốn thanh toán cho nhân viên [{0}-{1}] không?", code, name), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        salaryAdvanceModel.IsPayed = true;
                        salaryAdvanceModel.DatePayed = DateTime.Now;
                        EmployeeSalaryAdvanceBO.Instance.Update(salaryAdvanceModel);
                        loadData();
                        //grvData.SetFocusedRowCellValue(colIsPayed, true);
                        //grvData.FocusedRowHandle = -1;

                    }
                }
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
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

        private void btnIsApprovedTP_Click(object sender, EventArgs e)
        {
            bool isApproved = true;
            string approved = isApproved == true ? "duyệt" : "hủy duyệt";
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullname));
            int DepartmentID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colDepartmentID));
            int employeeId = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
            EmployeeSalaryAdvanceModel salaryAdvanceModel = new EmployeeSalaryAdvanceModel();
            if (id > 0)
            {
                salaryAdvanceModel = (EmployeeSalaryAdvanceModel)EmployeeSalaryAdvanceBO.Instance.FindByPK(id);
            }
            if (salaryAdvanceModel.IsPayed)
            {
                MessageBox.Show(string.Format("Nhân viên [{0}-{1}] đã được thanh toán!", code, name), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (salaryAdvanceModel.IsApproved_TP == true)
            {
                MessageBox.Show(string.Format("Nhân viên [{0}-{1}] đã được Trưởng bộ phận duyệt! ", code, name), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (DepartmentID != Global.DepartmentID && Global.DepartmentID != 1)
            {
                MessageBox.Show($"Nhân viên [{name}] không thuộc phòng [{Global.DepartmentName.ToUpper()}].\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (employeeId == Global.EmployeeID)
            {
                MessageBox.Show($"Bạn không được {approved} cho chính mình.\nVui lòng liên hệ cấp cao hơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show(string.Format("Bạn có muốn duyệt nhân viên [{0}-{1}] không?", code, name), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                salaryAdvanceModel.IsApproved_TP = true;
                salaryAdvanceModel.ApprovedTP = Global.EmployeeID;

                EmployeeSalaryAdvanceBO.Instance.Update(salaryAdvanceModel);
                grvData.SetFocusedRowCellValue(colIsApproved_TP, true);
                grvData.FocusedRowHandle = -1;
            }
        }

        private void btnIsApprovedHR_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullname));
            EmployeeSalaryAdvanceModel salaryAdvanceModel = new EmployeeSalaryAdvanceModel();
            if (id > 0)
            {
                salaryAdvanceModel = (EmployeeSalaryAdvanceModel)EmployeeSalaryAdvanceBO.Instance.FindByPK(id);
            }
            if (salaryAdvanceModel.IsPayed)
            {
                MessageBox.Show(string.Format("Nhân viên [{0}-{1}] đã được thanh toán! ", code, name), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (salaryAdvanceModel.IsApproved_HR == true)
            {
                MessageBox.Show(string.Format("Nhân viên [{0}-{1}] đã được Phòng HR duyệt! ", code, name), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!salaryAdvanceModel.IsApproved_TP)
            {
                MessageBox.Show(string.Format("Trưởng bộ phận chưa duyệt.\nVui lòng liên hệ Trưởng bộ phận! "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show(string.Format("Bạn có muốn duyệt nhân viên [{0}-{1}] không?", code, name), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                salaryAdvanceModel.IsApproved_HR = true;
                salaryAdvanceModel.ApprovedHR = Global.EmployeeID;
                EmployeeSalaryAdvanceBO.Instance.Update(salaryAdvanceModel);
                grvData.SetFocusedRowCellValue(colIsApproved_HR, true);
                grvData.FocusedRowHandle = -1;
            }
        }

        private void btnIsApprovedKT_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullname));
            EmployeeSalaryAdvanceModel salaryAdvanceModel = new EmployeeSalaryAdvanceModel();
            if (id > 0)
            {
                salaryAdvanceModel = (EmployeeSalaryAdvanceModel)EmployeeSalaryAdvanceBO.Instance.FindByPK(id);
            }
            if (salaryAdvanceModel.IsPayed)
            {
                MessageBox.Show(string.Format("Nhân viên [{0}-{1}] đã được thanh toán! ", code, name), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (salaryAdvanceModel.IsApproved_KT == true)
            {
                MessageBox.Show(string.Format("Nhân viên [{0}-{1}] đã được Phòng Kế Toán duyệt! ", code, name), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!salaryAdvanceModel.IsApproved_HR)
            {
                MessageBox.Show(string.Format("Phòng HR chưa duyệt.\nVui lòng liên hệ Phòng HR! "), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show(string.Format("Bạn có muốn duyệt nhân viên [{0}-{1}] không?", code, name), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                salaryAdvanceModel.IsApproved_KT = true;
                salaryAdvanceModel.ApprovedKT = Global.EmployeeID;
                EmployeeSalaryAdvanceBO.Instance.Update(salaryAdvanceModel);
                grvData.SetFocusedRowCellValue(colIsApproved_KT, true);
                grvData.FocusedRowHandle = -1;

            }
        }

        private void btnCancelApprovedKT_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullname));
            EmployeeSalaryAdvanceModel salaryAdvanceModel = new EmployeeSalaryAdvanceModel();
            if (id > 0)
            {
                salaryAdvanceModel = (EmployeeSalaryAdvanceModel)EmployeeSalaryAdvanceBO.Instance.FindByPK(id);
            }
            if (!salaryAdvanceModel.IsApproved_KT)
            {
                MessageBox.Show(string.Format("Nhân viên [{0}-{1}] chưa được Phòng Kế Toán duyệt! ", code, name), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (salaryAdvanceModel.IsPayed)
            {
                MessageBox.Show(string.Format("Nhân viên [{0}-{1}] đã được thanh toán.\nBạn không thể hủy duyệt!", code, name), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show(string.Format("Bạn có muốn huỷ duyệt nhân viên [{0}-{1}] không?", code, name), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                salaryAdvanceModel.IsApproved_KT = false;
                EmployeeSalaryAdvanceBO.Instance.Update(salaryAdvanceModel);
                grvData.SetFocusedRowCellValue(colIsApproved_KT, false);
                grvData.FocusedRowHandle = -1;

            }
        }

        private void btnCancelApprovedHR_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullname));
            EmployeeSalaryAdvanceModel salaryAdvanceModel = new EmployeeSalaryAdvanceModel();
            if (id > 0)
            {
                salaryAdvanceModel = (EmployeeSalaryAdvanceModel)EmployeeSalaryAdvanceBO.Instance.FindByPK(id);
            }
            if (!salaryAdvanceModel.IsApproved_HR)
            {
                MessageBox.Show(string.Format("Nhân viên [{0}-{1}] chưa được Phòng HR duyệt! ", code, name), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (salaryAdvanceModel.IsPayed)
            {
                MessageBox.Show(string.Format("Nhân viên [{0}-{1}] đã được thanh toán.\nBạn không thể hủy duyệt!", code, name), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show(string.Format("Bạn có muốn huỷ duyệt nhân viên [{0}-{1}] không?", code, name), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                salaryAdvanceModel.IsApproved_HR = false;
                salaryAdvanceModel.ApprovedHR = 0;
                EmployeeSalaryAdvanceBO.Instance.Update(salaryAdvanceModel);
                grvData.SetFocusedRowCellValue(colIsApproved_HR, false);
                grvData.FocusedRowHandle = -1;
            }
        }

        private void btnCancelApprovedTP_Click(object sender, EventArgs e)
        {
            bool isApproved = false;
            string approved = isApproved == true ? "duyệt" : "hủy duyệt";
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullname));
            int DepartmentID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colDepartmentID));
            int employeeId = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
            EmployeeSalaryAdvanceModel salaryAdvanceModel = new EmployeeSalaryAdvanceModel();
            if (id > 0)
            {
                salaryAdvanceModel = (EmployeeSalaryAdvanceModel)EmployeeSalaryAdvanceBO.Instance.FindByPK(id);
            }
            if (!salaryAdvanceModel.IsApproved_TP)
            {
                MessageBox.Show(string.Format("Nhân viên [{0}-{1}] chưa được Trưởng bộ phận duyệt!", code, name), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (salaryAdvanceModel.IsPayed)
            {
                MessageBox.Show(string.Format("Nhân viên [{0}-{1}] đã được thanh toán.\nBạn không thể hủy duyệt!", code, name), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (DepartmentID != Global.DepartmentID && Global.DepartmentID != 1)
            {
                MessageBox.Show($"Nhân viên [{name}] không thuộc phòng [{Global.DepartmentName.ToUpper()}].\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (employeeId == Global.EmployeeID)
            {
                MessageBox.Show($"Bạn không được {approved} cho chính mình.\nVui lòng liên hệ cấp cao hơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show(string.Format("Bạn có muốn huỷ duyệt nhân viên [{0}-{1}] không?", code, name), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                salaryAdvanceModel.IsApproved_TP = false;
                EmployeeSalaryAdvanceBO.Instance.Update(salaryAdvanceModel);
                grvData.SetFocusedRowCellValue(colIsApproved_TP, false);
                grvData.FocusedRowHandle = -1;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmSalaryAdvanceDetail frm = new frmSalaryAdvanceDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            bool isApprovedTP = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApproved_TP));
            bool isApprovedHR = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApproved_HR));
            bool isApprovedKT = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApproved_KT));

            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullname));
            if (isApprovedTP || isApprovedHR || isApprovedKT)
            {
                MessageBox.Show(string.Format("Nhân viên [{0}-{1}] đã được duyệt.\nVui lòng hủy duyệt trước khi sửa!", code, name), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var focusedRowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            EmployeeSalaryAdvanceModel model = (EmployeeSalaryAdvanceModel)EmployeeSalaryAdvanceBO.Instance.FindByPK(ID);
            frmSalaryAdvanceDetail frm = new frmSalaryAdvanceDetail();
            frm.employeeSalary = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
                grvData.FocusedRowHandle = focusedRowHandle;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool isApprovedTP = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApproved_TP));
            bool isApprovedHR = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApproved_HR));
            bool isApprovedKT = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApproved_KT));
            bool isPayed = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsPayed));

            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullname));
            if (isApprovedTP || isApprovedHR || isApprovedKT)
            {
                MessageBox.Show(string.Format("Nhân viên [{0}-{1}] đã được duyệt.\nVui lòng hủy duyệt trước khi xóa!", code, name), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (isPayed)
            {
                MessageBox.Show(string.Format("Nhân viên [{0}-{1}] đã được thanh toán.\nBạn không th xóa!", code, name), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            if (MessageBox.Show(string.Format("Bạn có muốn xóa nhân viên [{0}-{1}] không? ", code, name), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                EmployeeSalaryAdvanceBO.Instance.Delete(ID);
                grvData.DeleteSelectedRows();
            }
        }

        private void grvData_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == colMoney)
            {
                if (TextUtils.ToDecimal(e.Value) > 0)
                {
                    e.DisplayText = Decimal.Round(TextUtils.ToDecimal(e.Value), 0).ToString() + " đ";
                }
                else
                {
                    e.DisplayText = Decimal.Round(TextUtils.ToDecimal(e.Value), 0).ToString();
                }
            }
        }
    }
}
