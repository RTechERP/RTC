
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeePayrollDetailBO : BaseBO
	{
		private EmployeePayrollDetailFacade facade = EmployeePayrollDetailFacade.Instance;
		protected static EmployeePayrollDetailBO instance = new EmployeePayrollDetailBO();

		protected EmployeePayrollDetailBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeePayrollDetailBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	