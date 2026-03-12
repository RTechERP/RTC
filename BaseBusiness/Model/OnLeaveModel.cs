
using System;
namespace BMS.Model
{
	public partial class OnLeaveModel : BaseModel
	{
		public int ID {get; set;}
		
		public int UserLeave {get; set;}
		
		public int UserConfirm {get; set;}
		
		public DateTime? StartDate {get; set;}
		
		public DateTime? EndDate {get; set;}
		
		public bool Type {get; set;}
		
		public string Reason {get; set;}
		
		public bool Confirm {get; set;}
		
	}
}
	