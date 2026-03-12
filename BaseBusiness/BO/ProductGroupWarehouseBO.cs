
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProductGroupWarehouseBO : BaseBO
	{
		private ProductGroupWarehouseFacade facade = ProductGroupWarehouseFacade.Instance;
		protected static ProductGroupWarehouseBO instance = new ProductGroupWarehouseBO();

		protected ProductGroupWarehouseBO()
		{
			this.baseFacade = facade;
		}

		public static ProductGroupWarehouseBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	