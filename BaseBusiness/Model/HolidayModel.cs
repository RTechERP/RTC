
using System;
namespace BMS.Model
{
	public partial class HolidayModel : BaseModel
	{
		public int ID {get; set;}
		
		public DateTime HolidayDate {get; set;}
		
		public int HolidayYear {get; set;}
		
		public int HolidayMonth {get; set;}
		
		public int HolidayDay {get; set;}
		
		public string DayValue {get; set;}
		
		public string HolidayName {get; set;}
		
		public string HolidayCode {get; set;}
		
		public string Note {get; set;}
        public int TypeHoliday { get; set; }

    }
}
	