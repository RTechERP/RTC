
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProductGroupRTCFacade : BaseFacade
	{
		protected static ProductGroupRTCFacade instance = new ProductGroupRTCFacade(new ProductGroupRTCModel());
		protected ProductGroupRTCFacade(ProductGroupRTCModel model) : base(model)
		{
		}
		public static ProductGroupRTCFacade Instance
		{
			get { return instance; }
		}
		protected ProductGroupRTCFacade():base() 
		{ 
		} 
	
	}
}
	