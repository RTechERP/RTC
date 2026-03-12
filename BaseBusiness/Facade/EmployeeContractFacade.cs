
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeContractFacade : BaseFacade
	{
		protected static EmployeeContractFacade instance = new EmployeeContractFacade(new EmployeeContractModel());
		protected EmployeeContractFacade(EmployeeContractModel model) : base(model)
		{
		}
		public static EmployeeContractFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeContractFacade():base() 
		{ 
		} 
	
	}
}
	