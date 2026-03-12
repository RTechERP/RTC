
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeOvertimeBO : BaseBO
	{
		private EmployeeOvertimeFacade facade = EmployeeOvertimeFacade.Instance;
		protected static EmployeeOvertimeBO instance = new EmployeeOvertimeBO();

		protected EmployeeOvertimeBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeOvertimeBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	