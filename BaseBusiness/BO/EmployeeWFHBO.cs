
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeWFHBO : BaseBO
	{
		private EmployeeWFHFacade facade = EmployeeWFHFacade.Instance;
		protected static EmployeeWFHBO instance = new EmployeeWFHBO();

		protected EmployeeWFHBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeWFHBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	