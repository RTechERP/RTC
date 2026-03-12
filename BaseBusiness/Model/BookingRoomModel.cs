
using System;
namespace BMS.Model
{
	public partial class BookingRoomModel : BaseModel
	{
		public int ID {get; set;}
		
		public int MeetingRoomID {get; set;}
		
		public DateTime? DateRegister {get; set;}
		
		public int EmployeeID {get; set;}
		
		public DateTime? StartTime {get; set;}
		
		public DateTime? EndTime {get; set;}
		
		public string Content {get; set;}
		public int IsApproved { get; set;}
		
		public string CreatedBy {get; set;}
		
		public DateTime? CreatedDate {get; set;}
		
		public string UpdatedBy {get; set;}
		
		public DateTime? UpdatedDate {get; set;}
		
	}
}
	