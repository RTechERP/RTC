using BMS.Business;
using BMS.Model;
using BMS.Utils;
using Forms.Classes;
using Newtonsoft.Json;
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

namespace BMS
{
    public partial class frmAccountingContractDetail : _Forms
    {
        int warehouseID = 1;
        public AccountingContractModel contract = new AccountingContractModel();
        public bool isReceivedContract = false;

        object oldContract = new object();

        string[] listFileUpload;
        List<AccountingContractFileModel> listFiles = new List<AccountingContractFileModel>();

        public bool IsCopy = false;
        public frmAccountingContractDetail()
        {
            InitializeComponent();
        }

        private void frmAccountingContractDetail_Load(object sender, EventArgs e)
        {
            LoadContractType();
            LoadCustomer();
            LoadSupplier();
            LoadEmployee();
            LoadContract();
            LoadData();
        }


        void LoadContractType()
        {
            List<AccountingContractTypeModel> list = SQLHelper<AccountingContractTypeModel>.FindAll().OrderBy(x => x.STT).ToList();
            cboAccountingContractType.Properties.ValueMember = "ID";
            cboAccountingContractType.Properties.DisplayMember = "TypeName";
            cboAccountingContractType.Properties.DataSource = list;
        }


        void LoadCustomer()
        {
            List<CustomerModel> list = SQLHelper<CustomerModel>.FindByAttribute("IsDeleted", 0).OrderByDescending(x => x.ID).ToList();
            cboCustomer.Properties.ValueMember = "ID";
            cboCustomer.Properties.DisplayMember = "CustomerName";
            cboCustomer.Properties.DataSource = list;
        }

        void LoadSupplier()
        {
            List<SupplierSaleModel> list = SQLHelper<SupplierSaleModel>.FindAll().OrderByDescending(x => x.ID).ToList();
            cboSupplier.Properties.ValueMember = "ID";
            cboSupplier.Properties.DisplayMember = "NameNCC";
            cboSupplier.Properties.DataSource = list;
        }

        void LoadEmployee()
        {
            DataTable list = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = list;
        }

        void LoadContract()
        {
            List<AccountingContractModel> list = SQLHelper<AccountingContractModel>.FindAll().Where(x => x.IsDelete == false).OrderByDescending(x => x.DateInput).ToList();
            list.Insert(0, new AccountingContractModel() { ID = -1, ContractNumber = "HĐ cha" });
            cboContract.Properties.ValueMember = "ID";
            cboContract.Properties.DisplayMember = "ContractNumber";
            cboContract.Properties.DataSource = list;
        }

        void LoadData()
        {
            if (IsCopy) contract.ID = 0;
            //btnSave.Enabled = btnSaveNew.Enabled = !contract.IsApproved;
            if (contract.ID > 0 && !Global.IsAdmin)
            {
                btnSave.Enabled = btnSaveNew.Enabled = (isReceivedContract || contract.CreatedBy == Global.LoginName) && !contract.IsApproved;//Khánh update 27/01/2024
            }

            dtpDateInput.Value = contract.DateInput.HasValue ? contract.DateInput.Value : DateTime.Now;
            cboAccountingContractType.EditValue = contract.AccountingContractTypeID;
            txtContractNumber.Text = contract.ContractNumber;
            cboCompany.SelectedIndex = contract.Company;
            cboCustomer.EditValue = contract.CustomerID;
            txtContractValue.EditValue = contract.ContractValue;
            dtpDateExpired.Value = contract.DateExpired.HasValue ? contract.DateExpired.Value : DateTime.Now;
            cboContractGroup.SelectedIndex = contract.ContractGroup;
            cboSupplier.EditValue = contract.SupplierSaleID;
            dtpDateIsApprovedGroup.EditValue = contract.DateIsApprovedGroup;//.HasValue ? contract.DateIsApprovedGroup.Value : DateTime.Now;
            cboEmployee.EditValue = contract.EmployeeID;
            txtContractContent.Text = contract.ContractContent;
            txtContentPayment.Text = contract.ContentPayment;
            txtNote.Text = contract.Note;
            cboContract.EditValue = contract.ParentID;

            dtpDateReceived.EditValue = contract.DateReceived;
            txtQuantityDocument.Value = contract.QuantityDocument;
            dtpDateContract.EditValue = contract.DateContract;
            txtUnit.Text = contract.Unit;

            //if (contract.IsReceivedContract || isReceivedContract)
            //{
            //    dtpDateReceived.Focus();
            //}
            lblDateReceivedValidate.Visible = (contract.IsReceivedContract || isReceivedContract);

            oldContract = new
            {
                DateReceived = TextUtils.ToString(dtpDateReceived.EditValue),
                QuantityDocument = txtQuantityDocument.Value,
                Company = cboCompany.Text,
                ContractGroup = txtContractValue.Text,
                AccountingContractTypeID = cboAccountingContractType.Text,
                CustomerID = cboCustomer.Text,
                SupplierSaleID = cboSupplier.Text,
                ContractNumber = txtContractNumber.Text,
                ContractValue = txtContractValue.EditValue,
                DateExpired = dtpDateExpired.Value.ToString("dd/MM/yyyy"),
                DateIsApprovedGroup = TextUtils.ToDate5(dtpDateIsApprovedGroup.EditValue).ToString("dd/MM/yyyy"),
                EmployeeID = cboEmployee.Text,
                ParentID = cboContract.Text,
                ContractContent = txtContractContent.Text,
                ContentPayment = txtContentPayment.Text,
                IsReceivedContract = contract.IsReceivedContract ? "Đã nhận hồ sơ gốc" : "Huỷ/Chưa nhận hồ sơ gốc",
                DateContract = contract.DateContract,
                Unit = txtUnit.Text.Trim()
            };

            //Load file name
            listFiles = SQLHelper<AccountingContractFileModel>.FindByAttribute("AccountingContractID", contract.ID).OrderByDescending(x => x.ID).ToList();
            LoadFile(listFiles);
        }

