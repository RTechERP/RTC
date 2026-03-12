
using System;
namespace BMS.Model
{
	public partial class EmployeePOContactModel : BaseModel
	{
		public int ID {get; set;}
		
		public int EmployeeID {get; set;}
		
		public string Phone {get; set;}
		
		public string Email {get; set;}
		
		public int ComCode {get; set;}
		
	}
}
	