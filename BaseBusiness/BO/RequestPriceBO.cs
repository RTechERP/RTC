
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class RequestPriceBO : BaseBO
	{
		private RequestPriceFacade facade = RequestPriceFacade.Instance;
		protected static RequestPriceBO instance = new RequestPriceBO();

		protected RequestPriceBO()
		{
			this.baseFacade = facade;
		}

		public static RequestPriceBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	