
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class AdminMarketingDetailFacade : BaseFacade
	{
		protected static AdminMarketingDetailFacade instance = new AdminMarketingDetailFacade(new AdminMarketingDetailModel());
		protected AdminMarketingDetailFacade(AdminMarketingDetailModel model) : base(model)
		{
		}
		public static AdminMarketingDetailFacade Instance
		{
			get { return instance; }
		}
		protected AdminMarketingDetailFacade():base() 
		{ 
		} 
	
	}
}
	