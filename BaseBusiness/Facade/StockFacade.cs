
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class StockFacade : BaseFacade
	{
		protected static StockFacade instance = new StockFacade(new StockModel());
		protected StockFacade(StockModel model) : base(model)
		{
		}
		public static StockFacade Instance
		{
			get { return instance; }
		}
		protected StockFacade():base() 
		{ 
		} 
	
	}
}
	