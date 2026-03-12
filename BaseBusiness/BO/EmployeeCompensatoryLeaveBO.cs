
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeCompensatoryLeaveBO : BaseBO
	{
		private EmployeeCompensatoryLeaveFacade facade = EmployeeCompensatoryLeaveFacade.Instance;
		protected static EmployeeCompensatoryLeaveBO instance = new EmployeeCompensatoryLeaveBO();

		protected EmployeeCompensatoryLeaveBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeCompensatoryLeaveBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	