
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class BookingRoomFacade : BaseFacade
	{
		protected static BookingRoomFacade instance = new BookingRoomFacade(new BookingRoomModel());
		protected BookingRoomFacade(BookingRoomModel model) : base(model)
		{
		}
		public static BookingRoomFacade Instance
		{
			get { return instance; }
		}
		protected BookingRoomFacade():base() 
		{ 
		} 
	
	}
}
	