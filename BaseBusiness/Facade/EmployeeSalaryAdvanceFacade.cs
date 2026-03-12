
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeSalaryAdvanceFacade : BaseFacade
	{
		protected static EmployeeSalaryAdvanceFacade instance = new EmployeeSalaryAdvanceFacade(new EmployeeSalaryAdvanceModel());
		protected EmployeeSalaryAdvanceFacade(EmployeeSalaryAdvanceModel model) : base(model)
		{
		}
		public static EmployeeSalaryAdvanceFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeSalaryAdvanceFacade():base() 
		{ 
		} 
	
	}
}
	