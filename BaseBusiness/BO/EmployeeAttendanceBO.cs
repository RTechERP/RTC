
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeAttendanceBO : BaseBO
	{
		private EmployeeAttendanceFacade facade = EmployeeAttendanceFacade.Instance;
		protected static EmployeeAttendanceBO instance = new EmployeeAttendanceBO();

		protected EmployeeAttendanceBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeAttendanceBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	