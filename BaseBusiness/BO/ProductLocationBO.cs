
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProductLocationBO : BaseBO
	{
		private ProductLocationFacade facade = ProductLocationFacade.Instance;
		protected static ProductLocationBO instance = new ProductLocationBO();

		protected ProductLocationBO()
		{
			this.baseFacade = facade;
		}

		public static ProductLocationBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	