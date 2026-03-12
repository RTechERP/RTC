
using System;
namespace BMS.Model
{
	public partial class PartSummaryDetailModel : BaseModel
	{
		public long ID {get; set;}
		
		public int PartID {get; set;}
		
		public int RequestID {get; set;}
		
		public int QuotationID {get; set;}
		
		public int POCustomerID {get; set;}
		
		public int POSupplierID {get; set;}
		
		public int ProjectID {get; set;}
		
		public int SupplierID {get; set;}
		
		public int ManufacturerID {get; set;}
		
		public int ParentID {get; set;}
		
		public int ProjectModuleID {get; set;}
		
		public int SaleID {get; set;}
		
		public int BuyPersonID {get; set;}
		
		public int AskPriceID {get; set;}
		
		public string PartCode {get; set;}
		
		public string PartName {get; set;}
		
		public string PartCodeRTC {get; set;}
		
		public string PartNameRTC {get; set;}
		
		public decimal Qty {get; set;}
		
		public string Unit {get; set;}
		
		public string CurrencyUnit {get; set;}
		
		public decimal VAT {get; set;}
		
		public decimal Price {get; set;}
		
		public decimal PriceVAT {get; set;}
		
		public decimal TotalPrice {get; set;}
		
		public decimal TotalPriceVAT {get; set;}
		
		public decimal PriceCurrency {get; set;}
		
		public decimal TotalPriceCurrency {get; set;}
		
		public decimal DeliveryCost {get; set;}
		
		public decimal BankCost {get; set;}
		
		public decimal VATCurrencyPercent {get; set;}
		
		public decimal VATCurrencyPrice {get; set;}
		
		public decimal VATCurrencyCost {get; set;}
		
		public decimal CustomsCost {get; set;}
		
		public decimal FinishTotalPrice {get; set;}
		
		public decimal PriceQuotation {get; set;}
		
		public decimal TotalPriceQuotation {get; set;}
		
		public string ContactName {get; set;}
		
		public string ContactPhone {get; set;}
		
		public string ContactEmail {get; set;}
		
		public DateTime? DateRequestPrice {get; set;}
		
		public DateTime? DateReplyPrice {get; set;}
		
		public string PeriodExpected {get; set;}
		
		public decimal MinDay {get; set;}
		
		public decimal MaxDay {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
	}
}
	