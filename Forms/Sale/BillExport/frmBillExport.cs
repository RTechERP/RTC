using BMS.Business;
using BMS.Model;
using DevExpress.Utils;
using DevExpress.XtraPrinting;
using Forms.Enums;
using QRCoder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
using static Forms.Classes.cGlobVar;
using Excel = Microsoft.Office.Interop.Excel;

namespace BMS
{
    public partial class frmBillExport : _Forms
    {
        public string WarehouseCode;
        public frmBillExport()
        {
            InitializeComponent();
        }

        private void frmBillExport_Load(object sender, EventArgs e)
        {
            this.Text += " - " + WarehouseCode;

            DateTime datenow = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            dtpFromDate.Value = datenow.AddMonths(-1);

            txtPageNumber.Text = "1";
            loadProductGroup();
            cbProductGroup.CheckAll();
            LoadStatus();
            loadBillExport();
            //grvMaster_FocusedRowChanged(null, null);
        }

        #region Methods

        private void LoadStatus()
        {
            List<object> list = new List<object>() {
                new {ID = -1,Name = "--Tất cả--"},
                new {ID = 0,Name = "Mượn"},
                new {ID = 1,Name = "Tồn Kho"},
                new {ID = 2,Name = "Đã Xuất Kho"},
                //new {ID = 3,Name = "Chia Trước"},
                //new {ID = 4,Name = "Phiếu mượn nội bộ"},
                new {ID = 5,Name = "Xuất trả NCC"},
                new {ID = 6,Name = "Yêu cầu xuất kho"},
                new {ID = 7,Name = "Yêu cầu mượn"}, //PQ.Chien - 12/03/2025
            };
            cboStatusNew.Properties.DataSource = list;
            cboStatusNew.Properties.ValueMember = "ID";
            cboStatusNew.Properties.DisplayMember = "Name";
            cboStatusNew.EditValue = -1;
        }

        /// <summary>
        /// load nhóm
        /// </summary>
        void loadProductGroup()
        {
            //DataTable dt = TextUtils.Select("SELECT * FROM ProductGroup");
            List<ProductGroupModel> list = SQLHelper<ProductGroupModel>.FindAll();
            if (!Global.IsAdmin)
            {
                if (Global.DepartmentID == 6) list = list.Where(x => x.ProductGroupID == "C").ToList();
                else list = list.Where(x => x.ProductGroupID != "C").ToList();
            }

            cbProductGroup.Properties.DisplayMember = "ProductGroupName";
            cbProductGroup.Properties.ValueMember = "ID";
            cbProductGroup.Properties.DataSource = list;
        }

        /// <summary>
        /// load bảng Master
        /// </summary>
        private void loadBillExport()
        {
            grdMaster.DataSource = null;
            //DateTime dateTimeS = new DateTime();
            //if (!chkAllBillExport.Checked)
            //{
            //    dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            //    dtpFromDate.Enabled = dtpEndDate.Enabled = true;
            //}
            //else
            //{
            //    DataTable dtMinCreateDate = TextUtils.Select("SELECT MIN(CreatDate) as MinCreatDate FROM [dbo].[BillExport]");
            //    string[] minCreate = dtMinCreateDate.Rows[0]["MinCreatDate"].ToString().Split('/', ' ');
            //    dateTimeS = new DateTime(TextUtils.ToInt(minCreate[2]), TextUtils.ToInt(minCreate[1]), TextUtils.ToInt(minCreate[0]), 0, 0, 0);
            //    dtpFromDate.Enabled = dtpEndDate.Enabled = false;
            //}
            //DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);


            DateTime? dateStart = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            DateTime? dateEnd = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);

            if (chkAllBillExport.Checked) dateStart = dateEnd = null;
            dtpFromDate.Enabled = dtpEndDate.Enabled = !chkAllBillExport.Checked;

