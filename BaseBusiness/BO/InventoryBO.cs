
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class InventoryBO : BaseBO
	{
		private InventoryFacade facade = InventoryFacade.Instance;
		protected static InventoryBO instance = new InventoryBO();

		protected InventoryBO()
		{
			this.baseFacade = facade;
		}

		public static InventoryBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	