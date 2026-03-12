
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class TaxEmployeeContractFacade : BaseFacade
	{
		protected static TaxEmployeeContractFacade instance = new TaxEmployeeContractFacade(new TaxEmployeeContractModel());
		protected TaxEmployeeContractFacade(TaxEmployeeContractModel model) : base(model)
		{
		}
		public static TaxEmployeeContractFacade Instance
		{
			get { return instance; }
		}
		protected TaxEmployeeContractFacade():base() 
		{ 
		} 
	
	}
}
	