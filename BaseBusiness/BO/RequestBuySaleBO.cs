
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class RequestBuySaleBO : BaseBO
	{
		private RequestBuySaleFacade facade = RequestBuySaleFacade.Instance;
		protected static RequestBuySaleBO instance = new RequestBuySaleBO();

		protected RequestBuySaleBO()
		{
			this.baseFacade = facade;
		}

		public static RequestBuySaleBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	