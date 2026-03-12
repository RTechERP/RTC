using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using Syroot.Windows.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using GridView = DevExpress.XtraGrid.Views.Grid.GridView;
//using System.Web.UI.WebControls;

namespace BMS
{
    public partial class frmPaymentOrder : _Forms
    {

        DataSet dataSet = new DataSet();
        DataSet dataSetPaymentSpe = new DataSet();

        int step = 0;
        int approvedTBPID = 0;

        public frmPaymentOrder(int tbp)
        {
            InitializeComponent();

            //step = tbp ? 2 : 0;
            //approvedTBPID = tbp ? Global.EmployeeID : 0;


            //NXL Update 13/10/25
            if (tbp == 1)
            {
                step = 2;
                approvedTBPID = Global.EmployeeID;
            }
            else
            {
                step = 0;
                approvedTBPID = 0;
            }

            //  step = tbp ? 2 : 0;
            // approvedTBPID = tbp ? Global.EmployeeID : 0;

            if (tbp == 1)//TBP đăng nhập
            {
                btnApproveTBP_New.Visible = true;
                btnUnApproveTBP_New.Visible = true;
                toolStripSeparator14.Visible = true;

                toolStripSeparator12.Visible = false;
                toolStripSeparator11.Visible = false;
                toolStripSeparator13.Visible = false;
                toolStripSeparator15.Visible = false;
                toolStripSeparator8.Visible = false;
                toolStripSeparator7.Visible = false;
                toolStripSeparator4.Visible = false;
                toolStripSeparator5.Visible = false;
                btnAdd.Visible = false;
                btnEdit.Visible = false;
                btnDelete.Visible = false;
                btnTBP.Visible = false;
                btnHR.Visible = false;
                btnKTTT.Visible = false;
                btnKTT.Visible = false;
                btnBGĐ.Visible = false;
                btnTreeFolder.Visible = false;
            }
            if (tbp == 2)//User bình thường đăng nhập
            {
                btnApproveTBP_New.Visible = false;
                btnUnApproveTBP_New.Visible = false;
                toolStripSeparator14.Visible = false;

                toolStripSeparator12.Visible = false;
                toolStripSeparator11.Visible = false;
                toolStripSeparator13.Visible = false;
                toolStripSeparator15.Visible = false;
                toolStripSeparator8.Visible = false;
                toolStripSeparator7.Visible = false;
                toolStripSeparator4.Visible = false;
                toolStripSeparator5.Visible = false;
                btnAdd.Visible = true;
                btnEdit.Visible = true;
                btnDelete.Visible = true;
                btnTBP.Visible = false;
                btnHR.Visible = false;
                btnKTTT.Visible = false;
                btnKTT.Visible = false;
                btnBGĐ.Visible = false;
                btnTreeFolder.Visible = false;
                cboEmployee.EditValue = Global.EmployeeID;
                cboEmployee.Enabled = false;
            }// tbp=0: kế toán đăng nhập

            //NXL End Update 13/10/25
        }

        private void frmPaymentOrder_Load(object sender, EventArgs e)
        {
            Lib.LockEvents = true;
            dtpDateStart.Value = new DateTime(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, 1);
            dtpDateEnd.Value = dtpDateStart.Value.AddMonths(+2).AddSeconds(-1);
            cboTypeOrder.SelectedIndex = 0;
            cboIsApproved.SelectedIndex = 0;

            LoadDepartment();
            LoadEmployee();
            LoadPaymentOrderType();
            LoadData();
            Lib.LockEvents = false;
            //string pathDownload = KnownFolders.Downloads.Path;

            grdDataFile.ContextMenuStrip = grdSpeFile.ContextMenuStrip = contextMenuStrip2;
            grdFileBackSlip.ContextMenuStrip = contextMenuStrip3;
            grdFileBackSlip2.ContextMenuStrip = contextMenuStrip3;

        }

        void LoadDepartment()
        {
            //DataTable list = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { -1 });
            List<DepartmentModel> list = SQLHelper<DepartmentModel>.FindAll().OrderBy(x => x.STT).ToList();
            cboDepartment.Properties.ValueMember = "ID";
            cboDepartment.Properties.DisplayMember = "Name";
            cboDepartment.Properties.DataSource = list;

            if (Global.DepartmentCode == "GD" || Global.DepartmentCode == "KT" || Global.DepartmentCode == "HR" || Global.IsAdmin || Global.LoginName == "TrangLT" || Global.LoginName == "NV0058") //Nếu là BGĐ hoặc phòng kế toán hoặc nhận sự hoặc Lê Thị Trang
            {
                cboDepartment.EditValue = 0;
                cboDepartment.Enabled = true;
            }
            else
            {
                cboDepartment.EditValue = Global.DepartmentID;
                cboDepartment.Enabled = false;
            }
        }

        void LoadEmployee()
        {
            DataTable list = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = list;
        }

        void LoadPaymentOrderType()
        {
            //DataTable list = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { -1 });
            List<PaymentOrderTypeModel> list = SQLHelper<PaymentOrderTypeModel>.FindByAttribute("IsDelete", 0).OrderBy(x => x.STT).ToList();
            cboPaymentOrderType.Properties.ValueMember = "ID";
            cboPaymentOrderType.Properties.DisplayMember = "TypeName";
            cboPaymentOrderType.Properties.DataSource = list;
        }



        private void cboDepartment_EditValueChanged(object sender, EventArgs e)
        {

            btnFind_Click(null, null);
        }

        private void cboEmployee_EditValueChanged(object sender, EventArgs e)
        {
            btnFind_Click(null, null);
        }

        private void cboIsComingExpired_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnFind_Click(null, null);
        }

        private void cboPaymentOrderType_EditValueChanged(object sender, EventArgs e)
        {
            btnFind_Click(null, null);
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) > TextUtils.ToInt(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            btnFind_Click(null, null);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {

            if (TextUtils.ToInt(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = TextUtils.ToInt(txtPageNumber.Text) > 1 ? (TextUtils.ToInt(txtPageNumber.Text) - 1).ToString() : "1";
            btnFind_Click(null, null);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) >= TextUtils.ToInt(txtTotalPage.Text)) return;
            txtPageNumber.Text = (TextUtils.ToInt(txtPageNumber.Text) + 1).ToString();
            btnFind_Click(null, null);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) >= TextUtils.ToInt(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            btnFind_Click(null, null);
        }

        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail();
            loadFileBackFlip();
            loadFileBackFlip2();
        }

        private void btnApproveTBP_Click(object sender, EventArgs e)
        {
            Approve(1, sender);
        }

        private void btnUnApproveTBP_Click(object sender, EventArgs e)
        {
            Approve(2, sender);
        }

        private void btnApproveHR_Click(object sender, EventArgs e)
        {
            Approve(1, sender);
        }

        private void btnUnApproveHR_Click(object sender, EventArgs e)
        {
            Approve(2, sender);
        }

        private void btnApproveDocument_Click(object sender, EventArgs e)
        {
            Approve(1, sender);
        }

        private void btnUnApproveDocument_Click(object sender, EventArgs e)
        {
            Approve(2, sender);
        }

        private void btnReceiveDocument_Click(object sender, EventArgs e)
        {
            Approve(1, sender);
        }

        private void btnUnReceiveDocument_Click(object sender, EventArgs e)
        {
            Approve(2, sender);
        }

        private void btnIsPayment_Click(object sender, EventArgs e)
        {
            Approve(1, sender);
        }

        private void btnUnPayment_Click(object sender, EventArgs e)
        {
            Approve(2, sender);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Approve(1, sender);
        }

        private void btnUnApproveKT_Click(object sender, EventArgs e)
        {
            Approve(2, sender);
        }

        private void btnApproveBGĐ_Click(object sender, EventArgs e)
        {
            Approve(1, sender);
        }

        private void btnUnApproveBGĐ_Click(object sender, EventArgs e)
        {
            Approve(2, sender);
        }

