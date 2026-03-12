
using System;
namespace BMS.Model
{
	public partial class EmployeeNighShiftModel : BaseModel
	{
		public int ID {get; set;}
		
		public int IsApprovedTBP {get; set;}
		
		public int IsApprovedHR {get; set;}
		
		public int ApprovedTBP {get; set;}
		
		public int ApprovedHR {get; set;}
		
		public int EmployeeID {get; set;}
		
		public DateTime? DateRegister {get; set;}
		
		public DateTime? DateStart {get; set;}
		
		public DateTime? DateEnd {get; set;}
		
		public decimal BreaksTime {get; set;}
		
		public decimal TotalHours {get; set;}
		
		public string Location {get; set;}
		
		public string Note {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public string ReasonDeciline {get; set;}
		
		public string ReasonHREdit {get; set;}
		
	}
}
	