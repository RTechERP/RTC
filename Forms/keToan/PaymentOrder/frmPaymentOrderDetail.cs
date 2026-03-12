using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmPaymentOrderDetail : _Forms
    {

        public PaymentOrderModel paymentOrder = new PaymentOrderModel();
        public int ponccID = 0;

        ArrayList lstIDDelete = new ArrayList();
        public bool isCopy = false;

        TreeListNode nodeI = new TreeListNode();
        TreeListNode nodeII = new TreeListNode();
        TreeListNode nodeIII = new TreeListNode();
        TreeListNode childII = new TreeListNode();
        TreeListNode childIII_1 = new TreeListNode();
        TreeListNode childIII_2 = new TreeListNode();
        //TreeListNode childIII_3 = new TreeListNode();
        DataTable treeListData1 = new DataTable();

        int preTypeOrder = 0;
        List<FileInfo> listFileUpload = new List<FileInfo>();
        List<PaymentOrderFileModel> listFiles = new List<PaymentOrderFileModel>();


        List<PaymentOrderFileModel> listFileDelete = new List<PaymentOrderFileModel>();


        List<int> _sameTypeOrders = new List<int>() { 1, 3 };
        public frmPaymentOrderDetail()
        {
            treeListData1.Columns.Add("Stt", typeof(string));
            treeListData1.Columns.Add("ContentPayment", typeof(string));
            treeListData1.Columns.Add("Unit", typeof(string));
            treeListData1.Columns.Add("Quantity", typeof(decimal));
            treeListData1.Columns.Add("UnitPrice", typeof(decimal));
            treeListData1.Columns.Add("TotalMoney", typeof(decimal));
            treeListData1.Columns.Add("TotalPaymentAmount", typeof(decimal));
            treeListData1.Columns.Add("Note", typeof(string));

            InitializeComponent();
        }
        public void frmPaymentOrderDetail_Load(object sender, EventArgs e)
        {
            txtEmployee.Text = Global.AppFullName;
            txtDepartment.Text = Global.DepartmentName;
            rdgTypePayment.SelectedIndex = 0;
            txtReceiverInfo.Text = txtEmployee.Text;
            loadTypeOrder();

            loadPaymentOrderType();
            loadApproved();
            loadSupplierSale();
            loadTypeBankTransfer();
            cboTypeBankTransfer.SelectedValue = 5;
            loadCurrency();
            cboCurrency.EditValue = 1;
            LoadEmployee();

            LoadProject();
            LoadData();


            
        }

        void LoadProject()
        {
            List<ProjectModel> list = SQLHelper<ProjectModel>.FindAll().OrderByDescending(x => x.ID).ToList();
            cboProject.Properties.ValueMember = "ID";
            cboProject.Properties.DisplayMember = "ProjectName";
            cboProject.Properties.DataSource = list;
        }

        public void loadTypeOrder()
        {
            List<object> list = new List<object>() {
                new {ID = 0,Name = "---Chọn loại---"},
                new {ID = 1,Name = "Đề nghị tạm ứng"},
                new {ID = 2,Name = "Đề nghị thanh toán"},
                new {ID = 3,Name = "Đề nghị thu tiền"},
            };
            cboTypeOrder.DataSource = list;
            cboTypeOrder.ValueMember = "ID";
            cboTypeOrder.DisplayMember = "Name";
        }
        public void loadTypeBankTransfer()
        {
            List<object> list = new List<object>()
            {
                new {ID = 1, Name = "Chuyển khoản RTC"},
                new {ID = 2, Name = "Chuyển khoản MVI"},
                new {ID = 3, Name = "Chuyển khoản APR"},
                new {ID = 4, Name = "Chuyển khoản Yonko"},
                new {ID = 5, Name = "Chuyển khoản cá nhân"}
            };
            cboTypeBankTransfer.DataSource = list;
            cboTypeBankTransfer.ValueMember = "ID";
            cboTypeBankTransfer.DisplayMember = "Name";
        }
        public void loadPaymentOrderType()
        {
            var exp = new Expression("IsDelete", 0);
            List<PaymentOrderTypeModel> list = SQLHelper<PaymentOrderTypeModel>.FindByExpression(exp);
            cboPaymentOrderType.Properties.DataSource = list;
            cboPaymentOrderType.Properties.ValueMember = "ID";
            cboPaymentOrderType.Properties.DisplayMember = "TypeName";
        }
        public void loadApproved()
        {
            var exp = new Expression("Type", 3);
            List<EmployeeApproveModel> list = SQLHelper<EmployeeApproveModel>.FindByExpression(exp);
            cboApproved.Properties.DataSource = list;
            cboApproved.Properties.ValueMember = "EmployeeID";
            cboApproved.Properties.DisplayMember = "FullName";
        }
        public void loadSupplierSale()
        {
            List<SupplierSaleModel> list = SQLHelper<SupplierSaleModel>.FindAll();
            cboSupplierSale.Properties.DataSource = list;
            cboSupplierSale.Properties.ValueMember = "ID";
            cboSupplierSale.Properties.DisplayMember = "NameNCC";
        }

        void LoadPONCC(int supplierId)
        {
            List<PONCCModel> list = SQLHelper<PONCCModel>.FindByAttribute("SupplierSaleID", supplierId);
            cboPONCC.Properties.DataSource = list;
            cboPONCC.Properties.ValueMember = "ID";
            //cboPONCC.Properties.DisplayMember = "POCode";
            cboPONCC.Properties.DisplayMember = "BillCode";
        }
        public void loadCurrency()
        {
            List<CurrencyModel> list = SQLHelper<CurrencyModel>.FindAll();
            cboCurrency.Properties.DataSource = list;
            cboCurrency.Properties.ValueMember = "ID";
            cboCurrency.Properties.DisplayMember = "Code";
        }

        void LoadEmployee()
        {
            DataTable list = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = list;
            cboEmployee.EditValue = Global.EmployeeID;
            //Lh phuc add new hiện Tên bộ phận
            if (paymentOrder.EmployeeID > 0)
            {
                DataRow rowList = list.AsEnumerable().FirstOrDefault(row => row.Field<int>("ID") == paymentOrder.EmployeeID);
                if (rowList != null)
                {
                    txtDepartment.Text = TextUtils.ToString(rowList["DepartmentName"]);
                }
            }
        }
        void LoadFile(List<PaymentOrderFileModel> listFiles)
        {
            grdData.DataSource = listFiles;
            grvData.RefreshData();
        }
        void loadTreeList(int index)
        {
            if (index == 2)
            {
                nodeI = treeListData.AppendNode(null, null);
                nodeI.SetValue(colStt, "I");
                nodeI.SetValue(colContentPayment, "Số tiền đã tạm ứng");

                nodeII = treeListData.AppendNode(null, null);
                nodeII.SetValue(colStt, "II");
                nodeII.SetValue(colContentPayment, "Số tiền thanh toán");

                nodeIII = treeListData.AppendNode(null, null);
                nodeIII.SetValue(colStt, "III");
                nodeIII.SetValue(colContentPayment, "Chênh lệch");

                childIII_1 = treeListData.AppendNode(null, nodeIII);
                childIII_1.SetValue(colStt, 1);
                childIII_1.SetValue(colContentPayment, "Tạm ứng chi không hết (I-II)");
                childIII_2 = treeListData.AppendNode(null, nodeIII);
                childIII_2.SetValue(colStt, 2);
                childIII_2.SetValue(colContentPayment, "Số chi quá tạm ứng (II-I)");
                //childIII_3 = treeListData.AppendNode(null, null);
                //childIII_3.SetValue(colStt, 3);
            }
            else
            {
                //treeListData.AppendNode(null, null).SetValue(colStt, 1);
                TreeListNode newNode = treeListData.AppendNode(null, null);
                newNode.SetValue(colStt, 1);
            }

        }

        // lh Phuc chỉnh sửa

        private void cboSupplierSale_EditValueChanged(object sender, EventArgs e)
        {
            //int id = TextUtils.ToInt(cboSupplierSale.EditValue);
            SupplierSaleModel supplier = (SupplierSaleModel)cboSupplierSale.GetSelectedDataRow();
            if (supplier == null) return;

            LoadPONCC(supplier.ID);
            txtReceiverInfo.Text = supplier.NameNCC;
            txtAccountNumber.Text = supplier.SoTK;
            txtBank.Text = supplier.NganHang;

        }

        private void ckIsUrgent_CheckedChanged(object sender, EventArgs e)
        {
            label29.Visible = ckIsUrgent.CheckState == CheckState.Checked ? true : false;
        }
        private void treeListBackup(int index)
        {
            treeListData1.Rows.Clear();

            if (index == 1)
            {
                //if(treeListData1.Rows.Count > 0)
                //{
                //    treeListData1.AcceptChanges();
                //}
                //else
                //{
                foreach (TreeListNode node in treeListData.Nodes)
                {
                    DataRow newRow = treeListData1.NewRow();

                    newRow["Stt"] = node.GetValue(colStt);
                    newRow["ContentPayment"] = node.GetValue(colContentPayment);
                    newRow["Unit"] = node.GetValue(colUnit);
                    newRow["Quantity"] = node.GetValue(colQuantity) == null ? 0 : node.GetValue(colQuantity);
                    newRow["UnitPrice"] = node.GetValue(colUnitPrice) == null ? 0 : node.GetValue(colUnitPrice);
                    newRow["TotalMoney"] = node.GetValue(colTotalMoney) == null ? 0 : node.GetValue(colTotalMoney);
                    newRow["TotalPaymentAmount"] = node.GetValue(colTotalPaymentAmount) == null ? 0 : node.GetValue(colTotalPaymentAmount);
                    newRow["Note"] = node.GetValue(colNote);

                    treeListData1.Rows.Add(newRow);
                }
                //}
            }
            else
            {
                //if (treeListData1.Rows.Count > 0)
                //{
                //    treeListData1.AcceptChanges();
                //}
                //else
                //{
                for (int i = 0; i < treeListData.AllNodesCount; i++)
                {
                    TreeListNode node = treeListData.GetNodeByVisibleIndex(i);
                    TreeListNode parentNode = treeListData.FindNodeByID(1);
                    if (node.ParentNode == parentNode)
                    {
                        DataRow newRow = treeListData1.NewRow();
                        newRow["Stt"] = node.GetValue(colStt);
                        newRow["ContentPayment"] = node.GetValue(colContentPayment);
                        newRow["Unit"] = node.GetValue(colUnit);
                        newRow["Quantity"] = node.GetValue(colQuantity) == null ? 0 : node.GetValue(colQuantity);
                        newRow["UnitPrice"] = node.GetValue(colUnitPrice) == null ? 0 : node.GetValue(colUnitPrice);
                        newRow["TotalMoney"] = node.GetValue(colTotalMoney) == null ? 0 : node.GetValue(colTotalMoney);
                        newRow["TotalPaymentAmount"] = node.GetValue(colTotalPaymentAmount) == null ? 0 : node.GetValue(colTotalPaymentAmount);
                        newRow["Note"] = node.GetValue(colNote);
                        treeListData1.Rows.Add(newRow);
                    }
                }
                //}

            }
        }
        private void setValueNewNode(DataTable treeListData1, TreeListNode node)
        {
            foreach (DataRow row in treeListData1.Rows)
            {
                TreeListNode newNode = treeListData.AppendNode(null, node);
                newNode.SetValue(colStt, row["Stt"]);
                newNode.SetValue(colContentPayment, row["ContentPayment"]);
                newNode.SetValue(colUnit, row["Unit"]);
                newNode.SetValue(colQuantity, row["Quantity"]);
                newNode.SetValue(colUnitPrice, row["UnitPrice"]);
                newNode.SetValue(colTotalMoney, row["TotalMoney"]);
                newNode.SetValue(colTotalPaymentAmount, row["TotalPaymentAmount"]);
                newNode.SetValue(colNote, row["Note"]);
            }
        }
        // lh Phuc chỉnh sửa new
        private void cboTypeOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            int typeOrder = TextUtils.ToInt(cboTypeOrder.SelectedValue);
            if (cboTypeOrder.SelectedIndex == preTypeOrder) return;
            if (typeOrder == 2)
            {
                dtpDatePayment.Enabled = false;
                treeListBackup(1);

                treeListData.ClearNodes();
                if (treeListData1.Rows.Count > 0)
                {
                    loadTreeList(2);
                    setValueNewNode(treeListData1, nodeII);
                }
                else
                {
                    loadTreeList(2);
                    childII = treeListData.AppendNode(null, nodeII);
                    childII.SetValue(colStt, 1);
                }
                treeListData.ExpandAll();
            }
            else if (_sameTypeOrders.Contains(typeOrder))
            {
                dtpDatePayment.Enabled = true;
                if (preTypeOrder == 0)
                {
                    treeListBackup(1);
                }
                else if (preTypeOrder == 2)
                {
                    treeListBackup(2);
                }

                treeListData.ClearNodes();
                if (treeListData1.Rows.Count > 0)
                {
                    setValueNewNode(treeListData1, null);
                }
                else
                {
                    loadTreeList(1);
                }
            }
            else
            {
                dtpDatePayment.Enabled = true;
                if (preTypeOrder == 1)
                {
                    treeListBackup(1);
                }
                else if (preTypeOrder == 2)
                {
                    treeListBackup(2);
                }

                treeListData.ClearNodes();
                if (treeListData1.Rows.Count > 0)
                {
                    setValueNewNode(treeListData1, null);
                }
                else
                {
                    loadTreeList(1);
                }
            }
            preTypeOrder = cboTypeOrder.SelectedIndex;
        }
        private void rdgTypePayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdgTypePayment.SelectedIndex == 1)
            {
                cboTypeBankTransfer.Enabled = false;
                txtBank.Enabled = false;
                txtAccountNumber.Enabled = false;
                txtContentBankTransfer.Enabled = false;
            }

            else
            {
                cboTypeBankTransfer.Enabled = true;
                txtBank.Enabled = true;
                txtAccountNumber.Enabled = true;
                txtContentBankTransfer.Enabled = true;
            }

        }
        void LoadData()
        {

            //PQ.Chien - UPDATE - 07/08/2025
            panel1.Visible = false;
            txtReasonOrder.Height = 130;
            txtReceiverInfo.Height = 130;
            //END

            if (ponccID > 0)
            {
                treeListData.ClearNodes();
                PONCCModel po = SQLHelper<PONCCModel>.FindByID(ponccID);
                cboSupplierSale.EditValue = po.SupplierSaleID;
                cboPONCC.EditValue = po.ID;
                cboApproved.EditValue = 178;
                txtAccountNumber.Text = po.AccountNumberSupplier;
                txtReceiverInfo.Text = po.AccountNumberSupplier;
                txtBank.Text = po.BankSupplier;

                SupplierSaleModel supplierSale = SQLHelper<SupplierSaleModel>.FindByID(TextUtils.ToInt(po.SupplierSaleID));
                txtReceiverInfo.Text = supplierSale.NameNCC;

                List<PONCCDetailModel> listDetail = SQLHelper<PONCCDetailModel>.FindByAttribute("PONCCID", ponccID);

                cboCurrency.EditValue = po.CurrencyID;
                foreach (PONCCDetailModel item in listDetail)
                {
                    if (item.ProductSaleID > 0)
                    {
                        ProductSaleModel product = SQLHelper<ProductSaleModel>.FindByID(TextUtils.ToInt(item.ProductSaleID));

                        TreeListNode newNode = treeListData.AppendNode(null, null);
                        newNode.SetValue(colStt, item.STT);
                        newNode.SetValue(colContentPayment, product.ProductName);
                        newNode.SetValue(colUnit, product.Unit);
                        newNode.SetValue(colQuantity, item.QtyRequest);
                        newNode.SetValue(colUnitPrice, item.UnitPrice);
                        newNode.SetValue(colTotalMoney, item.TotalPrice);
                        newNode.SetValue(colNote, item.Note);
                    }
                    else if (item.ProductRTCID > 0)
                    {
                        ProductRTCModel product = SQLHelper<ProductRTCModel>.FindByID(TextUtils.ToInt(item.ProductRTCID));
                        if (product != null)
                        {
                            UnitCountKTModel unit = SQLHelper<UnitCountKTModel>.FindByID(TextUtils.ToInt(product.UnitCountID));

                            TreeListNode newNode = treeListData.AppendNode(null, null);
                            newNode.SetValue(colStt, item.STT);
                            newNode.SetValue(colContentPayment, product.ProductName);
                            newNode.SetValue(colUnit, unit == null ? "" : unit.UnitCountName);
                            newNode.SetValue(colQuantity, item.QtyRequest);
                            newNode.SetValue(colUnitPrice, item.UnitPrice);
                            newNode.SetValue(colTotalMoney, item.TotalPrice);
                            newNode.SetValue(colNote, item.Note);
                        }
                    }
                }

            }
            else if (paymentOrder.ID > 0)
            {
                if (isCopy == false)
                {
                    var exp1 = new Expression("Step", 1, "<>");
                    var exp2 = new Expression("IsApproved", 1, ">");
                    var exp3 = new Expression("PaymentOrderID", paymentOrder.ID);
                    List<PaymentOrderLogModel> logPayment = SQLHelper<PaymentOrderLogModel>.FindByExpression(exp1.And(exp2).And(exp3));
                    if (logPayment.Count > 0 || paymentOrder.IsDelete == true)
                    {
                        btnSaveNew.Enabled = false;
                        btnSave.Enabled = false;
                    }

                    //Load ds file đính kèm
                    listFiles = SQLHelper<PaymentOrderFileModel>.FindByAttribute("PaymentOrderID", paymentOrder.ID);
                    grdData.DataSource = listFiles;
                }

                PaymentOrderLogModel paymentOrderLog = SQLHelper<PaymentOrderLogModel>.FindByAttribute("PaymentOrderID", paymentOrder.ID).Where(x => x.Step == 2).FirstOrDefault();
                cboTypeOrder.SelectedValue = paymentOrder.TypeOrder;
                cboPaymentOrderType.EditValue = paymentOrder.PaymentOrderTypeID;
                dtpDateOrder.Value = isCopy ? DateTime.Now : paymentOrder.DateOrder.Value;
                cboSupplierSale.EditValue = paymentOrder.SupplierSaleID;
                cboPONCC.EditValue = paymentOrder.PONCCID;
                ckIsUrgent.CheckState = paymentOrder.IsUrgent == true ? CheckState.Checked : CheckState.Unchecked;
                if (ckIsUrgent.CheckState == CheckState.Checked)
                {
                    dtpDeadlinePayment.EditValue = isCopy ? DateTime.Now : paymentOrder.DeadlinePayment;
                }

                txtReasonOrder.Text = paymentOrder.ReasonOrder;
                txtReceiverInfo.Text = paymentOrder.ReceiverInfo;
                rdgTypePayment.SelectedIndex = TextUtils.ToInt(paymentOrder.TypePayment) - 1;
                var currency = SQLHelper<CurrencyModel>.FindByAttribute("Code", TextUtils.ToString(paymentOrder.Unit)).FirstOrDefault();
                cboCurrency.EditValue = currency == null ? 0 : currency.ID;
                txtTotalMoneyText.Text = paymentOrder.TotalMoneyText;
                cboApproved.EditValue = paymentOrderLog.EmployeeID;
                cboEmployee.EditValue = paymentOrder.EmployeeID;
                cboProject.EditValue = paymentOrder.ProjectID;

                //PQ.Chien - update - 01/08/2025
                chkIsBill.CheckState = paymentOrder.IsBill == true ? CheckState.Checked : CheckState.Unchecked;
                txtStartLocation.Text = paymentOrder.StartLocation;
                txtEndLocation.Text = paymentOrder.EndLocation;

                //if (TextUtils.ToInt(cboPaymentOrderType.EditValue) == 22)
                //{
                //    panel1.Visible = true;
                //    txtReasonOrder.Height = 49;
                //    txtReceiverInfo.Height = 49;
                //}
                //else
                //{
                //    panel1.Visible = false;
                //    txtReasonOrder.Height = 130;
                //    txtReceiverInfo.Height = 130;
                //}
                //END

                if (_sameTypeOrders.Contains(TextUtils.ToInt(paymentOrder.TypeOrder)) /*paymentOrder.TypeOrder == 1*/)
                {
                    dtpDatePayment.EditValue = isCopy ? DateTime.Now : paymentOrder.DatePayment;
                    //if (!string.IsNullOrEmpty(TextUtils.ToString(paymentOrder.DatePayment)))

                }

                if (paymentOrder.TypePayment == 1)
                {
                    cboTypeBankTransfer.SelectedIndex = TextUtils.ToInt(paymentOrder.TypeBankTransfer) - 1;
                    txtAccountNumber.Text = paymentOrder.AccountNumber;
                    txtBank.Text = paymentOrder.Bank;
                    txtContentBankTransfer.Text = paymentOrder.ContentBankTransfer;
                }
                treeListData.ClearNodes();
                List<PaymentOrderDetailModel> list = SQLHelper<PaymentOrderDetailModel>.FindByAttribute("PaymentOrderID", paymentOrder.ID);



                //ĐH.Hoàng update 25/04/2024
                treeListData.CellValueChanged -= treeListData_CellValueChanged;
                if (_sameTypeOrders.Contains(TextUtils.ToInt(paymentOrder.TypeOrder)) /*paymentOrder.TypeOrder == 1*/)
                {
                    foreach (PaymentOrderDetailModel item in list)
                    {
                        item.ID = isCopy == true ? 0 : item.ID;

                        TreeListNode newNode = treeListData.AppendNode(null, null);
                        newNode.SetValue(colPODetailID, item.ID);
                        newNode.SetValue(colStt, item.STT);
                        newNode.SetValue(colContentPayment, item.ContentPayment);
                        newNode.SetValue(colUnit, item.Unit);
                        newNode.SetValue(colQuantity, item.Quantity);
                        newNode.SetValue(colUnitPrice, item.UnitPrice);
                        newNode.SetValue(colTotalMoney, item.TotalMoney);
                        newNode.SetValue(colTotalPaymentAmount, item.TotalPaymentAmount);
                        newNode.SetValue(colNote, item.Note);
                        newNode.SetValue(colParentID, item.ParentID);
                        newNode.SetValue(colPaymentPercentage, item.PaymentPercentage);
                    }
                }
                else if (paymentOrder.TypeOrder == 2)
                {
                    int parentID = 0;
                    bool dup = false;
                    foreach (PaymentOrderDetailModel item in list)
                    {
                        if (item.ParentID == 0)
                        {
                            if (item.STT == "I")
                            {
                                nodeI = treeListData.AppendNode(null, null);
                                nodeI.SetValue(colPODetailID, item.ID);
                                nodeI.SetValue(colStt, item.STT);
                                nodeI.SetValue(colContentPayment, item.ContentPayment);
                                nodeI.SetValue(colUnit, item.Unit);
                                nodeI.SetValue(colQuantity, item.Quantity);
                                nodeI.SetValue(colUnitPrice, item.UnitPrice);
                                nodeI.SetValue(colTotalMoney, item.TotalMoney);
                                nodeI.SetValue(colTotalPaymentAmount, item.TotalPaymentAmount);
                                nodeI.SetValue(colNote, item.Note);
                                nodeI.SetValue(colParentID, item.ParentID);
                                nodeI.SetValue(colPaymentPercentage, item.PaymentPercentage);
                            }
                            if (item.STT == "II")
                            {
                                nodeII = treeListData.AppendNode(null, null);
                                nodeII.SetValue(colPODetailID, item.ID);
                                nodeII.SetValue(colStt, item.STT);
                                nodeII.SetValue(colContentPayment, item.ContentPayment);
                                nodeII.SetValue(colUnit, item.Unit);
                                nodeII.SetValue(colQuantity, item.Quantity);
                                nodeII.SetValue(colUnitPrice, item.UnitPrice);
                                nodeII.SetValue(colTotalMoney, item.TotalMoney);
                                nodeII.SetValue(colTotalPaymentAmount, item.TotalPaymentAmount);
                                nodeII.SetValue(colNote, item.Note);
                                nodeII.SetValue(colParentID, item.ParentID);
                                //nodeII.SetValue(colPaymentPercentage, item.PaymentPercentage);
                            }
                            if (item.STT == "III")
                            {
                                nodeIII = treeListData.AppendNode(null, null);
                                nodeIII.SetValue(colPODetailID, item.ID);
                                nodeIII.SetValue(colStt, item.STT);
                                nodeIII.SetValue(colContentPayment, item.ContentPayment);
                                nodeIII.SetValue(colUnit, item.Unit);
                                nodeIII.SetValue(colQuantity, item.Quantity);
                                nodeIII.SetValue(colUnitPrice, item.UnitPrice);
                                nodeIII.SetValue(colTotalMoney, item.TotalMoney);
                                nodeIII.SetValue(colTotalPaymentAmount, item.TotalPaymentAmount);
                                nodeIII.SetValue(colNote, item.Note);
                                nodeIII.SetValue(colParentID, item.ParentID);

                                parentID = item.ID;
                            }
                        }
                        if (item.ParentID != 0)
                        {
                            if (item.ParentID == TextUtils.ToInt(nodeII.GetValue(0)))
                            {
                                TreeListNode childNode = treeListData.AppendNode(null, nodeII);
                                childNode.SetValue(colPODetailID, item.ID);
                                childNode.SetValue(colStt, item.STT);
                                childNode.SetValue(colContentPayment, item.ContentPayment);
                                childNode.SetValue(colUnit, item.Unit);
                                childNode.SetValue(colQuantity, item.Quantity);
                                childNode.SetValue(colUnitPrice, item.UnitPrice);
                                childNode.SetValue(colTotalMoney, item.TotalMoney);
                                childNode.SetValue(colTotalPaymentAmount, item.TotalPaymentAmount);
                                childNode.SetValue(colNote, item.Note);
                                childNode.SetValue(colParentID, item.ParentID);
                                childNode.SetValue(colPaymentPercentage, item.PaymentPercentage);

                            }
                            if (item.ParentID == parentID && dup == false)
                            {
                                childIII_1 = treeListData.AppendNode(null, nodeIII);
                                childIII_1.SetValue(colPODetailID, item.ID);
                                childIII_1.SetValue(colStt, item.STT);
                                childIII_1.SetValue(colContentPayment, item.ContentPayment);
                                childIII_1.SetValue(colUnit, item.Unit);
                                childIII_1.SetValue(colQuantity, item.Quantity);
                                childIII_1.SetValue(colUnitPrice, item.UnitPrice);
                                childIII_1.SetValue(colTotalMoney, item.TotalMoney);
                                childIII_1.SetValue(colTotalPaymentAmount, item.TotalPaymentAmount);
                                childIII_1.SetValue(colNote, item.Note);
                                childIII_1.SetValue(colParentID, item.ParentID);
                                dup = true;
                                continue;
                            }
                            if (item.ParentID == parentID)
                            {
                                childIII_2 = treeListData.AppendNode(null, nodeIII);
                                childIII_2.SetValue(colPODetailID, item.ID);
                                childIII_2.SetValue(colStt, item.STT);
                                childIII_2.SetValue(colContentPayment, item.ContentPayment);
                                childIII_2.SetValue(colUnit, item.Unit);
                                childIII_2.SetValue(colQuantity, item.Quantity);
                                childIII_2.SetValue(colUnitPrice, item.UnitPrice);
                                childIII_2.SetValue(colTotalMoney, item.TotalMoney);
                                childIII_2.SetValue(colTotalPaymentAmount, item.TotalPaymentAmount);
                                childIII_2.SetValue(colNote, item.Note);
                                childIII_2.SetValue(colParentID, item.ParentID);
                            }
                        }
                    }
                }

                treeListData.ExpandAll();
                treeListData.CellValueChanged += treeListData_CellValueChanged;

                //paymentOrder.ID = 0;
                //treeListData.CellValueChanged -= treeListData_CellValueChanged;
                //foreach (PaymentOrderDetailModel item in list)
                //{
                //    if (item.ParentID == 0)
                //    {
                //        TreeListNode newNode = treeListData.AppendNode(null, null);
                //        newNode.SetValue(colPODetailID, item.ID);
                //        newNode.SetValue(colStt, item.STT);
                //        newNode.SetValue(colContentPayment, item.ContentPayment);
                //        newNode.SetValue(colUnit, item.Unit);
                //        newNode.SetValue(colQuantity, item.Quantity);
                //        newNode.SetValue(colUnitPrice, item.UnitPrice);
                //        newNode.SetValue(colTotalMoney, item.TotalMoney);
                //        newNode.SetValue(colNote, item.Note);
                //        newNode.SetValue(colParentID, item.ParentID);
                //    }
                //    if (item.ParentID != 0)
                //    {
                //        TreeListNode parentNode = treeListData.FindNodeByFieldValue("ID", item.ParentID);
                //        TreeListNode childNode = treeListData.AppendNode(null, parentNode);
                //        childNode.SetValue(colPODetailID, item.ID);
                //        childNode.SetValue(colStt, item.STT);
                //        childNode.SetValue(colContentPayment, item.ContentPayment);
                //        childNode.SetValue(colUnit, item.Unit);
                //        childNode.SetValue(colQuantity, item.Quantity);
                //        childNode.SetValue(colUnitPrice, item.UnitPrice);
                //        childNode.SetValue(colTotalMoney, item.TotalMoney);
                //        childNode.SetValue(colNote, item.Note);
                //        childNode.SetValue(colParentID, item.ParentID);
                //    }
                //}

                //treeListData.ExpandAll();
            }
        }
        bool SaveData()
        {
            treeListData.CloseEditor();
            if (!CheckValidate()) return false;
            paymentOrder.ID = isCopy == true ? 0 : paymentOrder.ID;    // lh Phuc add new


            paymentOrder.TypeOrder = TextUtils.ToInt(cboTypeOrder.SelectedValue);
            paymentOrder.PaymentOrderTypeID = TextUtils.ToInt(cboPaymentOrderType.EditValue);
            paymentOrder.DateOrder = TextUtils.ToDate5(dtpDateOrder.Value);
            if (ckIsUrgent.CheckState == CheckState.Checked) paymentOrder.DeadlinePayment = TextUtils.ToDate5(dtpDeadlinePayment.EditValue);
            paymentOrder.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
            paymentOrder.SupplierSaleID = TextUtils.ToInt(cboSupplierSale.EditValue);
            paymentOrder.PONCCID = TextUtils.ToInt(cboPONCC.EditValue);
            //paymentOrder.IsUrgent = ckIsUrgent.CheckState == CheckState.Checked ? true : false;
            paymentOrder.IsUrgent = TextUtils.ToBoolean(ckIsUrgent.CheckState);
            paymentOrder.ReasonOrder = TextUtils.ToString(txtReasonOrder.Text);
            paymentOrder.ReceiverInfo = TextUtils.ToString(txtReceiverInfo.Text);
            paymentOrder.TypePayment = rdgTypePayment.SelectedIndex == 1 ? 2 : 1;
            if (cboTypeOrder.SelectedIndex == 1)
            {
                paymentOrder.DatePayment = TextUtils.ToDate5(dtpDatePayment.EditValue);
            }
            paymentOrder.TypeBankTransfer = TextUtils.ToInt(cboTypeBankTransfer.SelectedValue);
            paymentOrder.AccountNumber = txtAccountNumber.Text;
            paymentOrder.Bank = txtBank.Text;
            paymentOrder.ContentBankTransfer = txtContentBankTransfer.Text;
            paymentOrder.Unit = TextUtils.ToInt(cboCurrency.EditValue) != -1 ? cboCurrency.Text : "vnd";

            if (paymentOrder.ID <= 0)
            {
                paymentOrder.Code = CreateCode(paymentOrder);
            }
            var nodeChenhLech = treeListData.FindNodeByFieldValue(colStt.FieldName, "III");
            decimal valueChenhLech1 = TextUtils.ToDecimal(treeListData.GetRowCellValue(nodeChenhLech, colTotalMoney));
            decimal valueChenhLech2 = TextUtils.ToDecimal(treeListData.GetRowCellValue(nodeChenhLech, colTotalPaymentAmount));
            decimal totalMoney1 = _sameTypeOrders.Contains(TextUtils.ToInt(paymentOrder.TypeOrder))/* paymentOrder.TypeOrder == 1*/ ? TextUtils.ToDecimal(treeListData.GetSummaryValue(colTotalMoney)) : valueChenhLech1;
            decimal totalMoney2 = _sameTypeOrders.Contains(TextUtils.ToInt(paymentOrder.TypeOrder))/* paymentOrder.TypeOrder == 1*/ ? TextUtils.ToDecimal(treeListData.GetSummaryValue(colTotalPaymentAmount)) : valueChenhLech2;
            //paymentOrder.TotalMoney = totalMoney;
            paymentOrder.TotalMoney = totalMoney2 <= 0 ? totalMoney1 : totalMoney2;
            paymentOrder.TotalMoneyText = NumberMoneyToText.ConvertNumberToTextVietNamese(TextUtils.ToDecimal(paymentOrder.TotalMoney), cboCurrency.Text.ToUpper());
            paymentOrder.ProjectID = TextUtils.ToInt(cboProject.EditValue);

            //PQ.Chien - update - 01/08/2025
            paymentOrder.IsBill = TextUtils.ToBoolean(chkIsBill.CheckState);
            paymentOrder.StartLocation = TextUtils.ToString(txtStartLocation.Text);
            paymentOrder.EndLocation = TextUtils.ToString(txtEndLocation.Text);
            //END

            if (paymentOrder.ID > 0)
            {
                //PaymentOrderBO.Instance.Update(paymentOrder);
                SQLHelper<PaymentOrderModel>.Update(paymentOrder);
            }
            else
            {
                paymentOrder.AccountingNote = "";
                //paymentOrder.ID = (int)PaymentOrderBO.Instance.Insert(paymentOrder);
                paymentOrder.ID = SQLHelper<PaymentOrderModel>.Insert(paymentOrder).ID;
            }

            List<TreeListNode> ListChildNode = new List<TreeListNode>();

            for (int i = 0; i < treeListData.AllNodesCount; i++)
            {
                TreeListNode nodeDetail = treeListData.GetNodeByVisibleIndex(i);
                int id = TextUtils.ToInt(treeListData.GetRowCellValue(nodeDetail, colPODetailID));

                id = isCopy == true ? 0 : id;
                if (ListChildNode.Contains(nodeDetail)) continue;
                PaymentOrderDetailModel detail = new PaymentOrderDetailModel();

                if (id > 0)
                {
                    detail = SQLHelper<PaymentOrderDetailModel>.FindByID(id);
                }

                detail.PaymentOrderID = paymentOrder.ID;
                detail.STT = TextUtils.ToString(treeListData.GetRowCellValue(nodeDetail, colStt));
                detail.ContentPayment = TextUtils.ToString(treeListData.GetRowCellValue(nodeDetail, colContentPayment));
                detail.Unit = TextUtils.ToString(treeListData.GetRowCellValue(nodeDetail, colUnit));
                detail.Quantity = TextUtils.ToDecimal(treeListData.GetRowCellValue(nodeDetail, colQuantity));
                detail.UnitPrice = TextUtils.ToDecimal(treeListData.GetRowCellValue(nodeDetail, colUnitPrice));
                detail.TotalMoney = TextUtils.ToDecimal(treeListData.GetRowCellValue(nodeDetail, colTotalMoney));
                detail.TotalPaymentAmount = TextUtils.ToDecimal(treeListData.GetRowCellValue(nodeDetail, colTotalPaymentAmount));
                detail.Note = TextUtils.ToString(treeListData.GetRowCellValue(nodeDetail, colNote));

                detail.PaymentPercentage = TextUtils.ToDecimal(treeListData.GetRowCellValue(nodeDetail, colPaymentPercentage));

                if (detail.PaymentPercentage > 0)
                {
                    detail.TotalPaymentAmount = (detail.TotalMoney * detail.PaymentPercentage) / 100;
                }
                //if (isCopy) detail.ID = 0;
                //detail.ID = isCopy == true ? 0 : detail.ID;

                if (detail.ID > 0)
                {
                    //PaymentOrderDetailBO.Instance.Update(detail);
                    SQLHelper<PaymentOrderDetailModel>.Update(detail);
                }
                else
                {
                    //detail.ID = TextUtils.ToInt(PaymentOrderDetailBO.Instance.Insert(detail));
                    detail.ID = SQLHelper<PaymentOrderDetailModel>.Insert(detail).ID;
                }

                var childNode = treeListData.FindNodes((node) =>
                {
                    return node.ParentNode == nodeDetail;
                }
                );
                if (childNode.Count() > 0)
                {
                    //ChildNode 
                    foreach (TreeListNode item in childNode)
                    {
                        ListChildNode.Add(item);
                        int idChild = TextUtils.ToInt(treeListData.GetRowCellValue(item, colPODetailID));
                        PaymentOrderDetailModel detailChild = new PaymentOrderDetailModel();
                        idChild = isCopy == true ? 0 : idChild;

                        if (idChild > 0)
                        {
                            detailChild = (PaymentOrderDetailModel)(PaymentOrderDetailBO.Instance.FindByPK(idChild));
                        }
                        detailChild.PaymentOrderID = paymentOrder.ID;
                        detailChild.STT = TextUtils.ToString(treeListData.GetRowCellValue(item, colStt));
                        detailChild.ContentPayment = TextUtils.ToString(treeListData.GetRowCellValue(item, colContentPayment));
                        detailChild.Unit = TextUtils.ToString(treeListData.GetRowCellValue(item, colUnit));
                        detailChild.Quantity = TextUtils.ToDecimal(treeListData.GetRowCellValue(item, colQuantity));
                        detailChild.UnitPrice = TextUtils.ToDecimal(treeListData.GetRowCellValue(item, colUnitPrice));
                        detailChild.TotalMoney = TextUtils.ToDecimal(treeListData.GetRowCellValue(item, colTotalMoney));
                        detailChild.TotalPaymentAmount = TextUtils.ToDecimal(treeListData.GetRowCellValue(item, colTotalPaymentAmount));
                        detailChild.Note = TextUtils.ToString(treeListData.GetRowCellValue(item, colNote));
                        detailChild.ParentID = detail.ID;

                        detailChild.PaymentPercentage = TextUtils.ToDecimal(treeListData.GetRowCellValue(item, colPaymentPercentage));

                        if (detailChild.PaymentPercentage > 0)
                        {
                            detailChild.TotalPaymentAmount = (detailChild.TotalMoney * detailChild.PaymentPercentage) / 100;
                        }
                        //if (isCopy) detailChild.ID = 0;
                        //detailChild.ID = isCopy == true ? 0 : detailChild.ID;
                        if (detailChild.ID > 0)
                        {
                            //PaymentOrderDetailBO.Instance.Update(detailChild);
                            SQLHelper<PaymentOrderDetailModel>.Update(detailChild);
                        }
                        else
                        {
                            //detailChild.ID = TextUtils.ToInt(PaymentOrderDetailBO.Instance.Insert(detailChild));
                            detailChild.ID = SQLHelper<PaymentOrderDetailModel>.Insert(detailChild).ID;
                        }

                    }
                }
            }
            if (lstIDDelete.Count > 0)
            {
                PaymentOrderDetailBO.Instance.Delete(lstIDDelete);
                //SQLHelper<PaymentOrderDetailModel>.DE(detailChild).ID;
            }
            CreateApprove(TextUtils.ToInt(cboApproved.EditValue), paymentOrder);
            UploadFile(paymentOrder.ID);
            RemoveFile();
            return true;
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


                if (type.TypeCode.Trim().ToLower() == "com")//Loại thanh toán đặc biệt
                {
                    listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 1, StepName = "Đề nghị thanh toán", DateApproved = DateTime.Now, IsApproved = 1, EmployeeID = Global.EmployeeID, EmployeeApproveActualID = Global.EmployeeID, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                    listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 2, StepName = "Trưởng bộ phận xác nhận", DateApproved = null, IsApproved = 0, EmployeeID = approvedTBPID, EmployeeApproveActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                    listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 3, StepName = "Kế toán trưởng xác nhận", DateApproved = null, IsApproved = 0, EmployeeID = headOfKT, EmployeeApproveActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                    listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 4, StepName = "Ban giám đốc xác nhận", DateApproved = null, IsApproved = 0, EmployeeID = 1, EmployeeApproveActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                    listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 5, StepName = "Nhận chứng từ", DateApproved = null, IsApproved = 0, EmployeeID = 0, EmployeeApproveActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
                    listLog.Add(new PaymentOrderLogModel() { PaymentOrderID = paymentOrder.ID, Step = 6, StepName = "Kế toán thanh toán", DateApproved = null, IsApproved = 0, EmployeeID = 0, EmployeeApproveActualID = 0, CreatedBy = Global.LoginName, CreatedDate = DateTime.Now, UpdatedBy = Global.LoginName, UpdatedDate = DateTime.Now });
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
                //var tbpApproved = SQLHelper<EmployeeApproveModel>.FindAll().Where(x => x.EmployeeID == Global.EmployeeID && x.Type == 3).ToList();
                //if (tbpApproved.Count > 0)
                //{
                //    var logTBP = listLog.FirstOrDefault(x => x.Step == 2);
                //    if (logTBP != null)
                //    {
                //        logTBP.IsApproved = 1;
                //        logTBP.EmployeeID = Global.EmployeeID;
                //        logTBP.EmployeeApproveActualID = Global.EmployeeID;
                //        logTBP.DateApproved = DateTime.Now;
                //    }
                //}
                foreach (PaymentOrderLogModel item in listLog)
                {
                    SQLHelper<PaymentOrderLogModel>.Insert(item);
                }


                //Add Notify
                string dealine = paymentOrder.DeadlinePayment.HasValue ? paymentOrder.DeadlinePayment.Value.ToString("dd/MM/yyyy HH:mm") : "";
                string textNotify = $"Yêu cầu duyệt đề nghị thanh toán.\n" + $"Số đề nghị: {paymentOrder.Code}\n" + $"Deadline: {dealine}";
                if (paymentOrder.PaymentOrderTypeID == 26)
                {
                    TextUtils.AddNotify("ĐỀ NGHỊ THANH TOÁN Chi phí thuế / Hải quan/ lệ phí".ToUpper(), textNotify, 330);

                }
                else
                {
                    TextUtils.AddNotify("ĐỀ NGHỊ THANH TOÁN".ToUpper(), textNotify, approvedTBPID);
                }

                ////Add Notify
                //string text = $"Yêu cầu duyệt đề nghị.\n" +
                //                $"Số: {paymentOrder.Code}";
                //notifyRepo.AddNotify("ĐỀ NGHỊ THANH TOÁN", text, paymentOrder.ApprovedTBPID);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                code = paymentOrder.TypeOrder == 1 ? $"ĐNTU{paymentOrder.DateOrder.Value.ToString("yyyyMMdd")}0001" : $"ĐNTT{paymentOrder.DateOrder.Value.ToString("yyyyMMdd")}0001";
                return code;
            }

            int sttOrder = TextUtils.ToInt(code.Substring(code.Length - 4)) + 1;
            string sttText = sttOrder.ToString();
            for (int i = 0; sttText.Length < 4; i++)
            {
                sttText = "0" + sttText;
            }

            code = paymentOrder.TypeOrder == 1 ? $"ĐNTU{paymentOrder.DateOrder.Value.ToString("yyyyMMdd")}{sttText}" : $"ĐNTT{paymentOrder.DateOrder.Value.ToString("yyyyMMdd")}{sttText}";
            return code;
        }
        private bool CheckValidate()
        {
            // lh Phuc add new
            if (cboTypeOrder.SelectedIndex <= 0)
            {
                MessageBox.Show("Vui lòng chọn Loại đề nghị", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (TextUtils.ToInt(cboPaymentOrderType.EditValue) < 1)
            {
                MessageBox.Show("Vui lòng chọn Loại nội dung đề nghị", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(TextUtils.ToString(dtpDateOrder.Value)))
            {
                MessageBox.Show("Vui lòng chọn Ngày đề nghị", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(TextUtils.ToString(cboApproved.EditValue)))
            {
                MessageBox.Show("Vui lòng chọn TBP duyệt", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (ckIsUrgent.CheckState == CheckState.Checked && string.IsNullOrEmpty(TextUtils.ToString(dtpDeadlinePayment.EditValue)))
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
            if (cboTypeOrder.SelectedIndex == 1 && string.IsNullOrEmpty(TextUtils.ToString(dtpDatePayment.EditValue)))
            {
                MessageBox.Show("Vui lòng chọn Thời gian thanh quyết toán", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (rdgTypePayment.SelectedIndex == 0)
            {
                if (cboTypeBankTransfer.SelectedIndex < 0)
                {
                    MessageBox.Show("Vui lòng chọn Hình thức chuyển khoản", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (string.IsNullOrEmpty(txtAccountNumber.Text))
                {
                    MessageBox.Show("Vui lòng nhập Số tài khoản", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (string.IsNullOrEmpty(txtBank.Text))
                {
                    MessageBox.Show("Vui lòng nhập Ngân hàng", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (string.IsNullOrEmpty(txtContentBankTransfer.Text))
                {
                    MessageBox.Show("Vui lòng nhập Nội dung chuyển khoản", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            //PQ.Chien - update - 01/08/2025
            if(TextUtils.ToInt(cboPaymentOrderType.EditValue) == 22) //Nếu là chi phí phương tiện
            {
                if(string.IsNullOrEmpty(txtStartLocation.Text))
                {
                    MessageBox.Show("Vui lòng nhập Điểm đi", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (string.IsNullOrEmpty(txtEndLocation.Text))
                {
                    MessageBox.Show("Vui lòng nhập Điểm đến", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            //END
            for (int i = 0; i < treeListData.AllNodesCount; i++)
            {
                TreeListNode node = treeListData.FindNodeByID(i);
                if (cboTypeOrder.SelectedIndex == 1)
                {
                    if (node[colContentPayment] == null)
                    {
                        MessageBox.Show("Vui lòng nhập Nội dung thanh toán", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    if (node[colTotalMoney] == null)
                    {
                        MessageBox.Show("Vui lòng nhập Thành tiền ", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    if (node[colTotalPaymentAmount] == null)
                    {
                        MessageBox.Show("Vui lòng nhập Tổng tiền thanh toán!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }

                else if (node.ParentNode == nodeII)
                {
                    if (node[colContentPayment] == null)
                    {
                        MessageBox.Show("Vui lòng nhập Nội dung thanh toán", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    if (node[colTotalMoney] == null)
                    {
                        MessageBox.Show("Vui lòng nhập Thành tiền ", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            //end

            //Check đã tạo đề nghị cho PO chưa
            int poID = TextUtils.ToInt(cboPONCC.EditValue);
            if (poID > 0)
            {
                var nodeChenhLech = treeListData.FindNodeByFieldValue(colStt.FieldName, "III");
                decimal valueChenhLech = TextUtils.ToDecimal(treeListData.GetRowCellValue(nodeChenhLech, colTotalMoney));
                decimal totalMoney = cboTypeOrder.SelectedIndex == 1 ? TextUtils.ToDecimal(treeListData.GetSummaryValue(colTotalMoney)) : valueChenhLech;


                PONCCModel poNCC = SQLHelper<PONCCModel>.FindByID(poID);

                decimal totalMoneyPO = poNCC == null ? 0 : TextUtils.ToDecimal(poNCC.TotalMoneyPO);

                //var listPaymentPO = SQLHelper<PaymentOrderModel>.FindByAttribute("PONCCID", poID);
                //decimal totalPayment = listPaymentPO.Sum(x => x.TotalMoney) + totalMoney;
                decimal totalPayment = TextUtils.ToDecimal(TextUtils.ExcuteScalar($"EXEC dbo.spGetPaymentOrderByPONCCID @PONCCID = {poID},@ID = {paymentOrder.ID}"));
                totalPayment += totalMoney;

                if (totalPayment > totalMoneyPO)
                {
                    MessageBox.Show("Tổng số tiền đề nghị thanh toán vượt quá tổng tiền PO.\nVui lòng kiểm tra lại!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }


            int[] orderTypeSelectProjects = new int[] { 19, 22 };
            int orderType = TextUtils.ToInt(cboPaymentOrderType.EditValue);
            if (orderTypeSelectProjects.Contains(orderType) && TextUtils.ToInt(cboProject.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng nhập Dự án!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        // // lh Phuc add new
        private void RecalculateSTT()
        {
            TreeListNode nodeII = null;

            // Find the node with value "II"
            foreach (TreeListNode node in treeListData.Nodes)
            {
                if (node.GetValue(colStt)?.ToString() == "II")
                {
                    nodeII = node;
                    break;
                }
            }

            if (nodeII != null)
            {
                int stt = 1;
                foreach (TreeListNode childNode in nodeII.Nodes)
                {
                    childNode.SetValue(colStt, stt++);
                    RecalculateChildSTT(childNode, ref stt);
                }
            }
        }

        private void RecalculateChildSTT(TreeListNode parentNode, ref int stt)
        {
            foreach (TreeListNode node in parentNode.Nodes)
            {
                node.SetValue(colStt, stt++);
                RecalculateChildSTT(node, ref stt);
            }
        }
        private void treeListData_MouseDown(object sender, MouseEventArgs e)
        {
            TreeListHitInfo hitInfo = treeListData.CalcHitInfo(e.Location);
            if (hitInfo.HitInfoType == HitInfoType.Column && hitInfo.Column == colAdd)
            {
                //int STT;
                //TreeListNode node = new TreeListNode();
                //if (TextUtils.ToInt(cboTypeOrder.SelectedValue) == 2)
                //{
                //    TreeListNode rootNode = treeListData.Nodes[1];
                //    int childCount = rootNode.Nodes.Count;
                //    STT = childCount + 1;
                //    node = treeListData.AppendNode(new object[] { }, rootNode);
                //    node[colStt] = STT;
                //    treeListData.ExpandAll();
                //}
                //else
                //{
                //    TreeList tl = (TreeList)treeListData;
                //    STT = TextUtils.ToInt(tl.GetRowCellValue(tl.Nodes.LastNode, colStt)) + 1;
                //    node = treeListData.AppendNode(new object[] { }, null);
                //    node[colStt] = STT;
                //}

                int STT;
                TreeListNode node = new TreeListNode();
                if (TextUtils.ToInt(cboTypeOrder.SelectedValue) == 2)
                {
                    TreeListNode rootNode = treeListData.Nodes[1];
                    int childCount = rootNode.Nodes.Count;
                    STT = childCount + 1;
                    node = treeListData.AppendNode(new object[] { }, rootNode);
                    node[colStt] = STT;
                }
                else
                {
                    TreeList tl = (TreeList)treeListData;
                    TreeListNode lastNode = tl.Nodes.LastNode;
                    STT = lastNode != null ? TextUtils.ToInt(lastNode.GetValue(colStt)) + 1 : 1;
                    node = treeListData.AppendNode(new object[] { }, null);
                    node[colStt] = STT;
                }
                treeListData.ExpandAll();
                treeListData.FocusedNode = node;

                RecalculateSTT();
            }
        }
        // end
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }
        private void treeListData_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            treeListData.CloseEditor();
            TreeListNode node = e.Node;
            decimal unitPrice = TextUtils.ToDecimal(treeListData.GetFocusedRowCellValue(colUnitPrice));
            decimal qty = TextUtils.ToDecimal(treeListData.GetFocusedRowCellValue(colQuantity));
            decimal totalMoney = unitPrice * qty;
            try
            {
                if (unitPrice >= 0 && qty > 0 && ponccID <= 0)
                {
                    if (e.Column == colQuantity || e.Column == colUnitPrice)
                    {
                        treeListData.SetFocusedRowCellValue(colTotalMoney, totalMoney);
                    }
                }


                if (e.Column == colPaymentPercentage || e.Column == colTotalMoney)
                {
                    decimal percent = TextUtils.ToDecimal(treeListData.GetFocusedRowCellValue(colPaymentPercentage));
                    totalMoney = TextUtils.ToDecimal(treeListData.GetFocusedRowCellValue(colTotalMoney));
                    decimal totalAmout = (totalMoney * percent) / 100;
                    treeListData.SetFocusedRowCellValue(colTotalPaymentAmount, totalAmout);
                }




                UpdateParentNodeTotalMoney(node);
                if (TextUtils.ToInt(cboCurrency.EditValue) > 0)
                {
                    decimal totalMoney1 = TextUtils.ToDecimal(treeListData.GetSummaryValue(colTotalMoney));
                    decimal totalMoney2 = TextUtils.ToDecimal(treeListData.GetSummaryValue(colTotalPaymentAmount));
                    totalMoney = totalMoney2 <= 0 ? totalMoney1 : totalMoney2;
                    txtTotalMoneyText.Text = NumberMoneyToText.ConvertNumberToTextVietNamese(totalMoney, cboCurrency.Text.ToUpper());
                }
                if (cboTypeOrder.SelectedIndex == 2)
                {
                    calTotalMoneyChildNode();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }
        private void UpdateParentNodeTotalMoney(TreeListNode node)
        {
            if (node == null)
                return;
            TreeListNode parentNode = node.ParentNode;
            decimal totalMoneySum = 0;
            decimal totalPaymentAmount = 0;
            if (parentNode != null)
            {
                foreach (TreeListNode childNode in parentNode.Nodes)
                {
                    decimal childTotalMoney = TextUtils.ToDecimal(childNode.GetValue(colTotalMoney));
                    totalMoneySum += childTotalMoney;

                    decimal childTotalPaymentAmount = TextUtils.ToDecimal(childNode.GetValue(colTotalPaymentAmount));
                    totalPaymentAmount += childTotalPaymentAmount;
                }
                parentNode.SetValue(colTotalMoney, totalMoneySum);
                parentNode.SetValue(colTotalPaymentAmount, totalPaymentAmount);// lh Phuc add new
            }
        }
        private void calTotalMoneyChildNode()
        {
            //if (nodeI[colTotalMoney] == null) nodeI.SetValue(colTotalMoney, 0);
            //if (nodeII[colTotalMoney] != null && nodeI[colTotalMoney] != null)
            //{
            //    decimal totalMoneySpread = (decimal)nodeII[colTotalMoney] - (decimal)nodeI[colTotalMoney];
            //    childIII_1[colTotalMoney] = totalMoneySpread > 0 ? 0 : Math.Abs(totalMoneySpread);
            //    childIII_2[colTotalMoney] = totalMoneySpread > 0 ? totalMoneySpread : 0;
            //    UpdateParentNodeTotalMoney(childIII_1);
            //}

            //if (nodeI[colTotalMoney] == null) nodeI.SetValue(colTotalMoney, 0);


            ///
            decimal totalMoneyNodeII = nodeII[colTotalMoney] != null ? (decimal)nodeII[colTotalMoney] : 0;
            decimal totalMoneyNodeI = nodeI[colTotalMoney] != null ? (decimal)nodeI[colTotalMoney] : 0;

            decimal totalMoneySpread = totalMoneyNodeII - totalMoneyNodeI;

            txtTotalMoneyText.Text = NumberMoneyToText.ConvertNumberToTextVietNamese(totalMoneySpread, cboCurrency.Text.ToUpper()); //11625
            if (totalMoneySpread != 0)
            {
                treeListData.Columns[colTotalMoney.FieldName].SummaryFooterStrFormat = $"{Math.Abs(totalMoneySpread):n2}";
            }

            childIII_1[colTotalMoney] = totalMoneySpread > 0 ? 0 : Math.Abs(totalMoneySpread);
            childIII_2[colTotalMoney] = totalMoneySpread > 0 ? totalMoneySpread : 0;

            // lh Phuc add new
            decimal totalPaymentAmountNodeII = nodeII[colTotalPaymentAmount] != null ? (decimal)nodeII[colTotalPaymentAmount] : 0;
            decimal totalPaymentAmountNodeI = nodeI[colTotalPaymentAmount] != null ? (decimal)nodeI[colTotalPaymentAmount] : 0;

            decimal totalPaymentAmountSpread = totalPaymentAmountNodeII - totalPaymentAmountNodeI;
            childIII_1[colTotalPaymentAmount] = totalPaymentAmountSpread > 0 ? 0 : Math.Abs(totalPaymentAmountSpread);
            childIII_2[colTotalPaymentAmount] = totalPaymentAmountSpread > 0 ? totalPaymentAmountSpread : 0;

            //UpdateParentNodeTotalMoney(childIII_2);
            UpdateParentNodeTotalMoney(childIII_1);
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
                //var content = new MultipartFormDataContent();

                List<PaymentOrderFileModel> listFiles = new List<PaymentOrderFileModel>();
                foreach (var file in listFileUpload)
                {
                    PaymentOrderFileModel fileOrder = new PaymentOrderFileModel();
                    fileOrder.PaymentOrderID = order.ID;
                    fileOrder.FileName = file.Name;
                    fileOrder.OriginPath = file.DirectoryName;
                    fileOrder.ServerPath = pathUpload;
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
                        SQLHelper<PaymentOrderFileModel>.Insert(fileOrder);
                    }
                }

                //HttpClient client = new HttpClient();
                //MultipartFormDataContent form = new MultipartFormDataContent();
                ////HttpContent content = new StringContent("fileToUpload");
                ////HttpContent DictionaryItems = new FormUrlEncodedContent(parameters);
                //form.Add(content, "fileToUpload");
                //form.Add(DictionaryItems, "medicineOrder");

                //var stream = new FileStream("c:\\TemporyFiles\\test.jpg", FileMode.Open);
                //content = new StreamContent(stream);
                //content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                //{
                //    Name = "fileToUpload",
                //    FileName = "AFile.txt"
                //};
                //form.Add(content);

                //var url = $"http://113.190.234.64:8083/api/Home/uploadfile?path={pathUpload}";
                //var result = await client.PostAsync(url, content);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
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
        }

        private void treeListData_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            string stt = TextUtils.ToString(e.Node.GetValue(colStt)).Trim();
            if (stt == "I" || stt == "II" || stt == "III")
            {
                e.Appearance.BackColor = Color.LightGray;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
        }
        private void treeListData_GetCustomSummaryValue(object sender, GetCustomSummaryValueEventArgs e)
        {
            //if (e.IsSummaryFooter)
            //{
            //    decimal total = 0;
            //    if (e.Column == colTotalMoney)
            //    {
            //        TreeList list = (TreeList)treeListData;
            //        //if (cboTypeOrder.SelectedIndex > 0)
            //        //{
            //        if (cboTypeOrder.SelectedIndex == 2)
            //        {
            //            total = nodeII[colTotalMoney] != null ? TextUtils.ToDecimal(nodeII[colTotalMoney]) : 0;
            //        }
            //        else
            //        {
            //            foreach (TreeListNode item in list.Nodes)
            //            {
            //                total += TextUtils.ToDecimal(item.GetValue(colTotalMoney));
            //            }
            //        }
            //        //}
            //    }
            //    e.CustomValue = total;
            //}
            // lh Phúc chỉnh sửa thêm dòng, và hiện stt
            if (e.IsSummaryFooter)
            {
                TreeList list = (TreeList)treeListData;

                if (e.Column == colTotalMoney)
                {
                    decimal total = 0;

                    if (cboTypeOrder.SelectedIndex == 2)
                    {
                        total = nodeII[colTotalMoney] != null ? TextUtils.ToDecimal(nodeII[colTotalMoney]) : 0;
                    }
                    else
                    {
                        foreach (TreeListNode item in list.Nodes)
                        {
                            total += TextUtils.ToDecimal(item.GetValue(colTotalMoney));
                        }
                    }

                    e.CustomValue = total;
                }

                if (e.Column == colTotalPaymentAmount)
                {
                    decimal totalPaymentAmount = 0;

                    if (cboTypeOrder.SelectedIndex == 2)
                    {
                        totalPaymentAmount = nodeII[colTotalPaymentAmount] != null ? TextUtils.ToDecimal(nodeII[colTotalPaymentAmount]) : 0;
                    }
                    else
                    {
                        foreach (TreeListNode item in list.Nodes)
                        {
                            totalPaymentAmount += TextUtils.ToDecimal(item.GetValue(colTotalPaymentAmount));
                        }
                    }

                    e.CustomValue = totalPaymentAmount;
                }
            }
        }
        private void cboCurrency_EditValueChanged(object sender, EventArgs e)
        {
            if (treeListData.AllNodesCount > 0)
            {
                decimal summaryTotal1 = (decimal)treeListData.GetSummaryValue(colTotalMoney);
                decimal summaryTotal2 = (decimal)treeListData.GetSummaryValue(colTotalPaymentAmount);
                string currencyType = TextUtils.ToString(cboCurrency.EditValue) == null ? paymentOrder.Unit : TextUtils.ToString(cboCurrency.Text).ToUpper();
                txtTotalMoneyText.Text = NumberMoneyToText.ConvertNumberToTextVietNamese(summaryTotal2 <= 0 ? summaryTotal1 : summaryTotal2, currencyType);
            }

        }
        private void treeListData_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            string[] stt = { "I", "II", "III" };
            bool checkNode = true;
            if (e.Column == colAdd)
            {
                TreeListNode nodeDele = treeListData.FocusedNode;
                if (stt.Contains(nodeDele.GetValue(colStt))) checkNode = false;
                if (nodeDele.ParentNode != null)
                {
                    if (TextUtils.ToString(nodeDele.ParentNode.GetValue(colStt)) == "III")
                        checkNode = false;
                }
                if (checkNode)
                {
                    if (MessageBox.Show("Bạn có chắc muốn xoá hàng này không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        nodeDele[colTotalMoney] = 0;
                        int id = TextUtils.ToInt(treeListData.FocusedNode.GetValue(colPODetailID));
                        UpdateParentNodeTotalMoney(nodeDele);
                        treeListData.DeleteNode(nodeDele);
                        lstIDDelete.Add(id);
                        if (cboTypeOrder.SelectedIndex == 2)
                        {
                            calTotalMoneyChildNode();
                        }
                        // load lại stt  LHPhuc add new
                        RecalculateSTT();
                    }
                }
            }
        }
        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            SaveData();
            paymentOrder = new PaymentOrderModel();
            cboTypeOrder.SelectedIndex = -1;
            cboPaymentOrderType.EditValue = 0;
            dtpDateOrder.Value = DateTime.Now;
            cboSupplierSale.EditValue = 0;
            cboPONCC.EditValue = 0;
            ckIsUrgent.CheckState = CheckState.Unchecked;
            dtpDeadlinePayment.EditValue = null;
            txtReasonOrder.Clear();
            txtReceiverInfo.Clear();
            rdgTypePayment.SelectedIndex = -1;
            cboCurrency.EditValue = 0;
            cboApproved.EditValue = -1;

            cboTypeBankTransfer.SelectedIndex = -1;
            txtAccountNumber.Clear();
            txtBank.Clear();
            txtContentBankTransfer.Clear();
            for (int i = treeListData.AllNodesCount - 1; i >= 0; i--)
            {
                TreeListNode node = treeListData.GetNodeByVisibleIndex(i);
                treeListData.DeleteNode(node);
            }
        }

        private void btnUploadFile_Click(object sender, EventArgs e)
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
        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            if (grvData.RowCount > 0)
            {
                var focusedRowHandle = grvData.FocusedRowHandle;
                grvData.DeleteSelectedRows();
                grvData.FocusedRowHandle = focusedRowHandle;
            }
        }

        private void treeListData_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            //if (treeListData.FocusedColumn == colTotalMoney)
            //{
            //    decimal uniPriceValue = treeListData.FocusedNode[colUnitPrice] == null ? 0 : (decimal)treeListData.FocusedNode.GetValue(colUnitPrice);
            //    decimal quantityValue = treeListData.FocusedNode[colQuantity] == null ? 0 : (decimal)treeListData.FocusedNode.GetValue(colQuantity);
            //    if (uniPriceValue > 0 && quantityValue > 0)
            //    {
            //        treeListData.BeginUpdate();
            //        e.Valid = false;
            //        e.ErrorText = "Không thể nhập thông tin này!";
            //        treeListData.EndUpdate();
            //    }

            //}
            //if (treeListData.FocusedColumn == colStt)
            //{
            //    treeListData.BeginUpdate();
            //    e.Valid = false;
            //    e.ErrorText = "Không thể nhập thông tin này!";
            //    treeListData.EndUpdate();
            //}
            //if (cboTypeOrder.SelectedIndex == 2)
            //{
            //    if (treeListData.FocusedColumn != colNote)
            //    {
            //        if (treeListData.FocusedNode.HasChildren)
            //        {
            //            treeListData.BeginUpdate();
            //            e.Valid = false;
            //            e.ErrorText = "Không thể nhập thông tin này!";
            //            treeListData.EndUpdate();
            //        }
            //        else if (treeListData.FocusedNode.ParentNode != null && treeListData.FocusedNode.ParentNode.Id != 1)
            //        {
            //            treeListData.BeginUpdate();
            //            e.Valid = false;
            //            e.ErrorText = "Không thể nhập thông tin này!";
            //            treeListData.EndUpdate();
            //        }
            //    }
            //    if (treeListData.FocusedColumn == colContentPayment || treeListData.FocusedColumn == colStt)
            //    {
            //        if (treeListData.FocusedNode.Id == 0)
            //        {
            //            treeListData.BeginUpdate();
            //            e.Valid = false;
            //            e.ErrorText = "Không thể nhập thông tin này!";
            //            treeListData.EndUpdate();
            //        }
            //    }
            //}

            if (Global.IsAdmin) return;

            if (treeListData.FocusedColumn == colTotalMoney)
            {
                decimal uniPriceValue = treeListData.FocusedNode[colUnitPrice] == null ? 0 : (decimal)treeListData.FocusedNode.GetValue(colUnitPrice);
                decimal quantityValue = treeListData.FocusedNode[colQuantity] == null ? 0 : (decimal)treeListData.FocusedNode.GetValue(colQuantity);
                if (uniPriceValue > 0 && quantityValue > 0)
                {
                    e.Valid = false;
                    e.ErrorText = "Nhắn phím ESC để tiếp tục!";
                    MessageBox.Show("Không thể nhập thông tin này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    treeListData.ClearFocusedColumn();
                }
            }
            if (treeListData.FocusedColumn == colStt)
            {
                e.Valid = false;
                e.ErrorText = "Nhắn phím ESC để tiếp tục!";
                MessageBox.Show("Không thể nhập thông tin này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                treeListData.ClearFocusedColumn();
            }
            if (cboTypeOrder.SelectedIndex == 2)
            {
                if (treeListData.FocusedColumn == colNote) return;
                if (treeListData.FocusedNode.HasChildren)
                {
                    e.Valid = false;
                    e.ErrorText = "Nhắn phím ESC để tiếp tục!";
                    MessageBox.Show("Không thể nhập thông tin này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    treeListData.ClearFocusedColumn();
                    treeListData.SetFocusedNode(nodeI);
                }
                else if (treeListData.FocusedNode.ParentNode != null && treeListData.FocusedNode.ParentNode.Id != 1)
                {
                    e.Valid = false;
                    e.ErrorText = "Nhắn phím ESC để tiếp tục!";
                    MessageBox.Show("Không thể nhập thông tin này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    treeListData.ClearFocusedColumn();
                    treeListData.SetFocusedNode(nodeI);
                }
                if (treeListData.FocusedColumn == colContentPayment && treeListData.FocusedNode.Id == 0)
                {
                    e.Valid = false;
                    e.ErrorText = "Nhắn phím ESC để tiếp tục!";
                    MessageBox.Show("Không thể nhập thông tin này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    treeListData.ClearFocusedColumn();
                }
            }
        }



        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue("ID"));
            string fileName = TextUtils.ToString(grvData.GetFocusedRowCellValue("FileName"));

            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá file đính kèm [{fileName}] không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                grvData.DeleteSelectedRows();
                if (id <= 0) return;
                PaymentOrderFileModel file = SQLHelper<PaymentOrderFileModel>.FindByID(id);
                listFileDelete.Add(file);
            }

        }

        // lh Phúc add new 
        private void treeListData_NodeChanged(object sender, NodeChangedEventArgs e)
        {
            RecalculateSTT();
        }

        private void treeListData_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int typeOrder = TextUtils.ToInt(cboTypeOrder.SelectedValue);
            if (_sameTypeOrders.Contains(typeOrder) /*typeOrder == 1*/) return;

            var node = treeListData.FocusedNode;
            var parentNode = node.ParentNode;
            bool isValid = false;
            if (parentNode != null) isValid = TextUtils.ToString(parentNode.GetValue("Stt")).Trim() == "III";

            if (treeListData.FocusedNode.HasChildren || isValid || treeListData.FocusedColumn == colTotalPaymentAmount)
            {
                e.Cancel = true;
            }
        }

        private void btnAddSupplierSale_Click(object sender, EventArgs e)
        {
            frmSupplierSaleDetail frm = new frmSupplierSaleDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadSupplierSale();
            }
        }

        private void label35_Click(object sender, EventArgs e)
        {

        }

        private void cboPaymentOrderType_EditValueChanged(object sender, EventArgs e)
        {
            if (TextUtils.ToInt(cboPaymentOrderType.EditValue) == 22)
            {
                panel1.Visible = true;
                txtReasonOrder.Height = 49;
                txtReceiverInfo.Height = 49;
            }
            else
            {
                panel1.Visible = false;
                txtReasonOrder.Height = 130;
                txtReceiverInfo.Height = 130;
            }
        }
    }
}