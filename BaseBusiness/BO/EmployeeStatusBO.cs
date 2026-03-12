
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeStatusBO : BaseBO
	{
		private EmployeeStatusFacade facade = EmployeeStatusFacade.Instance;
		protected static EmployeeStatusBO instance = new EmployeeStatusBO();

		protected EmployeeStatusBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeStatusBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	