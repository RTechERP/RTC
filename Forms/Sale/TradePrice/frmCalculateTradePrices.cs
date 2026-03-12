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
using BMS.Model;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using System.Collections;
using BMS.Business;
using Forms.Classes;
using DevExpress.XtraTreeList.Columns;
using System.IO;
using DevExpress.Utils;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using BMS.Utils;

namespace BMS
{
    public partial class frmCalculateTradePrices : _Forms
    {
        public DataTable dtSelected = new DataTable();
        public TradePriceModel tradePrice = new TradePriceModel();
        ArrayList lstIDDelete = new ArrayList();
        List<ProductSaleModel> listProduct = new List<ProductSaleModel>();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        List<TradePriceDetailModel> listDeletes = new List<TradePriceDetailModel>();

        public bool IsCopy = false;
        public frmCalculateTradePrices()
        {
            InitializeComponent();
        }

        private void frmCalculateTradePrices_Load(object sender, EventArgs e)
        {
            //ds = TextUtils.LoadDataSetFromSP("[spGetTradePrice]", new string[] { "@ID" }, new object[] { tradePrice.ID });
            //dt = ds.Tables[2].Clone();
            radioGroup1.SelectedIndex = 0;
            cboAdminSale.EditValue = Global.EmployeeID;
            loadCustomer();
            loadProduct();
            loadProject();
            loadCurrency();
            loadEmployee();
            LoadUnitCount();


            loadData();

        }
        private void loadProduct()
        {
            listProduct = SQLHelper<ProductSaleModel>.FindAll();
            cboProductDetail.DisplayMember = "ProductCode";
            cboProductDetail.ValueMember = "ID";
            cboProductDetail.DataSource = listProduct;
        }
        private void loadCustomer()
        {
            //List<CustomerModel> list = SQLHelper<CustomerModel>.FindAll();
            var exp1 = new Expression("IsDeleted", 1, "<>");
            List<CustomerModel> list = SQLHelper<CustomerModel>.FindByExpression(exp1);
            cboCustomer.Properties.DataSource = list;
            cboCustomer.Properties.DisplayMember = "CustomerCode";
            cboCustomer.Properties.ValueMember = "ID";
        }
        private void loadEmployee()
        {
            DataTable list = TextUtils.LoadDataFromSP("spGetEmployee", "A", new string[] { "@Status" }, new object[] { 0 });
            cboEmployee.Properties.ValueMember = "ID";
            cboEmployee.Properties.DisplayMember = "FullName";
            cboEmployee.Properties.DataSource = list;
            cboAdminSale.Properties.ValueMember = "ID";
            cboAdminSale.Properties.DisplayMember = "FullName";
            cboAdminSale.Properties.DataSource = list;
        }

        private void loadProject()
        {
            //int customerID = TextUtils.ToInt(cboCustomer.EditValue);
            //List<ProjectModel> list = SQLHelper<ProjectModel>.FindByAttribute("CustomerID", customerID);
            //if (list.Count <= 0) list = SQLHelper<ProjectModel>.FindAll();
            List<ProjectModel> list = SQLHelper<ProjectModel>.FindAll();
            cboProject.Properties.DataSource = list;
            cboProject.Properties.DisplayMember = "ProjectCode";
            cboProject.Properties.ValueMember = "ID";
        }

        private void cboProject_EditValueChanged(object sender, EventArgs e)
        {
            int projectID = (int)cboProject.EditValue;
            ProjectModel project = SQLHelper<ProjectModel>.FindByID(projectID);
            txtProjectName.Text = project.ProjectName;
        }


        void LoadUnitCount()
        {
            List<UnitCountModel> list = SQLHelper<UnitCountModel>.FindAll();
            cboUnit.DisplayMember = "UnitName";
            cboUnit.ValueMember = "ID";
            cboUnit.DataSource = list;
        }
        void loadData()
        {
            txtQuantityRTCVision.Enabled = tradePrice.IsRTCVision;
            chkIsRTCVision.Checked = tradePrice.IsRTCVision;
            txtQuantityRTCVision.Value = tradePrice.QuantityRTCVision;
            txtUnitPriceRTCVision.EditValue = tradePrice.UnitPriceRTCVision;

            if (dtSelected.Rows.Count > 0)
            {
                cboProject.EditValue = TextUtils.ToInt(dtSelected.Rows[0]["ProjectID"]);
                cboCustomer.EditValue = TextUtils.ToString(dtSelected.Rows[0]["CustomerID"]);
                txtProjectName.Text = TextUtils.ToString(dtSelected.Rows[0]["ProjectName"]);
                cboEmployee.EditValue = TextUtils.ToInt(dtSelected.Rows[0]["EmployeeID"]);
                cboAdminSale.EditValue = Global.EmployeeID;
                //dt.Columns.Remove("ID");
                for (int i = 0; i < dtSelected.Rows.Count; i++)
                {
                    TreeListNode node = tlData.AppendNode(new object[] { }, null);
                    DataRow row = dtSelected.Rows[i];
                    //DataRow newRow = dt.NewRow();
                    //newRow["STT"] = i + 1;
                    //newRow["Maker"] = TextUtils.ToString(row["Maker"]);
                    //newRow["ProductName"] = TextUtils.ToString(row["ProductName"]);
                    //newRow["ProductID"] = TextUtils.ToInt(row["ProductID"]);
                    //newRow["ProductCode"] = TextUtils.ToString(row["ProductCode"]);
                    //newRow["Quantity"] = TextUtils.ToInt(row["Quantity"]);
                    //newRow["Unit"] = TextUtils.ToInt(row["Unit"]);
                    //decimal unitPrice = TextUtils.ToDecimal(row["UnitPrice"]);
                    //decimal totalPrice = TextUtils.ToDecimal(row["TotalPrice"]);
                    //if (TextUtils.ToInt(row["CurrencyID"]) != 0)
                    //{
                    //    unitPrice = TextUtils.ToDecimal(row["UnitPrice"]) * TextUtils.ToDecimal(row["CurrencyRate"]);
                    //    totalPrice = TextUtils.ToDecimal(row["TotalPrice"]) * TextUtils.ToDecimal(row["CurrencyRate"]);
                    //}
                    //newRow["UnitImportPriceVND"] = unitPrice;
                    //newRow["TotalImportPriceVND"] = totalPrice;
                    //newRow["LeadTime"] = TextUtils.ToString(row["LeadTime"]);
                    //newRow["UnitPriceIncludeFees"] = TextUtils.ToInt(row["UnitImportPrice"]);
                    //newRow["TotalImportPriceIncludeFees"] = TextUtils.ToInt(row["TotalImportPrice"]);
                    //newRow["UnitPriceExpectCustomer"] = TextUtils.ToInt(row["UnitFactoryExportPrice"]);
                    //dt.Rows.Add(newRow);
                    node.SetValue(colSTT, i + 1);
                    node.SetValue(colMaker, TextUtils.ToString(row["Maker"]));
                    node.SetValue(colProductName, TextUtils.ToString(row["ProductName"]));
                    node.SetValue(colProductID, TextUtils.ToInt(row["ProductID"]));
                    node.SetValue(colProductCodeCustomer, TextUtils.ToString(row["ProductCode"]));
                    node.SetValue(colQuantity, TextUtils.ToInt(row["Quantity"]));
                    node.SetValue(colUnitCountID, TextUtils.ToInt(row["Unit"]));
                    decimal unitPrice = TextUtils.ToDecimal(row["UnitPrice"]);
                    decimal totalPrice = TextUtils.ToDecimal(row["TotalPrice"]);
                    if (TextUtils.ToInt(row["CurrencyID"]) != 0)
                    {
                        unitPrice = TextUtils.ToDecimal(row["UnitPrice"]) * TextUtils.ToDecimal(row["CurrencyRate"]);
                        totalPrice = TextUtils.ToDecimal(row["TotalPrice"]) * TextUtils.ToDecimal(row["CurrencyRate"]);
                    }
                    node.SetValue(colUnitImportPriceVND, unitPrice);
                    node.SetValue(colTotalImportPriceVND, totalPrice);
                    node.SetValue(colLeadTime, TextUtils.ToString(row["LeadTime"]));
                    node.SetValue(colUnitPriceIncludeFees, TextUtils.ToInt(row["UnitImportPrice"]));
                    node.SetValue(colTotalImportPriceIncludeFees, TextUtils.ToInt(row["TotalImportPrice"]));
                    node.SetValue(colUnitPriceExpectCustomer, TextUtils.ToInt(row["UnitFactoryExportPrice"]));
                }
                //TreeData.DataSource = dt;
                tlData.ExpandAll();
            }
            if (tradePrice.ID > 0)
            {
                cboCustomer.EditValue = tradePrice.CustomerID;
                cboProject.EditValue = tradePrice.ProjectID;
                cboEmployee.EditValue = tradePrice.EmployeeID;
                txtProjectName.Text = SQLHelper<ProjectModel>.FindByID(tradePrice.ProjectID).ProjectName;
                //txtCOM.EditValue = tradePrice.COM;
                txtRateCom.EditValue = tradePrice.RateCOM;


                loadDetail();

            }
            radioGroup1.SelectedIndex = !tradePrice.CurrencyType ? 0 : 1;
            cboCurrency.EditValue = tradePrice.CurrencyID;
            txtRate.EditValue = tradePrice.CurrencyRate;

            calculateTotal();

            if (!Global.IsAdmin)
            {
                btnSave.Enabled = btnSaveNew.Enabled = (tradePrice.IsApprovedSale != 1 && tradePrice.IsApprovedSale != 3);
            }
        }
        void loadCurrency()
        {
            //var exp1 = new Expression("ID", "1", "<>");
            List<CurrencyModel> list = SQLHelper<CurrencyModel>.FindAll();
            cboCurrency.Properties.DataSource = list;
            cboCurrency.Properties.ValueMember = "ID";
            cboCurrency.Properties.DisplayMember = "Code";


            cboCurrencyDetail.ValueMember = "ID";
            cboCurrencyDetail.DisplayMember = "Code";
            cboCurrencyDetail.DataSource = list;
        }
        void loadDetail()
        {
            DataTable dt = TextUtils.LoadDataFromSP("spGetTradePriceDetail","A", new string[] { "@TradePriceID" }, new object[] { tradePrice.ID });
            tlData.DataSource = dt;
            tlData.ExpandAll();
        }
        private void bntAdd_Click(object sender, EventArgs e)
        {
            List<int> listSTT = new List<int>();

            for (int i = 0; i < tlData.AllNodesCount; i++)
            {
                TreeListNode node = tlData.FindNodeByID(i);
                string strSTT = TextUtils.ToString(node.GetValue(colSTT));
                if (!strSTT.Contains("."))
                {
                    int stt = TextUtils.ToInt(node.GetValue(colSTT));
                    listSTT.Add(stt);
                }
            }
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    string strSTT = TextUtils.ToString(dt.Rows[i]["STT"]);
            //    if (!strSTT.Contains("."))
            //    {
            //        int stt = TextUtils.ToInt(dt.Rows[i]["STT"]);
            //        listSTT.Add(stt);
            //    }
            //}
            TreeListNode newNode = tlData.AppendNode(null, null);
            //DataRow dtrow = dt.NewRow();
            //dtrow["STT"] = listSTT.Count > 0 ? (listSTT.Max() + 1).ToString() : "1";
            newNode["STT"] = listSTT.Count > 0 ? (listSTT.Max() + 1).ToString() : "1";

            tlData.FocusedNode = newNode;
            tlData.FocusedColumn = colMaker;
            tlData.ExpandAll();
        }
        private void btnAddChild_Click(object sender, EventArgs e)
        {
            TreeListNode nodeFocus = tlData.FocusedNode;
            if (nodeFocus == null) return;


            TreeListNode lastNode = nodeFocus;
            if (nodeFocus.HasChildren)
            {
                lastNode = nodeFocus.Nodes.LastNode;
            }

            TreeListNode parentNode = nodeFocus.ParentNode;
            if (nodeFocus.HasChildren || parentNode == null)
            {
                //var nodeChilds = tlData.Nodes.Where(x => x.ParentNode == nodeFocus).ToList();

                TreeListNode newNode = tlData.AppendNode(null, nodeFocus);
                //string childSTT = nodeChilds.Count > 0 ? (nodeChilds.Count + 1).ToString() : "1";
                newNode["STT"] = nodeFocus.GetValue(colSTT) + "." + nodeFocus.Nodes.Count;
                newNode["CurrencyID"] = lastNode.GetValue(colCurrencyID);
                newNode["CurrencyRate"] = nodeFocus.GetValue(colCurrencyRate);

                tlData.FocusedNode = newNode;
            }
            else
            {
                //var nodeChilds = tlData.Nodes.Where(x => x.ParentNode == parentNode).ToList();

                TreeListNode newNode = tlData.AppendNode(null, parentNode);
                //string childSTT = nodeChilds.Count > 0 ? (nodeChilds.Count + 1).ToString() : "1";
                newNode["STT"] = parentNode.GetValue(colSTT) + "." + parentNode.Nodes.Count;
                newNode["CurrencyID"] = lastNode.GetValue(colCurrencyID);
                newNode["CurrencyRate"] = nodeFocus.GetValue(colCurrencyRate);

                tlData.FocusedNode = newNode;
            }

            tlData.FocusedColumn = colMaker;

            tlData.ExpandAll();
        }

