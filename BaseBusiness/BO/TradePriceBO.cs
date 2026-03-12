
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class TradePriceBO : BaseBO
	{
		private TradePriceFacade facade = TradePriceFacade.Instance;
		protected static TradePriceBO instance = new TradePriceBO();

		protected TradePriceBO()
		{
			this.baseFacade = facade;
		}

		public static TradePriceBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	