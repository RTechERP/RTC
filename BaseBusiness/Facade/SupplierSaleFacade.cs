
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class SupplierSaleFacade : BaseFacade
	{
		protected static SupplierSaleFacade instance = new SupplierSaleFacade(new SupplierSaleModel());
		protected SupplierSaleFacade(SupplierSaleModel model) : base(model)
		{
		}
		public static SupplierSaleFacade Instance
		{
			get { return instance; }
		}
		protected SupplierSaleFacade():base() 
		{ 
		} 
	
	}
}
	