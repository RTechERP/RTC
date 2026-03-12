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
using Forms.Classes;
using DevExpress.XtraGrid.Views.Grid;
using static Forms.Classes.DelegateEvents;
using Forms.Technical;
using DevExpress.Utils;
using BMS.Utils;

namespace BMS
{
    public partial class frmProductHistory : _Forms
    {
        public event ChildFormAcivated OnChildFormAcivated;
        public event ChildFormClosed OnChildFormClosed;
        public ProductGroupRTCModel _RTCModel = new ProductGroupRTCModel();
        public List<List<int>> ListGrv = new List<List<int>>();
        //public List<HistoryProductRTCModel> List = new List<HistoryProductRTCModel>();
        //public HistoryProductRTCModel oHistoryModel = new HistoryProductRTCModel();
        public int warehouseID;

        public bool IsbtnXuat;// Nếu nút này được bấm từ from PHIẾU XUẤT KỸ THUẬT thì bật nút btnXuat lên;
        public bool IsbtnExport;// Nếu nút này được bấm từ from PHIẾU XUẤT KỸ THUẬT thì bật nút btnXuat lên;
        public int Status = -1;
        public frmProductHistory()
        {
            InitializeComponent();
        }
        public frmProductHistory(int WarehouseID)
        {
            InitializeComponent();
            warehouseID = WarehouseID;
        }
        private void frmListTool_Load(object sender, EventArgs e)
        {
            btnXuat.Visible = IsbtnXuat;
            btnExport.Visible = IsbtnExport;
            //cboStatus1.SelectedIndex = 2;
            //this.Text += warehouseID == 1 ? " - HN" : (warehouseID == 2 ? " - HCM" : " - BN");
            this.Text += $" - {this.Tag}";

            // ngày bắt đầu khi load form bằng ngày hiện tại trừ đi 1 tháng
            //dtpFromDate.Value = Convert.ToDateTime(DateTime.Now.AddYears(-1).ToShortDateString());

            DateTime date = DateTime.Now.AddMonths(-1);
            dtpFromDate.Value = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);

            cboStatus.Properties.Items[1].CheckState = CheckState.Checked;

            LoadEmployee();
            LoadData();
            //btnDeleteHistory.Visible = (Global.IsAdmin && Global.EmployeeID <= 0);
        }

