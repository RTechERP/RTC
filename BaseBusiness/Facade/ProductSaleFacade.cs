
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProductSaleFacade : BaseFacade
	{
		protected static ProductSaleFacade instance = new ProductSaleFacade(new ProductSaleModel());
		protected ProductSaleFacade(ProductSaleModel model) : base(model)
		{
		}
		public static ProductSaleFacade Instance
		{
			get { return instance; }
		}
		protected ProductSaleFacade():base() 
		{ 
		} 
	
	}
}
	