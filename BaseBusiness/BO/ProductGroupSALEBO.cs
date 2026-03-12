
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProductGroupSALEBO : BaseBO
	{
		private ProductGroupSALEFacade facade = ProductGroupSALEFacade.Instance;
		protected static ProductGroupSALEBO instance = new ProductGroupSALEBO();

		protected ProductGroupSALEBO()
		{
			this.baseFacade = facade;
		}

		public static ProductGroupSALEBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	