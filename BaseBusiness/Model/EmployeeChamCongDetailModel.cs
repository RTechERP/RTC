
using System;
namespace BMS.Model
{
	public partial class EmployeeChamCongDetailModel : BaseModel
	{
		public int ID {get; set;}
		
		public int MasterID {get; set;}
		
		public int EmployeeID {get; set;}
		
		public string IDChamCong {get; set;}
		
		public DateTime? DayFinger {get; set;}
		
		public decimal WorkTime {get; set;}
		
		public int NoFinger {get; set;}
		
		public int TypeBussiness {get; set;}
		
		public decimal CostBussiness {get; set;}
		
		public int TypeOvertime {get; set;}
		
		public decimal TotalTimeOT {get; set;}
		
		public int Overnight {get; set;}
		
		public int OnLeaveType {get; set;}
		
		public decimal OnLeaveDay {get; set;}
		
		public decimal CostWorkEarly {get; set;}
		
		public decimal TotalDayWFH {get; set;}
		
		public string TotalDayText {get; set;}
		
		public decimal TotalDay {get; set;}
		
		public string FoodOrderText {get; set;}
		
		public int FoodOrderUse {get; set;}
		
		public int IsEarly {get; set;}
		
		public int IsLate {get; set;}
		
	}
}
	