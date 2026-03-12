
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class FoodOrderFacade : BaseFacade
	{
		protected static FoodOrderFacade instance = new FoodOrderFacade(new FoodOrderModel());
		protected FoodOrderFacade(FoodOrderModel model) : base(model)
		{
		}
		public static FoodOrderFacade Instance
		{
			get { return instance; }
		}
		protected FoodOrderFacade():base() 
		{ 
		} 
	
	}
}
	