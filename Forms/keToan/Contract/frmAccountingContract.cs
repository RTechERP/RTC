using BMS.Model;
using BMS.Utils;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmAccountingContract : _Forms
    {
        public frmAccountingContract()
        {
            InitializeComponent();
        }

        private void frmAccountingContract_Load(object sender, EventArgs e)
        {
            dtpDateStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDateEnd.Value = dtpDateStart.Value.AddMonths(+1).AddSeconds(-1);
            cboIsReceivedContract.SelectedIndex = 0;
            cboIsComingExpired.SelectedIndex = 0;

            LoadCustomer();
            LoadSupplier();
            LoadData();
        }

        void LoadCustomer()
        {
            //List<CustomerModel> list = SQLHelper<CustomerModel>.FindAll().OrderByDescending(x => x.ID).ToList();
            var exp1 = new Expression("IsDeleted", 1, "<>");
            var list = SQLHelper<CustomerModel>.FindByExpression(exp1).OrderByDescending(x => x.CreatedDate).ToList();
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

        void LoadData()
        {
            DateTime dateStart = new DateTime(dtpDateStart.Value.Year, dtpDateStart.Value.Month, dtpDateStart.Value.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(dtpDateEnd.Value.Year, dtpDateEnd.Value.Month, dtpDateEnd.Value.Day, 23, 59, 59);
            int company = 0;
            int group = 0;
            int type = 0;
            int customerId = TextUtils.ToInt(cboCustomer.EditValue);
            int supplierId = TextUtils.ToInt(cboSupplier.EditValue);
            int employeeId = 0;
            int isReceivedContract = cboIsReceivedContract.SelectedIndex - 1;
            int isComingExpired = cboIsComingExpired.SelectedIndex - 1;
            int pageNumber = TextUtils.ToInt(txtPageNumber.Text);
            int pageSize = TextUtils.ToInt(txtPageSize.Value);

            List<AccountingContractDTO> list = SQLHelper<AccountingContractDTO>.ProcedureToList("spGetAccountingContract",
                                                new string[]{ "@DateStart", "@DateEnd", "@Company", "@ContractGroup", "@AccountingContractTypeID", "@CustomerID", "@SupplierSaleID",
                                                    "@EmployeeID", "@IsReceivedContract","@IsComingExpired", "@Keyword", "@PageNumber", "@PageSize" },
                                                new object[] { dateStart, dateEnd, company, group, type, customerId, supplierId, employeeId, isReceivedContract, isComingExpired, txtKeyword.Text.Trim(), pageNumber, pageSize });

            tlData.DataSource = list;
            tlData.ExpandAll();

            if (list.Count > 0)
            {
                txtTotalPage.Text = list.FirstOrDefault().TotalPage.ToString();
            }
        }
        void LoadContractFile()
        {

            int accountingContractID = TextUtils.ToInt(tlData.GetFocusedRowCellValue(colID));
            List<AccountingContractFileDTO> list = SQLHelper<AccountingContractFileDTO>.ProcedureToList("spGetAccountingContractFile"
                                                                                        , new string[] { "@AccountingContractID" }
                                                                                        , new object[] { accountingContractID });
            grdData.DataSource = list;
        }

        bool CheckPermission(string createdBy)
        {
            return Global.LoginName.Trim() == createdBy.Trim() || Global.IsAdmin;
        }

        void Approved(bool isApproved)
        {
            List<int> listId = new List<int>();

            string isApprovedText = isApproved ? "duyệt" : "huỷ duyệt";
            var selectedNode = tlData.GetAllCheckedNodes();
            if (selectedNode.Count <= 0)
            {
                MessageBox.Show($"Vui lòng chọn hợp đồng muốn {isApprovedText}!", "Thông báo");
                return;
            }

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn {isApprovedText} hợp đồng đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                foreach (TreeListNode node in selectedNode)
                {
                    int id = TextUtils.ToInt(tlData.GetRowCellValue(node, colID));
                    if (id <= 0)
                    {
                        continue;
                    }

                    string approved = TextUtils.ToBoolean(tlData.GetRowCellValue(node, colIsApproved)) ? "Duyệt hợp đồng" : "Huỷ/Chưa duyệt hợp đồng";
                    listId.Add(id);

                    AccountingContractLogModel log = new AccountingContractLogModel();
                    log.AccountingContractID = id;
                    log.DateLog = DateTime.Now;
                    log.IsApproved = isApproved;
                    log.ContentLog = $"TÌNH TRẠNG HỢP ĐỒNG:\n" +
                                        $"từ {approved}\n" +
                                        $"thành {isApprovedText} hợp đồng";
                    SQLHelper<AccountingContractLogModel>.Insert(log);
                }

                int isApprovedValue = isApproved ? 1 : 0;
                string sql = $"UPDATE dbo.AccountingContract SET IsApproved = {isApprovedValue}, UpdatedDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                             $"UpdatedBy = '{Global.LoginName}' WHERE ID IN ({string.Join(",", listId)})";
                TextUtils.ExcuteSQL(sql);


                //Update log
            }
            LoadData();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboCustomer_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboSupplier_EditValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboIsReceivedContract_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) > TextUtils.ToInt(txtTotalPage.Text)) return;
            txtPageNumber.Text = "1";
            LoadData();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {

            if (TextUtils.ToInt(txtPageNumber.Text) == 1) return;
            txtPageNumber.Text = TextUtils.ToInt(txtPageNumber.Text) > 1 ? (TextUtils.ToInt(txtPageNumber.Text) - 1).ToString() : "1";
            LoadData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) >= TextUtils.ToInt(txtTotalPage.Text)) return;
            txtPageNumber.Text = (TextUtils.ToInt(txtPageNumber.Text) + 1).ToString();
            LoadData();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(txtPageNumber.Text) >= TextUtils.ToInt(txtTotalPage.Text)) return;
            txtPageNumber.Text = "" + txtTotalPage.Text;
            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAccountingContractDetail frm = new frmAccountingContractDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var rowhandled = tlData.FocusedNode;
            //int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            int id = TextUtils.ToInt(tlData.GetFocusedRowCellValue(colID));
            if (id <= 0)
            {
                return;
            }

            AccountingContractModel accounting = SQLHelper<AccountingContractModel>.FindByID(id);
            frmAccountingContractDetail frm = new frmAccountingContractDetail();
            frm.contract = accounting;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                //grvData.FocusedRowHandle = rowhandled;
                tlData.FocusedNode = rowhandled;
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            //int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            int id = TextUtils.ToInt(tlData.GetFocusedRowCellValue(colID));
            if (id <= 0)
            {
                return;
            }

            string createdBy = TextUtils.ToString(tlData.GetFocusedRowCellValue(colCreatedBy));
            if (!CheckPermission(createdBy))
            {
                MessageBox.Show($"Bạn không có quyền xoá!", "Thông báo");
                return;
            }

            string contractNumber = TextUtils.ToString(tlData.GetFocusedRowCellValue(colContractNumber));
            bool isApproved = TextUtils.ToBoolean(tlData.GetFocusedRowCellValue(colIsApproved));
            if (isApproved)
            {
                MessageBox.Show($"Hợp đồng [{contractNumber}] đã được duyệt.\nVui lòng huỷ duyệt trước!", "Thông báo");
                return;
            }

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá hợp đồng [{contractNumber}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                AccountingContractModel accounting = SQLHelper<AccountingContractModel>.FindByID(id);
                accounting.IsDelete = true;
                SQLHelper<AccountingContractModel>.Update(accounting);
                //grvData.DeleteSelectedRows();
                tlData.DeleteSelectedNodes();
            }
        }

        private void btnReceivedContract_Click(object sender, EventArgs e)
        {
            var rowhandled = tlData.FocusedNode;
            int id = TextUtils.ToInt(tlData.GetFocusedRowCellValue(colID));
            if (id <= 0)
            {
                return;
            }

            string contractNumber = TextUtils.ToString(tlData.GetFocusedRowCellValue(colContractNumber));
            bool isApproved = TextUtils.ToBoolean(tlData.GetFocusedRowCellValue(colIsApproved));
            if (isApproved)
            {
                MessageBox.Show($"Hợp đồng [{contractNumber}] đã được duyệt.\nVui lòng huỷ duyệt trước!", "Thông báo");
                return;
            }

            AccountingContractModel accounting = SQLHelper<AccountingContractModel>.FindByID(id);
            frmAccountingContractDetail frm = new frmAccountingContractDetail();
            frm.contract = accounting;
            frm.isReceivedContract = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                tlData.FocusedNode = rowhandled;
            }
        }

        private void btnCancelContract_Click(object sender, EventArgs e)
        {
            var rowhandled = tlData.FocusedNode;
            int id = TextUtils.ToInt(tlData.GetFocusedRowCellValue(colID));
            if (id <= 0)
            {
                return;
            }

            string contractNumber = TextUtils.ToString(tlData.GetFocusedRowCellValue(colContractNumber));
            bool isApproved = TextUtils.ToBoolean(tlData.GetFocusedRowCellValue(colIsApproved));
            if (isApproved)
            {
                MessageBox.Show($"Hợp đồng [{contractNumber}] đã được duyệt.\nVui lòng huỷ duyệt trước!", "Thông báo");
                return;
            }

            bool isReceivedContract = TextUtils.ToBoolean(tlData.GetFocusedRowCellValue(colIsReceivedContract));
            if (!isReceivedContract)
            {
                return;
            }

            //string contractNumber = TextUtils.ToString(tlData.GetFocusedRowCellValue(colContractNumber));
            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn huỷ nhận chứng từ hợp đồng [{contractNumber}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                AccountingContractModel accounting = SQLHelper<AccountingContractModel>.FindByID(id);

                //Update log
                AccountingContractLogModel log = new AccountingContractLogModel();
                log.AccountingContractID = accounting.ID;
                log.DateLog = DateTime.Now;
                log.IsReceivedContract = false;
                log.ContentLog = $"NGÀY TRẢ HỒ SƠ GỐC:\n" +
                                  $"từ {TextUtils.ToString(accounting.DateReceived.Value)}\n" +
                                  $"thành \n\n" +
                                  $"NHẬN CHỨNG TỪ GỐC:\n" +
                                  $"từ Đã nhận hồ sơ gốc\n" +
                                  $"thành Huỷ/Chưa nhận hồ sơ gốc";
                SQLHelper<AccountingContractLogModel>.Insert(log);

                //Update trạng thái nhận hồ sơ gốc
                accounting.DateReceived = null;
                accounting.IsReceivedContract = false;
                SQLHelper<AccountingContractModel>.Update(accounting);
            }

            LoadData();
            tlData.FocusedNode = rowhandled;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string filepath = Path.Combine(f.SelectedPath, $"TheoDoiHopDong_{dtpDateStart.Value.ToString("ddMMyyyy")}_{dtpDateEnd.Value.ToString("ddMMyyyy")}.xlsx");

                XlsxExportOptions optionsEx = new XlsxExportOptions();
                //optionsEx.ApplyFormattingToEntireColumn = DevExpress.Utils.DefaultBoolean.False;
                tlData.ExportToXlsx(filepath, optionsEx);
                Process.Start(filepath);

                //PrintingSystem printingSystem = new PrintingSystem();

                //PrintableComponentLink printableComponentLink1 = new PrintableComponentLink(printingSystem);
                //printableComponentLink1.Component = tlData;
                //try
                //{
                //    CompositeLink compositeLink = new CompositeLink(printingSystem);
                //    compositeLink.Links.Add(printableComponentLink1);

                //    compositeLink.CreatePageForEachLink();
                //    optionsEx.ExportMode = XlsxExportMode.SingleFilePageByPage;

                //    compositeLink.PrintingSystem.SaveDocument(filepath);
                //    compositeLink.ExportToXlsx(filepath, optionsEx);
                //    Process.Start(filepath);
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}
            }
        }

        //private void grvData_DoubleClick(object sender, EventArgs e)
        //{
        //    btnEdit_Click(null, null);
        //}

        //private void grvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        //{
        //    if (e.RowHandle >= 0)
        //    {
        //        int isComingExpired = TextUtils.ToInt(tlData.GetRowCellValue(e.RowHandle, colIsComingExpired));
        //        if (isComingExpired <= 0)
        //        {
        //            e.Appearance.BackColor = Color.Orange;
        //        }
        //    }
        //}

        private void tlData_DoubleClick(object sender, EventArgs e)
        {
            //string createdBy = TextUtils.ToString(tlData.GetFocusedRowCellValue(colCreatedBy));
            //if (!CheckPermission(createdBy))
            //{
            //    return;
            //}
            btnEdit_Click(null, null);
        }

        private void tlData_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            int isComingExpired = TextUtils.ToInt(tlData.GetRowCellValue(e.Node, colIsComingExpired));
            int typeContract = TextUtils.ToInt(tlData.GetRowCellValue(e.Node, colAccountingContractTypeID));
            if (isComingExpired == 1 && typeContract == 1) //CHỈ BÔI MÀU ĐỐI VỚI LOẠI HĐ = 1 (HĐ Nguyên tắc)
            {
                e.Appearance.BackColor = Color.Orange;
            }
        }

        private void btnReceivedContractContext_Click(object sender, EventArgs e)
        {
            btnReceivedContract_Click(null, null);
        }

        private void btnCancelContractContext_Click(object sender, EventArgs e)
        {
            btnCancelContract_Click(null, null);
        }

        private void btnContractLog_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(tlData.GetFocusedRowCellValue(colID));
            frmAccountingContractLog frm = new frmAccountingContractLog();
            frm.cboContract.EditValue = id;
            frm.Show();
        }

        private void btnContractFile_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(tlData.GetFocusedRowCellValue(colID));
            frmAccountingContractFile frm = new frmAccountingContractFile();
            frm.cboContract.EditValue = id;
            frm.Show();
        }

        private void btnTreeFolder_Click(object sender, EventArgs e)
        {
            //string pathUpload = @"\\192.168.1.190\Common\08. SOFTWARES\LeTheAnh\DemoContractAccounting";
            try
            {
                ConfigSystemModel config = SQLHelper<ConfigSystemModel>.FindByAttribute("KeyName", "PathAccounting").FirstOrDefault();
                string pathUpload = config != null ? $@"{config.KeyValue}" : "";
                if (string.IsNullOrEmpty(pathUpload))
                {
                    return;
                }

                Ping ping = new Ping();
                PingReply pingresult = ping.Send("192.168.1.190");
                pathUpload = pingresult.Status == IPStatus.Success ? config.KeyValue : config.KeyValue.Replace("192.168.1.190", "113.190.234.64");

                int id = TextUtils.ToInt(tlData.GetFocusedRowCellValue(colID));
                if (id <= 0)
                {
                    return;
                }
                AccountingContractModel contractModel = SQLHelper<AccountingContractModel>.ProcedureToList("spGetAccountingContractParent", new string[] { "@ID" }, new object[] { id }).FirstOrDefault();

                string company = TextUtils.ToString(tlData.GetFocusedRowCellValue(colCompanyName));
                string group = TextUtils.ToString(tlData.GetFocusedRowCellValue(colContractGroupText));
                string custormer = TextUtils.ToString(tlData.GetFocusedRowCellValue(colCustomerOrSupplier));
                string contractNumber = contractModel != null ? $@"\{contractModel.ContractNumber}" : "";

                string destFilename = $@"{company}\{group}\{custormer}{contractNumber}";
                destFilename = Path.Combine(pathUpload, destFilename);
                if (!Directory.Exists(destFilename))
                {
                    Directory.CreateDirectory(destFilename);
                }
                Process.Start(destFilename);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void btnUploadFile_Click(object sender, EventArgs e)
        {
            try
            {
                int contractID = TextUtils.ToInt(tlData.GetFocusedRowCellValue(colID));
                if (contractID <= 0)
                {
                    return;
                }
                bool isApproved = TextUtils.ToBoolean(tlData.GetFocusedRowCellValue(colIsApproved));
                string numberContract = TextUtils.ToString(tlData.GetFocusedRowCellValue(colContractNumber));
                if (isApproved)
                {
                    MessageBox.Show($"Hợp đồng [{numberContract}] đã được duyệt.\nBạn không thể upload file!", "Thông báo");
                    return;
                }

                //string pathUpload = @"\\192.168.1.190\Common\08. SOFTWARES\LeTheAnh\DemoContractAccounting";
                ConfigSystemModel config = SQLHelper<ConfigSystemModel>.FindByAttribute("KeyName", "PathAccounting").FirstOrDefault();
                string pathUpload = config != null ? $@"{config.KeyValue}" : "";

                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Multiselect = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var listSelected = dialog.FileNames;

                    AccountingContractModel contractModel = SQLHelper<AccountingContractModel>.ProcedureToList("spGetAccountingContractParent", new string[] { "@ID" }, new object[] { contractID }).FirstOrDefault();

                    string company = TextUtils.ToString(tlData.GetFocusedRowCellValue(colCompanyName)).ToUpper();
                    string group = TextUtils.ToString(tlData.GetFocusedRowCellValue(colContractGroupText)).ToUpper();
                    string custormer = TextUtils.ToString(tlData.GetFocusedRowCellValue(colCustomerOrSupplier));
                    string contractNumber = contractModel != null ? $@"\{contractModel.ContractNumber}" : "";
                    string destFilename = $@"{company}\{group}\{custormer}{contractNumber}";

                    destFilename = Path.Combine(pathUpload, destFilename);
                    if (!Directory.Exists(destFilename))
                    {
                        Directory.CreateDirectory(destFilename);
                    }

                    foreach (var file in listSelected)
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
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void btnApproved_Click(object sender, EventArgs e)
        {
            Approved(true);
        }

        private void btnUnApproved_Click(object sender, EventArgs e)
        {
            Approved(false);
        }

        private void btnApprovedContext_Click(object sender, EventArgs e)
        {
            btnApproved_Click(null, null);
        }

        private void btnUnApprovedContext_Click(object sender, EventArgs e)
        {
            btnUnApproved_Click(null, null);
        }

        private void tlData_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            string createdBy = TextUtils.ToString(tlData.GetFocusedRowCellValue(colCreatedBy));
            btnEdit.Enabled = btnDel.Enabled = CheckPermission(createdBy);
            LoadContractFile();
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            try
            {
                int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colContractID));
                if (id <= 0)
                {
                    return;
                }
                string path = TextUtils.ToString(grvData.GetFocusedRowCellValue(colServerPath));
                Process.Start(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }

        private void btnViewFile_Click(object sender, EventArgs e)
        {
            string fileName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFileName));
            string filePath = TextUtils.ToString(grvData.GetFocusedRowCellValue(colServerPath));
            if (string.IsNullOrEmpty(fileName))
            {
                return;
            }
            Process.Start(Path.Combine(filePath, fileName));
        }

        private void grvData_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string fileName = TextUtils.ToString(grvData.GetFocusedRowCellValue(colFileName));
                string filePath = TextUtils.ToString(grvData.GetFocusedRowCellValue(colServerPath));
                if (string.IsNullOrEmpty(fileName))
                {
                    return;
                }
                Process.Start(Path.Combine(filePath, fileName));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Thông báo");
            }
        }

        private void txtPageSize_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            var rowHandled = tlData.FocusedNode;
            int id = TextUtils.ToInt(tlData.GetFocusedRowCellValue(colID));
            if (id <= 0) return;

            AccountingContractModel ac = SQLHelper<AccountingContractModel>.FindByID(id);

            // tạo form mới gán dữ liệu
            frmAccountingContractDetail frm = new frmAccountingContractDetail();

            frm.contract.Company = ac.Company;
            frm.contract.ContractGroup = ac.ContractGroup;
            frm.contract.Unit = ac.Unit;
            frm.contract.AccountingContractTypeID = ac.AccountingContractTypeID;
            frm.contract.CustomerID = ac.CustomerID;
            frm.contract.SupplierSaleID = ac.SupplierSaleID;
            frm.contract.ContractValue = ac.ContractValue;
            frm.contract.DateIsApprovedGroup = ac.DateIsApprovedGroup;
            frm.contract.EmployeeID = ac.EmployeeID;
            frm.contract.ContractContent = ac.ContractContent;
            frm.contract.ContentPayment = ac.ContentPayment;
            frm.contract.Note = ac.Note;

            //frm.contract = ac;
            frm.IsCopy = true;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();

            }
        }

        private void btnCopyContext_Click(object sender, EventArgs e)
        {
            btnCopy_Click(null, null);
        }
    }
}
