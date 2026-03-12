
using System;
namespace BMS.Model
{
	public partial class NotifyModel : BaseModel
	{
		public int ID {get; set;}
		
		public string Title {get; set;}
		
		public string Text {get; set;}
		
		public int EmployeeID {get; set;}
		
		public int DepartmentID {get; set;}
		
		public int NotifyStatus {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
	}
}
	