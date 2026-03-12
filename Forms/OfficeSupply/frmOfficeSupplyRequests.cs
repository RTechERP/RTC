using BMS.Model;
using DevExpress.XtraGrid.Views.Grid;
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
using BMS.Utils;

namespace BMS
{
    public partial class frmOfficeSupplyRequests : _Forms
    {
        public frmOfficeSupplyRequests(bool isApprovedTBP)
        {
            InitializeComponent();
            if (isApprovedTBP)
            {
                foreach (ToolStripItem item in mnuMenu.Items)
                {
                    //if (item == btnImportExcelOld) continue;
                    item.Visible = !isApprovedTBP;
                }
                btnApprove.Visible = isApprovedTBP;
                btnDisapprove.Visible = isApprovedTBP;
                toolStripSeparator4.Visible = isApprovedTBP;
            }
        }

        private void frmOfficeSupplyRequests_Load(object sender, EventArgs e)
        {
            //dtpMonthPicker.Value = DateTime.Today;
            txtYear.Value = DateTime.Now.Year;
            txtMonth.Value = DateTime.Now.Month;

            LoadDepartment();
            LoadData();
        }
        #region LOAD DATA

        //List<int> userAlls = new List<int>() { 354, 156, 331 };
        void LoadDepartment()
        {
            Global.departmentIDs.Add(Global.DepartmentID);
            //int[] departmentIDs = new int[] { 9, 10, Global.DepartmentID };
            List<DepartmentModel> departmentList = SQLHelper<DepartmentModel>.FindAll().OrderBy(x => x.STT).ToList();
            if (Global.EmployeeID == 331 || Global.EmployeeID == 548) //nếu là hương và Hà admin
            {
                departmentList = departmentList.Where(x => Global.departmentIDs.Contains(x.ID)).ToList();
            }

            cboDepartment.Properties.DataSource = departmentList;
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.ValueMember = "ID";

            if (Global.EmployeeID == 54) Global.DepartmentID = 2;
            cboDepartment.EditValue = Global.DepartmentID;

            if (Global.EmployeeID != 331 && Global.EmployeeID != 548)
            {
                if (!Global.userAllsOfficeSupply.Contains(Global.EmployeeID) && !Global.IsAdmin)
                {
                    cboDepartment.EditValue = Global.DepartmentID;
                    cboDepartment.Enabled = false;
                }
            }
        }
        private void LoadData()
        {
            //DateTime MonthInput = dtpMonthPicker.Value;
            DateTime MonthInput = new DateTime((int)txtYear.Value, (int)txtMonth.Value, 1);
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);

            DataTable dt = TextUtils.GetDataTableFromSP("spGetOfficeSupplyRequests",
                new string[] { "@MonthInput", "@KeyWord", "@EmployeeID", "@DepartmentID" },
                new object[] { MonthInput, txtFilterText.Text.Trim(), 0, departmentID });

            grdData.DataSource = dt;


            LoadDetail();
        }

