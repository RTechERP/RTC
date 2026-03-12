
using System;
namespace BMS.Model
{
	public partial class RegisterIdeaScoreModel : BaseModel
	{
		public int ID {get; set;}
		
		public int RegisterIdeaID {get; set;}
		
		public int DepartmentID {get; set;}
		
		public decimal Score {get; set;}
		
		public bool IsTBP {get; set;}
		
		public bool IsBGD {get; set;}
		
		public bool IsApprovedTBP {get; set;}
		
		public DateTime? DateApprovedTBP {get; set;}
		
		public int ApprovedTBPID {get; set;}
		
		public bool IsApproved {get; set;}
		
		public DateTime? DateApproved {get; set;}
		
		public int ApprovedID {get; set;}
		
		public bool IsApprovedBGD {get; set;}
		
		public DateTime? DateApprovedBGD {get; set;}
		
		public int ApprovedBGDID {get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
	}
}
	