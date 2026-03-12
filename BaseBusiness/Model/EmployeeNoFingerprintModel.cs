
using System;
namespace BMS.Model
{
	public partial class EmployeeNoFingerprintModel : BaseModel
	{
		public int ID {get; set;}
		
		public int EmployeeID {get; set;}
		
		public int ApprovedTP {get; set;}
		
		public DateTime? DayWork {get; set;}
		
		public bool IsApprovedTP {get; set;}
		
		public string Note {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public int ApprovedHR {get; set;}
		
		public bool IsApprovedHR {get; set;}
		
		public int Type {get; set;}
		
		public int DecilineApprove {get; set;}
		
		public string ReasonDeciline {get; set;}
		
		public string ReasonHREdit {get; set;}
		
	}
}
	