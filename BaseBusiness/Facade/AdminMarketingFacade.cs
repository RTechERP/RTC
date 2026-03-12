
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class AdminMarketingFacade : BaseFacade
	{
		protected static AdminMarketingFacade instance = new AdminMarketingFacade(new AdminMarketingModel());
		protected AdminMarketingFacade(AdminMarketingModel model) : base(model)
		{
		}
		public static AdminMarketingFacade Instance
		{
			get { return instance; }
		}
		protected AdminMarketingFacade():base() 
		{ 
		} 
	
	}
}
	