
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeSalaryAdvanceBO : BaseBO
	{
		private EmployeeSalaryAdvanceFacade facade = EmployeeSalaryAdvanceFacade.Instance;
		protected static EmployeeSalaryAdvanceBO instance = new EmployeeSalaryAdvanceBO();

		protected EmployeeSalaryAdvanceBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeSalaryAdvanceBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	