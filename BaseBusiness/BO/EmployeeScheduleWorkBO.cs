
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeScheduleWorkBO : BaseBO
	{
		private EmployeeScheduleWorkFacade facade = EmployeeScheduleWorkFacade.Instance;
		protected static EmployeeScheduleWorkBO instance = new EmployeeScheduleWorkBO();

		protected EmployeeScheduleWorkBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeScheduleWorkBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	