
using System;
namespace BMS.Model
{
	public partial class TSAssetManagementHistoryModel : BaseModel
	{
		public int ID {get; set;}
		
		public int TSAssetManagementID {get; set;}
		
		public int EmployeeID {get; set;}
		
		public DateTime? DateBorrow {get; set;}
		
		public DateTime? DateExpectedReturn {get; set;}
		
		public DateTime? DateActualReturn {get; set;}
		
		public decimal QuantityBorrow {get; set;}
		
		public int Status {get; set;}
		
		public string Note {get; set;}
		
		public string Reason {get; set;}
		
		public bool IsDeleted {get; set;}
		
		public bool IsApproved {get; set;}
		
		public int EmployeeApprovedID {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
	}
}
	