        private void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployeeTeamAndDepartment", "A", new string[] { }, new object[] { });
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.ValueMember = "UserID";
            cboEmployee.Properties.DataSource = dt;
          
        }

        private void LoadData()
        {
            //DataTable dt = new DataTable();
            //dt = TextUtils.LoadDataFromSP("spHistoryProduct_New", "A", new string[] { "@Status" }, new object[] { Status });//spHistoryProduct

            DateTime ds = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0).AddSeconds(-1);
            DateTime de = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59).AddSeconds(+1);

            string status = TextUtils.ToString(cboStatus.EditValue);
            //int status = cboStatus1.SelectedIndex - 1;

            int pageNumber = TextUtils.ToInt(txtPageNumber.Text);
            int pageSize = TextUtils.ToInt(txtPageSize.Text);
            int userID = TextUtils.ToInt(cboEmployee.EditValue);

            using (WaitDialogForm fWait = new WaitDialogForm())
            {
                DataTable dtHistoryProduct = TextUtils.LoadDataFromSP("spGetHistoryProduct_New", "A",
                    new string[] { "@DateStart", "@DateEnd", "@Keyword", "@Status", "@WarehouseID", "@PageNumber", "@PageSize","@UserID" },
                    new object[] { ds, de, txtFilterText.Text.Trim(), status, warehouseID, pageNumber, pageSize,userID });

                //Stopwatch timer = new Stopwatch();
                //timer.Start();
                //var list = SQLHelper<HistoryProductRTCModel>.ProcedureToList("spGetHistoryProduct_New",
                //                                            new string[] { "@DateStart", "@DateEnd", "@Keyword", "@Status", "@WarehouseID" },
                //                                            new object[] { ds, de, txtFilterText.Text.Trim(), status, warehouseID });
                //timer.Stop();
                //MessageBox.Show(timer.ElapsedMilliseconds.ToString());

                grdData.DataSource = dtHistoryProduct;
                if (TextUtils.ToInt(status) == 2)
                {
                    colNumberBorrow.Caption = "Số lượng mất";
                    colPeople.Caption = "Người làm mất";
                    colDate1.Caption = "Ngày làm mất";
                }
                else
                {
                    if (TextUtils.ToInt(status) == 3)
                    {
                        colNumberBorrow.Caption = "Số lượng hỏng";
                        colPeople.Caption = "Người làm hỏng";
                        colDate1.Caption = "Ngày xác nhận";
                    }
                    else
                    {
                        colNumberBorrow.Caption = "Số lượng mượn";
                        colPeople.Caption = "Người mượn";
                        colDate1.Caption = "Ngày mượn";
                    }
                }
                if(dtHistoryProduct.Rows.Count > 0)
                {
                    txtTotalPage.Text = (dtHistoryProduct.Rows[0]["TotalPage"]).ToString();
                }
            }
        }
        /// <summary>
        /// chọn đăng kí mượn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBorrow_Click(object sender, EventArgs e)
        {
            frmProductHistoryBorrowDetail frm = new frmProductHistoryBorrowDetail(warehouseID);
            //frm.Status = new status(receive);
            //frm.Show();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }
        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            if (Global.IsRoot || Global.UserID == 24)
            {
                int id = Lib.ToInt(grvData.GetFocusedRowCellValue(colID));
                HistoryProductRTCModel model = (HistoryProductRTCModel)HistoryProductRTCBO.Instance.FindByPK(id);
                frmProductHistoryBorrowDetailAdmin frm = new frmProductHistoryBorrowDetailAdmin(warehouseID);
                frm._id = id;
                frm.historyProductRTC = model;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            //FindData();

            LoadData();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //FindData();

            LoadData();
        }
        private void btnReturn_Click(object sender, EventArgs e)
        {

            int userId = Global.UserID;
            bool isAdmin = Global.IDAdminDemo.Contains(userId);
            try
            {
                DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn trả sản phẩm", "Thông báo", MessageBoxButtons.YesNo);
                if (rs == DialogResult.No) return;
                Int32[] selectedRowHandles = grvData.GetSelectedRows();
                for (int i = 0; i < selectedRowHandles.Length; i++)
                {
                    // check.Add(Lib.ToInt(grvData.GetFocusedRowCellValue(colID)));  
                    int selectedRowHandle = selectedRowHandles[i];

                    int BillExportTechnicalID = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colBillExportTechnicalID)); //Update version 23/09/2022

                    if (selectedRowHandle >= 0)
                    {
                        int id = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colID));
                        if (id == 0) return;

                        int modulaLocationDetailID = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colModulaLocationDetailID));
                        HistoryProductRTCModel model = (HistoryProductRTCModel)HistoryProductRTCBO.Instance.FindByPK(id);
                        if (model.Status != 1 && model.Status != 4 && model.Status != 7)
                        {
                        }
                        else
                        {
                            if (Global.IsAdmin || isAdmin/*Global.UserID == 24 || Global.UserID == 88*/)//Check là admin, Admin HN, Admin HCM
                            {
                                if (modulaLocationDetailID > 0 && model.StatusPerson <= 0 && !(Global.IsAdmin && Global.EmployeeID <= 0))
                                {
                                    MessageBox.Show("Nhân viên chưa hoàn thành thao tác trả hàng.\nBạn không thể duyệt trả!", "Thông báo");
                                    return;
                                }

                                model.Status = 0;
                                model.DateReturn = DateTime.Now;
                                model.AdminConfirm = true;
                                HistoryProductRTCBO.Instance.Update(model);
                                int IDProduct = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colProductRTCID));
                                
                                TextUtils.ExcuteProcedure(StoreProcedures.spUpdateStatusProductRTCQRCode, new string[] { "@ProductRTCQRCodeID", "@Status" }, new object[] { model.ProductRTCQRCodeID, 1 });
                            }
                            else
                            {
                                //MessageBox.Show("Tài khoản Admin mới có thể sử dụng !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                model.Status = 4;
                                HistoryProductRTCBO.Instance.Update(model);
                            }
                            // model.NumberBorrow = model.NumberBorrow - TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colNumberBorrow));
                        }
                    }
                }
                LoadData();
            }
            catch
            {

            }
        }
        private void btnDuyenMuon_Click(object sender, EventArgs e)
        {
            Int32[] selectedRowHandles = grvData.GetSelectedRows();
            if (selectedRowHandles.Length <= 0) return;
            for (int i = 0; i < selectedRowHandles.Length; i++)
            {
                int selectedRowHandle = selectedRowHandles[i];
                int productHistoryID = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colID));
                int Status = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colStatusNew));
                if (productHistoryID == 0) return;

                int modulaLocationDetailID = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colModulaLocationDetailID));
                
                if (Status == 7)
                {

                    HistoryProductRTCModel historyProductRTCModel = (HistoryProductRTCModel)HistoryProductRTCBO.Instance.FindByPK(productHistoryID);
                    if (modulaLocationDetailID > 0 && historyProductRTCModel.StatusPerson <= 0)
                    {
                        MessageBox.Show("Nhân viên chưa hoàn thành thao tác lấy hàng.\nBạn không thể duyệt!", "Thông báo");
                        return;
                    }
                    historyProductRTCModel.Status = 1;
                    HistoryProductRTCBO.Instance.Update(historyProductRTCModel);
                }
            }
            LoadData();
        }
        private void txtFilterText_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //	FindData();
            //}
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
        private void btnLose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Chức năng chưa sửa đừng dùng .", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                return;
            }
            frmProductHistoryLoseDetail frm = new frmProductHistoryLoseDetail();
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
            //if (Global.IsRoot)
            //{
            //    if (e.Button == MouseButtons.Right)
            //    {
            //        ContextMenu m = new ContextMenu();
            //        this.ContextMenu = m;
            //        m.MenuItems.Add(new MenuItem("Ghi lại lỗi quy trình nhập xuất kho cá nhân", new System.EventHandler(this.Cancel_Click)));
            //        m.Show(this, new Point(e.X, e.Y));
            //    }
            //}
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            frmAddErrorPersonal frm = new frmAddErrorPersonal();
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
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
            frmProductHistoryExcel frmpHistoryExcel = new frmProductHistoryExcel();
            frmpHistoryExcel.Status = Status;
            frmpHistoryExcel.ShowDialog();
        }

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (Status == 2 || Status == 3) return;

            //DateTime nowDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            //DateTime DateReturnExpected = TextUtils.ToDate3(TextUtils.ToDate3(grvData.GetRowCellValue(e.RowHandle, colDateReturmExpected)).ToString("yyyy/MM/dd"));

            int statusNew = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colStatusNew));
            int BillExportTechnicalID = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colBillExportTechnicalID)); //Update version 23/09/2022

            if (/*nowDate.CompareTo(DateReturnExpected) == 0*/statusNew == 6)
            {
                e.Appearance.BackColor = Color.FromArgb(255, 255, 74); // Sắp đến ngày
                e.Appearance.ForeColor = Color.Black;
            }

            if (/*nowDate.CompareTo(DateReturnExpected) > 0*/statusNew == 5)
            {
                e.Appearance.BackColor = Color.FromArgb(239, 31, 62); // Quá hạn
                e.Appearance.ForeColor = Color.White;
            }

            //nếu phiếu mượn được tạo từ phiếu xuất thì sẽ có màu xanh da giời nhạt nhạt
            if (BillExportTechnicalID > 0)
            {
                e.Appearance.BackColor = Color.FromArgb(128, 255, 255);
                e.Appearance.ForeColor = Color.Black;
            }


            if (Lib.ToInt(grvData.GetRowCellValue(e.RowHandle, colStatus)) == 4)
            {
                e.Appearance.BackColor = Color.Lime;
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void btnListError_Click(object sender, EventArgs e)
        {
            frmHistoryError frm = new frmHistoryError(0);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }
        private void btnGiaHan_Click(object sender, EventArgs e)
        {
            Int32[] selectedRowHandles = grvData.GetSelectedRows();
            for (int i = 0; i < selectedRowHandles.Length; i++)
            {
                // check.Add(Lib.ToInt(grvData.GetFocusedRowCellValue(colID)));  
                int selectedRowHandle = selectedRowHandles[i];
                if (selectedRowHandle >= 0)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colID));
                    if (id == 0) return;
                    HistoryProductRTCModel model = (HistoryProductRTCModel)HistoryProductRTCBO.Instance.FindByPK(id);
                    model.DateReturnExpected = dtpNgayGiaHan.Value;
                    HistoryProductRTCBO.Instance.Update(model);

                    //Update lịch sử gia hạn
                    HistoryProductRTCLogModel logModel = new HistoryProductRTCLogModel();
                    logModel.HistoryProductRTCID = id;
                    logModel.DateReturnExpected = TextUtils.ToDate4(grvData.GetRowCellValue(selectedRowHandle, colDateReturmExpected));
                    SQLHelper<HistoryProductRTCLogModel>.Insert(logModel);
                }
            }
            LoadData();
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            List<HistoryProductRTCModel> list = new List<HistoryProductRTCModel>();
            Int32[] selectedRowHandles = grvData.GetSelectedRows();
            if (selectedRowHandles.Length <= 0) return;
            for (int i = 0; i < selectedRowHandles.Length; i++)
            {
                // check.Add(Lib.ToInt(grvData.GetFocusedRowCellValue(colID)));  
                int selectedRowHandle = selectedRowHandles[i];
                if (selectedRowHandle >= 0)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colID));
                    string Date = TextUtils.ToString(grvData.GetRowCellValue(selectedRowHandle, colDate2));
                    if (id == 0 || Date.Trim().Length > 0) continue;
                    HistoryProductRTCModel model = (HistoryProductRTCModel)HistoryProductRTCBO.Instance.FindByPK(id);
                    list.Add(model);
                }
            }
            if (list.Count <= 0) return;
            frmEditPerson frm = new frmEditPerson();
            //frmEditPerson_New frm = new frmEditPerson_New();
            frm.list = list;
            frm.ProductName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductName));
            frm.ProductCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductCode));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }




        private void btnBorrowNew_Click(object sender, EventArgs e)
        {

            frmProductHistoryBorrowDetailNew frm = new frmProductHistoryBorrowDetailNew(warehouseID, new List<string>());
            //frm.Status = new status(receive);
            //frm.Show();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }
        private void btnXuat_Click(object sender, EventArgs e)
        {
            int[] RowIndex = grvData.GetSelectedRows();
            for (int i = 0; i < RowIndex.Length; i++)
            {
                //List <int> list= new List<int>();
                //list.Add(TextUtils.ToInt(grvData.GetRowCellValue(RowIndex[i], colProductRTCID)));
                //list.Add(TextUtils.ToInt(grvData.GetRowCellValue(RowIndex[i], colNumberBorrow)));
                //ListGrv.Add(list);


                //Update version 23/09/2022
                List<int> list = new List<int>();
                list.Add(TextUtils.ToInt(grvData.GetRowCellValue(RowIndex[i], colProductRTCID)));
                list.Add(TextUtils.ToInt(grvData.GetRowCellValue(RowIndex[i], colNumberBorrow)));
                list.Add(TextUtils.ToInt(grvData.GetRowCellValue(RowIndex[i], colID)));
                int colBillExport = TextUtils.ToInt(grvData.GetRowCellValue(RowIndex[i], colBillExportTechnicalID));
                if (colBillExport > 0)
                {
                    ListGrv.Add(list);
                }
            }
            this.DialogResult = DialogResult.OK;

        }
        public List<DataRow> listRow = new List<DataRow>();
        private void btnExport_Click(object sender, EventArgs e)
        {
            int[] RowIndex = grvData.GetSelectedRows();
            for (int i = 0; i < RowIndex.Length; i++)
            {
                //Update version 1/11/2022
                DataRow Row = grvData.GetDataRow(RowIndex[i]);
                int BillTechExportID = TextUtils.ToInt(Row["BillExportTechnicalID"]);
                if (BillTechExportID <= 0)
                {
                    listRow.Add(Row);
                }

            }
            this.DialogResult = DialogResult.OK;
        }

        //HuyNV 5/11/2022
        private void btnLoseNew_Click(object sender, EventArgs e)
        {
            List<HistoryProductRTCModel> list = new List<HistoryProductRTCModel>();
            Int32[] selectedRowHandles = grvData.GetSelectedRows();
            if (selectedRowHandles.Length <= 0) return;
            for (int i = 0; i < selectedRowHandles.Length; i++)
            {
                // check.Add(Lib.ToInt(grvData.GetFocusedRowCellValue(colID)));  
                int selectedRowHandle = selectedRowHandles[i];
                if (selectedRowHandle >= 0)
                {
                    int ProductRTCQRCodeID = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colProductRTCQRCodeID));
                    int ProductID = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colProductRTCID));
                    if (ProductRTCQRCodeID <= 0 || ProductID <= 0) return;
                    TextUtils.ExcuteProcedure(StoreProcedures.spUpdateStatusProductRTCQRCode, new string[] { "@ProductRTCQRCodeID", "@Status" }, new object[] { ProductRTCQRCodeID, 4 });
                    //TextUtils.ExcuteSQL($"spUpdateStatusProductRTCQRCode '{ProductRTCQRCodeID}','{4}'");

                    //ProductRTCModel productRTCModel = (ProductRTCModel)ProductRTCBO.Instance.FindByPK(ProductID);
                    //productRTCModel.NumberInStore = productRTCModel.NumberInStore - 1;

                }
            }
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

        private void btnReturnNew_Click(object sender, EventArgs e)
        {
            frmReturnAdmin frm = new frmReturnAdmin(warehouseID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void frmProductHistory_Activated(object sender, EventArgs e)
        {
            if (OnChildFormAcivated != null)
                OnChildFormAcivated(this);
        }

        private void frmProductHistory_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (OnChildFormClosed != null)
                OnChildFormClosed(this);
        }

        private void grvData_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column != colPeople)
            {
                return;
            }

            if (Status == 2 || Status == 3) return;

            //DateTime nowDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            //DateTime DateReturnExpected = TextUtils.ToDate3(TextUtils.ToDate3(grvData.GetRowCellValue(e.RowHandle, colDateReturmExpected)).ToString("yyyy/MM/dd"));

            int statusNew = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colStatusNew));
            int BillExportTechnicalID = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colBillExportTechnicalID)); //Update version 23/09/2022

            if (/*nowDate.CompareTo(DateReturnExpected) == 0*/statusNew == 6)
            {
                e.Appearance.BackColor = Color.FromArgb(255, 255, 74); // Sắp đến ngày
                e.Appearance.ForeColor = Color.Black;
            }

            if (/*nowDate.CompareTo(DateReturnExpected) > 0*/statusNew == 5)
            {
                e.Appearance.BackColor = Color.FromArgb(239, 31, 62); // Quá hạn
                e.Appearance.ForeColor = Color.White;
            }

            //nếu phiếu mượn được tạo từ phiếu xuất thì sẽ có màu xanh da giời nhạt nhạt
            if (BillExportTechnicalID > 0)
            {
                e.Appearance.BackColor = Color.FromArgb(128, 255, 255);
                e.Appearance.ForeColor = Color.Black;
            }


            if (Lib.ToInt(grvData.GetRowCellValue(e.RowHandle, colStatus)) == 4)
            {
                e.Appearance.BackColor = Color.Lime;
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void btnHistoryLog_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id <= 0)
            {
                return;
            }

            frmHistoryProductRTCLog frm = new frmHistoryProductRTCLog();
            frm.historyID = id;
            frm.Show();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (!Global.IsRoot)
            {
                btnWriteError.Visible = false;
            }
            else
            {
                btnWriteError.Visible = true;
            }
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            int productID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductRTCID));
            frmMaterialDetailOfProductRTC frm = new frmMaterialDetailOfProductRTC(warehouseID);
            frm.ProductRTCID = productID;
            frm.ShowDialog();
        }

        private void btnShowBillExport_Click(object sender, EventArgs e)
        {
            int billExportID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colBillExportTechnicalID));
            if (billExportID <= 0) return;
            BillExportTechnicalModel model = SQLHelper<BillExportTechnicalModel>.FindByID(billExportID);
            frmBillExportTechDetail_New frm = new frmBillExportTechDetail_New(warehouseID);
            frm.billExport = model;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnDeleteHistory_Click(object sender, EventArgs e)
        {

            List<int> ids = new List<int>();
            int[] selectedRows = grvData.GetSelectedRows();

            if (selectedRows.Length <= 0)
            {
                MessageBox.Show("Vui lòng chọn thiết bị muốn xóa khỏi lịch sử mượn!", "Thông báo");
                return;
            }

            DialogResult dialog = MessageBox.Show("Bạn có chắc muốn xóa danh sách thiết bị đã chọn khỏi lịch sử mượn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.No) return;
            foreach (int row in selectedRows)
            {
                int id = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
                if (!ids.Contains(id)) ids.Add(id);
                //HistoryProductRTCBO.Instance.Delete(TextUtils.ToInt(grvData.GetRowCellValue(row, colID)));
            }


            if (ids.Count <= 0) return;
            var myDict = new Dictionary<string, object>()
            {
                { "IsDelete",true},
                { "UpdatedBy",Global.AppUserName},
                { "UpdatedDate",DateTime.Now},
            };

            var exp = new Expression("ID", string.Join(",", ids), "IN");
            SQLHelper<HistoryProductRTCModel>.UpdateFields(myDict, exp);
            LoadData();
        }

        private void cboStatus_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) > TextUtils.ToInt(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            LoadData();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = TextUtils.ToInt(txtPageNumber.Text) > 1 ? (TextUtils.ToInt(txtPageNumber.Text) - 1).ToString() : "1";
            LoadData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) >= TextUtils.ToInt(txtTotalPage.Text)) return;
            txtPageNumber.Text = (TextUtils.ToInt(txtPageNumber.Text) + 1).ToString();
            LoadData();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) >= TextUtils.ToInt(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            LoadData();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnProductHistoryLate_Click(object sender, EventArgs e)
        {
            frmProductHistoryLate frm = new frmProductHistoryLate(1);
            frm.Tag = "HN";
            frm.ShowDialog();
        }

        
    }
}

