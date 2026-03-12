
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class RequestBuyBO : BaseBO
	{
		private RequestBuyFacade facade = RequestBuyFacade.Instance;
		protected static RequestBuyBO instance = new RequestBuyBO();

		protected RequestBuyBO()
		{
			this.baseFacade = facade;
		}

		public static RequestBuyBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	