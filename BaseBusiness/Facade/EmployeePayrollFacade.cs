
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeePayrollFacade : BaseFacade
	{
		protected static EmployeePayrollFacade instance = new EmployeePayrollFacade(new EmployeePayrollModel());
		protected EmployeePayrollFacade(EmployeePayrollModel model) : base(model)
		{
		}
		public static EmployeePayrollFacade Instance
		{
			get { return instance; }
		}
		protected EmployeePayrollFacade():base() 
		{ 
		} 
	
	}
}
	