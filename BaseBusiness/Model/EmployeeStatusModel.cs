
using System;
namespace BMS.Model
{
	public partial class EmployeeStatusModel : BaseModel
	{
		public int ID {get; set;}
		
		public string StatusCode {get; set;}
		
		public string StatusName {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
	}
}
	