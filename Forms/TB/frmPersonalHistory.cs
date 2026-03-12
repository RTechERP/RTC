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
    public partial class frmPersonalHistory : _Forms
    {
        public ProductGroupRTCModel _RTCModel = new ProductGroupRTCModel();
        //public HistoryProductRTCModel oHistoryModel = new HistoryProductRTCModel();
        public string Status = "";
        private int warehouseID;
        public frmPersonalHistory()
        {
            InitializeComponent();
        }
        public frmPersonalHistory(int WarehouseID)
        {
            InitializeComponent();
            warehouseID = WarehouseID;
        }

        private void frmListTool_Load(object sender, EventArgs e)
        {
            //cboStatus.Properties.Items[1].CheckState = CheckState.Checked;
            //cboStatus.Properties.Items[5].CheckState = CheckState.Checked;
            //cboStatus.Properties.Items[6].CheckState = CheckState.Checked;

            cboStatus.SetEditValue("1,5,6");


            // ngày bắt đầu khi load form bằng ngày hiện tại trừ đi 1 tháng
            dtpFromDate.Value = Convert.ToDateTime(DateTime.Now.AddYears(-1).ToShortDateString());

            btnGiaHan.Visible = labelGiaHanMuon.Visible = dtpNgayGiaHan.Visible = (Global.AppUserName.Trim().ToLower() == "admin"
                || Global.AppUserName.Trim().ToLower() == "thaonv");
            this.Text += warehouseID == 1 ? " - HN" : (warehouseID == 2 ? " - HCM" : " - BN");

            LoadData();
        }


        private void LoadData()
        {
            //DataTable dt = new DataTable();
            //dt = TextUtils.LoadDataFromSP("spHistoryProductByUserID", "A", new string[] { "@Status", "@UserID", "@WarehouseID" }, new object[] { Status, Global.UserID, warehouseID });
            //grdData.DataSource = dt;
            //if (Status == 2)
            //{
            //    colNumberBorrow.Caption = "Số lượng mất";
            //    colPeople.Caption = "Người làm mất";
            //    colDate1.Caption = "Ngày làm mất";
            //}
            //else
            //{
            //    if (Status == 3)
            //    {
            //        colNumberBorrow.Caption = "Số lượng hỏng";
            //        colPeople.Caption = "Người làm hỏng";
            //        colDate1.Caption = "Ngày xác nhận";
            //    }
            //    else
            //    {
            //        colNumberBorrow.Caption = "Số lượng mượn";
            //        colPeople.Caption = "Người mượn";
            //        colDate1.Caption = "Ngày mượn";
            //    }
            //}
            int pageNumber = TextUtils.ToInt(txtPageNumber.Text);
            int pageSize = TextUtils.ToInt(txtPageSize.Text);
            DateTime ds = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0).AddSeconds(-1);
            DateTime de = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59).AddSeconds(+1);
            Status = "";
            string status = cboStatus.EditValue.ToString();

            //string inputStatus = "";
            //string[] splitArr = status.Split(',');

            //for (int i = 0; i < splitArr.Length; i++)
            //{
            //    if (TextUtils.ToInt(splitArr[i]) == 0)
            //    {
            //        inputStatus = "0,1,2,3,4,5,6,7";
            //        continue;
            //    }

            //    int index = TextUtils.ToInt(splitArr[i]) - 1;
            //    inputStatus += index.ToString();
            //    if (i < splitArr.Length - 1)
            //    {
            //        inputStatus += ",";
            //    }
            //}          

            DataTable dtHistoryProduct = TextUtils.LoadDataFromSP("spGetHistoryProduct_New", "A",
                new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@Keyword", "@Status", "@WarehouseID", "@UserID" },
                new object[] { pageNumber, pageSize, ds, de, txtFilterText.Text.Trim(), status, warehouseID, Global.UserID });

            if (dtHistoryProduct.Rows.Count > 0)
            {
                txtTotalPage.Text = TextUtils.ToString(dtHistoryProduct.Rows[0]["TotalPage"]);
            }


            grdData.DataSource = dtHistoryProduct;
            for (int i = 0; i < grvData.RowCount; i++)
            {
                if (TextUtils.ToInt(grvData.GetRowCellValue(i, colStatus)) == 2)
                {
                    colNumberBorrow.Caption = "Số lượng mất";
                    colPeople.Caption = "Người làm mất";
                    colDate1.Caption = "Ngày làm mất";
                }
                else
                {
                    if (TextUtils.ToInt(grvData.GetRowCellValue(i, colStatus)) == 3)
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
        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            //int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //if (ID == 0) return;
            //ProductRTCModel model = (ProductRTCModel)ProductRTCBO.Instance.FindByPK(ID);
            //frmProductHistoryBorrowDetail frm = new frmProductHistoryBorrowDetail();
            //frm.oProductRTCModel = model;
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    LoadData();
            //}
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            //FindData();
            LoadData();
        }

        //private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    // int index = cboStatus.SelectedIndex;
        //    var x = cboStatus.Properties.GetCheckedItems();
        //    int index = TextUtils.ToInt(cboStatus.EditValue);
        //   // Status = index - 1;
        //    LoadData();
        //}

        private void btnReturn_Click(object sender, EventArgs e)
        {
            //int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //if (id == 0) return;
            //HistoryProductRTCModel model = (HistoryProductRTCModel)HistoryProductRTCBO.Instance.FindByPK(id);
            //if (model.Status != 1 && model.Status != 4)
            //{
            //    MessageBox.Show("Sản phẩm này không phải là sản phẩm đang mượn! Xin thử lại. ", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //string name = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductName));
            //DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn trả sản phẩm: [" + name.ToUpper() + "]", "Thông báo", MessageBoxButtons.YesNo);
            //if (rs == DialogResult.No) return;
            //if (Global.LoginName.ToLower().Equals("admin"))
            //{
            //    model.Status = 0;
            //    model.DateReturn = DateTime.Now;
            //    model.AdminConfirm = true;
            //    HistoryProductRTCBO.Instance.Update(model);
            //    ArrayList arrProduct = ProductRTCBO.Instance.FindByAttribute("ProductCode", grvData.GetFocusedRowCellValue(colProductCode).ToString());
            //    if (arrProduct != null && arrProduct.Count > 0)
            //    {
            //        ProductRTCModel pModel = arrProduct[0] as ProductRTCModel;
            //        pModel.NumberInStore += TextUtils.ToInt(grvData.GetFocusedRowCellValue(colNumberBorrow));
            //        ProductRTCBO.Instance.Update(pModel);
            //        _Forms frmProduct = (_Forms)Application.OpenForms["frmProductRTC"];
            //        _Forms frmMain = (_Forms)Application.OpenForms["frmMain"];
            //        if (frmProduct != null && frmMain != null)
            //        {
            //            frmProduct.Dispose();
            //            TextUtils.OpenChildForm(new frmProductRTC(), frmMain);
            //            TextUtils.OpenChildForm(new frmProductHistory(), frmMain);
            //        }
            //    }
            //}
            //else
            //{
            //    model.Status = 4;
            //    MessageBox.Show("Đăng ký trả thiết bị thành công! Đang chờ admin duyệt.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    HistoryProductRTCBO.Instance.Update(model);
            //}
            //// model.NumberBorrow = model.NumberBorrow - TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colNumberBorrow));
            //LoadData();
            //List<int> check = new List<int>();
            DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn trả sản phẩm", "Thông báo", MessageBoxButtons.YesNo);
            if (rs == DialogResult.No) return;
            Int32[] selectedRowHandles = grvData.GetSelectedRows();
            for (int i = 0; i < selectedRowHandles.Length; i++)
            {
                // check.Add(Lib.ToInt(grvData.GetFocusedRowCellValue(colID)));  
                int selectedRowHandle = selectedRowHandles[i];
                if (selectedRowHandle >= 0)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colID));
                    string ProductName = TextUtils.ToString(grvData.GetRowCellValue(selectedRowHandle, colProductName));
                    if (id == 0) return;
                    HistoryProductRTCModel model = (HistoryProductRTCModel)HistoryProductRTCBO.Instance.FindByPK(id);
                    if (model.Status != 1 && model.Status != 4 && model.Status != 7)
                    {
                    }
                    else
                    {
                        if (Global.LoginName.ToLower().Equals("admin"))
                        {
                            model.Status = 0;
                            model.DateReturn = DateTime.Now;
                            model.AdminConfirm = true;
                            HistoryProductRTCBO.Instance.Update(model);
                            ArrayList arrProduct = ProductRTCBO.Instance.FindByAttribute("ProductCode", grvData.GetRowCellValue(selectedRowHandle, colProductCode).ToString());
                            if (arrProduct != null && arrProduct.Count > 0)
                            {
                                ProductRTCModel pModel = arrProduct[0] as ProductRTCModel;
                                pModel.NumberInStore += TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colNumberBorrow));
                                ProductRTCBO.Instance.Update(pModel);
                                _Forms frmProduct = (_Forms)Application.OpenForms["frmProductRTC"];
                                _Forms frmMain = (_Forms)Application.OpenForms["frmMain"];
                                if (frmProduct != null && frmMain != null)
                                {
                                    frmProduct.Dispose();
                                    TextUtils.OpenChildForm(new frmProductRTC(warehouseID), frmMain);
                                    TextUtils.OpenChildForm(new frmProductHistory(warehouseID), frmMain);
                                }
                            }
                        }
                        else
                        {
                            model.Status = 4;
                            HistoryProductRTCBO.Instance.Update(model);
                        }
                        // model.NumberBorrow = model.NumberBorrow - TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colNumberBorrow));
                    }
                }

            }
            LoadData();
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
            DateTime _dateEnd = TextUtils.ToDate(dtpEndDate.Value.ToString());
            DateTime _dateBegin = TextUtils.ToDate(dtpFromDate.Value.ToString());
            if (checkDate())
            {
                if (_data == "")
                {
                    LoadData();
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt = TextUtils.LoadDataFromSP("spFindHistoryBorrow", "A", new string[] { "@Name", "@DateEnd", "@DateBegin", "@WarehouseID" }, new object[] { _data, _dateEnd, _dateBegin, warehouseID });
                    grdData.DataSource = dt;
                }
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
            // frmpHistoryExcel.Status = Status;
            frmpHistoryExcel.ShowDialog();
        }

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            // if (Status == 2 || Status == 3) return;


            //DateTime nowDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            //DateTime DateReturnExpected = TextUtils.ToDate3(TextUtils.ToDate3(grvData.GetRowCellValue(e.RowHandle, colDateReturmExpected)).ToString("yyyy/MM/dd"));
            //if (grvData.GetRowCellValue(e.RowHandle, colDate2) == System.DBNull.Value)
            //{
            //    if (nowDate.CompareTo(DateReturnExpected) == 0)
            //    {
            //        e.Appearance.BackColor = Color.FromArgb(255, 255, 74);

            //    }

            //    if (nowDate.CompareTo(DateReturnExpected) > 0)
            //    {
            //        e.Appearance.BackColor = Color.FromArgb(239, 31, 62);
            //        e.Appearance.ForeColor = Color.White;
            //    }
            //}
            //if (Lib.ToInt(grvData.GetRowCellValue(e.RowHandle, colStatus)) == 4)
            //{
            //    e.Appearance.BackColor = Color.Lime;
            //}

            int statusNew = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colStatusNew));
            int BillExportTechnicalID = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colBillExportTechnicalID)); //Update version 23/09/2022

            if (/*nowDate.CompareTo(DateReturnExpected) == 0*/statusNew == 6)
            {
                e.Appearance.BackColor = Color.FromArgb(255, 255, 74); // Sắp đến ngày
                e.Appearance.ForeColor = Color.Black;
            }
            else

            if (/*nowDate.CompareTo(DateReturnExpected) > 0*/statusNew == 5)
            {
                e.Appearance.BackColor = Color.FromArgb(239, 31, 62); // Quá hạn
                e.Appearance.ForeColor = Color.White;
            }

            //nếu phiếu mượn được tạo từ phiếu xuất thì sẽ có màu xanh da giời nhạt nhạt
            else if (BillExportTechnicalID > 0)
            {
                e.Appearance.BackColor = Color.FromArgb(128, 255, 255);
                e.Appearance.ForeColor = Color.Black;
            }
            else if (Lib.ToInt(grvData.GetRowCellValue(e.RowHandle, colStatus)) == 4)
            {
                e.Appearance.BackColor = Color.Lime;
                e.Appearance.ForeColor = Color.Black;
            }
            else if (TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colDualDate)) == 1)
            {
                e.Appearance.BackColor = Color.Yellow;
            }
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

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(view.GetFocusedRowCellValue(view.FocusedColumn)));
                e.Handled = true;
            }
        }

        private void cboStatus_EditValueChanged(object sender, EventArgs e)
        {
            var index = cboStatus.EditValue;
            Status = index.ToString();
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


    }
}

