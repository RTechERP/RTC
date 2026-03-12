
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class InventoryStockFacade : BaseFacade
	{
		protected static InventoryStockFacade instance = new InventoryStockFacade(new InventoryStockModel());
		protected InventoryStockFacade(InventoryStockModel model) : base(model)
		{
		}
		public static InventoryStockFacade Instance
		{
			get { return instance; }
		}
		protected InventoryStockFacade():base() 
		{ 
		} 
	
	}
}
	