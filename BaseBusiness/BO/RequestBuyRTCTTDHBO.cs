
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class RequestBuyRTCTTDHBO : BaseBO
	{
		private RequestBuyRTCTTDHFacade facade = RequestBuyRTCTTDHFacade.Instance;
		protected static RequestBuyRTCTTDHBO instance = new RequestBuyRTCTTDHBO();

		protected RequestBuyRTCTTDHBO()
		{
			this.baseFacade = facade;
		}

		public static RequestBuyRTCTTDHBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	