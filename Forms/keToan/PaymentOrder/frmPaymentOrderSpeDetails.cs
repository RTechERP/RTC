using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BaseBusiness.DTO;

namespace BMS
{
    public partial class frmPaymentOrderSpeDetails : _Forms
    {
        public PaymentOrderModel paymentOrder = new PaymentOrderModel();
        public bool isCopy = false;
        public DataTable dt = new DataTable();
        public DataTable dtCustomer = new DataTable();
        List<FileInfo> listFileUpload = new List<FileInfo>();
        List<PaymentOrderFileModel> listFiles = new List<PaymentOrderFileModel>();
        //List<PaymentOrderCustomerModel> listCus = new List<PaymentOrderCustomerModel>();

        List<int> isDeleteDetails = new List<int>();
        List<PaymentOrderFileModel> listFileDelete = new List<PaymentOrderFileModel>();
        List<PaymentOrderDetailDTO> listUserTeamSale = new List<PaymentOrderDetailDTO>();
        public frmPaymentOrderSpeDetails()
        {
            InitializeComponent();
        }
        private void frmPaymentOrderSpeDetails_Load(object sender, EventArgs e)
        {
            txtEmployee.Text = Global.AppFullName;
            txtDepartment.Text = Global.DepartmentName;
            txtReceiverInfo.Text = txtEmployee.Text;
            label19.Hide();
            loadTypeOrder();
            loadPaymentOrderType();
            loadApproved();
            loadCustomer();
            loadCurrency();
            cboCurrency.EditValue = 1;
            loadPONCC();
            loadPaymentMethods();
            LoadDataDetails();
        }
        public class TypeDocument
        {
            public int ID { get; set; }
            public string TypeDocumentName { get; set; }
            public bool IsCheck { get; set; }
        }
        private void loadPONCC()
        {
            List<TypeDocument> data = new List<TypeDocument>();
            data.Add(new TypeDocument() { ID = 1, TypeDocumentName = "PO KH" });
            data.Add(new TypeDocument() { ID = 2, TypeDocumentName = "Hóa đơn" });
            cboTypeDocument.Properties.DataSource = data;
            cboTypeDocument.Properties.DisplayMember = "TypeDocumentName";
            cboTypeDocument.Properties.ValueMember = "ID";
        }
        private void loadPaymentMethods()
        {
            List<object> data = new List<object>();
            data.Add(new { ID = 0, PaymentMethods = "Tiền mặt" });
            data.Add(new { ID = 1, PaymentMethods = "Chuyển khoản" });
            cboPaymentMethods.DataSource = data;
            cboPaymentMethods.ValueMember = "ID";
            cboPaymentMethods.DisplayMember = "PaymentMethods";
        }
        public void LoadDataDetails()
        {
            dt = SQLHelper<PaymentOrderDetailModel>.LoadDataFromSP("spGetPaymentOrderDetail", new string[] { "@PaymentOrderID" }, new object[] { paymentOrder.ID });
            grdDetails.DataSource = dt;
            listFiles = SQLHelper<PaymentOrderFileModel>.FindByAttribute("PaymentOrderID", paymentOrder.ID);
            grdFileData.DataSource = listFiles;

            dtCustomer = SQLHelper<PaymentOrderCustomerModel>.GetTableData($"SELECT * FROM dbo.PaymentOrderCustomer WHERE PaymentOrderID = {paymentOrder.ID}");
            grdCustomer.DataSource = dtCustomer;

            if (paymentOrder.ID == 0) return;
            PaymentOrderLogModel paymentOrderLog = SQLHelper<PaymentOrderLogModel>.FindByAttribute("PaymentOrderID", paymentOrder.ID).Where(x => x.Step == 2).FirstOrDefault();
            cboApproved.EditValue = paymentOrderLog.EmployeeID;
            //cboOrderType.EditValue = paymentOrder.PaymentOrderTypeID;  // lee min khooi update 09/10/2024
            dtpDateOrder.Value = TextUtils.ToDate3(paymentOrder.DateOrder);
            dtpDatePayment.EditValue = paymentOrder.DatePayment;
            txtNumberDocument.Text = paymentOrder.NumberDocument;
            IsDeadline.Checked = TextUtils.ToBoolean(paymentOrder.IsUrgent);
            if (IsDeadline.Checked) { dpkDeadline.EditValue = paymentOrder.DeadlinePayment; label19.Show(); }
            txtReasonOrder.Text = TextUtils.ToString(paymentOrder.ReasonOrder);
            txtReceiverInfo.Text = TextUtils.ToString(paymentOrder.ReceiverInfo);
            txtTotalMoneyText.Text = TextUtils.ToString(paymentOrder.TotalMoneyText);


            //==================================================== lee min khooi update 01/10/2024 ==============================================
            var listDocument = SQLHelper<PaymentOrderTypeDocumentModel>.FindByAttribute("PaymentOrderID", paymentOrder.ID).Select(p => p.TypeDocumentID).ToList();
            cboTypeDocument.SetEditValue(string.Join(",", listDocument));


            // lee min khooi update 09/10/2024
            var listOrderType = SQLHelper<PaymentOrderOrderTypeModel>.FindByAttribute("PaymentOrderID", paymentOrder.ID).Select(p => p.PaymentOrderTypeID).ToList();
            cboOrderType.SetEditValue(string.Join(",", listOrderType));
            //var listTypeOrder = SQLHelper<PaymentOrderTypeOrderModel>.FindByAttribute("PaymentOrderID", paymentOrder.ID).Select(p => p.TypeOrderID).ToList();
            //cboTypeOrder.SetEditValue(string.Join(",", listTypeOrder));
            //==================================================== end update 01/10/2024 ==============================================

        }
        // =========================================================== Lee Min Khooi Update 18/06/2024 ===========================================================

