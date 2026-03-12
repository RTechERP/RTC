
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class DebtSupplierFacade : BaseFacade
	{
		protected static DebtSupplierFacade instance = new DebtSupplierFacade(new DebtSupplierModel());
		protected DebtSupplierFacade(DebtSupplierModel model) : base(model)
		{
		}
		public static DebtSupplierFacade Instance
		{
			get { return instance; }
		}
		protected DebtSupplierFacade():base() 
		{ 
		} 
	
	}
}
	