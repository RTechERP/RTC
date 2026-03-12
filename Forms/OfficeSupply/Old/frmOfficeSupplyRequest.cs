using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMS.Model;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using System.Diagnostics;
using DevExpress.XtraEditors.Repository;
using DevExpress.Spreadsheet;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using ClosedXML.Excel;
using DevExpress.XtraGrid.Columns;

namespace BMS
{
    public partial class frmOfficeSupplyRequest : _Forms
    {
        //bool isAdmin = Global.IsAdmin || Global.IsAdminSale;
        public frmOfficeSupplyRequest(bool isApprovedTBP)
        {
            InitializeComponent();

            if (isApprovedTBP)
            {
                foreach (ToolStripItem item in mnuMenu.Items)
                {
                    if (item == btnImportExcelOld) continue;
                    item.Visible = !isApprovedTBP;
                }
                btnApprove.Visible = isApprovedTBP;
                btnDisapprove.Visible = isApprovedTBP;
                toolStripSeparator4.Visible = isApprovedTBP;
            }
            
        }

        private void OfficeSupplyRequest_Load(object sender, EventArgs e)
        {
            dtpMonthPicker.Value = DateTime.Today;
            LoadDepartment();
            LoadData();
        }

        void LoadDepartment()
        {
            List<DepartmentModel> departmentList = SQLHelper<DepartmentModel>.FindAll();
            cboDepartment.Properties.DataSource = departmentList;
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.EditValue = Global.DepartmentID;
        }
        private void LoadData()
        {

            //DateTime MonthInput = dtpMonthPicker.EditValue == null ? DateTime.Now : dtpMonthPicker.DateTime;
            DateTime MonthInput = dtpMonthPicker.Value;//== null ? DateTime.Now : dtpMonthPicker.DateTime;
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);

            DataTable dt = TextUtils.GetDataTableFromSP("spGetOfficeSupplyRequest",
                new string[] { "@MonthInput", "@KeyWord", "@UserID", "@DepartmentID" },
                new object[] { MonthInput, txtFilterText.Text.Trim(), 0, departmentID });
            grdData.DataSource = dt;
        }


