using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Forms.Classes.cGlobVar;
using DevExpress.Utils;
using DevExpress.XtraEditors.Filtering.Templates;
using ClosedXML.Excel;
using System.Net;
using DevExpress.Charts.Native;

namespace BMS
{
    public partial class frmVehicleBookingManagement : _Forms
    {
        string[] arrParamName = null;
        object[] arrParamValue = null;


        DataTable dt = new DataTable();
        public frmVehicleBookingManagement()
        {
            InitializeComponent();
        }
        private void frmVehicleBookingManagement_Load(object sender, EventArgs e)
        {
            //LinhTN update 11/06/2024
            List<object> lstCategory = new List<object>() {
                new {Category = 0, CategoryText = "Tất cả"},
                new {Category = 1, CategoryText = "Đăng ký đi"},
                new {Category = 5, CategoryText = "Đăng ký về"},
                new {Category = 4, CategoryText = "Chủ động phương tiện"},
                new {Category = 2, CategoryText = "Đăng ký giao hàng"},
                new {Category = 6, CategoryText = "Đăng ký lấy hàng"}
            };
            cboCategory.DataSource = lstCategory;
            cboCategory.ValueMember = "Category";
            cboCategory.DisplayMember = "CategoryText";

            //LinhTN update 11/06/2024
            List<object> lstStatus = new List<object>() {
                new {Status = 0, StatusText = "Tất cả"},
                new {Status = 1, StatusText = "Chờ xếp"},
                new {Status = 2, StatusText = "Đã xếp"},
                new {Status = 4, StatusText = "Chủ động phương tiện"}
            };
            cboStatus.DataSource = lstStatus;
            cboStatus.ValueMember = "Status";
            cboStatus.DisplayMember = "StatusText";


            //dtpD.Value = DateTime.Now.AddDays(+1);
            dtpDateStart.Value = DateTime.Now;
            cboCategory.SelectedValue = 0;
            cboStatus.SelectedValue = 0;
            loadVehicleBookingManagement();
        }
        void loadVehicleBookingManagement()
        {

            if (this.dt.GetChanges() != null)
            {
                string message = "Bạn vừa thay đổi Thời gian xuất phát thực tế.\nBạn có muốn lưu lại không?";
                DialogResult dialog = MessageBox.Show(message, "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    SaveChange(message);
                }
                else if (dialog == DialogResult.Cancel)
                {
                    return;
                }
            }
            int cate = TextUtils.ToInt(cboCategory.SelectedValue);
            int status = TextUtils.ToInt(cboStatus.SelectedValue);
            DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59); ;
            int isCheck = chkCancel.Checked ? 1 : 0;

