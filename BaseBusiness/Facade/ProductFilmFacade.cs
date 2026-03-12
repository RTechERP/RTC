
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProductFilmFacade : BaseFacade
	{
		protected static ProductFilmFacade instance = new ProductFilmFacade(new ProductFilmModel());
		protected ProductFilmFacade(ProductFilmModel model) : base(model)
		{
		}
		public static ProductFilmFacade Instance
		{
			get { return instance; }
		}
		protected ProductFilmFacade():base() 
		{ 
		} 
	
	}
}
	