        void Approved(bool isApproved)
        {
            string isApprovedText = isApproved ? "duyệt" : "huỷ duyệt";
            int isApprovedValue = isApproved ? 1 : 0;
            int[] rowSelecteds = grvData.GetSelectedRows();
            if (rowSelecteds.Length <= 0)
            {
                MessageBox.Show($"Vui lòng chọn VPP muốn {isApprovedText}!", "Thông báo");
            }

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn {isApprovedText} danh sách VPP đã chọn không?\n" +
                                                    $"Những VPP của phòng ban khác đăng ký sẽ được bỏ qua", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                List<int> listIDs = new List<int>();
                Global.DepartmentID = Global.EmployeeID == 54 ? 2 : Global.DepartmentID;
                foreach (int row in rowSelecteds)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(row, "ID"));
                    int departmentId = TextUtils.ToInt(grvData.GetRowCellValue(row, colDepartmentID));
                    if (id <= 0) continue;
                    if (Global.DepartmentID != departmentId && !Global.IsAdmin) continue;
                    listIDs.Add(id);
                }

                if (listIDs.Count <= 0) return;
                string idText = string.Join(",", listIDs);
                string sql = $"UPDATE OfficeSupplyRequest SET IsApproved = {isApprovedValue}," +
                                $"ApprovedID = {Global.EmployeeID}," +
                                $"DateApproved = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                                $"UpdatedDate='{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                                $"UpdatedBy='{Global.LoginName}'" +
                                $"WHERE ID IN ({idText})";

                TextUtils.ExcuteSQL(sql);
                LoadData();
            }
        }

        #region Button Click/ Value Change
        private void dtpMonthPicker_EditValueChanged(object sender, EventArgs e)
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
        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            OfficeSupplyImportExcel frm = new OfficeSupplyImportExcel();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            //DateTime MonthInput = dtpMonthPicker.EditValue == null ? DateTime.Now : dtpMonthPicker.DateTime;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xlsx)|*.xlsx";
            sfd.FileName = $"OfficeSupplyRequest_{dtpMonthPicker.Value.ToString("ddMMyy")}";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    grvData.ExportToXlsx(sfd.FileName);
                    Process.Start(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grdData.DataSource == null) return;
            var rows = GetSelectedRows();
            if (rows.Count == 0) return;
            foreach (DataRowView row in rows)
            {
                int userIDRecive = TextUtils.ToInt(row["UserIDReceive"]);
                if (userIDRecive > 0)
                {
                    MessageBox.Show("Không thể xóa đăng ký đã duyệt nhận", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (DataRowView row in rows)
                {
                    string strID = TextUtils.ToString(row["ID"]);
                    SQLHelper<OfficeSupplyRequestModel>.DeleteByAttribute("ID", strID);
                }
                LoadData();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmOfficeSupplyRequestDetail frm = new frmOfficeSupplyRequestDetail();
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK || result == DialogResult.Cancel)
            {
                LoadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            bool isReceived = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colReceived));
            if (isReceived)
            {
                MessageBox.Show("Không thể sửa đăng ký đã duyệt nhận", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            long id = TextUtils.ToInt64(grvData.GetFocusedRowCellValue(colID));
            var model = SQLHelper<OfficeSupplyRequestModel>.FindByID(id);
            frmOfficeSupplyRequestDetail frm = new frmOfficeSupplyRequestDetail();
            frm.model = model;
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK || result == DialogResult.Cancel)
            {
                LoadData();
            }
        }
        private void btnPassQuantity_Click(object sender, EventArgs e)
        {
            if (grdData.DataSource == null) return;
            var rows = GetSelectedRows();
            if (rows.Count == 0) return;
            foreach (DataRowView row in rows)
            {
                int userIDRecive = TextUtils.ToInt(row["UserIDReceive"]);
                if (userIDRecive > 0)
                {
                    MessageBox.Show("Không thể sửa đăng ký đã duyệt nhận", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            if (MessageBox.Show("Copy số lượng đề xuất sang số lượng thực nhận?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (DataRowView row in rows)
                {
                    long id = TextUtils.ToInt64(row["ID"]);
                    var model = SQLHelper<OfficeSupplyRequestModel>.FindByID(id);
                    model.QuantityReceived = model.Quantity;
                    SQLHelper<OfficeSupplyRequestModel>.Update(model);
                }
                LoadData();
            }
        }
        private void btnApprove_Click(object sender, EventArgs e)
        {
            Approved(true);
            //if (grdData.DataSource == null) return;
            //var rows = GetSelectedRows();
            //if (rows.Count == 0) return;
            //if (MessageBox.Show("Xác nhận duyệt?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    foreach (DataRowView row in rows)
            //    {
            //        long id = TextUtils.ToInt64(row["ID"]);
            //        var model = SQLHelper<OfficeSupplyRequestModel>.FindByID(id);
            //        model.UserIDReceive = model.UserID;
            //        SQLHelper<OfficeSupplyRequestModel>.Update(model);
            //    }
            //    LoadData();
            //}
        }
        private void btnDisapprove_Click(object sender, EventArgs e)
        {
            Approved(false);

            //if (grdData.DataSource == null) return;
            //var rows = GetSelectedRows();
            //if (rows.Count == 0) return;
            //if (MessageBox.Show("Xác nhận hủy duyệt?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    foreach (DataRowView row in rows)
            //    {
            //        long id = TextUtils.ToInt64(row["ID"]);
            //        var model = SQLHelper<OfficeSupplyRequestModel>.FindByID(id);
            //        model.UserIDReceive = 0;
            //        SQLHelper<OfficeSupplyRequestModel>.Update(model);
            //    }
            //    LoadData();
            //}
        }
        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(e.Location);

            if (hitInfo.Column != null && hitInfo.Column.Name == "colReceived" && hitInfo.InRowCell)
            {
                view.CloseEditor();
                bool currentValue = (bool)view.GetRowCellValue(hitInfo.RowHandle, hitInfo.Column);
                view.SetRowCellValue(hitInfo.RowHandle, hitInfo.Column, currentValue == !currentValue);
                view.UpdateCurrentRow();
            }
        }
        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;

            if (e.Column.Name == "colReceived")
            {
                long id = TextUtils.ToInt(view.GetRowCellValue(e.RowHandle, "ID"));
                var model = SQLHelper<OfficeSupplyRequestModel>.FindByID(id);
                if (MessageBox.Show($"Xác nhận {(model.UserIDReceive == 0 || model.UserIDReceive == null ? "" : "hủy")}duyệt?",
                    TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    model.UserIDReceive = model.UserIDReceive > 0 ? 0 : model.UserID;
                    SQLHelper<OfficeSupplyRequestModel>.Update(model);
                    LoadData();
                }
            }
        }
        #endregion
        #region Style
        private void grvData_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            bool isExceeded = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "ExceedsLimit"));
            if (isExceeded)
            {
                e.Appearance.BackColor = Color.Orange;
            }
            e.HighPriority = true;
        }
        #endregion
        private List<DataRowView> GetSelectedRows()
        {
            var selectedRows = new List<DataRowView>();

            if (grvData.FocusedRowHandle >= 0)
            {
                DataRowView focusedRow = (DataRowView)grvData.GetFocusedRow();
                selectedRows.Add(focusedRow);
            }

            int[] selectedHandles = grvData.GetSelectedRows();
            foreach (int rowHandle in selectedHandles)
            {
                if (rowHandle != grvData.FocusedRowHandle)
                {
                    DataRowView row = (DataRowView)grvData.GetRow(rowHandle);
                    selectedRows.Add(row);
                }
            }

            return selectedRows;
        }

        private void grvData_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            //if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Column)
            //{
            //    for (int i = e.Menu.Items.Count - 1; i >= 0; i--)
            //    {
            //        if (e.Menu.Items[i].Caption == "Ẩn cột này" || e.Menu.Items[i].Caption == "Điều chỉnh ẩn hiện cột")
            //        {
            //            e.Menu.Items.RemoveAt(i);
            //        }
            //    }
            //}
        }
    }
}