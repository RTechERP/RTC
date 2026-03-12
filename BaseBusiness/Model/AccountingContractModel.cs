
using System;
namespace BMS.Model
{
	public partial class AccountingContractModel : BaseModel
	{
		public int ID {get; set;}
		
		public DateTime? DateInput {get; set;}
		
		public int Company {get; set;}
		
		public int ContractGroup {get; set;}
		
		public int AccountingContractTypeID {get; set;}
		
		public int CustomerID {get; set;}
		
		public int SupplierSaleID {get; set;}
		
		public string ContractNumber {get; set;}
		public DateTime? DateContract { get; set;}
		
		public string ContractContent {get; set;}
		
		public decimal ContractValue {get; set;}
		public string Unit { get; set;}
		
		public string ContentPayment {get; set;}
		
		public DateTime? DateExpired {get; set;}
		
		public DateTime? DateIsApprovedGroup {get; set;}
		
		public int EmployeeID {get; set;}
		
		public string Note {get; set;}
		
		public bool IsReceivedContract {get; set;}
		
		public DateTime? DateReceived {get; set;}
		
		public int QuantityDocument {get; set;}
		
		public bool IsApproved {get; set;}
		
		public int ParentID {get; set;}
		
		public bool IsDelete {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
	}
}
	