        public void loadCustomer()
        {
            dtCustomer = SQLHelper<PaymentOrderCustomerModel>.GetTableData($"SELECT * FROM dbo.PaymentOrderCustomer WHERE PaymentOrderID = {paymentOrder.ID}");
            grdCustomer.DataSource = dtCustomer;
            Expression ex1 = new Expression("IsDeleted", 0);
            cboCustomer.DataSource = SQLHelper<CustomerModel>.FindByExpression(ex1);
            cboCustomer.ValueMember = "ID";
            cboCustomer.DisplayMember = "CustomerName";
        }
        // =========================================================== End ===========================================================

        public class TypeOrder
        {
            public int ID { get; set; }
            public string TypeName { get; set; }
            public bool IsCheck { get; set; }
        }
        public void loadTypeOrder()
        {
            List<TypeOrder> list = new List<TypeOrder>() {
                new TypeOrder() {ID = 1,TypeName = "Đề nghị tạm ứng"},
                new TypeOrder() {ID = 2,TypeName = "Đề nghị thanh toán"},
            };
            cboTypeOrder.Properties.DataSource = list;
            cboTypeOrder.Properties.DisplayMember = "TypeName";
            cboTypeOrder.Properties.ValueMember = "ID";
        }
        public void loadPaymentOrderType()
        {
            //==================================================== lee min khooi update 05/09/2024 ==============================================
            Expression exp = new Expression("IsDelete", 0);
            Expression exp2 = new Expression("IsSpecialOrder", 1);
            List<PaymentOrderTypeModel> list = SQLHelper<PaymentOrderTypeModel>.FindByExpression(exp.And(exp2));
            cboOrderType.Properties.DataSource = list;
            cboOrderType.Properties.ValueMember = "ID";
            cboOrderType.Properties.DisplayMember = "TypeName";
        }
        private void cboTypeOrder_SelectedIndexChanged(object sender, EventArgs e)
        {


        }
        public void loadCurrency()
        {
            List<CurrencyModel> list = SQLHelper<CurrencyModel>.FindAll();
            cboCurrency.Properties.DataSource = list;
            cboCurrency.Properties.ValueMember = "ID";
            cboCurrency.Properties.DisplayMember = "Code";
        }
        void LoadFile(List<PaymentOrderFileModel> listFiles)
        {
            grdFileData.DataSource = listFiles;
            grvFileData.RefreshData();
        }
        public void loadApproved()
        {
            var exp = new Expression("Type", 3);
            List<EmployeeApproveModel> list = SQLHelper<EmployeeApproveModel>.FindByExpression(exp);
            cboApproved.Properties.DataSource = list;
            cboApproved.Properties.ValueMember = "EmployeeID";
            cboApproved.Properties.DisplayMember = "FullName";
        }

        private void grvDetails_GetCustomSummaryValue(object sender, DevExpress.XtraTreeList.GetCustomSummaryValueEventArgs e)
        {
            /* if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
             {
                 GridColumnSummaryItem summaryItem = e.Item as GridColumnSummaryItem;
                 if (summaryItem != null && summaryItem.FieldName == "TotalMoney")
                 {
                     decimal total = 0;
                     GridView view = sender as GridView;
                     for (int i = 0; i < view.RowCount; i++)
                     {
                         total += Convert.ToDecimal(view.GetRowCellValue(i, summaryItem.FieldName));
                     }
                     e.TotalValue = total;
                 }
             }*/
        }

        private void grvDetails_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvDetails.CalcHitInfo(e.Location);

                if (info.Column != null && info.Column == colAdd && info.HitTest == GridHitTest.Column)
                {
                    grvDetails.FocusedRowHandle = -1;
                    dt.AcceptChanges();
                    DataRow dtrow = dt.NewRow();

                    int stt = dt.Rows.Count;
                    int idMapping = TextUtils.ToInt(grvDetails.GetRowCellValue(stt - 1, colIdMapping));
                    dtrow["STT"] = stt + 1;
                    dtrow["IdMapping"] = idMapping + 1;
                    dt.Rows.Add(dtrow);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in dialog.FileNames)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    PaymentOrderFileModel fileRequest = new PaymentOrderFileModel()
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


