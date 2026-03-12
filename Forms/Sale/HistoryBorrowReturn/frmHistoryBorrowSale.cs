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
    public partial class frmHistoryBorrowSale : _Forms
    {
        public ProductGroupRTCModel _RTCModel = new ProductGroupRTCModel();
        public int Status = -1;
        private int warehouseID;
        private bool viewAll = true;

        public frmHistoryBorrowSale()
        {
            InitializeComponent();
        }

        public frmHistoryBorrowSale(int WarehouseID, bool ViewAll)
        {
            InitializeComponent();
            warehouseID = WarehouseID;
            viewAll = ViewAll;
            if (viewAll)
                cboEmployee.ReadOnly = false;
            else
                cboEmployee.ReadOnly = true;
        }
        bool firstLoad = true;
        private void frmListTool_Load(object sender, EventArgs e)
        {
            this.Text += " - " + SQLHelper<WarehouseModel>.FindByID(warehouseID).WarehouseCode;
            dtpFromDate.Value = new DateTime(DateTime.Now.Year, 1, 1);
            txtPageNumber.Text = "1";
            cbStatus.SelectedIndex = 1;

            loadCboProduct();
            loadCboEmployee();
            loadData();


        }

        private void loadCboProduct()
        {
            ArrayList _lstProductGroup = ProductGroupBO.Instance.FindByAttribute("IsVisible", 1);
            cboProductGroup.Properties.DataSource = _lstProductGroup;
            cboProductGroup.Properties.DisplayMember = "ProductGroupName";
            cboProductGroup.Properties.ValueMember = "ID";
        }
        private void loadCboEmployee()
        {
            //List<EmployeeModel> _lstEmployee = SQLHelper<EmployeeModel>.FindAll();
            List<UsersModel> _lstEmployee = SQLHelper<UsersModel>.FindAll();
            cboEmployee.Properties.DataSource = _lstEmployee;
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.ValueMember = "ID";

            cboEmployee.EditValue = viewAll ? 0 : Global.UserID;
        }
        private void loadData()
        {
            DateTime _dateBegin = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            DateTime _dateEnd = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);

            int _pageNumber = TextUtils.ToInt(txtPageNumber.Text);
            int _pageSize = TextUtils.ToInt(txtPageSize.Value);
            int _productGroupID = TextUtils.ToInt(cboProductGroup.EditValue);
            int _employeeID = TextUtils.ToInt(cboEmployee.EditValue);
            string _filterText = TextUtils.ToString(txtFilterText.Text);

            DataTable dt = TextUtils.LoadDataFromSP("spGetHistoryBorrowSale", "A",
                new string[] { "@PageNumber", "@PageSize", "@DateBegin", "@DateEnd", "@ProductGroupID", "@ReturnStatus", "@FilterText", "@WareHouseID", "@EmployeeID" },
                new object[] { _pageNumber, _pageSize, _dateBegin, _dateEnd, _productGroupID, cbStatus.SelectedIndex - 1, _filterText, warehouseID, _employeeID });
            grdData.DataSource = dt;

            if (dt.Rows.Count == 0)
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

        private void btnBorrow_Click(object sender, EventArgs e)
        {
            frmProductHistoryBorrowDetail frm = new frmProductHistoryBorrowDetail(warehouseID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadData();
            //if (firstLoad) return;
            //FindData();
        }

        private void frmHistoryBorrowSale_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    FindData();
            //    return;
            //}

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
                loadData();
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

        private void btnLose_Click(object sender, EventArgs e)
        {
            frmProductHistoryLoseDetail frm = new frmProductHistoryLoseDetail(warehouseID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }

        private void btnBroken_Click(object sender, EventArgs e)
        {
            frmProductHistoryBrokenDetail frm = new frmProductHistoryBrokenDetail(warehouseID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
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

        private void btnListError_Click(object sender, EventArgs e)
        {
            frmHistoryError frm = new frmHistoryError(Global.UserID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnBorrowDetail_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colBillID));
            if (ID == 0) return;

            //BillExportModel model = (BillExportModel)BillExportBO.Instance.FindByPK(ID);
            BillExportModel model = SQLHelper<BillExportModel>.FindByID(ID);
            frmBillExportDetailNew frm = new frmBillExportDetailNew(false);
            //frm.WarehouseCode = ((WarehouseModel)WarehouseBO.Instance.FindByPK(warehouseID)).WarehouseCode;
            frm.WarehouseCode = SQLHelper<WarehouseModel>.FindByID(warehouseID).WarehouseCode;
            frm.billExport = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
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
            //BillImportModel model = (BillImportModel)BillImportBO.Instance.FindByPK(ID);
            BillImportModel model = SQLHelper<BillImportModel>.FindByID(ID);
            frmBillImportDetail frm = new frmBillImportDetail();
            //frm.WarehouseCode = ((WarehouseModel)WarehouseBO.Instance.FindByPK(warehouseID)).WarehouseCode;
            frm.WarehouseCode = SQLHelper<WarehouseModel>.FindByID(warehouseID).WarehouseCode;
            frm.billImport = model;
            frm.IDDetail = ID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
                grvData.FocusedRowHandle = focusedRowHandle;
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                grvData.OptionsSelection.MultiSelect = false;
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
                grvData.OptionsSelection.MultiSelect = true;
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

        List<int> _idDetails = new List<int>();
        private void btnCreateImport_Click(object sender, EventArgs e)
        {
            int[] _rowHandle = grvData.GetSelectedRows();
            if (_rowHandle.Length == 0)
            {
                MessageBox.Show("VUI LÒNG TÍCH CHỌN VÀO BẢN GHI BẠN CẦN SINH PHIẾU TRẢ!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string warehouseCode = SQLHelper<WarehouseModel>.FindByID(warehouseID).WarehouseCode;
            //List<int> _borrowIDs = new List<int>();
            DataTable _dataHistory = ((DataTable)grdData.DataSource).Clone();
            _idDetails = new List<int>();
            foreach (int index in _rowHandle)
            {
                //_borrowIDs.Add(Lib.ToInt(grvData.GetRowCellValue(index, colBorrowID)));
                bool returnedStatus = TextUtils.ToBoolean(grvData.GetRowCellValue(index, colReturnedStatus));
                if (returnedStatus) continue;

                DataRow r = grvData.GetDataRow(index);
                _dataHistory.ImportRow(r);

                _idDetails.Add(TextUtils.ToInt(grvData.GetRowCellValue(index, colBorrowID)));
            }

            //DataTable filterData = _dataHistory.AsEnumerable().Where(row => Lib.ToInt(row.Field<int>("ProductGroupID")) == groupID).CopyToDataTable();
            List<string> distinctReturner = _dataHistory.AsEnumerable().Select(row => row.Field<string>("Code")).Distinct().ToList();
            if (distinctReturner.Count > 1)
            {
                MessageBox.Show("VUI LÒNG CHỌN CÁC SẢN PHẨM CHỈ TRONG 1 NGƯỜI MƯỢN ĐỂ TẠO PHIẾU TRẢ", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            List<int> distinctGroups = _dataHistory.AsEnumerable().Select(row => row.Field<int>("ProductGroupID")).Distinct().ToList();
            foreach (int groupID in distinctGroups)
            {
                DataTable filterData = _dataHistory.AsEnumerable().Where(row => Lib.ToInt(row.Field<int>("ProductGroupID")) == groupID).CopyToDataTable();
                frmBillImportDetail frm = new frmBillImportDetail();
                frm.WarehouseCode = warehouseCode;
                frm.groupID = groupID;
                frm._dataHistory = filterData;
                frm.cboBillTypeNew.EditValue = 1;
                frm.ShowDialog();
            }

            loadData();

            keepSelectedHandle();
        }

        private void keepSelectedHandle()
        {
            grvData.ClearSelection();
            DataTable source = (DataTable)grdData.DataSource;
            List<DataRow> _rowHandles = source.AsEnumerable().Where(row => _idDetails.Contains(TextUtils.ToInt(row.Field<int>(colBorrowID.FieldName)))).ToList();
            foreach (DataRow row in _rowHandles)
            {
                int rowHandle = source.Rows.IndexOf(row);
                grvData.SelectRow(rowHandle);
            }
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnApproveReturned_Click(object sender, EventArgs e)
        {
            int[] _rowHandle = grvData.GetSelectedRows();
            if (_rowHandle.Length == 0)
            {
                MessageBox.Show("VUI LÒNG TÍCH CHỌN VÀO BẢN GHI CẦN DUYỆT TRẢ!", Lib.Caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            ApproveReturned(true, _rowHandle);
        }

        private void btnCancelReturned_Click(object sender, EventArgs e)
        {
            int[] _rowHandle = grvData.GetSelectedRows();
            if (_rowHandle.Length == 0)
            {
                MessageBox.Show("VUI LÒNG TÍCH CHỌN VÀO BẢN GHI CẦN HỦY DUYỆT TRẢ!", Lib.Caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            ApproveReturned(false, _rowHandle);
        }

        private void ApproveReturned(bool Returned, int[] _rowHandle)
        {
            List<int> IDs = new List<int>();
            foreach (int index in _rowHandle)
            {
                IDs.Add(Lib.ToInt(grvData.GetRowCellValue(index, colBorrowID)));
            }

            if (IDs.Count == 0) return;
            string strIDs = string.Join(", ", IDs);
            int status = Returned ? 1 : 0;
            string sql = $"UPDATE BillExportDetail SET ReturnedStatus = {status}," +
                            $"UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd")}', " +
                            $"UpdatedBy = '{Global.LoginName}' " +
                            $"WHERE ID IN ({strIDs})";
            //TextUtils.ExcuteScalar(sql);
            TextUtils.ExcuteSQL(sql);
            loadData();
        }

        private void btnSummaryReturnDetail_Click(object sender, EventArgs e)
        {
            int _borrowID = Lib.ToInt(grvData.GetFocusedRowCellValue(colBorrowID));
            frmSummaryReturnDetail frm = new frmSummaryReturnDetail(warehouseID);
            frm._exportDetailID = _borrowID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //loadData();
            }
        }

        private void frmHistoryBorrowSale_Activated(object sender, EventArgs e)
        {
            if (firstLoad)
            {
                firstLoad = false;
                return;
            }
            loadData();
            keepSelectedHandle();
        }

        private void cboProductGroup_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void grvData_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            if (TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colDualDate)) == 1)
            {
                e.Appearance.BackColor = Color.Yellow;
            }


            //Check quá hạn
            DateTime? expectReturnDate = TextUtils.ToDate4(grvData.GetRowCellValue(e.RowHandle, colExpectReturnDate));
            if (expectReturnDate.HasValue)
            {
                if (expectReturnDate.Value.Date < DateTime.Now.Date)
                {
                    e.Appearance.BackColor = Color.Pink;
                }
            }
        }
    }
}

