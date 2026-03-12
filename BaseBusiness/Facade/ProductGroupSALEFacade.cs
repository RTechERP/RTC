
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProductGroupSALEFacade : BaseFacade
	{
		protected static ProductGroupSALEFacade instance = new ProductGroupSALEFacade(new ProductGroupSALEModel());
		protected ProductGroupSALEFacade(ProductGroupSALEModel model) : base(model)
		{
		}
		public static ProductGroupSALEFacade Instance
		{
			get { return instance; }
		}
		protected ProductGroupSALEFacade():base() 
		{ 
		} 
	
	}
}
	