            int status = TextUtils.ToInt(cboStatusNew.EditValue);
            int pageNumber = TextUtils.ToInt(txtPageNumber.Text);
            int pageSize = TextUtils.ToInt(txtPageSize.Value);

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tải dữ liệu..."))
            {


                DataTable dt = TextUtils.LoadDataFromSP("spGetBillExport_New", "A"
                                        , new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@Status", "@KhoType", "@FilterText", "@WarehouseCode" }
                                        , new object[] { pageNumber, pageSize, dateStart, dateEnd, status, cbProductGroup.EditValue, txtFilterText.Text, WarehouseCode });


                grdMaster.DataSource = dt;
                if (dt.Rows.Count == 0) return;
                txtTotalPage.Text = TextUtils.ToString(dt.Rows[0]["TotalPage"]);
            }
        }
        void loadDocSale()
        {
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            DataTable dt = TextUtils.Select($"Select * from DocumentSale where BillID = {ID}  AND BillType = 2");
            grdDocumentFile.DataSource = dt;
        }
        /// <summary>
        /// load bảng Detail
        /// </summary>
        void loadBillExportDetail()
        {
            try
            {
                DataTable dt = TextUtils.LoadDataFromSP("spGetBillExportDetail", "A", new string[] { "@BillID" }, new object[] { TextUtils.ToInt64(grvMaster.GetFocusedRowCellValue(colIDMaster)) });
                grdData.DataSource = dt;

                treeList1.DataSource = dt;
                treeList1.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
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
            //frmBillExportDetail frm = new frmBillExportDetail();
            frmBillExportDetailNew frm = new frmBillExportDetailNew(false);
            frm.WarehouseCode = WarehouseCode;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadBillExport();
                //grvMaster_FocusedRowChanged(null, null);
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
            //BillExportModel model = (BillExportModel)BillExportBO.Instance.FindByPK(ID);
            BillExportModel model = SQLHelper<BillExportModel>.FindByID(ID);
            frmBillExportDetailNew frm = new frmBillExportDetailNew(false);
            frm.WarehouseCode = WarehouseCode;
            frm.billExport = model;
            frm.IDDetail = ID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //loadBillExport();
                //grvMaster.FocusedRowHandle = focusedRowHandle;
                //grvMaster_FocusedRowChanged(null, null);
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
                MessageBox.Show(String.Format("Bạn không thể xóa phiếu xuất [{0}] này!", code), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Question);
                return;
            }

            if (!grvMaster.IsDataRow(grvMaster.FocusedRowHandle)) return;

            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue("ID"));
            if (ID == 0) return;

