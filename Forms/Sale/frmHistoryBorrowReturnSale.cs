using BMS.Business;
using BMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Forms;
using System.Diagnostics;
using System.Collections;
using DevExpress.Data.Linq;
using Microsoft.VisualBasic.ApplicationServices;
using DevExpress.XtraGrid.Views.Grid;

namespace BMS
{
    public partial class frmHistoryBorrowReturnSale : _Forms
    {
        public ProductGroupRTCModel _RTCModel = new ProductGroupRTCModel();
        public int Status = -1;
        private int userID = -1;
        private int departmentID = -1;
        private int warehouseID = 1;
        public frmHistoryBorrowReturnSale()
        {
            InitializeComponent();
        }
        public frmHistoryBorrowReturnSale(int WarehouseID)
        {
            InitializeComponent();
            warehouseID = WarehouseID;
        }

        private void frmListTool_Load(object sender, EventArgs e)
        {
            LoadEmployee();
            LoadDepartments();

            // ngày bắt đầu khi load form bằng ngày hiện tại trừ đi 1 tháng
            dtpFromDate.Value = DateTime.Now.AddMonths(-1);
            txtPageNumber.Text = "1";
            
            LoadData();

            //btnGiaHan.Visible = labelGiaHanMuon.Visible = dtpNgayGiaHan.Visible = (Global.AppUserName.Trim().ToLower() == "admin"
            //    || Global.AppUserName.Trim().ToLower() == "thaonv");
            //this.Text += warehouseID == 1 ? " - HN" : (warehouseID == 2 ? " - HCM" : " - BN");
        }

        private void LoadDepartments()
        {
            ArrayList _lstDepartment = DepartmentBO.Instance.FindAll();
            cbDepartments.Properties.DataSource = _lstDepartment;
            cbDepartments.Properties.DisplayMember = "Name";
            cbDepartments.Properties.ValueMember = "ID";
        }

        private void LoadEmployee()
        {
            ArrayList _lstEmployee = EmployeeBO.Instance.FindAll();
            cbEmployee.Properties.DataSource = _lstEmployee;
            cbEmployee.Properties.DisplayMember = "FullName";
            cbEmployee.Properties.ValueMember = "ID";
        }

        private void LoadData()
        {
            //DateTime _dateBegin = Lib.ToDate3(dtpFromDate.Value);
            //DateTime _dateEnd = Lib.ToDate3(dtpEndDate.Value);
            
            DateTime _dateBegin = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0,0,0);
            DateTime _dateEnd = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);

            int _pageNumber = TextUtils.ToInt(txtPageNumber.Text);
            int _pageSize = TextUtils.ToInt(txtPageSize.Value);
            string _filterText = TextUtils.ToString(txtFilterText.Text);

            DataTable dt = TextUtils.LoadDataFromSP("spGetHistoryBorrowReturnSale", "A", 
                new string[] { "@PageNumber", "@PageSize", "@DateBegin", "@DateEnd", "@WarehouseID", "@UserID", "@DepartmentID", "@FilterText" }, 
                new object[] { _pageNumber, _pageSize, _dateBegin, _dateEnd, warehouseID, userID, departmentID, _filterText});
            grdData.DataSource = dt;

