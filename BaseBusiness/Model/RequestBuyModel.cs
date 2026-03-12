
using System;
namespace BMS.Model
{
	public partial class RequestBuyModel : BaseModel
	{
		public int ID {get; set;}
		
		public string RequestBuyCode {get; set;}
		
		public int CustomerID {get; set;}
		
		public int ProjectID {get; set;}
		
		public int DepartmentID {get; set;}
		
		public int RequestPersonID {get; set;}
		
		public int RequestBuyStatus {get; set;}
		
		public int RequestType {get; set;}
		
		public DateTime? DeadLine {get; set;}
		
		public string Purpose {get; set;}
		
		public string Note {get; set;}
		
		public int YearCreate {get; set;}
		
		public int SortOrder {get; set;}
		
		public decimal DeliveryCost {get; set;}
		
		public decimal CustomsCost {get; set;}
		
		public decimal BankCost {get; set;}
		
		public bool IsImport {get; set;}
		
		public decimal QtySet {get; set;}
		
		public decimal Price {get; set;}
		
		public decimal TotalPrice {get; set;}
		
		public bool IsApproved {get; set;}
		
		public int ApprovedID {get; set;}
		
		public DateTime? ApprovedDate {get; set;}
		
		public bool IsDeleted {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public int SupplierID {get; set;}
		
	}
}
	