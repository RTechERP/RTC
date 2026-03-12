
using System;
namespace BMS.Model
{
	public partial class RequestPriceDetailModel : BaseModel
	{
		public long ID {get; set;}
		
		public int RequestPriceID {get; set;}
		
		public long ParentID {get; set;}
		
		public int PartID {get; set;}
		
		public string PartCode {get; set;}
		
		public string PartName {get; set;}
		
		public string Manufacturer {get; set;}
		
		public int SupplierID {get; set;}
		
		public string SupplierName {get; set;}
		
		public int AskPriceID {get; set;}
		
		public string PeriodExpected {get; set;}
		
		public decimal MinDay {get; set;}
		
		public decimal MaxDay {get; set;}
		
		public DateTime? SupplierReplyDate {get; set;}
		
		public string Unit {get; set;}
		
		public string CurrencyUnit {get; set;}
		
		public decimal CurrencyRate {get; set;}
		
		public decimal QtySet {get; set;}
		
		public decimal QtyPS {get; set;}
		
		public decimal Qty {get; set;}
		
		public decimal Price {get; set;}
		
		public decimal TotalPrice {get; set;}
		
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
		
		public decimal FinishPrice {get; set;}
		
		public decimal FinishTotalPrice {get; set;}
		
		public decimal PricePS {get; set;}
		
		public string ContactName {get; set;}
		
		public string ContactPhone {get; set;}
		
		public string ContactEmail {get; set;}
		
		public string ContactWebsite {get; set;}
		
		public string Status {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public int STT {get; set;}
		
		public int Priority {get; set;}
		
		public DateTime? DeadLine {get; set;}
		
		public string ProjectName {get; set;}
		
		public string CustomerName {get; set;}
		
		public int UserID {get; set;}
		
		public string Note {get; set;}
		
		public string Link {get; set;}
		
		public string FileName {get; set;}
		
		public bool IsApproved {get; set;}
		
		public DateTime? AskDate {get; set;}
		
		public string NoteSlowCheck {get; set;}
		
		public int Month {get; set;}
		
		public int Year {get; set;}
		
	}
}
	