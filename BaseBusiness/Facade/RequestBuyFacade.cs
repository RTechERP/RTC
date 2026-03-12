
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class RequestBuyFacade : BaseFacade
	{
		protected static RequestBuyFacade instance = new RequestBuyFacade(new RequestBuyModel());
		protected RequestBuyFacade(RequestBuyModel model) : base(model)
		{
		}
		public static RequestBuyFacade Instance
		{
			get { return instance; }
		}
		protected RequestBuyFacade():base() 
		{ 
		} 
	
	}
}
	