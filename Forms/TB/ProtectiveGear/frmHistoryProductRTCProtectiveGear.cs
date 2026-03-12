using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using Forms.Classes;
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
    public partial class frmHistoryProductRTCProtectiveGear : _Forms
    {
        public int warehouseID = 5;
        public int Status = -1;
        public List<DataRow> listRow = new List<DataRow>();

        //lee min khooi update 11/10/2024
        List<int> hcnsIDs = SQLHelper<vUserGroupLinkModel>.FindByAttribute("Code", "N34").Select(p => p.UserID).ToList();
        bool isAdmin = false;

        public frmHistoryProductRTCProtectiveGear()
        {
            InitializeComponent();
        }

        private void frmHistoryProductRTCProtectiveGear_Load(object sender, EventArgs e)
        {
            isAdmin = hcnsIDs.Contains(Global.UserID) || Global.IsAdmin;
            this.Text += $" - {this.Tag}";
            DateTime date = DateTime.Now.AddMonths(-1);
            dtpFromDate.Value = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            cboStatus.Properties.Items[1].CheckState = CheckState.Checked;
            cboIsDeleted.SelectedIndex = 1;

            LoadEmployee();
            LoadData();

            btnApprovedReturn.Enabled = btnDuyenMuon.Enabled = btnEdit.Enabled = btnDelete.Enabled = btnApprovedGiaHan.Enabled = isAdmin;

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
            DateTime ds = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0).AddSeconds(-1);
            DateTime de = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59).AddSeconds(+1);

            string status = TextUtils.ToString(cboStatus.EditValue);

            int pageNumber = TextUtils.ToInt(txtPageNumber.Text);
            int pageSize = TextUtils.ToInt(txtPageSize.Text);
            int userID = TextUtils.ToInt(cboEmployee.EditValue);
            int isDeleted = cboIsDeleted.SelectedIndex - 1;
            using (WaitDialogForm fWait = new WaitDialogForm())
            {
                DataTable dtHistoryProduct = TextUtils.LoadDataFromSP("spGetHistoryProduct_New", "A",
                    new string[] { "@DateStart", "@DateEnd", "@Keyword", "@Status", "@WarehouseID", "@PageNumber", "@PageSize", "@UserID", "@IsDeleted" },
                    new object[] { ds, de, txtFilterText.Text.Trim(), status, warehouseID, pageNumber, pageSize, userID, isDeleted });
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
                if (dtHistoryProduct.Rows.Count > 0)
                {
                    txtTotalPage.Text = (dtHistoryProduct.Rows[0]["TotalPage"]).ToString();
                }
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();

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

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = TextUtils.ToInt(txtPageNumber.Text) > 1 ? (TextUtils.ToInt(txtPageNumber.Text) - 1).ToString() : "1";
            LoadData();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) > TextUtils.ToInt(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            LoadData();
        }

        private void cboStatus_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
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

            //        //Update lịch sử gia hạn
            //        HistoryProductRTCLogModel logModel = new HistoryProductRTCLogModel();
            //        logModel.HistoryProductRTCID = id;
            //        logModel.DateReturnExpected = TextUtils.ToDate4(grvData.GetRowCellValue(selectedRowHandle, colDateReturmExpected));
            //        SQLHelper<HistoryProductRTCLogModel>.Insert(logModel);
            //    }
            //}
            //LoadData();


            try
            {
                DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn trả sản phẩm", "Thông báo", MessageBoxButtons.YesNo);
                if (rs == DialogResult.No) return;
                Int32[] selectedRowHandles = grvData.GetSelectedRows();
                for (int i = 0; i < selectedRowHandles.Length; i++)
                {
                    int selectedRowHandle = selectedRowHandles[i];
                    int BillExportTechnicalID = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colBillExportTechnicalID));
                    if (selectedRowHandle >= 0)
                    {
                        int id = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colID));
                        if (id == 0) continue;
                        HistoryProductRTCModel model = (HistoryProductRTCModel)HistoryProductRTCBO.Instance.FindByPK(id);
                        //bool isValid = 
                        if (model.Status == 0) continue;
                        model.DateReturnExpected = dtpNgayGiaHan.Value;
                        if (isAdmin)
                        {
                            model.Status = 1;

                            model.AdminConfirm = true;
                            //HistoryProductRTCBO.Instance.Update(model);
                            SQLHelper<HistoryProductRTCModel>.Update(model);
                            //int IDProduct = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colProductRTCID));
                            //TextUtils.ExcuteProcedure(StoreProcedures.spUpdateStatusProductRTCQRCode, new string[] { "@ProductRTCQRCodeID", "@Status" }, new object[] { model.ProductRTCQRCodeID, 1 });
                        }
                        else
                        {
                            model.Status = 8;
                            //HistoryProductRTCBO.Instance.Update(model);
                            SQLHelper<HistoryProductRTCModel>.Update(model);
                        }

                        //Update lịch sử gia hạn
                        HistoryProductRTCLogModel logModel = new HistoryProductRTCLogModel();
                        logModel.HistoryProductRTCID = id;
                        logModel.DateReturnExpected = TextUtils.ToDate4(grvData.GetRowCellValue(selectedRowHandle, colDateReturmExpected));
                        SQLHelper<HistoryProductRTCLogModel>.Insert(logModel);
                    }
                }
                LoadData();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, TextUtils.Caption);
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

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            //GridView view = sender as GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn));
                if (string.IsNullOrWhiteSpace(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void grvData_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column != colPeople)
            {
                return;
            }

            if (Status == 2 || Status == 3) return;

            int statusNew = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colStatusNew));
            int BillExportTechnicalID = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colBillExportTechnicalID));

            if (statusNew == 6)
            {
                e.Appearance.BackColor = Color.FromArgb(255, 255, 74); // Sắp đến ngày
                e.Appearance.ForeColor = Color.Black;
            }

            if (statusNew == 5)
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

        private void btnBorrow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmHistoryProductRTCDetailProtectiveGear frm = new frmHistoryProductRTCDetailProtectiveGear();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnReturn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //int userId = Global.UserID;
            //bool isAdmin = Global.IDAdminDemo.Contains(userId);
            //try
            //{
            //    DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn trả sản phẩm", "Thông báo", MessageBoxButtons.YesNo);
            //    if (rs == DialogResult.No) return;
            //    Int32[] selectedRowHandles = grvData.GetSelectedRows();
            //    for (int i = 0; i < selectedRowHandles.Length; i++)
            //    {
            //        int selectedRowHandle = selectedRowHandles[i];

            //        int BillExportTechnicalID = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colBillExportTechnicalID));

            //        if (selectedRowHandle >= 0)
            //        {
            //            int id = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colID));
            //            if (id == 0) return;
            //            HistoryProductRTCModel model = (HistoryProductRTCModel)HistoryProductRTCBO.Instance.FindByPK(id);
            //            if (model.Status != 1 && model.Status != 4 && model.Status != 7)
            //            {
            //            }
            //            else
            //            {
            //                if (Global.IsAdmin || isAdmin)
            //                {
            //                    model.Status = 0;
            //                    model.DateReturn = DateTime.Now;
            //                    model.AdminConfirm = true;
            //                    HistoryProductRTCBO.Instance.Update(model);
            //                    int IDProduct = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colProductRTCID));
            //                    TextUtils.ExcuteProcedure(StoreProcedures.spUpdateStatusProductRTCQRCode, new string[] { "@ProductRTCQRCodeID", "@Status" }, new object[] { model.ProductRTCQRCodeID, 1 });
            //                }
            //                else
            //                {
            //                    MessageBox.Show("Tài khoản Admin mới có thể sử dụng !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                    model.Status = 4;
            //                    HistoryProductRTCBO.Instance.Update(model);
            //                }
            //            }
            //        }
            //    }
            //    LoadData();
            //}
            //catch (Exception err)
            //{
            //    MessageBox.Show(err.Message, TextUtils.Caption);
            //}

            // ================================================================= lee min khooi update 11/10/2024 =================================================================
            try
            {
                DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn trả sản phẩm", "Thông báo", MessageBoxButtons.YesNo);
                if (rs == DialogResult.No) return;
                Int32[] selectedRowHandles = grvData.GetSelectedRows();
                for (int i = 0; i < selectedRowHandles.Length; i++)
                {
                    int selectedRowHandle = selectedRowHandles[i];
                    int BillExportTechnicalID = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colBillExportTechnicalID));
                    if (selectedRowHandle >= 0)
                    {
                        int id = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colID));
                        if (id == 0) continue;
                        HistoryProductRTCModel model = (HistoryProductRTCModel)HistoryProductRTCBO.Instance.FindByPK(id);
                        bool isValid = model.Status != 1 && model.Status != 4 && model.Status != 7;
                        if (isValid) continue;

                        if (isAdmin)
                        {
                            model.Status = 0;
                            model.DateReturn = DateTime.Now;
                            model.AdminConfirm = true;
                            HistoryProductRTCBO.Instance.Update(model);
                            int IDProduct = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colProductRTCID));
                            TextUtils.ExcuteProcedure(StoreProcedures.spUpdateStatusProductRTCQRCode, new string[] { "@ProductRTCQRCodeID", "@Status" }, new object[] { model.ProductRTCQRCodeID, 1 });
                        }
                        else
                        {
                            model.Status = 4;
                            HistoryProductRTCBO.Instance.Update(model);
                        }
                    }
                }
                LoadData();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, TextUtils.Caption);
            }

        }

        private void btnDuyenMuon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Int32[] selectedRowHandles = grvData.GetSelectedRows();
            if (selectedRowHandles.Length <= 0) return;
            for (int i = 0; i < selectedRowHandles.Length; i++)
            {
                int selectedRowHandle = selectedRowHandles[i];
                int productHistoryID = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colID));
                int Status = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colStatusNew));

                HistoryProductRTCModel historyProductRTCModel = SQLHelper<HistoryProductRTCModel>.FindByID(productHistoryID);
                if (historyProductRTCModel.ID <= 0) return;
                if (Status == 7)
                {
                    historyProductRTCModel.Status = 1;
                    //historyProductRTCModel.DateReturnExpected = new DateTime(2024,10,31);
                    //historyProductRTCModel.DateReturn = null;
                }
                historyProductRTCModel.IsDelete = false;
                SQLHelper<HistoryProductRTCModel>.Update(historyProductRTCModel);
            }
            LoadData();
        }

        private void btnLoad_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadData();
        }

        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<HistoryProductRTCModel> list = new List<HistoryProductRTCModel>();
            Int32[] selectedRowHandles = grvData.GetSelectedRows();
            if (selectedRowHandles.Length <= 0) return;
            for (int i = 0; i < selectedRowHandles.Length; i++)
            {
                int selectedRowHandle = selectedRowHandles[i];
                if (selectedRowHandle >= 0)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colID));
                    string productCode = TextUtils.ToString(grvData.GetRowCellValue(selectedRowHandle, colProductCode));
                    DateTime? dateReturn = TextUtils.ToDate4(grvData.GetRowCellValue(selectedRowHandle, colDate2));
                    if (id == 0 || dateReturn.HasValue)
                    {
                        MessageBox.Show($"Sản phẩm [{productCode}] đã trả! Không thể sửa người mượn!", "Thông báo");
                        return;
                    };
                    HistoryProductRTCModel model = (HistoryProductRTCModel)HistoryProductRTCBO.Instance.FindByPK(id);
                    list.Add(model);
                }
            }
            if (list.Count <= 0) return;
            frmEditPerson frm = new frmEditPerson();
            frm.list = list;
            frm.ProductName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductName));
            frm.ProductCode = TextUtils.ToString(grvData.GetFocusedRowCellValue(colProductCode));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnProductHistoryLate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmProductHistoryLate frm = new frmProductHistoryLate(1);
            frm.Tag = "HN";
            frm.ShowDialog();
        }

        private void btnExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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



        // ================================================================= lee min khooi update 11/10/2024 =================================================================
        private void btnApprovedReturn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn duyệt trả sản phẩm không?", "Thông báo", MessageBoxButtons.YesNo);
                if (rs == DialogResult.No) return;
                int[] selectedRowHandles = grvData.GetSelectedRows();
                for (int i = 0; i < selectedRowHandles.Length; i++)
                {
                    int selectedRowHandle = selectedRowHandles[i];
                    int BillExportTechnicalID = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colBillExportTechnicalID));
                    if (selectedRowHandle >= 0)
                    {
                        int id = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colID));
                        if (id == 0) continue;
                        HistoryProductRTCModel model = (HistoryProductRTCModel)HistoryProductRTCBO.Instance.FindByPK(id);
                        bool isValid = model.Status != 1 && model.Status != 4 && model.Status != 7;
                        if (isValid) continue;
                        if (Global.IsAdmin || isAdmin)
                        {
                            model.Status = 0;
                            model.DateReturn = DateTime.Now;
                            model.AdminConfirm = true;
                            HistoryProductRTCBO.Instance.Update(model);
                            int IDProduct = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandle, colProductRTCID));
                            TextUtils.ExcuteProcedure(StoreProcedures.spUpdateStatusProductRTCQRCode, new string[] { "@ProductRTCQRCodeID", "@Status" }, new object[] { model.ProductRTCQRCodeID, 1 });
                        }
                    }
                }
                LoadData();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, TextUtils.Caption);
            }
        }

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (!isAdmin) return;
                int[] selectedRowHandles = grvData.GetSelectedRows();
                if (selectedRowHandles.Length <= 0)
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm muốn xóa!", "Thông báo");
                    return;
                }
                DialogResult dialog = MessageBox.Show("Bạn có chắc chắn muốn xóa các phiếu mượn không?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    List<int> lstIDs = new List<int>();

                    for (int i = 0; i < selectedRowHandles.Length; i++)
                    {
                        int id = TextUtils.ToInt(grvData.GetRowCellValue(selectedRowHandles[i], colID));
                        if (id == 0) continue;
                        if (!lstIDs.Contains(id)) lstIDs.Add(id);
                    }

                    string idText = string.Join(",", lstIDs);
                    Dictionary<string, object> newDict = new Dictionary<string, object>()
                    {
                        {"IsDelete", 1},
                        {"UpdatedBy", Global.AppUserName},
                        {"UpdatedDate", DateTime.Now}
                    };
                    Expression ex1 = new Expression("ID", idText, "IN");
                    SQLHelper<HistoryProductRTCModel>.UpdateFields(newDict, ex1);
                    LoadData();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + err.Message);
            }

        }

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            bool isDeleted = TextUtils.ToBoolean(grvData.GetRowCellValue(e.RowHandle, colIsDelete));
            if (isDeleted)
            {
                e.Appearance.BackColor = Color.Red;
                e.Appearance.ForeColor = Color.White;
                e.HighPriority = true;
            }
            else
            {
                var view = sender as GridView;
                if (view.FocusedRowHandle == e.RowHandle)
                {
                    e.Appearance.BackColor = Color.LightYellow;
                    //e.HighPriority = true;
                }
            }
        }

        private void btnApprovedGiaHan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnDuyenMuon_ItemClick(null, null);
        }
    }
}
