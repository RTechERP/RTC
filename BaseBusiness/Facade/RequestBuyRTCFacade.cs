
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class RequestBuyRTCFacade : BaseFacade
	{
		protected static RequestBuyRTCFacade instance = new RequestBuyRTCFacade(new RequestBuyRTCModel());
		protected RequestBuyRTCFacade(RequestBuyRTCModel model) : base(model)
		{
		}
		public static RequestBuyRTCFacade Instance
		{
			get { return instance; }
		}
		protected RequestBuyRTCFacade():base() 
		{ 
		} 
	
	}
}
	