
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProductRTCBO : BaseBO
	{
		private ProductRTCFacade facade = ProductRTCFacade.Instance;
		protected static ProductRTCBO instance = new ProductRTCBO();

		protected ProductRTCBO()
		{
			this.baseFacade = facade;
		}

		public static ProductRTCBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	