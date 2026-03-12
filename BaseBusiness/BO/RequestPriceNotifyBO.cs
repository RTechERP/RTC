
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class RequestPriceNotifyBO : BaseBO
	{
		private RequestPriceNotifyFacade facade = RequestPriceNotifyFacade.Instance;
		protected static RequestPriceNotifyBO instance = new RequestPriceNotifyBO();

		protected RequestPriceNotifyBO()
		{
			this.baseFacade = facade;
		}

		public static RequestPriceNotifyBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	