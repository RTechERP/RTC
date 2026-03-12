
using System;
namespace BMS.Model
{
	public partial class EmployeeSalaryAdvanceModel : BaseModel
	{
		public int ID {get; set;}
		
		public int EmployeeID {get; set;}
		
		public decimal Money {get; set;}
		
		public DateTime? DateRequest {get; set;}
		
		public bool IsApproved_TP {get; set;}
		
		public bool IsApproved_HR {get; set;}
		
		public bool IsApproved_KT {get; set;}
		
		public int ApprovedTP {get; set;}
		
		public int ApprovedHR {get; set;}
		
		public int ApprovedKT {get; set;}
		
		public bool IsPayed {get; set;}
		
		public string Reason {get; set;}
		
		public DateTime? DatePayed {get; set;}
		
		public string Note {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
	}
}
	