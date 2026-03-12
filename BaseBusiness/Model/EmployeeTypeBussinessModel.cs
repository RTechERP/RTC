
using System;
namespace BMS.Model
{
	public partial class EmployeeTypeBussinessModel : BaseModel
	{
		public int ID {get; set;}
		
		public string TypeCode {get; set;}
		
		public string TypeName {get; set;}
		
		public decimal Cost {get; set;}
		
	}
}
	