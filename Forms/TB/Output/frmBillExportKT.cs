using BMS.Business;
using BMS.Model;
using DevExpress.Utils;
using Forms.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace BMS
{
    public partial class frmBillExportKT : _Forms
    {
        public frmBillExportKT()
        {
            InitializeComponent();
        }

        private void frmBillExport_Load(object sender, EventArgs e)
        {
            DateTime datenow = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            dtpFromDate.Value = datenow.AddMonths(-1);

            txtPageNumber.Text = "1";
            loadProductGroup();
            cbProductGroup.CheckAll();
            loadBillExport();

        }

        #region Methods
        /// <summary>
        /// load nhóm
        /// </summary>
        void loadProductGroup()
        {
            DataTable dt = TextUtils.Select("SELECT * FROM ProductGroup");
            cbProductGroup.Properties.DisplayMember = "ProductGroupName";
            cbProductGroup.Properties.ValueMember = "ID";
            cbProductGroup.Properties.DataSource = dt;
        }

        /// <summary>
        /// load bảng Master
        /// </summary>
        private void loadBillExport()
        {
            DateTime dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);

            DataTable dt = new DataTable();
            dt = TextUtils.LoadDataFromSP("spGetBillExport_New", "A"
                , new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@Status", "@KhoType", "@FilterText" }
                , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value),
                   dateTimeS, dateTimeE,cbStatus.SelectedIndex,cbProductGroup.EditValue, txtFilterText.Text });
            grdMaster.DataSource = dt;

            if (dt.Rows.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(dt.Rows[0]["TotalPage"]);
        }

        /// <summary>
        /// load bảng Detail
        /// </summary>
        void loadBillExportDetail()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetBillExportDetail", "A", new string[] { "@BillID" }, new object[] { TextUtils.ToInt64(grvMaster.GetFocusedRowCellValue(colIDMaster)) });
            grdData.DataSource = dt;
        }
        #endregion

        #region Buttons Events
        /// <summary>
        /// doubleclick vào phiếu để hiển thị thông tin phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdMaster_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        /// <summary>
        /// click button để tạo phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            frmBillExportDetailNew frm = new frmBillExportDetailNew(false);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadBillExport();
                grvMaster_FocusedRowChanged(null, null);
            }
        }

        /// <summary>
        /// click button sửa phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvMaster.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (ID == 0) return;
            BillExportModel model = (BillExportModel)BillExportBO.Instance.FindByPK(ID);
            frmBillExportDetailNew frm = new frmBillExportDetailNew(false);
            frm.billExport = model;
            frm.IDDetail = ID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadBillExport();
                grvMaster.FocusedRowHandle = focusedRowHandle;
                grvMaster_FocusedRowChanged(null, null);
            }
        }

        /// <summary>
        /// click button xóa phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvMaster.FocusedRowHandle;
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproved));
            string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCode));
            if (isApproved == true)
            {
                MessageBox.Show(String.Format("Bạn không thể xóa phiếu xuất [{0}] này?", code), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Question);
                return;
            }

            if (!grvMaster.IsDataRow(grvMaster.FocusedRowHandle)) return;

            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue("ID"));
            if (ID == 0) return;

            if (MessageBox.Show(String.Format("Bạn có chắc muốn xóa phiếu xuất [{0}] không?", code), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                BillExportBO.Instance.Delete(ID);
                BillExportDetailBO.Instance.DeleteByAttribute("BillID", ID);
                grvMaster.DeleteSelectedRows();
                grvMaster.FocusedRowHandle = focusedRowHandle;
                grvMaster_FocusedRowChanged(null, null);
            }
            //thêm lịch sử người xóa phiếu
            TextUtils.ExcuteSQL($"Insert into HistoryDeleteBill(BillID,UserID,DeleteDate,Name,TypeBill) values ({ID},{Global.UserID},'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{Global.AppUserName}','{code}') ");

        }

        /// <summary>
        /// click button duyệt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIsApproved_Click(object sender, EventArgs e)
        {
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproved));
            if (isApproved == true)
            {
                string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCode));
                MessageBox.Show(String.Format("Phiếu xuất [{0}] đã được duyệt.", code), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Question);
                return;
            }
            approved(true);
        }

        /// <summary>
        /// click button hủy duyệt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelApproved_Click(object sender, EventArgs e)
        {
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproved));
            if (isApproved == false)
            {
                string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCode));
                MessageBox.Show(String.Format("Phiếu xuất [{0}] chưa được duyệt.", code), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Question);
                return;
            }
            approved(false);
        }

        /// <summary>
        /// click để xuất ra excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcel_Click(object sender, EventArgs e)
        {

            bool IsApprove = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproved));
            if (!IsApprove)
            {
                MessageBox.Show("Phiếu phải được duyệt trước khi in. Vui lòng kiểm tra lại trạng thái ! ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
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
            string fileSourceName = "PhieuXuatSALE.xls";

            string sourcePath = Application.StartupPath + "\\" + fileSourceName;
            string phieucode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCode));
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

            DataTable dtMaster = TextUtils.LoadDataFromSP("spGetExportExcel", "A", new string[] { "@ID" }, new object[] { IDMaster });

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

                    string totalName = TextUtils.ToString(dtMaster.Rows[0]["Code"]);
                    workSheet.Cells[6, 1] = "Số: " + totalName;
                    workSheet.Cells[9, 4] = TextUtils.ToString(dtMaster.Rows[0]["FullName"]);
                    workSheet.Cells[10, 4] = TextUtils.ToString(dtMaster.Rows[0]["CustomerName"]);
                    workSheet.Cells[11, 4] = TextUtils.ToString(dtMaster.Rows[0]["Address"]);
                    workSheet.Cells[12, 4] = TextUtils.ToString(dtMaster.Rows[0]["AddressStock"]);
                    workSheet.Cells[25, 3] = TextUtils.ToString(dtMaster.Rows[0]["FullNameSender"]);

                    string creatDate1 = TextUtils.ToDate3(dtMaster.Rows[0]["CreatDate"]).ToString("dd");
                    string creatDate2 = TextUtils.ToDate3(dtMaster.Rows[0]["CreatDate"]).ToString("MM");
                    string creatDate3 = TextUtils.ToDate3(dtMaster.Rows[0]["CreatDate"]).ToString("yyyy");
                    workSheet.Cells[18, 8] = "Ngày " + creatDate1 + " tháng " + creatDate2 + " năm " + creatDate3;

                    for (int i = dtMaster.Rows.Count - 1; i >= 0; i--)
                    {
                        workSheet.Cells[16, 1] = i + 1;
                        workSheet.Cells[16, 2] = TextUtils.ToString(dtMaster.Rows[i]["ProductNewCode"]);
                        workSheet.Cells[16, 3] = TextUtils.ToString(dtMaster.Rows[i]["ProductCode"]);
                        workSheet.Cells[16, 4] = TextUtils.ToString(dtMaster.Rows[i]["ProductFullName"]);
                        workSheet.Cells[16, 5] = TextUtils.ToString(dtMaster.Rows[i]["ProductName"]);
                        workSheet.Cells[16, 6] = TextUtils.ToString(dtMaster.Rows[i]["Unit"]);
                        workSheet.Cells[16, 7] = TextUtils.ToDecimal(dtMaster.Rows[i]["Qty"]);
                        workSheet.Cells[16, 8] = TextUtils.ToString(dtMaster.Rows[i]["ProjectNameText"]);
                        workSheet.Cells[16, 9] = TextUtils.ToString(dtMaster.Rows[i]["ProductTypeText"]);
                        workSheet.Cells[16, 10] = TextUtils.ToString(dtMaster.Rows[i]["ProductGroupName"]);
                        workSheet.Cells[16, 11] = TextUtils.ToString(dtMaster.Rows[i]["Note"]);
                        ((Excel.Range)workSheet.Rows[16]).Insert();
                    }
                    ((Excel.Range)workSheet.Rows[15]).Delete();
                    ((Excel.Range)workSheet.Rows[15]).Delete();
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

        /// <summary>
        /// click button để thay đổi trạng thái phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShippedOut_Click(object sender, EventArgs e)
        {
            string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCode));
            BillExportModel _BillExport = new BillExportModel();
            int status = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colStatus));
            if (status == (int)EStatus.Borrow || status == (int)EStatus.Inventory)
            {
                int statuscode = 2;
                if (MessageBox.Show(string.Format("Bạn có muốn thay đổi trạng thái phiếu xuất {0}?", code), TextUtils.Caption, MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.No) return;

                int IDMaster = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
                string sql = string.Format(@"UPDATE dbo.BillExport SET Status = '{0}' WHERE ID = {1}", statuscode, IDMaster);

                TextUtils.ExcuteScalar(sql);
                loadBillExport();
            }
            else
            {
                MessageBox.Show(string.Format("Xin vui lòng kiểm tra lại trạng thái sản phẩm"));
            }
        }
        #endregion

        /// <summary>
        /// hàm duyệt
        /// </summary>
        /// <param name="isApproved"></param>
        void approved(bool isApproved)
        {
            int status = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colStatus));
            //if(status ==3)
            //{
            //    MessageBox.Show(string.Format("Phiếu này không được duyệt, kiểm tra lại trạng thái", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error));
            //    return;
            //}    
            if (MessageBox.Show(string.Format("Bạn có chắc muốn {0} duyệt phiếu này?", isApproved ? "" : "bỏ"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int iD = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
                int unapprove = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colUnApprove));
                if (Global.IsAdmin)
                {

                }
                else if (unapprove == 2 && Global.UserID != 1135)
                {
                    MessageBox.Show("Bạn Không có quyền duyệt phiếu này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;

                }
                string sql = string.Format(@"UPDATE dbo.BillExport SET IsApproved = {0}, UnApprove ={2} WHERE ID = {1}", isApproved ? 1 : 0, iD, isApproved ? 1 : 2);
                TextUtils.ExcuteSQL(sql);
                calculateExport();
                calculatePOKH();
                if (isApproved == true)
                {
                    grvMaster.SetFocusedRowCellValue(colIsApproved, 1);
                    grvMaster.SetFocusedRowCellValue(colUnApprove, 1);
                }
                else
                {
                    grvMaster.SetFocusedRowCellValue(colIsApproved, 0);
                    grvMaster.SetFocusedRowCellValue(colUnApprove, 2);
                }

            }
        }

        /// <summary>
        /// tính toán số lượng duyệt
        /// </summary>
        void calculateExport()
        {
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            TextUtils.ExcuteProcedure("spCalculateExport", new string[] { "@ID" }, new object[] { ID });
        }

        /// <summary>
        /// hàm tính số lượng thực tế , còn lại khi chọn từ POKH
        /// </summary>
        private void calculatePOKH()
        {
            int IDMaster = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            TextUtils.ExcuteProcedure("spCalculatePOKH", new string[] { "@IDMaster" }, new object[] { IDMaster });
        }

        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadBillExportDetail();
        }

        /// <summary>
        /// click button tìm kiếm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFind_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grvMaster.FocusedRowHandle;
            loadBillExport();
            grvMaster.FocusedRowHandle = focusedRowHandle - 1;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadBillExport();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadBillExport();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadBillExport();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadBillExport();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            loadBillExport();
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadBillExport();
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
            string fileSourceName = "FormXuatKho.xlsx";
            string sourcePath = Application.StartupPath + "\\" + fileSourceName;
            string phieucode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCode));
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

            DataTable dtMaster = TextUtils.LoadDataFromSP("spGetExportExcel", "A", new string[] { "@ID" }, new object[] { IDMaster });

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

                        workSheet.Cells[3, 8] = TextUtils.ToString(dtMaster.Rows[i]["CustomerCode"]);
                        workSheet.Cells[3, 9] = TextUtils.ToString(dtMaster.Rows[i]["CustomerName"]);
                        workSheet.Cells[3, 12] = TextUtils.ToString(dtMaster.Rows[i]["Note"]);
                        workSheet.Cells[3, 13] = TextUtils.ToString(dtMaster.Rows[i]["FullName"]);
                        workSheet.Cells[3, 24] = TextUtils.ToString(dtMaster.Rows[i]["ProductCode"]);
                        workSheet.Cells[3, 25] = TextUtils.ToString(dtMaster.Rows[i]["ProductName"]);
                        workSheet.Cells[3, 27] = TextUtils.ToString(dtMaster.Rows[i]["ProductGroupName"]);
                        workSheet.Cells[3, 31] = TextUtils.ToString(dtMaster.Rows[i]["Unit"]);
                        workSheet.Cells[3, 32] = TextUtils.ToString(dtMaster.Rows[i]["Qty"]);
                        workSheet.Cells[3, 3] = TextUtils.ToString(dtMaster.Rows[i]["CreatDate"]);
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

        private void btnUnApproved_Click(object sender, EventArgs e)
        {
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproved));
            if (isApproved == false)
            {
                string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCode));
                MessageBox.Show(String.Format("Phiếu xuất [{0}] chưa được duyệt.", code), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Question);
                return;
            }
            int status = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colStatus));
            if (status != 2)
                approved(false);
        }
    }
}
