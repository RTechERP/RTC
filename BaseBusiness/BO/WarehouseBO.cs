
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class WarehouseBO : BaseBO
	{
		private WarehouseFacade facade = WarehouseFacade.Instance;
		protected static WarehouseBO instance = new WarehouseBO();

		protected WarehouseBO()
		{
			this.baseFacade = facade;
		}

		public static WarehouseBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	