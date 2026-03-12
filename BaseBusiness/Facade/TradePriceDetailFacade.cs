
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class TradePriceDetailFacade : BaseFacade
	{
		protected static TradePriceDetailFacade instance = new TradePriceDetailFacade(new TradePriceDetailModel());
		protected TradePriceDetailFacade(TradePriceDetailModel model) : base(model)
		{
		}
		public static TradePriceDetailFacade Instance
		{
			get { return instance; }
		}
		protected TradePriceDetailFacade():base() 
		{ 
		} 
	
	}
}
	