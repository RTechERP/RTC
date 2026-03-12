
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class RequestPaidPOBO : BaseBO
	{
		private RequestPaidPOFacade facade = RequestPaidPOFacade.Instance;
		protected static RequestPaidPOBO instance = new RequestPaidPOBO();

		protected RequestPaidPOBO()
		{
			this.baseFacade = facade;
		}

		public static RequestPaidPOBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	