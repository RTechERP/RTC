
using System;
namespace BMS.Model
{
	public partial class FollowProjectDetailModel : BaseModel
	{
		public int ID {get; set;}
		
		public int Status {get; set;}
		
		public DateTime? PODate {get; set;}
		
		public DateTime? DeliveryRequestedDate {get; set;}
		
		public DateTime? OderDate {get; set;}
		
		public DateTime? ShipmentDate {get; set;}
		
		public DateTime? ArrivalDate {get; set;}
		
		public string Partner {get; set;}
		
		public int Qty {get; set;}
		
		public int FollowProjectID {get; set;}
		
		public int ProductID {get; set;}
		
		public string ProjectModel {get; set;}
		
		public string StandardModel {get; set;}
		
		public decimal UnitPriceUSD {get; set;}
		
		public decimal UnitPriceVND {get; set;}
		
		public decimal TotalPriceUSD {get; set;}
		
		public decimal TotalPriceVND {get; set;}
		
		public decimal ImportTax {get; set;}
		
		public decimal ImportTaxVND {get; set;}
		
		public decimal TotalImportTax {get; set;}
		
		public decimal CustomFees {get; set;}
		
		public decimal Declaration {get; set;}
		
		public decimal BankCharges {get; set;}
		
		public decimal InsuranceFees {get; set;}
		
		public decimal TransportFee {get; set;}
		
		public decimal TotalCustomfees {get; set;}
		
		public decimal NumberOfTransactions {get; set;}
		
		public decimal TotalBankCharges {get; set;}
		
		public int Progress {get; set;}
		
		public decimal Exchange {get; set;}
		
		public int Debt {get; set;}
		
		public string Bill {get; set;}
		
		public bool IsPay {get; set;}
		
		public bool IsAddWarehouse {get; set;}
		
		public bool IsItemReceived {get; set;}
		
		public bool IsBillStatus {get; set;}
		
		public DateTime? PayDate {get; set;}
		
		public int QtyCustomer {get; set;}
		
		public string PONo {get; set;}
		
		public int LeadTime {get; set;}
		
		public bool StatusShip {get; set;}
		
		public bool IsAlreadyDelivered {get; set;}
		
		public string Note {get; set;}
		
		public int ParentID {get; set;}
		
		public decimal CostIncludingVATDetail {get; set;}
		
		public decimal CostWithoutVATDetail {get; set;}
		
		public decimal TaxDetail {get; set;}
		
		public int NewRow {get; set;}
		
		public int POKHDetailID {get; set;}
		
		public int STT {get; set;}
		
		public decimal OldPrice {get; set;}
		
		public int Month {get; set;}
		
		public int Year {get; set;}
		
		public int SupplierID {get; set;}
		
		public bool SlowDelivery {get; set;}
		
		public string NoteDelivery {get; set;}
		
	}
}
	