
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeRegisterWorkBO : BaseBO
	{
		private EmployeeRegisterWorkFacade facade = EmployeeRegisterWorkFacade.Instance;
		protected static EmployeeRegisterWorkBO instance = new EmployeeRegisterWorkBO();

		protected EmployeeRegisterWorkBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeRegisterWorkBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	