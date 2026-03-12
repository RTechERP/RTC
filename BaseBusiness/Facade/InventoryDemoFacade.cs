
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class InventoryDemoFacade : BaseFacade
	{
		protected static InventoryDemoFacade instance = new InventoryDemoFacade(new InventoryDemoModel());
		protected InventoryDemoFacade(InventoryDemoModel model) : base(model)
		{
		}
		public static InventoryDemoFacade Instance
		{
			get { return instance; }
		}
		protected InventoryDemoFacade():base() 
		{ 
		} 
	
	}
}
	