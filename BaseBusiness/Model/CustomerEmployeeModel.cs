
using System;
namespace BMS.Model
{
	public partial class CustomerEmployeeModel : BaseModel
	{
		public int ID {get; set;}
		
		public int CustomerID {get; set;}
		
		public int EmployeeID {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
	}
}
	