using BMS;
using BMS.Business;
using BMS.Model;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using Forms.Classes;
using QRCoder;
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

namespace Forms.Technical
{
    public partial class frmBillImportTechnical : _Forms
    {
        public int warehouseID;
        public frmBillImportTechnical()
        {
            InitializeComponent();
        }
        public frmBillImportTechnical(int WarehouseID)
        {
            InitializeComponent();
            warehouseID = WarehouseID;
        }
        private void frmBillImportTechnical_Load(object sender, EventArgs e)
        {
            cboStatus.SelectedIndex = 0;
            DateTime datenow = new DateTime(dtpDS.Value.Year, dtpDS.Value.Month, dtpDS.Value.Day, 0, 0, 0);
            dtpDS.Value = datenow.AddMonths(-1);
            txtPageNumber.Text = "1";
            loadBillImport();
            this.Text += warehouseID == 1 ? " - HN" : (warehouseID == 2 ? " - HCM" : " - BN");
            grvDetail.CustomUnboundColumnData += gridView1_CustomUnboundColumnData;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmBillImportTechDetail_New frm = new frmBillImportTechDetail_New(warehouseID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadBillImport();
            }

        }



        /// <summary>
        /// load Master
        /// </summary>
        void loadBillImport()
        {
            DateTime dateTimeS = new DateTime(dtpDS.Value.Year, dtpDS.Value.Month, dtpDS.Value.Day, 0, 0, 0);
            DateTime dateTimeE = new DateTime(dtpDE.Value.Year, dtpDE.Value.Month, dtpDE.Value.Day, 23, 59, 59);

            DataSet dataSet = new DataSet();
            dataSet = TextUtils.LoadDataSetFromSP("spGetBillImportTechnical"
                , new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@Status", "@FilterText", "@WarehouseID" }
                , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), dateTimeS, dateTimeE, cboStatus.SelectedIndex - 1, txtKeyword.Text, warehouseID });
            grdBillImportTech.DataSource = dataSet.Tables[0];
            if (dataSet.Tables.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(dataSet.Tables[1].Rows[0]["TotalPage"]);
            loadBillImportDetail();
        }



        /// <summary>
        /// load bảng Detail
        /// </summary>
        void loadBillImportDetail()
        {
            int ID = TextUtils.ToInt(grvBillImportTech.GetFocusedRowCellValue(colID));
            DataTable dt = TextUtils.LoadDataFromSP("spGetBillImportDetailTechnical", "A", new string[] { "@ID" }, new object[] { ID });
            grdDetail.DataSource = dt;
        }

