
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class RequestBuySaleDetailBO : BaseBO
	{
		private RequestBuySaleDetailFacade facade = RequestBuySaleDetailFacade.Instance;
		protected static RequestBuySaleDetailBO instance = new RequestBuySaleDetailBO();

		protected RequestBuySaleDetailBO()
		{
			this.baseFacade = facade;
		}

		public static RequestBuySaleDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	