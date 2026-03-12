using BMS;
using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Forms.Sale.RequestInvoice
{
    public partial class frmStatusRequestInvoiceLink: _Forms
    {
        public int requestInvoiceID = 0;
        public int customerID = 0;
        DataTable dtStatus = new DataTable();
        List<RequestInvoiceStatusModel> listStatuses = new List<RequestInvoiceStatusModel>();
        List<FileInfo> listFileUpload = new List<FileInfo>();
        List<RequestInvoiceFileModel> listFiles = new List<RequestInvoiceFileModel>();
        List<RequestInvoiceFileModel> listFileDelete = new List<RequestInvoiceFileModel>();

        public frmStatusRequestInvoiceLink()
        {
            InitializeComponent();
            
        }
        private void frmStatusRequestInvoiceLink_Load(object sender, EventArgs e)
        {
            listStatuses = SQLHelper<RequestInvoiceStatusModel>.FindAll();
            loadData();
            loadStatus();
            loadStatusFile();
            grdDataFile.ContextMenuStrip = contextMenuStrip1;
        }

        private void loadData()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetRequestInvoiceStatus", "A", new string[] { "@RequestInvoiceID" },
                           new object[] { requestInvoiceID });
            grdMaster.DataSource = dt;
        }
        void loadStatus()
        {
            dtStatus = TextUtils.Select($"SELECT * FROM RequestInvoiceStatus WHERE IsDeleted <> 1");
            cboStatus.DisplayMember = "StatusName";
            cboStatus.ValueMember = "ID";
            cboStatus.DataSource = dtStatus;
        }
        void loadStatusFile()
        {
            if(requestInvoiceID > 0)
            {
                //DataTable dtFile = TextUtils.Select($@"SELECT * FROM RequestInvoiceFile WHERE RequestInvoiceID = {requestInvoiceID} AND FileType = 2");
                var exp1 = new BMS.Utils.Expression("RequestInvoiceID", requestInvoiceID);
                var exp2 = new BMS.Utils.Expression("FileType", 2);
                var exp3 = new BMS.Utils.Expression("IsDeleted", 0);
                List<RequestInvoiceFileModel> listFiles = SQLHelper<RequestInvoiceFileModel>.FindByExpression(exp1.And(exp2).And(exp3));
                grdDataFile.DataSource = listFiles;
            }    
        }
        void LoadFile(List<RequestInvoiceFileModel> listFiles)
        {
            grdDataFile.DataSource = listFiles;
            grvDataFile.RefreshData();
        }
        private void grvMaster_CellValueChanged_1(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var view = sender as GridView;
            if (e.Column.FieldName == "StatusID")
            {
                var newStatusID = TextUtils.ToInt(e.Value);

                for (int i = 0; i < view.RowCount; i++)
                {
                    if (i == e.RowHandle) continue; // bỏ qua dòng đang edit

                    var otherStatusID = TextUtils.ToInt(view.GetRowCellValue(i, "StatusID"));

                    if (newStatusID == otherStatusID)
                    {
                        MessageBox.Show(
                            $"Trạng thái đã tồn tại",
                            "Lỗi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );

                        view.SetRowCellValue(e.RowHandle, e.Column, null);

                        break;
                    }
                }
            }

            if (e.Column.FieldName == "StatusID")
            {
                int statusId = TextUtils.ToInt(e.Value);
                loadStatusProgress(statusId);
            }
        }
        private void grvMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int statusId = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colStatus));
            loadStatusProgress(statusId);
        }
        void loadStatusProgress(int statusId)
        {
            DataTable dtProgress = new DataTable();
            dtProgress.Columns.Add("Name");
            dtProgress.Columns.Add("Value");

            switch (statusId)
            {
                case 1:
                    dtProgress.Rows.Add("Chờ duyệt", 1);
                    dtProgress.Rows.Add("Duyệt", 2);
                    dtProgress.Rows.Add("Không duyệt", 3);
                    dtProgress.Rows.Add("Yêu cầu bổ sung", 4);
                    break;

                case 2:
                    dtProgress.Rows.Add("Chờ duyệt", 1);
                    dtProgress.Rows.Add("Duyệt", 2);
                    dtProgress.Rows.Add("Không duyệt", 3);
                    dtProgress.Rows.Add("Yêu cầu bổ sung", 4);
                    break;

                case 3:
                    dtProgress.Rows.Add("Chờ duyệt", 1);
                    dtProgress.Rows.Add("Duyệt", 2);
                    dtProgress.Rows.Add("Không duyệt", 3);
                    dtProgress.Rows.Add("Yêu cầu bổ sung", 4);
                    break;

                default:
                    dtProgress.Rows.Add("Chờ duyệt", 1);
                    dtProgress.Rows.Add("Duyệt", 2); 
                    dtProgress.Rows.Add("Không duyệt", 3);
                    dtProgress.Rows.Add("Yêu cầu bổ sung", 4);
                    break;
            }

            cboStatusProgress.DataSource = dtProgress;
            cboStatusProgress.DisplayMember = "Name";
            cboStatusProgress.ValueMember = "Value";
        }

        private void btnChosenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in dialog.FileNames)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    RequestInvoiceFileModel fileRequest = new RequestInvoiceFileModel()
                    {
                        FileName = fileInfo.Name,
                        OriginPath = fileInfo.DirectoryName
                    };

                    listFiles.Insert(0, fileRequest);
                    listFileUpload.Add(fileInfo);
                }
                LoadFile(listFiles);
            }
        }

        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvDataFile.GetFocusedRowCellValue("ID"));
            string fileName = TextUtils.ToString(grvDataFile.GetFocusedRowCellValue("FileName"));

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá file tờ khai hải quan: [{fileName}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                grvDataFile.DeleteSelectedRows();
                if (id <= 0) return;
                RequestInvoiceFileModel file = SQLHelper<RequestInvoiceFileModel>.FindByID(id);
                listFileDelete.Add(file);
            }
        }


        private void grvMaster_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvMaster.CalcHitInfo(new Point(e.X, e.Y));
                if (info.Column == colSTTStatus && e.Y < 40)
                {
                    grvMaster.FocusedRowHandle = -1;
                    DataTable dt = (DataTable)grdMaster.DataSource;
                    dt.AcceptChanges();
                    DataRow dtrow = dt.NewRow();

                    //int stt = dt.Rows.Count;
                    //int idMapping = TextUtils.ToInt(grvMaster.GetRowCellValue(stt - 1, colSTTStatus));
                    //dtrow["STT"] = stt + 1;
                    dt.Rows.Add(dtrow);
                    grdMaster.DataSource = dt;
                }
            }
        }
        List<RequestInvoiceStatusLinkModel> lstDeleteStatus = new List<RequestInvoiceStatusLinkModel>();
        private void btnDeleteStatus_Click(object sender, EventArgs e)
        {
                int id = TextUtils.ToInt(grvMaster.GetFocusedRowCellValue(colID));
                //string statusName = TextUtils.ToString(grvMaster.GetFocusedRowCellValue(colStatus));
                DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá trạng thái này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    grvMaster.DeleteSelectedRows();
                    if (id <= 0) return;
                RequestInvoiceStatusLinkModel item = SQLHelper<RequestInvoiceStatusLinkModel>.FindByID(id);
                    lstDeleteStatus.Add(item);
                }

                //if (id > 0) lstDeleteStatusID.Add(id);
                //grvMaster.DeleteSelectedRows();
                //for (int i = 0; i < grvMaster.RowCount; i++)
                //{
                //    grvMaster.SetRowCellValue(i, colSTTStatus, i + 1);
                //}

        }
        bool saveData()
        {
            grvMaster.CloseEditor(); 
            if (!validate()) return false;
            for (int i = 0; i < grvMaster.RowCount; i++)
            {
                int detailID = TextUtils.ToInt(grvMaster.GetRowCellValue(i, colID));
                RequestInvoiceStatusLinkModel model = SQLHelper<RequestInvoiceStatusLinkModel>.FindByID(detailID) ?? new RequestInvoiceStatusLinkModel();
                model.RequestInvoiceID = requestInvoiceID;
                model.StatusID = TextUtils.ToInt(grvMaster.GetRowCellValue(i, colStatus));
                model.IsApproved = TextUtils.ToInt(grvMaster.GetRowCellValue(i, colIsApproved));
                model.IsCurrent = TextUtils.ToBoolean(grvMaster.GetRowCellValue(i, colIsCurrent));
                model.AmendReason = TextUtils.ToString(grvMaster.GetRowCellValue(i, colAmendReason));
                model.CreatedBy = Global.AppUserName;
                model.CreatedDate = model.CreatedDate ?? DateTime.Now;
                model.UpdatedBy = Global.AppUserName;
                model.UpdatedDate = DateTime.Now;
                model.IsDeleted = false;

                if(model.IsCurrent == true)
                {
                    string sql = string.Format($@"UPDATE RequestInvoiceStatusLink SET IsCurrent = 0 WHERE RequestInvoiceID = {requestInvoiceID} AND ID <> {model.ID}");
                    TextUtils.ExcuteSQL(sql);
                }   
                
                if (model.ID > 0)
                {
                    SQLHelper<RequestInvoiceStatusLinkModel>.Update(model);
                }
                else
                {
                    SQLHelper<RequestInvoiceStatusLinkModel>.Insert(model);
                }

            }   
            foreach( var item in lstDeleteStatus)
            {
                var myDict = new Dictionary<string, object>()
                        {
                            { RequestInvoiceStatusLinkModel_Enum.IsDeleted.ToString(),true},
                            { RequestInvoiceStatusLinkModel_Enum.UpdatedBy.ToString(),Global.AppUserName},
                            { RequestInvoiceStatusLinkModel_Enum.UpdatedDate.ToString(),DateTime.Now},
                        };

                SQLHelper<RequestInvoiceStatusLinkModel>.UpdateFieldsByID(myDict, item.ID);
            }    
            UploadFile(requestInvoiceID);
            if(listFileDelete.Count > 0) { RemoveFile(); }

            this.DialogResult = DialogResult.OK;
            return true;
        }
        public async void RemoveFile()
        {
            try
            {
                if (listFileDelete.Count <= 0) return;
                var url = $"http://113.190.234.64:8083/api/Home/removefile?path=";
                //var url = $"http://localhost:8390/api/Home/removefile?path=";
                var client = new HttpClient();
                foreach (var item in listFileDelete)
                {
                    url += $@"{item.ServerPath}\{item.FileName}";
                    var result = await client.GetAsync(url);

                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        //SQLHelper<RequestInvoiceFileModel>.Delete(item);
                        var myDict = new Dictionary<string, object>()
                        {
                            { RequestInvoiceFileModel_Enum.IsDeleted.ToString(),true},
                            { RequestInvoiceFileModel_Enum.UpdatedBy.ToString(),Global.AppUserName},
                            { RequestInvoiceFileModel_Enum.UpdatedDate.ToString(),DateTime.Now},
                        };

                        SQLHelper<RequestInvoiceModel>.UpdateFieldsByID(myDict, item.ID);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }
        public async void UploadFile(int requestInvoiceID)
        {
            try
            {
                //ConfigSystemModel config = SQLHelper<ConfigSystemModel>.FindByAttribute("KeyName", "RequestInvoiceFile").FirstOrDefault();
                //if (config == null || string.IsNullOrEmpty(config.KeyValue))
                //{
                //    MessageBox.Show("Vui lòng chọn đường dẫn lưu trên server!", "Thông báo");
                //    return;
                //}
                //DataTable dt = TextUtils.Select($@"SELECT CustomerShortName FROM Customer WHERE ID = {customerID}");

                CustomerModel customer = SQLHelper<CustomerModel>.FindByID(customerID);
                RequestInvoiceModel requestInvoice = SQLHelper<RequestInvoiceModel>.FindByID(requestInvoiceID);
                if (requestInvoice == null || requestInvoice.ID <= 0) return;
                if (requestInvoice.EmployeeRequestID != Global.EmployeeID && !Global.IsAdmin) return;
                //string customerShortName = dt.Rows[0]["CustomerShortName"].ToString();
                //string customerShortName = customer.CustomerShortName;
                string year = DateTime.Now.Year.ToString();
                string month = DateTime.Now.Month.ToString();
                //string pathServer = "\\\\192.168.1.190\\Logistic\\TỜ KHAI XUẤT KHẨU\\TK xuất khẩu\\";
                string pathServer = @"D:\\LeTheAnh\\Image\\Upload\\Logistic\\TỜ KHAI XUẤT KHẨU\\TK xuất khẩu\\";
                string pathPattern = $@"{customer.CustomerShortName}\{year}\T{month}\{requestInvoice.Code}\";
                string pathUpload = Path.Combine(pathServer, pathPattern);

                var client = new HttpClient();
                //var content = new MultipartFormDataContent();

                List<PaymentOrderFileModel> listFiles = new List<PaymentOrderFileModel>();
                foreach (var file in listFileUpload)
                {
                    RequestInvoiceFileModel fileRequest = new RequestInvoiceFileModel();
                    fileRequest.RequestInvoiceID = requestInvoice.ID;
                    fileRequest.FileName = file.Name;
                    fileRequest.OriginPath = file.DirectoryName;
                    fileRequest.ServerPath = pathUpload;
                    fileRequest.FileType = 2; // type = 2: Kiểu file thuộc loại Tờ khai chứng từ
                    //SQLHelper<PaymentOrderFileModel>.Insert(fileOrder);

                    if (file.Length < 0) continue;

                    //using var fileStream = file.OpenReadStream();
                    var fileStream = new FileStream(file.FullName, FileMode.Open);
                    byte[] bytes = new byte[file.Length];
                    fileStream.Read(bytes, 0, (int)file.Length);
                    var byteArrayContent = new ByteArrayContent(bytes);

                    MultipartFormDataContent content = new MultipartFormDataContent();
                    content.Add(byteArrayContent, "file", file.Name);

                    var url = $"http://113.190.234.64:8083/api/Home/uploadfile?path={pathUpload}";
                    var result = await client.PostAsync(url, content);
                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        SQLHelper<RequestInvoiceFileModel>.Insert(fileRequest);
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }
        private bool validate()
        {
            bool hasCurrent = false;
            for (int i = 0; i < grvMaster.RowCount; i++)
            {
                var isApproved = TextUtils.ToInt(grvMaster.GetRowCellValue(i, "IsApproved"));
                var amendReason = grvMaster.GetRowCellValue(i, "AmendReason")?.ToString().Trim();
                var isCurrent = TextUtils.ToBoolean(grvMaster.GetRowCellValue(i, "IsCurrent"));
                if (isApproved == 4 && string.IsNullOrEmpty(amendReason))
                {
                    MessageBox.Show(
                        $"Dòng {i + 1}: Bắt buộc nhập Lý do yêu cầu bổ sung khi tình trạng là yêu cầu bổ sung",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    grvMaster.FocusedRowHandle = i;
                    grvMaster.FocusedColumn = grvMaster.Columns["AmendReason"];
                    return false;
                }

                if (isCurrent) hasCurrent = true;
            }

            if (!hasCurrent)
            {
                MessageBox.Show(
                    "Vui lòng chọn trạng thái hiện tại",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }

            return true;
        }

        private void grvMaster_ShowingEditor(object sender, CancelEventArgs e)
        {
            var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;

            if (view.FocusedColumn.FieldName == "AmendReason")
            {
                var value = view.GetRowCellValue(view.FocusedRowHandle, "IsApproved");

                if (value == null || TextUtils.ToInt(value) != 4)
                {
                    e.Cancel = true; 
                }
            }
        }

        private void grvMaster_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "IsCurrent")
            {
                // Bỏ check tất cả dòng khác
                for (int i = 0; i < grvMaster.RowCount; i++)
                {
                    if (i != e.RowHandle)
                    {
                        grvMaster.SetRowCellValue(i, e.Column, false);
                    }
                }

                // Đặt giá trị true cho dòng hiện tại
                grvMaster.SetRowCellValue(e.RowHandle, e.Column, true);
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (saveData())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnStatusManagement_Click(object sender, EventArgs e)
        {
            frmRequestInvoiceStatus frm = new frmRequestInvoiceStatus();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                loadStatus();
                loadData();
            }
        }

        private void btnViewFile_Click(object sender, EventArgs e)
        {
            try
            {
                string path = TextUtils.ToString(grvDataFile.GetFocusedRowCellValue(colServerPath));
                string fileName = TextUtils.ToString(grvDataFile.GetFocusedRowCellValue(colFileName));

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
                //string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                //string pathDownload = Path.Combine(userFolder, "Downloads", "ToKhaiHaiQuan");

                //if (!Directory.Exists(pathDownload))
                //{
                //    Directory.CreateDirectory(pathDownload);
                //}
                //string pathServer = "pokhhn";
                //POKHModel pokh = SQLHelper<POKHModel>.FindByID(TextUtils.ToInt(grvDataFile.GetFocusedRowCellValue(colPOKHID)));

                //int warehouseID = pokh.WarehouseID;
                //if (warehouseID == 2) pathServer = "pokhhcm";
                ////string poNumber = TextUtils.ToString(grvMaster.GetFocusedRowCellValue("PONumber"));
                //string poNumber = TextUtils.ToString(pokh.PONumber);

                //string fileName = TextUtils.ToString(grvDataFile.GetFocusedRowCellValue(colFileName));
                //string folderDownload = Path.Combine(pathDownload, fileName);
                //string url = $"http://113.190.234.64:8083/api/{pathServer}/{poNumber}/{fileName}";

                //WebClient webClient = new WebClient();
                //webClient.DownloadFile(url, folderDownload);
                //Process.Start(folderDownload);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void grvMaster_RowStyle(object sender, RowStyleEventArgs e)
        {

        }
    }
}
