
using System;
namespace BMS.Model
{
	public partial class ProjectItemModel : BaseModel
	{
		public int ID {get; set;}
		
		public int Status {get; set;}
		
		public string STT {get; set;}
		
		public int UserID {get; set;}
		
		public int ProjectID {get; set;}
		
		public string Mission {get; set;}
		
		public DateTime? PlanStartDate {get; set;}
		
		public DateTime? PlanEndDate {get; set;}
		
		public DateTime? ActualStartDate {get; set;}
		
		public DateTime? ActualEndDate {get; set;}
		
		public string Note {get; set;}
		
		public decimal TotalDayPlan {get; set;}
		
		public decimal PercentItem {get; set;}
		
		public int ParentID {get; set;}
		
		public decimal TotalDayActual {get; set;}
		
		public int ItemLate {get; set;}
		
		public decimal TimeSpan {get; set;}
		
		public int TypeProjectItem {get; set;}
		
		public decimal PercentageActual {get; set;}
		
		public int EmployeeIDRequest {get; set;}
		
		public DateTime? UpdatedDateActual {get; set;}
		
		public int IsApproved {get; set;}
		
		public string Code {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public bool IsUpdateLate {get; set;}
		
		public string ReasonLate {get; set;}
		
		public DateTime? UpdatedDateReasonLate {get; set;}
		
		public bool IsApprovedLate {get; set;}
		public int EmployeeRequestID { get; set;}
		public string EmployeeRequestName { get; set;}
		
	}
}
	