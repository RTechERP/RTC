
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeBO : BaseBO
	{
		private EmployeeFacade facade = EmployeeFacade.Instance;
		protected static EmployeeBO instance = new EmployeeBO();

		protected EmployeeBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	