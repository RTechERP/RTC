
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class RequestBuySaleDetailFacade : BaseFacade
	{
		protected static RequestBuySaleDetailFacade instance = new RequestBuySaleDetailFacade(new RequestBuySaleDetailModel());
		protected RequestBuySaleDetailFacade(RequestBuySaleDetailModel model) : base(model)
		{
		}
		public static RequestBuySaleDetailFacade Instance
		{
			get { return instance; }
		}
		protected RequestBuySaleDetailFacade():base() 
		{ 
		} 
	
	}
}
	