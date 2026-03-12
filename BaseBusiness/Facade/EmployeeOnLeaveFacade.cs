
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeOnLeaveFacade : BaseFacade
	{
		protected static EmployeeOnLeaveFacade instance = new EmployeeOnLeaveFacade(new EmployeeOnLeaveModel());
		protected EmployeeOnLeaveFacade(EmployeeOnLeaveModel model) : base(model)
		{
		}
		public static EmployeeOnLeaveFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeOnLeaveFacade():base() 
		{ 
		} 
	
	}
}
	