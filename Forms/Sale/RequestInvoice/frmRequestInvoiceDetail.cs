using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmRequestInvoiceDetail : _Forms
    {
        public int IDDetail;
        public RequestInvoiceModel requestInvoice = new RequestInvoiceModel();
        ArrayList lstIDDelete = new ArrayList();

        List<int> lstDeletedDetails = new List<int>();
        List<int> lstDeletedFile = new List<int>();
        DataTable dtProductSale = new DataTable();
        DataTable dtProject = new DataTable();
        DataTable dtCustomer = new DataTable();
        DataTable dtProduct = new DataTable();
        ArrayList lstSelect = new ArrayList();
        List<int> rowSelectedRow = new List<int>();
        List<FileInfo> listFileUpload = new List<FileInfo>();
        List<RequestInvoiceFileModel> listFiles = new List<RequestInvoiceFileModel>();
        List<RequestInvoiceFileModel> listFileDelete = new List<RequestInvoiceFileModel>();

        List<ProductSaleModel> lsProductSale = new List<ProductSaleModel>();
        List<POKHDetailModel> lstPODetail = new List<POKHDetailModel>();
        List<ProjectModel> lsProject = new List<ProjectModel>();
        List<CustomerModel> lstCustomer = new List<CustomerModel>();



        public int customerID = 0;
        public bool isPOKH = false;
        string projectCode = "";
        public DataTable dtDetail = new DataTable();
        List<int> listSelected = new List<int>(); // NTA B - update 17/10/25
        public frmRequestInvoiceDetail()
        {
            InitializeComponent();
            grdFile.ContextMenuStrip = contextMenuStrip1;
        }

        private void frmRequestInvoiceDetail_Load(object sender, EventArgs e)
        {

            loadCustomer();
            loadStatus();
            //loadSender();
            loadProject();
            loadProduct();
            loadPODetail();
            loadBillNumber();
            loadUsers();
            loadTaxCompany();

            loadRequestInvoiceDetail();
            //this.cbProduct.EditValueChanged += new System.EventHandler(cbProduct_EditValueChanged);

        }

        private void loadRequestInvoiceDetail()
        {
            if (requestInvoice.ID > 0)
            {
                txtCode.Text = requestInvoice.Code;
                cboUser.EditValue = requestInvoice.EmployeeRequestID;
                cboCustomer.EditValue = requestInvoice.CustomerID;
                //cboSender.EditValue = requestInvoice.ReceriverID;
                txtNote.Text = requestInvoice.Note;
                cboTaxCompany.EditValue = requestInvoice.TaxCompanyID;
                cboStatusNew.EditValue = requestInvoice.Status;
                chkIsCustomsDeclared.Checked = TextUtils.ToBoolean(requestInvoice.IsCustomsDeclared);
            }

            if (isPOKH)
            {
                dtDetail.DefaultView.Sort = "STT ASC";
                dtDetail = dtDetail.DefaultView.ToTable();
                dtDetail.AcceptChanges();
                grdData.DataSource = dtDetail;
                grvData.CloseEditor();
                grvData.FocusedRowHandle = -1;
            }
            else
            {
                dtDetail = TextUtils.LoadDataFromSP("spGetRequestInvoiceDetailsByID", "A", new string[] { "@RequestInvoiceID" }, new object[] { requestInvoice.ID });
                grdData.DataSource = dtDetail;
            }

            LoadFile();
        }

        void LoadFile()
        {
            List<RequestInvoiceFileModel> files = SQLHelper<RequestInvoiceFileModel>.FindByAttribute(RequestInvoiceFileModel_Enum.RequestInvoiceID.ToString(), requestInvoice.ID);
            grdDataFile.DataSource = files;
        }
        void LoadPOKHFile()
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colPOKHID));
            List<POKHFileModel> files = SQLHelper<POKHFileModel>.FindByAttribute("POKHID", id);
            grdFile.DataSource = files;
        }
        void loadBillNumber()
        {
            int number = 0;
            string month = TextUtils.ToString(DateTime.Now.ToString("MM"));
            string day = TextUtils.ToString(DateTime.Now.ToString("dd"));
            string year = TextUtils.ToString(DateTime.Now.Year).Substring(2);
            string date = year + month + day;
            string Billcode = TextUtils.ToString(TextUtils.ExcuteScalar($"SELECT TOP 1 Code FROM RequestInvoice Where Month(CreatedDate)={DateTime.Now.Month} and Year(CreatedDate)={DateTime.Now.Year} and Day(CreatedDate)={DateTime.Now.Day} ORDER BY ID DESC"));
            if (requestInvoice.ID == 0)
            {
                if (Billcode == "") // ktra tháng bdau và tháng đc update
                {
                    txtCode.Text = "YCXHD" + date + "001";
                    return;
                }
                else number = TextUtils.ToInt(Billcode.Substring(Billcode.Length - 3)); // tách lấy 3 số cuối convert sang int
                string dem = TextUtils.ToString(number + 1);
                for (int i = 0; dem.Length < 3; i++)
                {
                    dem = "0" + dem;
                }
                txtCode.Text = "YCXHD" + date + TextUtils.ToString(dem);
            }
        }

        private void loadStatus()
        {
            List<object> lst = new List<object>()
            {
                new {ID = 1, Name = "Yêu cầu xuất hóa đơn"},
                new {ID = 2, Name = "Đã xuất nháp"},
                new {ID = 3, Name = "Đã phát hành hóa đơn"},
            };
            cboStatusNew.Properties.DataSource = lst;
            cboStatusNew.Properties.ValueMember = "ID";
            cboStatusNew.Properties.DisplayMember = "Name";

            cboStatusNew.EditValue = 1;
        }
        public void loadCustomer()
        {

            //List<CustomerModel> lstCustomer = SQLHelper<CustomerModel>.FindAll();
            dtCustomer = new DataTable();
            dtCustomer = TextUtils.Select("SELECT * FROM Customer where IsDeleted <> 1");
            cboCustomer.Properties.DisplayMember = "CustomerName";
            cboCustomer.Properties.ValueMember = "ID";
            cboCustomer.Properties.DataSource = dtCustomer;
            cboCustomer.EditValue = requestInvoice.CustomerID;


        }
        private void loadPODetail()
        {
            List<POKHDetailModel> lstPODetail = SQLHelper<POKHDetailModel>.FindAll();
            cbPODetail.DisplayMember = "ID";
            cbPODetail.ValueMember = "ID";
            cbPODetail.DataSource = lstPODetail;
        }
        private void loadTaxCompany()
        {
            List<object> list = new List<object>()
            {
                new {ID = 1, TaxCompany = "RTC"},
                new {ID = 2, TaxCompany = "APR"},
                new {ID = 3, TaxCompany = "MVI"},
                new {ID = 4, TaxCompany = "Yonko"},

            };
            cboTaxCompany.Properties.DataSource = list;
            cboTaxCompany.Properties.ValueMember = "ID";
            cboTaxCompany.Properties.DisplayMember = "TaxCompany";
            if (requestInvoice.ID == 0) cboTaxCompany.EditValue = 1;

        }
        public void loadUsers()
        {

            List<EmployeeModel> lstEmployee = SQLHelper<EmployeeModel>.FindByAttribute("Status", 0);
            cboUser.Properties.DisplayMember = "FullName";
            cboUser.Properties.ValueMember = "ID";
            cboUser.Properties.DataSource = lstEmployee;
            cboUser.EditValue = Global.EmployeeID;
        }

        //public void loadSender()
        //{

        //    List<UsersModel> lstUser = SQLHelper<UsersModel>.FindAll();
        //    cboSender.Properties.DisplayMember = "FullName";
        //    cboSender.Properties.ValueMember = "ID";
        //    cboSender.Properties.DataSource = lstUser;

        //    if (requestInvoice != null)
        //    {
        //        cboSender.EditValue = requestInvoice.ReceriverID;
        //    }
        //}
        private void loadProject()
        {

            lsProject = SQLHelper<ProjectModel>.FindAll();
            cbProject.DisplayMember = "ProjectName";
            cbProject.ValueMember = "ID";
            cbProject.DataSource = lsProject;

        }
        private void loadProduct()
        {
            lsProductSale = SQLHelper<ProductSaleModel>.FindAll();
            cbProduct.DisplayMember = "ProductCode";
            cbProduct.ValueMember = "ID";
            cbProduct.DataSource = lsProductSale;
            //colProductSaleID.ColumnEdit = cbProduct;
        }

        private void cboCustomer_EditValueChanged(object sender, EventArgs e)
        {

            if (dtCustomer.Rows.Count <= 0) return;
            if (cboCustomer.Text.Trim() == "") return;
            DataRow[] dr = dtCustomer.Select($"ID={cboCustomer.EditValue}");
            txtAddress.Text = TextUtils.ToString(dr[0]["Address"]);

            //if (lstCustomer.Count <= 0) return;
            //if (cboCustomer.Text.Trim() == "") return;
            //var dt = lstCustomer.Where(x => x.ID == (int)cboCustomer.EditValue);
            //txtAddress.Text = TextUtils.ToString(dt);
        }


        private bool ValidateForm()
        {
            DataTable dt;
            if (requestInvoice.ID > 0)
            {
                string Billcode = txtCode.Text.Trim();
                if (Billcode.Contains("YCXHD"))
                {
                    Billcode = Billcode.Substring(4);
                }
                int strID = requestInvoice.ID;

                dt = TextUtils.Select($"Select top 1 ID From RequestInvoice Where Code LIKE '%{Billcode}%' and ID <> {strID}");
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Số phiếu này đã tồn tại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }
            else
            {
                dt = TextUtils.Select("Select top 1 ID From RequestInvoice where Code = '" + txtCode.Text.Trim() + "'");
                if (dt.Rows.Count > 0)
                {
                    loadBillNumber();
                    MessageBox.Show($"Phiếu đã tồn tại. Phiểu được đổi tên thành: {txtCode.Text.Trim()}", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if (txtCode.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy điền số phiếu.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            //else if (txtInvoiceNumber.Text.Trim() == "")
            //{
            //    MessageBox.Show("Xin hãy điền số hóa đơn.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}
            else if (cboCustomer.Text.Trim() == "")
            {
                MessageBox.Show("Xin hãy chọn khách hàng.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else if (TextUtils.ToInt(cboStatusNew.EditValue) < 0)
            {
                MessageBox.Show("Xin hãy chọn trạng thái.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            //else if (cboSender.Text.Trim() == "")
            //{
            //    MessageBox.Show("Xin hãy chọn người gửi.", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}

            for (int i = 0; i < grvData.RowCount; i++) //NTA B - update 17/09/25
            {
                bool isStock = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsStock));
                string billCode = TextUtils.ToString(grvData.GetRowCellValue(grvData.FocusedRowHandle, colBillImportCode));
                decimal quantity = TextUtils.ToDecimal(grvData.GetRowCellValue(grvData.FocusedRowHandle, colQuantity));

                string productNewCode = TextUtils.ToString(grvData.GetRowCellValue(i, colProductNewCode));
                int stt = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));
                if (!isStock)
                {
                    string someBill = TextUtils.ToString(grvData.GetRowCellValue(i, colSomeBill));
                    string productName = TextUtils.ToString(grvData.GetRowCellDisplayText(i, colProductSaleID));

                    if (someBill == "")
                    {
                        MessageBox.Show($"Vì không là hàng lầy từ Tồn kho, bắt buộc phải có Hóa đơn đầu vào cho mã sản phẩm {productNewCode} - STT: {stt}", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }
                }
                else if (!string.IsNullOrWhiteSpace(billCode))
                {
                    MessageBox.Show($"Bạn không thể chọn Tồn kho vì đã có Phiếu nhập kho [{billCode}] cho mã sản phẩm {productNewCode} - STT: {stt}", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }


                if (quantity <= 0)
                {
                    MessageBox.Show($"Vui lòng nhập Số lượng cho mã sản phẩm {productNewCode} - STT: {stt}", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }

            return true;
        }

        private void dtpCreatDate_ValueChanged(object sender, EventArgs e)
        {
            if (requestInvoice.ID == 0)
            {
                loadBillNumber();
            }
        }

        private void btnAddAndClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            loadBillNumber();
            if (saveData())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        bool saveData()
        {
            grvData.CloseEditor(); // NTA B update 17/09/25
            if (!ValidateForm()) return false;

            txtCode.Focus();
            RequestInvoiceModel model = SQLHelper<RequestInvoiceModel>.FindByID(requestInvoice.ID) ?? new RequestInvoiceModel();
            model.Code = txtCode.Text.Trim();
            model.EmployeeRequestID = TextUtils.ToInt(cboUser.EditValue);
            //model.ReceriverID = TextUtils.ToInt(cboSender.EditValue);
            model.CustomerID = TextUtils.ToInt(cboCustomer.EditValue);
            model.TaxCompanyID = TextUtils.ToInt(cboTaxCompany.EditValue);
            model.Status = TextUtils.ToInt(cboStatusNew.EditValue);
            model.DateRequest = dtpRequestDate.Value;
            model.Note = txtNote.Text.Trim();
            model.IsDeleted = false;
            model.IsUrgency = chIsUrgency.Checked;//ndnhat update 14/08/2025
            model.DealineUrgency = TextUtils.ToDate4(dpkDealineUrgency.EditValue);
            model.IsCustomsDeclared = chkIsCustomsDeclared.Checked;
            //if(TextUtils.ToString(requestInvoice.DateRequest) == "")
            //{
            //    requestInvoice.DateRequest = dtpRequestDate.Value;
            //    FileStream fs = new FileStream("InvoiceLog.text", FileMode.OpenOrCreate);
            //    fs.Write(Encoding.UTF8.GetBytes(dtpRequestDate.Value.ToString()), 0, Encoding.UTF8.GetByteCount(dtpRequestDate.Value.ToString()));
            //    fs.Write(Encoding.UTF8.GetBytes(txtCode.Text), 0, Encoding.UTF8.GetByteCount(txtCode.Text));
            //}
            if (model.ID > 0)
            {
                SQLHelper<RequestInvoiceModel>.Update(model);
            }
            else
            {
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = Global.AppUserName;
                model.ID = SQLHelper<RequestInvoiceModel>.Insert(model).ID;

                //NTA B update 24/09/25
                RequestInvoiceStatusLinkModel statusLinkModelDefault = new RequestInvoiceStatusLinkModel();
                statusLinkModelDefault.RequestInvoiceID = model.ID;
                statusLinkModelDefault.StatusID = 1;
                statusLinkModelDefault.IsApproved = 1;
                statusLinkModelDefault.IsCurrent = true;
                statusLinkModelDefault.CreatedBy = Global.AppUserName;
                statusLinkModelDefault.CreatedDate = DateTime.Now;
                SQLHelper<RequestInvoiceStatusLinkModel>.Insert(statusLinkModelDefault);
                //END

            }




            //============================= Lưu details  =============================
            for (int i = 0; i < grvData.RowCount; i++)
            {
                int detailID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                RequestInvoiceDetailModel requestInvoiceDetail = SQLHelper<RequestInvoiceDetailModel>.FindByID(detailID) ?? new RequestInvoiceDetailModel();

                requestInvoiceDetail.RequestInvoiceID = model.ID;

                requestInvoiceDetail.ProductSaleID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductSaleID));
                requestInvoiceDetail.ProductByProject = TextUtils.ToString(grvData.GetRowCellValue(i, colProductFullName));
                requestInvoiceDetail.Quantity = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQuantity));
                requestInvoiceDetail.ProjectID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProjectID));
                requestInvoiceDetail.POKHDetailID = TextUtils.ToInt(grvData.GetRowCellValue(i, colPOKHDetailID));
                requestInvoiceDetail.Specifications = TextUtils.ToString(grvData.GetRowCellValue(i, colspecifications));
                requestInvoiceDetail.InvoiceNumber = TextUtils.ToString(grvData.GetRowCellValue(i, colInvoiceNumber));
                requestInvoiceDetail.InvoiceDate = TextUtils.ToDate2(grvData.GetRowCellValue(i, colInvoiceDate));
                requestInvoiceDetail.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));
                requestInvoiceDetail.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));
                requestInvoiceDetail.IsStock = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsStock)); //NTA B - update 17/09/25
                if (requestInvoiceDetail.ID > 0)
                {
                    SQLHelper<RequestInvoiceDetailModel>.Update(requestInvoiceDetail);
                }
                else
                {
                    SQLHelper<RequestInvoiceDetailModel>.Insert(requestInvoiceDetail);
                    //grvData.SetRowCellValue(i, colID, requestInvoiceDetail.ID);
                }


                //Update số hóa đơn PO KH
                //if (requestInvoiceDetail.POKHDetailID > 0)
                //{
                //    var myDict = new Dictionary<string, object>()
                //    {
                //        {"BillNumber" ,requestInvoiceDetail.InvoiceNumber},
                //        {"BillDate" ,requestInvoiceDetail.InvoiceDate},
                //        {"UpdatedDate" ,DateTime.Now},
                //    };

                //    SQLHelper<POKHDetailModel>.UpdateFieldsByID(myDict, TextUtils.ToInt(requestInvoiceDetail.POKHDetailID));
                //}

            }

            if (lstIDDelete.Count > 0)
            {
                var myDict = new Dictionary<string, object>()
                {
                    {RequestInvoiceDetailModel_Enum.IsDeleted.ToString(),true },
                    {RequestInvoiceDetailModel_Enum.UpdatedDate.ToString(),DateTime.Now },
                    {RequestInvoiceDetailModel_Enum.UpdatedBy.ToString(),Global.AppUserName },
                };

                var exp = new Expression(RequestInvoiceDetailModel_Enum.ID, string.Join(",", lstIDDelete.ToArray()), "IN");
                SQLHelper<RequestInvoiceDetailModel>.UpdateFields(myDict, exp);

                lstIDDelete.Clear();
            }

            UploadFile(model.ID);
            return true;
        }



        public async void UploadFile(int requestInvoiceID)
        {
            try
            {
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
                        SQLHelper<RequestInvoiceFileModel>.Delete(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\n{ex.ToString()}", "Thông báo");
            }
        }

        private void btnAddAndNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (saveData())
            {
                cboCustomer.Text = "";
                //cboSender.Text = "";
                cboStatusNew.Text = "";
                cboTaxCompany.Text = "";
                cboUser.Text = "";
                txtAddress.Clear();
                txtNote.Clear();
                requestInvoice = new RequestInvoiceModel();
                loadBillNumber();
            }
        }

        private void cbProduct_EditValueChanged(object sender, EventArgs e)
        {
            //grvData.FocusedRowHandle = -1;
            grvData.CloseEditor();
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProductSaleID));
            //DataRow[] rows = dtProduct.Select("ProductSaleID= " + ID);
            var rows = lsProductSale.Where(x => x.ID == ID).FirstOrDefault();
            if (rows != null)
            {
                string productName = TextUtils.ToString(rows.ProductName);
                string productNewCode = TextUtils.ToString(rows.ProductNewCode);
                string unit = TextUtils.ToString(rows.Unit);
                grvData.SetFocusedRowCellValue(colProductName, productName);
                grvData.SetFocusedRowCellValue(colProductNewCode, productNewCode);
                grvData.SetFocusedRowCellValue(colUnit, unit);
                //string productNewCode = TextUtils.ToString(rows[0]["ProductNewCode"]);
                //string unit = TextUtils.ToString(rows[0]["Unit"]);
                //grvData.SetFocusedRowCellValue(colProductName, productName);
                //grvData.SetFocusedRowCellValue(colProductNewCode, productNewCode);
                //grvData.SetFocusedRowCellValue(colUnit, unit);
            }

        }

        private void cbProject_EditValueChanged(object sender, EventArgs e)
        {
            //grvData.FocusedRowHandle = -1;
            grvData.CloseEditor();
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProjectID));
            var rows = lsProject.Where(x => x.ID == ID).FirstOrDefault();
            if (rows != null)
            {
                projectCode = TextUtils.ToString(rows.ProjectCode);

                grvData.SetFocusedRowCellValue(colProjectCode, projectCode);
                //for (int i = 0; i < grvData.RowCount; i++)
                //{
                //    string item = TextUtils.ToString(grvData.GetRowCellValue(i, colProjectCode));
                //    if (item == "" || item == null)
                //    {
                //        grvData.SetRowCellValue(i, colProjectCode, projectCode);
                //    }
                //}
            }
        }

        private void grvData_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvData.CalcHitInfo(e.Location);

                if (info.Column != null && info.Column == colSTT && info.HitTest == GridHitTest.Column)
                {
                    grvData.FocusedRowHandle = -1;
                    dtDetail.AcceptChanges();
                    DataRow dtrow = dtDetail.NewRow();
                    dtrow["STT"] = grvData.RowCount + 1;
                    dtDetail.Rows.Add(dtrow);
                    grdData.DataSource = dtDetail;
                    dtDetail.AcceptChanges();

                }
            }
        }

        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == colQuantity || e.Column == colProductSaleID)
            {
                RequestInvoiceDetailModel detail = new RequestInvoiceDetailModel();
                int idRequestInvoice = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
                //detail = (RequestInvoiceDetailModel)(RequestInvoiceDetailBO.Instance.FindByPK(idRequestInvoice));
                detail = SQLHelper<RequestInvoiceDetailModel>.FindByID(idRequestInvoice);
                int ID = TextUtils.ToInt(grvData.GetRowCellValue(e.RowHandle, colProductSaleID));
                float sum = 0;
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    // kiểm tra 2 mã sp trùng nhau thì tổng Qty được cộng vào
                    int IDSearch = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductSaleID));
                    if (ID == IDSearch)
                    {
                        float qty = TextUtils.ToFloat(grvData.GetRowCellValue(i, colQuantity));
                        sum += qty;
                    }
                    if (idRequestInvoice > 0)
                    {
                        detail.Quantity = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colQuantity));
                        //RequestInvoiceDetailBO.Instance.Update(detail);
                        SQLHelper<RequestInvoiceDetailModel>.Update(detail);
                    }
                }

                // gán tổng Qty vào cột tương ứng (vào cả mã hàng trùng nhau)
                for (int j = 0; j < grvData.RowCount; j++)
                {
                    int IDSearch = TextUtils.ToInt(grvData.GetRowCellValue(j, colProductSaleID));
                    if (ID == IDSearch)
                    {
                        grvData.SetRowCellValue(j, colTotalQty, sum);
                    }
                    if (idRequestInvoice > 0)
                    {
                        detail.Quantity = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colQuantity));
                        //RequestInvoiceDetailBO.Instance.Update(detail);
                        SQLHelper<RequestInvoiceDetailModel>.Update(detail);
                    }
                }
            }
            if (e.Column == colProjectID)
            {
                int projectID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colProjectID));
                if (projectID == 0) return;
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    int item = TextUtils.ToInt(grvData.GetRowCellValue(i, colProjectID));
                    if (item == 0)
                    {
                        grvData.SetRowCellValue(i, colProjectID, projectID);
                    }
                }
            }
        }

        private void btnAddSeialNumber_Click(object sender, EventArgs e)
        {

        }

        //private void btnNew_Click(object sender, EventArgs e)
        //{
        //    int STT;
        //    DataTable dt = (DataTable)grdData.DataSource;

        //    // khi click STT tự động tăng
        //    if (dt.Rows.Count == 0)
        //    {
        //        STT = 1;
        //    }
        //    else
        //    {
        //        STT = TextUtils.ToInt(grvData.GetRowCellValue(dt.Rows.Count - 1, "STT")) + 1;
        //    }
        //    DataRow dtrow = dt.NewRow();
        //    dtrow["STT"] = STT;
        //    dt.Rows.Add(dtrow);
        //    grdData.DataSource = dt;
        //}

        private void btnDelet_Click(object sender, EventArgs e)
        {
            if (grdData.DataSource == null) return;


            //string strName = TextUtils.ToString(grvData.GetFocusedRowCellDisplayText(colProductSaleID));
            int[] selectedRows = grvData.GetSelectedRows();

            if (selectedRows.Length <= 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm muốn xóa!", "Thông báo");
                return;
            }

            if (MessageBox.Show(String.Format($"Bạn có chắc muốn xóa {selectedRows.Length} sản phẩm đã chọn không?"), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //grvData.DeleteSelectedRows();
                //if (strID > 0)
                //{
                //    lstIDDelete.Add(strID);
                //}
                
                foreach (int row in selectedRows)
                {
                    int strID = TextUtils.ToInt(grvData.GetRowCellValue(row, colID));
                    if (strID <= 0) continue;
                    if (lstIDDelete.Contains(strID)) continue;
                    lstIDDelete.Add(strID);
                }


                grvData.DeleteSelectedRows();
            }

        }

        private void cbPODetail_EditValueChanged(object sender, EventArgs e)
        {
            grvData.CloseEditor();
            int ID = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colNote));

            var rows = lstPODetail.Where(x => x.ID == ID).FirstOrDefault();
            if (rows != null)
            {
                string spec = TextUtils.ToString(rows.Spec);

                grvData.SetFocusedRowCellValue(colspecifications, spec);


            }
        }

        private void grdFile_Click(object sender, EventArgs e)
        {

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
        void LoadFile(List<RequestInvoiceFileModel> listFiles)
        {
            grdDataFile.DataSource = listFiles;
            grvDataFile.RefreshData();
        }

        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvDataFile.GetFocusedRowCellValue("ID"));
            string fileName = TextUtils.ToString(grvDataFile.GetFocusedRowCellValue("FileName"));

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá file đính kèm [{fileName}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                grvDataFile.DeleteSelectedRows();
                if (id <= 0) return;
                RequestInvoiceFileModel file = SQLHelper<RequestInvoiceFileModel>.FindByID(id);
                listFileDelete.Add(file);
            }
        }

        private void grdData_Click(object sender, EventArgs e)
        {

        }

        private void cboSender_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void chIsUrgency_CheckedChanged(object sender, EventArgs e)
        {
            if (chIsUrgency.Checked)
            {
                dpkDealineUrgency.Enabled = true;
            }
            else
            {
                dpkDealineUrgency.Enabled = false;
                dpkDealineUrgency.EditValue = null;
            }
        }
        //ndnhat update 14/08/2025
        private void grvData_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            LoadPOKHFile();
        }

        private void btnViewFile_Click(object sender, EventArgs e)
        {
            try
            {
                string path = TextUtils.ToString(grvFile.GetFocusedRowCellValue(colServerPath));
                string fileName = TextUtils.ToString(grvFile.GetFocusedRowCellValue(colFileName));

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
                POKHModel pokh = SQLHelper<POKHModel>.FindByID(TextUtils.ToInt(grvData.GetFocusedRowCellValue(colPOKHID)));

                int warehouseID = pokh.WarehouseID;
                if (warehouseID == 2) pathServer = "pokhhcm";
                //string poNumber = TextUtils.ToString(grvMaster.GetFocusedRowCellValue("PONumber"));
                string poNumber = TextUtils.ToString(pokh.PONumber);

                string fileName = TextUtils.ToString(grvFile.GetFocusedRowCellValue(colFileName));
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

        private void grvData_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (grvData.FocusedColumn == colIsStock)
            {
                string billCode = TextUtils.ToString(grvData.GetRowCellValue(grvData.FocusedRowHandle, colBillImportCode));
                bool isStock = TextUtils.ToBoolean(e.Value);
                if (!string.IsNullOrWhiteSpace(billCode) && isStock)
                {
                    e.Valid = false;
                    e.ErrorText = $"Bạn không thể chọn Tồn kho vì đã có Phiếu nhập kho [{billCode}]";
                }
            }
        }

        private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            GridView view = (GridView)sender;
            //bool isSelected = TextUtils.ToBoolean(grvData.GetRowCellValue(e.RowHandle, colIsSelected));
            //if (isSelected)
            //{
            //    e.Appearance.BackColor = Color.LightYellow;
            //    e.HighPriority = true;
            //}

            if (view.FocusedRowHandle == e.RowHandle)
            {
                e.Appearance.BackColor = System.Drawing.Color.LightYellow;
                e.Appearance.ForeColor = System.Drawing.Color.Black;
                //e.HighPriority = true;
            }
        }

        private void chkIsSelected_EditValueChanged(object sender, EventArgs e)
        {
            //grvData.CloseEditor();
            //bool isSelected = TextUtils.ToBoolean(grvData.GetFocusedRowCellValue(colIsSelected));
            //int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            //if (isSelected)
            //{
            //    if (!listSelected.Contains(id)) listSelected.Add(id);
            //}
            //else
            //{
            //    listSelected.Remove(id);
            //}
        }

        bool isRecallCellValueChanged = false;
        private void grvData_CellValueChanged_1(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (isRecallCellValueChanged == true) return;
            try
            {
                if (e.Column.FieldName != colIsStock.FieldName && e.Column.FieldName != colInvoiceNumber.FieldName && e.Column.FieldName != colInvoiceDate.FieldName) return;
                using (WaitDialogForm fWait = new WaitDialogForm())
                {
                    isRecallCellValueChanged = true;
                    grvData.CloseEditor();

                    if (e.Column.FieldName == colIsStock.FieldName || e.Column.FieldName == colInvoiceNumber.FieldName || e.Column.FieldName == colInvoiceDate.FieldName)
                    {
                        if (e.Value == null) return;
                        //int[] selectedRows = grvMaster.GetSelectedRows();
                        //List<int> selectedIds = listSelected; // NTA B - update 15/09/25
                        int[] selectedRows = grvData.GetSelectedRows();
                        if (selectedRows.Length > 0)
                        {
                            foreach (int row in selectedRows)
                            {
                                //int row = grvData.LocateByValue("ID", id);
                                grvData.SetRowCellValue(row, grvData.Columns[e.Column.FieldName], e.Value);

                            }
                        }
                        else
                        {
                            grvData.SetRowCellValue(grvData.FocusedRowHandle, grvData.Columns[e.Column.FieldName], e.Value);
                        }
                        //grvMaster.SetRowCellValue(e.RowHandle, colIsUpdated, 1);
                    }

                }
            }
            finally
            {
                isRecallCellValueChanged = false;
            }
        }
        //end ndnhat update 14/08/2025
    }
}