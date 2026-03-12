
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class AddressStockFacade : BaseFacade
	{
		protected static AddressStockFacade instance = new AddressStockFacade(new AddressStockModel());
		protected AddressStockFacade(AddressStockModel model) : base(model)
		{
		}
		public static AddressStockFacade Instance
		{
			get { return instance; }
		}
		protected AddressStockFacade():base() 
		{ 
		} 
	
	}
}
	