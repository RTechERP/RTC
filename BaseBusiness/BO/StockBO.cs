
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class StockBO : BaseBO
	{
		private StockFacade facade = StockFacade.Instance;
		protected static StockBO instance = new StockBO();

		protected StockBO()
		{
			this.baseFacade = facade;
		}

		public static StockBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	