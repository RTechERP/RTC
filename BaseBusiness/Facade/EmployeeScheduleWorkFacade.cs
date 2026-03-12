
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeScheduleWorkFacade : BaseFacade
	{
		protected static EmployeeScheduleWorkFacade instance = new EmployeeScheduleWorkFacade(new EmployeeScheduleWorkModel());
		protected EmployeeScheduleWorkFacade(EmployeeScheduleWorkModel model) : base(model)
		{
		}
		public static EmployeeScheduleWorkFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeScheduleWorkFacade():base() 
		{ 
		} 
	
	}
}
	