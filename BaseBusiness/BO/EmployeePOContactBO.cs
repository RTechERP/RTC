
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeePOContactBO : BaseBO
	{
		private EmployeePOContactFacade facade = EmployeePOContactFacade.Instance;
		protected static EmployeePOContactBO instance = new EmployeePOContactBO();

		protected EmployeePOContactBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeePOContactBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	