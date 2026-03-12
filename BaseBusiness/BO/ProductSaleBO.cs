
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProductSaleBO : BaseBO
	{
		private ProductSaleFacade facade = ProductSaleFacade.Instance;
		protected static ProductSaleBO instance = new ProductSaleBO();

		protected ProductSaleBO()
		{
			this.baseFacade = facade;
		}

		public static ProductSaleBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	