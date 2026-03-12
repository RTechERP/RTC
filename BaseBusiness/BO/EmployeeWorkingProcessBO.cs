
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeWorkingProcessBO : BaseBO
	{
		private EmployeeWorkingProcessFacade facade = EmployeeWorkingProcessFacade.Instance;
		protected static EmployeeWorkingProcessBO instance = new EmployeeWorkingProcessBO();

		protected EmployeeWorkingProcessBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeWorkingProcessBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	