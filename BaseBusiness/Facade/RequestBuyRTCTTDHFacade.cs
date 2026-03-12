
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class RequestBuyRTCTTDHFacade : BaseFacade
	{
		protected static RequestBuyRTCTTDHFacade instance = new RequestBuyRTCTTDHFacade(new RequestBuyRTCTTDHModel());
		protected RequestBuyRTCTTDHFacade(RequestBuyRTCTTDHModel model) : base(model)
		{
		}
		public static RequestBuyRTCTTDHFacade Instance
		{
			get { return instance; }
		}
		protected RequestBuyRTCTTDHFacade():base() 
		{ 
		} 
	
	}
}
	