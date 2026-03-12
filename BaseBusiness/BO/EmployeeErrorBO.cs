
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeErrorBO : BaseBO
	{
		private EmployeeErrorFacade facade = EmployeeErrorFacade.Instance;
		protected static EmployeeErrorBO instance = new EmployeeErrorBO();

		protected EmployeeErrorBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeErrorBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	