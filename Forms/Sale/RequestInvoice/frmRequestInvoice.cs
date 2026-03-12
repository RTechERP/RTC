using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using BMS.Business;
using ClosedXML.Excel;
using BMS.Model;
using System.IO;
using System.Diagnostics;
using System.Net;
using DevExpress.XtraGrid.Views.Grid;
using Forms.Sale.RequestInvoice;
using BMS.Utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DevExpress.DataProcessing.InMemoryDataProcessor;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;

namespace BMS
{
    public partial class frmRequestInvoice : _Forms
    {
        List<ProjectModel> lsProject = new List<ProjectModel>();
        int _warehouseID = 0;
        //WarehouseModel _warehouse = new WarehouseModel();
        public frmRequestInvoice(int warehouseID)
        {
            InitializeComponent();
            _warehouseID = warehouseID;
           
        }

        private void frmRequestInvoice_Load(object sender, EventArgs e)
        {
            this.Text = $"YÊU CẦU XUẤT HÓA ĐƠN - {this.Tag}";

            //grdData.ContextMenuStrip = contextMenuStrip1;
            grdFile.ContextMenuStrip = contextMenuStrip2;
            grdFilePO.ContextMenuStrip = contextMenuStrip3;

            DateTime datenow = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            dtpFromDate.Value = datenow.AddMonths(-1);
            DateTime currentDate = DateTime.Now;
            dtpFromDate.Value = new DateTime(currentDate.Year, currentDate.Month, 1);
            dtpEndDate.Value = dtpFromDate.Value.AddMonths(1).AddDays(-1);

            loadRequestInvoice();
        }

        private void loadRequestInvoice()
        {
            DateTime dateStart = new DateTime(dtpFromDate.Value.Year, dtpFromDate.Value.Month, dtpFromDate.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpEndDate.Value.Year, dtpEndDate.Value.Month, dtpEndDate.Value.Day, 23, 59, 59);
            string keyWords = txtKeywords.Text.Trim();
            DataTable dt = TextUtils.LoadDataFromSP("spGetRequestInvoice", "spGetRequestInvoice", 
                                                    new string[] { "@DateStart", "@DateEnd", "@Keywords", "@WarehouseID" },
                                                    new object[] { dateStart, dateEnd, keyWords , _warehouseID});

            grdData.DataSource = dt;
            if (dt.Rows.Count == 0) return;
            LoadDetails();
            LoadPOKHFile();
            LoadFile();
        }
        private void LoadDetails()
        {
            //RequestInvoiceModel requestInvoice = new RequestInvoiceModel();
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            DataSet ds = TextUtils.LoadDataSetFromSP("spGetRequestInvoiceDetailsByID", new string[] { "@RequestInvoiceID" }, new object[] { id });
            grdDetail.DataSource = ds.Tables[0];
            grdFile.DataSource = ds.Tables[1];

            LoadPOKHFile();
        }

