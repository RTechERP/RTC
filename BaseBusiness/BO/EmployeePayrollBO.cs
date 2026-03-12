
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeePayrollBO : BaseBO
	{
		private EmployeePayrollFacade facade = EmployeePayrollFacade.Instance;
		protected static EmployeePayrollBO instance = new EmployeePayrollBO();

		protected EmployeePayrollBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeePayrollBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	