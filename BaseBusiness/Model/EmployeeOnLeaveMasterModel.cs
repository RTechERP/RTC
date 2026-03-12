
using System;
namespace BMS.Model
{
	public partial class EmployeeOnLeaveMasterModel : BaseModel
	{
		public int ID {get; set;}
		
		public int EmployeeID {get; set;}
		
		public decimal TotalDayInYear {get; set;}
		
		public decimal TotalDayOnLeave {get; set;}
		
		public decimal TotalDayNoOnLeave {get; set;}
		
		public decimal TotalDayRemain {get; set;}
		
		public int YearOnleave { get; set;}
		
	}
}
	