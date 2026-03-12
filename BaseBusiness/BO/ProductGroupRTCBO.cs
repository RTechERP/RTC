
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProductGroupRTCBO : BaseBO
	{
		private ProductGroupRTCFacade facade = ProductGroupRTCFacade.Instance;
		protected static ProductGroupRTCBO instance = new ProductGroupRTCBO();

		protected ProductGroupRTCBO()
		{
			this.baseFacade = facade;
		}

		public static ProductGroupRTCBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	