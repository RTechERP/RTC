
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class HolidayBO : BaseBO
	{
		private HolidayFacade facade = HolidayFacade.Instance;
		protected static HolidayBO instance = new HolidayBO();

		protected HolidayBO()
		{
			this.baseFacade = facade;
		}

		public static HolidayBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	