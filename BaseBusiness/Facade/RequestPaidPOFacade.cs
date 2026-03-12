
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class RequestPaidPOFacade : BaseFacade
	{
		protected static RequestPaidPOFacade instance = new RequestPaidPOFacade(new RequestPaidPOModel());
		protected RequestPaidPOFacade(RequestPaidPOModel model) : base(model)
		{
		}
		public static RequestPaidPOFacade Instance
		{
			get { return instance; }
		}
		protected RequestPaidPOFacade():base() 
		{ 
		} 
	
	}
}
	