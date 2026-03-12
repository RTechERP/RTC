
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeTypeBussinessBO : BaseBO
	{
		private EmployeeTypeBussinessFacade facade = EmployeeTypeBussinessFacade.Instance;
		protected static EmployeeTypeBussinessBO instance = new EmployeeTypeBussinessBO();

		protected EmployeeTypeBussinessBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeTypeBussinessBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	