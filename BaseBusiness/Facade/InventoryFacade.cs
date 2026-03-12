
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class InventoryFacade : BaseFacade
	{
		protected static InventoryFacade instance = new InventoryFacade(new InventoryModel());
		protected InventoryFacade(InventoryModel model) : base(model)
		{
		}
		public static InventoryFacade Instance
		{
			get { return instance; }
		}
		protected InventoryFacade():base() 
		{ 
		} 
	
	}
}
	