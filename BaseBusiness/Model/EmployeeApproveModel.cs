
using System;
namespace BMS.Model
{
	public partial class EmployeeApproveModel : BaseModel
	{
		public int ID {get; set;}
		
		public int EmployeeID {get; set;}
		
		public string Code {get; set;}
		
		public string FullName {get; set;}
		
		public int Type {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
	}
}
	