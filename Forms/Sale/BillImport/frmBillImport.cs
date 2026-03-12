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
using DevExpress.XtraPrinting;
using static Forms.Classes.cGlobVar;
using System.Net.NetworkInformation;
using BMS.Utils;
using Forms.Employee.TeamPhongBan;

namespace BMS
{
    public partial class frmBillImport : _Forms
    {
        public string WarehouseCode;
        public frmBillImport()
        {
            InitializeComponent();
        }

        private void frmBillImport_Load(object sender, EventArgs e)
        {
            this.Text += " - " + WarehouseCode;
            DateTime datenow = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            dtpFromDate.Value = datenow.AddMonths(-1);

            txtPageNumber.Text = "1";
            loadProductGroup();
            cbProductGroup.CheckAll();
            LoadBillType();
            loadBillImport();
            //grvMaster_FocusedRowChanged(null, null);
        }

        #region Methods
        /// <summary>
        /// load kho
        /// </summary>
        void loadProductGroup()
        {
            //DataTable dt = TextUtils.Select("SELECT * FROM ProductGroup");
            List<ProductGroupModel> list = SQLHelper<ProductGroupModel>.FindAll();
            if (!Global.IsAdmin && Global.DepartmentID == 6)
            {
                //if (Global.DepartmentID == 6) 
                list = list.Where(x => x.ProductGroupID == "C" || x.ProductGroupID == "TH").ToList();
                //else if (Global.DepartmentID != 5) list = list.Where(x => x.ProductGroupID != "C").ToList();
                //else list = list.Where(x => x.ProductGroupID != "C").ToList();
            }

            cbProductGroup.Properties.DisplayMember = "ProductGroupName";
            cbProductGroup.Properties.ValueMember = "ID";
            cbProductGroup.Properties.DataSource = list;
        }

        void LoadBillType()
        {
            List<object> list = new List<object>()
            {
                new {ID = -1, Name = "--Tất cả--"},
                new {ID = 0, Name = "Phiếu nhập kho"},
                new {ID = 1, Name = "Phiếu trả"},
                //new {ID = 2, Name = "PTNB"},
                new {ID = 3, Name = "Phiếu mượn NCC"},
                new {ID = 4, Name = "Yêu cầu nhập kho"}
            };
            cboBillTypeNew.Properties.DataSource = list;
            cboBillTypeNew.Properties.ValueMember = "ID";
            cboBillTypeNew.Properties.DisplayMember = "Name";
            cboBillTypeNew.EditValue = -1;
        }

        /// <summary>
        /// load Master
        /// </summary>
        void loadBillImport()
        {
            grdMaster.DataSource = null;
            DateTime dateTimeS = new DateTime();
            if (!chkAllBillImport.Checked)
            {
                dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
                dtpFromDate.Enabled = dtpEndDate.Enabled = true;
            }
            else
            {
                DataTable dtMinCreateDate = TextUtils.Select("SELECT MIN(CreatDate) as MinCreatDate  FROM [dbo].[BillImport]");
                string[] minCreate = dtMinCreateDate.Rows[0]["MinCreatDate"].ToString().Split('/', ' ');
                dateTimeS = new DateTime(TextUtils.ToInt(minCreate[2]), TextUtils.ToInt(minCreate[1]), TextUtils.ToInt(minCreate[0]), 0, 0, 0);
                dtpFromDate.Enabled = dtpEndDate.Enabled = false;
            }
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);
            int billType = TextUtils.ToInt(cboBillTypeNew.EditValue);


            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tải dữ liệu..."))
            {
                DataTable dt = TextUtils.LoadDataFromSP("spGetBillImport_New", "A"
                , new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@Status", "@KhoType", "@FilterText", "@WarehouseCode" }
                , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value),
                   dateTimeS, dateTimeE, billType, cbProductGroup.EditValue, txtFilterText.Text,WarehouseCode });
                grdMaster.DataSource = dt;

