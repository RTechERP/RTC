
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class CustomerEmployeeBO : BaseBO
	{
		private CustomerEmployeeFacade facade = CustomerEmployeeFacade.Instance;
		protected static CustomerEmployeeBO instance = new CustomerEmployeeBO();

		protected CustomerEmployeeBO()
		{
			this.baseFacade = facade;
		}

		public static CustomerEmployeeBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	