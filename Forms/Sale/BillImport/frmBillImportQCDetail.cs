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
using DevExpress.UIAutomation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DocumentFormat.OpenXml.Office2010.Excel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using DevExpress.DataProcessing.InMemoryDataProcessor;
using System.Net.Http;
using DevExpress.Utils.Extensions;
using System.Net;

namespace BMS
{
    public partial class frmBillImportQCDetail : _Forms
    {
        List<int> lsEmployeeID = new List<int>();
        List<int> lsLeaderID = new List<int>();
        List<string> lsEmailCC = new List<string>();
        List<int> lstDeletedDetails = new List<int>();
        public DataTable dtDetails = new DataTable();
        public BillImportQCModel billImportQC = new BillImportQCModel();
        public BillImportQCDetailModel billImportQCDetail = new BillImportQCDetailModel();
        private List<ProductSaleModel> lsProductSale = SQLHelper<ProductSaleModel>.FindAll();
        //private List<BillImportQCDetailModel> lsProductQC = SQLHelper<BillImportQCDetailModel>.FindByAttribute(BillImportQCDetailModel_Enum.IsDeleted.ToString(), 0);
        public bool isCheckBillQC = false;
        public bool isAddNewToBillImport = false;
        List<ProjectModel> lstProject = new List<ProjectModel>();

        public frmBillImportQCDetail()
        {
            InitializeComponent();
        }

        private void frmBillImportQCDetail_Load(object sender, EventArgs e)
        {
            loadProject();
            loadBilllNumber();
            LoadEmployeeRequest();
            LoadProductSale();
            LoadStatus();
            LoadData();
            if (isAddNewToBillImport)
            {
                grdDetails.MouseDown -= grdDetail_MouseDown;
                btnAddAndNew.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        private void loadProject()
        {
            this.lstProject = SQLHelper<ProjectModel>.FindAll();
            cboProject.DisplayMember = "ProjectCode";
            cboProject.ValueMember = "ID";
            cboProject.DataSource = lstProject;
        }

        void LoadEmployeeRequest()
        {
            int[] leaderID = new int[] { 97, 54, 115, 84 };
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            //List<EmployeeModel> lsEm = SQLHelper<EmployeeModel>.FindAll().Where(x => leaderID.Contains(x.ID)).ToList();
            DataTable leaders = TextUtils.LoadDataFromSP("spGetEmployeeApprove", "A", new string[] { "@Type" }, new object[] { 2 });

            cboEmployeeRequest.Properties.DataSource = dt;
            cboEmployeeRequest.Properties.ValueMember = "ID";
            cboEmployeeRequest.Properties.DisplayMember = "FullName";

            cboLeaderTech.DataSource = leaders;
            cboLeaderTech.ValueMember = "EmployeeID";
            cboLeaderTech.DisplayMember = "FullName";

            cboEmTech.DataSource = dt;
            cboEmTech.ValueMember = "ID";
            cboEmTech.DisplayMember = "FullName";

            if (billImportQC.ID > 0) cboEmployeeRequest.EditValue = billImportQC.EmployeeRequestID;
            else cboEmployeeRequest.EditValue = Global.EmployeeID;

            cboEmployeeRequest.Enabled = Global.IsAdmin;

            dtpDateRequest.Enabled = Global.EmployeeID == TextUtils.ToInt(cboEmployeeRequest.EditValue);
            dtpDealine.Enabled = Global.EmployeeID == TextUtils.ToInt(cboEmployeeRequest.EditValue);
        }

        void LoadStatus()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Status", typeof(string));

            // Thêm dữ liệu
            dt.Rows.Add(1, "OK");
            dt.Rows.Add(2, "NG");
            dt.Rows.Add(3, "Đã yêu cầu QC");

            cboStatus.DisplayMember = "Status";
            cboStatus.ValueMember = "ID";
            cboStatus.DataSource = dt;
        }

        void LoadProductSale()
        {
            cboProductSale.DisplayMember = "ProductCode";
            cboProductSale.ValueMember = "ID";
            cboProductSale.DataSource = lsProductSale;
        }

        private void cboProductSale_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                grvDetails.FocusedRowHandle = -1;
                int ID = TextUtils.ToInt(grvDetails.GetFocusedRowCellValue(colProductSaleID));
                string productName = lsProductSale.Where(x => x.ID == ID).FirstOrDefault().ProductName;
                grvDetails.SetFocusedRowCellValue(colProductName, productName);
            }
            catch (Exception)
            { }
        }

