
using System.Collections;
using BMS.Model;
namespace BMS.Facade
{
	
	public class EmployeeAttendanceFacade : BaseFacade
	{
		protected static EmployeeAttendanceFacade instance = new EmployeeAttendanceFacade(new EmployeeAttendanceModel());
		protected EmployeeAttendanceFacade(EmployeeAttendanceModel model) : base(model)
		{
		}
		public static EmployeeAttendanceFacade Instance
		{
			get { return instance; }
		}
		protected EmployeeAttendanceFacade():base() 
		{ 
		} 
	
	}
}
	