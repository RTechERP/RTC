
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProductivityIndexBO : BaseBO
	{
		private ProductivityIndexFacade facade = ProductivityIndexFacade.Instance;
		protected static ProductivityIndexBO instance = new ProductivityIndexBO();

		protected ProductivityIndexBO()
		{
			this.baseFacade = facade;
		}

		public static ProductivityIndexBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	