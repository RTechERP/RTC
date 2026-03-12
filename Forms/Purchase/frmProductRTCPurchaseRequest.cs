using BMS;
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
    public partial class frmProductRTCPurchaseRequest : _Forms
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


        int productRTCID = 0;
        public bool IsChanged = false;

        public frmProductRTCPurchaseRequest(int productRTCID)
        {
            InitializeComponent();
            this.productRTCID = productRTCID;
        }

        private void frmProductRTCBorrowRequest_Load(object sender, EventArgs e)
        {
            LoadEmployeeAprrove();//PQ.Chien - UPDATE - 17 / 04 / 2025
            LoadTicketType();//PQ.Chien - UPDATE - 17 / 04 / 2025
            LoadCustomer();
            LoadProductRTC();
            LoadProductGroup();
            LoadEmployee();
            LoadFirm();
            LoadCurrency();
            LoadSupplierSale();
            loadUnit();
            LoadData();
        }

        void LoadData()
        {
            


            //tnbinh update 22/08/25
            if (model.ID > 0)
            {
                dtpDateRequest.Value = model.DateRequest ?? DateTime.Now;
                dtpDateReturnExpected.Value = model.DateReturnExpected ?? DateTime.Now;

                if (model.UnitMoney != null)
                {
                    var currency = SQLHelper<CurrencyModel>.FindAll()
                        .FirstOrDefault(p => p.Code == (model.UnitMoney ?? ""));

                    int valueMoney = currency?.ID ?? 0;
                    cboUnitMoney.EditValue = valueMoney;
                }
                txtUnitPrice.Value = model.UnitPrice ?? 0;
                txtQuantity.Value = model?.Quantity ?? 0;
                txtTotalPrice.Value = model.TotalPrice ?? 0;
                txtTotaMoneyVAT.Value = model.TotaMoneyVAT ?? 0;
                txtTotalPriceExchange.Value = model.TotalPriceExchange ?? 0;
                txtHistoryPrice.Value = model.HistoryPrice ?? 0;
                txtCurrencyRate.Value = model.CurrencyRate ?? 0;
                txtVAT.Value = model.VAT ?? 0;
                if (model.SupplierSaleID != null)
                {
                    cboSupplierSale.EditValue = model.SupplierSaleID ?? 0;
                }
                txtUnitFactoryExportPrice.Value = model.UnitFactoryExportPrice ?? 0;
                txtNote.Text = model.Note ?? "";
                txtTotalImportPrice.Value = model.TotalImportPrice ?? 0;
                txtLeadTime.Value = model.TotalDayLeadTime ?? 0;
                txtUnitImportPrice.Value = model.UnitImportPrice ?? 0;
                ckIsImport.Checked = model.IsImport ?? false;
                cboProductSale.EditValue = model.ProductRTCID;

                cboTicketType.SelectedIndex = TextUtils.ToInt(model.TicketType);
                //if (model.EmployeeID != null)
                {
                    //EmployeeModel employee = SQLHelper<EmployeeModel>.FindByID(model.EmployeeID ?? 0);
                    cboEmployeeRequest.EditValue = model.EmployeeID;
                }
                //if (model.EmployeeIDRequestApproved != null)
                {
                    //EmployeeModel employee = SQLHelper<EmployeeModel>.FindByID(model.EmployeeIDRequestApproved ?? 0);
                    cboEmployeeBuy.EditValue = model.EmployeeIDRequestApproved;
                }
                //if (model.ProductGroupID != null)
                {
                    //ProductGroupRTCModel productgroup = SQLHelper<ProductGroupRTCModel>.FindByID(model.ProductGroupID ?? 0);
                    //cboProductGroup.EditValue = model.ProductGroupID;
                }
            }
            else
            {
                //ndnhat - 27/03/2025
                int id = TextUtils.ToInt(cboProductSale.EditValue);
                ProductRTCModel ProModel = SQLHelper<ProductRTCModel>.FindByID(id);
                txtProductName.Text = ProModel.ProductName;

                int firmID = TextUtils.ToInt(ProModel.FirmID);
                FirmModel firm = SQLHelper<FirmModel>.FindByID(firmID);
                if (firm.ID <= 0)
                {
                    var exp1 = new Expression(FirmModel_Enum.FirmName.ToString(), TextUtils.ToString(ProModel.Maker));
                    var exp2 = new Expression(FirmModel_Enum.FirmType.ToString(), 2);
                    firm = SQLHelper<FirmModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault() ?? new FirmModel();
                }

                cboFirm.EditValue = firm.ID;
                int unitId = TextUtils.ToInt(ProModel.UnitCountID);
                UnitCountKTModel unit = SQLHelper<UnitCountKTModel>.FindByID(unitId);
                cboUnitCount.EditValue = unit.ID;
                //end ndnhat - 27/03/2025
                cboProductGroup.EditValue = TextUtils.ToInt(ProModel.ProductGroupRTCID);
                if (!string.IsNullOrEmpty(ProModel.ProductCode))
                {
                    //DataTable dt = TextUtils.LoadDataFromSP("spGetHistoryPricePartlist", "A", new string[] { "@ProductRTCID" }, new object[] { id });
                    //DataRow dataHistoryPrice = dt.AsEnumerable().FirstOrDefault(x => x.Field<string>("ProductCode") == ProModel.ProductCode);

                    //decimal lastHistoryPrice = dataHistoryPrice != null ? TextUtils.ToDecimal(dataHistoryPrice["UnitPrice"]) : 0;
                    //txtHistoryPrice.Value = lastHistoryPrice;
                }
            }
            //end update


        }

        void Reset()
        {
            cboUnitCount.EditValue = 0;
            cboProductSale.EditValue = 0;
            //cboEmployeeBuy.EditValue = 0;
            cboFirm.EditValue = 0;
            cboProductGroup.EditValue = 0;
            //txtQuantity.Value = 0;
            txtProductName.Text = string.Empty;
            //cboUnitMoney.EditValue = 0;
            //txtUnitPrice.Value = 0;
            //txtNote.Text = string.Empty;
            dtpDateRequest.Value = DateTime.Now;
            //dtpDateReturnExpected.Value = DateTime.Now;
            //cboSupplierSale.EditValue = 0;
            //cboCustomer.EditValue = 0;
            //productRTCID = 0;
            //cboEmployeeRequest.EditValue = 0;
            //LoadCustomer();
            //LoadProductRTC();
            //LoadProductGroup();
            //LoadEmployee();
            //LoadFirm();
            //LoadCurrency();
            //LoadSupplierSale();
        }

        //PQ.Chien - UPDATE - 17 / 04 / 2025
        void LoadTicketType()
        {
            List<object> lst = new List<object>()
            {
                new {ID = 0, TicketType = "Phiếu mua"},
                new {ID = 1, TicketType = "Phiếu mượn"}
            };
            cboTicketType.DataSource = lst;
            cboTicketType.ValueMember = "ID";
            cboTicketType.DisplayMember = "TicketType";
            cboTicketType.SelectedIndex = 0;
        }
        //PQ.Chien - UPDATE - 17 / 04 / 2025
        private void cboTicketType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboTicketType.SelectedIndex == 1)
            {

                dtpDateReturnEstimated.Enabled = true;
                cboEmployeeApprove.Enabled = true;
                //dtpDateReturnExpected.Enabled = false;

            }
            else
            {

                dtpDateReturnEstimated.Enabled = false;
                cboEmployeeApprove.Enabled = false;
                //dtpDateReturnExpected.Enabled = true;
            }
        }

        //PQ.Chien - UPDATE - 17 / 04 / 2025
        void LoadEmployeeAprrove()
        {
            List<EmployeeApproveModel> list = SQLHelper<EmployeeApproveModel>.FindByAttribute("Type", 1);
            cboEmployeeApprove.Properties.ValueMember = "EmployeeID";
            cboEmployeeApprove.Properties.DisplayMember = "FullName";
            cboEmployeeApprove.Properties.DataSource = list;
        }

        void LoadFirm()
        {
            List<FirmModel> list = SQLHelper<FirmModel>.FindAll();
            cboFirm.Properties.ValueMember = "ID";
            cboFirm.Properties.DisplayMember = "FirmName";
            cboFirm.Properties.DataSource = list;
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
        void LoadProductRTC()
        {
            var listGroup = SQLHelper<ProductRTCModel>.FindByAttribute(ProductRTCModel_Enum.IsDelete.ToString(), 0);
            //var idGroup = string.Join(",", listGroup);
            //DataTable dt = TextUtils.LoadDataFromSP("spGetProductSale", "A", new string[] { "@IDgroup" }, new object[] { idGroup });

            cboProductSale.Properties.ValueMember = "ID";
            //cboProductSale.Properties.DisplayMember = "ProductCode";
            cboProductSale.Properties.DisplayMember = "ProductCodeRTC";
            cboProductSale.Properties.DataSource = listGroup;

            cboProductSale.EditValue = productRTCID;

        }

        //Load loại kho
        void LoadProductGroup()
        {
            List<ProductGroupRTCModel> list = SQLHelper<ProductGroupRTCModel>.FindAll();
            cboProductGroup.Properties.ValueMember = "ID";
            cboProductGroup.Properties.DisplayMember = "ProductGroupName";
            cboProductGroup.Properties.DataSource = list;

        }

        //Load nhân viên
        void LoadEmployee()
        {
            DataTable dt = TextUtils.GetDataTableFromSP("spGetEmployee", new string[] { "@Status" }, new object[] { 0 });

            cboEmployeeRequest.Properties.ValueMember = "ID";
            cboEmployeeRequest.Properties.DisplayMember = "FullName";
            cboEmployeeRequest.Properties.DataSource = dt;
            cboEmployeeRequest.EditValue = Global.EmployeeID;


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

        private void loadUnit()
        {
            var list = SQLHelper<UnitCountKTModel>.FindAll();
            cboUnitCount.Properties.DisplayMember = "UnitCountName";
            cboUnitCount.Properties.ValueMember = "ID";
            cboUnitCount.Properties.DataSource = list;
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

            if (string.IsNullOrWhiteSpace(txtProductCode.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Mã sản phẩm!", "Thông báo");
                return false;
            }

            if (TextUtils.ToInt(cboFirm.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Hãng!", "Thông báo");
                return false;
            }

            if (string.IsNullOrWhiteSpace(cboUnitCount.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Đơn vị!", "Thông báo");
                return false;
            }

            if (TextUtils.ToInt(cboProductGroup.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Loại kho!", "Thông báo");
                return false;
            }

            //if (TextUtils.ToInt(cboEmployeeBuy.EditValue) == 0 && model.ID == 0) //LinhTN update 20/08/2024
            //{
            //    MessageBox.Show("Vui lòng chọn Nhân viên mua!", "Thông báo");
            //    return false;
            //}

            if (txtQuantity.Value <= 0)
            {
                MessageBox.Show("Vui lòng nhập Số lượng!", "Thông báo");
                return false;
            }
            //PQ.Chien - UPDATE - 17 / 04 / 2025
            if (cboTicketType.SelectedIndex == 1 && cboEmployeeApprove.EditValue == null)
            {
                MessageBox.Show("Vui lòng chọn trưởng bộ phận duyệt!", "Thông báo");
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
            if (cboTicketType.SelectedIndex == 1 && TextUtils.ToInt(cboSupplierSale.EditValue) == 0)
            {
                MessageBox.Show("Vui lòng chọn Nhà cung cấp!", "Thông báo");
                return false;
            }

            //Kiểm tra nhập Deadline - start
            if (requestBought.IsTechBought != true/* && cboTicketType.SelectedIndex == 0*/)
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



            return true;
        }
        private bool Save()
        {
            if (!CheckValidate()) return false;
            //ProjectPartlistPurchaseRequestModel ProModel = new ProjectPartlistPurchaseRequestModel()
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

            //Khách hàng chưa lưu
            model.ProductRTCID = TextUtils.ToInt(cboProductSale.EditValue);
            model.ProductName = txtProductName.Text.Trim();
            model.ProductCode = txtProductCode.Text;
            //Hãng chưa lưu
            model.Maker = cboFirm.Text.Trim();
            //Đơn vị chưa lưu
            model.UnitName = cboUnitCount.Text.Trim();
            model.ProductGroupRTCID = TextUtils.ToInt(cboProductGroup.EditValue);
            model.EmployeeID = TextUtils.ToInt(cboEmployeeRequest.EditValue);
            model.EmployeeIDRequestApproved = TextUtils.ToInt(cboEmployeeBuy.EditValue);
            model.DateRequest = dtpDateRequest.Value;
            model.DateReturnExpected = dtpDateReturnExpected.Value;
            //ProModel.Quantity = txtQuantity.Value;
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
            model.Quantity = txtQuantity.Value;//ndnhat-27/03/2025
            model.EmployeeID = TextUtils.ToInt(cboEmployeeRequest.EditValue);

            // PQ.Chien - 15/04/2025======================
            model.DateReturnEstimated = dtpDateReturnEstimated.Value;
            model.TicketType = TextUtils.ToInt(cboTicketType.SelectedIndex);
            model.EmployeeApproveID = TextUtils.ToInt(cboEmployeeApprove.EditValue);
            model.ApprovedTBP = TextUtils.ToInt(cboEmployeeApprove.EditValue);
            //model.ProductSaleID = TextUtils.ToInt(cboProductSale.EditValue);

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
                model.Note = txtNote.Text;
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

                //POKHDetailModel obj = SQLHelper<POKHDetailModel>.FindByID(ProModel.POKHDetailID);
                //obj.Qty = TextUtils.ToInt(txtQuantity.Value);
                //SQLHelper<POKHDetailModel>.Update(obj);
            }

            //if (ProModel.ID <= 0)
            //{
            //    ProjectPartlistPurchaseRequestBO.Instance.Insert(ProModel);
            //    SendMail(Global.AppFullName, ProModel);

            //}

            IsChanged = true;
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                this.Close();//ndnhat-27/03/2025
                //this.DialogResult = DialogResult.OK;
            }
        }
        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                //cboProductSale.EditValue = 0;
                //cboEmployeeBuy.EditValue = 0;
                //txtQuantity.Value = 0;
                //this.InitializeComponent();

                model = new ProjectPartlistPurchaseRequestModel();
                Reset();
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
                //SearchLookUpEdit lookUpEdit = (SearchLookUpEdit)sender;
                //CurrencyModel currency = (CurrencyModel)lookUpEdit.GetSelectedDataRow();
                //currency = currency ?? new CurrencyModel();
                //bool isExpried = ((currency.DateExpried < DateTime.Now || currency.DateStart > DateTime.Now) && currency.Code.ToLower().Trim() != "vnd");
                //txtCurrencyRate.Value = !isExpried ? currency.CurrencyRate : 0;
                int ID = TextUtils.ToInt(cboUnitMoney.EditValue);
                CurrencyModel obj = SQLHelper<CurrencyModel>.FindByID(ID);
                txtCurrencyRate.Value = obj.CurrencyRate;
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
                cboFirm.Enabled = !(ID > 0);
                cboUnitCount.Enabled = !(ID > 0);
                cboProductGroup.Enabled = !(ID > 0);
                txtProductCode.Enabled = !(ID > 0);

                ProductRTCModel obj = SQLHelper<ProductRTCModel>.FindByID(ID);
                txtProductName.Text = obj.ProductName;
                txtProductCode.Text = obj.ProductCode;
                //txtUnit.Text = obj.Unit;
                //cboProductGroup.EditValue = obj.ProductGroupID;


                cboUnitCount.EditValue = SQLHelper<UnitCountKTModel>.FindByID(TextUtils.ToInt(obj.UnitCountID)).ID;
                cboProductGroup.EditValue = obj.ProductGroupRTCID;
                cboFirm.EditValue = obj.FirmID;
                //if (!string.IsNullOrEmpty(obj.ProductCode))
                //{
                //    DataTable dt = TextUtils.LoadDataFromSP("spGetHistoryPricePartlist", "A", new string[] { "@ProductSaleID" }, new object[] { ID });
                //    DataRow dataHistoryPrice = dt.AsEnumerable().FirstOrDefault(x => x.Field<string>("ProductCode") == obj.ProductCode);

                //    decimal lastHistoryPrice = dataHistoryPrice != null ? TextUtils.ToDecimal(dataHistoryPrice["UnitPrice"]) : 0;
                //    txtHistoryPrice.Value = lastHistoryPrice;
                //}
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnCreateProducSale_Click(object sender, EventArgs e)
        {
            LoadProductRTC();
        }

        private void cboCustomer_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