            if(dt.Rows.Count == 0)
            {
                txtTotalPage.Text = "0";
                return;
            }
            txtTotalPage.Text = Lib.ToString(dt.Rows[0]["TotalPage"]);
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            LoadData();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            LoadData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            LoadData();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            LoadData();
        }

        /// <summary>
        /// chọn đăng kí mượn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBorrow_Click(object sender, EventArgs e)
        {
            frmProductHistoryBorrowDetail frm = new frmProductHistoryBorrowDetail(warehouseID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();

            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            FindData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int index = comboBox1.SelectedIndex;
            //if (index == 0) Status = -1;
            //if (index == 1) Status = 1;
            //if (index == 2) Status = 0;
            //if (index == 3) Status = 2;
            //if (index == 4) Status = 3;
            //if (index == 5) Status = 4;
            //LoadData();
        }

        private void txtFilterText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FindData();
                return;
            }

            if (e.Control && e.KeyCode == Keys.C)
            {
                btnCopy_Click(null, null);
                e.Handled = true;
            }
        }

        private void FindData()
        {
            if (checkDate())
            {
                EmployeeModel _selectedEmployee = (EmployeeModel)cbEmployee.GetSelectedDataRow();
                userID = _selectedEmployee == null? -1 : TextUtils.ToInt(_selectedEmployee.UserID);

                DepartmentModel _selectedDepartment = (DepartmentModel)cbDepartments.GetSelectedDataRow();
                departmentID = _selectedDepartment == null ? -1 : _selectedDepartment.ID;

                LoadData();
            }
        }
        /// <summary>
        /// kiểm tra giá trị ngày tìm kiếm
        /// </summary>
        /// <returns></returns>
        private bool checkDate()
        {
            if (dtpFromDate.Value > dtpEndDate.Value)
            {
                MessageBox.Show("Ngày bắt đầu phải nhỏ hơn ngày kết thúc! Xin lựa chọn lại ngày tìm kiếm", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        /// <summary>
        /// đổi màu cột khi gần đến ngày hẹn trả sản phẩm hoặc quá ngày trả sản phẩm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grvData_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

        }

        private void btnLose_Click(object sender, EventArgs e)
        {
            frmProductHistoryLoseDetail frm = new frmProductHistoryLoseDetail(warehouseID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnBroken_Click(object sender, EventArgs e)
        {
            frmProductHistoryBrokenDetail frm = new frmProductHistoryBrokenDetail(warehouseID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void grdData_MouseClick(object sender, MouseEventArgs e)
        {
            if (Global.IsRoot)
            {

                if (e.Button == MouseButtons.Right)
                {

                    ContextMenu m = new ContextMenu();
                    this.ContextMenu = m;
                    m.MenuItems.Add(new MenuItem("Ghi lại lỗi quy trình nhập xuất kho cá nhân", new System.EventHandler(this.Cancel_Click)));
                    m.Show(this, new Point(e.X, e.Y));
                }
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            frmAddErrorPersonal frm = new frmAddErrorPersonal();
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colBorrowID));
            if (id == 0) return;
            frm._ProductHistoryID = id;
            frm.Show();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
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

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            //frmProductHistoryExcel frmpHistoryExcel = new frmProductHistoryExcel();
            //frmpHistoryExcel.Status = Status;
            //frmpHistoryExcel.ShowDialog();
        }

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            //if (Status == 2 || Status == 3) return;

            //DateTime nowDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            //DateTime DateReturnExpected = TextUtils.ToDate3(TextUtils.ToDate3(grvData.GetRowCellValue(e.RowHandle, colDateReturmExpected)).ToString("yyyy/MM/dd"));
            //if (grvData.GetRowCellValue(e.RowHandle, colDate2) == System.DBNull.Value)
            //{
            //    if (nowDate.CompareTo(DateReturnExpected) == 0)
            //        e.Appearance.BackColor = Color.FromArgb(255, 255, 74);
            //    if (nowDate.CompareTo(DateReturnExpected) > 0)
            //        e.Appearance.BackColor = Color.FromArgb(239, 31, 62);
            //}
            //if (Lib.ToInt(grvData.GetRowCellValue(e.RowHandle, colStatus)) == 4)
            //{
            //    e.Appearance.BackColor = Color.Lime;
            //}
        }

        private void btnListError_Click(object sender, EventArgs e)
        {
            frmHistoryError frm = new frmHistoryError(Global.UserID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }
        private void btnGiaHan_Click(object sender, EventArgs e)
        {
            //Int32[] selectedRowHandles = grvData.GetSelectedRows();
            //for (int i = 0; i < selectedRowHandles.Length; i++)
            //{
            //    // check.Add(Lib.ToInt(grvData.GetFocusedRowCellValue(colID)));  
            //    int selectedRowHandle = selectedRowHandles[i];
            //    if (selectedRowHandle >= 0)
            //    {
            //        int id = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colID));
            //        if (id == 0) return;
            //        HistoryProductRTCModel model = (HistoryProductRTCModel)HistoryProductRTCBO.Instance.FindByPK(id);
            //        model.DateReturnExpected = dtpNgayGiaHan.Value;
            //        HistoryProductRTCBO.Instance.Update(model);
            //    }
            //}
            //LoadData();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(view.GetFocusedRowCellValue(view.FocusedColumn)));
                e.Handled = true;
            }
        }

        private void btnBorrowDetail_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colBorrowID));
            if (ID == 0)
            {
                MessageBox.Show("TRONG KHOẢNG NGÀY NÀY, KHÔNG CÓ DỮ LIỆU PHIẾU MƯỢN CHO BẢN GHI NÀY!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            } 
                
            BillExportModel model = (BillExportModel)BillExportBO.Instance.FindByPK(ID);
            frmBillExportDetailNew frm = new frmBillExportDetailNew(false);
            frm.WarehouseCode = ((WarehouseModel)WarehouseBO.Instance.FindByPK(warehouseID)).WarehouseCode;
            frm.billExport = model;
            frm.IDDetail = ID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                grvData.FocusedRowHandle = focusedRowHandle;
            }
        }

        private void btnReturnDetail_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colReturnID));
            if (ID == 0)
            {
                MessageBox.Show("TRONG KHOẢNG NGÀY NÀY, KHÔNG CÓ DỮ LIỆU PHIẾU TRẢ CHO BẢN GHI NÀY!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            BillImportModel model = (BillImportModel)BillImportBO.Instance.FindByPK(ID);
            frmBillImportDetail frm = new frmBillImportDetail();
            frm.WarehouseCode = ((WarehouseModel)WarehouseBO.Instance.FindByPK(warehouseID)).WarehouseCode;
            frm.billImport = model;
            frm.IDDetail = ID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                grvData.FocusedRowHandle = focusedRowHandle;
            }
        }

        private void btnExportExcel_Click_1(object sender, EventArgs e)
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

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn)));
            }
            catch
            {
                Clipboard.SetText(" ");
            }
        }
    }
}

