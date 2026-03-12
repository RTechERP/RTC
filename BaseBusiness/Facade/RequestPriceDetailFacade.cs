
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class RequestPriceDetailFacade : BaseFacade
	{
		protected static RequestPriceDetailFacade instance = new RequestPriceDetailFacade(new RequestPriceDetailModel());
		protected RequestPriceDetailFacade(RequestPriceDetailModel model) : base(model)
		{
		}
		public static RequestPriceDetailFacade Instance
		{
			get { return instance; }
		}
		protected RequestPriceDetailFacade():base() 
		{ 
		} 
	
	}
}
	