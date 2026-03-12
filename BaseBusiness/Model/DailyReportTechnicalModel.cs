
using System;
namespace BMS.Model
{
	public partial class DailyReportTechnicalModel : BaseModel
	{
		public int ID {get; set;}
		
		public int UserReport {get; set;}
		
		public DateTime DateReport {get; set;}
		
		public int ProjectID {get; set;}
		
		public string Content {get; set;}
		
		public string Results {get; set;}
		
		public string Problem {get; set;}
		
		public string ProblemSolve {get; set;}
		
		public string PlanNextDay {get; set;}
		
		public string Note {get; set;}
		
		public bool Confirm {get; set;}
		
		public string Backlog {get; set;}
		
		public int DeleteFlag {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public int Type {get; set;}
		
		public int ReportLate {get; set;}
		
	}
}
	