        private void cboCustomer_EditValueChanged(object sender, EventArgs e)
        {
            txtCustomerName.Text = SQLHelper<CustomerModel>.FindByID((int)cboCustomer.EditValue).CustomerName;
            //loadProject();
        }

        private void TreeData_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            //if (e.Column == colDelete_1)
            //{
            //    if (MessageBox.Show("Bạn có chắc muốn xoá sản phẩm này không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            //    {
            //        TreeListNode nodeDele = TreeData.FocusedNode;
            //        int id_Parent = TextUtils.ToInt(TreeData.GetRowCellValue(nodeDele, colID));
            //        var listChild = TreeData.FindNodes((node) =>
            //        {
            //            return node.ParentNode == nodeDele;
            //        });
            //        if (listChild.Count() > 0)
            //        {
            //            foreach (TreeListNode item in listChild)
            //            {
            //                lstIDDelete.Add(TextUtils.ToInt(TreeData.GetRowCellValue(item, colID)));
            //                TreeData.DeleteNode(item);
            //            }
            //        }
            //        lstIDDelete.Add(id_Parent);
            //        TreeData.DeleteNode(nodeDele);
            //    }
            //}
        }

        private void cboProduct_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                SearchLookUpEdit lookUpEdit = (SearchLookUpEdit)sender;
                ProductSaleModel product = (ProductSaleModel)lookUpEdit.GetSelectedDataRow();
                if (product == null) return;

                UnitCountModel unitCount = SQLHelper<UnitCountModel>.FindByAttribute("UnitName", product.Unit.Trim()).FirstOrDefault();
                tlData.SetFocusedRowCellValue(colProductID, product.ID);
                tlData.SetFocusedRowCellValue(colProductName, product.ProductName);
                tlData.SetFocusedRowCellValue(colProductCodeCustomer, product.ProductCode);
                tlData.SetFocusedRowCellValue(colUnitCountID, unitCount != null ? unitCount.ID : 0);
                tlData.SetFocusedRowCellValue(colMaker, product.Maker);

