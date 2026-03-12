
using System;
namespace BMS.Model
{
	public partial class DailyReportSaleAdminModel : BaseModel
	{
		public int ID {get; set;}
		
		public int EmployeeID {get; set;}
		
		public int ReportTypeID {get; set;}
		
		public DateTime? DateReport {get; set;}
		
		public string ReportContent {get; set;}
		
		public int CustomerID {get; set;}
		
		public int EmployeeRequestID {get; set;}
		
		public string Result {get; set;}
		
		public string Problem {get; set;}
		
		public string ProblemSolve {get; set;}
		
		public string PlanNextDay {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		public int ProjectID { get; set;}
		
	}
}
	