            if (MessageBox.Show(String.Format("Bạn có chắc muốn xóa phiếu xuất [{0}] không?", code), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {


                //BillExportBO.Instance.Delete(ID);
                //BillExportDetailBO.Instance.DeleteByAttribute("BillID", ID);

                var myDictBillExport = new Dictionary<string, object>()
                {
                    {BillExportModel_Enum.IsDeleted.ToString(),true},
                    {BillExportModel_Enum.UpdatedBy.ToString(),Global.AppUserName},
                    {BillExportModel_Enum.UpdatedDate.ToString(),DateTime.Now},
                };

                SQLHelper<BillExportModel>.UpdateFieldsByID(myDictBillExport, ID);

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
            //bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproved));
            //if (isApproved == true)
            //{
            //    string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCode));
            //    MessageBox.Show(String.Format("Phiếu xuất [{0}] đã được nhận chứng từ.", code), TextUtils.Caption, MessageBoxButtons.OK,
            //            MessageBoxIcon.Question);
            //    return;
            //}
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
                MessageBox.Show(String.Format("Phiếu xuất [{0}] chưa nhận chứng từ.", code), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Question);
                return;
            }
            approved(false);
        }

        /// <summary>
        /// Xuất excel
        /// </summary>
        /// <param name="type">
        /// 1.Xuất theo nhóm item
        /// 2. Xuất tất cả item
        /// </param>
        void ExportExcel(int type)
        {

            string path = "";
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK) path = fbd.SelectedPath;
            else return;

            string fileSourceName = "PhieuXuatSALE.xls";

            string sourcePath = Application.StartupPath + "\\" + fileSourceName;

            int[] selectedRows = grvMaster.GetSelectedRows();

            List<int> rows = new List<int>();
            if (selectedRows.Length <= 0)
            {
                rows.Add(grvMaster.FocusedRowHandle);
                selectedRows = rows.ToArray();
            }

            foreach (var row in selectedRows)
            {
                int IDMaster = TextUtils.ToInt(grvMaster.GetRowCellValue(row, colIDMaster));
                if (IDMaster <= 0) continue;

                string phieucode = TextUtils.ToString(grvMaster.GetRowCellValue(row, colCode));
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

                        string fullName = TextUtils.ToString(grvMaster.GetRowCellValue(row, "FullName")).Trim();
                        string departmentName = TextUtils.ToString(grvMaster.GetRowCellValue(row, colDepartmentName)).Trim();

                        if (string.IsNullOrWhiteSpace(departmentName))
                        {
                            //workSheet.Cells[9, 4] = TextUtils.ToString(dtMaster.Rows[0]["FullName"]);
                            workSheet.Cells[9, 4] = fullName;
                        }
                        else
                        {
                            //workSheet.Cells[9, 4] = TextUtils.ToString(dtMaster.Rows[0]["FullName"]) + " / Phòng " + TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colDepartmentName));
                            workSheet.Cells[9, 4] = $"{fullName} / Phòng {departmentName}";
                        }


                        string customerName = TextUtils.ToString(dtMaster.Rows[0]["CustomerName"]).Trim();
                        string supplierName = TextUtils.ToString(dtMaster.Rows[0]["NameNCC"]).Trim();

                        //workSheet.Cells[10, 4] = TextUtils.ToString(dtMaster.Rows[0]["CustomerName"]);
                        workSheet.Cells[10, 3] = "'- Khách hàng/Nhà cung cấp:";
                        workSheet.Cells[10, 4] = string.IsNullOrEmpty(customerName) ? supplierName : customerName;
                        workSheet.Cells[11, 4] = TextUtils.ToString(dtMaster.Rows[0]["Address"]);
                        workSheet.Cells[12, 4] = TextUtils.ToString(dtMaster.Rows[0]["AddressStock"]);
                        workSheet.Cells[25, 3] = TextUtils.ToString(dtMaster.Rows[0]["FullNameSender"]);
                        if (WarehouseCode == "HN")
                        {
                            workSheet.Cells[15, 10] = "Loại vật tư";
                        }

                        //string creatDate1 = TextUtils.ToDate3(dtMaster.Rows[0]["CreatDate"]).ToString("dd");
                        //string creatDate2 = TextUtils.ToDate3(dtMaster.Rows[0]["CreatDate"]).ToString("MM");
                        //string creatDate3 = TextUtils.ToDate3(dtMaster.Rows[0]["CreatDate"]).ToString("yyyy");

                        DateTime? creatDate = TextUtils.ToDate3(dtMaster.Rows[0]["CreatDate"]);
                        string creatDateDay = creatDate.HasValue ? creatDate.Value.ToString("dd") : "";
                        string creatDateMonth = creatDate.HasValue ? creatDate.Value.ToString("MM") : "";
                        string creatDateYear = creatDate.HasValue ? creatDate.Value.ToString("yyyy") : "";

                        //workSheet.Cells[18, 9] = "Ngày " + creatDate1 + " tháng " + creatDate2 + " năm " + creatDate3;
                        workSheet.Cells[18, 9] = "Ngày " + creatDateDay + " tháng " + creatDateMonth + " năm " + creatDateYear;
                        workSheet.Cells[25, 9] = TextUtils.ToString(dtMaster.Rows[0]["FullName"]);

                        #region QR phiếu xuất
                        string qrCodeText = TextUtils.ToString(totalName);
                        QRCodeGenerator qrGenerator = new QRCodeGenerator();
                        QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrCodeText, QRCodeGenerator.ECCLevel.Q);
                        QRCode qrCode = new QRCode(qrCodeData);

                        Bitmap qrCodeBitmap = qrCode.GetGraphic(20, Color.Black, Color.White, true);
                        Bitmap resizedBitmap = new Bitmap(qrCodeBitmap, new Size(250, 250));

                        string tempFilePath = Path.Combine(Application.StartupPath, $"qrcode_{totalName}.png");
                        qrCodeBitmap.Save(tempFilePath);
                        // set vị trí và kích thước 
                        float left = 1000;
                        float top = 5;
                        float width = 100;
                        float height = 100;

                        workSheet.Shapes.AddPicture(tempFilePath,
                                                    Microsoft.Office.Core.MsoTriState.msoFalse,
                                                    Microsoft.Office.Core.MsoTriState.msoCTrue,
                                                    left, top, width, height);

                        File.Delete(tempFilePath);
                        #endregion


                        for (int i = dtMaster.Rows.Count - 1; i >= 0; i--)
                        //for (int i = grvData.RowCount - 1; i >= 0; i--)
                        {
                            int parentID = TextUtils.ToInt(grvData.GetRowCellValue(i, colParentID));
                            if (type == 1 && parentID != 0) continue;
                            workSheet.Cells[16, 1] = i + 1;
                            workSheet.Cells[16, 2] = TextUtils.ToString(dtMaster.Rows[i]["ProductNewCode"]);
                            workSheet.Cells[16, 3] = TextUtils.ToString(dtMaster.Rows[i]["ProductCode"]);
                            workSheet.Cells[16, 4] = TextUtils.ToString(dtMaster.Rows[i]["ProductFullName"]);
                            workSheet.Cells[16, 5] = TextUtils.ToString(dtMaster.Rows[i]["ProductName"]);
                            workSheet.Cells[16, 6] = TextUtils.ToString(dtMaster.Rows[i]["Unit"]);
                            workSheet.Cells[16, 7] = TextUtils.ToDecimal(dtMaster.Rows[i]["Qty"]);
                            workSheet.Cells[16, 8] = TextUtils.ToString(dtMaster.Rows[i]["ProjectCodeText"]);
                            workSheet.Cells[16, 9] = TextUtils.ToString(dtMaster.Rows[i]["ProjectNameText"]);
                            workSheet.Cells[16, 10] = TextUtils.ToString(dtMaster.Rows[i]["ProductTypeText"]);

                            workSheet.Cells[16, 11] = TextUtils.ToString(dtMaster.Rows[i]["UnitPricePOKH"]);
                            workSheet.Cells[16, 12] = TextUtils.ToString(dtMaster.Rows[i]["UnitPricePurchase"]);

                            workSheet.Cells[16, 13] = TextUtils.ToString(dtMaster.Rows[i]["WarehouseName"]);
                            if (WarehouseCode == "HN") workSheet.Cells[16, 13] = TextUtils.ToString(dtMaster.Rows[i]["ProductGroupName"]);

                            string note = TextUtils.ToString(dtMaster.Rows[i]["Note"]);
                            note = note.StartsWith("=") ? $"'{note}" : note;
                            workSheet.Cells[16, 14] = note;
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
                    if (selectedRows.Length <= 1) Process.Start(currentPath);
                }
            }

