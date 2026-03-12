
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class InventoryDemoBO : BaseBO
	{
		private InventoryDemoFacade facade = InventoryDemoFacade.Instance;
		protected static InventoryDemoBO instance = new InventoryDemoBO();

		protected InventoryDemoBO()
		{
			this.baseFacade = facade;
		}

		public static InventoryDemoBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	