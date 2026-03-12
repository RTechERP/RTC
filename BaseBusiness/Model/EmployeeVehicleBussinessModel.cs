
using System;
namespace BMS.Model
{
	public partial class EmployeeVehicleBussinessModel : BaseModel
	{
		public int ID {get; set;}
		
		public string VehicleCode {get; set;}
		
		public string VehicleName {get; set;}
		
		public decimal Cost {get; set;}
		
		public bool EditCost {get; set;}
		
	}
}
	