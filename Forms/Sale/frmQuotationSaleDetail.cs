using BMS.Business;
using BMS.Model;
using BMS.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTreeList.Nodes;
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
    public partial class frmQuotationSaleDetail : _Forms
    {
        public QuotationModel quotation = new QuotationModel();
        public TradePriceModel tradePrice = new TradePriceModel();

        DataTable dtDetail = new DataTable();
        public frmQuotationSaleDetail()
        {
            InitializeComponent();
        }

        private void frmQuotationSaleDetail_Load(object sender, EventArgs e)
        {
            loadCustomer();
            loadProject();
            loadTermCondition();
            loadEmployee();
            loadData();
        }
        void loadProject()
        {
            List<ProjectModel> list = SQLHelper<ProjectModel>.FindAll();
            cboProject.Properties.DataSource = list;
            cboProject.Properties.DisplayMember = "ProjectName";
            cboProject.Properties.ValueMember = "ID";
        }
        void loadEmployee()
        {
            List<EmployeeModel> list = SQLHelper<EmployeeModel>.FindAll();
            cboSale.Properties.DataSource = list;
            cboSale.Properties.DisplayMember = "FullName";
            cboSale.Properties.ValueMember = "ID";
        }
        void loadCustomer()
        {
            List<CustomerModel> list = SQLHelper<CustomerModel>.FindAll();
            cboCustomer.Properties.DataSource = list;
            cboCustomer.Properties.DisplayMember = "CustomerName";
            cboCustomer.Properties.ValueMember = "ID";
        }
        void loadTermCondition()
        {
            List<TermConditionModel> list = SQLHelper<TermConditionModel>.FindAll();
            cboTermCondition.Properties.DataSource = list;
            cboTermCondition.Properties.DisplayMember = "DescriptionVietnamese";
            cboTermCondition.Properties.ValueMember = "ID";
        }

        private void cboCustomer_EditValueChanged(object sender, EventArgs e)
        {
            List<CustomerContactModel> list = SQLHelper<CustomerContactModel>.FindByAttribute("CustomerID", TextUtils.ToInt(cboCustomer.EditValue));
            txtContactName.Text = list.Last().ContactName;
            txtContactEmail.Text = list.Last().ContactEmail;
            txtContactPhone.Text = list.Last().ContactPhone;
            txtCustomerAddress.Text = SQLHelper<CustomerModel>.FindByID(TextUtils.ToInt(cboCustomer.EditValue)).Address;
        }
        void loadData()
        {
            if (tradePrice.ID > 0)
            {
                //DataSet ds = TextUtils.LoadDataSetFromSP("spGetTradePrice", new string[] { "@ID", "@EmpID" }, new object[] { tradePrice.ID, 0 });
                dtDetail = TextUtils.LoadDataFromSP("spGetTradePriceDetail","A", new string[] { "@TradePriceID" }, new object[] { tradePrice.ID });
                //dtDetail = ds.Tables[2];
                cboCustomer.EditValue = tradePrice.CustomerID;
                cboProject.EditValue = tradePrice.ProjectID;
                cboSale.EditValue = tradePrice.SaleAdminID;
                dteQuotationDate.EditValue = DateTime.Now;
                int stt = 1;

                foreach (DataRow row in dtDetail.Rows)
                {
                    TreeListNode node = TreeData.AppendNode(null, null);
                    node[colSTT] = stt++;
                    //string productName = TextUtils.ToString(row["ProductName"]);
                    //string productCode = TextUtils.ToString(row["ProductCode"]);
                    node[colProductName] = TextUtils.ToString(row["ProductName"]);
                    node[colQuantity] = TextUtils.ToString(row["Quantity"]);
                    node[colUnit] = TextUtils.ToString(row["Unit"]);
                    node[colUnitImportPriceVND] = TextUtils.ToString(row["UnitImportPriceVND"]);
                    node[colTotalImportPriceVND] = TextUtils.ToString(row["TotalImportPriceVND"]);
                    node[colTradePriceDetailID] = TextUtils.ToInt(row["ID"]);
                }
                //TreeData.DataSource = dtDetail;
                //TreeData.DataSource = dtDetail;

                txtTotalPrice.EditValue = TreeData.GetSummaryValue(colTotalImportPriceVND);
                TreeData.ExpandAll();
            }
            if (quotation.ID > 0)
            {
                cboCustomer.EditValue = quotation.CustomerID;
                cboProject.EditValue = quotation.ProjectID;
                txtCode.Text = quotation.QuotationCode;
                cboSale.EditValue = quotation.SaleID;
                txtCustomerAddress.Text = SQLHelper<CustomerModel>.FindByID(TextUtils.ToInt(quotation.CustomerID)).Address;
                txtContactName.Text = quotation.ContactName;
                txtContactPhone.Text = quotation.ContactPhone;
                txtContactEmail.Text = quotation.ContactEmail;
                txtTotalPrice.EditValue = quotation.TotalPrice;
                txtDeliveryPeriod.Text = quotation.DeliveryPeriod;
                dteQuotationDate.EditValue = quotation.QuotationDate;
                List<QuotationTermLinkModel> listLink = SQLHelper<QuotationTermLinkModel>.FindByAttribute("QuotationID", quotation.ID);
                List<int> value = new List<int>();
                for (int i = 0; i < listLink.Count; i++)
                {
                    value.Add(listLink[i].TermConditionID);
                }
                string strValue = string.Join(",", value);

                cboTermCondition.SetEditValue(strValue);

                dtDetail = TextUtils.LoadDataFromSP("spGetQuotationDetail_ByMasterID", "A"
                 , new string[] { "@QuotationID" }
                 , new object[] { quotation.ID });
                dtDetail.AcceptChanges();
                TreeData.DataSource = dtDetail;
                colSTT.OptionsColumn.ReadOnly = colQuantity.OptionsColumn.ReadOnly = colUnit.OptionsColumn.ReadOnly
                    = colUnitImportPriceVND.OptionsColumn.ReadOnly = colTotalImportPriceVND.OptionsColumn.ReadOnly = true;
            }
            if (quotation.ID == 0)
                txtCode.Text = CreateCode();
        }
        string CreateCode()
        {
            DateTime dateNow = DateTime.Now;
            string code = "";
            var exp1 = new Expression("YEAR(CreatedDate)", dateNow.Year);
            var exp2 = new Expression("MONTH(CreatedDate)", dateNow.Month);
            var exp3 = new Expression("DAY(CreatedDate)", dateNow.Day);
            var currentCode = SQLHelper<QuotationModel>.FindByExpression(exp1.And(exp2).And(exp3)).OrderByDescending(x => x.ID).FirstOrDefault();


            code = currentCode == null ? "" : currentCode.QuotationCode;
            if (string.IsNullOrEmpty(code))
            {
                code = $"RTC_QUO_{dateNow.ToString("ddMMyyyy")}0001";
                return code;
            }

            int sttOrder = TextUtils.ToInt(code.Substring(code.Length - 4)) + 1;
            string sttText = sttOrder.ToString();
            for (int i = 0; sttText.Length < 4; i++)
            {
                sttText = "0" + sttText;
            }

            code = $"RTC_QUO_{dateNow.ToString("ddMMyyyy")}{sttText}";
            return code;
        }
        bool validate()
        {
            return true;
        }
        bool saveData()
        {
            if (!validate()) return false;

            quotation.ProjectID = TextUtils.ToInt(cboProject.EditValue);
            quotation.CustomerID = TextUtils.ToInt(cboCustomer.EditValue);
            quotation.SaleID = TextUtils.ToInt(cboSale.EditValue);

            quotation.QuotationDate = TextUtils.ToDate5(dteQuotationDate.EditValue);
            quotation.ContactEmail = txtContactEmail.Text.Trim();
            quotation.ContactName = txtContactName.Text.Trim();
            quotation.ContactPhone = txtContactPhone.Text.Trim();
            quotation.DeliveryPeriod = txtDeliveryPeriod.Text.Trim();
            quotation.PlaceDelivery = txtCustomerAddress.Text.Trim();
            quotation.TotalPrice = TextUtils.ToDecimal(txtTotalPrice.EditValue);

            quotation.QuotationType = 0;

            quotation.QuotationCode = txtCode.Text.Trim();
            QuotationTermLinkModel link = new QuotationTermLinkModel();

            if (quotation.ID == 0)
            {
                quotation.TradePriceID = tradePrice.ID;
                quotation.ID = (int)QuotationBO.Instance.Insert(quotation);
                foreach (CheckedListBoxItem item in cboTermCondition.Properties.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                    {
                        link.QuotationID = quotation.ID;
                        link.TermConditionID = TextUtils.ToInt(item.Value);
                        QuotationTermLinkBO.Instance.Insert(link);
                    }
                }
            }
            else
            {
                QuotationBO.Instance.Update(quotation);
                tradePrice = SQLHelper<TradePriceModel>.FindByID(quotation.TradePriceID);
                tradePrice.ProjectID = quotation.ProjectID;
                tradePrice.CustomerID = quotation.CustomerID;
                tradePrice.SaleAdminID = quotation.SaleID;
                TradePriceBO.Instance.Update(tradePrice);
                foreach (CheckedListBoxItem item in cboTermCondition.Properties.Items)
                {
                    var exp1 = new Expression("QuotationID", quotation.ID);
                    var exp2 = new Expression("TermConditionID", TextUtils.ToInt(item.Value));
                    link.QuotationID = quotation.ID;
                    link.TermConditionID = TextUtils.ToInt(item.Value);
                    List<QuotationTermLinkModel> listLink = SQLHelper<QuotationTermLinkModel>.FindByExpression(exp1.And(exp2));

                    if (listLink.Count <= 0)
                    {
                        if (item.CheckState == CheckState.Checked)
                        {
                            QuotationTermLinkBO.Instance.Insert(link);
                        }
                    }
                    else
                    {
                        if (item.CheckState == CheckState.Unchecked)
                        {
                            QuotationTermLinkBO.Instance.Delete(listLink.FirstOrDefault().ID);
                        }
                    }
                }
            }

            int count = TreeData.AllNodesCount;
            for (int i = 0; i < count; i++)
            {
                TreeListNode node = TreeData.GetNodeByVisibleIndex(i);
                long id = TextUtils.ToInt(TreeData.GetRowCellValue(node, colDetailID));
                QuotationDetailModel detail = new QuotationDetailModel();
                if (id > 0)
                {
                    //detail = (QuotationDetailModel)QuotationDetailBO.Instance.FindByPK(id);
                    detail = SQLHelper<QuotationDetailModel>.FindByID(id);
                }
                detail.STT = TextUtils.ToString(TreeData.GetRowCellValue(node, colSTT)).Trim();
                detail.QuotationID = quotation.ID;
                detail.PartCode = TextUtils.ToString(TreeData.GetRowCellValue(node, colProductCode));
                detail.PartName = TextUtils.ToString(TreeData.GetRowCellValue(node, colProductName));
                detail.Unit = TextUtils.ToString(TreeData.GetRowCellValue(node, colUnit));

                detail.Qty = TextUtils.ToDecimal(TreeData.GetRowCellValue(node, colQuantity));

                detail.FinishPrice = TextUtils.ToDecimal(TreeData.GetRowCellValue(node, colUnitImportPriceVND));
                detail.FinishTotalPrice = TextUtils.ToDecimal(TreeData.GetRowCellValue(node, colTotalImportPriceVND));

                detail.Maker = TextUtils.ToString(TreeData.GetRowCellValue(node, colMaker));
                detail.TradePriceDetailID = TextUtils.ToInt(TreeData.GetRowCellValue(node, colTradePriceDetailID));

                if (detail.ID == 0)
                {
                    QuotationDetailBO.Instance.Insert(detail);
                }
                else
                {
                    QuotationDetailBO.Instance.Update(detail);
                }


                //Update lại mã SP tính giá
                TradePriceDetailModel tradePriceDetail = SQLHelper<TradePriceDetailModel>.FindByID(detail.TradePriceDetailID);
                tradePriceDetail.Unit = detail.Unit;
                //tradePriceDetail.ProductCodeCustomer = detail.PartCode;
                tradePriceDetail.ProductName = detail.PartName;
                tradePriceDetail.Maker = detail.Maker;
                tradePriceDetail.Quantity = TextUtils.ToInt(detail.Qty);
                TradePriceDetailBO.Instance.Update(tradePriceDetail);
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void TreeData_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            int quantity = TextUtils.ToInt(TreeData.GetFocusedRowCellValue(colQuantity));
            decimal unitprice = TextUtils.ToDecimal(TreeData.GetFocusedRowCellValue(colUnitImportPriceVND));
            decimal total = quantity * unitprice;
            if (quantity > 0)
            {
                if (e.Column == colQuantity)
                {
                    TreeData.SetFocusedRowCellValue(colTotalImportPriceVND, total);
                }
            }

        }

        private void btnAddTermCondition_Click(object sender, EventArgs e)
        {
            frmTermConditionDetail frm = new frmTermConditionDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                loadTermCondition();
            }
        }
    }
}