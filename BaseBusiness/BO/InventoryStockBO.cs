
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class InventoryStockBO : BaseBO
	{
		private InventoryStockFacade facade = InventoryStockFacade.Instance;
		protected static InventoryStockBO instance = new InventoryStockBO();

		protected InventoryStockBO()
		{
			this.baseFacade = facade;
		}

		public static InventoryStockBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	