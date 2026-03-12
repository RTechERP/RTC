
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class RequestBuyDetailBO : BaseBO
	{
		private RequestBuyDetailFacade facade = RequestBuyDetailFacade.Instance;
		protected static RequestBuyDetailBO instance = new RequestBuyDetailBO();

		protected RequestBuyDetailBO()
		{
			this.baseFacade = facade;
		}

		public static RequestBuyDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	