        void LoadFile()
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));

            var exp1 = new Expression(RequestInvoiceFileModel_Enum.RequestInvoiceID.ToString(), id);//NTA B update 22/09/25
            var exp2 = new Expression(RequestInvoiceFileModel_Enum.FileType.ToString(), 1);//NTA B update 22/09/25


            //List<RequestInvoiceFileModel> files = SQLHelper<RequestInvoiceFileModel>.FindByAttribute(RequestInvoiceFileModel_Enum.RequestInvoiceID.ToString(), id);
            List<RequestInvoiceFileModel> files = SQLHelper<RequestInvoiceFileModel>.FindByExpression(exp1.And(exp2));//NTA B update 22/09/25
            grdFile.DataSource = files;
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void grdFile_Click(object sender, EventArgs e)
        {

        }

        private void grdDetail_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = grvData.FocusedRowHandle;
            frmRequestInvoiceDetail frm = new frmRequestInvoiceDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {

                loadRequestInvoice();
                grvData.FocusedRowHandle = rowHandle;
                //grvData_FocusedRowChanged(null, null);

            }
        }
        void LoadPOKHFile()
        {
            int id = TextUtils.ToInt(grvDetail.GetFocusedRowCellValue(colPOKHID));
            List<POKHFileModel> files = SQLHelper<POKHFileModel>.FindByAttribute("POKHID", id);
            grdFilePO.DataSource = files;
        }
        private void grvDetail_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadPOKHFile();
        }

        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (ID == 0) return;
            RequestInvoiceModel model = SQLHelper<RequestInvoiceModel>.FindByID(ID);
            frmRequestInvoiceDetail frm = new frmRequestInvoiceDetail();
            frm.requestInvoice = model;
            frm.IDDetail = ID;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadRequestInvoice();
                grvData.FocusedRowHandle = focusedRowHandle;
                grvData_FocusedRowChanged(null, null);
            }

        }

        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetails();
        }

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var focusedRowHandle = grvData.FocusedRowHandle;

            string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            //if (isApproved == true)
            //{
            //    MessageBox.Show(String.Format("Bạn không thể xóa phiếu xuất [{0}] này?", code), TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Question);
            //    return;
            //}
            if (!grvData.IsDataRow(grvData.FocusedRowHandle)) return;
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            if (ID == 0) return;
            if (MessageBox.Show(String.Format("Bạn có chắc muốn xóa hóa đơn xuất [{0}] không?", code), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //RequestInvoiceBO.Instance.Delete(ID);
                //RequestInvoiceDetailBO.Instance.DeleteByAttribute("RequestInvoiceID", ID);


                var myDict = new Dictionary<string, object>()
                {
                    { RequestInvoiceModel_Enum.IsDeleted.ToString(),true},
                    { RequestInvoiceModel_Enum.UpdatedBy.ToString(),Global.AppUserName},
                    { RequestInvoiceModel_Enum.UpdatedDate.ToString(),DateTime.Now},
                };

                SQLHelper<RequestInvoiceModel>.UpdateFieldsByID(myDict, ID);
                grvData.DeleteSelectedRows();
                grvData.FocusedRowHandle = focusedRowHandle;
                grvData_FocusedRowChanged(null, null);
            }
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadRequestInvoice();
        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            GridView view = (GridView)sender;
            if (view == null) return;

            //int rowHandle = grvData.FocusedRowHandle;
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn));
                if (string.IsNullOrWhiteSpace(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void grdData_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_ItemClick(null, null);
        }

        private void yềuCầuXuấtHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //if (MessageBox.Show("Bạn có muốn đổi trạng thái thành Yêu cầu xuất hóa đơn không ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    //TextUtils.ExcuteSQL($"Update RequestInvoice set Status = 1 where ID ={ID}");
            //    var myDict = new Dictionary<string, object>()
            //    {
            //        { RequestInvoiceModel_Enum.Status.ToString(),1},
            //        { RequestInvoiceModel_Enum.UpdatedBy.ToString(),Global.AppUserName},
            //        { RequestInvoiceModel_Enum.UpdatedDate.ToString(),DateTime.Now},
            //    };

            //    SQLHelper<RequestInvoiceModel>.UpdateFieldsByID(myDict, ID);

            //    loadRequestInvoice();
            //    LoadDetails();
            //}

            UpdateInvoiceStatus(1);
        }

        private void DaXuatNhapToolStripMenu_Click(object sender, EventArgs e)
        {
            //int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //if (MessageBox.Show("Bạn có muốn đổi trạng thái thành Đã xuất nháp không ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    //TextUtils.ExcuteSQL($"Update RequestInvoice set Status = 2 where ID ={ID}");

            //    var myDict = new Dictionary<string, object>()
            //    {
            //        { RequestInvoiceModel_Enum.Status.ToString(),2},
            //        { RequestInvoiceModel_Enum.UpdatedBy.ToString(),Global.AppUserName},
            //        { RequestInvoiceModel_Enum.UpdatedDate.ToString(),DateTime.Now},
            //    };

            //    SQLHelper<RequestInvoiceModel>.UpdateFieldsByID(myDict, ID);

            //    LoadDetails();
            //    loadRequestInvoice();
            //}

            UpdateInvoiceStatus(2);
        }

        private void DaPhatHanhHoaDonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //if (MessageBox.Show("Bạn có muốn đổi trạng thái thành Đã phát hành hóa đơn không ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    //TextUtils.ExcuteSQL($"Update RequestInvoice set Status = 3 where ID ={ID}");
            //    var myDict = new Dictionary<string, object>()
            //    {
            //        { RequestInvoiceModel_Enum.Status.ToString(),3},
            //        { RequestInvoiceModel_Enum.UpdatedBy.ToString(),Global.AppUserName},
            //        { RequestInvoiceModel_Enum.UpdatedDate.ToString(),DateTime.Now},
            //    };

            //    SQLHelper<RequestInvoiceModel>.UpdateFieldsByID(myDict, ID);

            //    LoadDetails();
            //    loadRequestInvoice();
            //}

            UpdateInvoiceStatus(3);
        }

        private void grdData_MouseClick(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            //{
            //    contextMenuStrip1.Show(this, this.PointToClient(MousePosition));
            //}
        }

        private void btnTreeFolder_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int requestInvoiceID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
                ConfigSystemModel config = SQLHelper<ConfigSystemModel>.FindByAttribute("KeyName", "RequestInvoiceFile").FirstOrDefault();
                if (config == null || string.IsNullOrEmpty(config.KeyValue))
                {
                    MessageBox.Show("Vui lòng chọn đường dẫn lưu trên server!", "Thông báo");
                    return;
                }

                RequestInvoiceModel requestInvoice = SQLHelper<RequestInvoiceModel>.FindByID(requestInvoiceID);
                if (requestInvoice == null || requestInvoice.ID <= 0) return;
                if (requestInvoice.EmployeeRequestID != Global.EmployeeID && !Global.IsAdmin) return;

                DateTime dateRequest = requestInvoice.CreatedDate.Value;
                string pathServer = config.KeyValue.Trim();
                string pathPattern = $@"{dateRequest.ToString("yyyy")}\T{dateRequest.ToString("MM")}\{requestInvoice.Code}";
                string pathUpload = Path.Combine(pathServer, pathPattern);

                Process.Start(pathUpload);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }

        private void btnViewAttachFile_Click(object sender, EventArgs e)
        {
            try
            {
                string path = TextUtils.ToString(grvFile.GetFocusedRowCellValue(colServerPath));
                string fileName = TextUtils.ToString(grvFile.GetFocusedRowCellValue(colFileName));

                Process.Start(Path.Combine(path, fileName));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}","Thông báo");
            }
        }

        private void btnDownloadAttachFile_Click(object sender, EventArgs e)
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

                DateTime dateRequest = TextUtils.ToDate5(grvData.GetFocusedRowCellValue(colCreatedDate));
                string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
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

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e) //NTA B -update 28/08/25
        {
            bool isUrgent = TextUtils.ToBoolean(grvData.GetRowCellValue(e.RowHandle, colIsUrgency));
            if(isUrgent)
            {
                e.Appearance.BackColor = Color.Orange;
            }
            var view = sender as GridView;
            if (view.FocusedRowHandle == e.RowHandle)
            {
                e.Appearance.BackColor = Color.GreenYellow;
            }
        }

        private void btnRequestInvoiceSummary_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmRequestInvoiceSummary frm = new frmRequestInvoiceSummary();
            frm.Show();
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
            int[] selectedRows = grvData.GetSelectedRows();
            int[] ids = selectedRows
                .Select(r => TextUtils.ToInt(grvData.GetRowCellValue(r, colID)))
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
                string idText = string.Join(",", ids);
                var exp = new Expression(RequestInvoiceModel_Enum.ID, idText, "IN");
                SQLHelper<RequestInvoiceModel>.UpdateFields(myDict, exp);
                //}

                LoadDetails();
                loadRequestInvoice();
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateInvoiceStatus(2);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateInvoiceStatus(1);
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateInvoiceStatus(3);
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateInvoiceStatus(4);
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateInvoiceStatus(5);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UpdateInvoiceStatus(4);
        }

        private void yêuCầuBổSungChứngTừToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateInvoiceStatus(5);
        }

        private void grvFilePO_DoubleClick(object sender, EventArgs e)
        {
            btnViewFile_Click(null, null);
        }

        private void btnViewFile_Click(object sender, EventArgs e)
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

        private void btnDownFile_Click(object sender, EventArgs e)
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
                POKHModel pokh = SQLHelper<POKHModel>.FindByID(TextUtils.ToInt(grvDetail.GetFocusedRowCellValue(colPOKHID)));

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

        private void grvFile_DoubleClick(object sender, EventArgs e)
        {
            btnViewAttachFile_Click(null, null);
        }

        private void btnRequestInvoiceLinkStatus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmStatusRequestInvoiceLink frm = new frmStatusRequestInvoiceLink();
            int selectedId = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (selectedId == 0)
            {
                MessageBox.Show("Vui lòng chọn yêu cầu xuất hóa đơn để thiết lập trạng thái!", "Thông báo");
                return;
            }
            frm.requestInvoiceID = selectedId;
            frm.customerID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colCustomerID));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadRequestInvoice();
            }
        }

        private void btnExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                    printableComponentLink1.Component = grdData;

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
    }
}