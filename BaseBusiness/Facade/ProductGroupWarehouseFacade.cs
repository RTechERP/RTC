
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProductGroupWarehouseFacade : BaseFacade
	{
		protected static ProductGroupWarehouseFacade instance = new ProductGroupWarehouseFacade(new ProductGroupWarehouseModel());
		protected ProductGroupWarehouseFacade(ProductGroupWarehouseModel model) : base(model)
		{
		}
		public static ProductGroupWarehouseFacade Instance
		{
			get { return instance; }
		}
		protected ProductGroupWarehouseFacade():base() 
		{ 
		} 
	
	}
}
	