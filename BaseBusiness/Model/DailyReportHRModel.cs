
using System;
namespace BMS.Model
{
	public partial class DailyReportHRModel : BaseModel
	{
		public int ID {get; set;}
		
		public int EmployeeID {get; set;}
		
		public DateTime? DateReport {get; set;}
		
		public int FilmManagementDetailID {get; set;}
		
		public int Quantity {get; set;}
		
		public decimal TimeActual {get; set;}
		
		public decimal PerformanceActual {get; set;}
		
		public decimal Percentage {get; set;}
		
		public decimal KmNumber {get; set;}
		
		public int TotalLate {get; set;}
		
		public decimal TotalTimeLate {get; set;}
		
		public string ReasonLate {get; set;}
		
		public string StatusVehicle {get; set;}
		
		public string Propose {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
	}
}
	