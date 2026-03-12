
using System;
namespace BMS.Model
{
	public partial class EmployeePayrollModel : BaseModel
	{
		public int ID {get; set;}
		
		public string Name {get; set;}
		
		public int _Month {get; set;}
		
		public int _Year {get; set;}
		
		public string Note {get; set;}
		
		public bool IsApproved {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
	}
}
	