                if (dt.Rows.Count == 0) return;
                txtTotalPage.Text = TextUtils.ToString(dt.Rows[0]["TotalPage"]);
            }

        }
        void loadDocSale()
        {
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            DataTable dt = TextUtils.Select($"Select * from DocumentSale where BillID = {ID}  AND BillType = 1");
            grdDocumentFile.DataSource = dt;
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
            //loadDocSale();
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
            frm.WarehouseCode = WarehouseCode;
            var warehouse = SQLHelper<WarehouseModel>.FindByAttribute("WarehouseCode", WarehouseCode).FirstOrDefault() ?? new WarehouseModel();
            frm.warehouseID = warehouse.ID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadBillImport();
                //grvMaster_FocusedRowChanged(null, null);
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
            //BillImportModel model = (BillImportModel)BillImportBO.Instance.FindByPK(ID);
            BillImportModel model = SQLHelper<BillImportModel>.FindByID(ID);
            frmBillImportDetail frm = new frmBillImportDetail();
            frm.billImport = model;
            frm.WarehouseCode = WarehouseCode;
            frm.IDDetail = ID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadBillImport();

                grvMaster.FocusedRowHandle = focusedRowHandle;
                //grvMaster_FocusedRowChanged(null, null);
            }

        }

        /// <summary>
        /// click button để xóa phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int[] selectedRow = grvMaster.GetSelectedRows();

            foreach (var item in selectedRow)
            {
                bool isApproved = TextUtils.ToBoolean(grvMaster.GetRowCellValue(item, colStatus));
                string billCode = TextUtils.ToString(grvMaster.GetRowCellValue(item, colBillCode));
                if (isApproved == true)
                {
                    MessageBox.Show(String.Format("Bạn không thể xóa phiếu nhập [{0}] vì đã được nhận chứng từ!.\nXin vui lòng kiểm tra lại.", billCode), TextUtils.Caption, MessageBoxButtons.OK,
                            MessageBoxIcon.Question);
                    return;
                }

            }
            List<int> listID = new List<int>();
            if (MessageBox.Show(string.Format("Bạn có muốn xóa phiếu nhập các phiếu Nhập đã chọn hay không ?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (var item in selectedRow)
                {
                    int ID = TextUtils.ToInt(grvMaster.GetRowCellValue(item, colIDMaster));
                    string billCode = TextUtils.ToString(grvMaster.GetRowCellValue(item, colBillCode));
                    if (ID == 0)
                    {
                        listID.Clear();
                        return;
                    }
                    var listDetails = SQLHelper<BillImportDetailModel>.FindByAttribute("BillImportID", ID);
                    var inventoryProjects = listDetails.Select(x => x.InventoryProjectID).ToList();
                    if (inventoryProjects.Count > 0)
                    {
                        var myDict = new Dictionary<string, object>()
                      {
                          {InventoryProjectModel_Enum.IsDeleted.ToString(),true },
                          {InventoryProjectModel_Enum.UpdatedBy.ToString(),Global.AppUserName },
                          {InventoryProjectModel_Enum.UpdatedDate.ToString(),DateTime.Now },
                      };
                        string idInventoryProject = string.Join(",", inventoryProjects);
                        var exp = new Expression(InventoryProjectModel_Enum.ID.ToString(), idInventoryProject, "IN");

                        SQLHelper<InventoryProjectModel>.UpdateFields(myDict, exp);
                    }

                    string poNCCDetailID = string.Join(",", listDetails.Select(x => x.PONCCDetailID));
                    TextUtils.ExcuteProcedure("spUpdateStatusPONCC",
                                                new string[] { "@PONCCDetailID", "@UpdatedBy" },
                                                new object[] { poNCCDetailID, Global.LoginName });
                    //TextUtils.ExcuteSQL($"Insert into HistoryDeleteBill(BillID,UserID,DeleteDate,Name,TypeBill) values ({ID},{Global.UserID},'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{Global.AppUserName}','{billCode}') ");
                    SQLHelper<HistoryDeleteBillModel>.Insert(new HistoryDeleteBillModel
                    {
                        BillID = ID,
                        UserID = Global.UserID,
                        DeleteDate = DateTime.Now,
                        Name = Global.AppUserName,
                        TypeBill = billCode
                    });
                    listID.Add(ID);
                }
            }
            var exp1 = new Expression(BillImportModel_Enum.ID.ToString(), string.Join(",", listID), "IN");
            var myDictBillImport = new Dictionary<string, object>()
              {
                  {BillImportModel_Enum.IsDeleted.ToString(),true},
                  {BillImportModel_Enum.UpdatedBy.ToString(),Global.AppUserName},
                  {BillImportModel_Enum.UpdatedDate.ToString(),DateTime.Now},
              };

            SQLHelper<BillImportModel>.UpdateFields(myDictBillImport, exp1);

            grvMaster.DeleteSelectedRows();
        }

        /// <summary>
        /// click button duyệt phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIsApproved_Click(object sender, EventArgs e)
        {
            //bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colStatus));
            //if (isApproved == true)
            //{
            //    string billCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colBillCode));
            //    MessageBox.Show(String.Format("Phiếu nhập [{0}] đã nhận chứng từ. Xin vui lòng kiểm tra lại!", billCode), TextUtils.Caption, MessageBoxButtons.OK,
            //            MessageBoxIcon.Question);
            //    return;
            //}
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
            if (isApproved)
            {

                if (isApproved == false)
                {
                    string billCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colBillCode));
                    MessageBox.Show(String.Format("Phiếu nhập [{0}] chưa nhận chứng từ.", billCode), TextUtils.Caption, MessageBoxButtons.OK,
                            MessageBoxIcon.Question);
                    return;
                }
                approved(false);
            }
        }

        /// <summary>
        /// click button xuất excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcel_Click(object sender, EventArgs e)
        {
            //bool IsApprove = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colStatus));
            //if (!IsApprove)
            //{
            //    MessageBox.Show("Phiếu phải được duyệt trước khi in. Vui lòng kiểm tra lại trạng thái ! ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

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

            int[] selectedRows = grvMaster.GetSelectedRows();
            List<int> rows = new List<int>();
            if (selectedRows.Length <= 0)
            {
                rows.Add(grvMaster.FocusedRowHandle);
                selectedRows = rows.ToArray();
            }

            foreach (var row in selectedRows)
            {
                int ID = TextUtils.ToInt(grvMaster.GetRowCellValue(row, colIDMaster));
                if (ID <= 0) continue;

                string billCode = TextUtils.ToString(grvMaster.GetRowCellValue(row, colBillCode));
                //string folder = Path.Combine(path, billCode);
                //string currentPath = path + "\\" + billCode + DateTime.Now.ToString("_dd_MM_yyyy_HH_mm_ss") + ".xlsx";
                string currentPath = Path.Combine(path, $"{billCode}_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss")}.xlsx");
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

                DataTable dt = TextUtils.LoadDataFromSP("spGetBillImportDetail", "A", new string[] { "@ID" }, new object[] { ID });


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
                        if (WarehouseCode != "HN")
                        {
                            workSheet.Cells[10, 4] = TextUtils.ToString(dt.Rows[0]["WarehouseName"]);
                            workSheet.Cells[10, 3] = "- Kho";
                        }
                        else
                        {
                            workSheet.Cells[10, 4] = TextUtils.ToString(dt.Rows[0]["ProductGroupName"]);
                        }
                        workSheet.Cells[6, 1] = billCode;
                        if (string.IsNullOrEmpty(TextUtils.ToString(grvMaster.GetRowCellValue(row, colDepartmentName))))
                        {
                            workSheet.Cells[8, 4] = TextUtils.ToString(dt.Rows[0]["Deliver"]);
                        }
                        else
                        {
                            workSheet.Cells[8, 4] = TextUtils.ToString(dt.Rows[0]["Deliver"]) + " / Phòng " + TextUtils.ToString(grvMaster.GetRowCellValue(row, colDepartmentName));
                        }

                        workSheet.Cells[24, 8] = TextUtils.ToString(dt.Rows[0]["Reciver"]);
                        workSheet.Cells[17, 8] = date;
                        workSheet.Cells[24, 3] = TextUtils.ToString(dt.Rows[0]["Deliver"]);
                        workSheet.Cells[11, 4] = TextUtils.ToString(dt.Rows[0]["RulePayName"]);

                        #region Tạo QR code cho phiếu nhập xuất
                        string qrCodeText = TextUtils.ToString(billCode);
                        QRCodeGenerator qrGenerator = new QRCodeGenerator();
                        QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrCodeText, QRCodeGenerator.ECCLevel.Q);
                        QRCode qrCode = new QRCode(qrCodeData);

                        Bitmap qrCodeBitmap = qrCode.GetGraphic(20, Color.Black, Color.White, true);
                        Bitmap resizedBitmap = new Bitmap(qrCodeBitmap, new Size(250, 250));

                        string tempFilePath = Path.Combine(Application.StartupPath, $"qrcode_{billCode}.png");
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

                        for (int i = dt.Rows.Count - 1; i >= 0; i--)
                        {
                            workSheet.Cells[15, 1] = i + 1;
                            workSheet.Cells[15, 2] = TextUtils.ToString(dt.Rows[i]["ProductNewCode"]);
                            workSheet.Cells[15, 3] = TextUtils.ToString(dt.Rows[i]["ProductCode"]);
                            workSheet.Cells[15, 4] = TextUtils.ToString(dt.Rows[i]["ProductName"]);
                            workSheet.Cells[15, 5] = TextUtils.ToString(dt.Rows[i]["Unit"]);
                            //workSheet.Cells[15, 5] = TextUtils.ToString(dt.Rows[i]["UnitName"]);
                            workSheet.Cells[15, 6] = TextUtils.ToString(dt.Rows[i]["ProjectCode"]);
                            workSheet.Cells[15, 7] = TextUtils.ToDecimal(dt.Rows[i]["Qty"]);
                            workSheet.Cells[15, 8] = TextUtils.ToString(dt.Rows[i]["SomeBill"]);
                            workSheet.Cells[15, 9] = TextUtils.ToString(dt.Rows[i]["ProjectCodeText"]);
                            workSheet.Cells[15, 10] = TextUtils.ToString(dt.Rows[i]["ProjectNameText"]);

                            workSheet.Cells[15, 11] = TextUtils.ToString(dt.Rows[i]["BillCodePO"]);
                            //workSheet.Cells[15, 11] = TextUtils.ToString(dt.Rows[i]["BillCodePO"]);

                            string note = TextUtils.ToString(dt.Rows[i]["Note"]);
                            note = note.StartsWith("=") ? $"'{note}" : note;
                            string codePM = TextUtils.ToString(dt.Rows[i]["CodeMaPhieuMuon"]);
                            //workSheet.Cells[15, 11] = $"{note}\n{codePM}".Trim();
                            workSheet.Cells[15, 12] = $"{note}\n{codePM}".Trim();
                            ((Excel.Range)workSheet.Rows[15]).Insert();
                        }
                        ((Excel.Range)workSheet.Rows[14]).Delete();
                        ((Excel.Range)workSheet.Rows[14]).Delete();
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

            //Process.Start(path);
            if (selectedRows.Length > 1) Process.Start(path);
        }
        #endregion

        /// <summary>
        /// hàm duyệt
        /// </summary>
        /// <param name="isApproved"></param>
        //void approved(bool isApproved)
        //{
        //    try
        //    {
        //        string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colBillCode));
        //        string message = isApproved ? "nhận chứng từ" : "huỷ nhận chứng từ";
        //        int billType = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colBillTypeNew));
        //        if (billType == 4)
        //        {
        //            MessageBox.Show($"Bạn không thể {message} phiếu Yêu cầu nhập kho!", "Thông báo");
        //            return;
        //        }

        //        if (MessageBox.Show($"Bạn có chắc muốn {message} phiếu [{code}] này?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //        {

        //            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));


        //            //int unapprove = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colUnApprove));
        //            //if (Global.IsAdmin)
        //            //{

        //            //}
        //            //else if (unapprove == 2 && Global.UserID != 1135)
        //            //{
        //            //    MessageBox.Show("Bạn Không có quyền duyệt phiếu này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            //    return;

        //            //}
        //            //string sql = string.Format("UPDATE dbo.BillImport SET Status = {0},UnApprove = {2}  WHERE ID = {1}", isApproved ? 1 : 0, ID, isApproved ? 1 : 2);
        //            //TextUtils.ExcuteSQL(sql);

        //            BillImportModel billImport = SQLHelper<BillImportModel>.FindByID(ID);
        //            billImport.Status = isApproved;

        //            SQLHelper<BillImportModel>.Update(billImport);

        //            calculateImport();


        //            //Update 11/10/2022
        //            UpdateTinhHinhDonHang(isApproved);

        //            if (isApproved == true)
        //            {
        //                //QUANG UPDATE 31/10/2023 KIỂM TRA VÀ UPDATE LẠI TRẠNG THÁI ĐÃ TRẢ CHO PHIẾU MƯỢN
        //                //List<BillImportDetailModel> _lstBillImportDetail = SQLHelper<BillImportDetailModel>.FindByAttribute("BillImportID", ID);
        //                //foreach (BillImportDetailModel model in _lstBillImportDetail)
        //                //{
        //                //    if (model.BillExportDetailID > 0)
        //                //        TextUtils.ExcuteScalar("spUpdateReturnedStatusForBillExportDetail", new string[] { "@BillExportDetailID", }, new object[] { model.BillExportDetailID });
        //                //}

        //                grvMaster.SetFocusedRowCellValue(colStatus, 1);
        //                grvMaster.SetFocusedRowCellValue(colUnApprove, 1);
        //            }
        //            else
        //            {
        //                grvMaster.SetFocusedRowCellValue(colStatus, 0);
        //                grvMaster.SetFocusedRowCellValue(colUnApprove, 2);
        //            }

        //            ////QUANG UPDATE 31/10/2023 KIỂM TRA VÀ UPDATE LẠI TRẠNG THÁI ĐÃ TRẢ CHO PHIẾU MƯỢN
        //            //TextUtils.ExcuteScalar("spUpdateReturnedStatusForBillExportDetail", new string[] { "@BillImportID", "@Approved" }, new object[] { ID, isApproved });

        //            UpdateLog(ID, isApproved);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
        //    }
        //}

        //Nhật update tự động nhận đủ chứng từ và duyệt 1 list
        void approved(bool isApproved)
        {
            try
            {
                List<string> list = new List<string>();
                int[] selectedRows = grvMaster.GetSelectedRows();
                string message = isApproved ? "nhận chứng từ" : "huỷ nhận chứng từ";
                if (selectedRows.Length <= 0)
                {
                    MessageBox.Show($"Vui lòng chọn phiếu Nhập kho để {message}!", "Thông báo");
                    return;
                }
                foreach (var row in selectedRows)
                {
                    int ID = TextUtils.ToInt(grvMaster.GetRowCellValue(row, colIDMaster));
                    string code = TextUtils.ToString(grvMaster.GetRowCellValue(row, colBillCode));
                    int billType = TextUtils.ToInt(grvMaster.GetRowCellValue(row, colBillTypeNew));

                    if (billType == 4)
                    {
                        MessageBox.Show($"Bạn không thể {message} phiếu Yêu cầu nhập kho [{code}]!", "Thông báo");
                        list.Clear();
                        return;
                    }
                    if (isApproved)
                    {
                        string currencyText = TextUtils.ToString(grvMaster.GetRowCellValue(row, colCurrencyList));

                        int[] lstCurrency = currencyText.Split(',').Select(n => TextUtils.ToInt(n)).ToArray();
                        string billCode = TextUtils.ToString(grvMaster.GetRowCellValue(row, colPONCCCodeList));
                        if (lstCurrency.Length > 1)
                        {
                            MessageBox.Show($"Phiếu nhập kho [{code}] có nhiều hơn 1 loại tiền tệ từ đơn mua hàng [{billCode}]. Vui lòng kiểm tra lại!", "Thông báo");
                            return;
                        }
                    }

                    list.Add(ID.ToString());
                }

                if (MessageBox.Show($"Bạn có chắc muốn {message} các phiếu này?", TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    //int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
                    var exp1 = new Expression(BillImportModel_Enum.ID.ToString(), string.Join(",", list), "IN");
                    var dict = new Dictionary<string, object>()
                    {
                        { BillImportModel_Enum.Status.ToString(), isApproved}
                    };
                    SQLHelper<BillImportModel>.UpdateFields(dict, exp1);
                    foreach (int item in selectedRows)
                    {
                        int ID = TextUtils.ToInt(grvMaster.GetRowCellValue(item, colIDMaster));
                        int warehouseID = TextUtils.ToInt(grvMaster.GetRowCellValue(item, colWarehouseName));
                        calculateImport(ID, warehouseID);
                        //Update 11/10/2022

                        if (isApproved == true)
                        {
                            string currencyText = TextUtils.ToString(grvMaster.GetRowCellValue(item, colCurrencyList));
                            if (!string.IsNullOrWhiteSpace(currencyText))
                            {
                                int[] lstCurrency = currencyText.Split(',').Select(n => TextUtils.ToInt(n)).ToArray();
                                //string billCode = TextUtils.ToString(grvMaster.GetRowCellValue(item, colPONCCCodeList));
                                decimal vat = TextUtils.ToDecimal(grvMaster.GetRowCellValue(item, colVAT));
                                if (lstCurrency.Length == 1 && TextUtils.ToInt(currencyText) != 1 && vat == 0)
                                {
                                    UpdateStatusDocumentImport(ID, isApproved);
                                }
                            }
                            
                            grvMaster.SetFocusedRowCellValue(colStatus, 1);
                            grvMaster.SetFocusedRowCellValue(colUnApprove, 1);
                        }
                        else
                        {
                            UpdateStatusDocumentImport(ID, isApproved);
                            grvMaster.SetFocusedRowCellValue(colStatus, 0);
                            grvMaster.SetFocusedRowCellValue(colUnApprove, 2);
                        }

                        ////QUANG UPDATE 31/10/2023 KIỂM TRA VÀ UPDATE LẠI TRẠNG THÁI ĐÃ TRẢ CHO PHIẾU MƯỢN
                        //TextUtils.ExcuteScalar("spUpdateReturnedStatusForBillExportDetail", new string[] { "@BillImportID", "@Approved" }, new object[] { ID, isApproved });
                        //ndnhat update 09/09/2025
                        UpdateLog(ID, isApproved);
                        //UpdateLog(list.Select(x => TextUtils.ToInt(x)).ToList(), isApproved);
                    }
                    UpdateTinhHinhDonHang(isApproved);
                    loadBillImport();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }

        //Cập nhật lịch sử nhận chứng từ
        void UpdateLog(int billImportID, bool status)
        {
            BillImportLogModel log = new BillImportLogModel();
            log.BillImportID = billImportID;
            log.StatusBill = status;
            log.DateStatus = DateTime.Now;

            BillImportLogBO.Instance.Insert(log);
        }

        /// <summary>
        /// hàm tính số lượng khi duyệt , hủy duyệt
        /// </summary>
        void calculateImport(int ID, int warehouseID)
        {
            //int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            //int warehouseID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colWarehouseName));

            //TextUtils.ExcuteProcedure("spCalculateImport", new string[] { "@ID" }, new object[] { ID });
            TextUtils.ExcuteProcedure(StoreProcedure.spCalculateImport_New, new string[] { "@ID", "@WarehouseID" }, new object[] { ID, warehouseID });
        }

        /// <summary>
        /// click button tìm kiếm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFind_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grvMaster.FocusedRowHandle;

            loadBillImport();

            grvMaster.FocusedRowHandle = focusedRowHandle;
            //grvMaster_FocusedRowChanged(null, null);
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadBillImport();
            //grvMaster_FocusedRowChanged(null, null);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadBillImport();
            //grvMaster_FocusedRowChanged(null, null);
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadBillImport();
            //grvMaster_FocusedRowChanged(null, null);
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            loadBillImport();
            //grvMaster_FocusedRowChanged(null, null);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadBillImport();
            //grvMaster_FocusedRowChanged(null, null);
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            List<int> lstBillID = new List<int>();
            int[] index = grvMaster.GetSelectedRows();
            for (int i = 0; i < index.Length; i++)
            {
                lstBillID.Add(TextUtils.ToInt(grvMaster.GetRowCellValue(index[i], colIDMaster)));
            }
            var focusedRowHandle = grvMaster.FocusedRowHandle;
            BillImportModel billImport = SQLHelper<BillImportModel>.FindByID(id);

            frmBillExportDetailNew frm = new frmBillExportDetailNew(false);
            frm.lstBillImportID = lstBillID;
            frm.WarehouseCode = WarehouseCode;
            frm.billImport = billImport;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadBillImport();

                grvMaster.FocusedRowHandle = focusedRowHandle;
                //grvMaster_FocusedRowChanged(null, null);
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
                    if (WarehouseCode == "HN")
                    {
                        workSheet.Cells[2, 14] = "Loại vật tư (*)";
                    }

                    for (int i = dtMaster.Rows.Count - 1; i >= 0; i--)
                    {
                        workSheet.Cells[15, 1] = i + 1;

                        workSheet.Cells[3, 6] = TextUtils.ToString(dtMaster.Rows[i]["CodeNCC"]);
                        workSheet.Cells[3, 7] = TextUtils.ToString(dtMaster.Rows[i]["NameNCC"]);
                        workSheet.Cells[3, 9] = TextUtils.ToString(dtMaster.Rows[i]["Note"]);
                        workSheet.Cells[3, 13] = TextUtils.ToString(dtMaster.Rows[i]["FullName"]);
                        workSheet.Cells[3, 12] = TextUtils.ToString(dtMaster.Rows[i]["ProductCode"]);
                        workSheet.Cells[3, 13] = TextUtils.ToString(dtMaster.Rows[i]["ProductName"]);
                        if (WarehouseCode == "HN")
                        {
                            workSheet.Cells[3, 14] = TextUtils.ToString(dtMaster.Rows[i]["ProductGroupName"]);
                        }
                        else
                        {
                            workSheet.Cells[3, 14] = TextUtils.ToString(dtMaster.Rows[i]["WarehouseName"]);
                        }
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

        private void chkAllBillImport_CheckedChanged(object sender, EventArgs e)
        {
            loadBillImport();
            //grvMaster_FocusedRowChanged(null, null);
        }

        private void hủyDuyệtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colStatus));
            if (isApproved)
            {
                int billType = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colBillTypeText));
                if (billType == 1)
                {
                    if (isApproved == false)
                    {
                        string billCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colBillCode));
                        MessageBox.Show(String.Format("Phiếu nhập [{0}] chưa được duyệt.", billCode), TextUtils.Caption, MessageBoxButtons.OK,
                                MessageBoxIcon.Question);
                        return;
                    }
                    approved(false);
                }
            }
        }

        private void btnExportList_Click(object sender, EventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            f.FileName = $"DanhSanhPhieuNhap_{DateTime.Now.ToString("ddMMyyyy")}.xlsx";
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



        //Update 11/10/2022
        void UpdateTinhHinhDonHang(bool isApproved)
        {
            int PONCCID = 0;
            if (grvData.RowCount == 0) return;
            for (int i = 0; i < grvData.RowCount; i++)
            {

                int PONCCDetailID = TextUtils.ToInt(grvData.GetRowCellValue(i, colPONCCDetailID));
                if (PONCCDetailID <= 0) return;

                //PONCCDetailModel model = (PONCCDetailModel)PONCCDetailBO.Instance.FindByPK(PONCCDetailID);
                PONCCDetailModel model = SQLHelper<PONCCDetailModel>.FindByID(PONCCDetailID);
                if (isApproved)
                {
                    model.QtyReal = model.QtyReal + TextUtils.ToInt(grvData.GetRowCellValue(i, colQuantity));
                    model.Soluongcon = model.QtyRequest - model.QtyReal;

                }
                else
                {
                    model.QtyReal = model.QtyReal - TextUtils.ToInt(grvData.GetRowCellValue(i, colQuantity));
                    model.Soluongcon = model.QtyRequest - model.QtyReal;

                }
                model.Status = model.Soluongcon == 0 ? 1 : 0;
                PONCCID = TextUtils.ToInt(model.PONCCID);
                //PONCCDetailBO.Instance.Update(model);
                SQLHelper<PONCCDetailModel>.Update(model);

                DataTable dt = TextUtils.Select($"Select * from PONCCDetail where PONCCID={PONCCID}");
                if (dt.Rows.Count <= 0) return;
                //Update PONCC Hoàn thành nếu Các PONCCDetail đã về hết .
                //PONCCModel poncc = (PONCCModel)PONCCBO.Instance.FindByPK(PONCCID);//Đang tiến hành 0//Hoàn thành 1//Huỷ 2
                PONCCModel poncc = SQLHelper<PONCCModel>.FindByID(PONCCID);//Đang tiến hành 0//Hoàn thành 1//Huỷ 2
                if (CheckPONCCDetaila(dt))
                {
                    if (poncc.Status == 0)
                    {
                        poncc.Status = 1;
                    }
                }
                else
                {
                    poncc.Status = poncc.Status == 1 ? 0 : poncc.Status_Old;
                }
                PONCCBO.Instance.Update(poncc);
            }
        }

        private bool CheckPONCCDetaila(DataTable dt)
        {
            int dem = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dem = TextUtils.ToInt(dt.Rows[i]["SoLuongCon"]) == 0 ? dem + 1 : dem;

            }
            if (dem == dt.Rows.Count) return true;
            return false;
        }

        private void btnAddFile_Click(object sender, EventArgs e)
        {
            if (grvMaster.GetFocusedRow() == null) return;
            OpenFileDialog o = new OpenFileDialog();
            o.Multiselect = true;
            if (o.ShowDialog() != DialogResult.OK) return;

            int idMaster = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            int year = TextUtils.ToDate5(grvMaster.GetFocusedRowCellValue(colCreatDate)).Year;
            string billCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colBillCode));

            try
            {
                foreach (var file in o.FileNames)
                {
                    string sql = $"SELECT * FROM DocumentSale WHERE BillID = {idMaster} AND BillType = 1 AND FileName = '{Path.GetFileName(file)}'";
                    DocumentSaleModel modelFile = SQLHelper<DocumentSaleModel>.SqlToModel(sql);
                    if (modelFile.ID > 0)
                    {
                        MessageBox.Show($"File [{Path.GetFileName(file)}] đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }

                    modelFile = new DocumentSaleModel();
                    modelFile.FileName = Path.GetFileName(file);
                    modelFile.FileNameOrigin = Path.GetFileName(file);
                    modelFile.BillID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
                    modelFile.BillType = 1;

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
                string billtypeText = "PhieuNhapKho";
                //string url = "http://192.168.1.2:8083/api/Home/uploadfile";
                string path = $@"\\192.168.1.190\Kho\VP.{WarehouseCode}\{billtypeText}\{year}\{billCode}";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                File.Copy(file, Path.Combine(path, Path.GetFileName(file)), true);
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
            string billtypeText = "PhieuNhapKho"; //: "PhieuXuatKho";
            string pathLocation = $@"\\192.168.1.190\Kho\"; //Thư mục trên server

            try
            {
                string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colBillCode));
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

        private void btnBillImportLog_Click(object sender, EventArgs e)
        {
            int billID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (billID <= 0)
            {
                return;
            }

            frmBillLog frm = new frmBillLog();
            frm.billType = 1;
            frm.billImportID = billID;
            frm.Show();
        }

        private void cboBillTypeNew_EditValueChanged(object sender, EventArgs e)
        {
            //loadBillImport();
            //loadBillImportDetail();
            //loadDocSale();
        }

        private void btnBillDocument_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (id <= 0) return;
            frmBillDocument frm = new frmBillDocument();
            frm.billType = 0;
            frm.cboBillCode.EditValue = id;
            frm.Show();
        }

        private void btnDocument_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvMaster.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colBillImportCode));
            if (ID == 0) return;
            BillDocumentImportModel model = (BillDocumentImportModel)BillDocumentImportBO.Instance.FindByPK(ID);

            frmBillDocumentImport frm = new frmBillDocumentImport();
            frm.billDIModel = model;
            frm.billDocumentImportID = ID;
            frm.code = code;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                grvMaster.FocusedRowHandle = focusedRowHandle;
                //grvMaster_FocusedRowChanged(null, null);
            }
        }

        private void btnScanBill_Click(object sender, EventArgs e)
        {
            var wh = SQLHelper<WarehouseModel>.FindByAttribute("WarehouseCode", WarehouseCode).FirstOrDefault();
            frmBillInforScanQR frm = new frmBillInforScanQR();
            frm.WarehouseCode = wh.WarehouseCode;
            frm.warehouseID = wh.ID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                btnFind_Click(null, null);
            }
        }

        frmBillImportSynthetic frm;
        private void btnSynthetic_Click(object sender, EventArgs e)
        {
            try
            {
                if (frm == null || frm.IsDisposed)
                {
                    frm = new frmBillImportSynthetic();
                    frm.WarehouseCode = WarehouseCode;
                    frm.FormClosed += (s, args) => frm = null;
                    frm.Show();
                }
                else
                {
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Activate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }

        private void grvMaster_RowStyle(object sender, RowStyleEventArgs e)
        {
            var view = sender as GridView;
            if (view == null) return;
            if (e.RowHandle < 0) return;
            int overdue = TextUtils.ToInt(view.GetRowCellValue(e.RowHandle, colOverdueQC));
            if (overdue > 0)
            {
                e.Appearance.BackColor = System.Drawing.Color.Red;
                e.Appearance.ForeColor = System.Drawing.Color.White;
            }
            else if (view.FocusedRowHandle == e.RowHandle)
            {
                e.Appearance.BackColor = Color.LightYellow;
                //e.HighPriority = true;
            }


        }

        private void cbProductGroup_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnApprovedStatusDocumentImport_Click(object sender, EventArgs e)
        {
            UpdateStatusDocumentImport(true);
        }

        private void btnUnApprovedStatusDocumentImport_Click(object sender, EventArgs e)
        {
            UpdateStatusDocumentImport(false);
        }

        /// <summary>
        /// Cập nhật trạng thái hồ sơ chứng từ
        /// </summary>
        /// <param name="status">true: Đã nhận đủ; false: Chưa nhận đủ</param>
        void UpdateStatusDocumentImport(bool status)
        {
            try
            {
                int[] selectedRows = grvMaster.GetSelectedRows();

                if (selectedRows.Length <= 0)
                {
                    MessageBox.Show("Vui lòng chọn phiếu muốn cập nhật trạng thái!", "Thông báo");
                    return;
                }
                string statusText = status ? "Nhận chứng từ" : "Hủy nhận chứng từ";
                DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn {statusText} danh sách phiếu đã chọn?\r\n" +
                    $"Những phiếu không phải bạn nhận sẽ tự động được bỏ qua!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.No) return;
                //List<int> ids = new List<int>();
                foreach (int row in selectedRows)
                {
                    int id = TextUtils.ToInt(grvMaster.GetRowCellValue(row, "ID"));
                    int doccumentReceiverID = TextUtils.ToInt(grvMaster.GetRowCellValue(row, "DoccumentReceiverID"));
                    if (doccumentReceiverID != Global.UserID && !Global.IsAdmin) continue;
                    //if (ids.Contains(id)) continue;
                    UpdateStatusDocumentImport(id, status);
                    //ids.Add(id);
                }


                //if (ids.Count() <= 0) return;

                //var myDict = new Dictionary<string, object>()
                //{
                //    {BillImportModel_Enum.StatusDocumentImport.ToString(), status },
                //    {BillImportModel_Enum.UpdatedBy.ToString(), Global.AppUserName },
                //    {BillImportModel_Enum.UpdatedDate.ToString(), DateTime.Now },
                //};

                //string idsText = string.Join(",", ids);

                //var exp = new Expression(BillImportModel_Enum.ID, idsText, "IN");
                //SQLHelper<BillImportModel>.UpdateFields(myDict, exp);

                loadBillImport();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }
        void UpdateStatusDocumentImport(int id, bool status)
        {
            try
            {
                if (id <= 0) return;
                var myDict = new Dictionary<string, object>()
                {
                    { BillImportModel_Enum.StatusDocumentImport.ToString(), status },
                    { BillImportModel_Enum.UpdatedBy.ToString(), Global.AppUserName },
                    { BillImportModel_Enum.UpdatedDate.ToString(), DateTime.Now },
                };

                //var exp = new Expression(BillImportModel_Enum.ID, id);
                SQLHelper<BillImportModel>.UpdateFieldsByID(myDict, id);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex}", "Thông báo");
            }
        }
    }

}


