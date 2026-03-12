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

namespace BMS
{
    public partial class frmProductHistoryNew : _Forms
    {
        public ProductGroupRTCModel _RTCModel = new ProductGroupRTCModel();
        public List<List<int>> ListGrv = new List<List<int>>();
        //public List<HistoryProductRTCModel> List = new List<HistoryProductRTCModel>();
        //public HistoryProductRTCModel oHistoryModel = new HistoryProductRTCModel();


        public bool IsbtnXuat;// Nếu nút này được bấm từ from PHIẾU XUẤT KỸ THUẬT thì bật nút btnXuat lên;
        public int Status = -1;
        int warehouseID;
        public frmProductHistoryNew(int WarehouseID)
        {
            InitializeComponent();
            warehouseID = WarehouseID;
        }
        private void frmListTool_Load(object sender, EventArgs e)
        {
            btnXuat.Visible = IsbtnXuat;

            LoadData();
            // ngày bắt đầu khi load form bằng ngày hiện tại trừ đi 1 tháng
            dtpFromDate.Value = Convert.ToDateTime(DateTime.Now.AddYears(-1).ToShortDateString());
        }
        private void LoadData()
        {
            DataTable dt = new DataTable();
            dt = TextUtils.LoadDataFromSP("spHistoryProduct_New", "A", new string[] { "@Status" }, new object[] { Status });
            grdData.DataSource = dt;
            if (Status == 2)
            {
                colNumberBorrow.Caption = "Số lượng mất";
                colPeople.Caption = "Người làm mất";
                colDate1.Caption = "Ngày làm mất";
            }
            else
            {
                if (Status == 3)
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
        }
        /// <summary>
        /// chọn đăng kí mượn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBorrow_Click(object sender, EventArgs e)
        {
            frmProductHistoryBorrowDetailNew frm = new frmProductHistoryBorrowDetailNew(warehouseID, new List<string>());
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
            //frmProductHistoryBorrowDetail frm = new frmProductHistoryBorrowDetail();
            ////frm.Status = new status(receive);
            ////frm.Show();
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    LoadData();
            //}
        }
        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            if (Global.IsRoot)
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
            FindData();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FindData();
        }
        private void btnReturn_Click(object sender, EventArgs e)
        {
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
                        HistoryProductRTCModel model = (HistoryProductRTCModel)HistoryProductRTCBO.Instance.FindByPK(id);
                        if (model.Status != 1 && model.Status != 4)
                        {
                        }
                        else
                        {
                            if (Global.IsAdmin)
                            {
                                if (BillExportTechnicalID > 0) return;//Update version 23/09/2022

                                model.Status = 0;
                                model.DateReturn = DateTime.Now;
                                model.AdminConfirm = true;
                                HistoryProductRTCBO.Instance.Update(model);
                                int IDProduct = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colProductRTCID));
                                //ArrayList arrProduct = ProductRTCBO.Instance.FindByAttribute("ID", grvData.GetRowCellValue(selectedRowHandle, colProductCode).ToString());
                                ProductRTCModel pModel = (ProductRTCModel)ProductRTCBO.Instance.FindByPK(IDProduct);
                                if (pModel.ID > 0)
                                {

                                    //Update version 23/09/2022
                                    if (BillExportTechnicalID <= 0)
                                    {
                                        //Update lại số lượng tồn kho khi trả đồ mượn
                                        pModel.NumberInStore += TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colNumberBorrow));
                                    }

                                    //pModel.NumberInStore += TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colNumberBorrow));
                                    ProductRTCBO.Instance.Update(pModel);
                                    _Forms frmProduct = (_Forms)Application.OpenForms["frmProductRTC"];
                                    _Forms frmMain = (_Forms)Application.OpenForms["frmMain"];
                                    if (frmProduct != null && frmMain != null)
                                    {
                                        frmProduct.Dispose();
                                        TextUtils.OpenChildForm(new frmProductRTC(1), frmMain);
                                        TextUtils.OpenChildForm(new frmProductHistory(), frmMain);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Tài khoản Admin mới có thể sử dụng !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        private void txtFilterText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FindData();
            }
        }
        private void FindData()
        {
            string _data = TextUtils.ToString(txtFilterText.Text.Trim());
            int status = cboStatus.SelectedIndex - 1;

            DateTime _dateBegin = dtpFromDate.Value;
            _dateBegin = new DateTime(_dateBegin.AddDays(-1).Year, _dateBegin.AddDays(-1).Month, _dateBegin.AddDays(-1).Day, 23, 59, 59);
            DateTime _dateEnd = dtpEndDate.Value;
            _dateEnd = new DateTime(_dateEnd.AddDays(+1).Year, _dateEnd.AddDays(+1).Month, _dateEnd.AddDays(+1).Day, 0, 0, 0);


            if (!string.IsNullOrEmpty(_data) || status > -1)
            {
                _dateBegin = new DateTime(1950, 1, 1, 0, 0, 0);
            }


            DataTable dt = new DataTable();
            dt = TextUtils.LoadDataFromSP("spFindHistoryBorrow", "A", new string[] { "@Name", "@DateBegin", "@DateEnd", "@Status" }, new object[] { _data, _dateBegin, _dateEnd, status });
            grdData.DataSource = dt;

            //if (checkDate())
            //{
            //	if (_data == "")
            //	{
            //		LoadData();
            //	}
            //	else
            //	{
            //		DataTable dt = new DataTable();
            //		dt = TextUtils.LoadDataFromSP("spFindHistoryBorrow", "A", new string[] { "@Name", "@DateEnd", "@DateBegin","@Status" }, new object[] { _data, _dateEnd, _dateBegin });
            //		grdData.DataSource = dt;
            //	}
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
            frmProductHistoryLoseDetail frm = new frmProductHistoryLoseDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnBroken_Click(object sender, EventArgs e)
        {
            frmProductHistoryBrokenDetail frm = new frmProductHistoryBrokenDetail(1);
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
            DateTime nowDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime DateReturnExpected = TextUtils.ToDate3(TextUtils.ToDate3(grvData.GetRowCellValue(e.RowHandle, colDateReturmExpected)).ToString("yyyy/MM/dd"));
            int BillExportTechnicalID = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colBillExportTechnicalID)); //Update version 23/09/2022
            if (grvData.GetRowCellValue(e.RowHandle, colDate2) == System.DBNull.Value)//Update version 23/09/2022
            {
                if (nowDate.CompareTo(DateReturnExpected) == 0)
                    e.Appearance.BackColor = Color.FromArgb(255, 255, 74);
                if (nowDate.CompareTo(DateReturnExpected) > 0)
                    e.Appearance.BackColor = Color.FromArgb(239, 31, 62);
                //nếu phiếu mượn được tạo từ phiếu xuất thì sẽ có màu xanh da giời nhạt nhạt
                if (BillExportTechnicalID > 0)
                {
                    e.Appearance.BackColor = Color.FromArgb(128, 255, 255);
                }
            }
            if (Lib.ToInt(grvData.GetRowCellValue(e.RowHandle, colStatus)) == 4)
            {
                e.Appearance.BackColor = Color.Lime;
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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmProductHistoryBorrowDetailNew frm = new frmProductHistoryBorrowDetailNew(warehouseID, new List<string>());
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }
    }
}

