
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class WarehouseFacade : BaseFacade
	{
		protected static WarehouseFacade instance = new WarehouseFacade(new WarehouseModel());
		protected WarehouseFacade(WarehouseModel model) : base(model)
		{
		}
		public static WarehouseFacade Instance
		{
			get { return instance; }
		}
		protected WarehouseFacade():base() 
		{ 
		} 
	
	}
}
	