                //tlData.Focus();
                //int ID = TextUtils.ToInt(tlData.GetFocusedRowCellValue(colProductID));
                //List<ProductSaleModel> list = listProduct.Where(x => x.ID == ID).ToList();
                //if (list.Count > 0)
                //{
                //    string productName = TextUtils.ToString(list[0].ProductName);
                //    string productcode = TextUtils.ToString(list[0].ProductCode);
                //    string unit = TextUtils.ToString(list[0].Unit);
                //    string maker = TextUtils.ToString(list[0].Maker);
                //    tlData.SetFocusedRowCellValue(colProductName, productName);
                //    tlData.SetFocusedRowCellValue(colProductCode, productcode);
                //    tlData.SetFocusedRowCellValue(colUnitCountID, unit);
                //    tlData.SetFocusedRowCellValue(colMaker, maker);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
            }
        }
        bool saveData()
        {
            if (!validate()) return false;

            tradePrice.ProjectID = TextUtils.ToInt(cboProject.EditValue);
            tradePrice.CustomerID = TextUtils.ToInt(cboCustomer.EditValue);
            //tradePrice.COM = TextUtils.ToDecimal(txtCOM.Text);
            tradePrice.RateCOM = TextUtils.ToDecimal(txtRateCom.Text);
            tradePrice.EmployeeID = TextUtils.ToInt(cboEmployee.EditValue);
            tradePrice.SaleAdminID = TextUtils.ToInt(cboAdminSale.EditValue);
            tradePrice.CurrencyType = radioGroup1.SelectedIndex == 1;
            tradePrice.CurrencyID = TextUtils.ToInt(cboCurrency.EditValue);
            tradePrice.CurrencyRate = TextUtils.ToDecimal(txtRate.EditValue);
            tradePrice.UnitPriceDelivery = TextUtils.ToDecimal(txtUnitPriceDelivery.EditValue);
            tradePrice.QuantityDelivery = TextUtils.ToInt(txtQuantityDelivery.EditValue);

            if (tradePrice.ID > 0)
            {
                TradePriceBO.Instance.Update(tradePrice);
            }
            else
            {
                tradePrice.ID = TextUtils.ToInt(TradePriceBO.Instance.Insert(tradePrice));
            }

            List<TreeListNode> ListChildNode = new List<TreeListNode>();

            //DataTable dt = TextUtils.Select($"Select * from TradePriceDetail where TradePriceID={tradePrice.ID}");

            for (int i = 0; i < tlData.AllNodesCount; i++)
            {
                TreeListNode nodeDetail = tlData.GetNodeByVisibleIndex(i);
                int id = TextUtils.ToInt(tlData.GetRowCellValue(nodeDetail, colID));
                if (ListChildNode.Contains(nodeDetail)) continue;
                TradePriceDetailModel detail = new TradePriceDetailModel();

                if (id > 0)
                {
                    detail = SQLHelper<TradePriceDetailModel>.FindByID(id);
                }
                //detail.ID = TextUtils.ToInt(TreeData.GetRowCellValue(nodeDetail, colID_1));
                detail.STT = TextUtils.ToString(tlData.GetRowCellValue(nodeDetail, colSTT));
                detail.TradePriceID = tradePrice.ID;
                detail.ProductID = TextUtils.ToInt(tlData.GetRowCellValue(nodeDetail, colProductID));
                detail.Maker = TextUtils.ToString(tlData.GetRowCellValue(nodeDetail, colMaker));
                detail.Quantity = TextUtils.ToInt(tlData.GetRowCellValue(nodeDetail, colQuantity));
                //detail.Unit = TextUtils.ToString(tlData.GetRowCellValue(nodeDetail, colUnitCountID));
                detail.UnitImportPriceUSD = TextUtils.ToDecimal(tlData.GetRowCellValue(nodeDetail, colUnitImportPriceUSD));
                detail.TotalImportPriceUSD = TextUtils.ToDecimal(tlData.GetRowCellValue(nodeDetail, colTotalImportPriceUSD));
                detail.UnitImportPriceVND = TextUtils.ToDecimal(tlData.GetRowCellValue(nodeDetail, colUnitImportPriceVND));
                detail.TotalImportPriceVND = TextUtils.ToDecimal(tlData.GetRowCellValue(nodeDetail, colTotalImportPriceVND));
                detail.BankCharge = TextUtils.ToDecimal(tlData.GetRowCellValue(nodeDetail, colBankCharge));
                detail.ProtectiveTariff = TextUtils.ToDecimal(tlData.GetRowCellValue(nodeDetail, colProtectiveTariff));
                detail.ProtectiveTariffPerPcs = TextUtils.ToDecimal(tlData.GetRowCellValue(nodeDetail, colProtectiveTariffPerPcs));
                detail.TotalProtectiveTariff = TextUtils.ToDecimal(tlData.GetRowCellValue(nodeDetail, colTotalProtectiveTariff));
                detail.OrtherFees = TextUtils.ToDecimal(tlData.GetRowCellValue(nodeDetail, colOrtherFees));
                detail.CustomFees = TextUtils.ToDecimal(tlData.GetRowCellValue(nodeDetail, colCustomFees));
                detail.TotalImportPriceIncludeFees = TextUtils.ToDecimal(tlData.GetRowCellValue(nodeDetail, colTotalImportPriceIncludeFees));
                detail.UnitPriceIncludeFees = TextUtils.ToDecimal(tlData.GetRowCellValue(nodeDetail, colUnitPriceIncludeFees));
                detail.CMPerSet = TextUtils.ToDecimal(tlData.GetRowCellValue(nodeDetail, colCMPerSet));
                detail.UnitPriceExpectCustomer = TextUtils.ToDecimal(tlData.GetRowCellValue(nodeDetail, colUnitPriceExpectCustomer));
                detail.TotalPriceExpectCustomer = TextUtils.ToDecimal(tlData.GetRowCellValue(nodeDetail, colTotalPriceExpectCustomer));
                detail.Profit = TextUtils.ToDecimal(tlData.GetRowCellValue(nodeDetail, colProfit));
                detail.ProfitPercent = TextUtils.ToDecimal(tlData.GetRowCellValue(nodeDetail, colProfitPercent));
                detail.LeadTime = TextUtils.ToString(tlData.GetRowCellValue(nodeDetail, colLeadTime));
                detail.TotalPriceLabor = TextUtils.ToDecimal(tlData.GetRowCellValue(nodeDetail, colTotalPriceLabor));
                detail.TotalPriceRTCVision = TextUtils.ToDecimal(tlData.GetRowCellValue(nodeDetail, colTotalPriceRTCVision));
                detail.TotalPrice = TextUtils.ToDecimal(tlData.GetRowCellValue(nodeDetail, colTotalPrice));
                detail.UnitPricePerCOM = TextUtils.ToDecimal(tlData.GetRowCellValue(nodeDetail, colUnitPricePerCOM));
                detail.Note = TextUtils.ToString(tlData.GetRowCellValue(nodeDetail, colNote));
                detail.ProductCodeCustomer = TextUtils.ToString(tlData.GetRowCellValue(nodeDetail, colProductCodeCustomer));
                detail.UnitCountID = TextUtils.ToInt(tlData.GetRowCellValue(nodeDetail, colUnitCountID));

                detail.FeeShipPcs = TextUtils.ToDecimal(tlData.GetRowCellValue(nodeDetail, colFeeShipPcs));
                detail.ProductName = TextUtils.ToString(tlData.GetRowCellValue(nodeDetail, colProductName));
                detail.ProductCode = TextUtils.ToString(tlData.GetRowCellDisplayText(nodeDetail, colProductID));
                detail.ProductCodeOrigin = TextUtils.ToString(tlData.GetRowCellValue(nodeDetail, colProductCodeOrigin));

                detail.CurrencyID= TextUtils.ToInt(tlData.GetRowCellValue(nodeDetail, colCurrencyID));
                detail.CurrencyRate= TextUtils.ToDecimal(tlData.GetRowCellValue(nodeDetail, colCurrencyRate));
                detail.Margin= TextUtils.ToDecimal(tlData.GetRowCellValue(nodeDetail, colMargin));


                if (detail.ID > 0)
                {
                    TradePriceDetailBO.Instance.Update(detail);
                }
                else
                {
                    detail.ID = TextUtils.ToInt(TradePriceDetailBO.Instance.Insert(detail));
                }

                //Find all NodeChild of nodeDetail
                var childNode = tlData.FindNodes((node) =>
                {
                    return node.ParentNode == nodeDetail;
                }
                );
                if (childNode.Count() > 0)
                {
                    //ChildNode 
                    foreach (TreeListNode item in childNode)
                    {
                        //if (item == nodeDetail) continue;
                        ListChildNode.Add(item);
                        int idChild = TextUtils.ToInt(tlData.GetRowCellValue(item, colID));
                        TradePriceDetailModel detailChild = new TradePriceDetailModel();


                        if (idChild > 0)
                        {
                            detailChild = SQLHelper<TradePriceDetailModel>.FindByID(idChild);
                        }
                        //detail.ID = TextUtils.ToInt(TreeData.GetRowCellValue(nodeDetail, colID_1));
                        detailChild.STT = TextUtils.ToString(tlData.GetRowCellValue(item, colSTT));
                        detailChild.TradePriceID = tradePrice.ID;
                        detailChild.ProductID = TextUtils.ToInt(tlData.GetRowCellValue(item, colProductID));
                        detailChild.ProductCodeCustomer = TextUtils.ToString(tlData.GetRowCellValue(item, colProductCodeCustomer));
                        //detailChild.Maker = TextUtils.ToString(TreeData.GetRowCellValue(item, colMaker));
                        detailChild.Maker = detail.Maker;
                        detailChild.Quantity = TextUtils.ToInt(tlData.GetRowCellValue(item, colQuantity));
                        detailChild.Unit = TextUtils.ToString(tlData.GetRowCellValue(item, colUnitCountID));
                        detailChild.UnitImportPriceUSD = TextUtils.ToDecimal(tlData.GetRowCellValue(item, colUnitImportPriceUSD));
                        detailChild.TotalImportPriceUSD = TextUtils.ToDecimal(tlData.GetRowCellValue(item, colTotalImportPriceUSD));
                        detailChild.UnitImportPriceVND = TextUtils.ToDecimal(tlData.GetRowCellValue(item, colUnitImportPriceVND));
                        detailChild.TotalImportPriceVND = TextUtils.ToDecimal(tlData.GetRowCellValue(item, colTotalImportPriceVND));
                        detailChild.BankCharge = TextUtils.ToDecimal(tlData.GetRowCellValue(item, colBankCharge));
                        detailChild.ProtectiveTariff = TextUtils.ToDecimal(tlData.GetRowCellValue(item, colProtectiveTariff));
                        detailChild.ProtectiveTariffPerPcs = TextUtils.ToDecimal(tlData.GetRowCellValue(item, colProtectiveTariffPerPcs));
                        detailChild.TotalProtectiveTariff = TextUtils.ToDecimal(tlData.GetRowCellValue(item, colTotalProtectiveTariff));
                        detailChild.OrtherFees = TextUtils.ToDecimal(tlData.GetRowCellValue(item, colOrtherFees));
                        detailChild.CustomFees = TextUtils.ToDecimal(tlData.GetRowCellValue(item, colCustomFees));
                        detailChild.TotalImportPriceIncludeFees = TextUtils.ToDecimal(tlData.GetRowCellValue(item, colTotalImportPriceIncludeFees));
                        detailChild.UnitPriceIncludeFees = TextUtils.ToDecimal(tlData.GetRowCellValue(item, colUnitPriceIncludeFees));
                        detailChild.CMPerSet = TextUtils.ToDecimal(tlData.GetRowCellValue(item, colCMPerSet));
                        detailChild.UnitPriceExpectCustomer = TextUtils.ToDecimal(tlData.GetRowCellValue(item, colUnitPriceExpectCustomer));
                        detailChild.TotalPriceExpectCustomer = TextUtils.ToDecimal(tlData.GetRowCellValue(item, colTotalPriceExpectCustomer));
                        detailChild.Profit = TextUtils.ToDecimal(tlData.GetRowCellValue(item, colProfit));
                        detailChild.ProfitPercent = TextUtils.ToDecimal(tlData.GetRowCellValue(item, colProfitPercent));
                        detailChild.LeadTime = TextUtils.ToString(tlData.GetRowCellValue(item, colLeadTime));
                        detailChild.TotalPriceLabor = TextUtils.ToDecimal(tlData.GetRowCellValue(item, colTotalPriceLabor));
                        detailChild.TotalPriceRTCVision = TextUtils.ToDecimal(tlData.GetRowCellValue(item, colTotalPriceRTCVision));
                        detailChild.TotalPrice = TextUtils.ToDecimal(tlData.GetRowCellValue(item, colTotalPrice));
                        detailChild.UnitPricePerCOM = TextUtils.ToDecimal(tlData.GetRowCellValue(item, colUnitPricePerCOM));
                        detailChild.Note = TextUtils.ToString(tlData.GetRowCellValue(item, colNote));
                        detailChild.ParentID = detail.ID;

                        if (detailChild.ID > 0)
                        {
                            TradePriceDetailBO.Instance.Update(detailChild);
                        }
                        else
                        {
                            detailChild.ID = TextUtils.ToInt(TradePriceDetailBO.Instance.Insert(detailChild));
                        }
                    }
                }
            }


            //Xoá danh sách cho tiết
            if (listDeletes.Count > 0)
            {
                SQLHelper<TradePriceDetailModel>.DeleteListModel(listDeletes);
            }
            return true;
        }
        bool validate()
        {
            if (TextUtils.ToInt(cboCustomer.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng chọn Khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (TextUtils.ToInt(cboProject.EditValue) <= 0)
            {
                MessageBox.Show("Vui lòng chọn Dự án!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }

        void calculateTotal()
        {
            decimal exw = 0;
            decimal margin = 0;
            decimal totalProfitRate = 0;


            if (TextUtils.ToDecimal(tlData.GetSummaryValue(colTotalImportPriceVND)) > 0)
            {
                exw = TextUtils.ToDecimal(tlData.GetSummaryValue(colTotalPriceExpectCustomer)) / TextUtils.ToDecimal(tlData.GetSummaryValue(colTotalImportPriceVND));
            }
            if (TextUtils.ToDecimal(tlData.GetSummaryValue(colTotalImportPriceIncludeFees)) > 0)
            {
                margin = TextUtils.ToDecimal(tlData.GetSummaryValue(colTotalPriceExpectCustomer)) - TextUtils.ToDecimal(tlData.GetSummaryValue(colCMPerSet)) / TextUtils.ToDecimal(tlData.GetSummaryValue(colTotalImportPriceIncludeFees));
            }
            decimal totalProfit = TextUtils.ToDecimal(tlData.GetSummaryValue(colTotalPriceExpectCustomer)) - TextUtils.ToDecimal(tlData.GetSummaryValue(colCMPerSet)) - TextUtils.ToDecimal(tlData.GetSummaryValue(colTotalImportPriceIncludeFees));
            if (TextUtils.ToDecimal(tlData.GetSummaryValue(colTotalPriceExpectCustomer)) > 0)
            {
                totalProfitRate = totalProfit / TextUtils.ToDecimal(tlData.GetSummaryValue(colTotalPriceExpectCustomer));
            }
            decimal totalCMPerSet = TextUtils.ToDecimal(tlData.GetSummaryValue(colTotalPriceExpectCustomer)) * TextUtils.ToDecimal(txtRateCom.Text);

            //txtEXW.Text = "EXW = " + Math.Round(exw, 2).ToString("#,##0.##");
            //txtMargin.Text = "Margin = " + Math.Round(margin, 2).ToString("#,##0.##");
            //txtTotalProfit.Text = "Lợi nhuận = " + Math.Round(totalProfit, 2).ToString("#,##0.##");
            //txtTotalProfitRate.Text = "Tỷ lệ lợi nhuận = " + Math.Round(totalProfitRate * 100, 2).ToString("#,##0.##") + "%";
            //txtTotalCMPerSet.Text = "Tổng CM/Set = " + Math.Round(totalCMPerSet, 2).ToString("#,##0.##");

            bandEXW.Caption = $"EXW = {exw.ToString("n2")}";
            bandMargin.Caption = $"Margin = {margin.ToString("n2")}";
            bandTotalProfit.Caption = $"Lợi nhuận = {totalProfit.ToString("n2")}";
            bandTotalProfitRate.Caption = $"Tỷ lệ lợi nhuận = {totalProfitRate.ToString("n2")} %";
            bandTotalCMPerSet.Caption = $"Tổng CM/Set = {totalCMPerSet.ToString("n2")}";
        }


        //private void TreeData_CellValueChanged(object sender, CellValueChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Column == colQuantity || e.Column == colUnitImportPriceUSD || e.Column == colProtectiveTariff || e.Column == colProtectiveTariffPerPcs || e.Column == colBankCharge
        //            || e.Column == colOrtherFees || e.Column == colCustomFees || e.Column == colUnitPriceExpectCustomer || e.Column == colTotalPriceLabor)
        //        {
        //            calculate();
        //            //calculateParentNode();
        //        }
        //        calculateTotal();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Thông báo");
        //    }
        //}

        //void calculate()
        //{
        //    decimal qty = TextUtils.ToDecimal(tlData.GetFocusedRowCellValue(colQuantity));
        //    decimal unitPriceImportUSD = TextUtils.ToDecimal(tlData.GetFocusedRowCellValue(colUnitImportPriceUSD));
        //    decimal protectiveTariff = TextUtils.ToDecimal(tlData.GetFocusedRowCellValue(colProtectiveTariff));
        //    decimal ortherFees = TextUtils.ToDecimal(tlData.GetFocusedRowCellValue(colOrtherFees));
        //    decimal customFees = TextUtils.ToDecimal(tlData.GetFocusedRowCellValue(colCustomFees));
        //    decimal bankCharge = TextUtils.ToDecimal(tlData.GetFocusedRowCellValue(colBankCharge));
        //    decimal unitPriceExpectCustomer = TextUtils.ToDecimal(tlData.GetFocusedRowCellValue(colUnitPriceExpectCustomer));
        //    decimal totalPriceLabor = TextUtils.ToDecimal(tlData.GetFocusedRowCellValue(colTotalPriceLabor));
        //    decimal protectiveTariffPerPcs = TextUtils.ToDecimal(tlData.GetFocusedRowCellValue(colProtectiveTariffPerPcs));
        //    decimal rateCurrency = TextUtils.ToDecimal(txtRate.Text);
        //    decimal totalPriceImportUSD = qty * unitPriceImportUSD;

        //    decimal unitPriceImportVND = unitPriceImportUSD * rateCurrency;
        //    decimal totalPriceImportVND = unitPriceImportVND * qty;
        //    decimal totalProtectiveTariff = protectiveTariffPerPcs * qty;
        //    decimal totalImportPriceIncludeFees = totalPriceImportVND + bankCharge + totalProtectiveTariff + ortherFees + customFees;
        //    decimal unitPriceIncludeFees = totalPriceImportVND > 0 ? totalImportPriceIncludeFees / totalPriceImportVND * unitPriceImportVND : 0;
        //    decimal cmPerSet = TextUtils.ToDecimal(txtRateCom.Text) * unitPriceExpectCustomer;
        //    decimal totalPriceExpectCustomer = unitPriceExpectCustomer * qty;
        //    decimal profit = totalPriceExpectCustomer - totalImportPriceIncludeFees;
        //    decimal profitPercent = totalPriceExpectCustomer > 0 ? profit / totalPriceExpectCustomer : 0;
        //    //decimal totalPriceRTCVision = TextUtils.ToDecimal(15000000) + TextUtils.ToDecimal(txtCOM.Text);
        //    decimal totalPriceRTCVision = TextUtils.ToDecimal(tlData.GetFocusedRowCellValue(colTotalPriceRTCVision)) + TextUtils.ToDecimal(txtRateCom.Text);
        //    decimal totalPrice = totalPriceExpectCustomer + totalPriceLabor + totalPriceRTCVision;

        //    tlData.SetFocusedRowCellValue(colTotalImportPriceUSD, totalPriceImportUSD);
        //    tlData.SetFocusedRowCellValue(colUnitImportPriceVND, unitPriceImportVND);
        //    tlData.SetFocusedRowCellValue(colTotalImportPriceVND, totalPriceImportVND);
        //    tlData.SetFocusedRowCellValue(colTotalProtectiveTariff, totalProtectiveTariff);
        //    tlData.SetFocusedRowCellValue(colTotalImportPriceIncludeFees, totalImportPriceIncludeFees);
        //    tlData.SetFocusedRowCellValue(colUnitPriceIncludeFees, unitPriceIncludeFees);
        //    tlData.SetFocusedRowCellValue(colTotalPriceExpectCustomer, Math.Round(totalPriceExpectCustomer, 2));
        //    tlData.SetFocusedRowCellValue(colProfit, profit);
        //    //tlData.SetFocusedRowCellValue(colProfitPercent, Math.Round(profitPercent, 2));
        //    tlData.SetFocusedRowCellValue(colProfitPercent, profitPercent * 100);
        //    tlData.SetFocusedRowCellValue(colTotalPrice, totalPrice);
        //    tlData.SetFocusedRowCellValue(colCMPerSet, cmPerSet);
        //    tlData.SetFocusedRowCellValue(colTotalPriceRTCVision, totalPriceRTCVision);

        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            saveData();
            tradePrice = new TradePriceModel();
            cboCustomer.EditValue = -1;
            cboProject.EditValue = -1;
            lstIDDelete.Clear();
            for (int i = tlData.AllNodesCount - 1; i >= 0; i--)
            {
                TreeListNode node = tlData.GetNodeByVisibleIndex(i);
                tlData.DeleteNode(node);
            }
        }

        //void calculateParentNode()
        //{
        //    List<TreeListNode> listChild = new List<TreeListNode>();
        //    for (int i = 0; i < TreeData.AllNodesCount; i++)
        //    {
        //        TreeListNode node = TreeData.FindNodeByID(i);
        //        if (listChild.Contains(node)) continue;
        //        var childNode = TreeData.FindNodes((item) =>
        //        {
        //            return item.ParentNode == node;
        //        }
        //        );
        //        if (childNode.Count() > 0)
        //        {
        //            decimal qty = 0;
        //            decimal unitPriceImportUSD = 0;
        //            decimal ortherFees = 0;
        //            decimal customFees = 0;
        //            decimal unitPriceExpectCustomer = 0;
        //            decimal totalPriceLabor = 0;
        //            decimal totalPriceImportUSD = 0;
        //            decimal unitPriceImportVND = 0;
        //            decimal totalPriceImportVND = 0;
        //            decimal protectiveTariffPerPcs = 0;
        //            decimal totalProtectiveTariff = 0;
        //            decimal totalImportPriceIncludeFees = 0;
        //            decimal unitPriceIncludeFees = 0;
        //            decimal cmPerSet = 0;
        //            decimal totalPriceExpectCustomer = 0;
        //            decimal profit = 0;
        //            decimal profitPercent = 0;
        //            decimal totalPrice = 0;
        //            foreach (TreeListNode child in childNode)
        //            {
        //                qty += TextUtils.ToDecimal(child.GetValue(colQuantity));
        //                unitPriceImportUSD += TextUtils.ToDecimal(child.GetValue(colUnitImportPriceUSD));
        //                ortherFees += TextUtils.ToDecimal(child.GetValue(colOrtherFees));
        //                customFees += TextUtils.ToDecimal(child.GetValue(colCustomFees));
        //                unitPriceExpectCustomer += TextUtils.ToDecimal(child.GetValue(colUnitPriceExpectCustomer));
        //                totalPriceLabor += TextUtils.ToDecimal(child.GetValue(colTotalPriceLabor));
        //                totalPriceImportUSD += TextUtils.ToDecimal(child.GetValue(colTotalImportPriceUSD));
        //                totalPriceImportVND += TextUtils.ToDecimal(child.GetValue(colTotalImportPriceVND));
        //                protectiveTariffPerPcs += TextUtils.ToDecimal(child.GetValue(colProtectiveTariffPerPcs));
        //                totalProtectiveTariff += TextUtils.ToDecimal(child.GetValue(colTotalProtectiveTariff));
        //                totalImportPriceIncludeFees += TextUtils.ToDecimal(child.GetValue(colTotalImportPriceIncludeFees));
        //                unitPriceIncludeFees += TextUtils.ToDecimal(child.GetValue(colUnitPriceIncludeFees));
        //                cmPerSet += TextUtils.ToDecimal(child.GetValue(coCMPerSet));
        //                totalPriceExpectCustomer += TextUtils.ToDecimal(child.GetValue(colTotalPriceExpectCustomer));
        //                profit += TextUtils.ToDecimal(child.GetValue(colProfit));
        //                profitPercent += TextUtils.ToDecimal(child.GetValue(colProfitPercent));
        //                unitPriceImportVND += TextUtils.ToDecimal(child.GetValue(colUnitImportPriceVND));
        //                totalPrice += TextUtils.ToDecimal(child.GetValue(colTotalPrice));
        //            }
        //            node["Quantity"] = qty;
        //            node["UnitImportPriceUSD"] = unitPriceImportUSD;
        //            node["OrtherFees"] = ortherFees;
        //            node["CustomFees"] = customFees;
        //            node["UnitPriceExpectCustomer"] = unitPriceExpectCustomer;
        //            node["TotalPriceLabor"] = totalPriceLabor;
        //            node["TotalImportPriceVND"] = totalPriceImportVND;
        //            node["TotalImportPriceUSD"] = totalPriceImportUSD;
        //            node["ProtectiveTariffPerPcs"] = protectiveTariffPerPcs;
        //            node["TotalProtectiveTariff"] = totalProtectiveTariff;
        //            node["TotalImportPriceIncludeFees"] = totalImportPriceIncludeFees;
        //            node["UnitPriceIncludeFees"] = unitPriceIncludeFees;
        //            node["CMPerSet"] = cmPerSet;
        //            node["TotalPriceExpectCustomer"] = totalPriceExpectCustomer;
        //            node["Profit"] = profit;
        //            node["ProfitPercent"] = profitPercent;
        //            node["UnitImportPriceVND"] = unitPriceImportVND;
        //            node["TotalPriceRTCVision"] = childNode[0]["TotalPriceRTCVision"];
        //            node["TotalPrice"] = totalPrice;
        //        }
        //    }
        //}
        private void txtCOM_TextChanged(object sender, EventArgs e)
        {
            //decimal COM = TextUtils.ToDecimal(txtCOM.Text);
            //decimal totalPriceRTCVision = 0;
            //for (int i = 0; i < tlData.AllNodesCount; i++)
            //{
            //    TreeListNode node = tlData.GetNodeByVisibleIndex(i);
            //    totalPriceRTCVision = COM + 15000000;
            //    tlData.SetRowCellValue(node, colTotalPriceRTCVision, totalPriceRTCVision);
            //}
            //calculate();
        }

        private void txtRateCom_TextChanged(object sender, EventArgs e)
        {
            decimal rateCom = TextUtils.ToDecimal(txtRateCom.Text);
            decimal CMPerSet = 0;
            for (int i = 0; i < tlData.AllNodesCount; i++)
            {
                TreeListNode node = tlData.GetNodeByVisibleIndex(i);
                CMPerSet = rateCom * TextUtils.ToDecimal(tlData.GetRowCellValue(node, colUnitPriceExpectCustomer));
                tlData.SetRowCellValue(node, colCMPerSet, CMPerSet);
            }
            //calculate();
        }

        private void TreeData_GetCustomSummaryValue(object sender, GetCustomSummaryValueEventArgs e)
        {

        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            frmTradePriceImportExcel frm = new frmTradePriceImportExcel();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                tradePrice = frm.tradePrice;
                loadData();
            }
        }

        private void cboCurrency_EditValueChanged(object sender, EventArgs e)
        {
            //if (TextUtils.ToInt(cboCurrency.EditValue) > 0)
            //{
            //    string codeCurrency = cboCurrency.Properties.PopupView.GetRowCellValue(TextUtils.ToInt(cboCurrency.EditValue) - 2, colCurrencyCode).ToString().ToUpper();
            //    DateTime dateExpriedUnofficialQuota = Convert.ToDateTime(cboCurrency.Properties.PopupView.GetRowCellValue(TextUtils.ToInt(cboCurrency.EditValue) - 2, colDateExpriedUnofficialQuota));
            //    DateTime dateExpriedOfficialQuota = Convert.ToDateTime(cboCurrency.Properties.PopupView.GetRowCellValue(TextUtils.ToInt(cboCurrency.EditValue) - 2, colDateExpriedOfficialQuota));
            //    TreeData.Columns[colUnitImportPriceUSD.AbsoluteIndex].Caption = $"Đơn giá nhập EXW ({codeCurrency})";
            //    if (radioGroup1.SelectedIndex == 0)
            //    {
            //        if (dateExpriedOfficialQuota < DateTime.Now)
            //        {
            //            MessageBox.Show($"Tỷ giá chính ngạch của {codeCurrency} đã hết hạn!");
            //            txtRate.Text = "0";
            //        }
            //        else
            //            txtRate.Text = TextUtils.ToDecimal(cboCurrency.Properties.PopupView.GetRowCellValue(TextUtils.ToInt(cboCurrency.EditValue) - 2, colCurrencyRateOfficialQuota)).ToString("#,##0.##");
            //    }
            //    if (radioGroup1.SelectedIndex == 1)
            //    {
            //        if (dateExpriedUnofficialQuota < DateTime.Now)
            //        {
            //            MessageBox.Show($"Tỷ giá tiểu ngạch của {codeCurrency} đã hết hạn!");
            //            txtRate.Text = "0";
            //        }
            //        else
            //            txtRate.Text = TextUtils.ToDecimal(cboCurrency.Properties.PopupView.GetRowCellValue(TextUtils.ToInt(cboCurrency.EditValue) - 2, colCurrencyRateUnofficialQuota)).ToString("#,##0.##");
            //    }
            //    calculate();
            //}

            //txtRate.EditValue = 0;
            //DateTime dateNow = DateTime.Now.Date;
            //CurrencyModel currency = (CurrencyModel)cboCurrency.GetSelectedDataRow();
            //if (currency != null)
            //{
            //    if (radioGroup1.SelectedIndex == 0) //Chính ngạch
            //    {
            //        if (currency.DateExpriedOfficialQuota.HasValue)
            //        {
            //            txtRate.EditValue = currency.CurrencyRateOfficialQuota;
            //            if (currency.DateExpriedOfficialQuota.Value.Date < dateNow) //Hết hạn
            //            {
            //                MessageBox.Show($"Tỷ giá chính ngạch của {currency.Code} đã hết hạn!");
            //                txtRate.EditValue = 0;
            //            }
            //        }
            //    }
            //    else //Tiểu ngạch
            //    {
            //        if (currency.DateExpriedUnofficialQuota.HasValue)
            //        {
            //            txtRate.EditValue = currency.CurrencyRateUnofficialQuota;
            //            if (currency.DateExpriedUnofficialQuota.Value.Date < dateNow) //Hết hạn
            //            {
            //                MessageBox.Show($"Tỷ giá tiểu ngạch của {currency.Code} đã hết hạn!");
            //                txtRate.EditValue = 0;
            //            }
            //        }
            //    }
            //    calculate();
            //}

            //GetCurrencyRate();


        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (TextUtils.ToInt(cboCurrency.EditValue) > 0)
            //{
            //    string codeCurrency = cboCurrency.Properties.PopupView.GetRowCellValue(TextUtils.ToInt(cboCurrency.EditValue) - 2, colCurrencyCode).ToString().ToUpper();
            //    DateTime dateExpriedUnofficialQuota = Convert.ToDateTime(cboCurrency.Properties.PopupView.GetRowCellValue(TextUtils.ToInt(cboCurrency.EditValue) - 2, colDateExpriedUnofficialQuota));
            //    DateTime dateExpriedOfficialQuota = Convert.ToDateTime(cboCurrency.Properties.PopupView.GetRowCellValue(TextUtils.ToInt(cboCurrency.EditValue) - 2, colDateExpriedOfficialQuota));
            //    TreeData.Columns[colUnitImportPriceUSD.AbsoluteIndex].Caption = $"Đơn giá nhập EXW ({codeCurrency})";
            //    if (radioGroup1.SelectedIndex == 0)
            //    {
            //        if (dateExpriedOfficialQuota < DateTime.Now)
            //        {
            //            MessageBox.Show($"Tỷ giá chính ngạch của {codeCurrency} đã hết hạn!");
            //            txtRate.Text = "0";
            //        }
            //        else
            //            txtRate.Text = TextUtils.ToDecimal(cboCurrency.Properties.PopupView.GetRowCellValue(TextUtils.ToInt(cboCurrency.EditValue) - 2, colCurrencyRateOfficialQuota)).ToString("#,##0.##");
            //    }
            //    if (radioGroup1.SelectedIndex == 1)
            //    {
            //        if (dateExpriedUnofficialQuota < DateTime.Now)
            //        {
            //            MessageBox.Show($"Tỷ giá tiểu ngạch của {codeCurrency} đã hết hạn!");
            //            txtRate.Text = "0";
            //        }
            //        else
            //            txtRate.Text = TextUtils.ToDecimal(cboCurrency.Properties.PopupView.GetRowCellValue(TextUtils.ToInt(cboCurrency.EditValue) - 2, colCurrencyRateUnofficialQuota)).ToString("#,##0.##");
            //    }
            //    calculate();
            //}

            //GetCurrencyRate();


            foreach (TreeListNode node in tlData.GetNodeList())
            {
                GetCurrencyRate(node);
            }
        }


        void GetCurrencyRate(TreeListNode node)
        {
            txtRate.EditValue = 0;
            DateTime dateNow = DateTime.Now.Date;

            int currencyID = TextUtils.ToInt(tlData.GetRowCellValue(node, colCurrencyID));
            CurrencyModel currency = SQLHelper<CurrencyModel>.FindByID(currencyID);
            //currency = currency ?? new CurrencyModel();

            decimal currencyRate = 0;
            //if (currency != null)
            {
                //tlData.Columns[colUnitImportPriceUSD.AbsoluteIndex].Caption = $"Đơn giá nhập EXW ({currency.Code})";
                if (radioGroup1.SelectedIndex == 0) //Chính ngạch
                {
                    if (currency.DateExpriedOfficialQuota.HasValue)
                    {
                        //txtRate.EditValue = currency.CurrencyRateOfficialQuota;
                        currencyRate = currency.CurrencyRateOfficialQuota;
                        if (currency.DateExpriedOfficialQuota.Value.Date < dateNow) //Hết hạn
                        {
                            MessageBox.Show($"Tỷ giá chính ngạch của {currency.Code} đã hết hạn!");
                            //txtRate.EditValue = 0;
                            currencyRate = 0;
                        }
                    }
                }
                else //Tiểu ngạch
                {
                    if (currency.DateExpriedUnofficialQuota.HasValue)
                    {
                        //txtRate.EditValue = currency.CurrencyRateUnofficialQuota;
                        currencyRate = currency.CurrencyRateUnofficialQuota;
                        if (currency.DateExpriedUnofficialQuota.Value.Date < dateNow) //Hết hạn
                        {
                            MessageBox.Show($"Tỷ giá tiểu ngạch của {currency.Code} đã hết hạn!");
                            //txtRate.EditValue = 0;
                            currencyRate = 0;
                        }
                    }
                }
            }

            //tlData.SetRowCellValue(node, "CurrencyRate", currencyRate);

            node.SetValue("CurrencyRate", currencyRate);

            //calculate();
            //foreach (TreeListNode node in tlData.GetNodeList())
            {
                Calculator(node);
            }

            calculateTotal();
        }


        //void DeleteNode(TreeListNode node)
        //{
        //    //var focusNode = node;

        //    var focusNodeParent = node.ParentNode;
        //    tlData.DeleteNode(node);

        //    if (focusNodeParent != null)
        //    {
        //        string stt = TextUtils.ToString(focusNodeParent.GetValue(colSTT));
        //        for (int i = 0; i < focusNodeParent.Nodes.Count; i++)
        //        {
        //            tlData.SetRowCellValue(focusNodeParent.Nodes[i], "STT", $"{stt}.{i + 1}");
        //        }
        //    }
        //}

        //private void btnDelete_Click(object sender, EventArgs e)
        //{
        //    var selectedNodes = tlData.GetAllCheckedNodes();

        //    DialogResult dialogResult = new DialogResult();
        //    if (selectedNodes.Count <= 0)
        //    {
        //        string stt = TextUtils.ToString(tlData.GetFocusedRowCellValue(colSTT));
        //        dialogResult = MessageBox.Show($"Bạn có chắc muốn xoá dòng stt [{stt}]?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //        if (dialogResult == DialogResult.No) return;
        //        //DeleteNode(tlData.FocusedNode);

        //        tlData.DeleteNode(tlData.FocusedNode);

        //        int id = TextUtils.ToInt(tlData.GetFocusedRowCellValue(colID));
        //        TradePriceDetailModel detail = SQLHelper<TradePriceDetailModel>.FindByID(id);
        //        listDeletes.Add(detail);

        //    }
        //    else
        //    {

        //        dialogResult = MessageBox.Show("Bạn có chắc muốn xoá danh sách đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //        if (dialogResult == DialogResult.No) return;
        //        foreach (TreeListNode node in selectedNodes)
        //        {
        //            //DeleteNode(node);
        //            tlData.DeleteNode(node);

        //            int id = TextUtils.ToInt(node.GetValue(colID));
        //            TradePriceDetailModel detail = SQLHelper<TradePriceDetailModel>.FindByID(id);
        //            listDeletes.Add(detail);
        //        }
        //    }


        //    using (WaitDialogForm fWait = new WaitDialogForm())
        //    {
        //        //Update stt
        //        for (int i = 0; i < tlData.Nodes.Count; i++)
        //        {
        //            var node = tlData.Nodes[i];
        //            tlData.SetRowCellValue(node, "STT", i + 1);

        //            for (int j = 0; j < node.Nodes.Count; j++)
        //            {
        //                tlData.SetRowCellValue(node.Nodes[j], "STT", $"{i + 1}.{j + 1}");
        //            }
        //        }


        //        //Tính toán lại số liệu
        //        foreach (TreeListNode node in tlData.GetNodeList())
        //        {
        //            Calculator(node);
        //        }
        //    }



        //    //var focusNode = tlData.FocusedNode;

        //    //var focusNodeParent = focusNode.ParentNode;
        //    //tlData.DeleteNode(focusNode);

        //    //if (focusNodeParent != null)
        //    //{
        //    //    string stt = TextUtils.ToString(focusNodeParent.GetValue(colSTT));
        //    //    for (int i = 0; i < focusNodeParent.Nodes.Count; i++)
        //    //    {
        //    //        tlData.SetRowCellValue(focusNodeParent.Nodes[i], "STT", $"{stt}.{i + 1}");
        //    //    }
        //    //}


        //    //return;
        //    //var rowSelecteds = tlData.GetAllCheckedNodes();
        //    //if (rowSelecteds.Count <= 0)
        //    //{
        //    //    DialogResult dialog = MessageBox.Show("Bạn có chắc muốn xoá sản phẩm đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //    //    if (dialog != DialogResult.Yes) return;
        //    //    int id = TextUtils.ToInt(tlData.GetFocusedRowCellValue(colID));
        //    //    tlData.DeleteNode(tlData.FocusedNode);

        //    //    if (id <= 0) return;
        //    //    TradePriceDetailModel detail = SQLHelper<TradePriceDetailModel>.FindByID(id);
        //    //    listDeletes.Add(detail);

        //    //}
        //    //else
        //    //{
        //    //    DialogResult dialog = MessageBox.Show("Bạn có chắc muốn xoá danh sách đã chọn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //    //    if (dialog != DialogResult.Yes) return;
        //    //    foreach (TreeListNode node in rowSelecteds)
        //    //    {
        //    //        int id = TextUtils.ToInt(tlData.GetFocusedRowCellValue(colID));
        //    //        tlData.DeleteNode(node);

        //    //        if (id <= 0) continue;
        //    //        TradePriceDetailModel detail = SQLHelper<TradePriceDetailModel>.FindByID(id);
        //    //        listDeletes.Add(detail);

        //    //    }
        //    //}


        //    //foreach (TreeListNode node in tlData.GetNodeList())
        //    //{
        //    //    Calcalator(node);
        //    //}
        //}

        private void tlData_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (Lib.LockEvents) return;
            try
            {
                Lib.LockEvents = true;
                tlData.CloseEditor();

                if (e.Column.OptionsColumn.ReadOnly) return;
                Calculator(e.Node);

                ////Get giá trị
                //decimal rate = TextUtils.ToDecimal(txtRate.EditValue);//Tỷ giá
                //decimal rateCOM = TextUtils.ToDecimal(txtRateCom.EditValue);//Điền tỷ lệ COM
                //decimal com = TextUtils.ToDecimal(txtCOM.EditValue);//COM
                //decimal quantityRTCVision = txtQuantityRTCVision.Value;//Số lượng RTC Vision
                //decimal unitPriceRTCVision = TextUtils.ToDecimal(txtUnitPriceRTCVision.EditValue);//Số lượng RTC Vision
                //decimal quantity = TextUtils.ToDecimal(tlData.GetFocusedRowCellValue(colQuantity)); //Số lượng
                //decimal unitPriceImportEXW = TextUtils.ToDecimal(tlData.GetFocusedRowCellValue(colUnitImportPriceUSD)); //Đơn giá nhâp EXW (USD)

                //decimal bankCharge = TextUtils.ToDecimal(tlData.GetFocusedRowCellValue(colBankCharge)); //Phí ngân hàng
                //decimal protectiveTariff = TextUtils.ToDecimal(tlData.GetFocusedRowCellValue(colProtectiveTariff)); //Thuế nhập khẩu (%)
                //decimal feeShipPcs = TextUtils.ToDecimal(tlData.GetFocusedRowCellValue(colFeeShipPcs)); //Phí vận chuyển /1pcs
                //decimal ortherFees = TextUtils.ToDecimal(tlData.GetFocusedRowCellValue(colOrtherFees)); //Phí vận chuyển,HCHQ, CO, MSDS,vv bảo hiểm
                //decimal customFees = TextUtils.ToDecimal(tlData.GetFocusedRowCellValue(colCustomFees)); //Phí khai HQ
                //decimal unitPriceExpectCustomer = TextUtils.ToDecimal(tlData.GetFocusedRowCellValue(colUnitPriceExpectCustomer)); //Đơn giá dự kiến báo khách
                //decimal totalPriceLabor = TextUtils.ToDecimal(tlData.GetFocusedRowCellValue(colTotalPriceLabor)); //Giá báo nhân công

                ////Tính các trường theo công thức
                //decimal totalImport = quantity * unitPriceImportEXW; //Tổng giá nhập 
                //decimal unitImportPriceVND = unitPriceImportEXW * rate; //Đơn giá nhập chưa chi phí (VND)
                //decimal totalImportPriceVND = unitImportPriceVND * quantity; //Tổng giá nhập chưa Chi phí(VND)
                //decimal protectiveTariffPerPcs = protectiveTariff * (unitImportPriceVND + feeShipPcs); //Thuế nhập khẩu /1pcs(VND)
                //decimal totalProtectiveTariff = protectiveTariff * quantity; //Tổng thuế nhập khẩu
                //decimal totalImportPriceIncludeFees = totalImportPriceVND + bankCharge + totalProtectiveTariff + ortherFees + customFees; //Tổng giá nhập chưa VAT
                //decimal unitPriceIncludeFees = totalImportPriceVND != 0 ? totalImportPriceIncludeFees / totalImportPriceVND * unitImportPriceVND : 0; //Đơn giá về kho
                //decimal cmSet = (rateCOM * unitPriceExpectCustomer) / 100; //CM/set

                //decimal totalPriceExpectCustomer = unitPriceExpectCustomer * quantity; //Tổng đơn hàng
                //decimal profit = totalPriceExpectCustomer - totalImportPriceIncludeFees; //Lợi nhuận
                //decimal profitPercent = totalPriceExpectCustomer != 0 ? (profit / totalPriceExpectCustomer) * 100 : 0; //Tỷ lệ lợi nhuận
                //decimal totalPriceRTCVision = (unitPriceRTCVision + (unitPriceRTCVision * com / 100)) * quantityRTCVision; //Giá báo RTC Vision Software
                //decimal totalPrice = totalPriceExpectCustomer + totalPriceLabor + totalPriceRTCVision; //Tổng giá trị đầu ra đơn hàng

                ////Gán giá trị cho các trường
                //tlData.SetRowCellValue(e.Node, colTotalImportPriceUSD, totalImport);
                //tlData.SetRowCellValue(e.Node, colUnitImportPriceVND, unitImportPriceVND);
                //tlData.SetRowCellValue(e.Node, colTotalImportPriceVND, totalImportPriceVND);
                //tlData.SetRowCellValue(e.Node, colProtectiveTariffPerPcs, protectiveTariffPerPcs);
                //tlData.SetRowCellValue(e.Node, colTotalProtectiveTariff, totalProtectiveTariff);
                //tlData.SetRowCellValue(e.Node, colTotalImportPriceIncludeFees, totalImportPriceIncludeFees);
                //tlData.SetRowCellValue(e.Node, colUnitPriceIncludeFees, unitPriceIncludeFees);
                //tlData.SetRowCellValue(e.Node, colCMPerSet, cmSet);
                //tlData.SetRowCellValue(e.Node, colTotalPriceExpectCustomer, totalPriceExpectCustomer);
                //tlData.SetRowCellValue(e.Node, colProfit, profit);
                //tlData.SetRowCellValue(e.Node, colProfitPercent, profitPercent);
                //tlData.SetRowCellValue(e.Node, colTotalPriceRTCVision, totalPriceRTCVision);
                //tlData.SetRowCellValue(e.Node, colTotalPrice, totalPrice);
            }
            finally
            {
                Lib.LockEvents = false;
            }
        }

        private void chkIsRTCVision_CheckedChanged(object sender, EventArgs e)
        {
            txtQuantityRTCVision.Enabled = chkIsRTCVision.Checked;
            txtUnitPriceRTCVision.EditValue = chkIsRTCVision.Checked ? 15000000 : 0;
            foreach (TreeListNode node in tlData.GetNodeList())
            {
                Calculator(node);
            }
        }


        void Calculator(TreeListNode node)
        {
            //tlData.FocusedNode = tlData.Nodes.FirstNode;
            //Get giá trị
            //decimal currencyRate = TextUtils.ToDecimal(txtRate.EditValue);//Tỷ giá
            decimal currencyRate = TextUtils.ToDecimal(tlData.GetRowCellValue(node, colCurrencyRate));//Tỷ giá
            decimal rateCOM = TextUtils.ToDecimal(txtRateCom.EditValue);//Điền tỷ lệ COM
            //decimal com = TextUtils.ToDecimal(txtRateCom.EditValue);//COM
            decimal quantityRTCVision = txtQuantityRTCVision.Value;//Số lượng RTC Vision
            decimal unitPriceRTCVision = TextUtils.ToDecimal(txtUnitPriceRTCVision.EditValue);//Số lượng RTC Vision

            decimal quantity = TextUtils.ToDecimal(tlData.GetRowCellValue(node, colQuantity)); //Số lượng
            decimal unitPriceImportEXW = TextUtils.ToDecimal(tlData.GetRowCellValue(node, colUnitImportPriceUSD)); //Đơn giá nhâp EXW (USD)

            decimal bankCharge = TextUtils.ToDecimal(tlData.GetRowCellValue(node, colBankCharge)); //Phí ngân hàng
            decimal protectiveTariff = TextUtils.ToDecimal(tlData.GetRowCellValue(node, colProtectiveTariff)); //Thuế nhập khẩu (%)
            decimal feeShipPcs = TextUtils.ToDecimal(tlData.GetRowCellValue(node, colFeeShipPcs)); //Phí vận chuyển /1pcs
            decimal ortherFees = TextUtils.ToDecimal(tlData.GetRowCellValue(node, colOrtherFees)); //Phí vận chuyển,HCHQ, CO, MSDS,vv bảo hiểm
            decimal customFees = TextUtils.ToDecimal(tlData.GetRowCellValue(node, colCustomFees)); //Phí khai HQ
            //decimal unitPriceExpectCustomer = TextUtils.ToDecimal(tlData.GetRowCellValue(node, colUnitPriceExpectCustomer)); //Đơn giá dự kiến báo khách
            decimal totalPriceLabor = TextUtils.ToDecimal(tlData.GetRowCellValue(node, colTotalPriceLabor)); //Giá báo nhân công

            

            //Tính các trường theo công thức
            decimal totalImport = quantity * unitPriceImportEXW; //Tổng giá nhập 
            decimal unitImportPriceVND = unitPriceImportEXW * currencyRate; //Đơn giá nhập chưa chi phí (VND)
            decimal totalImportPriceVND = unitImportPriceVND * quantity; //Tổng giá nhập chưa Chi phí(VND)
            decimal protectiveTariffPerPcs = protectiveTariff * (unitImportPriceVND + feeShipPcs); //Thuế nhập khẩu /1pcs(VND)
            decimal totalProtectiveTariff = protectiveTariff * quantity; //Tổng thuế nhập khẩu
            //decimal totalImportPriceIncludeFees = totalImportPriceVND + bankCharge + totalProtectiveTariff + ortherFees + customFees; //Tổng giá nhập chưa VAT
            decimal totalImportPriceIncludeFees = TextUtils.ToDecimal(tlData.GetRowCellValue(node, colTotalImportPriceIncludeFees)); //Tổng giá nhập chưa VAT
            decimal unitPriceIncludeFees = totalImportPriceVND != 0 ? totalImportPriceIncludeFees / totalImportPriceVND * unitImportPriceVND : 0; //Đơn giá về kho

            //Tính đơn giá dự kiến báo khách
            if (tlData.FocusedColumn == colMargin)
            {
                decimal margin = TextUtils.ToDecimal(tlData.GetRowCellValue(node, colMargin)); //Margin

                var unitPriceExpect = Math.Round((double)(margin * unitPriceIncludeFees), 1);
                tlData.SetRowCellValue(node, colUnitPriceExpectCustomer, unitPriceExpect);
            }
            else if (tlData.FocusedColumn == colUnitPriceExpectCustomer)
            {
                decimal unitPriceExpect = TextUtils.ToDecimal(tlData.GetRowCellValue(node, colUnitPriceExpectCustomer)); //Đơn giá dự kiến báo khách

                decimal value = unitPriceIncludeFees == 0 ? 0 : unitPriceExpect / unitPriceIncludeFees;
                tlData.SetRowCellValue(node, colMargin, value);
            }

            

            decimal unitPriceExpectCustomer = TextUtils.ToDecimal(tlData.GetRowCellValue(node, colUnitPriceExpectCustomer)); //Đơn giá dự kiến báo khách

            decimal cmSet = (rateCOM * unitPriceExpectCustomer) / 100; //CM/set

            decimal totalPriceExpectCustomer = unitPriceExpectCustomer * quantity; //Tổng đơn hàng
            decimal profit = totalPriceExpectCustomer - totalImportPriceIncludeFees; //Lợi nhuận
            decimal profitPercent = totalPriceExpectCustomer != 0 ? (profit / totalPriceExpectCustomer) * 100 : 0; //Tỷ lệ lợi nhuận
            decimal totalPriceRTCVision = (unitPriceRTCVision + (unitPriceRTCVision * rateCOM)) * quantityRTCVision; //Giá báo RTC Vision Software
            decimal totalPrice = totalPriceExpectCustomer + totalPriceLabor + totalPriceRTCVision; //Tổng giá trị đầu ra đơn hàng

            //Gán giá trị cho các trường
            tlData.SetRowCellValue(node, colTotalImportPriceUSD, totalImport);
            tlData.SetRowCellValue(node, colUnitImportPriceVND, unitImportPriceVND);
            tlData.SetRowCellValue(node, colTotalImportPriceVND, totalImportPriceVND);
            tlData.SetRowCellValue(node, colProtectiveTariffPerPcs, protectiveTariffPerPcs);
            tlData.SetRowCellValue(node, colTotalProtectiveTariff, totalProtectiveTariff);
            tlData.SetRowCellValue(node, colTotalImportPriceIncludeFees, totalImportPriceIncludeFees);
            tlData.SetRowCellValue(node, colUnitPriceIncludeFees, unitPriceIncludeFees);
            tlData.SetRowCellValue(node, colCMPerSet, cmSet);
            tlData.SetRowCellValue(node, colTotalPriceExpectCustomer, totalPriceExpectCustomer);
            tlData.SetRowCellValue(node, colProfit, profit);
            tlData.SetRowCellValue(node, colProfitPercent, profitPercent);
            tlData.SetRowCellValue(node, colTotalPriceRTCVision, totalPriceRTCVision);
            tlData.SetRowCellValue(node, colTotalPrice, totalPrice);

            CalculatorBankCharge();
            CalculatorTotalImport();

            calculateTotal();

        }

        void CalculatorBankCharge()
        {
            //Tính phí ngân hàng
            decimal unitPriceDelivery = TextUtils.ToDecimal(txtUnitPriceDelivery.EditValue);
            decimal quantityDelivery = TextUtils.ToDecimal(txtQuantityDelivery.EditValue);
            decimal sumTotalImportPriceVND = TextUtils.ToDecimal(tlData.GetSummaryValue(colTotalImportPriceVND));
            decimal bankCharge = unitPriceDelivery * quantityDelivery + (0.002m * sumTotalImportPriceVND);

            //tlData.Nodes.FirstNode.SetValue("BankCharge", bankCharge);

            foreach (TreeListNode node in tlData.GetNodeList())
            {
                node.SetValue("BankCharge", bankCharge);
            }
        }

        void CalculatorTotalImport()
        {
            if (chkIsMergeTotalImportPriceIncludeFees.Checked)
            {
                decimal totalImportPriceVND = TextUtils.ToDecimal(tlData.GetSummaryValue(colTotalImportPriceVND)); //Tổng giá nhập chưa chi phí
                decimal bankCharge = TextUtils.ToDecimal(tlData.GetRowCellValue(tlData.FocusedNode, colBankCharge)); //Phí ngân hàng
                decimal totalProtectiveTariff = TextUtils.ToDecimal(tlData.GetSummaryValue(colTotalProtectiveTariff)); //Tổng thuế nhập khẩu
                decimal ortherFees = TextUtils.ToDecimal(tlData.GetSummaryValue(colOrtherFees)); //Phí vận chuyển,HCHQ, CO, MSDS,vv bảo hiểm
                decimal customFees = TextUtils.ToDecimal(tlData.GetSummaryValue(colCustomFees)); //Phí khai HQ
                decimal totalImportPriceIncludeFees = totalImportPriceVND + bankCharge + totalProtectiveTariff + ortherFees + customFees; //Tổng giá nhập chưa VAT

                //tlData.Nodes.FirstNode.SetValue("TotalImportPriceIncludeFees", totalImportPriceIncludeFees);

                foreach (TreeListNode node in tlData.GetNodeList())
                {
                    node.SetValue("TotalImportPriceIncludeFees", totalImportPriceIncludeFees);
                }
            }
            else
            {
                foreach (TreeListNode node in tlData.GetNodeList())
                {
                    decimal totalImportPriceVND = TextUtils.ToDecimal(tlData.GetRowCellValue(node, colTotalImportPriceVND)); //Tổng giá nhập chưa Chi phí(VND)
                    decimal bankCharge = TextUtils.ToDecimal(tlData.GetRowCellValue(node, colBankCharge)); //Phí ngân hàng
                    decimal totalProtectiveTariff = TextUtils.ToDecimal(tlData.GetRowCellValue(node, colTotalProtectiveTariff)); //Tổng thuế nhập khẩu
                    decimal ortherFees = TextUtils.ToDecimal(tlData.GetRowCellValue(node, colOrtherFees)); //Phí vận chuyển,HCHQ, CO, MSDS,vv bảo hiểm
                    decimal customFees = TextUtils.ToDecimal(tlData.GetRowCellValue(node, colCustomFees)); //Phí khai HQ
                    decimal totalImportPriceIncludeFees = totalImportPriceVND + bankCharge + totalProtectiveTariff + ortherFees + customFees; //Tổng giá nhập chưa VAT

                    node.SetValue("TotalImportPriceIncludeFees", totalImportPriceIncludeFees);
                }
            }
        }

        private void txtQuantityRTCVision_ValueChanged(object sender, EventArgs e)
        {
            foreach (TreeListNode node in tlData.GetNodeList())
            {
                Calculator(node);
            }
        }

        private void txtRateCom_EditValueChanged(object sender, EventArgs e)
        {
            decimal COM = TextUtils.ToDecimal(txtRateCom.Text);
            decimal totalPriceRTCVision = 0;
            for (int i = 0; i < tlData.AllNodesCount; i++)
            {
                TreeListNode node = tlData.GetNodeByVisibleIndex(i);
                totalPriceRTCVision = COM + 15000000;
                tlData.SetRowCellValue(node, colTotalPriceRTCVision, totalPriceRTCVision);
            }

            foreach (TreeListNode node in tlData.GetNodeList())
            {
                Calculator(node);
            }
        }

        //private void txtCOM_EditValueChanged(object sender, EventArgs e)
        //{
        //    foreach (TreeListNode node in tlData.GetNodeList())
        //    {
        //        Calculator(node);
        //    }
        //}

        private void tlData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                //var rowSelecteds = tlData.FocusedNode();
            }
        }

        private void cboCurrencyDetail_EditValueChanged(object sender, EventArgs e)
        {
            tlData.CloseEditor();
            SearchLookUpEdit lookUpEdit = (SearchLookUpEdit)sender;

            tlData.SetFocusedRowCellValue("CurrencyID", TextUtils.ToInt(lookUpEdit.EditValue));
            GetCurrencyRate(tlData.FocusedNode);
        }

        private void chkIsMergeTotalImportPriceIncludeFees_CheckedChanged(object sender, EventArgs e)
        {
            CalculatorTotalImport();
        }


        
    }
}