
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeFoodOrderBO : BaseBO
	{
		private EmployeeFoodOrderFacade facade = EmployeeFoodOrderFacade.Instance;
		protected static EmployeeFoodOrderBO instance = new EmployeeFoodOrderBO();

		protected EmployeeFoodOrderBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeFoodOrderBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	