
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeEarlyLateBO : BaseBO
	{
		private EmployeeEarlyLateFacade facade = EmployeeEarlyLateFacade.Instance;
		protected static EmployeeEarlyLateBO instance = new EmployeeEarlyLateBO();

		protected EmployeeEarlyLateBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeEarlyLateBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	