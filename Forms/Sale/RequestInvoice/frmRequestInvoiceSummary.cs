using BMS;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using BMS.Utils;
using DevExpress.XtraGrid.Views.Grid;
using BMS.Model;
using System.IO;
using System.Net;

namespace Forms.Sale.RequestInvoice
{
    //NTA B - update 28/08/2025
    public partial class frmRequestInvoiceSummary : _Forms
    {
        DataTable dtCustomer = new DataTable();
        public frmRequestInvoiceSummary()
        {
            InitializeComponent();
        }

        private void frmRequestInvoiceSummary_Load(object sender, EventArgs e)
        {
            grdFile.ContextMenuStrip = contextMenuStrip2;
            grdFilePO.ContextMenuStrip = contextMenuStrip3;
            DateTime datenow = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            dtpFromDate.Value = datenow.AddMonths(-1);
            DateTime currentDate = DateTime.Now;
            dtpFromDate.Value = new DateTime(currentDate.Year, currentDate.Month, 1);
            dtpEndDate.Value = dtpFromDate.Value.AddMonths(1).AddDays(-1);
            loadStatus();
            loadCustomer();
            loadUsers();
            loadData();
        }

        private void loadData()
        {
            DateTime dateStart = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);
            string keyWords = txtKeywords.Text.Trim();
            int customerId = TextUtils.ToInt(cboCustomer.EditValue);
            int userId = TextUtils.ToInt(cboUser.EditValue);
            int status = TextUtils.ToInt(cboStatus.EditValue);

            DataTable dt = TextUtils.LoadDataFromSP("spGetRequestInvoiceSummary", "A",
                new string[] { "@DateStart", "@DateEnd", "@Keywords", "@CustomerID", "@UserID", "@Status" },
                new object[] { dateStart, dateEnd, keyWords, customerId, userId, status });

