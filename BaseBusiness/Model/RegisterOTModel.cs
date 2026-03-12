
using System;
namespace BMS.Model
{
	public partial class RegisterOTModel : BaseModel
	{
		public int ID {get; set;}
		
		public int UserOT {get; set;}
		
		public bool TypeOT {get; set;}
		
		public bool TypeGOB {get; set;}
		
		public DateTime? StartDate {get; set;}
		
		public DateTime? EndDate {get; set;}
		
		public decimal SumTime {get; set;}
		
		public decimal CostsOT {get; set;}
		
		public int CustomerID {get; set;}
		
		public bool NotCheckInOffice {get; set;}
		
		public int Vehicle {get; set;}
		
		public decimal CostVehicle {get; set;}
		
		public bool Overnight {get; set;}
		
		public decimal CostON {get; set;}
		
		public bool Confirm {get; set;}
		
	}
}
	