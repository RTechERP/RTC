
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class EmployeeContractBO : BaseBO
	{
		private EmployeeContractFacade facade = EmployeeContractFacade.Instance;
		protected static EmployeeContractBO instance = new EmployeeContractBO();

		protected EmployeeContractBO()
		{
			this.baseFacade = facade;
		}

		public static EmployeeContractBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	