            grdMaster.DataSource = dt;
        }
        public void loadUsers()
        {
            List<EmployeeModel> lstEmployee = SQLHelper<EmployeeModel>.FindByAttribute("Status", 0);
            cboUser.Properties.DisplayMember = "FullName";
            cboUser.Properties.ValueMember = "ID";
            cboUser.Properties.DataSource = lstEmployee;
            cboUser.EditValue = Global.EmployeeID;
        }
        public void loadCustomer()
        {

            dtCustomer = new DataTable();
            dtCustomer = TextUtils.Select("SELECT * FROM Customer where IsDeleted <> 1");
            cboCustomer.Properties.DisplayMember = "CustomerName";
            cboCustomer.Properties.ValueMember = "ID";
            cboCustomer.Properties.DataSource = dtCustomer;
        }
        private void loadStatus()
        {
            List<object> list = new List<object>()
            {
                new {ID = 1, Status = "Yêu cầu xuất hóa đơn"},
                new {ID = 2, Status = "Đã xuất nháp"},
                new {ID = 3, Status = "Đã phát hành hóa đơn"},
                new {ID = 4, Status = "Đã nhận biên bản"},
                new {ID = 5, Status = "Yêu cầu bổ sung biên bản"},

            };
            cboStatus.Properties.DataSource = list;
            cboStatus.Properties.ValueMember = "ID";
            cboStatus.Properties.DisplayMember = "Status";

        }
        private void LoadDetails()
        {
            RequestInvoiceModel requestInvoice = new RequestInvoiceModel();
            int id = TextUtils.ToInt(grvMaster .GetFocusedRowCellValue(colRequestInvoiceID));
            DataSet ds = TextUtils.LoadDataSetFromSP("spGetRequestInvoiceDetailsByID", new string[] { "@RequestInvoiceID" }, new object[] { id });
            grdFile.DataSource = ds.Tables[1];

            LoadPOKHFile();
        }
        void LoadPOKHFile()
        {
            int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colPOKHID));
            List<POKHFileModel> files = SQLHelper<POKHFileModel>.FindByAttribute("POKHID", id);
            grdFilePO.DataSource = files;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnExcelExport_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Excel Files|*.xlsx";
            f.FileName = $"TongHopYeuCauXuatHoaDon_{DateTime.Now.ToString("ddMMyy")}.xlsx";

            if (f.ShowDialog() == DialogResult.OK)
            {
                using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
                {
                    //string filepath = Path.Combine(f.SelectedPath, $"");
                    string filepath = f.FileName;

                    XlsxExportOptions optionsEx = new XlsxExportOptions();
                    PrintingSystem printingSystem = new PrintingSystem();

                    PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                    printableComponentLink1.Component = grdMaster;

                    try
                    {
                        CompositeLink compositeLink = new CompositeLink(printingSystem);
                        compositeLink.Links.Add(printableComponentLink1);

                        compositeLink.CreatePageForEachLink();
                        optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;

                        compositeLink.PrintingSystem.SaveDocument(filepath);
                        compositeLink.ExportToXlsx(filepath, optionsEx);
                        Process.Start(filepath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }


        /// <summary>
        /// Cập nhật trạng thái của các hóa đơn được chọn trong lưới dữ liệu.
        /// Nếu trạng thái là "Yêu cầu bổ sung biên bản" (status = 5), yêu cầu người dùng nhập lý do chỉnh sửa.
        /// </summary>
        /// <param name="status">
        /// Trạng thái cần cập nhật cho hóa đơn:
        /// 1: Yêu cầu xuất hóa đơn
        /// 2: Đã xuất nháp
        /// 3: Đã phát hành hóa đơn
        /// 4: Đã nhận biên bản
        /// 5: Yêu cầu bổ sung biên bản (có yêu cầu nhập lý do chỉnh sửa)
        /// </param>
        private void UpdateInvoiceStatus(int status)
        {
            string message = "";
            string amendReason = "";
            int[] selectedRows = grvMaster.GetSelectedRows();
            int[] ids = selectedRows
                .Select(r => TextUtils.ToInt(grvMaster.GetRowCellValue(r, colRequestInvoiceID)))
                .Where(id => id > 0)
                .ToArray();
            

            if (ids.Length == 0)
            {
                MessageBox.Show("Bạn chưa chọn dòng cần cập nhật trạng thái");
                return;
            }
            if (status == 1)
            {
                message = "Bạn có muốn đổi trạng thái thành Yêu cầu xuất hóa đơn không ?";
            }
            else if (status == 2)
            {
                message = "Bạn có muốn đổi trạng thái thành Đã xuất nháp không ?";
            }
            else if (status == 3)
            {
                message = "Bạn có muốn đổi trạng thái thành Đã phát hành hóa đơn không ?";
            }
            else if (status == 4)
            {
                message = "Bạn có muốn đổi trạng thái thành Đã nhận biên bản không ?";
            }
            else if (status == 5)
            {
                message = "Bạn có muốn đổi trạng thái thành Yêu cầu bổ sung biên bản không ?";
                frmAmendReasonRequestInvoice frm = new frmAmendReasonRequestInvoice();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    amendReason = frm.AmendReason;
                }
                else return;
            }
            if (MessageBox.Show(message, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //foreach (int id in ids)
                //{
                var myDict = new Dictionary<string, object>()
                {
                    { RequestInvoiceModel_Enum.Status.ToString(), status },
                    { RequestInvoiceModel_Enum.UpdatedBy.ToString(), Global.AppUserName },
                    { RequestInvoiceModel_Enum.UpdatedDate.ToString(), DateTime.Now },
                    { RequestInvoiceModel_Enum.AmendReason.ToString(), amendReason ?? "" }
                };

                //SQLHelper<RequestInvoiceModel>.UpdateFieldsByID(myDict, id);
                var exp = new Expression(RequestInvoiceModel_Enum.ID, string.Join(",", ids), "IN");
                SQLHelper<RequestInvoiceModel>.UpdateFields(myDict, exp);
                //}

                loadData();
            }
        }

        private void btnYeuCauXuat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateInvoiceStatus(1);
        }

        private void btnXuatNhap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateInvoiceStatus(2);
        }

        private void btnDaPhatHanhHoaDon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateInvoiceStatus(3);
        }

        private void btnDaNhanBienBan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateInvoiceStatus(4);
        }

        private void btnYeuCauBoSung_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateInvoiceStatus(5);
        }

        private void grvMaster_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            bool isUrgent = TextUtils.ToBoolean(grvMaster.GetRowCellValue(e.RowHandle, colIsUrgency));
            if (isUrgent)
            {
                e.Appearance.BackColor = Color.Orange;
            }
            var view = sender as GridView;
            if (view.FocusedRowHandle == e.RowHandle)
            {
                e.Appearance.BackColor = Color.GreenYellow;
            }
        }

        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetails();
        }

        private void btnViewAttachFile_Click_1(object sender, EventArgs e)
        {
            try
            {
                string path = TextUtils.ToString(grvFile.GetFocusedRowCellValue(colServerPath));
                string fileName = TextUtils.ToString(grvFile.GetFocusedRowCellValue(colFileName));

                Process.Start(Path.Combine(path, fileName));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }

        private void btnDownloadAttachFile_Click_1(object sender, EventArgs e)
        {
            try
            {
                //string pathDownload = Path.Combine(KnownFolders.Downloads.Path, "DeNghiThanhToan");
                //string pathDownload = Path.Combine(Application.StartupPath, "DeNghiThanhToan");

                string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string pathDownload = Path.Combine(userFolder, "Downloads", "YeuCauXuatHoaDon");

                if (!Directory.Exists(pathDownload))
                {
                    Directory.CreateDirectory(pathDownload);
                }

                DateTime dateRequest = TextUtils.ToDate5(grvMaster.GetFocusedRowCellValue(colCreatedDate));
                string code = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colCode));
                string fileName = TextUtils.ToString(grvFile.GetFocusedRowCellValue(colFileName));
                //string pathServer = config.KeyValue.Trim();
                string pathPattern = $@"{dateRequest.ToString("yyyy")}\T{dateRequest.ToString("MM")}\{code}";
                string folderDownload = Path.Combine(pathDownload, fileName);

                //Process.Start(pathUpload);

                //string pathPattern = $@"NĂM {dateOrder.Year}/ĐỀ NGHỊ THANH TOÁN/THÁNG {dateOrder.ToString("MM.yyyy")}/{dateOrder.ToString("dd.MM.yyyy")}/{code}";

                //string fileName = TextUtils.ToString(grvDataFile.GetFocusedRowCellValue(colFileName));
                //string folderDownload = Path.Combine(pathDownload, fileName);
                string url = $"http://113.190.234.64:8083/api/ycxuathoadon/{pathPattern}/{fileName}";

                WebClient webClient = new WebClient();
                webClient.DownloadFile(url, folderDownload);
                Process.Start(folderDownload);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }

        private void btnViewFile_Click_1(object sender, EventArgs e)
        {
            try
            {
                string path = TextUtils.ToString(grvFilePO.GetFocusedRowCellValue(colServerPath));
                string fileName = TextUtils.ToString(grvFilePO.GetFocusedRowCellValue(colFileName));

                Process.Start(Path.Combine(path, fileName));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDownFile_Click_1(object sender, EventArgs e)
        {
            try
            {
                string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string pathDownload = Path.Combine(userFolder, "Downloads", "POKH");

                if (!Directory.Exists(pathDownload))
                {
                    Directory.CreateDirectory(pathDownload);
                }
                string pathServer = "pokhhn";
                POKHModel pokh = SQLHelper<POKHModel>.FindByID(TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colPOKHID)));

                int warehouseID = pokh.WarehouseID;
                if (warehouseID == 2) pathServer = "pokhhcm";
                //string poNumber = TextUtils.ToString(grvMaster.GetFocusedRowCellValue("PONumber"));
                string poNumber = TextUtils.ToString(pokh.PONumber);

                string fileName = TextUtils.ToString(grvFilePO.GetFocusedRowCellValue(colFileName));
                string folderDownload = Path.Combine(pathDownload, fileName);
                string url = $"http://113.190.234.64:8083/api/{pathServer}/{poNumber}/{fileName}";

                WebClient webClient = new WebClient();
                webClient.DownloadFile(url, folderDownload);
                Process.Start(folderDownload);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void splitContainerControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void grvMaster_RowStyle_1(object sender, RowStyleEventArgs e)
        {
            bool isUrgent = TextUtils.ToBoolean(grvMaster.GetRowCellValue(e.RowHandle, colIsUrgency));
            if (isUrgent)
            {
                e.Appearance.BackColor = Color.Orange;
            }
            var view = sender as GridView;
            if (view.FocusedRowHandle == e.RowHandle)
            {
                e.Appearance.BackColor = Color.GreenYellow;
            }
        }

        private void btnRequestInvoiceLinkStatus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmStatusRequestInvoiceLink frm = new frmStatusRequestInvoiceLink();
            int selectedId = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue("ID"));
            if (selectedId == 0)
            {
                MessageBox.Show("Vui lòng chọn yêu cầu xuất hóa đơn để thiết lập trạng thái!", "Thông báo");
                return;
            }
            frm.requestInvoiceID = selectedId;
            frm.customerID = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue("CustomerID"));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }
    }
}