        void LoadDetail()
        {
            int officeSupplyRequestsID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIDRequest));
            DataTable dt = TextUtils.GetDataTableFromSP("spGetOfficeSupplyRequestsDetail",
                new string[] { "@OfficeSupplyRequestsID" },
                new object[] { officeSupplyRequestsID });
            grdDataDetail.DataSource = dt;
        }
        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
        }
        #endregion

        #region ADD, EDIT, DELETE, COPY, APPROVE
        private void btnNew_Click(object sender, EventArgs e)
        {
            DateTime dateExpired = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 5, 23, 59, 59);
            if (DateTime.Now > dateExpired && !Global.IsAdmin && Global.EmployeeID != 395)//nv Nhi
            {
                MessageBox.Show("Bạn không thể đăng ký VPP sau ngày mùng 5!", "Thông báo");
                return;
            }


            frmOfficeSupplyRequestsDetail frm = new frmOfficeSupplyRequestsDetail();
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK || result == DialogResult.Cancel)
            {
                LoadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focus = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIDRequest));
            if (ID == 0) return;
            OfficeSupplyRequestsModel model = SQLHelper<OfficeSupplyRequestsModel>.FindByID(ID);
            if (model == null) return;
            frmOfficeSupplyRequestsDetail frm = new frmOfficeSupplyRequestsDetail();
            frm.OSRequestID = TextUtils.ToInt(model.ID);
            frm.OSRequestModel = model;
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK || result == DialogResult.Cancel)
            {
                LoadData();
                grvData_FocusedRowChanged(null, null);
                grvData.FocusedRowHandle = focus;
            }
        }
        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DateTime dateExpired = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 5, 23, 59, 59);
            if (DateTime.Now > dateExpired && !Global.IsAdmin)
            {
                MessageBox.Show("Bạn không thể xoá đăng ký VPP sau ngày mùng 5!", "Thông báo");
                return;
            }

            var selectedRows = grvData.GetSelectedRows();
            if (selectedRows.Length <= 0)
            {
                MessageBox.Show($"Vui lòng chọn người đăng ký VPP muốn xóa!", "Thông báo");
                return;
            }

            if (MessageBox.Show($"Bạn có muốn xóa các VPP đã chọn hay không?\nNhững VPP đã duyệt thì không thể xóa!", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                List<int> listIDs = new List<int>();
                foreach (int row in selectedRows)
                {
                    int ID = TextUtils.ToInt(grvData.GetRowCellValue(row, colIDRequest));
                    if (ID == 0) continue;

                    bool approved = TextUtils.ToBoolean(grvData.GetRowCellValue(row, colIsApproved));
                    bool isAdminApproved = TextUtils.ToBoolean(grvData.GetRowCellValue(row, colIsAdminApproved));
                    if (approved || isAdminApproved) continue;

                    listIDs.Add(ID);
                    //SQLHelper<OfficeSupplyRequestsModel>.DeleteModelByID(ID);
                    //grvData.DeleteRow(row);
                    //SQLHelper<OfficeSupplyRequestsDetailModel>.DeleteByAttribute("OfficeSupplyRequestsID", ID);
                }

                if (listIDs.Count <= 0) return;

                var myDict = new Dictionary<string, object>()
                {
                    { OfficeSupplyRequestsModel_Enum.IsDeleted.ToString(),true},
                    { OfficeSupplyRequestsModel_Enum.UpdatedDate.ToString(),DateTime.Now},
                    { OfficeSupplyRequestsModel_Enum.UpdatedBy.ToString(),Global.AppUserName},
                };

                var exp = new Expression(OfficeSupplyRequestsModel_Enum.ID, string.Join(",", listIDs), "IN");
                SQLHelper<OfficeSupplyRequestsModel>.UpdateFields(myDict, exp);

                LoadData();
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colIDRequest));
            if (ID == 0) return;
            frmOfficeSupplyRequestsDetail frm = new frmOfficeSupplyRequestsDetail();
            frm.OSRequestID = ID;
            frm.isCopy = true;
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK || result == DialogResult.Cancel)
            {
                LoadData();
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            isApprove(true);
        }

        private void btnDisapprove_Click(object sender, EventArgs e)
        {
            isApprove(false);
        }

        void isApprove(bool approve)
        {
            string txtApprove = approve ? "duyệt" : "hủy duyệt";

            var selectedRows = grvData.GetSelectedRows();
            if (selectedRows.Length <= 0)
            {
                MessageBox.Show($"Vui lòng chọn người đăng ký VPP muốn {txtApprove}!", "Thông báo");
                return;
            }



            if (MessageBox.Show($"Bạn có muốn {txtApprove} các VPP đã chọn hay không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                foreach (int row in selectedRows)
                {
                    int ID = TextUtils.ToInt(grvData.GetRowCellValue(row, colIDRequest));
                    if (ID == 0) continue;

                    OfficeSupplyRequestsModel model = SQLHelper<OfficeSupplyRequestsModel>.FindByID(ID);
                    if (model == null) continue;

                    if (approve && model.IsAdminApproved == false)continue;
                    if (model.IsApproved == approve)continue;

                    model.DateApproved = DateTime.Now;
                    model.IsApproved = approve;
                    model.ApprovedID = Global.EmployeeID;

                    SQLHelper<OfficeSupplyRequestsModel>.Update(model);
                    grvData.SetRowCellValue(row, colIsApproved, approve);
                }

                LoadData();
            }
        }

        void AdminApproved(bool isAdminApproved)
        {
            string txtApprove = isAdminApproved ? "duyệt" : "hủy duyệt";

            var selectedRows = grvData.GetSelectedRows();
            if (selectedRows.Length <= 0)
            {
                MessageBox.Show($"Vui lòng chọn người đăng ký VPP muốn {txtApprove}!", "Thông báo");
                return;
            }



            if (MessageBox.Show($"Bạn có muốn {txtApprove} các VPP đã chọn hay không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                List<int> listIDs = new List<int>();
                foreach (int row in selectedRows)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(row, colIDRequest));
                    if (id == 0) continue;

                    bool isApproved = TextUtils.ToBoolean(grvData.GetRowCellValue(row, colIsApproved));
                    if (!isAdminApproved && isApproved) continue;
                    listIDs.Add(id);
                }

                if (listIDs.Count <= 0) return;

                var myDict = new Dictionary<string, object>()
                {
                    { OfficeSupplyRequestsModel_Enum.IsAdminApproved.ToString(),isAdminApproved},
                    { OfficeSupplyRequestsModel_Enum.DateAdminApproved.ToString(),DateTime.Now},
                    { OfficeSupplyRequestsModel_Enum.AdminApprovedID.ToString(),Global.EmployeeID},
                };

                var exp = new Expression(OfficeSupplyRequestsModel_Enum.ID, string.Join(",", listIDs), "IN");
                SQLHelper<OfficeSupplyRequestsModel>.UpdateFields(myDict,exp);
                //model.DateAdminApproved = DateTime.Now;
                //model.IsAdminApproved = isApproved;
                //model.AdminApprovedID = Global.EmployeeID;

                //SQLHelper<OfficeSupplyRequestsModel>.Update(model);
                LoadData();
            }
        }
        #endregion

        private void dtpMonthPicker_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtFilterText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) LoadData();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtYear_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtMonth_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnAdminApproved_Click(object sender, EventArgs e)
        {
            AdminApproved(true);
        }

        private void btnUnAdminApproved_Click(object sender, EventArgs e)
        {
            AdminApproved(false);
        }
    }
}