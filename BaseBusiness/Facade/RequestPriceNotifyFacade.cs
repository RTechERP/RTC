
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class RequestPriceNotifyFacade : BaseFacade
	{
		protected static RequestPriceNotifyFacade instance = new RequestPriceNotifyFacade(new RequestPriceNotifyModel());
		protected RequestPriceNotifyFacade(RequestPriceNotifyModel model) : base(model)
		{
		}
		public static RequestPriceNotifyFacade Instance
		{
			get { return instance; }
		}
		protected RequestPriceNotifyFacade():base() 
		{ 
		} 
	
	}
}
	