        void LoadFile(List<AccountingContractFileModel> listFiles)
        {
            grdData.DataSource = listFiles;
            //txtContractFile.Text = string.Join(Environment.NewLine, files);
            grvData.RefreshData();
        }

        bool SaveData()
        {
            if (!CheckValidate())
            {
                return false;
            }

            contract.DateInput = dtpDateInput.Value;
            contract.Company = cboCompany.SelectedIndex;
            contract.ContractGroup = cboContractGroup.SelectedIndex;
            contract.Unit = txtUnit.Text.Trim().ToUpper();
            contract.AccountingContractTypeID = TextUtils.ToInt(cboAccountingContractType.EditValue);
            contract.CustomerID = TextUtils.ToInt(cboCustomer.EditValue);
            contract.SupplierSaleID = TextUtils.ToInt(cboSupplier.EditValue);
            contract.ContractNumber = txtContractNumber.Text.Trim();
            contract.ContractValue = TextUtils.ToDecimal(txtContractValue.EditValue);
            contract.DateExpired = dtpDateExpired.Value;
            contract.DateIsApprovedGroup = TextUtils.ToDate4(dtpDateIsApprovedGroup.EditValue);
            contract.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
            contract.ContractContent = txtContractContent.Text.Trim();
            contract.ContentPayment = txtContentPayment.Text.Trim();
            contract.Note = txtNote.Text.Trim();

            contract.ParentID = TextUtils.ToInt(cboContract.EditValue) <= 0 ? 0 : TextUtils.ToInt(cboContract.EditValue);
            contract.DateReceived = TextUtils.ToDate4(dtpDateReceived.EditValue);
            contract.QuantityDocument = TextUtils.ToInt(txtQuantityDocument.Value);
            contract.IsReceivedContract = contract.DateReceived.HasValue;
            contract.DateContract = TextUtils.ToDate4(dtpDateContract.EditValue);

            //contract.IsApproved = contract.DateReceived.HasValue;//Khánh update 27/01/2024

            var currentContract = new
            {
                DateReceived = TextUtils.ToString(dtpDateReceived.EditValue),
                QuantityDocument = txtQuantityDocument.Value,
                Company = cboCompany.Text,
                ContractGroup = txtContractValue.Text,
                AccountingContractTypeID = cboAccountingContractType.Text,
                CustomerID = cboCustomer.Text,
                SupplierSaleID = cboSupplier.Text,
                ContractNumber = txtContractNumber.Text,
                ContractValue = txtContractValue.EditValue,
                DateExpired = dtpDateExpired.Value.ToString("dd/MM/yyyy"),
                DateIsApprovedGroup = TextUtils.ToDate5(dtpDateIsApprovedGroup.EditValue).ToString("dd/MM/yyyy"),
                EmployeeID = cboEmployee.Text,
                ParentID = cboContract.Text,
                ContractContent = txtContractContent.Text,
                ContentPayment = txtContentPayment.Text,
                IsReceivedContract = contract.IsReceivedContract ? "Đã nhận hồ sơ gốc" : "Huỷ/Chưa nhận hồ sơ gốc",
                DateContract = contract.DateContract,
                Unit = contract.Unit
            };

            if (contract.ID > 0)
            {
                SaveLog(oldContract, currentContract);
                SQLHelper<AccountingContractModel>.Update(contract);
            }
            else
            {
                //SQLHelper<AccountingContractModel>.Insert(contract);
                contract.ID = (int)AccountingContractBO.Instance.Insert(contract);
            }

            UploadFile(contract.ID);

            return true;
        }

