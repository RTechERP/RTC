
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class ProductFilmBO : BaseBO
	{
		private ProductFilmFacade facade = ProductFilmFacade.Instance;
		protected static ProductFilmBO instance = new ProductFilmBO();

		protected ProductFilmBO()
		{
			this.baseFacade = facade;
		}

		public static ProductFilmBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	