
using System;
namespace BMS.Model
{
	public partial class EmployeeBussinessModel : BaseModel
	{
		public int ID {get; set;}
		
		public int EmployeeID {get; set;}
		
		public bool IsApproved {get; set;}
		
		public int ApprovedID {get; set;}
		
		public DateTime? DayBussiness {get; set;}
		
		public int TypeBusiness {get; set;}
		
		public string Location {get; set;}
		
		public int VehicleID {get; set;}
		
		public decimal CostVehicle {get; set;}
		
		public decimal CostBussiness {get; set;}
		
		public decimal TotalMoney {get; set;}
		
		public bool NotChekIn {get; set;}
		
		public string Note {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public bool Overnight {get; set;}
		
		public decimal CostOvernight {get; set;}
		
		public bool WorkEarly {get; set;}
		
		public decimal CostWorkEarly {get; set;}
		
		public int DecilineApprove {get; set;}
		
		public int ApprovedHR {get; set;}
		
		public bool IsApprovedHR {get; set;}
		
		public string ReasonDeciline {get; set;}
		
		public int OvernightType {get; set;}
		
		public string ReasonHREdit {get; set;}
        public bool? IsApprovedBGD { get; set; } // bit, null
        public int? ApprovedBGDID { get; set; } // int, null
        public DateTime? DateApprovedBGD { get; set; } // datetime, null

    }
}
	