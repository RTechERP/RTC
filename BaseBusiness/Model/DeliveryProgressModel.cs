
using System;
namespace BMS.Model
{
	public partial class DeliveryProgressModel : BaseModel
	{
		public int ID {get; set;}
		
		public int Total {get; set;}
		
		public int OnSchedule {get; set;}
		
		public int SlowProgress {get; set;}
		
		public int SlowProgressAccepted {get; set;}
		
		public decimal Ratio {get; set;}
		
		public int Month {get; set;}
		
		public string WeekText {get; set;}
		
		public int Year {get; set;}
		
		public int UserID {get; set;}
		
		public int Week {get; set;}
		
	}
}
	