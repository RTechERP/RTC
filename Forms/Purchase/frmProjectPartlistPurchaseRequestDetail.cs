using BMS.Business;
using BMS.Model;
using BMS.Utils;
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

namespace BMS
{
    public partial class frmProjectPartlistPurchaseRequestDetail : _Forms
    {
        //public int jobRequirementID { get; set; }
        //public int employeeRequestID { get; set; }
        //public DateTime deadline = new DateTime();

        public JobRequirementModel jobRequirement = new JobRequirementModel();

        //LinhTN update 20/08/2024 - START
        public ProjectPartlistPurchaseRequestModel model = new ProjectPartlistPurchaseRequestModel();
        public int customerID = 0;
        public int quantity = 0;
        public string statusRequest = "Yêu cầu mua hàng";
        //LinhTN update 20/08/2024 - END


        //public bool isTechBought = false;
        //public int projectParlistID = 0;

        public ProjectPartlistPurchaseRequestModel requestBought = new ProjectPartlistPurchaseRequestModel();
        public frmProjectPartlistPurchaseRequestDetail()
        {
            InitializeComponent();
        }

        private void frmProjectPartlistPurchaseRequestDetail_Load(object sender, EventArgs e)
        {
            cboNote.SelectedIndex = 0;
            LoadCustomer();
            LoadProductSale();
            LoadProductGroup();
            LoadEmployee();
            LoadCurrency();
            LoadSupplierSale();

            LoadData();
        }

        void LoadData()
        {
            //cboEmployeeRequest.EditValue = Global.EmployeeID;
            //dtpDateReturnExpected.Value = jobRequirement.DeadlineRequest.HasValue ? jobRequirement.DeadlineRequest.Value : DateTime.Now;
            //txtNote.Text = jobRequirement.Note;

            //LinhTN update 20/08/2024 - START
            if (model.ID == 0) //Yêu cầu mua hàng từ phòng nhân sự
            {
                cboEmployeeRequest.EditValue = Global.EmployeeID;
                dtpDateReturnExpected.Value = jobRequirement.DeadlineRequest.HasValue ? jobRequirement.DeadlineRequest.Value : DateTime.Now;
                txtNote.Text = jobRequirement.Note;
            }
            else //Load chi tiết yêu cầu mua hàng - sửa Y/C
            {
                cboCustomer.Enabled = true;
                //cboProductGroup.Enabled = true;
                cboUnitMoney.Enabled = true;
                txtUnitPrice.Enabled = true;
                txtVAT.Enabled = true;
                cboEmployeeRequest.Enabled = true;
                cboSupplierSale.Enabled = true;
                txtLeadTime.Enabled = true;
                txtNote.Enabled = true;
                ckIsImport.Enabled = true;
                btnSaveNew.Enabled = false;
                label35.Visible = false;
                txtHistoryPrice.Enabled = true;
                dtpDateRequest.Enabled = true;


                dtpDateRequest.Value = model.DateRequest.HasValue ? model.DateRequest.Value : DateTime.Now;
                dtpDateReturnExpected.Value = model.DateReturnExpected.HasValue ? model.DateReturnExpected.Value : DateTime.Now;
                txtUnitPrice.Value = TextUtils.ToDecimal(model.UnitPrice);
                txtQuantity.Value = quantity;
                txtVAT.Value = TextUtils.ToDecimal(model.VAT);
                txtUnitFactoryExportPrice.Value = TextUtils.ToDecimal(model.UnitFactoryExportPrice);
                txtUnitImportPrice.Value = TextUtils.ToDecimal(model.UnitImportPrice);
                txtTotalImportPrice.Value = TextUtils.ToDecimal(model.TotalImportPrice);
                txtLeadTime.Value = TextUtils.ToDecimal(model.TotalDayLeadTime);
                txtNote.Text = model.Note;
                ckIsImport.Checked = TextUtils.ToBoolean(model.IsImport);

                cboCustomer.EditValue = customerID; //LinhTN update 20/08/2024
                cboProductSale.EditValue = model.ProductSaleID; //LinhTN update 20/08/2024
                cboProductGroup.EditValue = model.ProductGroupID; //LinhTN update 20/08/2024
                cboEmployeeRequest.EditValue = model.EmployeeID; //LinhTN update 20/08/2024
                cboEmployeeBuy.EditValue = model.EmployeeIDRequestApproved; //LinhTN update 20/08/2024
                cboUnitMoney.EditValue = model.CurrencyID; //LinhTN update 20/08/2024
                cboSupplierSale.EditValue = model.SupplierSaleID; //LinhTN update 20/08/2024
            }
            txtStatusRequest.Text = statusRequest;
            //LinhTN update 20/08/2024 - END

            if (requestBought.IsTechBought == true)
            {
                txtProductName.Text = requestBought.ProductName;
                txtQuantity.Value = TextUtils.ToDecimal(requestBought.Quantity);
            }

            cboUnitMoney.Enabled = requestBought.IsTechBought == true;
            txtVAT.Enabled = requestBought.IsTechBought == true;
            cboSupplierSale.Enabled = requestBought.IsTechBought == true;
            ckIsImport.Enabled = requestBought.IsTechBought == true;
            cboNote.Visible = requestBought.IsTechBought == true;
            txtNote.Visible = requestBought.IsTechBought != true;
            txtLeadTime.Enabled = requestBought.IsTechBought == true;
        }

