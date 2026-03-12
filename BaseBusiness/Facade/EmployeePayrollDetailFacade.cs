
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeePayrollDetailFacade : BaseFacade
	{
		protected static EmployeePayrollDetailFacade instance = new EmployeePayrollDetailFacade(new EmployeePayrollDetailModel());
		protected EmployeePayrollDetailFacade(EmployeePayrollDetailModel model) : base(model)
		{
		}
		public static EmployeePayrollDetailFacade Instance
		{
			get { return instance; }
		}
		protected EmployeePayrollDetailFacade():base() 
		{ 
		} 
	
	}
}
	