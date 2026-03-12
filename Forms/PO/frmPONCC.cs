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
using System.Security.AccessControl;


namespace BMS
{
    public partial class frmPONCC : _Forms
    {
        public frmPONCC()
        {
            InitializeComponent();

        }

        private void frmPONCC_Load(object sender, EventArgs e)
        {
            cbTT.SelectedIndex = 3;
            cbChoseExcel.SelectedIndex = 0;
            // ngày bắt đầu khi load form bằng ngày hiện tại trừ đi 1 tháng
            DateTime datenow = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            dtpFromDate.Value = datenow.AddMonths(-1);

            txtPageNumber.Text = "1";
            loadSupplier();
            loadPONCC();
           
        }

        #region Methods
        /// <summary>
        /// load khách hàng
        /// </summary>
        void loadSupplier()
        {
            DataTable dt = TextUtils.Select("SELECT ID, NameNCC FROM SupplierSale ");
            cboSupplier.Properties.DisplayMember = "NameNCC";
            cboSupplier.Properties.ValueMember = "ID";
            cboSupplier.Properties.DataSource = dt;
        }
        private void loadPONCC()
        {
            DateTime dateTimeS = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            DateTime dateTimeE = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);

            DataSet oDataSet = TextUtils.LoadDataSetFromSP("spGetPONCC_Khanh"
                , new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@FilterText", "@SupplierID", "@TrangThai" }
                , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), dateTimeS, dateTimeE, txtFilterText.Text.Trim(), TextUtils.ToInt(cboSupplier.EditValue), cbTT.SelectedIndex });
            grdMaster.DataSource = oDataSet.Tables[0];

            if (oDataSet.Tables.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(oDataSet.Tables[1].Rows[0]["TotalPage"]);

            loadPONCCDetail();
        }

        void loadPONCCDetail()
        {
            //if (grvMaster.RowCount <= 0) return;
            int IDMaster = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            int Currency = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colCurrency));
            if (Currency == 0) grvDetail.Columns["CurrencyExchange"].Visible = false;
            DataTable dt = TextUtils.LoadDataFromSP("spGetPONCCDetail", "A", new string[] { "@PONCCID" }, new object[] { IDMaster });
            grdDetail.DataSource = dt;
        }
        #endregion

        #region Buttons Events
        /// <summary>
        /// click button tạo nhóm sản phẩm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            frmPONCCDetail frm = new frmPONCCDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadPONCC();
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvMaster.FocusedRowHandle;

            int IDMaster = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (IDMaster == 0) return;
            PONCCModel model = (PONCCModel)PONCCBO.Instance.FindByPK(IDMaster);
            frmPONCCDetail frm = new frmPONCCDetail();
            frm.oPONCC = model;
            frm.IDDetail = IDMaster;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadPONCC();

                grvMaster.FocusedRowHandle = focusedRowHandle;
                grvMaster_FocusedRowChanged(null, null);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvMaster.FocusedRowHandle;

            if (grvMaster.RowCount <= 0) return;
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproved));
            string poCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colPOCode));
            if (isApproved == true)
            {
                MessageBox.Show(String.Format("Bạn không thể xóa PO [{0}] này? Xin vui lòng kiểm tra lại.", poCode), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Question);
                return;
            }

            int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue("ID"));
            if (ID == 0) return;
            if (MessageBox.Show(string.Format("Bạn có muốn xóa PO [{0}] hay không ?", poCode), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                PONCCBO.Instance.Delete(ID);
                PONCCDetailBO.Instance.DeleteByAttribute("PONCCID", ID);
                //Update lại NgayDatHang,NgayDukienVe,IsApproved_Level1 trong RequetBuyRTC
                for (int i = 0; i < grvDetail.RowCount; i++)
                {
                    int RequestBuyRTCID = TextUtils.ToInt(grvDetail.GetRowCellValue(i, colRequestBuyRTCID));
                    TextUtils.ExcuteSQL($"exec spDateOrderUpdateRequestBuyRTC {RequestBuyRTCID}");
                }
                grvMaster.DeleteSelectedRows();
            }
            //if (grvMaster.RowCount == 0)
            //{
            //    grvMaster.FocusedRowHandle = focusedRowHandle;
            //}
            //else
            //{
            //    grvMaster.FocusedRowHandle = focusedRowHandle + 1;
            //}
        }
        /// <summary>
        /// Không dùng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
         /*
       void UpdateRequestBuyRTC(bool values)
       {
           if(values)
           {
               for (int i = 0; i < grvDetail.RowCount; i++)
               {
                   //Update RequestBuyRTC
                   int RequestBuyRTCID = TextUtils.ToInt(grvDetail.GetRowCellValue(i, colRequestBuyRTCID));
                   if (RequestBuyRTCID > 0)
                   {
                       RequestBuyRTCModel buyRTCModel = (RequestBuyRTCModel)RequestBuyRTCBO.Instance.FindByPK(RequestBuyRTCID);
                       int QtyReal = buyRTCModel.QtyReal;
                       //buyRTCModel.QtyReal = QtyReal + TextUtils.ToInt(grvDetail.GetRowCellValue(i, colQtyReal));
                       buyRTCModel.PONCCID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
                       if (buyRTCModel.QtyReal == buyRTCModel.Qty)
                       {
                           buyRTCModel.IsApproved_Level1 = true;
                       }

                       RequestBuyRTCBO.Instance.Update(buyRTCModel);

                   }
               }
           }
           else
           {
               for (int i = 0; i < grvDetail.RowCount; i++)
               {

                   //Update RequestBuyRTC
                   int RequestBuyRTCID = TextUtils.ToInt(grvDetail.GetRowCellValue(i, colRequestBuyRTCID));
                   if (RequestBuyRTCID > 0)
                   {
                       // buyRTCModel.SupplierID = TextUtils.ToInt();
                       //int qtyreal = TextUtils.ToInt(grvDetail.GetRowCellValue(i, colQtyReal));
                      // TextUtils.ExcuteSQL($"exec spUpdateRequestBuyRTCbyPONCCDetail {RequestBuyRTCID},{qtyreal}");
                   }
               }
           }

       

    }*/
        private void btnIsApproved_Click(object sender, EventArgs e)
        {
            if (grvMaster.RowCount <= 0) return;
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproved));
            if (isApproved == true)
            {
                string billCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colPOCode));
                MessageBox.Show(String.Format("PO [{0}] đã được duyệt. Xin vui lòng kiểm tra lại!", billCode), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Question);
                return;
            }

            approved(true);
            //grvMaster.SetFocusedRowCellValue(colIsApproved, 1);

        }

        private void btnCancelApproved_Click(object sender, EventArgs e)
        {
            if (grvMaster.RowCount <= 0) return;
            bool isApproved = TextUtils.ToBoolean(grvMaster.GetFocusedRowCellValue(colIsApproved));
            if (isApproved == false)
            {
                string billCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colPOCode));
                MessageBox.Show(String.Format("PO [{0}] chưa được duyệt. Xin vui lòng kiểm tra lại!", billCode), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Question);
                return;

            }
            approved(false);
            // grvMaster.SetFocusedRowCellValue(colIsApproved, 0);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            loadPONCC();
        }
        #endregion

        void approved(bool isApproved)
        {
            if (grvMaster.RowCount > 0)
            {
                PONCCModel _BillXuat = new PONCCModel();
                string billCode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colPOCode));
                if (MessageBox.Show(string.Format("Bạn có chắc muốn {0} duyệt phiếu {1} không?", isApproved ? "" : "bỏ", billCode), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int ID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
                    string sql = string.Format("UPDATE dbo.PONCC SET IsApproved = {0} WHERE ID = {1}", isApproved ? 1 : 0, ID);
                    TextUtils.ExcuteSQL(sql);
                    grvMaster.SetFocusedRowCellValue(colIsApproved, isApproved);
                    //UpdateRequestBuyRTC(isApproved);
                }
            }

        }

        private void txtFilterText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loadPONCC();
            }
        }

        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            loadPONCCDetail();
        }

        private void grvMaster_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) > int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            loadPONCC();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = int.Parse(txtPageNumber.Text) > 1 ? (int.Parse(txtPageNumber.Text) - 1).ToString() : "1";
            loadPONCC();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = (int.Parse(txtPageNumber.Text) + 1).ToString();
            loadPONCC();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtPageNumber.Text) >= int.Parse(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            loadPONCC();
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {
            txtPageNumber.Text = "1";
            loadPONCC();
        }

        private void grvDetail_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == colVat)
            {
                e.DisplayText = TextUtils.ToDecimal(e.Value) + "%";
            }
        }

        public double MeasureTextHeight(string text,  Excel.Font font, double width)
        {
            var bitmap = new Bitmap(1, 1);
            var graphics = Graphics.FromImage(bitmap);

            var pixelWidth = Convert.ToInt32(width * 7);  //7 pixels per excel column width
            var fontSize =font.Size * 1.01f;
            var drawingFont = new Font(font.Name, fontSize);
            var size = graphics.MeasureString(text, drawingFont, pixelWidth, new StringFormat { FormatFlags = StringFormatFlags.MeasureTrailingSpaces });

            //72 DPI and 96 points per inch.  Excel height in points with max of 409 per Excel requirements.
            return Math.Min(Convert.ToDouble(size.Height) * 72 / 96, 409);
        }

        //27092022
        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (cbChoseExcel.SelectedIndex == 1)
            {
                Excel_ENG();
            }
            else
            {
                Excel_VN();
            }


        }

        void Excel_VN()
        {
            int IDMaster = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (IDMaster == 0) return;
            int SupplierID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colSupplierID));
            int EmployeeID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colEmployeeIDID));
            int Company = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colCompany));

            DataTable dtNCC = TextUtils.Select($"Select * from SupplierSale where ID={SupplierID}");
            //DataTable dtEmployee = TextUtils.Select($"exec spGetEmployeePOContactByEmployeeID {EmployeeID},{Company}");
            DataTable dtEmployee = TextUtils.LoadDataFromSP("spGetEmployeePOContactByEmployeeID","A", new string[] { "@UserID", "@Company" }, new object[] { EmployeeID, Company });
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
            string fileSourceName = @"PONCC\MauPONCC.xls";

            string sourcePath = Application.StartupPath + "\\" + fileSourceName;
            string phieucode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colPOCode));
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

            // DataTable dtMaster = TextUtils.LoadDataFromSP("spGetExportExcel", "A", new string[] { "@ID" }, new object[] { IDMaster });

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                Excel.Application app = new Excel.Application();
                Excel.Workbook workBoook =default(Excel.Workbook); 
                Excel.Worksheet workSheet = new Excel.Worksheet();
                
                try
                {
                    app = new Excel.Application();
                    app.Workbooks.Open(currentPath);
                    //Các cty khác nhau có sheet khác nhau
                    workBoook = app.Workbooks[1];
                    workSheet = Company == 0 ? (Excel.Worksheet)workBoook.Worksheets[1] :
                               Company == 1 ? (Excel.Worksheet)workBoook.Worksheets[2] : (Excel.Worksheet)workBoook.Worksheets[3];

                    
                    //??????//
                    // workSheet.Cells[4, 9] = TextUtils.ToString(dtNCC.Rows[0]["CodeNCC"]);
                    workSheet.Cells[7, 16] = TextUtils.ToString(dtNCC.Rows[0]["NameNCC"]);
                    workSheet.Cells[7, 42] = DateTime.Now.ToString("dd/MM/yyyy");
                    //Chỗ này là DMMH
                    workSheet.Cells[8, 41] = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colPOCode));
                    workSheet.Cells[8, 4] = TextUtils.ToString(dtNCC.Rows[0]["AddressNCC"]);
                    workSheet.Cells[11, 9] = TextUtils.ToString(dtNCC.Rows[0]["MaSoThue"]);
                    // workSheet.Cells[9, 2] = TextUtils.ToString(dtNCC.Rows[0]["NVPhuTrach"]);
                    workSheet.Cells[12, 8] = TextUtils.ToString(dtNCC.Rows[0]["PhoneNCC"]);
                    workSheet.Cells[14, 6] = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colNote));



                    ////Ngày giao hàng
                    DateTime dt = TextUtils.ToDate3(grvMaster.GetFocusedRowCellValue(colDeliveryDate));
                    workSheet.Cells[24, 14] = dt.ToString("dd/MM/yyyy");
                    workSheet.Cells[25, 19] = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colAddressDelivery));
                    workSheet.Cells[27, 21] = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colRulePay));
                    workSheet.Cells[28, 11] ="Ngân hàng:"+TextUtils.ToString(dtNCC.Rows[0]["NganHang"]) +"STK:"+TextUtils.ToString(dtNCC.Rows[0]["SoTK"]);
                    //Thông tin liên hệ
                    if (dtEmployee.Rows.Count > 0)
                    {
                        workSheet.Cells[37, 27] = "Phone:" + dtEmployee.Rows[0]["Phone"] + "\n" + "Email:" + dtEmployee.Rows[0]["Email"];
                    }
                    double thanhtien = 0;
                    double total = 0;
                    double vatMoney = 0;
                    for (int i = 0; i < grvDetail.RowCount; i++)
                    {
                        thanhtien += TextUtils.ToDouble(grvDetail.GetRowCellValue(i, colThanhTien));
                        total += TextUtils.ToDouble(grvDetail.GetRowCellValue(i, coltotalPrice));
                        vatMoney += TextUtils.ToDouble(grvDetail.GetRowCellValue(i, colVatMoney));
                    }
                     
                    string vat= TextUtils.ToString(grvDetail.GetFocusedRowCellValue(colVat));
                    //tiền trc vat
                    workSheet.Cells[19, 38] = thanhtien;
                    workSheet.Cells[20, 38] = vatMoney;
                    workSheet.Cells[20, 23] = $"'{vat.ToString()}%";
                    workSheet.Cells[21, 38] = total;
                   


                    //TienBangChu
                    string numberletter=ConvertnumbersToLetters.So_chu_VN(total);
                    //MessageBox.Show(numberletter);
                    workSheet.Cells[22, 20] = numberletter;
                    ///Chua xong
                    ///

                    for (int i = 0; i < grvDetail.RowCount; i++)
                    {
                       // ((Excel.Range)workSheet.Rows[18]).Rows.AutoOutline();
                        //workSheet.Cells[18, 1] = i + 1;
                        workSheet.Cells[18, 1] = TextUtils.ToString(grvDetail.GetRowCellValue(i, colProductCode_));
                        workSheet.Cells[18, 12] = TextUtils.ToString(grvDetail.GetRowCellValue(i, colProductName_));
                        workSheet.Cells[18, 32] = TextUtils.ToString(grvDetail.GetRowCellValue(i, colUnit));
                        workSheet.Cells[18, 33] = TextUtils.ToInt(grvDetail.GetRowCellValue(i, colQtyRequest));
                        workSheet.Cells[18, 35] = TextUtils.ToInt(grvDetail.GetRowCellValue(i, colPrice));
                        workSheet.Cells[18, 42] = TextUtils.ToInt(grvDetail.GetRowCellValue(i, colThanhTien));
                        string s = TextUtils.ToString(grvDetail.GetRowCellValue(i, colProductName_));
                        decimal valu = s.Length;
                        decimal ss = valu / 71;
                        if(ss>1)
                        {
                            ((Excel.Range)workSheet.Cells[18, 12]).RowHeight = ss * 20+20;
                        }    
                       

                        //Tongtien += TextUtils.ToDecimal(grvDetail.GetRowCellValue(i, coltotalPrice));
                        ((Excel.Range)workSheet.Rows[18]).Insert(Excel.XlInsertShiftDirection.xlShiftDown, ((Excel.Range)workSheet.Rows[18]).Copy());
                       
                    }


                    // ((Excel.Range)workSheet.Rows[15]).Delete();
                    ((Excel.Range)workSheet.Rows[18]).Delete();
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
        void Excel_ENG()
        {
            int IDMaster = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colIDMaster));
            if (IDMaster == 0) return;
            int SupplierID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colSupplierID));
            int EmployeeID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colEmployeeIDID));
            int Company = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colCompany));

            DataTable dtNCC = TextUtils.Select($" exec spGetSunplierSaleAndSupplierSaleContact { SupplierID}");
            DataTable dtEmployee = TextUtils.Select($"exec spGetEmployeePOContactByEmployeeID {EmployeeID},{Company}");
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
            //12/10/2022
            string fileSourceName = @"PONCC\MauPONCC.xls";

            string sourcePath = Application.StartupPath + "\\" + fileSourceName;
            string phieucode = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colPOCode));
            string currentPath = path + "\\" + phieucode + DateTime.Now.ToString("_dd_MM_yyyy_HH_mm_ss") + ".xls";
            try
            {
                File.Copy(sourcePath, currentPath, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi tạo phiếu!" + Environment.NewLine + ex.Message,TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            // DataTable dtMaster = TextUtils.LoadDataFromSP("spGetExportExcel", "A", new string[] { "@ID" }, new object[] { IDMaster });

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
                    //Các cty khác nhau có sheet khác nhau
                    workBoook = app.Workbooks[1];
                    workSheet = Company == 0 ? (Excel.Worksheet)workBoook.Worksheets[4] :
                                Company == 1 ? (Excel.Worksheet)workBoook.Worksheets[5] : (Excel.Worksheet)workBoook.Worksheets[6];

                    // workSheet = (Excel.Worksheet)workBoook.Worksheets[4];
                    //??????//
                    // workSheet.Cells[4, 9] = TextUtils.ToString(dtNCC.Rows[0]["CodeNCC"]);
                    workSheet.Cells[2, 24] = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colPOCode));
                    workSheet.Cells[5, 12] = TextUtils.ToString(dtNCC.Rows[0]["NameNCC"]);
                    workSheet.Cells[5, 40] = DateTime.Now.ToString("dd/MM/yyyyy");
                    //workSheet.Cells[8, 41] = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colPOCode));
                    workSheet.Cells[6, 7] = TextUtils.ToString(dtNCC.Rows[0]["AddressNCC"]);
                    workSheet.Cells[8, 8] = TextUtils.ToString(dtNCC.Rows[0]["MaSoThue"]);
                    //typemoney
                    int type = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colCurrency));
                    string typevalues =type == 0 ? "VND" :
                                       type == 1 ? "USD" :
                                       type == 2 ? "EUR" :
                                       type == 3 ? "RMB" : "JPY";

                    workSheet.Cells[7, 43] = typevalues;
                    workSheet.Cells[10, 13] = TextUtils.ToString(dtNCC.Rows[0]["SupplierName"]);
                    workSheet.Cells[13, 14] = TextUtils.ToString(dtNCC.Rows[0]["SupplierPhone"]);
                    workSheet.Cells[13, 26] = TextUtils.ToString(dtNCC.Rows[0]["SupplierEmail"]);
                    //EmployeePOContact
                    if (dtEmployee.Rows.Count > 0)
                    {
                        workSheet.Cells[17, 1] = "Purchasing Manager: Ms." + dtEmployee.Rows[0]["FullName"] + " - Phone:" + dtEmployee.Rows[0]["Phone"] + " -Email:" + dtEmployee.Rows[0]["Email"];
                    }
                    workSheet.Cells[18, 10] = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colNote));
                    ////Ngày giao hàng
                    DateTime dt = TextUtils.ToDate3(grvMaster.GetFocusedRowCellValue(colDeliveryDate));
                    workSheet.Cells[29, 11] = dt.ToString("dd/MM/yyyy");
                    workSheet.Cells[30, 12] = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colAddressDelivery));
                    workSheet.Cells[31, 4] = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colRulePay));
                    workSheet.Cells[32, 12] = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colBankingFee));
                    workSheet.Cells[33, 13] = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colFedexAccount));
                    workSheet.Cells[35, 13] ="Bank:"+TextUtils.ToString(dtNCC.Rows[0]["NganHang"])+"\n"+
                                             "Bank account number"+ TextUtils.ToString(dtNCC.Rows[0]["SoTK"]);//Bank Account



                    double thanhtien = 0;
                    double total = 0;
                    double vatMoney = 0;
                    for (int i = 0; i < grvDetail.RowCount; i++)
                    {
                        thanhtien += TextUtils.ToDouble(grvDetail.GetRowCellValue(i, colThanhTien));
                        total += TextUtils.ToDouble(grvDetail.GetRowCellValue(i, coltotalPrice));
                        vatMoney += TextUtils.ToDouble(grvDetail.GetRowCellValue(i, colVatMoney));
                    }

                    workSheet.Cells[23, 36] = thanhtien;
                    //workSheet.Cells[20, 38] = (thanhtien * 0.1);
                    workSheet.Cells[25, 30] = thanhtien;
                    if (dtEmployee.Rows.Count > 0)
                    {
                        workSheet.Cells[47, 18] = dtEmployee.Rows[0]["FullName"];
                    }
                    
                    //string thanhtienstring =thanhtien.ToString();
                    //int index = thanhtienstring.Length;
                    ////Convert numbers To array int
                    //int[] thanhtien_number= new int[index];
                    //for (int i = 0; i <thanhtienstring.Length; i++)
                    //{
                    //    thanhtien_number[i] =TextUtils.ToInt(thanhtienstring.Substring(i, 1));
                    //}
                    if (type==1)
                    {
                        string number = ConvertnumbersToLetters_ENG.MoneyToWords(thanhtien)+ " Dollars";
                        //MessageBox.Show(thanhtien);
                        workSheet.Cells[26, 15] = number;
                    }    
                    else if(type==2)
                    {
                        string number = ConvertnumbersToLetters_ENG.MoneyToWords(thanhtien)+ " Euro";
                        //MessageBox.Show(thanhtien);
                        workSheet.Cells[26, 15] = number;
                    }    
                    else if(type==3)
                    {
                        string number = ConvertnumbersToLetters_JAPAN.generateNumber(thanhtien) +" 元";
                        //MessageBox.Show(thanhtien);
                        workSheet.Cells[26, 15] = number;
                    }    
                    else if(type==4)
                    {
                        string number = ConvertnumbersToLetters_JAPAN.generateNumber(thanhtien) + " 円";
                        workSheet.Cells[26,15] = number;
                    }    
                    for (int i = 0; i < grvDetail.RowCount; i++)
                    {

                        workSheet.Cells[21, 1] = grvDetail.RowCount - i;
                        workSheet.Cells[21, 3] = TextUtils.ToString(grvDetail.GetRowCellValue(i, colProductName_));
                        //workSheet.Cells[18, 12] = TextUtils.ToString(grvDetail.GetRowCellValue(i, colProductName_));
                        workSheet.Cells[21, 28] = TextUtils.ToString(grvDetail.GetRowCellValue(i, colUnit));
                        workSheet.Cells[21, 31] = TextUtils.ToInt(grvDetail.GetRowCellValue(i, colQtyRequest));
                        workSheet.Cells[21, 35] = TextUtils.ToInt(grvDetail.GetRowCellValue(i, colPrice));//.ToString("G29");
                        workSheet.Cells[21, 42] = TextUtils.ToInt(grvDetail.GetRowCellValue(i, colThanhTien));//.ToString("G29");
                        string s = TextUtils.ToString(grvDetail.GetRowCellValue(i, colProductName_));
                        decimal valu = s.Length;
                        decimal ss = valu /53;
                        if (ss > 1)
                        {
                            ((Excel.Range)workSheet.Cells[21,3]).RowHeight = ss * 20 + 20;
                        }
                        //Tongtien += TextUtils.ToDecimal(grvDetail.GetRowCellValue(i, coltotalPrice));
                        ((Excel.Range)workSheet.Rows[21]).Insert(Excel.XlInsertShiftDirection.xlShiftDown, ((Excel.Range)workSheet.Rows[21]).Copy());

                    }
                    // ((Excel.Range)workSheet.Rows[15]).Delete();
                    ((Excel.Range)workSheet.Rows[21]).Delete();
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

        private void bntEditExcel_Click(object sender, EventArgs e)
        {
            string path_=Application.ExecutablePath.ToString();
            int index = path_.LastIndexOf(@"\");

            string newstring = path_.Substring(0,index) +@"\PONCC";

            //string path = @"D:\Huy\RTC_280622\RTC_300822\RTC\BMS\bin\x86\Debug\PONCC";
            // Directory.CreateDirectory($@"{path}\DanhMucVatTu");
            try
            {
                Process.Start(newstring);
            }
            catch (Exception)
            {

                MessageBox.Show("Không tìm thấy đường dẫn phù hợp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            return;

        }

        private void grvMaster_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column == colStatusText)
            {
                if (TextUtils.ToInt(grvMaster.GetRowCellValue(e.RowHandle, colStatus))==0)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 255, 128);
                }
                else if(TextUtils.ToInt(grvMaster.GetRowCellValue(e.RowHandle, colStatus)) == 1)
                {
                    e.Appearance.BackColor = Color.FromArgb(128, 255, 128); 
                }
                else if(TextUtils.ToInt(grvMaster.GetRowCellValue(e.RowHandle, colStatus)) == 2)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                }    

            }
        }

        private void grdMaster_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }
    }
}


