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
using System.CodeDom.Compiler;
using Forms;
using QRCoder;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using DevExpress.XtraEditors.Controls;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections;

namespace BMS
{
    public partial class frmBillImportKT : _Forms
    {
        public frmBillImportKT()
        {
            InitializeComponent();
        }

        private void frmBillImport_Load(object sender, EventArgs e)
        {
            DateTime datenow = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            dtpFromDate.Value = datenow.AddMonths(-1);

            txtPageNumber.Text = "1";
            loadProductGroup();
            cbProductGroup.CheckAll();
            loadBillImport();
        }

        #region Methods
        /// <summary>
        /// load kho
        /// </summary>
        void loadProductGroup()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM ProductGroup");
            cbProductGroup.Properties.DisplayMember = "ProductGroupName";
            cbProductGroup.Properties.ValueMember = "ID";
            cbProductGroup.Properties.DataSource = dt;
        }

        /// <summary>
        /// load Master
        /// </summary>
        void loadBillImport()
        {
            DateTime dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);

            DataTable dt = new DataTable();
            dt = TextUtils.LoadDataFromSP("spGetBillImport_New", "A"
                , new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@Status", "@KhoType", "@FilterText" }
                , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value),
                   dateTimeS, dateTimeE, cbStatus.SelectedIndex, cbProductGroup.EditValue, txtFilterText.Text });
            grdMaster.DataSource = dt;

            if (dt.Rows.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(dt.Rows[0]["TotalPage"]);
        }

        /// <summary>
        /// load bảng Detail
        /// </summary>
        void loadBillImportDetail()
        {
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            DataTable dt = TextUtils.LoadDataFromSP("spGetBillImportDetail", "A", new string[] { "@ID" }, new object[] { ID });
            grdData.DataSource = dt;
        }
        #endregion

        /// <summary>
        /// hàm focus đến bảng detail
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadBillImportDetail();
        }

        /// <summary>
        /// Double Click mở ra form sửa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdMaster_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        #region Buttons Events
        /// <summary>
        /// click button tạo phiếu nhập
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            frmBillImportDetail frm = new frmBillImportDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadBillImport();
                grvMaster_FocusedRowChanged(null, null);
            }
        }

        /// <summary>
        /// click button để sửa phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvMaster.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (ID == 0) return;
            BillImportModel model = (BillImportModel)BillImportBO.Instance.FindByPK(ID);
            frmBillImportDetail frm = new frmBillImportDetail();
            frm.billImport = model;
            frm.IDDetail = ID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadBillImport();

                grvMaster.FocusedRowHandle = focusedRowHandle;
                grvMaster_FocusedRowChanged(null, null);
            }

        }

        /// <summary>
        /// click button để xóa phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvMaster.FocusedRowHandle;
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colStatus));
            string billCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colBillCode));
            if (isApproved == true)
            {
                MessageBox.Show(String.Format("Bạn không thể xóa phiếu nhập [{0}]? Xin vui lòng kiểm tra lại.", billCode), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Question);
                return;
            }
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (ID == 0) return;
            int strID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue("ID"));
            if (MessageBox.Show(string.Format("Bạn có muốn xóa phiếu nhập [{0}] hay không ?", billCode), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                BillImportBO.Instance.Delete(strID);
                BillImportDetailBO.Instance.DeleteByAttribute("BillImportID", strID);
                grvMaster.DeleteSelectedRows();
                grvMaster.FocusedRowHandle = focusedRowHandle;
                grvMaster_FocusedRowChanged(null, null);
            }
            //thêm lịch sử người xóa phiếu
            TextUtils.ExcuteSQL($"Insert into HistoryDeleteBill(BillID,UserID,DeleteDate,Name,TypeBill) values ({ID},{Global.UserID},'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{Global.AppUserName}','{billCode}') ");
        }

        /// <summary>
        /// click button duyệt phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIsApproved_Click(object sender, EventArgs e)
        {
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colStatus));
            if (isApproved == true)
            {
                string billCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colBillCode));
                MessageBox.Show(String.Format("Phiếu nhập [{0}] đã được duyệt. Xin vui lòng kiểm tra lại!", billCode), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Question);
                return;
            }
            approved(true);
        }

        /// <summary>
        /// click buttoon hủy duyệt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelApproved_Click(object sender, EventArgs e)
        {
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colStatus));
            if (isApproved == false)
            {
                string billCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colBillCode));
                MessageBox.Show(String.Format("Phiếu nhập [{0}] chưa được duyệt.", billCode), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Question);
                return;
            }
            approved(false);
        }

        /// <summary>
        /// click button xuất excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcel_Click(object sender, EventArgs e)
        {
            bool IsApprove = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colStatus));
            if (!IsApprove)
            {
                MessageBox.Show("Phiếu phải được duyệt trước khi in. Vui lòng kiểm tra lại trạng thái ! ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (ID == 0) return;
            string path = "";
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                path = fbd.SelectedPath;
            }
            else
            {
                return;
            }
            string fileSourceName = "BillImportSale.xlsx";

            string sourcePath = Application.StartupPath + "\\" + fileSourceName;
            string billCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colBillCode));
            string currentPath = path + "\\" + billCode + DateTime.Now.ToString("_dd_MM_yyyy_HH_mm_ss") + ".xlsx";
            try
            {
                File.Copy(sourcePath, currentPath, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi tạo báo giá!" + Environment.NewLine + ex.Message,
                    TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            DataTable dt = new DataTable();
            dt = TextUtils.LoadDataFromSP("spGetBillImportDetail", "A", new string[] { "@ID" }, new object[] { ID });


            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                Excel.Application app = default(Excel.Application);
                Excel.Workbook workBoook = default(Excel.Workbook);
                Excel.Worksheet workSheet = default(Excel.Worksheet);
                try
                {
                    DateTime dtime = TextUtils.ToDate3(dt.Rows[0]["CreatDate"]);
                    string date = $"Ngày {dtime.Day} Tháng {dtime.Month} Năm {dtime.Year}";
                    app = new Excel.Application();
                    app.Workbooks.Open(currentPath);
                    workBoook = app.Workbooks[1];
                    workSheet = (Excel.Worksheet)workBoook.Worksheets[1];
                    workSheet.Cells[9, 4] = TextUtils.ToString(dt.Rows[0]["NameNCC"]);
                    workSheet.Cells[6, 1] = billCode;
                    workSheet.Cells[8, 4] = TextUtils.ToString(dt.Rows[0]["Deliver"]);
                    workSheet.Cells[22, 7] = TextUtils.ToString(dt.Rows[0]["Reciver"]);
                    workSheet.Cells[15, 7] = date;

                    for (int i = dt.Rows.Count - 1; i >= 0; i--)
                    {
                        workSheet.Cells[13, 1] = i + 1;
                        workSheet.Cells[13, 2] = TextUtils.ToString(dt.Rows[i]["ProductNewCode"]);
                        workSheet.Cells[13, 3] = TextUtils.ToString(dt.Rows[i]["ProductCode"]);
                        workSheet.Cells[13, 4] = TextUtils.ToString(dt.Rows[i]["ProductName"]);
                        workSheet.Cells[13, 5] = TextUtils.ToString(dt.Rows[i]["Unit"]);
                        workSheet.Cells[13, 6] = TextUtils.ToDecimal(dt.Rows[i]["Qty"]);
                        workSheet.Cells[13, 7] = TextUtils.ToString(dt.Rows[i]["SomeBill"]);
                        workSheet.Cells[13, 8] = TextUtils.ToString(dt.Rows[i]["ProductGroupName"]);
                        workSheet.Cells[13, 9] = TextUtils.ToString(dt.Rows[i]["ProjectNameText"]);
                        workSheet.Cells[13, 10] = TextUtils.ToString(dt.Rows[i]["Note"]);
                        ((Excel.Range)workSheet.Rows[13]).Insert();
                    }
                    ((Excel.Range)workSheet.Rows[12]).Delete();
                    ((Excel.Range)workSheet.Rows[12]).Delete();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (app != null)
                    {
                        app.ActiveWorkbook.Save();
                        app.Workbooks.Close();
                        app.Quit();
                    }
                }
                Process.Start(currentPath);
            }
        }
        #endregion

        /// <summary>
        /// hàm duyệt
        /// </summary>
        /// <param name="isApproved"></param>
        void approved(bool isApproved)
        {
            if (MessageBox.Show(string.Format("Bạn có chắc muốn {0} duyệt phiếu này?", isApproved ? "" : "bỏ"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
                int unapprove = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colUnApprove));
                if (Global.IsAdmin)
                {

                }
                else if (unapprove == 2 && Global.UserID != 1135)
                {
                    MessageBox.Show("Bạn Không có quyền duyệt phiếu này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;

                }
                string sql = string.Format("UPDATE dbo.BillImport SET Status = {0},UnApprove = {2}  WHERE ID = {1}", isApproved ? 1 : 0, ID, isApproved ? 1 : 2);
                TextUtils.ExcuteSQL(sql);
                calculateImport();
                if (isApproved == true)
                {
                    grvMaster.SetFocusedRowCellValue(colStatus, 1);
                    grvMaster.SetFocusedRowCellValue(colUnApprove, 1);
                }
                else
                {
                    grvMaster.SetFocusedRowCellValue(colStatus, 0);
                    grvMaster.SetFocusedRowCellValue(colUnApprove, 2);
                }

            }
        }

        /// <summary>
        /// hàm tính số lượng khi duyệt , hủy duyệt
        /// </summary>
        void calculateImport()
        {
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            TextUtils.ExcuteProcedure("spCalculateImport", new string[] { "@ID" }, new object[] { ID });
        }

        /// <summary>
        /// click button tìm kiếm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFind_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grvMaster.FocusedRowHandle;
            grvMaster.FocusedRowHandle = focusedRowHandle - 1;
            loadBillImport();

        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadBillImport();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadBillImport();
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadBillImport();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            loadBillImport();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadBillImport();
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvMaster.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (ID == 0) return;
            BillImportModel model = (BillImportModel)BillImportBO.Instance.FindByPK(ID);
            frmBillExportDetailNew frm = new frmBillExportDetailNew(false);
            frm.billImport = model;
            frm.IDDetail = ID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadBillImport();

                grvMaster.FocusedRowHandle = focusedRowHandle;
                grvMaster_FocusedRowChanged(null, null);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            int IDMaster = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (IDMaster == 0) return;

            string path = "";
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                path = fbd.SelectedPath;
            }
            else
            {
                return;
            }
            string fileSourceName = "FormNhapKho.xlsx";
            string sourcePath = Application.StartupPath + "\\" + fileSourceName;
            string phieucode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colBillCode));
            string currentPath = path + "\\" + phieucode + DateTime.Now.ToString("_dd_MM_yyyy_HH_mm_ss") + ".xls";
            try
            {
                File.Copy(sourcePath, currentPath, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi tạo phiếu!" + Environment.NewLine + ex.Message,
                    TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            DataTable dtMaster = TextUtils.LoadDataFromSP("spGetBillImportDetail", "A", new string[] { "@ID" }, new object[] { IDMaster });

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                Excel.Application app = default(Excel.Application);
                Excel.Workbook workBoook = default(Excel.Workbook);
                Excel.Worksheet workSheet = default(Excel.Worksheet);
                try
                {
                    app = new Excel.Application();
                    app.Workbooks.Open(currentPath);
                    workBoook = app.Workbooks[1];
                    workSheet = (Excel.Worksheet)workBoook.Worksheets[1];

                    for (int i = dtMaster.Rows.Count - 1; i >= 0; i--)
                    {
                        workSheet.Cells[15, 1] = i + 1;

                        workSheet.Cells[3, 6] = TextUtils.ToString(dtMaster.Rows[i]["CodeNCC"]);
                        workSheet.Cells[3, 7] = TextUtils.ToString(dtMaster.Rows[i]["NameNCC"]);
                        workSheet.Cells[3, 9] = TextUtils.ToString(dtMaster.Rows[i]["Note"]);
                        workSheet.Cells[3, 13] = TextUtils.ToString(dtMaster.Rows[i]["FullName"]);
                        workSheet.Cells[3, 12] = TextUtils.ToString(dtMaster.Rows[i]["ProductCode"]);
                        workSheet.Cells[3, 13] = TextUtils.ToString(dtMaster.Rows[i]["ProductName"]);
                        workSheet.Cells[3, 14] = TextUtils.ToString(dtMaster.Rows[i]["ProductGroupName"]);
                        workSheet.Cells[3, 18] = TextUtils.ToString(dtMaster.Rows[i]["Unit"]);
                        workSheet.Cells[3, 19] = TextUtils.ToString(dtMaster.Rows[i]["Qty"]);
                        workSheet.Cells[3, 3] = TextUtils.ToDate2(dtMaster.Rows[i]["CreatDate"]);
                        ((Excel.Range)workSheet.Rows[3]).Insert();
                    }
                    ((Excel.Range)workSheet.Rows[2]).Delete();
                    ((Excel.Range)workSheet.Rows[2]).Delete();
                    ((Excel.Range)workSheet.Columns).AutoFit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (app != null)
                    {
                        app.ActiveWorkbook.Save();
                        app.Workbooks.Close();
                        app.Quit();
                    }
                }
                Process.Start(currentPath);
            }
        }

        private void grdMaster_Click(object sender, EventArgs e)
        {

        }
    }
}


