
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class ProductivityIndexFacade : BaseFacade
	{
		protected static ProductivityIndexFacade instance = new ProductivityIndexFacade(new ProductivityIndexModel());
		protected ProductivityIndexFacade(ProductivityIndexModel model) : base(model)
		{
		}
		public static ProductivityIndexFacade Instance
		{
			get { return instance; }
		}
		protected ProductivityIndexFacade():base() 
		{ 
		} 
	
	}
}
	