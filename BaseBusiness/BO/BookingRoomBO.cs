
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class BookingRoomBO : BaseBO
	{
		private BookingRoomFacade facade = BookingRoomFacade.Instance;
		protected static BookingRoomBO instance = new BookingRoomBO();

		protected BookingRoomBO()
		{
			this.baseFacade = facade;
		}

		public static BookingRoomBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	