
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class DiscountOrderFacade : BaseFacade
	{
		protected static DiscountOrderFacade instance = new DiscountOrderFacade(new DiscountOrderModel());
		protected DiscountOrderFacade(DiscountOrderModel model) : base(model)
		{
		}
		public static DiscountOrderFacade Instance
		{
			get { return instance; }
		}
		protected DiscountOrderFacade():base() 
		{ 
		} 
	
	}
}
	