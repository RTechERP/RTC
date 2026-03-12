
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class RequestBuySaleFacade : BaseFacade
	{
		protected static RequestBuySaleFacade instance = new RequestBuySaleFacade(new RequestBuySaleModel());
		protected RequestBuySaleFacade(RequestBuySaleModel model) : base(model)
		{
		}
		public static RequestBuySaleFacade Instance
		{
			get { return instance; }
		}
		protected RequestBuySaleFacade():base() 
		{ 
		} 
	
	}
}
	