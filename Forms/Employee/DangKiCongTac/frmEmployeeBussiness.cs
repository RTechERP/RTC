
using BMS.Business;
using BMS.Model;
using DevExpress.XtraPrinting;
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
    public partial class frmEmployeeBussiness : _Forms
    {
        int StatusApprove, IDApproved;
        string[] arrParamName = null;
        object[] arrParamValue = null;
        public frmEmployeeBussiness()
        {
            InitializeComponent();
        }

        private void frmEmployeeBussiness_Load(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Now.AddDays(-1);
            txtPageNumber.Text = "1";
            LoadDepartment();
            IDApproved = Global.EmployeeID;
            cbApprovedStatusTBP.SelectedIndex = 1; //Chưa duyệt

            if (Global.DepartmentID == 1)
            {
                IDApproved = 0;
                cbDepartment.EditValue = 0;
            }
            else if (Global.DepartmentID == 6)
            {
                IDApproved = 0;
                cbDepartment.EditValue = 0;

                colCostBussiness.Visible = true;
                colCostOvernight.Visible = true;
                colCostVehicle.Visible = true;
                colCostWorkEarly.Visible = true;

                colCostBussiness.VisibleIndex = 10;
                colCostOvernight.VisibleIndex = 11;
                colCostVehicle.VisibleIndex = 12;
                colCostWorkEarly.VisibleIndex = 13;
            }
            else
                cbDepartment.EditValue = Global.DepartmentID;
            loadData();
            grdDataFile.ContextMenuStrip = contextMenuStrip2;
        }
        private void loadData()
        {
            //if (cbApprovedStatusTBP.SelectedIndex == 1)
            //    StatusApprove = 1;
            //else if (cbApprovedStatusTBP.SelectedIndex == 2)
            //    StatusApprove = 0;
            StatusApprove = cbApprovedStatusTBP.SelectedIndex - 1;
            int departmentId = TextUtils.ToInt(cbDepartment.EditValue);
            DateTime dateTimeS = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, dtpStartDate.Value.Day, 00, 00, 00).AddSeconds(-1);
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59).AddSeconds(+1);
            //Khánh bỏ vì thừa
            //if (cbApprovedStatusTBP.SelectedIndex == 0)
            //{
            //    arrParamName = new string[] { "@PageNumber", "@PageSize", "@StartDate", "@EndDate", "@Keyword", "@DepartmentID", "@IDApprovedTP" };
            //    arrParamValue = new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), dateTimeS, dateTimeE, txtSearch.Text.Trim(), departmentId, IDApproved };
            //}
            //else
            //{
            //    arrParamName = new string[] { "@PageNumber", "@PageSize", "@StartDate", "@EndDate", "@Keyword", "@DepartmentID", "@IDApprovedTP", "@Status" };
            //    arrParamValue = new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), dateTimeS, dateTimeE, txtSearch.Text.Trim(), departmentId, IDApproved, StatusApprove };
            //}
            arrParamName = new string[] { "@PageNumber", "@PageSize", "@StartDate", "@EndDate", "@Keyword", "@DepartmentID", "@IDApprovedTP", "@Status" };
            arrParamValue = new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), dateTimeS, dateTimeE, txtSearch.Text.Trim(), departmentId, IDApproved, StatusApprove };
            DataSet ds = TextUtils.LoadDataSetFromSP("spGetEmployeeBussiness", arrParamName, arrParamValue);
            grdData.DataSource = ds.Tables[0];
            if (ds.Tables.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(ds.Tables[1].Rows[0]["TotalPage"]);
        }

        void LoadFile()
        {
            grdDataFile.DataSource = null;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id <= 0) return;
            List<EmployeeBussinessFileModel> lstFile = SQLHelper<EmployeeBussinessFileModel>.FindByAttribute(EmployeeBussinessFileModel_Enum.EmployeeBussinessID.ToString(), id);
            grdDataFile.DataSource = lstFile;
        }

        void LoadDepartment()
        {
            DataTable dtDepartment = TextUtils.Select($"SELECT ID, Code, Name FROM Department");
            cbDepartment.Properties.DataSource = dtDepartment;
            cbDepartment.Properties.DisplayMember = "Name";
            cbDepartment.Properties.ValueMember = "ID";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //EmployeeBussinessModel model = new EmployeeBussinessModel();
            frmEmployeeBussinessDetail frm = new frmEmployeeBussinessDetail();
            //frm.employee = model;
            frm.dtBussiness = TextUtils.LoadDataFromSP("spGetEmployeeBussinessDetail", "A",
                                                        new string[] { "@EmployeeID", "@DayBussiness" },
                                                        new object[] { 0, DateTime.Now });
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            bool isApproved = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApprovedTBP));
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
            //if (isApproved == true)
            //{
            //    MessageBox.Show(String.Format("Nhân viên [{0}-{1}] đã được duyệt.\nVui lòng huỷ duyệt trước khi sửa!", code, name), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Question);
            //    return;
            //}

            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            int employee = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colEmployeeID));
            DateTime dayBussiness = TextUtils.ToDate5(grvData.GetFocusedRowCellValue(colDayBussiness));
            EmployeeBussinessModel model = (EmployeeBussinessModel)EmployeeBussinessBO.Instance.FindByPK(ID);
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeBussinessDetail", "A",
                                                        new string[] { "@EmployeeID", "@DayBussiness" },
                                                        new object[] { employee, dayBussiness });

            frmEmployeeBussinessDetail frm = new frmEmployeeBussinessDetail();
            //frm.employee = model;
            frm.dtBussiness = dt;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
                grvData.FocusedRowHandle = focusedRowHandle;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool isApproved = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApprovedTBP));
            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
            if (isApproved == true)
            {
                MessageBox.Show(String.Format("Nhân viên [{0}-{1}] đã được duyệt.\nVui lòng huỷ duyệt trước khi xóa!", code, name), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }

            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            if (MessageBox.Show(string.Format("Bạn có muốn xóa nhân viên [{0}-{1}] không? ", code, name), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int[] rowIndex = grvData.GetSelectedRows();
                for (int i = 0; i < rowIndex.Length; i++)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colID));
                    EmployeeBussinessBO.Instance.Delete(id);
                }
                grvData.DeleteSelectedRows();
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

        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            sfd.FileName = $"DanhSachCongTac_T{dtpStartDate.Value.Month}_{dtpStartDate.Value.Year}";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                XlsExportOptionsEx optionsEx = new XlsExportOptionsEx();
                grvData.OptionsPrint.AutoWidth = false;
                grvData.OptionsPrint.ExpandAllDetails = false;
                grvData.OptionsPrint.PrintDetails = true;
                grvData.OptionsPrint.UsePrintStyles = true;
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                try
                {
                    grvData.ExportToXls(sfd.FileName, optionsEx);
                    Process.Start(sfd.FileName);
                }
                catch (Exception)
                {
                }
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
                bool isApprovedTP = TextUtils.ToBoolean(grvData.GetRowCellValue(rowIndex[i], colIsApprovedTBP));
                int employeeId = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colEmployeeID));
                if (employeeId == 0) continue;
                string employeeName = TextUtils.ToString(grvData.GetRowCellValue(rowIndex[i], colFullName));

                if (!isApprovedTP)
                {
                    MessageBox.Show($"Bạn không thể {approved} vì nhân viên [{employeeName}] chưa được TBP duyệt.\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        if (TextUtils.ToBoolean(grvData.GetRowCellValue(rowIndex[i], colIsApprovedTBP)))
                        {
                            listID.Add(TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colID)));
                            int type = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colTypeName));
                            //CalculateCoefficientOff(IsApproved, rowIndex[i]);
                            //CalculateDayOff(IsApproved, rowIndex[i]);
                            int ID = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colID));
                            //EmployeeBussinessModel model = (EmployeeBussinessModel)EmployeeBussinessBO.Instance.FindByPK(ID);
                            EmployeeBussinessModel model = SQLHelper<EmployeeBussinessModel>.FindByID(ID);
                            if (model != null)
                            {
                                model.ApprovedHR = Global.EmployeeID;
                                //EmployeeBussinessBO.Instance.Update(model);
                                SQLHelper<EmployeeBussinessModel>.Update(model);
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
                            int type = TextUtils.ToInt(grvData.GetRowCellValue(rowIndex[i], colTypeName));
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

                TextUtils.ExcuteProcedure("spUpdateTableByFieldNameAndID",
                    new string[] { "@TableName", "@FieldName", "@Value", "@ID", "@ValueUpdatedBy", "@ValueUpdatedDate" },
                    new object[] { "EmployeeBussiness", fieldName, IsApproved ? 1 : 0, string.Join(",", listID), Global.LoginName, DateTime.Now });
            }
            loadData();
        }
        private void btnIsApproved_Click(object sender, EventArgs e)
        {
            //bool isApproved = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApproved));
            //string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            //string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
            //if (isApproved == true)
            //{
            //    MessageBox.Show(String.Format("Nhân viên [{0}-{1}] đã được duyệt.\nVui lòng thử lại sau! ", code,name), TextUtils.Caption, MessageBoxButtons.OK,MessageBoxIcon.Question);
            //    return;
            //}
            approved(true, "IsApprovedTBP", true);
        }

        private void btnCancelApproved_Click(object sender, EventArgs e)
        {
            //bool isApproved = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsApproved));
            //string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            //string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName));
            //if (isApproved == false)
            //{
            //    MessageBox.Show(String.Format("Nhân viên [{0}-{1}] chưa được duyệt.\nVui lòng thử lại sau! ", code, name), TextUtils.Caption, MessageBoxButtons.OK,MessageBoxIcon.Question);
            //    return;
            //}
            approved(false, "IsApprovedTBP", true);
        }
        private void btnChiTiet_Click_1(object sender, EventArgs e)
        {
            frmWorkManagement frm = new frmWorkManagement();
            frm.Show();

            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    loadData();
            //}
        }

        private void cbApprovedStatusTBP_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void cbDepartment_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnIsApprovedHR_Click(object sender, EventArgs e)
        {
            approved(true, "IsApprovedHR", false);
        }

        private void btnCancelApprovedHR_Click(object sender, EventArgs e)
        {
            approved(false, "IsApprovedHR", false);
        }

        //Xem chi tiết ảnh hóa đơn phương tiện
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            btnXemchitietanh_Click(null, null);
        }
        private void btnXemchitietanh_Click(object sender, EventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID <= 0)
            {
                return;
            }

            var bussinesInfo = new
            {
                ID = ID,
                FullName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFullName)),
                TypeBussiness = TextUtils.ToString(grvData.GetFocusedRowCellValue(colTypeName)),
                Location = TextUtils.ToString(grvData.GetFocusedRowCellValue(colLocation)),
                DayBussiness = TextUtils.ToDate5(grvData.GetFocusedRowCellValue(colDayBussiness))
            };
            //List<object> listBussiness = new List<object>()
            //{
            //    new {
            //    }
            //}.ToList();

            frmEmployeeBussinessVehicle frm = new frmEmployeeBussinessVehicle();
            //frm.dataVihicle = dt;
            //frm.bussinessID = ID;
            frm.bussiness = bussinesInfo;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
                grvData.FocusedRowHandle = rowHandle;
            }
        }

        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadFile();
        }

        private void btnViewAttachFile_Click(object sender, EventArgs e)
        {
            try
            {
                string path = TextUtils.ToString(grvDataFile.GetFocusedRowCellValue(colServerPath));
                string fileName = TextUtils.ToString(grvDataFile.GetFocusedRowCellValue(colFileName));

                Process.Start(Path.Combine(path, fileName));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPhuPhi_Click(object sender, EventArgs e)
        {
            EmployeeTypeBussinessModel model = (EmployeeTypeBussinessModel)EmployeeTypeBussinessBO.Instance.FindByPK(1);
            frmEmployeeSetting frm = new frmEmployeeSetting();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }
    }
}
