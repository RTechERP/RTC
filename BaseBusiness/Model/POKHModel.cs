
using System;
namespace BMS.Model
{
	public partial class POKHModel : BaseModel
	{
		public int ID {get; set;}
		
		public int Status {get; set;}
		
		public string POCode {get; set;}
		
		public string UserName {get; set;}
		
		public int ProjectID {get; set;}
		
		public string BillCode {get; set;}
		
		public DateTime? ReceivedDatePO {get; set;}
		
		public decimal TotalMoneyPO {get; set;}
		
		public DateTime? StartDate {get; set;}
		
		public DateTime? EndDate {get; set;}
		
		public int DeliveryStatus {get; set;}
		
		public int ImportStatus {get; set;}
		
		public int ExportStatus {get; set;}
		
		public string Note {get; set;}
		
		public string GroupID {get; set;}
		
		public int CustomerID {get; set;}
		
		public int EndUserID {get; set;}
		
		public int DealerID {get; set;}
		
		public bool IsApproved {get; set;}
		
		public int POType {get; set;}
		
		public int Month {get; set;}
		
		public int Year {get; set;}
		
		public bool NewAccount {get; set;}
		
		public int UserID {get; set;}
		
		public string EndUser {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public decimal ReceiveMoney {get; set;}
		
		public bool IsPay {get; set;}
		
		public bool IsShip {get; set;}
		
		public bool IsExport {get; set;}
		
		public bool IsBill {get; set;}
		
		public decimal TotalMoneyKoVAT {get; set;}
		
		public int FollowProjectID {get; set;}
		
		public int PartID {get; set;}
		
		public int UserType {get; set;}
		
		public int QuotationID {get; set;}
		
		public string PONumber {get; set;}
		
		public bool IsMerge {get; set;}
		public int WarehouseID { get; set;}
		public int CurrencyID { get; set;}
		public int PaymentStatus { get; set; }
		public int AccountType { get; set; }
        public decimal? Discount { get; set; }
        public decimal? TotalMoneyDiscount { get; set; }

    }
}
	