        //Load khách hàng
        void LoadCustomer()
        {
            List<CustomerModel> list = SQLHelper<CustomerModel>.FindAll();
            cboCustomer.Properties.ValueMember = "ID";
            cboCustomer.Properties.DisplayMember = "CustomerName";
            cboCustomer.Properties.DataSource = list;

        }

        //Load sản phẩm
        void LoadProductSale()
        {
            var listGroup = SQLHelper<ProductGroupModel>.FindAll().Select(x => x.ID).ToList();
            var idGroup = string.Join(",", listGroup);
            DataTable dt = TextUtils.LoadDataFromSP("spGetProductSale", "A", new string[] { "@IDgroup" }, new object[] { idGroup });

            cboProductSale.Properties.ValueMember = "ID";
            cboProductSale.Properties.DisplayMember = "ProductCode";
            cboProductSale.Properties.DataSource = dt;

        }

        //Load loại kho
        void LoadProductGroup()
        {
            List<ProductGroupModel> list = SQLHelper<ProductGroupModel>.FindAll();
            cboProductGroup.Properties.ValueMember = "ID";
            cboProductGroup.Properties.DisplayMember = "ProductGroupName";
            cboProductGroup.Properties.DataSource = list;

        }

        //Load nhân viên
        void LoadEmployee()
        {
            var dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });

            cboEmployeeRequest.Properties.ValueMember = "ID";
            cboEmployeeRequest.Properties.DisplayMember = "FullName";
            cboEmployeeRequest.Properties.DataSource = dt;

