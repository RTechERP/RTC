
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeFacade : BaseFacade
	{
		protected static EmployeeFacade instance = new EmployeeFacade(new EmployeeModel());
		protected EmployeeFacade(EmployeeModel model) : base(model)
		{
		}
		public static EmployeeFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeFacade():base() 
		{ 
		} 
	
	}
}
	