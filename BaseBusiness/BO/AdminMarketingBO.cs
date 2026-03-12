
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class AdminMarketingBO : BaseBO
	{
		private AdminMarketingFacade facade = AdminMarketingFacade.Instance;
		protected static AdminMarketingBO instance = new AdminMarketingBO();

		protected AdminMarketingBO()
		{
			this.baseFacade = facade;
		}

		public static AdminMarketingBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	