        void SaveLog(object old, object current)
        {
            var resultCompare = TextUtils.DeepEquals(old, current);
            bool equal = TextUtils.ToBoolean(resultCompare.GetType().GetProperty("equal").GetValue(resultCompare));

            if (!equal)
            {
                string contentLog = "";
                List<string> propertys = (List<string>)resultCompare.GetType().GetProperty("property").GetValue(resultCompare);
                foreach (string property in propertys)
                {
                    if (property == "Note")
                    {
                        continue;
                    }
                    string propertyText = TextUtils.ToString(cGlobVar.accountingContract.GetType().GetProperty(property).GetValue(cGlobVar.accountingContract));
                    contentLog += $"{propertyText.ToUpper()}:\n" +
                                  $"từ {old.GetType().GetProperty(property).GetValue(old)}\n" +
                                  $"thành {current.GetType().GetProperty(property).GetValue(current)}\n\n";
                }

                AccountingContractLogModel log = new AccountingContractLogModel();
                log.AccountingContractID = contract.ID;
                log.DateLog = DateTime.Now;
                log.IsReceivedContract = contract.IsReceivedContract;
                log.ContentLog = contentLog;

                SQLHelper<AccountingContractLogModel>.Insert(log);
            }
        }

        public void UploadFile(int contractID)
        {
            try
            {
                //string pathUpload = @"\\192.168.1.190\Common\08. SOFTWARES\LeTheAnh\DemoContractAccounting";

                ConfigSystemModel config = SQLHelper<ConfigSystemModel>.FindByAttribute("KeyName", "PathAccounting").FirstOrDefault();
                string pathUpload = config != null ? $@"{config.KeyValue}" : "";

                AccountingContractModel contract = SQLHelper<AccountingContractModel>.ProcedureToList("spGetAccountingContractParent", new string[] { "@ID" }, new object[] { contractID }).FirstOrDefault();

                string custormer = cboContractGroup.SelectedIndex == 1 ? cboSupplier.Text : cboCustomer.Text;
                string contractNumber = contract != null ? $@"\{contract.ContractNumber}" : "";
                string destFilename = $@"{cboCompany.Text.ToUpper()}\{cboContractGroup.Text.ToUpper()}\{custormer}{contractNumber}";

                destFilename = Path.Combine(pathUpload, destFilename);
                if (!Directory.Exists(destFilename))
                {
                    Directory.CreateDirectory(destFilename);
                }

                if (listFileUpload == null || listFileUpload.Length <= 0)
                {
                    return;
                }
                foreach (var file in listFileUpload)
                {
                    FileInfo fileInfo = new FileInfo(file);


                    File.Copy(fileInfo.FullName, Path.Combine(destFilename, fileInfo.Name), true);

                    AccountingContractFileModel contractFile = new AccountingContractFileModel();
                    contractFile.AccountingContractID = contractID;
                    contractFile.FileName = fileInfo.Name;
                    contractFile.OriginPath = fileInfo.DirectoryName;
                    contractFile.ServerPath = destFilename;
                    SQLHelper<AccountingContractFileModel>.Insert(contractFile);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        bool CheckValidate()
        {
            if (cboCompany.SelectedIndex <= 0)
            {
                MessageBox.Show("Vui lòng nhập Công ty!", "Thông báo");
                return false;
            }

            if (cboContractGroup.SelectedIndex <= 0)
            {
                MessageBox.Show("Vui lòng nhập Phân loại HĐ chính!", "Thông báo");
                return false;
            }

            if (TextUtils.ToInt(cboAccountingContractType.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Loại HĐ!", "Thông báo");
                return false;
            }

            if (cboContractGroup.SelectedIndex == 1 && TextUtils.ToInt(cboSupplier.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Nhà cung cấp!", "Thông báo");
                return false;
            }
            else if (cboContractGroup.SelectedIndex == 2 && TextUtils.ToInt(cboCustomer.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Khách hàng!", "Thông báo");
                return false;
            }

            if (string.IsNullOrEmpty(txtContractNumber.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Số HĐ/PL!", "Thông báo");
                return false;
            }

            var typeSelected = (AccountingContractTypeModel)cboAccountingContractType.Properties.GetRowByKeyValue(cboAccountingContractType.EditValue);
            //int contractValue = TextUtils.ToInt(txtContractValue.EditValue);

            //string typeSelectedValue = typeSelected.TypeCode.Trim();
            //string type = "HĐNT";
            //bool compare = typeSelectedValue == type; //string.Compare(, "HDNT", true);
            // int compare = string.Compare(typeSelected.TypeCode, "HDNT", true);

            AccountingContractTypeModel contractType = SQLHelper<AccountingContractTypeModel>.FindByID(TextUtils.ToInt(cboAccountingContractType.EditValue));

            if (TextUtils.ToDecimal(txtContractValue.EditValue) <= 0 && contractType.IsContractValue == true)
            {
                MessageBox.Show("Vui lòng nhập Giá trị HĐ!", "Thông báo");
                return false;
            }

            if (string.IsNullOrEmpty(txtUnit.Text) && contractType.IsContractValue == true)
            {
                MessageBox.Show("Vui lòng nhập ĐVT!", "Thông báo");
                return false;
            }


            if (TextUtils.ToInt(cboEmployee.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập NV phụ trách!", "Thông báo");
                return false;
            }

            if (string.IsNullOrEmpty(txtContractContent.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Nội dung HĐ!", "Thông báo");
                return false;
            }

            if (string.IsNullOrEmpty(txtContractContent.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập Nội dung thanh toán!", "Thông báo");
                return false;
            }

            if (contract.ID > 0)
            {
                AccountingContractModel contractCurrent = new AccountingContractModel()
                {
                    DateInput = contract.DateInput,
                    Company = contract.Company,
                    ContractGroup = contract.ContractGroup,
                    AccountingContractTypeID = contract.AccountingContractTypeID,
                    CustomerID = contract.CustomerID,
                    SupplierSaleID = contract.SupplierSaleID,
                    ContractNumber = contract.ContractNumber,
                    ContractValue = contract.ContractValue,
                    DateExpired = contract.DateExpired,
                    DateIsApprovedGroup = contract.DateIsApprovedGroup,
                    EmployeeID = contract.EmployeeID,
                    ContractContent = contract.ContractContent,
                    ContentPayment = contract.ContentPayment,
                    ParentID = contract.ParentID,
                    DateContract = contract.DateContract,
                    Unit = contract.Unit,
                };

                string contractValue = String.Format("{0:0.00}", txtContractValue.EditValue);
                AccountingContractModel contractUpdate = new AccountingContractModel()
                {
                    DateInput = dtpDateInput.Value,
                    Company = cboCompany.SelectedIndex,
                    ContractGroup = cboContractGroup.SelectedIndex,
                    AccountingContractTypeID = TextUtils.ToInt(cboAccountingContractType.EditValue),
                    CustomerID = TextUtils.ToInt(cboCustomer.EditValue),
                    SupplierSaleID = TextUtils.ToInt(cboSupplier.EditValue),
                    ContractNumber = txtContractNumber.Text.Trim(),
                    ContractValue = TextUtils.ToDecimal(contractValue),
                    DateExpired = dtpDateExpired.Value,
                    DateIsApprovedGroup = TextUtils.ToDate4(dtpDateIsApprovedGroup.EditValue),
                    EmployeeID = TextUtils.ToInt(cboEmployee.EditValue),
                    ContractContent = txtContractContent.Text.Trim(),
                    ContentPayment = txtContentPayment.Text.Trim(),
                    ParentID = TextUtils.ToInt(cboContract.EditValue),
                    DateContract = TextUtils.ToDate4(dtpDateContract.EditValue),
                    Unit = txtUnit.Text.Trim(),
                };
                //var isUpdate = !JSONEquals(contractCurrent, contractUpdate);
                var resultCompare = TextUtils.DeepEquals(contractCurrent, contractUpdate);
                bool equal = TextUtils.ToBoolean(resultCompare.GetType().GetProperty("equal").GetValue(resultCompare));

                if (!equal)
                {
                    //string property = TextUtils.ToString(resultCompare.GetType().GetProperty("property").GetValue(resultCompare));
                    //string propertyText = TextUtils.ToString(cGlobVar.accountingContract.GetType().GetProperty(property).GetValue(cGlobVar.accountingContract));
                    //if ((string.IsNullOrEmpty(txtNote.Text.Trim()) || txtNote.Text.Trim().ToLower() == contract.Note.Trim().ToLower()))
                    //{
                    //    MessageBox.Show($"Bạn vừa thay đổi thông tin [{propertyText}].\nVui lòng nhập Nội dung thay đổi!", "Thông báo");
                    //    return false;
                    //}

                    string propertyText = "";
                    List<string> propertys = (List<string>)resultCompare.GetType().GetProperty("property").GetValue(resultCompare);
                    foreach (string property in propertys)
                    {
                        propertyText += TextUtils.ToString(cGlobVar.accountingContract.GetType().GetProperty(property).GetValue(cGlobVar.accountingContract)) + "; ";
                    }

                    if ((string.IsNullOrEmpty(txtNote.Text.Trim()) || txtNote.Text.Trim().ToLower() == contract.Note.Trim().ToLower()))
                    {
                        MessageBox.Show($"Bạn vừa thay đổi thông tin [{propertyText}].\nVui lòng nhập Nội dung thay đổi!", "Thông báo");
                        return false;
                    }
                }
            }

            if (isReceivedContract && !TextUtils.ToDate4(dtpDateReceived.EditValue).HasValue)
            {
                MessageBox.Show("Vui lòng nhập Ngày trả hồ sơ gốc!", "Thông báo");
                return false;
            }

            if (isReceivedContract && txtQuantityDocument.Value <= 0)
            {
                MessageBox.Show("Vui lòng nhập SL hồ sơ!", "Thông báo");
                return false;
            }

            return true;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                contract = new AccountingContractModel();
                txtContractNumber.Clear();
                txtContractValue.EditValue = 0;
                txtContractContent.Clear();
                txtContentPayment.Clear();
                txtNote.Clear();
            }
        }

        private void btnAddType_Click(object sender, EventArgs e)
        {
            frmAccountingContractTypeDetail frm = new frmAccountingContractTypeDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadContractType();
                cboAccountingContractType.EditValue = frm.contractType.ID;
            }
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            //frmCustomerDetail frm = new frmCustomerDetail();
            frmCustomerDetailNew frm = new frmCustomerDetailNew(warehouseID);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadCustomer();
                cboCustomer.EditValue = frm.customer.ID;
            }
        }

        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            frmSupplierSaleDetail frm = new frmSupplierSaleDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadSupplier();
                cboSupplier.EditValue = frm.supplier.ID;
            }
        }

        private void cboContractGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboContractGroup.SelectedIndex == 1)
            {
                cboCustomer.Enabled = btnAddCustomer.Enabled = lblCustomerValidate.Visible = false;
                cboSupplier.Enabled = btnAddSupplier.Enabled = lblSupplierValidate.Visible = true;
            }
            else if (cboContractGroup.SelectedIndex == 2)
            {
                cboCustomer.Enabled = btnAddCustomer.Enabled = lblCustomerValidate.Visible = true;
                cboSupplier.Enabled = btnAddSupplier.Enabled = lblSupplierValidate.Visible = false;
            }
            else
            {
                cboCustomer.Enabled = btnAddCustomer.Enabled = lblCustomerValidate.Visible = true;
                cboSupplier.Enabled = btnAddSupplier.Enabled = lblSupplierValidate.Visible = true;
            }
        }

        private void frmAccountingContractDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnUploadFile_Click(object sender, EventArgs e)
        {
            UploadFile(contract.ID);
        }

        private void btnChosenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //List<AccountingContractFileModel> listFiles = new List<AccountingContractFileModel>();
                listFileUpload = dialog.FileNames;
                //var listSelected = dialog.FileNames;

                //var exp1 = new Expression("ID", contractID);
                //var exp2 = new Expression("ParentID", contractID);
                //var exp3 = new Expression("IsDelete", 1,"<>");

                //txtContractFile.AppendText("");
                txtContractFile.Text = string.Join(Environment.NewLine, dialog.FileNames);
                foreach (string file in dialog.FileNames)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    AccountingContractFileModel fileContract = new AccountingContractFileModel()
                    {
                        FileName = fileInfo.Name,
                        ServerPath = fileInfo.DirectoryName
                    };

                    listFiles.Insert(0, fileContract);
                }

                LoadFile(listFiles);
            }
        }

        //private void btnViewFile_Click(object sender, EventArgs e)
        //{
        //    string fileName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFileName));
        //    string filePath = TextUtils.ToString(grvData.GetFocusedRowCellValue(colServerPath));

        //    Process.Start(Path.Combine(filePath, fileName));
        //}

        //private void grdData_DoubleClick(object sender, EventArgs e)
        //{

        //}

        private void btnViewContractFile_Click(object sender, EventArgs e)
        {
            string fileName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFileName));
            string filePath = TextUtils.ToString(grvData.GetFocusedRowCellValue(colServerPath));
            if (string.IsNullOrEmpty(fileName))
            {
                return;
            }
            Process.Start(Path.Combine(filePath, fileName));
        }
    }
}
