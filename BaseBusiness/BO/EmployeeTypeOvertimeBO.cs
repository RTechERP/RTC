
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeTypeOvertimeBO : BaseBO
	{
		private EmployeeTypeOvertimeFacade facade = EmployeeTypeOvertimeFacade.Instance;
		protected static EmployeeTypeOvertimeBO instance = new EmployeeTypeOvertimeBO();

		protected EmployeeTypeOvertimeBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeTypeOvertimeBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	