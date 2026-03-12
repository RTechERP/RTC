
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeApproveBO : BaseBO
	{
		private EmployeeApproveFacade facade = EmployeeApproveFacade.Instance;
		protected static EmployeeApproveBO instance = new EmployeeApproveBO();

		protected EmployeeApproveBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeApproveBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	