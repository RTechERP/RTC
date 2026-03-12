
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class HolidayFacade : BaseFacade
	{
		protected static HolidayFacade instance = new HolidayFacade(new HolidayModel());
		protected HolidayFacade(HolidayModel model) : base(model)
		{
		}
		public static HolidayFacade Instance
		{
			get { return instance; }
		}
		protected HolidayFacade():base() 
		{ 
		} 
	
	}
}
	