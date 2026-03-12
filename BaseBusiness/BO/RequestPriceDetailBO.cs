
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class RequestPriceDetailBO : BaseBO
	{
		private RequestPriceDetailFacade facade = RequestPriceDetailFacade.Instance;
		protected static RequestPriceDetailBO instance = new RequestPriceDetailBO();

		protected RequestPriceDetailBO()
		{
			this.baseFacade = facade;
		}

		public static RequestPriceDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	