
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class RequestBuyRTCBO : BaseBO
	{
		private RequestBuyRTCFacade facade = RequestBuyRTCFacade.Instance;
		protected static RequestBuyRTCBO instance = new RequestBuyRTCBO();

		protected RequestBuyRTCBO()
		{
			this.baseFacade = facade;
		}

		public static RequestBuyRTCBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	