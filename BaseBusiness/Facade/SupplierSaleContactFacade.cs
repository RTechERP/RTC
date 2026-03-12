
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class SupplierSaleContactFacade : BaseFacade
	{
		protected static SupplierSaleContactFacade instance = new SupplierSaleContactFacade(new SupplierSaleContactModel());
		protected SupplierSaleContactFacade(SupplierSaleContactModel model) : base(model)
		{
		}
		public static SupplierSaleContactFacade Instance
		{
			get { return instance; }
		}
		protected SupplierSaleContactFacade():base() 
		{ 
		} 
	
	}
}
	