
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeTypeOvertimeFacade : BaseFacade
	{
		protected static EmployeeTypeOvertimeFacade instance = new EmployeeTypeOvertimeFacade(new EmployeeTypeOvertimeModel());
		protected EmployeeTypeOvertimeFacade(EmployeeTypeOvertimeModel model) : base(model)
		{
		}
		public static EmployeeTypeOvertimeFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeTypeOvertimeFacade():base() 
		{ 
		} 
	
	}
}
	