        private void grvBillImportTech_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadBillImportDetail();
        }

        private void grdBillImportTech_Click(object sender, EventArgs e)
        {

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

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
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

        private void btnFind_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grvBillImportTech.FocusedRowHandle;
            grvBillImportTech.FocusedRowHandle = focusedRowHandle - 1;
            loadBillImport();
        }

        private void gridView1_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData && e.Column.FieldName == "Image")
            {
                string fileName = grvDetail.GetListSourceRowCellValue(e.ListSourceRowIndex, colImage).ToString();
                e.Value = Bitmap.FromFile(fileName);

            }
        }

        private void btnIsApproved_Click(object sender, EventArgs e)
        {
            bool isApproved = TextUtils.ToBoolean(grvBillImportTech.GetFocusedRowCellValue(colStatus));
            if (isApproved == true)
            {
                //string billCode = TextUtils.ToString(grvBillImportTech.GetFocusedRowCellValue(colBillCode));
                //MessageBox.Show(String.Format("Phiếu nhập [{0}] đã được duyệt. Xin vui lòng kiểm tra lại!", billCode), TextUtils.Caption, MessageBoxButtons.OK,
                //        MessageBoxIcon.Question);
                return;
            }
            int approverID = TextUtils.ToInt(grvBillImportTech.GetFocusedRowCellValue(colApprover));
            //EmployeeModel employee = SQLHelper<EmployeeModel>.FindByID(Global.EmployeeID);
            //if (employee == null)
            //{
            //    MessageBox.Show("Mã nhân viên không có trong cơ sở dữ liệu!", TextUtils.Caption, MessageBoxButtons.OK,
            //            MessageBoxIcon.Warning);
            //    return;
            //}
            if (approverID != Global.EmployeeID)
            {
                MessageBox.Show("Bạn không có quyền duyệt phiếu này!", TextUtils.Caption, MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            approved(true);
        }

        //HuyNV 5/11/2022
        void UpdateHistoryProductRTC()
        {
            bool values = TextUtils.ToBoolean(grvBillImportTech.GetFocusedRowCellValue(colStatus));

            for (int i = 0; i < grvDetail.RowCount; i++)
            {
                int ID = TextUtils.ToInt(grvDetail.GetRowCellValue(i, colHistoryProductRTCID));
                if (ID <= 0) continue;
                DateTime dateTime = TextUtils.ToDate3(grvBillImportTech.GetFocusedRowCellValue(colCreatDate));
                TextUtils.ExcuteSQL($"Exec spUpdateHistoryProductRTC {ID},{values},'{dateTime.ToString("yyyy-MM-dd HH:mm:ss")}'");

                int ProductRTCQRCodeID = TextUtils.ToInt(grvDetail.GetRowCellValue(i, colProductRTCQRCodeID));
                if (ProductRTCQRCodeID > 0)
                {
                    if (values)
                    {
                        //Status=1 Trong Kho
                        TextUtils.ExcuteProcedure(StoreProcedures.spUpdateStatusProductRTCQRCode, new string[] { "@ProductRTCQRCodeID", "@Status" }, new object[] { ProductRTCQRCodeID, 1 });

                    }
                    else
                    {
                        //Status=3 Đã xuất
                        TextUtils.ExcuteProcedure(StoreProcedures.spUpdateStatusProductRTCQRCode, new string[] { "@ProductRTCQRCodeID", "@Status" }, new object[] { ProductRTCQRCodeID, 3 });
                    }
                }
            }

            for (int i = 0; i < grvDetail.RowCount; i++)
            {
                int ProductID = TextUtils.ToInt(grvDetail.GetRowCellValue(i, colProductID));
                if (ProductID <= 0) continue;
                ProductRTCModel productRTCModel = (ProductRTCModel)ProductRTCBO.Instance.FindByPK(ProductID);
                if (productRTCModel != null)
                {
                    productRTCModel.Note += " - " + TextUtils.ToString(grvDetail.GetRowCellValue(i, colNote));
                    ProductRTCBO.Instance.Update(productRTCModel);
                }

            }
        }
        void approved(bool isApproved)
        {
            string billCode = TextUtils.ToString(grvBillImportTech.GetFocusedRowCellValue(colBillCode));
            if (MessageBox.Show(string.Format("Bạn có chắc muốn {0} duyệt phiếu [{1}] này ?", isApproved ? "" : "bỏ", billCode), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int ID = TextUtils.ToInt(grvBillImportTech.GetFocusedRowCellValue(colID));
                //string sql = string.Format("UPDATE dbo.BillImportTechnical SET Status = {0} WHERE ID = {1}", isApproved ? 1 : 0, ID);

                BillImportTechnicalModel billImport = SQLHelper<BillImportTechnicalModel>.FindByID(ID);
                billImport.Status = isApproved;
                SQLHelper<BillImportTechnicalModel>.Update(billImport);

                //TextUtils.ExcuteSQL(sql);
                //HuyNV_5/11/2022
                //calculateImport();
                //if (isApproved == true)
                //{
                //    grvBillImportTech.SetFocusedRowCellValue(colStatus, 1);
                //}                
                //else
                //{
                //    grvBillImportTech.SetFocusedRowCellValue(colStatus, 0);
                //}
                //UpdateHistoryProductRTC();
                //Khánh udpate 28/11/2023
                UpdateLog(ID, isApproved);
                loadBillImport();
            }

        }
        //Cập nhật lịch sử nhận chứng từ Khánh update 28/11/2023
        void UpdateLog(int billImportID, bool status)
        {
            BillImportTechnicalLogModel log = new BillImportTechnicalLogModel();
            log.BillImportTechnicalID = billImportID;
            log.StatusBill = status;
            log.DateStatus = DateTime.Now;
            BillImportTechnicalLogBO.Instance.Insert(log);
        }

        private void calculateImport()
        {
            int ID = TextUtils.ToInt(grvBillImportTech.GetFocusedRowCellValue(colID));
            TextUtils.ExcuteProcedure("spCalculateImportTechnical", new string[] { "@ID" }, new object[] { ID });
        }

        private void btnUnapproved_Click(object sender, EventArgs e)
        {
            //bool isApproved = TextUtils.ToBoolean(grvBillImportTech.GetFocusedRowCellValue(colStatus));
            //if (isApproved == false)
            //{
            //    string billCode = TextUtils.ToString(grvBillImportTech.GetFocusedRowCellValue(colBillCode));
            //    MessageBox.Show(String.Format("Phiếu nhập [{0}] chưa được duyệt.", billCode), TextUtils.Caption, MessageBoxButtons.OK,
            //            MessageBoxIcon.Question);
            //    return;
            //}

            bool isApproved = TextUtils.ToBoolean(grvBillImportTech.GetFocusedRowCellValue(colStatus));
            if (isApproved == false)
            {
                //string billCode = TextUtils.ToString(grvBillImportTech.GetFocusedRowCellValue(colBillCode));
                //MessageBox.Show(String.Format("Phiếu nhập [{0}] chưa được duyệt.", billCode), TextUtils.Caption, MessageBoxButtons.OK,
                //        MessageBoxIcon.Question);
                return;
            }
            int approverID = TextUtils.ToInt(grvBillImportTech.GetFocusedRowCellValue(colApprover));
            if (approverID != 54)//ID A QUYỀN
            {
                MessageBox.Show("Bạn không có quyền hủy duyệt phiếu này!", TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                return;
            }
            approved(false);
        }

        private void btnDeleteBillImport_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvBillImportTech.FocusedRowHandle;
            bool isApproved = TextUtils.ToBoolean(grvBillImportTech.GetFocusedRowCellValue(colStatus));
            string billCode = TextUtils.ToString(grvBillImportTech.GetFocusedRowCellValue(colBillCode));
            if (isApproved == true)
            {
                MessageBox.Show(String.Format("Phiếu nhập [{0}] đã được duyệt.\nBạn không thể xóa phiếu này!", billCode), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Question);
                return;
            }
            int ID = TextUtils.ToInt(grvBillImportTech.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            int strID = TextUtils.ToInt(grvBillImportTech.GetFocusedRowCellValue("ID"));
            if (MessageBox.Show(string.Format("Bạn có chắc muốn xóa phiếu nhập [{0}] hay không?", billCode), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var listDetails = SQLHelper<BillImportDetailTechnicalModel>.FindByAttribute("BillImportTechID", strID);


                //BillImportTechnicalBO.Instance.Delete(strID);
                SQLHelper<BillImportTechnicalModel>.DeleteModelByID(strID);
                //BillImportDetailTechnicalBO.Instance.DeleteByAttribute("BillImportTechID", strID);
                SQLHelper<BillImportDetailTechnicalModel>.DeleteByAttribute("BillImportTechID", strID);
                grvBillImportTech.DeleteSelectedRows();
                grvBillImportTech.FocusedRowHandle = focusedRowHandle;
                grvBillImportTech_FocusedRowChanged(null, null);

                //Update trạng thái PO NCC
                //TextUtils.ExcuteProcedure("spUpdateStatusPONCC", new string[] { "@BillImportID", "@UpdatedBy", "@WarehouseType" }, new object[] { ID, Global.LoginName, 0 });

                string poNCCDetailID = string.Join(",", listDetails.Select(x => x.PONCCDetailID));
                TextUtils.ExcuteProcedure("spUpdateStatusPONCC",
                                            new string[] { "@PONCCDetailID", "@UpdatedBy" },
                                            new object[] { poNCCDetailID, Global.LoginName });

            }
            //thêm lịch sử người xóa phiếu
            TextUtils.ExcuteSQL($"Insert into HistoryDeleteBill(BillID,UserID,DeleteDate,Name,TypeBill) values ({ID},{Global.UserID},'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{Global.AppUserName}','{billCode}') ");
            loadBillImport();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            bool isApproved = TextUtils.ToBoolean(grvBillImportTech.GetFocusedRowCellValue(colStatus));
            string billCode = TextUtils.ToString(grvBillImportTech.GetFocusedRowCellValue(colBillCode));

            var focusedRowHandle = grvBillImportTech.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvBillImportTech.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            //BillImportTechnicalModel model = (BillImportTechnicalModel)BillImportTechnicalBO.Instance.FindByPK(ID);
            BillImportTechnicalModel model = SQLHelper<BillImportTechnicalModel>.FindByID(ID);
            frmBillImportTechDetail_New frm = new frmBillImportTechDetail_New(warehouseID);
            if (isApproved)
            {
                frm.IsEdit = true;
            }
            frm.billImport = model;
            frm.IDDetail = ID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadBillImport();

                grvBillImportTech.FocusedRowHandle = focusedRowHandle;
                grvBillImportTech_FocusedRowChanged(null, null);
            }


        }

        private void grvBillImportTech_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        //Phiếu nhập
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            //bool approved = TextUtils.ToBoolean(grvBillImportTech.GetFocusedRowCellValue(colStatus));
            //if (!approved)
            //{
            //    MessageBox.Show("Phiếu phải được duyệt trước khi in.\nVui lòng kiểm tra lại! ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            int ID = TextUtils.ToInt(grvBillImportTech.GetFocusedRowCellValue(colID));
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
            string fileSourceName = "BillImportTechnicalNew.xlsx";

            string sourcePath = Application.StartupPath + "\\" + fileSourceName;
            string billCode = TextUtils.ToString(grvBillImportTech.GetFocusedRowCellValue(colBillCode));
            string currentPath = path + "\\" + billCode + DateTime.Now.ToString("_dd_MM_yyyy_HH_mm_ss") + ".xlsx";
            try
            {
                File.Copy(sourcePath, currentPath, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            //DataTable dt = TextUtils.LoadDataFromSP("spGetBillImportDetailTechnical", "A", new string[] { "@ID" }, new object[] { ID });

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
            {
                //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                Microsoft.Office.Interop.Excel.Application app = default(Microsoft.Office.Interop.Excel.Application);
                Microsoft.Office.Interop.Excel.Workbook workBoook = default(Microsoft.Office.Interop.Excel.Workbook);
                Microsoft.Office.Interop.Excel.Worksheet workSheet = default(Microsoft.Office.Interop.Excel.Worksheet);

                try
                {
                    DateTime dtime = TextUtils.ToDate5(grvBillImportTech.GetFocusedRowCellValue(colCreatDate));
                    //string date = $"Ngày {dtime.Day} Tháng {dtime.Month} Năm {dtime.Year}";
                    string date = $"Hà Nội, Ngày {dtime.Day} tháng {dtime.Month} năm {dtime.Year}";

                    app = new Microsoft.Office.Interop.Excel.Application();
                    app.Workbooks.Open(currentPath);
                    workBoook = app.Workbooks[1];
                    workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBoook.Worksheets[1];

                    //workSheet.Cells[9, 4] = TextUtils.ToString(grvBillImportTech.GetFocusedRowCellValue(colSuplier));
                    //workSheet.Cells[6, 1] = billCode;
                    //workSheet.Cells[8, 4] = TextUtils.ToString(grvBillImportTech.GetFocusedRowCellValue(colDeliver));
                    //workSheet.Cells[22, 7] = TextUtils.ToString(grvBillImportTech.GetFocusedRowCellValue(colReceiver));
                    //workSheet.Cells[15, 7] = date;

                    string suplierName = TextUtils.ToString(grvBillImportTech.GetFocusedRowCellValue(colSuplier)).Trim();
                    string customerName = TextUtils.ToString(grvBillImportTech.GetFocusedRowCellValue(colCustomerName)).Trim();

                    workSheet.Cells[16, 3] = string.IsNullOrEmpty(suplierName) ? customerName : suplierName;
                    workSheet.Cells[15, 2] = $"Số: {billCode}";

                    string departmentName = TextUtils.ToString(grvBillImportTech.GetFocusedRowCellValue(colDepartmentName)).Trim();
                    departmentName = string.IsNullOrEmpty(departmentName) ? "" : $" / Phòng {departmentName}";
                    workSheet.Cells[17, 3] = TextUtils.ToString(grvBillImportTech.GetFocusedRowCellValue(colDeliver)) + departmentName;

                    //if (string.IsNullOrEmpty())
                    //    workSheet.Cells[17, 3] = TextUtils.ToString(grvBillImportTech.GetFocusedRowCellValue(colDeliver));
                    //else
                    //    workSheet.Cells[17, 3] = TextUtils.ToString(grvBillImportTech.GetFocusedRowCellValue(colDeliver)) + " / Phòng " + TextUtils.ToString(grvBillImportTech.GetFocusedRowCellValue(colDepartmentName));
                    workSheet.Cells[16, 7] = TextUtils.ToString(grvBillImportTech.GetFocusedRowCellValue(colReceiver));
                    workSheet.Cells[14, 2] = date;
                    workSheet.Cells[35, 5] = TextUtils.ToString(grvBillImportTech.GetFocusedRowCellValue(colDeliver));

                    #region Qr code phiếu nhập phòng kỹ thuật
                    string qrCodeText = TextUtils.ToString(billCode);
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrCodeText, QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qrCodeData);

                    Bitmap qrCodeBitmap = qrCode.GetGraphic(20, Color.Black, Color.White, true);
                    Bitmap resizedBitmap = new Bitmap(qrCodeBitmap, new Size(250, 250));

                    string tempFilePath = Path.Combine(Application.StartupPath, $"qrcode_{billCode}.png");
                    qrCodeBitmap.Save(tempFilePath);
                    // set vị trí và kích thước 
                    float left = 680;
                    float top = 28;
                    float width = 90;
                    float height = 90;

                    workSheet.Shapes.AddPicture(tempFilePath,
                                                Microsoft.Office.Core.MsoTriState.msoFalse,
                                                Microsoft.Office.Core.MsoTriState.msoCTrue,
                                                left, top, width, height);

                    File.Delete(tempFilePath);
                    #endregion

                    for (int i = grvDetail.RowCount - 1; i >= 0; i--)
                    {
                        //workSheet.Cells[13, 1] = i + 1;
                        //workSheet.Cells[13, 2] = TextUtils.ToString(dt.Rows[i]["ProductCodeRTC"]);
                        //workSheet.Cells[13, 3] = TextUtils.ToString(dt.Rows[i]["ProductCode"]);
                        //workSheet.Cells[13, 4] = TextUtils.ToString(dt.Rows[i]["ProductName"]);
                        //workSheet.Cells[13, 5] = TextUtils.ToString(dt.Rows[i]["UnitName"]);
                        //workSheet.Cells[13, 6] = TextUtils.ToDecimal(dt.Rows[i]["Quantity"]);
                        //workSheet.Cells[13, 7] = TextUtils.ToString(dt.Rows[i]["Maker"]);
                        //workSheet.Cells[13, 8] = TextUtils.ToString(dt.Rows[i]["Note"]);


                        //((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[13]).Insert();

                        workSheet.Cells[24, 2] = i + 1;
                        workSheet.Cells[24, 3] = TextUtils.ToString(grvDetail.GetRowCellValue(i, colSerial));

                        //workSheet.Cells[13, 2] = TextUtils.ToString(dt.Rows[i]["ProductCodeRTC"]);

                        workSheet.Cells[24, 4] = TextUtils.ToString(grvDetail.GetRowCellValue(i, colProductName));
                        workSheet.Cells[24, 5] = TextUtils.ToDecimal(grvDetail.GetRowCellValue(i, colQuantity));
                        workSheet.Cells[24, 6] = TextUtils.ToString(grvDetail.GetRowCellValue(i, colUnit));
                        workSheet.Cells[24, 7] = TextUtils.ToString(grvDetail.GetRowCellValue(i, colMaker));
                        workSheet.Cells[24, 8] = TextUtils.ToString(grvDetail.GetRowCellValue(i, colWarehouseType));
                        workSheet.Cells[24, 9] = TextUtils.ToString(grvDetail.GetRowCellValue(i, colProductCodeRTC));
                        workSheet.Cells[24, 10] = TextUtils.ToString(grvDetail.GetRowCellValue(i, colNote));

                        ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[24]).Insert();
                    }
                    //((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[12]).Delete();
                    //((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[12]).Delete();

                    ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[23]).Delete();
                    ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[23]).Delete();



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
        private void grvBillImportTech_KeyDown(object sender, KeyEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(view.GetFocusedRowCellValue(view.FocusedColumn)));
                e.Handled = true;
            }
        }

        private void grvDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(TextUtils.ToString(grvDetail.GetFocusedRowCellValue(grvDetail.FocusedColumn)));
                e.Handled = true;
            }
        }
        //Khánh update 29/11/2023
        private void btnShowLog_Click(object sender, EventArgs e)
        {
            int billID = TextUtils.ToInt(grvBillImportTech.GetFocusedRowCellValue(colID));
            if (billID <= 0) return;

            frmBillLog frm = new frmBillLog();
            frm.billType = 2;
            frm.billImportID = billID;
            frm.Show();
        }

        private void btnHoSoChungTu_Click(object sender, EventArgs e)
        {
            //var focusedRowHandle = grvBillImportTech.FocusedRowHandle;
            //int ID = TextUtils.ToInt(grvBillImportTech.GetFocusedRowCellValue(colID));
            //string code = TextUtils.ToString(grvBillImportTech.GetFocusedRowCellValue(colBillCode));
            //if (ID == 0) return;

            //frmBillDocumentImportTechnical frm = new frmBillDocumentImportTechnical();
            //frm.billImportTechnicalID = ID;
            //frm.code = code;
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    grvBillImportTech.FocusedRowHandle = focusedRowHandle;
            //    grvBillImportTech_FocusedRowChanged(null, null);
            //}


            var focusedRowHandle = grvBillImportTech.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvBillImportTech.GetFocusedRowCellValue(colID));
            string code = TextUtils.ToString(grvBillImportTech.GetFocusedRowCellValue(colBillCode));
            if (ID == 0) return;
            BillDocumentImportTechnicalModel model = (BillDocumentImportTechnicalModel)BillDocumentImportTechnicalBO.Instance.FindByPK(ID);

            frmBillDocumentImportTechnical frm = new frmBillDocumentImportTechnical();
            frm.BDITModel = model;
            frm.billImportTechnicalID = ID;
            frm.code = code;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                grvBillImportTech.FocusedRowHandle = focusedRowHandle;
                //grvMaster_FocusedRowChanged(null, null);
            }
        }

        private void btnScanBill_Click(object sender, EventArgs e)
        {
            var wh = SQLHelper<WarehouseModel>.FindByID(warehouseID);
            frmBillImportTechQR frm = new frmBillImportTechQR();
            frm.WarehouseCode = wh.WarehouseCode;
            frm.warehouseID = wh.ID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                btnFind_Click(null, null);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmCheckHistoryTech frm = new frmCheckHistoryTech();
            frm.wareHouseId = warehouseID;
            frm.Show();
        }

        private void btnDocumentImportTechnical_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvBillImportTech.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvBillImportTech.GetFocusedRowCellValue(colID));
            string code = TextUtils.ToString(grvBillImportTech.GetFocusedRowCellValue(colBillCode));
            if (ID == 0) return;
            BillDocumentImportTechnicalModel model = (BillDocumentImportTechnicalModel)BillDocumentImportTechnicalBO.Instance.FindByPK(ID);

            frmBillDocumentImportTechnical frm = new frmBillDocumentImportTechnical();
            frm.BDITModel = model;
            frm.billImportTechnicalID = ID;
            frm.code = code;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                grvBillImportTech.FocusedRowHandle = focusedRowHandle;
                //grvMaster_FocusedRowChanged(null, null);
            }
        }
        frmBillImportTechnicalSummary frm;
        private void btnBillImportTechnicalSummary_Click(object sender, EventArgs e)
        {
            try
            {
                if (frm == null || frm.IsDisposed)
                {
                    frm = new frmBillImportTechnicalSummary(warehouseID);
                    //frm.WarehouseCode = WarehouseCode;
                    frm.WarehouseId = warehouseID;
                    frm.WindowState = FormWindowState.Maximized;
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
    }
}
