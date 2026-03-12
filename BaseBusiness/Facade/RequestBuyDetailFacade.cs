
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class RequestBuyDetailFacade : BaseFacade
	{
		protected static RequestBuyDetailFacade instance = new RequestBuyDetailFacade(new RequestBuyDetailModel());
		protected RequestBuyDetailFacade(RequestBuyDetailModel model) : base(model)
		{
		}
		public static RequestBuyDetailFacade Instance
		{
			get { return instance; }
		}
		protected RequestBuyDetailFacade():base() 
		{ 
		} 
	
	}
}
	