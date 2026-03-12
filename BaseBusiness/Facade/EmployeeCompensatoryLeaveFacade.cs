
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeCompensatoryLeaveFacade : BaseFacade
	{
		protected static EmployeeCompensatoryLeaveFacade instance = new EmployeeCompensatoryLeaveFacade(new EmployeeCompensatoryLeaveModel());
		protected EmployeeCompensatoryLeaveFacade(EmployeeCompensatoryLeaveModel model) : base(model)
		{
		}
		public static EmployeeCompensatoryLeaveFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeCompensatoryLeaveFacade():base() 
		{ 
		} 
	
	}
}
	