            if (selectedRows.Length > 1) Process.Start(path);


        }

        /// <summary>
        /// click để xuất ra excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcel_Click(object sender, EventArgs e)
        {
            ExportExcel(2);
            //bool IsApprove = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproved));
            //if (!IsApprove)
            //{
            //    MessageBox.Show("Phiếu phải được duyệt trước khi in. Vui lòng kiểm tra lại trạng thái ! ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

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
                grvMaster_FocusedRowChanged(null, null);
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
            string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCode));
            string message = isApproved ? "nhận chứng từ" : "huỷ nhận chứng từ";
            //int status = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colStatus));
            //if(status ==3)
            //{
            //    MessageBox.Show(string.Format("Phiếu này không được duyệt, kiểm tra lại trạng thái", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error));
            //    return;
            //}    
            if (MessageBox.Show($"Bạn có chắc muốn {message} phiếu [{code}] này?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
                //int unapprove = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colUnApprove));
                //if (Global.IsAdmin)
                //{

                //}
                //else if (unapprove == 2 && Global.UserID != 1135)
                //{
                //    MessageBox.Show("Bạn Không có quyền duyệt phiếu này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;

                //}
                //string sql = string.Format(@"UPDATE dbo.BillExport SET IsApproved = {0}, UnApprove ={2} WHERE ID = {1}", isApproved ? 1 : 0, iD, isApproved ? 1 : 2);
                //TextUtils.ExcuteSQL(sql);

                BillExportModel billExport = SQLHelper<BillExportModel>.FindByID(id);
                billExport.IsApproved = isApproved;

                SQLHelper<BillExportModel>.Update(billExport);

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

                UpdateLog(id, isApproved);
            }
        }

        //Cập nhật lịch sử nhận chứng từ
        void UpdateLog(int billExportID, bool status)
        {
            BillExportLogModel log = new BillExportLogModel();
            log.BillExportID = billExportID;
            log.StatusBill = status;
            log.DateStatus = DateTime.Now;

            BillExportLogBO.Instance.Insert(log);
        }
        /// <summary>
        /// tính toán số lượng duyệt
        /// </summary>
        void calculateExport()
        {
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            //int WarehouseID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colWarehouseName));
            int WarehouseID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colWarehouseID));
            //TextUtils.ExcuteProcedure("spCalculateExport", new string[] { "@ID" }, new object[] { ID });
            TextUtils.ExcuteProcedure(StoreProcedure.spCalculateExport_New, new string[] { "@ID", "@WarehouseID" }, new object[] { ID, WarehouseID });
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
            //loadDocSale();
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
            grvMaster.FocusedRowHandle = focusedRowHandle;
            //grvMaster_FocusedRowChanged(null, null);
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadBillExport();
            //grvMaster_FocusedRowChanged(null, null);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadBillExport();
            //grvMaster_FocusedRowChanged(null, null);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadBillExport();
            //grvMaster_FocusedRowChanged(null, null);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadBillExport();
            //grvMaster_FocusedRowChanged(null, null);
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            loadBillExport();
            //grvMaster_FocusedRowChanged(null, null);
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadBillExport();
            //grvMaster_FocusedRowChanged(null, null);
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
            string currentPath = path + "\\" + phieucode + DateTime.Now.ToString("_dd_MM_yyyy_HH_mm_ss") + ".xlsx";
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
                    if (WarehouseCode == "HN")
                    {
                        workSheet.Cells[2, 14] = "Loại vật tư (*)";
                    }

                    for (int i = dtMaster.Rows.Count - 1; i >= 0; i--)
                    {
                        workSheet.Cells[15, 1] = i + 1;

                        workSheet.Cells[3, 8] = TextUtils.ToString(dtMaster.Rows[i]["CustomerCode"]);
                        workSheet.Cells[3, 9] = TextUtils.ToString(dtMaster.Rows[i]["CustomerName"]);
                        workSheet.Cells[3, 12] = TextUtils.ToString(dtMaster.Rows[i]["Note"]);
                        workSheet.Cells[3, 13] = TextUtils.ToString(dtMaster.Rows[i]["FullName"]);
                        workSheet.Cells[3, 24] = TextUtils.ToString(dtMaster.Rows[i]["ProductCode"]);
                        workSheet.Cells[3, 25] = TextUtils.ToString(dtMaster.Rows[i]["ProductName"]);
                        if (WarehouseCode == "HN")
                        {
                            workSheet.Cells[3, 14] = TextUtils.ToString(dtMaster.Rows[i]["ProductGroupName"]);
                        }
                        else
                        {
                            workSheet.Cells[3, 14] = TextUtils.ToString(dtMaster.Rows[i]["WarehouseName"]);
                        }
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

        private void chkAllBillExport_CheckedChanged(object sender, EventArgs e)
        {
            loadBillExport();
            //grvMaster_FocusedRowChanged(null, null);
        }

        private void btnExportList_Click(object sender, EventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            f.FileName = $"DanhSanhPhieuXuat_{DateTime.Now.ToString("ddMMyyyy")}.xlsx";

            if (f.ShowDialog() == DialogResult.OK)
            {
                XlsxExportOptionsEx optionsEx = new XlsxExportOptionsEx();
                optionsEx.AllowSortingAndFiltering = DevExpress.Utils.DefaultBoolean.False;
                optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                grvMaster.OptionsPrint.PrintSelectedRowsOnly = false;
                try
                {
                    //string filepath = $"{f.SelectedPath}\\DanhSanhPhieuNhap_{DateTime.Now.ToString("ddMMyyyy")}.xls";
                    grvMaster.ExportToXlsx(f.FileName, optionsEx);

                    Process.Start(f.FileName);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btnAddFile_Click(object sender, EventArgs e)
        {
            if (grvMaster.GetFocusedRow() == null) return;
            OpenFileDialog o = new OpenFileDialog();
            o.Multiselect = true;
            if (o.ShowDialog() != DialogResult.OK) return;

            int idMaster = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            int year = TextUtils.ToDate5(grvMaster.GetFocusedRowCellValue(colCreatDate)).Year;
            string billCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCode));

            try
            {
                foreach (var file in o.FileNames)
                {
                    var fileName = Path.GetFileName(file);
                    DocumentSaleModel modelFile = SQLHelper<DocumentSaleModel>.SqlToModel($"SELECT * FROM DocumentSale WHERE BillID = {idMaster} AND BillType = 2 AND FileName = '{Path.GetFileName(file)}'");
                    if (modelFile.ID > 0)
                    {
                        MessageBox.Show($"File [{Path.GetFileName(file)}] đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }

                    modelFile = new DocumentSaleModel();
                    modelFile.FileName = Path.GetFileName(file);
                    modelFile.FileNameOrigin = Path.GetFileName(file);
                    modelFile.BillID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
                    modelFile.BillType = 2;

                    DocumentFileBO.Instance.Insert(modelFile);

                    //listFilename.Add(fileCopy);
                    //Đẩy lên sever 
                    UploadFile(file, year, billCode);

                    //Load lại data
                    loadDocSale();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            int[] RowIndex = grvDocumentFile.GetSelectedRows();
            if (RowIndex.Count() < 1)
            {
                MessageBox.Show("Vui lòng chọn để xóa !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa những file vừa chọn ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {


                for (int i = 0; i < RowIndex.Length; i++)
                {
                    int id = TextUtils.ToInt(grvDocumentFile.GetRowCellValue(RowIndex[i], colIDFileName));
                    DocumentSaleBO.Instance.Delete(id);
                }
                loadDocSale();
            }
        }

        private void UploadFile(string file, int year, string billCode)
        {
            try
            {
                string billtypeText = "PhieuXuatKho";
                //string url = "http://192.168.1.2:8083/api/Home/uploadfile";
                string path = $@"\\192.168.1.190\Kho\VP.{WarehouseCode}\{billtypeText}\{year}\{billCode}";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                File.Copy(file, Path.Combine(path, Path.GetFileName(file)));
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            //using (var client = new HttpClient())
            //{
            //    using (var formData = new MultipartFormDataContent())
            //    {
            //        formData.Add(new StringContent(path), "path");
            //        formData.Add(new StreamContent(File.OpenRead(file)), "file");

            //        var response = await client.PostAsync(url, formData);
            //    }
            //}
        }

        private void btnTreeFolder_Click(object sender, EventArgs e)
        {
            string billtypeText = "PhieuXuatKho"; //: "PhieuXuatKho";
            string pathLocation = $@"\\192.168.1.190\Kho\"; //Thư mục trên server

            try
            {
                string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCode));
                int year = TextUtils.ToDate5(grvMaster.GetFocusedRowCellValue(colCreatDate)).Year;

                Ping ping = new Ping();
                PingReply reply = ping.Send("192.168.1.190");
                if (reply.Status != IPStatus.Success)
                {
                    pathLocation = $@"\\113.190.234.64\Kho\";
                }

                string path = $@"{pathLocation}\VP.{WarehouseCode}\{billtypeText}\{year}\{code}\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                Process.Start(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Thông báo");
            }
        }

        private void grvMaster_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(grvMaster.GetFocusedRowCellValue(grvMaster.FocusedColumn)));
                e.Handled = true;
            }
        }

        private void txtFilterText_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBillExportLog_Click(object sender, EventArgs e)
        {
            int billID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (billID <= 0)
            {
                return;
            }

            frmBillLog frm = new frmBillLog();
            frm.billType = 0;
            frm.billExportID = billID;
            frm.Show();
        }

        private void cboStatusNew_EditValueChanged(object sender, EventArgs e)
        {
            //loadBillExport();
            //loadBillExportDetail();
        }

        private void btnBillDocument_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (id <= 0) return;
            frmBillDocument frm = new frmBillDocument();
            frm.billType = 1;
            frm.cboBillCode.EditValue = id;
            frm.Show();
        }


        private void btnPrepared_Click(object sender, EventArgs e)
        {
            string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCode));

            if (MessageBox.Show($"Bạn muốn xác nhận đã chuẩn bị xong phiếu [{code}] này?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));

                BillExportModel billExport = SQLHelper<BillExportModel>.FindByID(id);
                billExport.IsPrepared = true;
                billExport.PreparedDate = DateTime.Now;
                SQLHelper<BillExportModel>.Update(billExport);

                if (billExport.IsPrepared == true)
                    grvMaster.SetFocusedRowCellValue(colIsPrepared, 1);
                grvMaster.SetFocusedRowCellValue(colIsPrepared, 0);
                loadBillExport();

            }
        }

        private void btnReceived_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));

            BillExportModel billExport = SQLHelper<BillExportModel>.FindByID(id);

            if (billExport.IsPrepared == false) return;

            string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCode));

            if (MessageBox.Show($"Bạn muốn xác nhận đã nhận hàng phiếu [{code}] này?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                billExport.IsReceived = true;
                billExport.Status = 2;
                billExport.CreatDate = DateTime.Now;
                SQLHelper<BillExportModel>.Update(billExport);

                if (billExport.IsReceived == true)
                    grvMaster.SetFocusedRowCellValue(colIsReceived, 1);
                grvMaster.SetFocusedRowCellValue(colIsReceived, 0);
                loadBillExport();

            }
        }

        private void btnBillDocumentExport_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvMaster.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCode));
            if (ID == 0) return;

            frmBillDocumentExport frm = new frmBillDocumentExport();
            frm.billExportID = ID;
            frm.code = code;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                grvMaster.FocusedRowHandle = focusedRowHandle;
                grvMaster_FocusedRowChanged(null, null);
            }
        }

        private void btnScanBill_Click(object sender, EventArgs e)
        {
            var wh = SQLHelper<WarehouseModel>.FindByAttribute("WarehouseCode", WarehouseCode).FirstOrDefault();
            frmBillExportQR frm = new frmBillExportQR();
            frm.WarehouseCode = wh.WarehouseCode;
            frm.warehouseID = wh.ID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                btnFind_Click(null, null);
            }
        }

        private void btnSynthetic_Click(object sender, EventArgs e)
        {
            frmBillExportSynthetic frm = new frmBillExportSynthetic();
            frm.WarehouseCode = WarehouseCode;
            frm.ShowDialog();
            if (DialogResult == DialogResult.OK) loadBillExport();
        }

        private void btnBorrowNCCReport_Click(object sender, EventArgs e)
        {
            frmInventoryBorrowNCC frm = new frmInventoryBorrowNCC();
            frm.warehouseID = 1;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadBillExport();
            }
        }

        private void btnExportGroupItem_Click(object sender, EventArgs e)
        {
            ExportExcel(1);
        }

        private void btnExportAllItem_Click(object sender, EventArgs e)
        {
            ExportExcel(2);
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
