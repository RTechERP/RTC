using BaseBusiness.DTO;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Forms.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BMS
{
    public partial class frmPONCCDetailNew : _Forms
    {
        DataTable dt = new DataTable();
        public PONCCModel po = new PONCCModel();
        //public List<ProjectPartlistPurchaseRequestModel> listRequest = new List<ProjectPartlistPurchaseRequestModel>();
        public List<ProjectPartlistPurchaseRequestDTO> listRequest = new List<ProjectPartlistPurchaseRequestDTO>();
        private List<int> lstBillImportId = new List<int>();

        List<PONCCDetailModel> listDelete = new List<PONCCDetailModel>();

        int employeeTbpPur = 178;

        public Tuple<bool, PONCCModel> dataCopy = new Tuple<bool, PONCCModel>(false, new PONCCModel());

        public int currencyID = 0;
        public int supplierSaleID = 0;

        public bool IsSave = false;

        public frmPONCCDetailNew()
        {
            InitializeComponent();
        }

        private void frmPONCCDetailNew_Load(object sender, EventArgs e)
        {
            this.Text += string.IsNullOrEmpty(TextUtils.ToString(this.Tag)) ? "" : $" - {this.Tag}";
            LoadSupplierSale();
            LoadEmployee();
            LoadCurrency();
            LoadRulePay();
            LoadProductSale();
            LoadProductRTC();
            LoadProject();
            LoadProductType();//PQ.Chien - UPDATE - 17 / 04 / 2025
            LoadDocumentImport();
            LoadPOType(); //PQ.Chien - UPDATE - 17 / 04 / 2025
            LoadData();

            //txtCurrencyRate.ReadOnly = !Global.IsAdmin;


            colDeadlineDelivery.OptionsColumn.AllowEdit = (Global.IsAdmin && Global.EmployeeID <= 0);
            colDeadlineDelivery.OptionsColumn.ReadOnly = !(Global.IsAdmin && Global.EmployeeID <= 0);


            //stackPanel4.Visible = Global.IsAdmin;

            LogActions();

        }

        void LoadData()
        {
            btnSave.Enabled = btnSaveAndClose.Enabled = btnSaveNew.Enabled = (!TextUtils.ToBoolean(po.IsApproved));
            txtBillCode.ReadOnly = (po.ID > 0 && !Global.IsAdmin);

            //chkIsCheckTotalMoneyPO.Visible = listRequest.Count > 0;
            txtReasonForFailure.Enabled = chkOrderQualityNotMet.Checked;
            if (dataCopy.Item1)
            {
                //MessageBox.Show("copy");
                LoadDataCopy();
            }
            else
            {
                if (po.ID > 0)
                {
                    var rulePays = SQLHelper<PONCCRulePayModel>.FindByAttribute("PONCCID", po.ID).Select(x => x.RulePayID).ToList();
                    //Thông tin chung
                    cboSupplierSale.EditValue = po.SupplierSaleID;
                    txtPOCode.Text = po.POCode;
                    txtNote.Text = po.Note;
                    cboEmployee.EditValue = po.EmployeeID;
                    //cboRulePay1.SetEditValue(string.Join(",", rulePays));
                    cboRulePay.EditValue = (string.Join(",", rulePays));
                    cboCompany.SelectedIndex = TextUtils.ToInt(po.Company);

                    //Hàng
                    dtpRequestDate.Value = po.RequestDate.Value;
                    txtBillCode.Text = po.BillCode;
                    cboStatus.SelectedIndex = TextUtils.ToInt(po.Status);
                    txtTotalMoneyPO.EditValue = po.TotalMoneyPO; ;
                    cboCurrency.EditValue = po.CurrencyID;
                    txtCurrencyRate.EditValue = po.CurrencyRate;
                    dtpDeliveryDate.Value = po.DeliveryDate.Value;

                    //Khác
                    txtAddressDelivery.Text = po.AddressDelivery;
                    txtOtherTerms.Text = po.OtherTerms;

                    //Thông tin bổ sung
                    txtAccountNumberSupplier.Text = po.AccountNumberSupplier;
                    txtBankCharge.Text = po.BankCharge;
                    txtFedexAccount.Text = po.FedexAccount;
                    txtOriginItem.Text = po.OriginItem;
                    txtSupplierVoucher.Text = po.SupplierVoucher;

                    txtBankSupplier.Text = po.BankSupplier;
                    txtRuleIncoterm.Text = po.RuleIncoterm;
                    txtOrderTarget.Text = po.OrderTargets;
                    chkNCCNew.Checked = TextUtils.ToBoolean(po.NCCNew);
                    chkDeptSupplier.Checked = TextUtils.ToBoolean(po.DeptSupplier);
                    cboPOType.SelectedIndex = TextUtils.ToInt(po.POType);
                    txtShippingPoint.Text = po.ShippingPoint;


                    chkOrderQualityNotMet.Checked = TextUtils.ToBoolean(po.OrderQualityNotMet);
                    txtReasonForFailure.Text = po.ReasonForFailure;
                }
                else
                {
                    LoadPOCode();
                    LoadBillCode();

                    cboCompany.SelectedIndex = 0;
                    cboStatus.SelectedIndex = 0;
                    cboEmployee.EditValue = Global.EmployeeID;
                    cboCurrency.EditValue = currencyID;
                    cboSupplierSale.EditValue = supplierSaleID;

                    cboPOType.SelectedIndex = TextUtils.ToInt(listRequest.Max(x => x.TicketType));
                }

                //if (Global.IsAdmin || Global.EmployeeID == employeeTbpPur)
                //{
                //    cboStatus.Enabled = true;
                //}
                //else if ((po.Status == 4 || po.Status == 5))
                //{
                //    cboStatus.Enabled = false;
                //}

                cboStatus.Enabled = (Global.IsAdmin || Global.EmployeeID == employeeTbpPur);
                LoadDetail();
            }


        }
        private void LoadDocumentImport()
        {
            DataTable data = TextUtils.LoadDataFromSP("spGetAllDocumentImportByPONCCID", "A", new string[] { "@PONCCID", "@BillImportID" }, new object[] { po.ID, 0 });
            grdDocImport.DataSource = data;
        }

        //PQ.Chien - UPDATE - 17 / 04 / 2025
        void LoadPOType()
        {
            List<object> lst = new List<object>()
            {
                new {ID = 0, POType = "PO thương mại"},
                new {ID = 1, POType = "PO mượn"}
            };
            cboPOType.DataSource = lst;
            cboPOType.ValueMember = "ID";
            cboPOType.DisplayMember = "POType";
            cboPOType.SelectedIndex = 0;
        }
        //PQ.Chien - UPDATE - 17 / 04 / 2025
        private void cboPOType_SelectedValueChanged(object sender, EventArgs e)
        {

        }


        //PQ.Chien - UPDATE - 17 / 04 / 2025
        void LoadProductType()
        {
            List<object> lst = new List<object>()
            {
                new {ID = 0, ProductType = "Phi mậu dịch"},
                new {ID = 1, ProductType = "Hàng thương mại"},
                new {ID = 2, ProductType = "Tạp nhập tái xuất"}

            };
            cboProductType.DataSource = lst;
            cboProductType.ValueMember = "ID";
            cboProductType.DisplayMember = "ProductType";
        }



        //PQ.Chien - UPDATE - 17 / 04 / 2025
        void LoadDetail()
        {
            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetPONCCDetail_Khanh", new string[] { "@PONCCID" }, new object[] { po.ID });

            //Load danh sách tham chiều
            stackPanel2.Controls.Clear();
            DataTable dtRef = dataSet.Tables[1];
            for (int i = 0; i < dtRef.Rows.Count; i++)
            {
                //============================== Lee Min Khooi Update lấy danh sách BillImportId để find lại lúc sửa ID sản phẩm trong PONCCDetails ==============================================
                int billImportId = TextUtils.ToInt(dtRef.Rows[i]["ID"]);
                if (billImportId > 0)
                {
                    lstBillImportId.Add(billImportId);
                }
                //============================================= End ============================================================
                LinkLabel linkLabel = new LinkLabel();
                linkLabel.Text = $"{TextUtils.ToString(dtRef.Rows[i]["BillImportCode"])} - {TextUtils.ToString(dtRef.Rows[i]["WarehouseCode"])}";
                linkLabel.Tag = $"{TextUtils.ToString(dtRef.Rows[i]["ID"])};{TextUtils.ToString(dtRef.Rows[i]["WarehouseType"])}";
                linkLabel.AutoSize = true;
                linkLabel.LinkClicked += LinkLabel_LinkClicked;
                stackPanel2.Controls.Add(linkLabel);
            }


            //Load chi tiết PO
            dt = dataSet.Tables[0];
            if (listRequest.Count > 0)
            {

                int[] productGroupIDHR = new int[] { 77, 80 };
                for (int i = 0; i < listRequest.Count; i++)
                {

                    ProjectPartlistPurchaseRequestDTO item = listRequest[i];


                    //var productSale = (DataRowView)cboProductSale.GetRowByKeyValue(item.ProductSaleID);
                    var productSale = (DataRowView)cboProductSale.GetRowByKeyValue(item.ProductSaleID);

                    var productRTC = (DataRowView)cboProductRTC.GetRowByKeyValue(item.ProductRTCID);

                    DataRow dataRow = dt.NewRow();
                    dataRow["STT"] = i + 1;
                    dataRow["ProductSaleID"] = item.ProductSaleID;
                    dataRow["ParentProductCode"] = item.ParentProductCode;//ndnhat update 14/10/2025
                    dataRow["IsPurchase"] = item.IsPurchase;//ndnhat update 14/10/2025
                    if (productSale != null)
                    {
                        dataRow["ProductName"] = productSale["ProductName"];
                        dataRow["ProductNewCode"] = productSale["ProductNewCode"];
                        dataRow["ProductGroupName"] = productSale["ProductGroupName"];

                        dataRow["ProductCodeOfSupplier"] = TextUtils.ToString(productSale["ProductName"]) + " " + TextUtils.ToString(productSale["ProductCode"]);
                        if (productGroupIDHR.Contains(TextUtils.ToInt(item.ProductGroupID))) dataRow["ProductCodeOfSupplier"] = TextUtils.ToString(productSale["ProductName"]);
                        dataRow["Unit"] = productSale["Unit"];
                    }
                    else if (productRTC != null)
                    {
                        dataRow["ProductName"] = productRTC["ProductName"];
                        dataRow["ProductNewCode"] = productRTC["ProductCodeRTC"];
                        dataRow["ProductGroupName"] = productRTC["ProductGroupName"];
                        dataRow["ProductCodeOfSupplier"] = TextUtils.ToString(productRTC["ProductName"]) + " " + TextUtils.ToString(productRTC["ProductCode"]);
                        dataRow["Unit"] = productRTC["UnitCountName"];
                    }


                    if (item.IsCommercialProduct) dataRow["ProductCodeOfSupplier"] = item.GuestCode;
                    if (!string.IsNullOrWhiteSpace(item.SpecialCode)) dataRow["ProductCodeOfSupplier"] = item.SpecialCode;

                    dataRow[colUnitPrice.FieldName] = item.UnitPrice;
                    dataRow["ProjectPartlistPurchaseRequestID"] = item.ID;
                    dataRow["ProjectPartListID"] = TextUtils.ToInt(item.ProjectPartListID);
                    dataRow["QtyRequest"] = item.Quantity;
                    dataRow["ThanhTien"] = item.Quantity * item.UnitPrice;
                    dataRow["TotalPrice"] = item.Quantity * item.UnitPrice;

                    dataRow["ProjectID"] = item.ProjectID;
                    dataRow["ProjectName"] = item.ProjectName;
                    dataRow["DeadlineDelivery"] = item.Deadline;

                    if (cboCurrency.Text.ToLower() != "VND".ToLower()) item.VAT = 0;
                    dataRow["VAT"] = item.VAT;
                    dataRow["VATMoney"] = (item.Quantity * item.UnitPrice * item.VAT) / 100;
                    //dataRow["VATMoney"] = item.TotaMoneyVAT;
                    dataRow["PONCCDetailRequestBuyID"] = item.PONCCDetailRequestBuyID;

                    dataRow["PriceHistory"] = item.HistoryPrice;

                    dataRow["DateReturnEstimated"] = DBNull.Value;
                    if (item.DateReturnEstimated.HasValue) dataRow["DateReturnEstimated"] = item.DateReturnEstimated.Value;
                    dataRow[colProductRTCID.FieldName] = item.ProductRTCID;
                    dataRow[colIsBill.FieldName] = item.VAT > 0;
                    dataRow[colIsStock.FieldName] = item.IsStock;
                    dataRow[colUnitName.FieldName] = item.UnitName;

                    dt.Rows.Add(dataRow);
                }
                dt.AcceptChanges();

            }
            grdData.DataSource = dt;

            //CalculatorTotalMoneyPO();
            CalculatorTotalPrice();
        }


        void LoadDataCopy()
        {
            var poCopy = dataCopy.Item2;
            var rulePays = SQLHelper<PONCCRulePayModel>.FindByAttribute("PONCCID", poCopy.ID).Select(x => x.RulePayID).ToList();
            //Thông tin chung
            cboSupplierSale.EditValue = poCopy.SupplierSaleID;
            //txtPOCode.Text = poCopy.POCode;
            txtNote.Text = poCopy.Note;
            //cboEmployee.EditValue = poCopy.EmployeeID;
            cboRulePay.EditValue = (string.Join(",", rulePays));
            cboCompany.SelectedIndex = TextUtils.ToInt(poCopy.Company);

            //Hàng
            //dtpRequestDate.Value = poCopy.RequestDate.Value;
            //txtBillCode.Text = poCopy.BillCode;
            cboStatus.SelectedIndex = 0;
            txtTotalMoneyPO.EditValue = poCopy.TotalMoneyPO;
            cboCurrency.EditValue = poCopy.CurrencyID;
            txtCurrencyRate.EditValue = poCopy.CurrencyRate;
            //dtpDeliveryDate.Value = poCopy.DeliveryDate.Value;

            //Khác
            txtAddressDelivery.Text = poCopy.AddressDelivery;
            txtOtherTerms.Text = poCopy.OtherTerms;

            //Thông tin bổ sung
            txtAccountNumberSupplier.Text = poCopy.AccountNumberSupplier;
            txtBankCharge.Text = poCopy.BankCharge;
            txtFedexAccount.Text = poCopy.FedexAccount;
            txtOriginItem.Text = poCopy.OriginItem;
            txtSupplierVoucher.Text = poCopy.SupplierVoucher;

            txtBankSupplier.Text = poCopy.BankSupplier;
            txtRuleIncoterm.Text = poCopy.RuleIncoterm;
            txtOrderTarget.Text = poCopy.OrderTargets;
            chkNCCNew.Checked = TextUtils.ToBoolean(poCopy.NCCNew);
            chkDeptSupplier.Checked = TextUtils.ToBoolean(poCopy.DeptSupplier);

            cboPOType.SelectedIndex = TextUtils.ToInt(poCopy.POType);//PQ.Chien - UPDATE - 17 / 04 / 2025
            txtShippingPoint.Text = poCopy.ShippingPoint; //lt.anh update 08/05/2025
            LoadPOCode();
            LoadBillCode();

            //LoadDataDetail
            DataSet dataSet = TextUtils.LoadDataSetFromSP("spGetPONCCDetail_Khanh", new string[] { "@PONCCID" }, new object[] { poCopy.ID });
            dt = dataSet.Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                row["ID"] = 0;
                row["PONCCID"] = 0;
                row["ProjectPartlistPurchaseRequestID"] = 0;
            }
            dt.AcceptChanges();
            grdData.DataSource = dt;
        }

        private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = (LinkLabel)sender;

            var tag = TextUtils.ToString(linkLabel.Tag).Split(';');
            int billImportId = TextUtils.ToInt(tag[0]);
            string warehouseType = TextUtils.ToString(tag[1]).Trim();

            if (warehouseType.ToLower() == "sale")
            {
                BillImportModel model = SQLHelper<BillImportModel>.FindByID(billImportId);
                WarehouseModel warehouse = SQLHelper<WarehouseModel>.FindByID(TextUtils.ToInt(model.WarehouseID));

                frmBillImportDetail frm = new frmBillImportDetail();
                frm.IDDetail = billImportId;
                frm.billImport = model;
                frm.WarehouseCode = warehouse == null ? "" : warehouse.WarehouseCode;
                frm.warehouseID = TextUtils.ToInt(model.WarehouseID);

                frm.Tag = model.BillImportCode;
                frm.poNCCId = po.ID;
                TextUtils.OpenChildForm(frm, null);
                frm.FormClosed += Frm_FormClosed;
            }
            else
            {
                BillImportTechnicalModel model = SQLHelper<BillImportTechnicalModel>.FindByID(billImportId);
                WarehouseModel warehouse = SQLHelper<WarehouseModel>.FindByID(TextUtils.ToInt(model.WarehouseID));

                frmBillImportTechDetail_New frm = new frmBillImportTechDetail_New();
                frm.IDDetail = billImportId;
                frm.billImport = model;
                //frm.WarehouseCode = warehouse == null ? "" : warehouse.WarehouseCode;
                frm.warehouseID = TextUtils.ToInt(model.WarehouseID);

                frm.Tag = model.BillCode;
                frm.poNCCId = po.ID;
                TextUtils.OpenChildForm(frm, null);
                frm.FormClosed += Frm_FormClosed;


                //if (frm.ShowDialog() == DialogResult.OK)
                //{
                //    LoadDocumentImport();
                //}
            }

        }

        void LoadSupplierSale()
        {
            List<SupplierSaleModel> list = SQLHelper<SupplierSaleModel>.FindAll().OrderByDescending(x => x.NgayUpdate).ToList();
            cboSupplierSale.Properties.ValueMember = "ID";
            cboSupplierSale.Properties.DisplayMember = "NameNCC";
            cboSupplierSale.Properties.DataSource = list;
        }

        void LoadCurrency()
        {
            List<CurrencyModel> list = SQLHelper<CurrencyModel>.FindAll();
            cboCurrency.Properties.ValueMember = "ID";
            cboCurrency.Properties.DisplayMember = "Code";
            cboCurrency.Properties.DataSource = list;
        }

        void LoadEmployee()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = dt;

            cboEmployee.EditValue = Global.EmployeeID;
        }

        void LoadRulePay()
        {
            List<RulePayModel> list = SQLHelper<RulePayModel>.FindAll();
            //cboRulePay1.Properties.ValueMember = "ID";
            //cboRulePay1.Properties.DisplayMember = "Note";
            //cboRulePay1.Properties.DataSource = list;

            cboRulePay.Properties.ValueMember = "ID";
            cboRulePay.Properties.DisplayMember = "Note";
            cboRulePay.Properties.DataSource = list;
        }


        void LoadProductSale()
        {
            //List<ProductSaleModel> list = SQLHelper<ProductSaleModel>.FindAll();

            var listGroup = SQLHelper<ProductGroupModel>.FindAll().Select(x => x.ID).ToList();
            var idGroup = string.Join(",", listGroup);
            DataTable dt = TextUtils.LoadDataFromSP("spGetProductSale", "A", new string[] { "@IDgroup" }, new object[] { idGroup });

            cboProductSale.ValueMember = "ID";
            cboProductSale.DisplayMember = "ProductCode";
            cboProductSale.DataSource = dt;
        }

        void LoadProject()
        {
            List<ProjectModel> list = SQLHelper<ProjectModel>.FindAll().OrderByDescending(x => x.ID).ToList();
            cboProject.ValueMember = "ID";
            cboProject.DisplayMember = "ProjectCode";
            cboProject.DataSource = list;
        }

        void LoadProductRTC()
        {
            DataTable dt = TextUtils.GetTable("spGetProductRTC", "A");
            cboProductRTC.ValueMember = "ID";
            cboProductRTC.DisplayMember = "ProductCode";
            cboProductRTC.DataSource = dt;
        }

        void LoadPOCode()
        {
            string code = "";
            SupplierSaleModel supplier = (SupplierSaleModel)cboSupplierSale.GetSelectedDataRow();
            if (supplier != null && supplier.ID > 0)
            {
                code = $"{DateTime.Now.ToString("MMyyyy")}-{supplier.CodeNCC}-";
                string currentCode = TextUtils.ToString(TextUtils.ExcuteScalar($"EXEC dbo.spGetPOCodeInPONCC N'{code}'"));

                int stt = 1;
                if (!string.IsNullOrEmpty(currentCode.Trim()))
                {
                    stt = TextUtils.ToInt(currentCode.Substring(code.Length));
                    stt++;
                }

                string sttText = stt.ToString();
                while (sttText.Length < 3)
                {
                    sttText = $"0{sttText}";
                }
                code += sttText;
            }

            txtPOCode.Text = code;
        }



        //PQ.Chien - UPDATE - 17 / 04 / 2025
        void LoadBillCode()
        {
            try
            {
                // Determine the prefix based on the selected PO type
                string code = cboPOType.SelectedValue?.ToString() == "0" ? "DMH" : "DEMO";

                // Filter records by the selected prefix and extract STT
                var listPO = SQLHelper<PONCCModel>.FindAll()
                    .Where(x => !string.IsNullOrEmpty(x.BillCode) && x.BillCode.StartsWith(code))
                    .Select(x => new
                    {
                        ID = x.ID,
                        BillCode = x.BillCode,
                        STT = TextUtils.ToInt(x.BillCode.Substring(code.Length))
                    }).ToList();

                // Calculate the next sequence number
                int stt = listPO.Count <= 0 ? 0 : listPO.Max(x => x.STT);
                stt++;

                // Format the sequence number to have at least 5 digits
                string sttText = stt.ToString();
                while (sttText.Length < 5)
                {
                    sttText = $"0{sttText}";
                }

                // Combine prefix and sequence number
                code += sttText;

                txtBillCode.Text = code;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi khi tạo số ĐMH!\r\n{ex.ToString()}", "Thông báo");
            }
        }

        bool CheckValidate()
        {
            string pattern = @"^[a-zA-Z0-9_-]+$";
            Regex regex = new Regex(pattern);

            //string codePONCC = TextUtils.ToString(txtPOCode.Text).Trim();
            //var poncc = SQLHelper<PONCCModel>.FindByAttribute("POCode", codePONCC);
            //if (poncc.Count > 0 && po.ID <= 0)
            //{
            //    MessageBox.Show($"Mã PO NCC [{codePONCC}] đã tồn tại!", "Thông báo");
            //    return false;
            //}
            //string billCode = TextUtils.ToString(txtBillCode.Text).Trim();
            //var poNCC = SQLHelper<PONCCModel>.FindByAttribute("BillCode", billCode);
            //if (poNCC.Count > 0 && po.ID <= 0)
            //{
            //    MessageBox.Show($"Mã đơn hàng [{billCode}] đã tồn tại!", "Thông báo");
            //    return false;
            //}


            // ===================== Lee Min Khooi Update Check validate khi hoàn thành không được sửa ============================================
            if (po.ID > 0)
            {
                if (!(po.Status == 0 || po.Status == 5) && !(Global.EmployeeID == employeeTbpPur || Global.IsAdmin))
                {
                    var oldStatus = cboStatus.Items[TextUtils.ToInt(po.Status)];
                    MessageBox.Show($"PONCC đã {oldStatus}! Không được sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }
            // ===================== END  ============================================



            if (TextUtils.ToInt(cboSupplierSale.EditValue) == 0)
            {
                MessageBox.Show($"Vui lòng nhập NCC", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (string.IsNullOrEmpty(txtPOCode.Text))
            {
                MessageBox.Show($"Vui lòng nhập Mã PO NCC", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                bool isCheck = regex.IsMatch(txtPOCode.Text.Trim());
                if (!isCheck)
                {
                    MessageBox.Show("Mã PO NCC chỉ chứa chữ cái tiếng Anh và số!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }


            if (string.IsNullOrEmpty(txtBillCode.Text.Trim()))
            {
                MessageBox.Show($"Vui lòng nhập Số đơn hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                bool isCheck = regex.IsMatch(txtBillCode.Text.Trim());
                if (!isCheck)
                {
                    MessageBox.Show("Số đơn hàng chỉ chứa chữ cái tiếng Anh và số!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }

            var exp1 = new Expression("POCode", txtPOCode.Text.Trim());
            var exp2 = new Expression("BillCode", txtBillCode.Text.Trim());
            var exp3 = new Expression("ID", po.ID, "<>");
            var exp4 = new Expression("IsDeleted", 1, "<>");
            var exp5 = new Expression(PONCCModel_Enum.POType, cboPOType.SelectedValue);
            var poCodes = SQLHelper<PONCCModel>.FindByExpression(exp1.And(exp3).And(exp4));
            var poBillCodes = SQLHelper<PONCCModel>.FindByExpression(exp2.And(exp3).And(exp4).And(exp5));

            if (poCodes.Count > 0)
            {
                MessageBox.Show($"Mã PO NCC [{txtPOCode.Text.Trim()}] đã tồn tại!", TextUtils.Caption);
                return false;
            }

            if (poBillCodes.Count > 0)
            {
                {
                    DialogResult dialogResult = MessageBox.Show($"Số đơn hàng [{txtBillCode.Text.Trim()}] đã tồn tại.\n" +
                                                                $"Bạn có muốn tự động tăng Số đơn hàng không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        LoadBillCode();
                        return true;
                    }
                    else
                    {
                        txtBillCode.Text = po.BillCode;
                        return false;
                    }
                }

                //return dialogResult == DialogResult.Yes;
            }

            if (TextUtils.ToInt(cboEmployee.EditValue) == 0)
            {
                MessageBox.Show($"Vui lòng nhập NV mua hàng", "Thông báo");
                return false;
            }

            //bool atLeastOneChecked = false;
            //foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in cboRulePay.Properties.Items)
            //{
            //    if (item.CheckState == CheckState.Checked)
            //    {
            //        atLeastOneChecked = true;
            //        break;
            //    }
            //}

            //if (string.IsNullOrEmpty(TextUtils.ToString(cboRulePay.EditValue)))
            if (TextUtils.ToInt(cboRulePay.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng chọn điều khoản TT", "Thông báo");
                return false;
            }

            if (cboCompany.SelectedIndex <= 0)
            {
                MessageBox.Show($"Vui lòng nhập Công ty", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (string.IsNullOrEmpty(TextUtils.ToString(dtpRequestDate.Value)))
            {
                MessageBox.Show($"Vui lòng nhập Ngày đơn hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            //if (string.IsNullOrEmpty(txtBillCode.Text))
            //{
            //    MessageBox.Show($"Vui lòng nhập Mã đơn hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}

            //if (cboStatus.SelectedIndex == -1)
            //{
            //    MessageBox.Show($"Vui lòng nhập Tình trạng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}

            if (string.IsNullOrEmpty(txtTotalMoneyPO.Text))
            {
                MessageBox.Show($"Vui lòng nhập Tổng tiền PO", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (TextUtils.ToInt(cboCurrency.EditValue) == 0)
            {
                MessageBox.Show($"Vui lòng nhập Loại tiền", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (string.IsNullOrEmpty(TextUtils.ToString(dtpDeliveryDate.Value)))
            {
                MessageBox.Show($"Vui lòng nhập Ngày giao hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }


            if (listRequest.Count > 0)
            {
                if (chkIsCheckTotalMoneyPO.Checked)
                {
                    decimal totalPrice = TextUtils.ToDecimal(colThanhTien.SummaryItem.SummaryValue);
                    decimal totalPriceRequest = listRequest.Sum(x => (TextUtils.ToDecimal(x.Quantity) * TextUtils.ToDecimal(x.UnitPrice)));

                    if (totalPrice > totalPriceRequest)
                    {
                        MessageBox.Show($"Tổng Thành tiền không được lớn hơn tổng Thành tiền duyệt mua ({totalPriceRequest.ToString("n2")}).\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }
                }
                else
                {
                    for (int i = 0; i < grvData.RowCount; i++)
                    {
                        int purchaseRequestId = TextUtils.ToInt(grvData.GetRowCellValue(i, colProjectPartlistPurchaseRequestID));
                        ProjectPartlistPurchaseRequestModel purchaseRequest = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByID(purchaseRequestId);
                        if (purchaseRequest == null || purchaseRequest.ID <= 0) continue;

                        decimal unitPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colUnitPrice));
                        int stt = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));

                        if (unitPrice > purchaseRequest.UnitPrice)
                        {
                            MessageBox.Show($"Đơn giá mua không được lớn hơn đơn giá duyệt mua.\nVui lòng kiểm tra lại (Stt: {stt})", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return false;
                        }
                    }
                }
            }

            for (int i = 0; i < grvData.RowCount; i++)
            {
                int stt = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));
                int qtyRequest = TextUtils.ToInt(grvData.GetRowCellValue(i, colQtyRequest));
                if (qtyRequest <= 0)
                {
                    MessageBox.Show($"Số lượng phải lớn hơn 0 (dòng {stt})", "Thông báo");
                    return false;
                }
            }

            if (chkOrderQualityNotMet.Checked && string.IsNullOrWhiteSpace(txtReasonForFailure.Text))
            {
                MessageBox.Show($"Vui lòng nhập lý do không đạt chất lượng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            return true;

            // Lee Min Khooi Update 04/06/2024
            //int count = 0;
            //grvDocImport.FocusedRowHandle = -1;
            //for (int i = 0; i < grvDocImport.RowCount; i++)
            //{
            //    bool isCheck = TextUtils.ToBoolean(grvDocImport.GetRowCellValue(i, colDocIsChecked));
            //    if (isCheck) count++;
            //}
            //if (count == 0)
            //{
            //    MessageBox.Show($"Phải chọn ít nhất 1 Hồ sơ đi kèm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}
            //===================================================================

            //return true;
        }

        bool SaveData()
        {
            try
            {
                grvData.FocusedRowHandle = -1;
                if (!CheckValidate()) return false;
                IsSave = true; //Khánh
                //Thông tin chung
                po.SupplierSaleID = TextUtils.ToInt(cboSupplierSale.EditValue);
                po.POCode = txtPOCode.Text.Trim();
                po.Note = txtNote.Text.Trim();
                po.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
                po.Company = cboCompany.SelectedIndex;
                po.POType = cboPOType.SelectedIndex;//PQ.Chien - UPDATE - 17 / 04 / 2025
                //Hàng
                po.RequestDate = dtpRequestDate.Value;
                po.BillCode = txtBillCode.Text.Trim();
                po.Status = cboStatus.SelectedIndex;
                po.TotalMoneyPO = TextUtils.ToDecimal(txtTotalMoneyPO.EditValue);
                po.CurrencyID = TextUtils.ToInt(cboCurrency.EditValue);
                po.CurrencyRate = TextUtils.ToDecimal(txtCurrencyRate.EditValue);
                po.DeliveryDate = dtpDeliveryDate.Value;

                //Khác
                po.AddressDelivery = txtAddressDelivery.Text.Trim();
                po.OtherTerms = txtOtherTerms.Text.Trim();

                //Thông tin bổ sung
                po.AccountNumberSupplier = txtAccountNumberSupplier.Text.Trim();
                po.BankCharge = txtBankCharge.Text.Trim();
                po.FedexAccount = txtFedexAccount.Text.Trim();
                po.OriginItem = txtOriginItem.Text.Trim();
                po.SupplierVoucher = txtSupplierVoucher.Text.Trim();

                po.BankSupplier = txtBankSupplier.Text.Trim();
                po.RuleIncoterm = txtRuleIncoterm.Text.Trim();
                po.OrderTargets = txtOrderTarget.Text.Trim();
                po.NCCNew = chkNCCNew.Checked;
                po.DeptSupplier = chkDeptSupplier.Checked;
                po.ShippingPoint = txtShippingPoint.Text.Trim();

                po.OrderQualityNotMet = chkOrderQualityNotMet.Checked;
                po.ReasonForFailure = chkOrderQualityNotMet.Checked ? txtReasonForFailure.Text.Trim() : "";

                if (po.ID > 0)
                {
                    SQLHelper<PONCCModel>.Update(po);
                }
                else
                {
                    po.ID = SQLHelper<PONCCModel>.Insert(po).ID;
                }

                //Update điều khoản thanh toán
                UpdateRulePay();

                //string rulePaySelects = TextUtils.ToString(cboRulePay.EditValue);
                //SQLHelper<PONCCRulePayModel>.DeleteByAttribute("PONCCID", po.ID);
                //foreach (string item in rulePaySelects.Split(','))
                //{
                //    PONCCRulePayModel rulePay = new PONCCRulePayModel();
                //    rulePay.PONCCID = po.ID;
                //    rulePay.RulePayID = TextUtils.ToInt(item);

                //    SQLHelper<PONCCRulePayModel>.Insert(rulePay);
                //}

                //Insert PONCC Details
                for (int i = 0; i < grvData.RowCount; i++)
                {
                    int id = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                    PONCCDetailModel detail = SQLHelper<PONCCDetailModel>.FindByID(id);
                    detail = detail == null ? new PONCCDetailModel() : detail;

                    detail.PONCCID = po.ID;
                    detail.ProjectPartlistPurchaseRequestID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProjectPartlistPurchaseRequestID));
                    detail.ProjectPartListID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProjectPartListID));
                    detail.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));
                    detail.ProductSaleID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductSaleID));
                    detail.ProductCodeOfSupplier = TextUtils.ToString(grvData.GetRowCellValue(i, colProductCodeOfSupplier)).Trim();
                    detail.QtyRequest = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQtyRequest));
                    detail.UnitPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colUnitPrice));
                    detail.ThanhTien = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colThanhTien));

                    detail.VAT = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colVAT));
                    detail.DiscountPercent = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colDiscountPercent));
                    detail.FeeShip = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colFeeShip));
                    //===========================================================   Lee Min Khooi Update ===========================================================
                    //detail.TotalPrice = TextUtils.ToDecimal(totalPriceFormat);
                    //string currencyExchangeFormat = String.Format("{0:0.00}", detail.TotalPrice * po.CurrencyRate);
                    //detail.CurrencyExchange = TextUtils.ToDecimal(currencyExchangeFormat);
                    //detail.Discount = TextUtils.ToDecimal(String.Format("{0:0.00}", (detail.ThanhTien * detail.DiscountPercent) / 100));
                    //string vatMoneyFormat = String.Format("{0:0.00}", (detail.ThanhTien * detail.VAT) / 100);
                    //detail.VATMoney = TextUtils.ToDecimal(vatMoneyFormat);


                    detail.VATMoney = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colVATMoney));
                    detail.Discount = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colDiscount));
                    string totalPriceFormat = String.Format("{0:0.00}", detail.ThanhTien + detail.VATMoney + detail.FeeShip - detail.Discount);
                    detail.TotalPrice = TextUtils.ToDecimal(totalPriceFormat);

                    string currencyExchangeFormat = String.Format("{0:0.00}", detail.TotalPrice * po.CurrencyRate);
                    detail.CurrencyExchange = TextUtils.ToDecimal(currencyExchangeFormat);
                    //========================================= END ============================================================================================
                    detail.ExpectedDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, colExpectedDate));
                    detail.ActualDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, colActualDate));
                    detail.PriceSale = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colPriceSale));
                    detail.PriceHistory = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colPriceHistory));
                    detail.BiddingPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colBiddingPrice));
                    detail.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));
                    detail.ProjectID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProjectID));

                    detail.ProjectName = TextUtils.ToString(grvData.GetRowCellValue(i, colProjectName));
                    detail.ProductRTCID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductRTCID));
                    detail.DeadlineDelivery = TextUtils.ToDate4(grvData.GetRowCellValue(i, colDeadlineDelivery));
                    detail.IsBill = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsBill));

                    detail.ProductType = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductType));//PQ.Chien - UPDATE - 17 / 04 / 2025
                    detail.DateReturnEstimated = TextUtils.ToDate4(grvData.GetRowCellValue(i, colDateReturnEstimated));
                    detail.IsStock = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsStock));
                    detail.UnitName = TextUtils.ToString(grvData.GetRowCellValue(i, colUnitName));
                    detail.ParentProductCode = TextUtils.ToString(grvData.GetRowCellValue(i, colParentProductCode));//ndnhat update 14/10/2025
                    detail.IsPurchase = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsPurchase));//ndnhat update 14/10/2025
                    if (detail.ID > 0)
                    {
                        // =================== Lee Min Khooi Update 04/06/2024 ==================================
                        CreateLogPONCCDetailModel(detail);
                        SQLHelper<PONCCDetailModel>.Update(detail);
                        UpdateBillImportDetail(detail);
                        // =================== END  ==================================

                    }
                    else
                    {
                        detail.ID = SQLHelper<PONCCDetailModel>.Insert(detail).ID;
                    }

                    UpdatePurchaseRequest(TextUtils.ToInt(detail.ProjectPartlistPurchaseRequestID), TextUtils.ToInt(po.SupplierSaleID));

                    string PONCCDetailRequestBuyID = TextUtils.ToString(grvData.GetRowCellValue(i, colPONCCDetailRequestBuyID));
                    UpdatePONCCDetailRequestBuy(detail.ID, PONCCDetailRequestBuyID);


                    string productCode = "";
                    if (detail.ProductSaleID > 0) productCode = TextUtils.ToString(grvData.GetRowCellDisplayText(i, colProductSaleID));
                    else if (detail.ProductRTCID > 0) productCode = TextUtils.ToString(grvData.GetRowCellDisplayText(i, colProductRTCID));
                    AddNotify(PONCCDetailRequestBuyID, detail.ExpectedDate, productCode);
                }

                if (listDelete.Count > 0)
                {
                    SQLHelper<PONCCDetailModel>.DeleteListModel(listDelete);
                    listDelete.Clear();
                }


                // Lee Min Khooi Update 04/06/2024
                //grvDocImport.FocusedRowHandle = -1;
                //for (int i = 0; i < grvDocImport.RowCount; i++)
                //{
                //    int DocId = TextUtils.ToInt(grvDocImport.GetRowCellValue(i, colDocID));
                //    Expression x1 = new Expression("PONCCID", po.ID);
                //    Expression x2 = new Expression("DocumentImportID", DocId);
                //    DocumentImportPONCCModel model = SQLHelper<DocumentImportPONCCModel>.FindByExpression(x1.And(x2)).FirstOrDefault() ?? new DocumentImportPONCCModel();

                //    bool isCheck = TextUtils.ToBoolean(grvDocImport.GetRowCellValue(i, colDocIsChecked));
                //    if (model.Status > 0) continue;
                //    else 
                //    if (isCheck)
                //    {
                //        model.PONCCID = po.ID;
                //        model.DocumentImportID = DocId;
                //        //if (model.ID > 0) DocumentImportPONCCBO.Instance.Update(model);
                //        DocumentImportPONCCBO.Instance.Insert(model);
                //    }
                //}
                UpdateDocument();
                //===================================================================

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Thông báo");
                return false;
            }
        }


        void UpdateRulePay()
        {
            try
            {
                //Update điều khoản thanh toán
                string rulePaySelects = TextUtils.ToString(cboRulePay.EditValue);
                SQLHelper<PONCCRulePayModel>.DeleteByAttribute("PONCCID", po.ID);
                foreach (string item in rulePaySelects.Split(','))
                {
                    PONCCRulePayModel rulePay = new PONCCRulePayModel();
                    rulePay.PONCCID = po.ID;
                    rulePay.RulePayID = TextUtils.ToInt(item);

                    SQLHelper<PONCCRulePayModel>.Insert(rulePay);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n(UpdateRulePay)", "Thông báo");
            }
        }

        void UpdateDocument()
        {
            //try
            //{
            //    grvDocImport.FocusedRowHandle = -1;

            //    Expression x1 = new Expression("PONCCID", po.ID);
            //    Expression x2 = new Expression("Status", 0);
            //    List<DocumentImportPONCCModel> listDocument = SQLHelper<DocumentImportPONCCModel>.FindByExpression(x1.And(x2));
            //    if (listDocument.Count > 0) SQLHelper<DocumentImportPONCCModel>.DeleteListModel(listDocument);

            //    for (int i = 0; i < grvDocImport.RowCount; i++)
            //    {
            //        int DocId = TextUtils.ToInt(grvDocImport.GetRowCellValue(i, colDocID));

            //        Expression x3 = new Expression("DocumentImportID", DocId);
            //        DocumentImportPONCCModel model = SQLHelper<DocumentImportPONCCModel>.FindByExpression(x1.And(x3)).FirstOrDefault() ?? new DocumentImportPONCCModel();

            //        bool isCheck = TextUtils.ToBoolean(grvDocImport.GetRowCellValue(i, colDocIsChecked));
            //        if (model.Status > 0) continue;
            //        if (isCheck)
            //        {
            //            model.PONCCID = po.ID;
            //            model.DocumentImportID = DocId;
            //            //if (model.ID > 0) DocumentImportPONCCBO.Instance.Update(model);
            //            //DocumentImportPONCCBO.Instance.Insert(model);
            //            SQLHelper<DocumentImportPONCCModel>.Insert(model);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.ToString() + "\n(UpdateDocument)", "Thông báo");
            //}
        }


        void UpdatePurchaseRequest(int id, int supplierSaleId)
        {
            try
            {
                ProjectPartlistPurchaseRequestModel request = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByID(id);
                if (request == null) return;
                if (supplierSaleId == request.SupplierSaleID) return;

                request.SupplierSaleID = supplierSaleId;
                if (request.ID > 0)
                {
                    SQLHelper<ProjectPartlistPurchaseRequestModel>.Update(request);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n(UpdatePurchaseRequest)", "Thông báo");
            }
        }


        void UpdatePONCCDetailRequestBuy(int poNCCDetailID, string projectPartlistPurchaseRequestID)
        {
            if (string.IsNullOrWhiteSpace(projectPartlistPurchaseRequestID)) return;
            string[] idRequestBuys = projectPartlistPurchaseRequestID.Split(';');

            var exp1 = new Expression("PONCCDetailID", poNCCDetailID);
            foreach (string idRequestBuy in idRequestBuys)
            {
                var exp2 = new Expression("ProjectPartlistPurchaseRequestID", TextUtils.ToInt(idRequestBuy));
                PONCCDetailRequestBuyModel poRequestBuy = SQLHelper<PONCCDetailRequestBuyModel>.FindByExpression(exp1.And(exp2)).FirstOrDefault();
                poRequestBuy = poRequestBuy ?? new PONCCDetailRequestBuyModel();
                poRequestBuy.PONCCDetailID = poNCCDetailID;
                poRequestBuy.ProjectPartlistPurchaseRequestID = TextUtils.ToInt(idRequestBuy);
                if (poRequestBuy.ID <= 0)
                {
                    SQLHelper<PONCCDetailRequestBuyModel>.Insert(poRequestBuy);
                }
                else
                {
                    SQLHelper<PONCCDetailRequestBuyModel>.Update(poRequestBuy);
                }
            }
        }

        void CalculatorTotalPrice(int rowHandle)
        {
            decimal thanhTien = TextUtils.ToDecimal(grvData.GetRowCellValue(rowHandle, colThanhTien));
            decimal totalMoneyVAT = TextUtils.ToDecimal(grvData.GetRowCellValue(rowHandle, colVATMoney));
            decimal feeShip = TextUtils.ToDecimal(grvData.GetRowCellValue(rowHandle, colFeeShip));
            decimal discount = TextUtils.ToDecimal(grvData.GetRowCellValue(rowHandle, colDiscount));
            decimal totalPrice = thanhTien + totalMoneyVAT + feeShip - discount;
            grvData.SetRowCellValue(rowHandle, colTotalPrice, totalPrice);

            decimal totalMoneyChange = TextUtils.ToInt(cboCurrency.EditValue) != 0 ? totalPrice * TextUtils.ToDecimal(txtCurrencyRate.EditValue) : 0;
            grvData.SetRowCellValue(rowHandle, colCurrencyExchange, totalMoneyChange);

            //CalculatorTotalMoneyPO();
        }

        void AddNotify(string projectPartlistPurchaseRequestID, DateTime? expectDateReturn, string productCode)
        {
            if (string.IsNullOrWhiteSpace(projectPartlistPurchaseRequestID)) return;
            string[] idRequestBuys = projectPartlistPurchaseRequestID.Split(';');
            foreach (string idRequestBuy in idRequestBuys)
            {
                ProjectPartlistPurchaseRequestModel requestBuy = SQLHelper<ProjectPartlistPurchaseRequestModel>.FindByID(TextUtils.ToInt(idRequestBuy));
                //string productCode = TextUtils.ToString(grvData.GetRowCellValue(row, colProductCode));
                string dateReturn = expectDateReturn.HasValue ? expectDateReturn.Value.ToString("dd/MM/yyyy") : "";
                string textNotify = $"Sản phầm [{productCode}] đã đặt hàng\n" +
                                    $"Ngày về dự kiến: {dateReturn}";

                //int employee = TextUtils.ToInt(grvData.GetRowCellValue(row, colEmployeeID));
                TextUtils.AddNotify("THÔNG BÁO ĐẶT HÀNG", textNotify, TextUtils.ToInt(requestBuy.EmployeeID));
            }


        }

        //void CalculatorTotalMoneyPO()
        //{
        //    grvData.FocusedRowHandle = -1;

        //    DataTable dt = (DataTable)grdData.DataSource;
        //    dt.AcceptChanges();

        //    decimal totalMoneyPO = dt.AsEnumerable().Sum(x => x.Field<decimal>("TotalPrice"));

        //    //txtTotalMoneyPO.EditValue = colTotalPrice.SummaryItem.SummaryValue;
        //    txtTotalMoneyPO.EditValue = totalMoneyPO;
        //}

        void CalculatorDetail(int row)
        {
            decimal quantity = TextUtils.ToDecimal(grvData.GetRowCellValue(row, colQtyRequest));
            decimal unitPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(row, colUnitPrice));
            decimal thanhTien = TextUtils.ToDecimal(grvData.GetRowCellValue(row, colThanhTien));
            decimal vat = TextUtils.ToDecimal(grvData.GetRowCellValue(row, colVAT));
            decimal vatMoney = TextUtils.ToDecimal(grvData.GetRowCellValue(row, colVATMoney));
            decimal discountPercent = TextUtils.ToDecimal(grvData.GetRowCellValue(row, colDiscountPercent));
            decimal discount = TextUtils.ToDecimal(grvData.GetRowCellValue(row, colDiscount));
        }

        private void cboSupplierSale_EditValueChanged(object sender, EventArgs e)
        {

            if (po.ID <= 0)
            {
                LoadPOCode();
            }

            SupplierSaleModel supplier = (SupplierSaleModel)cboSupplierSale.GetSelectedDataRow();
            if (supplier == null) return;
            txtAccountNumberSupplier.Text = supplier.SoTK;
            txtBankSupplier.Text = supplier.SoTK;
            txtAddressSupplier.Text = supplier.AddressNCC;
            txtMaSoThueNCC.Text = supplier.MaSoThue;

            cboRulePay.EditValue = supplier.RulePayID;
            chkDeptSupplier.Checked = supplier.IsDebt;
            txtFedexAccount.Text = supplier.FedexAccount;
            txtOriginItem.Text = supplier.OriginItem;
            txtBankCharge.Text = supplier.BankCharge;
            txtAddressDelivery.Text = supplier.AddressDelivery;
            txtNote.Text = supplier.Description;
            cboCompany.SelectedIndex = supplier.Company;
            txtRuleIncoterm.Text = supplier.RuleIncoterm;
        }

        //private void btnSave_Click(object sender, EventArgs e)
        //{
        //    if (SaveData())
        //    {
        //        this.DialogResult = DialogResult.OK;
        //    }
        //}

        private void btnSaveNew_Click(object sender, EventArgs e)
        {

            if (SaveData())
            {
                po = new PONCCModel();
                LoadData();
                this.Text = "CHI TIẾT PO NCC";
                this.Tag = po.POCode;
            }
        }

        private void cboCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            //colCurrencyExchange.Visible = cboCurrency.SelectedIndex != 0;
            //if (cboCurrency.SelectedIndex == 0) txtCurrencyRate.EditValue = 0;
            //for (int i = 0; i < grvData.RowCount; i++)
            //{
            //    CalculatorTotalPrice(i);
            //}
        }

        private void txtCurrencyRate_EditValueChanged(object sender, EventArgs e)
        {
            //for (int i = 0; i < grvData.RowCount; i++)
            //{
            //    CalculatorTotalPrice(i);
            //}
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            grvData.SelectRow(grvData.FocusedRowHandle);
            string productNewCode = TextUtils.ToString(grvData.GetFocusedRowCellDisplayText(colProductNewCode));
            DialogResult dialog = MessageBox.Show($"Bạn có chắc muốn xoá sản phẩm [{productNewCode}] khỏi danh sách không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog != DialogResult.Yes) return;
            int id = TextUtils.ToInt(grvData.GetFocusedRowCellDisplayText(colID));

            PONCCDetailModel detail = SQLHelper<PONCCDetailModel>.FindByID(id);
            listDelete.Add(detail);
            grvData.DeleteSelectedRows();

            //var row = dt.Rows[0].Delete;
            CalculatorTotalPrice();
        }

        private void frmPONCCDetailNew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DataTable dtChange = dt.GetChanges();
                if (dtChange != null)
                {
                    DialogResult dialog = MessageBox.Show("Những thay đổi chưa được lưu.\n Bạn có muốn lưu lại thông tin trước khi thoát không?", "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (dialog == DialogResult.Yes)
                    {
                        btnSaveAndClose_Click(null, null);
                    }
                    else if (dialog == DialogResult.No)
                    {
                        //this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    //this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                //MessageBox.Show(e.KeyCode.ToString());
                btnSave_Click(null, null);
            }
            else if (e.Control && e.KeyCode == Keys.A)
            {
                //MessageBox.Show(e.KeyCode.ToString());
            }
        }

        bool isRecallCellValueChanged = false;
        private void grvData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (Lib.LockEvents) return;
            try
            {
                Lib.LockEvents = true;
                grvData.FocusedRowHandle = -1;


                //if (!e.Column.OptionsColumn.AllowEdit) return;
                if (e.Column != colProductSaleID &&
                    e.Column != colQtyRequest &&
                    e.Column != colUnitPrice &&
                    e.Column != colVAT &&
                    e.Column != colVATMoney &&
                    e.Column != colDiscountPercent &&
                    e.Column != colDiscount &&
                    e.Column != colFeeShip &&
                    e.Column != colThanhTien &&
                    e.Column != colExpectedDate &&
                    e.Column != colActualDate) return;

                //Update thông tin sản phầm
                //if (e.Column == colProductSaleID)
                //{
                //    int productSaleId = TextUtils.ToInt(grvData.GetFocusedRowCellValue(e.Column));
                //    ProductSaleModel product = (ProductSaleModel)cboProductSale.GetRowByKeyValue(productSaleId);
                //    if (product == null) return;
                //    grvData.SetFocusedRowCellValue(colProductName, product.ProductName);
                //    grvData.SetFocusedRowCellValue(colProductNewCode, product.ProductNewCode);
                //    grvData.SetFocusedRowCellValue(colUnit, product.Unit);
                //}

                //Update thành tiền
                if (e.Column == colQtyRequest || e.Column == colUnitPrice)
                {
                    decimal quantity = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colQtyRequest));
                    decimal unitPrice = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colUnitPrice));

                    decimal thanhTien = quantity * unitPrice;
                    grvData.SetFocusedRowCellValue(colThanhTien, thanhTien);

                    //Update VAT
                    decimal vat = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colVAT));
                    decimal totalMoneyVAT = (thanhTien * vat) / 100;
                    grvData.SetFocusedRowCellValue(colVATMoney, totalMoneyVAT);

                    //Update chiết khấu
                    decimal discount = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colDiscountPercent));
                    decimal totalMoneyDiscount = (thanhTien * discount) / 100;
                    grvData.SetFocusedRowCellValue(colDiscount, totalMoneyDiscount);
                }

                if (e.Column == colThanhTien)
                {
                    decimal quantity = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colQtyRequest));
                    decimal thanhTien = TextUtils.ToDecimal(e.Value);

                    //Update đơn giá
                    if (quantity > 0)
                    {
                        decimal unitPrice = thanhTien / quantity;
                        grvData.SetFocusedRowCellValue(colUnitPrice, unitPrice);
                    }

                    //Update VAT
                    decimal vat = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colVAT));
                    decimal totalMoneyVAT = (thanhTien * vat) / 100;
                    grvData.SetFocusedRowCellValue(colVATMoney, totalMoneyVAT);

                    //Update chiết khấu
                    decimal discount = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colDiscountPercent));
                    decimal totalMoneyDiscount = (thanhTien * discount) / 100;
                    grvData.SetFocusedRowCellValue(colDiscount, totalMoneyDiscount);
                }


                //Update tổng tiền VAT và % VAT
                if (e.Column == colVAT)
                {
                    decimal thanhTien = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colThanhTien));
                    decimal vat = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colVAT));
                    decimal totalMoneyVAT = (thanhTien * vat) / 100;
                    grvData.SetFocusedRowCellValue(colVATMoney, totalMoneyVAT);
                    grvData.SetFocusedRowCellValue(colIsBill, (vat > 0));
                }

                if (e.Column == colVATMoney)
                {
                    decimal thanhTien = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colThanhTien));
                    decimal totalMoneyVAT = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colVATMoney));
                    decimal vat = thanhTien <= 0 ? 0 : totalMoneyVAT / thanhTien;
                    grvData.SetFocusedRowCellValue(colVAT, vat * 100);
                }

                //Update chiết khấu
                if (e.Column == colDiscountPercent)
                {
                    decimal thanhTien = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colThanhTien));
                    decimal discount = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colDiscountPercent));
                    decimal totalMoneyDiscount = (thanhTien * discount) / 100;
                    grvData.SetFocusedRowCellValue(colDiscount, totalMoneyDiscount);
                }

                if (e.Column == colDiscount)
                {
                    decimal thanhTien = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colThanhTien));
                    decimal discount = TextUtils.ToDecimal(grvData.GetFocusedRowCellValue(colDiscount));
                    decimal discountPercent = thanhTien <= 0 ? 0 : discount / thanhTien;
                    grvData.SetFocusedRowCellValue(colDiscountPercent, discountPercent * 100);
                }

                CalculatorTotalPrice(e.RowHandle);
                CalculatorTotalPrice();

                if (e.Column == colExpectedDate || e.Column == colActualDate || e.Column == colVAT)
                {
                    if (isRecallCellValueChanged == true) return;
                    try
                    {
                        isRecallCellValueChanged = true;
                        if (e.Value != null && grvData.SelectedRowsCount > 0)
                        {
                            foreach (int row in grvData.GetSelectedRows())
                            {
                                grvData.SetRowCellValue(row, grvData.Columns[e.Column.FieldName], e.Value);

                                if (e.Column == colVAT)
                                {
                                    decimal thanhTien = TextUtils.ToDecimal(grvData.GetRowCellValue(row, colThanhTien));
                                    decimal vat = TextUtils.ToDecimal(grvData.GetRowCellValue(row, colVAT));
                                    decimal totalMoneyVAT = (thanhTien * vat) / 100;
                                    grvData.SetRowCellValue(row, colVATMoney, totalMoneyVAT);
                                }

                            }
                        }
                    }
                    finally
                    {
                        isRecallCellValueChanged = false;
                    }
                }
            }
            finally
            {
                Lib.LockEvents = false;

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
                    dt.AcceptChanges();
                    DataRow dtrow = dt.NewRow();

                    int stt = 0;
                    if (dt.Rows.Count > 0)
                    {
                        stt = dt.AsEnumerable().Max(x => x.Field<int>("STT"));
                    }

                    int projectID = TextUtils.ToInt(grvData.GetRowCellValue(grvData.FocusedRowHandle, colProjectID));
                    string projectName = TextUtils.ToString(grvData.GetRowCellValue(grvData.FocusedRowHandle, colProjectName));

                    dtrow["STT"] = stt + 1;
                    //dtrow["ProjectID"] = projectID;
                    //dtrow["ProjectName"] = projectName;
                    dt.Rows.Add(dtrow);
                }
            }
        }

        private void btnSelectPurchaseRequest_Click(object sender, EventArgs e)
        {
            frmProjectPartlistPurchaseRequest frm = new frmProjectPartlistPurchaseRequest();
            frm.isSelectedPO = true;
            frm.supplierSaleId = TextUtils.ToInt(cboSupplierSale.EditValue);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (listRequest.Count > 0)
                {
                    int stt = dt.Rows.Count;
                    for (int i = 0; i < listRequest.Count; i++)
                    {
                        ProjectPartlistPurchaseRequestModel item = listRequest[i];
                        ProductSaleModel product = (ProductSaleModel)cboProductSale.GetRowByKeyValue(item.ProductSaleID);
                        DataRow dataRow = dt.NewRow();
                        dataRow["STT"] = stt + i + 1;
                        dataRow["ProductSaleID"] = item.ProductSaleID;
                        dataRow["ProductName"] = product.ProductName;
                        dataRow["ProductNewCode"] = product.ProductNewCode;
                        dataRow["Unit"] = product.Unit;
                        dataRow["ProjectPartlistPurchaseRequestID"] = item.ID;
                        dataRow["QtyRequest"] = item.Quantity;
                        dataRow["UnitPrice"] = item.UnitPrice;
                        dataRow["ThanhTien"] = item.Quantity * item.UnitPrice;
                        dataRow["TotalPrice"] = item.Quantity * item.UnitPrice;
                        dt.Rows.Add(dataRow);
                    }
                    dt.AcceptChanges();
                }

                grdData.DataSource = dt;
            }
        }



        private void label38_Click(object sender, EventArgs e)
        {

        }

        private void cboCurrency_EditValueChanged(object sender, EventArgs e)
        {
            CurrencyModel currency = (CurrencyModel)cboCurrency.GetSelectedDataRow();
            //if (currency == null) return;
            txtCurrencyRate.Text = currency == null ? "" : TextUtils.ToString(currency.CurrencyRate);

            if (currency != null)
            {
                bool isExpried = (DateTime.Now.Date >= currency.DateStart.Value.Date && DateTime.Now.Date <= currency.DateExpried.Value.Date);

                txtCurrencyRate.Text = isExpried ? currency.CurrencyRate.ToString() : "";
                colCurrencyExchange.Visible = currency.Code.Trim().ToLower() != "vnd";
            }
            else
            {
                txtCurrencyRate.Text = "";
                colCurrencyExchange.Visible = false;
            }

            for (int i = 0; i < grvData.RowCount; i++)
            {
                CalculatorTotalPrice(i);
            }

            CalculatorTotalPrice();
        }

        private void btnAddSupplierSale_Click(object sender, EventArgs e)
        {
            frmSupplierSaleDetail frm = new frmSupplierSaleDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadSupplierSale();
            }
        }

        private void btnPrintPOVietNamese_Click(object sender, EventArgs e)
        {
            ShowDetail(1);
        }

        private void btnPrintPOEnglish_Click(object sender, EventArgs e)
        {
            ShowDetail(2);
        }


        void ShowDetail(int type)
        {
            grvData.FocusedRowHandle = -1;
            //int id = TextUtils.ToInt(grvData.GetFocusedRowCellValue(colID));
            if (po.ID <= 0)
            {
                MessageBox.Show("Vui lòng lưu trước khi In PO!", "Thông báo");
                return;
            }
            else
            {
                if (IsChangePO())
                {
                    MessageBox.Show("Nội dung PO đã được thay đổi, Vui lòng lưu trước khi In!", "Thông báo");
                    return;
                }
                //var dataChange = dt.GetChanges();
                //if (dataChange != null)
                //{
                //    MessageBox.Show("Bạn vừa thay đổi thông tin sản phẩm.\nVui lòng lưu trước khi In PO", "Thông báo");
                //    return;
                //}

                //string currencyRatePrint = String.Format("{0:0.00}", txtCurrencyRate.EditValue);
                //string totalMoneyPOPrint = String.Format("{0:0.00}", txtTotalMoneyPO.EditValue);
                //var poPrint = new PONCCModel()
                //{
                //    SupplierSaleID = TextUtils.ToInt(cboSupplierSale.EditValue),
                //    POCode = txtPOCode.Text.Trim(),
                //    Note = txtNote.Text.Trim(),
                //    EmployeeID = TextUtils.ToInt(cboEmployee.EditValue),
                //    RulePay = TextUtils.ToString(cboRulePay.EditValue),
                //    Company = cboCompany.SelectedIndex,
                //    RequestDate = dtpRequestDate.Value.Date,
                //    BillCode = txtBillCode.Text.Trim(),
                //    Status = cboStatus.SelectedIndex,
                //    TotalMoneyPO = TextUtils.ToDecimal(totalMoneyPOPrint),
                //    CurrencyID = TextUtils.ToInt(cboCurrency.EditValue),
                //    CurrencyRate = TextUtils.ToDecimal(currencyRatePrint),
                //    DeliveryDate = dtpDeliveryDate.Value.Date,
                //    AddressDelivery = txtAddressDelivery.Text.Trim(),
                //    OtherTerms = txtOtherTerms.Text.Trim(),
                //    AccountNumberSupplier = txtAccountNumberSupplier.Text.Trim(),
                //    FedexAccount = txtFedexAccount.Text.Trim(),
                //    OriginItem = txtOriginItem.Text.Trim(),
                //    SupplierVoucher = txtSupplierVoucher.Text.Trim(),
                //    BankCharge = txtBankCharge.Text.Trim(),
                //    RuleIncoterm = txtRuleIncoterm.Text.Trim(),
                //    OrderTargets = txtOrderTarget.Text.Trim(),
                //    NCCNew = chkNCCNew.Checked,
                //    DeptSupplier = chkDeptSupplier.Checked
                //};

                //PONCCModel poNcc = SQLHelper<PONCCModel>.FindByID(po.ID);
                //var rulePays = SQLHelper<PONCCRulePayModel>.FindByAttribute("PONCCID", po.ID).Select(x => x.RulePayID).ToList();

                //string currencyRate = String.Format("{0:0.00}", poNcc.CurrencyRate);
                //string totalMoneyPO = String.Format("{0:0.00}", poNcc.TotalMoneyPO);
                //var poOld = new PONCCModel()
                //{
                //    SupplierSaleID = poNcc.SupplierSaleID,
                //    POCode = poNcc.POCode.Trim(),
                //    Note = poNcc.Note.Trim(),
                //    EmployeeID = poNcc.EmployeeID,
                //    RulePay = string.Join(", ", rulePays),
                //    Company = poNcc.Company,
                //    RequestDate = poNcc.RequestDate.Value.Date,
                //    BillCode = poNcc.BillCode.Trim(),
                //    Status = poNcc.Status,
                //    TotalMoneyPO = TextUtils.ToDecimal(totalMoneyPO),
                //    CurrencyID = poNcc.CurrencyID,
                //    CurrencyRate = TextUtils.ToDecimal(currencyRate),
                //    DeliveryDate = poNcc.DeliveryDate.Value.Date,
                //    AddressDelivery = poNcc.AddressDelivery,
                //    OtherTerms = poNcc.OtherTerms.Trim(),
                //    AccountNumberSupplier = poNcc.AccountNumberSupplier.Trim(),
                //    FedexAccount = poNcc.FedexAccount.Trim(),
                //    OriginItem = poNcc.OriginItem.Trim(),
                //    SupplierVoucher = poNcc.SupplierVoucher.Trim(),
                //    BankCharge = poNcc.BankCharge.Trim(),
                //    RuleIncoterm = poNcc.RuleIncoterm.Trim(),
                //    OrderTargets = poNcc.OrderTargets.Trim(),
                //    NCCNew = poNcc.NCCNew,
                //    DeptSupplier = poNcc.DeptSupplier
                //};

                //var compareObject = TextUtils.DeepEquals(poPrint, poOld);
                //bool equal = TextUtils.ToBoolean(compareObject.GetType().GetProperty("equal").GetValue(compareObject));
                //var property = compareObject.GetType().GetProperty("property").GetValue(compareObject);
                //if (!equal)
                //{
                //    MessageBox.Show("Bạn vừa thay đổi thông tin PO.\nVui lòng lưu trước khi In PO", "Thông báo");
                //    return;
                //}
            }


            frmPONCCViewDetail frm = new frmPONCCViewDetail();
            frm.poId = po.ID;
            frm.type = type;
            frm.Show();
        }

        void CalculatorTotalPrice()
        {
            try
            {
                grvData.FocusedRowHandle = -1;
                var summarys = grvData.Columns[colTotalPrice.FieldName].Summary;
                if (summarys.Count > 0)
                {
                    grvData.Columns[colTotalPrice.FieldName].Summary.Clear();
                }

                DataTable data = (DataTable)grdData.DataSource;
                if (data == null || data.Rows.Count <= 0) return;
                data.AcceptChanges();

                var dataPrice = data.AsEnumerable().Select(x => TextUtils.ToDecimal(x.Field<object>("TotalPrice")).ToString("n2")).ToArray();
                var dataTotalPrice = dataPrice.Select(decimal.Parse).ToArray();

                var totalPrice = dataTotalPrice.Sum();

                grvData.Columns[colTotalPrice.FieldName].Summary.Add(new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Custom, colTotalPrice.FieldName, $"{totalPrice.ToString("n2")}"));
                txtTotalMoneyPO.EditValue = totalPrice;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }


        bool IsChangePO()
        {
            var dataChange = dt.GetChanges();
            if (dataChange != null)
            {
                return true;
            }

            string currencyRatePrint = String.Format("{0:0.00}", txtCurrencyRate.EditValue);
            string totalMoneyPOPrint = String.Format("{0:0.00}", txtTotalMoneyPO.EditValue);
            var poPrint = new PONCCModel()
            {
                SupplierSaleID = TextUtils.ToInt(cboSupplierSale.EditValue),
                POCode = txtPOCode.Text.Trim(),
                Note = txtNote.Text.Trim(),
                EmployeeID = TextUtils.ToInt(cboEmployee.EditValue),
                RulePay = TextUtils.ToString(cboRulePay.EditValue),
                Company = cboCompany.SelectedIndex,
                RequestDate = dtpRequestDate.Value.Date,
                BillCode = txtBillCode.Text.Trim(),
                Status = cboStatus.SelectedIndex,
                TotalMoneyPO = TextUtils.ToDecimal(totalMoneyPOPrint),
                CurrencyID = TextUtils.ToInt(cboCurrency.EditValue),
                CurrencyRate = TextUtils.ToDecimal(currencyRatePrint),
                DeliveryDate = dtpDeliveryDate.Value.Date,
                AddressDelivery = txtAddressDelivery.Text.Trim(),
                OtherTerms = txtOtherTerms.Text.Trim(),
                AccountNumberSupplier = txtAccountNumberSupplier.Text.Trim(),
                FedexAccount = txtFedexAccount.Text.Trim(),
                OriginItem = txtOriginItem.Text.Trim(),
                SupplierVoucher = txtSupplierVoucher.Text.Trim(),
                BankCharge = txtBankCharge.Text.Trim(),
                RuleIncoterm = txtRuleIncoterm.Text.Trim(),
                OrderTargets = txtOrderTarget.Text.Trim(),
                NCCNew = chkNCCNew.Checked,
                DeptSupplier = chkDeptSupplier.Checked
            };

            if (po.ID <= 0) return false;
            PONCCModel poNcc = SQLHelper<PONCCModel>.FindByID(po.ID);
            var rulePays = SQLHelper<PONCCRulePayModel>.FindByAttribute("PONCCID", po.ID).Select(x => x.RulePayID).ToList();

            string currencyRate = String.Format("{0:0.00}", poNcc.CurrencyRate);
            string totalMoneyPO = String.Format("{0:0.00}", poNcc.TotalMoneyPO);
            var poOld = new PONCCModel()
            {
                SupplierSaleID = poNcc.SupplierSaleID,
                POCode = poNcc.POCode.Trim(),
                Note = poNcc.Note.Trim(),
                EmployeeID = poNcc.EmployeeID,
                RulePay = string.Join(", ", rulePays),
                Company = poNcc.Company,
                RequestDate = poNcc.RequestDate.Value.Date,
                BillCode = poNcc.BillCode.Trim(),
                Status = poNcc.Status,
                TotalMoneyPO = TextUtils.ToDecimal(totalMoneyPO),
                CurrencyID = poNcc.CurrencyID,
                CurrencyRate = TextUtils.ToDecimal(currencyRate),
                DeliveryDate = poNcc.DeliveryDate.Value.Date,
                AddressDelivery = poNcc.AddressDelivery,
                OtherTerms = poNcc.OtherTerms.Trim(),
                AccountNumberSupplier = poNcc.AccountNumberSupplier.Trim(),
                FedexAccount = poNcc.FedexAccount.Trim(),
                OriginItem = poNcc.OriginItem.Trim(),
                SupplierVoucher = poNcc.SupplierVoucher.Trim(),
                BankCharge = poNcc.BankCharge.Trim(),
                RuleIncoterm = poNcc.RuleIncoterm.Trim(),
                OrderTargets = poNcc.OrderTargets.Trim(),
                NCCNew = poNcc.NCCNew,
                DeptSupplier = poNcc.DeptSupplier
            };

            //569029200.00M,
            //592595568.00M

            var compareObject = TextUtils.DeepEquals(poPrint, poOld);
            bool equal = TextUtils.ToBoolean(compareObject.GetType().GetProperty("equal").GetValue(compareObject));
            List<string> propertys = (List<string>)compareObject.GetType().GetProperty("property").GetValue(compareObject);
            if (!equal)
            {
                MessageBox.Show($"Bạn vừa thay đổi thông tin PO.\r\n({string.Join(",", propertys)})", "Thông báo");
                return true;
            }

            return false;
        }
        private void frmPONCCDetailNew_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.DialogResult = DialogResult.OK;
            //var dataChange = dt.GetChanges();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                if (listRequest.Count > 0) listRequest.Clear();
                LoadDetail();
                MessageBox.Show("Lưu thành công!", "Thông báo");
            }
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                dt.AcceptChanges();
                this.Close();
                //this.DialogResult = DialogResult.OK;
            }
        }

        int[] employeePurchaseIDs = new int[] { 49, 179 };
        private void cboProductSale_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit lookUpEdit = (SearchLookUpEdit)sender;
            //ProductSaleModel product = (ProductSaleModel)lookUpEdit.GetSelectedDataRow();

            DataRowView dataRow = (DataRowView)lookUpEdit.GetSelectedDataRow();

            grvData.SetFocusedRowCellValue(colProductRTCID, 0);

            grvData.SetFocusedRowCellValue(colProductSaleID, dataRow == null ? 0 : TextUtils.ToInt(dataRow["ID"]));
            grvData.SetFocusedRowCellValue(colProductName, dataRow == null ? "" : TextUtils.ToString(dataRow["ProductName"]));
            grvData.SetFocusedRowCellValue(colProductNewCode, dataRow == null ? "" : TextUtils.ToString(dataRow["ProductNewCode"]));
            //grvData.SetFocusedRowCellValue(colUnit, dataRow == null ? "" : TextUtils.ToString(dataRow["Unit"]));
            grvData.SetFocusedRowCellValue(colUnitName, dataRow == null ? "" : TextUtils.ToString(dataRow["Unit"]));

            string productCode = dataRow == null ? "" : TextUtils.ToString(dataRow["ProductCode"]);
            string productName = dataRow == null ? "" : TextUtils.ToString(dataRow["ProductName"]);

            string productCodeOfSupplier = employeePurchaseIDs.Contains(Global.EmployeeID) ? productName : productName + " " + productCode;


            grvData.SetFocusedRowCellValue(colProductCodeOfSupplier, productCodeOfSupplier);
            grvData.SetFocusedRowCellValue(colProductGroupName, dataRow == null ? "" : TextUtils.ToString(dataRow["ProductGroupName"]));

            int productId = dataRow == null ? 0 : TextUtils.ToInt(dataRow["ID"]);// product.ID;
            //string productCode = dataRow == null ? "" : TextUtils.ToString(dataRow["ProductCode"]);// product.ProductCode;
            if (!string.IsNullOrEmpty(productCode))
            {
                //DataTable dt = TextUtils.LoadDataFromSP("spGetHistoryPricePartlist", "A", new string[] { "@ProductSaleID" }, new object[] { productId });
                //DataRow dataHistoryPrice = dt.AsEnumerable().FirstOrDefault(x => x.Field<string>("ProductCode") == productCode);
                DataTable dt = TextUtils.LoadDataFromSP("spGetHistoryPricePartlistForProduct", "A", new string[] { "@ProductSaleID", "@ProductCode" }, new object[] { productId, productCode });
                DataRow dataHistoryPrice = dt.AsEnumerable().FirstOrDefault();

                decimal lastHistoryPrice = dataHistoryPrice != null ? TextUtils.ToDecimal(dataHistoryPrice["UnitPrice"]) : 0;
                grvData.SetFocusedRowCellValue(colPriceHistory, lastHistoryPrice);
            }
        }

        private void btnAddRulePay_Click(object sender, EventArgs e)
        {
            frmRulePayDetail frm = new frmRulePayDetail();
            //TextUtils.OpenChildForm(frm, null);
            //frm.FormClosed += Frm_FormClosed;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadRulePay();
            }
        }

        private void Frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadRulePay();
            LoadDocumentImport();
        }

        private void cboCurrency_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //var button = e.Button;
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Search)
            {
                LoadCurrency();
                cboCurrency_EditValueChanged(null, null);
                CalculatorTotalPrice();
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            frmProductDetailSale frm = new frmProductDetailSale();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadProductSale();
            }
        }

        private void cboProject_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit lookUpEdit = (SearchLookUpEdit)sender;
            ProjectModel project = (ProjectModel)lookUpEdit.GetSelectedDataRow();
            if (project != null)
            {
                grvData.SetFocusedRowCellValue(colProjectID, project.ID);
                grvData.SetFocusedRowCellValue(colProjectName, project.ProjectName);
            }
            else
            {
                //DataRow row = grvData.GetFocusedDataRow();
                //int index = dt.Rows.IndexOf(grvData.GetFocusedDataRow());
                dt.Rows[grvData.FocusedRowHandle]["ProjectName"] = dt.Rows[grvData.FocusedRowHandle]["ProjectName", DataRowVersion.Original];
            }

            //grvData.SetFocusedRowCellValue(colProjectID, project == null ? 0 : project.ID);
            //grvData.SetFocusedRowCellValue(colProjectName, project == null ? "" : project.ProjectName);

        }

        private void cboProductRTC_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit lookUpEdit = (SearchLookUpEdit)sender;
            DataRowView dataRow = (DataRowView)lookUpEdit.GetSelectedDataRow();

            grvData.SetFocusedRowCellValue(colProductSaleID, 0);

            grvData.SetFocusedRowCellValue(colProductRTCID, dataRow == null ? 0 : TextUtils.ToInt(dataRow["ID"]));
            grvData.SetFocusedRowCellValue(colProductName, dataRow == null ? "" : TextUtils.ToString(dataRow["ProductName"]));
            grvData.SetFocusedRowCellValue(colProductNewCode, dataRow == null ? "" : TextUtils.ToString(dataRow["ProductCodeRTC"]));
            //grvData.SetFocusedRowCellValue(colUnit, dataRow == null ? "" : TextUtils.ToString(dataRow["UnitCountName"]));
            grvData.SetFocusedRowCellValue(colUnitName, dataRow == null ? "" : TextUtils.ToString(dataRow["UnitCountName"]));

            //string productCodeOfSupplier = dataRow == null ? "" : TextUtils.ToString(dataRow["ProductName"]) + " " + TextUtils.ToString(dataRow["ProductCode"]);

            string productCode = dataRow == null ? "" : TextUtils.ToString(dataRow["ProductCode"]);
            string productName = dataRow == null ? "" : TextUtils.ToString(dataRow["ProductName"]);

            string productCodeOfSupplier = employeePurchaseIDs.Contains(Global.EmployeeID) ? productName : productName + " " + productCode;

            grvData.SetFocusedRowCellValue(colProductCodeOfSupplier, productCodeOfSupplier);
            grvData.SetFocusedRowCellValue(colProductGroupName, dataRow == null ? "" : TextUtils.ToString(dataRow["ProductGroupName"]));

            int productId = dataRow == null ? 0 : TextUtils.ToInt(dataRow["ID"]);
            //string productCode = dataRow == null ? "" : TextUtils.ToString(dataRow["ProductCode"]);
            if (!string.IsNullOrEmpty(productCode))
            {
                DataTable dt = TextUtils.LoadDataFromSP("spGetHistoryPricePartlistForProduct", "A", new string[] { "@ProductRTCID", "@ProductCode" }, new object[] { productId, productCode });
                DataRow dataHistoryPrice = dt.AsEnumerable().FirstOrDefault();

                decimal lastHistoryPrice = dataHistoryPrice != null ? TextUtils.ToDecimal(dataHistoryPrice["UnitPrice"]) : 0;
                grvData.SetFocusedRowCellValue(colPriceHistory, lastHistoryPrice);
            }
        }

        private void grvData_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {

        }

        private void frmPONCCDetailNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsChangePO())
            {
                DialogResult dialog = MessageBox.Show("Nội dung PO đã được thay đổi, bạn có muốn lưu hay không?", "Thông báo", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    btnSaveAndClose_Click(null, null);
                }
                else if (dialog == DialogResult.No)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
        // Lee Min Khooi Update 04/06/2024
        private void DocChecked_Click(object sender, EventArgs e)
        {
            int curentRow = grvDocImport.FocusedRowHandle;
            bool isCheck = TextUtils.ToBoolean(grvDocImport.GetRowCellValue(curentRow, colDocIsChecked));
            int status = TextUtils.ToInt(grvDocImport.GetRowCellValue(curentRow, colDocStatus));
            string statusText = TextUtils.ToString(grvDocImport.GetRowCellValue(curentRow, "StatusText"));
            if (status > 0)
            {
                grvDocImport.SetRowCellValue(curentRow, "IsChecked", isCheck);
                MessageBox.Show($"Hồ sơ đi kèm [{statusText}], không thể sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }


        // Tạo log khi update PONCCDetailModel
        private void CreateLogPONCCDetailModel(PONCCDetailModel detail)
        {
            try
            {
                PONCCDetailModel oldDetail = SQLHelper<PONCCDetailModel>.FindByID(detail.ID) ?? new PONCCDetailModel();
                if (oldDetail.ID == 0) return;
                var resultCompare = TextUtils.DeepEquals(oldDetail, detail);
                bool equal = TextUtils.ToBoolean(resultCompare.GetType().GetProperty("equal").GetValue(resultCompare));

                if (!equal)
                {
                    string contentLog = "";
                    List<string> propertys = (List<string>)resultCompare.GetType().GetProperty("property").GetValue(resultCompare);
                    foreach (string property in propertys)
                    {
                        if (property == "Note") continue;

                        string propertyText = TextUtils.ToString(cGlobVar.ponccDetails.GetType().GetProperty(property).GetValue(cGlobVar.ponccDetails));
                        contentLog += $"{propertyText.ToUpper()}:\n" +
                                      $"từ {oldDetail.GetType().GetProperty(property).GetValue(oldDetail)}\n" +
                                      $"thành {detail.GetType().GetProperty(property).GetValue(detail)}\n\n";
                    }

                    PONCCDetailLogModel log = new PONCCDetailLogModel();
                    log.DateLog = DateTime.Now;
                    log.ContentLog = contentLog;

                    SQLHelper<PONCCDetailLogModel>.Insert(log);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + $"\n(CreateLogPONCCDetailModel)", "Thông báo");
            }
        }

        // Update BillImportDetail khi update lại PONCCDetailModel
        private void UpdateBillImportDetail(PONCCDetailModel detail)
        {

            //List<BillImportDetailModel> billImports = SQLHelper<BillImportDetailModel>.FindByAttribute("PONCCDetailID", detail.ID);
            //foreach (BillImportDetailModel item in billImports)
            //{

            //}

            try
            {
                if (lstBillImportId.Count <= 0 || detail.ID == 0) return;
                for (int i = 0; i < lstBillImportId.Count; i++)
                {
                    Expression ep1 = new Expression("BillImportID", lstBillImportId[i]);
                    Expression ep2 = new Expression("PONCCDetailID", detail.ID);
                    List<BillImportDetailModel> lst = SQLHelper<BillImportDetailModel>.FindByExpression(ep1.And(ep2));
                    foreach (BillImportDetailModel item in lst)
                    {
                        item.ProductID = detail.ProductSaleID;
                        item.Price = detail.UnitPrice;
                        item.QtyRequest = detail.QtyRequest;
                        item.ProjectCode = detail.ProductCodeOfSupplier;
                        item.ProjectID = detail.ProjectID;
                        if (item.ID <= 0)
                        {
                            SQLHelper<BillImportDetailModel>.Insert(item);
                        }
                        else
                        {
                            SQLHelper<BillImportDetailModel>.Update(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + $"\n(UpdateBillImportDetail)", "Thông báo");
            }
        }

        private void grdDocImport_Click(object sender, EventArgs e)
        {

        }
        //=========================lee min khooi update 27/09/2024 ==========================================

        private void btnOpenYCMH_Click(object sender, EventArgs e)
        {
            frmProjectPartlistPurchaseRequest frm = new frmProjectPartlistPurchaseRequest();
            frm.isYCMH = true;
            frm.supplierSaleId = TextUtils.ToInt(cboSupplierSale.EditValue);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.lstYCMH.Count <= 0) return;
                string lstRequestBuyIDs = String.Join(";", frm.lstYCMH);
                string lstCodes = String.Join("; ", frm.lstYCMHCode);
                grvData.SetFocusedRowCellValue(colPONCCDetailRequestBuyID, lstRequestBuyIDs);
                grvData.SetFocusedRowCellValue(colChooseYCMH, lstCodes);
                //MessageBox.Show("Chọn YCMH thành công!", TextUtils.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cboPOType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (!Global.IsAdmin) return;
            int value = TextUtils.ToInt(cboPOType.SelectedIndex);
            if (cboPOType.SelectedIndex == 1) //PO mượn
            {
                grvData.Columns[$"{colBiddingPrice.FieldName}"].Caption = "Giá NCC";
                grvData.Columns[$"{colDateReturnEstimated.FieldName}"].VisibleIndex = grvData.Columns[colActualDate.FieldName].VisibleIndex + 1;
                grvData.Columns[$"{colProductType.FieldName}"].VisibleIndex = grvData.Columns[colActualDate.FieldName].VisibleIndex + 2;
                grvData.Columns[$"{colPriceSale.FieldName}"].VisibleIndex = -1;

                txtBillCode.MaxLength = 9;
            }
            else //PO thương mại
            {
                grvData.Columns[$"{colBiddingPrice.FieldName}"].Caption = "Giá chào thầu";
                grvData.Columns[$"{colProductType.FieldName}"].VisibleIndex = -1;
                grvData.Columns[$"{colProductType.FieldName}"].VisibleIndex = -1;
                grvData.Columns[$"{colPriceSale.FieldName}"].VisibleIndex = grvData.Columns[colActualDate.FieldName].VisibleIndex + 1;

                txtBillCode.MaxLength = 8;
            }

            colProductSaleID.OptionsColumn.AllowEdit = cboPOType.SelectedIndex == 0;

            if (po.ID <= 0) LoadBillCode();
        }

        private void cboCompany_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        #region lưu log
        private void LogActions()
        {
            try
            {
                var controls = new Component[] { btnSave, btnSaveAndClose, btnSaveNew };
                var actions = Enumerable.Repeat<string>(po.ID > 0 ? "Update" : "Add", controls.Length).ToArray();
                var logDatas = Enumerable.Repeat<Func<dynamic, dynamic>>(GetDataChange, controls.Length).ToArray();
                var oldData = GetCurrentData();
                var logger = new Logger(controls, actions, logDatas, this, oldData);
                logger.Start();
            }
            catch
            {

            }
        }
        private PONCCDetailsLog GetCurrentData()
        {
            var data = new PONCCDetailsLog();
            var detailList = new List<PONCCDetailModel>();
            for (int i = 0; i < grvData.RowCount; i++)
            {
                var detail = new PONCCDetailModel();
                detail.ID = TextUtils.ToInt(grvData.GetRowCellValue(i, colID));
                detail.PONCCID = po.ID;
                detail.ProjectPartlistPurchaseRequestID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProjectPartlistPurchaseRequestID));
                detail.ProjectPartListID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProjectPartListID));
                detail.STT = TextUtils.ToInt(grvData.GetRowCellValue(i, colSTT));
                detail.ProductSaleID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductSaleID));
                detail.ProductCodeOfSupplier = TextUtils.ToString(grvData.GetRowCellValue(i, colProductCodeOfSupplier)).Trim();
                detail.QtyRequest = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colQtyRequest));
                detail.UnitPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colUnitPrice));
                detail.ThanhTien = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colThanhTien));
                detail.VAT = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colVAT));
                detail.DiscountPercent = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colDiscountPercent));
                detail.FeeShip = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colFeeShip));
                detail.VATMoney = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colVATMoney));
                detail.Discount = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colDiscount));
                string totalPriceFormat = String.Format("{0:0.00}", detail.ThanhTien + detail.VATMoney + detail.FeeShip - detail.Discount);
                detail.TotalPrice = TextUtils.ToDecimal(totalPriceFormat);
                string currencyExchangeFormat = String.Format("{0:0.00}", detail.TotalPrice * po.CurrencyRate);
                detail.CurrencyExchange = TextUtils.ToDecimal(currencyExchangeFormat);
                detail.ExpectedDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, colExpectedDate));
                detail.ActualDate = TextUtils.ToDate4(grvData.GetRowCellValue(i, colActualDate));
                detail.PriceSale = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colPriceSale));
                detail.PriceHistory = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colPriceHistory));
                detail.BiddingPrice = TextUtils.ToDecimal(grvData.GetRowCellValue(i, colBiddingPrice));
                detail.Note = TextUtils.ToString(grvData.GetRowCellValue(i, colNote));
                detail.ProjectID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProjectID));
                detail.ProjectName = TextUtils.ToString(grvData.GetRowCellValue(i, colProjectName));
                detail.ProductRTCID = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductRTCID));
                detail.DeadlineDelivery = TextUtils.ToDate4(grvData.GetRowCellValue(i, colDeadlineDelivery));
                detail.IsBill = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsBill));
                detail.ProductType = TextUtils.ToInt(grvData.GetRowCellValue(i, colProductType));
                detail.DateReturnEstimated = TextUtils.ToDate4(grvData.GetRowCellValue(i, colDateReturnEstimated));
                detail.IsStock = TextUtils.ToBoolean(grvData.GetRowCellValue(i, colIsStock));
                detailList.Add(detail);
            }
            data.PONCC = po;
            data.Details = detailList;
            return data;
        }
        private dynamic GetDataChange(dynamic oldData)
        {
            var oldDataLog = (PONCCDetailsLog)oldData;
            var newDataLog = GetCurrentData();
            return new
            {
                PONCC = new
                {
                    Old = oldDataLog.PONCC,
                    New = newDataLog.PONCC
                },
                Details = new
                {
                    Old = oldDataLog.Details,
                    New = newDataLog.Details
                }
            };
        }
        #endregion

        private void chkOrderQualityNotMet_CheckedChanged(object sender, EventArgs e)
        {
            txtReasonForFailure.Enabled = chkOrderQualityNotMet.Checked;
        }
    }

    public class PONCCDetailsLog
    {
        public PONCCModel PONCC;
        public List<PONCCDetailModel> Details;
    }
}
