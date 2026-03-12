
using System;
namespace BMS.Model
{
	public partial class EmployeeOnLeaveModel : BaseModel
	{
		public int ID {get; set;}
		
		public int EmployeeID {get; set;}
		
		public int ApprovedTP {get; set;}
		
		public int ApprovedHR {get; set;}
		
		public int TimeOnLeave {get; set;}
		
		public DateTime? StartDate {get; set;}
		
		public DateTime? EndDate {get; set;}
		
		public decimal TotalTime {get; set;}
		
		public int Type {get; set;}
		
		public int TypeIsReal {get; set;}
		
		public decimal TotalDay {get; set;}
		
		public string Reason {get; set;}
		
		public string Note {get; set;}
		
		public bool IsApprovedTP {get; set;}
		
		public bool IsApprovedHR {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public bool IsCancelTP {get; set;}
		
		public bool IsCancelHR {get; set;}
		
		public bool IsCancelRegister {get; set;}
		
		public int DecilineApprove {get; set;}
		
		public string ReasonCancel {get; set;}
		
		public DateTime? DateCancel {get; set;}
		
		public bool DeleteFlag {get; set;}
		
		public string ReasonDeciline {get; set;}
		
		public string ReasonHREdit {get; set;}
        public bool? IsApprovedBGD { get; set; } // bit, null
        public int? ApprovedBGDID { get; set; } // int, null
        public DateTime? DateApprovedBGD { get; set; } // datetime, null

    }
}
	