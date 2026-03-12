
using System;
namespace BMS.Model
{
	public partial class PurchaseOrderModel : BaseModel
	{
		public int ID {get; set;}
		
		public int SupplierID {get; set;}
		
		public int BuyPersonID {get; set;}
		
		public string PurchaseOrderCode {get; set;}
		
		public DateTime? PODate {get; set;}
		
		public string PeriodExpected {get; set;}
		
		public decimal TotalPrice {get; set;}
		
		public decimal TotalVAT {get; set;}
		
		public decimal FinishPrice {get; set;}
		
		public string ContactName {get; set;}
		
		public string ContactPhone {get; set;}
		
		public string ContactEmail {get; set;}
		
		public string Payment {get; set;}
		
		public string DateAndDelivery {get; set;}
		
		public string DeliveryAndFees {get; set;}
		
		public string PlaceOfDelivery {get; set;}
		
		public int POStatus {get; set;}
		
		public bool IsApproved {get; set;}
		
		public int ApprovedID {get; set;}
		
		public DateTime? ApprovedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
	}
}
	