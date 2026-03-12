
using System;
namespace BMS.Model
{
	public partial class WorkPlanDetailModel : BaseModel
	{
		public int ID {get; set;}
		
		public int UserID {get; set;}
		
		public string WorkContent {get; set;}
		
		public DateTime? DateDay {get; set;}
		
		public string Location {get; set;}
		
		public int ProjectID {get; set;}
		
		public int STT {get; set;}
		
		public int WorkPlanID {get; set;}
		
	}
}
	