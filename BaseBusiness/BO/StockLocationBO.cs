
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class StockLocationBO : BaseBO
	{
		private StockLocationFacade facade = StockLocationFacade.Instance;
		protected static StockLocationBO instance = new StockLocationBO();

		protected StockLocationBO()
		{
			this.baseFacade = facade;
		}

		public static StockLocationBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	