            this.dt = TextUtils.LoadDataFromSP("spGetVehicleBookingManagement", "VehicleBookingManagement",
                                    new string[] { "@StartDate", "@EndDate", "@Keyword", "Category", "@Status", "@IsCancel" },
                                    new object[] { dateStart, dateEnd, txtKeyword.Text.Trim(), cate, status, isCheck });
            grdVehicleBookingManagement.DataSource = dt;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmVehicleBookingManagementDetail frm = new frmVehicleBookingManagementDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadVehicleBookingManagement();
            }
        }

        //private void btnEdit_Click(object sender, EventArgs e)
        //{
        //    var focusedRowHandle = grvVehicleBookingManagement.FocusedRowHandle;
        //    int id = TextUtils.ToInt(grvVehicleBookingManagement.GetFocusedRowCellValue(colID));
        //    if (id > 0)
        //    {
        //        VehicleBookingManagementModel model = (VehicleBookingManagementModel)VehicleBookingManagementBO.Instance.FindByPK(id);
        //        frmVehicleBookingManagementDetail frm = new frmVehicleBookingManagementDetail();
        //        frm.vehicleBookingManagementModel = model;
        //        if (frm.ShowDialog() == DialogResult.OK)
        //        {
        //            loadVehicleBookingManagement();
        //            grvVehicleBookingManagement.FocusedRowHandle = focusedRowHandle;
        //        }
        //    }
        //}

        //private void grdVehicleBookingManagement_DoubleClick(object sender, EventArgs e)
        //{
        //    btnEdit_Click(null, null);
        //}        

        //private void btnDelete_Click(object sender, EventArgs e)
        //{
        //    int id = TextUtils.ToInt(grvVehicleBookingManagement.GetFocusedRowCellValue(colID));

        //    string company = TextUtils.ToString(grvVehicleBookingManagement.GetFocusedRowCellValue(colCompanyNameArrives));
        //    string name = TextUtils.ToString(grvVehicleBookingManagement.GetFocusedRowCellValue(colBookerVehicles));
        //    if (MessageBox.Show(string.Format("Bạn có thực sự muốn xóa lịch đặt xe đến [{0}] được đặt bởi [{1}]?", company, name), "Xóa xe", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
        //    {
        //        VehicleManagementBO.Instance.Delete(id);
        //        grvVehicleBookingManagement.DeleteSelectedRows();
        //    }
        //}
        private void btnFind_Click(object sender, EventArgs e)
        {
            loadVehicleBookingManagement();
        }
        private void dtpDS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) loadVehicleBookingManagement();
        }

        private void txtKeyword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) loadVehicleBookingManagement();
        }

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadVehicleBookingManagement();
        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadVehicleBookingManagement();
        }
        private void ckCancel_CheckedChanged(object sender, EventArgs e)
        {
            loadVehicleBookingManagement();
        }
        private void btnArrange_Click(object sender, EventArgs e)
        {
            int[] rowSelecteds = grvVehicleBookingManagement.GetSelectedRows();
            if (rowSelecteds.Length > 0)
            {
                List<VehicleBookingManagementModel> lstModel = new List<VehicleBookingManagementModel>();
                int count = 0;
                foreach (int rowIndex in rowSelecteds)
                {
                    //int ID = TextUtils.ToInt(grvVehicleBookingManagement.GetRowCellValue(rowIndex, colID));
                    //if (ID > 0)
                    //{
                    //    string approvedTBP = TextUtils.ToString(grvVehicleBookingManagement.GetRowCellValue(rowIndex, colApprovedTBPText));
                    //    if (approvedTBP == "Chờ duyệt")
                    //    {
                    //        count++;
                    //        continue;
                    //    }
                    //    lstModel.Add((VehicleBookingManagementModel)VehicleBookingManagementBO.Instance.FindByPK(ID));
                    //}
                    int id = TextUtils.ToInt(grvVehicleBookingManagement.GetRowCellValue(rowIndex, colID));
                    bool isProblemArises = TextUtils.ToBoolean(grvVehicleBookingManagement.GetRowCellValue(rowIndex, colIsProblemArises));
                    bool isApprovedTBP = TextUtils.ToBoolean(grvVehicleBookingManagement.GetRowCellValue(rowIndex, colIsApprovedTBP));

                    if (id <= 0) continue;
                    if (isProblemArises && !isApprovedTBP)
                    {
                        count++;
                        continue;
                    }

                    VehicleBookingManagementModel booking = SQLHelper<VehicleBookingManagementModel>.FindByID(id);
                    lstModel.Add(booking);
                }
                //if (count > 0)
                //{
                //    MessageBox.Show("Tất cả các đơn đăng ký có [Vấn đề phát sinh] chưa được TBP duyệt sẽ không được xếp xe!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    //return;
                //}

                if (lstModel.Count <= 0)
                {
                    MessageBox.Show("Đăng ký xe chưa được duyệt phát sinh hoặc không tồn tại đăng ký xe!", "Thông báo");
                    return;
                }
                string message = count > 0 ? "\nTất cả các đơn đăng ký có [Vấn đề phát sinh] chưa được TBP duyệt sẽ không được xếp xe!" : "";

                DialogResult dialog = MessageBox.Show("Bạn có chắc muốn xếp xe cho danh sách đặt xe đã chọn không?" +
                                                        $"{message}", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialog == DialogResult.No) return;

                frmVehicleSchedule frm = new frmVehicleSchedule();
                frm.lisVehicleBookingManagementModel = lstModel;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    loadVehicleBookingManagement();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn đơn đăng ký xe muốn xếp xe!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnWatingArrange_Click(object sender, EventArgs e)
        {
            int[] rowSelecteds = grvVehicleBookingManagement.GetSelectedRows();
            if (rowSelecteds.Length > 0)
            {
                foreach (int rowIndex in rowSelecteds)
                {
                    int ID = TextUtils.ToInt(grvVehicleBookingManagement.GetRowCellValue(rowIndex, colID));
                    if (ID <= 0) continue;
                    //var model = (VehicleBookingManagementModel)VehicleBookingManagementBO.Instance.FindByPK(ID);
                    VehicleBookingManagementModel model = SQLHelper<VehicleBookingManagementModel>.FindByID(ID);
                    model.Status = 1;
                    model.IsCancel = false;
                    model.DepartureAddressActual = "";
                    model.DepartureDateActual = null;
                    model.VehicleManagementID = 0;
                    model.DriverName = "";
                    model.LicensePlate = "";
                    model.NameVehicleCharge = "";
                    model.DriverPhoneNumber = "";
                    //VehicleBookingManagementBO.Instance.Update(model);
                    SQLHelper<VehicleBookingManagementModel>.Update(model);
                }
                loadVehicleBookingManagement();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            int[] rowSelecteds = grvVehicleBookingManagement.GetSelectedRows();
            if (rowSelecteds.Length > 0)
            {
                foreach (int rowIndex in rowSelecteds)
                {
                    int ID = TextUtils.ToInt(grvVehicleBookingManagement.GetRowCellValue(rowIndex, colID));
                    if (ID <= 0) continue;

                    //var model = (VehicleBookingManagementModel)VehicleBookingManagementBO.Instance.FindByPK(ID);
                    VehicleBookingManagementModel model = SQLHelper<VehicleBookingManagementModel>.FindByID(ID);
                    model.Status = 3;
                    model.IsCancel = true;
                    //model.DepartureAddress = "";
                    //model.DepartureDate = null;
                    //model.VehicleManagementID = 0;
                    //model.DriverName = "";
                    //model.LicensePlate = "";
                    //model.NameVehicleCharge = "";
                    //model.DriverPhoneNumber = "";
                    //VehicleBookingManagementBO.Instance.Update(model);
                    SQLHelper<VehicleBookingManagementModel>.Update(model);

                }

                loadVehicleBookingManagement();
            }
        }

        private void btnShowImages_Click(object sender, EventArgs e)
        {
            int[] rowSelecteds = grvVehicleBookingManagement.GetSelectedRows();
            if (rowSelecteds.Length > 0)
            {
                List<VehicleBookingManagementModel> lstModel = new List<VehicleBookingManagementModel>();
                foreach (int rowIndex in rowSelecteds)
                {
                    int ID = TextUtils.ToInt(grvVehicleBookingManagement.GetRowCellValue(rowIndex, colID));
                    if (ID > 0)
                    {
                        lstModel.Add((VehicleBookingManagementModel)VehicleBookingManagementBO.Instance.FindByPK(ID));
                    }
                }
                frmVehicleBookingFileImages frm = new frmVehicleBookingFileImages();
                frm.lisVehicleBookingManagementModel = lstModel;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    loadVehicleBookingManagement();
                }
            }
        }
        private void grvVehicleBookingManagement_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                //string status = TextUtils.ToString(grvVehicleBookingManagement.GetRowCellValue(e.RowHandle, colStatusText));
                int status = TextUtils.ToInt(grvVehicleBookingManagement.GetRowCellValue(e.RowHandle, "Status"));
                if (status == 3/*"Hủy xếp"*/)
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.ForeColor = Color.White;
                }
                else if (status == 1 || status == 4/*"Chưa xếp"*/)
                {
                    e.Appearance.BackColor = Color.Yellow;
                    //string approvedTBP = TextUtils.ToString(grvVehicleBookingManagement.GetRowCellValue(e.RowHandle, colApprovedTBPText));
                    bool isApprovedTBP = TextUtils.ToBoolean(grvVehicleBookingManagement.GetRowCellValue(e.RowHandle, "IsApprovedTBP"));
                    bool isProblemArises = TextUtils.ToBoolean(grvVehicleBookingManagement.GetRowCellValue(e.RowHandle, "IsProblemArises"));
                    if (!isApprovedTBP && isProblemArises/*approvedTBP == "Chờ duyệt"*/)
                    {
                        e.Appearance.BackColor = Color.LightGray;
                    }
                }
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            Approve(true);
        }

        private void btnUnApprove_Click(object sender, EventArgs e)
        {
            Approve(false);
        }

        private void Approve(bool IsApprovedTBP)
        {
            string isApprovedText = IsApprovedTBP ? "duyệt" : "huỷ duyệt";
            int[] rowSelecteds = grvVehicleBookingManagement.GetSelectedRows();
            if (rowSelecteds.Length <= 0)
            {
                MessageBox.Show($"Vui lòng chọn đăng ký xe muốn {isApprovedText}!", "Thông báo");
                return;
            }

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn {isApprovedText} danh sách đặt xe phát sinh đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog != DialogResult.Yes) return;

            Global.DepartmentID = Global.EmployeeID == 54 ? 2 : Global.DepartmentID;
            foreach (int rowIndex in rowSelecteds)
            {
                int ID = TextUtils.ToInt(grvVehicleBookingManagement.GetRowCellValue(rowIndex, colID));
                bool isProblem = TextUtils.ToBoolean(grvVehicleBookingManagement.GetRowCellValue(rowIndex, colIsProblemArises));
                int departmentId = TextUtils.ToInt(grvVehicleBookingManagement.GetRowCellValue(rowIndex, colDepartmentID));
                if (ID <= 0) continue;
                if (!isProblem) continue;
                if (Global.DepartmentID != departmentId && !Global.IsAdmin) continue;
                var model = SQLHelper<VehicleBookingManagementModel>.FindByID(ID);
                model.IsApprovedTBP = IsApprovedTBP;
                VehicleBookingManagementBO.Instance.Update(model);
            }
            loadVehicleBookingManagement();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = Path.Combine(f.SelectedPath, $"LichTrinhXe_{dtpDateStart.Value.ToString("ddMMyy")}.xlsx");

                XlsxExportOptionsEx optionsEx = new XlsxExportOptionsEx();

                optionsEx.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;


                grvVehicleBookingManagement.OptionsPrint.AutoWidth = false;
                grvVehicleBookingManagement.OptionsPrint.ExpandAllDetails = false;
                grvVehicleBookingManagement.OptionsPrint.PrintDetails = true;
                grvVehicleBookingManagement.OptionsPrint.UsePrintStyles = true;
                //optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;

                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                printableComponentLink1.Component = grdVehicleBookingManagement;

                try
                {
                    CompositeLink compositeLink = new CompositeLink(printingSystem);
                    compositeLink.Links.Add(printableComponentLink1);

                    compositeLink.CreatePageForEachLink();
                    optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;

                    compositeLink.PrintingSystem.SaveDocument(filepath);
                    compositeLink.ExportToXlsx(filepath, optionsEx);


                    InsertImagesToExcel(filepath);
                    Process.Start(filepath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        private List<Image> LoadImagesForBookingSync(int bookingId)
        {
            List<Image> imgs = new List<Image>();

            try
            {
                var exp = new Expression("VehicleBookingID", bookingId);
                List<VehicleBookingFileModel> files = SQLHelper<VehicleBookingFileModel>.FindByExpression(exp);
                foreach (var f in files)
                {
                    if (f.CreatedDate == null || string.IsNullOrEmpty(f.FileName))
                        continue;

                    string folder = f.CreatedDate.Value.ToString("dd.MM.yyyy");
                    string url = $"http://113.190.234.64:8083/api/datxe/DANGKYDATXENGAY{folder}/{f.FileName}";

                    try
                    {
                        var request = (HttpWebRequest)WebRequest.Create(url);
                        using (var response = request.GetResponse())
                        using (var stream = response.GetResponseStream())
                        {
                            imgs.Add(Image.FromStream(stream));
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải ảnh Booking ID = {bookingId}: {ex.Message}",
                                "Lỗi tải ảnh", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return imgs;
        }


        private void InsertImagesToExcel(string filePath)
        {
            try
            {
                var dt = (DataTable)grdVehicleBookingManagement.DataSource;
                if (dt == null || dt.Rows.Count == 0) return;

                const int DEFAULT_IMAGE_WIDTH = 120;
                const int DEFAULT_IMAGE_HEIGHT = 90;
                //var workbooka = new XLWorkbook(filePath);
                using (var workbook = new XLWorkbook(filePath))
                {
                    var sheet = workbook.Worksheet(1);
                    var headerRow = 2;
                    var color = XLColor.FromHtml("#D3D3D3");
                    sheet.Row(1).Style.Fill.BackgroundColor = color;
                    sheet.Row(3).Style.Fill.BackgroundColor = color;


                    //Tìm cột ID động
                    int idColumn = -1;
                    for (int col = 1; col <= sheet.ColumnCount(); col++)
                    {
                        if (sheet.Cell(headerRow, col).Value.ToString().Trim() == "ID")
                        {
                            idColumn = col;
                            break;
                        }
                    }
                    if (idColumn == -1)
                    {
                        MessageBox.Show("Không tìm thấy cột ID!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    //Tìm hoặc tạo cột ảnh ban đầu
                    List<int> orderImageColumns = new List<int>();
                    int startCol = sheet.LastColumnUsed()?.ColumnNumber() + 1 ?? 40;
                    string headerPrefix = "Ảnh kiện hàng";

                    // Tìm các cột ảnh sẵn có
                    for (int col = 1; col <= sheet.ColumnCount(); col++)
                    {
                        string header = sheet.Cell(headerRow, col).Value.ToString().Trim();
                        if (header.StartsWith(headerPrefix))
                        {
                            orderImageColumns.Add(col);
                        }
                    }

                    // Nếu không có cột nào thì tạo cột đầu tiên
                    if (orderImageColumns.Count == 0)
                    {
                        int newCol = startCol;
                        var headerCell = sheet.Cell(headerRow, newCol);
                        headerCell.Value = $"{headerPrefix} 1";
                        headerCell.Style.Font.Bold = true;
                        headerCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        headerCell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        headerCell.Style.Fill.BackgroundColor = color;
                        headerCell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        headerCell.Style.Border.OutsideBorderColor = XLColor.FromHtml("#A9A9A9");
                        sheet.Column(newCol).Width = (double)DEFAULT_IMAGE_WIDTH / 7;
                        orderImageColumns.Add(newCol);
                    }

                    // Duyệt từng hàng dữ liệu
                    int maxDataRows = sheet.LastRowUsed()?.RowNumber() ?? 0;
                    for (int excelRow = 4; excelRow <= maxDataRows; excelRow++)
                    {
                        var idCell = sheet.Cell(excelRow, idColumn).Value;
                        if (!int.TryParse(idCell.ToString(), out int bookingId)) continue;

                        var imgs = LoadImagesForBookingSync(bookingId);
                        if (imgs == null || imgs.Count == 0) continue;

                        // Nếu ID này có nhiều ảnh hơn số cột hiện tại → thêm cột mới
                        if (imgs.Count > orderImageColumns.Count)
                        {
                            int currentCount = orderImageColumns.Count;
                            for (int i = currentCount; i < imgs.Count; i++)
                            {
                                int newCol = startCol + i;
                                var headerCell = sheet.Cell(headerRow, newCol);
                                headerCell.Value = $"{headerPrefix} {i + 1}";
                                headerCell.Style.Font.Bold = true;
                                headerCell.Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                                headerCell.Style.Alignment.Vertical = ClosedXML.Excel.XLAlignmentVerticalValues.Center;
                                headerCell.Style.Fill.BackgroundColor = color;
                                headerCell.Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                                headerCell.Style.Border.OutsideBorderColor = XLColor.FromHtml("#A9A9A9");
                                sheet.Column(newCol).Width = (double)DEFAULT_IMAGE_WIDTH / 7;
                                orderImageColumns.Add(newCol);
                            }
                        }

                        // Set chiều cao hàng theo ảnh
                        sheet.Row(excelRow).Height = (double)DEFAULT_IMAGE_HEIGHT * 1;

                        // Chèn ảnh
                        for (int i = 0; i < imgs.Count; i++)
                        {
                            using (var img = new Bitmap(imgs[i]))
                            using (var ms = new MemoryStream())
                            {
                                // Resize ảnh giữ tỷ lệ
                                using (var resized = new Bitmap(img.Width, img.Height))
                                using (Graphics g = Graphics.FromImage(resized))
                                {
                                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                    g.Clear(Color.White);
                                    int drawW = (int)(img.Width);
                                    int drawH = (int)(img.Height);
                                    int xOffset = (img.Width - drawW) / 2;
                                    int yOffset = (img.Height - drawH) / 2;
                                    g.DrawImage(img, xOffset, yOffset, drawW, drawH);
                                    resized.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                }

                                ms.Position = 0;
                                var cell = sheet.Cell(excelRow, orderImageColumns[i]);
                                cell.Clear();
                                var picture = sheet.AddPicture(ms);
                                picture.MoveTo(cell, 2, 5);

                                float scale = Math.Min((float)DEFAULT_IMAGE_WIDTH / img.Width, (float)DEFAULT_IMAGE_HEIGHT / img.Height);
                                picture.Scale(scale);
                            }
                        }
                    }
                    workbook.Save();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }

        private void btnScheduleVehicle_Click(object sender, EventArgs e)
        {
            frmExportVehicleSchedule frm = new frmExportVehicleSchedule();
            frm.date = dtpDateStart.Value;
            frm.dtpDateStart.Value = dtpDateStart.Value;
            frm.dtpDateEnd.Value = dtpDateEnd.Value;
            frm.Show();

            //SendMail();
        }

        private void grvVehicleBookingManagement_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(grvVehicleBookingManagement.GetFocusedRowCellValue(grvVehicleBookingManagement.FocusedColumn));
                if (string.IsNullOrEmpty(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void btnAddVehicleMoney_Click(object sender, EventArgs e)
        {
            frmUpdateVehicleMoney frm = new frmUpdateVehicleMoney();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                int[] selectedRows = grvVehicleBookingManagement.GetSelectedRows();
                if (selectedRows.Length <= 0)
                {
                    int id = TextUtils.ToInt(grvVehicleBookingManagement.GetRowCellValue(grvVehicleBookingManagement.FocusedRowHandle, "ID"));
                    if (id <= 0) return;

                    var myDict = new Dictionary<string, object>()
                    {
                        {"VehicleMoney",TextUtils.ToDecimal(frm.textEdit2.EditValue)},
                        {"UpdatedBy",Global.LoginName },
                        {"UpdatedDate",DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")},
                    };
                    SQLHelper<VehicleBookingManagementModel>.UpdateFieldsByID(myDict, id);
                }
                else
                {
                    foreach (int row in selectedRows)
                    {
                        int id = TextUtils.ToInt(grvVehicleBookingManagement.GetRowCellValue(row, "ID"));
                        if (id <= 0) continue;

                        var myDict = new Dictionary<string, object>()
                        {
                            {"VehicleMoney",TextUtils.ToDecimal(frm.textEdit2.EditValue)},
                            {"UpdatedBy",Global.LoginName },
                            {"UpdatedDate",DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")},
                        };
                        SQLHelper<VehicleBookingManagementModel>.UpdateFieldsByID(myDict, id);
                    }
                }

                loadVehicleBookingManagement();
            }

        }

        bool isRecallCellValueChanged = false;
        private void grvVehicleBookingManagement_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gridView = (GridView)sender;
            if (gridView == null) return;

            //int id = TextUtils.ToInt(gridView.GetFocusedRowCellValue(colID));
            //MessageBox.Show(gridView.Name, id.ToString());


            //if (gridView.FocusedColumn == colCurrencyID) return;
            if (isRecallCellValueChanged == true) return;
            try
            {

                using (WaitDialogForm fWait = new WaitDialogForm())
                {
                    isRecallCellValueChanged = true;
                    gridView.CloseEditor();

                    int[] selectedRows = gridView.GetSelectedRows();

                    if (selectedRows.Length > 0)
                    {
                        if (e.Value == null) return;
                        foreach (int row in selectedRows)
                        {
                            if (e.Column.FieldName == colDepartureDateActual.FieldName)
                            {
                                gridView.SetRowCellValue(row, gridView.Columns[e.Column.FieldName], e.Value);

                                string departureDateActualText = TextUtils.ToString(gridView.GetRowCellDisplayText(row, e.Column.FieldName));
                                gridView.SetRowCellValue(row, colDepartureDateActualText.FieldName, departureDateActualText);
                            }

                        }
                    }
                    else
                    {
                        int row = gridView.FocusedRowHandle;

                        string departureDateActualText = TextUtils.ToString(gridView.GetRowCellDisplayText(row, e.Column.FieldName));
                        gridView.SetRowCellValue(row, colDepartureDateActualText.FieldName, departureDateActualText);
                    }
                }
            }
            finally
            {
                isRecallCellValueChanged = false;
            }
        }

        private void btnUpdateDepartureDateActual_Click(object sender, EventArgs e)
        {
            SaveChange("");
        }


        void SaveChange(string message)
        {
            grvVehicleBookingManagement.CloseEditor();
            var dtChanges = this.dt.GetChanges();
            if (dtChanges == null) return;

            if (string.IsNullOrWhiteSpace(message))
            {
                DialogResult dialog = MessageBox.Show("Bạn có chắc muốn cập nhật lại Thời gian xuất phát thực tế không!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.No) return;

                foreach (DataRow row in dtChanges.Rows)
                {
                    int id = TextUtils.ToInt(row[colID.FieldName]);

                    var myDict = new Dictionary<string, object>()
                    {
                        {"DepartureDateActual",TextUtils.ToDate4(row[colDepartureDateActualText.FieldName]) },
                        {"UpdatedBy",Global.AppUserName },
                        {"UpdatedDate",DateTime.Now },
                    };

                    SQLHelper<VehicleBookingManagementModel>.UpdateFieldsByID(myDict, id);
                }
            }
            else
            {
                foreach (DataRow row in dtChanges.Rows)
                {
                    int id = TextUtils.ToInt(row[colID.FieldName]);

                    var myDict = new Dictionary<string, object>()
                    {
                        {"DepartureDateActual",TextUtils.ToDate4(row[colDepartureDateActualText.FieldName]) },
                        {"UpdatedBy",Global.AppUserName },
                        {"UpdatedDate",DateTime.Now },
                    };

                    SQLHelper<VehicleBookingManagementModel>.UpdateFieldsByID(myDict, id);
                }
            }
            dt.AcceptChanges();
        }

        private void frmVehicleBookingManagement_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.dt.GetChanges() != null)
            {
                string message = "Bạn vừa thay đổi Thời gian xuất phát thực tế.\nBạn có muốn lưu lại không?";
                DialogResult dialog = MessageBox.Show(message, "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    SaveChange(message);
                }
                else if (dialog == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }


        bool SendMail() // NTA B update 08/08/25
        {
            DataTable dt = grdVehicleBookingManagement.DataSource as DataTable;
            EmployeeSendEmailModel sendEmail = new EmployeeSendEmailModel();
            try
            {
                if (dt != null)
                {
                    var grouped = dt.AsEnumerable()
                    .Where(r => r.Field<int>("Status") != 1)
                    .GroupBy(r => r.Field<string>("FullName"))
                    .Select(g => new
                    {
                        FullName = g.Key,
                        ID = g.Select(x => x.Field<int>("PassengerEmployeeID")).FirstOrDefault(),
                        Email = g.Select(x => x.Field<string>("Email")).FirstOrDefault(),
                        DepartureDateActual = g.Select(x => x.Field<DateTime>("DepartureDateActual")).FirstOrDefault(),
                        ListDepartureDate =
                            g.Where(x => x.Field<DateTime?>("DepartureDateActual").HasValue)
                             .Select(x => new
                             {
                                 DepartureDateActual = x.Field<DateTime>("DepartureDateActual"),
                                 VehicleName = x.Field<string>("VehicleInformation"),
                                 LicensePlate = x.Field<string>("LicensePlate"),
                                 DriverName = x.Field<string>("DriverName"),
                                 DepartureAddress = x.Field<string>("DepartureAddress"),
                                 SpecificDestinationAddress = x.Field<string>("SpecificDestinationAddress"),
                             })
                             .Distinct()
                             .OrderBy(d => d.DepartureDateActual)
                    })
                    .ToList();
                    foreach (var row in grouped)
                    {
                        sendEmail.Subject = $"LỊCH TRÌNH XE - {row.FullName} - NGÀY: {row.DepartureDateActual.ToString("dd/MM/yyyy")}".ToUpper();
                        sendEmail.EmailTo = row.Email;
                        sendEmail.StatusSend = 1;
                        //sendEmail.DateSend = DateTime.Now;
                        sendEmail.EmployeeID = Global.EmployeeID;
                        sendEmail.Receiver = row.ID;
                        sendEmail.TableInfor = "VehicleBookingManagement";
                        sendEmail.Body = $@"
                        <div style='font-family: Arial; font-size: 14px;'>
                            Xin chào <b>{row.FullName}</b>,<br/>
                            {string.Join("<br/><br/>", row.ListDepartureDate.Select(d => $@"
                            Lịch trình xe của bạn ngày <b>{d.DepartureDateActual:dd/MM/yyyy}</b>:<br/><br/>
                                - <b>Thời gian khởi hành:</b> {d.DepartureDateActual:dd/MM/yyyy HH:mm}<br/>
                                - <b>Xe:</b> {d.VehicleName}<br/>
                                - <b>Tài xế:</b> {d.DriverName}<br/>
                                - <b>Điểm đi:</b> {d.DepartureAddress}<br/>
                                - <b>Điểm đến:</b> {d.SpecificDestinationAddress}
                            "))}
                            <br/><br/>
                            Trân trọng!
                        </div>
                        ";
                        SQLHelper<EmployeeSendEmailModel>.Insert(sendEmail);
                    }
                }
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}