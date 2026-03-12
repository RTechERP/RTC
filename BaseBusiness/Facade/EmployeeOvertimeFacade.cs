
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeOvertimeFacade : BaseFacade
	{
		protected static EmployeeOvertimeFacade instance = new EmployeeOvertimeFacade(new EmployeeOvertimeModel());
		protected EmployeeOvertimeFacade(EmployeeOvertimeModel model) : base(model)
		{
		}
		public static EmployeeOvertimeFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeOvertimeFacade():base() 
		{ 
		} 
	
	}
}
	