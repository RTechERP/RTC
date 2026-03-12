using BaseBusiness.DTO;
using BMS.Model;
using DevExpress.XtraReports.UI;
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
    public partial class frmQuotationSaleViewDetail : _Forms
    {
        public int quotationID = 0;
        string imageURL = "http://113.190.234.64:8083/api/imagesign";
        public frmQuotationSaleViewDetail()
        {
            InitializeComponent();
        }

        private void documentViewer1_Load(object sender, EventArgs e)
        {
            LoadQuotationSaleReport();
        }
        void LoadQuotationSaleReport()
        {
            QuotationSaleReport quotationReport = new QuotationSaleReport();
            documentViewer1.DocumentSource = quotationReport;
            List<QuotationDetailDTO> listDetail = SQLHelper<QuotationDetailDTO>.ProcedureToList("spGetQuotationDetail_ByMasterID", new string[] { "@QuotationID" }, new object[] { quotationID });
            quotationReport.DataSource = listDetail;
            QuotationModel quotation = SQLHelper<QuotationModel>.FindByID(quotationID);
            if (quotation == null) return;
            CustomerModel customer = SQLHelper<CustomerModel>.FindByID(quotation.CustomerID);
            EmployeeModel emp = SQLHelper<EmployeeModel>.FindByID(quotation.SaleID);
            List<QuotationTermLinkModel> listLink = SQLHelper<QuotationTermLinkModel>.FindByAttribute("QuotationID", quotationID);
            List<int> listTermID = listLink.Select(link => link.TermConditionID).ToList();
            GroupFooterBand groupFooter = quotationReport.Bands.OfType<GroupFooterBand>().FirstOrDefault(b => b.Name == "GroupFooter2");
            float currentY = 1f;

            float labelWidth = 830f;
            float labelHeight = 50f;
            foreach (int termID in listTermID)
            {
                TermConditionModel term = SQLHelper<TermConditionModel>.FindByID(termID);
                string description = $"* {term.DescriptionVietnamese}\n* {term.DescriptionEnglish}";
                XRLabel label = new XRLabel();
                label.WordWrap = true;
                label.Multiline = true;
                label.Text = description;
                label.LocationF = new PointF(1f, currentY);

                label.SizeF = new SizeF(labelWidth, labelHeight);

                label.Borders = DevExpress.XtraPrinting.BorderSide.All;
                label.BorderColor = Color.Black;
                label.BorderWidth = 0.5f;
                label.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;

                groupFooter.Controls.Add(label);

                currentY += labelHeight;
            }

            quotationReport.Parameters["QuotationCode"].Value = quotation.QuotationCode;
            quotationReport.Parameters["QuotationDate"].Value = quotation.QuotationDate.Value.ToString("dd/MM/yyyy");
            quotationReport.Parameters["CustomerName"].Value = customer.CustomerName;
            quotationReport.Parameters["Address"].Value = customer.Address;
            quotationReport.Parameters["ContactName"].Value = quotation.ContactName;
            quotationReport.Parameters["ContactPhone"].Value = quotation.ContactPhone != null ? quotation.ContactPhone : "";
            quotationReport.Parameters["ContactMail"].Value = quotation.ContactEmail != null ? quotation.ContactEmail : "";
            quotationReport.Parameters["TotalPrice"].Value = (int)quotation.TotalPrice;
            quotationReport.Parameters["TotalMoneyText"].Value = NumberMoneyToText.ConvertNumberToTextVietNamese(quotation.TotalPrice, "VND");
            quotationReport.Parameters["TotalMoneyTextEnglish"].Value = NumberMoneyToText.ConvertNumberToTextEnglish(quotation.TotalPrice, "VND");
            quotationReport.Parameters["DeliveryPeriod"].Value = quotation.DeliveryPeriod != null ? quotation.DeliveryPeriod : "";
            quotationReport.Parameters["FullName"].Value = emp.FullName != null? emp.FullName : "";
            quotationReport.Parameters["SDTCaNhan"].Value = emp.SDTCaNhan != null ? emp.SDTCaNhan : "";
            quotationReport.Parameters["EmailCongTy"].Value = emp.EmailCongTy != null ? emp.EmailCongTy : "";
            //quotationReport.picApproved.ImageUrl = $".jpg";
        }

        private void frmQuotationSaleViewDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmQuotationSaleViewDetail_Load(object sender, EventArgs e)
        {

        }
    }
}