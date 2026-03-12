
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class SupplierContactFacade : BaseFacade
	{
		protected static SupplierContactFacade instance = new SupplierContactFacade(new SupplierContactModel());
		protected SupplierContactFacade(SupplierContactModel model) : base(model)
		{
		}
		public static SupplierContactFacade Instance
		{
			get { return instance; }
		}
		protected SupplierContactFacade():base() 
		{ 
		} 
	
	}
}
	