        private void btnDeleteRow_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (grdDetails.DataSource == null) return;
            int ID = TextUtils.ToInt(grvDetails.GetFocusedRowCellValue(colID));
            int STT = TextUtils.ToInt(grvDetails.GetFocusedRowCellDisplayText(colSTT));
            if (MessageBox.Show(String.Format("Bạn có chắc chắn muốn xóa nội dung thứ [{0}] của thông tin đề nghị không?", STT), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvDetails.DeleteSelectedRows();
                for (int i = 0; i < grvDetails.RowCount; i++)
                {
                    grvDetails.SetRowCellValue(i, colSTT, i + 1);
                }

                if (ID > 0) isDeleteDetails.Add(ID);
            }
        }
        private void totalMoneyToText()
        {
            grvDetails.FocusedRowHandle = -1;
            decimal unitPrice = TextUtils.ToDecimal(grvDetails.Columns["TotalMoney"].SummaryItem.SummaryValue);
            int qty = TextUtils.ToInt(cboCurrency.EditValue);
            decimal totalMoney = unitPrice * qty;
            try
            {
                if (TextUtils.ToInt(cboCurrency.EditValue) > 0)
                {
                    txtTotalMoneyText.Text = NumberMoneyToText.ConvertNumberToTextVietNamese(unitPrice, cboCurrency.Text.ToUpper());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }
        private void grvDetails_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void cboCurrency_EditValueChanged(object sender, EventArgs e)
        {
            totalMoneyToText();
        }

        private void grvDetails_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            totalMoneyToText();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvFileData.GetFocusedRowCellValue("ID"));
            string fileName = TextUtils.ToString(grvFileData.GetFocusedRowCellValue("FileName"));

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá file đính kèm [{fileName}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                grvFileData.DeleteSelectedRows();
                if (id <= 0) return;
                PaymentOrderFileModel file = SQLHelper<PaymentOrderFileModel>.FindByID(id);
                listFileDelete.Add(file);
            }
        }

