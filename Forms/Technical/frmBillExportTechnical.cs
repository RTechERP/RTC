using BMS;
using BMS.Business;
using BMS.Model;
using DevExpress.Utils;
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
    public partial class frmBillExportTechnical : _Forms
    {
        public frmBillExportTechnical()
        {
            InitializeComponent();
        }

        private void frmBillExportTechnical_Load(object sender, EventArgs e)
        {
           

            DateTime datenow = new DateTime(dtpDS.Value.Year, dtpDS.Value.Month, dtpDS.Value.Day, 0, 0, 0);
            dtpDS.Value = datenow.AddMonths(-1);

            cboStatus.SelectedIndex = 2;
            txtPageNumber.Text = "1";
            loadBillExport();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmBillExportTechDetail frm = new frmBillExportTechDetail();
            if (frm.ShowDialog() == DialogResult.OK )
            {
                loadBillExport();
            }
        }

        void loadBillExport()
        {
            
            DateTime dateTimeS = new DateTime(dtpDS.Value.Year, dtpDS.Value.Month, dtpDS.Value.Day, 0, 0, 0);
            DateTime dateTimeE = new DateTime(dtpDE.Value.Year, dtpDE.Value.Month, dtpDE.Value.Day, 23, 59, 59);

            DataSet dataSet = new DataSet();
            dataSet = TextUtils.LoadDataSetFromSP("spGetBillExportTechnical"
                , new string[] { "@PageNumber", "@PageSize", "@DateStart", "@DateEnd", "@Status", "@FilterText" }
                , new object[] { TextUtils.ToInt(txtPageNumber.Text), TextUtils.ToInt(txtPageSize.Value), dateTimeS, dateTimeE,  cboStatus.SelectedIndex, txtKeyword.Text });
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
            BillExportTechnicalModel model = (BillExportTechnicalModel)BillExportTechnicalBO.Instance.FindByPK(ID);
            frmBillExportTechDetail frm = new frmBillExportTechDetail();
            if(TextUtils.ToBoolean(grvBillExportTech.GetFocusedRowCellValue(colStatus)))
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
            DataTable dt = TextUtils.LoadDataFromSP("spGetBillExportTechDetail", "A", new string[] { "@Id" }, new object[] { ID });
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

                //Update lại HistoryProductRTC trước
                if (AddHistoryProduct)
                {
                    //Xoá bỏ các phiếu mượn đã tạo
                    HistoryProductRTCBO.Instance.DeleteByAttribute("BillExportTechnicalID", strID);//Xoá nếu các phiếu mượn này được tạo từ phiếu xuất
                }
                else
                {
                    for (int i = 0; i < grvDetail.RowCount; i++)
                    {
                        grvBillExportTech.Focus();
                        int HistoryRTCID = TextUtils.ToInt(grvDetail.GetRowCellValue(i, colHistoryProductRTCID));
                        if (HistoryRTCID <= 0) return;
                        HistoryProductRTCModel oHistoryModel = (HistoryProductRTCModel)HistoryProductRTCBO.Instance.FindByPK(HistoryRTCID);
                        if (oHistoryModel == null) return;
                        //oHistoryModel.Note = "";
                        oHistoryModel.BillExportTechnicalID = 0;
                        HistoryProductRTCBO.Instance.Update(oHistoryModel);
                    }

                }

                BillExportTechnicalBO.Instance.Delete(strID);
                BillExportDetailTechnicalBO.Instance.DeleteByAttribute("BillExportTechID", strID);
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
                string billCode = TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colCode));
                MessageBox.Show(String.Format("Phiếu xuất [{0}] đã được duyệt. Xin vui lòng kiểm tra lại!", billCode), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Question);
                return;
            }
            approved(true);
        }

        private void btnUnapproved_Click(object sender, EventArgs e)
        {
            bool isApproved = TextUtils.ToBoolean(grvBillExportTech.GetFocusedRowCellValue(colStatus));
            if (isApproved == false)
            {
                string billCode = TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colCode));
                MessageBox.Show(String.Format("Phiếu xuất [{0}] chưa được duyệt.", billCode), TextUtils.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Question);
                return;
            }
            approved(false);
        }
        void InsertOrDeleteHistoryProductRTC()
        {
            int MasterID = TextUtils.ToInt(grvBillExportTech.GetFocusedRowCellValue(colID));
            bool CkeckAdd = TextUtils.ToBoolean(grvBillExportTech.GetFocusedRowCellValue(colCheckAddHistoryProductRTC));//Có thêm phiếu mượn hay không
            bool Status = TextUtils.ToBoolean(grvBillExportTech.GetFocusedRowCellValue(colStatus));
            //Thêm phiếu mượn

            for (int i = 0; i < grvDetail.RowCount; i++)
            {
                if (Status && CkeckAdd)
                {

                    HistoryProductRTCModel oHistoryModel = new HistoryProductRTCModel();
                    oHistoryModel.ProductRTCID = TextUtils.ToInt(grvDetail.GetRowCellValue(i, colProductID));
                    //oHistoryModel.CreatedBy = Global.LoginName;
                    oHistoryModel.DateBorrow = TextUtils.ToDate(grvBillExportTech.GetFocusedRowCellValue(colCreatDate).ToString());
                    oHistoryModel.DateReturnExpected = TextUtils.ToDate(grvBillExportTech.GetFocusedRowCellValue(colExpectedDate).ToString());
                    //oHistoryModel.DateReturnExpected = TextUtils.ToDate(dtpReturn.Value.ToString("yyyy/MM/dd HH:mm:ss"));
                    //  UsersModel user = UsersBO.Instance.FindByAttribute("LoginName", cbUser.Text)[0] as UsersModel;
                    oHistoryModel.PeopleID = TextUtils.ToInt(grvBillExportTech.GetFocusedRowCellValue(colReceiverID));
                    oHistoryModel.Note = "Phiếu xuất " + grvBillExportTech.GetFocusedRowCellValue(colCode).ToString()+" - "+ grvDetail.GetRowCellValue(i,colNote).ToString();
                    oHistoryModel.Project =TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colProjectName));
                    oHistoryModel.Status = 1;
                    oHistoryModel.BillExportTechnicalID = MasterID;
                    oHistoryModel.NumberBorrow = TextUtils.ToDecimal(grvDetail.GetRowCellValue(i, colQuantity));
                   
                    HistoryProductRTCBO.Instance.Insert(oHistoryModel);
                }
                else if(Status && !CkeckAdd)
                {
                    int HistoryRTCID = TextUtils.ToInt(grvDetail.GetRowCellValue(i, colHistoryProductRTCID));
                    if (HistoryRTCID <= 0) return;
                    HistoryProductRTCModel oHistoryModel =(HistoryProductRTCModel)HistoryProductRTCBO.Instance.FindByPK(HistoryRTCID);
                    //oHistoryModel.CreatedBy = Global.LoginName;
                    //oHistoryModel.DateReturnExpected = TextUtils.ToDate(dtpReturn.Value.ToString("yyyy/MM/dd HH:mm:ss"));
                    //  UsersModel user = UsersBO.Instance.FindByAttribute("LoginName", cbUser.Text)[0] as UsersModel;
                    oHistoryModel.Note = "Phiếu xuất " + grvBillExportTech.GetFocusedRowCellValue(colCode).ToString();
                    oHistoryModel.BillExportTechnicalID = MasterID;
                    HistoryProductRTCBO.Instance.Update(oHistoryModel);
                }    
                else if(!Status && CkeckAdd)
                {
                    //Nếu phiếu xuất-> phiếu mượn.
                    
                    HistoryProductRTCBO.Instance.DeleteByAttribute("BillExportTechnicalID", MasterID);

                   
                }
                else if (!Status && !CkeckAdd)
                {
                    //Nếu phiếu mượn ->phiếu xuất
                    int HistoryRTCID = TextUtils.ToInt(grvDetail.GetRowCellValue(i, colHistoryProductRTCID));
                    if (HistoryRTCID <= 0) return;
                    HistoryProductRTCModel oHistoryModel = (HistoryProductRTCModel)HistoryProductRTCBO.Instance.FindByPK(HistoryRTCID);
                    //oHistoryModel.Note = "";
                    oHistoryModel.BillExportTechnicalID =0;
                    HistoryProductRTCBO.Instance.Update(oHistoryModel);
                }
            }
        }
        void approved(bool isApproved)
        {
            string billCode = TextUtils.ToString(grvBillExportTech.GetFocusedRowCellValue(colCode));
            int ID = TextUtils.ToInt(grvBillExportTech.GetFocusedRowCellValue(colID));
            if (ID <= 0) return;
            if (MessageBox.Show(string.Format("Bạn có chắc muốn {0} duyệt phiếu [{1}] này ?", isApproved ? "" : "bỏ", billCode), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
               
                string sql = string.Format("UPDATE dbo.BillExportTechnical SET Status = {0} WHERE ID = {1}", isApproved ? 1 : 0, ID);
                TextUtils.ExcuteSQL(sql);
                calculateExport();
                if (isApproved == true)
                    grvBillExportTech.SetFocusedRowCellValue(colStatus, true);
                else
                    grvBillExportTech.SetFocusedRowCellValue(colStatus, false);

                InsertOrDeleteHistoryProductRTC();
            }
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
            string fileSourceName = "BillExportTechnical.xlsx";

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

            DataTable dtMaster = TextUtils.LoadDataFromSP("spGetBillExportTechExcel", "A", new string[] { "@ID" }, new object[] { IDMaster });

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                Microsoft.Office.Interop.Excel.Application app = default(Microsoft.Office.Interop.Excel.Application);
                Microsoft.Office.Interop.Excel.Workbook workBoook = default(Microsoft.Office.Interop.Excel.Workbook);
                Microsoft.Office.Interop.Excel.Worksheet workSheet = default(Microsoft.Office.Interop.Excel.Worksheet);
                try
                {
                    app = new Microsoft.Office.Interop.Excel.Application();
                    app.Workbooks.Open(currentPath);
                    workBoook = app.Workbooks[1];
                    workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBoook.Worksheets[1];

                    
                    string creatDate1 = TextUtils.ToDate3(dtMaster.Rows[0]["CreatedDate"]).ToString("dd");
                    string creatDate2 = TextUtils.ToDate3(dtMaster.Rows[0]["CreatedDate"]).ToString("MM");
                    string creatDate3 = TextUtils.ToDate3(dtMaster.Rows[0]["CreatedDate"]).ToString("yyyy");
                    if (TextUtils.ToString(dtMaster.Rows[0]["CreatedDate"]) == "")
                    {
                        creatDate1 = TextUtils.ToDate3(dtMaster.Rows[0]["UpdatedDate"]).ToString("dd");
                        creatDate2 = TextUtils.ToDate3(dtMaster.Rows[0]["UpdatedDate"]).ToString("MM");
                        creatDate3 = TextUtils.ToDate3(dtMaster.Rows[0]["UpdatedDate"]).ToString("yyyy");
                    }
                    workSheet.Cells[16, 2] = "Hà Nội, Ngày " + creatDate1 + " tháng " + creatDate2 + " năm " + creatDate3;

                    string totalName = TextUtils.ToString(dtMaster.Rows[0]["Code"]);
                    //workSheet.Cells[6, 1] = "Số: " + totalName;

                    string customerName = TextUtils.ToString(dtMaster.Rows[0]["CustomerName"]);
                    string supplierCode = TextUtils.ToString(dtMaster.Rows[0]["CodeNCC"]);

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
                    workSheet.Cells[19, 3] = supplierCode;
                    workSheet.Cells[18, 4] = TextUtils.ToString(dtMaster.Rows[0]["Code"]);
                    

                    if (customerName != "")
                    {
                        workSheet.Cells[20, 3] = TextUtils.ToString(dtMaster.Rows[0]["Receiver"]) + " - " + customerName;
                    }
                    else
                    {
                        workSheet.Cells[20, 3] = TextUtils.ToString(dtMaster.Rows[0]["Receiver"]);
                    }
                    workSheet.Cells[19, 7] = workSheet.Cells[39, 5] = TextUtils.ToString(dtMaster.Rows[0]["Deliver"]);
                    workSheet.Cells[21, 3] = TextUtils.ToString(dtMaster.Rows[0]["Addres"]);
                    workSheet.Cells[39, 10] = TextUtils.ToString(dtMaster.Rows[0]["Receiver"]);
                    //workSheet.Cells[25, 3] = TextUtils.ToString(dtMaster.Rows[0]["D"]);

                    string fileName = TextUtils.ToString(dtMaster.Rows[0]["Image"]);
                    if (fileName != "")
                    {
                        workSheet.Shapes.AddPicture(fileName, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 100, 800, 200, 100);
                    }

                    for (int i = dtMaster.Rows.Count - 1; i >= 0; i--)
                    {
                        workSheet.Cells[28, 2] = i + 1;
                        workSheet.Cells[28, 3] = TextUtils.ToString(dtMaster.Rows[i]["ProductCode"]);
                        workSheet.Cells[28, 4] = TextUtils.ToString(dtMaster.Rows[i]["ProductName"]);
                        workSheet.Cells[28, 5] = TextUtils.ToString(dtMaster.Rows[i]["Quantity"]); //Quantity
                        workSheet.Cells[28, 6] = TextUtils.ToString(dtMaster.Rows[i]["UnitName"]); 
                        workSheet.Cells[28, 7] = TextUtils.ToString(dtMaster.Rows[i]["Maker"]);//UnitName

                        workSheet.Cells[28, 8] = TextUtils.ToString(dtMaster.Rows[i]["WarehouseType"]);
                        workSheet.Cells[28, 9] = TextUtils.ToString(dtMaster.Rows[i]["ProductCodeRTC"]);
                        workSheet.Cells[28, 10] = TextUtils.ToString(dtMaster.Rows[i]["Note"]);

                        ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[28]).Insert();
                    }
                    ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[27]).Delete();
                    ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[27]).Delete();
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

        private void repositoryItemPictureEdit2_ImageLoading(object sender, DevExpress.XtraEditors.Repository.SaveLoadImageEventArgs e)
        {
            e.Image = Image.FromFile("F:\\Pictures\\909366.jpg");
        }

       
    }
}
