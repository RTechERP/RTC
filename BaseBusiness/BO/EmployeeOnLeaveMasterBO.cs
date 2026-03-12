
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeOnLeaveMasterBO : BaseBO
	{
		private EmployeeOnLeaveMasterFacade facade = EmployeeOnLeaveMasterFacade.Instance;
		protected static EmployeeOnLeaveMasterBO instance = new EmployeeOnLeaveMasterBO();

		protected EmployeeOnLeaveMasterBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeOnLeaveMasterBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	