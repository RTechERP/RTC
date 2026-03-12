
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeBussinessBO : BaseBO
	{
		private EmployeeBussinessFacade facade = EmployeeBussinessFacade.Instance;
		protected static EmployeeBussinessBO instance = new EmployeeBussinessBO();

		protected EmployeeBussinessBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeBussinessBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	