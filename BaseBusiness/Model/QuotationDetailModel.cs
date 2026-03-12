
using System;
namespace BMS.Model
{
	public partial class QuotationDetailModel : BaseModel
	{
		public long ID {get; set;}
		
		public long ParentID {get; set;}
		
		public int QuotationID {get; set;}
		
		public int ManufacturerID {get; set;}
		
		public int SupplierID {get; set;}
		
		public long RequestPriceDetailID {get; set;}
		
		public int PartID {get; set;}
		
		public string PartCode {get; set;}
		
		public string PartName {get; set;}
		
		public string PartCodeRTC {get; set;}
		
		public string PartNameRTC {get; set;}
		
		public string PeriodExpected {get; set;}
		
		public string Unit {get; set;}
		
		public string CurrencyUnit {get; set;}
		
		public decimal CurrencyRate {get; set;}
		
		public decimal QtySet {get; set;}
		
		public decimal QtyPS {get; set;}
		
		public decimal Qty {get; set;}
		
		public decimal VAT {get; set;}
		
		public decimal PriceVAT {get; set;}
		
		public decimal TotalVAT {get; set;}
		
		public decimal PriceCurrency {get; set;}
		
		public decimal TotalPriceCurrency {get; set;}
		
		public decimal DeliveryCost {get; set;}
		
		public decimal BankCost {get; set;}
		
		public decimal CustomsCost {get; set;}
		
		public decimal TaxImportPercent {get; set;}
		
		public decimal TaxImporPrice {get; set;}
		
		public decimal TaxImporTotal {get; set;}
		
		public decimal PricePS {get; set;}
		
		public decimal FinishPrice {get; set;}
		
		public decimal FinishTotalPrice {get; set;}
		
		public decimal PriceVT {get; set;}
		
		public decimal TotalVT {get; set;}
		
		public decimal PriceOld {get; set;}
		
		public decimal Price {get; set;}
		
		public decimal TotalPrice {get; set;}
		
		public string ContactName {get; set;}
		
		public string ContactPhone {get; set;}
		
		public string ContactEmail {get; set;}
		
		public string ContactWebsite {get; set;}
		
		public string Delivery {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		public string STT { get; set;}
		public string Maker { get; set;}
		public int TradePriceDetailID { get; set;}
		
	}
}
	