        bool CheckValidate()
        {
            DateTime dateRequest = TextUtils.ToDate3(dtpDateRequest.EditValue);
            DateTime deadline = TextUtils.ToDate3(dtpDealine.EditValue);
            grvDetails.CloseEditor();

            if (TextUtils.ToInt(cboEmployeeRequest.EditValue) == 0)
            {
                MessageBox.Show("Vui lòng chọn người yêu cầu!", "Thông báo", MessageBoxButtons.OK);
                return false;
            }

            if (dtpDateRequest.EditValue == null)
            {
                MessageBox.Show("Vui lòng chọn ngày yêu cầu!", "Thông báo", MessageBoxButtons.OK);
                return false;
            }

            if (dtpDealine.EditValue == null)
            {
                MessageBox.Show("Vui lòng chọn dealine!", "Thông báo", MessageBoxButtons.OK);
                return false;
            }

            if (deadline.Date < dateRequest.Date)
            {
                MessageBox.Show("Vui lòng chọn lại dealine không được nhỏ hơn ngày yêu cầu!", "Thông báo", MessageBoxButtons.OK);
                return false;
            }

            if (grvDetails.RowCount <= 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần QC!", "Thông báo", MessageBoxButtons.OK);
                return false;
            }


            List<int> lsProductID = new List<int>();
            for (int i = 0; i < grvDetails.RowCount; i++)
            {

                int productSaleID = TextUtils.ToInt(grvDetails.GetRowCellValue(i, colProductSaleID));
                int stt = TextUtils.ToInt(grvDetails.GetRowCellValue(i, colDetailSTT));
                int leaderID = TextUtils.ToInt(grvDetails.GetRowCellValue(i, colLeaderTech));
                if (productSaleID <= 0)
                {
                    MessageBox.Show($"Vui lòng chọn sản phẩm dòng {stt}!", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }

                if (leaderID <= 0)
                {
                    MessageBox.Show($"Vui lòng chọn Leader dòng {stt}!", "Thông báo", MessageBoxButtons.OK);
                    return false;
                }

                if (!lsProductID.Contains(productSaleID)) lsProductID.Add(productSaleID);
                else
                {
                    //MessageBox.Show($"Vui lòng chọn lại sản phẩm dòng {stt} đã tồn tại!", "Thông báo", MessageBoxButtons.OK);
                    //return false;
                }

                //if(lsProductQC.Any(x => x.ProductSaleID == productSaleID))
                //{
                //	MessageBox.Show($"Vui lòng chọn lại sản phẩm dòng {stt} đã được yêu cầu QC!", "Thông báo", MessageBoxButtons.OK);
                //	return false;
                //}
            }

            return true;
        }

        void LoadData()
        {
            if (billImportQC.ID > 0)
            {
                txtRequestCode.Text = billImportQC.RequestImportCode;
                dtpDateRequest.EditValue = billImportQC.RequestDateQC;
                dtpDealine.EditValue = billImportQC.Dealine;
            }
            else dtpDateRequest.EditValue = DateTime.Now;

            if (dtDetails.Rows.Count <= 0)
            {
                dtDetails = TextUtils.LoadDataFromSP("spGetBillImportRequestQCDetail", "A",
                new string[] { "@BillImportRequestID" },
                new object[] { billImportQC.ID });
            }
            grdDetails.DataSource = dtDetails;

            foreach (DataRow item in dtDetails.Rows)
            {
                int id = TextUtils.ToInt(item["ID"]);
                int stt = TextUtils.ToInt(item["STT"]);
                List<BillImportQCDetailFilesModel> lstFile = SQLHelper<BillImportQCDetailFilesModel>.FindByExpression(new Expression("BillImportQCDetailID", id));
                if (lstFile.Count > 0)
                {
                    _dicFiles.Add(stt, lstFile);

                    string fileCheckSheet = string.Join(";", lstFile.Where(p => p.FileType == 1).Select(x => x.FileName));
                    string fileReport = string.Join(";", lstFile.Where(p => p.FileType == 2).Select(x => x.FileName));

                    grvDetails.SetRowCellValue(dtDetails.Rows.IndexOf(item), "CheckSheet", fileCheckSheet);
                    grvDetails.SetRowCellValue(dtDetails.Rows.IndexOf(item), "Report", fileReport);
                }
            }

            LoadDataFile();

            if (isCheckBillQC)
            {
                btnSave.Enabled = btnAddAndNew.Enabled = dtpDateRequest.Enabled = dtpDealine.Enabled = false;
                grvDetails.OptionsBehavior.ReadOnly = true;
            }
        }


        void LoadDataFile()
        {
            grdDataCheckSheet.DataSource = null;
            grdDataTestReport.DataSource = null;

            int billImportQCDetailID = TextUtils.ToInt(grvDetails.GetFocusedRowCellValue("ID"));

            var files = TextUtils.LoadDataSetFromSP("spGetBillImportQCDetailFiles", new string[] { "@BillImportQCDetailID" }, new object[] { billImportQCDetailID });
            var fileCheckSheets = files.Tables[0];//FILE CHECK SHEET
            var fileTestReports = files.Tables[1]; //File test report;

            grdDataCheckSheet.DataSource = fileCheckSheets;
            grdDataTestReport.DataSource = fileTestReports;
        }

        void loadBilllNumber()
        {
            if (billImportQC.ID > 0)
            {
                txtRequestCode.Text = billImportQC.RequestImportCode;
                return;
            }

            int code = 0;

            string month = TextUtils.ToString(DateTime.Now.ToString("MM"));
            string day = TextUtils.ToString(DateTime.Now.ToString("dd"));
            string year = TextUtils.ToString(DateTime.Now.Year).Substring(2);
            string date = year + month + day;

            Expression ex = new Expression("Month(CreatedDate)", DateTime.Now.Month);
            Expression ex1 = new Expression("Year(CreatedDate)", DateTime.Now.Year);
            Expression ex2 = new Expression("Day(CreatedDate)", DateTime.Now.Day);
            Expression ex3 = new Expression("IsDeleted", 0);
            var bill = SQLHelper<BillImportQCModel>.FindByExpression(ex.And(ex1).And(ex2).And(ex3)).OrderByDescending(x => x.ID).FirstOrDefault();
            string requestImportCode = "";
            if (bill != null) requestImportCode = bill.RequestImportCode;

            if (requestImportCode.Contains("YCQC"))
            {
                requestImportCode = requestImportCode.Substring(4);
            }

            if (billImportQC.ID == 0)
            {
                if (requestImportCode == "")
                {
                    txtRequestCode.Text = "YCQC" + date + "001";
                    return;
                }
                else
                {
                    code = TextUtils.ToInt(requestImportCode.Substring(requestImportCode.Length - 3));
                }

                if (code == 0)
                {
                    txtRequestCode.Text = "YCQC" + date + "001";
                    return;
                }
                else
                {
                    string dem = TextUtils.ToString(code + 1);
                    for (int i = 0; dem.Length < 3; i++)
                    {
                        dem = "0" + dem;
                    }

                    txtRequestCode.Text = "YCQC" + date + TextUtils.ToString(dem);
                }
            }

        }

        private void btnSaveAndClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (CheckValidate())
            {
                SaveData();
                this.DialogResult = DialogResult.OK;

            }
        }

