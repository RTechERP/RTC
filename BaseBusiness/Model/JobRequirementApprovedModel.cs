
using System;
namespace BMS.Model
{
	public partial class JobRequirementApprovedModel : BaseModel
	{
		public int ID {get; set;}
		
		public int JobRequirementID {get; set;}
		
		public int Step {get; set;}
		
		public string StepName {get; set;}
		
		public int IsApproved {get; set;}
		
		public DateTime? DateApproved {get; set;}
		
		public int ApprovedID {get; set;}
		
		public int ApprovedActualID {get; set;}
		
		public string ReasonCancel {get; set;}
		
		public string ContentLog {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
	}
}
	