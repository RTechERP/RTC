
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class AdminMarketingDetailBO : BaseBO
	{
		private AdminMarketingDetailFacade facade = AdminMarketingDetailFacade.Instance;
		protected static AdminMarketingDetailBO instance = new AdminMarketingDetailBO();

		protected AdminMarketingDetailBO()
		{
			this.baseFacade = facade;
		}

		public static AdminMarketingDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	