        private void btnTreeFolder_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            GetFolderPayment(id, true);

        }

        private void grvDataFile_DoubleClick(object sender, EventArgs e)
        {
            ////btnDownloadAttachFile_Click(null, null);
            //var tabSelected = xtraTabControl1.SelectedTabPage;
            //if (tabSelected.Controls.Count <= 0) return;
            //GridControl gridControl = (GridControl)tabSelected.Controls[0].Controls[0];
            //GridView gridView = gridControl.MainView as GridView;

            //if (gridView == null) return;

            //ToolStripMenuItem toolStrip = (ToolStripMenuItem)sender;
            //ContextMenuStrip contextMenu = (ContextMenuStrip)toolStrip.Owner;
            //var gridControlFile = (GridControl)contextMenu.SourceControl;
            //var gridViewFile = (GridView)gridControlFile.MainView;
            //if (gridViewFile == null) return;

            try
            {
                string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string pathDownload = Path.Combine(userFolder, "Downloads", "DeNghiThanhToan");

                if (!Directory.Exists(pathDownload))
                {
                    Directory.CreateDirectory(pathDownload);
                }

                DateTime dateOrder = TextUtils.ToDate5(grvData.GetFocusedRowCellValue(colDateOrder));
                string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
                string pathPattern = $@"NĂM {dateOrder.Year}/ĐỀ NGHỊ THANH TOÁN/THÁNG {dateOrder.ToString("MM.yyyy")}/{dateOrder.ToString("dd.MM.yyyy")}/{code}";

                string fileName = TextUtils.ToString(grvDataFile.GetFocusedRowCellValue(colFileName));
                string folderDownload = Path.Combine(pathDownload, fileName);
                string url = $"http://113.190.234.64:8083/api/paymentorder/{pathPattern}/{fileName}";

                if (File.Exists(folderDownload))
                {
                    folderDownload = Path.Combine(pathDownload, $"{Path.GetFileNameWithoutExtension(fileName)}_{code}_{DateTime.Now.ToString("HHmmss")}.{Path.GetExtension(fileName)}");
                }


                WebClient webClient = new WebClient();
                webClient.DownloadFile(url, folderDownload);
                if (Global.DepartmentID == 4) Process.Start(pathDownload);
                Process.Start(folderDownload);
            }
            catch
            {
                try
                {
                    string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    string pathDownload = Path.Combine(userFolder, "Downloads", "DeNghiThanhToan");

                    if (!Directory.Exists(pathDownload))
                    {
                        Directory.CreateDirectory(pathDownload);
                    }

                    DateTime dateOrder = TextUtils.ToDate5(grvData.GetFocusedRowCellValue(colCreatedDate.FieldName));
                    string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
                    string pathPattern = $@"NĂM {dateOrder.Year}/ĐỀ NGHỊ THANH TOÁN/THÁNG {dateOrder.ToString("MM.yyyy")}/{dateOrder.ToString("dd.MM.yyyy")}/{code}";

                    string fileName = TextUtils.ToString(grvDataFile.GetFocusedRowCellValue(colFileName));
                    string folderDownload = Path.Combine(pathDownload, fileName);
                    string url = $"http://113.190.234.64:8083/api/paymentorder/{pathPattern}/{fileName}";

                    if (File.Exists(folderDownload))
                    {
                        folderDownload = Path.Combine(pathDownload, $"{Path.GetFileNameWithoutExtension(fileName)}_{code}_{DateTime.Now.ToString("HHmmss")}.{Path.GetExtension(fileName)}");
                    }


                    WebClient webClient = new WebClient();
                    webClient.DownloadFile(url, folderDownload);
                    if (Global.DepartmentID == 4) Process.Start(pathDownload);
                    Process.Start(folderDownload);


                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
                }
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            int focusTab = xtraTabControl1.SelectedTabPageIndex;//NXluong update 06/10/25
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = Path.Combine(f.SelectedPath, $"TheoDoiChiPhiVP_{dtpDateStart.Value.ToString("ddMMyy")}_{dtpDateEnd.Value.ToString("ddMMyy")}.xlsx");

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                PrintingSystem printingSystem = new PrintingSystem();

                PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                //printableComponentLink1.Component = grdData;
                //NXLuong update 06/10/25
                if (focusTab == 0)
                {
                    printableComponentLink1.Component = grdData;
                }
                else
                {
                    printableComponentLink1.Component = grdPaymentSpe;
                }
                //NXLuong end  update 06/10/25
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

        static void MergeExcel(Excel.Worksheet workSheet, string startRow, string endRow)
        {
            try
            {
                Excel.Range range = workSheet.Range[startRow, endRow];
                range.Merge();
            }
            catch { }
        }

        private void btnTreeFolderContext_Click(object sender, EventArgs e)
        {
            btnTreeFolder_Click(null, null);
        }

        private void btnLogPaymentOrder_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id <= 0)
            {
                return;
            }

            frmPaymentOrderLog frm = new frmPaymentOrderLog();
            frm.cboPaymentOrder.EditValue = id;
            frm.Show();
        }

        private void btnTBP_Click(object sender, EventArgs e)
        {

        }

        private void tlDetail_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            string stt = TextUtils.ToString(e.Node.GetValue("Stt")).Trim();
            if (stt == "I" || stt == "II" || stt == "III")
            {
                e.Appearance.BackColor = Color.LightGray;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id <= 0) return;
            frmPaymentOrderViewDetail frm = new frmPaymentOrderViewDetail(id);
            frm.Show();
        }

        private void grvData_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column != colTotalMoneyPayment && e.Column != colTotalPayment) return;
            string currency = TextUtils.ToString(grvData.GetFocusedRowCellValue(colUnitPayment));
            decimal value = TextUtils.ToDecimal(e.Value);
            if (currency.ToLower() == "vnd")
            {
                e.DisplayText = value.ToString("n0");
            }
            else
            {
                e.DisplayText = value.ToString("n2");
            }

        }

        private void grvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(grvData.GetFocusedRowCellValue(grvData.FocusedColumn)).Trim();
                if (string.IsNullOrEmpty(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void btnApproveDocumentHR_Click(object sender, EventArgs e)
        {
            Approve(1, sender as ToolStripMenuItem);
        }

        private void btnUnApproveDocumentHR_Click(object sender, EventArgs e)
        {
            Approve(2, sender as ToolStripMenuItem);
        }

        private void btnHR_Click(object sender, EventArgs e)
        {

        }

        private void btnDownloadFile_Click(object sender, EventArgs e)
        {
            //DirectoryInfo info = new DirectoryInfo(@"\\192.168.1.2\ftp\UpdateVersion\RTC");
            //FileInfo[] file = info.GetFiles();
            //string newFile = file.OrderByDescending(x => Convert.ToInt32(Path.GetFileNameWithoutExtension(x.FullName))).FirstOrDefault().Name;
            //WebClient webClient = new WebClient();
            //webClient.DownloadFile("http://192.168.1.2:8083/api/Upload/Images/Test/657.zip", Path.Combine(@"C:\Users\Admin\Desktop", newFile));

            //FolderBrowserDialog f = new FolderBrowserDialog();
            //if (f.ShowDialog() == DialogResult.OK)
            //{
            //    DateTime dateOrder = TextUtils.ToDate5(grvData.GetFocusedRowCellValue(colDateOrder));
            //    string  code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            //    string fileName = TextUtils.ToString(grvDataFile.GetFocusedRowCellValue(colFileName));
            //    string pathPattern = $@"NĂM {dateOrder.Year}/ĐỀ NGHỊ THANH TOÁN/THÁNG {dateOrder.ToString("MM.yyyy")}/{dateOrder.ToString("dd.MM.yyyy")}/{code}";
            //    string url = $"http://113.190.234.64:8083/api/paymentorder/{pathPattern}/{fileName}";

            //    WebClient webClient = new WebClient();
            //    webClient.DownloadFile(url, Path.Combine(f.SelectedPath, fileName));
            //}
        }


        private void btnDownloadAttachFile_Click(object sender, EventArgs e)
        {
            var tabSelected = xtraTabControl1.SelectedTabPage;
            if (tabSelected.Controls.Count <= 0) return;

            //var a = tabSelected.Controls[0];
            GridControl gridControl = (GridControl)tabSelected.Controls[0].Controls[0].Controls[0];
            GridView gridView = gridControl.MainView as GridView;

            if (gridView == null) return;

            ToolStripMenuItem toolStrip = (ToolStripMenuItem)sender;
            ContextMenuStrip contextMenu = (ContextMenuStrip)toolStrip.Owner;
            var gridControlFile = (GridControl)contextMenu.SourceControl;
            var gridViewFile = (GridView)gridControlFile.MainView;
            if (gridViewFile == null) return;

            try
            {
                string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string pathDownload = Path.Combine(userFolder, "Downloads", "DeNghiThanhToan");

                if (!Directory.Exists(pathDownload))
                {
                    Directory.CreateDirectory(pathDownload);
                }

                DateTime dateOrder = TextUtils.ToDate5(gridView.GetFocusedRowCellValue(colDateOrder));
                string code = TextUtils.ToString(gridView.GetFocusedRowCellValue(colCode));
                string pathPattern = $@"NĂM {dateOrder.Year}/ĐỀ NGHỊ THANH TOÁN/THÁNG {dateOrder.ToString("MM.yyyy")}/{dateOrder.ToString("dd.MM.yyyy")}/{code}";

                string fileName = TextUtils.ToString(gridViewFile.GetFocusedRowCellValue(colFileName));
                string folderDownload = Path.Combine(pathDownload, fileName);
                string url = $"http://113.190.234.64:8083/api/paymentorder/{pathPattern}/{fileName}";

                if (File.Exists(folderDownload))
                {
                    folderDownload = Path.Combine(pathDownload, $"{Path.GetFileNameWithoutExtension(fileName)}_{code}_{DateTime.Now.ToString("HHmmss")}.{Path.GetExtension(fileName)}");
                }


                WebClient webClient = new WebClient();
                webClient.DownloadFile(url, folderDownload);
                if (Global.DepartmentID == 4) Process.Start(pathDownload);
                Process.Start(folderDownload);
            }
            catch
            {
                try
                {
                    string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    string pathDownload = Path.Combine(userFolder, "Downloads", "DeNghiThanhToan");

                    if (!Directory.Exists(pathDownload))
                    {
                        Directory.CreateDirectory(pathDownload);
                    }

                    DateTime dateOrder = TextUtils.ToDate5(gridView.GetFocusedRowCellValue(colCreatedDate.FieldName));
                    string code = TextUtils.ToString(gridView.GetFocusedRowCellValue(colCode));
                    string pathPattern = $@"NĂM {dateOrder.Year}/ĐỀ NGHỊ THANH TOÁN/THÁNG {dateOrder.ToString("MM.yyyy")}/{dateOrder.ToString("dd.MM.yyyy")}/{code}";

                    string fileName = TextUtils.ToString(gridViewFile.GetFocusedRowCellValue(colFileName));
                    string folderDownload = Path.Combine(pathDownload, fileName);
                    string url = $"http://113.190.234.64:8083/api/paymentorder/{pathPattern}/{fileName}";

                    if (File.Exists(folderDownload))
                    {
                        folderDownload = Path.Combine(pathDownload, $"{Path.GetFileNameWithoutExtension(fileName)}_{code}_{DateTime.Now.ToString("HHmmss")}.{Path.GetExtension(fileName)}");
                    }


                    WebClient webClient = new WebClient();
                    webClient.DownloadFile(url, folderDownload);
                    if (Global.DepartmentID == 4) Process.Start(pathDownload);
                    Process.Start(folderDownload);


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo");
                }
            }
        }

        private void btnViewAttachFile_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem toolStrip = (ToolStripMenuItem)sender;
                ContextMenuStrip contextMenu = (ContextMenuStrip)toolStrip.Owner;
                var gridControl = (GridControl)contextMenu.SourceControl;
                var gridView = (DevExpress.XtraGrid.Views.Grid.GridView)gridControl.MainView;
                if (gridView == null) return;

                string path = TextUtils.ToString(gridView.GetFocusedRowCellValue(colServerPath));
                string fileName = TextUtils.ToString(gridView.GetFocusedRowCellValue(colFileName));

                Process.Start(Path.Combine(path, fileName));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        void GetFolderPayment(int id, bool isShow)
        {
            try
            {
                //int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
                //if (id <= 0)
                //{
                //    return;
                //}
                PaymentOrderModel order = SQLHelper<PaymentOrderModel>.FindByID(id);
                if (order == null) return;

                ConfigSystemModel config = SQLHelper<ConfigSystemModel>.FindByAttribute("KeyName", "PathPaymentOrder").FirstOrDefault();
                string pathServer = string.IsNullOrEmpty(config.KeyValue.Trim()) ? "\\192.168.1.190" : config.KeyValue.Trim();

                if (Global.DefaultFileName == "defaultonline.ini")
                {
                    pathServer.Replace("\\192.168.1.190", "\\113.190.234.64");
                }

                DateTime dateOrder = order.DateOrder.Value;
                string code = order.Code;
                string pathPattern = $@"NĂM {dateOrder.Year}\ĐỀ NGHỊ THANH TOÁN\THÁNG {dateOrder.ToString("MM.yyyy")}\{dateOrder.ToString("dd.MM.yyyy")}\{code}";

                string path = Path.Combine(pathServer, pathPattern);

                if (isShow)
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    Process.Start(path);
                }
                else
                {
                    DirectoryInfo di = new DirectoryInfo(path);
                    di.Attributes = FileAttributes.Hidden;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grvData_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //if (e.RowHandle < 0) return;
            //if (e.Column != colCode) return;
            //if (e.Column == colCode)
            //{
            //    int isApproved = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle,colIsApproved));
            //    if (isApproved == 2)
            //    {
            //        e.Appearance.BackColor = Color.Red;
            //        e.Appearance.ForeColor = Color.White;
            //    }
            //}
        }

        private void grvData_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

        }

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;

            var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null) return;

            bool isUrgent = TextUtils.ToBoolean(view.GetRowCellValue(e.RowHandle, colIsUrgent));
            int isApproved = TextUtils.ToInt(view.GetRowCellValue(e.RowHandle, colIsApproved));
            int type = TextUtils.ToInt(view.GetRowCellValue(e.RowHandle, colPaymentOrderTypeID));

            if (isApproved != 1 && type == 2)
            {
                e.Appearance.BackColor = Color.LightYellow;
                e.Appearance.ForeColor = Color.Black;
            }
            else if (isApproved == 2)
            {
                e.Appearance.BackColor = Color.Red;
                e.Appearance.ForeColor = Color.White;
                //e.HighPriority = true;
            }
            else if (isUrgent && cboDepartment.Enabled)
            {
                e.Appearance.BackColor = Color.Orange;
                //e.Appearance.ForeColor = Color.White;
                //e.HighPriority = true;
            }
            else
            {
                if (view.FocusedRowHandle == e.RowHandle)
                {
                    e.Appearance.BackColor = Color.LightYellow;
                    //e.HighPriority = true;
                }
            }
        }

        private void btnUpdateDocument_Click(object sender, EventArgs e)
        {
            Approve(3, sender as ToolStripMenuItem);
        }

        private void cboIsApproved_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnFind_Click(null, null);
        }



        private void btnCopyPaymentOrder_Click(object sender, EventArgs e)
        {
            btnCopy_Click(null, null);
        }


        //==========================================LMK Update ===================================================================================
        void LoadData()
        {
            int typeOrder = cboTypeOrder.SelectedIndex;
            int paymentOrderTypeID = TextUtils.ToInt(cboPaymentOrderType.EditValue);
            DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59);
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            int employeeID = TextUtils.ToInt(cboEmployee.EditValue);
            string keyword = txtKeyword.Text.Trim();

            int pageNumber = TextUtils.ToInt(txtPageNumber.Text);
            int pageSize = TextUtils.ToInt(txtPageSize.Value);
            //List<PaymentOrderDTO> list = SQLHelper<PaymentOrderDTO>.ProcedureToList("spGetPaymentOrder",
            //                                        new string[] { "@PageNumber", "@PageSize", "@TypeOrder", "@PaymentOrderTypeID", "@DateStart", "@DateEnd", "@DepartmentID", "@EmployeeID", "@Keyword" },
            //                                        new object[] { pageNumber, pageSize, typeOrder, paymentOrderTypeID, dateStart, dateEnd, departmentID, employeeID, keyword });

            //int isIgnoreHR = Global.DepartmentCode == "HR" ? 0 : -1;
            int isIgnoreHR = -1;
            int isApproved = cboIsApproved.SelectedIndex - 1;

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát", "Đang load dữ liệu"))
            {
                dataSet = TextUtils.LoadDataSetFromSP("spGetPaymentOrder",
                            new string[] { "@PageNumber", "@PageSize", "@TypeOrder", "@PaymentOrderTypeID", "@DateStart", "@DateEnd", "@DepartmentID", "@EmployeeID", "@Keyword", "@IsIgnoreHR", "@IsApproved", "@IsSpecialOrder", "@ApprovedTBPID", "@Step", "@IsShowTable" },
                            new object[] { pageNumber, pageSize, typeOrder, paymentOrderTypeID, dateStart, dateEnd, departmentID, employeeID, keyword, isIgnoreHR, isApproved, 0, approvedTBPID, step, 0 });

                DataTable dt = dataSet.Tables[0];
                grdData.DataSource = dt;
                if (dt.Rows.Count > 0)
                {
                    txtTotalPage.Text = TextUtils.ToString(dt.Rows[0]["TotalPage"]);
                }

                var currencys = dt.AsEnumerable().Select(x => TextUtils.ToString(x.Field<string>(colUnitPayment.FieldName)).ToUpper())
                                                 .Distinct()
                                                 .ToList();

                foreach (GridColumn col in grvData.Columns)
                {
                    var summaryCustoms = col.Summary.Where(x => x.SummaryType == DevExpress.Data.SummaryItemType.Custom).ToList();
                    if (summaryCustoms.Count > 0)
                    {
                        col.Summary.Clear();
                    }
                }

                for (int i = 0; i < currencys.Count; i++)
                {
                    string item = currencys[i];
                    var sumOfValuesInMarch = dt.AsEnumerable()
                                            .Where(x => TextUtils.ToString(x.Field<string>(colUnitPayment.FieldName)).ToUpper() == item && (x.IsNull(colIsApproved.FieldName) || TextUtils.ToInt(x.Field<int?>(colIsApproved.FieldName)) != 2))
                                            .Sum(x => TextUtils.ToDecimal(x.Field<decimal?>(colTotalPaymentActual.FieldName)));

                    string value = sumOfValuesInMarch.ToString("n2");
                    if (item == "VND") value = sumOfValuesInMarch.ToString("n0");

                    int indexOffset = colTotalPaymentActual.VisibleIndex - (currencys.Count - 1);
                    indexOffset = indexOffset <= 7 ? 7 : indexOffset;

                    GridColumn col = grvData.Columns.FirstOrDefault(x => x.VisibleIndex == indexOffset + i);
                    if (col == null) continue;
                    grvData.Columns[col.FieldName].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, col.FieldName, $"{item} = {value}"));
                }

                if (Global.DepartmentID == 4) colDateOrder.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;

            }

        }

        void LoadDataOrderSpe()
        {
            int typeOrder = cboTypeOrder.SelectedIndex;
            int paymentOrderTypeID = TextUtils.ToInt(cboPaymentOrderType.EditValue);
            DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59);
            int departmentID = TextUtils.ToInt(cboDepartment.EditValue);
            int employeeID = TextUtils.ToInt(cboEmployee.EditValue);
            string keyword = txtKeyword.Text.Trim();

            int pageNumber = TextUtils.ToInt(txtPageNumber.Text);
            int pageSize = TextUtils.ToInt(txtPageSize.Value);
            //List<PaymentOrderDTO> list = SQLHelper<PaymentOrderDTO>.ProcedureToList("spGetPaymentOrder",
            //                                        new string[] { "@PageNumber", "@PageSize", "@TypeOrder", "@PaymentOrderTypeID", "@DateStart", "@DateEnd", "@DepartmentID", "@EmployeeID", "@Keyword" },
            //                                        new object[] { pageNumber, pageSize, typeOrder, paymentOrderTypeID, dateStart, dateEnd, departmentID, employeeID, keyword });

            int isIgnoreHR = Global.DepartmentCode == "HR" ? 0 : -1;
            int isApproved = cboIsApproved.SelectedIndex - 1;
            dataSetPaymentSpe = TextUtils.LoadDataSetFromSP("spGetPaymentOrder",
                                                   new string[] { "@PageNumber", "@PageSize", "@TypeOrder", "@PaymentOrderTypeID", "@DateStart", "@DateEnd", "@DepartmentID", "@EmployeeID", "@Keyword", "@IsIgnoreHR", "@IsApproved", "@IsSpecialOrder" },
                                                   new object[] { pageNumber, pageSize, typeOrder, paymentOrderTypeID, dateStart, dateEnd, departmentID, employeeID, keyword, isIgnoreHR, isApproved, 1 });

            DataTable dtPayMentSpe = dataSetPaymentSpe.Tables[0];
            grdPaymentSpe.DataSource = dtPayMentSpe;
            if (dtPayMentSpe.Rows.Count > 0)
            {
                txtTotalPage.Text = TextUtils.ToString(dtPayMentSpe.Rows[0]["TotalPage"]);
            }
        }
        void LoadDetail(int paymentOrderType = 0)
        {
            int paymentOrderID = paymentOrderType == 1 ? TextUtils.ToInt(grvPaymentSpe.GetFocusedRowCellValue(colID)) : TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));

            DataSet data = TextUtils.LoadDataSetFromSP("spGetPaymentOrderByID",
                                                    new string[] { "@ID" },
                                                    new object[] { paymentOrderID });

            DataTable dtDetails = data.Tables[1];
            DataTable dtFiles = data.Tables[3];

            if (paymentOrderType == 1)
            {
                grdSpeDetails.DataSource = dtDetails;
                grdSpeFile.DataSource = dtFiles;
                loadFileBackFlip2();
            }
            else
            {
                tlDetail.DataSource = dtDetails;
                grdDataFile.DataSource = dtFiles;
                loadFileBackFlip();
                tlDetail.ExpandAll();
            }
        }

        void Approve(int status, object sender /*ToolStripMenuItem button*/)
        {
            string[] buttonTBPActions = new string[] { "btnApproveTBP_New", "btnUnApproveTBP_New" }; //LinhTN update 17/08/2024

            int tabSelect = xtraTabControl1.SelectedTabPage == xtraTabPage1 ? 1 : 2;

            DevExpress.XtraGrid.Views.Grid.GridView gridView = tabSelect == 1 ? grvData : grvPaymentSpe;
            int[] selectedRowHandles = gridView.GetSelectedRows();

            if (selectedRowHandles.Count() <= 0)
            {
                //MessageBox.Show("Vui lòng chọn phiếu để thực hiện thao tác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //return;

                selectedRowHandles = new int[] { gridView.FocusedRowHandle };
            }

            string reasonRequestAppendFileHR = "";
            string reasonCancel = "";
            string reasonCancel_1 = "";
            bool isRequestAppendFileHR = false;

            foreach (var rowHandle in selectedRowHandles)
            {
                //int rowHandle = tabSelect == 1 ? grvData.FocusedRowHandle : grvPaymentSpe.FocusedRowHandle;

                int actionStep = 0;
                //string statusText = button.Text;
                string statusText = ""; //LinhTN update 17/08/2024

                int id = TextUtils.ToInt(gridView.GetRowCellValue(rowHandle, tabSelect == 1 ? colID : colPaymentSpeId));
                if (id <= 0)
                {
                    return;
                }

                int currentStep = TextUtils.ToInt(gridView.GetRowCellValue(rowHandle, tabSelect == 1 ? colStep : colSpeStep));
                int paymentOrderTypeID = TextUtils.ToInt(gridView.GetRowCellValue(rowHandle, tabSelect == 1 ? colPaymentOrderTypeID : colSpePaymentOrderTypeID));
                PaymentOrderTypeModel type = SQLHelper<PaymentOrderTypeModel>.FindByID(paymentOrderTypeID);
                PaymentOrderModel paymentOrder = SQLHelper<PaymentOrderModel>.FindByID(id);
                //Update action step


                //LinhTN update 17/08/2024 - start
                string dropdownButtonName = "";
                string dropdownButtonText = "";
                string buttonName = "";
                string buttonText = "";
                if (sender.GetType().Name == "ToolStripMenuItem")
                {
                    ToolStripMenuItem button = (ToolStripMenuItem)sender;
                    ToolStripItem dropdownButton = (ToolStripItem)button.OwnerItem;
                    dropdownButtonName = dropdownButton.Name;
                    dropdownButtonText = dropdownButton.Text;
                    buttonName = button.Name;
                    buttonText = button.Text;
                    statusText = button.Text;
                }
                else
                {
                    ToolStripButton toolStripButton = (ToolStripButton)sender;
                    if (buttonTBPActions.Contains(toolStripButton.Name))
                    {
                        dropdownButtonName = "btnTBP";
                        statusText = toolStripButton.Text;
                    }
                }
                //LinhTN update 17/08/2024 - end

                //ToolStripItem dropdownButton = (ToolStripItem)button.OwnerItem;
                if (paymentOrder.IsSpecialOrder == true)
                {
                    if (dropdownButtonName == "btnTBP") actionStep = 2;
                    else if (dropdownButtonName == "btnHR")
                    {

                    }
                    else if (dropdownButtonName == "btnKTT") actionStep = 3;
                    else if (dropdownButtonName == "btnBGĐ") actionStep = 4;
                    else if (dropdownButtonName == "btnKTTT")
                    {
                        if (buttonName == "btnReceiveDocument" || buttonName == "btnUnApproveDocument") return;
                        else if (buttonName == "btnIsPayment" || buttonName == "btnUnPayment") actionStep = 5;
                    }
                }
                else if (type.IsIgnoreHR)
                {
                    if (dropdownButtonName == "btnTBP") actionStep = 2;
                    else if (dropdownButtonName == "btnHR")
                    {

                    }
                    else if (dropdownButtonName == "btnKTTT")
                    {
                        if (buttonName == "btnApproveDocument" || buttonName == "btnUnApproveDocument" || buttonName == "btnUpdateDocument") actionStep = 3;
                        else if (buttonName == "btnReceiveDocument" || buttonName == "btnUnReceiveDocument") actionStep = 6;
                        else if (buttonName == "btnIsPayment" || buttonName == "btnUnPayment") actionStep = 7;
                    }
                    else if (dropdownButtonName == "btnKTT") actionStep = 4;
                    else if (dropdownButtonName == "btnBGĐ") actionStep = 5;
                }
                else
                {
                    DateTime date = new DateTime(2024, 03, 03).Date;

                    if (paymentOrder.DateOrder.Value.Date <= date)
                    {
                        if (dropdownButtonName == "btnTBP") actionStep = 2;
                        else if (dropdownButtonName == "btnHR") //actionStep = 3;
                        {
                            if (buttonName == "btnApproveDocumentHR" || buttonName == "btnUnApproveDocumentHR") actionStep = 0;
                            else if (buttonName == "btnApproveHR" || buttonName == "btnUnApproveHR") actionStep = 3;
                        }
                        else if (dropdownButtonName == "btnKTTT")
                        {
                            if (buttonName == "btnApproveDocument" || buttonName == "btnUnApproveDocument" || buttonName == "btnUpdateDocument") actionStep = 4;
                            else if (buttonName == "btnReceiveDocument" || buttonName == "btnUnReceiveDocument") actionStep = 7;
                            else if (buttonName == "btnIsPayment" || buttonName == "btnUnPayment") actionStep = 8;
                        }
                        else if (dropdownButtonName == "btnKTT") actionStep = 5;
                        else if (dropdownButtonName == "btnBGĐ") actionStep = 6;
                    }
                    else
                    {
                        if (dropdownButtonName == "btnTBP") actionStep = 2;
                        else if (dropdownButtonName == "btnHR")
                        {
                            //actionStep = 3;
                            //if (buttonName == "btnApproveDocumentHR" || buttonName == "btnUnApproveDocumentHR") actionStep = 3;
                            // lee min khooi update 03/10/2024
                            if (buttonName == "btnApproveDocumentHR" || buttonName == "btnUnApproveDocumentHR" || buttonName == "btnHRUpdateDocument") actionStep = 3;
                            else if (buttonName == "btnApproveHR" || buttonName == "btnUnApproveHR") actionStep = 4;
                        }
                        else if (dropdownButtonName == "btnKTTT")
                        {
                            if (buttonName == "btnApproveDocument" || buttonName == "btnUnApproveDocument" || buttonName == "btnUpdateDocument") actionStep = 5;
                            else if (buttonName == "btnReceiveDocument" || buttonName == "btnUnReceiveDocument") actionStep = 8;
                            else if (buttonName == "btnIsPayment" || buttonName == "btnUnPayment") actionStep = 9;
                        }
                        else if (dropdownButtonName == "btnKTT") actionStep = 6;
                        else if (dropdownButtonName == "btnBGĐ") actionStep = 7;
                    }
                }

                string code = TextUtils.ToString(gridView.GetRowCellValue(rowHandle, tabSelect == 1 ? colCode : colPaymentSpeCode));
                if (actionStep == 0)
                {
                    MessageBox.Show($"Đề nghị [{code}] không cần {dropdownButtonText.Trim().ToLower()} {buttonText.Trim().ToLower()}!");
                    return;
                }

                int isApproved = TextUtils.ToInt(gridView.GetRowCellValue(rowHandle, tabSelect == 1 ? colIsApproved : colSpeIsApproved));

                //if (!Global.IsAdmin)
                //{

                //}

                if (actionStep == currentStep)
                {
                    if (status == 1 && isApproved == 2 && dropdownButtonName != "btnBGĐ")
                    {
                        MessageBox.Show($"Bạn không thể {statusText}!", "Thông báo");
                        return;
                    }
                    else if (status == 1 && isApproved == 1)
                    {
                        MessageBox.Show($"Đề nghị [{paymentOrder.Code}] đã được duyệt!", "Thông báo");
                        return;
                    }

                }
                else if (actionStep < currentStep)
                {
                    if (status != 1)
                    {
                        MessageBox.Show("Vui lòng huỷ duyệt ở bước sau trước!", "Thông báo");
                        return;
                    }
                }
                else
                {

                    if (isApproved != 1)
                    {
                        MessageBox.Show($"Bạn không thể {statusText}!", "Thông báo");
                        return;
                    }
                    else if ((actionStep - currentStep) == 1) //Ở ngay bước sau
                    {
                        if (status == 1 && isApproved == 2 && dropdownButtonName != "btnBGĐ")
                        {
                            MessageBox.Show("Bạn không thể huỷ duyệt!", "Thông báo");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng duyệt ở bước trước!", "Thông báo");
                        return;
                    }
                }

                //Get quy trình duyệt của phiếu
                var exp1 = new Expression("PaymentOrderID", id);
                var exp2 = new Expression("Step", actionStep);
                PaymentOrderLogModel log = SQLHelper<PaymentOrderLogModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
                if (status == 2)
                {
                    if (String.IsNullOrWhiteSpace(reasonCancel))
                    {
                        frmPaymentOrderUnApprove frm = new frmPaymentOrderUnApprove();
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            if (string.IsNullOrEmpty(frm.txtReasonCancel.Text.Trim())) return;
                            reasonCancel = frm.txtReasonCancel.Text.Trim();
                        }
                    }
                    //log.StepName = $"{dropdownButton.Text} {button.Text.ToLower()}";
                    log.DateApproved = DateTime.Now;
                    log.IsApproved = status;
                    log.EmployeeApproveActualID = Global.EmployeeID;
                    log.ReasonCancel += $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}: " + reasonCancel + "\n";
                    log.ContentLog += $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}: {Global.AppFullName} {buttonText.ToLower()}\n";

                    SQLHelper<PaymentOrderLogModel>.Update(log);


                    GetFolderPayment(id, false);
                }
                else
                {
                    if (buttonName == "btnApproveDocument" || buttonName == "btnApproveKT" || buttonName == "btnUpdateDocument")
                    {
                        bool isUpdateDocument = buttonName == "btnUpdateDocument";
                        if (String.IsNullOrWhiteSpace(reasonCancel_1))
                        {
                            frmPaymentOrderUnApprove frm = new frmPaymentOrderUnApprove();
                            frm.label1.Text = isUpdateDocument ? "Lý do bổ sung" : "Kế toán hoạch toán";
                            frm.label2.Visible = isUpdateDocument;
                            frm.Text = isUpdateDocument ? "bổ sung chứng từ".ToUpper() : "Kế toán hoạch toán".ToUpper();
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                reasonCancel_1 = frm.txtReasonCancel.Text.Trim();
                            }
                        }

                        //PaymentOrderModel paymentOrder = SQLHelper<PaymentOrderModel>.FindByID(id);
                        paymentOrder = paymentOrder == null ? new PaymentOrderModel() : paymentOrder;

                        if (!isUpdateDocument)
                        {
                            paymentOrder.AccountingNote += reasonCancel_1 + "\n";
                            if (paymentOrder.ID > 0) SQLHelper<PaymentOrderModel>.Update(paymentOrder);
                        }

                        log.DateApproved = DateTime.Now;
                        log.IsApproved = status;
                        log.EmployeeApproveActualID = Global.EmployeeID;
                        log.ReasonCancel = "";
                        log.ContentLog += $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}: {Global.AppFullName} {buttonText.ToLower()}\n";

                        log.IsRequestAppendFileAC = (status == 3);
                        log.ReasonRequestAppendFileAC = reasonCancel_1;

                        //SQLHelper<PaymentOrderLogModel>.Update(log);
                    }
                    else
                    {
                        //string code = TextUtils.ToString(grvData.GetRowCellValue(grvData.FocusedRowHandle, colCode));
                        DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn {statusText.ToLower()} đề nghị [{code}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialog == DialogResult.Yes)
                        {

                            if (buttonName == "btnHRUpdateDocument" && !isRequestAppendFileHR) //Mở form nhập lý do
                            {
                                frmPaymentOrderUnApprove frm = new frmPaymentOrderUnApprove();
                                frm.Text = "YÊU CẦU BỔ SUNG CHỨNG TỪ";
                                frm.label1.Text = "Lý do bổ sung";
                                frm.label2.Visible = false;
                                if (frm.ShowDialog() == DialogResult.OK)
                                {
                                    reasonRequestAppendFileHR = frm.txtReasonCancel.Text.Trim();
                                    isRequestAppendFileHR = true;
                                }
                            }
                            log.ReasonRequestAppendFileHR = reasonRequestAppendFileHR;
                            log.IsRequestAppendFileHR = isRequestAppendFileHR;
                            log.DateApproved = DateTime.Now;
                            log.IsApproved = status;
                            log.EmployeeApproveActualID = Global.EmployeeID;
                            log.ReasonCancel = "";
                            log.ContentLog += $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}: {Global.AppFullName} {buttonText.ToLower()}\n";

                            if (buttonName == "btnUpdateDocument") log.IsRequestAppendFileAC = true;

                        }
                    }
                    SQLHelper<PaymentOrderLogModel>.Update(log);
                }
                //Add Notify
                SendNotifyApproved(status, actionStep, type.IsIgnoreHR, paymentOrder);
            }

            if (tabSelect == 1) LoadData();
            else LoadDataOrderSpe();
        }




        private void btnFind_Click(object sender, EventArgs e)
        {
            if (Lib.LockEvents) return;
            if (xtraTabControl1.SelectedTabPage == xtraTabPage1)
            {
                LoadData();
                LoadDetail();
            }
            else
            {
                LoadDataOrderSpe();
                LoadDetail(1);
            }
        }
        private void btnCopy_Click(object sender, EventArgs e)
        {
            bool isSpeTab = xtraTabControl1.SelectedTabPage == xtraTabPage2 ? true : false;
            DevExpress.XtraGrid.Views.Grid.GridView grv = isSpeTab ? grvPaymentSpe : grvData;
            var focusedRowHandle = grv.FocusedRowHandle;
            int ID = TextUtils.ToInt(grv.GetFocusedRowCellValue(isSpeTab ? colPaymentSpeId : colID));
            //PaymentOrderModel model = (PaymentOrderModel)PaymentOrderBO.Instance.FindByPK(ID);
            PaymentOrderModel model = SQLHelper<PaymentOrderModel>.FindByID(ID);

            var frm = isSpeTab ? (Form)new frmPaymentOrderSpeDetails() : new frmPaymentOrderDetail();

            if (isSpeTab)
            {
                ((frmPaymentOrderSpeDetails)frm).paymentOrder = model;
                ((frmPaymentOrderSpeDetails)frm).isCopy = true;
            }
            else
            {
                ((frmPaymentOrderDetail)frm).paymentOrder = model;
                ((frmPaymentOrderDetail)frm).isCopy = true;
            }
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (isSpeTab) LoadDataOrderSpe();
                else LoadData();
                grv.FocusedRowHandle = focusedRowHandle;
                LoadDetail();
            }

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool isTab1 = xtraTabControl1.SelectedTabPage == xtraTabPage1 ? true : false;
            Form frm = isTab1 ? (Form)new frmPaymentOrderDetail() : new frmPaymentOrderSpeDetails();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (isTab1) LoadData();
                else LoadDataOrderSpe();
                grvData_FocusedRowChanged(null, null);
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            bool isSpeTab = xtraTabControl1.SelectedTabPage == xtraTabPage2 ? true : false;
            DevExpress.XtraGrid.Views.Grid.GridView grv = isSpeTab ? grvPaymentSpe : grvData;
            var focusedRowHandle = grv.FocusedRowHandle;
            int ID = TextUtils.ToInt(grv.GetFocusedRowCellValue(isSpeTab ? colPaymentSpeId : colID));
            if (ID <= 0) return;
            //PaymentOrderModel model = (PaymentOrderModel)PaymentOrderBO.Instance.FindByPK(ID);
            PaymentOrderModel model = SQLHelper<PaymentOrderModel>.FindByID(ID);

            var frm = isSpeTab ? (Form)new frmPaymentOrderSpeDetails() : new frmPaymentOrderDetail();

            if (isSpeTab) ((frmPaymentOrderSpeDetails)frm).paymentOrder = model;
            else ((frmPaymentOrderDetail)frm).paymentOrder = model;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (isSpeTab) LoadDataOrderSpe();
                else LoadData();
                grv.FocusedRowHandle = focusedRowHandle;
                LoadDetail();
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //bool isSpeTab = xtraTabControl1.SelectedTabPage == xtraTabPage2 ? true : false;
            bool isSpeTab = xtraTabControl1.SelectedTabPage == xtraTabPage2;
            DevExpress.XtraGrid.Views.Grid.GridView grv = isSpeTab ? grvPaymentSpe : grvData;
            if (grv.RowCount > 0)
            {
                var focusedRowHandle = grv.FocusedRowHandle;
                int ID = TextUtils.ToInt(grv.GetFocusedRowCellValue(isSpeTab ? colPaymentSpeId : colID));
                int step = TextUtils.ToInt(grv.GetFocusedRowCellValue(isSpeTab ? colSpeStep : colStep));
                string code = TextUtils.ToString(grv.GetFocusedRowCellValue(isSpeTab ? colPaymentSpeCode : colCode));
                if (ID == 0) return;
                DataTable dttt = new DataTable();
                dttt = TextUtils.LoadDataFromSP("spGetPaymentOrderDetail", "A", new string[] { "@PaymentOrderID" }, new object[] { ID });

                bool isAdmin = (Global.IsAdmin && Global.EmployeeID <= 0);
                PaymentOrderModel paymentOrder = SQLHelper<PaymentOrderModel>.FindByID(ID);
                if (paymentOrder.EmployeeID != Global.EmployeeID && !isAdmin)
                {
                    MessageBox.Show("Bạn không thể xoá đề nghị của người khác!");

                }
                else if (step != 1 && !isAdmin)
                {
                    string stepName = TextUtils.ToString(grv.GetFocusedRowCellValue(isSpeTab ? colSpeStepName : colStepName));
                    MessageBox.Show($"Đề nghị [{paymentOrder.Code}] đã được {stepName}.\nBạn không thể xoá!");

                }
                else
                {
                    if (MessageBox.Show(string.Format("Bạn có muốn xóa đề nghị thanh toán [{0}] hay không ?", code), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //PaymentOrderDetailBO.Instance.Delete(ID);

                        //var myDict = new Dictionary<string, object>()
                        //{
                        //    {"IsDelete",true },
                        //    {"UpdatedBy",Global.AppUserName },
                        //    {"UpdatedDate",DateTime.Now },
                        //};


                        //SQLHelper<PaymentOrderModel>.UpdateFieldsByID(myDict, ID);

                        paymentOrder.IsDelete = true;
                        paymentOrder.UpdatedDate = DateTime.Now;
                        paymentOrder.UpdatedBy = Global.AppUserName;
                        SQLHelper<PaymentOrderModel>.Update(paymentOrder);


                        //if (dttt.Rows.Count > 0)
                        //{
                        //    //PaymentOrderDetailBO.Instance.DeleteByAttribute("PaymentOrderID", ID);
                        //    tlDetail.DeleteSelectedNodes();
                        //}
                        //PaymentOrderBO.Instance.Delete(ID);
                        grv.DeleteSelectedRows();
                        grv.FocusedRowHandle = focusedRowHandle;
                        grvData_FocusedRowChanged(null, null);
                    }
                }
            }
        }
        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            //if (e.Page == xtraTabPage1)
            //{
            //    LoadData();
            //}
            //else if (e.Page == xtraTabPage2)
            //{
            //    LoadDataOrderSpe();
            //}

            //====================  Lee min khooi update 28/08/2024  =========================================
            if (e.Page == xtraTabPage1)
            {
                btnApproveDocument.Visible = btnUpdateDocument.Visible = btnUnApproveDocument.Visible = true;
                LoadData();
            }
            else if (e.Page == xtraTabPage2)
            {
                btnApproveDocument.Visible = btnUpdateDocument.Visible = btnUnApproveDocument.Visible = false;
                LoadDataOrderSpe();
            }
            //====================  Lee min khooi END update 28/08/2024  =========================================
        }

        private void grvPaymentSpe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                string value = TextUtils.ToString(grvPaymentSpe.GetFocusedRowCellValue(grvPaymentSpe.FocusedColumn)).Trim();
                if (string.IsNullOrEmpty(value)) return;
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }

        private void grvPaymentSpe_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadDetail(1);
        }

        private void grvPaymentSpe_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column != colSpeTotalMoney) return;
            string currency = TextUtils.ToString(grvPaymentSpe.GetFocusedRowCellValue(colSpeUnit));
            decimal value = TextUtils.ToDecimal(e.Value);
            if (currency.ToLower() == "vnd")
            {
                e.DisplayText = value.ToString("n0");
            }
            else
            {
                e.DisplayText = value.ToString("n2");
            }
        }

        private void grvSpeFile_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                //string pathDownload = Path.Combine(KnownFolders.Downloads.Path, "DeNghiThanhToan");
                //string pathDownload = Path.Combine(Application.StartupPath, "DeNghiThanhToan");

                string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string pathDownload = Path.Combine(userFolder, "Downloads", "DeNghiThanhToan");

                if (!Directory.Exists(pathDownload))
                {
                    Directory.CreateDirectory(pathDownload);
                }

                DateTime dateOrder = TextUtils.ToDate5(grvPaymentSpe.GetFocusedRowCellValue(colDateOrder));
                string code = TextUtils.ToString(grvPaymentSpe.GetFocusedRowCellValue(colCode));
                string pathPattern = $@"NĂM {dateOrder.Year}/ĐỀ NGHỊ THANH TOÁN/THÁNG {dateOrder.ToString("MM.yyyy")}/{dateOrder.ToString("dd.MM.yyyy")}/{code}";

                string fileName = TextUtils.ToString(grvSpeFile.GetFocusedRowCellValue(colFileName));
                string folderDownload = Path.Combine(pathDownload, fileName);
                string url = $"http://113.190.234.64:8083/api/paymentorder/{pathPattern}/{fileName}";

                WebClient webClient = new WebClient();
                webClient.DownloadFile(url, folderDownload);
                Process.Start(folderDownload);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void grvPaymentSpe_DoubleClick(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvPaymentSpe.GetFocusedRowCellValue("ID"));
            if (id <= 0) return;
            frmPaymentOrderViewDetail frm = new frmPaymentOrderViewDetail(id);
            frm.Show();
        }

        private void btnApproveTBP_New_Click(object sender, EventArgs e)
        {
            Approve(1, sender);
        }

        private void btnUnApproveTBP_New_Click(object sender, EventArgs e)
        {
            Approve(2, sender);
        }


        //=====================================================lee min khooi update 26/09/2024=====================================================
        private void SendNotifyApproved(int status, int step, bool isHrIgnore, PaymentOrderModel payment)
        {
            List<string> stepNormal = new List<string>()
            {
                "Đề nghị thanh toán" ,  "Trưởng bộ phận xác nhận",  "Nhân sự xác nhận",  "TBP Nhân sự xác nhận",  "Check hồ sơ",  "Kế toán trưởng xác nhận",  "Ban giám đốc xác nhận",  "Nhận chứng từ",  "Kế toán thanh toán"
            };
            List<Step> lstCode = new List<Step>()
            {
                new Step(){step = 3, code = "N59"},//HCNS
                new Step(){step = 4, code = "N56"},//TBP HR
                new Step(){step = 5, code = "N55"},//Kế toán
                new Step(){step = 6, code = "N61"},//KTT
                new Step(){step = 7, code = "N58"},//BGĐ
                new Step(){step = 8, code = "N55"},//KT
                new Step(){step = 9, code = "N55"}//KT
            };
            string currentStep = status == 1 ? (isHrIgnore == true ? stepNormal[step + 1] : stepNormal[step - 1]) : stepNormal[0];
            string statusText = status == 1 ? "Được duyệt" : "Bị hủy";
            string textNotify = $"Đề nghị thanh toán đã {statusText} tại bước {currentStep} \n" + $"Số đề nghị: {payment.Code}\n" + $"Deadline: {payment.DeadlinePayment}";

            step = (isHrIgnore && step >= 3) ? step + 2 : step;
            if (status == 2)
            {
                Expression ex1 = new Expression("PaymentOrderID", payment.ID);
                Expression ex2 = new Expression("Step", 1);
                PaymentOrderLogModel log = SQLHelper<PaymentOrderLogModel>.FindByExpression(ex1.And(ex2)).FirstOrDefault();
                if (log == null) return;
                TextUtils.AddNotify("ĐỀ NGHỊ THANH TOÁN", textNotify, TextUtils.ToInt(log.EmployeeID));
            }
            else if (status == 1)
            {

                Step stepRule = lstCode.FirstOrDefault(p => p.step == step) ?? new Step();
                List<EmployeeModel> lstEmp = SQLHelper<EmployeeModel>.ProcedureToList("spGetAllEmployeeByUserGroupCode", new string[] { "@Code" }, new object[] { stepRule.code });
                foreach (EmployeeModel item in lstEmp)
                {
                    TextUtils.AddNotify("ĐỀ NGHỊ THANH TOÁN", textNotify, TextUtils.ToInt(item.ID));
                }

                if (payment.PaymentOrderTypeID == 26) //Add riêng cho KTT
                {
                    TextUtils.AddNotify("ĐỀ NGHỊ THANH TOÁN Chi phí thuế / Hải quan/ lệ phí".ToUpper(), textNotify, 330);
                }
            }
        }
        private class Step
        {
            public int step { get; set; }
            public string code { get; set; } = "";
        }

        // lee min khooi update 03/10/2024
        private void btnHRUpdateDocument_Click(object sender, EventArgs e)
        {
            Approve(3, sender as ToolStripMenuItem);
        }

        private void btnExportExcels_Click(object sender, EventArgs e)
        {
            if (grvData.RowCount > 0)
            {
                if (grvData.SelectedRowsCount == 0)
                {
                    MessageBox.Show("Vui lòng chọn phiếu cần in!", "Thông báo", MessageBoxButtons.OK);
                    return;
                }

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

                for (int i = 0; i < grvData.RowCount; i++)
                {
                    if (grvData.IsRowSelected(i))
                    {
                        string fileSourceName = "PaymentOrderReport.xlsx";
                        string sourcePath = Application.StartupPath + "\\" + fileSourceName;
                        string paymentCode = TextUtils.ToString(grvData.GetRowCellValue(i, colCode));
                        string currentPath = path + "\\" + paymentCode + DateTime.Now.ToString("_dd_MM_yyyy_HH_mm_ss") + ".xlsx";
                        try
                        {
                            File.Copy(sourcePath, currentPath, true);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }
                        int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                        ExportExcels(id, currentPath);
                    }
                }
            }
            else return;
        }

        void ExportExcels(int id, string currentPath)
        {
            if (id == 0) return;
            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetPaymentOrderByID", new string[] { "@ID" }, new object[] { id });
            DataTable dtOrder = dataSet.Tables[0];
            DataTable dtDetail = dataSet.Tables[1];
            DataTable dtSign = dataSet.Tables[2];
            if (dtOrder.Rows.Count <= 0) return;

            #region Khai báo các biến
            DateTime dt = TextUtils.ToDate5(grvData.GetFocusedRowCellValue(colDateOrder));
            string date = $"Ngày {dt.Day} tháng {dt.Month} năm {dt.Year}";
            int typeOrder = TextUtils.ToInt(dtOrder.Rows[0]["TypeOrder"]);
            int typePaymentOrder = TextUtils.ToInt(dtOrder.Rows[0]["TypePayment"]);
            string nameNCC = TextUtils.ToString(dtOrder.Rows[0]["NameNCC"]);
            string POCode = TextUtils.ToString(dtOrder.Rows[0]["POCode"]);

            string numberDocument = typeOrder == 1 ? "BM01-RTC.AC-QT03" : "BM02-RTC.AC-QT03";
            string totalMoneyText = TextUtils.ToString(dtOrder.Rows[0]["TotalMoneyText"]);
            totalMoneyText = totalMoneyText[0].ToString().ToUpper() + totalMoneyText.Substring(1);
            #endregion

            using (WaitDialogForm fWait = new WaitDialogForm("Vui lòng chờ trong giây lát...", "Đang tạo phiếu..."))
            {
                Microsoft.Office.Interop.Excel.Application app = default(Microsoft.Office.Interop.Excel.Application);
                Microsoft.Office.Interop.Excel.Workbook workBoook = default(Microsoft.Office.Interop.Excel.Workbook);
                Microsoft.Office.Interop.Excel.Worksheet workSheet = default(Microsoft.Office.Interop.Excel.Worksheet);

                try
                {
                    app = new Microsoft.Office.Interop.Excel.Application();
                    app.Workbooks.Open(currentPath);

                    workBoook = app.Workbooks[1];
                    workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBoook.Worksheets[typeOrder];

                    workSheet.Cells[4, 1] = $"GIẤY {TextUtils.ToString(dtOrder.Rows[0]["TypeOrderText"]).ToUpper()}";
                    workSheet.Cells[5, 1] = $"Số {TextUtils.ToString(dtOrder.Rows[0]["Code"])}";
                    workSheet.Cells[6, 1] = $"Ngày {TextUtils.ToDate5(dtOrder.Rows[0]["DateOrder"]).Day} tháng {TextUtils.ToDate5(dtOrder.Rows[0]["DateOrder"]).Month} năm {TextUtils.ToDate5(dtOrder.Rows[0]["DateOrder"]).Year}";//chung
                    workSheet.Cells[8, 3] = $": {TextUtils.ToString(dtOrder.Rows[0]["FullName"])}";
                    workSheet.Cells[8, 8] = $": {TextUtils.ToString(dtOrder.Rows[0]["DepartmentName"])}";
                    workSheet.Cells[9, 3] = $": {TextUtils.ToString(dtOrder.Rows[0]["ReasonOrder"])}";
                    workSheet.Cells[11, 3] = $": {TextUtils.ToString(dtOrder.Rows[0]["CustomerName"])}";
                    workSheet.Cells[13, 3] = $": {TextUtils.ToString(dtOrder.Rows[0]["AccountNumber"])}";
                    workSheet.Cells[13, 8] = $": {TextUtils.ToString(dtOrder.Rows[0]["Bank"])}";

                    workSheet.Cells[14, 3] = $": {TextUtils.ToString(dtOrder.Rows[0]["TypeBankTransferText"])}";
                    workSheet.Cells[15, 3] = $": {TextUtils.ToString(dtOrder.Rows[0]["ContentBankTransfer"])}";
                    workSheet.Cells[16, 8] = $": {TextUtils.ToString(dtOrder.Rows[0]["Unit"]).ToUpper()}";
                    workSheet.Cells[11, 3] = $": {TextUtils.ToString(dtOrder.Rows[0]["ReceiverInfo"]).ToUpper()}";

                    // Xuất excel cho phiếu yêu cầu thành toán
                    if (typeOrder == 2 && dtDetail.Rows.Count > 0)
                    {
                        string nameNcc = TextUtils.ToString(dtOrder.Rows[0]["NameNCC"]);
                        if (nameNcc == "")
                        {
                            ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[10]).Hidden = true;
                            workSheet.Cells[11, 1] = "3.";
                            workSheet.Cells[16, 1] = "4.";
                        }

                        workSheet.Cells[10, 3] = $": {TextUtils.ToString(dtOrder.Rows[0]["NameNCC"])}";
                        workSheet.Cells[1, 8] = numberDocument;
                        workSheet.Cells[2, 8] = "Lần ban hành: 01";
                        workSheet.Cells[3, 8] = "Trang: 1";

                        workSheet.Cells[20, 1] = $"Số tiền bằng chữ: {TextUtils.ToString(dtOrder.Rows[0]["TotalMoneyText"])}";
                        workSheet.Cells[20, 1].Font.Italic = true;

                        workSheet.Cells[22, 1] = $"{TextUtils.ToString(dtOrder.Rows[0]["AccountingNote"])}";

                        var signEmployee = dtSign.Select("Step = 1 and IsApproved = 1");
                        var signTBP = dtSign.Select("Step = 2 and IsApproved = 1");
                        var signKT = dtSign.Select("Step = 4 and IsApproved = 1");
                        var signBGĐ = dtSign.Select("Step = 5 and IsApproved = 1");

                        workSheet.Cells[25, 1] = typeOrder == 1 ? "Người đề nghị tạm ứng" : "Người đề nghị thanh toán";
                        workSheet.Cells[25, 3] = "Trưởng BP";
                        workSheet.Cells[25, 6] = "Phòng kế toán";
                        workSheet.Cells[25, 8] = "Ban giám đốc";

                        workSheet.Cells[27, 1] = signEmployee.Length <= 0 ? "" : $"{TextUtils.ToString(signEmployee[0]["FullName"])}\n{TextUtils.ToDate5(signEmployee[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm")}";
                        workSheet.Cells[27, 3] = signTBP.Length <= 0 ? "" : $"{TextUtils.ToString(signTBP[0]["FullName"])}\n{TextUtils.ToDate5(signTBP[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm")}";
                        workSheet.Cells[27, 6] = signKT.Length <= 0 ? "" : $"{TextUtils.ToString(signKT[0]["FullName"])}\n{TextUtils.ToDate5(signKT[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm")}";
                        workSheet.Cells[27, 8] = signBGĐ.Length <= 0 ? "" : $"{TextUtils.ToString(signBGĐ[0]["FullName"])}\n{TextUtils.ToDate5(signBGĐ[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm")}";
                        for (int i = dtDetail.Rows.Count - 1; i >= 0; i--)
                        {
                            workSheet.Cells[19, 1] = TextUtils.ToString(dtDetail.Rows[i]["Stt"]);
                            workSheet.Cells[19, 2] = TextUtils.ToString(dtDetail.Rows[i]["ContentPayment"]);
                            workSheet.Cells[19, 3] = TextUtils.ToString(dtDetail.Rows[i]["Unit"]);
                            workSheet.Cells[19, 4] = TextUtils.ToInt(dtDetail.Rows[i]["Quantity"]) != 0 ? TextUtils.ToInt(dtDetail.Rows[i]["Quantity"]).ToString("N0") : "";
                            workSheet.Cells[19, 5] = TextUtils.ToInt(dtDetail.Rows[i]["UnitPrice"]) != 0 ? TextUtils.ToInt(dtDetail.Rows[i]["UnitPrice"]).ToString("N0") : "";
                            workSheet.Cells[19, 6] = TextUtils.ToInt(dtDetail.Rows[i]["TotalMoney"]) != 0 ? TextUtils.ToInt(dtDetail.Rows[i]["TotalMoney"]).ToString("N0") : "";
                            workSheet.Cells[19, 7] = "";
                            workSheet.Cells[19, 8] = "";
                            workSheet.Cells[19, 9] = TextUtils.ToString(dtDetail.Rows[i]["Note"]);

                            if (TextUtils.ToString(dtDetail.Rows[i]["Stt"]) == "I" ||
                               TextUtils.ToString(dtDetail.Rows[i]["Stt"]) == "II" ||
                               TextUtils.ToString(dtDetail.Rows[i]["Stt"]) == "III")
                            {
                                #region SetLayout
                                workSheet.Cells[19, 1].Font.Bold = true;
                                workSheet.Cells[19, 2].Font.Bold = true;
                                workSheet.Cells[19, 3].Font.Bold = true;
                                workSheet.Cells[19, 4].Font.Bold = true;
                                workSheet.Cells[19, 5].Font.Bold = true;
                                workSheet.Cells[19, 6].Font.Bold = true;
                                workSheet.Cells[19, 7].Font.Bold = true;
                                workSheet.Cells[19, 8].Font.Bold = true;
                                workSheet.Cells[19, 9].Font.Bold = true;
                                workSheet.Cells[19, 1].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                                workSheet.Cells[19, 1].Borders.Weight = Excel.XlBorderWeight.xlThin;
                                workSheet.Cells[19, 2].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                                workSheet.Cells[19, 2].Borders.Weight = Excel.XlBorderWeight.xlThin;
                                workSheet.Cells[19, 3].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                                workSheet.Cells[19, 3].Borders.Weight = Excel.XlBorderWeight.xlThin;
                                workSheet.Cells[19, 4].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                                workSheet.Cells[19, 4].Borders.Weight = Excel.XlBorderWeight.xlThin;
                                workSheet.Cells[19, 5].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                                workSheet.Cells[19, 5].Borders.Weight = Excel.XlBorderWeight.xlThin;
                                workSheet.Cells[19, 6].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                                workSheet.Cells[19, 6].Borders.Weight = Excel.XlBorderWeight.xlThin;
                                workSheet.Cells[19, 7].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                                workSheet.Cells[19, 7].Borders.Weight = Excel.XlBorderWeight.xlThin;
                                workSheet.Cells[19, 8].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                                workSheet.Cells[19, 8].Borders.Weight = Excel.XlBorderWeight.xlThin;
                                workSheet.Cells[19, 9].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                                workSheet.Cells[19, 9].Borders.Weight = Excel.XlBorderWeight.xlThin;
                                #endregion
                            }
                            ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[19]).Insert();
                        }
                        ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[18]).Delete();
                        ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[18]).Delete();
                    }
                    else if (typeOrder == 1 && dtDetail.Rows.Count > 0)
                    {
                        workSheet.Cells[1, 9] = numberDocument;
                        workSheet.Cells[2, 9] = "Lần ban hành: 01";
                        workSheet.Cells[3, 9] = "Trang: 1";
                        workSheet.Cells[10, 3] = $": Ngày {TextUtils.ToDate5(dtOrder.Rows[0]["DatePayment"]).Day} tháng {TextUtils.ToDate5(dtOrder.Rows[0]["DatePayment"]).Month} năm {TextUtils.ToDate5(dtOrder.Rows[0]["DatePayment"]).Year}";


                        workSheet.Cells[21, 1] = $"Số tiền bằng chữ: {TextUtils.ToString(dtOrder.Rows[0]["TotalMoneyText"])}";
                        workSheet.Cells[21, 1].Font.Italic = true;
                        workSheet.Cells[23, 1] = $"{TextUtils.ToString(dtOrder.Rows[0]["AccountingNote"])}";

                        var signEmployee = dtSign.Select("Step = 1 and IsApproved = 1");
                        var signTBP = dtSign.Select("Step = 2 and IsApproved = 1");
                        var signHrm = dtSign.Select("Step = 4 and IsApproved = 1");
                        var signKT = dtSign.Select("Step = 6 and IsApproved = 1");
                        var signBGĐ = dtSign.Select("Step = 7 and IsApproved = 1");

                        workSheet.Cells[27, 1] = typeOrder == 1 ? "Người đề nghị tạm ứng" : "Người đề nghị thanh toán";
                        workSheet.Cells[27, 3] = "Trưởng BP";
                        workSheet.Cells[27, 5] = "Phòng nhân sự";
                        workSheet.Cells[27, 7] = "Phòng kế toán";
                        workSheet.Cells[27, 9] = "Ban giám đốc";

                        workSheet.Cells[30, 1] = signEmployee.Length <= 0 ? "" : $"{TextUtils.ToString(signEmployee[0]["FullName"])}\n{TextUtils.ToDate5(signEmployee[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm")}";
                        workSheet.Cells[30, 3] = signTBP.Length <= 0 ? "" : $"{TextUtils.ToString(signTBP[0]["FullName"])}\n{TextUtils.ToDate5(signTBP[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm")}";
                        workSheet.Cells[30, 5] = signHrm.Length <= 0 ? "" : $"{TextUtils.ToString(signHrm[0]["FullName"])}\n{TextUtils.ToDate5(signHrm[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm")}";
                        workSheet.Cells[30, 7] = signKT.Length <= 0 ? "" : $"{TextUtils.ToString(signKT[0]["FullName"])}\n{TextUtils.ToDate5(signKT[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm")}";
                        workSheet.Cells[30, 9] = signBGĐ.Length <= 0 ? "" : $"{TextUtils.ToString(signBGĐ[0]["FullName"])}\n{TextUtils.ToDate5(signBGĐ[0]["DateApproved"]).ToString("dd/MM/yyyy HH:mm")}";
                        for (int i = dtDetail.Rows.Count - 1; i >= 0; i--)
                        {
                            workSheet.Cells[19, 1] = TextUtils.ToString(dtDetail.Rows[i]["Stt"]);
                            workSheet.Cells[19, 2] = TextUtils.ToString(dtDetail.Rows[i]["ContentPayment"]);
                            workSheet.Cells[19, 3] = TextUtils.ToString(dtDetail.Rows[i]["Unit"]);
                            workSheet.Cells[19, 4] = TextUtils.ToDecimal(dtDetail.Rows[i]["Quantity"]) != 0 ? TextUtils.ToDecimal(dtDetail.Rows[i]["Quantity"]).ToString("N0") : "";
                            workSheet.Cells[19, 5] = TextUtils.ToDecimal(dtDetail.Rows[i]["UnitPrice"]) != 0 ? TextUtils.ToDecimal(dtDetail.Rows[i]["UnitPrice"]).ToString("N0") : "";
                            workSheet.Cells[19, 6] = TextUtils.ToDecimal(dtDetail.Rows[i]["TotalMoney"]) != 0 ? TextUtils.ToDecimal(dtDetail.Rows[i]["TotalMoney"]).ToString("N0") : "";
                            workSheet.Cells[19, 7] = "";
                            workSheet.Cells[19, 8] = "";
                            workSheet.Cells[19, 9] = TextUtils.ToString(dtDetail.Rows[i]["Note"]);
                            ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[19]).Insert();
                        }
                        ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[18]).Delete();
                        ((Microsoft.Office.Interop.Excel.Range)workSheet.Rows[18]).Delete();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (app != null)
                    {
                        // xóa các sheet không liên quan
                        app.DisplayAlerts = false;
                        for (int i = app.ActiveWorkbook.Sheets.Count; i >= 1; i--)
                            if (i != typeOrder)
                                ((Microsoft.Office.Interop.Excel.Worksheet)app.ActiveWorkbook.Sheets[i]).Delete();

                        app.ActiveWorkbook.Save();
                        app.Workbooks.Close();
                        app.Quit();
                    }
                }
                Process.Start(currentPath);
            }
        }

        private void btnPOAttachBackSlip_Click(object sender, EventArgs e)
        {

        }
        void loadFileBackFlip()
        {
            grdFileBackSlip.DataSource = null;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (id <= 0) return;
            List<PaymentOrderFileBankSlipModel> files = SQLHelper<PaymentOrderFileBankSlipModel>.FindByAttribute("PaymentOrderID", id);

            //var files = SQLHelper<PaymentOrderFileBankSlipModel>.GetTableData($"SELECT * FROM dbo.PaymentOrderFileBankSlip WHERE PaymentOrderID = {id}");
            grdFileBackSlip.DataSource = files;
            grvFileBackSlip.BestFitColumns();
            //grvFileBackSlip.RefreshData();

            //grdFileBackSlip.RefreshDataSource();
            //if (files.Count > 0)
            //{
            //    grvFileBackSlip.FocusedRowHandle = 0;
            //}
        }
        void loadFileBackFlip2()
        {
            grdFileBackSlip2.DataSource = null;
            int id = TextUtils.ToInt(grvPaymentSpe.GetFocusedRowCellValue(colID));
            if (id <= 0) return;
            List<PaymentOrderFileBankSlipModel> lstFile = SQLHelper<PaymentOrderFileBankSlipModel>.FindByAttribute("PaymentOrderID", id);
            grdFileBackSlip2.DataSource = lstFile;
            grvFileBackSlip2.BestFitColumns();
            //if (lstFile.Count > 0)
            //{
            //    grvFileBackSlip2.FocusedRowHandle = 0;
            //}
        }

        private void grvFileBackSlip_DoubleClick(object sender, EventArgs e)
        {
            //toolStripMenuItem2_Click(null, null);

            DownloadFileBankSlip(grvData, grvFileBackSlip);
        }

        private void grvFileBackSlip2_DoubleClick(object sender, EventArgs e)
        {
            //toolStripMenuItem2_Click(null, null);

            DownloadFileBankSlip(grvPaymentSpe, grvFileBackSlip2);

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    ToolStripMenuItem toolStrip = (ToolStripMenuItem)sender;
            //    ContextMenuStrip contextMenu = (ContextMenuStrip)toolStrip.Owner;
            //    var gridControl = (GridControl)contextMenu.SourceControl;
            //    var gridView = (GridView)gridControl.MainView;
            //    if (gridView == null) return;
            //    ViewFileBankSlip(gridView);

            //    //string path = TextUtils.ToString(grvFileBackSlip.GetFocusedRowCellValue(colServerPath));
            //    //string fileName = TextUtils.ToString(grvFileBackSlip.GetFocusedRowCellValue(colFileName));

            //    //Process.Start(Path.Combine(path, fileName));
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

            try
            {
                ToolStripMenuItem toolStrip = (ToolStripMenuItem)sender;
                ContextMenuStrip contextMenu = (ContextMenuStrip)toolStrip.Owner;
                var gridControl = (GridControl)contextMenu.SourceControl;
                var gridView = (GridView)gridControl.MainView;
                if (gridView == null) return;

                string path = TextUtils.ToString(gridView.GetFocusedRowCellValue(colServerPath));
                string fileName = TextUtils.ToString(gridView.GetFocusedRowCellValue(colFileName));

                Process.Start(Path.Combine(path, fileName));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var tabSelected = xtraTabControl1.SelectedTabPage;
            if (tabSelected.Controls.Count <= 0) return;

            //var a = tabSelected.Controls[0];
            GridControl gridControl = (GridControl)tabSelected.Controls[0].Controls[0].Controls[0];
            GridView gridView = gridControl.MainView as GridView;

            if (gridView == null) return;

            ToolStripMenuItem toolStrip = (ToolStripMenuItem)sender;
            ContextMenuStrip contextMenu = (ContextMenuStrip)toolStrip.Owner;
            var gridControlFile = (GridControl)contextMenu.SourceControl;
            var gridViewFile = (GridView)gridControlFile.MainView;
            if (gridViewFile == null) return;
            DownloadFileBankSlip(gridView, gridViewFile);
            //try
            //{
            //    string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            //    string pathDownload = Path.Combine(userFolder, "Downloads", "DeNghiThanhToan");

            //    if (!Directory.Exists(pathDownload))
            //    {
            //        Directory.CreateDirectory(pathDownload);
            //    }

            //    DateTime dateOrder = TextUtils.ToDate5(grvData.GetFocusedRowCellValue(colDateOrder));
            //    string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            //    string pathPattern = $@"NĂM {dateOrder.Year}/ĐỀ NGHỊ THANH TOÁN/THÁNG {dateOrder.ToString("MM.yyyy")}/{dateOrder.ToString("dd.MM.yyyy")}/{code}";

            //    string fileName = TextUtils.ToString(grvFileBackSlip.GetFocusedRowCellValue(colFileName));
            //    string folderDownload = Path.Combine(pathDownload, fileName);
            //    string url = $"http://113.190.234.64:8083/api/paymentorder/{pathPattern}/{fileName}";

            //    if (File.Exists(folderDownload))
            //    {
            //        folderDownload = Path.Combine(pathDownload, $"{Path.GetFileNameWithoutExtension(fileName)}_{code}_{DateTime.Now.ToString("HHmmss")}.{Path.GetExtension(fileName)}");
            //    }


            //    WebClient webClient = new WebClient();
            //    webClient.DownloadFile(url, folderDownload);
            //    if (Global.DepartmentID == 4) Process.Start(pathDownload);
            //    Process.Start(folderDownload);
            //}
            //catch
            //{
            //    try
            //    {
            //        string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            //        string pathDownload = Path.Combine(userFolder, "Downloads", "DeNghiThanhToan");

            //        if (!Directory.Exists(pathDownload))
            //        {
            //            Directory.CreateDirectory(pathDownload);
            //        }

            //        DateTime dateOrder = TextUtils.ToDate5(grvData.GetFocusedRowCellValue(colCreatedDate.FieldName));
            //        string code = TextUtils.ToString(grvData.GetFocusedRowCellValue(colCode));
            //        string pathPattern = $@"NĂM {dateOrder.Year}/ĐỀ NGHỊ THANH TOÁN/THÁNG {dateOrder.ToString("MM.yyyy")}/{dateOrder.ToString("dd.MM.yyyy")}/{code}";

            //        string fileName = TextUtils.ToString(grvFileBackSlip.GetFocusedRowCellValue(colFileName));
            //        string folderDownload = Path.Combine(pathDownload, fileName);
            //        string url = $"http://113.190.234.64:8083/api/paymentorder/{pathPattern}/{fileName}";

            //        if (File.Exists(folderDownload))
            //        {
            //            folderDownload = Path.Combine(pathDownload, $"{Path.GetFileNameWithoutExtension(fileName)}_{code}_{DateTime.Now.ToString("HHmmss")}.{Path.GetExtension(fileName)}");
            //        }


            //        WebClient webClient = new WebClient();
            //        webClient.DownloadFile(url, folderDownload);
            //        if (Global.DepartmentID == 4) Process.Start(pathDownload);
            //        Process.Start(folderDownload);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, "Thông báo");
            //    }

            //}
        }


        void DownloadFileBankSlip(GridView gridView, GridView gridViewFile)
        {
            try
            {
                string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string pathDownload = Path.Combine(userFolder, "Downloads", "DeNghiThanhToan");

                if (!Directory.Exists(pathDownload))
                {
                    Directory.CreateDirectory(pathDownload);
                }

                DateTime dateOrder = TextUtils.ToDate5(gridView.GetFocusedRowCellValue(colDateOrder));
                string code = TextUtils.ToString(gridView.GetFocusedRowCellValue(colCode));
                string pathPattern = $@"NĂM {dateOrder.Year}/ĐỀ NGHỊ THANH TOÁN/THÁNG {dateOrder.ToString("MM.yyyy")}/{dateOrder.ToString("dd.MM.yyyy")}/{code}";

                string fileName = TextUtils.ToString(gridViewFile.GetFocusedRowCellValue(colFileName));
                string folderDownload = Path.Combine(pathDownload, fileName);
                string url = $"http://113.190.234.64:8083/api/paymentorder/{pathPattern}/{fileName}";

                if (File.Exists(folderDownload))
                {
                    folderDownload = Path.Combine(pathDownload, $"{Path.GetFileNameWithoutExtension(fileName)}_{code}_{DateTime.Now.ToString("HHmmss")}.{Path.GetExtension(fileName)}");
                }


                WebClient webClient = new WebClient();
                webClient.DownloadFile(url, folderDownload);
                if (Global.DepartmentID == 4) Process.Start(pathDownload);
                Process.Start(folderDownload);
            }
            catch
            {
                try
                {
                    string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    string pathDownload = Path.Combine(userFolder, "Downloads", "DeNghiThanhToan");

                    if (!Directory.Exists(pathDownload))
                    {
                        Directory.CreateDirectory(pathDownload);
                    }

                    DateTime dateOrder = TextUtils.ToDate5(gridView.GetFocusedRowCellValue(colCreatedDate.FieldName));
                    string code = TextUtils.ToString(gridView.GetFocusedRowCellValue(colCode));
                    string pathPattern = $@"NĂM {dateOrder.Year}/ĐỀ NGHỊ THANH TOÁN/THÁNG {dateOrder.ToString("MM.yyyy")}/{dateOrder.ToString("dd.MM.yyyy")}/{code}";

                    string fileName = TextUtils.ToString(gridViewFile.GetFocusedRowCellValue(colFileName));
                    string folderDownload = Path.Combine(pathDownload, fileName);
                    string url = $"http://113.190.234.64:8083/api/paymentorder/{pathPattern}/{fileName}";

                    if (File.Exists(folderDownload))
                    {
                        folderDownload = Path.Combine(pathDownload, $"{Path.GetFileNameWithoutExtension(fileName)}_{code}_{DateTime.Now.ToString("HHmmss")}.{Path.GetExtension(fileName)}");
                    }


                    WebClient webClient = new WebClient();
                    webClient.DownloadFile(url, folderDownload);
                    if (Global.DepartmentID == 4) Process.Start(pathDownload);
                    Process.Start(folderDownload);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Thông báo");
                }

            }
        }
        private void btnPOAttachBankSlip_Click(object sender, EventArgs e)
        {
            bool isSpeTab = xtraTabControl1.SelectedTabPage == xtraTabPage1;
            int id = isSpeTab ? TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID")) : TextUtils.ToInt(grvPaymentSpe.GetFocusedRowCellValue("ID"));
            frmPOAttachBackSlip frm = new frmPOAttachBackSlip();
            frm.PaymentOrderID = id;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //if (isSpeTab)
                loadFileBackFlip();
                //else
                loadFileBackFlip2();
            }
        }

        private void btnContract_Click(object sender, EventArgs e)
        {
            frmContractSummary frm = new frmContractSummary();
            frm.Show();
        }
    }
}