        private void btnSaveAndNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (CheckValidate())
            {
                SaveData();
                cboEmployeeRequest.EditValue = Global.EmployeeID;
                dtpDealine.EditValue = null;
                dtDetails = new DataTable();
                billImportQC = new BillImportQCModel();
                loadBilllNumber();
                LoadData();
            }
        }

        async void SaveData()
        {
            DateTime dateRequest = TextUtils.ToDate3(dtpDateRequest.EditValue);
            DateTime deadline = TextUtils.ToDate3(dtpDealine.EditValue);
            int emRequestID = TextUtils.ToInt(cboEmployeeRequest.EditValue);
            bool isNew = false;

            string billCode = txtRequestCode.Text;

            billImportQC.RequestImportCode = billCode;
            billImportQC.EmployeeRequestID = emRequestID;
            billImportQC.RequestDateQC = dateRequest;
            billImportQC.Dealine = deadline;
            billImportQC.IsDeleted = false;

            if (billImportQC.ID <= 0)
            {
                billImportQC.ID = SQLHelper<BillImportQCModel>.Insert(billImportQC).ID;
                isNew = true;
            }
            else
            {
                billImportQC.UpdatedBy = Global.LoginName;
                billImportQC.UpdatedDate = DateTime.Now;
                SQLHelper<BillImportQCModel>.Update(billImportQC);
            }

            for (int i = 0; i < grvDetails.RowCount; i++)
            {
                int idDetail = TextUtils.ToInt(grvDetails.GetRowCellValue(i, colDetailID));
                BillImportQCDetailModel detail = new BillImportQCDetailModel();
                if (idDetail > 0) detail = SQLHelper<BillImportQCDetailModel>.FindByID(idDetail);

                int leaderTechID = TextUtils.ToInt(grvDetails.GetRowCellValue(i, colLeaderTech));
                int employeeTechID = TextUtils.ToInt(grvDetails.GetRowCellValue(i, colEmployeeTechID));

                if (!lsLeaderID.Contains(leaderTechID) && leaderTechID > 0) lsLeaderID.Add(leaderTechID);
                if (!lsEmployeeID.Contains(employeeTechID) && employeeTechID > 0) lsEmployeeID.Add(employeeTechID);

                detail.BillImportQCID = billImportQC.ID;
                detail.ProductSaleID = TextUtils.ToInt(grvDetails.GetRowCellValue(i, colProductSaleID));
                detail.BillImportDetailID = TextUtils.ToInt(grvDetails.GetRowCellValue(i, colBillImportDetailID));

                detail.LeaderTechID = leaderTechID;
                detail.EmployeeTechID = employeeTechID;

                int status = TextUtils.ToInt(grvDetails.GetRowCellValue(i, colStatus));
                if (status == 0) detail.Status = 3;
                else detail.Status = status;

                detail.Note = TextUtils.ToString(grvDetails.GetRowCellValue(i, colDetailNote));
                detail.IsDeleted = false;

                detail.ProjectID = TextUtils.ToInt(grvDetails.GetRowCellValue(i, colProjectID));
                detail.POKHCode = TextUtils.ToString(grvDetails.GetRowCellValue(i, colPOKHCode));
                detail.Quantity = TextUtils.ToDecimal(grvDetails.GetRowCellValue(i, colQuantity));

                if (idDetail > 0)
                {
                    detail.UpdatedBy = Global.LoginName;
                    detail.UpdatedDate = DateTime.Now;
                    SQLHelper<BillImportQCDetailModel>.Update(detail);
                }
                else
                {
                    detail.ID = SQLHelper<BillImportQCDetailModel>.Insert(detail).ID;
                }
                int stt = TextUtils.ToInt(grvDetails.GetRowCellValue(i, "STT"));
                await UploadFile(detail, stt);
                RemoveFile(stt);
            }

            if (lstDeletedDetails.Count > 0)
            {
                foreach (int i in lstDeletedDetails)
                {
                    var detail = SQLHelper<BillImportQCDetailModel>.FindByID(i);
                    if (detail != null)
                    {
                        SQLHelper<BillImportQCDetailModel>.Delete(detail);

                    }
                    ;
                }
            }

            if (isNew)
            {
                
                string emailCCs = "";
                foreach (int id in lsEmployeeID)
                {
                    string emailCC = SQLHelper<EmployeeModel>.FindByID(id).EmailCongTy;
                    if (!lsEmailCC.Contains(emailCC) && emailCC != "") lsEmailCC.Add(emailCC);
                }

                string emailCCRequest = SQLHelper<EmployeeModel>.FindByID(emRequestID)?.EmailCongTy;
                if (!lsEmailCC.Contains(emailCCRequest) && emailCCRequest != "") lsEmailCC.Add(emailCCRequest);

                if (lsEmailCC.Count > 0) emailCCs = string.Join(";", lsEmailCC);

                foreach (int leaderID in lsLeaderID)
                {
                    DataRow[] dtr = ((DataTable)grdDetails.DataSource).Select($"LeaderTechID = {leaderID}");

                    string emailEmRequest = SQLHelper<EmployeeModel>.FindByID(leaderID).EmailCongTy;
                    SetInforEmail(emailCCs, emailEmRequest, leaderID, dtr);
                }
            }
        }

    //    void SetInforEmail(string emailCCs, string emailEmRequest, int receivedEmID, DataRow[] dtr)
    //    {
    //        string subject = $"YÊU CẦU QC SẢN PHẨM";
    //        string leaderFullName = SQLHelper<EmployeeModel>.FindByID(receivedEmID).FullName;

    //        StringBuilder tableContent = new StringBuilder();

    //        foreach (var row in dtr)
    //        {
    //            string productName = TextUtils.ToString(row["ProductName"]);
    //            string Note = TextUtils.ToString(row["Note"]);
    //            int productID = TextUtils.ToInt(row["ProductSaleID"]);
    //            var product = lsProductSale.Where(x => x.ID == productID).FirstOrDefault();

    //            tableContent.Append($"<tr style=\"border: 1px solid black;\">");
    //            tableContent.Append($"<td style=\"border: 1px solid black; padding: 8px;\">{product.ProductCode}</td>");
    //            tableContent.Append($"<td style=\"border: 1px solid black; padding: 8px;\">{product.ProductName}</td>");
    //            tableContent.Append($"<td style=\"border: 1px solid black; padding: 8px;\">{Note}</td>");
    //            tableContent.Append("</tr>");
    //        }

    //        string body = $@"
				//<div>
				//	<p style=""font-weight: bold; color: red;"">[NO REPLY]</p>
				//	<p> Dear anh {leaderFullName}</p>
				//	<p> Nhân viên pur {cboEmployeeRequest.Text} yêu cầu QC {dtr.Count()} sản phẩm sau: </p>
				//</div>
				//<div style=""margin-top: 30px;"">
				//	<table style=""border-collapse: collapse; width: 100%;"">
				//		<tr style=""border: 1px solid black;"">
				//			<th style=""border: 1px solid black; padding: 8px;"">Mã sản phẩm</th>
				//			<th style=""border: 1px solid black; padding: 8px;"">Tên sản phẩm</th>
				//			<th style=""border: 1px solid black; padding: 8px;"">Ghi chú</th>
				//		</tr>
				//		{tableContent}
				//	</table>
				//</div>
				//<div style=""margin-top: 30px;"">
				//	<p style=""font-weight: bold; color: red;""> Dealine {dtpDealine.DateOnly}</p>
				//	<p> Thanks </p>
				//	<p> {cboEmployeeRequest.Text} </p>
				//</div>";
    //        EmployeeSendEmailModel e = new EmployeeSendEmailModel();
    //        e.Subject = subject;
    //        e.EmailTo = emailEmRequest;
    //        e.EmailCC = emailCCs;
    //        e.TableInfor = "BillImportQC";
    //        e.Body = body;
    //        e.StatusSend = 1;
    //        e.EmployeeID = Global.EmployeeID;
    //        e.Receiver = receivedEmID;
    //        SQLHelper<EmployeeSendEmailModel>.Insert(e);
    //    }

        void SetInforEmail(string emailCCs, string emailEmRequest, int receivedEmID, DataRow[] dtr) // NTA B update 151025
        {
            string subject = $"YÊU CẦU QC SẢN PHẨM";
            string leaderFullName = SQLHelper<EmployeeModel>.FindByID(receivedEmID).FullName;

            StringBuilder tableContent = new StringBuilder();

            foreach (var row in dtr)
            {
                int projectID = TextUtils.ToInt(row["ProjectID"]);
                string productName = TextUtils.ToString(row["ProductName"]);
                string Note = TextUtils.ToString(row["Note"]);
                int productID = TextUtils.ToInt(row["ProductSaleID"]);
                decimal quantity = TextUtils.ToInt(row["Quantity"]);
                var product = lsProductSale.Where(x => x.ID == productID).FirstOrDefault();
                var project = lstProject.Where(x => x.ID == projectID).FirstOrDefault();

                tableContent.Append($"<tr style=\"border: 1px solid black;\">");
                tableContent.Append($"<td style=\"border: 1px solid black; padding: 8px;\">{project.ProjectCode}</td>");
                tableContent.Append($"<td style=\"border: 1px solid black; padding: 8px;\">{product.ProductCode}</td>");
                tableContent.Append($"<td style=\"border: 1px solid black; padding: 8px;\">{product.ProductName}</td>");
                tableContent.Append($"<td style=\"border: 1px solid black; padding: 8px;\">{quantity}</td>");
                tableContent.Append($"<td style=\"border: 1px solid black; padding: 8px;\">{Note}</td>");
                tableContent.Append("</tr>");
            }

            string body = $@"
				<div>
					<p style=""font-weight: bold; color: red;"">[NO REPLY]</p>
					<p> Dear anh {leaderFullName}</p>
					<p> Nhân viên mua {cboEmployeeRequest.Text} yêu cầu QC {dtr.Count()} sản phẩm sau: </p>
					<p style=""font-weight: bold; color: red;""> Deadline {dtpDealine.DateOnly}</p>
				</div>
				<div style=""margin-top: 30px;"">
					<table style=""border-collapse: collapse; width: 100%;"">
						<tr style=""border: 1px solid black;"">
							<th style=""border: 1px solid black; padding: 8px;"">Dự án</th>
							<th style=""border: 1px solid black; padding: 8px;"">Mã sản phẩm</th>
							<th style=""border: 1px solid black; padding: 8px;"">Tên sản phẩm</th>
							<th style=""border: 1px solid black; padding: 8px;"">Số lượng</th>
							<th style=""border: 1px solid black; padding: 8px;"">Ghi chú</th>
						</tr>
						{tableContent}
					</table>
				</div>
				<div style=""margin-top: 30px;"">
					<p> Thanks </p>
					<p> {cboEmployeeRequest.Text} </p>
				</div>";
            EmployeeSendEmailModel e = new EmployeeSendEmailModel();
            e.Subject = subject;
            e.EmailTo = emailEmRequest;
            e.EmailCC = emailCCs;
            e.TableInfor = "BillImportQC";
            e.Body = body;
            e.StatusSend = 1;
            e.DateSend = DateTime.Now; //NTA B update 151025
            e.EmployeeID = Global.EmployeeID;
            e.Receiver = receivedEmID;
            SQLHelper<EmployeeSendEmailModel>.Insert(e);
        }

        private void btnContentDelete_Click(object sender, EventArgs e)
        {
            int stt = TextUtils.ToInt(grvDetails.GetFocusedRowCellValue(colDetailSTT));
            int idDetail = TextUtils.ToInt(grvDetails.GetFocusedRowCellValue(colDetailID));
            DialogResult dialog = MessageBox.Show($"Bạn có chắc chắn muốn xóa sản phẩm thứ [{stt}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                if (!lstDeletedDetails.Contains(idDetail) && idDetail > 0)
                {
                    lstDeletedDetails.Add(idDetail);

                    if (_dicFiles.ContainsKey(stt))
                    {
                        if (!_fileDeletes.ContainsKey(stt))
                            _fileDeletes.Add(stt, new List<BillImportQCDetailFilesModel>());
                        _fileDeletes[stt].AddRange(_dicFiles[stt]);
                    }
                }
                if (_dicFiles.ContainsKey(stt))
                    _dicFiles[stt].Clear();
                grvDetails.DeleteRow(grvDetails.FocusedRowHandle);
                // Cập nhật lại STT và đồng bộ lại dictionary
                var newDicFiles = new Dictionary<int, List<BillImportQCDetailFilesModel>>();
                var newFileDeletes = new Dictionary<int, List<BillImportQCDetailFilesModel>>();
                var newUploadFiles = new Dictionary<int, List<FileInfo>>();
                for (int i = 0; i < grvDetails.RowCount; i++)
                {
                    int newStt = i + 1;
                    grvDetails.SetRowCellValue(i, colDetailSTT, newStt);
                    // Nếu dictionary cũ chứa key theo STT cũ (dựa vào STT mới = i + 1)
                    if (newStt >= stt)
                    {
                        if (_dicFiles.TryGetValue(newStt + 1, out var dicFiles))
                        {
                            newDicFiles[newStt] = dicFiles;
                        }

                        if (_fileDeletes.TryGetValue(newStt + 1, out var deletes))
                        {
                            newFileDeletes[newStt] = deletes;
                        }

                        if (_uploadFiles.TryGetValue(newStt + 1, out var uploadFiles))
                        {
                            newUploadFiles[newStt] = uploadFiles;
                        }
                    }
                    else
                    {
                        if (_dicFiles.TryGetValue(newStt, out var dicFiles))
                        {
                            newDicFiles[newStt] = dicFiles;
                        }

                        if (_fileDeletes.TryGetValue(newStt, out var deletes))
                        {
                            newFileDeletes[newStt] = deletes;
                        }

                        if (_uploadFiles.TryGetValue(newStt, out var uploadFiles))
                        {
                            newUploadFiles[newStt] = uploadFiles;
                        }
                    }


                }

                _dicFiles = newDicFiles;
                _fileDeletes = newFileDeletes;
                _uploadFiles = newUploadFiles;
            }
            ((DataTable)grdDetails.DataSource).AcceptChanges();
        }

        private void grdDetail_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvDetails.CalcHitInfo(e.Location);

                if (info.Column != null && info.Column == colDetailSTT && info.HitTest == GridHitTest.Column)
                {
                    grvDetails.FocusedRowHandle = -1;

                    DataRow dtrow = dtDetails.NewRow();
                    dtrow["STT"] = grvDetails.RowCount + 1;
                    dtDetails.Rows.Add(dtrow);

                    grdDetails.DataSource = dtDetails;
                    dtDetails.AcceptChanges();
                }
            }
        }

        private void frmBillImportQCDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void grvDetails_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {

        }


        int[] statuss = new int[] { 0, 3 };
        private void grvDetails_ShowingEditor(object sender, CancelEventArgs e)
        {
            if ((Global.IsAdmin && Global.EmployeeID <= 0)) return;

            int emRequestID = TextUtils.ToInt(cboEmployeeRequest.EditValue);
            if (billImportQC.ID > 0)
            {
                if (grvDetails.FocusedColumn.FieldName == "LeaderTechID")
                {
                    emRequestID = TextUtils.ToInt(cboEmployeeRequest.EditValue);
                    if (emRequestID == Global.EmployeeID && emRequestID > 0) e.Cancel = false;
                    else e.Cancel = true;
                }
            }

            if (grvDetails.FocusedColumn.FieldName == "EmployeeTechID")
            {
                int leaderID = TextUtils.ToInt(grvDetails.GetFocusedRowCellValue(colLeaderTech));
                if (leaderID == Global.EmployeeID && leaderID > 0) e.Cancel = false;
                else e.Cancel = true;
            }

            if (grvDetails.FocusedColumn.FieldName == "Status")
            {
                int emTechID = TextUtils.ToInt(grvDetails.GetFocusedRowCellValue(colEmployeeTechID));
                if (emTechID == Global.EmployeeID && emTechID > 0) e.Cancel = false;
                else e.Cancel = true;
            }

            if (grvDetails.FocusedColumn.FieldName == "ProductSaleID" || grvDetails.FocusedColumn == colAdd)
            {
                int status = TextUtils.ToInt(grvDetails.GetFocusedRowCellValue(colStatus));
                if (statuss.Contains(status) && emRequestID == Global.EmployeeID && emRequestID > 0) e.Cancel = false;
                else e.Cancel = true;
            }

            if (isAddNewToBillImport && (grvDetails.FocusedColumn.FieldName == "ProductSaleID" || grvDetails.FocusedColumn == colAdd
                || grvDetails.FocusedColumn.FieldName == colDetailSTT.FieldName))
            {
                e.Cancel = true;
                grdDetails.MouseDown -= grdDetail_MouseDown;
            }
        }


        Dictionary<int, List<FileInfo>> _uploadFiles = new Dictionary<int, List<FileInfo>>();
        Dictionary<int, List<BillImportQCDetailFilesModel>> _fileDeletes = new Dictionary<int, List<BillImportQCDetailFilesModel>>();
        Dictionary<int, List<BillImportQCDetailFilesModel>> _dicFiles = new Dictionary<int, List<BillImportQCDetailFilesModel>>();
        public async Task UploadFile(BillImportQCDetailModel detail, int stt)
        {
            try
            {
                if (!_uploadFiles.ContainsKey(stt) || _uploadFiles[stt] == null) return;
                ConfigSystemModel config = SQLHelper<ConfigSystemModel>.FindByAttribute("KeyName", "BillImportQCDetailFiles").FirstOrDefault();
                if (config == null || string.IsNullOrEmpty(config.KeyValue))
                {
                    MessageBox.Show("Vui lòng chọn đường dẫn lưu trên server!", "Thông báo");
                    return;
                }

                ProjectModel project = SQLHelper<ProjectModel>.FindByID(detail.ProjectID ?? 0);

                BillImportQCModel order = SQLHelper<BillImportQCModel>.FindByID(TextUtils.ToInt(detail.BillImportQCID));
                if (order == null || order.ID <= 0) return;
                if (order.EmployeeRequestID != Global.EmployeeID && !Global.IsAdmin) return;

                string pathServer = config.KeyValue.Trim();
                //string pathPattern = $@"NĂM {order.RequestDateQC.Value.Year}\THÁNG {order.RequestDateQC.Value.ToString("MM.yyyy")}\{order.RequestDateQC.Value.ToString("dd.MM.yyyy")}\{order.RequestImportCode}";
                int year = project.CreatedDate.HasValue ? project.CreatedDate.Value.Year : DateTime.Now.Year;
                string projectCode = string.IsNullOrWhiteSpace(project.ProjectCode) ? "" : project.ProjectCode.Trim();

                string pathPattern = $@"{year}\{projectCode}\TaiLieuChung\QC\{order.RequestImportCode}";
                string pathUpload = Path.Combine(pathServer, pathPattern);
                //string pathUpload = "\\\\192.168.1.190\\Common\\08. SOFTWARES\\LeTheAnh\\DemoUploadFile";

                var client = new HttpClient();

                //List<BillImportQCDetailFilesModel> listFiles = new List<BillImportQCDetailFilesModel>();
                foreach (var file in _uploadFiles[stt])
                {
                    _dicFiles.TryGetValue(stt, out List<BillImportQCDetailFilesModel> lstFile);

                    BillImportQCDetailFilesModel fileOrder = lstFile.FirstOrDefault(p => p.FileName == file.Name) ?? new BillImportQCDetailFilesModel();
                    fileOrder.BillImportQCDetailID = detail.ID;
                    fileOrder.FileName = file.Name;
                    fileOrder.OriginPath = file.DirectoryName;
                    fileOrder.ServerPath = pathUpload;

                    if (file.Length < 0) continue;

                    byte[] bytes;
                    using (var fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
                    {
                        bytes = new byte[fileStream.Length];
                        await fileStream.ReadAsync(bytes, 0, (int)fileStream.Length);
                    }

                    var byteArrayContent = new ByteArrayContent(bytes);

                    MultipartFormDataContent content = new MultipartFormDataContent();
                    content.Add(byteArrayContent, "file", file.Name);

                    var url = $"http://113.190.234.64:8083/api/Home/uploadfile?path={pathUpload}";
                    var result = await client.PostAsync(url, content);
                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        SQLHelper<BillImportQCDetailFilesModel>.Insert(fileOrder);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void HandleAttachFile(int mode)
        {
            DataRow focusedRow = grvDetails.GetFocusedDataRow();
            int stt = TextUtils.ToInt(focusedRow["STT"]);
            var frm = new frmAttachFileImportQC(mode)
            {
                FocusedRow = focusedRow,
                ListFileViews = _dicFiles.ContainsKey(stt) ? _dicFiles[stt] : new List<BillImportQCDetailFilesModel>()
            };
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (!_dicFiles.ContainsKey(stt))
                {
                    _dicFiles[stt] = new List<BillImportQCDetailFilesModel>();
                    _dicFiles[stt].AddRange(frm.ListFiles);
                }

                foreach (var file in frm.ListFileDelete)
                {
                    _dicFiles[stt].Remove(file);
                    if (_uploadFiles.ContainsKey(stt))
                        _uploadFiles[stt].RemoveAll(p => p.Name == file.FileName);
                }
                if (!_fileDeletes.ContainsKey(stt)) _fileDeletes[stt] = new List<BillImportQCDetailFilesModel>();
                _fileDeletes[stt].AddRange(frm.ListFileDelete);
                if (!_uploadFiles.ContainsKey(stt)) _uploadFiles[stt] = new List<FileInfo>();
                _uploadFiles[stt].AddRange(frm.ListFileUpload);
                string data = string.Join(";", _dicFiles[stt].Where(p => p.FileType == mode).Select(p => p.FileName));
                if (mode == 1)
                {
                    grvDetails.SetFocusedRowCellValue("CheckSheet", data);
                }
                else
                {
                    grvDetails.SetFocusedRowCellValue("Report", data);
                }




            }
        }
        public async void RemoveFile(int stt)
        {
            try
            {
                if (!_fileDeletes.ContainsKey(stt)) return;
                if (_fileDeletes.Count <= 0) return;
                var url = $"http://113.190.234.64:8083/api/Home/removefile?path=";
                //var url = $"http://localhost:8390/api/Home/removefile?path=";
                var client = new HttpClient();
                foreach (var item in _fileDeletes[stt])
                {
                    url += $@"{item.ServerPath}\{item.FileName}";
                    var result = await client.GetAsync(url);

                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        SQLHelper<BillImportQCDetailFilesModel>.Delete(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }


        void DownloadFile(GridView view)
        {
            try
            {
                if (view == null) return;
                //string tableName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colTableName));
                string api = "http://113.190.234.64:8083/api";
                //string folderName = "";
                //if (tableName == "EmployeeBussiness") folderName = "CongTac";
                //else if (tableName == "EmployeeOvertime") folderName = "LamThem";
                //else return;


                //api = @"http://192.168.1.2:8083/api/lamthem/Năm 2025/Tháng 7/Ngày 30.07.2025/NV076/1031533_cancel_close_cross_delete_remove_icon.png";

                string filePath = TextUtils.ToString(view.GetFocusedRowCellValue("FilePath"));
                string fileName = TextUtils.ToString(view.GetFocusedRowCellValue("FileName"));
                if (string.IsNullOrWhiteSpace(fileName)) return;

                string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string pathDownload = Path.Combine(userFolder, "Downloads", "Yêu Cầu QC");

                if (!Directory.Exists(pathDownload))
                {
                    Directory.CreateDirectory(pathDownload);
                }

                Uri uri = new Uri(api + filePath);
                WebClient webClient = new WebClient();
                webClient.DownloadFile(uri, Path.Combine(pathDownload, fileName));
                Process.Start(pathDownload);
                Process.Start(Path.Combine(pathDownload, fileName));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không tìm thấy file!\r\n{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }

        }

        private void btnAttachCheckSheet_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            HandleAttachFile(1);
        }

        private void btnAttachReport_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            HandleAttachFile(2);
        }

        private void grvDetails_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDataFile();
        }

        private void grvDataCheckSheet_DoubleClick(object sender, EventArgs e)
        {
            DownloadFile(sender as GridView);
        }

        private void grvDataTestReport_DoubleClick(object sender, EventArgs e)
        {
            DownloadFile(sender as GridView);
        }

        bool isRecallCellValueChanged = false;
        private void grvDetails_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gridView = (GridView)sender;
            if (gridView == null) return;

            bool isColumn = e.Column.FieldName == colLeaderTech.FieldName || e.Column.FieldName == colEmployeeTechID.FieldName;
            if (!isColumn) return;
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


                        if (isColumn)
                        {
                            foreach (int row in selectedRows)
                            {
                                gridView.SetRowCellValue(row, gridView.Columns[e.Column.FieldName], e.Value);
                            }
                        }

                    }
                }
            }
            finally
            {
                isRecallCellValueChanged = false;
            }
        }
    }
}
