
using System;
namespace BMS.Model
{
	public partial class QuotationModel : BaseModel
	{
		public int ID {get; set;}
		
		public int RequestPriceID {get; set;}
		
		public int ProjectID {get; set;}
		
		public int CustomerID {get; set;}
		
		public int SaleID {get; set;}
		
		public int CurrencyID {get; set;}
		
		public string POCode {get; set;}
		
		public bool IsApproved {get; set;}
		
		public int QuotationType {get; set;}
		
		public int QuotationStatus {get; set;}
		
		public string QuotationCode {get; set;}
		
		public DateTime? QuotationDate {get; set;}
		
		public string ContactName {get; set;}
		
		public string ContactPhone {get; set;}
		
		public string ContactEmail {get; set;}
		
		public string TotalName {get; set;}
		
		public decimal Qty {get; set;}
		
		public decimal QtySet {get; set;}
		
		public decimal VAT {get; set;}
		
		public decimal PricePS {get; set;}
		
		public decimal PricePSVAT {get; set;}
		
		public decimal PriceVT {get; set;}
		
		public decimal TotalVT {get; set;}
		
		public decimal Price {get; set;}
		
		public decimal TotalPrice {get; set;}
		
		public decimal PriceVAT {get; set;}
		
		public decimal TotalPriceVAT {get; set;}
		
		public decimal DeliveryCost {get; set;}
		
		public decimal BankCost {get; set;}
		
		public decimal CustomsCost {get; set;}
		
		public decimal Rate {get; set;}
		
		public string Note {get; set;}
		
		public string DeliveryPeriod {get; set;}
		
		public string DeliveryFees {get; set;}
		
		public string Validity {get; set;}
		
		public string CustomClearance {get; set;}
		
		public string Warranty {get; set;}
		
		public string Payment {get; set;}
		
		public string PlaceDelivery {get; set;}
		
		public string Adjusting {get; set;}
		
		public bool IsDelete {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		public int TradePriceID { get; set;}
		
	}
}
	