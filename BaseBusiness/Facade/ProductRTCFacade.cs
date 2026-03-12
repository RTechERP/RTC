
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProductRTCFacade : BaseFacade
	{
		protected static ProductRTCFacade instance = new ProductRTCFacade(new ProductRTCModel());
		protected ProductRTCFacade(ProductRTCModel model) : base(model)
		{
		}
		public static ProductRTCFacade Instance
		{
			get { return instance; }
		}
		protected ProductRTCFacade():base() 
		{ 
		} 
	
	}
}
	