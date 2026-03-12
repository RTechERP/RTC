
using System;
namespace BMS.Model
{
	public partial class EmployeeOvertimeModel : BaseModel
	{
		public int ID {get; set;}
		
		public bool IsApproved {get; set;}
		
		public int EmployeeID {get; set;}
		
		public int ApprovedID {get; set;}
		
		public DateTime? DateRegister {get; set;}
		
		public int Location {get; set;}
		
		public int TypeID {get; set;}
		
		public DateTime? TimeStart {get; set;}
		
		public DateTime? EndTime {get; set;}
		
		public decimal TimeReality {get; set;}
		
		public decimal TotalTime {get; set;}
		
		public decimal CostOvertime {get; set;}
		
		public string Note {get; set;}
		
		public bool Overnight {get; set;}
		
		public decimal CostOvernight {get; set;}
		
		public string Reason {get; set;}
		
		public int DecilineApprove {get; set;}
		
		public int ApprovedHR {get; set;}
		
		public bool IsApprovedHR {get; set;}
		
		public string ReasonDeciline {get; set;}
		
		public int ProjectID {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public string ReasonHREdit {get; set;}
        public bool? IsApprovedBGD { get; set; } // bit, null
        public int? ApprovedBGDID { get; set; } // int, null
        public DateTime? DateApprovedBGD { get; set; } // datetime, null
        public bool IsSeniorApproved { get; set; } // datetime, null
        public int ApprovedSeniorID { get; set; } // datetime, null
        public DateTime? DateApprovedSenitor { get; set; } // datetime, null
    }
}
	