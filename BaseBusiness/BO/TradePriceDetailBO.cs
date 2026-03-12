
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class TradePriceDetailBO : BaseBO
	{
		private TradePriceDetailFacade facade = TradePriceDetailFacade.Instance;
		protected static TradePriceDetailBO instance = new TradePriceDetailBO();

		protected TradePriceDetailBO()
		{
			this.baseFacade = facade;
		}

		public static TradePriceDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	