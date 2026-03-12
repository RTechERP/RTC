
using System;
namespace BMS.Model
{
	public partial class WorkPlanModel : BaseModel
	{
		public int ID {get; set;}
		
		public int UserID {get; set;}
		
		public string WorkContent {get; set;}
		
		public DateTime? StartDate {get; set;}
		
		public DateTime? EndDate {get; set;}
		
		public int TotalDay {get; set;}
        public int STT { get; set; }
        public string Location { get; set; }
        public int ProjectID { get; set; }
    }
}
	