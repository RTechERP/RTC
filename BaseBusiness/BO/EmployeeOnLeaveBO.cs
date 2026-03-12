
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeOnLeaveBO : BaseBO
	{
		private EmployeeOnLeaveFacade facade = EmployeeOnLeaveFacade.Instance;
		protected static EmployeeOnLeaveBO instance = new EmployeeOnLeaveBO();

		protected EmployeeOnLeaveBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeOnLeaveBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	