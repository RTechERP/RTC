
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProductLocationFacade : BaseFacade
	{
		protected static ProductLocationFacade instance = new ProductLocationFacade(new ProductLocationModel());
		protected ProductLocationFacade(ProductLocationModel model) : base(model)
		{
		}
		public static ProductLocationFacade Instance
		{
			get { return instance; }
		}
		protected ProductLocationFacade():base() 
		{ 
		} 
	
	}
}
	