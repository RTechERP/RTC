using BMS;
using BMS.Business;
using BMS.Model;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using Forms.Classes;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Forms.Technical
{
    public partial class frmBillExportTechnical_New : _Forms
    {

        public int warehouseID;
        public frmBillExportTechnical_New()
        {
            InitializeComponent();
        }
        public frmBillExportTechnical_New(int WarehouseID)
        {
            InitializeComponent();
            warehouseID = WarehouseID;
        }
        private void frmBillExportTechnical_Load(object sender, EventArgs e)
        {
            DateTime datenow = new DateTime(dtpDS.Value.Year, dtpDS.Value.Month, dtpDS.Value.Day, 0, 0, 0);
            dtpDS.Value = datenow.AddMonths(-1);

            cboStatus.SelectedIndex = 2;
            txtPageNumber.Text = "1";
            loadBillExport();
            this.Text += warehouseID == 1 ? " - HN" : (warehouseID == 2 ? " - HCM" : " - BN");
            LoadProject();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmBillExportTechDetail_New frm = new frmBillExportTechDetail_New(warehouseID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadBillExport();
            }
        }
        void LoadProject()
        {
            List<ProjectModel> lst = SQLHelper<ProjectModel>.FindAll();
            cboProjectID.DataSource = lst;
            cboProjectID.DisplayMember = "ProjectCode";
            cboProjectID.ValueMember = "ID";
        }
        void loadBillExport()
        {
            DateTime dateTimeS = new DateTime(dtpDS.Value.Year, dtpDS.Value.Month, dtpDS.Value.Day, 0, 0, 0);
            DateTime dateTimeE = new DateTime(dtpDE.Value.Year, dtpDE.Value.Month, dtpDE.Value.Day, 23, 59, 59);

            DataSet dataSet = new DataSet();
            dataSet = TextUtils.LoadDataSetFromSP("spGetBillExportTechnical"
                , new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@Status", "@FilterText", "@WarehouseID" }
                , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), dateTimeS, dateTimeE, cboStatus.SelectedIndex, txtKeyword.Text, warehouseID });
            grdBillExportTech.DataSource = dataSet.Tables[0];
            if (dataSet.Tables.Count == 0) return;
            txtTotalPage.Text = TextUtils.ToString(dataSet.Tables[1].Rows[0]["TotalPage"]);

            loadBillExportDetail();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = grvBillExportTech.FocusedRowHandle;
            grvBillExportTech.FocusedRowHandle = focusedRowHandle - 1;
            loadBillExport();
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvBillExportTech.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvBillExportTech.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            //BillExportTechnicalModel model = (BillExportTechnicalModel)BillExportTechnicalBO.Instance.FindByPK(ID);
            BillExportTechnicalModel model = SQLHelper<BillExportTechnicalModel>.FindByID(ID);
            frmBillExportTechDetail_New frm = new frmBillExportTechDetail_New(warehouseID);
            if (TextUtils.ToBoolean(grvBillExportTech.GetFocusedRowCellValue(colStatus)))
            {
                frm.IsEdit = true;
            }
            frm.billExport = model;
            frm.IDDetail = ID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadBillExport();

                grvBillExportTech.FocusedRowHandle = focusedRowHandle;
                grvBillExportTech_FocusedRowChanged(null, null);
            }
        }

        /// <summary>
        /// load bảng Detail
        /// </summary>
        void loadBillExportDetail()
        {
            int ID = TextUtils.ToInt(grvBillExportTech.GetFocusedRowCellValue(colID));
            DataTable dt = TextUtils.LoadDataFromSP(StoreProcedures.spGetBillExportTechDetail_New, "A", new string[] { "@Id" }, new object[] { ID });
            grdDetail.DataSource = dt;
        }

        private void grvBillExportTech_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            loadBillExportDetail();
        }

        private void grvBillExportTech_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "BillType")
            {
                if (TextUtils.ToBoolean(e.Value) == false)
                {
                    e.DisplayText = "Trả";
                }
                else
                {
                    e.DisplayText = "Cho mượn";
                }
            }
            //if (e.Column.FieldName == "Status")
            //{
            //    if (TextUtils.ToInt(e.Value) == 0)
            //    {
            //        e.DisplayText = "Chưa duyệt";
            //    }
            //    else
            //    {
            //        e.DisplayText = "Đã duyệt";
            //    }
            //}
        }

        private void grvBillExportTech_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvBillExportTech.FocusedRowHandle;
            bool isApproved = TextUtils.ToBoolean(grvBillExportTech.GetFocusedRowCellValue(colStatus));
            bool AddHistoryProduct = TextUtils.ToBoolean(grvBillExportTech.GetFocusedRowCellValue(colCheckAddHistoryProductRTC));
            string billCode = TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colCode));
            if (isApproved == true)
            {
                MessageBox.Show(String.Format("Bạn không thể xóa phiếu xuất [{0}]? Xin vui lòng kiểm tra lại.", billCode), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Question);
                return;
            }
            int ID = TextUtils.ToInt(grvBillExportTech.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            int strID = TextUtils.ToInt(grvBillExportTech.GetFocusedRowCellValue("ID"));
            if (MessageBox.Show(string.Format("Bạn có muốn xóa phiếu xuất [{0}] hay không ?", billCode), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //huyNV 5/11/2022
                ////Update lại HistoryProductRTC trước
                //if (AddHistoryProduct)
                //{
                //    //Xoá bỏ các phiếu mượn đã tạo
                //    HistoryProductRTCBO.Instance.DeleteByAttribute("BillExportTechnicalID", strID);//Xoá nếu các phiếu mượn này được tạo từ phiếu xuất
                //}
                //else
                //{
                //    for (int i = 0; i < grvDetail.RowCount; i++)
                //    {
                //        grvBillExportTech.Focus();
                //        int HistoryRTCID = TextUtils.ToInt(grvDetail.GetRowCellValue(i, colHistoryProductRTCID));
                //        if (HistoryRTCID <= 0) return;
                //        HistoryProductRTCModel oHistoryModel = (HistoryProductRTCModel)HistoryProductRTCBO.Instance.FindByPK(HistoryRTCID);
                //        if (oHistoryModel == null) return;
                //        //oHistoryModel.Note = "";
                //        oHistoryModel.BillExportTechnicalID = 0;
                //        HistoryProductRTCBO.Instance.Update(oHistoryModel);
                //    }

                //}

                BillExportTechnicalBO.Instance.Delete(strID);
                BillExportDetailTechnicalBO.Instance.DeleteByAttribute("BillExportTechID", strID);
                string sql = $"UPDATE HistoryProductRTC SET IsDelete = 1 WHERE BillExportTechnicalID = {strID}";
                TextUtils.ExcuteSQL(sql);

                grvBillExportTech.DeleteSelectedRows();
                grvBillExportTech.FocusedRowHandle = focusedRowHandle;
                grvBillExportTech_FocusedRowChanged(null, null);




            }
            //thêm lịch sử người xóa phiếu
            TextUtils.ExcuteSQL($"Insert into HistoryDeleteBill(BillID,UserID,DeleteDate,Name,TypeBill) values ({ID},{Global.UserID},'{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{Global.AppUserName}','{billCode}') ");
            loadBillExport();

        }

        private void btnApproved_Click(object sender, EventArgs e)
        {
            bool isApproved = TextUtils.ToBoolean(grvBillExportTech.GetFocusedRowCellValue(colStatus));
            if (isApproved == true)
            {
                //string billCode = TextUtils.ToString(grvBillImportTech.GetFocusedRowCellValue(colBillCode));
                //MessageBox.Show(String.Format("Phiếu nhập [{0}] đã được duyệt. Xin vui lòng kiểm tra lại!", billCode), TextUtils.Caption, MessageBoxButtons.OK,
                //        MessageBoxIcon.Question);
                return;
            }
            int approverID = TextUtils.ToInt(grvBillExportTech.GetFocusedRowCellValue(colApprover));
            //EmployeeModel employee = SQLHelper<EmployeeModel>.FindByID(Global.EmployeeID);
            //if (employee == null)
            //{
            //    MessageBox.Show("Mã nhân viên không có trong cơ sở dữ liệu!", TextUtils.Caption, MessageBoxButtons.OK,
            //            MessageBoxIcon.Warning);
            //    return;
            //}
            if (approverID != Global.EmployeeID)
            {
                MessageBox.Show("Bạn không có quyền duyệt phiếu này!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            approved(true);
        }

        private void btnUnapproved_Click(object sender, EventArgs e)
        {
            bool isApproved = TextUtils.ToBoolean(grvBillExportTech.GetFocusedRowCellValue(colStatus));
            if (isApproved == false)
            {
                //string billCode = TextUtils.ToString(grvBillImportTech.GetFocusedRowCellValue(colBillCode));
                //MessageBox.Show(String.Format("Phiếu nhập [{0}] chưa được duyệt.", billCode), TextUtils.Caption, MessageBoxButtons.OK,
                //        MessageBoxIcon.Question);
                return;
            }
            int approverID = TextUtils.ToInt(grvBillExportTech.GetFocusedRowCellValue(colApprover));
            if (approverID != 54) //ID A Quyền
            {
                MessageBox.Show("Bạn không có quyền hủy duyệt phiếu này!", TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                return;
            }
            approved(false);
        }

        void InsertOrDeleteHistoryProductRTC()
        {
            int MasterID = TextUtils.ToInt(grvBillExportTech.GetFocusedRowCellValue(colID));
            bool CkeckAdd = TextUtils.ToBoolean(grvBillExportTech.GetFocusedRowCellValue(colCheckAddHistoryProductRTC));//Có thêm phiếu mượn hay không
            bool Status = TextUtils.ToBoolean(grvBillExportTech.GetFocusedRowCellValue(colStatus));

            List<int> listHistoryID = new List<int>();

            //Thêm phiếu mượn
            for (int i = 0; i < grvDetail.RowCount; i++)
            {
                //Lt.Anh update add danh sách id lịch sử mượn vào list
                int historyID = TextUtils.ToInt(grvDetail.GetRowCellValue(i, colHistoryProductRTCID));
                if (historyID > 0)
                {
                    listHistoryID.Add(historyID);
                }


                int ProductRTCQRCodeID = 0;// TextUtils.ToInt(grvDetail.GetRowCellValue(i, colProductRTCQRCodeID));
                int detailID = TextUtils.ToInt(grvDetail.GetRowCellValue(i, colIdDetail));
                if (Status)
                {
                    if (CkeckAdd)
                    {
                        //
                        DataTable dt = TextUtils.LoadDataFromSP("spGetBillExportTechDetailSerial", "A", new string[] { "@BillExportTechDetailID", "@WarehouseID" }, new object[] { detailID, warehouseID });
                        if (dt.Rows.Count > 0)
                        {
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                HistoryProductRTCModel oHistoryModel = new HistoryProductRTCModel();
                                oHistoryModel.ProductRTCQRCode = TextUtils.ToString(dt.Rows[j]["SerialNumber"]);
                                oHistoryModel.ProductRTCID = TextUtils.ToInt(grvDetail.GetRowCellValue(i, colProductID));
                                oHistoryModel.DateBorrow = TextUtils.ToDate5(grvBillExportTech.GetFocusedRowCellValue(colCreatDate));
                                oHistoryModel.DateReturnExpected = TextUtils.ToDate5(grvBillExportTech.GetFocusedRowCellValue(colExpectedDate));
                                oHistoryModel.PeopleID = TextUtils.ToInt(grvBillExportTech.GetFocusedRowCellValue(colReceiverID));
                                oHistoryModel.Note += "Phiếu xuất " + grvBillExportTech.GetFocusedRowCellValue(colCode).ToString() + " - " + grvDetail.GetRowCellValue(i, colNote).ToString();
                                oHistoryModel.Project = TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colProjectName));
                                oHistoryModel.Status = 1;
                                oHistoryModel.BillExportTechnicalID = MasterID;
                                oHistoryModel.NumberBorrow = 1;// TextUtils.ToDecimal(grvDetail.GetRowCellValue(i, colQuantity));
                                oHistoryModel.WarehouseID = warehouseID;
                                HistoryProductRTCBO.Instance.Insert(oHistoryModel);

                                //update status trong bảng ProductRTCQRCode status=3(Xuất Kho)
                                if (ProductRTCQRCodeID > 0)
                                {
                                    TextUtils.ExcuteProcedure(StoreProcedures.spUpdateStatusProductRTCQRCode,
                                        new string[] { "@ProductRTCQRCodeID", "@Status", "@ProductRTCQRCode" },
                                        new object[] { ProductRTCQRCodeID, 3, oHistoryModel.ProductRTCQRCode });
                                    //TextUtils.ExcuteSQL($"spUpdateStatusProductRTCQRCode '{oHistoryModel.ProductRTCQRCodeID}','{3}'");
                                }
                            }
                        }
                        else
                        {
                            //Duyệt + thêm phiếu mượn
                            HistoryProductRTCModel oHistoryModel = new HistoryProductRTCModel();
                            oHistoryModel.ProductRTCQRCodeID = 0;
                            oHistoryModel.ProductRTCID = TextUtils.ToInt(grvDetail.GetRowCellValue(i, colProductID));
                            oHistoryModel.DateBorrow = TextUtils.ToDate5(grvBillExportTech.GetFocusedRowCellValue(colCreatDate));
                            oHistoryModel.DateReturnExpected = TextUtils.ToDate5(grvBillExportTech.GetFocusedRowCellValue(colExpectedDate));
                            oHistoryModel.PeopleID = TextUtils.ToInt(grvBillExportTech.GetFocusedRowCellValue(colReceiverID));
                            oHistoryModel.Note += "Phiếu xuất " + grvBillExportTech.GetFocusedRowCellValue(colCode).ToString() + " - " + grvDetail.GetRowCellValue(i, colNote).ToString();
                            oHistoryModel.Project = TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colProjectName));
                            oHistoryModel.Status = 1;
                            oHistoryModel.BillExportTechnicalID = MasterID;
                            oHistoryModel.NumberBorrow = TextUtils.ToDecimal(grvDetail.GetRowCellValue(i, colQuantity));
                            oHistoryModel.WarehouseID = warehouseID;

                            HistoryProductRTCBO.Instance.Insert(oHistoryModel);
                        }
                    }
                }
                else
                {
                    if (CkeckAdd)
                    {
                        //Huỷ duyệt + Thêm phiếu mượn
                        //Xóa bỏ các phiếu mượn được tạo từ phiếu xuất
                        HistoryProductRTCBO.Instance.DeleteByAttribute("BillExportTechnicalID", MasterID);
                    }

                    //update status trong bảng ProductRTCQRCode status=1(Trong Kho)
                    if (ProductRTCQRCodeID > 0)
                    {
                        TextUtils.ExcuteProcedure(StoreProcedures.spUpdateStatusProductRTCQRCode, new string[] { "@ProductRTCQRCodeID", "@Status" }, new object[] { ProductRTCQRCodeID, 1 });
                    }

                }

            }


            if (listHistoryID.Count > 0)
            {
                int billExportID = Status ? MasterID : 0;
                string query = $"UPDATE HistoryProductRTC " +
                                    $"SET BillExportTechnicalID = {billExportID}, " +
                                    $"UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', " +
                                    $"UpdatedBy = '{Global.LoginName}' " +
                                $"WHERE ID IN({string.Join(",", listHistoryID)})";
                TextUtils.ExcuteSQL(query);
            }


        }
        void approved(bool isApproved)
        {
            string billCode = TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colCode));
            int ID = TextUtils.ToInt(grvBillExportTech.GetFocusedRowCellValue(colID));
            if (ID <= 0) return;
            if (MessageBox.Show(string.Format("Bạn có chắc muốn {0} duyệt phiếu [{1}] này ?", isApproved ? "" : "bỏ", billCode), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                //string sql = string.Format("UPDATE dbo.BillExportTechnical SET Status = {0} WHERE ID = {1}", isApproved ? 1 : 0, ID);
                //TextUtils.ExcuteSQL(sql);

                BillExportTechnicalModel billExport = SQLHelper<BillExportTechnicalModel>.FindByID(ID);
                billExport.Status = isApproved ? 1 : 0;

                SQLHelper<BillExportTechnicalModel>.Update(billExport);

                // calculateExport();
                //if (isApproved == true)
                //    grvBillExportTech.SetFocusedRowCellValue(colStatus, true);
                //else
                //    grvBillExportTech.SetFocusedRowCellValue(colStatus, false);

                //InsertOrDeleteHistoryProductRTC();
                //Khánh update 28/110/2023
                UpdateLog(ID, isApproved);
                loadBillExport();
            }
        }
        //Cập nhật lịch sử nhận chứng từ Khánh update 28/11/2023
        void UpdateLog(int billImportID, bool status)
        {
            BillExportTechnicalLogModel log = new BillExportTechnicalLogModel();
            log.BillExportTechnicalID = billImportID;
            log.StatusBill = status;
            log.DateStatus = DateTime.Now;
            BillExportTechnicalLogBO.Instance.Insert(log);
        }

        /// <summary>
        /// Update lại số lượng tồn kho của thiết bị khi xuất kho
        /// </summary>
        private void calculateExport()
        {
            int ID = TextUtils.ToInt(grvBillExportTech.GetFocusedRowCellValue(colID));
            TextUtils.ExcuteProcedure("spCalculateExportTechnical", new string[] { "@ID" }, new object[] { ID });
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            int IDMaster = TextUtils.ToInt(grvBillExportTech.GetFocusedRowCellValue(colID));
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
            string fileSourceName = "PXKD.xlsx";

            string sourcePath = Application.StartupPath + "\\" + fileSourceName;
            string phieucode = TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colCode));
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

            //DataTable dtMaster = TextUtils.LoadDataFromSP("spGetBillExportTechExcel", "A", new string[] { "@ID" }, new object[] { IDMaster });

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
            {
                //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                Microsoft.Office.Interop.Excel.Application app = default(Microsoft.Office.Interop.Excel.Application);
                Microsoft.Office.Interop.Excel.Workbook workBoook = default(Microsoft.Office.Interop.Excel.Workbook);
                Microsoft.Office.Interop.Excel.Worksheet workSheet = default(Microsoft.Office.Interop.Excel.Worksheet);
                try
                {
                    app = new Microsoft.Office.Interop.Excel.Application();
                    app.Workbooks.Open(currentPath);
                    workBoook = app.Workbooks[1];
                    workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBoook.Worksheets[1];

                    DateTime createdDate = TextUtils.ToDate5(grvBillExportTech.GetFocusedRowCellValue(colCreatDate));
                    //string creatDate1 = TextUtils.ToDate5(dtMaster.Rows[0]["CreatedDate"]).ToString("dd");
                    //string creatDate2 = TextUtils.ToDate5(dtMaster.Rows[0]["CreatedDate"]).ToString("MM");
                    //string creatDate3 = TextUtils.ToDate5(dtMaster.Rows[0]["CreatedDate"]).ToString("yyyy");

                    //if (TextUtils.ToString(dtMaster.Rows[0]["CreatedDate"]) == "")
                    //{
                    //    creatDate1 = TextUtils.ToDate3(dtMaster.Rows[0]["UpdatedDate"]).ToString("dd");
                    //    creatDate2 = TextUtils.ToDate3(dtMaster.Rows[0]["UpdatedDate"]).ToString("MM");
                    //    creatDate3 = TextUtils.ToDate3(dtMaster.Rows[0]["UpdatedDate"]).ToString("yyyy");
                    //}
                    workSheet.Cells[14, 2] = "Hà Nội, Ngày " + createdDate.Day + " tháng " + createdDate.Month + " năm " + createdDate.Year;

                    //string totalName = TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colCode));
                    //workSheet.Cells[6, 1] = "Số: " + totalName;

                    //string customerName = TextUtils.ToString(dtMaster.Rows[0]["CustomerName"]);
                    string customerName = "";
                    string supplierCode = TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colNameNCC)).Trim();
                    string customerFullName = TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colCustomerName)).Trim();

                    //if (customerName != "")
                    //{
                    //    workSheet.Cells[19, 2] = "Khách hàng";
                    //    workSheet.Cells[19, 3] = customerName;
                    //}
                    //else if (supplierCode != "")
                    //{
                    //    //workSheet.Cells[19, 2] = "Khách hàng";
                    //    
                    //}

                    string billCode = TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colCode));
                    workSheet.Cells[16, 4] = billCode;
                    workSheet.Cells[17, 3] = string.IsNullOrEmpty(supplierCode) ? customerFullName : supplierCode;

                    workSheet.Cells[18, 3] = TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colProjectNameMaster));


                    if (customerName != "")
                    {
                        workSheet.Cells[19, 3] = TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colReceiver)) + " - " + customerName;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colDepartmentName))))
                            workSheet.Cells[19, 3] = TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colReceiver));
                        else
                            workSheet.Cells[19, 3] = TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colReceiver)) + " / Phòng " + TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colDepartmentName));
                    }
                    workSheet.Cells[17, 7] = workSheet.Cells[39, 5] = TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colDeliver));
                    workSheet.Cells[20, 3] = TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colAddres));
                    workSheet.Cells[39, 10] = TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colReceiver));
                    //workSheet.Cells[25, 3] = TextUtils.ToString(dtMaster.Rows[0]["D"]);

                    //string fileName = TextUtils.ToString(dtMaster.Rows[0]["Image"]);
                    string fileName = TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colImage));
                    if (fileName != "")
                    {
                        workSheet.Shapes.AddPicture(fileName, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 100, 800, 200, 100);
                    }

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
                        workSheet.Cells[26, 2] = i + 1;
                        workSheet.Cells[26, 3] = TextUtils.ToString(grvDetail.GetRowCellValue(i, colProductCode));
                        workSheet.Cells[26, 4] = TextUtils.ToString(grvDetail.GetRowCellValue(i, colProductName));
                        workSheet.Cells[26, 5] = TextUtils.ToString(grvDetail.GetRowCellValue(i, colQuantity)); //Quantity
                        workSheet.Cells[26, 6] = TextUtils.ToString(grvDetail.GetRowCellValue(i, colUnit));
                        workSheet.Cells[26, 7] = TextUtils.ToString(grvDetail.GetRowCellValue(i, colMaker));//UnitName

                        workSheet.Cells[26, 8] = TextUtils.ToString(grvDetail.GetRowCellValue(i, colWarehouseType));
                        workSheet.Cells[26, 9] = TextUtils.ToString(grvDetail.GetRowCellValue(i, colInternalCode));
                        workSheet.Cells[26, 10] = TextUtils.ToString(grvDetail.GetRowCellValue(i, colNote));

                        ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[26]).Insert();
                    }
                    ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[26]).Delete();
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


        //private void btnExportExcel_Click(object sender, EventArgs e)
        //{
        //    int IDMaster = TextUtils.ToInt(grvBillExportTech.GetFocusedRowCellValue(colID));
        //    if (IDMaster == 0) return;

        //    string path = "";
        //    FolderBrowserDialog fbd = new FolderBrowserDialog();
        //    if (fbd.ShowDialog() == DialogResult.OK)
        //    {
        //        path = fbd.SelectedPath;
        //    }
        //    else
        //    {
        //        return;
        //    }
        //    string fileSourceName = "BillExportTechnical.xlsx";

        //    string sourcePath = Application.StartupPath + "\\" + fileSourceName;
        //    string phieucode = TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colCode));
        //    string currentPath = path + "\\" + phieucode + DateTime.Now.ToString("_dd_MM_yyyy_HH_mm_ss") + ".xlsx";
        //    try
        //    {
        //        File.Copy(sourcePath, currentPath, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Có lỗi khi tạo phiếu!" + Environment.NewLine + ex.Message,
        //            TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //        return;
        //    }

        //    //DataTable dtMaster = TextUtils.LoadDataFromSP("spGetBillExportTechExcel", "A", new string[] { "@ID" }, new object[] { IDMaster });

        //    using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
        //    {
        //        //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        //        Microsoft.Office.Interop.Excel.Application app = default(Microsoft.Office.Interop.Excel.Application);
        //        Microsoft.Office.Interop.Excel.Workbook workBoook = default(Microsoft.Office.Interop.Excel.Workbook);
        //        Microsoft.Office.Interop.Excel.Worksheet workSheet = default(Microsoft.Office.Interop.Excel.Worksheet);
        //        try
        //        {
        //            app = new Microsoft.Office.Interop.Excel.Application();
        //            app.Workbooks.Open(currentPath);
        //            workBoook = app.Workbooks[1];
        //            workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBoook.Worksheets[1];

        //            DateTime createdDate = TextUtils.ToDate5(grvBillExportTech.GetFocusedRowCellValue(colCreatDate));
        //            //string creatDate1 = TextUtils.ToDate5(dtMaster.Rows[0]["CreatedDate"]).ToString("dd");
        //            //string creatDate2 = TextUtils.ToDate5(dtMaster.Rows[0]["CreatedDate"]).ToString("MM");
        //            //string creatDate3 = TextUtils.ToDate5(dtMaster.Rows[0]["CreatedDate"]).ToString("yyyy");

        //            //if (TextUtils.ToString(dtMaster.Rows[0]["CreatedDate"]) == "")
        //            //{
        //            //    creatDate1 = TextUtils.ToDate3(dtMaster.Rows[0]["UpdatedDate"]).ToString("dd");
        //            //    creatDate2 = TextUtils.ToDate3(dtMaster.Rows[0]["UpdatedDate"]).ToString("MM");
        //            //    creatDate3 = TextUtils.ToDate3(dtMaster.Rows[0]["UpdatedDate"]).ToString("yyyy");
        //            //}
        //            workSheet.Cells[16, 2] = "Hà Nội, Ngày " + createdDate.Day + " tháng " + createdDate.Month + " năm " + createdDate.Year;

        //            //string totalName = TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colCode));
        //            //workSheet.Cells[6, 1] = "Số: " + totalName;

        //            //string customerName = TextUtils.ToString(dtMaster.Rows[0]["CustomerName"]);
        //            string customerName = "";
        //            string supplierCode = TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colNameNCC));

        //            //if (customerName != "")
        //            //{
        //            //    workSheet.Cells[19, 2] = "Khách hàng";
        //            //    workSheet.Cells[19, 3] = customerName;
        //            //}
        //            //else if (supplierCode != "")
        //            //{
        //            //    //workSheet.Cells[19, 2] = "Khách hàng";
        //            //    
        //            //}
        //            workSheet.Cells[19, 3] = supplierCode;
        //            workSheet.Cells[18, 4] = TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colCode));


        //            if (customerName != "")
        //            {
        //                workSheet.Cells[20, 3] = TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colReceiver)) + " - " + customerName;
        //            }
        //            else
        //            {
        //                workSheet.Cells[20, 3] = TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colReceiver));
        //            }
        //            workSheet.Cells[19, 7] = workSheet.Cells[39, 5] = TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colDeliver));
        //            workSheet.Cells[21, 3] = TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colAddres));
        //            workSheet.Cells[39, 10] = TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colReceiver));
        //            //workSheet.Cells[25, 3] = TextUtils.ToString(dtMaster.Rows[0]["D"]);

        //            //string fileName = TextUtils.ToString(dtMaster.Rows[0]["Image"]);
        //            string fileName = TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colImage));
        //            if (fileName != "")
        //            {

        //                workSheet.Shapes.AddPicture(fileName, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 100, 800, 200, 100);
        //            }

        //            for (int i = grvDetail.RowCount - 1; i >= 0; i--)
        //            {
        //                workSheet.Cells[28, 2] = i + 1;
        //                workSheet.Cells[28, 3] = TextUtils.ToString(grvDetail.GetRowCellValue(i, colProductCode));
        //                workSheet.Cells[28, 4] = TextUtils.ToString(grvDetail.GetRowCellValue(i, colProductName));
        //                workSheet.Cells[28, 5] = TextUtils.ToString(grvDetail.GetRowCellValue(i, colQuantity)); //Quantity
        //                workSheet.Cells[28, 6] = TextUtils.ToString(grvDetail.GetRowCellValue(i, colUnit));
        //                workSheet.Cells[28, 7] = TextUtils.ToString(grvDetail.GetRowCellValue(i, colMaker));//UnitName

        //                workSheet.Cells[28, 8] = TextUtils.ToString(grvDetail.GetRowCellValue(i, colWarehouseType));
        //                workSheet.Cells[28, 9] = TextUtils.ToString(grvDetail.GetRowCellValue(i, colInternalCode));
        //                workSheet.Cells[28, 10] = TextUtils.ToString(grvDetail.GetRowCellValue(i, colNote));

        //                ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[28]).Insert();
        //            }
        //            ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[27]).Delete();
        //            ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[27]).Delete();
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //        }
        //        finally
        //        {
        //            if (app != null)
        //            {
        //                app.ActiveWorkbook.Save();
        //                app.Workbooks.Close();
        //                app.Quit();
        //            }
        //        }
        //        Process.Start(currentPath);
        //    }
        //}

        private void repositoryItemPictureEdit2_ImageLoading(object sender, DevExpress.XtraEditors.Repository.SaveLoadImageEventArgs e)
        {
            e.Image = Image.FromFile("F:\\Pictures\\909366.jpg");
        }

        private void grvBillExportTech_KeyDown(object sender, KeyEventArgs e)
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

        private void btnShowLog_Click(object sender, EventArgs e)
        {
            int billID = TextUtils.ToInt(grvBillExportTech.GetFocusedRowCellValue(colID));
            if (billID <= 0)
            {
                return;
            }

            frmBillLog frm = new frmBillLog();
            frm.billType = 3;
            frm.billExportID = billID;
            frm.Show();
        }

        private void btnBillDocumentExportTechnical_Click(object sender, EventArgs e)
        {
            var focusedRowHandle = grvBillExportTech.FocusedRowHandle;
            int id = TextUtils.ToInt(grvBillExportTech.GetFocusedRowCellValue(colID));
            string code = TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colCode));
            if (id <= 0) return;

            frmBillDocumentExportTechnical frm = new frmBillDocumentExportTechnical();
            frm.BillExportTechnicalID = id;
            frm.code = code;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                grvBillExportTech.FocusedRowHandle = focusedRowHandle;
                grvBillExportTech_FocusedRowChanged(null, null);
            }
        }

        private void btnScanBill_Click(object sender, EventArgs e)
        {
            var wh = SQLHelper<WarehouseModel>.FindByID(warehouseID);
            frmBillExportTechScan frm = new frmBillExportTechScan();
            frm.WarehouseCode = wh.WarehouseCode;
            frm.warehouseID = wh.ID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                btnFind_Click(null, null);
            }
        }
    }
}