        private bool CheckValidate()
        {
            grvCustomer.FocusedRowHandle = -1;
            grvDetails.FocusedRowHandle = -1;
            bool isCheckQT = false;

            // lee min khooi update 09/10/2024
            //if (string.IsNullOrWhiteSpace(cboTypeOrder.EditValue.ToString()))
            //{
            //    MessageBox.Show("Vui lòng chọn Loại đề nghị", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}

            if (string.IsNullOrEmpty(TextUtils.ToString(dtpDateOrder.Value)))
            {
                MessageBox.Show("Vui lòng chọn Ngày đề nghị", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (isCheckQT && string.IsNullOrEmpty(TextUtils.ToString(dtpDatePayment.EditValue)))
            {
                MessageBox.Show("Vui lòng chọn Thời gian thanh quyết toán", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(TextUtils.ToString(cboApproved.EditValue)))
            {
                MessageBox.Show("Vui lòng chọn TBP duyệt", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(cboOrderType.EditValue.ToString()))  // lee min khooi update 09/10/2024
            {
                MessageBox.Show("Vui lòng chọn Loại nội dung đề nghị", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (IsDeadline.Checked && string.IsNullOrEmpty(TextUtils.ToString(dpkDeadline.EditValue)))
            {
                MessageBox.Show("Vui lòng chọn Deadline", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(txtReasonOrder.Text))
            {
                MessageBox.Show("Vui lòng nhập Lý do", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(txtReceiverInfo.Text))
            {
                MessageBox.Show("Vui lòng nhập Thông tin người nhận tiền", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            for (int i = 0; i < grvDetails.RowCount; i++)
            {
                if (string.IsNullOrEmpty(TextUtils.ToString(grvDetails.GetRowCellValue(i, colContentPayment))))
                {
                    MessageBox.Show("Xin vui lòng nhập Đối tượng COM!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }

                if (string.IsNullOrEmpty(TextUtils.ToString(grvDetails.GetRowCellValue(i, colTotalMoney))))
                {
                    MessageBox.Show("Xin vui lòng nhập Số tiền!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }

                if (string.IsNullOrEmpty(TextUtils.ToString(grvDetails.GetRowCellValue(i, colPaymentMethod))))
                {
                    MessageBox.Show("Xin vui lòng nhập Hình thức thanh toán!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }

                if (string.IsNullOrEmpty(TextUtils.ToString(grvDetails.GetRowCellValue(i, colPaymentInfor))))
                {
                    MessageBox.Show("Xin vui lòng nhập Thông tin thanh toán!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }

            return true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (IsDeadline.Checked == true) label19.Show();
            else label19.Hide();
        }
        private void Reset()
        {
            paymentOrder = new PaymentOrderModel();
            dt = new DataTable();
            listFileUpload = new List<FileInfo>();
            listFiles = new List<PaymentOrderFileModel>();
            isDeleteDetails = new List<int>();
            listFileDelete = new List<PaymentOrderFileModel>();

            loadTypeOrder();
            loadPONCC();
            dtpDateOrder.Value = DateTime.Now;
            txtEmployee.Clear();
            txtDepartment.Clear();
            dtpDatePayment.EditValue = null;
            cboApproved.EditValue = null;
            cboOrderType.Reset();  // lee min khooi update 09/10/2024
            txtNumberDocument.Clear();
            dpkDeadline.EditValue = null;
            txtReasonOrder.Clear();
            txtReceiverInfo.Clear();

            grdDetails.DataSource = dt;
            grvDetails.RefreshData();
            grdFileData.DataSource = listFiles;
            grvFileData.RefreshData();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            };
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                Reset();
                MessageBox.Show("Lưu thành công!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            };
        }

        bool SaveData()
        {
            if (!CheckValidate()) return false;
            //paymentOrder.TypeOrder = TextUtils.ToInt(cboTypeOrder.SelectedValue);
            //paymentOrder.PONCCID = TextUtils.ToInt(cboPONCC.EditValue);
            // paymentOrder.PaymentOrderTypeID = TextUtils.ToInt(cboOrderType.EditValue);
            paymentOrder.DateOrder = dtpDateOrder.Value;
            paymentOrder.EmployeeID = Global.EmployeeID;
            paymentOrder.NumberDocument = TextUtils.ToString(txtNumberDocument.Text);
            paymentOrder.IsUrgent = IsDeadline.Checked;
            if (IsDeadline.Checked) paymentOrder.DeadlinePayment = Convert.ToDateTime(dpkDeadline.EditValue);
            paymentOrder.ReasonOrder = TextUtils.ToString(txtReasonOrder.Text);
            paymentOrder.ReceiverInfo = TextUtils.ToString(txtReceiverInfo.Text);
            paymentOrder.IsSpecialOrder = true;
            paymentOrder.TotalMoneyText = TextUtils.ToString(txtTotalMoneyText.Text);
            paymentOrder.TotalMoney = TextUtils.ToDecimal(grvDetails.Columns["TotalMoney"].SummaryItem.SummaryValue);

            if (isCopy) paymentOrder.ID = 0;
            if (paymentOrder.ID > 0)
            {
                PaymentOrderBO.Instance.Update(paymentOrder);
            }
            else
            {
                paymentOrder.Code = CreateCode(paymentOrder);
                paymentOrder.ID = (int)PaymentOrderBO.Instance.Insert(paymentOrder);
            }
            CreatePOSCus(paymentOrder.ID);




            for (int i = 0; i < grvDetails.RowCount; i++)
            {
                int id = TextUtils.ToInt(grvDetails.GetRowCellValue(i, colDetailsID));
                PaymentOrderDetailModel detail = new PaymentOrderDetailModel();
                if (id > 0)
                {
                    detail = SQLHelper<PaymentOrderDetailModel>.FindByID(id);
                }

                detail.PaymentOrderID = paymentOrder.ID;
                detail.STT = TextUtils.ToString(grvDetails.GetRowCellValue(i, colSTT));
                detail.ContentPayment = TextUtils.ToString(grvDetails.GetRowCellValue(i, colContentPayment));
                detail.TotalMoney = TextUtils.ToDecimal(grvDetails.GetRowCellValue(i, colTotalMoney));
                detail.PaymentMethods = TextUtils.ToInt(grvDetails.GetRowCellValue(i, colPaymentMethod));
                detail.PaymentInfor = TextUtils.ToString(grvDetails.GetRowCellValue(i, colPaymentInfor));
                detail.EmployeeID = TextUtils.ToInt(grvDetails.GetRowCellValue(i, colUserTeamSale));
                detail.Note = TextUtils.ToString(grvDetails.GetRowCellValue(i, colNote));
                if (isCopy) detail.ID = 0;
                if (detail.ID > 0)
                {
                    PaymentOrderDetailBO.Instance.Update(detail);
                }
                else
                {
                    detail.ID = TextUtils.ToInt(PaymentOrderDetailBO.Instance.Insert(detail));
                }

                int idMapping = TextUtils.ToInt(grvDetails.GetRowCellValue(i, colIdMapping));
                if (listUserTeamSale.Any(p => p.ID == idMapping))
                {
                    List<int> lstUSID = listUserTeamSale.Where(p => p.ID == idMapping).FirstOrDefault().ListUserTeamSale.Select(p => p.ID).ToList();
                    CreatePOSDUsSale(detail.ID, lstUSID);
                }
            }



            //============================ lee min khooi update 05/09/2024 =====================
            // Xoas phaafn tuwr cux
            SQLHelper<PaymentOrderTypeDocumentModel>.DeleteByAttribute("PaymentOrderID", paymentOrder.ID);
            string[] typeDocumentIDs = cboTypeDocument.EditValue.ToString().Split(',');
            foreach (string typeDocumentID in typeDocumentIDs)
            {
                int typeDocId = TextUtils.ToInt(typeDocumentID);
                if (typeDocId <= 0) continue;
                PaymentOrderTypeDocumentModel newModel = new PaymentOrderTypeDocumentModel()
                {
                    PaymentOrderID = paymentOrder.ID,
                    TypeDocumentID = typeDocId
                };
                SQLHelper<PaymentOrderTypeDocumentModel>.Insert(newModel);

            }

            // ========================= leee min khooi update 09/10/2024 ============================
            SQLHelper<PaymentOrderOrderTypeModel>.DeleteByAttribute("PaymentOrderID", paymentOrder.ID);
            string[] orderTypeIDs = cboOrderType.EditValue.ToString().Split(',');
            foreach (string orderTypeID in orderTypeIDs)
            {
                int otID = TextUtils.ToInt(orderTypeID);
                if (otID <= 0) continue;
                PaymentOrderOrderTypeModel newModel = new PaymentOrderOrderTypeModel()
                {
                    PaymentOrderID = paymentOrder.ID,
                    PaymentOrderTypeID = otID
                };
                SQLHelper<PaymentOrderOrderTypeModel>.Insert(newModel);

            }

            // lee min khooi update 09/10/2024
            //SQLHelper<PaymentOrderTypeOrderModel>.DeleteByAttribute("PaymentOrderID", paymentOrder.ID);
            //string[] typeOrderIDs = cboTypeOrder.EditValue.ToString().Split(',');
            //foreach (string typeOrderID in typeOrderIDs)
            //{
            //    int typeId = TextUtils.ToInt(typeOrderID);
            //    if (typeId <= 0) continue;

            //    PaymentOrderTypeOrderModel newModel = new PaymentOrderTypeOrderModel()
            //    {
            //        PaymentOrderID = paymentOrder.ID,
            //        TypeOrderID = typeId
            //    };
            //    if (typeId == 1)
            //    {
            //        paymentOrder.DatePayment = Convert.ToDateTime(dtpDatePayment.EditValue);
            //        PaymentOrderBO.Instance.Update(paymentOrder);
            //    }
            //    SQLHelper<PaymentOrderTypeOrderModel>.Insert(newModel);
            //}
            //============================ end update 01/10/2024 =====================


            if (isDeleteDetails.Count > 0)
            {
                foreach (int item in isDeleteDetails)
                {
                    PaymentOrderDetailBO.Instance.Delete(item);

                }
            }

            CreateApprove(TextUtils.ToInt(cboApproved.EditValue), paymentOrder);
            UploadFile(paymentOrder.ID);
            RemoveFile();
            return true;
        }
        public async void RemoveFile()
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
                    SQLHelper<PaymentOrderFileModel>.Delete(item);
                }
            }
            listFileDelete = new List<PaymentOrderFileModel>();
        }
        public async void UploadFile(int paymentOrderID)
        {
            try
            {
                ConfigSystemModel config = SQLHelper<ConfigSystemModel>.FindByAttribute("KeyName", "PathPaymentOrder").FirstOrDefault();
                if (config == null || string.IsNullOrEmpty(config.KeyValue))
                {
                    MessageBox.Show("Vui lòng chọn đường dẫn lưu trên server!", "Thông báo");
                    return;
                }

                PaymentOrderModel order = SQLHelper<PaymentOrderModel>.FindByID(paymentOrderID);
                if (order == null || order.ID <= 0) return;
                if (order.EmployeeID != Global.EmployeeID) return;

                string pathServer = config.KeyValue.Trim();
                string pathPattern = $@"NĂM {order.DateOrder.Value.Year}\ĐỀ NGHỊ THANH TOÁN\THÁNG {order.DateOrder.Value.ToString("MM.yyyy")}\{order.DateOrder.Value.ToString("dd.MM.yyyy")}\{order.Code}";
                string pathUpload = Path.Combine(pathServer, pathPattern);

                var client = new HttpClient();

                List<PaymentOrderFileModel> listFiles = new List<PaymentOrderFileModel>();
                foreach (var file in listFileUpload)
                {
                    PaymentOrderFileModel fileOrder = new PaymentOrderFileModel();
                    fileOrder.PaymentOrderID = order.ID;
                    fileOrder.FileName = file.Name;
                    fileOrder.OriginPath = file.DirectoryName;
                    fileOrder.ServerPath = pathUpload;

                    if (file.Length < 0) continue;

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
                        SQLHelper<PaymentOrderFileModel>.Insert(fileOrder);
                    }
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }
        public void CreateApprove(int approvedTBPID, PaymentOrderModel paymentOrder)
        {
            string _typeHanhChinh = "2,3,4,5,6,7";
            string _typeHR = "1,4,5";
            string[] stepNames = new string[] { "Đề nghị thanh toán", "Trưởng bộ phận xác nhận", "Nhân sự xác nhận", "TBP Nhân sự xác nhận", "KT Check hồ sơ", "Kế toán trưởng xác nhận", "Ban giám đốc xác nhận", "Nhận chứng từ", "Kế toán thanh toán" };

            try
            {
                var list = SQLHelper<PaymentOrderLogModel>.FindAll().Where(x => x.PaymentOrderID == paymentOrder.ID);
                foreach (var item in list)
                {
                    SQLHelper<PaymentOrderLogModel>.Delete(item);
                }

                List<PaymentOrderLogModel> listLog = new List<PaymentOrderLogModel>();
                PaymentOrderTypeModel type = SQLHelper<PaymentOrderTypeModel>.FindByID((int)(SQLHelper<PaymentOrderModel>.FindByID(paymentOrder.ID).PaymentOrderTypeID));
                var departments = SQLHelper<DepartmentModel>.FindAll();

                int headOfHR = departments.FirstOrDefault(x => x.Code == "HR") == null ? 0 : TextUtils.ToInt(departments.FirstOrDefault(x => x.Code == "HR").HeadofDepartment);
                int headOfKT = departments.FirstOrDefault(x => x.Code == "KT") == null ? 0 : TextUtils.ToInt(departments.FirstOrDefault(x => x.Code == "KT").HeadofDepartment);

                var typeHanhChinh = _typeHanhChinh.Split(',');
                var typeHR = _typeHR.Split(',');

                int employeeHR = typeHR.Contains(type.ID.ToString()) ? 5 : 0;
                int employeeHanhChinh = typeHanhChinh.Contains(type.ID.ToString()) ? 156 : 0;
                int employeeStep3 = employeeHR > 0 ? employeeHR : employeeHanhChinh;


                if (type.IsSpecialOrder == true)//Loại thanh toán đặc biệt
                {
                    listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 1, StepName = "Đề nghị thanh toán", DateApproved = DateTime.Now, IsApproved = 1, EmployeeID = Global.EmployeeID, EmployeeApproveActualID = Global.EmployeeID, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                    listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 2, StepName = "Trưởng bộ phận xác nhận", DateApproved = null, IsApproved = 0, EmployeeID = approvedTBPID, EmployeeApproveActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                    listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 3, StepName = "Kế toán trưởng duyệt", DateApproved = null, IsApproved = 0, EmployeeID = headOfKT, EmployeeApproveActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                    listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 4, StepName = "Ban giám đốc duyệ", DateApproved = null, IsApproved = 0, EmployeeID = 1, EmployeeApproveActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                    listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 5, StepName = "Kế toán thanh toán", DateApproved = null, IsApproved = 0, EmployeeID = 32, EmployeeApproveActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                    listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 6, StepName = "Khách hàng đã nhận", DateApproved = null, IsApproved = 0, EmployeeID = Global.EmployeeID, EmployeeApproveActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                }
                else if (type.IsIgnoreHR == true)
                {
                    listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 1, StepName = "Đề nghị thanh toán", DateApproved = DateTime.Now, IsApproved = 1, EmployeeID = Global.EmployeeID, EmployeeApproveActualID = Global.EmployeeID, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                    listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 2, StepName = "Trưởng bộ phận xác nhận", DateApproved = null, IsApproved = 0, EmployeeID = approvedTBPID, EmployeeApproveActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                    listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 3, StepName = "Check hồ sơ", DateApproved = null, IsApproved = 0, EmployeeID = 0, EmployeeApproveActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                    listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 4, StepName = "Kế toán trưởng xác nhận", DateApproved = null, IsApproved = 0, EmployeeID = headOfKT, EmployeeApproveActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                    listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 5, StepName = "Ban giám đốc xác nhận", DateApproved = null, IsApproved = 0, EmployeeID = 1, EmployeeApproveActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                    listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 6, StepName = "Nhận chứng từ", DateApproved = null, IsApproved = 0, EmployeeID = 0, EmployeeApproveActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                    listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 7, StepName = "Kế toán thanh toán", DateApproved = null, IsApproved = 0, EmployeeID = 0, EmployeeApproveActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                }
                else
                {
                    DateTime date = new DateTime(2024, 03, 03).Date;
                    if (DateTime.Now.Date <= date)
                    {
                        listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 1, StepName = "Đề nghị thanh toán", DateApproved = DateTime.Now, IsApproved = 1, EmployeeID = Global.EmployeeID, EmployeeApproveActualID = Global.EmployeeID, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                        listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 2, StepName = "Trưởng bộ phận xác nhận", DateApproved = null, IsApproved = 0, EmployeeID = approvedTBPID, EmployeeApproveActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                        //listLog.Add(new PaymentOrderLog() { PaymentOrderID = paymentOrder.Id, Step = 3, StepName = "Nhân sự xác nhận", DateApproved = null, IsApproved = 0, EmployeeID = employeeStep3, EmployeeApproveActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                        listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 3, StepName = "TBP Nhân sự xác nhận", DateApproved = null, IsApproved = 0, EmployeeID = headOfHR, EmployeeApproveActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                        listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 4, StepName = "Check hồ sơ", DateApproved = null, IsApproved = 0, EmployeeID = 0, EmployeeApproveActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                        listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 5, StepName = "Kế toán trưởng xác nhận", DateApproved = null, IsApproved = 0, EmployeeID = headOfKT, EmployeeApproveActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                        listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 6, StepName = "Ban giám đốc xác nhận", DateApproved = null, IsApproved = 0, EmployeeID = 1, EmployeeApproveActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                        listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 7, StepName = "Nhận chứng từ", DateApproved = null, IsApproved = 0, EmployeeID = 0, EmployeeApproveActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                        listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 8, StepName = "Kế toán thanh toán", DateApproved = null, IsApproved = 0, EmployeeID = 0, EmployeeApproveActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                    }
                    else
                    {
                        listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 1, StepName = "Đề nghị thanh toán", DateApproved = DateTime.Now, IsApproved = 1, EmployeeID = Global.EmployeeID, EmployeeApproveActualID = Global.EmployeeID, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                        listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 2, StepName = "Trưởng bộ phận xác nhận", DateApproved = null, IsApproved = 0, EmployeeID = approvedTBPID, EmployeeApproveActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                        listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 3, StepName = "Nhân sự xác nhận", DateApproved = null, IsApproved = 0, EmployeeID = employeeStep3, EmployeeApproveActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                        listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 4, StepName = "TBP Nhân sự xác nhận", DateApproved = null, IsApproved = 0, EmployeeID = headOfHR, EmployeeApproveActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                        listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 5, StepName = "Check hồ sơ", DateApproved = null, IsApproved = 0, EmployeeID = 0, EmployeeApproveActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                        listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 6, StepName = "Kế toán trưởng xác nhận", DateApproved = null, IsApproved = 0, EmployeeID = headOfKT, EmployeeApproveActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                        listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 7, StepName = "Ban giám đốc xác nhận", DateApproved = null, IsApproved = 0, EmployeeID = 1, EmployeeApproveActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                        listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 8, StepName = "Nhận chứng từ", DateApproved = null, IsApproved = 0, EmployeeID = 0, EmployeeApproveActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                        listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 9, StepName = "Kế toán thanh toán", DateApproved = null, IsApproved = 0, EmployeeID = 0, EmployeeApproveActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                    }
                }

                //Check nếu là TBP là người tạo đề nghị
                var tbpApproved = SQLHelper<EmployeeApproveModel>.FindAll().Where(x => x.EmployeeID == Global.EmployeeID && x.Type == 3).ToList();
                if (tbpApproved.Count > 0)
                {
                    var logTBP = listLog.FirstOrDefault(x => x.Step == 2);
                    if (logTBP != null)
                    {
                        logTBP.IsApproved = 1;
                        logTBP.EmployeeID = Global.EmployeeID;
                        logTBP.EmployeeApproveActualID = Global.EmployeeID;
                        logTBP.DateApproved = DateTime.Now;
                    }
                }
                foreach (PaymentOrderLogModel item in listLog)
                {
                    SQLHelper<PaymentOrderLogModel>.Insert(item);
                }



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //===================== Lee Min Khooi Update  20/06/2024===============================

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo info = grvCustomer.CalcHitInfo(e.Location);

                if (info.Column != null && info.Column == colCusAdd && info.HitTest == GridHitTest.Column)
                {
                    dtCustomer = (DataTable) grdCustomer.DataSource;
                    grvCustomer.FocusedRowHandle = -1;
                    dtCustomer.AcceptChanges();
                    DataRow dtrow = dtCustomer.NewRow();
                    dtCustomer.Rows.Add(dtrow);
                    grdCustomer.DataSource = dtCustomer;
                }
            }
        }

        private void btnCusDelete_Click(object sender, EventArgs e)
        {
            if (grdCustomer.DataSource == null) return;
            //int ID = TextUtils.ToInt(grvCustomer.GetFocusedRowCellValue(colCustomer));
            if (MessageBox.Show(String.Format("Bạn có chắc chắn muốn xóa khách hàng thứ [{0}] không?", grvCustomer.FocusedRowHandle + 1), TextUtils.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                grvCustomer.DeleteSelectedRows();
            }
        }
        private void btnShowUserTeamSale_Click(object sender, EventArgs e)
        {
            /* int rowHandle = grvDetails.FocusedRowHandle;

             int idMapping = TextUtils.ToInt(grvDetails.GetRowCellValue(rowHandle, colIdMapping));
             int detailID = TextUtils.ToInt(grvDetails.GetRowCellValue(rowHandle, colDetailsID));
             frmSpeDetailUserTeamSale frm = new frmSpeDetailUserTeamSale();

             PaymentOrderDetailDTO dto = listUserTeamSale.FirstOrDefault(p => p.ID == idMapping);
             if (dto == null) frm.ListTeamSale = SQLHelper<UserTeamSaleModel>.ProcedureToList("spGetAllUserTeamSaleByPaymentOrderDetailID", new string[] { "@PaymentOrderDetailID" }, new object[] { detailID });
             else frm.ListTeamSale = dto.ListUserTeamSale;
             if (frm.ShowDialog() == DialogResult.OK)
             {
                 PaymentOrderDetailDTO tempData = new PaymentOrderDetailDTO();
                 tempData.ID = idMapping;
                 tempData.ListUserTeamSale = new List<UserTeamSaleModel>();
                 tempData.ListUserTeamSale = frm.ListTeamSale;
                 listUserTeamSale.Add(tempData);
                 string userTeamSale = string.Join(@"\n", tempData.ListUserTeamSale.Select(p => p.Name));
                 grvDetails.SetRowCellValue(rowHandle, colUserTeamSale, userTeamSale);
             }*/
        }

        private void CreatePOSCus(int posID)
        {
            if (grvCustomer.RowCount <= 0) return;
            //PaymentOrderCustomerBO.Instance.DeleteByAttribute("PaymentOrderID", posID);
            SQLHelper<PaymentOrderCustomerModel>.DeleteByAttribute("PaymentOrderID", posID);
            for (int i = 0; i < grvCustomer.RowCount; i++)
            {
                PaymentOrderCustomerModel model = new PaymentOrderCustomerModel();
                model.CustomerID = TextUtils.ToInt(grvCustomer.GetRowCellValue(i, colCustomer));
                if (model.CustomerID <= 0) continue;
                model.PaymentOrderID = posID;
                //PaymentOrderCustomerBO.Instance.Insert(model);
                SQLHelper<PaymentOrderCustomerModel>.Insert(model);
            }
        }

        private void CreatePOSDUsSale(int posdID, List<int> listUserTeamSaleID)
        {
            if (listUserTeamSaleID.Count <= 0) return;
            //PaymentOrderDetailUserTeamSaleBO.Instance.DeleteByAttribute("PaymentOrderDetailID", posdID);
            SQLHelper<PaymentOrderDetailUserTeamSaleModel>.DeleteByAttribute("PaymentOrderDetailID", posdID);
            for (int i = 0; i < listUserTeamSaleID.Count; i++)
            {
                PaymentOrderDetailUserTeamSaleModel model = new PaymentOrderDetailUserTeamSaleModel();
                model.UserTeamSaleID = listUserTeamSaleID[i];
                model.PaymentOrderDetailID = posdID;
                //PaymentOrderDetailUserTeamSaleBO.Instance.Insert(model);
                SQLHelper<PaymentOrderDetailUserTeamSaleModel>.Insert(model);
            }
        }

        private void grvDetails_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column == colUserTeamSale)
            {
                int rowHandle = e.RowHandle; //grvDetails.FocusedRowHandle;

                int idMapping = TextUtils.ToInt(grvDetails.GetRowCellValue(rowHandle, colIdMapping));
                int detailID = TextUtils.ToInt(grvDetails.GetRowCellValue(rowHandle, colDetailsID));
                frmSpeDetailUserTeamSale frm = new frmSpeDetailUserTeamSale();

                PaymentOrderDetailDTO dto = listUserTeamSale.FirstOrDefault(p => p.ID == idMapping);
                if (dto == null) frm.ListTeamSale = SQLHelper<UserTeamSaleModel>.ProcedureToList("spGetAllUserTeamSaleByPaymentOrderDetailID", new string[] { "@PaymentOrderDetailID" }, new object[] { detailID });
                else frm.ListTeamSale = dto.ListUserTeamSale;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    PaymentOrderDetailDTO tempData = new PaymentOrderDetailDTO();
                    tempData.ID = idMapping;
                    tempData.ListUserTeamSale = new List<UserTeamSaleModel>();
                    tempData.ListUserTeamSale = frm.ListTeamSale;

                    string userTeamSale = string.Join("\n", tempData.ListUserTeamSale.Select(p => p.Name));
                    grvDetails.SetRowCellValue(rowHandle, colUserTeamSale, userTeamSale);
                    listUserTeamSale.Remove(listUserTeamSale.FirstOrDefault(p => p.ID == tempData.ID));
                    listUserTeamSale.Add(tempData);
                }
            }
        }

        private void cboTypeOrder_EditValueChanged(object sender, EventArgs e)
        {
            string[] typeOrderIDs = cboTypeOrder.EditValue.ToString().Split(',');
            foreach (string typeOrderID in typeOrderIDs)
            {
                bool isActive = TextUtils.ToInt(typeOrderID) == 1;
                dtpDatePayment.Enabled = isActive;
                label21.Visible = isActive;
                if (isActive) return;
            }
        }

        string CreateCode(PaymentOrderModel paymentOrder)
        {

            DateTime dateNow = DateTime.Now;
            string code = "";
            var exp1 = new Expression("YEAR(CreatedDate)", dateNow.Year);
            var exp2 = new Expression("MONTH(CreatedDate)", dateNow.Month);
            var exp3 = new Expression("DAY(CreatedDate)", dateNow.Day);
            var exp4 = new Expression("TypeOrder", paymentOrder.TypeOrder);
            var currentCode = SQLHelper<PaymentOrderModel>.FindByExpression(exp1.And(exp2).And(exp3).And(exp4)).OrderByDescending(x => x.ID).FirstOrDefault();
            code = currentCode == null ? "" : currentCode.Code;
            if (string.IsNullOrEmpty(code))
            {
                code = $"ĐNTTĐB{paymentOrder.DateOrder.Value.ToString("yyyyMMdd")}0001";
                return code;
            }
            int sttOrder = TextUtils.ToInt(code.Substring(code.Length - 4)) + 1;
            string sttText = sttOrder.ToString();
            for (int i = 0; sttText.Length < 4; i++)
            {
                sttText = "0" + sttText;
            }
            code = $"ĐNTTĐB{paymentOrder.DateOrder.Value.ToString("yyyyMMdd")}{sttText}";
            return code;
        }
    }
}
