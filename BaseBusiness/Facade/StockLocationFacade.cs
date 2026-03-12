
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class StockLocationFacade : BaseFacade
	{
		protected static StockLocationFacade instance = new StockLocationFacade(new StockLocationModel());
		protected StockLocationFacade(StockLocationModel model) : base(model)
		{
		}
		public static StockLocationFacade Instance
		{
			get { return instance; }
		}
		protected StockLocationFacade():base() 
		{ 
		} 
	
	}
}
	