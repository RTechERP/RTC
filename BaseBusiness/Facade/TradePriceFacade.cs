
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class TradePriceFacade : BaseFacade
	{
		protected static TradePriceFacade instance = new TradePriceFacade(new TradePriceModel());
		protected TradePriceFacade(TradePriceModel model) : base(model)
		{
		}
		public static TradePriceFacade Instance
		{
			get { return instance; }
		}
		protected TradePriceFacade():base() 
		{ 
		} 
	
	}
}
	