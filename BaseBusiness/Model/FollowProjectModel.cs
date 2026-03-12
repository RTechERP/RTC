
using System;
namespace BMS.Model
{
	public partial class FollowProjectModel : BaseModel
	{
		public int ID {get; set;}
		
		public string POCode {get; set;}
		
		public int CustomerID {get; set;}
		
		public string Project {get; set;}
		
		public string Status {get; set;}
		
		public decimal TotalCostWithoutVAT {get; set;}
		
		public decimal TotalCostIncludingVAT {get; set;}
		
		public decimal Tax {get; set;}
		
		public decimal TotalCustomFees {get; set;}
		
		public decimal TotalTransportFee {get; set;}
		
		public decimal TotalCustomerQuotation {get; set;}
		
		public decimal TotalBankCharges {get; set;}
		
		public decimal TransportFee {get; set;}
		
		public decimal CustomerQuotation {get; set;}
		
		public DateTime? CreateDate {get; set;}
		
		public int UserID {get; set;}
		
		public int ProjectID {get; set;}
		
		public int POKHID {get; set;}
		
		public decimal Exchange {get; set;}
		
		public decimal CustomFees {get; set;}
		
		public decimal Declaration {get; set;}
		
		public decimal BankCharges {get; set;}
		
		public decimal NumberOfTransactions {get; set;}
		
		public bool IsApproved {get; set;}
		
	}
}
	