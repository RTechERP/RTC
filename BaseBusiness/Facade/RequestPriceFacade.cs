
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class RequestPriceFacade : BaseFacade
	{
		protected static RequestPriceFacade instance = new RequestPriceFacade(new RequestPriceModel());
		protected RequestPriceFacade(RequestPriceModel model) : base(model)
		{
		}
		public static RequestPriceFacade Instance
		{
			get { return instance; }
		}
		protected RequestPriceFacade():base() 
		{ 
		} 
	
	}
}
	