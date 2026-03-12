
using System;
using System.Collections;
using BMS.Facade;
using BMS.Model;
namespace BMS.Business
{

	
	public class CustomerPartBO : BaseBO
	{
		private CustomerPartFacade facade = CustomerPartFacade.Instance;
		protected static CustomerPartBO instance = new CustomerPartBO();

		protected CustomerPartBO()
		{
			this.baseFacade = facade;
		}

		public static CustomerPartBO Instance
		{
			get { return instance; }
		}
		
	
	}
}
	