            cboEmployeeBuy.Properties.ValueMember = "ID";
            cboEmployeeBuy.Properties.DisplayMember = "FullName";
            cboEmployeeBuy.Properties.DataSource = dt;
        }

        //Load loại tiền
        void LoadCurrency()
        {
            List<CurrencyModel> list = SQLHelper<CurrencyModel>.FindAll();
            cboUnitMoney.Properties.ValueMember = "ID";
            cboUnitMoney.Properties.DisplayMember = "Code";
            cboUnitMoney.Properties.DataSource = list;
        }

        //Load nhà cung cấp
        void LoadSupplierSale()
        {
            List<SupplierSaleModel> list = SQLHelper<SupplierSaleModel>.FindAll().OrderByDescending(x => x.ID).ToList();
            cboSupplierSale.Properties.ValueMember = "ID";
            cboSupplierSale.Properties.DisplayMember = "NameNCC";
            cboSupplierSale.Properties.DataSource = list;
        }
        bool CheckValidate()
        {
            //if (TextUtils.ToInt(cboProductSale.EditValue) == 0)
            //{
            //    MessageBox.Show("Vui lòng chọn Sản phẩm!", "Thông báo");
            //    return false;
            //}
            //if (TextUtils.ToInt(cboEmployeeRequest.EditValue) == 0)
            //{
            //    MessageBox.Show("Vui lòng chọn Người yêu cầu!", "Thông báo");
            //    return false;
            //}

            if (string.IsNullOrWhiteSpace(txtProductName.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Tên sản phẩm!", "Thông báo");
                return false;
            }

            if (TextUtils.ToInt(cboEmployeeBuy.EditValue) == 0 && model.ID == 0) //LinhTN update 20/08/2024
            {
                MessageBox.Show("Vui lòng chọn Nhân viên mua!", "Thông báo");
                return false;
            }

            if (txtQuantity.Value == 0)
            {
                MessageBox.Show("Vui lòng nhập Số lượng!", "Thông báo");
                return false;
            }
            //if (TextUtils.ToInt(cboUnitMoney.EditValue) == 0)
            //{
            //    MessageBox.Show("Vui lòng chọn Loại tiền!", "Thông báo");
            //    return false;
            //}
            //if (txtUnitPrice.Value == 0)
            //{
            //    MessageBox.Show("Vui lòng nhập Đơn giá!", "Thông báo");
            //    return false;
            //}
            //if (TextUtils.ToInt(cboSupplierSale.EditValue) == 0)
            //{
            //    MessageBox.Show("Vui lòng chọn Nhà cung cấp!", "Thông báo");
            //    return false;
            //}

            //Kiểm tra nhập Deadline - start
            if (requestBought.IsTechBought != true)
            {
                DateTime deadline = dtpDateReturnExpected.Value;
                DateTime dateNow = DateTime.Now;

                double timeSpan = (deadline.Date - dateNow.Date).TotalDays + 1;
                if (dateNow.Hour < 15)
                {
                    if (timeSpan < 2)
                    {
                        MessageBox.Show("Deadline tối thiếu là 2 ngày từ ngày hiện tại!", "Thông báo");
                        return false;
                    }
                }
                else if (timeSpan < 3)
                {
                    MessageBox.Show("Yêu cầu từ sau 15h nên ngày Deadline sẽ bắt đầu tính từ ngày hôm sau và tối thiểu là 2 ngày!", "Thông báo");
                    return false;
                }

                if (deadline.DayOfWeek == DayOfWeek.Sunday || deadline.DayOfWeek == DayOfWeek.Saturday)
                {
                    MessageBox.Show("Deadline phải là ngày làm việc (T2 - T6)!", "Thông báo");
                    return false;
                }

                int coutWeekday = 0;
                for (int i = 0; i < timeSpan; i++)
                {
                    DateTime dateValue = dateNow.Date.AddDays(i);
                    if (dateValue.DayOfWeek == DayOfWeek.Sunday || dateValue.DayOfWeek == DayOfWeek.Saturday)
                    {
                        coutWeekday++;
                    }
                }

                if (coutWeekday > 0)
                {
                    DialogResult dialog = MessageBox.Show($"Deadline sẽ không tính Thứ 7 và Chủ nhật.\nBạn có chắc muốn chọn Deadline là ngày [{deadline.ToString("dd/MM/yyyy")}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    return dialog == DialogResult.Yes;
                }
            }
            //Kiểm tra nhập Deadline - end


            if (requestBought.IsTechBought == true && cboNote.SelectedIndex <= 0)
            {
                MessageBox.Show("Vui lòng nhập Ghi chú!", "Thông báo");
                return false;
            }
            return true;
        }
        private bool Save()
        {
            if (!CheckValidate()) return false;
            //ProjectPartlistPurchaseRequestModel model = new ProjectPartlistPurchaseRequestModel()
            //{
            //    JobRequirementID = jobRequirement.ID,
            //    //Khách hàng chưa lưu
            //    ProductSaleID = TextUtils.ToInt(cboProductSale.EditValue),
            //    ProductName = txtProductName.Text.Trim(),
            //    ProductCode = cboProductSale.Text,
            //    //Hãng chưa lưu
            //    //Đơn vị chưa lưu
            //    ProductGroupID = TextUtils.ToInt(cboProductGroup.EditValue),
            //    StatusRequest = 1, //Yêu cầu mua hàng
            //    EmployeeID = TextUtils.ToInt(cboEmployeeRequest.EditValue),
            //    EmployeeIDRequestApproved = TextUtils.ToInt(cboEmployeeBuy.EditValue),
            //    DateRequest = dtpDateRequest.Value,
            //    DateReturnExpected = dtpDateReturnExpected.Value,
            //    Quantity = txtQuantity.Value,
            //    CurrencyID = TextUtils.ToInt(cboUnitMoney.EditValue),
            //    UnitMoney = cboUnitMoney.Text,
            //    CurrencyRate = txtCurrencyRate.Value,
            //    UnitPrice = txtUnitPrice.Value,
            //    TotalPrice = txtTotalPrice.Value,
            //    HistoryPrice = txtHistoryPrice.Value,
            //    TotalPriceExchange = txtTotalPriceExchange.Value,
            //    VAT = txtVAT.Value,
            //    TotaMoneyVAT = txtTotaMoneyVAT.Value,
            //    SupplierSaleID = TextUtils.ToInt(cboSupplierSale.EditValue),
            //    UnitFactoryExportPrice = txtUnitFactoryExportPrice.Value,
            //    TotalImportPrice = txtTotalImportPrice.Value,
            //    LeadTime = TextUtils.ToString(txtLeadTime.Value),
            //    IsImport = ckIsImport.Checked,
            //    Note = txtNote.Text,
            //};

            //LinhTN update 20/08/2024 - START
            //Khách hàng chưa lưu
            //model.CustomerID = TextUtils.ToInt(cboCustomer.EditValue);
            model.ProductSaleID = TextUtils.ToInt(cboProductSale.EditValue);
            model.ProductName = txtProductName.Text.Trim();
            model.ProductCode = cboProductSale.Text;
            //Hãng chưa lưu
            //Đơn vị chưa lưu
            model.ProductGroupID = TextUtils.ToInt(cboProductGroup.EditValue);
            model.EmployeeID = TextUtils.ToInt(cboEmployeeRequest.EditValue);
            model.EmployeeIDRequestApproved = TextUtils.ToInt(cboEmployeeBuy.EditValue);
            model.DateRequest = dtpDateRequest.Value;
            model.DateReturnExpected = dtpDateReturnExpected.Value;
            model.Quantity = txtQuantity.Value;
            model.CurrencyID = TextUtils.ToInt(cboUnitMoney.EditValue);
            model.UnitMoney = cboUnitMoney.Text;
            model.CurrencyRate = txtCurrencyRate.Value;
            model.UnitPrice = txtUnitPrice.Value;
            model.TotalPrice = txtTotalPrice.Value;
            model.HistoryPrice = txtHistoryPrice.Value;
            model.TotalPriceExchange = txtTotalPriceExchange.Value;
            model.VAT = txtVAT.Value;
            model.TotaMoneyVAT = txtTotaMoneyVAT.Value;
            model.SupplierSaleID = TextUtils.ToInt(cboSupplierSale.EditValue);
            model.UnitFactoryExportPrice = txtUnitFactoryExportPrice.Value;
            model.TotalImportPrice = txtTotalImportPrice.Value;
            model.TotalDayLeadTime = TextUtils.ToInt(txtLeadTime.Value);
            model.IsImport = ckIsImport.Checked;
            model.Note = txtNote.Text;
            //LinhTN update 20/08/2024 - END

            if (requestBought.IsTechBought == true)
            {
                var requestBoughts = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByAttribute(ProjectPartlistPurchaseRequestModel_Enum.ProjectPartListID.ToString(), requestBought.ProjectPartListID);
                if (requestBoughts.Count > 0)
                {
                    var myDict = new Dictionary<string, object>()
                    {
                        {ProjectPartlistPurchaseRequestModel_Enum.IsDeleted.ToString(), requestBought.IsTechBought}
                    };
                    var exp = new Expression(ProjectPartlistPurchaseRequestModel_Enum.ID, string.Join(",", requestBoughts.Select(x => x.ID)), "IN");

                    SQLHelper<ProjectPartlistPurchaseRequestModel>.UpdateFields(myDict, exp);
                }

                model.IsTechBought = requestBought.IsTechBought;
                model.ProjectPartListID = requestBought.ProjectPartListID;
                model.Note = cboNote.Text;
                model.ProductCode = requestBought.ProductCode;
            }

            if (model.ID <= 0)
            {
                model.JobRequirementID = jobRequirement.ID;
                model.StatusRequest = 1; //Yêu cầu mua hàng
                SQLHelper<ProjectPartlistPurchaseRequestModel>.Insert(model);
                SendMail(Global.AppFullName, model);


                //Add notify
                string textNotify = $"Người yêu cầu: {cboEmployeeRequest.Text}.\nDeadline: {dtpDateReturnExpected.Value:dd/MM/yyyy}.";
                TextUtils.AddNotify("YÊU CẦU MUA HÀNG", textNotify, TextUtils.ToInt(cboEmployeeBuy.EditValue));
            }
            else //LinhTN update 20/08/2024
            {
                SQLHelper<ProjectPartlistPurchaseRequestModel>.Update(model);

                //POKHDetailModel obj = SQLHelper<POKHDetailModel>.FindByID(model.POKHDetailID);
                //obj.Qty = TextUtils.ToInt(txtQuantity.Value);
                //SQLHelper<POKHDetailModel>.Update(obj);
            }

            //if (model.ID <= 0)
            //{
            //    ProjectPartlistPurchaseRequestBO.Instance.Insert(model);
            //    SendMail(Global.AppFullName, model);

            //}



            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                this.DialogResult = DialogResult.OK;
            }
        }
        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                cboProductSale.EditValue = 0;
                cboEmployeeBuy.EditValue = 0;
                txtQuantity.Value = 0;
                this.InitializeComponent();
            }
        }
        void UpdatePrice()
        {
            //Tính thành tiền chưa VAT
            decimal totalPrice = txtUnitPrice.Value * txtQuantity.Value;
            txtTotalPrice.Value = totalPrice;

            //Tính thành tiền quy đổi (VND)
            decimal totalPriceExchange = txtUnitPrice.Value * txtQuantity.Value * txtCurrencyRate.Value;
            txtTotalPriceExchange.Value = totalPriceExchange;

            //Tính thành tiền có VAT
            decimal totaMoneyVAT = totalPrice + ((totalPrice * txtVAT.Value) / 100); ;
            txtTotaMoneyVAT.Value = totaMoneyVAT;
        }

        void SendMail(string fullName, ProjectPartlistPurchaseRequestModel requestBuy)
        {
            if (requestBuy.ID <= 0) return;
            EmployeeSendEmailModel sendEmail = new EmployeeSendEmailModel();

            EmployeeModel employee = SQLHelper<EmployeeModel>.FindByID(TextUtils.ToInt(requestBuy.EmployeeIDRequestApproved));
            //List<JobRequirementDetailModel> detail = SQLHelper<JobRequirementDetailModel>.FindByAttribute("JobRequirementID", requestBuy.ID);

            //JobRequirementDetailModel contents = detail.Where(x => x.STT == 1).FirstOrDefault() ?? new JobRequirementDetailModel();
            //JobRequirementDetailModel reason = detail.Where(x => x.STT == 3).FirstOrDefault() ?? new JobRequirementDetailModel();
            //JobRequirementDetailModel deadline = detail.Where(x => x.STT == 7).FirstOrDefault() ?? new JobRequirementDetailModel();

            sendEmail.Subject = $"YÊU CẦU MUA HÀNG - {fullName.ToUpper()} - {DateTime.Now.ToString("dd/MM/yyyy")}";
            sendEmail.EmailTo = $"{employee.EmailCongTy}";
            sendEmail.EmailCC = $"";
            sendEmail.Body = $@"<div> <p style=""font-weight: bold; color: red;"">[NO REPLY]</p> <p> Dear anh/chị {employee.FullName} </p ></div >
                        <div style = ""margin-top: 30px;"">
                        <p> Cho em yêu cầu mua hàng thông tin sản phẩm như sau: </p>
                        <p> Mã sản phẩm: {requestBuy.ProductCode}</p>
                        <p> Tên sản phẩm: {requestBuy.ProductName}</p>
                        <p> Số lượng: {requestBuy.Quantity}</p>
                        <p> Deadline: {requestBuy.DateReturnExpected}</p>
                        </div>
                        <div style = ""margin-top: 30px;"">
                        <p> Thanks </p>
                        <p> {fullName}</p>
                        </div>";

            sendEmail.StatusSend = 1;
            sendEmail.EmployeeID = TextUtils.ToInt(jobRequirement.EmployeeID);
            sendEmail.Receiver = TextUtils.ToInt(jobRequirement.ApprovedTBPID);
            //sendEmailRepo.Create(e);

            SQLHelper<EmployeeSendEmailModel>.Insert(sendEmail);
        }

        private void txtQuantity_ValueChanged(object sender, EventArgs e)
        {
            UpdatePrice();
        }
        private void cboUnitMoney_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                SearchLookUpEdit lookUpEdit = (SearchLookUpEdit)sender;
                CurrencyModel currency = (CurrencyModel)lookUpEdit.GetSelectedDataRow();
                currency = currency ?? new CurrencyModel();
                bool isExpried = ((currency.DateExpried < DateTime.Now || currency.DateStart > DateTime.Now) && currency.Code.ToLower().Trim() != "vnd");
                txtCurrencyRate.Value = !isExpried ? currency.CurrencyRate : 0;
                //int ID = TextUtils.ToInt(cboUnitMoney.EditValue);
                //CurrencyModel obj = SQLHelper<CurrencyModel>.FindByID(ID);
                //txtCurrencyRate.Value = obj.CurrencyRate;
                UpdatePrice();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        private void txtUnitPrice_ValueChanged(object sender, EventArgs e)
        {
            UpdatePrice();
        }
        private void txtVAT_ValueChanged(object sender, EventArgs e)
        {
            UpdatePrice();
        }

        private void ckIsImport_CheckedChanged(object sender, EventArgs e)
        {
            if (ckIsImport.Checked)
            {
                txtUnitFactoryExportPrice.Enabled = true;
                txtUnitImportPrice.Enabled = true;
                txtTotalImportPrice.Enabled = true;
            }
            else
            {
                txtUnitFactoryExportPrice.Enabled = false;
                txtUnitImportPrice.Enabled = false;
                txtTotalImportPrice.Enabled = false;
            }
        }

        private void cboProductSale_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                int ID = TextUtils.ToInt(cboProductSale.EditValue);
                txtProductName.Enabled = !(ID > 0);
                ProductSaleModel obj = SQLHelper<ProductSaleModel>.FindByID(ID);
                txtMaker.Text = obj.Maker;
                txtProductName.Text = obj.ProductName;
                txtUnit.Text = obj.Unit;
                cboProductGroup.EditValue = obj.ProductGroupID;
                if (!string.IsNullOrEmpty(obj.ProductCode))
                {
                    DataTable dt = TextUtils.LoadDataFromSP("spGetHistoryPricePartlist", "A", new string[] { "@ProductSaleID" }, new object[] { ID });
                    DataRow dataHistoryPrice = dt.AsEnumerable().FirstOrDefault(x => x.Field<string>("ProductCode") == obj.ProductCode);

                    decimal lastHistoryPrice = dataHistoryPrice != null ? TextUtils.ToDecimal(dataHistoryPrice["UnitPrice"]) : 0;
                    txtHistoryPrice.Value = lastHistoryPrice;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnCreateProducSale_Click(object sender, EventArgs e)
        {
            frmAssetDetail frm = new frmAssetDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadProductSale();
            }
        }
    }
}